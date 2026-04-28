# Fingerprint BYB

> Etiqueta human-readable: `BYB_CLIENT_NAV-OPERACION-PARADA-2024_APPLIED`
> Capturado: 29-abr-2026 desde EC2 `52.41.114.122,1437/IMS4MB_BYB_PRD`
>
> **Aclaracion EC2**: copia parcial. La productiva real esta en laptop Erik
> y debe validar este fingerprint contra su BD propia.

## 1. Macro-tag

> **BYB = NAV (NavSync.exe), 2 bodegas (PRODUCTO TERMINADO + DAÑADO),
> 623 productos, modelo PRODUCT-CENTRIC con flags conservadores
> (lote+vencimiento 73.4%), modulo verificacion HALF-IMPLEMENTED
> (estado existe pero sin tablas), talla y color PREPARADO pero vacio,
> OPERACION PARADA entre 2024-2025 (outbox masivo en 2023, silencio
> en 2024, casi nada en 2025).**

## 2. Identidad

| | |
|---|---|
| BD en EC2 | `IMS4MB_BYB_PRD` |
| Ambiente | PRD (productivo, pero operacion parada) |
| Total tablas | 348 |
| Total productos | 623 |
| Bodegas | 2 |
| Talla y color | NO usa (tablas existen pero vacias) |

## 3. Volumen operativo (medido 29-abr-2026)

| Tabla / metrica | Valor |
|---|---|
| `trans_pe_enc.Despachado` | **9,597** (~98.6%) |
| `trans_pe_enc.Anulado` | 27 |
| `trans_pe_enc.Nuevo` | 27 |
| `trans_pe_enc.Verificado` | **8** (uso bajisimo) |
| `trans_pe_enc.Pendiente` | 4 |
| outbox total | **533,329** (volumen masivo historico) |
| outbox enviado=1 | 277,417 (52%) |
| outbox enviado=0 | **255,912 (48% backlog)** |
| outbox INGRESO | 110,902 |
| outbox SALIDA | 422,427 |
| outbox periodo | 09-may-2022 a 14-oct-2025 |
| `log_error_wms` | 203,782 (modelo unificado dominante) |
| `log_error_wms_pe` | 14 (uso bajisimo) |
| `log_error_wms_rec` | 18 |
| `log_error_wms_oc` | 9 |
| `log_error_wms_pick` | 3 |
| `log_error_wms_ubic` | 1 |

### Patron temporal outbox (CRITICO — operacion parada)

| Periodo | Cantidad | Comentario |
|---|---|---|
| 2023-04 | 33,058 | inicio explosion |
| 2023-05 | 42,991 | pico |
| 2023-06 | 42,755 | sostenido |
| 2023-07 | 43,126 | sostenido |
| 2023-08 | 43,789 | pico |
| 2023-09 | 37,122 | sostenido |
| 2023-10 | 31,953 | descenso |
| 2023-11 | 37,774 | repunte |
| 2023-12 | 15,939 | bajada brusca |
| 2024-06 | **1** | colapso operacion |
| 2025-09 | 6 | residual |
| 2025-10 | 13 | residual |

> **DIAGNOSTICO**: BYB tuvo operacion intensa todo 2023, colapso a partir
> de enero 2024 (entre dic-2023 con 16K y jun-2024 con 1 hay 6 meses sin
> registros). Despues actividad residual 2025. Refuerza L-010 (NAV no
> procesa ingresos BYB): el sync NAV se rompio entre 2023-2024 y nunca
> se recompuso. Hipotesis nueva Q-BYB-CORTE-2024 levantada.

## 4. Features ON/OFF

| Feature | Estado | Evidencia |
|---|---|---|
| Talla y color | **OFF (preparado, no migrado)** | tablas `talla`, `color`, `producto_talla_color` existen pero VACIAS (0 filas) |
| Verificacion etiquetas | **HALF-IMPLEMENTED** | 8 pedidos en estado `Verificado` PERO `trans_verificacion_etiqueta`, `verificacion_estado`, `log_verificacion_bof` NO EXISTEN en EC2 |
| License plate | **PARCIAL** | producto.genera_lp_old=21.8% (136/623), config_enc.genera_lp=True |
| Control lote | **ON** | producto.control_lote=73.4% (457/623), config_enc.control_lote=NULL |
| Control vencimiento | **ON** | producto.control_vencimiento=73.4%, coordinado con lote |
| Control peso | **OFF** | producto.control_peso=0%, peso_recepcion/despacho=0% |
| Interface SAP B1 | **OFF** | `interface_sap=False` |
| Interface NAV | **ON** | `nombre_ejecutable='NavSync.exe'`, `crear_recepcion_de_compra_nav=True`, `crear_recepcion_de_transferencia_nav=True` |
| Reservar UMBAS primero | **ON** | `reservar_umbas_primero=True` (unico cliente con esto en True) |
| Implosion/explosion automatica | **ON** | + `explosion_automatica_desde_ubicacion_picking=True` |
| `Ejecutar_En_Despacho_Automaticamente` | **OFF** | (los SAP lo tienen ON) |
| Reabasto sofisticado | **ON** | `excluir_ubicaciones_reabasto=True`, `considerar_paletizado_en_reabasto=True`, `considerar_disponibilidad_ubicacion_reabasto=True` (los SAP lo tienen False) |
| Modelo log SEGMENTADO | **OFF** | log unificado 203K vs segmentado total <50 filas |

