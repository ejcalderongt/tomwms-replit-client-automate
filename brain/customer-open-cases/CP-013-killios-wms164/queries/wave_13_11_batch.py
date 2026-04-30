"""wave-13-11: re-medicion live de CP-013/V-DATAWAY-004 post-restablecimiento firewall.

Contexto:
  Wave 13-9 corrio q01..q12 contra TOMWMS_KILLIOS_PRD_2026 (live) con resultado
  469 combos / 919 filas redundantes / 18.7%. Wave 13-10 fue offline (firewall
  caido del lado servidor). Wave 13-11 re-mide live ahora que Erik abrio el SG
  (regla sgr-04f0866290d18a0ec, IP 35.227.125.212/32 -> puerto 1437).

Objetivo:
  - Confirmar que los numeros 469/919 son estables vs snapshot wms-db-brain
    del 2026-04-27.
  - Medir la cronicidad: cuanto tiempo lleva activo el bug.
  - Refinar las hipotesis activas (H1..H4) con datos live.
  - Detectar nuevos patrones que el snapshot offline no podia ver.

READ-ONLY. SELECT puro. Una sola conexion. Todas las consultas en serie.

Re-correr con:
  python3 wave_13_11_batch.py | tee outputs/wave-13-11/batch.out
"""
import os, datetime
from _db import q

def show(title, rows, max_rows=30):
    print(f"\n{'='*78}\n>>> {title}\n{'='*78}")
    if not rows:
        print("  (sin filas)")
        return
    cols = list(rows[0].keys())
    print("  " + " | ".join(cols))
    for r in rows[:max_rows]:
        print("  " + " | ".join(str(r[c])[:35] for c in cols))
    if len(rows) > max_rows:
        print(f"  ... +{len(rows)-max_rows} mas")

# Q11 fresh: marker #EJCAJUSTEDESFASE (sigue refutando V-001)
show("q11 marker EJCAJUSTEDESFASE", q("""
    SELECT COUNT(*) cnt FROM trans_movimientos WHERE Serie = '#EJCAJUSTEDESFASE'
"""))

# Q12 fresh: alcance actual del anti-patron
show("q12 alcance actual", q("""
    SELECT
        (SELECT COUNT(*) FROM stock WHERE Cantidad > 0) AS total_stock_activo,
        sub.combos_duplicados, sub.filas_totales_en_combos,
        sub.filas_redundantes, sub.un_involucradas
    FROM (
        SELECT COUNT(*) AS combos_duplicados,
               SUM(filas) AS filas_totales_en_combos,
               SUM(filas - 1) AS filas_redundantes,
               SUM(total_un) AS un_involucradas
        FROM (
            SELECT IdProductoBodega, IdUbicacion, IdProductoEstado, Lote, lic_plate,
                   COUNT(*) AS filas, SUM(Cantidad) AS total_un
            FROM stock WHERE Cantidad > 0
            GROUP BY IdProductoBodega, IdUbicacion, IdProductoEstado, Lote, lic_plate
            HAVING COUNT(*) > 1
        ) sub2
    ) sub
"""))

# Q13: caso fundacional WMS164 (134176, 134177) sigue intacto?
show("q13 caso fundacional WMS164", q("""
    SELECT IdStock, IdProductoBodega, IdUbicacion, IdProductoEstado,
           Lote, lic_plate, Cantidad, fecha_ingreso
    FROM stock WHERE IdStock IN (134176, 134177)
    ORDER BY IdStock
"""))

# Q14: stocks creados desde el snapshot (delta de actividad)
show("q14 stocks nuevos post-snapshot 2026-04-27 01:29Z", q("""
    SELECT COUNT(*) AS stocks_nuevos,
           MIN(fecha_ingreso) AS primera_fecha,
           MAX(fecha_ingreso) AS ultima_fecha,
           DATEDIFF(HOUR, MIN(fecha_ingreso), MAX(fecha_ingreso)) AS rango_horas
    FROM stock WHERE fecha_ingreso >= '2026-04-27 01:29:00'
"""))

# Q15: duplicados FRESCOS (combos con al menos una fila post-snapshot)
show("q15 duplicados frescos post-snapshot", q("""
    SELECT
        COUNT(*) AS combos_con_duplicado_fresco,
        SUM(filas) AS filas_totales,
        SUM(filas_nuevas) AS filas_nuevas,
        SUM(filas - 1) AS filas_redundantes_totales
    FROM (
        SELECT IdProductoBodega, IdUbicacion, IdProductoEstado, Lote, lic_plate,
               COUNT(*) AS filas,
               SUM(CASE WHEN fecha_ingreso >= '2026-04-27 01:29:00' THEN 1 ELSE 0 END) AS filas_nuevas
        FROM stock WHERE Cantidad > 0
        GROUP BY IdProductoBodega, IdUbicacion, IdProductoEstado, Lote, lic_plate
        HAVING COUNT(*) > 1 AND SUM(CASE WHEN fecha_ingreso >= '2026-04-27 01:29:00' THEN 1 ELSE 0 END) >= 1
    ) sub
"""))

