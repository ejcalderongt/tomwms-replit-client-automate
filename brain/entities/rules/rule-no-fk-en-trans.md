---
id: rule-no-fk-en-trans
tipo: rule
ambito: BD-completa
severidad: critica
fecha_descubierta: 2026-04-27
metodo_validacion: queries-sys-foreign_keys
modulos_afectados:
  - mod-cliente-lotes
  - mod-importacion-excel
casos_relacionados:
  - case-2026-04-importar-lotes-cliente
---

# RULE: Sin FKs declaradas — integridad por DAL

## 1. Regla

**Las tablas de TOMWMS_KILLIOS_PRD no tienen foreign keys declaradas a nivel BD.** La integridad referencial la garantiza el DAL VB.NET (`clsLn*`), no el motor SQL Server.

## 2. Evidencia (validada 2026-04-27)

Consultas sobre `sys.foreign_keys` para tablas representativas:

| Tabla | FKs entrantes | FKs salientes |
|---|---|---|
| `cliente_lotes` | 0 | 0 |
| `trans_oc_det_lote` | 0 | 0 |

Ningún SP/vista/función toca estas tablas (`sys.sql_expression_dependencies` vacío). Toda la lógica vive en VB.

## 3. Implicaciones operativas

### 3.1 Para inserciones nuevas (caso típico: import bulk Excel)

- **Validar FKs lógicas en VB ANTES del INSERT**: `IdCliente` existe en `cliente`, `IdProducto` existe en `producto`, etc.
- **No confiar en errores de motor**: el INSERT no va a fallar por FK violada — va a entrar basura silenciosamente.
- **Cualquier import masivo (Excel, API, SP nuevo) debe replicar las validaciones del DAL**, o llamar al DAL existente fila por fila dentro de una transacción.

### 3.2 Para queries analíticas

- **`JOIN` libre**: no hay garantía de matching. Usar `LEFT JOIN` defensivamente y verificar `IS NULL` en el lado derecho para detectar huérfanos.
- **Conteos por relación**: pueden no cuadrar con el padre.

### 3.3 Para refactors de schema

- **Renombrar/eliminar columna del padre NO falla en hijos**: el motor no avisa. Hay que buscar referencias en código VB manualmente (o con F3 cuando esté).
- **Cambio de tipo en PK del padre**: idem, el hijo queda inconsistente sin warning.

### 3.4 Para tests/sandbox

- **Borrar fila de `cliente`** no falla aunque haya `cliente_lotes` huérfanos. Hay que limpiar manualmente o vía script.

## 4. Excepción / matiz

- **PKs sí están declaradas** (verificado: `PK_cliente_lote` clustered en `cliente_lotes`).
- **Constraints `UNIQUE` o `CHECK`** pueden existir tabla por tabla — no se asume su ausencia universalmente.
- **Triggers** podrían existir y suplir parcialmente la integridad — verificar con `sys.triggers` antes de asumir nada.

## 5. Antipatterns

- ❌ Confiar en "el motor va a tirar error de FK" — no va a pasar.
- ❌ Saltarse el DAL "para ir más rápido" en un bulk insert — la integridad se rompe sin aviso.
- ❌ Asumir que `SELECT count(*) FROM hija WHERE IdPadre IN (SELECT IdPadre FROM padre)` siempre cuadra con `SELECT count(*) FROM hija`.

## 6. Patrón correcto

```vb
' Bulk import siempre así:
Dim conn As New SqlConnection(CST)
conn.Open()
Dim trans = conn.BeginTransaction(IsolationLevel.ReadUncommitted)
Try
    For Each row In stagingRows
        ' Validar FKs lógicas ANTES
        If Not ClienteExiste(row.IdCliente) Then Throw ...
        If Not ProductoExiste(row.IdProducto) Then Throw ...
        ' Llamar DAL pasando la transacción
        clsLnCliente_lotes.Insertar(beEntity, conn, trans)
    Next
    trans.Commit()
Catch ex As Exception
    trans.Rollback()
    Throw
Finally
    conn.Close()
End Try
```
