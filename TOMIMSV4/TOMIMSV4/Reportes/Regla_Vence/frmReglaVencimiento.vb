Imports System.IO
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmReglaVencimiento

    Public Property vNombreArchivoLayOutGrid As String = ""
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol
    Private IsLoading As Boolean = False

    Private Sub frmReglaVencimiento_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try

            vNombreArchivoLayOutGrid = DgridReglasVencimiento.Name & ".xml"

            IsLoading = True

            Listar_Reporte()

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
       Text,
       MessageBoxButtons.OK,
       MessageBoxIcon.Error)
        Finally
            IsLoading = False
        End Try
    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick
        Try
            cmdImprimir.Enabled = False
            Imprimir_Vista()
            cmdImprimir.Enabled = True

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
      Text,
      MessageBoxButtons.OK,
      MessageBoxIcon.Error)
        End Try
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
            printLink.Component = DgridReglasVencimiento
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

        Dim reportHeader As String = vbNewLine & "Reporte por Regla de Vencimiento"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub cmdSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdSalir.ItemClick
        Close()
    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick

        cmdActualizar.Enabled = False
        Listar_Reporte()
        cmdActualizar.Enabled = True

    End Sub

    Private Sub Listar_Reporte()

        Dim dtReglasVencimiento As New DataTable
        DgridReglasVencimiento.DataSource = Nothing

        Try

            If Not IsLoading Then
                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormCaption("Obteniendo registros...")
            End If


            '#GT21122023: iteramos las reglas de vencimiento registradas sin discriminar bodega
            Dim listReglasVencimiento As New List(Of clsBeRegla_vencimiento)
            listReglasVencimiento = clsLnRegla_vencimiento.Get_All()

            For Each regla In listReglasVencimiento

                Dim DT As New DataTable
                Dim IdReglaVencimiento As Integer = 0
                Dim pReglaVencimiento As String = ""
                Dim pIPropietarioMercancia As Integer = 0
                Dim pIPropietarioBodega = 0
                Dim pDiasRegla As Integer = IIf(regla.TiempoVencimientoDias > 0, regla.TiempoVencimientoDias, 0)

                IdReglaVencimiento = regla.IdReglaVencimiento
                pReglaVencimiento = regla.NombreRegla
                pIPropietarioBodega = clsLnPropietario_bodega.Get_IdPropietarioBodega_By_IdPropietario_And_IdBodega(regla.IdPropietarioMercancia, regla.IdBodega)

                DT = clsLnStock.Get_Rpt_Horizonte_Critico_By_IdBodega_And_IdPropietarioBodega(0,
                                                                                              regla.IdBodega,
                                                                                              pIPropietarioBodega,
                                                                                              pDiasRegla,
                                                                                              False)

                Dim colTiempoVencimiento As New DataColumn("TiempoVencimientoDias", GetType(Int32))
                Dim colNombreRegla As New DataColumn("NombreRegla", GetType(String))
                colTiempoVencimiento.DefaultValue = regla.TiempoVencimientoDias
                colNombreRegla.DefaultValue = regla.NombreRegla & " - " & regla.TiempoVencimientoDias & " (días vencimiento)"
                DT.Columns.Add(colTiempoVencimiento)
                DT.Columns.Add(colNombreRegla)

                If DT.Rows.Count > 0 Then
                    dtReglasVencimiento.Merge(DT, False)
                End If
            Next

            If dtReglasVencimiento.Rows.Count > 0 Then
                lblRegs.Caption = "Registros: " & dtReglasVencimiento.Rows.Count
            End If

            DgridReglasVencimiento.DataSource = dtReglasVencimiento

            If GridView1.RowCount > 0 Then

                GridView1.Columns(15).GroupIndex = 0

                GridView1.Columns("TiempoVencimientoDias").Caption = "Restricción (días)"
                GridView1.Columns("NombreRegla").Caption = "Regla"
                GridView1.Columns("Ubic").Caption = "Ubicación"
                GridView1.Columns("Fecha_Vence").Caption = "Vence"
                GridView1.Columns("Fecha_Ingreso").Caption = "Ingreso"
                GridView1.Columns("lic_plate").Caption = "Licencia"


                GridView1.Columns("CantPres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("CantPres").DisplayFormat.FormatString = "{0:n6}"
                GridView1.Columns("CantPres").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("CantPres").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("CantUMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("CantUMBas").DisplayFormat.FormatString = "{0:n6}"
                GridView1.Columns("CantUMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("CantUMBas").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("Tolerancia_dias").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Tolerancia_dias").DisplayFormat.FormatString = "{0:n2}"

                GridView1.OptionsBehavior.AutoExpandAllGroups = True
                GridView1.OptionsView.ShowFooter = True
                GridView1.BestFitColumns(True)

            End If

            Restore_LayOut()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            If Not SplashScreenManager.Default Is Nothing Then
                If SplashScreenManager.Default.IsSplashFormVisible Then
                    SplashScreenManager.CloseForm(False)
                End If
            End If
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

    Private Sub GridView1_Layout(sender As Object, e As EventArgs) Handles GridView1.Layout

        If IsLoading Then Exit Sub

        Guardar_Layout()

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

    Private Sub mnuEnviarCorreo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEnviarCorreo.ItemClick

        Try

            DgridReglasVencimiento.ExportToXlsx("WMS_ReglasVencimiento.xlsx")

            Dim frmMail As New frmcorreorpt
            frmMail.gridReporte = DgridReglasVencimiento
            frmMail.ShowDialog()

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
          Text,
          MessageBoxButtons.OK,
          MessageBoxIcon.Error)
        End Try

    End Sub
End Class