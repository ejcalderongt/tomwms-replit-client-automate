<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStockResJornadaMerca
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmStockResJornadaMerca))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.btnGenerar = New DevExpress.XtraBars.BarButtonItem()
        Me.btnImport = New DevExpress.XtraBars.BarButtonItem()
        Me.btnSalir = New DevExpress.XtraBars.BarButtonItem()
        Me.lblRegs = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.grpFechas = New DevExpress.XtraEditors.GroupControl()
        Me.deMesReporte = New DevExpress.XtraEditors.DateEdit()
        Me.cmbRegimen = New DevExpress.XtraEditors.LookUpEdit()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblPrg = New System.Windows.Forms.Label()
        Me.lblDel = New System.Windows.Forms.Label()
        Me.dgridDetalle = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpFechas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpFechas.SuspendLayout()
        CType(Me.deMesReporte.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deMesReporte.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbRegimen.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgridDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.btnGenerar, Me.btnImport, Me.btnSalir, Me.lblRegs})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 6
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1081, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'btnGenerar
        '
        Me.btnGenerar.Caption = "Generar"
        Me.btnGenerar.Id = 1
        Me.btnGenerar.ImageOptions.SvgImage = CType(resources.GetObject("btnGenerar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnGenerar.Name = "btnGenerar"
        '
        'btnImport
        '
        Me.btnImport.Caption = "Exportar Excel"
        Me.btnImport.Id = 2
        Me.btnImport.ImageOptions.SvgImage = CType(resources.GetObject("btnImport.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnImport.Name = "btnImport"
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
        Me.lblRegs.Caption = "Registros: 0"
        Me.lblRegs.Id = 5
        Me.lblRegs.Name = "lblRegs"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Valorización por mercaderia"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.btnGenerar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.btnImport)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.btnSalir)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblRegs)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 596)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1081, 30)
        '
        'grpFechas
        '
        Me.grpFechas.Controls.Add(Me.deMesReporte)
        Me.grpFechas.Controls.Add(Me.cmbRegimen)
        Me.grpFechas.Controls.Add(Me.Label2)
        Me.grpFechas.Controls.Add(Me.lblPrg)
        Me.grpFechas.Controls.Add(Me.lblDel)
        Me.grpFechas.Dock = System.Windows.Forms.DockStyle.Left
        Me.grpFechas.Location = New System.Drawing.Point(0, 193)
        Me.grpFechas.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grpFechas.Name = "grpFechas"
        Me.grpFechas.Size = New System.Drawing.Size(316, 403)
        Me.grpFechas.TabIndex = 10
        Me.grpFechas.Text = "Filtros"
        '
        'deMesReporte
        '
        Me.deMesReporte.EditValue = Nothing
        Me.deMesReporte.Location = New System.Drawing.Point(106, 117)
        Me.deMesReporte.MenuManager = Me.RibbonControl
        Me.deMesReporte.Name = "deMesReporte"
        Me.deMesReporte.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deMesReporte.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deMesReporte.Properties.Mask.EditMask = "y"
        Me.deMesReporte.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView
        Me.deMesReporte.Size = New System.Drawing.Size(186, 22)
        Me.deMesReporte.TabIndex = 26
        '
        'cmbRegimen
        '
        Me.cmbRegimen.Location = New System.Drawing.Point(13, 71)
        Me.cmbRegimen.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbRegimen.MenuManager = Me.RibbonControl
        Me.cmbRegimen.Name = "cmbRegimen"
        Me.cmbRegimen.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbRegimen.Properties.NullText = ""
        Me.cmbRegimen.Size = New System.Drawing.Size(279, 22)
        Me.cmbRegimen.TabIndex = 36
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 16)
        Me.Label2.TabIndex = 37
        Me.Label2.Text = "Regimen:"
        '
        'lblPrg
        '
        Me.lblPrg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPrg.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblPrg.Location = New System.Drawing.Point(2, 340)
        Me.lblPrg.Name = "lblPrg"
        Me.lblPrg.Size = New System.Drawing.Size(312, 61)
        Me.lblPrg.TabIndex = 7
        '
        'lblDel
        '
        Me.lblDel.AutoSize = True
        Me.lblDel.Location = New System.Drawing.Point(11, 120)
        Me.lblDel.Name = "lblDel"
        Me.lblDel.Size = New System.Drawing.Size(81, 16)
        Me.lblDel.TabIndex = 3
        Me.lblDel.Text = "Mes reporte:"
        '
        'dgridDetalle
        '
        Me.dgridDetalle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridDetalle.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dgridDetalle.Location = New System.Drawing.Point(316, 193)
        Me.dgridDetalle.MainView = Me.GridView1
        Me.dgridDetalle.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dgridDetalle.MenuManager = Me.RibbonControl
        Me.dgridDetalle.Name = "dgridDetalle"
        Me.dgridDetalle.Size = New System.Drawing.Size(765, 403)
        Me.dgridDetalle.TabIndex = 18
        Me.dgridDetalle.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.DetailHeight = 431
        Me.GridView1.GridControl = Me.dgridDetalle
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.ReadOnly = True
        Me.GridView1.OptionsFind.AlwaysVisible = True
        Me.GridView1.OptionsView.ColumnAutoWidth = False
        '
        'frmStockResJornadaMerca
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1081, 626)
        Me.Controls.Add(Me.dgridDetalle)
        Me.Controls.Add(Me.grpFechas)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frmStockResJornadaMerca"
        Me.Ribbon = Me.RibbonControl
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Valorización por mercaderia"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpFechas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpFechas.ResumeLayout(False)
        Me.grpFechas.PerformLayout()
        CType(Me.deMesReporte.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deMesReporte.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbRegimen.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgridDetalle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents grpFechas As DevExpress.XtraEditors.GroupControl
    Friend WithEvents deMesReporte As DevExpress.XtraEditors.DateEdit
    Friend WithEvents cmbRegimen As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label2 As Label
    Friend WithEvents lblPrg As Label
    Friend WithEvents lblDel As Label
    Friend WithEvents dgridDetalle As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents btnGenerar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnImport As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnSalir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lblRegs As DevExpress.XtraBars.BarButtonItem
End Class
