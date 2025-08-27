Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen

Public Class frmServicios_List

    Public pIdBodega As Integer
    Public pIdPropietario As Integer
    Public gBeOrdenCompra As New clsBeTrans_oc_enc
    Public gBePedidoEnc As New clsBeTrans_pe_enc

    Private DT As New DataTable

    Public Property TipoTransaccion As pTipoTransaccion
    Enum pTipoTransaccion
        Ingreso = 1
        Salida = 2
    End Enum

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Call_Bind_Servicios As New MethodInvoker(AddressOf Bind_Servicios_To_Grid)

    Public Sub Bind_Servicios_To_Grid()

        Try

            If (IsHandleCreated) Then

                SyncLock Dgrid

                    Dgrid.DataSource = Nothing

                    Dgrid.BeginUpdate()

                    Dgrid.DataSource = DT

                    If GridView1.Columns.Count > 0 Then

                        GridView1.Columns("IdBodega").Visible = False
                        GridView1.Columns("IdPropietario").Visible = False

                        Dim ColActivo As GridColumn = GridView1.Columns.ColumnByFieldName("activo")
                        If Not ColActivo Is Nothing Then
                            ColActivo.Visible = False
                        End If

                        'GridView1.Columns("Activo").Visible = False
                        'GridView1.Columns("IdPropietarioBodega").Visible = False
                        'GridView1.Columns("es_devolucion").Visible = False
                        GridView1.Columns("MI3_Estatus").Visible = False

                        Dim col = New DevExpress.XtraGrid.Columns.GridColumn With
                        {.Name = "Enviado_A_ERP_Flag",
                        .Caption = "MI3_Estatus",
                        .FieldName = "Enviado_A_ERP_Flag",
                        .UnboundType = DevExpress.Data.UnboundColumnType.Object,
                        .ColumnEdit = RepositoryItemPictureEdit1}

                        If GridView1.Columns.ColumnByName("Enviado_A_ERP_Flag") Is Nothing Then
                            GridView1.Columns.Add(col)
                        End If

                        If Not AP.Bodega.Es_Bodega_Fiscal Then

                            '#EJC20210715: Buscar columnas por nombre en grid antes de colocar visible/invisible.
                            Dim ColNoOrden = GridView1.Columns.FirstOrDefault(Function(x) x.FieldName = "no_orden")
                            If Not ColNoOrden Is Nothing Then GridView1.Columns(ColNoOrden.FieldName).Visible = False

                            Dim ColNoPoliza = GridView1.Columns.FirstOrDefault(Function(x) x.FieldName = "No_Poliza")
                            If Not ColNoPoliza Is Nothing Then GridView1.Columns(ColNoPoliza.FieldName).Visible = False

                        End If

                        col.Visible = True

                    End If

                    If GridView1.RowCount > 0 Then
                        lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)
                    End If

                    Try
                        GridView1.OptionsView.ColumnAutoWidth = False
                        GridView1.BestFitColumns()
                    Catch ex As Exception
                    End Try

                    Dgrid.EndUpdate()

                    GridView1.LayoutChanged()

                End SyncLock

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Public Sub Listar_Servicios()

        Try

            If AP.IdBodega = 0 AndAlso pIdPropietario = 0 Then
                DT = clsLnTrans_servicio_enc.GetAll(chkActivos.Checked, dtpFechaDel.Value, dtpFechaAl.Value, 0, 0)

            Else
                DT = clsLnTrans_servicio_enc.GetAll(chkActivos.Checked, dtpFechaDel.Value, dtpFechaAl.Value, AP.IdBodega, pIdPropietario)
                gBeOrdenCompra = Nothing
            End If

            If IsHandleCreated Then
                BeginInvoke(Call_Bind_Servicios)
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
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

    Private Sub Dgrid_DoubleClick(sender As Object, e As EventArgs) Handles Dgrid.DoubleClick
        Procesa_Registro()
    End Sub

    Private Sub Procesa_Registro()

        Try

            If (GridView1.RowCount > 0) Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow()

                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormDescription("Documento de Servicios...")
                Application.DoEvents()

                gBeOrdenCompra = New clsBeTrans_oc_enc
                gBePedidoEnc = New clsBeTrans_pe_enc

                Dim IdServicioenc As Integer = Integer.Parse(Dr.Item("IdServicioEnc"))
                Dim EsIngreso As Boolean = Boolean.Parse(Dr.Item("Es_Ingreso"))

                '#EJC20210322: Antes
                'gBeOrdenCompra.IdBodega = gBeOrdenCompra.ProveedorBodega.IdBodega

                If EsIngreso Then
                    Dim IdOrdenCompraEnc As Integer = IIf(IsDBNull(Dr.Item("IdDocumentoIngreso")), 0, Dr.Item("IdDocumentoIngreso"))
                    If IdOrdenCompraEnc > 0 Then
                        gBeOrdenCompra = clsLnTrans_oc_enc.GetSingle(IdOrdenCompraEnc)
                        gBeOrdenCompra.IdBodega = gBeOrdenCompra.IdBodega
                    End If
                    gBePedidoEnc = Nothing
                Else
                    Dim IdPedidoEnc As Integer = IIf(IsDBNull(Dr.Item("IdDocumentoSalida")), 0, Dr.Item("IdDocumentoSalida"))
                    If IdPedidoEnc > 0 Then
                        gBePedidoEnc = clsLnTrans_pe_enc.GetSingle(IdPedidoEnc)
                        gBePedidoEnc.IdBodega = gBePedidoEnc.IdBodega
                    End If
                    gBeOrdenCompra = Nothing
                End If

                Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle

                If Modo = pModo.Lista Then

                    Cierra_Instancia_Previa(frmRegServicio)

                    With frmRegServicio
                        .Modo = frmRegServicio.ModoTrans.Editar
                        .gBeOrdenCompra = gBeOrdenCompra
                        .gBePedidoEnc = gBePedidoEnc
                        .TipoTransaccion = IIf(EsIngreso, frmRegServicio.pTipoTransaccion.Ingreso, frmRegServicio.pTipoTransaccion.Salida)
                        .gBeServicio.IdServicioEnc = IdServicioenc
                        clsLnTrans_servicio_enc.GetSingle(.gBeServicio)
                        .InvokeListarServicios = AddressOf Listar_Servicios
                        .MdiParent = MdiParent
                        .WindowState = FormWindowState.Normal

                        If OpcionesMenu IsNot Nothing Then
                            .OpcionesMenu = OpcionesMenu
                            .mnuGuardar.Enabled = .OpcionesMenu.Modificar
                            .mnuActualizar.Enabled = .OpcionesMenu.Modificar
                            .mnuEliminar.Enabled = .OpcionesMenu.Eliminar
                        End If

                        .Show()
                        .Focus()
                    End With

                    GridView1.FocusedRowHandle = lSelectionIndex

                ElseIf Modo = pModo.Seleccion Then
                    DialogResult = DialogResult.OK
                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub chkActivos_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkActivos.CheckedChanged
        Listar_Servicios()
    End Sub

    Private Sub mnuSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSalir.ItemClick
        Close()
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        Listar_Servicios()
    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick
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
            printLink.Component = Dgrid
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

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

            'If dtpFechaDel.Value > dtpFechaAl.Value Or dtpFechaAl.Value < dtpFechaDel.Value Then
            '    Throw New Exception("Seleccione un rango de fechas válido.")
            'End If

            Listar_Servicios()

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

            'If dtpFechaDel.Value > dtpFechaAl.Value Or dtpFechaAl.Value < dtpFechaDel.Value Then
            '    Throw New Exception("Seleccione un rango de fechas válido.")
            'End If

            Listar_Servicios()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub GridView1_RowStyle(sender As Object, e As RowStyleEventArgs) Handles GridView1.RowStyle

        Try

            GridView1.OptionsBehavior.Editable = False
            GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
            GridView1.FocusRectStyle = DrawFocusRectStyle.RowFocus
            GridView1.OptionsSelection.EnableAppearanceFocusedRow = True
            GridView1.OptionsSelection.EnableAppearanceHideSelection = True
            GridView1.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            GridView1.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            GridView1.Appearance.FocusedRow.ForeColor = Color.White
            GridView1.Appearance.SelectedRow.ForeColor = Color.White
            GridView1.Appearance.SelectedRow.Options.UseBackColor = True
            GridView1.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub mnuMI3Sync_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuMI3Sync.ItemClick

        Try

            mnuMI3Sync.Enabled = False

            If AP.IdConfiguracionInterface <> -1 Then
                If Ejecutar_Interface("6-" & AP.IdConfiguracionInterface & "-" & gIndiceInstancia & "-" & AP.UsuarioAp.IdUsuario & "-0-0" & "-" & clsBD.Instancia.NombreInstancia, Me) Then
                    Listar_Servicios()
                End If
            Else
                XtraMessageBox.Show("El archivo de configuración .ini no tiene un identificador de configuración para interface",
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation)
            End If

            mnuMI3Sync.Enabled = True

        Catch ex As Exception
            mnuMI3Sync.Enabled = True
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub Dgrid_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgrid.KeyDown
        If e.KeyCode = Keys.Enter AndAlso GridView1.RowCount > 0 Then
            Procesa_Registro()
        End If
    End Sub

    Private Sub frmServicios_List_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            Listar_Servicios()

            If TipoTransaccion = pTipoTransaccion.Ingreso Then
                Me.Text = "Listado de registro de servicios de Ingreso"
            Else
                Me.Text = "Listado de registro de servicios de Salidas"
            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub frmServicios_List_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub GridView1_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles GridView1.RowCellStyle

        Try

            If e.RowHandle > -1 Then

                Dim View As GridView = sender

                If e.Column.FieldName = "Enviado_A_ERP" Then

                    Dim lObj As Object = View.GetRowCellValue(e.RowHandle, View.Columns("Enviado_A_ERP"))

                    If lObj IsNot Nothing Then

                        If Not lObj Then
                            e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                            e.Appearance.BackColor = Color.Salmon
                            e.Appearance.BackColor2 = Color.SeaShell
                        Else
                            e.Appearance.BackColor = ColorTranslator.FromHtml("#63C76B")
                            e.Appearance.BackColor2 = Color.Transparent
                        End If

                    End If

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

    Private Sub GridView1_CustomUnboundColumnData(sender As Object, e As CustomColumnDataEventArgs) Handles GridView1.CustomUnboundColumnData

        Try

            'Dim View As GridView = sender
            Dim view As GridView = TryCast(sender, GridView)

            If e.Column.FieldName = "Enviado_A_ERP_Flag" AndAlso e.IsGetData() Then

                Dim rowHandle As Integer = view.GetRowHandle(e.ListSourceRowIndex)
                Dim Enviado_A_Erp As Object = view.GetRowCellValue(rowHandle, view.Columns("Enviado_A_ERP"))
                Dim Estado As Object = view.GetRowCellValue(rowHandle, view.Columns("Estado"))
                Dim vEnviado_A_Erp As Boolean = False

                If Not Enviado_A_Erp Is Nothing Then
                    Boolean.TryParse(Enviado_A_Erp.ToString(), vEnviado_A_Erp)
                Else
                    Enviado_A_Erp = False
                End If


                If Enviado_A_Erp IsNot Nothing Then

                    Dim iconRed As Image = ImageCollection1.Images(0)
                    Dim iconGreen As Image = ImageCollection1.Images(1)
                    Dim iconYellow As Image = ImageCollection1.Images(2)

                    If Estado Is Nothing Then Estado = "NUEVA"

                    If Estado.ToString = "NUEVA" Then
                        'e.Value = 0 'No se le ha generado recepción ni se ha procesado.
                        'RepositoryItemImageEdit1.ContextImageOptions.Image = iconYellow
                        'RepositoryItemImageEdit1.ContextImageOptions.Image = ImageCollection1.Images(2)
                        e.Value = ImageCollection1.Images(2)
                    Else
                        If Not vEnviado_A_Erp Then
                            'e.Value = 1 ' iconRed 'Ya se procesó pero no se pudo enviar a ERP.
                            'RepositoryItemImageEdit1.ContextImageOptions.Image = ImageCollection1.Images(1)
                            e.Value = ImageCollection1.Images(0) '#CKFK 20180509 07:20 AM Modifiqué el indice de la imagen para que fuera la roja
                        Else
                            'e.Value = 0 'iconGreen 'Ya se procesó y se envió correctamente a ERP.
                            'RepositoryItemImageEdit1.ContextImageOptions.Image = iconGreen
                            'RepositoryItemImageEdit1.ContextImageOptions.Image = ImageCollection1.Images(0)
                            e.Value = ImageCollection1.Images(1) '#CKFK 20180509 07:20 AM Modifiqué el indice de la imagen para que fuera la verde
                        End If
                    End If

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

    Private Sub Nuevo_Documento_Ingreso(ByVal Consolidado As Boolean)

        Try

            Cierra_Instancia_Previa(frmOrdenCompra)

            With frmRegServicio
                .Modo = frmRegServicio.ModoTrans.Nuevo
                .TipoTransaccion = TipoTransaccion
                .MdiParent = MdiParent
                .WindowState = FormWindowState.Normal
                .InvokeListarServicios = AddressOf Listar_Servicios

                If OpcionesMenu IsNot Nothing Then
                    .OpcionesMenu = OpcionesMenu
                    .mnuGuardar.Enabled = .OpcionesMenu.Modificar
                    .mnuActualizar.Enabled = .OpcionesMenu.Modificar
                    .mnuEliminar.Enabled = .OpcionesMenu.Eliminar
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

    Private Sub mnuNuevo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuNuevo.ItemClick

        Try
            mnuNuevo.Enabled = False

            Nuevo_Documento_Ingreso(False)

            mnuNuevo.Enabled = True

        Catch ex As Exception

        Finally
            mnuNuevo.Enabled = True
        End Try

    End Sub

End Class