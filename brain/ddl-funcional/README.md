---
id: README
tipo: ddl-funcional
estado: vigente
titulo: ddl-funcional/ — diccionario funcional del WMS
tags: [ddl-funcional]
---

# ddl-funcional/ — diccionario funcional del WMS

> No es un diccionario estructural (eso lo da `sys.columns`). Es un
> diccionario **funcional**: que SIGNIFICA cada flag/parametro a nivel
> negocio, y que comportamiento dispara en la BD.
>
> Objetivo (Erik 29-abr-2026): "necesito que internamente modelemos un
> lenguaje en el que podamos comunicarnos eufemismos de los datos".
> Ejemplo: cuando digo `predicado_recibe_producto`, ya implica un set
> de chequeos sobre flags del producto y un flujo predicho de tablas
> afectadas, sin tener que enumerarlos cada vez.

## Documentos

- `producto.md` — todos los flags funcionales del producto y su
  comportamiento. Source of truth para predicar sobre productos.
- `bodega.md` — capability flags por bodega (`control_talla_color`,
  `tipo_pantalla_*`).
- `predicados.md` — el lenguaje interno: que dice cada predicado y que
  flujo desencadena.

## Convencion de predicados

Un predicado tiene la forma `predicado_<verbo>_<objeto>` y se expande
automaticamente a una tupla de chequeos + un flujo predicho.

Ejemplo:
```
predicado_recibe_producto(p)
  ⇒ chequeos:
    p.control_lote        ∈ {0,1}
    p.control_vencimiento ∈ {0,1}
    p.control_lp          ∈ {0,1}  (genera_lp / genera_lp_old)
    p.control_peso        ∈ {0,1}
    p.presentacion        is not null
    p.umbas (unidad base) is not null
  ⇒ flujo predicho de tablas:
    INSERT trans_re_det
    INSERT stock_rec
    IF bodega.habilitar_stock_inmediato:
      INSERT stock
    IF p.control_lote: requiere lote en payload
    IF p.control_vencimiento: requiere fecha_vence en payload
    IF p.control_lp: emite lic_plate (entry en licencia_item)
    IF p.control_peso: requiere peso_recibido + valida vs peso_referencia ± peso_tolerancia
    IF bodega.control_talla_color: requiere IdProductoTallaColor
```
