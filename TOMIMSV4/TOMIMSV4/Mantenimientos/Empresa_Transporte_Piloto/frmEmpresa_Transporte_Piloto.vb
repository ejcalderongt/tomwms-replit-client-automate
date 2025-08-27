Imports DevExpress.XtraEditors

Public Class frmEmpresa_Transporte_Piloto

    Private pListObJT As New List(Of clsTabla)
    Public pObjBEJ As New clsBeEmpresa_transporte_pilotos
    Public Delegate Sub Listar_EmpresaTransporte_Piloto()
    Public Property InvokeListarEmpresaPiloto As Listar_EmpresaTransporte_Piloto

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

    Private Sub frmEmpresa_Transporte_Piloto_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            pListObJT = clsBD.GetLongitudByTabla("empresa_transporte_pilotos")

            If Not IMS.Listar_EmpresaTransportePorEmpresa(cmbEmpresaT, AP.IdEmpresa) Then
                XtraMessageBox.Show("No hay empresas de transporte definidas para la aplicación.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            Dim Licencias As DataTable = TipoLicencias()

            cmbTipoLicencia.Properties.DataSource = Licencias
            cmbTipoLicencia.Properties.DisplayMember = "Nombre"
            cmbTipoLicencia.Properties.ValueMember = "Id"
            cmbTipoLicencia.ItemIndex = 0

            Select Case Modo

                Case TipoTrans.Nuevo

                    lblCodigo.Text = clsLnEmpresa_transporte_pilotos.MaxID()
                    cmbEmpresaT.Enabled = True
                    User_agrTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_agrDateEdit.Text = Now
                    User_modTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_modDateEdit.Text = Now

                    If OpcionesMenu IsNot Nothing Then
                        mnuGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False
                    txtDPI.Enabled = True
                    cmbEmpresaT.Enabled = True
                    dtmFechaNacimiento.DateTime = Today
                    dtmFechaInicio.DateTime = Today
                    dtmFechaSalida.DateTime = Today
                    dtmFechaExpiracionCarnet.DateTime = Today
                    dtmFechaExpiracionLicencia.DateTime = Today

                Case TipoTrans.Editar

                    'clsLnEmpresa_transporte_pilotos.Obtener(pObjBEJ)
                    lblCodigo.Text = pObjBEJ.IdPiloto
                    cmbEmpresaT.EditValue = pObjBEJ.IdEmpresaTransporte

                    txtNombres.Text = pObjBEJ.Nombres
                    txtApellidos.Text = pObjBEJ.Apellidos
                    txtTelefono.Text = pObjBEJ.Telefono
                    txtCorreo.Text = pObjBEJ.Correo_electronico
                    txtNoCarnet.Text = pObjBEJ.No_carnet

                    dtmFechaExpiracionCarnet.EditValue = pObjBEJ.Fecha_expiracion_carnet

                    txtDPI.Text = pObjBEJ.No_dpi
                    txtCodigoBarra.Text = pObjBEJ.Codigo_barra
                    txtDireccion.Text = pObjBEJ.Direccion

                    '#GT08092022: el tipo es string, se valida que no venga vacio.
                    If pObjBEJ.IdTipoLicencia <> "" Then
                        'cmbTipoLicencia.ItemIndex = pObjBEJ.IdTipoLicencia - 1
                        cmbTipoLicencia.EditValue = pObjBEJ.IdTipoLicencia
                    End If

                    txtLicencia.Text = pObjBEJ.No_Licencia
                    dtmFechaExpiracionLicencia.EditValue = pObjBEJ.Fecha_expiracion_licencia

                    If pObjBEJ.Foto IsNot Nothing Then
                        picFoto.Image = ByteArrayToImage(pObjBEJ.Foto)
                    End If

                    dtmFechaNacimiento.EditValue = pObjBEJ.Fecha_nacimiento
                    dtmFechaInicio.EditValue = pObjBEJ.Fecha_ingreso
                    dtmFechaSalida.EditValue = pObjBEJ.Fecha_salida

                    picFoto.SizeMode = PictureBoxSizeMode.StretchImage
                    User_agrTextEdit.Text = pObjBEJ.User_agr
                    Fec_agrDateEdit.Text = pObjBEJ.Fec_agr
                    User_modTextEdit.Text = pObjBEJ.User_mod
                    Fec_modDateEdit.Text = pObjBEJ.Fec_mod

                    mnuGuardar.Enabled = False

                    If OpcionesMenu IsNot Nothing Then
                        mnuActualizar.Enabled = OpcionesMenu.Modificar
                        mnuEliminar.Enabled = OpcionesMenu.Eliminar
                    End If

                    txtDPI.Enabled = False
                    cmbEmpresaT.Enabled = False

                    chkActivo.Checked = pObjBEJ.Activo

            End Select

            Application.DoEvents()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

        Focus()

        txtNombres.Focus()

    End Sub

    Shared Function TipoLicencias() As DataTable

        Dim table As New DataTable

        table.Columns.Add("Id", GetType(String))
        table.Columns.Add("Nombre", GetType(String))

        table.Rows.Add("A", "A")
        table.Rows.Add("B", "B")
        table.Rows.Add("C", "C")
        table.Rows.Add("M", "M")
        table.Rows.Add("E", "E")
        Return table

    End Function

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick

        Try

            mnuGuardar.Enabled = False

            If Datos_Correctos() Then

                If MessageBox.Show("¿Guardar el Piloto?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    If Guardar() Then
                        XtraMessageBox.Show("Se guardó el registro.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        If Not InvokeListarEmpresaPiloto Is Nothing Then InvokeListarEmpresaPiloto.Invoke
                        mnuGuardar.Enabled = True
                        Close()

                    End If
                End If

            End If

            mnuGuardar.Enabled = True
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Function Guardar() As Boolean
        Guardar = False
        Try

            Dim ObjN As New clsBeEmpresa_transporte_pilotos() With {.No_dpi = txtDPI.Text.Trim()}
            If clsLnEmpresa_transporte_pilotos.ExisteDPI(ObjN.No_dpi) Then
                XtraMessageBox.Show(String.Format("El DPI {0} ingresado ya existe.", txtDPI.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtDPI.SelectAll()
                Return False
            End If

            ObjN.No_Licencia = txtLicencia.Text.Trim()
            If clsLnEmpresa_transporte_pilotos.Existe_No_Licencia(ObjN.No_Licencia) Then
                XtraMessageBox.Show(String.Format("El Número de Licencia {0} ingresado ya existe.", txtLicencia.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtLicencia.SelectAll()
                Return False
            End If

            ObjN.IdEmpresaTransporte = cmbEmpresaT.EditValue
            ObjN.IdPiloto = clsLnEmpresa_transporte_pilotos.MaxID()

            ObjN.Nombres = txtNombres.Text.Trim()
            ObjN.Apellidos = txtApellidos.Text.Trim()
            ObjN.Telefono = txtTelefono.Text.Trim()
            ObjN.Correo_electronico = txtCorreo.Text.Trim()
            ObjN.No_carnet = txtNoCarnet.Text.Trim()
            ObjN.Fecha_expiracion_carnet = dtmFechaExpiracionCarnet.EditValue

            'GT 08092022 se obtiene el IdTipoLicencia del combo
            'Dim fila As Object = cmbTipoLicencia.GetSelectedDataRow
            'Dim IdTipoLicencia_ As Integer

            'If fila Is Nothing Then
            '    Throw New Exception("Error_20220908_0830: el Tipo de licencia no es valido.")
            'Else
            '    IdTipoLicencia_ = fila.Item("IdTipoLicencia")
            'End If

            ObjN.IdTipoLicencia = cmbTipoLicencia.EditValue

            ObjN.Fecha_expiracion_licencia = dtmFechaExpiracionLicencia.EditValue

            ObjN.Codigo_barra = txtCodigoBarra.Text.Trim()
            ObjN.Direccion = txtDireccion.Text.Trim()

            If picFoto.Image IsNot Nothing Then
                ObjN.Foto = ImageToByteArray(picFoto.Image)
            End If

            ObjN.Fecha_nacimiento = dtmFechaNacimiento.EditValue
            ObjN.Fecha_ingreso = dtmFechaInicio.EditValue
            ObjN.Fecha_salida = dtmFechaSalida.EditValue

            ObjN.Activo = True
            ObjN.User_agr = AP.UsuarioAp.IdUsuario
            ObjN.Fec_agr = Now
            ObjN.User_mod = AP.UsuarioAp.IdUsuario
            ObjN.Fec_mod = Now

            Guardar = clsLnEmpresa_transporte_pilotos.Insertar(ObjN) > 0

            pObjBEJ = ObjN

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            If Datos_Correctos() Then


                '#GT08092022: El combo no maneja un id numerico, se valida si tiene un string
                If cmbTipoLicencia.EditValue <> "" Then
                    pObjBEJ.IdTipoLicencia = cmbTipoLicencia.EditValue
                End If

                pObjBEJ.Nombres = txtNombres.Text.Trim()
                pObjBEJ.Apellidos = txtApellidos.Text.Trim()
                pObjBEJ.Telefono = txtTelefono.Text.Trim()
                pObjBEJ.Correo_electronico = txtCorreo.Text.Trim()
                pObjBEJ.No_carnet = txtNoCarnet.Text.Trim()
                pObjBEJ.Fecha_expiracion_carnet = dtmFechaExpiracionCarnet.EditValue

                pObjBEJ.Fecha_expiracion_licencia = dtmFechaExpiracionLicencia.EditValue

                pObjBEJ.No_Licencia = txtLicencia.Text.Trim()
                pObjBEJ.Codigo_barra = txtCodigoBarra.Text.Trim()
                pObjBEJ.Direccion = txtDireccion.Text.Trim()

                pObjBEJ.Fecha_nacimiento = dtmFechaNacimiento.EditValue
                pObjBEJ.Fecha_ingreso = dtmFechaInicio.EditValue
                pObjBEJ.Fecha_salida = dtmFechaSalida.EditValue

                If picFoto.Image IsNot Nothing Then
                    pObjBEJ.Foto = ImageToByteArray(picFoto.Image)
                End If


                pObjBEJ.User_mod = AP.UsuarioAp.IdUsuario
                pObjBEJ.Fec_mod = Now

                pObjBEJ.Activo = chkActivo.Checked

                Actualizar = clsLnEmpresa_transporte_pilotos.Actualizar(pObjBEJ) > 0
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Datos_Correctos() As Boolean
        Datos_Correctos = False
        Try
            If cmbEmpresaT.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Empresa Transporte.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf String.IsNullOrEmpty(txtNombres.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Nombre.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombres.Focus()

            ElseIf txtNombres.Text.Count > pListObJT.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRES").Longitud Then
                XtraMessageBox.Show(String.Format("Los Nombres deben de tener como máximo {0} carácteres.", pListObJT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "NOMBRES").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombres.Focus()

            ElseIf String.IsNullOrEmpty(txtApellidos.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Apellido.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtApellidos.Focus()

            ElseIf txtApellidos.Text.Count > pListObJT.Find(Function(b) b.NombreCampo.ToUpper = "APELLIDOS").Longitud Then
                XtraMessageBox.Show(String.Format("Los Apellidos deben de tener como máximo {0} carácteres.", pListObJT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "APELLIDOS").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtApellidos.Focus()

            ElseIf txtTelefono.Text.Count > pListObJT.Find(Function(b) b.NombreCampo.ToUpper = "TELEFONO").Longitud Then
                XtraMessageBox.Show(String.Format("El Teléfono deben de tener como máximo {0} carácteres.", pListObJT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "TELEFONO").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtTelefono.Focus()

            ElseIf txtCorreo.Text.Count > pListObJT.Find(Function(b) b.NombreCampo.ToUpper = "CORREO_ELECTRONICO").Longitud Then
                XtraMessageBox.Show(String.Format("El Correo Electrónico deben de tener como máximo {0} carácteres.", pListObJT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "CORREO_ELECTRONICO").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtCorreo.Focus()

            ElseIf txtNoCarnet.Text.Count > pListObJT.Find(Function(b) b.NombreCampo.ToUpper = "NO_CARNET").Longitud Then
                XtraMessageBox.Show(String.Format("El Número de Carnet deben de tener como máximo {0} carácteres.", pListObJT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "NO_CARNET").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNoCarnet.Focus()

            ElseIf String.IsNullOrEmpty(txtLicencia.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Número de Licencia.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtLicencia.Focus()

            ElseIf txtLicencia.Text.Count > pListObJT.Find(Function(b) b.NombreCampo.ToUpper = "NO_LICENCIA").Longitud Then
                XtraMessageBox.Show(String.Format("El Número de Licencia deben de tener como máximo {0} carácteres.", pListObJT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "NO_LICENCIA").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtLicencia.Focus()

            ElseIf txtCodigoBarra.Text.Count > pListObJT.Find(Function(b) b.NombreCampo.ToUpper = "CODIGO_BARRA").Longitud Then
                XtraMessageBox.Show(String.Format("El Código de Barra deben de tener como máximo {0} carácteres.", pListObJT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "CODIGO_BARRA").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtCodigoBarra.Focus()

            ElseIf txtDireccion.Text.Count > pListObJT.Find(Function(b) b.NombreCampo.ToUpper = "DIRECCION").Longitud Then
                XtraMessageBox.Show(String.Format("La Dirección deben de tener como máximo {0} carácteres.", pListObJT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "CODIGO_BARRA").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtDireccion.Focus()

            ElseIf cmbTipoLicencia.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Tipo de Licencia.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf String.IsNullOrEmpty(txtDPI.Text.Trim) Then
                XtraMessageBox.Show("Ingrese DPI.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtDPI.Focus()

            ElseIf txtDPI.Text.Count > pListObJT.Find(Function(b) b.NombreCampo.ToUpper = "NO_DPI").Longitud Then
                XtraMessageBox.Show(String.Format("El Número de DPI deben de tener como máximo {0} carácteres.", pListObJT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "NO_DPI").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtDPI.Focus()

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
            XtraMessageBox.Show("Se actualizó el registro.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            InvokeListarEmpresaPiloto.Invoke
            Close()
        End If
        mnuActualizar.Enabled = True
    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try
            mnuEliminar.Enabled = False

            If MessageBox.Show("¿Desactivar el Piloto?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                pObjBEJ.Activo = False

                If clsLnEmpresa_transporte_pilotos.Actualizar(pObjBEJ) > 0 Then
                    XtraMessageBox.Show("Se ha desactivado el registro.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    InvokeListarEmpresaPiloto.Invoke
                    Close()
                    frmEmpresa_Transporte_PilotoList.Dgrid.Refresh()
                End If

            End If
            mnuEliminar.Enabled = True

        Catch ex As Exception
            mnuEliminar.Enabled = True
            If ex.HResult = -2146233088 Then TablasRelacionadas("empresa_transporte_pilotos", pObjBEJ.IdPiloto)
        End Try

    End Sub

    Private Sub btnExaminar_Click(sender As Object, e As EventArgs) Handles btnExaminar.Click
        Dim ofd As New OpenFileDialog() With {.Filter = "Imagenes JPG|*.jpg", .RestoreDirectory = True}
        If ofd.ShowDialog = DialogResult.OK Then
            picFoto.Image = Image.FromFile(ofd.FileName)
        End If
    End Sub

    Public Shared Function ImageToByteArray(ByVal imageIn As Image) As Byte()
        Dim ms As New IO.MemoryStream()
        imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
        Return ms.ToArray()
    End Function

    Public Shared Function ByteArrayToImage(ByVal byteArrayIn As Byte()) As Image
        Dim ms As New IO.MemoryStream(byteArrayIn)
        Return Image.FromStream(ms)
    End Function

    Private Sub frmEmpresa_Transporte_Piloto_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub cmdImprimirCarnet_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimirCarnet.ItemClick

    End Sub

    Private Sub PanDatosMotivoAnulacion_Paint(sender As Object, e As PaintEventArgs) Handles PanDatosMotivoAnulacion.Paint

    End Sub
End Class