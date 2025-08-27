<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMovimientos_Detalle
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMovimientos_Detalle))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.cmdActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImprimir = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdSalir = New DevExpress.XtraBars.BarButtonItem()
        Me.lblRegs = New DevExpress.XtraBars.BarStaticItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.grdMovimientosDetalle = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.grpMovimientos = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbBodega = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtNoFac = New System.Windows.Forms.TextBox()
        Me.lblRec = New System.Windows.Forms.LinkLabel()
        Me.txtIdRec = New System.Windows.Forms.TextBox()
        Me.txtNoDocOC = New System.Windows.Forms.TextBox()
        Me.txtIdOC = New System.Windows.Forms.TextBox()
        Me.lblOC = New System.Windows.Forms.LinkLabel()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdMovimientosDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpMovimientos.SuspendLayout()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.cmdActualizar, Me.cmdImprimir, Me.cmdSalir, Me.lblRegs, Me.RibbonControl.SearchEditItem})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 6
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1396, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'cmdActualizar
        '
        Me.cmdActualizar.Caption = "Actualizar"
        Me.cmdActualizar.Id = 1
        Me.cmdActualizar.ImageOptions.SvgImage = CType(resources.GetObject("cmdActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdActualizar.Name = "cmdActualizar"
        '
        'cmdImprimir
        '
        Me.cmdImprimir.Caption = "Imprimir"
        Me.cmdImprimir.Id = 3
        Me.cmdImprimir.ImageOptions.SvgImage = CType(resources.GetObject("cmdImprimir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdImprimir.Name = "cmdImprimir"
        '
        'cmdSalir
        '
        Me.cmdSalir.Caption = "Salir"
        Me.cmdSalir.Id = 4
        Me.cmdSalir.ImageOptions.SvgImage = CType(resources.GetObject("cmdSalir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdSalir.Name = "cmdSalir"
        '
        'lblRegs
        '
        Me.lblRegs.Caption = "Registros: 0"
        Me.lblRegs.Id = 5
        Me.lblRegs.Name = "lblRegs"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Movimientos por documento de ingreso"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdImprimir)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdSalir)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblRegs)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 784)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1396, 30)
        '
        'grdMovimientosDetalle
        '
        Me.grdMovimientosDetalle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdMovimientosDetalle.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdMovimientosDetalle.Location = New System.Drawing.Point(0, 273)
        Me.grdMovimientosDetalle.MainView = Me.GridView1
        Me.grdMovimientosDetalle.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdMovimientosDetalle.MenuManager = Me.RibbonControl
        Me.grdMovimientosDetalle.Name = "grdMovimientosDetalle"
        Me.grdMovimientosDetalle.Size = New System.Drawing.Size(1396, 511)
        Me.grdMovimientosDetalle.TabIndex = 2
        Me.grdMovimientosDetalle.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.DetailHeight = 431
        Me.GridView1.GridControl = Me.grdMovimientosDetalle
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.ReadOnly = True
        Me.GridView1.OptionsFind.AlwaysVisible = True
        '
        'grpMovimientos
        '
        Me.grpMovimientos.Controls.Add(Me.Label1)
        Me.grpMovimientos.Controls.Add(Me.cmbBodega)
        Me.grpMovimientos.Controls.Add(Me.txtNoFac)
        Me.grpMovimientos.Controls.Add(Me.lblRec)
        Me.grpMovimientos.Controls.Add(Me.txtIdRec)
        Me.grpMovimientos.Controls.Add(Me.txtNoDocOC)
        Me.grpMovimientos.Controls.Add(Me.txtIdOC)
        Me.grpMovimientos.Controls.Add(Me.lblOC)
        Me.grpMovimientos.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpMovimientos.Location = New System.Drawing.Point(0, 193)
        Me.grpMovimientos.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grpMovimientos.Name = "grpMovimientos"
        Me.grpMovimientos.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grpMovimientos.Size = New System.Drawing.Size(1396, 80)
        Me.grpMovimientos.TabIndex = 3
        Me.grpMovimientos.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(27, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 17)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Bodega:"
        '
        'cmbBodega
        '
        Me.cmbBodega.Location = New System.Drawing.Point(89, 31)
        Me.cmbBodega.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbBodega.MenuManager = Me.RibbonControl
        Me.cmbBodega.Name = "cmbBodega"
        Me.cmbBodega.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodega.Properties.NullText = ""
        Me.cmbBodega.Size = New System.Drawing.Size(264, 22)
        Me.cmbBodega.TabIndex = 7
        '
        'txtNoFac
        '
        Me.txtNoFac.Enabled = False
        Me.txtNoFac.Location = New System.Drawing.Point(1162, 31)
        Me.txtNoFac.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNoFac.Name = "txtNoFac"
        Me.txtNoFac.Size = New System.Drawing.Size(180, 23)
        Me.txtNoFac.TabIndex = 6
        '
        'lblRec
        '
        Me.lblRec.AutoSize = True
        Me.lblRec.Location = New System.Drawing.Point(992, 41)
        Me.lblRec.Name = "lblRec"
        Me.lblRec.Size = New System.Drawing.Size(71, 17)
        Me.lblRec.TabIndex = 5
        Me.lblRec.TabStop = True
        Me.lblRec.Text = "Recepción"
        '
        'txtIdRec
        '
        Me.txtIdRec.Location = New System.Drawing.Point(1064, 31)
        Me.txtIdRec.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtIdRec.Name = "txtIdRec"
        Me.txtIdRec.Size = New System.Drawing.Size(90, 23)
        Me.txtIdRec.TabIndex = 4
        '
        'txtNoDocOC
        '
        Me.txtNoDocOC.Enabled = False
        Me.txtNoDocOC.Location = New System.Drawing.Point(635, 31)
        Me.txtNoDocOC.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNoDocOC.Name = "txtNoDocOC"
        Me.txtNoDocOC.Size = New System.Drawing.Size(299, 23)
        Me.txtNoDocOC.TabIndex = 3
        '
        'txtIdOC
        '
        Me.txtIdOC.Location = New System.Drawing.Point(537, 31)
        Me.txtIdOC.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtIdOC.Name = "txtIdOC"
        Me.txtIdOC.Size = New System.Drawing.Size(90, 23)
        Me.txtIdOC.TabIndex = 2
        '
        'lblOC
        '
        Me.lblOC.AutoSize = True
        Me.lblOC.Location = New System.Drawing.Point(422, 34)
        Me.lblOC.Name = "lblOC"
        Me.lblOC.Size = New System.Drawing.Size(118, 17)
        Me.lblOC.TabIndex = 1
        Me.lblOC.TabStop = True
        Me.lblOC.Text = "Orden de Compra"
        '
        'frmMovimientos_Detalle
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1396, 814)
        Me.Controls.Add(Me.grdMovimientosDetalle)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.grpMovimientos)
        Me.Controls.Add(Me.RibbonControl)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmMovimientos_Detalle"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Movimientos por documento de ingreso"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdMovimientosDetalle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpMovimientos.ResumeLayout(False)
        Me.grpMovimientos.PerformLayout()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents cmdActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdImprimir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdSalir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents grdMovimientosDetalle As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grpMovimientos As GroupBox
    Friend WithEvents lblOC As LinkLabel
    Friend WithEvents txtNoFac As TextBox
    Friend WithEvents lblRec As LinkLabel
    Friend WithEvents txtIdRec As TextBox
    Friend WithEvents txtNoDocOC As TextBox
    Friend WithEvents txtIdOC As TextBox
    Friend WithEvents lblRegs As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents Label1 As Label
    Friend WithEvents cmbBodega As DevExpress.XtraEditors.LookUpEdit
End Class
