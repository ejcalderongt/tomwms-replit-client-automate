# WmsBodegaApi — POC de Remote DAL via WebAPI 4.8

Wrapper HTTP/JSON sobre `TOMWMS.clsLnBodega` (la DAL existente, sin tocar nada).
Sirve para **medir y comprender** la diferencia entre el patrón actual (WinForms
abre `SqlConnection` directo a la BD remota) vs. el patrón Remote DAL (WinForms
llama a una WebAPI hosteada **al lado de la BD**, que internamente usa la misma
DAL en local).

> Stack: **VB.NET .NET Framework 4.8 + ASP.NET Web API 2** — mismo target que tu
> solución TOMIMSV4. **Cero cambios** en `DAL.vbproj`, `Entity.vbproj` ni en
> `frmBodega.vb`. Reutiliza `clsBeBodega` y `clsLnBodega` tal como están hoy.

---

## 1. Qué hay acá

```
WmsBodegaApi/
  WmsBodegaApi.sln
  WmsBodegaApi.WebApi/
    WmsBodegaApi.WebApi.vbproj          ← Web API 2, .NET 4.8, refs a DAL+Entity+AppGlobal
    Web.config                           ← appSettings CST (mismo que el WMS) + ApiKey
    Global.asax + Global.asax.vb         ← bootstrap
    packages.config                      ← Microsoft.AspNet.WebApi 5.2.9 + Newtonsoft.Json 13
    App_Start/
      WebApiConfig.vb                    ← routing + JSON formatter (camelCase, Forma A)
    Controllers/
      BodegaController.vb                ← 8 endpoints que mapean los 8 métodos LN del frm
    Dtos/
      ApiResponse.vb                     ← wrapper Forma A {data, error}
      BodegaDto.vb                       ← espejo de clsBeBodega (60+ props)
      BodegaMapper.vb                    ← clsBeBodega <-> BodegaDto (1:1 explícito)
    Filters/
      JsonExceptionFilter.vb             ← exceptions → Forma A consistente
    My Project/
      AssemblyInfo.vb
```

---

## 2. Mapa: qué reemplaza cada endpoint del flujo actual de `frmBodega`

| # | Línea de `frmBodega.vb` | Llamada legacy directa a la DAL | Endpoint nuevo (Remote) |
|---|---|---|---|
| 1 | 290, 1383 | `clsLnBodega.MaxID()` | `GET /api/bodega/maxid` |
| 2 | 942 | `clsLnBodega.Obtener(pBeBodega)` | `GET /api/bodega/{idBodega}` |
| 3 | 1469 | `clsLnBodega.Exists_By_IdEmpresa(idEmpresa)` | `GET /api/bodega/exists/{idEmpresa}` |
| 4 | 1568 | `clsLnBodega.Insertar(pBeBodega)` | `POST /api/bodega` (body: `BodegaDto`) |
| 5 | 1815, 2176, 2257 | `clsLnBodega.Actualizar(pBeBodega)` | `PUT /api/bodega/{idBodega}` (body: `BodegaDto`) |
| 6 | 4621 | `clsLnBodega.Inserta_Ubicaciones_Por_Defecto(idB, idU)` | `POST /api/bodega/{idBodega}/ubicaciones-defecto/{idUsuario}` |
| 7 | 5115, 5141 | `clsLnBodega.Habilitar_Reemplazo(idB, True/False)` | `POST /api/bodega/{idBodega}/habilitar-reemplazo` (body: `{habilitar:bool}`) |
| 8 | 4891 | `clsLnBodega.Unificar_Bodegas(...)` | `POST /api/bodega/unificar` (body: `UnificarBodegasRequest`) |

Todas las respuestas son **Forma A** (regla del proyecto, ver `replit.md` §4.2):

```json
// Éxito
{ "data": { ... }, "error": null }
// Error
{ "data": null, "error": { "code": "NOT_FOUND", "message": "...", "details": "..." } }
```

---

## 3. Cómo importar el proyecto a tu Solution actual del WMS

### Opción A — `git subtree` o copia directa

1. Copiar la carpeta `WmsBodegaApi/` dentro del repo `TOMWMS_BOF` (al lado de `TOMIMSV4/`, por ejemplo).
2. En tu Solution principal de Visual Studio (`TOMIMSV4.sln` o equivalente):
   - Click derecho en la Solution → **Add → Existing Project** → seleccionar `WmsBodegaApi.WebApi.vbproj`.
