"""Wave 13-14 — Evidencias V3 caso 1261/371 y duplicados de stock.
Reemplaza tesis V1 (release 30-nov) y V2 (explosion en recepcion).

Hallazgos:
- IdTransaccion en trans_movimientos NO es tipo, es proceso/encabezado.
- Tipo verdadero esta en IdTipoTarea -> sis_tipo_tarea (35 tipos).
- Bug es de doble naturaleza:
  (a) Falta MERGE/UPSERT en INSERT a stock destino (197 clusters explosivos).
  (b) Servicio backend desacoplado de trans_movimientos (97% sin matching).
- Usuario dominante: id=18 ('Auditoria'), 953/1388 filas duplicadas (69%).
"""
import sys, csv, os
sys.path.insert(0, os.path.dirname(os.path.abspath(__file__)))
from _db import q

OUT = os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", "outputs", "wave-13-14")
os.makedirs(OUT, exist_ok=True)

def export(name, rows):
    if not rows: return
    with open(os.path.join(OUT, name), "w", newline="", encoding="utf-8") as f:
        w = csv.DictWriter(f, fieldnames=list(rows[0].keys()))
        w.writeheader()
        for r in rows: w.writerow({k:("" if v is None else v) for k,v in r.items()})

E03 = """
SELECT m.IdTipoTarea, st.Nombre AS tipo_nombre,
       COUNT(*) movs,
       SUM(CASE WHEN m.IdUbicacionOrigen=m.IdUbicacionDestino THEN 1 ELSE 0 END) internos,
       SUM(CASE WHEN m.IdPresentacion IS NOT NULL THEN 1 ELSE 0 END) con_pres,
       AVG(m.cantidad) avg_cant,
       MIN(m.fecha) primera, MAX(m.fecha) ultima
FROM trans_movimientos m
LEFT JOIN sis_tipo_tarea st ON st.IdTipoTarea = m.IdTipoTarea
WHERE m.IdProductoBodega IN (
    SELECT DISTINCT IdProductoBodega FROM stock s WHERE s.cantidad>0
      AND EXISTS (SELECT 1 FROM stock s2
        WHERE s2.IdProductoBodega=s.IdProductoBodega AND s2.IdUbicacion=s.IdUbicacion
          AND s2.IdProductoEstado=s.IdProductoEstado AND s2.lote=s.lote
          AND ISNULL(s2.lic_plate,'')=ISNULL(s.lic_plate,'')
          AND s2.IdStock<>s.IdStock AND s2.cantidad>0))
GROUP BY m.IdTipoTarea, st.Nombre
ORDER BY 3 DESC
"""

E04 = """
SELECT IdStock, cantidad, IdPresentacion, IdUnidadMedida, lic_plate, lote,
       IdProductoEstado, IdRecepcionEnc, IdRecepcionDet, IdPedidoEnc, IdPickingEnc, IdDespachoEnc,
       IdUbicacion_anterior, fecha_ingreso, fec_agr, user_agr, fec_mod, user_mod,
       uds_lic_plate, no_bulto
FROM stock
WHERE IdProductoBodega=1261 AND IdUbicacion=371 AND cantidad>0
ORDER BY fecha_ingreso, IdStock
"""

E06 = """
WITH dup AS (
    SELECT s.IdStock, s.IdProductoBodega, s.IdUbicacion, s.IdProductoEstado, s.lote, s.lic_plate,
           s.cantidad, s.IdPresentacion, s.IdUnidadMedida,
           s.IdRecepcionEnc, s.IdPedidoEnc, s.IdPickingEnc, s.IdDespachoEnc
    FROM stock s
    WHERE s.cantidad>0
      AND EXISTS (SELECT 1 FROM stock s2
        WHERE s2.IdProductoBodega=s.IdProductoBodega AND s2.IdUbicacion=s.IdUbicacion
          AND s2.IdProductoEstado=s.IdProductoEstado AND s2.lote=s.lote
          AND ISNULL(s2.lic_plate,'')=ISNULL(s.lic_plate,'')
          AND s2.IdStock<>s.IdStock AND s2.cantidad>0)
)
SELECT
    CASE
        WHEN IdRecepcionEnc IS NOT NULL AND IdRecepcionEnc<>0 THEN 'RECEPCION'
        WHEN IdPickingEnc   IS NOT NULL AND IdPickingEnc<>0   THEN 'PICKING'
        WHEN IdPedidoEnc    IS NOT NULL AND IdPedidoEnc<>0    THEN 'PEDIDO'
        WHEN IdDespachoEnc  IS NOT NULL AND IdDespachoEnc<>0  THEN 'DESPACHO'
        ELSE 'SIN_REF'
    END AS origen,
    CASE WHEN IdPresentacion IS NULL THEN 'sin_pres' ELSE 'con_pres' END AS pres,
    CASE WHEN ISNULL(lic_plate,'') IN ('','0') THEN 'sin_lp' ELSE 'con_lp' END AS lp,
    COUNT(*) filas, SUM(cantidad) cant_total, AVG(cantidad) avg_cant
FROM dup
GROUP BY
    CASE
        WHEN IdRecepcionEnc IS NOT NULL AND IdRecepcionEnc<>0 THEN 'RECEPCION'
        WHEN IdPickingEnc   IS NOT NULL AND IdPickingEnc<>0   THEN 'PICKING'
        WHEN IdPedidoEnc    IS NOT NULL AND IdPedidoEnc<>0    THEN 'PEDIDO'
        WHEN IdDespachoEnc  IS NOT NULL AND IdDespachoEnc<>0  THEN 'DESPACHO'
        ELSE 'SIN_REF'
    END,
    CASE WHEN IdPresentacion IS NULL THEN 'sin_pres' ELSE 'con_pres' END,
    CASE WHEN ISNULL(lic_plate,'') IN ('','0') THEN 'sin_lp' ELSE 'con_lp' END
ORDER BY 4 DESC
"""

