#!/usr/bin/env python3
"""
parse_trace_bof.py — Analizador de WmsTrace BOF/MI3 para TOM WMS
Uso: python3 parse_trace_bof.py <log_file>

Archivo generado en: C:\\TOM\\Logs\\wms-trace-YYYYMMDD.log

Formato de líneas:
  2026-05-28 14:23:01.123  OP  >>           op=Procesar_Pedido_Compra_MI3 ctx=OC-0001 th=main
  2026-05-28 14:23:02.890  OP  << OK        op=Procesar_Pedido_Compra_MI3 dt=1767ms sql_roundtrips=4 ctx=OC-0001
  2026-05-28 14:23:01.125  TX  BEGIN        ctx=OC-0001 th=main
  2026-05-28 14:23:02.100  TX  COMMIT       ctx=OC-0001 dt=975ms
  2026-05-28 14:23:01.200  MI  >>           svc=PedidoCompra meth=Insert key=OC-0001
  2026-05-28 14:23:02.950  MI  << OK        svc=PedidoCompra meth=Insert key=OC-0001 dt=1750ms
  2026-05-28 14:23:01.150  SQL >>           sp=sp_Insert_Trans_Re_Det ctx=GuardarRecepcion
  2026-05-28 14:23:01.210  SQL <<           sp=sp_Insert_Trans_Re_Det rows=1 dt=60ms
  2026-05-28 14:23:01.130  !!  N+1          sp=sp_Get_Producto ctx=GuardarRecepcion llamado 4x en 500ms
  2026-05-28 14:23:03.000  !!  SLOW_TX      transacción 5123ms → revisar SP dentro del bloque
  2026-05-28 14:23:00.000  ==  RESET        sesión='prueba-mi3-killios-dia1'
  2026-05-28 14:30:00.000  ==  STATS        ...

#EJC20260528
"""

import sys, re, collections, statistics
from pathlib import Path

# ── Regex ─────────────────────────────────────────────────────────────────────
RE_LINE    = re.compile(r'(\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}\.\d{3})\s+(\w+)\s+([\w<>!= ]+?)\s{2,}(.*)')
RE_OP_START= re.compile(r'op=(\S+) ctx=(\S*)')
RE_OP_END  = re.compile(r'op=(\S+) (OK|ERR) dt=(-?\d+)ms sql_roundtrips=(\d+)')
RE_TX      = re.compile(r'ctx=(\S*)(?: dt=(-?\d+)ms)?(?: reason=(.*)|$)')
RE_SQL_S   = re.compile(r'sp=(\S+) ctx=(\S*)')
RE_SQL_E   = re.compile(r'sp=(\S+) rows=(-?\d+) dt=(-?\d+)ms')
RE_MI_S    = re.compile(r'svc=(\S+) meth=(\S+) key=(\S*)')
RE_MI_E    = re.compile(r'svc=(\S+) meth=(\S+) key=(\S*) dt=(-?\d+)ms')
RE_ANOMALY = re.compile(r'(.*)')
RE_RESET   = re.compile(r"sesión='([^']*)'")

def sep(char="─", n=72): print(char * n)

class Session:
    def __init__(self, label):
        self.label = label
        self.timeline = []
        self.ops = collections.defaultdict(list)       # opName → [dt_ms]
        self.ops_err = collections.defaultdict(int)    # opName → errCount
        self.ops_sql = collections.defaultdict(list)   # opName → [sqlCount]
        self.tx_times = []
        self.sql_times = collections.defaultdict(list) # spName → [dt_ms]
        self.mi_times = collections.defaultdict(list)  # "svc.meth" → [dt_ms]
        self.anomalies = []