## 5. Bodegas (2)

| IdBodega | Codigo | Nombre | `tipo_pantalla_*` |
|---|---|---|---|
| 2 | BA0002 | PRODUCTO TERMINADO | 0/0/0 |
| 24 | BA0024 | PRODUCTO TERMINADO DAÑADO | 0/0/0 |

> Esquema simple: una bodega de PT y una de PT dañado. NO hay materia
> prima (probablemente operacion solo de distribucion).

## 6. Interface ERP

- ERP: **NAV (Microsoft Dynamics NAV / Business Central)**
- Binario sync: `NavSync.exe` (sin sufijo cliente — primer cliente NAV
  visto, posiblemente original)
- `interface_sap = False`
- `crear_recepcion_de_compra_nav = True`
- `crear_recepcion_de_transferencia_nav = True`
- Confirma L-010: NAV no procesa los ingresos automaticamente desde
  el WMS, hay que crear documentos en NAV manualmente o por proceso.

## 7. Tablas exclusivas BYB (vs los otros 4 clientes)

```
+ clientes_control_calidad
+ color (vacia)
+ i_nav_conversion
+ i_nav_ped_traslado_det_120828    \  snapshots historicos con
+ i_nav_ped_traslado_enc_120828    /  fecha en el nombre
+ impresora_mensaje
+ log_error_wms_oc / pick / rec / ubic   ← solo 4 (no 6 como MAMPA)
+ menu_opcion
+ producto_lote_correccion        ← correccion de lote (manual?)
+ producto_talla_color (vacia)
+ rol_usuario_estado
+ talla (vacia)
+ tipo_etiqueta_detalle
+ ... +8 mas
```

## 8. Sub-perfiles internos

- BA0002 vs BA0024: ambas son PRODUCTO TERMINADO, una sana y una dañada.
  Comportamiento esperado identico salvo por el flag de calidad.
- 136 productos con `genera_lp_old=True` y 487 productos con `=False`.
  → Sub-perfil de productos **con LP** (paletizado) vs **sin LP** (suelto).
  Es el primer cliente que vemos con esa segmentacion (los otros 4
  tienen 100% genera_lp_old).

## 9. Diagnosticos abiertos BYB

- **Q-BYB-CORTE-2024 (NUEVO)**: por que BYB dejo de operar entre
  enero 2024 y mayo 2025? Hipotesis: el sync NAV se rompio definitivamente,
  o el cliente cambio de WMS, o operacion suspendida.
- **Q-BYB-VERIF-INCOMPLETA (NUEVO)**: 8 pedidos en estado `Verificado`
  sin las 3 tablas de soporte. ¿El estado se setea por estado machine
  pero no se registra detalle? ¿O las tablas existian y se borraron?
- **Q-BYB-OUTBOX-BACKLOG (NUEVO)**: 255,912 outbox sin enviar. ¿Es el
  efecto del corte 2024 (NavSync no proceso) o existe issue distinto?
- **Q-VERIF-BB**: clarificar si "BB" en el brain es lo mismo que BYB
  o es otro cliente distinto. Hasta ahora se usaba como sinonimo
  pero hay que confirmar.

## 10. Aprendizajes especificos BYB

1. **Primer cliente NAV** del set, confirma binario `NavSync.exe`
   (sin sufijo). Patron de naming: SAP usa `SAPBOSync<Cliente>.exe`,
   NAV usaba `NavSync.exe` generico.
2. **Operacion parada 2024**: el outbox muestra exactamente cuando
   colapso (entre dic-2023 y jun-2024). Util como caso de estudio
   para detectar problemas similares en otros clientes.
3. **Verificacion half-implemented**: el estado de pedido `Verificado`
   existe en `trans_pe_enc` pero las tablas de soporte
   (`trans_verificacion_etiqueta` etc) no se crearon. Sugiere que
   el modulo se desarrollo a medias y se freno.
4. **Talla y color preparado, no activo**: tablas creadas pero 0 filas.
   Sugiere que se intentaba activar pero no se completo. Antecedente
   de MAMPA (que SI activo el modulo).
5. **Modelo PRODUCT-CENTRIC con flags en NULL en config_enc**: BYB pone
   los control_* en producto, y deja el config_enc con NULL. Variante
   del Modelo B (BECOFARMA usa False, BYB usa NULL). NULL puede
   significar "no se evaluo nunca". Pendiente si es semanticamente
   distinto a False.
6. **Reabasto sofisticado**: BYB tiene `excluir_ubicaciones_reabasto`,
   `considerar_paletizado_en_reabasto`, `considerar_disponibilidad_ubicacion_reabasto`
   todos en True (los SAP los tienen en False). Sugiere que BYB explotaba
   el reabasto del WMS de forma intensiva.
7. **`reservar_umbas_primero=True`**: unico cliente con esa configuracion.
   Patron de reserva FIFO sobre las unidades base (UMBAS) antes que
   sobre presentaciones.
