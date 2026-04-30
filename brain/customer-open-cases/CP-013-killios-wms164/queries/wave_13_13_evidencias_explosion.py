"""wave-13-13: profundizacion sobre la pista de Erik (residual de explosion).

Ejecuta 8 queries (D01 a D24) y exporta CSVs:
  D01_patron_present_lic.csv         distribucion present x licencia (las 1388)
  D14_caso_1261_371_movimientos.csv  movimientos asociados al caso de Erik
  D19_idtransaccion_duplicados.csv   tipos de transaccion involucrados
  D22_serie_transacciones.csv        timeline de aparicion de cada IdTransaccion
  D23_cuadre_general.csv             resumen del cuadre teorico
  D24_top30_inflacion.csv            top 30 combos con mayor descuadre
"""
import os, csv, sys, datetime
sys.path.insert(0, os.path.dirname(__file__))
from _db import q

OUT = os.path.join(os.path.dirname(__file__), "..", "outputs", "wave-13-13")
os.makedirs(OUT, exist_ok=True)

def export(name, rows):
    if not rows: print(f"  {name}: 0 filas"); return
    with open(os.path.join(OUT, name), "w", newline="", encoding="utf-8") as f:
        w = csv.DictWriter(f, fieldnames=list(rows[0].keys())); w.writeheader()
        for r in rows: w.writerow({k:("" if v is None else v) for k, v in r.items()})
    print(f"  {name}: {len(rows)} filas")


print("=" * 70)
print("D01 — Patron present x licencia en las 1388 filas duplicadas")
print("=" * 70)
e = q("""
SELECT
    CASE WHEN s.IdPresentacion IS NULL THEN 'sin_presentacion' ELSE 'con_presentacion' END AS presentacion,
    CASE WHEN s.lic_plate IS NULL OR s.lic_plate IN ('','0') THEN 'sin_licencia' ELSE 'con_licencia' END AS licencia,
    COUNT(*) filas, SUM(s.Cantidad) un_totales
FROM stock s
WHERE s.Cantidad > 0
  AND EXISTS (SELECT 1 FROM stock s2
    WHERE s2.IdProductoBodega=s.IdProductoBodega AND s2.IdUbicacion=s.IdUbicacion
      AND s2.IdProductoEstado=s.IdProductoEstado AND s2.Lote=s.Lote
      AND ISNULL(s2.lic_plate,'')=ISNULL(s.lic_plate,'')
      AND s2.IdStock<>s.IdStock AND s2.Cantidad>0)
GROUP BY
    CASE WHEN s.IdPresentacion IS NULL THEN 'sin_presentacion' ELSE 'con_presentacion' END,
    CASE WHEN s.lic_plate IS NULL OR s.lic_plate IN ('','0') THEN 'sin_licencia' ELSE 'con_licencia' END
ORDER BY 3 DESC
""")
export("D01_patron_present_lic.csv", e)

print()
print("=" * 70)
print("D14 — Movimientos asociados al caso 1261/371 (2026-02-06 12:00-13:00)")
print("=" * 70)
e = q("""
SELECT IdMovimiento, IdTransaccion, IdUbicacionOrigen, IdUbicacionDestino,
       IdEstadoOrigen, IdEstadoDestino, IdPresentacion, IdUnidadMedida,
       cantidad, lic_plate, fecha
FROM trans_movimientos
WHERE IdProductoBodega = 1261
  AND fecha >= '2026-02-06 12:00:00' AND fecha < '2026-02-06 13:00:00'
ORDER BY fecha, IdMovimiento
""")
export("D14_caso_1261_371_movimientos.csv", e)

print()
print("=" * 70)
print("D19 — IdTransaccion presentes en mov de productos con duplicados (28-30 nov 2025)")
print("=" * 70)
e = q("""
SELECT m.IdTransaccion, COUNT(*) movimientos,
       SUM(CASE WHEN m.IdUbicacionOrigen=m.IdUbicacionDestino THEN 1 ELSE 0 END) internos,
       SUM(CASE WHEN m.IdPresentacion IS NOT NULL THEN 1 ELSE 0 END) con_presentacion,
       AVG(m.cantidad) avg_cant,
       MIN(m.fecha) primera_fecha, MAX(m.fecha) ultima_fecha
FROM trans_movimientos m
WHERE m.IdProductoBodega IN (
    SELECT DISTINCT IdProductoBodega FROM stock s WHERE s.Cantidad>0
      AND EXISTS (SELECT 1 FROM stock s2
        WHERE s2.IdProductoBodega=s.IdProductoBodega AND s2.IdUbicacion=s.IdUbicacion
          AND s2.IdProductoEstado=s.IdProductoEstado AND s2.Lote=s.Lote
          AND ISNULL(s2.lic_plate,'')=ISNULL(s.lic_plate,'')
          AND s2.IdStock<>s.IdStock AND s2.Cantidad>0))
  AND m.fecha >= '2025-11-28' AND m.fecha < '2025-12-01'
GROUP BY m.IdTransaccion
ORDER BY 2 DESC
""")
export("D19_idtransaccion_duplicados.csv", e)

