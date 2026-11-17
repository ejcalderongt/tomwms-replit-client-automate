# Correccion de ingreso SAP: cantidades, cierre y marcas locales

Fecha: 2026-10-24. #CKFK20261024_SYNC_INGRESO_CANTIDADES

Rama verificada y modificada: `dev_2026_estable` de TOMWMS_BOF.
No se hizo commit, push, despliegue, envio SAP ni consultas/escrituras de datos.

## Evidencia

La usuaria reporto OC WMS 12077, numero 10005280 y DocEntry SAP 333376.
El log anuncio entrada 329662, luego rechazo de Close() y finalmente exito.
Las capturas de consultas sobre SBO_COLONIAL_PRODUCTIVA muestran la OC 333376
abierta y sin entradas asociadas; la entrada 329662 consultada pertenece a OC
333640 / numero 328352. No esta probado por que coincide ese numero con una
entrada ajena ni que hubo un fallo identico en versiones anteriores.

El codigo ignoraba el error de cierre, confirmaba SQL local antes de terminar
SAP y podia devolver True aun cuando la transaccion SAP ya no estuviera activa.
SAP documenta rollback automatico ante una llamada DI API fallida dentro de una
transaccion: https://help.sap.com/doc/089315d8d0f8475a9fc84fb919b501a3/10.0/en-US/SDKhelp/SAPbobsCOM~Company~StartTransaction.html

## Archivos y alcance

- SAPSYNCCUMBRE/Clases Interface Sync/Pedido_Compra/clsSyncSAPPedidoCompra.vb:
  seleccion por OC/recepcion/bodega, comparacion decimal a seis posiciones por
  producto/linea/presentacion, cantidades historicas independientes y sin mutar
  objetos, detalle de la primera diferencia, no Close/Update sobre OC ya cerrada,
  errores de creacion/cierre/update detienen el proceso, guard de transaccion
  SAP activa, validacion de referencia y orden base despues del commit.
  Las referencias y marcas SQL del ingreso comparten transaccion con propietario
  exterior y se confirman despues de SAP; error anterior al commit revierte SQL.
  La comparacion se ejecuta antes de adquirir bloqueos SQL de escritura.
  El mensaje previo al commit informa que la entrada aun es provisional.
  El retorno de devolucion se propaga para evitar aceptar un resultado False
  ignorado al introducir el guard de creacion.
- TOMIMSV4/DAL/Transacciones/Recepcion/Recepcion_Detalle/clsLnTrans_re_det_Partial.vb:
  Get_All_By_Orden_Compra_Filtro cargaba No_Linea en IdProductoBodega;
  ahora carga Obj.No_Linea. No se cambia SQL ni esquema.

No se modifico el helper compartido clsSapTransaction ni sus otros llamadores.
El parametro SQL adicional es opcional para conservar compatibilidad de llamadas.
Se conservaron las validaciones de referencia ajena aplicadas anteriormente.

## Validacion

Compilacion aislada exitosa de Entity, DAL y SAPSYNCCUMBRE, con fuentes de esta
rama y artefactos en scratch/20261024-sap-ingreso-fix/build. Se recompilo Entity
porque el binario local anterior no incluia propiedades existentes en la rama.
No se sustituyeron binarios de trabajo.

20 pruebas aisladas correctas. El ejecutable de pruebas incorpora los bloques
reales de calculo, decision de cierre y guard de commit de la fuente candidata,
con dependencias simuladas. Incluye cantidades de los seis productos reportados,
ruido decimal, faltantes/sobrantes, historicos, no mutacion, producto repetido,
presentaciones, pallet, factor invalido, OC cerrada, cierre real, back order,
rechazos de Close/Update y transaccion SAP perdida/activa.

git diff --check correcto. Ambos VB guardados UTF-8 con BOM y CRLF.
No hubo pruebas de integracion contra SAP ni SQL. La causa concreta de la
diferencia del documento requiere observar el nuevo detalle en una ejecucion.

## Limites y recuperacion

SAP y SQL no comparten una transaccion distribuida. Si SAP confirma y despues
falla la conciliacion/commit local, se muestra una advertencia con la referencia
SAP para comprobarla antes de reenvio. No se promete recuperacion automatica
ante caida del proceso o perdida de conexion durante la confirmacion.

Esta correccion no limpia marcas o referencias ya afectadas por intentos pasados.
Reindex remoto pendiente: token Brain no disponible en la sesion.

Copias anteriores, fuentes verificadas, proyectos de compilacion y pruebas:
scratch/20261024-sap-ingreso-fix/.
