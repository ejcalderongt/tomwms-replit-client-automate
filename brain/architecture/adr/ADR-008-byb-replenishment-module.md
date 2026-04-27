# ADR-008: Modulo de reabastecimiento de picking activo en BYB

## Contexto

BYB (IMS4MB_BYB_PRD) es el unico cliente productivo que usa el **modulo de reabastecimiento de picking**. Killios y CEALSA tienen las tablas pero no las usan operativamente.

## Tablas/vistas exclusivas/operativas en BYB

- `zona_picking` — definicion de zonas dentro de la bodega
- `zona_picking_tramo` — subdivision por tramos (filas, niveles)
- `producto_rellenado` — config min/max por producto-ubicacion
- `operador_zona_picking_tramo` — asignacion de operadores a zonas/tramos
- `VW_ProductoRellenado` — vista calculada de productos a rellenar
- `VW_MinimosMaximosPorPresentacion` — min/max por presentacion
- `VW_rptMinimosMaximos` y `VW_rptMinimosMaximos_v2` — reportes operativos
- `VW_Operador_Zona_Picking`, `VW_Zona_Picking_Tramo`, `VW_Zona_Picking_Tramo_Operador`

## Flags de reabasto en i_nav_config_enc

```
excluir_ubicaciones_reabasto                    # excluye al reservar
considerar_paletizado_en_reabasto               # respeta cantidad por pallet
considerar_disponibilidad_ubicacion_reabasto    # verifica espacio en destino
```

## Decision para el bridge

Los escenarios de **reabastecimiento** (no implementados aun) deben usar BYB como cliente. Killios y CEALSA quedan marcados como N/A para esa categoria.

Categoria propuesta: `brain/test-scenarios/replenishment/REP-NNN.yaml` con `requires_client: ["byb"]`.

## Pendiente

- Mapear el algoritmo exacto de generacion de tareas de relleno (probablemente via SP o trigger).
- Documentar el flujo: detecta picking < min → genera tarea → asigna operador → ejecuta movimiento → actualiza stock.
- Identificar como interactua con la reserva: ¿se reserva contra picking despues de relleno o antes?
