# MH05: corrección de corredores — 2026-10-28

<!-- #EJC20261028_EXO_HILERAS -->

Cliente MHS, base TOMWMS_MHS_DEV, bodega 5/MH05. Código BOF dev_2028_merge; fuentes y hashes en sources.json. Cambios locales sin commit ni push. No se desplegó el ejecutable en uso.

Se corrigió la agrupación invertida horizontal/vertical y el mínimo artificial de tres registros por hilera. Una hilera puede estar representada por un solo tramo. Se conservan los filtros de colisiones, límites y aprobación humana. Los corredores candidatos no son rutas operativas. Se reportan pares rechazados con IDs y ancho libre, racks sin clasificación de picking y estado por ubicación elegible. Cobertura cero solo se confirma si no hay accesos y existen ubicaciones elegibles; con segmentos de acceso la cobertura sigue desconocida hasta verificar vínculos.

La lectura SQL de geometría contiene 22 tramos y 288 ubicaciones. Las únicas ubicaciones activas físicas marcadas picking son 286/294 en tramos 20/28, sin dimensiones. Ocho tramos carecen de dimensiones. Los huecos centrales registrados son 0.78 m y no cumplen mínimo 1.20 m. No se cambiaron medidas, banderas, pedidos, stock ni asignaciones.

Esquema: script DBA/20261012_wms_exoesqueleto_versionado_proposal.sql, SHA256 F87AA8E10068897CDB8C3614385FFC7079E27EE1C95B6D4117906289D7FC86F1. Dry-run con rollback correcto y aplicación posterior únicamente en MHS_DEV: las cuatro tablas existen, incluida acceso. Esto verifica presencia, no persistencia real desde la pantalla ni conformidad completa de tablas preexistentes.

Validación: build WMS correcto (advertencias existentes de dependencias/DevExpress); cuatro casos nuevos, horizontal/vertical y hueco estrecho/amplio; pruebas existentes de telemetría y reconexión correctas. Replay de MH05: 128 nodos, 129 enlaces, 10 corredores candidatos, cero colisiones reconocidas y cero nodos fuera. Longitud transitable 303.74 m; cero accesos y cobertura 0%. Los 19 nodos aislados del cálculo global pertenecen a propuesta que aún requiere revisión; no presentar todos los nodos como red transitable aprobada.

Auditoría automática WARN: First() en rutinas existentes protegido por bucles/conteos; catálogo SQL automático omitido. Revisión manual: sin cambios de estados operativos; cambios de grafo en memoria y telemetría, esquema aplicado solo en DEV. El runner de correlación tenía error de interpolación $t:; se ejecutó copia corregida fuera del repo fuente. No presentar WARN como PASS automático.

Evidencia en analysis/brain-improvement/: mh05-build-20261028.log, mh05-tests-final/, mh05-telemetry-final/, mh05-replay-final/, mhs-mh05-layout-20261028.json y mh05-change-audit.yml.

Pendiente: prueba visual real, validar medidas físicas y clasificación, vincular cada ubicación a su acceso, verificar guardado/recarga y luego simular pedido. No implementados Dijkstra/A* integrado ni asignación automática de operadores.


## Validación visual posterior — #EJC20261028_EXO_HOLGURA

La captura posterior y telemetría 20261028-183924 reproducen los diez candidatos. La revisión detectó que nueve grupos carecen de holgura lateral: al comparar hileras no contiguas podía caer un eje entre racks intermedios. Cero colisiones de línea no acreditaba ancho operativo. Se añadió validación del segmento candidato completo contra obstáculos expandidos por medio ancho de pasillo y contra límites; se conserva margen de seguridad existente. También se recalcula el contador de candidatos después del filtro. Esta revisión sustituye la interpretación anterior de diez candidatos como mejora validada; aún no eran rutas aprobadas.

Replay corregido: un grupo candidato horizontal en y=6.61 m, dos secciones; cero nodos aislados y un componente, cero colisiones y nodos fuera. Continúa cobertura cero por las dos ubicaciones sin base. No se cambió SQL en esta validación. Build aislado, ejecutable de pantalla pendiente de actualización; no asumir que la captura contiene el último filtro.


## Separación de repositorios — #EJC20261029_BRAIN_UBICACION

El script Test-HilerasExoesqueleto.ps1 fue trasladado desde BOF/tools/picking a scripts/ de este handoff. Es validación operativa del Brain; no debe volver a añadirse al repositorio fuente. La ruta anterior en sources.json se conserva solo como procedencia histórica.
