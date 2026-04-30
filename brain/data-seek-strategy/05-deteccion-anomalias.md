# 05 — Detección de anomalías

Anomalías típicas que aparecen en BDs WMS antiguas y cómo cazarlas. Cada sección incluye query base y referencia al template ejecutable.

---

## A. Overflows en cantidades

**Qué buscamos:** valores que rompen el orden de magnitud razonable de la operación. Un cliente que mueve <50,000 UM/mes no debería tener movimientos individuales >100,000 UM. Si los hay, son outliers (error de digitación, test mal hecho, corrupción).

**Estrategia:**
1. Calcular percentil 99 y media de la columna.
2. Listar rows con valor > N × percentil 99 o > umbral absoluto duro (ej: 1,000,000 UM).
3. Inspeccionar manualmente: ¿es real o corrupto?

**Query genérica:**
```sql
WITH stats AS (
  SELECT AVG(<col>*1.0) avg_v, MAX(<col>) max_v,
         PERCENTILE_CONT(0.99) WITHIN GROUP (ORDER BY <col>) OVER () p99
  FROM <tabla> WHERE <col> > 0
)
SELECT TOP 50 * FROM <tabla>
WHERE <col> > 1000000 OR <col> > (SELECT TOP 1 p99 FROM stats) * 100
ORDER BY <col> DESC;
```

**Caso real:** CP-013 detectó `trans_ajuste_det.idajustedet=638` con 4.8 billones UM, observación "sa", del 2021. Distorsionaba todos los agregados.

**Template:** `templates/audit-overflow.py`.

---

## B. Gaps temporales de actividad

**Qué buscamos:** períodos donde una tabla esperable de tener actividad regular tiene cero rows. Indica cambio de proceso, migración, o problema oculto.

**Estrategia:**
1. Agrupar por mes la tabla.
2. Listar todos los meses entre min(fecha) y max(fecha).
3. Marcar los meses con cero actividad.

**Query genérica:**
```sql
WITH meses AS (
  SELECT DATEFROMPARTS(YEAR(MIN(<fecha>)), MONTH(MIN(<fecha>)), 1) AS m,
         DATEFROMPARTS(YEAR(MAX(<fecha>)), MONTH(MAX(<fecha>)), 1) AS mmax
  FROM <tabla>
), serie AS (
  SELECT m FROM meses
  UNION ALL
  SELECT DATEADD(MONTH, 1, m) FROM serie, meses WHERE DATEADD(MONTH,1,m) <= meses.mmax
)
SELECT s.m, COUNT(t.<pk>) n
FROM serie s LEFT JOIN <tabla> t ON FORMAT(t.<fecha>,'yyyy-MM') = FORMAT(s.m,'yyyy-MM')
GROUP BY s.m
HAVING COUNT(t.<pk>) = 0
ORDER BY s.m
OPTION (MAXRECURSION 1000);
```

**Caso real:** CP-013 detectó gap 2023-04 a 2025-11 (2.5 años sin ajustes manuales) que sugiere cambio de proceso o migración no documentada.

**Template:** `templates/audit-gaps-temporales.py`.

---

## C. Saldo neto negativo (stock vs reservas)

**Qué buscamos:** productos cuyas reservas vivas suman más que el stock real. Si aparece, el cliente puede vender algo que no podrá despachar.

Ver Receta E en `03-flujos-investigacion.md`. Template: `audit-saldo-neto.py`.

---

## D. Fechas inválidas

**Qué buscamos:** rows con fecha < 2010 o > año actual + 5 (defaults SQL Server, errores de digitación, corrupción).

```sql
SELECT TOP 100 <pk>, <fecha>
FROM <tabla>
WHERE <fecha> < '2010-01-01' OR <fecha> > DATEADD(YEAR, 5, GETDATE())
ORDER BY <fecha>;
```

---

## E. Stock activo en ubicaciones inactivas

**Qué buscamos:** stock con `activo=1` pero `ubicacion.activo=0`. Inconsistencia.

```sql
SELECT s.IdStock, s.IdProductoBodega, s.lote, s.cantidad, u.nombre, u.activo
FROM stock s WITH (NOLOCK)
JOIN ubicacion u ON u.IdUbicacion = s.IdUbicacion
WHERE s.activo = 1 AND u.activo = 0;
```

---

## F. Reservas zombi

**Qué buscamos:** reservas en `stock_res` cuyo `stock` está inactivo.

```sql
SELECT sr.IdStock, sr.cantidad, sr.IdTransaccion, sr.estado, sr.Indicador, s.activo
FROM stock_res sr
JOIN stock s ON s.IdStock = sr.IdStock
WHERE s.activo = 0;
```

Si hay muchas, el flujo de liberación de reservas tiene bug.

---

## G. Pickings dañados sin AJCANTN compensatorio (bug CP-013)

Ver Receta B en `03-flujos-investigacion.md`. Template: `audit-danados-sin-ajuste.py`.

---

## H. CESTs sin operador HH

**Qué buscamos:** cambios de estado masivos hechos desde BOF (operador 0). Pueden indicar que el equipo está usando el BOF para limpiar problemas que deberían resolverse en flujo HH normal.

```sql
SELECT user_agr, COUNT(*) n
FROM trans_movimientos
WHERE IdTipoTarea = 3 -- CEST
  AND ISNULL(IdOperadorBodega_Origen, 0) = 0
GROUP BY user_agr ORDER BY n DESC;
```

---

## I. Productos con velocidad inconsistente

**Qué buscamos:** un producto que recibe 100 UM/día y de pronto pasa 30 días sin movimiento. Puede indicar que el sistema dejó de descontar o stock se quedó "pegado".

(Pendiente template; receta exploratoria por ahora.)

---

## Reglas generales

1. **Toda detección de anomalía produce un CSV** que se guarda en `outputs/wave-NN/`.
2. **Toda anomalía descartada se documenta** en el reporte como "outlier descartado: id=X razón=Y".
3. **Si el patrón se repite en N clientes**, agregarlo como anti-patrón en `04-anti-patrones-y-trampas.md`.
