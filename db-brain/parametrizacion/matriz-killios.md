# Matriz de parametrización — Killios

> Estado real de los flags clave en `TOMWMS_KILLIOS_PRD` validado 2026-04-27.

## cliente (1.083 filas)

| Flag | Activos / Total | % | Notas |
|---|---|---|---|
| `despachar_lotes_completos` | 0 / 1.083 | 0% | Killios permite despacho parcial de lote |
| `control_ultimo_lote` | 0 / 1.083 | 0% | Killios no fuerza última-lote |

> Otros 7 flags de cliente sin medir aún en este snapshot. Próxima iteración.

## producto (319 filas)

| Flag | Activos / Total | % | Notas |
|---|---|---|---|
| `control_lote` | **318** / 319 | **99.7%** | Trazabilidad de lote universal salvo 1 producto |
| `genera_lote` | 0 / 319 | 0% | Killios NO autogenera lote — viene del proveedor |

> Otros 15 flags de producto sin medir aún. Próxima iteración.

## bodega

> Sin medición en este snapshot. Bodega tiene ~57 flags, algunos de comportamiento crítico para lotes (`homologar_lote_vencimiento`, `restringir_lote_en_reemplazo`, `despachar_producto_vencido`). Pendiente de extraer cantidad de bodegas y % activación por flag.

## ajuste_tipo

> Sin medición. 5 flags incluyendo el typo `momdifica_vencimiento`.

## Comportamiento WMS deducido para Killios

Combinando los flags medidos:

| Comportamiento | Estado en Killios |
|---|---|
| Trazabilidad de lote | **SÍ** (universal, 99.7% productos) |
| Origen del lote | **Externo** (proveedor) — el WMS no autogenera |
| Despacho de lote completo obligatorio | **NO** (permite parcial) |
| Validación contra último lote | **NO** |
| Importación de lotes por cliente vía Excel | **Pendiente** (caso `wms-brain://entities/cases/case-2026-04-importar-lotes-cliente`) |

## Próximos passes en este archivo

1. Medir restantes 7 flags de cliente.
2. Medir restantes 15 flags de producto.
3. Medir todos los flags de bodega + cantidad de bodegas.
4. Medir flags de ajuste_tipo.
5. Cuando exista `matriz-becofarma.md` o similar, generar `diff-cross-cliente.md`.

## Cross-refs
- `parametrizacion/flags-cliente`, `flags-producto`, `flags-bodega`, `flags-ajuste-tipo`
- `wms-brain://entities/cases/case-2026-04-importar-lotes-cliente`
