Imports DevExpress.XtraEditors

Public Class frmCamara

    Public pObjBEJ As New clsBeCamara

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Property Modo As TipoTrans

    Public Sub New(ByVal pModo As TipoTrans)
        InitializeComponent()
        Modo = pModo
    End Sub

    Private Sub frmCamara_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Try

            Select Case Modo
                Case TipoTrans.Nuevo

                    lblCodigo.Text = clsLnCamara.MaxID
                    User_agrTextEdit.Text = AP.UsuarioAp.IdUsuario
                    Fec_agrDateEdit.Text = Now
                    User_modTextEdit.Text = AP.UsuarioAp.IdUsuario
                    Fec_modDateEdit.Text = Now
                    mnuGuardar.Enabled = True
                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False

                Case TipoTrans.Editar

                    clsLnCamara.Obtener(pObjBEJ)

                    lblCodigo.Text = pObjBEJ.IdCamara
                    txtCodigo.Text = pObjBEJ.Codigo
                    txtNombre.Text = pObjBEJ.Nombre
                    txtModelo.Text = pObjBEJ.Modelo
                    txtSerie.Text = pObjBEJ.Serie
                    txtIp.Text = pObjBEJ.Ip
                    txtUbicacion.Value = pObjBEJ.IdUbicacion
                    chkActivo.Checked = pObjBEJ.Activo

                    User_agrTextEdit.Text = pObjBEJ.User_agr
                    Fec_agrDateEdit.Text = pObjBEJ.Fec_agr
                    User_modTextEdit.Text = pObjBEJ.User_mod
                    Fec_modDateEdit.Text = pObjBEJ.Fec_mod

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

            pObjBEJ = New clsBeCamara() With {.IdCamara = clsLnCamara.MaxID(),
                .Codigo = txtCodigo.Text.Trim(),
                .Nombre = txtNombre.Text.Trim(),
                .Modelo = txtModelo.Text.Trim(),
                .Serie = txtSerie.Text.Trim(),
                .Ip = txtIp.Text.Trim(),
                .IdUbicacion = txtUbicacion.Value,
                .Activo = True,
                .User_agr = AP.UsuarioAp.IdUsuario,
                .Fec_agr = Now,
                .User_mod = AP.UsuarioAp.IdUsuario,
                .Fec_mod = Now}

            Guardar = clsLnCamara.Insertar(pObjBEJ) > 0
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Function

    Private Function Actualizar() As Boolean
        Actualizar = False
        Try
            If Datos_Correctos() Then

                pObjBEJ.Codigo = txtCodigo.Text.Trim()
                pObjBEJ.Nombre = txtNombre.Text.Trim()
                pObjBEJ.Modelo = txtModelo.Text.Trim()
                pObjBEJ.Serie = txtSerie.Text.Trim()
                pObjBEJ.Ip = txtIp.Text.Trim()
                pObjBEJ.IdUbicacion = txtUbicacion.Value
                pObjBEJ.Activo = chkActivo.Checked
                pObjBEJ.User_mod = AP.UsuarioAp.IdUsuario
                pObjBEJ.Fec_mod = Now

                Actualizar = clsLnCamara.Actualizar(pObjBEJ) > 0
            End If
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick
        If Datos_Correctos() Then
            If MessageBox.Show("¿Guardar Cámara?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
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

            If MessageBox.Show("¿Desactivar la cámara?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                pObjBEJ.Activo = False
                If clsLnCamara.Actualizar(pObjBEJ) > 0 Then
                    XtraMessageBox.Show("Se ha desactivado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Close()
                    frmCamara_List.Dgrid.Refresh()
                End If
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtUbicacion_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtUbicacion.KeyPress
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