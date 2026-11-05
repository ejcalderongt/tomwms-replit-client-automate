---
id: 2026-09-23-inventario-ciclico-busqueda-global-producto
tipo: proposal
estado: aplicado-local-pendiente-revision
ramas_afectadas: [dev_2026_estable]
tags: [inventario-ciclico, hh, bof, producto, ubicacion, licencia, EJC20260923]
---

# Inventario ciclico: restaurar busqueda global por producto

## Problema

La carga parcial por resumen de ubicaciones conserva el rendimiento, pero la HH ya no dispone de todos los detalles para resolver un codigo de producto antes de seleccionar una ubicacion. Adicionalmente, con `Control_Gondola` el texto se valida directamente como ubicacion y no alcanza la ruta historica de producto.

## Contrato propuesto

- Mantener sin cambios los WebMethods existentes.
- Agregar `Inventario_Ciclico_Listar_Conteo_By_Producto` con inventario, operador, pendientes, producto y talla/color.
- Reutilizar el mismo `DataTable` de `Inventario_Ciclico_Listar_Conteo`, agregando filtros opcionales exactos en DAL.
- La HH primero intenta resolver el valor como producto local; si no tiene detalle cargado, resuelve el codigo de barras y solicita al nuevo endpoint todas las filas de ese producto.
- Si existe una fila se abre; si existen varias, se listan licencia/ubicacion y se solicita la ubicacion.

## Compatibilidad

El cambio es aditivo: no renombra metodos, no elimina parametros y no modifica el orden ni el tipo de las columnas devueltas. Requiere publicar BOF antes o junto con la nueva APK; las HH anteriores no llaman al metodo nuevo.

## Validacion

- Compilar `WSHHRN/WSHHRN.vbproj`.
- Compilar `:app:compileDebugJavaWithJavac`.
- Probar codigo unico, codigo con varias ubicaciones/licencias, ubicacion, codigo inexistente, talla/color y `Control_Gondola`.
