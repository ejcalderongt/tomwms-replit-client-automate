---
id: L-062
tipo: learning
estado: vigente
revision: 2
titulo: "Carolina: topologia local verificada y preflight seguro"
clientes: [CORE]
ramas: [wms-brain, dev_2026_estable]
tags: [carolina, topologia_local, preflight, federacion, embeddings, git, seguridad]
fecha: "2026-08-19"
confianza: alta
---

# L-062 - Carolina: topologia local y preflight seguro

## Hallazgo

La federacion local no puede reutilizar rutas de otra estacion. En la maquina
de Carolina, los checkouts verificados son:

- BOF: `C:\Users\carol\source\repos\TOMWMS_BOF`.
- HH Android: `C:\Users\carol\StudioProjects\TOMHH2025`.
- Brain canonico: `C:\Users\carol\Documents\wms-brain`.
- Arbol indexable del brain: `C:\Users\carol\Documents\wms-brain\wms-brain`.

El manifiesto `wms-brain/tools/wms-federation.yml` conserva rutas
`C:\Users\yejc2\...` porque corresponde a la estacion de Erik. No debe
reescribirse con rutas de Carolina para publicar este aprendizaje: la topologia
por estacion pertenece a un overlay local o a documentacion indexable.

## Regla operativa

1. Ejecutar el preflight local antes de analizar o modificar BOF/HH.
2. Tratar BOF y HH como repos de codigo fuente; el brain y los repos de
   intercambio son plano de control.
3. Si BOF o HH ya tienen cambios locales, preservarlos y limitar la curacion a
   archivos nuevos o no solapados.
4. No asumir una rama historica: observar la rama activa. El 2026-08-19 BOF y
   HH estaban en `dev_2026_estable`, no en `dev_2028_merge`.
5. `Conn.ini`, secretos, connection strings y configuracion local quedan fuera
   de la curacion salvo instruccion explicita.
6. Para bundles: `dry-run` antes de aplicar; aplicar solo con confirmacion y sin
   merge automatico.
7. Para eventos del brain: probar con `brain_bridge.mjs` y `--no-push` antes de
   publicar.
8. No versionar rutas de Carolina sobre el manifiesto operativo de Erik. Enviar
   el contexto mediante learning/handoff para que Erik lo indexe con su propio
   manifiesto.
9. La falta de endpoint de embeddings en Carolina no bloquea la curacion. El
   resultado valido es dejar documentos fuente completos mas una solicitud de
   indexado para la estacion que si dispone del servicio.

## Evidencia

- Preflight `Test-TomWmsLocal.ps1` exitoso para existencia de BOF, HH, brain,
  exchange, db-brain y brain-client.
- Git observo cambios locales preexistentes tanto en BOF como en HH; no fueron
  alterados por esta curacion.
- `wms-brain/tools/wms-federation.yml` apunta a la cuenta `yejc2`, coherente con
  la estacion de Erik; se preservo sin cambios para su reindexado.
- Instruccion de Erik: enviar el contexto y la documentacion necesarios para
  indexarlos en su PC o dejar la anotacion correspondiente.
- Reconfirmacion de Erik del 2026-08-19: repetir la actualizacion del brain con
  los cambios recientes, respetando vectores y formatos.
- Solicitud estructurada de indexado:
  `wms-brain/brain/handoffs/2026-08-19-carolina-local-operator/INDEX-REQUEST.yml`.

## Implicaciones

- Erik puede indexar este handoff desde su estacion sin adoptar paths de
  Carolina.
- El estado de la curacion y el estado del indice son independientes: los
  documentos pueden estar versionados y listos aunque la materializacion del
  indice se ejecute despues en otra PC.
- Los agentes deben separar topologia local verificada de rutas historicas
  documentadas en otros equipos.
- Un preflight con advertencias no autoriza limpiar, resetear ni descartar el
  trabajo local de BOF/HH.

## Vinculos

- `wms-brain/tools/wms-federation.yml`
- `wms-brain/brain/handoffs/2026-08-19-carolina-local-operator/README.md`
- `wms-brain/brain/handoffs/2026-08-19-carolina-local-operator/INDEX-REQUEST.yml`
- `brain/learnings/L-057-proc-carolina-vector-index-y-saludo-erik.md`

## Q abiertas

- Definir un overlay local no versionado para que cada estacion resuelva sus
  rutas sin modificar el manifiesto de otra persona.

## Confianza

Alta: rutas, ramas y estado fueron observados mediante preflight y Git local.
