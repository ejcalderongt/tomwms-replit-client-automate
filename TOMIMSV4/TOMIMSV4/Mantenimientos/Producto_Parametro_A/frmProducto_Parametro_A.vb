Imports System.Reflection
Imports DevExpress.XtraEditors

Public Class frmProducto_Parametro_A

    Private pListObjT As New List(Of clsTabla)
    Public pObjPF As New clsBeProducto_parametro_a
    Public Delegate Sub listarProductoParametroA()
    Public Property InvokeListarProductoParametroA As listarProductoParametroA

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

    Private Sub frmProducto_Parametro_A_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            pListObjT = clsBD.GetLongitudByTabla("producto_parametro_a")

            'IMS.Listar_Propietarios_By_IdEmpresa(cmbPropietario, AP.IdEmpresa)

            Select Case Modo

                Case TipoTrans.Nuevo

                    lblIdentificadorFamilia.Text = clsLnProducto_parametro_a.MaxID() + 1
                    'User_agrTextEdit.Text = AP.UsuarioAp.Nombres + " " + AP.UsuarioAp.Apellidos
                    'Fec_agrDateEdit.Text = Now
                    'User_modTextEdit.Text = AP.UsuarioAp.Nombres + " " + AP.UsuarioAp.Apellidos
                    'Fec_modDateEdit.Text = Now

                    If OpcionesMenu IsNot Nothing Then
                        mnuGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False

                Case TipoTrans.Editar

                    lblIdentificadorFamilia.Text = pObjPF.IdProductoParametroA
                    txtCodigo.Text = pObjPF.Codigo
                    txtNombre.Text = pObjPF.Nombre
                    chkActivo.Checked = pObjPF.Activo
                    mnuGuardar.Enabled = False

                    If OpcionesMenu IsNot Nothing Then
                        mnuActualizar.Enabled = OpcionesMenu.Modificar
                        mnuEliminar.Enabled = OpcionesMenu.Eliminar
                    End If

            End Select

            txtCodigo.Select()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try


    End Sub

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick
        mnuGuardar.Enabled = False

        If Datos_Correctos() Then

            If MessageBox.Show("¿Guardar el parámetro?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                If Guardar() Then XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                If InvokeListarProductoParametroA IsNot Nothing Then
                    InvokeListarProductoParametroA.Invoke()
                End If

                Close()

            End If

        End If

        mnuGuardar.Enabled = True
    End Sub

    Private Function Datos_Correctos() As Boolean

        Datos_Correctos = False

        Try

            If String.IsNullOrEmpty(txtCodigo.Text.Trim()) Then
                XtraMessageBox.Show("Defina un código.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtCodigo.Focus()
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

    Private Function Guardar() As Boolean

        Guardar = False

        Try

            Dim ObjN As New clsBeProducto_parametro_a
            ObjN.IdProductoParametroA = clsLnProducto_parametro_a.MaxID() + 1
            ObjN.Codigo = txtCodigo.Text.Trim
            ObjN.Nombre = txtNombre.Text.Trim
            ObjN.Activo = True
            ObjN.User_agr = AP.UsuarioAp.IdUsuario
            ObjN.Fec_agr = Now
            ObjN.User_mod = AP.UsuarioAp.IdUsuario
            ObjN.Fec_mod = Now

            Guardar = clsLnProducto_parametro_a.Insertar(ObjN) > 0

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        mnuActualizar.Enabled = False
        If Actualizar() Then
            XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            If InvokeListarProductoParametroA IsNot Nothing Then
                InvokeListarProductoParametroA.Invoke()
            End If
            Close()
        End If
        mnuActualizar.Enabled = True
    End Sub

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            If Datos_Correctos() Then

                pObjPF.Codigo = txtCodigo.Text.Trim()
                pObjPF.Nombre = txtNombre.Text.Trim()
                pObjPF.User_mod = AP.UsuarioAp.IdUsuario
                pObjPF.Fec_mod = Now

                pObjPF.Activo = chkActivo.Checked

                Actualizar = clsLnProducto_parametro_a.Actualizar(pObjPF) > 0
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick
        Try

            mnuEliminar.Enabled = False
            Eliminar_Parametro()
            mnuEliminar.Enabled = True

        Catch ex As Exception
            mnuEliminar.Enabled = True
            If ex.HResult = -2146233088 Then TablasRelacionadas("producto_parametro_a", pObjPF.IdProductoParametroA)
        End Try
    End Sub

    Private Sub Eliminar_Parametro()

        Try

            If clsLnProducto_parametro_a.Existe_Parametro_By_IdParametroA(pObjPF.IdProductoParametroA) Then
                XtraMessageBox.Show("El parámetro ya esta asociado con un producto, no se puede eliminar.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else

                If XtraMessageBox.Show("¿Eliminar el producto?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    Dim vRegistrosEliminados As Integer = clsLnProducto_parametro_a.Eliminar(pObjPF)

                    If Not vRegistrosEliminados = 0 Then

                        XtraMessageBox.Show("Se eliminó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                        If Not InvokeListarProductoParametroA Is Nothing Then InvokeListarProductoParametroA.Invoke

                        Close()

                    End If

                End If

            End If

        Catch ex As Exception
            If ex.HResult = -2146233088 Then
                TablasRelacionadas("producto_parametro_a", pObjPF.IdProductoParametroA)
            Else
                XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
            End If
        End Try

    End Sub

End Class