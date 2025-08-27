<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNavEnt
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
        Dim Label10 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNavEnt))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.txtPunto = New System.Windows.Forms.TextBox()
        Me.txtNombre = New System.Windows.Forms.TextBox()
        Me.lblCod = New System.Windows.Forms.Label()
        Me.lblPunto = New System.Windows.Forms.Label()
        Me.lblNombre = New System.Windows.Forms.Label()
        Me.lblCodigo = New System.Windows.Forms.Label()
        Me.grpValor = New DevExpress.XtraEditors.GroupControl()
        Me.grdFiltros = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.txtValor = New System.Windows.Forms.TextBox()
        Me.lblCodFiltro = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ToolStripEnt = New System.Windows.Forms.ToolStrip()
        Me.cmdNewV = New System.Windows.Forms.ToolStripButton()
        Me.cmdGuardarV = New System.Windows.Forms.ToolStripButton()
        Me.cmdEliminar = New System.Windows.Forms.ToolStripButton()
        Label10 = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.grpValor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpValor.SuspendLayout()
        CType(Me.grdFiltros, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStripEnt.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label10
        '
        Label10.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label10.AutoSize = True
        Label10.Location = New System.Drawing.Point(12, 115)
        Label10.Name = "Label10"
        Label10.Size = New System.Drawing.Size(35, 13)
        Label10.TabIndex = 3
        Label10.Text = "Valor:"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuGuardar, Me.mnuActualizar, Me.mnuEliminar})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 4
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1002, 143)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'mnuGuardar
        '
        Me.mnuGuardar.Caption = "Guardar"
        Me.mnuGuardar.Id = 1
        Me.mnuGuardar.ImageOptions.Image = CType(resources.GetObject("mnuGuardar.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuGuardar.ImageOptions.LargeImage = CType(resources.GetObject("mnuGuardar.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuGuardar.Name = "mnuGuardar"
        '
        'mnuActualizar
        '
        Me.mnuActualizar.Caption = "Actualizar"
        Me.mnuActualizar.Id = 2
        Me.mnuActualizar.ImageOptions.Image = CType(resources.GetObject("mnuActualizar.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuActualizar.ImageOptions.LargeImage = CType(resources.GetObject("mnuActualizar.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuActualizar.Name = "mnuActualizar"
        '
        'mnuEliminar
        '
        Me.mnuEliminar.Caption = "Eliminar"
        Me.mnuEliminar.Id = 3
        Me.mnuEliminar.ImageOptions.Image = CType(resources.GetObject("mnuEliminar.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuEliminar.ImageOptions.LargeImage = CType(resources.GetObject("mnuEliminar.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuEliminar.Name = "mnuEliminar"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Menú"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEliminar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 729)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1002, 31)
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.txtPunto)
        Me.GroupControl1.Controls.Add(Me.txtNombre)
        Me.GroupControl1.Controls.Add(Me.lblCod)
        Me.GroupControl1.Controls.Add(Me.lblPunto)
        Me.GroupControl1.Controls.Add(Me.lblNombre)
        Me.GroupControl1.Controls.Add(Me.lblCodigo)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 143)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1002, 193)
        Me.GroupControl1.TabIndex = 0
        '
        'txtPunto
        '
        Me.txtPunto.Location = New System.Drawing.Point(137, 124)
        Me.txtPunto.Name = "txtPunto"
        Me.txtPunto.Size = New System.Drawing.Size(149, 21)
        Me.txtPunto.TabIndex = 5
        '
        'txtNombre
        '
        Me.txtNombre.Location = New System.Drawing.Point(137, 89)
        Me.txtNombre.Name = "txtNombre"
        Me.txtNombre.Size = New System.Drawing.Size(149, 21)
        Me.txtNombre.TabIndex = 3
        '
        'lblCod
        '
        Me.lblCod.AutoSize = True
        Me.lblCod.Location = New System.Drawing.Point(134, 55)
        Me.lblCod.Name = "lblCod"
        Me.lblCod.Size = New System.Drawing.Size(15, 13)
        Me.lblCod.TabIndex = 1
        Me.lblCod.Text = "--"
        '
        'lblPunto
        '
        Me.lblPunto.AutoSize = True
        Me.lblPunto.Location = New System.Drawing.Point(28, 127)
        Me.lblPunto.Name = "lblPunto"
        Me.lblPunto.Size = New System.Drawing.Size(39, 13)
        Me.lblPunto.TabIndex = 4
        Me.lblPunto.Text = "Punto:"
        '
        'lblNombre
        '
        Me.lblNombre.AutoSize = True
        Me.lblNombre.Location = New System.Drawing.Point(28, 92)
        Me.lblNombre.Name = "lblNombre"
        Me.lblNombre.Size = New System.Drawing.Size(48, 13)
        Me.lblNombre.TabIndex = 2
        Me.lblNombre.Text = "Nombre:"
        '
        'lblCodigo
        '
        Me.lblCodigo.AutoSize = True
        Me.lblCodigo.Location = New System.Drawing.Point(28, 55)
        Me.lblCodigo.Name = "lblCodigo"
        Me.lblCodigo.Size = New System.Drawing.Size(44, 13)
        Me.lblCodigo.TabIndex = 0
        Me.lblCodigo.Text = "Código:"
        '
        'grpValor
        '
        Me.grpValor.Controls.Add(Me.grdFiltros)
        Me.grpValor.Controls.Add(Me.txtValor)
        Me.grpValor.Controls.Add(Me.lblCodFiltro)
        Me.grpValor.Controls.Add(Me.Label1)
        Me.grpValor.Controls.Add(Label10)
        Me.grpValor.Controls.Add(Me.ToolStripEnt)
        Me.grpValor.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.grpValor.Location = New System.Drawing.Point(0, 336)
        Me.grpValor.Name = "grpValor"
        Me.grpValor.Size = New System.Drawing.Size(1002, 393)
        Me.grpValor.TabIndex = 1
        Me.grpValor.Text = "Filtros"
        '
        'grdFiltros
        '
        Me.grdFiltros.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.grdFiltros.Location = New System.Drawing.Point(2, 159)
        Me.grdFiltros.MainView = Me.GridView1
        Me.grdFiltros.MenuManager = Me.RibbonControl
        Me.grdFiltros.Name = "grdFiltros"
        Me.grdFiltros.Size = New System.Drawing.Size(998, 232)
        Me.grdFiltros.TabIndex = 5
        Me.grdFiltros.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.grdFiltros
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.ReadOnly = True
        Me.GridView1.OptionsFind.AlwaysVisible = True
        '
        'txtValor
        '
        Me.txtValor.Location = New System.Drawing.Point(114, 107)
        Me.txtValor.Name = "txtValor"
        Me.txtValor.Size = New System.Drawing.Size(153, 21)
        Me.txtValor.TabIndex = 4
        '
        'lblCodFiltro
        '
        Me.lblCodFiltro.AutoSize = True
        Me.lblCodFiltro.Location = New System.Drawing.Point(111, 80)
        Me.lblCodFiltro.Name = "lblCodFiltro"
        Me.lblCodFiltro.Size = New System.Drawing.Size(15, 13)
        Me.lblCodFiltro.TabIndex = 2
        Me.lblCodFiltro.Text = "--"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 80)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Codigo:"
        '
        'ToolStripEnt
        '
        Me.ToolStripEnt.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdNewV, Me.cmdGuardarV, Me.cmdEliminar})
        Me.ToolStripEnt.Location = New System.Drawing.Point(2, 20)
        Me.ToolStripEnt.Name = "ToolStripEnt"
        Me.ToolStripEnt.Size = New System.Drawing.Size(998, 25)
        Me.ToolStripEnt.TabIndex = 0
        Me.ToolStripEnt.Text = "ToolStrip1"
        '
        'cmdNewV
        '
        Me.cmdNewV.Image = CType(resources.GetObject("cmdNewV.Image"), System.Drawing.Image)
        Me.cmdNewV.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdNewV.Name = "cmdNewV"
        Me.cmdNewV.Size = New System.Drawing.Size(62, 22)
        Me.cmdNewV.Text = "Nuevo"
        '
        'cmdGuardarV
        '
        Me.cmdGuardarV.Image = CType(resources.GetObject("cmdGuardarV.Image"), System.Drawing.Image)
        Me.cmdGuardarV.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdGuardarV.Name = "cmdGuardarV"
        Me.cmdGuardarV.Size = New System.Drawing.Size(69, 22)
        Me.cmdGuardarV.Text = "Guardar"
        '
        'cmdEliminar
        '
        Me.cmdEliminar.Image = CType(resources.GetObject("cmdEliminar.Image"), System.Drawing.Image)
        Me.cmdEliminar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdEliminar.Name = "cmdEliminar"
        Me.cmdEliminar.Size = New System.Drawing.Size(70, 22)
        Me.cmdEliminar.Text = "Eliminar"
        '
        'frmNavEnt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1002, 760)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.grpValor)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frmNavEnt"
        Me.Ribbon = Me.RibbonControl
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Entidad"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.grpValor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpValor.ResumeLayout(False)
        Me.grpValor.PerformLayout()
        CType(Me.grdFiltros, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStripEnt.ResumeLayout(False)
        Me.ToolStripEnt.PerformLayout()
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
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblPunto As Label
    Friend WithEvents lblNombre As Label
    Friend WithEvents lblCodigo As Label
    Friend WithEvents lblCod As Label
    Friend WithEvents txtNombre As TextBox
    Friend WithEvents txtPunto As TextBox
    Friend WithEvents grpValor As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ToolStripEnt As ToolStrip
    Friend WithEvents cmdNewV As ToolStripButton
    Friend WithEvents cmdGuardarV As ToolStripButton
    Friend WithEvents cmdEliminar As ToolStripButton
    Friend WithEvents lblCodFiltro As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents txtValor As TextBox
    Friend WithEvents grdFiltros As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
End Class
