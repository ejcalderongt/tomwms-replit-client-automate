<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmRptStockTransito
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRptStockTransito))
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Me.btnColap = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.btnImprimir = New DevExpress.XtraBars.BarButtonItem()
        Me.btnActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.btnSalir = New DevExpress.XtraBars.BarButtonItem()
        Me.lblRegs = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminarLayoutGrid = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuGuardarLayoutGrid = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar1 = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.grpFechas = New DevExpress.XtraEditors.GroupControl()
        Me.cmbBodega = New DevExpress.XtraEditors.LookUpEdit()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblPrg = New System.Windows.Forms.Label()
        Me.grd = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.btnColap, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpFechas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpFechas.SuspendLayout()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnColap
        '
        Me.btnColap.ExpandCollapseItem.Id = 0
        Me.btnColap.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.btnColap.ExpandCollapseItem, Me.btnImprimir, Me.btnActualizar, Me.btnSalir, Me.lblRegs, Me.mnuEliminarLayoutGrid, Me.mnuGuardarLayoutGrid})
        Me.btnColap.Location = New System.Drawing.Point(0, 0)
        Me.btnColap.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnColap.MaxItemId = 8
        Me.btnColap.Name = "btnColap"
        Me.btnColap.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.btnColap.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.btnColap.Size = New System.Drawing.Size(1665, 193)
        Me.btnColap.StatusBar = Me.RibbonStatusBar1
        '
        'btnImprimir
        '
        Me.btnImprimir.Caption = "Imprimir"
        Me.btnImprimir.Id = 1
        Me.btnImprimir.ImageOptions.SvgImage = CType(resources.GetObject("btnImprimir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnImprimir.Name = "btnImprimir"
        '
        'btnActualizar
        '
        Me.btnActualizar.Caption = "Actualizar"
        Me.btnActualizar.Id = 2
        Me.btnActualizar.ImageOptions.SvgImage = CType(resources.GetObject("btnActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnActualizar.Name = "btnActualizar"
        '
        'btnSalir
        '
        Me.btnSalir.Caption = "Salir"
        Me.btnSalir.Id = 4
        Me.btnSalir.ImageOptions.SvgImage = CType(resources.GetObject("btnSalir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnSalir.Name = "btnSalir"
        '
        'lblRegs
        '
        Me.lblRegs.Caption = "Registros:0"
        Me.lblRegs.Id = 5
        Me.lblRegs.Name = "lblRegs"
        '
        'mnuEliminarLayoutGrid
        '
        Me.mnuEliminarLayoutGrid.Caption = "Eliminar diseño grid"
        Me.mnuEliminarLayoutGrid.Id = 6
        Me.mnuEliminarLayoutGrid.ImageOptions.SvgImage = CType(resources.GetObject("mnuEliminarLayoutGrid.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEliminarLayoutGrid.Name = "mnuEliminarLayoutGrid"
        '
        'mnuGuardarLayoutGrid
        '
        Me.mnuGuardarLayoutGrid.Caption = "Guardar diseño de grid"
        Me.mnuGuardarLayoutGrid.Id = 7
        Me.mnuGuardarLayoutGrid.ImageOptions.SvgImage = CType(resources.GetObject("mnuGuardarLayoutGrid.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuGuardarLayoutGrid.Name = "mnuGuardarLayoutGrid"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Stock en Tránsito"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.btnImprimir)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.btnActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardarLayoutGrid)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEliminarLayoutGrid)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.btnSalir)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar1
        '
        Me.RibbonStatusBar1.ItemLinks.Add(Me.lblRegs)
        Me.RibbonStatusBar1.Location = New System.Drawing.Point(304, 695)
        Me.RibbonStatusBar1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar1.Name = "RibbonStatusBar1"
        Me.RibbonStatusBar1.Ribbon = Me.btnColap
        Me.RibbonStatusBar1.Size = New System.Drawing.Size(1361, 30)
        '
        'grpFechas
        '
        Me.grpFechas.Controls.Add(Me.cmbBodega)
        Me.grpFechas.Controls.Add(Me.Label1)
        Me.grpFechas.Controls.Add(Me.lblPrg)
        Me.grpFechas.Dock = System.Windows.Forms.DockStyle.Left
        Me.grpFechas.Location = New System.Drawing.Point(0, 193)
        Me.grpFechas.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grpFechas.Name = "grpFechas"
        Me.grpFechas.Size = New System.Drawing.Size(304, 532)
        Me.grpFechas.TabIndex = 4
        Me.grpFechas.Text = "Filtros"
        '
        'cmbBodega
        '
        Me.cmbBodega.Location = New System.Drawing.Point(14, 63)
        Me.cmbBodega.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbBodega.Name = "cmbBodega"
        Me.cmbBodega.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodega.Properties.NullText = ""
        Me.cmbBodega.Size = New System.Drawing.Size(283, 22)
        Me.cmbBodega.TabIndex = 9
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 16)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Bodega:"
        '
        'lblPrg
        '
        Me.lblPrg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPrg.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblPrg.Location = New System.Drawing.Point(2, 418)
        Me.lblPrg.Name = "lblPrg"
        Me.lblPrg.Size = New System.Drawing.Size(300, 112)
        Me.lblPrg.TabIndex = 7
        '
        'grd
        '
        Me.grd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grd.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        GridLevelNode1.RelationName = "Level1"
        Me.grd.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.grd.Location = New System.Drawing.Point(304, 193)
        Me.grd.MainView = Me.GridView1
        Me.grd.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grd.Name = "grd"
        Me.grd.Size = New System.Drawing.Size(1361, 502)
        Me.grd.TabIndex = 6
        Me.grd.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.grd
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.ReadOnly = True
        Me.GridView1.OptionsFind.AlwaysVisible = True
        Me.GridView1.OptionsView.ColumnAutoWidth = False
        Me.GridView1.OptionsView.ShowFooter = True
        '
        'frmRptStockTransito
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1665, 725)
        Me.Controls.Add(Me.grd)
        Me.Controls.Add(Me.RibbonStatusBar1)
        Me.Controls.Add(Me.grpFechas)
        Me.Controls.Add(Me.btnColap)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmRptStockTransito"
        Me.Ribbon = Me.btnColap
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar1
        Me.Text = "Stock en Tránsito"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.btnColap, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpFechas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpFechas.ResumeLayout(False)
        Me.grpFechas.PerformLayout()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnColap As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents btnImprimir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnSalir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonStatusBar1 As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents lblRegs As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEliminarLayoutGrid As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuGuardarLayoutGrid As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents grpFechas As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cmbBodega As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label1 As Label
    Friend WithEvents lblPrg As Label
    Friend WithEvents grd As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
End Class
