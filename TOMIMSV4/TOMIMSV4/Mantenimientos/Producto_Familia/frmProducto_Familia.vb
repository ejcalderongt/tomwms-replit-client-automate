Imports DevExpress.XtraEditors

Public Class frmProducto_Familia


    Private pListObjT As New List(Of clsTabla)
    Public pObjPF As New clsBeProducto_familia
    Public Delegate Sub listarProductoFamilia()
    Public Property InvokeListarProductoFamilia As listarProductoFamilia

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

    Private Sub frmProducto_Familia_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            pListObjT = clsBD.GetLongitudByTabla("producto_familia")

            IMS.Listar_Propietarios_By_IdEmpresa(cmbPropietario, AP.IdEmpresa)

            Select Case Modo

                Case TipoTrans.Nuevo

                    lblIdentificadorFamilia.Text = clsLnProducto_familia.Max_IdProducto_Familia
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

                    lblIdentificadorFamilia.Text = pObjPF.IdFamilia
                    cmbPropietario.EditValue = pObjPF.Propietario.IdPropietario
                    cmbPropietario.Enabled = False

                    txtCodigo.Text = pObjPF.Codigo
                    txtNombre.Text = pObjPF.Nombre
                    chkActivo.Checked = pObjPF.Activo

                    User_agrTextEdit.Text = pObjPF.User_agr
                    Fec_agrDateEdit.Text = pObjPF.Fec_agr
                    User_modTextEdit.Text = pObjPF.User_mod
                    Fec_modDateEdit.Text = pObjPF.Fec_mod

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

            Dim ObjN As New clsBeProducto_familia
            ObjN.Propietario = New clsBePropietarios
            ObjN.IdFamilia = clsLnProducto_familia.Max_IdProducto_Familia()
            ObjN.Propietario.IdPropietario = cmbPropietario.EditValue
            ObjN.Codigo = txtCodigo.Text.Trim
            ObjN.Nombre = txtNombre.Text.Trim
            ObjN.Activo = True
            ObjN.User_agr = AP.UsuarioAp.IdUsuario
            ObjN.Fec_agr = Now
            ObjN.User_mod = AP.UsuarioAp.IdUsuario
            ObjN.Fec_mod = Now

            Guardar = clsLnProducto_familia.Insertar(ObjN) > 0

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            If Datos_Correctos() Then

                pObjPF.Codigo = txtCodigo.Text.Trim()
                pObjPF.Nombre = txtNombre.Text.Trim()
                pObjPF.User_mod = AP.UsuarioAp.IdUsuario
                pObjPF.Fec_mod = Now

                pObjPF.Activo = chkActivo.Checked

                Actualizar = clsLnProducto_familia.Actualizar(pObjPF) > 0
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
            ElseIf txtNombre.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud Then
                DevExpress.XtraEditors.XtraMessageBox.Show("El Nombre debe de tener como máximo " & pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud & " carácteres.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

            If MessageBox.Show("¿Guardar la Familia?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                If Guardar() Then XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                If InvokeListarProductoFamilia IsNot Nothing Then
                    InvokeListarProductoFamilia.Invoke()
                End If

                Close()

            End If

        End If

        mnuGuardar.Enabled = True

    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        mnuActualizar.Enabled = False
        If Actualizar() Then
            XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            If InvokeListarProductoFamilia IsNot Nothing Then
                InvokeListarProductoFamilia.Invoke()
            End If
            Close()
        End If
        mnuActualizar.Enabled = True

    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try

            mnuEliminar.Enabled = False

            If pObjPF.Activo = False Then
                XtraMessageBox.Show("El registro ya se encuentra desactivado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf clsLnProducto_familia.ExisteProductoLigado(pObjPF.IdFamilia) Then
                If MessageBox.Show("¿Desactivar la Familia?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    clsLnProducto_familia.Eliminar(pObjPF)
                    If InvokeListarProductoFamilia IsNot Nothing Then
                        InvokeListarProductoFamilia.Invoke()
                    End If
                    Close()
                    frmProducto_FamiliaList.Dgrid.Refresh()
                End If
            Else
                If MessageBox.Show("¿Eliminar la Familia?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    clsLnProducto_familia.Delete(pObjPF.IdFamilia)
                    If InvokeListarProductoFamilia IsNot Nothing Then
                        InvokeListarProductoFamilia.Invoke()
                    End If
                    Close()
                    frmProducto_FamiliaList.Dgrid.Refresh()
                End If
            End If

            mnuEliminar.Enabled = True

        Catch ex As Exception
            mnuEliminar.Enabled = True
            If ex.HResult = -2146233088 Then TablasRelacionadas("producto_familia", pObjPF.IdFamilia)
        End Try

    End Sub

    Private Sub mnuAsignacion_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAsignacion.ItemClick
        XtraMessageBox.Show("En Mantenimiento", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub frmProducto_Familia_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

End Class