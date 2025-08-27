Imports DevExpress.XtraEditors

Public Class frmClienteTipo
    Public gBeClienteTipo As New clsBeCliente_tipo
    Public Delegate Sub listarTipoCliente()
    Public Property InvokeListarTipoCliente As listarTipoCliente

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Property Modo As TipoTrans

    Public Sub New(ByVal pModo As TipoTrans)
        InitializeComponent()
        Modo = pModo
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub frmClienteTipo_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            IMS.Listar_Propietarios_By_IdEmpresa(cmbPropietario, AP.IdEmpresa)

            Select Case Modo
                Case TipoTrans.Nuevo

                    User_agrTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_agrDateEdit.Text = Now
                    User_modTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_modDateEdit.Text = Now
                    mnuGuardar.Enabled = True
                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False
                    mnuAsignacion.Enabled = False
                    cmbPropietario.Enabled = True

                Case TipoTrans.Editar

                    cmbPropietario.EditValue = gBeClienteTipo.IdPropietario
                    cmbPropietario.Enabled = False

                    txtNombre.Text = gBeClienteTipo.NombreTipoCliente.Trim()

                    User_agrTextEdit.Text = gBeClienteTipo.User_agr
                    Fec_agrDateEdit.Text = gBeClienteTipo.Fec_agr
                    User_modTextEdit.Text = gBeClienteTipo.User_mod
                    Fec_modDateEdit.Text = gBeClienteTipo.Fec_mod

                    mnuGuardar.Enabled = False
                    mnuActualizar.Enabled = True
                    mnuEliminar.Enabled = True
                    mnuAsignacion.Enabled = True

            End Select


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Function Guardar() As Boolean

        Try

            Dim ObjN As New clsBeCliente_tipo() With {.IdPropietario = cmbPropietario.EditValue,
                .IdTipoCliente = clsLnCliente_tipo.MaxIDClienteTipo(),
                .NombreTipoCliente = txtNombre.Text.Trim(),
                .Activo = True,
                .User_agr = AP.UsuarioAp _
                .IdUsuario,
                .Fec_agr = Now,
                .User_mod = AP.UsuarioAp _
                .IdUsuario, .Fec_mod = Now}

            clsLnCliente_tipo.Insertar(ObjN)

            Return True


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            If Datos_Correctos() Then

                gBeClienteTipo.NombreTipoCliente = txtNombre.Text.Trim()

                gBeClienteTipo.User_mod = AP.UsuarioAp.IdUsuario
                gBeClienteTipo.Fec_mod = Now

                Actualizar = clsLnCliente_tipo.Actualizar(gBeClienteTipo) > 0

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function


    Private Function Datos_Correctos() As Boolean

        Datos_Correctos = False

        Try

            If cmbPropietario.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Propietario.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbPropietario.Focus()
            ElseIf String.IsNullOrEmpty(txtNombre.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Nombre.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombre.Focus()
            Else
                Datos_Correctos = True
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick

        If Datos_Correctos() Then
            If MessageBox.Show("¿Guardar Cliente Tipo?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                If Guardar() Then
                    XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    InvokeListarTipoCliente.Invoke
                    Close()
                End If
            End If
        End If

    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        If Actualizar() Then
            XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            InvokeListarTipoCliente.Invoke
            Close()
        End If
    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try

            If gBeClienteTipo.Activo = False Then
                XtraMessageBox.Show("El registro ya se encuentra desactivado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                If MessageBox.Show("¿Desactivar Cliente Tipo?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If clsLnCliente_tipo.Eliminar(gBeClienteTipo) > 0 Then
                        InvokeListarTipoCliente.Invoke
                        Close()
                        frmCienteTipo_List.Dgrid.Refresh()
                    End If
                End If
            End If

        Catch ex As Exception
            If ex.HResult = -2146233088 Then
                TablasRelacionadas("Cliente_tipo", gBeClienteTipo.IdTipoCliente)
            Else
                XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End Try

    End Sub

    Private Sub mnuAsignacion_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAsignacion.ItemClick
        XtraMessageBox.Show("En Mantenimiento", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub frmClienteTipo_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

End Class