Imports System.Drawing.Imaging
Imports System.IO
Imports DevExpress.XtraEditors

Public Class frmEmpresa

    Private pListObjT As New List(Of clsTabla)
    Public Empresa As New clsBeEmpresa
    Public Delegate Sub Listar_Empresas()
    Public Property Listar As Listar_Empresas

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

    Private Sub frmEmpresa_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            pListObjT = clsBD.GetLongitudByTabla("Empresa")
            IMS.Listar_MotivosAjuste(cbxMotivosAjuste)

            Select Case Modo

                Case TipoTrans.Nuevo

                    Empresa.IdEmpresa = clsLnEmpresa.MaxID()
                    IdEmpresaSpinEdit.Text = Empresa.IdEmpresa
                    User_agrTextEdit.Text = AP.UsuarioAp.IdUsuario
                    Fec_agrDateEdit.Text = Now
                    User_modTextEdit.Text = AP.UsuarioAp.IdUsuario
                    Fec_modDateEdit.Text = Now

                    If OpcionesMenu IsNot Nothing Then
                        mnuGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False
                    picFoto.SizeMode = PictureBoxSizeMode.CenterImage

                Case TipoTrans.Editar

                    clsLnEmpresa.Obtener(Empresa)
                    IdEmpresaSpinEdit.Text = Empresa.IdEmpresa
                    NombreTextEdit.Text = Empresa.Nombre
                    DireccionTextEdit.Text = Empresa.Direccion
                    TelefonoTextEdit.Text = Empresa.Telefono
                    EmailTextEdit.Text = Empresa.Email
                    Razon_socialTextEdit.Text = Empresa.Razon_social
                    RepresentanteTextEdit.Text = Empresa.Representante
                    Corr_cod_barraSpinEdit.Text = Empresa.Corr_cod_barra
                    Path_printerTextEdit.Text = Empresa.Path_printer
                    CodigoTextEdit.Text = Empresa.Codigo
                    Puerto_escanerSpinEdit.Text = Empresa.Puerto_escaner
                    ClaveTextEdit.Text = clsPublic.Desencriptar(Empresa.Clave)
                    chkActivo.Checked = Empresa.Activo
                    chkClienteRapido.Checked = Empresa.ClienteRapido
                    chkOperadorlogistico.Checked = Empresa.Operador_logistico
                    chkControlpresentaciones.Checked = Empresa.Control_presentaciones
                    chkAnulacionesporsupervisor.Checked = Empresa.Anulaciones_por_supervisor
                    chkCodigoAutomatico.Checked = Empresa.codigo_automatico
                    cbxMotivosAjuste.EditValue = Empresa.IdMotivoAjusteInventario


                    User_agrTextEdit.Text = Empresa.User_agr
                    Fec_agrDateEdit.Text = Empresa.Fec_agr
                    User_modTextEdit.Text = Empresa.User_mod
                    Fec_modDateEdit.Text = Empresa.Fec_mod

                    mnuGuardar.Enabled = False

                    If OpcionesMenu IsNot Nothing Then
                        mnuActualizar.Enabled = OpcionesMenu.Modificar
                        mnuEliminar.Enabled = OpcionesMenu.Eliminar
                    End If

                    If Empresa.Imagen IsNot Nothing Then
                        picFoto.Image = ByteArrayToImage(Empresa.Imagen)
                    End If

                    cbxCaducidad.CheckState = IIf(Empresa.Duracionclave = 0, CheckState.Checked, CheckState.Unchecked)

                    SpinEditDuracion.Value = Empresa.Duracionclave
                    SpinEditDuracionTemporal.Value = Empresa.Duracionclavetemporal

                    cbxBloqueo.CheckState = IIf(Empresa.Intento = 0, CheckState.Unchecked, CheckState.Checked)
                    SpinEditIntentos.Value = Empresa.Intento

                    chkBuscarActualizacionHH.Checked = Empresa.buscar_actualizacion_hh

                    picFoto.SizeMode = PictureBoxSizeMode.CenterImage

                    txtVersionBD.Text = Empresa.Version_BD
                    txtAWSToken.Text = Empresa.AWS_Token

            End Select

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub btnExaminar_Click(sender As Object, e As EventArgs) Handles btnExaminar.Click
        Dim gFile As New OpenFileDialog() With {.Filter = "All Images|*.BMP;*.DIB;*.RLE;*.JPG;*.JPEG;*.JPE;*.JFIF;*.GIF;*.TIF;*.TIFF;*.PNG|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|All Files|*.*"}
        gFile.ShowDialog()
        If gFile.FileName.Length <> 0 Then
            picFoto.Image = Image.FromFile(gFile.FileName)
        End If
    End Sub

    Public Shared Function ByteArrayToImage(ByVal byteArrayIn As Byte()) As Image
        Dim ms As New MemoryStream(byteArrayIn)
        Return Image.FromStream(ms)
    End Function

    Public Shared Function ImageToByteArray(ByVal imageIn As Image) As Byte()
        Dim ms As New MemoryStream()
        imageIn.Save(ms, ImageFormat.Jpeg)
        Return ms.ToArray()
    End Function

    Private Function Guardar() As Boolean

        Guardar = False

        Try

            Empresa = New clsBeEmpresa() With {.IdEmpresa = clsLnEmpresa.MaxID,
                .Nombre = NombreTextEdit.Text,
                .Direccion = DireccionTextEdit.Text,
                .Telefono = TelefonoTextEdit.Text,
                .Email = EmailTextEdit.Text,
                .Razon_social = Razon_socialTextEdit.Text,
                .Representante = RepresentanteTextEdit.Text,
                .Corr_cod_barra = Corr_cod_barraSpinEdit.Text,
                .Path_printer = Path_printerTextEdit.Text,
                .Activo = chkActivo.Checked,
                .User_agr = AP.UsuarioAp.IdUsuario,
                .User_mod = AP.UsuarioAp.IdUsuario,
                .ClienteRapido = chkClienteRapido.Checked}
            'pic = Image2Bytes(picFoto.Image)

            If picFoto.Image IsNot Nothing Then
                Empresa.Imagen = ImageToByteArray(picFoto.Image)
            End If

            Empresa.IdMotivoAjusteInventario = cbxMotivosAjuste.EditValue
            Empresa.Operador_logistico = chkOperadorlogistico.Checked
            Empresa.Puerto_escaner = Puerto_escanerSpinEdit.Text
            Empresa.Control_presentaciones = chkControlpresentaciones.Checked
            Empresa.Anulaciones_por_supervisor = chkAnulacionesporsupervisor.Checked
            Empresa.Codigo_automatico = chkCodigoAutomatico.Checked
            Empresa.Codigo = CodigoTextEdit.Text
            Empresa.Clave = clsPublic.Encriptar(ClaveTextEdit.Text)

            Empresa.Duracionclave = IIf(cbxCaducidad.CheckState = CheckState.Checked, 0, CInt(SpinEditDuracion.Value))
            Empresa.Duracionclavetemporal = CInt(SpinEditDuracionTemporal.Value)
            Empresa.politica_contraseñas = chkExigirPoliticaDeContraseña.Checked
            Empresa.Intento = IIf(cbxBloqueo.CheckState = CheckState.Checked, CInt(SpinEditIntentos.Value), 0)
            '#GT12052022: si la app busca actualizacion
            Empresa.buscar_actualizacion_hh = chkBuscarActualizacionHH.Checked

            Empresa.Version_BD = txtVersionBD.Text.Trim
            Empresa.AWS_Token = txtAWSToken.Text.Trim

            Guardar = clsLnEmpresa.Insertar(Empresa) > 0

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

        mnuGuardar.Enabled = False

        If Datos_Correctos() Then

            If XtraMessageBox.Show("¿Guardar Empresa?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                If Guardar() Then

                    XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    If Listar IsNot Nothing Then
                        Listar.Invoke()
                    End If

                    DialogResult = DialogResult.OK

                    Close()

                End If

            End If

        End If

        mnuGuardar.Enabled = True

    End Sub

    Private Function Datos_Correctos() As Boolean

        Datos_Correctos = False
        'Dim Obj As New clsTabla

        Try

            If String.IsNullOrEmpty(NombreTextEdit.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Nombre.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                NombreTextEdit.Focus()

            ElseIf NombreTextEdit.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud Then
                XtraMessageBox.Show(String.Format("El Nombre debe de tener como máximo {0} carácteres.", pListObjT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "NOMBRE").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                NombreTextEdit.Focus()

            ElseIf DireccionTextEdit.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "DIRECCION").Longitud Then
                XtraMessageBox.Show(String.Format("La Dirección debe de tener como máximo {0} carácteres.", pListObjT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "DIRECCION").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                DireccionTextEdit.Focus()

            ElseIf EmailTextEdit.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "EMAIL").Longitud Then
                XtraMessageBox.Show(String.Format("El correo electrónico debe de tener como máximo {0} carácteres.", pListObjT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "EMAIL").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                EmailTextEdit.Focus()

            ElseIf Path_printerTextEdit.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "PATH_PRINTER").Longitud Then
                XtraMessageBox.Show(String.Format("El Path de la impresora debe de tener como máximo {0} carácteres.", pListObjT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "PATH_PRINTER").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Path_printerTextEdit.Focus()

            ElseIf RepresentanteTextEdit.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "REPRESENTANTE").Longitud Then
                XtraMessageBox.Show(String.Format("El Representante debe de tener como máximo {0} carácteres.", pListObjT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "REPRESENTANTE").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                RepresentanteTextEdit.Focus()

            ElseIf TelefonoTextEdit.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "TELEFONO").Longitud Then
                XtraMessageBox.Show(String.Format("El Nombre debe de tener como máximo {0} carácteres.", pListObjT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "TELEFONO").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TelefonoTextEdit.Focus()

            ElseIf Razon_socialTextEdit.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "RAZON_SOCIAL").Longitud Then
                XtraMessageBox.Show(String.Format("La razón social debe de tener como máximo {0} carácteres.", pListObjT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "RAZON_SOCIAL").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Razon_socialTextEdit.Focus()

            ElseIf CodigoTextEdit.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "CODIGO").Longitud Then
                XtraMessageBox.Show(String.Format("El Código debe de tener como máximo {0} carácteres.", pListObjT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "CODIGO").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                CodigoTextEdit.Focus()

            ElseIf ClaveTextEdit.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "CLAVE").Longitud Then
                XtraMessageBox.Show(String.Format("La Clave debe de tener como máximo {0} carácteres.", pListObjT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "CLAVE").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ClaveTextEdit.Focus()

            Else
                Datos_Correctos = True
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Actualizar()

        Actualizar = False

        Try

            If Datos_Correctos() Then

                Empresa.Nombre = NombreTextEdit.Text
                Empresa.Direccion = DireccionTextEdit.Text
                Empresa.Telefono = TelefonoTextEdit.Text
                Empresa.Email = EmailTextEdit.Text
                Empresa.Razon_social = Razon_socialTextEdit.Text
                Empresa.Representante = RepresentanteTextEdit.Text
                Empresa.Corr_cod_barra = Corr_cod_barraSpinEdit.Text
                Empresa.Path_printer = Path_printerTextEdit.Text
                Empresa.Codigo = CodigoTextEdit.Text
                Empresa.Clave = clsPublic.Encriptar(ClaveTextEdit.Text)
                Empresa.Puerto_escaner = Puerto_escanerSpinEdit.Text
                Empresa.Activo = chkActivo.Checked
                Empresa.ClienteRapido = chkClienteRapido.Checked
                Empresa.Operador_logistico = chkOperadorlogistico.Checked
                Empresa.Control_presentaciones = chkControlpresentaciones.Checked
                Empresa.Anulaciones_por_supervisor = chkAnulacionesporsupervisor.Checked
                Empresa.Codigo_automatico = chkCodigoAutomatico.Checked

                If picFoto.Image IsNot Nothing Then
                    Empresa.Imagen = ImageToByteArray(picFoto.Image)
                End If

                Empresa.IdMotivoAjusteInventario = cbxMotivosAjuste.EditValue
                Empresa.User_mod = AP.UsuarioAp.IdUsuario
                Empresa.Fec_mod = Now
                Empresa.Duracionclave = IIf(cbxCaducidad.CheckState = CheckState.Checked, 0, CInt(SpinEditDuracion.Value))
                Empresa.Duracionclavetemporal = CInt(SpinEditDuracionTemporal.Value)
                Empresa.Intento = IIf(cbxBloqueo.CheckState = CheckState.Checked, CInt(SpinEditIntentos.Value), 0)
                Empresa.buscar_actualizacion_hh = chkBuscarActualizacionHH.Checked
                Empresa.Version_BD = txtVersionBD.Text.Trim
                Empresa.AWS_Token = txtAWSToken.Text.Trim

                Actualizar = clsLnEmpresa.Actualizar(Empresa) > 0

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        mnuActualizar.Enabled = False

        If Actualizar() Then
            XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            If Listar IsNot Nothing Then
                Listar.Invoke()
            End If
            DialogResult = DialogResult.OK
            Close()
        End If

        mnuActualizar.Enabled = True

    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try

            mnuEliminar.Enabled = False

            If XtraMessageBox.Show("¿Desactivar la Empresa?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) Then

                If clsLnEmpresa.Eliminar(Empresa) > 0 Then

                    XtraMessageBox.Show("Se ha eliminado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    If Listar IsNot Nothing Then
                        Listar.Invoke()
                    End If

                    DialogResult = DialogResult.OK

                    Close()

                End If

            End If

            mnuEliminar.Enabled = True

        Catch ex As Exception
            If ex.HResult = -2146233088 Then TablasRelacionadas("empresa", IdEmpresaSpinEdit.Text)

            mnuEliminar.Enabled = True

        End Try

    End Sub

    Private Sub cbxCaducidad_CheckedChanged(sender As Object, e As EventArgs) Handles cbxCaducidad.CheckedChanged
        SpinEditDuracion.Enabled = Not cbxCaducidad.Checked
        SpinEditDuracionTemporal.Enabled = SpinEditDuracion.Enabled
    End Sub

    Private Sub cbxBloqueo_EditValueChanged(sender As Object, e As EventArgs) Handles cbxBloqueo.EditValueChanged
        SpinEditIntentos.Enabled = cbxBloqueo.Checked
    End Sub

    Private Sub chkMostrarContraseña_CheckedChanged(sender As Object, e As EventArgs) Handles chkMostrarContraseña.CheckedChanged

        If chkMostrarContraseña.Checked Then
            ClaveTextEdit.Properties.PasswordChar = ""
        Else
            ClaveTextEdit.Properties.PasswordChar = "*"
        End If

    End Sub

    Private Sub frmEmpresa_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Close()
        End If
    End Sub

    Private Sub GroupControl3_Paint(sender As Object, e As PaintEventArgs) Handles GroupControl3.Paint

    End Sub
End Class