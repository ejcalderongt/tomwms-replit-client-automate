---
id: README
tipo: client
estado: vigente
titulo: Catalogo de clientes
ramas: [dev_2028_merge]
tags: [client]
---

# Catalogo de clientes

## BDs productivas

| Cliente | DB | Holding | País | ERP | Bodegas | Modo reserva | Modulo distintivo |
|---|---|---|---|---|---|---|---|
| KILLIOS | TOMWMS_KILLIOS_PRD | — | GT | SAP B1 (DI-API) | 6 oper + 1 virtual (BOD7) | **estricto** | Conversion cajas/decimales SAP |
| KILLIOS_2026 | TOMWMS_KILLIOS_PRD_2026 | — | GT | SAP B1 (DI-API) | (BD nueva 2026) | estricto | (variante 2026 — pendiente caracterizar) |
| BYB | IMS4MB_BYB_PRD | — | GT | NAV Dynamics | 2 | estricto | Reabastecimiento de picking activo |
| CEALSA | IMS4MB_CEALSA_QAS | — | GT | (sin ERP integrado) | 2 (1 general + 1 fiscal) | **discrecional** (3PL, no se invoca por defecto) | Stock jornada + prefacturacion + polizas |
| **MERCOPAN** | IMS4MB_MERCOPAN_PRD | [IDEALSA](./idealsa.md) | PA | pendiente | (pendiente) | (pendiente) | Stock cocinero + jornada consecutiva PA |
| **MERHONSA** | IMS4MB_MERHONSA_PRD | [IDEALSA](./idealsa.md) | HN | pendiente | (pendiente) | (pendiente) | Implosion + explosion auto, hereda LPs del ERP |
| **IDEALSA** | (sin BD propia confirmada) | (es el holding) | GT (Escuintla CD) | pendiente | (pendiente) | (pendiente) | **Holding** dueño de MERCOPAN+MERHONSA. CD propio en Escuintla. |
| MAMPA | TOMWMS_MAMPA_QA | — | GT | pendiente | (pendiente) | (pendiente) | Primer cliente migrado a `dev_2028_merge` |

## BDs diagnosticas (snapshots para entrenamiento del agente)

| Cliente | DB | Origen | Estado | Comentario |
|---|---|---|---|---|
| **BECOFARMA** | **IMS4MB_BECOFARMA_PRD** | Restaurada 28-abr-2026 desde productiva | **NO-PRODUCTIVA, snapshot diagnostico** | Sin SAPBOSync corriendo contra esta copia. Hechos de schema/modulos/catalogos son validos; metricas de salud operativa NO. Ver L-014. |

Server compartido EC2: `52.41.114.122,1437` (SQL Server, mixed auth).

## BDs en EC2 fuera del contexto WMS

El mismo server EC2 hospeda otras BDs **ajenas al WMS** (de la otra línea
de productos POS/Road de la empresa). Erik confirmó 2026-04-30 que NO
deben usarse como referencia para análisis del WMS:

- `LIVE` — fuera del scope WMS.
- `mpos_pollo_express_qa` — sistema POS/mPos.
- `POD_BETA` — Proof-of-Delivery beta (probablemente línea Road/POD).

→ Para queries READ-ONLY del agente, **filtrar siempre por las BDs de
la tabla anterior**. Nunca consultar las de esta sección.

## Modo de reserva

- **estricto**: `rechazar_pedido_incompleto=1`. El motor reserva el TOTAL o aborta y avisa al ERP. NO hay reserva parcial.
- **discrecional (3PL)**: el motor NO se invoca por defecto. Solo si el tipo de pedido tiene `trans_pe_tipo.ReservaStock=true` Y el escenario lo solicita. De lo contrario, el operador elige stock bajo peticion del cliente.

## Politica de conexion

- Killios y BYB son **PRODUCTIVAS**. CEALSA es QAS pero con datos reales del cliente. BECOFARMA es **diagnostica** (snapshot).
- TODA la operacion del agente es `SELECT`/`EXEC` de SPs de lectura. Cero modificaciones en cualquier BD (incluida BECOFARMA: la copia se podria refrescar y cualquier edit local se perderia).
- Whitelist de prefijos en `brain/wms-agent/wmsa/killios.py`: `SELECT, WITH, EXEC, EXECUTE, SET, DECLARE, PRINT`.

## Principio operativo

**Afinidad de procesos confirmable, afinidad de datos diferida**:

- Lo que SI se puede inferir de cualquier BD (productiva o diagnostica): schema, modulos exclusivos, naming, catalogos, relaciones, configuracion (`i_nav_config_enc`).
- Lo que SOLO se puede inferir de BDs productivas vivas: KPIs operativos (% i_nav_transacciones_out (outbox) pendiente, throughput, latencia, errores activos).
- BECOFARMA aqui sirve para el primer grupo, NO para el segundo.

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
- `learnings/L-014` — BECOFARMA es BD diagnostica, no productiva.
- `learnings/L-015` — modelo ClickOnce + dispatch dinamico via nombre_ejecutable.
- `way-of-thinking.md` — principios operativos y nota del autor.
