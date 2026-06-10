Imports System.IO
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid

Public Class frmResumenExistenciasUMBas

    Public listaStock As New List(Of clsBeVW_stock_res)
    Public Property ProductoEspecifico As New clsBeProducto
    Public Property IsLoading As Boolean = True
    Public Property vNombreArchivoLayOutGrid As String = ""
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub mnuSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSalir.ItemClick
        Close()
    End Sub

    Private Sub Imprimir_Vista()

        Try
            clsUiPrintHelper.PrintGridPreview(grdResumenUMB, AP.UsuarioAp.Nombres, AddressOf PrintableComponentLink_CreateReportHeaderArea, True, True, 12)
            Exit Sub
            GridView1.OptionsPrint.ExpandAllDetails = True
            GridView1.OptionsPrint.PrintDetails = True

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
            printLink.Component = grdResumenUMB
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

        Dim reportHeader As String = vbNewLine & "Detalle de Existencias"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub Cargar_Datos()

        IsLoading = True

        Try

            Dim DT As New DataTable("Dt")

            grdResumenUMB.DataSource = Nothing

            DT = clsLnStock.Get_Reporte_Stock_DataTable(cmbBodega.EditValue, cmbPropietario.EditValue)

            If Not DT Is Nothing Then

                If ProductoEspecifico IsNot Nothing AndAlso ProductoEspecifico.IdProducto > 0 Then

                    Dim query =
                        From c In DT.AsEnumerable()
                        Where c.Field(Of String)("codigo") = (ProductoEspecifico.Codigo)

                    If query.Count > 0 Then
                        DT = query.CopyToDataTable
                    Else
                        DT.Clear()
                    End If

                End If

            End If

            If Not DT Is Nothing Then

                If DT.Rows.Count > 0 Then
                    grdResumenUMB.DataSource = DT
                End If

                If GridView1.RowCount > 0 Then

                    lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

                    GridView1.OptionsView.ColumnAutoWidth = False

                    GridView1.BestFitColumns(True)

                    GridView1.OptionsView.ShowFooter = True

                    GridView1.Columns("CantidadUMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("CantidadUMBas").DisplayFormat.FormatString = "{0:n6}"
                    GridView1.Columns("CantidadUMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("CantidadUMBas").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView1.Columns("CantidadPresentacion").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("CantidadPresentacion").DisplayFormat.FormatString = "{0:n6}"
                    GridView1.Columns("CantidadPresentacion").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("CantidadPresentacion").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView1.Columns("Cantidad_Reservada_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Cantidad_Reservada_UMBas").DisplayFormat.FormatString = "{0:n6}"
                    GridView1.Columns("Cantidad_Reservada_UMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Cantidad_Reservada_UMBas").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView1.Columns("Cantidad_Reservada_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Cantidad_Reservada_Pres").DisplayFormat.FormatString = "{0:n6}"
                    GridView1.Columns("Cantidad_Reservada_Pres").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Cantidad_Reservada_Pres").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView1.Columns("Disponible_UMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Disponible_UMBas").SummaryItem.DisplayFormat = "{0:n6}"
                    GridView1.Columns("Disponible_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Disponible_UMBas").DisplayFormat.FormatString = "{0:n6}"

                    GridView1.Columns("Disponible_Presentación").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Disponible_Presentación").DisplayFormat.FormatString = "{0:n6}"
                    GridView1.Columns("Disponible_Presentación").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Disponible_Presentación").SummaryItem.DisplayFormat = "{0:n6}"

                End If

            End If

            Restore_LayOut()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            IsLoading = False
        End Try

    End Sub

    Private Sub frmResumenExistenciasUMBas_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            clsUiGridCopyHelper.AttachToForm(Me, "Copiar")
            vNombreArchivoLayOutGrid = grdResumenUMB.Name & ".xml"

            AP.Listar_Bodegas_By_Usuario(cmbBodega)

            IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, cmbBodega.EditValue)

            Cargar_Datos()

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        Cargar_Datos()
    End Sub

    Private Sub mnuImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuImprimir.ItemClick
        Imprimir_Vista()
    End Sub

    Private Sub cmbBodega_EditValueChanged(sender As Object, e As EventArgs) Handles cmbBodega.EditValueChanged

        Try

            IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, cmbBodega.EditValue)

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub linklblProducto_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles linklblProducto.LinkClicked

        Try

            Dim Rec As New frmProductoList() With {
                   .Modo = frmProductoList.pModo.Seleccion,
                   .StartPosition = FormStartPosition.CenterParent,
                   .WindowState = FormWindowState.Maximized}
            Rec.ShowDialog()

            grdResumenUMB.DataSource = Nothing

            If Rec.pObjProducto IsNot Nothing AndAlso Rec.pObjProducto.IdProducto <> 0 Then

                txtIdProducto.Text = Rec.pObjProducto.Codigo
                txtNombreProducto.Text = Rec.pObjProducto.Nombre
            End If

            Cargar_Datos()

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
        Cargar_Datos()
    End Sub

    Private Sub txtIdProducto_Validated(sender As Object, e As EventArgs) Handles txtIdProducto.Validated
        Try

            If String.IsNullOrEmpty(txtIdProducto.Text.Trim()) = False AndAlso txtIdProducto.Text > "0" Then

                ProductoEspecifico = clsLnProducto.Get_Single_By_Codigo(txtIdProducto.Text)

                If ProductoEspecifico IsNot Nothing AndAlso ProductoEspecifico.IdProducto > 0 Then
                    txtNombreProducto.Text = Trim(String.Format("{0}", ProductoEspecifico.Nombre))
                    Cargar_Datos()
                Else
                    XtraMessageBox.Show(String.Format("No existe producto con código {0}", txtIdProducto.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtIdProducto.Focus()
                    txtIdProducto.SelectAll()
                    ProductoEspecifico = Nothing
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
                XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        Exportar_Grid_A_Excel(grdResumenUMB, "WMS_ResumenExistenciasUMBas.xlsx")
    End Sub

    Private Sub cmbPropietario_EditValueChanged(sender As Object, e As EventArgs) Handles cmbPropietario.EditValueChanged
        Cargar_Datos()
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

            'mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Always

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
                'mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            Else
                'mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            End If

        Catch ex As Exception

        End Try

    End Sub

End Class



