# EVIDENCIAS CRONICIDAD V5 — Bug sistémico dañado_picking sin descuento de stock

## Cambio respecto a V4

V4 documentaba la cronicidad del WMS164 (8 CESTs BUEN→MAL en 4 meses por o31 Josué García).
V5 amplía a la causa raíz real, que es transversal a TODA la BD Killios y no específica de WMS164.

## Cifras que cuantifican el bug a nivel Killios

Wave 19, archivo W19-F-Killios-danados-totales-por-user.csv:

- **25 usuarios distintos** han marcado pickings como `dañado_picking=1, activo=1` desde junio 2025.
- **>10,800 líneas** acumuladas.
- **>320,000 unidades de medida (UM)** marcadas como dañadas y NO descontadas del stock.
- Periodo: 2025-06-09 hasta 2026-04-28 (10 meses largos sin remediación).

Top usuarios por volumen:
| user | UM dañadas | n_líneas | productos | lotes | nombre real (cuando lo sabemos) |
|------|-----------:|---------:|----------:|------:|---------------------------------|
| 10   | 142,509    | 3,426    | 111       | 348   | (pendiente identificar)         |
| 20   | 88,848     | 4,036    | 210       | 415   | Heidi López                     |
| 13   | 41,618     | 1,332    | 148       | 293   | Mario Santizo                   |
| 12   | 31,709     | 1,153    | 133       | 226   | (pendiente identificar)         |
| 28   | 5,682      | 217      | 40        | 81    | (pendiente identificar)         |

Patrón consistente entre todos los registros muestreados (W19-A):
- `dañado_picking = 1`
- `cantidad_verificada = 0`
- `cantidad_despachada = 0`
- `encontrado = True`
- `activo = 1` (no se anula)
- `IdOperadorBodega_Pickeo = 0` y `IdOperadorBodega_Asignado = 0` → procesados desde backoffice (BOF VB.NET), no desde HH Android.

## Productos top afectados (W19-G-Killios-top-productos-danados.csv)

| IdProductoBodega | UM dañadas no descontadas | Lotes |
|-----------------:|--------------------------:|------:|
| 395              | 24,580                    | 19    |
| 1515             | 21,421                    | 24    |
| 1350             | 18,978                    | 11    |
| 730              | 16,068                    | 5     |
| 1315             | 15,622                    | 9     |
| 1320             | 13,535                    | 12    |
| 1375             | 11,353                    | 7     |
| 1495             | 11,186                    | 8     |
| 1355             | 8,877                     | 14    |
| 370              | 6,769                     | 6     |

(WMS164/381 con 322 UM totales NO entra en el top 30 — la Killios tiene productos mucho más afectados sin reportar.)

## Por qué nadie lo detecta

1. **W18-05**: cero inventarios formales (`IdTipoTarea=6, INVE`) desde 2025-11-30.
2. **W18-10**: cero pickings anulados (`activo=0`). El sistema no permite revertir un dañado.
3. La sobre-estimación de stock es invisible en reportes operativos: el saldo BD muestra "stock disponible" sin distinguir entre disponible real y unidades que fueron retiradas-marcadas-dañadas.

## Implicación de negocio

Si el factor caja promedio en Killios es ~12 UM/caja, las **>320,000 UM fantasma** equivalen a ~26,700 cajas que la BD reporta como existentes y físicamente no están. Eso impacta:

- Disponibilidad para venta (vendés algo que no podés despachar).
- Valoración contable del inventario (sobre-estimado).
- Planificación de reposición (no se compra porque "hay stock").
- Confianza operativa en los reportes WMS.

## Trazabilidad

Wave 18-19 cubrió toda la cadena: trans_picking_ubic + trans_movimientos + stock_hist + producto_bodega_stock. La ausencia de movimiento `IdTipoTarea=17 (AJCANTN)` o de movimiento a ubicación MERMA tras un dañado es **estructural**, no anecdótica.

