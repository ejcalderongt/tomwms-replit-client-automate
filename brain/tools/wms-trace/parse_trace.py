#!/usr/bin/env python3
"""
parse_trace.py — Analizador de WmsTrace para TOM WMS HH
Uso: python3 parse_trace.py <logcat_file>

Recolección del logcat:
    adb logcat -v time -s WMS-T:* > trace_20260528_1430.log
    # O captura completa (para ver errores de otras tags):
    adb logcat -v time > trace_full.log

Formato esperado de líneas WMS-T:
    05-28 14:23:01.123  5432  5433 D WMS-T   : >> EXEC case=16 #1 t=... th=...
    05-28 14:23:02.890  5432  5432 D WMS-T   : << OK   case=16 dt=1767ms th=main
    05-28 14:23:01.125  5432  5433 W WMS-T   : !! OVERLAP case=16 inicia mientras case=1 sigue en vuelo

#EJC20260528
"""

import sys, re, collections, statistics
from pathlib import Path

# ── Regex patterns ─────────────────────────────────────────────────────────────

RE_LINE   = re.compile(r'(\d{2}-\d{2} \d{2}:\d{2}:\d{2}\.\d{3})\s+\d+\s+\d+\s+([DWIE])\s+WMS-T\s+:\s+(.*)')
RE_EXEC   = re.compile(r'>> EXEC case=(\d+) #(\d+) t=(\d+).*th=(\S+)')
RE_WS     = re.compile(r'>  WS meth=(\S+) t=(\d+).*th=(\S+)')
RE_WSEND  = re.compile(r'<  WS meth=(\S+) len=(\d+)b dt=(-?\d+)ms.*th=(\S+)')
RE_OK     = re.compile(r'<< OK   case=(\d+) dt=(-?\d+)ms(.*?)th=(\S+)')
RE_ERR    = re.compile(r'<< ERR  case=(\d+) dt=(-?\d+)ms(.*?)err=(.*)')
RE_OVERLAP= re.compile(r'!! OVERLAP case=(\d+) inicia mientras case=(\d+)')
RE_RELOAD = re.compile(r'!! RELOAD  case=(\d+).*gap=(-?\d+)ms')
RE_STATE  = re.compile(r'ST \[([^\]]+)\] (.*?) -> (.*?) case=(\d+)')
RE_ARRAY  = re.compile(r'AR \[([^\]]+)\] (\w+) sz=(\d+).*case=(\d+)')
RE_RESET  = re.compile(r'== RESET session=(.*?) ==')
RE_STATS  = re.compile(r'=== WmsTrace STATS')

# ── Case name map (extraído del switch en wsExecute / wsCallBack) ─────────────
CASE_NAMES = {
    1: "Get_Producto_By_IdProductoBodega",
    2: "Get_Estados_By_IdPropietario",
    3: "Get_All_Presentaciones_By_IdProducto",
    4: "Get_All_Codigos_Barra_By_IdProducto",
    5: "Get_All_ProductoParametros_By_IdProducto_HH",
    6: "Get_Resoluciones_Lp",
    7: "Get_Licenses_Plates_By_IdRecepcionEnc",
    8: "Existe_LP_By_IdRecepcionEnc",
    9: "MaxIDStockSeRec",
    10: "Get_All_Params_By_IdRecepcionEnc_Det",
    11: "Get_Resoluciones_Lp (alt)",
    12: "?",
    13: "?",
    14: "?",
    15: "?",
    16: "Guardar_Recepcion (CajaMaster / principal)",
    17: "Guardar_Recepcion_Edita",
    18: "RecepcionCajaMaster",
    19: "?",
    20: "?",
    21: "?",
    22: "?",
    25: "?",
    26: "?",
    39: "Get_Detalle_Rec_By_IdCompra_Licencia_JSON",
    42: "Get_List_Product_By_CodigoBarra_By_OrdenCompraEnc",
}

def cname(n):
    return CASE_NAMES.get(n, "?")

# ── Estructuras de datos ───────────────────────────────────────────────────────

class Session:
    def __init__(self, label):
        self.label = label
        self.events = []          # list of (timestamp_str, level, raw_msg, parsed_type, parsed_data)
        self.execs = {}           # case -> [dt_ms, ...]
        self.methods = {}         # method -> [dt_ms, ...]
        self.overlaps = []
        self.reloads = []
        self.errors = []
        self.state_changes = collections.defaultdict(list)
        self.array_muts = collections.defaultdict(list)
        self.timeline = []        # list of (ts, event_str)

# ── Parser principal ───────────────────────────────────────────────────────────

