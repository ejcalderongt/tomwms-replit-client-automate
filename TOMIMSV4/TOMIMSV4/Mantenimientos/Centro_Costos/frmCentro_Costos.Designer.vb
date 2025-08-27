<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCentro_Costos
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
        Dim IdEmpresaLabel As System.Windows.Forms.Label
        Dim NombreLabel As System.Windows.Forms.Label
        Dim Label7 As System.Windows.Forms.Label
        Dim Label12 As System.Windows.Forms.Label
        Dim Label8 As System.Windows.Forms.Label
        Dim Label6 As System.Windows.Forms.Label
        Dim Label9 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCentro_Costos))
        Dim Label2 As System.Windows.Forms.Label
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.chkActivo = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.chkControlInv = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.TabControlEmpresa = New DevExpress.XtraTab.XtraTabControl()
        Me.General = New DevExpress.XtraTab.XtraTabPage()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.lblCodigo = New DevExpress.XtraEditors.TextEdit()
        Me.cmbEmpresa = New DevExpress.XtraEditors.LookUpEdit()
        Me.CodigoTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.NombreTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.txtReferencia = New DevExpress.XtraEditors.TextEdit()
        IdEmpresaLabel = New System.Windows.Forms.Label()
        NombreLabel = New System.Windows.Forms.Label()
        Label7 = New System.Windows.Forms.Label()
        Label12 = New System.Windows.Forms.Label()
        Label8 = New System.Windows.Forms.Label()
        Label6 = New System.Windows.Forms.Label()
        Label9 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TabControlEmpresa, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControlEmpresa.SuspendLayout()
        Me.General.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.lblCodigo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbEmpresa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CodigoTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NombreTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReferencia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'IdEmpresaLabel
        '
        IdEmpresaLabel.AutoSize = True
        IdEmpresaLabel.Location = New System.Drawing.Point(13, 112)
        IdEmpresaLabel.Name = "IdEmpresaLabel"
        IdEmpresaLabel.Size = New System.Drawing.Size(51, 16)
        IdEmpresaLabel.TabIndex = 0
        IdEmpresaLabel.Text = "Código:"
        '
        'NombreLabel
        '
        NombreLabel.AutoSize = True
        NombreLabel.Location = New System.Drawing.Point(12, 146)
        NombreLabel.Name = "NombreLabel"
        NombreLabel.Size = New System.Drawing.Size(57, 16)
        NombreLabel.TabIndex = 4
        NombreLabel.Text = "Nombre:"
        '
        'Label7
        '
        Label7.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label7.AutoSize = True
        Label7.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label7.ForeColor = System.Drawing.Color.Red
        Label7.Location = New System.Drawing.Point(93, 74)
        Label7.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New System.Drawing.Size(21, 21)
        Label7.TabIndex = 38
        Label7.Text = "*"
        '
        'Label12
        '
        Label12.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label12.AutoSize = True
        Label12.Location = New System.Drawing.Point(13, 42)
        Label12.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Label12.Name = "Label12"
        Label12.Size = New System.Drawing.Size(19, 16)
        Label12.TabIndex = 35
        Label12.Text = "ID"
        '
        'Label8
        '
        Label8.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label8.AutoSize = True
        Label8.Location = New System.Drawing.Point(13, 71)
        Label8.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Label8.Name = "Label8"
        Label8.Size = New System.Drawing.Size(62, 16)
        Label8.TabIndex = 37
        Label8.Text = "Empresa:"
        '
        'Label6
        '
        Label6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label6.AutoSize = True
        Label6.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label6.ForeColor = System.Drawing.Color.Red
        Label6.Location = New System.Drawing.Point(93, 112)
        Label6.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(21, 21)
        Label6.TabIndex = 40
        Label6.Text = "*"
        '
        'Label9
        '
        Label9.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label9.AutoSize = True
        Label9.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label9.ForeColor = System.Drawing.Color.Red
        Label9.Location = New System.Drawing.Point(93, 146)
        Label9.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Label9.Name = "Label9"
        Label9.Size = New System.Drawing.Size(21, 21)
        Label9.TabIndex = 41
        Label9.Text = "*"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuGuardar, Me.mnuActualizar, Me.mnuEliminar, Me.chkActivo, Me.chkControlInv})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 7
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1074, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'mnuGuardar
        '
        Me.mnuGuardar.Caption = "Guardar"
        Me.mnuGuardar.Id = 1
        Me.mnuGuardar.ImageOptions.SvgImage = CType(resources.GetObject("mnuGuardar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuGuardar.Name = "mnuGuardar"
        '
        'mnuActualizar
        '
        Me.mnuActualizar.Caption = "Actualizar"
        Me.mnuActualizar.Id = 3
        Me.mnuActualizar.ImageOptions.SvgImage = CType(resources.GetObject("mnuActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuActualizar.Name = "mnuActualizar"
        '
        'mnuEliminar
        '
        Me.mnuEliminar.Caption = "Desactivar"
        Me.mnuEliminar.Id = 4
        Me.mnuEliminar.ImageOptions.SvgImage = CType(resources.GetObject("mnuEliminar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEliminar.Name = "mnuEliminar"
        '
        'chkActivo
        '
        Me.chkActivo.BindableChecked = True
        Me.chkActivo.Caption = "Activo"
        Me.chkActivo.Checked = True
        Me.chkActivo.Id = 5
        Me.chkActivo.Name = "chkActivo"
        '
        'chkControlInv
        '
        Me.chkControlInv.Caption = "Control Inventario"
        Me.chkControlInv.Id = 6
        Me.chkControlInv.Name = "chkControlInv"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1, Me.RibbonPageGroup2})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Centro Costos"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEliminar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonPageGroup2
        '
        Me.RibbonPageGroup2.ItemLinks.Add(Me.chkActivo)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.chkControlInv)
        Me.RibbonPageGroup2.Name = "RibbonPageGroup2"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 522)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1074, 30)
        '
        'TabControlEmpresa
        '
        Me.TabControlEmpresa.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControlEmpresa.Location = New System.Drawing.Point(0, 193)
        Me.TabControlEmpresa.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TabControlEmpresa.Name = "TabControlEmpresa"
        Me.TabControlEmpresa.SelectedTabPage = Me.General
        Me.TabControlEmpresa.Size = New System.Drawing.Size(1074, 329)
        Me.TabControlEmpresa.TabIndex = 2
        Me.TabControlEmpresa.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.General})
        '
        'General
        '
        Me.General.Controls.Add(Me.GroupControl1)
        Me.General.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.General.Name = "General"
        Me.General.Size = New System.Drawing.Size(1072, 299)
        Me.General.Text = "General"
        '
        'GroupControl1
        '
        Me.GroupControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.GroupControl1.Controls.Add(Label2)
        Me.GroupControl1.Controls.Add(Me.txtReferencia)
        Me.GroupControl1.Controls.Add(Label9)
        Me.GroupControl1.Controls.Add(Label6)
        Me.GroupControl1.Controls.Add(Me.lblCodigo)
        Me.GroupControl1.Controls.Add(Me.cmbEmpresa)
        Me.GroupControl1.Controls.Add(Label7)
        Me.GroupControl1.Controls.Add(Label12)
        Me.GroupControl1.Controls.Add(Label8)
        Me.GroupControl1.Controls.Add(Me.CodigoTextEdit)
        Me.GroupControl1.Controls.Add(IdEmpresaLabel)
        Me.GroupControl1.Controls.Add(NombreLabel)
        Me.GroupControl1.Controls.Add(Me.NombreTextEdit)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1072, 299)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Datos Centro de Costo"
        '
        'lblCodigo
        '
        Me.lblCodigo.Location = New System.Drawing.Point(133, 39)
        Me.lblCodigo.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.lblCodigo.MenuManager = Me.RibbonControl
        Me.lblCodigo.Name = "lblCodigo"
        Me.lblCodigo.Properties.Appearance.BackColor = System.Drawing.Color.Lavender
        Me.lblCodigo.Properties.Appearance.Options.UseBackColor = True
        Me.lblCodigo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.lblCodigo.Properties.Mask.EditMask = "[A-Za-z0-9._%-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}"
        Me.lblCodigo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.lblCodigo.Size = New System.Drawing.Size(241, 22)
        Me.lblCodigo.TabIndex = 36
        '
        'cmbEmpresa
        '
        Me.cmbEmpresa.Location = New System.Drawing.Point(133, 73)
        Me.cmbEmpresa.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.cmbEmpresa.MenuManager = Me.RibbonControl
        Me.cmbEmpresa.Name = "cmbEmpresa"
        Me.cmbEmpresa.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbEmpresa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbEmpresa.Properties.NullText = ""
        Me.cmbEmpresa.Size = New System.Drawing.Size(241, 22)
        Me.cmbEmpresa.TabIndex = 39
        '
        'CodigoTextEdit
        '
        Me.CodigoTextEdit.Location = New System.Drawing.Point(133, 109)
        Me.CodigoTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.CodigoTextEdit.MenuManager = Me.RibbonControl
        Me.CodigoTextEdit.Name = "CodigoTextEdit"
        Me.CodigoTextEdit.Properties.LookAndFeel.SkinName = "Office 2010 Blue"
        Me.CodigoTextEdit.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.CodigoTextEdit.Size = New System.Drawing.Size(241, 22)
        Me.CodigoTextEdit.TabIndex = 1
        '
        'NombreTextEdit
        '
        Me.NombreTextEdit.Location = New System.Drawing.Point(133, 143)
        Me.NombreTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.NombreTextEdit.MenuManager = Me.RibbonControl
        Me.NombreTextEdit.Name = "NombreTextEdit"
        Me.NombreTextEdit.Size = New System.Drawing.Size(241, 22)
        Me.NombreTextEdit.TabIndex = 5
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(12, 178)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(73, 16)
        Label2.TabIndex = 42
        Label2.Text = "Referencia:"
        '
        'txtReferencia
        '
        Me.txtReferencia.Location = New System.Drawing.Point(133, 175)
        Me.txtReferencia.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtReferencia.MenuManager = Me.RibbonControl
        Me.txtReferencia.Name = "txtReferencia"
        Me.txtReferencia.Size = New System.Drawing.Size(241, 22)
        Me.txtReferencia.TabIndex = 43
        '
        'frmCentro_Costos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1074, 552)
        Me.Controls.Add(Me.TabControlEmpresa)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frmCentro_Costos"
        Me.Ribbon = Me.RibbonControl
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Centro de Costos"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TabControlEmpresa, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControlEmpresa.ResumeLayout(False)
        Me.General.ResumeLayout(False)
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.lblCodigo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbEmpresa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CodigoTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NombreTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReferencia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents TabControlEmpresa As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents General As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents CodigoTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents NombreTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblCodigo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cmbEmpresa As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents chkActivo As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents chkControlInv As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents RibbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents txtReferencia As DevExpress.XtraEditors.TextEdit
End Class
