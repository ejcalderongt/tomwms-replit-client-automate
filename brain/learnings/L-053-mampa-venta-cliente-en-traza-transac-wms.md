# L-053 - Cliente en la traza de ventas TRANSAC_WMS

## Contexto
- En `SAPSYNCMAMPA`, las ventas importadas desde `TRANSAC_WMS` reutilizan el modelo de traslado.
- Por esa reutilización, el contexto técnico conserva los nombres genéricos `BodegaOrigen` y `BodegaDestino`.

## Regla
- Para el proceso `VENTA`, `U_Transfer_from_Code` representa la bodega origen.
- Para el proceso `VENTA`, `U_Transfer_to_Code` representa el cliente del pedido, no una bodega destino.
- Los mensajes humanos deben mostrar este segundo valor como `Cliente`.

## Alcance
- Es una corrección de presentación y trazabilidad.
- No cambia el mapeo, la validación del cliente, la reserva, el picking ni la persistencia del pedido.

## Implementación
- Archivo: `SAPSYNCMAMPA/Clases Interface Sync/Transacciones_WMS/clsSyncTransacWMS.vb`.
- Método: `ConstruirHumanErrorTransacWms`.
- Tag: `CKFK260903VentaClienteLog`.
