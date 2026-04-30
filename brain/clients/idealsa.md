---
client_id: idealsa
id: idealsa
es_holding: true
holding: idealsa
filiales: [mercopan, merhonsa]
implementaciones_directas: [idealsa-escuintla-gt]
country_holding: GT
country_filiales: [PA, HN]
db_propia: null
authored_by: agente-brain
authored_at: 2026-04-30T16:30:00-06:00
status: documentacion-inicial
fuente_principal: confirmacion-verbal-erik-2026-04-30
relacionado_con: [HOLDING_IDEALSA, RAMAS_Y_CLIENTES, mercopan, merhonsa]
tags: [holding/idealsa, cliente/idealsa, pais/GT, pais/PA, pais/HN]
---

# Cliente IDEALSA — Holding Centroamericano (GT, PA, HN)

> **Casa matriz** del grupo corporativo. Sede en **Guatemala**. Opera
> directamente un CD propio en **Escuintla (GT)** y posee dos filiales
> documentadas: **MERCOPAN** (Panamá) y **MERHONSA** (Honduras).

---

## Resumen

| Aspecto | Valor |
|---|---|
| Tipo | Holding corporativo |
| País del holding | Guatemala |
| Filiales conocidas | MERCOPAN (PA), MERHONSA (HN) |
| Operación directa | CD Escuintla, Guatemala |
| **BD propia en EC2** | **No existe todavía** (gap documentado) |
| Patrón corporativo | Schema `IMS4MB_*` replicado en cada filial |

---

## CD Escuintla (Guatemala) — operación directa

- **Implementación**: confirmada por Erik 2026-04-30.
- **BD productiva**: **NO está creada en el EC2 `52.41.114.122,1437`** al
  momento de este documento.
- **Implicación**: la operación del CD GT corre contra alguna BD que aún
  no fue mapeada en el brain. Posibilidades:
  - (a) Está en un EC2 distinto (Erik hospeda más servers).
  - (b) Está embebida en otra BD del grupo IDEALSA con un `propietario_id`
    diferente (multi-tenancy a nivel de fila, no de BD).
  - (c) Aún no migró del sistema previo.

---

## Filiales

### MERCOPAN (Panamá)
- BD: `IMS4MB_MERCOPAN_PRD`
- Estado: productiva (323 K movimientos al 2026-04-29).
- Particularidades: control regulatorio panameño (`stock_jornada_*`),
  rol "cocinero", reglas de notificación a contactos.
- Ver ficha completa: [`mercopan.md`](./mercopan.md)

### MERHONSA (Honduras)
- BD: `IMS4MB_MERHONSA_PRD`
- Estado: pre-productiva (3 stock, 0 movimientos al 2026-04-29, pero
  ya con tareas HH iniciadas).
- Particularidades: implosión + explosión automáticas (único en su
  grupo), pero no genera LPs propios — los hereda del ERP.
- Artefactos de migración: `RecuperacionINV`, `producto_presentacion_bk`,
  `tmp_licencia_item`, `stock_DESPACHADO` (sufijos `_bk`/`tmp_`).
- Ver ficha completa: [`merhonsa.md`](./merhonsa.md)

---

## Patrón holding confirmado

Las dos filiales comparten **315 / ~320 tablas (98 % schema común)** y
fueron creadas en EC2 dentro del mismo minuto (2026-04-28 20:14-20:15).
Se trata de un **template multi-tenant** que el holding replica por
filial, con personalización por país (regulatorio, productos).

Ver análisis completo en
[`agent-context/HOLDING_IDEALSA.md`](../agent-context/HOLDING_IDEALSA.md).

---

## Q-* abiertas para IDEALSA

- `Q-IDEALSA-DB-ESCUINTLA-GT`: ¿dónde corre la BD productiva del CD GT?
  ¿En este EC2 con otro nombre, o en otro server?
- `Q-IDEALSA-MASTER-DATA`: ¿hay sync de productos/propietarios entre
  filiales y matriz?
- `Q-IDEALSA-OTROS-PAISES`: ¿hay más filiales además de PA + HN?
  (Confirmado parcialmente: GT existe como operación directa.)

---

## Cross-refs

- [`HOLDING_IDEALSA.md`](../agent-context/HOLDING_IDEALSA.md) — análisis
  schema-level de las dos filiales.
- [`RAMAS_Y_CLIENTES.md`](../agent-context/RAMAS_Y_CLIENTES.md) — mapeo
  cliente → rama → BD para todo el portafolio.
- [`mercopan.md`](./mercopan.md), [`merhonsa.md`](./merhonsa.md) —
  fichas de filiales.
