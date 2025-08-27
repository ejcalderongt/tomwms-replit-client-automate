Imports System.IO
Imports System.Reflection
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid

Public Class frmExistenciaFiscal

    Public Property IsLoading As Boolean = True

    Public Sub New()
        InitializeComponent()
        IsLoading = False
    End Sub

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    '#EJC20210716_1159AM:Guardar LayoutGrid en frmExistenciaFiscal.vb
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

    Private Sub GridView1_Layout(sender As Object, e As EventArgs) Handles GridView1.Layout

        If IsLoading Then Exit Sub

        Guardar_Layout()

    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuActualizar.ItemClick

        mnuActualizar.Enabled = False
        Cargar_Datos()
        mnuActualizar.Enabled = True

    End Sub

    Private Sub Cargar_Datos()

        IsLoading = True
        Dim IdPropietario As Integer

        Try

            Dim DT As New DataTable("Dt")

            dgridStockFiscal.DataSource = Nothing

            If cmbPropietario.EditValue <> 0 Then
                Dim fila As Object = cmbPropietario.GetSelectedDataRow
                IdPropietario = fila.Item("IdPropietarioBodega")
            Else
                IdPropietario = 0
            End If

            DT = clsLnReportesFiscales.Get_Existencia_Fiscal_DataTable(IdPropietario)

            If Not DT Is Nothing Then

                If DT.Rows.Count > 0 Then
                    dgridStockFiscal.DataSource = DT
                End If

                If GridView1.RowCount > 0 Then

                    Restore_LayOut()

                    lblReg.Caption = String.Format("Registros: {0}", GridView1.RowCount)

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

    Private Sub frmExistenciaFiscal_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try

            mnuGuardarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            vNombreArchivoLayOutGrid = "frm_Existencia_Fiscal.xml"

            IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, AP.IdBodega)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub mnuImprimir_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuImprimir.ItemClick

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
            printLink.Component = dgridStockFiscal
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
                              vbNewLine & "Existencia Fiscal " &
                              vbNewLine & "BODEGA: " & AP.NomBodega

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub mnuExportarExc_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuExportarExc.ItemClick
        Exportar_Grid_A_Excel(dgridStockFiscal, "WMS_ExistenciaFiscal.xlsx")
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

    Private Sub mnuSalir_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuSalir.ItemClick
        Close()
    End Sub


    Private Sub cmbPropietario_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbPropietario.KeyDown

        Try

            If e.KeyCode = Keys.Back Then
                cmbPropietario.EditValue = 0
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

        End Try

    End Sub

End Class