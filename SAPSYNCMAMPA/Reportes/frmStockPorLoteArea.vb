Imports System.IO
Imports System.Reflection
Imports DevExpress.Data
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen

Public Class frmStockPorLoteArea

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Property IsLoading As Boolean = True
    Public Property Modo As pModo

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Dim sErrMsg As String = ""
    Dim lRetCode, lErrCode As Long

    Private Sub Cargar_Datos()

        Try


            grdStockPorLote.DataSource = Nothing

            Try

                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)

                If Not SplashScreenManager.Default.IsSplashFormVisible Then
                    SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                End If

                SplashScreenManager.Default.SetWaitFormDescription("Generando reporte...")

            Catch ex As Exception

            End Try

            Dim dtStockWMS As New DataTable
            dtStockWMS = clsLnStock.Get_Reporte_Stock_For_SAP_Sin_Lote(BeConfigEnc.Idbodega)

            Dim dtSAPB1 As New DataTable
            dtSAPB1 = Get_Existencias_From_SAP_AsDataTable(BeConfigEnc.Idbodega, BeConfigEnc.IdUsuario)

            If Not dtSAPB1 Is Nothing Then

                Dim dtReporteComparativo As New DataTable
                dtReporteComparativo = clsLnTrans_inv_teorico_erp.Get_Inventario_Vrs_Stock_Det_ERP_Sin_Lote(BeConfigEnc.Idbodega)

                IsLoading = True

                If Not dtReporteComparativo Is Nothing Then

                    If dtReporteComparativo.Rows.Count > 0 Then

                        GridView1.Columns.Clear()

                        grdStockPorLote.DataSource = dtReporteComparativo

                        lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

                        GridView1.PopulateColumns()

                        GridView1.OptionsView.ShowFooter = True

                        GridView1.Columns("StockWMS").SummaryItem.SummaryType = SummaryItemType.Sum
                        GridView1.Columns("StockWMS").SummaryItem.DisplayFormat = "{0:n6}"
                        GridView1.Columns("StockWMS").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        GridView1.Columns("StockWMS").DisplayFormat.FormatString = "{0:n6}"

                        GridView1.Columns("StockERP").SummaryItem.SummaryType = SummaryItemType.Sum
                        GridView1.Columns("StockERP").SummaryItem.DisplayFormat = "{0:n6}"
                        GridView1.Columns("StockERP").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        GridView1.Columns("StockERP").DisplayFormat.FormatString = "{0:n6}"

                        GridView1.Columns("Dif").SummaryItem.SummaryType = SummaryItemType.Sum
                        GridView1.Columns("Dif").SummaryItem.DisplayFormat = "{0:n6}"
                        GridView1.Columns("Dif").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        GridView1.Columns("Dif").DisplayFormat.FormatString = "{0:n6}"

                        'GridView1.Columns("Diferencia_Encontrada").Visible = False

                        GridView1.BestFitColumns()

                    End If

                End If

                IsLoading = False

                Set_LayOut_Grid()

            Else

                SplashScreenManager.CloseForm(False)

                XtraMessageBox.Show("No se pudo obtener la información de existencias de SAP.",
                                     Text,
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Error)

            End If


        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Cargar_Datos()
    End Sub

    Private Sub cmdSalir_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdSalir.ItemClick
        Close()
    End Sub

    Private Sub Imprimir_Vista()

        Try

            GridView1.OptionsPrint.ExpandAllDetails = True
            GridView1.OptionsPrint.PrintDetails = True

            Dim printingSystem1 As New DevExpress.XtraPrinting.PrintingSystem()
            Dim printLink As New DevExpress.XtraPrinting.PrintableComponentLink()

            AddHandler printLink.CreateReportHeaderArea, AddressOf PrintableComponentLink_CreateReportHeaderArea

            Const leftColumnFoot As String = "Páginas: [Page # of Pages #] "
            Dim leftColumnHead As String = "Usuario: [User Name] - MI3 "

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
            printLink.Component = grdStockPorLote
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Detalle de existencias por estado de producto"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdImprimir.ItemClick
        Imprimir_Vista()
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
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub cmdExToExcel_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdExToExcel.ItemClick
        Exportar_Grid_A_Excel(grdStockPorLote, "WMS_ExistenciasPorLote.xlsx")
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

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    '#EJC20210716:Guardar LayoutGrid en LotesPorUbi.
    Private vNombreArchivoLayOutGrid As String = ""

    Private Sub mnuGuardarLayoutGrid_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuGuardarLayoutGrid.ItemClick

        Try

            Dim Ms As New MemoryStream
            GridView1.SaveLayoutToStream(Ms)
            Ms.Seek(0, SeekOrigin.Begin)
            Dim MsReader As New StreamReader(Ms)
            Dim LayoutToText As String = MsReader.ReadToEnd()

            clsLnConfiguracion_usuario_enc.Guardar_Layout(BeConfigEnc.Idempresa,
                                                          IdUsuario,
                                                          HostName,
                                                          vNombreArchivoLayOutGrid,
                                                          LayoutToText)

            mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Always

            XtraMessageBox.Show("Diseño de grid guardado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)


        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub mnuEliminarLayoutGrid_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuEliminarLayoutGrid.ItemClick

        Try

            clsLnConfiguracion_usuario_enc.Delete_Layout(BeConfigEnc.Idempresa,
                                                         IdUsuario,
                                                         HostName,
                                                         vNombreArchivoLayOutGrid)

            XtraMessageBox.Show("Diseño de grid eliminado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Close()

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                              Text,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub frmStockPorLote_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Inicializando estructuras...")

            vNombreArchivoLayOutGrid = "grdStockPorLoteArea.xml"

            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface)

            If Not BeConfigEnc Is Nothing Then

                Set_LayOut_Grid()

            Else

                XtraMessageBox.Show("ERROR_202404102212: No se pudo obtener la configuración de interface para el IdConfiguracionInterface " & BD.Instancia.IdConfiguracionInterface,
                                      Text,
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Error)

                Close()

                Exit Sub

            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub Set_LayOut_Grid()

        Try

            Dim BeConfiguracionUsuarioDet As New clsBeConfiguracion_usuario_det

            BeConfiguracionUsuarioDet = clsLnConfiguracion_usuario_enc.Get_Layout(BeConfigEnc.Idempresa,
                                                                                  IdUsuario,
                                                                                  HostName,
                                                                                  vNombreArchivoLayOutGrid)

            If Not BeConfiguracionUsuarioDet Is Nothing Then
                GridView1.RestoreLayoutFromStream(BeConfiguracionUsuarioDet.Stream_Template)
                mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Always
            Else
                mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Never
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

            clsLnConfiguracion_usuario_enc.Guardar_Layout(BeConfigEnc.Idempresa,
                                                          IdUsuario,
                                                          HostName,
                                                          vNombreArchivoLayOutGrid,
                                                          LayoutToText)

            mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Always

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private dtExistenciasSAP As New DataTable

    Public Function Get_Existencias_From_SAP_AsDataTable(ByVal IdBodega As Integer, ByVal IdUsuarioAgr As Integer) As DataTable

        Get_Existencias_From_SAP_AsDataTable = Nothing
        Dim cTrans As New clsTransaccion
        Dim dtExistenciasSAP As New DataTable

        Try
            Dim codigoBodega As String = Strings.Right("00" & IdBodega.ToString(), 2)

            Dim query As String = "
            SELECT 
                ""Codigo_Producto"" AS ""Codigo"", 
                ""Total_Almacen"" AS ""Cantidad_Lote"", 
                ""Lote"", 
                ""Fecha_Vence"", 
                ""Codigo_Bodega"" AS ""codigo_area"", 
                ""UmBas"" 
            FROM ""VW_STOCK_POR_LOTE"" 
            WHERE ""Codigo_Bodega"" = '" & codigoBodega & "'"

            dtExistenciasSAP = HanaHelper.OpenDT(query)

            cTrans.Begin_Transaction()

            clsLnTrans_inv_teorico_erp.Eliminar_Todos(cTrans.lConnection, cTrans.lTransaction)

            Try
                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                If Not SplashScreenManager.Default.IsSplashFormVisible Then
                    SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                End If
                SplashScreenManager.Default.SetWaitFormDescription("Obteniendo existencias SAP")

            Catch ex As Exception
                ' Ignorar error visual
            End Try

            For Each row As DataRow In dtExistenciasSAP.Rows

                Dim BeInvTeoricoERP As New clsBeTrans_inv_teorico_erp With {
                .Idinvteoricoerp = clsLnTrans_inv_teorico_erp.MaxID(cTrans.lConnection, cTrans.lTransaction) + 1,
                .Codigo = row("Codigo").ToString(),
                .IdProducto = clsLnProducto.Get_IdProducto_By_Codigo(row("Codigo").ToString(), cTrans.lConnection, cTrans.lTransaction),
                .IdPresentacion = 0,
                .Cant = Convert.ToDecimal(row("Cantidad_Lote")),
                .Peso = 0,
                .IdUnidadMedida = clsLnUnidad_medida.Get_IdUnidadMedida_By_Codigo(row("UmBas").ToString(), cTrans.lConnection, cTrans.lTransaction),
                .Lote = row("Lote").ToString(),
                .Fecha_vence = If(row("Fecha_Vence").ToString() = "19000101", New Date(1900, 1, 1), Convert.ToDateTime(row("Fecha_Vence"))),
                .Idbodega = IdBodega,
                .Idubicacion = 0,
                .Lic_plate = "",
                .Codigo_area = row("codigo_area").ToString(),
                .Fecha_agr = Now,
                .Usuario_agr = IdUsuarioAgr
            }

                clsLnTrans_inv_teorico_erp.Insertar(BeInvTeoricoERP, cTrans.lConnection, cTrans.lTransaction)

                SplashScreenManager.Default.SetWaitFormDescription("Código: " & BeInvTeoricoERP.Codigo)

                Application.DoEvents()
            Next

            cTrans.Commit_Transaction()
            Get_Existencias_From_SAP_AsDataTable = dtExistenciasSAP

        Catch ex As Exception
            cTrans.RollBack_Transaction()
            Throw
        Finally
            cTrans.Close_Conection()
        End Try

    End Function

    Private Sub chkIncluirIdStock_CheckedChanged(sender As Object, e As ItemClickEventArgs)
        Cargar_Datos()
    End Sub

    Private Sub chkIncluirUbicacion_CheckedChanged(sender As Object, e As ItemClickEventArgs)
        Cargar_Datos()
    End Sub

    Private Sub grdStockPorLote_Click(sender As Object, e As EventArgs) Handles grdStockPorLote.Click

    End Sub

    Private Sub GridView1_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles GridView1.RowCellStyle

        Try

            Dim view As GridView = TryCast(sender, GridView)
            If view IsNot Nothing Then
                ' Comprobar si la fila actual tiene una diferencia
                Dim diferenciaEncontrada As Boolean = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, "Diferencia_Encontrada"))
                If diferenciaEncontrada Then
                    ' Cambiar el color de fondo de la fila a rojo claro si hay una diferencia
                    e.Appearance.BackColor = Color.LightCoral
                End If
            End If

        Catch ex As Exception

        End Try

    End Sub

End Class