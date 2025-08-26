Imports System.IO
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmSalidaFiscal

    Private vNombreArchivoLayOutGrid As String = ""
    Public Property IsLoading As Boolean = True

    Public Sub New()
        InitializeComponent()
        IsLoading = False
    End Sub

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol
    Private DT As New DataTable("SalidasFiscales")
    'Public listarDespacho As New List(Of clsBeVW_Despacho_Rep)

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum



    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick

        Try
            cmdActualizar.Enabled = False
            Cargar()
            cmdActualizar.Enabled = True
        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub


    Private Sub Cargar()

        Try

            If IsLoading Then Exit Sub

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Obteniendo registros...")
            'ListarDespacho.Clear()
            Dim DT As New DataTable

            DT.Clear()
            'DT = clsLnVW_Despacho_Rep.Get_All_By_Rango_Fechas(dtpFechaDel.Value, dtpFechaAl.Value)
            DT = clsLnReportesFiscales.Get_Salida_Fiscal(dtpFechaDel.Value, dtpFechaAl.Value, AP.IdBodega, True)

            IsLoading = True

            grdDetalleSalidas.DataSource = DT

            Restore_layout()

            If GridView1.Columns.Count > 0 Then



                GridView1.Columns("Licencia").Caption = "Licence Plate"
                GridView1.Columns("no_pase").Caption = "No. Pase Salida"

                GridView1.Columns("Cantidad_Pickeada").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Cantidad_Pickeada").DisplayFormat.FormatString = "{0:n6}"
                GridView1.Columns("Cantidad_Pickeada").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Cantidad_Pickeada").SummaryItem.DisplayFormat = "{0:n6}"
                GridView1.Columns("Cantidad_Verificada").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Cantidad_Verificada").DisplayFormat.FormatString = "{0:n6}"
                GridView1.Columns("Cantidad_Verificada").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Cantidad_Verificada").SummaryItem.DisplayFormat = "{0:n6}"
                GridView1.Columns("Peso_Pickeado").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Peso_Pickeado").DisplayFormat.FormatString = "{0:n6}"
                GridView1.Columns("Peso_Pickeado").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Peso_Pickeado").SummaryItem.DisplayFormat = "{0:n6}"
                GridView1.Columns("Peso_Verificado").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Peso_Verificado").DisplayFormat.FormatString = "{0:n6}"
                GridView1.Columns("Peso_Verificado").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Peso_Verificado").SummaryItem.DisplayFormat = "{0:n6}"
                GridView1.Columns("Cantidad_Despachada").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Cantidad_Despachada").DisplayFormat.FormatString = "{0:n6}"
                GridView1.Columns("Cantidad_Despachada").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Cantidad_Despachada").SummaryItem.DisplayFormat = "{0:n6}"
                GridView1.Columns("Peso_Despachado").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Peso_Despachado").DisplayFormat.FormatString = "{0:n6}"
                GridView1.Columns("Peso_Despachado").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Peso_Despachado").SummaryItem.DisplayFormat = "{0:n6}"
                GridView1.Columns("Fecha_Despacho").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                GridView1.Columns("Fecha_Despacho").DisplayFormat.FormatString = "G"

                GridView1.Columns("IdBodega").Visible = False
                GridView1.Columns("IdRecepcion").Visible = False
                GridView1.Columns("IdStock").Visible = False
                GridView1.Columns("IdPedidoEnc").Visible = False
                GridView1.Columns("IdPedidoDet").Visible = False
                GridView1.Columns("IdDespachoEnc").Visible = False
                GridView1.Columns("IdDespachoDet").Visible = False
                GridView1.Columns("IdPropietarioBodega").Visible = False
                GridView1.Columns("IdProductoBodega").Visible = False
                GridView1.Columns("IdProductoEstado").Visible = False
                GridView1.Columns("IdPresentacion").Visible = False
                GridView1.Columns("IdUnidadMedida").Visible = False
                GridView1.Columns("IdProducto").Visible = False
                GridView1.Columns("Acepto").Visible = False
                GridView1.Columns("Encontrado").Visible = False
                GridView1.Columns("Codigo_Cliente").Visible = False
                GridView1.Columns("idubicacionvirtual").Visible = False
                GridView1.Columns("NombreEstado").Visible = False
                GridView1.Columns("numero").Visible = False
                GridView1.Columns("factor").Visible = False


                GridView1.OptionsView.ShowFooter = True
                GridView1.BestFitColumns(True)

                lblRegs.Caption = String.Format("Registros: 0")
                lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount - 1)

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally

            IsLoading = False

            If Not SplashScreenManager.Default Is Nothing Then
                If SplashScreenManager.Default.IsSplashFormVisible Then
                    SplashScreenManager.CloseForm(False)
                End If
            End If
        End Try

        Application.DoEvents()

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
            printLink.Component = grdDetalleSalidas
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

        Dim reportHeader As String = vbNewLine & "frmSalidas_fiscales"

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

    Private Sub dtpFechaDel_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaDel.ValueChanged

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


    Private Sub frmSalidaFiscal_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try

            mnuGuardarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            vNombreArchivoLayOutGrid = "frmSalidas_Fiscales.xml"

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

    Private Sub cmdExportarExcel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdExportarExcel.ItemClick

        Try

            Dim myStream As Stream
            Dim saveFileDialog1 As New SaveFileDialog()

            saveFileDialog1.Filter = "xlsx files (*.xlsx)|*.xlsx"
            saveFileDialog1.FilterIndex = 1
            saveFileDialog1.RestoreDirectory = True
            saveFileDialog1.FileName = "WMS_ExistenciaFiscal.xlsx"

            If saveFileDialog1.ShowDialog() = DialogResult.OK Then
                myStream = saveFileDialog1.OpenFile()
                If (myStream IsNot Nothing) Then
                    ' Code to write the stream goes here.
                    grdDetalleSalidas.ExportToXlsx(myStream)
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
End Class