def parse(filepath):
    sessions = [Session("default")]

    with open(filepath, encoding="utf-8", errors="replace") as f:
        for raw in f:
            raw = raw.rstrip()
            m = RE_LINE.match(raw)
            if not m: continue
            ts, layer, tag, body = m.group(1), m.group(2), m.group(3).strip(), m.group(4)
            s = sessions[-1]

            if layer == "==" and tag == "RESET":
                mr = RE_RESET.search(body)
                label = mr.group(1) if mr else "?"
                sessions.append(Session(label))
                sessions[-1].timeline.append((ts, f"[RESET] sesión='{label}'"))
                continue

            if layer == "OP":
                if ">>" in tag:
                    mo = RE_OP_START.search(body)
                    if mo:
                        s.timeline.append((ts, f">> OP  {mo.group(1)} ctx={mo.group(2)}"))
                elif "<<" in tag:
                    mo = RE_OP_END.search(body)
                    if mo:
                        opn, result, dt, sqls = mo.group(1), mo.group(2), int(mo.group(3)), int(mo.group(4))
                        s.ops[opn].append(dt)
                        s.ops_sql[opn].append(sqls)
                        if result == "ERR": s.ops_err[opn] += 1
                        flag = " ← LENTO" if dt > 8000 else ""
                        s.timeline.append((ts, f"<< OP  {opn} {result} dt={dt}ms sqls={sqls}{flag}"))

            elif layer == "TX":
                mo = RE_TX.search(body)
                ctx = mo.group(1) if mo else "?"
                dt_str = mo.group(2) if mo else None
                if tag == "BEGIN":
                    s.timeline.append((ts, f"TX BEGIN ctx={ctx}"))
                elif tag == "COMMIT":
                    dt = int(dt_str) if dt_str else -1
                    s.tx_times.append(dt)
                    flag = " ← LENTA" if dt > 5000 else ""
                    s.timeline.append((ts, f"TX COMMIT ctx={ctx} dt={dt}ms{flag}"))
                elif tag == "ROLLBACK":
                    s.timeline.append((ts, f"TX ROLLBACK ctx={ctx}"))

            elif layer == "SQL":
                if ">>" in tag:
                    mo = RE_SQL_S.search(body)
                    if mo:
                        s.timeline.append((ts, f"SQL>> {mo.group(1)} ctx={mo.group(2)}"))
                elif "<<" in tag:
                    mo = RE_SQL_E.search(body)
                    if mo:
                        sp, rows, dt = mo.group(1), int(mo.group(2)), int(mo.group(3))
                        s.sql_times[sp].append(dt)
                        flag = " ← SLOW_SQL" if dt > 3000 else ""
                        s.timeline.append((ts, f"SQL<< {sp} rows={rows} dt={dt}ms{flag}"))

            elif layer == "MI":
                if ">>" in tag:
                    mo = RE_MI_S.search(body)
                    if mo:
                        s.timeline.append((ts, f"MI>> {mo.group(1)}.{mo.group(2)} key={mo.group(3)}"))
                elif "<<" in tag:
                    mo = RE_MI_E.search(body)
                    if mo:
                        svc, meth, key, dt = mo.group(1), mo.group(2), mo.group(3), int(mo.group(4))
                        ok = "OK" if "OK" in tag else "ERR"
                        s.mi_times[f"{svc}.{meth}"].append(dt)
                        flag = " ← SLOW_MI" if dt > 5000 else ""
                        s.timeline.append((ts, f"MI<< {svc}.{meth} key={key} {ok} dt={dt}ms{flag}"))

            elif layer == "!!":
                s.anomalies.append((ts, tag, body))
                s.timeline.append((ts, f"!! {tag:<12} {body}"))

    return [s for s in sessions if s.timeline or s.ops or s.anomalies]