E07 = """
WITH dup AS (
    SELECT s.IdStock, s.cantidad, s.user_agr
    FROM stock s
    WHERE s.cantidad>0
      AND EXISTS (SELECT 1 FROM stock s2
        WHERE s2.IdProductoBodega=s.IdProductoBodega AND s2.IdUbicacion=s.IdUbicacion
          AND s2.IdProductoEstado=s.IdProductoEstado AND s2.lote=s.lote
          AND ISNULL(s2.lic_plate,'')=ISNULL(s.lic_plate,'')
          AND s2.IdStock<>s.IdStock AND s2.cantidad>0)
)
SELECT user_agr, COUNT(*) filas, SUM(cantidad) cant_total
FROM dup GROUP BY user_agr ORDER BY 2 DESC
"""

E11 = """
WITH dup AS (
    SELECT s.IdStock, s.IdProductoBodega, s.IdUbicacion, s.IdProductoEstado, s.lote, s.lic_plate,
           s.cantidad, s.fec_agr
    FROM stock s
    WHERE s.cantidad>0
      AND EXISTS (SELECT 1 FROM stock s2
        WHERE s2.IdProductoBodega=s.IdProductoBodega AND s2.IdUbicacion=s.IdUbicacion
          AND s2.IdProductoEstado=s.IdProductoEstado AND s2.lote=s.lote
          AND ISNULL(s2.lic_plate,'')=ISNULL(s.lic_plate,'')
          AND s2.IdStock<>s.IdStock AND s2.cantidad>0)
)
SELECT n_filas AS filas_en_mismo_segundo, COUNT(*) clusters, SUM(cant) cant_total
FROM (
    SELECT IdProductoBodega, IdUbicacion, IdProductoEstado, lote, ISNULL(lic_plate,'') lp,
           CONVERT(VARCHAR(19), fec_agr, 120) seg, COUNT(*) n_filas, SUM(cantidad) cant
    FROM dup
    GROUP BY IdProductoBodega, IdUbicacion, IdProductoEstado, lote, ISNULL(lic_plate,''),
             CONVERT(VARCHAR(19), fec_agr, 120)
    HAVING COUNT(*)>=2
) c
GROUP BY n_filas ORDER BY n_filas DESC
"""

E13 = """
WITH dup AS (
    SELECT s.IdStock, s.IdProductoBodega, s.IdUbicacion, s.fec_agr
    FROM stock s
    WHERE s.cantidad>0
      AND EXISTS (SELECT 1 FROM stock s2
        WHERE s2.IdProductoBodega=s.IdProductoBodega AND s2.IdUbicacion=s.IdUbicacion
          AND s2.IdProductoEstado=s.IdProductoEstado AND s2.lote=s.lote
          AND ISNULL(s2.lic_plate,'')=ISNULL(s.lic_plate,'')
          AND s2.IdStock<>s.IdStock AND s2.cantidad>0)
),
match_dest AS (
    SELECT d.IdStock, MIN(ABS(DATEDIFF(SECOND, m.fecha, d.fec_agr))) min_seg
    FROM dup d
    INNER JOIN trans_movimientos m
       ON m.IdProductoBodega = d.IdProductoBodega
      AND (m.IdUbicacionDestino = d.IdUbicacion OR m.IdUbicacionOrigen = d.IdUbicacion)
    GROUP BY d.IdStock
)
SELECT
    CASE
        WHEN min_seg IS NULL THEN '0_no_match'
        WHEN min_seg <= 5 THEN '1_<=5seg'
        WHEN min_seg <= 60 THEN '2_<=1min'
        WHEN min_seg <= 600 THEN '3_<=10min'
        WHEN min_seg <= 3600 THEN '4_<=1hora'
        WHEN min_seg <= 86400 THEN '5_<=1dia'
        ELSE '6_>1dia'
    END bucket,
    COUNT(*) filas
FROM dup d LEFT JOIN match_dest md ON md.IdStock = d.IdStock
GROUP BY
    CASE
        WHEN min_seg IS NULL THEN '0_no_match'
        WHEN min_seg <= 5 THEN '1_<=5seg'
        WHEN min_seg <= 60 THEN '2_<=1min'
        WHEN min_seg <= 600 THEN '3_<=10min'
        WHEN min_seg <= 3600 THEN '4_<=1hora'
        WHEN min_seg <= 86400 THEN '5_<=1dia'
        ELSE '6_>1dia'
    END
ORDER BY 1
"""

if __name__ == "__main__":
    export("E03_idtipotarea_productos_duplicados.csv", q(E03))
    export("E04_caso_1261_stock_full_trazabilidad.csv", q(E04))
    export("E06_tipologia_filas_duplicadas.csv", q(E06))
    export("E07_user_agr_duplicados.csv", q(E07))
    export("E11_clusters_explosivos.csv", q(E11))
    export("E13_distancia_temporal_match.csv", q(E13))
    print("Wave 13-14 OK")
