# L-020 — Tres modelos de donde viven los flags de control

> Etiqueta: `L-020_DATA_MODELOS-CONFIG-PRODUCTO-VS-BODEGA_APPLIED`
> Fecha: 29-abr-2026
> Origen: comparacion cross-cliente K7/BECOFARMA/MAMPA

## Hallazgo

Los flags `control_lote`, `control_vencimiento`, `control_peso`, `genera_lp`
viven en DOS lugares: `producto.*` y `i_nav_config_enc.*`. Distintos
clientes adoptan modelos distintos sobre cual es la fuente de verdad.

### Modelo A — BODEGA-CENTRIC (ej: MAMPA)

```
producto.control_lote        = 0%   (todo en NULL/0)
producto.control_vencimiento = 0%
producto.control_peso        = 0%
producto.genera_lp_old       = 100% (todos en 1, "potencialmente puede")

i_nav_config_enc.control_lote = configurado por bodega
```

**Caracteristica**: el WMS ignora los flags de producto, mira solo la
bodega. Util cuando todos los productos del catalogo se comportan igual
en cada bodega (zapateria: en TIENDA se vende igual, en CEDIS se distribuye
igual).

### Modelo B — PRODUCT-CENTRIC (ej: BECOFARMA)

```
producto.control_lote         = 97.4%  (poblado producto a producto)
producto.control_vencimiento  = 97.4%
producto.control_peso         = 0%
producto.genera_lp_old        = 100%

i_nav_config_enc.control_lote = generico (True)
```

**Caracteristica**: el flag de cabecera bodega es general (todo va con
control), y luego el producto define excepciones (los 47 productos sin
lote son no-perecederos especificos). Util cuando el catalogo tiene
heterogeneidad alta (farma: la mayoria es perecedera, algunos no).

### Modelo C — MIXTO / INTERSECCION (ej: K7)

```
producto.control_lote        = 99.7%
producto.control_vencimiento = 87.8%
producto.control_peso        = 0%
producto.genera_lp_old       = 100%

i_nav_config_enc.control_lote = True
i_nav_config_enc.control_peso = True (pero producto en 0%)
```

**Caracteristica**: ambos lados activos. La logica del codigo aplica
**AND** (interseccion): solo controla si los dos dicen true.
`control_peso` esta True en config pero 0% en producto → en la
practica, NO se controla peso (porque la AND con producto da false).

## Implicaciones

1. **Para certificar**: cuando se valida un cliente, hay que fijar PRIMERO
   en que modelo opera. Misma feature da resultados distintos segun modelo.
2. **Para migrar a un cliente nuevo**: elegir modelo A si el catalogo es
   homogeneo, B si es heterogeneo, C si hace falta capacidad de override
   por producto.
3. **Para WebAPI nueva**: necesitamos exponer el flag EFECTIVO (resultado
   de la combinacion), no el flag crudo. Lo correcto es tener un endpoint
   `/producto/{id}/bodega/{idb}/efectivo` que devuelva el calculo.
4. **Para test cases**: matriz de pruebas debe incluir las 3 combinaciones
   (producto=1+config=1, producto=1+config=0, producto=0+config=1).

## Patron predicho de la WebAPI

```csharp
bool EfectivoControlLote(Producto p, IConfigEnc c) =>
    (p.control_lote ?? false) && c.control_lote; // AND para Modelo C
    // O p.control_lote (si Modelo B) — pero usamos AND por seguridad
```

## Pendientes

- Confirmar con codigo VB.NET cual es la formula real. Hipotesis: AND.
- Otros clientes (BYB, CEALSA, CUMBRE, IDEALSA, INELAC) — clasificar
  cada uno en A/B/C cuando se les haga fingerprint.
- ¿Hay algun cliente Modelo D que use OR en vez de AND? Improbable
  pero a confirmar.
