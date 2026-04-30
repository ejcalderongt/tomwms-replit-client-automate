"""Wave 15 — Análisis del reporte 'Stock en una fecha' caso WMS164 BM2601.

Hallazgos:
- NO hay duplicación de devolución (Treviso compra ≠ Imaginova devolución).
- Reporte hardcoded en cliente VB.NET BOF (no en BD).
- Cuenta movs internos de proceso (DESP/PIK/VERI/UBIC) como Salidas → infla.
- Aritmética interna del reporte inconsistente con sus propios valores.
"""
import sys, csv, os
sys.path.insert(0, os.path.dirname(os.path.abspath(__file__)))
from _db import q

OUT = os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", "outputs", "wave-15-reporte")
os.makedirs(OUT, exist_ok=True)

def export(name, rows):
    if not rows: return
    with open(os.path.join(OUT, name), "w", newline="", encoding="utf-8") as f:
        w = csv.DictWriter(f, fieldnames=list(rows[0].keys()))
        w.writeheader()
        for r in rows: w.writerow({k:("" if v is None else v) for k,v in r.items()})

# F01 — Comparación de las 2 OC
export("F01_oc_1480_vs_2061_encabezados.csv", q("""
SELECT IdOrdenCompraEnc, IdPropietarioBodega, IdProveedorBodega, IdTipoIngresoOC, IdEstadoOC,
       IdMotivoDevolucion, Fecha_Creacion, No_Documento, User_Agr, Procedencia, Referencia,
       Observacion, Fecha_Recepcion, no_documento_recepcion_erp, Codigo_Empresa_ERP
FROM trans_oc_enc WHERE IdOrdenCompraEnc IN (1480, 2061) ORDER BY IdOrdenCompraEnc
"""))

# F02 — Detalles WMS164 en cada recepcion
export("F02_recepciones_WMS164_treviso_imaginova.csv", q("""
SELECT d.IdRecepcionEnc, d.No_Linea, d.cantidad_recibida, d.IdPresentacion, d.nombre_presentacion,
       d.IdProductoEstado, d.nombre_producto_estado, d.lote, d.fecha_vence, d.lic_plate,
       d.IdMotivoDevolucion, d.user_agr, d.fec_agr
FROM trans_re_det d
WHERE d.IdRecepcionEnc IN (1567,1597,1608,1653,1657,1675,1705,2179) AND d.IdProductoBodega=381
ORDER BY d.IdRecepcionEnc, d.No_Linea
"""))

# F03 — Movs WMS164 BM2601 (todos)
export("F03_movs_BM2601_full.csv", q("""
SELECT m.IdMovimiento, m.IdTipoTarea, st.Nombre AS tipo_tarea,
       m.fecha, m.cantidad, m.IdPresentacion, m.lote, m.lic_plate,
       m.IdUbicacionOrigen, m.IdUbicacionDestino,
       m.IdEstadoOrigen, eO.nombre AS estado_origen,
       m.IdEstadoDestino, eD.nombre AS estado_destino,
       m.usuario_agr, m.IdRecepcion, m.IdRecepcionDet, m.IdPedidoEnc, m.IdDespachoEnc
FROM trans_movimientos m
LEFT JOIN sis_tipo_tarea st ON st.IdTipoTarea=m.IdTipoTarea
LEFT JOIN producto_estado eO ON eO.IdEstado=m.IdEstadoOrigen
LEFT JOIN producto_estado eD ON eD.IdEstado=m.IdEstadoDestino
WHERE m.IdProductoBodega=381 AND m.lote='BM2601' ORDER BY m.fecha
"""))

# F04 — Resumen movs BM2601 BUEN
export("F04_resumen_movs_BM2601_buen.csv", q("""
SELECT m.IdTipoTarea, st.Nombre AS tipo,
       SUM(CASE WHEN m.IdEstadoOrigen=1 AND m.IdEstadoDestino=1 THEN m.cantidad ELSE 0 END) interno_BUEN,
       SUM(CASE WHEN m.IdEstadoOrigen=1 AND m.IdEstadoDestino<>1 THEN m.cantidad ELSE 0 END) sale_BUEN,
       SUM(CASE WHEN m.IdEstadoOrigen<>1 AND m.IdEstadoDestino=1 THEN m.cantidad ELSE 0 END) entra_BUEN,
       COUNT(*) movs
FROM trans_movimientos m
LEFT JOIN sis_tipo_tarea st ON st.IdTipoTarea=m.IdTipoTarea
WHERE m.IdProductoBodega=381 AND m.lote='BM2601' AND m.fecha BETWEEN '2021-01-29' AND '2026-04-29'
GROUP BY m.IdTipoTarea, st.Nombre ORDER BY 5 DESC
"""))

