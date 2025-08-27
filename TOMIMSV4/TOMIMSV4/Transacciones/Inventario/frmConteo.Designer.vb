<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConteo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConteo))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.cmdGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.lblCodigoEnc = New System.Windows.Forms.Label()
        Me.lblCodigo = New System.Windows.Forms.Label()
        Me.lblId = New System.Windows.Forms.Label()
        Me.lblCodReconteo = New System.Windows.Forms.Label()
        Me.lblProducto = New System.Windows.Forms.Label()
        Me.lblEstadoProducto = New System.Windows.Forms.Label()
        Me.lblPresentacion = New System.Windows.Forms.Label()
        Me.lblUbicacion = New System.Windows.Forms.Label()
        Me.dtpFechaVence = New DevExpress.XtraEditors.DateEdit()
        Me.lblFechaVence = New System.Windows.Forms.Label()
        Me.lblLote = New System.Windows.Forms.Label()
        Me.lblCantidadAnterior = New System.Windows.Forms.Label()
        Me.lblPesoAnterior = New System.Windows.Forms.Label()
        Me.txtProducto = New DevExpress.XtraEditors.TextEdit()
        Me.txtUbicacion = New DevExpress.XtraEditors.TextEdit()
        Me.txtLote = New DevExpress.XtraEditors.TextEdit()
        Me.txtCantidadAnterior = New System.Windows.Forms.NumericUpDown()
        Me.txtPesoAnterior = New System.Windows.Forms.NumericUpDown()
        Me.grpReconteo = New DevExpress.XtraEditors.GroupControl()
        Me.cmbOperador = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblOperador = New System.Windows.Forms.Label()
        Me.txtLicencia = New DevExpress.XtraEditors.TextEdit()
        Me.lblLicPlate = New System.Windows.Forms.Label()
        Me.txtCantidadStock = New System.Windows.Forms.NumericUpDown()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbPresentacion = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbEstadoProducto = New DevExpress.XtraEditors.LookUpEdit()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFechaVence.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFechaVence.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtProducto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUbicacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCantidadAnterior, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPesoAnterior, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpReconteo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpReconteo.SuspendLayout()
        CType(Me.cmbOperador.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLicencia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCantidadStock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPresentacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbEstadoProducto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.cmdGuardar, Me.cmdEliminar})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 3
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1010, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'cmdGuardar
        '
        Me.cmdGuardar.Caption = "Actualizar"
        Me.cmdGuardar.Id = 1
        Me.cmdGuardar.ImageOptions.SvgImage = CType(resources.GetObject("cmdGuardar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdGuardar.Name = "cmdGuardar"
        '
        'cmdEliminar
        '
        Me.cmdEliminar.Caption = "Eliminar conteo"
        Me.cmdEliminar.Id = 2
        Me.cmdEliminar.ImageOptions.SvgImage = CType(resources.GetObject("cmdEliminar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdEliminar.Name = "cmdEliminar"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Conteo"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdGuardar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdEliminar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 515)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1010, 30)
        '
        'lblCodigoEnc
        '
        Me.lblCodigoEnc.AutoSize = True
        Me.lblCodigoEnc.Location = New System.Drawing.Point(25, 96)
        Me.lblCodigoEnc.Name = "lblCodigoEnc"
        Me.lblCodigoEnc.Size = New System.Drawing.Size(69, 16)
        Me.lblCodigoEnc.TabIndex = 0
        Me.lblCodigoEnc.Text = "Inventario:"
        '
        'lblCodigo
        '
        Me.lblCodigo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCodigo.Location = New System.Drawing.Point(149, 96)
        Me.lblCodigo.Name = "lblCodigo"
        Me.lblCodigo.Size = New System.Drawing.Size(309, 21)
        Me.lblCodigo.TabIndex = 1
        Me.lblCodigo.Text = "---"
        '
        'lblId
        '
        Me.lblId.AutoSize = True
        Me.lblId.Location = New System.Drawing.Point(25, 54)
        Me.lblId.Name = "lblId"
        Me.lblId.Size = New System.Drawing.Size(54, 16)
        Me.lblId.TabIndex = 2
        Me.lblId.Text = "IdStock:"
        '
        'lblCodReconteo
        '
        Me.lblCodReconteo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCodReconteo.Location = New System.Drawing.Point(149, 54)
        Me.lblCodReconteo.Name = "lblCodReconteo"
        Me.lblCodReconteo.Size = New System.Drawing.Size(309, 21)
        Me.lblCodReconteo.TabIndex = 3
        Me.lblCodReconteo.Text = "---"
        '
        'lblProducto
        '
        Me.lblProducto.AutoSize = True
        Me.lblProducto.Location = New System.Drawing.Point(25, 134)
        Me.lblProducto.Name = "lblProducto"
        Me.lblProducto.Size = New System.Drawing.Size(62, 16)
        Me.lblProducto.TabIndex = 12
        Me.lblProducto.Text = "Producto:"
        '
        'lblEstadoProducto
        '
        Me.lblEstadoProducto.AutoSize = True
        Me.lblEstadoProducto.Location = New System.Drawing.Point(25, 208)
        Me.lblEstadoProducto.Name = "lblEstadoProducto"
        Me.lblEstadoProducto.Size = New System.Drawing.Size(104, 16)
        Me.lblEstadoProducto.TabIndex = 13
        Me.lblEstadoProducto.Text = "Estado Producto:"
        '
        'lblPresentacion
        '
        Me.lblPresentacion.AutoSize = True
        Me.lblPresentacion.Location = New System.Drawing.Point(25, 250)
        Me.lblPresentacion.Name = "lblPresentacion"
        Me.lblPresentacion.Size = New System.Drawing.Size(85, 16)
        Me.lblPresentacion.TabIndex = 14
        Me.lblPresentacion.Text = "Presentación:"
        '
        'lblUbicacion
        '
        Me.lblUbicacion.AutoSize = True
        Me.lblUbicacion.Location = New System.Drawing.Point(534, 57)
        Me.lblUbicacion.Name = "lblUbicacion"
        Me.lblUbicacion.Size = New System.Drawing.Size(66, 16)
        Me.lblUbicacion.TabIndex = 15
        Me.lblUbicacion.Text = "Ubicación:"
        '
        'dtpFechaVence
        '
        Me.dtpFechaVence.EditValue = New Date(2018, 3, 7, 10, 34, 56, 981)
        Me.dtpFechaVence.Location = New System.Drawing.Point(658, 93)
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
        Me.lblFechaVence.Location = New System.Drawing.Point(534, 96)
        Me.lblFechaVence.Name = "lblFechaVence"
        Me.lblFechaVence.Size = New System.Drawing.Size(85, 16)
        Me.lblFechaVence.TabIndex = 17
        Me.lblFechaVence.Text = "Fecha Vence:"
        '
        'lblLote
        '
        Me.lblLote.AutoSize = True
        Me.lblLote.Location = New System.Drawing.Point(534, 138)
        Me.lblLote.Name = "lblLote"
        Me.lblLote.Size = New System.Drawing.Size(36, 16)
        Me.lblLote.TabIndex = 18
        Me.lblLote.Text = "Lote:"
        '
        'lblCantidadAnterior
        '
        Me.lblCantidadAnterior.AutoSize = True
        Me.lblCantidadAnterior.Location = New System.Drawing.Point(534, 206)
        Me.lblCantidadAnterior.Name = "lblCantidadAnterior"
        Me.lblCantidadAnterior.Size = New System.Drawing.Size(62, 16)
        Me.lblCantidadAnterior.TabIndex = 19
        Me.lblCantidadAnterior.Text = "Cantidad:"
        '
        'lblPesoAnterior
        '
        Me.lblPesoAnterior.AutoSize = True
        Me.lblPesoAnterior.Location = New System.Drawing.Point(534, 246)
        Me.lblPesoAnterior.Name = "lblPesoAnterior"
        Me.lblPesoAnterior.Size = New System.Drawing.Size(39, 16)
        Me.lblPesoAnterior.TabIndex = 20
        Me.lblPesoAnterior.Text = "Peso:"
        '
        'txtProducto
        '
        Me.txtProducto.Location = New System.Drawing.Point(149, 134)
        Me.txtProducto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtProducto.MenuManager = Me.RibbonControl
        Me.txtProducto.Name = "txtProducto"
        Me.txtProducto.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtProducto.Properties.ReadOnly = True
        Me.txtProducto.Size = New System.Drawing.Size(309, 22)
        Me.txtProducto.TabIndex = 23
        '
        'txtUbicacion
        '
        Me.txtUbicacion.Location = New System.Drawing.Point(658, 48)
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
        Me.txtLote.Location = New System.Drawing.Point(658, 130)
        Me.txtLote.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtLote.MenuManager = Me.RibbonControl
        Me.txtLote.Name = "txtLote"
        Me.txtLote.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtLote.Size = New System.Drawing.Size(309, 22)
        Me.txtLote.TabIndex = 27
        '
        'txtCantidadAnterior
        '
        Me.txtCantidadAnterior.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCantidadAnterior.DecimalPlaces = 6
        Me.txtCantidadAnterior.Location = New System.Drawing.Point(658, 203)
        Me.txtCantidadAnterior.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCantidadAnterior.Maximum = New Decimal(New Integer() {1569325056, 23283064, 0, 0})
        Me.txtCantidadAnterior.Name = "txtCantidadAnterior"
        Me.txtCantidadAnterior.Size = New System.Drawing.Size(309, 23)
        Me.txtCantidadAnterior.TabIndex = 28
        '
        'txtPesoAnterior
        '
        Me.txtPesoAnterior.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPesoAnterior.DecimalPlaces = 6
        Me.txtPesoAnterior.Location = New System.Drawing.Point(658, 244)
        Me.txtPesoAnterior.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtPesoAnterior.Maximum = New Decimal(New Integer() {1569325056, 23283064, 0, 0})
        Me.txtPesoAnterior.Name = "txtPesoAnterior"
        Me.txtPesoAnterior.Size = New System.Drawing.Size(309, 23)
        Me.txtPesoAnterior.TabIndex = 30
        '
        'grpReconteo
        '
        Me.grpReconteo.Controls.Add(Me.cmbOperador)
        Me.grpReconteo.Controls.Add(Me.lblOperador)
        Me.grpReconteo.Controls.Add(Me.txtLicencia)
        Me.grpReconteo.Controls.Add(Me.lblLicPlate)
        Me.grpReconteo.Controls.Add(Me.txtCantidadStock)
        Me.grpReconteo.Controls.Add(Me.Label1)
        Me.grpReconteo.Controls.Add(Me.cmbPresentacion)
        Me.grpReconteo.Controls.Add(Me.cmbEstadoProducto)
        Me.grpReconteo.Controls.Add(Me.txtPesoAnterior)
        Me.grpReconteo.Controls.Add(Me.txtCantidadAnterior)
        Me.grpReconteo.Controls.Add(Me.txtLote)
        Me.grpReconteo.Controls.Add(Me.txtUbicacion)
        Me.grpReconteo.Controls.Add(Me.txtProducto)
        Me.grpReconteo.Controls.Add(Me.lblPesoAnterior)
        Me.grpReconteo.Controls.Add(Me.lblCantidadAnterior)
        Me.grpReconteo.Controls.Add(Me.lblLote)
        Me.grpReconteo.Controls.Add(Me.lblFechaVence)
        Me.grpReconteo.Controls.Add(Me.dtpFechaVence)
        Me.grpReconteo.Controls.Add(Me.lblUbicacion)
        Me.grpReconteo.Controls.Add(Me.lblPresentacion)
        Me.grpReconteo.Controls.Add(Me.lblEstadoProducto)
        Me.grpReconteo.Controls.Add(Me.lblProducto)
        Me.grpReconteo.Controls.Add(Me.lblCodReconteo)
        Me.grpReconteo.Controls.Add(Me.lblId)
        Me.grpReconteo.Controls.Add(Me.lblCodigo)
        Me.grpReconteo.Controls.Add(Me.lblCodigoEnc)
        Me.grpReconteo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpReconteo.Location = New System.Drawing.Point(0, 193)
        Me.grpReconteo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grpReconteo.Name = "grpReconteo"
        Me.grpReconteo.Size = New System.Drawing.Size(1010, 322)
        Me.grpReconteo.TabIndex = 2
        '
        'cmbOperador
        '
        Me.cmbOperador.Location = New System.Drawing.Point(149, 169)
        Me.cmbOperador.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbOperador.MenuManager = Me.RibbonControl
        Me.cmbOperador.Name = "cmbOperador"
        Me.cmbOperador.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbOperador.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbOperador.Properties.NullText = ""
        Me.cmbOperador.Size = New System.Drawing.Size(309, 22)
        Me.cmbOperador.TabIndex = 38
        '
        'lblOperador
        '
        Me.lblOperador.AutoSize = True
        Me.lblOperador.Location = New System.Drawing.Point(25, 173)
        Me.lblOperador.Name = "lblOperador"
        Me.lblOperador.Size = New System.Drawing.Size(66, 16)
        Me.lblOperador.TabIndex = 37
        Me.lblOperador.Text = "Operador:"
        '
        'txtLicencia
        '
        Me.txtLicencia.Location = New System.Drawing.Point(658, 279)
        Me.txtLicencia.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtLicencia.MenuManager = Me.RibbonControl
        Me.txtLicencia.Name = "txtLicencia"
        Me.txtLicencia.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtLicencia.Properties.ReadOnly = True
        Me.txtLicencia.Size = New System.Drawing.Size(309, 22)
        Me.txtLicencia.TabIndex = 36
        '
        'lblLicPlate
        '
        Me.lblLicPlate.AutoSize = True
        Me.lblLicPlate.Location = New System.Drawing.Point(534, 279)
        Me.lblLicPlate.Name = "lblLicPlate"
        Me.lblLicPlate.Size = New System.Drawing.Size(57, 16)
        Me.lblLicPlate.TabIndex = 35
        Me.lblLicPlate.Text = "Licencia:"
        '
        'txtCantidadStock
        '
        Me.txtCantidadStock.DecimalPlaces = 6
        Me.txtCantidadStock.Enabled = False
        Me.txtCantidadStock.Location = New System.Drawing.Point(658, 168)
        Me.txtCantidadStock.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCantidadStock.Maximum = New Decimal(New Integer() {1569325056, 23283064, 0, 0})
        Me.txtCantidadStock.Name = "txtCantidadStock"
        Me.txtCantidadStock.Size = New System.Drawing.Size(309, 23)
        Me.txtCantidadStock.TabIndex = 34
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(534, 170)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 16)
        Me.Label1.TabIndex = 33
        Me.Label1.Text = "Stock:"
        '
        'cmbPresentacion
        '
        Me.cmbPresentacion.Location = New System.Drawing.Point(149, 241)
        Me.cmbPresentacion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbPresentacion.MenuManager = Me.RibbonControl
        Me.cmbPresentacion.Name = "cmbPresentacion"
        Me.cmbPresentacion.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbPresentacion.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPresentacion.Properties.NullText = ""
        Me.cmbPresentacion.Properties.ReadOnly = True
        Me.cmbPresentacion.Size = New System.Drawing.Size(309, 22)
        Me.cmbPresentacion.TabIndex = 32
        '
        'cmbEstadoProducto
        '
        Me.cmbEstadoProducto.Location = New System.Drawing.Point(149, 204)
        Me.cmbEstadoProducto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbEstadoProducto.MenuManager = Me.RibbonControl
        Me.cmbEstadoProducto.Name = "cmbEstadoProducto"
        Me.cmbEstadoProducto.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbEstadoProducto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbEstadoProducto.Properties.NullText = ""
        Me.cmbEstadoProducto.Size = New System.Drawing.Size(309, 22)
        Me.cmbEstadoProducto.TabIndex = 31
        '
        'frmConteo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1010, 545)
        Me.Controls.Add(Me.grpReconteo)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmConteo"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Conteo"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFechaVence.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFechaVence.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtProducto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUbicacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCantidadAnterior, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPesoAnterior, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpReconteo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpReconteo.ResumeLayout(False)
        Me.grpReconteo.PerformLayout()
        CType(Me.cmbOperador.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLicencia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCantidadStock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPresentacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbEstadoProducto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout

End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents cmdGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lblCodigoEnc As Label
    Friend WithEvents lblCodigo As Label
    Friend WithEvents lblId As Label
    Friend WithEvents lblCodReconteo As Label
    Friend WithEvents lblProducto As Label
    Friend WithEvents lblEstadoProducto As Label
    Friend WithEvents lblPresentacion As Label
    Friend WithEvents lblUbicacion As Label
    Friend WithEvents dtpFechaVence As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lblFechaVence As Label
    Friend WithEvents lblLote As Label
    Friend WithEvents lblCantidadAnterior As Label
    Friend WithEvents lblPesoAnterior As Label
    Friend WithEvents txtProducto As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtUbicacion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtLote As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCantidadAnterior As NumericUpDown
    Friend WithEvents txtPesoAnterior As NumericUpDown
    Friend WithEvents grpReconteo As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cmbPresentacion As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbEstadoProducto As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmdEliminar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents txtCantidadStock As NumericUpDown
    Friend WithEvents Label1 As Label
    Friend WithEvents txtLicencia As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblLicPlate As Label
    Friend WithEvents cmbOperador As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblOperador As Label
End Class
