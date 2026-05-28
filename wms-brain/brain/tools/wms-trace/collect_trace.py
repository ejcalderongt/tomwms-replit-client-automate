#!/usr/bin/env python3
"""
collect_trace.py — Colector multi-fuente de trazas WMS.
Correlaciona HH (logcat), WebService (rolling file), BOF (rolling file)
por TraceId y genera un artefacto YAML computable por el brain.

Uso:
    # Modo trace-id exacto (correlación perfecta):
    python3 collect_trace.py \\
        --hh-log  trace_hh_20260528.log \\
        --ws-log  wms-ws-trace-20260528.log \\
        --bof-log wms-bof-trace-20260528.log \\
        --out     traces/

    # Modo fecha (analiza todos los traces del día):
    python3 collect_trace.py \\
        --ws-log  wms-ws-trace-20260528.log \\
        --bof-log wms-bof-trace-20260528.log \\
        --date    20260528 \\
        --out     traces/

    # Modo 'last N' (últimas N operaciones del WS):
    python3 collect_trace.py \\
        --ws-log  wms-ws-trace-20260528.log \\
        --last    10 \\
        --out     traces/

Output por cada trace:
    traces/session-{trace_id}.yml    ← computable por brain
    traces/analysis-{trace_id}.md   ← legible por humano
    traces/index-{date}.md          ← índice del día

#EJC20260528
"""

import sys, re, os, json, yaml
import collections, statistics
from pathlib import Path
from datetime import datetime, timezone
import argparse, textwrap

# ── Regex por capa ─────────────────────────────────────────────────────────────

# HH (logcat WMS-T format)
RE_HH  = re.compile(r'(\d{2}-\d{2} \d{2}:\d{2}:\d{2}\.\d{3}).*WMS-T\s*:\s*(.*)')
RE_HH_EXEC = re.compile(r'>> EXEC case=(\d+) #(\d+).*th=(\S+)')
RE_HH_OK   = re.compile(r'<< OK\s+case=(\d+) dt=(-?\d+)ms')
RE_HH_ERR  = re.compile(r'<< ERR\s+case=(\d+) dt=(-?\d+)ms.*err=(.*)')
RE_HH_OVER = re.compile(r'!! OVERLAP case=(\d+)')

# WS (wms-ws-trace-*.log)
RE_WS      = re.compile(r'(\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}\.\d{3})\s+(\w+)\s+([\w<>! ]+?)\s{2,}(.*)')
RE_WS_TID  = re.compile(r'trace=([A-Za-z0-9\-]+)')
RE_WS_METH = re.compile(r'meth=(\S+)')
RE_WS_DT   = re.compile(r'dt=(-?\d+)ms')
RE_WS_ROWS = re.compile(r'rows=(-?\d+)')
RE_WS_LEN  = re.compile(r'len=(-?\d+)b')
RE_WS_SP   = re.compile(r'sp=(\S+)')
RE_WS_STAT = re.compile(r'status=(\d+)')

# BOF (wms-bof-trace-*.log) — mismo formato que WS
RE_BOF     = RE_WS  # reutilizar

# ── Estructuras ────────────────────────────────────────────────────────────────

class TraceSession:
    def __init__(self, trace_id):
        self.trace_id = trace_id
        self.start_ms = None
        self.end_ms = None
        self.method = None
        self.status = "UNKNOWN"
        self.status_code = 200
        self.response_bytes = 0
        self.hh_events = []
        self.ws_events = []
        self.bof_events = []
        self.bof_ops = []
        self.bof_txs = []
        self.sql_calls = []
        self.anomalies = []
        self.hh_case = None
        self.hh_dt_ms = None

# ── Parsers ────────────────────────────────────────────────────────────────────

