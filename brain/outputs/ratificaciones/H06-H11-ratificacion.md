---
output_type: ratificacion-bulk
audience: Erik Calderon (PrograX24)
ciclo_origen: 8a
hallazgos: [H06, H07, H08, H09, H10, H11]
version: V1
status: pendiente-decision
authored_by: agente-brain
authored_at: 2026-04-28T22:30:00-03:00
proposito: |
  Bloque A del recordatorio: ratificar formalmente los 6 hallazgos producidos
  durante el Ciclo 8a (validados via SQL live READ-ONLY contra K7-PRD, BB-PRD, C9-QAS).
  Cada card pide UNA decision: Aceptar / Rechazar / Diferir.
  Los aceptados se promueven a ADR formales (ADR-013 en adelante).
ratificacion_pendiente_de: Erik Calderon (PrograX24)
---

# Ratificacion de hallazgos del Ciclo 8a (H06-H11)

> **Como usar este documento**: para cada card, marcas en el campo "Decision" una de tres opciones:
> - **A** (Aceptar) - el hallazgo se promueve a ADR formal con numero asignado.
> - **R** (Rechazar) - se descarta. Argumentar por que en "Notas".
> - **D** (Diferir) - se vuelve a evaluar en un Ciclo futuro. Indicar trigger en "Notas" (ej: "cuando tengamos backups sincronizados").
>
> Tambien podes anotar comentarios libres por hallazgo. Una vez completas las 6 decisiones, te aplico la promocion en bloque (rename de propuestas a ADR, actualizacion de status, cross-references).

---

## H06 - Bypass estado=Despachado sin trans_despacho_det existe como camino tecnico

| Campo | Valor |
|---|---|
| **Hallazgo** | En K7-PRD se encontro 1 pedido (2025-06) con estado='Despachado' y 0 filas en `trans_despacho_det`. El proceso de bypass es **tecnicamente posible** sin validacion server-side bloqueante. |
| **Tipo** | Validacion de proceso (afinidad de procesos) |
| **Validacion** | SQL live READ-ONLY contra TOMWMS_KILLIOS_PRD (28-abr-2026, snapshot del EC2) |
| **Frecuencia observada** | 1 caso en snapshot (NO comparable con la frecuencia que reportan personas del cliente: ellos trabajan sobre backups mas recientes) |
| **Accion concreta** | **Recalibrar texto de ADR-012**: separar "el proceso existe" (validado) de "la frecuencia es de N casos" (diferido a afinidad de datos). |
| **Riesgo si no se ratifica** | ADR-012 mantiene un numero (43) que ya no corresponde al snapshot de hoy. Otros documentos pueden seguir citando un numero obsoleto. |
| **ADR sugerido si se acepta** | ADR-013-bypass-despachado-proceso-validado (complementa, no reemplaza, ADR-012) |
| **Decision** | ☐ Aceptar  ☐ Rechazar  ☐ Diferir |
| **Notas** | _(libre)_ |

---

## H07 - Becofarma tiene perfil operativo putaway-heavy (50% UBIC) vs Killios outbound-heavy (71% PIK)

| Campo | Valor |
|---|---|
| **Hallazgo** | Distribucion de tareas HH muy distinta entre clientes: K7-PRD = 71% PIK + 28% RECE; BB-PRD = 50% UBIC + 31% RECE + 19% PIK; C9-QAS = 74% PIK + 26% RECE. Las 3 BDs comparten el catalogo `sis_tipo_tarea`. |
| **Tipo** | Hallazgo de perfil operativo (afinidad de procesos confirmada en datos) |
| **Validacion** | Q-014 ejecutada contra las 3 BDs (28-abr-2026) |
| **Implicaciones** | (a) KPIs de productividad por cliente NO pueden ser uniformes - hay que parametrizar peso de tareas. (b) El modulo UBIC merece prioridad de optimizacion en BB y baja en K7. (c) Tipos como TRASL/REUB/CEST suman <1% en las 3 BDs. |
| **Accion concreta** | (1) WebAPI nueva expone endpoint "tipos activos" filtrado por cliente (no devolver tipos con <1%). (2) Documentar el perfil operativo de cada cliente en `brain/clients/<cliente>.md`. |
| **ADR sugerido si se acepta** | ADR-014-perfiles-operativos-por-cliente |
| **Decision** | ☐ Aceptar  ☐ Rechazar  ☐ Diferir |
| **Notas** | _(libre)_ |

---

## H08 - i_nav_transacciones_out (outbox) NO emite eventos de "pedido creado/modificado" (solo de despacho que arrastra IdPedidoEnc)

