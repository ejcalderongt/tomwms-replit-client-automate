# Cliente CEALSA

> 3PL multi-cliente. **El motor de reserva del WMS NO se ejecuta por defecto**. Solo se invoca si la configuracion (`ReservaStock` por tipo de pedido) o el operador lo solicita explicitamente.

## Identificacion

- **Database**: `IMS4MB_CEALSA_QAS` (QAS pero con datos reales)
- **ERP**: sin ERP integrado
- **Rubro**: 3PL (logistica para terceros — almacenadora)
- **Bodegas**: 2 (1 general + 1 fiscal passthrough)

## Bodegas

| idBodega | Codigo | Tipo | Reserva activa |
|---|---|---|---|
| 1 | (general) | operativa | depende del tipo de pedido |
| 2 | B02 (BODEGA FISCAL) | passthrough fiscal | NO |

## Modelo operacional 3PL — IMPORTANTE

El WMS en una almacenadora **brinda servicios logisticos a otros**: recibe lo que dicen las **polizas de entrada** y saca lo que dicen las **polizas de salida**. En este sentido:

- **NO ejecuta mecanismos de reserva por defecto**.
- La reserva se activa solo si:
  - La configuracion del tipo de pedido lo dice (`trans_pe_tipo.ReservaStock=true`), o
  - El proceso solicita explicitamente la reserva.
- De lo contrario, **el operador elige discrecionalmente el stock a despachar bajo peticion del cliente**.

**Evidencia productiva**: `trans_pe_det_log_reserva` tiene **0 registros en CEALSA QAS**. El motor del WMS legacy nunca se invoco.

## Tipos de pedido (trans_pe_tipo)

| Id | Nombre | ReservaStock | control_poliza |
|---|---|---|---|
| 1 | PE0001 | SI | **SI** |
| 2 | PE0002 | SI | no |
| 3 | PE0003 | SI | no |
| 4 | PE0004 | NO | no |
| 5 | PE0005 | SI | **SI** |
| 7 | Requisicion | SI | no |
| 9 | PDV_NAV | SI | no |

`PE0001` y `PE0005` tienen `control_poliza=SI` → tipos exclusivos del modelo 3PL con poliza asociada en `trans_pe_pol`.

## Configuracion aprendida (i_nav_config_enc)

### Bodega general (id=1)

| Flag | Valor |
|---|---|
| `control_vencimiento` | true |
| `control_lote` | true |
| `IdTipoRotacion` | 3 (FEFO) |
| `equiparar_cliente_con_propietario_en_doc_salida` | **true** (3PL) |
| `interface_sap` | false |

### Bodega fiscal (id=2) — PASSTHROUGH

| Flag | Valor |
|---|---|
| `control_vencimiento` | **false** |
| `control_lote` | **false** |
| `control_peso` | false |
| `IdTipoRotacion` | **null** |
| `explosion_automatica` | false |
| `interface_sap` | false |
| `equiparar_cliente_con_propietario_en_doc_salida` | true |

Casi todos los flags estan apagados. Es bodega contable de transito fiscal: se usa para reflejar entradas/salidas legales (polizas) sin operacion fisica de reserva.

## Particularidades

- `equiparar_cliente_con_propietario_en_doc_salida=true`: en 3PL el cliente del documento ES el propietario del producto.
- **VW_Stock_Jornada** (74 cols): reporte canonico para auditoria diaria 3PL.
- **trans_prefactura_*** (enc 22, det 19, mov 13 cols): modelo de prefacturacion al cliente.
- **trans_pe_pol** (41 cols): polizas de importacion/exportacion (`bl_no`, `NoPoliza`, `viaje_no`, `buque_no`, `fecha_abordaje`, etc.) — **CRITICO para CEALSA**.
- **trans_pe_servicios** (9 cols): servicios facturables.
- **NO tiene proceso ni datos de reabastecimiento**.

## Implicancias para el bridge

- **Escenarios de reserva NO aplican por defecto** en CEALSA. Solo aplican si el tipo de pedido tiene `ReservaStock=true` Y el escenario lo solicita explicitamente.
- **La bodega fiscal (id=2) es N/A para todos los escenarios de reserva**.
- **Categorias futuras especificas** (no implementadas):
  - `brain/test-scenarios/journal/JOU-NNN.yaml` para stock jornada
  - `brain/test-scenarios/billing/BIL-NNN.yaml` para prefactura
  - `brain/test-scenarios/policy/POL-NNN.yaml` para flujo de polizas (PE0001/PE0005)
- **Escenarios SAP/NAV nunca aplican** a CEALSA.
