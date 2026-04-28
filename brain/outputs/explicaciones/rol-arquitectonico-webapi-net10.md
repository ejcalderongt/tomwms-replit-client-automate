# Rol arquitectonico de la WebAPI .NET 10 — los 3 modelos posibles

> Aclaracion solicitada por Erik el 29-abr-2026: "no comprendi porque
> hablas en parte de que el webapi 'escribe' (Si la WebAPI escribe en
> esta tabla para mantener compatibilidad con SAPBOSync.exe/NavSync.exe)
> podrias ampliar o explicar lo que estas comprendiendo?"
>
> Estado: PROPUESTA arquitectonica para discusion. Necesita decision de
> Erik antes de avanzar al ADR.

## El contexto que tenia en la cabeza

Cuando hable en el L-017 de "si la WebAPI escribe a `i_nav_transacciones_out`",
la palabra clave es **si**. Era un condicional, no una afirmacion.
Estaba contemplando que existen **3 modelos posibles** para insertar la
WebAPI nueva en el ecosistema TOMWMS, y la decision de cual adoptar
todavia no esta tomada. Te las desarrollo aca para que elijamos.

---

## Modelo A — WebAPI como CONSUMIDORA (reemplaza los .exe)

```
WMS .NET BOF (VB.NET) ─escribe SP─> i_nav_transacciones_out ─polling─> WebAPI .NET 10 ─HTTP─> SAP B1
WMS .NET HH (Android) ─escribe SP─>           |
                                              └─polling─> WebAPI .NET 10 ─OData─> NAV / Business Central
```

### Que cambia
- **Reemplaza**: `SAPBOSync.exe`, `NavSync.exe`, `SAPSync.exe` y
  binarios equivalentes que hoy estan empaquetados en el ClickOnce
  y se invocan via `i_nav_config_enc.nombre_ejecutable` (ver L-015).
- **NO toca**: ningun SP del WMS, ningun trigger, ninguna tabla.
  El WMS sigue escribiendo a `i_nav_transacciones_out` como hoy.

### Quien escribe a `i_nav_transacciones_out`
- Lo escriben los SP del WMS (igual que hoy).
- La WebAPI **solo lee**. Marca `enviado=1` y `fec_envio=getdate()`
  cuando recibe ack del ERP.

### Pros
- **Riesgo bajo**: contrato con el WMS no cambia. Cero modificaciones
  a la BD ni a los SP existentes.
- **Migracion gradual**: se puede activar la WebAPI cliente por cliente
  apagando el .exe del ClickOnce respectivo y dejando el resto operando
  con .exe legacy.
- **Rollback trivial**: si la WebAPI falla, se vuelve a habilitar el
  .exe.
- **Resuelve la deuda real**: el problema actual son los .exe (codigo
  legacy, dificil de mantener, sin testing, deploy via ClickOnce).
  El outbox como patron es solido y no necesita refactor.

### Contras
- **No mejora el throughput** del flujo WMS->outbox (sigue siendo SP
  con escritura sincronica).
- **El outbox queda como acoplamiento BD-mediated** entre WMS y WebAPI.
  Si en el futuro se quiere distribuir la WebAPI en multi-instancia,
  la coordinacion del polling sobre la cola necesita locking optimista.

### Cuando aplicaria mi comentario sobre el "0 sentinela"
- **No aplica**. La WebAPI no escribe a la tabla en este modelo. Solo
  necesita LEER respetando el sentinela (`WHERE idpedidoenc > 0`).

---

## Modelo B — WebAPI como PUNTO DE ENTRADA (reemplaza los SP)

```
WMS .NET BOF ─HTTP─> WebAPI .NET 10 ─escribe─> i_nav_transacciones_out
WMS .NET HH ─HTTP─> WebAPI .NET 10        └──HTTP/OData─> SAP / NAV
                          └─escribe BD WMS (pedidos, despachos, recepciones)
```

### Que cambia
- **Reemplaza**: los SP del WMS que escriben a `i_nav_transacciones_out`
  (los que se disparan al confirmar un despacho, una recepcion, etc).
- **Reemplaza**: los .exe legacy (igual que en Modelo A).
- **El WMS llama HTTP a la WebAPI** en lugar de invocar SP locales.

### Quien escribe a `i_nav_transacciones_out`
- Lo escribe la WebAPI en respuesta a llamadas del WMS.

### Pros
- **Contrato unico** entre WMS y WebAPI: HTTP/JSON tipado con OpenAPI.
- **Logica centralizada** en .NET 10 con testing real, observabilidad,
  trazabilidad.
