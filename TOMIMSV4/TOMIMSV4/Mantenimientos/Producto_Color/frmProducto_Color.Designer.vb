<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProducto_Color
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
        Dim lblDescripcion As System.Windows.Forms.Label
        Dim ActivoLabel As System.Windows.Forms.Label
        Dim lblIdClasificacionS As System.Windows.Forms.Label
        Dim IdPropietarioLabel As System.Windows.Forms.Label
        Dim DescripcionLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProducto_Color))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.cmdGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.PaletColor = New DevExpress.XtraEditors.ColorEdit()
        Me.txtDescripcion = New DevExpress.XtraEditors.TextEdit()
        Me.cmbPropietario = New DevExpress.XtraEditors.LookUpEdit()
        Me.chkActivo = New DevExpress.XtraEditors.CheckEdit()
        Me.lblIdColor = New System.Windows.Forms.Label()
        Me.txtNombre = New DevExpress.XtraEditors.TextEdit()
        lblDescripcion = New System.Windows.Forms.Label()
        ActivoLabel = New System.Windows.Forms.Label()
        lblIdClasificacionS = New System.Windows.Forms.Label()
        IdPropietarioLabel = New System.Windows.Forms.Label()
        DescripcionLabel = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.PaletColor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescripcion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombre.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblDescripcion
        '
        lblDescripcion.AutoSize = True
        lblDescripcion.Location = New System.Drawing.Point(26, 152)
        lblDescripcion.Name = "lblDescripcion"
        lblDescripcion.Size = New System.Drawing.Size(42, 16)
        lblDescripcion.TabIndex = 11
        lblDescripcion.Text = "Color:"
        '
        'ActivoLabel
        '
        ActivoLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        ActivoLabel.AutoSize = True
        ActivoLabel.Location = New System.Drawing.Point(26, 189)
        ActivoLabel.Name = "ActivoLabel"
        ActivoLabel.Size = New System.Drawing.Size(46, 16)
        ActivoLabel.TabIndex = 8
        ActivoLabel.Text = "Activo:"
        '
        'lblIdClasificacionS
        '
        lblIdClasificacionS.AutoSize = True
        lblIdClasificacionS.Location = New System.Drawing.Point(26, 46)
        lblIdClasificacionS.Name = "lblIdClasificacionS"
        lblIdClasificacionS.Size = New System.Drawing.Size(78, 16)
        lblIdClasificacionS.TabIndex = 0
        lblIdClasificacionS.Text = "Identificador"
        '
        'IdPropietarioLabel
        '
        IdPropietarioLabel.AutoSize = True
        IdPropietarioLabel.Location = New System.Drawing.Point(26, 81)
        IdPropietarioLabel.Name = "IdPropietarioLabel"
        IdPropietarioLabel.Size = New System.Drawing.Size(74, 16)
        IdPropietarioLabel.TabIndex = 2
        IdPropietarioLabel.Text = "Propietario:"
        '
        'DescripcionLabel
        '
        DescripcionLabel.AutoSize = True
        DescripcionLabel.Location = New System.Drawing.Point(26, 116)
        DescripcionLabel.Name = "DescripcionLabel"
        DescripcionLabel.Size = New System.Drawing.Size(57, 16)
        DescripcionLabel.TabIndex = 4
        DescripcionLabel.Text = "Nombre:"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.cmdGuardar, Me.cmdActualizar, Me.cmdEliminar})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 4
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.Size = New System.Drawing.Size(860, 193)
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
        Me.RibbonPage1.Text = "Producto Colores"
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
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 700)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(860, 30)
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.PaletColor)
        Me.GroupControl1.Controls.Add(lblDescripcion)
        Me.GroupControl1.Controls.Add(Me.txtDescripcion)
        Me.GroupControl1.Controls.Add(Me.cmbPropietario)
        Me.GroupControl1.Controls.Add(ActivoLabel)
        Me.GroupControl1.Controls.Add(Me.chkActivo)
        Me.GroupControl1.Controls.Add(Me.lblIdColor)
        Me.GroupControl1.Controls.Add(lblIdClasificacionS)
        Me.GroupControl1.Controls.Add(IdPropietarioLabel)
        Me.GroupControl1.Controls.Add(DescripcionLabel)
        Me.GroupControl1.Controls.Add(Me.txtNombre)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 193)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(860, 507)
        Me.GroupControl1.TabIndex = 3
        Me.GroupControl1.Text = "Datos del color"
        '
        'PaletColor
        '
        Me.PaletColor.EditValue = System.Drawing.Color.Empty
        Me.PaletColor.Location = New System.Drawing.Point(162, 149)
        Me.PaletColor.MenuManager = Me.RibbonControl
        Me.PaletColor.Name = "PaletColor"
        Me.PaletColor.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.PaletColor.Size = New System.Drawing.Size(133, 22)
        Me.PaletColor.TabIndex = 15
        '
        'txtDescripcion
        '
        Me.txtDescripcion.Enabled = False
        Me.txtDescripcion.Location = New System.Drawing.Point(351, 149)
        Me.txtDescripcion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtDescripcion.MenuManager = Me.RibbonControl
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.Size = New System.Drawing.Size(111, 22)
        Me.txtDescripcion.TabIndex = 12
        '
        'cmbPropietario
        '
        Me.cmbPropietario.Location = New System.Drawing.Point(162, 79)
        Me.cmbPropietario.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbPropietario.MenuManager = Me.RibbonControl
        Me.cmbPropietario.Name = "cmbPropietario"
        Me.cmbPropietario.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPropietario.Properties.NullText = ""
        Me.cmbPropietario.Size = New System.Drawing.Size(300, 22)
        Me.cmbPropietario.TabIndex = 10
        '
        'chkActivo
        '
        Me.chkActivo.EditValue = True
        Me.chkActivo.Location = New System.Drawing.Point(162, 185)
        Me.chkActivo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkActivo.MenuManager = Me.RibbonControl
        Me.chkActivo.Name = "chkActivo"
        Me.chkActivo.Properties.Caption = ""
        Me.chkActivo.Size = New System.Drawing.Size(41, 24)
        Me.chkActivo.TabIndex = 9
        '
        'lblIdColor
        '
        Me.lblIdColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblIdColor.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIdColor.Location = New System.Drawing.Point(162, 44)
        Me.lblIdColor.Name = "lblIdColor"
        Me.lblIdColor.Size = New System.Drawing.Size(300, 25)
        Me.lblIdColor.TabIndex = 1
        '
        'txtNombre
        '
        Me.txtNombre.Location = New System.Drawing.Point(162, 113)
        Me.txtNombre.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNombre.MenuManager = Me.RibbonControl
        Me.txtNombre.Name = "txtNombre"
        Me.txtNombre.Size = New System.Drawing.Size(300, 22)
        Me.txtNombre.TabIndex = 5
        '
        'frmProducto_Color
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(860, 730)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frmProducto_Color"
        Me.Ribbon = Me.RibbonControl
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Producto Color"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.PaletColor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescripcion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombre.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtDescripcion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cmbPropietario As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents chkActivo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents lblIdColor As Label
    Friend WithEvents txtNombre As DevExpress.XtraEditors.TextEdit
    Friend WithEvents PaletColor As DevExpress.XtraEditors.ColorEdit
End Class
