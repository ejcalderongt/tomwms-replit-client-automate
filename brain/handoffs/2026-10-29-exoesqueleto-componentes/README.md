# Componentes de exoesqueleto — 2026-10-29

<!-- #EJC20261029_EXO_COMPONENTES -->

Alcance: BOF dev_2028_merge, cambios locales sin commit/push. Se conservan cambios anteriores de MH05. Telemetría suministrada por usuario: BOD1/IdBodega=1, IdEmpresa=1; cliente y base no acreditados por el archivo. No atribuir a MHS ni CAREMED por semejanza visual.

Implementado: mapa determinista de componentes por enlaces transitables; candidatos y bloqueados no unen componentes. Identificadores Cn por orden de nodo mínimo (pueden cambiar al modificar topología, no son llaves persistentes). Nodos exclusivos de candidato separados de aislados reales. Pantalla: botones Colorear componentes/Mostrar tipos de enlace y Detalle de componentes; enlaces y nodos coloreados, etiquetas Cn y hover en edición. Diálogo desplazable con nodos, enlaces y segmentos de acceso por componente. Vista normal conserva colores por tipo. No cambia geometría ni estados al alternar vista.

Telemetría JSON agrega componentes_transitables, nodos_solo_candidatos y nodos_aislados_sin_candidato. Métricas antiguas conservadas para compatibilidad. Cada componente declara ubicaciones_atendidas=null y vínculo pendiente: Entity/DAL/formulario aún no vinculan ubicaciones con nodos de acceso. No inventar asociación por proximidad ni confundir 21 segmentos de acceso con 21 ubicaciones. Señalamiento de ubicaciones atendidas pendiente de ese contrato; esta entrega identifica componentes y explicita el límite.

Hallazgo nuevo verificado al reproducir el grafo adjunto: BOD1 tiene 30 componentes con enlaces, cuatro nodos exclusivos de candidatos y cero aislados sin candidato. Corrige la interpretación previa de cuatro aislados reales. El reporte antiguo de 34 incluye nodos que no forman parte de la red transitable.

Pruebas: sintético con dos componentes, puente bloqueado y candidato, nodo aislado; habilitar puente une componentes. BOD1 reproduce 30 componentes y 21 segmentos de acceso. JSON conserva cobertura desconocida; render offscreen revisado visualmente. Compilación WMS correcta con advertencias existentes de dependencias/DevExpress. git diff --check correcto. Sin llamadas SQL, sin HH ni Reference.vb. Ejecutable aislado en AppData/Local/Temp/wms-exo-componentes-20261029/WMS_DEV.exe; la aplicación operativa no fue reemplazada.

Evidencia: analysis/brain-improvement/componentes-build.log, componentes-test-final/ (JSON y PNG), componentes-audit.yml. Auditoría automática WARN únicamente por consulta SQL omitida; revisión manual confirma que esta entrega no consulta ni modifica BD. No presentar el gate como PASS automático. Fuentes y hashes en sources.json.

Pendiente: validación visual en pantalla real, vínculo persistido ubicación-nodo y comprobación de cobertura operativa, antes de aprobar rutas o asignar operadores.


## Separación de repositorios — #EJC20261029_BRAIN_UBICACION

El script Test-ComponentesExoesqueleto.ps1 fue trasladado desde BOF/tools/picking a scripts/ de este handoff. Es validación operativa del Brain; no debe volver a añadirse al repositorio fuente. La ruta anterior en sources.json se conserva solo como procedencia histórica.
