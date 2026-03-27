<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmVerificacionBOF
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
        Dim Label8 As System.Windows.Forms.Label
        Dim Label5 As System.Windows.Forms.Label
        Dim lblScan As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVerificacionBOF))
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim GridFormatRule1 As DevExpress.XtraGrid.GridFormatRule = New DevExpress.XtraGrid.GridFormatRule()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.cmdGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.grpScan = New DevExpress.XtraEditors.GroupControl()
        Me.lbPedidos = New DevExpress.XtraEditors.LabelControl()
        Me.cmbMotivo = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbEstado = New DevExpress.XtraEditors.LookUpEdit()
        Me.lbOk = New DevExpress.XtraEditors.LabelControl()
        Me.txtCantidad = New DevExpress.XtraEditors.TextEdit()
        Me.txtScanner = New DevExpress.XtraEditors.TextEdit()
        Me.txtColor = New DevExpress.XtraEditors.TextEdit()
        Me.txtTalla = New DevExpress.XtraEditors.TextEdit()
        Me.txtDescripcionProducto = New DevExpress.XtraEditors.TextEdit()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.dgridListaPedido = New DevExpress.XtraGrid.GridControl()
        Me.gvListaPedido = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView6 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.peProducto = New DevExpress.XtraEditors.PictureEdit()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Label8 = New System.Windows.Forms.Label()
        Label5 = New System.Windows.Forms.Label()
        lblScan = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpScan, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpScan.SuspendLayout()
        CType(Me.cmbMotivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbEstado.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCantidad.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtScanner.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtColor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTalla.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescripcionProducto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.dgridListaPedido, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvListaPedido, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.peProducto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label8
        '
        Label8.AutoSize = True
        Label8.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label8.Location = New System.Drawing.Point(7, 81)
        Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label8.Name = "Label8"
        Label8.Size = New System.Drawing.Size(102, 21)
        Label8.TabIndex = 52
        Label8.Text = "Descripcion:"
        '
        'Label5
        '
        Label5.AutoSize = True
        Label5.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label5.Location = New System.Drawing.Point(552, 157)
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
        lblScan.Location = New System.Drawing.Point(7, 47)
        lblScan.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblScan.Name = "lblScan"
        lblScan.Size = New System.Drawing.Size(46, 21)
        lblScan.TabIndex = 43
        lblScan.Text = "SKU:"
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label1.Location = New System.Drawing.Point(552, 81)
        Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(52, 21)
        Label1.TabIndex = 55
        Label1.Text = "Talla:"
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label2.Location = New System.Drawing.Point(552, 119)
        Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(54, 21)
        Label2.TabIndex = 56
        Label2.Text = "Color:"
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label3.Location = New System.Drawing.Point(7, 118)
        Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(67, 21)
        Label3.TabIndex = 61
        Label3.Text = "Estado:"
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label4.Location = New System.Drawing.Point(7, 152)
        Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(65, 21)
        Label4.TabIndex = 62
        Label4.Text = "Motivo:"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.cmdGuardar})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 2
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.Size = New System.Drawing.Size(1547, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'cmdGuardar
        '
        Me.cmdGuardar.Caption = "Guardar"
        Me.cmdGuardar.Id = 1
        Me.cmdGuardar.ImageOptions.SvgImage = CType(resources.GetObject("cmdGuardar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdGuardar.Name = "cmdGuardar"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Verificación BOF"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdGuardar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 940)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1547, 30)
        '
        'grpScan
        '
        Me.grpScan.AppearanceCaption.BackColor = System.Drawing.Color.SteelBlue
        Me.grpScan.AppearanceCaption.Options.UseBackColor = True
        Me.grpScan.Controls.Add(Me.lbPedidos)
        Me.grpScan.Controls.Add(Label4)
        Me.grpScan.Controls.Add(Label3)
        Me.grpScan.Controls.Add(Me.cmbMotivo)
        Me.grpScan.Controls.Add(Me.cmbEstado)
        Me.grpScan.Controls.Add(Me.lbOk)
        Me.grpScan.Controls.Add(Me.txtCantidad)
        Me.grpScan.Controls.Add(Me.txtScanner)
        Me.grpScan.Controls.Add(Label5)
        Me.grpScan.Controls.Add(Me.txtColor)
        Me.grpScan.Controls.Add(lblScan)
        Me.grpScan.Controls.Add(Label2)
        Me.grpScan.Controls.Add(Label8)
        Me.grpScan.Controls.Add(Label1)
        Me.grpScan.Controls.Add(Me.txtTalla)
        Me.grpScan.Controls.Add(Me.txtDescripcionProducto)
        Me.grpScan.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpScan.Location = New System.Drawing.Point(4, 2)
        Me.grpScan.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.grpScan.Name = "grpScan"
        Me.grpScan.Size = New System.Drawing.Size(911, 299)
        Me.grpScan.TabIndex = 52
        Me.grpScan.Text = "Producto"
        '
        'lbPedidos
        '
        Me.lbPedidos.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lbPedidos.Appearance.Options.UseFont = True
        Me.lbPedidos.Location = New System.Drawing.Point(820, 40)
        Me.lbPedidos.Name = "lbPedidos"
        Me.lbPedidos.Size = New System.Drawing.Size(86, 24)
        Me.lbPedidos.TabIndex = 64
        Me.lbPedidos.Text = "Pedidos:"
        '
        'cmbMotivo
        '
        Me.cmbMotivo.Location = New System.Drawing.Point(115, 152)
        Me.cmbMotivo.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.cmbMotivo.MenuManager = Me.RibbonControl
        Me.cmbMotivo.Name = "cmbMotivo"
        Me.cmbMotivo.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.cmbMotivo.Properties.Appearance.Options.UseFont = True
        Me.cmbMotivo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbMotivo.Properties.NullText = ""
        Me.cmbMotivo.Size = New System.Drawing.Size(376, 24)
        Me.cmbMotivo.TabIndex = 59
        '
        'cmbEstado
        '
        Me.cmbEstado.Location = New System.Drawing.Point(115, 117)
        Me.cmbEstado.Margin = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.cmbEstado.MenuManager = Me.RibbonControl
        Me.cmbEstado.Name = "cmbEstado"
        Me.cmbEstado.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.cmbEstado.Properties.Appearance.Options.UseFont = True
        Me.cmbEstado.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbEstado.Properties.NullText = ""
        Me.cmbEstado.Size = New System.Drawing.Size(376, 24)
        Me.cmbEstado.TabIndex = 58
        '
        'lbOk
        '
        Me.lbOk.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbOk.Appearance.Image = CType(resources.GetObject("lbOk.Appearance.Image"), System.Drawing.Image)
        Me.lbOk.Appearance.Options.UseFont = True
        Me.lbOk.Appearance.Options.UseImage = True
        Me.lbOk.ImageOptions.Image = CType(resources.GetObject("lbOk.ImageOptions.Image"), System.Drawing.Image)
        Me.lbOk.Location = New System.Drawing.Point(499, 38)
        Me.lbOk.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.lbOk.Name = "lbOk"
        Me.lbOk.Size = New System.Drawing.Size(32, 32)
        Me.lbOk.TabIndex = 57
        '
        'txtCantidad
        '
        Me.txtCantidad.Location = New System.Drawing.Point(629, 154)
        Me.txtCantidad.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.Properties.Appearance.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtCantidad.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCantidad.Properties.Appearance.Options.UseBackColor = True
        Me.txtCantidad.Properties.Appearance.Options.UseFont = True
        Me.txtCantidad.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtCantidad.Properties.MaxLength = 50
        Me.txtCantidad.Size = New System.Drawing.Size(169, 28)
        Me.txtCantidad.TabIndex = 48
        '
        'txtScanner
        '
        Me.txtScanner.Location = New System.Drawing.Point(115, 40)
        Me.txtScanner.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtScanner.Name = "txtScanner"
        Me.txtScanner.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtScanner.Properties.Appearance.Options.UseFont = True
        Me.txtScanner.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtScanner.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtScanner.Properties.MaxLength = 50
        Me.txtScanner.Size = New System.Drawing.Size(376, 28)
        Me.txtScanner.TabIndex = 42
        '
        'txtColor
        '
        Me.txtColor.Location = New System.Drawing.Point(629, 116)
        Me.txtColor.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtColor.Name = "txtColor"
        Me.txtColor.Properties.Appearance.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtColor.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtColor.Properties.Appearance.Options.UseBackColor = True
        Me.txtColor.Properties.Appearance.Options.UseFont = True
        Me.txtColor.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtColor.Properties.MaxLength = 50
        Me.txtColor.Size = New System.Drawing.Size(169, 28)
        Me.txtColor.TabIndex = 54
        '
        'txtTalla
        '
        Me.txtTalla.Location = New System.Drawing.Point(629, 78)
        Me.txtTalla.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtTalla.Name = "txtTalla"
        Me.txtTalla.Properties.Appearance.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtTalla.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTalla.Properties.Appearance.Options.UseBackColor = True
        Me.txtTalla.Properties.Appearance.Options.UseFont = True
        Me.txtTalla.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtTalla.Properties.MaxLength = 50
        Me.txtTalla.Size = New System.Drawing.Size(169, 28)
        Me.txtTalla.TabIndex = 53
        '
        'txtDescripcionProducto
        '
        Me.txtDescripcionProducto.Location = New System.Drawing.Point(115, 78)
        Me.txtDescripcionProducto.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtDescripcionProducto.Name = "txtDescripcionProducto"
        Me.txtDescripcionProducto.Properties.Appearance.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtDescripcionProducto.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescripcionProducto.Properties.Appearance.Options.UseBackColor = True
        Me.txtDescripcionProducto.Properties.Appearance.Options.UseFont = True
        Me.txtDescripcionProducto.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtDescripcionProducto.Properties.MaxLength = 50
        Me.txtDescripcionProducto.Size = New System.Drawing.Size(376, 28)
        Me.txtDescripcionProducto.TabIndex = 48
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.dgridListaPedido)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl2.Location = New System.Drawing.Point(4, 305)
        Me.GroupControl2.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(911, 436)
        Me.GroupControl2.TabIndex = 53
        Me.GroupControl2.Text = "Lista"
        '
        'dgridListaPedido
        '
        Me.dgridListaPedido.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgridListaPedido.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridListaPedido.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode1.RelationName = "Level1"
        Me.dgridListaPedido.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.dgridListaPedido.Location = New System.Drawing.Point(2, 28)
        Me.dgridListaPedido.MainView = Me.gvListaPedido
        Me.dgridListaPedido.Margin = New System.Windows.Forms.Padding(4)
        Me.dgridListaPedido.MenuManager = Me.RibbonControl
        Me.dgridListaPedido.Name = "dgridListaPedido"
        Me.dgridListaPedido.Size = New System.Drawing.Size(907, 406)
        Me.dgridListaPedido.TabIndex = 1
        Me.dgridListaPedido.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvListaPedido, Me.GridView6})
        '
        'gvListaPedido
        '
        Me.gvListaPedido.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvListaPedido.Appearance.HeaderPanel.Options.UseFont = True
        Me.gvListaPedido.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvListaPedido.Appearance.Row.Options.UseFont = True
        Me.gvListaPedido.DetailHeight = 431
        GridFormatRule1.Name = "Format0"
        GridFormatRule1.Rule = Nothing
        Me.gvListaPedido.FormatRules.Add(GridFormatRule1)
        Me.gvListaPedido.GridControl = Me.dgridListaPedido
        Me.gvListaPedido.Name = "gvListaPedido"
        Me.gvListaPedido.OptionsBehavior.Editable = False
        Me.gvListaPedido.OptionsView.ColumnAutoWidth = False
        Me.gvListaPedido.OptionsView.ShowAutoFilterRow = True
        Me.gvListaPedido.OptionsView.ShowFooter = True
        Me.gvListaPedido.OptionsView.ShowGroupPanel = False
        '
        'GridView6
        '
        Me.GridView6.GridControl = Me.dgridListaPedido
        Me.GridView6.Name = "GridView6"
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.peProducto)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Right
        Me.PanelControl1.Location = New System.Drawing.Point(923, 193)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(624, 747)
        Me.PanelControl1.TabIndex = 58
        '
        'peProducto
        '
        Me.peProducto.Dock = System.Windows.Forms.DockStyle.Fill
        Me.peProducto.Location = New System.Drawing.Point(2, 2)
        Me.peProducto.MenuManager = Me.RibbonControl
        Me.peProducto.Name = "peProducto"
        Me.peProducto.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.[Auto]
        Me.peProducto.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch
        Me.peProducto.Size = New System.Drawing.Size(620, 743)
        Me.peProducto.TabIndex = 0
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.TableLayoutPanel1)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl2.Location = New System.Drawing.Point(0, 193)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(923, 747)
        Me.PanelControl2.TabIndex = 59
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.grpScan, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupControl2, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40.78062!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 59.21938!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(919, 743)
        Me.TableLayoutPanel1.TabIndex = 54
        '
        'frmVerificacionBOF
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1547, 970)
        Me.Controls.Add(Me.PanelControl2)
        Me.Controls.Add(Me.PanelControl1)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frmVerificacionBOF"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Verificación BOF"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpScan, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpScan.ResumeLayout(False)
        Me.grpScan.PerformLayout()
        CType(Me.cmbMotivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbEstado.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCantidad.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtScanner.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtColor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTalla.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescripcionProducto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.dgridListaPedido, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvListaPedido, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.peProducto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents grpScan As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtDescripcionProducto As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtScanner As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCantidad As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtColor As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtTalla As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dgridListaPedido As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvListaPedido As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView6 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents peProducto As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents lbOk As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmdGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmbMotivo As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbEstado As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lbPedidos As DevExpress.XtraEditors.LabelControl
End Class
