---
id: README
tipo: heat-map-params
estado: vigente
titulo: Capa 1 — Parametros a nivel EMPRESA
tags: [heat-map-params]
---

# Capa 1 — Parametros a nivel EMPRESA

> Granularidad logica: empresa. La tabla fisica `i_nav_config_enc`
> tiene 1 fila por bodega, pero varios campos son consistentes a
> nivel empresa (decimales, ejecutable Sync, interfaces ERP).

## Tabla origen

`i_nav_config_enc` (63 cols en BYB)

## Filas por cliente

| Cliente | Filas (1 por bodega) | Empresas | Bodegas | Propietarios |
|---|---|---|---|---|
| BECOFARMA | 1 | 1 | 1 | 1 |
| K7 | 6 | 1 | 6 | 1 |
| MAMPA | 33 | 1 | 33 | 1 |
| BYB | 2 | 1 | 2 | 1 |
| CEALSA | 2 | 1 | 2 | 1 |

> Cada cliente tiene 1 sola empresa. Los parametros "empresa" se
> extraen de la primera fila o del consenso entre filas.

## Catalogo de parametros — funcionales (inferido del nombre)

### Decimales y unidades
| Col | Tipo | Funcion | Estandar inferido |
|---|---|---|---|
| `convertir_decimales_a_umbas` | int | Si convierte decimales a UMBas (probable Si/No/Forzar) | varia |

### Sync con ERP (interface)
| Col | Tipo | Funcion |
|---|---|---|
| `nombre_ejecutable` | nvarchar(50) | Nombre del ejecutable Sync (NavSync.exe, CEALSASync.exe, SAPBOSync.exe, etc.) |
| `interface_sap` | bit | Tiene interface SAP |
| `sap_control_draft_ajustes` | bit | Maneja drafts de ajustes en SAP |
| `sap_control_draft_traslados` | bit | Maneja drafts de traslados en SAP |
| `crear_recepcion_de_compra_nav` | bit | Crea recepcion de compra desde NAV |
| `crear_recepcion_de_transferencia_nav` | bit | Crea recepcion de transferencia desde NAV |
| `push_ingreso_nav_desde_hh` | bit | Empuja ingreso a NAV desde HH |
| `inferir_bonificacion_pedido_sap` | bit | Infere bonificacion en pedidos SAP |
| `rechazar_bonificacion_incompleta` | bit | Rechaza bonificacion si esta incompleta |
| `equiparar_productos` | bit | Equipara productos entre WMS y ERP |

### Defaults para creacion de productos (cuando no vienen del ERP)
| Col | Tipo | Funcion |
|---|---|---|
| `IdProductoEstado` | int | Estado por defecto al recibir |
| `idFamilia` | int | Familia por defecto |
| `idclasificacion` | int | Clasificacion por defecto |
| `idMarca` | int | Marca por defecto |
| `idTipoProducto` | int | Tipo de producto por defecto |
| `IdTipoEtiqueta` | int | Tipo de etiqueta por defecto |
| `IdTipoRotacion` | int | Tipo de rotacion (FIFO/FEFO/etc) |
| `IdIndiceRotacion` | int | Indice de rotacion |
| `control_lote` | bit | Default control de lote |
| `control_vencimiento` | bit | Default control de vencimiento |
| `control_peso` | bit | Default control de peso |
| `genera_lp` | bit | Default genera LP |
| `dias_vida_defecto_perecederos` | int | Vida util default para perecederos |

### Politica de pedidos
| Col | Tipo | Funcion |
|---|---|---|
| `rechazar_pedido_incompleto` | int | Politica si pedido esta incompleto |
| `despachar_existencia_parcial` | int | Permite despacho parcial |
| `reservar_umbas_primero` | bit | Reserva UMBas antes que presentaciones |
| `Ejecutar_En_Despacho_Automaticamente` | bit | Auto-ejecuta despacho |
| `excluir_recepcion_picking` | bit | Excluye RECEPCION de las ubicaciones de picking |

### Reabasto
| Col | Tipo | Funcion |
|---|---|---|
| `excluir_ubicaciones_reabasto` | bit | Excluye ciertas ubicaciones del reabasto |
| `considerar_paletizado_en_reabasto` | bit | Considera paletizado al reabastecer |
| `considerar_disponibilidad_ubicacion_reabasto` | bit | Considera disponibilidad de ubic destino |

### Explosion / Implosion
| Col | Tipo | Funcion |
|---|---|---|
| `explosion_automatica` | bit | Explota presentaciones automaticamente |
| `implosion_automatica` | bit | Implosion automatica (juntar presentaciones menores en mayor) |
| `explosion_automatica_desde_ubicacion_picking` | bit | Explota desde ubicacion de picking |
| `explosion_automatica_nivel_max` | int | Nivel maximo de explosion |

