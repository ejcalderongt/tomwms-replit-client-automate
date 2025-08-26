Imports DevExpress.XtraEditors

Public Class frmMunicipio

    Public Muni As New clsBePais_municipio

    Private Depto As New clsBePais_departamento
    Private dDepto As New clsLnPais_departamento
    Private pLIstObjT As New List(Of clsTabla)
    Public Delegate Sub Listar_Municipios()
    Public Property InvokeListarMunicipios As Listar_Municipios

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

    Private Sub frmMunicipio_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            pLIstObjT = clsBD.GetLongitudByTabla("pais_municipio")
            IMS.Listar_Paises(cmbPais)

            Try
                IMS.Listar_Departamentos(cmbDepartamento, cmbPais.EditValue)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

            Select Case Modo

                Case TipoTrans.Nuevo

                    Me.txtIdMunicipio.Value = Muni.IdMunicipio
                    Me.txtIdMunicipio.Enabled = False

                    User_agrTextEdit.Text = AP.UsuarioAp.Codigo.ToString
                    Fec_agrDateEdit.Text = Now.ToShortDateString

                    User_modTextEdit.Text = AP.UsuarioAp.Codigo.ToString
                    Fec_modDateEdit.Text = Now.ToShortDateString

                    Try

                        Muni.IdMunicipio = clsLnPais_municipio.MaxID()
                        txtIdMunicipio.Text = Muni.IdMunicipio

                    Catch ex As Exception
                        Throw New Exception(ex.Message)
                    End Try

                    If OpcionesMenu IsNot Nothing Then
                        mnuGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False

                Case TipoTrans.Editar

                    clsLnPais_municipio.Obtener(Muni)

                    txtNomMunicipio.Text = Muni.Nombre
                    txtIdMunicipio.Text = Muni.IdMunicipio

                    cmbPais.EditValue = clsLnPais_departamento.GetIdPaisByIdDepartamento(Muni.IdDepartamento)
                    cmbDepartamento.EditValue = Muni.IdDepartamento

                    'Bitácora
                    Dim UserBitacora As New clsBeUsuario() With {.IdUsuario = Muni.User_agr}
                    clsLnUsuario.Obtener(UserBitacora)

                    'Bitácora
                    'Usuario agregó
                    User_agrTextEdit.Text = UserBitacora.Codigo
                    Fec_agrDateEdit.Text = Depto.Fec_agr

                    'Usuario modificó
                    UserBitacora.IdUsuario = Muni.User_mod
                    If UserBitacora.IdUsuario <> 0 Then
                        clsLnUsuario.Obtener(UserBitacora)
                        User_modTextEdit.Text = UserBitacora.Codigo
                        Fec_modDateEdit.Text = Depto.Fec_mod.ToShortDateString
                    End If
                    'Fin Bitácora

                    mnuGuardar.Enabled = False

                    If OpcionesMenu IsNot Nothing Then
                        mnuActualizar.Enabled = OpcionesMenu.Modificar
                        mnuEliminar.Enabled = OpcionesMenu.Eliminar
                    End If

            End Select

            txtNomMunicipio.Focus()

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

            Muni.IdMunicipio = clsLnPais_municipio.MaxID()
            Muni.Nombre = txtNomMunicipio.Text
            Muni.IdMunicipio = Me.txtIdMunicipio.Text
            Muni.IdDepartamento = cmbDepartamento.EditValue

            Muni.User_mod = AP.UsuarioAp.IdUsuario.ToString
            Muni.User_agr = AP.UsuarioAp.IdUsuario.ToString

            Guardar = clsLnPais_municipio.Insertar(Muni) > 0

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

                Muni.Nombre = txtNomMunicipio.Text
                Muni.IdDepartamento = cmbDepartamento.EditValue

                Muni.User_mod = AP.UsuarioAp.IdUsuario.ToString
                Muni.Fec_mod = Now
                Muni.IdMunicipio = txtIdMunicipio.Text

                Actualizar = clsLnPais_municipio.Actualizar(Muni) > 0

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

            If XtraMessageBox.Show("¿Guardar Municipio?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                If Guardar() Then XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                If InvokeListarMunicipios IsNot Nothing Then
                    InvokeListarMunicipios.Invoke()
                End If
                Close()

            End If

        End If

    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        If Actualizar() Then
            XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            If InvokeListarMunicipios IsNot Nothing Then
                InvokeListarMunicipios.Invoke()
            End If
            Close()
        End If

    End Sub

    Private Function Datos_Correctos() As Boolean

        Datos_Correctos = False

        Try

            If cmbPais.Text = "" OrElse cmbPais.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione el País.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbPais.Focus()
            ElseIf cmbDepartamento.Text = "" OrElse cmbDepartamento.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione el Departamento.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbDepartamento.Focus()
            ElseIf String.IsNullOrEmpty(txtNomMunicipio.Text) Then
                XtraMessageBox.Show("Ingrese Nombre.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNomMunicipio.Focus()
            ElseIf txtNomMunicipio.Text.Count > pLIstObjT.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud Then
                XtraMessageBox.Show(String.Format("El Nombre debe de tener como máximo {0} carácteres.", pLIstObjT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "NOMBRE").Longitud),
                                    Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtNomMunicipio.Focus()
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

        If XtraMessageBox.Show("¿Eliminar el municipio?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

            If clsLnPais_municipio.Eliminar(Muni) > 0 Then
                XtraMessageBox.Show("Se ha eliminado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                If InvokeListarMunicipios IsNot Nothing Then
                    InvokeListarMunicipios.Invoke()
                End If
                Close()
            End If

        End If

    End Sub

    Private Sub cmbDepartamento_SelectedIndexChanged(sender As Object, e As EventArgs)

        Try

            If Modo = TipoTrans.Nuevo Then
                Muni.IdDepartamento = cmbDepartamento.EditValue
                Muni.IdMunicipio = clsLnPais_municipio.MaxID()
                txtIdMunicipio.Text = Muni.IdMunicipio
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

    Private Sub cmbPais_SelectedIndexChanged(sender As Object, e As EventArgs)

        If cmbPais.ItemIndex > 0 AndAlso cmbPais.EditValue <> 0 Then
            IMS.Listar_Departamentos(cmbDepartamento, cmbPais.EditValue)
        End If

    End Sub

    Private Sub frmMunicipio_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub
End Class