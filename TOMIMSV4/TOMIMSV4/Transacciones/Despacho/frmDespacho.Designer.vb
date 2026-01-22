<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDespacho
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If components IsNot Nothing Then
                components.Dispose()
            End If
            If DTStockRes IsNot Nothing Then
                DTStockRes.Dispose()
                DTStockRes = Nothing
            End If
            If BeDespachoEnc IsNot Nothing Then
                BeDespachoEnc.Dispose()
                BeDespachoEnc = Nothing
            End If
            If gBeRoadRuta IsNot Nothing Then
                gBeRoadRuta.Dispose()
                gBeRoadRuta = Nothing
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim Label10 As System.Windows.Forms.Label
        Dim Label12 As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim IdPropietarioLabel As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim Label37 As System.Windows.Forms.Label
        Dim Label38 As System.Windows.Forms.Label
        Dim Label30 As System.Windows.Forms.Label
        Dim Label7 As System.Windows.Forms.Label
        Dim Fec_agrLabel As System.Windows.Forms.Label
        Dim User_agrLabel As System.Windows.Forms.Label
        Dim Fec_modLabel As System.Windows.Forms.Label
        Dim User_modLabel As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim Label5 As System.Windows.Forms.Label
        Dim Label6 As System.Windows.Forms.Label
        Dim Label8 As System.Windows.Forms.Label
        Dim Label9 As System.Windows.Forms.Label
        Dim Label11 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDespacho))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuAnular = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImprimir = New DevExpress.XtraBars.BarSubItem()
        Me.cmdListaUbicacion = New DevExpress.XtraBars.BarButtonItem()
        Me.btwnegativeStock = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.lnkPedido = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuImprimir = New DevExpress.XtraBars.BarSubItem()
        Me.mnuImprimirDespacho = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuImprimirListaEmpaque = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuGuardarDatosCabecera = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuRepetirEncabezadoEnCadaPagina = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.XtraTabControl1 = New DevExpress.XtraTab.XtraTabControl()
        Me.tbPagina1 = New DevExpress.XtraTab.XtraTabPage()
        Me.GrpDato = New DevExpress.XtraEditors.GroupControl()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.grpTransporte = New DevExpress.XtraEditors.GroupControl()
        Me.lcmbRuta = New DevExpress.XtraEditors.LookUpEdit()
        Me.lcmbVehiculo = New DevExpress.XtraEditors.LookUpEdit()
        Me.lcmbPiloto = New DevExpress.XtraEditors.LookUpEdit()
        Me.lnkRuta = New System.Windows.Forms.LinkLabel()
        Me.lnkVehiculo = New System.Windows.Forms.LinkLabel()
        Me.lnkPiloto = New System.Windows.Forms.LinkLabel()
        Me.grpDatosAsociados = New DevExpress.XtraEditors.GroupControl()
        Me.txtDocumentoExterno = New System.Windows.Forms.TextBox()
        Me.txtCantidadBultos = New System.Windows.Forms.NumericUpDown()
        Me.txtObservacion = New System.Windows.Forms.TextBox()
        Me.txtMarchamo = New System.Windows.Forms.TextBox()
        Me.txtNumero = New System.Windows.Forms.NumericUpDown()
        Me.txtNoPase = New System.Windows.Forms.NumericUpDown()
        Me.grpDatosGenerales = New DevExpress.XtraEditors.GroupControl()
        Me.lblC = New System.Windows.Forms.TextBox()
        Me.dtmFechaDespacho = New DevExpress.XtraEditors.DateEdit()
        Me.cmbPropietario = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbBodega = New DevExpress.XtraEditors.LookUpEdit()
        Me.chkActivo = New DevExpress.XtraEditors.CheckEdit()
        Me.lblEstado = New System.Windows.Forms.Label()
        Me.grpDatosFecha = New DevExpress.XtraEditors.GroupControl()
        Me.dtmFechaTarea = New DevExpress.XtraEditors.DateEdit()
        Me.GroupControl4 = New DevExpress.XtraEditors.GroupControl()
        Me.dtmHoraFhh = New System.Windows.Forms.DateTimePicker()
        Me.dtmHoraIhh = New System.Windows.Forms.DateTimePicker()
        Me.GroupControl9 = New DevExpress.XtraEditors.GroupControl()
        Me.dtmHoraF = New System.Windows.Forms.DateTimePicker()
        Me.dtmHoraI = New System.Windows.Forms.DateTimePicker()
        Me.tbPedido = New DevExpress.XtraTab.XtraTabPage()
        Me.grpPedido = New DevExpress.XtraEditors.GroupControl()
        Me.grdPedido = New System.Windows.Forms.DataGridView()
        Me.Pedido = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Referencia = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Bodega = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cliente = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Propietario = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FechaPedido = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tbDetalleProducto = New DevExpress.XtraTab.XtraTabPage()
        Me.grdProducto = New System.Windows.Forms.DataGridView()
        Me.PedidoEnc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CodigoPedido = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ProductoDetalle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PresentacionDetalle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UMDetalle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EstadoDetalle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cantidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ClienteDias = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CantidadRecibida = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Operador = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.PedidoDet = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tbUbicPicking = New DevExpress.XtraTab.XtraTabPage()
        Me.panelUbic = New DevExpress.XtraEditors.PanelControl()
        Me.grdUbicPicking = New DevExpress.XtraGrid.GridControl()
        Me.DsOrdenCompraRecepcionOperador = New TOMWMS.DsOrdenCompraRecepcionOperador()
        Me.grdvPickingUbic = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tbPagina2 = New DevExpress.XtraTab.XtraTabPage()
        Me.GrpDetalle = New DevExpress.XtraEditors.GroupControl()
        Me.grdListaDespacho = New System.Windows.Forms.DataGridView()
        Me.IdPickingUbic = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Codigo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Producto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Presentacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Estado = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UnidadMedida = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Ubicacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IdDespachoDet = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CantidadPedido = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CantidadPicking = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CantidadVerificada = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Fecha = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IdProductoBodega = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IdProductoEstado = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IdPresentacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IdUnidadMedida = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IdPickingEnc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lnkAgregarPickingUbicacion = New System.Windows.Forms.LinkLabel()
        Me.XtraTabPage1 = New DevExpress.XtraTab.XtraTabPage()
        Me.dgridPacking = New DevExpress.XtraGrid.GridControl()
        Me.gvPacking = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.DataBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Fec_agrDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_agrTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_modDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_modTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.dkDespacho = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.PedidoEnc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CodigoPedido = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ProductoDetalle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PresentacionDetalle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UMDetalle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EstadoDetalle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cantidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ClienteDias = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CantidadRecibida = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Operador = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.PedidoDet = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTalla = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colColor = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.xtpDifPickVrsPack = New DevExpress.XtraTab.XtraTabPage()
        Label10 = New System.Windows.Forms.Label()
        Label12 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        IdPropietarioLabel = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        Label37 = New System.Windows.Forms.Label()
        Label38 = New System.Windows.Forms.Label()
        Label30 = New System.Windows.Forms.Label()
        Label7 = New System.Windows.Forms.Label()
        Fec_agrLabel = New System.Windows.Forms.Label()
        User_agrLabel = New System.Windows.Forms.Label()
        Fec_modLabel = New System.Windows.Forms.Label()
        User_modLabel = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        Label5 = New System.Windows.Forms.Label()
        Label6 = New System.Windows.Forms.Label()
        Label8 = New System.Windows.Forms.Label()
        Label9 = New System.Windows.Forms.Label()
        Label11 = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabControl1.SuspendLayout()
        Me.tbPagina1.SuspendLayout()
        CType(Me.GrpDato, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpDato.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.grpTransporte, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpTransporte.SuspendLayout()
        CType(Me.lcmbRuta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lcmbVehiculo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lcmbPiloto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpDatosAsociados, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpDatosAsociados.SuspendLayout()
        CType(Me.txtCantidadBultos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNumero, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoPase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpDatosGenerales, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpDatosGenerales.SuspendLayout()
        CType(Me.dtmFechaDespacho.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtmFechaDespacho.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpDatosFecha, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpDatosFecha.SuspendLayout()
        CType(Me.dtmFechaTarea.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtmFechaTarea.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl4.SuspendLayout()
        CType(Me.GroupControl9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl9.SuspendLayout()
        Me.tbPedido.SuspendLayout()
        CType(Me.grpPedido, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpPedido.SuspendLayout()
        CType(Me.grdPedido, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbDetalleProducto.SuspendLayout()
        CType(Me.grdProducto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbUbicPicking.SuspendLayout()
        CType(Me.panelUbic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelUbic.SuspendLayout()
        CType(Me.grdUbicPicking, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsOrdenCompraRecepcionOperador, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdvPickingUbic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbPagina2.SuspendLayout()
        CType(Me.GrpDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpDetalle.SuspendLayout()
        CType(Me.grdListaDespacho, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabPage1.SuspendLayout()
        CType(Me.dgridPacking, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvPacking, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dkDespacho, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerBottom.SuspendLayout()
        Me.DockPanel1.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label10
        '
        Label10.AutoSize = True
        Label10.Location = New System.Drawing.Point(13, 82)
        Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label10.Name = "Label10"
        Label10.Size = New System.Drawing.Size(45, 16)
        Label10.TabIndex = 2
        Label10.Text = "Estado"
        '
        'Label12
        '
        Label12.AutoSize = True
        Label12.Location = New System.Drawing.Point(13, 49)
        Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label12.Name = "Label12"
        Label12.Size = New System.Drawing.Size(46, 16)
        Label12.TabIndex = 0
        Label12.Text = "Código"
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(13, 114)
        Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(100, 16)
        Label2.TabIndex = 4
        Label2.Text = "Fecha Despacho"
        '
        'IdPropietarioLabel
        '
        IdPropietarioLabel.AutoSize = True
        IdPropietarioLabel.Location = New System.Drawing.Point(13, 146)
        IdPropietarioLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        IdPropietarioLabel.Name = "IdPropietarioLabel"
        IdPropietarioLabel.Size = New System.Drawing.Size(54, 16)
        IdPropietarioLabel.TabIndex = 6
        IdPropietarioLabel.Text = "Bodega:"
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(13, 180)
        Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(74, 16)
        Label1.TabIndex = 8
        Label1.Text = "Propietario:"
        '
        'Label37
        '
        Label37.AutoSize = True
        Label37.Location = New System.Drawing.Point(13, 82)
        Label37.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label37.Name = "Label37"
        Label37.Size = New System.Drawing.Size(60, 16)
        Label37.TabIndex = 2
        Label37.Text = "Hora Fin:"
        '
        'Label38
        '
        Label38.AutoSize = True
        Label38.Location = New System.Drawing.Point(10, 48)
        Label38.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label38.Name = "Label38"
        Label38.Size = New System.Drawing.Size(73, 16)
        Label38.TabIndex = 0
        Label38.Text = "Hora Inicio:"
        '
        'Label30
        '
        Label30.AutoSize = True
        Label30.Location = New System.Drawing.Point(10, 82)
        Label30.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label30.Name = "Label30"
        Label30.Size = New System.Drawing.Size(60, 16)
        Label30.TabIndex = 2
        Label30.Text = "Hora Fin:"
        '
        'Label7
        '
        Label7.AutoSize = True
        Label7.Location = New System.Drawing.Point(10, 48)
        Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label7.Name = "Label7"
        Label7.Size = New System.Drawing.Size(73, 16)
        Label7.TabIndex = 0
        Label7.Text = "Hora Inicio:"
        '
        'Fec_agrLabel
        '
        Fec_agrLabel.AutoSize = True
        Fec_agrLabel.Location = New System.Drawing.Point(47, 43)
        Fec_agrLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Fec_agrLabel.Name = "Fec_agrLabel"
        Fec_agrLabel.Size = New System.Drawing.Size(91, 16)
        Fec_agrLabel.TabIndex = 4
        Fec_agrLabel.Text = "Fecha Agregó:"
        '
        'User_agrLabel
        '
        User_agrLabel.AutoSize = True
        User_agrLabel.Location = New System.Drawing.Point(47, 11)
        User_agrLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        User_agrLabel.Name = "User_agrLabel"
        User_agrLabel.Size = New System.Drawing.Size(100, 16)
        User_agrLabel.TabIndex = 0
        User_agrLabel.Text = "Usuario Agregó:"
        '
        'Fec_modLabel
        '
        Fec_modLabel.AutoSize = True
        Fec_modLabel.Location = New System.Drawing.Point(531, 43)
        Fec_modLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Fec_modLabel.Name = "Fec_modLabel"
        Fec_modLabel.Size = New System.Drawing.Size(97, 16)
        Fec_modLabel.TabIndex = 6
        Fec_modLabel.Text = "Fecha Modificó:"
        '
        'User_modLabel
        '
        User_modLabel.AutoSize = True
        User_modLabel.Location = New System.Drawing.Point(531, 11)
        User_modLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        User_modLabel.Name = "User_modLabel"
        User_modLabel.Size = New System.Drawing.Size(106, 16)
        User_modLabel.TabIndex = 3
        User_modLabel.Text = "Usuario Modificó:"
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.Location = New System.Drawing.Point(13, 214)
        Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(46, 16)
        Label3.TabIndex = 10
        Label3.Text = "Activo:"
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(4, 47)
        Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(62, 16)
        Label4.TabIndex = 0
        Label4.Text = "No. Pase:"
        '
        'Label5
        '
        Label5.AutoSize = True
        Label5.Location = New System.Drawing.Point(4, 85)
        Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(86, 16)
        Label5.TabIndex = 2
        Label5.Text = "Número/RTN:"
        '
        'Label6
        '
        Label6.AutoSize = True
        Label6.Location = New System.Drawing.Point(4, 118)
        Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(80, 16)
        Label6.TabIndex = 4
        Label6.Text = "Cant. Bultos:"
        '
        'Label8
        '
        Label8.AutoSize = True
        Label8.Location = New System.Drawing.Point(4, 153)
        Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label8.Name = "Label8"
        Label8.Size = New System.Drawing.Size(72, 16)
        Label8.TabIndex = 6
        Label8.Text = "Marchamo:"
        '
        'Label9
        '
        Label9.AutoSize = True
        Label9.Location = New System.Drawing.Point(4, 217)
        Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label9.Name = "Label9"
        Label9.Size = New System.Drawing.Size(82, 16)
        Label9.TabIndex = 8
        Label9.Text = "Observación:"
        '
        'Label11
        '
        Label11.AutoSize = True
        Label11.Location = New System.Drawing.Point(4, 182)
        Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label11.Name = "Label11"
        Label11.Size = New System.Drawing.Size(123, 16)
        Label11.TabIndex = 10
        Label11.Text = "Documento externo:"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuGuardar, Me.mnuActualizar, Me.mnuAnular, Me.cmdImprimir, Me.cmdListaUbicacion, Me.btwnegativeStock, Me.lnkPedido, Me.mnuImprimir, Me.mnuImprimirDespacho, Me.mnuImprimirListaEmpaque, Me.mnuGuardarDatosCabecera, Me.mnuRepetirEncabezadoEnCadaPagina})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(4)
        Me.RibbonControl.MaxItemId = 16
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1291, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'mnuGuardar
        '
        Me.mnuGuardar.Caption = "Guardar"
        Me.mnuGuardar.Id = 1
        Me.mnuGuardar.ImageOptions.SvgImage = CType(resources.GetObject("mnuGuardar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuGuardar.Name = "mnuGuardar"
        '
        'mnuActualizar
        '
        Me.mnuActualizar.Caption = "Actualizar"
        Me.mnuActualizar.Id = 2
        Me.mnuActualizar.ImageOptions.SvgImage = CType(resources.GetObject("mnuActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuActualizar.Name = "mnuActualizar"
        '
        'mnuAnular
        '
        Me.mnuAnular.Caption = "Anular"
        Me.mnuAnular.Id = 3
        Me.mnuAnular.ImageOptions.SvgImage = CType(resources.GetObject("mnuAnular.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuAnular.Name = "mnuAnular"
        '
        'cmdImprimir
        '
        Me.cmdImprimir.Caption = "Imprimir"
        Me.cmdImprimir.Id = 4
        Me.cmdImprimir.ImageOptions.Image = CType(resources.GetObject("cmdImprimir.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdImprimir.ImageOptions.LargeImage = CType(resources.GetObject("cmdImprimir.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.cmdImprimir.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.cmdListaUbicacion)})
        Me.cmdImprimir.Name = "cmdImprimir"
        '
        'cmdListaUbicacion
        '
        Me.cmdListaUbicacion.Caption = "Lista de Ubicaciones"
        Me.cmdListaUbicacion.Id = 5
        Me.cmdListaUbicacion.Name = "cmdListaUbicacion"
        '
        'btwnegativeStock
        '
        Me.btwnegativeStock.Caption = "AllowNegativeStockException"
        Me.btwnegativeStock.Id = 7
        Me.btwnegativeStock.Name = "btwnegativeStock"
        Me.btwnegativeStock.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'lnkPedido
        '
        Me.lnkPedido.Caption = "Agregar Pedido"
        Me.lnkPedido.Id = 8
        Me.lnkPedido.ImageOptions.SvgImage = CType(resources.GetObject("lnkPedido.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.lnkPedido.Name = "lnkPedido"
        '
        'mnuImprimir
        '
        Me.mnuImprimir.Caption = "Imprimir"
        Me.mnuImprimir.Id = 10
        Me.mnuImprimir.ImageOptions.SvgImage = CType(resources.GetObject("mnuImprimir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuImprimir.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuImprimirDespacho), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuImprimirListaEmpaque)})
        Me.mnuImprimir.Name = "mnuImprimir"
        '
        'mnuImprimirDespacho
        '
        Me.mnuImprimirDespacho.Caption = "Despacho"
        Me.mnuImprimirDespacho.Id = 11
        Me.mnuImprimirDespacho.Name = "mnuImprimirDespacho"
        '
        'mnuImprimirListaEmpaque
        '
        Me.mnuImprimirListaEmpaque.Caption = "Lista de empaque"
        Me.mnuImprimirListaEmpaque.Id = 12
        Me.mnuImprimirListaEmpaque.Name = "mnuImprimirListaEmpaque"
        '
        'mnuGuardarDatosCabecera
        '
        Me.mnuGuardarDatosCabecera.Caption = "Actualizar datos de cabecera"
        Me.mnuGuardarDatosCabecera.Id = 13
        Me.mnuGuardarDatosCabecera.ImageOptions.SvgImage = CType(resources.GetObject("mnuGuardarDatosCabecera.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuGuardarDatosCabecera.Name = "mnuGuardarDatosCabecera"
        '
        'mnuRepetirEncabezadoEnCadaPagina
        '
        Me.mnuRepetirEncabezadoEnCadaPagina.BindableChecked = True
        Me.mnuRepetirEncabezadoEnCadaPagina.Caption = "Encabezado en cada página"
        Me.mnuRepetirEncabezadoEnCadaPagina.Checked = True
        Me.mnuRepetirEncabezadoEnCadaPagina.Id = 15
        Me.mnuRepetirEncabezadoEnCadaPagina.Name = "mnuRepetirEncabezadoEnCadaPagina"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Despacho"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardarDatosCabecera)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuAnular)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.lnkPedido)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuImprimir)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.btwnegativeStock)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuRepetirEncabezadoEnCadaPagina)
        Me.RibbonPageGroup1.ItemsLayout = DevExpress.XtraBars.Ribbon.RibbonPageGroupItemsLayout.OneRow
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 756)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1291, 30)
        Me.RibbonStatusBar.Visible = False
        '
        'XtraTabControl1
        '
        Me.XtraTabControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.XtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XtraTabControl1.HeaderAutoFill = DevExpress.Utils.DefaultBoolean.[False]
        Me.XtraTabControl1.Location = New System.Drawing.Point(0, 193)
        Me.XtraTabControl1.Margin = New System.Windows.Forms.Padding(12)
        Me.XtraTabControl1.MaxTabPageWidth = 100
        Me.XtraTabControl1.Name = "XtraTabControl1"
        Me.XtraTabControl1.Padding = New System.Windows.Forms.Padding(12)
        Me.XtraTabControl1.SelectedTabPage = Me.tbPagina1
        Me.XtraTabControl1.Size = New System.Drawing.Size(1291, 537)
        Me.XtraTabControl1.TabIndex = 0
        Me.XtraTabControl1.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tbPagina1, Me.tbPedido, Me.tbDetalleProducto, Me.tbUbicPicking, Me.tbPagina2, Me.XtraTabPage1, Me.xtpDifPickVrsPack})
        '
        'tbPagina1
        '
        Me.tbPagina1.Appearance.PageClient.BackColor = System.Drawing.SystemColors.ControlDark
        Me.tbPagina1.Appearance.PageClient.Options.UseBackColor = True
        Me.tbPagina1.Controls.Add(Me.GrpDato)
        Me.tbPagina1.Margin = New System.Windows.Forms.Padding(4)
        Me.tbPagina1.Name = "tbPagina1"
        Me.tbPagina1.Size = New System.Drawing.Size(1040, 507)
        Me.tbPagina1.Text = "Datos Despacho"
        '
        'GrpDato
        '
        Me.GrpDato.Controls.Add(Me.TableLayoutPanel1)
        Me.GrpDato.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpDato.Location = New System.Drawing.Point(0, 0)
        Me.GrpDato.Margin = New System.Windows.Forms.Padding(4)
        Me.GrpDato.Name = "GrpDato"
        Me.GrpDato.Size = New System.Drawing.Size(1040, 507)
        Me.GrpDato.TabIndex = 0
        Me.GrpDato.Text = "Datos"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.grpTransporte, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.grpDatosAsociados, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.grpDatosGenerales, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.grpDatosFecha, 1, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(2, 28)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55.97484!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 44.02516!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1036, 477)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'grpTransporte
        '
        Me.grpTransporte.Controls.Add(Me.lcmbRuta)
        Me.grpTransporte.Controls.Add(Me.lcmbVehiculo)
        Me.grpTransporte.Controls.Add(Me.lcmbPiloto)
        Me.grpTransporte.Controls.Add(Me.lnkRuta)
        Me.grpTransporte.Controls.Add(Me.lnkVehiculo)
        Me.grpTransporte.Controls.Add(Me.lnkPiloto)
        Me.grpTransporte.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpTransporte.Location = New System.Drawing.Point(4, 270)
        Me.grpTransporte.Margin = New System.Windows.Forms.Padding(4)
        Me.grpTransporte.Name = "grpTransporte"
        Me.grpTransporte.Size = New System.Drawing.Size(510, 203)
        Me.grpTransporte.TabIndex = 1
        Me.grpTransporte.Text = "Información de Transporte"
        '
        'lcmbRuta
        '
        Me.lcmbRuta.Location = New System.Drawing.Point(134, 116)
        Me.lcmbRuta.Margin = New System.Windows.Forms.Padding(4)
        Me.lcmbRuta.Name = "lcmbRuta"
        Me.lcmbRuta.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.lcmbRuta.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lcmbRuta.Size = New System.Drawing.Size(219, 22)
        Me.lcmbRuta.TabIndex = 5
        '
        'lcmbVehiculo
        '
        Me.lcmbVehiculo.Location = New System.Drawing.Point(134, 86)
        Me.lcmbVehiculo.Margin = New System.Windows.Forms.Padding(4)
        Me.lcmbVehiculo.Name = "lcmbVehiculo"
        Me.lcmbVehiculo.Properties.AcceptEditorTextAsNewValue = DevExpress.Utils.DefaultBoolean.[True]
        Me.lcmbVehiculo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.lcmbVehiculo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lcmbVehiculo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.lcmbVehiculo.Size = New System.Drawing.Size(219, 22)
        Me.lcmbVehiculo.TabIndex = 3
        '
        'lcmbPiloto
        '
        Me.lcmbPiloto.Location = New System.Drawing.Point(134, 57)
        Me.lcmbPiloto.Margin = New System.Windows.Forms.Padding(4)
        Me.lcmbPiloto.Name = "lcmbPiloto"
        Me.lcmbPiloto.Properties.AcceptEditorTextAsNewValue = DevExpress.Utils.DefaultBoolean.[True]
        Me.lcmbPiloto.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.lcmbPiloto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lcmbPiloto.Properties.PopupFormMinSize = New System.Drawing.Size(199, 199)
        Me.lcmbPiloto.Properties.PopupWidth = 199
        Me.lcmbPiloto.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.lcmbPiloto.Size = New System.Drawing.Size(219, 22)
        Me.lcmbPiloto.TabIndex = 1
        '
        'lnkRuta
        '
        Me.lnkRuta.AutoSize = True
        Me.lnkRuta.Location = New System.Drawing.Point(23, 119)
        Me.lnkRuta.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lnkRuta.Name = "lnkRuta"
        Me.lnkRuta.Size = New System.Drawing.Size(33, 16)
        Me.lnkRuta.TabIndex = 4
        Me.lnkRuta.TabStop = True
        Me.lnkRuta.Text = "Ruta"
        '
        'lnkVehiculo
        '
        Me.lnkVehiculo.AutoSize = True
        Me.lnkVehiculo.Location = New System.Drawing.Point(23, 89)
        Me.lnkVehiculo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lnkVehiculo.Name = "lnkVehiculo"
        Me.lnkVehiculo.Size = New System.Drawing.Size(55, 16)
        Me.lnkVehiculo.TabIndex = 2
        Me.lnkVehiculo.TabStop = True
        Me.lnkVehiculo.Text = "Vehículo"
        '
        'lnkPiloto
        '
        Me.lnkPiloto.AutoSize = True
        Me.lnkPiloto.Location = New System.Drawing.Point(23, 59)
        Me.lnkPiloto.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lnkPiloto.Name = "lnkPiloto"
        Me.lnkPiloto.Size = New System.Drawing.Size(38, 16)
        Me.lnkPiloto.TabIndex = 0
        Me.lnkPiloto.TabStop = True
        Me.lnkPiloto.Text = "Piloto"
        '
        'grpDatosAsociados
        '
        Me.grpDatosAsociados.Controls.Add(Label11)
        Me.grpDatosAsociados.Controls.Add(Me.txtDocumentoExterno)
        Me.grpDatosAsociados.Controls.Add(Me.txtCantidadBultos)
        Me.grpDatosAsociados.Controls.Add(Label9)
        Me.grpDatosAsociados.Controls.Add(Me.txtObservacion)
        Me.grpDatosAsociados.Controls.Add(Label8)
        Me.grpDatosAsociados.Controls.Add(Me.txtMarchamo)
        Me.grpDatosAsociados.Controls.Add(Label6)
        Me.grpDatosAsociados.Controls.Add(Label5)
        Me.grpDatosAsociados.Controls.Add(Me.txtNumero)
        Me.grpDatosAsociados.Controls.Add(Me.txtNoPase)
        Me.grpDatosAsociados.Controls.Add(Label4)
        Me.grpDatosAsociados.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpDatosAsociados.Location = New System.Drawing.Point(522, 2)
        Me.grpDatosAsociados.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.grpDatosAsociados.Name = "grpDatosAsociados"
        Me.grpDatosAsociados.Size = New System.Drawing.Size(510, 262)
        Me.grpDatosAsociados.TabIndex = 2
        Me.grpDatosAsociados.Text = "Datos asociados"
        '
        'txtDocumentoExterno
        '
        Me.txtDocumentoExterno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDocumentoExterno.Location = New System.Drawing.Point(134, 178)
        Me.txtDocumentoExterno.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDocumentoExterno.Name = "txtDocumentoExterno"
        Me.txtDocumentoExterno.ReadOnly = True
        Me.txtDocumentoExterno.Size = New System.Drawing.Size(246, 23)
        Me.txtDocumentoExterno.TabIndex = 11
        '
        'txtCantidadBultos
        '
        Me.txtCantidadBultos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCantidadBultos.DecimalPlaces = 6
        Me.txtCantidadBultos.Location = New System.Drawing.Point(134, 114)
        Me.txtCantidadBultos.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCantidadBultos.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtCantidadBultos.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtCantidadBultos.Name = "txtCantidadBultos"
        Me.txtCantidadBultos.Size = New System.Drawing.Size(247, 23)
        Me.txtCantidadBultos.TabIndex = 5
        '
        'txtObservacion
        '
        Me.txtObservacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtObservacion.Location = New System.Drawing.Point(134, 207)
        Me.txtObservacion.Margin = New System.Windows.Forms.Padding(4)
        Me.txtObservacion.Multiline = True
        Me.txtObservacion.Name = "txtObservacion"
        Me.txtObservacion.Size = New System.Drawing.Size(246, 52)
        Me.txtObservacion.TabIndex = 9
        '
        'txtMarchamo
        '
        Me.txtMarchamo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMarchamo.Location = New System.Drawing.Point(134, 149)
        Me.txtMarchamo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtMarchamo.Name = "txtMarchamo"
        Me.txtMarchamo.Size = New System.Drawing.Size(246, 23)
        Me.txtMarchamo.TabIndex = 7
        '
        'txtNumero
        '
        Me.txtNumero.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNumero.Location = New System.Drawing.Point(134, 82)
        Me.txtNumero.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNumero.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtNumero.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtNumero.Name = "txtNumero"
        Me.txtNumero.Size = New System.Drawing.Size(247, 23)
        Me.txtNumero.TabIndex = 3
        '
        'txtNoPase
        '
        Me.txtNoPase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNoPase.Location = New System.Drawing.Point(134, 46)
        Me.txtNoPase.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNoPase.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtNoPase.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtNoPase.Name = "txtNoPase"
        Me.txtNoPase.Size = New System.Drawing.Size(247, 23)
        Me.txtNoPase.TabIndex = 1
        '
        'grpDatosGenerales
        '
        Me.grpDatosGenerales.Controls.Add(Me.lblC)
        Me.grpDatosGenerales.Controls.Add(Me.dtmFechaDespacho)
        Me.grpDatosGenerales.Controls.Add(Me.cmbPropietario)
        Me.grpDatosGenerales.Controls.Add(Me.cmbBodega)
        Me.grpDatosGenerales.Controls.Add(Label3)
        Me.grpDatosGenerales.Controls.Add(Me.chkActivo)
        Me.grpDatosGenerales.Controls.Add(Me.lblEstado)
        Me.grpDatosGenerales.Controls.Add(Label10)
        Me.grpDatosGenerales.Controls.Add(Label12)
        Me.grpDatosGenerales.Controls.Add(Label2)
        Me.grpDatosGenerales.Controls.Add(IdPropietarioLabel)
        Me.grpDatosGenerales.Controls.Add(Label1)
        Me.grpDatosGenerales.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpDatosGenerales.Location = New System.Drawing.Point(4, 2)
        Me.grpDatosGenerales.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.grpDatosGenerales.Name = "grpDatosGenerales"
        Me.grpDatosGenerales.Size = New System.Drawing.Size(510, 262)
        Me.grpDatosGenerales.TabIndex = 0
        Me.grpDatosGenerales.Text = "Generales"
        '
        'lblC
        '
        Me.lblC.BackColor = System.Drawing.Color.Lavender
        Me.lblC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblC.Location = New System.Drawing.Point(134, 46)
        Me.lblC.Margin = New System.Windows.Forms.Padding(4)
        Me.lblC.Name = "lblC"
        Me.lblC.ReadOnly = True
        Me.lblC.Size = New System.Drawing.Size(346, 23)
        Me.lblC.TabIndex = 1
        '
        'dtmFechaDespacho
        '
        Me.dtmFechaDespacho.EditValue = New Date(2017, 11, 23, 10, 9, 4, 0)
        Me.dtmFechaDespacho.Location = New System.Drawing.Point(134, 112)
        Me.dtmFechaDespacho.Margin = New System.Windows.Forms.Padding(4)
        Me.dtmFechaDespacho.MenuManager = Me.RibbonControl
        Me.dtmFechaDespacho.Name = "dtmFechaDespacho"
        Me.dtmFechaDespacho.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.dtmFechaDespacho.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtmFechaDespacho.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtmFechaDespacho.Size = New System.Drawing.Size(346, 22)
        Me.dtmFechaDespacho.TabIndex = 5
        '
        'cmbPropietario
        '
        Me.cmbPropietario.Location = New System.Drawing.Point(134, 178)
        Me.cmbPropietario.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbPropietario.MenuManager = Me.RibbonControl
        Me.cmbPropietario.Name = "cmbPropietario"
        Me.cmbPropietario.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbPropietario.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPropietario.Properties.NullText = ""
        Me.cmbPropietario.Size = New System.Drawing.Size(346, 22)
        Me.cmbPropietario.TabIndex = 9
        '
        'cmbBodega
        '
        Me.cmbBodega.Location = New System.Drawing.Point(134, 144)
        Me.cmbBodega.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbBodega.MenuManager = Me.RibbonControl
        Me.cmbBodega.Name = "cmbBodega"
        Me.cmbBodega.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbBodega.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodega.Properties.NullText = ""
        Me.cmbBodega.Size = New System.Drawing.Size(346, 22)
        Me.cmbBodega.TabIndex = 7
        '
        'chkActivo
        '
        Me.chkActivo.EditValue = True
        Me.chkActivo.Location = New System.Drawing.Point(134, 210)
        Me.chkActivo.Margin = New System.Windows.Forms.Padding(4)
        Me.chkActivo.MenuManager = Me.RibbonControl
        Me.chkActivo.Name = "chkActivo"
        Me.chkActivo.Properties.Caption = ""
        Me.chkActivo.Size = New System.Drawing.Size(41, 24)
        Me.chkActivo.TabIndex = 11
        '
        'lblEstado
        '
        Me.lblEstado.AutoSize = True
        Me.lblEstado.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEstado.Location = New System.Drawing.Point(136, 82)
        Me.lblEstado.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblEstado.Name = "lblEstado"
        Me.lblEstado.Size = New System.Drawing.Size(59, 17)
        Me.lblEstado.TabIndex = 3
        Me.lblEstado.Text = "NUEVO"
        '
        'grpDatosFecha
        '
        Me.grpDatosFecha.Controls.Add(Me.dtmFechaTarea)
        Me.grpDatosFecha.Controls.Add(Me.GroupControl4)
        Me.grpDatosFecha.Controls.Add(Me.GroupControl9)
        Me.grpDatosFecha.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpDatosFecha.Location = New System.Drawing.Point(522, 270)
        Me.grpDatosFecha.Margin = New System.Windows.Forms.Padding(4)
        Me.grpDatosFecha.Name = "grpDatosFecha"
        Me.grpDatosFecha.Size = New System.Drawing.Size(510, 203)
        Me.grpDatosFecha.TabIndex = 3
        Me.grpDatosFecha.Text = "Fecha Documento"
        '
        'dtmFechaTarea
        '
        Me.dtmFechaTarea.EditValue = New Date(2017, 11, 20, 10, 8, 10, 708)
        Me.dtmFechaTarea.Location = New System.Drawing.Point(141, 34)
        Me.dtmFechaTarea.Margin = New System.Windows.Forms.Padding(4)
        Me.dtmFechaTarea.MenuManager = Me.RibbonControl
        Me.dtmFechaTarea.Name = "dtmFechaTarea"
        Me.dtmFechaTarea.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtmFechaTarea.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtmFechaTarea.Size = New System.Drawing.Size(204, 22)
        Me.dtmFechaTarea.TabIndex = 0
        '
        'GroupControl4
        '
        Me.GroupControl4.Controls.Add(Label37)
        Me.GroupControl4.Controls.Add(Me.dtmHoraFhh)
        Me.GroupControl4.Controls.Add(Label38)
        Me.GroupControl4.Controls.Add(Me.dtmHoraIhh)
        Me.GroupControl4.Enabled = False
        Me.GroupControl4.Location = New System.Drawing.Point(247, 71)
        Me.GroupControl4.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl4.Name = "GroupControl4"
        Me.GroupControl4.Size = New System.Drawing.Size(234, 127)
        Me.GroupControl4.TabIndex = 2
        Me.GroupControl4.Text = "Hora HandHeld"
        '
        'dtmHoraFhh
        '
        Me.dtmHoraFhh.CustomFormat = "hh:mm:ss"
        Me.dtmHoraFhh.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtmHoraFhh.Location = New System.Drawing.Point(91, 78)
        Me.dtmHoraFhh.Margin = New System.Windows.Forms.Padding(4)
        Me.dtmHoraFhh.Name = "dtmHoraFhh"
        Me.dtmHoraFhh.ShowUpDown = True
        Me.dtmHoraFhh.Size = New System.Drawing.Size(131, 23)
        Me.dtmHoraFhh.TabIndex = 3
        '
        'dtmHoraIhh
        '
        Me.dtmHoraIhh.CustomFormat = "hh:mm:ss"
        Me.dtmHoraIhh.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtmHoraIhh.Location = New System.Drawing.Point(91, 44)
        Me.dtmHoraIhh.Margin = New System.Windows.Forms.Padding(4)
        Me.dtmHoraIhh.Name = "dtmHoraIhh"
        Me.dtmHoraIhh.ShowUpDown = True
        Me.dtmHoraIhh.Size = New System.Drawing.Size(131, 23)
        Me.dtmHoraIhh.TabIndex = 1
        '
        'GroupControl9
        '
        Me.GroupControl9.Controls.Add(Label30)
        Me.GroupControl9.Controls.Add(Me.dtmHoraF)
        Me.GroupControl9.Controls.Add(Label7)
        Me.GroupControl9.Controls.Add(Me.dtmHoraI)
        Me.GroupControl9.Location = New System.Drawing.Point(7, 71)
        Me.GroupControl9.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl9.Name = "GroupControl9"
        Me.GroupControl9.Size = New System.Drawing.Size(233, 127)
        Me.GroupControl9.TabIndex = 1
        Me.GroupControl9.Text = "Hora Teórica"
        '
        'dtmHoraF
        '
        Me.dtmHoraF.CustomFormat = "hh:mm:ss"
        Me.dtmHoraF.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtmHoraF.Location = New System.Drawing.Point(90, 78)
        Me.dtmHoraF.Margin = New System.Windows.Forms.Padding(4)
        Me.dtmHoraF.Name = "dtmHoraF"
        Me.dtmHoraF.ShowUpDown = True
        Me.dtmHoraF.Size = New System.Drawing.Size(131, 23)
        Me.dtmHoraF.TabIndex = 3
        '
        'dtmHoraI
        '
        Me.dtmHoraI.CustomFormat = "hh:mm:ss"
        Me.dtmHoraI.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtmHoraI.Location = New System.Drawing.Point(90, 44)
        Me.dtmHoraI.Margin = New System.Windows.Forms.Padding(4)
        Me.dtmHoraI.Name = "dtmHoraI"
        Me.dtmHoraI.ShowUpDown = True
        Me.dtmHoraI.Size = New System.Drawing.Size(131, 23)
        Me.dtmHoraI.TabIndex = 1
        '
        'tbPedido
        '
        Me.tbPedido.Controls.Add(Me.grpPedido)
        Me.tbPedido.Margin = New System.Windows.Forms.Padding(4)
        Me.tbPedido.Name = "tbPedido"
        Me.tbPedido.Size = New System.Drawing.Size(1040, 507)
        Me.tbPedido.Text = "Pedido"
        '
        'grpPedido
        '
        Me.grpPedido.Controls.Add(Me.grdPedido)
        Me.grpPedido.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpPedido.Location = New System.Drawing.Point(0, 0)
        Me.grpPedido.Margin = New System.Windows.Forms.Padding(4)
        Me.grpPedido.Name = "grpPedido"
        Me.grpPedido.Size = New System.Drawing.Size(1040, 507)
        Me.grpPedido.TabIndex = 0
        Me.grpPedido.Text = "Pedido"
        '
        'grdPedido
        '
        Me.grdPedido.AllowUserToResizeRows = False
        Me.grdPedido.BackgroundColor = System.Drawing.Color.AliceBlue
        Me.grdPedido.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 7.8!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdPedido.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdPedido.ColumnHeadersHeight = 40
        Me.grdPedido.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Pedido, Me.Referencia, Me.Bodega, Me.Cliente, Me.Propietario, Me.FechaPedido})
        Me.grdPedido.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdPedido.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.grdPedido.EnableHeadersVisualStyles = False
        Me.grdPedido.GridColor = System.Drawing.Color.Navy
        Me.grdPedido.Location = New System.Drawing.Point(2, 28)
        Me.grdPedido.Margin = New System.Windows.Forms.Padding(4)
        Me.grdPedido.MultiSelect = False
        Me.grdPedido.Name = "grdPedido"
        Me.grdPedido.ReadOnly = True
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Tahoma", 7.8!)
        DataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdPedido.RowHeadersDefaultCellStyle = DataGridViewCellStyle12
        Me.grdPedido.RowHeadersVisible = False
        Me.grdPedido.RowHeadersWidth = 40
        Me.grdPedido.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdPedido.Size = New System.Drawing.Size(1036, 477)
        Me.grdPedido.TabIndex = 1
        '
        'Pedido
        '
        Me.Pedido.HeaderText = "Pedido"
        Me.Pedido.MinimumWidth = 6
        Me.Pedido.Name = "Pedido"
        Me.Pedido.ReadOnly = True
        Me.Pedido.Width = 125
        '
        'Referencia
        '
        Me.Referencia.HeaderText = "Referencia"
        Me.Referencia.MinimumWidth = 6
        Me.Referencia.Name = "Referencia"
        Me.Referencia.ReadOnly = True
        Me.Referencia.Width = 125
        '
        'Bodega
        '
        Me.Bodega.HeaderText = "Bodega"
        Me.Bodega.MinimumWidth = 6
        Me.Bodega.Name = "Bodega"
        Me.Bodega.ReadOnly = True
        Me.Bodega.Width = 125
        '
        'Cliente
        '
        Me.Cliente.HeaderText = "Cliente"
        Me.Cliente.MinimumWidth = 6
        Me.Cliente.Name = "Cliente"
        Me.Cliente.ReadOnly = True
        Me.Cliente.Width = 125
        '
        'Propietario
        '
        Me.Propietario.HeaderText = "Propietario"
        Me.Propietario.MinimumWidth = 6
        Me.Propietario.Name = "Propietario"
        Me.Propietario.ReadOnly = True
        Me.Propietario.Width = 125
        '
        'FechaPedido
        '
        Me.FechaPedido.HeaderText = "Fecha Pedido"
        Me.FechaPedido.MinimumWidth = 6
        Me.FechaPedido.Name = "FechaPedido"
        Me.FechaPedido.ReadOnly = True
        Me.FechaPedido.Width = 125
        '
        'tbDetalleProducto
        '
        Me.tbDetalleProducto.Controls.Add(Me.grdProducto)
        Me.tbDetalleProducto.Margin = New System.Windows.Forms.Padding(4)
        Me.tbDetalleProducto.Name = "tbDetalleProducto"
        Me.tbDetalleProducto.Size = New System.Drawing.Size(1289, 507)
        Me.tbDetalleProducto.Text = "Detalle Producto"
        '
        'grdProducto
        '
        Me.grdProducto.AllowUserToResizeRows = False
        Me.grdProducto.BackgroundColor = System.Drawing.Color.AliceBlue
        Me.grdProducto.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.AliceBlue
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 7.8!)
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdProducto.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.grdProducto.ColumnHeadersHeight = 40
        Me.grdProducto.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.PedidoEnc, Me.CodigoPedido, Me.ProductoDetalle, Me.PresentacionDetalle, Me.UMDetalle, Me.EstadoDetalle, Me.Cantidad, Me.ClienteDias, Me.CantidadRecibida, Me.Operador, Me.PedidoDet, Me.colTalla, Me.colColor})
        Me.grdProducto.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdProducto.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.grdProducto.EnableHeadersVisualStyles = False
        Me.grdProducto.GridColor = System.Drawing.Color.Navy
        Me.grdProducto.Location = New System.Drawing.Point(0, 0)
        Me.grdProducto.Margin = New System.Windows.Forms.Padding(4)
        Me.grdProducto.MultiSelect = False
        Me.grdProducto.Name = "grdProducto"
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.Chocolate
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Tahoma", 7.8!)
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdProducto.RowHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.grdProducto.RowHeadersVisible = False
        Me.grdProducto.RowHeadersWidth = 40
        Me.grdProducto.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdProducto.Size = New System.Drawing.Size(1289, 507)
        Me.grdProducto.TabIndex = 1
        '
        'PedidoEnc
        '
        DataGridViewCellStyle14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle14.Format = "N0"
        Me.PedidoEnc.DefaultCellStyle = DataGridViewCellStyle14
        Me.PedidoEnc.HeaderText = "Pedido"
        Me.PedidoEnc.MinimumWidth = 6
        Me.PedidoEnc.Name = "PedidoEnc"
        Me.PedidoEnc.ReadOnly = True
        Me.PedidoEnc.Width = 125
        '
        'CodigoPedido
        '
        Me.CodigoPedido.HeaderText = "Código"
        Me.CodigoPedido.MinimumWidth = 6
        Me.CodigoPedido.Name = "CodigoPedido"
        Me.CodigoPedido.ReadOnly = True
        Me.CodigoPedido.Width = 125
        '
        'ProductoDetalle
        '
        Me.ProductoDetalle.HeaderText = "Producto"
        Me.ProductoDetalle.MinimumWidth = 6
        Me.ProductoDetalle.Name = "ProductoDetalle"
        Me.ProductoDetalle.ReadOnly = True
        Me.ProductoDetalle.Width = 125
        '
        'PresentacionDetalle
        '
        Me.PresentacionDetalle.HeaderText = "Presentación"
        Me.PresentacionDetalle.MinimumWidth = 6
        Me.PresentacionDetalle.Name = "PresentacionDetalle"
        Me.PresentacionDetalle.ReadOnly = True
        Me.PresentacionDetalle.Width = 125
        '
        'UMDetalle
        '
        Me.UMDetalle.HeaderText = "Unidad Medida"
        Me.UMDetalle.MinimumWidth = 6
        Me.UMDetalle.Name = "UMDetalle"
        Me.UMDetalle.ReadOnly = True
        Me.UMDetalle.Width = 125
        '
        'EstadoDetalle
        '
        Me.EstadoDetalle.HeaderText = "Estado"
        Me.EstadoDetalle.MinimumWidth = 6
        Me.EstadoDetalle.Name = "EstadoDetalle"
        Me.EstadoDetalle.ReadOnly = True
        Me.EstadoDetalle.Width = 125
        '
        'Cantidad
        '
        DataGridViewCellStyle15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle15.Format = "N2"
        Me.Cantidad.DefaultCellStyle = DataGridViewCellStyle15
        Me.Cantidad.HeaderText = "Cantidad"
        Me.Cantidad.MinimumWidth = 6
        Me.Cantidad.Name = "Cantidad"
        Me.Cantidad.ReadOnly = True
        Me.Cantidad.Width = 125
        '
        'ClienteDias
        '
        Me.ClienteDias.HeaderText = "Cliente Días"
        Me.ClienteDias.MinimumWidth = 6
        Me.ClienteDias.Name = "ClienteDias"
        Me.ClienteDias.ReadOnly = True
        Me.ClienteDias.Width = 125
        '
        'CantidadRecibida
        '
        DataGridViewCellStyle16.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle16.Format = "N2"
        DataGridViewCellStyle16.NullValue = "0.0"
        Me.CantidadRecibida.DefaultCellStyle = DataGridViewCellStyle16
        Me.CantidadRecibida.HeaderText = "Cantidad Recibida"
        Me.CantidadRecibida.MinimumWidth = 6
        Me.CantidadRecibida.Name = "CantidadRecibida"
        Me.CantidadRecibida.ReadOnly = True
        Me.CantidadRecibida.Visible = False
        Me.CantidadRecibida.Width = 125
        '
        'Operador
        '
        Me.Operador.HeaderText = "Operador"
        Me.Operador.MinimumWidth = 6
        Me.Operador.Name = "Operador"
        Me.Operador.Visible = False
        Me.Operador.Width = 125
        '
        'PedidoDet
        '
        Me.PedidoDet.HeaderText = "Pedido Det"
        Me.PedidoDet.MinimumWidth = 6
        Me.PedidoDet.Name = "PedidoDet"
        Me.PedidoDet.Visible = False
        Me.PedidoDet.Width = 125
        '
        'tbUbicPicking
        '
        Me.tbUbicPicking.Controls.Add(Me.panelUbic)
        Me.tbUbicPicking.Margin = New System.Windows.Forms.Padding(4)
        Me.tbUbicPicking.Name = "tbUbicPicking"
        Me.tbUbicPicking.Size = New System.Drawing.Size(1040, 507)
        Me.tbUbicPicking.Text = "Ubicación Picking"
        '
        'panelUbic
        '
        Me.panelUbic.Controls.Add(Me.grdUbicPicking)
        Me.panelUbic.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelUbic.Location = New System.Drawing.Point(0, 0)
        Me.panelUbic.Margin = New System.Windows.Forms.Padding(4)
        Me.panelUbic.Name = "panelUbic"
        Me.panelUbic.Size = New System.Drawing.Size(1040, 507)
        Me.panelUbic.TabIndex = 0
        '
        'grdUbicPicking
        '
        Me.grdUbicPicking.DataSource = Me.DsOrdenCompraRecepcionOperador
        Me.grdUbicPicking.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdUbicPicking.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.grdUbicPicking.Location = New System.Drawing.Point(2, 2)
        Me.grdUbicPicking.MainView = Me.grdvPickingUbic
        Me.grdUbicPicking.Margin = New System.Windows.Forms.Padding(4)
        Me.grdUbicPicking.MenuManager = Me.RibbonControl
        Me.grdUbicPicking.Name = "grdUbicPicking"
        Me.grdUbicPicking.Size = New System.Drawing.Size(1036, 503)
        Me.grdUbicPicking.TabIndex = 0
        Me.grdUbicPicking.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdvPickingUbic})
        '
        'DsOrdenCompraRecepcionOperador
        '
        Me.DsOrdenCompraRecepcionOperador.DataSetName = "DsOrdenCompraRecepcionOperador"
        Me.DsOrdenCompraRecepcionOperador.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'grdvPickingUbic
        '
        Me.grdvPickingUbic.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdvPickingUbic.Appearance.HeaderPanel.Options.UseFont = True
        Me.grdvPickingUbic.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdvPickingUbic.Appearance.Row.Options.UseFont = True
        Me.grdvPickingUbic.DetailHeight = 431
        Me.grdvPickingUbic.GridControl = Me.grdUbicPicking
        Me.grdvPickingUbic.Name = "grdvPickingUbic"
        Me.grdvPickingUbic.OptionsFind.AlwaysVisible = True
        Me.grdvPickingUbic.OptionsView.ColumnAutoWidth = False
        Me.grdvPickingUbic.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        '
        'tbPagina2
        '
        Me.tbPagina2.Controls.Add(Me.GrpDetalle)
        Me.tbPagina2.Margin = New System.Windows.Forms.Padding(4)
        Me.tbPagina2.Name = "tbPagina2"
        Me.tbPagina2.Padding = New System.Windows.Forms.Padding(12)
        Me.tbPagina2.PageVisible = False
        Me.tbPagina2.Size = New System.Drawing.Size(1040, 507)
        Me.tbPagina2.Text = "Detalle Picking"
        '
        'GrpDetalle
        '
        Me.GrpDetalle.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GrpDetalle.Controls.Add(Me.grdListaDespacho)
        Me.GrpDetalle.Controls.Add(Me.lnkAgregarPickingUbicacion)
        Me.GrpDetalle.Location = New System.Drawing.Point(2, 2)
        Me.GrpDetalle.Margin = New System.Windows.Forms.Padding(4)
        Me.GrpDetalle.Name = "GrpDetalle"
        Me.GrpDetalle.Size = New System.Drawing.Size(1356, 529)
        Me.GrpDetalle.TabIndex = 0
        Me.GrpDetalle.Text = "Lista"
        '
        'grdListaDespacho
        '
        Me.grdListaDespacho.AllowUserToResizeRows = False
        Me.grdListaDespacho.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdListaDespacho.BackgroundColor = System.Drawing.Color.AliceBlue
        Me.grdListaDespacho.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.AliceBlue
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Tahoma", 7.8!)
        DataGridViewCellStyle8.ForeColor = System.Drawing.Color.MidnightBlue
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.White
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdListaDespacho.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.grdListaDespacho.ColumnHeadersHeight = 40
        Me.grdListaDespacho.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IdPickingUbic, Me.Codigo, Me.Producto, Me.Presentacion, Me.Estado, Me.UnidadMedida, Me.Ubicacion, Me.IdDespachoDet, Me.CantidadPedido, Me.CantidadPicking, Me.CantidadVerificada, Me.Fecha, Me.IdProductoBodega, Me.IdProductoEstado, Me.IdPresentacion, Me.IdUnidadMedida, Me.IdPickingEnc})
        Me.grdListaDespacho.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.grdListaDespacho.EnableHeadersVisualStyles = False
        Me.grdListaDespacho.GridColor = System.Drawing.Color.Navy
        Me.grdListaDespacho.Location = New System.Drawing.Point(6, 68)
        Me.grdListaDespacho.Margin = New System.Windows.Forms.Padding(4)
        Me.grdListaDespacho.MultiSelect = False
        Me.grdListaDespacho.Name = "grdListaDespacho"
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.Chocolate
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Tahoma", 7.8!)
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdListaDespacho.RowHeadersDefaultCellStyle = DataGridViewCellStyle10
        DataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle20.BackColor = System.Drawing.Color.Chocolate
        DataGridViewCellStyle20.Font = New System.Drawing.Font("Tahoma", 7.8!)
        DataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdListaDespacho.RowHeadersDefaultCellStyle = DataGridViewCellStyle20
        Me.grdListaDespacho.RowHeadersVisible = False
        Me.grdListaDespacho.RowHeadersWidth = 40
        Me.grdListaDespacho.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdListaDespacho.Size = New System.Drawing.Size(1344, 457)
        Me.grdListaDespacho.TabIndex = 1
        '
        'IdPickingUbic
        '
        DataGridViewCellStyle19.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle19.Format = "N0"
        Me.IdPickingUbic.DefaultCellStyle = DataGridViewCellStyle19
        Me.IdPickingUbic.HeaderText = "Picking Ubicación"
        Me.IdPickingUbic.MinimumWidth = 6
        Me.IdPickingUbic.Name = "IdPickingUbic"
        Me.IdPickingUbic.ReadOnly = True
        Me.IdPickingUbic.Width = 125
        '
        'Codigo
        '
        Me.Codigo.HeaderText = "Código"
        Me.Codigo.MinimumWidth = 6
        Me.Codigo.Name = "Codigo"
        Me.Codigo.ReadOnly = True
        Me.Codigo.Width = 125
        '
        'Producto
        '
        Me.Producto.HeaderText = "Producto"
        Me.Producto.MinimumWidth = 6
        Me.Producto.Name = "Producto"
        Me.Producto.ReadOnly = True
        Me.Producto.Width = 125
        '
        'Presentacion
        '
        Me.Presentacion.HeaderText = "Presentación"
        Me.Presentacion.MinimumWidth = 6
        Me.Presentacion.Name = "Presentacion"
        Me.Presentacion.ReadOnly = True
        Me.Presentacion.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Presentacion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Presentacion.Width = 125
        '
        'Estado
        '
        Me.Estado.HeaderText = "Estado"
        Me.Estado.MinimumWidth = 6
        Me.Estado.Name = "Estado"
        Me.Estado.ReadOnly = True
        Me.Estado.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Estado.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Estado.Width = 125
        '
        'UnidadMedida
        '
        Me.UnidadMedida.HeaderText = "Unidad Medida"
        Me.UnidadMedida.MinimumWidth = 6
        Me.UnidadMedida.Name = "UnidadMedida"
        Me.UnidadMedida.ReadOnly = True
        Me.UnidadMedida.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.UnidadMedida.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.UnidadMedida.Width = 125
        '
        'Ubicacion
        '
        Me.Ubicacion.HeaderText = "Ubicación"
        Me.Ubicacion.MinimumWidth = 6
        Me.Ubicacion.Name = "Ubicacion"
        Me.Ubicacion.ReadOnly = True
        Me.Ubicacion.Width = 125
        '
        'IdDespachoDet
        '
        Me.IdDespachoDet.HeaderText = "IdDespachoDet"
        Me.IdDespachoDet.MinimumWidth = 6
        Me.IdDespachoDet.Name = "IdDespachoDet"
        Me.IdDespachoDet.ReadOnly = True
        Me.IdDespachoDet.Visible = False
        Me.IdDespachoDet.Width = 125
        '
        'CantidadPedido
        '
        Me.CantidadPedido.HeaderText = "Cantidad Pedido"
        Me.CantidadPedido.MinimumWidth = 6
        Me.CantidadPedido.Name = "CantidadPedido"
        Me.CantidadPedido.ReadOnly = True
        Me.CantidadPedido.Width = 125
        '
        'CantidadPicking
        '
        Me.CantidadPicking.HeaderText = "Cantidad Picking"
        Me.CantidadPicking.MinimumWidth = 6
        Me.CantidadPicking.Name = "CantidadPicking"
        Me.CantidadPicking.ReadOnly = True
        Me.CantidadPicking.Width = 125
        '
        'CantidadVerificada
        '
        Me.CantidadVerificada.HeaderText = "Cantidad Verificada"
        Me.CantidadVerificada.MinimumWidth = 6
        Me.CantidadVerificada.Name = "CantidadVerificada"
        Me.CantidadVerificada.ReadOnly = True
        Me.CantidadVerificada.Width = 125
        '
        'Fecha
        '
        Me.Fecha.HeaderText = "Fecha"
        Me.Fecha.MinimumWidth = 6
        Me.Fecha.Name = "Fecha"
        Me.Fecha.Width = 80
        '
        'IdProductoBodega
        '
        Me.IdProductoBodega.HeaderText = "IdProductoBodega"
        Me.IdProductoBodega.MinimumWidth = 6
        Me.IdProductoBodega.Name = "IdProductoBodega"
        Me.IdProductoBodega.ReadOnly = True
        Me.IdProductoBodega.Visible = False
        Me.IdProductoBodega.Width = 125
        '
        'IdProductoEstado
        '
        Me.IdProductoEstado.HeaderText = "IdProductoEstado"
        Me.IdProductoEstado.MinimumWidth = 6
        Me.IdProductoEstado.Name = "IdProductoEstado"
        Me.IdProductoEstado.ReadOnly = True
        Me.IdProductoEstado.Width = 125
        '
        'IdPresentacion
        '
        Me.IdPresentacion.HeaderText = "IdPresentacion"
        Me.IdPresentacion.MinimumWidth = 6
        Me.IdPresentacion.Name = "IdPresentacion"
        Me.IdPresentacion.ReadOnly = True
        Me.IdPresentacion.Visible = False
        Me.IdPresentacion.Width = 125
        '
        'IdUnidadMedida
        '
        Me.IdUnidadMedida.HeaderText = "IdUnidadMedida"
        Me.IdUnidadMedida.MinimumWidth = 6
        Me.IdUnidadMedida.Name = "IdUnidadMedida"
        Me.IdUnidadMedida.ReadOnly = True
        Me.IdUnidadMedida.Visible = False
        Me.IdUnidadMedida.Width = 125
        '
        'IdPickingEnc
        '
        Me.IdPickingEnc.HeaderText = "IdPickingEnc"
        Me.IdPickingEnc.MinimumWidth = 6
        Me.IdPickingEnc.Name = "IdPickingEnc"
        Me.IdPickingEnc.ReadOnly = True
        Me.IdPickingEnc.Visible = False
        Me.IdPickingEnc.Width = 125
        '
        'lnkAgregarPickingUbicacion
        '
        Me.lnkAgregarPickingUbicacion.AutoSize = True
        Me.lnkAgregarPickingUbicacion.Location = New System.Drawing.Point(12, 37)
        Me.lnkAgregarPickingUbicacion.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lnkAgregarPickingUbicacion.Name = "lnkAgregarPickingUbicacion"
        Me.lnkAgregarPickingUbicacion.Size = New System.Drawing.Size(182, 16)
        Me.lnkAgregarPickingUbicacion.TabIndex = 0
        Me.lnkAgregarPickingUbicacion.TabStop = True
        Me.lnkAgregarPickingUbicacion.Text = "Agregar Ubicación Picking (F2)"
        '
        'XtraTabPage1
        '
        Me.XtraTabPage1.Controls.Add(Me.dgridPacking)
        Me.XtraTabPage1.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.XtraTabPage1.Name = "XtraTabPage1"
        Me.XtraTabPage1.Size = New System.Drawing.Size(1040, 507)
        Me.XtraTabPage1.Text = "Packing"
        '
        'dgridPacking
        '
        Me.dgridPacking.DataSource = Me.DsOrdenCompraRecepcionOperador
        Me.dgridPacking.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridPacking.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.dgridPacking.Location = New System.Drawing.Point(0, 0)
        Me.dgridPacking.MainView = Me.gvPacking
        Me.dgridPacking.Margin = New System.Windows.Forms.Padding(4)
        Me.dgridPacking.MenuManager = Me.RibbonControl
        Me.dgridPacking.Name = "dgridPacking"
        Me.dgridPacking.Size = New System.Drawing.Size(1040, 507)
        Me.dgridPacking.TabIndex = 1
        Me.dgridPacking.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvPacking})
        '
        'gvPacking
        '
        Me.gvPacking.DetailHeight = 431
        Me.gvPacking.GridControl = Me.dgridPacking
        Me.gvPacking.Name = "gvPacking"
        Me.gvPacking.OptionsFind.AlwaysVisible = True
        Me.gvPacking.OptionsView.ColumnAutoWidth = False
        Me.gvPacking.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.gvPacking.OptionsView.ShowFooter = True
        '
        'DataBindingSource
        '
        Me.DataBindingSource.DataMember = "Data"
        Me.DataBindingSource.DataSource = Me.DsOrdenCompraRecepcionOperador
        '
        'Fec_agrDateEdit
        '
        Me.Fec_agrDateEdit.EditValue = Nothing
        Me.Fec_agrDateEdit.Enabled = False
        Me.Fec_agrDateEdit.Location = New System.Drawing.Point(149, 39)
        Me.Fec_agrDateEdit.Margin = New System.Windows.Forms.Padding(4)
        Me.Fec_agrDateEdit.MenuManager = Me.RibbonControl
        Me.Fec_agrDateEdit.Name = "Fec_agrDateEdit"
        Me.Fec_agrDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit.Size = New System.Drawing.Size(261, 22)
        Me.Fec_agrDateEdit.TabIndex = 5
        '
        'User_agrTextEdit
        '
        Me.User_agrTextEdit.Enabled = False
        Me.User_agrTextEdit.Location = New System.Drawing.Point(149, 7)
        Me.User_agrTextEdit.Margin = New System.Windows.Forms.Padding(4)
        Me.User_agrTextEdit.MenuManager = Me.RibbonControl
        Me.User_agrTextEdit.Name = "User_agrTextEdit"
        Me.User_agrTextEdit.Size = New System.Drawing.Size(261, 22)
        Me.User_agrTextEdit.TabIndex = 1
        '
        'Fec_modDateEdit
        '
        Me.Fec_modDateEdit.EditValue = Nothing
        Me.Fec_modDateEdit.Enabled = False
        Me.Fec_modDateEdit.Location = New System.Drawing.Point(634, 39)
        Me.Fec_modDateEdit.Margin = New System.Windows.Forms.Padding(4)
        Me.Fec_modDateEdit.MenuManager = Me.RibbonControl
        Me.Fec_modDateEdit.Name = "Fec_modDateEdit"
        Me.Fec_modDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit.Size = New System.Drawing.Size(261, 22)
        Me.Fec_modDateEdit.TabIndex = 7
        '
        'User_modTextEdit
        '
        Me.User_modTextEdit.Enabled = False
        Me.User_modTextEdit.Location = New System.Drawing.Point(634, 7)
        Me.User_modTextEdit.Margin = New System.Windows.Forms.Padding(4)
        Me.User_modTextEdit.MenuManager = Me.RibbonControl
        Me.User_modTextEdit.Name = "User_modTextEdit"
        Me.User_modTextEdit.Size = New System.Drawing.Size(261, 22)
        Me.User_modTextEdit.TabIndex = 2
        '
        'dkDespacho
        '
        Me.dkDespacho.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerBottom})
        Me.dkDespacho.Form = Me
        Me.dkDespacho.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane"})
        '
        'hideContainerBottom
        '
        Me.hideContainerBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.hideContainerBottom.Controls.Add(Me.DockPanel1)
        Me.hideContainerBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 730)
        Me.hideContainerBottom.Margin = New System.Windows.Forms.Padding(4)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(1291, 26)
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.ID = New System.Guid("a08cb207-90fb-483b-a54d-14f9e4608151")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 791)
        Me.DockPanel1.Margin = New System.Windows.Forms.Padding(4)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(200, 88)
        Me.DockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.SavedIndex = 0
        Me.DockPanel1.Size = New System.Drawing.Size(1713, 89)
        Me.DockPanel1.Text = "Bitácora"
        Me.DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.Fec_agrDateEdit)
        Me.DockPanel1_Container.Controls.Add(Me.User_agrTextEdit)
        Me.DockPanel1_Container.Controls.Add(Fec_agrLabel)
        Me.DockPanel1_Container.Controls.Add(User_modLabel)
        Me.DockPanel1_Container.Controls.Add(Me.User_modTextEdit)
        Me.DockPanel1_Container.Controls.Add(User_agrLabel)
        Me.DockPanel1_Container.Controls.Add(Fec_modLabel)
        Me.DockPanel1_Container.Controls.Add(Me.Fec_modDateEdit)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(4, 39)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1704, 46)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'PedidoEnc
        '
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle4.Format = "N0"
        Me.PedidoEnc.DefaultCellStyle = DataGridViewCellStyle4
        Me.PedidoEnc.HeaderText = "Pedido"
        Me.PedidoEnc.MinimumWidth = 6
        Me.PedidoEnc.Name = "PedidoEnc"
        Me.PedidoEnc.ReadOnly = True
        Me.PedidoEnc.Width = 125
        '
        'CodigoPedido
        '
        Me.CodigoPedido.HeaderText = "Código"
        Me.CodigoPedido.MinimumWidth = 6
        Me.CodigoPedido.Name = "CodigoPedido"
        Me.CodigoPedido.ReadOnly = True
        Me.CodigoPedido.Width = 125
        '
        'ProductoDetalle
        '
        Me.ProductoDetalle.HeaderText = "Producto"
        Me.ProductoDetalle.MinimumWidth = 6
        Me.ProductoDetalle.Name = "ProductoDetalle"
        Me.ProductoDetalle.ReadOnly = True
        Me.ProductoDetalle.Width = 125
        '
        'PresentacionDetalle
        '
        Me.PresentacionDetalle.HeaderText = "Presentación"
        Me.PresentacionDetalle.MinimumWidth = 6
        Me.PresentacionDetalle.Name = "PresentacionDetalle"
        Me.PresentacionDetalle.ReadOnly = True
        Me.PresentacionDetalle.Width = 125
        '
        'UMDetalle
        Me.xtpDifPickVrsPack.Name = "xtpDifPickVrsPack"
        Me.xtpDifPickVrsPack.Size = New System.Drawing.Size(1040, 507)
        Me.xtpDifPickVrsPack.Text = "Diferencia Packing"
        '
        Me.UMDetalle.HeaderText = "Unidad Medida"
        Me.UMDetalle.MinimumWidth = 6
        Me.UMDetalle.Name = "UMDetalle"
        Me.UMDetalle.ReadOnly = True
        Me.UMDetalle.Width = 125
        '
        'EstadoDetalle
        '
        Me.EstadoDetalle.HeaderText = "Estado"
        Me.EstadoDetalle.MinimumWidth = 6
        Me.EstadoDetalle.Name = "EstadoDetalle"
        Me.EstadoDetalle.ReadOnly = True
        Me.EstadoDetalle.Width = 125
        '
        'Cantidad
        '
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle5.Format = "N2"
        Me.Cantidad.DefaultCellStyle = DataGridViewCellStyle5
        Me.Cantidad.HeaderText = "Cantidad"
        Me.Cantidad.MinimumWidth = 6
        Me.Cantidad.Name = "Cantidad"
        Me.Cantidad.ReadOnly = True
        Me.Cantidad.Width = 125
        '
        'ClienteDias
        '
        Me.ClienteDias.HeaderText = "Cliente Días"
        Me.ClienteDias.MinimumWidth = 6
        Me.ClienteDias.Name = "ClienteDias"
        Me.ClienteDias.ReadOnly = True
        Me.ClienteDias.Width = 125
        '
        'CantidadRecibida
        '
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle6.Format = "N2"
        DataGridViewCellStyle6.NullValue = "0.0"
        Me.CantidadRecibida.DefaultCellStyle = DataGridViewCellStyle6
        Me.CantidadRecibida.HeaderText = "Cantidad Recibida"
        Me.CantidadRecibida.MinimumWidth = 6
        Me.CantidadRecibida.Name = "CantidadRecibida"
        Me.CantidadRecibida.ReadOnly = True
        Me.CantidadRecibida.Visible = False
        Me.CantidadRecibida.Width = 125
        '
        'Operador
        '
        Me.Operador.HeaderText = "Operador"
        Me.Operador.MinimumWidth = 6
        Me.Operador.Name = "Operador"
        Me.Operador.Visible = False
        Me.Operador.Width = 125
        '
        'PedidoDet
        '
        Me.PedidoDet.HeaderText = "Pedido Det"
        Me.PedidoDet.MinimumWidth = 6
        Me.PedidoDet.Name = "PedidoDet"
        Me.PedidoDet.Visible = False
        Me.PedidoDet.Width = 125
        '
        'colTalla
        '
        Me.colTalla.HeaderText = "Talla"
        Me.colTalla.MinimumWidth = 6
        Me.colTalla.Name = "colTalla"
        Me.colTalla.ReadOnly = True
        Me.colTalla.Width = 125
        '
        'colColor
        '
        Me.colColor.HeaderText = "Color"
        Me.colColor.MinimumWidth = 6
        Me.colColor.Name = "colColor"
        Me.colColor.ReadOnly = True
        Me.colColor.Width = 125
        '
        'frmDespacho
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1291, 786)
        Me.Controls.Add(Me.XtraTabControl1)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmDespacho"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Despacho"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabControl1.ResumeLayout(False)
        Me.tbPagina1.ResumeLayout(False)
        CType(Me.GrpDato, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpDato.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.grpTransporte, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpTransporte.ResumeLayout(False)
        Me.grpTransporte.PerformLayout()
        CType(Me.lcmbRuta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lcmbVehiculo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lcmbPiloto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpDatosAsociados, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpDatosAsociados.ResumeLayout(False)
        Me.grpDatosAsociados.PerformLayout()
        CType(Me.txtCantidadBultos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNumero, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoPase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpDatosGenerales, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpDatosGenerales.ResumeLayout(False)
        Me.grpDatosGenerales.PerformLayout()
        CType(Me.dtmFechaDespacho.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtmFechaDespacho.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpDatosFecha, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpDatosFecha.ResumeLayout(False)
        CType(Me.dtmFechaTarea.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtmFechaTarea.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl4.ResumeLayout(False)
        Me.GroupControl4.PerformLayout()
        CType(Me.GroupControl9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl9.ResumeLayout(False)
        Me.GroupControl9.PerformLayout()
        Me.tbPedido.ResumeLayout(False)
        CType(Me.grpPedido, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpPedido.ResumeLayout(False)
        CType(Me.grdPedido, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbDetalleProducto.ResumeLayout(False)
        CType(Me.grdProducto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbUbicPicking.ResumeLayout(False)
        CType(Me.panelUbic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelUbic.ResumeLayout(False)
        CType(Me.grdUbicPicking, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsOrdenCompraRecepcionOperador, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdvPickingUbic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbPagina2.ResumeLayout(False)
        CType(Me.GrpDetalle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpDetalle.ResumeLayout(False)
        Me.GrpDetalle.PerformLayout()
        CType(Me.grdListaDespacho, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabPage1.ResumeLayout(False)
        CType(Me.dgridPacking, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvPacking, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dkDespacho, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerBottom.ResumeLayout(False)
        Me.DockPanel1.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        Me.DockPanel1_Container.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents mnuGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuAnular As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents XtraTabControl1 As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents tbPagina2 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tbPagina1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GrpDato As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GrpDetalle As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lnkVehiculo As System.Windows.Forms.LinkLabel
    Friend WithEvents lblEstado As System.Windows.Forms.Label
    Friend WithEvents grpDatosFecha As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl4 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dtmHoraFhh As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtmHoraIhh As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupControl9 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dtmHoraF As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtmHoraI As System.Windows.Forms.DateTimePicker
    Friend WithEvents DataBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DsOrdenCompraRecepcionOperador As TOMWMS.DsOrdenCompraRecepcionOperador
    Friend WithEvents Fec_agrDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_agrTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_modDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_modTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lnkAgregarPickingUbicacion As System.Windows.Forms.LinkLabel
    Friend WithEvents grdListaDespacho As System.Windows.Forms.DataGridView
    Friend WithEvents chkActivo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents grpTransporte As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lnkPiloto As LinkLabel
    Friend WithEvents cmdImprimir As DevExpress.XtraBars.BarSubItem
    Friend WithEvents cmdListaUbicacion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lnkRuta As LinkLabel
    Friend WithEvents txtNumero As NumericUpDown
    Friend WithEvents txtNoPase As NumericUpDown
    Friend WithEvents txtMarchamo As TextBox
    Friend WithEvents txtObservacion As TextBox
    Friend WithEvents txtCantidadBultos As NumericUpDown
    Friend WithEvents IdPickingUbic As DataGridViewTextBoxColumn
    Friend WithEvents Codigo As DataGridViewTextBoxColumn
    Friend WithEvents Producto As DataGridViewTextBoxColumn
    Friend WithEvents Presentacion As DataGridViewTextBoxColumn
    Friend WithEvents Estado As DataGridViewTextBoxColumn
    Friend WithEvents UnidadMedida As DataGridViewTextBoxColumn
    Friend WithEvents Ubicacion As DataGridViewTextBoxColumn
    Friend WithEvents IdDespachoDet As DataGridViewTextBoxColumn
    Friend WithEvents CantidadPedido As DataGridViewTextBoxColumn
    Friend WithEvents CantidadPicking As DataGridViewTextBoxColumn
    Friend WithEvents CantidadVerificada As DataGridViewTextBoxColumn
    Friend WithEvents Fecha As DataGridViewTextBoxColumn
    Friend WithEvents IdProductoBodega As DataGridViewTextBoxColumn
    Friend WithEvents IdProductoEstado As DataGridViewTextBoxColumn
    Friend WithEvents IdPresentacion As DataGridViewTextBoxColumn
    Friend WithEvents IdUnidadMedida As DataGridViewTextBoxColumn
    Friend WithEvents IdPickingEnc As DataGridViewTextBoxColumn
    Friend WithEvents tbPedido As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents grpPedido As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grdPedido As DataGridView
    Friend WithEvents tbDetalleProducto As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents grdProducto As DataGridView
    Friend WithEvents tbUbicPicking As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents panelUbic As DevExpress.XtraEditors.PanelControl
    Friend WithEvents grdUbicPicking As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdvPickingUbic As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents dkDespacho As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents cmbBodega As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbPropietario As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents dtmFechaTarea As DevExpress.XtraEditors.DateEdit
    Friend WithEvents dtmFechaDespacho As DevExpress.XtraEditors.DateEdit
    Friend WithEvents PedidoEnc As DataGridViewTextBoxColumn
    Friend WithEvents CodigoPedido As DataGridViewTextBoxColumn
    Friend WithEvents ProductoDetalle As DataGridViewTextBoxColumn
    Friend WithEvents PresentacionDetalle As DataGridViewTextBoxColumn
    Friend WithEvents UMDetalle As DataGridViewTextBoxColumn
    Friend WithEvents EstadoDetalle As DataGridViewTextBoxColumn
    Friend WithEvents Cantidad As DataGridViewTextBoxColumn
    Friend WithEvents ClienteDias As DataGridViewTextBoxColumn
    Friend WithEvents CantidadRecibida As DataGridViewTextBoxColumn
    Friend WithEvents Operador As DataGridViewComboBoxColumn
    Friend WithEvents PedidoDet As DataGridViewTextBoxColumn
    Friend WithEvents lblC As TextBox
    Friend WithEvents Pedido As DataGridViewTextBoxColumn
    Friend WithEvents Referencia As DataGridViewTextBoxColumn
    Friend WithEvents Bodega As DataGridViewTextBoxColumn
    Friend WithEvents Cliente As DataGridViewTextBoxColumn
    Friend WithEvents Propietario As DataGridViewTextBoxColumn
    Friend WithEvents FechaPedido As DataGridViewTextBoxColumn
    Friend WithEvents btwnegativeStock As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents lnkPedido As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents grpDatosAsociados As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grpDatosGenerales As DevExpress.XtraEditors.GroupControl
    Friend WithEvents XtraTabPage1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents dgridPacking As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvPacking As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents mnuImprimir As DevExpress.XtraBars.BarSubItem
    Friend WithEvents mnuImprimirDespacho As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuImprimirListaEmpaque As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lcmbRuta As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lcmbVehiculo As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lcmbPiloto As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents mnuGuardarDatosCabecera As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuRepetirEncabezadoEnCadaPagina As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents txtDocumentoExterno As TextBox
    Friend WithEvents PedidoEnc As DataGridViewTextBoxColumn
    Friend WithEvents CodigoPedido As DataGridViewTextBoxColumn
    Friend WithEvents ProductoDetalle As DataGridViewTextBoxColumn
    Friend WithEvents PresentacionDetalle As DataGridViewTextBoxColumn
    Friend WithEvents UMDetalle As DataGridViewTextBoxColumn
    Friend WithEvents EstadoDetalle As DataGridViewTextBoxColumn
    Friend WithEvents Cantidad As DataGridViewTextBoxColumn
    Friend WithEvents ClienteDias As DataGridViewTextBoxColumn
    Friend WithEvents CantidadRecibida As DataGridViewTextBoxColumn
    Friend WithEvents Operador As DataGridViewComboBoxColumn
    Friend WithEvents PedidoDet As DataGridViewTextBoxColumn
    Friend WithEvents colTalla As DataGridViewTextBoxColumn
    Friend WithEvents colColor As DataGridViewTextBoxColumn
End Class
