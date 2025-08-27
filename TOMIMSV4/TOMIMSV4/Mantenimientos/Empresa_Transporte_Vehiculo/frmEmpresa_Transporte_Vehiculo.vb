Imports DevExpress.XtraEditors

Public Class frmEmpresa_Transporte_Vehiculo
    Private pListObjt As New List(Of clsTabla)
    Public pObjBEJ As New clsBeEmpresa_transporte_vehiculos
    Public Delegate Sub Listar_EmpresaTransporte_Vehiculo()
    Public Property InvokeListarEmpresaTransporteVehiculo As Listar_EmpresaTransporte_Vehiculo

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

    Private Sub frmEmpresa_Transporte_Vehiculo_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            pListObjt = clsBD.GetLongitudByTabla("empresa_transporte_vehiculos")

            If Not IMS.Listar_EmpresaTransportePorEmpresa(cmbEmpresaT, AP.IdEmpresa) Then
                XtraMessageBox.Show("No hay empresas de transporte definidas para la aplicación.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            IMS.Listar_TipoContenedor(cmbTipoContenedor)

            Select Case Modo

                Case TipoTrans.Nuevo

                    lblCodigo.Text = clsLnEmpresa_transporte_vehiculos.MaxID
                    cmbEmpresaT.Enabled = True
                    User_agrTextEdit.Text = AP.UsuarioAp.Nombres + " " + AP.UsuarioAp.Apellidos
                    Fec_agrDateEdit.Text = Now
                    User_modTextEdit.Text = AP.UsuarioAp.Nombres + " " + AP.UsuarioAp.Apellidos
                    Fec_modDateEdit.Text = Now

                    If OpcionesMenu IsNot Nothing Then
                        mnuGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False
                    txtPlaca.Enabled = True
                    cmbEmpresaT.Enabled = True

                Case TipoTrans.Editar

                    'clsLnEmpresa_transporte_vehiculos.Obtener(pObjBEJ)
                    lblCodigo.Text = pObjBEJ.IdVehiculo
                    cmbEmpresaT.EditValue = pObjBEJ.IdEmpresaTransporte
                    txtPlaca.Text = pObjBEJ.Placa
                    txtModelo.Text = pObjBEJ.Modelo
                    txtMarca.Text = pObjBEJ.Marca
                    txtTipo.Text = pObjBEJ.Tipo
                    txtPlacaComercial.Text = pObjBEJ.Placa_comercial
                    txtPeso.Text = pObjBEJ.Peso
                    txtVolumen.Text = pObjBEJ.Volumen
                    txtAlto.Text = pObjBEJ.Alto
                    txtLargo.Text = pObjBEJ.Largo
                    txtAncho.Text = pObjBEJ.Ancho

                    If pObjBEJ.IdTipoContenedor > 0 Then
                        cmbTipoContenedor.EditValue = pObjBEJ.IdTipoContenedor
                    End If

                    chkEsContenedor.Checked = pObjBEJ.Es_contedor
                    chkActivo.Checked = pObjBEJ.Activo

                    User_agrTextEdit.Text = pObjBEJ.User_agr
                    Fec_agrDateEdit.Text = pObjBEJ.Fec_agr
                    User_modTextEdit.Text = pObjBEJ.User_mod
                    Fec_modDateEdit.Text = pObjBEJ.Fec_mod

                    mnuGuardar.Enabled = False

                    If OpcionesMenu IsNot Nothing Then
                        mnuActualizar.Enabled = OpcionesMenu.Modificar
                        mnuEliminar.Enabled = OpcionesMenu.Eliminar
                    End If

                    txtPlaca.Enabled = False
                    cmbEmpresaT.Enabled = False

            End Select

            Application.DoEvents()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

        txtPlaca.Focus()

    End Sub

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick

        mnuGuardar.Enabled = False
        If Datos_Correctos() Then

            If MessageBox.Show("¿Guardar el Vehículo?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                If Guardar() Then
                    XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    If Not InvokeListarEmpresaTransporteVehiculo Is Nothing Then InvokeListarEmpresaTransporteVehiculo.Invoke
                    Close()
                End If

            End If

        End If
        mnuGuardar.Enabled = True

    End Sub

    Private Function Guardar() As Boolean

        Guardar = False

        Try

            pObjBEJ = New clsBeEmpresa_transporte_vehiculos()

            pObjBEJ.IdEmpresaTransporte = cmbEmpresaT.EditValue
            pObjBEJ.IdVehiculo = clsLnEmpresa_transporte_vehiculos.MaxID()

            pObjBEJ.Placa = txtPlaca.Text.Trim()

            If clsLnEmpresa_transporte_vehiculos.Existe_Placa(txtPlaca.Text.Trim()) Then
                XtraMessageBox.Show(String.Format("La placa {0} ingresada ya existe.", txtPlaca.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtPlaca.SelectAll()
                Return False
            End If

            pObjBEJ.Marca = txtMarca.Text.Trim()
            pObjBEJ.Modelo = txtModelo.Text.Trim()
            pObjBEJ.Tipo = txtTipo.Text.Trim()
            pObjBEJ.Placa_comercial = txtPlacaComercial.Text.Trim()

            pObjBEJ.Peso = txtPeso.Text.Trim()
            pObjBEJ.Volumen = txtVolumen.Text.Trim()
            pObjBEJ.Alto = txtAlto.Text.Trim()
            pObjBEJ.Largo = txtLargo.Text.Trim()
            pObjBEJ.Ancho = txtAncho.Text.Trim()

            If cmbTipoContenedor.ItemIndex > -1 Then
                pObjBEJ.IdTipoContenedor = cmbTipoContenedor.EditValue
            End If

            pObjBEJ.Es_contedor = chkEsContenedor.Checked

            pObjBEJ.Activo = True
            pObjBEJ.User_agr = AP.UsuarioAp.IdUsuario
            pObjBEJ.Fec_agr = Now
            pObjBEJ.User_mod = AP.UsuarioAp.IdUsuario
            pObjBEJ.Fec_mod = Now

            Guardar = clsLnEmpresa_transporte_vehiculos.Insertar(pObjBEJ) > 0

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try
            If Datos_Correctos() Then

                pObjBEJ.Placa = txtPlaca.Text.Trim()
                pObjBEJ.Marca = txtMarca.Text.Trim()
                pObjBEJ.Modelo = txtModelo.Text.Trim()
                pObjBEJ.Tipo = txtTipo.Text.Trim()
                pObjBEJ.Placa_comercial = txtPlacaComercial.Text.Trim()

                pObjBEJ.Peso = txtPeso.Text.Trim()
                pObjBEJ.Volumen = txtVolumen.Text.Trim()
                pObjBEJ.Alto = txtAlto.Text.Trim()
                pObjBEJ.Largo = txtLargo.Text.Trim()
                pObjBEJ.Ancho = txtAncho.Text.Trim()

                If cmbTipoContenedor.ItemIndex > -1 Then
                    pObjBEJ.IdTipoContenedor = cmbTipoContenedor.EditValue
                End If

                pObjBEJ.Es_contedor = chkEsContenedor.Checked
                pObjBEJ.Activo = chkActivo.Checked

                pObjBEJ.User_mod = AP.UsuarioAp.IdUsuario
                pObjBEJ.Fec_mod = Now
                Actualizar = clsLnEmpresa_transporte_vehiculos.Actualizar(pObjBEJ) > 0

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Datos_Correctos() As Boolean
        Datos_Correctos = False
        Try
            If cmbEmpresaT.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Empresa Transporte.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf String.IsNullOrEmpty(txtPlaca.Text.Trim) Then
                XtraMessageBox.Show("Ingrese Placa.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtPlaca.Focus()
            ElseIf txtPlaca.Text.Count > pListObjt.Find(Function(b) b.NombreCampo.ToUpper = "PLACA").Longitud Then
                XtraMessageBox.Show("La Placa debe de tener como máximo " & pListObjt.Find(Function(b) b.NombreCampo.ToUpper = "PLACA").Longitud & " carácteres.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtPlaca.Focus()

            ElseIf txtMarca.Text.Count > pListObjt.Find(Function(b) b.NombreCampo.ToUpper = "MARCA").Longitud Then
                XtraMessageBox.Show("La Marca debe de tener como máximo " & pListObjt.Find(Function(b) b.NombreCampo.ToUpper = "MARCA").Longitud & " carácteres.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtMarca.Focus()

            ElseIf txtModelo.Text.Count > pListObjt.Find(Function(b) b.NombreCampo.ToUpper = "MODELO").Longitud Then
                XtraMessageBox.Show("El Modelo debe de tener como máximo " & pListObjt.Find(Function(b) b.NombreCampo.ToUpper = "MODELO").Longitud & " carácteres.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtModelo.Focus()

            ElseIf txtTipo.Text.Count > pListObjt.Find(Function(b) b.NombreCampo.ToUpper = "TIPO").Longitud Then
                XtraMessageBox.Show("El Tipo debe de tener como máximo " & pListObjt.Find(Function(b) b.NombreCampo.ToUpper = "TIPO").Longitud & " carácteres.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtTipo.Focus()

            ElseIf txtPlacaComercial.Text.Count > pListObjt.Find(Function(b) b.NombreCampo.ToUpper = "PLACA_COMERCIAL").Longitud Then
                XtraMessageBox.Show("La Placa Comercial debe de tener como máximo " & pListObjt.Find(Function(b) b.NombreCampo.ToUpper = "PLACA_COMERCIAL").Longitud & " carácteres.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtPlacaComercial.Focus()

            Else
                Datos_Correctos = True
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Function

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        mnuActualizar.Enabled = False
        If Actualizar() Then
            XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            InvokeListarEmpresaTransporteVehiculo.Invoke
            Close()
        End If
        mnuActualizar.Enabled = True

    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try

            mnuEliminar.Enabled = False
            If MessageBox.Show("¿Desactivar el Vehículo?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                pObjBEJ.Activo = False

                If clsLnEmpresa_transporte_vehiculos.Actualizar(pObjBEJ) > 0 Then
                    XtraMessageBox.Show("Se ha desactivado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    InvokeListarEmpresaTransporteVehiculo.Invoke
                    Close()
                    frmEmpresa_Transporte_VehiculoList.Dgrid.Refresh()
                End If

            End If
            mnuEliminar.Enabled = True

        Catch ex As Exception
            mnuEliminar.Enabled = True
            If ex.HResult = -2146233088 Then TablasRelacionadas("empresa_transporte_vehiculos", pObjBEJ.IdVehiculo)
        End Try

    End Sub

    Private Sub frmEmpresa_Transporte_Vehiculo_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

End Class