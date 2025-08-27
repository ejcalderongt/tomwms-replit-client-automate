Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmUnidad_Medida_Conversion_List

    Public UMConversion As New List(Of clsBeUnidad_medida_conversion)
    Public UMC As New clsBeUnidad_medida_conversion

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub Nueva_UnidadMedidaConversion()

        Try

            Cierra_Instancia_Previa(frmUnidad_Medida_Conversion)

            With frmUnidad_Medida_Conversion
                .Modo = frmUnidad_Medida_Conversion.TipoTrans.Nuevo
                .MdiParent = MdiParent
                .OpcionesMenu = OpcionesMenu
                .cmdGuardar.Enabled = .OpcionesMenu.Modificar
                .cmdActualizar.Enabled = .OpcionesMenu.Modificar
                .cmdEliminar.Enabled = .OpcionesMenu.Eliminar
                .InvokeListarUMConversion = AddressOf ListarUMConversion
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

    Private Sub cmdNuevo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdNuevo.ItemClick
        Nueva_UnidadMedidaConversion()
    End Sub

    Private Sub ListarUMConversion()

        Try

            UMConversion = clsLnUnidad_medida_conversion.GetAll()

            grdUMConver.DataSource = UMConversion

            GridView1.OptionsView.ColumnAutoWidth = False
            GridView1.BestFitColumns()

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub frmUnidad_Medida_Conversion_List_Load(sender As Object, e As EventArgs) Handles Me.Load
        ListarUMConversion()
    End Sub

    Private Sub Procesar_Registro()

        Try

            If GridView1.RowCount > 0 Then

                Dim DataR As clsBeUnidad_medida_conversion
                DataR = GridView1.GetFocusedRow()
                Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle

                If Modo = pModo.Lista Then

                    Cierra_Instancia_Previa(frmUnidad_Medida_Conversion)

                    With frmUnidad_Medida_Conversion
                        .Modo = frmUnidad_Medida_Conversion.TipoTrans.Editar
                        .OpcionesMenu = OpcionesMenu
                        .cmdGuardar.Enabled = .OpcionesMenu.Modificar
                        .cmdActualizar.Enabled = .OpcionesMenu.Modificar
                        .cmdEliminar.Enabled = .OpcionesMenu.Eliminar
                        .UMConversion.IdConversion = Integer.Parse(DataR.IdConversion)
                        .InvokeListarUMConversion = AddressOf ListarUMConversion
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

    Private Sub grdUMConver_DoubleClick(sender As Object, e As EventArgs) Handles grdUMConver.DoubleClick
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

            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

        End Try

    End Sub

    Private Sub cmdSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdSalir.ItemClick
        Close()
    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        ListarUMConversion()
    End Sub

    Private Sub grdUMConver_KeyDown(sender As Object, e As KeyEventArgs) Handles grdUMConver.KeyDown
        If e.KeyCode = Keys.Enter Then Procesar_Registro()
    End Sub

    Private Sub frmUnidad_Medida_Conversion_List_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub
End Class