def parse_ws_log(filepath):
    """Parsea wms-ws-trace-*.log, devuelve dict trace_id → TraceSession."""
    sessions = {}
    if not filepath or not Path(filepath).exists():
        return sessions

    with open(filepath, encoding="utf-8", errors="replace") as f:
        for raw in f:
            m = RE_WS.match(raw.rstrip())
            if not m:
                continue
            ts, layer, tag, body = m.group(1), m.group(2), m.group(3).strip(), m.group(4)

            tid_m = RE_WS_TID.search(body)
            if not tid_m:
                continue
            tid = tid_m.group(1)

            if tid not in sessions:
                sessions[tid] = TraceSession(tid)
            s = sessions[tid]

            meth_m = RE_WS_METH.search(body)
            dt_m   = RE_WS_DT.search(body)
            sp_m   = RE_WS_SP.search(body)
            rows_m = RE_WS_ROWS.search(body)
            len_m  = RE_WS_LEN.search(body)
            stat_m = RE_WS_STAT.search(body)

            dt = int(dt_m.group(1)) if dt_m else -1

            if layer == "WS" and ">>" in tag:
                s.method = meth_m.group(1) if meth_m else None
                s.start_ms = ts
                s.ws_events.append({"t": 0, "type": "REQUEST_IN", "method": s.method})

            elif layer == "WS" and "<<" in tag:
                s.end_ms = ts
                s.status = "OK" if "OK" in tag else "ERROR"
                s.ws_events.append({"t": dt, "type": "RESPONSE_OUT",
                                    "status": s.status, "duration_ms": dt})

            elif layer == "RS" and ">>" in tag:
                if stat_m:
                    s.status_code = int(stat_m.group(1))
                if len_m:
                    s.response_bytes = int(len_m.group(1))

            elif layer == "SQL" and ">>" in tag:
                sp = sp_m.group(1) if sp_m else "?"
                s.sql_calls.append({"sp": sp, "start": ts, "duration_ms": None, "rows": None})

            elif layer == "SQL" and "<<" in tag:
                sp = sp_m.group(1) if sp_m else "?"
                rows = int(rows_m.group(1)) if rows_m else -1
                # Match to open call
                for call in reversed(s.sql_calls):
                    if call["sp"] == sp and call["duration_ms"] is None:
                        call["duration_ms"] = dt
                        call["rows"] = rows
                        break
                else:
                    s.sql_calls.append({"sp": sp, "duration_ms": dt, "rows": rows})

            elif layer == "!!":
                s.anomalies.append({"source": "WS", "tag": tag, "msg": body, "ts": ts})

    return sessions


def parse_bof_log(filepath, ws_sessions):
    """Parsea wms-bof-trace-*.log, agrega datos de BOF a las sesiones WS existentes."""
    if not filepath or not Path(filepath).exists():
        return

    with open(filepath, encoding="utf-8", errors="replace") as f:
        for raw in f:
            m = RE_BOF.match(raw.rstrip())
            if not m:
                continue
            ts, layer, tag, body = m.group(1), m.group(2), m.group(3).strip(), m.group(4)

            tid_m = RE_WS_TID.search(body)
            tid = tid_m.group(1) if tid_m else None
            dt_m = RE_WS_DT.search(body)
            dt = int(dt_m.group(1)) if dt_m else -1

            # Buscar sesión por trace_id
            s = ws_sessions.get(tid) if tid else None
            if s is None:
                continue

            if layer == "OP" and ">>" in tag:
                op_m = re.search(r'op=(\S+)', body)
                op = op_m.group(1) if op_m else "?"
                s.bof_events.append({"type": "OP_START", "op": op, "ts": ts})

            elif layer == "OP" and "<<" in tag:
                op_m   = re.search(r'op=(\S+)', body)
                sql_m  = re.search(r'sql_roundtrips=(\d+)', body)
                op  = op_m.group(1) if op_m else "?"
                sqls = int(sql_m.group(1)) if sql_m else 0
                ok = "OK" if "OK" in tag else "ERR"
                s.bof_ops.append({"name": op, "duration_ms": dt, "sql_roundtrips": sqls, "status": ok})
                s.bof_events.append({"type": "OP_END", "op": op, "status": ok, "duration_ms": dt, "ts": ts})

            elif layer == "TX":
                if tag == "BEGIN":
                    s.bof_txs.append({"begin_ts": ts, "commit_ts": None, "duration_ms": None})
                elif tag == "COMMIT":
                    for tx in reversed(s.bof_txs):
                        if tx["commit_ts"] is None:
                            tx["commit_ts"] = ts
                            tx["duration_ms"] = dt
                            break
                elif tag == "ROLLBACK":
                    s.bof_txs.append({"begin_ts": None, "rollback_ts": ts, "reason": body})

            elif layer == "SQL" and ">>" in tag:
                sp_m = RE_WS_SP.search(body)
                sp = sp_m.group(1) if sp_m else "?"
                s.bof_events.append({"type": "SQL_START", "sp": sp, "ts": ts})

            elif layer == "SQL" and "<<" in tag:
                sp_m   = RE_WS_SP.search(body)
                rows_m = RE_WS_ROWS.search(body)
                sp   = sp_m.group(1) if sp_m else "?"
                rows = int(rows_m.group(1)) if rows_m else -1
                s.sql_calls.append({"sp": sp, "source": "BOF", "duration_ms": dt, "rows": rows})
                s.bof_events.append({"type": "SQL_END", "sp": sp, "rows": rows, "duration_ms": dt, "ts": ts})

            elif layer == "!!":
                s.anomalies.append({"source": "BOF", "tag": tag, "msg": body, "ts": ts})


