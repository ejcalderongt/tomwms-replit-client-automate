Imports System.Data.SqlClient
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports TOMWMS.WSListaCliente
Imports TOMWMS.WSPedidosCliente

Public Class clsSyncNavPedidoVenta : Inherits clsInterfaceBase

    Implements IDisposable

    Property pBodega As String = ""

    Private fichaPedidosVenta() As Pedidos

    Dim VContadorBitacoraTomims As Integer = 0
    Dim VContadorBitacoraIntermedia As Integer = 0

    Private wsPedidoVentaService As New Pedidos_Service() With
    {
    .UseDefaultCredentials = UsarCredencialesPorDefecto,
    .Credentials = CredencialesConexion
    }

    '#EJC20210426: CodeUnit de NAV para WMS, agregado por la bodega de PT.
    Private wsCUWMS As New CUWMS.CUWMS() With {.UseDefaultCredentials = UsarCredencialesPorDefecto,
                                               .Credentials = CredencialesConexion
                                              }

    Public Sub Dispose() Implements IDisposable.Dispose
        If wsPedidoVentaService IsNot Nothing Then
            wsPedidoVentaService.Dispose()
            wsPedidoVentaService = Nothing
        End If
    End Sub

    Dim BeNavEjecRes As clsBeI_nav_ejecucion_res = Nothing

    Public Function Importar_Pedidos_Venta_Desde_WSNav_A_TablaIntermedia(ByVal lblprg As RichTextBox,
                                                                         ByRef prg As System.Windows.Forms.ProgressBar,
                                                                         ByRef cnnLog As SqlConnection) As Boolean

        Importar_Pedidos_Venta_Desde_WSNav_A_TablaIntermedia = False

        Dim lConection As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConection.Open() : lTransaction = lConection.BeginTransaction(IsolationLevel.ReadCommitted)

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("********** PROCESANDO DOCUMENTO EN TABLA INTERMEDIA ********** ")
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()

            Dim lPedidosTransfRec As New List(Of Pedidos)

            '#EJC20180503: Pedidos de transferencia dirigidos hacia -> bodegas de MP.
            lPedidosTransfRec = Get_Pedidos_Venta_FromWS(lConection, lTransaction, True)

            BeNavEjecucionRes.Registros_ws = lPedidosTransfRec.Count

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, cnnLog)

            Dim BeI_nav_PedidoCompra As clsBeI_nav_ped_compra_enc
            Dim BeI_nav_PedidoCompraDet As clsBeI_nav_ped_compra_det
            Dim BeProductoBodega As New clsBeProducto_bodega
            Dim BeNavLote As New clsBeI_nav_ped_compra_det_lote

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Pedidos de traslado en WS: {0} ", fichaPedidosVenta.Count))

            prg.Maximum = lPedidosTransfRec.Count

            Dim vContador As Integer = 0

            For Each PT As Pedidos In lPedidosTransfRec

                BeI_nav_PedidoCompra = New clsBeI_nav_ped_compra_enc

                CopyObject(PT, BeI_nav_PedidoCompra)

                If Not PT.Posting_DateSpecified Then
                    PT.Posting_Date = Now.Date
                ElseIf PT.Posting_Date.Year <= 1000 Then
                    PT.Posting_Date = Now.Date
                End If

                If Not PT.Shipment_DateSpecified Then
                    PT.Shipment_Date = Now.Date
                ElseIf PT.Shipment_Date.Year <= 1000 Then
                    PT.Shipment_Date = Now.Date
                End If

                'Proveedor
                BeI_nav_PedidoCompra.Buy_From_Vendor_Name = PT.Sell_to_Customer_Name
                BeI_nav_PedidoCompra.Buy_From_Vendor_No = PT.Sell_to_Customer_No
                BeI_nav_PedidoCompra.No = PT.No
                BeI_nav_PedidoCompra.Status = PT.Status

                If PT.External_Document_No IsNot Nothing Then
                    BeI_nav_PedidoCompra.Internal_Transfer_Document_No = PT.External_Document_No
                End If

                clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Pedido Compra: {0} ", BeI_nav_PedidoCompra.No, vbNewLine))

                Try
                    'PT-086861
                    'PT-086728
                    '#EJC20180503: Es un documento de transferencia desde otra bodega hacia las bodegas de MP.
                    BeI_nav_PedidoCompra.Is_Internal_Transfer = True
                    BeI_nav_PedidoCompra.Status = 1 'Lanzado

                    If Not clsLnI_nav_ped_compra_enc.Exist(BeI_nav_PedidoCompra.No, lConection, lTransaction) Then
                        'Insertar encabezado
                        clsLnI_nav_ped_compra_enc.Insertar(BeI_nav_PedidoCompra, lConection, lTransaction)
                    End If

                    VContadorBitacoraIntermedia += 1

                    prg.Value = vContador

                    vContador += 1

                    Application.DoEvents()

                    'Insertar detalle
                    If Not PT.SalesLines Is Nothing Then

                        For Each L As Sales_Order_Line In PT.SalesLines

                            BeI_nav_PedidoCompraDet = New clsBeI_nav_ped_compra_det

                            Try

                                Try

                                    CopyObject(L, BeI_nav_PedidoCompraDet)
                                    BeI_nav_PedidoCompraDet.No = L.No
                                    BeI_nav_PedidoCompraDet.Type = 2 'Articulo                                    
                                    BeI_nav_PedidoCompraDet.Quantity = L.Quantity

                                Catch ex As Exception
                                    Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
                                End Try

                                BeI_nav_PedidoCompraDet.NoEnc = PT.No

                                If Not L.No Is Nothing Then

                                    BeProductoBodega = clsLnProducto_bodega.Existe(L.No,
                                                                                   BeConfigEnc.Idbodega,
                                                                                   lConection,
                                                                                   lTransaction)

                                    'Existe el producto en el maestro?
                                    If Not BeProductoBodega Is Nothing Then

                                        '#EJC20180504: 
                                        'L.Qty_to_Ship La cantidad a recibir.
                                        'L.Qty_Shiped cuando ya se registró el envío.
                                        If (L.Qty_to_Ship = 0) AndAlso (L.Quantity_Shipped > 0) AndAlso (L.Quantity < L.Quantity_Shipped) Then

                                            If clsLnI_nav_ped_compra_det.Exist(BeI_nav_PedidoCompraDet, lConection, lTransaction) Then
                                                clsLnI_nav_ped_compra_det.Actualizar(BeI_nav_PedidoCompraDet, lConection, lTransaction)
                                                VContadorBitacoraIntermedia += 1
                                            Else
                                                clsLnI_nav_ped_compra_det.Insertar(BeI_nav_PedidoCompraDet, lConection, lTransaction)
                                                VContadorBitacoraIntermedia += 1
                                            End If

                                        Else
                                            clsPublic.Actualizar_Progreso(lblprg, String.Format("La cantidad para el producto:{0} ya fue completada.{1}", L.No, vbNewLine))
                                        End If

                                    Else

                                        Try

                                            clsLnI_nav_ejecucion_det_error.Inserta_Log("Producto no existe en maestro",
                                                                                       L.No,
                                                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                       BeConfigDet.Idnavconfigdet,
                                                                                       cnnLog)

                                            clsPublic.Actualizar_Progreso(lblprg, String.Format("Producto no existe en maestro: {0}{1}", L.No, vbNewLine))

                                        Catch ex As Exception
                                            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
                                        End Try

                                    End If 'Fin 'Existe el producto en el maestro?                  

                                End If

                            Catch ex As Exception

                                clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                           "Sin informacion",
                                                                           BeNavEjecucionEnc.IdEjecucionEnc,
                                                                           BeConfigDet.Idnavconfigdet, cnnLog)

                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar Linea desde el ws a intermedia en pedido de compra: {0}{1}{2}", BeI_nav_PedidoCompraDet.No, vbNewLine, ex.Message))

                            End Try

                        Next

                    Else
                        clsPublic.Actualizar_Progreso(lblprg, "Documento sin líneas de detalle.")
                    End If

                Catch ex As Exception

                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                      BeI_nav_PedidoCompra.No,
                                                      BeNavEjecucionEnc.IdEjecucionEnc,
                                                      BeConfigDet.Idnavconfigdet, cnnLog)

                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar Encabezado OC desde ws a intermedia: {0}{1}{2}", BeI_nav_PedidoCompra.No, vbNewLine, ex.Message))

                End Try

            Next

            lTransaction.Commit()

            clsPublic.Actualizar_Progreso(lblprg, "Fin de inserción en tabla intermedia de TOMWMS.")

            Importar_Pedidos_Venta_Desde_WSNav_A_TablaIntermedia = True

        Catch ex As Exception

            If Not lTransaction Is Nothing Then lTransaction.Rollback()

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet,
                                                       cnnLog)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar Ordenes Traslado desde ws a intermedia: {0}{1}", vbNewLine, ex.Message))

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))

        Finally
            If lConection.State = ConnectionState.Open Then lConection.Close()
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

                Dim BePresentacion As New clsBeProducto_Presentacion

                BePresentacion = clsLnProducto_presentacion.Existe_By_IdProducto_And_NombrePresentacion(BeProductoBodega.IdProducto,
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

                        BePresentacion = New clsBeProducto_Presentacion
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

                            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar presentación: {0}{1}", ex.Message, vbNewLine))

                        End Try

                    Else

                        Throw New Exception(String.Format("Error: No existe factor en unidad_medida_conversion 
                                                               para Producto: {0} UnidMedBas {1} <> UnidMed Ped. Compra {2} ",
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

    Public Function Importar_Pedidos_Venta_Desde_WS_A_Tabla_Intermedia(ByVal lblprg As RichTextBox,
                                                                       ByRef prg As System.Windows.Forms.ProgressBar,
                                                                       ByRef cnnLog As SqlConnection) As Boolean
        Importar_Pedidos_Venta_Desde_WS_A_Tabla_Intermedia = False

        Dim lConnection As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            clsPublic.Actualizar_Progreso(lblprg, "Consultando configuración de interface: " & BD.Instancia.IdConfiguracionInterface)

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

            clsPublic.Actualizar_Progreso(lblprg, "Procesando documento en tabla intermedia de TOMWMS.")

            Dim lPedidosTraslado As New List(Of Pedidos)

            lPedidosTraslado = Get_Pedidos_Venta_FromWS(lConnection, lTransaction, True)

            BeNavEjecucionRes.Registros_ws = lPedidosTraslado.Count

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, cnnLog)

            Dim BeI_Nav_PedidoVenta As clsBeI_nav_ped_traslado_enc
            Dim BeI_Nav_PedidoVentaDet As clsBeI_nav_ped_traslado_det
            Dim BeProductoBodega As New clsBeProducto_bodega

            clsPublic.Actualizar_Progreso(lblprg, vbCrLf)
            clsPublic.Actualizar_Progreso(lblprg, String.Format("Pedidos de Venta en WS: {0} ", fichaPedidosVenta.Count))

            prg.Maximum = lPedidosTraslado.Count

            Dim vContador As Integer = 0

            For Each PC As Pedidos In lPedidosTraslado

                If Not clsLnI_nav_ped_traslado_enc.Exist(PC.No) Then

                    BeI_Nav_PedidoVenta = New clsBeI_nav_ped_traslado_enc

                    CopyObject(PC, BeI_Nav_PedidoVenta)

                    If Not PC.Posting_DateSpecified Then
                        PC.Posting_Date = Now.Date
                    ElseIf PC.Posting_Date.Year <= 1000 Then
                        PC.Posting_Date = Now.Date
                    End If

                    If Not PC.Shipment_DateSpecified Then
                        PC.Shipment_Date = Now.Date
                    ElseIf PC.Shipment_Date.Year <= 1000 Then
                        PC.Shipment_Date = Now.Date
                    End If

                    BeI_Nav_PedidoVenta.No = PC.No
                    BeI_Nav_PedidoVenta.Status = PC.Status
                    BeI_Nav_PedidoVenta.Transfer_from_Code = PC.Location_Code
                    BeI_Nav_PedidoVenta.Transfer_from_Name = PC.Location_Code
                    BeI_Nav_PedidoVenta.Transfer_to_Code = PC.Sell_to_Customer_No
                    BeI_Nav_PedidoVenta.Transfer_to_Name = PC.Sell_to_Customer_Name
                    BeI_Nav_PedidoVenta.Transfer_to_Contact = PC.Sell_to_Contact
                    BeI_Nav_PedidoVenta.Transfer_to_CodeField = PC.Location_Code

                    If PC.External_Document_No IsNot Nothing Then
                        BeI_Nav_PedidoVenta.External_Document_No = PC.External_Document_No
                    Else
                        BeI_Nav_PedidoVenta.External_Document_No = ""
                    End If

                    clsPublic.Actualizar_Progreso(lblprg, vbCrLf)
                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Pedido de Venta: {0} ", BeI_Nav_PedidoVenta.No, vbNewLine))

                    Try

                        'Insertar Encabezado
                        clsLnI_nav_ped_traslado_enc.Insertar(BeI_Nav_PedidoVenta, lConnection, lTransaction)

                        VContadorBitacoraIntermedia += 1

                        prg.Value = vContador

                        vContador += 1

                        Application.DoEvents()

                        If Not PC.SalesLines Is Nothing Then

                            For Each L As Sales_Order_Line In PC.SalesLines

                                BeI_Nav_PedidoVentaDet = New clsBeI_nav_ped_traslado_det

                                Try

                                    CopyObject(L, BeI_Nav_PedidoVentaDet)

                                    BeI_Nav_PedidoVentaDet.NoEnc = PC.No
                                    BeI_Nav_PedidoVentaDet.No = L.No
                                    BeI_Nav_PedidoVentaDet.Price = L.Unit_Price
                                    BeI_Nav_PedidoVentaDet.Variant_Code = L.Variant_Code

                                    '#EJC20171106_1023AM_REF02: El valor nothing indica el final de la vista.
                                    If Not L.No Is Nothing Then

                                        BeProductoBodega = clsLnProducto_bodega.Existe(L.No, BeConfigEnc.Idbodega, lConnection, lTransaction)

                                        'Existe el producto en el maestro?
                                        If Not BeProductoBodega Is Nothing Then

                                            'Si Cantidad enviada es 0 se importa
                                            If L.Quantity_Shipped <> L.Quantity Then

                                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando producto : {0}{1}", L.No, vbNewLine))

                                                If clsLnI_nav_ped_traslado_det.Exist(BeI_Nav_PedidoVentaDet, lConnection, lTransaction) Then
                                                    clsLnI_nav_ped_traslado_det.ActualizarFromIn(BeI_Nav_PedidoVentaDet, lConnection, lTransaction)
                                                    VContadorBitacoraIntermedia += 1
                                                Else
                                                    clsLnI_nav_ped_traslado_det.Insertar(BeI_Nav_PedidoVentaDet, lConnection, lTransaction)
                                                    VContadorBitacoraIntermedia += 1
                                                End If

                                            End If

                                        Else

                                            Try

                                                clsLnI_nav_ejecucion_det_error.Inserta_Log("Producto no existe en maestro",
                                                                                            L.No,
                                                                                            BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                            BeConfigDet.Idnavconfigdet,
                                                                                            cnnLog)
                                                clsPublic.Actualizar_Progreso(lblprg, vbCrLf)
                                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Producto no existe en maestro: {0}{1}", L.No, vbNewLine))

                                            Catch ex As Exception
                                                Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
                                            End Try

                                        End If 'FIn Existe el producto en el maestro?

                                    End If

                                Catch ex As Exception

                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                             "Sin informacion",
                                                                             BeNavEjecucionEnc.IdEjecucionEnc,
                                                                             BeConfigDet.Idnavconfigdet,
                                                                             cnnLog)

                                    clsPublic.Actualizar_Progreso(lblprg, vbCrLf)
                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar Linea desde el ws a intermedia en pedido de traslado: {0}{1}{2}", BeI_Nav_PedidoVentaDet.No, vbNewLine, ex.Message))

                                End Try

                            Next

                        End If

                    Catch ex As Exception

                        clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                  BeI_Nav_PedidoVenta.No,
                                                                  BeNavEjecucionEnc.IdEjecucionEnc,
                                                                  BeConfigDet.Idnavconfigdet,
                                                                  cnnLog)

                        clsPublic.Actualizar_Progreso(lblprg, vbCrLf)
                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar Encabezado PT desde ws a intermedia: {0}{1}{2}", BeI_Nav_PedidoVenta.No, vbNewLine, ex.Message))

                    End Try

                End If

            Next

            'End If

            lTransaction.Commit()

            Importar_Pedidos_Venta_Desde_WS_A_Tabla_Intermedia = True

        Catch ex As Exception

            If Not lTransaction Is Nothing Then lTransaction.Rollback()

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet, cnnLog)

            clsPublic.Actualizar_Progreso(lblprg, vbCrLf)
            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar Ordenes Traslado desde ws a intermedia: {0}{1}", vbNewLine, ex.Message))

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))

        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            clsPublic.Actualizar_Progreso(lblprg, vbCrLf)
            clsPublic.Actualizar_Progreso(lblprg, "Fin de inserción en tabla intermedia.")
        End Try

    End Function

    Public Function Importar_Pedidos_Venta_Desde_WS_A_Tabla_Intermedia_Original(ByVal lblprg As RichTextBox,
                                                                                ByRef prg As System.Windows.Forms.ProgressBar,
                                                                                ByRef cnnLog As SqlConnection) As Boolean
        Importar_Pedidos_Venta_Desde_WS_A_Tabla_Intermedia_Original = False

        Dim lConnection As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            clsPublic.Actualizar_Progreso(lblprg, "Consultando configuración de interface: " & BD.Instancia.IdConfiguracionInterface)

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


            clsPublic.Actualizar_Progreso(lblprg, "Procesando documento en tabla intermedia de TOMWMS.")

            Dim lPedidosTraslado As New List(Of Pedidos)

            lPedidosTraslado = Get_Pedidos_Venta_FromWS(lConnection, lTransaction, True)

            BeNavEjecucionRes.Registros_ws = lPedidosTraslado.Count

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, cnnLog)

            Dim BeI_Nav_PedidoVenta As clsBeI_nav_ped_traslado_enc
            Dim BeI_Nav_PedidoVentaDet As clsBeI_nav_ped_traslado_det
            Dim BeProductoBodega As New clsBeProducto_bodega

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Pedidos de Venta en WS: {0} ", fichaPedidosVenta.Count))

            prg.Maximum = lPedidosTraslado.Count

            Dim vContador As Integer = 0

            'If clsLnI_nav_ped_traslado_det.Eliminar_Todos(lConnection, lTransaction) Then _
            '    AndAlso clsLnI_nav_ped_traslado_enc.Eliminar_Todos(lConnection, lTransaction) Then

            For Each PC As Pedidos In lPedidosTraslado

                BeI_Nav_PedidoVenta = New clsBeI_nav_ped_traslado_enc

                CopyObject(PC, BeI_Nav_PedidoVenta)

                If Not PC.Posting_DateSpecified Then
                    PC.Posting_Date = Now.Date
                ElseIf PC.Posting_Date.Year <= 1000 Then
                    PC.Posting_Date = Now.Date
                End If

                If Not PC.Shipment_DateSpecified Then
                    PC.Shipment_Date = Now.Date
                ElseIf PC.Shipment_Date.Year <= 1000 Then
                    PC.Shipment_Date = Now.Date
                End If

                BeI_Nav_PedidoVenta.No = PC.No
                BeI_Nav_PedidoVenta.Status = PC.Status
                BeI_Nav_PedidoVenta.Transfer_from_Code = PC.Location_Code
                BeI_Nav_PedidoVenta.Transfer_from_Name = PC.Location_Code
                BeI_Nav_PedidoVenta.Transfer_to_Code = PC.Sell_to_Customer_No
                BeI_Nav_PedidoVenta.Transfer_to_Name = PC.Sell_to_Customer_Name
                BeI_Nav_PedidoVenta.Transfer_to_Contact = PC.Sell_to_Contact
                BeI_Nav_PedidoVenta.Transfer_to_CodeField = PC.Location_Code

                If PC.External_Document_No IsNot Nothing Then
                    BeI_Nav_PedidoVenta.External_Document_No = PC.External_Document_No
                Else
                    BeI_Nav_PedidoVenta.External_Document_No = ""
                End If

                clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Pedido de Venta: {0} ", BeI_Nav_PedidoVenta.No, vbNewLine))

                Try

                    'Insertar Encabezado
                    clsLnI_nav_ped_traslado_enc.Insertar(BeI_Nav_PedidoVenta, lConnection, lTransaction)

                    VContadorBitacoraIntermedia += 1

                    prg.Value = vContador

                    vContador += 1

                    Application.DoEvents()

                    If Not PC.SalesLines Is Nothing Then

                        For Each L As Sales_Order_Line In PC.SalesLines

                            BeI_Nav_PedidoVentaDet = New clsBeI_nav_ped_traslado_det

                            Try

                                CopyObject(L, BeI_Nav_PedidoVentaDet)

                                BeI_Nav_PedidoVentaDet.NoEnc = PC.No
                                BeI_Nav_PedidoVentaDet.No = L.No
                                BeI_Nav_PedidoVentaDet.Price = L.Unit_Price
                                BeI_Nav_PedidoVentaDet.Variant_Code = L.Variant_Code

                                '#EJC20171106_1023AM_REF02: El valor nothing indica el final de la vista.
                                If Not L.No Is Nothing Then

                                    BeProductoBodega = clsLnProducto_bodega.Existe(L.No, BeConfigEnc.Idbodega, lConnection, lTransaction)

                                    'Existe el producto en el maestro?
                                    If Not BeProductoBodega Is Nothing Then

                                        'Si Cantidad enviada es 0 se importa
                                        If L.Quantity_Shipped <> L.Quantity Then

                                            clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando producto : {0}{1}", L.No, vbNewLine))

                                            If clsLnI_nav_ped_traslado_det.Exist(BeI_Nav_PedidoVentaDet, lConnection, lTransaction) Then
                                                clsLnI_nav_ped_traslado_det.ActualizarFromIn(BeI_Nav_PedidoVentaDet, lConnection, lTransaction)
                                                VContadorBitacoraIntermedia += 1
                                            Else
                                                clsLnI_nav_ped_traslado_det.Insertar(BeI_Nav_PedidoVentaDet, lConnection, lTransaction)
                                                VContadorBitacoraIntermedia += 1
                                            End If

                                        End If

                                    Else

                                        Try

                                            clsLnI_nav_ejecucion_det_error.Inserta_Log("Producto no existe en maestro",
                                                                                       L.No,
                                                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                       BeConfigDet.Idnavconfigdet,
                                                                                       cnnLog)

                                            clsPublic.Actualizar_Progreso(lblprg, String.Format("Producto no existe en maestro: {0}{1}", L.No, vbNewLine))

                                        Catch ex As Exception
                                            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
                                        End Try

                                    End If 'FIn Existe el producto en el maestro?

                                End If

                            Catch ex As Exception

                                clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                           "Sin informacion",
                                                                           BeNavEjecucionEnc.IdEjecucionEnc,
                                                                           BeConfigDet.Idnavconfigdet,
                                                                           cnnLog)

                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar Linea desde el ws a intermedia en pedido de traslado: {0}{1}{2}", BeI_Nav_PedidoVentaDet.No, vbNewLine, ex.Message))

                            End Try

                        Next

                    End If

                Catch ex As Exception

                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                               BeI_Nav_PedidoVenta.No,
                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                               BeConfigDet.Idnavconfigdet, cnnLog)

                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar Encabezado PT desde ws a intermedia: {0}{1}{2}", BeI_Nav_PedidoVenta.No, vbNewLine, ex.Message))

                End Try

            Next

            'End If

            lTransaction.Commit()

            Importar_Pedidos_Venta_Desde_WS_A_Tabla_Intermedia_Original = True

        Catch ex As Exception

            If Not lTransaction Is Nothing Then lTransaction.Rollback()

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet, cnnLog)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar Ordenes Traslado desde ws a intermedia: {0}{1}", vbNewLine, ex.Message))

            Throw ex

        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            clsPublic.Actualizar_Progreso(lblprg, "Fin de inserción en tabla intermedia.")
        End Try

    End Function

    Public Function Importar_Pedidos_Venta_Desde_Nav_A_WMS(ByRef lblprg As RichTextBox,
                                                           ByRef prg As System.Windows.Forms.ProgressBar,
                                                           Optional ByVal ForzarEjecucion As Boolean = False,
                                                           Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False) As Boolean
        Importar_Pedidos_Venta_Desde_Nav_A_WMS = False

        Dim lConnectionInterface As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransInterface As SqlTransaction = Nothing
        Dim pBePedidoEnc As clsBeTrans_pe_enc = Nothing
        Dim TrasladoExistente As clsBeTrans_pe_enc = Nothing
        Dim BeCliente As New clsBeCliente()
        Dim vContador As Integer = 0
        Dim vContadorLineasDet As Integer = 0
        Dim pClienteTiemposList As New List(Of clsBeCliente_tiempos)
        Dim BeProducto As New clsBeProducto()
        Dim pBePedidoDet As New clsBeTrans_pe_det()
        Dim vClienteTiempo As New clsBeCliente_tiempos()
        Dim vDiasVencimientoCliente As Integer = 0
        Dim BeUnidadMedida As New clsBeUnidad_medida()
        Dim BeProductoPresentacionDefecto As New clsBeProducto_Presentacion()
        Dim vContador_Lineas_Detalle_Pedido_Insertadas As Integer = 0
        Dim BeProductoPresentacion As New clsBeProducto_Presentacion()
        Dim lProductoPresentacion As New List(Of clsBeProducto_Presentacion)
        Dim vDeltaFactorPresentacion As Double = 0
        Dim vCantidadEnteraPresentacion As Double = 0
        Dim vCantidadSobranteUnidades As Double = 0
        Dim vExplosionAutomatica As Boolean = False
        Dim lBeStockResPedido As New List(Of clsBeStock_res)
        Dim vInsertoLineaDetalle As Boolean = False
        Dim Lineas_Detalle_Procesadas As New List(Of clsBeI_nav_ped_traslado_det)

        Try

            If Not ForzarEjecucion Then

                If Not Ejecutar_Interfaz("Pedido de Venta") Then
                    clsPublic.Actualizar_Progreso(lblprg, "La configuración de la interface indica que no se debe ejecutar en este momento. ")
                    Exit Function
                End If

            End If

            CnnLog.Open()

            BeNavEjecucionEnc.IdEjecucionEnc = clsLnI_nav_ejecucion_enc.MaxID(CnnLog)
            BeNavEjecucionEnc.IdNavConfigEnc = BD.Instancia.IdConfiguracionInterface
            BeNavEjecucionEnc.Fecha = Now

            clsLnI_nav_ejecucion_enc.Insertar_From_Interface(BeNavEjecucionEnc, CnnLog)

            BeNavEjecucionRes.IdEjecucionRes = clsLnI_nav_ejecucion_res.Max_IdEjecucionRes(CnnLog) + 1
            BeNavEjecucionRes.IdEjecucionEnc = BeNavEjecucionEnc.IdEjecucionEnc
            BeNavEjecucionRes.IdNavConfigDet = BeConfigDet.Idnavconfigdet
            BeNavEjecucionRes.Registros_ws = 0
            BeNavEjecucionRes.Registros_ti = 0
            BeNavEjecucionRes.Registros_WMS = 0
            BeNavEjecucionRes.Exitosa = False

            clsLnI_nav_ejecucion_res.Insertar(BeNavEjecucionRes, CnnLog)

            BeNavEjecRes = BeNavEjecucionRes

            lConnectionInterface.Open() : lTransInterface = lConnectionInterface.BeginTransaction(IsolationLevel.ReadUncommitted)

            clsPublic.Actualizar_Progreso(lblprg, vbCr)
            clsPublic.Actualizar_Progreso(lblprg, "Consultando configuración de interface: " & BD.Instancia.IdConfiguracionInterface)

            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface,
                                                          lConnectionInterface,
                                                          lTransInterface)

            If BeConfigEnc Is Nothing Then
                If BD.Instancia.IdConfiguracionInterface = 0 Then
                    Throw New Exception("No éstá definida la configuración de interface para el Id: " & BD.Instancia.IdConfiguracionInterface & " verifique el el conn.ini que se especificó el identificador de configuración para la interface.")
                Else
                    Throw New Exception("No éstá definida la configuración de interface para el Id: " & BD.Instancia.IdConfiguracionInterface & " verifique en la bd que existe el registro asociado al identificador de inteface: " & BD.Instancia.IdConfiguracionInterface)
                End If
            End If

            clsPublic.Actualizar_Progreso(lblprg, vbCr)
            clsPublic.Actualizar_Progreso(lblprg, "Consultando WebService de pedidoS de venta en: " & My.MySettings.Default.NavSync_WSPedidosVenta_Pedidos_Service)

            If Not Pregunta_Si_LLena_Intermedia Then

                If Not Importar_Pedidos_Venta_Desde_WS_A_Tabla_Intermedia(lblprg, prg, CnnLog) Then
                    Exit Function
                End If

            Else

                If XtraMessageBox.Show("¿Llenar tabla intermedia desde WS?", "Interface", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If Not Importar_Pedidos_Venta_Desde_WS_A_Tabla_Intermedia(lblprg, prg, CnnLog) Then
                        Exit Function
                    End If
                End If

            End If

            Dim lPedidoVentaEnc As New List(Of clsBeI_nav_ped_traslado_enc)

            clsPublic.Actualizar_Progreso(lblprg, "Consultando pedidos de venta en tabla intermedia ")

            lPedidoVentaEnc = clsLnI_nav_ped_traslado_enc.GetAll_Pedidos_Venta(lConnectionInterface, lTransInterface)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Pedidos de venta en tabla intermedia: {0}", lPedidoVentaEnc.Count))

            If lPedidoVentaEnc.Count > 0 Then

                prg.Maximum = lPedidoVentaEnc.Count

                prg.Value = 0

                clsPublic.Actualizar_Progreso(lblprg, "Trasladando documento a TOMWMS.")

                VContadorBitacoraTomims = 0

                For Each navPedidoVentaEnc As clsBeI_nav_ped_traslado_enc In lPedidoVentaEnc

                    If navPedidoVentaEnc.Status > 0 Then

                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando P.V.: {0} ", navPedidoVentaEnc.No, vbNewLine))

                        If navPedidoVentaEnc.Lineas_Detalle.Count > 0 Then

                            pBePedidoEnc = New clsBeTrans_pe_enc() With {.Referencia = navPedidoVentaEnc.No,
                                                                 .IdTipoPedido = navPedidoVentaEnc.Document_Type}

                            TrasladoExistente = clsLnTrans_pe_enc.Get_Single_By_Referencia(pBePedidoEnc, lConnectionInterface, lTransInterface)

                            vContadorLineasDet = 0

                            BeCliente = clsLnCliente.Get_Single_By_Codigo(navPedidoVentaEnc.Transfer_to_Code, lConnectionInterface, lTransInterface)

                            If BeCliente Is Nothing Then

                                Dim BeClienteBodegaDetalle As New clsBeCliente_bodega()
                                BeClienteBodegaDetalle = Insertar_Cliente_Single(navPedidoVentaEnc.Transfer_to_Code,
                                                                                     lConnectionInterface,
                                                                                     lTransInterface,
                                                                                     CnnLog,
                                                                                     lblprg,
                                                                                     prg)

                                If BeClienteBodegaDetalle Is Nothing Then
                                    Throw New Exception(String.Format("{0} No existe el cliente {1} en maestro para pedido de tralsado ", MethodBase.GetCurrentMethod.Name(), navPedidoVentaEnc.Transfer_to_Code))
                                Else
                                    BeCliente = BeClienteBodegaDetalle.Cliente
                                End If

                            End If

                            If Not TrasladoExistente Is Nothing Then
                                pBePedidoEnc.Activo = True
                            Else

                                '#EJC20171107_REF13_0506AM: El MaxId del IdPedidoEnc se genera dentro del insert                            
                                pBePedidoEnc.Fecha_Pedido = navPedidoVentaEnc.Posting_Date
                                pBePedidoEnc.Referencia = navPedidoVentaEnc.No
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
                                pBePedidoEnc.PropietarioBodega.IdPropietarioBodega = clsLnPropietarios.Get_IdPropietarioBodega_By_IdBodega_And_IdPropietario(BeConfigEnc.Idbodega, BeConfigEnc.IdPropietario, lConnectionInterface, lTransInterface)
                                pBePedidoEnc.IdPropietarioBodega = clsLnPropietarios.Get_IdPropietarioBodega_By_IdBodega_And_IdPropietario(BeConfigEnc.Idbodega, BeConfigEnc.IdPropietario, lConnectionInterface, lTransInterface)
                                pBePedidoEnc.TipoPedido = New clsBeTrans_pe_tipo
                                pBePedidoEnc.TipoPedido.IdTipoPedido = clsDataContractDI.tTipoDocumentoSalida.Pedido_De_Venta_NAV
                                pBePedidoEnc.Fecha_Pedido = navPedidoVentaEnc.Posting_Date
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
                                'pBePedidoEnc.No_documento = navPedidoVentaEnc.No
                                pBePedidoEnc.Local = True
                                pBePedidoEnc.Pallet_primero = True
                                pBePedidoEnc.Dias_cliente = 0
                                pBePedidoEnc.Anulado = False
                                pBePedidoEnc.IdPickingEnc = 0
                                pBePedidoEnc.RoadKilometraje = 0
                                pBePedidoEnc.RoadFechaEntr = navPedidoVentaEnc.Shipment_Date
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

                                clsLnTrans_pe_enc.Inserta_Encabezado(pBePedidoEnc, lConnectionInterface, lTransInterface)

                                pClienteTiemposList = clsLnCliente_tiempos.Get_All_Tiempos_By_IdCliente(pBePedidoEnc.IdCliente, lConnectionInterface, lTransInterface)

                                For Each PDet In navPedidoVentaEnc.Lineas_Detalle

                                    BeProductoPresentacion = Nothing

                                    If Not lBeStockResPedido Is Nothing Then
                                        lBeStockResPedido.Clear()
                                    End If

                                    PDet.Item_No = PDet.No
                                    BeProducto.Codigo = PDet.No

                                    BeProducto = clsLnProducto.Get_BeProducto_By_Codigo(BeProducto.Codigo,
                                                                                        BeConfigEnc.Idbodega,
                                                                                        lConnectionInterface, lTransInterface)

                                    BeUnidadMedida = clsLnUnidad_medida.Get_Unidad_Medida_By_Codigo(PDet.Unit_of_Measure_Code,
                                                                                                    lConnectionInterface,
                                                                                                    lTransInterface)
                                    If Not vClienteTiempo Is Nothing Then
                                        vDiasVencimientoCliente = vClienteTiempo.Dias_Local
                                    End If

                                    'Insertar detalle de pedido y reservar existencias.
                                    Debug.Print(PDet.No)

                                    vClienteTiempo = pClienteTiemposList.Find(Function(x) _
                                    x.IdClasificacion = BeProducto.Clasificacion.IdClasificacion _
                                    And x.IdFamilia = BeProducto.Familia.IdFamilia)


                                    '#EJC20210613: Si no están pidiendo en la unidad de medida básica.
                                    '#Asignar en el variant_code la presentación.
                                    If Not (BeProducto.UnidadMedida.Codigo = PDet.Unit_of_Measure_Code) Then

                                        If PDet.Variant_Code = "" Then

                                            If Not (PDet.Unit_of_Measure_Code.Trim = "") Then

                                                '#EJC20220314: Es una presentación?
                                                BeProductoPresentacion = clsLnProducto_presentacion.Get_By_Codigo_Producto_And_Presentacion(PDet.No,
                                                                                                                                            PDet.Unit_of_Measure_Code.Trim,
                                                                                                                                            lConnectionInterface,
                                                                                                                                            lTransInterface)

                                                '#EJC20220314: Existe una presentación con el codigo proporcionado.
                                                If Not BeProductoPresentacion Is Nothing Then

                                                    BeUnidadMedida = BeProducto.UnidadMedida
                                                    PDet.Variant_Code = PDet.Unit_of_Measure_Code

                                                End If

                                            End If

                                        End If

                                    Else
                                        '#EJC20220314:En teoría el producto viene en UMBAS.
                                        '#EJC20220224: Buscar por código.
                                        BeUnidadMedida = clsLnUnidad_medida.Get_Unidad_Medida_By_Codigo(PDet.Unit_of_Measure_Code,
                                                                                                        lConnectionInterface,
                                                                                                        lTransInterface)

                                        If BeUnidadMedida Is Nothing Then
                                            Throw New Exception(String.Format("{0} No existe la U.M. {1} en el maestro de WMS. ", MethodBase.GetCurrentMethod.Name(), PDet.Unit_of_Measure_Code))
                                        End If

                                        BeProductoPresentacionDefecto = clsLnProducto_presentacion.Get_Single_By_Codigo_Producto(PDet.Item_No,
                                                                                                                                 lConnectionInterface,
                                                                                                                                 lTransInterface)

                                    End If

                                    '#EJC20220224_0123AM: El producto viene en UMBAS.
                                    If BeProducto.UnidadMedida.Codigo = PDet.Unit_of_Measure_Code Then


                                        '#EJC20220224_0124: Si la implosión automática está activa en la configuración de la interface.
                                        If BeConfigEnc.Explosion_Automatica Then

                                            '#EJC20220224_0125: Buscar la presentación por "defecto", buscar la primera que deberían ser las cajas.
                                            lProductoPresentacion = clsLnProducto_presentacion.Get_All_By_IdProducto_By_IdBodega(BeProducto.IdProducto,
                                                                                                                          True,
                                                                                                                          BeConfigEnc.Idbodega,
                                                                                                                          lConnectionInterface,
                                                                                                                          lTransInterface)

                                            If Not lProductoPresentacion Is Nothing Then

                                                '#EJC20220224: Trabajar con la primera presentación, obtener el factor y determinar si debe 
                                                'ocurrir o no la implosión.
                                                If lProductoPresentacion.Count = 1 Then

                                                    BeProductoPresentacion = lProductoPresentacion(0)

                                                    If Not BeProductoPresentacion Is Nothing Then

                                                        '#EJC20220224_0126: Si la cantidad solicitada es mayor que el factor por presentación
                                                        'es decir: la cantidad excede las unidades por caja...
                                                        If PDet.Quantity > BeProductoPresentacion.Factor Then

                                                            vDeltaFactorPresentacion = Math.Round(PDet.Quantity / BeProductoPresentacion.Factor, 6)

                                                            vCantidadEnteraPresentacion = Math.Truncate(vDeltaFactorPresentacion)

                                                            vCantidadSobranteUnidades = Math.Round(Math.Abs((vCantidadEnteraPresentacion - vDeltaFactorPresentacion) * BeProductoPresentacion.Factor))

                                                            Dim vFactorDeRelacionUnidades As Double = 0

                                                            If vCantidadSobranteUnidades = 0 Then
                                                                PDet.Quantity = vCantidadEnteraPresentacion
                                                                PDet.Quantity_In_UMBas = 0
                                                                PDet.Variant_Code = BeProductoPresentacion.Nombre
                                                            Else
                                                                'PDet.Quantity = vCantidadEnteraPresentacion
                                                                PDet.Quantity = vDeltaFactorPresentacion
                                                                PDet.Quantity_In_UMBas = vCantidadSobranteUnidades
                                                                PDet.Variant_Code = BeProductoPresentacion.Nombre
                                                                '#EJC20220224: Estamos bien jodidos...
                                                                'Porque aquí habría que pedir una línea en caja y el sobrante en unidades...
                                                                Debug.WriteLine("Excepción medio controlada del tipo estamos jodidos.")
                                                            End If

                                                            vExplosionAutomatica = True

                                                        Else
                                                            'La cantidad es menor que una caja, solicitar unidades a WMS.
                                                            vExplosionAutomatica = False
                                                        End If

                                                    End If

                                                Else
                                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error_20220224_0121: La implosión automática está activa, pero se encontró más de una presentación para el producto: {0}, el sistema no puede determinar el factor de implosión.",
                                                                                                     BeProducto.Codigo),
                                                                                                     navPedidoVentaEnc.No,
                                                                                                     BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                                     BeConfigDet.Idnavconfigdet,
                                                                                                     CnnLog)
                                                End If

                                            End If

                                        End If

                                    End If

                                    If TrasladoExistente Is Nothing Then

                                        Try

                                            If Inserta_Linea_Detalle_Pedido(pBePedidoEnc.IdPedidoEnc,
                                                                            PDet,
                                                                            BeProducto,
                                                                            vDiasVencimientoCliente,
                                                                            BeUnidadMedida,
                                                                            BeProductoPresentacion,
                                                                            Nothing,
                                                                            BeConfigEnc,
                                                                            BeConfigEnc.Idbodega,
                                                                            pBePedidoEnc.IdPropietarioBodega,
                                                                            0,
                                                                            lblprg,
                                                                            lBeStockResPedido,
                                                                            lConnectionInterface,
                                                                            CnnLog,
                                                                            lTransInterface) Then

                                            End If

                                            If Not lBeStockResPedido Is Nothing Then

                                                vContador_Lineas_Detalle_Pedido_Insertadas += 1

                                                vInsertoLineaDetalle = True

                                                PDet.Qty_to_Ship = lBeStockResPedido.Sum(Function(x) x.Cantidad)

                                                Lineas_Detalle_Procesadas.Add(PDet)

                                                clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Línea procesa: {0} correctamente para pedido:{1} Código:{2} ", PDet.Line_No, PDet.NoEnc, PDet.No),
                                                                                           PDet.No,
                                                                                           BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                           BeConfigDet.Idnavconfigdet,
                                                                                           CnnLog)

                                                clsPublic.Actualizar_Progreso(lblprg, String.Format("{3} Línea procesa: {0} correctamente para pedido: {1} Código:{2} ", PDet.Line_No, PDet.NoEnc, PDet.No, vbTab))

                                            End If

                                        Catch ex As Exception
                                            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
                                        End Try

                                    Else 'es un pedido existente.

                                        'Si la línea de detalle no existe
                                        If Not clsLnTrans_pe_det.Existe(TrasladoExistente.IdPedidoEnc, PDet.Line_No,
                                                                        pBePedidoDet,
                                                                        PDet.No,
                                                                        lConnectionInterface,
                                                                        lTransInterface) Then

                                            Try

                                                If Inserta_Linea_Detalle_Pedido(pBePedidoEnc.IdPedidoEnc,
                                                                                PDet,
                                                                                BeProducto,
                                                                                vDiasVencimientoCliente,
                                                                                BeUnidadMedida,
                                                                                BeProductoPresentacion,
                                                                                Nothing,
                                                                                BeConfigEnc,
                                                                                BeConfigEnc.Idbodega,
                                                                                pBePedidoEnc.IdPropietarioBodega,
                                                                                0,
                                                                                lblprg,
                                                                                lBeStockResPedido,
                                                                                lConnectionInterface,
                                                                                CnnLog,
                                                                                lTransInterface) Then

                                                End If

                                                If Not lBeStockResPedido Is Nothing Then

                                                    vContador_Lineas_Detalle_Pedido_Insertadas += 1
                                                    vInsertoLineaDetalle = True

                                                    PDet.Qty_to_Ship = lBeStockResPedido.Sum(Function(x) x.Cantidad)

                                                    Lineas_Detalle_Procesadas.Add(PDet)

                                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Línea procesa: {0} correctamente para pedido:{1} (existente) Código:{2} ", PDet.Line_No, PDet.NoEnc, PDet.No),
                                                                                               PDet.No,
                                                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                               BeConfigDet.Idnavconfigdet,
                                                                                               CnnLog)

                                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("{3} Línea procesada:{0} correctamente para pedido:{1} (existente) Código:{2} ", PDet.Line_No, PDet.NoEnc, PDet.No, vbTab))

                                                End If

                                            Catch ex As Exception
                                                Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
                                            End Try

                                        Else

                                            If pBePedidoDet.Cantidad <> PDet.Quantity Then

                                                clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("El pedido: {0} existe,
                                                                                            la línea de detalle: {1} existe, 
                                                                                            cantidad_origen <> cantidad_destino
                                                                                            no se puede actualizar (aún)", PDet.NoEnc, PDet.Line_No),
                                                                                            PDet.No,
                                                                                            BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                            BeConfigDet.Idnavconfigdet,
                                                                                            CnnLog)

                                                clsPublic.Actualizar_Progreso(lblprg, String.Format("El pedido: {0} existe,
                                                                                la línea de detalle: {1} existe, 
                                                                                cantidad_origen <> cantidad_destino
                                                                                no se puede actualizar (aún)", PDet.NoEnc, PDet.Line_No))

                                            Else

                                                clsPublic.Actualizar_Progreso(lblprg, String.Format("El pedido: {0} existe,
                                                                                la línea de detalle: {1} existe, 
                                                                                cantidad_origen = cantidad_destino
                                                                                no se actualizará", PDet.NoEnc, PDet.Line_No))

                                            End If

                                        End If

                                    End If 'fin TrasladoExistente

                                Next

                                Try

                                    '#EJC20180712: No se insertó ninguna línea de detalle del pedido
                                    'Eliminar el encabezado.
                                    If vContador_Lineas_Detalle_Pedido_Insertadas = 0 Then
                                        clsLnTrans_pe_enc.Eliminar_Encabezado_Pedido(pBePedidoEnc.IdPedidoEnc, lConnectionInterface, lTransInterface)
                                        clsPublic.Actualizar_Progreso(lblprg, String.Format("El pedido {0} de traslado no tiene líneas de detalle válidas para el WMS y se eliminará la cabecera: {1}", navPedidoVentaEnc.No, vbNewLine))
                                    End If

                                Catch ex As Exception
                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al eliminar cabecera de pedido de transferencia sin detalle : {0} {1}", ex.Message, vbNewLine))
                                End Try

                            End If

                        End If

                    Else
                        clsPublic.Actualizar_Progreso(lblprg, String.Format("PT Inactivo {0} ", navPedidoVentaEnc.No, vbNewLine))
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
                                                           BeConfigDet.Idnavconfigdet,
                                                           CnnLog)
            End Try

            '#EJC20171105_1259AM_REF01: Agregar excepción a log.
            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet,
                                                       CnnLog)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar pedido de traslado a tabla DE TOMWMS: {0} {1}", ex.Message, vbNewLine))

        Finally

            If lConnectionInterface.State = ConnectionState.Open Then lConnectionInterface.Close()
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
            prg.Value = 0

            Dim difSegundos As Double = DateDiff(DateInterval.Second, BeNavEjecucionEnc.Fecha, Now)
            clsPublic.Actualizar_Progreso(lblprg, vbCrLf)
            clsPublic.Actualizar_Progreso(lblprg, String.Format("Tiempo transcurrido: {0} segundo(s)", difSegundos))

            clsPublic.Actualizar_Progreso(lblprg, vbCrLf)
            clsPublic.Actualizar_Progreso(lblprg, "Fin de procesamiento en TOMWMS.")

        End Try

    End Function

    Public Function Get_Pedidos_Venta_FromWS(ByRef lConnection As SqlConnection,
                                             ByRef lTransaction As SqlTransaction,
                                             Optional ByVal AplicarFiltros As Boolean = True) As List(Of Pedidos)

        Try

            Dim lPedidosTraslado As New List(Of Pedidos)
            Dim lFiltros As New List(Of clsBeI_nav_ent_filtros)

            lFiltros = clsLnI_nav_ent_filtros.Get_All_By_IdNavEnt(clsLnI_nav_ent_filtros.pEntidadesSycn.Pedido_Venta, lConnection, lTransaction)

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

                If vCriteria <> "" AndAlso pBodega <> "" Then
                    If pBodega <> vCriteria Then
                        Throw New Exception(String.Format("La Bodega del filtro: {0} no se corresponde con la Bodega de la interface: {1}", vCriteria, pBodega))
                    End If
                End If

                '#EJC20180426: Cambio transfer_to_code.
                Dim vFiltros As Pedidos_Filter()
                Dim vFiltrosBodegasDestino As New Pedidos_Filter With
                      {.Field = Pedidos_Fields.Location_Code,
                      .Criteria = vCriteria}

                'Importar cantidad enviada y si cantidad enviada > 0 no recibir
                vFiltros = New Pedidos_Filter() {vFiltrosBodegasDestino}

                Dim wsPedidoVentaService As New Pedidos_Service() With
                {
                .UseDefaultCredentials = UsarCredencialesPorDefecto,
                .Credentials = CredencialesConexion,
                .Url = My.MySettings.Default.NavSync_WSPedidosVenta_Pedidos_Service
                }

                fichaPedidosVenta = wsPedidoVentaService.ReadMultiple(vFiltros, Nothing, Nothing)

                For Each PC As Pedidos In fichaPedidosVenta
                    lPedidosTraslado.Add(PC)
                Next

            Else

                fichaPedidosVenta = wsPedidoVentaService.ReadMultiple(Nothing, Nothing, 1000)

                For Each PC As Pedidos In fichaPedidosVenta
                    lPedidosTraslado.Add(PC)
                Next

            End If

            Return lPedidosTraslado

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Insertar_Cliente_Single(ByVal CodigoClienteNAV As String,
                                                   ByRef lConnection As SqlConnection,
                                                   ByRef lTransaction As SqlTransaction,
                                                   ByRef lConnectionLog As SqlConnection,
                                                   ByRef lblprg As RichTextBox,
                                                   ByRef prg As Windows.Forms.ProgressBar) As clsBeCliente_bodega

        Insertar_Cliente_Single = Nothing

        Try

            Dim navCliente As New Lista_clientes()

            Dim lFamilias As New List(Of clsBeProducto_familia)
            Dim lClasificacion As New List(Of clsBeProducto_clasificacion)

            Dim wsClienteService As New Lista_clientes_Service() With
            {
            .UseDefaultCredentials = UsarCredencialesPorDefecto,
            .Credentials = CredencialesConexion
            }

            wsClienteService.Url = My.MySettings.Default.NavSync_WSListaClientes_Lista_clientes_Service
            navCliente = wsClienteService.Read(CodigoClienteNAV)

            Dim BeCliente As New clsBeCliente()
            Dim BeClienteBodega As New clsBeCliente_bodega()

            If Not navCliente Is Nothing Then

                clsPublic.Actualizar_Progreso(lblprg, vbCrLf)
                clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Cliente: {0} ", navCliente.No, vbNewLine))

                BeCliente.IdCliente = clsLnCliente.MaxID(lConnection, lTransaction) + 1
                BeCliente.IdTipoCliente = 1
                BeCliente.IdPropietario = BeConfigEnc.IdPropietario
                BeCliente.Codigo = CodigoClienteNAV
                BeCliente.Nombre_comercial = navCliente.Name
                BeCliente.Nombre_contacto = IIf(navCliente.Contact Is Nothing, navCliente.Name, navCliente.Contact)
                BeCliente.Direccion = navCliente.Address & " " & navCliente.City
                BeCliente.Sistema = True
                BeCliente.Activo = True
                BeCliente.IdEmpresa = BeConfigEnc.Idempresa
                BeCliente.Nit = IIf(navCliente.VAT_Registration_No Is Nothing, CodigoClienteNAV, navCliente.VAT_Registration_No)
                BeCliente.IdTipoCliente = 1
                BeCliente.Es_bodega_recepcion = False
                BeCliente.Es_Bodega_Traslado = False

                Try

                    clsLnCliente.Insertar(BeCliente,
                                          lConnection,
                                          lTransaction)

                    BeClienteBodega = New clsBeCliente_bodega()
                    BeClienteBodega.IdClienteBodega = clsLnCliente_bodega.MaxID(lConnection, lTransaction) + 1
                    BeClienteBodega.IdCliente = BeCliente.IdCliente
                    BeClienteBodega.IdBodega = BeConfigEnc.Idbodega
                    BeClienteBodega.Activo = True
                    BeClienteBodega.User_agr = BeConfigEnc.IdUsuario '1 Esto debería ser parametrizable?
                    BeClienteBodega.User_mod = BeConfigEnc.IdUsuario  '1 Esto debería ser parametrizable?
                    BeClienteBodega.Fec_agr = Now
                    BeClienteBodega.Fec_mod = Now
                    BeClienteBodega.Cliente = BeCliente

                    clsLnCliente_bodega.Insertar_From_Interface(BeClienteBodega,
                                                                lConnection,
                                                                lTransaction)

                    '#EJC202303031646: Insertar días por defecto para clientes.
                    If BeConfigEnc.Dias_Vida_Defecto_Perecederos > 0 Then

                        lFamilias = clsLnProducto_familia.Get_All_Filtro(True,
                                                                         BeConfigEnc.IdPropietario,
                                                                         lConnection,
                                                                         lTransaction)

                        lClasificacion = clsLnProducto_clasificacion.Get_All_Filtro(True,
                                                                                    BeConfigEnc.IdPropietario,
                                                                                    lConnection,
                                                                                    lTransaction)

                        If Not lFamilias Is Nothing AndAlso Not lClasificacion Is Nothing Then

                            Dim BeTiempoCliente As New clsBeCliente_tiempos

                            For Each F In lFamilias

                                For Each C In lClasificacion

                                    BeTiempoCliente = New clsBeCliente_tiempos()
                                    BeTiempoCliente.IdTiempoCliente = clsLnCliente_tiempos.MaxID(lConnection, lTransaction) + 1
                                    BeTiempoCliente.IdCliente = BeCliente.IdCliente
                                    BeTiempoCliente.IdFamilia = F.IdFamilia
                                    BeTiempoCliente.IdClasificacion = C.IdClasificacion
                                    BeTiempoCliente.Dias_Local = BeConfigEnc.Dias_Vida_Defecto_Perecederos
                                    BeTiempoCliente.Dias_Exterior = BeConfigEnc.Dias_Vida_Defecto_Perecederos
                                    BeTiempoCliente.User_agr = BeConfigEnc.IdUsuario
                                    BeTiempoCliente.User_mod = BeConfigEnc.IdUsuario
                                    BeTiempoCliente.Fec_agr = Now
                                    BeTiempoCliente.Fec_mod = Now
                                    BeTiempoCliente.Activo = True
                                    clsLnCliente_tiempos.Insertar(BeTiempoCliente, lConnection, lTransaction)

                                Next

                            Next

                        End If

                    End If

                    Return BeClienteBodega

                Catch ex As Exception

                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                               BeCliente.Codigo,
                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                               BeConfigDet.Idnavconfigdet, lConnectionLog)

                    clsPublic.Actualizar_Progreso(lblprg, vbCrLf)
                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar proveedor: {0}{1}{2}", BeCliente.Codigo, vbNewLine, ex.Message))

                End Try

            End If

        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar Proveedor a tabla DE TOMWMS: {0}", ex.Message))
            Throw ex
        End Try

    End Function

    '#CKFK20230113 Renombré la función de arriba que es la que originalmente se utilizaba y creé esta que es una copia de la que esta en la clase clsLnI_nav_ped_traslado_enc
    'y que es la función que llama a clsLnTrans_pe_det.Reservar_Stock_Por_Linea_Interface que a su vez llama a clsLnStock_res.Reserva_Stock_From_MI3 
    'solo le agregué el stockres para que lo devuelva
    Private Shared Function Inserta_Linea_Detalle_Pedido(ByVal pIdPedidoEnc As Integer,
                                                         ByRef pBeTrasladoDet As clsBeI_nav_ped_traslado_det,
                                                         ByVal pBePoducto As clsBeProducto,
                                                         ByVal pDiasVencimientoCliente As Integer,
                                                         ByVal pBeUnidadMedida As clsBeUnidad_medida,
                                                         ByVal pBePresentacion As clsBeProducto_Presentacion,
                                                         ByVal pBeCliente As clsBeCliente,
                                                         ByVal pBeConfigEnc As clsBeI_nav_config_enc,
                                                         ByVal pIdBodegaOrigen As Integer,
                                                         ByVal pIdPropietarioBodega As Integer,
                                                         ByVal pIdejecucionenc As Integer,
                                                         ByRef plblprg As RichTextBox,
                                                         ByRef pListStockResOUT As List(Of clsBeStock_res),
                                                         ByRef lConectionInterface As SqlConnection,
                                                         ByRef CnnLog As SqlConnection,
                                                         ByRef lTransactionInterface As SqlTransaction) As Boolean

        Inserta_Linea_Detalle_Pedido = False

        Dim pBePedidoDet As New clsBeTrans_pe_det
        Dim pBeStockRes As New clsBeStock_res
        Dim IdNavConfigDet As Integer = 102 'Pedidos de clientes
        Dim IdxPresentacion As Integer = -1

        Try

            pBePedidoDet = New clsBeTrans_pe_det
            pBePedidoDet.IdPedidoDet = clsLnTrans_pe_det.MaxID(lConectionInterface, lTransactionInterface) + 1
            pBePedidoDet.No_linea = pBeTrasladoDet.Line_No
            pBePedidoDet.Atributo_Variante_1 = pBeTrasladoDet.Variant_Code
            pBePedidoDet.IdPedidoEnc = pIdPedidoEnc
            pBePedidoDet.Producto = New clsBeProducto
            pBePedidoDet.Producto.IdProducto = clsLnProducto.Get_Id_Producto_By_IdProductoBodega(pBePoducto.IdProductoBodega,
                                                                                                 lConectionInterface,
                                                                                                 lTransactionInterface)
            pBePedidoDet.Producto.IdProductoBodega = pBePoducto.IdProductoBodega
            pBePedidoDet.IdProductoBodega = pBePoducto.IdProductoBodega
            pBePedidoDet.Codigo_Producto = pBeTrasladoDet.No
            pBePedidoDet.Producto.Codigo = pBeTrasladoDet.No
            '#EJC20220622:Quitar caractéres no permitidos.
            pBePedidoDet.Producto.Nombre = clsPublic.Quitar_Caracteres_No_Permitidos(pBeTrasladoDet.Description)
            pBePedidoDet.Nombre_producto = clsPublic.Quitar_Caracteres_No_Permitidos(pBeTrasladoDet.Description)
            pBePedidoDet.IdUnidadMedidaBasica = pBeUnidadMedida.IdUnidadMedida
            pBePedidoDet.Cantidad = pBeTrasladoDet.Quantity
            pBePedidoDet.Peso = 0
            pBePedidoDet.Precio = pBeTrasladoDet.Price
            pBePedidoDet.No_recepcion = 0
            pBePedidoDet.Cant_despachada = 0
            pBePedidoDet.IdEstado = pBeConfigEnc.IdProductoEstado
            pBePedidoDet.Ndias = pDiasVencimientoCliente
            pBePedidoDet.Nom_estado = "Buen Estado"
            pBePedidoDet.IsNew = True
            pBePedidoDet.Fec_agr = Now
            pBePedidoDet.User_agr = pBeConfigEnc.IdUsuario
            pBePedidoDet.RoadDes = 0
            pBePedidoDet.RoadDesMon = 0
            pBePedidoDet.RoadPrecioDoc = pBeTrasladoDet.Price
            pBePedidoDet.RoadTotal = Math.Round(pBeTrasladoDet.Price * pBeTrasladoDet.Quantity, 6)
            pBePedidoDet.RoadVAL1 = 0
            pBePedidoDet.RoadVAL2 = 0

            If Not pBeTrasladoDet.Variant_Code = "" Then
                If Not pBePresentacion Is Nothing Then
                    If pBePresentacion.IdPresentacion <> 0 Then
                        pBePedidoDet.Nom_presentacion = pBePresentacion.Nombre
                        pBePedidoDet.IdPresentacion = pBePresentacion.IdPresentacion
                        pBePedidoDet.Factor = pBePresentacion.Factor
                    Else
                        pBePedidoDet.Nom_presentacion = ""
                    End If
                End If
            Else
                pBePedidoDet.Nom_presentacion = ""
            End If

            pBePedidoDet.Nom_unid_med = pBeTrasladoDet.Unit_of_Measure_Code
            pBePedidoDet.Nom_estado = "Buen Estado"

            pBeStockRes.IdStockRes = 0
            pBeStockRes.IdTransaccion = pIdPedidoEnc
            pBeStockRes.IdPedidoDet = pBePedidoDet.IdPedidoDet
            pBeStockRes.Indicador = "PED"
            pBeStockRes.añada = 0
            pBeStockRes.Cantidad = pBeTrasladoDet.Quantity
            pBeStockRes.Estado = "PPC"
            pBePedidoDet.Ndias = pDiasVencimientoCliente
            pBeStockRes.User_agr = pBeConfigEnc.IdUsuario
            pBeStockRes.Fec_agr = Now
            pBeStockRes.User_mod = pBeConfigEnc.IdUsuario
            pBeStockRes.Fec_mod = Now
            pBeStockRes.Host = pBeTrasladoDet.Source_ID
            pBeStockRes.Serial = pBeTrasladoDet.Line_No

            Dim BeProductoEstadoList As New List(Of clsBeProducto_estado)

            Dim vIdPropietario As Integer = clsLnPropietario_bodega.Get_IdPropietario_By_IdBodega_IdPropietarioBodega(pIdBodegaOrigen,
                                                                                                                      pIdPropietarioBodega,
                                                                                                                      lConectionInterface,
                                                                                                                      lTransactionInterface)

            Try

                '#EJC202220620:Buscar el estado de producto de la interface.
                Dim vIdEstadoProductoInterface As Integer = pBeConfigEnc.IdProductoEstado

                BeProductoEstadoList = clsLnProducto_estado.Existe_IdEstado_By_IdPropietario(vIdPropietario,
                                                                                             vIdEstadoProductoInterface,
                                                                                             lConectionInterface,
                                                                                             lTransactionInterface)

                If Not BeProductoEstadoList Is Nothing Then

                    If Not BeProductoEstadoList.FirstOrDefault() Is Nothing Then
                        pBeStockRes.IdProductoEstado = BeProductoEstadoList.FirstOrDefault.IdEstado()
                    Else
                        Throw New Exception("ERR_202205121200A: Error al obtener el estado de producto por defecto para los parámetros IdPropietario: " & pIdPropietarioBodega & " and IdBodega: " & pIdBodegaOrigen)
                    End If

                Else
                    Throw New Exception("ERR_202205121200B: Error al obtener el estado de producto por defecto para los parámetros IdPropietario: " & pIdPropietarioBodega & " and IdBodega: " & pIdBodegaOrigen)
                End If

            Catch ex As Exception
                Throw New Exception("ERES_TU: " & ex.Message)
            End Try

            pBeStockRes.IdPedido = pIdPedidoEnc
            pBeStockRes.IdPedidoDet = pBePedidoDet.IdPedidoDet
            pBeStockRes.IdProductoBodega = pBePoducto.IdProductoBodega
            '#EJC20220512: 'clsLnPropietarios.Get_IdPropietarioBodega_By_IdBodega_And_IdPropietario(pIdBodega,pIdPropietarioBodega,lConectionInterface,lTransactionInterface)
            pBeStockRes.IdPropietarioBodega = pIdPropietarioBodega
            pBeStockRes.IdUnidadMedida = clsLnProducto.Get_Id_Unidad_Medida_By_Codigo(pBePedidoDet.Producto.Codigo,
                                                                                      lConectionInterface,
                                                                                      lTransactionInterface)
            pBeStockRes.Atributo_Variante_1 = pBePedidoDet.Atributo_Variante_1

            '#EJC20190314: Asignar control ultimo lote a objeto de reserva.
            If Not pBeCliente Is Nothing Then
                pBeStockRes.Control_Ultimo_Lote = pBeCliente.Control_Ultimo_Lote
            Else
                pBeStockRes.Control_Ultimo_Lote = False
            End If

            Dim BePresentacion As New clsBeProducto_Presentacion

            If pBePedidoDet.IdPresentacion <> 0 Then

                If Not pBePedidoDet.Atributo_Variante_1 Is Nothing Then

                    If Not pBePedidoDet.Atributo_Variante_1 = "" Then

                        BePresentacion = New clsBeProducto_Presentacion
                        BePresentacion = clsLnProducto_presentacion.Existe_Presentacion_By_Codigo(pBePedidoDet.Producto.IdProducto,
                                                                                                  pBePedidoDet.Atributo_Variante_1,
                                                                                                  lConectionInterface,
                                                                                                  lTransactionInterface)

                        If Not BePresentacion Is Nothing Then
                            pBeStockRes.IdPresentacion = BePresentacion.IdPresentacion
                        Else

                            '#EJC202210181952: Buscar por código de barra (BYB)
                            BePresentacion = clsLnProducto_presentacion.Existe_Presentacion_By_IdProducto_And_CodigoBarra(pBePedidoDet.Producto.IdProducto,
                                                                                                                          pBePedidoDet.Codigo_Producto,
                                                                                                                          lConectionInterface,
                                                                                                                          lTransactionInterface)

                            If Not BePresentacion Is Nothing Then
                                pBeStockRes.IdPresentacion = BePresentacion.IdPresentacion
                            Else
                                pBeStockRes.IdPresentacion = 0 'No se encontró la presentación solicitada
                            End If

                        End If

                    Else
                        pBeStockRes.IdPresentacion = 0 'La solicitud es en UMBAS.
                    End If


                Else
                    pBeStockRes.IdPresentacion = 0 'No se encontró la presentación solicitada
                End If

            End If

            If pBeStockRes.Control_Ultimo_Lote Then
                '#EJC20190314: Capturar último lote despachado para evitar enviar el mismo.
                pBeStockRes.Ultimo_Lote = clsLnVW_Despacho_Rep.Get_Ultimo_Lote_By_IdCliente(pBeCliente.IdCliente,
                                                                                            pBePedidoDet.Producto.IdProducto)
            End If

            If Not pBeCliente Is Nothing Then
                '#EJC20220712_0853:Asignar la ubicación con la que se va a abastecer el pedido de un determinado cliente.
                'MI3: (Quedaría pendiente validar si la ubicación que trae es válida, pero eso que lo haga otro... que esté viendo mi pantalla.
                If Val(pBeCliente.IdUbicacionAbastecerCon) <> 0 Then
                    pBeStockRes.IdUbicacionAbastecerCon = pBeCliente.IdUbicacionAbastecerCon
                Else
                    pBeStockRes.IdUbicacionAbastecerCon = 0
                End If
            Else
                pBeStockRes.IdUbicacionAbastecerCon = 0
            End If


            Try
                '#CKFK20221012 Agregué la función que devuelve el stock reservado
                If clsLnTrans_pe_det.Reservar_Stock_Por_Linea_Interface(pDiasVencimientoCliente,
                                                                        pBeTrasladoDet,
                                                                        pBePedidoDet,
                                                                        pBeStockRes,
                                                                        pBeTrasladoDet.Source_ID,
                                                                        pBeConfigEnc,
                                                                        pIdPropietarioBodega,
                                                                        pListStockResOUT,
                                                                        plblprg,
                                                                        lConectionInterface,
                                                                        lTransactionInterface) Then
                    Inserta_Linea_Detalle_Pedido = True

                    pBeTrasladoDet.Process_Result = "Ok"
                    clsLnI_nav_ped_traslado_det.Actualizar_Process_Result(pBeTrasladoDet,
                                                                          lConectionInterface,
                                                                          lTransactionInterface)

                Else

                    pBeTrasladoDet.Process_Result = String.Format("Error en Reservar_Stock_Por_Linea para el pedido: {0} línea: {1} Código_Producto: {3} U.M.: {4} V.C.: {5} Descripción del error: {2} Cantidad: {6}. ", pBeTrasladoDet.NoEnc,
                                                                                        pBeTrasladoDet.Line_No,
                                                                                        "No pudo reservar el producto",
                                                                                        pBeTrasladoDet.Item_No,
                                                                                        pBeTrasladoDet.Unit_of_Measure_Code,
                                                                                        pBeTrasladoDet.Variant_Code,
                                                                                        pBeTrasladoDet.Quantity)

                    clsLnI_nav_ped_traslado_det.Actualizar_Process_Result(pBeTrasladoDet,
                                                                          lConectionInterface,
                                                                          lTransactionInterface)
                End If

            Catch ex As Exception

                Dim vMensajeEx As String = String.Format("Error en Reservar_Stock_Por_Linea para el pedido: {0} línea: {1} Código_Producto: {3} U.M.: {4} V.C.: {5} Descripción del error: {2} Cantidad: {6}", pBeTrasladoDet.NoEnc,
                                                                                        pBeTrasladoDet.Line_No,
                                                                                        ex.Message,
                                                                                        pBeTrasladoDet.Item_No,
                                                                                        pBeTrasladoDet.Unit_of_Measure_Code,
                                                                                        pBeTrasladoDet.Variant_Code,
                                                                                        pBeTrasladoDet.Quantity)

                clsLnI_nav_ejecucion_det_error.Inserta_Log(vMensajeEx,
                                                           pBeTrasladoDet.No,
                                                           pIdejecucionenc,
                                                           IdNavConfigDet,
                                                           CnnLog)

                pBeTrasladoDet.Process_Result = vMensajeEx

                clsLnI_nav_ped_traslado_det.Actualizar_Process_Result(pBeTrasladoDet,
                                                                      lConectionInterface,
                                                                      lTransactionInterface)


                clsPublic.Actualizar_Progreso(plblprg, vMensajeEx)

                If pBeConfigEnc.Rechazar_pedido_incompleto Then
                    Throw New Exception(vMensajeEx)
                End If

            End Try

        Catch ex As Exception
            Dim st As New StackTrace(True)
            st = New StackTrace(ex, True)
            Dim vMsgError As String = String.Format("ERR_RELAY_202303011324A: {0} {1} - Línea: {2}", MethodBase.GetCurrentMethod.Name(), ex.Message, st.GetFrame(0).GetFileLineNumber().ToString)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Sub Enviar_Transacciones_De_Salida(ByRef lblprg As RichTextBox,
                                              ByRef prg As Windows.Forms.ProgressBar)

        Dim lTransaccionesSalida As New List(Of clsBeI_nav_transacciones_out)
        Dim lTransaccionesSalidaReproceso As New List(Of clsBeI_nav_transacciones_out)
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        '#EJC20180614: Tratar de registrar pedidos de transferencia que no se registraron en NAV.
        Dim PedidoVenta As New Pedidos
        Dim Sl As New clsSyncLotes()
        Dim lLotes As New List(Of WSPaginaLotes.Pagina_lotes)
        Dim LoteEnviado As New WSPaginaLotes.Pagina_lotes
        Dim vContadorReproceso As Integer = 0

        Try

            CnnLog.Open()

            lTransaccionesSalida = clsLnI_nav_transacciones_out.Get_Lotes_Salida_Pendientes_Envio_By_IdTipoDocumento(clsDataContractDI.tTipoDocumentoSalida.Pedido_De_Venta_NAV)

            Dim wsPedidoVentaService As New Pedidos_Service() With
                {
                .UseDefaultCredentials = UsarCredencialesPorDefecto,
                .Credentials = CredencialesConexion
                }

            wsPedidoVentaService.Url = My.Settings.NavSync_WSPedidosVenta_Pedidos_Service

            If Not lTransaccionesSalida Is Nothing AndAlso lTransaccionesSalida.Count > 0 Then

                clsPublic.Actualizar_Progreso(lblprg, String.Format("Transacciones a enviar: {0}", lTransaccionesSalida.Count))

                '#CKFK 20211121 Agregué el campo IdTipoDocumento
                Dim ListaPedidosTransf = (From i In lTransaccionesSalida
                                          Group i By Keys = New With {Key i.No_pedido, Key i.Idpedidoenc, Key i.Codigo_Bodega_Origen, i.IdTipoDocumento} Into Group
                                          Select New With {Key Keys.No_pedido, Key Keys.Idpedidoenc, Key Keys.Codigo_Bodega_Origen, Key Keys.IdTipoDocumento})
                Dim Enviado_A_Erp As Boolean = False '#CKFK 20180518 10:20 PM Agregué la validación de si el registro ya fue enviado o no a NAV

                For Each PT In ListaPedidosTransf

                    Enviado_A_Erp = clsLnTrans_pe_enc.Get_Estado_Enviado_A_ERP(PT.No_pedido)

                    If Not Enviado_A_Erp Then

                        Try


                            If Enviar_Lotes_PV(PT.No_pedido,
                                               lTransaccionesSalida,
                                               lblprg,
                                               prg) Then

                                If Enviar_Cantidades_Pedido_Venta(PT.No_pedido,
                                                                  lTransaccionesSalida,
                                                                  lblprg,
                                                                  prg) Then

                                    '#EJC20220411: Ricardo indicó que el registro no es necesario,
                                    'Glenda debe hacerlo en un determinado momento.
                                    'wsPedidoVentaService.RegistrarEnvTransfer(PT.No_pedido)

                                End If

                            End If

                            clsPublic.Actualizar_Progreso(lblprg, String.Format("registros de pedido de venta enviados correctamente: {0}", lTransaccionesSalida.Count))

                            clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP(PT.Idpedidoenc, True, BeConfigEnc.IdUsuario)

                        Catch ex As Exception

                            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al registrar el pedido:{0} en el ERP. Error: {1}", PT.No_pedido, ex.Message))

                            clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al registrar el pedido:{0} en el ERP. Error: {1}", PT.No_pedido, ex.Message),
                                                                       PT.No_pedido,
                                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                                       BeConfigDet.Idnavconfigdet,
                                                                       CnnLog)

                        End Try

                    Else

                        Try

                            PedidoVenta = wsPedidoVentaService.Read(PT.No_pedido)

                            If Not PedidoVenta Is Nothing Then

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
                                        If Enviar_Lotes_PV(PT.No_pedido, lTransaccionesSalidaReproceso, lblprg, prg) Then
                                            If Enviar_Cantidades_Pedido_Venta(PT.No_pedido, lTransaccionesSalidaReproceso, lblprg, prg) Then
                                                'El registro no es necesario, leer: #EJC20180614 (arriba)  ;)
                                            End If
                                        End If
                                    End If

                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Se registró lote (extraviado en conmutación) para el pedido de venta:{0} correctamente en el ERP.", PT.No_pedido))

                                    clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP(PT.Idpedidoenc, True, BeConfigEnc.IdUsuario)

                                End If


                            Else

                                If Not Enviado_A_Erp Then

                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Se registró el pedido de venta:{0} correctamente en el ERP.", PT.No_pedido))

                                    '#EJC20180614: El pedido ya se recibió en la bodega destino.
                                    clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP(PT.Idpedidoenc, True, BeConfigEnc.IdUsuario)

                                End If

                            End If

                        Catch ex As Exception

                            If ex.Message = "There is nothing to post." Then 'Pedido sin nada que registrar

                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Nada que registrar para pedido: {0} en NAV.", PT.No_pedido))

                                clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP(PT.Idpedidoenc, True, BeConfigEnc.IdUsuario)

                            Else

                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al registrar el pedido:{0} en el ERP. Error: {1}", PT.No_pedido, ex.Message))

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
                clsPublic.Actualizar_Progreso(lblprg, "No hay registros pendientes de envío en i_nav_transacciones_out")
            End If

        Catch ex As Exception
            Throw ex
        Finally
            prg.Value = 0
            prg.Visible = False
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
        End Try

    End Sub

    Private Function Enviar_Lotes_PV(ByVal NoPedidoTransf As String,
                                    ByRef lTransaccionesSalida As List(Of clsBeI_nav_transacciones_out),
                                    ByRef lblprg As RichTextBox,
                                    ByRef prg As System.Windows.Forms.ProgressBar) As Boolean

        Enviar_Lotes_PV = False

        Try

            prg.Maximum = lTransaccionesSalida.Count
            prg.Visible = True

            Dim vContador As Integer = 0
            Dim BePresentacion As New clsBeProducto_Presentacion()
            Dim vCantidad As Double = 0
            Dim vUnidMed As String = ""

            Dim listaLotes = lTransaccionesSalida.Where(Function(x) x.No_pedido = NoPedidoTransf AndAlso x.Enviado = False).ToList()

            If Not listaLotes Is Nothing Then

                wsCUWMS.Url = My.MySettings.Default.NavSync_CUWMS_CUWMS

                For Each I In listaLotes

                    Try

                        If Not I.Idpresentacion = 0 Then

                            '#EJC20220411:Ricardo indica que los lotes van en cantidad base.
                            '#EJC20180418: Enviar la cantidad en UMBAS.
                            'BePresentacion.IdPresentacion = I.Idpresentacion
                            'clsLnProducto_presentacion.GetSingle(BePresentacion)
                            'vCantidad = Math.Round(I.Cantidad / BePresentacion.Factor, 5)

                            ''Enviar a Nav el nombre/codigo de la unidad de medida básica.
                            'vUnidMed = clsLnUnidad_medida.Get_Nombre_By_IdUnidadMedida(I.Idunidadmedida)

                            vCantidad = I.Cantidad

                        Else
                            vCantidad = I.Cantidad
                            vUnidMed = I.Unidad_medida
                        End If

                        '#EJC20180803: Se redondean los decimales hasta que se envian los datos a NAV
                        'porque NAV solo soporta 5 decimales, el sistema actualmente trabaja con 6
                        'pero en el futuro, deben existir dos parámetros para el redonde de decimales.                        

                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Lote de Pedido: {0} Línea:{1} Código:{2} Cantidad:{3} Lote:{4} ", I.No_pedido, I.No_linea, I.Codigo_producto, vCantidad, I.Lote))

                        '#EJC20180503: Enviar siempre UMBAS en Enviar_Lotes_Ingreso.
                        wsCUWMS.LoteLineaPedidoVenta(I.No_pedido,
                                                     I.No_linea,
                                                     I.Codigo_producto,
                                                     "",
                                                     vUnidMed,
                                                     Math.Round(vCantidad, 5),
                                                     I.Lote,
                                                     I.Fecha_vence)

                        vContador += 1

                        prg.Value = vContador

                        I.Enviado = True

                    Catch ex As Exception

                        '#EJC20171105_1259AM_REF01: Agregar excepción a log.
                        clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al ejecutar codeunit de NAV LoteLineaPedidoVenta: {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                                          "",
                                                          BeNavEjecucionEnc.IdEjecucionEnc,
                                                          BeConfigDet.Idnavconfigdet)

                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al ejecutar codeunit de NAV LoteLineaPedidoVenta: {0} {1}", ex.Message, vbNewLine))

                    End Try

                Next

            End If

            Enviar_Lotes_PV = (listaLotes.Count = vContador)

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Function Enviar_Cantidades_Pedido_Venta(ByVal NoPedidoTransf As String,
                                                    ByRef lTransaccionesSalida As List(Of clsBeI_nav_transacciones_out),
                                                    ByRef lblprg As RichTextBox,
                                                    ByRef prg As System.Windows.Forms.ProgressBar) As Boolean

        Enviar_Cantidades_Pedido_Venta = False

        Try

            prg.Maximum = lTransaccionesSalida.Count
            prg.Visible = True

            Dim vContador As Integer = 0
            Dim vCantidad As Double = 0
            Dim BePresentacion As New clsBeProducto_Presentacion()
            Dim vUnidMed As String = ""

            For Each T In lTransaccionesSalida.Where(Function(x) x.No_pedido = NoPedidoTransf AndAlso x.Enviado = False)
                T.Enviado = True
            Next

            Dim ListaResumen = (From i In lTransaccionesSalida.Where(Function(x) x.No_pedido = NoPedidoTransf)
                                Group i By Keys = New With {Key i.No_pedido, Key i.No_linea,
                                Key i.Codigo_producto, Key i.Codigo_variante, Key i.Enviado, Key i.Idpresentacion} Into Group
                                Select New With {Keys.No_pedido, Keys.No_linea,
                                                 Keys.Codigo_producto,
                                                 Keys.Codigo_variante,
                                                 Keys.Enviado,
                                                 Keys.Idpresentacion,
                                                 .Cantidad = Group.Sum(Function(x) x.Cantidad)})

            Dim lista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)

            For Each I In ListaResumen

                Try

                    '#EJC20180803: Se redondean los decimales hasta que se envian los datos a NAV
                    'porque NAV solo soporta 5 decimales, el sistema actualmente trabaja con 6
                    'pero en el futuro, deben existir dos parámetros para el redondeo de decimales.                    

                    If Not I.Idpresentacion = 0 Then

                        '#EJC20180418: Enviar la cantidad en UMBAS.
                        BePresentacion.IdPresentacion = I.Idpresentacion
                        clsLnProducto_presentacion.GetSingle(BePresentacion)
                        vCantidad = Math.Round(I.Cantidad / BePresentacion.Factor, 5)

                    Else
                        vCantidad = I.Cantidad
                    End If

                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando cantidad pedido de venta: {0} Línea:{1} Código:{2} Cantidad:{3} ", I.No_pedido, I.No_linea, I.Codigo_producto, vCantidad))

                    wsCUWMS.CantLineaPedidoVenta(I.No_pedido,
                                                 I.No_linea,
                                                 I.Codigo_producto,
                                                 Math.Round(vCantidad, 5))

                    lista_A_Actualizar = lTransaccionesSalida.Where(Function(x) x.No_pedido = I.No_pedido _
                                                                    AndAlso x.No_linea = I.No_linea _
                                                                    AndAlso x.Codigo_producto = I.Codigo_producto _
                                                                    AndAlso x.Codigo_variante = I.Codigo_variante _
                                                                    AndAlso x.Enviado = True).ToList()

                    If Not lista_A_Actualizar Is Nothing Then

                        For Each T In lista_A_Actualizar
                            clsLnI_nav_transacciones_out.Actualizar_Bandera_Enviado(T)
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

                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al enviar lote de pedido de transf. a Nav desde WS: {0} {1}", ex.Message, vbNewLine))

                End Try

            Next

            '#CKFK 20180526 07:08 PM agregué esta línea Enviar_Cantidades_Transf = True porque la función siempre devolvía False
            Enviar_Cantidades_Pedido_Venta = True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

End Class