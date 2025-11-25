<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVerificacionBOF
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
        Dim Label8 As System.Windows.Forms.Label
        Dim lblBarraProducto As System.Windows.Forms.Label
        Dim Label5 As System.Windows.Forms.Label
        Dim lblScan As System.Windows.Forms.Label
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim GridFormatRule1 As DevExpress.XtraGrid.GridFormatRule = New DevExpress.XtraGrid.GridFormatRule()
        Dim Label1 As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVerificacionBOF))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.grpScan = New DevExpress.XtraEditors.GroupControl()
        Me.txtLote = New DevExpress.XtraEditors.TextEdit()
        Me.txtDescripcionProducto = New DevExpress.XtraEditors.TextEdit()
        Me.txtScanner = New DevExpress.XtraEditors.TextEdit()
        Me.txtCantidad = New DevExpress.XtraEditors.TextEdit()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.dgridListaPedido = New DevExpress.XtraGrid.GridControl()
        Me.gvListaPedido = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView6 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.txtTalla = New DevExpress.XtraEditors.TextEdit()
        Me.txtColor = New DevExpress.XtraEditors.TextEdit()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.SvgImageBox1 = New DevExpress.XtraEditors.SvgImageBox()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Label8 = New System.Windows.Forms.Label()
        lblBarraProducto = New System.Windows.Forms.Label()
        Label5 = New System.Windows.Forms.Label()
        lblScan = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpScan, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpScan.SuspendLayout()
        CType(Me.txtLote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescripcionProducto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtScanner.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCantidad.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.dgridListaPedido, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvListaPedido, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTalla.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtColor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.SvgImageBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 1
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.Size = New System.Drawing.Size(1547, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "RibbonPage1"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        Me.RibbonPageGroup1.Text = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 763)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1547, 30)
        '
        'grpScan
        '
        Me.grpScan.AppearanceCaption.BackColor = System.Drawing.Color.SteelBlue
        Me.grpScan.AppearanceCaption.Options.UseBackColor = True
        Me.grpScan.AppearanceCaption.Options.UseTextOptions = True
        Me.grpScan.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.grpScan.Controls.Add(Me.txtCantidad)
        Me.grpScan.Controls.Add(Me.txtScanner)
        Me.grpScan.Controls.Add(Label5)
        Me.grpScan.Controls.Add(Me.txtColor)
        Me.grpScan.Controls.Add(lblScan)
        Me.grpScan.Controls.Add(Label2)
        Me.grpScan.Controls.Add(Label8)
        Me.grpScan.Controls.Add(Label1)
        Me.grpScan.Controls.Add(Me.txtLote)
        Me.grpScan.Controls.Add(Me.txtTalla)
        Me.grpScan.Controls.Add(lblBarraProducto)
        Me.grpScan.Controls.Add(Me.txtDescripcionProducto)
        Me.grpScan.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpScan.Location = New System.Drawing.Point(2, 2)
        Me.grpScan.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.grpScan.Name = "grpScan"
        Me.grpScan.Size = New System.Drawing.Size(988, 280)
        Me.grpScan.TabIndex = 52
        '
        'Label8
        '
        Label8.AutoSize = True
        Label8.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label8.Location = New System.Drawing.Point(14, 80)
        Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label8.Name = "Label8"
        Label8.Size = New System.Drawing.Size(102, 21)
        Label8.TabIndex = 52
        Label8.Text = "Descripcion:"
        '
        'txtLote
        '
        Me.txtLote.Location = New System.Drawing.Point(124, 115)
        Me.txtLote.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtLote.Name = "txtLote"
        Me.txtLote.Properties.Appearance.BackColor = System.Drawing.Color.Wheat
        Me.txtLote.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLote.Properties.Appearance.Options.UseBackColor = True
        Me.txtLote.Properties.Appearance.Options.UseFont = True
        Me.txtLote.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtLote.Properties.MaxLength = 50
        Me.txtLote.Size = New System.Drawing.Size(277, 28)
        Me.txtLote.TabIndex = 50
        '
        'lblBarraProducto
        '
        lblBarraProducto.AutoSize = True
        lblBarraProducto.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblBarraProducto.Location = New System.Drawing.Point(14, 118)
        lblBarraProducto.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblBarraProducto.Name = "lblBarraProducto"
        lblBarraProducto.Size = New System.Drawing.Size(48, 21)
        lblBarraProducto.TabIndex = 51
        lblBarraProducto.Text = "Lote:"
        '
        'txtDescripcionProducto
        '
        Me.txtDescripcionProducto.Location = New System.Drawing.Point(124, 77)
        Me.txtDescripcionProducto.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtDescripcionProducto.Name = "txtDescripcionProducto"
        Me.txtDescripcionProducto.Properties.Appearance.BackColor = System.Drawing.Color.Wheat
        Me.txtDescripcionProducto.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescripcionProducto.Properties.Appearance.Options.UseBackColor = True
        Me.txtDescripcionProducto.Properties.Appearance.Options.UseFont = True
        Me.txtDescripcionProducto.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtDescripcionProducto.Properties.MaxLength = 50
        Me.txtDescripcionProducto.Size = New System.Drawing.Size(277, 28)
        Me.txtDescripcionProducto.TabIndex = 48
        '
        'Label5
        '
        Label5.AutoSize = True
        Label5.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label5.Location = New System.Drawing.Point(14, 232)
        Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(81, 21)
        Label5.TabIndex = 49
        Label5.Text = "Cantidad:"
        '
        'txtScanner
        '
        Me.txtScanner.Location = New System.Drawing.Point(124, 39)
        Me.txtScanner.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtScanner.Name = "txtScanner"
        Me.txtScanner.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtScanner.Properties.Appearance.Options.UseFont = True
        Me.txtScanner.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtScanner.Properties.MaxLength = 50
        Me.txtScanner.Size = New System.Drawing.Size(277, 28)
        Me.txtScanner.TabIndex = 42
        '
        'txtCantidad
        '
        Me.txtCantidad.Location = New System.Drawing.Point(124, 229)
        Me.txtCantidad.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCantidad.Properties.Appearance.Options.UseFont = True
        Me.txtCantidad.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtCantidad.Properties.MaxLength = 50
        Me.txtCantidad.Size = New System.Drawing.Size(277, 28)
        Me.txtCantidad.TabIndex = 48
        '
        'lblScan
        '
        lblScan.AutoSize = True
        lblScan.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblScan.Location = New System.Drawing.Point(16, 46)
        lblScan.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblScan.Name = "lblScan"
        lblScan.Size = New System.Drawing.Size(66, 21)
        lblScan.TabIndex = 43
        lblScan.Text = "Código:"
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.dgridListaPedido)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupControl2.Location = New System.Drawing.Point(2, 286)
        Me.GroupControl2.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(988, 282)
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
        Me.dgridListaPedido.Size = New System.Drawing.Size(984, 252)
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
        'txtTalla
        '
        Me.txtTalla.Location = New System.Drawing.Point(124, 153)
        Me.txtTalla.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtTalla.Name = "txtTalla"
        Me.txtTalla.Properties.Appearance.BackColor = System.Drawing.Color.Wheat
        Me.txtTalla.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTalla.Properties.Appearance.Options.UseBackColor = True
        Me.txtTalla.Properties.Appearance.Options.UseFont = True
        Me.txtTalla.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtTalla.Properties.MaxLength = 50
        Me.txtTalla.Size = New System.Drawing.Size(277, 28)
        Me.txtTalla.TabIndex = 53
        '
        'txtColor
        '
        Me.txtColor.Location = New System.Drawing.Point(124, 191)
        Me.txtColor.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtColor.Name = "txtColor"
        Me.txtColor.Properties.Appearance.BackColor = System.Drawing.Color.Wheat
        Me.txtColor.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtColor.Properties.Appearance.Options.UseBackColor = True
        Me.txtColor.Properties.Appearance.Options.UseFont = True
        Me.txtColor.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtColor.Properties.MaxLength = 50
        Me.txtColor.Size = New System.Drawing.Size(277, 28)
        Me.txtColor.TabIndex = 54
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label1.Location = New System.Drawing.Point(16, 156)
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
        Label2.Location = New System.Drawing.Point(14, 194)
        Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(54, 21)
        Label2.TabIndex = 56
        Label2.Text = "Color:"
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.SvgImageBox1)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Right
        Me.PanelControl1.Location = New System.Drawing.Point(992, 193)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(555, 570)
        Me.PanelControl1.TabIndex = 58
        '
        'SvgImageBox1
        '
        Me.SvgImageBox1.Location = New System.Drawing.Point(19, 20)
        Me.SvgImageBox1.Name = "SvgImageBox1"
        Me.SvgImageBox1.Size = New System.Drawing.Size(524, 533)
        Me.SvgImageBox1.SvgImage = CType(resources.GetObject("SvgImageBox1.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.SvgImageBox1.TabIndex = 0
        Me.SvgImageBox1.Text = "SvgImageBox1"
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.grpScan)
        Me.PanelControl2.Controls.Add(Me.GroupControl2)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl2.Location = New System.Drawing.Point(0, 193)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(992, 570)
        Me.PanelControl2.TabIndex = 59
        '
        'frmVerificacionBOF
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1547, 793)
        Me.Controls.Add(Me.PanelControl2)
        Me.Controls.Add(Me.PanelControl1)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frmVerificacionBOF"
        Me.Ribbon = Me.RibbonControl
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "frmVerificacionBOF"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpScan, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpScan.ResumeLayout(False)
        Me.grpScan.PerformLayout()
        CType(Me.txtLote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescripcionProducto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtScanner.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCantidad.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.dgridListaPedido, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvListaPedido, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTalla.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtColor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.SvgImageBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents grpScan As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtLote As DevExpress.XtraEditors.TextEdit
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
    Friend WithEvents SvgImageBox1 As DevExpress.XtraEditors.SvgImageBox
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
End Class
