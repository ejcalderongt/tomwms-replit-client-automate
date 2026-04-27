# Scenario: doble-despacho

Reproduce el caso real de un pedido despachado en **dos viajes parciales**
(60 + 40 = 100). Sirve para reproducir y testear:

- Bug P-16b (encabezado vuelve a PENDIENTE despues de despachar).
- Calculo correcto de `cantidad_pendiente` en outbox.
- Sincronizacion outbox cuando hay multiples emisiones por pedido.
- Estado del encabezado en transiciones intermedias (`PARCIAL`).

## Como se ejecuta

```powershell
# Solo en LOCAL_DEV o un perfil DEV explicito
Send-WmsBrainSeed -Scenario doble-despacho -Profile LOCAL_DEV -Confirm

# Validar que las expectations pasan
Test-WmsBrainScenario -Scenario doble-despacho -Profile LOCAL_DEV

# Limpiar
Send-WmsBrainSeed -Scenario doble-despacho -Profile LOCAL_DEV -Mode Teardown -Confirm
```

## Safety

Este scenario tiene `denyOnPrd: true` y `requiresConfirm: true` en
`scenario.json`. El cliente debe **rechazar** correrlo contra cualquier
perfil cuyo nombre matchee `*-PRD` o `*-QAS`. Si por error se intenta,
debe abortar antes de tocar la BD.

Tambien antes del setup, el cliente debe:

1. Tomar snapshot de las 5 tablas afectadas (filtradas por marker).
2. Loggear la operacion en `%APPDATA%\WmsBrainClient\history\<opid>.json`.
3. Pedir confirmacion explicita.

Si el setup falla a mitad, el `XACT_ABORT ON` rollbackea automaticamente.
Si el operador quiere deshacer despues, `Undo-WmsBrainOperation -Id <opid>`
ejecuta el teardown automaticamente.

## Expectations

5 validaciones (E1-E5):
- E1: pedido_enc en estado DESPACHADO.
- E2: 2 despachos confirmados.
- E3: cantidad despachada total = 100.
- E4: pedido_det con cantidad_despachada=100, estado=DESPACHADO.
- E5: 2 outbox SALIDA pendientes (60 + 40).

## Marker

Todos los registros tienen `user_agr = 'SCENARIO-doble-despacho-001'`
para identificacion y limpieza. Si necesitas correr el scenario en
paralelo (varios devs en la misma BD), versionar el marker.

## Casos reales asociados

- **K7** caso 2328 (pedido con 2 viajes en marzo 2026).
- **BB** decenas de casos similares (revisar `i_nav_transacciones_out`
  con `cantidad_enviada > 0 AND cantidad_enviada <> cantidad`).
