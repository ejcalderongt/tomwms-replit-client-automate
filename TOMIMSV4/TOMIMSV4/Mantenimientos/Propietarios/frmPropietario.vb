Imports System.Drawing.Imaging
Imports System.IO
Imports System.Reflection
Imports DevExpress.Data
Imports DevExpress.Xpf.Bars
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmPropietario

    Private DT As DataTable
    Public pBePropietario As New clsBePropietarios
    Public gBePropietarioBodegaList As List(Of clsBePropietario_bodega)
    Public pIdPropietario As Integer
    Private pListDestinatarios As New List(Of clsBePropietario_destinatario)
    Private pObjDestinatario As New clsBePropietario_destinatario
    Private pListObjRE As List(Of clsBePropietario_reglas_enc)
    Private pListObjRD As List(Of clsBePropietario_reglas_det)
    Private pListBeUM As List(Of clsBeUnidad_medida)
    Private pListBeProductoEstado As List(Of clsBeProducto_estado)
    Private pListBeProducto As List(Of clsBeProducto)
    Private pListBeMovimientos As List(Of clsBeVW_Movimientos)
    Private pListBeStock As List(Of clsBeVW_stock_res)

    Private pGridRegla As Boolean
    Private pListObjT As New List(Of clsTabla)
    Public Delegate Sub listar_Propietario()
    Public Property InvokeListarPropietarios As listar_Propietario
    Private DTproductoGrid As New DataTable("Producto")
    Private Sub Init_DataTable_Productos()

        DTproductoGrid = New DataTable("Producto")
        DTproductoGrid.Columns.Add("Correlativo", GetType(Integer))
        DTproductoGrid.Columns.Add("Clasificación", GetType(String))
        DTproductoGrid.Columns.Add("Familia", GetType(String))
        DTproductoGrid.Columns.Add("Marca", GetType(String))
        DTproductoGrid.Columns.Add("UMBas", GetType(String))
        DTproductoGrid.Columns.Add("Código", GetType(String))
        DTproductoGrid.Columns.Add("Codigo_Barra", GetType(String))
        DTproductoGrid.Columns.Add("Nombre", GetType(String))
        DTproductoGrid.Columns.Add("Costo", GetType(Double))
        DTproductoGrid.Columns.Add("Precio", GetType(Double))
        DTproductoGrid.Columns.Add("Kit", GetType(Boolean))
        DTproductoGrid.Columns.Add("Fecha_Agr", GetType(DateTime))

    End Sub

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
                    Dim lRow As DataRow = DsPropietario.Data.NewRow
                    lRow.Item("IdBodega") = vIdBodega
                    lRow.Item("Bodega") = clsLnBodega.Get_Nombre_Bodega_By_IdBodega(vIdBodega)
                    lRow.Item("Selección") = False

                    If TipoTrans.Editar Then

                        If gBePropietarioBodegaList IsNot Nothing AndAlso gBePropietarioBodegaList.Count > 0 Then

                            For Each BePropietarioBodega As clsBePropietario_bodega In gBePropietarioBodegaList

                                If BePropietarioBodega.IdBodega = CInt(DT(i)(0)) AndAlso BePropietarioBodega.Activo Then
                                    lRow.Item("Selección") = True
                                    lRow.Item("IdPropietarioBodega") = BePropietarioBodega.IdPropietarioBodega
                                End If

                                lRow.Item("IdInterno") = BePropietarioBodega.IdPropietarioBodega

                            Next

                        End If

                    End If

                    DsPropietario.Data.AddDataRow(lRow)

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

                lIndex = gBePropietarioBodegaList.FindIndex(Function(b) b.IdBodega = CInt(Dr.Item("IdBodega")) _
                                                  And b.IdPropietario = pBePropietario.IdPropietario)
                If lIndex > -1 Then
                    If ritem.Checked Then
                        gBePropietarioBodegaList(lIndex).Activo = True
                    Else
                        gBePropietarioBodegaList(lIndex).Activo = False
                    End If
                    gBePropietarioBodegaList(lIndex).User_mod = AP.UsuarioAp.IdUsuario
                    gBePropietarioBodegaList(lIndex).Fec_mod = Now
                Else
                    Dim BePropietarioBodega As New clsBePropietario_bodega()
                    BePropietarioBodega.IdBodega = CInt(Dr.Item("IdBodega"))
                    BePropietarioBodega.IdPropietario = pBePropietario.IdPropietario
                    BePropietarioBodega.User_agr = AP.UsuarioAp.IdUsuario
                    BePropietarioBodega.Fec_agr = Now
                    BePropietarioBodega.User_mod = AP.UsuarioAp.IdUsuario
                    BePropietarioBodega.Fec_mod = Now
                    BePropietarioBodega.Activo = True
                    gBePropietarioBodegaList.Add(BePropietarioBodega)
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub ValidaBodegas()

        Try

            DsPropietario.Clear()
            DT = IMS.Listar_Bodegas()
            gBePropietarioBodegaList = New List(Of clsBePropietario_bodega)
            gBePropietarioBodegaList = clsLnPropietario_bodega.Get_All_By_IdPropietario(pBePropietario.IdPropietario).ToList()
            ListaBodegas()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub LlenaTipoActualizacionCosto()

        Try

            cmbTipoActualizacion.Properties.DisplayMember = "NombreActualizacionCosto"
            cmbTipoActualizacion.Properties.ValueMember = "IdTipoActualizacionCosto"
            cmbTipoActualizacion.Properties.DataSource = clsLnTipo_actualizacion_costo.GetAll().ToList
            cmbTipoActualizacion.ItemIndex = 0


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub frmPropietario_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            pListObjT = clsBD.GetLongitudByTabla("propietarios")

            If Not IMS.Listar_Empresas(cmbEmpresa) Then
                XtraMessageBox.Show("No hay empresas definidas para la aplicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Close()
            End If

            txtTelefono.Properties.Mask.EditMask = "\((\d{3})\) (\d{4})-(\d{4})"
            txtTelefono.Properties.Mask.MaskType = Mask.MaskType.RegEx

            LlenaTipoActualizacionCosto()

            pListDestinatarios = clsLnPropietario_destinatario.GetAllByIdPropietario(pBePropietario.IdPropietario).ToList()

            Init_DataTable_Productos()

            Select Case Modo

                Case TipoTrans.Nuevo

                    lblCodigo.Text = clsLnPropietarios.MaxID()
                    cmbEmpresa.Enabled = True

                    User_agrTextEdit.Text = AP.UsuarioAp.Nombres + " " + AP.UsuarioAp.Apellidos
                    Fec_agrDateEdit.Text = Now
                    User_modTextEdit.Text = AP.UsuarioAp.Nombres + " " + AP.UsuarioAp.Apellidos
                    Fec_modDateEdit.Text = Now

                    If OpcionesMenu IsNot Nothing Then
                        mnuGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False
                    cmbEmpresa.Enabled = True
                    mnuStock.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                    mnuMovimientos.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

                    xtraPropietario.TabPages.Remove(PropietarioBodega)
                    xtraPropietario.TabPages.Remove(Destinatarios)
                    xtraPropietario.TabPages.Remove(tabUM)
                    xtraPropietario.TabPages.Remove(TabEstados)
                    xtraPropietario.TabPages.Remove(TabProductos)
                    xtraPropietario.TabPages.Remove(TabStock)
                    xtraPropietario.TabPages.Remove(tabMovimientos)

                Case TipoTrans.Editar

                    CargarDatos()

                    mnuGuardar.Enabled = False

                    If OpcionesMenu IsNot Nothing Then
                        mnuActualizar.Enabled = OpcionesMenu.Modificar
                        mnuEliminar.Enabled = OpcionesMenu.Eliminar
                    End If

            End Select

            If pListDestinatarios IsNot Nothing AndAlso pListDestinatarios.Count = 0 Then
                lblCodigoD.Text = "1"
            Else
                lblCodigoD.Text = pListDestinatarios.Max(Function(b) b.IdDestinatarioPropietario) + 1
            End If

            ContactoTextEdit.Focus()
            Application.DoEvents()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub Listar_Reglas()

        Try

            GridMensaje.DataSource = Nothing

            Dim DT As New DataTable("Destinatario")
            DT.Columns.Add("Código", GetType(Integer))
            DT.Columns.Add("Regla", GetType(String))
            DT.Columns.Add("Propietario", GetType(String))
            DT.Columns.Add("Mensaje", GetType(String))
            '#GT14112025: nueva columna para diferenciar sobre que aplica la regla
            DT.Columns.Add("TipoRegla", GetType(String))

            If pListObjRE IsNot Nothing AndAlso pListObjRE.Count > 0 Then

                For Each r As clsBePropietario_reglas_enc In pListObjRE.FindAll(Function(b) b.Activo = chkActivoM.Checked)
                    DT.Rows.Add(r.IdReglaPropietarioEnc, r.Regla.Nombre, r.Propietario, r.Mensaje.Nombre, r.TipoRegla)
                Next

            End If

            GridMensaje.DataSource = DT
            ViewMensaje.Columns("Propietario").Visible = False
            ViewMensaje.Columns(0).GroupIndex = 0
            ViewMensaje.OptionsBehavior.AutoExpandAllGroups = True
            ViewMensaje.BestFitColumns()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub CargarDatos()

        Try

            CargarEncagezado()

            ValidaBodegas()

            If pIdPropietario = 0 Then pIdPropietario = pBePropietario.IdPropietario

            pListObjRE = clsLnPropietario_reglas_enc.Get_All_By_IdPropietario(pBePropietario.IdPropietario)

            xtraPropietario.TabPages.Add(tabUM)
            xtraPropietario.TabPages.Add(TabEstados)
            xtraPropietario.TabPages.Add(TabProductos)
            xtraPropietario.TabPages.Add(TabStock)
            xtraPropietario.TabPages.Add(tabMovimientos)

            '#CKFK20260415 Puse esto en comentario porque demora demasiado en cargar la forma cuando son muchos productos
            'Listar_Productos_By_Propietario()
            Listar_Estados_By_Propietario()
            Listar_Unidades_De_Medida_By_Propietario()
            Listar_Reglas()

            pListObjRD = clsLnPropietario_reglas_det.GetAll()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub CargarEncagezado()

        Try

            Dim BePropietario As New clsBePropietarios
            BePropietario = clsLnPropietarios.GetSingle(pBePropietario.IdPropietario)

            CopyObject(BePropietario, pBePropietario)

            lblCodigo.Text = pBePropietario.IdPropietario
            cmbEmpresa.EditValue = pBePropietario.IdEmpresa

            If pBePropietario.IdTipoActualizacionCosto <> Nothing AndAlso pBePropietario.IdTipoActualizacionCosto <> 0 Then
                cmbTipoActualizacion.EditValue = pBePropietario.IdTipoActualizacionCosto
            Else
                cmbTipoActualizacion.ItemIndex = -1
            End If

            ContactoTextEdit.Text = pBePropietario.Contacto
            Nombre_comercialTextEdit.Text = pBePropietario.Nombre_comercial
            txtTelefono.Text = pBePropietario.Telefono
            DireccionTextEdit.Text = pBePropietario.Direccion
            EmailTextEdit.Text = pBePropietario.Email
            txtCodigo.Text = pBePropietario.Codigo
            txtNIT.Text = pBePropietario.NIT

            txtCodigo.ReadOnly = pBePropietario.Sistema

            If pBePropietario.Imagen IsNot Nothing Then
                picFoto.Image = ByteArrayToImage(pBePropietario.Imagen)
            End If

            picFoto.SizeMode = PictureBoxSizeMode.CenterImage
            chkActualizarPrecioOC.Checked = pBePropietario.Actualiza_costo_oc
            chkActivo.Checked = pBePropietario.Activo
            chkEsConsolidador.Checked = pBePropietario.Es_Consolidador

            User_agrTextEdit.Text = pBePropietario.User_agr
            Fec_agrDateEdit.Text = pBePropietario.Fec_agr
            User_modTextEdit.Text = pBePropietario.User_mod
            Fec_modDateEdit.Text = pBePropietario.Fec_mod

            chkActivarUX.Checked = pBePropietario.ControlUx

            cmbEmpresa.Enabled = False
            mnuGuardar.Enabled = False
            mnuActualizar.Enabled = True
            mnuEliminar.Enabled = True
            cmbEmpresa.Enabled = False
            mnuStock.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            mnuMovimientos.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

            Cargar_Destinatarios()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick

        mnuGuardar.Enabled = False

        If Datos_Correctos() Then

            If MessageBox.Show("¿Guardar Propietario?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                If Guardar() Then

                    If MessageBox.Show("Se guardó el registro. ¿Asignar Bodegas?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                        xtraPropietario.TabPages.Add(PropietarioBodega)
                        xtraPropietario.TabPages.Add(Destinatarios)
                        xtraPropietario.TabPages.Add(tabUM)
                        xtraPropietario.TabPages.Add(TabEstados)
                        xtraPropietario.TabPages.Add(TabProductos)
                        xtraPropietario.TabPages.Add(TabStock)
                        xtraPropietario.TabPages.Add(tabMovimientos)

                        ValidaBodegas()

                        Listar_Unidades_De_Medida_By_Propietario()
                        Listar_Estados_By_Propietario()

                        '#CKFK20260415 Puse esto en comentario porque demora demasiado en cargar la forma cuando son muchos productos
                        ' Listar_Productos_By_Propietario()

                        mnuGuardar.Enabled = False
                        mnuActualizar.Enabled = True
                        mnuEliminar.Enabled = True

                    Else
                        InvokeListarPropietarios.Invoke
                        Close()
                    End If

                End If

            End If

        End If

        mnuGuardar.Enabled = True

    End Sub

    Private Sub Listar_Movimientos_By_Propietario()

        Dim dt As DataTable

        Try

            dt = clsLnTrans_movimientos.Get_All_Movimientos_By_IdPropietario(pIdPropietario)

            dgridMovimientos.DataSource = Nothing

            If dt.Rows.Count > 0 Then

                dgridMovimientos.DataSource = dt

                gviewMovimientos.OptionsView.ShowFooter = True
                gviewMovimientos.Columns("cantidad").SummaryItem.SummaryType = SummaryItemType.Sum
                gviewMovimientos.Columns("cantidad").SummaryItem.DisplayFormat = "{0:n6}"
                gviewMovimientos.Columns("cantidad").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                gviewMovimientos.Columns("cantidad").DisplayFormat.FormatString = "{0:n6}"

                gviewMovimientos.Columns("Cantidad_Presentacion").SummaryItem.SummaryType = SummaryItemType.Sum
                gviewMovimientos.Columns("Cantidad_Presentacion").SummaryItem.DisplayFormat = "{0:n6}"
                gviewMovimientos.Columns("Cantidad_Presentacion").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                gviewMovimientos.Columns("Cantidad_Presentacion").DisplayFormat.FormatString = "{0:n6}"

                gviewMovimientos.Columns("peso").SummaryItem.SummaryType = SummaryItemType.Sum
                gviewMovimientos.Columns("peso").SummaryItem.DisplayFormat = "{0:n6}"

                gviewMovimientos.Columns("peso").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                gviewMovimientos.Columns("peso").DisplayFormat.FormatString = "{0:n6}"

                gviewMovimientos.Columns("fecha").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                gviewMovimientos.Columns("fecha").DisplayFormat.FormatString = "G"

                gviewMovimientos.Columns("IdBodegaOrigen").Visible = False

                gviewMovimientos.Columns("codigo_barra").Caption = "Código Barra"
                gviewMovimientos.Columns("codigo").Caption = "Código"
                gviewMovimientos.Columns("cantidad").Caption = "Cantidad U.M.Bas"
                gviewMovimientos.Columns("peso").Caption = "Peso"
                gviewMovimientos.Columns("lote").Caption = "Lote"
                gviewMovimientos.Columns("fecha").Caption = "Fecha_Movimiento"
                gviewMovimientos.Columns("fecha_vence").Caption = "Fecha Vence"
                gviewMovimientos.Columns("barra_pallet").Caption = "Licencia"
                gviewMovimientos.Columns("Cantidad_Presentacion").Caption = "Cantidad Presentación"

                gviewMovimientos.Columns("Propietario").VisibleIndex = 0
                gviewMovimientos.Columns("Poliza").VisibleIndex = 1
                gviewMovimientos.Columns("Producto").VisibleIndex = 2
                gviewMovimientos.Columns("codigo").VisibleIndex = 3
                gviewMovimientos.Columns("codigo_barra").VisibleIndex = 4
                gviewMovimientos.Columns("cantidad").VisibleIndex = 5
                gviewMovimientos.Columns("Unidad de Medida").VisibleIndex = 6
                gviewMovimientos.Columns("Cantidad_Presentacion").VisibleIndex = 8
                gviewMovimientos.Columns("Presentación").VisibleIndex = 9
                gviewMovimientos.Columns("peso").VisibleIndex = 10
                gviewMovimientos.Columns("lote").VisibleIndex = 11
                gviewMovimientos.Columns("fecha_vence").VisibleIndex = 12
                gviewMovimientos.Columns("barra_pallet").VisibleIndex = 13
                gviewMovimientos.Columns("fecha").VisibleIndex = 14
                gviewMovimientos.Columns("Estado Origen").VisibleIndex = 15
                gviewMovimientos.Columns("Estado Destino").VisibleIndex = 16
                gviewMovimientos.Columns("IdProducto").Visible = False

            End If

            If gviewMovimientos.Columns.Count > 0 Then
                gviewMovimientos.BestFitColumns()
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

    Private Sub Listar_Stock_By_Propietario()

        Try

            Dim DT As New DataTable
            DT = clsLnStock.Get_Reporte_Stock_By_Propietario(pIdPropietario)

            If DT.Rows.Count > 0 Then

                dgridStock.DataSource = DT

                grdvStock.OptionsView.ShowFooter = True

                grdvStock.Columns("Cantidad_Reservada_UMBas").SummaryItem.SummaryType = SummaryItemType.Sum
                grdvStock.Columns("Cantidad_Reservada_UMBas").SummaryItem.DisplayFormat = "{0:n6}"
                grdvStock.Columns("Cantidad_Reservada_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvStock.Columns("Cantidad_Reservada_UMBas").DisplayFormat.FormatString = "{0:n6}"

                grdvStock.Columns("Cantidad_Reservada_Pres").SummaryItem.SummaryType = SummaryItemType.Sum
                grdvStock.Columns("Cantidad_Reservada_Pres").SummaryItem.DisplayFormat = "{0:n6}"
                grdvStock.Columns("Cantidad_Reservada_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvStock.Columns("Cantidad_Reservada_Pres").DisplayFormat.FormatString = "{0:n6}"

                grdvStock.Columns("Peso").SummaryItem.SummaryType = SummaryItemType.Sum
                grdvStock.Columns("Peso").SummaryItem.DisplayFormat = "{0:n6}"
                grdvStock.Columns("Peso").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvStock.Columns("Peso").DisplayFormat.FormatString = "{0:n6}"

                grdvStock.Columns("CantidadUMBas").SummaryItem.SummaryType = SummaryItemType.Sum
                grdvStock.Columns("CantidadUMBas").SummaryItem.DisplayFormat = "{0:n6}"
                grdvStock.Columns("CantidadUMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvStock.Columns("CantidadUMBas").DisplayFormat.FormatString = "{0:n6}"

                grdvStock.Columns("CantidadPresentacion").SummaryItem.SummaryType = SummaryItemType.Sum
                grdvStock.Columns("CantidadPresentacion").SummaryItem.DisplayFormat = "{0:n6}"
                grdvStock.Columns("CantidadPresentacion").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvStock.Columns("CantidadPresentacion").DisplayFormat.FormatString = "{0:n6}"

                grdvStock.Columns("Disponible_UMBas").SummaryItem.SummaryType = SummaryItemType.Sum
                grdvStock.Columns("Disponible_UMBas").SummaryItem.DisplayFormat = "{0:n6}"
                grdvStock.Columns("Disponible_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvStock.Columns("Disponible_UMBas").DisplayFormat.FormatString = "{0:n6}"

                grdvStock.Columns("Disponible_Presentación").SummaryItem.SummaryType = SummaryItemType.Sum
                grdvStock.Columns("Disponible_Presentación").SummaryItem.DisplayFormat = "{0:n6}"
                grdvStock.Columns("Disponible_Presentación").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvStock.Columns("Disponible_Presentación").DisplayFormat.FormatString = "{0:n6}"

                grdvStock.Columns("Fecha_Ingreso").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grdvStock.Columns("Fecha_Ingreso").DisplayFormat.FormatString = "G"

                Dim item As GridGroupSummaryItem = New GridGroupSummaryItem() _
                    With {.FieldName = "Cantidad_Reservada_UMBas", .SummaryType = SummaryItemType.Sum,
                    .DisplayFormat = "{0:n6}", .ShowInGroupColumnFooter = grdvStock.Columns("Cantidad_Reservada")}
                grdvStock.GroupSummary.Add(item)

                Dim item1 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                    With {.FieldName = "Cantidad_UMBas", .SummaryType = SummaryItemType.Sum,
                    .DisplayFormat = "{0:n6}", .ShowInGroupColumnFooter = grdvStock.Columns("Cantidad_UMBas")}
                grdvStock.GroupSummary.Add(item1)

                Dim item2 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                    With {.FieldName = "Cantidad_Presentacion", .SummaryType = SummaryItemType.Sum,
                    .DisplayFormat = "{0:n6}", .ShowInGroupColumnFooter = grdvStock.Columns("Cantidad_Presentacion")}
                grdvStock.GroupSummary.Add(item2)

                Dim item3 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                    With {.FieldName = "Disponible_UMBas", .SummaryType = SummaryItemType.Sum,
                    .DisplayFormat = "{0:n6}", .ShowInGroupColumnFooter = grdvStock.Columns("Disponible_UMBas")}
                grdvStock.GroupSummary.Add(item3)


                Dim item4 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                    With {.FieldName = "Disponible_Presentación", .SummaryType = SummaryItemType.Sum,
                    .DisplayFormat = "{0:n6}", .ShowInGroupColumnFooter = grdvStock.Columns("Disponible_Presentación")}
                grdvStock.GroupSummary.Add(item4)

                grdvStock.Columns("IdPresentacion").Visible = False

                grdvStock.ExpandAllGroups()

                grdvStock.BestFitColumns()

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Private Sub Listar_Estados_By_Propietario()

        Try

            Dim lista As New List(Of clsBeProducto_estado)
            lista = clsLnProducto_estado.Get_All_Filtro(True, pIdPropietario)

            If lista IsNot Nothing AndAlso lista.Count > 0 Then

                Dim DT As New DataTable("Estado")
                DT.Columns.Add("IdEstado", GetType(Integer))
                DT.Columns.Add("Estado", GetType(String))

                For Each BeProductoEstado As clsBeProducto_estado In lista
                    DT.Rows.Add(BeProductoEstado.IdEstado,
                                BeProductoEstado.Nombre)
                Next

                dgridEstados.DataSource = DT

                If (gvEstados.Columns.Count <> 0) Then

                    Try

                        gvEstados.Columns("IdEstado").Caption = "Código"
                        gvEstados.OptionsView.ColumnAutoWidth = False
                        gvEstados.BestFitColumns()

                    Catch ex As Exception
                        XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End Try

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try


    End Sub
    Private Sub Listar_Productos_By_Propietario()

        Try

            dgridProductos.DataSource = Nothing

            Dim DTProducto As New DataTable

            DTproductoGrid.Clear()

            If pIdPropietario <> 0 Then
                DTProducto = clsLnProducto.Get_All_By_IdPropietario(pIdPropietario)
            End If

            If DTProducto IsNot Nothing AndAlso DTProducto.Rows.Count > 0 Then

                For Each BeProducto As DataRow In DTProducto.Rows

                    Dim newRow As DataRow = DTproductoGrid.NewRow()

                    With newRow

                        .Item("Correlativo") = BeProducto("IdProductoBodega")
                        .Item("Clasificación") = BeProducto("Clasificación")
                        .Item("Familia") = BeProducto("Familia")
                        .Item("Marca") = BeProducto("Marca")
                        .Item("UMBas") = BeProducto("Unidad Medida")
                        .Item("Código") = BeProducto("Codigo")
                        .Item("Codigo_Barra") = BeProducto("Codigo_barra")
                        .Item("Nombre") = BeProducto("Nombre")
                        .Item("Costo") = BeProducto("Costo")
                        .Item("Precio") = BeProducto("Precio")
                        .Item("Kit") = BeProducto("Kit")
                        .Item("Fecha_Agr") = BeProducto("Fec_agr")

                    End With

                    DTproductoGrid.Rows.Add(newRow)

                Next

            End If

            dgridProductos.DataSource = DTproductoGrid

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name, ex.Message),
        Text,
        MessageBoxButtons.OK,
        MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name, ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub
    Private Sub Listar_Unidades_De_Medida_By_Propietario()

        Try

            Dim DT As New DataTable
            DT = clsLnUnidad_medida.Get_All_By_IdPropietario(pIdPropietario)

            dgridUM.DataSource = DT

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Function Guardar() As Boolean

        Guardar = False

        Try

            Dim BePropietario As New clsBePropietarios() With
                {.IdPropietario = clsLnPropietarios.MaxID(),
                .IdEmpresa = cmbEmpresa.EditValue}

            If chkActualizarPrecioOC.Checked Then
                BePropietario.IdTipoActualizacionCosto = cmbTipoActualizacion.EditValue
            Else
                BePropietario.IdTipoActualizacionCosto = Nothing
            End If

            BePropietario.Contacto = ContactoTextEdit.Text.Trim()
            BePropietario.Nombre_comercial = Nombre_comercialTextEdit.Text.Trim()
            BePropietario.Telefono = txtTelefono.Text.Trim()
            BePropietario.Direccion = DireccionTextEdit.Text.Trim()
            BePropietario.Email = EmailTextEdit.Text.Trim()
            BePropietario.Codigo = txtCodigo.Text.Trim()
            BePropietario.NIT = txtNIT.Text.Trim()

            If picFoto.Image IsNot Nothing Then
                BePropietario.Imagen = ImageToByteArray(picFoto.Image)
            End If

            BePropietario.Activo = True
            BePropietario.User_agr = AP.UsuarioAp.IdUsuario
            BePropietario.Fec_agr = Now
            BePropietario.User_mod = AP.UsuarioAp.IdUsuario
            BePropietario.Fec_mod = Now
            BePropietario.Actualiza_costo_oc = chkActualizarPrecioOC.Checked

            If Not txtCodigoAcceso.Text = "" AndAlso Not txtClaveAcceso.Text = "" Then
                If txtClaveAcceso.Text = txtConfirmarClave.Text Then
                    BePropietario.Codigo_Acceso = txtCodigoAcceso.Text.Trim()
                    BePropietario.Clave_Acceso = clsPublic.Encriptar(txtClaveAcceso.Text.Trim())
                Else
                    Throw New Exception("Las claves de acceso para TOMWMSUX no coinciden.")
                End If
            End If

            pBePropietario.IdPropietario = BePropietario.IdPropietario
            BePropietario.Es_Consolidador = chkEsConsolidador.Checked
            BePropietario.ControlUx = chkActivarUX.Checked

            Guardar = IIf(clsLnPropietarios.Guardar_Nuevo_Propietario(BePropietario, AP.IdBodega) > 0, True, False)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Datos_Correctos() As Boolean

        Datos_Correctos = False

        Try

            If cmbEmpresa.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Empresa.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            ElseIf String.IsNullOrEmpty(ContactoTextEdit.Text.Trim) Then
                XtraMessageBox.Show("Ingrese contacto.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ContactoTextEdit.Focus()

            ElseIf ContactoTextEdit.Text.Count > pListObjT.Find(Function(b) b.NombreCampo = "contacto").Longitud Then
                XtraMessageBox.Show("El contacto debe de tener como máximo " & pListObjT.Find(Function(b) b.NombreCampo = "contacto").Longitud & " carácteres.",
                                    Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ContactoTextEdit.Focus()

            ElseIf String.IsNullOrEmpty(Nombre_comercialTextEdit.Text.Trim) Then
                XtraMessageBox.Show("Ingrese Nombre Comercial.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Nombre_comercialTextEdit.Focus()

            ElseIf Nombre_comercialTextEdit.Text.Count > pListObjT.Find(Function(b) b.NombreCampo = "nombre_comercial").Longitud Then
                XtraMessageBox.Show("El Nombre Comercial debe de tener como máximo " & pListObjT.Find(Function(b) b.NombreCampo = "nombre_comercial").Longitud & " carácteres.",
                                    Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Nombre_comercialTextEdit.Focus()

            ElseIf txtTelefono.Text.Count > pListObjT.Find(Function(b) b.NombreCampo = "telefono").Longitud Then
                XtraMessageBox.Show("El Teléfono debe de tener como máximo " & pListObjT.Find(Function(b) b.NombreCampo = "telefono").Longitud & " carácteres.",
                                    Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtTelefono.Focus()

            ElseIf DireccionTextEdit.Text.Count > pListObjT.Find(Function(b) b.NombreCampo = "direccion").Longitud Then
                XtraMessageBox.Show("La Dirección debe de tener como máximo " & pListObjT.Find(Function(b) b.NombreCampo = "direccion").Longitud & " carácteres.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                DireccionTextEdit.Focus()

            ElseIf EmailTextEdit.Text.Count > pListObjT.Find(Function(b) b.NombreCampo = "email").Longitud Then
                XtraMessageBox.Show("El Correo electrónico debe de tener como máximo " & pListObjT.Find(Function(b) b.NombreCampo = "email").Longitud & " carácteres.",
                                    Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                EmailTextEdit.Focus()

            ElseIf chkActualizarPrecioOC.Checked Then

                If cmbTipoActualizacion.ItemIndex = -1 Then
                    XtraMessageBox.Show("Seleccione tipo de actualización costo.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else
                    Datos_Correctos = True
                End If

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

                If chkActualizarPrecioOC.Checked Then
                    pBePropietario.IdTipoActualizacionCosto = cmbTipoActualizacion.EditValue
                Else
                    pBePropietario.IdTipoActualizacionCosto = Nothing
                End If

                pBePropietario.IdEmpresa = cmbEmpresa.EditValue
                pBePropietario.Contacto = ContactoTextEdit.Text.Trim()
                pBePropietario.Nombre_comercial = Nombre_comercialTextEdit.Text.Trim()
                pBePropietario.Telefono = txtTelefono.Text.Trim()
                pBePropietario.Direccion = DireccionTextEdit.Text.Trim()
                pBePropietario.Email = EmailTextEdit.Text.Trim()
                pBePropietario.Activo = chkActivo.Checked
                pBePropietario.Codigo = txtCodigo.Text.Trim()
                pBePropietario.NIT = txtNIT.Text.Trim()

                If picFoto.Image IsNot Nothing Then
                    pBePropietario.Imagen = ImageToByteArray(picFoto.Image)
                End If

                pBePropietario.User_mod = AP.UsuarioAp.IdUsuario
                pBePropietario.Fec_mod = Now
                pBePropietario.Actualiza_costo_oc = chkActualizarPrecioOC.Checked
                pBePropietario.Es_Consolidador = chkEsConsolidador.Checked

                If Not txtCodigoAcceso.Text = "" AndAlso Not txtClaveAcceso.Text = "" Then
                    If txtClaveAcceso.Text = txtConfirmarClave.Text Then
                        pBePropietario.Codigo_Acceso = txtCodigoAcceso.Text.Trim()
                        pBePropietario.Clave_Acceso = clsPublic.Encriptar(txtClaveAcceso.Text.Trim())
                    Else
                        Throw New Exception("Las claves de acceso para TOMWMSUX no coinciden.")
                    End If
                End If

                pBePropietario.ControlUx = chkActivarUX.Checked

                Return clsLnPropietarios.ActualizarDatos(pBePropietario, gBePropietarioBodegaList, pListDestinatarios)

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        mnuActualizar.Enabled = False
        If Actualizar() Then
            XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            InvokeListarPropietarios.Invoke
            Close()
        End If
        mnuActualizar.Enabled = True

    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try

            mnuEliminar.Enabled = False

            If MessageBox.Show("¿Desactivar Propietario?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                Dim ObjMD As New clsBePropietarios
                CopyObject(pBePropietario, ObjMD)

                If clsLnPropietarios.Eliminar(ObjMD) > 0 Then
                    XtraMessageBox.Show("Se ha desactivado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    InvokeListarPropietarios.Invoke
                    Close()
                    frmPropietario_List.Dgrid.Refresh()
                End If

            End If

            mnuEliminar.Enabled = True
        Catch ex As Exception
            mnuEliminar.Enabled = True
            If ex.HResult = -2146233088 Then
                TablasRelacionadas("Propietarios", lblCodigo.Text)
            Else
                XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

        End Try

    End Sub

    Public Function ByteArrayToImage(ByVal byteArrayIn As Byte()) As System.Drawing.Image
        Dim ms As New MemoryStream(byteArrayIn)
        Return System.Drawing.Image.FromStream(ms)
    End Function

    Public Function ImageToByteArray(ByVal imageIn As System.Drawing.Image) As Byte()
        Dim ms As New MemoryStream()
        imageIn.Save(ms, ImageFormat.Jpeg)
        Return ms.ToArray()
    End Function

    Private Sub chkActualizarPrecioOC_CheckedChanged(sender As Object, e As EventArgs) Handles chkActualizarPrecioOC.CheckedChanged
        If chkActualizarPrecioOC.Checked Then
            cmbTipoActualizacion.Enabled = True
        Else
            cmbTipoActualizacion.Enabled = False
            cmbTipoActualizacion.ItemIndex = -1
        End If
    End Sub

    Private Sub btnExaminar_Click(sender As Object, e As EventArgs) Handles btnExaminar.Click
        Dim gFile As New OpenFileDialog
        gFile.Filter = "All Images|*.BMP;*.DIB;*.RLE;*.JPG;*.JPEG;*.JPE;*.JFIF;*.GIF;*.TIF;*.TIFF;*.PNG;*.ico|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|All Files|*.*"
        gFile.ShowDialog()
        If gFile.FileName.Length <> 0 Then
            picFoto.Image = Image.FromFile(gFile.FileName)
        End If
    End Sub

    'Destinatarios
    Private Sub Cargar_Destinatarios()

        Try

            Limpiar()

            Dim DT As New DataTable("Destinatarios")
            DT.Columns.Add("Correlativo", GetType(Integer))
            DT.Columns.Add("IdPropietario", GetType(Integer))
            DT.Columns.Add("Nombre", GetType(String))
            DT.Columns.Add("Apellido", GetType(String))
            DT.Columns.Add("Correo Electronico", GetType(String))
            DT.Columns.Add("Telefono", GetType(String))
            DT.Columns.Add("Telefono2", GetType(String))
            DT.Columns.Add("Cargo", GetType(String))

            grdDetalle.DataSource = Nothing

            For Each BePropietarioDestinatario As clsBePropietario_destinatario In pListDestinatarios.FindAll(Function(b) b.Activo = chkDestinatarioActivo.Checked)

                Dim lRow As DataRow = DT.NewRow
                lRow(0) = BePropietarioDestinatario.IdDestinatarioPropietario
                lblCodigoD.Text = BePropietarioDestinatario.IdDestinatarioPropietario
                lRow(1) = BePropietarioDestinatario.IdPropietario
                lRow(2) = BePropietarioDestinatario.Nombre
                lRow(3) = BePropietarioDestinatario.Apellido
                lRow(4) = BePropietarioDestinatario.Correo_electronico
                lRow(5) = BePropietarioDestinatario.Telefono
                lRow(6) = BePropietarioDestinatario.Telefono1
                lRow(7) = BePropietarioDestinatario.Cargo
                DT.Rows.Add(lRow)

            Next

            grdDetalle.DataSource = DT
            GridViewDet.Columns("IdPropietario").Visible = False
            grdDetalle.Refresh()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdGuardar.Click

        Me.Cursor = Cursors.WaitCursor

        Try

            If String.IsNullOrEmpty(txtNombreDestinatario.Text) And String.IsNullOrEmpty(txtNombreDestinatario.Text) Then
                XtraMessageBox.Show("Ingrese Nombre.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombreDestinatario.Focus()
                Return
            ElseIf String.IsNullOrEmpty(txtApellidoDestinatario.Text) And String.IsNullOrEmpty(txtApellidoDestinatario.Text) Then
                XtraMessageBox.Show("Ingrese Apellido.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtApellidoDestinatario.Focus()
                Return
            ElseIf String.IsNullOrEmpty(txtEmailDestinatario.Text) And String.IsNullOrEmpty(txtEmailDestinatario.Text) Then
                XtraMessageBox.Show("Ingrese Email", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtEmailDestinatario.Focus()
                Return
            ElseIf String.IsNullOrEmpty(txtTelefono1.Text) And String.IsNullOrEmpty(txtTelefono1.Text) Then
                XtraMessageBox.Show("Ingrese Número de Teléfono", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtEmailDestinatario.Focus()
                Return
            End If

            Dim pIndex As Integer = -1

            pIndex = pListDestinatarios.FindIndex(Function(b) b.IdDestinatarioPropietario = CInt(cmdGuardar.Tag))

            If pIndex > -1 Then
                pListDestinatarios(pIndex).Nombre = txtNombreDestinatario.Text
                pListDestinatarios(pIndex).Apellido = txtApellidoDestinatario.Text
                pListDestinatarios(pIndex).Correo_electronico = txtEmailDestinatario.Text
                pListDestinatarios(pIndex).Telefono = txtTelefono1.Text
                pListDestinatarios(pIndex).Telefono1 = txtTelefono2.Text
                pListDestinatarios(pIndex).Cargo = txtCargoDestinatario.Text
                pListDestinatarios(pIndex).Activo = chkActivoDest.Checked
                cmdGuardar.Tag = Nothing
            Else

                Dim ObjDestinatario As New clsBePropietario_destinatario

                If pListDestinatarios IsNot Nothing AndAlso pListDestinatarios.Count > 0 Then
                    ObjDestinatario.IdDestinatarioPropietario = pListDestinatarios.Max(Function(b) b.IdDestinatarioPropietario) + 1
                Else
                    ObjDestinatario.IdDestinatarioPropietario = 1
                End If

                ObjDestinatario.Nombre = txtNombreDestinatario.Text
                ObjDestinatario.Apellido = txtApellidoDestinatario.Text
                ObjDestinatario.Correo_electronico = txtEmailDestinatario.Text
                ObjDestinatario.Telefono = txtTelefono1.Text
                ObjDestinatario.Telefono1 = txtTelefono2.Text
                ObjDestinatario.Cargo = txtCargoDestinatario.Text
                ObjDestinatario.Activo = True
                ObjDestinatario.IsNew = True
                pListDestinatarios.Add(ObjDestinatario)

            End If

            Cargar_Destinatarios()

            Limpiar()

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        Try
            If MessageBox.Show("¿Desactivar el Destinatario?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                pObjDestinatario.IdDestinatarioPropietario = CInt(cmdGuardar.Tag)

                Dim lIndex As Integer = -1
                If pObjDestinatario.IdDestinatarioPropietario > 0 Then
                    lIndex = pListDestinatarios.FindIndex(Function(b) b.IdDestinatarioPropietario = pObjDestinatario.IdDestinatarioPropietario)
                End If

                If lIndex > -1 Then
                    clsLnPropietario_destinatario.DeleteDestinatario(pObjDestinatario.IdDestinatarioPropietario)
                    pListDestinatarios.RemoveAt(lIndex)
                    Cargar_Destinatarios()
                    Limpiar()
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Limpiar()

        Try

            lblCodigoD.Text = "-"
            txtNombreDestinatario.Text = String.Empty
            txtApellidoDestinatario.Text = String.Empty
            txtEmailDestinatario.Text = String.Empty
            txtTelefono1.Text = String.Empty
            txtTelefono2.Text = String.Empty
            txtCargoDestinatario.Text = String.Empty
            txtNombreDestinatario.Focus()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub grdDetalle_DoubleClick(sender As Object, e As EventArgs) Handles grdDetalle.DoubleClick
        Try

            If GridViewDet.RowCount > 0 Then

                Dim Dr As DataRowView = GridViewDet.GetFocusedRow

                Dim lIndex As Integer = -1

                lIndex = pListDestinatarios.FindIndex(Function(b) b.IdDestinatarioPropietario = CInt(Dr.Item("Correlativo")))

                If lIndex > -1 Then
                    cmdGuardar.Tag = pListDestinatarios(lIndex).IdDestinatarioPropietario
                    lblCodigoD.Text = cmdGuardar.Tag
                    txtNombreDestinatario.Text = pListDestinatarios(lIndex).Nombre
                    txtApellidoDestinatario.Text = pListDestinatarios(lIndex).Apellido
                    txtEmailDestinatario.Text = pListDestinatarios(lIndex).Correo_electronico
                    txtTelefono1.Text = pListDestinatarios(lIndex).Telefono
                    txtTelefono2.Text = pListDestinatarios(lIndex).Telefono1
                    txtCargoDestinatario.Text = pListDestinatarios(lIndex).Cargo
                    chkActivoDest.Checked = pListDestinatarios(lIndex).Activo
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub GridMensaje_Click(sender As Object, e As EventArgs) Handles GridMensaje.Click

        pGridRegla = True
        GetDatos()

    End Sub

    Private Sub GetDatos()

        Try

            If ViewMensaje.RowCount > 0 Then
                Dim Dr As DataRowView = ViewMensaje.GetFocusedRow
                Listar_Destinatario(Dr.Item("Código"), chkActivoD.Checked)
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

    Private Sub Listar_Destinatario(ByVal pIdReglaPropietarioEnc As Integer, ByVal pActivo As Boolean)

        Try

            GridDestinatario.DataSource = Nothing

            If pListObjRD IsNot Nothing AndAlso pListObjRD.Count > 0 Then

                Dim DT As New DataTable("Destinatario")
                DT.Columns.Add("IdReglaPropietarioDet", GetType(Integer))
                DT.Columns.Add("Código", GetType(Integer))
                DT.Columns.Add("NombreDestinatario", GetType(String))

                For Each r As clsBePropietario_reglas_det In IIf(pIdReglaPropietarioEnc > 0, pListObjRD.FindAll(Function(b) b.IdReglaPropietarioEnc = pIdReglaPropietarioEnc _
                                                                                                                And b.Activo = chkActivoD.Checked),
                                                                                                                pListObjRD.FindAll(Function(c) c.Activo = chkActivoD.Checked))
                    DT.Rows.Add(r.IdReglaPropietarioDet, r.IdDestinatarioPropietario, r.NombreDestinatario)
                Next

                GridDestinatario.DataSource = DT

                If ViewDestinatario.Columns.Count > 0 Then
                    ViewDestinatario.Columns("IdReglaPropietarioDet").Visible = False
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

    Private Sub chkActivoD_CheckedChanged(sender As Object, e As EventArgs) Handles chkActivoD.CheckedChanged
        GetDatos()
    End Sub

    Private Sub chkActivoM_CheckedChanged(sender As Object, e As EventArgs) Handles chkActivoM.CheckedChanged
        Listar_Reglas()
    End Sub

    Private Sub cmdAgregar_Click(sender As Object, e As EventArgs) Handles cmdAgregar.Click

        Try

            Dim Add As New frmPropietarioReglaRecepcion(frmPropietarioReglaRecepcion.TipoTrans.Nuevo)
            Add.pNombrePropietario = Nombre_comercialTextEdit.Text.Trim
            Add.pIdPropietario = pBePropietario.IdPropietario
            Add.ShowDialog()

            pListObjRE = clsLnPropietario_reglas_enc.Get_All_By_IdPropietario(pBePropietario.IdPropietario).ToList

            Listar_Reglas()

            pListObjRD = clsLnPropietario_reglas_det.GetAll().ToList

            GetDatos()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub GridMensaje_DoubleClick(sender As Object, e As EventArgs) Handles GridMensaje.DoubleClick

        Try

            If ViewMensaje.RowCount > 0 Then
                Dim Dr As DataRowView = ViewMensaje.GetFocusedRow

                Dim Es_Proceso As Boolean

                Dim pReglaEnc = clsLnPropietario_reglas_enc.GetSingle(CInt(Dr.Item("Código")))
                If pReglaEnc IsNot Nothing Then
                    Dim regla_recepcion = New clsBeReglas_recepcion()
                    regla_recepcion.IdReglaRecepcion = pReglaEnc.IdReglaRecepcion

                    If clsLnReglas_recepcion.GetSingle(regla_recepcion) Then
                        Es_Proceso = regla_recepcion.Es_Proceso
                    End If
                End If


                If Es_Proceso Then

                    Dim i As Integer = ViewMensaje.FocusedRowHandle
                    Dim FrmPropietarioProcesos As New frmPropietarioReglasMensajes(frmPropietarioReglasMensajes.TipoTrans.Editar)
                    FrmPropietarioProcesos.pIdReglaPropietarioEnc = CInt(Dr.Item("Código"))
                    'Edit.pNombrePropietario = Nombre_comercialTextEdit.Text.Trim
                    FrmPropietarioProcesos.pBePropietario = pBePropietario
                    FrmPropietarioProcesos.ShowDialog()
                    pListObjRE = clsLnPropietario_reglas_enc.Get_All_By_IdPropietario(pBePropietario.IdPropietario).ToList
                    'Listar_Reglas()
                    'pListObjRD = clsLnPropietario_reglas_det.GetAll().ToList
                    'GetDatos()
                    ViewMensaje.FocusedRowHandle = i

                Else

                    Dim i As Integer = ViewMensaje.FocusedRowHandle
                    Dim Edit As New frmPropietarioReglaRecepcion(frmPropietarioReglaRecepcion.TipoTrans.Editar)
                    Edit.pIdReglaPropietarioEnc = CInt(Dr.Item("Código"))
                    Edit.pNombrePropietario = Nombre_comercialTextEdit.Text.Trim
                    Edit.pIdPropietario = pBePropietario.IdPropietario
                    Edit.ShowDialog()
                    pListObjRE = clsLnPropietario_reglas_enc.Get_All_By_IdPropietario(pBePropietario.IdPropietario).ToList
                    Listar_Reglas()
                    pListObjRD = clsLnPropietario_reglas_det.GetAll().ToList
                    GetDatos()
                    ViewMensaje.FocusedRowHandle = i

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

    Private Sub GridDestinatario_DoubleClick(sender As Object, e As EventArgs) Handles GridDestinatario.DoubleClick

    End Sub

    Private Sub chkDestinatarioActivo_CheckedChanged(sender As Object, e As EventArgs) Handles chkDestinatarioActivo.CheckedChanged
        Cargar_Destinatarios()
    End Sub


    Private Sub cmdEliminar_Click(sender As Object, e As EventArgs) Handles cmdEliminar.Click

        Try

            Dim i As Integer = -1

            If ViewMensaje.RowCount > 0 Then
                i = ViewMensaje.FocusedRowHandle
            End If

            If i > -1 AndAlso pGridRegla = True Then
                Dim Dr As DataRowView = ViewMensaje.GetFocusedRow
                If MessageBox.Show(String.Format("¿Desea desactivar la Regla {0}?", Dr.Item("Regla")), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    clsLnPropietario_reglas_enc.Desactivar_Regla(Dr.Item("Código"))
                    XtraMessageBox.Show("Regla desactivada.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    pListObjRE = clsLnPropietario_reglas_enc.Get_All_By_IdPropietario(pBePropietario.IdPropietario).ToList
                    Listar_Reglas()
                    pListObjRD = clsLnPropietario_reglas_det.GetAll().ToList
                End If
            Else
                If ViewDestinatario.RowCount > 0 Then
                    i = ViewDestinatario.FocusedRowHandle
                    If i > -1 AndAlso pGridRegla = False Then
                        Dim Dd As DataRowView = ViewDestinatario.GetFocusedRow
                        If MessageBox.Show(String.Format("¿Desea desactivar el Destinatario {0}?", Dd.Item("NombreDestinatario")), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            clsLnPropietario_reglas_enc.Desactivar_Regla(Dd.Item("IdReglaPropietarioDet"))
                            XtraMessageBox.Show("Destinatario desactivado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            pListObjRE = clsLnPropietario_reglas_enc.Get_All_By_IdPropietario(pBePropietario.IdPropietario).ToList
                            Listar_Reglas()
                            pListObjRD = clsLnPropietario_reglas_det.GetAll().ToList
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


    Private Sub GridDestinatario_Click(sender As Object, e As EventArgs) Handles GridDestinatario.Click
        pGridRegla = False
    End Sub

    Private Sub ViewMensaje_RowStyle(sender As Object, e As RowStyleEventArgs) Handles ViewMensaje.RowStyle

        Try

            ViewMensaje.OptionsBehavior.Editable = False
            ViewMensaje.OptionsSelection.EnableAppearanceFocusedCell = False

            ViewMensaje.FocusRectStyle = DrawFocusRectStyle.RowFocus

            ViewMensaje.OptionsSelection.EnableAppearanceFocusedRow = True
            ViewMensaje.OptionsSelection.EnableAppearanceHideSelection = True
            ViewMensaje.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            ViewMensaje.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")

            ViewMensaje.Appearance.FocusedRow.ForeColor = Color.White
            ViewMensaje.Appearance.SelectedRow.ForeColor = Color.White

            ViewMensaje.Appearance.SelectedRow.Options.UseBackColor = True
            ViewMensaje.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub ViewDestinatario_RowStyle(sender As Object, e As RowStyleEventArgs) Handles ViewDestinatario.RowStyle

        Try

            ViewDestinatario.OptionsBehavior.Editable = False
            ViewDestinatario.OptionsSelection.EnableAppearanceFocusedCell = False

            ViewDestinatario.FocusRectStyle = DrawFocusRectStyle.RowFocus

            ViewDestinatario.OptionsSelection.EnableAppearanceFocusedRow = True
            ViewDestinatario.OptionsSelection.EnableAppearanceHideSelection = True
            ViewDestinatario.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            ViewDestinatario.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")

            ViewDestinatario.Appearance.FocusedRow.ForeColor = Color.White
            ViewDestinatario.Appearance.SelectedRow.ForeColor = Color.White

            ViewDestinatario.Appearance.SelectedRow.Options.UseBackColor = True
            ViewDestinatario.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub gridView1_RowStyle(sender As Object, e As RowStyleEventArgs) Handles gridView1.RowStyle

        Try

            gridView1.OptionsBehavior.Editable = True
            gridView1.OptionsSelection.EnableAppearanceFocusedCell = False

            gridView1.FocusRectStyle = DrawFocusRectStyle.RowFocus

            gridView1.OptionsSelection.EnableAppearanceFocusedRow = True
            gridView1.OptionsSelection.EnableAppearanceHideSelection = True
            gridView1.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            gridView1.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")

            gridView1.Appearance.FocusedRow.ForeColor = Color.White
            gridView1.Appearance.SelectedRow.ForeColor = Color.White

            gridView1.Appearance.SelectedRow.Options.UseBackColor = True
            gridView1.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub GridViewDet_RowStyle(sender As Object, e As RowStyleEventArgs) Handles GridViewDet.RowStyle

        Try

            GridViewDet.OptionsBehavior.Editable = False
            GridViewDet.OptionsSelection.EnableAppearanceFocusedCell = False

            GridViewDet.FocusRectStyle = DrawFocusRectStyle.RowFocus

            GridViewDet.OptionsSelection.EnableAppearanceFocusedRow = True
            GridViewDet.OptionsSelection.EnableAppearanceHideSelection = True
            GridViewDet.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            GridViewDet.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")

            GridViewDet.Appearance.FocusedRow.ForeColor = Color.White
            GridViewDet.Appearance.SelectedRow.ForeColor = Color.White

            GridViewDet.Appearance.SelectedRow.Options.UseBackColor = True
            GridViewDet.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub frmPropietario_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub cmdNewPR_Click(sender As Object, e As EventArgs) Handles cmdNewPR.Click
        Limpiar()
    End Sub

    Private Sub mnuStock_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuStock.ItemClick

        Try

            If XtraMessageBox.Show("¿Listar el stock puede demorar, continuar?", "Consulta de stock en propietario.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Listar_Stock_By_Propietario()
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

    Private Sub mnuMovimientos_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuMovimientos.ItemClick


        Try

            If XtraMessageBox.Show("¿Listar los movimientos puede demorar, se mostrarán los movimientos del último més continuar?", "Consulta de movimientos en propietario.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Listar_Movimientos_By_Propietario()
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

    Private Sub cmdAlertas_Click(sender As Object, e As EventArgs) Handles cmdAlertas.Click
        Try

            Dim Add As New frmPropietarioReglasMensajes(frmPropietarioReglasMensajes.TipoTrans.Nuevo)
            Add.pIdReglaPropietarioEnc = 0
            Add.pBePropietario = pBePropietario
            Add.ShowDialog()


        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Sub

    Private Sub GrpEmpresaTB_Paint(sender As Object, e As PaintEventArgs) Handles GrpEmpresaTB.Paint

    End Sub
End Class