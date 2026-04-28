# L-019 — `i_nav_config_enc` es la fuente maestra de capability flags

> Etiqueta: `L-019_PARAM_CONFIG-ENC-FUENTE-MAESTRA_APPLIED`
> Fecha: 29-abr-2026
> Origen: cierre Q-CAPABILITY-FLAG-VERIF parcial + analisis K7/BECOFARMA/MAMPA

## Hallazgo

La tabla `i_nav_config_enc` es la **fuente maestra de capability flags
por bodega** en TOMWMS. Tiene una fila por cada (empresa, bodega)
configurada. Los flags incluyen al menos:

### Flags de control core

| Flag | Tipo | Significado |
|---|---|---|
| `control_lote` | bit | Si la bodega valida lote |
| `control_vencimiento` | bit | Si la bodega valida vencimiento |
| `control_peso` | bit | Si la bodega valida peso |
| `genera_lp` | bit | **Genera license plate** (sin sufijo `_old`) |
| `IdTipoEtiqueta` | int | Tipo etiqueta default por bodega |
| `IdTipoRotacion` | int | FEFO/FIFO/LIFO por bodega |

### Flags de comportamiento operativo

| Flag | Significado |
|---|---|
| `rechazar_pedido_incompleto` | bit |
| `despachar_existencia_parcial` | bit |
| `convertir_decimales_a_umbas` | bit (UMBAS = unidad base) |
| `generar_pedido_ingreso_bodega_destino` | bit |
| `generar_recepcion_auto_bodega_destino` | bit |
| `equiparar_cliente_con_propietario_en_doc_salida` | bit |
| `push_ingreso_nav_desde_hh` | bit |
| `reservar_umbas_primero` | bit |
| `implosion_automatica` / `explosion_automatica` | bit |
| `Ejecutar_En_Despacho_Automaticamente` | bit |
| `excluir_recepcion_picking` | bit |
| `valida_solo_codigo` | bit ← clave para "pantalla simplificada" |
| `bodega_facturacion` | varchar |
| `bodega_prorrateo` / `bodega_prorrateo1` | varchar |
| `bodega_faltante` | varchar |
| `dias_vida_defecto_perecederos` | int |
| `requerir_centro_costo_obligatorio` | bit |

### Flags de interface ERP

| Flag | Significado |
|---|---|
| `interface_sap` | bit (SAP B1 ON/OFF) |
| `nombre_ejecutable` | varchar (`SAPBOSync<Cliente>.exe`) |
| `sap_control_draft_ajustes` | bit |
| `sap_control_draft_traslados` | bit |
| `inferir_bonificacion_pedido_sap` | bit |
| `crear_recepcion_de_compra_nav` | bit |
| `crear_recepcion_de_transferencia_nav` | bit |

## Por que importa

1. Buscar capability flags **empieza siempre aca**, no en `producto`.
2. Cada bodega puede tener config DISTINTA (CEDIS vs TIENDA en MAMPA).
3. Migracion de un cliente a otro: copiar este registro y ajustar.
4. **Fingerprint generator**: este registro define el 80% del perfil.

## Confirmaciones

- K7 BOD1: `interface_sap=True, nombre_ejecutable='SAPBOSyncKillios.exe', genera_lp=True, control_lote=True, control_vencimiento=True, control_peso=True`
- BECOFARMA GENERAL: `interface_sap=True, nombre_ejecutable='SAPBOSync.exe', genera_lp=True, control_lote=True, control_vencimiento=True, control_peso=False, dias_vida_defecto_perecederos=365`
- MAMPA CEDIS (b21): `interface_sap=True, nombre_ejecutable='SAPBOSyncMampa.exe', genera_lp=True, control_lote=False, control_vencimiento=False, control_peso=False, valida_solo_codigo=True`

## Cierra hipotesis

- Q-PRODUCTO-GENERA-LP-NUEVO (parcial): `genera_lp` SI existe (sin sufijo),
  pero a nivel BODEGA (`i_nav_config_enc.genera_lp`), no en `producto`.
  El producto sigue teniendo solo `genera_lp_old`. Modelo dual.
- Q-CAPABILITY-FLAG-VERIF (parcial): para verificacion ver L-021. Para
  el resto de capabilities, viven aca.

## Pendiente

- Ver si hay flags ADICIONALES en otras tablas (`producto`, `bodega`,
  `cliente_config`, `proveedor_config`) que no esten en este registro.
- Documentar el orden de precedencia: ¿que pasa si un flag esta en
  config_enc Y en producto y dan distinto? Ver L-020.
