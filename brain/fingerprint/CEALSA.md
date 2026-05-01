---
id: CEALSA
tipo: fingerprint
estado: vigente
titulo: Fingerprint CEALSA
clientes: [cealsa]
tags: [fingerprint, cliente/cealsa]
---

# Fingerprint CEALSA

> Etiqueta human-readable: `CEALSA_CLIENT_QAS-CEALSASYNC-PROPIO_APPLIED`
> Capturado: 29-abr-2026 desde EC2 `52.41.114.122,1437/IMS4MB_CEALSA_QAS`
>
> **Aclaracion EC2**: ambiente QAS (no productivo). El productivo real
> probablemente esta en laptop Erik o ambiente cliente. Validar fingerprint.

## 1. Macro-tag

> **CEALSA = ambiente QAS, ERP propio sincronizado con CEALSASync.exe
> (NO es SAP ni NAV directos), 2 bodegas, 1714 productos con modelo
> PRODUCT-CENTRIC HETEROGENEO (control_lote 44.9%, vencimiento 1.4%,
> peso 0.4%), 41 tablas exclusivas (modulo Polizas, kits, vistas
> especificas, cuadrillas), modulo verificacion etiquetas NO existe
> (sin tablas), outbox COMPLETAMENTE VACIO (0 filas).**

## 2. Identidad

| | |
|---|---|
| BD en EC2 | `IMS4MB_CEALSA_QAS` |
| Ambiente | **QAS** (no productivo, sin trafico real) |
| Total tablas | 351 |
| Total productos | 1,714 |
| Bodegas | 2 |
| Talla y color | NO usa (tablas no existen) |

## 3. Volumen operativo (medido 29-abr-2026)

| Tabla / metrica | Valor |
|---|---|
| `trans_pe_enc.Despachado` | **3,707** (TODOS, no hay otros estados) |
| outbox total | **0** (vacio) |
| outbox INGRESO | 0 |
| outbox SALIDA | 0 |
| outbox periodo | (no aplica) |
| `log_error_wms` | 3 (casi sin uso) |
| Modelo log segmentado | NO usa (solo unificado, 3 filas) |

> **NOTA**: 3,707 pedidos `Despachado` con outbox vacio sugiere que
> CEALSA-QAS se carga via SP/seeds para pruebas, no por flujo real
> que pase por el outbox.

## 4. Features ON/OFF

| Feature | Estado | Evidencia |
|---|---|---|
| Talla y color | **OFF (no preparado)** | tablas `talla`, `color`, `producto_talla_color` NO existen |
| Verificacion etiquetas | **OFF (no implementado)** | ninguna de las 3 tablas existe |
| License plate | **ON** | `i_nav_config_enc.genera_lp=True`, producto.genera_lp_old=100% |
| Control lote | **MIXTO HETEROGENEO** | producto.control_lote=44.9% (769/1714), config_enc.control_lote=False |
| Control vencimiento | **OFF en mayoria** | producto.control_vencimiento=1.4% (24/1714) |
| Control peso | **OFF en mayoria** | producto.control_peso=0.4% (6/1714), coord. con peso_recepcion/despacho |
| Interface SAP B1 | **OFF** | `interface_sap=False` |
| Interface NAV | **OFF** | `crear_recepcion_de_compra_nav=False`, `crear_recepcion_de_transferencia_nav=False` |
| ERP propio | **ON** | `nombre_ejecutable='CEALSASync.exe'` (binario propio) |
| Acuerdos comerciales | **ON** | `IdAcuerdoEnc=1` + tablas `i_nav_acuerdo_productos`, `cealsa_vwacuerdocomercial*` |
| Cuadrillas | **ON** | tablas `cuadrilla_enc/det_*/tipo` (igual que MAMPA) |
| Stock por jornada | **ON** | tablas `stock_jornada_*` (igual que MAMPA) |
| Modulo Polizas | **ON (CEALSA-only)** | tablas `Polizas_CEALSA`, `Polizas_Ilegibles` |
| Modulo Kits | **ON (CEALSA-only)** | tabla `producto_kit_composicion` |
| Equiparar cliente con propietario | **ON** | igual BECOFARMA |

## 5. Bodegas (2)

| IdBodega | Codigo | Nombre | `tipo_pantalla_*` | Confirmar Erik |
|---|---|---|---|---|
| (a confirmar) | (a confirmar) | (a confirmar) | (a confirmar) | (datos no expandidos en query inicial) |

> **Pendiente**: query especifica para listar las 2 bodegas de CEALSA
> con todos sus campos. La consulta inicial salio truncada.

## 6. Interface ERP

- ERP: **propio del cliente CEALSA** (no SAP ni NAV identificables
  directamente).
- Binario sync: `CEALSASync.exe` ← NUEVO PATRON. Hasta ahora teniamos:
  - `SAPBOSync<Cliente>.exe` para SAP B1 (Killios, Mampa)
  - `SAPBOSync.exe` generico (BECOFARMA, caso historico)
  - `NavSync.exe` para NAV (BYB)
  - `<Cliente>Sync.exe` para ERP propio (CEALSA — primer caso)
