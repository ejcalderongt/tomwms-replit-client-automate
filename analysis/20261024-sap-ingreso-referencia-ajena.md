# Entrada SAP asociada a otra orden

Fecha: 2026-10-24. #CKFK20261024_SYNC_INGRESO_SAP

Rama final: `dev_2026_estable`, repositorio local `TOMWMS_BOF`.
Archivo: `SAPSYNCCUMBRE/Clases Interface Sync/Pedido_Compra/clsSyncSAPPedidoCompra.vb`.

## Evidencia y causa

El incidente reporta WMS IdOrdenCompraEnc=12077, DocNum=10005280,
DocEntry de orden SAP=333376. La interfaz acepta la entrada SAP 329642,
que la usuaria identifica como asociada a 10005290.

Flujo: `Enviar_Transacciones_De_Ingreso_SAP` lee `trans_re_oc` por orden y
recepcion mediante `Get_Objetos_Documento_Ingreso`. Luego
`Enviar_Entrada_Mercancia_OC_SAP` llama
`Documento_Ingreso_Ya_Existe_En_SAP` antes de crear una entrada.
La funcion solo comprobaba existencia por DocEntry y coincidencia opcional de
DocNum. Al devolver True, el llamador marcaba la orden como enviada.
Faltaba comprobar el documento base de las lineas SAP.

La procedencia historica del valor 329642 no se ha demostrado: no se consultaron
las bases del cliente ni SAP. El catalogo historico de Killios no contiene las
columnas ERP modernas de `trans_re_oc`; no valida el estado del cliente afectado.

## Correccion local

El helper recibe el DocEntry esperado de la OC. Antes de aceptar la referencia,
exige que la entrada no este cancelada, tenga lineas y que todas sus lineas
tengan BaseType de orden de compra y BaseEntry igual al esperado.
Una referencia ajena lanza error antes del retorno exitoso, de la creacion de
otra entrada y de la marca local de exito. Se elimina el nombre personal del
mensaje y los comentarios del archivo. No se cambian datos ni esquema SQL.

El cambio inicial se retiro de `tom_2028_prd` y se adapto al archivo de
`dev_2026_estable`, que contiene cambios adicionales propios de esa rama.
Newtonsoft.Json.xml se preservo byte a byte; su contenido coincide con la rama
destino. Quedo un respaldo en el stash
`codex-preservar-Newtonsoft-cambio-rama-20261024` y una copia en scratch.

## Validacion y limites

- Compilacion aislada Debug/x86 de SAPSYNCCUMBRE exitosa con MSBuild de VS 18,
  bibliotecas locales AppGlobal/DAL/Entity e Interop.SAPbobsCOM existente.
  La copia de validacion del proyecto usa la referencia Interop explicita porque
  la biblioteca COM no esta registrada para el entorno de compilacion.
- `git diff --check` correcto; archivo VB conserva UTF-8 BOM.
- No se ejecuto la interfaz ni se verificaron documentos en SAP.
- No hubo commit, push, despliegue ni modificaciones de datos.
- Reindex remoto pendiente: token Brain no disponible en esta sesion.
- Validar orden base evita esta asociacion entre ordenes; no demuestra por si
  solo identidad de una recepcion parcial dentro de una misma orden.

Consultas preparadas, sin ejecutar:
`scratch/20261024-sap-ingreso/diagnostico-solo-lectura.sql`.
Log de compilacion:
`scratch/20261024-sap-ingreso/build-dev_2026_estable.log`.
