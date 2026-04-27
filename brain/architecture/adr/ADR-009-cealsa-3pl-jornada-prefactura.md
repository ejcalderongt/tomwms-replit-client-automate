# ADR-009: Modelo CEALSA — 3PL con jornada y prefacturacion

## Contexto

CEALSA (IMS4MB_CEALSA_QAS) es el unico cliente con modelo 3PL: opera bodegas para terceros, factura por servicios. Sus outputs principales no son reservas sino **stock jornada** (snapshot diario) y **prefacturacion**.

## Estructura

- `VW_Stock_Jornada` — 74 columnas, snapshot diario consolidado.
- `trans_prefactura_enc` (22) + `trans_prefactura_det` (19) + `trans_prefactura_mov` (13) — modelo enc/det/mov clasico.
- `jornada_laboral`, `operador_jornada_laboral`, `jornada_sistema` — modelo de jornadas (operadores 3PL).

## Flags distintivos

```
equiparar_cliente_con_propietario_en_doc_salida = true   # cliente del doc = propietario
interface_sap = false
```

## Bodega fiscal (idBodega=2)

Tiene casi todos los flags apagados:
```
control_vencimiento = false
control_lote = false
IdTipoRotacion = null
explosion_automatica = false
```

Es **passthrough fiscal**: las cantidades pasan por ahi para asiento contable pero no se opera. **No aplican escenarios de reserva** sobre esta bodega.

## Decision para el bridge

1. Categoria nueva (no implementada): `brain/test-scenarios/journal/JOU-NNN.yaml` para validar consistencia de stock jornada cross-bodega.
2. Categoria nueva: `brain/test-scenarios/billing/BIL-NNN.yaml` para escenarios de prefacturacion.
3. Para escenarios de reserva existentes, CEALSA aplica solo en bodega 1 (general). La bodega 2 (fiscal) siempre devuelve N/A.

## Pendiente

- Documentar las 74 columnas de VW_Stock_Jornada.
- Modelar el flujo de generacion de prefactura.
- Identificar los SPs/jobs que generan el snapshot diario.
