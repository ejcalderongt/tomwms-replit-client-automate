---
id: README
tipo: heat-map-params
estado: vigente
titulo: heat-map-params — Mapa de calor de parametros del WMS
tags: [heat-map-params]
---

# heat-map-params — Mapa de calor de parametros del WMS

> **Concepto Erik (29-abr-2026)**: subir un nivel de abstraccion sobre
> el sendero. En vez de patrones cerrados cliente-por-cliente, modelar
> el WMS como **conjuntos de parametros por capa**. La homologacion nace
> de comparar valores entre clientes: si dos clientes tienen los mismos
> flags y a nivel de datos no hay diferencia, eso es el ESTANDAR. Si un
> cliente añade Z extra, eso es VARIANTE documentada contra el estandar.

## Capas

| Capa | Tabla(s) clave | Granularidad | Pregunta que responde |
|---|---|---|---|
| 01 Empresa | `i_nav_config_enc` (campos globales) | empresa | Cuantos decimales? Que ejecutable Sync? Que interface ERP? |
| 02 Bodega | `bodega` (101 cols) + `i_nav_config_enc` (por bodega) | bodega | Recibe produccion? Bodega de calidad? Genera etiqueta o pre-impresa? Reabasto? Voz? Talla/color? |
| 03 Interface | `i_nav_config_enc` flags interface_*, `i_nav_transacciones_*`, tablas `_vw*` | conexion ERP | Que ERP? Que se transmite? Stock o prefactura? |
| 04 Producto | `producto` (60 cols), `producto_bodega` (9 cols), `producto_estado_ubic` | producto | Lote? Vencimiento? Peso? LP? Talla/color? Serie? Aniada? |
| 05 Tipo Documento | `trans_oc_ti` (24 cols ingresos) + `trans_pe_tipo` (30 cols salidas) | tipo doc | Empuja stock? Verificacion? Reserva? Empaque tarima? Devolucion? Pedido consolidado? |
| 06 Procesos Homologados | derivado de capas 1-5 + `sis_tipo_tarea` + trazas reales | proceso | Recepcion / Put-away / Picking / Verificacion / Packing / Despacho / Transferencias / Devolucion: estandar y variantes por cliente. |

## Niveles de homologacion

```
ESTANDAR
  └── proceso definido por flags-default + datos-iguales cross-cliente
      ├── VARIANTE-CLIENTE-A (delta: que flag cambia, que tabla extra usa)
      ├── VARIANTE-CLIENTE-B (delta: que flag cambia)
      └── ...
```

Ejemplo: PACKING
- ESTANDAR PACKING: bodega.empaque_tarima=True, sis_tipo_tarea=12 PACK,
  trans_pe_tipo.empaque_tarima=True, captura_estiba_ingreso opcional.
- VARIANTE BYB: añade tabla `trans_packing_*` (a confirmar) y CEST
  (sis_tipo_tarea=3) como pre-paso.
- VARIANTE K7: estandar puro (sin extras).
- AUSENTE: BECOFARMA, MAMPA, CEALSA.

## Como leer cada capa

Cada `0X-<capa>/README.md` tiene secciones:

1. **Catalogo de parametros**: listado de cols con su nombre tecnico,
   tipo, y descripcion funcional inferida.
2. **Tabla cross-cliente**: valor del parametro en BECOFARMA, K7, MAMPA,
   BYB, CEALSA (cuando esta disponible).
3. **Flags activos por cliente**: que flags estan en True en cada cliente,
   agrupados por afinidad funcional.
4. **Implicaciones para el sendero**: como cada flag cambia el
   comportamiento del sistema, mapeado al graph-EQL del sendero.
5. **Pendientes**: que valores faltan, que hipotesis quedan abiertas.

## Cross-cliente exhaustivo

El folder [`cross-cliente/`](cross-cliente/) contiene la matriz completa
`flag x cliente` con datos REALES de los 5 clientes en EC2. Cada archivo
documenta una capa con: schema drift, valores reales por cliente, y lecturas
operativas. Se construyo en abril 2026 como insumo para el code-deep-flow
posterior.

- [`cross-cliente/README.md`](cross-cliente/README.md) — Indice + conteos
  macro por cliente.
- [`cross-cliente/01-i_nav_config_enc.md`](cross-cliente/01-i_nav_config_enc.md)
  — 78 cols union, taxonomia ERP corregida (3 clientes son SAP_BO, no NAV).
- [`cross-cliente/02-bodega.md`](cross-cliente/02-bodega.md) — 123 cols union,
  flags por bodega y matriz capabilities.
- [`cross-cliente/03-tipos-documento.md`](cross-cliente/03-tipos-documento.md)
  — `trans_oc_ti` y `trans_pe_tipo` cross-cliente.
- [`cross-cliente/04-producto.md`](cross-cliente/04-producto.md) —
  `producto`, `producto_bodega`, `producto_estado`, `producto_estado_ubic`
  con distribuciones reales. Cierra el inventario de las 4 capas
  fundamentales y resuelve el caso CEALSA 3.200 estados (scaffolding
  ruido por propietarios).

## Pendientes

- Capa 6: archivos dedicados por proceso (recepcion.md, picking.md, etc),
  primero ESTANDAR, luego variantes — esto se va llenando durante el
  code-deep-flow.
- Capa 3: investigar inbox tables (`i_nav_*`, `cealsa_vw*`) — primer
  insumo del code-deep-flow.
- Confirmar caso CLAVAUD (cliente futuro o legacy).
- Confirmar uso de `marcar_registros_enviados_mi3` (que es MI3).
- Capturar trans_oc_ti K7 (faltante en barrido cross-cliente).
- IDPRODUCTOPARAMETROA/B + industria_motriz: confirmados DEPRECATED por
  Erik (28-abr-2026), reflejado en 02-bodega y 04-producto.

## Estado del inventario

Las 4 capas fundamentales (config, bodega, tipos doc, producto) estan
cubiertas con datos REALES de los 5 clientes. Drift medido. Hipotesis
Q-* registradas. Listo para arrancar **code-deep-flow** (Wave 6+):
dado un parametro, mapear como viaja por el backend / BOF VB.NET / HH
Android y que tablas afecta.
