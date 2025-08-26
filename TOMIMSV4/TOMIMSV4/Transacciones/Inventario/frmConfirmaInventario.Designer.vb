<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfirmaInventario
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConfirmaInventario))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.cmdAceptar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdCancelar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.grpCalculoStock = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.rdNoRecalcularStock = New System.Windows.Forms.RadioButton()
        Me.rdStockInventarioMovs = New System.Windows.Forms.RadioButton()
        Me.rdStockInventario = New System.Windows.Forms.RadioButton()
        Me.grdRecepciones = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.grpInfo = New System.Windows.Forms.GroupBox()
        Me.cmbBodega = New DevExpress.XtraEditors.LookUpEdit()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblinfohr = New System.Windows.Forms.Label()
        Me.dtHora = New DevExpress.XtraEditors.DateEdit()
        Me.dtFecha = New DevExpress.XtraEditors.DateEdit()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCalculoStock.SuspendLayout()
        CType(Me.grdRecepciones, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpInfo.SuspendLayout()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtHora.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtHora.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtFecha.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtFecha.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.cmdAceptar, Me.cmdCancelar, Me.RibbonControl.SearchEditItem})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 3
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(999, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'cmdAceptar
        '
        Me.cmdAceptar.Caption = "Aceptar"
        Me.cmdAceptar.Id = 1
        Me.cmdAceptar.ImageOptions.SvgImage = CType(resources.GetObject("cmdAceptar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdAceptar.Name = "cmdAceptar"
        '
        'cmdCancelar
        '
        Me.cmdCancelar.Caption = "Cancelar"
        Me.cmdCancelar.Id = 2
        Me.cmdCancelar.ImageOptions.SvgImage = CType(resources.GetObject("cmdCancelar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdCancelar.Name = "cmdCancelar"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Confirmación de Inventario"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdAceptar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdCancelar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 913)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(999, 30)
        '
        'grpCalculoStock
        '
        Me.grpCalculoStock.Controls.Add(Me.Label4)
        Me.grpCalculoStock.Controls.Add(Me.Label3)
        Me.grpCalculoStock.Controls.Add(Me.rdNoRecalcularStock)
        Me.grpCalculoStock.Controls.Add(Me.rdStockInventarioMovs)
        Me.grpCalculoStock.Controls.Add(Me.rdStockInventario)
        Me.grpCalculoStock.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.grpCalculoStock.Location = New System.Drawing.Point(0, 674)
        Me.grpCalculoStock.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grpCalculoStock.Name = "grpCalculoStock"
        Me.grpCalculoStock.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grpCalculoStock.Size = New System.Drawing.Size(999, 239)
        Me.grpCalculoStock.TabIndex = 3
        Me.grpCalculoStock.TabStop = False
        Me.grpCalculoStock.Text = "Cálculo Stock"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(524, 117)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(273, 17)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "[Este proceso puede tardar varios minutos]"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(524, 66)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(475, 17)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "[Si hay movimientos posteriores NO se garantiza la reconstrucción del Stock]"
        '
        'rdNoRecalcularStock
        '
        Me.rdNoRecalcularStock.AutoSize = True
        Me.rdNoRecalcularStock.Location = New System.Drawing.Point(38, 170)
        Me.rdNoRecalcularStock.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.rdNoRecalcularStock.Name = "rdNoRecalcularStock"
        Me.rdNoRecalcularStock.Size = New System.Drawing.Size(148, 21)
        Me.rdNoRecalcularStock.TabIndex = 2
        Me.rdNoRecalcularStock.TabStop = True
        Me.rdNoRecalcularStock.Text = "No recalcular stock "
        Me.rdNoRecalcularStock.UseVisualStyleBackColor = True
        '
        'rdStockInventarioMovs
        '
        Me.rdStockInventarioMovs.AutoSize = True
        Me.rdStockInventarioMovs.Location = New System.Drawing.Point(38, 112)
        Me.rdStockInventarioMovs.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.rdStockInventarioMovs.Name = "rdStockInventarioMovs"
        Me.rdStockInventarioMovs.Size = New System.Drawing.Size(347, 21)
        Me.rdStockInventarioMovs.TabIndex = 1
        Me.rdStockInventarioMovs.TabStop = True
        Me.rdStockInventarioMovs.Text = "Stock = Inventario + (entradas - salidas) posteriores"
        Me.rdStockInventarioMovs.UseVisualStyleBackColor = True
        '
        'rdStockInventario
        '
        Me.rdStockInventario.AutoSize = True
        Me.rdStockInventario.Location = New System.Drawing.Point(38, 62)
        Me.rdStockInventario.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.rdStockInventario.Name = "rdStockInventario"
        Me.rdStockInventario.Size = New System.Drawing.Size(144, 21)
        Me.rdStockInventario.TabIndex = 0
        Me.rdStockInventario.TabStop = True
        Me.rdStockInventario.Text = "Stock = Inventario"
        Me.rdStockInventario.UseVisualStyleBackColor = True
        '
        'grdRecepciones
        '
        Me.grdRecepciones.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdRecepciones.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdRecepciones.Location = New System.Drawing.Point(0, 291)
        Me.grdRecepciones.MainView = Me.GridView1
        Me.grdRecepciones.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdRecepciones.MenuManager = Me.RibbonControl
        Me.grdRecepciones.Name = "grdRecepciones"
        Me.grdRecepciones.Size = New System.Drawing.Size(999, 383)
        Me.grdRecepciones.TabIndex = 4
        Me.grdRecepciones.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.DetailHeight = 431
        Me.GridView1.GridControl = Me.grdRecepciones
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.ReadOnly = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'grpInfo
        '
        Me.grpInfo.Controls.Add(Me.cmbBodega)
        Me.grpInfo.Controls.Add(Me.Label2)
        Me.grpInfo.Controls.Add(Me.lblinfohr)
        Me.grpInfo.Controls.Add(Me.dtHora)
        Me.grpInfo.Controls.Add(Me.dtFecha)
        Me.grpInfo.Controls.Add(Me.Label1)
        Me.grpInfo.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpInfo.Location = New System.Drawing.Point(0, 193)
        Me.grpInfo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grpInfo.Name = "grpInfo"
        Me.grpInfo.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grpInfo.Size = New System.Drawing.Size(999, 98)
        Me.grpInfo.TabIndex = 5
        Me.grpInfo.TabStop = False
        '
        'cmbBodega
        '
        Me.cmbBodega.Location = New System.Drawing.Point(542, 38)
        Me.cmbBodega.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbBodega.MenuManager = Me.RibbonControl
        Me.cmbBodega.Name = "cmbBodega"
        Me.cmbBodega.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodega.Properties.NullText = ""
        Me.cmbBodega.Properties.ReadOnly = True
        Me.cmbBodega.Size = New System.Drawing.Size(185, 22)
        Me.cmbBodega.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(509, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(28, 17)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "en:"
        '
        'lblinfohr
        '
        Me.lblinfohr.AutoSize = True
        Me.lblinfohr.Location = New System.Drawing.Point(324, 42)
        Me.lblinfohr.Name = "lblinfohr"
        Me.lblinfohr.Size = New System.Drawing.Size(39, 17)
        Me.lblinfohr.TabIndex = 3
        Me.lblinfohr.Text = "a las:"
        '
        'dtHora
        '
        Me.dtHora.EditValue = New Date(2018, 3, 20, 10, 20, 46, 0)
        Me.dtHora.Location = New System.Drawing.Point(381, 38)
        Me.dtHora.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtHora.MenuManager = Me.RibbonControl
        Me.dtHora.Name = "dtHora"
        Me.dtHora.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtHora.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtHora.Properties.DisplayFormat.FormatString = "t"
        Me.dtHora.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtHora.Properties.EditFormat.FormatString = "t"
        Me.dtHora.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtHora.Properties.Mask.EditMask = "t"
        Me.dtHora.Properties.ReadOnly = True
        Me.dtHora.Size = New System.Drawing.Size(108, 22)
        Me.dtHora.TabIndex = 2
        '
        'dtFecha
        '
        Me.dtFecha.EditValue = New Date(2018, 3, 20, 10, 18, 39, 0)
        Me.dtFecha.Location = New System.Drawing.Point(150, 38)
        Me.dtFecha.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
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
        Me.Label1.Location = New System.Drawing.Point(51, 42)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Inventario a:"
        '
        'frmConfirmaInventario
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(999, 943)
        Me.Controls.Add(Me.grdRecepciones)
        Me.Controls.Add(Me.grpCalculoStock)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.grpInfo)
        Me.Controls.Add(Me.RibbonControl)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmConfirmaInventario"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Confirmación de Inventario"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCalculoStock.ResumeLayout(False)
        Me.grpCalculoStock.PerformLayout()
        CType(Me.grdRecepciones, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpInfo.ResumeLayout(False)
        Me.grpInfo.PerformLayout()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtHora.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtHora.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtFecha.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtFecha.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents cmdAceptar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdCancelar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents grpCalculoStock As GroupBox
    Friend WithEvents grdRecepciones As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grpInfo As GroupBox
    Friend WithEvents dtFecha As DevExpress.XtraEditors.DateEdit
    Friend WithEvents Label1 As Label
    Friend WithEvents cmbBodega As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label2 As Label
    Friend WithEvents lblinfohr As Label
    Friend WithEvents dtHora As DevExpress.XtraEditors.DateEdit
    Friend WithEvents rdNoRecalcularStock As RadioButton
    Friend WithEvents rdStockInventarioMovs As RadioButton
    Friend WithEvents rdStockInventario As RadioButton
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
End Class
