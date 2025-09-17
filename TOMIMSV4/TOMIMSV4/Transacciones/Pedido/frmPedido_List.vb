Imports System.IO
Imports System.Reflection
Imports DevExpress.Data
Imports DevExpress.Utils
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen

Public Class frmPedido_List

    'Private pBePedidoEnc As clsBeTrans_pe_enc
    Public pBePedidoEnc As clsBeTrans_pe_enc

    Public Property Modo As pModo

    Public Property vNombreArchivoLayOutGrid As String = "frmPedido_List.vb"
    Public Property vNombreArchivoLayOutGridDetalle As String = ""
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol
    Public Call_Bind_Listar_Pedidos As New MethodInvoker(AddressOf Listar_Pedidos)
    Public Sub New()
        InitializeComponent()
    End Sub

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub frmPedido_List_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

        If e.KeyCode = Keys.N AndAlso e.Control Then

            If clsLnMenu_rol.Permiso_Funcionalidad("3.2.1.1", AP.IdRol) Then
                Nuevo_Pedido()
            Else
                XtraMessageBox.Show("No tiene permisos para crear nuevos documentos de salida",
                                    Text,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation)
            End If

        End If

        If e.KeyCode = Keys.Escape Then Close()

    End Sub

    Private Sub frmPedido_List_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            mnuNuevo.Enabled = clsLnMenu_rol.Permiso_Funcionalidad("3.2.1.1", AP.IdRol)

            '#EJC20210716:Restaurar LayoutGrid en LotesPorUbi.
            vNombreArchivoLayOutGrid = "grdPedidoListDespachos.xml"

            vNombreArchivoLayOutGridDetalle = "grdPedidoListDetalle.xml"

            Listar_Pedidos()

            If clsLnMenu_rol.Permiso_Funcionalidad("3.2.1.2", AP.IdRol) Then
                mnuEliminarPedido.Visibility = BarItemVisibility.Always
                ' mnuEliminarPedido.Enabled = True
            Else
                mnuEliminarPedido.Visibility = BarItemVisibility.Never
                ' mnuEliminarPedido.Enabled = False
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub frmPedido_List_Closed(sender As Object, e As EventArgs) Handles Me.Closed

        Try

            'RibbonControl.SaveLayoutToXml(fileNameLayout)

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private IsLoading As Boolean = False

    Public Sub Listar_Pedidos()

        IsLoading = True

        Try

            If gviewEncabezadoPedido Is Nothing Then
                Exit Sub
            End If

            gviewEncabezadoPedido.Columns.Clear()

            DgridPedido.DataSource = Nothing

            DgridPedido.BeginUpdate()

            Dim Dt As New DataTable

            Dt = clsLnTrans_pe_enc.GetAll(chkActivos.Checked,
                                          dtpFechaDel.Value,
                                          dtpFechaAl.Value,
                                          chkAnulados.Checked,
                                          AP.IdBodega,
                                          chkDespachados.Checked,
                                          chkSinExistencias.Checked,
                                          chkSinExistenciasERP.Checked)

            DgridPedido.DataSource = Dt

            If gviewEncabezadoPedido.RowCount > 0 Then
                lblRegs.Caption = String.Format("Registros: {0}", gviewEncabezadoPedido.RowCount)
            End If

            Dim col = New Columns.GridColumn With
                        {.Name = "Enviado_A_ERP_Flag",
                        .Caption = "MI3_Estatus",
                        .FieldName = "Enviado_A_ERP_Flag",
                        .UnboundType = UnboundColumnType.Object,
                        .ColumnEdit = RepositoryItemPictureEdit1}

            If gviewEncabezadoPedido.Columns.ColumnByName("Enviado_A_ERP_Flag") Is Nothing Then
                gviewEncabezadoPedido.Columns.Add(col)
            End If

            col.Visible = True

            Try

                If gviewEncabezadoPedido.Columns.Count > 1 Then
                    gviewEncabezadoPedido.Columns("fec_agr").DisplayFormat.FormatType = FormatType.DateTime
                    gviewEncabezadoPedido.Columns("fec_agr").DisplayFormat.FormatString = "G"
                    gviewEncabezadoPedido.Columns("fec_agr").Caption = "Fecha_Agregado"
                    gviewEncabezadoPedido.Columns("anulado").Caption = "Anulado"
                    gviewEncabezadoPedido.Columns("activo").Caption = "Activo"
                    gviewEncabezadoPedido.Columns("no_documento").Caption = "No_Documento"
                    gviewEncabezadoPedido.Columns("referencia").Caption = "Referencia"
                    gviewEncabezadoPedido.Columns("IdBodega").Visible = False
                    'gviewEncabezadoPedido.Columns("IdPrioridadPicking").Visible = False
                End If

            Catch ex As Exception

            End Try

            Dim col1 = New Columns.GridColumn With
                        {.Name = "IdPrioridadPickingFlag",
                        .Caption = "Prioridad",
                        .FieldName = "IdPrioridadPickingFlag",
                        .UnboundType = UnboundColumnType.Object,
                        .ColumnEdit = RepositoryItemPictureEdit1}

            If gviewEncabezadoPedido.Columns.ColumnByName("IdPrioridadPickingFlag") Is Nothing Then
                gviewEncabezadoPedido.Columns.Add(col1)
            End If

            col1.Visible = True

            DgridPedido.EndUpdate()

            Set_LayOut_Grid()

            Try

                '#EJC20220427: Prevent column width lost.
                If Not ExisteLayOuotGridEnc Then

                    gviewEncabezadoPedido.OptionsView.ColumnAutoWidth = False

                    If gviewEncabezadoPedido.Columns.Count > 0 Then
                        If gviewEncabezadoPedido.RowCount > 0 Then
                            gviewEncabezadoPedido.BestFitColumns()
                        End If
                    End If

                End If

            Catch ex As Exception
            End Try

            DTPedidosIncompletosReserva = clsLnI_nav_ped_traslado_enc.Get_All_Pedidos_Incompletos()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            IsLoading = False
        End Try

    End Sub

    Private Sub Nuevo_Pedido()

        Try

            Cierra_Instancia_Previa(frmPedido)

            With frmPedido

                .Modo = frmPedido.TipoTrans.Nuevo
                .MdiParent = MdiParent
                .InvokeListarPedidos = AddressOf Listar_Pedidos
                .WindowState = FormWindowState.Normal

                If OpcionesMenu IsNot Nothing Then
                    .OpcionesMenu = OpcionesMenu
                    .mnuGuardar.Enabled = .OpcionesMenu.Modificar
                    .cmdActualizar.Enabled = .OpcionesMenu.Modificar
                    .cmdEliminar.Enabled = .OpcionesMenu.Eliminar
                End If

                .Show()
                .Focus()
            End With

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Public Sub Cierra_Instancia_Previa(ByRef Myform As Form)

        Try

            For Each objForm In My.Application.OpenForms
                If (Trim(objForm.Name) = Trim(Myform.Name)) Then
                    Myform.Close()
                    Exit For
                End If
            Next

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub mnuNuevo_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuNuevo.ItemClick
        mnuNuevo.Enabled = False
        Nuevo_Pedido()
        mnuNuevo.Enabled = True
    End Sub

    Private Sub Procesar_Registro()

        Try

            If (gviewEncabezadoPedido.RowCount > 0) Then

                Dim Dr As DataRowView = gviewEncabezadoPedido.GetFocusedRow

                If Not Dr Is Nothing Then

                    SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                    SplashScreenManager.Default.SetWaitFormDescription("Abriendo documento")

                    '#GT17092025: metodo exclusivo para cargar pedido y detalle sin filtrar que alguna linea tenga stock_liberado y no se muestre en el grid
                    pBePedidoEnc = New clsBeTrans_pe_enc
                    pBePedidoEnc = clsLnTrans_pe_enc.GetSingle_For_Pedido(Dr.Item("Correlativo"))

                    Dim lSelectionIndex As Integer = gviewEncabezadoPedido.FocusedRowHandle

                    If pBePedidoEnc Is Nothing Or pBePedidoEnc.IdPedidoEnc = 0 Then
                        clsLnTrans_pe_enc.Eliminar_Pedido(Dr.Item("Correlativo"))
                        Throw New Exception("El pedido con correlativo WMS: " & Dr.Item("Correlativo") & " fué modificado, eliminado o es un pedido inconsistente y no se puede recuperar")
                    ElseIf pBePedidoEnc.Estado = "NUEVO" AndAlso pBePedidoEnc.Ubicacion = "TMP" Then
                        XtraMessageBox.Show("El pedido seleccionado se creó de forma incorrecta por el usuario en el WMS y no se concluyó: Ubicación = TMP (Valide Cliente y Propietario)", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                    '#GT09092024: si es fiscal cargar poliza.
                    If AP.Bodega.Es_Bodega_Fiscal Then
                        pBePedidoEnc.ObjPoliza = clsLnTrans_pe_pol.GetSingleId(pBePedidoEnc.IdPedidoEnc)
                    Else
                        pBePedidoEnc.ObjPoliza = Nothing
                    End If

                    If Modo = pModo.Lista Then

                        Cierra_Instancia_Previa(frmPedido)

                        clsLnLog_error_wms.Agregar_Error("ADVERTENCIA_202302231702: El IdUsuario: " & AP.UsuarioAp.IdUsuario & " abrió el IdPedidoEnc: " & pBePedidoEnc.IdPedidoEnc)

                        With frmPedido
                            .Modo = frmPedido.TipoTrans.Editar
                            .pBePedidoEnc = pBePedidoEnc
                            .InvokeListarPedidos = AddressOf Listar_Pedidos
                            .MdiParent = MdiParent
                            .WindowState = FormWindowState.Normal
                            .Text = "Pedido " & pBePedidoEnc.IdPedidoEnc & " - " & pBePedidoEnc.Referencia

                            If OpcionesMenu IsNot Nothing Then
                                .OpcionesMenu = OpcionesMenu
                                .mnuGuardar.Enabled = .OpcionesMenu.Modificar
                                .cmdActualizar.Enabled = .OpcionesMenu.Modificar
                                .cmdEliminar.Enabled = .OpcionesMenu.Eliminar
                            End If

                            .Show()
                            .Focus()
                        End With

                        gviewEncabezadoPedido.FocusedRowHandle = lSelectionIndex

                    ElseIf Modo = pModo.Seleccion Then
                        'Hide()
                        DialogResult = DialogResult.OK
                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            If Not SplashScreenManager.Default Is Nothing Then
                If SplashScreenManager.Default.IsSplashFormVisible Then
                    SplashScreenManager.CloseForm(False)
                End If
            End If
        End Try

    End Sub

    Private Sub Dgrid_DoubleClick(sender As Object, e As EventArgs) Handles DgridPedido.DoubleClick
        Procesar_Registro()
    End Sub

    Private Sub Dgrid_Click(sender As Object, e As EventArgs) Handles DgridPedido.Click
        Mostrar_Detalle()
    End Sub
    Private Sub chkActivos_CheckedChanged(sender As Object, e As ItemClickEventArgs) Handles chkActivos.CheckedChanged
        Listar_Pedidos()
    End Sub

    Private Sub mnuSalir_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuSalir.ItemClick
        Close()
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuActualizar.ItemClick
        Listar_Pedidos()
    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdImprimir.ItemClick
        Imprimir_Vista()
    End Sub

    Private Sub Imprimir_Vista()

        Try

            Dim printingSystem1 As New DevExpress.XtraPrinting.PrintingSystem()
            Dim printLink As New DevExpress.XtraPrinting.PrintableComponentLink()

            AddHandler printLink.CreateReportHeaderArea, AddressOf PrintableComponentLink_CreateReportHeaderArea

            Const leftColumnFoot As String = "Páginas: [Page # of Pages #] "
            Dim leftColumnHead As String = "Usuario: [User Name] - " & AP.UsuarioAp.Nombres

            Const rightColumn As String = "Fecha: [Date Printed] [Time Printed] "

            Dim phf As DevExpress.XtraPrinting.PageHeaderFooter =
            TryCast(printLink.PageHeaderFooter, DevExpress.XtraPrinting.PageHeaderFooter)

            phf.Header.Content.Clear()

            phf.Footer.Content.AddRange(New String() _
            {leftColumnFoot})
            phf.Footer.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Near

            phf.Header.Content.AddRange(New String() {leftColumnHead, "", rightColumn})
            phf.Header.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Far

            printingSystem1.PageSettings.Landscape = True
            printLink.Component = DgridPedido
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Listado de Ordenes de Compra"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub dtpFechaDel_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaDel.ValueChanged

        Try

            Listar_Pedidos()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub dtpFechaAl_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaAl.ValueChanged

        Try

            Listar_Pedidos()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub GridView1_RowStyle(sender As Object, e As RowStyleEventArgs) Handles gviewEncabezadoPedido.RowStyle

        Try

            gviewEncabezadoPedido.OptionsBehavior.Editable = False
            gviewEncabezadoPedido.OptionsSelection.EnableAppearanceFocusedCell = False
            gviewEncabezadoPedido.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
            gviewEncabezadoPedido.OptionsSelection.EnableAppearanceFocusedRow = True
            gviewEncabezadoPedido.OptionsSelection.EnableAppearanceHideSelection = True
            gviewEncabezadoPedido.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            gviewEncabezadoPedido.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            gviewEncabezadoPedido.Appearance.FocusedRow.ForeColor = Color.White
            gviewEncabezadoPedido.Appearance.SelectedRow.ForeColor = Color.White
            gviewEncabezadoPedido.Appearance.SelectedRow.Options.UseBackColor = True
            gviewEncabezadoPedido.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub mnuMI3Sync_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuMI3Sync.ItemClick

        Try

            If AP.IdConfiguracionInterface <> -1 Then

                If clsBD.Instancia.IdConfiguracionInterface = 1989 Then

                    lblPrg.Visible = True

                    Dim vCodigoBodega As String = clsLnBodega.Get_Codigo_By_IdBodega(AP.IdBodega)

                    lblPrg.Text = ""

                    If clsSyncPedidoRoad.Procesar_Pedidos_Road(1,
                                                               vCodigoBodega,
                                                               AP.IdEmpresa,
                                                               AP.IdBodega,
                                                               lblPrg,
                                                               prg) Then
                        Listar_Pedidos()
                        'lblPrg.Visible = False
                    End If

                ElseIf Ejecutar_Interface("4-" & AP.IdConfiguracionInterface & "-" & gIndiceInstancia & "-" & AP.UsuarioAp.IdUsuario & "-0-0" & "-" & clsBD.Instancia.NombreInstancia, Me) Then
                    Listar_Pedidos()
                End If
            Else
                XtraMessageBox.Show("El archivo de configuración .ini no tiene un identificador de configuración para interface",
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation)
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Dgrid_KeyDown(sender As Object, e As KeyEventArgs) Handles DgridPedido.KeyDown
        If e.KeyCode = Keys.Enter Then Procesar_Registro()
    End Sub

    Private Sub GridView1_CustomUnboundColumnData(sender As Object, e As CustomColumnDataEventArgs) Handles gviewEncabezadoPedido.CustomUnboundColumnData

        If IsLoading Then Exit Sub

        Try

            Dim iconGreen As Image = ImageCollection1.Images(1)
            Dim iconRed As Image = ImageCollection1.Images(0)
            Dim iconYellow As Image = ImageCollection1.Images(2)
            Dim view As GridView = TryCast(sender, GridView)
            Dim rowHandle As Integer = view.GetRowHandle(e.ListSourceRowIndex)

            If e.Column.FieldName = "Enviado_A_ERP_Flag" AndAlso e.IsGetData() Then

                Dim Enviado_A_Erp As Object = view.GetRowCellValue(rowHandle, view.Columns("Enviado_A_ERP"))
                Dim Estado As Object = view.GetRowCellValue(rowHandle, view.Columns("Estado"))
                Dim vEnviado_A_ERP As Boolean = False

                If Enviado_A_Erp IsNot Nothing Then

                    Boolean.TryParse(Enviado_A_Erp.ToString(), vEnviado_A_ERP)

                    If Not Estado Is Nothing Then

                        If Estado.ToString = "Nuevo" Then
                            e.Value = iconYellow 'No se le ha generado picking ni se ha procesado.
                        Else
                            If Not vEnviado_A_ERP Then
                                e.Value = iconRed 'Ya se procesó pero no se pudo enviar a ERP.
                            Else
                                e.Value = iconGreen 'Ya se procesó y se envió correctamente a ERP.
                            End If
                        End If

                    End If

                Else
                    e.Value = ImageCollection1.Images(1)
                End If

            End If

            If e.Column.FieldName = "IdPrioridadPickingFlag" AndAlso e.IsGetData() Then

                Dim IdPrioridadPicking As Object = view.GetRowCellValue(rowHandle, view.Columns("IdPrioridadPicking"))
                Dim vIdPrioridadPicking As Integer = 0

                If IdPrioridadPicking IsNot Nothing Then

                    Integer.TryParse(IdPrioridadPicking.ToString(), vIdPrioridadPicking)

                    Select Case vIdPrioridadPicking

                        Case 0
                            e.Value = iconGreen
                        Case 1
                            e.Value = iconYellow
                        Case 2
                            e.Value = iconRed
                    End Select

                End If

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub chkAnulados_CheckedChanged(sender As Object, e As ItemClickEventArgs) Handles chkAnulados.CheckedChanged
        Listar_Pedidos()
    End Sub

    Private Sub chkDespachados_CheckedChanged(sender As Object, e As ItemClickEventArgs) Handles chkDespachados.CheckedChanged
        Listar_Pedidos()
    End Sub

    Private DTPedidosIncompletosReserva As New DataTable

    Private Sub frmPedido_List_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        '#EJC20201208: Mostrar rich text si tiene inteface con Road.
        lblPrg.Visible = (clsBD.Instancia.IdConfiguracionInterface = 1989)

        '#EJC20210716:Restaurar LayoutGrid en LotesPorUbi.
        vNombreArchivoLayOutGrid = "grdPedidoListDespachos.xml"

        vNombreArchivoLayOutGridDetalle = "grdPedidoListDetalle.xml"

        If clsLnMenu_rol.Permiso_Funcionalidad("3.2.1.2", AP.IdRol) Then
            mnuEliminarPedido.Enabled = True
        Else
            mnuEliminarPedido.Enabled = False
        End If

        If AP.Bodega.Rango_Dias_Documentos > 0 Then
            dtpFechaDel.Value = Now.Date.AddDays(-(AP.Bodega.Rango_Dias_Documentos))
            dtpFechaAl.Value = Now.Date.AddDays(AP.Bodega.Rango_Dias_Documentos)
        End If

        Try

            DTPedidosIncompletosReserva = clsLnI_nav_ped_traslado_enc.Get_All_Pedidos_Incompletos()

        Catch ex As Exception
        End Try

    End Sub

    Private ExisteLayOuotGridEnc As Boolean = False
    Private Sub Restore_LayOut_Grid()

        Try

            Dim BeConfiguracionUsuarioDet As New clsBeConfiguracion_usuario_det

            BeConfiguracionUsuarioDet = clsLnConfiguracion_usuario_enc.Get_Layout(AP.IdEmpresa,
                                                                                  AP.UsuarioAp.IdUsuario,
                                                                                  AP.HostName,
                                                                                  vNombreArchivoLayOutGrid)


            If Not BeConfiguracionUsuarioDet Is Nothing Then
                gviewEncabezadoPedido.RestoreLayoutFromStream(BeConfiguracionUsuarioDet.Stream_Template)
                mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            Else
                mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            End If

            Dim BeConfiguracionUsuarioDet1 As New clsBeConfiguracion_usuario_det

            BeConfiguracionUsuarioDet1 = clsLnConfiguracion_usuario_enc.Get_Layout(AP.IdEmpresa,
                                                                                  AP.UsuarioAp.IdUsuario,
                                                                                  AP.HostName,
                                                                                  vNombreArchivoLayOutGridDetalle)


            If Not BeConfiguracionUsuarioDet1 Is Nothing Then
                gviewDetallePedido.RestoreLayoutFromStream(BeConfiguracionUsuarioDet1.Stream_Template)
                mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Always
            Else
                mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Never
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub Mostrar_Detalle()

        Try

            If (gviewEncabezadoPedido.RowCount > 0) Then

                If chkMostrarGridDetalle.Checked Then

                    Dim Dr As DataRowView = gviewEncabezadoPedido.GetFocusedRow
                    Dim vIdPedidoEnc As Integer = Dr.Item("Correlativo")
                    Dim lSelectionIndex As Integer = gviewEncabezadoPedido.FocusedRowHandle

                    If Modo = pModo.Lista Then

                        Dim DT As New DataTable

                        DT = clsLnTrans_pe_det.Get_VW_Progreso_Picking_By_IdPedidoEnc_And_IdBodega(vIdPedidoEnc, AP.Bodega.IdBodega)

                        dgridDetalle.DataSource = DT

                        gviewDetallePedido.Columns("Cantidad_Solicitada").DisplayFormat.FormatType = FormatType.Numeric
                        gviewDetallePedido.Columns("Cantidad_Solicitada").DisplayFormat.FormatString = "{0:n6}"
                        gviewDetallePedido.Columns("Cantidad_Solicitada").SummaryItem.SummaryType = SummaryItemType.Sum
                        gviewDetallePedido.Columns("Cantidad_Solicitada").SummaryItem.DisplayFormat = "{0:n6}"

                        gviewDetallePedido.Columns("Cantidad_Picking").DisplayFormat.FormatType = FormatType.Numeric
                        gviewDetallePedido.Columns("Cantidad_Picking").DisplayFormat.FormatString = "{0:n6}"
                        gviewDetallePedido.Columns("Cantidad_Picking").SummaryItem.SummaryType = SummaryItemType.Sum
                        gviewDetallePedido.Columns("Cantidad_Picking").SummaryItem.DisplayFormat = "{0:n6}"

                        gviewDetallePedido.Columns("Cantidad_Verificada").DisplayFormat.FormatType = FormatType.Numeric
                        gviewDetallePedido.Columns("Cantidad_Verificada").DisplayFormat.FormatString = "{0:n6}"
                        gviewDetallePedido.Columns("Cantidad_Verificada").SummaryItem.SummaryType = SummaryItemType.Sum
                        gviewDetallePedido.Columns("Cantidad_Verificada").SummaryItem.DisplayFormat = "{0:n6}"

                        gviewDetallePedido.Columns("Cantidad_Despachada").DisplayFormat.FormatType = FormatType.Numeric
                        gviewDetallePedido.Columns("Cantidad_Despachada").DisplayFormat.FormatString = "{0:n6}"
                        gviewDetallePedido.Columns("Cantidad_Despachada").SummaryItem.SummaryType = SummaryItemType.Sum
                        gviewDetallePedido.Columns("Cantidad_Despachada").SummaryItem.DisplayFormat = "{0:n6}"

                        gviewDetallePedido.Columns("Cantidad_Solicitada").SummaryItem.SummaryType = SummaryItemType.Sum
                        gviewDetallePedido.Columns("Cantidad_Solicitada").SummaryItem.DisplayFormat = "{0:n6}"

                        gviewDetallePedido.Columns("Cantidad_Picking").SummaryItem.SummaryType = SummaryItemType.Sum
                        gviewDetallePedido.Columns("Cantidad_Picking").SummaryItem.DisplayFormat = "{0:n6}"

                        gviewDetallePedido.Columns("Cantidad_Verificada").SummaryItem.SummaryType = SummaryItemType.Sum
                        gviewDetallePedido.Columns("Cantidad_Verificada").SummaryItem.DisplayFormat = "{0:n6}"

                        gviewDetallePedido.Columns("Cantidad_Despachada").SummaryItem.SummaryType = SummaryItemType.Sum
                        gviewDetallePedido.Columns("Cantidad_Despachada").SummaryItem.DisplayFormat = "{0:n6}"

                        gviewDetallePedido.Columns("Fecha_Picking").DisplayFormat.FormatType = FormatType.DateTime
                        gviewDetallePedido.Columns("Fecha_Picking").DisplayFormat.FormatString = "G"

                        gviewDetallePedido.Columns("Fecha_Verificacion").DisplayFormat.FormatType = FormatType.DateTime
                        gviewDetallePedido.Columns("Fecha_Verificacion").DisplayFormat.FormatString = "G"

                        Restore_LayOut_Grid_Detalle()

                        If Not ExisteLayOutDetealle Then
                            gviewDetallePedido.BestFitColumns()
                        End If

                        gviewEncabezadoPedido.FocusedRowHandle = lSelectionIndex

                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub chkMostrarGridDetalle_CheckedChanged(sender As Object, e As ItemClickEventArgs) Handles chkMostrarGridDetalle.CheckedChanged

        dgridDetalle.Visible = chkMostrarGridDetalle.Checked
        SplitContainer1.Panel2Collapsed = Not chkMostrarGridDetalle.Checked

    End Sub

    Private ExisteLayOutDetealle As Boolean = False

    Private Sub Restore_LayOut_Grid_Detalle()

        Try

            Dim BeConfiguracionUsuarioDet As New clsBeConfiguracion_usuario_det

            BeConfiguracionUsuarioDet = clsLnConfiguracion_usuario_enc.Get_Layout(AP.IdEmpresa,
                                                                                  AP.UsuarioAp.IdUsuario,
                                                                                  AP.HostName,
                                                                                  vNombreArchivoLayOutGrid)


            If Not BeConfiguracionUsuarioDet Is Nothing Then
                gviewEncabezadoPedido.RestoreLayoutFromStream(BeConfiguracionUsuarioDet.Stream_Template)
                mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Always
                ExisteLayOutDetealle = True
            Else
                mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Never
                ExisteLayOutDetealle = False
            End If


            Dim BeConfiguracionUsuarioDet1 As New clsBeConfiguracion_usuario_det

            BeConfiguracionUsuarioDet1 = clsLnConfiguracion_usuario_enc.Get_Layout(AP.IdEmpresa,
                                                                                  AP.UsuarioAp.IdUsuario,
                                                                                  AP.HostName,
                                                                                  vNombreArchivoLayOutGridDetalle)


            If Not BeConfiguracionUsuarioDet1 Is Nothing Then
                gviewDetallePedido.RestoreLayoutFromStream(BeConfiguracionUsuarioDet1.Stream_Template)
                mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Always
                ExisteLayOutDetealle = True
            Else
                mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Never
                ExisteLayOutDetealle = False
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub mnuGuardarLayoutGrid_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuGuardarLayoutGrid.ItemClick

        Try

            If IsLoading Then Exit Sub

            Dim Ms As New MemoryStream
            gviewEncabezadoPedido.SaveLayoutToStream(Ms)
            Ms.Seek(0, SeekOrigin.Begin)
            Dim MsReader As New StreamReader(Ms)
            Dim LayoutToText As String = MsReader.ReadToEnd()

            clsLnConfiguracion_usuario_enc.Guardar_Layout(AP.IdEmpresa,
                                                          AP.UsuarioAp.IdUsuario,
                                                          AP.HostName,
                                                          vNombreArchivoLayOutGrid,
                                                          LayoutToText)

            Dim Ms1 As New MemoryStream
            gviewDetallePedido.SaveLayoutToStream(Ms1)
            Ms1.Seek(0, SeekOrigin.Begin)
            Dim MsReader1 As New StreamReader(Ms1)
            Dim LayoutToText1 As String = MsReader1.ReadToEnd()

            clsLnConfiguracion_usuario_enc.Guardar_Layout(AP.IdEmpresa,
                                                          AP.UsuarioAp.IdUsuario,
                                                          AP.HostName,
                                                          vNombreArchivoLayOutGridDetalle,
                                                          LayoutToText1)

            mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Always

            XtraMessageBox.Show("Diseño de grid guardado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)


        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub GridView_Layout(sender As Object, e As EventArgs) Handles gviewEncabezadoPedido.Layout

        Try

            If IsLoading Then Exit Sub

            Dim Ms As New MemoryStream
            gviewEncabezadoPedido.SaveLayoutToStream(Ms)
            Ms.Seek(0, SeekOrigin.Begin)
            Dim MsReader As New StreamReader(Ms)
            Dim LayoutToText As String = MsReader.ReadToEnd()

            clsLnConfiguracion_usuario_enc.Guardar_Layout(AP.IdEmpresa,
                                                          AP.UsuarioAp.IdUsuario,
                                                          AP.HostName,
                                                          vNombreArchivoLayOutGrid,
                                                          LayoutToText)

            mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Always

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub mnuEliminarLayoutGrid_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuEliminarLayoutGrid.ItemClick

        Try

            clsLnConfiguracion_usuario_enc.Delete_Layout(AP.IdEmpresa,
                                                          AP.UsuarioAp.IdUsuario,
                                                          AP.HostName,
                                                          vNombreArchivoLayOutGrid)

            XtraMessageBox.Show("Diseño de grid eliminado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Close()

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                              Text,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Set_LayOut_Grid()

        Try

            Dim BeConfiguracionUsuarioDet As New clsBeConfiguracion_usuario_det

            BeConfiguracionUsuarioDet = clsLnConfiguracion_usuario_enc.Get_Layout(AP.IdEmpresa,
                                                                                  AP.UsuarioAp.IdUsuario,
                                                                                  AP.HostName,
                                                                                 vNombreArchivoLayOutGrid)

            If Not BeConfiguracionUsuarioDet Is Nothing Then
                gviewEncabezadoPedido.RestoreLayoutFromStream(BeConfiguracionUsuarioDet.Stream_Template)
                mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Always
            Else
                mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Never
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub mnuEliminarPedido_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuEliminarPedido.ItemClick

        Try

            If Not permiteMenu("3.2.1.2") Then
                Return
            End If

            If (gviewEncabezadoPedido.RowCount > 0) Then

                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormDescription("Eliminando documento...")

                Dim Dr As DataRowView = gviewEncabezadoPedido.GetFocusedRow

                If Not Dr Is Nothing Then

                    pBePedidoEnc = New clsBeTrans_pe_enc
                    pBePedidoEnc = clsLnTrans_pe_enc.GetSingle(Dr.Item("Correlativo"))

                    Dim lSelectionIndex As Integer = gviewEncabezadoPedido.FocusedRowHandle

                    If pBePedidoEnc Is Nothing Or pBePedidoEnc.IdPedidoEnc = 0 Then
                        clsLnTrans_pe_enc.Eliminar_Pedido(Dr.Item("Correlativo"))
                        Throw New Exception("El pedido con correlativo WMS: " & Dr.Item("Correlativo") & " fue modificado, eliminado o es un pedido inconsistente y no se puede recuperar")
                    ElseIf pBePedidoEnc.Estado = "NUEVO" AndAlso pBePedidoEnc.Ubicacion = "TMP" Then
                        XtraMessageBox.Show("El pedido seleccionado se creó de forma incorrecta por el usuario en el WMS y no se concluyó: Ubicación = TMP (Valide Cliente y Propietario)", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                    pBePedidoEnc.ObjPoliza = clsLnTrans_pe_pol.GetSingleId(pBePedidoEnc.IdPedidoEnc)

                    If Modo = pModo.Lista Then

                        If clsLnTrans_pe_enc.Tiene_Picking_Asociado(pBePedidoEnc.IdPedidoEnc) Then

                            SplashScreenManager.CloseForm(False)
                            XtraMessageBox.Show("No se puede anular, picking asociado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)

                        Else

                            If pBePedidoEnc.Estado = "Nuevo" _
                                OrElse pBePedidoEnc.Estado = "Incompleto" _
                                OrElse pBePedidoEnc.Estado = "Pendiente" _
                                OrElse pBePedidoEnc.Estado = "Verificado" _
                                OrElse pBePedidoEnc.Estado = "Anulado" Then

                                SplashScreenManager.CloseForm(False)

                                If XtraMessageBox.Show("¿Eliminar documento de salida:" & pBePedidoEnc.Referencia & "?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                                    Using MA As New frmMotivo_AnulacionList()

                                        With MA

                                            .Modo = frmMotivo_AnulacionList.pModo.Seleccion
                                            .BeMotivoAnulacionBodega.IdBodega = pBePedidoEnc.IdBodega

                                            If .ShowDialog() = DialogResult.OK Then

                                                '#CKFK20220324 Modifiqué esto para que cuando esté habilitado el Sync_MI3 y se elimine el documento de salida
                                                'también se elimine en NAV
                                                If pBePedidoEnc.Sync_MI3 Then

                                                    SplashScreenManager.CloseForm(False)

                                                    If XtraMessageBox.Show("¿Eliminar documento de ERP?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                                                        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                                                        SplashScreenManager.Default.SetWaitFormDescription("Anulando...")

                                                        '#EJC202211221049: Validar que la instancia no sea nothing para eliminar desde WS
                                                        If Not wsTOMHHInstance Is Nothing Then

                                                            Dim ArchHeader As New wsTOMHH.clsArchHeader()
                                                            ArchHeader.Tipo = "WM"

                                                            'Si no existe picking no debo borrar
                                                            Dim vResultBorraPicking As Boolean = wsTOMHHInstance.Borrar_Picking(ArchHeader,
                                                                                                                                pBePedidoEnc.Referencia)

                                                            If Not vResultBorraPicking Then
                                                                SplashScreenManager.CloseForm(False)
                                                                XtraMessageBox.Show("No se pudo eliminar el envío de almacén, ni el pedido.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                            Else

                                                                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                                                                SplashScreenManager.Default.SetWaitFormDescription("Anulando...")

                                                                If clsLnTrans_pe_enc.Eliminar_Pedido_By_IdPedidoEnc_And_Referencia(pBePedidoEnc,
                                                                                                                                   AP.Bodega.Eliminar_Documento_Salida,
                                                                                                                                   AP.UsuarioAp.IdUsuario,
                                                                                                                                   MA.BeMotivoAnulacionBodega.IdMotivoAnulacionBodega) Then

                                                                    SplashScreenManager.CloseForm(False)

                                                                    XtraMessageBox.Show("Se ha eliminado el pedido, el envío de almacén y se ha liberado el stock reservado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                                                                    Listar_Pedidos()

                                                                Else
                                                                    SplashScreenManager.CloseForm(False)
                                                                    XtraMessageBox.Show("No se pudo eliminar el pedido.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                                End If

                                                            End If

                                                        Else

                                                            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                                                            SplashScreenManager.Default.SetWaitFormDescription("Eliminando...")

                                                            If clsLnTrans_pe_enc.Eliminar_Pedido_By_IdPedidoEnc_And_Referencia(pBePedidoEnc,
                                                                                                                               AP.Bodega.Eliminar_Documento_Salida,
                                                                                                                               AP.UsuarioAp.IdUsuario) Then

                                                                SplashScreenManager.CloseForm(False)

                                                                Dim vInterfaceSAP As Boolean = clsLnI_nav_config_enc.Get_Interface_SAP(AP.IdConfiguracionInterface)

                                                                Listar_Pedidos()

                                                                Try

                                                                    If vInterfaceSAP Then
                                                                        'EJC202403271301: Actualizar el estado enviado a WMS a 2, para que se peuda volver a importar.

                                                                        Dim vArgumentosAEnviarAInterface As String = ""
                                                                        Dim tipoDocumento As New clsDataContractDI.tTipoDocumentoSalida

                                                                        tipoDocumento = pBePedidoEnc.IdTipoPedido

                                                                        Select Case tipoDocumento
                                                                            Case clsDataContractDI.tTipoDocumentoSalida.Devolucion_Proveedor
                                                                                vArgumentosAEnviarAInterface = "12-" & AP.IdConfiguracionInterface & "-" & gIndiceInstancia & "-" & AP.UsuarioAp.IdUsuario & "-" & pBePedidoEnc.Referencia & "-2"
                                                                            Case clsDataContractDI.tTipoDocumentoSalida.Pedido_De_Cliente
                                                                                vArgumentosAEnviarAInterface = "8-" & AP.IdConfiguracionInterface & "-" & gIndiceInstancia & "-" & AP.UsuarioAp.IdUsuario & "-" & pBePedidoEnc.Referencia & "-2"
                                                                            Case clsDataContractDI.tTipoDocumentoSalida.Traslado_Por_Estados_SAP
                                                                                vArgumentosAEnviarAInterface = "7-" & AP.IdConfiguracionInterface & "-" & gIndiceInstancia & "-" & AP.UsuarioAp.IdUsuario & "-" & pBePedidoEnc.Referencia & "-2"
                                                                        End Select

                                                                        'EJC202403271301: Actualizar el estado enviado a WMS a 2, para que se peuda volver a importar.
                                                                        If Ejecutar_Interface(vArgumentosAEnviarAInterface, Me) Then
                                                                            XtraMessageBox.Show("Se ha eliminado el pedido y se ha liberado el stock reservado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                                        End If

                                                                    End If

                                                                Catch ex As Exception
                                                                    XtraMessageBox.Show("Se ha eliminado el pedido y se ha liberado el stock reservado, sin embargo no se pudo actualizar el estado en SAP", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                                    Exit Sub
                                                                End Try

                                                            Else
                                                                SplashScreenManager.CloseForm(False)
                                                                XtraMessageBox.Show("No se pudo eliminar el pedido.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                            End If

                                                        End If

                                                    Else

                                                        If clsLnTrans_pe_enc.Eliminar_Pedido_By_IdPedidoEnc_And_Referencia(pBePedidoEnc,
                                                                                                                           AP.Bodega.Eliminar_Documento_Salida,
                                                                                                                           AP.UsuarioAp.IdUsuario) Then

                                                            SplashScreenManager.CloseForm(False)

                                                            XtraMessageBox.Show("Se ha eliminado el pedido y se ha liberado el stock reservado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                                                            Listar_Pedidos()

                                                        Else
                                                            XtraMessageBox.Show("No se pudo eliminar el pedido.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        End If

                                                    End If

                                                Else

                                                    Dim vInterfaceSAP As Boolean = clsLnI_nav_config_enc.Get_Interface_SAP(AP.IdConfiguracionInterface)

                                                    If clsLnTrans_pe_enc.Eliminar_Pedido_By_IdPedidoEnc_And_Referencia(pBePedidoEnc,
                                                                                                                       AP.Bodega.Eliminar_Documento_Salida,
                                                                                                                       AP.UsuarioAp.IdUsuario) Then

                                                        Listar_Pedidos()

                                                        SplashScreenManager.CloseForm(False)

                                                        Try

                                                            If vInterfaceSAP Then
                                                                'EJC202403271301: Actualizar el estado enviado a WMS a 2, para que se peuda volver a importar.
                                                                If Ejecutar_Interface("8-" & AP.IdConfiguracionInterface & "-" & gIndiceInstancia & "-" & AP.UsuarioAp.IdUsuario & "-" & pBePedidoEnc.Referencia & "-2" & "-" & clsBD.Instancia.NombreInstancia, Me) Then
                                                                    XtraMessageBox.Show("Se ha eliminado el pedido y se ha liberado el stock reservado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                                End If
                                                            End If

                                                        Catch ex As Exception
                                                            XtraMessageBox.Show("Se ha eliminado el pedido y se ha liberado el stock reservado, sin embargo no se pudo actualizar el estado en SAP", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                            Exit Sub
                                                        End Try

                                                    Else
                                                        SplashScreenManager.CloseForm(False)
                                                        XtraMessageBox.Show("No se pudo eliminar el pedido.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    End If

                                                End If

                                            End If

                                        End With

                                    End Using

                                End If

                            End If

                        End If

                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally

            If Not SplashScreenManager.Default Is Nothing Then
                If SplashScreenManager.Default.IsSplashFormVisible Then
                    SplashScreenManager.CloseForm(False)
                End If
            End If

        End Try

    End Sub

    Private Sub mnuExportarExcel_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuExportarExcel.ItemClick

        Try

            Exportar_Grid_A_Excel(DgridPedido, "WMS_ListaPedidos_" & DateTime.Now.ToString("yyyy_MM_dd") & ".xlsx")

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Exportar_Grid_A_Excel(ByRef dGrid As GridControl, ByVal NomArchivo As String)

        Try

            Try

                Dim myStream As Stream
                Dim saveFileDialog1 As New SaveFileDialog()

                saveFileDialog1.Filter = "xlsx files (*.xlsx)|*.xlsx"
                saveFileDialog1.FilterIndex = 1
                saveFileDialog1.RestoreDirectory = True
                saveFileDialog1.FileName = NomArchivo

                If saveFileDialog1.ShowDialog() = DialogResult.OK Then
                    myStream = saveFileDialog1.OpenFile()
                    If (myStream IsNot Nothing) Then
                        ' Code to write the stream goes here.
                        dGrid.ExportToXlsx(myStream)
                        myStream.Close()
                    End If
                End If

            Catch ex As Exception
                XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
            End Try

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub gviewEncabezadoPedido_RowStyle(sender As Object, e As RowStyleEventArgs) Handles gviewEncabezadoPedido.RowStyle

        Try

            If e.RowHandle >= 0 Then

                Dim View As GridView = sender

                '#EJC20210223: Formateo condicional de columnas por reabasto.
                If gviewEncabezadoPedido.RowCount > 0 Then

                    If View.Columns.ColumnByFieldName("referencia") IsNot Nothing Then

                        Dim vReferencia As String = View.GetRowCellDisplayText(e.RowHandle, View.Columns("referencia"))

                        Dim PedidoIncompleto As Boolean = DTPedidosIncompletosReserva.AsEnumerable().Any(Function(row) row.Field(Of String)("No") = vReferencia)

                        If (PedidoIncompleto) Then
                            e.Appearance.BackColor = Color.White
                            e.Appearance.BackColor2 = Color.LightSalmon
                        Else
                            e.Appearance.BackColor = Color.White
                            e.Appearance.BackColor2 = Color.White
                        End If

                    End If

                    If View.Columns.ColumnByFieldName("IdPrioridadPicking") IsNot Nothing Then

                        Dim vReferencia As String = View.GetRowCellDisplayText(e.RowHandle, View.Columns("IdPrioridadPicking"))

                        Select Case vReferencia

                            Case 0

                            Case 1

                            Case 2

                        End Select

                        'If (PedidoIncompleto) Then
                        '    e.Appearance.BackColor = Color.White
                        '    e.Appearance.BackColor2 = Color.LightSalmon
                        'Else
                        '    e.Appearance.BackColor = Color.White
                        '    e.Appearance.BackColor2 = Color.White
                        'End If

                    End If

                End If

            End If

        Catch ex As Exception

            If Not SplashScreenManager.Default Is Nothing Then
                If SplashScreenManager.Default.IsSplashFormVisible Then
                    SplashScreenManager.CloseForm(False)
                End If
            End If

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub gviewEncabezadoPedido_ColumnFilterChanged(sender As Object, e As EventArgs) Handles gviewEncabezadoPedido.ColumnFilterChanged
        lblRegs.Caption = String.Format("Registros: {0}", String.Format("{0:#,##0.00}", gviewEncabezadoPedido.DataRowCount.ToString()))
    End Sub

    Private Sub chkSinExistencias_CheckedChanged(sender As Object, e As ItemClickEventArgs) Handles chkSinExistencias.CheckedChanged
        Listar_Pedidos()
    End Sub

    Private Sub chkSinExistenciasERP_CheckedChanged(sender As Object, e As ItemClickEventArgs) Handles chkSinExistenciasERP.CheckedChanged
        Listar_Pedidos()
    End Sub
End Class