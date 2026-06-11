Imports System.Data.SqlClient
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports TOMWMS.WSCantidadPedidoTransferencia
Imports TOMWMS.WSCreaPicking
Imports TOMWMS.WSFichaBodegas
Imports TOMWMS.WSLotePedidoTransferencia
Imports TOMWMS.WSPaginaLotes
Imports TOMWMS.WSPedidoTransferencia
Imports TOMWMS.WSPicking
Imports TOMWMS.WSRecepcionesAlm
Imports TOMWMS.WSRegistraTransferEnvio

Public Class clsSyncNavPedidoTraslado : Inherits clsInterfaceBase

    Implements IDisposable

    Property pBodega As String = ""

    Private fichaPedidosTraslado() As Pedidos_Transferencia

    Dim VContadorBitacoraTomims As Integer = 0
    Dim VContadorBitacoraIntermedia As Integer = 0

    Private wsPedidoTransferenciaService As New Pedidos_Transferencia_Service() With
    {
    .UseDefaultCredentials = UsarCredencialesPorDefecto,
    .Credentials = CredencialesConexion
    }

    Dim wsLotePedidoTransferencia As New Lote_PedidoTransferencia With {
    .UseDefaultCredentials = UsarCredencialesPorDefecto,
    .Credentials = CredencialesConexion}

    Dim wsCantidadPedidoTransferencia As New CantidadEnviar_PedidoTransferencia With {
    .UseDefaultCredentials = UsarCredencialesPorDefecto,
    .Credentials = CredencialesConexion}

    Private wsRegistra_Transfer_Envio As New Registra_Transfer_Envio() With
            {
            .UseDefaultCredentials = UsarCredencialesPorDefecto,
            .Credentials = CredencialesConexion
            }

    Public Sub Dispose() Implements IDisposable.Dispose
        If wsPedidoTransferenciaService IsNot Nothing Then
            wsPedidoTransferenciaService.Dispose()
            wsPedidoTransferenciaService = Nothing
        End If
    End Sub

    Dim BeNavEjecRes As clsBeI_nav_ejecucion_res = Nothing

    Public Function Importar_Pedidos_Transferencia_Recepcion_Desde_WSNav_A_TablaIntermedia(ByVal lblprg As RichTextBox,
                                                                                           ByRef prg As System.Windows.Forms.ProgressBar,
                                                                                           ByRef cnnLog As SqlConnection) As Boolean

        Importar_Pedidos_Transferencia_Recepcion_Desde_WSNav_A_TablaIntermedia = False

        Dim lConnection As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Actualizar_Progreso(lblprg, vbCrLf)
            Actualizar_Progreso(lblprg, "Procesando documento en tabla intermedia.")

            Dim lPedidosTransfRec As New List(Of Pedidos_Transferencia)

            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface,
                                                                        lConnection, lTransaction)

            If BeConfigEnc Is Nothing Then
                If BD.Instancia.IdConfiguracionInterface = 0 Then
                    Throw New Exception("No éstá definida la configuración de interface para el Id: " & BD.Instancia.IdConfiguracionInterface & " verifique el el conn.ini que se especificó el identificador de configuración para la interface.")
                Else
                    Throw New Exception("No éstá definida la configuración de interface para el Id: " & BD.Instancia.IdConfiguracionInterface & " verifique en la bd que existe el registro asociado al identificador de inteface: " & BD.Instancia.IdConfiguracionInterface)
                End If
            End If

            '#EJC20180503: Pedidos de transferencia dirigidos hacia -> bodegas de MP.
            lPedidosTransfRec = Get_Pedidos_Transferencia_Recepcion_FromWS(lConnection, lTransaction, True)

            BeNavEjecucionRes.Registros_ws = lPedidosTransfRec.Count

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, cnnLog)

            Dim BeI_nav_PedidoCompra As clsBeI_nav_ped_compra_enc
            Dim BeI_nav_PedidoCompraDet As clsBeI_nav_ped_compra_det
            Dim BeProductoBodega As New clsBeProducto_bodega
            Dim BeNavLote As New clsBeI_nav_ped_compra_det_lote

            Actualizar_Progreso(lblprg, vbCrLf)
            Actualizar_Progreso(lblprg, String.Format("Pedidos de traslado en WS (NAV): {0} ", fichaPedidosTraslado.Count))

            prg.Maximum = lPedidosTransfRec.Count

            Dim vContador As Integer = 0

            ''Borrar tablas intermedias.
            If clsLnI_nav_ped_compra_det_lote.EliminarTodos(lConnection, lTransaction) _
                AndAlso clsLnI_nav_ped_compra_det.EliminarTodos(lConnection, lTransaction) _
                AndAlso clsLnI_nav_ped_compra_enc.EliminarTodos(lConnection, lTransaction) Then

                For Each PT As Pedidos_Transferencia In lPedidosTransfRec

                    BeI_nav_PedidoCompra = New clsBeI_nav_ped_compra_enc

                    If PT.No = "PT-243520" Then
                        Debug.Print("Aquí")
                    End If

                    CopyObject(PT, BeI_nav_PedidoCompra)

                    If Not PT.Posting_DateSpecified Then
                        PT.Posting_Date = Now.Date
                    ElseIf PT.Posting_Date.Year <= 1000 Then
                        PT.Posting_Date = Now.Date
                    End If

                    If Not PT.Receipt_DateSpecified Then
                        PT.Receipt_Date = Now.Date
                    ElseIf PT.Receipt_Date.Year <= 1000 Then
                        PT.Receipt_Date = Now.Date
                    End If

                    If Not PT.Shipment_DateSpecified Then
                        PT.Shipment_Date = Now.Date
                    ElseIf PT.Shipment_Date.Year <= 1000 Then
                        PT.Shipment_Date = Now.Date
                    End If

                    'Proveedor
                    BeI_nav_PedidoCompra.Buy_From_Vendor_Name = PT.Transfer_from_Name
                    BeI_nav_PedidoCompra.Buy_From_Vendor_No = PT.Transfer_from_Code
                    BeI_nav_PedidoCompra.No = PT.No
                    BeI_nav_PedidoCompra.Status = PT.Status

                    Actualizar_Progreso(lblprg, String.Format("Procesando Pedido Compra: {0} ", BeI_nav_PedidoCompra.No, vbNewLine))

                    Try
                        'PT-086861
                        'PT-086728
                        '#EJC20180503: Es un documento de transferencia desde otra bodega hacia las bodegas de MP.
                        BeI_nav_PedidoCompra.Is_Internal_Transfer = True
                        BeI_nav_PedidoCompra.Status = 1 'Lanzado

                        If Not clsLnI_nav_ped_compra_enc.Exist(BeI_nav_PedidoCompra.No, lConnection, lTransaction) Then
                            'Insertar encabezado
                            clsLnI_nav_ped_compra_enc.Insertar(BeI_nav_PedidoCompra, lConnection, lTransaction)
                        End If

                        VContadorBitacoraIntermedia += 1

                        prg.Value = vContador

                        vContador += 1

                        Application.DoEvents()

                        Dim lLotes As New List(Of Pagina_lotes)
                        Dim lLotesTmp As New List(Of Pagina_lotes)
                        Dim listLotes As New List(Of String)
                        Dim vLote As New Pagina_lotes
                        Dim cantLote = 0
                        Dim vTotalLotes As Integer = 0

                        'Insertar detalle
                        If Not PT.TransferLines Is Nothing Then

                            For Each L As Transfer_Order_Line In PT.TransferLines

                                BeI_nav_PedidoCompraDet = New clsBeI_nav_ped_compra_det

                                Try

                                    Try

                                        CopyObject(L, BeI_nav_PedidoCompraDet)
                                        BeI_nav_PedidoCompraDet.No = L.Item_No
                                        BeI_nav_PedidoCompraDet.Type = 2 'Articulo                                    
                                        BeI_nav_PedidoCompraDet.Quantity = L.Quantity

                                    Catch ex As Exception
                                        Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
                                    End Try

                                    BeI_nav_PedidoCompraDet.NoEnc = PT.No

                                    If Not L.Item_No Is Nothing Then

                                        BeProductoBodega = clsLnProducto_bodega.Existe(L.Item_No, BeConfigEnc.Idbodega, lConnection, lTransaction)

                                        'Existe el producto en el maestro?
                                        If Not BeProductoBodega Is Nothing Then

                                            '#EJC20180504: 
                                            'L.Qty_to_Ship La cantidad a recibir.
                                            'L.Qty_Shiped cuando ya se registró el envío.
                                            If (L.Qty_to_Ship = 0) AndAlso (L.Quantity_Shipped > 0) AndAlso (L.Quantity_Received < L.Quantity_Shipped) Then

                                                If clsLnI_nav_ped_compra_det.Exist(BeI_nav_PedidoCompraDet, lConnection, lTransaction) Then
                                                    clsLnI_nav_ped_compra_det.Actualizar(BeI_nav_PedidoCompraDet, lConnection, lTransaction)
                                                    VContadorBitacoraIntermedia += 1
                                                Else
                                                    clsLnI_nav_ped_compra_det.Insertar(BeI_nav_PedidoCompraDet,
                                                                                       lConnection,
                                                                                       lTransaction)
                                                    VContadorBitacoraIntermedia += 1
                                                End If

                                                lLotes = clsSyncLotes.Get_Lista_Lotes(PT.No,
                                                                                      L.Item_No,
                                                                                      L.Line_No)

                                                '#CKFK20240531 No se inicializaban la lista de lotes temporales
                                                lLotesTmp = New List(Of Pagina_lotes)

                                                If lLotes.Count > 1 Then

                                                    listLotes = lLotes.Select(Function(x) x.Lot_No).Distinct.ToList

                                                    For Each Lote In listLotes

                                                        cantLote = 0
                                                        vTotalLotes = 0
                                                        vLote = New Pagina_lotes

                                                        For Each NavLote In lLotes.Where(Function(y) y.Lot_No = Lote).ToList
                                                            If vTotalLotes = 0 Then
                                                                vLote = NavLote
                                                            End If
                                                            cantLote += NavLote.Quantity_Base
                                                            vTotalLotes += 1
                                                        Next

                                                        vLote.Quantity_Base = cantLote
                                                        lLotesTmp.Add(vLote)

                                                    Next

                                                    lLotes = lLotesTmp

                                                End If

                                                For Each NavLote In lLotes

                                                    BeNavLote = New clsBeI_nav_ped_compra_det_lote
                                                    BeNavLote.NoEnc = PT.No
                                                    BeNavLote.source_ID = NavLote.Source_ID
                                                    BeNavLote.Source_Prod_Order_Line = NavLote.Source_Prod_Order_Line
                                                    BeNavLote.Item_No = NavLote.Item_No
                                                    BeNavLote.Lot_No = NavLote.Lot_No
                                                    BeNavLote.Expiration_Date = IIf(NavLote.Vencimiento_Calculado = "01/01/0001", "01/01/1900", NavLote.Vencimiento_Calculado)
                                                    BeNavLote.Entry_No = 0 'NavLote.Entry_No -> ya no vino en cambio de ws: #EJC20180516
                                                    BeNavLote.Source_Type = NavLote.Source_Type
                                                    BeNavLote.Quantity_Base = NavLote.Quantity_Base
                                                    BeNavLote.Variant_Code = IIf(NavLote.Variant_Code Is Nothing, "", NavLote.Variant_Code)

                                                    If clsLnI_nav_ped_compra_det_lote.Exist(BeNavLote, lConnection, lTransaction) Then
                                                        clsLnI_nav_ped_compra_det_lote.Actualizar(BeNavLote, lConnection, lTransaction)
                                                    Else
                                                        clsLnI_nav_ped_compra_det_lote.Insertar(BeNavLote, lConnection, lTransaction)
                                                    End If

                                                Next

                                            Else
                                                Actualizar_Progreso(lblprg, String.Format("La cantidad para el producto:{0} ya fue completada.{1}", L.Item_No, vbNewLine))
                                            End If

                                        Else

                                            Try

                                                clsLnI_nav_ejecucion_det_error.Inserta_Log("Producto no existe en maestro",
                                                                                           L.Item_No,
                                                                                           BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                           BeConfigDet.Idnavconfigdet,
                                                                                           cnnLog)

                                                Actualizar_Progreso(lblprg, String.Format("Producto no existe en maestro: {0}{1}", L.Item_No, vbNewLine))

                                            Catch ex As Exception
                                                Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
                                            End Try

                                        End If 'Fin 'Existe el producto en el maestro?                  

                                    End If

                                Catch ex As Exception

                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                            "Sin informacion",
                                                                            BeNavEjecucionEnc.IdEjecucionEnc,
                                                                            BeConfigDet.Idnavconfigdet,
                                                                            cnnLog)

                                    Actualizar_Progreso(lblprg, String.Format("Error al insertar Linea desde el ws a intermedia en pedido de compra: {0}{1}{2}", BeI_nav_PedidoCompraDet.No, vbNewLine, ex.Message))

                                End Try

                            Next

                        End If

                    Catch ex As Exception

                        clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                   BeI_nav_PedidoCompra.No,
                                                                   BeNavEjecucionEnc.IdEjecucionEnc,
                                                                   BeConfigDet.Idnavconfigdet,
                                                                   cnnLog)

                        Actualizar_Progreso(lblprg, String.Format("Error al insertar Encabezado OC desde ws a intermedia: {0}{1}{2}", BeI_nav_PedidoCompra.No, vbNewLine, ex.Message))

                    End Try

                Next

            End If

            lTransaction.Commit()

            Actualizar_Progreso(lblprg, vbNewLine)
            Actualizar_Progreso(lblprg, "Fin de procesamiento en tabla intermedia.")

            Importar_Pedidos_Transferencia_Recepcion_Desde_WSNav_A_TablaIntermedia = True

        Catch ex As Exception

            If Not lTransaction Is Nothing Then lTransaction.Rollback()

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet, cnnLog)

            Actualizar_Progreso(lblprg, String.Format("Error al insertar Ordenes Traslado desde ws a intermedia: {0}{1}", vbNewLine, ex.Message))

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))

        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Private Function Asigna_Unidad_De_Medida(ByRef BePedidoCompraDet As clsBeTrans_oc_det,
                                             ByRef navPedidoCompraDet As clsBeI_nav_ped_compra_det,
                                             ByRef BeUnidadMedidaPedCompra As clsBeUnidad_medida,
                                             ByRef BeProductoBodega As clsBeProducto_bodega,
                                             ByRef lblprg As RichTextBox,
                                             ByRef lConnection As SqlConnection,
                                             ByRef lTransaction As SqlTransaction,
                                             ByRef lConnectionLog As SqlConnection) As Boolean

        Asigna_Unidad_De_Medida = False

        Try

            'Existe el producto con U.M.Bas = U.M. de pedido de compra.
            If Not clsLnProducto.Existe(navPedidoCompraDet.No,
                                        BeUnidadMedidaPedCompra.IdUnidadMedida,
                                        lConnection,
                                        lTransaction) Then

                Dim BePresentacion As New clsBeProducto_Presentacion()

                BePresentacion = clsLnProducto_presentacion.
                Existe_By_IdProducto_And_NombrePresentacion(BeProductoBodega.IdProducto,
                                                            navPedidoCompraDet.Unit_of_Measure_Code,
                                                            lConnection,
                                                            lTransaction)

                If Not BePresentacion Is Nothing Then
                    'La presentación ya existe
                    BePedidoCompraDet.IdPresentacion = BePresentacion.IdPresentacion
                    BePedidoCompraDet.Presentacion.IdPresentacion = BePresentacion.IdPresentacion
                    BePedidoCompraDet.IdUnidadMedidaBasica = BeProductoBodega.Producto.IdUnidadMedidaBasica
                    BePedidoCompraDet.UnidadMedida.IdUnidadMedida = BeProductoBodega.Producto.IdUnidadMedidaBasica
                    BePedidoCompraDet.Nombre_unidad_medida_basica = BeProductoBodega.Producto.UnidadMedida.Nombre
                Else

                    Dim vFactorConv As Double = clsLnUnidad_medida_conversion.Get_Factor(BeUnidadMedidaPedCompra.IdUnidadMedida,
                                                                                         BeProductoBodega.Producto.UnidadMedida.IdUnidadMedida,
                                                                                         lConnection,
                                                                                         lTransaction)

                    'Existe factor para crear la presentación con la unidad de medida del pedido de compra.
                    If vFactorConv > 0 Then

                        BePresentacion = New clsBeProducto_Presentacion()
                        BePresentacion.IdPresentacion = clsLnProducto_presentacion.MaxID(lConnection, lTransaction) + 1
                        BePresentacion.IdProducto = BeProductoBodega.IdProducto
                        BePresentacion.Codigo_barra = BeProductoBodega.Producto.Codigo_barra + navPedidoCompraDet.Unit_of_Measure_Code
                        BePresentacion.Nombre = navPedidoCompraDet.Unit_of_Measure_Code
                        BePresentacion.Imprime_barra = True
                        BePresentacion.Peso = 0
                        BePresentacion.Alto = 0
                        BePresentacion.Largo = 0
                        BePresentacion.Ancho = 0
                        BePresentacion.Factor = vFactorConv
                        BePresentacion.MinimoExistencia = 0
                        BePresentacion.MaximoExistencia = 0
                        BePresentacion.User_agr = BeConfigEnc.IdUsuario
                        BePresentacion.User_mod = BeConfigEnc.IdUsuario
                        BePresentacion.Fec_agr = Now
                        BePresentacion.Fec_mod = Now
                        BePresentacion.Activo = True
                        BePresentacion.EsPallet = False
                        BePresentacion.Precio = 0
                        BePresentacion.MinimoPeso = 0
                        BePresentacion.MaximoPeso = 0
                        BePresentacion.Costo = 0
                        BePresentacion.CamasPorTarima = 0
                        BePresentacion.CajasPorCama = 0
                        BePresentacion.Genera_lp_auto = False
                        BePresentacion.Permitir_paletizar = False
                        BePresentacion.Sistema = True
                        BePresentacion.Codigo = BeProductoBodega.Producto.Codigo

                        Try

                            clsLnProducto_presentacion.Insertar(BePresentacion, lConnection, lTransaction)

                            BePedidoCompraDet.IdPresentacion = BePresentacion.IdPresentacion
                            BePedidoCompraDet.Presentacion.IdPresentacion = BePresentacion.IdPresentacion
                            BePedidoCompraDet.IdUnidadMedidaBasica = BeProductoBodega.Producto.IdUnidadMedidaBasica
                            BePedidoCompraDet.UnidadMedida.IdUnidadMedida = BeProductoBodega.Producto.IdUnidadMedidaBasica
                            BePedidoCompraDet.Nombre_unidad_medida_basica = BeProductoBodega.Producto.UnidadMedida.Nombre

                        Catch ex As Exception

                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                       BePedidoCompraDet.Codigo_Producto,
                                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                                       BeConfigDet.Idnavconfigdet,
                                                                       lConnectionLog)

                            Actualizar_Progreso(lblprg, String.Format("Error al insertar presentación: {0}{1}", ex.Message, vbNewLine))

                        End Try

                    Else

                        Throw New Exception(String.Format("Error: No existe factor en unidad_medida_conversion para Producto: {0} UnidMedBas {1} <> UnidMed Ped. Compra {2} ",
                                                          navPedidoCompraDet.No,
                                                          BeProductoBodega.Producto.UnidadMedida.Nombre,
                                                          navPedidoCompraDet.Unit_of_Measure_Code))

                    End If 'Fin Sí: 'Existe factor para crear la presentación con la unidad de medida del pedido de compra.                   

                End If 'Fin sí: Existe presentación.              

            Else
                'La unidad de medida básica del producto es = a la unidad de medida del pedido de compra.
                'Se utiliza la UM del pedido de compra aunque la básica del maestro sea otra porque existe factor de conversión
                BePedidoCompraDet.IdUnidadMedidaBasica = BeUnidadMedidaPedCompra.IdUnidadMedida
                BePedidoCompraDet.UnidadMedida.IdUnidadMedida = BeUnidadMedidaPedCompra.IdUnidadMedida
                BePedidoCompraDet.Nombre_unidad_medida_basica = navPedidoCompraDet.Unit_of_Measure_Code
            End If

            Asigna_Unidad_De_Medida = True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Function Importar_Pedidos_Transferencia_Recepcion_Desde_Tabla_Intermedia_A_Tabla_TOMIMS(ByRef lblprg As RichTextBox,
                                                                                                   ByRef prg As System.Windows.Forms.ProgressBar,
                                                                                                   Optional ByVal ForzarEjecucion As Boolean = False,
                                                                                                   Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False) As Boolean

        Importar_Pedidos_Transferencia_Recepcion_Desde_Tabla_Intermedia_A_Tabla_TOMIMS = False

        Dim CnnInterface As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransInterface As SqlTransaction = Nothing
        Dim DifCant As Double = 0

        Try

            If Not ForzarEjecucion Then

                If Not Ejecutar_Interfaz("Pedido compra") Then
                    Actualizar_Progreso(lblprg, "La configuración de la interface indica que no se debe ejecutar en este momento. ")
                    Exit Function
                End If

            End If

            CnnLog.Open()

            BeNavEjecucionEnc.IdEjecucionEnc = 0 '0'0' 0' clsLnI_nav_ejecucion_res.Max_IdEjecucionRes(CnnLog) + 1
            BeNavEjecucionEnc.IdNavConfigEnc = BD.Instancia.IdConfiguracionInterface
            BeNavEjecucionEnc.Fecha = Now

            clsLnI_nav_ejecucion_enc.Insertar_From_Interface(BeNavEjecucionEnc, CnnLog)

            BeNavEjecucionRes.IdEjecucionRes = 0 '0' clsLnI_nav_ejecucion_res.Max_IdEjecucionRes(CnnLog) + 1
            BeNavEjecucionRes.IdEjecucionEnc = BeNavEjecucionEnc.IdEjecucionEnc
            BeNavEjecucionRes.IdNavConfigDet = BeConfigDet.Idnavconfigdet
            BeNavEjecucionRes.Registros_ws = 0
            BeNavEjecucionRes.Registros_ti = 0
            BeNavEjecucionRes.Registros_WMS = 0
            BeNavEjecucionRes.Exitosa = False

            clsLnI_nav_ejecucion_res.Insertar(BeNavEjecucionRes, CnnLog)

            BeNavEjecRes = BeNavEjecucionRes

            CnnInterface.Open() : lTransInterface = CnnInterface.BeginTransaction(IsolationLevel.ReadUncommitted)

            'Aquí en esta línea se asigna la url del servicio, por eso reformatie, para encontrarla....
            wsPedidoTransferenciaService.Url = BD.Instancia.URLPedidosTransferencia

            Actualizar_Progreso(lblprg, "Consultando WebService de bodega en: " & My.MySettings.Default.DynamicsNavInterface_WsPedidoTransferencia_Pedidos_Transferencia_Service)

            wsPedidoTransferenciaService.Url = My.MySettings.Default.DynamicsNavInterface_WsPedidoTransferencia_Pedidos_Transferencia_Service

            If Not Pregunta_Si_LLena_Intermedia Then

                If Not Importar_Pedidos_Transferencia_Recepcion_Desde_WSNav_A_TablaIntermedia(lblprg, prg, CnnLog) Then
                    Exit Function
                End If

            Else

                If XtraMessageBox.Show("¿Llenar tabla intermedia desde NAV?", "Interface pedidos de compra.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If Not Importar_Pedidos_Transferencia_Recepcion_Desde_WSNav_A_TablaIntermedia(lblprg, prg, CnnLog) Then
                        Exit Function
                    End If
                End If

            End If

            Dim lPedidoCompraEnc As New List(Of clsBeI_nav_ped_compra_enc)

            Actualizar_Progreso(lblprg, "Consultando pedidos de compra en tabla intermedia ")

            lPedidoCompraEnc = clsLnI_nav_ped_compra_enc.GetAll(CnnInterface, lTransInterface, lblprg, prg, True)

            Actualizar_Progreso(lblprg, String.Format("Pedidos de Compra en tabla intermedia: {0}", lPedidoCompraEnc.Count))

            If lPedidoCompraEnc.Count > 0 Then

                Dim gBeOrdenCompra As clsBeTrans_oc_enc = Nothing
                Dim PedidoCompraExistente As clsBeTrans_oc_enc = Nothing
                Dim vContador As Integer = 0
                Dim vContadorLineasDet As Integer = 0
                Dim BeProveedorBodega As New clsBeProveedor_bodega
                Dim BeProductoBodega As New clsBeProducto_bodega
                Dim BePresentacion As New clsBeProducto_Presentacion
                Dim BePedidoCompraDet As New clsBeTrans_oc_det
                Dim BeOcDetLote As New clsBeTrans_oc_det_lote
                Dim BeOcDetLoteTmp As New clsBeTrans_oc_det_lote

                BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface,
                                                                        CnnInterface, lTransInterface)

                If BeConfigEnc Is Nothing Then
                    If BD.Instancia.IdConfiguracionInterface = 0 Then
                        Throw New Exception("No éstá definida la configuración de interface para el Id: " & BD.Instancia.IdConfiguracionInterface & " verifique el el conn.ini que se especificó el identificador de configuración para la interface.")
                    Else
                        Throw New Exception("No éstá definida la configuración de interface para el Id: " & BD.Instancia.IdConfiguracionInterface & " verifique en la bd que existe el registro asociado al identificador de inteface: " & BD.Instancia.IdConfiguracionInterface)
                    End If
                End If

                prg.Maximum = lPedidoCompraEnc.Count

                prg.Value = 0

                VContadorBitacoraTomims = 0

                Actualizar_Progreso(lblprg, "Trasladando documento a TOMWMS.")

                Dim BeUnidadMedidaPedCompra As New clsBeUnidad_medida()

                Dim vMensajeREsultadoCUWMS As String = ""


                '#EJC20210426: CodeUnit de NAV para WMS, agregado por la bodega de PT.
                Dim wsCUWMS As New CUWMS.CUWMS() With {.UseDefaultCredentials = UsarCredencialesPorDefecto,
                                                       .Credentials = CredencialesConexion
                                                      }

                wsCUWMS.Url = My.MySettings.Default.NavSync_CUWMS_CUWMS

                For Each navPedidoTransferRec As clsBeI_nav_ped_compra_enc In lPedidoCompraEnc

                    If navPedidoTransferRec.Status <> 0 Then

                        Actualizar_Progreso(lblprg, String.Format("Procesando Documento de transferencia: {0} ", navPedidoTransferRec.No, vbNewLine))

                        gBeOrdenCompra = New clsBeTrans_oc_enc() With {.Referencia = navPedidoTransferRec.No,
                                                                       .IdTipoIngresoOC = navPedidoTransferRec.Document_Type}

                        PedidoCompraExistente = clsLnTrans_oc_enc.Get_Single_By_Referencia(gBeOrdenCompra, CnnInterface, lTransInterface, True)

                        prg.Value = vContador

                        vContador += 1
                        vContadorLineasDet = 0

                        'El pedido de compra existe y debe ser actualizado.
                        If Not PedidoCompraExistente Is Nothing Then

                            gBeOrdenCompra.Activo = True

                            BeProveedorBodega = clsLnProveedor.Get_ProveedorBodega_By_Codigo_Proveedor(navPedidoTransferRec.Buy_From_Vendor_No,
                                                                                                   BeConfigEnc.Idbodega,
                                                                                                   CnnInterface,
                                                                                                   lTransInterface)
                            If gBeOrdenCompra.ProveedorBodega Is Nothing Then
                                gBeOrdenCompra.ProveedorBodega = New clsBeProveedor_bodega
                            End If

                            gBeOrdenCompra.IdProveedorBodega = BeProveedorBodega.IdAsignacion
                            gBeOrdenCompra.IdTipoIngresoOC = 2 'P.T. REC NAV
                            gBeOrdenCompra.No_Documento = navPedidoTransferRec.Vendor_Invoice_No
                            gBeOrdenCompra.User_Mod = BeConfigEnc.IdUsuario
                            gBeOrdenCompra.Fec_Mod = Now
                            gBeOrdenCompra.Procedencia = ""
                            gBeOrdenCompra.No_Marchamo = ""
                            gBeOrdenCompra.Referencia = navPedidoTransferRec.No
                            gBeOrdenCompra.Observacion = navPedidoTransferRec.Posting_Description
                            gBeOrdenCompra.Control_Poliza = False
                            gBeOrdenCompra.Push_To_NAV = True

                            If gBeOrdenCompra.IsNew Then
                                gBeOrdenCompra.ObjPoliza = Nothing
                            End If

                            clsLnTrans_oc_enc.Actualizar(gBeOrdenCompra, CnnInterface, lTransInterface)

                            Actualizar_Progreso(lblprg, String.Format("Procesando# : {0}{1}", navPedidoTransferRec.No, vbNewLine))

                            VContadorBitacoraTomims += 1

                            If navPedidoTransferRec.Lineas_Detalle.Count > 0 Then

                                For Each navPedidoCompraDet As clsBeI_nav_ped_compra_det In navPedidoTransferRec.Lineas_Detalle

                                    vContadorLineasDet += 1

                                    Try

                                        BeProductoBodega = clsLnProducto_bodega.Existe(navPedidoCompraDet.No,
                                                                                       BeConfigEnc.Idbodega,
                                                                                       CnnInterface,
                                                                                       lTransInterface)

                                        'Existe el producto en el maestro?
                                        If BeProductoBodega IsNot Nothing Then

                                            'Existe el producto en el detalle de la orden de compra en la tabla DE TOMWMS?
                                            BePedidoCompraDet = clsLnTrans_oc_det.Exist(PedidoCompraExistente.IdOrdenCompraEnc,
                                                                                        navPedidoCompraDet.Line_No,
                                                                                        CnnInterface,
                                                                                        lTransInterface)

                                            BeUnidadMedidaPedCompra.Nombre = navPedidoCompraDet.Unit_of_Measure_Code
                                            BeUnidadMedidaPedCompra = clsLnUnidad_medida.Existe_By_Codigo_And_IdPropietario(navPedidoCompraDet.Unit_of_Measure_Code,
                                                                                                                            BeConfigEnc.IdPropietario,
                                                                                                                            CnnInterface,
                                                                                                                            lTransInterface)

                                            'La unidad de medida existe?
                                            If BeUnidadMedidaPedCompra Is Nothing Then
                                                'unidad de medida no existe en tabla UNIDAD_MEDIDA
                                                Throw New Exception(String.Format("Producto: {0} UnidMedBas {1} No existe ",
                                                                     navPedidoCompraDet.No,
                                                                     BeProductoBodega.Producto.UnidadMedida.Nombre))

                                            End If 'Fin sí: unidad de medida no existe.

