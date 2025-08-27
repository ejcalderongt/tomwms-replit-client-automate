<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmStockResJornada
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmStockResJornada))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.btnGenerar = New DevExpress.XtraBars.BarButtonItem()
        Me.btnImport = New DevExpress.XtraBars.BarButtonItem()
        Me.btnExit = New DevExpress.XtraBars.BarButtonItem()
        Me.lblRegs = New DevExpress.XtraBars.BarStaticItem()
        Me.BarSubItem1 = New DevExpress.XtraBars.BarSubItem()
        Me.mnuDetallePorBodega = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuResumenPorBodega = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.grpFechas = New DevExpress.XtraEditors.GroupControl()
        Me.deMesReporte = New DevExpress.XtraEditors.DateEdit()
        Me.txtRedondeo = New System.Windows.Forms.NumericUpDown()
        Me.txtDecreto = New System.Windows.Forms.NumericUpDown()
        Me.txtEmision = New System.Windows.Forms.NumericUpDown()
        Me.txtPrima = New System.Windows.Forms.NumericUpDown()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbRegimen = New DevExpress.XtraEditors.LookUpEdit()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblPrg = New System.Windows.Forms.Label()
        Me.lblDel = New System.Windows.Forms.Label()
        Me.dgridDetallePorBodega = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.XtraTabControl1 = New DevExpress.XtraTab.XtraTabControl()
        Me.tabDetallePorBodega = New DevExpress.XtraTab.XtraTabPage()
        Me.tabResumenPorBodega = New DevExpress.XtraTab.XtraTabPage()
        Me.dgridResumenPorBodega = New DevExpress.XtraGrid.GridControl()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpFechas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpFechas.SuspendLayout()
        CType(Me.deMesReporte.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deMesReporte.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRedondeo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDecreto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEmision, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPrima, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbRegimen.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgridDetallePorBodega, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabControl1.SuspendLayout()
        Me.tabDetallePorBodega.SuspendLayout()
        Me.tabResumenPorBodega.SuspendLayout()
        CType(Me.dgridResumenPorBodega, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.RibbonControl.SearchEditItem, Me.btnGenerar, Me.btnImport, Me.btnExit, Me.lblRegs, Me.BarSubItem1, Me.mnuDetallePorBodega, Me.mnuResumenPorBodega})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 9
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1139, 193)
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
        Me.btnImport.Id = 3
        Me.btnImport.ImageOptions.SvgImage = CType(resources.GetObject("btnImport.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnImport.Name = "btnImport"
        '
        'btnExit
        '
        Me.btnExit.Caption = "Salir"
        Me.btnExit.Id = 4
        Me.btnExit.ImageOptions.SvgImage = CType(resources.GetObject("btnExit.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnExit.Name = "btnExit"
        '
        'lblRegs
        '
        Me.lblRegs.Caption = "Registros: 0"
        Me.lblRegs.Id = 5
        Me.lblRegs.Name = "lblRegs"
        '
        'BarSubItem1
        '
        Me.BarSubItem1.Caption = "Imprimir"
        Me.BarSubItem1.Id = 6
        Me.BarSubItem1.ImageOptions.SvgImage = CType(resources.GetObject("BarSubItem1.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.BarSubItem1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuDetallePorBodega), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuResumenPorBodega)})
        Me.BarSubItem1.Name = "BarSubItem1"
        '
        'mnuDetallePorBodega
        '
        Me.mnuDetallePorBodega.Caption = "Detalle por bodega"
        Me.mnuDetallePorBodega.Id = 7
        Me.mnuDetallePorBodega.Name = "mnuDetallePorBodega"
        '
        'mnuResumenPorBodega
        '
        Me.mnuResumenPorBodega.Caption = "Resumen por bodega"
        Me.mnuResumenPorBodega.Id = 8
        Me.mnuResumenPorBodega.Name = "mnuResumenPorBodega"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Resumen valorización"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.btnGenerar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.btnImport)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.BarSubItem1)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.btnExit)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblRegs)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 722)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1139, 30)
        '
        'grpFechas
        '
        Me.grpFechas.Controls.Add(Me.deMesReporte)
        Me.grpFechas.Controls.Add(Me.txtRedondeo)
        Me.grpFechas.Controls.Add(Me.txtDecreto)
        Me.grpFechas.Controls.Add(Me.txtEmision)
        Me.grpFechas.Controls.Add(Me.txtPrima)
        Me.grpFechas.Controls.Add(Me.Label5)
        Me.grpFechas.Controls.Add(Me.Label4)
        Me.grpFechas.Controls.Add(Me.Label3)
        Me.grpFechas.Controls.Add(Me.Label1)
        Me.grpFechas.Controls.Add(Me.cmbRegimen)
        Me.grpFechas.Controls.Add(Me.Label2)
        Me.grpFechas.Controls.Add(Me.lblPrg)
        Me.grpFechas.Controls.Add(Me.lblDel)
        Me.grpFechas.Dock = System.Windows.Forms.DockStyle.Left
        Me.grpFechas.Location = New System.Drawing.Point(0, 193)
        Me.grpFechas.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grpFechas.Name = "grpFechas"
        Me.grpFechas.Size = New System.Drawing.Size(316, 529)
        Me.grpFechas.TabIndex = 9
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
        'txtRedondeo
        '
        Me.txtRedondeo.Location = New System.Drawing.Point(186, 306)
        Me.txtRedondeo.Maximum = New Decimal(New Integer() {8, 0, 0, 0})
        Me.txtRedondeo.Minimum = New Decimal(New Integer() {2, 0, 0, 0})
        Me.txtRedondeo.Name = "txtRedondeo"
        Me.txtRedondeo.Size = New System.Drawing.Size(106, 23)
        Me.txtRedondeo.TabIndex = 46
        Me.txtRedondeo.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'txtDecreto
        '
        Me.txtDecreto.DecimalPlaces = 8
        Me.txtDecreto.Location = New System.Drawing.Point(106, 259)
        Me.txtDecreto.Name = "txtDecreto"
        Me.txtDecreto.Size = New System.Drawing.Size(186, 23)
        Me.txtDecreto.TabIndex = 45
        Me.txtDecreto.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'txtEmision
        '
        Me.txtEmision.DecimalPlaces = 8
        Me.txtEmision.Location = New System.Drawing.Point(106, 224)
        Me.txtEmision.Name = "txtEmision"
        Me.txtEmision.Size = New System.Drawing.Size(186, 23)
        Me.txtEmision.TabIndex = 19
        Me.txtEmision.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'txtPrima
        '
        Me.txtPrima.DecimalPlaces = 8
        Me.txtPrima.Location = New System.Drawing.Point(106, 190)
        Me.txtPrima.Name = "txtPrima"
        Me.txtPrima.Size = New System.Drawing.Size(186, 23)
        Me.txtPrima.TabIndex = 18
        Me.txtPrima.Value = New Decimal(New Integer() {72603, 0, 0, 524288})
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(13, 308)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(167, 17)
        Me.Label5.TabIndex = 43
        Me.Label5.Text = "Decimales para redondeo:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 261)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(90, 17)
        Me.Label4.TabIndex = 40
        Me.Label4.Text = "Decreto (%):"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 226)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(87, 17)
        Me.Label3.TabIndex = 39
        Me.Label3.Text = "Emisión (%):"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 192)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 17)
        Me.Label1.TabIndex = 38
        Me.Label1.Text = "Prima (%):"
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
        Me.Label2.Size = New System.Drawing.Size(66, 17)
        Me.Label2.TabIndex = 37
        Me.Label2.Text = "Regimen:"
        '
        'lblPrg
        '
        Me.lblPrg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPrg.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblPrg.Location = New System.Drawing.Point(2, 466)
        Me.lblPrg.Name = "lblPrg"
        Me.lblPrg.Size = New System.Drawing.Size(312, 61)
        Me.lblPrg.TabIndex = 7
        '
        'lblDel
        '
        Me.lblDel.AutoSize = True
        Me.lblDel.Location = New System.Drawing.Point(11, 120)
        Me.lblDel.Name = "lblDel"
        Me.lblDel.Size = New System.Drawing.Size(85, 17)
        Me.lblDel.TabIndex = 3
        Me.lblDel.Text = "Mes reporte:"
        '
        'dgridDetallePorBodega
        '
        Me.dgridDetallePorBodega.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridDetallePorBodega.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dgridDetallePorBodega.Location = New System.Drawing.Point(0, 0)
        Me.dgridDetallePorBodega.MainView = Me.GridView1
        Me.dgridDetallePorBodega.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dgridDetallePorBodega.MenuManager = Me.RibbonControl
        Me.dgridDetallePorBodega.Name = "dgridDetallePorBodega"
        Me.dgridDetallePorBodega.Size = New System.Drawing.Size(821, 499)
        Me.dgridDetallePorBodega.TabIndex = 17
        Me.dgridDetallePorBodega.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.DetailHeight = 431
        Me.GridView1.GridControl = Me.dgridDetallePorBodega
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.ReadOnly = True
        Me.GridView1.OptionsFind.AlwaysVisible = True
        Me.GridView1.OptionsView.ColumnAutoWidth = False
        '
        'XtraTabControl1
        '
        Me.XtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XtraTabControl1.Location = New System.Drawing.Point(316, 193)
        Me.XtraTabControl1.Name = "XtraTabControl1"
        Me.XtraTabControl1.SelectedTabPage = Me.tabDetallePorBodega
        Me.XtraTabControl1.Size = New System.Drawing.Size(823, 529)
        Me.XtraTabControl1.TabIndex = 23
        Me.XtraTabControl1.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tabDetallePorBodega, Me.tabResumenPorBodega})
        '
        'tabDetallePorBodega
        '
        Me.tabDetallePorBodega.Controls.Add(Me.dgridDetallePorBodega)
        Me.tabDetallePorBodega.Name = "tabDetallePorBodega"
        Me.tabDetallePorBodega.Size = New System.Drawing.Size(821, 499)
        Me.tabDetallePorBodega.Text = "Detalle por bodega"
        '
        'tabResumenPorBodega
        '
        Me.tabResumenPorBodega.Controls.Add(Me.dgridResumenPorBodega)
        Me.tabResumenPorBodega.Name = "tabResumenPorBodega"
        Me.tabResumenPorBodega.Size = New System.Drawing.Size(821, 499)
        Me.tabResumenPorBodega.Text = "Resumen por bodega"
        '
        'dgridResumenPorBodega
        '
        Me.dgridResumenPorBodega.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridResumenPorBodega.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dgridResumenPorBodega.Location = New System.Drawing.Point(0, 0)
        Me.dgridResumenPorBodega.MainView = Me.GridView2
        Me.dgridResumenPorBodega.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dgridResumenPorBodega.MenuManager = Me.RibbonControl
        Me.dgridResumenPorBodega.Name = "dgridResumenPorBodega"
        Me.dgridResumenPorBodega.Size = New System.Drawing.Size(821, 499)
        Me.dgridResumenPorBodega.TabIndex = 18
        Me.dgridResumenPorBodega.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView2})
        '
        'GridView2
        '
        Me.GridView2.DetailHeight = 431
        Me.GridView2.GridControl = Me.dgridResumenPorBodega
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsBehavior.ReadOnly = True
        Me.GridView2.OptionsFind.AlwaysVisible = True
        Me.GridView2.OptionsView.ColumnAutoWidth = False
        '
        'frmStockResJornada
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1139, 752)
        Me.Controls.Add(Me.XtraTabControl1)
        Me.Controls.Add(Me.grpFechas)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frmStockResJornada"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Resumen de Valorizacion"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpFechas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpFechas.ResumeLayout(False)
        Me.grpFechas.PerformLayout()
        CType(Me.deMesReporte.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deMesReporte.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRedondeo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDecreto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEmision, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPrima, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbRegimen.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgridDetallePorBodega, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabControl1.ResumeLayout(False)
        Me.tabDetallePorBodega.ResumeLayout(False)
        Me.tabResumenPorBodega.ResumeLayout(False)
        CType(Me.dgridResumenPorBodega, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents grpFechas As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cmbRegimen As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label2 As Label
    Friend WithEvents lblPrg As Label
    Friend WithEvents lblDel As Label
    Friend WithEvents dgridDetallePorBodega As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents btnGenerar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnImport As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnExit As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lblRegs As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents XtraTabControl1 As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents tabDetallePorBodega As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tabResumenPorBodega As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents dgridResumenPorBodega As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents Label5 As Label
    Friend WithEvents txtRedondeo As NumericUpDown
    Friend WithEvents txtDecreto As NumericUpDown
    Friend WithEvents txtEmision As NumericUpDown
    Friend WithEvents txtPrima As NumericUpDown
    Friend WithEvents BarSubItem1 As DevExpress.XtraBars.BarSubItem
    Friend WithEvents mnuDetallePorBodega As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuResumenPorBodega As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents deMesReporte As DevExpress.XtraEditors.DateEdit
End Class
