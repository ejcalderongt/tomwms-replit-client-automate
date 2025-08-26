Imports DevExpress.XtraEditors

Public Class frmServicioC

    Public pObjBEJ As New clsBeI_nav_servicio

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Property Modo As TipoTrans

    Public Sub New(ByVal pModo As TipoTrans)
        InitializeComponent()
        Modo = pModo
    End Sub

    Private Sub frmServicio_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Try

            Select Case Modo
                Case TipoTrans.Nuevo

                    lblCodigo.Text = clsLnI_nav_servicio.MaxID()
                    User_agrTextEdit.Text = AP.UsuarioAp.IdUsuario
                    Fec_agrDateEdit.Text = Now
                    User_modTextEdit.Text = AP.UsuarioAp.IdUsuario
                    Fec_modDateEdit.Text = Now
                    mnuGuardar.Enabled = True
                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False

                Case TipoTrans.Editar

                    clsLnI_nav_servicio.GetSingle(pObjBEJ)

                    lblCodigo.Text = pObjBEJ.IdServicio
                    txtCodigo.Text = pObjBEJ.Codigo_servicio
                    txtNombre.Text = pObjBEJ.Descripcion
                    txtModelo.Text = pObjBEJ.Nemonico
                    chkActivo.Checked = pObjBEJ.Activo

                    mnuGuardar.Enabled = False
                    mnuActualizar.Enabled = True
                    mnuEliminar.Enabled = True

            End Select
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

        txtCodigo.Focus()

    End Sub

    Private Function Datos_Correctos()
        Datos_Correctos = False
        Try
            If String.IsNullOrEmpty(txtNombre.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Nombre.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

            pObjBEJ = New clsBeI_nav_servicio() With {.IdServicio = clsLnI_nav_servicio.MaxID(),
                .Codigo_servicio = txtCodigo.Text.Trim(),
                .Descripcion = txtNombre.Text.Trim(),
                .Nemonico = txtModelo.Text.Trim(),
                .Activo = True}

            Guardar = clsLnI_nav_servicio.Insertar(pObjBEJ) > 0
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Function

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            If Datos_Correctos() Then

                pObjBEJ.Codigo_servicio = txtCodigo.Text.Trim()
                pObjBEJ.Descripcion = txtNombre.Text.Trim()
                pObjBEJ.Nemonico = txtModelo.Text.Trim()
                pObjBEJ.Activo = chkActivo.Checked
                Actualizar = clsLnI_nav_servicio.Actualizar(pObjBEJ) > 0

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick
        If Datos_Correctos() Then
            If MessageBox.Show("¿Guardar Servicio?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                If Guardar() Then
                    XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Close()
                End If
            End If
        End If
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        If Actualizar() Then
            XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Close()
        End If
    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try

            If MessageBox.Show("¿Desactivar Servicio?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                pObjBEJ.Activo = False
                If clsLnI_nav_servicio.Actualizar(pObjBEJ) > 0 Then
                    XtraMessageBox.Show("Se ha desactivado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Close()
                    frmServicio_List.Dgrid.Refresh()
                End If
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtUbicacion_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Not (Char.IsControl(e.KeyChar) And e.KeyChar <> ".") Then
            e.Handled = True
        End If
        If e.KeyChar = "." Then
            e.Handled = True
        End If
        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        End If
    End Sub

End Class