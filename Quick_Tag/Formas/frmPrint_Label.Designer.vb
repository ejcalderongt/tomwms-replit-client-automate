<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrint_Label
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrint_Label))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.txtCajaHasta = New DevExpress.XtraEditors.SpinEdit()
        Me.txtCajaDesde = New DevExpress.XtraEditors.SpinEdit()
        Me.lblFinca = New DevExpress.XtraEditors.LabelControl()
        Me.gbErrores = New System.Windows.Forms.GroupBox()
        Me.lblPrg = New System.Windows.Forms.RichTextBox()
        Me.prg = New System.Windows.Forms.ProgressBar()
        Me.lblTLog = New DevExpress.XtraEditors.LabelControl()
        Me.txtPalletHasta = New DevExpress.XtraEditors.SpinEdit()
        Me.PictureEdit1 = New DevExpress.XtraEditors.PictureEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.txtLote = New DevExpress.XtraEditors.TextEdit()
        Me.lblCaja = New DevExpress.XtraEditors.LabelControl()
        Me.txtPalletDesde = New DevExpress.XtraEditors.SpinEdit()
        Me.lblLote = New DevExpress.XtraEditors.LabelControl()
        Me.cmbFinca = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.lblNombreFinca = New DevExpress.XtraEditors.LabelControl()
        Me.cmbImpresoras = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblPallet = New DevExpress.XtraEditors.LabelControl()
        Me.dtPackDate = New DevExpress.XtraEditors.DateEdit()
        Me.btnPrintRollo = New DevExpress.XtraEditors.SimpleButton()
        Me.lblPackDate = New DevExpress.XtraEditors.LabelControl()
        Me.btPrintIndividual = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.txtCajaHasta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCajaDesde.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbErrores.SuspendLayout()
        CType(Me.txtPalletHasta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPalletDesde.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbFinca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbImpresoras.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtPackDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtPackDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.RibbonControl.Size = New System.Drawing.Size(1408, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        Me.RibbonControl.Visible = False
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
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 916)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1408, 30)
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.txtCajaHasta)
        Me.GroupControl1.Controls.Add(Me.txtCajaDesde)
        Me.GroupControl1.Controls.Add(Me.lblFinca)
        Me.GroupControl1.Controls.Add(Me.gbErrores)
        Me.GroupControl1.Controls.Add(Me.txtPalletHasta)
        Me.GroupControl1.Controls.Add(Me.PictureEdit1)
        Me.GroupControl1.Controls.Add(Me.LabelControl2)
        Me.GroupControl1.Controls.Add(Me.txtLote)
        Me.GroupControl1.Controls.Add(Me.lblCaja)
        Me.GroupControl1.Controls.Add(Me.txtPalletDesde)
        Me.GroupControl1.Controls.Add(Me.lblLote)
        Me.GroupControl1.Controls.Add(Me.cmbFinca)
        Me.GroupControl1.Controls.Add(Me.LabelControl3)
        Me.GroupControl1.Controls.Add(Me.lblNombreFinca)
        Me.GroupControl1.Controls.Add(Me.cmbImpresoras)
        Me.GroupControl1.Controls.Add(Me.lblPallet)
        Me.GroupControl1.Controls.Add(Me.dtPackDate)
        Me.GroupControl1.Controls.Add(Me.btnPrintRollo)
        Me.GroupControl1.Controls.Add(Me.lblPackDate)
        Me.GroupControl1.Controls.Add(Me.btPrintIndividual)
        Me.GroupControl1.Controls.Add(Me.LabelControl1)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 193)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1408, 723)
        Me.GroupControl1.TabIndex = 6
        Me.GroupControl1.Text = "Parametros de Impresión"
        '
        'txtCajaHasta
        '
        Me.txtCajaHasta.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtCajaHasta.Location = New System.Drawing.Point(1255, 264)
        Me.txtCajaHasta.Margin = New System.Windows.Forms.Padding(5)
        Me.txtCajaHasta.Name = "txtCajaHasta"
        Me.txtCajaHasta.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.txtCajaHasta.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCajaHasta.Properties.Appearance.Options.UseBackColor = True
        Me.txtCajaHasta.Properties.Appearance.Options.UseFont = True
        Me.txtCajaHasta.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtCajaHasta.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.txtCajaHasta.Properties.IsFloatValue = False
        Me.txtCajaHasta.Properties.MaskSettings.Set("MaskManagerType", GetType(DevExpress.Data.Mask.NumericMaskManager))
        Me.txtCajaHasta.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False")
        Me.txtCajaHasta.Properties.MaskSettings.Set("mask", "d")
        Me.txtCajaHasta.Size = New System.Drawing.Size(128, 58)
        Me.txtCajaHasta.TabIndex = 6
        '
        'txtCajaDesde
        '
        Me.txtCajaDesde.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtCajaDesde.Location = New System.Drawing.Point(1064, 267)
        Me.txtCajaDesde.Margin = New System.Windows.Forms.Padding(5)
        Me.txtCajaDesde.Name = "txtCajaDesde"
        Me.txtCajaDesde.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.txtCajaDesde.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCajaDesde.Properties.Appearance.Options.UseBackColor = True
        Me.txtCajaDesde.Properties.Appearance.Options.UseFont = True
        Me.txtCajaDesde.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtCajaDesde.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.txtCajaDesde.Properties.IsFloatValue = False
        Me.txtCajaDesde.Properties.MaskSettings.Set("MaskManagerType", GetType(DevExpress.Data.Mask.NumericMaskManager))
        Me.txtCajaDesde.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False")
        Me.txtCajaDesde.Properties.MaskSettings.Set("mask", "d")
        Me.txtCajaDesde.Size = New System.Drawing.Size(128, 58)
        Me.txtCajaDesde.TabIndex = 5
        '
        'lblFinca
        '
        Me.lblFinca.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFinca.Appearance.Options.UseFont = True
        Me.lblFinca.Location = New System.Drawing.Point(35, 53)
        Me.lblFinca.LookAndFeel.SkinName = "DevExpress Dark Style"
        Me.lblFinca.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.lblFinca.LookAndFeel.UseDefaultLookAndFeel = False
        Me.lblFinca.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblFinca.Name = "lblFinca"
        Me.lblFinca.Size = New System.Drawing.Size(138, 48)
        Me.lblFinca.TabIndex = 35
        Me.lblFinca.Text = "FINCA:"
        '
        'gbErrores
        '
        Me.gbErrores.Controls.Add(Me.lblPrg)
        Me.gbErrores.Controls.Add(Me.prg)
        Me.gbErrores.Controls.Add(Me.lblTLog)
        Me.gbErrores.Location = New System.Drawing.Point(911, 380)
        Me.gbErrores.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.gbErrores.Name = "gbErrores"
        Me.gbErrores.Padding = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.gbErrores.Size = New System.Drawing.Size(472, 309)
        Me.gbErrores.TabIndex = 96
        Me.gbErrores.TabStop = False
        '
        'lblPrg
        '
        Me.lblPrg.BackColor = System.Drawing.Color.OldLace
        Me.lblPrg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPrg.Location = New System.Drawing.Point(4, 73)
        Me.lblPrg.Margin = New System.Windows.Forms.Padding(4)
        Me.lblPrg.Name = "lblPrg"
        Me.lblPrg.Size = New System.Drawing.Size(464, 234)
        Me.lblPrg.TabIndex = 5
        Me.lblPrg.Text = ""
        '
        'prg
        '
        Me.prg.Dock = System.Windows.Forms.DockStyle.Top
        Me.prg.Location = New System.Drawing.Point(4, 45)
        Me.prg.Margin = New System.Windows.Forms.Padding(4)
        Me.prg.Name = "prg"
        Me.prg.Size = New System.Drawing.Size(464, 28)
        Me.prg.TabIndex = 4
        Me.prg.Visible = False
        '
        'lblTLog
        '
        Me.lblTLog.Appearance.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTLog.Appearance.Options.UseFont = True
        Me.lblTLog.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblTLog.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.lblTLog.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblTLog.Location = New System.Drawing.Point(4, 18)
        Me.lblTLog.Margin = New System.Windows.Forms.Padding(4)
        Me.lblTLog.Name = "lblTLog"
        Me.lblTLog.Size = New System.Drawing.Size(464, 27)
        Me.lblTLog.TabIndex = 3
        Me.lblTLog.Text = "Log"
        '
        'txtPalletHasta
        '
        Me.txtPalletHasta.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtPalletHasta.Location = New System.Drawing.Point(626, 270)
        Me.txtPalletHasta.Margin = New System.Windows.Forms.Padding(5)
        Me.txtPalletHasta.Name = "txtPalletHasta"
        Me.txtPalletHasta.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.txtPalletHasta.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPalletHasta.Properties.Appearance.Options.UseBackColor = True
        Me.txtPalletHasta.Properties.Appearance.Options.UseFont = True
        Me.txtPalletHasta.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtPalletHasta.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.txtPalletHasta.Properties.IsFloatValue = False
        Me.txtPalletHasta.Properties.MaskSettings.Set("MaskManagerType", GetType(DevExpress.Data.Mask.NumericMaskManager))
        Me.txtPalletHasta.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False")
        Me.txtPalletHasta.Properties.MaskSettings.Set("mask", "d")
        Me.txtPalletHasta.Size = New System.Drawing.Size(189, 58)
        Me.txtPalletHasta.TabIndex = 4
        '
        'PictureEdit1
        '
        Me.PictureEdit1.EditValue = CType(resources.GetObject("PictureEdit1.EditValue"), Object)
        Me.PictureEdit1.Location = New System.Drawing.Point(1156, 53)
        Me.PictureEdit1.MenuManager = Me.RibbonControl
        Me.PictureEdit1.Name = "PictureEdit1"
        Me.PictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.[Auto]
        Me.PictureEdit1.Size = New System.Drawing.Size(223, 155)
        Me.PictureEdit1.TabIndex = 49
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Location = New System.Drawing.Point(1209, 273)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(28, 48)
        Me.LabelControl2.TabIndex = 46
        Me.LabelControl2.Text = "A"
        '
        'txtLote
        '
        Me.txtLote.EditValue = ""
        Me.txtLote.Location = New System.Drawing.Point(333, 182)
        Me.txtLote.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtLote.MenuManager = Me.RibbonControl
        Me.txtLote.Name = "txtLote"
        Me.txtLote.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLote.Properties.Appearance.Options.UseFont = True
        Me.txtLote.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.txtLote.Properties.MaskSettings.Set("MaskManagerType", GetType(DevExpress.Data.Mask.NumericMaskManager))
        Me.txtLote.Properties.MaskSettings.Set("mask", "d")
        Me.txtLote.Size = New System.Drawing.Size(482, 56)
        Me.txtLote.TabIndex = 2
        '
        'lblCaja
        '
        Me.lblCaja.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCaja.Appearance.Options.UseFont = True
        Me.lblCaja.Location = New System.Drawing.Point(915, 274)
        Me.lblCaja.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblCaja.Name = "lblCaja"
        Me.lblCaja.Size = New System.Drawing.Size(141, 48)
        Me.lblCaja.TabIndex = 41
        Me.lblCaja.Text = "CAJA #"
        '
        'txtPalletDesde
        '
        Me.txtPalletDesde.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtPalletDesde.Location = New System.Drawing.Point(333, 270)
        Me.txtPalletDesde.Margin = New System.Windows.Forms.Padding(5)
        Me.txtPalletDesde.Name = "txtPalletDesde"
        Me.txtPalletDesde.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.txtPalletDesde.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPalletDesde.Properties.Appearance.Options.UseBackColor = True
        Me.txtPalletDesde.Properties.Appearance.Options.UseFont = True
        Me.txtPalletDesde.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtPalletDesde.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.txtPalletDesde.Properties.IsFloatValue = False
        Me.txtPalletDesde.Properties.MaskSettings.Set("MaskManagerType", GetType(DevExpress.Data.Mask.NumericMaskManager))
        Me.txtPalletDesde.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False")
        Me.txtPalletDesde.Properties.MaskSettings.Set("mask", "d")
        Me.txtPalletDesde.Size = New System.Drawing.Size(198, 58)
        Me.txtPalletDesde.TabIndex = 3
        '
        'lblLote
        '
        Me.lblLote.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLote.Appearance.Options.UseFont = True
        Me.lblLote.Location = New System.Drawing.Point(35, 171)
        Me.lblLote.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblLote.Name = "lblLote"
        Me.lblLote.Size = New System.Drawing.Size(122, 48)
        Me.lblLote.TabIndex = 33
        Me.lblLote.Text = "LOTE:"
        '
        'cmbFinca
        '
        Me.cmbFinca.Location = New System.Drawing.Point(333, 47)
        Me.cmbFinca.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbFinca.MenuManager = Me.RibbonControl
        Me.cmbFinca.Name = "cmbFinca"
        Me.cmbFinca.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbFinca.Properties.Appearance.Options.UseFont = True
        Me.cmbFinca.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbFinca.Properties.NullText = ""
        Me.cmbFinca.Properties.PopupFormMinSize = New System.Drawing.Size(600, 200)
        Me.cmbFinca.Properties.PopupWidth = 100
        Me.cmbFinca.Size = New System.Drawing.Size(482, 54)
        Me.cmbFinca.TabIndex = 1
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Location = New System.Drawing.Point(35, 484)
        Me.LabelControl3.LookAndFeel.SkinName = "DevExpress Dark Style"
        Me.LabelControl3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.LabelControl3.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(292, 48)
        Me.LabelControl3.TabIndex = 95
        Me.LabelControl3.Text = "IMPRESORAS:"
        '
        'lblNombreFinca
        '
        Me.lblNombreFinca.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNombreFinca.Appearance.Options.UseFont = True
        Me.lblNombreFinca.Location = New System.Drawing.Point(333, 113)
        Me.lblNombreFinca.LookAndFeel.SkinName = "DevExpress Dark Style"
        Me.lblNombreFinca.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.lblNombreFinca.LookAndFeel.UseDefaultLookAndFeel = False
        Me.lblNombreFinca.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblNombreFinca.Name = "lblNombreFinca"
        Me.lblNombreFinca.Size = New System.Drawing.Size(241, 48)
        Me.lblNombreFinca.TabIndex = 36
        Me.lblNombreFinca.Text = "nombre finca"
        '
        'cmbImpresoras
        '
        Me.cmbImpresoras.Location = New System.Drawing.Point(333, 478)
        Me.cmbImpresoras.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbImpresoras.MenuManager = Me.RibbonControl
        Me.cmbImpresoras.Name = "cmbImpresoras"
        Me.cmbImpresoras.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbImpresoras.Properties.Appearance.Options.UseFont = True
        Me.cmbImpresoras.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbImpresoras.Properties.NullText = ""
        Me.cmbImpresoras.Properties.PopupFormMinSize = New System.Drawing.Size(600, 200)
        Me.cmbImpresoras.Properties.PopupWidth = 100
        Me.cmbImpresoras.Size = New System.Drawing.Size(482, 54)
        Me.cmbImpresoras.TabIndex = 8
        '
        'lblPallet
        '
        Me.lblPallet.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPallet.Appearance.Options.UseFont = True
        Me.lblPallet.Location = New System.Drawing.Point(35, 275)
        Me.lblPallet.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblPallet.Name = "lblPallet"
        Me.lblPallet.Size = New System.Drawing.Size(168, 48)
        Me.lblPallet.TabIndex = 38
        Me.lblPallet.Text = "PALLET:"
        '
        'dtPackDate
        '
        Me.dtPackDate.EditValue = New Date(2017, 11, 20, 10, 7, 9, 549)
        Me.dtPackDate.Location = New System.Drawing.Point(333, 377)
        Me.dtPackDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtPackDate.Name = "dtPackDate"
        Me.dtPackDate.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 25.0!)
        Me.dtPackDate.Properties.Appearance.Options.UseFont = True
        Me.dtPackDate.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.dtPackDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtPackDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtPackDate.Size = New System.Drawing.Size(482, 58)
        Me.dtPackDate.TabIndex = 7
        '
        'btnPrintRollo
        '
        Me.btnPrintRollo.Appearance.Font = New System.Drawing.Font("Trebuchet MS", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrintRollo.Appearance.Options.UseFont = True
        Me.btnPrintRollo.ImageOptions.SvgImage = CType(resources.GetObject("btnPrintRollo.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnPrintRollo.Location = New System.Drawing.Point(35, 588)
        Me.btnPrintRollo.LookAndFeel.SkinName = "Metropolis"
        Me.btnPrintRollo.LookAndFeel.UseDefaultLookAndFeel = False
        Me.btnPrintRollo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnPrintRollo.Name = "btnPrintRollo"
        Me.btnPrintRollo.Size = New System.Drawing.Size(360, 101)
        Me.btnPrintRollo.TabIndex = 9
        Me.btnPrintRollo.Text = "IMPRIMIR ROLLO"
        '
        'lblPackDate
        '
        Me.lblPackDate.Appearance.Font = New System.Drawing.Font("Tahoma", 25.0!)
        Me.lblPackDate.Appearance.Options.UseFont = True
        Me.lblPackDate.Location = New System.Drawing.Point(35, 380)
        Me.lblPackDate.Margin = New System.Windows.Forms.Padding(4)
        Me.lblPackDate.Name = "lblPackDate"
        Me.lblPackDate.Size = New System.Drawing.Size(228, 51)
        Me.lblPackDate.TabIndex = 43
        Me.lblPackDate.Text = "PACK DATE:"
        '
        'btPrintIndividual
        '
        Me.btPrintIndividual.Appearance.Font = New System.Drawing.Font("Trebuchet MS", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btPrintIndividual.Appearance.Options.UseFont = True
        Me.btPrintIndividual.ImageOptions.SvgImage = CType(resources.GetObject("btPrintIndividual.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btPrintIndividual.Location = New System.Drawing.Point(455, 587)
        Me.btPrintIndividual.LookAndFeel.SkinName = "Metropolis"
        Me.btPrintIndividual.LookAndFeel.UseDefaultLookAndFeel = False
        Me.btPrintIndividual.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btPrintIndividual.Name = "btPrintIndividual"
        Me.btPrintIndividual.Size = New System.Drawing.Size(360, 100)
        Me.btPrintIndividual.TabIndex = 10
        Me.btPrintIndividual.Text = "IMPRIMIR INDIVIDUAL"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Location = New System.Drawing.Point(565, 280)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(28, 48)
        Me.LabelControl1.TabIndex = 45
        Me.LabelControl1.Text = "A"
        '
        'frmPrint_Label
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1408, 946)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frmPrint_Label"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "frmPrint_Label"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.txtCajaHasta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCajaDesde.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbErrores.ResumeLayout(False)
        CType(Me.txtPalletHasta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPalletDesde.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbFinca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbImpresoras.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtPackDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtPackDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtCajaHasta As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents txtCajaDesde As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents lblFinca As DevExpress.XtraEditors.LabelControl
    Friend WithEvents gbErrores As GroupBox
    Friend WithEvents lblPrg As RichTextBox
    Friend WithEvents prg As ProgressBar
    Friend WithEvents lblTLog As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtPalletHasta As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents PictureEdit1 As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtLote As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblCaja As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtPalletDesde As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents lblLote As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbFinca As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblNombreFinca As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbImpresoras As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblPallet As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dtPackDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents btnPrintRollo As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblPackDate As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btPrintIndividual As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl

End Class
