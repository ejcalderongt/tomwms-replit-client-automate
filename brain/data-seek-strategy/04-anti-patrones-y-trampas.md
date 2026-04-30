# 04 — Anti-patrones y trampas

Lista viva de errores que cometimos (o casi) y cómo evitarlos. Cada entrada incluye **síntoma**, **causa**, **solución**.

---

## T01 — `cantidad_hist` NO es delta, es snapshot

**Síntoma:** sumás `cantidad_hist` esperando obtener total movido y te da un número absurdamente grande.

**Causa:** `cantidad_hist` en `trans_movimientos` es el saldo de la ubicación/LP DESPUÉS del movimiento, no la cantidad movida. Lo mismo aplica a `stock_hist.cantidad`.

**Solución:** usar `cantidad` (delta) para sumar; usar `cantidad_hist` solo para reconstruir saldos puntuales en el tiempo. Si necesitás el delta entre snapshots de stock_hist, hacer LAG por IdStock ordenado por fecha.

---

## T02 — Reservas zombi en `stock_res`

**Síntoma:** sumás `stock_res.cantidad` y te queda mucho más alto que las reservas reales.

**Causa:** `stock_res` no tiene columna `activo`. Hay reservas contra `IdStock` cuyo `stock.activo=0` (stock retirado, reservas zombi).

**Solución:** SIEMPRE joinear `stock_res` contra `stock` y filtrar `stock.activo=1`. Ejemplo en Receta E.

---

## T03 — Nombres de columnas inconsistentes entre tablas

**Síntoma:** la query falla con "Invalid column name".

**TOMWMS NO es consistente.** Verificado contra Killios `TOMWMS_KILLIOS_PRD_2026` (SQL Server 2022, 360 tablas):

| Tabla                | Fecha de creación | Usuario de creación |
|----------------------|-------------------|---------------------|
| `trans_movimientos`  | `fecha_agr`       | `usuario_agr`       |
| `trans_picking_ubic` | `fec_agr`         | `user_agr`          |
| `trans_ajuste_enc`   | `fec_agr` (+ `fecha` propia del ajuste) | `user_agr` (+ `idusuario`) |
| `stock_hist`         | `fec_agr`         | `user_agr`          |

**Regla práctica:** asumí `fec_agr`/`user_agr` por defecto en todas las tablas; la excepción es `trans_movimientos` que usa la versión larga.

**Otros gotchas:**
- En `usuario`: NO existe columna `usuario` (login string). Mostrar nombre con `nombres + ' ' + ISNULL(apellidos, '')`.
- En `trans_ajuste_enc`: PK es `idajusteenc` todo minúscula; igual `idajustedet` en det. Pero `IdProductoBodega` es PascalCase. Inconsistente.
- En `trans_picking_ubic` hay DOS flags de daño: `dañado_picking` y `dañado_verificacion`. El bug de CP-013 es del primero.
- Mezcla de PascalCase y snake_case en TOMWMS (`IdProductoBodega` pero `cantidad_hist`). No asumas, verificá con `sp_help` o `sys.columns`.

**Solución:** ante la duda, `EXEC sp_help '<tabla>'` o:
```sql
SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('<tabla>') ORDER BY column_id;
```

---

## T04 — Joins duplicantes

**Síntoma:** sumás cantidades y te da el doble (o triple) del esperado.

**Causa:** join contra una tabla con N rows por PK del padre (ej: `stock` joineado con `stock_res` sin GROUP BY produce N copias de cada stock).

**Solución:** subqueries con `SUM` antes del join, o `OUTER APPLY` con TOP 1 si solo necesitás un valor representativo. Ejemplo en Receta E (CTEs `stk` y `res` agregan ANTES de joinear).

---

## T05 — Overflows / cantidades absurdas

**Síntoma:** sumás cantidades y obtenés números en miles de millones donde esperabas miles.

**Causa:** rows corruptos con valores absurdos. Confirmado en CP-013: `trans_ajuste_det.idajustedet=638` tenía `cantidad_original=4,804,442,747,532` (4.8 billones), observación "sa", del 2021. Probablemente test mal hecho dejado en producción.

**Solución:** ANTES de cualquier `SUM()` sobre tablas con cantidades, correr `audit-overflow.py` para detectar outliers > N desviaciones estándar o > umbral absoluto. Excluir explícitamente del análisis y documentar en el reporte.

---

## T06 — Snapshots vs vivas

**Síntoma:** los números del histórico no cuadran con los de la vista actual.

**Causa:** `producto_bodega_stock`, `VW_Stock_Resumen` y similares pueden estar desactualizadas si el bug del cliente afecta justo el flujo de actualización. `stock_hist` puede no tener todos los movimientos si solo se guarda en algunos casos.

**Solución:** confirmar el cuadre entre `stock` (viva) y los resúmenes/historiales antes de basar conclusiones en ellos. Si difieren, asumí que `stock` viva es la verdad operativa y los demás están sucios.

---

## T07 — Tipo de tarea AJCANTN no aplica para dañados

**Síntoma:** el cliente dice "marqué como dañado, debería haber generado ajuste" pero no encontrás el AJCANTN.

**Causa:** el flujo `dañado_picking=1` desde BOF NO genera movimiento `IdTipoTarea=17`. Es un bug funcional documentado en CP-013, no un error de búsqueda.

**Solución:** no busques AJCANTN para cubrir dañados; el correcto sería un movimiento a ubicación MERMA, que tampoco se genera. Por eso el bug.

---

## T08 — Killios no usa stock_jornada

**Síntoma:** la tabla `stock_jornada` está vacía; pensás que es un problema de auditoría.

**Causa:** Killios tiene el parámetro de "snapshot diario" en false. La tabla simplemente no se llena. No es bug.

