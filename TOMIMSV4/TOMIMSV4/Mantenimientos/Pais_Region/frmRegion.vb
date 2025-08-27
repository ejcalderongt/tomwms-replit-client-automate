Imports DevExpress.XtraEditors
Public Class frmRegion
    Private pListObjT As New List(Of clsTabla)
    Public pRegion As New clsBePais_region
    Public Delegate Sub Listar_Regiones()
    Public Property InvokeListarRegion As Listar_Regiones

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

    Private Sub frmRegión_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            pListObjT = clsBD.GetLongitudByTabla("pais_region")
            IMS.Listar_Paises(cmbPais)

            Select Case Modo

                Case TipoTrans.Nuevo

                    pRegion.IdRegion = clsLnPais_region.MaxID()

                    txtIdRegion.Value = pRegion.IdRegion
                    txtIdRegion.Enabled = False

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

                    clsLnPais_region.Obtener(pRegion)

                    cmbPais.EditValue = pRegion.IdPais

                    txtNomRegion.Text = pRegion.Nombre
                    txtIdRegion.Text = pRegion.IdRegion

                    'Bitácora
                    Dim UserBitacora As New clsBeUsuario() With {.IdUsuario = AP.UsuarioAp.IdUsuario}

                    clsLnUsuario.Obtener(UserBitacora)

                    'Bitácora
                    'Usuario agregó
                    User_agrTextEdit.Text = clsPublic.Desencriptar(UserBitacora.Codigo)
                    Fec_agrDateEdit.Text = pRegion.Fec_agr

                    'Usuario modificó
                    UserBitacora.IdUsuario = pRegion.User_mod

                    If UserBitacora.IdUsuario <> 0 Then
                        clsLnUsuario.Obtener(UserBitacora)
                        User_modTextEdit.Text = UserBitacora.Codigo
                        Fec_modDateEdit.Text = pRegion.Fec_mod
                    End If

                    'Fin Bitácora

                    mnuGuardar.Enabled = False

                    If OpcionesMenu IsNot Nothing Then
                        mnuActualizar.Enabled = OpcionesMenu.Modificar
                        mnuEliminar.Enabled = OpcionesMenu.Eliminar
                    End If

            End Select

            txtNomRegion.Focus()

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

            pRegion.Nombre = txtNomRegion.Text
            pRegion.IdRegion = txtIdRegion.Text
            pRegion.IdPais = cmbPais.EditValue

            pRegion.User_mod = AP.UsuarioAp.IdUsuario.ToString
            pRegion.User_agr = AP.UsuarioAp.IdUsuario.ToString

            Guardar = clsLnPais_region.Insertar(pRegion) > 0

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

                pRegion.Nombre = txtNomRegion.Text
                pRegion.User_mod = AP.UsuarioAp.IdUsuario.ToString
                pRegion.Fec_mod = Now
                pRegion.IdRegion = txtIdRegion.Text
                pRegion.IdPais = cmbPais.EditValue

                Actualizar = clsLnPais_region.Actualizar(pRegion) > 0

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

            If XtraMessageBox.Show("¿Guardar Región?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                If Guardar() Then XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                If InvokeListarRegion IsNot Nothing Then
                    InvokeListarRegion.Invoke()
                End If
                Close()

            End If

        End If

    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        If Actualizar() Then
            XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            If InvokeListarRegion IsNot Nothing Then
                InvokeListarRegion.Invoke()
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
            ElseIf String.IsNullOrEmpty(txtNomRegion.Text) Then
                XtraMessageBox.Show("Ingrese Nombre.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNomRegion.Focus()
            ElseIf txtNomRegion.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud Then
                XtraMessageBox.Show(String.Format("El Nombre debe de tener como máximo {0} carácteres.", pListObjT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "NOMBRE").Longitud),
                                    Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNomRegion.Focus()
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

        If XtraMessageBox.Show("¿Eliminar Región?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

            If clsLnPais_region.Eliminar(pRegion) > 0 Then
                XtraMessageBox.Show("Se ha eliminado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                InvokeListarRegion.Invoke
                Close()
            End If

        End If

    End Sub

    Private Sub frmRegion_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

End Class