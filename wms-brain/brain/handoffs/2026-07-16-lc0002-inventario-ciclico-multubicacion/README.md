# LC0002 - inventario cíclico por categoría con producto multiubicación

Estado: diagnóstico confirmado por recorrido de código; fix pendiente de una
siguiente pasada. No interpretar este documento como implementación aplicada.

Caso: inventario cíclico 293, producto `7500435126144`, congelado con 12
unidades en ubicación 2249 y 12 en ubicación 2501.

## Cadena causal

1. `trans_inv_stock` conserva correctamente cada existencia congelada por
   `IdStock + IdUbicacion`.
2. La pantalla BOF construye nodos distintos por producto y ubicación, pero el
   `Tag` del nodo solo conserva `IdProductoBodega`.
3. `cmdAsignaOpProd_Click` reconstruye `IdUbicacion` separando por `#` el texto
   visible de la ubicación.
4. Seleccionar un producto dentro del filtro de categoría asigna únicamente el
   nodo producto-ubicación marcado; no expande el producto a todas sus
   ubicaciones congeladas.
5. Si la ubicación 2501 no quedó en `trans_inv_ciclico`, la HH reutiliza la fila
   esperada de 2249 y envía 2501 como `IdUbicacion_nuevo`.
6. El DAL interpreta la diferencia como cambio de ubicación e inserta una
   línea con `Cant_stock = 0`; regularización la muestra como sobrante aunque
   las 12 unidades ya existían en el congelado de 2501.

## Paths de entrada

BOF, carga y asignación:

`TOMIMSV4/TOMIMSV4/Transacciones/Inventario/frmInventario.vb`

- `Listar_Productos_Asignados`
- `tlProductosInventario_AfterCheckNode`
- `cmdAsignaOpProd_Click`

DAL de conteo y cambio de ubicación:

`TOMIMSV4/DAL/Inventario/InvCiclico/clsLnTrans_inv_ciclico_Partial.vb`

- `Get_Inventario_Ciclico`
- `Actualiza_Conteo_Ciclico_HH`
- inserción con `Cant_stock = 0` cuando se interpreta un cambio real

HH:

`app/src/main/java/com/dts/tom/Transacciones/InventarioCiclico/frm_inv_cic_add.java`

- forma el payload con `IdUbicacion` original e `IdUbicacion_nuevo` capturado.

## Identidad canónica

Para asignación y conteo no basta `IdProductoBodega`. Debe conservarse:

`IdInventarioEnc + IdProductoBodega + IdStock + IdUbicacion`

Lote, estado, presentación, licencia y talla/color deben mantenerse cuando
formen parte de la granularidad del stock.

## Diseño candidato para la siguiente pasada

1. BOF: guardar `IdUbicacion` numérico en el nodo y eliminar el parseo del
   texto visible.
2. BOF: en modo producto/categoría expandir cada producto seleccionado a todas
   sus filas congeladas `IdStock + IdUbicacion`.
3. WS/DAL: antes de crear un supuesto cambio con `Cant_stock = 0`, buscar una
   fila congelada/no asignada para el producto en `IdUbicacion_nuevo`; si
   existe, vincular el conteo a esa identidad real.
4. HH: al capturar ubicación, resolver primero la tarea exacta
   producto-ubicación y no reutilizar silenciosamente otra ubicación como
   origen.
5. Pre-cierre: reconciliar `trans_inv_stock` contra `trans_inv_ciclico` por la
   identidad canónica y alertar por ubicaciones congeladas sin tarea.

## Evidencia DB esperada antes del fix

- Congelado: filas 2249 = 12 y 2501 = 12.
- Cíclico asignado: fila esperada únicamente para 2249.
- Conteo adicional: origen 2249, destino 2501, `Cant_stock = 0`, cantidad 12.

Validar con consultas de solo lectura en la BD correcta de La Cumbre antes de
modificar código. No usar KILLIOS ni inferir su esquema/datos para este caso.

## Separación de cambios

El fix BOF/VB y cualquier protección HH/Java deben implementarse y validarse en
cambios separados. No tocar `Reference.vb`.
