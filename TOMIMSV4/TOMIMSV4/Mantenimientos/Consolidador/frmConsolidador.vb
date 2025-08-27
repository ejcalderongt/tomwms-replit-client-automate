Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Mask

Public Class frmConsolidador

    Private DT As DataTable

    Public pIdCliente As Integer

    Public Property Propietario As New clsBePropietarios
    Public gBeConsolidador As New clsBeConsolidador

    'Private pClienteTiemposList As New List(Of clsBeCliente_tiempos)
    'Private pDirEntList As New List(Of clsBeCliente_direccion)
    'Private pIdTiempoCliente As String = String.Empty
    'Private pIdDireccionEntregaCliente As String = String.Empty

    Public Delegate Sub ListarClientes()
    Public Property InvokeListarClientes As ListarClientes

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


    Private Sub frmConsolidador_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Top = 10
        Left = (Screen.PrimaryScreen.WorkingArea.Width - Width) / 2

        Try

            IMS.Listar_Empresas(cmbEmpresa)

            txtTelefono.Properties.Mask.EditMask = "\((\d{3})\) (\d{4})-(\d{4})"
            txtTelefono.Properties.Mask.MaskType = MaskType.RegEx

            Select Case Modo

                Case TipoTrans.Nuevo

                    lblConsolidador.Text = clsLnConsolidador.MaxID()
                    'User_agrTextEdit.Text = AP.UsuarioAp.IdUsuario
                    'Fec_agrDateEdit.Text = Now
                    'User_modTextEdit.Text = AP.UsuarioAp.IdUsuario
                    'Fec_modDateEdit.Text = Now

                    If OpcionesMenu IsNot Nothing Then
                        mnuGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False


                    'If Propietario.IdPropietario <> 0 Then
                    '    lcmbPropietario.EditValue = Propietario.IdPropietario
                    '    lcmbPropietario.Enabled = False
                    'End If

                Case TipoTrans.Editar

                    Cargar_Consolidador()
                    'Cargar_Tiempos_Aceptacion(False)
                    'Cargar_Direcciones_Entrega(False)

                    mnuGuardar.Enabled = False
                    mnuActualizar.Enabled = OpcionesMenu.Modificar
                    mnuEliminar.Enabled = OpcionesMenu.Eliminar

            End Select

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try


    End Sub


    Private Sub Cargar_Consolidador()

        Try

            lblConsolidador.Text = gBeConsolidador.Idconsolidador
            txtCodigo.Text = gBeConsolidador.Codigo
            txtNombreComercial.Text = gBeConsolidador.Nom_comercial
            txtTelefono.Text = gBeConsolidador.Telefono
            txtNit.Text = gBeConsolidador.Nit
            txtDireccion.Text = gBeConsolidador.Direccion
            chkActivo.Checked = gBeConsolidador.Activo

            'User_agrTextEdit.Text = gBeCliente.User_agr
            'Fec_agrDateEdit.Text = gBeCliente.Fec_agr
            'User_modTextEdit.Text = gBeCliente.User_mod
            'Fec_modDateEdit.Text = gBeCliente.Fec_mod

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick
        If Datos_Correctos() Then
            If MessageBox.Show("¿Guardar Datos?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                If Guardar() Then
                    XtraMessageBox.Show("Se guardaron los datos", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    InvokeListarClientes.Invoke
                    Close()
                End If
            End If
        End If
    End Sub

    Private Function Datos_Correctos()

        Datos_Correctos = False

        Try

            If String.IsNullOrEmpty(txtCodigo.Text.Trim) Then
                XtraMessageBox.Show("Ingrese Código", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtCodigo.Focus()
            ElseIf String.IsNullOrEmpty(txtNombreComercial.Text.Trim) Then
                XtraMessageBox.Show("Ingrese Nombre Comercial", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombreComercial.Focus()
            ElseIf String.IsNullOrEmpty(txtRazonSocial.Text.Trim) Then
                XtraMessageBox.Show("Ingrese razón social", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtRazonSocial.Focus()
            ElseIf String.IsNullOrEmpty(txtNit.Text.Trim) Then
                XtraMessageBox.Show("Ingrese NIT", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtDireccion.Focus()
            ElseIf String.IsNullOrEmpty(txtTelefono.Text.Trim) Then
                XtraMessageBox.Show("Ingrese Teléfono", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtTelefono.Focus()
            ElseIf String.IsNullOrEmpty(txtDireccion.Text.Trim) Then
                XtraMessageBox.Show("Ingrese Dirección", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtDireccion.Focus()

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

            Dim ObjNC As New clsBeConsolidador()

            With ObjNC

                .Codigo = txtCodigo.Text.Trim()
                .IdEmpresa = cmbEmpresa.EditValue
                .Nom_comercial = txtNombreComercial.Text.Trim()
                .Telefono = txtTelefono.Text.Trim()
                .Nit = txtNit.Text.Trim()
                .Direccion = txtDireccion.Text.Trim()
                .User_agr = AP.UsuarioAp.IdUsuario
                .Fec_agr = Now
                .User_mod = AP.UsuarioAp.IdUsuario
                .Fec_mod = Now
                .Activo = True

            End With


            clsLnConsolidador.Guardar_Transaccion(ObjNC)

            Return True

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        If Actualizar() Then
            XtraMessageBox.Show("Se actualizaron los datos", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            InvokeListarClientes.Invoke
            Close()
        End If
    End Sub

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            If Datos_Correctos() Then

                gBeConsolidador.Codigo = txtCodigo.Text.Trim()
                gBeConsolidador.Nom_comercial = txtNombreComercial.Text.Trim()

                gBeConsolidador.Telefono = txtTelefono.Text.Trim()
                gBeConsolidador.Nit = txtNit.Text.Trim()
                gBeConsolidador.Direccion = txtDireccion.Text.Trim()

                gBeConsolidador.User_mod = AP.UsuarioAp.IdUsuario
                gBeConsolidador.Fec_mod = Now

                gBeConsolidador.Activo = chkActivo.Checked

                clsLnConsolidador.Guardar_Transaccion(gBeConsolidador)
                Return True

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

End Class