3. Editar `WmsBodegaApi.WebApi.vbproj`: en el bloque `<ItemGroup>` que tiene los `<Reference Include="DAL">`, `<Reference Include="Entity">` y `<Reference Include="AppGlobal">`, **reemplazar por `<ProjectReference>`** apuntando a tus proyectos reales de la Solution. Algo así:

   ```xml
   <ProjectReference Include="..\..\TOMIMSV4\DAL\DAL.vbproj">
     <Project>{GUID-DEL-DAL}</Project>
     <Name>DAL</Name>
   </ProjectReference>
   <ProjectReference Include="..\..\TOMIMSV4\Entity\Entity.vbproj">
     <Project>{GUID-DEL-ENTITY}</Project>
     <Name>Entity</Name>
   </ProjectReference>
   <ProjectReference Include="..\..\AppGlobal\AppGlobal.vbproj">
     <Project>{GUID-DEL-APPGLOBAL}</Project>
     <Name>AppGlobal</Name>
   </ProjectReference>
   ```

   (alternativa: dejar los `<Reference>` con `HintPath` apuntando a los DLLs ya compilados de tu proyecto WMS).
4. Click derecho sobre `WmsBodegaApi.WebApi` → **Manage NuGet Packages** → restaurar (`Microsoft.AspNet.WebApi 5.2.9` y `Newtonsoft.Json 13.0.3`).
5. Build de la Solution.

### Opción B — Solution standalone (recomendado para arrancar)

Abrir directamente `WmsBodegaApi.sln` en VS, ajustar las 3 referencias DLL a la ruta correcta de tus binarios compilados, restaurar NuGet, build.

---

## 4. Configuración (`Web.config`)

Antes de correr:

```xml
<appSettings>
  <add key="CST" value="Server=...;Database=TOMWMS_KILLIOS_PRD;User Id=sa;Password=...;TrustServerCertificate=True;" />
  <add key="ApiKey" value="poc-bodega-key-cambiar" />
</appSettings>
```

- `CST` = exactamente el mismo connection string que la DAL ya lee
  (`Configuration.ConfigurationManager.AppSettings("CST")` en `clsLnBodega.Insertar:147`).
- `ApiKey` = secret simple de prueba. El controller exige header `X-API-Key`.

---

## 5. Cómo correrlo localmente

1. Build OK en VS.
2. Click derecho `WmsBodegaApi.WebApi` → **View → View in Browser** (lanza IIS Express en `http://localhost:59800/`).
3. Smoke test con `curl` (o Postman):

   ```bash
   # MaxID
   curl -H "X-API-Key: poc-bodega-key-cambiar" \
        http://localhost:59800/api/bodega/maxid

   # Existe alguna bodega para idEmpresa=1?
   curl -H "X-API-Key: poc-bodega-key-cambiar" \
        http://localhost:59800/api/bodega/exists/1

   # Obtener bodega 1
   curl -H "X-API-Key: poc-bodega-key-cambiar" \
        http://localhost:59800/api/bodega/1
   ```

Respuesta esperada para `MaxID`:

```json
{ "data": 23, "error": null }
```

---

## 6. Cómo el `frmBodega` lo consumiría (próximo paso del POC)

El siguiente paso natural — y lo que demuestra el patrón completo — es introducir
una **interface** y dos implementaciones de `clsLnBodega`:

```vb
' En DAL: interface nueva (no rompe nada)
Public Interface IClsLnBodega
    Function MaxID() As Integer
    Function Obtener(ByRef oBe As clsBeBodega) As Boolean
    Function Exists_By_IdEmpresa(idEmpresa As Integer) As Boolean
    Function Insertar(ByRef oBe As clsBeBodega) As Integer
    Function Actualizar(ByRef oBe As clsBeBodega) As Integer
    ' ... etc
End Interface

' Implementación 1: la actual (Shared methods envueltos en una clase de instancia)
Public Class clsLnBodega_Local : Implements IClsLnBodega
    Public Function MaxID() As Integer Implements IClsLnBodega.MaxID
        Return clsLnBodega.MaxID()
    End Function
    ' ... resto delega 1:1
End Class

' Implementación 2: HttpClient → esta WebAPI
Public Class clsLnBodega_Remote : Implements IClsLnBodega
    Private Shared ReadOnly _http As New HttpClient() With {
        .BaseAddress = New Uri(ConfigurationManager.AppSettings("WebApiBaseUrl")),
        .Timeout = TimeSpan.FromSeconds(30)
    }
    Public Function MaxID() As Integer Implements IClsLnBodega.MaxID
        _http.DefaultRequestHeaders.Remove("X-API-Key")
        _http.DefaultRequestHeaders.Add("X-API-Key", ConfigurationManager.AppSettings("WebApiKey"))
        Dim json = _http.GetStringAsync("api/bodega/maxid").Result
        Dim resp = JsonConvert.DeserializeObject(Of ApiResponse(Of Integer))(json)
        If resp.Error IsNot Nothing Then Throw New Exception(resp.Error.Message)
        Return resp.Data
    End Function
    ' ... resto idem
End Class

' Factory por config
Public Module BodegaFactory
    Public Function Create() As IClsLnBodega
        Select Case ConfigurationManager.AppSettings("BodegaDataMode")
            Case "Remote" : Return New clsLnBodega_Remote()
            Case Else     : Return New clsLnBodega_Local()
        End Select
    End Function
End Module
```

