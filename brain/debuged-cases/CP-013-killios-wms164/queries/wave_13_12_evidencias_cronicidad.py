"""wave-13-12: re-correr queries del hallazgo de cronicidad y exportar CSVs.

Genera 4 archivos CSV listos para abrir en Excel:
  E01_definicion_duplicado.csv      — los 469 combos duplicados (la base)
  E02_filas_duplicadas_completas.csv — las 919 filas individuales (para auditar fila por fila)
  E03_distribucion_mensual.csv      — la tabla mes a mes
  E04_distribucion_diaria_top60.csv — los 60 dias con mas filas duplicadas
  E05_pico_noviembre_2025.csv       — desglose dia por dia de noviembre 2025
"""
import csv, os, sys, datetime
sys.path.insert(0, "/tmp/wms-brain-fresh/brain/debuged-cases/CP-013-killios-wms164/queries")
from _db import q

OUT = "/tmp/wave_13_12_csvs"
os.makedirs(OUT, exist_ok=True)


def export_csv(name, rows):
    path = os.path.join(OUT, name)
    if not rows:
        print(f"  {name}: SIN FILAS")
        return
    with open(path, "w", newline="", encoding="utf-8") as f:
        w = csv.DictWriter(f, fieldnames=list(rows[0].keys()))
        w.writeheader()
        for r in rows:
            w.writerow({k: ("" if v is None else v) for k, v in r.items()})
    print(f"  {name}: {len(rows)} filas exportadas")


print("=" * 70)
print("E01 — Definición de duplicado: los 469 combos (llave natural repetida)")
print("=" * 70)
e01 = q("""
SELECT
    s.IdProductoBodega,
    pb.IdProducto,
    p.codigo  AS producto_codigo,
    p.nombre  AS producto_nombre,
    s.IdUbicacion,
    u.descripcion AS ubicacion_codigo,
    s.IdProductoEstado,
    pe.nombre AS estado,
    s.Lote,
    s.lic_plate,
    COUNT(*)      AS filas_duplicadas,
    SUM(s.Cantidad) AS un_totales,
    MIN(s.fecha_ingreso) AS primera_fecha,
    MAX(s.fecha_ingreso) AS ultima_fecha
FROM stock s
LEFT JOIN producto_bodega pb ON pb.IdProductoBodega = s.IdProductoBodega
LEFT JOIN producto         p ON p.IdProducto         = pb.IdProducto
OUTER APPLY (SELECT TOP 1 descripcion FROM bodega_ubicacion bu WHERE bu.IdUbicacion = s.IdUbicacion) u
LEFT JOIN producto_estado pe ON pe.IdEstado = s.IdProductoEstado
WHERE s.Cantidad > 0
  AND EXISTS (
      SELECT 1 FROM stock s2
      WHERE s2.IdProductoBodega = s.IdProductoBodega
        AND s2.IdUbicacion      = s.IdUbicacion
        AND s2.IdProductoEstado = s.IdProductoEstado
        AND s2.Lote             = s.Lote
        AND ISNULL(s2.lic_plate,'') = ISNULL(s.lic_plate,'')
        AND s2.IdStock         <> s.IdStock
        AND s2.Cantidad > 0
  )
GROUP BY
    s.IdProductoBodega, pb.IdProducto, p.codigo, p.nombre,
    s.IdUbicacion, u.descripcion,
    s.IdProductoEstado, pe.Nombre,
    s.Lote, s.lic_plate
ORDER BY filas_duplicadas DESC, un_totales DESC
""")
export_csv("E01_definicion_duplicado.csv", e01)

print()
print("=" * 70)
print("E02 — Las 919 filas individuales que componen los duplicados")
print("=" * 70)
e02 = q("""
SELECT
    s.IdStock,
    s.IdProductoBodega,
    p.codigo  AS producto_codigo,
    p.nombre  AS producto_nombre,
    s.IdUbicacion,
    u.descripcion AS ubicacion_codigo,
    s.IdProductoEstado,
    pe.nombre AS estado,
    s.Lote,
    s.lic_plate,
    s.Cantidad,
    s.fecha_ingreso
FROM stock s
LEFT JOIN producto_bodega pb ON pb.IdProductoBodega = s.IdProductoBodega
LEFT JOIN producto         p ON p.IdProducto         = pb.IdProducto
OUTER APPLY (SELECT TOP 1 descripcion FROM bodega_ubicacion bu WHERE bu.IdUbicacion = s.IdUbicacion) u
LEFT JOIN producto_estado pe ON pe.IdEstado = s.IdProductoEstado
WHERE s.Cantidad > 0
  AND EXISTS (
      SELECT 1 FROM stock s2
      WHERE s2.IdProductoBodega = s.IdProductoBodega
        AND s2.IdUbicacion      = s.IdUbicacion
        AND s2.IdProductoEstado = s.IdProductoEstado
        AND s2.Lote             = s.Lote
        AND ISNULL(s2.lic_plate,'') = ISNULL(s.lic_plate,'')
        AND s2.IdStock         <> s.IdStock
        AND s2.Cantidad > 0
  )
ORDER BY s.IdProductoBodega, s.IdUbicacion, s.IdProductoEstado, s.Lote, s.lic_plate, s.IdStock
""")
export_csv("E02_filas_duplicadas_completas.csv", e02)