- `interface_sap = False`, `crear_recepcion_de_*_nav = False` →
  realmente no usa los flags estandar.
- Hipotesis: CEALSA tiene un sistema custom (quiza Oracle, quiza ERP
  propio del cliente) que se sincroniza via CEALSASync.exe con un
  protocolo adaptado.

## 7. Tablas exclusivas CEALSA (vs los otros 4 clientes) — 41 tablas

```
MODULO POLIZAS (CEALSA-only):
+ Polizas_CEALSA
+ Polizas_Ilegibles

MODULO KITS:
+ producto_kit_composicion

VISTAS DEL ERP PROPIO (cliente-prefixed):
+ cealsa_vwacuerdocomercialdet
+ cealsa_vwacuerdocomercialenc
+ cealsa_vwclientes

CATALOGO BC:
+ cbcatalogoproductos       ← prefijo "cb" sospechoso (Business Central?)

WIZARD CREACION DE BODEGAS (CEALSA-only):
+ crear_bodega_sector
+ crear_bodega_tramo
+ crear_bodega_ubicacion

COMPARTIDAS CON MAMPA:
+ Listado_General
+ consolidador
+ cuadrilla_det_montacarga / det_operador / enc / tipo
+ detalle_de_movimiento
+ equiparacion_ubicaciones
+ existencia
+ faltantes
+ i_nav_acuerdo_productos
+ i_nav_unidad_medida
+ licencia_pendientes
+ stock_jornada_consecutivo / desfase

OPERACION:
+ TMP_UBICACIONES_FALTANES   ← typo en "FALTANTES"
+ pendiente_jornada
+ log_portal_ux
+ reset_portal_ux

LICENCIAS (modelo nuevo, lowercase):
+ licencias                   ← lowercase, distinto a BECOFARMA "Licencias" PascalCase
```

## 8. Sub-perfiles internos

- Productos con `control_lote=True` (44.9%) vs sin (55.1%): heterogeneidad
  alta a nivel producto. Sugiere catalogo mixto entre productos que
  necesitan trazabilidad por lote y productos commodity.
- 24 productos con `control_vencimiento=True` (1.4%): subset muy chico
  con vencimiento — productos especiales.
- 6 productos con `control_peso=True` (0.4%): subset minusculo con
  control de peso — granel especifico.

## 9. Diagnosticos abiertos CEALSA

- **Q-CEALSA-OUTBOX-VACIO (NUEVO)**: ¿por que outbox=0? Posibilidades:
  (a) ambiente QAS sin datos reales,
  (b) outbox se procesa y se purga inmediatamente,
  (c) CEALSASync.exe usa otra tabla de outbox.
- **Q-CEALSA-CEALSASYNC-ERP (NUEVO)**: ¿que ERP destino tiene CEALSASync.exe?
  No es SAP ni NAV. ¿Es BC, Oracle, sistema propio del cliente?
- **Q-CEALSA-LICENCIAS-LOWERCASE (NUEVO)**: tabla `licencias` (lowercase)
  vs BECOFARMA `Licencias` (PascalCase) vs `licencia_*` (snake legacy).
  ¿3 modelos coexisten en el codigo? Critico para Q-LICENCIAS-MODELO-NUEVO.

## 10. Aprendizajes especificos CEALSA

1. **Cuarto patron de naming del sincronizador**: `<Cliente>Sync.exe`
   para ERPs propios del cliente. Antes teniamos solo SAP/NAV genericos.
   Ahora un patron de naming generalizado:
   - `SAPBOSync.exe` / `SAPBOSync<Cliente>.exe` → SAP B1
   - `NavSync.exe` / `Nav<Cliente>Sync.exe` → NAV
   - `<Cliente>Sync.exe` → propio del cliente
   Ver L-022 nuevo.
2. **CEALSA es hibrido entre MAMPA y BECOFARMA**: usa cuadrillas
   y stock_jornada (como MAMPA), pero sin talla/color y sin
   verificacion (como BECOFARMA). Es un perfil intermedio.
3. **Modulo de Polizas (CEALSA-only)**: probablemente para transporte
   o seguros. Importante para clientes que necesitan trazabilidad
   de polizas con cargas.
4. **Wizard de creacion de bodegas** (`crear_bodega_sector/tramo/ubicacion`):
   CEALSA tiene tooling para bootstrap de bodegas. Util como referencia
   si necesitamos implementar setup automatizado en otros clientes.
5. **Vistas con prefijo cliente** (`cealsa_vw*`): patron interesante
   para integraciones a medida — vistas que adaptan el catalogo
   estandar a las nominaciones del cliente.
6. **Outbox vacio en QAS** sugiere que el ambiente se puebla por SP
   directos para pruebas. No es comparable directamente a los volumenes
   PRD de los otros clientes.
7. **PascalCase `licencias` (lowercase) coexiste con `licencia_*`
   (snake legacy)**. BECOFARMA tiene `Licencias` (PascalCase con L
   mayuscula). Esto sugiere que **HAY 3 MODELOS de licencias en juego**
   y que cada cliente esta en una etapa distinta de migracion.
