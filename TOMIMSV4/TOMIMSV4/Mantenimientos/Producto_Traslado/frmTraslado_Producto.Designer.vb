<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmTraslado_Producto
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
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
        Dim User_agrLabel As System.Windows.Forms.Label
        Dim Fec_agrLabel As System.Windows.Forms.Label
        Dim User_modLabel As System.Windows.Forms.Label
        Dim Fec_modLabel As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim IdEmpresaLabel As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim Label10 As System.Windows.Forms.Label
        Dim Label5 As System.Windows.Forms.Label
        Dim Label6 As System.Windows.Forms.Label
        Dim Label7 As System.Windows.Forms.Label
        Dim Label13 As System.Windows.Forms.Label
        Dim Label8 As System.Windows.Forms.Label
        Dim Label9 As System.Windows.Forms.Label
        Dim Label11 As System.Windows.Forms.Label
        Dim Label12 As System.Windows.Forms.Label
        Dim Label14 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTraslado_Producto))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.User_agrTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_agrDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_modTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_modDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.DataBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsPropietario = New DsPropietario()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.txtEstado = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.txtNoDocumento = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.txtID = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.dtpFechaEntrega = New DevExpress.XtraEditors.DateEdit()
        Me.dtpFechaTraslado = New DevExpress.XtraEditors.DateEdit()
        Me.cmbMuelle = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbPropietario = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbEmpresa = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbBodegaDesti = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbBodegaOrigen = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtIdRuta = New DevExpress.XtraEditors.TextEdit()
        Me.txtNombreRuta = New DevExpress.XtraEditors.TextEdit()
        Me.lnkidRuta = New System.Windows.Forms.LinkLabel()
        Me.dtpHoraMaximaEntrega = New System.Windows.Forms.DateTimePicker()
        Me.dtpHoraInicialEntrega = New System.Windows.Forms.DateTimePicker()
        Me.dtpHoraFinalTraslado = New System.Windows.Forms.DateTimePicker()
        Me.dtpHoraInicialTraslado = New System.Windows.Forms.DateTimePicker()
        Me.txtNoGuia = New DevExpress.XtraEditors.TextEdit()
        Me.txtIdPiloto = New DevExpress.XtraEditors.TextEdit()
        Me.txtNombrePiloto = New DevExpress.XtraEditors.TextEdit()
        Me.lnkidPiloto = New System.Windows.Forms.LinkLabel()
        Me.txtIdVehiculo = New DevExpress.XtraEditors.TextEdit()
        Me.txtNombreVehiculo = New DevExpress.XtraEditors.TextEdit()
        Me.lnkidVehiculo = New System.Windows.Forms.LinkLabel()
        Me.txtObservacion = New DevExpress.XtraEditors.MemoEdit()
        Me.cbxAnularTraslado = New DevExpress.XtraEditors.CheckEdit()
        Me.cbxLocal = New DevExpress.XtraEditors.CheckEdit()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.dgrid = New System.Windows.Forms.DataGridView()
        Me.colIdProducto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colIsNew = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColCodProducto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColNomProducto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colUnidadMedida = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPresentacion = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.colEstadoProducto = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.colCantidadExistencia = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPesoExistencia = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColCantidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColPeso = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColPrecio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColTotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IdLinea = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lnkAgregarProducto = New System.Windows.Forms.LinkLabel()
        Me.dkTraslado_Producto = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.xtraTraslado_Producto = New DevExpress.XtraTab.XtraTabControl()
        Me.Traslado = New DevExpress.XtraTab.XtraTabPage()
        Me.DetalleTraslado = New DevExpress.XtraTab.XtraTabPage()
        User_agrLabel = New System.Windows.Forms.Label()
        Fec_agrLabel = New System.Windows.Forms.Label()
        User_modLabel = New System.Windows.Forms.Label()
        Fec_modLabel = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        IdEmpresaLabel = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Label10 = New System.Windows.Forms.Label()
        Label5 = New System.Windows.Forms.Label()
        Label6 = New System.Windows.Forms.Label()
        Label7 = New System.Windows.Forms.Label()
        Label13 = New System.Windows.Forms.Label()
        Label8 = New System.Windows.Forms.Label()
        Label9 = New System.Windows.Forms.Label()
        Label11 = New System.Windows.Forms.Label()
        Label12 = New System.Windows.Forms.Label()
        Label14 = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsPropietario, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.txtNoDocumento.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtID.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.dtpFechaEntrega.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFechaEntrega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFechaTraslado.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFechaTraslado.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbMuelle.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbEmpresa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBodegaDesti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBodegaOrigen.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdRuta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreRuta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoGuia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdPiloto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombrePiloto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdVehiculo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreVehiculo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtObservacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbxAnularTraslado.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbxLocal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        CType(Me.dgrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dkTraslado_Producto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerBottom.SuspendLayout()
        Me.DockPanel1.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.xtraTraslado_Producto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtraTraslado_Producto.SuspendLayout()
        Me.Traslado.SuspendLayout()
        Me.DetalleTraslado.SuspendLayout()
        Me.SuspendLayout()
        '
        'User_agrLabel
        '
        User_agrLabel.AutoSize = True
        User_agrLabel.Location = New System.Drawing.Point(10, 18)
        User_agrLabel.Name = "User_agrLabel"
        User_agrLabel.Size = New System.Drawing.Size(106, 17)
        User_agrLabel.TabIndex = 0
        User_agrLabel.Text = "Usuario Agregó:"
        '
        'Fec_agrLabel
        '
        Fec_agrLabel.AutoSize = True
        Fec_agrLabel.Location = New System.Drawing.Point(10, 50)
        Fec_agrLabel.Name = "Fec_agrLabel"
        Fec_agrLabel.Size = New System.Drawing.Size(97, 17)
        Fec_agrLabel.TabIndex = 4
        Fec_agrLabel.Text = "Fecha Agregó:"
        '
        'User_modLabel
        '
        User_modLabel.AutoSize = True
        User_modLabel.Location = New System.Drawing.Point(520, 18)
        User_modLabel.Name = "User_modLabel"
        User_modLabel.Size = New System.Drawing.Size(111, 17)
        User_modLabel.TabIndex = 2
        User_modLabel.Text = "Usuario Modificó:"
        '
        'Fec_modLabel
        '
        Fec_modLabel.AutoSize = True
        Fec_modLabel.Location = New System.Drawing.Point(520, 50)
        Fec_modLabel.Name = "Fec_modLabel"
        Fec_modLabel.Size = New System.Drawing.Size(102, 17)
        Fec_modLabel.TabIndex = 6
        Fec_modLabel.Text = "Fecha Modificó:"
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(34, 85)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(102, 17)
        Label4.TabIndex = 2
        Label4.Text = " Bodega Origen"
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(34, 128)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(77, 17)
        Label1.TabIndex = 9
        Label1.Text = " Propietario"
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(34, 171)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(44, 17)
        Label2.TabIndex = 11
        Label2.Text = "Muelle"
        '
        'IdEmpresaLabel
        '
        IdEmpresaLabel.AutoSize = True
        IdEmpresaLabel.Location = New System.Drawing.Point(34, 42)
        IdEmpresaLabel.Name = "IdEmpresaLabel"
        IdEmpresaLabel.Size = New System.Drawing.Size(61, 17)
        IdEmpresaLabel.TabIndex = 0
        IdEmpresaLabel.Text = "Empresa"
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.Location = New System.Drawing.Point(483, 126)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(104, 17)
        Label3.TabIndex = 7
        Label3.Text = "Bodega Destino"
        '
        'Label10
        '
        Label10.AutoSize = True
        Label10.Location = New System.Drawing.Point(38, 402)
        Label10.Name = "Label10"
        Label10.Size = New System.Drawing.Size(84, 17)
        Label10.TabIndex = 37
        Label10.Text = "Observacion"
        '
        'Label5
        '
        Label5.AutoSize = True
        Label5.Location = New System.Drawing.Point(34, 290)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(120, 17)
        Label5.TabIndex = 25
        Label5.Text = "Fecha del Traslado"
        '
        'Label6
        '
        Label6.AutoSize = True
        Label6.Location = New System.Drawing.Point(34, 331)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(73, 17)
        Label6.TabIndex = 29
        Label6.Text = "Hora Inicial"
        '
        'Label7
        '
        Label7.AutoSize = True
        Label7.Location = New System.Drawing.Point(34, 367)
        Label7.Name = "Label7"
        Label7.Size = New System.Drawing.Size(67, 17)
        Label7.TabIndex = 33
        Label7.Text = "Hora Final"
        '
        'Label13
        '
        Label13.AutoSize = True
        Label13.Location = New System.Drawing.Point(483, 85)
        Label13.Name = "Label13"
        Label13.Size = New System.Drawing.Size(106, 17)
        Label13.TabIndex = 4
        Label13.Text = "Numero de Guia"
        '
        'Label8
        '
        Label8.AutoSize = True
        Label8.Location = New System.Drawing.Point(488, 367)
        Label8.Name = "Label8"
        Label8.Size = New System.Drawing.Size(176, 17)
        Label8.TabIndex = 35
        Label8.Text = "Hora maxima para entregar"
        '
        'Label9
        '
        Label9.AutoSize = True
        Label9.Location = New System.Drawing.Point(488, 331)
        Label9.Name = "Label9"
        Label9.Size = New System.Drawing.Size(160, 17)
        Label9.TabIndex = 31
        Label9.Text = "Hora Inicial para entregar"
        '
        'Label11
        '
        Label11.AutoSize = True
        Label11.Location = New System.Drawing.Point(488, 290)
        Label11.Name = "Label11"
        Label11.Size = New System.Drawing.Size(115, 17)
        Label11.TabIndex = 27
        Label11.Text = "Fecha de Entrega"
        '
        'Label12
        '
        Label12.AutoSize = True
        Label12.Location = New System.Drawing.Point(38, 207)
        Label12.Name = "Label12"
        Label12.Size = New System.Drawing.Size(77, 17)
        Label12.TabIndex = 15
        Label12.Text = "Envio Local"
        '
        'Label14
        '
        Label14.AutoSize = True
        Label14.Location = New System.Drawing.Point(488, 245)
        Label14.Name = "Label14"
        Label14.Size = New System.Drawing.Size(101, 17)
        Label14.TabIndex = 23
        Label14.Text = "Anular Traslado"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.RibbonControl.SearchEditItem, Me.mnuGuardar, Me.mnuActualizar, Me.mnuEliminar})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 5
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(940, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'mnuGuardar
        '
        Me.mnuGuardar.Caption = "Guardar"
        Me.mnuGuardar.Id = 1
        Me.mnuGuardar.ImageOptions.SvgImage = CType(resources.GetObject("mnuGuardar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuGuardar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.G))
        Me.mnuGuardar.Name = "mnuGuardar"
        '
        'mnuActualizar
        '
        Me.mnuActualizar.Caption = "Actualizar"
        Me.mnuActualizar.Id = 2
        Me.mnuActualizar.ImageOptions.SvgImage = CType(resources.GetObject("mnuActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuActualizar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A))
        Me.mnuActualizar.Name = "mnuActualizar"
        '
        'mnuEliminar
        '
        Me.mnuEliminar.Caption = "Eliminar"
        Me.mnuEliminar.Id = 3
        Me.mnuEliminar.ImageOptions.SvgImage = CType(resources.GetObject("mnuEliminar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEliminar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E))
        Me.mnuEliminar.Name = "mnuEliminar"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Menu"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEliminar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 841)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(940, 30)
        '
        'User_agrTextEdit
        '
        Me.User_agrTextEdit.Enabled = False
        Me.User_agrTextEdit.Location = New System.Drawing.Point(147, 15)
        Me.User_agrTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.User_agrTextEdit.MenuManager = Me.RibbonControl
        Me.User_agrTextEdit.Name = "User_agrTextEdit"
        Me.User_agrTextEdit.Size = New System.Drawing.Size(264, 22)
        Me.User_agrTextEdit.TabIndex = 1
        '
        'Fec_agrDateEdit
        '
        Me.Fec_agrDateEdit.EditValue = Nothing
        Me.Fec_agrDateEdit.Enabled = False
        Me.Fec_agrDateEdit.Location = New System.Drawing.Point(147, 47)
        Me.Fec_agrDateEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Fec_agrDateEdit.MenuManager = Me.RibbonControl
        Me.Fec_agrDateEdit.Name = "Fec_agrDateEdit"
        Me.Fec_agrDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit.Size = New System.Drawing.Size(264, 22)
        Me.Fec_agrDateEdit.TabIndex = 5
        '
        'User_modTextEdit
        '
        Me.User_modTextEdit.Enabled = False
        Me.User_modTextEdit.Location = New System.Drawing.Point(657, 15)
        Me.User_modTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.User_modTextEdit.MenuManager = Me.RibbonControl
        Me.User_modTextEdit.Name = "User_modTextEdit"
        Me.User_modTextEdit.Size = New System.Drawing.Size(264, 22)
        Me.User_modTextEdit.TabIndex = 3
        '
        'Fec_modDateEdit
        '
        Me.Fec_modDateEdit.EditValue = Nothing
        Me.Fec_modDateEdit.Enabled = False
        Me.Fec_modDateEdit.Location = New System.Drawing.Point(657, 47)
        Me.Fec_modDateEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Fec_modDateEdit.MenuManager = Me.RibbonControl
        Me.Fec_modDateEdit.Name = "Fec_modDateEdit"
        Me.Fec_modDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit.Size = New System.Drawing.Size(264, 22)
        Me.Fec_modDateEdit.TabIndex = 7
        '
        'DataBindingSource
        '
        Me.DataBindingSource.DataMember = "Data"
        Me.DataBindingSource.DataSource = Me.DsPropietario
        '
        'DsPropietario
        '
        Me.DsPropietario.DataSetName = "DsPropietario"
        Me.DsPropietario.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.txtEstado)
        Me.PanelControl1.Controls.Add(Me.LabelControl2)
        Me.PanelControl1.Controls.Add(Me.txtNoDocumento)
        Me.PanelControl1.Controls.Add(Me.LabelControl1)
        Me.PanelControl1.Controls.Add(Me.txtID)
        Me.PanelControl1.Controls.Add(Me.LabelControl5)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 193)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(940, 50)
        Me.PanelControl1.TabIndex = 0
        '
        'txtEstado
        '
        Me.txtEstado.Appearance.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEstado.Appearance.ForeColor = System.Drawing.Color.Red
        Me.txtEstado.Appearance.Options.UseFont = True
        Me.txtEstado.Appearance.Options.UseForeColor = True
        Me.txtEstado.Location = New System.Drawing.Point(789, 10)
        Me.txtEstado.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtEstado.Name = "txtEstado"
        Me.txtEstado.Size = New System.Drawing.Size(108, 31)
        Me.txtEstado.TabIndex = 5
        Me.txtEstado.Text = "Anulado"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(743, 25)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(38, 16)
        Me.LabelControl2.TabIndex = 4
        Me.LabelControl2.Text = "Estado"
        '
        'txtNoDocumento
        '
        Me.txtNoDocumento.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNoDocumento.Enabled = False
        Me.txtNoDocumento.Location = New System.Drawing.Point(392, 16)
        Me.txtNoDocumento.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNoDocumento.MenuManager = Me.RibbonControl
        Me.txtNoDocumento.Name = "txtNoDocumento"
        Me.txtNoDocumento.Size = New System.Drawing.Size(211, 22)
        Me.txtNoDocumento.TabIndex = 3
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(303, 20)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(83, 16)
        Me.LabelControl1.TabIndex = 2
        Me.LabelControl1.Text = "No Documento"
        '
        'txtID
        '
        Me.txtID.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtID.Enabled = False
        Me.txtID.Location = New System.Drawing.Point(50, 15)
        Me.txtID.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtID.MenuManager = Me.RibbonControl
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(105, 22)
        Me.txtID.TabIndex = 1
        '
        'LabelControl5
        '
        Me.LabelControl5.Location = New System.Drawing.Point(30, 20)
        Me.LabelControl5.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(12, 16)
        Me.LabelControl5.TabIndex = 0
        Me.LabelControl5.Text = "ID"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.dtpFechaEntrega)
        Me.GroupControl1.Controls.Add(Me.dtpFechaTraslado)
        Me.GroupControl1.Controls.Add(Me.cmbMuelle)
        Me.GroupControl1.Controls.Add(Me.cmbPropietario)
        Me.GroupControl1.Controls.Add(Me.cmbEmpresa)
        Me.GroupControl1.Controls.Add(Me.cmbBodegaDesti)
        Me.GroupControl1.Controls.Add(Me.cmbBodegaOrigen)
        Me.GroupControl1.Controls.Add(Me.txtIdRuta)
        Me.GroupControl1.Controls.Add(Me.txtNombreRuta)
        Me.GroupControl1.Controls.Add(Me.lnkidRuta)
        Me.GroupControl1.Controls.Add(Label14)
        Me.GroupControl1.Controls.Add(Label12)
        Me.GroupControl1.Controls.Add(Me.dtpHoraMaximaEntrega)
        Me.GroupControl1.Controls.Add(Me.dtpHoraInicialEntrega)
        Me.GroupControl1.Controls.Add(Label8)
        Me.GroupControl1.Controls.Add(Label9)
        Me.GroupControl1.Controls.Add(Label11)
        Me.GroupControl1.Controls.Add(Me.dtpHoraFinalTraslado)
        Me.GroupControl1.Controls.Add(Me.dtpHoraInicialTraslado)
        Me.GroupControl1.Controls.Add(Me.txtNoGuia)
        Me.GroupControl1.Controls.Add(Label13)
        Me.GroupControl1.Controls.Add(Label7)
        Me.GroupControl1.Controls.Add(Label6)
        Me.GroupControl1.Controls.Add(Me.txtIdPiloto)
        Me.GroupControl1.Controls.Add(Label5)
        Me.GroupControl1.Controls.Add(Me.txtNombrePiloto)
        Me.GroupControl1.Controls.Add(Me.lnkidPiloto)
        Me.GroupControl1.Controls.Add(Me.txtIdVehiculo)
        Me.GroupControl1.Controls.Add(Me.txtNombreVehiculo)
        Me.GroupControl1.Controls.Add(Me.lnkidVehiculo)
        Me.GroupControl1.Controls.Add(Me.txtObservacion)
        Me.GroupControl1.Controls.Add(Label10)
        Me.GroupControl1.Controls.Add(Label3)
        Me.GroupControl1.Controls.Add(IdEmpresaLabel)
        Me.GroupControl1.Controls.Add(Me.cbxAnularTraslado)
        Me.GroupControl1.Controls.Add(Me.cbxLocal)
        Me.GroupControl1.Controls.Add(Label2)
        Me.GroupControl1.Controls.Add(Label1)
        Me.GroupControl1.Controls.Add(Label4)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(938, 542)
        Me.GroupControl1.TabIndex = 0
        '
        'dtpFechaEntrega
        '
        Me.dtpFechaEntrega.EditValue = New Date(2017, 11, 20, 10, 18, 30, 138)
        Me.dtpFechaEntrega.Location = New System.Drawing.Point(651, 282)
        Me.dtpFechaEntrega.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtpFechaEntrega.MenuManager = Me.RibbonControl
        Me.dtpFechaEntrega.Name = "dtpFechaEntrega"
        Me.dtpFechaEntrega.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpFechaEntrega.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpFechaEntrega.Size = New System.Drawing.Size(252, 22)
        Me.dtpFechaEntrega.TabIndex = 45
        '
        'dtpFechaTraslado
        '
        Me.dtpFechaTraslado.EditValue = New Date(2017, 11, 20, 10, 17, 46, 319)
        Me.dtpFechaTraslado.Location = New System.Drawing.Point(170, 282)
        Me.dtpFechaTraslado.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtpFechaTraslado.MenuManager = Me.RibbonControl
        Me.dtpFechaTraslado.Name = "dtpFechaTraslado"
        Me.dtpFechaTraslado.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpFechaTraslado.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpFechaTraslado.Size = New System.Drawing.Size(252, 22)
        Me.dtpFechaTraslado.TabIndex = 44
        '
        'cmbMuelle
        '
        Me.cmbMuelle.Location = New System.Drawing.Point(170, 161)
        Me.cmbMuelle.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbMuelle.MenuManager = Me.RibbonControl
        Me.cmbMuelle.Name = "cmbMuelle"
        Me.cmbMuelle.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbMuelle.Properties.NullText = ""
        Me.cmbMuelle.Size = New System.Drawing.Size(252, 22)
        Me.cmbMuelle.TabIndex = 43
        '
        'cmbPropietario
        '
        Me.cmbPropietario.Location = New System.Drawing.Point(170, 116)
        Me.cmbPropietario.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbPropietario.MenuManager = Me.RibbonControl
        Me.cmbPropietario.Name = "cmbPropietario"
        Me.cmbPropietario.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPropietario.Properties.NullText = ""
        Me.cmbPropietario.Size = New System.Drawing.Size(252, 22)
        Me.cmbPropietario.TabIndex = 42
        '
        'cmbEmpresa
        '
        Me.cmbEmpresa.Location = New System.Drawing.Point(170, 44)
        Me.cmbEmpresa.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbEmpresa.MenuManager = Me.RibbonControl
        Me.cmbEmpresa.Name = "cmbEmpresa"
        Me.cmbEmpresa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbEmpresa.Properties.NullText = ""
        Me.cmbEmpresa.Size = New System.Drawing.Size(252, 22)
        Me.cmbEmpresa.TabIndex = 41
        '
        'cmbBodegaDesti
        '
        Me.cmbBodegaDesti.Location = New System.Drawing.Point(651, 119)
        Me.cmbBodegaDesti.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbBodegaDesti.MenuManager = Me.RibbonControl
        Me.cmbBodegaDesti.Name = "cmbBodegaDesti"
        Me.cmbBodegaDesti.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodegaDesti.Properties.NullText = ""
        Me.cmbBodegaDesti.Size = New System.Drawing.Size(252, 22)
        Me.cmbBodegaDesti.TabIndex = 40
        '
        'cmbBodegaOrigen
        '
        Me.cmbBodegaOrigen.Location = New System.Drawing.Point(170, 76)
        Me.cmbBodegaOrigen.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbBodegaOrigen.MenuManager = Me.RibbonControl
        Me.cmbBodegaOrigen.Name = "cmbBodegaOrigen"
        Me.cmbBodegaOrigen.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodegaOrigen.Properties.NullText = ""
        Me.cmbBodegaOrigen.Size = New System.Drawing.Size(252, 22)
        Me.cmbBodegaOrigen.TabIndex = 39
        '
        'txtIdRuta
        '
        Me.txtIdRuta.Location = New System.Drawing.Point(93, 241)
        Me.txtIdRuta.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtIdRuta.MenuManager = Me.RibbonControl
        Me.txtIdRuta.Name = "txtIdRuta"
        Me.txtIdRuta.Properties.Mask.EditMask = "n0"
        Me.txtIdRuta.Size = New System.Drawing.Size(69, 22)
        Me.txtIdRuta.TabIndex = 21
        '
        'txtNombreRuta
        '
        Me.txtNombreRuta.Location = New System.Drawing.Point(169, 241)
        Me.txtNombreRuta.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNombreRuta.MenuManager = Me.RibbonControl
        Me.txtNombreRuta.Name = "txtNombreRuta"
        Me.txtNombreRuta.Properties.ReadOnly = True
        Me.txtNombreRuta.Size = New System.Drawing.Size(252, 22)
        Me.txtNombreRuta.TabIndex = 22
        '
        'lnkidRuta
        '
        Me.lnkidRuta.AutoSize = True
        Me.lnkidRuta.Location = New System.Drawing.Point(33, 247)
        Me.lnkidRuta.Name = "lnkidRuta"
        Me.lnkidRuta.Size = New System.Drawing.Size(37, 17)
        Me.lnkidRuta.TabIndex = 20
        Me.lnkidRuta.TabStop = True
        Me.lnkidRuta.Text = "Ruta"
        '
        'dtpHoraMaximaEntrega
        '
        Me.dtpHoraMaximaEntrega.CustomFormat = "hh:mm:ss tt"
        Me.dtpHoraMaximaEntrega.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpHoraMaximaEntrega.Location = New System.Drawing.Point(651, 359)
        Me.dtpHoraMaximaEntrega.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtpHoraMaximaEntrega.Name = "dtpHoraMaximaEntrega"
        Me.dtpHoraMaximaEntrega.ShowUpDown = True
        Me.dtpHoraMaximaEntrega.Size = New System.Drawing.Size(251, 23)
        Me.dtpHoraMaximaEntrega.TabIndex = 36
        '
        'dtpHoraInicialEntrega
        '
        Me.dtpHoraInicialEntrega.CustomFormat = "hh:mm:ss tt"
        Me.dtpHoraInicialEntrega.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpHoraInicialEntrega.Location = New System.Drawing.Point(651, 321)
        Me.dtpHoraInicialEntrega.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtpHoraInicialEntrega.Name = "dtpHoraInicialEntrega"
        Me.dtpHoraInicialEntrega.ShowUpDown = True
        Me.dtpHoraInicialEntrega.Size = New System.Drawing.Size(251, 23)
        Me.dtpHoraInicialEntrega.TabIndex = 32
        '
        'dtpHoraFinalTraslado
        '
        Me.dtpHoraFinalTraslado.CustomFormat = "hh:mm:ss tt"
        Me.dtpHoraFinalTraslado.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtpHoraFinalTraslado.Location = New System.Drawing.Point(170, 359)
        Me.dtpHoraFinalTraslado.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtpHoraFinalTraslado.Name = "dtpHoraFinalTraslado"
        Me.dtpHoraFinalTraslado.ShowUpDown = True
        Me.dtpHoraFinalTraslado.Size = New System.Drawing.Size(251, 23)
        Me.dtpHoraFinalTraslado.TabIndex = 34
        '
        'dtpHoraInicialTraslado
        '
        Me.dtpHoraInicialTraslado.CustomFormat = "hh:mm:ss tt"
        Me.dtpHoraInicialTraslado.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtpHoraInicialTraslado.Location = New System.Drawing.Point(170, 321)
        Me.dtpHoraInicialTraslado.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtpHoraInicialTraslado.Name = "dtpHoraInicialTraslado"
        Me.dtpHoraInicialTraslado.ShowUpDown = True
        Me.dtpHoraInicialTraslado.Size = New System.Drawing.Size(251, 23)
        Me.dtpHoraInicialTraslado.TabIndex = 30
        '
        'txtNoGuia
        '
        Me.txtNoGuia.Location = New System.Drawing.Point(652, 76)
        Me.txtNoGuia.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNoGuia.MenuManager = Me.RibbonControl
        Me.txtNoGuia.Name = "txtNoGuia"
        Me.txtNoGuia.Properties.Mask.EditMask = "n0"
        Me.txtNoGuia.Size = New System.Drawing.Size(252, 22)
        Me.txtNoGuia.TabIndex = 5
        '
        'txtIdPiloto
        '
        Me.txtIdPiloto.Location = New System.Drawing.Point(576, 203)
        Me.txtIdPiloto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtIdPiloto.MenuManager = Me.RibbonControl
        Me.txtIdPiloto.Name = "txtIdPiloto"
        Me.txtIdPiloto.Properties.Mask.EditMask = "n0"
        Me.txtIdPiloto.Size = New System.Drawing.Size(69, 22)
        Me.txtIdPiloto.TabIndex = 18
        '
        'txtNombrePiloto
        '
        Me.txtNombrePiloto.Location = New System.Drawing.Point(651, 203)
        Me.txtNombrePiloto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNombrePiloto.MenuManager = Me.RibbonControl
        Me.txtNombrePiloto.Name = "txtNombrePiloto"
        Me.txtNombrePiloto.Properties.ReadOnly = True
        Me.txtNombrePiloto.Size = New System.Drawing.Size(252, 22)
        Me.txtNombrePiloto.TabIndex = 19
        '
        'lnkidPiloto
        '
        Me.lnkidPiloto.AutoSize = True
        Me.lnkidPiloto.Location = New System.Drawing.Point(488, 207)
        Me.lnkidPiloto.Name = "lnkidPiloto"
        Me.lnkidPiloto.Size = New System.Drawing.Size(41, 17)
        Me.lnkidPiloto.TabIndex = 17
        Me.lnkidPiloto.TabStop = True
        Me.lnkidPiloto.Text = "Piloto"
        '
        'txtIdVehiculo
        '
        Me.txtIdVehiculo.Location = New System.Drawing.Point(576, 162)
        Me.txtIdVehiculo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtIdVehiculo.MenuManager = Me.RibbonControl
        Me.txtIdVehiculo.Name = "txtIdVehiculo"
        Me.txtIdVehiculo.Properties.Mask.EditMask = "n0"
        Me.txtIdVehiculo.Size = New System.Drawing.Size(69, 22)
        Me.txtIdVehiculo.TabIndex = 13
        '
        'txtNombreVehiculo
        '
        Me.txtNombreVehiculo.Location = New System.Drawing.Point(651, 162)
        Me.txtNombreVehiculo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNombreVehiculo.MenuManager = Me.RibbonControl
        Me.txtNombreVehiculo.Name = "txtNombreVehiculo"
        Me.txtNombreVehiculo.Properties.ReadOnly = True
        Me.txtNombreVehiculo.Size = New System.Drawing.Size(252, 22)
        Me.txtNombreVehiculo.TabIndex = 14
        '
        'lnkidVehiculo
        '
        Me.lnkidVehiculo.AutoSize = True
        Me.lnkidVehiculo.Location = New System.Drawing.Point(488, 169)
        Me.lnkidVehiculo.Name = "lnkidVehiculo"
        Me.lnkidVehiculo.Size = New System.Drawing.Size(58, 17)
        Me.lnkidVehiculo.TabIndex = 10
        Me.lnkidVehiculo.TabStop = True
        Me.lnkidVehiculo.Text = "Vehiculo"
        '
        'txtObservacion
        '
        Me.txtObservacion.Location = New System.Drawing.Point(170, 399)
        Me.txtObservacion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtObservacion.MenuManager = Me.RibbonControl
        Me.txtObservacion.Name = "txtObservacion"
        Me.txtObservacion.Size = New System.Drawing.Size(733, 98)
        Me.txtObservacion.TabIndex = 38
        '
        'cbxAnularTraslado
        '
        Me.cbxAnularTraslado.Location = New System.Drawing.Point(651, 241)
        Me.cbxAnularTraslado.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbxAnularTraslado.MenuManager = Me.RibbonControl
        Me.cbxAnularTraslado.Name = "cbxAnularTraslado"
        Me.cbxAnularTraslado.Properties.Caption = ""
        Me.cbxAnularTraslado.Size = New System.Drawing.Size(77, 24)
        Me.cbxAnularTraslado.TabIndex = 24
        '
        'cbxLocal
        '
        Me.cbxLocal.EditValue = True
        Me.cbxLocal.Location = New System.Drawing.Point(170, 199)
        Me.cbxLocal.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbxLocal.MenuManager = Me.RibbonControl
        Me.cbxLocal.Name = "cbxLocal"
        Me.cbxLocal.Properties.Caption = "Los producto seran enviados dentro del País"
        Me.cbxLocal.Size = New System.Drawing.Size(23, 24)
        Me.cbxLocal.TabIndex = 16
        '
        'GroupControl3
        '
        Me.GroupControl3.Controls.Add(Me.dgrid)
        Me.GroupControl3.Controls.Add(Me.lnkAgregarProducto)
        Me.GroupControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl3.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(938, 542)
        Me.GroupControl3.TabIndex = 0
        Me.GroupControl3.Text = "Lista de Productos"
        '
        'dgrid
        '
        Me.dgrid.AllowUserToResizeRows = False
        Me.dgrid.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 7.8!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.MidnightBlue
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgrid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colIdProducto, Me.colIsNew, Me.ColCodProducto, Me.ColNomProducto, Me.colUnidadMedida, Me.colPresentacion, Me.colEstadoProducto, Me.colCantidadExistencia, Me.colPesoExistencia, Me.ColCantidad, Me.ColPeso, Me.ColPrecio, Me.ColTotal, Me.IdLinea})
        Me.dgrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.dgrid.GridColor = System.Drawing.Color.Navy
        Me.dgrid.Location = New System.Drawing.Point(12, 55)
        Me.dgrid.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dgrid.MultiSelect = False
        Me.dgrid.Name = "dgrid"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Tahoma", 7.8!)
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgrid.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.dgrid.RowHeadersVisible = False
        Me.dgrid.RowHeadersWidth = 40
        Me.dgrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgrid.Size = New System.Drawing.Size(906, 279)
        Me.dgrid.TabIndex = 1
        '
        'colIdProducto
        '
        Me.colIdProducto.HeaderText = "IdProducto"
        Me.colIdProducto.MinimumWidth = 6
        Me.colIdProducto.Name = "colIdProducto"
        Me.colIdProducto.ReadOnly = True
        Me.colIdProducto.Visible = False
        Me.colIdProducto.Width = 125
        '
        'colIsNew
        '
        Me.colIsNew.HeaderText = "IsNew"
        Me.colIsNew.MinimumWidth = 6
        Me.colIsNew.Name = "colIsNew"
        Me.colIsNew.ReadOnly = True
        Me.colIsNew.Visible = False
        Me.colIsNew.Width = 125
        '
        'ColCodProducto
        '
        Me.ColCodProducto.HeaderText = "Código"
        Me.ColCodProducto.MinimumWidth = 6
        Me.ColCodProducto.Name = "ColCodProducto"
        Me.ColCodProducto.Width = 125
        '
        'ColNomProducto
        '
        Me.ColNomProducto.HeaderText = "Descripción"
        Me.ColNomProducto.MinimumWidth = 6
        Me.ColNomProducto.Name = "ColNomProducto"
        Me.ColNomProducto.ReadOnly = True
        Me.ColNomProducto.Width = 200
        '
        'colUnidadMedida
        '
        Me.colUnidadMedida.HeaderText = "U.M. Basica"
        Me.colUnidadMedida.MinimumWidth = 6
        Me.colUnidadMedida.Name = "colUnidadMedida"
        Me.colUnidadMedida.ReadOnly = True
        Me.colUnidadMedida.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colUnidadMedida.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colUnidadMedida.Width = 150
        '
        'colPresentacion
        '
        Me.colPresentacion.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.colPresentacion.HeaderText = "Presentación"
        Me.colPresentacion.MinimumWidth = 6
        Me.colPresentacion.Name = "colPresentacion"
        Me.colPresentacion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.colPresentacion.Width = 150
        '
        'colEstadoProducto
        '
        Me.colEstadoProducto.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.colEstadoProducto.HeaderText = "Estado"
        Me.colEstadoProducto.MinimumWidth = 6
        Me.colEstadoProducto.Name = "colEstadoProducto"
        Me.colEstadoProducto.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colEstadoProducto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.colEstadoProducto.Width = 125
        '
        'colCantidadExistencia
        '
        Me.colCantidadExistencia.HeaderText = "Cant. Disp"
        Me.colCantidadExistencia.MinimumWidth = 6
        Me.colCantidadExistencia.Name = "colCantidadExistencia"
        Me.colCantidadExistencia.ReadOnly = True
        Me.colCantidadExistencia.Width = 125
        '
        'colPesoExistencia
        '
        Me.colPesoExistencia.HeaderText = "Peso Disp."
        Me.colPesoExistencia.MinimumWidth = 6
        Me.colPesoExistencia.Name = "colPesoExistencia"
        Me.colPesoExistencia.ReadOnly = True
        Me.colPesoExistencia.Width = 125
        '
        'ColCantidad
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "N2"
        DataGridViewCellStyle2.NullValue = "0"
        Me.ColCantidad.DefaultCellStyle = DataGridViewCellStyle2
        Me.ColCantidad.HeaderText = "Cantidad"
        Me.ColCantidad.MinimumWidth = 6
        Me.ColCantidad.Name = "ColCantidad"
        Me.ColCantidad.Width = 125
        '
        'ColPeso
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N3"
        DataGridViewCellStyle3.NullValue = "0"
        Me.ColPeso.DefaultCellStyle = DataGridViewCellStyle3
        Me.ColPeso.HeaderText = "Peso"
        Me.ColPeso.MinimumWidth = 6
        Me.ColPeso.Name = "ColPeso"
        Me.ColPeso.Width = 125
        '
        'ColPrecio
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N2"
        DataGridViewCellStyle4.NullValue = Nothing
        Me.ColPrecio.DefaultCellStyle = DataGridViewCellStyle4
        Me.ColPrecio.HeaderText = "Precio"
        Me.ColPrecio.MinimumWidth = 6
        Me.ColPrecio.Name = "ColPrecio"
        Me.ColPrecio.ReadOnly = True
        Me.ColPrecio.Width = 125
        '
        'ColTotal
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "N2"
        DataGridViewCellStyle5.NullValue = Nothing
        Me.ColTotal.DefaultCellStyle = DataGridViewCellStyle5
        Me.ColTotal.HeaderText = "Total"
        Me.ColTotal.MinimumWidth = 6
        Me.ColTotal.Name = "ColTotal"
        Me.ColTotal.ReadOnly = True
        Me.ColTotal.Width = 125
        '
        'IdLinea
        '
        Me.IdLinea.HeaderText = "IdLinea"
        Me.IdLinea.MinimumWidth = 6
        Me.IdLinea.Name = "IdLinea"
        Me.IdLinea.Visible = False
        Me.IdLinea.Width = 125
        '
        'lnkAgregarProducto
        '
        Me.lnkAgregarProducto.AutoSize = True
        Me.lnkAgregarProducto.Location = New System.Drawing.Point(12, 36)
        Me.lnkAgregarProducto.Name = "lnkAgregarProducto"
        Me.lnkAgregarProducto.Size = New System.Drawing.Size(146, 17)
        Me.lnkAgregarProducto.TabIndex = 0
        Me.lnkAgregarProducto.TabStop = True
        Me.lnkAgregarProducto.Text = "Agregar Producto (F2)"
        '
        'dkTraslado_Producto
        '
        Me.dkTraslado_Producto.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerBottom})
        Me.dkTraslado_Producto.Form = Me
        Me.dkTraslado_Producto.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane"})
        '
        'hideContainerBottom
        '
        Me.hideContainerBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.hideContainerBottom.Controls.Add(Me.DockPanel1)
        Me.hideContainerBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 815)
        Me.hideContainerBottom.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(940, 26)
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.ID = New System.Guid("bbba892d-9cf2-4d6d-be54-5c1b33fa41a9")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 0)
        Me.DockPanel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(200, 93)
        Me.DockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.SavedIndex = 0
        Me.DockPanel1.Size = New System.Drawing.Size(940, 114)
        Me.DockPanel1.Text = "Bitácora"
        Me.DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(User_agrLabel)
        Me.DockPanel1_Container.Controls.Add(Me.User_agrTextEdit)
        Me.DockPanel1_Container.Controls.Add(Me.Fec_agrDateEdit)
        Me.DockPanel1_Container.Controls.Add(Me.User_modTextEdit)
        Me.DockPanel1_Container.Controls.Add(Fec_agrLabel)
        Me.DockPanel1_Container.Controls.Add(User_modLabel)
        Me.DockPanel1_Container.Controls.Add(Fec_modLabel)
        Me.DockPanel1_Container.Controls.Add(Me.Fec_modDateEdit)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(3, 31)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(933, 80)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'xtraTraslado_Producto
        '
        Me.xtraTraslado_Producto.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtraTraslado_Producto.Location = New System.Drawing.Point(0, 243)
        Me.xtraTraslado_Producto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.xtraTraslado_Producto.Name = "xtraTraslado_Producto"
        Me.xtraTraslado_Producto.SelectedTabPage = Me.Traslado
        Me.xtraTraslado_Producto.Size = New System.Drawing.Size(940, 572)
        Me.xtraTraslado_Producto.TabIndex = 1
        Me.xtraTraslado_Producto.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.Traslado, Me.DetalleTraslado})
        '
        'Traslado
        '
        Me.Traslado.Controls.Add(Me.GroupControl1)
        Me.Traslado.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Traslado.Name = "Traslado"
        Me.Traslado.Size = New System.Drawing.Size(938, 542)
        Me.Traslado.Text = "Datos Traslado"
        '
        'DetalleTraslado
        '
        Me.DetalleTraslado.Controls.Add(Me.GroupControl3)
        Me.DetalleTraslado.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DetalleTraslado.Name = "DetalleTraslado"
        Me.DetalleTraslado.Size = New System.Drawing.Size(938, 542)
        Me.DetalleTraslado.Text = "Detalle Traslado"
        '
        'frmTraslado_Producto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(940, 871)
        Me.Controls.Add(Me.xtraTraslado_Producto)
        Me.Controls.Add(Me.PanelControl1)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmTraslado_Producto"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Traslado de Producto"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsPropietario, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.txtNoDocumento.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtID.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.dtpFechaEntrega.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFechaEntrega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFechaTraslado.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFechaTraslado.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbMuelle.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbEmpresa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBodegaDesti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBodegaOrigen.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdRuta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreRuta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoGuia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdPiloto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombrePiloto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdVehiculo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreVehiculo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtObservacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbxAnularTraslado.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbxLocal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        Me.GroupControl3.PerformLayout()
        CType(Me.dgrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dkTraslado_Producto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerBottom.ResumeLayout(False)
        Me.DockPanel1.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        Me.DockPanel1_Container.PerformLayout()
        CType(Me.xtraTraslado_Producto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtraTraslado_Producto.ResumeLayout(False)
        Me.Traslado.ResumeLayout(False)
        Me.DetalleTraslado.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents mnuGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEliminar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents User_agrTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_agrDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_modTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_modDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents DataBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DsPropietario As DsPropietario
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents txtEstado As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtNoDocumento As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtID As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dtpHoraMaximaEntrega As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpHoraInicialEntrega As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpHoraFinalTraslado As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpHoraInicialTraslado As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtNoGuia As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtIdPiloto As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNombrePiloto As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lnkidPiloto As System.Windows.Forms.LinkLabel
    Friend WithEvents txtIdVehiculo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNombreVehiculo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lnkidVehiculo As System.Windows.Forms.LinkLabel
    Friend WithEvents txtObservacion As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents cbxAnularTraslado As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents cbxLocal As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dgrid As System.Windows.Forms.DataGridView
    Friend WithEvents colIdProducto As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colIsNew As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColCodProducto As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColNomProducto As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colUnidadMedida As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPresentacion As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents colEstadoProducto As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents colCantidadExistencia As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPesoExistencia As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColCantidad As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColPeso As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColPrecio As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColTotal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IdLinea As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lnkAgregarProducto As System.Windows.Forms.LinkLabel
    Friend WithEvents txtIdRuta As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNombreRuta As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lnkidRuta As System.Windows.Forms.LinkLabel
    Friend WithEvents dkTraslado_Producto As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents xtraTraslado_Producto As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents Traslado As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents DetalleTraslado As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents cmbBodegaOrigen As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbBodegaDesti As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbEmpresa As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbPropietario As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbMuelle As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents dtpFechaTraslado As DevExpress.XtraEditors.DateEdit
    Friend WithEvents dtpFechaEntrega As DevExpress.XtraEditors.DateEdit
End Class
