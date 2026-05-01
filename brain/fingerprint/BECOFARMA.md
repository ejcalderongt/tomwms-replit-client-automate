---
id: BECOFARMA
tipo: fingerprint
estado: vigente
titulo: Fingerprint BECOFARMA
clientes: [becofarma]
tags: [fingerprint, cliente/becofarma]
---

# Fingerprint BECOFARMA

> Etiqueta human-readable: `BECOFARMA_CLIENT_FARMA-SAP-B1-PRODUCT-CENTRIC_APPLIED`
> Capturado: 29-abr-2026 desde EC2 `52.41.114.122,1437/IMS4MB_BECOFARMA_PRD`
> Outbox refrescado hasta 27-abr-2026 (productiva al dia). EC2 esta SYNC en BECOFARMA.

## 1. Macro-tag

> **BECOFARMA = farmaceutica SAP B1, UNA SOLA BODEGA (GENERAL),
> 1835 productos perecederos (97.4% lote+vencimiento), modulo
> verificacion ON con uso bajo, modelo configuracion product-centric,
> CUELLO DE BOTELLA EN DESPACHO desde W12-mar-2026 (H29), 31K outbox
> SIN ENVIAR (sync con SAP roto).**

## 2. Identidad

| | |
|---|---|
| BD en EC2 | `IMS4MB_BECOFARMA_PRD` |
| Ambiente | PRD (productivo) |
| Total tablas | 354 |
| Total productos | 1,835 |
| Bodegas | **1 sola** (`GENERAL`) |
| Talla y color | NO usa |

## 3. Volumen operativo (medido 29-abr-2026)

| Tabla / metrica | Valor |
|---|---|
| `trans_pe_enc.Despachado` | **4,411** |
| `trans_pe_enc.Pickeado` | **3,802** ← cuello de botella H29 |
| `trans_pe_enc.Pendiente` | 282 |
| `trans_pe_enc.Anulado` | 22 |
| `trans_pe_enc.NUEVO` | 18 |
| `trans_pe_enc.Verificado` | **4** (uso bajo del modulo) |
| outbox total | 36,576 |
| outbox enviado=1 | 5,313 (14.5%) |
| outbox enviado=0 | **31,263 (85.5% backlog)** |
| outbox INGRESO | 5,090 |
| outbox SALIDA | 31,486 |
| outbox periodo | 04-ene-2026 a 27-abr-2026 |
| `log_error_wms` | 52,418 (volumen alto) |
| `log_error_wms_pe` | 7,098 |
| `log_error_wms_rec` | 179 |
| `log_error_wms_oc` | 56 |
| `log_error_wms_pick` | 69 |
| `log_error_wms_reab` | 24 |
| `log_error_wms_ubic` | 295 |

## 4. Features ON/OFF

| Feature | Estado | Evidencia |
|---|---|---|
| Talla y color | **OFF** | bodega `GENERAL` `control_talla_color=False` |
| Verificacion etiquetas | **ON pero uso bajo** | 4 pedidos `Verificado` + `verificacion_estado` 2 filas + `log_verificacion_bof` 84 filas |
| License plate | **ON** | `i_nav_config_enc.genera_lp=True` + producto.genera_lp_old=100% |
| Control lote | **ON** | producto.control_lote=97.4% (1788/1835) + config_enc=True |
| Control vencimiento | **ON** | producto.control_vencimiento=97.4% (1787/1835) + config_enc=True |
| Control peso | **OFF** | producto.control_peso=0% + config_enc.control_peso=False |
| Interface SAP B1 | **ON** | `interface_sap=True`, `nombre_ejecutable='SAPBOSync.exe'` |
| Modelo log SEGMENTADO | **ON** | tablas `log_error_wms_pe/rec/oc/pick/reab/ubic` presentes |
| Pre-factura | **ON** (BECOFARMA-only) | tablas `trans_prefactura_enc/det/mov` exclusivas |
| Equiparar cliente con propietario | **ON** | `equiparar_cliente_con_propietario_en_doc_salida=True` |
| Vida defecto perecederos | 365 dias | `dias_vida_defecto_perecederos=365` |

## 5. Bodegas (1)

| IdBodega | Codigo | Nombre | `tipo_pantalla_*` |
|---|---|---|---|
| 1 | 01 | GENERAL | 0/0/0 (todas legacy) |

## 6. Interface ERP

- ERP: **SAP Business One**
- Binario sync: `SAPBOSync.exe` (sin sufijo cliente — caso historico)
- **CRITICO**: 31,263 outbox `enviado=0` desde enero 2026. El sync
  esta roto o muy retrasado. Esto es la **raiz de H29**: si SAP no
  recibe los DESPACHO, los pedidos quedan en `Pickeado` esperando.

## 7. Tablas exclusivas BECOFARMA (vs K7 y MAMPA)

```
+ Licencias                  ← PascalCase, modelo nuevo
+ LIcencias2                 ← typo "LI" en mayusculas, sospechoso
+ Presentacion_Factor        ← unidades de presentacion farma
+ pedidos_no_despachados     ← reporte/staging de H29
+ producto_presentacion1     ← variante de presentacion
+ trans_prefactura_enc       \
+ trans_prefactura_det        > MODULO PRE-FACTURA exclusivo BECOFARMA
+ trans_prefactura_mov       /
```

## 8. Sub-perfiles internos

- Una sola bodega → no hay sub-perfiles por bodega.
- Sub-perfil notable de PRODUCTO: 47 productos sin control_lote
  (1835 - 1788) — investigar si son productos especiales (no
  perecederos, kits, etc).

## 9. Diagnosticos abiertos BECOFARMA

- **H29 confirmado con datos**: 3,802 pedidos en `Pickeado` + 31,263
  outbox sin enviar. WAIT_CLIENT.
- Q-LICENCIAS-MODELO-NUEVO: las tablas `Licencias` y `LIcencias2`
  (PascalCase) sugieren migracion en curso desde el modelo viejo
  `licencia_*` (snake_case lowercase).
- Q-VERIF-BB: pendiente confirmar si BB es lo mismo que BECOFARMA o
  un cliente distinto.
- ¿Por que el typo en `LIcencias2`? — meta-pregunta para Erik.

## 10. Aprendizajes especificos BECOFARMA

1. **Modelo product-centric**: BECOFARMA pone los flags en `producto`
   (control_lote=97.4%, control_vencimiento=97.4%) y deja
   `i_nav_config_enc` con valores genericos. Inverso a MAMPA (todo en
   bodega) y K7 (mixto). Ver L-020.
2. **Cuello despacho desde W12-mar**: 3,802 pedidos pickeados sin cerrar
   y 31,263 outbox sin enviar es la firma del problema H29. La causa
   es `SAPBOSync.exe` no procesando o conexion SAP rota.
3. **Modulo pre-factura** es BECOFARMA-only. Sugiere que en farma se
   genera factura provisional antes del despacho fisico, pattern de
   negocio especifico del rubro.
4. **Modelo log segmentado** tambien presente en BECOFARMA (no solo
   MAMPA). Confirma adopcion como estandar.
5. **`Licencias` y `LIcencias2` (PascalCase)**: convention nueva
   coexistiendo con `licencia_*` (snake_case). Sugiere migracion en
   curso. Importante para Q-LICENCIAS-MODELO-NUEVO.
6. **`equiparar_cliente_con_propietario_en_doc_salida=True`** (mientras
   K7 lo tiene en False): BECOFARMA opera con un solo propietario
   logico, mientras K7 tiene multi-entidad (Kilio/Garesa).
