---
slug: 2026-05-19-codex-learning-byb-mi3-reserva-umbas
agente: codex-local
fecha: 2026-05-19T16:20:00-06:00
cliente: BYB
ambiente: QA
repo_afectado: TOMWMS
branch: dev_2028_merge
area: BOF / reserva MI3 / stock_res
tag_codigo: "#EJCBYB20250519CKF"
---

# Propuesta para Brain Keeper

## Resumen

Se analizo el caso reportado por BYB QA en `Erores_WMS_140526.docx`, relacionado con fallos de reserva MI3 antes de crear/enviar picking a NAV.

El hallazgo principal fue una sobre-reserva en el flujo legacy BOF `Reserva_Stock_From_MI3`: cuando el pedido venia en UMBAS (`IdPresentacion = 0`) y el algoritmo entraba al fallback/recursion de UMBAS, podia usar `vCantidadDecimalUMBas` en vez del pendiente real `vCantidadPendiente`.

Esto permitia que una linea que debia completar pocas unidades absorbiera todo el stock disponible de un registro UMBAS.

## Evidencia del caso

Documento cliente:

- `C:\Users\yejc2\Downloads\Erores_WMS_140526.docx`

Base QA BYB:

- servidor: `172.16.100.36`
- base: `IMS4MB_BYB_QAS2`
- usuario: no documentado aqui por seguridad

Ejemplo confirmado en BD:

- Documento: `EA-153305`
- Pedido WMS: `IdPedidoEnc = 37303`
- Producto: `00194250`
- Linea NAV/WMS: `120000`
- Pedido: `15 UNI`
- Reservas generadas:
  - `12` desde `IdStock = 757275`, `IdPresentacion = 134`, presentacion `CJ`, factor `4`
  - `997` desde `IdStock = 778373`, `IdPresentacion = 0`
- Resultado mostrado al cliente: `1009`

Interpretacion:

- La primera reserva de `12` era valida como consumo parcial.
- El pendiente real era `3`.
- La recursion a UMBAS debia reservar `3`, pero termino reservando `997`, que corresponde al remanente disponible completo del stock UMBAS `778373`.

## Causa tecnica

En `TOMIMSV4/DAL/Transacciones/Stock_Reservado/clsLnStock_res_Partial.vb`, dentro de `Reserva_Stock_From_MI3`, el flujo posterior a `Inserta_Stock_Reservado(lBeStockAReservar, ...)` prepara una solicitud recursiva `BeStockResUMBas`.

Antes del ajuste, la cantidad de la solicitud recursiva podia tomar `vCantidadDecimalUMBas`:

```vbnet
BeStockResUMBas.Cantidad = vCantidadDecimalUMBas
```

El problema es que `vCantidadDecimalUMBas` no representa siempre el pendiente real cuando el pedido ya viene en UMBAS. Esa variable tambien participa en la logica de fraccion/explosion de presentacion.

Para pedidos con `pStockResSolicitud.IdPresentacion = 0`, el valor canonico del remanente es `vCantidadPendiente`.

## Cambio aplicado en codigo

Archivo:

- `C:\Users\yejc2\source\repos\TOMWMS\TOMIMSV4\DAL\Transacciones\Stock_Reservado\clsLnStock_res_Partial.vb`

Zona:

- `Reserva_Stock_From_MI3`
- alrededor de la linea `25553`
- tag: `#EJCBYB20250519CKF`

Regla implementada:

```vbnet
If pStockResSolicitud.IdPresentacion = 0 Then
    vCantidadDecimalUMBas = vCantidadPendiente
ElseIf ...
    ...
End If
```

Y la solicitud recursiva se redondea:

```vbnet
BeStockResUMBas.Cantidad = Math.Round(vCantidadDecimalUMBas, 6)
```

## Por que se hizo asi

El parche es intencionalmente quirurgico:

- No cambia el flujo general de `Reserva_Stock_From_MI3`.
- No toca el motor nuevo `WMS.DALCore/Reserva_Stock`.
- No altera la logica cuando el pedido viene en presentacion.
- Solo corrige el caso donde la solicitud ya esta en UMBAS y la recursion debe respetar el pendiente real.

Esto reduce riesgo porque el proceso legacy ya funciona para el core WMS y el objetivo inmediato es corregir la regresion observada en QA sin redisenar el motor de reserva.

## Validacion ejecutada

Compilacion:

```powershell
MSBuild C:\Users\yejc2\source\repos\TOMWMS\TOMIMSV4\DAL\DAL.vbproj /t:Build /p:Configuration=Debug /p:Platform=AnyCPU /v:minimal
```

Resultado:

```text
DAL.dll compilado correctamente.
```

Codificacion:

```text
UTF-8 BOM preservado: EF BB BF
```

## Solicitud a Brain Keeper

Se solicita analizar y confirmar:

1. Si la regla es correcta: para `IdPresentacion = 0`, la recursion UMBAS debe usar `vCantidadPendiente` como fuente de verdad.
2. Si este ajuste debe promoverse como regla global del modulo de reserva:
   - "Cuando el pedido ya viene en UMBAS, no usar variables de fraccion/explosion de presentacion para determinar el pendiente recursivo".
3. Si se debe replicar la misma proteccion en el motor nuevo `WMS.DALCore/Reserva_Stock`, especialmente en `ReservationLoopStep` y `UMBasExplosionHandler`.
4. Si conviene agregar una consulta/SP de auditoria para detectar casos historicos donde `stock_res.cantidad` supera la cantidad solicitada por linea en UMBAS.

## Riesgos / what-if pendientes

- Puede existir otra zona de `Reserva_Stock_From_MI3` con el mismo patron de recursion UMBAS.
- La sobre-reserva ya generada en QA no se corrige con este parche; requiere regularizacion de datos si Erik decide limpiar los casos existentes.
- El flujo WebAPI/DALCore tiene su propia implementacion de reserva. Por ahora no fue modificado para evitar mezclar el parche BOF con el motor nuevo.
- NAV puede fallar por razones adicionales despues de una reserva correcta WMS, como lote/cantidad disponible para "Create Pick". El caso `EA-153314` parece incluir ese segundo tipo de problema.
