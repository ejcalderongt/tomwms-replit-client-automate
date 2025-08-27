Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmBodegaTramo_List

    Public lista As New List(Of clsBeBodega_tramo)
    Public gBeBodegaTramo As New clsBeBodega_tramo
    Public pIdBodega As Integer
    Public pIdInventario As Integer

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Property Modo As pModo
    Enum pModo
        Lista = 1
        Seleccion = 2
        Inventario = 3
    End Enum

    Private Sub cargar()

        Try

            If Modo = pModo.Inventario Then
                grdTramos.DataSource = clsLnBodega_tramo.GetAllByInventario(pIdInventario)
            Else
                grdTramos.DataSource = clsLnBodega_tramo.GetAll()
            End If

            If GridView1.RowCount > 0 Then
                GridView1.Columns("IdFontEnc").Visible = False
                GridView1.Columns("IdTipoProductoDefault").Visible = False
                GridView1.Columns("Orientacion").Visible = False
                GridView1.Columns("Indice_x").Visible = False
                GridView1.Columns("Codigo").Visible = False
                GridView1.Columns("Margen_inferior").Visible = False
                GridView1.Columns("Margen_superior").Visible = False
                GridView1.Columns("Margen_derecho").Visible = False
                GridView1.Columns("Margen_izquierdo").Visible = False
                GridView1.Columns("Activo").Visible = False
                GridView1.Columns("Fec_mod").Visible = False
                GridView1.Columns("User_mod").Visible = False
                GridView1.Columns("Fec_agr").Visible = False
                GridView1.Columns("User_agr").Visible = False
                GridView1.Columns("Sistema").Visible = False
                GridView1.Columns("VolumenUtilizado").Visible = False
                GridView1.Columns("CantidadUtilizada").Visible = False
                GridView1.Columns("Tag").Visible = False
                GridView1.Columns("pFont").Visible = False
                GridView1.BestFitColumns()
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub frmBodegaTramo_List_Load(sender As Object, e As EventArgs) Handles Me.Load
        cargar()
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        mnuActualizar.Enabled = False
        cargar()
        mnuActualizar.Enabled = True
    End Sub

    Private Sub mnuSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSalir.ItemClick
        Close()
    End Sub

    Private Sub grdTramos_DoubleClick(sender As Object, e As EventArgs) Handles grdTramos.DoubleClick

        Try

            If (GridView1.RowCount > 0) Then

                Dim Dr As clsBeBodega_tramo
                Dr = GridView1.GetFocusedRow()
                Dim Obj As New clsBeBodega_tramo

                Obj = clsLnBodega_tramo.GetSingle(Dr.IdTramo, AP.IdBodega)
                Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle
                If Modo = pModo.Seleccion Then
                    gBeBodegaTramo = Obj
                    Hide()
                ElseIf Modo = pModo.Inventario Then
                    gBeBodegaTramo = Obj
                    Hide()
                End If

            End If


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub
End Class