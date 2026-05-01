---
id: README
tipo: heat-map-params
estado: vigente
titulo: Capa 4 — Parametros a nivel PRODUCTO
tags: [heat-map-params]
---

# Capa 4 — Parametros a nivel PRODUCTO

> El producto tiene 60 cols en `producto`. La relacion `producto_bodega`
> tiene solo 9 cols (es relacion N:M sin params adicionales).

## Tablas origen

- `producto` (60 cols)
- `producto_bodega` (9 cols)
- `producto_presentacion` (a documentar)
- `producto_presentaciones_conversiones` (a documentar)
- `producto_estado_ubic` (mapeo estado → ubicacion por bodega)

## Catalogo de parametros — `producto`

### Identidad
- `IdProducto`, `codigo`, `codigo_barra`, `nombre`, `IdPropietario`

### Catalogos (FK)
- `IdClasificacion`, `IdFamilia`, `IdMarca`, `IdTipoProducto`
- `IdUnidadMedidaBasica` (UMBas)
- `IdUnidadMedidaCobro` (UM de cobro, distinta de UMBas)
- `IdCamara` (camara fria)
- `IdTipoRotacion` (FIFO/FEFO/LIFO)
- `IdIndiceRotacion`
- `IdSimbologia` (tipo de codigo de barra)
- `IdArancel`
- `IdPerfilSerializado`
- `IdTipoEtiqueta`
- `IdTipoManufactura`

### Flags de comportamiento
| Flag | Funcion |
|---|---|
| `control_lote` | Maneja lote |
| `control_vencimiento` | Maneja vencimiento (DERIVADO de control_lote o paralelo?) |
| `control_peso` | Maneja peso variable |
| `peso_recepcion` | Captura peso al recibir |
| `peso_despacho` | Captura peso al despachar |
| `temperatura_recepcion` | Captura temperatura al recibir |
| `temperatura_despacho` | Captura temperatura al despachar |
| `materia_prima` | Es materia prima |
| `kit` | Es producto kit |
| `fechamanufactura` | Captura fecha manufactura |
| `capturar_aniada` | Captura aniada/cosecha (vinos!) |
| `captura_arancel` | Captura arancel (importacion) |
| `es_hardware` | Es hardware (electronicos) |

### Datos comerciales
- `precio`, `costo`
- `existencia_min`, `existencia_max`
- `peso_referencia`, `peso_tolerancia`
- `tolerancia` (cantidad)
- `ciclo_vida` (dias)
- `dias_inventario_promedio`

### Dimensiones
- `largo`, `alto`, `ancho`

### Series
- `noserie`, `noparte`

### Misc
- `imagen` (image)
- `IDPRODUCTOPARAMETROA`, `IDPRODUCTOPARAMETROB` — **DEPRECATED**.
  Diseñados para una venta unica de repuestos del rubro automotriz que
  nunca llego a operar productivamente. Acompañan al flag `industria_motriz`
  de bodega (tambien deprecated). NO priorizar en WebAPI nuevo. Mantener
  por compatibilidad de schema solamente. Sin clientes activos que los usen.
- `margen_impresion`

### Auditoria
- `user_agr`, `fec_agr`, `user_mod`, `fec_mod`

## Catalogo `producto_bodega` (9 cols)

```
IdProductoBodega   PK
IdProducto         FK
IdBodega           FK
activo             bit
sistema            bit  ← creado por sistema vs manual
user_agr, fec_agr, user_mod, fec_mod
```

> **Observacion**: la relacion N:M entre producto y bodega NO tiene
> parametros propios mas alla de activo/sistema. Esto significa que
> los flags de comportamiento del producto son GLOBALES (no se pueden
> sobreescribir por bodega).

## producto_estado_ubic (a documentar)

Mapea cada (estado, bodega) → ubicacion fisica. Util para:
- BECOFARMA: mapea Cuarentena (estado) → ubicacion CUARENTENA en bodega 1.
- Otros clientes: defaults para cada estado.

## Cross-cliente (PENDIENTE)

Distribucion de uso de cada flag:

| Flag | % productos en BECOFARMA | K7 | MAMPA | BYB | CEALSA |
|---|---|---|---|---|---|
| control_lote | 100% (probable) | ? | 0% (calzado) | 100% (alimentos) | 100% (farma) |
| control_vencimiento | 100% (farma) | ? | 0% | 100% | 0% (anomalo) |
| genera_lp_old | ? | ? | 100% | False (no usa LP por unidad) | True |
| capturar_aniada | ? | ? | ? | ? | ? |
| ... | | | | | |

## Implicaciones para WebAPI

- Los flags de producto definen las CAPABILITIES requeridas para procesarlo:
  - `control_lote=True` → requiere capability `lote-management`.
  - `control_peso=True` → requiere capability `weight-capture`.
  - `capturar_aniada=True` → requiere capability `vintage-capture`.
- Las capabilities son aditivas (un producto puede tener varias).
- Si el cliente no tiene esas capabilities habilitadas a nivel bodega,
  el producto se rechaza o se procesa sin el campo opcional.

## Pendientes

- Documentar `producto_presentacion` y `producto_presentaciones_conversiones`.
- Investigar `IDPRODUCTOPARAMETROA/B` (uso desconocido).
- Confirmar si `IdUnidadMedidaCobro` siempre = `IdUnidadMedidaBasica` o
  difiere (caso peso variable).
- Cross-cliente exhaustivo de % de uso de cada flag.
