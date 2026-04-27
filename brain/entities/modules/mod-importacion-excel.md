---
id: mod-importacion-excel
tipo: module
familia: importacion
patron: staging-wide-then-promote
casos_relacionados:
  - case-2026-04-importar-lotes-cliente
modulos_relacionados:
  - mod-cliente-lotes
tablas_log:
  - log_importacion_excel
tablas_staging_ejemplo:
  - trans_bodega_ubicaciones_excel
clase_base_excel: /TOMIMSV4/TOMIMSV4/Mantenimientos/Carga_Excel/clsExcel.vb
forms_existentes:
  - frmCargaExcel.vb
  - frmCargaExcel_OC.vb
  - frmCargaExcel_DI.vb
  - frmCargaExcel_Inv_Ini_Op_Log.vb
  - frmCargaExcel_Anterior.vb
directorio: /TOMIMSV4/TOMIMSV4/Mantenimientos/Carga_Excel/
---

# MODULE: Patrón de importación Excel

## 1. Convención del sistema

TOMWMS usa un patrón consistente para todas las importaciones desde Excel: **staging "wide" → promoción a tablas reales**.

```
Excel del usuario → tabla staging wide (col1..colN nvarchar) → clase VB parsea/valida → INSERT en tabla(s) real(es) → log
```

## 2. Componentes obligatorios

### 2.1 Tabla staging "wide"

- Nombre: `<feature>_excel` (ej. `trans_bodega_ubicaciones_excel`).
- Estructura: PK identity + columnas semánticas conocidas (`cod_bodega`, `tipo_ubicacion`, etc.) + **70+ columnas genéricas** `col1`, `col2`, ..., `colN` todas `nvarchar(100)`.
- Razón del wide: el Excel se vuelca crudo, todas las columnas como string. La validación y el casting ocurren en VB.

### 2.2 Form de carga

- Path: `/TOMIMSV4/TOMIMSV4/Mantenimientos/Carga_Excel/frmCargaExcel_<Feature>.vb`.
- Templates de referencia (existentes):
  - `frmCargaExcel.vb` — genérico base.
  - `frmCargaExcel_OC.vb` — orden de compra (también opera sobre tabla con lotes — referencia más cercana al caso `cliente_lotes`).
  - `frmCargaExcel_DI.vb` — documentos de ingreso.
  - `frmCargaExcel_Inv_Ini_Op_Log.vb` — inventario inicial.
- Designer.vb + .resx asociados.

### 2.3 Clase base

- `/TOMIMSV4/TOMIMSV4/Mantenimientos/Carga_Excel/clsExcel.vb` — lectura Excel y bind a DataSet.

### 2.4 Clase de promoción (Entity/Transacciones)

- Path típico: `/TOMIMSV4/Entity/Transacciones/clsBeTrans_<feature>_excel.vb` o equivalente.
- Responsabilidad: parsea cada fila de la staging, valida (FKs lógicas, formatos), llama al DAL real (`clsLn<TablaReal>.Insertar`) **dentro de una sola transacción**.

## 3. Logging

### 3.1 Log genérico — `log_importacion_excel`

Tabla creada **2022-05-06**, hoy **vacía** (0 filas — sub-uso o limpieza periódica).

| Columna | Tipo | Notas |
|---|---|---|
| `IdImportacion` | int | PK identity |
| `IdEmpresa` | int | Multi-tenant |
| `IdBodega` | int | Bodega destino |
| `IdUsuario` | int | Quién importó |
| `hash_archivo` | nvarchar(300) | **Hash del Excel** — detecta reintento del mismo archivo |
| `fecha` | datetime | Cuándo |

**Sin SPs ni vistas que la mencionen**. Inserción 100% desde DAL VB.

### 3.2 Log dedicado opcional

Por encima del log genérico, cada feature puede tener su propia clase `clsBeLog_importacion_excel_<feature>.vb` para datos específicos del proceso (rejected rows, conteos, errores tipados).

## 4. Cómo crear un nuevo "importar X desde Excel"

1. **Crear tabla staging** `<X>_excel` en repo `DBA` (mismo patrón que `trans_bodega_ubicaciones_excel`).
2. **Crear form** `frmCargaExcel_<X>.vb` copiando la variante más parecida (ej. `_OC` para algo con lotes/productos).
3. **Crear clase de promoción** que:
   - Lee filas de la staging.
   - Valida cada fila (FKs lógicas, business rules).
   - **Abre UNA transacción** y la pasa a cada `Insertar` del DAL real.
   - Inserta una fila en `log_importacion_excel` con `hash_archivo`.
   - Hace `Commit` o `Rollback` atómico.
4. **Validar duplicados** consultando `log_importacion_excel` por `hash_archivo` antes de procesar.

## 5. Anti-patterns a evitar

- ❌ Insertar fila por fila sin transacción remota → N transacciones → no atomicidad.
- ❌ Procesar Excel directamente sin staging → no hay re-procesamiento ni auditoría.
- ❌ Inventar nombre de tabla staging fuera del patrón `<feature>_excel`.
- ❌ Saltarse `log_importacion_excel` → no se detectan reintentos del mismo archivo.