#Region "Cod_Variante_A_Presentacion"
                                            If navPedidoCompraDet.Variant_Code <> "" Then

                                                BePresentacion = clsLnProducto_presentacion.Existe_By_IdProducto_And_NombrePresentacion(BeProductoBodega.IdProducto,
                                                                                                                                        navPedidoCompraDet.Variant_Code,
                                                                                                                                        CnnInterface,
                                                                                                                                        lTransInterface)
                                                If BePresentacion Is Nothing Then
                                                    Throw New Exception("ERROR_202303031404: La presentación: " & navPedidoCompraDet.Variant_Code & " no existe para el código de producto " & navPedidoCompraDet.No)
                                                End If 'Fin sí: BePresentacion IsNothing (Presentación no existe y se insertó)

                                            End If 'Fin sí: Cod_Variante <> ""

#End Region

                                            'Producto No existe en la tabla de detalle DE TOMWMS. trans_oc_det
                                            If BePedidoCompraDet Is Nothing Then

                                                Try

                                                    BePedidoCompraDet = New clsBeTrans_oc_det
                                                    BePedidoCompraDet.IdOrdenCompraEnc = PedidoCompraExistente.IdOrdenCompraEnc
                                                    BePedidoCompraDet.IdOrdenCompraDet = clsLnTrans_oc_det.MaxID(BePedidoCompraDet.IdOrdenCompraEnc, CnnInterface, lTransInterface) + 1
                                                    BePedidoCompraDet.IdProductoBodega = BeProductoBodega.IdProductoBodega

                                                    If Not BePresentacion Is Nothing Then
                                                        BePedidoCompraDet.IdPresentacion = BePresentacion.IdPresentacion
                                                        BePedidoCompraDet.Presentacion.IdPresentacion = BePresentacion.IdPresentacion
                                                    Else
                                                        BePedidoCompraDet.IdPresentacion = 0
                                                    End If

                                                    BePedidoCompraDet.Nombre_producto = clsPublic.Quitar_Caracteres_No_Permitidos(navPedidoCompraDet.Description)
                                                    BePedidoCompraDet.Nombre_unidad_medida_basica = navPedidoCompraDet.Unit_of_Measure_Code
                                                    BePedidoCompraDet.Cantidad = navPedidoCompraDet.Quantity

                                                    '#EJC20220420: Hotfix, actualizar solo si lo recibido en el ERP es mayor que lo que tiene WMS.
                                                    If (navPedidoCompraDet.Quantity_Received > BePedidoCompraDet.Cantidad_recibida) Then
                                                        BePedidoCompraDet.Cantidad_recibida = navPedidoCompraDet.Quantity_Received
                                                    End If

                                                    BePedidoCompraDet.Costo = navPedidoCompraDet.Direct_Unit_Cost
                                                    BePedidoCompraDet.Total_linea = navPedidoCompraDet.Line_Amount
                                                    BePedidoCompraDet.No_Linea = navPedidoCompraDet.Line_No
                                                    BePedidoCompraDet.Activo = True
                                                    BePedidoCompraDet.Porcentaje_arancel = 0
                                                    BePedidoCompraDet.User_agr = BeConfigEnc.IdUsuario
                                                    BePedidoCompraDet.User_mod = BeConfigEnc.IdUsuario
                                                    BePedidoCompraDet.Atributo_variante_1 = navPedidoCompraDet.Variant_Code

                                                    If Asigna_Unidad_De_Medida(BePedidoCompraDet,
                                                                            navPedidoCompraDet,
                                                                            BeUnidadMedidaPedCompra,
                                                                            BeProductoBodega,
                                                                            lblprg,
                                                                            CnnInterface,
                                                                            lTransInterface,
                                                                            CnnLog) Then

                                                        clsLnTrans_oc_det.Insertar(BePedidoCompraDet, CnnInterface, lTransInterface)

                                                        If Not navPedidoTransferRec.Lineas_Detalle_Lotes Is Nothing Then

                                                            If navPedidoTransferRec.Lineas_Detalle_Lotes.Count > 0 Then

                                                                For Each Lote In navPedidoTransferRec.Lineas_Detalle_Lotes.Where(Function(x) x.NoEnc = navPedidoCompraDet.NoEnc _
                                                                AndAlso x.Item_No = navPedidoCompraDet.No _
                                                                AndAlso x.Source_Prod_Order_Line = navPedidoCompraDet.Line_No)

                                                                    BeOcDetLote = New clsBeTrans_oc_det_lote

                                                                    BeOcDetLote.IdOrdenCompraEnc = BePedidoCompraDet.IdOrdenCompraEnc
                                                                    BeOcDetLote.IdOrdenCompraDet = BePedidoCompraDet.IdOrdenCompraDet
                                                                    BeOcDetLote.IdOrdenCompraDetLote = clsLnTrans_oc_det_lote.MaxID(CnnInterface, lTransInterface) + 1
                                                                    BeOcDetLote.Cantidad = Lote.Quantity_Base
                                                                    BeOcDetLote.No_linea = Lote.Source_Prod_Order_Line
                                                                    BeOcDetLote.IdProductoBodega = BePedidoCompraDet.IdProductoBodega
                                                                    BeOcDetLote.Lote = Lote.Lot_No
                                                                    BeOcDetLote.Cantidad_recibida = 0
                                                                    BeOcDetLote.Codigo_producto = Lote.Item_No
                                                                    BeOcDetLote.IdPresentacion = BePedidoCompraDet.IdPresentacion
                                                                    BeOcDetLote.IdUnidadMedidaBasica = BePedidoCompraDet.IdUnidadMedidaBasica
                                                                    BeOcDetLote.Activo = 1
                                                                    BeOcDetLote.No_Documento = navPedidoTransferRec.No

                                                                    If clsLnTrans_oc_det_lote.Exist(BeOcDetLote.IdOrdenCompraEnc,
                                                                                                    BeOcDetLote.No_linea,
                                                                                                    BeOcDetLote.IdOrdenCompraDetLote,
                                                                                                    CnnInterface,
                                                                                                    lTransInterface) Is Nothing Then

                                                                        clsLnTrans_oc_det_lote.Insertar(BeOcDetLote, CnnInterface, lTransInterface)

                                                                    End If

                                                                Next

                                                            End If

                                                        End If

                                                        VContadorBitacoraTomims += 1

                                                    End If

                                                Catch ex As Exception

                                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                                               BePedidoCompraDet.Nombre_producto,
                                                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                               BeConfigDet.Idnavconfigdet, CnnLog)


                                                    Actualizar_Progreso(lblprg, String.Format("Error al insertar Detalle en : {0}{1}", ex.Message, vbNewLine))

                                                End Try

                                            Else 'Producto sí existe en tabla trans_oc_det

                                                Try

                                                    BePedidoCompraDet.IdOrdenCompraEnc = BePedidoCompraDet.IdOrdenCompraEnc
                                                    BePedidoCompraDet.IdOrdenCompraDet = BePedidoCompraDet.IdOrdenCompraDet
                                                    BePedidoCompraDet.IdProductoBodega = BeProductoBodega.IdProductoBodega
                                                    BePedidoCompraDet.Codigo_Producto = navPedidoCompraDet.No
                                                    BePedidoCompraDet.Nombre_producto = clsPublic.Quitar_Caracteres_No_Permitidos(navPedidoCompraDet.Description)
                                                    BePedidoCompraDet.Nombre_unidad_medida_basica = navPedidoCompraDet.Unit_of_Measure_Code

                                                    If BePedidoCompraDet.Cantidad = 0 Then
                                                        BePedidoCompraDet.Cantidad = navPedidoCompraDet.Quantity
                                                    Else

                                                        DifCant = navPedidoCompraDet.Quantity - BePedidoCompraDet.Cantidad

                                                        Select Case DifCant

                                                            Case 0
                                                                Actualizar_Progreso(lblprg, String.Format("La cantidad no se modificó para pedido {0} producto {1} {2}", navPedidoTransferRec.No, navPedidoCompraDet.No, vbNewLine))
                                                            Case Is > 0
                                                                Actualizar_Progreso(lblprg, String.Format("La cantidad incrementó respecto a TOM para pedido {0} producto {1} {2} ", navPedidoTransferRec.No, navPedidoCompraDet.No, vbNewLine))
                                                            Case Is < 0
                                                                Actualizar_Progreso(lblprg, String.Format("La cantidad disminuyó respecto al original en WMS  para pedido {0} producto {1} {2} ", navPedidoTransferRec.No, navPedidoCompraDet.No, vbNewLine))
                                                            Case Else
                                                                Exit Select
                                                        End Select

                                                        BePedidoCompraDet.Cantidad = navPedidoCompraDet.Quantity

                                                    End If

                                                    '#EJC20220420: Hotfix, actualizar solo si lo recibido en el ERP es mayor que lo que tiene WMS.
                                                    If (navPedidoCompraDet.Quantity_Received > BePedidoCompraDet.Cantidad_recibida) Then
                                                        BePedidoCompraDet.Cantidad_recibida = navPedidoCompraDet.Quantity_Received
                                                    End If

                                                    BePedidoCompraDet.Costo = navPedidoCompraDet.Direct_Unit_Cost
                                                    BePedidoCompraDet.Total_linea = navPedidoCompraDet.Line_Amount
                                                    BePedidoCompraDet.No_Linea = navPedidoCompraDet.Line_No
                                                    BePedidoCompraDet.Activo = True
                                                    BePedidoCompraDet.Porcentaje_arancel = 0
                                                    BePedidoCompraDet.User_agr = BeConfigEnc.IdUsuario
                                                    BePedidoCompraDet.User_mod = BeConfigEnc.IdUsuario
                                                    BePedidoCompraDet.Atributo_variante_1 = navPedidoCompraDet.Variant_Code

                                                    clsLnTrans_oc_det.Actualizar_Desde_Interface(BePedidoCompraDet, CnnInterface, lTransInterface)

                                                    If Not PedidoCompraExistente.DetalleLotes Is Nothing Then

                                                        If PedidoCompraExistente.DetalleLotes.Count > 0 Then

                                                            '#CKFK202300302 Puse esto porque considero que los lotes se deben de traer de la i_nav
                                                            For Each LoteNav In navPedidoTransferRec.Lineas_Detalle_Lotes.Where(Function(x) x.NoEnc = navPedidoCompraDet.NoEnc _
                                                                                                                                AndAlso x.Item_No = navPedidoCompraDet.No _
                                                                                                                                AndAlso x.Source_Prod_Order_Line = navPedidoCompraDet.Line_No)

                                                                BeOcDetLote = New clsBeTrans_oc_det_lote
                                                                BeOcDetLote.IdOrdenCompraEnc = BePedidoCompraDet.IdOrdenCompraEnc
                                                                BeOcDetLote.IdOrdenCompraDet = BePedidoCompraDet.IdOrdenCompraDet
                                                                BeOcDetLote.Cantidad = LoteNav.Quantity_Base
                                                                BeOcDetLote.No_linea = LoteNav.Source_Prod_Order_Line
                                                                BeOcDetLote.IdProductoBodega = BePedidoCompraDet.IdProductoBodega
                                                                BeOcDetLote.Lote = LoteNav.Lot_No
                                                                BeOcDetLote.Fecha_vence = LoteNav.Expiration_Date
                                                                BeOcDetLote.IdPresentacion = BePedidoCompraDet.IdPresentacion
                                                                BeOcDetLote.Presentacion = BePedidoCompraDet.Presentacion
                                                                BeOcDetLote.IdUnidadMedidaBasica = BePedidoCompraDet.IdUnidadMedidaBasica
                                                                BeOcDetLote.UnidadMedida = BePedidoCompraDet.UnidadMedida
                                                                BeOcDetLote.Activo = 1
                                                                BeOcDetLote.No_Documento = navPedidoCompraDet.No
                                                                BeOcDetLote.Cantidad_recibida = 0

                                                                BeOcDetLote.Codigo_producto = LoteNav.Item_No

                                                                BeOcDetLoteTmp = New clsBeTrans_oc_det_lote
                                                                BeOcDetLoteTmp = clsLnTrans_oc_det_lote.ExistLote(BeOcDetLote.IdOrdenCompraEnc,
                                                                                                        BeOcDetLote.No_linea,
                                                                                                        BeOcDetLote.Lote,
                                                                                                        CnnInterface,
                                                                                                        lTransInterface)

                                                                If BeOcDetLoteTmp Is Nothing Then
                                                                    BeOcDetLote.IdOrdenCompraDetLote = clsLnTrans_oc_det_lote.MaxID(CnnInterface, lTransInterface) + 1
                                                                    clsLnTrans_oc_det_lote.Insertar(BeOcDetLote, CnnInterface, lTransInterface)
                                                                Else
                                                                    BeOcDetLoteTmp.Cantidad = LoteNav.Quantity_Base
                                                                    clsLnTrans_oc_det_lote.Actualizar(BeOcDetLoteTmp, CnnInterface, lTransInterface)
                                                                End If

                                                            Next

                                                        End If

                                                    End If

                                                    VContadorBitacoraTomims += 1

                                                Catch ex As Exception

                                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                                               BePedidoCompraDet.Nombre_producto,
                                                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                               BeConfigDet.Idnavconfigdet,
                                                                                               CnnLog)

                                                    Actualizar_Progreso(lblprg, String.Format("Pedido Sin Detalle: {0}{1}", ex.Message, vbNewLine))

                                                End Try

                                            End If

                                        End If 'Fin sí: producto existe.

                                    Catch ex As Exception

                                        If Not navPedidoTransferRec.Lineas_Detalle Is Nothing Then
                                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                                       "Pedido Sin Detalle",
                                                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                       BeConfigDet.Idnavconfigdet,
                                                                                       CnnLog)
                                        End If

                                        Actualizar_Progreso(lblprg, String.Format("Pedido Sin Detalle: {0}{1}", ex.Message, vbNewLine))

                                    End Try

                                Next

                            End If

                        Else

                            '#EJC20180108: Se agregó validación porque el detalle de la O.C. puede tener líneas no válidas a recibir en el WMS.
                            'Si la O.C. tiene detalle en la tabla intermedia
                            If navPedidoTransferRec.Lineas_Detalle.Count = 0 Then
                                Actualizar_Progreso(lblprg, String.Format("Documento #:{0} No tiene detalle válido aparentemente.{1}", navPedidoTransferRec.No, vbNewLine))
                            Else

                                gBeOrdenCompra.IdOrdenCompraEnc = clsLnTrans_oc_enc.MaxID(CnnInterface, lTransInterface) + 1
                                gBeOrdenCompra.PropietarioBodega = New clsBePropietario_bodega
                                gBeOrdenCompra.PropietarioBodega.IdPropietarioBodega = clsLnPropietarios.Get_IdPropietarioBodega_By_IdBodega_And_IdPropietario(BeConfigEnc.Idbodega,
                                                                                                                                                               BeConfigEnc.IdPropietario,
                                                                                                                                                               CnnInterface,
                                                                                                                                                               lTransInterface)
                                gBeOrdenCompra.IdEstadoOC = 1
                                gBeOrdenCompra.Hora_Creacion = Now
                                gBeOrdenCompra.User_Agr = BeConfigEnc.IdUsuario
                                gBeOrdenCompra.Fec_Agr = Now
                                gBeOrdenCompra.Fecha_Creacion = Now
                                gBeOrdenCompra.Activo = True

                                BeProveedorBodega = clsLnProveedor.Get_ProveedorBodega_By_Codigo_Proveedor(navPedidoTransferRec.Buy_From_Vendor_No,
                                                                                                           BeConfigEnc.Idbodega,
                                                                                                           CnnInterface,
                                                                                                           lTransInterface)

                                If BeProveedorBodega Is Nothing Then

                                    BeProveedorBodega = clsSyncNavProveedor.Insertar_Bodega_Origen_Como_Proveedor(navPedidoTransferRec.Buy_From_Vendor_No, CnnInterface, lTransInterface, CnnLog, lblprg, prg)

                                    If BeProveedorBodega Is Nothing Then

                                        clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("El proveedor: {0} no existe, no se puede importar el pedido de compra: {1}",
                                                                                  navPedidoTransferRec.Buy_From_Vendor_No,
                                                                                  navPedidoTransferRec.No),
                                                                                  navPedidoTransferRec.Buy_From_Vendor_No,
                                                                                  BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                  BeConfigDet.Idnavconfigdet,
                                                                                  CnnLog)

                                        Actualizar_Progreso(lblprg, String.Format("Error al insertar el pedido de compra: {0} El proveedor: {1} no existe, ¿Ya se actualizó maestro de proveedores?", navPedidoTransferRec.Buy_From_Vendor_No, navPedidoTransferRec.No, vbNewLine))

                                        Throw New Exception("No logramos insertar el proveedor asociado a un pedido de compra, lamentamos el inconveniente")

                                    Else
                                        Actualizar_Progreso(lblprg, String.Format("El proveedor: {1} no existía pero se insertó para el pedido de compra: {0}. Nada de que preocuparse :) ", navPedidoTransferRec.Buy_From_Vendor_No, navPedidoTransferRec.No, vbNewLine))
                                    End If

                                End If

                                If gBeOrdenCompra.ProveedorBodega Is Nothing Then
                                    gBeOrdenCompra.ProveedorBodega = New clsBeProveedor_bodega
                                End If

                                gBeOrdenCompra.IdProveedorBodega = BeProveedorBodega.IdAsignacion
                                gBeOrdenCompra.IdTipoIngresoOC = BeConfigEnc.IdTipoDocumentoTransferenciasIngreso '2 'Transferencia de recepción
                                gBeOrdenCompra.No_Documento = navPedidoTransferRec.Vendor_Invoice_No
                                gBeOrdenCompra.User_Mod = BeConfigEnc.IdUsuario
                                gBeOrdenCompra.Fec_Mod = Now
                                gBeOrdenCompra.Procedencia = navPedidoTransferRec.Buy_From_Vendor_No
                                gBeOrdenCompra.No_Marchamo = ""
                                gBeOrdenCompra.Referencia = navPedidoTransferRec.No
                                gBeOrdenCompra.Observacion = navPedidoTransferRec.Posting_Description
                                gBeOrdenCompra.Control_Poliza = False
                                gBeOrdenCompra.IdBodega = BeConfigEnc.Idbodega
                                gBeOrdenCompra.Push_To_NAV = True

                                If gBeOrdenCompra.IsNew Then
                                    gBeOrdenCompra.ObjPoliza = Nothing
                                End If

                                gBeOrdenCompra.Enviado_A_ERP = False

                                Try

                                    '#EJC20210428: Crear primero la recepción de almacen en NAV, antes de insertar en DOC
                                    'en WMS.
                                    '#EJC20210426: En esta variable se asigna el número de documento de rececpción de NAV.

                                    '#CKFK20220429 Esto no aplica para bodega no avanzada
                                    If Es_Bodega_Avanzada(pBodega) Then

                                        If BeConfigEnc.Crear_Recepcion_De_Transferencia_NAV Then

                                            vMensajeREsultadoCUWMS = ""

                                            wsCUWMS.CreateTransferReceipt(navPedidoTransferRec.No, vMensajeREsultadoCUWMS)

                                            '#CKFK 2021116 Leer la recepcion e inicializar a 0 las cantidades
                                            Dim vUrlRecepcionAlmacen As String = My.MySettings.Default.NavSync_WSRecepcionesAlm_Recep_Almacen_Service

                                            Dim ws3 As New Recep_Almacen_Service() With
                                            {
                                                .UseDefaultCredentials = False,
                                                .Credentials = CredencialesConexion,
                                                .Url = vUrlRecepcionAlmacen
                                            }

                                            Dim RecepcionAlmacen As New Recep_Almacen()
                                            RecepcionAlmacen = ws3.Read(vMensajeREsultadoCUWMS)

                                            '#EJC20210324: Modificar cantidad a tomar/colocar 0, para que se pueda recibir parcial en HH.
                                            For Each Lu As Whse_Receipt_Line In RecepcionAlmacen.WhseReceiptLines
                                                Lu.Qty_to_Receive = 0
                                            Next

                                            '#EJC20210412: Actualizar la cantidad registrada en la HH en NAV.
                                            ws3.Update(RecepcionAlmacen)

                                            If gBeOrdenCompra.No_Documento.Trim = "" Then
                                                gBeOrdenCompra.No_Documento = vMensajeREsultadoCUWMS
                                            End If

                                            gBeOrdenCompra.No_Documento_Recepcion_ERP = vMensajeREsultadoCUWMS

                                        End If

                                    End If

                                    clsLnTrans_oc_enc.Insertar(gBeOrdenCompra, CnnInterface, lTransInterface)

                                    VContadorBitacoraTomims += 1

                                    If navPedidoTransferRec.Lineas_Detalle.Count > 0 Then

                                        For Each navPedidoCompraDet As clsBeI_nav_ped_compra_det In navPedidoTransferRec.Lineas_Detalle

                                            vContadorLineasDet += 1

                                            BePedidoCompraDet = New clsBeTrans_oc_det() With {.IdOrdenCompraEnc = gBeOrdenCompra.IdOrdenCompraEnc,
                                            .IdOrdenCompraDet = clsLnTrans_oc_det.MaxID(gBeOrdenCompra.IdOrdenCompraEnc, CnnInterface, lTransInterface) + 1}

                                            '#20180101_1203:Línea agregada para actulización en envío.
                                            'BePedidoCompraDet.No_Linea = navPedidoCompraDet.No

                                            BeProductoBodega = clsLnProducto_bodega.Existe(navPedidoCompraDet.No, BeConfigEnc.Idbodega, CnnInterface, lTransInterface)

                                            BeUnidadMedidaPedCompra = clsLnUnidad_medida.Existe_Unidad_Medida(navPedidoCompraDet.Unit_of_Measure_Code)

