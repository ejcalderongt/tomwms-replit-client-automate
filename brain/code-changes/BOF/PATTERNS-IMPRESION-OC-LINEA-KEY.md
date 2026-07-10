---
tipo: other
---
# Patron: impresion OC debe usar llave de linea, no solo producto

> Caso observado en `frmImpresionRecepcion_OC` durante la preimpresion de
> orden de compra. El problema aparecia cuando una misma OC contenia varias
> lineas con el mismo producto y el mismo `IdProductoBodega`.

## Resumen

La pantalla de impresion de recepcion/OC mostraba la linea correcta en UI,
pero el `EditValue` del combo principal estaba amarrado a `IdProductoBodega`.
Cuando la OC trae el mismo producto repetido en varias lineas, ese valor no
distingue entre lineas distintas y el evento de cambio puede dejar de reflejar
la seleccion real.

El efecto operativo era:

- el operador cambiaba visualmente entre lineas iguales;
- `EditValueChanged` no recargaba la linea como se esperaba;
- lote/licencia quedaban desfasados respecto a la linea real;
- el documento de datos estaba bien, pero la UI no quedaba anclada a la llave
  correcta.

## Flujo afectado

```
frmImpresionRecepcion_OC
  Cargar_productos_oc()
    cmbProducto.ValueMember = IdProductoBodega  (antes)
    cmbProducto.DisplayMember = Nombre_producto

  cmbProducto_EditValueChanged()
    recupera fila actual
    toma IdOrdenCompraDet, IdProductoBodega, No_Linea
    carga presentacion + lotes

  Cargar_oc_lotes()
    cmbLote.ValueMember = IdLote
    cmbLote.DisplayMember = lote

  cmbLote_EditValueChanged()
    toma lote/fecha/licencia de la linea seleccionada
```

## Causa raiz

`IdProductoBodega` no es una llave de linea. Es una llave de producto dentro
de la bodega y puede repetirse en varias lineas del mismo documento.

Cuando la OC contiene varias lineas con el mismo producto, la UI necesita una
llave que represente la linea de negocio, no el producto:

- `IdOrdenCompraDet` como minimo;
- o una llave compuesta `IdOrdenCompraEnc + IdOrdenCompraDet` si se quiere
  mayor explicitud.

## Fix aplicado

Se corrigio la pantalla para que el combo principal de producto quede ligado a
`IdOrdenCompraDet` en vez de `IdProductoBodega`.

Impacto del cambio:

- cada linea de la OC ahora tiene identidad propia en la UI;
- el cambio entre lineas repetidas dispara la recarga correcta de lote y
  licencia;
- se evita que el combo quede "pegado" a la primera ocurrencia del producto.

## Regla derivada

Si una pantalla usa un lookup para seleccionar linea de documento y el
documento puede repetir el mismo producto, el `ValueMember` debe ser la llave
de linea, no la llave del producto.

## Referencia de codigo

- `TOMIMSV4/TOMIMSV4/Mantenimientos/Impresion_OC/frmImpresion_OC.vb`
  - `Cargar_productos_oc()`
  - `cmbProducto_EditValueChanged()`
  - `Cargar_oc_lotes()`
  - `cmbLote_EditValueChanged()`

## Alcance del aprendizaje

Este patron aplica tambien a otras pantallas BOF o de recepcion donde:

1. el combo muestra descripciones repetidas;
2. la seleccion real depende de `IdOrdenCompraDet`, `No_Linea` o una llave
   equivalente;
3. la UI dispara recargas dependientes por evento.

