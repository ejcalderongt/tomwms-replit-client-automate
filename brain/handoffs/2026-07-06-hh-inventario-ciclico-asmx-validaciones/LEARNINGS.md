---
tipo: other
autores: [codex-local, erik]
---

# Verificaciones obligatorias

## Tecnicas

- [ ] Confirmar que la HH apunta a `TOMHHWS.asmx.vb` y no al WebAPI.
- [ ] Validar que la llave exacta `ubicacion + gondola` decide el flujo.
- [ ] Verificar que una gondola existente en otra ubicacion se rechaza.
- [ ] Verificar que una combinacion nueva para la ubicacion si continua.
- [ ] Confirmar que la lista visible solo trae el detalle de la llave exacta.
- [ ] Mantener trazas en archivo de texto cuando haga falta auditar el flujo.

## Criterios de continuidad

- [ ] Si el metodo JSON no esta publicado, seguir con el camino SOAP estable.
- [ ] Si se publica el metodo JSON, registrar el cambio de forma atomica en el
      brain antes de mover la HH a ese endpoint.

