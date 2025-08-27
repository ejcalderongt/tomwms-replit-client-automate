Imports DevExpress.XtraEditors

Public Class frmConfiguracionList

    Public Sub New()
        InitializeComponent()
    End Sub
    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol
    Public idEmpresa As New Integer
    Public IdConfiguracionInterface As New Integer

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub Listar_Configuraciones()
        Try

            If (IdConfiguracionInterface <> -1) Then
                Dgrid.DataSource = clsLnI_nav_config_enc.ListarFiltrados_By_Correlativo(IdConfiguracionInterface)
            Else
                Dgrid.DataSource = clsLnI_nav_config_enc.ListarFiltrados()
            End If

            If (GridView1.Columns.Count <> 0) Then
                Try
                    GridView1.Columns("idempresa").Visible = False
                    GridView1.Columns("idbodega").Visible = False
                    GridView1.Columns("idPropietario").Visible = False
                    mnuNuevo.Enabled = False


                Catch ex As Exception
                    XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End Try
            Else
                mnuNuevo.Enabled = True
            End If

            lblRegs.Caption = "Registros: " & GridView1.RowCount

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try
    End Sub

    Private Sub frmConfiguracionList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Listar_Configuraciones()
    End Sub

    Private Sub mnuSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSalir.ItemClick
        Close()
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        Listar_Configuraciones()
    End Sub

    Private Sub Dgrid_DoubleClick(sender As Object, e As EventArgs) Handles Dgrid.DoubleClick

        Try

            Modo = pModo.Lista

            If (GridView1.RowCount > 0) Then
                Dim Dr As DataRowView = GridView1.GetFocusedRow
                Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle
                If Modo = pModo.Lista Then
                    Dim Config As New frmConfiguracionHorarios(frmConfiguracionHorarios.TipoTrans.Editar)
                    Config.OpcionesMenu = OpcionesMenu
                    'Config.mnuGuardar.Enabled = Config.OpcionesMenu.Modificar
                    'Config.mnuActualizar.Enabled = Config.OpcionesMenu.Modificar
                    ' Config.mnuEliminar.Enabled = Config.OpcionesMenu.Eliminar
                    Config.BeConfigEnc.Idnavconfigenc = Dr.Item("Correlativo")
                    Config.ShowDialog()
                    Config.Dispose()
                    Listar_Configuraciones()
                    GridView1.FocusedRowHandle = lSelectionIndex
                ElseIf Modo = pModo.Seleccion Then
                    Hide()
                End If
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuNuevo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuNuevo.ItemClick

        Try

            Dim Config As New frmConfiguracionHorarios(frmConfiguracionHorarios.TipoTrans.Nuevo)

            Config.ShowDialog()
            Config.Dispose()

            Listar_Configuraciones()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub
End Class