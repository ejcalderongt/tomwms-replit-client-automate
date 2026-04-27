---
id: case-2026-04-importar-lotes-cliente
tipo: case
fecha_apertura: 2026-04-26
fecha_actualizacion: 2026-04-27
estado: en-analisis
prioridad: media
solicitante: Erik Calderón (PrograX24)
rama_objetivo: dev_2023_estable
clientes_alcance: [Killios]
interpretacion_elegida: D
sub_aristas_pendientes: [D.1, D.2]
modulos_relacionados:
  - mod-cliente-lotes
  - mod-importacion-excel
reglas_relacionadas:
  - rule-no-fk-en-trans
decisiones_relacionadas:
  - dec-2026-04-killios-acceso-replit
tablas_sql:
  - cliente_lotes
  - cliente
  - producto
  - log_importacion_excel
archivos_codigo_existentes:
  - /TOMIMSV4/DAL/Mantenimientos/Cliente/clsLnCliente_lotes.vb
  - /TOMIMSV4/Entity/Mantenimientos/Cliente/clsBeCliente_lotes.vb
  - /TOMIMSV4/TOMIMSV4/Mantenimientos/Carga_Excel/clsExcel.vb
  - /TOMIMSV4/TOMIMSV4/Mantenimientos/Carga_Excel/frmCargaExcel.vb
archivos_codigo_a_crear_estimado:
  - /TOMIMSV4/TOMIMSV4/Mantenimientos/Carga_Excel/frmCargaExcel_Cliente_Lotes.vb (TBD)
  - /TOMIMSV4/Entity/Mantenimientos/Cliente/clsBeCliente_lotes_excel.vb (TBD)
  - tabla SQL: cliente_lotes_excel (staging wide, TBD)
impacto_hh: ninguno
---

# CASE: Importar lotes por cliente desde Excel

## 1. Contexto

Erik solicitó "importar lotes desde Excel" en `dev_2023_estable`. Tras analizar el modelo real de Killios y el código existente, se identificaron 5 interpretaciones posibles. Erik confirmó la **interpretación D**: importación masiva de la relación **cliente ↔ producto ↔ lote** (con flag de bloqueo), que pobla la tabla `cliente_lotes`.

## 2. Modelo SQL afectado (validado 2026-04-27)

### Tabla destino: `cliente_lotes`

- **Estado de uso**: vacía (0 filas).
- **Última modificación de schema**: 2025-07-13 (relativamente reciente — feature joven, infraestructura preparada pero sin uso productivo).
- **PK**: `PK_cliente_lote` clustered en `IdClienteLote` (single column, identity probable).
- **FKs declaradas**: NINGUNA (entrantes ni salientes). Integridad por DAL (ver `rule-no-fk-en-trans`).
- **SPs/vistas que la mencionan**: 0. Toda la lógica vive en VB.NET.

### Tablas de contexto

- **`cliente`** (1083 filas) — flags relevantes: `despachar_lotes_completos` (bit), `control_ultimo_lote` (bit). **No hay** flag "controla cliente_lotes" — la inscripción se infiere por presencia de filas para ese cliente.
- **`producto`** (319 filas) — flags relevantes: `control_lote` (bit) y `genera_lote` (bit). **318 de 319 productos tienen `control_lote=1`** (99%); **0 productos tienen `genera_lote=1`** → el lote nunca se autogenera, siempre se ingresa manual.
- **`log_importacion_excel`** (vacía, schema desde 2022-05-06) — log genérico de imports con `hash_archivo` para deduplicación.

Detalle completo: ver `mod-cliente-lotes` y `mod-importacion-excel`.

## 3. Código existente en BOF (rama `dev_2023_estable`)

### Capa DAL/Entity de `cliente_lotes` — YA EXISTE

| Archivo | Líneas | Rol |
|---|---|---|
| `/TOMIMSV4/Entity/Mantenimientos/Cliente/clsBeCliente_lotes.vb` | 24 | Clase entity con 11 properties (IdClienteLote, IdCliente, Lote, IdProductoEstado, IdProducto, User_agr/Fec_agr/User_mod/Fec_mod, Activo, **Bloquear**). Implementa `ICloneable`. |
| `/TOMIMSV4/DAL/Mantenimientos/Cliente/clsLnCliente_lotes.vb` | 458 | DAL con métodos `Cargar`, `Insertar`, `Actualizar` y otros. Soporta transacción remota (param opcional `pConnection`/`pTransaction`). Usa `IsolationLevel.ReadUncommitted` en transacciones locales. |