# Q16: tasa de duplicados por dia (top 30 dias del historico, sin filtro de fecha)
show("q16 tasa duplicados por dia (top 30)", q("""
    SELECT TOP 30 CAST(s.fecha_ingreso AS DATE) dia, COUNT(*) filas_dup
    FROM stock s
    INNER JOIN (
        SELECT IdProductoBodega, IdUbicacion, IdProductoEstado, Lote, lic_plate
        FROM stock WHERE Cantidad > 0
        GROUP BY IdProductoBodega, IdUbicacion, IdProductoEstado, Lote, lic_plate
        HAVING COUNT(*) > 1
    ) cd ON cd.IdProductoBodega=s.IdProductoBodega AND cd.IdUbicacion=s.IdUbicacion
        AND cd.IdProductoEstado=s.IdProductoEstado AND cd.Lote=s.Lote
        AND ISNULL(cd.lic_plate,'')=ISNULL(s.lic_plate,'')
    WHERE s.Cantidad > 0 AND s.fecha_ingreso IS NOT NULL
    GROUP BY CAST(s.fecha_ingreso AS DATE)
    ORDER BY filas_dup DESC
"""))

# Q17: distribucion mensual (cronicidad)
show("q17 distribucion mensual de filas duplicadas", q("""
    SELECT YEAR(s.fecha_ingreso) y, MONTH(s.fecha_ingreso) m, COUNT(*) filas
    FROM stock s
    INNER JOIN (
        SELECT IdProductoBodega, IdUbicacion, IdProductoEstado, Lote, lic_plate
        FROM stock WHERE Cantidad > 0
        GROUP BY IdProductoBodega, IdUbicacion, IdProductoEstado, Lote, lic_plate
        HAVING COUNT(*) > 1
    ) cd ON cd.IdProductoBodega=s.IdProductoBodega AND cd.IdUbicacion=s.IdUbicacion
        AND cd.IdProductoEstado=s.IdProductoEstado AND cd.Lote=s.Lote
        AND ISNULL(cd.lic_plate,'')=ISNULL(s.lic_plate,'')
    WHERE s.Cantidad > 0
    GROUP BY YEAR(s.fecha_ingreso), MONTH(s.fecha_ingreso)
    ORDER BY y DESC, m DESC
"""))

# Q18: distribucion del numero de filas por combo duplicado
show("q18 distribucion combos duplicados", q("""
    SELECT COUNT(*) casos_2_filas,
           SUM(CASE WHEN filas=2 THEN 1 ELSE 0 END) exactamente_2,
           SUM(CASE WHEN filas=3 THEN 1 ELSE 0 END) exactamente_3,
           SUM(CASE WHEN filas>=4 THEN 1 ELSE 0 END) cuatro_o_mas,
           MAX(filas) max_filas_en_un_combo
    FROM (
        SELECT IdProductoBodega, IdUbicacion, IdProductoEstado, Lote, lic_plate, COUNT(*) filas
        FROM stock WHERE Cantidad>0
        GROUP BY IdProductoBodega, IdUbicacion, IdProductoEstado, Lote, lic_plate
        HAVING COUNT(*) > 1
    ) s
"""))

# Q19: lic_plate breakdown - CRITICO para H1
show("q19 lic_plate breakdown (refuta H1 fuerte)", q("""
    SELECT
        CASE WHEN lic_plate IS NULL THEN 'NULL'
             WHEN lic_plate = '' THEN 'VACIO'
             WHEN lic_plate = '0' THEN 'CERO'
             ELSE 'CON_VALOR' END estado,
        COUNT(*) combos, SUM(filas) filas
    FROM (
        SELECT IdProductoBodega, IdUbicacion, IdProductoEstado, Lote, lic_plate, COUNT(*) filas
        FROM stock WHERE Cantidad>0
        GROUP BY IdProductoBodega, IdUbicacion, IdProductoEstado, Lote, lic_plate
        HAVING COUNT(*) > 1
    ) s
    GROUP BY CASE WHEN lic_plate IS NULL THEN 'NULL'
                  WHEN lic_plate = '' THEN 'VACIO'
                  WHEN lic_plate = '0' THEN 'CERO'
                  ELSE 'CON_VALOR' END
    ORDER BY combos DESC
"""))

