# Cómo testear el flujo completo: frmBodega → WebAPI → BD

POC end-to-end para validar el patrón Remote DAL con riesgo controlado.
**Tiempo estimado**: 30-45 min para el smoke test (`MaxID`).

---

## Resumen del enfoque

En lugar de meter una interfaz formal con DI (eso es para fase 2), usamos un
**proxy estático drop-in** llamado `clsLnBodegaProxy` que vive en el WMS y:

- Si `BodegaDataMode=Local` → delega 1:1 a `clsLnBodega.X(...)` → comportamiento idéntico a hoy.
- Si `BodegaDataMode=Remote` → invoca la WebAPI por HttpClient.

Y en `frmBodega.vb` hacemos **un único Find & Replace mecánico**:
`clsLnBodega.` → `clsLnBodegaProxy.`

Toggle por config = riesgo cero. Si rompe algo, ponés `Local` y volvés al estado actual sin recompilar.

---

## Pre-requisitos

- [x] Proyecto `WmsBodegaApi.WebApi` compilado y corriendo (IIS Express en `http://localhost:59800/` o IIS).
- [x] El `Web.config` de la WebAPI tiene el `CST` apuntando a la BD del cliente que vas a probar (ej. KILLIOS).
- [x] El `Web.config` de la WebAPI tiene `<add key="ApiKey" value="poc-bodega-key-cambiar" />`.
- [x] Tenés acceso a editar y recompilar el WMS.exe (TOMIMSV4).

---

## Paso 1 — Agregar el proxy al WMS

1. Copiar el archivo `clsLnBodegaProxy.vb` al proyecto **TOMIMSV4** (WMS.vbproj). Sugerencia de ubicación:

   ```
   TOMIMSV4/TOMIMSV4/Mantenimientos/Bodega/clsLnBodegaProxy.vb
   ```

   (al lado de `frmBodega.vb`, fácil de encontrar).

2. En Solution Explorer: click derecho sobre la carpeta `Bodega` → **Agregar → Elemento existente** → seleccionar `clsLnBodegaProxy.vb`.

3. Verificar que el proyecto WMS.vbproj ya tenga `<Reference Include="Newtonsoft.Json">` y `<Reference Include="System.Net.Http">`. Lo primero **lo tiene** (lo confirmé en `DAL.vbproj` y WMS.vbproj lo hereda transitivamente). `System.Net.Http` viene en el GAC de .NET 4.8 — agregalo desde **Add Reference → Assemblies → Framework**.

4. Build de TOMIMSV4. Tiene que compilar sin tocar nada más (el proxy vive en el namespace `TOMWMS`, así que `clsBeBodega` y `clsLnBodega` se ven sin imports extra).

---

## Paso 2 — Agregar las keys al App.config del WMS

Abrí `TOMIMSV4/TOMIMSV4/App.config` (o `WMS.exe.config` ya desplegado), y dentro del `<appSettings>` existente agregá:

```xml
<add key="BodegaDataMode" value="Local" />
<add key="WebApiBaseUrl" value="http://localhost:59800/" />
<add key="WebApiKey"      value="poc-bodega-key-cambiar" />
```

Con `BodegaDataMode=Local` el comportamiento es idéntico al de hoy.
**Esto te permite hacer el cambio de código y publicarlo sin riesgo. El "switch" se hace después editando solo el config.**

---

## Paso 3 — Find & Replace en `frmBodega.vb`

En **Visual Studio**, abrí `frmBodega.vb`, **Ctrl+H** (Reemplazar en archivo actual):

| Buscar | Reemplazar con | Match case | Whole word |
|---|---|---|---|
| `clsLnBodega.` | `clsLnBodegaProxy.` | ☑ | ☐ |

Reemplazar todo. Tiene que reemplazar **exactamente 12 ocurrencias** (en líneas 290, 942, 1383, 1469, 1568, 1815, 2176, 2257, 4621, 4891, 5115, 5141).

> Nota importante: la regex no matchea `clsLnBodega_monitor_parametro` (porque tiene `_` después, no `.`), ni la declaración `Public pBeBodega As New clsBeBodega` (es la entidad). Solo matchea exactamente `clsLnBodega.` con punto, que son las invocaciones a métodos Shared.

Build de TOMIMSV4. Tiene que compilar OK.

---

## Paso 4 — Smoke test 1: `MaxID` (read-only, lo más simple)

`MaxID` es lo más fácil de testear porque:
- Es solo lectura (no toca datos).
- Devuelve un `Integer` (sin entidad compleja).
- Lo ejecuta el form al abrir en modo "Nuevo" (línea 1383) y al consultar (línea 290).

### Procedimiento

1. **Modo Local (baseline)**:
   - Confirmá `BodegaDataMode=Local` en App.config.
   - Abrí WMS.exe, andá a Mantenimiento → Bodega → **Nuevo**.
   - Verificá que el campo IdBodega se llena con el siguiente ID disponible (ej. 24).
   - Cerrá sin guardar.

2. **Modo Remote**:
   - Cerrá WMS.exe.
   - Cambiá `BodegaDataMode=Remote` en App.config.
   - Verificá que la WebAPI esté corriendo: en otro browser/curl:
     ```bash
     curl -H "X-API-Key: poc-bodega-key-cambiar" http://localhost:59800/api/bodega/maxid
     ```
     Esperás algo como `{"data":24,"error":null}`.
   - Abrí WMS.exe, andá a Mantenimiento → Bodega → **Nuevo**.
   - Verificá que el campo IdBodega se llena **con el mismo número** (ej. 24).

✅ Si los dos modos devuelven el mismo número, el patrón funciona.

### Cómo verificar que efectivamente fue por la WebAPI (y no por la BD directa)

