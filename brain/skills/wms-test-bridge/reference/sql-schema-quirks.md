# Quirks del schema SQL Server (deuda historica)

## 1. Typo en columna de explosion automatica — RESUELTO

La tabla `i_nav_config_enc` tiene **dos columnas casi-iguales**:

| Columna | Estado |
|---|---|
| `explosion_automatica_nivel_max` | **FUENTE DE VERDAD** (con la `n`). El motor lee esta. |
| `explosio_automatica_nivel_max` | **DEPRECADA**. Sin la `n`. Resultado de un `ALTER TABLE` historico mal ejecutado. NO USAR. |

En Killios prod tienen valores **divergentes**: typo=`1`, correcto=`-1` (sin limite).

**Cero referencias en SPs/vistas SQL** para ambas columnas — el motor las lee desde codigo .NET/VB. La fuente de verdad esta en codigo, no se puede verificar contra SQL.

**Pendiente**: cuando se haga un release del motor, dropear `explosio_automatica_nivel_max`. Ver ADR-010.

## 2. VW_Configuracioninv NO contiene los flags

La vista `VW_Configuracioninv` solo expone 8 columnas (metadatos). Los 69 flags reales viven en `i_nav_config_enc`. Cualquier herramienta de aprendizaje **debe** apuntar a la tabla.

## 3. Bodega de facturacion virtual BOD7

Killios tiene `bodega_facturacion = "BOD7"` en las 6 bodegas operativas, **pero `BOD7` no existe en la tabla `bodega`**:

```sql
-- bodega:
1 | BOD1 | Bodega Principal
2 | PRTOK | Bodega de Prorateo Kilio
3 | PRTO  | Bodega de Prorateo Garesa
4 | BOD5  | Bodega Amatitlan
5 | PRTK17 | Bodega de Prorateo Kilio Z17
6 | PRT17  | Bodega de Prorateo Garesa Z17
-- (no hay BOD7)

-- pero:
SELECT bodega_facturacion FROM i_nav_config_enc WHERE idEmpresa=1
-- => "BOD7" en TODAS las filas
```

**Hipotesis confirmada por Erik**: desfase entre la data que vimos al cargar Amazon vs la realidad (en otras vistas/sistemas BOD7 sí aparece). Se trata como bodega de facturacion REAL en el modelo.

**Accion**: incluir BOD7 en `brain/clients/killios.md` como bodega virtual de tipo "facturacion".

## 4. Pares de bodega de prorrateo

`bodega_prorrateo` y `bodega_prorrateo1` se usan **cruzados** entre Amatitlan y Z17:

| Bodega | bodega_prorrateo | bodega_prorrateo1 |
|---|---|---|
| BOD1 (principal Amatitlan) | PRT17 | PRTK17 |
| PRTOK (prorrateo Kilio) | PRTO | PRTOK |

Modela el flujo: **compra → prorrateo (costeo SAP) → traslado a operativa**, con dos entidades legales (Kilio/Garesa) compartiendo bodegas fisicas.

## 5. Snapshots manuales en producción

En Killios PRD existen tablas backup historicas en el schema:
- `stock_res_20250624` (snapshot del 2025-06-24)
- `stock_res_ped_164` (snapshot del pedido 164)

Son backups manuales que un dev hizo para investigar incidentes. Indican **deuda de proceso**: deberia haber un mecanismo regulado de snapshot/restore en lugar de tablas paralelas dejadas en prod.

## 6. trans_pe_det_log_reserva — fuente de verdad operativa

El motor escribe cada decision de reserva en `trans_pe_det_log_reserva` con:

- `Caso_Reserva` (nvarchar/50): identificador del caso ejecutado, ej. `CASO_#24_EJC202310090957`. Sufijo `_EJCxxx` = marca de la version del motor.
- `MensajeLog` (nvarchar/MAX): texto largo con `Fecha Mínima`, `DiasVencimiento`, `FechaMinimaVenceZonaPicking` y demas variables internas del calculo.
- `EsError` (bit): true si la decision fue un error.

Este es el lugar de auditoria. Tambien sirve como **fuente de verdad observada**: los `Caso_Reserva` que NUNCA aparecen en el log de produccion son escenarios teoricos que el motor jamas ejecuto.

## 7. Whitelist de prefijos SQL en assert_read_only

`brain/wms-agent/wmsa/killios.py` permite: `SELECT, WITH, EXEC, EXECUTE, SET, DECLARE, PRINT`. Bloquea `INSERT, UPDATE, DELETE, DROP, TRUNCATE, ALTER, CREATE` y patrones `EXEC sp_executesql` con texto mutante.
