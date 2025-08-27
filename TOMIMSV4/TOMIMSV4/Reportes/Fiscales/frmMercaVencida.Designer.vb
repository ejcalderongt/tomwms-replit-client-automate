<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMercaVencida
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMercaVencida))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.cmdActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImprimir = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdSalir = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImpExcel = New DevExpress.XtraBars.BarButtonItem()
        Me.lblRegs = New DevExpress.XtraBars.BarStaticItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.grpFechas = New DevExpress.XtraEditors.GroupControl()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblPropietarios = New System.Windows.Forms.Label()
        Me.lcmbPropietario = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbBodega = New DevExpress.XtraEditors.LookUpEdit()
        Me.dtpfechaHasta = New System.Windows.Forms.DateTimePicker()
        Me.lblAl = New System.Windows.Forms.Label()
        Me.prg = New DevExpress.XtraEditors.ProgressBarControl()
        Me.dgridMercanciaVencida = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpFechas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpFechas.SuspendLayout()
        CType(Me.lcmbPropietario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.prg.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgridMercanciaVencida, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.cmdActualizar, Me.cmdImprimir, Me.cmdSalir, Me.cmdImpExcel, Me.lblRegs})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 6
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1454, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'cmdActualizar
        '
        Me.cmdActualizar.Caption = "Generar"
        Me.cmdActualizar.Id = 1
        Me.cmdActualizar.ImageOptions.SvgImage = CType(resources.GetObject("cmdActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdActualizar.Name = "cmdActualizar"
        '
        'cmdImprimir
        '
        Me.cmdImprimir.Caption = "Imprimir"
        Me.cmdImprimir.Id = 2
        Me.cmdImprimir.ImageOptions.SvgImage = CType(resources.GetObject("cmdImprimir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdImprimir.Name = "cmdImprimir"
        '
        'cmdSalir
        '
        Me.cmdSalir.Caption = "Salir"
        Me.cmdSalir.Id = 3
        Me.cmdSalir.ImageOptions.SvgImage = CType(resources.GetObject("cmdSalir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdSalir.Name = "cmdSalir"
        '
        'cmdImpExcel
        '
        Me.cmdImpExcel.Caption = "Exportar Excel"
        Me.cmdImpExcel.Id = 4
        Me.cmdImpExcel.ImageOptions.SvgImage = CType(resources.GetObject("cmdImpExcel.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdImpExcel.Name = "cmdImpExcel"
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
        Me.RibbonPage1.Text = "Mercaderia vencida"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdImprimir)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdImpExcel)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdSalir)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblRegs)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 699)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1454, 30)
        '
        'grpFechas
        '
        Me.grpFechas.Controls.Add(Me.Label1)
        Me.grpFechas.Controls.Add(Me.lblPropietarios)
        Me.grpFechas.Controls.Add(Me.lcmbPropietario)
        Me.grpFechas.Controls.Add(Me.cmbBodega)
        Me.grpFechas.Controls.Add(Me.dtpfechaHasta)
        Me.grpFechas.Controls.Add(Me.lblAl)
        Me.grpFechas.Dock = System.Windows.Forms.DockStyle.Left
        Me.grpFechas.Location = New System.Drawing.Point(0, 193)
        Me.grpFechas.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grpFechas.Name = "grpFechas"
        Me.grpFechas.Size = New System.Drawing.Size(308, 506)
        Me.grpFechas.TabIndex = 8
        Me.grpFechas.Text = "Filtros"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 16)
        Me.Label1.TabIndex = 40
        Me.Label1.Text = "Bodega:"
        '
        'lblPropietarios
        '
        Me.lblPropietarios.AutoSize = True
        Me.lblPropietarios.Location = New System.Drawing.Point(14, 180)
        Me.lblPropietarios.Name = "lblPropietarios"
        Me.lblPropietarios.Size = New System.Drawing.Size(51, 16)
        Me.lblPropietarios.TabIndex = 39
        Me.lblPropietarios.Text = "Cliente:"
        '
        'lcmbPropietario
        '
        Me.lcmbPropietario.Location = New System.Drawing.Point(12, 208)
        Me.lcmbPropietario.Name = "lcmbPropietario"
        Me.lcmbPropietario.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.lcmbPropietario.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lcmbPropietario.Size = New System.Drawing.Size(283, 22)
        Me.lcmbPropietario.TabIndex = 38
        '
        'cmbBodega
        '
        Me.cmbBodega.Location = New System.Drawing.Point(12, 66)
        Me.cmbBodega.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbBodega.MenuManager = Me.RibbonControl
        Me.cmbBodega.Name = "cmbBodega"
        Me.cmbBodega.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodega.Properties.NullText = ""
        Me.cmbBodega.Size = New System.Drawing.Size(283, 22)
        Me.cmbBodega.TabIndex = 9
        '
        'dtpfechaHasta
        '
        Me.dtpfechaHasta.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpfechaHasta.Location = New System.Drawing.Point(51, 115)
        Me.dtpfechaHasta.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtpfechaHasta.Name = "dtpfechaHasta"
        Me.dtpfechaHasta.Size = New System.Drawing.Size(233, 23)
        Me.dtpfechaHasta.TabIndex = 4
        '
        'lblAl
        '
        Me.lblAl.AutoSize = True
        Me.lblAl.Location = New System.Drawing.Point(14, 118)
        Me.lblAl.Name = "lblAl"
        Me.lblAl.Size = New System.Drawing.Size(23, 16)
        Me.lblAl.TabIndex = 3
        Me.lblAl.Text = "Al:"
        '
        'prg
        '
        Me.prg.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.prg.Location = New System.Drawing.Point(308, 644)
        Me.prg.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.prg.MenuManager = Me.RibbonControl
        Me.prg.Name = "prg"
        Me.prg.Size = New System.Drawing.Size(1146, 55)
        Me.prg.TabIndex = 12
        '
        'dgridMercanciaVencida
        '
        Me.dgridMercanciaVencida.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridMercanciaVencida.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dgridMercanciaVencida.Location = New System.Drawing.Point(308, 193)
        Me.dgridMercanciaVencida.MainView = Me.GridView1
        Me.dgridMercanciaVencida.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dgridMercanciaVencida.MenuManager = Me.RibbonControl
        Me.dgridMercanciaVencida.Name = "dgridMercanciaVencida"
        Me.dgridMercanciaVencida.Size = New System.Drawing.Size(1146, 451)
        Me.dgridMercanciaVencida.TabIndex = 16
        Me.dgridMercanciaVencida.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.DetailHeight = 431
        Me.GridView1.GridControl = Me.dgridMercanciaVencida
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.ReadOnly = True
        Me.GridView1.OptionsFind.AlwaysVisible = True
        Me.GridView1.OptionsView.ColumnAutoWidth = False
        '
        'frmMercaVencida
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1454, 729)
        Me.Controls.Add(Me.dgridMercanciaVencida)
        Me.Controls.Add(Me.prg)
        Me.Controls.Add(Me.grpFechas)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frmMercaVencida"
        Me.Ribbon = Me.RibbonControl
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Mercaderia Vencida"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpFechas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpFechas.ResumeLayout(False)
        Me.grpFechas.PerformLayout()
        CType(Me.lcmbPropietario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.prg.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgridMercanciaVencida, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents grpFechas As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cmbBodega As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents dtpfechaHasta As DateTimePicker
    Friend WithEvents lblAl As Label
    Friend WithEvents prg As DevExpress.XtraEditors.ProgressBarControl
    Friend WithEvents cmdActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdImprimir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdSalir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdImpExcel As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents dgridMercanciaVencida As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lblRegs As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents lblPropietarios As Label
    Friend WithEvents lcmbPropietario As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label1 As Label
End Class
