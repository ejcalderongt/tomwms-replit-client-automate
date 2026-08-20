# L-051 - PROC: Sincronizacion de ajustes debe ser idempotente entre WMS y SAP

> Etiqueta: `L-051_PROC_AJUSTES_IDEMPOTENCIA_MARK-SAP-NO-REIMPORTAR`
> Fecha: 2026-06-12
> Origen: trazado del flujo `SAPSYNCMAMPA\clsSyncTransacWMS.vb`

## Hallazgo

La sincronizacion de ajustes seguia una secuencia de aplicacion en WMS y marcado posterior en SAP (`TRANSAC_WMS`). Para blindar reintentos, se identifico que el importador debe ser idempotente:

- si el ajuste ya existe en WMS, no debe volver a insertarse;
- si SAP no quedo marcado por un fallo anterior, debe reintentarse el marcado sin duplicar la aplicacion;
- si el ajuste si se inserta correctamente, el marcado en SAP debe seguir siendo parte del flujo exitoso.

## Evidencia

- `SAPSYNCMAMPA\Clases Interface Sync\Transacciones_WMS\clsSyncTransacWMS.vb`
- `TOMIMSV4\DAL\Mantenimientos\Ajustes\clsLnTrans_ajuste_enc_Partial.vb`
- `TOMIMSV4\TOMIMSV4\Transacciones\Ajustes\frmAjusteStock.vb`

## Lo que se toco

1. Se agrego una validacion local por referencia contra `VW_Ajustes_List` para detectar ajustes ya importados en WMS.
2. Si el ajuste ya existe, el flujo omite `Inserta_Stock_Y_Movimiento` y solo reintenta `Marcar_Transac_Wms_Por_DocEntries_SLAsync`.
3. Si el ajuste nuevo se procesa bien, el flujo sigue marcando en SAP al final.
4. Se agregaron tags inline `CKFK260612AjusteIdempotencia` para dejar huella del cambio.

## Implicaciones

- Evita dobles ajustes si el proceso vuelve a leer el mismo documento desde SAP por un fallo previo de marcado.
- El mismo patron deberia replicarse en los otros procesadores de documentos de la interface que terminen en un marcado SAP posterior.

## Confianza

Alta para ajustes; pendiente de replicacion homogenea en devoluciones, anulaciones y ventas.
