<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDatosProducto
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDatosProducto))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.cmdActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.txtProducto = New DevExpress.XtraEditors.TextEdit()
        Me.txtCodigo = New DevExpress.XtraEditors.TextEdit()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtCantidad = New System.Windows.Forms.NumericUpDown()
        Me.dtFechaVence = New DevExpress.XtraEditors.DateEdit()
        Me.txtLote = New DevExpress.XtraEditors.TextEdit()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblVence = New System.Windows.Forms.Label()
        Me.lblLote = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.txtProducto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodigo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCantidad, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtFechaVence.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtFechaVence.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.RibbonControl.Size = New System.Drawing.Size(805, 193)
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
        Me.RibbonPage1.Text = "Producto"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdActualizar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.txtProducto)
        Me.PanelControl1.Controls.Add(Me.txtCodigo)
        Me.PanelControl1.Controls.Add(Me.Label5)
        Me.PanelControl1.Controls.Add(Me.Label4)
        Me.PanelControl1.Controls.Add(Me.txtCantidad)
        Me.PanelControl1.Controls.Add(Me.dtFechaVence)
        Me.PanelControl1.Controls.Add(Me.txtLote)
        Me.PanelControl1.Controls.Add(Me.Label3)
        Me.PanelControl1.Controls.Add(Me.lblVence)
        Me.PanelControl1.Controls.Add(Me.lblLote)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl1.Location = New System.Drawing.Point(0, 193)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(805, 249)
        Me.PanelControl1.TabIndex = 2
        '
        'txtProducto
        '
        Me.txtProducto.Location = New System.Drawing.Point(294, 68)
        Me.txtProducto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtProducto.MenuManager = Me.RibbonControl
        Me.txtProducto.Name = "txtProducto"
        Me.txtProducto.Properties.ReadOnly = True
        Me.txtProducto.Size = New System.Drawing.Size(227, 22)
        Me.txtProducto.TabIndex = 9
        '
        'txtCodigo
        '
        Me.txtCodigo.Location = New System.Drawing.Point(294, 28)
        Me.txtCodigo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCodigo.MenuManager = Me.RibbonControl
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.Properties.ReadOnly = True
        Me.txtCodigo.Size = New System.Drawing.Size(227, 22)
        Me.txtCodigo.TabIndex = 8
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(124, 71)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(70, 17)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Producto:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(124, 32)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 17)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Código:"
        '
        'txtCantidad
        '
        Me.txtCantidad.DecimalPlaces = 6
        Me.txtCantidad.Location = New System.Drawing.Point(294, 197)
        Me.txtCantidad.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCantidad.Maximum = New Decimal(New Integer() {100000000, 0, 0, 0})
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.Size = New System.Drawing.Size(188, 23)
        Me.txtCantidad.TabIndex = 5
        '
        'dtFechaVence
        '
        Me.dtFechaVence.EditValue = New Date(2018, 6, 22, 10, 6, 35, 0)
        Me.dtFechaVence.Location = New System.Drawing.Point(294, 154)
        Me.dtFechaVence.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtFechaVence.MenuManager = Me.RibbonControl
        Me.dtFechaVence.Name = "dtFechaVence"
        Me.dtFechaVence.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtFechaVence.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtFechaVence.Size = New System.Drawing.Size(227, 22)
        Me.dtFechaVence.TabIndex = 4
        Me.dtFechaVence.Visible = False
        '
        'txtLote
        '
        Me.txtLote.Location = New System.Drawing.Point(294, 113)
        Me.txtLote.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtLote.MenuManager = Me.RibbonControl
        Me.txtLote.Name = "txtLote"
        Me.txtLote.Size = New System.Drawing.Size(227, 22)
        Me.txtLote.TabIndex = 3
        Me.txtLote.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(124, 197)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(67, 17)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Cantidad:"
        '
        'lblVence
        '
        Me.lblVence.AutoSize = True
        Me.lblVence.Location = New System.Drawing.Point(124, 158)
        Me.lblVence.Name = "lblVence"
        Me.lblVence.Size = New System.Drawing.Size(127, 17)
        Me.lblVence.TabIndex = 1
        Me.lblVence.Text = "Fecha Vencimiento:"
        Me.lblVence.Visible = False
        '
        'lblLote
        '
        Me.lblLote.AutoSize = True
        Me.lblLote.Location = New System.Drawing.Point(124, 117)
        Me.lblLote.Name = "lblLote"
        Me.lblLote.Size = New System.Drawing.Size(40, 17)
        Me.lblLote.TabIndex = 0
        Me.lblLote.Text = "Lote:"
        Me.lblLote.Visible = False
        '
        'frmDatosProducto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(805, 442)
        Me.Controls.Add(Me.PanelControl1)
        Me.Controls.Add(Me.RibbonControl)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmDatosProducto"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Producto"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.txtProducto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodigo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCantidad, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtFechaVence.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtFechaVence.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents cmdActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents Label3 As Label
    Friend WithEvents lblVence As Label
    Friend WithEvents lblLote As Label
    Friend WithEvents txtLote As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCantidad As NumericUpDown
    Friend WithEvents dtFechaVence As DevExpress.XtraEditors.DateEdit
    Friend WithEvents txtProducto As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCodigo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
End Class
