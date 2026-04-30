---
client_id: mercopan
id: mercopan
db_name: IMS4MB_MERCOPAN_PRD
holding: idealsa
country: PA
rubro: distribucion mercantil (aceite + detergentes)
erp: pendiente-confirmar
authored_by: agente-brain
authored_at: 2026-04-30T16:30:00-06:00
status: PRODUCTIVA-CONFIRMADA-2026-04-29
fuente_principal: SQL live READ-ONLY 2026-04-28
relacionado_con: [HOLDING_IDEALSA, idealsa, merhonsa]
tags: [cliente/mercopan, holding/idealsa, pais/PA]
---

# Cliente MERCOPAN — Filial Panamá del holding IDEALSA

> **Mer**cantil **Co**mercial **Pan**amá. Filial del holding
> [`IDEALSA`](./idealsa.md). BD productiva en EC2 con 323 K movimientos
> al 2026-04-29.

---

## Datos rápidos

| Aspecto | Valor |
|---|---|
| Holding | [IDEALSA](./idealsa.md) |
| País | Panamá |
| BD | `IMS4MB_MERCOPAN_PRD` |
| Server EC2 | `52.41.114.122,1437` |
| Schema flavor | `IMS4MB_*` (multi-tenant) |
| Tablas | 322 |
| Vistas | 194 |
| SPs | 38 |
| Stock al 2026-04-29 | 2.724 filas |
| Movimientos al 2026-04-29 | 323.374 filas |
| Tareas HH cambio ubic | 737 enc / 3.840 det |
| Rama BOF probable | `dev_2028_merge` (por timing de creación) |

---

## Particularidades schema (vs MERHONSA y resto del portafolio)

Tiene 7 tablas exclusivas que no están en su filial hermana MERHONSA:
- `StockCocinero`, `stock_BK_Cocinero` — **rol "cocinero"** (¿producción
  de comidas en almacén? mezclas pre-armadas?)
- `reglas_vencimiento_contacto` — notificación al contacto comercial
  cuando un lote vence.
- `rol_usuario_estado` — multi-rol con habilitación granular.
- `stock_jornada_consecutivo`, `stock_jornada_desfase`,
  `stock_jornada_fecha_consecutiva` — **regulatorio panameño** (control
  de jornada consecutiva, similar a MAMPA pero distinto).

## Configuración `i_nav_config_enc` (TOP 1)

| Flag | MERCOPAN |
|---|---|
| `genera_lp` | **True** |
| `implosion_automatica` | False |
| `explosion_automatica` | False |
| `Ejecutar_En_Despacho_Automaticamente` | False |
| `generar_recepcion_auto_bodega_destino` | False |

→ Patrón "moderno" (genera LPs propios, sin auto-implosión/explosión).

---

## Vinculación con BUG-001 (dañado_picking)

MERCOPAN aparece en el listado de BDs afectadas por `BUG-001` (junto a
KILLIOS y BYB). Confirmado en wave 19+21 cross-cliente. Volumen exacto
de líneas con `dañado_picking=1` sin AJCANTN está en
[`BUG-001/CASOS-RELACIONADOS.md`](../wms-known-issues/BUG-001-danado-picking-no-resta-inventario/CASOS-RELACIONADOS.md).

---

## Cross-refs

- Holding: [`idealsa.md`](./idealsa.md), [`agent-context/HOLDING_IDEALSA.md`](../agent-context/HOLDING_IDEALSA.md)
- Filial hermana: [`merhonsa.md`](./merhonsa.md)
- Bug del producto que afecta: [`BUG-001`](../wms-known-issues/BUG-001-danado-picking-no-resta-inventario/INDEX.md)
- Mapeo general: [`RAMAS_Y_CLIENTES.md`](../agent-context/RAMAS_Y_CLIENTES.md)
