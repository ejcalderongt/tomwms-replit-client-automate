Imports DevExpress.XtraEditors

Public Class frmTipoCuadrilla

    Private pListObjT As New List(Of clsTabla)
    Public pObjBEJ As New clsBeCuadrilla_tipo
    Public Delegate Sub ListarTipocuadrilla()
    Public Property InvokeListarTipocuadrilla As ListarTipocuadrilla

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Property Modo As TipoTrans
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub frmTipoCuadrilla_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            pListObjT = clsBD.GetLongitudByTabla("cuadrilla_tipo")

            Select Case Modo

                Case TipoTrans.Nuevo

                    lblCodigo.Text = clsLnCuadrilla_tipo.MaxID()
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

                    clsLnCuadrilla_tipo.GetSingle(pObjBEJ)

                    lblCodigo.Text = pObjBEJ.IdTipoCuadrilla
                    txtNombre.Text = pObjBEJ.Nombre

                    User_agrTextEdit.Text = pObjBEJ.User_agr
                    Fec_agrDateEdit.Text = pObjBEJ.Fec_agr
                    User_modTextEdit.Text = pObjBEJ.User_mod
                    Fec_modDateEdit.Text = pObjBEJ.Fec_mod

                    mnuGuardar.Enabled = False
                    mnuActualizar.Enabled = OpcionesMenu.Modificar
                    mnuEliminar.Enabled = OpcionesMenu.Eliminar

            End Select

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

        Me.Focus()
        txtNombre.Focus()

    End Sub

    Private Function Datos_Correctos()

        Datos_Correctos = False

        Try

            If String.IsNullOrEmpty(txtNombre.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Nombre", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtNombre.Focus()
            ElseIf txtNombre.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud Then
                XtraMessageBox.Show("El Nombre debe de tener como máximo " & pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud & " carácteres.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

            pObjBEJ = New clsBeCuadrilla_tipo()

            pObjBEJ.IdTipoCuadrilla = clsLnCuadrilla_tipo.MaxID()
            pObjBEJ.Nombre = txtNombre.Text.Trim()

            pObjBEJ.Es_recepcion = chkRecibe.Checked
            pObjBEJ.Es_transito = chkTransporta.Checked
            pObjBEJ.Es_picking = chkPickea.Checked
            pObjBEJ.Es_verificacion = chkVerifica.Checked
            pObjBEJ.Es_ubicacion = chkUbica.Checked

            pObjBEJ.User_agr = AP.UsuarioAp.IdUsuario
            pObjBEJ.Fec_agr = Now
            pObjBEJ.User_mod = AP.UsuarioAp.IdUsuario
            pObjBEJ.Fec_mod = Now


            Guardar = clsLnCuadrilla_tipo.Insertar(pObjBEJ) > 0

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

                pObjBEJ.Es_recepcion = chkRecibe.Checked
                pObjBEJ.Es_transito = chkTransporta.Checked
                pObjBEJ.Es_picking = chkPickea.Checked
                pObjBEJ.Es_verificacion = chkVerifica.Checked
                pObjBEJ.Es_ubicacion = chkUbica.Checked

                pObjBEJ.User_mod = AP.UsuarioAp.IdUsuario
                pObjBEJ.Fec_mod = Now

                Actualizar = clsLnCuadrilla_tipo.Actualizar(pObjBEJ) > 0

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

        Try

            If Datos_Correctos() Then

                If XtraMessageBox.Show("¿Guardar Tipo Cuadrilla?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                    If Guardar() Then

                        XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                        If InvokeListarTipocuadrilla IsNot Nothing Then
                            InvokeListarTipocuadrilla.Invoke()
                        End If

                        Close()

                    End If

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

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        If Actualizar() Then
            XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            If InvokeListarTipocuadrilla IsNot Nothing Then
                InvokeListarTipocuadrilla.Invoke()
            End If
            Close()
        End If
    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try

            If XtraMessageBox.Show("¿Eliminar tipo de cuadrilla?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                If clsLnCuadrilla_tipo.Eliminar(pObjBEJ) > 0 Then

                    XtraMessageBox.Show("Se ha desactivado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    If InvokeListarTipocuadrilla IsNot Nothing Then
                        InvokeListarTipocuadrilla.Invoke()
                    End If

                    Close()

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

    Private Sub frmTipoCuadrilla_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

End Class