def parse_hh_log(filepath, ws_sessions):
    """Parsea logcat HH (WMS-T), correlaciona por proximidad temporal con sesiones WS."""
    if not filepath or not Path(filepath).exists():
        return

    hh_events = []
    with open(filepath, encoding="utf-8", errors="replace") as f:
        for raw in f:
            m = RE_HH.search(raw.rstrip())
            if not m:
                continue
            ts, msg = m.group(1), m.group(2)
            hh_events.append((ts, msg))

    # Correlación temporal simple: buscar WS session cuyo start_ms coincide ±2s
    for s in ws_sessions.values():
        if not s.start_ms:
            continue
        # Extraer hora del WS start
        try:
            ws_hh = s.start_ms[11:23]  # HH:MM:SS.mmm
        except Exception:
            continue
        for ts, msg in hh_events:
            hh_ts = ts[3:]  # skip MM-DD, get HH:MM:SS.mmm
            if abs(_ts_diff_ms(ws_hh, hh_ts)) < 2000:
                oe_m = RE_HH_EXEC.search(msg)
                ok_m = RE_HH_OK.search(msg)
                er_m = RE_HH_ERR.search(msg)
                ov_m = RE_HH_OVER.search(msg)
                if oe_m:
                    s.hh_case = int(oe_m.group(1))
                    s.hh_events.append({"type": "EXEC", "case": s.hh_case, "ts": ts})
                elif ok_m:
                    s.hh_dt_ms = int(ok_m.group(2))
                    s.hh_events.append({"type": "OK", "case": int(ok_m.group(1)), "dt_ms": s.hh_dt_ms, "ts": ts})
                elif er_m:
                    s.hh_events.append({"type": "ERR", "case": int(er_m.group(1)), "dt_ms": int(er_m.group(2)), "err": er_m.group(3)[:80], "ts": ts})
                elif ov_m:
                    s.anomalies.append({"source": "HH", "tag": "OVERLAP", "case": int(ov_m.group(1)), "ts": ts})


def _ts_diff_ms(a, b):
    """Diferencia aproximada en ms entre dos timestamps HH:MM:SS.mmm."""
    try:
        ta = [int(x) for x in re.split(r'[:\.]', a[:12])]
        tb = [int(x) for x in re.split(r'[:\.]', b[:12])]
        return ((ta[0]-tb[0])*3600000 + (ta[1]-tb[1])*60000 +
                (ta[2]-tb[2])*1000 + (ta[3]-tb[3]))
    except Exception:
        return 9999999


# ── YAML output ────────────────────────────────────────────────────────────────

