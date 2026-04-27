# Flags del maestro `producto`

> 17 columnas bit en `dbo.producto` (validado 2026-04-27). Killios tiene 319 productos registrados.

## Listado completo

| Flag | Killios actual | Descripción funcional preliminar |
|---|---|---|
| `activo` | (TBD) | Producto activo (vs dado de baja) |
| `serializado` | (TBD) | Tracking por número de serie individual |
| `genera_lote` | **0/319** | Si =1: el sistema autogenera el lote en recepción. **Nadie lo usa en Killios** → lote viene del proveedor. |
| `genera_lp_old` | (TBD) | Legacy: generación de License Plate (probable deprecado) |
| `control_vencimiento` | (TBD) | Si =1: tracking de fecha de vencimiento por lote |
| `control_lote` | **318/319** | Si =1: tracking obligatorio de lote en recepción/picking/despacho. **Casi universal en Killios.** |
| `peso_recepcion` | (TBD) | Captura peso al recibir |
| `peso_despacho` | (TBD) | Captura peso al despachar |
| `temperatura_recepcion` | (TBD) | Captura temperatura al recibir |
| `temperatura_despacho` | (TBD) | Captura temperatura al despachar |
| `materia_prima` | (TBD) | Clasificación: producto es MP (vs PT) |
| `kit` | (TBD) | Producto es ensamblado (kit con componentes) |
| `fechamanufactura` | (TBD) | Captura fecha de manufactura |
| `capturar_aniada` | (TBD) | Captura "añada" (probablemente vinos) |
| `control_peso` | (TBD) | Validación de peso esperado vs capturado |
| `captura_arancel` | (TBD) | Captura nro de arancel aduanero |
| `es_hardware` | (TBD) | Clasificación tipo hardware |

## Patrones detectados en Killios

- **Lote universal, generación nunca**: `control_lote=1` casi total + `genera_lote=0` total → patrón "lote del proveedor, no del WMS".
- **Pares peso/temperatura recepción↔despacho**: simetría de captura entre ambos extremos del flujo.
- **Vacíos esperables**: `captura_aniada` (sólo vinos), `captura_arancel` (sólo importadores).

## Cross-refs
- `db-brain://tables/producto`
- `parametrizacion/matriz-killios#producto`
- `wms-brain://entities/cases/case-2026-04-importar-lotes-cliente` — el flag `control_lote` es central al caso
