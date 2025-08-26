Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmNavEnt_List

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Property Modo As pModo

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub Listar()

        Try

            grdEntidad.DataSource = clsLnI_nav_ent.GetAll()

            lblRegs.Caption = "Registros: " & GridView1.RowCount

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuNuevo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuNuevo.ItemClick
        Dim Entidad As New frmNavEnt(frmNavEnt.TipoTrans.Nuevo)
        Entidad.ShowDialog()
        Entidad.Dispose()
        Listar()
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        Listar()
    End Sub

    Private Sub grdEntidad_DoubleClick(sender As Object, e As EventArgs) Handles grdEntidad.DoubleClick

        Try

            Modo = pModo.Lista

            If (GridView1.RowCount > 0) Then

                Dim Dr As New clsBeI_nav_ent
                Dr = GridView1.GetFocusedRow

                Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle

                If Modo = pModo.Lista Then
                    Dim NavEnt As New frmNavEnt(frmNavEnt.TipoTrans.Editar) With {.BeNavEnt = Dr}
                    NavEnt.ShowDialog()
                    NavEnt.Dispose()
                    Listar()
                    GridView1.FocusedRowHandle = lSelectionIndex
                ElseIf Modo = pModo.Seleccion Then
                    Hide()
                End If
            End If
        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

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

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub frmNavEnt_List_Load(sender As Object, e As EventArgs) Handles Me.Load
        Listar()
    End Sub
End Class