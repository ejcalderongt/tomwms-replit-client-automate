<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAjustePositivo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAjustePositivo))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.cmdGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.grpReconteo = New DevExpress.XtraEditors.GroupControl()
        Me.cmbUmbas = New DevExpress.XtraEditors.LookUpEdit()
        Me.lbColor = New DevExpress.XtraEditors.LabelControl()
        Me.lbTalla = New DevExpress.XtraEditors.LabelControl()
        Me.cmbColor = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbTalla = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbProductos = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.lbUmbas = New DevExpress.XtraEditors.LabelControl()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtLicencia = New DevExpress.XtraEditors.TextEdit()
        Me.lblUbicacion = New System.Windows.Forms.Label()
        Me.dtpFechaVence = New DevExpress.XtraEditors.DateEdit()
        Me.lblFechaVence = New System.Windows.Forms.Label()
        Me.lblLote = New System.Windows.Forms.Label()
        Me.lblLicPlate = New System.Windows.Forms.Label()
        Me.lblPesoAnterior = New System.Windows.Forms.Label()
        Me.txtUbicacion = New DevExpress.XtraEditors.TextEdit()
        Me.txtLote = New DevExpress.XtraEditors.TextEdit()
        Me.txtPeso = New System.Windows.Forms.NumericUpDown()
        Me.lblOperador = New System.Windows.Forms.Label()
        Me.cmbProductoPresentacion = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbProductoEstado = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtCantidad = New System.Windows.Forms.NumericUpDown()
        Me.lblCantidadAnterior = New System.Windows.Forms.Label()
        Me.lblPresentacion = New System.Windows.Forms.Label()
        Me.lblEstadoProducto = New System.Windows.Forms.Label()
        Me.lbIdStock = New System.Windows.Forms.Label()
        Me.lblId = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpReconteo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpReconteo.SuspendLayout()
        CType(Me.cmbUmbas.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbColor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTalla.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbProductos.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.txtLicencia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFechaVence.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFechaVence.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUbicacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPeso, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbProductoPresentacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbProductoEstado.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCantidad, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.cmdGuardar})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 3
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.Size = New System.Drawing.Size(1012, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'cmdGuardar
        '
        Me.cmdGuardar.Caption = "Guardar"
        Me.cmdGuardar.Id = 1
        Me.cmdGuardar.ImageOptions.SvgImage = CType(resources.GetObject("cmdGuardar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdGuardar.Name = "cmdGuardar"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Ajuste a producto sin existencia"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdGuardar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 543)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1012, 30)
        '
        'grpReconteo
        '
        Me.grpReconteo.Controls.Add(Me.cmbUmbas)
        Me.grpReconteo.Controls.Add(Me.lbColor)
        Me.grpReconteo.Controls.Add(Me.lbTalla)
        Me.grpReconteo.Controls.Add(Me.cmbColor)
        Me.grpReconteo.Controls.Add(Me.cmbTalla)
        Me.grpReconteo.Controls.Add(Me.cmbProductos)
        Me.grpReconteo.Controls.Add(Me.lbUmbas)
        Me.grpReconteo.Controls.Add(Me.GroupBox2)
        Me.grpReconteo.Controls.Add(Me.lblOperador)
        Me.grpReconteo.Controls.Add(Me.cmbProductoPresentacion)
        Me.grpReconteo.Controls.Add(Me.cmbProductoEstado)
        Me.grpReconteo.Controls.Add(Me.txtCantidad)
        Me.grpReconteo.Controls.Add(Me.lblCantidadAnterior)
        Me.grpReconteo.Controls.Add(Me.lblPresentacion)
        Me.grpReconteo.Controls.Add(Me.lblEstadoProducto)
        Me.grpReconteo.Controls.Add(Me.lbIdStock)
        Me.grpReconteo.Controls.Add(Me.lblId)
        Me.grpReconteo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpReconteo.Location = New System.Drawing.Point(0, 193)
        Me.grpReconteo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grpReconteo.Name = "grpReconteo"
        Me.grpReconteo.Size = New System.Drawing.Size(1012, 350)
        Me.grpReconteo.TabIndex = 3
        '
        'cmbUmbas
        '
        Me.cmbUmbas.Location = New System.Drawing.Point(149, 217)
        Me.cmbUmbas.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbUmbas.MenuManager = Me.RibbonControl
        Me.cmbUmbas.Name = "cmbUmbas"
        Me.cmbUmbas.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbUmbas.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbUmbas.Properties.NullText = ""
        Me.cmbUmbas.Size = New System.Drawing.Size(309, 22)
        Me.cmbUmbas.TabIndex = 85
        '
        'lbColor
        '
        Me.lbColor.Location = New System.Drawing.Point(28, 326)
        Me.lbColor.Name = "lbColor"
        Me.lbColor.Size = New System.Drawing.Size(30, 16)
        Me.lbColor.TabIndex = 84
        Me.lbColor.Text = "Color"
        '
        'lbTalla
        '
        Me.lbTalla.Location = New System.Drawing.Point(28, 293)
        Me.lbTalla.Name = "lbTalla"
        Me.lbTalla.Size = New System.Drawing.Size(28, 16)
        Me.lbTalla.TabIndex = 83
        Me.lbTalla.Text = "Talla"
        '
        'cmbColor
        '
        Me.cmbColor.Location = New System.Drawing.Point(149, 322)
        Me.cmbColor.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbColor.MenuManager = Me.RibbonControl
        Me.cmbColor.Name = "cmbColor"
        Me.cmbColor.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbColor.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbColor.Properties.NullText = ""
        Me.cmbColor.Size = New System.Drawing.Size(309, 22)
        Me.cmbColor.TabIndex = 82
        '
        'cmbTalla
        '
        Me.cmbTalla.Location = New System.Drawing.Point(149, 290)
        Me.cmbTalla.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbTalla.MenuManager = Me.RibbonControl
        Me.cmbTalla.Name = "cmbTalla"
        Me.cmbTalla.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbTalla.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTalla.Properties.NullText = ""
        Me.cmbTalla.Size = New System.Drawing.Size(309, 22)
        Me.cmbTalla.TabIndex = 81
        '
        'cmbProductos
        '
        Me.cmbProductos.Location = New System.Drawing.Point(149, 106)
        Me.cmbProductos.Margin = New System.Windows.Forms.Padding(6)
        Me.cmbProductos.MenuManager = Me.RibbonControl
        Me.cmbProductos.Name = "cmbProductos"
        Me.cmbProductos.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbProductos.Properties.PopupView = Me.GridLookUpEdit1View
        Me.cmbProductos.Size = New System.Drawing.Size(309, 22)
        Me.cmbProductos.TabIndex = 80
        '
        'GridLookUpEdit1View
        '
        Me.GridLookUpEdit1View.DetailHeight = 682
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'lbUmbas
        '
        Me.lbUmbas.Location = New System.Drawing.Point(28, 218)
        Me.lbUmbas.Name = "lbUmbas"
        Me.lbUmbas.Size = New System.Drawing.Size(84, 16)
        Me.lbUmbas.TabIndex = 40
        Me.lbUmbas.Text = "Unidad Medida"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtLicencia)
        Me.GroupBox2.Controls.Add(Me.lblUbicacion)
        Me.GroupBox2.Controls.Add(Me.dtpFechaVence)
        Me.GroupBox2.Controls.Add(Me.lblFechaVence)
        Me.GroupBox2.Controls.Add(Me.lblLote)
        Me.GroupBox2.Controls.Add(Me.lblLicPlate)
        Me.GroupBox2.Controls.Add(Me.lblPesoAnterior)
        Me.GroupBox2.Controls.Add(Me.txtUbicacion)
        Me.GroupBox2.Controls.Add(Me.txtLote)
        Me.GroupBox2.Controls.Add(Me.txtPeso)
        Me.GroupBox2.Location = New System.Drawing.Point(483, 45)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Size = New System.Drawing.Size(495, 212)
        Me.GroupBox2.TabIndex = 39
        Me.GroupBox2.TabStop = False
        '
        'txtLicencia
        '
        Me.txtLicencia.Location = New System.Drawing.Point(143, 62)
        Me.txtLicencia.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtLicencia.MenuManager = Me.RibbonControl
        Me.txtLicencia.Name = "txtLicencia"
        Me.txtLicencia.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtLicencia.Size = New System.Drawing.Size(309, 22)
        Me.txtLicencia.TabIndex = 36
        '
        'lblUbicacion
        '
        Me.lblUbicacion.AutoSize = True
        Me.lblUbicacion.Location = New System.Drawing.Point(19, 29)
        Me.lblUbicacion.Name = "lblUbicacion"
        Me.lblUbicacion.Size = New System.Drawing.Size(66, 16)
        Me.lblUbicacion.TabIndex = 15
        Me.lblUbicacion.Text = "Ubicación:"
        '
        'dtpFechaVence
        '
        Me.dtpFechaVence.EditValue = New Date(2018, 3, 7, 10, 34, 56, 981)
        Me.dtpFechaVence.Location = New System.Drawing.Point(143, 99)
        Me.dtpFechaVence.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtpFechaVence.MenuManager = Me.RibbonControl
        Me.dtpFechaVence.Name = "dtpFechaVence"
        Me.dtpFechaVence.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.dtpFechaVence.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpFechaVence.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpFechaVence.Size = New System.Drawing.Size(309, 22)
        Me.dtpFechaVence.TabIndex = 16
        '
        'lblFechaVence
        '
        Me.lblFechaVence.AutoSize = True
        Me.lblFechaVence.Location = New System.Drawing.Point(19, 102)
        Me.lblFechaVence.Name = "lblFechaVence"
        Me.lblFechaVence.Size = New System.Drawing.Size(85, 16)
        Me.lblFechaVence.TabIndex = 17
        Me.lblFechaVence.Text = "Fecha Vence:"
        '
        'lblLote
        '
        Me.lblLote.AutoSize = True
        Me.lblLote.Location = New System.Drawing.Point(19, 139)
        Me.lblLote.Name = "lblLote"
        Me.lblLote.Size = New System.Drawing.Size(36, 16)
        Me.lblLote.TabIndex = 18
        Me.lblLote.Text = "Lote:"
        '
        'lblLicPlate
        '
        Me.lblLicPlate.AutoSize = True
        Me.lblLicPlate.Location = New System.Drawing.Point(19, 68)
        Me.lblLicPlate.Name = "lblLicPlate"
        Me.lblLicPlate.Size = New System.Drawing.Size(57, 16)
        Me.lblLicPlate.TabIndex = 35
        Me.lblLicPlate.Text = "Licencia:"
        '
        'lblPesoAnterior
        '
        Me.lblPesoAnterior.AutoSize = True
        Me.lblPesoAnterior.Location = New System.Drawing.Point(19, 175)
        Me.lblPesoAnterior.Name = "lblPesoAnterior"
        Me.lblPesoAnterior.Size = New System.Drawing.Size(39, 16)
        Me.lblPesoAnterior.TabIndex = 20
        Me.lblPesoAnterior.Text = "Peso:"
        '
        'txtUbicacion
        '
        Me.txtUbicacion.Location = New System.Drawing.Point(143, 26)
        Me.txtUbicacion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtUbicacion.MenuManager = Me.RibbonControl
        Me.txtUbicacion.Name = "txtUbicacion"
        Me.txtUbicacion.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtUbicacion.Properties.ReadOnly = True
        Me.txtUbicacion.Size = New System.Drawing.Size(309, 22)
        Me.txtUbicacion.TabIndex = 26
        '
        'txtLote
        '
        Me.txtLote.Location = New System.Drawing.Point(143, 131)
        Me.txtLote.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtLote.MenuManager = Me.RibbonControl
        Me.txtLote.Name = "txtLote"
        Me.txtLote.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtLote.Size = New System.Drawing.Size(309, 22)
        Me.txtLote.TabIndex = 27
        '
        'txtPeso
        '
        Me.txtPeso.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPeso.DecimalPlaces = 6
        Me.txtPeso.Location = New System.Drawing.Point(143, 173)
        Me.txtPeso.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtPeso.Maximum = New Decimal(New Integer() {1569325056, 23283064, 0, 0})
        Me.txtPeso.Name = "txtPeso"
        Me.txtPeso.Size = New System.Drawing.Size(309, 23)
        Me.txtPeso.TabIndex = 30
        '
        'lblOperador
        '
        Me.lblOperador.AutoSize = True
        Me.lblOperador.Location = New System.Drawing.Point(25, 110)
        Me.lblOperador.Name = "lblOperador"
        Me.lblOperador.Size = New System.Drawing.Size(62, 16)
        Me.lblOperador.TabIndex = 37
        Me.lblOperador.Text = "Producto:"
        '
        'cmbProductoPresentacion
        '
        Me.cmbProductoPresentacion.Location = New System.Drawing.Point(149, 178)
        Me.cmbProductoPresentacion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbProductoPresentacion.MenuManager = Me.RibbonControl
        Me.cmbProductoPresentacion.Name = "cmbProductoPresentacion"
        Me.cmbProductoPresentacion.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbProductoPresentacion.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbProductoPresentacion.Properties.NullText = ""
        Me.cmbProductoPresentacion.Size = New System.Drawing.Size(309, 22)
        Me.cmbProductoPresentacion.TabIndex = 32
        '
        'cmbProductoEstado
        '
        Me.cmbProductoEstado.Location = New System.Drawing.Point(149, 141)
        Me.cmbProductoEstado.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbProductoEstado.MenuManager = Me.RibbonControl
        Me.cmbProductoEstado.Name = "cmbProductoEstado"
        Me.cmbProductoEstado.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbProductoEstado.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbProductoEstado.Properties.NullText = ""
        Me.cmbProductoEstado.Size = New System.Drawing.Size(309, 22)
        Me.cmbProductoEstado.TabIndex = 31
        '
        'txtCantidad
        '
        Me.txtCantidad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCantidad.DecimalPlaces = 6
        Me.txtCantidad.Location = New System.Drawing.Point(149, 256)
        Me.txtCantidad.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCantidad.Maximum = New Decimal(New Integer() {1569325056, 23283064, 0, 0})
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.Size = New System.Drawing.Size(309, 23)
        Me.txtCantidad.TabIndex = 28
        Me.txtCantidad.Visible = False
        '
        'lblCantidadAnterior
        '
        Me.lblCantidadAnterior.AutoSize = True
        Me.lblCantidadAnterior.Location = New System.Drawing.Point(25, 259)
        Me.lblCantidadAnterior.Name = "lblCantidadAnterior"
        Me.lblCantidadAnterior.Size = New System.Drawing.Size(62, 16)
        Me.lblCantidadAnterior.TabIndex = 19
        Me.lblCantidadAnterior.Text = "Cantidad:"
        Me.lblCantidadAnterior.Visible = False
        '
        'lblPresentacion
        '
        Me.lblPresentacion.AutoSize = True
        Me.lblPresentacion.Location = New System.Drawing.Point(25, 181)
        Me.lblPresentacion.Name = "lblPresentacion"
        Me.lblPresentacion.Size = New System.Drawing.Size(85, 16)
        Me.lblPresentacion.TabIndex = 14
        Me.lblPresentacion.Text = "Presentación:"
        '
        'lblEstadoProducto
        '
        Me.lblEstadoProducto.AutoSize = True
        Me.lblEstadoProducto.Location = New System.Drawing.Point(25, 145)
        Me.lblEstadoProducto.Name = "lblEstadoProducto"
        Me.lblEstadoProducto.Size = New System.Drawing.Size(104, 16)
        Me.lblEstadoProducto.TabIndex = 13
        Me.lblEstadoProducto.Text = "Estado Producto:"
        '
        'lbIdStock
        '
        Me.lbIdStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbIdStock.Location = New System.Drawing.Point(149, 68)
        Me.lbIdStock.Name = "lbIdStock"
        Me.lbIdStock.Size = New System.Drawing.Size(309, 21)
        Me.lbIdStock.TabIndex = 3
        '
        'lblId
        '
        Me.lblId.AutoSize = True
        Me.lblId.Location = New System.Drawing.Point(25, 69)
        Me.lblId.Name = "lblId"
        Me.lblId.Size = New System.Drawing.Size(54, 16)
        Me.lblId.TabIndex = 2
        Me.lblId.Text = "IdStock:"
        '
        'frmAjustePositivo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1012, 573)
        Me.Controls.Add(Me.grpReconteo)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frmAjustePositivo"
        Me.Ribbon = Me.RibbonControl
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Ajuste Positivo"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpReconteo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpReconteo.ResumeLayout(False)
        Me.grpReconteo.PerformLayout()
        CType(Me.cmbUmbas.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbColor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTalla.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbProductos.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.txtLicencia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFechaVence.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFechaVence.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUbicacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPeso, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbProductoPresentacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbProductoEstado.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCantidad, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents grpReconteo As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblOperador As Label
    Friend WithEvents lblLicPlate As Label
    Friend WithEvents cmbProductoPresentacion As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbProductoEstado As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents txtPeso As NumericUpDown
    Friend WithEvents txtLote As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtUbicacion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblPesoAnterior As Label
    Friend WithEvents lblLote As Label
    Friend WithEvents lblFechaVence As Label
    Friend WithEvents dtpFechaVence As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lblUbicacion As Label
    Friend WithEvents lblPresentacion As Label
    Friend WithEvents lblEstadoProducto As Label
    Friend WithEvents lbIdStock As Label
    Friend WithEvents lblId As Label
    Friend WithEvents cmdGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents txtLicencia As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCantidad As NumericUpDown
    Friend WithEvents lblCantidadAnterior As Label
    Friend WithEvents lbUmbas As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbProductos As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lbColor As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lbTalla As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbColor As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbTalla As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbUmbas As DevExpress.XtraEditors.LookUpEdit
End Class
