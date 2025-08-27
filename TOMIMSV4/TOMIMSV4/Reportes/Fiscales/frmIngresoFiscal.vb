Imports System.IO
Imports System.Reflection
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen

Public Class frmIngresoFiscal

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

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        mnuActualizar.Enabled = False
        Listar_Ingresos()
        mnuActualizar.Enabled = True
    End Sub

    Private Sub Listar_Ingresos()

        Try

            If IsLoading Then Exit Sub

            Dim DT As New DataTable

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Cargando datos...")
            SplashScreenManager.Default.SetWaitFormCaption("Espere por favor.")

            DT = clsLnReportesFiscales.Get_Ingreso_Fiscal(dtpFechaDel.Value, dtpFechaAl.Value, AP.IdBodega, True)
            IsLoading = True

            dgrid.DataSource = DT

            Restore_LayOut()

            If GridView1.Columns.Count > 0 Then

                GridView1.OptionsView.ColumnAutoWidth = False
                GridView1.BestFitColumns(True)

                GridView1.Columns("IdBodega").Caption = "Código Bodega"
                GridView1.Columns("dua").Caption = "DUCA"
                GridView1.Columns("nombre").Caption = "Nombre"
                GridView1.Columns("IdOrdenCompraEnc").Caption = "Doc. de Ingreso"
                GridView1.Columns("proveedor").Caption = "Proveedor"
                GridView1.Columns("tipo_ingreso").Caption = "Tipo Ingreso"
                GridView1.Columns("No_Documento_Devolucion").Caption = "No. Doc Devolución"
                GridView1.Columns("codigo").Caption = "Código"
                GridView1.Columns("codigo_barra").Caption = "Código Barra"
                GridView1.Columns("nombre_producto").Caption = "Descripción Producto"
                GridView1.Columns("estado").Caption = "Estado"
                GridView1.Columns("cantidad").Caption = "Cantidad"
                GridView1.Columns("recibido").Caption = "Recibido"
                GridView1.Columns("nombre_unidad_medida_basica").Caption = "UmBas"
                GridView1.Columns("peso").Caption = "Peso"
                GridView1.Columns("clasificacion").Caption = "Clasificación"
                GridView1.Columns("observacion_ingreso").Caption = "Observacion Ingreso"
                GridView1.Columns("codigo_regimen").Caption = "Régimen"
                GridView1.Columns("numero_orden").Caption = "Numero Orden"
                GridView1.Columns("codigo_poliza").Caption = "Código Póliza"
                GridView1.Columns("placa").Caption = "Placa"
                GridView1.Columns("No_Contenedor").Caption = "Contenedor"
                GridView1.Columns("cbm").Caption = "CBM"
                GridView1.Columns("observacion").Caption = "Observación Recepción"
                GridView1.Columns("carta_cupo").Caption = "Carta Cupo"
                GridView1.Columns("activo").Caption = "Activo"

                GridView1.Columns("cantidad").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("cantidad").SummaryItem.DisplayFormat = "{0:n6}"
                GridView1.Columns("cantidad").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("cantidad").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("recibido").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("recibido").SummaryItem.DisplayFormat = "{0:n6}"
                GridView1.Columns("recibido").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("recibido").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Fecha_Ingreso").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                GridView1.Columns("Fecha_Ingreso").DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss"

                GridView1.Columns("fecha_llegada_poliza").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                GridView1.Columns("fecha_llegada_poliza").DisplayFormat.FormatString = "G"


                GridView1.Columns("IdOrdenCompraDet").Visible = False
                GridView1.Columns("IdOrdenCompraPol").Visible = False
                GridView1.Columns("IdProveedorBodega").Visible = False
                GridView1.Columns("IdTipoIngresoOC").Visible = False
                GridView1.Columns("IdPedidoEncDevolucion").Visible = False
                GridView1.Columns("IdPresentacion").Visible = False
                GridView1.Columns("IdProductoBodega").Visible = False
                GridView1.Columns("IdUnidadMedidaBasica").Visible = False
                GridView1.Columns("IdRegimen").Visible = False
                GridView1.Columns("IdMotivoDevolucion").Visible = False

            End If


            If GridView1.RowCount > 0 Then
                BarStaticItem1.Caption = String.Format("Registros: {0}", GridView1.RowCount)
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            IsLoading = False
            SplashScreenManager.CloseForm(False)
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

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As System.Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "TOM, WMS" &
                              vbNewLine & "Ingresos con póliza " &
                              vbNewLine & "BODEGA: " & AP.NomBodega

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub


    Private Sub GridView1_RowCellStyle(sender As Object, e As RowCellStyleEventArgs)

        If e.Column.FieldName = "estado" Then

            Dim View As GridView = sender
            Dim TipoA As Object = View.GetRowCellDisplayText(e.RowHandle, View.Columns("estado"))

            If TipoA = "ANULADA" Then
                e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                e.Appearance.BackColor = Color.Salmon
                e.Appearance.BackColor2 = Color.SeaShell
            End If

        End If

        If e.Column.FieldName = "recibido" Then

            Dim View As GridView = sender
            Dim TipoC As Object = View.GetRowCellDisplayText(e.RowHandle, View.Columns("cantidad"))
            Dim TipoR As Object = View.GetRowCellDisplayText(e.RowHandle, View.Columns("recibido"))

            If TipoC > TipoR Then
                e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                e.Appearance.BackColor = Color.Salmon
                e.Appearance.BackColor2 = Color.SeaShell
            ElseIf TipoC < TipoR Then
                e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                e.Appearance.BackColor = Color.Salmon
                e.Appearance.BackColor2 = Color.SeaShell
            Else
                e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Regular)
                e.Appearance.BackColor = Color.Green
                e.Appearance.BackColor2 = Color.White
            End If

        End If

        If e.Column.FieldName = "cantidad" Then

            Dim View As GridView = sender
            Dim TipoC As Object = View.GetRowCellDisplayText(e.RowHandle, View.Columns("cantidad"))
            Dim TipoR As Object = View.GetRowCellDisplayText(e.RowHandle, View.Columns("recibido"))

            If TipoC > TipoR Then
                e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                e.Appearance.BackColor = Color.Salmon
                e.Appearance.BackColor2 = Color.SeaShell
            ElseIf TipoC < TipoR Then
                e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                e.Appearance.BackColor = Color.Salmon
                e.Appearance.BackColor2 = Color.SeaShell
            Else
                e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Regular)
                e.Appearance.BackColor = Color.Green
                e.Appearance.BackColor2 = Color.White
            End If

        End If

    End Sub


    Private Sub GridView1_RowClick(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs)

        Try

            If Not sender.currentCell Is Nothing Then
                ReleaseRowPicking = sender.CurrentCell.RowIndex
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


    Private ReleaseRowPicking As Integer = -1
    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs)

        Dim dtHistorica As New DataTable

        Try

            Dim view As GridView = sender
            If view Is Nothing Then
                Return
            End If
            If e.FocusedRowHandle >= 0 Then
                ReleaseRowPicking = view.GetDataRow(e.FocusedRowHandle)("IdOrdenCompraEnc")
            End If

            If ReleaseRowPicking > -1 Then

                '#GT19102023: cargamos las polizas históricas sobre la OC seleccionada en el grid
                dtHistorica.Clear()
                dtHistorica = clsLnReportesFiscales.Get_Ingreso_Fiscal(dtpFechaDel.Value, dtpFechaAl.Value, AP.IdBodega, False, ReleaseRowPicking)

                If dtHistorica IsNot Nothing AndAlso dtHistorica.Rows.Count > 0 Then
                    dgridPolizasCorregidas.DataSource = dtHistorica
                    DockPanelPolizasCorregidas.Visibility = Docking.DockVisibility.Visible


                    GridView2.Columns("IdBodega").Caption = "Código Bodega"
                    GridView2.Columns("IdOrdenCompraDet").Visible = False
                    GridView2.Columns("IdOrdenCompraPol").Visible = False
                    GridView2.Columns("IdProveedorBodega").Visible = False
                    GridView2.Columns("IdTipoIngresoOC").Visible = False
                    GridView2.Columns("IdPedidoEncDevolucion").Visible = False
                    GridView2.Columns("IdPresentacion").Visible = False
                    GridView2.Columns("IdProductoBodega").Visible = False
                    GridView2.Columns("IdUnidadMedidaBasica").Visible = False
                    GridView2.Columns("IdRegimen").Visible = False

                    GridView2.OptionsView.ColumnAutoWidth = False
                    GridView2.BestFitColumns(True)

                Else
                    dgridPolizasCorregidas.DataSource = Nothing
                    DockPanelPolizasCorregidas.Visibility = Docking.DockVisibility.Hidden
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


    '#EJC20210716_1159AM:Guardar LayoutGrid en frm_ingreso_con_poliza.vb
    Private vNombreArchivoLayOutGrid As String = ""
    Private Sub frmIngresoFiscal_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            mnuGuardarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            vNombreArchivoLayOutGrid = "frm_ingreso_con_poliza.xml"

        Catch ex As Exception

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

    Private Sub GridView1_Layout(sender As Object, e As EventArgs)

        If IsLoading Then Exit Sub

        Guardar_Layout()

    End Sub

    Private Sub mnuExportarExc_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuExportarExc.ItemClick
        Exportar_Grid_A_Excel(dgrid, "WMS_IngresoFiscal.xlsx")
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

End Class