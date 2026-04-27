# Pasada 3.1 — Bloque B: Entidades y Modelo de Datos

## Estado
- **Generado**: 2026-04-27T04:55:54.939Z
- **Fuente**: TOMWMS_KILLIOS_PRD (read-only) + TOMWMS_BOF/_git/TOMWMS_BOF (Azure, branch `dev_2028_merge`)
- **Cobertura**: 538 clases entity en `/TOMIMSV4/Entity/`, 345 tablas en Killios clasificadas en 10 buckets, 14 archivos entity de muestra parseados.

## Objetivo del bloque
Identificar el **modelo de objetos del WMS** (capa Entity) y mapearlo contra las **tablas físicas** de Killios. Determinar qué entidades son maestras (catálogos), cuáles son transaccionales, y cómo se relacionan con la capa DAL (clsLn) y los WebMethods del HH.

---

## B.1 Capa Entity (proyecto `/TOMIMSV4/Entity/`)

- **538 clases** con prefijo `clsBe*` (`clsBe` = "Class Business Entity").
- Distribución por prefijo: `clsBe` 529, `clsBE` 9.

### Top 12 carpetas de Entity

| Carpeta | Clases | Comentario |
|---|---:|---|
| `/TOMIMSV4/Entity/Inventario/Ciclico` | 11 | - |
| `/TOMIMSV4/Entity/Transacciones/Movimiento` | 10 | - |
| `/TOMIMSV4/Entity/Mantenimientos/Ajustes` | 9 | - |
| `/TOMIMSV4/Entity/Mantenimientos/Cliente` | 9 | Cliente + tipo + dirección + tiempos + autorizaciones (9 archivos por entidad media) |
| `/TOMIMSV4/Entity/Mantenimientos/NotificacionEvento` | 9 | Eventos del WMS para notificación a usuarios |
| `/TOMIMSV4/Entity/Transacciones/Pedido` | 9 | Encabezado + detalle + estados + reservas + log de pedido |
| `/TOMIMSV4/Entity/Mantenimientos/Cuadrilla` | 7 | Operadores agrupados + asignaciones + horarios |
| `/TOMIMSV4/Entity/FixTheBug` | 6 | Carpeta legacy de patches (revisar si está vigente) |
| `/TOMIMSV4/Entity/Mantenimientos/Reglas_Ubicacion/UbicacionSugerida` | 6 | Reglas de ubicación sugerida + parámetros |
| `/TOMIMSV4/Entity/Transacciones/Inkjet` | 6 | Impresión inkjet en línea de producción |
| `/TOMIMSV4/Entity/Log` | 5 | Entidades de logs varios |
| `/TOMIMSV4/Entity/Mantenimientos/Proveedor` | 5 | Proveedor + bodega + condiciones |

### Patrón de cada entidad (validado con 10 muestras parseadas)

Cada entidad tiene típicamente **3 archivos** (a veces hasta 9 en dominios complejos):

1. `clsBe<Nombre>.vb` — clase auto-generada con properties = columnas de la tabla SQL. Implementa `ICloneable`. Mapeo 1:1 con la tabla.
2. `clsBe<Nombre>_Partial.vb` — extensión a mano. Implementa `IDisposable`. Agrega:
   - Properties de **navegación** a entidades relacionadas (ej. `Cliente As clsBeEmpresa`, `ProductoEstado As New ...`).
   - Helpers (`IsNew As Boolean`, validaciones, helpers de lookups).
3. `clsBe<Nombre>_Lista.vb` y/o variantes (`_Filtro`, `_Resumen`, `_Vista`) — listados/filtros tipados específicos.

### Ejemplos validados

| Entidad (clsBe) | Tabla SQL | Properties auto | Properties navegación | Implements |
|---|---|---:|---:|---|
| `clsBeCliente`  | `cliente` | 31 | - | ICloneable |
| `clsBeCliente` (_Partial) | `cliente` | - | 6 | IDisposable |
| `clsBeProveedor`  | `proveedor` | 28 | - | ICloneable |
| `clsBeStock`  | `stock` | 34 | - | ICloneable |
| `clsBeStock` (_Partial) | `stock` | - | 14 | IDisposable |
| `clsBeTrans_despacho_det`  | `trans_despacho_det` | 20 | - | ICloneable |
| `clsBeTrans_despacho_enc`  | `trans_despacho_enc` | 21 | - | ICloneable |
| `clsBeTrans_pe_det`  | `trans_pe_det` | 48 | - | ICloneable |
| `clsBeTrans_pe_enc`  | `trans_pe_enc` | 71 | - | ICloneable |
| `clsBeTrans_pe_enc` (_Partial) | `trans_pe_enc` | - | 10 | IDisposable |

> Confirma mapping directo: `clsBeTrans_pe_enc` ↔ tabla `trans_pe_enc` (71 props parseadas vs 70 columnas reales en Killios — diferencia de 1 = property calculada).

---

## B.2 Tablas Killios — clasificación en 10 buckets

Sobre las **345 tablas** del catálogo Killios, las clasifiqué heurísticamente:

| Bucket | Tablas | Filas totales | Descripción |
|---|---:|---:|---|
| Sin clasificar — requiere segunda pasada (incluye stock, marcaje, etc.) | 141 | 77,741 | |
| Transaccional `trans_*` — operaciones del WMS | 84 | 424,894 | |
| Maestros de dominio (cliente, producto, bodega, etc.) | 63 | 24,068 | |
| Interfaces de integración (i_nav_*) | 25 | 65,111 | |
| Configuración + parametrización (bloque D) | 11 | 170 | |
| Catálogos pequeños (tipos) | 8 | 33 | |
| Historiales/snapshots (`_hist`, `_YYYYMMDD`) | 5 | 26,386 | |
| Sistema (sis_*) — meta del WMS | 5 | 48 | |
| Logs transaccionales `log_*` | 2 | 66,339 | |
| Backups y snapshots | 1 | 8,630 | |

### Top 15 maestras por filas

| Tabla | Filas | Rol |
|---|---:|---|
| `bodega_ubicacion` | 9,510 | Mapa de ubicaciones físicas en cada bodega (rack-tramo-posición) |
| `proveedor_bodega` | 4,594 | Asignación proveedor ↔ bodega |
| `cliente_bodega` | 3,400 | Asignación cliente ↔ bodega |
| `producto_bodega` | 1,914 | Producto habilitado en bodega + parámetros (mín, máx, sugerencia ubicación) |
| `proveedor` | 1,386 | Maestro de proveedores |
| `cliente` | 1,083 | Maestro de clientes |
| `pais_municipio` | 401 | Geografía (municipios) |
| `producto` | 319 | Maestro de productos |
| `producto_codigos_barra` | 310 | Códigos de barra alternativos por producto |
| `producto_presentacion` | 295 | Presentaciones por producto (caja, unidad, pallet) |
| `bodega_tramo` | 171 | Tramos dentro de cada rack |
| `bodega_sector` | 153 | Sectores físicos de la bodega |
| `operador_bodega` | 84 | Operadores asignados por bodega |
| `pais_departamento` | 69 | Departamentos/provincias |
| `usuario_bodega` | 63 | Usuarios habilitados por bodega |

### Top 15 "otro" — re-clasificar (mezcla de núcleo operativo)

| Tabla | Filas | Re-clasificación propuesta |
|---|---:|---|
| `t_producto_bodega` | 42,357 | Heredado (`t_*`) — producto×bodega histórico (42k, modify 2019). Posible candidato a deprecación. |
| `dh_ocupacion_bodega` | 8,144 | DataHistorial — ocupación de bodega (snapshots horarios) |
| `stock` | 4,703 | **NÚCLEO** — stock físico actual (33 cols). NO usa prefijo `trans_`. Mover a bucket `nucleo_operativo`. |
| `stock_rec` | 4,394 | Stock en proceso de recepción |
| `marcaje` | 3,701 | Registro de marcaje de operadores (entrada/salida turno) |
| `tmp_picking` | 2,951 | Tabla temporal de picking (re-construir entre runs) |
| `cod_barra_clc` | 2,432 | Cache local de códigos de barra calculados |
| `tarea_hh` | 1,817 | **NÚCLEO** — cola de tareas asignadas al HH |
| `estructura_ubicacion` | 1,448 | Estructura jerárquica de ubicaciones (rack→tramo→pos) |
| `ubicaciones_por_regla` | 1,102 | Ubicaciones generadas por reglas |
| `menu_rol` | 863 | Permisos (bloque D) |
| `stock_res` | 454 | **NÚCLEO** — reservas de stock (35 cols). Mover a `nucleo_operativo`. |
| `tmp_estructura_ubicacion` | 400 | (pendiente clasificar) |
| `tmp_stock_res` | 400 | (pendiente clasificar) |
| `menu_sistema` | 289 | (pendiente clasificar) |

> **Hallazgo importante**: el núcleo operativo del WMS (stock, stock_res, tarea_hh, marcaje) **NO usa prefijo `trans_`**. Estas tablas son tan o más críticas que las `trans_*`. Una segunda pasada del clasificador debería crear un bucket `nucleo_operativo` para ellas.

---

## B.3 Implicaciones para el Brain

1. **Mapeo Entity → Tabla es 1:1 y nombrable.** El Brain puede generar el grafo `clsBe<X>` ↔ tabla `<x>` ↔ `clsLn<X>` ↔ WMs sin LSP/AST porque el naming es regular.

2. **El proyecto Entity es "el modelo del Brain".** Las 538 clases definen el universo de objetos. Si el Brain no conoce una clase, no es parte del WMS. Cualquier WM que devuelva un tipo no presente en Entity es señal de DTO ad-hoc (revisar).

3. **Las navegaciones en `_Partial` son las relaciones del modelo.** Brain debe extraerlas para conocer joins implícitos (ej. `clsBeStock.ProductoEstado → producto_estado`).

4. **Bucket `otro` (141 tablas) es el riesgo mayor de modelo incompleto.** Las críticas (stock, marcaje, tarea_hh) están ahí. Conviene segunda pasada heurística.

5. **"FixTheBug" requiere decisión.** Hay 6 clases bajo `/Entity/FixTheBug/` — preguntar si es legacy a deprecar o parche vigente.

## Anexo: archivos generados por este bloque

| Archivo | Contenido |
|---|---|
| `data/passada-3-1-bloque-B-entity-files.json` | 538 clases entity catalogadas |
| `data/passada-3-1-bloque-B-tables-classified.json` | 345 tablas Killios en 10 buckets |
| `data/passada-3-1-bloque-B-entity-samples.json` | 10 entity parseadas (props + navegaciones) |
| `analysis/passada-3-1-bloque-B-entidades-y-modelo.md` | Este documento |