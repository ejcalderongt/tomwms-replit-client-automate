<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProducto
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProducto))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.cmdGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.lblCorrelativo = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.lblFinca = New DevExpress.XtraEditors.LabelControl()
        Me.cmbFinca = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtDescripcion = New DevExpress.XtraEditors.TextEdit()
        Me.lblDescripcion = New DevExpress.XtraEditors.LabelControl()
        Me.txtCodigo = New DevExpress.XtraEditors.TextEdit()
        Me.lblIP = New DevExpress.XtraEditors.LabelControl()
        Me.chkActivo = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.txtImgProducto = New System.Windows.Forms.RichTextBox()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.cmbFinca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescripcion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodigo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.cmdGuardar, Me.cmdActualizar, Me.chkActivo})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 4
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1267, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'cmdGuardar
        '
        Me.cmdGuardar.Caption = "Guardar"
        Me.cmdGuardar.Id = 1
        Me.cmdGuardar.ImageOptions.SvgImage = CType(resources.GetObject("cmdGuardar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdGuardar.Name = "cmdGuardar"
        '
        'cmdActualizar
        '
        Me.cmdActualizar.Caption = "Actualizar"
        Me.cmdActualizar.Id = 2
        Me.cmdActualizar.ImageOptions.SvgImage = CType(resources.GetObject("cmdActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdActualizar.Name = "cmdActualizar"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Producto"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdGuardar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.chkActivo)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 796)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1267, 30)
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.txtImgProducto)
        Me.GroupControl1.Controls.Add(Me.LabelControl1)
        Me.GroupControl1.Controls.Add(Me.lblCorrelativo)
        Me.GroupControl1.Controls.Add(Me.LabelControl2)
        Me.GroupControl1.Controls.Add(Me.lblFinca)
        Me.GroupControl1.Controls.Add(Me.cmbFinca)
        Me.GroupControl1.Controls.Add(Me.txtDescripcion)
        Me.GroupControl1.Controls.Add(Me.lblDescripcion)
        Me.GroupControl1.Controls.Add(Me.txtCodigo)
        Me.GroupControl1.Controls.Add(Me.lblIP)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 193)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1267, 603)
        Me.GroupControl1.TabIndex = 3
        Me.GroupControl1.Text = "Configurar producto"
        '
        'lblCorrelativo
        '
        Me.lblCorrelativo.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCorrelativo.Appearance.Options.UseFont = True
        Me.lblCorrelativo.Location = New System.Drawing.Point(242, 57)
        Me.lblCorrelativo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblCorrelativo.Name = "lblCorrelativo"
        Me.lblCorrelativo.Size = New System.Drawing.Size(18, 38)
        Me.lblCorrelativo.TabIndex = 53
        Me.lblCorrelativo.Text = "0"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Location = New System.Drawing.Point(36, 57)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(167, 38)
        Me.LabelControl2.TabIndex = 52
        Me.LabelControl2.Text = "Correlativo:"
        '
        'lblFinca
        '
        Me.lblFinca.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFinca.Appearance.Options.UseFont = True
        Me.lblFinca.Location = New System.Drawing.Point(36, 124)
        Me.lblFinca.LookAndFeel.SkinName = "DevExpress Dark Style"
        Me.lblFinca.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.lblFinca.LookAndFeel.UseDefaultLookAndFeel = False
        Me.lblFinca.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblFinca.Name = "lblFinca"
        Me.lblFinca.Size = New System.Drawing.Size(89, 38)
        Me.lblFinca.TabIndex = 51
        Me.lblFinca.Text = "Finca:"
        '
        'cmbFinca
        '
        Me.cmbFinca.Location = New System.Drawing.Point(242, 118)
        Me.cmbFinca.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbFinca.MenuManager = Me.RibbonControl
        Me.cmbFinca.Name = "cmbFinca"
        Me.cmbFinca.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbFinca.Properties.Appearance.Options.UseFont = True
        Me.cmbFinca.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbFinca.Properties.NullText = ""
        Me.cmbFinca.Properties.PopupFormMinSize = New System.Drawing.Size(600, 200)
        Me.cmbFinca.Properties.PopupWidth = 100
        Me.cmbFinca.Size = New System.Drawing.Size(396, 44)
        Me.cmbFinca.TabIndex = 50
        '
        'txtDescripcion
        '
        Me.txtDescripcion.EditValue = ""
        Me.txtDescripcion.Location = New System.Drawing.Point(242, 280)
        Me.txtDescripcion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtDescripcion.MenuManager = Me.RibbonControl
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescripcion.Properties.Appearance.Options.UseFont = True
        Me.txtDescripcion.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.txtDescripcion.Size = New System.Drawing.Size(985, 46)
        Me.txtDescripcion.TabIndex = 47
        '
        'lblDescripcion
        '
        Me.lblDescripcion.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescripcion.Appearance.Options.UseFont = True
        Me.lblDescripcion.Location = New System.Drawing.Point(36, 284)
        Me.lblDescripcion.LookAndFeel.SkinName = "DevExpress Dark Style"
        Me.lblDescripcion.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.lblDescripcion.LookAndFeel.UseDefaultLookAndFeel = False
        Me.lblDescripcion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblDescripcion.Name = "lblDescripcion"
        Me.lblDescripcion.Size = New System.Drawing.Size(181, 38)
        Me.lblDescripcion.TabIndex = 46
        Me.lblDescripcion.Text = "Descripción:"
        '
        'txtCodigo
        '
        Me.txtCodigo.EditValue = ""
        Me.txtCodigo.Location = New System.Drawing.Point(242, 196)
        Me.txtCodigo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCodigo.MenuManager = Me.RibbonControl
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCodigo.Properties.Appearance.Options.UseFont = True
        Me.txtCodigo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.txtCodigo.Size = New System.Drawing.Size(396, 46)
        Me.txtCodigo.TabIndex = 44
        '
        'lblIP
        '
        Me.lblIP.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIP.Appearance.Options.UseFont = True
        Me.lblIP.Location = New System.Drawing.Point(36, 200)
        Me.lblIP.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblIP.Name = "lblIP"
        Me.lblIP.Size = New System.Drawing.Size(112, 38)
        Me.lblIP.TabIndex = 45
        Me.lblIP.Text = "Código:"
        '
        'chkActivo
        '
        Me.chkActivo.Caption = "Activo"
        Me.chkActivo.Id = 3
        Me.chkActivo.Name = "chkActivo"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Location = New System.Drawing.Point(36, 358)
        Me.LabelControl1.LookAndFeel.SkinName = "DevExpress Dark Style"
        Me.LabelControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.LabelControl1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(181, 38)
        Me.LabelControl1.TabIndex = 54
        Me.LabelControl1.Text = "Descripción:"
        '
        'txtImgProducto
        '
        Me.txtImgProducto.Location = New System.Drawing.Point(242, 345)
        Me.txtImgProducto.Name = "txtImgProducto"
        Me.txtImgProducto.Size = New System.Drawing.Size(983, 228)
        Me.txtImgProducto.TabIndex = 55
        Me.txtImgProducto.Text = ""
        '
        'frmProducto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1267, 826)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frmProducto"
        Me.Ribbon = Me.RibbonControl
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Producto"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.cmbFinca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescripcion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodigo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtDescripcion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblDescripcion As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtCodigo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblIP As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblFinca As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbFinca As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmdGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lblCorrelativo As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents chkActivo As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents txtImgProducto As RichTextBox
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
End Class
