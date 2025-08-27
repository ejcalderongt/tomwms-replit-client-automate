<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmImprimir_Etiqueta
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmImprimir_Etiqueta))
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.lblVersion = New DevExpress.XtraBars.BarStaticItem()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.RibbonPage2 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.txtCajaHasta = New DevExpress.XtraEditors.SpinEdit()
        Me.txtCajaDesde = New DevExpress.XtraEditors.SpinEdit()
        Me.txtPalletHasta = New DevExpress.XtraEditors.SpinEdit()
        Me.txtPalletDesde = New DevExpress.XtraEditors.SpinEdit()
        Me.gbErrores = New System.Windows.Forms.GroupBox()
        Me.lblPrg = New System.Windows.Forms.RichTextBox()
        Me.prg = New System.Windows.Forms.ProgressBar()
        Me.lblTLog = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbImpresoras = New DevExpress.XtraEditors.LookUpEdit()
        Me.PictureEdit1 = New DevExpress.XtraEditors.PictureEdit()
        Me.btnPrintRollo = New DevExpress.XtraEditors.SimpleButton()
        Me.btPrintIndividual = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.lblPackDate = New DevExpress.XtraEditors.LabelControl()
        Me.dtPackDate = New DevExpress.XtraEditors.DateEdit()
        Me.lblCaja = New DevExpress.XtraEditors.LabelControl()
        Me.lblPallet = New DevExpress.XtraEditors.LabelControl()
        Me.lblNombreFinca = New DevExpress.XtraEditors.LabelControl()
        Me.lblFinca = New DevExpress.XtraEditors.LabelControl()
        Me.cmbFinca = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblLote = New DevExpress.XtraEditors.LabelControl()
        Me.txtLote = New DevExpress.XtraEditors.TextEdit()
        Me.BehaviorManager1 = New DevExpress.Utils.Behaviors.BehaviorManager(Me.components)
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.txtLoteTxt = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.lblProducto = New DevExpress.XtraEditors.LabelControl()
        Me.PictureEdit2 = New DevExpress.XtraEditors.PictureEdit()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCajaHasta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCajaDesde.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPalletHasta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPalletDesde.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbErrores.SuspendLayout()
        CType(Me.cmbImpresoras.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtPackDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtPackDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbFinca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BehaviorManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.txtLoteTxt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureEdit2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblVersion)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 932)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1418, 30)
        '
        'lblVersion
        '
        Me.lblVersion.Caption = "Versión 20250618"
        Me.lblVersion.Id = 1
        Me.lblVersion.ImageOptions.SvgImage = CType(resources.GetObject("lblVersion.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.lblVersion.Name = "lblVersion"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.lblVersion})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.RibbonControl.MaxItemId = 2
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage2})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.ShowItemCaptionsInPageHeader = True
        Me.RibbonControl.ShowToolbarCustomizeItem = False
        Me.RibbonControl.Size = New System.Drawing.Size(1418, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        Me.RibbonControl.Toolbar.ShowCustomizeItem = False
        '
        'RibbonPage2
        '
        Me.RibbonPage2.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup2})
        Me.RibbonPage2.Name = "RibbonPage2"
        Me.RibbonPage2.Text = "Impresion"
        '
        'RibbonPageGroup2
        '
        Me.RibbonPageGroup2.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.[True]
        Me.RibbonPageGroup2.Name = "RibbonPageGroup2"
        '
        'txtCajaHasta
        '
        Me.txtCajaHasta.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtCajaHasta.Location = New System.Drawing.Point(1252, 192)
        Me.txtCajaHasta.Margin = New System.Windows.Forms.Padding(5)
        Me.txtCajaHasta.Name = "txtCajaHasta"
        Me.txtCajaHasta.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.txtCajaHasta.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCajaHasta.Properties.Appearance.Options.UseBackColor = True
        Me.txtCajaHasta.Properties.Appearance.Options.UseFont = True
        Me.txtCajaHasta.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtCajaHasta.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.txtCajaHasta.Properties.IsFloatValue = False
        Me.txtCajaHasta.Properties.MaskSettings.Set("MaskManagerType", GetType(DevExpress.Data.Mask.NumericMaskManager))
        Me.txtCajaHasta.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False")
        Me.txtCajaHasta.Properties.MaskSettings.Set("mask", "d")
        Me.txtCajaHasta.Size = New System.Drawing.Size(128, 38)
        Me.txtCajaHasta.TabIndex = 6
        '
        'txtCajaDesde
        '
        Me.txtCajaDesde.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtCajaDesde.Location = New System.Drawing.Point(1060, 192)
        Me.txtCajaDesde.Margin = New System.Windows.Forms.Padding(5)
        Me.txtCajaDesde.Name = "txtCajaDesde"
        Me.txtCajaDesde.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.txtCajaDesde.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCajaDesde.Properties.Appearance.Options.UseBackColor = True
        Me.txtCajaDesde.Properties.Appearance.Options.UseFont = True
        Me.txtCajaDesde.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtCajaDesde.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.txtCajaDesde.Properties.IsFloatValue = False
        Me.txtCajaDesde.Properties.MaskSettings.Set("MaskManagerType", GetType(DevExpress.Data.Mask.NumericMaskManager))
        Me.txtCajaDesde.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False")
        Me.txtCajaDesde.Properties.MaskSettings.Set("mask", "d")
        Me.txtCajaDesde.Size = New System.Drawing.Size(128, 38)
        Me.txtCajaDesde.TabIndex = 5
        '
        'txtPalletHasta
        '
        Me.txtPalletHasta.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtPalletHasta.Location = New System.Drawing.Point(624, 189)
        Me.txtPalletHasta.Margin = New System.Windows.Forms.Padding(5)
        Me.txtPalletHasta.Name = "txtPalletHasta"
        Me.txtPalletHasta.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.txtPalletHasta.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPalletHasta.Properties.Appearance.Options.UseBackColor = True
        Me.txtPalletHasta.Properties.Appearance.Options.UseFont = True
        Me.txtPalletHasta.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtPalletHasta.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.txtPalletHasta.Properties.IsFloatValue = False
        Me.txtPalletHasta.Properties.MaskSettings.Set("MaskManagerType", GetType(DevExpress.Data.Mask.NumericMaskManager))
        Me.txtPalletHasta.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False")
        Me.txtPalletHasta.Properties.MaskSettings.Set("mask", "d")
        Me.txtPalletHasta.Size = New System.Drawing.Size(189, 40)
        Me.txtPalletHasta.TabIndex = 4
        '
        'txtPalletDesde
        '
        Me.txtPalletDesde.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtPalletDesde.Location = New System.Drawing.Point(330, 189)
        Me.txtPalletDesde.Margin = New System.Windows.Forms.Padding(5)
        Me.txtPalletDesde.Name = "txtPalletDesde"
        Me.txtPalletDesde.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.txtPalletDesde.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPalletDesde.Properties.Appearance.Options.UseBackColor = True
        Me.txtPalletDesde.Properties.Appearance.Options.UseFont = True
        Me.txtPalletDesde.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtPalletDesde.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.txtPalletDesde.Properties.IsFloatValue = False
        Me.txtPalletDesde.Properties.MaskSettings.Set("MaskManagerType", GetType(DevExpress.Data.Mask.NumericMaskManager))
        Me.txtPalletDesde.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False")
        Me.txtPalletDesde.Properties.MaskSettings.Set("mask", "d")
        Me.txtPalletDesde.Size = New System.Drawing.Size(198, 38)
        Me.txtPalletDesde.TabIndex = 3
        '
        'gbErrores
        '
        Me.gbErrores.Controls.Add(Me.lblPrg)
        Me.gbErrores.Controls.Add(Me.prg)
        Me.gbErrores.Controls.Add(Me.lblTLog)
        Me.gbErrores.Location = New System.Drawing.Point(842, 255)
        Me.gbErrores.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.gbErrores.Name = "gbErrores"
        Me.gbErrores.Padding = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.gbErrores.Size = New System.Drawing.Size(541, 330)
        Me.gbErrores.TabIndex = 96
        Me.gbErrores.TabStop = False
        '
        'lblPrg
        '
        Me.lblPrg.BackColor = System.Drawing.Color.OldLace
        Me.lblPrg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPrg.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrg.Location = New System.Drawing.Point(4, 73)
        Me.lblPrg.Margin = New System.Windows.Forms.Padding(4)
        Me.lblPrg.Name = "lblPrg"
        Me.lblPrg.Size = New System.Drawing.Size(533, 255)
        Me.lblPrg.TabIndex = 5
        Me.lblPrg.Text = ""
        '
        'prg
        '
        Me.prg.Dock = System.Windows.Forms.DockStyle.Top
        Me.prg.Location = New System.Drawing.Point(4, 45)
        Me.prg.Margin = New System.Windows.Forms.Padding(4)
        Me.prg.Name = "prg"
        Me.prg.Size = New System.Drawing.Size(533, 28)
        Me.prg.TabIndex = 4
        Me.prg.Visible = False
        '
        'lblTLog
        '
        Me.lblTLog.Appearance.Font = New System.Drawing.Font("Consolas", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTLog.Appearance.Options.UseFont = True
        Me.lblTLog.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblTLog.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.lblTLog.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblTLog.Location = New System.Drawing.Point(4, 18)
        Me.lblTLog.Margin = New System.Windows.Forms.Padding(4)
        Me.lblTLog.Name = "lblTLog"
        Me.lblTLog.Size = New System.Drawing.Size(533, 27)
        Me.lblTLog.TabIndex = 3
        Me.lblTLog.Text = "Log"
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Location = New System.Drawing.Point(33, 290)
        Me.LabelControl3.LookAndFeel.SkinName = "DevExpress Dark Style"
        Me.LabelControl3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.LabelControl3.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl3.Margin = New System.Windows.Forms.Padding(4)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(189, 31)
        Me.LabelControl3.TabIndex = 95
        Me.LabelControl3.Text = "IMPRESORAS:"
        '
        'cmbImpresoras
        '
        Me.cmbImpresoras.Location = New System.Drawing.Point(330, 284)
        Me.cmbImpresoras.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbImpresoras.MenuManager = Me.RibbonControl
        Me.cmbImpresoras.Name = "cmbImpresoras"
        Me.cmbImpresoras.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbImpresoras.Properties.Appearance.Options.UseFont = True
        Me.cmbImpresoras.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbImpresoras.Properties.NullText = ""
        Me.cmbImpresoras.Properties.PopupFormMinSize = New System.Drawing.Size(600, 199)
        Me.cmbImpresoras.Properties.PopupWidth = 100
        Me.cmbImpresoras.Size = New System.Drawing.Size(482, 38)
        Me.cmbImpresoras.TabIndex = 8
        '
        'PictureEdit1
        '
        Me.PictureEdit1.EditValue = CType(resources.GetObject("PictureEdit1.EditValue"), Object)
        Me.PictureEdit1.Location = New System.Drawing.Point(1186, 41)
        Me.PictureEdit1.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.PictureEdit1.MenuManager = Me.RibbonControl
        Me.PictureEdit1.Name = "PictureEdit1"
        Me.PictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.[Auto]
        Me.PictureEdit1.Size = New System.Drawing.Size(194, 130)
        Me.PictureEdit1.TabIndex = 49
        '
        'btnPrintRollo
        '
        Me.btnPrintRollo.Appearance.Font = New System.Drawing.Font("Trebuchet MS", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrintRollo.Appearance.Options.UseFont = True
        Me.btnPrintRollo.ImageOptions.SvgImage = CType(resources.GetObject("btnPrintRollo.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnPrintRollo.Location = New System.Drawing.Point(34, 339)
        Me.btnPrintRollo.LookAndFeel.SkinName = "Metropolis"
        Me.btnPrintRollo.LookAndFeel.UseDefaultLookAndFeel = False
        Me.btnPrintRollo.Margin = New System.Windows.Forms.Padding(4)
        Me.btnPrintRollo.Name = "btnPrintRollo"
        Me.btnPrintRollo.Size = New System.Drawing.Size(360, 101)
        Me.btnPrintRollo.TabIndex = 9
        Me.btnPrintRollo.Text = "IMPRIMIR ROLLO"
        '
        'btPrintIndividual
        '
        Me.btPrintIndividual.Appearance.Font = New System.Drawing.Font("Trebuchet MS", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btPrintIndividual.Appearance.Options.UseFont = True
        Me.btPrintIndividual.ImageOptions.SvgImage = CType(resources.GetObject("btPrintIndividual.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btPrintIndividual.Location = New System.Drawing.Point(454, 338)
        Me.btPrintIndividual.LookAndFeel.SkinName = "Metropolis"
        Me.btPrintIndividual.LookAndFeel.UseDefaultLookAndFeel = False
        Me.btPrintIndividual.Margin = New System.Windows.Forms.Padding(4)
        Me.btPrintIndividual.Name = "btPrintIndividual"
        Me.btPrintIndividual.Size = New System.Drawing.Size(360, 100)
        Me.btPrintIndividual.TabIndex = 10
        Me.btPrintIndividual.Text = "IMPRIMIR INDIVIDUAL"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Location = New System.Drawing.Point(1205, 192)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(4)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(18, 31)
        Me.LabelControl2.TabIndex = 46
        Me.LabelControl2.Text = "A"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Location = New System.Drawing.Point(563, 200)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(18, 31)
        Me.LabelControl1.TabIndex = 45
        Me.LabelControl1.Text = "A"
        '
        'lblPackDate
        '
        Me.lblPackDate.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPackDate.Appearance.Options.UseFont = True
        Me.lblPackDate.Location = New System.Drawing.Point(33, 240)
        Me.lblPackDate.Margin = New System.Windows.Forms.Padding(4)
        Me.lblPackDate.Name = "lblPackDate"
        Me.lblPackDate.Size = New System.Drawing.Size(162, 31)
        Me.lblPackDate.TabIndex = 43
        Me.lblPackDate.Text = "PACK DATE:"
        '
        'dtPackDate
        '
        Me.dtPackDate.EditValue = New Date(2017, 11, 20, 10, 7, 9, 549)
        Me.dtPackDate.Location = New System.Drawing.Point(330, 237)
        Me.dtPackDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtPackDate.Name = "dtPackDate"
        Me.dtPackDate.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtPackDate.Properties.Appearance.Options.UseFont = True
        Me.dtPackDate.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.dtPackDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtPackDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtPackDate.Size = New System.Drawing.Size(482, 38)
        Me.dtPackDate.TabIndex = 7
        '
        'lblCaja
        '
        Me.lblCaja.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCaja.Appearance.Options.UseFont = True
        Me.lblCaja.Location = New System.Drawing.Point(911, 192)
        Me.lblCaja.Margin = New System.Windows.Forms.Padding(4)
        Me.lblCaja.Name = "lblCaja"
        Me.lblCaja.Size = New System.Drawing.Size(92, 31)
        Me.lblCaja.TabIndex = 41
        Me.lblCaja.Text = "CAJA #"
        '
        'lblPallet
        '
        Me.lblPallet.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPallet.Appearance.Options.UseFont = True
        Me.lblPallet.Location = New System.Drawing.Point(33, 192)
        Me.lblPallet.Margin = New System.Windows.Forms.Padding(4)
        Me.lblPallet.Name = "lblPallet"
        Me.lblPallet.Size = New System.Drawing.Size(109, 31)
        Me.lblPallet.TabIndex = 38
        Me.lblPallet.Text = "PALLET:"
        '
        'lblNombreFinca
        '
        Me.lblNombreFinca.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNombreFinca.Appearance.Options.UseFont = True
        Me.lblNombreFinca.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblNombreFinca.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.lblNombreFinca.Location = New System.Drawing.Point(332, 93)
        Me.lblNombreFinca.LookAndFeel.SkinName = "DevExpress Dark Style"
        Me.lblNombreFinca.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.lblNombreFinca.LookAndFeel.UseDefaultLookAndFeel = False
        Me.lblNombreFinca.Margin = New System.Windows.Forms.Padding(4)
        Me.lblNombreFinca.Name = "lblNombreFinca"
        Me.lblNombreFinca.Size = New System.Drawing.Size(483, 33)
        Me.lblNombreFinca.TabIndex = 36
        Me.lblNombreFinca.Text = "..."
        '
        'lblFinca
        '
        Me.lblFinca.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFinca.Appearance.Options.UseFont = True
        Me.lblFinca.Location = New System.Drawing.Point(35, 53)
        Me.lblFinca.LookAndFeel.SkinName = "DevExpress Dark Style"
        Me.lblFinca.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.lblFinca.LookAndFeel.UseDefaultLookAndFeel = False
        Me.lblFinca.Margin = New System.Windows.Forms.Padding(4)
        Me.lblFinca.Name = "lblFinca"
        Me.lblFinca.Size = New System.Drawing.Size(91, 31)
        Me.lblFinca.TabIndex = 35
        Me.lblFinca.Text = "FINCA:"
        '
        'cmbFinca
        '
        Me.cmbFinca.Location = New System.Drawing.Point(332, 47)
        Me.cmbFinca.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbFinca.MenuManager = Me.RibbonControl
        Me.cmbFinca.Name = "cmbFinca"
        Me.cmbFinca.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbFinca.Properties.Appearance.Options.UseFont = True
        Me.cmbFinca.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbFinca.Properties.NullText = ""
        Me.cmbFinca.Properties.PopupFormMinSize = New System.Drawing.Size(600, 199)
        Me.cmbFinca.Properties.PopupWidth = 100
        Me.cmbFinca.Size = New System.Drawing.Size(482, 38)
        Me.cmbFinca.TabIndex = 1
        '
        'lblLote
        '
        Me.lblLote.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLote.Appearance.Options.UseFont = True
        Me.lblLote.Location = New System.Drawing.Point(34, 140)
        Me.lblLote.Margin = New System.Windows.Forms.Padding(4)
        Me.lblLote.Name = "lblLote"
        Me.lblLote.Size = New System.Drawing.Size(79, 31)
        Me.lblLote.TabIndex = 33
        Me.lblLote.Text = "LOTE:"
        '
        'txtLote
        '
        Me.txtLote.EditValue = ""
        Me.txtLote.Location = New System.Drawing.Point(331, 140)
        Me.txtLote.Margin = New System.Windows.Forms.Padding(4)
        Me.txtLote.MenuManager = Me.RibbonControl
        Me.txtLote.Name = "txtLote"
        Me.txtLote.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLote.Properties.Appearance.Options.UseFont = True
        Me.txtLote.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.txtLote.Properties.MaskSettings.Set("MaskManagerType", GetType(DevExpress.Data.Mask.NumericMaskManager))
        Me.txtLote.Properties.MaskSettings.Set("mask", "d")
        Me.txtLote.Size = New System.Drawing.Size(482, 40)
        Me.txtLote.TabIndex = 2
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.txtLoteTxt)
        Me.GroupControl1.Controls.Add(Me.LabelControl4)
        Me.GroupControl1.Controls.Add(Me.lblProducto)
        Me.GroupControl1.Controls.Add(Me.PictureEdit2)
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
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1418, 739)
        Me.GroupControl1.TabIndex = 5
        Me.GroupControl1.Text = "Parametros de Impresión"
        '
        'txtLoteTxt
        '
        Me.txtLoteTxt.EditValue = ""
        Me.txtLoteTxt.Location = New System.Drawing.Point(332, 545)
        Me.txtLoteTxt.Margin = New System.Windows.Forms.Padding(4)
        Me.txtLoteTxt.MenuManager = Me.RibbonControl
        Me.txtLoteTxt.Name = "txtLoteTxt"
        Me.txtLoteTxt.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLoteTxt.Properties.Appearance.Options.UseFont = True
        Me.txtLoteTxt.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.txtLoteTxt.Properties.MaskSettings.Set("MaskManagerType", GetType(DevExpress.Data.Mask.NumericMaskManager))
        Me.txtLoteTxt.Properties.MaskSettings.Set("mask", "d")
        Me.txtLoteTxt.Properties.ReadOnly = True
        Me.txtLoteTxt.Size = New System.Drawing.Size(482, 40)
        Me.txtLoteTxt.TabIndex = 34
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl4.Appearance.Options.UseFont = True
        Me.LabelControl4.Location = New System.Drawing.Point(35, 549)
        Me.LabelControl4.Margin = New System.Windows.Forms.Padding(4)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(138, 31)
        Me.LabelControl4.TabIndex = 35
        Me.LabelControl4.Text = "LOTE TXT:"
        '
        'lblProducto
        '
        Me.lblProducto.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProducto.Appearance.Options.UseFont = True
        Me.lblProducto.Appearance.Options.UseTextOptions = True
        Me.lblProducto.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.lblProducto.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.lblProducto.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblProducto.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.lblProducto.Location = New System.Drawing.Point(35, 448)
        Me.lblProducto.LookAndFeel.SkinName = "DevExpress Dark Style"
        Me.lblProducto.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.lblProducto.LookAndFeel.UseDefaultLookAndFeel = False
        Me.lblProducto.Margin = New System.Windows.Forms.Padding(4)
        Me.lblProducto.Name = "lblProducto"
        Me.lblProducto.Size = New System.Drawing.Size(780, 84)
        Me.lblProducto.TabIndex = 98
        Me.lblProducto.Text = "..."
        '
        'PictureEdit2
        '
        Me.PictureEdit2.EditValue = Global.MES.My.Resources.Resources.QuickTag_DTS
        Me.PictureEdit2.Location = New System.Drawing.Point(911, 41)
        Me.PictureEdit2.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.PictureEdit2.MenuManager = Me.RibbonControl
        Me.PictureEdit2.Name = "PictureEdit2"
        Me.PictureEdit2.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.[Auto]
        Me.PictureEdit2.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch
        Me.PictureEdit2.Size = New System.Drawing.Size(197, 130)
        Me.PictureEdit2.TabIndex = 97
        '
        'frmImprimir_Etiqueta
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1418, 962)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.Name = "frmImprimir_Etiqueta"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Impresión de Etiqueta"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCajaHasta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCajaDesde.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPalletHasta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPalletDesde.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbErrores.ResumeLayout(False)
        CType(Me.cmbImpresoras.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtPackDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtPackDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbFinca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BehaviorManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.txtLoteTxt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureEdit2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents lblLote As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtLote As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblFinca As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbFinca As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblCaja As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblPallet As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblNombreFinca As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblPackDate As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dtPackDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnPrintRollo As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btPrintIndividual As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents PictureEdit1 As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbImpresoras As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents BehaviorManager1 As DevExpress.Utils.Behaviors.BehaviorManager
    Friend WithEvents gbErrores As GroupBox
    Friend WithEvents lblPrg As RichTextBox
    Friend WithEvents prg As ProgressBar
    Friend WithEvents lblTLog As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtPalletDesde As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents txtCajaHasta As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents txtCajaDesde As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents txtPalletHasta As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents RibbonPage2 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents PictureEdit2 As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents lblProducto As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblVersion As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents txtLoteTxt As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
End Class
