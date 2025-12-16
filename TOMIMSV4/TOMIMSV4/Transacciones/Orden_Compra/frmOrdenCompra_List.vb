Imports System.IO
Imports System.Reflection
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen

Public Class frmOrdenCompra_List

    Public pIdBodega As Integer
    Public pIdPropietario As Integer

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol
    Public gBeOrdenCompra As clsBeTrans_oc_enc
    Private DT As New DataTable
    Public Property vNombreArchivoLayOutGrid As String = ""
    Public Property IsLoading As Boolean = True

    Public Sub New()
        InitializeComponent()
        IsLoading = False
    End Sub

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Public Call_Bind_Pedidos_Compra As New MethodInvoker(AddressOf Bind_Pedidos_Compra_To_Grid)

    Public Sub Bind_Pedidos_Compra_To_Grid()

        Try

            IsLoading = True

            If (IsHandleCreated) Then

                SyncLock Dgrid

                    Dgrid.DataSource = Nothing

                    Dgrid.BeginUpdate()

                    Dgrid.DataSource = DT

                    If GridView1.Columns.Count > 0 Then

                        GridView1.Columns("IdBodega").Visible = False
                        GridView1.Columns("IdPropietario").Visible = False
                        GridView1.Columns("Activo").Visible = False
                        GridView1.Columns("IdPropietarioBodega").Visible = False
                        GridView1.Columns("es_devolucion").Visible = False
                        GridView1.Columns("Enviado_A_ERP").Visible = False

                        Dim col = New Columns.GridColumn With
                        {.Name = "Enviado_A_ERP_Flag",
                        .Caption = "MI3_Estatus",
                        .FieldName = "Enviado_A_ERP_Flag",
                        .UnboundType = DevExpress.Data.UnboundColumnType.Object,
                        .ColumnEdit = RepositoryItemPictureEdit1}

                        If GridView1.Columns.ColumnByName("Enviado_A_ERP_Flag") Is Nothing Then
                            GridView1.Columns.Add(col)
                        End If

                        If Not AP.Bodega.Es_Bodega_Fiscal Then

                            Dim ColNoOrden = GridView1.Columns.FirstOrDefault(Function(x) x.FieldName = "NoOrden")
                            If Not ColNoOrden Is Nothing Then GridView1.Columns("NoOrden").Visible = False

                            Dim ColNoPoliza = GridView1.Columns.FirstOrDefault(Function(x) x.FieldName = "NoPoliza")
                            If Not ColNoPoliza Is Nothing Then GridView1.Columns("NoPoliza").Visible = False

                        End If

                        col.Visible = True

                    End If

                    If GridView1.RowCount > 0 Then
                        lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)
                    End If

                    Try

                        Set_LayOut_Grid()

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
        Finally
            IsLoading = False
        End Try

    End Sub

    Public Sub Listar_Pedidos_Compra()

        Try

            If AP.IdBodega = 0 AndAlso pIdPropietario = 0 Then
                DT = clsLnTrans_oc_enc.GetAll(chkActivos.Checked,
                                              dtpFechaDel.Value,
                                              dtpFechaAl.Value,
                                              0,
                                              0)
            Else
                DT = clsLnTrans_oc_enc.GetAll(chkActivos.Checked,
                                              dtpFechaDel.Value,
                                              dtpFechaAl.Value,
                                              AP.IdBodega,
                                              pIdPropietario)
            End If

            If IsHandleCreated Then
                BeginInvoke(Call_Bind_Pedidos_Compra)
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

            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace)

        End Try

    End Sub

    Private Sub Dgrid_DoubleClick(sender As Object, e As EventArgs) Handles Dgrid.DoubleClick

        Try

            If (GridView1.RowCount > 0) Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow

                If Dr Is Nothing Then Exit Sub

                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)

                If Dr.Item("es_devolucion") Then
                    SplashScreenManager.Default.SetWaitFormCaption("Devolución")
                Else
                    SplashScreenManager.Default.SetWaitFormCaption("Documento de Ingreso..")
                End If

                gBeOrdenCompra = New clsBeTrans_oc_enc
                Dim IdOrdenCompraEnc As Integer = Integer.Parse(Dr.Item("Código"))

                Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle
                GridView1.FocusedRowHandle = lSelectionIndex

                Procesar_Registro(IdOrdenCompraEnc)

            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)

            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace)

        End Try

    End Sub

    Public Sub Procesar_Registro(ByVal IdOrdenCompraEnc As Integer)

        Try

            If (IdOrdenCompraEnc > 0) Then

                gBeOrdenCompra = clsLnTrans_oc_enc.GetSingle(IdOrdenCompraEnc)
                gBeOrdenCompra.IdBodega = gBeOrdenCompra.IdBodega

                If Modo = pModo.Lista Then

                    Cierra_Instancia_Previa(frmOrdenCompra)

                    '#MECR03102025: Se agrego nueva bitacora de logs para OC
                    Dim vMsgAdvertencia As String = "ADVERTENCIA_202302231652: El IdUsuario: " & AP.UsuarioAp.IdUsuario & " abrió el IdOrdenCompraEnc: " & gBeOrdenCompra.IdOrdenCompraEnc
                    'clsLnLog_error_wms.Agregar_Error("ADVERTENCIA_202302231652: El IdUsuario: " & AP.UsuarioAp.IdUsuario & " abrió el IdOrdenCompraEnc: " & gBeOrdenCompra.IdOrdenCompraEnc)
                    clsLnLog_error_wms_oc.Agregar_Error(vMsgAdvertencia, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, pIdOCEnc:=gBeOrdenCompra.IdOrdenCompraEnc)

                    With frmOrdenCompra

                        .Modo = frmOrdenCompra.ModoTrans.Editar
                        .gBeOrdenCompra = gBeOrdenCompra
                        .TipoTrans = IIf(gBeOrdenCompra.TipoIngreso.Es_Poliza_Consolidada,
                                         frmOrdenCompra.eTipoTrans.Consolidado,
                                         frmOrdenCompra.eTipoTrans.Simple)
                        .InvokeListarPedidosCompra = AddressOf Listar_Pedidos_Compra
                        .InvokeCargarPedidoCompra = AddressOf Procesar_Registro
                        .MdiParent = MdiParent
                        .WindowState = FormWindowState.Normal
                        .Text = gBeOrdenCompra.IdOrdenCompraEnc & " - " & gBeOrdenCompra.Referencia

                        If OpcionesMenu IsNot Nothing Then
                            .OpcionesMenu = OpcionesMenu
                        End If

                        .Show()
                        .Focus()

                    End With

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
        Listar_Pedidos_Compra()
    End Sub

    Private Sub mnuSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSalir.ItemClick
        Close()
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        Listar_Pedidos_Compra()
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

            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace)

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

            Listar_Pedidos_Compra()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace)

        End Try

    End Sub

    Private Sub dtpFechaAl_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaAl.ValueChanged

        Try

            Listar_Pedidos_Compra()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace)

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

            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace)

        End Try

    End Sub

    Private Sub mnuMI3Sync_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuMI3Sync.ItemClick

        Dim BeConfigEnc As New clsBeI_nav_config_enc

        Try

            mnuMI3Sync.Enabled = False

            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(clsBD.Instancia.IdConfiguracionInterface)

            If clsBD.Instancia.IdConfiguracionInterface = 1989 Then
                If Ejecutar_Interface("1989-" & AP.IdConfiguracionInterface & "-" & gIndiceInstancia & "-" & AP.UsuarioAp.IdUsuario & "-0-0" & "-" & clsBD.Instancia.NombreInstancia, Me) Then
                    Listar_Pedidos_Compra()
                End If
            Else

                Dim vArgumentosAEnviarAInterface As String = "6-" & AP.IdConfiguracionInterface & "-" & gIndiceInstancia & "-" & AP.UsuarioAp.IdUsuario & "-0-0" & "-" & clsBD.Instancia.NombreInstancia

                If Not BeConfigEnc Is Nothing Then
                    If BeConfigEnc.Interface_SAP Then
                        '#EJC202402160626: Recibir pedidos de compra / documentos de ingreso.
                        vArgumentosAEnviarAInterface = "3-" & AP.IdConfiguracionInterface & "-" & gIndiceInstancia & "-" & AP.UsuarioAp.IdUsuario & "-0-0" & "-" & clsBD.Instancia.NombreInstancia
                    End If
                End If

                If AP.IdConfiguracionInterface <> -1 Then
                    If Ejecutar_Interface(vArgumentosAEnviarAInterface, Me) Then
                        Listar_Pedidos_Compra()
                    End If
                Else
                    XtraMessageBox.Show("El archivo de configuración .ini no tiene un identificador de configuración para interface",
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation)
                End If
            End If

            mnuMI3Sync.Enabled = True

        Catch ex As Exception
            mnuMI3Sync.Enabled = True
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub Dgrid_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgrid.KeyDown
        If e.KeyCode = Keys.Enter AndAlso GridView1.RowCount > 0 Then
            Dgrid_DoubleClick(sender, e)
        End If
    End Sub

    Private Sub frmOrdenCompra_List_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            If AP.Bodega.Rango_Dias_Documentos > 0 Then
                dtpFechaDel.Value = Now.Date.AddDays(-(AP.Bodega.Rango_Dias_Documentos))
                dtpFechaAl.Value = Now.Date.AddDays(AP.Bodega.Rango_Dias_Documentos)
            End If

            mnuNuevo.Enabled = clsLnMenu_rol.Permiso_Funcionalidad("2.1.1.1", AP.IdRol)
            vNombreArchivoLayOutGrid = "grdPedidoListDocIngresos.xml"

            If clsLnMenu_rol.Permiso_Funcionalidad("3.2.1.2", AP.IdRol) Then
                mnuEliminarDocumentoIngreso.Visibility = BarItemVisibility.Always
            Else
                mnuEliminarDocumentoIngreso.Visibility = BarItemVisibility.Never
            End If

            Listar_Pedidos_Compra()

            '#EJC20210317: Verificar el parámetro para hablitar o no el botón
            AP.Bodega = clsLnBodega.GetSingle_By_Idbodega(AP.IdBodega)

            If Not AP.Bodega.habilitar_ingreso_consolidado Then
                mnuNuevoIngresoConsolidados.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            Else
                mnuNuevoIngresoConsolidados.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            End If

        Catch ex As Exception
            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace)
        End Try

    End Sub

    Private Sub frmOrdenCompra_List_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub GridView1_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles GridView1.RowCellStyle

        Try

            If e.RowHandle > -1 Then

                Dim View As GridView = sender

                If e.Column.FieldName = "Enviado_A_ERP" Then

                    '#EJC202207190733
                    Dim lObj As Object = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, View.Columns("Enviado_A_ERP"))), False, View.GetRowCellValue(e.RowHandle, View.Columns("Enviado_A_ERP")))

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

            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace)

        End Try

    End Sub

    Private Sub GridView1_CustomUnboundColumnData(sender As Object, e As CustomColumnDataEventArgs) Handles GridView1.CustomUnboundColumnData

        Try

            Dim view As GridView = TryCast(sender, GridView)

            If e.Column.FieldName = "Enviado_A_ERP_Flag" AndAlso e.IsGetData() Then

                Dim rowHandle As Integer = view.GetRowHandle(e.ListSourceRowIndex)
                Dim Enviado_A_Erp As Object = view.GetRowCellValue(rowHandle, view.Columns("Enviado_A_ERP"))
                Dim Estado As Object = view.GetRowCellValue(rowHandle, view.Columns("Estado"))
                Dim vEnviado_A_Erp As Boolean = False

                Boolean.TryParse(Enviado_A_Erp.ToString(), vEnviado_A_Erp)

                If Enviado_A_Erp IsNot Nothing Then

                    Dim iconRed As Image = ImageCollection1.Images(0)
                    Dim iconGreen As Image = ImageCollection1.Images(1)
                    Dim iconYellow As Image = ImageCollection1.Images(2)

                    If Estado.ToString = "NUEVA" Then
                        e.Value = ImageCollection1.Images(2)
                    Else
                        If Not vEnviado_A_Erp Then
                            e.Value = ImageCollection1.Images(0)
                        Else
                            e.Value = ImageCollection1.Images(1)
                        End If
                    End If

                End If

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace)

        End Try

    End Sub

    Private Sub Nuevo_Documento_Ingreso(ByVal Consolidado As Boolean)

        Try

            Cierra_Instancia_Previa(frmOrdenCompra)

            With frmOrdenCompra
                .Modo = frmOrdenCompra.ModoTrans.Nuevo
                If Consolidado Then .TipoTrans = frmOrdenCompra.eTipoTrans.Consolidado
                .MdiParent = MdiParent
                .InvokeListarPedidosCompra = AddressOf Listar_Pedidos_Compra
                .WindowState = FormWindowState.Normal
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

            If clsLnMenu_rol.Permiso_Funcionalidad("2.1.1", AP.IdRol) Then
                Nuevo_Documento_Ingreso(False)
            Else
                XtraMessageBox.Show("No tiene permisos para crear nuevos documentos de ingreso",
                                    Text,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation)
            End If

            mnuNuevo.Enabled = True

        Catch ex As Exception

        Finally
            mnuNuevo.Enabled = True
        End Try

    End Sub

    Private Sub mnuNuevoIngresoConsolidados_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuNuevoIngresoConsolidados.ItemClick

        Try

            mnuNuevoIngresoConsolidados.Enabled = False

            If clsLnMenu_rol.Permiso_Funcionalidad("2.1.1", AP.IdRol) Then
                Nuevo_Documento_Ingreso(True)
            Else
                XtraMessageBox.Show("No tiene permisos para crear nuevos documentos de ingreso",
                                    Text,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation)
            End If

            mnuNuevoIngresoConsolidados.Enabled = True

        Catch ex As Exception
            mnuNuevoIngresoConsolidados.Enabled = True
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            mnuNuevoIngresoConsolidados.Enabled = True
        End Try

    End Sub

    Private Sub mnuExportarExcel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuExportarExcel.ItemClick

        Try

            Exportar_Grid_A_Excel(Dgrid, "WMS_ListaDocumentosIngreso_" & Now.ToString("yyyy_MM_dd") & ".xlsx")

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
            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace)
        End Try

    End Sub

    Private Sub mnuEliminarLayoutGrid_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminarLayoutGrid.ItemClick

        Try

            If File.Exists(vNombreArchivoLayOutGrid) Then
                File.Delete(vNombreArchivoLayOutGrid)
                mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Never
            End If

            XtraMessageBox.Show("Diseño de grid eliminado!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Close()

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                              Text,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error)

            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace)

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
                GridView1.RestoreLayoutFromStream(BeConfiguracionUsuarioDet.Stream_Template)
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub GridView_Layout(sender As Object, e As EventArgs) Handles GridView1.Layout

        Try

            If IsLoading Then Exit Sub

            Dim Ms As New MemoryStream
            GridView1.SaveLayoutToStream(Ms)
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

            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace)

        End Try

    End Sub

    Private Sub gviewEncabezadoPedido_ColumnFilterChanged(sender As Object, e As EventArgs) Handles GridView1.ColumnFilterChanged
        lblRegs.Caption = String.Format("Registros: {0}", String.Format("{0:#,##0.00}", GridView1.DataRowCount.ToString()))
    End Sub

    Private Sub mnuEliminarDocumentoIngreso_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuEliminarDocumentoIngreso.ItemClick

        Try

            If Not permiteMenu("3.2.1.2") Then
                Return
            End If

            If (GridView1.RowCount > 0) Then

                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormDescription("Eliminando documento...")

                Dim Dr As DataRowView = GridView1.GetFocusedRow

                If Not Dr Is Nothing Then

                    gBeOrdenCompra = New clsBeTrans_oc_enc
                    gBeOrdenCompra = clsLnTrans_oc_enc.GetSingle(Dr.Item("Código"))

                    Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle

                    If Modo = pModo.Lista Then

                        If clsLnTrans_re_oc.Tiene_Recepciones(gBeOrdenCompra.IdOrdenCompraEnc) Then

                            SplashScreenManager.CloseForm(False)

                            XtraMessageBox.Show("No se puede eliminar el documento tiene una o mas tareas de recepción asociadas.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)

                        Else

                            If gBeOrdenCompra.IdEstadoOC = clsDataContractDI.tEstadoOC.NUEVA Then

                                SplashScreenManager.CloseForm(False)

                                If XtraMessageBox.Show("¿Eliminar documento de ingreso:" & gBeOrdenCompra.Referencia & "?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                                    SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                                    SplashScreenManager.Default.SetWaitFormDescription("Eliminando...")

                                    If clsLnTrans_oc_enc.Eliminar_OC(gBeOrdenCompra, AP.UsuarioAp) Then

                                        '#MECR03102025: Se agrego nueva bitacora de logs para OC
                                        Dim vMsgAdvertencia As String = "ADVERTENCIA_202302231700: El IdUsuario: " & AP.UsuarioAp.IdUsuario & " eliminó la Orden Compra Enc: " & gBeOrdenCompra.IdOrdenCompraEnc
                                        'clsLnLog_error_wms.Agregar_Error("ADVERTENCIA_202302231700: El IdUsuario: " & AP.UsuarioAp.IdUsuario & " eliminó la Orden Compra Enc: " & gBeOrdenCompra.IdOrdenCompraEnc)
                                        clsLnLog_error_wms_oc.Agregar_Error(vMsgAdvertencia, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, pIdOCEnc:=gBeOrdenCompra.IdOrdenCompraEnc)

                                        SplashScreenManager.CloseForm(False)

                                        XtraMessageBox.Show("Documento de ingreso eliminado correctamente.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                                        Listar_Pedidos_Compra()

                                    Else
                                        SplashScreenManager.CloseForm(False)
                                        XtraMessageBox.Show("No se pudo eliminar el documento de ingreso.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    End If

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
End Class