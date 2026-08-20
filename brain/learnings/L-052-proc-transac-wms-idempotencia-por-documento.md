# L-052 - Idempotencia por documento en TRANSAC_WMS

## Contexto
- La interface `SAPSYNCMAMPA` sincroniza varios flujos desde SAP a WMS.
- Si el marcado en SAP falla luego de importar, el reintento no debe duplicar el documento.

## Regla
- Antes de insertar, validar si el documento ya existe en WMS.
- Si ya existe, no reinserta.
- Solo reintenta el marcado en SAP con los `DocEntries` asociados.

## Llaves por flujo
- Ajustes: `Referencia` + ventana por fecha sobre `vw_ajustes`.
- Ventas: `Referencia` del pedido.
- Devoluciones / anulaciones sobre OC: `Referencia` + `IdTipoIngresoOC` + `IdBodega`.

## Traza
- Mantener traza técnica interna con:
  - proceso
  - documento
  - referencia
  - docentries
  - causa
  - mensaje técnico
- Mostrar en bitácora un mensaje humano breve para el usuario.

## Implementación
- Archivo principal: `SAPSYNCMAMPA\Clases Interface Sync\Transacciones_WMS\clsSyncTransacWMS.vb`
- Se agregaron helpers `PedidoYaExisteEnWMS(...)`, `OrdenCompraYaExisteEnWMS(...)` y `AjusteYaExisteEnWMS(...)`.
- Se aplicó el patrón en ventas, devoluciones y anulaciones, además de ajustes.
