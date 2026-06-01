<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRegularizarInventarioRFID
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRegularizarInventarioRFID))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.cmdRegularizar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.grpInfo = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbBodega = New DevExpress.XtraEditors.LookUpEdit()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.rdStockInventarioMovs = New System.Windows.Forms.RadioButton()
        Me.lblinfohr = New System.Windows.Forms.Label()
        Me.rdStockInventario = New System.Windows.Forms.RadioButton()
        Me.dtHora = New DevExpress.XtraEditors.DateEdit()
        Me.dtFecha = New DevExpress.XtraEditors.DateEdit()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.prg = New System.Windows.Forms.ProgressBar()
        Me.tabRegu = New DevExpress.XtraTab.XtraTabControl()
        Me.tabAjustes = New DevExpress.XtraTab.XtraTabPage()
        Me.grdRegularizar = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tabMov = New DevExpress.XtraTab.XtraTabPage()
        Me.dgridMovimientos = New DevExpress.XtraGrid.GridControl()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tabIdStockConReserva = New DevExpress.XtraTab.XtraTabPage()
        Me.grdInventarioConReserva = New DevExpress.XtraGrid.GridControl()
        Me.grdvInventarioConReserva = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tabRegularizacion = New DevExpress.XtraTab.XtraTabPage()
        Me.dgridRegularizado = New DevExpress.XtraGrid.GridControl()
        Me.GridViewRegularizado = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.lblPrg = New DevExpress.XtraBars.BarButtonItem()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpInfo.SuspendLayout()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtHora.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtHora.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtFecha.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtFecha.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tabRegu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabRegu.SuspendLayout()
        Me.tabAjustes.SuspendLayout()
        CType(Me.grdRegularizar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabMov.SuspendLayout()
        CType(Me.dgridMovimientos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabIdStockConReserva.SuspendLayout()
        CType(Me.grdInventarioConReserva, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdvInventarioConReserva, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabRegularizacion.SuspendLayout()
        CType(Me.dgridRegularizado, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewRegularizado, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.cmdRegularizar, Me.lblPrg})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 3
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.Size = New System.Drawing.Size(1542, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'cmdRegularizar
        '
        Me.cmdRegularizar.Caption = "Actualizar Stock"
        Me.cmdRegularizar.Id = 1
        Me.cmdRegularizar.ImageOptions.SvgImage = CType(resources.GetObject("BarButtonItem1.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdRegularizar.Name = "cmdRegularizar"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Regularización"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdRegularizar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblPrg)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 690)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1542, 30)
        '
        'grpInfo
        '
        Me.grpInfo.Controls.Add(Me.Label4)
        Me.grpInfo.Controls.Add(Me.cmbBodega)
        Me.grpInfo.Controls.Add(Me.Label3)
        Me.grpInfo.Controls.Add(Me.Label2)
        Me.grpInfo.Controls.Add(Me.rdStockInventarioMovs)
        Me.grpInfo.Controls.Add(Me.lblinfohr)
        Me.grpInfo.Controls.Add(Me.rdStockInventario)
        Me.grpInfo.Controls.Add(Me.dtHora)
        Me.grpInfo.Controls.Add(Me.dtFecha)
        Me.grpInfo.Controls.Add(Me.Label1)
        Me.grpInfo.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpInfo.Location = New System.Drawing.Point(0, 193)
        Me.grpInfo.Margin = New System.Windows.Forms.Padding(4)
        Me.grpInfo.Name = "grpInfo"
        Me.grpInfo.Padding = New System.Windows.Forms.Padding(4)
        Me.grpInfo.Size = New System.Drawing.Size(1542, 102)
        Me.grpInfo.TabIndex = 2
        Me.grpInfo.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(1091, 33)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(255, 16)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "[Este proceso puede tardar varios minutos]"
        '
        'cmbBodega
        '
        Me.cmbBodega.Location = New System.Drawing.Point(510, 25)
        Me.cmbBodega.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbBodega.MenuManager = Me.RibbonControl
        Me.cmbBodega.Name = "cmbBodega"
        Me.cmbBodega.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodega.Properties.NullText = ""
        Me.cmbBodega.Properties.ReadOnly = True
        Me.cmbBodega.Size = New System.Drawing.Size(186, 22)
        Me.cmbBodega.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(1091, 65)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(448, 16)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "[Si hay movimientos posteriores NO se garantiza la reconstrucción del Stock]"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(476, 28)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 16)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "en:"
        '
        'rdStockInventarioMovs
        '
        Me.rdStockInventarioMovs.AutoSize = True
        Me.rdStockInventarioMovs.Checked = True
        Me.rdStockInventarioMovs.Location = New System.Drawing.Point(719, 28)
        Me.rdStockInventarioMovs.Margin = New System.Windows.Forms.Padding(4)
        Me.rdStockInventarioMovs.Name = "rdStockInventarioMovs"
        Me.rdStockInventarioMovs.Size = New System.Drawing.Size(329, 20)
        Me.rdStockInventarioMovs.TabIndex = 6
        Me.rdStockInventarioMovs.TabStop = True
        Me.rdStockInventarioMovs.Text = "Stock = Inventario + (entradas - salidas) posteriores"
        Me.rdStockInventarioMovs.UseVisualStyleBackColor = True
        '
        'lblinfohr
        '
        Me.lblinfohr.AutoSize = True
        Me.lblinfohr.Location = New System.Drawing.Point(292, 28)
        Me.lblinfohr.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblinfohr.Name = "lblinfohr"
        Me.lblinfohr.Size = New System.Drawing.Size(39, 16)
        Me.lblinfohr.TabIndex = 2
        Me.lblinfohr.Text = "a las:"
        '
        'rdStockInventario
        '
        Me.rdStockInventario.AutoSize = True
        Me.rdStockInventario.Location = New System.Drawing.Point(719, 63)
        Me.rdStockInventario.Margin = New System.Windows.Forms.Padding(4)
        Me.rdStockInventario.Name = "rdStockInventario"
        Me.rdStockInventario.Size = New System.Drawing.Size(133, 20)
        Me.rdStockInventario.TabIndex = 8
        Me.rdStockInventario.Text = "Stock = Inventario"
        Me.rdStockInventario.UseVisualStyleBackColor = True
        '
        'dtHora
        '
        Me.dtHora.EditValue = New Date(2018, 3, 20, 10, 20, 46, 0)
        Me.dtHora.Location = New System.Drawing.Point(349, 25)
        Me.dtHora.Margin = New System.Windows.Forms.Padding(4)
        Me.dtHora.MenuManager = Me.RibbonControl
        Me.dtHora.Name = "dtHora"
        Me.dtHora.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtHora.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtHora.Properties.DisplayFormat.FormatString = "t"
        Me.dtHora.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtHora.Properties.EditFormat.FormatString = "t"
        Me.dtHora.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtHora.Properties.MaskSettings.Set("mask", "t")
        Me.dtHora.Properties.ReadOnly = True
        Me.dtHora.Size = New System.Drawing.Size(108, 22)
        Me.dtHora.TabIndex = 3
        '
        'dtFecha
        '
        Me.dtFecha.EditValue = New Date(2018, 3, 20, 10, 18, 39, 0)
        Me.dtFecha.Location = New System.Drawing.Point(118, 25)
        Me.dtFecha.Margin = New System.Windows.Forms.Padding(4)
        Me.dtFecha.MenuManager = Me.RibbonControl
        Me.dtFecha.Name = "dtFecha"
        Me.dtFecha.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtFecha.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtFecha.Properties.ReadOnly = True
        Me.dtFecha.Size = New System.Drawing.Size(155, 22)
        Me.dtFecha.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(19, 28)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Inventario a:"
        '
        'prg
        '
        Me.prg.Dock = System.Windows.Forms.DockStyle.Top
        Me.prg.Location = New System.Drawing.Point(0, 295)
        Me.prg.Margin = New System.Windows.Forms.Padding(4)
        Me.prg.Name = "prg"
        Me.prg.Size = New System.Drawing.Size(1542, 28)
        Me.prg.TabIndex = 3
        Me.prg.Visible = False
        '
        'tabRegu
        '
        Me.tabRegu.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabRegu.Location = New System.Drawing.Point(0, 323)
        Me.tabRegu.Margin = New System.Windows.Forms.Padding(4)
        Me.tabRegu.Name = "tabRegu"
        Me.tabRegu.SelectedTabPage = Me.tabAjustes
        Me.tabRegu.Size = New System.Drawing.Size(1542, 367)
        Me.tabRegu.TabIndex = 6
        Me.tabRegu.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tabAjustes, Me.tabMov, Me.tabIdStockConReserva, Me.tabRegularizacion})
        '
        'tabAjustes
        '
        Me.tabAjustes.Controls.Add(Me.grdRegularizar)
        Me.tabAjustes.Margin = New System.Windows.Forms.Padding(4)
        Me.tabAjustes.Name = "tabAjustes"
        Me.tabAjustes.Size = New System.Drawing.Size(1540, 337)
        Me.tabAjustes.Text = "Ajustes"
        '
        'grdRegularizar
        '
        Me.grdRegularizar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdRegularizar.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.grdRegularizar.Location = New System.Drawing.Point(0, 0)
        Me.grdRegularizar.MainView = Me.GridView1
        Me.grdRegularizar.Margin = New System.Windows.Forms.Padding(4)
        Me.grdRegularizar.MenuManager = Me.RibbonControl
        Me.grdRegularizar.Name = "grdRegularizar"
        Me.grdRegularizar.Size = New System.Drawing.Size(1540, 337)
        Me.grdRegularizar.TabIndex = 2
        Me.grdRegularizar.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.DetailHeight = 431
        Me.GridView1.GridControl = Me.grdRegularizar
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.ReadOnly = True
        Me.GridView1.OptionsFind.AlwaysVisible = True
        Me.GridView1.OptionsView.ColumnAutoWidth = False
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'tabMov
        '
        Me.tabMov.Controls.Add(Me.dgridMovimientos)
        Me.tabMov.Margin = New System.Windows.Forms.Padding(4)
        Me.tabMov.Name = "tabMov"
        Me.tabMov.Size = New System.Drawing.Size(1540, 337)
        Me.tabMov.Text = "Movimientos"
        '
        'dgridMovimientos
        '
        Me.dgridMovimientos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridMovimientos.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.dgridMovimientos.Location = New System.Drawing.Point(0, 0)
        Me.dgridMovimientos.MainView = Me.GridView2
        Me.dgridMovimientos.Margin = New System.Windows.Forms.Padding(4)
        Me.dgridMovimientos.MenuManager = Me.RibbonControl
        Me.dgridMovimientos.Name = "dgridMovimientos"
        Me.dgridMovimientos.Size = New System.Drawing.Size(1540, 337)
        Me.dgridMovimientos.TabIndex = 0
        Me.dgridMovimientos.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView2})
        '
        'GridView2
        '
        Me.GridView2.DetailHeight = 431
        Me.GridView2.GridControl = Me.dgridMovimientos
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsEditForm.PopupEditFormWidth = 933
        '
        'tabIdStockConReserva
        '
        Me.tabIdStockConReserva.Controls.Add(Me.grdInventarioConReserva)
        Me.tabIdStockConReserva.Margin = New System.Windows.Forms.Padding(4)
        Me.tabIdStockConReserva.Name = "tabIdStockConReserva"
        Me.tabIdStockConReserva.Size = New System.Drawing.Size(1540, 337)
        Me.tabIdStockConReserva.Text = "Inventario con reserva"
        '
        'grdInventarioConReserva
        '
        Me.grdInventarioConReserva.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdInventarioConReserva.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.grdInventarioConReserva.Location = New System.Drawing.Point(0, 0)
        Me.grdInventarioConReserva.MainView = Me.grdvInventarioConReserva
        Me.grdInventarioConReserva.Margin = New System.Windows.Forms.Padding(4)
        Me.grdInventarioConReserva.MenuManager = Me.RibbonControl
        Me.grdInventarioConReserva.Name = "grdInventarioConReserva"
        Me.grdInventarioConReserva.Size = New System.Drawing.Size(1540, 337)
        Me.grdInventarioConReserva.TabIndex = 0
        Me.grdInventarioConReserva.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdvInventarioConReserva})
        '
        'grdvInventarioConReserva
        '
        Me.grdvInventarioConReserva.DetailHeight = 431
        Me.grdvInventarioConReserva.GridControl = Me.grdInventarioConReserva
        Me.grdvInventarioConReserva.Name = "grdvInventarioConReserva"
        Me.grdvInventarioConReserva.OptionsEditForm.PopupEditFormWidth = 933
        Me.grdvInventarioConReserva.OptionsView.ColumnAutoWidth = False
        '
        'tabRegularizacion
        '
        Me.tabRegularizacion.Controls.Add(Me.dgridRegularizado)
        Me.tabRegularizacion.Name = "tabRegularizacion"
        Me.tabRegularizacion.Size = New System.Drawing.Size(1540, 337)
        Me.tabRegularizacion.Text = "Regularización"
        '
        'dgridRegularizado
        '
        Me.dgridRegularizado.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridRegularizado.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.dgridRegularizado.Location = New System.Drawing.Point(0, 0)
        Me.dgridRegularizado.MainView = Me.GridViewRegularizado
        Me.dgridRegularizado.Margin = New System.Windows.Forms.Padding(4)
        Me.dgridRegularizado.MenuManager = Me.RibbonControl
        Me.dgridRegularizado.Name = "dgridRegularizado"
        Me.dgridRegularizado.Size = New System.Drawing.Size(1540, 337)
        Me.dgridRegularizado.TabIndex = 1
        Me.dgridRegularizado.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewRegularizado})
        '
        'GridViewRegularizado
        '
        Me.GridViewRegularizado.DetailHeight = 431
        Me.GridViewRegularizado.GridControl = Me.dgridRegularizado
        Me.GridViewRegularizado.Name = "GridViewRegularizado"
        Me.GridViewRegularizado.OptionsBehavior.ReadOnly = True
        Me.GridViewRegularizado.OptionsEditForm.PopupEditFormWidth = 933
        Me.GridViewRegularizado.OptionsView.ColumnAutoWidth = False
        Me.GridViewRegularizado.OptionsView.ShowAutoFilterRow = True
        '
        'lblPrg
        '
        Me.lblPrg.Caption = "Registros: 0"
        Me.lblPrg.Id = 2
        Me.lblPrg.Name = "lblPrg"
        '
        'frmRegularizarInventarioRFID
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1542, 720)
        Me.Controls.Add(Me.tabRegu)
        Me.Controls.Add(Me.prg)
        Me.Controls.Add(Me.grpInfo)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frmRegularizarInventarioRFID"
        Me.Ribbon = Me.RibbonControl
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Regularización Inventario RFID"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpInfo.ResumeLayout(False)
        Me.grpInfo.PerformLayout()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtHora.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtHora.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtFecha.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtFecha.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tabRegu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabRegu.ResumeLayout(False)
        Me.tabAjustes.ResumeLayout(False)
        CType(Me.grdRegularizar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabMov.ResumeLayout(False)
        CType(Me.dgridMovimientos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabIdStockConReserva.ResumeLayout(False)
        CType(Me.grdInventarioConReserva, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdvInventarioConReserva, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabRegularizacion.ResumeLayout(False)
        CType(Me.dgridRegularizado, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewRegularizado, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents cmdRegularizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents grpInfo As GroupBox
    Friend WithEvents Label4 As Label
    Friend WithEvents cmbBodega As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents rdStockInventarioMovs As RadioButton
    Friend WithEvents lblinfohr As Label
    Friend WithEvents rdStockInventario As RadioButton
    Friend WithEvents dtHora As DevExpress.XtraEditors.DateEdit
    Friend WithEvents dtFecha As DevExpress.XtraEditors.DateEdit
    Friend WithEvents Label1 As Label
    Friend WithEvents prg As ProgressBar
    Friend WithEvents tabRegu As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents tabAjustes As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents grdRegularizar As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents tabMov As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents dgridMovimientos As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents tabIdStockConReserva As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents grdInventarioConReserva As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdvInventarioConReserva As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents tabRegularizacion As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents dgridRegularizado As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewRegularizado As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lblPrg As DevExpress.XtraBars.BarButtonItem
End Class