print()
print("=" * 70)
print("E03 — Distribución mensual de filas duplicadas")
print("=" * 70)
e03 = q("""
SELECT
    YEAR(s.fecha_ingreso) anio,
    MONTH(s.fecha_ingreso) mes,
    COUNT(*) filas_duplicadas,
    SUM(s.Cantidad) un_totales,
    COUNT(DISTINCT s.IdProductoBodega) productos_distintos
FROM stock s
WHERE s.Cantidad > 0
  AND EXISTS (
      SELECT 1 FROM stock s2
      WHERE s2.IdProductoBodega = s.IdProductoBodega
        AND s2.IdUbicacion      = s.IdUbicacion
        AND s2.IdProductoEstado = s.IdProductoEstado
        AND s2.Lote             = s.Lote
        AND ISNULL(s2.lic_plate,'') = ISNULL(s.lic_plate,'')
        AND s2.IdStock         <> s.IdStock
        AND s2.Cantidad > 0
  )
GROUP BY YEAR(s.fecha_ingreso), MONTH(s.fecha_ingreso)
ORDER BY YEAR(s.fecha_ingreso) DESC, MONTH(s.fecha_ingreso) DESC
""")
export_csv("E03_distribucion_mensual.csv", e03)
print()
print("Tabla resumen mensual:")
print(f"{'anio':>6} {'mes':>4} {'filas_dup':>10} {'un_totales':>12} {'productos':>10}")
for r in e03:
    print(f"{r['anio']:>6} {r['mes']:>4} {r['filas_duplicadas']:>10} {r['un_totales'] or 0:>12.2f} {r['productos_distintos']:>10}")

print()
print("=" * 70)
print("E04 — Top 60 días con más filas duplicadas creadas")
print("=" * 70)
e04 = q("""
SELECT TOP 60
    CAST(s.fecha_ingreso AS DATE) dia,
    DATENAME(WEEKDAY, s.fecha_ingreso) dia_semana,
    COUNT(*) filas_duplicadas,
    SUM(s.Cantidad) un_totales,
    COUNT(DISTINCT s.IdProductoBodega) productos_distintos
FROM stock s
WHERE s.Cantidad > 0
  AND EXISTS (
      SELECT 1 FROM stock s2
      WHERE s2.IdProductoBodega = s.IdProductoBodega
        AND s2.IdUbicacion      = s.IdUbicacion
        AND s2.IdProductoEstado = s.IdProductoEstado
        AND s2.Lote             = s.Lote
        AND ISNULL(s2.lic_plate,'') = ISNULL(s.lic_plate,'')
        AND s2.IdStock         <> s.IdStock
        AND s2.Cantidad > 0
  )
GROUP BY CAST(s.fecha_ingreso AS DATE), DATENAME(WEEKDAY, s.fecha_ingreso)
ORDER BY COUNT(*) DESC
""")
export_csv("E04_distribucion_diaria_top60.csv", e04)
print()
print("Top 15 dias:")
print(f"{'dia':>12} {'dia_semana':>12} {'filas_dup':>10} {'un_totales':>12}")
for r in e04[:15]:
    print(f"{str(r['dia']):>12} {r['dia_semana']:>12} {r['filas_duplicadas']:>10} {r['un_totales'] or 0:>12.2f}")

print()
print("=" * 70)
print("E05 — Desglose dia por dia de NOVIEMBRE 2025 (mes pico)")
print("=" * 70)
e05 = q("""
SELECT
    CAST(s.fecha_ingreso AS DATE) dia,
    DATENAME(WEEKDAY, s.fecha_ingreso) dia_semana,
    COUNT(*) filas_duplicadas,
    SUM(s.Cantidad) un_totales
FROM stock s
WHERE s.Cantidad > 0
  AND s.fecha_ingreso >= '2025-11-01'
  AND s.fecha_ingreso <  '2025-12-01'
  AND EXISTS (
      SELECT 1 FROM stock s2
      WHERE s2.IdProductoBodega = s.IdProductoBodega
        AND s2.IdUbicacion      = s.IdUbicacion
        AND s2.IdProductoEstado = s.IdProductoEstado
        AND s2.Lote             = s.Lote
        AND ISNULL(s2.lic_plate,'') = ISNULL(s.lic_plate,'')
        AND s2.IdStock         <> s.IdStock
        AND s2.Cantidad > 0
  )
GROUP BY CAST(s.fecha_ingreso AS DATE), DATENAME(WEEKDAY, s.fecha_ingreso)
ORDER BY CAST(s.fecha_ingreso AS DATE)
""")
export_csv("E05_pico_noviembre_2025.csv", e05)
print()
print("Noviembre 2025 dia por dia:")
print(f"{'dia':>12} {'dia_semana':>12} {'filas_dup':>10}")
for r in e05:
    print(f"{str(r['dia']):>12} {r['dia_semana']:>12} {r['filas_duplicadas']:>10}")

print()
print("=" * 70)
print(f"DONE {datetime.datetime.utcnow()}Z — CSVs en {OUT}")
print("=" * 70)
