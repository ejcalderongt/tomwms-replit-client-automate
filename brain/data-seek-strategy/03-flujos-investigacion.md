# 03 — Flujos de investigación (recetas)

Recetas paso a paso para los tipos de caso más comunes. Cada receta lista las queries en orden y qué hay que validar en cada paso.

---

## Receta A: Cuadre de un lote específico (BD vs físico)

**Contexto:** el cliente reporta que un lote tiene más/menos stock en BD que físico.

1. **Identificar producto y lote**
   ```sql
   SELECT p.IdProducto, pb.IdProductoBodega, p.codigo, p.nombre
   FROM producto p
   JOIN producto_bodega pb ON pb.IdProducto = p.IdProducto
   WHERE p.codigo = '<CODIGO>';
   ```

2. **Stock activo del lote por LP/ubicación**
   ```sql
   SELECT s.IdStock, s.IdUbicacion, u.nombre ubic, s.IdLicencia, s.cantidad
   FROM stock s WITH (NOLOCK)
   LEFT JOIN ubicacion u ON u.IdUbicacion = s.IdUbicacion
   WHERE s.IdProductoBodega = <PB> AND s.lote = '<LOTE>' AND s.activo = 1;
   ```

3. **Recepciones del lote**
   ```sql
   SELECT IdMovimiento, fecha_agr, cantidad, IdLicencia
   FROM trans_movimientos
   WHERE IdProductoBodega = <PB> AND lote = '<LOTE>' AND IdTipoTarea = 1
   ORDER BY fecha_agr;
   ```

4. **Despachos del lote**
   ```sql
   SELECT IdMovimiento, fecha_agr, cantidad, referencia
   FROM trans_movimientos
   WHERE IdProductoBodega = <PB> AND lote = '<LOTE>' AND IdTipoTarea = 5
   ORDER BY fecha_agr;
   ```

5. **Ajustes formales (AJCANTN) del lote**
   ```sql
   SELECT IdMovimiento, fecha_agr, cantidad, referencia
   FROM trans_movimientos
   WHERE IdProductoBodega = <PB> AND lote = '<LOTE>' AND IdTipoTarea = 17;
   ```

6. **Cuadre matemático esperado:** `recepciones − despachos + ajustes = stock activo actual`. Si no cuadra, salta a Receta B (dañados sin descuento) o Receta D (ajustes manuales históricos).

7. **Convertir a cajas** usando `producto_presentacion.factor`. Confirmar con el cliente cuál presentación usa para reportar.

**Caso de referencia:** CP-013 (KILLIOS WMS164/BG2512) — wave 18 + 20.

---

## Receta B: Detectar dañados sin descuento (bug CP-013 generalizado)

**Contexto:** sospecha de stock fantasma. Aplica si el cliente usa el flag `dañado_picking` desde el BOF.

1. **Líneas con flag dañado activo**
   ```sql
   SELECT user_agr, COUNT(*) n_lineas, SUM(cantidad_solicitada) um_danadas,
          COUNT(DISTINCT IdProductoBodega) productos, COUNT(DISTINCT lote) lotes,
          MIN(fecha_agr) desde, MAX(fecha_agr) hasta
   FROM trans_picking_ubic
   WHERE dañado_picking = 1 AND activo = 1
     AND cantidad_verificada = 0 AND cantidad_despachada = 0
   GROUP BY user_agr ORDER BY um_danadas DESC;
   ```

2. **Productos top afectados**
   ```sql
   SELECT TOP 30 IdProductoBodega, SUM(cantidad_solicitada) um, COUNT(DISTINCT lote) lotes
   FROM trans_picking_ubic
   WHERE dañado_picking = 1 AND activo = 1
     AND cantidad_verificada = 0 AND cantidad_despachada = 0
   GROUP BY IdProductoBodega ORDER BY um DESC;
   ```

3. **Confirmar que NO generó AJCANTN compensatorio** (cruce contra trans_movimientos por IdLicencia + lote + ventana temporal de ±1 día):
   ```sql
   SELECT pu.IdPickingUbic, pu.cantidad_solicitada, pu.fecha_agr
   FROM trans_picking_ubic pu
   LEFT JOIN trans_movimientos m ON m.IdLicencia = pu.IdLicencia
        AND m.lote = pu.lote AND m.IdTipoTarea = 17
        AND ABS(DATEDIFF(DAY, m.fecha_agr, pu.fecha_agr)) <= 1
   WHERE pu.dañado_picking = 1 AND pu.activo = 1 AND m.IdMovimiento IS NULL;
   ```

4. Cuantificar UM totales fantasma. Si > 1% del stock total del cliente, escalar como bug crítico.

**Template ejecutable:** `templates/audit-danados-sin-ajuste.py`.

---

## Receta C: Reconstrucción del flujo de un picking

**Contexto:** el cliente reporta que un pedido específico llegó incompleto/dañado.

1. **Encabezado del despacho**
   ```sql
   SELECT * FROM trans_despacho_enc WHERE referencia = '<NRO_PEDIDO>';
   ```

2. **Líneas del despacho**
   ```sql
   SELECT * FROM trans_despacho_det WHERE IdDespachoEnc = <ID>;
   ```