### Produccion
| Col | Tipo | Funcion |
|---|---|---|
| `codigo_proveedor_produccion` | nvarchar(50) | Codigo del proveedor que es la planta de produccion (recibe produccion!) |

### Especifico CLAVAUD (revisar)
| Col | Tipo | Funcion |
|---|---|---|
| `conservar_zona_picking_clavaud` | bit | Caso especial CLAVAUD (cliente futuro o legacy?) |

### Productos NO COMERCIALES
| Col | Tipo | Funcion |
|---|---|---|
| `codigo_bodega_erp_nc` | nvarchar(50) | Codigo bodega ERP para no comerciales |
| `lote_defecto_entrada_nc` | nvarchar(50) | Lote default para no comerciales |
| `vence_defecto_nc` | datetime | Fecha vencimiento default para no comerciales |
| `IdProductoEstado_NC` | int | Estado para no comerciales |

### Transferencias
| Col | Tipo | Funcion |
|---|---|---|
| `IdTipoDocumentoTransferenciasIngreso` | int | Tipo doc default para transferencias de ingreso |
| `generar_pedido_ingreso_bodega_destino` | bit | Genera pedido de ingreso en bodega destino (caso WMS<->WMS) |
| `generar_recepcion_auto_bodega_destino` | bit | Genera recepcion automatica en bodega destino |
| `equiparar_cliente_con_propietario_en_doc_salida` | bit | Equipara cliente=propietario en docs de salida |

### Acuerdos comerciales
| Col | Tipo | Funcion |
|---|---|---|
| `IdAcuerdoEnc` | int | ID del acuerdo comercial padre |

### Otros
| Col | Tipo | Funcion |
|---|---|---|
| `recepcion_genera_historico` | bit | Recepcion genera historico |
| `valida_solo_codigo` | bit | Valida solo por codigo (no por LP) |
| `bodega_facturacion` | nvarchar(50) | Bodega de facturacion |
| `bodega_prorrateo` / `bodega_prorrateo1` | nvarchar(50) | Bodegas de prorrateo (caso K7!) |
| `rango_dias_importacion` | int | Rango de dias para importar |

## Sample BYB (real, 29-abr-2026)

```
nombre_ejecutable=NavSync.exe
codigo_proveedor_produccion=1060315
IdProductoEstado=1
IdTipoEtiqueta=2
IdTipoRotacion=3
IdTipoDocumentoTransferenciasIngreso=8
explosion_automatica_nivel_max=1
convertir_decimales_a_umbas=1
despachar_existencia_parcial=1

Flags ON (13):
  generar_pedido_ingreso_bodega_destino
  generar_recepcion_auto_bodega_destino
  genera_lp
  crear_recepcion_de_transferencia_nav
  crear_recepcion_de_compra_nav
  implosion_automatica
  explosion_automatica
  reservar_umbas_primero
  explosion_automatica_desde_ubicacion_picking
  conservar_zona_picking_clavaud
  excluir_ubicaciones_reabasto
  considerar_paletizado_en_reabasto
  considerar_disponibilidad_ubicacion_reabasto

Flags OFF (11):
  equiparar_cliente_con_propietario_en_doc_salida
  push_ingreso_nav_desde_hh
  Ejecutar_En_Despacho_Automaticamente
  interface_sap
  sap_control_draft_ajustes
  sap_control_draft_traslados
  inferir_bonificacion_pedido_sap
  rechazar_bonificacion_incompleta
  equiparar_productos
  valida_solo_codigo
  excluir_recepcion_picking
```

## Cross-cliente (PENDIENTE — proxima iteracion)

Tabla matriz `flag x cliente`:

| Flag | BECOFARMA | K7 | MAMPA | BYB | CEALSA |
|---|---|---|---|---|---|
| nombre_ejecutable | ? | ? | ? | NavSync.exe | CEALSASync.exe |
| interface_sap | ? | ? | ? | False | ? |
| explosion_automatica | ? | ? | ? | True | ? |
| ... (63 cols) | | | | | |

## Implicaciones para WebAPI .NET 10

- `nombre_ejecutable` → mapeo cliente → adaptador ERP (clase generica
  `IErpSyncAdapter`).
- `interface_sap` → flag de routing al adaptador SAP vs NAV.
- `convertir_decimales_a_umbas` → politica de conversion en el dominio.
- `codigo_proveedor_produccion` → cliente recibe produccion → habilita
  flujo `recepcion-produccion` distinto de `recepcion-compra`.
- `explosion/implosion_automatica` → capability opcional habilitada
  por config.