| Campo | Valor |
|---|---|
| **Hallazgo** | En K7-PRD y BB-PRD, el 100% de filas del i_nav_transacciones_out que tienen `IdPedidoEnc` tambien tienen `IdDespachoEnc` (con_pedido = con_despacho exactos). El i_nav_transacciones_out NO emite eventos de "pedido creado/modificado" - solo de despacho. |
| **Tipo** | Hallazgo de semantica del i_nav_transacciones_out (afinidad de procesos validada) |
| **Validacion** | Q-009 ejecutada contra las 3 BDs (28-abr-2026). C9-QAS tiene 0 filas (entorno sin trafico). |
| **Implicaciones** | (a) Navigator NO ve pedidos hasta que estan despachados. (b) El bridge se simplifica: 4 tipos colapsa a 2 efectivos (recepcion + despacho con FKs adicionales: `IdPedidoEnc` y `IdOcEnc` viajan de polizon). (c) Confirma parcialmente lo que dijo Carol en P-19. |
| **Accion concreta** | Diseniar el bridge brain<->WMS (.NET 8) con 2 tipos de evento principales (RECEPCION, DESPACHO) y FKs opcionales (`IdPedidoEnc`, `IdOcEnc`, `IdProveedor`). Eliminar el plumbing de los otros 2 tipos no usados. |
| **ADR sugerido si se acepta** | ADR-015-bridge-i_nav_transacciones_out-2-tipos-efectivos |
| **Decision** | ☐ Aceptar  ☐ Rechazar  ☐ Diferir |
| **Notas** | _(libre)_ |

---

## H09 - trans_pe_pol.IdOrdenPedidoEnc es un alias historico de trans_pe_enc.IdPedidoEnc

| Campo | Valor |
|---|---|
| **Hallazgo** | La PK de `trans_pe_enc` es `IdPedidoEnc`, pero la FK en `trans_pe_pol` se llama `IdOrdenPedidoEnc`. Verificacion empirica en C9-QAS: los valores son identicos (183=183, 184=184). Es un alias historico de un refactor parcial: `orden_pedido` -> `pedido`, donde la tabla de polizas no se migro. |
| **Tipo** | Tech debt de schema (naming inconsistente) |
| **Validacion** | Q-013 + verificacion JOIN en IMS4MB_CEALSA_QAS (28-abr-2026) |
| **Riesgo** | Cualquier ORM/codegen que infiera relaciones por nombre va a fallar. Mapeo manual obligatorio en EF Core. |
| **Accion concreta** | (1) Documentar el alias en `brain/sql-catalog/` (crear archivo `naming-aliases.md` o agregar a uno existente). (2) En la WebAPI .NET 10, mapear via attribute en EF Core: la entidad usa `IdPedidoEnc` como property, con `[Column("IdOrdenPedidoEnc")]` cuando se usa contra `trans_pe_pol`. (3) NO renombrar la columna en SQL (alto impacto, sin valor inmediato). |
| **ADR sugerido si se acepta** | ADR-016-naming-alias-idordenpedidoenc-idpedidoenc |
| **Decision** | ☐ Aceptar  ☐ Rechazar  ☐ Diferir |
| **Notas** | _(libre)_ |

---

## H10 - C9-QAS: 25 de 1441 pedidos fiscales (1.7%) sin trans_pe_pol. Confirma H-04 (poliza dual-state frontend-only)

| Campo | Valor |
|---|---|
| **Hallazgo** | En IMS4MB_CEALSA_QAS, 1416 de 1441 pedidos cuyo tipo tiene `control_poliza=1` tienen registro en `trans_pe_pol` (98.3%). 25 pedidos fiscales (1.7%) NO tienen poliza captada. Valida empiricamente H-04 previo. |
| **Tipo** | Validacion empirica de bug previo (H-04) - data quality |
| **Validacion** | Q-013 ejecutada contra IMS4MB_CEALSA_QAS (28-abr-2026) |
| **Implicaciones** | Si la captura de poliza ocurre solo en UI sin commit transaccional al pedido, es esperable que ~1-2% se pierda. |
| **Accion concreta** | (1) Reforzar la propuesta P3-FISCAL-LOCK con validacion server-side bloqueante (no solo UX). (2) Auditar esos 25 pedidos especificos: ¿fueron creados por path de devolucion (donde la poliza no aplica) o se "colaron" sin poliza? (3) Que la WebAPI .NET 10 emita alerta cuando un pedido fiscal se cierre sin `trans_pe_pol`. |
| **ADR sugerido si se acepta** | ADR-017-fiscal-lock-server-side-bloqueante (refuerza P3 ya existente) |
| **Decision** | ☐ Aceptar  ☐ Rechazar  ☐ Diferir |
| **Notas** | _(libre)_ |