# F05 — Resumen movs BM2601 MAL
export("F05_resumen_movs_BM2601_mal.csv", q("""
SELECT m.IdTipoTarea, st.Nombre AS tipo,
       SUM(CASE WHEN m.IdEstadoOrigen=16 AND m.IdEstadoDestino=16 THEN m.cantidad ELSE 0 END) interno_MAL,
       SUM(CASE WHEN m.IdEstadoOrigen=16 AND m.IdEstadoDestino<>16 THEN m.cantidad ELSE 0 END) sale_MAL,
       SUM(CASE WHEN m.IdEstadoOrigen<>16 AND m.IdEstadoDestino=16 THEN m.cantidad ELSE 0 END) entra_MAL,
       COUNT(*) movs
FROM trans_movimientos m
LEFT JOIN sis_tipo_tarea st ON st.IdTipoTarea=m.IdTipoTarea
WHERE m.IdProductoBodega=381 AND m.lote='BM2601' AND m.fecha BETWEEN '2021-01-29' AND '2026-04-29'
GROUP BY m.IdTipoTarea, st.Nombre ORDER BY 5 DESC
"""))

# F06 — Stock actual BM2601 por estado
export("F06_stock_actual_BM2601.csv", q("""
SELECT IdProductoEstado, IdPresentacion, COUNT(*) filas, SUM(cantidad) cant_total_um_bas,
       SUM(CASE WHEN IdPresentacion<>0 THEN cantidad/5.0 ELSE cantidad END) cajas_aprox
FROM stock WHERE IdProductoBodega=381 AND lote='BM2601' AND cantidad>0
GROUP BY IdProductoEstado, IdPresentacion ORDER BY IdProductoEstado, IdPresentacion
"""))

# F07 — Cambios de estado BM2601 detalle
export("F07_cest_BM2601_detalle.csv", q("""
SELECT m.IdMovimiento, m.fecha, m.cantidad, m.IdPresentacion, m.lic_plate,
       m.IdEstadoOrigen, eO.nombre estO, m.IdEstadoDestino, eD.nombre estD, m.usuario_agr
FROM trans_movimientos m
LEFT JOIN producto_estado eO ON eO.IdEstado=m.IdEstadoOrigen
LEFT JOIN producto_estado eD ON eD.IdEstado=m.IdEstadoDestino
WHERE m.IdProductoBodega=381 AND m.lote='BM2601' AND m.IdTipoTarea=3
ORDER BY m.fecha
"""))

# F08 — Confirmar que SP/Vistas no contienen las cols del reporte
export("F08_busqueda_sp_reporte.csv", q("""
SELECT o.name, o.type_desc, o.modify_date,
       CASE WHEN m.definition LIKE '%Inventario_Inicial%' THEN 1 ELSE 0 END tiene_inv_ini,
       CASE WHEN m.definition LIKE '%Existencia_Al%' THEN 1 ELSE 0 END tiene_exist_al,
       CASE WHEN m.definition LIKE '%Ajustes_P%' THEN 1 ELSE 0 END tiene_ajust_p,
       CASE WHEN m.definition LIKE '%Stock en una fecha%' THEN 1 ELSE 0 END tiene_titulo
FROM sys.sql_modules m INNER JOIN sys.objects o ON o.object_id=m.object_id
WHERE o.type IN ('P','V','FN','TF','IF')
  AND (m.definition LIKE '%Inventario_Inicial%' OR m.definition LIKE '%Existencia_Al%'
       OR m.definition LIKE '%Ajustes_P%' OR m.definition LIKE '%Stock en una fecha%')
"""))

if __name__ == "__main__":
    print("Wave 15 OK - 8 CSVs en outputs/wave-15-reporte/")
