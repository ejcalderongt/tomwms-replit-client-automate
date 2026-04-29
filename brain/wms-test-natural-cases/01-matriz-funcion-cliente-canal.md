# 01 — Matriz "función de reserva × canal × cliente"

> **Mapa maestro**. Permite saber, dado un canal de entrada (BOF, HH, NAV, SAP, MI3, Reabasto), qué función de reserva se invoca y bajo qué condiciones.

## Inventario completo de funciones de `clsLnStock_res_Partial.vb` (branch `dev_2028_merge`)

| Función | Línea aprox | Variantes / overloads | Categoría |
|---------|------------|----------------------|-----------|
| `Reserva_Stock` | 138 | 1 | core base |
| `Reserva_Stock_NAV_BYB` | 442 | 1 | adapter cliente-específico |
| `Reserva_Stock_Lista_Result` | 858 | 1 | con lista de lotes específica |
| `Reserva_Stock_Especifico` | 1459, 1612 | 2 | reserva contra IdStock target |
| `Reservar_Stock_By_IdStock` | 1800, 2409, 2658 | 3 | reserva directa post-decision |
| `Reservar_Stock_By_Stock_Reem` | 2089 | 1 | reserva con reemplazo paralelo |
| `Reemplazar_IdStock_By_Stock` | 2897, 3064 | 2 | swap reservado |
| `Reemplazar_ListaPu_By_Stock` | 3201 | 1 | swap por lista PU |
| `Reemplazar_ListaPu_By_Stock_Sin_Exist` | 3347 | 1 | swap forzado sin existencia |
| `Modificar_PickingUbic_By_Reem` | 3526 | 1 | mantenimiento post-reemplazo |
| `Reemplazo_Verificacion_By_ListPickingUbic` | 3654 | 1 | reemplazo en verificación |
| `Reemplazo_Automatico` | 3801 | 1 | swap auto (HH-driven) |
| `Reemplazo_Automatico_Conso` | 3963 | 1 | swap auto consolidado |
| `ReemplazarAuto_IdStock_By_Stock` | 4097 | 1 | swap auto target |
| `Reservar_Y_Reemplazar_Stock_By_IdStock` | 4238 | 1 | combo reserva+reemplazo |
| **`Reserva_Stock_From_Reabasto`** | **9856** | 1 | adapter origen reabasto |
| **`Reserva_Stock_From_MI3`** | **18192** | 1 | adapter origen MI3 |
| **`Reserva_Stock_From_SAP`** | **26680** | 1 | adapter origen SAP |

Total funciones únicas: **18+**. Total líneas archivo: **>26.680**.

## Matriz canal → función

| Canal de origen | Archivo caller | Línea | Función invocada | Notas |
|----------------|----------------|-------|------------------|-------|
| **Pedido NAV/MI3 (recepción)** | `clsLnTrans_pe_det_Partial.vb` | 2431 | `Reserva_Stock_Lista_Result` | con lista de lotes específica |
| **Pedido normal** | `clsLnTrans_pe_det_Partial.vb` | 3713 | `Reserva_Stock` | core, con/sin Clavaud |
| **Pedido específico (lote target)** | `clsLnTrans_pe_det_Partial.vb` | 3818 | `Reserva_Stock_Especifico` | NAV pre-asigna lote |
| **Pedido vía MI3** | `clsLnTrans_pe_det_Partial.vb` | 4048, 4181 | `Reserva_Stock_From_MI3` | adapter MI3 |
| **HH crea picking** | `clsLnTrans_picking_ubic_Partial.vb` | 6187 | `Reservar_Stock_By_IdStock` | operador en piso |
| **HH reporta cantidad dañada** | `clsLnTrans_picking_ubic_Partial.vb` | 6348 | `Reservar_Stock_By_IdStock` | re-reserva diferencia |
| **Reabasto picking** | `clsLnTrans_reabastecimiento_log_partial.vb` | 729 | `Reserva_Stock_From_Reabasto` | adapter reabasto |
| **HH ubicación SAP** | `clsLnTrans_ubic_hh_det.vb` | 579 | `Reserva_Stock_From_SAP` | adapter SAP |
| **NAV envío almacén (auto-explosión)** | `clsSyncNavEnvioAlm.vb` | 1194, 2750 | flag `Explosion_Automatica` | dispara reserva +explosión |
| **WS HH ResHHRN** | `WSHHRN/TOMHHWS.asmx.vb` | 4423 | `Reservar_Stock_By_IdStock` | endpoint HH |
| **WS HH ResHHRN** | `WSHHRN/TOMHHWS.asmx.vb` | 4498 | `Reemplazar_IdStock_By_Stock` | swap desde HH |
| **WS HH ResHHRN** | `WSHHRN/TOMHHWS.asmx.vb` | 4578, 4685 | `Reemplazar_ListaPu_By_Stock` | swap por PU |
| **WS HH ResHHRN** | `WSHHRN/TOMHHWS.asmx.vb` | 4670 | `Reemplazo_Verificacion_By_ListPickingUbic` | en verificación |
| **WS HH ResHHRN (auto)** | `WSHHRN/TOMHHWS.asmx.vb` | 4752, 4767 | `Reemplazo_Automatico*` | swap auto |
| **WS HH ResHHRN combo** | `WSHHRN/TOMHHWS.asmx.vb` | 4897 | `Reservar_Y_Reemplazar_Stock_By_IdStock` | combo |
| **Form reemplazo** | `frmCantidadreemplazo.vb` | 337, 353, 426, 440, 523 | varias `Reemplazar_*` | UI BOF |

