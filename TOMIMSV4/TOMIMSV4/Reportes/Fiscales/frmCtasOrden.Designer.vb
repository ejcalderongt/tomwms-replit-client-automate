<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCtasOrden
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCtasOrden))
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
        Me.cmbRegimen = New DevExpress.XtraEditors.LookUpEdit()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblPrg = New System.Windows.Forms.Label()
        Me.cmbTransaccion = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblPropietario = New System.Windows.Forms.Label()
        Me.dtpfechaHasta = New System.Windows.Forms.DateTimePicker()
        Me.lblAl = New System.Windows.Forms.Label()
        Me.grdExistencias = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpFechas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpFechas.SuspendLayout()
        CType(Me.cmbRegimen.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTransaccion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdExistencias, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.RibbonControl.SearchEditItem, Me.cmdActualizar, Me.cmdImprimir, Me.cmdSalir, Me.cmdImpExcel, Me.lblRegs})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 6
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1326, 193)
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
        Me.RibbonPage1.Text = "Cuentas de Orden"
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
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 693)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1326, 30)
        '
        'grpFechas
        '
        Me.grpFechas.Controls.Add(Me.cmbRegimen)
        Me.grpFechas.Controls.Add(Me.Label1)
        Me.grpFechas.Controls.Add(Me.lblPrg)
        Me.grpFechas.Controls.Add(Me.cmbTransaccion)
        Me.grpFechas.Controls.Add(Me.lblPropietario)
        Me.grpFechas.Controls.Add(Me.dtpfechaHasta)
        Me.grpFechas.Controls.Add(Me.lblAl)
        Me.grpFechas.Dock = System.Windows.Forms.DockStyle.Left
        Me.grpFechas.Location = New System.Drawing.Point(0, 193)
        Me.grpFechas.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grpFechas.Name = "grpFechas"
        Me.grpFechas.Size = New System.Drawing.Size(304, 500)
        Me.grpFechas.TabIndex = 7
        Me.grpFechas.Text = "Filtros"
        '
        'cmbRegimen
        '
        Me.cmbRegimen.Location = New System.Drawing.Point(12, 120)
        Me.cmbRegimen.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbRegimen.MenuManager = Me.RibbonControl
        Me.cmbRegimen.Name = "cmbRegimen"
        Me.cmbRegimen.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbRegimen.Properties.NullText = ""
        Me.cmbRegimen.Size = New System.Drawing.Size(283, 22)
        Me.cmbRegimen.TabIndex = 9
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 96)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 17)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Regimen:"
        '
        'lblPrg
        '
        Me.lblPrg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPrg.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblPrg.Location = New System.Drawing.Point(2, 386)
        Me.lblPrg.Name = "lblPrg"
        Me.lblPrg.Size = New System.Drawing.Size(300, 112)
        Me.lblPrg.TabIndex = 7
        '
        'cmbTransaccion
        '
        Me.cmbTransaccion.Location = New System.Drawing.Point(12, 183)
        Me.cmbTransaccion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbTransaccion.MenuManager = Me.RibbonControl
        Me.cmbTransaccion.Name = "cmbTransaccion"
        Me.cmbTransaccion.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTransaccion.Properties.NullText = ""
        Me.cmbTransaccion.Size = New System.Drawing.Size(283, 22)
        Me.cmbTransaccion.TabIndex = 6
        '
        'lblPropietario
        '
        Me.lblPropietario.AutoSize = True
        Me.lblPropietario.Location = New System.Drawing.Point(12, 159)
        Me.lblPropietario.Name = "lblPropietario"
        Me.lblPropietario.Size = New System.Drawing.Size(115, 17)
        Me.lblPropietario.TabIndex = 6
        Me.lblPropietario.Text = "Tipo movimiento:"
        '
        'dtpfechaHasta
        '
        Me.dtpfechaHasta.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpfechaHasta.Location = New System.Drawing.Point(49, 46)
        Me.dtpfechaHasta.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtpfechaHasta.Name = "dtpfechaHasta"
        Me.dtpfechaHasta.Size = New System.Drawing.Size(246, 23)
        Me.dtpfechaHasta.TabIndex = 4
        '
        'lblAl
        '
        Me.lblAl.AutoSize = True
        Me.lblAl.Location = New System.Drawing.Point(12, 46)
        Me.lblAl.Name = "lblAl"
        Me.lblAl.Size = New System.Drawing.Size(32, 17)
        Me.lblAl.TabIndex = 3
        Me.lblAl.Text = "Del:"
        '
        'grdExistencias
        '
        Me.grdExistencias.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdExistencias.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdExistencias.Location = New System.Drawing.Point(304, 193)
        Me.grdExistencias.MainView = Me.GridView1
        Me.grdExistencias.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdExistencias.MenuManager = Me.RibbonControl
        Me.grdExistencias.Name = "grdExistencias"
        Me.grdExistencias.Size = New System.Drawing.Size(1022, 500)
        Me.grdExistencias.TabIndex = 16
        Me.grdExistencias.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.DetailHeight = 431
        Me.GridView1.GridControl = Me.grdExistencias
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.ReadOnly = True
        Me.GridView1.OptionsFind.AlwaysVisible = True
        Me.GridView1.OptionsView.ColumnAutoWidth = False
        '
        'frmCtasOrden
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1326, 723)
        Me.Controls.Add(Me.grdExistencias)
        Me.Controls.Add(Me.grpFechas)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frmCtasOrden"
        Me.Ribbon = Me.RibbonControl
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Cuentas de Orden Detalle"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpFechas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpFechas.ResumeLayout(False)
        Me.grpFechas.PerformLayout()
        CType(Me.cmbRegimen.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTransaccion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdExistencias, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents grpFechas As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cmbRegimen As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label1 As Label
    Friend WithEvents lblPrg As Label
    Friend WithEvents cmbTransaccion As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblPropietario As Label
    Friend WithEvents dtpfechaHasta As DateTimePicker
    Friend WithEvents lblAl As Label
    Friend WithEvents cmdActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdImprimir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdSalir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdImpExcel As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents grdExistencias As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lblRegs As DevExpress.XtraBars.BarStaticItem
End Class
