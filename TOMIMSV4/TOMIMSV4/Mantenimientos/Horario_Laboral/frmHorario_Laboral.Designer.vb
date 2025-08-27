<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmHorario_Laboral
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
        Me.components = New System.ComponentModel.Container()
        Dim IdBodegaLabel As System.Windows.Forms.Label
        Dim User_agrLabel As System.Windows.Forms.Label
        Dim Fec_agrLabel As System.Windows.Forms.Label
        Dim User_modLabel As System.Windows.Forms.Label
        Dim Fec_modLabel As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim Label5 As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim Label6 As System.Windows.Forms.Label
        Dim Label7 As System.Windows.Forms.Label
        Dim Label8 As System.Windows.Forms.Label
        Dim Label9 As System.Windows.Forms.Label
        Dim Label10 As System.Windows.Forms.Label
        Dim Label11 As System.Windows.Forms.Label
        Dim Label12 As System.Windows.Forms.Label
        Dim Label14 As System.Windows.Forms.Label
        Dim lblTotalHorasUsadas As System.Windows.Forms.Label
        Dim Label13 As System.Windows.Forms.Label
        Dim NombreLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHorario_Laboral))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.cmbTurno = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmdJornada = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbBodega = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtNombre = New DevExpress.XtraEditors.TextEdit()
        Me.lblCodigo = New System.Windows.Forms.Label()
        Me.chkHoraExtra = New System.Windows.Forms.CheckBox()
        Me.txtTiempoRetraso = New System.Windows.Forms.NumericUpDown()
        Me.txtMMS2 = New System.Windows.Forms.NumericUpDown()
        Me.txtMMS1 = New System.Windows.Forms.NumericUpDown()
        Me.txtMMI2 = New System.Windows.Forms.NumericUpDown()
        Me.txtMMI1 = New System.Windows.Forms.NumericUpDown()
        Me.dtmHoraFin = New System.Windows.Forms.DateTimePicker()
        Me.dtmHoraInicio = New System.Windows.Forms.DateTimePicker()
        Me.Fec_agrDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_agrTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_modDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_modTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.groupDetalle = New DevExpress.XtraEditors.GroupControl()
        Me.chkActivos = New DevExpress.XtraEditors.CheckEdit()
        Me.ToolStrip = New System.Windows.Forms.ToolStrip()
        Me.cmdNuevo = New System.Windows.Forms.ToolStripButton()
        Me.cmdAdd = New System.Windows.Forms.ToolStripButton()
        Me.cmdActualizar = New System.Windows.Forms.ToolStripButton()
        Me.cmdDelete = New System.Windows.Forms.ToolStripButton()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lblTotalHorasJornada = New System.Windows.Forms.Label()
        Me.chkDomingo = New System.Windows.Forms.CheckBox()
        Me.chkSabado = New System.Windows.Forms.CheckBox()
        Me.chkViernes = New System.Windows.Forms.CheckBox()
        Me.chkJueves = New System.Windows.Forms.CheckBox()
        Me.chkMiercoles = New System.Windows.Forms.CheckBox()
        Me.chkMartes = New System.Windows.Forms.CheckBox()
        Me.chkLunes = New System.Windows.Forms.CheckBox()
        Me.Dgrid = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.dkHorarioLaboral = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        IdBodegaLabel = New System.Windows.Forms.Label()
        User_agrLabel = New System.Windows.Forms.Label()
        Fec_agrLabel = New System.Windows.Forms.Label()
        User_modLabel = New System.Windows.Forms.Label()
        Fec_modLabel = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        Label5 = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        Label6 = New System.Windows.Forms.Label()
        Label7 = New System.Windows.Forms.Label()
        Label8 = New System.Windows.Forms.Label()
        Label9 = New System.Windows.Forms.Label()
        Label10 = New System.Windows.Forms.Label()
        Label11 = New System.Windows.Forms.Label()
        Label12 = New System.Windows.Forms.Label()
        Label14 = New System.Windows.Forms.Label()
        lblTotalHorasUsadas = New System.Windows.Forms.Label()
        Label13 = New System.Windows.Forms.Label()
        NombreLabel = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.cmbTurno.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdJornada.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombre.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTiempoRetraso, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMMS2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMMS1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMMI2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMMI1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.groupDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupDetalle.SuspendLayout()
        CType(Me.chkActivos.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip.SuspendLayout()
        CType(Me.Dgrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dkHorarioLaboral, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerBottom.SuspendLayout()
        Me.DockPanel1.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        Me.SuspendLayout()
        '
        'IdBodegaLabel
        '
        IdBodegaLabel.AutoSize = True
        IdBodegaLabel.Location = New System.Drawing.Point(19, 36)
        IdBodegaLabel.Name = "IdBodegaLabel"
        IdBodegaLabel.Size = New System.Drawing.Size(54, 16)
        IdBodegaLabel.TabIndex = 0
        IdBodegaLabel.Text = "Bodega:"
        '
        'User_agrLabel
        '
        User_agrLabel.AutoSize = True
        User_agrLabel.Location = New System.Drawing.Point(12, 20)
        User_agrLabel.Name = "User_agrLabel"
        User_agrLabel.Size = New System.Drawing.Size(60, 16)
        User_agrLabel.TabIndex = 0
        User_agrLabel.Text = "user agr:"
        '
        'Fec_agrLabel
        '
        Fec_agrLabel.AutoSize = True
        Fec_agrLabel.Location = New System.Drawing.Point(237, 20)
        Fec_agrLabel.Name = "Fec_agrLabel"
        Fec_agrLabel.Size = New System.Drawing.Size(52, 16)
        Fec_agrLabel.TabIndex = 2
        Fec_agrLabel.Text = "fec agr:"
        '
        'User_modLabel
        '
        User_modLabel.AutoSize = True
        User_modLabel.Location = New System.Drawing.Point(521, 20)
        User_modLabel.Name = "User_modLabel"
        User_modLabel.Size = New System.Drawing.Size(66, 16)
        User_modLabel.TabIndex = 4
        User_modLabel.Text = "user mod:"
        '
        'Fec_modLabel
        '
        Fec_modLabel.AutoSize = True
        Fec_modLabel.Location = New System.Drawing.Point(766, 20)
        Fec_modLabel.Name = "Fec_modLabel"
        Fec_modLabel.Size = New System.Drawing.Size(58, 16)
        Fec_modLabel.TabIndex = 6
        Fec_modLabel.Text = "fec mod:"
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(17, 109)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(73, 16)
        Label2.TabIndex = 13
        Label2.Text = "Hora Inicio:"
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.Location = New System.Drawing.Point(17, 142)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(60, 16)
        Label3.TabIndex = 15
        Label3.Text = "Hora Fin:"
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(17, 81)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(30, 16)
        Label4.TabIndex = 9
        Label4.Text = "Dia:"
        '
        'Label5
        '
        Label5.AutoSize = True
        Label5.Location = New System.Drawing.Point(547, 36)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(57, 16)
        Label5.TabIndex = 1
        Label5.Text = "Jornada:"
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(537, 74)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(154, 16)
        Label1.TabIndex = 8
        Label1.Text = "Minutos Mínimos Ingreso:"
        '
        'Label6
        '
        Label6.AutoSize = True
        Label6.Location = New System.Drawing.Point(536, 104)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(157, 16)
        Label6.TabIndex = 12
        Label6.Text = "Minutos Máximos Ingreso:"
        '
        'Label7
        '
        Label7.AutoSize = True
        Label7.Location = New System.Drawing.Point(537, 170)
        Label7.Name = "Label7"
        Label7.Size = New System.Drawing.Size(149, 16)
        Label7.TabIndex = 21
        Label7.Text = "Minutos Máximos Salida:"
        '
        'Label8
        '
        Label8.AutoSize = True
        Label8.Location = New System.Drawing.Point(537, 136)
        Label8.Name = "Label8"
        Label8.Size = New System.Drawing.Size(146, 16)
        Label8.TabIndex = 17
        Label8.Text = "Minutos Mínimos Salida:"
        '
        'Label9
        '
        Label9.AutoSize = True
        Label9.Location = New System.Drawing.Point(16, 172)
        Label9.Name = "Label9"
        Label9.Size = New System.Drawing.Size(114, 16)
        Label9.TabIndex = 19
        Label9.Text = "Retraso Permitido:"
        '
        'Label10
        '
        Label10.AutoSize = True
        Label10.Location = New System.Drawing.Point(15, 205)
        Label10.Name = "Label10"
        Label10.Size = New System.Drawing.Size(84, 16)
        Label10.TabIndex = 23
        Label10.Text = "Horas Extras:"
        '
        'Label11
        '
        Label11.AutoSize = True
        Label11.Location = New System.Drawing.Point(547, 68)
        Label11.Name = "Label11"
        Label11.Size = New System.Drawing.Size(46, 16)
        Label11.TabIndex = 4
        Label11.Text = "Turno:"
        '
        'Label12
        '
        Label12.AutoSize = True
        Label12.Location = New System.Drawing.Point(19, 101)
        Label12.Name = "Label12"
        Label12.Size = New System.Drawing.Size(46, 16)
        Label12.TabIndex = 9
        Label12.Text = "Código"
        '
        'Label14
        '
        Label14.AutoSize = True
        Label14.Location = New System.Drawing.Point(170, 206)
        Label14.Name = "Label14"
        Label14.Size = New System.Drawing.Size(106, 16)
        Label14.TabIndex = 25
        Label14.Text = "Horas de jornada"
        '
        'lblTotalHorasUsadas
        '
        lblTotalHorasUsadas.AutoSize = True
        lblTotalHorasUsadas.Location = New System.Drawing.Point(427, 206)
        lblTotalHorasUsadas.Name = "lblTotalHorasUsadas"
        lblTotalHorasUsadas.Size = New System.Drawing.Size(17, 16)
        lblTotalHorasUsadas.TabIndex = 28
        lblTotalHorasUsadas.Text = "--"
        '
        'Label13
        '
        Label13.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label13.AutoSize = True
        Label13.Location = New System.Drawing.Point(873, 206)
        Label13.Name = "Label13"
        Label13.Size = New System.Drawing.Size(46, 16)
        Label13.TabIndex = 29
        Label13.Text = "Activo:"
        '
        'NombreLabel
        '
        NombreLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        NombreLabel.AutoSize = True
        NombreLabel.Location = New System.Drawing.Point(19, 69)
        NombreLabel.Name = "NombreLabel"
        NombreLabel.Size = New System.Drawing.Size(57, 16)
        NombreLabel.TabIndex = 5
        NombreLabel.Text = "Nombre:"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.RibbonControl.SearchEditItem, Me.mnuGuardar, Me.mnuActualizar, Me.mnuEliminar})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 5
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1072, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'mnuGuardar
        '
        Me.mnuGuardar.Caption = "Guardar"
        Me.mnuGuardar.Id = 1
        Me.mnuGuardar.ImageOptions.SvgImage = CType(resources.GetObject("mnuGuardar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuGuardar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.G))
        Me.mnuGuardar.Name = "mnuGuardar"
        '
        'mnuActualizar
        '
        Me.mnuActualizar.Caption = "Actualizar"
        Me.mnuActualizar.Id = 2
        Me.mnuActualizar.ImageOptions.SvgImage = CType(resources.GetObject("mnuActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuActualizar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A))
        Me.mnuActualizar.Name = "mnuActualizar"
        '
        'mnuEliminar
        '
        Me.mnuEliminar.Caption = "Desactivar"
        Me.mnuEliminar.Id = 3
        Me.mnuEliminar.ImageOptions.SvgImage = CType(resources.GetObject("mnuEliminar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEliminar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E))
        Me.mnuEliminar.Name = "mnuEliminar"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Mantenimiento horario laboral"
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
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 929)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1072, 30)
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.cmbTurno)
        Me.GroupControl1.Controls.Add(Me.cmdJornada)
        Me.GroupControl1.Controls.Add(Me.cmbBodega)
        Me.GroupControl1.Controls.Add(Me.txtNombre)
        Me.GroupControl1.Controls.Add(NombreLabel)
        Me.GroupControl1.Controls.Add(Me.lblCodigo)
        Me.GroupControl1.Controls.Add(Label12)
        Me.GroupControl1.Controls.Add(Label11)
        Me.GroupControl1.Controls.Add(Label5)
        Me.GroupControl1.Controls.Add(IdBodegaLabel)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl1.Location = New System.Drawing.Point(0, 193)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1072, 144)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Horario Laboral"
        '
        'cmbTurno
        '
        Me.cmbTurno.Location = New System.Drawing.Point(713, 65)
        Me.cmbTurno.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbTurno.MenuManager = Me.RibbonControl
        Me.cmbTurno.Name = "cmbTurno"
        Me.cmbTurno.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTurno.Properties.NullText = ""
        Me.cmbTurno.Size = New System.Drawing.Size(303, 22)
        Me.cmbTurno.TabIndex = 6
        '
        'cmdJornada
        '
        Me.cmdJornada.Location = New System.Drawing.Point(713, 32)
        Me.cmdJornada.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmdJornada.MenuManager = Me.RibbonControl
        Me.cmdJornada.Name = "cmdJornada"
        Me.cmdJornada.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmdJornada.Properties.NullText = ""
        Me.cmdJornada.Size = New System.Drawing.Size(303, 22)
        Me.cmdJornada.TabIndex = 2
        '
        'cmbBodega
        '
        Me.cmbBodega.Location = New System.Drawing.Point(133, 38)
        Me.cmbBodega.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbBodega.MenuManager = Me.RibbonControl
        Me.cmbBodega.Name = "cmbBodega"
        Me.cmbBodega.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodega.Properties.NullText = ""
        Me.cmbBodega.Size = New System.Drawing.Size(303, 22)
        Me.cmbBodega.TabIndex = 3
        '
        'txtNombre
        '
        Me.txtNombre.Location = New System.Drawing.Point(133, 70)
        Me.txtNombre.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNombre.MenuManager = Me.RibbonControl
        Me.txtNombre.Name = "txtNombre"
        Me.txtNombre.Size = New System.Drawing.Size(303, 22)
        Me.txtNombre.TabIndex = 7
        '
        'lblCodigo
        '
        Me.lblCodigo.AutoSize = True
        Me.lblCodigo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCodigo.Location = New System.Drawing.Point(130, 101)
        Me.lblCodigo.Name = "lblCodigo"
        Me.lblCodigo.Size = New System.Drawing.Size(0, 17)
        Me.lblCodigo.TabIndex = 8
        '
        'chkHoraExtra
        '
        Me.chkHoraExtra.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkHoraExtra.AutoSize = True
        Me.chkHoraExtra.Location = New System.Drawing.Point(133, 205)
        Me.chkHoraExtra.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkHoraExtra.Name = "chkHoraExtra"
        Me.chkHoraExtra.Size = New System.Drawing.Size(18, 17)
        Me.chkHoraExtra.TabIndex = 24
        Me.chkHoraExtra.UseVisualStyleBackColor = True
        '
        'txtTiempoRetraso
        '
        Me.txtTiempoRetraso.Location = New System.Drawing.Point(140, 167)
        Me.txtTiempoRetraso.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtTiempoRetraso.Maximum = New Decimal(New Integer() {60, 0, 0, 0})
        Me.txtTiempoRetraso.Name = "txtTiempoRetraso"
        Me.txtTiempoRetraso.Size = New System.Drawing.Size(311, 23)
        Me.txtTiempoRetraso.TabIndex = 20
        Me.txtTiempoRetraso.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'txtMMS2
        '
        Me.txtMMS2.Location = New System.Drawing.Point(709, 170)
        Me.txtMMS2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtMMS2.Maximum = New Decimal(New Integer() {60, 0, 0, 0})
        Me.txtMMS2.Name = "txtMMS2"
        Me.txtMMS2.Size = New System.Drawing.Size(303, 23)
        Me.txtMMS2.TabIndex = 22
        Me.txtMMS2.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'txtMMS1
        '
        Me.txtMMS1.Location = New System.Drawing.Point(709, 135)
        Me.txtMMS1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtMMS1.Maximum = New Decimal(New Integer() {60, 0, 0, 0})
        Me.txtMMS1.Name = "txtMMS1"
        Me.txtMMS1.Size = New System.Drawing.Size(303, 23)
        Me.txtMMS1.TabIndex = 18
        Me.txtMMS1.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'txtMMI2
        '
        Me.txtMMI2.Location = New System.Drawing.Point(708, 103)
        Me.txtMMI2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtMMI2.Maximum = New Decimal(New Integer() {60, 0, 0, 0})
        Me.txtMMI2.Name = "txtMMI2"
        Me.txtMMI2.Size = New System.Drawing.Size(303, 23)
        Me.txtMMI2.TabIndex = 14
        Me.txtMMI2.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'txtMMI1
        '
        Me.txtMMI1.Location = New System.Drawing.Point(708, 71)
        Me.txtMMI1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtMMI1.Maximum = New Decimal(New Integer() {60, 0, 0, 0})
        Me.txtMMI1.Name = "txtMMI1"
        Me.txtMMI1.Size = New System.Drawing.Size(303, 23)
        Me.txtMMI1.TabIndex = 10
        Me.txtMMI1.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'dtmHoraFin
        '
        Me.dtmHoraFin.Checked = False
        Me.dtmHoraFin.CustomFormat = "HH:mm:ss"
        Me.dtmHoraFin.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtmHoraFin.Location = New System.Drawing.Point(140, 135)
        Me.dtmHoraFin.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtmHoraFin.Name = "dtmHoraFin"
        Me.dtmHoraFin.ShowUpDown = True
        Me.dtmHoraFin.Size = New System.Drawing.Size(311, 23)
        Me.dtmHoraFin.TabIndex = 16
        '
        'dtmHoraInicio
        '
        Me.dtmHoraInicio.Checked = False
        Me.dtmHoraInicio.CustomFormat = "HH:mm:ss"
        Me.dtmHoraInicio.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtmHoraInicio.Location = New System.Drawing.Point(140, 102)
        Me.dtmHoraInicio.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtmHoraInicio.Name = "dtmHoraInicio"
        Me.dtmHoraInicio.ShowUpDown = True
        Me.dtmHoraInicio.Size = New System.Drawing.Size(311, 23)
        Me.dtmHoraInicio.TabIndex = 11
        Me.dtmHoraInicio.Value = New Date(2016, 1, 26, 12, 7, 0, 0)
        '
        'Fec_agrDateEdit
        '
        Me.Fec_agrDateEdit.EditValue = Nothing
        Me.Fec_agrDateEdit.Enabled = False
        Me.Fec_agrDateEdit.Location = New System.Drawing.Point(300, 16)
        Me.Fec_agrDateEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Fec_agrDateEdit.MenuManager = Me.RibbonControl
        Me.Fec_agrDateEdit.Name = "Fec_agrDateEdit"
        Me.Fec_agrDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit.Size = New System.Drawing.Size(175, 22)
        Me.Fec_agrDateEdit.TabIndex = 3
        '
        'User_agrTextEdit
        '
        Me.User_agrTextEdit.Enabled = False
        Me.User_agrTextEdit.Location = New System.Drawing.Point(78, 16)
        Me.User_agrTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.User_agrTextEdit.MenuManager = Me.RibbonControl
        Me.User_agrTextEdit.Name = "User_agrTextEdit"
        Me.User_agrTextEdit.Size = New System.Drawing.Size(152, 22)
        Me.User_agrTextEdit.TabIndex = 1
        '
        'Fec_modDateEdit
        '
        Me.Fec_modDateEdit.EditValue = Nothing
        Me.Fec_modDateEdit.Enabled = False
        Me.Fec_modDateEdit.Location = New System.Drawing.Point(831, 16)
        Me.Fec_modDateEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Fec_modDateEdit.MenuManager = Me.RibbonControl
        Me.Fec_modDateEdit.Name = "Fec_modDateEdit"
        Me.Fec_modDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit.Size = New System.Drawing.Size(175, 22)
        Me.Fec_modDateEdit.TabIndex = 7
        '
        'User_modTextEdit
        '
        Me.User_modTextEdit.Enabled = False
        Me.User_modTextEdit.Location = New System.Drawing.Point(593, 16)
        Me.User_modTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.User_modTextEdit.MenuManager = Me.RibbonControl
        Me.User_modTextEdit.Name = "User_modTextEdit"
        Me.User_modTextEdit.Size = New System.Drawing.Size(152, 22)
        Me.User_modTextEdit.TabIndex = 5
        '
        'groupDetalle
        '
        Me.groupDetalle.Controls.Add(Label13)
        Me.groupDetalle.Controls.Add(Me.chkActivos)
        Me.groupDetalle.Controls.Add(Me.ToolStrip)
        Me.groupDetalle.Controls.Add(Me.Label15)
        Me.groupDetalle.Controls.Add(lblTotalHorasUsadas)
        Me.groupDetalle.Controls.Add(Me.lblTotalHorasJornada)
        Me.groupDetalle.Controls.Add(Me.chkDomingo)
        Me.groupDetalle.Controls.Add(Label14)
        Me.groupDetalle.Controls.Add(Me.chkSabado)
        Me.groupDetalle.Controls.Add(Me.chkViernes)
        Me.groupDetalle.Controls.Add(Me.chkJueves)
        Me.groupDetalle.Controls.Add(Me.chkMiercoles)
        Me.groupDetalle.Controls.Add(Me.chkMartes)
        Me.groupDetalle.Controls.Add(Me.chkLunes)
        Me.groupDetalle.Controls.Add(Me.txtTiempoRetraso)
        Me.groupDetalle.Controls.Add(Label2)
        Me.groupDetalle.Controls.Add(Me.dtmHoraInicio)
        Me.groupDetalle.Controls.Add(Label3)
        Me.groupDetalle.Controls.Add(Me.chkHoraExtra)
        Me.groupDetalle.Controls.Add(Me.dtmHoraFin)
        Me.groupDetalle.Controls.Add(Label10)
        Me.groupDetalle.Controls.Add(Label4)
        Me.groupDetalle.Controls.Add(Label9)
        Me.groupDetalle.Controls.Add(Label1)
        Me.groupDetalle.Controls.Add(Me.txtMMS2)
        Me.groupDetalle.Controls.Add(Me.txtMMI1)
        Me.groupDetalle.Controls.Add(Label7)
        Me.groupDetalle.Controls.Add(Label6)
        Me.groupDetalle.Controls.Add(Me.txtMMS1)
        Me.groupDetalle.Controls.Add(Me.txtMMI2)
        Me.groupDetalle.Controls.Add(Label8)
        Me.groupDetalle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.groupDetalle.Location = New System.Drawing.Point(0, 337)
        Me.groupDetalle.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.groupDetalle.Name = "groupDetalle"
        Me.groupDetalle.Size = New System.Drawing.Size(1072, 302)
        Me.groupDetalle.TabIndex = 1
        Me.groupDetalle.Text = "Configuracion de Horarios"
        Me.groupDetalle.Visible = False
        '
        'chkActivos
        '
        Me.chkActivos.EditValue = True
        Me.chkActivos.Location = New System.Drawing.Point(999, 203)
        Me.chkActivos.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkActivos.MenuManager = Me.RibbonControl
        Me.chkActivos.Name = "chkActivos"
        Me.chkActivos.Properties.Caption = ""
        Me.chkActivos.Size = New System.Drawing.Size(41, 24)
        Me.chkActivos.TabIndex = 30
        '
        'ToolStrip
        '
        Me.ToolStrip.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdNuevo, Me.cmdAdd, Me.cmdActualizar, Me.cmdDelete})
        Me.ToolStrip.Location = New System.Drawing.Point(2, 28)
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.Size = New System.Drawing.Size(1068, 27)
        Me.ToolStrip.TabIndex = 0
        Me.ToolStrip.Text = "ToolStrip4"
        '
        'cmdNuevo
        '
        Me.cmdNuevo.Image = CType(resources.GetObject("cmdNuevo.Image"), System.Drawing.Image)
        Me.cmdNuevo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdNuevo.Name = "cmdNuevo"
        Me.cmdNuevo.Size = New System.Drawing.Size(76, 24)
        Me.cmdNuevo.Text = "Nuevo"
        '
        'cmdAdd
        '
        Me.cmdAdd.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAdd.Image = Global.TOMWMS.My.Resources.Resources.green_check
        Me.cmdAdd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(86, 24)
        Me.cmdAdd.Text = "Guardar"
        '
        'cmdActualizar
        '
        Me.cmdActualizar.Image = Global.TOMWMS.My.Resources.Resources.cheked
        Me.cmdActualizar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdActualizar.Name = "cmdActualizar"
        Me.cmdActualizar.Size = New System.Drawing.Size(99, 24)
        Me.cmdActualizar.Text = "Actualizar"
        '
        'cmdDelete
        '
        Me.cmdDelete.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.Image = Global.TOMWMS.My.Resources.Resources.desactivar
        Me.cmdDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(102, 24)
        Me.cmdDelete.Text = "Desactivar"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(330, 206)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(85, 16)
        Me.Label15.TabIndex = 27
        Me.Label15.Text = "Horas Usadas"
        '
        'lblTotalHorasJornada
        '
        Me.lblTotalHorasJornada.AutoSize = True
        Me.lblTotalHorasJornada.Location = New System.Drawing.Point(287, 206)
        Me.lblTotalHorasJornada.Name = "lblTotalHorasJornada"
        Me.lblTotalHorasJornada.Size = New System.Drawing.Size(17, 16)
        Me.lblTotalHorasJornada.TabIndex = 26
        Me.lblTotalHorasJornada.Text = "--"
        '
        'chkDomingo
        '
        Me.chkDomingo.AutoSize = True
        Me.chkDomingo.Location = New System.Drawing.Point(413, 71)
        Me.chkDomingo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkDomingo.Name = "chkDomingo"
        Me.chkDomingo.Size = New System.Drawing.Size(37, 20)
        Me.chkDomingo.TabIndex = 7
        Me.chkDomingo.Tag = "7"
        Me.chkDomingo.Text = "D"
        Me.chkDomingo.UseVisualStyleBackColor = True
        '
        'chkSabado
        '
        Me.chkSabado.AutoSize = True
        Me.chkSabado.Location = New System.Drawing.Point(362, 71)
        Me.chkSabado.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkSabado.Name = "chkSabado"
        Me.chkSabado.Size = New System.Drawing.Size(37, 20)
        Me.chkSabado.TabIndex = 6
        Me.chkSabado.Tag = "6"
        Me.chkSabado.Text = "S"
        Me.chkSabado.UseVisualStyleBackColor = True
        '
        'chkViernes
        '
        Me.chkViernes.AutoSize = True
        Me.chkViernes.Location = New System.Drawing.Point(317, 71)
        Me.chkViernes.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkViernes.Name = "chkViernes"
        Me.chkViernes.Size = New System.Drawing.Size(37, 20)
        Me.chkViernes.TabIndex = 5
        Me.chkViernes.Tag = "5"
        Me.chkViernes.Text = "V"
        Me.chkViernes.UseVisualStyleBackColor = True
        '
        'chkJueves
        '
        Me.chkJueves.AutoSize = True
        Me.chkJueves.Location = New System.Drawing.Point(272, 71)
        Me.chkJueves.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkJueves.Name = "chkJueves"
        Me.chkJueves.Size = New System.Drawing.Size(34, 20)
        Me.chkJueves.TabIndex = 4
        Me.chkJueves.Tag = "4"
        Me.chkJueves.Text = "J"
        Me.chkJueves.UseVisualStyleBackColor = True
        '
        'chkMiercoles
        '
        Me.chkMiercoles.AutoSize = True
        Me.chkMiercoles.Location = New System.Drawing.Point(223, 71)
        Me.chkMiercoles.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkMiercoles.Name = "chkMiercoles"
        Me.chkMiercoles.Size = New System.Drawing.Size(39, 20)
        Me.chkMiercoles.TabIndex = 3
        Me.chkMiercoles.Tag = "3"
        Me.chkMiercoles.Text = "M"
        Me.chkMiercoles.UseVisualStyleBackColor = True
        '
        'chkMartes
        '
        Me.chkMartes.AutoSize = True
        Me.chkMartes.Location = New System.Drawing.Point(176, 71)
        Me.chkMartes.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkMartes.Name = "chkMartes"
        Me.chkMartes.Size = New System.Drawing.Size(39, 20)
        Me.chkMartes.TabIndex = 2
        Me.chkMartes.Tag = "2"
        Me.chkMartes.Text = "M"
        Me.chkMartes.UseVisualStyleBackColor = True
        '
        'chkLunes
        '
        Me.chkLunes.AutoSize = True
        Me.chkLunes.Location = New System.Drawing.Point(133, 71)
        Me.chkLunes.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkLunes.Name = "chkLunes"
        Me.chkLunes.Size = New System.Drawing.Size(35, 20)
        Me.chkLunes.TabIndex = 1
        Me.chkLunes.Tag = "1"
        Me.chkLunes.Text = "L"
        Me.chkLunes.UseVisualStyleBackColor = True
        '
        'Dgrid
        '
        Me.Dgrid.Cursor = System.Windows.Forms.Cursors.Default
        Me.Dgrid.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Dgrid.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Dgrid.Location = New System.Drawing.Point(0, 639)
        Me.Dgrid.MainView = Me.GridView1
        Me.Dgrid.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Dgrid.MenuManager = Me.RibbonControl
        Me.Dgrid.Name = "Dgrid"
        Me.Dgrid.Size = New System.Drawing.Size(1072, 264)
        Me.Dgrid.TabIndex = 31
        Me.Dgrid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.DetailHeight = 431
        Me.GridView1.GridControl = Me.Dgrid
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'dkHorarioLaboral
        '
        Me.dkHorarioLaboral.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerBottom})
        Me.dkHorarioLaboral.Form = Me
        Me.dkHorarioLaboral.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane"})
        '
        'hideContainerBottom
        '
        Me.hideContainerBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.hideContainerBottom.Controls.Add(Me.DockPanel1)
        Me.hideContainerBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 903)
        Me.hideContainerBottom.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(1072, 26)
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.ID = New System.Guid("4623737c-04d4-4ffa-bd02-153fa1574d7a")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 0)
        Me.DockPanel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(200, 89)
        Me.DockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.SavedIndex = 0
        Me.DockPanel1.Size = New System.Drawing.Size(1072, 89)
        Me.DockPanel1.Text = "Bitácora"
        Me.DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(User_agrLabel)
        Me.DockPanel1_Container.Controls.Add(Me.User_agrTextEdit)
        Me.DockPanel1_Container.Controls.Add(Me.Fec_agrDateEdit)
        Me.DockPanel1_Container.Controls.Add(Me.User_modTextEdit)
        Me.DockPanel1_Container.Controls.Add(Fec_agrLabel)
        Me.DockPanel1_Container.Controls.Add(Fec_modLabel)
        Me.DockPanel1_Container.Controls.Add(Me.Fec_modDateEdit)
        Me.DockPanel1_Container.Controls.Add(User_modLabel)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(4, 34)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1064, 51)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'frmHorario_Laboral
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1072, 959)
        Me.Controls.Add(Me.groupDetalle)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.Dgrid)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmHorario_Laboral"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Mantenimiento horario laboral"
        CType(Me.RibbonControl,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GroupControl1,System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupControl1.ResumeLayout(false)
        Me.GroupControl1.PerformLayout
        CType(Me.cmbTurno.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cmdJornada.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cmbBodega.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtNombre.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtTiempoRetraso,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtMMS2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtMMS1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtMMI2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtMMI1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.Fec_agrDateEdit.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.User_agrTextEdit.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.Fec_modDateEdit.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.User_modTextEdit.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.groupDetalle,System.ComponentModel.ISupportInitialize).EndInit
        Me.groupDetalle.ResumeLayout(false)
        Me.groupDetalle.PerformLayout
        CType(Me.chkActivos.Properties,System.ComponentModel.ISupportInitialize).EndInit
        Me.ToolStrip.ResumeLayout(false)
        Me.ToolStrip.PerformLayout
        CType(Me.Dgrid,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GridView1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dkHorarioLaboral,System.ComponentModel.ISupportInitialize).EndInit
        Me.hideContainerBottom.ResumeLayout(false)
        Me.DockPanel1.ResumeLayout(false)
        Me.DockPanel1_Container.ResumeLayout(false)
        Me.DockPanel1_Container.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents mnuGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEliminar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents Fec_agrDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_agrTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_modDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_modTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents dtmHoraFin As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtmHoraInicio As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtMMI1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtTiempoRetraso As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtMMS2 As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtMMS1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtMMI2 As System.Windows.Forms.NumericUpDown
    Friend WithEvents chkHoraExtra As System.Windows.Forms.CheckBox
    Friend WithEvents lblCodigo As System.Windows.Forms.Label
    Friend WithEvents groupDetalle As DevExpress.XtraEditors.GroupControl
    Friend WithEvents Label15 As Label
    Friend WithEvents lblTotalHorasJornada As Label
    Friend WithEvents chkDomingo As CheckBox
    Friend WithEvents chkSabado As CheckBox
    Friend WithEvents chkViernes As CheckBox
    Friend WithEvents chkJueves As CheckBox
    Friend WithEvents chkMiercoles As CheckBox
    Friend WithEvents chkMartes As CheckBox
    Friend WithEvents chkLunes As CheckBox
    Friend WithEvents Dgrid As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ToolStrip As ToolStrip
    Friend WithEvents cmdAdd As ToolStripButton
    Friend WithEvents cmdDelete As ToolStripButton
    Friend WithEvents chkActivos As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents cmdActualizar As ToolStripButton
    Friend WithEvents txtNombre As DevExpress.XtraEditors.TextEdit
    Friend WithEvents dkHorarioLaboral As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents cmbBodega As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmdJornada As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbTurno As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmdNuevo As ToolStripButton
End Class
