# Telemetry Schema

`#EJC20260527` Minimum event envelope:

- `trace_id`
- `process`
- `layer` (`HH|BOF|WS|DAL|SQL`)
- `symbol`
- `entity_ids` (`IdPickingEnc`, `IdPedidoEnc`, `IdStockRes`, `IdMovimiento`)
- `state_before`, `state_after`
- `result` (`ok|warn|error`)
- `timestamp`

Keep payloads short and PII-safe.