---

## H11 - BB tambien tiene basura en trans_reabastecimiento_log (755 filas). Extiende H-02 a BB

| Campo | Valor |
|---|---|
| **Hallazgo** | `trans_reabastecimiento_log` tiene 755 filas en BB-PRD, 1218 en K7-PRD, 0 en C9-QAS. Carol (P-24) habia identificado el problema solo en Killios. El patron "basura de instalacion no limpiada por SP CLBD_PRC" afecta tambien a BB. |
| **Tipo** | Tech debt de instalacion (extension de H-02) |
| **Validacion** | Verificacion durante Q-014 setup (28-abr-2026) |
| **Implicaciones** | (a) La accion atomica de H-02 (agregar `trans_reabastecimiento_log` al SP `CLBD_PRC`) es una mejora estructural del INSTALADOR, no un fix puntual de Killios. (b) Hay otras tablas de modulos no-usados que pudieron quedar pobladas (al menos: `trans_inv_*`, `trans_devolucion_*`). |
| **Accion concreta** | (1) Modificar el SP `CLBD_PRC` para incluir `trans_reabastecimiento_log` en la limpieza. (2) Ejecutar TRUNCATE/DELETE controlado en BB (con criterio temporal). (3) Auditar trans_inv_*, trans_devolucion_* y otras tablas de modulos no usados (esto es input para el Ciclo 8b). |
| **ADR sugerido si se acepta** | ADR-018-clbd-prc-extender-tablas-instalacion |
| **Decision** | ☐ Aceptar  ☐ Rechazar  ☐ Diferir |
| **Notas** | _(libre)_ |

---

## Bulk decisions (atajos)

Si querés decidir en bloque, marcá una opcion:
- ☐ Aceptar las 6 (recomendado por el agente: las 6 estan validadas via SQL live, no hay especulacion)
- ☐ Aceptar todas EXCEPTO las que listo: __________
- ☐ Diferir todas (fundamentar trigger): __________
- ☐ Rechazar todas (fundamentar)

---

## Recomendacion del agente

**Aceptar las 6**. Justificacion:

| H | Por que aceptar |
|---|---|
| H06 | Solo recalibra texto de ADR-012 sin invalidar la decision de fondo. Costo near-zero, aclara la doctrina "afinidad de procesos vs afinidad de datos". |
| H07 | Insumo directo para diseniar la WebAPI .NET 10 multi-tenant. Sin esto, el endpoint de KPIs queda mal modelado desde el dia 1. |
| H08 | Simplificacion concreta del bridge brain<->WMS (.NET 8). Menos codigo, menos casos. Validado al 100% en 2 BDs productivas. |
| H09 | Tech debt que ya existe - solo lo formalizamos antes de que el ORM nuevo trope. |
| H10 | Refuerza propuesta P3-FISCAL-LOCK existente con evidencia empirica concreta. Habilita la priorizacion del ticket. |
| H11 | Mejora del INSTALADOR (impacto en TODOS los clientes nuevos). Costo de implementacion bajo, valor alto. |

Ningun hallazgo conflictua entre si. Aceptar todos en bloque deja la base mas limpia para arrancar el Ciclo 8b (afinidad de procesos extendida) o el bloque C (arquitectura WebAPI .NET 10).

---

## Procedimiento post-decision (lo hace el agente al recibir tu marca)

1. Por cada hallazgo aceptado: crear `architecture/adr/ADR-NNN-<slug>.md` con plantilla ADR completa (status: ratificado, contexto, decision, consecuencias, alternativas).
2. Actualizar las propuestas en `_proposals/` con `status: ratificado` + `adr_promovido_a: ADR-NNN`.
3. Actualizar `consolidacion-ciclo-8a.md` con la tabla de ratificaciones aplicadas.
4. Si hay rechazos: dejar la propuesta con `status: rechazado` + tu justificacion.
5. Si hay diferidos: dejar `status: diferido` + trigger declarado.
6. Commit unico + push.

## Cross-references

- Propuestas origen: `brain/_proposals/20260428-1905-H06...md` hasta `20260428-1910-H11...md`
- Consolidacion del Ciclo 8a: `brain/outputs/consolidaciones-ciclo/consolidacion-ciclo-8a.md`
- ADR previo afectado: `brain/architecture/adr/ADR-012-bypass-estado-despachado.md` (H06 lo recalibra)
- Hallazgo previo afectado: `H-04` (H10 lo refuerza), `H-02` (H11 lo extiende)
