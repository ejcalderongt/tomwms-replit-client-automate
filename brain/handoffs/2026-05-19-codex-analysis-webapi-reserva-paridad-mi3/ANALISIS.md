---
tipo: other
autores: [erik]
---
# Analisis de paridad: reserva MI3 legacy vs .NET Core WebAPI/DALCore

Fecha: 2026-05-19
Repo local analizado: `C:\Users\yejc2\source\repos\TOMWMS`

## Veredicto corto

La implementacion actual en `.NET Core WebAPI/DALCore` no puede considerarse
equivalente al flujo legacy VB que proceso el payload MI3 `EA-153305`.

DALCore ya tiene una buena base para diagnosticar fallos de reserva
(`ReservationContext.FailureReasons`, `ReservationFailureCode`,
`UltimoMensajeFallo`), pero:

1. La razon detallada se pierde en el metodo padre porque se sobrescribe con un
   mensaje generico en `Process_Result`.
2. La normalizacion `Variant_Code`/UM/presentacion no replica de forma completa
   el flujo VB que genero el trace `1.291667 CJ -> 62 UMBas`.
3. El pipeline convierte cantidades con la presentacion por defecto, no con la
   presentacion realmente solicitada por `Variant_Code`, salvo que coincidan.
4. Algunas razones de negocio quedan demasiado genericas para operacion
   (`NO_STOCK`, `PICKING_ZONE_REQUIRED_NO_STOCK`) y no distinguen FEFO,
   vencimiento de cliente, reservas previas, ubicacion no picking sin explosion,
   etc.

## Base Brain consultada

Brain confirma que `stock_res` es el centro del flujo de reserva y tiene muchos
writers historicos en BOF y SQL productivo. Por eso cualquier cambio debe ser
acotado al flujo MI3/WebAPI y no reescribir el contrato de `stock_res`.

Brain tambien ubica `trans_pe_det_log_reserva` como tabla historica de log
operativo, con DAL legacy en:

- `TOMIMSV4/DAL/Transacciones/Pedido/Log_Reserva/clsLnTrans_pe_det_log_reserva.vb`
- `TOMIMSV4/DAL/Transacciones/Pedido/Log_Reserva/clsLnTrans_pe_det_log_reserva_partial.vb`

Ese mecanismo sirve como referencia conceptual para exponer razones de reserva
sin contaminar el mensaje principal con punteros de debug.

## Flujo legacy observado en el payload

Caso real observado:

- Documento: `EA-153305`
- Pedido: `37320`
- Linea: `50000`
- Producto: `00025004`
- Payload: `Quantity=1.291667`, `Unit_of_Measure_Code=UNI`, `Variant_Code=CJ`
- Presentacion: `CJ`, factor `48`
- Resultado del trace: `Cantidad_Despues=62`, `IdPresentacion_Despues=0`
- Reserva: `62` UMBas

La clave no fue solo el pipeline de reserva; antes de entrar a
`Reserva_Stock_From_MI3`, legacy normalizo la linea a unidades base.

En VB, esa normalizacion vive en:

`TOMIMSV4/DAL/Transacciones/Pedido/clsLnTrans_pe_det_Partial.vb`

Etapas del trace:

- `payload_interface`
- `normaliza_linea_por_um_variant`
- `linea_convertida_a_umbas`
- `entrada_reserva_stock_from_mi3`

La conversion efectiva vista fue:

```text
1.291667 CJ * 48 = 62.000016 -> Ceiling/Round => 62 UMBas
```

## Flujo actual en .NET Core

Entrada principal:

`WMS.DALCore/I_nav_ped_traslado_enc/clsLnI_nav_ped_traslado_enc.cs`

El metodo `Imp_Ped_Trans_Env_Desde_Tab_Inter_A_WMS`:

- valida producto, unidad y presentacion;
- llama `Inserta_Linea_Detalle_Pedido`;
- si la reserva falla, captura `PDet.Process_Result` en `mensajesFallo`;
- pero luego sobrescribe la fila con:

```text
ERROR_202310021910A: No se pudo completar la reserva, consulte log_error_wms.
```

Esto significa que, aunque `Inserta_Linea_Detalle_Pedido` haya escrito una
razon precisa, la tabla intermedia visible termina con el mensaje generico.

Dentro de `Inserta_Linea_Detalle_Pedido`, DALCore si intenta armar un mensaje
mejor:

```csharp
if (!string.IsNullOrWhiteSpace(pBeStockRes.UltimoMensajeFallo))
    vMensajeEx += $" Razon: {pBeStockRes.UltimoMensajeFallo}";
```

Pero ese mensaje no queda como resultado final si el metodo padre lo pisa.

## Riesgo de paridad en la conversion CJ/UMBas

En `WMS.DALCore/I_nav_ped_traslado_enc/clsLnI_nav_ped_traslado_enc.cs`, el
detalle se inicializa asi:

```csharp
pBePedidoDet.Cantidad = pBeTrasladoDet.Quantity;
pBeStockRes.Cantidad = pBeTrasladoDet.Quantity;
```

Luego, si `Convertir_decimales_a_umbas == 1 && Interface_SAP`, se separa la
cantidad decimal:

```csharp
Split_Decimal(1.291667) => entero=1, decimal=0.291667
decimal * 48 => 14
entero * 48 => 48
```

La logica actual solo asigna la parte entera convertida si `vCantidadEnteraPres
> 0`, por lo que el request de stock puede quedar como `48` UMBas y no `62`
UMBas. Esto no coincide con el trace legacy.