#Region "COD_VARIANTE_A_PRESENTACION"

                                            If navPedidoCompraDet.Variant_Code <> "" Then

                                                BePresentacion = clsLnProducto_presentacion.Existe_By_IdProducto_And_NombrePresentacion(BeProductoBodega.IdProducto,
                                                                                                                                        navPedidoCompraDet.Variant_Code,
                                                                                                                                        CnnInterface,
                                                                                                                                        lTransInterface)

                                                If BePresentacion Is Nothing Then
                                                    Throw New Exception("ERROR_202303031404F: La presentación: " & navPedidoCompraDet.Variant_Code & " no existe para el código de producto " & navPedidoCompraDet.No)
                                                End If

                                            End If

#End Region

                                            If BeProductoBodega IsNot Nothing Then

                                                Try

                                                    'Existe el producto en el detalle de la orden de compra en la tabla DE TOMWMS?
                                                    PedidoCompraExistente = clsLnTrans_oc_det.Exist(BePedidoCompraDet.IdOrdenCompraEnc,
                                                                                                    navPedidoCompraDet.Line_No,
                                                                                                    CnnInterface,
                                                                                                    lTransInterface)

                                                    'Producto No existe en la tabla de detalle DE TOMWMS. trans_oc_det
                                                    If PedidoCompraExistente Is Nothing Then

                                                        Try

                                                            BePedidoCompraDet.IdProductoBodega = BeProductoBodega.IdProductoBodega
                                                            BePedidoCompraDet.Codigo_Producto = navPedidoCompraDet.No
                                                            BePedidoCompraDet.Nombre_producto = clsPublic.Quitar_Caracteres_No_Permitidos(navPedidoCompraDet.Description)
                                                            BePedidoCompraDet.Nombre_unidad_medida_basica = navPedidoCompraDet.Unit_of_Measure_Code
                                                            BePedidoCompraDet.Cantidad = navPedidoCompraDet.Quantity

                                                            '#EJC20220420: Hotfix, actualizar solo si lo recibido en el ERP es mayor que lo que tiene WMS.
                                                            If (navPedidoCompraDet.Quantity_Received > BePedidoCompraDet.Cantidad_recibida) Then
                                                                BePedidoCompraDet.Cantidad_recibida = navPedidoCompraDet.Quantity_Received
                                                            End If

                                                            BePedidoCompraDet.Costo = navPedidoCompraDet.Direct_Unit_Cost
                                                            BePedidoCompraDet.Total_linea = navPedidoCompraDet.Line_Amount
                                                            BePedidoCompraDet.No_Linea = navPedidoCompraDet.Line_No
                                                            BePedidoCompraDet.Activo = True
                                                            BePedidoCompraDet.Porcentaje_arancel = 0
                                                            BePedidoCompraDet.User_agr = BeConfigEnc.IdUsuario
                                                            BePedidoCompraDet.User_mod = BeConfigEnc.IdUsuario
                                                            BePedidoCompraDet.Atributo_variante_1 = navPedidoCompraDet.Variant_Code

                                                            If Not BePresentacion Is Nothing Then
                                                                BePedidoCompraDet.IdPresentacion = BePresentacion.IdPresentacion
                                                                BePedidoCompraDet.Presentacion.IdPresentacion = BePresentacion.IdPresentacion
                                                            Else
                                                                BePedidoCompraDet.IdPresentacion = 0
                                                            End If

                                                            If Asigna_Unidad_De_Medida(BePedidoCompraDet,
                                                                                       navPedidoCompraDet,
                                                                                       BeUnidadMedidaPedCompra,
                                                                                       BeProductoBodega,
                                                                                       lblprg,
                                                                                       CnnInterface,
                                                                                       lTransInterface,
                                                                                       CnnLog) Then

                                                                clsLnTrans_oc_det.Insertar(BePedidoCompraDet, CnnInterface, lTransInterface)

                                                                If Not navPedidoTransferRec.Lineas_Detalle_Lotes Is Nothing Then

                                                                    If navPedidoTransferRec.Lineas_Detalle_Lotes.Count > 0 Then

                                                                        For Each Lote In navPedidoTransferRec.Lineas_Detalle_Lotes.Where(Function(x) x.NoEnc = navPedidoCompraDet.NoEnc _
                                                                                                                                         AndAlso x.Item_No = navPedidoCompraDet.No _
                                                                                                                                         AndAlso x.Source_Prod_Order_Line = navPedidoCompraDet.Line_No)

                                                                            BeOcDetLote = New clsBeTrans_oc_det_lote
                                                                            BeOcDetLote.IdOrdenCompraEnc = BePedidoCompraDet.IdOrdenCompraEnc
                                                                            BeOcDetLote.IdOrdenCompraDet = BePedidoCompraDet.IdOrdenCompraDet
                                                                            BeOcDetLote.IdOrdenCompraDetLote = clsLnTrans_oc_det_lote.MaxID(CnnInterface, lTransInterface) + 1
                                                                            BeOcDetLote.Cantidad = Lote.Quantity_Base
                                                                            BeOcDetLote.No_linea = Lote.Source_Prod_Order_Line
                                                                            BeOcDetLote.IdProductoBodega = BePedidoCompraDet.IdProductoBodega
                                                                            BeOcDetLote.Lote = Lote.Lot_No
                                                                            BeOcDetLote.Fecha_vence = Lote.Expiration_Date
                                                                            BeOcDetLote.IdPresentacion = BePedidoCompraDet.IdPresentacion
                                                                            BeOcDetLote.Presentacion = BePedidoCompraDet.Presentacion
                                                                            BeOcDetLote.IdUnidadMedidaBasica = BePedidoCompraDet.IdUnidadMedidaBasica
                                                                            BeOcDetLote.UnidadMedida = BePedidoCompraDet.UnidadMedida
                                                                            BeOcDetLote.Activo = 1
                                                                            BeOcDetLote.No_Documento = navPedidoCompraDet.No
                                                                            BeOcDetLote.Cantidad_recibida = 0

                                                                            BeOcDetLote.Codigo_producto = Lote.Item_No

                                                                            clsLnTrans_oc_det_lote.Insertar(BeOcDetLote, CnnInterface, lTransInterface)

                                                                        Next

                                                                    End If

                                                                End If

                                                                VContadorBitacoraTomims += 1

                                                            End If

                                                        Catch ex As Exception

                                                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                                                       BePedidoCompraDet.Nombre_producto,
                                                                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                                       BeConfigDet.Idnavconfigdet,
                                                                                                       CnnLog)

                                                            Actualizar_Progreso(lblprg, String.Format("Error al insertar Detalle en : {0}{1}", ex.Message, vbNewLine))

                                                        End Try


                                                    Else 'Producto sí existe en tabla trans_oc_det

                                                        Try

                                                            BePedidoCompraDet.IdProductoBodega = BeProductoBodega.IdProductoBodega
                                                            BePedidoCompraDet.Codigo_Producto = navPedidoCompraDet.No
                                                            BePedidoCompraDet.Nombre_producto = clsPublic.Quitar_Caracteres_No_Permitidos(navPedidoCompraDet.Description)
                                                            BePedidoCompraDet.Nombre_unidad_medida_basica = navPedidoCompraDet.Unit_of_Measure_Code

                                                            If BePedidoCompraDet.Cantidad = 0 Then
                                                                BePedidoCompraDet.Cantidad = navPedidoCompraDet.Quantity
                                                            Else

                                                                DifCant = navPedidoCompraDet.Quantity - BePedidoCompraDet.Cantidad

                                                                Actualizar_Progreso(lblprg, vbNewLine)

                                                                Select Case DifCant

                                                                    Case 0
                                                                        Actualizar_Progreso(lblprg, String.Format("La cantidad no se modificó para pedido {0} producto {1} ", navPedidoTransferRec.No, navPedidoCompraDet.No))
                                                                    Case Is > 0
                                                                        Actualizar_Progreso(lblprg, String.Format("La cantidad incrementó respecto a TOM para pedido {0} producto {1} ", navPedidoTransferRec.No, navPedidoCompraDet.No))
                                                                    Case Is < 0
                                                                        Actualizar_Progreso(lblprg, String.Format("La cantidad disminuyó respecto al original en WMS  para pedido {0} producto {1} ", navPedidoTransferRec.No, navPedidoCompraDet.No))
                                                                    Case Else
                                                                        Exit Select
                                                                End Select

                                                                BePedidoCompraDet.Cantidad = navPedidoCompraDet.Quantity

                                                            End If

                                                            BePedidoCompraDet.Cantidad_recibida = navPedidoCompraDet.Quantity_Received
                                                            BePedidoCompraDet.Costo = navPedidoCompraDet.Direct_Unit_Cost
                                                            BePedidoCompraDet.Total_linea = navPedidoCompraDet.Line_Amount
                                                            BePedidoCompraDet.No_Linea = navPedidoCompraDet.Line_No
                                                            BePedidoCompraDet.Activo = True
                                                            BePedidoCompraDet.Porcentaje_arancel = 0
                                                            BePedidoCompraDet.User_agr = BeConfigEnc.IdUsuario
                                                            BePedidoCompraDet.User_mod = BeConfigEnc.IdUsuario
                                                            BePedidoCompraDet.Atributo_variante_1 = navPedidoCompraDet.Variant_Code

                                                            If Not BePresentacion Is Nothing Then
                                                                BePedidoCompraDet.IdPresentacion = BePresentacion.IdPresentacion
                                                                BePedidoCompraDet.Presentacion.IdPresentacion = BePresentacion.IdPresentacion
                                                            Else
                                                                BePedidoCompraDet.IdPresentacion = 0
                                                            End If

                                                            If Asigna_Unidad_De_Medida(BePedidoCompraDet,
                                                                                       navPedidoCompraDet,
                                                                                       BeUnidadMedidaPedCompra,
                                                                                       BeProductoBodega,
                                                                                       lblprg,
                                                                                       CnnInterface,
                                                                                       lTransInterface,
                                                                                       CnnLog) Then

                                                                clsLnTrans_oc_det.Actualizar(BePedidoCompraDet,
                                                                                             CnnInterface,
                                                                                             lTransInterface)

                                                                If Not navPedidoTransferRec.Lineas_Detalle_Lotes Is Nothing Then

                                                                    If navPedidoTransferRec.Lineas_Detalle_Lotes.Count > 0 Then

                                                                        For Each Lote In navPedidoTransferRec.Lineas_Detalle_Lotes.Where(Function(x) x.NoEnc = navPedidoCompraDet.NoEnc _
                                                                                                                                         AndAlso x.Item_No = navPedidoCompraDet.No _
                                                                                                                                         AndAlso x.Source_Prod_Order_Line = navPedidoCompraDet.Line_No)

                                                                            BeOcDetLote = New clsBeTrans_oc_det_lote
                                                                            BeOcDetLote.IdOrdenCompraEnc = BePedidoCompraDet.IdOrdenCompraEnc
                                                                            BeOcDetLote.IdOrdenCompraDet = BePedidoCompraDet.IdOrdenCompraDet
                                                                            BeOcDetLote.IdOrdenCompraDetLote = clsLnTrans_oc_det_lote.MaxID(CnnInterface, lTransInterface) + 1
                                                                            BeOcDetLote.Cantidad = Lote.Quantity_Base
                                                                            BeOcDetLote.No_linea = Lote.Source_Prod_Order_Line
                                                                            BeOcDetLote.IdProductoBodega = BePedidoCompraDet.IdProductoBodega
                                                                            BeOcDetLote.Lote = Lote.Lot_No
                                                                            BeOcDetLote.Fecha_vence = Lote.Expiration_Date
                                                                            BeOcDetLote.IdPresentacion = BePedidoCompraDet.IdPresentacion
                                                                            BeOcDetLote.Presentacion = BePedidoCompraDet.Presentacion
                                                                            BeOcDetLote.IdUnidadMedidaBasica = BePedidoCompraDet.IdUnidadMedidaBasica
                                                                            BeOcDetLote.UnidadMedida = BePedidoCompraDet.UnidadMedida
                                                                            BeOcDetLote.Activo = 1
                                                                            BeOcDetLote.No_Documento = navPedidoCompraDet.No
                                                                            BeOcDetLote.Cantidad_recibida = 0
                                                                            BeOcDetLote.Codigo_producto = Lote.Item_No

                                                                            clsLnTrans_oc_det_lote.Insertar(BeOcDetLote, CnnInterface, lTransInterface)

                                                                        Next

                                                                    End If

                                                                End If

                                                                VContadorBitacoraTomims += 1

                                                            End If
                                                        Catch ex As Exception

                                                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                                                       BePedidoCompraDet.Nombre_producto,
                                                                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                                       BeConfigDet.Idnavconfigdet,
                                                                                                       CnnLog)

                                                            Actualizar_Progreso(lblprg, String.Format("Error al insertar Detalle en : {0}{1}", ex.Message, vbNewLine))

                                                        End Try


                                                    End If

                                                Catch ex As Exception

                                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                      BePedidoCompraDet.Nombre_producto,
                                                                      BeNavEjecucionEnc.IdEjecucionEnc,
                                                                      BeConfigDet.Idnavconfigdet,
                                                                      CnnLog)

                                                    Actualizar_Progreso(lblprg, String.Format("Error al insertar desde ws a intermedia: {0}{1}{2}", BePedidoCompraDet.Nombre_producto, ex.Message, vbNewLine))

                                                End Try

                                            Else

                                                clsLnI_nav_ejecucion_det_error.Inserta_Log("Producto no existe en maestro ",
                                                                                          navPedidoCompraDet.No,
                                                                                          BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                          BeConfigDet.Idnavconfigdet,
                                                                                          CnnLog)

                                                Actualizar_Progreso(lblprg, String.Format("No existe Producto Bodega: {0}{1}", navPedidoCompraDet.No, vbNewLine))

                                            End If

                                        Next

                                    End If

                                    '#EJC20230124: Actualizar a importado (creo que 2 es otra cosa)
                                    navPedidoTransferRec.Status = 3 'Importado

                                    clsLnI_nav_ped_compra_enc.Actualizar_Estado(navPedidoTransferRec,
                                                                                CnnInterface,
                                                                                lTransInterface)

                                Catch ex As Exception

                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                               navPedidoTransferRec.No,
                                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                                               BeConfigDet.Idnavconfigdet,
                                                                               CnnLog)

                                    Actualizar_Progreso(lblprg, String.Format("Error al insertar pedido de traslado con Referencia: {0}{1}{2}", navPedidoTransferRec.No, vbNewLine, ex.Message))

                                End Try

                                Application.DoEvents()

                            End If

                        End If

                    Else
                        Actualizar_Progreso(lblprg, String.Format("Pedido de traslado inactivo {0} ", navPedidoTransferRec.No, vbNewLine))
                    End If

                Next

            End If

            lTransInterface.Commit()


            Actualizar_Progreso(lblprg, vbNewLine)
            Actualizar_Progreso(lblprg, String.Format("Pedidos de traslado procesados  correctamente: {0}", VContadorBitacoraTomims))

            BeNavEjecucionRes.Registros_ti = VContadorBitacoraIntermedia
            BeNavEjecucionRes.Registros_WMS = VContadorBitacoraTomims

            If VContadorBitacoraIntermedia = VContadorBitacoraTomims Then
                BeNavEjecucionRes.Exitosa = True
            Else
                BeNavEjecucionRes.Exitosa = False
            End If

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, CnnLog)

        Catch ex As Exception

            If Not lTransInterface Is Nothing Then lTransInterface.Rollback()
            prg.Value = 0

            '#EJC20171105_1259AM_REF01: Agregar excepción a log.
            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                              "",
                                              BeNavEjecucionEnc.IdEjecucionEnc,
                                              BeConfigDet.Idnavconfigdet, CnnLog)

            Actualizar_Progreso(lblprg, String.Format("Error al insertar pedido de compra a tabla DE TOMWMS: {0} {1}", ex.Message, vbNewLine))

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))

        Finally

            If CnnInterface.State = ConnectionState.Open Then CnnInterface.Close()
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()

            Actualizar_Progreso(lblprg, "Fin de procesamient en TOMWMS")

            Dim difSegundos As Double = DateDiff(DateInterval.Second, BeNavEjecucionEnc.Fecha, Now)
            Actualizar_Progreso(lblprg, String.Format("Tiempo transcurrido: {0} segundo(s)", difSegundos))

        End Try

    End Function

    Public Function Importar_Pedidos_Transferencia_Envio_Desde_WSNav_A_TablaIntermedia(ByVal lblprg As RichTextBox,
                                                                                       ByRef prg As System.Windows.Forms.ProgressBar,
                                                                                       ByRef cnnLog As SqlConnection) As Boolean

        Importar_Pedidos_Transferencia_Envio_Desde_WSNav_A_TablaIntermedia = False

        Dim lConnection As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            lblprg.AppendText("Consultando configuración de interface: " & BD.Instancia.IdConfiguracionInterface)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface,
                                                          lConnection,
                                                          lTransaction)

            If BeConfigEnc Is Nothing Then
                If BD.Instancia.IdConfiguracionInterface = 0 Then
                    Throw New Exception("No éstá definida la configuración de interface para el Id: " & BD.Instancia.IdConfiguracionInterface & " verifique el el conn.ini que se especificó el identificador de configuración para la interface.")
                Else
                    Throw New Exception("No éstá definida la configuración de interface para el Id: " & BD.Instancia.IdConfiguracionInterface & " verifique en la bd que existe el registro asociado al identificador de inteface: " & BD.Instancia.IdConfiguracionInterface)
                End If

            End If

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("********** PROCESANDO DOCUMENTO EN TABLA INTERMEDIA ********** ")
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()

            Dim lPedidosTraslado As New List(Of Pedidos_Transferencia)

            '#CKFK20230817 Obtengo los pedidos de transferencia de envío, salidas
            lPedidosTraslado = Get_Pedidos_Transferencia_Envio_FromWS(lConnection, lTransaction, True)

            BeNavEjecucionRes.Registros_ws = lPedidosTraslado.Count

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, cnnLog)

            Dim BeI_Nav_PedidoTraslado As clsBeI_nav_ped_traslado_enc
            Dim BeI_Nav_PedidoTrasladoDet As clsBeI_nav_ped_traslado_det
            Dim BeProductoBodega As New clsBeProducto_bodega

            lblprg.AppendText(String.Format("Pedidos de traslado en WS: {0} ", fichaPedidosTraslado.Count))
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()
            lblprg.Refresh()

            prg.Maximum = lPedidosTraslado.Count

            Dim vContador As Integer = 0
            Dim BeBodega As New clsBeBodega

            'If clsLnI_nav_ped_traslado_det.Eliminar_Todos(lConnection, lTransaction) _
            '    AndAlso clsLnI_nav_ped_traslado_enc.Eliminar_Todos(lConnection, lTransaction) Then

            For Each PC As Pedidos_Transferencia In lPedidosTraslado

                BeI_Nav_PedidoTraslado = New clsBeI_nav_ped_traslado_enc

                CopyObject(PC, BeI_Nav_PedidoTraslado)

                If Not PC.Posting_DateSpecified Then
                    PC.Posting_Date = Now.Date
                ElseIf PC.Posting_Date.Year <= 1000 Then
                    PC.Posting_Date = Now.Date
                End If

                If Not PC.Receipt_DateSpecified Then
                    PC.Receipt_Date = Now.Date
                ElseIf PC.Receipt_Date.Year <= 1000 Then
                    PC.Receipt_Date = Now.Date
                End If

                If Not PC.Shipment_DateSpecified Then
                    PC.Shipment_Date = Now.Date
                ElseIf PC.Shipment_Date.Year <= 1000 Then
                    PC.Shipment_Date = Now.Date
                End If

                BeI_Nav_PedidoTraslado.No = PC.No
                BeI_Nav_PedidoTraslado.Status = PC.Status
                BeI_Nav_PedidoTraslado.Transfer_from_Code = PC.Transfer_from_Code
                BeI_Nav_PedidoTraslado.Transfer_from_Name = PC.Transfer_from_Name
                BeI_Nav_PedidoTraslado.Transfer_to_Code = PC.Transfer_to_Code
                BeI_Nav_PedidoTraslado.Transfer_to_Name = PC.Transfer_to_Name
                BeI_Nav_PedidoTraslado.Transfer_to_Contact = PC.Transfer_to_Contact
                BeI_Nav_PedidoTraslado.Transfer_to_CodeField = PC.Transfer_to_Code

                lblprg.AppendText(String.Format("Procesando Pedido Traslado: {0} ", BeI_Nav_PedidoTraslado.No, vbNewLine))
                lblprg.AppendText(vbNewLine)
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                Try

                    If BeI_Nav_PedidoTraslado.No = "PT-086912" Then
                        Debug.Print("espera")
                    End If
                    'Insertar Encabezado
                    clsLnI_nav_ped_traslado_enc.Insertar(BeI_Nav_PedidoTraslado, lConnection, lTransaction)

                    VContadorBitacoraIntermedia += 1

                    prg.Value = vContador

                    vContador += 1

                    Application.DoEvents()

                    If Not PC.TransferLines Is Nothing Then

                        For Each L As Transfer_Order_Line In PC.TransferLines

                            BeI_Nav_PedidoTrasladoDet = New clsBeI_nav_ped_traslado_det

                            Try

                                CopyObject(L, BeI_Nav_PedidoTrasladoDet)

                                BeI_Nav_PedidoTrasladoDet.NoEnc = PC.No
                                BeI_Nav_PedidoTrasladoDet.No = L.Item_No

                                If Not L.Variant_Code Is Nothing Then
                                    Debug.Print("Espera")
                                End If

                                BeI_Nav_PedidoTrasladoDet.Variant_Code = L.Variant_Code

                                '#EJC20171106_0926AM_REF01: En pruebas este valor devolvía nothing para algunos elementos obtenidos por eso agregué validación 
                                '(Nothing podría ser el fin de la lista, sin embargo entró el ciclo en una de las líneas del pedido, por lo que la línea del pedido
                                'puede tener información en una línea que (tal vez) no sea un producto?, más bien es un servicio. (Asumo)

                                '#EJC20171106_1023AM_REF02: El valor nothing indica el final de la vista.
                                If Not L.Item_No Is Nothing Then


                                    BeProductoBodega = clsLnProducto_bodega.Existe(L.Item_No, BeConfigEnc.Idbodega, lConnection, lTransaction)

                                    'Existe el producto en el maestro?
                                    If Not BeProductoBodega Is Nothing Then

                                        '#EJC20211117: Si la unidad de medida básica del producto es diferente de la unidad de medida solicitada en el inventario
                                        'meter en el cod_variante la unidad solicitada y en Unit_of_Measure_Code la unidad de medida base
                                        'para que pida en presentación.
                                        If BeI_Nav_PedidoTrasladoDet.Variant_Code Is Nothing Then
                                            If BeI_Nav_PedidoTrasladoDet.Unit_of_Measure_Code <> BeProductoBodega.Producto.UnidadMedida.Nombre Then
                                                BeI_Nav_PedidoTrasladoDet.Variant_Code = BeI_Nav_PedidoTrasladoDet.Unit_of_Measure_Code
                                                BeI_Nav_PedidoTrasladoDet.Unit_of_Measure_Code = BeProductoBodega.Producto.UnidadMedida.Nombre
                                            End If
                                        End If

                                        'Si Cantidad enviada es 0 se importa
                                        If L.Quantity_Shipped <> L.Quantity Then

                                            lblprg.AppendText(String.Format("Procesando producto : {0}{1}", L.Item_No, vbNewLine))
                                            lblprg.AppendText(vbNewLine)
                                            lblprg.Refresh()
                                            lblprg.SelectionStart = lblprg.TextLength
                                            lblprg.ScrollToCaret()
                                            lblprg.ScrollToCaret()

                                            If clsLnI_nav_ped_traslado_det.Exist(BeI_Nav_PedidoTrasladoDet, lConnection, lTransaction) Then
                                                clsLnI_nav_ped_traslado_det.ActualizarFromIn(BeI_Nav_PedidoTrasladoDet, lConnection, lTransaction)
                                                VContadorBitacoraIntermedia += 1
                                            Else
                                                clsLnI_nav_ped_traslado_det.Insertar(BeI_Nav_PedidoTrasladoDet, lConnection, lTransaction)
                                                VContadorBitacoraIntermedia += 1
                                            End If

                                        End If

                                    Else

                                        Try

                                            clsLnI_nav_ejecucion_det_error.Inserta_Log("Producto no existe en maestro",
                                                                                           L.Item_No,
                                                                                           BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                           BeConfigDet.Idnavconfigdet,
                                                                                           cnnLog)

                                            lblprg.AppendText(String.Format("Producto no existe en maestro: {0}{1}", L.Item_No, vbNewLine))
                                            lblprg.AppendText(vbNewLine)
                                            lblprg.Refresh()
                                            lblprg.SelectionStart = lblprg.TextLength
                                            lblprg.ScrollToCaret()

                                        Catch ex As Exception
                                            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
                                        End Try

                                    End If 'FIn Existe el producto en el maestro?

                                Else
                                    Debug.Print("_: " & L.Description)
                                End If

                            Catch ex As Exception

                                clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                               "Sin informacion",
                                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                                               BeConfigDet.Idnavconfigdet, cnnLog)

                                lblprg.AppendText(String.Format("Error al insertar Linea desde el ws a intermedia en pedido de traslado: {0}{1}{2}", BeI_Nav_PedidoTrasladoDet.No, vbNewLine, ex.Message))
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()

                            End Try

                        Next

                    Else
                        Console.WriteLine("Pedido de compra sin lineas de detalle?")
                    End If

                Catch ex As Exception

                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                          BeI_Nav_PedidoTraslado.No,
                                                          BeNavEjecucionEnc.IdEjecucionEnc,
                                                          BeConfigDet.Idnavconfigdet, cnnLog)

                    lblprg.AppendText(String.Format("Error al insertar Encabezado PT desde ws a intermedia: {0}{1}{2}", BeI_Nav_PedidoTraslado.No, vbNewLine, ex.Message))
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                End Try

            Next

            'End If

            lTransaction.Commit()

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("********** FIN DE INSERCIÓN EN TABLA INTERMEDIA ********** ")
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Importar_Pedidos_Transferencia_Envio_Desde_WSNav_A_TablaIntermedia = True

        Catch ex As Exception

            If Not lTransaction Is Nothing Then lTransaction.Rollback()

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet, cnnLog)

            lblprg.AppendText(String.Format("Error al insertar Ordenes Traslado desde ws a intermedia: {0}{1}", vbNewLine, ex.Message))

            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))

        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Private Function Inserta_Linea_Detalle_Pedido(ByVal pIdPedidoEnc As Integer,
                                                  ByVal PDet As clsBeI_nav_ped_traslado_det,
                                                  ByVal BeProducto As clsBeProducto,
                                                  ByVal vDiasVencimientoCliente As Integer,
                                                  ByVal BeUnidadMedida As clsBeUnidad_medida,
                                                  ByRef lblprg As RichTextBox,
                                                  ByRef lConnectionInterface As SqlConnection,
                                                  ByRef CnnLog As SqlConnection,
                                                  ByRef lTransInterface As SqlTransaction) As Boolean

        Inserta_Linea_Detalle_Pedido = False

        Dim pBePedidoDet As New clsBeTrans_pe_det
        Dim pBeStockRes As New clsBeStock_res
        Dim BeNavConfigEnc As New clsBeI_nav_config_enc

        Try

            If BeConfigEnc Is Nothing Then

                BeNavConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface,
                                                                 lConnectionInterface,
                                                                 lTransInterface)

                BeConfigEnc = BeNavConfigEnc

            Else

                If BeConfigEnc.Idnavconfigenc = -1 Then

                    BeNavConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface,
                                                                     lConnectionInterface,
                                                                     lTransInterface)

                    BeConfigEnc = BeNavConfigEnc

                End If

            End If

            pBePedidoDet = New clsBeTrans_pe_det
            pBePedidoDet.IdPedidoDet = 0
            pBePedidoDet.No_linea = PDet.Line_No
            pBePedidoDet.Atributo_Variante_1 = PDet.Variant_Code
            pBePedidoDet.IdPedidoEnc = pIdPedidoEnc
            pBePedidoDet.Producto = New clsBeProducto
            pBePedidoDet.Producto.IdProducto = clsLnProducto.Get_Id_Producto_By_IdProductoBodega(BeProducto.IdProductoBodega,
                                                                                                 lConnectionInterface,
                                                                                                 lTransInterface)
            pBePedidoDet.Producto.IdProductoBodega = BeProducto.IdProductoBodega
            pBePedidoDet.IdProductoBodega = BeProducto.IdProductoBodega
            pBePedidoDet.Producto.Codigo = PDet.Item_No
            pBePedidoDet.IdPresentacion = 0
            pBePedidoDet.IdUnidadMedidaBasica = BeProducto.UnidadMedida.IdUnidadMedida
            pBePedidoDet.Cantidad = PDet.Quantity
            pBePedidoDet.Peso = 0 'PDet.Quantity
            pBePedidoDet.Precio = 0
            pBePedidoDet.No_recepcion = 0
            pBePedidoDet.Cant_despachada = 0
            pBePedidoDet.IdEstado = BeConfigEnc.IdProductoEstado
            pBePedidoDet.Ndias = vDiasVencimientoCliente
            pBePedidoDet.Nom_estado = "Buen Estado"
            pBePedidoDet.IsNew = True
            pBePedidoDet.Fec_agr = Now
            pBePedidoDet.User_agr = BeConfigEnc.IdUsuario
            pBePedidoDet.RoadDes = 0
            pBePedidoDet.RoadDesMon = 0
            pBePedidoDet.RoadPrecioDoc = 0
            pBePedidoDet.RoadTotal = 0
            pBePedidoDet.RoadVAL1 = 0
            pBePedidoDet.RoadVAL2 = 0
            pBePedidoDet.Codigo_Producto = PDet.No
            pBePedidoDet.Nombre_producto = clsPublic.Quitar_Caracteres_No_Permitidos(PDet.Description)
            pBePedidoDet.Nom_presentacion = ""
            pBePedidoDet.Nom_unid_med = PDet.Unit_of_Measure_Code
            pBePedidoDet.Nom_estado = "Buen Estado"
            pBeStockRes.IdStockRes = 0
            pBeStockRes.IdTransaccion = pIdPedidoEnc
            pBeStockRes.IdPedidoDet = pBePedidoDet.IdPedidoDet
            pBeStockRes.Indicador = "PED"
            pBeStockRes.añada = 0
            pBeStockRes.Cantidad = PDet.Quantity
            pBeStockRes.Estado = "PPC"
            pBePedidoDet.Ndias = vDiasVencimientoCliente
            pBeStockRes.User_agr = BeConfigEnc.IdUsuario
            pBeStockRes.Fec_agr = Now
            pBeStockRes.User_mod = BeConfigEnc.IdUsuario
            pBeStockRes.Fec_mod = Now
            pBeStockRes.Host = "Interface"
            pBeStockRes.IdPresentacion = 0 'De momemento.
            pBeStockRes.IdProductoEstado = 1 'Por defecto.
            pBeStockRes.IdPedido = pIdPedidoEnc
            pBeStockRes.IdPedidoDet = pBePedidoDet.IdPedidoDet
            pBeStockRes.IdProductoBodega = BeProducto.IdProductoBodega
            pBeStockRes.IdPropietarioBodega = clsLnPropietarios.Get_IdPropietarioBodega_By_IdBodega_And_IdPropietario(BeConfigEnc.Idbodega,
                                                                                                                      BeConfigEnc.IdPropietario,
                                                                                                                      lConnectionInterface,
                                                                                                                      lTransInterface)
            pBeStockRes.IdUnidadMedida = BeProducto.UnidadMedida.IdUnidadMedida
            pBeStockRes.Atributo_Variante_1 = pBePedidoDet.Atributo_Variante_1

            Dim BePresentacion As New clsBeProducto_Presentacion

            If pBePedidoDet.Atributo_Variante_1 <> "" Then

                BePresentacion = clsLnProducto_presentacion.Existe_By_IdProducto_And_NombrePresentacion(pBePedidoDet.Producto.IdProducto,
                                                                                                        pBePedidoDet.Atributo_Variante_1,
                                                                                                        lConnectionInterface,
                                                                                                        lTransInterface)

                If Not BePresentacion Is Nothing Then
                    pBeStockRes.IdPresentacion = BePresentacion.IdPresentacion
                Else
                    pBeStockRes.IdPresentacion = -1 'No se encontró la presentación solicitada
                End If

            End If

            Try

                If clsLnTrans_pe_det.Reservar_Stock_Por_Linea_Interface(vDiasVencimientoCliente,
                                                                        PDet,
                                                                        pBePedidoDet,
                                                                        pBeStockRes,
                                                                        "Interface",
                                                                        BeConfigEnc,
                                                                        BeConfigEnc.IdPropietario,
                                                                        lConnectionInterface,
                                                                        lTransInterface) Then
                    Inserta_Linea_Detalle_Pedido = True
                End If

            Catch ex As Exception

                'clsLnTrans_pe_det.Eliminar(pBePedidoDet,CnnInterface,lTransInterface)

                clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error en 
                                                            Reservar_Stock_Por_Linea 
                                                            para el pedido: {0} 
                                                            línea: {1} 
                                                            Código_Producto: {3}
                                                            U.M.: {4}
                                                            V.C.: {5}
                                                            Descripción del error: {2} ", PDet.NoEnc,
                                                                         PDet.Line_No,
                                                                         ex.Message,
                                                                         PDet.Item_No,
                                                                         PDet.Unit_of_Measure_Code,
                                                                         PDet.Variant_Code),
                                                        PDet.No,
                                                        BeNavEjecucionEnc.IdEjecucionEnc,
                                                        BeConfigDet.Idnavconfigdet, CnnLog)

                lblprg.AppendText(String.Format("Error en
                                                Reservar_Stock_Por_Linea 
                                                para el pedido: {0} 
                                                línea: {1} 
                                                Código_Producto: {4}
                                                U.M.: {5}
                                                V.C.: {6}
                                                Descripción del error: {2}{3} ",
                                                PDet.NoEnc,
                                                PDet.Line_No,
                                                ex.Message,
                                                vbNewLine,
                                                PDet.Item_No,
                                                PDet.Unit_of_Measure_Code,
                                                PDet.Variant_Code))
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

            End Try

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Function Importar_Pedidos_Transferencia_Envio_Desde_Tabla_Intermedia_A_Tabla_TOMIMS(ByRef lblprg As RichTextBox,
                                                                                               ByRef prg As System.Windows.Forms.ProgressBar,
                                                                                               Optional ByVal ForzarEjecucion As Boolean = False,
                                                                                               Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False) As Boolean

        Importar_Pedidos_Transferencia_Envio_Desde_Tabla_Intermedia_A_Tabla_TOMIMS = False

        Dim lConectionInterface As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransInterface As SqlTransaction = Nothing

        Try

            If Not ForzarEjecucion Then

                If Not Ejecutar_Interfaz("Pedido traslado") Then

                    Actualizar_Progreso(lblprg, "La configuración de la interface indica que no se debe ejecutar en este momento. ")
                    Exit Function

                End If

            End If

            CnnLog.Open()

            BeNavEjecucionEnc.IdEjecucionEnc = 0 '0'0' 0' clsLnI_nav_ejecucion_res.Max_IdEjecucionRes(CnnLog) + 1
            BeNavEjecucionEnc.IdNavConfigEnc = BD.Instancia.IdConfiguracionInterface
            BeNavEjecucionEnc.Fecha = Now

            clsLnI_nav_ejecucion_enc.Insertar_From_Interface(BeNavEjecucionEnc, CnnLog)

            BeNavEjecucionRes.IdEjecucionRes = 0 '0' clsLnI_nav_ejecucion_res.Max_IdEjecucionRes(CnnLog) + 1
            BeNavEjecucionRes.IdEjecucionEnc = BeNavEjecucionEnc.IdEjecucionEnc
            BeNavEjecucionRes.IdNavConfigDet = BeConfigDet.Idnavconfigdet
            BeNavEjecucionRes.Registros_ws = 0
            BeNavEjecucionRes.Registros_ti = 0
            BeNavEjecucionRes.Registros_WMS = 0
            BeNavEjecucionRes.Exitosa = False

            clsLnI_nav_ejecucion_res.Insertar(BeNavEjecucionRes, CnnLog)

            BeNavEjecRes = BeNavEjecucionRes

            lConectionInterface.Open() : lTransInterface = lConectionInterface.BeginTransaction(IsolationLevel.ReadUncommitted)

            'Mira aqui pone el default, esto esta mal
            Actualizar_Progreso(lblprg, "Consultando WebService de pedido de traslado en: " & My.MySettings.Default.DynamicsNavInterface_WsPedidoTransferencia_Pedidos_Transferencia_Service)

            If Not Pregunta_Si_LLena_Intermedia Then

                If Not Importar_Pedidos_Transferencia_Envio_Desde_WSNav_A_TablaIntermedia(lblprg, prg, CnnLog) Then
                    Exit Function
                End If

            Else

                If XtraMessageBox.Show("¿Llenar tabla intermedia desde WS?", "Interface", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If Not Importar_Pedidos_Transferencia_Envio_Desde_WSNav_A_TablaIntermedia(lblprg, prg, CnnLog) Then
                        Exit Function
                    End If
                End If

            End If

            Dim lPedidoTrasladoEnc As New List(Of clsBeI_nav_ped_traslado_enc)

            Actualizar_Progreso(lblprg, "Consultando pedidos de traslado en tabla intermedia.")

            '#CKFK20230817 
            lPedidoTrasladoEnc = clsLnI_nav_ped_traslado_enc.GetAll_Pedidos_Transferencia(lConectionInterface,
                                                                                          lTransInterface)

            Actualizar_Progreso(lblprg, String.Format("Pedidos de traslado en tabla intermedia: {0}", lPedidoTrasladoEnc.Count))

            If lPedidoTrasladoEnc.Count > 0 Then

                Dim pBePedidoEnc As clsBeTrans_pe_enc = Nothing
                Dim TrasladoExistente As clsBeTrans_pe_enc = Nothing
                Dim BeCliente As New clsBeCliente
                Dim vContador As Integer = 0
                Dim vContadorLineasDet As Integer = 0
                Dim pClienteTiemposList As New List(Of clsBeCliente_tiempos)
                Dim BeProducto As New clsBeProducto
                Dim pBePedidoDet As New clsBeTrans_pe_det
                Dim vClienteTiempo As New clsBeCliente_tiempos
                Dim vDiasVencimientoCliente As Integer = 0
                Dim BeUnidadMedida As New clsBeUnidad_medida
                Dim vContador_Lineas_Detalle_Pedido_Insertadas As Integer = 0

                BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface,
                                                              lConectionInterface,
                                                              lTransInterface)

                If BeConfigEnc Is Nothing Then
                    If BD.Instancia.IdConfiguracionInterface = 0 Then
                        Throw New Exception("No éstá definida la configuración de interface para el Id: " & BD.Instancia.IdConfiguracionInterface & " verifique el el conn.ini que se especificó el identificador de configuración para la interface.")
                    Else
                        Throw New Exception("No éstá definida la configuración de interface para el Id: " & BD.Instancia.IdConfiguracionInterface & " verifique en la bd que existe el registro asociado al identificador de inteface: " & BD.Instancia.IdConfiguracionInterface)
                    End If
                End If

                prg.Maximum = lPedidoTrasladoEnc.Count

                prg.Value = 0

                Actualizar_Progreso(lblprg, String.Format("Trasladando documento a TOMWMS."))

                VContadorBitacoraTomims = 0

                For Each navPedidoTrasladoEnc As clsBeI_nav_ped_traslado_enc In lPedidoTrasladoEnc

                    If navPedidoTrasladoEnc.Status > 0 Then

                        Actualizar_Progreso(lblprg, String.Format("Procesando P.T.: {0} ", navPedidoTrasladoEnc.No, vbNewLine))

                        If navPedidoTrasladoEnc.Lineas_Detalle.Count > 0 Then

                            If navPedidoTrasladoEnc.No = "OP-00040247" Then
                                MsgBox("Hola!")
                                Debug.Print("Espera")
                            End If

                            pBePedidoEnc = New clsBeTrans_pe_enc() With {.Referencia = navPedidoTrasladoEnc.No,
                                                                         .IdTipoPedido = navPedidoTrasladoEnc.Document_Type}

                            TrasladoExistente = clsLnTrans_pe_enc.Get_Single_By_Referencia(pBePedidoEnc,
                                                                                           lConectionInterface,
                                                                                           lTransInterface)

                            vContadorLineasDet = 0

                            BeCliente = clsLnCliente.Get_Single_By_Codigo(navPedidoTrasladoEnc.Transfer_to_Code,
                                                                          lConectionInterface,
                                                                          lTransInterface)

                            If BeCliente Is Nothing Then
                                Throw New Exception(String.Format("{0} No existe el cliente {1} en maestro para pedido de tralsado ", MethodBase.GetCurrentMethod.Name(), navPedidoTrasladoEnc.Transfer_to_Code))
                            End If

                            If Not TrasladoExistente Is Nothing Then
                                pBePedidoEnc.Activo = True
                            Else

                                '#EJC20171107_REF13_0506AM: El MaxId del IdPedidoEnc se genera dentro del insert                            
                                pBePedidoEnc.Fecha_Pedido = navPedidoTrasladoEnc.Posting_Date
                                pBePedidoEnc.Referencia = navPedidoTrasladoEnc.No
                                If BeConfigEnc Is Nothing Then
                                    BeConfigEnc = New clsBeI_nav_config_enc
                                    BeConfigEnc.Idbodega = 1
                                End If
                                pBePedidoEnc.IdBodega = BeConfigEnc.Idbodega
                                pBePedidoEnc.Cliente = New clsBeCliente
                                pBePedidoEnc.Cliente.IdCliente = BeCliente.IdCliente
                                pBePedidoEnc.IdCliente = BeCliente.IdCliente
                                pBePedidoEnc.IdMuelle = 0
                                pBePedidoEnc.PropietarioBodega = New clsBePropietario_bodega
                                pBePedidoEnc.PropietarioBodega.IdPropietarioBodega = BeConfigEnc.IdPropietario
                                pBePedidoEnc.IdPropietarioBodega = BeConfigEnc.IdPropietario
                                pBePedidoEnc.TipoPedido = New clsBeTrans_pe_tipo
                                pBePedidoEnc.TipoPedido.IdTipoPedido = 3
                                pBePedidoEnc.Fecha_Pedido = navPedidoTrasladoEnc.Posting_Date
                                pBePedidoEnc.Hora_ini = Now
                                pBePedidoEnc.Hora_fin = Now.AddHours(1)
                                pBePedidoEnc.HoraEntregaDesde = Now
                                pBePedidoEnc.HoraEntregaHasta = Now.AddHours(1)
                                pBePedidoEnc.Ubicacion = 1
                                pBePedidoEnc.Estado = "Nuevo"
                                pBePedidoEnc.No_despacho = 0
                                pBePedidoEnc.Activo = True
                                pBePedidoEnc.User_agr = BeConfigEnc.IdUsuario
                                pBePedidoEnc.Fec_agr = Now
                                pBePedidoEnc.User_mod = BeConfigEnc.IdUsuario
                                pBePedidoEnc.Fec_mod = Now
                                '#EJC20171107_REF14_0507AM: Se sobreescribe No_documento en InsertaEncabezado por consecutivo de sistema
                                'pBePedidoEnc.No_documento = navPedidoTrasladoEnc.No
                                pBePedidoEnc.Local = True
                                pBePedidoEnc.Pallet_primero = True
                                pBePedidoEnc.Dias_cliente = 0
                                pBePedidoEnc.Anulado = False
                                pBePedidoEnc.IdPickingEnc = 0
                                pBePedidoEnc.RoadKilometraje = 0
                                pBePedidoEnc.RoadFechaEntr = navPedidoTrasladoEnc.Shipment_Date
                                pBePedidoEnc.RoadDirEntrega = ""
                                pBePedidoEnc.RoadTotal = 0
                                pBePedidoEnc.RoadDesMonto = 0
                                pBePedidoEnc.RoadImpMonto = 0
                                pBePedidoEnc.RoadPeso = 0
                                pBePedidoEnc.RoadBandera = 0
                                pBePedidoEnc.RoadStatCom = ""
                                pBePedidoEnc.RoadCalcoBJ = 0
                                pBePedidoEnc.RoadImpres = 0
                                pBePedidoEnc.RoadADD1 = ""
                                pBePedidoEnc.RoadADD2 = ""
                                pBePedidoEnc.RoadADD3 = ""
                                pBePedidoEnc.RoadStatProc = 0
                                pBePedidoEnc.RoadRechazado = 0
                                pBePedidoEnc.RoadRazon_Rechazado = 0
                                pBePedidoEnc.RoadInformado = 0
                                pBePedidoEnc.RoadSucursal = ""
                                pBePedidoEnc.RoadIdDespacho = 0
                                pBePedidoEnc.RoadIdFacturacion = 0
                                pBePedidoEnc.RoadIdRuta = 0
                                pBePedidoEnc.RoadIdVendedor = 0
                                pBePedidoEnc.RoadIdRutaDespacho = 0
                                pBePedidoEnc.RoadIdVendedorDespacho = 0
                                pBePedidoEnc.Enviado_A_ERP = False

                                clsLnTrans_pe_enc.Inserta_Encabezado(pBePedidoEnc, lConectionInterface, lTransInterface)

                                pClienteTiemposList = clsLnCliente_tiempos.Get_All_Tiempos_By_IdCliente(pBePedidoEnc.IdCliente, lConectionInterface, lTransInterface).ToList()

                                For Each PDet In navPedidoTrasladoEnc.Lineas_Detalle

                                    BeProducto.Codigo = PDet.Item_No

                                    BeProducto = clsLnProducto.Get_BeProducto_By_Codigo(BeProducto.Codigo,
                                                                                       BeConfigEnc.Idbodega,
                                                                                       lConectionInterface,
                                                                                       lTransInterface)

                                    BeUnidadMedida = clsLnUnidad_medida.Get_Unidad_Medida_By_Codigo(PDet.Unit_of_Measure_Code,
                                                                                                    lConectionInterface,
                                                                                                    lTransInterface)
                                    If Not vClienteTiempo Is Nothing Then
                                        vDiasVencimientoCliente = vClienteTiempo.Dias_Local
                                    End If

                                    'Insertar detalle de pedido y reservar existencias.
                                    Debug.Print(PDet.No)

                                    vClienteTiempo = pClienteTiemposList.Find(Function(x) _
                                    x.IdClasificacion = BeProducto.Clasificacion.IdClasificacion _
                                    And x.IdFamilia = BeProducto.Familia.IdFamilia)

                                    If TrasladoExistente Is Nothing Then

                                        Try

                                            If Inserta_Linea_Detalle_Pedido(pBePedidoEnc.IdPedidoEnc,
                                                                            PDet,
                                                                            BeProducto,
                                                                            vDiasVencimientoCliente,
                                                                            BeUnidadMedida,
                                                                            lblprg,
                                                                            lConectionInterface,
                                                                            CnnLog,
                                                                            lTransInterface) Then

                                                vContador_Lineas_Detalle_Pedido_Insertadas += 1

                                            End If

                                        Catch ex As Exception
                                            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
                                        End Try

                                    Else 'es un pedido existente.

                                        'Si la línea de detalle no existe
                                        If Not clsLnTrans_pe_det.Existe(TrasladoExistente.IdPedidoEnc, PDet.Line_No,
                                                                        pBePedidoDet,
                                                                        PDet.No,
                                                                        lConectionInterface,
                                                                        lTransInterface) Then

                                            Try

                                                If Inserta_Linea_Detalle_Pedido(pBePedidoEnc.IdPedidoEnc,
                                                                                PDet,
                                                                                BeProducto,
                                                                                vDiasVencimientoCliente,
                                                                                BeUnidadMedida,
                                                                                lblprg,
                                                                                lConectionInterface,
                                                                                CnnLog,
                                                                                lTransInterface) Then

                                                    vContador_Lineas_Detalle_Pedido_Insertadas += 1

                                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Línea nueva: {0} agregada a pedido existente: {1} ", PDet.Line_No, PDet.NoEnc),
                                                                                               PDet.No,
                                                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                               BeConfigDet.Idnavconfigdet,
                                                                                               CnnLog)

                                                    lblprg.AppendText(String.Format("Línea nueva: {0} agregada a pedido existente: {1} ", PDet.Line_No, PDet.NoEnc))
                                                    lblprg.AppendText(vbNewLine)
                                                    lblprg.SelectionStart = lblprg.TextLength
                                                    lblprg.ScrollToCaret()

                                                End If

                                            Catch ex As Exception
                                                Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
                                            End Try

                                        Else

                                            If pBePedidoDet.Cantidad <> PDet.Quantity Then

                                                clsLnI_nav_ejecucion_det_error.Inserta_Log(
                                                    String.Format("El pedido: {0} existe,
                                                        la línea de detalle: {1} existe, 
                                                        cantidad_origen <> cantidad_destino
                                                        no se puede actualizar (aún)", PDet.NoEnc, PDet.Line_No),
                                                        PDet.No,
                                                        BeNavEjecucionEnc.IdEjecucionEnc,
                                                        BeConfigDet.Idnavconfigdet, CnnLog)

                                                lblprg.AppendText(String.Format("El pedido: {0} exite,
                                                        la línea de detalle: {1} existe, 
                                                        cantidad_origen <> cantidad_destino
                                                        no se puede actualizar (aún)", PDet.NoEnc, PDet.Line_No))
                                                lblprg.AppendText(vbNewLine)
                                                lblprg.SelectionStart = lblprg.TextLength
                                                lblprg.ScrollToCaret()

                                            Else

                                                lblprg.AppendText(String.Format("El pedido: {0} existe,
                                                        la línea de detalle: {1} existe, 
                                                        cantidad_origen = cantidad_destino
                                                        no se actualizará", PDet.NoEnc, PDet.Line_No))
                                                lblprg.AppendText(vbNewLine)
                                                lblprg.SelectionStart = lblprg.TextLength
                                                lblprg.ScrollToCaret()

                                            End If

                                        End If

                                    End If 'fin TrasladoExistente

                                Next

                                Try

                                    '#EJC20180712: No se insertó ninguna línea de detalle del pedido
                                    'Eliminar el encabezado.
                                    If vContador_Lineas_Detalle_Pedido_Insertadas = 0 Then

                                        clsLnTrans_pe_enc.Eliminar_Encabezado_Pedido(pBePedidoEnc.IdPedidoEnc,
                                                                                     lConectionInterface,
                                                                                     lTransInterface)
                                        lblprg.AppendText(String.Format("El pedido {0} de traslado no tiene líneas de detalle válidas para el WMS y se eliminará la cabecera: {1}", navPedidoTrasladoEnc.No, vbNewLine))
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.Refresh()
                                        lblprg.SelectionStart = lblprg.TextLength
                                        lblprg.ScrollToCaret()

                                    End If

                                Catch ex As Exception
                                    lblprg.AppendText(String.Format("Error al eliminar cabecera de pedido de transferencia sin detalle : {0} {1}", ex.Message, vbNewLine))
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                End Try

                            End If

                        End If

                    Else

                        lblprg.AppendText(String.Format("PT Inactivo {0} ", navPedidoTrasladoEnc.No, vbNewLine))
                        lblprg.AppendText(vbNewLine)
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                    End If

                    vContador += 1

                    prg.Value = vContador

                Next

            End If

            lTransInterface.Commit()

            '#EJC20171107_REF04_0250AM: Desplegarcantidad de registros de pedidos de compra procesados
            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("********** FIN DE INSERCIÓN EN TOMWMS ********** ")
            lblprg.AppendText(vbNewLine)
            lblprg.AppendText(String.Format("Pedidos de traslado procesados correctamente: {0}", VContadorBitacoraTomims))
            lblprg.AppendText(vbNewLine)
            Dim difSegundos As Double = DateDiff(DateInterval.Second, BeNavEjecucionEnc.Fecha, Now)
            lblprg.AppendText(String.Format("Tiempo transcurrido: {0} segundo(s)", difSegundos))
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            BeNavEjecucionRes.Registros_ti = VContadorBitacoraIntermedia
            BeNavEjecucionRes.Registros_WMS = VContadorBitacoraTomims

            If VContadorBitacoraIntermedia = VContadorBitacoraTomims Then
                BeNavEjecucionRes.Exitosa = True
            Else
                BeNavEjecucionRes.Exitosa = False
            End If

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, CnnLog)

        Catch ex As Exception

            Try
                If Not lTransInterface Is Nothing Then lTransInterface.Rollback()
            Catch ex1 As Exception
                '#EJC20171105_1259AM_REF01: Agregar excepción a log.
                clsLnI_nav_ejecucion_det_error.Inserta_Log(ex1.Message,
                                            "",
                                            BeNavEjecucionEnc.IdEjecucionEnc,
                                            BeConfigDet.Idnavconfigdet, CnnLog)
            End Try

            '#EJC20171105_1259AM_REF01: Agregar excepción a log.
            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                              "",
                                              BeNavEjecucionEnc.IdEjecucionEnc,
                                              BeConfigDet.Idnavconfigdet, CnnLog)

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText(String.Format("Error al insertar pedido de traslado a tabla DE TOMWMS: {0} {1}", ex.Message, vbNewLine))
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            Throw ex

        Finally
            If lConectionInterface.State = ConnectionState.Open Then lConectionInterface.Close()
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
            prg.Value = 0
        End Try

    End Function

    Public Function Get_Pedidos_Transferencia_Envio_FromWS(ByRef lConnection As SqlConnection,
                                                           ByRef lTransaction As SqlTransaction,
                                                           Optional ByVal AplicarFiltros As Boolean = True) As List(Of Pedidos_Transferencia)

        Try

            Dim lPedidosTraslado As New List(Of Pedidos_Transferencia)

            Dim lFiltros As New List(Of clsBeI_nav_ent_filtros)
            lFiltros = clsLnI_nav_ent_filtros.Get_All_By_IdNavEnt(clsLnI_nav_ent_filtros.pEntidadesSycn.Pedido_Traslado,
                                                                  lConnection,
                                                                  lTransaction)

            Dim StartDate As String = "12142022.."
            Dim vCriteria As String = ""
            Dim vContador As Integer = 0

            For Each FiltroCategoria In lFiltros

                If FiltroCategoria.Tipo_Filtro = "" OrElse FiltroCategoria.Tipo_Filtro = "BODEGA" Then

                    If vContador = 0 Then
                        vCriteria = FiltroCategoria.Valor
                    Else
                        vCriteria += "|" & FiltroCategoria.Valor
                    End If

                ElseIf FiltroCategoria.Tipo_Filtro = "FECHA_INICIO" Then
                    StartDate = FiltroCategoria.Valor
                End If

                vContador += 1

            Next

            If AplicarFiltros Then

                '#EJC20180426: Cambio transfer_to_code.
                Dim vFiltros As Pedidos_Transferencia_Filter()
                Dim vFiltrosBodegasDestino As New Pedidos_Transferencia_Filter With
                      {.Field = Pedidos_Transferencia_Fields.Transfer_from_Code,
                      .Criteria = vCriteria}

                Dim vFiltroFechaDesde As New Pedidos_Transferencia_Filter With
                      {.Field = Pedidos_Transferencia_Fields.Posting_Date,
                      .Criteria = StartDate}

                Dim vFiltroEstatus As New Pedidos_Transferencia_Filter With
                      {.Field = Pedidos_Transferencia_Fields.Status,
                      .Criteria = "Released"}

                'Importar cantidad enviada y si cantidad enviada > 0 no recibir
                vFiltros = New Pedidos_Transferencia_Filter() {vFiltrosBodegasDestino, vFiltroEstatus, vFiltroFechaDesde}

                'wsPedidoTransferenciaService.Url = BD.Instancia.URLPedidosTransferencia
                wsPedidoTransferenciaService.Url = My.MySettings.Default.DynamicsNavInterface_WsPedidoTransferencia_Pedidos_Transferencia_Service

                '#CKFK20230817 Obtener los pedidos de transferencia de envío
                fichaPedidosTraslado = wsPedidoTransferenciaService.ReadMultiple(vFiltros, Nothing, 1000)

                For Each PC As Pedidos_Transferencia In fichaPedidosTraslado
                    lPedidosTraslado.Add(PC)
                Next

            Else

                fichaPedidosTraslado = wsPedidoTransferenciaService.ReadMultiple(Nothing, Nothing, 1000)

                For Each PC As Pedidos_Transferencia In fichaPedidosTraslado
                    lPedidosTraslado.Add(PC)
                Next

            End If

            Return lPedidosTraslado

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Function Get_Pedido_Transferencia_Envio_FromWS(ByRef lConnection As SqlConnection,
                                                          ByRef lTransaction As SqlTransaction,
                                                          ByVal lCodigoTransferencia As String,
                                                          Optional ByVal AplicarFiltros As Boolean = True
                                                         ) As Pedidos_Transferencia

        Try

            Dim lPedidoTraslado As New Pedidos_Transferencia

            Dim lFiltros As New List(Of clsBeI_nav_ent_filtros)
            lFiltros = clsLnI_nav_ent_filtros.Get_All_By_IdNavEnt(clsLnI_nav_ent_filtros.pEntidadesSycn.Pedido_Traslado,
                                                                  lConnection,
                                                                  lTransaction)

            Dim vCriteria As String = ""
            Dim vContador As Integer = 0

            For Each FiltroCategoria In lFiltros

                If vContador = 0 Then
                    vCriteria = FiltroCategoria.Valor
                Else
                    vCriteria += "|" & FiltroCategoria.Valor
                End If

                vContador += 1

            Next

            If AplicarFiltros Then

                '#EJC20180426: Cambio transfer_to_code.
                Dim vFiltros As Pedidos_Transferencia_Filter()
                Dim vFiltrosBodegasDestino As New Pedidos_Transferencia_Filter With
                      {.Field = Pedidos_Transferencia_Fields.Transfer_from_Code,
                      .Criteria = vCriteria}

                Dim vFiltroEstatus As New Pedidos_Transferencia_Filter With
                      {.Field = Pedidos_Transferencia_Fields.Status,
                      .Criteria = "Released"}

                'Importar cantidad enviada y si cantidad enviada > 0 no recibir
                vFiltros = New Pedidos_Transferencia_Filter() {vFiltrosBodegasDestino, vFiltroEstatus}

                wsPedidoTransferenciaService.Url = BD.Instancia.URLPedidosTransferencia

                lPedidoTraslado = wsPedidoTransferenciaService.Read(lCodigoTransferencia)

            Else

                lPedidoTraslado = wsPedidoTransferenciaService.Read(lCodigoTransferencia)

            End If

            Return lPedidoTraslado

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Function Get_Pedidos_Transferencia_Recepcion_FromWS(ByRef lConnection As SqlConnection,
                                                               ByRef lTransaction As SqlTransaction,
                                                               Optional ByVal AplicarFiltros As Boolean = True) As List(Of Pedidos_Transferencia)

        Try

            Dim lPedidosTraslado As New List(Of Pedidos_Transferencia)
            Dim lFiltros As New List(Of clsBeI_nav_ent_filtros)
            Dim StartDate As String = "01092021D"

            lFiltros = clsLnI_nav_ent_filtros.Get_All_By_IdNavEnt(clsLnI_nav_ent_filtros.pEntidadesSycn.Pedido_Traslado,
                                                                  lConnection,
                                                                  lTransaction)

            Dim vCriteria As String = ""
            Dim vContador As Integer = 0

            For Each FiltroCategoria In lFiltros

                If FiltroCategoria.Tipo_Filtro = "" OrElse FiltroCategoria.Tipo_Filtro = "BODEGA" Then

                    If vContador = 0 Then
                        vCriteria = FiltroCategoria.Valor
                    Else
                        vCriteria += "|" & FiltroCategoria.Valor
                    End If

                ElseIf FiltroCategoria.Tipo_Filtro = "FECHA_INICIO" Then
                    StartDate = FiltroCategoria.Valor
                End If

                vContador += 1

            Next

            If vCriteria <> "" AndAlso pBodega <> "" Then
                If pBodega <> vCriteria AndAlso Not vCriteria.Contains(pBodega) Then
                    Throw New Exception(String.Format("La Bodega del filtro: {0} no se corresponde con la Bodega de la interface: {1}", vCriteria, pBodega))
                End If
            End If

            If AplicarFiltros Then

                '#EJC20180426: Cambmio transfer_to_code.
                Dim vFiltros As Pedidos_Transferencia_Filter()

                Dim vFiltrosBodegasDestino As New Pedidos_Transferencia_Filter With
                      {.Field = Pedidos_Transferencia_Fields.Transfer_to_Code,
                      .Criteria = vCriteria}

                Dim vFiltroEstatus As New Pedidos_Transferencia_Filter With
                      {.Field = Pedidos_Transferencia_Fields.Status,
                      .Criteria = "Released"}

                Dim vFiltro3 As New Pedidos_Transferencia_Filter() With
                    {.Field = Pedidos_Transferencia_Fields.Posting_Date,
                    .Criteria = StartDate} '"01/03/2021.."

                'Dim vFiltroTieneEnvio As New Pedidos_Transferencia_Filter With
                '      {.Field = Pedidos_Transferencia_Fields.Last_Shipment_No,
                '      .Criteria = " <> 0 "}

                'Importar cantidad enviada y si cantidad enviada > 0 no recibir
                vFiltros = New Pedidos_Transferencia_Filter() {vFiltrosBodegasDestino, vFiltroEstatus, vFiltro3}

                '#CKFK20230817 Llamado a WS para obtener los pedido de transferencia de recepción
                fichaPedidosTraslado = wsPedidoTransferenciaService.ReadMultiple(vFiltros, Nothing, 0)

                For Each PC As Pedidos_Transferencia In fichaPedidosTraslado '.Where(Function(x) x.No = "PT-206133").ToList
                    lPedidosTraslado.Add(PC)
                    If Not PC.Last_Shipment_No Is Nothing Then
                        lPedidosTraslado.Add(PC)
                    End If
                Next

            Else

                Dim vFiltroEstatus As New Pedidos_Transferencia_Filter With
                      {.Field = Pedidos_Transferencia_Fields.Status,
                      .Criteria = "Released"}

                fichaPedidosTraslado = wsPedidoTransferenciaService.ReadMultiple(Nothing, Nothing, 1000)

                For Each PC As Pedidos_Transferencia In fichaPedidosTraslado
                    If Not PC.Last_Shipment_No Is Nothing Then
                        lPedidosTraslado.Add(PC)
                    End If
                Next

            End If

            Return lPedidosTraslado

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Sub Enviar_Transacciones_De_Salida(ByRef lblprg As RichTextBox,
                                              ByRef prg As Windows.Forms.ProgressBar,
                                              ByVal pIdBodega As Integer)

        Dim lTransaccionesSalida As New List(Of clsBeI_nav_transacciones_out)
        Dim lTransaccionesSalidaReproceso As New List(Of clsBeI_nav_transacciones_out)
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        '#EJC20180614: Tratar de registrar pedidos de transferencia que no se registraron en NAV.
        Dim TransferNav As New Pedidos_Transferencia
        Dim Sl As New clsSyncLotes()
        Dim lLotes As New List(Of Pagina_lotes)
        Dim LoteEnviado As New Pagina_lotes
        Dim vContadorReproceso As Integer = 0
        Dim lTransPtPendienteRegistroEnNav As New List(Of clsBeTrans_pe_enc)
        Dim BeBodegaNAV As Ficha_Bodegas
        Dim vCodeUnitNavError As Boolean = False
        Dim lDocumentosHojaDeTrabajo As New List(Of String)
        Dim vRespuestaSetWarehouseDocuments As String = ""
        Dim CurrentWkshName As String = "GENERICO" '#EJC20210614: Según Ricardo, este valor se envía fijo.
        Dim TheWorkSheetNAV As New Crea_picking()
        Dim ThePickingNAV As New Picking
        Dim vNoPickingNAV As String = ""
        Dim vRespuestaRegisterPutAway As String = ""
        Dim CurrentSortingMethod As Integer = 1 '#EJC20210614: Según Ricardo, este valor se envía fijo.
        Dim vFechaVenceNav As String = ""
        Dim Bodega_Avanzada As Boolean = False '#CKFK 20211123 Agregué variable para determinar si una bodega es avanzada o no
        Dim procesoError As String = ""

        Try

            CnnLog.Open()

            lTransaccionesSalida = clsLnI_nav_transacciones_out.Get_Lotes_Salida_Pendientes_Envio_By_IdBodega(pIdBodega)

            Dim wsBodegaService As New Ficha_Bodegas_Service() With
                {
                .UseDefaultCredentials = UsarCredencialesPorDefecto,
                .Credentials = CredencialesConexion
                }

            wsBodegaService.Url = My.Settings.DynamicsNavInterface_WSFichaBodegas_Ficha_Bodegas_Service

            '#EJC20210426: CodeUnit de NAV para WMS, agregado por la bodega de PT.
            Dim wsCUWMS As New CUWMS.CUWMS() With {.UseDefaultCredentials = UsarCredencialesPorDefecto,
                                                   .Credentials = CredencialesConexion
                                                   }

            wsCUWMS.Url = My.MySettings.Default.NavSync_CUWMS_CUWMS

            '#EJC20210426: CodeUnit de NAV para WMS, agregado por la bodega de PT.
            Dim wsSrvPickingNAV As New Picking_Service() With {.UseDefaultCredentials = UsarCredencialesPorDefecto,
                                                               .Credentials = CredencialesConexion
                                                              }

            wsSrvPickingNAV.Url = My.MySettings.Default.NavSync_WSPicking_Picking_Service

            Dim srvHojaDeTrabajo As New Crea_picking_Service() With
                {
                .UseDefaultCredentials = UsarCredencialesPorDefecto,
                .Credentials = CredencialesConexion
                }

            srvHojaDeTrabajo.Url = My.Settings.NavSync_WSCreaPicking_Crea_picking_Service

            If Not lTransaccionesSalida Is Nothing AndAlso lTransaccionesSalida.Count > 0 Then

                Actualizar_Progreso(lblprg, String.Format(vbNewLine & "Registros: {0}", lTransaccionesSalida.Count))

                '#CKFK 20211121 Agregué el campo IdTipoDocumento
                Dim ListaPedidosTransf = (From i In lTransaccionesSalida
                                          Group i By Keys = New With {Key i.No_pedido, Key i.Idpedidoenc, Key i.Codigo_Bodega_Origen, i.IdTipoDocumento} Into Group
                                          Select New With {Key Keys.No_pedido, Key Keys.Idpedidoenc, Key Keys.Codigo_Bodega_Origen, Key Keys.IdTipoDocumento})
                Dim Enviado_A_Erp As Boolean = False '#CKFK 20180518 10:20 PM Agregué la validación de si el registro ya fue enviado o no a NAV

                Dim BeEmpresa As New clsBeEmpresa
                clsLnEmpresa.GetSingle_By_IdBodega(BeEmpresa, pIdBodega)

                BeConfigEnc = clsLnI_nav_config_enc.Get_Single_By_IdBodega_And_IdEmpresa(pIdBodega, BeEmpresa.IdEmpresa)

                For Each PT In ListaPedidosTransf

                    Enviado_A_Erp = clsLnTrans_pe_enc.Get_Estado_Enviado_A_ERP(PT.No_pedido)

                    If Not PT.Codigo_Bodega_Origen = "" Then
                        Bodega_Avanzada = Es_Bodega_Avanzada(PT.Codigo_Bodega_Origen)
                        BeBodegaNAV = wsBodegaService.Read(PT.Codigo_Bodega_Origen)
                    End If

                    If Not Enviado_A_Erp Then

                        If Not Bodega_Avanzada Then

                            Try

                                If Enviar_Lotes_Transf(PT.No_pedido,
                                                       PT.Idpedidoenc,
                                                       lTransaccionesSalida,
                                                       lblprg,
                                                       prg) Then

                                    If Enviar_Cantidades_Transf(PT.No_pedido,
                                                                PT.Idpedidoenc,
                                                                lTransaccionesSalida,
                                                                lblprg,
                                                                prg) Then

                                        wsRegistra_Transfer_Envio.RegistrarEnvTransfer(PT.No_pedido)

                                    End If

                                End If

                                Actualizar_Progreso(lblprg, String.Format(vbNewLine & "Transacciones de salida enviadas correctamente: {0}", lTransaccionesSalida.Count))

                                clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP(PT.Idpedidoenc,
                                                                                  True,
                                                                                  BeConfigEnc.IdUsuario)

                            Catch ex As Exception

                                Actualizar_Progreso(lblprg, String.Format("Error al registrar el pedido:{0} en el ERP. Error: {1}", PT.No_pedido, ex.Message))

                                clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al registrar el pedido:{0} en el ERP. Error: {1}", PT.No_pedido, ex.Message),
                                                                           PT.No_pedido,
                                                                           BeNavEjecucionEnc.IdEjecucionEnc,
                                                                           BeConfigDet.Idnavconfigdet,
                                                                           CnnLog)

                            End Try

                        Else

                            Try

                                '#CKFK 20211123 Registrar el Picking de los pedidos de transferencia o de venta
                                If PT.IdTipoDocumento = clsDataContractDI.tTipoDocumentoSalida.Transferencia_Interna_WMS Then

                                    Dim vURLPickingNAV As String = My.Settings.NavSync_WSPicking_Picking_Service

                                    Dim pkcService As New Picking_Service With
                                        {
                                            .UseDefaultCredentials = False,
                                            .Credentials = CredencialesConexion,
                                            .Url = vURLPickingNAV
                                        }

                                    '#CKFK 20211123 Esto me va a servir para registrar la transferencia
                                    Dim vUrlCodeUnit As String = My.Settings.NavSync_CUWMS_CUWMS

                                    Dim ws2 As New CUWMS.CUWMS() With
                                        {
                                            .UseDefaultCredentials = False,
                                            .Credentials = CredencialesConexion,
                                            .Url = vUrlCodeUnit
                                        }

                                    Dim vURLEnviosAlm As String
                                    vURLEnviosAlm = My.Settings.NavSync_WSEnvioAlm_Envio_alm_Service

                                    Dim wsEnviosAlm As New WSEnvioAlm.Envio_alm_Service With
                                        {
                                            .UseDefaultCredentials = False,
                                            .Credentials = CredencialesConexion,
                                            .Url = vURLEnviosAlm
                                        }


                                    Dim BePedidoEnc As New clsBeTrans_pe_enc
                                    Dim BePickingUbic As New List(Of clsBeTrans_picking_ubic)

                                    BePedidoEnc = clsLnTrans_pe_enc.GetSingle(PT.Idpedidoenc, False)

                                    '#CKFK 20211209 Cambie la forma de obtener los picking ubic
                                    BePickingUbic = clsLnTrans_picking_ubic.Get_Picking_Ubic_By_IdPicking(BePedidoEnc.IdPickingEnc)

                                    Dim vPicking As New Picking

                                    procesoError = Strings.Format("Lectura del Picking {0} ", BePedidoEnc.No_Picking_ERP)
                                    vPicking = pkcService.Read(BePedidoEnc.No_Picking_ERP)

                                    If vPicking IsNot Nothing Then

                                        Dim cant_modif As Boolean = False

                                        '#CKFK 20211125 Analizar con EJC cual es la forma correcta
                                        For Each PL In vPicking.WhseActivityLines

                                            'Validar si las cantidades son correctas
                                            For Each PKW In BePickingUbic

                                                If PKW.CodigoProducto = PL.Item_No AndAlso
                                                   PKW.Lote = PL.Lot_No AndAlso
                                                   PKW.Fecha_Vence = PL.Expiration_Date AndAlso
                                                   (PKW.ProductoUnidadMedida = PL.Unit_of_Measure_Code OrElse
                                                   PKW.ProductoPresentacion = PL.Unit_of_Measure_Code) Then

                                                    If PL.Qty_to_Handle <> PKW.Cantidad_despachada Then
                                                        PL.Qty_to_Handle = PKW.Cantidad_despachada
                                                        cant_modif = True
                                                    End If

                                                End If

                                            Next

                                        Next

                                        If cant_modif Then
                                            procesoError = Strings.Format("Update del Picking {0} ", BePedidoEnc.No_Picking_ERP)
                                            pkcService.Update(vPicking)
                                        End If

                                        procesoError = Strings.Format("RegisterPutAway del Picking {0} ", BePedidoEnc.No_Picking_ERP)
                                        ws2.RegisterPutAway(BePedidoEnc.No_Picking_ERP, vRespuestaRegisterPutAway)

                                        If Not vRespuestaRegisterPutAway.Contains("Successfully Created") Then
                                            Throw New Exception(procesoError & " " & vRespuestaRegisterPutAway)
                                        Else
                                            procesoError = Strings.Format("RegistraEnvioAlmace del Envío {0} ", BePedidoEnc.Referencia)

                                            '#CKFK 20211210 Se agregó esto para modificar la fecha del envío cuando sea menor al mes actual
                                            Dim vEnvioAlm As New WSEnvioAlm.Envio_alm
                                            vEnvioAlm = wsEnviosAlm.Read(BePedidoEnc.Referencia)

                                            If DateDiff(DateInterval.Month, vEnvioAlm.Posting_Date, Now) > 0 Then
                                                vEnvioAlm.Posting_Date = Now
                                                wsEnviosAlm.Update(vEnvioAlm)
                                            End If

                                            ws2.RegistraEnvioAlmace(BePedidoEnc.Referencia)
                                        End If

                                    End If

                                End If

                                Actualizar_Progreso(lblprg, String.Format(vbNewLine & "Transacciones de salida enviadas correctamente: {0}", lTransaccionesSalida.Count))

                                clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP(PT.Idpedidoenc, True, BeConfigEnc.IdUsuario)

                            Catch ex As Exception

                                Actualizar_Progreso(lblprg, String.Format(vbNewLine & "Error al registrar el pedido:{0} en el ERP. Error: {1} dentro del proceso {2}", PT.No_pedido, ex.Message, procesoError))

                                clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al registrar el pedido:{0} en el ERP. Error: {1} dentro del proceso {2}", PT.No_pedido, ex.Message, procesoError),
                                                                           PT.No_pedido,
                                                                           BeNavEjecucionEnc.IdEjecucionEnc,
                                                                           BeConfigDet.Idnavconfigdet,
                                                                           CnnLog)

                            End Try

                        End If

                    Else

                        If Not Bodega_Avanzada Then

                            Try

                                TransferNav = wsPedidoTransferenciaService.Read(PT.No_pedido)

                                If Not TransferNav Is Nothing Then

                                    lLotes = Sl.Get_Lista_Lotes(PT.No_pedido)

                                    '#EJC20180614: Si tiene registros enviados intento registrar, si no, no.
                                    lTransaccionesSalidaReproceso = clsLnI_nav_transacciones_out.Get_Lotes_Salida_Pendientes_Envio(PT.No_pedido)

                                    If lTransaccionesSalidaReproceso.Count > 0 Then

                                        If lLotes.Count > 0 Then

                                            vContadorReproceso = 0

                                            For Each LoteWMS In lTransaccionesSalidaReproceso

                                                LoteEnviado = lLotes.Find(Function(x) x.Item_No = LoteWMS.Codigo_producto _
                                                                          AndAlso x.Quantity_Base = LoteWMS.Cantidad _
                                                                          AndAlso x.Lot_No = LoteWMS.Lote)

                                                If LoteEnviado Is Nothing Then 'El lote no se ha enviado a NAV
                                                    LoteWMS.Enviado = False
                                                    vContadorReproceso += 1
                                                Else
                                                    'El lote ya fue enviado
                                                End If

                                            Next

                                        End If

                                        If vContadorReproceso > 0 Then
                                            If Enviar_Lotes_Transf(PT.No_pedido, PT.Idpedidoenc, lTransaccionesSalidaReproceso, lblprg, prg) Then
                                                If Enviar_Cantidades_Transf(PT.No_pedido, PT.Idpedidoenc, lTransaccionesSalidaReproceso, lblprg, prg) Then
                                                    'Igual registro al final ;)
                                                End If
                                            End If
                                        End If

                                        Try

                                            wsRegistra_Transfer_Envio.RegistrarEnvTransfer(PT.No_pedido)

                                            Actualizar_Progreso(lblprg, String.Format(vbNewLine & "Se registró el pedido:{0} correctamente en el ERP.", PT.No_pedido))

                                        Catch ex As Exception

                                            Actualizar_Progreso(lblprg, String.Format(vbNewLine & "Error al registrar el pedido:{0} en el ERP. Error: {1}", PT.No_pedido, ex.Message))

                                            clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al registrar el pedido:{0} en el ERP. Error: {1}", PT.No_pedido, ex.Message),
                                                                                        BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                        BeConfigDet.Idnavconfigdet,
                                                                                        PT.No_pedido,
                                                                                        CnnLog)

                                        End Try

                                        clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP(PT.Idpedidoenc, True, BeConfigEnc.IdUsuario)

                                    End If


                                Else

                                    If Not Enviado_A_Erp Then
                                        Actualizar_Progreso(lblprg, String.Format(vbNewLine & "Se registró el pedido:{0} correctamente en el ERP.", PT.No_pedido))
                                        '#EJC20180614: El pedido ya se recibió en la bodega destino.
                                        clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP(PT.Idpedidoenc, True, BeConfigEnc.IdUsuario)
                                    Else
                                        Actualizar_Progreso(lblprg, String.Format(vbNewLine & "El pedido:{0} ya fue enviado al ERP (Verifique si se completó y marque los registros en WMS para no ser enviados nuevamente)", PT.No_pedido))
                                    End If

                                End If

                            Catch ex As Exception

                                If ex.Message.Contains("There is nothing to post.") Then 'Pedido sin nada que registrar

                                    Actualizar_Progreso(lblprg, String.Format(vbNewLine & "Nada que registrar para pedido: {0} en NAV.", PT.No_pedido))

                                    clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP(PT.Idpedidoenc, True, BeConfigEnc.IdUsuario)

                                Else

                                    Actualizar_Progreso(lblprg, String.Format(vbNewLine & "Error al registrar el pedido:{0} en el ERP. Error: {1}", PT.No_pedido, ex.Message))

                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al registrar el pedido:{0} en el ERP. Error: {1}", PT.No_pedido, ex.Message),
                                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                                               BeConfigDet.Idnavconfigdet,
                                                                               PT.No_pedido,
                                                                               CnnLog)

                                End If

                            End Try

                        Else

                            Try
                                '#CKFK 20211123 Registrar el Picking de los pedidos de transferencia o de venta
                                If PT.IdTipoDocumento = clsDataContractDI.tTipoDocumentoSalida.Transferencia_Interna_WMS OrElse
                                        PT.IdTipoDocumento = clsDataContractDI.tTipoDocumentoSalida.Pedido_De_Cliente Then

                                    Dim vURLPickingNAV As String = My.Settings.NavSync_WSPicking_Picking_Service

                                    Dim pkcService As New Picking_Service With
                                            {
                                                .UseDefaultCredentials = False,
                                                .Credentials = CredencialesConexion,
                                                .Url = vURLPickingNAV
                                            }

                                    '#CKFK 20211123 Esto me va a servir para registrar la transferencia
                                    Dim vUrlCodeUnit As String = My.Settings.NavSync_CUWMS_CUWMS

                                    Dim ws2 As New CUWMS.CUWMS() With
                                            {
                                                .UseDefaultCredentials = False,
                                                .Credentials = CredencialesConexion,
                                                .Url = vUrlCodeUnit
                                            }
                                    Dim BePedidoEnc As New clsBeTrans_pe_enc
                                    BePedidoEnc = clsLnTrans_pe_enc.GetSingle(PT.Idpedidoenc, False)

                                    Dim vPicking As New Picking

                                    vPicking = pkcService.Read(BePedidoEnc.No_Picking_ERP)

                                    If vPicking IsNot Nothing Then
                                        ws2.RegisterPutAway(BePedidoEnc.No_Picking_ERP, vRespuestaRegisterPutAway)
                                    End If

                                End If

                                Actualizar_Progreso(lblprg, String.Format(vbNewLine & "Se registró el pedido:{0} correctamente en el ERP.", PT.No_pedido))

                                clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP(PT.Idpedidoenc, True, BeConfigEnc.IdUsuario)

                            Catch ex As Exception

                                If ex.Message.Contains("There is nothing to post.") Then 'Pedido sin nada que registrar

                                    Actualizar_Progreso(lblprg, String.Format(vbNewLine & "Nada que registrar para pedido: {0} en NAV.", PT.No_pedido))

                                    clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP(PT.Idpedidoenc, True, BeConfigEnc.IdUsuario)

                                Else

                                    Actualizar_Progreso(lblprg, String.Format(vbNewLine & "Error al registrar el pedido:{0} en el ERP. Error: {1}", PT.No_pedido, ex.Message))

                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al registrar el pedido:{0} en el ERP. Error: {1}", PT.No_pedido, ex.Message),
                                                                                BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                BeConfigDet.Idnavconfigdet,
                                                                                PT.No_pedido,
                                                                                CnnLog)

                                End If

                            End Try

                        End If

                    End If

                Next

            End If

            '#EJC202309131252: Registro de documentos pendientes interface BYB.
            lTransPtPendienteRegistroEnNav = clsLnTrans_pe_enc.Get_All_Pendiente_Registro_MI3()

            Actualizar_Progreso(lblprg, "Buscando documentos pendientes de registro.")

            If lTransPtPendienteRegistroEnNav.Count > 0 Then

                For Each PT In lTransPtPendienteRegistroEnNav

                    'Cerrada 
                    If PT.Estado = "Despachado" OrElse PT.Estado = "Verificado" Then

                        Actualizar_Progreso(lblprg, "Registrando documento: " & PT.Referencia)

                        If Not Bodega_Avanzada Then

                            Try

                                If PT.Referencia <> "" Then

                                    TransferNav = wsPedidoTransferenciaService.Read(PT.Referencia)

                                    If Not TransferNav Is Nothing Then

                                        lLotes = Sl.Get_Lista_Lotes(PT.Referencia)

                                        '#EJC20180614: Si tiene registros enviados intento registrar, si no, no.
                                        lTransaccionesSalida = clsLnI_nav_transacciones_out.Get_Lotes_Salida_Pendientes_Envio(PT.Referencia)

                                        If lTransaccionesSalida.Count > 0 Then

                                            If lLotes.Count > 0 Then

                                                vContadorReproceso = 0

                                                For Each LoteWMS In lTransaccionesSalida

                                                    LoteEnviado = lLotes.Find(Function(x) x.Item_No = LoteWMS.Codigo_producto _
                                                                              AndAlso x.Quantity_Base = LoteWMS.Cantidad _
                                                                              AndAlso x.Lot_No = LoteWMS.Lote)

                                                    If LoteEnviado Is Nothing Then 'El lote no se ha enviado a NAV
                                                        LoteWMS.Enviado = False
                                                        vContadorReproceso += 1
                                                    Else
                                                        'El lote ya fue enviado
                                                    End If

                                                Next

                                                If vContadorReproceso > 0 Then
                                                    If Enviar_Lotes_Transf(PT.Referencia, lTransaccionesSalida, lblprg, prg) Then
                                                        If Enviar_Cantidades_Transf(PT.Referencia, lTransaccionesSalida, lblprg, prg) Then
                                                            'Igual registro al final ;)
                                                        End If
                                                    End If
                                                End If

                                            Else
                                                '#CKFK20230903 Agregué el sino para que en caso de que no se hayan enviado lotes a NAV se envíen
                                                If Enviar_Lotes_Transf(PT.Referencia, lTransaccionesSalida, lblprg, prg) Then
                                                    If Enviar_Cantidades_Transf(PT.Referencia, lTransaccionesSalida, lblprg, prg) Then
                                                        'Igual registro al final ;)
                                                    End If
                                                End If
                                            End If

                                            wsRegistra_Transfer_Envio.RegistrarEnvTransfer(PT.Referencia)

                                            Actualizar_Progreso(lblprg, String.Format("Se registró el pedido:{0} correctamente en el ERP.", PT.Referencia))

                                            clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP(PT.IdPedidoEnc, True, BeConfigEnc.IdUsuario)

                                        End If

                                    Else

                                        Actualizar_Progreso(lblprg, String.Format("Se registró el pedido:{0} correctamente en el ERP.", PT.Referencia))
                                        '#EJC20180614: El pedido ya se recibió en la bodega destino.
                                        clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP(PT.IdPedidoEnc, True, BeConfigEnc.IdUsuario)

                                    End If

                                Else
                                    Throw New Exception("El pedido " & PT.IdPedidoEnc & " no tiene referencia de NAV, no se puede registrar")
                                End If

                            Catch ex As Exception

                                If ex.Message.CompareTo("There is nothing to post.") Then 'Pedido sin nada que registrar
                                    Actualizar_Progreso(lblprg, String.Format("Nada que registrar para pedido: {0} en NAV.", PT.Referencia))
                                Else

                                    Dim vMensaje As String = String.Format(vbTab & "#ERROR202309131249: al registrar el pedido:{0} en el ERP. Error: {1}", PT.Referencia, ex.Message)

                                    Actualizar_Progreso(lblprg, vMensaje)

                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(vMensaje.Trim,
                                                                               PT.Referencia,
                                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                                               BeConfigDet.Idnavconfigdet,
                                                                               CnnLog)

                                End If

                            End Try

                        Else

                            Try
                                '#CKFK 20211123 Registrar el Picking de los pedidos de transferencia o de venta
                                If PT.IdTipoPedido = clsDataContractDI.tTipoDocumentoSalida.Transferencia_Interna_WMS Then

                                    Dim vURLPickingNAV As String = My.Settings.NavSync_WSPicking_Picking_Service

                                    Dim pkcService As New Picking_Service With
                                                {
                                                    .UseDefaultCredentials = False,
                                                    .Credentials = CredencialesConexion,
                                                    .Url = vURLPickingNAV
                                                }

                                    '#CKFK 20211123 Esto me va a servir para registrar la transferencia
                                    Dim vUrlCodeUnit As String = My.Settings.NavSync_CUWMS_CUWMS

                                    Dim ws2 As New CUWMS.CUWMS() With
                                                {
                                                    .UseDefaultCredentials = False,
                                                    .Credentials = CredencialesConexion,
                                                    .Url = vUrlCodeUnit
                                                }

                                    Dim vPicking As New Picking

                                    vPicking = pkcService.Read(PT.No_Picking_ERP)

                                    If vPicking IsNot Nothing Then
                                        ws2.RegisterPutAway(PT.No_Picking_ERP, vRespuestaRegisterPutAway)
                                    End If

                                    Actualizar_Progreso(lblprg, String.Format("Se registró el pedido:{0} correctamente en el ERP.", PT.Referencia))

                                    clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP(PT.IdPedidoEnc, True, BeConfigEnc.IdUsuario)

                                End If

                            Catch ex As Exception

                                If ex.Message.Contains("There is nothing to post.") Then 'Pedido sin nada que registrar
                                    Actualizar_Progreso(lblprg, String.Format("Nada que registrar para pedido: {0} en NAV.", PT.Referencia))
                                Else

                                    Dim vMensaje As String = String.Format(vbTab & "#ERROR202309131249A: al registrar el pedido:{0} en el ERP. Error: {1}", PT.Referencia, ex.Message)

                                    Actualizar_Progreso(lblprg, vMensaje)

                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(vMensaje.Trim(),
                                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                                               BeConfigDet.Idnavconfigdet,
                                                                               PT.Referencia,
                                                                               CnnLog)
                                End If

                            End Try

                        End If

                    End If

                Next

            Else
                Actualizar_Progreso(lblprg, "No hay transacciones para enviar.")
            End If

            Actualizar_Progreso(lblprg, String.Format(vbNewLine & "Fin del proceso: {0}", Now))

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            prg.Value = 0
            prg.Visible = False
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
        End Try

    End Sub

    Public Sub Enviar_Transacciones_De_Salida_Anterior(ByRef lblprg As RichTextBox,
                                                       ByRef prg As Windows.Forms.ProgressBar)

        Dim lTransaccionesSalida As New List(Of clsBeI_nav_transacciones_out)
        Dim lTransaccionesSalidaReproceso As New List(Of clsBeI_nav_transacciones_out)
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim TransferNav As New Pedidos_Transferencia
        Dim Sl As New clsSyncLotes()
        Dim lLotes As New List(Of Pagina_lotes)
        Dim LoteEnviado As New Pagina_lotes
        Dim vContadorReproceso As Integer = 0
        Dim lTransPtPendienteRegistroEnNav As New List(Of clsBeTrans_pe_enc)
        Dim BeBodegaNAV As Ficha_Bodegas
        Dim vCodeUnitNavError As Boolean = False
        Dim lDocumentosHojaDeTrabajo As New List(Of String)
        Dim vRespuestaSetWarehouseDocuments As String = ""
        Dim CurrentWkshName As String = "GENERICO" '#EJC20210614: Según Ricardo, este valor se envía fijo.
        Dim TheWorkSheetNAV As New Crea_picking()
        Dim ThePickingNAV As New Picking
        Dim vNoPickingNAV As String = ""
        Dim vRespuestaRegisterPutAway As String = ""
        Dim CurrentSortingMethod As Integer = 1 '#EJC20210614: Según Ricardo, este valor se envía fijo.
        Dim vFechaVenceNav As String = ""
        Dim Bodega_Avanzada As Boolean = False '#CKFK 20211123 Agregué variable para determinar si una bodega es avanzada o no

        Try

            CnnLog.Open()

            lTransaccionesSalida = clsLnI_nav_transacciones_out.Get_Lotes_Salida_Pendientes_Envio_By_IdBodega(BeConfigEnc.Idbodega)

            Dim wsBodegaService As New Ficha_Bodegas_Service() With
                {
                .UseDefaultCredentials = UsarCredencialesPorDefecto,
                .Credentials = CredencialesConexion
                }

            wsBodegaService.Url = My.Settings.DynamicsNavInterface_WSFichaBodegas_Ficha_Bodegas_Service

            '#EJC20210426: CodeUnit de NAV para WMS, agregado por la bodega de PT.
            Dim wsCUWMS As New CUWMS.CUWMS() With {.UseDefaultCredentials = UsarCredencialesPorDefecto,
                                                   .Credentials = CredencialesConexion
                                                   }

            wsCUWMS.Url = My.MySettings.Default.NavSync_CUWMS_CUWMS

            '#EJC20210426: CodeUnit de NAV para WMS, agregado por la bodega de PT.
            Dim wsSrvPickingNAV As New Picking_Service() With {.UseDefaultCredentials = UsarCredencialesPorDefecto,
                                                               .Credentials = CredencialesConexion
                                                              }

            wsSrvPickingNAV.Url = My.MySettings.Default.NavSync_WSPicking_Picking_Service

            Dim srvHojaDeTrabajo As New Crea_picking_Service() With
                {
                .UseDefaultCredentials = UsarCredencialesPorDefecto,
                .Credentials = CredencialesConexion
                }

            srvHojaDeTrabajo.Url = My.Settings.NavSync_WSCreaPicking_Crea_picking_Service

            If Not lTransaccionesSalida Is Nothing AndAlso lTransaccionesSalida.Count > 0 Then

                Actualizar_Progreso(lblprg, String.Format("Transacciones a enviar: {0}", lTransaccionesSalida.Count))

                '#CKFK 20211121 Agregué el campo IdTipoDocumento
                Dim ListaPedidosTransf = (From i In lTransaccionesSalida
                                          Group i By Keys = New With {Key i.No_pedido, Key i.Idpedidoenc, Key i.Codigo_Bodega_Origen, i.IdTipoDocumento} Into Group
                                          Select New With {Key Keys.No_pedido, Key Keys.IdPedidoEnc, Key Keys.Codigo_Bodega_Origen, Key Keys.IdTipoDocumento})
                Dim Enviado_A_Erp As Boolean = False '#CKFK 20180518 10:20 PM Agregué la validación de si el registro ya fue enviado o no a NAV

                For Each PT In ListaPedidosTransf

                    Enviado_A_Erp = clsLnTrans_pe_enc.Get_Estado_Enviado_A_ERP(PT.No_pedido)
                    Bodega_Avanzada = Es_Bodega_Avanzada(PT.Codigo_Bodega_Origen)

                    If Not Enviado_A_Erp Then

                        BeBodegaNAV = wsBodegaService.Read(PT.Codigo_Bodega_Origen)

                        If Enviar_Lotes_Transf(PT.No_pedido, lTransaccionesSalida, lblprg, prg) Then

                            If Enviar_Cantidades_Transf(PT.No_pedido, lTransaccionesSalida, lblprg, prg) Then

                                Try

                                    If Not Bodega_Avanzada Then
                                        wsRegistra_Transfer_Envio.RegistrarEnvTransfer(PT.No_pedido)
                                    Else

                                        '#CKFK 20211123 Registrar el Picking de los pedidos de transferencia o de venta
                                        If PT.IdTipoDocumento = clsDataContractDI.tTipoDocumentoSalida.Transferencia_Interna_WMS Then

                                            Dim vURLPickingNAV As String = My.Settings.NavSync_WSPicking_Picking_Service

                                            Dim pkcService As New Picking_Service With
                                                {
                                                    .UseDefaultCredentials = False,
                                                    .Credentials = CredencialesConexion,
                                                    .Url = vURLPickingNAV
                                                }

                                            '#CKFK 20211123 Esto me va a servir para registrar la transferencia
                                            Dim vUrlCodeUnit As String = My.Settings.NavSync_CUWMS_CUWMS

                                            Dim ws2 As New CUWMS.CUWMS() With
                                                {
                                                    .UseDefaultCredentials = False,
                                                    .Credentials = CredencialesConexion,
                                                    .Url = vUrlCodeUnit
                                                }

                                            Dim BePedidoEnc As New clsBeTrans_pe_enc

                                            BePedidoEnc = clsLnTrans_pe_enc.GetSingle(PT.IdPedidoEnc, False)

                                            Dim vPicking As New Picking

                                            vPicking = pkcService.Read(BePedidoEnc.No_Picking_ERP)

                                            For Each PL In vPicking.WhseActivityLines

                                                PL.Qty_Handled = 0

                                            Next

                                            pkcService.Update(vPicking)

                                            If vPicking IsNot Nothing Then

                                                ws2.RegisterPutAway(BePedidoEnc.No_Picking_ERP, vRespuestaRegisterPutAway)

                                                If vRespuestaRegisterPutAway.Contains("Sucessfully created") Then
                                                    Throw New Exception(vRespuestaRegisterPutAway)
                                                End If

                                            End If

                                        End If

                                    End If

                                    Actualizar_Progreso(lblprg, String.Format("Transacciones de salida enviadas correctamente: {0}", lTransaccionesSalida.Count))

                                    clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP(PT.IdPedidoEnc, True, BeConfigEnc.IdUsuario)

                                Catch ex As Exception

                                    Actualizar_Progreso(lblprg, String.Format("Error al registrar el pedido:{0} en el ERP. Error: {1}", PT.No_pedido, ex.Message))

                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al registrar el pedido:{0} en el ERP. Error: {1}", PT.No_pedido, ex.Message),
                                                                                PT.No_pedido,
                                                                                BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                BeConfigDet.Idnavconfigdet,
                                                                                CnnLog)

                                End Try

                            End If

                        End If

                    Else

                        Try

                            TransferNav = wsPedidoTransferenciaService.Read(PT.No_pedido)

                            If Not TransferNav Is Nothing Then

                                lLotes = Sl.Get_Lista_Lotes(PT.No_pedido)

                                '#EJC20180614: Si tiene registros enviados intento registrar, si no, no.
                                lTransaccionesSalidaReproceso = clsLnI_nav_transacciones_out.Get_Lotes_Salida_Pendientes_Envio(PT.No_pedido)

                                If lTransaccionesSalidaReproceso.Count > 0 Then

                                    If lLotes.Count > 0 Then

                                        vContadorReproceso = 0

                                        For Each LoteWMS In lTransaccionesSalidaReproceso

                                            LoteEnviado = lLotes.Find(Function(x) x.Item_No = LoteWMS.Codigo_producto _
                                                        AndAlso x.Quantity_Base = LoteWMS.Cantidad _
                                                        AndAlso x.Lot_No = LoteWMS.Lote)

                                            If LoteEnviado Is Nothing Then 'El lote no se ha enviado a NAV
                                                LoteWMS.Enviado = False
                                                vContadorReproceso += 1
                                            Else
                                                'El lote ya fue enviado
                                            End If

                                        Next

                                    End If

                                    If vContadorReproceso > 0 Then
                                        If Enviar_Lotes_Transf(PT.No_pedido, lTransaccionesSalidaReproceso, lblprg, prg) Then
                                            If Enviar_Cantidades_Transf(PT.No_pedido, lTransaccionesSalidaReproceso, lblprg, prg) Then
                                                'Igual registro al final ;)
                                            End If
                                        End If
                                    End If

                                    If Not Bodega_Avanzada Then
                                        wsRegistra_Transfer_Envio.RegistrarEnvTransfer(PT.No_pedido)
                                    Else
                                        '#CKFK 20211123 Registrar el Picking de los pedidos de transferencia o de venta
                                        If PT.IdTipoDocumento = clsDataContractDI.tTipoDocumentoSalida.Transferencia_Interna_WMS OrElse
                                            PT.IdTipoDocumento = clsDataContractDI.tTipoDocumentoSalida.Pedido_De_Cliente Then

                                            Dim vURLPickingNAV As String = My.Settings.NavSync_WSPicking_Picking_Service

                                            Dim pkcService As New Picking_Service With
                                                {
                                                    .UseDefaultCredentials = False,
                                                    .Credentials = CredencialesConexion,
                                                    .Url = vURLPickingNAV
                                                }

                                            '#CKFK 20211123 Esto me va a servir para registrar la transferencia
                                            Dim vUrlCodeUnit As String = My.Settings.NavSync_CUWMS_CUWMS

                                            Dim ws2 As New CUWMS.CUWMS() With
                                                {
                                                    .UseDefaultCredentials = False,
                                                    .Credentials = CredencialesConexion,
                                                    .Url = vUrlCodeUnit
                                                }

                                            Dim BePedidoEnc As New clsBeTrans_pe_enc
                                            BePedidoEnc = clsLnTrans_pe_enc.GetSingle(PT.IdPedidoEnc, False)

                                            Dim vPicking As New Picking
                                            vPicking = pkcService.Read(BePedidoEnc.No_Picking_ERP)

                                            If vPicking IsNot Nothing Then
                                                ws2.RegisterPutAway(BePedidoEnc.No_Picking_ERP, vRespuestaRegisterPutAway)
                                            End If

                                        End If

                                        Actualizar_Progreso(lblprg, String.Format("Se registró el pedido:{0} correctamente en el ERP.", PT.No_pedido))

                                        clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP(PT.IdPedidoEnc, True, BeConfigEnc.IdUsuario)

                                    End If

                                End If


                            Else

                                If Not Enviado_A_Erp Then
                                    Actualizar_Progreso(lblprg, String.Format("Se registró el pedido:{0} correctamente en el ERP.", PT.No_pedido))
                                    '#EJC20180614: El pedido ya se recibió en la bodega destino.
                                    clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP(PT.IdPedidoEnc, True, BeConfigEnc.IdUsuario)
                                End If

                            End If

                        Catch ex As Exception

                            If ex.Message.Contains("There is nothing to post.") Then 'Pedido sin nada que registrar

                                Actualizar_Progreso(lblprg, String.Format("Nada que registrar para pedido: {0} en NAV.", PT.No_pedido))
                                clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP(PT.IdPedidoEnc, True, BeConfigEnc.IdUsuario)

                            Else

                                Actualizar_Progreso(lblprg, String.Format("Error al registrar el pedido:{0} en el ERP. Error: {1}", PT.No_pedido, ex.Message))

                                clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al registrar el pedido:{0} en el ERP. Error: {1}", PT.No_pedido, ex.Message),
                                                                           BeNavEjecucionEnc.IdEjecucionEnc,
                                                                           BeConfigDet.Idnavconfigdet,
                                                                           PT.No_pedido,
                                                                           CnnLog)

                            End If

                        End Try

                    End If


                Next

            Else

                lTransPtPendienteRegistroEnNav = clsLnTrans_pe_enc.Get_All_Pendiente_Registro_MI3()

                If lTransPtPendienteRegistroEnNav.Count > 0 Then

                    For Each PT In lTransPtPendienteRegistroEnNav

                        'Cerrada 
                        If PT.Estado = "Despachado" Or PT.Estado = "Verificado" Then

                            Try

                                TransferNav = wsPedidoTransferenciaService.Read(PT.Referencia)

                                If Not TransferNav Is Nothing Then

                                    lLotes = Sl.Get_Lista_Lotes(PT.Referencia)

                                    '#EJC20180614: Si tiene registros enviados intento registrar, si no, no.
                                    lTransaccionesSalida = clsLnI_nav_transacciones_out.Get_Lotes_Salida_Pendientes_Envio(PT.Referencia)

                                    If lTransaccionesSalida.Count > 0 Then

                                        If lLotes.Count > 0 Then

                                            vContadorReproceso = 0

                                            For Each LoteWMS In lTransaccionesSalida

                                                LoteEnviado = lLotes.Find(Function(x) x.Item_No = LoteWMS.Codigo_producto _
                                                                AndAlso x.Quantity_Base = LoteWMS.Cantidad _
                                                                AndAlso x.Lot_No = LoteWMS.Lote)

                                                If LoteEnviado Is Nothing Then 'El lote no se ha enviado a NAV
                                                    LoteWMS.Enviado = False
                                                    vContadorReproceso += 1
                                                End If

                                            Next

                                        End If

                                        If vContadorReproceso > 0 Then
                                            If Enviar_Lotes_Transf(PT.Referencia, lTransaccionesSalida, lblprg, prg) Then
                                                If Enviar_Cantidades_Transf(PT.Referencia, lTransaccionesSalida, lblprg, prg) Then
                                                    'Igual registro al final ;)
                                                End If
                                            End If
                                        End If

                                        If Not Bodega_Avanzada Then
                                            wsRegistra_Transfer_Envio.RegistrarEnvTransfer(PT.Referencia)
                                        Else
                                            '#CKFK 20211123 Registrar el Picking de los pedidos de transferencia o de venta
                                            If PT.IdTipoPedido = clsDataContractDI.tTipoDocumentoSalida.Transferencia_Interna_WMS OrElse
                                                PT.IdTipoPedido = clsDataContractDI.tTipoDocumentoSalida.Pedido_De_Cliente Then

                                                Dim vURLPickingNAV As String = My.Settings.NavSync_WSPicking_Picking_Service

                                                Dim pkcService As New Picking_Service With
                                                    {
                                                        .UseDefaultCredentials = False,
                                                        .Credentials = CredencialesConexion,
                                                        .Url = vURLPickingNAV
                                                    }

                                                '#CKFK 20211123 Esto me va a servir para registrar la transferencia
                                                Dim vUrlCodeUnit As String = My.Settings.NavSync_CUWMS_CUWMS

                                                Dim ws2 As New CUWMS.CUWMS() With
                                                    {
                                                        .UseDefaultCredentials = False,
                                                        .Credentials = CredencialesConexion,
                                                        .Url = vUrlCodeUnit
                                                    }

                                                Dim vPicking As New Picking

                                                vPicking = pkcService.Read(PT.No_Picking_ERP)

                                                If vPicking IsNot Nothing Then
                                                    ws2.RegisterPutAway(PT.No_Picking_ERP, vRespuestaRegisterPutAway)
                                                End If

                                            End If

                                        End If

                                        Actualizar_Progreso(lblprg, String.Format("Se registró el pedido:{0} correctamente en el ERP.", PT.Referencia))

                                        clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP(PT.IdPedidoEnc, True, BeConfigEnc.IdUsuario)

                                    End If

                                Else

                                    Actualizar_Progreso(lblprg, String.Format("Se registró el pedido:{0} correctamente en el ERP.", PT.Referencia))
                                    '#EJC20180614: El pedido ya se recibió en la bodega destino.
                                    clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP(PT.IdPedidoEnc, True, BeConfigEnc.IdUsuario)

                                End If

                            Catch ex As Exception

                                If ex.Message.Contains("There is nothing to post.") Then 'Pedido sin nada que registrar                                    
                                    Actualizar_Progreso(lblprg, String.Format("Nada que registrar para pedido: {0} en NAV.", PT.Referencia))
                                Else

                                    Actualizar_Progreso(lblprg, String.Format("Error al registrar el pedido:{0} en el ERP. Error: {1}", PT.Referencia, ex.Message))

                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al registrar el pedido:{0} en el ERP. Error: {1}", PT.Referencia, ex.Message),
                                                                                BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                BeConfigDet.Idnavconfigdet,
                                                                                PT.Referencia,
                                                                                CnnLog)

                                End If

                            End Try

                        End If

                    Next

                Else
                    Actualizar_Progreso(lblprg, "No hay transacciones para enviar.")
                End If

            End If

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            prg.Value = 0
            prg.Visible = False
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
        End Try

    End Sub

    Private Function Es_Bodega_Avanzada(pBodega As String) As Boolean

        Dim BeBodegaNAV As Ficha_Bodegas

        Es_Bodega_Avanzada = False

        Try

            Dim wsBodegaService As New Ficha_Bodegas_Service() With
                {
                .UseDefaultCredentials = UsarCredencialesPorDefecto,
                .Credentials = CredencialesConexion
                }

            wsBodegaService.Url = My.Settings.DynamicsNavInterface_WSFichaBodegas_Ficha_Bodegas_Service

            'Si es una bodega avanzada 
            BeBodegaNAV = wsBodegaService.Read(pBodega)

            Return BeBodegaNAV.Require_Pick AndAlso BeBodegaNAV.Require_Receive

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Private Function Enviar_Lotes_Transf(ByVal NoPedidoTransf As String,
                                         ByRef lTransaccionesSalida As List(Of clsBeI_nav_transacciones_out),
                                         ByRef lblprg As RichTextBox,
                                         ByRef prg As System.Windows.Forms.ProgressBar) As Boolean

        Return Enviar_Lotes_Transf(NoPedidoTransf, 0, lTransaccionesSalida, lblprg, prg)

    End Function

    Private Function Enviar_Lotes_Transf(ByVal NoPedidoTransf As String,
                                         ByVal pIdPedidoEnc As Integer,
                                         ByRef lTransaccionesSalida As List(Of clsBeI_nav_transacciones_out),
                                         ByRef lblprg As RichTextBox,
                                         ByRef prg As System.Windows.Forms.ProgressBar) As Boolean

        Enviar_Lotes_Transf = False

        Try

            prg.Maximum = lTransaccionesSalida.Count
            prg.Visible = True

            Dim vContador As Integer = 0
            Dim BePresentacion As New clsBeProducto_Presentacion
            Dim vCantidad As Double = 0
            Dim vUnidMed As String = ""

            '#EJC20260608: Blindaje La Cumbre/NAV.
            'Evita mezclar líneas de otro IdPedidoEnc cuando comparten No_pedido.
            Dim listaLotes = lTransaccionesSalida.Where(Function(x) x.No_pedido = NoPedidoTransf AndAlso
                                                                    x.Enviado = False AndAlso
                                                                    (pIdPedidoEnc <= 0 OrElse x.Idpedidoenc = pIdPedidoEnc)).ToList()

            If Not listaLotes Is Nothing Then

                For Each I In listaLotes

                    Actualizar_Progreso(lblprg, String.Format(vbNewLine & "Procesando Lote de Pedido: {0} Línea:{1} ", I.No_pedido, I.No_linea))

                    Try

                        If Not I.Idpresentacion = 0 Then

                            '#EJC20180418: Enviar la cantidad en UMBAS.
                            BePresentacion.IdPresentacion = I.Idpresentacion
                            clsLnProducto_presentacion.GetSingle(BePresentacion)
                            vCantidad = I.Cantidad * BePresentacion.Factor

                            'Enviar a Nav el nombre/codigo de la unidad de medida básica.
                            vUnidMed = clsLnUnidad_medida.Get_Nombre_By_IdUnidadMedida(I.Idunidadmedida)

                        Else
                            vCantidad = I.Cantidad
                            vUnidMed = I.Unidad_medida
                        End If

                        '#EJC20180803: Se redondean los decimales hasta que se envian los datos a NAV
                        'porque NAV solo soporta 5 decimales, el sistema actualmente trabaja con 6
                        'pero en el futuro, deben existir dos parámetros para el redonde de decimales.

                        wsLotePedidoTransferencia.Url = My.Settings.DynamicsNavInterface_WSLotePedidoTransferencia_Lote_PedidoTransferencia

                        '#EJC20180503: Enviar siempre UMBAS en Enviar_Lotes_Ingreso.
                        wsLotePedidoTransferencia.LoteLinPedidoTransfer(I.No_pedido,
                                                                        I.No_linea,
                                                                        I.Codigo_producto,
                                                                        I.Codigo_variante,
                                                                        vUnidMed,
                                                                        Math.Round(vCantidad, 5),
                                                                        I.Lote)

                        vContador += 1

                        prg.Value = vContador

                        I.Enviado = True

                    Catch ex As Exception

                        Dim vMensaje As String = String.Format("#ERROR_202309130955 Lotes (Env): {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
                        '#EJC20171105_1259AM_REF01: Agregar excepción a log.
                        clsLnI_nav_ejecucion_det_error.Inserta_Log(vMensaje,
                                                          "",
                                                          BeNavEjecucionEnc.IdEjecucionEnc,
                                                          BeConfigDet.Idnavconfigdet)

                        Actualizar_Progreso(lblprg, vMensaje)

                    End Try

                Next

            End If

            '
            Enviar_Lotes_Transf = (listaLotes.Count = vContador)

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    '#EJC20180111: Enviar las cantidades sumadas y agrupadas por producto (sin considerar el lote)
    Private Function Enviar_Cantidades_Transf(ByVal NoPedidoTransf As String,
                                              ByRef lTransaccionesSalida As List(Of clsBeI_nav_transacciones_out),
                                              ByRef lblprg As RichTextBox,
                                              ByRef prg As System.Windows.Forms.ProgressBar) As Boolean

        Return Enviar_Cantidades_Transf(NoPedidoTransf, 0, lTransaccionesSalida, lblprg, prg)

    End Function

    Private Function Enviar_Cantidades_Transf(ByVal NoPedidoTransf As String,
                                              ByVal pIdPedidoEnc As Integer,
                                              ByRef lTransaccionesSalida As List(Of clsBeI_nav_transacciones_out),
                                              ByRef lblprg As RichTextBox,
                                              ByRef prg As System.Windows.Forms.ProgressBar) As Boolean

        Enviar_Cantidades_Transf = False

        Try

            prg.Maximum = lTransaccionesSalida.Count
            prg.Visible = True

            Dim vContador As Integer = 0

            For Each T In lTransaccionesSalida.Where(Function(x) x.No_pedido = NoPedidoTransf AndAlso
                                                                 x.Enviado = False AndAlso
                                                                 (pIdPedidoEnc <= 0 OrElse x.Idpedidoenc = pIdPedidoEnc))
                T.Enviado = True
            Next

            Dim ListaResumen = (From i In lTransaccionesSalida.Where(Function(x) x.No_pedido = NoPedidoTransf AndAlso
                                                                         (pIdPedidoEnc <= 0 OrElse x.Idpedidoenc = pIdPedidoEnc))
                                Group i By Keys = New With {Key i.No_pedido, Key i.No_linea,
                                Key i.Codigo_producto, Key i.Codigo_variante, Key i.Enviado} Into Group
                                Select New With {Keys.No_pedido, Keys.No_linea,
                                                 Keys.Codigo_producto,
                                                 Keys.Codigo_variante,
                                                 Keys.Enviado,
                                                 .Cantidad = Group.Sum(Function(x) x.Cantidad)})

            Dim lista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)

            For Each I In ListaResumen

                Actualizar_Progreso(lblprg, String.Format(vbNewLine & "Procesando Cantidad de Pedido: {0} Línea:{1} ", I.No_pedido, I.No_linea))

                Try

                    '#EJC20180803: Se redondean los decimales hasta que se envian los datos a NAV
                    'porque NAV solo soporta 5 decimales, el sistema actualmente trabaja con 6
                    'pero en el futuro, deben existir dos parámetros para el redonde de decimales.

                    wsCantidadPedidoTransferencia.Url = My.Settings.DynamicsNavInterface_WSCantidadPedidoTransferencia_CantidadEnviar_PedidoTransferencia

                    wsCantidadPedidoTransferencia.LineaPedidoTransferCantEnviar(I.No_pedido,
                                                                                I.No_linea,
                                                                                I.Codigo_producto,
                                                                                I.Codigo_variante,
                                                                                Math.Round(I.Cantidad, 5))

                    lista_A_Actualizar = lTransaccionesSalida.Where(Function(x) x.No_pedido = I.No_pedido AndAlso
                        x.No_linea = I.No_linea AndAlso x.Codigo_producto = I.Codigo_producto AndAlso
                        x.Codigo_variante = I.Codigo_variante AndAlso x.Enviado = True AndAlso
                        (pIdPedidoEnc <= 0 OrElse x.Idpedidoenc = pIdPedidoEnc)).ToList()

                    If Not lista_A_Actualizar Is Nothing Then

                        For Each T In lista_A_Actualizar
                            clsLnI_nav_transacciones_out.Actualizar(T)
                        Next

                    End If

                    vContador += 1

                    prg.Value = vContador

                Catch ex As Exception

                    '#EJC20171105_1259AM_REF01: Agregar excepción a log.
                    clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                                      "",
                                                      BeNavEjecucionEnc.IdEjecucionEnc,
                                                      BeConfigDet.Idnavconfigdet)

                    Actualizar_Progreso(lblprg, String.Format(vbNewLine & "Error al enviar cantidad de pedido de transf. a Nav desde WS: {0} {1}", ex.Message, vbNewLine))

                End Try

            Next

            '#CKFK 20180526 07:08 PM agregué esta línea Enviar_Cantidades_Transf = True porque la función siempre devolvía False
            Enviar_Cantidades_Transf = True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Sub Actualizar_Progreso(ByRef lblPrg As RichTextBox, mensaje As String)
        lblPrg.AppendText(mensaje & vbNewLine)
        lblPrg.Refresh()
        lblPrg.SelectionStart = lblPrg.TextLength
        lblPrg.ScrollToCaret()
    End Sub

End Class
