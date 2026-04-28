# Modelo del sendero — Mapeo de tablas reales

> Catalogo de las tablas que componen el sendero del producto, con
> nombres CONFIRMADOS en EC2. Erik mencion algunos nombres
> aproximados; aca se documenta el nombre real que usa el codigo.

## Tablas confirmadas en los 5 clientes (existencia)

| Mencionado por Erik | Nombre real en EC2 | Confirmado en |
|---|---|---|
| `trans_oc_enc/det/ti` | `trans_oc_enc`, `trans_oc_det`, `trans_oc_ti` | TODOS (5/5) |
| `trans_re_enc/det` | `trans_re_enc`, `trans_re_det` | TODOS (5/5) |
| `trans_oc_det_lotes` | **`trans_oc_det_lote`** (singular) | TODOS (verificado en BECOFARMA) |
| `stock_rec` | `stock_rec` | TODOS (5/5) |
| `i_nav_barras_pallet` | `i_nav_barras_pallet` | TODOS (5/5) |
| `trans_movimientos` | `trans_movimientos` | TODOS (5/5) |
| `vw_movimientos_n1` | **`VW_Movimientos_N1`** (mayusculas) | TODOS (5/5) |
| `ubicacion` | **`bodega_ubicacion`** | TODOS |
| `estado_producto` | **`producto_estado`** | TODOS |
| `presentacion` | (no existe sola; se usa) `producto_presentacion` | TODOS |
| `tipo_oc` | (no existe; campo `IdTipoIngresoOC` apunta a `trans_oc_ti`) | TODOS |

## Catalogo completo de tablas del sendero

### A. Catalogos (lookup)

| Tabla | Cols clave | Rol en el sendero |
|---|---|---|
| `producto` | IdProducto, codigo, control_lote, control_vencimiento, control_peso, genera_lp_old, IdTipoEtiqueta, IdUnidadMedidaBasica | flags estaticos del producto |
| `producto_bodega` | IdProductoBodega, IdProducto, IdBodega | producto materializado en bodega (instancia) |
| `producto_presentacion` | IdPresentacion, IdProducto, nombre, factor | presentaciones (Caja12, Bulto, etc) |
| `producto_presentaciones_conversiones` | IdPresentacion_origen, IdPresentacion_destino, factor | conversion entre presentaciones |
| `producto_presentacion_tarima` | IdPresentacion, IdTarima | presentacion especifica para tarimas |
| `producto_estado` | IdEstado, IdPropietario, nombre, IdUbicacionDefecto, utilizable, dañado, sistema, dias_vencimiento_clasificacion, tolerancia_dias_vencimiento, reservar_en_umbas | estados que puede tener un producto |
| `producto_estado_ubic` | IdProductoEstadUbic, IdEstado, IdBodega, IdUbicacionDefecto | mapeo estado→ubicacion DEFAULT por bodega |
| `bodega` | IdBodega, codigo, nombre, ... | bodegas |
| `bodega_ubicacion` | IdUbicacion, IdTramo, descripcion, ubicacion_picking, ubicacion_recepcion, acepta_pallet, IdIndiceRotacion, IdTipoRotacion, bloqueada, dañado, activo | ubicaciones fisicas |
| `area_estado` | (a documentar) | mapeo area → estado permitido |
| `motivo_ubicacion` | IdMotivo, descripcion | razon de movimiento de ubicacion |
| `regla_ubicacion`, `ubicaciones_por_regla` | (a documentar) | sistema de reglas para resolver ubicacion |
| `unidad_medida` | IdUnidadMedida, IdPropietario, Nombre | unidades (UMBas, Cj, Bulto) |
| `sis_tipo_tarea` | IdTipoTarea, Nombre, Contabilizar, Activo | tipos de movimiento (RECE, UBIC, PIK, DESP, ...) — ver `03-tipos-tarea.md` |

### B. Documentos de ingreso

| Tabla | Cols clave | Rol |
|---|---|---|
| `trans_oc_enc` | IdOrdenCompraEnc, IdTipoIngresoOC, IdEstadoOC, IdBodega, No_Documento, Fecha_Recepcion, IdMuelleRecepcion, Programar_Recepcion, Enviado_A_ERP, push_to_nav | encabezado de orden de ingreso |
| `trans_oc_det` | IdOrdenCompraDet, IdProductoBodega, IdPresentacion, IdUnidadMedidaBasica, No_Linea, nombre_producto, cantidad esperada | detalle de linea |
| `trans_oc_det_lote` | IdLineaLote, IdOrdenCompraDet, lote, fecha_vence, cantidad | lotes esperados por linea |
| `trans_oc_ti` | IdTipoIngresoOC, descripcion, defaults | tipo de orden de ingreso (clasificacion) |
| `trans_oc_estado` | (a documentar) | estados validos de la OC |
| `trans_oc_pol` | IdOrdenCompraEnc, codigo_poliza | datos de poliza (si Control_Poliza=True) |
| `i_nav_ped_compra_det_lote` | (a documentar) | lotes pre-asignados desde ERP NAV |
| `i_nav_ped_traslado_det_lote` | idem para traslados | idem |
| `i_nav_barras_pallet` | IdPallet, Codigo, Camas_Por_Tarima, Cajas_Por_Cama, Lote, Bodega_Origen, Bodega_Destino, IdRecepcion, Recibido | tarimas pre-recibidas |

### C. Recepcion fisica