Luego `WMS.DALCore/Pedido/clsLnTrans_pe_det.cs` tiene una normalizacion similar
a la de VB, pero incompleta:

```csharp
pBePedidoDet.Cantidad = Math.Ceiling(Math.Round(pBeTrasladoDet.Quantity * pBePedidoDet.Factor, 2));
pBePedidoDet.Nom_presentacion = "";
pBePedidoDet.IdPresentacion = 0;
```

Diferencias contra VB:

- no actualiza `pBeStockRes.Cantidad`;
- no actualiza `pBeStockRes.IdPresentacion`;
- `pBePedidoDet.Factor` no se asigna en `Inserta_Linea_Detalle_Pedido`, y en
  `clsBeTrans_pe_det` su default es `0.0`.

Por lo tanto, si entra al branch de normalizacion, el C# puede calcular `0` o
mantener un `pBeStockRes.Cantidad` distinto a `pBePedidoDet.Cantidad`.

## Pipeline DALCore

El pipeline esta en:

`WMS.DALCore/Reserva_Stock`

Orden:

1. `ValidationStep`
2. `EntityLoadingStep`
3. `StockQueryStep`
4. `DateCalculationStep`
5. `ReservationLoopStep`
6. `PostProcessingStep`

Puntos positivos:

- `ValidationStep` inicializa `PendingQuantity`.
- `StockQueryStep` separa picking y no-picking, resta reservado y detecta:
  `NO_STOCK`, `ALL_STOCK_EXPIRED`, `LOT_NOT_FOUND`,
  `PICKING_ZONE_REQUIRED_NO_STOCK`, `NON_PICKING_ZONE_REQUIRED_NO_STOCK`.
- `ReservationLoopStep` aplica FEFO por fecha minima, hace fallback de
  explosion/UMBas y limita sobre-reserva con clamp.
- `PostProcessingStep` inserta `stock_res` y actualiza
  `Quantity_Reserved_WMS`.

Punto delicado:

`EntityLoadingStep.ConvertQuantityToUnits` convierte cualquier
`Request.IdPresentacion > 0` usando `DefaultPresentation.Factor`.

Eso solo es correcto si la presentacion solicitada por `Variant_Code` es la
presentacion por defecto. Para productos con varias presentaciones, DALCore
debe resolver el factor de `Request.IdPresentacion` o del `Variant_Code`
solicitado, no asumir default.

## Respuestas de no reserva

Hoy DALCore tiene dos capas:

Interna:

- `ReservationFailureCode`
- `ReservationFailureReason`
- `ReservationContext.FailureReasons`
- `clsBeStock_res.UltimoMensajeFallo`

Visible para operador:

- `Process_Result` en `i_nav_ped_traslado_det`
- `log_error_wms`

La capa interna ya sirve para construir mensajes como:

- no hay stock aplicable;
- stock existe pero esta vencido;
- stock solo existe en no-picking;
- stock solo existe en picking;
- lote especifico no encontrado;
- fecha de manufactura invalida.

Pero la capa visible aun no conserva la razon; termina como "consulte
log_error_wms". Eso no cumple el objetivo operativo.

## Recomendacion de implementacion

1. Portar a C# la normalizacion exacta que ya quedo estable en VB:
   si `Variant_Code` de interfaz difiere del valor en BD y la UM no coincide
   con la presentacion del pedido, convertir `pBePedidoDet` y `pBeStockRes` a
   UMBas con el mismo factor.

2. En `Inserta_Linea_Detalle_Pedido`, asignar `pBePedidoDet.Factor` desde
   `pBePresentacion.Factor` cuando exista presentacion.

3. Corregir la conversion fraccional para que `1.291667 * 48` produzca `62`,
   no solo la parte entera `48`.

4. En `EntityLoadingStep`, usar el factor de la presentacion solicitada
   (`Request.IdPresentacion`) y no siempre el factor de la presentacion por
   defecto.

5. Evitar que el metodo padre sobrescriba `Process_Result` especifico con el
   mensaje generico. Si se requiere mantener el codigo de error, componer:

```text
ERROR_202310021910A: No se pudo completar la reserva. Motivo: <razon operativa>.
```

6. Expandir/matchear codigos a una taxonomia operacional:

- `SIN_STOCK_APLICABLE`
- `RESERVADO_POR_OTROS`
- `SIN_VENCIMIENTO_VALIDO`
- `FEFO_BLOQUEA_PICKING`
- `SOLO_NO_PICKING_SIN_EXPLOSION`
- `EXPLOSION_NIVEL_NO_APLICA`
- `PRESENTACION_NO_APLICA`
- `UBICACION_CLIENTE_OBLIGATORIA`
- `TALLA_COLOR_NO_APLICA`
- `PARCIAL_NO_PERMITIDA`

7. Agregar una prueba dirigida con el payload `EA-153305`:

```text
Quantity=1.291667
Variant_Code=CJ
Factor=48
Esperado: PedidoDet.Cantidad=62, StockRes.Cantidad=62, IdPresentacion=0
```

## Conclusion

La WebAPI/DALCore tiene mejor arquitectura para explicar por que no se reservo,
pero todavia no funciona igual que el flujo legacy observado para este payload.

La mejora recomendada no es reescribir la reserva: es portar la normalizacion
pre-reserva y conservar la razon final de negocio hasta `Process_Result`.

