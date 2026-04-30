Imports System.Reflection
Imports System.Threading.Tasks
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Mask
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraSplashScreen

Public Class frmCliente

    Private DT As DataTable

    Public pIdCliente As Integer
    Public pClienteBodegaList As New List(Of clsBeCliente_bodega)
    Public Cliente_Bodega As New clsBeCliente_bodega
    Public Property Propietario As New clsBePropietarios
    Private pClienteTiemposList As New List(Of clsBeCliente_tiempos)
    Private pDirEntList As New List(Of clsBeCliente_direccion)
    Private pIdTiempoCliente As String = String.Empty
    Private pIdDireccionEntregaCliente As String = String.Empty
    Public gBeCliente As New clsBeCliente
    Public Delegate Sub ListarClientes()
    Public Property InvokeListarClientes As ListarClientes

    'Private AreaBodegaGridLookUpEdit As New RepositoryItemGridLookUpEdit

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

    Private Sub ListaBodegas()

        Try

            Grid.BeginUpdate()

            If DT.Rows.Count > 0 Then

                Dim vIdBodega As Integer = 0
                Dim Indice As Integer = 0

                For i As Integer = 0 To DT.Rows.Count - 1

                    vIdBodega = DT(i)(0)
                    Dim lRow As DataRow = DsCliente.Data.NewRow
                    lRow.Item("IdBodega") = vIdBodega
                    lRow.Item("Bodega") = clsLnBodega.Get_Nombre_Bodega_By_IdBodega(vIdBodega)
                    lRow.Item("Selección") = False
                    lRow.Item("IdAreaDestino") = 0

                    Indice = i

                    If TipoTrans.Editar Then

                        If pClienteBodegaList IsNot Nothing AndAlso pClienteBodegaList.Count > 0 Then

                            Parallel.ForEach(pClienteBodegaList, Sub(BeClienteBodega As clsBeCliente_bodega)

                                                                     If BeClienteBodega.IdBodega = CInt(DT(Indice)(0)) AndAlso BeClienteBodega.Activo Then
                                                                         lRow.Item("Selección") = True
                                                                         lRow.Item("IdAsignacion") = BeClienteBodega.IdClienteBodega
                                                                     End If

                                                                     lRow.Item("IdInterno") = BeClienteBodega.IdClienteBodega
                                                                     lRow.Item("IdAreaDestino") = BeClienteBodega.IdAreaDestino

                                                                 End Sub)

                            Llena_AreaLookUp_Grid(vIdBodega)

                        End If

                    End If

                    If Not lRow Is Nothing Then
                        DsCliente.Data.AddDataRow(lRow)
                    End If

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

                If pClienteBodegaList IsNot Nothing Then
                    lIndex = pClienteBodegaList.FindIndex(Function(b) b.IdBodega = CInt(Dr.Item("IdBodega")) _
                                                      And b.IdCliente = gBeCliente.IdCliente)
                End If

                If lIndex > -1 Then

                    If ritem.Checked Then
                        pClienteBodegaList(lIndex).Activo = True
                    Else
                        pClienteBodegaList(lIndex).Activo = False
                    End If

                    pClienteBodegaList(lIndex).User_mod = AP.UsuarioAp.IdUsuario
                    pClienteBodegaList(lIndex).Fec_mod = Now

                    If Dr.Item("IdAreaDestino") > 0 Then
                        pClienteBodegaList(lIndex).IdAreaDestino = CInt(Dr.Item("IdAreaDestino"))
                    Else
                        pClienteBodegaList(lIndex).IdAreaDestino = Nothing
                    End If

                Else

                    If pClienteBodegaList Is Nothing Then
                        pClienteBodegaList = New List(Of clsBeCliente_bodega)
                    End If

                    Dim beClienteBodega As New clsBeCliente_bodega() With {.IdBodega = Dr.Item("IdBodega"),
                        .IdCliente = gBeCliente.IdCliente,
                        .User_agr = AP.UsuarioAp.IdUsuario,
                        .Fec_agr = Now,
                        .User_mod = AP.UsuarioAp.IdUsuario,
                        .Fec_mod = Now,
                        .Activo = True}

                    If Dr.Item("IdAreaDestino") > 0 Then
                        beClienteBodega.IdAreaDestino = CInt(Dr.Item("IdAreaDestino"))
                    Else
                        beClienteBodega.IdAreaDestino = Nothing
                    End If

                    pClienteBodegaList.Add(beClienteBodega)

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub AreaGridLookUpEdit_Leave(ByVal sender As Object, ByVal e As EventArgs) Handles AreaBodegaGridLookUpEdit.Leave

        Try

            Dim lista As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
            If lista.EditValue Is Nothing Then Return
            Dim drLineaGrid As DataRow = gridView1.GetFocusedDataRow()
            If drLineaGrid Is Nothing Then Return

            Dim vBeAreaBodega As Object = lista.Properties.GetRowByKeyValue(lista.EditValue)

            If Not vBeAreaBodega Is Nothing Then

                Dim drArea As DataRow = (TryCast(lista.Properties.GetRowByKeyValue(lista.EditValue), DataRowView)).Row
                If drArea Is Nothing Then Return

                drLineaGrid("IdAreaDestino") = drArea("IdArea")

                Dim Dr As DataRowView = gridView1.GetFocusedRow
                Dim lIndex As Integer = -1

                If pClienteBodegaList IsNot Nothing Then
                    lIndex = pClienteBodegaList.FindIndex(Function(b) b.IdBodega = CInt(Dr.Item("IdBodega")) _
                                                      And b.IdCliente = gBeCliente.IdCliente)
                End If

                If lIndex > -1 Then

                    pClienteBodegaList(lIndex).User_mod = AP.UsuarioAp.IdUsuario
                    pClienteBodegaList(lIndex).Fec_mod = Now

                    If Dr.Item("IdAreaDestino") > 0 Then
                        pClienteBodegaList(lIndex).IdAreaDestino = CInt(Dr.Item("IdAreaDestino"))
                    Else
                        pClienteBodegaList(lIndex).IdAreaDestino = Nothing
                    End If

                Else

                    If pClienteBodegaList Is Nothing Then
                        pClienteBodegaList = New List(Of clsBeCliente_bodega)
                    End If

                    Dim beClienteBodega As New clsBeCliente_bodega() With {.IdBodega = Dr.Item("IdBodega"),
                        .IdCliente = gBeCliente.IdCliente,
                        .User_agr = AP.UsuarioAp.IdUsuario,
                        .Fec_agr = Now,
                        .User_mod = AP.UsuarioAp.IdUsuario,
                        .Fec_mod = Now,
                        .Activo = True}

                    If Dr.Item("IdAreaDestino") > 0 Then
                        beClienteBodega.IdAreaDestino = CInt(Dr.Item("IdAreaDestino"))
                    Else
                        beClienteBodega.IdAreaDestino = Nothing
                    End If

                    pClienteBodegaList.Add(beClienteBodega)

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub ValidaBodegas()

        Try

            DsCliente.Clear()
            DT = IMS.Listar_Bodegas()

            pClienteBodegaList = New List(Of clsBeCliente_bodega)
            pClienteBodegaList = clsLnCliente_bodega.Get_All_By_IdCliente(gBeCliente.IdCliente)

            If Not pClienteBodegaList Is Nothing Then
                pClienteBodegaList = pClienteBodegaList.ToList()
            End If

            ListaBodegas()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub frmCliente_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            pClienteTiemposList = New List(Of clsBeCliente_tiempos)

            IMS.Listar_Empresas(cmbEmpresa)
            IMS.Listar_Propietarios_By_IdEmpresa(lcmbPropietario, cmbEmpresa.EditValue)
            IMS.Listar_TipoCliente(cmbTipoCliente)
            IMS.Listar_Paises(cmbPais)
            IMS.Listar_Bodegas_Por_Empresa(cmbBodegaWMS, cmbEmpresa.EditValue)
            IMS.Listar_Areas_By_IdBodega_For_Combo(cmbBodegaAreaSAP, AP.IdBodega)
            IMS.Listar_Ubicaciones_Despacho_By_IdBodega(txtIdUbicacionAbastecerCon, AP.IdBodega)

            ValidaBodegas()

            txtTelefono.Properties.Mask.EditMask = "\((\d{3})\) (\d{4})-(\d{4})"
            txtTelefono.Properties.Mask.MaskType = MaskType.RegEx

            Select Case Modo

                Case TipoTrans.Nuevo

                    lblCodigo.Text = clsLnCliente.MaxID()
                    lblCodigo.Enabled = False
                    User_agrTextEdit.Text = AP.UsuarioAp.IdUsuario
                    Fec_agrDateEdit.Text = Now
                    User_modTextEdit.Text = AP.UsuarioAp.IdUsuario
                    Fec_modDateEdit.Text = Now

                    If OpcionesMenu IsNot Nothing Then
                        mnuGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False
                    mnuAsignacion.Enabled = False

                    If Propietario.IdPropietario <> 0 Then
                        lcmbPropietario.EditValue = Propietario.IdPropietario
                        lcmbPropietario.Enabled = False
                    End If

                Case TipoTrans.Editar

                    Cargar_Cliente()
                    Cargar_Tiempos_Aceptacion(False)
                    Cargar_Direcciones_Entrega(False)

                    lblCodigo.Enabled = False
                    mnuGuardar.Enabled = False

                    If OpcionesMenu IsNot Nothing Then
                        mnuActualizar.Enabled = OpcionesMenu.Modificar
                        mnuEliminar.Enabled = OpcionesMenu.Eliminar
                    End If

                    mnuAsignacion.Enabled = True

            End Select

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Cargar_Cliente()

        Try

            lcmbPropietario.EditValue = gBeCliente.IdPropietario
            lblCodigo.Text = gBeCliente.IdCliente
            txtCodigo.Text = gBeCliente.Codigo
            txtNombreComercial.Text = gBeCliente.Nombre_comercial
            txtContacto.Text = gBeCliente.Nombre_contacto
            txtTelefono.Text = gBeCliente.Telefono
            txtNit.Text = gBeCliente.Nit
            txtDireccion.Text = gBeCliente.Direccion
            txtCorreo.Text = gBeCliente.Correo_electronico
            chkManufactura.Checked = gBeCliente.Realiza_manufactura
            cmbTipoCliente.EditValue = gBeCliente.IdTipoCliente
            chkEsBodRecep.Checked = gBeCliente.Es_bodega_recepcion
            chkEsBodegaTraslado.Checked = gBeCliente.Es_Bodega_Traslado
            chkActivo.Checked = gBeCliente.Activo
            chkSistema.Checked = gBeCliente.Sistema
            chkControlUltimoLote.Checked = gBeCliente.Control_Ultimo_Lote
            chkControlCalidad.Checked = gBeCliente.Control_Calidad
            User_agrTextEdit.Text = gBeCliente.User_agr
            Fec_agrDateEdit.Text = gBeCliente.Fec_agr
            User_modTextEdit.Text = gBeCliente.User_mod
            Fec_modDateEdit.Text = gBeCliente.Fec_mod

            If gBeCliente.IdUbicacionVirtual <> 0 Then
                cmbBodegaWMS.EditValue = gBeCliente.IdUbicacionVirtual
            End If

            txtReferenciaCliente.Text = gBeCliente.Referencia

            If gBeCliente.IdPropietario <> 0 Then
                lcmbPropietario.EditValue = gBeCliente.IdPropietario
                lcmbPropietario.Enabled = False
            End If

            '#EJC20220329: IdUbicacionAbastecerCon
            If gBeCliente.IdUbicacionAbastecerCon <> 0 Then
                txtIdUbicacionAbastecerCon.EditValue = gBeCliente.IdUbicacionAbastecerCon
            Else
                txtIdUbicacionAbastecerCon.EditValue = Nothing
            End If

            cmbBodegaAreaSAP.EditValue = gBeCliente.IdBodegaAreaSAP
            chkEsProveedor.Checked = gBeCliente.Es_Proveedor

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Cargar_Tiempos_Aceptacion(ByVal pGuardo As Boolean)

        Try

            If pGuardo = False Then

                pClienteTiemposList = clsLnCliente_tiempos.Get_All_Tiempos_By_IdCliente(gBeCliente.IdCliente)

            End If

            If pClienteTiemposList Is Nothing Then Exit Sub

            Dim DT As New DataTable("Tiempo")
            DT.Columns.Add("Código", GetType(Integer))
            DT.Columns.Add("IdFamilia", GetType(Integer))
            DT.Columns.Add("Familia", GetType(String))
            DT.Columns.Add("IdClasificacion", GetType(Integer))
            DT.Columns.Add("Clasificación", GetType(String))
            DT.Columns.Add("Días Locales", GetType(Integer))
            DT.Columns.Add("Días Exteriores", GetType(Integer))
            DT.Columns.Add("Es Manufactura", GetType(Boolean))

            Dim Correlativo As Integer = 1

            If pClienteTiemposList.Count > 0 Then
                Correlativo = pClienteTiemposList.Max(Function(m) m.IdTiempoCliente)
            End If

            Parallel.ForEach(pClienteTiemposList.OrderBy(Function(o) o.IdTiempoCliente), Sub(Obj As clsBeCliente_tiempos)
                                                                                             SyncLock DT

                                                                                                 If Obj.IdTiempoCliente = 0 AndAlso Obj.IdCliente = 0 Then
                                                                                                     Correlativo += 1
                                                                                                     Obj.IdTiempoCliente = Correlativo
                                                                                                 End If

                                                                                                 Dim lRow As DataRow = DT.NewRow()

                                                                                                 lRow(0) = Obj.IdTiempoCliente

                                                                                                 If Obj.IdFamilia <> Nothing AndAlso Obj.IdFamilia <> 0 Then
                                                                                                     lRow(1) = Obj.IdFamilia
                                                                                                     lRow(2) = Obj.Familia.Nombre
                                                                                                 End If

                                                                                                 If Obj.IdClasificacion <> Nothing AndAlso Obj.IdClasificacion <> 0 Then
                                                                                                     lRow(3) = Obj.IdClasificacion
                                                                                                     lRow(4) = Obj.Clasificacion.Nombre
                                                                                                 End If

                                                                                                 lRow(5) = Obj.Dias_Local
                                                                                                 lRow(6) = Obj.Dias_Exterior
                                                                                                 lRow(7) = Obj.Es_Manufactura

                                                                                                 DT.Rows.Add(lRow)

                                                                                             End SyncLock
                                                                                         End Sub)

            GridTiempo.DataSource = DT
            GridViewTiempo.Columns("IdFamilia").Visible = False
            GridViewTiempo.Columns("IdClasificacion").Visible = False
            GridTiempo.Refresh()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Cargar_Direcciones_Entrega(ByVal pGuardo As Boolean)

        Try

            If pGuardo = False Then
                pDirEntList = clsLnCliente_direccion.GetAllDireccionesByCliente(gBeCliente.IdCliente)
            End If

            If pDirEntList Is Nothing Then Exit Sub

            Dim DT As New DataTable("DirEnt")
            DT.Columns.Add("Codigo", GetType(Integer))
            DT.Columns.Add("Pais", GetType(String))
            DT.Columns.Add("Departamento", GetType(String))
            DT.Columns.Add("Region", GetType(String))
            DT.Columns.Add("Municipio", GetType(String))
            DT.Columns.Add("Avenida", GetType(String))
            DT.Columns.Add("Calle", GetType(String))
            DT.Columns.Add("NoCasa", GetType(String))
            DT.Columns.Add("Zona", GetType(String))
            DT.Columns.Add("Direccion", GetType(String))
            DT.Columns.Add("Referencia", GetType(String))

            Dim Correlativo As Integer = 1

            If pDirEntList.Count > 0 Then
                Correlativo = pDirEntList.Max(Function(m) m.IdDireccion)
            End If

            Dim bePais As New clsBePaises
            Dim beDepto As New clsBePais_departamento
            Dim beRegion As New clsBePais_region
            Dim beMunicipio As New clsBePais_municipio

            Parallel.ForEach(pDirEntList.OrderBy(Function(o) o.IdDireccion), Sub(DirEnt As clsBeCliente_direccion)
                                                                                 SyncLock DT
                                                                                     If DirEnt.IdDireccion = 0 AndAlso DirEnt.IdCliente = 0 Then

                                                                                         Correlativo += 1
                                                                                         DirEnt.IdDireccion = Correlativo

                                                                                     End If

                                                                                     Dim lRow As DataRow = DT.NewRow()

                                                                                     lRow("Codigo") = DirEnt.IdDireccion

                                                                                     If DirEnt.IdMunicipio <> Nothing AndAlso DirEnt.IdMunicipio <> 0 Then

                                                                                         beMunicipio.IdMunicipio = DirEnt.IdMunicipio

                                                                                         If clsLnPais_municipio.Obtener(beMunicipio) Then

                                                                                             lRow("Municipio") = beMunicipio.Nombre

                                                                                             DirEnt.IdDepartamento = clsLnPais_municipio.GetIdDepartamentoByIdMunicipio(beMunicipio.IdMunicipio)
                                                                                             DirEnt.IdPais = clsLnPais_departamento.GetIdPaisByIdDepartamento(DirEnt.IdDepartamento)

                                                                                         End If

                                                                                     End If

                                                                                     If DirEnt.IdPais <> Nothing AndAlso DirEnt.IdPais <> 0 Then

                                                                                         bePais.IdPais = DirEnt.IdPais

                                                                                         If clsLnPaises.Obtener(bePais) Then
                                                                                             lRow("Pais") = bePais.NOMBRE
                                                                                         End If

                                                                                     End If

                                                                                     If DirEnt.IdDepartamento <> Nothing AndAlso DirEnt.IdDepartamento <> 0 Then

                                                                                         beDepto.IdDepartamento = DirEnt.IdDepartamento

                                                                                         If clsLnPais_departamento.Obtener(beDepto) Then
                                                                                             lRow("Departamento") = beDepto.Nombre
                                                                                         End If

                                                                                     End If

                                                                                     If DirEnt.IdRegion <> Nothing AndAlso DirEnt.IdRegion <> 0 Then

                                                                                         beRegion.IdRegion = DirEnt.IdRegion

                                                                                         If clsLnPais_region.Obtener(beRegion) Then
                                                                                             lRow("Region") = beRegion.Nombre
                                                                                         End If

                                                                                     End If

                                                                                     lRow("Avenida") = DirEnt.Avenida
                                                                                     lRow("Calle") = DirEnt.Calle
                                                                                     lRow("NoCasa") = DirEnt.No_Casa
                                                                                     lRow("Zona") = DirEnt.Zona
                                                                                     lRow("Direccion") = DirEnt.Direccion
                                                                                     lRow("Referencia") = DirEnt.Referencia

                                                                                     DT.Rows.Add(lRow)
                                                                                 End SyncLock
                                                                             End Sub)

            dgridDirecciones.DataSource = DT
            dgridDirecciones.Refresh()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
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

            Dim BeCliente As New clsBeCliente()

            With BeCliente

                .IdEmpresa = cmbEmpresa.EditValue
                .IdPropietario = lcmbPropietario.EditValue
                .IdTipoCliente = cmbTipoCliente.EditValue
                .Codigo = txtCodigo.Text.Trim()
                .Nombre_comercial = txtNombreComercial.Text.Trim()
                .Nombre_contacto = txtContacto.Text.Trim()
                .Telefono = txtTelefono.Text.Trim()
                .Nit = txtNit.Text.Trim()
                .Direccion = txtDireccion.Text.Trim()
                .Correo_electronico = txtCorreo.Text.Trim()
                .Realiza_manufactura = chkManufactura.Checked
                .User_agr = AP.UsuarioAp.IdUsuario
                .Fec_agr = Now
                .User_mod = AP.UsuarioAp.IdUsuario
                .Fec_mod = Now
                .Sistema = chkSistema.Checked
                .Es_bodega_recepcion = chkEsBodRecep.Checked
                .Es_Bodega_Traslado = chkEsBodegaTraslado.Checked
                If cmbBodegaWMS.EditValue IsNot Nothing Then
                    .IdUbicacionVirtual = cmbBodegaWMS.EditValue
                End If

                .Referencia = txtReferenciaCliente.Text.Trim
                .Activo = True
                .Control_Ultimo_Lote = chkControlUltimoLote.Checked
                .Control_Calidad = chkControlCalidad.Checked
                .IdUbicacionAbastecerCon = txtIdUbicacionAbastecerCon.EditValue
                .IdBodegaAreaSAP = cmbBodegaAreaSAP.EditValue
                .Es_Proveedor = chkEsProveedor.Checked

            End With

            '#EJC202210200814:Si el cliente no está asignado a la bodega por defecto, se asocia a la bodega.
            If Not clsLnCliente.Tiene_Cliente_Bodega(lblCodigo.Text.Trim,
                                                     cmbBodegaWMS.EditValue) Then

                pClienteBodegaList = New List(Of clsBeCliente_bodega)

                Cliente_Bodega = New clsBeCliente_bodega()

                '#GT20102022_0900: si no hay bodega en el combo, se deja la bodega de la sesión
                If cmbBodegaWMS.EditValue Is Nothing Then
                    Cliente_Bodega.IdBodega = AP.IdBodega
                Else
                    Cliente_Bodega.IdBodega = cmbBodegaWMS.EditValue
                End If

                Cliente_Bodega.IdCliente = lblCodigo.Text
                Cliente_Bodega.User_agr = AP.UsuarioAp.IdUsuario
                Cliente_Bodega.Fec_agr = Date.Now
                Cliente_Bodega.User_mod = AP.UsuarioAp.IdUsuario
                Cliente_Bodega.Fec_mod = Date.Now
                Cliente_Bodega.Activo = True

                pClienteBodegaList.Add(Cliente_Bodega)

            End If

            clsLnCliente.Guardar_Transaccion(BeCliente,
                                             pClienteTiemposList,
                                             pClienteBodegaList,
                                             pDirEntList)

            Return True

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            If Datos_Correctos() Then

                gBeCliente.Codigo = txtCodigo.Text.Trim()
                gBeCliente.Nombre_comercial = txtNombreComercial.Text.Trim()
                gBeCliente.Nombre_contacto = txtContacto.Text.Trim()
                gBeCliente.Telefono = txtTelefono.Text.Trim()
                gBeCliente.Nit = txtNit.Text.Trim()
                gBeCliente.Direccion = txtDireccion.Text.Trim()
                gBeCliente.Correo_electronico = txtCorreo.Text.Trim()
                gBeCliente.Realiza_manufactura = chkManufactura.Checked
                gBeCliente.User_mod = AP.UsuarioAp.IdUsuario
                gBeCliente.Fec_mod = Now
                gBeCliente.Sistema = chkSistema.Checked
                gBeCliente.Es_bodega_recepcion = chkEsBodRecep.Checked
                gBeCliente.Activo = chkActivo.Checked
                gBeCliente.Es_Bodega_Traslado = chkEsBodegaTraslado.Checked
                gBeCliente.IdUbicacionVirtual = cmbBodegaWMS.EditValue
                gBeCliente.Referencia = txtReferenciaCliente.Text.Trim
                gBeCliente.Control_Ultimo_Lote = chkControlUltimoLote.Checked
                gBeCliente.Control_Calidad = chkControlCalidad.Checked
                gBeCliente.IdUbicacionAbastecerCon = txtIdUbicacionAbastecerCon.EditValue
                gBeCliente.IdBodegaAreaSAP = cmbBodegaAreaSAP.EditValue
                gBeCliente.Es_Proveedor = chkEsProveedor.Checked

                clsLnCliente.Guardar_Transaccion(gBeCliente,
                                                 pClienteTiemposList,
                                                 pClienteBodegaList,
                                                 pDirEntList)
                Actualizar = True

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick
        Try
            If Datos_Correctos() Then
                If MessageBox.Show("¿Guardar Datos?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If Guardar() Then
                        XtraMessageBox.Show("Se guardaron los datos", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        If Not InvokeListarClientes Is Nothing Then InvokeListarClientes.Invoke()
                        Close()
                    End If
                End If
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

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        Try
            If Actualizar() Then
                XtraMessageBox.Show("Se actualizaron los datos", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                If Not InvokeListarClientes Is Nothing Then InvokeListarClientes.Invoke
                Close()
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

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try

            If MessageBox.Show("¿Desactivar Datos?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                gBeCliente.Activo = False

                clsLnCliente.Actualizar(gBeCliente)

                XtraMessageBox.Show("Se han desactivado los registros", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                If Not InvokeListarClientes Is Nothing Then InvokeListarClientes.Invoke()

                Close()

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub lnkClasificacion_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkClasificacion.LinkClicked

        txtIdClasificacion.Text = String.Empty
        txtNombreClasificacion.Text = String.Empty

        Try

            Dim Clasificacion As New frmProducto_ClasificacionList() With {.Modo = frmProducto_ClasificacionList.pModo.Seleccion}

            If OpcionesMenu IsNot Nothing Then
                Clasificacion.OpcionesMenu = OpcionesMenu
                Clasificacion.mnuActualizar.Enabled = OpcionesMenu.Leer
                Clasificacion.mnuNuevo.Enabled = OpcionesMenu.Modificar
            End If

            Clasificacion.ShowDialog()

            If Not Clasificacion.pObjPC Is Nothing Then

                If Clasificacion.pObjPC.IdClasificacion <> 0 Then
                    txtIdClasificacion.Text = Clasificacion.pObjPC.IdClasificacion
                    txtNombreClasificacion.Text = Clasificacion.pObjPC.Nombre
                End If

            End If

            Clasificacion.Close()
            Clasificacion.Dispose()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub lnkFamilia_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkFamilia.LinkClicked

        txtIdFamilia.Text = String.Empty
        txtNombreFamilia.Text = String.Empty

        Try

            Dim Familia As New frmProducto_FamiliaList() With {.Modo = frmProducto_FamiliaList.pModo.Seleccion}

            If OpcionesMenu IsNot Nothing Then
                Familia.OpcionesMenu = OpcionesMenu
                Familia.mnuActualizar.Enabled = OpcionesMenu.Leer
                Familia.mnuNuevo.Enabled = OpcionesMenu.Modificar
            End If

            Familia.ShowDialog()

            If Not Familia.pObjPF Is Nothing Then
                If Familia.pObjPF.IdFamilia <> 0 Then
                    txtIdFamilia.Text = Familia.pObjPF.IdFamilia
                    txtNombreFamilia.Text = Familia.pObjPF.Nombre
                End If
            End If

            Familia.Close()
            Familia.Dispose()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub LimpiarCamposTiemposCliente()

        txtIdClasificacion.Text = String.Empty
        txtNombreClasificacion.Text = String.Empty
        txtIdFamilia.Text = String.Empty
        txtNombreFamilia.Text = String.Empty
        txtDiaLocal.Text = 0
        txtDiaExterior.Text = 0

    End Sub

    Private Sub LimpiarCamposDireccionesEntrega()

        chkLocal.Checked = True
        txtNoCasa.Text = ""
        txtReferenciaDireccion.Text = ""
        txtZona.Text = ""
        txtAvenida.Text = ""
        txtCalle.Text = ""
        txtCordX.Text = ""
        txtCordY.Text = ""
        txtDireccionEntrega.Text = ""

    End Sub

    Private Sub txtDiaLocal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDiaLocal.KeyPress

        If Not (Char.IsControl(e.KeyChar) And e.KeyChar <> ".") Then
            e.Handled = True
        End If

        If e.KeyChar = "." Then
            e.Handled = True
        End If

        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        End If

    End Sub

    Private Sub txtDiaExterior_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDiaExterior.KeyPress

        If Not (Char.IsControl(e.KeyChar) And e.KeyChar <> ".") Then
            e.Handled = True
        End If

        If e.KeyChar = "." Then
            e.Handled = True
        End If

        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        End If

    End Sub

    Private Sub GridTiempo_DoubleClick(sender As Object, e As EventArgs) Handles GridTiempo.DoubleClick

        LimpiarCamposTiemposCliente()

        Try

            Dim Dr As DataRowView = GridViewTiempo.GetFocusedRow

            pIdTiempoCliente = CStr(Dr.Item("Código"))

            If Dr.Item("IdFamilia") IsNot DBNull.Value AndAlso Dr.Item("IdFamilia") IsNot Nothing Then

                txtIdFamilia.Text = CInt(Dr.Item("IdFamilia"))

                Dim lIndex As Integer = -1

                lIndex = pClienteTiemposList.FindIndex(Function(f) f.IdFamilia = CInt(txtIdFamilia.Text.Trim()))

                If lIndex > -1 Then
                    txtNombreFamilia.Text = pClienteTiemposList(lIndex).Familia.Nombre
                End If

            End If

            If Dr.Item("IdClasificacion") IsNot DBNull.Value AndAlso Dr.Item("IdClasificacion") IsNot Nothing Then

                txtIdClasificacion.Text = CInt(Dr.Item("IdClasificacion"))

                Dim lIndex As Integer = -1

                lIndex = pClienteTiemposList.FindIndex(Function(c) c.IdClasificacion = CInt(txtIdClasificacion.Text.Trim()))

                If lIndex > -1 Then
                    txtNombreClasificacion.Text = pClienteTiemposList(lIndex).Clasificacion.Nombre
                End If

            End If

            txtDiaLocal.Text = CInt(Dr.Item("Días Locales"))
            txtDiaExterior.Text = CInt(Dr.Item("Días Exteriores"))

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            pIdTiempoCliente = String.Empty
        End Try

    End Sub

    Private Sub txtIdClasificacion_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtIdClasificacion.PreviewKeyDown

        Try

            If e.KeyData = Keys.Tab Then

                If txtIdClasificacion.Text > "0" Then

                    If String.IsNullOrEmpty(txtIdClasificacion.Text.Trim()) = False Then

                        Dim Obj As New clsBeProducto_clasificacion

                        Obj = clsLnProducto_clasificacion.GetSingle(txtIdClasificacion.Text.Trim())

                        If Obj IsNot Nothing AndAlso Obj.IdClasificacion > 0 Then
                            txtNombreClasificacion.Text = Obj.Nombre
                        Else
                            XtraMessageBox.Show(String.Format("No existe Clasificación con código {0}", txtIdClasificacion.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            txtNombreClasificacion.Text = String.Empty
                            txtIdClasificacion.Focus() : txtIdClasificacion.SelectAll()
                        End If

                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub txtIdFamilia_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtIdFamilia.PreviewKeyDown

        Try

            If e.KeyData = Keys.Tab Then

                If txtIdFamilia.Text > "0" Then

                    If String.IsNullOrEmpty(txtIdFamilia.Text.Trim()) = False Then

                        Dim Obj As New clsBeProducto_familia

                        Obj = clsLnProducto_familia.GetSingle(txtIdFamilia.Text.Trim())

                        If Obj IsNot Nothing AndAlso Obj.IdFamilia > 0 Then
                            txtNombreFamilia.Text = Obj.Nombre
                        Else
                            XtraMessageBox.Show(String.Format("No existe Familia con código {0}", txtIdFamilia.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            txtNombreFamilia.Text = String.Empty
                            txtIdFamilia.Focus() : txtIdFamilia.SelectAll()
                        End If

                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtIdClasificacion_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIdClasificacion.KeyPress
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
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub txtIdFamilia_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIdFamilia.KeyPress
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
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub cmdAgregarDireccion_Click(sender As Object, e As EventArgs) Handles cmdAgregarDireccion.Click

        Try

            If String.IsNullOrEmpty(txtDireccionEntrega.Text) And String.IsNullOrEmpty(txtDireccionEntrega.Text) Then
                XtraMessageBox.Show("Ingrese dirección.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim lIndex As Integer = -1

            If pIdDireccionEntregaCliente = String.Empty Then pIdDireccionEntregaCliente = "0"

            lIndex = pDirEntList.FindIndex(Function(b) b.IdDireccion = CInt(pIdDireccionEntregaCliente))

            Dim vDirEntClie As New clsBeCliente_direccion

            If lIndex = -1 Then 'No Existe

                vDirEntClie = New clsBeCliente_direccion
                vDirEntClie.IdDireccion = 0
                vDirEntClie.IdMunicipio = cmbMunicipio.EditValue
                vDirEntClie.IdPais = cmbPais.EditValue
                vDirEntClie.IdRegion = cmbRegion.EditValue
                vDirEntClie.IdDepartamento = cmbDepartamento.EditValue
                vDirEntClie.Local = chkLocal.Checked
                vDirEntClie.No_Casa = txtNoCasa.Text
                vDirEntClie.Referencia = txtReferenciaDireccion.Text
                vDirEntClie.Zona = txtZona.Text
                vDirEntClie.Avenida = txtAvenida.Text
                vDirEntClie.Calle = txtCalle.Text
                vDirEntClie.Coordenada_x = txtCordX.Text
                vDirEntClie.Coordenada_y = txtCordY.Text
                vDirEntClie.Direccion = txtDireccionEntrega.Text
                vDirEntClie.Fec_agr = Now
                vDirEntClie.Fec_mod = Now
                vDirEntClie.User_agr = AP.UsuarioAp.IdUsuario
                vDirEntClie.User_mod = AP.UsuarioAp.IdUsuario
                vDirEntClie.Activo = True

                pDirEntList.Add(vDirEntClie)

            Else 'Si existe

                pDirEntList(lIndex).IdMunicipio = cmbMunicipio.EditValue
                pDirEntList(lIndex).IdPais = cmbPais.EditValue
                pDirEntList(lIndex).IdRegion = cmbRegion.EditValue
                pDirEntList(lIndex).IdDepartamento = cmbDepartamento.EditValue
                pDirEntList(lIndex).Local = chkLocal.Checked
                pDirEntList(lIndex).No_Casa = txtNoCasa.Text
                pDirEntList(lIndex).Referencia = txtReferenciaDireccion.Text
                pDirEntList(lIndex).Zona = txtZona.Text
                pDirEntList(lIndex).Avenida = txtAvenida.Text
                pDirEntList(lIndex).Calle = txtCalle.Text
                pDirEntList(lIndex).Coordenada_x = txtCordX.Text
                pDirEntList(lIndex).Coordenada_y = txtCordY.Text
                pDirEntList(lIndex).Direccion = txtDireccionEntrega.Text

                pDirEntList(lIndex).Fec_mod = Now
                pDirEntList(lIndex).User_mod = AP.UsuarioAp.IdUsuario

            End If

            Cargar_Direcciones_Entrega(True)

            LimpiarCamposDireccionesEntrega()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdGuardar_Click(sender As Object, e As EventArgs) Handles cmdGuardar.Click

        Try

            If String.IsNullOrEmpty(txtIdClasificacion.Text) And String.IsNullOrEmpty(txtIdFamilia.Text) Then
                XtraMessageBox.Show("Seleccione Clasificación o Familia.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return
            ElseIf txtDiaLocal.Text > 0 = False Then
                XtraMessageBox.Show("Ingrese días locales mayor a 0.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return
            ElseIf txtDiaExterior.Text > 0 = False Then
                XtraMessageBox.Show("Ingrese días exteriorees mayor a 0.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return
            End If

            Dim ListC As New List(Of clsBeCliente_tiempos)
            Dim ListF As New List(Of clsBeCliente_tiempos)

            Dim lIndex As Integer = -1

            If pIdTiempoCliente = String.Empty Then pIdTiempoCliente = "0"


            If pClienteTiemposList IsNot Nothing Then
                lIndex = pClienteTiemposList.FindIndex(Function(b) b.IdTiempoCliente = CInt(pIdTiempoCliente))
            Else
                pClienteTiemposList = New List(Of clsBeCliente_tiempos)()
            End If

            If lIndex > -1 Then

                If String.IsNullOrEmpty(txtIdClasificacion.Text.Trim()) = False Then

                    ListC = pClienteTiemposList.FindAll(Function(x) x.IdClasificacion = CInt(txtIdClasificacion.Text.Trim()))

                    If String.IsNullOrEmpty(txtIdFamilia.Text) = False AndAlso ListC.Count > 0 Then

                        lIndex = pClienteTiemposList.FindIndex(Function(cf) cf.IdClasificacion = CInt(txtIdClasificacion.Text.Trim()) _
                                                                     AndAlso cf.IdFamilia = CInt(txtIdFamilia.Text.Trim()) _
                                                                     AndAlso cf.IdTiempoCliente = CInt(pIdTiempoCliente))
                        'If a Then
                        '    XtraMessageBox.Show(String.Format("La clasificación {0} y la familia {1} ya están ingresadas.", txtNombreClasificacion.Text.Trim(), txtNombreFamilia.Text.Trim()),
                        '                        Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Return
                        'End If

                    ElseIf String.IsNullOrEmpty(txtIdFamilia.Text) AndAlso ListC.Count > 1 AndAlso Not pClienteTiemposList(lIndex).IdFamilia <> 0 Then
                        Dim b As Boolean = pClienteTiemposList.Exists(Function(cl) cl.IdClasificacion = CInt(txtIdClasificacion.Text.Trim()))
                        If b Then
                            XtraMessageBox.Show(String.Format("La clasificación {0} ya está ingresada.", txtNombreClasificacion.Text.Trim()),
                                                Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Return
                        End If
                    End If
                    pClienteTiemposList(lIndex).IdClasificacion = CInt(txtIdClasificacion.Text.Trim())
                    pClienteTiemposList(lIndex).Clasificacion.Nombre = txtNombreClasificacion.Text.Trim()
                Else
                    pClienteTiemposList(lIndex).IdClasificacion = Nothing
                    pClienteTiemposList(lIndex).Clasificacion.Nombre = String.Empty
                End If

                If String.IsNullOrEmpty(txtIdFamilia.Text.Trim()) = False AndAlso pClienteTiemposList(lIndex).IdClasificacion <> 0 Then
                    ListF = New List(Of clsBeCliente_tiempos)
                    ListF = pClienteTiemposList.FindAll(Function(x) x.IdFamilia = CInt(txtIdFamilia.Text.Trim()))
                    If String.IsNullOrEmpty(txtIdClasificacion.Text) AndAlso ListF.Count > 0 Then
                        Dim c As Boolean = pClienteTiemposList.Exists(Function(cl) cl.IdFamilia = CInt(txtIdFamilia.Text.Trim()))
                        If c Then
                            XtraMessageBox.Show(String.Format("La familia {0} ya está ingresada.", txtNombreFamilia.Text.Trim()),
                                                Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Return
                        End If
                    End If

                    pClienteTiemposList(lIndex).IdFamilia = CInt(txtIdFamilia.Text.Trim())
                    pClienteTiemposList(lIndex).Familia.Nombre = txtNombreFamilia.Text.Trim()
                Else
                    pClienteTiemposList(lIndex).IdFamilia = Nothing
                    pClienteTiemposList(lIndex).Familia.Nombre = String.Empty
                End If

                pClienteTiemposList(lIndex).Dias_Local = CInt(txtDiaLocal.Text)
                pClienteTiemposList(lIndex).Dias_Exterior = CInt(txtDiaExterior.Text)
                pClienteTiemposList(lIndex).User_mod = AP.UsuarioAp.IdUsuario
                pClienteTiemposList(lIndex).Fec_mod = Now

            Else
                Dim BeClienteTiempos As New clsBeCliente_tiempos() With {.Clasificacion = New clsBeProducto_clasificacion(), .Familia = New clsBeProducto_familia()}

                If String.IsNullOrEmpty(txtIdClasificacion.Text.Trim()) = False Then
                    BeClienteTiempos.IdClasificacion = CInt(txtIdClasificacion.Text.Trim())
                    BeClienteTiempos.Clasificacion.Nombre = txtNombreClasificacion.Text.Trim()
                    If pClienteTiemposList IsNot Nothing AndAlso pClienteTiemposList.Count > 0 Then
                        ListC = pClienteTiemposList.FindAll(Function(x) x.IdClasificacion = BeClienteTiempos.IdClasificacion)
                    End If
                End If
                If String.IsNullOrEmpty(txtIdFamilia.Text.Trim()) = False Then
                    BeClienteTiempos.IdFamilia = CInt(txtIdFamilia.Text.Trim())
                    BeClienteTiempos.Familia.Nombre = txtNombreFamilia.Text.Trim()
                    If pClienteTiemposList IsNot Nothing AndAlso pClienteTiemposList.Count > 0 Then
                        ListF = New List(Of clsBeCliente_tiempos)
                        ListF = pClienteTiemposList.FindAll(Function(x) x.IdFamilia = BeClienteTiempos.IdFamilia)
                    End If
                End If

                Dim a, b, c As Boolean

                If ListC.Count > 0 And ListF IsNot Nothing Then
                    a = pClienteTiemposList.Exists(Function(cf) cf.IdClasificacion = BeClienteTiempos.IdClasificacion And cf.IdFamilia = BeClienteTiempos.IdFamilia)
                    If a Then
                        XtraMessageBox.Show(String.Format("La clasificación {0} y la familia {1} ya están ingresadas.", txtNombreClasificacion.Text.Trim(), txtNombreFamilia.Text.Trim()),
                                            Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Return
                    End If
                ElseIf String.IsNullOrEmpty(txtIdFamilia.Text) AndAlso ListC.Count > 1 Then
                    b = pClienteTiemposList.Exists(Function(cl) cl.IdClasificacion = BeClienteTiempos.IdClasificacion)
                    If b Then
                        XtraMessageBox.Show(String.Format("La clasificación {0} ya está ingresada.", txtNombreClasificacion.Text.Trim()),
                                            Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Return
                    End If
                ElseIf String.IsNullOrEmpty(txtIdClasificacion.Text) AndAlso ListF IsNot Nothing AndAlso ListF.Count > 0 Then
                    c = pClienteTiemposList.Exists(Function(cl) cl.IdFamilia = BeClienteTiempos.IdFamilia)
                    If c Then
                        XtraMessageBox.Show(String.Format("La familia {0} ya está ingresada.", txtNombreFamilia.Text.Trim()),
                                            Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Return
                    End If
                End If

                BeClienteTiempos.Dias_Local = CInt(txtDiaLocal.Value)
                BeClienteTiempos.Dias_Exterior = CInt(txtDiaExterior.Value)

                BeClienteTiempos.User_agr = AP.UsuarioAp.IdUsuario
                BeClienteTiempos.Fec_agr = Now
                BeClienteTiempos.User_mod = AP.UsuarioAp.IdUsuario
                BeClienteTiempos.Fec_mod = Now
                BeClienteTiempos.Activo = True
                BeClienteTiempos.Es_Manufactura = chkManufactura.Checked

                pClienteTiemposList.Add(BeClienteTiempos)

            End If

            Cargar_Tiempos_Aceptacion(True)
            LimpiarCamposTiemposCliente()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub picMostrarUbicacionesCliente_Click(sender As Object, e As EventArgs)

        Dim Mapa As New frmMapa() With {.lDirCliente = pDirEntList}
        Mapa.ShowDialog()

    End Sub

    Private Sub dgridDirecciones_DoubleClick(sender As Object, e As EventArgs) Handles dgridDirecciones.DoubleClick

        LimpiarCamposDireccionesEntrega()

        Try

            Dim Dr As DataRowView = gvDireccionesCli.GetFocusedRow

            pIdDireccionEntregaCliente = CStr(Dr.Item("Codigo"))

            Dim lIndex As Integer = -1

            lIndex = pDirEntList.FindIndex(Function(c) c.IdDireccion = pIdDireccionEntregaCliente)

            If lIndex > -1 Then

                txtIdDireccion.Text = pIdDireccionEntregaCliente

                cmbMunicipio.EditValue = pDirEntList(lIndex).IdMunicipio
                cmbPais.EditValue = pDirEntList(lIndex).IdPais
                cmbRegion.EditValue = pDirEntList(lIndex).IdRegion
                cmbDepartamento.EditValue = pDirEntList(lIndex).IdDepartamento
                chkLocal.Checked = pDirEntList(lIndex).Local
                txtNoCasa.Text = pDirEntList(lIndex).Local
                txtReferenciaDireccion.Text = pDirEntList(lIndex).Referencia
                txtZona.Text = pDirEntList(lIndex).Zona
                txtAvenida.Text = pDirEntList(lIndex).Avenida
                txtCalle.Text = pDirEntList(lIndex).Calle
                txtCordX.Text = pDirEntList(lIndex).Coordenada_x
                txtCordY.Text = pDirEntList(lIndex).Coordenada_y
                txtDireccionEntrega.Text = pDirEntList(lIndex).Direccion

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            pIdDireccionEntregaCliente = String.Empty
        End Try

    End Sub

    Private Sub GridViewTiempo_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles GridViewTiempo.RowStyle

        Try

            GridViewTiempo.OptionsBehavior.Editable = False
            GridViewTiempo.OptionsSelection.EnableAppearanceFocusedCell = False

            GridViewTiempo.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus

            GridViewTiempo.OptionsSelection.EnableAppearanceFocusedRow = True
            GridViewTiempo.OptionsSelection.EnableAppearanceHideSelection = True
            GridViewTiempo.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            GridViewTiempo.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")

            GridViewTiempo.Appearance.FocusedRow.ForeColor = Color.White
            GridViewTiempo.Appearance.SelectedRow.ForeColor = Color.White

            GridViewTiempo.Appearance.SelectedRow.Options.UseBackColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

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

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub gvDireccionesCli_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gvDireccionesCli.RowStyle

        Try

            gvDireccionesCli.OptionsBehavior.Editable = False
            gvDireccionesCli.OptionsSelection.EnableAppearanceFocusedCell = False
            gvDireccionesCli.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
            gvDireccionesCli.OptionsSelection.EnableAppearanceFocusedRow = True
            gvDireccionesCli.OptionsSelection.EnableAppearanceHideSelection = True
            gvDireccionesCli.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            gvDireccionesCli.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            gvDireccionesCli.Appearance.FocusedRow.ForeColor = Color.White
            gvDireccionesCli.Appearance.SelectedRow.ForeColor = Color.White
            gvDireccionesCli.Appearance.SelectedRow.Options.UseBackColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub cmbPais_EditValueChanged(sender As Object, e As EventArgs) Handles cmbPais.EditValueChanged
        If cmbPais.Text <> "" AndAlso cmbPais.EditValue <> 0 Then
            IMS.Listar_Departamentos(cmbDepartamento, cmbPais.EditValue)
            IMS.Listar_Region(cmbRegion, cmbPais.EditValue)
        End If
    End Sub

    Private Sub cmbDepartamento_EditValueChanged(sender As Object, e As EventArgs) Handles cmbDepartamento.EditValueChanged
        If cmbDepartamento.Text > "" AndAlso cmbDepartamento.EditValue <> 0 Then
            IMS.Listar_Municipios(cmbMunicipio, cmbDepartamento.EditValue)
        End If
    End Sub


    Private Sub frmCliente_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub cmbBodega_EditValueChanged(sender As Object, e As EventArgs)

        Try

            Cliente_Bodega = New clsBeCliente_bodega()

            If lblCodigo.Text.Trim <> "" Then

                If Not clsLnCliente.Tiene_Cliente_Bodega(lblCodigo.Text.Trim,
                                                         cmbBodegaWMS.EditValue) Then

                    Cliente_Bodega.IdBodega = cmbBodegaWMS.EditValue
                    Cliente_Bodega.IdCliente = lblCodigo.Text
                    Cliente_Bodega.User_agr = AP.UsuarioAp.IdUsuario
                    Cliente_Bodega.Fec_agr = Date.Now
                    Cliente_Bodega.User_mod = AP.UsuarioAp.IdUsuario
                    Cliente_Bodega.Fec_mod = Date.Now
                    Cliente_Bodega.Activo = True

                    pClienteBodegaList.Add(Cliente_Bodega)

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

    Private Sub cmbBodega_KeyDown(sender As Object, e As KeyEventArgs)

        Try

            If e.KeyCode = Keys.Delete Then

                If clsLnCliente.Tiene_Cliente_Bodega(lblCodigo.Text.Trim, cmbBodegaWMS.EditValue) Then

                    If MessageBox.Show(String.Format("¿Desea eliminar la asignación de la bodega: {0}?", cmbBodegaWMS.EditValue), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                        gBeCliente.IdUbicacionVirtual = 0

                        If clsLnCliente_bodega.Eliminar_ClienteBodega(cmbBodegaWMS.EditValue, lblCodigo.Text) > 0 Then

                            If clsLnCliente.Actualizar_BodegaVirtual(gBeCliente) > 0 Then

                                MessageBox.Show("Eliminación completa", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Close()

                            End If

                        End If

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

    Private Sub cmdDesactivarPresentacion_Click(sender As Object, e As EventArgs) Handles cmdDesactivarPresentacion.Click
        Try

            If XtraMessageBox.Show("¿Está seguro de eliminar la dirección?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

            End If

            Dim lIndex As Integer = -1

            If pIdDireccionEntregaCliente = String.Empty Then pIdDireccionEntregaCliente = "0"

            lIndex = pDirEntList.FindIndex(Function(b) b.IdDireccion = CInt(pIdDireccionEntregaCliente))

            If lIndex = -1 Then 'No Existe

            Else 'Si Existe

                Dim BeDirec As New clsBeCliente_direccion

                BeDirec.IdCliente = pDirEntList(lIndex).IdCliente
                BeDirec.IdDireccion = pDirEntList(lIndex).IdDireccion

                If clsLnCliente_direccion.Eliminar(BeDirec) > 0 Then
                    MsgBox("Dirección eliminada correctamente")
                End If

                pDirEntList.RemoveAt(lIndex)

            End If

            Cargar_Direcciones_Entrega(True)


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
        LimpiarCamposDireccionesEntrega()
    End Sub

    Private Sub mnuMapa_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuMapa.ItemClick

        Try

            Dim Mapa As New frmMapa() With {.lDirCliente = pDirEntList}
            Mapa.ShowDialog()

        Catch ex As Exception

        End Try

    End Sub

    Private Sub chkEsBodRecep_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkEsBodRecep.CheckedChanged

        If chkEsBodRecep.Checked Then
            If chkSistema.Checked Then
                cmbBodegaWMS.ReadOnly = False
            End If
        Else
            If chkSistema.Checked Then
                If chkEsBodegaTraslado.Checked Then
                    cmbBodegaWMS.ReadOnly = False
                End If
            Else
                cmbBodegaWMS.ReadOnly = True
            End If
        End If

    End Sub

    Private Sub chkEsBodegaTraslado_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkEsBodegaTraslado.CheckedChanged

        If chkEsBodegaTraslado.Checked Then
            If chkSistema.Checked Then
                cmbBodegaWMS.ReadOnly = False
            End If
        Else
            If chkSistema.Checked Then
                If chkEsBodRecep.Checked Then
                    cmbBodegaWMS.ReadOnly = False
                End If
            Else
                cmbBodegaWMS.ReadOnly = True
            End If

        End If

    End Sub

    Private Sub chkSistema_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkSistema.CheckedChanged
        If chkSistema.Checked Then
            If chkEsBodRecep.Checked OrElse chkEsBodegaTraslado.Checked Then
                cmbBodegaWMS.ReadOnly = False
            End If
        Else
            XtraMessageBox.Show("No es posible desmarcar", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            chkSistema.Checked = True
        End If
    End Sub

    Private Sub txtIdUbicacionAbastecerCon_KeyDown(sender As Object, e As KeyEventArgs) Handles txtIdUbicacionAbastecerCon.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                txtIdUbicacionAbastecerCon.EditValue = Nothing
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Llena_AreaLookUp_Grid(ByVal pIdBodega As Integer)

        Try

            AreaBodegaGridLookUpEdit.ValueMember = "IdArea"
            AreaBodegaGridLookUpEdit.DisplayMember = "Nombre"
            AreaBodegaGridLookUpEdit.NullText = String.Empty
            Dim DT As New DataTable
            DT = clsLnBodega_area.Get_All_Areas_By_IdBodega_For_Combo(True, pIdBodega)
            If Not DT Is Nothing Then
                AreaBodegaGridLookUpEdit.DataSource = DT
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub cmdGuardarTodos_Click(sender As Object, e As EventArgs) Handles cmdGuardarTodos.Click


        Try

            Dim ListC As New List(Of clsBeCliente_tiempos)
            Dim ListF As New List(Of clsBeCliente_tiempos)

            Dim lIndex As Integer = -1

            If pIdTiempoCliente = String.Empty Then pIdTiempoCliente = "0"

            For Each TiempoCliente In pClienteTiemposList

                TiempoCliente.Dias_Exterior = txtDiaExterior.Value
                TiempoCliente.Dias_Local = txtDiaLocal.Value

            Next

            Cargar_Tiempos_Aceptacion(True)
            LimpiarCamposTiemposCliente()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try


    End Sub

    Private Sub cmbBodegaWMS_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbBodegaWMS.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                cmbBodegaWMS.EditValue = Nothing
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub tsSeleccionarClientes_Toggled(sender As Object, e As EventArgs) Handles tsSeleccionarClientes.Toggled

        If tsSeleccionarClientes.IsOn Then
            Seleccionar_Clientes(True)
        Else
            Seleccionar_Clientes(False)
        End If

    End Sub

    Private Sub Seleccionar_Clientes(valor As Boolean)

        For i As Integer = 0 To gridView1.RowCount - 1
            gridView1.SetRowCellValue(i, "Selección", valor)
        Next

    End Sub

    Private Sub LlenarGridLookups(ByVal Edicion As Boolean)
        CargarEstadosProducto()
        CargarGridLookUpLotes(Edicion)
    End Sub

    Private Sub CargarGridLookUpLotes(ByVal Edicion As Boolean)

        Try

            If gBeCliente Is Nothing Then Exit Sub

            Dim dtLotes As DataTable = clsLnStock.Get_Lotes_Disponibles_DT_By_IdCliente(AP.IdBodega, gBeCliente.IdCliente, Edicion)

            With cmbLote

                .Properties.DataSource = dtLotes
                .Properties.DisplayMember = "Lote"
                .Properties.ValueMember = "Lote"
                .Properties.PopulateViewColumns()

                Dim view As DevExpress.XtraGrid.Views.Grid.GridView = CType(.Properties.View, DevExpress.XtraGrid.Views.Grid.GridView)
                view.Columns("IdProductoEstado").Visible = False
                view.Columns("IdProducto").Visible = False

                cmbLote.Properties.PopupFormWidth = 800

            End With

        Catch ex As Exception
            MessageBox.Show("Error al cargar los lotes disponibles: " & ex.Message)
        End Try

    End Sub

    Private Sub CargarEstadosProducto()

        Try

            Dim dtEstadosProducto As DataTable = clsLnStock.Get_Estados_Producto_En_Stock()

            With cmbEstadoProducto

                .Properties.DataSource = dtEstadosProducto
                .Properties.DisplayMember = "nombre"
                .Properties.ValueMember = "IdEstado"
                .Properties.PopulateViewColumns()

                Dim view As DevExpress.XtraGrid.Views.Grid.GridView = CType(.Properties.View, DevExpress.XtraGrid.Views.Grid.GridView)
                view.BestFitColumns()

            End With

        Catch ex As Exception
            MessageBox.Show("Error al cargar los estados de producto: " & ex.Message)
        End Try
    End Sub

    Private Sub cmbLote_EditValueChanged(sender As Object, e As EventArgs) Handles cmbLote.EditValueChanged

        Try

            Dim view As DevExpress.XtraGrid.Views.Grid.GridView = CType(cmbLote.Properties.View, DevExpress.XtraGrid.Views.Grid.GridView)
            Dim selectedRowHandle As Integer = view.FocusedRowHandle

            If selectedRowHandle >= 0 Then

                Dim estado As Integer = view.GetRowCellValue(selectedRowHandle, "IdProductoEstado")
                Dim idproducto As Integer = view.GetRowCellValue(selectedRowHandle, "IdProducto")

                cmbEstadoProducto.EditValue = estado
                cmbEstadoProducto.Tag = idproducto

            End If

        Catch ex As Exception
            MessageBox.Show("Error al asignar estado del producto: " & ex.Message)
        End Try

    End Sub

    Private Sub mnuNuevoLote_Click(sender As Object, e As EventArgs) Handles mnuNuevoLote.Click
        Nuevo_Cliente_Lote()
    End Sub

    Private Sub Nuevo_Cliente_Lote()
        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Cargando lotes..")

            txtIdClienteLote.Text = clsLnCliente_lotes.MaxID() + 1
            txtIdClienteLote.Tag = 0
            LlenarGridLookups(False)

            cmbLote.EditValue = Nothing
            txtUsuarioAgregoLote.Text = AP.UsuarioAp.Nombres
            txtUsuarioModificoLote.Text = AP.UsuarioAp.Nombres
            dtpFechaAgregoLote.EditValue = Date.Now
            dtpFechaModificoLote.EditValue = Date.Now

        Catch ex As Exception
            MessageBox.Show("Error al cargar configuración: " & ex.Message)
        Finally
            SplashScreenManager.CloseForm()
        End Try
    End Sub

    Private Sub xtrtabClientes_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles xtrtabClientes.SelectedPageChanged
        If e.Page.Name = "tabLotes" Then
            Listar_Lotes()
        End If
    End Sub
    Private Sub Listar_Lotes()
        Listar_Lotes_Permitidos() : Listar_Lotes_Bloqueados()
    End Sub
    Private Sub Listar_Lotes_Permitidos()
        Try

            Dim dtLotesPermitidos As DataTable = clsLnCliente_lotes.Get_Lotes_By_IdCliente(gBeCliente.IdCliente, False)
            dgridLotesPermitidos.DataSource = dtLotesPermitidos

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Listar_Lotes_Bloqueados()
        Try
            Dim dtlotesBloqueados As DataTable = clsLnCliente_lotes.Get_Lotes_By_IdCliente(gBeCliente.IdCliente, True)
            DgridLotesBloqueados.DataSource = dtlotesBloqueados
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub dgridLotesPermitidos_DoubleClick(sender As Object, e As EventArgs) Handles dgridLotesPermitidos.DoubleClick, DgridLotesBloqueados.DoubleClick
        Try
            Dim view As DevExpress.XtraGrid.Views.Grid.GridView = CType(sender.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
            Dim rowHandle As Integer = view.FocusedRowHandle

            If rowHandle < 0 Then Exit Sub

            LlenarGridLookups(True)

            Dim IdClienteLote As Integer = Convert.ToInt32(view.GetRowCellValue(rowHandle, "IdClienteLote"))
            Dim BeClienteLote As clsBeCliente_lotes = clsLnCliente_lotes.GetSingle_By_IdClienteLote(IdClienteLote)

            txtIdClienteLote.Text = BeClienteLote.IdClienteLote
            txtIdClienteLote.Tag = BeClienteLote.IdClienteLote
            cmbLote.EditValue = BeClienteLote.Lote
            cmbEstadoProducto.EditValue = BeClienteLote.IdProductoEstado
            cmbEstadoProducto.Tag = BeClienteLote.IdProducto

            Dim BeUsuarioAgrego As clsBeUsuario = clsLnUsuario.GetSingle(BeClienteLote.User_agr)
            If Not BeUsuarioAgrego Is Nothing Then
                txtUsuarioAgregoLote.Text = BeUsuarioAgrego.Nombres
            Else
                txtUsuarioAgregoLote.Text = "Desconocido"
            End If

            Dim BeUsuarioModifico As clsBeUsuario = clsLnUsuario.GetSingle(BeClienteLote.User_mod)
            If Not BeUsuarioModifico Is Nothing Then
                txtUsuarioModificoLote.Text = BeUsuarioModifico.Nombres
            Else
                txtUsuarioModificoLote.Text = "Desconocido"
            End If

            dtpFechaAgregoLote.EditValue = BeClienteLote.Fec_agr
            dtpFechaModificoLote.EditValue = BeClienteLote.Fec_mod
            chkBloquear.EditValue = BeClienteLote.Bloquear
            chkActivo.Checked = BeClienteLote.Activo

        Catch ex As Exception
            MessageBox.Show("Error al seleccionar lote: " & ex.Message)
        End Try

    End Sub

    Private Sub mnuGuardarLote_Click(sender As Object, e As EventArgs) Handles mnuGuardarLote.Click

        Try

            Dim beClienteLote As New clsBeCliente_lotes

            If Val(txtIdClienteLote.Tag) = 0 Then

                beClienteLote.IdClienteLote = clsLnCliente_lotes.MaxID() + 1
                beClienteLote.IdCliente = gBeCliente.IdCliente
                beClienteLote.Lote = cmbLote.EditValue
                beClienteLote.IdProductoEstado = cmbEstadoProducto.EditValue
                beClienteLote.IdProducto = cmbEstadoProducto.Tag
                beClienteLote.User_agr = AP.UsuarioAp.IdUsuario
                beClienteLote.Fec_agr = Date.Now
                beClienteLote.User_mod = AP.UsuarioAp.IdUsuario
                beClienteLote.Fec_mod = Date.Now
                beClienteLote.Bloquear = chkBloquear.IsOn
                beClienteLote.Activo = True
                clsLnCliente_lotes.Insertar(beClienteLote)

            Else

                beClienteLote = clsLnCliente_lotes.GetSingle_By_IdClienteLote(CInt(txtIdClienteLote.Tag))

                If Not beClienteLote Is Nothing Then

                    beClienteLote.Lote = cmbLote.EditValue
                    beClienteLote.IdProductoEstado = cmbEstadoProducto.EditValue
                    beClienteLote.IdProducto = cmbEstadoProducto.Tag
                    beClienteLote.User_mod = AP.UsuarioAp.IdUsuario
                    beClienteLote.Fec_mod = Date.Now
                    beClienteLote.Bloquear = chkBloquear.IsOn
                    beClienteLote.Activo = chkLoteActivo.IsOn
                    clsLnCliente_lotes.Actualizar(beClienteLote)

                End If

            End If

            Nuevo_Cliente_Lote()

            Listar_Lotes()

        Catch ex As Exception
            MessageBox.Show("Error al guardar lote: " & ex.Message)
        End Try

    End Sub

    Private Sub mnuEliminarLote_Click(sender As Object, e As EventArgs) Handles mnuEliminarLote.Click

        If Not Val(txtIdClienteLote.Tag) = 0 Then

            Dim BeClienteLote As clsBeCliente_lotes = clsLnCliente_lotes.GetSingle_By_IdClienteLote(txtIdClienteLote.Tag)

            If Not BeClienteLote Is Nothing Then

                If XtraMessageBox.Show("¿Eliminar configuración de lote: " & BeClienteLote.IdClienteLote & " ?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    If clsLnCliente_lotes.Eliminar(BeClienteLote.IdClienteLote) > 0 Then
                        Listar_Lotes() : Nuevo_Cliente_Lote()
                    End If

                End If

            End If

        End If

    End Sub

    Private Sub txtIdProductoEstadoDefecto_KeyDown(sender As Object, e As KeyEventArgs) Handles txtIdProductoEstadoDefecto.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                txtIdProductoEstadoDefecto.EditValue = Nothing
            End If
        Catch ex As Exception

        End Try
    End Sub

End Class