# data-seek-strategy

Metodología re-usable para investigar data en bases de datos TOMWMS .NET. Independiente del cliente. Documenta los caminos, queries, anti-patrones y herramientas que probamos en casos reales (CP-001 en adelante) y que demostraron servir.

## Filosofía

1. **Primero entender, después extraer.** Antes de escribir un SELECT, leer la sección de tablas clave (`02-tablas-clave-tomwms.md`) y descartar columnas trampa.
2. **Read-only siempre.** Conectarse con un usuario que SOLO pueda leer. Si por alguna razón hay que escribir, se hace por separado y con DBA del cliente avisado.
3. **CSVs versionados.** Toda extracción que se publique en el brain va a `outputs/wave-NN/` del caso correspondiente. Nunca pegamos data sensible en chat.
4. **Cuadre matemático antes que narrativa.** Las hipótesis se cierran con tablas (recepciones − despachos − ajustes = saldo BD esperado), no con prosa.
5. **Buscar el patrón histórico.** Cualquier bug nuevo probablemente tiene precedentes en `trans_ajuste_enc/det`. Cruzar siempre.
6. **Detector de outliers obligatorio.** Antes de agregar cantidades, correr el `audit-overflow.py` para descartar registros corruptos (ej: cantidades en billones, fechas absurdas).

## Índice

| Doc                                    | Tema                                                                  |
|----------------------------------------|-----------------------------------------------------------------------|
| `01-setup-conexion-bd.md`              | Cómo conectarse via pymssql con env vars, qué nunca hacer             |
| `02-tablas-clave-tomwms.md`            | Catálogo de tablas críticas con cols importantes y trampas comunes    |
| `03-flujos-investigacion.md`           | Recetas: cuadre lote, dañados, ajustes históricos, picking trace      |
| `04-anti-patrones-y-trampas.md`        | `cantidad_hist != cantidad`, snapshots, AJCANTN no aplica a dañados, etc |
| `05-deteccion-anomalias.md`            | Overflows, outliers, gaps temporales, fechas inválidas                |
| `06-glosario-tipos-tarea-y-motivos.md` | RECE/UBIC/PIK/DESP/INVE/AJCANTN/CEST + motivos ajuste                 |
| `templates/_db.py`                     | Conexión RO con env vars, helper `q()` que devuelve dicts             |
| `templates/audit-overflow.py`          | Detector de cantidades anómalas en tablas con columna numérica        |
| `templates/audit-gaps-temporales.py`   | Detector de huecos de actividad (ej: gap 2.5 años en ajustes)         |
| `templates/audit-saldo-neto.py`        | Stock vs reservas por producto, encuentra negativos                   |
| `templates/audit-danados-sin-ajuste.py`| Generaliza el bug de CP-013 a cualquier cliente                       |
| `templates/csv_helpers.py`             | Helper `csv_out(name, rows)` consistente entre scripts                |

## Cómo usar en un caso nuevo

1. Leer `01-setup-conexion-bd.md` y configurar credenciales del cliente.
2. Copiar templates relevantes a `brain/debuged-cases/<CASO>/queries/`.
3. Personalizar y ejecutar. Salidas a `outputs/wave-NN/`.
4. Si descubrís una trampa nueva o un patrón nuevo, **actualizar `04-anti-patrones-y-trampas.md`** y/o `02-tablas-clave-tomwms.md`. La doc es viva.
5. Si es un cliente nuevo, crear `client-index/<cliente>.yml` con sus particularidades (factor caja, módulos no usados, usuarios relevantes, outliers conocidos).

## Casos que alimentaron esta doc

- **CP-013 KILLIOS WMS164** (2026-04): bug `dañado_picking sin descuento`, descubrimiento de >320k UM fantasma transversales, gap histórico 2.5 años en ajustes, outlier corrupto idajustedet=638 (4.8B UM).

(Agregar siguiente caso al cerrar.)
