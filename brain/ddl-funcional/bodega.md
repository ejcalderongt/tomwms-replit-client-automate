---
id: bodega
tipo: ddl-funcional
estado: vigente
titulo: ddl-funcional/bodega.md — capability flags por bodega
tags: [ddl-funcional]
---

# ddl-funcional/bodega.md — capability flags por bodega

> En MAMPA detectamos que la bodega tiene flags propios que sobre-escriben
> o complementan los del producto. Esto explica por que el mismo
> producto puede comportarse distinto en bodega 1 vs bodega 21.

## Flags conocidos (validados en MAMPA 29-abr-2026)

| Flag | Tipo | Significado funcional |
|---|---|---|
| `control_talla_color` | bit | Si ON: las pantallas de pickeo/recepcion/verificacion piden IdProductoTallaColor (no solo IdProducto) |
| `TIPO_PANTALLA_PICKING` | int | 0 = pantalla legacy / 1 = pantalla nueva. Codigo HH bifurca en runtime |
| `tipo_pantalla_recepcion` | int | idem para recepcion |
| `tipo_pantalla_verificacion` | int | idem para verificacion |

## Patrones detectados en MAMPA

- 17 bodegas con `tipo_pantalla_* = 1` (perfil "nuevo")
- 1 bodega (CEDIS SAN JUAN, IdBodega=21) con `tipo_pantalla_* = 0`
  (perfil "legacy") — pendiente investigar por que.
- TODAS las bodegas con `control_talla_color = True`.

## Predicado base: `predicado_perfil_bodega(b)`

Una bodega se caracteriza por:
```
(control_talla_color, TIPO_PANTALLA_PICKING,
 tipo_pantalla_recepcion, tipo_pantalla_verificacion)
```

Util para predecir que pantalla del HH se va a abrir y que campos
va a pedir. Critico cuando armemos sets de prueba: "para esta
bodega+producto, que pantalla se abre y que campos valida?".