def build_yaml(s):
    """Construye el artefacto YAML para una TraceSession."""
    duration_ms = -1
    try:
        if s.bof_ops:
            duration_ms = max(op["duration_ms"] for op in s.bof_ops if op["duration_ms"])
        elif s.ws_events:
            resp = next((e for e in s.ws_events if e["type"] == "RESPONSE_OUT"), None)
            if resp:
                duration_ms = resp.get("duration_ms", -1)
    except Exception:
        pass

    # Detección N+1
    sp_counts = collections.Counter(c["sp"] for c in s.sql_calls if "sp" in c)
    for sp, cnt in sp_counts.items():
        if cnt > 3:
            s.anomalies.append({
                "type": "N+1",
                "sp": sp,
                "count": cnt,
                "recommendation": f"Consolidar {cnt} llamadas a {sp} en un SELECT...WHERE id IN (...) o TVP"
            })

    # Detección TX lenta
    for tx in s.bof_txs:
        dt = tx.get("duration_ms", 0) or 0
        if dt > 5000:
            s.anomalies.append({
                "type": "SLOW_TX",
                "duration_ms": dt,
                "recommendation": "Revisar SP con CURSOR o locks dentro del bloque transaccional"
            })

    # Tablas tocadas (inferidas de SP names)
    tables_touched = set()
    sp_table_hints = {
        "trans_re_enc": ["Trans_Re_Enc", "Recepcion_Enc"],
        "trans_re_det": ["Trans_Re_Det", "Recepcion_Det"],
        "stock_rec":    ["Stock_Rec", "StockRec"],
        "trans_picking_ubic": ["Picking_Ubic", "PickingUbic"],
        "trans_oc_enc": ["Pedido_Compra", "OC_Enc"],
    }
    for call in s.sql_calls:
        sp = call.get("sp", "")
        for table, hints in sp_table_hints.items():
            if any(h.lower() in sp.lower() for h in hints):
                tables_touched.add(table)

    # State path
    state_path = []
    if s.hh_case:
        state_path.append(f"HH.EXEC(case={s.hh_case})")
    if s.method:
        state_path.append(f"WS.{s.method}")
    for op in s.bof_ops:
        state_path.append(f"LN.{op['name']}")
    if s.bof_txs:
        state_path.append("TX.BEGIN")
        if s.sql_calls:
            unique_sps = list(dict.fromkeys(c["sp"] for c in s.sql_calls))
            for sp in unique_sps[:5]:
                cnt = sum(1 for c in s.sql_calls if c.get("sp") == sp)
                state_path.append(f"SQL.{sp}{f' ×{cnt}' if cnt > 1 else ''}")
        state_path.append("TX.COMMIT" if any(t.get("commit_ts") for t in s.bof_txs) else "TX.ROLLBACK")
    if s.status:
        state_path.append(f"WS.{s.status}({s.status_code})")
    if s.hh_case and s.hh_dt_ms:
        state_path.append(f"HH.CALLBACK(case={s.hh_case},OK)")

    doc = {
        "trace_id": s.trace_id,
        "timestamp_start": s.start_ms or "?",
        "duration_ms": duration_ms,
        "status": s.status,
        "status_code": s.status_code,
        "operation": {
            "name": s.method or "?",
            "source": f"HH.case={s.hh_case}" if s.hh_case else "WS",
        },
        "layers": {
            "hh": {
                "case": s.hh_case,
                "duration_ms": s.hh_dt_ms,
                "events": s.hh_events[:20],
            },
            "webservice": {
                "method": s.method,
                "duration_ms": next((e.get("duration_ms") for e in s.ws_events if e["type"] == "RESPONSE_OUT"), None),
                "response_bytes": s.response_bytes,
                "status_code": s.status_code,
                "sql_count": len([c for c in s.sql_calls if c.get("source") != "BOF"]),
            },
            "bof": {
                "operations": s.bof_ops,
                "transactions": s.bof_txs,
                "sql_count": len([c for c in s.sql_calls if c.get("source") == "BOF"]),
            },
        },
        "sql_calls": s.sql_calls[:50],
        "data_touched": {
            "tables": [{"name": t, "inferred": True} for t in sorted(tables_touched)],
        },
        "state_path": state_path,
        "anomalies": s.anomalies,
        "span_tree": getattr(s, "span_tree", []) or [],
        "brain": {
            "analyzed": False,
            "support_tickets": [],
            "suggested_improvements": [],
        }
    }
    return doc


def build_md(s, yaml_doc):
    """Genera el MD legible para humano."""
    lines = [
        f"# Trace {s.trace_id}",
        f"",
        f"| Campo | Valor |",
        f"|---|---|",
        f"| TraceId | `{s.trace_id}` |",
        f"| Inicio | {s.start_ms or '?'} |",
        f"| Duración | {yaml_doc['duration_ms']}ms |",
        f"| Estado | {s.status} ({s.status_code}) |",
        f"| Método WS | `{s.method or '?'}` |",
        f"| Caso HH | {s.hh_case or '?'} |",
        f"",
        f"## Ruta de estado",
        f"",
        f"```",
        " → ".join(yaml_doc["state_path"]) if yaml_doc["state_path"] else "(sin datos)",
        f"```",
        f"",
    ]

    if s.anomalies:
        lines += ["## Anomalías detectadas", ""]
        for a in s.anomalies:
            lines.append(f"- **{a.get('tag') or a.get('type')}** ({a.get('source','?')}): {a.get('msg') or a.get('recommendation','')}")
        lines.append("")

    if s.bof_ops:
        lines += ["## Operaciones BOF/LN", "", f"| Operación | dt ms | SQL calls | Status |", "|---|---|---|---|"]
        for op in s.bof_ops:
            lines.append(f"| {op['name']} | {op['duration_ms']} | {op['sql_roundtrips']} | {op['status']} |")
        lines.append("")

    if s.sql_calls:
        lines += ["## SQL calls (top por duración)", "", "| SP | dt ms | rows |", "|---|---|---|"]
        for call in sorted(s.sql_calls, key=lambda x: -(x.get("duration_ms") or 0))[:10]:
            lines.append(f"| `{call.get('sp','?')}` | {call.get('duration_ms','-')} | {call.get('rows','-')} |")
        lines.append("")

    lines += [
        "## Brain inbox",
        "",
        f"Este trace puede ser analizado por el brain. Copiar el YAML a `wms-brain/brain/_inbox/`.",
    ]
    return "\n".join(lines)




