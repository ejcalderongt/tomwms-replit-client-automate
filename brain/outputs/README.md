# brain/outputs/

Carpeta raiz para entregables formateados producidos por el agente brain.

## Taxonomia

| Subcarpeta | Tipo de output | Audiencia |
|---|---|---|
| `guias-implementacion/` | Guias tecnicas con SQL, codigo, contratos. Sufijo de version `-V0`, `-V1`. | Programador / equipo de desarrollo |
| `respuestas-cliente/` | Documentos cara al cliente (Carol, etc.) con saludos y tono didactico. | Usuario funcional / cliente |
| `consolidaciones-pasada/` | Cierre de cada pasada de analisis: hallazgos, decisiones, anexos. | Erik + equipo (interno) |
| `drafts-respuestas/` | Borradores de respuestas a personas o casos especificos antes de enviar. | Interno |
| `reportes-datos/` | Cuando se haga afinidad de datos: CSV/MD con stats por cliente. | Erik + cliente segun caso |

## Convenciones

- Cada archivo lleva header YAML al principio con: `output_type`, `audience`, `version`, `status`, `authored_by`, `authored_at`.
- Estados validos en `status`: `draft`, `ratificado`, `obsoleto`.
- Cuando un output queda obsoleto, NO se borra. Se cambia `status: obsoleto` y se agrega `superseded_by: <archivo nuevo>`.
- Las versiones se numeran V0, V1, V2... y se preservan todas para trazabilidad.
- Cross-references en el cuerpo apuntan a paths relativos a la raiz del repo.

## Diferencia con `wms-specific-process-flow/`

`wms-specific-process-flow/` queda como zona de notas raw, mapas, hipotesis, queries de pasada y bug reports - material de trabajo. `outputs/` es solo material PRODUCIDO Y ENTREGABLE.
