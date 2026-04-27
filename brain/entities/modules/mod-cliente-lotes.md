---
id: mod-cliente-lotes
tipo: module
familia: lotes
db_principal: TOMWMS_KILLIOS_PRD
tabla_principal: cliente_lotes
estado_uso: vacio
ultima_modificacion_bd: 2025-07-13
filas_actuales: 0
casos_relacionados:
  - case-2026-04-importar-lotes-cliente
modulos_relacionados:
  - mod-importacion-excel
reglas_aplicables:
  - rule-no-fk-en-trans
archivos_dal:
  - /TOMIMSV4/DAL/Mantenimientos/Cliente/clsLnCliente_lotes.vb
archivos_entity:
  - /TOMIMSV4/Entity/Mantenimientos/Cliente/clsBeCliente_lotes.vb
archivos_form: []
sps_relacionados: []
impacto_hh: ninguno
---

# MODULE: cliente_lotes (lotes asociados a cliente)

## 1. Propósito

Permite definir, para cada combinación **cliente × producto × lote**, una regla de aceptación o **bloqueo** explícito. Modela escenarios como:

- "El cliente X exige lote ABC para el producto P" (acepta sólo lotes específicos).
- "El cliente Y no puede recibir el lote XYZ del producto P" (lote bloqueado a ese cliente).

Hoy la tabla está **vacía** — la infraestructura existe pero no hay operativa que la pueble.

## 2. Schema de tabla `cliente_lotes` (validado 2026-04-27)

| Columna | Tipo | Nullable | Notas |
|---|---|---|---|
| `IdClienteLote` | int | NO | PK clustered (`PK_cliente_lote`), identity probable |
| `IdCliente` | int | NO | FK lógica a `cliente` (no declarada en BD) |
| `IdProducto` | int | NO | FK lógica a `producto` (no declarada en BD) |
| `Lote` | nvarchar(100) | YES | Identificador textual del lote |
| `IdProductoEstado` | int | YES | FK lógica a estado del producto |
| `Activo` | bit | YES | Soft-delete |
| `Bloquear` | bit | YES | **KEY**: `true` = bloquear este lote para este cliente |
| `user_agr` / `fec_agr` | nvarchar(50) / datetime | YES | Auditoría alta |
| `user_mod` / `fec_mod` | nvarchar(50) / datetime | YES | Auditoría modificación |

### Relaciones (lógicas, no declaradas)

- **No hay FKs en BD**. Ver `rule-no-fk-en-trans`.
- **No hay SPs/vistas/triggers** que mencionen `cliente_lotes`. Toda la lógica vive en el DAL VB.NET.

## 3. Capa DAL — `clsLnCliente_lotes.vb` (existe)

**Path**: `/TOMIMSV4/DAL/Mantenimientos/Cliente/clsLnCliente_lotes.vb` (458 líneas).

### Métodos detectados

| Método | Firma | Notas |
|---|---|---|
| `Cargar` | `Public Shared Sub Cargar(ByRef oBe, ByRef dr)` | Popula entity desde DataRow, con coerción de DBNull. |
| `Insertar` | `Public Shared Function Insertar(ByRef oBe, [pConn], [pTrans]) As Integer` | INSERT con builder `Ins`. Soporta **transacción remota** (params opcionales). Si no hay transacción remota: abre conexión local con `IsolationLevel.ReadUncommitted`. |
| `Actualizar` | `Public Shared Function Actualizar(ByRef oBe, [pConn], [pTrans]) As Integer` | UPDATE con builder `Upd` y `WHERE IdClienteLote = @IdClienteLote`. Mismo patrón de transacción. |
| (otros) | — | Resto del archivo (líneas 120-458) probablemente tiene `Eliminar`, `Buscar`, `Listar*`. Confirmar en F3. |

### Patrón clave: transacción remota

Todos los métodos de escritura aceptan `pConnection`/`pTransaction` opcionales. **Cualquier import bulk debe abrir UNA transacción y pasarla a CADA `Insertar`** para garantizar atomicidad. Llamar `Insertar` sin transacción remota dentro de un loop = N transacciones independientes = no atomicidad.

## 4. Capa Entity — `clsBeCliente_lotes.vb` (existe)

**Path**: `/TOMIMSV4/Entity/Mantenimientos/Cliente/clsBeCliente_lotes.vb` (24 líneas).

```vb
Public Class clsBeCliente_lotes
    Implements ICloneable
    Public Property IdClienteLote() As Integer = 0
    Public Property IdCliente() As Integer = 0
    Public Property Lote() As String = ""
    Public Property IdProductoEstado() As Integer = 0
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Activo() As Boolean = False
    Public Property Bloquear() As Boolean = False
    Public Property IdProducto() As Integer = 0
    Sub New() : End Sub
    Public Function Clone() As Object Implements ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function
End Class
```

## 5. Capa Form — NO EXISTE

Búsqueda en árbol BOF `dev_2023_estable`: 0 archivos `frm*Cliente_lotes*.vb`. Hay que crearlo si se quiere UI.

## 6. Configuración relacionada

Estos flags afectan cómo el sistema interpreta `cliente_lotes`:

| Tabla | Flag | Significado | Estado en Killios |
|---|---|---|---|
| `cliente` | `despachar_lotes_completos` | No despacho parcial de lote | (no medido por cliente) |
| `cliente` | `control_ultimo_lote` | Validar contra último lote despachado | (no medido por cliente) |
| `producto` | `control_lote` | Producto requiere tracking de lote | **318 de 319 = 99%** |
| `producto` | `genera_lote` | Sistema autogenera lote | **0 de 319 = 0%** |

**Implicación**: prácticamente todo el catálogo de productos lleva tracking de lote. Si se implementa `cliente_lotes` operativa, afecta a casi todas las combinaciones cliente↔producto.

## 7. Estado de uso

- Tabla creada: schema modificado por última vez **2025-07-13**, hace ~9 meses.
- Filas: **0**.
- DAL+Entity: presentes y funcionales.
- Form/UI: **ausente**.
- Bulk import: **ausente**.

**Lectura**: feature diseñado e infraestructura preparada, pero nunca expuesto al usuario final. Caso D apunta a completarlo.