def parse(filepath):
    sessions = []
    current = Session("default")
    sessions.append(current)

    with open(filepath, encoding="utf-8", errors="replace") as f:
        for raw_line in f:
            raw_line = raw_line.rstrip()
            m = RE_LINE.search(raw_line)
            if not m:
                continue

            ts, level, msg = m.group(1), m.group(2), m.group(3)

            # Reset de sesión
            mr = RE_RESET.search(msg)
            if mr:
                current = Session(mr.group(1))
                sessions.append(current)
                current.timeline.append((ts, f"[RESET] sesión='{mr.group(1)}'"))
                continue

            # >> EXEC
            me = RE_EXEC.search(msg)
            if me:
                case, count, t, th = int(me.group(1)), int(me.group(2)), int(me.group(3)), me.group(4)
                current.timeline.append((ts, f">> case={case} ({cname(case)}) #{count} th={th}"))
                continue

            # << OK
            mo = RE_OK.search(msg)
            if mo:
                case, dt, hint, th = int(mo.group(1)), int(mo.group(2)), mo.group(3).strip(), mo.group(4)
                if case not in current.execs:
                    current.execs[case] = []
                current.execs[case].append(dt)
                current.timeline.append((ts, f"<< OK   case={case} ({cname(case)}) dt={dt}ms{hint}"))
                continue

            # << ERR
            me2 = RE_ERR.search(msg)
            if me2:
                case, dt, hint, err = int(me2.group(1)), int(me2.group(2)), me2.group(3).strip(), me2.group(4)
                current.errors.append((ts, case, dt, err[:100]))
                current.timeline.append((ts, f"<< ERR  case={case} ({cname(case)}) dt={dt}ms err={err[:80]}"))
                continue

            # >  WS
            mw = RE_WS.search(msg)
            if mw:
                method, t, th = mw.group(1), int(mw.group(2)), mw.group(3)
                current.timeline.append((ts, f">  WS   {method} th={th}"))
                continue

            # <  WS
            mwe = RE_WSEND.search(msg)
            if mwe:
                method, ln, dt, th = mwe.group(1), int(mwe.group(2)), int(mwe.group(3)), mwe.group(4)
                if method not in current.methods:
                    current.methods[method] = []
                current.methods[method].append(dt)
                current.timeline.append((ts, f"<  WS   {method} len={ln}b dt={dt}ms"))
                continue

            # !! OVERLAP
            mol = RE_OVERLAP.search(msg)
            if mol:
                new_c, old_c = int(mol.group(1)), int(mol.group(2))
                current.overlaps.append((ts, new_c, old_c))
                current.timeline.append((ts, f"!! OVERLAP case={new_c} vs case={old_c} ← RACE CONDITION"))
                continue

            # !! RELOAD
            mrl = RE_RELOAD.search(msg)
            if mrl:
                case, gap = int(mrl.group(1)), int(mrl.group(2))
                current.reloads.append((ts, case, gap))
                current.timeline.append((ts, f"!! RELOAD  case={case} gap={gap}ms ← REPROCESO"))
                continue

            # ST state
            mst = RE_STATE.search(msg)
            if mst:
                label, before, after, case = mst.group(1), mst.group(2), mst.group(3), int(mst.group(4))
                current.state_changes[label].append((ts, case, before, after))
                continue

            # AR array
            mar = RE_ARRAY.search(msg)
            if mar:
                name, op, sz, case = mar.group(1), mar.group(2), int(mar.group(3)), int(mar.group(4))
                current.array_muts[name].append((ts, case, op, sz))
                continue

    return sessions

# ── Reporte ───────────────────────────────────────────────────────────────────

def print_separator(char="─", n=70):
    print(char * n)

