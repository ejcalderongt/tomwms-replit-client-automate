Imports System.Threading.Tasks
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository

Public Class frmProveedor

    Private DT As DataTable
    Public pBeProveedor As New clsBeProveedor
    Public pBeProveedorBodegaList As List(Of clsBeProveedor_bodega)

    Public pIdProveedor As Integer
    Public Delegate Sub listar_Proveedor()
    Public Property InvokeListarProveedor As listar_Proveedor


    '#GT23102023: Gestion de tiempos
    Private pProveedorTiemposList As New List(Of clsBeProveedor_tiempos)
    Private pIdTiempoProveedor As String = String.Empty

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

                For i As Integer = 0 To DT.Rows.Count - 1

                    vIdBodega = DT(i)(0)

                    Llena_AreaLookUp_Grid(vIdBodega)

                    Dim lRow As DataRow = Nothing
                    lRow = DsProveedor.Data.NewRow
                    lRow.Item("IdBodega") = vIdBodega
                    lRow.Item("Bodega") = clsLnBodega.Get_Nombre_Bodega_By_IdBodega(vIdBodega)
                    lRow.Item("Asignar") = False
                    lRow.Item("IdAreaOrigen") = 0

                    If TipoTrans.Editar Then

                        If pBeProveedorBodegaList IsNot Nothing AndAlso pBeProveedorBodegaList.Count > 0 Then

                            For Each BeProveedorBodega As clsBeProveedor_bodega In pBeProveedorBodegaList

                                If BeProveedorBodega.IdBodega = CInt(DT(i)(0)) AndAlso BeProveedorBodega.Activo Then
                                    lRow.Item("Asignar") = True
                                    lRow.Item("IdAsignacion") = BeProveedorBodega.IdAsignacion
                                End If

                                lRow.Item("IdInterno") = BeProveedorBodega.IdAsignacion
                                lRow.Item("IdAreaOrigen") = BeProveedorBodega.IdAreaOrigen
                            Next

                        End If

                    End If

                    DsProveedor.Data.AddDataRow(lRow)

                Next

            End If

            Grid.EndUpdate()
            Grid.ForceInitialize()

            Dim ritem As RepositoryItemCheckEdit = TryCast(GridView1.Columns("Asignar").RealColumnEdit, RepositoryItemCheckEdit)
            AddHandler ritem.CheckedChanged, AddressOf ritem_CheckedChanged

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub ritem_CheckedChanged(sender As Object, e As EventArgs)

        Try

            Dim ritem As CheckEdit = TryCast(sender, CheckEdit)

            If Not ritem Is Nothing Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow
                Dim lIndex As Integer = -1

                lIndex = pBeProveedorBodegaList.FindIndex(Function(b) b.IdBodega = CInt(Dr.Item("IdBodega")) _
                                                  And b.IdProveedor = pBeProveedor.IdProveedor)
                If lIndex > -1 Then
                    If ritem.Checked Then
                        pBeProveedorBodegaList(lIndex).Activo = True
                    Else
                        pBeProveedorBodegaList(lIndex).Activo = False
                    End If
                    pBeProveedorBodegaList(lIndex).User_mod = AP.UsuarioAp.IdUsuario
                    pBeProveedorBodegaList(lIndex).Fec_mod = Now
                Else
                    Dim BeProveedorBodega As New clsBeProveedor_bodega()
                    BeProveedorBodega.IdBodega = CInt(Dr.Item("IdBodega"))
                    BeProveedorBodega.IdProveedor = pBeProveedor.IdProveedor
                    BeProveedorBodega.User_agr = AP.UsuarioAp.IdUsuario
                    BeProveedorBodega.Fec_agr = Now
                    BeProveedorBodega.User_mod = AP.UsuarioAp.IdUsuario
                    BeProveedorBodega.Fec_mod = Now
                    BeProveedorBodega.Activo = True
                    pBeProveedorBodegaList.Add(BeProveedorBodega)
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub ValidaBodegas()

        Try

            DsProveedor.Clear()
            DT = IMS.Listar_Bodegas()
            pBeProveedorBodegaList = New List(Of clsBeProveedor_bodega)
            pBeProveedorBodegaList = clsLnProveedor_bodega.Get_All_By_IdProveedor(pBeProveedor.IdProveedor).ToList()
            ListaBodegas()

            If chkSistema.Checked OrElse chkBodegaRec.Checked OrElse chkBodegaTras.Checked Then
                cmbBodegaWMS.ReadOnly = False
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub frmProveedor_Load(sender As Object, e As EventArgs) Handles MyBase.Load



        Try

            IMS.Listar_Empresas(cmbEmpresa)
            IMS.Listar_Propietarios_By_IdEmpresa(cmbPropietario, cmbEmpresa.EditValue)
            IMS.Listar_Bodegas_Por_Empresa(cmbBodegaWMS, AP.IdEmpresa)
            IMS.Listar_Areas_By_IdBodega_For_Combo(cmbBodegaAreaSAP, AP.IdBodega)
            IMS.Listar_Paises(cmbPais)
            txtTelefono.Properties.Mask.EditMask = "\((\d{3})\) (\d{4})-(\d{4})"
            txtTelefono.Properties.Mask.MaskType = Mask.MaskType.RegEx

            Select Case Modo

                Case TipoTrans.Nuevo

                    lblCodigo.Text = clsLnProveedor.MaxID()
                    cmbEmpresa.Enabled = True

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

                    cmbEmpresa.Enabled = True
                    cmbPropietario.Enabled = True

                    xtrTabProveedor.TabPages.Remove(ProveedorBodega)
                    xtrTabProveedor.TabPages.Remove(tiempoAceptacion)

                Case TipoTrans.Editar

                    Dim BeProveedor As New clsBeProveedor
                    BeProveedor = clsLnProveedor.GetSingle(pBeProveedor.IdProveedor)

                    CopyObject(BeProveedor, pBeProveedor)

                    lblCodigo.Text = pBeProveedor.IdProveedor
                    cmbEmpresa.EditValue = pBeProveedor.IdEmpresa
                    cmbPropietario.EditValue = pBeProveedor.IdPropietario
                    txtCodProveedor.Text = pBeProveedor.Codigo
                    NombreTextEdit.Text = pBeProveedor.Nombre
                    txtTelefono.Text = pBeProveedor.Telefono
                    txtNit.Text = pBeProveedor.Nit
                    txtDireccion.Text = pBeProveedor.Direccion
                    txtCorreo.Text = pBeProveedor.Email
                    txtContacto.Text = pBeProveedor.Contacto
                    chkMuestraPrecio.Checked = pBeProveedor.Muestra_precio
                    chkActualizaCostoOC.Checked = pBeProveedor.Actualiza_costo_oc
                    chkActivo.Checked = pBeProveedor.Activo

                    User_agrTextEdit.Text = pBeProveedor.User_agr
                    Fec_agrDateEdit.Text = pBeProveedor.Fec_agr
                    User_modTextEdit.Text = pBeProveedor.User_mod
                    Fec_modDateEdit.Text = pBeProveedor.Fec_mod

                    txtReferenciaCliente.Text = pBeProveedor.Referencia
                    chkSistema.Checked = pBeProveedor.Sistema
                    chkBodegaRec.Checked = pBeProveedor.Es_Bodega_Recepcion
                    chkBodegaTras.Checked = pBeProveedor.Es_Bodega_Traslado
                    cmbBodegaAreaSAP.EditValue = pBeProveedor.IdBodegaAreaSAP

                    cmbEmpresa.Enabled = False
                    mnuGuardar.Enabled = False

                    If OpcionesMenu IsNot Nothing Then
                        mnuActualizar.Enabled = OpcionesMenu.Modificar
                        mnuEliminar.Enabled = OpcionesMenu.Eliminar
                    End If

                    cmbEmpresa.Enabled = False
                    cmbPropietario.Enabled = False

                    ValidaBodegas()
                    '#GT24102023: tiempos de aceptación
                    Cargar_Tiempos_Aceptacion(False)
                    '#GT30102023: tab para mostrar los tiempos de aceptación
                    xtrTabProveedor.TabPages.Add(tiempoAceptacion)

                    If pBeProveedor.IdUbicacionVirtual <> 0 Then
                        cmbBodegaWMS.EditValue = pBeProveedor.IdUbicacionVirtual
                    End If

                    If pBeProveedor.idPais <> 0 Then
                        cmbPais.EditValue = pBeProveedor.idPais
                    Else
                        cmbPais.EditValue = Nothing
                    End If

            End Select

            NombreTextEdit.Focus()
            Application.DoEvents()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick

        mnuGuardar.Enabled = False

        If Datos_Correctos() Then

            If MessageBox.Show("¿Guardar Proveedor?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                If Guardar() Then

                    XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    If MessageBox.Show("¿Desea asignar Bodegas?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        xtrTabProveedor.TabPages.Add(ProveedorBodega)
                        xtrTabProveedor.TabPages.Add(tiempoAceptacion)
                        ValidaBodegas()
                        'mnuGuardar.Enabled = False
                        mnuActualizar.Enabled = True
                        mnuEliminar.Enabled = True
                    Else
                        InvokeListarProveedor.Invoke
                        Close()
                    End If

                End If

            End If

        End If

        'mnuGuardar.Enabled = True

    End Sub

    Private Function Guardar() As Boolean

        Guardar = False

        Try

            Dim BeProveedor As New clsBeProveedor() With {.IdProveedor = clsLnProveedor.MaxID(),
                .IdEmpresa = cmbEmpresa.EditValue,
                .IdPropietario = cmbPropietario.EditValue,
                .Codigo = txtCodProveedor.Text.Trim(),
                .Nombre = NombreTextEdit.Text.Trim(),
                .Telefono = txtTelefono.Text.Trim(),
                .Nit = txtNit.Text.Trim(),
                .Direccion = txtDireccion.Text.Trim(),
                .Email = txtCorreo.Text.Trim(),
                .Contacto = txtContacto.Text.Trim(),
                .Muestra_precio = chkMuestraPrecio.Checked,
                .Activo = True,
                .User_agr = AP.UsuarioAp.IdUsuario,
                .Fec_agr = Now,
                .User_mod = AP.UsuarioAp.IdUsuario,
                .Fec_mod = Now,
                .Actualiza_costo_oc = chkActualizaCostoOC.Checked,
                .IdUbicacionVirtual = IIf(cmbBodegaWMS.EditValue IsNot Nothing, cmbBodegaWMS.EditValue, 0),
                .Es_Bodega_Recepcion = chkBodegaRec.Checked,
                .Es_Bodega_Traslado = chkBodegaTras.Checked,
                .Referencia = txtReferenciaCliente.Text,
                .Sistema = chkSistema.Checked,
                .Es_Proveedor_Servicio = chkEsProveedorServicio.Checked,
                .IdPais = cmbPais.EditValue
            }

            BeProveedor.IdBodegaAreaSAP = cmbBodegaAreaSAP.EditValue

            CopyObject(BeProveedor, pBeProveedor)

            clsLnProveedor.Guardar_Transaccion(BeProveedor,
                                               pProveedorTiemposList,
                                               pBeProveedorBodegaList)

            Guardar = True

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Datos_Correctos() As Boolean

        Datos_Correctos = False

        Try

            If cmbEmpresa.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Empresa.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf cmbPropietario.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Propietario.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf String.IsNullOrEmpty(NombreTextEdit.Text.Trim) Then
                XtraMessageBox.Show("Ingrese Nombre.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                NombreTextEdit.Focus()
            Else
                Datos_Correctos = True
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            If Datos_Correctos() Then

                pBeProveedor.Codigo = txtCodProveedor.Text.Trim()
                pBeProveedor.Nombre = NombreTextEdit.Text.Trim()
                pBeProveedor.Telefono = txtTelefono.Text.Trim()
                pBeProveedor.Nit = txtNit.Text.Trim()
                pBeProveedor.Direccion = txtDireccion.Text.Trim()
                pBeProveedor.Email = txtCorreo.Text.Trim()
                pBeProveedor.Contacto = txtContacto.Text.Trim()
                pBeProveedor.Muestra_precio = chkMuestraPrecio.Checked

                pBeProveedor.User_mod = AP.UsuarioAp.IdUsuario
                pBeProveedor.Fec_mod = Now
                pBeProveedor.Actualiza_costo_oc = chkActualizaCostoOC.Checked
                '#GT05120022: se toma el estado del check en true durante el UPDATE.
                pBeProveedor.Activo = chkActivo.Checked

                pBeProveedor.IdUbicacionVirtual = IIf(cmbBodegaWMS.EditValue IsNot Nothing, cmbBodegaWMS.EditValue, 0)
                pBeProveedor.Es_Bodega_Recepcion = chkBodegaRec.Checked
                pBeProveedor.Es_Bodega_Traslado = chkBodegaTras.Checked
                pBeProveedor.Referencia = txtReferenciaCliente.Text
                pBeProveedor.Sistema = chkSistema.Checked
                pBeProveedor.IdBodegaAreaSAP = cmbBodegaAreaSAP.EditValue
                pBeProveedor.IsNew = False
                pBeProveedor.idPais = cmbPais.EditValue

                'Return clsLnProveedor_bodega.ActualizarDatos(pBeProveedor, pBeProveedorBodegaList)
                clsLnProveedor.Guardar_Transaccion(pBeProveedor,
                                                   pProveedorTiemposList,
                                                   pBeProveedorBodegaList)

                Actualizar = True

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        mnuActualizar.Enabled = False

        If Actualizar() Then

            XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            If Not InvokeListarProveedor Is Nothing Then InvokeListarProveedor.Invoke

            Close()

        End If

        mnuGuardar.Enabled = True
        mnuActualizar.Enabled = True

    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try

            mnuEliminar.Enabled = False

            If MessageBox.Show("¿Eliminar Proveedor?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                Dim ObjMD As New clsBeProveedor

                CopyObject(pBeProveedor, ObjMD)

                If clsLnProveedor.Eliminar(ObjMD) > 0 Then

                    XtraMessageBox.Show("Se ha eliminado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    If Not InvokeListarProveedor Is Nothing Then InvokeListarProveedor.Invoke()

                    Close()

                    frmProveedor_List.Dgrid.Refresh()

                End If

            End If

            mnuEliminar.Enabled = True

        Catch ex As Exception

            mnuEliminar.Enabled = True
            If ex.HResult = -2146233088 Then
                TablasRelacionadas("Proveedor", lblCodigo.Text)
            Else
                XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End Try

    End Sub

    Private Sub gridView1_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles GridView1.RowStyle

        Try

            GridView1.OptionsBehavior.Editable = True
            GridView1.OptionsSelection.EnableAppearanceFocusedCell = True

            GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus

            GridView1.OptionsSelection.EnableAppearanceFocusedRow = True
            GridView1.OptionsSelection.EnableAppearanceHideSelection = True
            GridView1.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            GridView1.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")

            GridView1.Appearance.FocusedRow.ForeColor = Color.White
            GridView1.Appearance.SelectedRow.ForeColor = Color.White

            GridView1.Appearance.SelectedRow.Options.UseBackColor = True
            GridView1.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub frmProveedor_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub chkSistema_CheckedChanged(sender As Object, e As EventArgs)
        If chkSistema.Checked Then
            If chkBodegaRec.Checked OrElse chkBodegaTras.Checked Then
                If Modo = TipoTrans.Editar OrElse Modo = TipoTrans.Nuevo Then
                    cmbBodegaWMS.ReadOnly = False
                End If
            End If
        Else
            If Modo = TipoTrans.Editar Then
                XtraMessageBox.Show("No es posible desmarcar", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                chkSistema.Checked = True
            End If
        End If
    End Sub

    Private Sub chkBodegaRec_CheckedChanged(sender As Object, e As EventArgs)

        If chkBodegaRec.Checked Then
            If chkSistema.Checked Then
                cmbBodegaWMS.ReadOnly = False
            End If
        Else
            If chkSistema.Checked Then
                If chkBodegaTras.Checked Then
                    cmbBodegaWMS.ReadOnly = False
                End If
            Else
                cmbBodegaWMS.ReadOnly = True
            End If
        End If

    End Sub

    Private Sub chkBodegaTras_CheckedChanged(sender As Object, e As EventArgs)

        If chkBodegaTras.Checked Then
            If chkSistema.Checked Then
                If Modo = TipoTrans.Editar Then
                    cmbBodegaWMS.ReadOnly = False
                End If
            End If
        Else
            If chkSistema.Checked Then
                If chkBodegaRec.Checked Then
                    If Modo = TipoTrans.Editar Then
                        cmbBodegaWMS.ReadOnly = False
                    End If
                End If
            Else
                cmbBodegaWMS.ReadOnly = True
            End If

        End If

    End Sub

    Private Sub cmbBodega_EditValueChanged(sender As Object, e As EventArgs) Handles cmbBodegaWMS.EditValueChanged

        Try

            If lblCodigo.Text.Trim <> "" Then

                If Not clsLnProveedor_bodega.Tiene_Proveedor_Bodega(lblCodigo.Text.Trim, cmbBodegaWMS.EditValue) Then

                    Dim Obj As New clsBeProveedor_bodega()

                    Obj.IdProveedor = lblCodigo.Text.Trim
                    Obj.IdBodega = cmbBodegaWMS.EditValue
                    Obj.Activo = True
                    Obj.User_agr = AP.UsuarioAp.IdUsuario
                    Obj.Fec_agr = Date.Now
                    Obj.User_mod = AP.UsuarioAp.IdUsuario
                    Obj.Fec_mod = Date.Now

                    If Modo = TipoTrans.Nuevo Then
                        pBeProveedorBodegaList = New List(Of clsBeProveedor_bodega)
                    End If

                    pBeProveedorBodegaList.Add(Obj)

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

    Private Sub cmbBodega_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbBodegaWMS.KeyDown

        Try

            If e.KeyCode = Keys.Delete Then

                If clsLnProveedor_bodega.Tiene_Proveedor_Bodega(lblCodigo.Text.Trim, cmbBodegaWMS.EditValue) Then

                    If MessageBox.Show(String.Format("¿Desea eliminar la asignación de la bodega: {0}?", cmbBodegaWMS.EditValue), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                        pBeProveedor.IdUbicacionVirtual = 0

                        If clsLnProveedor_bodega.Elimina_ProveedorBodega(cmbBodegaWMS.EditValue, lblCodigo.Text) > 0 Then

                            If clsLnProveedor.Actualizar_IdBodegaVirtual(pBeProveedor) > 0 Then

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

            Dim ListC As New List(Of clsBeProveedor_tiempos)
            Dim ListF As New List(Of clsBeProveedor_tiempos)

            Dim lIndex As Integer = -1

            If pIdTiempoProveedor = String.Empty Then pIdTiempoProveedor = "0"

            lIndex = pProveedorTiemposList.FindIndex(Function(b) b.IdTiempoProveedor = CInt(pIdTiempoProveedor))

            If lIndex > -1 Then

                If String.IsNullOrEmpty(txtIdClasificacion.Text.Trim()) = False Then

                    ListC = pProveedorTiemposList.FindAll(Function(x) x.IdClasificacion = CInt(txtIdClasificacion.Text.Trim()))

                    If String.IsNullOrEmpty(txtIdFamilia.Text) = False AndAlso ListC.Count > 0 Then

                        lIndex = pProveedorTiemposList.FindIndex(Function(cf) cf.IdClasificacion = CInt(txtIdClasificacion.Text.Trim()) _
                                                                     AndAlso cf.IdFamilia = CInt(txtIdFamilia.Text.Trim()) _
                                                                     AndAlso cf.IdTiempoProveedor = CInt(pIdTiempoProveedor))

                    ElseIf String.IsNullOrEmpty(txtIdFamilia.Text) AndAlso ListC.Count > 1 AndAlso Not pProveedorTiemposList(lIndex).IdFamilia <> 0 Then
                        Dim b As Boolean = pProveedorTiemposList.Exists(Function(cl) cl.IdClasificacion = CInt(txtIdClasificacion.Text.Trim()))
                        If b Then
                            XtraMessageBox.Show(String.Format("La clasificación {0} ya está ingresada.", txtNombreClasificacion.Text.Trim()),
                                                Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Return
                        End If
                    End If
                    pProveedorTiemposList(lIndex).IdClasificacion = CInt(txtIdClasificacion.Text.Trim())
                    pProveedorTiemposList(lIndex).Clasificacion.Nombre = txtNombreClasificacion.Text.Trim()
                Else
                    pProveedorTiemposList(lIndex).IdClasificacion = Nothing
                    pProveedorTiemposList(lIndex).Clasificacion.Nombre = String.Empty
                End If

                If String.IsNullOrEmpty(txtIdFamilia.Text.Trim()) = False AndAlso pProveedorTiemposList(lIndex).IdClasificacion <> 0 Then
                    ListF = New List(Of clsBeProveedor_tiempos)
                    ListF = pProveedorTiemposList.FindAll(Function(x) x.IdFamilia = CInt(txtIdFamilia.Text.Trim()))
                    If String.IsNullOrEmpty(txtIdClasificacion.Text) AndAlso ListF.Count > 0 Then
                        Dim c As Boolean = pProveedorTiemposList.Exists(Function(cl) cl.IdFamilia = CInt(txtIdFamilia.Text.Trim()))
                        If c Then
                            XtraMessageBox.Show(String.Format("La familia {0} ya está ingresada.", txtNombreFamilia.Text.Trim()),
                                                Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Return
                        End If
                    End If

                    pProveedorTiemposList(lIndex).IdFamilia = CInt(txtIdFamilia.Text.Trim())
                    pProveedorTiemposList(lIndex).Familia.Nombre = txtNombreFamilia.Text.Trim()
                Else
                    pProveedorTiemposList(lIndex).IdFamilia = Nothing
                    pProveedorTiemposList(lIndex).Familia.Nombre = String.Empty
                End If

                pProveedorTiemposList(lIndex).Dias_Local = CInt(txtDiaLocal.Text)
                pProveedorTiemposList(lIndex).Dias_Exterior = CInt(txtDiaExterior.Text)
                pProveedorTiemposList(lIndex).User_mod = AP.UsuarioAp.IdUsuario
                pProveedorTiemposList(lIndex).Fec_mod = Now

            Else
                Dim Obj As New clsBeProveedor_tiempos() With {.Clasificacion = New clsBeProducto_clasificacion(), .Familia = New clsBeProducto_familia()}

                If String.IsNullOrEmpty(txtIdClasificacion.Text.Trim()) = False Then
                    Obj.IdClasificacion = CInt(txtIdClasificacion.Text.Trim())
                    Obj.Clasificacion.Nombre = txtNombreClasificacion.Text.Trim()
                    If pProveedorTiemposList IsNot Nothing AndAlso pProveedorTiemposList.Count > 0 Then
                        ListC = pProveedorTiemposList.FindAll(Function(x) x.IdClasificacion = Obj.IdClasificacion)
                    End If
                End If
                If String.IsNullOrEmpty(txtIdFamilia.Text.Trim()) = False Then
                    Obj.IdFamilia = CInt(txtIdFamilia.Text.Trim())
                    Obj.Familia.Nombre = txtNombreFamilia.Text.Trim()
                    If pProveedorTiemposList IsNot Nothing AndAlso pProveedorTiemposList.Count > 0 Then
                        ListF = New List(Of clsBeProveedor_tiempos)
                        ListF = pProveedorTiemposList.FindAll(Function(x) x.IdFamilia = Obj.IdFamilia)
                    End If
                End If

                Dim a, b, c As Boolean

                If ListC.Count > 0 And ListF IsNot Nothing Then
                    a = pProveedorTiemposList.Exists(Function(cf) cf.IdClasificacion = Obj.IdClasificacion And cf.IdFamilia = Obj.IdFamilia)
                    If a Then
                        XtraMessageBox.Show(String.Format("La clasificación {0} y la familia {1} ya están ingresadas.", txtNombreClasificacion.Text.Trim(), txtNombreFamilia.Text.Trim()),
                                            Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Return
                    End If
                ElseIf String.IsNullOrEmpty(txtIdFamilia.Text) AndAlso ListC.Count > 1 Then
                    b = pProveedorTiemposList.Exists(Function(cl) cl.IdClasificacion = Obj.IdClasificacion)
                    If b Then
                        XtraMessageBox.Show(String.Format("La clasificación {0} ya está ingresada.", txtNombreClasificacion.Text.Trim()),
                                            Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Return
                    End If
                ElseIf String.IsNullOrEmpty(txtIdClasificacion.Text) AndAlso ListF IsNot Nothing AndAlso ListF.Count > 0 Then
                    c = pProveedorTiemposList.Exists(Function(cl) cl.IdFamilia = Obj.IdFamilia)
                    If c Then
                        XtraMessageBox.Show(String.Format("La familia {0} ya está ingresada.", txtNombreFamilia.Text.Trim()),
                                            Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Return
                    End If
                End If

                Obj.Dias_Local = CInt(txtDiaLocal.Value)
                Obj.Dias_Exterior = CInt(txtDiaExterior.Value)

                Obj.User_agr = AP.UsuarioAp.IdUsuario
                Obj.Fec_agr = Now
                Obj.User_mod = AP.UsuarioAp.IdUsuario
                Obj.Fec_mod = Now
                Obj.Activo = True

                pProveedorTiemposList.Add(Obj)

            End If

            Cargar_Tiempos_Aceptacion(True)
            LimpiarCamposTiemposCliente()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub GridTiempo_DoubleClick(sender As Object, e As EventArgs) Handles GridTiempo.DoubleClick

        LimpiarCamposTiemposCliente()

        Try

            Dim Dr As DataRowView = GridViewTiempo.GetFocusedRow

            pIdTiempoProveedor = CStr(Dr.Item("Código"))

            If Dr.Item("IdFamilia") IsNot DBNull.Value AndAlso Dr.Item("IdFamilia") IsNot Nothing Then

                txtIdFamilia.Text = CInt(Dr.Item("IdFamilia"))

                Dim lIndex As Integer = -1

                lIndex = pProveedorTiemposList.FindIndex(Function(f) f.IdFamilia = CInt(txtIdFamilia.Text.Trim()))

                If lIndex > -1 Then
                    txtNombreFamilia.Text = pProveedorTiemposList(lIndex).Familia.Nombre
                End If

            End If

            If Dr.Item("IdClasificacion") IsNot DBNull.Value AndAlso Dr.Item("IdClasificacion") IsNot Nothing Then

                txtIdClasificacion.Text = CInt(Dr.Item("IdClasificacion"))

                Dim lIndex As Integer = -1

                lIndex = pProveedorTiemposList.FindIndex(Function(c) c.IdClasificacion = CInt(txtIdClasificacion.Text.Trim()))

                If lIndex > -1 Then
                    txtNombreClasificacion.Text = pProveedorTiemposList(lIndex).Clasificacion.Nombre
                End If

            End If

            txtDiaLocal.Text = CInt(Dr.Item("Días Locales"))
            txtDiaExterior.Text = CInt(Dr.Item("Días Exteriores"))

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            pIdTiempoProveedor = String.Empty
        End Try

    End Sub


    Private Sub Cargar_Tiempos_Aceptacion(ByVal pGuardo As Boolean)

        Try

            If pGuardo = False Then
                pProveedorTiemposList = clsLnProveedor_tiempos.Get_All_Tiempos_By_IdCliente(pBeProveedor.IdProveedor).ToList()
            End If

            Dim DT As New DataTable("Tiempo")
            DT.Columns.Add("Código", GetType(Integer))
            DT.Columns.Add("IdFamilia", GetType(Integer))
            DT.Columns.Add("Familia", GetType(String))
            DT.Columns.Add("IdClasificacion", GetType(Integer))
            DT.Columns.Add("Clasificación", GetType(String))
            DT.Columns.Add("Días Locales", GetType(Integer))
            DT.Columns.Add("Días Exteriores", GetType(Integer))

            Dim Correlativo As Integer = 1

            If pProveedorTiemposList.Count > 0 Then
                Correlativo = pProveedorTiemposList.Max(Function(m) m.IdTiempoProveedor)
            End If

            Parallel.ForEach(pProveedorTiemposList.OrderBy(Function(o) o.IdTiempoProveedor), Sub(beProveedorTiempos As clsBeProveedor_tiempos)
                                                                                                 SyncLock DT

                                                                                                     If beProveedorTiempos.IdTiempoProveedor = 0 AndAlso beProveedorTiempos.IdProveedor = 0 Then
                                                                                                         Correlativo += 1
                                                                                                         beProveedorTiempos.IdTiempoProveedor = Correlativo
                                                                                                     End If

                                                                                                     Dim lRow As DataRow = DT.NewRow()

                                                                                                     lRow(0) = beProveedorTiempos.IdTiempoProveedor

                                                                                                     If beProveedorTiempos.IdFamilia <> Nothing AndAlso beProveedorTiempos.IdFamilia <> 0 Then
                                                                                                         lRow(1) = beProveedorTiempos.IdFamilia
                                                                                                         lRow(2) = beProveedorTiempos.Familia.Nombre
                                                                                                     End If

                                                                                                     If beProveedorTiempos.IdClasificacion <> Nothing AndAlso beProveedorTiempos.IdClasificacion <> 0 Then
                                                                                                         lRow(3) = beProveedorTiempos.IdClasificacion
                                                                                                         lRow(4) = beProveedorTiempos.Clasificacion.Nombre
                                                                                                     End If

                                                                                                     lRow(5) = beProveedorTiempos.Dias_Local
                                                                                                     lRow(6) = beProveedorTiempos.Dias_Exterior

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

    Private Sub Llena_AreaLookUp_Grid(ByVal pIdBodega As Integer)

        Try
            AreaBodegaGridLookUpEdit.DataSource = clsLnBodega_area.Get_All_Areas_By_IdBodega_For_Combo(True, pIdBodega)
            AreaBodegaGridLookUpEdit.ValueMember = "IdArea"
            AreaBodegaGridLookUpEdit.DisplayMember = "Nombre"
            AreaBodegaGridLookUpEdit.NullText = String.Empty

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub AreaGridLookUpEdit_Leave(ByVal sender As Object, ByVal e As EventArgs) Handles AreaBodegaGridLookUpEdit.Leave

        Try

            Dim lista As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
            If lista.EditValue Is Nothing Then Return
            Dim drLineaGrid As DataRow = GridView1.GetFocusedDataRow()
            If drLineaGrid Is Nothing Then Return

            Dim vBeAreaBodega As Object = lista.Properties.GetRowByKeyValue(lista.EditValue)

            If Not vBeAreaBodega Is Nothing Then

                Dim drArea As DataRow = (TryCast(lista.Properties.GetRowByKeyValue(lista.EditValue), DataRowView)).Row
                If drArea Is Nothing Then Return

                Dim Dr As DataRowView = GridView1.GetFocusedRow
                Dim lIndex As Integer = -1

                lIndex = pBeProveedorBodegaList.FindIndex(Function(b) b.IdBodega = CInt(Dr.Item("IdBodega")) _
                                                  And b.IdProveedor = pBeProveedor.IdProveedor)
                If lIndex > -1 Then

                    pBeProveedorBodegaList(lIndex).User_mod = AP.UsuarioAp.IdUsuario
                    pBeProveedorBodegaList(lIndex).Fec_mod = Now

                    If Dr.Item("IdAreaOrigen") > 0 Then
                        pBeProveedorBodegaList(lIndex).IdAreaOrigen = CInt(Dr.Item("IdAreaOrigen"))
                    Else
                        pBeProveedorBodegaList(lIndex).IdAreaOrigen = Nothing
                    End If

                Else

                    If pBeProveedorBodegaList Is Nothing Then
                        pBeProveedorBodegaList = New List(Of clsBeProveedor_bodega)
                    End If

                    Dim BeProveedorBodega As New clsBeProveedor_bodega()
                    BeProveedorBodega.IdBodega = CInt(Dr.Item("IdBodega"))
                    BeProveedorBodega.IdProveedor = pBeProveedor.IdProveedor
                    BeProveedorBodega.User_agr = AP.UsuarioAp.IdUsuario
                    BeProveedorBodega.Fec_agr = Now
                    BeProveedorBodega.User_mod = AP.UsuarioAp.IdUsuario
                    BeProveedorBodega.Fec_mod = Now
                    BeProveedorBodega.Activo = True

                    If Dr.Item("IdAreaOrigen") > 0 Then
                        BeProveedorBodega.IdAreaOrigen = drLineaGrid("IdAreaOrigen")
                    Else
                        BeProveedorBodega.IdAreaOrigen = Nothing
                    End If

                    pBeProveedorBodegaList.Add(BeProveedorBodega)
                End If

                drLineaGrid("IdAreaOrigen") = drArea("IdArea")

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

End Class