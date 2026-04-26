# wms-tomwms — Convenciones de código

Referencia rápida al escribir VB, Java o SQL nuevo en el WMS. Complemento de `SKILL.md`.

## 1. Migración XML → JSON (Forma A)

**Política**: oportunista. Métodos legacy estables NO se migran. Funcionalidad nueva → JSON por defecto.

### Estructura del wrapper

Toda respuesta JSON del backend al HH (o al cliente web) usa un único contrato:

```json
{
  "data": { ... },
  "error": null
}
```

Si hay error:

```json
{
  "data": null,
  "error": {
    "code": "VALIDATION_FAILED",
    "message": "El producto no tiene stock disponible",
    "detail": "stock=0 en ubicacion=A-01-02"
  }
}
```

### Implementación VB.NET

```vb.net
Imports System.Web.Script.Serialization

<WebMethod()>
Public Function GetStockJson(ByVal pCodigo As String) As String
    Dim ser As New JavaScriptSerializer()
    Try
        Dim datos = ConsultarStockInterno(pCodigo)
        Return ser.Serialize(New With {
            .data = datos,
            .error = Nothing
        })
    Catch ex As Exception
        Return ser.Serialize(New With {
            .data = Nothing,
            .error = New With {
                .code = "INTERNAL",
                .message = ex.Message,
                .detail = ex.StackTrace
            }
        })
    End Try
End Function
```

**Por qué `JavaScriptSerializer` y no `Newtonsoft.Json`**:

- Forma parte del .NET Framework base (`System.Web.Extensions`). No agrega dependencia.
- Es el que ya está usado en el resto del backend; mantener consistencia.
- Si en el futuro se migra el módulo a .NET Core / .NET 8, ahí sí Newtonsoft o `System.Text.Json`.

### Status codes

- **200** siempre que la respuesta sea sintácticamente válida, **incluso si `error != null`**. El error de negocio va en el wrapper, no en el HTTP status.
- **500** solo para errores de infraestructura (BD caída, deserialización rota antes de poder armar el wrapper).

Esto facilita que el cliente HH/web procese la respuesta uniformemente sin inspección adicional del HTTP status.

### Contrato HH → WS

El HH manda un único string JSON como parámetro del SOAP envelope:

```java
SoapObject request = new SoapObject(NAMESPACE, "GetStockJson");
String payload = new Gson().toJson(new GetStockReq("ABC123"));
request.addProperty("pCodigo", payload);  // o el nombre que use el método
```

El backend deserializa con `JavaScriptSerializer.Deserialize<TReq>(pCodigo)`.

---

## 2. Encoding y caracteres

- **VB**: UTF-8 **con BOM**. VS lo respeta. Verificar con `file <archivo>` antes de commit.
- **Java HH**: UTF-8 sin BOM (default Android Studio). Eliminar cualquier `String.replace("ñ","n")` o equivalente.
- **SQL scripts**: UTF-8 sin BOM. SSMS guarda con BOM por default — corregir si es necesario.
- **Strings con `ñ`, acentos, comillas tipográficas**: respetar. La HH y la BD ya manejan UTF-8.

---

## 3. Naming

### VB.NET

| Tipo | Convención | Ejemplo |
|---|---|---|
| Clase | `cls<Nombre>` (legacy) o `<Nombre>` (nuevo) | `cls_DBSql`, `clsLnAjuste` |
| Form | `frm<Nombre>` | `frmAjusteStock` |
| WebMethod | `<Verbo><Sustantivo>[Json]` | `GetStockJson`, `SaveAjuste` |
| Variable local | camelCase con prefijo de tipo Hungarian-light | `pCodigo`, `lstStock`, `iCantidad`, `dFecha` |
| Constante | `UPPER_SNAKE` | `MAX_RETRIES` |
| SP llamado desde DAL | método del DAL refleja el SP: `Get_Stock_Actual` → `GetStockActual()` |

### SQL

| Tipo | Convención |
|---|---|
| Tabla | minúsculas con `_`: `trans_ajuste`, `stock_lote` |
| Vista | `VW_<Pascal>`: `VW_Stock_Res`, `VW_Despachos_Pendientes` |
| SP | `<Verbo>_<Sustantivo>`: `Get_Stock_Actual`, `Save_Ajuste` |
| Función | `fn_<nombre>` |
| Trigger | `tr_<tabla>_<evento>` |

### Java (HH)

| Tipo | Convención |
|---|---|
| Activity | `<Nombre>Activity` |
| Adapter | `<Nombre>Adapter` |
| Modelo | `<Nombre>` (Pascal) |
| Constante | `UPPER_SNAKE` |

---

## 4. Formato de commits

### TOMWMS_BOF (VB.NET)

```
#EJCRP <tipo>(<área>): <mensaje corto>

Descripción opcional.
```

`<tipo>`: `fix`, `feat`, `refactor`, `chore`, `doc`.
`<área>`: `frmAjuste`, `WSHHRN`, `DAL`, etc.

### TOMHH2025 (Java)

```
#GT_DDMMAAAA: <mensaje corto>
```

Reemplazar `GT` por las iniciales del autor (ver §3 del SKILL).

### Bundles del repo de intercambio

```
[v23] <descripción corta>

Detalle opcional.
```

---

## 5. SQL — patrones permitidos vs prohibidos

### OK

- `SELECT` con `JOIN`, `WITH (NOLOCK)` cuando es lectura concurrente.
- SPs con `BEGIN TRY / BEGIN CATCH` y `THROW;` (no `RAISERROR` legacy).
- Transacciones explícitas en SPs que tocan más de una tabla.

### Prohibido sin OK explícito

- `DROP TABLE`, `ALTER TABLE` (cambio de tipo de columna).
- `TRUNCATE` en producción.
- Cursors anidados (`DECLARE CURSOR ... DECLARE CURSOR`).
- `sp_executesql` con queries dinámicas que concatenen input del usuario sin parametrizar.

---

## 6. Manejo de errores

### VB.NET (legacy)

```vb.net
Try
    ' lógica
Catch ex As Exception
    ' loguear via clsLog (o equivalente del módulo)
    ' devolver error al caller, NUNCA tragarlo silencioso
    Throw  ' o return wrapper {error} si es WebMethod JSON
End Try
```

### Java HH

```java
try {
    // lógica
} catch (Exception e) {
    Log.e(TAG, "Operación X falló", e);
    // mostrar Toast o dialog al operario, NUNCA fallar silencioso
}
```

### Anti-patterns

- `Catch ex As Exception\n    ' nada` (tragar excepciones).
- `On Error Resume Next` (legacy VB6, prohibido en código nuevo).
- Devolver `null` sin logging cuando el caller no puede distinguir "no encontrado" de "error".

---

## 7. Logging

Usar el wrapper de logging del proyecto (`clsLog` o equivalente), NO `Console.WriteLine` ni `System.out.println` en código de producción. Niveles:

- `Debug`: solo en build dev, no en producción.
- `Info`: operaciones de negocio relevantes (ajuste guardado, despacho confirmado).
- `Warn`: situaciones recuperables que merecen atención.
- `Error`: excepciones, fallos de operación.

**Nunca loguear**: passwords, tokens, JWT, payloads completos con datos sensibles del cliente.
