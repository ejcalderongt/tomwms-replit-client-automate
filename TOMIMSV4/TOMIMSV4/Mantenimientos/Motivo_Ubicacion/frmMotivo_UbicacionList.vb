Imports DevExpress.XtraEditors

Public Class frmMotivo_UbicacionList

    Public gBeMotivoUbicacion As New clsBeMotivo_ubicacion

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub frmMotivo_UbicacionList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Listar_Motivos_Ubicacion()
    End Sub

    Private Sub Listar_Motivos_Ubicacion()

        Dgrid.DataSource = Nothing

        Try

            Dim lista As New List(Of clsBeMotivo_ubicacion)

            lista = clsLnMotivo_ubicacion.GetAll(chkActivos.Checked)

            If lista.Count > 0 Then

                Dim Dt As New DataTable("MotivoUbicacion")
                Dt.Columns.Add("Código", GetType(Integer))
                Dt.Columns.Add("Descripción", GetType(String))

                For Each obj As clsBeMotivo_ubicacion In lista
                    Dt.Rows.Add(obj.IdMotivoUbicacion, obj.Nombre)
                Next

                Dgrid.DataSource = Dt

                Try
                    GridView1.OptionsView.ColumnAutoWidth = False
                    GridView1.BestFitColumns()
                Catch ex As Exception

                End Try

            End If

            lblReg.Caption = String.Format("Registros: {0}", GridView1.RowCount)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub chkActivos_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkActivos.CheckedChanged
        Listar_Motivos_Ubicacion()
    End Sub

    Private Sub mnuSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSalir.ItemClick
        Close()
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
            printLink.Component = Dgrid
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & Text

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub Procesa_Registro()

        Try

            If (GridView1.RowCount > 0) Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow
                gBeMotivoUbicacion = clsLnMotivo_ubicacion.GetSingle(Dr.Item("Código"))
                Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle

                If Modo = pModo.Lista Then

                    Cierra_Instancia_Previa(frmMotivoUbicacion)

                    With frmMotivoUbicacion
                        .Modo = frmMotivoUbicacion.TipoTrans.Editar
                        .gBeMotivoUbicacion = gBeMotivoUbicacion
                        .Listar = AddressOf Listar_Motivos_Ubicacion
                        .MdiParent = MdiParent
                        .OpcionesMenu = OpcionesMenu()
                        .mnuGuardar.Enabled = OpcionesMenu.Modificar
                        .mnuActualizar.Enabled = OpcionesMenu.Modificar
                        .mnuEliminar.Enabled = OpcionesMenu.Eliminar
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
        Procesa_Registro()
    End Sub

    Private Sub Nuevo_Motivo_Ubicacion()

        Cierra_Instancia_Previa(frmMotivoUbicacion)

        With frmMotivoUbicacion
            .Modo = frmMotivoUbicacion.TipoTrans.Nuevo
            .listar = AddressOf Listar_Motivos_Ubicacion
            .MdiParent = MdiParent
            .OpcionesMenu = OpcionesMenu()
            If OpcionesMenu IsNot Nothing Then
                .mnuGuardar.Enabled = OpcionesMenu.Modificar
                .mnuActualizar.Enabled = OpcionesMenu.Modificar
                .mnuEliminar.Enabled = OpcionesMenu.Eliminar
            End If
            .Show()
            .Focus()
        End With

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

    Private Sub mnuNuevo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuNuevo.ItemClick
        Nuevo_Motivo_Ubicacion()
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

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Dgrid_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgrid.KeyDown
        If e.KeyCode = Keys.Enter Then Procesa_Registro()
    End Sub

    Private Sub frmMotivo_UbicacionList_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

End Class