print()
print("=" * 70)
print("D22 — Timeline: primera y ultima fecha de cada IdTransaccion candidato")
print("=" * 70)
e = q("""
SELECT IdTransaccion, COUNT(*) movs,
       MIN(fecha) primera_aparicion, MAX(fecha) ultima_aparicion,
       SUM(CASE WHEN IdUbicacionOrigen=IdUbicacionDestino THEN 1 ELSE 0 END) internos
FROM trans_movimientos
WHERE IdTransaccion IN (2, 7926,7927,7928,7929,7930,7931,7932,7933,7934,7935,
                           7936,7937,7938,7939,7940,7941,7942,7943,7944,7945)
GROUP BY IdTransaccion
ORDER BY MIN(fecha)
""")
export("D22_serie_transacciones.csv", e)

print()
print("=" * 70)
print("D23 — Cuadre teorico GENERAL (con COLLATE Modern_Spanish_CI_AS)")
print("=" * 70)
e = q("""
WITH combos AS (
    SELECT DISTINCT s.IdProductoBodega, s.IdUbicacion, s.IdProductoEstado, s.Lote
    FROM stock s WHERE s.Cantidad>0
      AND EXISTS (SELECT 1 FROM stock s2
        WHERE s2.IdProductoBodega=s.IdProductoBodega AND s2.IdUbicacion=s.IdUbicacion
          AND s2.IdProductoEstado=s.IdProductoEstado AND s2.Lote=s.Lote
          AND ISNULL(s2.lic_plate,'')=ISNULL(s.lic_plate,'')
          AND s2.IdStock<>s.IdStock AND s2.Cantidad>0)
),
sa AS (
    SELECT c.IdProductoBodega, c.IdUbicacion, c.IdProductoEstado, c.Lote,
           SUM(s.Cantidad) un_actual, COUNT(*) filas_stock
    FROM combos c INNER JOIN stock s
      ON s.IdProductoBodega=c.IdProductoBodega AND s.IdUbicacion=c.IdUbicacion
     AND s.IdProductoEstado=c.IdProductoEstado AND s.Lote=c.Lote AND s.Cantidad>0
    GROUP BY c.IdProductoBodega, c.IdUbicacion, c.IdProductoEstado, c.Lote
),
mo AS (
    SELECT c.IdProductoBodega, c.IdUbicacion, c.IdProductoEstado, c.Lote,
           SUM(CASE WHEN m.IdUbicacionDestino=c.IdUbicacion AND m.IdUbicacionOrigen<>c.IdUbicacion THEN m.cantidad ELSE 0 END) ingresos,
           SUM(CASE WHEN m.IdUbicacionOrigen=c.IdUbicacion AND m.IdUbicacionDestino<>c.IdUbicacion THEN m.cantidad ELSE 0 END) egresos,
           SUM(CASE WHEN m.IdUbicacionOrigen=c.IdUbicacion AND m.IdUbicacionDestino=c.IdUbicacion THEN m.cantidad ELSE 0 END) internos_un,
           SUM(CASE WHEN m.IdUbicacionOrigen=c.IdUbicacion AND m.IdUbicacionDestino=c.IdUbicacion THEN 1 ELSE 0 END) internos_movs
    FROM combos c LEFT JOIN trans_movimientos m
      ON m.IdProductoBodega=c.IdProductoBodega
     AND (m.IdUbicacionOrigen=c.IdUbicacion OR m.IdUbicacionDestino=c.IdUbicacion)
     AND m.lote COLLATE Modern_Spanish_CI_AS = c.Lote COLLATE Modern_Spanish_CI_AS
    GROUP BY c.IdProductoBodega, c.IdUbicacion, c.IdProductoEstado, c.Lote
)
SELECT COUNT(*) total_combos,
       SUM(CASE WHEN sa.un_actual > (ISNULL(mo.ingresos,0)-ISNULL(mo.egresos,0)) THEN 1 ELSE 0 END) inflados,
       SUM(CASE WHEN sa.un_actual = (ISNULL(mo.ingresos,0)-ISNULL(mo.egresos,0)) THEN 1 ELSE 0 END) balanceados,
       SUM(CASE WHEN sa.un_actual < (ISNULL(mo.ingresos,0)-ISNULL(mo.egresos,0)) THEN 1 ELSE 0 END) sub_declarados,
       SUM(sa.un_actual) total_un_stock, SUM(ISNULL(mo.ingresos,0)) total_ingresos,
       SUM(ISNULL(mo.egresos,0)) total_egresos,
       SUM(ISNULL(mo.internos_un,0)) total_internos_un,
       SUM(ISNULL(mo.internos_movs,0)) total_internos_movs,
       SUM(sa.un_actual - (ISNULL(mo.ingresos,0)-ISNULL(mo.egresos,0))) inflacion_neta
FROM sa LEFT JOIN mo
  ON mo.IdProductoBodega=sa.IdProductoBodega AND mo.IdUbicacion=sa.IdUbicacion
 AND mo.IdProductoEstado=sa.IdProductoEstado
 AND mo.Lote COLLATE Modern_Spanish_CI_AS = sa.Lote COLLATE Modern_Spanish_CI_AS
""")
export("D23_cuadre_general.csv", e)