## Patrón identificado

### Funciones `_From_<CANAL>`
Son **adapters** específicos por canal de origen. Cada uno hace pre/post-procesamiento antes/después de delegar al core. Ejemplos:
- `From_MI3`: probablemente normaliza códigos de bodega del ERP (NAV/SAP) a IdBodega WMS
- `From_SAP`: maneja `sap_control_draft_*` y referencia drafts
- `From_Reabasto`: re-reserva stock recién movido a picking, marca origen reabasto

### Funciones `Reservar_*_By_IdStock`
Usadas cuando el caller ya decidió el `IdStock` exacto (típicamente HH después de escanear). Saltea el ranking FIFO/FEFO porque ya está elegido.

### Funciones `Reemplazar_*` y `Reemplazo_*`
**Siempre desde HH** (web service `TOMHHWS.asmx.vb` o form `frmCantidadreemplazo`). El operador en piso es quien dispara el reemplazo cuando lo que el WMS sugirió no se puede tomar (lote dañado, faltante físico, vencido, ubicación bloqueada).

→ **El reemplazo NO es automático del WMS**. Es operador-driven. La función `Reemplazo_Automatico` se llama "automático" porque automatiza la **decisión** de qué stock alternativo elegir, no porque el WMS lo dispare solo.

## Matriz cliente → función específica conocida

| Cliente | Función específica | Razón |
|---------|--------------------|-------|
| **BECO + NAV** (BYB) | `Reserva_Stock_NAV_BYB` | LP largo correlativo NAV requiere matching especial (ver Wave 6.2) |
| **CEALSA** (3PL) | core `Reserva_Stock` con `Operador_logistico=True` | config por (IdBodega, IdEmpresa) |
| **MERHONSA, MERCOPAN** (IDEALSA) | core `Reserva_Stock` + `Conservar_Zona_Picking_Clavaud` (MERCOPAN) | Clavaud nace en Panamá |
| **K7, MAMPA** | core `Reserva_Stock` con `Operador_logistico=False` | config por (IdBodega, IdPropietario) |
| **Cualquiera con SAP** | `Reserva_Stock_From_SAP` | flag `interface_sap=True` |

## Q-* abiertas

- **Q-FROM-MI3-DIFF** (alta) — qué hace `Reserva_Stock_From_MI3` distinto del core. Hipótesis: maneja códigos ERP, convierte unidades NAV→WMS, registra trazabilidad de origen.
- **Q-FROM-SAP-DIFF** (alta) — idem para SAP. Hipótesis: maneja `sap_control_draft_*`.
- **Q-FROM-REABASTO-DIFF** (media) — `Reserva_Stock_From_Reabasto` debe re-reservar stock que acaba de llegar a picking. ¿Cómo evita el conflicto con el reabasto en curso?
- **Q-NAV-BYB-WHY** (media) — ¿por qué BECO/BYB requiere función propia? ¿Es solo el LP NAV o hay más diferencias?
- **Q-REEMPLAZO-WHO-DECIDES** (media) — `Reemplazo_Automatico` decide automáticamente la alternativa, pero ¿quién dispara la llamada? ¿Es el HH cuando detecta faltante o es proactivo del WMS?
