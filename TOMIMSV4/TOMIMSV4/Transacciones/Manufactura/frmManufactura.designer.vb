<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmManufactura
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If components IsNot Nothing Then
                components.Dispose()
            End If
            'If DTOperadores IsNot Nothing Then
            '    DTOperadores.Dispose()
            '    DTOperadores = Nothing
            'End If

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
        Dim SplashScreenManager1 As DevExpress.XtraSplashScreen.SplashScreenManager = New DevExpress.XtraSplashScreen.SplashScreenManager(Me, Nothing, True, True)
        Dim Label4 As System.Windows.Forms.Label
        Dim Label7 As System.Windows.Forms.Label
        Dim Label30 As System.Windows.Forms.Label
        Dim Label38 As System.Windows.Forms.Label
        Dim Label37 As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim IdPropietarioLabel As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim Label12 As System.Windows.Forms.Label
        Dim Label10 As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim Label5 As System.Windows.Forms.Label
        Dim lblScan As System.Windows.Forms.Label
        Dim Label6 As System.Windows.Forms.Label
        Dim lblBarraProducto As System.Windows.Forms.Label
        Dim Label8 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmManufactura))
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim GridFormatRule1 As DevExpress.XtraGrid.GridFormatRule = New DevExpress.XtraGrid.GridFormatRule()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.lblRegistros = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.cmdGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuAsignacion = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem3 = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdCodigoBarra = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImprimeCodigoBarra = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImprmirCodigoBarra = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdUbicacion = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImprimir = New DevExpress.XtraBars.BarButtonItem()
        Me.SubImprimir = New DevExpress.XtraBars.BarSubItem()
        Me.cmdPreIngreso = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdCostoArancel = New DevExpress.XtraBars.BarButtonItem()
        Me.lblRegs = New DevExpress.XtraBars.BarStaticItem()
        Me.lnkAgregarPedido = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdCerrar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage3 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup3 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView4 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.DataBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsOrdenCompraRecepcionOperador = New DsOrdenCompraRecepcionOperador()
        Me.Bar2 = New DevExpress.XtraBars.Bar()
        Me.dkRecepcion = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.tmrActualizarDatosRecepcion = New System.Windows.Forms.Timer(Me.components)
        Me.XtraTabPageUbicacionPicking = New DevExpress.XtraTab.XtraTabPage()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.dgridPickingUbic = New DevExpress.XtraGrid.GridControl()
        Me.grdvPickingUbic = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView6 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.grpScan = New DevExpress.XtraEditors.GroupControl()
        Me.txtLote = New DevExpress.XtraEditors.TextEdit()
        Me.txtDescripcionProducto = New DevExpress.XtraEditors.TextEdit()
        Me.txtCantidadRegistrada = New System.Windows.Forms.LinkLabel()
        Me.txtScanner = New DevExpress.XtraEditors.TextEdit()
        Me.txtCantidadEsperada = New System.Windows.Forms.LinkLabel()
        Me.txtCantidad = New DevExpress.XtraEditors.TextEdit()
        Me.GridView7 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.XtratabPagePedido = New DevExpress.XtraTab.XtraTabPage()
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.GroupControl6 = New DevExpress.XtraEditors.GroupControl()
        Me.dgridPedidos = New System.Windows.Forms.DataGridView()
        Me.IdPedido = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Referencia = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Bodega = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cliente = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Propietario = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FechaPedido = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EstadoP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.cmbAgrupamiento = New System.Windows.Forms.ComboBox()
        Me.dgridDetalleManufactura = New System.Windows.Forms.DataGridView()
        Me.IdPedidoEnc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Codigo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Producto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Presentacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UnidadMedida = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Estado = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cantidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ClienteDias = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CantidadRecibida = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OperadorBodega = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.IdPedidoDet = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.XtratabPageDato = New DevExpress.XtraTab.XtraTabPage()
        Me.GroupControl10 = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl5 = New DevExpress.XtraEditors.GroupControl()
        Me.cmbTipoManufactura = New DevExpress.XtraEditors.LookUpEdit()
        Me.dtmFechaManufactura = New DevExpress.XtraEditors.DateEdit()
        Me.cmbBodegas = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbPropietario = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblEstado = New System.Windows.Forms.Label()
        Me.lblCodigo = New System.Windows.Forms.Label()
        Me.GrpTarea = New DevExpress.XtraEditors.GroupControl()
        Me.dtmFechaTarea = New DevExpress.XtraEditors.DateEdit()
        Me.GroupControl4 = New DevExpress.XtraEditors.GroupControl()
        Me.dtmHoraFhh = New System.Windows.Forms.DateTimePicker()
        Me.dtmHoraIhh = New System.Windows.Forms.DateTimePicker()
        Me.GroupControl9 = New DevExpress.XtraEditors.GroupControl()
        Me.dtmHoraF = New System.Windows.Forms.DateTimePicker()
        Me.dtmHoraI = New System.Windows.Forms.DateTimePicker()
        Me.XtraTabControl1 = New DevExpress.XtraTab.XtraTabControl()
        Me.DxErrorProvider1 = New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(Me.components)
        Label4 = New System.Windows.Forms.Label()
        Label7 = New System.Windows.Forms.Label()
        Label30 = New System.Windows.Forms.Label()
        Label38 = New System.Windows.Forms.Label()
        Label37 = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        IdPropietarioLabel = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        Label12 = New System.Windows.Forms.Label()
        Label10 = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Label5 = New System.Windows.Forms.Label()
        lblScan = New System.Windows.Forms.Label()
        Label6 = New System.Windows.Forms.Label()
        lblBarraProducto = New System.Windows.Forms.Label()
        Label8 = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsOrdenCompraRecepcionOperador, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dkRecepcion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabPageUbicacionPicking.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.dgridPickingUbic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdvPickingUbic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpScan, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpScan.SuspendLayout()
        CType(Me.txtLote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescripcionProducto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtScanner.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCantidad.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtratabPagePedido.SuspendLayout()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel1.SuspendLayout()
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel2.SuspendLayout()
        Me.SplitContainerControl1.SuspendLayout()
        CType(Me.GroupControl6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl6.SuspendLayout()
        CType(Me.dgridPedidos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.dgridDetalleManufactura, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtratabPageDato.SuspendLayout()
        CType(Me.GroupControl10, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl10.SuspendLayout()
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl5.SuspendLayout()
        CType(Me.cmbTipoManufactura.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtmFechaManufactura.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtmFechaManufactura.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBodegas.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpTarea, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpTarea.SuspendLayout()
        CType(Me.dtmFechaTarea.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtmFechaTarea.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl4.SuspendLayout()
        CType(Me.GroupControl9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl9.SuspendLayout()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabControl1.SuspendLayout()
        CType(Me.DxErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplashScreenManager1
        '
        SplashScreenManager1.ClosingDelay = 500
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(5, 39)
        Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(58, 16)
        Label4.TabIndex = 1
        Label4.Text = "Agrupar:"
        '
        'Label7
        '
        Label7.AutoSize = True
        Label7.Location = New System.Drawing.Point(10, 49)
        Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label7.Name = "Label7"
        Label7.Size = New System.Drawing.Size(73, 16)
        Label7.TabIndex = 0
        Label7.Text = "Hora Inicio:"
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
        'Label38
        '
        Label38.AutoSize = True
        Label38.Location = New System.Drawing.Point(10, 49)
        Label38.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label38.Name = "Label38"
        Label38.Size = New System.Drawing.Size(73, 16)
        Label38.TabIndex = 0
        Label38.Text = "Hora Inicio:"
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
        'Label1
        '
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(24, 222)
        Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(74, 16)
        Label1.TabIndex = 8
        Label1.Text = "Propietario:"
        '
        'IdPropietarioLabel
        '
        IdPropietarioLabel.AutoSize = True
        IdPropietarioLabel.Location = New System.Drawing.Point(24, 188)
        IdPropietarioLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        IdPropietarioLabel.Name = "IdPropietarioLabel"
        IdPropietarioLabel.Size = New System.Drawing.Size(54, 16)
        IdPropietarioLabel.TabIndex = 6
        IdPropietarioLabel.Text = "Bodega:"
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(24, 156)
        Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(122, 16)
        Label2.TabIndex = 4
        Label2.Text = "Fecha manufactura:"
        '
        'Label12
        '
        Label12.AutoSize = True
        Label12.Location = New System.Drawing.Point(24, 55)
        Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label12.Name = "Label12"
        Label12.Size = New System.Drawing.Size(46, 16)
        Label12.TabIndex = 0
        Label12.Text = "Código"
        '
        'Label10
        '
        Label10.AutoSize = True
        Label10.Location = New System.Drawing.Point(24, 89)
        Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label10.Name = "Label10"
        Label10.Size = New System.Drawing.Size(45, 16)
        Label10.TabIndex = 2
        Label10.Text = "Estado"
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.Location = New System.Drawing.Point(24, 123)
        Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(112, 16)
        Label3.TabIndex = 20
        Label3.Text = "Tipo Manufactura:"
        '
        'Label5
        '
        Label5.AutoSize = True
        Label5.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label5.Location = New System.Drawing.Point(420, 37)
        Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(81, 21)
        Label5.TabIndex = 49
        Label5.Text = "Cantidad:"
        '
        'lblScan
        '
        lblScan.AutoSize = True
        lblScan.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblScan.Location = New System.Drawing.Point(27, 37)
        lblScan.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblScan.Name = "lblScan"
        lblScan.Size = New System.Drawing.Size(66, 21)
        lblScan.TabIndex = 43
        lblScan.Text = "Código:"
        '
        'Label6
        '
        Label6.Font = New System.Drawing.Font("Tahoma", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label6.Location = New System.Drawing.Point(1240, 41)
        Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(31, 59)
        Label6.TabIndex = 39
        Label6.Text = "/"
        '
        'lblBarraProducto
        '
        lblBarraProducto.AutoSize = True
        lblBarraProducto.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblBarraProducto.Location = New System.Drawing.Point(567, 108)
        lblBarraProducto.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblBarraProducto.Name = "lblBarraProducto"
        lblBarraProducto.Size = New System.Drawing.Size(48, 21)
        lblBarraProducto.TabIndex = 51
        lblBarraProducto.Text = "Lote:"
        '
        'Label8
        '
        Label8.AutoSize = True
        Label8.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label8.Location = New System.Drawing.Point(567, 66)
        Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label8.Name = "Label8"
        Label8.Size = New System.Drawing.Size(102, 21)
        Label8.TabIndex = 52
        Label8.Text = "Descripcion:"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblRegistros)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 852)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1554, 30)
        '
        'lblRegistros
        '
        Me.lblRegistros.Caption = "Registros: 0"
        Me.lblRegistros.Id = 25
        Me.lblRegistros.Name = "lblRegistros"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.cmdGuardar, Me.mnuAsignacion, Me.BarButtonItem1, Me.BarButtonItem2, Me.BarButtonItem3, Me.cmdCodigoBarra, Me.cmdImprimeCodigoBarra, Me.cmdImprmirCodigoBarra, Me.cmdEliminar, Me.cmdUbicacion, Me.cmdImprimir, Me.SubImprimir, Me.cmdPreIngreso, Me.cmdCostoArancel, Me.lblRegs, Me.lnkAgregarPedido, Me.lblRegistros, Me.cmdCerrar})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RibbonControl.MaxItemId = 27
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1554, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'cmdGuardar
        '
        Me.cmdGuardar.Caption = "Guardar"
        Me.cmdGuardar.Id = 1
        Me.cmdGuardar.ImageOptions.SvgImage = CType(resources.GetObject("cmdGuardar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdGuardar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.G))
        Me.cmdGuardar.Name = "cmdGuardar"
        Me.cmdGuardar.ShortcutKeyDisplayString = "G"
        '
        'mnuAsignacion
        '
        Me.mnuAsignacion.Caption = "Asignacion"
        Me.mnuAsignacion.Id = 4
        Me.mnuAsignacion.ImageOptions.Image = CType(resources.GetObject("mnuAsignacion.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuAsignacion.ImageOptions.LargeImage = CType(resources.GetObject("mnuAsignacion.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuAsignacion.Name = "mnuAsignacion"
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "Estadística"
        Me.BarButtonItem1.Id = 5
        Me.BarButtonItem1.ImageOptions.Image = CType(resources.GetObject("BarButtonItem1.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItem1.ImageOptions.LargeImage = CType(resources.GetObject("BarButtonItem1.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'BarButtonItem2
        '
        Me.BarButtonItem2.Caption = "Movimientos"
        Me.BarButtonItem2.Id = 6
        Me.BarButtonItem2.ImageOptions.Image = CType(resources.GetObject("BarButtonItem2.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItem2.ImageOptions.LargeImage = CType(resources.GetObject("BarButtonItem2.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.BarButtonItem2.Name = "BarButtonItem2"
        '
        'BarButtonItem3
        '
        Me.BarButtonItem3.Caption = "BarButtonItem3"
        Me.BarButtonItem3.Id = 7
        Me.BarButtonItem3.Name = "BarButtonItem3"
        '
        'cmdCodigoBarra
        '
        Me.cmdCodigoBarra.Caption = "Códigos de barra"
        Me.cmdCodigoBarra.Id = 8
        Me.cmdCodigoBarra.ImageOptions.Image = CType(resources.GetObject("cmdCodigoBarra.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdCodigoBarra.ImageOptions.LargeImage = CType(resources.GetObject("cmdCodigoBarra.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.cmdCodigoBarra.Name = "cmdCodigoBarra"
        '
        'cmdImprimeCodigoBarra
        '
        Me.cmdImprimeCodigoBarra.Caption = "Imprimir Código Barra"
        Me.cmdImprimeCodigoBarra.Id = 9
        Me.cmdImprimeCodigoBarra.ImageOptions.Image = CType(resources.GetObject("cmdImprimeCodigoBarra.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdImprimeCodigoBarra.ImageOptions.LargeImage = CType(resources.GetObject("cmdImprimeCodigoBarra.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.cmdImprimeCodigoBarra.Name = "cmdImprimeCodigoBarra"
        '
        'cmdImprmirCodigoBarra
        '
        Me.cmdImprmirCodigoBarra.Caption = "Código Barra"
        Me.cmdImprmirCodigoBarra.Id = 10
        Me.cmdImprmirCodigoBarra.ImageOptions.Image = CType(resources.GetObject("cmdImprmirCodigoBarra.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdImprmirCodigoBarra.ImageOptions.LargeImage = CType(resources.GetObject("cmdImprmirCodigoBarra.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.cmdImprmirCodigoBarra.Name = "cmdImprmirCodigoBarra"
        '
        'cmdEliminar
        '
        Me.cmdEliminar.Caption = "Anular"
        Me.cmdEliminar.Id = 12
        Me.cmdEliminar.ImageOptions.SvgImage = CType(resources.GetObject("cmdEliminar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdEliminar.Name = "cmdEliminar"
        '
        'cmdUbicacion
        '
        Me.cmdUbicacion.Caption = "Ubicación"
        Me.cmdUbicacion.Id = 13
        Me.cmdUbicacion.ImageOptions.Image = CType(resources.GetObject("cmdUbicacion.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdUbicacion.ImageOptions.LargeImage = CType(resources.GetObject("cmdUbicacion.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.cmdUbicacion.Name = "cmdUbicacion"
        '
        'cmdImprimir
        '
        Me.cmdImprimir.Caption = "Imprimir"
        Me.cmdImprimir.Id = 14
        Me.cmdImprimir.ImageOptions.Image = CType(resources.GetObject("cmdImprimir.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdImprimir.ImageOptions.LargeImage = CType(resources.GetObject("cmdImprimir.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.cmdImprimir.Name = "cmdImprimir"
        '
        'SubImprimir
        '
        Me.SubImprimir.Caption = "Imprimir"
        Me.SubImprimir.Id = 15
        Me.SubImprimir.ImageOptions.Image = CType(resources.GetObject("SubImprimir.ImageOptions.Image"), System.Drawing.Image)
        Me.SubImprimir.ImageOptions.LargeImage = CType(resources.GetObject("SubImprimir.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.SubImprimir.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.cmdPreIngreso), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdCostoArancel)})
        Me.SubImprimir.Name = "SubImprimir"
        '
        'cmdPreIngreso
        '
        Me.cmdPreIngreso.Caption = "Orden Compra Pre Ingreso"
        Me.cmdPreIngreso.Id = 16
        Me.cmdPreIngreso.Name = "cmdPreIngreso"
        '
        'cmdCostoArancel
        '
        Me.cmdCostoArancel.Caption = "Costo y Arancel"
        Me.cmdCostoArancel.Id = 17
        Me.cmdCostoArancel.Name = "cmdCostoArancel"
        '
        'lblRegs
        '
        Me.lblRegs.Caption = "Registros:"
        Me.lblRegs.Id = 20
        Me.lblRegs.Name = "lblRegs"
        '
        'lnkAgregarPedido
        '
        Me.lnkAgregarPedido.Caption = "Agregar Pedido"
        Me.lnkAgregarPedido.Id = 23
        Me.lnkAgregarPedido.ImageOptions.SvgImage = CType(resources.GetObject("lnkAgregarPedido.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.lnkAgregarPedido.Name = "lnkAgregarPedido"
        '
        'cmdCerrar
        '
        Me.cmdCerrar.Caption = "Finalizar"
        Me.cmdCerrar.Id = 26
        Me.cmdCerrar.ImageOptions.SvgImage = CType(resources.GetObject("cmdCerrar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdCerrar.Name = "cmdCerrar"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Menu"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdGuardar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdEliminar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.lnkAgregarPedido)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdCerrar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'mnuEliminar
        '
        Me.mnuEliminar.Caption = "Eliminar"
        Me.mnuEliminar.Id = 3
        Me.mnuEliminar.ImageOptions.Image = CType(resources.GetObject("mnuEliminar.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuEliminar.ImageOptions.LargeImage = CType(resources.GetObject("mnuEliminar.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuEliminar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E))
        Me.mnuEliminar.Name = "mnuEliminar"
        '
        'RibbonPage3
        '
        Me.RibbonPage3.Name = "RibbonPage3"
        Me.RibbonPage3.Text = "Opción"
        '
        'RibbonPageGroup3
        '
        Me.RibbonPageGroup3.Name = "RibbonPageGroup3"
        '
        'GridView1
        '
        Me.GridView1.Name = "GridView1"
        '
        'GridView2
        '
        Me.GridView2.Name = "GridView2"
        '
        'GridView3
        '
        Me.GridView3.Name = "GridView3"
        '
        'GridView4
        '
        Me.GridView4.Name = "GridView4"
        '
        'DataBindingSource
        '
        Me.DataBindingSource.DataMember = "Data"
        Me.DataBindingSource.DataSource = Me.DsOrdenCompraRecepcionOperador
        '
        'DsOrdenCompraRecepcionOperador
        '
        Me.DsOrdenCompraRecepcionOperador.DataSetName = "DsOrdenCompraRecepcionOperador"
        Me.DsOrdenCompraRecepcionOperador.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Bar2
        '
        Me.Bar2.BarName = "Main menu"
        Me.Bar2.DockCol = 0
        Me.Bar2.DockRow = 0
        Me.Bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar2.OptionsBar.MultiLine = True
        Me.Bar2.OptionsBar.UseWholeRow = True
        Me.Bar2.Text = "Main menu"
        '
        'dkRecepcion
        '
        Me.dkRecepcion.Form = Me
        Me.dkRecepcion.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane"})
        '
        'tmrActualizarDatosRecepcion
        '
        Me.tmrActualizarDatosRecepcion.Interval = 3000
        '
        'XtraTabPageUbicacionPicking
        '
        Me.XtraTabPageUbicacionPicking.Appearance.HeaderActive.BackColor = System.Drawing.Color.PowderBlue
        Me.XtraTabPageUbicacionPicking.Appearance.HeaderActive.Options.UseBackColor = True
        Me.XtraTabPageUbicacionPicking.Controls.Add(Me.GroupControl2)
        Me.XtraTabPageUbicacionPicking.Controls.Add(Me.grpScan)
        Me.XtraTabPageUbicacionPicking.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.XtraTabPageUbicacionPicking.Name = "XtraTabPageUbicacionPicking"
        Me.XtraTabPageUbicacionPicking.Size = New System.Drawing.Size(1552, 629)
        Me.XtraTabPageUbicacionPicking.Text = "Detalle"
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.dgridPickingUbic)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl2.Location = New System.Drawing.Point(0, 167)
        Me.GroupControl2.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(1552, 462)
        Me.GroupControl2.TabIndex = 2
        Me.GroupControl2.Text = "Detalle"
        '
        'dgridPickingUbic
        '
        Me.dgridPickingUbic.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgridPickingUbic.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridPickingUbic.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        GridLevelNode1.RelationName = "Level1"
        Me.dgridPickingUbic.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.dgridPickingUbic.Location = New System.Drawing.Point(2, 28)
        Me.dgridPickingUbic.MainView = Me.grdvPickingUbic
        Me.dgridPickingUbic.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dgridPickingUbic.MenuManager = Me.RibbonControl
        Me.dgridPickingUbic.Name = "dgridPickingUbic"
        Me.dgridPickingUbic.Size = New System.Drawing.Size(1548, 432)
        Me.dgridPickingUbic.TabIndex = 1
        Me.dgridPickingUbic.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdvPickingUbic, Me.GridView6})
        '
        'grdvPickingUbic
        '
        Me.grdvPickingUbic.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdvPickingUbic.Appearance.HeaderPanel.Options.UseFont = True
        Me.grdvPickingUbic.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdvPickingUbic.Appearance.Row.Options.UseFont = True
        Me.grdvPickingUbic.DetailHeight = 431
        GridFormatRule1.Name = "Format0"
        GridFormatRule1.Rule = Nothing
        Me.grdvPickingUbic.FormatRules.Add(GridFormatRule1)
        Me.grdvPickingUbic.GridControl = Me.dgridPickingUbic
        Me.grdvPickingUbic.Name = "grdvPickingUbic"
        Me.grdvPickingUbic.OptionsBehavior.Editable = False
        Me.grdvPickingUbic.OptionsView.ColumnAutoWidth = False
        Me.grdvPickingUbic.OptionsView.ShowAutoFilterRow = True
        Me.grdvPickingUbic.OptionsView.ShowFooter = True
        Me.grdvPickingUbic.OptionsView.ShowGroupPanel = False
        '
        'GridView6
        '
        Me.GridView6.GridControl = Me.dgridPickingUbic
        Me.GridView6.Name = "GridView6"
        '
        'grpScan
        '
        Me.grpScan.AppearanceCaption.BackColor = System.Drawing.Color.SteelBlue
        Me.grpScan.AppearanceCaption.Options.UseBackColor = True
        Me.grpScan.AppearanceCaption.Options.UseTextOptions = True
        Me.grpScan.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.grpScan.Controls.Add(Label8)
        Me.grpScan.Controls.Add(Me.txtLote)
        Me.grpScan.Controls.Add(lblBarraProducto)
        Me.grpScan.Controls.Add(Me.txtDescripcionProducto)
        Me.grpScan.Controls.Add(Me.txtCantidadRegistrada)
        Me.grpScan.Controls.Add(Label5)
        Me.grpScan.Controls.Add(Label6)
        Me.grpScan.Controls.Add(Me.txtScanner)
        Me.grpScan.Controls.Add(Me.txtCantidadEsperada)
        Me.grpScan.Controls.Add(Me.txtCantidad)
        Me.grpScan.Controls.Add(lblScan)
        Me.grpScan.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpScan.Location = New System.Drawing.Point(0, 0)
        Me.grpScan.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.grpScan.Name = "grpScan"
        Me.grpScan.Size = New System.Drawing.Size(1552, 167)
        Me.grpScan.TabIndex = 51
        '
        'txtLote
        '
        Me.txtLote.Location = New System.Drawing.Point(677, 105)
        Me.txtLote.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtLote.Name = "txtLote"
        Me.txtLote.Properties.Appearance.BackColor = System.Drawing.Color.Wheat
        Me.txtLote.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLote.Properties.Appearance.Options.UseBackColor = True
        Me.txtLote.Properties.Appearance.Options.UseFont = True
        Me.txtLote.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtLote.Properties.MaxLength = 50
        Me.txtLote.Size = New System.Drawing.Size(401, 28)
        Me.txtLote.TabIndex = 50
        '
        'txtDescripcionProducto
        '
        Me.txtDescripcionProducto.Location = New System.Drawing.Point(677, 63)
        Me.txtDescripcionProducto.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtDescripcionProducto.Name = "txtDescripcionProducto"
        Me.txtDescripcionProducto.Properties.Appearance.BackColor = System.Drawing.Color.Wheat
        Me.txtDescripcionProducto.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescripcionProducto.Properties.Appearance.Options.UseBackColor = True
        Me.txtDescripcionProducto.Properties.Appearance.Options.UseFont = True
        Me.txtDescripcionProducto.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtDescripcionProducto.Properties.MaxLength = 50
        Me.txtDescripcionProducto.Size = New System.Drawing.Size(401, 28)
        Me.txtDescripcionProducto.TabIndex = 48
        '
        'txtCantidadRegistrada
        '
        Me.txtCantidadRegistrada.BackColor = System.Drawing.Color.Red
        Me.txtCantidadRegistrada.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCantidadRegistrada.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCantidadRegistrada.LinkColor = System.Drawing.Color.LightGray
        Me.txtCantidadRegistrada.Location = New System.Drawing.Point(1133, 50)
        Me.txtCantidadRegistrada.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.txtCantidadRegistrada.Name = "txtCantidadRegistrada"
        Me.txtCantidadRegistrada.Size = New System.Drawing.Size(110, 45)
        Me.txtCantidadRegistrada.TabIndex = 8
        Me.txtCantidadRegistrada.TabStop = True
        Me.txtCantidadRegistrada.Text = "0"
        Me.txtCantidadRegistrada.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtScanner
        '
        Me.txtScanner.Location = New System.Drawing.Point(31, 63)
        Me.txtScanner.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtScanner.Name = "txtScanner"
        Me.txtScanner.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtScanner.Properties.Appearance.Options.UseFont = True
        Me.txtScanner.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtScanner.Properties.MaxLength = 50
        Me.txtScanner.Size = New System.Drawing.Size(380, 28)
        Me.txtScanner.TabIndex = 42
        '
        'txtCantidadEsperada
        '
        Me.txtCantidadEsperada.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCantidadEsperada.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCantidadEsperada.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCantidadEsperada.LinkColor = System.Drawing.Color.LightGray
        Me.txtCantidadEsperada.Location = New System.Drawing.Point(1268, 50)
        Me.txtCantidadEsperada.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.txtCantidadEsperada.Name = "txtCantidadEsperada"
        Me.txtCantidadEsperada.Size = New System.Drawing.Size(110, 45)
        Me.txtCantidadEsperada.TabIndex = 44
        Me.txtCantidadEsperada.TabStop = True
        Me.txtCantidadEsperada.Text = "0"
        Me.txtCantidadEsperada.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtCantidad
        '
        Me.txtCantidad.Location = New System.Drawing.Point(419, 63)
        Me.txtCantidad.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCantidad.Properties.Appearance.Options.UseFont = True
        Me.txtCantidad.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtCantidad.Properties.MaxLength = 50
        Me.txtCantidad.Size = New System.Drawing.Size(119, 28)
        Me.txtCantidad.TabIndex = 48
        '
        'GridView7
        '
        Me.GridView7.Name = "GridView7"
        '
        'XtratabPagePedido
        '
        Me.XtratabPagePedido.Controls.Add(Me.SplitContainerControl1)
        Me.XtratabPagePedido.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.XtratabPagePedido.Name = "XtratabPagePedido"
        Me.XtratabPagePedido.Padding = New System.Windows.Forms.Padding(12, 12, 12, 12)
        Me.XtratabPagePedido.Size = New System.Drawing.Size(1552, 629)
        Me.XtratabPagePedido.Text = "Pedidos"
        '
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.SplitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl1.Horizontal = False
        Me.SplitContainerControl1.Location = New System.Drawing.Point(12, 12)
        Me.SplitContainerControl1.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        '
        'SplitContainerControl1.Panel1
        '
        Me.SplitContainerControl1.Panel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.GroupControl6)
        Me.SplitContainerControl1.Panel1.Text = "Panel1"
        '
        'SplitContainerControl1.Panel2
        '
        Me.SplitContainerControl1.Panel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.GroupControl1)
        Me.SplitContainerControl1.Panel2.Text = "Panel2"
        Me.SplitContainerControl1.ShowSplitGlyph = DevExpress.Utils.DefaultBoolean.[False]
        Me.SplitContainerControl1.Size = New System.Drawing.Size(1528, 605)
        Me.SplitContainerControl1.SplitterPosition = 169
        Me.SplitContainerControl1.TabIndex = 1
        '
        'GroupControl6
        '
        Me.GroupControl6.Controls.Add(Me.dgridPedidos)
        Me.GroupControl6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl6.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl6.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupControl6.Name = "GroupControl6"
        Me.GroupControl6.Size = New System.Drawing.Size(1520, 165)
        Me.GroupControl6.TabIndex = 0
        Me.GroupControl6.Text = "Encabezado de Pedido"
        '
        'dgridPedidos
        '
        Me.dgridPedidos.AllowUserToResizeRows = False
        Me.dgridPedidos.BackgroundColor = System.Drawing.Color.LightSteelBlue
        Me.dgridPedidos.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 7.8!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.MidnightBlue
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgridPedidos.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgridPedidos.ColumnHeadersHeight = 40
        Me.dgridPedidos.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IdPedido, Me.Referencia, Me.Bodega, Me.Cliente, Me.Propietario, Me.FechaPedido, Me.EstadoP})
        Me.dgridPedidos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridPedidos.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.dgridPedidos.EnableHeadersVisualStyles = False
        Me.dgridPedidos.GridColor = System.Drawing.Color.Navy
        Me.dgridPedidos.Location = New System.Drawing.Point(2, 28)
        Me.dgridPedidos.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dgridPedidos.MultiSelect = False
        Me.dgridPedidos.Name = "dgridPedidos"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 7.8!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgridPedidos.RowHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgridPedidos.RowHeadersVisible = False
        Me.dgridPedidos.RowHeadersWidth = 40
        Me.dgridPedidos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgridPedidos.Size = New System.Drawing.Size(1516, 135)
        Me.dgridPedidos.TabIndex = 1
        '
        'IdPedido
        '
        Me.IdPedido.HeaderText = "Pedido"
        Me.IdPedido.MinimumWidth = 6
        Me.IdPedido.Name = "IdPedido"
        Me.IdPedido.ReadOnly = True
        Me.IdPedido.Width = 125
        '
        'Referencia
        '
        Me.Referencia.HeaderText = "Referencia"
        Me.Referencia.MinimumWidth = 6
        Me.Referencia.Name = "Referencia"
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
        'EstadoP
        '
        Me.EstadoP.HeaderText = "Estado"
        Me.EstadoP.MinimumWidth = 6
        Me.EstadoP.Name = "EstadoP"
        Me.EstadoP.ReadOnly = True
        Me.EstadoP.Width = 125
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Label4)
        Me.GroupControl1.Controls.Add(Me.cmbAgrupamiento)
        Me.GroupControl1.Controls.Add(Me.dgridDetalleManufactura)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1520, 416)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Detalles de Pedidos"
        '
        'cmbAgrupamiento
        '
        Me.cmbAgrupamiento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAgrupamiento.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAgrupamiento.ForeColor = System.Drawing.Color.Navy
        Me.cmbAgrupamiento.Items.AddRange(New Object() {"Consolidado", "Detalle"})
        Me.cmbAgrupamiento.Location = New System.Drawing.Point(71, 36)
        Me.cmbAgrupamiento.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbAgrupamiento.Name = "cmbAgrupamiento"
        Me.cmbAgrupamiento.Size = New System.Drawing.Size(171, 25)
        Me.cmbAgrupamiento.TabIndex = 2
        '
        'dgridDetalleManufactura
        '
        Me.dgridDetalleManufactura.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgridDetalleManufactura.BackgroundColor = System.Drawing.Color.LightSteelBlue
        Me.dgridDetalleManufactura.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.AliceBlue
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 7.8!)
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.MidnightBlue
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgridDetalleManufactura.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgridDetalleManufactura.ColumnHeadersHeight = 40
        Me.dgridDetalleManufactura.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IdPedidoEnc, Me.Codigo, Me.Producto, Me.Presentacion, Me.UnidadMedida, Me.Estado, Me.Cantidad, Me.ClienteDias, Me.CantidadRecibida, Me.OperadorBodega, Me.IdPedidoDet})
        Me.dgridDetalleManufactura.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.dgridDetalleManufactura.EnableHeadersVisualStyles = False
        Me.dgridDetalleManufactura.GridColor = System.Drawing.Color.Navy
        Me.dgridDetalleManufactura.Location = New System.Drawing.Point(6, 74)
        Me.dgridDetalleManufactura.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dgridDetalleManufactura.MultiSelect = False
        Me.dgridDetalleManufactura.Name = "dgridDetalleManufactura"
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Tahoma", 7.8!)
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgridDetalleManufactura.RowHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.dgridDetalleManufactura.RowHeadersVisible = False
        Me.dgridDetalleManufactura.RowHeadersWidth = 40
        Me.dgridDetalleManufactura.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgridDetalleManufactura.Size = New System.Drawing.Size(1509, 331)
        Me.dgridDetalleManufactura.TabIndex = 3
        '
        'IdPedidoEnc
        '
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.Format = "N0"
        Me.IdPedidoEnc.DefaultCellStyle = DataGridViewCellStyle4
        Me.IdPedidoEnc.HeaderText = "Pedido"
        Me.IdPedidoEnc.MinimumWidth = 6
        Me.IdPedidoEnc.Name = "IdPedidoEnc"
        Me.IdPedidoEnc.ReadOnly = True
        Me.IdPedidoEnc.Width = 125
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
        'Cantidad
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.Format = "N2"
        DataGridViewCellStyle5.NullValue = Nothing
        Me.Cantidad.DefaultCellStyle = DataGridViewCellStyle5
        Me.Cantidad.HeaderText = "Cantidad"
        Me.Cantidad.MinimumWidth = 6
        Me.Cantidad.Name = "Cantidad"
        Me.Cantidad.ReadOnly = True
        Me.Cantidad.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Cantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Cantidad.Width = 125
        '
        'ClienteDias
        '
        Me.ClienteDias.HeaderText = "Cliente Días"
        Me.ClienteDias.MinimumWidth = 6
        Me.ClienteDias.Name = "ClienteDias"
        Me.ClienteDias.Width = 125
        '
        'CantidadRecibida
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.Format = "N2"
        DataGridViewCellStyle6.NullValue = "0.0"
        Me.CantidadRecibida.DefaultCellStyle = DataGridViewCellStyle6
        Me.CantidadRecibida.HeaderText = "Cantidad Recibida"
        Me.CantidadRecibida.MinimumWidth = 6
        Me.CantidadRecibida.Name = "CantidadRecibida"
        Me.CantidadRecibida.ReadOnly = True
        Me.CantidadRecibida.Width = 125
        '
        'OperadorBodega
        '
        Me.OperadorBodega.HeaderText = "Operador"
        Me.OperadorBodega.MinimumWidth = 6
        Me.OperadorBodega.Name = "OperadorBodega"
        Me.OperadorBodega.Width = 125
        '
        'IdPedidoDet
        '
        Me.IdPedidoDet.HeaderText = "IdPedidoDet"
        Me.IdPedidoDet.MinimumWidth = 6
        Me.IdPedidoDet.Name = "IdPedidoDet"
        Me.IdPedidoDet.ReadOnly = True
        Me.IdPedidoDet.Visible = False
        Me.IdPedidoDet.Width = 125
        '
        'XtratabPageDato
        '
        Me.XtratabPageDato.Appearance.PageClient.BackColor = System.Drawing.SystemColors.ControlDark
        Me.XtratabPageDato.Appearance.PageClient.Options.UseBackColor = True
        Me.XtratabPageDato.Controls.Add(Me.GroupControl10)
        Me.XtratabPageDato.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.XtratabPageDato.Name = "XtratabPageDato"
        Me.XtratabPageDato.Size = New System.Drawing.Size(1552, 629)
        Me.XtratabPageDato.Text = "General"
        '
        'GroupControl10
        '
        Me.GroupControl10.Controls.Add(Me.GroupControl5)
        Me.GroupControl10.Controls.Add(Me.GrpTarea)
        Me.GroupControl10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl10.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl10.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupControl10.Name = "GroupControl10"
        Me.GroupControl10.ShowCaption = False
        Me.GroupControl10.Size = New System.Drawing.Size(1552, 629)
        Me.GroupControl10.TabIndex = 0
        Me.GroupControl10.Text = "Datos"
        '
        'GroupControl5
        '
        Me.GroupControl5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.GroupControl5.Controls.Add(Me.cmbTipoManufactura)
        Me.GroupControl5.Controls.Add(Label3)
        Me.GroupControl5.Controls.Add(Me.dtmFechaManufactura)
        Me.GroupControl5.Controls.Add(Me.cmbBodegas)
        Me.GroupControl5.Controls.Add(Me.cmbPropietario)
        Me.GroupControl5.Controls.Add(Me.lblEstado)
        Me.GroupControl5.Controls.Add(Label10)
        Me.GroupControl5.Controls.Add(Label12)
        Me.GroupControl5.Controls.Add(Label2)
        Me.GroupControl5.Controls.Add(IdPropietarioLabel)
        Me.GroupControl5.Controls.Add(Me.lblCodigo)
        Me.GroupControl5.Controls.Add(Label1)
        Me.GroupControl5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl5.Location = New System.Drawing.Point(2, 2)
        Me.GroupControl5.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.GroupControl5.Name = "GroupControl5"
        Me.GroupControl5.Size = New System.Drawing.Size(1142, 625)
        Me.GroupControl5.TabIndex = 20
        Me.GroupControl5.Text = "Generales"
        '
        'cmbTipoManufactura
        '
        Me.cmbTipoManufactura.Location = New System.Drawing.Point(155, 121)
        Me.cmbTipoManufactura.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbTipoManufactura.MenuManager = Me.RibbonControl
        Me.cmbTipoManufactura.Name = "cmbTipoManufactura"
        Me.cmbTipoManufactura.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTipoManufactura.Properties.NullText = ""
        Me.cmbTipoManufactura.Size = New System.Drawing.Size(329, 22)
        Me.cmbTipoManufactura.TabIndex = 21
        '
        'dtmFechaManufactura
        '
        Me.dtmFechaManufactura.EditValue = New Date(2017, 11, 20, 9, 15, 49, 963)
        Me.dtmFechaManufactura.Location = New System.Drawing.Point(158, 153)
        Me.dtmFechaManufactura.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dtmFechaManufactura.MenuManager = Me.RibbonControl
        Me.dtmFechaManufactura.Name = "dtmFechaManufactura"
        Me.dtmFechaManufactura.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtmFechaManufactura.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtmFechaManufactura.Size = New System.Drawing.Size(329, 22)
        Me.dtmFechaManufactura.TabIndex = 19
        '
        'cmbBodegas
        '
        Me.cmbBodegas.Location = New System.Drawing.Point(158, 185)
        Me.cmbBodegas.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbBodegas.MenuManager = Me.RibbonControl
        Me.cmbBodegas.Name = "cmbBodegas"
        Me.cmbBodegas.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodegas.Properties.NullText = ""
        Me.cmbBodegas.Size = New System.Drawing.Size(329, 22)
        Me.cmbBodegas.TabIndex = 18
        '
        'cmbPropietario
        '
        Me.cmbPropietario.Location = New System.Drawing.Point(158, 218)
        Me.cmbPropietario.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbPropietario.MenuManager = Me.RibbonControl
        Me.cmbPropietario.Name = "cmbPropietario"
        Me.cmbPropietario.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPropietario.Properties.NullText = ""
        Me.cmbPropietario.Size = New System.Drawing.Size(329, 22)
        Me.cmbPropietario.TabIndex = 17
        '
        'lblEstado
        '
        Me.lblEstado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblEstado.Location = New System.Drawing.Point(158, 87)
        Me.lblEstado.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblEstado.Name = "lblEstado"
        Me.lblEstado.Size = New System.Drawing.Size(328, 20)
        Me.lblEstado.TabIndex = 3
        '
        'lblCodigo
        '
        Me.lblCodigo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCodigo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCodigo.Location = New System.Drawing.Point(158, 53)
        Me.lblCodigo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCodigo.Name = "lblCodigo"
        Me.lblCodigo.Size = New System.Drawing.Size(328, 20)
        Me.lblCodigo.TabIndex = 1
        '
        'GrpTarea
        '
        Me.GrpTarea.Controls.Add(Me.dtmFechaTarea)
        Me.GrpTarea.Controls.Add(Me.GroupControl4)
        Me.GrpTarea.Controls.Add(Me.GroupControl9)
        Me.GrpTarea.Dock = System.Windows.Forms.DockStyle.Right
        Me.GrpTarea.Location = New System.Drawing.Point(1144, 2)
        Me.GrpTarea.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GrpTarea.Name = "GrpTarea"
        Me.GrpTarea.Size = New System.Drawing.Size(406, 625)
        Me.GrpTarea.TabIndex = 15
        Me.GrpTarea.Text = "Fecha Tarea"
        '
        'dtmFechaTarea
        '
        Me.dtmFechaTarea.EditValue = New Date(2017, 11, 20, 9, 17, 59, 855)
        Me.dtmFechaTarea.Location = New System.Drawing.Point(8, 41)
        Me.dtmFechaTarea.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dtmFechaTarea.MenuManager = Me.RibbonControl
        Me.dtmFechaTarea.Name = "dtmFechaTarea"
        Me.dtmFechaTarea.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtmFechaTarea.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtmFechaTarea.Size = New System.Drawing.Size(232, 22)
        Me.dtmFechaTarea.TabIndex = 20
        '
        'GroupControl4
        '
        Me.GroupControl4.Controls.Add(Label37)
        Me.GroupControl4.Controls.Add(Me.dtmHoraFhh)
        Me.GroupControl4.Controls.Add(Label38)
        Me.GroupControl4.Controls.Add(Me.dtmHoraIhh)
        Me.GroupControl4.Enabled = False
        Me.GroupControl4.Location = New System.Drawing.Point(247, 71)
        Me.GroupControl4.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
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
        Me.dtmHoraFhh.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
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
        Me.dtmHoraIhh.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
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
        Me.GroupControl9.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
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
        Me.dtmHoraF.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
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
        Me.dtmHoraI.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dtmHoraI.Name = "dtmHoraI"
        Me.dtmHoraI.ShowUpDown = True
        Me.dtmHoraI.Size = New System.Drawing.Size(131, 23)
        Me.dtmHoraI.TabIndex = 1
        '
        'XtraTabControl1
        '
        Me.XtraTabControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.XtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XtraTabControl1.HeaderAutoFill = DevExpress.Utils.DefaultBoolean.[False]
        Me.XtraTabControl1.Location = New System.Drawing.Point(0, 193)
        Me.XtraTabControl1.Margin = New System.Windows.Forms.Padding(12, 12, 12, 12)
        Me.XtraTabControl1.MaxTabPageWidth = 100
        Me.XtraTabControl1.Name = "XtraTabControl1"
        Me.XtraTabControl1.Padding = New System.Windows.Forms.Padding(12, 12, 12, 12)
        Me.XtraTabControl1.SelectedTabPage = Me.XtratabPageDato
        Me.XtraTabControl1.Size = New System.Drawing.Size(1554, 659)
        Me.XtraTabControl1.TabIndex = 4
        Me.XtraTabControl1.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.XtratabPageDato, Me.XtratabPagePedido, Me.XtraTabPageUbicacionPicking})
        '
        'DxErrorProvider1
        '
        Me.DxErrorProvider1.ContainerControl = Me
        '
        'frmManufactura
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1554, 882)
        Me.Controls.Add(Me.XtraTabControl1)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "frmManufactura"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Manufactura"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsOrdenCompraRecepcionOperador, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dkRecepcion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabPageUbicacionPicking.ResumeLayout(False)
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.dgridPickingUbic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdvPickingUbic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpScan, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpScan.ResumeLayout(False)
        Me.grpScan.PerformLayout()
        CType(Me.txtLote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescripcionProducto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtScanner.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCantidad.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtratabPagePedido.ResumeLayout(False)
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel1.ResumeLayout(False)
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        CType(Me.GroupControl6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl6.ResumeLayout(False)
        CType(Me.dgridPedidos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.dgridDetalleManufactura, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtratabPageDato.ResumeLayout(False)
        CType(Me.GroupControl10, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl10.ResumeLayout(False)
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl5.ResumeLayout(False)
        Me.GroupControl5.PerformLayout()
        CType(Me.cmbTipoManufactura.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtmFechaManufactura.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtmFechaManufactura.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBodegas.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpTarea, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpTarea.ResumeLayout(False)
        CType(Me.dtmFechaTarea.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtmFechaTarea.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl4.ResumeLayout(False)
        Me.GroupControl4.PerformLayout()
        CType(Me.GroupControl9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl9.ResumeLayout(False)
        Me.GroupControl9.PerformLayout()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabControl1.ResumeLayout(False)
        CType(Me.DxErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents mnuEliminar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents cmdGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuAsignacion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem3 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdCodigoBarra As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdImprimeCodigoBarra As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdImprmirCodigoBarra As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdEliminar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdUbicacion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonPage3 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup3 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView4 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cmdImprimir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents SubImprimir As DevExpress.XtraBars.BarSubItem
    Friend WithEvents cmdPreIngreso As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdCostoArancel As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents Bar2 As DevExpress.XtraBars.Bar
    Friend WithEvents DataBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DsOrdenCompraRecepcionOperador As DsOrdenCompraRecepcionOperador
    Friend WithEvents dkRecepcion As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents lblRegs As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents tmrActualizarDatosRecepcion As Timer
    Friend WithEvents lnkAgregarPedido As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents XtraTabControl1 As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents XtratabPageDato As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GroupControl5 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dtmFechaManufactura As DevExpress.XtraEditors.DateEdit
    Friend WithEvents cmbBodegas As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbPropietario As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblEstado As Label
    Friend WithEvents lblCodigo As Label
    Friend WithEvents GrpTarea As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dtmFechaTarea As DevExpress.XtraEditors.DateEdit
    Friend WithEvents GroupControl4 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dtmHoraFhh As DateTimePicker
    Friend WithEvents dtmHoraIhh As DateTimePicker
    Friend WithEvents GroupControl9 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dtmHoraF As DateTimePicker
    Friend WithEvents dtmHoraI As DateTimePicker
    Friend WithEvents XtratabPagePedido As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents GroupControl6 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dgridPedidos As DataGridView
    Friend WithEvents IdPedido As DataGridViewTextBoxColumn
    Friend WithEvents Referencia As DataGridViewTextBoxColumn
    Friend WithEvents Bodega As DataGridViewTextBoxColumn
    Friend WithEvents Cliente As DataGridViewTextBoxColumn
    Friend WithEvents Propietario As DataGridViewTextBoxColumn
    Friend WithEvents FechaPedido As DataGridViewTextBoxColumn
    Friend WithEvents EstadoP As DataGridViewTextBoxColumn
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cmbAgrupamiento As ComboBox
    Friend WithEvents dgridDetalleManufactura As DataGridView
    Friend WithEvents IdPedidoEnc As DataGridViewTextBoxColumn
    Friend WithEvents Codigo As DataGridViewTextBoxColumn
    Friend WithEvents Producto As DataGridViewTextBoxColumn
    Friend WithEvents Presentacion As DataGridViewTextBoxColumn
    Friend WithEvents UnidadMedida As DataGridViewTextBoxColumn
    Friend WithEvents Estado As DataGridViewTextBoxColumn
    Friend WithEvents Cantidad As DataGridViewTextBoxColumn
    Friend WithEvents ClienteDias As DataGridViewTextBoxColumn
    Friend WithEvents CantidadRecibida As DataGridViewTextBoxColumn
    Friend WithEvents OperadorBodega As DataGridViewComboBoxColumn
    Friend WithEvents IdPedidoDet As DataGridViewTextBoxColumn
    Friend WithEvents XtraTabPageUbicacionPicking As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents dgridPickingUbic As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdvPickingUbic As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView6 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView7 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cmbTipoManufactura As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grpScan As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtScanner As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCantidad As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCantidadRegistrada As LinkLabel
    Friend WithEvents txtCantidadEsperada As LinkLabel
    Friend WithEvents txtDescripcionProducto As DevExpress.XtraEditors.TextEdit
    Friend WithEvents DxErrorProvider1 As DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider
    Friend WithEvents txtLote As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblRegistros As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdCerrar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GroupControl10 As DevExpress.XtraEditors.GroupControl
End Class
