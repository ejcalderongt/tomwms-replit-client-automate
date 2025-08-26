Imports DevExpress.XtraEditors

Public Class frmRegla_Recepcion
    Public pObjRegla As New clsBeReglas_recepcion
    Public Delegate Sub listar_Reglas_Recepcion()
    Public Property InvokeListarReglasRecepcion As listar_Reglas_Recepcion

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

    Private Sub frmRegla_Recepcion_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            Select Case Modo

                Case TipoTrans.Nuevo

                    lblCodigo.Text = clsLnReglas_recepcion.MaxID
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

                    clsLnReglas_recepcion.Obtener(pObjRegla)
                    lblCodigo.Text = pObjRegla.IdReglaRecepcion
                    txtCodigo.Text = pObjRegla.Codigo
                    txtNombre.Text = pObjRegla.Nombre
                    txtDescripcion.Text = pObjRegla.Descripcion
                    chkRechazar.Checked = pObjRegla.Rechazar
                    chkStockND.Checked = pObjRegla.StockNoDisponible
                    chkActivo.Checked = pObjRegla.Activo
                    User_agrTextEdit.Text = pObjRegla.User_agr
                    Fec_agrDateEdit.Text = pObjRegla.Fec_agr
                    User_modTextEdit.Text = pObjRegla.User_mod
                    Fec_modDateEdit.Text = pObjRegla.Fec_mod

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

        txtCodigo.Focus()

    End Sub

    Private Function Datos_Correctos()
        Datos_Correctos = False
        Try
            If String.IsNullOrEmpty(txtCodigo.Text.Trim) Then
                XtraMessageBox.Show("Ingrese Codigo.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtCodigo.Focus()
            ElseIf String.IsNullOrEmpty(txtNombre.Text.Trim) Then
                XtraMessageBox.Show("Ingrese Nombre.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombre.Focus()
            ElseIf String.IsNullOrEmpty(txtDescripcion.Text.Trim) Then
                XtraMessageBox.Show("Ingrese Descripción.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtDescripcion.Focus()
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

            pObjRegla = New clsBeReglas_recepcion()

            pObjRegla.IdReglaRecepcion = clsLnReglas_recepcion.MaxID()
            pObjRegla.Codigo = txtCodigo.Text
            pObjRegla.Nombre = txtNombre.Text
            pObjRegla.Descripcion = txtDescripcion.Text
            pObjRegla.Rechazar = chkRechazar.Checked
            pObjRegla.StockNoDisponible = chkStockND.Checked
            pObjRegla.User_agr = AP.UsuarioAp.IdUsuario
            pObjRegla.Fec_agr = Now
            pObjRegla.User_mod = AP.UsuarioAp.IdUsuario
            pObjRegla.Fec_mod = Now
            pObjRegla.Activo = True

            Guardar = clsLnReglas_recepcion.Insertar(pObjRegla) > 0

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

                pObjRegla.Codigo = txtCodigo.Text
                pObjRegla.Nombre = txtNombre.Text
                pObjRegla.Descripcion = txtDescripcion.Text
                pObjRegla.Rechazar = chkRechazar.Checked
                pObjRegla.StockNoDisponible = chkStockND.Checked
                pObjRegla.User_mod = AP.UsuarioAp.IdUsuario
                pObjRegla.Fec_mod = Now
                pObjRegla.Activo = chkActivo.Checked
                Actualizar = clsLnReglas_recepcion.Actualizar(pObjRegla) > 0

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
            If XtraMessageBox.Show("¿Guardar la regla recepción?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                If Guardar() Then
                    XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    InvokeListarReglasRecepcion.Invoke
                    Close()
                End If
            End If
        End If
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        If Actualizar() Then
            XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            InvokeListarReglasRecepcion.Invoke
            Close()
        End If
    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick
        Try

            If XtraMessageBox.Show("¿Desactivar el registro?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                pObjRegla.Activo = False
                If clsLnReglas_recepcion.Actualizar(pObjRegla) > 0 Then
                    XtraMessageBox.Show("Se ha desactivado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    InvokeListarReglasRecepcion.Invoke
                    Close()
                    frmReglas_recepcion_List.Dgrid.Refresh()
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

    Private Sub frmRegla_Recepcion_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

End Class