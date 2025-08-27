Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "ITransaccionesOut" in both code and config file together.
<ServiceContract()>
Public Interface ITransaccionesOut

    ''' <summary>
    ''' Obtiene una lista de los registros pendientes de procesar de la tabla i_nav_transaciones_out
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function Get_Ingresos_Pendientes_De_Procesar() As List(Of clsBeI_nav_transacciones_out)

    <OperationContract()>
    Function Get_Despachos_Pendientes_De_Procesar() As List(Of clsBeI_nav_transacciones_out)

    <OperationContract()>
    Function Get_Ingresos_Pendientes_De_Procesar_By_No_Pedido(ByVal pNoPedido As String) As List(Of clsBeI_nav_transacciones_out)

    <OperationContract()>
    Function Get_Despachos_Pendientes_De_Procesar_By_No_Pedido(ByVal pNoPedido As String) As List(Of clsBeI_nav_transacciones_out)

    <OperationContract()>
    Function Get_Ingresos_Pendientes_De_Procesar_By_No_Pedido_And_IdTipoDocumento(pIdTipoDocumento As Integer, pNoPedido As String) As List(Of clsBeI_nav_transacciones_out)

    ''' <summary>
    ''' 'Actualiza un registro que ya fue procesado del ERP.
    ''' </summary>
    ''' <param name="BeINavTransaccionesOut">Recibe un objeto del tipo BeINavTransaccionesOut</param>
    ''' <returns></returns>
    <OperationContract()>
    Function Actualizar_Registro_Procesado(ByRef BeINavTransaccionesOut As clsBeI_nav_transacciones_out) As Integer

    ''' <summary>
    ''' 'Actualiza un registro de almacenaje que ya fue procesado en el ERP
    ''' </summary>
    ''' <param name="No_Documento_Procesado_ERP">Recibe el no_documento</param>
    ''' <param name="pEnviado">Recibe true or false</param>
    ''' <returns></returns>
    <OperationContract()>
    Function Actualizar_Registro_Procesado_By_No_Referencia_ERP_Almacenaje(ByVal No_Documento_Procesado_ERP As String,
                                                                           ByVal pEnviado As Boolean) As Integer

    ''' <summary>
    ''' 'Actualiza un registro de servcios que ya fue procesado en el ERP
    ''' </summary>
    ''' <param name="No_Documento_Procesado_ERP">Recibe el no_documento</param>
    ''' <param name="pEnviado">Recibe true or false</param>
    ''' <returns></returns>
    <OperationContract()>
    Function Actualizar_Registro_Procesado_By_No_Referencia_ERP_Servicios(ByVal No_Documento_Procesado_ERP As String,
                                                                          ByVal pEnviado As Boolean) As Integer

    <OperationContract()>
    Function Get_Ajustes_Pendientes_Envio(ByRef Resultado As String) As List(Of clsBeAjustesMI3)

    ''' <summary>
    ''' Inserta un mensaje de error al procesar una transacción en el ERP
    ''' </summary>
    ''' <param name="pMensajeInterface"></param>
    ''' <returns>Devuelve la cantidad de filas afectadas por el insert</returns>
    <OperationContract()>
    Function Insertar_Mensaje_Error_ERP(pMensajeInterface As clsBeI_nav_transacciones_out_error) As Integer

    <OperationContract()>
    Function Get_Mensajes_Error_ERP(ByVal IdDirectorio As Integer,
                                    ByVal FechaDesde As Date,
                                    ByVal FechaHasta As Date) As List(Of clsBeI_nav_transacciones_out_error)

    <OperationContract()>
    Function Actualizar_Ajuste_Procesado(ByVal pIdAjusteEnc As Integer,
                                         ByVal pEnviado_A_ERP As Boolean,
                                         ByVal pReferencia As String) As Integer

    <OperationContract()>
    Function Get_Ingresos_Pendientes_De_Procesar_By_IdTipoDocumento(ByVal pIdTipoDocumento As Integer) As List(Of clsBeI_nav_transacciones_out)

    <OperationContract()>
    Function Get_Despachos_Pendientes_De_Procesar_By_No_Pedido_And_IdTipoDocumento(ByVal pNoPedido As String,
                                                                                   ByVal pIdTipoDocumento As clsDataContractDI.tTipoDocumentoSalida) As List(Of clsBeI_nav_transacciones_out)

    <OperationContract()>
    Function Get_Stock_Jornada_By_IdPropietario_And_Rango_Fechas(ByVal pIdPropietario As Integer,
                                                                 ByVal FechaDesde As Date,
                                                                 ByVal FechaHasta As Date) As List(Of clsBeStock_jornada)

    <OperationContract()>
    Function Get_Almacenaje_Historico_By_IdPropietario_And_Rango_Fechas(ByVal pIdCliente As Integer,
                                                                        ByVal pIdConsolidador As Integer,
                                                                        ByVal pNoOrden As String,
                                                                        ByVal FechaDesde As Date,
                                                                        ByVal FechaHasta As Date,
                                                                        ByVal Almacen As clsDataContractDI.tTipoAlmacen,
                                                                        ByVal Clasificacion As String,
                                                                        ByVal pTipoRubro As clsDataContractDI.tRubroERP,
                                                                        ByVal pProducto As String) As List(Of clsBeStock_jornada_logistico)

    <OperationContract()>
    Function Get_Almacenaje_Historico_By_No_Orden_And_Rango_Fechas(ByVal pNoOrden As String,
                                                                   ByVal FechaDesde As Date,
                                                                   ByVal FechaHasta As Date) As List(Of clsBeStock_jornada_logistico)

    <OperationContract()>
    Function Get_Servicios_By_IdPropietario_And_Rango_Fechas(ByVal pIdCliente As Integer,
                                                             ByVal pIdConsolidador As Integer,
                                                             ByVal pNoOrden As String,
                                                             ByVal FechaDesde As Date,
                                                             ByVal FechaHasta As Date,
                                                             ByVal Almacen As clsDataContractDI.tTipoAlmacen,
                                                             ByVal pCodigoProducto As String) As List(Of clsBe_Servicio_Logistico)
    <OperationContract()>
    Function Get_Servicios_By_No_Orden_And_Rango_Fechas(ByVal pNoOrden As String,
                                                        ByVal FechaDesde As Date,
                                                        ByVal FechaHasta As Date) As List(Of clsBeStock_jornada_logistico)

    <OperationContract()>
    Function Get_Devolucion_De_Ruta_By_Filtros(ByVal pIdTipoDocumento As clsDataContractDI.tTipoDocumentoIngreso,
                                               ByVal pCodigoBodega As String,
                                               ByVal pNoDocumentoDevolucionRef As String) As List(Of clsBeI_nav_transacciones_out)

    <OperationContract()>
    Function Insertar_Referencia_Documento_Salida(ByVal pDocumentoRef As clsBeTrans_pe_docu_ref) As Integer

    <OperationContract()>
    Function Actualizar_Registro_Procesado_By_No_Pedido(pNoPedido As String) As Integer

    <OperationContract()>
    Function Actualizar_Registro_Procesado_By_No_Pedido_And_IdTipoDocumento(pNoPedido As String, pIdTipoDocumento As Integer) As Integer

    <OperationContract()>
    Function Set_Flag_Activo_Documento_Referencia_Salida(IdDocumentoRef As Integer, Activo As Boolean) As Integer

    <OperationContract()>
    Function Get_Documento_Referencia_By_Filtros(Serie As String, NoFactura As String, Empresa As String, Bodega As String) As clsBeTrans_pe_docu_ref

    <OperationContract()>
    Function Actualizar_Registro_Procesado_By_IdDespacho_And_Codigo_Producto(pNoPedido As String, IdDespachoWMS As Integer, pCodigoProducto As String) As Integer

    <OperationContract()>
    Function Get_Single_By_IdTransaccion(IdTransaccion As Integer) As clsBeI_nav_transacciones_out

    <OperationContract()>
    Function Actualizar_Registro_Procesado_By_IdRecepcionEnc_And_Codigo_Producto(pNoPedido As String, IdDespachoWMS As Integer, pCodigoProducto As String) As Integer

    <OperationContract()>
    Function Desactivar_Referencia_Documento_Salida(pReferencia As String) As Integer

    <OperationContract()>
    Function Insertar_Referencia_Documento_Salida_Con_Detalle(pDocumentoRef As clsBeTrans_pe_docu_ref, lDetalle As List(Of clsBeTrans_pe_docu_ref_det)) As Integer

    <OperationContract()>
    Function Actualizar_Registro_Procesado_By_NoPedido_And_Codigo_Producto(pNoPedido As String, pCodigoProducto As String) As Integer

End Interface