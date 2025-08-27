Imports DevExpress.XtraEditors

Public Class frmAjusteMotivo

    Public gBeAjusteMotivo As New clsBeAjuste_motivo
    Public Delegate Sub ListarAjusteMotivo()
    Public Property InvokeListarAjusteMotivo As ListarAjusteMotivo

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Property Modo As TipoTrans
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Public Sub New(ByVal pModo As TipoTrans)
        InitializeComponent()
        Modo = pModo
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub frnAjusteMotivo_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            Select Case Modo

                Case TipoTrans.Nuevo

                    User_agrTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_agrDateEdit.Text = Now

                    User_modTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_modDateEdit.Text = Now

                    lblCod.Text = clsLnAjuste_motivo.MaxID()

                    If OpcionesMenu IsNot Nothing Then
                        mnuGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False

                Case TipoTrans.Editar

                    User_modTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_modDateEdit.Text = Now

                    mnuGuardar.Enabled = False

                    If OpcionesMenu IsNot Nothing Then
                        mnuActualizar.Enabled = OpcionesMenu.Modificar
                        mnuEliminar.Enabled = OpcionesMenu.Eliminar
                    End If

                    cargarDatos()

            End Select

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick

        If Guardar() Then
            XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            InvokeListarAjusteMotivo.Invoke
            Close()
        Else
            XtraMessageBox.Show("No se pudo guardar el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub

    Private Function Guardar() As Boolean

        Guardar = False

        Try

            gBeAjusteMotivo.Idmotivoajuste = lblCod.Text
            gBeAjusteMotivo.Nombre = txtNombre.Text.Trim
            gBeAjusteMotivo.Fec_agr = Fec_agrDateEdit.Text
            gBeAjusteMotivo.User_agr = User_agrTextEdit.Text
            gBeAjusteMotivo.Fec_mod = Fec_modDateEdit.Text
            gBeAjusteMotivo.User_mod = User_modTextEdit.Text
            gBeAjusteMotivo.Activo = chkActivo.Checked
            gBeAjusteMotivo.Sistema = chkSistema.Checked

            clsLnAjuste_motivo.Insertar(gBeAjusteMotivo)

            Guardar = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        If Actualizar() Then
            XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            InvokeListarAjusteMotivo.Invoke
            Close()
        Else
            XtraMessageBox.Show("No se pudo actualizar el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            gBeAjusteMotivo.Nombre = txtNombre.Text.Trim
            gBeAjusteMotivo.Fec_mod = Fec_modDateEdit.Text
            gBeAjusteMotivo.User_mod = User_modTextEdit.Text
            gBeAjusteMotivo.Activo = chkActivo.Checked
            gBeAjusteMotivo.Sistema = chkSistema.Checked

            clsLnAjuste_motivo.Actualizar(gBeAjusteMotivo)

            Actualizar = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        If XtraMessageBox.Show("¿Eliminar el registro?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

            gBeAjusteMotivo.Activo = False

            If clsLnAjuste_motivo.Actualizar(gBeAjusteMotivo) > 0 Then

                XtraMessageBox.Show("Se ha eliminado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                InvokeListarAjusteMotivo.Invoke
                Close()

            End If

        End If

    End Sub

    Private Sub cargarDatos()

        Try

            lblCod.Text = gBeAjusteMotivo.Idmotivoajuste
            txtNombre.Text = gBeAjusteMotivo.Nombre
            Fec_agrDateEdit.Text = gBeAjusteMotivo.Fec_agr
            User_agrTextEdit.Text = gBeAjusteMotivo.User_agr
            chkActivo.Checked = gBeAjusteMotivo.Activo
            chkSistema.Checked = gBeAjusteMotivo.Sistema

            ' Bloquea botones de actualizar y eliminar si es motivo de sistema
            mnuActualizar.Enabled = Not gBeAjusteMotivo.Sistema
            mnuEliminar.Enabled = Not gBeAjusteMotivo.Sistema

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub frmAjusteMotivo_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

End Class