Imports System.IO
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen

Public Class frmPedidoDetalleBuscador

    Public Property pBePedidoEnc As New clsBeTrans_pe_enc
    Public Property pListaPedidos As New List(Of Integer)
    Public Property IdBodega As Integer = 0
    Public Property IdPropietarioBodega As Integer = 0
    Public Property EstadoDespachado As Boolean = False

    Public Property vNombreArchivoLayOutGrid As String = ""

    Public Property Es_Manufactura As Boolean = False


    Public Enum ProcesoSolicitante
        Despacho = 1
        Picking = 0
    End Enum

    Public Property Modo As ProcesoSolicitante = ProcesoSolicitante.Picking

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub frmAgregaPedidoDetalle_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            '#EJC20210716:Restaurar LayoutGrid en LotesPorUbi.
            vNombreArchivoLayOutGrid = "grdPedidoDetalleBuscador.xml"

            chkMostrarSoloMisDocumentos.Caption = "Mostrar solo mis documentos (" & AP.UsuarioAp.Nombres & ")"

            chkMostrarSoloMisDocumentos.Checked = AP.Bodega.Filtrar_Pedidos_Usuario

            Listar_Pedidos()

            pBePedidoEnc = Nothing

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try


    End Sub

    Private Sub Listar_Pedidos()

        Try

            Dim Dt As New DataTable

            Dim pedidos As String = ""

            If pListaPedidos.Count > 0 Then

                For Each reg In pListaPedidos
                    pedidos += reg & ","
                Next

                pedidos = pedidos.Substring(0, pedidos.Length - 1)
                pedidos = String.Format(" and pedido not in ({0})", pedidos)

            End If

            Dim vIdUsuarioAgregoDocumento As Integer = 0

            If chkMostrarSoloMisDocumentos.Checked Then
                vIdUsuarioAgregoDocumento = AP.UsuarioAp.IdUsuario
            End If

            '#GT23022024: Si la busqueda proviene de manufactura, mostrar todos los pedidos con estado Pickeado
            If Es_Manufactura Then

                Dt = clsLnTrans_pe_enc.Get_All_Activos_By_Manufactura(dtpFechaDel.Value,
                                                                      dtpFechaAl.Value,
                                                                      Modo,
                                                                      pedidos,
                                                                      IdBodega,
                                                                      EstadoDespachado,
                                                                      IdPropietarioBodega,
                                                                      vIdUsuarioAgregoDocumento,
                                                                      Es_Manufactura)

            Else
                If IdBodega > 0 Then
                    Dt = clsLnTrans_pe_enc.Get_All_Activos(dtpFechaDel.Value,
                                                           dtpFechaAl.Value,
                                                           Modo,
                                                           pedidos,
                                                           IdBodega,
                                                           EstadoDespachado,
                                                           IdPropietarioBodega,
                                                           vIdUsuarioAgregoDocumento,
                                                           tsAnulados.IsOn)

                Else
                    Dt = clsLnTrans_pe_enc.Get_All_Activos(dtpFechaDel.Value,
                                                           dtpFechaAl.Value,
                                                           Modo,
                                                           pedidos,
                                                           AP.IdBodega,
                                                           EstadoDespachado,
                                                           IdPropietarioBodega,
                                                           vIdUsuarioAgregoDocumento,
                                                           tsAnulados.IsOn)
                End If
            End If

            Dgrid.DataSource = Nothing
            Dgrid.DataSource = Dt

            If GridView1.RowCount > 0 Then
                lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)
            End If

            GridView1.Columns("Pedido").GroupIndex = 0
            GridView1.Columns("IdPedidoDet").Visible = False
            GridView1.Columns("IdProducto").Visible = False

            If GridView1.Columns.Count > 0 Then

                GridView1.Columns("NombreCliente").VisibleIndex = 1
                GridView1.Columns("NombreCliente").Caption = "Cliente"

                GridView1.OptionsView.ShowFooter = True

                GridView1.Columns("Cantidad").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Cantidad").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("Cantidad").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Cantidad").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Cantidad_Pickeada").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Cantidad_Pickeada").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("Cantidad_Pickeada").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Cantidad_Pickeada").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Cantidad_Verificada").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Cantidad_Verificada").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("Cantidad_Verificada").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Cantidad_Verificada").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Cantidad_Despachada").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Cantidad_Despachada").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("Cantidad_Despachada").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Cantidad_Despachada").DisplayFormat.FormatString = "{0:n6}"

                GridView1.ExpandAllGroups()

                Restore_LayOut_Grid()

                If Not ExisteLayOut Then
                    GridView1.BestFitColumns(True)
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

    Private Sub Dgrid_DoubleClick(sender As Object, e As EventArgs) Handles Dgrid.DoubleClick

        Try

            If (GridView1.RowCount > 0) Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow

                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormCaption("Pedido")

                Dim lIdPedidoEnc As Integer = Dr.Item("Pedido")

                '#CKFK20171026_0526PM: Se agregó validación para que el pedido no esté incompleto ni anulado
                Dim lEstadoPedido As String = Dr.Item("EstadoPedido")

                If lEstadoPedido = "Anulado" Then

                    SplashScreenManager.CloseForm(False)

                    XtraMessageBox.Show(String.Format("El pedido {0} está en estado {1}, no se puede agregar al picking", lIdPedidoEnc, lEstadoPedido), Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Else

                    '#EJC20171002: Antes se devolvía solo el detalle del pedido, ahora se devuelve el encabezado y el detalle.
                    pBePedidoEnc = New clsBeTrans_pe_enc()
                    pBePedidoEnc.IdPedidoEnc = lIdPedidoEnc
                    pBePedidoEnc = clsLnTrans_pe_enc.GetSingle(lIdPedidoEnc)

                    If pBePedidoEnc.Picking.Requiere_Preparacion Then

                        If Not clsLnTrans_pe_enc.Tiene_Packing(lIdPedidoEnc) Then

                            SplashScreenManager.CloseForm(False)

                            XtraMessageBox.Show(String.Format("El pedido {0} tiene configurado packing y no se ha realizado el proceso, no se puede agregar al picking", lIdPedidoEnc, lEstadoPedido), Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                            DialogResult = DialogResult.Cancel

                        Else

                            Dim vFinalizado As Boolean = False
                            vFinalizado = clsLnTrans_pe_enc.Packing_Finalizado(lIdPedidoEnc)

                            If Not vFinalizado Then

                                SplashScreenManager.CloseForm(False)
                                XtraMessageBox.Show("El packing no se ha finalizado, no se puede despachar", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                                DialogResult = DialogResult.Cancel

                            Else
                                DialogResult = DialogResult.OK
                            End If


                        End If

                    Else
                        DialogResult = DialogResult.OK
                    End If

                End If

                SplashScreenManager.CloseForm(False)

                Hide()

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub mnuSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSalir.ItemClick
        Close()
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        Listar_Pedidos()
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

        Dim reportHeader As String = vbNewLine & "Listado de Pedidos"

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

    Private Sub GridView1_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles GridView1.RowStyle

        Try

            GridView1.OptionsBehavior.Editable = False
            GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
            GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
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

    'Private Sub Guardar_Layout()

    '    Try

    '        Dim Ms As New MemoryStream
    '        GridView1.SaveLayoutToStream(Ms)
    '        Ms.Seek(0, SeekOrigin.Begin)
    '        Dim MsReader As New StreamReader(Ms)
    '        Dim LayoutToText As String = MsReader.ReadToEnd()

    '        clsLnConfiguracion_usuario_enc.Guardar_Layout(AP.IdEmpresa,
    '                                                      AP.UsuarioAp.IdUsuario,
    '                                                      AP.HostName,
    '                                                      vNombreArchivoLayOutGrid,
    '                                                      LayoutToText)


    '        mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

    '    Catch ex As Exception

    '        XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
    '        Text,
    '        MessageBoxButtons.OK,
    '        MessageBoxIcon.Error)

    '        Dim vMsgError As String = ex.Message
    '        clsLnLog_error_wms.Agregar_Error(vMsgError)

    '    End Try

    'End Sub

    Private ExisteLayOut As Boolean = False
    Private Sub Restore_LayOut_Grid()

        Try

            '#EJC20210716:Restaurar LayoutGrid en LotesPorUbi.
            'vNombreArchivoLayOutGrid = "grdPedidoDetalleBuscador.xml"

            Dim BeConfiguracionUsuarioDet As New clsBeConfiguracion_usuario_det

            BeConfiguracionUsuarioDet = clsLnConfiguracion_usuario_enc.Get_Layout(AP.IdEmpresa,
                                                                                  AP.UsuarioAp.IdUsuario,
                                                                                  AP.HostName,
                                                                                  vNombreArchivoLayOutGrid)


            If Not BeConfiguracionUsuarioDet Is Nothing Then
                GridView1.RestoreLayoutFromStream(BeConfiguracionUsuarioDet.Stream_Template)
                mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                ExisteLayOut = True
            Else
                mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                ExisteLayOut = False
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub GridView1_Layout(sender As Object, e As EventArgs) Handles GridView1.Layout
        'Guardar_Layout()
    End Sub

    Private Sub mnuEliminarLayoutGrid_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminarLayoutGrid.ItemClick

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

    Private Sub mnuGuardarLayoutGrid_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardarLayoutGrid.ItemClick
        Try


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

            mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

            XtraMessageBox.Show("Diseño de grid guardado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)


        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Sub

    Private Sub gviewEncabezadoPedido_RowStyle(sender As Object, e As RowStyleEventArgs) Handles GridView1.RowStyle

        Try

            If e.RowHandle >= 0 Then

                Dim View As GridView = sender

                '#EJC20210223: Formateo condicional de columnas por reabasto.
                If GridView1.RowCount > 0 Then

                    If View.Columns.ColumnByFieldName("EstadoPedido") IsNot Nothing Then

                        Dim vEstado As String = View.GetRowCellDisplayText(e.RowHandle, View.Columns("EstadoPedido"))

                        If vEstado = "Anulado" Then
                            e.Appearance.BackColor = Color.White
                            e.Appearance.BackColor2 = Color.LightSalmon
                        Else
                            e.Appearance.BackColor = Color.White
                            e.Appearance.BackColor2 = Color.White
                        End If

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

    Private Sub frmPedidoDetalleBuscador_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        If AP.Bodega.Rango_Dias_Documentos > 0 Then
            dtpFechaDel.Value = Now.Date.AddDays(-(AP.Bodega.Rango_Dias_Documentos))
            dtpFechaAl.Value = Now.Date.AddDays(AP.Bodega.Rango_Dias_Documentos)
        End If

    End Sub
End Class