---
client_id: merhonsa
id: merhonsa
db_name: IMS4MB_MERHONSA_PRD
holding: idealsa
country: HN
rubro: distribucion mercantil (aceite + detergentes, sucursal)
erp: pendiente-confirmar
authored_by: agente-brain
authored_at: 2026-04-30T16:30:00-06:00
status: PRE-PRODUCTIVA-2026-04-29
fuente_principal: SQL live READ-ONLY 2026-04-28
relacionado_con: [HOLDING_IDEALSA, idealsa, mercopan]
tags: [cliente/merhonsa, holding/idealsa, pais/HN]
---

# Cliente MERHONSA — Sucursal Honduras del holding IDEALSA

> **Mer**cantil **Hon**dureña **S.A.** Sucursal del holding
> [`IDEALSA`](./idealsa.md). BD pre-productiva en EC2 (3 stock, 0
> movimientos, pero tareas HH ya iniciadas al 2026-04-29).

---

## Datos rápidos

| Aspecto | Valor |
|---|---|
| Holding | [IDEALSA](./idealsa.md) |
| País | Honduras |
| BD | `IMS4MB_MERHONSA_PRD` |
| Server EC2 | `52.41.114.122,1437` |
| Schema flavor | `IMS4MB_*` (multi-tenant) |
| Tablas | 319 |
| Vistas | 204 |
| SPs | 37 |
| Stock al 2026-04-29 | 3 filas |
| Movimientos al 2026-04-29 | 0 |
| Tareas HH cambio ubic | 331 enc / 16.429 det |
| Rama BOF probable | `dev_2028_merge` (por timing de creación) |

---

## Particularidades schema (vs MERCOPAN)

Tiene 4 tablas exclusivas no presentes en MERCOPAN:
- `RecuperacionINV` — recuperación de inventario (¿después de
  incidentes/pérdidas?).
- `producto_presentacion_bk` — backup manual de `producto_presentacion`.
- `stock_DESPACHADO` — histórico de stock que ya salió por despacho.
- `tmp_licencia_item` — workspace temporal de un proceso batch.

→ Patrón: **artefactos de recuperación post-error o de migración**
(sufijos `_bk`, `tmp_`, `RecuperacionINV`). Indica que tuvo problemas
de data y se hicieron backups manuales durante la migración.

## Configuración `i_nav_config_enc` (TOP 1)

| Flag | MERHONSA |
|---|---|
| `genera_lp` | **False** |
| `implosion_automatica` | **True** |
| `explosion_automatica` | **True** |
| `Ejecutar_En_Despacho_Automaticamente` | False |
| `generar_recepcion_auto_bodega_destino` | True |

→ **Paradoja LP**: única filial con implosión+explosión automáticas pero
NO genera LPs propios. **Hipótesis**: hereda LPs del ERP corporativo
(probable NAV o SAP del holding) y solo los manipula. Pendiente confirmar.

→ Pregunta abierta: `Q-MERHONSA-PARADOJA-LP` — ¿de dónde vienen los LPs
originales si no los genera? Ver
[`HOLDING_IDEALSA.md`](../agent-context/HOLDING_IDEALSA.md).

---

## Estado pre-productivo

Al 2026-04-29:
- 0 movimientos transaccionales (`trans_movimientos` vacía).
- 3 filas de stock (probablemente seed data de pruebas).
- 331 encabezados / 16.429 detalles de tareas HH cambio ubicación
  → **arranque operativo en curso** (operadores ya están movilizando
  productos en preparación al go-live).

→ Probable go-live MERHONSA: **inminente** (mayo-junio 2026 estimado
por el ritmo de tareas HH).

---

## Cross-refs

- Holding: [`idealsa.md`](./idealsa.md), [`agent-context/HOLDING_IDEALSA.md`](../agent-context/HOLDING_IDEALSA.md)
- Filial hermana: [`mercopan.md`](./mercopan.md)
- Mapeo general: [`RAMAS_Y_CLIENTES.md`](../agent-context/RAMAS_Y_CLIENTES.md)
