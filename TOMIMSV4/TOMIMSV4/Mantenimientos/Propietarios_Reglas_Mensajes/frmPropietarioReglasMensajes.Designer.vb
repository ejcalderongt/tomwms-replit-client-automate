<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPropietarioReglasMensajes
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPropietarioReglasMensajes))
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem3 = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.GroupControl8 = New DevExpress.XtraEditors.GroupControl()
        Me.ToolStripPR = New System.Windows.Forms.ToolStrip()
        Me.cmdGuardarMensaje = New System.Windows.Forms.ToolStripButton()
        Me.lnkDestinatario = New System.Windows.Forms.LinkLabel()
        Me.cmbDestinatario = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbMensaje = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbProceso = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblNombrePropietario = New System.Windows.Forms.Label()
        Me.lnkMensaje = New System.Windows.Forms.LinkLabel()
        Me.lnkRegla = New System.Windows.Forms.LinkLabel()
        Me.lblCodigo = New System.Windows.Forms.Label()
        Me.GroupControl9 = New DevExpress.XtraEditors.GroupControl()
        Me.GridMensajes = New DevExpress.XtraGrid.GridControl()
        Me.gvMensajes = New DevExpress.XtraGrid.Views.Grid.GridView()
        Label12 = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl8.SuspendLayout()
        Me.ToolStripPR.SuspendLayout()
        CType(Me.cmbDestinatario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbMensaje.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbProceso.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl9.SuspendLayout()
        CType(Me.GridMensajes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvMensajes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label12
        '
        Label12.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label12.AutoSize = True
        Label12.Location = New System.Drawing.Point(50, 73)
        Label12.Name = "Label12"
        Label12.Size = New System.Drawing.Size(69, 16)
        Label12.TabIndex = 1
        Label12.Text = "Correlativo"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuGuardar, Me.BarButtonItem2, Me.BarButtonItem3})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 4
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1397, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'mnuGuardar
        '
        Me.mnuGuardar.Caption = "Guardar"
        Me.mnuGuardar.Id = 1
        Me.mnuGuardar.ImageOptions.SvgImage = CType(resources.GetObject("mnuGuardar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuGuardar.Name = "mnuGuardar"
        '
        'BarButtonItem2
        '
        Me.BarButtonItem2.Caption = "Actualizar"
        Me.BarButtonItem2.Id = 2
        Me.BarButtonItem2.ImageOptions.SvgImage = CType(resources.GetObject("BarButtonItem2.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.BarButtonItem2.Name = "BarButtonItem2"
        '
        'BarButtonItem3
        '
        Me.BarButtonItem3.Caption = "Eliminar"
        Me.BarButtonItem3.Id = 3
        Me.BarButtonItem3.ImageOptions.SvgImage = CType(resources.GetObject("BarButtonItem3.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.BarButtonItem3.Name = "BarButtonItem3"
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
        Me.RibbonPageGroup1.ItemLinks.Add(Me.BarButtonItem2)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.BarButtonItem3)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 738)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1397, 30)
        '
        'GroupControl8
        '
        Me.GroupControl8.Controls.Add(Me.ToolStripPR)
        Me.GroupControl8.Controls.Add(Me.lnkDestinatario)
        Me.GroupControl8.Controls.Add(Me.cmbDestinatario)
        Me.GroupControl8.Controls.Add(Me.cmbMensaje)
        Me.GroupControl8.Controls.Add(Me.cmbProceso)
        Me.GroupControl8.Controls.Add(Me.lblNombrePropietario)
        Me.GroupControl8.Controls.Add(Me.lnkMensaje)
        Me.GroupControl8.Controls.Add(Me.lnkRegla)
        Me.GroupControl8.Controls.Add(Me.lblCodigo)
        Me.GroupControl8.Controls.Add(Label12)
        Me.GroupControl8.Controls.Add(Me.GroupControl9)
        Me.GroupControl8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl8.Location = New System.Drawing.Point(0, 193)
        Me.GroupControl8.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl8.Name = "GroupControl8"
        Me.GroupControl8.Size = New System.Drawing.Size(1397, 545)
        Me.GroupControl8.TabIndex = 2
        Me.GroupControl8.Text = "Datos Regla"
        '
        'ToolStripPR
        '
        Me.ToolStripPR.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ToolStripPR.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStripPR.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdGuardarMensaje})
        Me.ToolStripPR.Location = New System.Drawing.Point(2, 28)
        Me.ToolStripPR.Name = "ToolStripPR"
        Me.ToolStripPR.Size = New System.Drawing.Size(1393, 27)
        Me.ToolStripPR.TabIndex = 16
        Me.ToolStripPR.Text = "ToolStrip2"
        '
        'cmdGuardarMensaje
        '
        Me.cmdGuardarMensaje.Image = CType(resources.GetObject("cmdGuardarMensaje.Image"), System.Drawing.Image)
        Me.cmdGuardarMensaje.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdGuardarMensaje.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdGuardarMensaje.Name = "cmdGuardarMensaje"
        Me.cmdGuardarMensaje.Size = New System.Drawing.Size(130, 24)
        Me.cmdGuardarMensaje.Text = "Agregar a lista"
        '
        'lnkDestinatario
        '
        Me.lnkDestinatario.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lnkDestinatario.AutoSize = True
        Me.lnkDestinatario.Location = New System.Drawing.Point(51, 169)
        Me.lnkDestinatario.Name = "lnkDestinatario"
        Me.lnkDestinatario.Size = New System.Drawing.Size(75, 16)
        Me.lnkDestinatario.TabIndex = 15
        Me.lnkDestinatario.TabStop = True
        Me.lnkDestinatario.Text = "Destinatario"
        '
        'cmbDestinatario
        '
        Me.cmbDestinatario.Location = New System.Drawing.Point(169, 166)
        Me.cmbDestinatario.MenuManager = Me.RibbonControl
        Me.cmbDestinatario.Name = "cmbDestinatario"
        Me.cmbDestinatario.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbDestinatario.Size = New System.Drawing.Size(378, 22)
        Me.cmbDestinatario.TabIndex = 14
        '
        'cmbMensaje
        '
        Me.cmbMensaje.Location = New System.Drawing.Point(169, 131)
        Me.cmbMensaje.MenuManager = Me.RibbonControl
        Me.cmbMensaje.Name = "cmbMensaje"
        Me.cmbMensaje.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbMensaje.Size = New System.Drawing.Size(378, 22)
        Me.cmbMensaje.TabIndex = 13
        '
        'cmbProceso
        '
        Me.cmbProceso.Location = New System.Drawing.Point(169, 96)
        Me.cmbProceso.MenuManager = Me.RibbonControl
        Me.cmbProceso.Name = "cmbProceso"
        Me.cmbProceso.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbProceso.Size = New System.Drawing.Size(378, 22)
        Me.cmbProceso.TabIndex = 12
        '
        'lblNombrePropietario
        '
        Me.lblNombrePropietario.AutoSize = True
        Me.lblNombrePropietario.Font = New System.Drawing.Font("Tahoma", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNombrePropietario.Location = New System.Drawing.Point(50, 36)
        Me.lblNombrePropietario.Name = "lblNombrePropietario"
        Me.lblNombrePropietario.Size = New System.Drawing.Size(0, 21)
        Me.lblNombrePropietario.TabIndex = 0
        '
        'lnkMensaje
        '
        Me.lnkMensaje.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lnkMensaje.AutoSize = True
        Me.lnkMensaje.Location = New System.Drawing.Point(50, 134)
        Me.lnkMensaje.Name = "lnkMensaje"
        Me.lnkMensaje.Size = New System.Drawing.Size(55, 16)
        Me.lnkMensaje.TabIndex = 6
        Me.lnkMensaje.TabStop = True
        Me.lnkMensaje.Text = "Mensaje"
        '
        'lnkRegla
        '
        Me.lnkRegla.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lnkRegla.AutoSize = True
        Me.lnkRegla.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.lnkRegla.Location = New System.Drawing.Point(50, 102)
        Me.lnkRegla.Name = "lnkRegla"
        Me.lnkRegla.Size = New System.Drawing.Size(52, 16)
        Me.lnkRegla.TabIndex = 3
        Me.lnkRegla.TabStop = True
        Me.lnkRegla.Text = "Proceso"
        '
        'lblCodigo
        '
        Me.lblCodigo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCodigo.AutoSize = True
        Me.lblCodigo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCodigo.Location = New System.Drawing.Point(217, 73)
        Me.lblCodigo.Name = "lblCodigo"
        Me.lblCodigo.Size = New System.Drawing.Size(14, 17)
        Me.lblCodigo.TabIndex = 2
        Me.lblCodigo.Text = "-"
        '
        'GroupControl9
        '
        Me.GroupControl9.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupControl9.Controls.Add(Me.GridMensajes)
        Me.GroupControl9.Location = New System.Drawing.Point(0, 215)
        Me.GroupControl9.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl9.Name = "GroupControl9"
        Me.GroupControl9.Size = New System.Drawing.Size(1392, 324)
        Me.GroupControl9.TabIndex = 11
        Me.GroupControl9.Text = "Listado mensajes por proceso."
        '
        'GridMensajes
        '
        Me.GridMensajes.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GridMensajes.Cursor = System.Windows.Forms.Cursors.Default
        Me.GridMensajes.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        GridLevelNode1.RelationName = "Level1"
        Me.GridMensajes.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.GridMensajes.Location = New System.Drawing.Point(2, 59)
        Me.GridMensajes.MainView = Me.gvMensajes
        Me.GridMensajes.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GridMensajes.MenuManager = Me.RibbonControl
        Me.GridMensajes.Name = "GridMensajes"
        Me.GridMensajes.Size = New System.Drawing.Size(1387, 240)
        Me.GridMensajes.TabIndex = 3
        Me.GridMensajes.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvMensajes})
        '
        'gvMensajes
        '
        Me.gvMensajes.DetailHeight = 431
        Me.gvMensajes.GridControl = Me.GridMensajes
        Me.gvMensajes.Name = "gvMensajes"
        Me.gvMensajes.OptionsBehavior.Editable = False
        Me.gvMensajes.OptionsFind.AlwaysVisible = True
        Me.gvMensajes.OptionsView.ShowGroupPanel = False
        '
        'frmPropietarioReglasMensajes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1397, 768)
        Me.Controls.Add(Me.GroupControl8)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frmPropietarioReglasMensajes"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Propietario: Mensajes por Proceso"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl8.ResumeLayout(False)
        Me.GroupControl8.PerformLayout()
        Me.ToolStripPR.ResumeLayout(False)
        Me.ToolStripPR.PerformLayout()
        CType(Me.cmbDestinatario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbMensaje.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbProceso.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl9.ResumeLayout(False)
        CType(Me.GridMensajes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvMensajes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents mnuGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem3 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GroupControl8 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblNombrePropietario As Label
    Friend WithEvents lnkMensaje As LinkLabel
    Friend WithEvents lnkRegla As LinkLabel
    Friend WithEvents lblCodigo As Label
    Friend WithEvents GroupControl9 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GridMensajes As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvMensajes As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cmbMensaje As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbProceso As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lnkDestinatario As LinkLabel
    Friend WithEvents cmbDestinatario As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents ToolStripPR As ToolStrip
    Friend WithEvents cmdGuardarMensaje As ToolStripButton
End Class
