Imports System.IO
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen

Public Class frmMovimientosKardex

    Private pListMovs As New List(Of clsBeVW_Movimientos)
    Private DT As New DataTable("MovsCardex")
    Public pBeListaProductos As New List(Of Integer)
    Public Property ProductoEspecifico As New clsBeProducto

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub SetDatataTable()

        DT.Columns.Add("Propietario", GetType(String))
        DT.Columns.Add("Póliza", GetType(String))
        DT.Columns.Add("Producto", GetType(String))
        DT.Columns.Add("Código", GetType(String))
        DT.Columns.Add("Código Barra", GetType(String))
        DT.Columns.Add("Cantidad U.M.Bas", GetType(Double))
        DT.Columns.Add("UMBas", GetType(String))
        DT.Columns.Add("Cantidad Presentación", GetType(Double))
        DT.Columns.Add("Presentación", GetType(String))
        DT.Columns.Add("Peso", GetType(Double))
        DT.Columns.Add("Lote", GetType(String))
        DT.Columns.Add("FechaVence", GetType(Date))
        DT.Columns.Add("Licencia", GetType(String))
        DT.Columns.Add("FechaMovimiento", GetType(Date))
        DT.Columns.Add("Estado Origen", GetType(String))
        DT.Columns.Add("Estado Destino", GetType(String))
        DT.Columns.Add("Ubicación Origen", GetType(String))
        DT.Columns.Add("Ubicación Destino", GetType(String))
        DT.Columns.Add("Tipo Tarea", GetType(String))
        DT.Columns.Add("IdBodegaOrigen", GetType(Integer))
        DT.Columns.Add("IdProducto", GetType(Integer))
        DT.Columns.Add("Clasificacion", GetType(String))
        DT.Columns.Add("Area_Origen", GetType(String))
        DT.Columns.Add("Operador", GetType(String))
        DT.Columns.Add("Talla", GetType(String))
        DT.Columns.Add("Color", GetType(String))

    End Sub

    Public Function Get_Lista_Movimientos() As Boolean

        Dim clsTransaccion As New clsTransaccion

        Get_Lista_Movimientos = False

        Try

            clsTransaccion.Begin_Transaction()

            If txtIdProducto.Text.Trim <> "" AndAlso Not ProductoEspecifico Is Nothing Then

                Dim IdProductoBodega As Integer = clsLnProducto_bodega.Get_IdProductoBodega_By_IdProducto_And_IdBodega(ProductoEspecifico.IdProducto,
                                                                                                                       cmbBodega.EditValue)

                pListMovs = clsLnTrans_movimientos.Get_All_Movimientos_By_IdProducto(dtpFechaDel.Value,
                                                                                     dtpFechaAl.Value,
                                                                                     IdProductoBodega,
                                                                                     cmbBodega.EditValue,
                                                                                     cmbPropietario.EditValue,
                                                                                     txtLote.Text.Trim())
            Else

                If ProductoEspecifico Is Nothing OrElse ProductoEspecifico.IdProducto = 0 Then
                    '#CKFK 20181003 Agregue aqui la funcion que devuelve el IDPropietario porque estaba enviando el IdPropietarioBodega
                    Dim vIdPropietario As Integer = IMS.Get_IdPropietario_By_IdBodega(cmbBodega.EditValue, cmbPropietario.EditValue)
                    pBeListaProductos = clsLnProducto.Get_All_IdProducto_By_IdPropietario_And_Bodega(vIdPropietario,
                                                                                                     cmbBodega.EditValue)
                End If

                Dim vListaMovimientos As New List(Of clsBeVW_Movimientos)

                For Each Prod In pBeListaProductos

                    vListaMovimientos = clsLnTrans_movimientos.Get_All_Movimientos_By_IdProducto(dtpFechaDel.Value,
                                                                                                 dtpFechaAl.Value,
                                                                                                 Prod, cmbBodega.EditValue,
                                                                                                 cmbPropietario.EditValue,
                                                                                                 clsTransaccion.lConnection, clsTransaccion.lTransaction)

                    pListMovs.AddRange(vListaMovimientos)

                    Application.DoEvents()

                Next

            End If

            clsTransaccion.Commit_Transaction()

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        Finally
            clsTransaccion.Close_Conection()
        End Try

    End Function

    Public Property Skip_Saving_Layout As Boolean = False
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Private Sub Cargar()

        Skip_Saving_Layout = True

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Cargarndo...")

            Dim IdPropietarioBodega As Integer = 0
            Dim cant As Double

            pListMovs = clsLnTrans_movimientos.Get_Movimientos_Kardex(cmbBodega.EditValue,
                                                                      dtpFechaDel.Value,
                                                                      dtpFechaAl.Value,
                                                                      ProductoEspecifico.IdProductoBodega,
                                                                      cmbPropietario.EditValue)

            If pListMovs.Count > 0 Then

                grdKardex.DataSource = Nothing

                DT.Clear()

                For Each BeVW_Movimientos As clsBeVW_Movimientos In pListMovs

                    If BeVW_Movimientos.IdTipoTarea = clsDataContractDI.tTipoTarea.DESP OrElse
                        BeVW_Movimientos.IdTipoTarea = clsDataContractDI.tTipoTarea.AJCANTN OrElse
                        BeVW_Movimientos.IdTipoTarea = clsDataContractDI.tTipoTarea.AJCANTNI OrElse
                        BeVW_Movimientos.TipoTarea = clsDataContractDI.tTipoTarea.TRAS OrElse
                        BeVW_Movimientos.TipoTarea = clsDataContractDI.tTipoTarea.AJLOTENI OrElse
                        BeVW_Movimientos.TipoTarea = clsDataContractDI.tTipoTarea.AJVENCENI Then
                        cant = (BeVW_Movimientos.Cantidad * -1)
                    Else
                        cant = BeVW_Movimientos.Cantidad
                    End If

                    If BeVW_Movimientos.IdPresentacion <> 0 Then
                        BeVW_Movimientos.Cantidad_Presentacion = (cant / BeVW_Movimientos.Factor)
                    Else
                        BeVW_Movimientos.Cantidad_Presentacion = 0
                    End If

                    DT.Rows.Add(BeVW_Movimientos.Propietario,
                                BeVW_Movimientos.Poliza,
                                BeVW_Movimientos.Producto,
                                BeVW_Movimientos.Codigo,
                                BeVW_Movimientos.CodigoBarra,
                                cant,
                                BeVW_Movimientos.UMBas,
                                BeVW_Movimientos.Cantidad_Presentacion,
                                BeVW_Movimientos.Presentacion,
                                BeVW_Movimientos.Peso,
                                BeVW_Movimientos.Lote,
                                BeVW_Movimientos.Fecha_Vence,
                                BeVW_Movimientos.Lic_Plate,
                                BeVW_Movimientos.Fecha,
                                BeVW_Movimientos.EstadoOrigen,
                                BeVW_Movimientos.EstadoDestino,
                                BeVW_Movimientos.UbicOrigen,
                                BeVW_Movimientos.UbicDestino,
                                BeVW_Movimientos.TipoTarea,
                                BeVW_Movimientos.IdBodegaOrigen,
                                BeVW_Movimientos.IdProducto,
                                BeVW_Movimientos.Clasificacion,
                                BeVW_Movimientos.Area_Origen,
                                BeVW_Movimientos.Operador,
                                BeVW_Movimientos.Talla,
                                BeVW_Movimientos.Color)

                Next

            End If

            grdKardex.DataSource = DT

            If GridView1.Columns.Count > 0 Then

                GridView1.OptionsView.ShowFooter = True

                GridView1.Columns("Código").Group()

                Dim item As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "Cantidad U.M.Bas",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "DISP: {0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("Cantidad U.M.Bas")}
                GridView1.GroupSummary.Add(item)

                Dim item1 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "Peso",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("Peso")}
                GridView1.GroupSummary.Add(item1)

                Dim item2 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "Propietario",
                .SummaryType = DevExpress.Data.SummaryItemType.Count,
                .DisplayFormat = "Movimientos: {0:n}",
                .ShowInGroupColumnFooter = GridView1.Columns("Propietario")}
                GridView1.GroupSummary.Add(item2)

                GridView1.Columns("Cantidad U.M.Bas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Cantidad U.M.Bas").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("Cantidad U.M.Bas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Cantidad U.M.Bas").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Cantidad Presentación").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Cantidad Presentación").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("Cantidad Presentación").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Cantidad Presentación").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Peso").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Peso").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("Peso").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Peso").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("FechaMovimiento").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                GridView1.Columns("FechaMovimiento").DisplayFormat.FormatString = "G"

                Dim item3 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "Cantidad Presentación",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "DISP: {0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("Cantidad Presentación")}
                GridView1.GroupSummary.Add(item3)

                GridView1.Columns("IdBodegaOrigen").Visible = False
                GridView1.Columns("IdProducto").Visible = False

                GridView1.ExpandAllGroups()

                lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

                Restore_LayOut()

                GridView1.BestFitColumns(True)

            End If

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception

            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            Skip_Saving_Layout = False
        End Try

    End Sub

    Private Sub cmbBodega_EditValueChanging(sender As Object, e As ChangingEventArgs)
        Cargar()
        GridView1.Focus()
    End Sub

    Private Sub dtpFechaAl_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaAl.ValueChanged

        Try

            Cargar()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub dtpFechaDel_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaDel.ValueChanged

        Try

            Cargar()

            GridView1.Focus()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub frmMovimientosCardex_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            '#EJC20210719:Restaurar LayoutGrid en grdExistDoc.
            vNombreArchivoLayOutGrid = "frmMovimientosCardex.xml"

            AP.Listar_Bodegas_By_Usuario(cmbBodega)

            cmbBodega.EditValue = Integer.Parse(AP.IdBodega)

            IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, cmbBodega.EditValue)

            SetDatataTable()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Restore_LayOut()

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

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Cargar()
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
            printLink.Component = grdKardex
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

        Dim reportHeader As String = vbNewLine & "Listado de Movimientos Cardex"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick
        Imprimir_Vista()
    End Sub

    Private Sub cmdSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdSalir.ItemClick
        Close()
    End Sub

    Private Sub GridView1_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles GridView1.RowCellStyle

        If e.Column.FieldName = "Cantidad U.M.Bas" Then

            Dim View As GridView = sender
            Dim TipoT As Object = View.GetRowCellDisplayText(e.RowHandle, View.Columns("Tipo Tarea"))

            If TipoT = "DESP" OrElse TipoT = "TRAS" Then
                e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Regular)
                e.Appearance.BackColor = Color.Salmon
                e.Appearance.BackColor2 = Color.SeaShell
            ElseIf TipoT = "AJCANTN" OrElse TipoT = "AJCANTNI" OrElse
                TipoT = "AJLOTNI" OrElse TipoT = "AJVENCENI" Then
                e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Regular)
                e.Appearance.BackColor = Color.Salmon
                e.Appearance.BackColor2 = Color.SeaShell
            ElseIf TipoT = "AJCANTP" Then
                e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Regular)
                e.Appearance.BackColor = Color.Green
                e.Appearance.BackColor2 = Color.White
            ElseIf TipoT = "RECE" OrElse TipoT = "INVE" OrElse
                TipoT = "AJLOTPI" OrElse TipoT = "AJVENCEPI" OrElse
                TipoT = "AJCANTPI" OrElse TipoT = "CESTI" OrElse TipoT = "CUBII" Then
                e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Regular)
                e.Appearance.BackColor = Color.Green
                e.Appearance.BackColor2 = Color.White
            End If

        End If

    End Sub

    Private Sub cmbBodega_EditValueChanged(sender As Object, e As EventArgs) Handles cmbBodega.EditValueChanged
        IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, cmbBodega.EditValue)
    End Sub

    Private Sub txtIdProducto_Validated(sender As Object, e As EventArgs) Handles txtIdProducto.Validated

        Try

            If String.IsNullOrEmpty(txtIdProducto.Text.Trim()) = False AndAlso txtIdProducto.Text > "0" Then

                ProductoEspecifico = clsLnProducto.Get_Single_By_Codigo(txtIdProducto.Text)

                If ProductoEspecifico IsNot Nothing AndAlso ProductoEspecifico.IdProducto > 0 Then
                    txtNombreProducto.Text = Trim(String.Format("{0}", ProductoEspecifico.Nombre))
                    ProductoEspecifico.IdProductoBodega = clsLnProducto_bodega.Get_IdProductoBodega_By_IdProducto_And_IdBodega(ProductoEspecifico.IdProducto, AP.IdBodega)
                    Cargar()
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

    Private Sub lblProducto_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblProducto.LinkClicked

        Try

            Dim Rec As New frmProductoList() With {
                   .Modo = frmProductoList.pModo.Seleccion,
                   .StartPosition = FormStartPosition.CenterParent,
                   .WindowState = FormWindowState.Maximized}
            Rec.ShowDialog()

            grdKardex.DataSource = Nothing

            If Rec.pObjProducto IsNot Nothing AndAlso Rec.pObjProducto.IdProducto <> 0 Then

                txtIdProducto.Text = Rec.pObjProducto.Codigo
                txtNombreProducto.Text = Rec.pObjProducto.Nombre
                'GT140720211637: Se obtiene el producto y la bodega para recargar la lista 
                ProductoEspecifico.IdProductoBodega = clsLnProducto_bodega.Get_IdProductoBodega_By_IdProducto_And_IdBodega(Rec.pObjProducto.IdProducto, AP.IdBodega)
            End If

            Cargar()

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

        If txtIdProducto.Text.Trim = "" Then
            ProductoEspecifico.IdProductoBodega = 0
            Cargar()
        End If

    End Sub

    '#EJC20210716:Guardar LayoutGrid en LotesPorUbi en frmStockConDocumento.
    Private vNombreArchivoLayOutGrid As String = ""

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

            XtraMessageBox.Show("Diseño de grid guardado!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

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

    Private Sub cmdExToExcel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdExportarExcel.ItemClick
        Exportar_Grid_A_Excel(grdKardex, "WMS_MovimientosKardex.xlsx")
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
                XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub gviewEncabezadoPedido_ColumnFilterChanged(sender As Object, e As EventArgs) Handles GridView1.ColumnFilterChanged
        lblRegs.Caption = String.Format("Registros: {0}", String.Format("{0:#,##0.00}", GridView1.DataRowCount.ToString()))
    End Sub

End Class