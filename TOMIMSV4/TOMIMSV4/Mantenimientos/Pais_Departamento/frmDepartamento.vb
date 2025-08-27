Imports DevExpress.XtraEditors

Public Class frmDepartamento

    Private pListObjT As New List(Of clsTabla)
    Public Depto As New clsBePais_departamento
    Public Delegate Sub Listar_Departamentos()
    Public Property InvokeListarDepartamentos As Listar_Departamentos

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

    Private Sub frmDepartamento_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            pListObjT = clsBD.GetLongitudByTabla("pais_departamento")

            IMS.Listar_Paises(cmbPais)

            Select Case Modo

                Case TipoTrans.Nuevo

                    Depto.IdDepartamento = clsLnPais_departamento.MaxID()

                    txtIdDepartamento.Value = Depto.IdDepartamento
                    txtIdDepartamento.Enabled = False

                    User_agrTextEdit.Text = AP.UsuarioAp.Codigo.ToString
                    Fec_agrDateEdit.Text = Now.ToShortDateString

                    User_modTextEdit.Text = AP.UsuarioAp.Codigo.ToString
                    Fec_modDateEdit.Text = Now.ToShortDateString

                    If OpcionesMenu IsNot Nothing Then
                        mnuGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False

                Case TipoTrans.Editar

                    clsLnPais_departamento.Obtener(Depto)

                    cmbPais.EditValue = Depto.IdPais

                    txtNomDepto.Text = Depto.Nombre
                    txtIdDepartamento.Text = Depto.IdDepartamento

                    'Bitácora
                    Dim UserBitacora As New clsBeUsuario() With {.IdUsuario = Depto.User_agr}
                    clsLnUsuario.Obtener(UserBitacora)

                    'Usuario agregó
                    User_agrTextEdit.Text = clsPublic.Desencriptar(UserBitacora.Codigo)
                    Fec_agrDateEdit.Text = Depto.Fec_agr

                    'Usuario modificó
                    UserBitacora = New clsBeUsuario() With {.IdUsuario = Depto.User_agr}
                    clsLnUsuario.Obtener(UserBitacora)
                    User_modTextEdit.Text = clsPublic.Desencriptar(UserBitacora.Codigo)
                    Fec_modDateEdit.Text = Depto.Fec_mod.ToShortDateString
                    'Fin Bitácora

                    mnuGuardar.Enabled = False

                    If OpcionesMenu IsNot Nothing Then
                        mnuActualizar.Enabled = OpcionesMenu.Modificar
                        mnuEliminar.Enabled = OpcionesMenu.Eliminar
                    End If

            End Select

            txtNomDepto.Focus()

            Application.DoEvents()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Function Guardar() As Boolean

        Guardar = False

        Try

            Depto.IdPais = cmbPais.EditValue
            Depto.IdDepartamento = clsLnPais_departamento.MaxID
            Depto.Nombre = txtNomDepto.Text
            Depto.IdDepartamento = txtIdDepartamento.Text

            Depto.User_mod = AP.UsuarioAp.IdUsuario.ToString
            Depto.User_agr = AP.UsuarioAp.IdUsuario.ToString

            Guardar = clsLnPais_departamento.Insertar(Depto) > 0

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

                Depto.IdPais = cmbPais.EditValue
                Depto.Nombre = txtNomDepto.Text
                Depto.User_mod = AP.UsuarioAp.IdUsuario.ToString
                Depto.Fec_mod = Now
                Depto.IdDepartamento = txtIdDepartamento.Text

                Actualizar = clsLnPais_departamento.Actualizar(Depto) > 0

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

            If XtraMessageBox.Show("¿Guardar departamento?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                If Guardar() Then XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                If InvokeListarDepartamentos IsNot Nothing Then
                    InvokeListarDepartamentos.Invoke()
                End If
                Close()

            End If

        End If

    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        If Actualizar() Then
            XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            If InvokeListarDepartamentos IsNot Nothing Then
                InvokeListarDepartamentos.Invoke()
            End If
            Close()
        End If

    End Sub

    Private Function Datos_Correctos() As Boolean

        Datos_Correctos = False

        Try

            If cmbPais.Text = "" OrElse cmbPais.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione el País", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbPais.Focus()
            ElseIf String.IsNullOrEmpty(txtNomDepto.Text) Then
                XtraMessageBox.Show("Ingrese Nombre", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNomDepto.Focus()
            ElseIf txtNomDepto.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud Then
                XtraMessageBox.Show(String.Format("El Nombre debe de tener como máximo {0} carácteres.", pListObjT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "NOMBRE").Longitud),
                                    Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNomDepto.Focus()
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

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        If XtraMessageBox.Show("¿Eliminar el departamento?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

            If clsLnPais_departamento.Eliminar(Depto) > 0 Then
                XtraMessageBox.Show("Se ha eliminado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                If InvokeListarDepartamentos IsNot Nothing Then
                    InvokeListarDepartamentos.Invoke()
                End If
                Close()
            End If

        End If

    End Sub

    Private Sub frmDepartamento_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

End Class