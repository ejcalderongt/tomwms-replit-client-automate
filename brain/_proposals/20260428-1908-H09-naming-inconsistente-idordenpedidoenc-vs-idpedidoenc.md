---
id: 20260428-1908-H09-naming-inconsistente-idordenpedidoenc-vs-idpedidoenc
tipo: proposal
estado: vigente
titulo: 20260428-1908-H09 - trans_pe_pol usa IdOrdenPedidoEnc como FK, pero apunta al IdPedidoEnc de trans_pe_enc (mismo valor).
tags: [proposal]
---

# 20260428-1908-H09 - trans_pe_pol usa IdOrdenPedidoEnc como FK, pero apunta al IdPedidoEnc de trans_pe_enc (mismo valor).

> Generado por agente brain (sesion replit) el 28 abril 2026 tras cierre Ciclo 8a via ejecucion live SQL.

## Contexto

Hallazgo durante Q-013 (28-abr-2026): la PK de trans_pe_enc es IdPedidoEnc, pero la FK en trans_pe_pol se llama IdOrdenPedidoEnc. Verificacion empirica en C9-QAS: SELECT pp.IdOrdenPedidoEnc, pe.IdPedidoEnc devuelve valores identicos (183=183, 184=184, etc). Es un alias de naming - misma columna logica, dos nombres distintos. Sugiere: (a) refactor historico parcial donde el modelo era 'orden_pedido' y se renombro a 'pedido' pero la tabla de polizas quedo sin migrar; (b) RIESGO: cualquier ORM/codegen que infiera relaciones por nombre va a fallar - hay que mapear manualmente. Para nueva WebAPI: documentar este alias en el schema-canon, definir property name unica (probablemente IdPedidoEnc) y mapear via attribute en EF Core.

## Modulos tocados

- `trans_pe_pol`
- `trans_pe_enc`
- `schema-canon`
- `naming-convention`
- `ef-core-mapping`

## Decision provisional

`accepted_documentar_alias_validated_via_live_sql`

## Ratificacion pendiente de

Erik Calderon (PrograX24)

## Cross-references

- Inbox event: `brain/_inbox/20260428-1908-H09-naming-inconsistente-idordenpedidoenc-vs-idpedidoenc.json`
- Tags: `validated-via-live-sql`, `schema-quirk`, `naming-debt`, `tech-debt`, `cealsa`, `q-013`
- Preguntas origen: `Q-013`
- Respondedoras: `SQL-agente`, `schema-validator`
- Ciclo: 8a
- Doc consolidacion: `brain/wms-specific-process-flow/consolidacion-ciclo-8a.md`
