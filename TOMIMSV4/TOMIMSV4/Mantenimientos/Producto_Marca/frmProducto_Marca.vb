Imports DevExpress.XtraEditors

Public Class frmProducto_Marca

    Public pObjMarca As New clsBeProducto_marca
    Public Delegate Sub listarProductoMarca()
    Public Property InvokeListarProductoMarca As listarProductoMarca

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

    Private Sub frmProducto_Marca_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            IMS.Listar_Propietarios_By_IdEmpresa(cmbPropietario, AP.IdEmpresa)

            Select Case Modo

                Case TipoTrans.Nuevo

                    lblCod.Text = clsLnProducto_marca.Max_IdMarca()
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

                    lblCod.Text = pObjMarca.IdMarca
                    cmbPropietario.EditValue = pObjMarca.IdPropietario
                    cmbPropietario.Enabled = False

                    txtNombre.Text = pObjMarca.Nombre
                    chkActivo.Checked = pObjMarca.Activo

                    User_agrTextEdit.Text = pObjMarca.User_agr
                    Fec_agrDateEdit.Text = pObjMarca.Fec_agr
                    User_modTextEdit.Text = pObjMarca.User_mod
                    Fec_modDateEdit.Text = pObjMarca.Fec_mod

                    mnuGuardar.Enabled = False
                    mnuActualizar.Enabled = OpcionesMenu.Modificar
                    mnuEliminar.Enabled = OpcionesMenu.Eliminar
                    mnuAsignacion.Enabled = OpcionesMenu.Modificar

            End Select

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Function Guardar() As Boolean

        Guardar = False

        Try

            Dim ObjN As New clsBeProducto_marca
            ObjN.IdMarca = clsLnProducto_marca.Max_IdMarca()
            ObjN.IdPropietario = cmbPropietario.EditValue
            ObjN.Nombre = txtNombre.Text.Trim()
            ObjN.Activo = True

            ObjN.User_agr = AP.UsuarioAp.IdUsuario
            ObjN.Fec_agr = Now
            ObjN.User_mod = AP.UsuarioAp.IdUsuario
            ObjN.Fec_mod = Now

            Guardar = clsLnProducto_marca.Insertar(ObjN) > 0

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            If Datos_Correctos() Then

                pObjMarca.Nombre = txtNombre.Text.Trim()
                pObjMarca.User_mod = AP.UsuarioAp.IdUsuario
                pObjMarca.Fec_mod = Now

                pObjMarca.Activo = chkActivo.Checked

                Actualizar = clsLnProducto_marca.Actualizar(pObjMarca) > 0

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

        mnuGuardar.Enabled = False
        If Datos_Correctos() Then

            If MessageBox.Show("¿Guardar la Marca?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                If Guardar() Then
                    XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    If InvokeListarProductoMarca IsNot Nothing Then
                        InvokeListarProductoMarca.Invoke()
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
            If InvokeListarProductoMarca IsNot Nothing Then
                InvokeListarProductoMarca.Invoke()
            End If
            Close()
        End If
        mnuActualizar.Enabled = True

    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try

            mnuEliminar.Enabled = False

            If pObjMarca.Activo = False Then
                XtraMessageBox.Show("El registro ya se encuentra desactivado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf clsLnProducto_marca.ExisteProductoLigado(pObjMarca.IdMarca) Then
                If MessageBox.Show("¿Desactivar la Marca?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    clsLnProducto_marca.Eliminar(pObjMarca)
                    If InvokeListarProductoMarca IsNot Nothing Then
                        InvokeListarProductoMarca.Invoke()
                    End If
                    Close()
                    frmProducto_MarcaList.Dgrid.Refresh()

                End If
            Else
                If MessageBox.Show("¿Eliminar la Marca?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    clsLnProducto_marca.Delete(pObjMarca.IdMarca)
                    If InvokeListarProductoMarca IsNot Nothing Then
                        InvokeListarProductoMarca.Invoke()
                    End If
                    Close()
                    frmProducto_MarcaList.Dgrid.Refresh()
                End If
            End If

            mnuEliminar.Enabled = True

        Catch ex As Exception
            mnuEliminar.Enabled = True
            If ex.HResult = -2146233088 Then TablasRelacionadas("producto_marca", pObjMarca.IdMarca)
        End Try

    End Sub

    Private Sub mnuAsignacion_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAsignacion.ItemClick
        XtraMessageBox.Show("En Mantenimiento", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub frmProducto_Marca_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

End Class