- **Habilita la WebAPI a poblar campos como `correlation_id` automaticamente**
  para tracing distribuido.
- **Permite agregar validacion antes de escribir al outbox** (ej.
  rechazar una transaccion con FK invalida o cantidad negativa).

### Contras
- **Riesgo alto**: hay que portar TODOS los SP del WMS que escriben
  al outbox. Volumen de trabajo grande, regresiones potenciales.
- **Cambio de contrato del WMS**: el codigo VB.NET del BOF y el codigo
  Android del HH tienen que migrar de "llamar SP local" a "llamar HTTP".
  Esto es una refactor mayor del cliente WMS.
- **Latencia**: cada confirmacion de despacho/recepcion hace un round
  trip HTTP. Hoy es local DB.
- **Rollback dificil**: una vez que el WMS llama HTTP, volver a SP
  requiere re-deploy de WMS.

### Cuando aplicaria mi comentario sobre el "0 sentinela"
- **AQUI APLICA**. La WebAPI escribe a la tabla. Para no romper a los
  consumidores legacy (ya sean los .exe que sigan corriendo en transicion,
  o reportes/integraciones que asuman 0 en lugar de NULL en las FKs no
  aplicables), debe rellenar las FKs no aplicables con `0`, no NULL.

---

## Modelo C — WebAPI ELIMINA el outbox (refactor radical)

```
WMS .NET BOF ─HTTP─> WebAPI .NET 10 ─HTTP/OData (sincronico)─> SAP / NAV
WMS .NET HH ─HTTP─> WebAPI .NET 10 ─HTTP/OData (sincronico)─> SAP / NAV
```

Sin tabla intermedia. La WebAPI empuja en el mismo request al ERP.
Si el ERP responde OK, devuelve OK al WMS; si falla, rollback completo.

### Pros
- **Eliminacion total de la deuda** del outbox y de los .exe legacy.
- **Latencia end-to-end conocida** (no hay polling intermedio).

### Contras
- **Riesgo extremo**: perdida de la garantia at-least-once que da el
  outbox. Si el ERP esta caido, la transaccion del WMS falla.
- **Acoplamiento sincronico** WMS<->ERP que rompe el desacoplamiento
  actual.
- **No recomendado** para este proyecto. Se menciona solo por completitud.

---

## Mi recomendacion

**Modelo A para la fase 1**, con migracion eventual a **Modelo B en
fase 2** si los pros lo justifican.

### Razones:

1. **El problema real es los .exe legacy**, no el outbox. Atacar el
   problema real primero, dejar el outbox como esta (es un buen patron).
2. **Modelo A se puede deployar cliente por cliente** apagando el .exe
   del ClickOnce respectivo. Modelo B requiere modificar el WMS.
3. **Modelo A permite validar la WebAPI nueva en produccion** sin
   cambiar el contrato del WMS. Si algo falla, el rollback es trivial.
4. **Modelo B se vuelve viable cuando el WMS este modernizado** (ej.
   migracion del BOF a una version mas nueva de .NET donde llamar HTTP
   sea natural). Hoy con VB.NET legacy el costo del cambio es alto.

### Roadmap propuesto:

```
fase 0 (estado actual):
  WMS escribe SP -> outbox -> .exe legacy (ClickOnce) -> SAP/NAV

fase 1 (Modelo A — reemplazo de .exe):
  WMS escribe SP -> outbox -> WebAPI .NET 10 -> SAP/NAV
  Beneficios inmediatos: observabilidad, testing, deploy moderno,
  centralizacion logica de integracion.

fase 2 (Modelo B — opcional, mediano plazo):
  WMS llama HTTP -> WebAPI escribe outbox + push a SAP/NAV
  Habilita validacion previa, correlation_id automatico, contrato
  unico tipado.

fase 3 (Modelo C — desaconsejado):
  No recomendado por las razones de riesgo arriba.
```

## Pregunta directa a Erik

¿Confirmas que arrancamos por **Modelo A** (WebAPI como reemplazo de
los .exe, sin tocar el WMS)? Si si, levanto el ADR-001 con esa decision
y la propuesta concreta de endpoints, seguridad, observabilidad,
estrategia de polling sobre el outbox, y plan de migracion cliente
por cliente (orden sugerido: BECOFARMA primero porque ya tenemos
diagnostico exhaustivo, luego K7 y BB).

Si preferis ir directo al Modelo B: tambien podemos, pero necesitaria
un releve previo de los SP del WMS que escriben al outbox (cuantos,
cuales, complejidad) para estimar esfuerzo.