# Q20: top productos con mas filas duplicadas (subquery, no WITH)
show("q20 top productos generadores de duplicados", q("""
    SELECT TOP 25 p.codigo, p.nombre,
        SUM(sub.filas) filas_dup, SUM(sub.un) un_total
    FROM (
        SELECT IdProductoBodega, IdUbicacion, IdProductoEstado, Lote, lic_plate,
               COUNT(*) filas, SUM(Cantidad) un
        FROM stock WHERE Cantidad > 0
        GROUP BY IdProductoBodega, IdUbicacion, IdProductoEstado, Lote, lic_plate
        HAVING COUNT(*) > 1
    ) sub
    JOIN producto_bodega pb ON pb.IdProductoBodega=sub.IdProductoBodega
    JOIN producto p ON p.IdProducto=pb.IdProducto
    GROUP BY p.codigo, p.nombre
    ORDER BY filas_dup DESC, un_total DESC
"""))

# Q21: combos degenerados (con muchas filas)
show("q21 combos degenerados >= 8 filas", q("""
    SELECT TOP 5 IdProductoBodega, IdUbicacion, IdProductoEstado, Lote, lic_plate,
           COUNT(*) filas, SUM(Cantidad) un
    FROM stock WHERE Cantidad>0
    GROUP BY IdProductoBodega, IdUbicacion, IdProductoEstado, Lote, lic_plate
    HAVING COUNT(*) >= 8
    ORDER BY COUNT(*) DESC
"""))

# Q22: tipos de tarea sobre lotes en combos duplicados (REFUTA "exclusivo CEST")
show("q22 tipos de tarea sobre lotes duplicados (DESDE 2025-05)", q("""
    SELECT st.IdTipoTarea, st.Nombre, COUNT(*) movs
    FROM trans_movimientos m
    JOIN sis_tipo_tarea st ON st.IdTipoTarea=m.IdTipoTarea
    WHERE m.fecha >= '2025-05-01'
      AND EXISTS (
          SELECT 1 FROM stock s
          WHERE s.IdProductoBodega=m.IdProductoBodega
            AND s.Lote COLLATE Modern_Spanish_CI_AS = m.lote COLLATE Modern_Spanish_CI_AS
            AND s.Cantidad>0
            AND EXISTS (
                SELECT 1 FROM stock s2
                WHERE s2.IdProductoBodega=s.IdProductoBodega
                  AND s2.IdUbicacion=s.IdUbicacion AND s2.IdProductoEstado=s.IdProductoEstado
                  AND s2.Lote=s.Lote AND ISNULL(s2.lic_plate,'')=ISNULL(s.lic_plate,'')
                  AND s2.Cantidad>0 AND s2.IdStock<>s.IdStock
            )
      )
    GROUP BY st.IdTipoTarea, st.Nombre
    ORDER BY movs DESC
"""))

# Q23: rango temporal del bug (cronicidad medida)
show("q23 cronicidad - rango temporal del bug", q("""
    SELECT MIN(s.fecha_ingreso) primer_dup, MAX(s.fecha_ingreso) ultimo_dup,
           DATEDIFF(DAY, MIN(s.fecha_ingreso), MAX(s.fecha_ingreso)) dias_de_bug
    FROM stock s
    INNER JOIN (
        SELECT IdProductoBodega, IdUbicacion, IdProductoEstado, Lote, lic_plate
        FROM stock WHERE Cantidad > 0
        GROUP BY IdProductoBodega, IdUbicacion, IdProductoEstado, Lote, lic_plate
        HAVING COUNT(*) > 1
    ) cd ON cd.IdProductoBodega=s.IdProductoBodega AND cd.IdUbicacion=s.IdUbicacion
        AND cd.IdProductoEstado=s.IdProductoEstado AND cd.Lote=s.Lote
        AND ISNULL(cd.lic_plate,'')=ISNULL(s.lic_plate,'')
"""))

# Q24: confirmar que la BD sigue sin UNIQUE INDEX (V-005 sigue vivo)
show("q24 V-005 confirmacion: indices unique sobre stock", q("""
    SELECT name, type_desc, is_unique, is_primary_key, is_unique_constraint
    FROM sys.indexes
    WHERE object_id = OBJECT_ID('dbo.stock')
    ORDER BY name
"""))

# Q25: confirmar que el check Cantidad>0 sigue activo (H4 sigue posible)
show("q25 H4 confirmacion: check Stock_NonNegative", q("""
    SELECT cc.name, cc.definition, cc.is_disabled
    FROM sys.check_constraints cc
    WHERE cc.parent_object_id = OBJECT_ID('dbo.stock')
"""))

print(f"\n{'='*78}\nbatch wave-13-11 completo {datetime.datetime.utcnow()}Z")
