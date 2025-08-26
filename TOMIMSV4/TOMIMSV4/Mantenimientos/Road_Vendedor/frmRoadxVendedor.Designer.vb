<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRoadxVendedor
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
        Dim Label13 As System.Windows.Forms.Label
        Dim Label12 As System.Windows.Forms.Label
        Dim Label11 As System.Windows.Forms.Label
        Dim Label10 As System.Windows.Forms.Label
        Dim Label9 As System.Windows.Forms.Label
        Dim Label8 As System.Windows.Forms.Label
        Dim Label7 As System.Windows.Forms.Label
        Dim Label6 As System.Windows.Forms.Label
        Dim Label5 As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim IdBodegaLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRoadxVendedor))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.cmbRuta = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtRuta = New DevExpress.XtraEditors.TextEdit()
        Me.txtSpinPrecio = New DevExpress.XtraEditors.SpinEdit()
        Me.txtDatapickerFechaUltimaLiquidacion = New DevExpress.XtraEditors.DateEdit()
        Me.txtBodega = New DevExpress.XtraEditors.TextEdit()
        Me.cbxBloquear = New System.Windows.Forms.CheckBox()
        Me.txtSpinDevolucionSAP = New DevExpress.XtraEditors.SpinEdit()
        Me.txtLiquidado = New DevExpress.XtraEditors.TextEdit()
        Me.txtCodVehiculo = New DevExpress.XtraEditors.TextEdit()
        Me.txtSubbodega = New DevExpress.XtraEditors.TextEdit()
        Me.txtSpinNivel = New DevExpress.XtraEditors.SpinEdit()
        Me.txtClave = New DevExpress.XtraEditors.TextEdit()
        Me.txtNombre = New DevExpress.XtraEditors.TextEdit()
        Me.txtCodigo = New DevExpress.XtraEditors.TextEdit()
        Me.txtIDVendedor = New DevExpress.XtraEditors.TextEdit()
        Label13 = New System.Windows.Forms.Label()
        Label12 = New System.Windows.Forms.Label()
        Label11 = New System.Windows.Forms.Label()
        Label10 = New System.Windows.Forms.Label()
        Label9 = New System.Windows.Forms.Label()
        Label8 = New System.Windows.Forms.Label()
        Label7 = New System.Windows.Forms.Label()
        Label6 = New System.Windows.Forms.Label()
        Label5 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        IdBodegaLabel = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.cmbRuta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRuta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSpinPrecio.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDatapickerFechaUltimaLiquidacion.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDatapickerFechaUltimaLiquidacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSpinDevolucionSAP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLiquidado.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodVehiculo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSubbodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSpinNivel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtClave.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombre.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodigo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIDVendedor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label13
        '
        Label13.AutoSize = True
        Label13.Location = New System.Drawing.Point(20, 457)
        Label13.Name = "Label13"
        Label13.Size = New System.Drawing.Size(104, 17)
        Label13.TabIndex = 27
        Label13.Text = "Devolucion SAP"
        '
        'Label12
        '
        Label12.AutoSize = True
        Label12.Location = New System.Drawing.Point(20, 425)
        Label12.Name = "Label12"
        Label12.Size = New System.Drawing.Size(61, 17)
        Label12.TabIndex = 25
        Label12.Text = "Bloquear"
        '
        'Label11
        '
        Label11.AutoSize = True
        Label11.Location = New System.Drawing.Point(20, 393)
        Label11.Name = "Label11"
        Label11.Size = New System.Drawing.Size(156, 17)
        Label11.TabIndex = 23
        Label11.Text = "Ultima Fecha Liquidacion"
        '
        'Label10
        '
        Label10.AutoSize = True
        Label10.Location = New System.Drawing.Point(20, 361)
        Label10.Name = "Label10"
        Label10.Size = New System.Drawing.Size(74, 17)
        Label10.TabIndex = 21
        Label10.Text = "Liquidando"
        '
        'Label9
        '
        Label9.AutoSize = True
        Label9.Location = New System.Drawing.Point(20, 329)
        Label9.Name = "Label9"
        Label9.Size = New System.Drawing.Size(87, 17)
        Label9.TabIndex = 19
        Label9.Text = "Cod Vehiculo"
        '
        'Label8
        '
        Label8.AutoSize = True
        Label8.Location = New System.Drawing.Point(20, 297)
        Label8.Name = "Label8"
        Label8.Size = New System.Drawing.Size(82, 17)
        Label8.TabIndex = 17
        Label8.Text = "Sub bodega"
        '
        'Label7
        '
        Label7.AutoSize = True
        Label7.Location = New System.Drawing.Point(20, 233)
        Label7.Name = "Label7"
        Label7.Size = New System.Drawing.Size(45, 17)
        Label7.TabIndex = 13
        Label7.Text = "Precio"
        '
        'Label6
        '
        Label6.AutoSize = True
        Label6.Location = New System.Drawing.Point(20, 199)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(41, 17)
        Label6.TabIndex = 11
        Label6.Text = "Nivel:"
        '
        'Label5
        '
        Label5.AutoSize = True
        Label5.Location = New System.Drawing.Point(20, 167)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(42, 17)
        Label5.TabIndex = 8
        Label5.Text = "Ruta:"
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(20, 135)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(41, 17)
        Label4.TabIndex = 6
        Label4.Text = "Clave"
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.Location = New System.Drawing.Point(20, 71)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(51, 17)
        Label3.TabIndex = 2
        Label3.Text = "Codigo"
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(20, 39)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(22, 17)
        Label2.TabIndex = 0
        Label2.Text = "ID"
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(20, 103)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(62, 17)
        Label1.TabIndex = 4
        Label1.Text = "Nombre:"
        '
        'IdBodegaLabel
        '
        IdBodegaLabel.AutoSize = True
        IdBodegaLabel.Location = New System.Drawing.Point(20, 265)
        IdBodegaLabel.Name = "IdBodegaLabel"
        IdBodegaLabel.Size = New System.Drawing.Size(59, 17)
        IdBodegaLabel.TabIndex = 15
        IdBodegaLabel.Text = "Bodega:"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuGuardar, Me.mnuActualizar, Me.mnuEliminar, Me.RibbonControl.SearchEditItem})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 4
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(555, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'mnuGuardar
        '
        Me.mnuGuardar.Caption = "Guardar"
        Me.mnuGuardar.Id = 1
        Me.mnuGuardar.ImageOptions.SvgImage = CType(resources.GetObject("mnuGuardar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuGuardar.Name = "mnuGuardar"
        '
        'mnuActualizar
        '
        Me.mnuActualizar.Caption = "Actualizar"
        Me.mnuActualizar.Id = 2
        Me.mnuActualizar.ImageOptions.SvgImage = CType(resources.GetObject("mnuActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuActualizar.Name = "mnuActualizar"
        '
        'mnuEliminar
        '
        Me.mnuEliminar.Caption = "Eliminar"
        Me.mnuEliminar.Id = 3
        Me.mnuEliminar.ImageOptions.SvgImage = CType(resources.GetObject("mnuEliminar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEliminar.Name = "mnuEliminar"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Vendedor ROAD"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEliminar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 711)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(555, 30)
        '
        'GroupControl1
        '
        Me.GroupControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.GroupControl1.Controls.Add(Me.cmbRuta)
        Me.GroupControl1.Controls.Add(Me.txtRuta)
        Me.GroupControl1.Controls.Add(Me.txtSpinPrecio)
        Me.GroupControl1.Controls.Add(Me.txtDatapickerFechaUltimaLiquidacion)
        Me.GroupControl1.Controls.Add(Me.txtBodega)
        Me.GroupControl1.Controls.Add(Me.cbxBloquear)
        Me.GroupControl1.Controls.Add(Label13)
        Me.GroupControl1.Controls.Add(Me.txtSpinDevolucionSAP)
        Me.GroupControl1.Controls.Add(Label12)
        Me.GroupControl1.Controls.Add(Label11)
        Me.GroupControl1.Controls.Add(Me.txtLiquidado)
        Me.GroupControl1.Controls.Add(Label10)
        Me.GroupControl1.Controls.Add(Me.txtCodVehiculo)
        Me.GroupControl1.Controls.Add(Label9)
        Me.GroupControl1.Controls.Add(Label8)
        Me.GroupControl1.Controls.Add(Me.txtSubbodega)
        Me.GroupControl1.Controls.Add(Label7)
        Me.GroupControl1.Controls.Add(Label6)
        Me.GroupControl1.Controls.Add(Me.txtSpinNivel)
        Me.GroupControl1.Controls.Add(Label5)
        Me.GroupControl1.Controls.Add(Label4)
        Me.GroupControl1.Controls.Add(Me.txtClave)
        Me.GroupControl1.Controls.Add(Me.txtNombre)
        Me.GroupControl1.Controls.Add(Me.txtCodigo)
        Me.GroupControl1.Controls.Add(Label3)
        Me.GroupControl1.Controls.Add(Me.txtIDVendedor)
        Me.GroupControl1.Controls.Add(Label2)
        Me.GroupControl1.Controls.Add(Label1)
        Me.GroupControl1.Controls.Add(IdBodegaLabel)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 193)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(555, 518)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Datos de Vendedor"
        '
        'cmbRuta
        '
        Me.cmbRuta.Location = New System.Drawing.Point(166, 165)
        Me.cmbRuta.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbRuta.MenuManager = Me.RibbonControl
        Me.cmbRuta.Name = "cmbRuta"
        Me.cmbRuta.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbRuta.Properties.NullText = ""
        Me.cmbRuta.Size = New System.Drawing.Size(192, 22)
        Me.cmbRuta.TabIndex = 29
        '
        'txtRuta
        '
        Me.txtRuta.Location = New System.Drawing.Point(365, 164)
        Me.txtRuta.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtRuta.MenuManager = Me.RibbonControl
        Me.txtRuta.Name = "txtRuta"
        Me.txtRuta.Size = New System.Drawing.Size(147, 22)
        Me.txtRuta.TabIndex = 10
        '
        'txtSpinPrecio
        '
        Me.txtSpinPrecio.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtSpinPrecio.Location = New System.Drawing.Point(166, 229)
        Me.txtSpinPrecio.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtSpinPrecio.MenuManager = Me.RibbonControl
        Me.txtSpinPrecio.Name = "txtSpinPrecio"
        Me.txtSpinPrecio.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtSpinPrecio.Properties.IsFloatValue = False
        Me.txtSpinPrecio.Properties.Mask.EditMask = "N00"
        Me.txtSpinPrecio.Size = New System.Drawing.Size(346, 24)
        Me.txtSpinPrecio.TabIndex = 14
        '
        'txtDatapickerFechaUltimaLiquidacion
        '
        Me.txtDatapickerFechaUltimaLiquidacion.EditValue = Nothing
        Me.txtDatapickerFechaUltimaLiquidacion.Location = New System.Drawing.Point(166, 389)
        Me.txtDatapickerFechaUltimaLiquidacion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtDatapickerFechaUltimaLiquidacion.MenuManager = Me.RibbonControl
        Me.txtDatapickerFechaUltimaLiquidacion.Name = "txtDatapickerFechaUltimaLiquidacion"
        Me.txtDatapickerFechaUltimaLiquidacion.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtDatapickerFechaUltimaLiquidacion.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtDatapickerFechaUltimaLiquidacion.Size = New System.Drawing.Size(346, 22)
        Me.txtDatapickerFechaUltimaLiquidacion.TabIndex = 24
        '
        'txtBodega
        '
        Me.txtBodega.Location = New System.Drawing.Point(166, 261)
        Me.txtBodega.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtBodega.MenuManager = Me.RibbonControl
        Me.txtBodega.Name = "txtBodega"
        Me.txtBodega.Size = New System.Drawing.Size(346, 22)
        Me.txtBodega.TabIndex = 16
        '
        'cbxBloquear
        '
        Me.cbxBloquear.AutoSize = True
        Me.cbxBloquear.Location = New System.Drawing.Point(166, 423)
        Me.cbxBloquear.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbxBloquear.Name = "cbxBloquear"
        Me.cbxBloquear.Size = New System.Drawing.Size(40, 21)
        Me.cbxBloquear.TabIndex = 26
        Me.cbxBloquear.Text = "Si"
        Me.cbxBloquear.UseVisualStyleBackColor = True
        '
        'txtSpinDevolucionSAP
        '
        Me.txtSpinDevolucionSAP.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtSpinDevolucionSAP.Location = New System.Drawing.Point(166, 453)
        Me.txtSpinDevolucionSAP.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtSpinDevolucionSAP.MenuManager = Me.RibbonControl
        Me.txtSpinDevolucionSAP.Name = "txtSpinDevolucionSAP"
        Me.txtSpinDevolucionSAP.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtSpinDevolucionSAP.Properties.IsFloatValue = False
        Me.txtSpinDevolucionSAP.Properties.Mask.EditMask = "N00"
        Me.txtSpinDevolucionSAP.Size = New System.Drawing.Size(346, 24)
        Me.txtSpinDevolucionSAP.TabIndex = 28
        '
        'txtLiquidado
        '
        Me.txtLiquidado.Location = New System.Drawing.Point(166, 357)
        Me.txtLiquidado.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtLiquidado.MenuManager = Me.RibbonControl
        Me.txtLiquidado.Name = "txtLiquidado"
        Me.txtLiquidado.Size = New System.Drawing.Size(346, 22)
        Me.txtLiquidado.TabIndex = 22
        '
        'txtCodVehiculo
        '
        Me.txtCodVehiculo.Location = New System.Drawing.Point(166, 325)
        Me.txtCodVehiculo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCodVehiculo.MenuManager = Me.RibbonControl
        Me.txtCodVehiculo.Name = "txtCodVehiculo"
        Me.txtCodVehiculo.Size = New System.Drawing.Size(346, 22)
        Me.txtCodVehiculo.TabIndex = 20
        '
        'txtSubbodega
        '
        Me.txtSubbodega.Location = New System.Drawing.Point(166, 293)
        Me.txtSubbodega.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtSubbodega.MenuManager = Me.RibbonControl
        Me.txtSubbodega.Name = "txtSubbodega"
        Me.txtSubbodega.Size = New System.Drawing.Size(346, 22)
        Me.txtSubbodega.TabIndex = 18
        '
        'txtSpinNivel
        '
        Me.txtSpinNivel.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtSpinNivel.Location = New System.Drawing.Point(166, 196)
        Me.txtSpinNivel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtSpinNivel.MenuManager = Me.RibbonControl
        Me.txtSpinNivel.Name = "txtSpinNivel"
        Me.txtSpinNivel.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtSpinNivel.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtSpinNivel.Properties.IsFloatValue = False
        Me.txtSpinNivel.Properties.Mask.EditMask = "N00"
        Me.txtSpinNivel.Size = New System.Drawing.Size(346, 24)
        Me.txtSpinNivel.TabIndex = 12
        '
        'txtClave
        '
        Me.txtClave.Location = New System.Drawing.Point(166, 132)
        Me.txtClave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtClave.MenuManager = Me.RibbonControl
        Me.txtClave.Name = "txtClave"
        Me.txtClave.Size = New System.Drawing.Size(346, 22)
        Me.txtClave.TabIndex = 7
        '
        'txtNombre
        '
        Me.txtNombre.Location = New System.Drawing.Point(166, 100)
        Me.txtNombre.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNombre.MenuManager = Me.RibbonControl
        Me.txtNombre.Name = "txtNombre"
        Me.txtNombre.Size = New System.Drawing.Size(346, 22)
        Me.txtNombre.TabIndex = 5
        '
        'txtCodigo
        '
        Me.txtCodigo.Location = New System.Drawing.Point(166, 68)
        Me.txtCodigo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCodigo.MenuManager = Me.RibbonControl
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.Size = New System.Drawing.Size(346, 22)
        Me.txtCodigo.TabIndex = 3
        '
        'txtIDVendedor
        '
        Me.txtIDVendedor.Enabled = False
        Me.txtIDVendedor.Location = New System.Drawing.Point(166, 36)
        Me.txtIDVendedor.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtIDVendedor.MenuManager = Me.RibbonControl
        Me.txtIDVendedor.Name = "txtIDVendedor"
        Me.txtIDVendedor.Size = New System.Drawing.Size(346, 22)
        Me.txtIDVendedor.TabIndex = 1
        '
        'frmRoadxVendedor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(555, 741)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmRoadxVendedor"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Vendedor ROAD"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.cmbRuta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRuta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSpinPrecio.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDatapickerFechaUltimaLiquidacion.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDatapickerFechaUltimaLiquidacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSpinDevolucionSAP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLiquidado.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodVehiculo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSubbodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSpinNivel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtClave.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombre.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodigo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIDVendedor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents mnuGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEliminar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cbxBloquear As System.Windows.Forms.CheckBox
    Friend WithEvents txtSpinDevolucionSAP As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents txtLiquidado As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCodVehiculo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtSubbodega As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtSpinNivel As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents txtClave As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNombre As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCodigo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtIDVendedor As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtDatapickerFechaUltimaLiquidacion As DevExpress.XtraEditors.DateEdit
    Friend WithEvents txtBodega As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtSpinPrecio As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents txtRuta As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cmbRuta As DevExpress.XtraEditors.LookUpEdit

End Class
