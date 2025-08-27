<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUnidad_Medida_Conversion
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUnidad_Medida_Conversion))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.cmdGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.grpUnidadMedidaConversion = New DevExpress.XtraEditors.GroupControl()
        Me.txtFactor = New System.Windows.Forms.NumericUpDown()
        Me.cmbDestino = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbOrigen = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblID = New System.Windows.Forms.Label()
        Me.lblFactor = New System.Windows.Forms.Label()
        Me.lblUnidadDestino = New System.Windows.Forms.Label()
        Me.lblOrigen = New System.Windows.Forms.Label()
        Me.lblCodigo = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpUnidadMedidaConversion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpUnidadMedidaConversion.SuspendLayout()
        CType(Me.txtFactor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbDestino.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbOrigen.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.cmdGuardar, Me.cmdActualizar, Me.cmdEliminar, Me.RibbonControl.SearchEditItem})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 4
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(932, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'cmdGuardar
        '
        Me.cmdGuardar.Caption = "Guardar"
        Me.cmdGuardar.Id = 1
        Me.cmdGuardar.ImageOptions.SvgImage = CType(resources.GetObject("cmdGuardar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdGuardar.Name = "cmdGuardar"
        '
        'cmdActualizar
        '
        Me.cmdActualizar.Caption = "Actualizar"
        Me.cmdActualizar.Id = 2
        Me.cmdActualizar.ImageOptions.SvgImage = CType(resources.GetObject("cmdActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdActualizar.Name = "cmdActualizar"
        '
        'cmdEliminar
        '
        Me.cmdEliminar.Caption = "Eliminar"
        Me.cmdEliminar.Id = 3
        Me.cmdEliminar.ImageOptions.SvgImage = CType(resources.GetObject("cmdEliminar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdEliminar.Name = "cmdEliminar"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Unidad Medida Conversion"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdGuardar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdEliminar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 573)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(932, 30)
        '
        'grpUnidadMedidaConversion
        '
        Me.grpUnidadMedidaConversion.Controls.Add(Me.txtFactor)
        Me.grpUnidadMedidaConversion.Controls.Add(Me.cmbDestino)
        Me.grpUnidadMedidaConversion.Controls.Add(Me.cmbOrigen)
        Me.grpUnidadMedidaConversion.Controls.Add(Me.lblID)
        Me.grpUnidadMedidaConversion.Controls.Add(Me.lblFactor)
        Me.grpUnidadMedidaConversion.Controls.Add(Me.lblUnidadDestino)
        Me.grpUnidadMedidaConversion.Controls.Add(Me.lblOrigen)
        Me.grpUnidadMedidaConversion.Controls.Add(Me.lblCodigo)
        Me.grpUnidadMedidaConversion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpUnidadMedidaConversion.Location = New System.Drawing.Point(0, 193)
        Me.grpUnidadMedidaConversion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grpUnidadMedidaConversion.Name = "grpUnidadMedidaConversion"
        Me.grpUnidadMedidaConversion.Size = New System.Drawing.Size(932, 380)
        Me.grpUnidadMedidaConversion.TabIndex = 2
        '
        'txtFactor
        '
        Me.txtFactor.DecimalPlaces = 4
        Me.txtFactor.Location = New System.Drawing.Point(288, 181)
        Me.txtFactor.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtFactor.Name = "txtFactor"
        Me.txtFactor.Size = New System.Drawing.Size(217, 23)
        Me.txtFactor.TabIndex = 7
        '
        'cmbDestino
        '
        Me.cmbDestino.Location = New System.Drawing.Point(288, 151)
        Me.cmbDestino.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbDestino.MenuManager = Me.RibbonControl
        Me.cmbDestino.Name = "cmbDestino"
        Me.cmbDestino.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbDestino.Properties.NullText = ""
        Me.cmbDestino.Size = New System.Drawing.Size(217, 22)
        Me.cmbDestino.TabIndex = 6
        '
        'cmbOrigen
        '
        Me.cmbOrigen.Location = New System.Drawing.Point(288, 121)
        Me.cmbOrigen.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbOrigen.MenuManager = Me.RibbonControl
        Me.cmbOrigen.Name = "cmbOrigen"
        Me.cmbOrigen.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbOrigen.Properties.NullText = ""
        Me.cmbOrigen.Size = New System.Drawing.Size(217, 22)
        Me.cmbOrigen.TabIndex = 5
        '
        'lblID
        '
        Me.lblID.AutoSize = True
        Me.lblID.Location = New System.Drawing.Point(285, 100)
        Me.lblID.Name = "lblID"
        Me.lblID.Size = New System.Drawing.Size(18, 17)
        Me.lblID.TabIndex = 4
        Me.lblID.Text = "--"
        '
        'lblFactor
        '
        Me.lblFactor.AutoSize = True
        Me.lblFactor.Location = New System.Drawing.Point(85, 184)
        Me.lblFactor.Name = "lblFactor"
        Me.lblFactor.Size = New System.Drawing.Size(52, 17)
        Me.lblFactor.TabIndex = 3
        Me.lblFactor.Text = "Factor:"
        '
        'lblUnidadDestino
        '
        Me.lblUnidadDestino.AutoSize = True
        Me.lblUnidadDestino.Location = New System.Drawing.Point(85, 154)
        Me.lblUnidadDestino.Name = "lblUnidadDestino"
        Me.lblUnidadDestino.Size = New System.Drawing.Size(151, 17)
        Me.lblUnidadDestino.TabIndex = 2
        Me.lblUnidadDestino.Text = "Unidad Medida Destino:"
        '
        'lblOrigen
        '
        Me.lblOrigen.AutoSize = True
        Me.lblOrigen.Location = New System.Drawing.Point(85, 124)
        Me.lblOrigen.Name = "lblOrigen"
        Me.lblOrigen.Size = New System.Drawing.Size(145, 17)
        Me.lblOrigen.TabIndex = 1
        Me.lblOrigen.Text = "Unidad Mediad Origen:"
        '
        'lblCodigo
        '
        Me.lblCodigo.AutoSize = True
        Me.lblCodigo.Location = New System.Drawing.Point(85, 100)
        Me.lblCodigo.Name = "lblCodigo"
        Me.lblCodigo.Size = New System.Drawing.Size(56, 17)
        Me.lblCodigo.TabIndex = 0
        Me.lblCodigo.Text = "Código:"
        '
        'frmUnidad_Medida_Conversion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(932, 603)
        Me.Controls.Add(Me.grpUnidadMedidaConversion)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmUnidad_Medida_Conversion"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Unidad Medida Conversion"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpUnidadMedidaConversion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpUnidadMedidaConversion.ResumeLayout(False)
        Me.grpUnidadMedidaConversion.PerformLayout()
        CType(Me.txtFactor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbDestino.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbOrigen.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents cmdGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdEliminar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents grpUnidadMedidaConversion As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtFactor As NumericUpDown
    Friend WithEvents cmbDestino As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbOrigen As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblID As Label
    Friend WithEvents lblFactor As Label
    Friend WithEvents lblUnidadDestino As Label
    Friend WithEvents lblOrigen As Label
    Friend WithEvents lblCodigo As Label
End Class