Después en `frmBodega.vb` un único cambio mecánico:
- `clsLnBodega.MaxID()` → `BodegaFactory.Create().MaxID()` (o inyectar la dependencia).
- Idem para los otros 7 métodos.

Con eso, **el form no se entera** si está hablando con la BD directa o con la WebAPI.
Toggle por config: `<add key="BodegaDataMode" value="Local" />` (o `"Remote"`).

---

## 7. Plan de prueba con cronómetro (lo que valida el patrón)

Lo importante del POC no es que funcione — eso ya está implícito — sino **medir**.

| Escenario | Hosting de la WebAPI | RTT cliente→API | RTT API→BD | Resultado esperable |
|---|---|---|---|---|
| **A — Baseline actual** | (no hay WebAPI, BOF llama directo a BD) | n/a | ~150ms (GT→Oregon) | lento como hoy |
| **B — Trampa típica** | WebAPI corriendo en GT (al lado del BOF) | ~1ms | ~150ms | igual o peor que A (no mejora) |
| **C — Patrón correcto** | WebAPI en EC2 AWS (mismo VPC que la BD) | ~150ms | <1ms | **5-50x más rápido** |

Procedimiento:

1. Instrumentar el `frmBodega` con `Stopwatch` alrededor de la operación "Cargar bodega" o "Guardar bodega".
2. Medir 10 ciclos en escenario A (modo `Local`, hoy).
3. Deployar este wrapper en IIS de un EC2 AWS en la misma región del SQL Server.
4. Cambiar `BodegaDataMode=Remote` y `WebApiBaseUrl=https://tu-ec2/api/`.
5. Medir 10 ciclos en escenario C.
6. Comparar promedios y publicar resultado.

Para el flujo **Insertar**: típicamente `Insertar` hace 1 sola query (no es chatty),
así que la mejora ahí es marginal. Para el flujo **Cargar Bodega completa con
áreas/sectores/tramos/ubicaciones** (que es lo que hace `Cargar_Bodega_Areas` +
`Cargar_Bodega_Sector` + ... → ~50-200 queries en cascada), la mejora es
**dramática** si después agregás un endpoint coarse-grained tipo
`GET /api/bodega/{id}/full-tree` que internamente hace todas las queries en server.

---

## 8. Limitaciones conocidas del POC

1. **`Unificar_Bodegas`**: la firma real toma varios parámetros (líneas 2520+ del Partial).
   El controller acá solo expone los 2 obvios (`IdBodegaDestino`, `IdBodegaOrigen`).
   Hay que completar `UnificarBodegasRequest` con los argumentos restantes según la firma real.
2. **Auth POC**: header `X-API-Key` plano. En producción usar Azure AD / Windows Integrated / JWT.
3. **HTTPS**: el `Web.config` no fuerza HTTPS. En PRD obligatorio + cert válido.
4. **No hay manejo de transacciones distribuidas**: si una operación legacy del frm
   abría `SqlTransaction` desde el cliente y la pasaba a varias llamadas LN, en el
   modo Remote esa transacción tiene que vivir en el servidor. Para la pantalla
   `frmBodega` no es problema (cada operación es atómica), pero hay que tenerlo
   presente para otras pantallas más complejas.
5. **Coarse-grained endpoints**: este POC solo expone los 8 métodos LN 1:1. El
   verdadero salto de performance viene cuando agregás endpoints "por caso de uso"
   que devuelven todo lo que la pantalla necesita en 1 sola roundtrip. Recomendado
   como fase 2 del POC.

---

## 9. Próximos pasos sugeridos

1. **Importar y compilar** este proyecto en tu Solution. Verificar refs.
2. **Smoke test** con curl/Postman desde tu PC contra el dev server local.
3. **Deploy a IIS en un EC2 AWS** en la misma región del SQL Server.
4. **Escribir las 2 implementaciones** `clsLnBodega_Local` / `clsLnBodega_Remote` y el factory.
5. **Modificar `frmBodega.vb`** (cambio mecánico de 8 líneas) para usar el factory.
6. **Cronometrar** escenario A vs C. Reportar números.
7. Si los números validan: **agregar endpoint coarse-grained** `GET /api/bodega/{id}/full-tree`
   que devuelva bodega + áreas + sectores + tramos + ubicaciones en 1 sola roundtrip.
   Medir de nuevo. Acá es donde aparece el factor 50-100x.

---

## 10. Repositorio interno

Este POC vive en el repo `tomwms-replit-client-automate` rama `wms-brain`,
carpeta `wms-brain/poc/WmsBodegaApi/`. No interfiere con `wms-brain/brain/` ni
con ninguna otra carpeta del repo.