3. **Pickings asociados**
   ```sql
   SELECT * FROM trans_picking_ubic WHERE IdDespachoEnc = <ID> ORDER BY fecha_agr;
   ```

4. **Movimientos generados**
   ```sql
   SELECT * FROM trans_movimientos WHERE referencia = '<NRO_PEDIDO>' ORDER BY fecha_agr;
   ```

5. **Cambios de estado del stock involucrado** (si hay sospecha de cesteo):
   ```sql
   SELECT * FROM stock_hist WHERE IdStock IN (<lista>) ORDER BY fecha_agr;
   ```

---

## Receta D: Cruce histórico de ajustes manuales

**Contexto:** validar si un bug es nuevo o crónico. Ver si el equipo del cliente lo viene compensando manualmente.

1. **Volumen total y rango temporal**
   ```sql
   SELECT MIN(fecha) desde, MAX(fecha) hasta, COUNT(*) n FROM trans_ajuste_enc;
   ```

2. **Distribución por mes y signo** (detecta gaps temporales)
   ```sql
   SELECT FORMAT(e.fecha,'yyyy-MM') mes,
          SUM(CASE WHEN d.idtipoajuste=3 THEN 1 ELSE 0 END) pos,
          SUM(CASE WHEN d.idtipoajuste=5 THEN 1 ELSE 0 END) neg,
          SUM(CASE WHEN d.idtipoajuste=3 THEN d.cantidad_nueva-d.cantidad_original ELSE 0 END) um_pos,
          SUM(CASE WHEN d.idtipoajuste=5 THEN d.cantidad_nueva-d.cantidad_original ELSE 0 END) um_neg
   FROM trans_ajuste_enc e
   JOIN trans_ajuste_det d ON d.idajusteenc = e.idajusteenc
   GROUP BY FORMAT(e.fecha,'yyyy-MM') ORDER BY mes;
   ```

3. **Top motivos**
   ```sql
   SELECT m.nombre motivo,
          CASE d.idtipoajuste WHEN 3 THEN 'Positivo' WHEN 5 THEN 'Negativo' ELSE 'Otro' END tipo,
          COUNT(*) n, SUM(d.cantidad_nueva - d.cantidad_original) delta_total
   FROM trans_ajuste_det d
   JOIN ajuste_motivo m ON m.idmotivoajuste = d.idmotivoajuste
   GROUP BY m.nombre, d.idtipoajuste ORDER BY n DESC;
   ```

4. **Outlier check obligatorio** antes de agregar UM (ver `audit-overflow.py`).

5. **Buscar ajustes específicos sobre el producto investigado**:
   ```sql
   SELECT e.idajusteenc, e.fecha, u.nombres+' '+ISNULL(u.apellidos,'') usr,
          e.referencia, e.ajuste_por_inventario, m.nombre motivo,
          d.cantidad_original, d.cantidad_nueva,
          d.cantidad_nueva-d.cantidad_original delta, d.observacion
   FROM trans_ajuste_det d
   JOIN trans_ajuste_enc e ON e.idajusteenc = d.idajusteenc
   LEFT JOIN ajuste_motivo m ON m.idmotivoajuste = d.idmotivoajuste
   LEFT JOIN usuario u ON u.IdUsuario = e.idusuario
   WHERE d.IdProductoBodega = <PB> ORDER BY e.fecha DESC;
   ```

**Caso de referencia:** CP-013 wave 21.

---

## Receta E: Detectar saldo neto negativo (stock vs reservas)

**Contexto:** alguien reporta que el reporte de stock muestra negativos.

```sql
WITH stk AS (
  SELECT IdProductoBodega, SUM(cantidad) tot
  FROM stock WITH (NOLOCK) WHERE activo=1 GROUP BY IdProductoBodega
),
res AS (
  SELECT s.IdProductoBodega, SUM(sr.cantidad) tot
  FROM stock_res sr WITH (NOLOCK)
  JOIN stock s ON s.IdStock = sr.IdStock
  WHERE s.activo = 1 GROUP BY s.IdProductoBodega
)
SELECT TOP 60 stk.IdProductoBodega, p.codigo, p.nombre,
       stk.tot stock_total, ISNULL(res.tot,0) reservas_total,
       stk.tot - ISNULL(res.tot,0) neto
FROM stk LEFT JOIN res ON res.IdProductoBodega = stk.IdProductoBodega
LEFT JOIN producto_bodega pb ON pb.IdProductoBodega = stk.IdProductoBodega
LEFT JOIN producto p ON p.IdProducto = pb.IdProducto
WHERE stk.tot - ISNULL(res.tot,0) < 0
ORDER BY 6;
```

**Si devuelve 0 filas hoy:** documentar que el reporte negativo del cliente fue puntual y posiblemente recurrente. Cruzar con Receta D para ver si compensaron manualmente.

**Template:** `templates/audit-saldo-neto.py`.

---

## Convención de salida

Toda receta produce CSVs en `outputs/wave-NN/` con prefijo `WNN-XX-descripcion.csv`. Usar `csv_helpers.csv_out(name, rows)` para consistencia.
