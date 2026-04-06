Imports System.EnterpriseServices
Imports System.Reflection
Imports DevExpress.XtraEditors

Public Class frmAjusteStock_List

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub Listar_Ajustes()
        Try

            Dgrid.DataSource = clsLnTrans_ajuste_enc.Get_All_VW(dtpFechaDel.Value, dtpFechaAl.Value, AP.IdBodega)
            GridView1.BestFitColumns()

            lblRegs.Caption = "Registros: " & GridView1.RowCount
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub Nuevo_Ajuste()

        Try

            Cierra_Instancia_Previa(frmAjusteStock)

            With frmAjusteStock
                .Modo = frmAjusteStock.TipoTrans.Nuevo
                .MdiParent = MdiParent
                .InvokeListarAjustes = AddressOf Listar_Ajustes
                .WindowState = FormWindowState.Maximized

                If OpcionesMenu IsNot Nothing Then
                    .OpcionesMenu = OpcionesMenu
                End If

                .mnuGuardar.Enabled = IIf(OpcionesMenu IsNot Nothing, OpcionesMenu.Modificar, True)

                .Show()
                .Focus()
            End With

            Listar_Ajustes()

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Public Sub Cierra_Instancia_Previa(ByRef Myform As Form)

        Try

            For Each objForm In My.Application.OpenForms
                If (Trim(objForm.Name) = Trim(Myform.Name)) Then
                    Myform.Close()
                    Exit For
                End If
            Next

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub mnuNuevo_ItemClick_1(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuNuevo.ItemClick
        Nuevo_Ajuste()
    End Sub

    Private Sub mnuActualizar_ItemClick_1(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        mnuActualizar.Enabled = False
        Listar_Ajustes()
        mnuActualizar.Enabled = True
    End Sub

    Private Sub mnuSalir_ItemClick_1(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSalir.ItemClick
        Close()
    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick
        Imprimir_Vista()
    End Sub

    Private Sub Procesar_Registro()

        Try

            If (GridView1.RowCount > 0) Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow
                Dim IdAjusteEnc As Integer = Dr.Item("Correlativo")
                Dim enc = New clsBeTrans_ajuste_enc
                enc = clsLnTrans_ajuste_enc.GetSingle(IdAjusteEnc)

                Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle

                If Modo = pModo.Lista Then

                    Cierra_Instancia_Previa(frmAjusteStock)

                    With frmAjusteStock

                        .Modo = frmAjusteStock.TipoTrans.Editar
                        .pBeTransAjustEnc = enc
                        .InvokeListarAjustes = AddressOf Listar_Ajustes
                        .MdiParent = MdiParent
                        .WindowState = FormWindowState.Maximized

                        If OpcionesMenu IsNot Nothing Then
                            .OpcionesMenu = OpcionesMenu
                        End If

                        .mnuGuardar.Enabled = IIf(OpcionesMenu IsNot Nothing, OpcionesMenu.Modificar, True)

                        .Show()
                        .Focus()
                    End With

                    GridView1.FocusedRowHandle = lSelectionIndex

                ElseIf Modo = pModo.Seleccion Then
                    Hide()
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Dgrid_DoubleClick(sender As Object, e As EventArgs) Handles Dgrid.DoubleClick
        Procesar_Registro()
    End Sub

    Private Sub GridView1_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles GridView1.RowStyle

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

            If e.RowHandle >= 0 Then

                Dim vEsBorrador As Boolean = False
                Dim vValor = GridView1.GetRowCellValue(e.RowHandle, "borrador")

                If vValor IsNot Nothing AndAlso Not IsDBNull(vValor) Then
                    Boolean.TryParse(vValor.ToString(), vEsBorrador)
                End If

                If vEsBorrador Then
                    e.Appearance.BackColor = Color.MistyRose
                    e.Appearance.ForeColor = Color.Black
                    e.HighPriority = True
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

    Private Sub dtpFechaDel_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaDel.ValueChanged

        Try

            'If dtpFechaDel.Value > dtpFechaAl.Value Or dtpFechaAl.Value < dtpFechaDel.Value Then Throw New Exception("Seleccione un rango de fechas válido.")

            Listar_Ajustes()

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

            'If dtpFechaDel.Value > dtpFechaAl.Value Or dtpFechaAl.Value < dtpFechaDel.Value Then Throw New Exception("Seleccione un rango de fechas válido.")

            Listar_Ajustes()

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
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Listado de Ajustes de Inventario"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub Dgrid_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgrid.KeyDown
        If e.KeyCode = Keys.Enter Then Procesar_Registro()
    End Sub

    Private Sub frmAjusteStock_List_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub frmAjusteStock_List_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Listar_Ajustes()
    End Sub
End Class