# ── v2: Span tree builder (OTel-inspired) ─────────────────────────────────────

# Regex para parsear líneas v2 con span= parent= name=
RE_SPAN_OPEN  = re.compile(r'span=([a-f0-9]{8,16})\s+parent=([a-f0-9]{0,16})\s+trace=(\S+)\s+name=(\S+)(.*)')
RE_SPAN_CLOSE = re.compile(r'span=([a-f0-9]{8,16})\s+status=(\S+)\s+dt=(-?\d+)ms(.*)')
RE_ATTRS      = re.compile(r'(\w[\w.]+)=(\S+)')

def parse_log_v2(filepath):
    """
    Parsea logs v2 (WmsTrace_v2.vb / WmsTraceWS_v2.vb).
    Formato: YYYY-MM-DD HH:mm:ss.fff  LAYER  TAG          span=XXXX parent=YYYY trace=ZZZ name=... [attrs]
    Devuelve dict: trace_id → lista de spans (flat), para luego armar el árbol.
    """
    spans_by_trace = {}   # trace_id → {span_id: span_dict}
    if not filepath or not Path(filepath).exists():
        return spans_by_trace

    with open(filepath, encoding="utf-8", errors="replace") as f:
        for raw in f:
            m = RE_WS.match(raw.rstrip())
            if not m:
                continue
            ts, layer, tag, body = m.group(1), m.group(2), m.group(3).strip(), m.group(4)

            if ">>" in tag and "span=" in body:
                sm = RE_SPAN_OPEN.search(body)
                if not sm:
                    continue
                sid, parent, tid, name = sm.group(1), sm.group(2), sm.group(3), sm.group(4)
                # Parsear atributos
                attrs = {}
                for am in RE_ATTRS.finditer(sm.group(5)):
                    attrs[am.group(1)] = am.group(2)

                if tid not in spans_by_trace:
                    spans_by_trace[tid] = {}
                spans_by_trace[tid][sid] = {
                    "span_id": sid,
                    "parent_span_id": parent,
                    "trace_id": tid,
                    "name": name,
                    "start_ts": ts,
                    "end_ts": None,
                    "duration_ms": None,
                    "status": None,
                    "layer": layer,
                    "attributes": attrs,
                    "children": [],
                    "anomalies": [],
                }

            elif "<<" in tag and "span=" in body:
                sm = RE_SPAN_CLOSE.search(body)
                if not sm:
                    continue
                sid, status, dt = sm.group(1), sm.group(2), int(sm.group(3))
                for trace_spans in spans_by_trace.values():
                    if sid in trace_spans:
                        trace_spans[sid]["end_ts"] = ts
                        trace_spans[sid]["duration_ms"] = dt
                        trace_spans[sid]["status"] = status
                        break

            elif "!!" in layer:
                # Anomalía: buscar el span activo más cercano por trace
                tid_m = RE_WS_TID.search(body)
                if tid_m and tid_m.group(1) in spans_by_trace:
                    # Agregar al último span del trace
                    trace_spans = spans_by_trace[tid_m.group(1)]
                    if trace_spans:
                        last_sid = list(trace_spans.keys())[-1]
                        trace_spans[last_sid]["anomalies"].append({
                            "tag": tag, "msg": body[:100], "ts": ts
                        })

    return spans_by_trace


def build_span_tree(spans_dict):
    """
    Convierte dict {span_id: span} en árbol anidado (children).
    Devuelve lista de spans raíz (parent_span_id = '').
    """
    by_id = dict(spans_dict)  # copia
    roots = []

    for sid, span in by_id.items():
        pid = span.get("parent_span_id", "")
        if pid and pid in by_id:
            by_id[pid]["children"].append(span)
        else:
            roots.append(span)

    return roots


