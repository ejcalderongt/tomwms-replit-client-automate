Imports DevExpress.XtraEditors

Public Class frmProducto_Tipo

    Private pListObjT As New List(Of clsTabla)
    Public pObjPT As New clsBeProducto_tipo
    Public Delegate Sub listarProductoTipo()
    Public Property InvokeListarProductoTipo As listarProductoTipo

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

    Private Sub frmProducto_Tipo_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            pListObjT = clsBD.GetLongitudByTabla("producto_tipo")

            IMS.Listar_Propietarios_By_IdEmpresa(cmbPropietario, AP.IdEmpresa)

            Select Case Modo

                Case TipoTrans.Nuevo

                    lblCodigo.Text = clsLnProducto_tipo.MAXIdTipoProducto()
                    User_agrTextEdit.Text = AP.UsuarioAp.Nombres + " " + AP.UsuarioAp.Apellidos
                    Fec_agrDateEdit.Text = Now
                    User_modTextEdit.Text = AP.UsuarioAp.Nombres + " " + AP.UsuarioAp.Apellidos
                    Fec_modDateEdit.Text = Now

                    If OpcionesMenu IsNot Nothing Then
                        mnuGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False
                    mnuAsignacion.Enabled = False
                    cmbPropietario.Enabled = True

                Case TipoTrans.Editar

                    lblCodigo.Text = pObjPT.IdTipoProducto
                    cmbPropietario.EditValue = pObjPT.IdPropietario
                    cmbPropietario.Enabled = False

                    txtCodigo.Text = pObjPT.Codigo
                    txtNombre.Text = pObjPT.NombreTipoProducto

                    User_agrTextEdit.Text = pObjPT.User_agr
                    Fec_agrDateEdit.Text = pObjPT.Fec_agr
                    User_modTextEdit.Text = pObjPT.User_mod
                    Fec_modDateEdit.Text = pObjPT.Fec_mod

                    mnuGuardar.Enabled = False

                    If OpcionesMenu IsNot Nothing Then
                        mnuActualizar.Enabled = OpcionesMenu.Modificar
                        mnuEliminar.Enabled = OpcionesMenu.Eliminar
                        mnuAsignacion.Enabled = OpcionesMenu.Modificar
                    End If

            End Select

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

        Me.Focus()
        txtNombre.Focus()

    End Sub

    Private Function Guardar() As Boolean

        Guardar = False

        Try

            Dim ObjN As New clsBeProducto_tipo
            ObjN.IdTipoProducto = clsLnProducto_tipo.MAXIdTipoProducto()
            ObjN.IdPropietario = cmbPropietario.EditValue
            ObjN.Codigo = txtCodigo.Text.Trim
            ObjN.NombreTipoProducto = txtNombre.Text
            ObjN.Activo = True
            ObjN.User_agr = AP.UsuarioAp.IdUsuario
            ObjN.Fec_agr = Now
            ObjN.User_mod = AP.UsuarioAp.IdUsuario
            ObjN.Fec_mod = Now

            clsLnProducto_tipo.Insertar(ObjN)

            Guardar = True


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            If Datos_Correctos() Then

                pObjPT.Codigo = txtCodigo.Text.Trim
                pObjPT.NombreTipoProducto = txtNombre.Text.Trim()
                pObjPT.User_mod = AP.UsuarioAp.IdUsuario
                pObjPT.Fec_mod = Now
                pObjPT.Activo = chkActivo.Checked
                clsLnProducto_tipo.Actualizar(pObjPT)
                Actualizar = True

            End If


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Datos_Correctos() As Boolean

        Datos_Correctos = False

        Try

            If cmbPropietario.EditValue = -1 Then
                XtraMessageBox.Show("Seleccione Propietario.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbPropietario.Focus()
            ElseIf String.IsNullOrEmpty(txtNombre.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Nombre.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombre.Focus()
            ElseIf txtNombre.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRETIPOPRODUCTO").Longitud Then
                DevExpress.XtraEditors.XtraMessageBox.Show("El Nombre debe de tener como máximo " & pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRETIPOPRODUCTO").Longitud & " carácteres.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombre.Focus()
            Else
                Datos_Correctos = True
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick

        mnuGuardar.Enabled = False

        If Datos_Correctos() Then

            If MessageBox.Show("¿Guardar el Tipo?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                If Guardar() Then

                    XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    If InvokeListarProductoTipo IsNot Nothing Then
                        InvokeListarProductoTipo.Invoke()
                    End If

                    Close()

                End If

            End If

        End If

        mnuGuardar.Enabled = True

    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        mnuActualizar.Enabled = False
        If Actualizar() Then
            XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            If InvokeListarProductoTipo IsNot Nothing Then
                InvokeListarProductoTipo.Invoke()
            End If
            Close()
        End If
        mnuActualizar.Enabled = True

    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try
            mnuEliminar.Enabled = False

            If pObjPT.Activo = False Then
                XtraMessageBox.Show("El registro ya se encuentra desactivado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf clsLnProducto_tipo.ExisteProductoLigado(pObjPT.IdTipoProducto) Then
                If MessageBox.Show("¿Desactivar el Tipo?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    clsLnProducto_tipo.Eliminar(pObjPT)
                    If InvokeListarProductoTipo IsNot Nothing Then
                        InvokeListarProductoTipo.Invoke()
                    End If
                    Close()
                    frmProducto_TipoList.Dgrid.Refresh()
                End If
            Else
                If MessageBox.Show("¿Eliminar el Tipo?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    clsLnProducto_tipo.Delete(pObjPT.IdTipoProducto)
                    If InvokeListarProductoTipo IsNot Nothing Then
                        InvokeListarProductoTipo.Invoke()
                    End If
                    Close()
                    frmProducto_TipoList.Dgrid.Refresh()
                End If
            End If

            mnuEliminar.Enabled = True

        Catch ex As Exception
            mnuEliminar.Enabled = True
            If ex.HResult = -2146233088 Then TablasRelacionadas("producto_tipo", pObjPT.IdTipoProducto)
        End Try

    End Sub

    Private Sub mnuAsignacion_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAsignacion.ItemClick
        XtraMessageBox.Show("En Mantenimiento", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub frmProducto_Tipo_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub
End Class