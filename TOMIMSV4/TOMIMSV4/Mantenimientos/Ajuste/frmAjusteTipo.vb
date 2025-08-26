Imports DevExpress.XtraEditors

Public Class frmAjusteTipo

    Public gBeAjusteTipo As New clsBeAjuste_tipo
    Public Delegate Sub ListarTipos()
    Public Property InvokeListarTipos As ListarTipos

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

    Private Sub frmAjusteTipo_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            Select Case Modo

                Case TipoTrans.Nuevo

                    User_agrTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_agrDateEdit.Text = Now

                    User_modTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_modDateEdit.Text = Now

                    lblCod.Text = clsLnAjuste_tipo.MaxID()

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
            InvokeListarTipos.Invoke
            Close()
        Else
            XtraMessageBox.Show("No se pudo guardar el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub

    Private Function Guardar() As Boolean

        Guardar = False

        Try

            gBeAjusteTipo.Idtipoajuste = lblCod.Text
            gBeAjusteTipo.Nombre = txtNombre.Text.Trim
            gBeAjusteTipo.Modifica_lote = chkLote.Checked
            gBeAjusteTipo.Momdifica_vencimiento = chkVence.Checked
            gBeAjusteTipo.Modifica_cantidad = chkCant.Checked
            gBeAjusteTipo.Modifica_peso = chkPeso.Checked
            gBeAjusteTipo.Fec_agr = Fec_agrDateEdit.Text
            gBeAjusteTipo.User_agr = User_agrTextEdit.Text
            gBeAjusteTipo.Fec_mod = Fec_modDateEdit.Text
            gBeAjusteTipo.User_mod = User_modTextEdit.Text
            gBeAjusteTipo.Activo = chkActivo.Checked

            clsLnAjuste_tipo.Insertar(gBeAjusteTipo)

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

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        If Actualizar() Then
            XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            InvokeListarTipos.Invoke
            Close()
        Else
            XtraMessageBox.Show("No se pudo actualizar el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            gBeAjusteTipo.Nombre = txtNombre.Text.Trim
            gBeAjusteTipo.Modifica_lote = chkLote.Checked
            gBeAjusteTipo.Momdifica_vencimiento = chkVence.Checked
            gBeAjusteTipo.Modifica_cantidad = chkCant.Checked
            gBeAjusteTipo.Modifica_peso = chkPeso.Checked
            gBeAjusteTipo.Fec_mod = Fec_modDateEdit.Text
            gBeAjusteTipo.User_mod = User_modTextEdit.Text
            gBeAjusteTipo.Activo = chkActivo.Checked

            clsLnAjuste_tipo.Actualizar(gBeAjusteTipo)

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

            gBeAjusteTipo.Activo = False

            If clsLnAjuste_tipo.Actualizar(gBeAjusteTipo) > 0 Then

                XtraMessageBox.Show("Se ha eliminado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                InvokeListarTipos.Invoke
                Close()

            End If

        End If

    End Sub

    Private Sub cargarDatos()

        Try

            lblCod.Text = gBeAjusteTipo.Idtipoajuste
            txtNombre.Text = gBeAjusteTipo.Nombre
            chkLote.Checked = gBeAjusteTipo.Modifica_lote
            chkVence.Checked = gBeAjusteTipo.Momdifica_vencimiento
            chkCant.Checked = gBeAjusteTipo.Modifica_cantidad
            chkPeso.Checked = gBeAjusteTipo.Modifica_peso
            Fec_agrDateEdit.Text = gBeAjusteTipo.Fec_agr
            User_agrTextEdit.Text = gBeAjusteTipo.User_agr
            chkActivo.Checked = gBeAjusteTipo.Activo

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub frmAjusteTipo_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

End Class