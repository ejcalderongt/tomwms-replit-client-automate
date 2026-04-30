"""WAVE 17 — validar A/B/C contra integración SAP y lp/lote consistency.

Distingue:
  A) Recepción mal contada (foco: Recepción 2179 + reconteo)
  B) Extravío posterior (foco: huecos en stock_hist sin mov correspondiente)
  C) Lote-swap en picking (foco: lp consistency BG2512 vs físico)

Pre-req: conexión BD restaurada.
"""
import sys, csv, os
sys.path.insert(0, ".")
from _db import q

OUT = "../outputs/wave-17-root-cause"
os.makedirs(OUT, exist_ok=True)
prod = q("SELECT IdProducto FROM producto_bodega WHERE IdProductoBodega=381")[0]['IdProducto']

def csv_out(name, rows):
    if not rows:
        with open(f"{OUT}/{name}", "w") as f: f.write("vacio\n# ZERO\n")
        print(f"  {name}: 0"); return
    cols = list(rows[0].keys())
    with open(f"{OUT}/{name}", "w", newline="", encoding="utf-8") as f:
        w = csv.DictWriter(f, fieldnames=cols); w.writeheader()
        for r in rows: w.writerow({k:("" if v is None else v) for k,v in r.items()})
    print(f"  {name}: {len(rows)} filas")

# ───────── BLOQUE 1: integración SAP ─────────
print("[1] Cols ERP/Enviado en recepcion/despacho")
for t in ['trans_recepcion_enc','trans_despacho_enc','trans_re_det','trans_dd_det']:
    cols = [c['COLUMN_NAME'] for c in q(
        f"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME='{t}' "
        "AND (COLUMN_NAME LIKE '%ERP%' OR COLUMN_NAME LIKE '%SAP%' OR COLUMN_NAME LIKE '%Enviado%' OR COLUMN_NAME LIKE '%integr%')")]
    print(f"  {t}: {cols}")

print("\n[2] Recepción 2179 estado de envío SAP")
csv_out("W17-01-recepcion-2179-erp.csv", q("""
SELECT IdRecepcion, fecha_recepcion, IdProveedor, no_documento, Enviado_A_ERP,
       fec_envio_erp, doc_erp, estado, observacion, user_agr
FROM trans_recepcion_enc WHERE IdRecepcion=2179
"""))

print("\n[3] Despachos BG2512 estado de envío SAP")
csv_out("W17-02-despachos-BG2512-erp.csv", q(f"""
SELECT DISTINCT de.IdDespachoEnc, de.fecha, de.no_pase, de.estado, de.no_documento_externo,
       de.Enviado_A_ERP, de.fec_envio_erp, de.doc_erp, de.observacion, de.user_agr
FROM trans_despacho_enc de
INNER JOIN trans_despacho_det dd ON dd.IdDespachoEnc=de.IdDespachoEnc
INNER JOIN trans_picking_ubic pu ON pu.IdPickingUbic=dd.IdPickingUbic
WHERE dd.IdProductoBodega=381 AND pu.lote='BG2512'
ORDER BY de.fecha
"""))

# ───────── BLOQUE 2: hipótesis C — lote-swap en picking ─────────
print("\n[4] Verificar consistencia lp↔lote en stock_hist BG2512")
csv_out("W17-03-lp-lote-consistency.csv", q(f"""
SELECT lic_plate, lote, COUNT(DISTINCT IdStock) idstocks, MIN(fec_agr) primer_uso, MAX(fec_agr) ultimo_uso
FROM stock_hist
WHERE IdProductoBodega=381 AND lic_plate IS NOT NULL AND lic_plate<>''
GROUP BY lic_plate, lote
ORDER BY lic_plate, primer_uso
"""))

print("\n[5] Detección lp con MÁS DE UN lote (proxy de swap)")
csv_out("W17-04-lp-multilote.csv", q(f"""
SELECT lic_plate, COUNT(DISTINCT lote) lotes_distintos, STRING_AGG(CAST(lote AS VARCHAR(20)), '|') lotes
FROM stock_hist
WHERE IdProductoBodega=381 AND lic_plate IS NOT NULL AND lic_plate<>''
GROUP BY lic_plate
HAVING COUNT(DISTINCT lote) > 1
"""))

print("\n[6] Movs salida BG2512 con lp y verificar si ese lp tuvo otro lote antes")
csv_out("W17-05-salidas-BG2512-lp-historia.csv", q(f"""
SELECT m.IdMovimiento, m.fecha, m.lote, m.lic_plate, m.cantidad, m.IdDespachoEnc, m.IdDespachoDet,
       (SELECT COUNT(DISTINCT sh.lote) FROM stock_hist sh
        WHERE sh.lic_plate=m.lic_plate AND sh.IdProductoBodega=381) AS lotes_que_uso_este_lp,
       (SELECT STRING_AGG(CAST(sh.lote AS VARCHAR(20)),'|') FROM
          (SELECT DISTINCT lote FROM stock_hist sh2
           WHERE sh2.lic_plate=m.lic_plate AND sh2.IdProductoBodega=381) sh) AS lista_lotes_lp
FROM trans_movimientos m
WHERE m.IdProductoBodega=381 AND m.lote='BG2512' AND m.IdDespachoEnc IS NOT NULL
  AND m.lic_plate IS NOT NULL AND m.lic_plate<>''
ORDER BY m.fecha
"""))

# ───────── BLOQUE 3: hipótesis B — extravío sin mov ─────────
print("\n[7] stock_hist eliminados sin contraparte de mov (huecos)")
csv_out("W17-06-stock-elim-sin-mov.csv", q(f"""
SELECT sh.IdStock, sh.lote, sh.lic_plate, sh.IdUbicacion, sh.IdProductoEstado,
       sh.cantidad, sh.fec_agr, sh.fec_mod, sh.user_mod
FROM stock_hist sh
WHERE sh.IdProductoBodega=381 AND sh.lote='BG2512' AND sh.cantidad=0
  AND NOT EXISTS (SELECT 1 FROM trans_movimientos m WHERE m.IdStock_origen=sh.IdStock OR m.IdStock_destino=sh.IdStock)
ORDER BY sh.fec_mod
"""))

# ───────── BLOQUE 4: validación reconteo ─────────
print("\n[8] ¿Hubo reconteo en recepción 2179?")
csv_out("W17-07-reconteo-rec2179.csv", q("""
SELECT * FROM trans_inv_reconteo WHERE idreconteo IN (
    SELECT IdReconteo FROM trans_re_enc WHERE IdRecepcion=2179
)
"""))
print("\nFIN wave 17.")
