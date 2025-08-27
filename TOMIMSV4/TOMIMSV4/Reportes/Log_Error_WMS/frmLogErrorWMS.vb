Imports System.IO
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen

Public Class frmLogErrorWMS

    Private gDT As New DataTable("StockEnUnaFecha")
    Public pBeListaProductos As New List(Of Integer)
    Public lLogErrorWMS As New DataTable
    Public RepMovEnUnaFecha As New List(Of clsBeStockEnUnaFecha)
    Public Property Modo As pModo
    Public Property ProductoEspecifico As New clsBeProducto
    Public Property ModoDepuracion As Boolean = False
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    '#EJC20210716:Guardar LayoutGrid en LotesPorUbi.
    Private vNombreArchivoLayOutGrid As String = "frmlogerrorwms.xml"

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Public Function Get_Lista_Movimientos() As Boolean

        Get_Lista_Movimientos = False
        Dim cTrans As New clsTransaccion()

        Try

            If txtIdProducto.Text.Trim <> "" AndAlso Not ProductoEspecifico Is Nothing Then

                Dim IdProductoBodega As Integer = clsLnProducto_bodega.Get_IdProductoBodega_By_IdProducto_And_IdBodega(ProductoEspecifico.IdProducto, cmbBodega.EditValue)

                lLogErrorWMS = clsLnLog_error_wms.Get_All_Log_By_Filtros(dtpFechaDesde.Value,
                                                                            dtpfechaHasta.Value,
                                                                            IdProductoBodega,
                                                                            cmbBodega.EditValue)
            Else

                If ProductoEspecifico Is Nothing OrElse ProductoEspecifico.IdProducto = 0 Then
                    lblPrg.Text = "Obteniendo listado de productos"
                    lblPrg.Refresh()
                    lLogErrorWMS = clsLnLog_error_wms.Get_All_Log_By_Rango_Fechas(dtpFechaDesde.Value,
                                                                                  dtpfechaHasta.Value)
                End If

            End If

            If Not lLogErrorWMS Is Nothing Then
                Get_Lista_Movimientos = lLogErrorWMS.Rows.Count > 0
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Sub Generar_Reporte()

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Generando...")

            Dim BeStockEnFecha As New clsBeStockEnUnaFecha
            Dim Idx As Integer = -1
            Dim Idx1 As Integer = -1.0F

            lLogErrorWMS = New DataTable()

            RepMovEnUnaFecha.Clear()
            lblPrg.Visible = True

            If Get_Lista_Movimientos() Then

                prg.Visible = True
                prg.Properties.Step = 1
                prg.Properties.PercentView = True
                prg.Properties.Maximum = lLogErrorWMS.Rows.Count
                prg.Properties.Minimum = 0

                Llena_Grid()

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            SplashScreenManager.CloseForm(False)
            prg.Visible = False
        End Try

    End Sub

    Private Sub Llena_Grid()

        Dim pBeStock As New clsBeStock
        Dim pBeStockSinEstado As New clsBeStock
        Dim BeDiferencia As New clsBeDiferencias_movimientos
        Dim vIdDiferencia As Integer = 0
        Dim vExistenciaSinEstado As Double = 0
        Dim lMovimientos As New List(Of clsBeTrans_movimientos)

        Try

            dgrid.DataSource = lLogErrorWMS

            Restore_LayOut_Grid()

            GridView1.OptionsView.ShowFooter = True

            Dim item0 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "Cantidad",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "Cantidad: {0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("Cantidad")}
            GridView1.GroupSummary.Add(item0)

            GridView1.Columns("Fecha").DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss"

            GridView1.ExpandAllGroups()
            GridView1.BestFitColumns(True)

            lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            prg.Visible = False
            lblPrg.Visible = False
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

    Private Sub frmStockEnUnaFecha_Load(sender As Object, e As EventArgs) Handles Me.Load

        '#EJC20210716:Restaurar LayoutGrid en LotesPorUbi.
        vNombreArchivoLayOutGrid = "frmLogError.xml"

        AP.Listar_Bodegas_By_Usuario(cmbBodega)

        cmbBodega.EditValue = Integer.Parse(AP.IdBodega)

        IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, cmbBodega.EditValue)

        Try

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub BarButtonItem3_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem3.ItemClick
        Close()
    End Sub

    Private Sub lblProducto_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblProducto.LinkClicked

        Try

            Dim Rec As New frmProductoList() With {
                   .Modo = frmProductoList.pModo.Seleccion,
                   .StartPosition = FormStartPosition.CenterParent,
                   .WindowState = FormWindowState.Maximized}
            Rec.ShowDialog()

            dgrid.DataSource = Nothing

            If Rec.pObjProducto IsNot Nothing AndAlso Rec.pObjProducto.IdProducto <> 0 Then

                txtIdProducto.Text = Rec.pObjProducto.Codigo
                txtNombreProducto.Text = Rec.pObjProducto.Nombre
            End If

            Generar_Reporte()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub txtIdProducto_Validated(sender As Object, e As EventArgs) Handles txtIdProducto.Validated

        Try

            If String.IsNullOrEmpty(txtIdProducto.Text.Trim()) = False AndAlso txtIdProducto.Text > "0" Then

                ProductoEspecifico = clsLnProducto.Get_Single_By_Codigo(txtIdProducto.Text)

                If ProductoEspecifico IsNot Nothing AndAlso ProductoEspecifico.IdProducto > 0 Then
                    txtNombreProducto.Text = Trim(String.Format("{0}", ProductoEspecifico.Nombre))
                Else
                    XtraMessageBox.Show(String.Format("No existe producto con código {0}", txtIdProducto.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtIdProducto.Focus()
                    txtIdProducto.SelectAll()
                    ProductoEspecifico = Nothing
                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Generar_Reporte()
    End Sub

    Private Sub frmStockEnUnaFecha_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            '#EJC20210716:Restaurar LayoutGrid en grdStockPorLote - frmstockPorLote_posicion.
            'vNombreArchivoLayOutGrid = "grdStockEnUnaFecha.xml"

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub txtIdProducto_TextChanged(sender As Object, e As EventArgs) Handles txtIdProducto.TextChanged
        txtNombreProducto.Text = ""
        ProductoEspecifico = Nothing
    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick

        Try

            Imprimir_Vista()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

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
            printLink.Component = dgrid
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

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As System.Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "TOM, WMS" &
                              vbNewLine & "Stock en una fecha " &
                              vbNewLine & "BODEGA: " & AP.NomBodega

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick

        Try

            'dgrid.ExportToXlsx()

            Dim myStream As Stream
            Dim saveFileDialog1 As New SaveFileDialog()

            saveFileDialog1.Filter = "xlsx files (*.xlsx)|*.xlsx"
            saveFileDialog1.FilterIndex = 1
            saveFileDialog1.RestoreDirectory = True
            saveFileDialog1.FileName = "WMS_Stock_En_Una_Fecha_Del_" & FormatoFechas.tFecha(dtpFechaDesde.Value) & "_Al_" & FormatoFechas.tFecha(dtpfechaHasta.Value) & ".xlsx"

            If saveFileDialog1.ShowDialog() = DialogResult.OK Then
                myStream = saveFileDialog1.OpenFile()
                If (myStream IsNot Nothing) Then
                    ' Code to write the stream goes here.
                    dgrid.ExportToXlsx(myStream)
                    myStream.Close()
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

    Private Sub cmbBodega_EditValueChanged(sender As Object, e As EventArgs) Handles cmbBodega.EditValueChanged

        IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, cmbBodega.EditValue)

    End Sub

    Private Sub dgrid_DoubleClick(sender As Object, e As EventArgs) Handles dgrid.DoubleClick

        Try

            If (GridView1.RowCount > 0) Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow
                Dim CodigoProducto As String
                Dim Lote As String
                Dim Diferencia As Double = 0

                CodigoProducto = Dr.Item("Código")
                Lote = Dr.Item("Lote")
                Diferencia = Dr.Item("Diferencia")

                If XtraMessageBox.Show(String.Format("¿Ir a detalle de existencias?" & vbNewLine & "Código: " & CodigoProducto & vbNewLine & "Lote: " & Lote & vbNewLine & "Diferencia: " & Diferencia),
                                       Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle

                    With frmListStockSeek
                        .Modo = frmStockSeek.TipoTrans.Editar
                        .rpCodigoProducto = CodigoProducto
                        .rpLote = Lote
                        .Diferencia = Diferencia
                        .IdBodega = cmbBodega.EditValue
                        .MdiParent = MdiParent
                        .WindowState = FormWindowState.Normal
                        .Show()
                        .Focus()
                    End With

                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub frmStockEnUnaFecha_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

        If e.Control AndAlso e.KeyCode = Keys.D Then
            ModoDepuracion = True
            MsgBox("Modo depuración activado, tenga cuidado...", MsgBoxStyle.Information, Text)
        End If

    End Sub

    Private Sub Guardar_Layout()

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

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Restore_LayOut_Grid()

        Try

            Dim BeConfiguracionUsuarioDet As New clsBeConfiguracion_usuario_det

            BeConfiguracionUsuarioDet = clsLnConfiguracion_usuario_enc.Get_Layout(AP.IdEmpresa,
                                                                                  AP.UsuarioAp.IdUsuario,
                                                                                  AP.HostName,
                                                                                  vNombreArchivoLayOutGrid)


            If Not BeConfiguracionUsuarioDet Is Nothing Then
                GridView1.RestoreLayoutFromStream(BeConfiguracionUsuarioDet.Stream_Template)
                mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            Else
                mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            End If

        Catch ex As Exception

        End Try

    End Sub

    'Private Sub GridView1_Layout(sender As Object, e As EventArgs) Handles GridView1.Layout
    '    Guardar_Layout()
    'End Sub

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

End Class