print()
print("=" * 70)
print("D24 — Top 30 combos con mayor inflacion absoluta (positiva o negativa)")
print("=" * 70)
e = q("""
WITH combos AS (
    SELECT DISTINCT s.IdProductoBodega, s.IdUbicacion, s.IdProductoEstado, s.Lote
    FROM stock s WHERE s.Cantidad>0
      AND EXISTS (SELECT 1 FROM stock s2
        WHERE s2.IdProductoBodega=s.IdProductoBodega AND s2.IdUbicacion=s.IdUbicacion
          AND s2.IdProductoEstado=s.IdProductoEstado AND s2.Lote=s.Lote
          AND ISNULL(s2.lic_plate,'')=ISNULL(s.lic_plate,'')
          AND s2.IdStock<>s.IdStock AND s2.Cantidad>0)
),
sa AS (
    SELECT c.IdProductoBodega, c.IdUbicacion, c.IdProductoEstado, c.Lote,
           SUM(s.Cantidad) un_actual, COUNT(*) filas_stock
    FROM combos c INNER JOIN stock s
      ON s.IdProductoBodega=c.IdProductoBodega AND s.IdUbicacion=c.IdUbicacion
     AND s.IdProductoEstado=c.IdProductoEstado AND s.Lote=c.Lote AND s.Cantidad>0
    GROUP BY c.IdProductoBodega, c.IdUbicacion, c.IdProductoEstado, c.Lote
),
mo AS (
    SELECT c.IdProductoBodega, c.IdUbicacion, c.IdProductoEstado, c.Lote,
           SUM(CASE WHEN m.IdUbicacionDestino=c.IdUbicacion AND m.IdUbicacionOrigen<>c.IdUbicacion THEN m.cantidad ELSE 0 END) ing,
           SUM(CASE WHEN m.IdUbicacionOrigen=c.IdUbicacion AND m.IdUbicacionDestino<>c.IdUbicacion THEN m.cantidad ELSE 0 END) egr
    FROM combos c LEFT JOIN trans_movimientos m
      ON m.IdProductoBodega=c.IdProductoBodega
     AND (m.IdUbicacionOrigen=c.IdUbicacion OR m.IdUbicacionDestino=c.IdUbicacion)
     AND m.lote COLLATE Modern_Spanish_CI_AS = c.Lote COLLATE Modern_Spanish_CI_AS
    GROUP BY c.IdProductoBodega, c.IdUbicacion, c.IdProductoEstado, c.Lote
)
SELECT TOP 30 sa.IdProductoBodega, sa.IdUbicacion, sa.Lote, sa.filas_stock,
       sa.un_actual, ISNULL(mo.ing,0) ingresos, ISNULL(mo.egr,0) egresos,
       (ISNULL(mo.ing,0)-ISNULL(mo.egr,0)) neto_teorico,
       (sa.un_actual - (ISNULL(mo.ing,0)-ISNULL(mo.egr,0))) inflacion
FROM sa LEFT JOIN mo
  ON mo.IdProductoBodega=sa.IdProductoBodega AND mo.IdUbicacion=sa.IdUbicacion
 AND mo.IdProductoEstado=sa.IdProductoEstado
 AND mo.Lote COLLATE Modern_Spanish_CI_AS = sa.Lote COLLATE Modern_Spanish_CI_AS
ORDER BY ABS(sa.un_actual - (ISNULL(mo.ing,0)-ISNULL(mo.egr,0))) DESC
""")
export("D24_top30_inflacion.csv", e)

print()
print(f"DONE {datetime.datetime.utcnow()}Z — CSVs en outputs/wave-13-13/")
