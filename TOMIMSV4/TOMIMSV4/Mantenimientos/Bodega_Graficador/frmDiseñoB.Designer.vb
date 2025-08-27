<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDiseñoB
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If components IsNot Nothing Then
                    components.Dispose()
                End If
                If BeBodega IsNot Nothing Then
                    BeBodega.Dispose()
                    BeBodega = Nothing
                End If
                If vFont IsNot Nothing Then
                    vFont.Dispose()
                    vFont = Nothing
                End If
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim lblAncho As System.Windows.Forms.Label
        Dim lblLargo As System.Windows.Forms.Label
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.cmdExportar = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkInvertir = New System.Windows.Forms.CheckBox()
        Me.prgUbicacionesPorTramo = New DevExpress.XtraEditors.ProgressBarControl()
        Me.prgTramos = New DevExpress.XtraEditors.ProgressBarControl()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.cmdDibujarGrid = New System.Windows.Forms.Button()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.txtZoom = New System.Windows.Forms.NumericUpDown()
        Me.txtAncho = New System.Windows.Forms.NumericUpDown()
        Me.txtLargo = New System.Windows.Forms.NumericUpDown()
        Me.dfttc = New DevExpress.Utils.DefaultToolTipController(Me.components)
        Me.cmdGetStock = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbBodegas = New System.Windows.Forms.ComboBox()
        Me.PanBorde = New DevExpress.XtraEditors.PanelControl()
        Me.PanBodega = New DevExpress.XtraEditors.PanelControl()
        Me.XtraScrollableControl1 = New DevExpress.XtraEditors.XtraScrollableControl()
        Me.txtCodigo = New System.Windows.Forms.TextBox()
        Me.cmdActualizar = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.cmbEmpresa = New DevExpress.XtraEditors.LookUpEdit()
        Me.sfd = New System.Windows.Forms.SaveFileDialog()
        lblAncho = New System.Windows.Forms.Label()
        lblLargo = New System.Windows.Forms.Label()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DockPanel1.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.prgUbicacionesPorTramo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.prgTramos.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.txtZoom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAncho, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLargo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanBorde, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanBorde.SuspendLayout()
        CType(Me.PanBodega, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraScrollableControl1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.cmbEmpresa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblAncho
        '
        Me.dfttc.SetAllowHtmlText(lblAncho, DevExpress.Utils.DefaultBoolean.[Default])
        lblAncho.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblAncho.AutoSize = True
        lblAncho.Location = New System.Drawing.Point(75, 82)
        lblAncho.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblAncho.Name = "lblAncho"
        lblAncho.Size = New System.Drawing.Size(52, 17)
        lblAncho.TabIndex = 2
        lblAncho.Text = "Ancho:"
        '
        'lblLargo
        '
        Me.dfttc.SetAllowHtmlText(lblLargo, DevExpress.Utils.DefaultBoolean.[Default])
        lblLargo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblLargo.AutoSize = True
        lblLargo.Location = New System.Drawing.Point(75, 49)
        lblLargo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblLargo.Name = "lblLargo"
        lblLargo.Size = New System.Drawing.Size(48, 17)
        lblLargo.TabIndex = 0
        lblLargo.Text = "Largo:"
        '
        'DockManager1
        '
        Me.DockManager1.Form = Me
        Me.DockManager1.RootPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.DockPanel1})
        Me.DockManager1.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane"})
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left
        Me.DockPanel1.ID = New System.Guid("2a5de0b2-fb34-48ba-923c-26c46abeb907")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 0)
        Me.DockPanel1.Margin = New System.Windows.Forms.Padding(4)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(367, 200)
        Me.DockPanel1.Size = New System.Drawing.Size(367, 782)
        '
        'DockPanel1_Container
        '
        Me.dfttc.SetAllowHtmlText(Me.DockPanel1_Container, DevExpress.Utils.DefaultBoolean.[Default])
        Me.DockPanel1_Container.Controls.Add(Me.cmdExportar)
        Me.DockPanel1_Container.Controls.Add(Me.GroupBox1)
        Me.DockPanel1_Container.Controls.Add(Me.prgUbicacionesPorTramo)
        Me.DockPanel1_Container.Controls.Add(Me.prgTramos)
        Me.DockPanel1_Container.Controls.Add(Me.GroupControl2)
        Me.DockPanel1_Container.Controls.Add(Me.GroupControl1)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(4, 32)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(357, 746)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'cmdExportar
        '
        Me.dfttc.SetAllowHtmlText(Me.cmdExportar, DevExpress.Utils.DefaultBoolean.[Default])
        Me.cmdExportar.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.cmdExportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdExportar.Location = New System.Drawing.Point(0, 718)
        Me.cmdExportar.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdExportar.Name = "cmdExportar"
        Me.cmdExportar.Size = New System.Drawing.Size(357, 28)
        Me.cmdExportar.TabIndex = 6
        Me.cmdExportar.Text = "Exportar"
        Me.cmdExportar.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.dfttc.SetAllowHtmlText(Me.GroupBox1, DevExpress.Utils.DefaultBoolean.[Default])
        Me.GroupBox1.Controls.Add(Me.chkInvertir)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 584)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(357, 82)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        '
        'chkInvertir
        '
        Me.dfttc.SetAllowHtmlText(Me.chkInvertir, DevExpress.Utils.DefaultBoolean.[Default])
        Me.chkInvertir.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkInvertir.AutoSize = True
        Me.chkInvertir.Location = New System.Drawing.Point(75, 41)
        Me.chkInvertir.Margin = New System.Windows.Forms.Padding(4)
        Me.chkInvertir.Name = "chkInvertir"
        Me.chkInvertir.Size = New System.Drawing.Size(73, 21)
        Me.chkInvertir.TabIndex = 4
        Me.chkInvertir.Text = "Invertir"
        Me.chkInvertir.UseVisualStyleBackColor = True
        '
        'prgUbicacionesPorTramo
        '
        Me.prgUbicacionesPorTramo.Dock = System.Windows.Forms.DockStyle.Top
        Me.prgUbicacionesPorTramo.Location = New System.Drawing.Point(0, 562)
        Me.prgUbicacionesPorTramo.Margin = New System.Windows.Forms.Padding(4)
        Me.prgUbicacionesPorTramo.Name = "prgUbicacionesPorTramo"
        Me.prgUbicacionesPorTramo.Properties.ShowTitle = True
        Me.prgUbicacionesPorTramo.Properties.Step = 1
        Me.prgUbicacionesPorTramo.Size = New System.Drawing.Size(357, 22)
        Me.prgUbicacionesPorTramo.TabIndex = 3
        Me.prgUbicacionesPorTramo.Visible = False
        '
        'prgTramos
        '
        Me.prgTramos.Dock = System.Windows.Forms.DockStyle.Top
        Me.prgTramos.Location = New System.Drawing.Point(0, 540)
        Me.prgTramos.Margin = New System.Windows.Forms.Padding(4)
        Me.prgTramos.Name = "prgTramos"
        Me.prgTramos.Properties.ShowTitle = True
        Me.prgTramos.Properties.Step = 1
        Me.prgTramos.Size = New System.Drawing.Size(357, 22)
        Me.prgTramos.TabIndex = 2
        Me.prgTramos.Visible = False
        '
        'GroupControl2
        '
        Me.GroupControl2.Appearance.Options.UseBackColor = True
        Me.GroupControl2.Controls.Add(Me.cmdDibujarGrid)
        Me.GroupControl2.Controls.Add(Me.Label27)
        Me.GroupControl2.Controls.Add(Me.txtZoom)
        Me.GroupControl2.Controls.Add(Me.txtAncho)
        Me.GroupControl2.Controls.Add(Me.txtLargo)
        Me.GroupControl2.Controls.Add(lblAncho)
        Me.GroupControl2.Controls.Add(lblLargo)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl2.Location = New System.Drawing.Point(0, 276)
        Me.GroupControl2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(357, 264)
        Me.GroupControl2.TabIndex = 1
        Me.GroupControl2.Text = "Parámetros por bodega"
        Me.GroupControl2.ToolTipController = Me.dfttc.DefaultController
        '
        'cmdDibujarGrid
        '
        Me.dfttc.SetAllowHtmlText(Me.cmdDibujarGrid, DevExpress.Utils.DefaultBoolean.[Default])
        Me.cmdDibujarGrid.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.cmdDibujarGrid.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdDibujarGrid.Location = New System.Drawing.Point(2, 234)
        Me.cmdDibujarGrid.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdDibujarGrid.Name = "cmdDibujarGrid"
        Me.cmdDibujarGrid.Size = New System.Drawing.Size(353, 28)
        Me.cmdDibujarGrid.TabIndex = 6
        Me.cmdDibujarGrid.Text = "Dibujar Grid - 20211207"
        Me.cmdDibujarGrid.UseVisualStyleBackColor = True
        Me.cmdDibujarGrid.Visible = False
        '
        'Label27
        '
        Me.dfttc.SetAllowHtmlText(Me.Label27, DevExpress.Utils.DefaultBoolean.[Default])
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(75, 116)
        Me.Label27.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(49, 17)
        Me.Label27.TabIndex = 4
        Me.Label27.Text = "Zoom:"
        '
        'txtZoom
        '
        Me.dfttc.SetAllowHtmlText(Me.txtZoom, DevExpress.Utils.DefaultBoolean.[Default])
        Me.txtZoom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtZoom.DecimalPlaces = 2
        Me.txtZoom.Location = New System.Drawing.Point(133, 113)
        Me.txtZoom.Margin = New System.Windows.Forms.Padding(4)
        Me.txtZoom.Name = "txtZoom"
        Me.txtZoom.ReadOnly = True
        Me.txtZoom.Size = New System.Drawing.Size(176, 23)
        Me.txtZoom.TabIndex = 5
        Me.txtZoom.Value = New Decimal(New Integer() {20, 0, 0, 0})
        '
        'txtAncho
        '
        Me.dfttc.SetAllowHtmlText(Me.txtAncho, DevExpress.Utils.DefaultBoolean.[Default])
        Me.txtAncho.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAncho.DecimalPlaces = 3
        Me.txtAncho.Enabled = False
        Me.txtAncho.Location = New System.Drawing.Point(133, 80)
        Me.txtAncho.Margin = New System.Windows.Forms.Padding(4)
        Me.txtAncho.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtAncho.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.txtAncho.Name = "txtAncho"
        Me.txtAncho.ReadOnly = True
        Me.txtAncho.Size = New System.Drawing.Size(176, 23)
        Me.txtAncho.TabIndex = 3
        '
        'txtLargo
        '
        Me.dfttc.SetAllowHtmlText(Me.txtLargo, DevExpress.Utils.DefaultBoolean.[Default])
        Me.txtLargo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLargo.DecimalPlaces = 3
        Me.txtLargo.Enabled = False
        Me.txtLargo.Location = New System.Drawing.Point(133, 47)
        Me.txtLargo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtLargo.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtLargo.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.txtLargo.Name = "txtLargo"
        Me.txtLargo.ReadOnly = True
        Me.txtLargo.Size = New System.Drawing.Size(176, 23)
        Me.txtLargo.TabIndex = 1
        '
        'cmdGetStock
        '
        Me.dfttc.SetAllowHtmlText(Me.cmdGetStock, DevExpress.Utils.DefaultBoolean.[Default])
        Me.cmdGetStock.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.cmdGetStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdGetStock.Location = New System.Drawing.Point(3, 78)
        Me.cmdGetStock.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdGetStock.Name = "cmdGetStock"
        Me.cmdGetStock.Size = New System.Drawing.Size(347, 28)
        Me.cmdGetStock.TabIndex = 4
        Me.cmdGetStock.Text = "Mostrar Existencias"
        Me.cmdGetStock.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.dfttc.SetAllowHtmlText(Me.Label1, DevExpress.Utils.DefaultBoolean.[Default])
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Enabled = False
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.DimGray
        Me.Label1.Location = New System.Drawing.Point(3, 44)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Empresa"
        '
        'Label2
        '
        Me.dfttc.SetAllowHtmlText(Me.Label2, DevExpress.Utils.DefaultBoolean.[Default])
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Enabled = False
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.DimGray
        Me.Label2.Location = New System.Drawing.Point(3, 78)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 17)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Bodega"
        '
        'cmbBodegas
        '
        Me.dfttc.SetAllowHtmlText(Me.cmbBodegas, DevExpress.Utils.DefaultBoolean.[Default])
        Me.cmbBodegas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBodegas.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbBodegas.ForeColor = System.Drawing.Color.Navy
        Me.cmbBodegas.Location = New System.Drawing.Point(75, 74)
        Me.cmbBodegas.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbBodegas.Name = "cmbBodegas"
        Me.cmbBodegas.Size = New System.Drawing.Size(227, 25)
        Me.cmbBodegas.TabIndex = 3
        '
        'PanBorde
        '
        Me.dfttc.SetAllowHtmlText(Me.PanBorde, DevExpress.Utils.DefaultBoolean.[Default])
        Me.PanBorde.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.PanBorde.Appearance.Image = Global.TOMWMS.My.Resources.Resources.muro
        Me.PanBorde.Appearance.Options.UseBackColor = True
        Me.PanBorde.Appearance.Options.UseImage = True
        Me.PanBorde.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PanBorde.Controls.Add(Me.PanBodega)
        Me.PanBorde.Location = New System.Drawing.Point(22, 26)
        Me.PanBorde.Margin = New System.Windows.Forms.Padding(4)
        Me.PanBorde.Name = "PanBorde"
        Me.PanBorde.Size = New System.Drawing.Size(320, 405)
        Me.PanBorde.TabIndex = 1
        Me.PanBorde.Visible = False
        '
        'PanBodega
        '
        Me.dfttc.SetAllowHtmlText(Me.PanBodega, DevExpress.Utils.DefaultBoolean.[Default])
        Me.PanBodega.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.PanBodega.Location = New System.Drawing.Point(13, 12)
        Me.PanBodega.Margin = New System.Windows.Forms.Padding(4)
        Me.PanBodega.Name = "PanBodega"
        Me.PanBodega.Size = New System.Drawing.Size(267, 123)
        Me.PanBodega.TabIndex = 0
        '
        'XtraScrollableControl1
        '
        Me.dfttc.SetAllowHtmlText(Me.XtraScrollableControl1, DevExpress.Utils.DefaultBoolean.[Default])
        Me.XtraScrollableControl1.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.XtraScrollableControl1.Appearance.Options.UseBackColor = True
        Me.XtraScrollableControl1.Controls.Add(Me.txtCodigo)
        Me.XtraScrollableControl1.Controls.Add(Me.PanBorde)
        Me.XtraScrollableControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XtraScrollableControl1.Location = New System.Drawing.Point(367, 0)
        Me.XtraScrollableControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.XtraScrollableControl1.Name = "XtraScrollableControl1"
        Me.XtraScrollableControl1.Size = New System.Drawing.Size(818, 782)
        Me.XtraScrollableControl1.TabIndex = 0
        '
        'txtCodigo
        '
        Me.dfttc.SetAllowHtmlText(Me.txtCodigo, DevExpress.Utils.DefaultBoolean.[Default])
        Me.txtCodigo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCodigo.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtCodigo.Location = New System.Drawing.Point(0, 0)
        Me.txtCodigo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.Size = New System.Drawing.Size(818, 22)
        Me.txtCodigo.TabIndex = 0
        '
        'cmdActualizar
        '
        Me.dfttc.SetAllowHtmlText(Me.cmdActualizar, DevExpress.Utils.DefaultBoolean.[Default])
        Me.cmdActualizar.Dock = System.Windows.Forms.DockStyle.Top
        Me.cmdActualizar.Location = New System.Drawing.Point(3, 19)
        Me.cmdActualizar.Name = "cmdActualizar"
        Me.cmdActualizar.Size = New System.Drawing.Size(347, 39)
        Me.cmdActualizar.TabIndex = 5
        Me.cmdActualizar.Text = "Actualizar"
        Me.cmdActualizar.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.dfttc.SetAllowHtmlText(Me.GroupBox2, DevExpress.Utils.DefaultBoolean.[Default])
        Me.GroupBox2.Controls.Add(Me.cmdGetStock)
        Me.GroupBox2.Controls.Add(Me.cmdActualizar)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox2.Location = New System.Drawing.Point(2, 165)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(353, 109)
        Me.GroupBox2.TabIndex = 6
        Me.GroupBox2.TabStop = False
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.GroupBox2)
        Me.GroupControl1.Controls.Add(Me.cmbEmpresa)
        Me.GroupControl1.Controls.Add(Me.Label1)
        Me.GroupControl1.Controls.Add(Me.Label2)
        Me.GroupControl1.Controls.Add(Me.cmbBodegas)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(357, 276)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Datos Generales"
        Me.GroupControl1.ToolTipController = Me.dfttc.DefaultController
        '
        'cmbEmpresa
        '
        Me.cmbEmpresa.Location = New System.Drawing.Point(75, 41)
        Me.cmbEmpresa.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbEmpresa.Name = "cmbEmpresa"
        Me.cmbEmpresa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbEmpresa.Properties.NullText = ""
        Me.cmbEmpresa.Size = New System.Drawing.Size(228, 22)
        Me.cmbEmpresa.TabIndex = 1
        '
        'frmDiseñoB
        '
        Me.dfttc.SetAllowHtmlText(Me, DevExpress.Utils.DefaultBoolean.[Default])
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1185, 782)
        Me.Controls.Add(Me.XtraScrollableControl1)
        Me.Controls.Add(Me.DockPanel1)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmDiseñoB"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Graficador de bodega"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DockPanel1.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.prgUbicacionesPorTramo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.prgTramos.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.GroupControl2.PerformLayout()
        CType(Me.txtZoom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAncho, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLargo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanBorde, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanBorde.ResumeLayout(False)
        CType(Me.PanBodega, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraScrollableControl1.ResumeLayout(False)
        Me.XtraScrollableControl1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.cmbEmpresa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents prgUbicacionesPorTramo As DevExpress.XtraEditors.ProgressBarControl
    Friend WithEvents prgTramos As DevExpress.XtraEditors.ProgressBarControl
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents Label27 As Label
    Friend WithEvents txtZoom As NumericUpDown
    Friend WithEvents txtAncho As NumericUpDown
    Friend WithEvents txtLargo As NumericUpDown
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents cmbBodegas As ComboBox
    Friend WithEvents PanBorde As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanBodega As DevExpress.XtraEditors.PanelControl
    Friend WithEvents XtraScrollableControl1 As DevExpress.XtraEditors.XtraScrollableControl
    Friend WithEvents cmbEmpresa As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents txtCodigo As TextBox
    Friend WithEvents cmdGetStock As Button
    Friend WithEvents chkInvertir As CheckBox
    Friend WithEvents dfttc As DevExpress.Utils.DefaultToolTipController
    Friend WithEvents cmdActualizar As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents cmdExportar As Button
    Friend WithEvents sfd As SaveFileDialog
    Friend WithEvents cmdDibujarGrid As Button
End Class
