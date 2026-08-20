---
slug: 2026-08-19-carolina-local-operator
estado: listo_para_indexar_en_pc_erik
fecha: "2026-08-19"
owner: core
clientes: [CORE]
ramas: [wms-brain, dev_2026_estable]
tags: [carolina, local_paths, preflight, federacion, embeddings, brain_bridge, bundle]
freshness_days: 90
---

# Carolina local operator - topologia y guardrails

## Rutas verificadas

- BOF: `C:\Users\carol\source\repos\TOMWMS_BOF`.
- HH: `C:\Users\carol\StudioProjects\TOMHH2025`.
- Brain: `C:\Users\carol\Documents\wms-brain`.
- Exchange: `C:\Users\carol\Documents\Codex\2026-06-12\clona-este-repositorio-contextualiza-y-analiza\tomwms-replit-client-automate-fresh`.
- DB brain: `C:\Users\carol\Documents\Codex\2026-06-12\clona-este-repositorio-contextualiza-y-analiza\tomwms-db-brain`.
- Brain client: `C:\Users\carol\Documents\Codex\2026-06-12\clona-este-repositorio-contextualiza-y-analiza\tomwms-wms-brain-client`.

## Routing

- BOF y HH son fuentes; no hacer reset, descarte, push o merge automatico.
- Brain, exchange y db-brain son plano de control y conocimiento.
- Para esquema o dominio SQL, consultar db-brain antes de inferir desde codigo.
- Para HH, enrutar por `domain-hh-android`; para servicios `.asmx`, combinar
  `domain-integration-services` con `domain-hh-android`.

## Preflight y ramas

Ejecutar el preflight local antes de operar. La rama debe observarse, no
presuponerse. Al 2026-08-19, BOF y HH estaban en `dev_2026_estable` y ambos
tenian cambios locales preexistentes. Esas advertencias obligan a preservar el
trabajo; no son una solicitud de limpieza.

## Bundles y eventos

- Bundle: ejecutar `apply_bundle.mjs --dry-run` antes de cualquier aplicacion.
- Aplicacion: requiere confirmacion; debe dejar rama efimera y nunca mezclar
  automaticamente a una rama estable.
- Brain events: usar `brain_bridge.mjs` contra `wms-brain` y preferir
  `--no-push` durante la validacion local.

## Seguridad

No registrar passwords, tokens ni connection strings completas. No editar
`Conn.ini`, `.config`, `.ini` o secretos salvo encargo explicito. La
configuracion local conocida no es deuda que un agente deba limpiar.

## Vector de federacion

El manifiesto versionado mantiene las rutas de la PC de Erik. Las rutas de
Carolina se documentan aqui y no deben reemplazar las de Erik. Este handoff
queda bajo `brain/handoffs`, una fuente recorrida por el indexador, para que la
topologia y los guardrails sean recuperables por busqueda semantica.

## Entrega para indexado en la PC de Erik

La solicitud legible por maquina vive en `INDEX-REQUEST.yml`. Despues de
recibir los commits fuente en su checkout `wms-brain`, Erik puede ejecutar
desde el arbol anidado `wms-brain`:

```powershell
python .\tools\wms-embeddings\wms_embeddings.py index --incremental
python .\tools\wms-embeddings\wms_embeddings.py query "Carolina topologia local preflight seguro"
python .\tools\wms-embeddings\wms_embeddings.py export-html
```

La primera consulta debe recuperar este handoff o el learning `L-062`. Si el
endpoint local no expone `/models` y `/embeddings`, conservar la anotacion como
pendiente y no generar un indice parcial.

## Estado de entrega

- Contexto humano: completo.
- Paquete vectorizable: completo.
- Solicitud de indexado: preparada.
- Indexado en Carolina: no requerido por instruccion de Erik.
- Indexado en PC de Erik: pendiente de recepcion/publicacion de los commits.
- Push: no realizado; requiere autorizacion separada.
