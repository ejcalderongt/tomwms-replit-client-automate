# LC0002 - inventario cíclico por categoría con producto multiubicación

Estado: fix BOF/DAL implementado el 2026-07-16 en el working tree
`dev_2028_merge`. El código TOMWMS queda sin commit para revisión de Erik.

Caso: inventario cíclico 293, producto `7500435126144`, con 12 unidades
congeladas en ubicación 2249 y 12 en ubicación 2501.

## Evidencia de La Cumbre QA

Validado con consultas de solo lectura en `TOMWMS_LA_CUMBRE_QA`:

- `trans_inv_stock`: `IdStock=632822`, ubicación 2249, cantidad 12.
- `trans_inv_stock`: `IdStock=651892`, ubicación 2501, cantidad 12.
- `trans_inv_ciclico`: la fila de 2249 conservó `Cant_stock=12`.
- `trans_inv_ciclico`: el conteo de `IdStock=651892` quedó con origen 2501,
  destino 2249, `Cant_stock=0`, cantidad 12 y `Regularizar=1`.

El congelado era correcto. La anomalía se introdujo al seleccionar/asignar y
al tratar posteriormente una existencia congelada como conteo no asignado.
El inventario 293 observado en esta restauración QA tiene fecha 2026-04-08;
por ello los IDs y datos prueban la mecánica, pero deben volver a correlacionarse
si se valida contra otra restauración del incidente.

## Causa fina

### 1. Selección BOF con filtro

En `frmInventarioProductos.vb`, el checkbox invertía el valor de la fila
enfocada en vez de persistir el valor real del editor. Además, `Marcar todos`
recorría enteros `0..DataRowCount-1` como si fueran row handles consecutivos.
Esa suposición no es segura con filtros de categoría y podía omitir una fila
visible o modificar una oculta, creando una asignación parcial silenciosa.

### 2. Conteo no asignado sin reconciliar congelado

`Agregar_Conteo` insertaba directamente la captura como existencia nueva. Si
la ubicación omitida sí estaba en `trans_inv_stock`, la línea quedaba con
`Cant_stock=0` y podía representarse como sobrante/cambio de ubicación aunque
la existencia ya formaba parte del congelado.

## Fix implementado

### BOF - selección filtrada determinística

Tag: `#EJC20260716_LC0002_INV_CICLICO_SELECCION_FILTRO`

Path:
`TOMIMSV4/TOMIMSV4/Transacciones/Inventario/frmInventarioProductos.vb`

- El checkbox guarda `ritem.Checked`; ya no invierte la fila enfocada.
- `Marcar todos` recorre `GetVisibleRowHandle` y solo modifica data rows
  visibles.
- Se publica el editor y se valida que ninguna fila visible quede sin marcar
  antes de continuar.

### DAL - resolver identidad congelada exacta

Tag: `#EJC20260716_LC0002_INV_CICLICO_CONGELADO_EXACTO`

Path:
`TOMIMSV4/DAL/Inventario/InvCiclico/clsLnTrans_inv_ciclico_Partial.vb`

Antes de insertar un conteo no asignado, se consulta el congelado por:

`IdInventarioEnc + IdProductoBodega + IdUbicacion`

Cuando los datos recibidos lo permiten, también se restringe por `IdStock`,
licencia, vencimiento, estado, presentación y talla/color. Solo si queda una
coincidencia única se hidratan la identidad y cantidades teóricas congeladas,
se limpia `IdUbicacion_nuevo` y se desactivan `EsNuevo/Regularizar`.

Si no existe coincidencia o hay más de una, se conserva el flujo anterior.
Esta regla evita resolver por aproximación una licencia/lote equivocado.

## Regla reusable

En inventario cíclico, la identidad mínima de reconciliación es:

`IdInventarioEnc + IdProductoBodega + IdStock + IdUbicacion`

La selección visual debe operar sobre las filas visibles reales, no sobre
índices supuestamente equivalentes a row handles. Un conteo no asignado no es
automáticamente un sobrante: primero debe descartarse una fila congelada exacta
en la ubicación capturada.

## Validación y riesgos residuales

- `DAL.vbproj`: compilación limpia, 0 errores y 0 advertencias.
- Se preservó UTF-8 con BOM en ambos archivos VB.
- La compilación completa de `WMS.vbproj` está bloqueada por dependencias
  históricas ajenas al cambio (`SAPSYNCMAMPA`, recursos pre-serializados y
  referencias externas). El compile parcial no reportó errores en el form
  modificado.
- El audit gate emitió WARN únicamente porque omitió lookup DB; la correlación
  QA se ejecutó manualmente con SELECT y queda documentada arriba.
- Falta prueba funcional BOF: filtro por categoría, marcar individual/marcar
  todos y confirmar que todas las ubicaciones del SKU generan tarea.
- Falta prueba funcional DAL: conteo exacto congelado, coincidencia ambigua y
  ubicación realmente inexistente.

No se modificó HH/Java ni `Reference.vb`. Cualquier mejora HH debe viajar en
un cambio separado.