def report(sessions):
    for s_idx, s in enumerate(sessions):
        print()
        print_separator("═")
        print(f"  SESIÓN {s_idx+1}: '{s.label}'")
        print_separator("═")

        # ── 1. Anomalías críticas ──────────────────────────────────────────────
        if s.overlaps or s.reloads or s.errors:
            print()
            print("  ⚠️  ANOMALÍAS DETECTADAS")
            print_separator()
            for ts, new_c, old_c in s.overlaps:
                print(f"  [RACE]   {ts} — case={new_c} ({cname(new_c)}) inició con case={old_c} ({cname(old_c)}) en vuelo")
            for ts, case, gap in s.reloads:
                print(f"  [RELOAD] {ts} — case={case} ({cname(case)}) relanzado, gap={gap}ms (reproceso innecesario?)")
            for ts, case, dt, err in s.errors:
                hint = " ← POSIBLE TIMEOUT" if dt > 10000 else (" ← error rápido/param" if dt < 200 else "")
                print(f"  [ERROR]  {ts} — case={case} ({cname(case)}) dt={dt}ms{hint}: {err}")
        else:
            print()
            print("  ✓  Sin race conditions, reloads ni errores detectados")

        # ── 2. Timeline ────────────────────────────────────────────────────────
        if s.timeline:
            print()
            print("  TIMELINE DE LLAMADAS")
            print_separator()
            for ts, ev in s.timeline:
                print(f"  {ts}  {ev}")

        # ── 3. Estadísticas por caso ───────────────────────────────────────────
        if s.execs:
            print()
            print("  ESTADÍSTICAS POR CASO")
            print_separator()
            print(f"  {'case':<6} {'count':<7} {'avg ms':<9} {'max ms':<9} {'min ms':<9}  nombre")
            print(f"  {'─'*6} {'─'*7} {'─'*9} {'─'*9} {'─'*9}  {'─'*30}")
            for case in sorted(s.execs.keys()):
                times = s.execs[case]
                avg = statistics.mean(times) if times else 0
                mn  = min(times) if times else 0
                mx  = max(times) if times else 0
                cnt = len(times)
                flag = "  ← LENTO" if mx > 5000 else ("  ← FRECUENTE" if cnt > 5 else "")
                print(f"  {case:<6} {cnt:<7} {avg:<9.0f} {mx:<9} {mn:<9}  {cname(case)}{flag}")

        # ── 4. Estadísticas por método WS ─────────────────────────────────────
        if s.methods:
            print()
            print("  TIEMPOS POR MÉTODO SOAP")
            print_separator()
            print(f"  {'avg ms':<9} {'max ms':<9} {'cnt':<5}  método")
            for meth in sorted(s.methods.keys(), key=lambda m: -statistics.mean(s.methods[m])):
                times = s.methods[meth]
                avg = statistics.mean(times)
                mx  = max(times)
                cnt = len(times)
                flag = "  ← LENTO" if avg > 3000 else ""
                print(f"  {avg:<9.0f} {mx:<9} {cnt:<5}  {meth}{flag}")

        # ── 5. Mutaciones de arrays (si se instrumentaron) ─────────────────────
        if s.array_muts:
            print()
            print("  MUTACIONES DE ARRAYS")
            print_separator()
            for name, muts in sorted(s.array_muts.items()):
                ops = collections.Counter(op for _, _, op, _ in muts)
                print(f"  [{name}]  ops={dict(ops)}  total={len(muts)}")

        # ── 6. Cambios de estado (si se instrumentaron) ────────────────────────
        if s.state_changes:
            print()
            print("  CAMBIOS DE ESTADO")
            print_separator()
            for label, changes in sorted(s.state_changes.items()):
                print(f"  [{label}]  cambios={len(changes)}")
                for ts, case, before, after in changes[:5]:  # max 5 líneas por campo
                    print(f"    {ts} case={case}: {before} → {after}")
                if len(changes) > 5:
                    print(f"    ... ({len(changes)-5} más)")

        # ── 7. Recomendaciones automáticas ────────────────────────────────────
        print()
        print("  RECOMENDACIONES")
        print_separator()
        recoms = []
        for case, times in s.execs.items():
            avg = statistics.mean(times) if times else 0
            if avg > 5000:
                recoms.append(f"  → case={case} ({cname(case)}) promedio {avg:.0f}ms — evaluar paginación o caché local")
            if len(times) > 8:
                recoms.append(f"  → case={case} ({cname(case)}) llamado {len(times)}x — posible reload innecesario")
        for ts, new_c, old_c in s.overlaps:
            recoms.append(f"  → Agregar guard 'isOperationInProgress' antes de ws.callback={new_c} para evitar overlap")
        for ts, case, gap in s.reloads:
            if gap < 500:
                recoms.append(f"  → case={case} ({cname(case)}) relanzado en {gap}ms — revisar si hay double-tap o listener duplicado")
        if not recoms:
            recoms.append("  → No se detectaron patrones de mejora obvios en esta sesión")
        for r in recoms:
            print(r)

    print()
    print_separator("═")
    print("  FIN DEL ANÁLISIS")
    print_separator("═")

# ── Main ───────────────────────────────────────────────────────────────────────

if __name__ == "__main__":
    if len(sys.argv) < 2:
        print(__doc__)
        sys.exit(1)

    filepath = Path(sys.argv[1])
    if not filepath.exists():
        print(f"ERROR: archivo no encontrado: {filepath}")
        sys.exit(1)

    print(f"\nAnalizando: {filepath} ({filepath.stat().st_size // 1024} KB)")
    sessions = parse(filepath)
    filtered = [s for s in sessions if s.timeline or s.execs or s.overlaps or s.reloads or s.errors]

    if not filtered:
        print("No se encontraron líneas WMS-T en el archivo.")
        print("Verifica que el logcat fue recolectado con: adb logcat -s WMS-T:* -v time")
        sys.exit(1)

    print(f"Sesiones encontradas: {len(filtered)}")
    report(filtered)
