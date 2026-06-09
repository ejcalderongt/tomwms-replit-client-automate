# EJC20260605 - Mejora estructural: FK barras impresas -> lote OC (MHS)

## Propuesta
Sí: para robustecer trazabilidad, se debe persistir el vínculo directo:

- `i_nav_barras_pallet.IdOrdenCompraDetLote` -> `trans_oc_det_lote.IdOrdenCompraDetLote` (FK)

Esto elimina dependencia de matching por texto (`lote`) y evita ambigüedad.

## Script listo
- [EJC20260605_MHS_FK_BARRA_PALLET_LOTE.sql](C:\Users\yejc2\source\repos\TOMWMS\TOMIMSV4\TOMIMSV4\Docs\EJC20260605_MHS_FK_BARRA_PALLET_LOTE.sql)

Incluye:
1. `ALTER TABLE` para nueva columna nullable.
2. Backfill por `(IdOrdenCompraEnc, IdOrdenCompraDet, lote normalizado, fecha_vence)`.
3. Índices recomendados.
4. FK con `WITH CHECK`.
5. Queries de validación/huérfanos.

## Dónde aterrizar en código (DAL/BOF)

### 1) Entidad
Archivo: `Entity/Interface/Barras_Pallet/clsBeI_nav_barras_pallet.vb`
- Agregar propiedad:
`Public Property IdOrdenCompraDetLote As Integer = 0`

### 2) Mapeo DAL i_nav_barras_pallet
Archivo: `DAL/Interface/Barras_Pallet/clsLnI_nav_barras_pallet.vb`
- En `Cargar(...)`: mapear columna `IdOrdenCompraDetLote`.
- En `Insertar(...)`: incluir `idordencompradetlote`.
- En `Actualizar(...)`: incluir `idordencompradetlote`.

### 3) Flujo impresión OC
Archivo: `Mantenimientos/Impresion_OC/frmImpresion_OC.vb`
- Cuando se selecciona `cmbLote`, tomar `IdLote` (`IdOrdenCompraDetLote`).
- Al crear/persistir `BeInavBarraPallet`, setear `IdOrdenCompraDetLote = IdLote`.

## Resultado esperado
- Cada barra/licencia impresa queda ligada a su lote exacto (FK).
- El grid de lotes puede calcular estado de impresión sin inferencias frágiles.
- Menor riesgo de inconsistencias por diferencias en texto de lote.

## Observación operativa
Mantener `IdOrdenCompraDetLote` nullable durante transición para no romper históricos.  
Luego de estabilizar, se puede evaluar volverlo `NOT NULL` para flujos MHS que siempre nacen de lote.

