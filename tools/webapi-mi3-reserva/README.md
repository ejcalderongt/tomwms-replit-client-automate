# Prueba WebAPI MI3 reserva BYB

`#EJC20260520_RESERVA_BYB_FIX`

Este kit genera el payload del pedido `EA-153305` para probar la misma ruta de reserva desde WebAPI.

## Generar payload

```powershell
cd C:\Users\yejc2\source\repos\TOMWMS\tools\webapi-mi3-reserva
.\build-mi3-salida-ea153305-payload.ps1 -No "EA-153305-WAPI01"
```

El archivo generado queda en:

```text
C:\Users\yejc2\source\repos\TOMWMS\tools\webapi-mi3-reserva\mi3-salida-ea153305-webapi.json
```

## Ejecutar contra WebAPI local

Antes de ejecutar, confirmar que `WMSWebAPI` este apuntando por `ConnectionStrings:CST`
a la base BYB QA usada para la prueba (`IMS4MB_BYB_QAS2`). El `appsettings.Development.json`
del repo puede estar apuntando a otro cliente.

```powershell
curl.exe -X POST "http://localhost:5196/api/sync/salidas/mi3/insertar" `
  -H "Content-Type: application/json" `
  --data-binary "@C:\Users\yejc2\source\repos\TOMWMS\tools\webapi-mi3-reserva\mi3-salida-ea153305-webapi.json"
```

Si se usa el perfil HTTPS:

```powershell
curl.exe -k -X POST "https://localhost:7194/api/sync/salidas/mi3/insertar" `
  -H "Content-Type: application/json" `
  --data-binary "@C:\Users\yejc2\source\repos\TOMWMS\tools\webapi-mi3-reserva\mi3-salida-ea153305-webapi.json"
```

## Meta esperada

- El WebAPI debe responder `Exito = true`.
- `LineasProcesadas` debe coincidir con las lineas creadas en `trans_pe_det`.
- `TotalReservado` debe salir de `stock_res`, no de una suma previa del payload.
- Las lineas en presentacion `CJ` con cantidad decimal deben quedar normalizadas a UM base cuando aplique, sin reservas fraccionarias artificiales.
- Las reservas parciales deben quedar justificadas por stock disponible real y reflejadas en `Quantity_Reserved_WMS`.

## Validar en SQL

Ejecutar:

```text
C:\Users\yejc2\source\repos\TOMWMS\tools\webapi-mi3-reserva\validar-mi3-salida-ea153305-webapi.sql
```

Cambiar `@No` si se genera otro numero de documento.
