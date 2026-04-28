# brain-map/ — mapeo cliente × tabla × proceso × funcionalidad

> Proposito: dar respuesta directa y univoca a preguntas tipo
> "¿Killios valida etiqueta en verificacion?" o "¿BECOFARMA usa
> licencia retroactiva?" sin tener que reconstruir el contexto cada
> vez.
>
> Audiencia primaria: agente brain (futuras consultas). Audiencia
> secundaria: humanos generando sets de pruebas, configurando deploys,
> diagnosticando issues por cliente.

## Estructura

- `funcionalidades-por-cliente.md` — Matriz funcionalidad × cliente.
  Cada celda dice ON/OFF/PARCIAL con evidencia (tablas pobladas,
  parametros, learnings).
- `tablas-por-funcionalidad.md` — Para cada funcionalidad, lista de
  tablas que la respaldan + cardinalidades esperadas por cliente.
- `parametros-por-cliente.md` — Capability flags y parametros de
  configuracion conocidos por cliente (campo a campo).

## Reglas de mantenimiento

1. Si una pregunta de Erik aterriza una nueva funcionalidad o un nuevo
   capability flag, **se actualiza acá** ademas del learning card.
2. Las celdas de la matriz tienen 4 estados:
   - **ON** (con evidencia: tabla X poblada, flag Y=1)
   - **OFF** (con evidencia: tabla X vacia, flag Y=0)
   - **DESCONOCIDO** (no se ha investigado)
   - **PARCIAL** (modulo activo en algunos sub-procesos solamente)
3. Toda celda **debe citar evidencia** (learning card, query, fecha
   de validacion). Sin evidencia => DESCONOCIDO.
4. **Distincion BD productiva vs BD diagnostica EC2**: las BDs en
   `52.41.114.122,1437` son copias parciales o desfasadas de las
   productivas reales que viven en el laptop de Erik. Una funcionalidad
   marcada OFF "porque la tabla no existe en EC2" es solo un
   indicador, NO una conclusion firme. Marcar DESCONOCIDO hasta
   confirmar con productiva real.
