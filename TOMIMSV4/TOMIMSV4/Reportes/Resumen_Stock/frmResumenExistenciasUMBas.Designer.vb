<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmResumenExistenciasUMBas
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmResumenExistenciasUMBas))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuImprimir = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuSalir = New DevExpress.XtraBars.BarButtonItem()
        Me.lblReg = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.RibbonStatusBar1 = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.lblRegs = New DevExpress.XtraBars.BarStaticItem()
        Me.grdResumenUMB = New DevExpress.XtraGrid.GridControl()
        Me.EncabezadoBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsDetalleExistenciasBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsDetalleExistencias = New DsDetalleExistencias()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtNombreProducto = New DevExpress.XtraEditors.TextEdit()
        Me.txtIdProducto = New DevExpress.XtraEditors.TextEdit()
        Me.linklblProducto = New System.Windows.Forms.LinkLabel()
        Me.cmbPropietario = New DevExpress.XtraEditors.LookUpEdit()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbBodega = New DevExpress.XtraEditors.LookUpEdit()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdResumenUMB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EncabezadoBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsDetalleExistenciasBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsDetalleExistencias, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.txtNombreProducto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdProducto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.RibbonControl.SearchEditItem, Me.mnuActualizar, Me.mnuImprimir, Me.mnuSalir, Me.lblReg, Me.BarButtonItem1})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 6
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1377, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar1
        '
        'mnuActualizar
        '
        Me.mnuActualizar.Caption = "Actualizar"
        Me.mnuActualizar.Id = 1
        Me.mnuActualizar.ImageOptions.SvgImage = CType(resources.GetObject("mnuActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuActualizar.Name = "mnuActualizar"
        '
        'mnuImprimir
        '
        Me.mnuImprimir.Caption = "Imprimir"
        Me.mnuImprimir.Id = 2
        Me.mnuImprimir.ImageOptions.SvgImage = CType(resources.GetObject("mnuImprimir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuImprimir.Name = "mnuImprimir"
        '
        'mnuSalir
        '
        Me.mnuSalir.Caption = "Salir"
        Me.mnuSalir.Id = 3
        Me.mnuSalir.ImageOptions.SvgImage = CType(resources.GetObject("mnuSalir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuSalir.Name = "mnuSalir"
        '
        'lblReg
        '
        Me.lblReg.Caption = "Registros:0"
        Me.lblReg.Id = 4
        Me.lblReg.Name = "lblReg"
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "Exportar a Excel"
        Me.BarButtonItem1.Id = 5
        Me.BarButtonItem1.ImageOptions.SvgImage = CType(resources.GetObject("BarButtonItem1.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Resumen de Existencias en UMBas"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuImprimir)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.BarButtonItem1)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuSalir)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblReg)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 912)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1377, 30)
        '
        'RibbonStatusBar1
        '
        Me.RibbonStatusBar1.ItemLinks.Add(Me.lblRegs)
        Me.RibbonStatusBar1.Location = New System.Drawing.Point(0, 481)
        Me.RibbonStatusBar1.Name = "RibbonStatusBar1"
        Me.RibbonStatusBar1.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar1.Size = New System.Drawing.Size(907, 33)
        '
        'lblRegs
        '
        Me.lblRegs.Caption = "Registros: 0"
        Me.lblRegs.Id = 4
        Me.lblRegs.Name = "lblRegs"
        '
        'grdResumenUMB
        '
        Me.grdResumenUMB.DataSource = Me.EncabezadoBindingSource
        Me.grdResumenUMB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdResumenUMB.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdResumenUMB.Location = New System.Drawing.Point(327, 193)
        Me.grdResumenUMB.MainView = Me.GridView1
        Me.grdResumenUMB.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdResumenUMB.MenuManager = Me.RibbonControl
        Me.grdResumenUMB.Name = "grdResumenUMB"
        Me.grdResumenUMB.Size = New System.Drawing.Size(1050, 719)
        Me.grdResumenUMB.TabIndex = 1
        Me.grdResumenUMB.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'EncabezadoBindingSource
        '
        Me.EncabezadoBindingSource.DataMember = "Encabezado"
        Me.EncabezadoBindingSource.DataSource = Me.DsDetalleExistenciasBindingSource
        '
        'DsDetalleExistenciasBindingSource
        '
        Me.DsDetalleExistenciasBindingSource.DataSource = Me.DsDetalleExistencias
        Me.DsDetalleExistenciasBindingSource.Position = 0
        '
        'DsDetalleExistencias
        '
        Me.DsDetalleExistencias.DataSetName = "DsDetalleExistencias"
        Me.DsDetalleExistencias.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'GridView1
        '
        Me.GridView1.DetailHeight = 431
        Me.GridView1.GridControl = Me.grdResumenUMB
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.ReadOnly = True
        Me.GridView1.OptionsFind.AlwaysVisible = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtNombreProducto)
        Me.GroupBox1.Controls.Add(Me.txtIdProducto)
        Me.GroupBox1.Controls.Add(Me.linklblProducto)
        Me.GroupBox1.Controls.Add(Me.cmbPropietario)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cmbBodega)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupBox1.Location = New System.Drawing.Point(0, 193)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(327, 719)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Filtros"
        '
        'txtNombreProducto
        '
        Me.txtNombreProducto.Location = New System.Drawing.Point(101, 239)
        Me.txtNombreProducto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNombreProducto.MenuManager = Me.RibbonControl
        Me.txtNombreProducto.Name = "txtNombreProducto"
        Me.txtNombreProducto.Size = New System.Drawing.Size(218, 22)
        Me.txtNombreProducto.TabIndex = 6
        '
        'txtIdProducto
        '
        Me.txtIdProducto.Location = New System.Drawing.Point(3, 239)
        Me.txtIdProducto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtIdProducto.MenuManager = Me.RibbonControl
        Me.txtIdProducto.Name = "txtIdProducto"
        Me.txtIdProducto.Size = New System.Drawing.Size(91, 22)
        Me.txtIdProducto.TabIndex = 5
        '
        'linklblProducto
        '
        Me.linklblProducto.AutoSize = True
        Me.linklblProducto.Location = New System.Drawing.Point(3, 198)
        Me.linklblProducto.Name = "linklblProducto"
        Me.linklblProducto.Size = New System.Drawing.Size(65, 17)
        Me.linklblProducto.TabIndex = 4
        Me.linklblProducto.TabStop = True
        Me.linklblProducto.Text = "Producto"
        '
        'cmbPropietario
        '
        Me.cmbPropietario.Location = New System.Drawing.Point(7, 123)
        Me.cmbPropietario.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbPropietario.MenuManager = Me.RibbonControl
        Me.cmbPropietario.Name = "cmbPropietario"
        Me.cmbPropietario.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPropietario.Properties.NullText = ""
        Me.cmbPropietario.Size = New System.Drawing.Size(313, 22)
        Me.cmbPropietario.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 103)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 17)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Propietario:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 38)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Bodega:"
        '
        'cmbBodega
        '
        Me.cmbBodega.Location = New System.Drawing.Point(7, 58)
        Me.cmbBodega.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbBodega.MenuManager = Me.RibbonControl
        Me.cmbBodega.Name = "cmbBodega"
        Me.cmbBodega.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodega.Properties.NullText = ""
        Me.cmbBodega.Size = New System.Drawing.Size(313, 22)
        Me.cmbBodega.TabIndex = 1
        '
        'frmResumenExistenciasUMBas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1377, 942)
        Me.Controls.Add(Me.grdResumenUMB)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmResumenExistenciasUMBas"
        Me.Ribbon = Me.RibbonControl
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Resumen de Existencias en UMBas"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdResumenUMB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EncabezadoBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsDetalleExistenciasBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsDetalleExistencias, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.txtNombreProducto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdProducto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents mnuActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuImprimir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuSalir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents grdResumenUMB As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents EncabezadoBindingSource As BindingSource
    Friend WithEvents DsDetalleExistenciasBindingSource As BindingSource
    Friend WithEvents DsDetalleExistencias As DsDetalleExistencias
    Friend WithEvents RibbonStatusBar1 As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents lblRegs As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents lblReg As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents cmbPropietario As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents cmbBodega As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents txtNombreProducto As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtIdProducto As DevExpress.XtraEditors.TextEdit
    Friend WithEvents linklblProducto As LinkLabel
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
End Class
