<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReconteo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReconteo))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.cmdActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.grpReconteo = New DevExpress.XtraEditors.GroupControl()
        Me.dtFecha_Vence = New DevExpress.XtraEditors.DateEdit()
        Me.cmbPresentacion = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbEstadoProd = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtPeso = New System.Windows.Forms.NumericUpDown()
        Me.txtCantidad = New System.Windows.Forms.NumericUpDown()
        Me.txtLote = New DevExpress.XtraEditors.TextEdit()
        Me.txtUbicacion = New DevExpress.XtraEditors.TextEdit()
        Me.txtProducto = New DevExpress.XtraEditors.TextEdit()
        Me.lblPeso = New System.Windows.Forms.Label()
        Me.lblCantidad = New System.Windows.Forms.Label()
        Me.lblLote = New System.Windows.Forms.Label()
        Me.lblFechaVence = New System.Windows.Forms.Label()
        Me.lblUbic = New System.Windows.Forms.Label()
        Me.lblPresentacion = New System.Windows.Forms.Label()
        Me.lblEstadoProd = New System.Windows.Forms.Label()
        Me.lblProducto = New System.Windows.Forms.Label()
        Me.lblIdStock = New System.Windows.Forms.Label()
        Me.lblStock = New System.Windows.Forms.Label()
        Me.lblcodenc = New System.Windows.Forms.Label()
        Me.lblEnc = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpReconteo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpReconteo.SuspendLayout()
        CType(Me.dtFecha_Vence.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtFecha_Vence.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPresentacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbEstadoProd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPeso, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCantidad, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUbicacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtProducto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.cmdActualizar, Me.RibbonControl.SearchEditItem})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 2
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1002, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'cmdActualizar
        '
        Me.cmdActualizar.Caption = "Actualizar"
        Me.cmdActualizar.Id = 1
        Me.cmdActualizar.ImageOptions.SvgImage = CType(resources.GetObject("cmdActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdActualizar.Name = "cmdActualizar"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "ReConteo"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdActualizar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 759)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1002, 30)
        '
        'grpReconteo
        '
        Me.grpReconteo.Controls.Add(Me.dtFecha_Vence)
        Me.grpReconteo.Controls.Add(Me.cmbPresentacion)
        Me.grpReconteo.Controls.Add(Me.cmbEstadoProd)
        Me.grpReconteo.Controls.Add(Me.txtPeso)
        Me.grpReconteo.Controls.Add(Me.txtCantidad)
        Me.grpReconteo.Controls.Add(Me.txtLote)
        Me.grpReconteo.Controls.Add(Me.txtUbicacion)
        Me.grpReconteo.Controls.Add(Me.txtProducto)
        Me.grpReconteo.Controls.Add(Me.lblPeso)
        Me.grpReconteo.Controls.Add(Me.lblCantidad)
        Me.grpReconteo.Controls.Add(Me.lblLote)
        Me.grpReconteo.Controls.Add(Me.lblFechaVence)
        Me.grpReconteo.Controls.Add(Me.lblUbic)
        Me.grpReconteo.Controls.Add(Me.lblPresentacion)
        Me.grpReconteo.Controls.Add(Me.lblEstadoProd)
        Me.grpReconteo.Controls.Add(Me.lblProducto)
        Me.grpReconteo.Controls.Add(Me.lblIdStock)
        Me.grpReconteo.Controls.Add(Me.lblStock)
        Me.grpReconteo.Controls.Add(Me.lblcodenc)
        Me.grpReconteo.Controls.Add(Me.lblEnc)
        Me.grpReconteo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpReconteo.Location = New System.Drawing.Point(0, 193)
        Me.grpReconteo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grpReconteo.Name = "grpReconteo"
        Me.grpReconteo.Size = New System.Drawing.Size(1002, 566)
        Me.grpReconteo.TabIndex = 2
        '
        'dtFecha_Vence
        '
        Me.dtFecha_Vence.EditValue = New Date(2018, 3, 19, 15, 57, 53, 0)
        Me.dtFecha_Vence.Location = New System.Drawing.Point(204, 353)
        Me.dtFecha_Vence.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtFecha_Vence.MenuManager = Me.RibbonControl
        Me.dtFecha_Vence.Name = "dtFecha_Vence"
        Me.dtFecha_Vence.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtFecha_Vence.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtFecha_Vence.Size = New System.Drawing.Size(192, 22)
        Me.dtFecha_Vence.TabIndex = 20
        '
        'cmbPresentacion
        '
        Me.cmbPresentacion.Location = New System.Drawing.Point(204, 263)
        Me.cmbPresentacion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbPresentacion.MenuManager = Me.RibbonControl
        Me.cmbPresentacion.Name = "cmbPresentacion"
        Me.cmbPresentacion.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPresentacion.Properties.NullText = ""
        Me.cmbPresentacion.Size = New System.Drawing.Size(192, 22)
        Me.cmbPresentacion.TabIndex = 18
        '
        'cmbEstadoProd
        '
        Me.cmbEstadoProd.Location = New System.Drawing.Point(204, 214)
        Me.cmbEstadoProd.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbEstadoProd.MenuManager = Me.RibbonControl
        Me.cmbEstadoProd.Name = "cmbEstadoProd"
        Me.cmbEstadoProd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbEstadoProd.Properties.NullText = ""
        Me.cmbEstadoProd.Size = New System.Drawing.Size(192, 22)
        Me.cmbEstadoProd.TabIndex = 17
        '
        'txtPeso
        '
        Me.txtPeso.DecimalPlaces = 2
        Me.txtPeso.Location = New System.Drawing.Point(204, 490)
        Me.txtPeso.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtPeso.Maximum = New Decimal(New Integer() {10000000, 0, 0, 0})
        Me.txtPeso.Name = "txtPeso"
        Me.txtPeso.Size = New System.Drawing.Size(140, 23)
        Me.txtPeso.TabIndex = 16
        '
        'txtCantidad
        '
        Me.txtCantidad.DecimalPlaces = 2
        Me.txtCantidad.Location = New System.Drawing.Point(204, 444)
        Me.txtCantidad.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCantidad.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.Size = New System.Drawing.Size(140, 23)
        Me.txtCantidad.TabIndex = 15
        '
        'txtLote
        '
        Me.txtLote.Location = New System.Drawing.Point(204, 399)
        Me.txtLote.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtLote.MenuManager = Me.RibbonControl
        Me.txtLote.Name = "txtLote"
        Me.txtLote.Size = New System.Drawing.Size(192, 22)
        Me.txtLote.TabIndex = 14
        '
        'txtUbicacion
        '
        Me.txtUbicacion.Location = New System.Drawing.Point(204, 308)
        Me.txtUbicacion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtUbicacion.MenuManager = Me.RibbonControl
        Me.txtUbicacion.Name = "txtUbicacion"
        Me.txtUbicacion.Properties.ReadOnly = True
        Me.txtUbicacion.Size = New System.Drawing.Size(285, 22)
        Me.txtUbicacion.TabIndex = 13
        '
        'txtProducto
        '
        Me.txtProducto.Location = New System.Drawing.Point(204, 162)
        Me.txtProducto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtProducto.MenuManager = Me.RibbonControl
        Me.txtProducto.Name = "txtProducto"
        Me.txtProducto.Properties.ReadOnly = True
        Me.txtProducto.Size = New System.Drawing.Size(285, 22)
        Me.txtProducto.TabIndex = 12
        '
        'lblPeso
        '
        Me.lblPeso.AutoSize = True
        Me.lblPeso.Location = New System.Drawing.Point(81, 492)
        Me.lblPeso.Name = "lblPeso"
        Me.lblPeso.Size = New System.Drawing.Size(42, 17)
        Me.lblPeso.TabIndex = 11
        Me.lblPeso.Text = "Peso:"
        '
        'lblCantidad
        '
        Me.lblCantidad.AutoSize = True
        Me.lblCantidad.Location = New System.Drawing.Point(81, 447)
        Me.lblCantidad.Name = "lblCantidad"
        Me.lblCantidad.Size = New System.Drawing.Size(67, 17)
        Me.lblCantidad.TabIndex = 10
        Me.lblCantidad.Text = "Cantidad:"
        '
        'lblLote
        '
        Me.lblLote.AutoSize = True
        Me.lblLote.Location = New System.Drawing.Point(81, 402)
        Me.lblLote.Name = "lblLote"
        Me.lblLote.Size = New System.Drawing.Size(40, 17)
        Me.lblLote.TabIndex = 9
        Me.lblLote.Text = "Lote:"
        '
        'lblFechaVence
        '
        Me.lblFechaVence.AutoSize = True
        Me.lblFechaVence.Location = New System.Drawing.Point(81, 362)
        Me.lblFechaVence.Name = "lblFechaVence"
        Me.lblFechaVence.Size = New System.Drawing.Size(90, 17)
        Me.lblFechaVence.TabIndex = 8
        Me.lblFechaVence.Text = "Fecha Vence:"
        '
        'lblUbic
        '
        Me.lblUbic.AutoSize = True
        Me.lblUbic.Location = New System.Drawing.Point(81, 316)
        Me.lblUbic.Name = "lblUbic"
        Me.lblUbic.Size = New System.Drawing.Size(71, 17)
        Me.lblUbic.TabIndex = 7
        Me.lblUbic.Text = "Ubicación:"
        '
        'lblPresentacion
        '
        Me.lblPresentacion.AutoSize = True
        Me.lblPresentacion.Location = New System.Drawing.Point(81, 267)
        Me.lblPresentacion.Name = "lblPresentacion"
        Me.lblPresentacion.Size = New System.Drawing.Size(91, 17)
        Me.lblPresentacion.TabIndex = 6
        Me.lblPresentacion.Text = "Presentación:"
        '
        'lblEstadoProd
        '
        Me.lblEstadoProd.AutoSize = True
        Me.lblEstadoProd.Location = New System.Drawing.Point(81, 218)
        Me.lblEstadoProd.Name = "lblEstadoProd"
        Me.lblEstadoProd.Size = New System.Drawing.Size(116, 17)
        Me.lblEstadoProd.TabIndex = 5
        Me.lblEstadoProd.Text = "Estado Producto:"
        '
        'lblProducto
        '
        Me.lblProducto.AutoSize = True
        Me.lblProducto.Location = New System.Drawing.Point(81, 166)
        Me.lblProducto.Name = "lblProducto"
        Me.lblProducto.Size = New System.Drawing.Size(70, 17)
        Me.lblProducto.TabIndex = 4
        Me.lblProducto.Text = "Producto:"
        '
        'lblIdStock
        '
        Me.lblIdStock.AutoSize = True
        Me.lblIdStock.Location = New System.Drawing.Point(201, 118)
        Me.lblIdStock.Name = "lblIdStock"
        Me.lblIdStock.Size = New System.Drawing.Size(33, 17)
        Me.lblIdStock.TabIndex = 3
        Me.lblIdStock.Text = "-----"
        '
        'lblStock
        '
        Me.lblStock.AutoSize = True
        Me.lblStock.Location = New System.Drawing.Point(81, 118)
        Me.lblStock.Name = "lblStock"
        Me.lblStock.Size = New System.Drawing.Size(64, 17)
        Me.lblStock.TabIndex = 2
        Me.lblStock.Text = "Id Stock:"
        '
        'lblcodenc
        '
        Me.lblcodenc.AutoSize = True
        Me.lblcodenc.Location = New System.Drawing.Point(201, 71)
        Me.lblcodenc.Name = "lblcodenc"
        Me.lblcodenc.Size = New System.Drawing.Size(33, 17)
        Me.lblcodenc.TabIndex = 1
        Me.lblcodenc.Text = "-----"
        '
        'lblEnc
        '
        Me.lblEnc.AutoSize = True
        Me.lblEnc.Location = New System.Drawing.Point(81, 71)
        Me.lblEnc.Name = "lblEnc"
        Me.lblEnc.Size = New System.Drawing.Size(103, 17)
        Me.lblEnc.TabIndex = 0
        Me.lblEnc.Text = "Id Encabezado:"
        '
        'frmReconteo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1002, 789)
        Me.Controls.Add(Me.grpReconteo)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmReconteo"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "ReConteo"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpReconteo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpReconteo.ResumeLayout(False)
        Me.grpReconteo.PerformLayout()
        CType(Me.dtFecha_Vence.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtFecha_Vence.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPresentacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbEstadoProd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPeso, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCantidad, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUbicacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtProducto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents cmdActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents grpReconteo As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblIdStock As Label
    Friend WithEvents lblStock As Label
    Friend WithEvents lblcodenc As Label
    Friend WithEvents lblEnc As Label
    Friend WithEvents lblProducto As Label
    Friend WithEvents lblPresentacion As Label
    Friend WithEvents lblEstadoProd As Label
    Friend WithEvents lblFechaVence As Label
    Friend WithEvents lblUbic As Label
    Friend WithEvents lblLote As Label
    Friend WithEvents lblPeso As Label
    Friend WithEvents lblCantidad As Label
    Friend WithEvents txtPeso As NumericUpDown
    Friend WithEvents txtCantidad As NumericUpDown
    Friend WithEvents txtLote As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtUbicacion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtProducto As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cmbPresentacion As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbEstadoProd As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents dtFecha_Vence As DevExpress.XtraEditors.DateEdit
End Class
