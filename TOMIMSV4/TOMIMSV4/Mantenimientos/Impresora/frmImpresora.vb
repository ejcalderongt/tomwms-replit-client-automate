Imports DevExpress.XtraEditors

Public Class frmImpresora

    Public gBeImpresora As New clsBeImpresora

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

    Private Sub frmCamara_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        txtNombre.Focus()

        Try

            If Not IMS.Listar_Empresas(cmbEmpresa) Then
                XtraMessageBox.Show("No hay empresas definidas para la aplicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            IMS.Listar_Bodegas_Por_Empresa(cmdBodega, cmbEmpresa.EditValue)

            Listar_Impresora_Marca(cmbMarca)
            Listar_Impresora_Lenguaje(cmbLenguaje)
            Listar_Impresora_Tipo_Conexion(cmbTipoConexion)

            Me.txtDireccionIP.Properties.Mask.EditMask = "([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}"
            Me.txtDireccionIP.Properties.Mask.MaskType = Mask.MaskType.RegEx

            Select Case Modo

                Case TipoTrans.Nuevo

                    lblCodigo.Text = clsLnImpresora.MaxID()
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

                    gBeImpresora = clsLnImpresora.Obtener(gBeImpresora.IdImpresora)

                    lblCodigo.Text = gBeImpresora.IdImpresora
                    cmbEmpresa.EditValue = gBeImpresora.IdEmpresa
                    cmdBodega.EditValue = gBeImpresora.IdBodega
                    txtNombre.Text = gBeImpresora.Nombre
                    txtDireccionIP.Text = gBeImpresora.Direccion_Ip
                    chkActivo.Checked = gBeImpresora.Activo

                    User_agrTextEdit.Text = gBeImpresora.User_agr
                    Fec_agrDateEdit.Text = gBeImpresora.Fec_agr
                    User_modTextEdit.Text = gBeImpresora.User_mod
                    Fec_modDateEdit.Text = gBeImpresora.Fec_mod
                    txtAddress.Text = gBeImpresora.mac_adress
                    txtPuerto.Text = gBeImpresora.Puerto
                    txtVelocidad.Text = gBeImpresora.Velocidad
                    chkEsMovil.Checked = gBeImpresora.Es_Movil

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

    End Sub

    Private Function Datos_Correctos()

        Datos_Correctos = False

        Try

            If cmbEmpresa.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Empresa.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf String.IsNullOrEmpty(txtNombre.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Nombre.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtNombre.Focus()
                'ElseIf String.IsNullOrEmpty(txtDireccionIP.Text) = False Then
                '    Dim llave As String() = Split(txtDireccionIP.Text, ".")
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

            gBeImpresora = New clsBeImpresora()
            gBeImpresora.IdImpresora = clsLnImpresora.MaxID()
            gBeImpresora.IdEmpresa = cmbEmpresa.EditValue
            gBeImpresora.Nombre = txtNombre.Text.Trim()
            gBeImpresora.Direccion_Ip = txtDireccionIP.Text.Trim
            gBeImpresora.Activo = True
            gBeImpresora.User_agr = AP.UsuarioAp.IdUsuario
            gBeImpresora.Fec_agr = Now
            gBeImpresora.User_mod = AP.UsuarioAp.IdUsuario
            gBeImpresora.Fec_mod = Now
            gBeImpresora.mac_adress = txtAddress.Text
            gBeImpresora.IdBodega = cmdBodega.EditValue
            gBeImpresora.Numero_Serie = txtNoSerie.Text.Trim
            gBeImpresora.IdImpresoraMarca = cmbMarca.EditValue
            gBeImpresora.IdLenguaje = cmbLenguaje.EditValue
            gBeImpresora.IdTipoConexion = cmbTipoConexion.EditValue
            gBeImpresora.Puerto = txtPuerto.EditValue
            gBeImpresora.Es_Movil = chkEsMovil.Checked
            gBeImpresora.Velocidad = txtVelocidad.EditValue

            Guardar = clsLnImpresora.Insertar(gBeImpresora) > 0

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

                gBeImpresora.Nombre = txtNombre.Text.Trim()
                gBeImpresora.IdEmpresa = cmbEmpresa.EditValue
                gBeImpresora.Direccion_Ip = txtDireccionIP.Text.Trim()
                gBeImpresora.Activo = chkActivo.Checked
                gBeImpresora.User_mod = AP.UsuarioAp.IdUsuario
                gBeImpresora.Fec_mod = Now
                gBeImpresora.mac_adress = txtAddress.Text
                gBeImpresora.IdBodega = cmdBodega.EditValue
                gBeImpresora.Numero_Serie = txtNoSerie.Text.Trim
                gBeImpresora.IdImpresoraMarca = cmbMarca.EditValue
                gBeImpresora.IdLenguaje = cmbLenguaje.EditValue
                gBeImpresora.IdTipoConexion = cmbTipoConexion.EditValue
                gBeImpresora.Puerto = txtPuerto.EditValue
                gBeImpresora.Es_Movil = chkEsMovil.Checked
                gBeImpresora.Velocidad = txtVelocidad.EditValue

                Actualizar = clsLnImpresora.Actualizar(gBeImpresora) > 0

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
            If XtraMessageBox.Show("¿Guardar Impresora?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                If Guardar() Then
                    XtraMessageBox.Show("Se guardó el registro.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Close()
                End If
            End If
        End If
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        If Actualizar() Then
            XtraMessageBox.Show("Se actualizó el registro.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Close()
        End If
    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try

            If XtraMessageBox.Show("¿Desactivar la Impresora?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                gBeImpresora.Activo = False
                If clsLnImpresora.Actualizar(gBeImpresora) > 0 Then
                    XtraMessageBox.Show("Se ha desactivado el registro.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Close()
                    frmImpresora_List.Dgrid.Refresh()
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

    Private Sub cmbEmpresa_EditValueChanged(sender As Object, e As EventArgs) Handles cmbEmpresa.EditValueChanged
        IMS.Listar_Bodegas_Por_Empresa(cmdBodega, cmbEmpresa.EditValue)
    End Sub

    Public Shared Function Listar_Impresora_Marca(ByRef Cmb As LookUpEdit) As Boolean

        Listar_Impresora_Marca = False

        Dim DT As New DataTable

        Try

            DT = clsLnImpresora_Marca.Listar()

            If DT.Rows.Count > 0 Then

                Cmb.Properties.DisplayMember = "Nombre"
                Cmb.Properties.ValueMember = "IdImpresoraMarca"
                Cmb.Properties.DataSource = DT

                If DT.Rows.Count >= 1 Then
                    Cmb.ItemIndex = 0
                    Cmb.Properties.PopupWidth = 700
                    Cmb.Properties.PopulateColumns()
                    Cmb.Properties.Columns(0).Visible = False
                    Cmb.Properties.BestFit()
                    Cmb.Properties.NullText = ""
                End If

            End If

            Listar_Impresora_Marca = DT.Rows.Count > 0

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Public Shared Function Listar_Impresora_Lenguaje(ByRef Cmb As LookUpEdit) As Boolean

        Listar_Impresora_Lenguaje = False

        Dim DT As New DataTable

        Try

            DT = clsLnImpresora_Lenguaje.Listar()

            If DT.Rows.Count > 0 Then

                Cmb.Properties.DisplayMember = "Nombre"
                Cmb.Properties.ValueMember = "IdImpresoraLenguaje"
                Cmb.Properties.DataSource = DT

                If DT.Rows.Count >= 1 Then
                    Cmb.ItemIndex = 0
                    Cmb.Properties.PopupWidth = 700
                    Cmb.Properties.PopulateColumns()
                    Cmb.Properties.Columns(0).Visible = False
                    Cmb.Properties.BestFit()
                    Cmb.Properties.NullText = ""
                End If

            End If

            Listar_Impresora_Lenguaje = DT.Rows.Count > 0

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Public Shared Function Listar_Impresora_Tipo_Conexion(ByRef Cmb As LookUpEdit) As Boolean

        Listar_Impresora_Tipo_Conexion = False

        Dim DT As New DataTable

        Try

            DT = clsLnImpresora_Tipo_Conexion.Listar()

            If DT.Rows.Count > 0 Then

                Cmb.Properties.DisplayMember = "Nombre"
                Cmb.Properties.ValueMember = "IdImpresoraTipoConexion"
                Cmb.Properties.DataSource = DT

                If DT.Rows.Count >= 1 Then
                    Cmb.ItemIndex = 0
                    Cmb.Properties.PopupWidth = 700
                    Cmb.Properties.PopulateColumns()
                    Cmb.Properties.Columns(0).Visible = False
                    Cmb.Properties.BestFit()
                    Cmb.Properties.NullText = ""
                End If

            End If

            Listar_Impresora_Tipo_Conexion = DT.Rows.Count > 0

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

End Class