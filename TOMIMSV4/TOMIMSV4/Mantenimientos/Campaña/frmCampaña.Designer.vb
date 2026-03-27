<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCampaña
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
        Dim Label1 As System.Windows.Forms.Label
        Dim lblNombreCampaña As System.Windows.Forms.Label
        Dim Label37 As System.Windows.Forms.Label
        Dim lblIdCampañaCorrelativo As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim lblCodigo As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCampaña))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.cmdGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.txtCodigo = New DevExpress.XtraEditors.TextEdit()
        Me.dtpFechaHasta = New System.Windows.Forms.DateTimePicker()
        Me.dtpFechaDesde = New System.Windows.Forms.DateTimePicker()
        Me.txtNombreCampaña = New DevExpress.XtraEditors.TextEdit()
        Me.chkActivo = New DevExpress.XtraEditors.CheckEdit()
        Me.lblIdCampaña = New System.Windows.Forms.Label()
        Me.XtraTabControl1 = New DevExpress.XtraTab.XtraTabControl()
        Me.xtpGeneral = New DevExpress.XtraTab.XtraTabPage()
        Me.xtpDetalle = New DevExpress.XtraTab.XtraTabPage()
        Me.dgridTallaColor = New DevExpress.XtraGrid.GridControl()
        Me.GridView7 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Label1 = New System.Windows.Forms.Label()
        lblNombreCampaña = New System.Windows.Forms.Label()
        Label37 = New System.Windows.Forms.Label()
        lblIdCampañaCorrelativo = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        lblCodigo = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.txtCodigo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreCampaña.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabControl1.SuspendLayout()
        Me.xtpGeneral.SuspendLayout()
        Me.xtpDetalle.SuspendLayout()
        CType(Me.dgridTallaColor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.ForeColor = System.Drawing.Color.Red
        Label1.Location = New System.Drawing.Point(411, 82)
        Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(251, 16)
        Label1.TabIndex = 16
        Label1.Text = "*Ciertos caracteres podrian ser removidos"
        '
        'lblNombreCampaña
        '
        lblNombreCampaña.AutoSize = True
        lblNombreCampaña.Location = New System.Drawing.Point(29, 83)
        lblNombreCampaña.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblNombreCampaña.Name = "lblNombreCampaña"
        lblNombreCampaña.Size = New System.Drawing.Size(57, 16)
        lblNombreCampaña.TabIndex = 9
        lblNombreCampaña.Text = "Nombre:"
        '
        'Label37
        '
        Label37.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label37.AutoSize = True
        Label37.Location = New System.Drawing.Point(29, 231)
        Label37.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label37.Name = "Label37"
        Label37.Size = New System.Drawing.Size(46, 16)
        Label37.TabIndex = 6
        Label37.Text = "Activa:"
        '
        'lblIdCampañaCorrelativo
        '
        lblIdCampañaCorrelativo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblIdCampañaCorrelativo.AutoSize = True
        lblIdCampañaCorrelativo.Location = New System.Drawing.Point(29, 44)
        lblIdCampañaCorrelativo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblIdCampañaCorrelativo.Name = "lblIdCampañaCorrelativo"
        lblIdCampañaCorrelativo.Size = New System.Drawing.Size(74, 16)
        lblIdCampañaCorrelativo.TabIndex = 0
        lblIdCampañaCorrelativo.Text = "Correlativo:"
        '
        'Label2
        '
        Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(29, 120)
        Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(104, 16)
        Label2.TabIndex = 19
        Label2.Text = "Campaña desde:"
        '
        'Label3
        '
        Label3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label3.AutoSize = True
        Label3.Location = New System.Drawing.Point(29, 158)
        Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(101, 16)
        Label3.TabIndex = 20
        Label3.Text = "Campaña hasta:"
        '
        'lblCodigo
        '
        lblCodigo.AutoSize = True
        lblCodigo.Location = New System.Drawing.Point(29, 197)
        lblCodigo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblCodigo.Name = "lblCodigo"
        lblCodigo.Size = New System.Drawing.Size(57, 16)
        lblCodigo.TabIndex = 22
        lblCodigo.Text = "Nombre:"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.cmdGuardar, Me.cmdActualizar, Me.cmdEliminar})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 4
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.Size = New System.Drawing.Size(1113, 193)
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
        'cmdEliminar
        '
        Me.cmdEliminar.Caption = "Eliminar"
        Me.cmdEliminar.Id = 3
        Me.cmdEliminar.ImageOptions.SvgImage = CType(resources.GetObject("cmdEliminar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdEliminar.Name = "cmdEliminar"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Campaña"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdGuardar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdEliminar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 651)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1113, 30)
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(lblCodigo)
        Me.GroupControl1.Controls.Add(Me.txtCodigo)
        Me.GroupControl1.Controls.Add(Label3)
        Me.GroupControl1.Controls.Add(Label2)
        Me.GroupControl1.Controls.Add(Me.dtpFechaHasta)
        Me.GroupControl1.Controls.Add(Me.dtpFechaDesde)
        Me.GroupControl1.Controls.Add(Label1)
        Me.GroupControl1.Controls.Add(lblNombreCampaña)
        Me.GroupControl1.Controls.Add(Me.txtNombreCampaña)
        Me.GroupControl1.Controls.Add(Label37)
        Me.GroupControl1.Controls.Add(Me.chkActivo)
        Me.GroupControl1.Controls.Add(Me.lblIdCampaña)
        Me.GroupControl1.Controls.Add(lblIdCampañaCorrelativo)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1111, 428)
        Me.GroupControl1.TabIndex = 2
        Me.GroupControl1.Text = "Datos Campaña"
        '
        'txtCodigo
        '
        Me.txtCodigo.Location = New System.Drawing.Point(162, 194)
        Me.txtCodigo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCodigo.MenuManager = Me.RibbonControl
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.Size = New System.Drawing.Size(241, 22)
        Me.txtCodigo.TabIndex = 21
        '
        'dtpFechaHasta
        '
        Me.dtpFechaHasta.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaHasta.Location = New System.Drawing.Point(162, 151)
        Me.dtpFechaHasta.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtpFechaHasta.Name = "dtpFechaHasta"
        Me.dtpFechaHasta.Size = New System.Drawing.Size(241, 23)
        Me.dtpFechaHasta.TabIndex = 18
        '
        'dtpFechaDesde
        '
        Me.dtpFechaDesde.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaDesde.Location = New System.Drawing.Point(162, 113)
        Me.dtpFechaDesde.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtpFechaDesde.Name = "dtpFechaDesde"
        Me.dtpFechaDesde.Size = New System.Drawing.Size(241, 23)
        Me.dtpFechaDesde.TabIndex = 17
        '
        'txtNombreCampaña
        '
        Me.txtNombreCampaña.Location = New System.Drawing.Point(162, 79)
        Me.txtNombreCampaña.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNombreCampaña.MenuManager = Me.RibbonControl
        Me.txtNombreCampaña.Name = "txtNombreCampaña"
        Me.txtNombreCampaña.Size = New System.Drawing.Size(241, 22)
        Me.txtNombreCampaña.TabIndex = 10
        '
        'chkActivo
        '
        Me.chkActivo.EditValue = True
        Me.chkActivo.Location = New System.Drawing.Point(162, 227)
        Me.chkActivo.Margin = New System.Windows.Forms.Padding(4)
        Me.chkActivo.MenuManager = Me.RibbonControl
        Me.chkActivo.Name = "chkActivo"
        Me.chkActivo.Properties.Caption = ""
        Me.chkActivo.Size = New System.Drawing.Size(31, 24)
        Me.chkActivo.TabIndex = 7
        '
        'lblIdCampaña
        '
        Me.lblIdCampaña.AutoSize = True
        Me.lblIdCampaña.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIdCampaña.Location = New System.Drawing.Point(159, 44)
        Me.lblIdCampaña.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblIdCampaña.Name = "lblIdCampaña"
        Me.lblIdCampaña.Size = New System.Drawing.Size(14, 17)
        Me.lblIdCampaña.TabIndex = 1
        Me.lblIdCampaña.Text = "-"
        '
        'XtraTabControl1
        '
        Me.XtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XtraTabControl1.Location = New System.Drawing.Point(0, 193)
        Me.XtraTabControl1.Name = "XtraTabControl1"
        Me.XtraTabControl1.SelectedTabPage = Me.xtpGeneral
        Me.XtraTabControl1.Size = New System.Drawing.Size(1113, 458)
        Me.XtraTabControl1.TabIndex = 5
        Me.XtraTabControl1.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtpGeneral, Me.xtpDetalle})
        '
        'xtpGeneral
        '
        Me.xtpGeneral.Controls.Add(Me.GroupControl1)
        Me.xtpGeneral.Name = "xtpGeneral"
        Me.xtpGeneral.Size = New System.Drawing.Size(1111, 428)
        Me.xtpGeneral.Text = "General"
        '
        'xtpDetalle
        '
        Me.xtpDetalle.Controls.Add(Me.dgridTallaColor)
        Me.xtpDetalle.Name = "xtpDetalle"
        Me.xtpDetalle.Size = New System.Drawing.Size(1111, 428)
        Me.xtpDetalle.Text = "Detalle"
        '
        'dgridTallaColor
        '
        Me.dgridTallaColor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridTallaColor.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.dgridTallaColor.Location = New System.Drawing.Point(0, 0)
        Me.dgridTallaColor.MainView = Me.GridView7
        Me.dgridTallaColor.Margin = New System.Windows.Forms.Padding(4)
        Me.dgridTallaColor.Name = "dgridTallaColor"
        Me.dgridTallaColor.Size = New System.Drawing.Size(1111, 428)
        Me.dgridTallaColor.TabIndex = 21
        Me.dgridTallaColor.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView7})
        '
        'GridView7
        '
        Me.GridView7.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView7.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView7.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView7.Appearance.Row.Options.UseFont = True
        Me.GridView7.DetailHeight = 437
        Me.GridView7.GridControl = Me.dgridTallaColor
        Me.GridView7.Name = "GridView7"
        Me.GridView7.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[True]
        Me.GridView7.OptionsBehavior.Editable = False
        Me.GridView7.OptionsView.ShowAutoFilterRow = True
        Me.GridView7.OptionsView.ShowGroupPanel = False
        '
        'frmCampaña
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1113, 681)
        Me.Controls.Add(Me.XtraTabControl1)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frmCampaña"
        Me.Ribbon = Me.RibbonControl
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Campaña"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.txtCodigo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreCampaña.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabControl1.ResumeLayout(False)
        Me.xtpGeneral.ResumeLayout(False)
        Me.xtpDetalle.ResumeLayout(False)
        CType(Me.dgridTallaColor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtNombreCampaña As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkActivo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents lblIdCampaña As Label
    Friend WithEvents dtpFechaHasta As DateTimePicker
    Friend WithEvents dtpFechaDesde As DateTimePicker
    Friend WithEvents cmdGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdEliminar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents txtCodigo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents XtraTabControl1 As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtpGeneral As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents xtpDetalle As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents dgridTallaColor As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView7 As DevExpress.XtraGrid.Views.Grid.GridView
End Class
