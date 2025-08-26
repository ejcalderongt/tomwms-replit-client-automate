<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInventarioConteo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInventarioConteo))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdVerificar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.grpConteo = New DevExpress.XtraEditors.GroupControl()
        Me.txtNomUbicacion = New System.Windows.Forms.TextBox()
        Me.txtIdUbicacion = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbProducto = New DevExpress.XtraEditors.LookUpEdit()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCantidad = New System.Windows.Forms.NumericUpDown()
        Me.lblCantidad = New System.Windows.Forms.Label()
        Me.cmbPEstado = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblPEstado = New System.Windows.Forms.Label()
        Me.dtFechaVence = New DevExpress.XtraEditors.DateEdit()
        Me.lblFechaVence = New System.Windows.Forms.Label()
        Me.txtLote = New System.Windows.Forms.TextBox()
        Me.lblLote = New System.Windows.Forms.Label()
        Me.lblUM = New System.Windows.Forms.Label()
        Me.cmbUM = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbPresentacion = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblPresetacion = New System.Windows.Forms.Label()
        Me.txtProducto = New System.Windows.Forms.TextBox()
        Me.lblProducto = New System.Windows.Forms.Label()
        Me.cmbOperador = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblOperador = New System.Windows.Forms.Label()
        Me.txtUbicacion = New System.Windows.Forms.TextBox()
        Me.lblUbicacion = New System.Windows.Forms.Label()
        Me.cmbTramo = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblTramo = New System.Windows.Forms.Label()
        Me.lblCodInv = New System.Windows.Forms.Label()
        Me.lblCodigoInv = New System.Windows.Forms.Label()
        Me.lblCod = New System.Windows.Forms.Label()
        Me.lblCodigo = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpConteo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpConteo.SuspendLayout()
        CType(Me.cmbProducto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCantidad, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPEstado.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtFechaVence.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtFechaVence.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbUM.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPresentacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbOperador.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTramo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuActualizar, Me.mnuEliminar, Me.cmdVerificar})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(4)
        Me.RibbonControl.MaxItemId = 4
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(878, 193)
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
        'cmdVerificar
        '
        Me.cmdVerificar.Caption = "Verificar"
        Me.cmdVerificar.Id = 3
        Me.cmdVerificar.Name = "cmdVerificar"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Menú"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEliminar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdVerificar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 650)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(878, 30)
        '
        'grpConteo
        '
        Me.grpConteo.Controls.Add(Me.txtNomUbicacion)
        Me.grpConteo.Controls.Add(Me.txtIdUbicacion)
        Me.grpConteo.Controls.Add(Me.Label2)
        Me.grpConteo.Controls.Add(Me.cmbProducto)
        Me.grpConteo.Controls.Add(Me.Label1)
        Me.grpConteo.Controls.Add(Me.txtCantidad)
        Me.grpConteo.Controls.Add(Me.lblCantidad)
        Me.grpConteo.Controls.Add(Me.cmbPEstado)
        Me.grpConteo.Controls.Add(Me.lblPEstado)
        Me.grpConteo.Controls.Add(Me.dtFechaVence)
        Me.grpConteo.Controls.Add(Me.lblFechaVence)
        Me.grpConteo.Controls.Add(Me.txtLote)
        Me.grpConteo.Controls.Add(Me.lblLote)
        Me.grpConteo.Controls.Add(Me.lblUM)
        Me.grpConteo.Controls.Add(Me.cmbUM)
        Me.grpConteo.Controls.Add(Me.cmbPresentacion)
        Me.grpConteo.Controls.Add(Me.lblPresetacion)
        Me.grpConteo.Controls.Add(Me.txtProducto)
        Me.grpConteo.Controls.Add(Me.lblProducto)
        Me.grpConteo.Controls.Add(Me.cmbOperador)
        Me.grpConteo.Controls.Add(Me.lblOperador)
        Me.grpConteo.Controls.Add(Me.txtUbicacion)
        Me.grpConteo.Controls.Add(Me.lblUbicacion)
        Me.grpConteo.Controls.Add(Me.cmbTramo)
        Me.grpConteo.Controls.Add(Me.lblTramo)
        Me.grpConteo.Controls.Add(Me.lblCodInv)
        Me.grpConteo.Controls.Add(Me.lblCodigoInv)
        Me.grpConteo.Controls.Add(Me.lblCod)
        Me.grpConteo.Controls.Add(Me.lblCodigo)
        Me.grpConteo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpConteo.Location = New System.Drawing.Point(0, 193)
        Me.grpConteo.Margin = New System.Windows.Forms.Padding(4)
        Me.grpConteo.Name = "grpConteo"
        Me.grpConteo.Size = New System.Drawing.Size(878, 457)
        Me.grpConteo.TabIndex = 0
        '
        'txtNomUbicacion
        '
        Me.txtNomUbicacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNomUbicacion.Location = New System.Drawing.Point(492, 173)
        Me.txtNomUbicacion.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNomUbicacion.Name = "txtNomUbicacion"
        Me.txtNomUbicacion.ReadOnly = True
        Me.txtNomUbicacion.Size = New System.Drawing.Size(342, 23)
        Me.txtNomUbicacion.TabIndex = 29
        '
        'txtIdUbicacion
        '
        Me.txtIdUbicacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIdUbicacion.Location = New System.Drawing.Point(492, 142)
        Me.txtIdUbicacion.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIdUbicacion.Name = "txtIdUbicacion"
        Me.txtIdUbicacion.Size = New System.Drawing.Size(342, 23)
        Me.txtIdUbicacion.TabIndex = 27
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(425, 144)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(66, 16)
        Me.Label2.TabIndex = 26
        Me.Label2.Text = "Ubicación:"
        '
        'cmbProducto
        '
        Me.cmbProducto.Location = New System.Drawing.Point(492, 106)
        Me.cmbProducto.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbProducto.MenuManager = Me.RibbonControl
        Me.cmbProducto.Name = "cmbProducto"
        Me.cmbProducto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbProducto.Properties.NullText = ""
        Me.cmbProducto.Size = New System.Drawing.Size(342, 22)
        Me.cmbProducto.TabIndex = 25
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(421, 107)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 16)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "Producto:"
        '
        'txtCantidad
        '
        Me.txtCantidad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCantidad.DecimalPlaces = 6
        Me.txtCantidad.Location = New System.Drawing.Point(172, 406)
        Me.txtCantidad.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCantidad.Maximum = New Decimal(New Integer() {100000000, 0, 0, 0})
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.Size = New System.Drawing.Size(225, 23)
        Me.txtCantidad.TabIndex = 23
        '
        'lblCantidad
        '
        Me.lblCantidad.AutoSize = True
        Me.lblCantidad.Location = New System.Drawing.Point(14, 409)
        Me.lblCantidad.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCantidad.Name = "lblCantidad"
        Me.lblCantidad.Size = New System.Drawing.Size(62, 16)
        Me.lblCantidad.TabIndex = 22
        Me.lblCantidad.Text = "Cantidad:"
        '
        'cmbPEstado
        '
        Me.cmbPEstado.Location = New System.Drawing.Point(172, 373)
        Me.cmbPEstado.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbPEstado.MenuManager = Me.RibbonControl
        Me.cmbPEstado.Name = "cmbPEstado"
        Me.cmbPEstado.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPEstado.Properties.NullText = ""
        Me.cmbPEstado.Size = New System.Drawing.Size(225, 22)
        Me.cmbPEstado.TabIndex = 21
        '
        'lblPEstado
        '
        Me.lblPEstado.AutoSize = True
        Me.lblPEstado.Location = New System.Drawing.Point(14, 377)
        Me.lblPEstado.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPEstado.Name = "lblPEstado"
        Me.lblPEstado.Size = New System.Drawing.Size(104, 16)
        Me.lblPEstado.TabIndex = 20
        Me.lblPEstado.Text = "Producto Estado:"
        '
        'dtFechaVence
        '
        Me.dtFechaVence.EditValue = New Date(2018, 1, 18, 12, 37, 13, 0)
        Me.dtFechaVence.Location = New System.Drawing.Point(172, 340)
        Me.dtFechaVence.Margin = New System.Windows.Forms.Padding(4)
        Me.dtFechaVence.MenuManager = Me.RibbonControl
        Me.dtFechaVence.Name = "dtFechaVence"
        Me.dtFechaVence.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtFechaVence.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtFechaVence.Size = New System.Drawing.Size(225, 22)
        Me.dtFechaVence.TabIndex = 19
        '
        'lblFechaVence
        '
        Me.lblFechaVence.AutoSize = True
        Me.lblFechaVence.Location = New System.Drawing.Point(14, 343)
        Me.lblFechaVence.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFechaVence.Name = "lblFechaVence"
        Me.lblFechaVence.Size = New System.Drawing.Size(85, 16)
        Me.lblFechaVence.TabIndex = 18
        Me.lblFechaVence.Text = "Fecha Vence:"
        '
        'txtLote
        '
        Me.txtLote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLote.Location = New System.Drawing.Point(172, 304)
        Me.txtLote.Margin = New System.Windows.Forms.Padding(4)
        Me.txtLote.Name = "txtLote"
        Me.txtLote.Size = New System.Drawing.Size(225, 23)
        Me.txtLote.TabIndex = 17
        '
        'lblLote
        '
        Me.lblLote.AutoSize = True
        Me.lblLote.Location = New System.Drawing.Point(14, 308)
        Me.lblLote.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblLote.Name = "lblLote"
        Me.lblLote.Size = New System.Drawing.Size(36, 16)
        Me.lblLote.TabIndex = 16
        Me.lblLote.Text = "Lote:"
        '
        'lblUM
        '
        Me.lblUM.AutoSize = True
        Me.lblUM.Location = New System.Drawing.Point(14, 274)
        Me.lblUM.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblUM.Name = "lblUM"
        Me.lblUM.Size = New System.Drawing.Size(96, 16)
        Me.lblUM.TabIndex = 14
        Me.lblUM.Text = "Unidad Medida:"
        '
        'cmbUM
        '
        Me.cmbUM.Location = New System.Drawing.Point(172, 271)
        Me.cmbUM.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbUM.MenuManager = Me.RibbonControl
        Me.cmbUM.Name = "cmbUM"
        Me.cmbUM.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbUM.Properties.NullText = ""
        Me.cmbUM.Properties.ReadOnly = True
        Me.cmbUM.Size = New System.Drawing.Size(225, 22)
        Me.cmbUM.TabIndex = 15
        '
        'cmbPresentacion
        '
        Me.cmbPresentacion.Location = New System.Drawing.Point(172, 238)
        Me.cmbPresentacion.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbPresentacion.MenuManager = Me.RibbonControl
        Me.cmbPresentacion.Name = "cmbPresentacion"
        Me.cmbPresentacion.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPresentacion.Properties.NullText = ""
        Me.cmbPresentacion.Size = New System.Drawing.Size(225, 22)
        Me.cmbPresentacion.TabIndex = 13
        '
        'lblPresetacion
        '
        Me.lblPresetacion.AutoSize = True
        Me.lblPresetacion.Location = New System.Drawing.Point(14, 241)
        Me.lblPresetacion.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPresetacion.Name = "lblPresetacion"
        Me.lblPresetacion.Size = New System.Drawing.Size(85, 16)
        Me.lblPresetacion.TabIndex = 12
        Me.lblPresetacion.Text = "Presentación:"
        '
        'txtProducto
        '
        Me.txtProducto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProducto.Location = New System.Drawing.Point(172, 204)
        Me.txtProducto.Margin = New System.Windows.Forms.Padding(4)
        Me.txtProducto.Name = "txtProducto"
        Me.txtProducto.ReadOnly = True
        Me.txtProducto.Size = New System.Drawing.Size(225, 23)
        Me.txtProducto.TabIndex = 11
        '
        'lblProducto
        '
        Me.lblProducto.AutoSize = True
        Me.lblProducto.Location = New System.Drawing.Point(14, 208)
        Me.lblProducto.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblProducto.Name = "lblProducto"
        Me.lblProducto.Size = New System.Drawing.Size(62, 16)
        Me.lblProducto.TabIndex = 10
        Me.lblProducto.Text = "Producto:"
        '
        'cmbOperador
        '
        Me.cmbOperador.Location = New System.Drawing.Point(172, 170)
        Me.cmbOperador.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbOperador.MenuManager = Me.RibbonControl
        Me.cmbOperador.Name = "cmbOperador"
        Me.cmbOperador.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbOperador.Properties.NullText = ""
        Me.cmbOperador.Properties.ReadOnly = True
        Me.cmbOperador.Size = New System.Drawing.Size(225, 22)
        Me.cmbOperador.TabIndex = 9
        '
        'lblOperador
        '
        Me.lblOperador.AutoSize = True
        Me.lblOperador.Location = New System.Drawing.Point(14, 174)
        Me.lblOperador.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblOperador.Name = "lblOperador"
        Me.lblOperador.Size = New System.Drawing.Size(66, 16)
        Me.lblOperador.TabIndex = 8
        Me.lblOperador.Text = "Operador:"
        '
        'txtUbicacion
        '
        Me.txtUbicacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUbicacion.Location = New System.Drawing.Point(172, 137)
        Me.txtUbicacion.Margin = New System.Windows.Forms.Padding(4)
        Me.txtUbicacion.Name = "txtUbicacion"
        Me.txtUbicacion.ReadOnly = True
        Me.txtUbicacion.Size = New System.Drawing.Size(225, 23)
        Me.txtUbicacion.TabIndex = 7
        '
        'lblUbicacion
        '
        Me.lblUbicacion.AutoSize = True
        Me.lblUbicacion.Location = New System.Drawing.Point(14, 140)
        Me.lblUbicacion.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblUbicacion.Name = "lblUbicacion"
        Me.lblUbicacion.Size = New System.Drawing.Size(66, 16)
        Me.lblUbicacion.TabIndex = 6
        Me.lblUbicacion.Text = "Ubicación:"
        '
        'cmbTramo
        '
        Me.cmbTramo.Location = New System.Drawing.Point(172, 105)
        Me.cmbTramo.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbTramo.MenuManager = Me.RibbonControl
        Me.cmbTramo.Name = "cmbTramo"
        Me.cmbTramo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTramo.Properties.NullText = ""
        Me.cmbTramo.Properties.ReadOnly = True
        Me.cmbTramo.Size = New System.Drawing.Size(225, 22)
        Me.cmbTramo.TabIndex = 5
        '
        'lblTramo
        '
        Me.lblTramo.AutoSize = True
        Me.lblTramo.Location = New System.Drawing.Point(14, 108)
        Me.lblTramo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTramo.Name = "lblTramo"
        Me.lblTramo.Size = New System.Drawing.Size(50, 16)
        Me.lblTramo.TabIndex = 4
        Me.lblTramo.Text = "Tramo:"
        '
        'lblCodInv
        '
        Me.lblCodInv.AutoSize = True
        Me.lblCodInv.Location = New System.Drawing.Point(168, 71)
        Me.lblCodInv.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCodInv.Name = "lblCodInv"
        Me.lblCodInv.Size = New System.Drawing.Size(17, 16)
        Me.lblCodInv.TabIndex = 3
        Me.lblCodInv.Text = "--"
        '
        'lblCodigoInv
        '
        Me.lblCodigoInv.AutoSize = True
        Me.lblCodigoInv.Location = New System.Drawing.Point(14, 71)
        Me.lblCodigoInv.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCodigoInv.Name = "lblCodigoInv"
        Me.lblCodigoInv.Size = New System.Drawing.Size(69, 16)
        Me.lblCodigoInv.TabIndex = 2
        Me.lblCodigoInv.Text = "Inventario:"
        '
        'lblCod
        '
        Me.lblCod.AutoSize = True
        Me.lblCod.Location = New System.Drawing.Point(168, 36)
        Me.lblCod.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCod.Name = "lblCod"
        Me.lblCod.Size = New System.Drawing.Size(17, 16)
        Me.lblCod.TabIndex = 1
        Me.lblCod.Text = "--"
        '
        'lblCodigo
        '
        Me.lblCodigo.AutoSize = True
        Me.lblCodigo.Location = New System.Drawing.Point(14, 36)
        Me.lblCodigo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCodigo.Name = "lblCodigo"
        Me.lblCodigo.Size = New System.Drawing.Size(51, 16)
        Me.lblCodigo.TabIndex = 0
        Me.lblCodigo.Text = "Código:"
        '
        'frmInventarioConteo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(878, 680)
        Me.Controls.Add(Me.grpConteo)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmInventarioConteo"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Detalle de conteo"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpConteo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpConteo.ResumeLayout(False)
        Me.grpConteo.PerformLayout()
        CType(Me.cmbProducto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCantidad, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPEstado.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtFechaVence.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtFechaVence.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbUM.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPresentacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbOperador.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTramo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout

End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents mnuActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEliminar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents grpConteo As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblCodInv As Label
    Friend WithEvents lblCodigoInv As Label
    Friend WithEvents lblCod As Label
    Friend WithEvents lblCodigo As Label
    Friend WithEvents lblTramo As Label
    Friend WithEvents cmbTramo As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents txtUbicacion As TextBox
    Friend WithEvents lblUbicacion As Label
    Friend WithEvents cmbOperador As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblOperador As Label
    Friend WithEvents txtProducto As TextBox
    Friend WithEvents lblProducto As Label
    Friend WithEvents lblPresetacion As Label
    Friend WithEvents cmbPresentacion As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents txtLote As TextBox
    Friend WithEvents lblLote As Label
    Friend WithEvents lblUM As Label
    Friend WithEvents cmbUM As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbPEstado As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblPEstado As Label
    Friend WithEvents dtFechaVence As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lblFechaVence As Label
    Friend WithEvents txtCantidad As NumericUpDown
    Friend WithEvents lblCantidad As Label
    Friend WithEvents cmdVerificar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmbProducto As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label1 As Label
    Friend WithEvents txtIdUbicacion As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtNomUbicacion As TextBox
End Class