def report(sessions):
    for idx, s in enumerate(sessions):
        print(); sep("═")
        print(f"  SESIÓN {idx+1}: '{s.label}'")
        sep("═")

        # Anomalías
        if s.anomalies:
            print("\n  ANOMALÍAS DETECTADAS")
            sep()
            for ts, tag, body in s.anomalies:
                print(f"  [{tag}] {ts}  {body}")
        else:
            print("\n  ✓  Sin anomalías detectadas (N+1, TX orphan, SLOW)")

        # Timeline
        if s.timeline:
            print("\n  TIMELINE")
            sep()
            for ts, ev in s.timeline:
                print(f"  {ts}  {ev}")

        # Estadísticas de operaciones
        if s.ops:
            print("\n  ESTADÍSTICAS POR OPERACIÓN LN")
            sep()
            print(f"  {'operación':<45} {'cnt':>5} {'avg ms':>8} {'max ms':>8} {'errs':>5} {'avg sqls':>8}")
            print(f"  {'─'*45} {'─'*5} {'─'*8} {'─'*8} {'─'*5} {'─'*8}")
            for op in sorted(s.ops, key=lambda x: -max(s.ops[x])):
                times = s.ops[op]
                sqls  = s.ops_sql.get(op, [0])
                avg   = statistics.mean(times)
                mx    = max(times)
                cnt   = len(times)
                errs  = s.ops_err.get(op, 0)
                avgsql= f"{statistics.mean(sqls):.1f}" if sqls else "─"
                flag  = "  ← SLOW" if mx > 8000 else ("  ← FRECUENTE" if cnt > 5 else "")
                print(f"  {op:<45} {cnt:>5} {avg:>8.0f} {mx:>8} {errs:>5} {avgsql:>8}{flag}")

        # Estadísticas de transacciones
        if s.tx_times:
            filtered = [t for t in s.tx_times if t >= 0]
            if filtered:
                print(f"\n  TRANSACCIONES SQL: count={len(filtered)} avg={statistics.mean(filtered):.0f}ms max={max(filtered)}ms")

        # Tiempos SQL por SP
        if s.sql_times:
            print("\n  TIEMPOS POR SP (instrumentación granular)")
            sep()
            for sp in sorted(s.sql_times, key=lambda x: -statistics.mean(s.sql_times[x])):
                times = s.sql_times[sp]
                avg = statistics.mean(times)
                mx  = max(times)
                flag = "  ← SLOW_SQL" if avg > 3000 else ""
                print(f"  {sp:<45} cnt={len(times)} avg={avg:.0f}ms max={mx}ms{flag}")

        # MI3 tiempos
        if s.mi_times:
            print("\n  TIEMPOS MI3 (boundary ERP→WMS)")
            sep()
            for key in sorted(s.mi_times, key=lambda x: -statistics.mean(s.mi_times[x])):
                times = s.mi_times[key]
                avg = statistics.mean(times)
                mx  = max(times)
                flag = "  ← SLOW_MI" if avg > 5000 else ""
                print(f"  {key:<45} cnt={len(times)} avg={avg:.0f}ms max={mx}ms{flag}")

        # Recomendaciones
        print("\n  RECOMENDACIONES AUTOMÁTICAS")
        sep()
        recoms = []
        for op, times in s.ops.items():
            if max(times) > 8000:
                recoms.append(f"  → {op}: máx {max(times)}ms — evaluar batch SP o índice faltante")
            sqls = s.ops_sql.get(op, [])
            if sqls and statistics.mean(sqls) > 8:
                recoms.append(f"  → {op}: promedio {statistics.mean(sqls):.1f} roundtrips SQL por llamada — candidato a SP batch")
        for ts, tag, body in s.anomalies:
            if "N+1" in tag:
                sp = re.search(r"sp=(\S+)", body)
                recoms.append(f"  → N+1 detectado en {sp.group(1) if sp else '?'}: consolidar en un SELECT IN (...) o table-valued parameter")
            if "SLOW_TX" in tag:
                recoms.append(f"  → Transacción lenta ({ts}): revisar locks de tabla o SP con CURSOR interno")
            if "TX_ORPHAN" in tag:
                recoms.append(f"  → TX ORPHAN ({ts}): revisar que todo catch llama RollBack_Transaction")
        if not recoms:
            recoms.append("  → Sin patrones de mejora obvios en esta sesión")
        for r in recoms:
            print(r)

    print(); sep("═"); print("  FIN"); sep("═")

if __name__ == "__main__":
    if len(sys.argv) < 2:
        print(__doc__); sys.exit(1)
    fp = Path(sys.argv[1])
    if not fp.exists():
        print(f"ERROR: {fp}"); sys.exit(1)
    print(f"\nAnalizando: {fp} ({fp.stat().st_size // 1024} KB)")
    sessions = parse(fp)
    if not sessions:
        print("No se encontraron eventos WmsTrace."); sys.exit(1)
    print(f"Sesiones: {len(sessions)}")
    report(sessions)
