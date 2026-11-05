# #EJC20261026_MHS_TARIMA_COMPLETA

## Regla y traza previa
Pedido 126 / picking 88 / MH07, TOMWMS_MHS_DEV. Criterio confirmado: contenido
original íntegro reservado al pedido, independientemente de cambios de ubicación.
Snapshot SELECT: JM000144=250 UM base (10 x 25); JM000141=1900 (76 x 25).
stock_rec, trans_re_det convertido y stock_res coinciden. Picking Nuevo/Tarima;
sin ejecución, packing ni cabeceras de licencia de empaque para esas LP.

frmLicenciaEmpaque.GenerarLicencias -> ConstruirSolicitudes ->
clsLnTrans_licencia_empaque.GenerarLoteDesdeSolicitudes -> resolución LP -> Insertar.
Cambio: detectar LP íntegras del contexto picking/pedido en la misma transacción,
registrar su código original y generar aparte el número adicional solicitado.
Generar e imprimir debe imprimir solo nuevas; recicladas conservan etiqueta física.
El detalle operativo de packing continúa en trans_packing_enc: registrar una
cabecera no debe simular que el producto ya fue pickeado, verificado o empacado.

## Implementación y contrato

- Entity: EsRecicladaRecepcion persistida; contador de impresiones real, sin simular
  una impresión para ocultar la etiqueta reutilizada.
- DAL: parámetro opcional IdPickingEnc conserva los llamadores tradicionales.
  Contexto validado contra pedido vigente y bodega dentro de la transacción.
  Solo Tipo_Preparacion=Tarima y Requiere_Preparacion habilitan reciclaje.
- GenerarEnTransaccion reserva con UPDLOCK/HOLDLOCK la resolución y consume
  correlativos solo para solicitudes adicionales; cabeceras originales y nuevas
  confirman o revierten juntas.
- RegistrarTarimasCompletas contrasta stock_rec activo, stock actual, reserva del
  pedido y cantidad_solicitada del picking en UM base. La cantidad de recepción
  y la solicitada se multiplican por factor de producto_presentacion (25 en el caso).
- Clave de comparación: recepción, propietario-bodega, producto, estado,
  presentación, UM, lote, serial y talla/color. No contiene ubicación.
  Se valida también stock_rec contra su IdRecepcionDet en trans_re_det.
  Precisión decimal(28,6); tolerancia máxima 0.000001 UM base.
- Una reserva de otro pedido/picking en la misma bodega descarta la LP.
  Hallazgo adicional: JM000141 existe en stock_res de bodega 14, pedido 74,
  cantidad 25; no afecta la comparación del contenido de MH07.
- La unicidad global de trans_licencia_empaque y el packing de otros pedidos/
  bodegas sí se respetan. La licencia reciclada existente para el mismo pedido
  se retorna con el mismo ID; no se reactiva una anulada ni se reasigna una ajena.
- Tope de 500 incluye recicladas y adicionales; se conserva mínimo de una adicional.
- UI: cantidad por pedido se presenta como adicionales; columna Etiqueta reutilizada.
  Generar e imprimir filtra las recicladas. El botón independiente Imprimir permite
  una reimpresión explícita; no es el flujo automático.
- Filtro pendientes de impresión excluye recicladas; el filtro impresas sigue
  mostrando únicamente las que tienen impresiones reales. Todas muestra ambas.
- Desde pedido seleccionado sin contexto explícito se usa su IdPickingEnc.
- Se elimina el carácter accidental | delante de For en ImprimirLicencias,
  previamente documentado como bloqueo de compilación.

## Esquema y despliegue

DBA canónico: C:/Users/yejc2/source/repos/DBA/migrations/20261026_mhs_tarimas_completas/

- aplicar.sql: columna bit NOT NULL con default 0; idempotente. Aplicada solo a
  10.0.0.31 / TOMWMS_MHS_DEV. Filas históricas quedan como no recicladas.
- revertir.sql: primero revertir binarios; se niega a borrar la marca si hay
  licencias recicladas para no perder trazabilidad.
- candidatas-diagnostico.sql: misma consulta de contenido, parámetros Pedido/Bodega/Picking.
- validar-dev.ps1: ejecuta DAL compilado dentro de transacciones con rollback.
- Antes de usar el DAL nuevo en otra base debe aplicarse la migración correspondiente.
  No se desplegaron WebServices ni se aplicaron cambios a producción.
- Binario local: TOMIMSV4/TOMIMSV4/bin/Debug/WMS_MHS.exe. Reiniciar BOF para cargarlo.

## Validación y evidencia

- BOF WMS.vbproj Debug/AnyCPU: compilación correcta, bof-build.log.
- WSHHRN.vbproj Debug/AnyCPU: compilación correcta, ws-build.log.
- Warnings existentes de resolución de ensamblados, DevExpress de evaluación y
  una propiedad obsoleta de frmPrincipal02; no son errores de este cambio.
- validar-dev.ps1: PASS, validacion-dev.log. Pruebas con DLL real:
  2 completas + 1 adicional; repetición con mismos IDs reciclados y consumo +1
  por solicitud; sin impresiones ficticias; reserva parcial; stock parcial;
  recepción distinta; mismo total con lote distinto; picking parcial; otro pedido;
  Granel sin reciclaje; origen incompatible; anulada; despachado; sin contexto;
  pedido ajeno al picking; rollback final de licencias y correlativos.
- La consulta GetSingleByLicPlate dentro de la transacción devuelve el contrato
  de packing: PEDIDO/126, Estado GENERADA, LP original y marca de reciclaje.
- Ninguna prueba simuló picking, verificación ni packing completados. Los cambios
  de datos de prueba se revirtieron; las inserciones de prueba pueden dejar saltos
  de identity en DEV, no licencias ni correlativos consumidos.
- Revisión de nulos/reglas: null-safety.txt, business-rules.txt y gate.yml.
  GroupBy produce grupos no vacíos: First() es seguro en ese contexto.
- Sin edición de Reference.vb, sin cambios Android, sin commits automáticos.
- No se ejecutó la interfaz ni se enviaron etiquetas a una impresora física.
  Sin prueba de carga o concurrencia simultánea; bloqueos revisados en código.
- Brain remoto responde 404 y no hay token de importación: documentación local
  actualizada; reindexación remota pendiente de disponibilidad del servicio.
