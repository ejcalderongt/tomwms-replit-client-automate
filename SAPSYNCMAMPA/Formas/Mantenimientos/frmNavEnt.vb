Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmNavEnt
    Public BeNavEnt As clsBeI_nav_ent
    Public BeNavEntFiltro As New clsBeI_nav_ent_filtros

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Private Property Modo As TipoTrans

    Sub New(ByVal pModo As TipoTrans)

        InitializeComponent()
        Modo = pModo

    End Sub

    Private Sub frmNavEnt_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            Select Case Modo

                Case TipoTrans.Nuevo

                    lblCod.Text = clsLnI_nav_ent.MaxID()
                    mnuGuardar.Enabled = True
                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False

                Case TipoTrans.Editar

                    mnuGuardar.Enabled = False
                    mnuActualizar.Enabled = True
                    mnuEliminar.Enabled = True

                    CargarDatos()
                    cargarFiltros()

            End Select

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Private Sub CargarDatos()

        Try

            If BeNavEnt IsNot Nothing Then
                lblCod.Text = BeNavEnt.Idnavent
                txtNombre.Text = BeNavEnt.Nombre
                txtPunto.Text = BeNavEnt.Endpoint
            End If

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))

        End Try

    End Sub

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick

        Try
            BeNavEnt = New clsBeI_nav_ent
            BeNavEnt.Idnavent = lblCod.Text
            BeNavEnt.Nombre = txtNombre.Text
            BeNavEnt.Endpoint = txtPunto.Text

            clsLnI_nav_ent.Insertar(BeNavEnt)

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        Try

            BeNavEnt.Nombre = txtNombre.Text
            BeNavEnt.Endpoint = txtPunto.Text

            clsLnI_nav_ent.Actualizar(BeNavEnt)

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try

            clsLnI_nav_ent.Eliminar(BeNavEnt)

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Private Sub cmdNewV_Click(sender As Object, e As EventArgs) Handles cmdNewV.Click

        cmdGuardarV.Tag = Nothing
        txtValor.Text = String.Empty
        lblCodFiltro.Text = clsLnI_nav_ent_filtros.MaxID()
        txtValor.Focus()

    End Sub

    Private Sub cmdGuardarV_Click(sender As Object, e As EventArgs) Handles cmdGuardarV.Click

        Try
            BeNavEntFiltro = New clsBeI_nav_ent_filtros
            BeNavEntFiltro.Idnaventfiltro = lblCodFiltro.Text.Trim
            BeNavEntFiltro.Idnavent = BeNavEnt.Idnavent
            BeNavEntFiltro.Valor = txtValor.Text.Trim

            clsLnI_nav_ent_filtros.Insertar(BeNavEntFiltro)

            cargarFiltros()

            cmdNewV_Click(Nothing, Nothing)

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub cargarFiltros()

        Try

            grdFiltros.DataSource = clsLnI_nav_ent_filtros.Get_All_By_IdNavEnt(BeNavEnt.Idnavent)

            If GridView1.RowCount > 0 Then
                GridView1.Columns("Idnaventfiltro").Caption = "Código"
            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub cmdEliminar_Click(sender As Object, e As EventArgs) Handles cmdEliminar.Click
        clsLnI_nav_ent_filtros.Eliminar(BeNavEntFiltro)
        cargarFiltros()

        cmdGuardarV.Tag = Nothing
        lblCodFiltro.Text = "--"
        txtValor.Text = String.Empty
        txtValor.Focus()

    End Sub

    Private Sub grdFiltros_DoubleClick(sender As Object, e As EventArgs) Handles grdFiltros.DoubleClick

        Try

            If GridView1.RowCount > 0 Then

                Dim Dr As New clsBeI_nav_ent_filtros
                Dr = GridView1.GetFocusedRow

                lblCodFiltro.Text = Dr.Idnaventfiltro
                txtValor.Text = Dr.Valor

                BeNavEntFiltro = Dr

            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

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
End Class