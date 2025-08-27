<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMontacarga
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim Label12 As System.Windows.Forms.Label
        Dim IdPropietarioLabel As System.Windows.Forms.Label
        Dim lblCodigoBarra As System.Windows.Forms.Label
        Dim lblNombre As System.Windows.Forms.Label
        Dim lblCodigo As System.Windows.Forms.Label
        Dim lblAlto As System.Windows.Forms.Label
        Dim lblPeso As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim Fec_agrLabel As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim Label5 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMontacarga))
        Dim lblNivelDesde As System.Windows.Forms.Label
        Dim lblNivelHasta As System.Windows.Forms.Label
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.btnGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuAsignacion = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem3 = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdCodigoBarra = New DevExpress.XtraBars.BarButtonItem()
        Me.btnImprimeCodigoBarra = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImprmirCodigoBarra = New DevExpress.XtraBars.BarButtonItem()
        Me.btnActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.btnEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdUbicacion = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPage2 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.nudcostohora = New System.Windows.Forms.NumericUpDown()
        Me.cbxEmpresa = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtID = New DevExpress.XtraEditors.TextEdit()
        Me.dcProximoMantenimiento = New DevExpress.XtraEditors.DateEdit()
        Me.dcFechaInicioOperaciones = New DevExpress.XtraEditors.DateEdit()
        Me.dcFechaCompra = New DevExpress.XtraEditors.DateEdit()
        Me.txtTipoMontacarga = New DevExpress.XtraEditors.TextEdit()
        Me.txtTipoCombustible = New DevExpress.XtraEditors.TextEdit()
        Me.nudDesplazamientoMotor = New System.Windows.Forms.NumericUpDown()
        Me.nudCapacidadBasica = New System.Windows.Forms.NumericUpDown()
        Me.txtSerie = New DevExpress.XtraEditors.TextEdit()
        Me.txtModelo = New DevExpress.XtraEditors.TextEdit()
        Me.txtNombre = New DevExpress.XtraEditors.TextEdit()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.Dgrid = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.BarButtonItem4 = New DevExpress.XtraBars.BarButtonItem()
        Me.TabDatos = New DevExpress.XtraTab.XtraTabControl()
        Me.XtraTabPage1 = New DevExpress.XtraTab.XtraTabPage()
        Me.XtraTabPage2 = New DevExpress.XtraTab.XtraTabPage()
        Me.txtNivelDesde = New System.Windows.Forms.NumericUpDown()
        Me.txtNivelHasta = New System.Windows.Forms.NumericUpDown()
        Label12 = New System.Windows.Forms.Label()
        IdPropietarioLabel = New System.Windows.Forms.Label()
        lblCodigoBarra = New System.Windows.Forms.Label()
        lblNombre = New System.Windows.Forms.Label()
        lblCodigo = New System.Windows.Forms.Label()
        lblAlto = New System.Windows.Forms.Label()
        lblPeso = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        Fec_agrLabel = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        Label5 = New System.Windows.Forms.Label()
        lblNivelDesde = New System.Windows.Forms.Label()
        lblNivelHasta = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.nudcostohora, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbxEmpresa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtID.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dcProximoMantenimiento.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dcProximoMantenimiento.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dcFechaInicioOperaciones.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dcFechaInicioOperaciones.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dcFechaCompra.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dcFechaCompra.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTipoMontacarga.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTipoCombustible.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudDesplazamientoMotor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudCapacidadBasica, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtModelo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombre.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        CType(Me.Dgrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TabDatos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabDatos.SuspendLayout()
        Me.XtraTabPage1.SuspendLayout()
        Me.XtraTabPage2.SuspendLayout()
        CType(Me.txtNivelDesde, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNivelHasta, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label12
        '
        Label12.AutoSize = True
        Label12.Location = New System.Drawing.Point(26, 43)
        Label12.Name = "Label12"
        Label12.Size = New System.Drawing.Size(51, 16)
        Label12.TabIndex = 0
        Label12.Text = "Código:"
        '
        'IdPropietarioLabel
        '
        IdPropietarioLabel.AutoSize = True
        IdPropietarioLabel.Location = New System.Drawing.Point(26, 75)
        IdPropietarioLabel.Name = "IdPropietarioLabel"
        IdPropietarioLabel.Size = New System.Drawing.Size(62, 16)
        IdPropietarioLabel.TabIndex = 2
        IdPropietarioLabel.Text = "Empresa:"
        '
        'lblCodigoBarra
        '
        lblCodigoBarra.AutoSize = True
        lblCodigoBarra.Location = New System.Drawing.Point(26, 175)
        lblCodigoBarra.Name = "lblCodigoBarra"
        lblCodigoBarra.Size = New System.Drawing.Size(42, 16)
        lblCodigoBarra.TabIndex = 8
        lblCodigoBarra.Text = "Serie:"
        '
        'lblNombre
        '
        lblNombre.AutoSize = True
        lblNombre.Location = New System.Drawing.Point(26, 143)
        lblNombre.Name = "lblNombre"
        lblNombre.Size = New System.Drawing.Size(53, 16)
        lblNombre.TabIndex = 6
        lblNombre.Text = "Modelo:"
        '
        'lblCodigo
        '
        lblCodigo.AutoSize = True
        lblCodigo.Location = New System.Drawing.Point(26, 111)
        lblCodigo.Name = "lblCodigo"
        lblCodigo.Size = New System.Drawing.Size(57, 16)
        lblCodigo.TabIndex = 4
        lblCodigo.Text = "Nombre:"
        '
        'lblAlto
        '
        lblAlto.AutoSize = True
        lblAlto.Location = New System.Drawing.Point(26, 239)
        lblAlto.Name = "lblAlto"
        lblAlto.Size = New System.Drawing.Size(139, 16)
        lblAlto.TabIndex = 12
        lblAlto.Text = "Desplazamiento Motor:"
        '
        'lblPeso
        '
        lblPeso.AutoSize = True
        lblPeso.Location = New System.Drawing.Point(26, 206)
        lblPeso.Name = "lblPeso"
        lblPeso.Size = New System.Drawing.Size(111, 16)
        lblPeso.TabIndex = 10
        lblPeso.Text = "Capacidad Básica:"
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(26, 333)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(108, 16)
        Label1.TabIndex = 16
        Label1.Text = "Tipo Montacarga:"
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(26, 301)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(111, 16)
        Label2.TabIndex = 14
        Label2.Text = "Tipo Combustible:"
        '
        'Fec_agrLabel
        '
        Fec_agrLabel.AutoSize = True
        Fec_agrLabel.Location = New System.Drawing.Point(28, 367)
        Fec_agrLabel.Name = "Fec_agrLabel"
        Fec_agrLabel.Size = New System.Drawing.Size(113, 16)
        Fec_agrLabel.TabIndex = 18
        Fec_agrLabel.Text = "Fecha de Compra:"
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.Location = New System.Drawing.Point(26, 400)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(117, 16)
        Label3.TabIndex = 20
        Label3.Text = "Inició Operaciones:"
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(26, 432)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(146, 16)
        Label4.TabIndex = 22
        Label4.Text = "Próximo Mantenimiento:"
        '
        'Label5
        '
        Label5.AutoSize = True
        Label5.Location = New System.Drawing.Point(26, 270)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(75, 16)
        Label5.TabIndex = 25
        Label5.Text = "Costo Hora:"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 780)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(953, 30)
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.RibbonControl.SearchEditItem, Me.btnGuardar, Me.mnuAsignacion, Me.BarButtonItem1, Me.BarButtonItem2, Me.BarButtonItem3, Me.cmdCodigoBarra, Me.btnImprimeCodigoBarra, Me.cmdImprmirCodigoBarra, Me.btnActualizar, Me.btnEliminar, Me.cmdUbicacion})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 14
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1, Me.RibbonPage2})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(953, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'btnGuardar
        '
        Me.btnGuardar.Caption = "Guardar"
        Me.btnGuardar.Id = 1
        Me.btnGuardar.ImageOptions.SvgImage = CType(resources.GetObject("btnGuardar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnGuardar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.G))
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.ShortcutKeyDisplayString = "G"
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
        'btnImprimeCodigoBarra
        '
        Me.btnImprimeCodigoBarra.Caption = "Imprimir Código Barra"
        Me.btnImprimeCodigoBarra.Id = 9
        Me.btnImprimeCodigoBarra.ImageOptions.Image = CType(resources.GetObject("btnImprimeCodigoBarra.ImageOptions.Image"), System.Drawing.Image)
        Me.btnImprimeCodigoBarra.ImageOptions.LargeImage = CType(resources.GetObject("btnImprimeCodigoBarra.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.btnImprimeCodigoBarra.Name = "btnImprimeCodigoBarra"
        Me.btnImprimeCodigoBarra.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'cmdImprmirCodigoBarra
        '
        Me.cmdImprmirCodigoBarra.Caption = "Código Barra"
        Me.cmdImprmirCodigoBarra.Id = 10
        Me.cmdImprmirCodigoBarra.ImageOptions.Image = CType(resources.GetObject("cmdImprmirCodigoBarra.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdImprmirCodigoBarra.ImageOptions.LargeImage = CType(resources.GetObject("cmdImprmirCodigoBarra.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.cmdImprmirCodigoBarra.Name = "cmdImprmirCodigoBarra"
        '
        'btnActualizar
        '
        Me.btnActualizar.Caption = "Actualizar"
        Me.btnActualizar.Id = 11
        Me.btnActualizar.ImageOptions.SvgImage = CType(resources.GetObject("btnActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnActualizar.Name = "btnActualizar"
        '
        'btnEliminar
        '
        Me.btnEliminar.Caption = "Eliminar"
        Me.btnEliminar.Id = 12
        Me.btnEliminar.ImageOptions.SvgImage = CType(resources.GetObject("btnEliminar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'cmdUbicacion
        '
        Me.cmdUbicacion.Caption = "Ubicación"
        Me.cmdUbicacion.Id = 13
        Me.cmdUbicacion.ImageOptions.Image = CType(resources.GetObject("cmdUbicacion.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdUbicacion.ImageOptions.LargeImage = CType(resources.GetObject("cmdUbicacion.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.cmdUbicacion.Name = "cmdUbicacion"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Montacarga"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.btnGuardar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.btnActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.btnEliminar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.btnImprimeCodigoBarra)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonPage2
        '
        Me.RibbonPage2.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup2})
        Me.RibbonPage2.Name = "RibbonPage2"
        Me.RibbonPage2.Text = "Imprimir"
        '
        'RibbonPageGroup2
        '
        Me.RibbonPageGroup2.ItemLinks.Add(Me.BarButtonItem1)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.BarButtonItem2)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.cmdImprmirCodigoBarra)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.cmdUbicacion)
        Me.RibbonPageGroup2.Name = "RibbonPageGroup2"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.txtNivelHasta)
        Me.GroupControl1.Controls.Add(lblNivelHasta)
        Me.GroupControl1.Controls.Add(Me.txtNivelDesde)
        Me.GroupControl1.Controls.Add(lblNivelDesde)
        Me.GroupControl1.Controls.Add(Me.nudcostohora)
        Me.GroupControl1.Controls.Add(Label5)
        Me.GroupControl1.Controls.Add(Me.cbxEmpresa)
        Me.GroupControl1.Controls.Add(Me.txtID)
        Me.GroupControl1.Controls.Add(Me.dcProximoMantenimiento)
        Me.GroupControl1.Controls.Add(Label4)
        Me.GroupControl1.Controls.Add(Me.dcFechaInicioOperaciones)
        Me.GroupControl1.Controls.Add(Label3)
        Me.GroupControl1.Controls.Add(Me.dcFechaCompra)
        Me.GroupControl1.Controls.Add(Fec_agrLabel)
        Me.GroupControl1.Controls.Add(Me.txtTipoMontacarga)
        Me.GroupControl1.Controls.Add(Label1)
        Me.GroupControl1.Controls.Add(Me.txtTipoCombustible)
        Me.GroupControl1.Controls.Add(Label2)
        Me.GroupControl1.Controls.Add(Me.nudDesplazamientoMotor)
        Me.GroupControl1.Controls.Add(Me.nudCapacidadBasica)
        Me.GroupControl1.Controls.Add(lblAlto)
        Me.GroupControl1.Controls.Add(lblPeso)
        Me.GroupControl1.Controls.Add(Me.txtSerie)
        Me.GroupControl1.Controls.Add(lblCodigoBarra)
        Me.GroupControl1.Controls.Add(Me.txtModelo)
        Me.GroupControl1.Controls.Add(lblNombre)
        Me.GroupControl1.Controls.Add(Me.txtNombre)
        Me.GroupControl1.Controls.Add(lblCodigo)
        Me.GroupControl1.Controls.Add(Label12)
        Me.GroupControl1.Controls.Add(IdPropietarioLabel)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(951, 557)
        Me.GroupControl1.TabIndex = 0
        '
        'nudcostohora
        '
        Me.nudcostohora.DecimalPlaces = 6
        Me.nudcostohora.Location = New System.Drawing.Point(180, 267)
        Me.nudcostohora.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.nudcostohora.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.nudcostohora.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.nudcostohora.Name = "nudcostohora"
        Me.nudcostohora.Size = New System.Drawing.Size(243, 23)
        Me.nudcostohora.TabIndex = 26
        '
        'cbxEmpresa
        '
        Me.cbxEmpresa.Location = New System.Drawing.Point(180, 73)
        Me.cbxEmpresa.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbxEmpresa.MenuManager = Me.RibbonControl
        Me.cbxEmpresa.Name = "cbxEmpresa"
        Me.cbxEmpresa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbxEmpresa.Properties.NullText = ""
        Me.cbxEmpresa.Size = New System.Drawing.Size(243, 22)
        Me.cbxEmpresa.TabIndex = 24
        '
        'txtID
        '
        Me.txtID.Enabled = False
        Me.txtID.Location = New System.Drawing.Point(180, 39)
        Me.txtID.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtID.MenuManager = Me.RibbonControl
        Me.txtID.Name = "txtID"
        Me.txtID.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control
        Me.txtID.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtID.Properties.Appearance.Options.UseBackColor = True
        Me.txtID.Properties.Appearance.Options.UseFont = True
        Me.txtID.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtID.Size = New System.Drawing.Size(243, 24)
        Me.txtID.TabIndex = 1
        '
        'dcProximoMantenimiento
        '
        Me.dcProximoMantenimiento.EditValue = Nothing
        Me.dcProximoMantenimiento.Location = New System.Drawing.Point(180, 428)
        Me.dcProximoMantenimiento.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dcProximoMantenimiento.MenuManager = Me.RibbonControl
        Me.dcProximoMantenimiento.Name = "dcProximoMantenimiento"
        Me.dcProximoMantenimiento.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dcProximoMantenimiento.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dcProximoMantenimiento.Size = New System.Drawing.Size(243, 22)
        Me.dcProximoMantenimiento.TabIndex = 23
        '
        'dcFechaInicioOperaciones
        '
        Me.dcFechaInicioOperaciones.EditValue = Nothing
        Me.dcFechaInicioOperaciones.Location = New System.Drawing.Point(180, 396)
        Me.dcFechaInicioOperaciones.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dcFechaInicioOperaciones.MenuManager = Me.RibbonControl
        Me.dcFechaInicioOperaciones.Name = "dcFechaInicioOperaciones"
        Me.dcFechaInicioOperaciones.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dcFechaInicioOperaciones.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dcFechaInicioOperaciones.Size = New System.Drawing.Size(243, 22)
        Me.dcFechaInicioOperaciones.TabIndex = 21
        '
        'dcFechaCompra
        '
        Me.dcFechaCompra.EditValue = Nothing
        Me.dcFechaCompra.Location = New System.Drawing.Point(180, 363)
        Me.dcFechaCompra.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dcFechaCompra.MenuManager = Me.RibbonControl
        Me.dcFechaCompra.Name = "dcFechaCompra"
        Me.dcFechaCompra.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dcFechaCompra.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dcFechaCompra.Size = New System.Drawing.Size(243, 22)
        Me.dcFechaCompra.TabIndex = 19
        '
        'txtTipoMontacarga
        '
        Me.txtTipoMontacarga.Location = New System.Drawing.Point(180, 330)
        Me.txtTipoMontacarga.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtTipoMontacarga.MenuManager = Me.RibbonControl
        Me.txtTipoMontacarga.Name = "txtTipoMontacarga"
        Me.txtTipoMontacarga.Size = New System.Drawing.Size(243, 22)
        Me.txtTipoMontacarga.TabIndex = 17
        '
        'txtTipoCombustible
        '
        Me.txtTipoCombustible.Location = New System.Drawing.Point(180, 298)
        Me.txtTipoCombustible.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtTipoCombustible.MenuManager = Me.RibbonControl
        Me.txtTipoCombustible.Name = "txtTipoCombustible"
        Me.txtTipoCombustible.Size = New System.Drawing.Size(243, 22)
        Me.txtTipoCombustible.TabIndex = 15
        '
        'nudDesplazamientoMotor
        '
        Me.nudDesplazamientoMotor.DecimalPlaces = 6
        Me.nudDesplazamientoMotor.Location = New System.Drawing.Point(180, 236)
        Me.nudDesplazamientoMotor.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.nudDesplazamientoMotor.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.nudDesplazamientoMotor.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.nudDesplazamientoMotor.Name = "nudDesplazamientoMotor"
        Me.nudDesplazamientoMotor.Size = New System.Drawing.Size(243, 23)
        Me.nudDesplazamientoMotor.TabIndex = 13
        '
        'nudCapacidadBasica
        '
        Me.nudCapacidadBasica.DecimalPlaces = 6
        Me.nudCapacidadBasica.Location = New System.Drawing.Point(180, 203)
        Me.nudCapacidadBasica.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.nudCapacidadBasica.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.nudCapacidadBasica.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.nudCapacidadBasica.Name = "nudCapacidadBasica"
        Me.nudCapacidadBasica.Size = New System.Drawing.Size(243, 23)
        Me.nudCapacidadBasica.TabIndex = 11
        '
        'txtSerie
        '
        Me.txtSerie.Location = New System.Drawing.Point(180, 171)
        Me.txtSerie.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtSerie.MenuManager = Me.RibbonControl
        Me.txtSerie.Name = "txtSerie"
        Me.txtSerie.Size = New System.Drawing.Size(243, 22)
        Me.txtSerie.TabIndex = 9
        '
        'txtModelo
        '
        Me.txtModelo.Location = New System.Drawing.Point(180, 139)
        Me.txtModelo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtModelo.MenuManager = Me.RibbonControl
        Me.txtModelo.Name = "txtModelo"
        Me.txtModelo.Size = New System.Drawing.Size(243, 22)
        Me.txtModelo.TabIndex = 7
        '
        'txtNombre
        '
        Me.txtNombre.Location = New System.Drawing.Point(180, 107)
        Me.txtNombre.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNombre.MenuManager = Me.RibbonControl
        Me.txtNombre.Name = "txtNombre"
        Me.txtNombre.Size = New System.Drawing.Size(243, 22)
        Me.txtNombre.TabIndex = 5
        '
        'GroupControl3
        '
        Me.GroupControl3.Controls.Add(Me.Dgrid)
        Me.GroupControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl3.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(951, 557)
        Me.GroupControl3.TabIndex = 0
        '
        'Dgrid
        '
        Me.Dgrid.Cursor = System.Windows.Forms.Cursors.Default
        Me.Dgrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Dgrid.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Dgrid.Location = New System.Drawing.Point(2, 28)
        Me.Dgrid.MainView = Me.GridView1
        Me.Dgrid.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Dgrid.Name = "Dgrid"
        Me.Dgrid.Size = New System.Drawing.Size(947, 527)
        Me.Dgrid.TabIndex = 0
        Me.Dgrid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.DetailHeight = 431
        Me.GridView1.GridControl = Me.Dgrid
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.Editable = False
        '
        'BarButtonItem4
        '
        Me.BarButtonItem4.Caption = "Guardar"
        Me.BarButtonItem4.Id = 1
        Me.BarButtonItem4.ImageOptions.Image = CType(resources.GetObject("BarButtonItem4.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItem4.ImageOptions.LargeImage = CType(resources.GetObject("BarButtonItem4.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.BarButtonItem4.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.G))
        Me.BarButtonItem4.Name = "BarButtonItem4"
        Me.BarButtonItem4.ShortcutKeyDisplayString = "G"
        '
        'TabDatos
        '
        Me.TabDatos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabDatos.Location = New System.Drawing.Point(0, 193)
        Me.TabDatos.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TabDatos.Name = "TabDatos"
        Me.TabDatos.SelectedTabPage = Me.XtraTabPage1
        Me.TabDatos.Size = New System.Drawing.Size(953, 587)
        Me.TabDatos.TabIndex = 3
        Me.TabDatos.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.XtraTabPage1, Me.XtraTabPage2})
        '
        'XtraTabPage1
        '
        Me.XtraTabPage1.Controls.Add(Me.GroupControl1)
        Me.XtraTabPage1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.XtraTabPage1.Name = "XtraTabPage1"
        Me.XtraTabPage1.Size = New System.Drawing.Size(951, 557)
        Me.XtraTabPage1.Text = "Datos Generales"
        '
        'XtraTabPage2
        '
        Me.XtraTabPage2.Controls.Add(Me.GroupControl3)
        Me.XtraTabPage2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.XtraTabPage2.Name = "XtraTabPage2"
        Me.XtraTabPage2.Size = New System.Drawing.Size(951, 557)
        Me.XtraTabPage2.Text = "Bodega Asignada"
        '
        'txtNivelDesde
        '
        Me.txtNivelDesde.BackColor = System.Drawing.Color.MistyRose
        Me.txtNivelDesde.Location = New System.Drawing.Point(180, 458)
        Me.txtNivelDesde.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNivelDesde.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.txtNivelDesde.Name = "txtNivelDesde"
        Me.txtNivelDesde.Size = New System.Drawing.Size(243, 23)
        Me.txtNivelDesde.TabIndex = 28
        '
        'lblNivelDesde
        '
        lblNivelDesde.AutoSize = True
        lblNivelDesde.Location = New System.Drawing.Point(26, 461)
        lblNivelDesde.Name = "lblNivelDesde"
        lblNivelDesde.Size = New System.Drawing.Size(78, 16)
        lblNivelDesde.TabIndex = 27
        lblNivelDesde.Text = "Nivel Desde:"
        '
        'txtNivelHasta
        '
        Me.txtNivelHasta.BackColor = System.Drawing.Color.MistyRose
        Me.txtNivelHasta.Location = New System.Drawing.Point(180, 489)
        Me.txtNivelHasta.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNivelHasta.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.txtNivelHasta.Name = "txtNivelHasta"
        Me.txtNivelHasta.Size = New System.Drawing.Size(243, 23)
        Me.txtNivelHasta.TabIndex = 30
        '
        'lblNivelHasta
        '
        lblNivelHasta.AutoSize = True
        lblNivelHasta.Location = New System.Drawing.Point(26, 492)
        lblNivelHasta.Name = "lblNivelHasta"
        lblNivelHasta.Size = New System.Drawing.Size(75, 16)
        lblNivelHasta.TabIndex = 29
        lblNivelHasta.Text = "Nivel Hasta:"
        '
        'frmMontacarga
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(953, 810)
        Me.Controls.Add(Me.TabDatos)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmMontacarga"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Montacarga"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.nudcostohora, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbxEmpresa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtID.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dcProximoMantenimiento.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dcProximoMantenimiento.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dcFechaInicioOperaciones.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dcFechaInicioOperaciones.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dcFechaCompra.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dcFechaCompra.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTipoMontacarga.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTipoCombustible.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudDesplazamientoMotor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudCapacidadBasica, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtModelo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombre.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        CType(Me.Dgrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TabDatos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabDatos.ResumeLayout(False)
        Me.XtraTabPage1.ResumeLayout(False)
        Me.XtraTabPage2.ResumeLayout(False)
        CType(Me.txtNivelDesde, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNivelHasta, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents btnGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuAsignacion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem3 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdCodigoBarra As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnImprimeCodigoBarra As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdImprmirCodigoBarra As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnEliminar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdUbicacion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonPage2 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents BarButtonItem4 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtSerie As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtModelo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNombre As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtTipoMontacarga As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtTipoCombustible As DevExpress.XtraEditors.TextEdit
    Friend WithEvents nudDesplazamientoMotor As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudCapacidadBasica As System.Windows.Forms.NumericUpDown
    Friend WithEvents dcProximoMantenimiento As DevExpress.XtraEditors.DateEdit
    Friend WithEvents dcFechaInicioOperaciones As DevExpress.XtraEditors.DateEdit
    Friend WithEvents dcFechaCompra As DevExpress.XtraEditors.DateEdit
    Friend WithEvents txtID As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents Dgrid As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TabDatos As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents XtraTabPage1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents XtraTabPage2 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents cbxEmpresa As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents nudcostohora As NumericUpDown
    Friend WithEvents txtNivelHasta As NumericUpDown
    Friend WithEvents txtNivelDesde As NumericUpDown
End Class
