Imports System.Data.SqlClient
Imports System.IO
Imports System.Reflection
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen

Public Class frmMovimientos_Retroactivo

    Private IsLoading As Boolean = False
    Public Property Modo As pModo
    Public Property ModoDepuracion As Boolean = False
    Public Property SeleccionMultiple As Boolean = False
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol
    Private listaStockSeleccionado As New List(Of clsBeStock_jornada)
    Private pObjStockJornada As clsBeStock_jornada
    Private pRetroactivoPendiente As Integer

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick

        cmdActualizar.Enabled = False
        Generar_Reporte()
        cmdActualizar.Enabled = True

    End Sub

    Private Sub frmAuditoriaRetroactivo_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            clsUiGridCopyHelper.AttachToForm(Me, "Copiar")
            lblPrg.Text = ""
            lblPrg.Refresh()
            pRetroactivoPendiente = 0

            IsLoading = True
            'IMS.ListarRegimen(cmbRegimen)
            AP.Listar_Bodegas_By_Usuario(cmbBodega)

            gbErrores.Visible = False
            btnResolverRetroactivo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            IsLoading = False
        End Try

    End Sub

    Private DTMovimientosSinRetroactivo As New DataTable

    Private Sub Generar_Reporte()

        Dim DT As New DataTable
        grdExistenciasConLp.DataSource = Nothing

        Try

            If Not IsLoading Then
                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormCaption("Obteniendo registros...")
            End If


            DTMovimientosSinRetroactivo = clsLnReportesFiscales.Get_Movimientos_Sin_Retroactivos(dtpFechaDesde.Value,
                                                                                                 dtpfechaHasta.Value)


            If Not DTMovimientosSinRetroactivo Is Nothing Then

                If DTMovimientosSinRetroactivo.Rows.Count > 0 Then

                    grdExistenciasConLp.DataSource = Nothing
                    grdExistenciasConLp.DataSource = DTMovimientosSinRetroactivo

                    'lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)
                    GridView1.OptionsView.ShowFooter = True

                    GridView1.Columns("estado").Caption = "Descripción"
                    GridView1.Columns("licencia").Caption = "Licencia"
                    GridView1.Columns("fecha_ingreso").Caption = "Fecha Ingreso"
                    GridView1.Columns("fecha_inicial_historico").Caption = "Fecha inicial en histórico"
                    GridView1.Columns("ticket_tms").Caption = "Ticket TMS"
                    GridView1.Columns("IdOrdenCompraEnc").Caption = "Doc. de Ingreso"
                    GridView1.Columns("IdRecepcionEnc").Caption = "Doc. de Recepción"
                    GridView1.Columns("dias_pendientes").Caption = "Días sin histórico"
                    GridView1.BestFitColumns(True)

                    lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

                    GridView1.OptionsBehavior.Editable = False

                End If

            End If


            If Not DTMovimientosSinRetroactivo Is Nothing Then

                If DTMovimientosSinRetroactivo.Rows.Count > 0 Then
                    gbErrores.Visible = True
                    btnResolverRetroactivo.Visibility = BarItemVisibility.Always
                Else
                    gbErrores.Visible = True
                    btnResolverRetroactivo.Visibility = BarItemVisibility.Never
                End If

            Else
                gbErrores.Visible = False
                btnResolverRetroactivo.Visibility = BarItemVisibility.Never
            End If

            Application.DoEvents()

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


    Private Sub GridView1_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles GridView1.RowCellStyle

        ' #EJC20260603_ROWSTYLE_PRINT_GUARD: evitar costo de formato por celda durante impresión.
        If clsUiPrintHelper.IsPrintingPreviewInProgress Then Exit Sub

        If e.Column.FieldName = "estado" Then

            Dim View As GridView = sender
            Dim TipoT As Object = View.GetRowCellDisplayText(e.RowHandle, View.Columns("estado"))

            If Not String.IsNullOrEmpty(TipoT) Then

                If TipoT = "Historico incompleto" Then
                    e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                    e.Appearance.BackColor = Color.Salmon
                    e.Appearance.BackColor2 = Color.SeaShell
                Else
                    e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Regular)
                    e.Appearance.BackColor = Color.Green
                    e.Appearance.BackColor2 = Color.White
                End If

            End If

        End If


        If e.Column.FieldName = "dias_pendientes" Then

            Dim View As GridView = sender
            Dim TipoT As Object = View.GetRowCellDisplayText(e.RowHandle, View.Columns("dias_pendientes"))

            If Not String.IsNullOrEmpty(TipoT) Then
                If TipoT > 0 Then
                    e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                    e.Appearance.BackColor = Color.Salmon
                    e.Appearance.BackColor2 = Color.SeaShell
                Else
                    e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Regular)
                    e.Appearance.BackColor = Color.Green
                    e.Appearance.BackColor2 = Color.White
                End If

            End If

        End If

    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdImprimir.ItemClick

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
            clsUiPrintHelper.PrintGridPreview(grdExistenciasConLp, AP.UsuarioAp.Nombres, AddressOf PrintableComponentLink_CreateReportHeaderArea, True)
            Exit Sub
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
            printLink.Component = grdExistenciasConLp
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
                              vbNewLine & "Resumen histórico " &
                              vbNewLine & "BODEGA: " & AP.NomBodega

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub cmdImpExcel_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdImpExcel.ItemClick

        Try

            Dim myStream As Stream
            Dim saveFileDialog1 As New SaveFileDialog()

            saveFileDialog1.Filter = "xlsx files (*.xlsx)|*.xlsx"
            saveFileDialog1.FilterIndex = 1
            saveFileDialog1.RestoreDirectory = True
            saveFileDialog1.FileName = "Movimientos_Retroactivo_" & "_Al_" & FormatoFechas.tFecha(dtpfechaHasta.Value) & ".xlsx"

            If saveFileDialog1.ShowDialog() = DialogResult.OK Then
                myStream = saveFileDialog1.OpenFile()
                If (myStream IsNot Nothing) Then
                    ' Code to write the stream goes here.
                    grdExistenciasConLp.ExportToXlsx(myStream)
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

    'Private Sub btnResolverRetroactivo_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnResolverRetroactivo.ItemClick


    '    Try

    '        Dim DataRowCount As Integer = GridView1.DataRowCount

    '        If DataRowCount = 0 Then
    '            XtraMessageBox.Show("Seleccione al menos un registro",
    '            Text,
    '            MessageBoxButtons.OK,
    '            MessageBoxIcon.Error)
    '        Else

    '            Dim licencia As String = ""
    '            Dim fecha_ticket As DateTime
    '            Dim fecha_ingreso As DateTime
    '            '#GT21062022_1623: limpiados objetos para nueva selección multiple
    '            listaStockSeleccionado = New List(Of clsBeStock_jornada)

    '            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
    '            SplashScreenManager.Default.SetWaitFormCaption("Validando selección...")

    '            For i As Integer = 0 To DataRowCount - 1

    '                'licencia = IIf(IsDBNull(GridView1.GetRowCellValue(i, "licencia")), 0, GridView1.GetRowCellValue(selectedRowHandles(i), "licencia"))
    '                'fecha_ticket = IIf(IsDBNull(GridView1.GetRowCellValue(i, "fecha_ingreso")), 0, GridView1.GetRowCellValue(selectedRowHandles(i), "fecha_ingreso"))
    '                'fecha_ingreso = IIf(IsDBNull(GridView1.GetRowCellValue(i, "fecha_inicial_historico")), 0, GridView1.GetRowCellValue(selectedRowHandles(i), "fecha_inicial_historico"))
    '                'pObjStockJornada = New clsBeStock_jornada


    '                If fecha_ticket.Date < fecha_ingreso.Date Then

    '                    pObjStockJornada.Lic_plate = licencia
    '                    pObjStockJornada.Fecha = fecha_ingreso 'uso este campo, para saber desde que fecha inserto historico, y validar dias de diferencia contra el ticket
    '                    pObjStockJornada.Fecha_Ingreso_Ticket_TMS = fecha_ticket 'uso este campo para saber la fecha desde donde debe iniciar la insersión del faltante en histórico
    '                    listaStockSeleccionado.Add(pObjStockJornada)

    '                Else
    '                    lblPrg.AppendText("Aviso: " & "la licencia " & licencia & " no aplica al proceso de retroactivo faltante.")
    '                    lblPrg.AppendText(vbNewLine)
    '                    lblPrg.Refresh()
    '                    lblPrg.SelectionStart = lblPrg.TextLength
    '                    lblPrg.ScrollToCaret()
    '                End If

    '                Application.DoEvents()

    '            Next

    '            If listaStockSeleccionado.Count > 0 Then
    '                SeleccionMultiple = True

    '                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
    '                SplashScreenManager.Default.SetWaitFormCaption("Guardando lista temporal...")

    '                Guardar_Licencia_Temporal()

    '            End If

    '        End If


    '    Catch ex As Exception

    '        SplashScreenManager.CloseForm(False)

    '        XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
    '                         Text,
    '                         MessageBoxButtons.OK,
    '                         MessageBoxIcon.Error)

    '        Dim vMsgError As String = ex.Message
    '        clsLnLog_error_wms.Agregar_Error(vMsgError)

    '    Finally
    '        SplashScreenManager.CloseForm(False)
    '    End Try

    'End Sub

    Private Sub btnResolverRetroactivo_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnResolverRetroactivo.ItemClick


        Try

            Dim DataRowCount As Integer = GridView1.DataRowCount

            If DTMovimientosSinRetroactivo.Rows.Count = 0 Then
                XtraMessageBox.Show("No hay registros pendiente de registro de retroactivo.",
                                    Text,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error)
            Else


                lblPrg.AppendText(vbNewLine)
                lblPrg.AppendText("limpiando temporales...")
                lblPrg.AppendText(vbNewLine)
                lblPrg.Refresh()
                lblPrg.SelectionStart = lblPrg.TextLength
                lblPrg.ScrollToCaret()

                clsLnStock_jornada.Limpiar_Temporal_Licencias()
                clsLnStock_jornada.Limpiar_Temporal_Stock_Jornada()

                Dim licencia As String = ""
                Dim fecha_ticket As DateTime
                Dim fecha_ingreso As DateTime
                '#GT21062022_1623: limpiados objetos para nueva selección multiple
                listaStockSeleccionado = New List(Of clsBeStock_jornada)

                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormCaption("Validando selección...")

                lblPrg.AppendText(vbNewLine)
                lblPrg.AppendText("Inicia proceso..." & Now)
                lblPrg.AppendText(vbNewLine)
                lblPrg.Refresh()
                lblPrg.SelectionStart = lblPrg.TextLength
                lblPrg.ScrollToCaret()
                lblPrg.AppendText(vbNewLine)

                For Each registro_sin_retroactivo As DataRow In DTMovimientosSinRetroactivo.Rows

                    licencia = IIf(IsDBNull(registro_sin_retroactivo.Item("licencia")), "", registro_sin_retroactivo.Item("licencia"))
                    fecha_ticket = IIf(IsDBNull(registro_sin_retroactivo.Item("fecha_ingreso")), "", registro_sin_retroactivo.Item("fecha_ingreso"))
                    fecha_ingreso = IIf(IsDBNull(registro_sin_retroactivo.Item("fecha_inicial_historico")), "", registro_sin_retroactivo.Item("fecha_inicial_historico"))

                    pObjStockJornada = New clsBeStock_jornada

                    If fecha_ticket.Date < fecha_ingreso.Date Then

                        pObjStockJornada.Lic_plate = licencia
                        pObjStockJornada.Fecha = fecha_ingreso 'uso este campo, para saber desde que fecha inserto historico, y validar dias de diferencia contra el ticket
                        pObjStockJornada.Fecha_Ingreso_Ticket_TMS = fecha_ticket 'uso este campo para saber la fecha desde donde debe iniciar la insersión del faltante en histórico
                        listaStockSeleccionado.Add(pObjStockJornada)

                        lblPrg.AppendText("Aviso: " & "la licencia " & licencia & " se agrega a lista.")
                        lblPrg.AppendText(vbNewLine)
                        lblPrg.Refresh()
                        lblPrg.SelectionStart = lblPrg.TextLength
                        lblPrg.ScrollToCaret()

                    Else
                        lblPrg.AppendText("Aviso: " & "la licencia " & licencia & " no aplica al proceso de retroactivo faltante.")
                        lblPrg.AppendText(vbNewLine)
                        lblPrg.Refresh()
                        lblPrg.SelectionStart = lblPrg.TextLength
                        lblPrg.ScrollToCaret()
                    End If

                    Application.DoEvents()

                Next

                If listaStockSeleccionado.Count > 0 Then

                    SeleccionMultiple = True

                    lblPrg.AppendText("Aviso: " & "la licencia " & licencia & " se agrega a lista.")
                    lblPrg.AppendText(vbNewLine)
                    lblPrg.Refresh()
                    lblPrg.SelectionStart = lblPrg.TextLength
                    lblPrg.ScrollToCaret()

                    SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                    SplashScreenManager.Default.SetWaitFormCaption("Guardando lista temporal...")

                    lblPrg.AppendText("Guardando lista temporal...")
                    lblPrg.AppendText(vbNewLine)
                    lblPrg.Refresh()
                    lblPrg.SelectionStart = lblPrg.TextLength
                    lblPrg.ScrollToCaret()

                    Guardar_Licencia_Temporal()

                End If

            End If


        Catch ex As Exception

            SplashScreenManager.CloseForm(False)

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub Guardar_Licencia_Temporal()

        Try

            lblPrg.AppendText(vbNewLine)
            lblPrg.AppendText("Insertando licencias en tabla temporal: " & Now)
            lblPrg.AppendText(vbNewLine)
            lblPrg.Refresh()
            lblPrg.SelectionStart = lblPrg.TextLength
            lblPrg.ScrollToCaret()

            If clsLnStock_jornada.Insertar_Temporal_Licencias_Pendientes_Retroactivo(listaStockSeleccionado) Then

                'SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                'SplashScreenManager.Default.SetWaitFormCaption("Agregando Retroactivo...")

                If Not Insertar_Retroactivo_Faltante() Then
                    Exit Sub
                End If

            End If

            lblPrg.AppendText(vbNewLine)
            lblPrg.AppendText("Fin de proceso: " & Now)
            lblPrg.AppendText(vbNewLine)
            lblPrg.Refresh()
            lblPrg.SelectionStart = lblPrg.TextLength
            lblPrg.ScrollToCaret()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            Dim vMsgError As String = String.Format("Error_13042023_1: {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            lblPrg.AppendText(vMsgError)
            lblPrg.AppendText(vbNewLine)
            lblPrg.Refresh()
            lblPrg.SelectionStart = lblPrg.TextLength
            lblPrg.ScrollToCaret()

        Finally

            clsLnStock_jornada.Limpiar_Temporal_Licencias()
            clsLnStock_jornada.Limpiar_Temporal_Stock_Jornada()
            SplashScreenManager.CloseForm(False)

        End Try

    End Sub

    Private Function Insertar_Retroactivo_Faltante() As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand
        Insertar_Retroactivo_Faltante = False
        Dim returnValue As Integer = 0

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim pTimeOut = clsBD.Instancia.TimeOutConBD

            lblPrg.AppendText(vbNewLine)
            lblPrg.AppendText("Ejecutando SP para insertar desfase...")
            lblPrg.AppendText(vbNewLine)
            lblPrg.Refresh()
            lblPrg.SelectionStart = lblPrg.TextLength
            lblPrg.ScrollToCaret()

            Dim vSQL As String = "SP_STOCK_JORNADA_DESFASE_RETROACTIVO"
            cmd = New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.StoredProcedure}
            cmd.CommandTimeout = 120
            cmd.Parameters.Add("@RegistrosARevisar", SqlDbType.Int)
            cmd.Parameters("@RegistrosARevisar").Direction = ParameterDirection.Output
            If pTimeOut > 0 Then
                cmd.CommandTimeout = pTimeOut
            End If
            cmd.ExecuteNonQuery()
            returnValue = cmd.Parameters("@RegistrosARevisar").Value

            If (returnValue > 0) Then

                lblPrg.AppendText(vbNewLine)
                lblPrg.AppendText("Aviso: El proceso histórico con retroactivo faltante agregó " & returnValue & " registros.")
                lblPrg.AppendText(vbNewLine)
                lblPrg.Refresh()
                lblPrg.SelectionStart = lblPrg.TextLength
                lblPrg.ScrollToCaret()
            Else
                lblPrg.AppendText(vbNewLine)
                lblPrg.AppendText("Error: El proceso histórico con retroactivo no devolvió cuantos registros inserto!.")
                lblPrg.AppendText(vbNewLine)
                lblPrg.Refresh()
                lblPrg.SelectionStart = lblPrg.TextLength
                lblPrg.ScrollToCaret()

            End If

            clsLnStock_jornada.Limpiar_Temporal_Licencias(lConnection, lTransaction)
            clsLnStock_jornada.Limpiar_Temporal_Stock_Jornada(lConnection, lTransaction)

            lblPrg.AppendText(vbNewLine)
            lblPrg.AppendText("limpiando data temporal...")
            lblPrg.AppendText(vbNewLine)
            lblPrg.Refresh()
            lblPrg.SelectionStart = lblPrg.TextLength
            lblPrg.ScrollToCaret()

            lTransaction.Commit()
            Insertar_Retroactivo_Faltante = True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
            Dim vMsgError As String = String.Format("Error_13042023_2: {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lTransaction.Dispose()
            lConnection.Dispose()
        End Try

    End Function


End Class



