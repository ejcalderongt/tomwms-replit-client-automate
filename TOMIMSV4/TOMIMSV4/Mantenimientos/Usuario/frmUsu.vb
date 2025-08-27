Imports System.Drawing.Imaging
Imports System.IO
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Camera

Public Class frmUsu

    Public Usuario As New clsBeUsuario
    Public selectedValuesRol As New clsBeRol
    Public selectedValuesusuarioSuperior As New clsBeUsuario
    Public ClaveAnterior As String
    Public Delegate Sub Listar_Usuarios()
    Public Property InvokeListarUsuarios As Listar_Usuarios

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
        EditarClave = 3
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

    Private Sub Listar_Empresas()
        If Not IMS.Listar_Empresas(cbxEmpresa) Then
            XtraMessageBox.Show("No hay empresas definidas para la aplicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub frmUsu_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            TelefonoTextEdit.Properties.Mask.EditMask = "\((\d{3})\) (\d{4})-(\d{4})"
            TelefonoTextEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
            '#GT30032023: no permitir que se habilite
            chkSistema.Enabled = False

            Init_DT_Resoluciones_LP()

            AP.Listar_Bodegas_By_Usuario(cmbBodega)

            Select Case Modo

                Case TipoTrans.Nuevo

                    Usuario.IdUsuario = clsLnUsuario.MaxID()

                    IdUsuarioSpinEdit.Value = Usuario.IdUsuario
                    IdUsuarioSpinEdit.Enabled = False

                    User_agrTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_agrDateEdit.Text = Now

                    User_modTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_modDateEdit.Text = Now
                    Listar_Empresas()

                    'Dim LocalHostName As String = Dns.GetHostName
                    'Dim LocalIP As IPHostEntry = Dns.GetHostEntry(LocalHostName)
                    'TelefonoTextEdit.Text = LocalIP.AddressList(LocalIP.AddressList.Count -1).ToString                    

                    If OpcionesMenu IsNot Nothing Then
                        mnuGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False

                    NombresTextEdit.Focus()

                Case TipoTrans.Editar

                    clsLnUsuario.Obtener(Usuario)

                    IdUsuarioSpinEdit.Text = Usuario.IdUsuario
                    NombresTextEdit.Text = Usuario.Nombres
                    ApellidosTextEdit.Text = Usuario.Apellidos
                    CedulaTextEdit.Text = Usuario.Cedula
                    DireccionTextEdit.Text = Usuario.Direccion
                    TelefonoTextEdit.Text = Usuario.Telefono
                    EmailTextEdit.Text = Usuario.Email

                    Try

                        CodigoTextEdit.Text = clsPublic.Desencriptar(Usuario.Codigo)
                        ClaveTextEdit.Text = clsPublic.Desencriptar(Usuario.Clave)
                        ConfirmarClaveTextEdit.Text = clsPublic.Desencriptar(Usuario.Clave)

                        If Usuario.Clave_autorizacion <> "" Then
                            txtClaveAutoriza.Text = clsPublic.Desencriptar(Usuario.Clave_autorizacion)
                        End If

                    Catch ex As Exception
                        XtraMessageBox.Show(ex.Message)
                    End Try

                    ClaveAnterior = ClaveTextEdit.Text                          'Comprobacion de Cadena
                    Ultimo_loginDateEdit.Text = Usuario.Ultimo_login
                    chkActivo.Checked = Usuario.Activo
                    chkSistema.Checked = Usuario.Sistema
                    User_agrTextEdit.Text = Usuario.User_agr
                    Fec_agrDateEdit.Text = Usuario.Fec_agr
                    User_modTextEdit.Text = Usuario.User_mod
                    Fec_modDateEdit.Text = Usuario.Fec_mod

                    If Usuario.Foto IsNot Nothing Then
                        picFoto.Image = ByteArrayToImage(Usuario.Foto)
                    End If

                    '#GT17072025: SAPB1
                    txtUsuarioSap.Text = Usuario.Usuario_sap_b1
                    txtClaveSap.Text = Usuario.Clave_sap_b1

                    Listar_Empresas()

                    Cargar_Resoluciones_LP()

                    If Usuario.Sistema Then
                        mnuGuardar.Enabled = False
                        mnuActualizar.Enabled = False
                        mnuEliminar.Enabled = False
                        cmdImprimirCarnet.Enabled = False
                    Else

                        cbxEmpresa.EditValue = Usuario.IdEmpresa

                        mnuGuardar.Enabled = False

                        If OpcionesMenu IsNot Nothing Then
                            mnuActualizar.Enabled = OpcionesMenu.Modificar
                            mnuEliminar.Enabled = OpcionesMenu.Eliminar
                        End If

                    End If

                    Listar_Permisos_Bodega()

                    NombresTextEdit.Focus()

                Case TipoTrans.EditarClave

                    clsLnUsuario.Obtener(Usuario)

                    IdUsuarioSpinEdit.Text = Usuario.IdUsuario
                    NombresTextEdit.Text = Usuario.Nombres
                    NombresTextEdit.Enabled = False
                    ApellidosTextEdit.Text = Usuario.Apellidos
                    ApellidosTextEdit.Enabled = False
                    CedulaTextEdit.Text = Usuario.Cedula
                    CedulaTextEdit.Enabled = False
                    DireccionTextEdit.Text = Usuario.Direccion
                    DireccionTextEdit.Enabled = False
                    TelefonoTextEdit.Text = Usuario.Telefono
                    TelefonoTextEdit.Enabled = False
                    EmailTextEdit.Text = Usuario.Email
                    EmailTextEdit.Enabled = False

                    Try

                        CodigoTextEdit.Text = clsPublic.Desencriptar(Usuario.Codigo)
                        CodigoTextEdit.Enabled = False
                        ClaveTextEdit.Text = clsPublic.Desencriptar(Usuario.Clave)
                        ConfirmarClaveTextEdit.Text = clsPublic.Desencriptar(Usuario.Clave)

                        If Usuario.Clave_autorizacion <> "" Then
                            txtClaveAutoriza.Text = clsPublic.Desencriptar(Usuario.Clave_autorizacion)
                        End If

                    Catch ex As Exception
                        XtraMessageBox.Show(ex.Message)
                    End Try

                    ClaveAnterior = ClaveTextEdit.Text                          'Comprobacion de Cadena
                    Ultimo_loginDateEdit.Text = Usuario.Ultimo_login
                    Ultimo_loginDateEdit.Enabled = False
                    chkActivo.Checked = Usuario.Activo
                    chkActivo.Enabled = False
                    chkSistema.Checked = Usuario.Sistema
                    User_agrTextEdit.Text = Usuario.User_agr
                    Fec_agrDateEdit.Text = Usuario.Fec_agr
                    User_modTextEdit.Text = Usuario.User_mod
                    Fec_modDateEdit.Text = Usuario.Fec_mod

                    If Usuario.Foto IsNot Nothing Then
                        picFoto.Image = ByteArrayToImage(Usuario.Foto)
                    End If

                    If Usuario.Sistema Then
                        mnuGuardar.Enabled = False
                        mnuActualizar.Enabled = False
                        mnuEliminar.Enabled = False
                        cmdImprimirCarnet.Enabled = False
                    Else
                        Listar_Empresas()
                        cbxEmpresa.EditValue = Usuario.IdEmpresa

                        mnuGuardar.Enabled = False
                        mnuActualizar.Enabled = True
                        mnuEliminar.Enabled = False
                        cmdImprimirCarnet.Enabled = False

                        cbxEmpresa.Enabled = False

                        XtraTabControl.TabPages(1).Visible = False

                    End If

            End Select

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

            Usuario.IdEmpresa = cbxEmpresa.EditValue
            Usuario.Nombres = NombresTextEdit.Text
            Usuario.Apellidos = ApellidosTextEdit.Text
            Usuario.Cedula = CedulaTextEdit.Text
            Usuario.Direccion = DireccionTextEdit.Text
            Usuario.Telefono = TelefonoTextEdit.Text
            Usuario.Email = EmailTextEdit.Text
            Usuario.Codigo = clsPublic.Encriptar(CodigoTextEdit.Text)
            Usuario.Clave = clsPublic.Encriptar(ClaveTextEdit.Text)
            Usuario.Clave_autorizacion = clsPublic.Encriptar(txtClaveAutoriza.Text)
            'Usuario.Ultimo_login = Ultimo_loginDateEdit.Text
            Usuario.Activo = chkActivo.Checked
            Usuario.Sistema = chkSistema.Checked
            Usuario.User_agr = AP.UsuarioAp.IdUsuario
            Usuario.Fec_agr = Now
            Usuario.User_mod = AP.UsuarioAp.IdUsuario
            Usuario.Fec_mod = Now
            '#GT17072025: aqui asigno los input SAPB1
            Usuario.Usuario_sap_b1 = txtUsuarioSap.Text
            Usuario.Clave_sap_b1 = txtClaveSap.Text

            If picFoto.Image IsNot Nothing Then
                Usuario.Foto = ImageToByteArray(picFoto.Image)
            End If

            Guardar = clsLnUsuario.Insertar(Usuario) > 0

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Function GuardarPermisos() As Boolean

        Dim objUsuarioBodega As clsBeUsuario_bodega
        Dim stado As Boolean = True

        For I = 0 To Dgrid.RowCount - 1
            If (Dgrid.Item(4, I).Value) Then 'GUARDA UNICAMENTE LOS PERMISOS ACTIVADOS
                Dim rol As clsBeRol = TryCast(Dgrid.Item(5, I).Value, clsBeRol)
                Dim usuarioSuperior As clsBeUsuario = TryCast(Dgrid.Item(6, I).Value, clsBeUsuario)
                objUsuarioBodega = New clsBeUsuario_bodega()
                objUsuarioBodega.IdUsuarioBodega = clsLnUsuario_bodega.MaxID()
                objUsuarioBodega.IdBodega = Dgrid.Item(1, I).Value
                objUsuarioBodega.IdUsuario = Integer.Parse(IdUsuarioSpinEdit.Text)
                objUsuarioBodega.IdUsuarioSuperior = usuarioSuperior.IdUsuario
                objUsuarioBodega.IdRol = rol.IdRol
                objUsuarioBodega.Activo = Dgrid.Item(4, I).Value
                objUsuarioBodega.User_agr = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                objUsuarioBodega.Fec_agr = Now
                objUsuarioBodega.User_mod = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                objUsuarioBodega.Fec_mod = Now
                stado = stado And (clsLnUsuario_bodega.Insertar(objUsuarioBodega) > 0)
            End If
        Next

        Return stado

    End Function

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            If Datos_Correctos() Then

                Usuario.Nombres = NombresTextEdit.Text
                Usuario.Apellidos = ApellidosTextEdit.Text
                Usuario.Cedula = CedulaTextEdit.Text
                Usuario.Direccion = DireccionTextEdit.Text
                Usuario.Telefono = TelefonoTextEdit.Text
                Usuario.Email = EmailTextEdit.Text
                Usuario.Codigo = clsPublic.Encriptar(CodigoTextEdit.Text)
                Usuario.Clave = clsPublic.Encriptar(ClaveTextEdit.Text)
                Usuario.Clave_autorizacion = clsPublic.Encriptar(txtClaveAutoriza.Text)
                Usuario.Activo = chkActivo.Checked
                Usuario.Sistema = chkSistema.Checked
                Usuario.User_mod = AP.UsuarioAp.IdUsuario
                Usuario.Fec_mod = Now
                '#GT17072025: datos SAPB1 para update
                Usuario.Usuario_sap_b1 = txtUsuarioSap.Text
                Usuario.Clave_sap_b1 = txtClaveSap.Text

                If picFoto.Image IsNot Nothing Then
                    Usuario.Foto = ImageToByteArray(picFoto.Image)
                End If

                Actualizar = clsLnUsuario.Actualizar(Usuario) > 0

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

    Private Function ActualizarPermisos() As Boolean

        Try

            Dim objUsuarioBodega As clsBeUsuario_bodega
            Dim clsLnUsuario_bodega As New clsLnUsuario_bodega()
            Dim stado As Boolean = True

            Dgrid.EndEdit()

            For I = 0 To Dgrid.RowCount - 1

                Dim objUsuarioBodegaanterior As New clsBeUsuario_bodega
                Dim vIdUsuarioBodAnt As Integer = Dgrid.Item(7, I).Value
                objUsuarioBodegaanterior.IdUsuarioBodega = vIdUsuarioBodAnt
                Dim rol As clsBeRol = TryCast(Dgrid.Item(5, I).Value, clsBeRol)
                Dim vIdUsuarioSuperior As Integer = Dgrid.Item(6, I).Value
                Dim usuarioSuperior As New clsBeUsuario
                usuarioSuperior.IdUsuario = vIdUsuarioSuperior

                objUsuarioBodega = New clsBeUsuario_bodega()
                objUsuarioBodega.IdBodega = Dgrid.Item(1, I).Value
                objUsuarioBodega.IdUsuario = Integer.Parse(IdUsuarioSpinEdit.Text)
                objUsuarioBodega.IdUsuarioSuperior = vIdUsuarioSuperior
                objUsuarioBodega.IdRol = rol.IdRol
                objUsuarioBodega.Activo = Dgrid.Item(4, I).Value
                objUsuarioBodega.User_agr = IIf(objUsuarioBodegaanterior.IdUsuarioBodega = 0, String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos), objUsuarioBodegaanterior.User_agr)
                objUsuarioBodega.Fec_agr = IIf(objUsuarioBodegaanterior.IdUsuarioBodega = 0, Now, objUsuarioBodegaanterior.Fec_agr)
                objUsuarioBodega.User_mod = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                objUsuarioBodega.Fec_mod = Now

                If (objUsuarioBodegaanterior.IdUsuarioBodega = 0) Then
                    If (Dgrid.Item(4, I).Value) Then
                        objUsuarioBodega.IdUsuarioBodega = clsLnUsuario_bodega.MaxID() + 1
                        stado = stado And (clsLnUsuario_bodega.Insertar(objUsuarioBodega) > 0)
                    End If
                Else
                    If (Dgrid.Item(4, I).Value) Then
                        objUsuarioBodega.IdUsuarioBodega = objUsuarioBodegaanterior.IdUsuarioBodega
                        stado = stado And (clsLnUsuario_bodega.Actualizar(objUsuarioBodega) > 0)
                    Else
                        objUsuarioBodega.IdUsuarioBodega = objUsuarioBodegaanterior.IdUsuarioBodega
                        stado = stado And (clsLnUsuario_bodega.Eliminar(objUsuarioBodega) > 0)
                    End If
                End If

            Next

            Return stado

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

            If Guardar() Then

                If GuardarPermisos() Then
                    XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Else
                    XtraMessageBox.Show("No se guardaron los permisos de Usuario", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

                InvokeListarUsuarios.Invoke
                Close()

            End If

        End If

    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        Try

            If Datos_Correctos() Then

                If Actualizar() Then

                    If ActualizarPermisos() Then
                        XtraMessageBox.Show("Se Actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        XtraMessageBox.Show("No se guardaron los permisos de Usuario", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If

                    If Not InvokeListarUsuarios Is Nothing Then
                        InvokeListarUsuarios.Invoke
                    End If

                    Close()

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Function Clave_Cumple_Politica() As Boolean

        Clave_Cumple_Politica = False

        Dim miclave As String = ClaveTextEdit.Text.Trim
        Dim miusuario As String = CodigoTextEdit.Text.Trim

        If (miusuario.Length >= 3 And miusuario.Length <= 10) Then

            If (miclave.Length >= 8) Then                                                               'LONGITUD DE CLAVE > 7

                If Not (miclave.Contains(miusuario) Or miclave.Contains(StrReverse(miusuario))) Then    'NO CONTIENE EL NOMBRE DE USUARIO AL DERECHO Y AL REVES

                    If Not (ContainsServerName(clsBD.Instancia.Server, miclave)) Then

                        If Not ContainsWorldUnatorizeth(miclave) Then

                            If CountDigit(miclave) > 1 And CountLetter(miclave) > 1 Then

                                If (Modo = TipoTrans.Nuevo) Then Return True

                                If (CheckKey(ClaveAnterior, miclave, 3)) Then
                                    Return True
                                Else
                                    XtraMessageBox.Show("La clave nueva debe de tener al menos 3 caracteres de ferentes a la clave anterior", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            Else
                                XtraMessageBox.Show("La Clave debe de tener cuando menos dos numero[0-9] y dos letras[a-z,A-Z]", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        Else
                            XtraMessageBox.Show("La Clave no debe contener datos tales como: 'welcome1', 'database1', 'account1', 'user1234', 'password1', 'oracle123', 'computer1', 'abcdefg1', 'change_on_install' ", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    Else
                        XtraMessageBox.Show("La Clave no debe contener datos de la instancia del Servidor", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    XtraMessageBox.Show("La Clave no debe contener el Nombre de Usario ni al derecho ni al reves ", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else
                XtraMessageBox.Show("La clave debe ser mayor a 8 Caracteres ", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Else
            XtraMessageBox.Show("El nombre de usuario debe tener un tamano de 3 a 10 Caracteres", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        Return False

    End Function

    Private Function CheckKey(ByVal ClaveAnterior As String, ByVal ClaveNueva As String, ByVal longitud As Integer) As Boolean
        Dim Clavegenerada As String = Replace(UCase(ClaveNueva), UCase(ClaveAnterior), "")                'quitar cadena anterior de cadena orginal

        If (Clavegenerada.Length > longitud) Then
            Return True
        End If

        Return False
    End Function

    Private Function CountDigit(ByVal World As String) As Integer
        Dim contador As Integer = 0
        For it = 0 To World.Length - 1
            If Char.IsDigit(World.ElementAt(it)) Then
                contador += 1
            End If
        Next
        Return contador
    End Function

    Private Function CountLetter(ByVal World As String) As Integer
        Dim contador As Integer = 0
        For it = 0 To World.Length - 1
            If Char.IsLetter(World.ElementAt(it)) Then
                contador += 1
            End If
        Next
        Return contador
    End Function

    Private Function ContainsWorldUnatorizeth(ByVal miClave As String) As Boolean
        Dim PalabrasNoPermitidas() As String = {"welcome1", "database1", "account1", "user1234", "password1", "oracle123", "computer1", "abcdefg1", "change_on_install", "oracle", "sql"}
        Dim ResultadoBusqueda As Boolean = False
        For i As Integer = 0 To PalabrasNoPermitidas.Length - 1
            If UCase(miClave).Contains(UCase(PalabrasNoPermitidas(i))) Then
                ResultadoBusqueda = True
            End If
        Next
        Return ResultadoBusqueda
    End Function

    Private Function ContainsServerName(ByRef ServerInstance As String, ByVal clave As String) As Boolean
        Dim ResultadoBusqueda As Boolean = False

        Dim ServidorParam() As String = Split(ServerInstance, Chr(92))

        For i As Integer = 0 To ServidorParam.Length - 1
            If ServidorParam(i).Contains(clave) Then
                ResultadoBusqueda = True
            End If
        Next

        Return ResultadoBusqueda
    End Function

    Private Function Datos_Correctos() As Boolean

        Datos_Correctos = False

        Try

            If NombresTextEdit.Text.Trim = "" Then
                XtraMessageBox.Show("Ingrese nombre", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                NombresTextEdit.Focus()
            ElseIf CodigoTextEdit.Text.Trim = "" Then
                XtraMessageBox.Show("Ingrese código de acceso del usuario", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                CodigoTextEdit.Focus()
            ElseIf ClaveTextEdit.Text.Trim = "" Then
                XtraMessageBox.Show("Ingrese clave de usuario", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                ClaveTextEdit.Focus()
            ElseIf ClaveTextEdit.Text.Trim <> ConfirmarClaveTextEdit.Text.Trim Then
                XtraMessageBox.Show("Las claves no coinciden", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                XtraTabControl.SelectedTabPageIndex = 0
                ConfirmarClaveTextEdit.Focus()
            ElseIf Not Clave_Autorizacion_Correcta() Then
                'Mensaje se dió dentro de función
            ElseIf AP.Exigir_Politica_Contraseñas AndAlso Not Clave_Cumple_Politica() Then
                'Mensaje se dió dentro de función
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

    Private Function Clave_Autorizacion_Correcta() As Boolean

        Clave_Autorizacion_Correcta = False

        Try

            If Dgrid.Rows.Count > 0 Then

                For Each oRow As DataGridViewRow In Dgrid.Rows

                    If oRow.Cells("ColClave").Value = True AndAlso oRow.Cells("ColAsignado").Value = True Then
                        If txtClaveAutoriza.Text.Trim = "" Then
                            XtraMessageBox.Show("Ingrese clave de autorización", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            CodigoTextEdit.Focus()
                            Exit Function
                        ElseIf txtReptClaveAuto.Text.Trim = "" Then
                            XtraMessageBox.Show("Confirme clave de autorización", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            ClaveTextEdit.Focus()
                            Exit Function
                        ElseIf txtClaveAutoriza.Text.Trim <> txtReptClaveAuto.Text.Trim Then
                            XtraMessageBox.Show("Las claves de autorización no coinciden", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            txtClaveAutoriza.Focus()
                            Exit Function
                        End If
                    End If

                Next

            End If

            Clave_Autorizacion_Correcta = True

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

        Try

            If XtraMessageBox.Show("¿Desactivar el usuario?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                If clsLnUsuario.Eliminar(Usuario) > 0 Then
                    XtraMessageBox.Show("Se ha eliminado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    InvokeListarUsuarios.Invoke
                    Close()
                End If

            End If

        Catch ex As Exception

            If ex.HResult = -2146233088 OrElse ex.HResult = -2146232060 Then
                TablasRelacionadas("Usuario", Usuario.IdUsuario)
            Else
                XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
            End If
        End Try

    End Sub

    Private Sub Listar_Permisos_Bodega()

        Dim DT As New DataTable

        Dgrid.Columns.Clear()

        Try

            Dim column As New DataGridViewTextBoxColumn() With {.Name = "ColIdEmpresa", .HeaderText = "Id Empresa", .Visible = True}
            '0
            DT.Columns.Add("ColIdEmpresa")

            Dgrid.Columns.Add(column)

            column = New DataGridViewTextBoxColumn
            column.Name = "ColIdBodega"
            column.HeaderText = "IdBodega"
            column.Visible = True
            '1
            DT.Columns.Add("ColIdBodega")

            Dgrid.Columns.Add(column)

            column = New DataGridViewTextBoxColumn
            column.Name = "ColNomEmpresa"
            column.HeaderText = "Empresa"
            column.Visible = True
            '2
            DT.Columns.Add("ColNomEmpresa")

            Dgrid.Columns.Add(column)

            column = New DataGridViewTextBoxColumn
            column.Name = "ColNomBodega"
            column.HeaderText = "Bodega"
            column.Visible = True
            '3
            DT.Columns.Add("ColNomBodega")

            Dgrid.Columns.Add(column)

            '4
            Dim chk As New DataGridViewCheckBoxColumn() With {.Name = "ColAsignado", .HeaderText = "Asignar", .Visible = True}
            DT.Columns.Add("ColAsignado")
            Dgrid.Columns.Add(chk)

            '5
            Dim listaRolesxEmpresa As New List(Of clsBeRol)
            listaRolesxEmpresa = clsLnRol.Get_All_By_Empresa(cbxEmpresa.EditValue)

            Dim colRol As New DataGridViewComboBoxColumn() With {.DataSource = listaRolesxEmpresa.ToArray(), .Name = "ColRol", .HeaderText = "Rol", .Visible = True}
            DT.Columns.Add("ColRol")
            Dgrid.Columns.Add(colRol)

            If listaRolesxEmpresa.Count > 0 Then
                selectedValuesRol = listaRolesxEmpresa(0)
            Else
                XtraMessageBox.Show("No hay roles definidos para esta empresa,Desea definir un rol", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

            '6
            Dim lista2 As New List(Of clsBeUsuario)
            lista2 = clsLnUsuario.GetAllByIdEmpresa(cbxEmpresa.EditValue)

            Dim columnusuariosuperior As New DataGridViewComboBoxColumn() With {.DataSource = lista2, .Name = "ColIdSuperior", .HeaderText = "Usuario Superior", .Visible = True, .ValueMember = "IdUsuario", .DisplayMember = "Nombres"}
            DT.Columns.Add("ColIdSuperior")
            Dgrid.Columns.Add(columnusuariosuperior)

            If lista2.Count > 0 Then
                selectedValuesusuarioSuperior = lista2(0)
            Else
                XtraMessageBox.Show(String.Format("No existe un usuario en la Bodega {0}  para Asignarlo como Usuario Superior", cbxEmpresa.Text), Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            Dim ColIdUsuarioBodega As New DataGridViewTextBoxColumn() With {.Name = "ColIdUsuarioBodega", .HeaderText = "Id Usuario Bodega", .Visible = True}
            '7
            DT.Columns.Add("ColIdUsuarioBodega")

            Dgrid.Columns.Add(ColIdUsuarioBodega)

            '8
            Dim chkClaveA As New DataGridViewCheckBoxColumn() With {.Name = "ColClave", .HeaderText = "Registrar Clave Autorización", .Visible = True}
            DT.Columns.Add("ColClave")
            Dgrid.Columns.Add(chkClaveA)

            Dim lBodegas As New List(Of clsBeBodega)

            lBodegas = clsLnBodega.Get_All_By_IdEmpresa(cbxEmpresa.EditValue)

            Dim IdEmpresa, IdBodega, NomEmpresa, NomBodega As String

            For Each Bod As clsBeBodega In lBodegas

                IdEmpresa = Bod.IdEmpresa
                IdBodega = Bod.IdBodega
                NomEmpresa = Bod.Empresa.Nombre
                NomBodega = Bod.Nombre

                Dim objfilaActual As New clsBeUsuario_bodega()

                Dim ver As Boolean = False

                If listaRolesxEmpresa.Count > 0 Then

                    objfilaActual.IdUsuario = Usuario.IdUsuario
                    objfilaActual.IdBodega = IdBodega

                    clsLnUsuario_bodega.obtenerPermiso(objfilaActual)

                    If lista2.Count > 0 Then
                        If (objfilaActual.IdUsuarioBodega <> 0) Then
                            selectedValuesRol = listaRolesxEmpresa.Find(Function(x) x.IdRol = objfilaActual.IdRol)
                            selectedValuesusuarioSuperior = lista2.Find(Function(x) x.IdUsuario = objfilaActual.IdUsuarioSuperior)
                            ver = objfilaActual.Activo
                        End If

                        '#CKFK 20210819 Agregué validacion para cuado el Usuario superior es Nulo
                        If selectedValuesusuarioSuperior Is Nothing Then
                            Dgrid.Rows.Add(IdEmpresa, IdBodega, NomEmpresa, NomBodega, ver, selectedValuesRol, selectedValuesRol, objfilaActual.IdUsuarioBodega, selectedValuesRol.Registrar_clave_autorizacion)
                        Else
                            Dgrid.Rows.Add(IdEmpresa, IdBodega, NomEmpresa, NomBodega, ver, selectedValuesRol, selectedValuesusuarioSuperior.IdUsuario, objfilaActual.IdUsuarioBodega, selectedValuesRol.Registrar_clave_autorizacion)
                        End If

                        If ver And selectedValuesRol.Registrar_clave_autorizacion Then
                            txtClaveAutoriza.Visible = True
                            txtReptClaveAuto.Visible = True
                            lblClaveAuto.Visible = True
                            lblReptClaAut.Visible = True
                        End If

                    Else
                        If (objfilaActual.IdUsuarioBodega <> 0) Then
                            selectedValuesRol = listaRolesxEmpresa.Find(Function(x) x.IdRol = objfilaActual.IdRol)
                            ver = objfilaActual.Activo
                        End If
                        Dgrid.Rows.Add(IdEmpresa, IdBodega, NomEmpresa, NomBodega, ver, selectedValuesRol, "", objfilaActual, selectedValuesRol.Registrar_clave_autorizacion)

                        If ver And selectedValuesRol.Registrar_clave_autorizacion Then
                            txtClaveAutoriza.Visible = True
                            txtReptClaveAuto.Visible = True
                            lblClaveAuto.Visible = True
                            lblReptClaAut.Visible = True
                        End If

                    End If

                Else
                    Dgrid.Rows.Add(IdEmpresa, IdBodega, NomEmpresa, NomBodega, ver, "", "", objfilaActual, "")
                End If

            Next

            'For i As Integer = 0 To DT1.Rows.Count - 1

            '    IdEmpresa = DT1.Rows(i).Item("IdEmpresa")
            '    IdBodega = DT1.Rows(i).Item("IdBodega")
            '    NomEmpresa = DT1.Rows(i).Item("NomEmpresa")
            '    NomBodega = DT1.Rows(i).Item("NomBodega")

            '    Dim objfilaActual As New clsBeUsuario_bodega()
            '    Dim ver As Boolean = False

            '    If listaRolesxEmpresa.Count > 0 Then

            '        objfilaActual.IdUsuario = Usuario.IdUsuario
            '        objfilaActual.IdBodega = IdBodega

            '        clsLnUsuario_bodega.obtenerPermiso(objfilaActual)

            '        If lista2.Count > 0 Then
            '            If (objfilaActual.IdUsuarioBodega <> 0) Then
            '                selectedValuesRol = listaRolesxEmpresa.Find(Function(x) x.IdRol = objfilaActual.IdRol)
            '                selectedValuesusuarioSuperior =lista2.Find(Function (x) x.IdUsuario = objfilaActual.IdUsuarioSuperior)
            '                ver = objfilaActual.Activo
            '            End If

            '            Dgrid.Rows.Add(IdEmpresa, IdBodega, NomEmpresa, NomBodega, ver, selectedValuesRol, selectedValuesusuarioSuperior.IdUsuario, objfilaActual.IdUsuarioBodega)
            '        Else
            '            If (objfilaActual.IdUsuarioBodega <> 0) Then
            '                selectedValuesRol = listaRolesxEmpresa.Find(Function(x) x.IdRol = objfilaActual.IdRol)
            '                ver = objfilaActual.Activo
            '            End If
            '            Dgrid.Rows.Add(IdEmpresa, IdBodega, NomEmpresa, NomBodega, ver, selectedValuesRol, "", objfilaActual)
            '        End If

            '    Else
            '        Dgrid.Rows.Add(IdEmpresa, IdBodega, NomEmpresa, NomBodega, ver, "", "", objfilaActual)
            '    End If

            'Next

            Dgrid.AutoGenerateColumns = False
            Dgrid.AllowUserToAddRows = False

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Dgrid_CellParsing(sender As Object, e As DataGridViewCellParsingEventArgs) Handles Dgrid.CellParsing
        If (e.ColumnIndex = 5) Then
            e.Value = selectedValuesRol
        End If

        If (e.ColumnIndex = 6) Then
            e.Value = selectedValuesusuarioSuperior
        End If

        e.ParsingApplied = True
    End Sub

    Private Sub Dgrid_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles Dgrid.EditingControlShowing

        Try

            Dim cb As System.Windows.Forms.ComboBox = TryCast(e.Control, System.Windows.Forms.ComboBox)

            If cb IsNot Nothing Then
                RemoveHandler cb.SelectedIndexChanged, AddressOf cb_SelectedIndexChanged
                AddHandler cb.SelectedIndexChanged, AddressOf cb_SelectedIndexChanged
            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub cb_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim comboBox As System.Windows.Forms.ComboBox = TryCast(sender, System.Windows.Forms.ComboBox)
        Dim rol As clsBeRol = TryCast(comboBox.SelectedItem, clsBeRol)

        If rol IsNot Nothing Then
            selectedValuesRol = comboBox.SelectedItem
        Else
            selectedValuesusuarioSuperior = comboBox.SelectedItem
        End If
    End Sub

    'Private Function buscarRol(lista As ArrayList, p2 As Integer) As clsBeRol
    '    For I = 0 To lista.Count - 1
    '        Dim comparar As clsBeRol = lista(I)
    '        If (comparar.IdRol = p2) Then
    '            Return comparar
    '        End If
    '    Next
    '    Return Nothing
    'End Function

    'Private Function buscarUsuario(lista As ArrayList, p2 As Integer) As clsBeUsuario
    '    For I = 0 To lista.Count - 1
    '        Dim comparar As clsBeUsuario = lista(I)
    '        If (comparar.IdUsuario = p2) Then
    '            Return comparar
    '        End If
    '    Next
    '    Return Nothing
    'End Function

    Private Sub cmbEmpresa_SelectedValueChanged(sender As Object, e As EventArgs)
        If (cbxEmpresa.ItemIndex > 0) Then
            Listar_Permisos_Bodega()
        End If
    End Sub

    Private Sub PictureEdit1_DoubleClick(sender As Object, e As EventArgs) Handles picFoto.DoubleClick

        Try

            Dim d As New TakePictureDialog()

            If d.ShowDialog() = DialogResult.OK Then
                picFoto.Image = d.Image
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

    Public Function ByteArrayToImage(ByVal byteArrayIn As Byte()) As Image
        Dim ms As New MemoryStream(byteArrayIn)
        Return Image.FromStream(ms)
    End Function

    Public Function ImageToByteArray(ByVal imageIn As Image) As Byte()
        Dim ms As New MemoryStream()
        imageIn.Save(ms, ImageFormat.Jpeg)
        Return ms.ToArray()
    End Function

    Private Sub Dgrid_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles Dgrid.CellValueChanged

        Try

            If Dgrid.RowCount > 0 Then

                Dim row As DataGridViewRow = Dgrid.Rows(e.RowIndex)
                Dim vAsignar As Boolean = IIf(IsDBNull(row.Cells("ColAsignado").Value), False, row.Cells("ColAsignado").Value)
                Dim vPermiteClave As Boolean = IIf(IsDBNull(row.Cells("ColClave").Value), False, row.Cells("ColClave").Value)

                If e.ColumnIndex = 4 And e.ColumnIndex = 8 Then
                    Dgrid.CommitEdit(DataGridViewDataErrorContexts.CurrentCellChange)
                End If

                If vAsignar And vPermiteClave Then
                    txtClaveAutoriza.Visible = True
                    txtReptClaveAuto.Visible = True
                    lblClaveAuto.Visible = True
                    lblReptClaAut.Visible = True
                Else
                    txtClaveAutoriza.Visible = False
                    txtReptClaveAuto.Visible = False
                    lblClaveAuto.Visible = False
                    lblReptClaAut.Visible = False
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

    Private Sub frmUsu_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub


    Private Sub cmdSavePR_Click(sender As Object, e As EventArgs) Handles cmdSavePR.Click

        Try

            If Existe_Resolucion_Activa_Por_Bodega() Then
                XtraMessageBox.Show("Ya existe una resolución activa para la bodega: " & cmbBodega.Text, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            '#GT28092022_1730: Si se quiere guardar resolución la serie es obligatoria con un valor.
            If txtNoSerie.Text = "" Or txtNoSerie.EditValue = "" Then
                XtraMessageBox.Show("La serie no puede estar vacia.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNoSerie.Focus()
                Exit Sub
            End If

            If Not Rangos_Resolucion_Correcto() Then
                XtraMessageBox.Show("El correlativo final debe ser mayor que el inicial ", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            If txtCorrelativoFinal.Value > 999999999 Then
                XtraMessageBox.Show("El correlativo final excede el permitido de 999999999.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            '#GT26012024: si es nuevo registro, validar que no existe previamente la serie
            If clsLnResolucion_lp_operador.Existe_Serie(txtNoSerie.Text) Then
                XtraMessageBox.Show(String.Format("La serie {0} ya existe para un operador.", txtNoSerie.Text), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNoSerie.Focus()
                Exit Sub
            End If

            Dim esNuevo As Boolean = Val(cmdSavePR.Tag) = 0
            If esNuevo Then
                If clsLnResolucion_lp_usuario.Existe_Serie(txtNoSerie.Text) Then
                    XtraMessageBox.Show(String.Format("La serie {0} ya existe para un usuario.", txtNoSerie.Text), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtNoSerie.Focus()
                    Exit Sub
                End If
            Else
                If clsLnResolucion_lp_usuario.Existe_Serie_By_IdUsuario_And_IdBodega(txtNoSerie.Text, Usuario.IdUsuario, cmbBodega.EditValue) Then
                    XtraMessageBox.Show(String.Format("La serie {0} ya existe para otro usuario.", txtNoSerie.Text), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtNoSerie.Focus()
                    Exit Sub
                End If
            End If


            Dim vIndice As Integer = 0
            Dim vIdResolucionLP As Integer = 0

            If (Val(cmdSavePR.Tag) = 0) Then 'Es un nuevo registro
                vIdResolucionLP = Val(lblIdResolucionLP.Text)
            Else
                vIdResolucionLP = Val(cmdSavePR.Tag)
            End If

            vIndice = lResolucionesLP.FindIndex(Function(x) x.Idresolucionlpusuario = vIdResolucionLP)

            If vIndice = -1 Then '#EJC20210305 Es nuevo.                

                Dim BeResolLP As New clsBeResolucion_lp_usuario()
                BeResolLP.Idresolucionlpusuario = clsLnResolucion_lp_usuario.MaxID() + 1
                BeResolLP.IsNew = True
                BeResolLP.Idbodega = cmbBodega.EditValue
                BeResolLP.Idusuario = Usuario.IdUsuario
                BeResolLP.Serie = txtNoSerie.Text
                BeResolLP.Correlativo_inicial = txtCorrelativoInicial.Value
                BeResolLP.Correlativo_final = txtCorrelativoFinal.Value
                BeResolLP.Correlativo_actual = txtCorrelativoActual.Value
                BeResolLP.Activo = True
                BeResolLP.User_agr = AP.UsuarioAp.IdUsuario
                BeResolLP.Fec_agr = Now
                BeResolLP.User_mod = AP.UsuarioAp.IdUsuario
                BeResolLP.Fec_mod = Now

                clsLnResolucion_lp_usuario.Insertar(BeResolLP)

                XtraMessageBox.Show("Registro agregado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Else

                Dim BeResolLP As New clsBeResolucion_lp_usuario()
                BeResolLP = lResolucionesLP(vIndice)
                BeResolLP.Serie = txtNoSerie.Text
                BeResolLP.Correlativo_inicial = txtCorrelativoInicial.Value
                BeResolLP.Correlativo_final = txtCorrelativoFinal.Value
                BeResolLP.Correlativo_actual = txtCorrelativoActual.Value
                BeResolLP.Activo = True
                BeResolLP.User_mod = AP.UsuarioAp.IdUsuario
                BeResolLP.Fec_mod = Now
                clsLnResolucion_lp_usuario.Actualizar(BeResolLP)

                XtraMessageBox.Show("Registro actualizado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If

            Cargar_Resoluciones_LP()

            Limpiar_Campos_Para_Nueva_Resolucion()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub dGridPresentacion_DoubleClick(sender As Object, e As EventArgs) Handles dGridResoluciones.DoubleClick

        Try

            If GrdResolucion.RowCount > 0 Then

                Dim Dr As DataRowView = GrdResolucion.GetFocusedRow

                Dim lIndex As Integer = -1
                lIndex = lResolucionesLP.FindIndex(Function(b) b.Idresolucionlpusuario = Dr.Item("Código"))

                If lIndex > -1 Then

                    pObjResolucionLP = lResolucionesLP.Find(Function(b) b.Idresolucionlpusuario = Dr.Item("Código"))

                    lblIdResolucionLP.Text = lResolucionesLP(lIndex).Idresolucionlpusuario
                    cmdSavePR.Tag = lResolucionesLP(lIndex).Idresolucionlpusuario
                    txtNoSerie.Text = lResolucionesLP(lIndex).Serie
                    txtCorrelativoInicial.Value = lResolucionesLP(lIndex).Correlativo_inicial
                    txtCorrelativoFinal.Value = lResolucionesLP(lIndex).Correlativo_final
                    txtCorrelativoActual.Value = lResolucionesLP(lIndex).Correlativo_actual
                    cmbBodega.EditValue = lResolucionesLP(lIndex).Idbodega
                    chkResolucionLPActiva.Checked = lResolucionesLP(lIndex).Activo

                    txtNoSerie.Focus()

                    txtCorrelativoInicial.ReadOnly = txtCorrelativoActual.Value <> 0

                    If txtCorrelativoActual.Value <> 0 Then
                        txtCorrelativoFinal.Minimum = txtCorrelativoActual.Value + 1
                    Else
                        txtCorrelativoFinal.Minimum = 1
                    End If

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


    Private lResolucionesLP As New List(Of clsBeResolucion_lp_usuario)
    Private pObjResolucionLP As New clsBeResolucion_lp_usuario()
    Private DTResolucionLP As New DataTable("Resolucion")
    Private Sub Init_DT_Resoluciones_LP()

        DTResolucionLP.Columns.Add("Código", GetType(Integer))
        DTResolucionLP.Columns.Add("Bodega", GetType(String))
        DTResolucionLP.Columns.Add("Serie", GetType(String))
        DTResolucionLP.Columns.Add("correlativo_inicial", GetType(Double))
        DTResolucionLP.Columns.Add("correlativo_final", GetType(Double))
        DTResolucionLP.Columns.Add("correlativo_actual", GetType(Double))
        DTResolucionLP.Columns.Add("Activo", GetType(Boolean))

    End Sub

    Private Sub Cargar_Resoluciones_LP()

        Try

            lResolucionesLP = clsLnResolucion_lp_usuario.Get_All_By_IdUsuario(Usuario.IdUsuario)

            dGridResoluciones.DataSource = Nothing

            If lResolucionesLP.Count > 0 Then

                Dim vNomPresentacionContenidaEnPallet As String = ""
                Dim vNomBodega As String = ""

                DTResolucionLP.Rows.Clear()

                For Each Obj As clsBeResolucion_lp_usuario In lResolucionesLP.FindAll(Function(b) b.Activo = chkActivoPR.Checked)

                    vNomBodega = clsLnBodega.Get_Nombre_Bodega_By_IdBodega(Obj.Idbodega)

                    DTResolucionLP.Rows.Add(Obj.Idresolucionlpusuario,
                                            vNomBodega,
                                            Obj.Serie,
                                            Obj.Correlativo_inicial,
                                            Obj.Correlativo_final,
                                            Obj.Correlativo_actual,
                                            Obj.Activo)
                Next

                dGridResoluciones.DataSource = DTResolucionLP

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

    Private Sub cmdNewPR_Click(sender As Object, e As EventArgs) Handles cmdNewPR.Click

        Try

            Limpiar_Campos_Para_Nueva_Resolucion()

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Function Existe_Resolucion_Activa_Por_Bodega() As Boolean

        Existe_Resolucion_Activa_Por_Bodega = False

        Try

            Dim vIndiceResolucion As Integer = 0

            vIndiceResolucion = lResolucionesLP.FindIndex(Function(x) x.Idbodega = cmbBodega.EditValue AndAlso x.Activo = True AndAlso x.Correlativo_actual < x.Correlativo_final AndAlso Not (x.Idresolucionlpusuario = cmdSavePR.Tag))

            Existe_Resolucion_Activa_Por_Bodega = vIndiceResolucion <> -1

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Function Rangos_Resolucion_Correcto() As Boolean

        Rangos_Resolucion_Correcto = True

        Try

            Rangos_Resolucion_Correcto = txtCorrelativoFinal.Value > txtCorrelativoInicial.Value

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Sub Limpiar_Campos_Para_Nueva_Resolucion()

        cmbBodega.EditValue = Nothing
        txtNoSerie.Text = ""
        txtCorrelativoInicial.Value = 1
        txtCorrelativoFinal.Minimum = 1
        txtCorrelativoFinal.Value = 1
        txtCorrelativoActual.Value = 0
        cmbBodega.Focus()
        lblIdResolucionLP.Text = clsLnResolucion_lp_usuario.MaxID() + 1
        cmdSavePR.Tag = 0

    End Sub
    Private Sub cmdDesactivarResolucion_Click(sender As Object, e As EventArgs) Handles cmdDesactivarResolucion.Click

        Try

            Dim BeResolLP As New clsBeResolucion_lp_usuario()
            BeResolLP.Idresolucionlpusuario = lblIdResolucionLP.Text
            BeResolLP.User_mod = AP.UsuarioAp.IdUsuario
            BeResolLP.Fec_mod = Now
            clsLnResolucion_lp_usuario.Desactivar(BeResolLP)

            XtraMessageBox.Show("Registro desactivado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Cargar_Resoluciones_LP()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub
    Private Sub chkActivoPR_CheckedChanged(sender As Object, e As EventArgs) Handles chkActivoPR.CheckedChanged
        Try
            Cargar_Resoluciones_LP()
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Sub

End Class