---
id: ADR-012
titulo: Bypass estado=Despachado - permitir con permiso explicito + razon obligatoria + log de auditoria
status: proposed (provisional - esperan ratificacion de EJC)
fecha: 2026-04-28
proponente: agente brain (sesion replit)
ratificador_pendiente: Erik Calderon
hallazgo_origen: H-04 (brain/_inbox/20260428-1903-H04-despacho-fantasma-bypass-estado.json)
preguntas_origen: P-16
adrs_relacionados: ADR-010 (reserva-webapi vs wms-legacy), ADR-011 (decisiones de schema legacy)
---

# ADR-012 - Bypass estado=Despachado: permitir con permiso explicito + razon obligatoria + log de auditoria

## Contexto

43 pedidos en Killios PRD (1.1% del trafico) tienen `trans_pe_enc.estado='Despachado'` sin fila correspondiente en `trans_despacho_enc`. Es decir, el flujo lineal Nuevo→Pendiente→Pickeado→(Verificado)→Despachado fue **bypaseado** para esos pedidos.

**Carol (pasada-7, P-16)** confirma literal: *"esto generalmente pasa porque se le cambia el estado al pedido a estado despachado, sin realmente despachar el producto, el WMS lo permite"*. Es una capacidad legacy intencional, no un bug accidental.

**Erik (tanda-1, P-08)** documenta un caso analogo (NUEVO → Pickeado directo para pedidos que el WMS inyecta automaticamente entre bodegas virtuales) que justifica la flexibilidad de saltarse el flujo lineal en escenarios controlados.

**SQL agente (tanda-2, P-16)** confirma que la matematica de "discrepancia 4032 vs 3989" no era huerfanos 1:1 (despacho_enc agrupa multiples pedidos), pero **dejo abierta** la sub-pregunta P-16b refinada: cuantos pedidos en `trans_despacho_det` tienen `trans_pe_enc.estado` distinto de Despachado? Si la respuesta es cero, el bypass de Carol es la unica fuente de divergencia.

## Decision

**Opcion (b): permitir el bypass con tres requisitos.**

El WebAPI nuevo (`reserva-webapi`) exponer un endpoint `POST /pedidos/{id}/forzar-estado` que permite cambiar el estado a Despachado **sin disparar un despacho fisico**, pero exigiendo:

### Requisito 1 - Permiso explicito en el rol

El usuario debe tener el permiso `pedidos.forzar_despachado`. No es un permiso por defecto; debe ser asignado explicitamente por administrador.

### Requisito 2 - Razon obligatoria en el request

```json
POST /pedidos/{id}/forzar-estado
{
  "estado_destino": "Despachado",
  "razon": "Pedido fue despachado por flujo manual fuera de WMS, registro de cierre por trazabilidad",
  "estado_anterior_observado": "Pickeado"
}
```

Validaciones:
- `razon`: minimo 20 caracteres, no whitespace solo, max 500 caracteres.
- `estado_destino`: solo "Despachado" o "Anulado" en esta version.
- `estado_anterior_observado`: debe coincidir con el estado actual del pedido (control de concurrencia).

### Requisito 3 - Log de auditoria en tabla nueva

```sql
CREATE TABLE aud_pedido_estado_forzado (
    Id              BIGINT          IDENTITY PRIMARY KEY,
    IdPedidoEnc     INT             NOT NULL,
    EstadoAnterior  VARCHAR(50)     NOT NULL,
    EstadoNuevo     VARCHAR(50)     NOT NULL,
    Razon           VARCHAR(500)    NOT NULL,
    UserId          INT             NOT NULL,
    UserNombre      VARCHAR(100)    NOT NULL,
    Fecha           DATETIME2       NOT NULL DEFAULT SYSDATETIME(),
    OrigenIP        VARCHAR(45)     NULL,
    UserAgent       VARCHAR(500)    NULL,
    INDEX IX_aud_pedido_estado_forzado_PedidoEnc (IdPedidoEnc),
    INDEX IX_aud_pedido_estado_forzado_Fecha (Fecha DESC)
);
```

