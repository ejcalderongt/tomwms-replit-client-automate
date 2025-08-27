<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTicketN
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTicketN))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuImprimir = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupControl5 = New DevExpress.XtraEditors.GroupControl()
        Me.lblFechaEgreso = New DevExpress.XtraEditors.LabelControl()
        Me.lblEgreso = New DevExpress.XtraEditors.LabelControl()
        Me.lblFechaIngreso = New DevExpress.XtraEditors.LabelControl()
        Me.lblIngreso = New DevExpress.XtraEditors.LabelControl()
        Me.GroupControl4 = New DevExpress.XtraEditors.GroupControl()
        Me.lblTicketNo = New DevExpress.XtraEditors.LabelControl()
        Me.txtNoTicket = New DevExpress.XtraEditors.TextEdit()
        Me.lblBodega = New DevExpress.XtraEditors.LabelControl()
        Me.cmbBodega = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblEstado = New DevExpress.XtraEditors.LabelControl()
        Me.lblPropietario = New DevExpress.XtraEditors.LabelControl()
        Me.cmbPropietario = New DevExpress.XtraEditors.LookUpEdit()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.lcPoliza = New DevExpress.XtraEditors.LabelControl()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.mnuIncidenciaTransporte = New System.Windows.Forms.ToolStripButton()
        Me.mnuInspeccionHH = New System.Windows.Forms.ToolStripButton()
        Me.mnuHistorialTransporte = New System.Windows.Forms.ToolStripButton()
        Me.chkOperacion = New DevExpress.XtraEditors.ToggleSwitch()
        Me.lblOperacion = New DevExpress.XtraEditors.LabelControl()
        Me.txtObservacion = New System.Windows.Forms.RichTextBox()
        Me.lblEmpresaTransporte = New DevExpress.XtraEditors.LabelControl()
        Me.lblTipoTransporte = New DevExpress.XtraEditors.LabelControl()
        Me.lblNoTC = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.lblNoPlaca = New DevExpress.XtraEditors.LabelControl()
        Me.cmbTipoTransporte = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbEmpresaTransporte = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtNoTC = New DevExpress.XtraEditors.TextEdit()
        Me.txtNoPoliza = New DevExpress.XtraEditors.TextEdit()
        Me.txtNoPlaca = New DevExpress.XtraEditors.TextEdit()
        Me.grpPiloto = New DevExpress.XtraEditors.GroupControl()
        Me.lblStatusLicencia = New System.Windows.Forms.RichTextBox()
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
        Me.mnuIncidenciaPiloto = New System.Windows.Forms.ToolStripButton()
        Me.mnuHistorialPiloto = New System.Windows.Forms.ToolStripButton()
        Me.txtTipoLicencia = New DevExpress.XtraEditors.LabelControl()
        Me.lblTipoLicencia = New DevExpress.XtraEditors.LabelControl()
        Me.lblFechaVenceDocumentoPiloto = New DevExpress.XtraEditors.LabelControl()
        Me.lblNoDocumentoPiloto = New DevExpress.XtraEditors.LabelControl()
        Me.lblApellidos = New DevExpress.XtraEditors.LabelControl()
        Me.lblNombres = New DevExpress.XtraEditors.LabelControl()
        Me.dtpFechaVencePiloto = New DevExpress.XtraEditors.DateEdit()
        Me.txtApellidosPiloto = New DevExpress.XtraEditors.TextEdit()
        Me.txtNombresPiloto = New DevExpress.XtraEditors.TextEdit()
        Me.txtNoDocumentoPiloto = New DevExpress.XtraEditors.TextEdit()
        Me.UnboundSource1 = New DevExpress.Data.UnboundSource(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Bar1 = New DevExpress.XtraBars.Bar()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl5.SuspendLayout()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl4.SuspendLayout()
        CType(Me.txtNoTicket.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.chkOperacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTipoTransporte.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbEmpresaTransporte.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoTC.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoPoliza.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoPlaca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpPiloto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpPiloto.SuspendLayout()
        Me.ToolStrip2.SuspendLayout()
        CType(Me.dtpFechaVencePiloto.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFechaVencePiloto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtApellidosPiloto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombresPiloto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoDocumentoPiloto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UnboundSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.RibbonControl.SearchEditItem, Me.mnuGuardar, Me.mnuActualizar, Me.mnuEliminar, Me.mnuImprimir})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 5
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.Size = New System.Drawing.Size(1517, 193)
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
        'mnuImprimir
        '
        Me.mnuImprimir.Caption = "Imprimir"
        Me.mnuImprimir.Id = 4
        Me.mnuImprimir.ImageOptions.SvgImage = CType(resources.GetObject("mnuImprimir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuImprimir.Name = "mnuImprimir"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Ticket de transporte"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEliminar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuImprimir)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 897)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1517, 30)
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.GroupControl5, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupControl4, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupControl2, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.grpPiloto, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 193)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 32.91572!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 67.08428!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1517, 704)
        Me.TableLayoutPanel1.TabIndex = 2
        '
        'GroupControl5
        '
        Me.GroupControl5.Controls.Add(Me.lblFechaEgreso)
        Me.GroupControl5.Controls.Add(Me.lblEgreso)
        Me.GroupControl5.Controls.Add(Me.lblFechaIngreso)
        Me.GroupControl5.Controls.Add(Me.lblIngreso)
        Me.GroupControl5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl5.Location = New System.Drawing.Point(761, 3)
        Me.GroupControl5.Name = "GroupControl5"
        Me.GroupControl5.Size = New System.Drawing.Size(753, 225)
        Me.GroupControl5.TabIndex = 3
        Me.GroupControl5.Text = "Tiempos"
        '
        'lblFechaEgreso
        '
        Me.lblFechaEgreso.Appearance.Font = New System.Drawing.Font("Tahoma", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFechaEgreso.Appearance.Options.UseFont = True
        Me.lblFechaEgreso.Appearance.Options.UseTextOptions = True
        Me.lblFechaEgreso.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblFechaEgreso.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblFechaEgreso.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblFechaEgreso.Location = New System.Drawing.Point(2, 184)
        Me.lblFechaEgreso.Name = "lblFechaEgreso"
        Me.lblFechaEgreso.Size = New System.Drawing.Size(749, 61)
        Me.lblFechaEgreso.TabIndex = 3
        Me.lblFechaEgreso.Text = "22/01/2021"
        '
        'lblEgreso
        '
        Me.lblEgreso.Appearance.BackColor = System.Drawing.Color.LightGreen
        Me.lblEgreso.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEgreso.Appearance.Options.UseBackColor = True
        Me.lblEgreso.Appearance.Options.UseFont = True
        Me.lblEgreso.Appearance.Options.UseTextOptions = True
        Me.lblEgreso.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblEgreso.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblEgreso.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblEgreso.Location = New System.Drawing.Point(2, 140)
        Me.lblEgreso.Name = "lblEgreso"
        Me.lblEgreso.Size = New System.Drawing.Size(749, 44)
        Me.lblEgreso.TabIndex = 2
        Me.lblEgreso.Text = "Egresó:"
        '
        'lblFechaIngreso
        '
        Me.lblFechaIngreso.Appearance.Font = New System.Drawing.Font("Tahoma", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFechaIngreso.Appearance.Options.UseFont = True
        Me.lblFechaIngreso.Appearance.Options.UseTextOptions = True
        Me.lblFechaIngreso.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblFechaIngreso.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblFechaIngreso.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblFechaIngreso.Location = New System.Drawing.Point(2, 79)
        Me.lblFechaIngreso.Name = "lblFechaIngreso"
        Me.lblFechaIngreso.Size = New System.Drawing.Size(749, 61)
        Me.lblFechaIngreso.TabIndex = 1
        Me.lblFechaIngreso.Text = "22/01/2021"
        '
        'lblIngreso
        '
        Me.lblIngreso.Appearance.BackColor = System.Drawing.Color.LightCoral
        Me.lblIngreso.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIngreso.Appearance.Options.UseBackColor = True
        Me.lblIngreso.Appearance.Options.UseFont = True
        Me.lblIngreso.Appearance.Options.UseTextOptions = True
        Me.lblIngreso.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblIngreso.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblIngreso.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblIngreso.Location = New System.Drawing.Point(2, 28)
        Me.lblIngreso.Name = "lblIngreso"
        Me.lblIngreso.Size = New System.Drawing.Size(749, 51)
        Me.lblIngreso.TabIndex = 0
        Me.lblIngreso.Text = "Ingresó:"
        '
        'GroupControl4
        '
        Me.GroupControl4.Controls.Add(Me.lblTicketNo)
        Me.GroupControl4.Controls.Add(Me.txtNoTicket)
        Me.GroupControl4.Controls.Add(Me.lblBodega)
        Me.GroupControl4.Controls.Add(Me.cmbBodega)
        Me.GroupControl4.Controls.Add(Me.lblEstado)
        Me.GroupControl4.Controls.Add(Me.lblPropietario)
        Me.GroupControl4.Controls.Add(Me.cmbPropietario)
        Me.GroupControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl4.Location = New System.Drawing.Point(3, 3)
        Me.GroupControl4.Name = "GroupControl4"
        Me.GroupControl4.Size = New System.Drawing.Size(752, 225)
        Me.GroupControl4.TabIndex = 0
        Me.GroupControl4.Text = "Datos del Ticket"
        '
        'lblTicketNo
        '
        Me.lblTicketNo.Appearance.Font = New System.Drawing.Font("Tahoma", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTicketNo.Appearance.Options.UseFont = True
        Me.lblTicketNo.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblTicketNo.Location = New System.Drawing.Point(430, 28)
        Me.lblTicketNo.Name = "lblTicketNo"
        Me.lblTicketNo.Size = New System.Drawing.Size(20, 33)
        Me.lblTicketNo.TabIndex = 0
        Me.lblTicketNo.Text = "#"
        '
        'txtNoTicket
        '
        Me.txtNoTicket.Dock = System.Windows.Forms.DockStyle.Right
        Me.txtNoTicket.EditValue = "0000000001"
        Me.txtNoTicket.Location = New System.Drawing.Point(450, 28)
        Me.txtNoTicket.MenuManager = Me.RibbonControl
        Me.txtNoTicket.Name = "txtNoTicket"
        Me.txtNoTicket.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNoTicket.Properties.Appearance.Options.UseFont = True
        Me.txtNoTicket.Properties.Appearance.Options.UseTextOptions = True
        Me.txtNoTicket.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtNoTicket.Properties.ReadOnly = True
        Me.txtNoTicket.Size = New System.Drawing.Size(300, 40)
        Me.txtNoTicket.TabIndex = 1
        '
        'lblBodega
        '
        Me.lblBodega.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBodega.Appearance.Options.UseFont = True
        Me.lblBodega.Location = New System.Drawing.Point(118, 95)
        Me.lblBodega.Name = "lblBodega"
        Me.lblBodega.Size = New System.Drawing.Size(67, 24)
        Me.lblBodega.TabIndex = 2
        Me.lblBodega.Text = "Bodega"
        '
        'cmbBodega
        '
        Me.cmbBodega.Location = New System.Drawing.Point(230, 82)
        Me.cmbBodega.Name = "cmbBodega"
        Me.cmbBodega.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbBodega.Properties.Appearance.Options.UseFont = True
        Me.cmbBodega.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbBodega.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodega.Size = New System.Drawing.Size(401, 30)
        Me.cmbBodega.TabIndex = 3
        '
        'lblEstado
        '
        Me.lblEstado.Appearance.BackColor = System.Drawing.Color.Yellow
        Me.lblEstado.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEstado.Appearance.Options.UseBackColor = True
        Me.lblEstado.Appearance.Options.UseFont = True
        Me.lblEstado.Appearance.Options.UseTextOptions = True
        Me.lblEstado.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblEstado.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblEstado.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.lblEstado.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblEstado.Location = New System.Drawing.Point(2, 179)
        Me.lblEstado.Name = "lblEstado"
        Me.lblEstado.Size = New System.Drawing.Size(748, 44)
        Me.lblEstado.TabIndex = 6
        Me.lblEstado.Text = "ABIERTO"
        '
        'lblPropietario
        '
        Me.lblPropietario.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPropietario.Appearance.Options.UseFont = True
        Me.lblPropietario.Location = New System.Drawing.Point(118, 136)
        Me.lblPropietario.Name = "lblPropietario"
        Me.lblPropietario.Size = New System.Drawing.Size(97, 24)
        Me.lblPropietario.TabIndex = 4
        Me.lblPropietario.Text = "Propietario"
        '
        'cmbPropietario
        '
        Me.cmbPropietario.Location = New System.Drawing.Point(230, 123)
        Me.cmbPropietario.Name = "cmbPropietario"
        Me.cmbPropietario.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbPropietario.Properties.Appearance.Options.UseFont = True
        Me.cmbPropietario.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbPropietario.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPropietario.Size = New System.Drawing.Size(401, 30)
        Me.cmbPropietario.TabIndex = 5
        '
        'GroupControl2
        '
        Me.GroupControl2.AppearanceCaption.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl2.AppearanceCaption.Options.UseFont = True
        Me.GroupControl2.Controls.Add(Me.lcPoliza)
        Me.GroupControl2.Controls.Add(Me.ToolStrip1)
        Me.GroupControl2.Controls.Add(Me.chkOperacion)
        Me.GroupControl2.Controls.Add(Me.lblOperacion)
        Me.GroupControl2.Controls.Add(Me.txtObservacion)
        Me.GroupControl2.Controls.Add(Me.lblEmpresaTransporte)
        Me.GroupControl2.Controls.Add(Me.lblTipoTransporte)
        Me.GroupControl2.Controls.Add(Me.lblNoTC)
        Me.GroupControl2.Controls.Add(Me.LabelControl1)
        Me.GroupControl2.Controls.Add(Me.lblNoPlaca)
        Me.GroupControl2.Controls.Add(Me.cmbTipoTransporte)
        Me.GroupControl2.Controls.Add(Me.cmbEmpresaTransporte)
        Me.GroupControl2.Controls.Add(Me.txtNoTC)
        Me.GroupControl2.Controls.Add(Me.txtNoPoliza)
        Me.GroupControl2.Controls.Add(Me.txtNoPlaca)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl2.Location = New System.Drawing.Point(761, 234)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(753, 467)
        Me.GroupControl2.TabIndex = 0
        Me.GroupControl2.Text = "Datos del transporte"
        '
        'lcPoliza
        '
        Me.lcPoliza.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lcPoliza.Appearance.Image = CType(resources.GetObject("lcPoliza.Appearance.Image"), System.Drawing.Image)
        Me.lcPoliza.Appearance.Options.UseFont = True
        Me.lcPoliza.Appearance.Options.UseImage = True
        Me.lcPoliza.ImageOptions.Image = CType(resources.GetObject("lcPoliza.ImageOptions.Image"), System.Drawing.Image)
        Me.lcPoliza.Location = New System.Drawing.Point(660, 148)
        Me.lcPoliza.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lcPoliza.Name = "lcPoliza"
        Me.lcPoliza.Size = New System.Drawing.Size(32, 32)
        Me.lcPoliza.TabIndex = 5
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuIncidenciaTransporte, Me.mnuInspeccionHH, Me.mnuHistorialTransporte})
        Me.ToolStrip1.Location = New System.Drawing.Point(2, 28)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(749, 35)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip4"
        '
        'mnuIncidenciaTransporte
        '
        Me.mnuIncidenciaTransporte.Image = CType(resources.GetObject("mnuIncidenciaTransporte.Image"), System.Drawing.Image)
        Me.mnuIncidenciaTransporte.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuIncidenciaTransporte.Name = "mnuIncidenciaTransporte"
        Me.mnuIncidenciaTransporte.Size = New System.Drawing.Size(123, 32)
        Me.mnuIncidenciaTransporte.Text = "Incidencia"
        '
        'mnuInspeccionHH
        '
        Me.mnuInspeccionHH.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Me.mnuInspeccionHH.Image = Global.TOMWMS.My.Resources.Resources.handheld
        Me.mnuInspeccionHH.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuInspeccionHH.Name = "mnuInspeccionHH"
        Me.mnuInspeccionHH.Size = New System.Drawing.Size(219, 32)
        Me.mnuInspeccionHH.Text = "Inspeccionar con handheld"
        '
        'mnuHistorialTransporte
        '
        Me.mnuHistorialTransporte.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mnuHistorialTransporte.Image = Global.TOMWMS.My.Resources.Resources.order
        Me.mnuHistorialTransporte.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuHistorialTransporte.Name = "mnuHistorialTransporte"
        Me.mnuHistorialTransporte.Size = New System.Drawing.Size(92, 32)
        Me.mnuHistorialTransporte.Text = "Historial"
        '
        'chkOperacion
        '
        Me.chkOperacion.EditValue = True
        Me.chkOperacion.Location = New System.Drawing.Point(242, 331)
        Me.chkOperacion.MenuManager = Me.RibbonControl
        Me.chkOperacion.Name = "chkOperacion"
        Me.chkOperacion.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOperacion.Properties.Appearance.Options.UseFont = True
        Me.chkOperacion.Properties.OffText = "Descarga"
        Me.chkOperacion.Properties.OnText = "Carga"
        Me.chkOperacion.Size = New System.Drawing.Size(189, 29)
        Me.chkOperacion.TabIndex = 13
        '
        'lblOperacion
        '
        Me.lblOperacion.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOperacion.Appearance.Options.UseFont = True
        Me.lblOperacion.Location = New System.Drawing.Point(96, 343)
        Me.lblOperacion.Name = "lblOperacion"
        Me.lblOperacion.Size = New System.Drawing.Size(90, 24)
        Me.lblOperacion.TabIndex = 12
        Me.lblOperacion.Text = "Operación"
        '
        'txtObservacion
        '
        Me.txtObservacion.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtObservacion.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.txtObservacion.Font = New System.Drawing.Font("Tahoma", 19.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtObservacion.Location = New System.Drawing.Point(2, 379)
        Me.txtObservacion.Name = "txtObservacion"
        Me.txtObservacion.Size = New System.Drawing.Size(749, 86)
        Me.txtObservacion.TabIndex = 14
        Me.txtObservacion.Text = ""
        '
        'lblEmpresaTransporte
        '
        Me.lblEmpresaTransporte.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmpresaTransporte.Appearance.Options.UseFont = True
        Me.lblEmpresaTransporte.Location = New System.Drawing.Point(96, 292)
        Me.lblEmpresaTransporte.Name = "lblEmpresaTransporte"
        Me.lblEmpresaTransporte.Size = New System.Drawing.Size(77, 24)
        Me.lblEmpresaTransporte.TabIndex = 10
        Me.lblEmpresaTransporte.Text = "Empresa"
        '
        'lblTipoTransporte
        '
        Me.lblTipoTransporte.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTipoTransporte.Appearance.Options.UseFont = True
        Me.lblTipoTransporte.Location = New System.Drawing.Point(96, 247)
        Me.lblTipoTransporte.Name = "lblTipoTransporte"
        Me.lblTipoTransporte.Size = New System.Drawing.Size(39, 24)
        Me.lblTipoTransporte.TabIndex = 8
        Me.lblTipoTransporte.Text = "Tipo"
        '
        'lblNoTC
        '
        Me.lblNoTC.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNoTC.Appearance.Options.UseFont = True
        Me.lblNoTC.Location = New System.Drawing.Point(96, 200)
        Me.lblNoTC.Name = "lblNoTC"
        Me.lblNoTC.Size = New System.Drawing.Size(45, 24)
        Me.lblNoTC.TabIndex = 6
        Me.lblNoTC.Text = "TC #"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Location = New System.Drawing.Point(96, 150)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(73, 24)
        Me.LabelControl1.TabIndex = 3
        Me.LabelControl1.Text = "Poliza #"
        '
        'lblNoPlaca
        '
        Me.lblNoPlaca.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNoPlaca.Appearance.Options.UseFont = True
        Me.lblNoPlaca.Location = New System.Drawing.Point(96, 105)
        Me.lblNoPlaca.Name = "lblNoPlaca"
        Me.lblNoPlaca.Size = New System.Drawing.Size(68, 24)
        Me.lblNoPlaca.TabIndex = 1
        Me.lblNoPlaca.Text = "Placa #"
        '
        'cmbTipoTransporte
        '
        Me.cmbTipoTransporte.Location = New System.Drawing.Point(242, 232)
        Me.cmbTipoTransporte.Name = "cmbTipoTransporte"
        Me.cmbTipoTransporte.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbTipoTransporte.Properties.Appearance.Options.UseFont = True
        Me.cmbTipoTransporte.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbTipoTransporte.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTipoTransporte.Size = New System.Drawing.Size(399, 30)
        Me.cmbTipoTransporte.TabIndex = 9
        '
        'cmbEmpresaTransporte
        '
        Me.cmbEmpresaTransporte.Location = New System.Drawing.Point(242, 280)
        Me.cmbEmpresaTransporte.Name = "cmbEmpresaTransporte"
        Me.cmbEmpresaTransporte.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbEmpresaTransporte.Properties.Appearance.Options.UseFont = True
        Me.cmbEmpresaTransporte.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbEmpresaTransporte.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbEmpresaTransporte.Size = New System.Drawing.Size(399, 30)
        Me.cmbEmpresaTransporte.TabIndex = 11
        '
        'txtNoTC
        '
        Me.txtNoTC.Location = New System.Drawing.Point(242, 185)
        Me.txtNoTC.MenuManager = Me.RibbonControl
        Me.txtNoTC.Name = "txtNoTC"
        Me.txtNoTC.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNoTC.Properties.Appearance.Options.UseFont = True
        Me.txtNoTC.Size = New System.Drawing.Size(399, 30)
        Me.txtNoTC.TabIndex = 7
        '
        'txtNoPoliza
        '
        Me.txtNoPoliza.Location = New System.Drawing.Point(242, 138)
        Me.txtNoPoliza.MenuManager = Me.RibbonControl
        Me.txtNoPoliza.Name = "txtNoPoliza"
        Me.txtNoPoliza.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNoPoliza.Properties.Appearance.Options.UseFont = True
        Me.txtNoPoliza.Size = New System.Drawing.Size(399, 30)
        Me.txtNoPoliza.TabIndex = 4
        '
        'txtNoPlaca
        '
        Me.txtNoPlaca.Location = New System.Drawing.Point(242, 93)
        Me.txtNoPlaca.MenuManager = Me.RibbonControl
        Me.txtNoPlaca.Name = "txtNoPlaca"
        Me.txtNoPlaca.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNoPlaca.Properties.Appearance.Options.UseFont = True
        Me.txtNoPlaca.Size = New System.Drawing.Size(399, 30)
        Me.txtNoPlaca.TabIndex = 2
        '
        'grpPiloto
        '
        Me.grpPiloto.AppearanceCaption.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpPiloto.AppearanceCaption.Options.UseFont = True
        Me.grpPiloto.Controls.Add(Me.lblStatusLicencia)
        Me.grpPiloto.Controls.Add(Me.ToolStrip2)
        Me.grpPiloto.Controls.Add(Me.txtTipoLicencia)
        Me.grpPiloto.Controls.Add(Me.lblTipoLicencia)
        Me.grpPiloto.Controls.Add(Me.lblFechaVenceDocumentoPiloto)
        Me.grpPiloto.Controls.Add(Me.lblNoDocumentoPiloto)
        Me.grpPiloto.Controls.Add(Me.lblApellidos)
        Me.grpPiloto.Controls.Add(Me.lblNombres)
        Me.grpPiloto.Controls.Add(Me.dtpFechaVencePiloto)
        Me.grpPiloto.Controls.Add(Me.txtApellidosPiloto)
        Me.grpPiloto.Controls.Add(Me.txtNombresPiloto)
        Me.grpPiloto.Controls.Add(Me.txtNoDocumentoPiloto)
        Me.grpPiloto.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpPiloto.Location = New System.Drawing.Point(3, 234)
        Me.grpPiloto.Name = "grpPiloto"
        Me.grpPiloto.Size = New System.Drawing.Size(752, 467)
        Me.grpPiloto.TabIndex = 2
        Me.grpPiloto.Text = "Datos del piloto"
        '
        'lblStatusLicencia
        '
        Me.lblStatusLicencia.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lblStatusLicencia.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblStatusLicencia.Font = New System.Drawing.Font("Tahoma", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusLicencia.Location = New System.Drawing.Point(2, 379)
        Me.lblStatusLicencia.Name = "lblStatusLicencia"
        Me.lblStatusLicencia.Size = New System.Drawing.Size(748, 86)
        Me.lblStatusLicencia.TabIndex = 11
        Me.lblStatusLicencia.Text = ""
        '
        'ToolStrip2
        '
        Me.ToolStrip2.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip2.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuIncidenciaPiloto, Me.mnuHistorialPiloto})
        Me.ToolStrip2.Location = New System.Drawing.Point(2, 28)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.Size = New System.Drawing.Size(748, 35)
        Me.ToolStrip2.TabIndex = 0
        Me.ToolStrip2.Text = "ToolStrip4"
        '
        'mnuIncidenciaPiloto
        '
        Me.mnuIncidenciaPiloto.Image = CType(resources.GetObject("mnuIncidenciaPiloto.Image"), System.Drawing.Image)
        Me.mnuIncidenciaPiloto.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuIncidenciaPiloto.Name = "mnuIncidenciaPiloto"
        Me.mnuIncidenciaPiloto.Size = New System.Drawing.Size(123, 32)
        Me.mnuIncidenciaPiloto.Text = "Incidencia"
        '
        'mnuHistorialPiloto
        '
        Me.mnuHistorialPiloto.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mnuHistorialPiloto.Image = Global.TOMWMS.My.Resources.Resources.order
        Me.mnuHistorialPiloto.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuHistorialPiloto.Name = "mnuHistorialPiloto"
        Me.mnuHistorialPiloto.Size = New System.Drawing.Size(92, 32)
        Me.mnuHistorialPiloto.Text = "Historial"
        '
        'txtTipoLicencia
        '
        Me.txtTipoLicencia.Appearance.Font = New System.Drawing.Font("Tahoma", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTipoLicencia.Appearance.ForeColor = System.Drawing.Color.Chocolate
        Me.txtTipoLicencia.Appearance.Options.UseFont = True
        Me.txtTipoLicencia.Appearance.Options.UseForeColor = True
        Me.txtTipoLicencia.Location = New System.Drawing.Point(244, 306)
        Me.txtTipoLicencia.Name = "txtTipoLicencia"
        Me.txtTipoLicencia.Size = New System.Drawing.Size(17, 33)
        Me.txtTipoLicencia.TabIndex = 10
        Me.txtTipoLicencia.Text = "A"
        '
        'lblTipoLicencia
        '
        Me.lblTipoLicencia.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTipoLicencia.Appearance.Options.UseFont = True
        Me.lblTipoLicencia.Location = New System.Drawing.Point(89, 307)
        Me.lblTipoLicencia.Name = "lblTipoLicencia"
        Me.lblTipoLicencia.Size = New System.Drawing.Size(39, 24)
        Me.lblTipoLicencia.TabIndex = 9
        Me.lblTipoLicencia.Text = "Tipo"
        '
        'lblFechaVenceDocumentoPiloto
        '
        Me.lblFechaVenceDocumentoPiloto.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFechaVenceDocumentoPiloto.Appearance.Options.UseFont = True
        Me.lblFechaVenceDocumentoPiloto.Location = New System.Drawing.Point(89, 261)
        Me.lblFechaVenceDocumentoPiloto.Name = "lblFechaVenceDocumentoPiloto"
        Me.lblFechaVenceDocumentoPiloto.Size = New System.Drawing.Size(54, 24)
        Me.lblFechaVenceDocumentoPiloto.TabIndex = 7
        Me.lblFechaVenceDocumentoPiloto.Text = "Vence"
        '
        'lblNoDocumentoPiloto
        '
        Me.lblNoDocumentoPiloto.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNoDocumentoPiloto.Appearance.Options.UseFont = True
        Me.lblNoDocumentoPiloto.Location = New System.Drawing.Point(89, 102)
        Me.lblNoDocumentoPiloto.Name = "lblNoDocumentoPiloto"
        Me.lblNoDocumentoPiloto.Size = New System.Drawing.Size(123, 24)
        Me.lblNoDocumentoPiloto.TabIndex = 1
        Me.lblNoDocumentoPiloto.Text = "Documento #"
        '
        'lblApellidos
        '
        Me.lblApellidos.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblApellidos.Appearance.Options.UseFont = True
        Me.lblApellidos.Location = New System.Drawing.Point(89, 211)
        Me.lblApellidos.Name = "lblApellidos"
        Me.lblApellidos.Size = New System.Drawing.Size(80, 24)
        Me.lblApellidos.TabIndex = 5
        Me.lblApellidos.Text = "Apellidos"
        '
        'lblNombres
        '
        Me.lblNombres.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNombres.Appearance.Options.UseFont = True
        Me.lblNombres.Location = New System.Drawing.Point(89, 152)
        Me.lblNombres.Name = "lblNombres"
        Me.lblNombres.Size = New System.Drawing.Size(79, 24)
        Me.lblNombres.TabIndex = 3
        Me.lblNombres.Text = "Nombres"
        '
        'dtpFechaVencePiloto
        '
        Me.dtpFechaVencePiloto.EditValue = Nothing
        Me.dtpFechaVencePiloto.Location = New System.Drawing.Point(244, 246)
        Me.dtpFechaVencePiloto.MenuManager = Me.RibbonControl
        Me.dtpFechaVencePiloto.Name = "dtpFechaVencePiloto"
        Me.dtpFechaVencePiloto.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFechaVencePiloto.Properties.Appearance.Options.UseFont = True
        Me.dtpFechaVencePiloto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpFechaVencePiloto.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpFechaVencePiloto.Size = New System.Drawing.Size(346, 30)
        Me.dtpFechaVencePiloto.TabIndex = 8
        '
        'txtApellidosPiloto
        '
        Me.txtApellidosPiloto.EditValue = ""
        Me.txtApellidosPiloto.Location = New System.Drawing.Point(244, 194)
        Me.txtApellidosPiloto.MenuManager = Me.RibbonControl
        Me.txtApellidosPiloto.Name = "txtApellidosPiloto"
        Me.txtApellidosPiloto.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtApellidosPiloto.Properties.Appearance.Options.UseFont = True
        Me.txtApellidosPiloto.Size = New System.Drawing.Size(346, 30)
        Me.txtApellidosPiloto.TabIndex = 6
        '
        'txtNombresPiloto
        '
        Me.txtNombresPiloto.EditValue = ""
        Me.txtNombresPiloto.Location = New System.Drawing.Point(244, 142)
        Me.txtNombresPiloto.MenuManager = Me.RibbonControl
        Me.txtNombresPiloto.Name = "txtNombresPiloto"
        Me.txtNombresPiloto.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNombresPiloto.Properties.Appearance.Options.UseFont = True
        Me.txtNombresPiloto.Size = New System.Drawing.Size(346, 30)
        Me.txtNombresPiloto.TabIndex = 4
        '
        'txtNoDocumentoPiloto
        '
        Me.txtNoDocumentoPiloto.EditValue = ""
        Me.txtNoDocumentoPiloto.Location = New System.Drawing.Point(244, 93)
        Me.txtNoDocumentoPiloto.MenuManager = Me.RibbonControl
        Me.txtNoDocumentoPiloto.Name = "txtNoDocumentoPiloto"
        Me.txtNoDocumentoPiloto.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNoDocumentoPiloto.Properties.Appearance.Options.UseFont = True
        Me.txtNoDocumentoPiloto.Size = New System.Drawing.Size(346, 30)
        Me.txtNoDocumentoPiloto.TabIndex = 2
        Me.txtNoDocumentoPiloto.Tag = resources.GetString("txtNoDocumentoPiloto.Tag")
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'Bar1
        '
        Me.Bar1.BarName = "Tools"
        Me.Bar1.DockCol = 0
        Me.Bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar1.Text = "Tools"
        '
        'frmTicketN
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1517, 927)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frmTicketN"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "TMS - Ticket de transporte"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl5.ResumeLayout(False)
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl4.ResumeLayout(False)
        Me.GroupControl4.PerformLayout()
        CType(Me.txtNoTicket.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.GroupControl2.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.chkOperacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTipoTransporte.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbEmpresaTransporte.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoTC.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoPoliza.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoPlaca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpPiloto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpPiloto.ResumeLayout(False)
        Me.grpPiloto.PerformLayout()
        Me.ToolStrip2.ResumeLayout(False)
        Me.ToolStrip2.PerformLayout()
        CType(Me.dtpFechaVencePiloto.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFechaVencePiloto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtApellidosPiloto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombresPiloto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoDocumentoPiloto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UnboundSource1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents mnuGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents grpPiloto As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dtpFechaVencePiloto As DevExpress.XtraEditors.DateEdit
    Friend WithEvents txtApellidosPiloto As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNombresPiloto As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNoDocumentoPiloto As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cmbTipoTransporte As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbEmpresaTransporte As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents txtNoTC As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNoPoliza As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNoPlaca As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GroupControl4 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cmbPropietario As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents mnuActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEliminar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GroupControl5 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblTicketNo As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblPropietario As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblFechaIngreso As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblEmpresaTransporte As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblTipoTransporte As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblNoTC As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblNoPlaca As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblFechaVenceDocumentoPiloto As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblNoDocumentoPiloto As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblApellidos As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblNombres As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblIngreso As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblFechaEgreso As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblEgreso As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblEstado As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtTipoLicencia As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblTipoLicencia As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblOperacion As DevExpress.XtraEditors.LabelControl
    Friend WithEvents UnboundSource1 As DevExpress.Data.UnboundSource
    Friend WithEvents chkOperacion As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents Timer1 As Timer
    Friend WithEvents lblBodega As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbBodega As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents txtObservacion As RichTextBox
    Friend WithEvents txtNoTicket As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Bar1 As DevExpress.XtraBars.Bar
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents mnuIncidenciaTransporte As ToolStripButton
    Friend WithEvents mnuInspeccionHH As ToolStripButton
    Friend WithEvents mnuHistorialTransporte As ToolStripButton
    Friend WithEvents ToolStrip2 As ToolStrip
    Friend WithEvents mnuIncidenciaPiloto As ToolStripButton
    Friend WithEvents mnuHistorialPiloto As ToolStripButton
    Friend WithEvents lblStatusLicencia As RichTextBox
    Friend WithEvents lcPoliza As DevExpress.XtraEditors.LabelControl
    Friend WithEvents mnuImprimir As DevExpress.XtraBars.BarButtonItem
End Class