**Solución:** confirmar parámetro de cada cliente en `client-index/<cliente>.yml` antes de basar análisis temporal en stock_jornada. Para Killios, usar `stock_hist` (que es por movimiento, no por día) o reconstruir desde `trans_movimientos`.

---

## T09 — Ñ y acentos en nombres de columnas

**Síntoma:** la query falla en codificación.

**Caso real:** `trans_picking_ubic.dañado_picking` (con Ñ). Si lo metés en un script Python sin `# coding: utf-8` y conexión configurada en latin1/utf8, puede romper.

**Solución:** usar `pymssql` con `charset='UTF-8'` en el connect, y declarar encoding utf-8 en los .py. Si igual falla, escapar como `[dañado_picking]`.

---

## T10 — `sa` puede leer todo, también escribir

**Síntoma:** querés correr una query rápida con `sa` y un typo te genera un DELETE.

**Causa:** Killios entrega `sa` para análisis. Es cuenta admin completa.

**Solución:** el `_db.py` template bloquea explícitamente cualquier statement que NO empiece con SELECT/WITH/EXEC sp_help. Si necesitás otra cosa, hacelo manualmente con doble confirmación.

---

## T11 — Filtrar por `activo=1` en producto_bodega

**Síntoma:** te aparecen productos descontinuados en los resultados.

**Causa:** `producto_bodega.activo=0` significa producto desactivado en esa bodega. El stock viejo puede seguir existiendo.

**Solución:** depende del análisis: si es auditoría de bug actual, filtrar por `pb.activo=1`. Si es auditoría histórica, NO filtrar (puede haber stock fantasma justamente en productos desactivados).

---

## T12 — `referencia` es string libre

**Síntoma:** querés joinear movimientos por número de pedido y no encontrás match exacto.

**Causa:** `referencia` puede tener prefijos/espacios/case distinto. Vi cosas como `"PED 12345"`, `"ped12345"`, `"12345  "`.

**Solución:** `LIKE '%<num>%'` o `LTRIM(RTRIM(UPPER(referencia)))` antes de comparar. Documentar el formato esperado por cliente en su yml.

---

## T14 — Collation conflict entre tablas

**Síntoma:** `Cannot resolve the collation conflict between "Modern_Spanish_CI_AS" and "SQL_Latin1_General_CP1_CI_AS" in the equal to operation.`

**Causa:** TOMWMS tiene tablas creadas en distintos momentos con distinto collation. `trans_movimientos` y `trans_picking_ubic` no comparten collation en `lic_plate`/`lote`. El error solo aparece cuando joineás strings entre ellas.

**Solución:** forzar `COLLATE DATABASE_DEFAULT` en ambos lados del JOIN/WHERE:

```sql
ON m.lic_plate COLLATE DATABASE_DEFAULT = pu.lic_plate COLLATE DATABASE_DEFAULT
AND m.lote COLLATE DATABASE_DEFAULT = pu.lote COLLATE DATABASE_DEFAULT
```

Aplicalo SIEMPRE que cruces strings entre tablas distintas. No daña nada en tablas que ya comparten collation.

---

## T13 — Fechas inválidas en BD

**Síntoma:** filtrás por fecha y te aparecen rows del año 1900 o 2099.

**Causa:** valores default de SQL Server cuando no se inserta fecha. Pueden ser legales o ser bug.

**Solución:** usar `audit-overflow.py` también sobre fechas (busca rows con `fecha < '2010-01-01' OR fecha > '2030-01-01'`). Documentar y excluir.

---

## T15. `producto_bodega` es tabla puente (solo IDs)

**Síntoma:** queries que esperan `pb.codigo` o `pb.nombre` fallan con `Invalid column name`.

**Causa:** `producto_bodega` solo tiene `IdProductoBodega`, `IdProducto`, `IdBodega`, flags y auditoría. Es la tabla puente entre `producto` (catálogo global con `codigo`/`nombre`/`codigo_barra`/`precio`) y la bodega operativa.

**Patrón correcto:**
```sql
SELECT p.codigo, p.nombre, pb.IdBodega
FROM trans_picking_ubic pu
LEFT JOIN producto_bodega pb ON pb.IdProductoBodega = pu.IdProductoBodega
LEFT JOIN producto p ON p.IdProducto = pb.IdProducto
```

**Bonus:** un mismo `IdProducto` puede tener múltiples `IdProductoBodega` por bodega o por presentación. En CP-014 se observó WMS92 con dos `IdProductoBodega` distintos para el mismo código. Si agrupás por código sin tener en cuenta la presentación podés ocultar diferencias reales.

---

## T16. `SUM(NULL)` rompe `f-string` formatting en Python

**Síntoma:** script Python con `f'{r["um"]:,.0f}'` lanza `unsupported format string passed to NoneType.__format__` en algunas BDs.

**Causa:** SQL Server devuelve `NULL` cuando agregás `SUM(col)` sobre cero filas. Eso vuelve a Python como `None`. Cualquier intento de formatear `None` con `:,.0f` o `:>10` revienta.

**Solución:** envolver siempre con `ISNULL(SUM(...),0)` en SQL, o `(r["um"] or 0)` en Python antes de formatear. Vale para `MIN`, `MAX`, `COUNT(DISTINCT col)` con cero filas también.

```sql
SELECT ISNULL(SUM(cantidad),0) AS um FROM trans_picking_ubic WHERE 1=0;
```

```python
print(f'{(r["um"] or 0):,.0f}')
```

**Origen:** detectado en CP-014 al iterar 7 BDs simultáneamente. 3 de las 7 lanzaron el error en queries que en Killios funcionaban porque siempre tenía datos.

---

(Agregar nuevas trampas a medida que aparezcan. Cada caso cerrado debe revisar este doc.)
