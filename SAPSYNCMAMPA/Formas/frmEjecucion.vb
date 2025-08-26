Imports System.Net
Imports System.Net.Security
Imports System.Reflection
Imports System.Security.Cryptography
Imports System.Security.Cryptography.X509Certificates
Imports DevExpress.XtraBars
Imports DevExpress.XtraBars.Ribbon
Imports DevExpress.XtraEditors
Imports TOMWMS.clsDataContractDI

Public Class frmEjecucion
    Public Property Interface_A_Ejecutar As Integer = -1
    Public procesoEnEjecucion As Boolean = False

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Async Sub Ejecuta_Interface_Bodegas(Optional ByVal Preguntar As Boolean = True)

        Try

            prg.Visible = True

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Actualizar registros?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                Await clsSyncSAPBodega.Insertar_Bodegas_Desde_Tabla_Intermedia_A_Tabla_TOMWMS(lblprg, prg, True, Preguntar)
            End If

        Catch ex As Exception
            Dim vMensaje As String = ex.Message
            clsPublic.Actualizar_Progreso(lblprg, vMensaje)
        Finally
            prg.Visible = False
        End Try

    End Sub

    Public Sub Ejecuta_Interface_Proveedores(Optional ByVal Preguntar As Boolean = True)

        Try

            prg.Visible = True

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Actualizar registros?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                clsSyncSAPProveedor.Insertar_Proveedores_Desde_TablaIntermedia_A_Tabla_TOMWMS(lblprg, prg, True, Preguntar)
            End If

        Catch ex As Exception

            Dim vMensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsPublic.Actualizar_Progreso(lblprg, vMensaje)
            prg.Visible = False

        End Try

    End Sub

    Private Sub mnuActualizarProveedores_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizarProveedores.ItemClick
        Ejecuta_Interface_Proveedores(True)
    End Sub

    Private tiempoProductos As Double = 0
    Dim frecuencia As Double = 0

    Private Sub timerProducto_Tick(sender As Object, e As EventArgs) Handles timerProducto.Tick
        If (frecuencia * 3) = tiempoProductos AndAlso Not BwProducto.IsBusy Then
            BwProducto.RunWorkerAsync()
            tiempoProductos = 0
            timerProducto.Enabled = False
        Else
            tiempoProductos += 1
        End If
    End Sub

    Private Sub BwProducto_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BwProducto.RunWorkerCompleted
        timerProducto.Enabled = True
    End Sub

    Private Sub BarButtonItem1_ItemClick_1(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReporteEjecuciones.ItemClick

        With frmEjecuciones
            .Modo = frmEjecuciones.pModo.Lista
            .WindowState = FormWindowState.Normal
            .Show()
            .Focus()
        End With

    End Sub
    Private Sub frmEjecucion_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            Application.DoEvents()

            Me.Text = "MI3_SYNC ->TOM, WMS (" & gVersionApp & " " & FormatoFechas.tFecha(gFechaVersion) & ")"

            'clsPublic.Actualizar_Progreso(lblprg, "Interface TOMWMS - SAP")
            'clsPublic.Actualizar_Progreso(lblprg, "Ejecución remota iniciada con parámetro: " & Interface_A_Ejecutar.ToString)

            If Interface_A_Ejecutar <> -1 Then

                mnuActualizarProveedores.Visibility = BarItemVisibility.Never
                mnuProductosI.Visibility = BarItemVisibility.Never
                mnuImprimir.Visibility = BarItemVisibility.Never
                mnuReporteEjecuciones.Visibility = BarItemVisibility.Never

            End If

            '#EJC20240709: Permisos por rol en interface parte 1.
            Try

                'DesactivarMenu()

                BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface)

                gConnectionStringSAPHana = "Server=" & BD.Instancia.SERVER_BD_SAP &
                                         " ; UserID=" & BD.Instancia.SAP_DB_USR &
                                         "; Password=" & BD.Instancia.SAP_DB_PW

                Dim BeUsuarioBodega As New clsBeUsuario_bodega
                If IdUsuario = -1 Then

                    If BeConfigEnc IsNot Nothing Then
                        IdUsuario = BeConfigEnc.IdUsuario
                    End If

                End If

                BeUsuarioBodega = clsLnUsuario_bodega.Get_Single_By_IdUsuario_And_IdBodega(IdUsuario, BeConfigEnc.Idbodega)

                If Not BeUsuarioBodega Is Nothing Then
                    '#CKFK Puse en comentario el habilitarmenurol
                    'HabilitarMenuRol(BeUsuarioBodega.IdRol, rbMain)
                End If

            Catch ex As Exception
                Dim vMensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
                clsPublic.Actualizar_Progreso(lblprg, vMensaje)
                prg.Visible = False
            End Try

            Select Case Interface_A_Ejecutar

                Case pInterfaceAEjecutar.Importar_Productos
                    mnuProductosI.Visibility = BarItemVisibility.Always
                Case pInterfaceAEjecutar.Importar_Pedidos_De_Compra
                    mnuProductosI.Visibility = BarItemVisibility.Always
                Case pInterfaceAEjecutar.Importar_Pedidos_De_Transferencia
                Case pInterfaceAEjecutar.Enviar_Pedidos_Compra
                    mnuEnviarPedidosCompra.Visibility = BarItemVisibility.Always
                Case pInterfaceAEjecutar.Enviar_Pedidos_Transferencia
                    mnuEnviarPedidosTransferencia.Visibility = BarItemVisibility.Always
                Case pInterfaceAEjecutar.Actualizar_Pedido_Cliente_No_Enviado
                    If Actualizar_Estado_Pedido_Cliente(NoDocEntrySAP, EstadoEnviadoSAP) Then
                        Close()
                    End If
                Case pInterfaceAEjecutar.Cerrar_Documento_Salida_SAP
                    If Cerrar_Solicitud_Traslado(NoDocEntrySAP) Then
                        Close()
                    End If
                Case Else
                    Exit Select

            End Select


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub cmdRptTransac_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdRptTransac.ItemClick

        Try

            With frmRegistrosInterface
                .Modo = frmRegistrosInterface.pModo.Lista
                .WindowState = FormWindowState.Maximized
                .Show()
                .Focus()
            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub Args_Showing(ByVal sender As Object, ByVal e As XtraMessageShowingArgs)
        e.Form.Icon = Me.Icon
    End Sub

    Private Sub mnuEnvios_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)

        Dim args As New XtraInputBoxArgs()
        Dim tmpResult As Object
        Dim pPedidoCliente As String = ""

        Try

            args.Caption = "Ingrese pedido de cliente"
            args.Prompt = "Pedido No."

            Dim editor As New TextEdit
            args.Editor = editor

            args.DefaultButtonIndex = 0
            args.DefaultResponse = ""
            AddHandler args.Showing, AddressOf Args_Showing

            tmpResult = XtraInputBox.Show(args)

            If Not tmpResult Is Nothing Then

                pPedidoCliente = tmpResult.ToString

                Ejecuta_interface_Pedido_Cliente(True, pPedidoCliente)

            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Public Sub Ejecuta_interface_Devolucion_Mercancia(Optional ByVal Preguntar As Boolean = True)

        Dim MostrarMensaje As Boolean = False

        Try

            prg.Visible = True

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Importar devolucion de mercancías?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                clsSyncSAPDevolucionMercancia.Insertar_Solicitud_Devol_Cli_A_TOMWMS(lblprg, prg, True, Preguntar)
            End If

        Catch ex As Exception
            If MostrarMensaje Then
                XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try

    End Sub

    Private Sub mnuBodegas_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuBodegas.ItemClick
        Ejecuta_Interface_Bodegas(True)
    End Sub

    Private Sub mnuDevolucionMercancia_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        Ejecuta_interface_Devolucion_Mercancia(True)
    End Sub
    Private Sub BarButtonItem4_ItemClick_2(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        Enviar_Documentos_Salida(True)
    End Sub

    Public Async Sub Ejecuta_interface_Pedido_Cliente(Optional ByVal Preguntar As Boolean = True,
                                                      Optional ByVal pPedidoCliente As String = "")

        Dim MostrarMensaje As Boolean = False

        Try

            prg.Visible = True

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Importar pedido de cliente?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                Await clsSyncSAPSPedidoCliente.Importar_Pedido_Cliente_SAP(lblprg, prg, True, Preguntar, pPedidoCliente)
            End If

        Catch ex As Exception
            If MostrarMensaje Then
                XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                clsPublic.Actualizar_Progreso(lblprg, "Error: " & ex.Message)
            End If
        End Try

    End Sub

    Private Sub Enviar_Documentos_Salida(Optional ByVal Preguntar As Boolean = True)

        Dim MostrarMensaje As Boolean = False

        Try

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Enviar salidas?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                clsSyncSAPSPedidoCliente.Enviar_Transacciones_De_Salida(lblprg, prg)
            End If

        Catch ex As Exception

            Dim vMensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)

            If MostrarMensaje Then
                XtraMessageBox.Show(vMensaje,
                                    Text,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error)
            Else
                clsPublic.Actualizar_Progreso(lblprg, vMensaje)
            End If

        End Try

    End Sub

    Private Function Actualizar_Estado_Pedido_Cliente(ByVal pNoDocEntrySAP As Integer, EstadoEnvio As clsDataContractDI.Estado_Enviado_SAP) As Boolean

        Actualizar_Estado_Pedido_Cliente = False

        Try

            If clsSyncSAPSPedidoCliente.Marcar_Pedido_Cliente_Sincronizado_SAP(pNoDocEntrySAP, EstadoEnvio, lblprg) Then
                Actualizar_Estado_Pedido_Cliente = True
            End If

        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lblprg, "Error: " & ex.Message)
        End Try

    End Function

    Private Sub mnuActualizarCodigosBarra_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizarCodigosBarra.ItemClick

        Try

            Dim SapProductoCodigosBarra As New clsSyncSapCodigosBarra
            SapProductoCodigosBarra.Importar_Codigos_Barra_Productos(lblprg, prg)

        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lblprg, "Error: " & ex.Message)
        End Try


    End Sub

    Private Sub mnuClientes_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuClientes.ItemClick
        Ejecuta_Interface_Clientes(True)
    End Sub

    Public Sub Ejecuta_Interface_Clientes(Optional ByVal Preguntar As Boolean = True)

        Try

            prg.Visible = True

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Actualizar registros?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                clsSyncSAPCliente.Insertar_Clientes_Desde_TablaIntermedia_A_Tabla_TOMWMS(lblprg, prg, True, Preguntar)
            End If

        Catch ex As Exception

            Dim vMensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsPublic.Actualizar_Progreso(lblprg, vMensaje)
            prg.Visible = False

        End Try

    End Sub

    Private Sub DesactivarMenu()

        Try

            Dim mCurrentItem As BarItem
            Dim mBarSubItem As BarSubItem
            Dim mSubLink As BarItemLink

            For Each currentPage As RibbonPage In rbMain.Pages

                For Each currentGroup As RibbonPageGroup In currentPage.Groups

                    currentGroup.Visible = False
                    currentGroup.Enabled = False

                    For Each currenLink As BarItemLink In currentGroup.ItemLinks

                        mCurrentItem = currenLink.Item

                        currenLink.Item.Visibility = BarItemVisibility.Never
                        currenLink.Item.Enabled = False

                        If mCurrentItem.GetType.FullName = "DevExpress.XtraBars.BarSubItem" Then

                            mBarSubItem = mCurrentItem

                            For Each mSubLink In mBarSubItem.ItemLinks

                                mSubLink.Item.Visibility = BarItemVisibility.Never
                                mSubLink.Item.Enabled = False

                                Debug.Print(String.Format("{0} - {1} - {2} - {3} - {4}", currentPage.Text, currentGroup.Text, mSubLink.Item.Caption, mSubLink.Item.Name, mSubLink.Item.GetType.FullName))

                            Next

                        End If

                    Next currenLink

                Next currentGroup

            Next currentPage

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Public Shared Sub HabilitarMenuRol(ByVal pIdRol As Integer, ByVal prbMain As RibbonControl)

        Dim mCurrentItem As BarItem
        Dim mBarSubItem As BarSubItem
        Dim mSubLink As BarItemLink

        ' Este procedimiento verifica que cada item del menu 
        ' esté disponible para el usuario que se ha logeado        
        Dim lMenuRol As New List(Of clsBeMenu_sistema)

        Try

            prbMain.Enabled = True

            lMenuRol = clsLnMenu_sistema.Get_All_By_IdRol(pIdRol)

            Dim BeMenuSistema As New clsBeMenu_sistema

            For Each currentPage As RibbonPage In prbMain.Pages

                BeMenuSistema = Nothing

                BeMenuSistema = lMenuRol.Find(Function(x) (x.Titulo = currentPage.Text OrElse x.Nombre_lgco = currentPage.Name) AndAlso x.Nivel = 1)

                If Not BeMenuSistema Is Nothing Then
                    currentPage.Visible = BeMenuSistema.Visible
                Else
                    currentPage.Visible = False
                    '#EJC20180419:No deshabilitar el ribon principal.
                    'currentPage.Ribbon.Enabled = False
                End If

                Debug.Print(String.Format("Habilita Page: {0} - {1}", currentPage.Text, currentPage.Name))

            Next

            For Each currentPage As RibbonPage In prbMain.Pages

                For Each currentGroup As RibbonPageGroup In currentPage.Groups

                    BeMenuSistema = lMenuRol.Find(Function(x) (x.Titulo = currentGroup.Text OrElse x.Nombre_lgco = currentGroup.Name) AndAlso x.Nivel = 2)

                    BeMenuSistema = lMenuRol.Find(Function(x) (x.Nombre_lgco = currentGroup.Name))

                    If Not BeMenuSistema Is Nothing Then
                        currentGroup.Visible = BeMenuSistema.Visible
                        currentGroup.Enabled = BeMenuSistema.Visible
                    Else
                        currentGroup.Visible = False
                        currentGroup.Enabled = False
                    End If

                    If currentPage.Name = "rpReportes" Then
                        Debug.Print(String.Format("Habilita Group: {0}/{1} - {2}/{3} ", currentPage.Text, currentPage.Name, currentGroup.Text, currentGroup.Name))
                    End If

                    Debug.Print(String.Format("Habilita Group: {0}/{1} - {2}/{3} ", currentPage.Text, currentPage.Name, currentGroup.Text, currentGroup.Name))


                Next currentGroup

            Next currentPage

            For Each currentPage As RibbonPage In prbMain.Pages

                For Each currentGroup As RibbonPageGroup In currentPage.Groups

                    For Each currenLink As BarItemLink In currentGroup.ItemLinks

                        BeMenuSistema = lMenuRol.Find(Function(x) (x.Titulo = currenLink.Item.Caption OrElse x.Nombre_lgco = currenLink.Item.Name) AndAlso x.Nivel = 3)

                        If Not BeMenuSistema Is Nothing Then
                            currenLink.KeyTip = BeMenuSistema.IdMenu

                            currenLink.Visible = BeMenuSistema.Visible
                            currenLink.Item.Visibility = BeMenuSistema.Visible
                            currenLink.Item.Enabled = BeMenuSistema.Visible
                        Else
                            currenLink.Visible = False
                            currenLink.Item.Visibility = False
                            currenLink.Item.Enabled = False
                        End If

                        Debug.Print(String.Format("Habilita Link: {0} - {1} - {2}", currentPage.Text, currentGroup.Text, currenLink.Item.Caption))

                    Next

                Next

            Next

            For Each currentPage As RibbonPage In prbMain.Pages

                For Each currentGroup As RibbonPageGroup In currentPage.Groups

                    For Each currenLink As BarItemLink In currentGroup.ItemLinks

                        mCurrentItem = currenLink.Item

                        If mCurrentItem.GetType.FullName = "DevExpress.XtraBars.BarSubItem" Then

                            mBarSubItem = mCurrentItem

                            Debug.Print("Ha: " & mBarSubItem.Caption)

                            For Each mSubLink In mBarSubItem.ItemLinks

                                If mSubLink.Item.Name = "mnuTareasPreIngreso" OrElse mSubLink.Item.Caption = "mnuTareasPreIngreso" Then
                                    Debug.Print("Espera")
                                Else
                                    Debug.Print(String.Format("Habilita: {0} - {1} - {2} - {3} - {4}", currentPage.Text, currentGroup.Text, mSubLink.Item.Caption, mSubLink.Item.Name, mSubLink.Item.GetType.FullName))
                                End If

                                BeMenuSistema = lMenuRol.Find(Function(x) (x.Titulo = mSubLink.Item.Name OrElse x.Nombre_lgco = mSubLink.Item.Name) AndAlso x.Nivel = 4)

                                If Not BeMenuSistema Is Nothing Then
                                    mSubLink.KeyTip = BeMenuSistema.IdMenu
                                    mSubLink.Visible = BeMenuSistema.Visible
                                    mSubLink.Item.Visibility = BeMenuSistema.Visible
                                    mSubLink.Item.Enabled = BeMenuSistema.Visible
                                Else
                                    mSubLink.Visible = False
                                    mSubLink.Item.Visibility = False
                                    mSubLink.Item.Enabled = False
                                End If

                            Next

                        Else

                            If mCurrentItem.GetType.FullName = "DevExpress.XtraBars.BarButtonItem" Then

                                BeMenuSistema = lMenuRol.Find(Function(x) (x.Titulo = mCurrentItem.Name OrElse x.Nombre_lgco = mCurrentItem.Name))

                                If Not BeMenuSistema Is Nothing Then
                                    Debug.Print("Opción de menú está configurada en nivel: " & BeMenuSistema.Nivel &
                                                " Se encontró en iteración de nivel 4 - Opción: " & mCurrentItem.Name)
                                Else
                                    Debug.Print("Ha_Na: " & mCurrentItem.GetType.FullName & " " & mCurrentItem.Name)
                                End If

                            Else
                                Debug.Print("Ha_Na: " & mCurrentItem.GetType.FullName & " " & mCurrentItem.Name)
                            End If

                        End If

                    Next

                Next

            Next

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub mnuReporteComparativoWMSvrsERP_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuReporteComparativoWMSvrsERP.ItemClick

        Try

            Dim frmStockPorLote As New frmStockPorLoteArea
            frmStockPorLote.Modo = frmStockPorLoteArea.pModo.Lista
            frmStockPorLote.ShowDialog()
            frmStockPorLote.Dispose()

        Catch ex As Exception

        End Try

    End Sub

    Private Function Cerrar_Solicitud_Traslado(ByVal pNoDocEntrySAP As Integer) As Boolean

        Cerrar_Solicitud_Traslado = False

        Try

            Dim BePedidoCliente As New clsBeTrans_pe_enc
            'BePedidoCliente = clsLnTrans_pe_enc.Get_Single_By_No_Documento(pNoDocEntrySAP)
            BePedidoCliente = clsLnTrans_pe_enc.Get_Single_By_Referencia_Documento(pNoDocEntrySAP)

            If Not BePedidoCliente Is Nothing Then

                If Not clsLnStock_res.Tiene_StockRes_By_IdPedidoEnc(BePedidoCliente.IdPedidoEnc, BePedidoCliente.IdBodega) Then

                    Dim SapTrasladoSync As New clsSyncSAPSPedidoCliente

                    If SapTrasladoSync.Cerrar_Lineas_Documento_Salida(pNoDocEntrySAP, lblprg) Then
                        Cerrar_Solicitud_Traslado = True
                    End If

                Else
                    clsPublic.Actualizar_Progreso(lblprg, "El traslado tiene stock reservado en WMS, no se puede marcar como despachado.")
                End If

            Else
                clsPublic.Actualizar_Progreso(lblprg, "No se pudo obtener el documento de WMS.")
            End If


        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lblprg, "Error: " & ex.Message)
        End Try

    End Function

    Private Sub mnuTallas_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuTallas.ItemClick
        Ejecuta_Interface_Tallas(True)
    End Sub

    Public Sub Ejecuta_Interface_Tallas(Optional ByVal Preguntar As Boolean = True)

        Try

            prg.Visible = True

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Actualizar registros?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then

                BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface)

                If BeConfigEnc IsNot Nothing Then

                    'Using sTalla As New clsSyncSapTalla()
                    '    sTalla.Get_Tallas_From_Sap_Hana(BeConfigEnc, lblprg, prg)
                    'End Using

                Else
                    Throw New Exception("#Error_20250304: No se definió la configuración de interface.")
                End If

            End If

        Catch ex As Exception

            Dim vMensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsPublic.Actualizar_Progreso(lblprg, vMensaje)
            prg.Visible = False

        End Try

    End Sub

    Private Sub mnuColores_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuColores.ItemClick
        Ejecuta_Interface_Colores(True)
    End Sub

    Public Sub Ejecuta_Interface_Colores(Optional ByVal Preguntar As Boolean = True)

        Try

            prg.Visible = True

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Actualizar registros?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then

                BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface)

                If BeConfigEnc IsNot Nothing Then

                    clsSyncSapColor.Get_Colores_From_Sap_Hana(BeConfigEnc, lblprg, prg)

                Else
                    Throw New Exception("#Error_20250304: No se definió la configuración de interface.")
                End If

            End If

        Catch ex As Exception

            Dim vMensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsPublic.Actualizar_Progreso(lblprg, vMensaje)
            prg.Visible = False

        End Try

    End Sub

    Private Sub Ejecuta_Interface_Facturas_Reserva_SAP_Hana(Optional ByVal Preguntar As Boolean = True,
                                                           Optional ByVal pNoDocumentoCompraSAP As String = "")

        Try

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Actualizar Facturas de Reserva?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                Dim unused = clsSyncSapFacturaReserva.Insertar_Facturas_Reserva_Desde_Tabla_Intermedia_A_Tabla_TOMWMS(lblprg, prg, True, Preguntar, pNoDocumentoCompraSAP)
            End If

        Catch ex As Exception

            Dim vMensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsPublic.Actualizar_Progreso(lblprg, vMensaje)

        End Try

    End Sub



    Private Sub frmEjecucion_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls Or
                                               SecurityProtocolType.Tls11 Or
                                               SecurityProtocolType.Tls12
        ServicePointManager.ServerCertificateValidationCallback = AddressOf AcceptAllCertifications

    End Sub
    Private Function AcceptAllCertifications(sender As Object, cert As X509Certificate, chain As X509Chain, sslPolicyErrors As SslPolicyErrors) As Boolean
        Return True
    End Function

    Private Sub Ejecuta_Interface_Traslados_Envio_SAP_Hana(Optional ByVal Preguntar As Boolean = True,
                                                           Optional ByVal pNoDocumentoSolTraslado As String = "")

        Try

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Actualizar traslados de envío?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                Dim unused = clsSyncSapTrasladosEnvio.Procesar_Solicitud_Traslado_SAP(lblprg, prg, pNoDocumentoSolTraslado)
            End If

        Catch ex As Exception

            Dim vMensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsPublic.Actualizar_Progreso(lblprg, vMensaje)

        End Try

    End Sub

    Private Sub Ejecuta_Interface_Sol_Devol_Prov_SAP_Hana(Optional ByVal Preguntar As Boolean = True,
                                                          Optional ByVal pNoDocumentoSolTraslado As String = "")

        Try

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Actualizar Solicitudes de devolución a Proveedor?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                Dim unused = clsSyncSapDevolProveedor.Procesar_Solicitud_Devol_Prov_SAP(lblprg, prg, pNoDocumentoSolTraslado)
            End If

        Catch ex As Exception

            Dim vMensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsPublic.Actualizar_Progreso(lblprg, vMensaje)

        End Try

    End Sub

    Private Sub mnuRecibirFacturaReservaProv_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuRecibirFacturaReservaProv.ItemClick

        Dim args As New XtraInputBoxArgs()
        Dim tmpResult As Object
        Dim pPedidoCompra As String = ""

        Try

            args.Caption = "Ingrese Factura de Reserva"
            args.Prompt = "Factura No."

            Dim editor As New TextEdit
            args.Editor = editor

            args.DefaultButtonIndex = 0
            args.DefaultResponse = ""
            AddHandler args.Showing, AddressOf Args_Showing

            tmpResult = XtraInputBox.Show(args)

            lblprg.Text = ""

            If Not tmpResult Is Nothing Then

                pPedidoCompra = tmpResult.ToString

                Ejecuta_Interface_Facturas_Reserva_SAP_Hana(True, pPedidoCompra)

            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                               Text,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub mnuEnviarFacturaReservaIngreso_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuEnviarFacturaReservaIngreso.ItemClick

        Try
            mnuEnviarPedidosCompra.Enabled = False
            clsSyncSapFacturaReserva.Enviar_Facturas_Reserva_Prov_Ingreso(lblprg, prg)
        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lblprg, "Error: " & ex.Message)
        Finally
            mnuEnviarPedidosCompra.Enabled = True
            prg.Visible = False
        End Try

    End Sub

    Private Sub mnuRecibirTrasladosCedis_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuRecibirTrasladosCedis.ItemClick


        Dim args As New XtraInputBoxArgs()
        Dim tmpResult As Object
        Dim pPedidoCompra As String = ""

        mnuRecibirTrasladosCedis.Enabled = False

        Try

            args.Caption = "Ingrese Traslado"
            args.Prompt = "Traslado No."

            Dim editor As New TextEdit
            args.Editor = editor

            args.DefaultButtonIndex = 0
            args.DefaultResponse = ""
            AddHandler args.Showing, AddressOf Args_Showing

            tmpResult = XtraInputBox.Show(args)

            lblprg.Text = ""

            If Not tmpResult Is Nothing Then

                pPedidoCompra = tmpResult.ToString

                Ejecuta_Interface_Traslados_Envio_SAP_Hana(True, pPedidoCompra)

            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                               Text,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        Finally
            mnuRecibirTrasladosCedis.Enabled = True
            prg.Visible = False
        End Try

    End Sub

    Private Sub BarButtonItem5_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuImportarSolDevolProv.ItemClick

        Dim args As New XtraInputBoxArgs()
        Dim tmpResult As Object
        Dim pSolDevolProveedor As String = ""

        mnuImportarSolDevolProv.Enabled = False

        Try

            args.Caption = "Ingrese Sol. Devolución"
            args.Prompt = "Devolución No."

            Dim editor As New TextEdit
            args.Editor = editor

            args.DefaultButtonIndex = 0
            args.DefaultResponse = ""
            AddHandler args.Showing, AddressOf Args_Showing

            tmpResult = XtraInputBox.Show(args)

            lblprg.Text = ""

            If Not tmpResult Is Nothing Then

                pSolDevolProveedor = tmpResult.ToString

                Ejecuta_Interface_Sol_Devol_Prov_SAP_Hana(True, pSolDevolProveedor)

            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                               Text,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        Finally
            mnuImportarSolDevolProv.Enabled = True
            prg.Visible = False
        End Try


    End Sub

    Private Sub mnuEnviarSolDevolProv_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuEnviarSolDevolProv.ItemClick


        Try

            Enviar_Solicitudes_Devol_Prov(True)

        Catch ex As Exception

            Dim vMensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsPublic.Actualizar_Progreso(lblprg, vMensaje)

        End Try

    End Sub

    Private Sub Enviar_Solicitudes_Devol_Prov(Optional ByVal Preguntar As Boolean = True)

        Dim MostrarMensajeError As Boolean = False
        procesoEnEjecucion = True

        Try

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Enviar Solicitudes de devolución de proveedor?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                clsSyncSapDevolProveedor.Enviar_Transacciones_De_Salida(lblprg, prg, tTipoDocumentoSalida.Devolucion_Proveedor)
            End If

        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lblprg, ex.Message)
        Finally
            procesoEnEjecucion = False
        End Try

    End Sub

    Private Sub mnuEnviarTrasladosCedis_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuEnviarTrasladosCedis.ItemClick
        mnuEnviarTrasladosCedis.Visibility = BarItemVisibility.Never
        Enviar_Traslado_Desde_Solicitud(True)
        mnuEnviarTrasladosCedis.Visibility = BarItemVisibility.Always
    End Sub

    Private Async Sub Enviar_Traslado_Desde_Solicitud(Optional ByVal Preguntar As Boolean = True)

        Dim MostrarMensaje As Boolean = False
        procesoEnEjecucion = True

        Try

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Generar traslados de stock desde solicitud?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                lblprg.Text = ""
                Await clsSyncSapTrasladosEnvio.Enviar_Traslados_Desde_Solicitud(lblprg,
                                                                          prg,
                                                                          tTipoDocumentoSalida.Transferencia_Interna_WMS)
            End If

        Catch ex As Exception

            Dim vMensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsPublic.Actualizar_Progreso(lblprg, vMensaje)

        Finally
            procesoEnEjecucion = False
        End Try

    End Sub

End Class