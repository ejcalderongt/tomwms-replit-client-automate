Imports System.IO
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmListStockReservado

    Public ListRegistros As New List(Of clsBeVW_Stock_Res_Pedido)
    Public Property SeleccionMultiple As Boolean = False
    Public listaStockSeleccionado As New List(Of clsBeVW_stock_res)
    Public pObjStock As clsBeVW_stock_res
    Private DT As New DataTable("StockReservado")
    Private IsLoading As Boolean = False

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Cargar_Datos()

        Try

            IsLoading = True

            DT.Clear() : grdStockRes.DataSource = Nothing

            DT = clsLnVW_Stock_Res_Pedido.Get_All_By_IdBodega_DT(AP.IdBodega)

            Dim vTotalReservado As Double = 0
            Dim vTotalDisponible As Double = 0

            If DT.Rows.Count > 0 Then

                grdStockRes.DataSource = DT

                If GridView1.RowCount > 0 Then

                    GridView1.OptionsView.ShowFooter = True
                    GridView1.OptionsView.ColumnAutoWidth = False

                    lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

                    GridView1.Columns("CantidadFisica").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("CantidadFisica").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView1.Columns("CantidadFisica").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("CantidadFisica").DisplayFormat.FormatString = "{0:n6}"


                    GridView1.Columns("Factor").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Factor").DisplayFormat.FormatString = "{0:n6}"
                    'GridView1.Columns("Cantidad").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    'GridView1.Columns("Cantidad").SummaryItem.DisplayFormat = "{0:n6}"

                    'GridView1.Columns("Cantidad").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    'GridView1.Columns("Cantidad").DisplayFormat.FormatString = "{0:n6}"

                    GridView1.Columns("cantidad_presentacion_reservada").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("cantidad_presentacion_reservada").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView1.Columns("cantidad_presentacion_reservada").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("cantidad_presentacion_reservada").DisplayFormat.FormatString = "{0:n6}"

                    GridView1.Columns("cantidad_umbas_reservada").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("cantidad_umbas_reservada").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView1.Columns("cantidad_umbas_reservada").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("cantidad_umbas_reservada").DisplayFormat.FormatString = "{0:n6}"

                    GridView1.Columns("Peso").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Peso").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView1.Columns("Peso").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Peso").DisplayFormat.FormatString = "{0:n6}"

                    GridView1.Columns("fec_agr").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                    GridView1.Columns("fec_agr").DisplayFormat.FormatString = "g"

                    GridView1.BestFitColumns(True)

                    vTotalReservado = DT.AsEnumerable.Sum(Function(x) x.Item("cantidad_umbas_reservada"))
                    vTotalDisponible = DT.AsEnumerable.Sum(Function(x) x.Item("CantidadFisica"))

                End If

            End If

            Set_Indicador_Reserva_Bodega(vTotalDisponible, vTotalReservado)

            Restore_LayOut_Grid()

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

    Private Sub Set_Indicador_Reserva_Bodega(ByVal TotalDisponible As Double, ByVal TotalReservado As Double)

        Try

            Dim vCantUbicacionesVacias As Integer = 0
            Dim vCantUbicacionesOcupadas As Integer = 0
            Dim vTotalUbicaciones As Integer = 0

            If TotalDisponible = 0 AndAlso TotalReservado = 0 Then

                SplitContainer1.Panel2Collapsed = True

            Else

                SplitContainer1.Panel2Collapsed = False

                vTotalUbicaciones = (TotalDisponible)

                lblStockDisponible.Text = "Disponible: " & TotalDisponible.ToString("N2")
                lblReservado.Text = "Reservado: " & TotalReservado.ToString("N2")

                LinearScaleComponent1.BeginInit()
                LinearScaleComponent1.MinValue = 0
                LinearScaleComponent1.MaxValue = 100
                LinearScaleComponent1.Value = (TotalReservado / TotalDisponible) * 100
                LinearScaleComponent1.EndInit()


            End If

            Application.DoEvents()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub frmListStockReservado_Load(sender As Object, e As EventArgs) Handles Me.Load

        vNombreArchivoLayOutGrid = "frmListStockReservado.xml"

        Cargar_Datos()
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

    Private Sub grdStockRes_DoubleClick(sender As Object, e As EventArgs) Handles grdStockRes.DoubleClick

        Try

            If (GridView1.RowCount > 0) Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow

                If Not Dr Is Nothing Then

                    Dim Obj As New clsBeVW_Stock_Res_Pedido
                    Obj.IdStockRes = Dr.Item("IdStockRes")
                    clsLnVW_Stock_Res_Pedido.GetSingle(Obj)

                    Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle

                    If Modo = pModo.Lista Then

                        With frmStockReservado
                            .Modo = frmStockReservado.TipoTrans.Editar
                            .pBeStockRes = Obj
                            .ShowDialog()
                            .Focus()
                        End With

                        GridView1.FocusedRowHandle = lSelectionIndex

                        Cargar_Datos()

                    ElseIf Modo = pModo.Seleccion Then
                        Hide()
                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Cargar_Datos()
    End Sub

    Private Sub cmdSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdSalir.ItemClick
        Close()
    End Sub

    Private Sub mnuExportarExcel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuExportarExcel.ItemClick
        Exportar_Grid_A_Excel(grdStockRes, "WMS_Stock_Reservado.xlsx")
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

    Private Sub mnuImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuImprimir.ItemClick
        Imprimir_Vista()
    End Sub

    Private Sub Imprimir_Vista()

        Try

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
            printLink.Component = grdStockRes
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

        Dim reportHeader As String = vbNewLine & "Detalle de stock reservado"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub chkSeleccionMultiple_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkSeleccionMultiple.CheckedChanged


        Try

            If chkSeleccionMultiple.Checked Then

                If XtraMessageBox.Show("Si habilita la selección múltiple se tomarán las cantidades completas de cada línea. ¿Continuar?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    GridView1.OptionsSelection.MultiSelect = True
                    GridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect
                    mnuTomarSeleccionados.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    mnuTomarSeleccionados.Enabled = True

                Else
                    chkSeleccionMultiple.Checked = False
                End If

            Else
                SeleccionMultiple = False
                chkSeleccionMultiple.Checked = False
                GridView1.OptionsSelection.MultiSelect = False
                GridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect
                mnuTomarSeleccionados.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                mnuTomarSeleccionados.Enabled = False
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

    Private Sub mnuTomarSeleccionados_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuTomarSeleccionados.ItemClick

        Dim lBeStockRes As New List(Of clsBeStock_res)

        Try

            Dim selectedRowHandles As Integer() = GridView1.GetSelectedRows()

            If selectedRowHandles.Length = 0 Then
                XtraMessageBox.Show("Seleccione al menos un registro",
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
            Else

                Dim IdStockRes As Integer = 0
                Dim IdStock As Integer = 0
                Dim DisponibleUMBAs As Double = 0
                Dim Aplica As String = False
                '#GT21062022_1623: limpiados objetos para nueva selección multiple
                listaStockSeleccionado = New List(Of clsBeVW_stock_res)

                If (selectedRowHandles.Length > 0) Then

                    If XtraMessageBox.Show(String.Format("¿Está seguro de eliminar el stock reservado (" & selectedRowHandles.Length & " registros) aunque su estado sea pickeado o verificado?"), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                        For i As Integer = 0 To selectedRowHandles.Length - 1

                            IdStockRes = GridView1.GetRowCellValue(selectedRowHandles(i), "IdStockRes")
                            IdStock = GridView1.GetRowCellValue(selectedRowHandles(i), "IdStock")

                            DisponibleUMBAs = GridView1.GetRowCellValue(selectedRowHandles(i), "Disponible_UMBas")
                            Aplica = GridView1.GetRowCellValue(selectedRowHandles(i), "Aplica")

                            Dim BeStockRes As New clsBeStock_res
                            BeStockRes.IdStockRes = IdStockRes
                            BeStockRes.IdStock = IdStock

                            '#EJC20210830: Validación de Picking asociado al stock reservado antes de liberarlo y también liberarlo de los procesos de picking.
                            If clsLnStock_res.GetSingle(BeStockRes) Then
                                lBeStockRes.Add(BeStockRes)
                            Else
                                XtraMessageBox.Show("No se obtuvo el registro de reserva, no se puede liberar el stock", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            End If

                        Next

                        If lBeStockRes.Count > 0 Then
                            If clsLnStock_res.Liberar_Stock_By_List(lBeStockRes,
                                                                    AP.UsuarioAp.IdUsuario) Then
                                SeleccionMultiple = True
                                DialogResult = DialogResult.OK
                                Cargar_Datos()
                            End If
                        Else
                            XtraMessageBox.Show("Error_20221006_1643: La lista de stock reservado está vacía",
                                                Text,
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Exclamation)
                        End If

                    End If

                Else
                    XtraMessageBox.Show("No se han seleccionado registros.",
                                        Text,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation)
                End If

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

    Private vNombreArchivoLayOutGrid As String = ""

    Private Sub GridView1_Layout(sender As Object, e As EventArgs) Handles GridView1.Layout
        Guardar_Layout()
    End Sub

    Private Sub Guardar_Layout()

        If IsLoading Then Exit Sub

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

            mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
        Text,
        MessageBoxButtons.OK,
        MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try


    End Sub

    Private ExisteLayOut As Boolean = False
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

    Private Sub Restore_LayOut_Grid()

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
End Class