**Opción 1 — Logs en la WebAPI**: en `WmsBodegaApi.WebApi/Web.config` agregá `<system.diagnostics>` para loguear requests, o más simple: poné un breakpoint en `BodegaController.GetMaxId()` y ejecutá la WebAPI en modo Debug en una segunda instancia de VS. Cuando abras "Nuevo" en el WMS, el breakpoint debe saltar.

**Opción 2 — IIS log**: si la corrés en IIS, mirá `C:\inetpub\logs\LogFiles\W3SVC*\u_ex*.log` y vas a ver el GET `/api/bodega/maxid` con tu IP origen.

**Opción 3 — Log a archivo desde el proxy** (la más portable). Editá `clsLnBodegaProxy.vb`, función `MaxID`, agregá:

```vb
Public Function MaxID() As Integer
    If IsRemote Then
        Dim r = GetJson(Of Integer)("api/bodega/maxid")
        IO.File.AppendAllText("C:\Temp\bodega-proxy.log",
            String.Format("{0:O} REMOTE MaxID -> {1}{2}", Date.Now, r, Environment.NewLine))
        Return r
    Else
        Dim r = clsLnBodega.MaxID()
        IO.File.AppendAllText("C:\Temp\bodega-proxy.log",
            String.Format("{0:O} LOCAL  MaxID -> {1}{2}", Date.Now, r, Environment.NewLine))
        Return r
    End If
End Function
```

Después de probar, mirá `C:\Temp\bodega-proxy.log` y vas a ver si dice `REMOTE` o `LOCAL`.

---

## Paso 5 — Smoke test 2: `Exists_By_IdEmpresa` (read-only)

Mismo procedimiento que `MaxID`. Se invoca al cambiar el combo Empresa o al intentar guardar (línea 1469). Modo Local vs Remote tienen que devolver el mismo Boolean.

---

## Paso 6 — Test medio: `Obtener` (lectura de entidad completa)

Este es el primero que serializa una entidad compleja. Importante porque valida que el roundtrip JSON no pierde campos críticos.

1. En modo Local, andá a Bodega → buscá una bodega existente (ej. ID 1) → click "Cargar". Anotá los valores que ves en pantalla (Codigo, Nombre, Activo, Coordenada_x/y, etc).
2. Cerrá WMS, cambiá a `Remote`, repetí el mismo "Cargar bodega ID 1".
3. Comparar pantalla por pantalla. Tienen que ser idénticos.

> Si ves campos vacíos o con `0` que en Local salían con valor, es probable que el `BodegaDto` o el `BodegaMapper` de la WebAPI no estén mapeando esa propiedad. Avisame con el nombre del campo y lo agrego.

---

## Paso 7 — Tests de escritura (`Insertar`, `Actualizar`)

Estos son los más caros porque modifican datos. Recomiendo hacerlos contra una **BD de DEV** (no PRD).

1. Configurá una BD de pruebas (clon de KILLIOS por ejemplo).
2. En `Web.config` de la WebAPI cambiá `CST` a apuntar a esa BD.
3. Modo Local: creá una bodega de prueba "POC-LOCAL". Anotá el ID.
4. Modo Remote: creá otra "POC-REMOTE". Tiene que crearse OK y aparecer en la BD.
5. Comparar las dos filas en SQL: todos los campos tienen que matchear las defaults esperadas, salvo Nombre.

---

## Paso 8 — Medir performance (lo que valida el patrón)

Una vez que el flow Remote anda igual que Local, el POC empieza a generar valor:

1. Hosteá la WebAPI en un EC2 que esté **en la misma región/VPC del SQL Server productivo**.
2. Cambiá `WebApiBaseUrl` en el WMS de tu PC al endpoint público de ese EC2.
3. Cronometrá con `Stopwatch` la operación "Cargar bodega completa con árbol" en los 2 modos.

| Modo | Latencia esperada | Por qué |
|---|---|---|
| Local (BD remota directa) | 200-2000 ms | N queries × ~150ms RTT cada una |
| Remote (WebAPI al lado de la BD) | 50-200 ms | 1 RTT del cliente + N queries locales (<1ms cada una) |

Cuando el número C ≪ A, **el patrón está validado**. Y cuando agregues el endpoint coarse-grained `GET /api/bodega/{id}/full-tree` (fase 2) la diferencia se vuelve aún más dramática.

---

## Troubleshooting rápido

| Síntoma | Causa probable | Fix |
|---|---|---|
| `WebAPI UNAUTHORIZED` | Falta header `X-API-Key` o no coincide | Verificar que `WebApiKey` del WMS = `ApiKey` del Web.config |
| `Unable to connect to the remote server` | WebAPI no está corriendo | Levantarla en VS o IIS |
| `WebAPI NOT_FOUND` al cargar bodega | El ID no existe, o el `CST` apunta a BD incorrecta | Verificar el connection string de la WebAPI |
| Campo con `ñ` queda vacío después de cargar | Mismatch en mapper (DanadoPicking ↔ Dañado) | Avisame y lo ajusto en el Mapper de la WebAPI |
| `cannot find type clsBeBodega` al compilar el proxy | Falta ref a Entity.dll en TOMIMSV4 | Ya está, sino agregar `<Reference Include="Entity">` |
| Cuelgue del form al guardar | Llamada HTTP sincrónica bloqueando UI thread | Para el POC está OK; en producción mover a Async/Await |

---

## Volver a Local en 5 segundos

Si algo va mal, editás App.config:
```xml
<add key="BodegaDataMode" value="Local" />
```
Reiniciás WMS.exe, listo. Cero recompilación, cero deploy. Ese es el valor del proxy con toggle.
