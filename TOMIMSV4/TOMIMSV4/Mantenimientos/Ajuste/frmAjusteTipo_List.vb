Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmAjusteTipo_List

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub ListarTipos()

        Try

            grdAjusteTipo.DataSource = clsLnAjuste_tipo.GetAll()

            If GridView1.RowCount > 0 Then

                GridView1.Columns("Idtipoajuste").Caption = "Código"
                GridView1.Columns("Modifica_lote").Caption = "Modifica Lote"
                GridView1.Columns("Momdifica_vencimiento").Caption = "Modifica Fecha Vencimiento"
                GridView1.Columns("Modifica_cantidad").Caption = "Modifica Cantidad"
                GridView1.Columns("Modifica_peso").Caption = "Modifica Peso"
                GridView1.Columns("Fec_agr").Visible = False
                GridView1.Columns("User_agr").Visible = False
                GridView1.Columns("Fec_mod").Visible = False
                GridView1.Columns("User_mod").Visible = False

            End If

            lblReg.Caption = String.Format("Registros: {0}", GridView1.RowCount)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub frmAjusteTipo_List_Load(sender As Object, e As EventArgs) Handles Me.Load
        ListarTipos()
    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        ListarTipos()
    End Sub

    Private Sub Nuevo_Tipo()

        Try

            Cierra_Instancia_Previa(frmAjusteTipo)

            With frmAjusteTipo
                .Modo = frmAjusteTipo.TipoTrans.Nuevo
                .MdiParent = MdiParent
                .InvokeListarTipos = AddressOf ListarTipos
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

    Private Sub cmdNuevo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        Nuevo_Tipo()
    End Sub

    Private Sub cmdSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdSalir.ItemClick
        Close()
    End Sub

    Private Sub Procesar_Registro()

        Try
            If GridView1.RowCount > 0 Then

                Dim Dr As New clsBeAjuste_tipo
                Dr = GridView1.GetFocusedRow
                Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle

                If Modo = pModo.Lista Then

                    Cierra_Instancia_Previa(frmAjusteTipo)

                    With frmAjusteTipo
                        .Modo = frmAjusteTipo.TipoTrans.Editar
                        .OpcionesMenu = OpcionesMenu
                        .mnuGuardar.Enabled = .OpcionesMenu.Modificar
                        .mnuActualizar.Enabled = .OpcionesMenu.Modificar
                        .mnuEliminar.Enabled = .OpcionesMenu.Eliminar
                        .gBeAjusteTipo = Dr
                        .InvokeListarTipos = AddressOf ListarTipos
                        .MdiParent = MdiParent
                        .WindowState = FormWindowState.Normal
                        .Show()
                        .Focus()
                    End With

                    GridView1.FocusedRowHandle = lSelectionIndex

                ElseIf Modo = pModo.Seleccion Then
                    Hide()
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

    Private Sub grdAjusteTipo_DoubleClick(sender As Object, e As EventArgs) Handles grdAjusteTipo.DoubleClick
        Procesar_Registro()
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

    Private Sub grdAjusteTipo_KeyDown(sender As Object, e As KeyEventArgs) Handles grdAjusteTipo.KeyDown
        If e.KeyCode = Keys.Enter Then Procesar_Registro()
    End Sub

    Private Sub frmAjusteTipo_List_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

End Class