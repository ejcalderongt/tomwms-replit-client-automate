# L-035 — META: en TOMWMS NO se codifica por cliente, TODO se parametriza

> Etiqueta: `L-035_META_NO-CODIFICAR-POR-CLIENTE_APPLIED`
> Fecha: 30-abr-2026
> Origen: Wave 11, respuesta Carolina al Q-CAPABILITY-FLAG (Bloque 7 cuestionario)
> Severidad: CRITICA — afecta como leer todo el codigo del WMS

## Hallazgo

**No existen `if Cliente.Codigo = "BYB" Then ... End If` esparcidos por el codigo.**

Cuando aparezca una funcionalidad que se comporta distinto segun el cliente, hay que buscar el **parametro** que la controla, no el codigo del cliente. La diferencia siempre vive en una de tres tablas de configuracion.

## Cita literal Carolina (Wave 11)

> "No codificamos por cliente, todo lo parametrizamos segun corresponda
> por la funcionalidad que vayamos a incorporar al WMS, las tablas que
> mas parametrizamos son Bodega, Producto, i_nav_config_enc que es la
> configuracion de la interface."

## Las tres tablas de parametrizacion principales

| Tabla | Que parametriza | Granularidad |
|---|---|---|
| `Bodega` | comportamiento operativo por bodega (horarios, flags de proceso, ej. `horario_ejecucion_historico` para stock_jornada) | por bodega |
| `Producto` | comportamiento por producto (controla licencias, tipos, etiquetas, etc.) | por producto |
| `i_nav_config_enc` | configuracion de la Interface ERP (que campos se mapean, que documentos sincronizan, hacia donde) | por instancia de Interface |

Tambien se confirmo en Wave 11 (L-038, Q34):
- `Empresa.generar_stock_jornada` (bandera) controla si stock_jornada se cierra diariamente.
- `producto_presentacion.IdTipoEtiqueta` (L-043) controla diseño de etiqueta por presentacion.

Es decir: **la familia de tablas parametricas es mas amplia** — Bodega, Producto, i_nav_config_enc son las TRES principales pero hay banderas en `Empresa`, `producto_presentacion`, etc.

## Implicaciones para el agente

1. **Buscar parametros, no clientes.** Si el sintoma es "en BYB se hace X pero en MAMPA no", la causa NO es un IF por cliente. Es una bandera en Bodega, en Producto, o en i_nav_config_enc, o en Empresa, o en una tabla parametrica especifica del modulo.
2. **Antes de proponer un fix tipo `if Cliente.Codigo = "X"`**, frenar. Eso violaria el patron arquitectonico del WMS. Proponer un parametro nuevo en la tabla correspondiente.
3. **Al leer codigo del BOF, HH o WMSWebAPI**, asumir que cualquier comportamiento variable se resuelve consultando una de estas tablas. Buscar el `Select` o el `EXEC sp_config_*` correspondiente.
4. **Excepcion conocida**: el prefijo `Nav` en clases/tablas (L-025) NO es codificacion por cliente, es nomenclatura historica de "Interface ERP".

## Q-* abiertas relacionadas

- Q-PARAMETROS-CATALOGO (nueva, abrir en bloque 14): listar todos los parametros relevantes en Bodega, Producto, i_nav_config_enc, Empresa. ¿Existe documentacion interna del catalogo de parametros? ¿O hay que reconstruirlo leyendo los `Select` del codigo?
- Q-PARAMETROS-DEFAULTS (nueva): cuando se da de alta una bodega/producto/empresa nueva, ¿que defaults se aplican? ¿Hay seed scripts versionados?

## Archivos del brain a actualizar (paso 2 Wave 11)

- `_index/INDEX.md`: agregar este learning como FUNDAMENTAL (al lado de L-025).
- `agent-context/AGENTS.md`: agregar regla "no proponer codigo IF por cliente, siempre parametrizar".
- `_conventions/`: si existe convencion de "como agregar funcionalidad nueva", agregar paso "1. ¿Que tabla parametrica controla esto?".

## Vinculos cruzados

- L-025: el prefijo `Nav` es historico, no por cliente.
- L-038 (Wave 11): `empresa.generar_stock_jornada` es ejemplo concreto de este patron.
- L-043 (Wave 11): `producto_presentacion.IdTipoEtiqueta` para licencias MHS, otro ejemplo.
- L-031 (Wave 10): "master congelada" — explica por que la nomenclatura no se cambia retroactivamente.
