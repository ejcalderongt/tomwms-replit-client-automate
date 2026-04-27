# Tablas del flujo de reserva

> Aprendido el 2026-04-27T15:28:58.419Z desde TOMWMS_KILLIOS_PRD. BYB y CEALSA tienen el mismo schema.

## Mapa del flujo

```
[ERP envia pedido]
       |
       v
i_nav_ped_traslado_enc / det / det_lote   (interface NAV/SAP -> WMS)
       |
       v
trans_pe_enc / trans_pe_det               (pedido interno WMS, 70 + 44 cols)
       |
       v
[motor de reserva ejecuta CASO_#X]
       |
       +----> stock_res                   (reservas activas, 35 cols)
       |
       +----> trans_pe_det_log_reserva    (auditoria de cada decision, 17 cols)
       |
       v
[picking, despacho, etc.]
```

## i_nav_ped_traslado_enc (24 cols)

Encabezado del pedido de traslado interfaz con NAV. Schema estilo NAV (`No`, `Posting_Date`, `Transfer_from_Code`, `Transfer_to_Code`, `Status`, etc.).

## i_nav_ped_traslado_det (22 cols) y _det_lote (11 cols)

Detalle por linea. `det_lote` agrega lote/serial/vencimiento.

## trans_pe_enc (70 cols)

Encabezado del pedido WMS interno. Tiene info de cliente, ruta, prioridad, fechas, ubicación, estado, totales calculados, etc.

## trans_pe_det (44 cols)

Detalle del pedido WMS. Columnas clave:
- `IdPedidoDet`, `IdPedidoEnc`
- `IdProductoBodega`, `IdEstado`, `IdPresentacion`, `IdUnidadMedidaBasica`
- `Cantidad`, `Peso`, `Precio`
- `cant_despachada` (cuanto se despacho realmente)
- `no_recepcion`, `ndias` (info de recepcion origen)

## stock_res (35 cols) — RESERVAS ACTIVAS

Cada fila es una reserva sobre un `stock` específico para un `pedido_det`. Columnas clave:
- `IdStockRes` (PK)
- `IdTransaccion`, `Indicador`
- `IdPedidoDet` → vincula con `trans_pe_det`
- `IdStock` → vincula con `stock`
- `IdPropietarioBodega`, `IdProductoBodega`, `IdProductoEstado`
- `IdPresentacion`, `IdUnidadMedida`
- `IdUbicacion`, `ubicacion_ant`
- + 23 columnas mas (cantidades, lote, vencimiento, fechas, audit)

Hay variantes:
- `stock_res_se` (9 cols) — reservas "service event" (separadas)
- `stock_res_20250624` y `stock_res_ped_164` — backups manuales en prod (deuda, no son operativas)

## trans_pe_det_log_reserva (17 cols) — FUENTE DE VERDAD OBSERVADA

Auditoria de cada decision del motor:

| Columna | Tipo | Descripcion |
|---|---|---|
| `IdLogReserva` | int | PK |
| `IdBodega` | int | bodega de la decision |
| `Fecha` | datetime | timestamp |
| `IdPedidoEnc` | int | pedido WMS afectado |
| `Line_No` | int | numero linea |
| `Item_No` | nvarchar/50 | producto |
| `UmBas` | nvarchar/50 | unidad medida basica |
| `Variant_Code` | nvarchar/50 | variante (lote/sabor/color en NAV) |
| `MensajeLog` | nvarchar(MAX) | texto largo con calculos internos |
| `Cantidad` | float | cantidad reservada |
| `Caso_Reserva` | nvarchar/50 | identificador del caso ej. `CASO_#24_EJC202310090957` |
| `EsError` | bit | true si fue error |
| `Referencia_Documento` | nvarchar/50 | doc origen |
| `Fecha_Vence` | date | vencimiento del lote reservado |
| `IdPedidoDet` | int | linea afectada |
| `IdStock` | int | stock origen |
| `IdStockRes` | int | reserva generada |

Vocabulario del `MensajeLog` (extraido de samples reales): `Fecha Mínima`, `DiasVencimiento`, `FechaMinimaVenceZonaPicking`, `vFechaMinima`.

## trans_pe_tipo (30 cols) — CONTROL DE FLUJO POR TIPO DE PEDIDO

Define qué hace el motor segun el tipo de documento:

| Columna | Tipo | Notas |
|---|---|---|
| `IdTipoPedido` | int | PK (1=PE0001, 3=PE0003, etc.) |
| `Nombre`, `Descripcion` | nvarchar | etiqueta |
| `Preparar` | bit | activa preparacion |
| `Verificar` | bit | activa verificacion |
| **`ReservaStock`** | **bit** | **si false, NO ejecuta el motor de reserva** |
| `ImprimeBarrasPicking` / `ImprimeBarrasPacking` | bit | impresion |
| **`control_poliza`** | **bit** | **CEALSA usa esto en PE0001 y PE0005 (3PL)** |
| `requerir_documento_ref` | bit | requiere doc referencia |
| `Generar_pedido_ingreso_bodega_destino` | bit | auto-pedido en destino |
| `IdTipoIngresoOC` | int | tipo ingreso para OC |
| ... 18 mas | | |

### Tipos por cliente (datos reales productivos)

**Killios** (6 tipos):
| Id | Nombre | ReservaStock | Notas |
|---|---|---|---|
| 1 | PE0001 | SI | |
| 3 | PE0003 | SI | |
| 4 | PE0004 | NO | sin reserva |
| 6 | TRAS_WMS | NO | traslado interno sin reserva |
| 9 | PDV_NAV | SI | punto de venta NAV |
| 12 | DEVPROV | SI | devolucion proveedor |

**BYB** (6 tipos): PE0001/02/03/Requisicion/PDV_NAV con SI; PE0004 con NO.

**CEALSA** (7 tipos): PE0001..03/05/Requisicion/PDV_NAV con SI; PE0004 con NO.
**`control_poliza=SI` en CEALSA: PE0001 y PE0005** (3PL).

## Otras tablas relacionadas

- `trans_pe_pol` (41 cols) — polizas asociadas (importacion/exportacion 3PL): `bl_no`, `NoPoliza`, `viaje_no`, `buque_no`, `fecha_abordaje`, `destino`, etc. **CRITICO para CEALSA**.
- `trans_pe_servicios` (9 cols) — servicios facturables del 3PL: `IdServicio`, `cantidad`, observaciones.
- `trans_pe_docu_ref` (13 cols) — documento de referencia.
