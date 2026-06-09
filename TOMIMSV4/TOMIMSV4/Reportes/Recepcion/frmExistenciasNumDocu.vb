Imports System.Drawing.Printing
Imports System.Reflection
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen

Public Class frmExistenciasNumDocu

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Property Modo As pModo
    Public Property gBeRecepcion As New clsBeTrans_re_enc
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub ListarIngresos()

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Obteniendo registros...")

            Dim DT As New DataTable

            DT = clsLnTrans_re_enc.Get_All_Existencias_Por_Documento(chkActivos.Checked, dtpFechaDel.Value, dtpFechaAl.Value)

            grdExistencias.DataSource = DT

            If GridView1.Columns.Count > 0 Then

                GridView1.OptionsView.ShowFooter = True

                GridView1.Columns("activo").Visible = False
                'GridView1.Columns("IdBodega").Visible = False

                GridView1.Columns("Usuario_Agrego").Visible = False
                GridView1.Columns("Fecha_Agrego").Visible = False
                GridView1.Columns("NombreProd").Caption = "Producto"
                GridView1.Columns("CodigoProd").Caption = "CódigoProd"
                GridView1.Columns("BarraProd").Caption = "Barra"
                GridView1.Columns("UM").Caption = "UMBas"
                GridView1.Columns("estado").Caption = "Estado"
                GridView1.Columns("PresProd").Caption = "Presentación"

                GridView1.Columns("Recibido").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Recibido").SummaryItem.DisplayFormat = "{0:n6}"
                GridView1.Columns("Recibido").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Recibido").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Existencia_Actual_UMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Existencia_Actual_UMBas").SummaryItem.DisplayFormat = "{0:n6}"
                GridView1.Columns("Existencia_Actual_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Existencia_Actual_UMBas").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Existencia_Actual_Pres").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Existencia_Actual_Pres").SummaryItem.DisplayFormat = "{0:n6}"
                GridView1.Columns("Existencia_Actual_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Existencia_Actual_Pres").DisplayFormat.FormatString = "{0:n6}"

                GridView1.OptionsView.ColumnAutoWidth = False

                GridView1.BestFitColumns(True)

            End If

            If GridView1.RowCount > 0 Then
                BarStaticItem1.Caption = String.Format("Registros: {0}", GridView1.RowCount)
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        Finally
            If Not SplashScreenManager.Default Is Nothing Then
                If SplashScreenManager.Default.IsSplashFormVisible Then
                    SplashScreenManager.CloseForm(False)
                End If
            End If
        End Try

        Application.DoEvents()

    End Sub

    Private Sub dtpFechaDel_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaDel.ValueChanged

        Try

            'If Me.dtpFechaDel.Value > Me.dtpFechaAl.Value Or Me.dtpFechaAl.Value < Me.dtpFechaDel.Value Then Throw New Exception("Seleccione un rango de fechas válido.")

            ListarIngresos()

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

            'If Me.dtpFechaDel.Value > Me.dtpFechaAl.Value Or Me.dtpFechaAl.Value < Me.dtpFechaDel.Value Then Throw New Exception("Seleccione un rango de fechas válido.")

            ListarIngresos()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub chkActivos_CheckedChanged(sender As Object, e As ItemClickEventArgs) Handles chkActivos.CheckedChanged
        ListarIngresos()
    End Sub

    Private Sub Imprimir_Vista()

        Try
            clsUiPrintHelper.PrintGridPreview(grdExistencias, AP.UsuarioAp.Nombres, AddressOf PrintableComponentLink_CreateReportHeaderArea, True)
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
            printLink.Component = grdExistencias
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Listado de Ingresos"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub GridView1_RowStyle(sender As Object, e As RowStyleEventArgs) Handles GridView1.RowStyle

        Try

            GridView1.OptionsBehavior.Editable = False
            GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
            GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
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

    Private Sub frmExistenciasNumDocu_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdActualizar.ItemClick
        ListarIngresos()
    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdImprimir.ItemClick
        Imprimir_Vista()
    End Sub

    Private Sub cmdSalir_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdSalir.ItemClick
        Close()
    End Sub

    Private Sub frmExistenciasNumDocu_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try
            clsUiGridCopyHelper.AttachToForm(Me, "Copiar")
            ListarIngresos()
        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub cmdImprimirLicencia_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdImprimirLicencia.ItemClick

        Try

            If (GridView1.RowCount > 0) Then

                If GridView1.SelectedRowsCount > 0 Then

                    Dim Dr As DataRow
                    Dim lp As String = ""
                    Dim Cod As String = ""
                    Dim Nombre As String = ""

                    For i As Integer = 0 To GridView1.SelectedRowsCount - 1

                        If GridView1.GetSelectedRows(i) >= 0 Then

                            Dr = GridView1.GetDataRow(GridView1.GetSelectedRows(i))

                            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)

                            Dim pd As PrintDialog = New PrintDialog()
                            pd.PrinterSettings = New PrinterSettings()

                            '#EJC20210714: Validar que el LP no sea nulo.
                            lp = IIf(IsDBNull(Dr.Item("Licencia")), "", Dr.Item("Licencia"))
                            Cod = Dr.Item("CodigoProd")
                            Nombre = Dr.Item("NombreProd")

                            If lp <> "" Then
                                SplashScreenManager.Default.SetWaitFormCaption("Imprimiendo barra de pallet")
                                Imprimir_Etiqueta(lp, Cod, Nombre, pd.PrinterSettings.PrinterName)
                                SplashScreenManager.CloseForm(False)
                            End If

                        End If

                    Next

                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub Imprimir_Etiqueta(ByVal Lp As String, ByVal CodigoProd As String, ByVal NombreProd As String, ByVal PrinterName As String)
        Dim vIdTipoEtiqueta As Integer
        Dim vZPL As String = ""
        Dim BeProducto As New clsBeProducto
        Dim CodigoBodega As String = ""
        Dim NombreBodega As String = ""
        Dim NombreEmpresa As String = ""

        Try

            BeProducto = clsLnProducto.Get_BeProducto_By_Codigo(CodigoProd, AP.Bodega.IdBodega)
            vIdTipoEtiqueta = BeProducto.IdTipoEtiqueta
            CodigoBodega = AP.Bodega.Codigo
            NombreBodega = AP.Bodega.Nombre
            NombreEmpresa = AP.NomEmpresa

            If vIdTipoEtiqueta = 1 Then
                vZPL = String.Format("^XA " +
                                        "^MMT " +
                                        "^PW700 " +
                                        "^LL0406 " +
                                        "^LS0 " +
                                        "^FT450,21^A0I,20,14^FH^FD{4}^FS " +
                                        "^FO2,40^GB670,0,5^FS " +
                                        "^FT270,61^A0I,30,24^FH^FD{0}^FS " +
                                        "^FT550,61^A0I,30,24^FH^FD{1}^FS " +
                                        "^FT670,306^A0I,30,24^FH^FD{2}^FS " +
                                        "^FT360,61^A0I,30,24^FH^FDBodega:^FS " +
                                        "^FT670,61^A0I,30,24^FH^FDEmpresa:^FS " +
                                        "^FT670,367^A0I,25,24^FH^FDTOMWMS No. Licencia^FS " +
                                        "^FO2,340^GB670,0,14^FS " +
                                        "^BY3,3,160^FT670,131^BCI,,Y,N " +
                                        "^FD{3}^FS " +
                                        "^PQ1,0,1,Y " +
                                        "^XZ", CodigoBodega + " - " + NombreBodega, NombreEmpresa,
                                        BeProducto.Codigo + " - " + BeProducto.Nombre,
                                        "$" + Lp,
                                        AP.UsuarioAp.Nombres + " " + AP.UsuarioAp.Apellidos + " / " + Format(Now, "dd-MMM-yyyy"))

            ElseIf vIdTipoEtiqueta = 2 Then
                vZPL = String.Format("^XA " +
                            "^MMT " +
                            "^PW600 " +
                            "^LL0406 " +
                            "^LS0 " +
                            "^FT450,21^A0I,20,14^FH^FD{4}^FS " +
                            "^FO2,40^GB670,0,5^FS  " +
                            "^FT440,100^A0I,28,30^FH^FD{0}^FS " +
                            "^FT560,100^A0I,26,30^FH^FDBodega:^FS " +
                            "^FT440,135^A0I,28,30^FH^FD{1}^FS " +
                            "^FT560,135^A0I,26,30^FH^FDEmpresa:^FS " +
                            "^FT560,180^A0I,90,100^FH^FD{2}^FS " +
                            "^BY3,3,160^FT550,280^BCI,,N,N " +
                            "^FD{2}^FS " +
                            "^PQ1,0,1,Y  " +
                            "^FT560,480^A0I,35,40^FH^FD{3}^FS " +
                            "^FO2,520^GB670,14,14^FS " +
                            "^FT560,550^A0I,25,24^FH^FDTOMWMS  No. Licencia^FS " +
                            "^XZ", CodigoBodega + "-" + NombreBodega,
                            NombreEmpresa,
                            "$" + Lp,
                            BeProducto.Codigo + " - " + BeProducto.Nombre,
                            AP.UsuarioAp.Nombres + " " + AP.UsuarioAp.Apellidos + " / " + Format(Now, "dd-MMM-yyyy"))

            ElseIf vIdTipoEtiqueta = 4 Then
                vZPL = String.Format("^XA " +
                                        "^MMT " +
                                        "^PW812 " +
                                        "^LL0630 " +
                                        "^LS0 " +
                                        "^FT450,21^A0I,20,14^FH^FD{4}^FS " +
                                        "^FO2,40^GB670,0,5^FS " +
                                        "^FT270,61^A0I,30,24^FH^FD{0}^FS " +
                                        "^FT550,61^A0I,30,24^FH^FD{1}^FS " +
                                        "^FT670,306^A0I,30,24^FH^FD{2}^FS " +
                                        "^FT360,61^A0I,30,24^FH^FDBodega:^FS " +
                                        "^FT670,61^A0I,30,24^FH^FDEmpresa:^FS " +
                                        "^FT670,367^A0I,25,24^FH^FDTOMWMS No. Licencia^FS " +
                                        "^FO2,340^GB670,0,14^FS " +
                                        "^BY3,3,160^FT670,131^BCI,,Y,N " +
                                        "^FD{3}^FS " +
                                        "^PQ1,0,1,Y " +
                                        "^XZ", CodigoBodega + " - " + NombreBodega, NombreEmpresa,
                                        BeProducto.Codigo + " - " + BeProducto.Nombre,
                                        "$" + Lp,
                                        AP.UsuarioAp.Nombres + " " + AP.UsuarioAp.Apellidos + " / " + Format(Now, "dd-MMM-yyyy"))
            End If

            If vZPL <> "" Then
                RawPrinterHelper.SendStringToPrinter(PrinterName, vZPL)
            Else
                XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), "No está definido el tipo de etiqueta"),
                                    Text,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error)
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



