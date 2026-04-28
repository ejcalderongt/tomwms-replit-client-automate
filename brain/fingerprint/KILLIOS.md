# Fingerprint KILLIOS (K7)

> Etiqueta human-readable: `KILLIOS_CLIENT_GASTRONOMICO-SAP-B1_APPLIED`
> Capturado: 29-abr-2026 desde EC2 `52.41.114.122,1437/TOMWMS_KILLIOS_PRD`
> Aclaracion EC2: copia parcial. Outbox solo hasta 19-ago-2025 (8 meses
> de desfase respecto al hoy). La productiva real esta en laptop Erik.

## 1. Macro-tag

> **KILLIOS = gastronomico SAP B1, 6 bodegas con concepto de prorrateo,
> productos perecederos con lote y vencimiento, modulo verificacion ON
> (confirmado screenshot 29-abr), modelo configuracion mixto
> (producto + i_nav_config_enc), 319 productos en EC2 desfasados.**

## 2. Identidad

| | |
|---|---|
| BD en EC2 | `TOMWMS_KILLIOS_PRD` |
| Ambiente | PRD (productivo, pero copia desfasada en EC2) |
| Total tablas | 345 |
| Total productos | 319 (EC2 desfasado) |
| Talla y color | NO usa (`bodega.control_talla_color=False` en todas) |

## 3. Volumen operativo (medido 29-abr-2026, datos EC2 hasta ago-2025)

| Tabla / metrica | Valor |
|---|---|
| `trans_pe_enc.estado=Despachado` | 3,989 (~96%) |
| `trans_pe_enc.estado=Pickeado` | 86 |
| `trans_pe_enc.estado=Pendiente` | 73 |
| `trans_pe_enc.estado=Anulado` | 33 |
| `trans_pe_enc.estado=NUEVO` | 14 |
| `trans_pe_enc.estado=Verificado` | **7** (modulo verif activo) |
| outbox total | 24,193 |
| outbox enviado=1 | 24,190 (99.99% sano) |
| outbox enviado=0 | **3** (sync sano, no hay backlog) |
| outbox INGRESO | 4,394 |
| outbox SALIDA | 19,799 |
| outbox periodo | 02-jun-2025 a 19-ago-2025 |

## 4. Features ON/OFF

| Feature | Estado | Evidencia |
|---|---|---|
| Talla y color | **OFF** | `bodega.control_talla_color=False` en TODAS |
| Verificacion etiquetas | **ON** | screenshot Erik 29-abr + 7 pedidos `Verificado` (L-018, H27_FIX_K7-ON) |
| License plate (lic_plate) | **ON** | `i_nav_config_enc.genera_lp=True` + producto.genera_lp_old=100% |
| Control lote | **ON** | producto.control_lote=99.7% + config_enc.control_lote=True |
| Control vencimiento | **ON** | producto.control_vencimiento=87.8% + config_enc=True |
| Control peso | **OFF** | producto.control_peso=0% + config_enc.control_peso=True (parado en producto) |
| Interface SAP B1 | **ON** | `i_nav_config_enc.interface_sap=True`, `nombre_ejecutable='SAPBOSyncKillios.exe'` |
| Solo enteros (L-009) | **ON** | comportamiento documentado |
| Bodegas de prorrateo | **ON** | concepto exclusivo K7 (BOD2/3/5/6 son `Bodega de Prorateo`) |
| Implosion/explosion automatica | **ON** | `implosion_automatica=True`, `explosion_automatica=True` |
| `Ejecutar_En_Despacho_Automaticamente` | **ON** | |
| `excluir_recepcion_picking` | **ON** | |
| Modulo CRM (Auditoria/Contacto/Propuesta/Prospecto) | **PRESENTE** | tablas exclusivas K7 — uso real desconocido |

## 5. Bodegas (6)

