Imports DevExpress.XtraEditors

Public Class frmMensajeRegla
    Public pObjBEJ As New clsBeMensaje_regla
    Public Delegate Sub ListarMensajes()
    Public Property InvokeListarMensajes As ListarMensajes

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

    Private Sub frmTarima_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try


            Select Case Modo

                Case TipoTrans.Nuevo
                    lblCodigo.Text = clsLnMensaje_regla.MaxID()
                    User_agrTextEdit.Text = AP.UsuarioAp.IdUsuario
                    Fec_agrDateEdit.Text = Now
                    User_modTextEdit.Text = AP.UsuarioAp.IdUsuario
                    Fec_modDateEdit.Text = Now

                    If OpcionesMenu IsNot Nothing Then
                        mnuGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False

                Case TipoTrans.Editar

                    lblCodigo.Text = pObjBEJ.IdMensajeRegla

                    txtNombre.Text = pObjBEJ.Nombre
                    chkActivo.Checked = pObjBEJ.Activo

                    User_agrTextEdit.Text = pObjBEJ.User_agr
                    Fec_agrDateEdit.Text = pObjBEJ.Fec_agr
                    User_modTextEdit.Text = pObjBEJ.User_mod
                    Fec_modDateEdit.Text = pObjBEJ.Fec_mod

                    mnuGuardar.Enabled = False

                    If OpcionesMenu IsNot Nothing Then
                        mnuActualizar.Enabled = OpcionesMenu.Modificar
                        mnuEliminar.Enabled = OpcionesMenu.Eliminar
                    End If

            End Select

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

        txtNombre.Focus()

    End Sub

    Private Function Datos_Correctos()

        Datos_Correctos = False

        Try
            If String.IsNullOrEmpty(txtNombre.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Nombre.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtNombre.Focus()
            Else
                Datos_Correctos = True
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Function Guardar() As Boolean

        Guardar = False

        Try

            pObjBEJ = New clsBeMensaje_regla() With {.IdMensajeRegla = clsLnMensaje_regla.MaxID(),
                .Nombre = txtNombre.Text.Trim(),
                .User_agr = AP.UsuarioAp.IdUsuario,
                .Fec_agr = Now,
                .User_mod = AP.UsuarioAp.IdUsuario,
                .Fec_mod = Now,
                .Activo = True}
            Guardar = clsLnMensaje_regla.Insertar(pObjBEJ) > 0

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            If Datos_Correctos() Then

                pObjBEJ.Nombre = txtNombre.Text.Trim()

                pObjBEJ.Activo = chkActivo.Checked

                pObjBEJ.User_mod = AP.UsuarioAp.IdUsuario
                pObjBEJ.Fec_mod = Now

                Actualizar = clsLnMensaje_regla.Actualizar(pObjBEJ) > 0

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick
        If Datos_Correctos() Then
            If XtraMessageBox.Show("¿Guardar Mensaje?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                If Guardar() Then
                    XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    InvokeListarMensajes.Invoke
                    Close()
                End If
            End If
        End If
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        If Actualizar() Then
            XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            InvokeListarMensajes.Invoke
            Close()
        End If
    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try

            If XtraMessageBox.Show("¿Desactivar el Mensaje?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                pObjBEJ.Activo = False
                If clsLnMensaje_regla.Actualizar(pObjBEJ) > 0 Then
                    XtraMessageBox.Show("Se ha desactivado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    InvokeListarMensajes.Invoke
                    Close()
                    frmTipoTarima_List.Dgrid.Refresh()
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

    Private Sub frmMensajeRegla_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub
End Class