def enrich_session_with_spans_v2(session, log_path):
    """
    Enriquece una TraceSession existente con los Spans v2 del log.
    Agrega el árbol de spans al campo 'span_tree'.
    """
    if not log_path or not Path(log_path).exists():
        return
    all_traces = parse_log_v2(log_path)
    spans_dict = all_traces.get(session.trace_id, {})
    if not spans_dict:
        return
    session.span_tree = build_span_tree(spans_dict)
    session.spans_flat = list(spans_dict.values())

    # Detección N+1 en el árbol: hermanos con mismo name
    def find_n1(node):
        by_name = {}
        for child in node.get("children", []):
            by_name.setdefault(child["name"], []).append(child)
        for name, siblings in by_name.items():
            if len(siblings) > 3:
                node.setdefault("anomalies", []).append({
                    "type": "N+1", "sp": name, "count": len(siblings),
                    "recommendation": f"Consolidar {len(siblings)} spans de '{name}' en batch"
                })
        for child in node.get("children", []):
            find_n1(child)
    for root in (session.span_tree if hasattr(session, "span_tree") else []):
        find_n1(root)

# ── Main ───────────────────────────────────────────────────────────────────────

def main():
    ap = argparse.ArgumentParser(description=__doc__, formatter_class=argparse.RawDescriptionHelpFormatter)
    ap.add_argument("--hh-log",  help="Logcat HH (WMS-T lines)")
    ap.add_argument("--ws-log",  required=True, help="wms-ws-trace-*.log")
    ap.add_argument("--bof-log", help="wms-bof-trace-*.log")
    ap.add_argument("--out",     default="traces", help="Directorio de salida")
    ap.add_argument("--last",    type=int, help="Analizar solo las últimas N sesiones WS")
    ap.add_argument("--trace-id", help="Analizar solo este TraceId exacto")
    args = ap.parse_args()

    out_dir = Path(args.out)
    out_dir.mkdir(parents=True, exist_ok=True)

    print(f"\nParsing WS log: {args.ws_log}")
    sessions = parse_ws_log(args.ws_log)
    print(f"  → {len(sessions)} trace sessions encontradas")

    if args.bof_log:
        print(f"Parsing BOF log: {args.bof_log}")
        parse_bof_log(args.bof_log, sessions)

    if args.hh_log:
        print(f"Parsing HH logcat: {args.hh_log}")
        parse_hh_log(args.hh_log, sessions)

    # Filtrado
    if args.trace_id:
        sessions = {k: v for k, v in sessions.items() if k == args.trace_id}
    elif args.last:
        items = sorted(sessions.items(), key=lambda x: x[1].start_ms or "")[-args.last:]
        sessions = dict(items)

    print(f"\nGenerando artefactos para {len(sessions)} sesiones → {out_dir}/")

    index_lines = [f"# Trace Index — {datetime.now():%Y-%m-%d}", ""]
    for tid, s in sorted(sessions.items(), key=lambda x: x[1].start_ms or ""):
        yaml_doc = build_yaml(s)
        md_doc   = build_md(s, yaml_doc)

        yaml_path = out_dir / f"session-{tid}.yml"
        md_path   = out_dir / f"analysis-{tid}.md"

        with open(yaml_path, "w", encoding="utf-8") as f:
            yaml.dump(yaml_doc, f, allow_unicode=True, sort_keys=False, default_flow_style=False)
        with open(md_path, "w", encoding="utf-8") as f:
            f.write(md_doc)

        dur = yaml_doc.get("duration_ms", "?")
        status = yaml_doc.get("status", "?")
        method = yaml_doc.get("operation", {}).get("name", "?")
        anom = len(yaml_doc.get("anomalies", []))
        flag = " ← ANOMALÍAS" if anom > 0 else ""
        flag += " ← SLOW" if isinstance(dur, int) and dur > 5000 else ""
        print(f"  {tid}  {method:<45} {status} {dur}ms  anomalías={anom}{flag}")
        index_lines.append(f"- [{tid}](session-{tid}.yml) — `{method}` {status} {dur}ms  anomalías={anom}{flag}")

    idx_path = out_dir / f"index-{datetime.now():%Y%m%d}.md"
    with open(idx_path, "w", encoding="utf-8") as f:
        f.write("\n".join(index_lines))

    print(f"\n✓ Artefactos en {out_dir}/")
    print(f"  Índice: {idx_path}")
    if any(len(build_yaml(s).get("anomalies", [])) > 0 for s in sessions.values()):
        print("  ⚠️  Se detectaron anomalías — revisar archivos marcados con 'ANOMALÍAS'")


if __name__ == "__main__":
    try:
        import yaml
    except ImportError:
        print("ERROR: falta pyyaml — instalar con: pip install pyyaml")
        sys.exit(1)
    main()