| IdBodega | Codigo | Nombre | `control_talla_color` | `tipo_pantalla_verificacion` |
|---|---|---|---|---|
| 1 | BOD1 | Bodega Principal | OFF | 0 |
| 2 | PRTOK | Bodega de Prorateo Kilio | OFF | 0 |
| 3 | PRTO | Bodega de Prorateo Garesa | OFF | 0 |
| 4 | BOD5 | Bodega Amatitlan | OFF | 0 |
| 5 | PRTK17 | Bodega de Prorateo Kilio Z17 | OFF | 0 |
| 6 | PRT17 | Bodega de Prorateo Garesa Z17 | OFF | 0 |

> NOTA: TODAS tienen `tipo_pantalla_verificacion=0` pero la verificacion
> SI esta activa (screenshot K7 29-abr). El flag `tipo_pantalla_*` indica
> tipo de pantalla, NO ON/OFF. Ver L-021.

## 6. Interface ERP

- ERP: **SAP Business One**
- Binario sync: `SAPBOSyncKillios.exe` (cliente-especifico, ver L-015)
- `interface_sap=True` en `i_nav_config_enc.BOD1`
- `sap_control_draft_ajustes=False`, `sap_control_draft_traslados=False`

## 7. Tablas exclusivas K7 (vs BECOFARMA y MAMPA)

```
+ Auditoria             + Contacto              + Infraestructura
+ Inv_SAP               + Inv_WMS               + Organizacion
+ Propuesta             + Prospecto             ← modulo CRM/comercial
+ cod_barra_clc
+ diferencias_movimientos
+ i_nav_config_producto_estado
+ operador_montacarga
+ producto_presentacion_bk    ← backup
+ producto_subtarea / producto_subtarea_tipo  ← concepto subtarea K7-only
+ stock_20250515 / stock_20250606 / stock_20250624  ← snapshots historicos
+ stock_bodegas23 / stock_picking20250624 / stock_res_20250624
+ stock_res_ped_164    ← reservas dump puntual
+ tmp_licencia_item / tmp_stock_res
+ us_solic_det / us_solic_enc   ← solicitudes de usuario (workflow propio)
```

## 8. Sub-perfiles internos

- **BOD1 (Bodega Principal)**: bodega operativa nominal.
- **BOD2/3/5/6 (PRTOK/PRTO/PRTK17/PRT17)**: bodegas de **prorrateo**.
  Concepto K7-specific: el WMS rotea stock entre bodegas Kilio y Garesa
  (entidades del cliente) usando bodegas tampon de prorrateo.
- **BOD7 (bodega_facturacion)**: referenciada en config_enc como bodega
  de facturacion pero no aparece en `bodega` — pendiente confirmar.

## 9. Diagnosticos abiertos K7

- Q-VERIF-K7-PERIODOS: por que en algunos periodos K7 no usa verificacion
  (intermitencia documentada en sesiones previas).
- Q-EC2-DESFASE: outbox solo hasta ago-2025 mientras hoy es abr-2026.

## 10. Aprendizajes especificos K7

1. **Bodegas de prorrateo** son una funcionalidad K7-only para multi-entidad
   (Kilio, Garesa, Z17). Modelo replicable a otros clientes con multi-empresa.
2. **Modulo CRM** (Contacto/Propuesta/Prospecto/Auditoria/Organizacion)
   esta presente pero uso real desconocido. Posiblemente abandonado o
   solo usado por modulo comercial separado.
3. **Snapshots historicos de stock** (`stock_20250515`, etc) sugieren
   practica de hacer copias puntuales antes de ajustes grandes.
4. **Modelo configuracion MIXTO**: producto.control_lote=99.7% AND
   i_nav_config_enc.control_lote=True. Ambos lados activos. Es lo que
   el codigo lee es la interseccion: solo hace control si los DOS dicen
   true. Patron diferente a MAMPA (solo bodega) y BECOFARMA (solo producto).
   Ver L-020.
5. **Sync sano**: solo 3 de 24,193 outbox sin enviar. SAPBOSyncKillios.exe
   funcionando bien.