Cada llamada al endpoint inserta una fila. Es append-only (no se borra nunca, no hay update).

### Comportamiento en el bridge

El bridge (skill `wms-test-bridge`) debe:
1. Detectar pedidos con `trans_pe_enc.estado='Despachado'` sin `trans_despacho_enc` correspondiente.
2. **Si existe fila en `aud_pedido_estado_forzado`**: clasificar como "bypass auditado" - OK.
3. **Si NO existe fila en `aud_pedido_estado_forzado`**: clasificar como "bypass huerfano" - alerta (puede ser legacy pre-WebAPI o uso del WMS legacy concurrente).

## Alternativas consideradas

### Opcion (a): prohibir el bypass

- **Pros**: limpia el modelo, fuerza el flujo lineal.
- **Contras**: rompe operacion. Los 43 pedidos historicos en Killios prueban que la capacidad SE USA en produccion para casos legitimos. Eliminarla obliga a Operations a buscar workarounds (ej. crear despachos sinteticos), peor opcion.
- **Veredicto**: rechazada. No respeta paridad operativa.

### Opcion (c): replicar legacy tal cual (sin auditoria)

- **Pros**: paridad ciega, minimo cambio.
- **Contras**: continua el problema actual (43 casos no trazables). El bridge no puede distinguir bypass legitimo de bug.
- **Veredicto**: rechazada. Pierde la oportunidad de mejorar visibilidad.

## Consecuencias

### Positivas

- Preserva la flexibilidad operativa que Operations realmente usa.
- Hace visible (no oculto) el bypass: cada caso queda registrado con quien, cuando, por que.
- Habilita reportes de uso del bypass para detectar abuso o patrones anomalos.
- El bridge tiene contrato claro para clasificar discrepancias en 2 categorias (auditadas vs huerfanas).

### Negativas

- Requiere migracion de schema (tabla `aud_pedido_estado_forzado` nueva).
- Requiere ajuste en BOF UI: agregar form modal con campo razon obligatorio cuando se fuerza estado.
- Requiere gestion de permisos: nuevo permission key `pedidos.forzar_despachado`.

### Mitigacion

1. La tabla `aud_pedido_estado_forzado` se puede crear en Pasada 9 antes del primer release del WebAPI. No bloquea el scaffold inicial.
2. El BOF puede mostrar el form en una iteracion siguiente; mientras tanto el endpoint del WebAPI exige el campo razon en el JSON request.
3. El permiso se puede default-deny y exigir habilitacion explicita por admin.

## Sub-pregunta abierta para Pasada 8a

Antes de aplicar, **cerrar la P-16b refinada con SQL READ-ONLY**: ¿cuantos pedidos en Killios PRD tienen `trans_pe_enc.estado='Despachado'` sin fila en `trans_despacho_enc`? ¿Coincide con los 43 reportados por Carol? Si el numero es muy diferente, recalibrar.

Query incluida en `brain/wms-specific-process-flow/queries-pasada-8a.md` (sub-Q P-16b).

## Ratificacion

Esta decision es **provisional**. Se aplica en el scaffold inicial del WebAPI. Si Erik la veta o pide cambiar el shape de la tabla aud o los requisitos del endpoint, hacer rollback antes de la primera release.

## Referencias

- Hallazgo: `brain/_inbox/20260428-1903-H04-despacho-fantasma-bypass-estado.json`
- Pregunta origen: `brain/wms-specific-process-flow/preguntas-pasada-7.md` P-16
- Respuesta Carol: `brain/wms-specific-process-flow/respuestas-pasada-7.md` P-16
- Refinamiento SQL: `brain/wms-specific-process-flow/respuestas-tanda-2.md` P-16
- Consolidacion: `brain/wms-specific-process-flow/consolidacion-pasada-7.md` D-03
- Bug report relacionado: `brain/wms-specific-process-flow/bug-report-p16b.md`
