# Catalogo de clientes

| Cliente | DB | ERP | Bodegas | Modo reserva | Modulo distintivo |
|---|---|---|---|---|---|
| KILLIOS | TOMWMS_KILLIOS_PRD | SAP B1 (DI-API) | 6 oper + 1 virtual (BOD7) | **estricto** | Conversion cajas/decimales SAP |
| BYB | IMS4MB_BYB_PRD | NAV Dynamics | 2 | estricto | Reabastecimiento de picking activo |
| CEALSA | IMS4MB_CEALSA_QAS | (sin ERP integrado) | 2 (1 general + 1 fiscal) | **discrecional** (3PL, no se invoca por defecto) | Stock jornada + prefacturacion + polizas |
| **BECOFARMA** | **IMS4MB_BECOFARMA_PRD** | **SAP B1 (DI-API, `SAPBOSync.exe`)** | **1 (GENERAL)** | (a confirmar — control_lote+vencimiento on) | **Verificacion etiquetas + reglas vencimiento + logging segmentado + 98% PIK** |

Server compartido EC2: `52.41.114.122,1437` (SQL Server, mixed auth).

## Modo de reserva

- **estricto**: `rechazar_pedido_incompleto=1`. El motor reserva el TOTAL o aborta y avisa al ERP. NO hay reserva parcial.
- **discrecional (3PL)**: el motor NO se invoca por defecto. Solo si el tipo de pedido tiene `trans_pe_tipo.ReservaStock=true` Y el escenario lo solicita. De lo contrario, el operador elige stock bajo peticion del cliente.

## Politica de conexion

- Killios, BYB y BECOFARMA son **PRODUCTIVAS**. CEALSA es QAS pero con datos reales del cliente.
- TODA la operacion del agente es `SELECT`/`EXEC` de SPs de lectura. Cero modificaciones.
- Whitelist de prefijos en `brain/wms-agent/wmsa/killios.py`: `SELECT, WITH, EXEC, EXECUTE, SET, DECLARE, PRINT`.

## Aparicion del cliente BECOFARMA (28-abr-2026)

La BD `IMS4MB_BECOFARMA_PRD` aparecio en el server hoy 28-abr-2026 a las 08:32. Sin embargo, su `i_nav_config_enc.fec_agr` es de **2017-09-11**, lo que indica que es una **migracion/restore de una instancia previa**, no un cliente nuevo. Detalles completos:

- `clients/becofarma.md` — perfil cliente.
- `wms-specific-process-flow/becofarma-mapping.md` — mapeo profundo (351 lineas).

## Documentacion relacionada

- `reference/config-flags.md` — los 69 flags de `i_nav_config_enc` con interpretacion firme.
- `reference/reserva-tables.md` — tablas del flujo de reserva (`stock_res`, `trans_pe_*`, `i_nav_ped_traslado_*`, log `trans_pe_det_log_reserva`).
- `reference/casos-reserva-observados.md` — `Caso_Reserva` reales del log productivo (Killios solo 9 casos activos).
- `reference/sql-schema-quirks.md` — typo `explosio_*` deprecada, BOD7 virtual, snapshots manuales.
- `reference/multi-env-config.md` — schema TOML para credenciales multi-ambiente.
- `adr/ADR-006` — multi-env config.
- `adr/ADR-007` — Killios+SAP B1+decimales.
- `adr/ADR-008` — BYB modulo reabasto.
- `adr/ADR-009` — CEALSA 3PL jornada/prefactura.
- `adr/ADR-010` — terminologia reserva-webapi vs reserva-WMS legacy.
- `way-of-thinking.md` — principios operativos y nota del autor.
