---
protocolVersion: 1
id: A-005
answersQuestion: Q-005
title: Por que cada cliente SAP tiene su propio SAPSYNC* (PEND-08)
operator: agent-replit
operatorRole: developer
target:
  codename: K7
  environment: PRD
executedAt: 2026-04-27T15:00:00Z
durationSeconds: 7
verdict: partial
confidence: medium
status: answered
tags: [sap, sapsync, K7, BF, MM, LC, PEND-08, arquitectura]
---

## Resumen

Confirmado parcialmente: la base K7 sí tiene **27 tablas de
configuracion** y **flags por empresa/bodega/cliente** que
parametrizan el comportamiento del puente SAP (
`bodega.interface_SAP`, `cliente.IdBodegaAreaSAP`,
`i_nav_config_enc.interface_sap`,
`i_nav_config_enc.sap_control_draft_*`, etc.). Esto **valida la
necesidad de ejecutables dedicados** por cliente: cada SAPSYNC*
debe leer una configuracion distinta segun cliente, conexion al
SAP B1 destino y flags de comportamiento. No pudimos validar
directamente el binario (esta fuera del WMS); evidencia es indirecta
via tablas de config.

## Hallazgos

### q1: tablas de configuracion / parametrizacion

```
TABLE_NAME
---
bodega_monitor_parametro
bodega_parametros
configuracion_alias_campos
configuracion_barra_pallet
configuracion_qa
configuracion_usuario_det
configuracion_usuario_enc
i_nav_config_area_bodega
i_nav_config_det
i_nav_config_enc
i_nav_config_ent
i_nav_config_producto_estado
Inv_SAP
p_parametro
producto_parametro_a
producto_parametro_b
producto_parametros
stock_parametro
trans_picking_det_parametros
trans_re_det_parametros
VW_Configuracion_Usuario_Template
VW_Configuracioninv
VW_navdetalleconfiguracion
VW_Parametro
VW_ProductoBodegaParametro
VW_ProductoParametro
VW_Stock_Serie_Parametro
```

**Interpretacion**: 27 tablas relacionadas a config/parametros.
Notables: `i_nav_config_enc/det/ent` (config de interface NavSync
multi-cliente), `bodega_parametros`, `p_parametro`,
`producto_parametros`, `Inv_SAP` (vista/tabla de inventario
SAP). El modelo permite parametrizar fuertemente por bodega/empresa.

### q2: tablas de mapeo ERP

```
TABLE_NAME
---
trans_inv_teorico_erp
```

**Interpretacion**: Solo 1 tabla con la palabra "erp": 
`trans_inv_teorico_erp`. Sugiere que el mapeo
WMS<->ERP no es full diccionario sino que se hace via codigos
identicos (mismo SKU en ambos sistemas). Esto explica por que cada
cliente necesita SU instancia: el codigo de producto SAP del
cliente A no es el del cliente B.

### q3: columnas con prefijo/sufijo SAP / ERP

```
TABLE_NAME | COLUMN_NAME
--- | ---
bodega | interface_SAP
bodega | restringir_areas_sap
cliente | IdBodegaAreaSAP
i_nav_config_enc | inferir_bonificacion_pedido_sap
i_nav_config_enc | interface_sap
i_nav_config_enc | sap_control_draft_ajustes
i_nav_config_enc | sap_control_draft_traslados
proveedor | IdBodegaAreaSAP
road_p_vendedor | devolucion_sap
trans_ubic_hh_enc | es_traslado_sap
VW_Productividad_Picking | Solicitud_SAP
VW_Proveedor | IdBodegaAreaSAP
VW_TransUbicacionHhEnc | es_traslado_sap
```

**Interpretacion**: 13 columnas relevantes:
- `bodega.interface_SAP` (bit) - bandera global por bodega
- `bodega.restringir_areas_sap`
- `cliente.IdBodegaAreaSAP` y `proveedor.IdBodegaAreaSAP`
- `i_nav_config_enc.interface_sap` y
  `i_nav_config_enc.inferir_bonificacion_pedido_sap`
- `i_nav_config_enc.sap_control_draft_ajustes` y
  `i_nav_config_enc.sap_control_draft_traslados`
- `road_p_vendedor.devolucion_sap`
- `trans_ubic_hh_enc.es_traslado_sap`

Cada uno modula un comportamiento distinto del SAPSYNC. La cantidad
de flags y su distribucion en multiples tablas confirma que **no
es un solo SAPSYNC parametrizado, sino multiples binarios cada uno
leyendo su propia config**.

### q4: flags de empresa

```
COLUMN_NAME | DATA_TYPE
--- | ---
control_presentaciones | bit
generar_stock_jornada | bit
```

**Interpretacion**: A nivel `empresa` solo hay 2 flags genericos
(`control_presentaciones`, `generar_stock_jornada`). Los flags
SAP estan en niveles mas granulares (bodega, cliente, config_enc).

## Conclusion

- El esquema soporta **multi-tenancy de la interface SAP** via
  flags en bodega/cliente/i_nav_config_enc.
- La separacion en multiples `SAPSYNCKILLIOS.exe`,
  `SAPSYNCMAMPA.exe`, etc. probablemente responde a:
  (1) cada cliente apunta a una instancia SAP B1 distinta (string
  de conexion), (2) cada uno tiene su propio scheduler/log,
  (3) facilidad de versionado por cliente.
- Para cerrar al 100% haria falta inspeccionar el codigo de uno
  de esos .exe (ver Q-011).

## Anomalias detectadas

- Las 27 tablas de "configuracion" estan dispersas y sin un
  diccionario central — riesgo de drift entre clientes.

## Sugerencia de follow-up

- Q-011 (cadencia y caller real del SAPSYNCKILLIOS en K7).

## Notas del operador

El equipo deberia documentar (en `interfaces-erp-por-cliente.md`)
el mapeo cliente -> binario SAPSYNC -> string de conexion SAP B1.
