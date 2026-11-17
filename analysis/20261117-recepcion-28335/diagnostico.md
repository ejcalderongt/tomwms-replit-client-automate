# Recepción 28335: caja master con detalle repetido

Diagnóstico inicial anterior a la corrección. El estado de entrega posterior
está en `entrega.md`; este documento conserva la evidencia del defecto original.

<!-- #CKFK20261117_REC_CM_RCA -->

Alcance: TOMWMS_MAMPA_QA, recepción 28335, ingreso 28359, rama local
dev_2026_estable de HH y BOF. Diagnóstico mediante consultas SELECT y replay
aislado. No se modificaron datos ni código del producto. No se verificó el hash
del APK o del servicio desplegado; la evidencia operativa coincide con la ruta
del código local. Las afirmaciones permanecen draft hasta revisión independiente.

## Resultado

El detalle de compra 2, SKU AG00034526041365, espera 7 y registra 6.
Los lotes 1348 y 1349 de J100000635 esperan 4 y 3; ambos conservan recibido 0.
Los detalles de recepción 46491 y 46492 registran 3 cada uno.
Stock_rec 48381/48382 y stock 649920/649921 contienen 3 cada uno y ambos
apuntan a 46491; 46492 queda sin stock_rec asociado. La licencia está vacía
en ambos detalles, existencias y movimientos de recepción del SKU.

La recepción está Pendiente, activa, con Habilitar_Stock=true, bodega 31,
Control_Talla_Color=true y bloquear_lp_hh=false. Tiene 26 detalles y 109 unidades
registradas. De los 25 detalles de compra, únicamente el 2 tiene diferencia:
7 esperadas contra 6 recibidas. Las 26 filas de stock de esta recepción suman
109 y no tienen pedido, picking o despacho en esos campos. Esto no descarta
reservas externas u otras dependencias y no autoriza una reversión.

## Causa de la cantidad

En frm_list_rec_prod.java, Procesa_Caja_Master, líneas 339-345, cada lote busca
el mismo objeto de detalle de compra, modifica Cantidad y lo agrega directamente
a ListaFiltradaOc. No crea una copia por lote. Para los lotes 1348=4 y 1349=3,
el segundo cambio deja ambas posiciones apuntando al mismo objeto con cantidad 3.
LoadCajaMaster y Guardar_Caja_Master leen esa lista y construyen dos entradas de 3.

ReplayCajaMaster.java reproduce la semántica de referencias con los valores del
caso: [4,3] produce [3,3]=6. Invirtiendo el orden, [3,4] produce [4,4]=8.
Es una reproducción aislada de la secuencia causal, no una prueba Android integral.

El servicio Asignar_IdRecepcionDet_StockRec, en clsLnTrans_re_det_Partial.vb,
líneas 2784-2818, asigna todas las coincidencias de la misma línea e identificador
temporal al primer identity de recepción. Esto coincide con las dos existencias
vinculadas a 46491. No debe asumirse una relación uno a uno en una recuperación.

## Causa del bloqueo y recarga completa

Carolina reporta: «Debe escanear la licencia de la caja master» y, al escanearla,
aparece todo el listado. El clic de grilla en frm_list_rec_prod.java, líneas
935-959, exige escanear mientras un lote tenga Cantidad_recibida<Cantidad.
Procesa_Caja_Master, líneas 310-345, recupera todas las filas de la licencia,
sin construir exclusivamente el saldo pendiente.

Los lotes de este ingreso vinculados a J100000635 conservan recibido 0.
En el servicio GuardarHH_CM, clsLnTrans_re_enc_Partial.vb, líneas 7031-7063,
la actualización por talla/color exige igualdad entre licencia del detalle de
recepción y licencia del lote. La primera está vacía y la segunda es J100000635.
No puede correlacionar y continúa sin actualizar el lote. La limpieza de LP en
HH antes del envío (frm_recepcion_datos.java, alrededor de 9777-9790) es una
ruta candidata cuando los parámetros no permiten LP; no se capturó el request
original para atribuir con certeza el punto de limpieza en el despliegue.

## Recuperación y corrección propuestas

Revertir y repetir con la misma versión puede volver a producir 6 u 8, según el
orden. Antes de repetir hay que separar las filas por identidad de lote o agrupar
correctamente su saldo sin modificar el detalle original de compra; preservar
la correlación con el lote incluso cuando no corresponda generar LP física;
actualizar cada lote por su PK y correlacionar correctamente stock_rec con el
detalle real. El reintento debe usar saldos verificados en servidor.

No es necesario dar por perdida toda la recepción para recuperar una unidad.
La alternativa preferente a evaluar es corregir únicamente el SKU afectado y
conciliar los contadores de lote. Revertir toda la recepción afectaría 109 unidades
de 26 detalles y requiere comprobar reservas, movimientos posteriores,
dependencias y estado de integración. Aún no se eligió ni preparó una operación
de escritura. Cualquier corrección de datos requiere primero un script concreto
para revisión y autorización explícita para ejecutarlo.

## Evidencia revisable

- `resumen-evidencia.json`: filas pertinentes al SKU y configuración funcional.
- `evidence.json`: resultados de los ocho SELECT; la licencia se repite en otros
  documentos, por lo que solo las filas del ingreso 28359 y recepción 28335
  respaldan las conclusiones de este caso.
- `diagnostico.sql`, `schema.sql`: consultas realizadas.
- `ReplayCajaMaster.java`, `replay.txt`: reproducción del defecto.
- `claims.json`: procedencia por hash; rutas relativas a C:/Users/carol.

Las rutas históricas del Brain y sus dominios no se trataron como prueba de
vigencia. El catálogo se regeneró; los dominios de BOF y HH tienen alternativas
unresolved y el de base de datos conserva estado disputed. El diagnóstico usa
directamente código local identificado por commit y resultados de QA.
