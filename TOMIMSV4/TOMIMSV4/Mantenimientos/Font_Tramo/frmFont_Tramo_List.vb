Imports System.Reflection
Imports DevExpress.XtraEditors

Public Class frmFont_Tramo_List
    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol
    Public pIdFont As Integer
    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum
    Private pBe As New List(Of clsBeFont_det)

    Private Sub Nueva_Font()

        Try

            Cierra_Instancia_Previa(frmFont_Tramo)

            With frmFont_Tramo
                .Modo = frmFont_Tramo.TipoTrans.Nuevo
                .MdiParent = MdiParent
                .OpcionesMenu = OpcionesMenu
                .mnuGuardar.Enabled = OpcionesMenu.Modificar
                .mnuActualizar.Enabled = OpcionesMenu.Modificar
                .mnuEliminar.Enabled = OpcionesMenu.Eliminar
                .InvokeListarFont = AddressOf Listar
                .WindowState = FormWindowState.Normal
                .Show()
                .Focus()
            End With

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

    Private Sub mnuNuevo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuNuevo.ItemClick
        Nueva_Font()
    End Sub

    Private Sub mnuSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSalir.ItemClick
        Close()
    End Sub

    Private Sub GridView1_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles GridFont.RowStyle

        Try

            GridFont.OptionsBehavior.Editable = False
            GridFont.OptionsSelection.EnableAppearanceFocusedCell = False

            GridFont.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus

            GridFont.OptionsSelection.EnableAppearanceFocusedRow = True
            GridFont.OptionsSelection.EnableAppearanceHideSelection = True
            GridFont.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            GridFont.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")

            GridFont.Appearance.FocusedRow.ForeColor = Color.White
            GridFont.Appearance.SelectedRow.ForeColor = Color.White

            GridFont.Appearance.SelectedRow.Options.UseBackColor = True
            GridFont.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private pObj As New clsBeFont_Det

    Private Sub Procesar_Registro()

        Try

            If (GridFont.RowCount > 0) Then

                Dim Dr As clsBeFont_Enc = GridFont.GetFocusedRow

                Dim Obj As New clsBeFont_Enc
                Obj = clsLnFont_enc.GetSingleByIdFontEnc(Dr.IdFontEnc)
                Dim lSelectionIndex As Integer = GridFont.FocusedRowHandle

                If Modo = pModo.Lista Then

                    Cierra_Instancia_Previa(frmFont_Tramo)

                    With frmFont_Tramo
                        .Modo = frmFont_Tramo.TipoTrans.Editar
                        .pObjBeFE = Obj
                        .InvokeListarFont = AddressOf Listar
                        .MdiParent = MdiParent
                        .OpcionesMenu = OpcionesMenu
                        .mnuGuardar.Enabled = OpcionesMenu.Modificar
                        .mnuActualizar.Enabled = OpcionesMenu.Modificar
                        .mnuEliminar.Enabled = OpcionesMenu.Eliminar
                        .WindowState = FormWindowState.Normal
                        .Show()
                        .Focus()
                    End With

                    GridFont.FocusedRowHandle = lSelectionIndex

                ElseIf Modo = pModo.Seleccion Then
                    'pBe = Obj
                    Hide()
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Dgrid_DoubleClick(sender As Object, e As EventArgs) Handles DgridFont.DoubleClick
        Procesar_Registro()
    End Sub

    Private Sub Listar()

        Try

            DgridFont.DataSource = Nothing

            Dim lista As New List(Of clsBeFont_enc)

            lista = clsLnFont_enc.GetAll().ToList()

            DgridFont.DataSource = lista

            'If lista IsNot Nothing AndAlso lista.Count > 0 Then

            '    Dim DT As New DataTable("Font")
            '    DT.Columns.Add("IdFontDet", GetType(Integer))
            '    DT.Columns.Add("Nombre", GetType(String))
            '    DT.Columns.Add("Letra", GetType(String))
            '    DT.Columns.Add("Tamaño", GetType(Double))
            '    DT.Columns.Add("Negrita", GetType(Boolean))
            '    DT.Columns.Add("Cursiva", GetType(Boolean))
            '    DT.Columns.Add("Subrayado", GetType(Boolean))
            '    DT.Columns.Add("ColorFont", GetType(String))
            '    DT.Columns.Add("ColorFondo", GetType(String))

            '    For Each Obj As clsBeFont_Enc In lista
            '        DT.Rows.Add(Obj.IdFontEnc, Obj.Nombre)
            '    Next

            '    DgridFont.DataSource = DT

            If (GridFont.Columns.Count <> 0) Then
                GridFont.OptionsView.ColumnAutoWidth = False
                GridFont.BestFitColumns()
            End If

            '    If (GridFont.Columns.Count <> 0) Then
            '        Try
            '            GridFont.Columns("IdFontDet").Visible = False
            '            GridFont.Columns("Nombre").Visible = True
            '            GridFont.Columns("Letra").Visible = True
            '            GridFont.Columns("Tamaño").Visible = True
            '            GridFont.Columns("Negrita").Visible = True
            '            GridFont.Columns("Cursiva").Visible = True
            '            GridFont.Columns("Subrayado").Visible = True
            '            GridFont.Columns("ColorFont").Visible = True
            '            GridFont.Columns("ColorFondo").Visible = True
            '        Catch ex As Exception
            '            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        End Try
            '    End If

            'End If

            lblRegs.Caption = "Registros: " & GridFont.RowCount

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub frmFont_Tramo_List_Load(sender As Object, e As EventArgs) Handles Me.Load
        Listar()
    End Sub

    Private Sub DgridFont_Click(sender As Object, e As EventArgs) Handles DgridFont.Click

    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        Listar()
    End Sub

    Private Sub DgridFont_KeyDown(sender As Object, e As KeyEventArgs) Handles DgridFont.KeyDown
        If e.KeyCode = Keys.Enter Then Procesar_Registro()
    End Sub

    Private Sub frmFont_Tramo_List_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

End Class