Imports System.Drawing.Imaging
Imports System.IO
Imports System.Reflection
Imports DevExpress.Data
Imports DevExpress.Data.Camera
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository

Public Class frmOperador

    Private pListObjT As New List(Of clsTabla)
    Private DT As DataTable
    Public pListObjPB As List(Of clsBeOperador_bodega)
    Public pIdOperador As Integer
    Public BeOperador As New clsBeOperador
    Public Delegate Sub Listar_Operadores()
    Public Property Listar As Listar_Operadores
    Private DTResolucionLP As New DataTable("Presentacion")
    Public Property lZonaPickingOperador As New List(Of clsBeOperador_zona_picking_tramo)

    Dim pOperadorJornadaLaboral As New clsBeOperador_jornada_laboral
    Dim listOperadorJornadaLaboral As New List(Of clsBeOperador_jornada_laboral)
    Private DTJornadasLaborales As New DataTable("JornadasLaborales")

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

    Private Sub Init_DT_Resoluciones_LP()

        DTResolucionLP.Columns.Add("Código", GetType(Integer))
        DTResolucionLP.Columns.Add("Bodega", GetType(String))
        DTResolucionLP.Columns.Add("Serie", GetType(String))
        DTResolucionLP.Columns.Add("correlativo_inicial", GetType(Double))
        DTResolucionLP.Columns.Add("correlativo_final", GetType(Double))
        DTResolucionLP.Columns.Add("correlativo_actual", GetType(Double))
        DTResolucionLP.Columns.Add("Activo", GetType(Boolean))

    End Sub

    Private Sub Init_DT_JornadasLaborales()

        DTJornadasLaborales.Columns.Add("Correlativo", GetType(Integer))
        DTJornadasLaborales.Columns.Add("Operador", GetType(String))
        DTJornadasLaborales.Columns.Add("Bodega", GetType(String))
        DTJornadasLaborales.Columns.Add("Jornada laboral", GetType(Integer))
        DTJornadasLaborales.Columns.Add("Activo", GetType(Boolean))

    End Sub

    Private Sub ListaBodegas()

        Try
            If DT.Rows.Count > 0 Then

                Dim vIdBodega As Integer = 0

                For i As Integer = 0 To DT.Rows.Count - 1

                    vIdBodega = DT(i)(0)
                    Dim lRow As DataRow = DsOperador.Data.NewRow
                    lRow.Item("IdBodega") = vIdBodega
                    lRow.Item("Bodega") = clsLnBodega.Get_Nombre_Bodega_By_IdBodega(vIdBodega)
                    lRow.Item("Selección") = False
                    If TipoTrans.Editar Then
                        If pListObjPB IsNot Nothing AndAlso pListObjPB.Count > 0 Then
                            For Each Obj As clsBeOperador_bodega In pListObjPB
                                If Obj.IdBodega = CInt(DT(i)(0)) AndAlso Obj.Activo Then
                                    lRow.Item("Selección") = True
                                    lRow.Item("IdOperadorBodega") = Obj.IdOperadorBodega
                                End If
                                lRow.Item("IdInterno") = Obj.IdOperadorBodega
                            Next
                        End If
                    End If
                    DsOperador.Data.AddDataRow(lRow)
                Next
            End If

            Grid.EndUpdate()
            Grid.ForceInitialize()
            Dim ritem As RepositoryItemCheckEdit = TryCast(gridView1.Columns("Selección").RealColumnEdit, RepositoryItemCheckEdit)
            AddHandler ritem.CheckedChanged, AddressOf ritem_CheckedChanged

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub ritem_CheckedChanged(sender As Object, e As EventArgs)

        Try

            Dim ritem As CheckEdit = TryCast(sender, CheckEdit)

            If Not ritem Is Nothing Then

                Dim Dr As DataRowView = gridView1.GetFocusedRow
                Dim lIndex As Integer = -1
                lIndex = pListObjPB.FindIndex(Function(b) b.IdBodega = CInt(Dr.Item("IdBodega")) _
                                                  And b.IdOperador = BeOperador.IdOperador)
                If lIndex > -1 Then
                    If ritem.Checked Then
                        pListObjPB(lIndex).Activo = True
                    Else
                        pListObjPB(lIndex).Activo = False
                    End If
                    pListObjPB(lIndex).User_mod = AP.UsuarioAp.IdUsuario
                    pListObjPB(lIndex).Fec_mod = Now
                Else
                    Dim Obj As New clsBeOperador_bodega() With {.IdBodega = Dr.Item("IdBodega"), .IdOperador = BeOperador.IdOperador, .User_agr = AP.UsuarioAp.IdUsuario, .Fec_agr = Now, .User_mod = AP.UsuarioAp.IdUsuario, .Fec_mod = Now, .Activo = True}
                    pListObjPB.Add(Obj)
                End If
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub ValidaBodegas()

        Try

            DsOperador.Clear()
            DT = IMS.Listar_Bodegas()
            pListObjPB = New List(Of clsBeOperador_bodega)
            pListObjPB = clsLnOperador_bodega.Get_All_By_IdOperador(BeOperador.IdOperador)
            ListaBodegas()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub frmOperador_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            pListObjT = clsBD.GetLongitudByTabla("operador")

            If Not IMS.Listar_Empresas(cmbEmpresa) Then
                XtraMessageBox.Show("No hay empresas definidas para la aplicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            IMS.Listar_JornadasPorBodega(cmbJornada, AP.IdBodega)
            'IMS.Listar_JornadasPorBodega(cmbJornadaLaboral, AP.IdBodega)

            Me.txtTelefono.Properties.Mask.EditMask = "\((\d{3})\) (\d{4})-(\d{4})"
            Me.txtTelefono.Properties.Mask.MaskType = Mask.MaskType.RegEx

            AP.Listar_Bodegas_By_Usuario(cmbBodega)

            Init_DT_Resoluciones_LP()

            Listar_Zonas_Picking()

            Init_DT_JornadasLaborales()

            Select Case Modo

                Case TipoTrans.Nuevo

                    lblCodigo.Text = clsLnOperador.MaxID()
                    cmbEmpresa.Enabled = True
                    chkActivo.Checked = True
                    chkSistema.Checked = False
                    User_agrTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_agrDateEdit.Text = Now
                    User_modTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_modDateEdit.Text = Now

                    If OpcionesMenu IsNot Nothing Then
                        mnuGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False
                    cmbEmpresa.Enabled = True

                    TabDatos.TabPages.Remove(TabOperadorBodega)
                    TabDatos.TabPages.Remove(xTabJornadaLaboral)
                    TabDatos.TabPages.Remove(ResolucionesLP)

                Case TipoTrans.Editar

                    clsLnOperador.Obtener(BeOperador)

                    lblCodigo.Text = BeOperador.IdOperador
                    txtIdRolOperador.Text = BeOperador.IdRolOperador
                    'cmbJornada.EditValue = BeOperador.IdJornada
                    'cmbJornadaLaboral.EditValue = BeOperador.IdJornada
                    cmbEmpresa.EditValue = BeOperador.IdEmpresa
                    cmbEmpresa.Enabled = False

                    txtNombres.Text = BeOperador.Nombres
                    txtApellidos.Text = BeOperador.Apellidos
                    txtDireccion.Text = BeOperador.Direccion
                    txtTelefono.Text = BeOperador.Telefono
                    txtCodigo.Text = BeOperador.Codigo
                    txtClave.Text = BeOperador.Clave
                    txtCostoHora.Text = BeOperador.Costo_hora
                    chkUsaHH.Checked = BeOperador.Usa_hh
                    chkSistema.Checked = BeOperador.Sistema

                    chkActivo.Checked = BeOperador.Activo

                    User_agrTextEdit.Text = BeOperador.User_agr
                    Fec_agrDateEdit.Text = BeOperador.Fec_agr
                    User_modTextEdit.Text = BeOperador.User_mod
                    Fec_modDateEdit.Text = BeOperador.Fec_mod

                    If BeOperador.Foto IsNot Nothing Then
                        picFoto.Image = ByteArrayToImage(BeOperador.Foto)
                    End If

                    chkRecibe.Checked = BeOperador.Recibe
                    chkUbica.Checked = BeOperador.Ubica
                    chkTransporta.Checked = BeOperador.Transporta
                    chkPickea.Checked = BeOperador.Pickea
                    chkVerifica.Checked = BeOperador.Verifica

                    BeOperador.Montacarga = chkMontacarga.Checked
                    Cargar_Rol() ' Metodo utilizado para conseguir nombre del rol

                    If BeOperador.Sistema Then
                        mnuGuardar.Enabled = False
                        mnuActualizar.Enabled = False
                        mnuEliminar.Enabled = False
                        cmdImprimeCarnet.Enabled = False
                        mnuCapturarFoto.Enabled = False
                    Else

                        mnuGuardar.Enabled = False

                        If OpcionesMenu IsNot Nothing Then
                            mnuActualizar.Enabled = OpcionesMenu.Modificar
                            mnuEliminar.Enabled = OpcionesMenu.Eliminar
                        End If

                    End If

                    ValidaBodegas()

                    Dim pBeMarcaje As New clsBeMarcaje
                    pBeMarcaje.IdEmpresa = BeOperador.IdEmpresa
                    pBeMarcaje.IdBodega = AP.IdBodega
                    pBeMarcaje.IdOperador = BeOperador.IdOperador
                    clsLnMarcaje.Get_Marcaje_By_Operador_And_FechaActual(pBeMarcaje)

                    If Not pBeMarcaje Is Nothing Then
                        dtpUltimaSesion.Value = pBeMarcaje.Hora_entro
                        picEstadoOp.Image = My.Resources.green_ball
                    Else

                        pBeMarcaje = New clsBeMarcaje
                        pBeMarcaje.IdEmpresa = BeOperador.IdEmpresa
                        pBeMarcaje.IdBodega = AP.IdBodega
                        pBeMarcaje.IdOperador = BeOperador.IdOperador
                        clsLnMarcaje.Get_Ultimo_Marcaje_By_Operador(pBeMarcaje)

                        If Not pBeMarcaje Is Nothing Then
                            dtpUltimaSesion.Value = pBeMarcaje.Hora_entro
                        End If

                        picEstadoOp.Image = My.Resources.red_ball

                    End If

                    Cargar_Resoluciones_LP()

                    Cargar_Zonas_Picking_Operador()

                    Cargar_Jornada_Laboral()

                    cargar_jornadas_laborales()

            End Select

            Application.DoEvents()

            Dim lCamarasInfo As New List(Of CameraDeviceInfo)
            lCamarasInfo = Camera.CameraControl.GetDevices()

            If Not lCamarasInfo Is Nothing Then

                If lCamarasInfo.Count > 0 Then
                    mnuCapturarFoto.Enabled = Not BeOperador.Sistema
                Else
                    CameraControl1.Visible = False
                    mnuCapturarFoto.Enabled = False
                End If

            Else
                CameraControl1.Visible = False
                mnuCapturarFoto.Enabled = False
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

        txtNombres.Focus()

    End Sub

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick

        Try

            If Datos_Correctos() Then

                If MessageBox.Show("¿Guardar el registro?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    If Guardar() Then

                        XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                        Cargar_Jornada_Laboral()
                        Cargar_Resoluciones_LP()

                        If MessageBox.Show("¿Asignar bodegas?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            TabDatos.TabPages.Add(TabOperadorBodega)
                            ValidaBodegas()
                            mnuGuardar.Enabled = False
                            mnuActualizar.Enabled = True
                            mnuEliminar.Enabled = True
                        Else
                            Close()
                        End If

                    End If

                    If Listar IsNot Nothing Then
                        Listar.Invoke()
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

    Private Sub Cargar_Jornada_Laboral()

        Try

            TabDatos.TabPages.Add(xTabJornadaLaboral)
            IMS.Listar_JornadasPorBodega(cmbJornadaLaboral, AP.IdBodega)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
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

            Cargar_Rol()

            BeOperador = New clsBeOperador()
            BeOperador.IdEmpresa = cmbEmpresa.EditValue
            BeOperador.IdOperador = clsLnOperador.MaxID()
            BeOperador.IdRolOperador = Integer.Parse(txtIdRolOperador.Text.Trim)
            BeOperador.IdJornada = cmbJornada.EditValue
            BeOperador.Nombres = txtNombres.Text.Trim()
            BeOperador.Apellidos = txtApellidos.Text.Trim()
            BeOperador.Direccion = txtDireccion.Text.Trim()
            BeOperador.Telefono = txtTelefono.Text.Trim()
            BeOperador.Codigo = txtCodigo.Text.Trim()
            BeOperador.Clave = txtClave.Text.Trim()
            BeOperador.Costo_hora = txtCostoHora.Text.Trim()
            BeOperador.Usa_hh = chkUsaHH.Checked
            BeOperador.Activo = True
            BeOperador.User_agr = AP.UsuarioAp.IdUsuario
            BeOperador.Fec_agr = Now
            BeOperador.User_mod = AP.UsuarioAp.IdUsuario
            BeOperador.Fec_mod = Now

            If picFoto.Image IsNot Nothing Then
                BeOperador.Foto = ImageToByteArray(picFoto.Image)
            End If

            BeOperador.Recibe = chkRecibe.Checked
            BeOperador.Ubica = chkUbica.Checked
            BeOperador.Transporta = chkTransporta.Checked
            BeOperador.Pickea = chkPickea.Checked
            BeOperador.Verifica = chkVerifica.Checked
            BeOperador.Montacarga = chkMontacarga.Checked
            BeOperador.Sistema = chkSistema.Checked

            Guardar = clsLnOperador.Insertar(BeOperador) > 0

            BeOperador = BeOperador

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            If Datos_Correctos() Then

                BeOperador.IdRolOperador = CInt(txtIdRolOperador.Text.Trim)
                BeOperador.IdJornada = cmbJornada.EditValue
                BeOperador.Nombres = txtNombres.Text.Trim()
                BeOperador.Apellidos = txtApellidos.Text.Trim()
                BeOperador.Direccion = txtDireccion.Text.Trim()
                BeOperador.Telefono = txtTelefono.Text.Trim()
                BeOperador.Codigo = txtCodigo.Text.Trim()
                BeOperador.Clave = txtClave.Text.Trim()
                BeOperador.Costo_hora = txtCostoHora.Text.Trim()
                BeOperador.Usa_hh = chkUsaHH.Checked

                BeOperador.User_mod = AP.UsuarioAp.IdUsuario
                BeOperador.Fec_mod = Now

                BeOperador.Activo = chkActivo.Checked

                If picFoto.Image IsNot Nothing Then
                    BeOperador.Foto = ImageToByteArray(picFoto.Image)
                End If

                BeOperador.Recibe = chkRecibe.Checked
                BeOperador.Ubica = chkUbica.Checked
                BeOperador.Transporta = chkTransporta.Checked
                BeOperador.Pickea = chkPickea.Checked
                BeOperador.Verifica = chkVerifica.Checked
                BeOperador.Montacarga = chkMontacarga.Checked
                BeOperador.Sistema = chkSistema.Checked

                Return clsLnOperador.ActualizarDatos(BeOperador, pListObjPB)

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Datos_Correctos() As Boolean

        Datos_Correctos = False

        Try

            If cmbEmpresa.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Empresa.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf String.IsNullOrEmpty(txtNombres.Text.Trim) Then
                XtraMessageBox.Show("Ingrese Nombres.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombres.Focus()
            ElseIf txtNombres.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRES").Longitud Then
                XtraMessageBox.Show(String.Format("Los Nombres deben de tener como máximo {0} carácteres.", pListObjT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "NOMBRES").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombres.Focus()
            ElseIf txtApellidos.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "APELLIDOS").Longitud Then
                XtraMessageBox.Show(String.Format("Los Apellidos deben de tener como máximo {0} carácteres.", pListObjT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "APELLIDOS").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtApellidos.Focus()
            ElseIf txtDireccion.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "DIRECCION").Longitud Then
                XtraMessageBox.Show(String.Format("La Dirección debe de tener como máximo {0} carácteres.", pListObjT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "DIRECCION").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtDireccion.Focus()
            ElseIf txtTelefono.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "TELEFONO").Longitud Then
                XtraMessageBox.Show(String.Format("El Teléfono debe de tener como máximo {0} carácteres.", pListObjT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "DIRECCION").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtTelefono.Focus()
            ElseIf txtCodigo.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "CODIGO").Longitud Then
                XtraMessageBox.Show(String.Format("El Código debe de tener como máximo {0} carácteres.", pListObjT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "CODIGO").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtCodigo.Focus()
            ElseIf txtClave.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "CLAVE").Longitud Then
                XtraMessageBox.Show(String.Format("La Clave debe de tener como máximo {0} carácteres.", pListObjT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "CLAVE").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtClave.Focus()
            ElseIf String.IsNullOrEmpty(txtIdRolOperador.Text.Trim) Then
                XtraMessageBox.Show("Ingrese Operador.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombreOperador.Text = ""
                txtIdRolOperador.Focus()
            ElseIf String.IsNullOrEmpty(txtNombreOperador.Text) Then
                XtraMessageBox.Show("Ingrese Operador.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtIdRolOperador.Focus()
            ElseIf cmbJornada.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Jornada Laboral.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                Datos_Correctos = True
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        Try

            Cargar_Rol()

            If Actualizar() Then
                XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                If Listar IsNot Nothing Then
                    Listar.Invoke()
                End If
                Close()
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

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try

            If MessageBox.Show("¿Desactivar el operador?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                If clsLnOperador.Activar_Desactivar_Operador(BeOperador, False) Then
                    XtraMessageBox.Show("Se ha desactivado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    If Listar IsNot Nothing Then
                        Listar.Invoke()
                    End If
                    Close()
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub lnkRolOperador_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkRolOperador.LinkClicked

        Try

            Dim frmDialogRolOperador As New frmRolOperadorList() With {.Modo = frmRolOperadorList.pModo.Seleccion}

            If OpcionesMenu IsNot Nothing Then
                frmDialogRolOperador.OpcionesMenu = OpcionesMenu
                frmDialogRolOperador.mnuNuevo.Enabled = OpcionesMenu.Modificar
                frmDialogRolOperador.mnuActualizar.Enabled = OpcionesMenu.Leer
            End If

            frmDialogRolOperador.ShowDialog()

            If frmDialogRolOperador.ObjetoRolOperador.IdRolOperador <> 0 Then
                txtIdRolOperador.Text = frmDialogRolOperador.ObjetoRolOperador.IdRolOperador
                txtNombreOperador.Text = frmDialogRolOperador.ObjetoRolOperador.Nombre
            End If

            frmDialogRolOperador.Close()
            frmDialogRolOperador.Dispose()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    'impedir que se escriba letras en el campo solo numeros y vacios no aceptar decimal solo enteros
    Private Sub txtIdRolOperador_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIdRolOperador.KeyPress

        Try

            If Not (Char.IsControl(e.KeyChar) And e.KeyChar <> ".") Then
                e.Handled = True
            End If

            If e.KeyChar = "." Then
                e.Handled = True
            End If

            If Char.IsDigit(e.KeyChar) Then
                e.Handled = False
            End If

            If e.KeyChar = Convert.ToChar(8) AndAlso txtIdRolOperador.Text.Length = 1 Then
                txtNombreOperador.Text = String.Empty
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtIdRol_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtIdRolOperador.PreviewKeyDown

        Try
            If e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter Then
                Cargar_Rol()
            End If
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    'Metodo creado x bismarck
    Private Sub Cargar_Rol()

        Dim Obj As New clsBeRol_operador

        If String.IsNullOrEmpty(txtIdRolOperador.Text.Trim()) = False Then

            Obj.IdRolOperador = CInt(txtIdRolOperador.Text.Trim())

            If Obj.IdRolOperador > 0 Then
                If clsLnRol_operador.Obtener(Obj) Then
                    txtNombreOperador.Text = Obj.Nombre
                Else
                    txtNombreOperador.Text = String.Empty
                    txtIdRolOperador.Focus() : txtIdRolOperador.SelectAll()
                End If
            Else
                txtIdRolOperador.Text = String.Empty
                txtNombreOperador.Text = String.Empty
                txtIdRolOperador.Focus()
                XtraMessageBox.Show("No existe un rol asignado para el operador " + txtNombres.Text, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Else
            txtIdRolOperador.Text = ""
            txtNombreOperador.Text = ""
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

    Private Sub gridView1_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gridView1.RowStyle

        Try

            gridView1.OptionsBehavior.Editable = True
            gridView1.OptionsSelection.EnableAppearanceFocusedCell = True
            gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
            gridView1.OptionsSelection.EnableAppearanceFocusedRow = True
            gridView1.OptionsSelection.EnableAppearanceHideSelection = True
            gridView1.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            gridView1.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            gridView1.Appearance.FocusedRow.ForeColor = Color.White
            gridView1.Appearance.SelectedRow.ForeColor = Color.White
            gridView1.Appearance.SelectedRow.Options.UseBackColor = True

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub btnExaminar_Click(sender As Object, e As EventArgs) Handles btnExaminar.Click
        Dim gFile As New OpenFileDialog() With {.Filter = "All Images|*.BMP;*.DIB;*.RLE;*.JPG;*.JPEG;*.JPE;*.JFIF;*.GIF;*.TIF;*.TIFF;*.PNG|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|All Files|*.*"}
        gFile.ShowDialog()
        If gFile.FileName.Length <> 0 Then
            picFoto.Image = Image.FromFile(gFile.FileName)
        End If
    End Sub

    Private Sub frmOperador_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuCapturarFoto.ItemClick

        Try

            If Not CameraControl1.Enabled Then
                CameraControl1.Enabled = True
            Else
                picFoto.Image = CameraControl1.TakeSnapshot()
            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)

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

    Private Sub Limpiar_Campos_Para_Nueva_Resolucion()

        cmbBodega.EditValue = Nothing
        txtNoSerie.Text = ""
        txtCorrelativoInicial.Value = 1
        txtCorrelativoFinal.Value = 1
        txtCorrelativoActual.Value = 0
        cmbBodega.Focus()
        lblIdResolucionLP.Text = clsLnResolucion_lp_operador.MaxID() + 1
        cmdSavePR.Tag = 0

    End Sub

    Private lResolucionesLP As New List(Of clsBeResolucion_lp_operador)
    Private pObjResolucionLP As New clsBeResolucion_lp_operador()

    Private Function Existe_Resolucion_Activa_Por_Bodega() As Boolean

        Existe_Resolucion_Activa_Por_Bodega = False

        Try

            Dim vIndiceResolucion As Integer = 0

            vIndiceResolucion = lResolucionesLP.FindIndex(Function(x) x.IdBodega = cmbBodega.EditValue AndAlso x.Activo = True AndAlso x.Correlativo_Actual < x.Correlativo_Final AndAlso Not (x.IdResolucionlp = cmdSavePR.Tag))

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

    Private Sub cmdSavePR_Click(sender As Object, e As EventArgs) Handles cmdSavePR.Click

        Try

            If Existe_Resolucion_Activa_Por_Bodega() Then
                XtraMessageBox.Show("Ya existe una resolución activa para la bodega: " & cmbBodega.Text, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            '#GT28092022_1730: Si se quiere guardar resolución la serie es obligatoria con un valor.
            If txtNoSerie.Text = "" OrElse txtNoSerie.EditValue = "" Then
                XtraMessageBox.Show("La serie no puede estar vacia.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNoSerie.Focus()
                Exit Sub
            End If

            If cmbBodega.EditValue = 0 Then
                XtraMessageBox.Show("Debe seleccionar la bodega de la resoluciónn ", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            If Not Rangos_Resolucion_Correcto() Then
                XtraMessageBox.Show("El correlativo final debe ser mayor que el inicial ", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            If Len(txtCorrelativoFinal.Value) > 9 Then
                XtraMessageBox.Show("El correlativo final excede el permitido de 999999999.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            '#GT26012024: si es nuevo registro, validar que no existe previamente la serie
            Dim esNuevo As Boolean = Val(cmdSavePR.Tag) = 0
            If esNuevo Then
                If clsLnResolucion_lp_operador.Existe_Serie(txtNoSerie.Text) Then
                    XtraMessageBox.Show(String.Format("La serie {0} ya existe para un operador.", txtNoSerie.Text), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtNoSerie.Focus()
                    Exit Sub
                End If
            Else
                If clsLnResolucion_lp_operador.Existe_Serie_By_IdOperador_And_IdBodega(txtNoSerie.Text, BeOperador.IdOperador, cmbBodega.EditValue) Then
                    XtraMessageBox.Show(String.Format("La serie {0} ya existe para otro operador.", txtNoSerie.Text), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtNoSerie.Focus()
                    Exit Sub
                End If
            End If


            '#EJC20240815: Validar que no exista serie en usuario también.
            If clsLnResolucion_lp_usuario.Existe_Serie(txtNoSerie.Text) Then
                XtraMessageBox.Show(String.Format("La serie {0} ya existe para un usuario.", txtNoSerie.Text), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNoSerie.Focus()
                Exit Sub
            End If

            Dim vIndice As Integer = 0
            Dim vIdResolucionLP As Integer = 0

            If (Val(cmdSavePR.Tag) = 0) Then 'Es un nuevo registro
                vIdResolucionLP = Val(lblIdResolucionLP.Text)
            Else
                vIdResolucionLP = Val(cmdSavePR.Tag)
            End If

            vIndice = lResolucionesLP.FindIndex(Function(x) x.IdResolucionlp = vIdResolucionLP)

            If vIndice = -1 Then '#EJC20210305 Es nuevo.                

                Dim BeResolLP As New clsBeResolucion_lp_operador()
                BeResolLP.IdResolucionlp = clsLnResolucion_lp_operador.MaxID() + 1
                BeResolLP.IsNew = True
                BeResolLP.IdBodega = cmbBodega.EditValue
                BeResolLP.IdOperador = BeOperador.IdOperador
                BeResolLP.Serie = txtNoSerie.Text
                BeResolLP.Correlativo_Inicial = txtCorrelativoInicial.Value
                BeResolLP.Correlativo_Final = txtCorrelativoFinal.Value
                BeResolLP.Correlativo_Actual = txtCorrelativoActual.Value
                BeResolLP.Activo = True
                BeResolLP.User_agr = AP.UsuarioAp.IdUsuario
                BeResolLP.Fec_agr = Now
                BeResolLP.User_mod = AP.UsuarioAp.IdUsuario
                BeResolLP.Fec_mod = Now

                clsLnResolucion_lp_operador.Insertar(BeResolLP)

                XtraMessageBox.Show("Registro agregado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Else

                Dim BeResolLP As New clsBeResolucion_lp_operador()
                BeResolLP = lResolucionesLP(vIndice)
                BeResolLP.IsNew = False
                BeResolLP.Serie = txtNoSerie.Text
                BeResolLP.Correlativo_Inicial = txtCorrelativoInicial.Value
                BeResolLP.Correlativo_Final = txtCorrelativoFinal.Value
                BeResolLP.Correlativo_Actual = txtCorrelativoActual.Value
                BeResolLP.Activo = True
                BeResolLP.User_mod = AP.UsuarioAp.IdUsuario
                BeResolLP.Fec_mod = Now
                clsLnResolucion_lp_operador.Actualizar(BeResolLP)

                XtraMessageBox.Show("Registro actualizado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If

            Cargar_Resoluciones_LP()

            Limpiar_Campos_Para_Nueva_Resolucion()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub dGridPresentacion_DoubleClick(sender As Object, e As EventArgs) Handles dGridPresentacion.DoubleClick

        Try

            If GrdPresentacion.RowCount > 0 Then

                Dim Dr As DataRowView = GrdPresentacion.GetFocusedRow

                Dim lIndex As Integer = -1
                lIndex = lResolucionesLP.FindIndex(Function(b) b.IdResolucionlp = Dr.Item("Código"))

                If lIndex > -1 Then

                    pObjResolucionLP = lResolucionesLP.Find(Function(b) b.IdResolucionlp = Dr.Item("Código"))

                    lblIdResolucionLP.Text = lResolucionesLP(lIndex).IdResolucionlp
                    cmdSavePR.Tag = lResolucionesLP(lIndex).IdResolucionlp
                    txtNoSerie.Text = lResolucionesLP(lIndex).Serie
                    txtCorrelativoInicial.Value = lResolucionesLP(lIndex).Correlativo_Inicial
                    txtCorrelativoFinal.Value = lResolucionesLP(lIndex).Correlativo_Final
                    txtCorrelativoActual.Value = lResolucionesLP(lIndex).Correlativo_Actual
                    cmbBodega.EditValue = lResolucionesLP(lIndex).IdBodega
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

    Private Sub Cargar_Resoluciones_LP()

        Try

            TabDatos.TabPages.Add(ResolucionesLP)

            lResolucionesLP = clsLnResolucion_lp_operador.Get_All_By_IdOperador(BeOperador.IdOperador)

            dGridPresentacion.DataSource = Nothing

            If lResolucionesLP.Count > 0 Then

                Dim vNomPresentacionContenidaEnPallet As String = ""
                Dim vNomBodega As String = ""

                DTResolucionLP.Rows.Clear()

                For Each Obj As clsBeResolucion_lp_operador In lResolucionesLP.FindAll(Function(b) b.Activo = chkActivoPR.Checked)

                    vNomBodega = clsLnBodega.Get_Nombre_Bodega_By_IdBodega(Obj.IdBodega)

                    DTResolucionLP.Rows.Add(Obj.IdResolucionlp,
                                            vNomBodega,
                                            Obj.Serie,
                                            Obj.Correlativo_Inicial,
                                            Obj.Correlativo_Final,
                                            Obj.Correlativo_Actual,
                                            Obj.Activo)
                Next

                dGridPresentacion.DataSource = DTResolucionLP

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

    Private Sub mnuNuevoTramo_Click(sender As Object, e As EventArgs) Handles mnuNuevoTramo.Click

        Try

            lblIdZonaPickingTramoOp.Visible = False
            lblIdZonaPickingTramoOp.Text = ""
            mnuGuardarTramo.Tag = ""
            lblIdZonaPickingTramoOp.Text = ""

            Desactivar_Dias()

        Catch ex As Exception

        End Try

    End Sub

    Private Function Guardar_Zona_Picking_Operador(ByVal pDia As Integer) As Boolean

        Guardar_Zona_Picking_Operador = False

        Dim BeOperadorZonaPickingTramo As New clsBeOperador_zona_picking_tramo
        Dim BeZonaPicking As New clsBeZona_picking

        Try

            BeZonaPicking = clsLnZona_picking.GetSingle(cmbZonaPicking.EditValue)

            If Not BeZonaPicking Is Nothing Then

                If Not BeZonaPicking.Lista_Zona_Picking_Tramo Is Nothing Then

                    If BeZonaPicking.Lista_Zona_Picking_Tramo.Count > 0 Then

                        For Each ZP In BeZonaPicking.Lista_Zona_Picking_Tramo

                            BeOperadorZonaPickingTramo.IdZonaPickingTramoOperador = clsLnOperador_zona_picking_tramo.MaxID() + 1
                            BeOperadorZonaPickingTramo.IdZonaPickingTramo = ZP.IdZonaPickingTramo
                            BeOperadorZonaPickingTramo.IdZonaPicking = cmbZonaPicking.EditValue
                            BeOperadorZonaPickingTramo.IdOperador = BeOperador.IdOperador
                            BeOperadorZonaPickingTramo.Dia_semana = pDia
                            BeOperadorZonaPickingTramo.User_agr = AP.UsuarioAp.IdUsuario
                            BeOperadorZonaPickingTramo.Fec_agr = Now
                            BeOperadorZonaPickingTramo.User_mod = AP.UsuarioAp.IdUsuario
                            BeOperadorZonaPickingTramo.Fec_mod = Now
                            BeOperadorZonaPickingTramo.Activo = True

                            If Not clsLnOperador_zona_picking_tramo.Exists(BeOperador.IdOperador, BeOperadorZonaPickingTramo.IdZonaPickingTramo, pDia) Then
                                Guardar_Zona_Picking_Operador = clsLnOperador_zona_picking_tramo.Insertar(BeOperadorZonaPickingTramo) > 0
                            End If

                        Next

                    End If

                End If

            End If

            Guardar_Zona_Picking_Operador = True

        Catch ex As Exception
            Throw ex
        End Try


    End Function

    Private Sub mnuGuardarTramo_Click(sender As Object, e As EventArgs) Handles mnuGuardarTramo.Click

        Try


            If chkLunes.Checked Then
                Guardar_Zona_Picking_Operador(1)
            End If
            If chkMartes.Checked Then
                Guardar_Zona_Picking_Operador(2)
            End If
            If chkMiercoles.Checked Then
                Guardar_Zona_Picking_Operador(3)
            End If
            If chkJueves.Checked Then
                Guardar_Zona_Picking_Operador(4)
            End If
            If chkViernes.Checked Then
                Guardar_Zona_Picking_Operador(5)
            End If
            If chkSabado.Checked Then
                Guardar_Zona_Picking_Operador(6)
            End If
            If chkDomingo.Checked Then
                Guardar_Zona_Picking_Operador(7)
            End If

            Cargar_Zonas_Picking_Operador()

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
          Text,
          MessageBoxButtons.OK,
          MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Listar_Zonas_Picking()

        Try

            Dim DT As New DataTable
            DT = clsLnZona_picking.Get_All_DT(True, AP.IdEmpresa)

            If DT.Rows.Count > 0 Then

                cmbZonaPicking.Properties.DisplayMember = "Nombre"
                cmbZonaPicking.Properties.ValueMember = "IdZonaPicking"
                cmbZonaPicking.Properties.DataSource = DT
                cmbZonaPicking.EditValue = AP.IdBodega
                cmbZonaPicking.Properties.PopupWidth = 700
                cmbZonaPicking.Properties.PopulateColumns()
                cmbZonaPicking.Properties.BestFit()
                cmbZonaPicking.Properties.NullText = ""

            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
          Text,
          MessageBoxButtons.OK,
          MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Cargar_Zonas_Picking_Operador()

        Try

            Dim DT As New DataTable
            DT = clsLnOperador_zona_picking_tramo.Get_All_DT_By_IdEmpresa_And_IdOperador(BeOperador.IdEmpresa, BeOperador.IdOperador)

            DgridZonaPickingOperador.DataSource = DT

            If GridView2.Columns.Count > 0 Then

                GridView2.Columns("IdEmpresa").Visible = False
                GridView2.Columns("IdZonaPickingTramo").Visible = False
                GridView2.Columns("IdZonaPicking").Visible = False
                GridView2.BestFitColumns()

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

    Private Sub DgridZonaPickingOperador_Click(sender As Object, e As EventArgs) Handles DgridZonaPickingOperador.Click

        Try

            Dim Dr As DataRowView = GridView2.GetFocusedRow
            Dim BeZonaPicking As New clsBeZona_picking()
            Dim lIndex As Integer = -1

            If Not Dr Is Nothing Then

                Dim BeZonaPickingTramoOp As New clsBeOperador_zona_picking_tramo
                Dim vIdZonaPicking As Integer = Dr.Item("IdZonaPicking")
                Dim vIdZonaPickingTramo As Integer = Dr.Item("IdZonaPickingTramo")
                Dim vIdZonaPickingTramoOperador As Integer = Dr.Item("IdZonaPickingTramoOperador")
                Dim vDia As Integer = Dr.Item("NoDia")

                BeZonaPicking = clsLnZona_picking.GetSingle(vIdZonaPicking)

                If Not BeZonaPicking Is Nothing Then

                    'El operador tiene asignada la zona de picking?
                    BeZonaPickingTramoOp = clsLnOperador_zona_picking_tramo.Get_Single_By_IdZonaPickingTramoOperador(vIdZonaPickingTramoOperador)

                    'Sí: la tiene asignada.
                    If Not BeZonaPicking.Lista_Zona_Picking_Tramo Is Nothing Then

                        'El objeto tiene registros?
                        If BeZonaPicking.Lista_Zona_Picking_Tramo.Count > 0 Then

                            Desactivar_Dias()

                            'La lista de zonas_picking_tramo ya contiene un registro con ese IdZonaPickingTramo
                            If (BeZonaPickingTramoOp.IdZonaPickingTramo = vIdZonaPickingTramo AndAlso BeZonaPickingTramoOp.Dia_semana = vDia) Then

                                mnuEliminarTramo.Tag = BeZonaPickingTramoOp.IdZonaPickingTramoOperador

                                cmbZonaPicking.EditValue = BeZonaPicking.IdZonaPicking

                                Set_Dia_Activo(vDia)

                                lblIdZonaPickingTramoOp.Text = "Configuración: " & BeZonaPickingTramoOp.IdZonaPickingTramoOperador
                                lblIdZonaPickingTramoOp.Visible = True

                            End If

                        End If

                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub
    Private Sub Desactivar_Dias()
        chkLunes.Checked = False
        chkMartes.Checked = False
        chkMiercoles.Checked = False
        chkJueves.Checked = False
        chkViernes.Checked = False
        chkSabado.Checked = False
        chkDomingo.Checked = False
    End Sub
    Private Sub Set_Dia_Activo(ByVal pDia As Integer)

        Try

            If pDia = 1 Then
                chkLunes.Checked = True
            ElseIf pDia = 2 Then
                chkMartes.Checked = True
            ElseIf pDia = 3 Then
                chkMiercoles.Checked = True
            ElseIf pDia = 4 Then
                chkJueves.Checked = True
            ElseIf pDia = 5 Then
                chkViernes.Checked = True
            ElseIf pDia = 6 Then
                chkSabado.Checked = True
            ElseIf pDia = 7 Then
                chkDomingo.Checked = True
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmbZonaPicking_EditValueChanged(sender As Object, e As EventArgs) Handles cmbZonaPicking.EditValueChanged

        Dim BeOperadorZonaPickingTramo As New clsBeOperador_zona_picking_tramo
        Dim BeZonaPicking As New clsBeZona_picking

        Try

            If Not cmbZonaPicking.EditValue Is Nothing Then

                Desactivar_Dias()

                BeZonaPicking = clsLnZona_picking.GetSingle(cmbZonaPicking.EditValue)

                If Not BeZonaPicking Is Nothing Then

                    If Not BeZonaPicking.Lista_Zona_Picking_Tramo Is Nothing Then

                        If BeZonaPicking.Lista_Zona_Picking_Tramo.Count > 0 Then

                            Listar_Tramos_Por_Zona_Picking(cmbZonaPicking.EditValue)

                            For Each ZP In BeZonaPicking.Lista_Zona_Picking_Tramo

                                For dia As Integer = 1 To 5

                                    If clsLnOperador_zona_picking_tramo.Exists(BeOperador.IdOperador, BeOperadorZonaPickingTramo.IdZonaPickingTramo, dia) Then

                                        Select Case dia
                                            Case 1
                                                chkLunes.Checked = True
                                            Case 2
                                                chkMartes.Checked = True
                                            Case 3
                                                chkMiercoles.Checked = True
                                            Case 4
                                                chkJueves.Checked = True
                                            Case 5
                                                chkViernes.Checked = True
                                            Case 6
                                                chkSabado.Checked = True
                                            Case 7
                                                chkDomingo.Checked = True
                                        End Select

                                    End If

                                Next

                            Next

                        End If

                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuEliminarTramo_Click(sender As Object, e As EventArgs) Handles mnuEliminarTramo.Click

        Try

            Dim Dr As DataRowView = GridView2.GetFocusedRow
            Dim BeZonaPicking As New clsBeZona_picking()
            Dim lIndex As Integer = -1

            If Not Dr Is Nothing Then

                Dim BeZonaPickingTramoOp As New clsBeOperador_zona_picking_tramo
                Dim vIdZonaPicking As Integer = Dr.Item("IdZonaPicking")
                Dim vIdZonaPickingTramo As Integer = Dr.Item("IdZonaPickingTramo")
                Dim vIdZonaPickingTramoOperador As Integer = Dr.Item("IdZonaPickingTramoOperador")
                Dim vDia As Integer = Dr.Item("NoDia")
                BeZonaPickingTramoOp = clsLnOperador_zona_picking_tramo.Get_Single_By_IdZonaPickingTramoOperador(vIdZonaPickingTramoOperador)

                If Not BeZonaPickingTramoOp Is Nothing Then

                    If XtraMessageBox.Show(String.Format("¿Eliminar configuración:{0}?", vIdZonaPickingTramoOperador), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                        clsLnOperador_zona_picking_tramo.Eliminar(BeZonaPickingTramoOp)

                        XtraMessageBox.Show(String.Format("Configuración: {0} eliminada correctamente de la zona de picking.", vIdZonaPickingTramoOperador),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information)

                        Cargar_Zonas_Picking_Operador()

                    End If

                Else
                    XtraMessageBox.Show(String.Format("No se pudo recuperar la configuración: {0} de la zona de picking.", vIdZonaPickingTramoOperador),
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation)
                End If

            End If


        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Listar_Tramos_Por_Zona_Picking(ByVal pIdZonaPicking As Integer)


        Try

            Dim DT As New DataTable
            DT = clsLnZona_picking_tramo.Get_All_VW_Zona_Picking_Tramo_By_IdZonaPicking(pIdZonaPicking)

            dgridTramosZonaPicking.DataSource = DT

            If (gvTramosPorZona.Columns.Count <> 0) Then

                Try

                    gvTramosPorZona.Columns("IdZonaPickingTramo").SummaryItem.SummaryType = SummaryItemType.Count
                    gvTramosPorZona.Columns("IdZonaPickingTramo").SummaryItem.DisplayFormat = "Registros: {0:n2}"

                    gvTramosPorZona.BestFitColumns()

                Catch ex As Exception
                    XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End Try

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub tbGuardar_Click(sender As Object, e As EventArgs) Handles tbGuardar.Click

        Try

            Dim pOperadorJornadaLaboral = New clsBeOperador_jornada_laboral
            pOperadorJornadaLaboral.IdOperadorJornadaLaboral = clsLnOperador_jornada_laboral.MaxID() + 1
            pOperadorJornadaLaboral.IdJornada = cmbJornadaLaboral.EditValue
            pOperadorJornadaLaboral.User_agr = AP.UsuarioAp.IdUsuario
            pOperadorJornadaLaboral.User_mod = AP.UsuarioAp.IdUsuario
            pOperadorJornadaLaboral.IdOperador = BeOperador.IdOperador
            pOperadorJornadaLaboral.Activo = chkActivo.Checked
            pOperadorJornadaLaboral.Fec_agr = Now
            pOperadorJornadaLaboral.Fec_mod = Now

            'GT 08022021 se obtiene la bodega del combo
            Dim registro As Object = cmbJornadaLaboral.GetSelectedDataRow
            Dim vIdBodega As Integer

            'GT08022022
            If registro Is Nothing Then
                Throw New Exception("Error_20220208_1204: la bodega no es valida.")
            Else
                vIdBodega = registro.Item("IdBodega")
            End If


            If vIdBodega > 0 Then
                pOperadorJornadaLaboral.IdBodega = vIdBodega
            End If

            If pOperadorJornadaLaboral.IdJornada = 0 Then
                Throw New Exception("ERROR_19042023: No se ha seleccionado una jornada laboral para el operador.")
                Exit Sub
            End If


            If clsLnOperador_jornada_laboral.Existe_Jornada_Laboral(pOperadorJornadaLaboral) Then
                XtraMessageBox.Show("El operador ya tiene asignada la Jornada Laboral : " & cmbJornadaLaboral.EditValue & " - " & cmbJornadaLaboral.Text, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            clsLnOperador_jornada_laboral.Insertar(pOperadorJornadaLaboral)
            cargar_jornadas_laborales()
            cmbJornadaLaboral.EditValue = 0
            XtraMessageBox.Show("Registro agregado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)


        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
           Text,
           MessageBoxButtons.OK,
           MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub cargar_jornadas_laborales()
        Try

            listOperadorJornadaLaboral = New List(Of clsBeOperador_jornada_laboral)
            listOperadorJornadaLaboral = clsLnOperador_jornada_laboral.Get_All_By_IdOperador(BeOperador.IdOperador)

            gdListaJornadaLaboral.DataSource = Nothing

            If listOperadorJornadaLaboral.Count > 0 Then

                Dim vNomPresentacionContenidaEnPallet As String = ""
                Dim vNomBodega As String = ""

                DTJornadasLaborales.Rows.Clear()

                For Each Obj As clsBeOperador_jornada_laboral In listOperadorJornadaLaboral.FindAll(Function(b) b.Activo = chkActivoPR.Checked)

                    vNomBodega = clsLnBodega.Get_Nombre_Bodega_By_IdBodega(Obj.IdBodega)

                    DTJornadasLaborales.Rows.Add(Obj.IdOperadorJornadaLaboral,
                                                 Obj.IdOperador,
                                                 vNomBodega,
                                                 Obj.IdJornada,
                                                 Obj.Activo)

                Next

                gdListaJornadaLaboral.DataSource = DTJornadasLaborales

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

    Private Sub cmdDesactivarResolucion_Click(sender As Object, e As EventArgs) Handles cmdDesactivarResolucion.Click
        Try

            Dim BeResolLP As New clsBeResolucion_lp_operador()
            BeResolLP.IdResolucionlp = lblIdResolucionLP.Text
            BeResolLP.User_mod = AP.UsuarioAp.IdUsuario
            BeResolLP.Fec_mod = Now
            clsLnResolucion_lp_operador.Desactivar(BeResolLP)

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