<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmInventarioDet
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInventarioDet))
        Me.RepositoryItemButtonEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdVerificar = New DevExpress.XtraBars.BarButtonItem()
        Me.BarEditItem1 = New DevExpress.XtraBars.BarEditItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.grpConteo = New DevExpress.XtraEditors.GroupControl()
        Me.txtLicencia = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbProducto = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.txtNomUbicacion = New System.Windows.Forms.TextBox()
        Me.txtIdUbicacion = New System.Windows.Forms.TextBox()
        Me.txtCantidad = New System.Windows.Forms.NumericUpDown()
        Me.lblCantidad = New System.Windows.Forms.Label()
        Me.cmbEstado = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblPEstado = New System.Windows.Forms.Label()
        Me.dtFechaVence = New DevExpress.XtraEditors.DateEdit()
        Me.lblFechaVence = New System.Windows.Forms.Label()
        Me.txtLote = New System.Windows.Forms.TextBox()
        Me.lblLote = New System.Windows.Forms.Label()
        Me.lblUM = New System.Windows.Forms.Label()
        Me.cmbUM = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbPresentacion = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblPresetacion = New System.Windows.Forms.Label()
        Me.lblProducto = New System.Windows.Forms.Label()
        Me.lblUbicacion = New System.Windows.Forms.Label()
        Me.lblCodInv = New System.Windows.Forms.Label()
        Me.lblCodigoInv = New System.Windows.Forms.Label()
        CType(Me.RepositoryItemButtonEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpConteo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpConteo.SuspendLayout()
        CType(Me.cmbProducto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCantidad, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbEstado.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtFechaVence.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtFechaVence.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbUM.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPresentacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RepositoryItemButtonEdit1
        '
        Me.RepositoryItemButtonEdit1.AutoHeight = False
        Me.RepositoryItemButtonEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.RepositoryItemButtonEdit1.Name = "RepositoryItemButtonEdit1"
        '
        'RibbonControl
        '
        Me.RibbonControl.EmptyAreaImageOptions.ImagePadding = New System.Windows.Forms.Padding(26, 24, 26, 24)
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuActualizar, Me.mnuEliminar, Me.cmdVerificar, Me.BarEditItem1})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 5
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.OptionsMenuMinWidth = 283
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(715, 158)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'mnuActualizar
        '
        Me.mnuActualizar.Caption = "Guardar"
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
        Me.cmdVerificar.ImageOptions.Image = CType(resources.GetObject("cmdVerificar.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdVerificar.ImageOptions.LargeImage = CType(resources.GetObject("cmdVerificar.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.cmdVerificar.Name = "cmdVerificar"
        '
        'BarEditItem1
        '
        Me.BarEditItem1.Caption = "BarEditItem1"
        Me.BarEditItem1.Edit = Me.RepositoryItemButtonEdit1
        Me.BarEditItem1.Id = 4
        Me.BarEditItem1.Name = "BarEditItem1"
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
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 528)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(715, 24)
        '
        'grpConteo
        '
        Me.grpConteo.Controls.Add(Me.txtLicencia)
        Me.grpConteo.Controls.Add(Me.Label1)
        Me.grpConteo.Controls.Add(Me.cmbProducto)
        Me.grpConteo.Controls.Add(Me.txtNomUbicacion)
        Me.grpConteo.Controls.Add(Me.txtIdUbicacion)
        Me.grpConteo.Controls.Add(Me.txtCantidad)
        Me.grpConteo.Controls.Add(Me.lblCantidad)
        Me.grpConteo.Controls.Add(Me.cmbEstado)
        Me.grpConteo.Controls.Add(Me.lblPEstado)
        Me.grpConteo.Controls.Add(Me.dtFechaVence)
        Me.grpConteo.Controls.Add(Me.lblFechaVence)
        Me.grpConteo.Controls.Add(Me.txtLote)
        Me.grpConteo.Controls.Add(Me.lblLote)
        Me.grpConteo.Controls.Add(Me.lblUM)
        Me.grpConteo.Controls.Add(Me.cmbUM)
        Me.grpConteo.Controls.Add(Me.cmbPresentacion)
        Me.grpConteo.Controls.Add(Me.lblPresetacion)
        Me.grpConteo.Controls.Add(Me.lblProducto)
        Me.grpConteo.Controls.Add(Me.lblUbicacion)
        Me.grpConteo.Controls.Add(Me.lblCodInv)
        Me.grpConteo.Controls.Add(Me.lblCodigoInv)
        Me.grpConteo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpConteo.Location = New System.Drawing.Point(0, 158)
        Me.grpConteo.Name = "grpConteo"
        Me.grpConteo.Size = New System.Drawing.Size(715, 370)
        Me.grpConteo.TabIndex = 0
        '
        'txtLicencia
        '
        Me.txtLicencia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLicencia.Location = New System.Drawing.Point(135, 148)
        Me.txtLicencia.Name = "txtLicencia"
        Me.txtLicencia.Size = New System.Drawing.Size(228, 21)
        Me.txtLicencia.TabIndex = 33
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(27, 148)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 13)
        Me.Label1.TabIndex = 32
        Me.Label1.Text = "Licencia:"
        '
        'cmbProducto
        '
        Me.cmbProducto.Location = New System.Drawing.Point(135, 64)
        Me.cmbProducto.MenuManager = Me.RibbonControl
        Me.cmbProducto.Name = "cmbProducto"
        Me.cmbProducto.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[True]
        Me.cmbProducto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbProducto.Properties.NullText = ""
        Me.cmbProducto.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.cmbProducto.Size = New System.Drawing.Size(480, 20)
        Me.cmbProducto.TabIndex = 31
        '
        'txtNomUbicacion
        '
        Me.txtNomUbicacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNomUbicacion.Location = New System.Drawing.Point(135, 235)
        Me.txtNomUbicacion.Name = "txtNomUbicacion"
        Me.txtNomUbicacion.ReadOnly = True
        Me.txtNomUbicacion.Size = New System.Drawing.Size(228, 21)
        Me.txtNomUbicacion.TabIndex = 30
        '
        'txtIdUbicacion
        '
        Me.txtIdUbicacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIdUbicacion.Location = New System.Drawing.Point(135, 206)
        Me.txtIdUbicacion.Name = "txtIdUbicacion"
        Me.txtIdUbicacion.Size = New System.Drawing.Size(228, 21)
        Me.txtIdUbicacion.TabIndex = 27
        '
        'txtCantidad
        '
        Me.txtCantidad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCantidad.DecimalPlaces = 6
        Me.txtCantidad.Location = New System.Drawing.Point(135, 320)
        Me.txtCantidad.Maximum = New Decimal(New Integer() {100000000, 0, 0, 0})
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.Size = New System.Drawing.Size(228, 21)
        Me.txtCantidad.TabIndex = 23
        '
        'lblCantidad
        '
        Me.lblCantidad.AutoSize = True
        Me.lblCantidad.Location = New System.Drawing.Point(27, 320)
        Me.lblCantidad.Name = "lblCantidad"
        Me.lblCantidad.Size = New System.Drawing.Size(54, 13)
        Me.lblCantidad.TabIndex = 22
        Me.lblCantidad.Text = "Cantidad:"
        '
        'cmbEstado
        '
        Me.cmbEstado.Location = New System.Drawing.Point(135, 292)
        Me.cmbEstado.MenuManager = Me.RibbonControl
        Me.cmbEstado.Name = "cmbEstado"
        Me.cmbEstado.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbEstado.Properties.NullText = ""
        Me.cmbEstado.Size = New System.Drawing.Size(228, 20)
        Me.cmbEstado.TabIndex = 21
        '
        'lblPEstado
        '
        Me.lblPEstado.AutoSize = True
        Me.lblPEstado.Location = New System.Drawing.Point(27, 292)
        Me.lblPEstado.Name = "lblPEstado"
        Me.lblPEstado.Size = New System.Drawing.Size(90, 13)
        Me.lblPEstado.TabIndex = 20
        Me.lblPEstado.Text = "Producto Estado:"
        '
        'dtFechaVence
        '
        Me.dtFechaVence.EditValue = New Date(2018, 1, 18, 12, 37, 13, 0)
        Me.dtFechaVence.Location = New System.Drawing.Point(135, 264)
        Me.dtFechaVence.MenuManager = Me.RibbonControl
        Me.dtFechaVence.Name = "dtFechaVence"
        Me.dtFechaVence.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtFechaVence.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtFechaVence.Size = New System.Drawing.Size(228, 20)
        Me.dtFechaVence.TabIndex = 19
        '
        'lblFechaVence
        '
        Me.lblFechaVence.AutoSize = True
        Me.lblFechaVence.Location = New System.Drawing.Point(27, 264)
        Me.lblFechaVence.Name = "lblFechaVence"
        Me.lblFechaVence.Size = New System.Drawing.Size(72, 13)
        Me.lblFechaVence.TabIndex = 18
        Me.lblFechaVence.Text = "Fecha Vence:"
        '
        'txtLote
        '
        Me.txtLote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLote.Location = New System.Drawing.Point(135, 177)
        Me.txtLote.Name = "txtLote"
        Me.txtLote.Size = New System.Drawing.Size(228, 21)
        Me.txtLote.TabIndex = 17
        '
        'lblLote
        '
        Me.lblLote.AutoSize = True
        Me.lblLote.Location = New System.Drawing.Point(27, 177)
        Me.lblLote.Name = "lblLote"
        Me.lblLote.Size = New System.Drawing.Size(32, 13)
        Me.lblLote.TabIndex = 16
        Me.lblLote.Text = "Lote:"
        '
        'lblUM
        '
        Me.lblUM.AutoSize = True
        Me.lblUM.Location = New System.Drawing.Point(27, 117)
        Me.lblUM.Name = "lblUM"
        Me.lblUM.Size = New System.Drawing.Size(81, 13)
        Me.lblUM.TabIndex = 14
        Me.lblUM.Text = "Unidad Medida:"
        '
        'cmbUM
        '
        Me.cmbUM.Location = New System.Drawing.Point(135, 117)
        Me.cmbUM.MenuManager = Me.RibbonControl
        Me.cmbUM.Name = "cmbUM"
        Me.cmbUM.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbUM.Properties.NullText = ""
        Me.cmbUM.Properties.ReadOnly = True
        Me.cmbUM.Size = New System.Drawing.Size(228, 20)
        Me.cmbUM.TabIndex = 15
        '
        'cmbPresentacion
        '
        Me.cmbPresentacion.Location = New System.Drawing.Point(135, 89)
        Me.cmbPresentacion.MenuManager = Me.RibbonControl
        Me.cmbPresentacion.Name = "cmbPresentacion"
        Me.cmbPresentacion.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[True]
        Me.cmbPresentacion.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPresentacion.Properties.NullText = ""
        Me.cmbPresentacion.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.cmbPresentacion.Size = New System.Drawing.Size(228, 20)
        Me.cmbPresentacion.TabIndex = 13
        '
        'lblPresetacion
        '
        Me.lblPresetacion.AutoSize = True
        Me.lblPresetacion.Location = New System.Drawing.Point(27, 89)
        Me.lblPresetacion.Name = "lblPresetacion"
        Me.lblPresetacion.Size = New System.Drawing.Size(73, 13)
        Me.lblPresetacion.TabIndex = 12
        Me.lblPresetacion.Text = "Presentación:"
        '
        'lblProducto
        '
        Me.lblProducto.AutoSize = True
        Me.lblProducto.Location = New System.Drawing.Point(27, 64)
        Me.lblProducto.Name = "lblProducto"
        Me.lblProducto.Size = New System.Drawing.Size(54, 13)
        Me.lblProducto.TabIndex = 10
        Me.lblProducto.Text = "Producto:"
        '
        'lblUbicacion
        '
        Me.lblUbicacion.AutoSize = True
        Me.lblUbicacion.Location = New System.Drawing.Point(27, 206)
        Me.lblUbicacion.Name = "lblUbicacion"
        Me.lblUbicacion.Size = New System.Drawing.Size(56, 13)
        Me.lblUbicacion.TabIndex = 6
        Me.lblUbicacion.Text = "Ubicación:"
        '
        'lblCodInv
        '
        Me.lblCodInv.AutoSize = True
        Me.lblCodInv.Location = New System.Drawing.Point(135, 44)
        Me.lblCodInv.Name = "lblCodInv"
        Me.lblCodInv.Size = New System.Drawing.Size(15, 13)
        Me.lblCodInv.TabIndex = 3
        Me.lblCodInv.Text = "--"
        '
        'lblCodigoInv
        '
        Me.lblCodigoInv.AutoSize = True
        Me.lblCodigoInv.Location = New System.Drawing.Point(27, 44)
        Me.lblCodigoInv.Name = "lblCodigoInv"
        Me.lblCodigoInv.Size = New System.Drawing.Size(61, 13)
        Me.lblCodigoInv.TabIndex = 2
        Me.lblCodigoInv.Text = "Inventario:"
        '
        'frmInventarioDet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(715, 552)
        Me.Controls.Add(Me.grpConteo)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frmInventarioDet"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Detalle de Inventario"
        CType(Me.RepositoryItemButtonEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpConteo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpConteo.ResumeLayout(False)
        Me.grpConteo.PerformLayout()
        CType(Me.cmbProducto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCantidad, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbEstado.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtFechaVence.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtFechaVence.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbUM.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPresentacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

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
    Friend WithEvents lblUbicacion As Label
    Friend WithEvents lblProducto As Label
    Friend WithEvents lblPresetacion As Label
    Friend WithEvents cmbPresentacion As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents txtLote As TextBox
    Friend WithEvents lblLote As Label
    Friend WithEvents lblUM As Label
    Friend WithEvents cmbUM As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbEstado As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblPEstado As Label
    Friend WithEvents dtFechaVence As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lblFechaVence As Label
    Friend WithEvents txtCantidad As NumericUpDown
    Friend WithEvents lblCantidad As Label
    Friend WithEvents cmdVerificar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents txtIdUbicacion As TextBox
    Friend WithEvents txtNomUbicacion As TextBox
    Friend WithEvents txtLicencia As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents cmbProducto As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents BarEditItem1 As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemButtonEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
End Class