| Tabla | Cols clave | Rol |
|---|---|---|
| `trans_re_enc` | IdRecepcionEnc, IdPropietarioBodega, fecha, IdEstado, IdMuelleRecepcion, IdOperadorBodega | encabezado de recepcion fisica |
| `trans_re_det` | IdRecepcionDet, IdRecepcionEnc, IdProductoBodega, IdProductoEstado, IdUbicacion, cantidad, peso, lote, lic_plate, fecha_vence | detalle de recepcion (lo que efectivamente entro) |
| `trans_re_oc` | IdRecepcionEnc, IdOrdenCompraEnc | link recepcion ↔ orden compra |
| `trans_re_det_lote_num` | (a documentar) | lotes detallados con numeracion |

### D. Stock activo

| Tabla | Cols clave | Rol |
|---|---|---|
| **`stock_rec`** | IdRecepcionDet, IdProductoBodega, IdProductoEstado, IdPresentacion, IdUnidadMedida, IdUbicacion, IdUbicacion_anterior, IdRecepcionEnc, IdRecepcionDet, IdPedidoEnc, IdPickingEnc, IdDespachoEnc, lote, lic_plate, serial, cantidad, peso, fecha_ingreso, fecha_vence, uds_lic_plate, no_bulto, fecha_manufactura, añada, temperatura, activo | **TABLA MAGISTRAL DEL SENDERO** — un registro por entrada unica al stock, conecta ingreso ↔ ubicacion actual ↔ salida |

> `stock_rec` tiene 38 columnas y es la tabla central. Cada fila representa
> "una unidad de stock" identificada por (producto, lote, lic_plate, ubicacion,
> estado). Se mueve entre ubicaciones cambiando `IdUbicacion` (y guardando
> el anterior en `IdUbicacion_anterior`). Cuando sale del WMS, se setea
> `IdDespachoEnc`/`IdPickingEnc` y `activo=0`.

### E. Movimientos (sender de transicion segun Erik)

| Tabla | Cols clave | Rol |
|---|---|---|
| **`trans_movimientos`** | IdMovimiento, IdTipoTarea, IdBodegaOrigen, IdBodegaDestino, IdUbicacionOrigen, IdUbicacionDestino, IdEstadoOrigen, IdEstadoDestino, IdProductoBodega, IdPresentacion, IdUnidadMedida, cantidad, peso, lote, fecha_vence, barra_pallet, IdRecepcion, fecha | **bitacora de TODA transicion** del producto: cada cambio de ubicacion, estado, presentacion, bodega, etc. genera una fila aca |
| `VW_Movimientos_N1` | (vista) | vista interpretada que une `trans_movimientos` con catalogos: nombres de propietario, producto, presentacion, estados origen/destino, ubicaciones origen/destino, tipo de tarea. Filtra por `IdTipoTarea IN (1, 2, 6, 13, 14, 15, 16)` para ingreso y une con UNION para salida |
| `VW_Movimientos`, `VW_Movimientos_N`, `VW_MovimientosDetalle`, `VW_Movimientos_Documento`, `VW_Movimientos_Propietario`, `VW_Movimientos_Retroactivos`, `vw_Movimientos_Poliza` | (vistas) | variantes para reporting/UI |

### F. Salida

| Tabla | Cols clave | Rol |
|---|---|---|
| `trans_pe_enc` | IdPedidoEnc, IdPropietarioBodega, IdEstado, ... | pedido encabezado |
| `trans_pe_det` | IdPedidoDet, IdPedidoEnc, IdProductoBodega, cantidad pedida | pedido detalle |
| `trans_pi_enc/det` | (picking) | encabezado/detalle de picking |
| `trans_des_enc/det` (o `trans_despacho_*`) | despacho | encabezado/detalle de despacho |
| `trans_despacho_det_lote_num` | lotes despachados con numero | trazabilidad de salida |

## Diagrama de relaciones (alto nivel)

```
                    ┌─ trans_oc_ti ─┐ (tipo)
                    │                │
             ┌──────┼─ trans_oc_enc ─┼──── trans_oc_det ─── trans_oc_det_lote
             │      │                │              │
             │      └─ trans_oc_pol ─┘              │
             │                                       │ (lotes esperados)
             │   (opcional)                          │
             ├── i_nav_ped_compra_det_lote ──────────┤
             │   i_nav_barras_pallet                 │
             │                                       │
             ▼                                       ▼
       trans_re_enc ─── trans_re_oc ────  trans_re_det ────► (genera) ────► stock_rec ◄────┐
                                                                                            │
                                                                                            │
                                                                                            │ historial
                                                                                            │
                                                                  trans_movimientos ────────┤
                                                                  (toda transicion          │
                                                                   genera fila aca)         │
                                                                                            │
                                                                                            │
                                                                          (sale por)        │
                                                                                            │
                                                            ┌──── trans_pe_enc/det ◄────────┘
                                                            │     trans_pi_enc/det
                                                            │     trans_des_enc/det
                                                            ▼
                                                     ERP / cliente

CATALOGOS (lookups laterales):
- producto, producto_bodega, producto_presentacion
- producto_estado (con IdUbicacionDefecto)
- producto_estado_ubic (mapeo por bodega)
- bodega, bodega_ubicacion
- sis_tipo_tarea (catalogo de tipos de movimiento)
- unidad_medida
```

## Pendientes

- Confirmar que `trans_pi_enc/det` (picking) y `trans_des_enc/det`
  (despacho) son los nombres reales — verificar con prefijo correcto.
- Documentar `area_estado`, `regla_ubicacion`, `ubicaciones_por_regla`
  (sistema de reglas).
- Documentar `Presentacion_Factor` (BECOFARMA-only) y como difiere
  del modelo estandar de presentaciones.
