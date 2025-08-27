Imports DevExpress.XtraEditors

Public Class frmTipoTranManufactura

    Public Delegate Sub Listar_Manufactura()
    Public Property InvokeListarTipoManufactura As listar_Manufactura
    Public BeTranManufacturaTipo As New clsBeTrans_manufactura_tipo

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Property Modo As TipoTrans
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    'Public Sub New(ByVal pModo As TipoTrans)
    '    'InitializeComponent()
    '    Modo = pModo
    'End Sub

    Private Sub frmTipoTranManufactura_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try


            Select Case Modo
                Case TipoTrans.Nuevo
                    BeTranManufacturaTipo = New clsBeTrans_manufactura_tipo
                    lblIdTransaccion.Text = clsLnTrans_manufactura_tipo.MaxID() + 1

                    If OpcionesMenu IsNot Nothing Then
                        mnuGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    mnuActualizar.Enabled = False
                    mnuDesactivar.Enabled = False

                Case TipoTrans.Editar

                    clsLnTrans_manufactura_tipo.Obtener(BeTranManufacturaTipo)
                    lblIdTransaccion.Text = BeTranManufacturaTipo.idtipomanufactura
                    txtDescripcion.Text = BeTranManufacturaTipo.Nombre
                    txtCodigo.Text = BeTranManufacturaTipo.Codigo
                    'chkActivo.Checked = BePais.Activo

                    mnuGuardar.Enabled = False

                    If OpcionesMenu IsNot Nothing Then
                        mnuActualizar.Enabled = OpcionesMenu.Modificar
                        mnuDesactivar.Enabled = OpcionesMenu.Eliminar
                    End If

            End Select

            Application.DoEvents()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick

        Try

            If Datos_Correctos() Then

                If MessageBox.Show("¿Guardar Tipo Transaccion de Manufactura?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    If Guardar() Then XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.DialogResult = DialogResult.OK
                    If InvokeListarTipoManufactura IsNot Nothing Then
                        InvokeListarTipoManufactura.Invoke()
                    End If
                    Close()
                End If

            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Function Datos_Correctos() As Boolean

        Datos_Correctos = False

        Try

            If String.IsNullOrEmpty(txtDescripcion.Text) Then
                XtraMessageBox.Show("Ingrese Descripción", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtDescripcion.Focus()
            ElseIf String.IsNullOrEmpty(txtCodigo.Text) Then
                XtraMessageBox.Show("Ingrese un código.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtCodigo.Focus()
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

            BeTranManufacturaTipo.idtipomanufactura = clsLnTrans_manufactura_tipo.MaxID() + 1
            BeTranManufacturaTipo.Nombre = txtDescripcion.Text
            BeTranManufacturaTipo.Codigo = txtCodigo.Text.Trim()
            BeTranManufacturaTipo.User_agr = AP.UsuarioAp.IdUsuario
            BeTranManufacturaTipo.Fec_agr = Now
            BeTranManufacturaTipo.User_mod = AP.UsuarioAp.IdUsuario
            BeTranManufacturaTipo.Fec_mod = Now

            Guardar = clsLnTrans_manufactura_tipo.Insertar(BeTranManufacturaTipo) > 0

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
            If InvokeListarTipoManufactura IsNot Nothing Then
                InvokeListarTipoManufactura.Invoke()
            End If
            Close()
        End If
    End Sub

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            If (Datos_Correctos()) Then

                BeTranManufacturaTipo.idtipomanufactura = CInt(lblIdTransaccion.Text)
                BeTranManufacturaTipo.Nombre = txtDescripcion.Text
                BeTranManufacturaTipo.Codigo = txtCodigo.Text.Trim()
                BeTranManufacturaTipo.User_mod = AP.UsuarioAp.IdUsuario
                BeTranManufacturaTipo.Fec_mod = Now

                Actualizar = clsLnTrans_manufactura_tipo.Actualizar(BeTranManufacturaTipo) > 0

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


    Private Sub frmTipoTranManufactura_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

End Class