**Hallazgo clave**: existe DAL+Entity completos pero **NO existe form** (`frm*Cliente_lotes*.vb`) ni clase de import. La infraestructura está lista, falta UI + bulk-import.

### Patrón Carga_Excel — YA EXISTE como template

Directorio `/TOMIMSV4/TOMIMSV4/Mantenimientos/Carga_Excel/` (20 archivos):

- `clsExcel.vb` — clase base de lectura Excel.
- `frmCargaExcel.vb` — form genérico.
- `frmCargaExcel_OC.vb` — variante orden de compra (referencia más cercana al caso D, también opera sobre tabla con lotes).
- `frmCargaExcel_DI.vb` — variante documentos de ingreso.
- `frmCargaExcel_Inv_Ini_Op_Log.vb` — variante inventario inicial.
- `frmCargaExcel_Anterior.vb` — legacy.

**No existe** `frmCargaExcel_Cliente_Lotes.vb` ni `frmCargaExcel_Lotes.vb` — son los archivos a crear.

## 4. Cross-impact

| Capa | Impacto | Detalle |
|---|---|---|
| **BOF DAL** | Sin cambio | `clsLnCliente_lotes` ya tiene `Insertar`. |
| **BOF Entity** | Sin cambio | `clsBeCliente_lotes` ya tiene todos los properties. |
| **BOF Form** | **CREAR** | Nuevo `frmCargaExcel_Cliente_Lotes.vb` siguiendo patrón de `frmCargaExcel_OC.vb`. |
| **BOF tabla staging** | **CREAR** | Nueva tabla `cliente_lotes_excel` wide (`col1..colN` nvarchar(100)) en repo `DBA`. |
| **BOF clase import** | **CREAR** | Nueva `clsBeTrans_cliente_lotes_excel.vb` o equivalente en Entity/Transacciones. |
| **BOF logging** | Reusar | `log_importacion_excel` existente (con `hash_archivo`). Opcional: `clsBeLog_importacion_excel_cliente_lotes.vb`. |
| **HH (TOMHH2025)** | **NINGUNO** | Búsqueda en árbol HH: 0 referencias a `cliente_lote`/`ClienteLote`. La operativa de lotes en HH va por otras tablas (`trans_re_det_lote_num`, `trans_oc_det_lote`). |
| **WS** | **NINGUNO** | No hay WebMethod de cliente_lotes detectado. Operación 100% backoffice. |
| **SAPSYNC / Navision** | **NINGUNO esperado** | Ninguna interfaz `i_nav_*` toca `cliente_lotes`. |

## 5. Sub-aristas (a confirmar con Erik en F4)

### D.1 — Bulk import inicial
Cargar lotes históricos cliente↔producto desde Excel para poblar `cliente_lotes` por primera vez con datos legacy. Implica:
- Form de carga + validación.
- ¿Reglas: lotes que el cliente recibió alguna vez? ¿lotes bloqueados explícitamente? ¿ambos con flag `Bloquear`?
- ¿Productos validados contra `producto.control_lote=1`? (318 de 319 cumplen).

### D.2 — Setup operativo nuevo
Definir cuándo se invoca la tabla en el flujo. La presencia de `cliente_lotes` con `Bloquear=true` debería:
- Bloquear despacho del lote a ese cliente.
- ¿Se valida en HH al picking? ¿En BOF al armar pedido? ¿Ambos?
- Implica tocar lógica de despacho (`trans_despacho_*`) y posiblemente HH.

## 6. Próximos pasos

1. **F1 (este commit)**: documentar caso, módulos, reglas y decisión arquitectural.
2. **F2** (sql-catalog): generar entity SQL de `cliente_lotes` + tablas relacionadas con datos vivos.
3. **F3** (code-index): mapear automáticamente las referencias VB y generar `usos_codigo` en el módulo.
4. **F4** (`wms-brain ask`): correr "qué hago para implementar D.1" y validar que la respuesta agregada sea correcta vs este análisis manual.
5. **Cuando se decida implementar**: pedir a Erik decisión D.1 vs D.2 (o ambas en orden), y armar bundle para openclaw.
