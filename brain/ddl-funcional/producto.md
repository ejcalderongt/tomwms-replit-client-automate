---
id: producto
tipo: ddl-funcional
estado: vigente
titulo: ddl-funcional/producto.md — flags funcionales del producto
tags: [ddl-funcional]
---

# ddl-funcional/producto.md — flags funcionales del producto

> Tabla `producto` tiene 60 columnas. Aca documento solo las que
> determinan COMPORTAMIENTO funcional del producto en el WMS.
>
> Datos de cardinalidad medidos en MAMPA-QA (29-abr-2026): 31,397 productos.

## Identidad

| Columna | Tipo | Significado |
|---|---|---|
| `IdProducto` | int | PK |
| `codigo` | varchar | Codigo SKU del cliente (cross-ERP) |
| `nombre` | varchar | Descripcion humana |

## Flags de control (governan que se valida y que se registra)

| Flag | Tipo | Significado funcional | Si =1 dispara |
|---|---|---|---|
| `control_lote` | bit | Producto requiere registro de lote | Pide lote en recepcion, valida vs `lote` en stock_rec/trans_re_det |
| `control_vencimiento` | bit | Producto requiere registro de fecha vencimiento | Pide fecha en recepcion, valida vs `fecha_vence` en stock_rec |
| `control_peso` | bit | Producto se valida por peso (no por cantidad nominal) | Pide peso recepcion, valida vs `peso_referencia ± peso_tolerancia` |
| `peso_recepcion` | bit | Pesa especificamente al recibir | INSERT `peso_verificado` en trans_re_det |
| `peso_despacho` | bit | Pesa especificamente al despachar | INSERT `peso_verificado` en trans_despacho_det |
| `genera_lote` | bit | El WMS genera lote automaticamente si no viene del proveedor | Crea lote interno con regla del cliente |
| `genera_lp_old` | bit | Producto genera license plate automatica al recibir (modelo viejo) | Crea entry en `licencia_item` + `lic_plate` |

> **PENDIENTE confirmar Erik**: ¿hay un `genera_lp` (sin `_old`) que
> reemplace al `genera_lp_old`? El sufijo sugiere migracion en curso.

## Flags de clasificacion (governan rotacion y tipo de manufactura)

| Flag | Tipo | Significado |
|---|---|---|
| `IdClasificacion` | int | FK clasificacion (familia/subfamilia ABC) |
| `IdTipoProducto` | int | FK tipo_producto (terminado / materia prima / etc) |
| `IdTipoRotacion` | int | FK tipo_rotacion (FEFO / FIFO / LIFO) |
| `IdTipoEtiqueta` | int | FK tipo_etiqueta (define ZPL / formato impresion) |
| `IdTipoManufactura` | int | FK tipo_manufactura (propio / maquila / etc) |

## Parametros numericos

| Columna | Tipo | Uso |
|---|---|---|
| `peso_referencia` | float | Peso esperado por unidad |
| `peso_tolerancia` | float | Tolerancia +/- aceptada en validacion |

## Distribucion de flags en MAMPA (medida 29-abr-2026)

> Cuanto se usan estos flags en la realidad. Util para saber que
> "perfil tipico de producto" tiene cada cliente.

```
Total productos MAMPA = 31,397

(distribucion concreta a poblar con la siguiente query:
  SELECT flag, COUNT(CASE WHEN flag=1 THEN 1 END) as activo, COUNT(*) total)
```

## Predicado base: `predicado_perfil_producto(p)`

Todo producto en TOMWMS se puede caracterizar por su tupla:
```
(control_lote, control_vence, control_lp, control_peso,
 IdTipoProducto, IdTipoRotacion, IdTipoEtiqueta)
```

Dos productos con la **misma tupla** se comportan identicamente en el
flujo WMS, **independientemente del cliente**.

Esto habilita **agrupar productos por perfil cross-cliente** (objetivo
Erik 29-abr): "el producto A de killios funciona igual al producto B
de becofarma porque tienen mismo perfil". Ver
`patrones-producto/perfiles.md` para los perfiles tipicos detectados.
