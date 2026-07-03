# #EJC20260620 mPos ajuste talla/color: busqueda inteligente

## Contexto

Pantalla mPos: `inventario/ajuste_talla_color`.

Necesidad operativa: la barra del listado de ajustes no debe limitarse a `corel`. Para auditoria y soporte de ajustes talla/color, el usuario necesita filtrar ajustes que contengan un producto o variante escaneada.

## Cambio aplicado en mPos

- `application/views/inventario/ajuste_talla_color/menu.php`
  - El campo visible pasa de "Buscar correlativo" a "Buscar correlativo, producto, barra, talla o color".
  - El parametro enviado es `busqueda`.
- `assets/js/inventario/ajuste_talla_color.js`
  - `bform.busqueda` reemplaza el uso UI de `bform.correlativo`.
- `application/models/inventario/Ajuste_model.php`
  - `buscar()` conserva compatibilidad con `correlativo`, pero prioriza `busqueda`.
  - El filtro combina encabezado (`d_mov.corel`, `d_mov.referencia`) y detalle via `EXISTS`.
  - El detalle consulta `d_movd`, `p_producto`, `p_producto_talla_color` y `p_producto_color`.

## Regla reutilizable

En procesos talla/color, una busqueda por codigo de barra, SKU, talla o color debe resolverse contra la variante real guardada en el detalle operativo, no contra todas las variantes del producto.

Patron recomendado:

1. Filtrar encabezados con `EXISTS` sobre el detalle del documento/movimiento.
2. Unir variantes por `codigo_producto` y por la unidad/variante persistida (`TALLA - COLOR` en este caso).
3. Permitir busqueda por:
   - correlativo/referencia,
   - codigo interno de producto,
   - codigo/SKU visible,
   - descripcion,
   - codigo de barra de talla/color,
   - talla,
   - color,
   - unidad variante.
4. Mantener compatibilidad con el parametro anterior cuando exista.

## Riesgos evitados

- Falsos positivos: una barra de otra talla/color del mismo producto no debe hacer aparecer un ajuste que no contiene esa variante.
- Busqueda lenta en frontend: no cargar todo para filtrar en navegador; el filtro pertenece al SQL del listado.
- Regresion UX: conservar filtros actuales de bodega, fechas y estado.

## Validacion

- `php -l application/models/inventario/Ajuste_model.php`
- `php -l application/controllers/inventario/Ajuste_talla_color.php`
- `http://127.0.0.1:8080/index.php/inventario/ajuste_talla_color` responde 200.

Nota: la prueba directa al endpoint `buscar` sin sesion devuelve login, por lo que la validacion de datos reales debe hacerse desde navegador autenticado.
