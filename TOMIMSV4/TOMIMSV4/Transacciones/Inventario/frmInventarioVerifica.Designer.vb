<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmInventarioVerifica
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInventarioVerifica))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.grpVerificaInv = New DevExpress.XtraEditors.GroupControl()
        Me.txtNomUbicacion = New System.Windows.Forms.TextBox()
        Me.txtIdUbicacion = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtUbicacion = New System.Windows.Forms.TextBox()
        Me.lblUbicacion = New System.Windows.Forms.Label()
        Me.txtCantidad = New System.Windows.Forms.NumericUpDown()
        Me.lblCantidad = New System.Windows.Forms.Label()
        Me.cmbPEstado = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblPEstado = New System.Windows.Forms.Label()
        Me.lblUM = New System.Windows.Forms.Label()
        Me.cmbUM = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbPresentacion = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblPresetacion = New System.Windows.Forms.Label()
        Me.txtProducto = New System.Windows.Forms.TextBox()
        Me.lblProducto = New System.Windows.Forms.Label()
        Me.cmbOperador = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblOperador = New System.Windows.Forms.Label()
        Me.cmbTramo = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblTramo = New System.Windows.Forms.Label()
        Me.lblCodInv = New System.Windows.Forms.Label()
        Me.lblCodigoInv = New System.Windows.Forms.Label()
        Me.lblCod = New System.Windows.Forms.Label()
        Me.lblCodigo = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpVerificaInv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpVerificaInv.SuspendLayout()
        CType(Me.txtCantidad, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPEstado.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbUM.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPresentacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbOperador.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTramo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.EmptyAreaImageOptions.ImagePadding = New System.Windows.Forms.Padding(26, 24, 26, 24)
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuActualizar, Me.mnuEliminar})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 3
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.OptionsMenuMinWidth = 283
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(836, 158)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'mnuActualizar
        '
        Me.mnuActualizar.Caption = "Actualizar"
        Me.mnuActualizar.Id = 1
        Me.mnuActualizar.ImageOptions.SvgImage = CType(resources.GetObject("mnuActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuActualizar.Name = "mnuActualizar"
        '
        'mnuEliminar
        '
        Me.mnuEliminar.Caption = "Eliminar"
        Me.mnuEliminar.Id = 2
        Me.mnuEliminar.ImageOptions.SvgImage = CType(resources.GetObject("mnuEliminar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEliminar.Name = "mnuEliminar"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Opciones de Lista"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEliminar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 443)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(836, 24)
        '
        'grpVerificaInv
        '
        Me.grpVerificaInv.Controls.Add(Me.txtNomUbicacion)
        Me.grpVerificaInv.Controls.Add(Me.txtIdUbicacion)
        Me.grpVerificaInv.Controls.Add(Me.Label2)
        Me.grpVerificaInv.Controls.Add(Me.txtUbicacion)
        Me.grpVerificaInv.Controls.Add(Me.lblUbicacion)
        Me.grpVerificaInv.Controls.Add(Me.txtCantidad)
        Me.grpVerificaInv.Controls.Add(Me.lblCantidad)
        Me.grpVerificaInv.Controls.Add(Me.cmbPEstado)
        Me.grpVerificaInv.Controls.Add(Me.lblPEstado)
        Me.grpVerificaInv.Controls.Add(Me.lblUM)
        Me.grpVerificaInv.Controls.Add(Me.cmbUM)
        Me.grpVerificaInv.Controls.Add(Me.cmbPresentacion)
        Me.grpVerificaInv.Controls.Add(Me.lblPresetacion)
        Me.grpVerificaInv.Controls.Add(Me.txtProducto)
        Me.grpVerificaInv.Controls.Add(Me.lblProducto)
        Me.grpVerificaInv.Controls.Add(Me.cmbOperador)
        Me.grpVerificaInv.Controls.Add(Me.lblOperador)
        Me.grpVerificaInv.Controls.Add(Me.cmbTramo)
        Me.grpVerificaInv.Controls.Add(Me.lblTramo)
        Me.grpVerificaInv.Controls.Add(Me.lblCodInv)
        Me.grpVerificaInv.Controls.Add(Me.lblCodigoInv)
        Me.grpVerificaInv.Controls.Add(Me.lblCod)
        Me.grpVerificaInv.Controls.Add(Me.lblCodigo)
        Me.grpVerificaInv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpVerificaInv.Location = New System.Drawing.Point(0, 158)
        Me.grpVerificaInv.Name = "grpVerificaInv"
        Me.grpVerificaInv.Size = New System.Drawing.Size(836, 285)
        Me.grpVerificaInv.TabIndex = 0
        '
        'txtNomUbicacion
        '
        Me.txtNomUbicacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNomUbicacion.Location = New System.Drawing.Point(504, 132)
        Me.txtNomUbicacion.Name = "txtNomUbicacion"
        Me.txtNomUbicacion.ReadOnly = True
        Me.txtNomUbicacion.Size = New System.Drawing.Size(293, 21)
        Me.txtNomUbicacion.TabIndex = 34
        '
        'txtIdUbicacion
        '
        Me.txtIdUbicacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIdUbicacion.Location = New System.Drawing.Point(504, 106)
        Me.txtIdUbicacion.Name = "txtIdUbicacion"
        Me.txtIdUbicacion.Size = New System.Drawing.Size(293, 21)
        Me.txtIdUbicacion.TabIndex = 33
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(407, 110)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(90, 13)
        Me.Label2.TabIndex = 32
        Me.Label2.Text = "Nueva Ubicación:"
        '
        'txtUbicacion
        '
        Me.txtUbicacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUbicacion.Location = New System.Drawing.Point(504, 80)
        Me.txtUbicacion.Name = "txtUbicacion"
        Me.txtUbicacion.ReadOnly = True
        Me.txtUbicacion.Size = New System.Drawing.Size(293, 21)
        Me.txtUbicacion.TabIndex = 31
        '
        'lblUbicacion
        '
        Me.lblUbicacion.AutoSize = True
        Me.lblUbicacion.Location = New System.Drawing.Point(407, 82)
        Me.lblUbicacion.Name = "lblUbicacion"
        Me.lblUbicacion.Size = New System.Drawing.Size(89, 13)
        Me.lblUbicacion.TabIndex = 30
        Me.lblUbicacion.Text = "Ubicación Actual:"
        '
        'txtCantidad
        '
        Me.txtCantidad.DecimalPlaces = 2
        Me.txtCantidad.Location = New System.Drawing.Point(157, 248)
        Me.txtCantidad.Maximum = New Decimal(New Integer() {100000000, 0, 0, 0})
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.Size = New System.Drawing.Size(192, 21)
        Me.txtCantidad.TabIndex = 17
        '
        'lblCantidad
        '
        Me.lblCantidad.AutoSize = True
        Me.lblCantidad.Location = New System.Drawing.Point(22, 250)
        Me.lblCantidad.Name = "lblCantidad"
        Me.lblCantidad.Size = New System.Drawing.Size(54, 13)
        Me.lblCantidad.TabIndex = 16
        Me.lblCantidad.Text = "Cantidad:"
        '
        'cmbPEstado
        '
        Me.cmbPEstado.Location = New System.Drawing.Point(157, 221)
        Me.cmbPEstado.MenuManager = Me.RibbonControl
        Me.cmbPEstado.Name = "cmbPEstado"
        Me.cmbPEstado.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPEstado.Properties.NullText = ""
        Me.cmbPEstado.Size = New System.Drawing.Size(193, 20)
        Me.cmbPEstado.TabIndex = 15
        '
        'lblPEstado
        '
        Me.lblPEstado.AutoSize = True
        Me.lblPEstado.Location = New System.Drawing.Point(22, 224)
        Me.lblPEstado.Name = "lblPEstado"
        Me.lblPEstado.Size = New System.Drawing.Size(90, 13)
        Me.lblPEstado.TabIndex = 14
        Me.lblPEstado.Text = "Producto Estado:"
        '
        'lblUM
        '
        Me.lblUM.AutoSize = True
        Me.lblUM.Location = New System.Drawing.Point(22, 198)
        Me.lblUM.Name = "lblUM"
        Me.lblUM.Size = New System.Drawing.Size(81, 13)
        Me.lblUM.TabIndex = 12
        Me.lblUM.Text = "Unidad Medida:"
        '
        'cmbUM
        '
        Me.cmbUM.Location = New System.Drawing.Point(157, 195)
        Me.cmbUM.MenuManager = Me.RibbonControl
        Me.cmbUM.Name = "cmbUM"
        Me.cmbUM.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbUM.Properties.NullText = ""
        Me.cmbUM.Properties.ReadOnly = True
        Me.cmbUM.Size = New System.Drawing.Size(193, 20)
        Me.cmbUM.TabIndex = 13
        '
        'cmbPresentacion
        '
        Me.cmbPresentacion.Location = New System.Drawing.Point(157, 169)
        Me.cmbPresentacion.MenuManager = Me.RibbonControl
        Me.cmbPresentacion.Name = "cmbPresentacion"
        Me.cmbPresentacion.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPresentacion.Properties.NullText = ""
        Me.cmbPresentacion.Size = New System.Drawing.Size(193, 20)
        Me.cmbPresentacion.TabIndex = 11
        '
        'lblPresetacion
        '
        Me.lblPresetacion.AutoSize = True
        Me.lblPresetacion.Location = New System.Drawing.Point(22, 172)
        Me.lblPresetacion.Name = "lblPresetacion"
        Me.lblPresetacion.Size = New System.Drawing.Size(73, 13)
        Me.lblPresetacion.TabIndex = 10
        Me.lblPresetacion.Text = "Presentación:"
        '
        'txtProducto
        '
        Me.txtProducto.Location = New System.Drawing.Point(157, 142)
        Me.txtProducto.Name = "txtProducto"
        Me.txtProducto.ReadOnly = True
        Me.txtProducto.Size = New System.Drawing.Size(193, 21)
        Me.txtProducto.TabIndex = 9
        '
        'lblProducto
        '
        Me.lblProducto.AutoSize = True
        Me.lblProducto.Location = New System.Drawing.Point(22, 145)
        Me.lblProducto.Name = "lblProducto"
        Me.lblProducto.Size = New System.Drawing.Size(54, 13)
        Me.lblProducto.TabIndex = 8
        Me.lblProducto.Text = "Producto:"
        '
        'cmbOperador
        '
        Me.cmbOperador.Location = New System.Drawing.Point(157, 115)
        Me.cmbOperador.MenuManager = Me.RibbonControl
        Me.cmbOperador.Name = "cmbOperador"
        Me.cmbOperador.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbOperador.Properties.NullText = ""
        Me.cmbOperador.Properties.ReadOnly = True
        Me.cmbOperador.Size = New System.Drawing.Size(193, 20)
        Me.cmbOperador.TabIndex = 7
        '
        'lblOperador
        '
        Me.lblOperador.AutoSize = True
        Me.lblOperador.Location = New System.Drawing.Point(22, 118)
        Me.lblOperador.Name = "lblOperador"
        Me.lblOperador.Size = New System.Drawing.Size(57, 13)
        Me.lblOperador.TabIndex = 6
        Me.lblOperador.Text = "Operador:"
        '
        'cmbTramo
        '
        Me.cmbTramo.Location = New System.Drawing.Point(157, 85)
        Me.cmbTramo.MenuManager = Me.RibbonControl
        Me.cmbTramo.Name = "cmbTramo"
        Me.cmbTramo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTramo.Properties.NullText = ""
        Me.cmbTramo.Properties.ReadOnly = True
        Me.cmbTramo.Size = New System.Drawing.Size(193, 20)
        Me.cmbTramo.TabIndex = 5
        '
        'lblTramo
        '
        Me.lblTramo.AutoSize = True
        Me.lblTramo.Location = New System.Drawing.Point(22, 88)
        Me.lblTramo.Name = "lblTramo"
        Me.lblTramo.Size = New System.Drawing.Size(41, 13)
        Me.lblTramo.TabIndex = 4
        Me.lblTramo.Text = "Tramo:"
        '
        'lblCodInv
        '
        Me.lblCodInv.AutoSize = True
        Me.lblCodInv.Location = New System.Drawing.Point(154, 62)
        Me.lblCodInv.Name = "lblCodInv"
        Me.lblCodInv.Size = New System.Drawing.Size(15, 13)
        Me.lblCodInv.TabIndex = 3
        Me.lblCodInv.Text = "--"
        '
        'lblCodigoInv
        '
        Me.lblCodigoInv.AutoSize = True
        Me.lblCodigoInv.Location = New System.Drawing.Point(22, 62)
        Me.lblCodigoInv.Name = "lblCodigoInv"
        Me.lblCodigoInv.Size = New System.Drawing.Size(61, 13)
        Me.lblCodigoInv.TabIndex = 2
        Me.lblCodigoInv.Text = "Inventario:"
        '
        'lblCod
        '
        Me.lblCod.AutoSize = True
        Me.lblCod.Location = New System.Drawing.Point(154, 37)
        Me.lblCod.Name = "lblCod"
        Me.lblCod.Size = New System.Drawing.Size(15, 13)
        Me.lblCod.TabIndex = 1
        Me.lblCod.Text = "--"
        '
        'lblCodigo
        '
        Me.lblCodigo.AutoSize = True
        Me.lblCodigo.Location = New System.Drawing.Point(22, 37)
        Me.lblCodigo.Name = "lblCodigo"
        Me.lblCodigo.Size = New System.Drawing.Size(44, 13)
        Me.lblCodigo.TabIndex = 0
        Me.lblCodigo.Text = "Código:"
        '
        'frmInventarioVerifica
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(836, 467)
        Me.Controls.Add(Me.grpVerificaInv)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frmInventarioVerifica"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Inventario Verificación"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpVerificaInv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpVerificaInv.ResumeLayout(False)
        Me.grpVerificaInv.PerformLayout()
        CType(Me.txtCantidad, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPEstado.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbUM.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPresentacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbOperador.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTramo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents mnuActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEliminar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents grpVerificaInv As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtCantidad As NumericUpDown
    Friend WithEvents lblCantidad As Label
    Friend WithEvents cmbPEstado As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblPEstado As Label
    Friend WithEvents lblUM As Label
    Friend WithEvents cmbUM As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbPresentacion As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblPresetacion As Label
    Friend WithEvents txtProducto As TextBox
    Friend WithEvents lblProducto As Label
    Friend WithEvents cmbOperador As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblOperador As Label
    Friend WithEvents cmbTramo As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblTramo As Label
    Friend WithEvents lblCodInv As Label
    Friend WithEvents lblCodigoInv As Label
    Friend WithEvents lblCod As Label
    Friend WithEvents lblCodigo As Label
    Friend WithEvents txtNomUbicacion As TextBox
    Friend WithEvents txtIdUbicacion As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtUbicacion As TextBox
    Friend WithEvents lblUbicacion As Label
End Class
