Imports System.IO
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen

Public Class frmMovimientosporLote

    Private pListMovs As New List(Of clsBeVW_Movimientos)
    Private DT As New DataTable("MovimientosKardexLote")
    Private IsLoading As Boolean = False
    Public pIdProducto As Integer

    Public Property Modo As pModo
    Public Property ModoDepuracion As Boolean = False
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub frmMovIngresoSalidaConLote_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            '#EJC20210716:Restaurar LayoutGrid en LotesPorUbi.
            vNombreArchivoLayOutGrid = "grdMovimientosPorLote.xml"

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Inicializando reporte...")

            AP.Listar_Bodegas_By_Usuario(cmbBodega)

            cmbBodega.EditValue = Integer.Parse(AP.IdBodega)

            Cargar()

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                  Text,
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Error)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub


    Private Sub Cargar()

        Dim dt As DataTable

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Consultando datos...")


            If Modo = pModo.Seleccion Then
                dt = clsLnTrans_movimientos.Get_All_Movimientos_By_Producto(cmbBodega.EditValue, dtpFechaDel.Value, dtpFechaAl.Value, pIdProducto)
            Else
                dt = clsLnTrans_movimientos.Get_Movimientos(cmbBodega.EditValue, dtpFechaDel.Value, dtpFechaAl.Value, txtLote.Text)
            End If

            If cmbBodega.Text <> "" Then

                grdKardex.DataSource = Nothing

                If dt.Rows.Count > 0 Then

                    grdKardex.DataSource = dt

                End If

            End If


            If GridView1.Columns.Count > 0 Then

                Restore_LayOut()

                GridView1.OptionsView.ShowFooter = True

                GridView1.Columns("codigo").Group()
                GridView1.Columns("lote").Group()


                Dim item As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "cantidad",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "DISP: {0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("cantidad")}
                GridView1.GroupSummary.Add(item)


                Dim item1 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "peso",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("peso")}
                GridView1.GroupSummary.Add(item1)

                Dim item2 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "Cantidad_Presentacion",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "DISP: {0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("Cantidad_Presentacion")}
                GridView1.GroupSummary.Add(item2)

                Dim item3 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "Propietario",
                .SummaryType = DevExpress.Data.SummaryItemType.Count,
                .DisplayFormat = "Movimientos: {0:n}",
                .ShowInGroupColumnFooter = GridView1.Columns("Propietario")}
                GridView1.GroupSummary.Add(item3)


                GridView1.Columns("IdBodegaOrigen").Visible = False
                GridView1.Columns("codigo_barra").Caption = "Código Barra"
                GridView1.Columns("codigo").Caption = "Código"
                GridView1.Columns("cantidad").Caption = "Cantidad U.M.Bas"
                GridView1.Columns("peso").Caption = "Peso"
                GridView1.Columns("lote").Caption = "Lote"
                GridView1.Columns("fecha").Caption = "Fecha Movimiento"
                GridView1.Columns("fecha_vence").Caption = "Fecha Vence"
                GridView1.Columns("barra_pallet").Caption = "Licencia"
                GridView1.Columns("Cantidad_Presentacion").Caption = "Cantidad Presentación"

                GridView1.Columns("fecha").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                GridView1.Columns("fecha").DisplayFormat.FormatString = "G"


                GridView1.ExpandAllGroups()

                GridView1.BestFitColumns(True)

                'lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub GridView1_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles GridView1.RowCellStyle

        Try

            If e.Column.FieldName = "Cantidad" Then

                Dim View As GridView = sender
                Dim TipoT As Object = View.GetRowCellDisplayText(e.RowHandle, View.Columns("Tipo Tarea"))

                If TipoT = "DESP" Then
                    e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                    e.Appearance.BackColor = Color.Salmon
                    e.Appearance.BackColor2 = Color.SeaShell
                ElseIf TipoT = "AJCANTN" Then
                    e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                    e.Appearance.BackColor = Color.Salmon
                    e.Appearance.BackColor2 = Color.SeaShell
                ElseIf TipoT = "AJCANTP" Then
                    e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                    e.Appearance.BackColor = Color.Green
                    e.Appearance.BackColor2 = Color.White
                ElseIf TipoT = "RECE" Then
                    e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Regular)
                    e.Appearance.BackColor = Color.Green
                    e.Appearance.BackColor2 = Color.White
                End If


            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)

        End Try

    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Cargar()
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

        Dim reportHeader As String = vbNewLine & "Listado de Movimientos Por Lote"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub cmdSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdSalir.ItemClick
        Close()
    End Sub

    Private vNombreArchivoLayOutGrid As String = ""

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

End Class