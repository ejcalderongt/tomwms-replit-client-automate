Partial Class clsCadenaConexion

#Region "INTERFACE NAV - BYB"

    Public Property IdConfiguracionInterface As Integer = -1
    Public Property URLBodegas As String = "http://186.151.196.178:50501/DynamicsNAV80/WS/PRALCASA/Page/Ficha_Bodegas"
    Public Property URLProductos As String = "http://186.151.196.178:50501/DynamicsNAV80/WS/PRALCASA/Page/Ficha_Bodegas"
    Public Property URLProveedores As String = "http://186.151.196.178:50501/DynamicsNAV80/WS/PRALCASA/Page/Proveedores"
    Public Property URLPedidosCompra As String = "http://186.151.196.178:50501/DynamicsNAV80/WS/PRALCASA/Page/Pedidos_Compra"
    Public Property URLPedidosTransferencia As String = "http://186.151.196.178:50501/DynamicsNAV80/WS/PRALCASA/Page/Pedidos_Transferencia"
    Public Property URLCategoriasProducto As String = "http://186.151.196.178:50501/DynamicsNAV80/WS/PRALCASA/Page/Categorias_Productos"
    Public Property URLGruposProducto As String = "http://186.151.196.178:50501/DynamicsNAV80/WS/PRALCASA/Page/Grupos_Productos"
    Public Property URLTablaConversiones As String = "http://186.151.196.178:50501/DynamicsNAV80/WS/PRALCASA/Page/Tabla_Conversiones"
    Public Property URLLotePedidoCompra As String = "http://186.151.196.178:50601/DynamicsNAV80/WS/PRALCASA/Codeunit/Lote_PedidoCompra"
    Public Property URLCantidadPedidoCompra As String = "http://186.151.196.178:50601/DynamicsNAV80/WS/PRALCASA/Codeunit/CantidadRecibir_PedidoCompra"
    Public Property URLLotePedidoTrans As String = "http://186.151.196.178:50601/DynamicsNAV80/WS/PRALCASA/Codeunit/Lote_PedidoTransferencia"
    Public Property URLCantidadPedidoTrans As String = "http://186.151.196.178:50601/DynamicsNAV80/WS/PRALCASA/Codeunit/CantidadEnviar_PedidoTransferencia"
    Public Property URLRegistroRecepCompra As String = "http://186.151.196.178:50601/DynamicsNAV80/WS/PRALCASA/Codeunit/Registra_Recepcion_Compra"
    Public Property URLRegistroTransfEnvio As String = "http://186.151.196.178:50601/DynamicsNAV80/WS/PRALCASA/Codeunit/Registra_Transfer_Envio"
    Public Property URLRegistroTransfRecep As String = "http://186.151.196.178:50601/DynamicsNAV80/WS/PRALCASA/Codeunit/Registra_Transfer_Envio"
    Public Property URLAjusteInventario As String = "http://186.151.196.178:50601/DynamicsNAV80/WS/PRALCASA/Codeunit/Ajustes_Inventario"
    Public Property URLLotesTransfRec As String = "http://186.151.196.178:50601/DynamicsNAV80/WS/PRALCASA/Page/Pagina_lotes"
    Public Property URLSeries As String = "http://186.151.196.178:50601/DynamicsNAV80/WS/PRALCASA/Page/Series"
    Public Property URLDespachoRoad As String = "http://192.168.0.4/MI3/Road/srvRoadDespacho.svc"
    Public Property URLSecciones_Diario As String = "http://186.151.196.178:50601/DynamicsNAV80/WS/PRALCASA/Page/Secciones_Diario"
    Public Property URLPedidosVenta As String = "http://SRVNAVP001ORI1.byb.gt:50601/DynamicsNAV110/WS/PRALCASA/Page/Pedidos"
    Public Property URLEnviosAlm As String = "http://SRVNAVP001ORI1.byb.gt:50601/DynamicsNAV110/WS/PRALCASA/Page/Envio_alm"
    Public Property URLReceAlm As String = "http://186.151.196.178:1050/DynamicsNAV110/WS/PRALCASA/Page/Recep_Almacen"
    Public Property URLOrdenesProduccion As String = "http://SRVNAVP001ORI1.byb.gt:50601/DynamicsNAV110/WS/PRALCASA/Page/OP_Lanzadas"
    Public Property URLCUWMS As String = "http://186.151.196.178:50605/DynamicsNAV110/WS/PRALCASA/Codeunit/CUWMS"
    Public Property URLCreaPicking As String = "http://186.151.196.178:7047/DynamicsNAV110/WS/PRALCASA/Page/Crea_picking"
    Public Property WSDevolucion As String = "http://168.194.73.242:1050/DynamicsNAV110/WS/PRALCASA/Page/Devolucion"
    Public Property WSDiarioAlmacen As String = "http://168.194.73.242:1050/DynamicsNAV110/WS/PRALCASA/Page/Diario_Almacen"
    Public Property WSDevolucionVenta As String = "http://168.194.73.242:1050/DynamicsNAV110/WS/PRALCASA/Page/Devolucion_venta"
    Public Property WSPicking As String = "http://168.194.73.242:1050/DynamicsNAV110/WS/PRALCASA/Page/Picking"
    Public Property WSMovProductos As String = "http://168.194.73.242:1050/DynamicsNAV110/WS/PRALCASA/Page/Mov_Productos"
    Public Property WSUbicarAlmacen As String = "http://168.194.73.242:1050/DynamicsNAV110/WS/PRALCASA/Page/Ubicar_Almacen"
    Public Property WSDimensiones As String = "http://SRVNAVP001ORI1.byb.gt:7047/DynamicsNAV110/WS/PRALCASA/Page/Dimensiones"

    ''' <summary>
    ''' #EJC202211221047
    ''' </summary>
    ''' <returns></returns>
    Public Property WSTOMHH As String = ""

    Public Property UsuarioWS As String = "wmsbodega1"
    Public Property ClaveWS As String = "31/05+Byb"
    Public Property URLClientes As String = ""

#End Region

    ''' <summary>
    ''' #EJC20210104: Define el formato de documento de despacho.
    ''' </summary>
    ''' <returns></returns>
    Public Property Formato_Despacho As Integer = 1

    ''' <summary>
    ''' #CKFK20240312: Define el tab de recepción que va a estar habilitado.
    ''' </summary>
    ''' <returns></returns>
    Public Property Formato_Recepcion As Integer = 1

    Public Property Modo_Debug As Boolean = False

#Region "INTERFACE SAPBO"

    Public Property LICENSESERVER_SAP_BO As String = ""
    Public Property SAP_COMPANY_DB As String = ""
    Public Property SAP_COMPANY_DB2 As String = ""
    Public Property SERVER_BD_SAP As String = ""
    Public Property SAP_USR As String = ""
    Public Property SAP_USR_PW As String = ""
    Public Property SAP_DB_USR As String = ""
    Public Property SAP_DB_PW As String = ""
    Public Property SAP_DB_VERSION As String = "2017"

#End Region

#Region "INTERFACE SQL - CEALSA"

    Public Property SERVER_ERP As String = "192.168.0.63"
    Public Property NOMBRE_BD_ERP As String = "aritecdb"
    Public Property USUARIO_BD_ERP As String = "dts_userdb"
    Public Property CLAVE_BD_ERP As String = "G9q&9Tf2"
    Public Property TIMEOUT_BD_ERP As String = "0"


#End Region


End Class