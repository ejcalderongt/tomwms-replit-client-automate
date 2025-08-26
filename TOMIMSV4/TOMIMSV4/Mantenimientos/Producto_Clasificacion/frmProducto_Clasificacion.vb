Imports DevExpress.XtraEditors

Public Class frmProducto_Clasificacion

    Private pListObjT As New List(Of clsTabla)
    Public pBeProductoClasificacion As New clsBeProducto_clasificacion
    Public Delegate Sub listarProductoClasificacion()
    Public Property InvokeListarProductoClasificacion As listarProductoClasificacion

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

    Private Sub frmProducto_Clasificacion_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            pListObjT = clsBD.GetLongitudByTabla("producto_clasificacion")

            IMS.Listar_Propietarios_By_IdEmpresa(cmbPropietario, AP.IdEmpresa)

            Select Case Modo

                Case TipoTrans.Nuevo

                    lblIdClasificacion.Text = clsLnProducto_clasificacion.Max_IdProducto_Clasificacion
                    User_agrTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_agrDateEdit.Text = Now
                    User_modTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)

                    Fec_modDateEdit.Text = Now

                    If OpcionesMenu IsNot Nothing Then
                        mnuGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False
                    mnuAsignacion.Enabled = False
                    cmbPropietario.Enabled = True

                Case TipoTrans.Editar

                    lblIdClasificacion.Text = pBeProductoClasificacion.IdClasificacion
                    cmbPropietario.EditValue = pBeProductoClasificacion.Propietario.IdPropietario
                    cmbPropietario.Enabled = False

                    txtCodigo.Text = pBeProductoClasificacion.Codigo
                    txtNombre.Text = pBeProductoClasificacion.Nombre
                    chkSistema.Checked = pBeProductoClasificacion.Sistema
                    chkActivo.Checked = pBeProductoClasificacion.Activo

                    User_agrTextEdit.Text = pBeProductoClasificacion.User_agr
                    Fec_agrDateEdit.Text = pBeProductoClasificacion.Fec_agr
                    User_modTextEdit.Text = pBeProductoClasificacion.User_mod
                    Fec_modDateEdit.Text = pBeProductoClasificacion.Fec_mod

                    mnuGuardar.Enabled = False

                    mnuActualizar.Enabled = IIf(OpcionesMenu IsNot Nothing, OpcionesMenu.Modificar, True)
                    mnuEliminar.Enabled = IIf(OpcionesMenu IsNot Nothing, OpcionesMenu.Eliminar, True)
                    mnuAsignacion.Enabled = IIf(OpcionesMenu IsNot Nothing, OpcionesMenu.Modificar, True)

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

            Dim ObjN As New clsBeProducto_clasificacion
            ObjN.IdClasificacion = clsLnProducto_clasificacion.Max_IdProducto_Clasificacion
            ObjN.Propietario = New clsBePropietarios
            ObjN.Propietario.IdPropietario = cmbPropietario.EditValue
            ObjN.Codigo = txtCodigo.Text
            ObjN.Nombre = txtNombre.Text
            ObjN.Activo = True
            ObjN.Sistema = chkSistema.Checked
            ObjN.User_agr = AP.UsuarioAp.IdUsuario
            ObjN.User_mod = AP.UsuarioAp.IdUsuario
            ObjN.Fec_agr = Now
            ObjN.Fec_mod = Now

            Guardar = clsLnProducto_clasificacion.Insertar(ObjN) > 0

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            If Datos_Correctos() Then

                pBeProductoClasificacion.Codigo = txtCodigo.Text
                pBeProductoClasificacion.Nombre = txtNombre.Text
                pBeProductoClasificacion.Sistema = chkSistema.Checked
                pBeProductoClasificacion.User_mod = AP.UsuarioAp.IdUsuario
                pBeProductoClasificacion.Fec_mod = Now

                pBeProductoClasificacion.Activo = chkActivo.Checked

                Actualizar = clsLnProducto_clasificacion.Actualizar(pBeProductoClasificacion) > 0

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

            If MessageBox.Show("¿Guardar la Clasificación?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                If Guardar() Then XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                If InvokeListarProductoClasificacion IsNot Nothing Then
                    InvokeListarProductoClasificacion.Invoke()
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
            If InvokeListarProductoClasificacion IsNot Nothing Then
                InvokeListarProductoClasificacion.Invoke()
            End If
            Close()
        End If

        mnuActualizar.Enabled = True

    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try

            mnuEliminar.Enabled = False

            If pBeProductoClasificacion.Activo = False Then
                XtraMessageBox.Show("El registro ya se encuentra desactivado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf clsLnProducto_clasificacion.ExisteProductoLigado(pBeProductoClasificacion.IdClasificacion) Then
                If MessageBox.Show("¿Desactivar la Clasificación?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If Datos_Correctos() Then
                        clsLnProducto_clasificacion.Eliminar(pBeProductoClasificacion)
                        If InvokeListarProductoClasificacion IsNot Nothing Then
                            InvokeListarProductoClasificacion.Invoke()
                        End If
                        Close()
                        frmProducto_ClasificacionList.Dgrid.Refresh()
                    End If
                End If
            Else
                If MessageBox.Show("¿Eliminar la Clasificación?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    clsLnProducto_clasificacion.Delete(pBeProductoClasificacion.IdClasificacion)
                    If InvokeListarProductoClasificacion IsNot Nothing Then
                        InvokeListarProductoClasificacion.Invoke()
                    End If
                    Close()
                    frmProducto_ClasificacionList.Dgrid.Refresh()
                End If
            End If

            mnuEliminar.Enabled = True
        Catch ex As Exception

            mnuEliminar.Enabled = True
            If ex.HResult = -2146233088 Then TablasRelacionadas("producto_clasificacion", pBeProductoClasificacion.IdClasificacion)
        End Try

    End Sub

    Private Sub mnuAsignacion_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAsignacion.ItemClick
        XtraMessageBox.Show("En Mantenimiento", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub frmProducto_Clasificacion_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

End Class