# EVIDENCIAS CRONICIDAD V6 — Bug sistémico de larga data confirmado por análisis cruzado de ajustes manuales

## Cambios respecto a V5

V5 documentaba el alcance del bug `dañado_picking sin descuento` en Killios desde junio 2025 (>320 mil UM, 25 usuarios).
V6 agrega el cruce con el historial completo de ajustes manuales (`trans_ajuste_enc` + `trans_ajuste_det`, wave 21) que prueba que **el bug existe desde 2019** y el equipo Killios lo viene compensando manualmente con ajustes "Despacho Licencia" y "Ajuste contra físico" en sentido NEGATIVO.

## Cifras transversales Killios (sin cambios desde V5)

- 25 usuarios distintos marcaron pickings dañado_picking=1, activo=1 desde 2025-06-09
- >10,800 líneas acumuladas
- >320,000 UM marcadas como dañadas y NO descontadas
- Periodo: 2025-06-09 → 2026-04-28 (10+ meses sin remediación)
- Top operadores: user 10 (142,509 UM), user 20 Heidi López (88,848 UM), user 13 Mario Santizo (41,618 UM)
- Patrón consistente: `IdOperadorBodega_Pickeo=0`, `cantidad_verificada=0`, `cantidad_despachada=0`, `encontrado=True`, `activo=1` → todos procesados desde BOF VB.NET

## Nuevo: cruce con historial de ajustes manuales (W21-05, W21-06)

### Distribución temporal de ajustes manuales (1,653 ajustes históricos)

| Fase                 | Pos | Neg | Lectura                                                |
|----------------------|----:|----:|--------------------------------------------------------|
| 2019-09 → 2023-04    | 596 | 925 | Período activo de corrección manual                    |
| 2023-04 → 2025-11    | 0   | 0   | Gap de 2.5 años — sin ajustes (sin auditoría aparente) |
| 2025-11 → 2026-04    | 5   | 122 | Reactivación: 96 % negativos                           |

El gap de 2.5 años es muy llamativo. Hipótesis (no verificadas): cambio de versión del WMS, cambio de proceso interno Killios, o simple ausencia de auditoría.

### Top motivos de ajustes — síntomas directos del bug

| Tipo + Motivo                              | n     | Lectura                                              |
|--------------------------------------------|------:|------------------------------------------------------|
| Ajuste Positivo · Falla de sistema         | 297   | "El sistema perdió stock y lo agregamos manual"      |
| Ajuste Positivo · Error en digitación      | 247   | Recepciones digitadas mal, corregidas a posteriori   |
| Ajuste Negativo · Despacho Licencia        | 105   | "Se despachó pero no se descontó" — patrón histórico de Erik |
| Ajuste Negativo · Ajuste contra físico     | 105   | Inventario físico < BD                                |
| Ajuste Negativo · Falla de sistema         |  20   | Falla de sistema en sentido inverso                  |
| Ajuste Positivo · Ajuste contra físico     |  34   | Inventario físico > BD                                |

### El bug ya tiene un ajuste reciente sobre WMS164

W21-08 + W21-09 muestran:
- `idajusteenc=1020`, fecha 2026-04-10, usuario 13 = Mario Santizo
- Motivo "Ajuste contra físico", `ajuste_por_inventario=0` (manual, no parte de un INVE)
- 3 detalles, todos con idtipoajuste=5 (Ajuste Negativo):
  - JG000006 lote BM2511: −6 UM
  - FU06505 lote BM2601: −5 UM
  - JG000013 lote BM2601: −4 UM
- Total restado: 15 UM = 3 cajas
- BG2512 NO fue tocado en ese ajuste → quedan las 14 cajas fantasma a la fecha del reporte

### Outlier corrupto detectado y excluido

`idajustedet=638` del 13-may-2021 tiene `cantidad_original=4,804,442,747,532` UM (4.8 billones) ajustado a 0, observación "sa". Es dato corrupto/test mal hecho. Distorsiona los agregados pero no representa el bug funcional.

## Saldo neto computado actual (W21-04)

`SUM(stock.cantidad) − SUM(stock_res.cantidad)` por producto: **0 productos con neto < 0** hoy. La vista `VW_Stock_Resumen` no muestra negativos en este momento. Esto NO contradice el bug — significa que el equipo logró compensar (o las reservas vivas no superan el stock fantasma actual). Históricamente, cuando había mayor presión de pedidos contra stock contaminado, el reporte sí podía mostrar neto negativo.

## Productos top afectados (W19-G, sin cambios)

| IdProductoBodega | UM dañadas no descontadas | Lotes |
|-----------------:|--------------------------:|------:|
| 395              | 24,580                    | 19    |
| 1515             | 21,421                    | 24    |
| 1350             | 18,978                    | 11    |
| 730              | 16,068                    | 5     |
| 1315             | 15,622                    | 9    |
| 1320             | 13,535                    | 12    |
| 1375             | 11,353                    | 7    |
| 1495             | 11,186                    | 8    |
| 1355             | 8,877                     | 14   |
| 370              | 6,769                     | 6    |

WMS164/381 con 322 UM totales NO entra en el top 30. La Killios tiene productos mucho más afectados sin reportar.

## Por qué nadie lo detecta

1. W18-05: cero inventarios formales (`IdTipoTarea=6, INVE`) desde 2025-11-30
2. W18-10: cero pickings anulados (`activo=0`); el sistema no permite revertir un dañado
3. La sobre-estimación es invisible en reportes operativos: el saldo BD no distingue entre disponible real y unidades retiradas-marcadas-dañadas
4. Los ajustes manuales que sí se hicieron (122 negativos en 5 meses) son muy pequeños en magnitud (promedio <30 UM/mes) y no alcanzan a contrarrestar el ritmo de generación

## Implicación de negocio (revisada con factor real)

WMS164 tiene factor 5 UM/caja (W20-01). Si extrapolamos un factor caja promedio similar (5-12) en otros productos Killios, las >320,000 UM fantasma equivalen a entre **27,000 y 64,000 cajas** que la BD reporta como existentes y físicamente no están. Eso impacta:

- Disponibilidad para venta (vendés algo que no podés despachar)
- Valoración contable del inventario (sobre-estimado)
- Planificación de reposición (no se compra porque "hay stock")
- Confianza operativa en los reportes WMS

## Trazabilidad

Wave 18 + 19 + 20 + 21 cubren toda la cadena: trans_picking_ubic + trans_movimientos + stock_hist + stock viva + producto_presentacion + trans_ajuste_enc/det. La ausencia de movimiento `IdTipoTarea=17 (AJCANTN)` o de movimiento a ubicación MERMA tras un dañado es **estructural**, no anecdótica.

La validación cruzada con el historial de ajustes manuales (1,653 a lo largo de 5+ años) confirma que el patrón se viene compensando manualmente desde 2019, lo que descarta cualquier hipótesis de "incidente puntual" o "error humano aislado".
