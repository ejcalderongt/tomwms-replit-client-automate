<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmResolucion_Lp
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If components IsNot Nothing Then
                components.Dispose()
            End If
            If gBeResolucion_Lp IsNot Nothing Then
                gBeResolucion_Lp = Nothing
            End If
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
        Dim lblSerie As System.Windows.Forms.Label
        Dim User_agrLabel As System.Windows.Forms.Label
        Dim Fec_agrLabel As System.Windows.Forms.Label
        Dim User_modLabel As System.Windows.Forms.Label
        Dim Fec_modLabel As System.Windows.Forms.Label
        Dim CodigoLabel As System.Windows.Forms.Label
        Dim lblCodigo As System.Windows.Forms.Label
        Dim lblCorrelativoInicial As System.Windows.Forms.Label
        Dim ActivoLabel As System.Windows.Forms.Label
        Dim lblOperador As System.Windows.Forms.Label
        Dim lblBodega As System.Windows.Forms.Label
        Dim lblCorrelativoFinal As System.Windows.Forms.Label
        Dim lblCorrelativoActual As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmResolucion_Lp))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuAsignacion = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.txtCorrelativoActual = New System.Windows.Forms.NumericUpDown()
        Me.txtCorrelativoFinal = New System.Windows.Forms.NumericUpDown()
        Me.cmbBodega = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbOperador = New DevExpress.XtraEditors.LookUpEdit()
        Me.lbl = New System.Windows.Forms.Label()
        Me.chkActivo = New DevExpress.XtraEditors.CheckEdit()
        Me.txtCorrelativoInicial = New System.Windows.Forms.NumericUpDown()
        Me.txtNoSerie = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_agrDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_agrTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_modDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_modTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.dkArancel = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        lblSerie = New System.Windows.Forms.Label()
        User_agrLabel = New System.Windows.Forms.Label()
        Fec_agrLabel = New System.Windows.Forms.Label()
        User_modLabel = New System.Windows.Forms.Label()
        Fec_modLabel = New System.Windows.Forms.Label()
        CodigoLabel = New System.Windows.Forms.Label()
        lblCodigo = New System.Windows.Forms.Label()
        lblCorrelativoInicial = New System.Windows.Forms.Label()
        ActivoLabel = New System.Windows.Forms.Label()
        lblOperador = New System.Windows.Forms.Label()
        lblBodega = New System.Windows.Forms.Label()
        lblCorrelativoFinal = New System.Windows.Forms.Label()
        lblCorrelativoActual = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.txtCorrelativoActual, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCorrelativoFinal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbOperador.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCorrelativoInicial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dkArancel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerBottom.SuspendLayout()
        Me.DockPanel1.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblSerie
        '
        lblSerie.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblSerie.AutoSize = True
        lblSerie.Location = New System.Drawing.Point(46, 163)
        lblSerie.Name = "lblSerie"
        lblSerie.Size = New System.Drawing.Size(42, 17)
        lblSerie.TabIndex = 2
        lblSerie.Text = "Serie:"
        '
        'User_agrLabel
        '
        User_agrLabel.AutoSize = True
        User_agrLabel.Location = New System.Drawing.Point(55, 11)
        User_agrLabel.Name = "User_agrLabel"
        User_agrLabel.Size = New System.Drawing.Size(106, 17)
        User_agrLabel.TabIndex = 0
        User_agrLabel.Text = "Usuario Agregó:"
        '
        'Fec_agrLabel
        '
        Fec_agrLabel.AutoSize = True
        Fec_agrLabel.Location = New System.Drawing.Point(55, 43)
        Fec_agrLabel.Name = "Fec_agrLabel"
        Fec_agrLabel.Size = New System.Drawing.Size(97, 17)
        Fec_agrLabel.TabIndex = 4
        Fec_agrLabel.Text = "Fecha Agregó:"
        '
        'User_modLabel
        '
        User_modLabel.AutoSize = True
        User_modLabel.Location = New System.Drawing.Point(443, 11)
        User_modLabel.Name = "User_modLabel"
        User_modLabel.Size = New System.Drawing.Size(111, 17)
        User_modLabel.TabIndex = 3
        User_modLabel.Text = "Usuario Modificó:"
        '
        'Fec_modLabel
        '
        Fec_modLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Fec_modLabel.AutoSize = True
        Fec_modLabel.Location = New System.Drawing.Point(443, 43)
        Fec_modLabel.Name = "Fec_modLabel"
        Fec_modLabel.Size = New System.Drawing.Size(102, 17)
        Fec_modLabel.TabIndex = 6
        Fec_modLabel.Text = "Fecha Modificó:"
        '
        'CodigoLabel
        '
        CodigoLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        CodigoLabel.AutoSize = True
        CodigoLabel.Location = New System.Drawing.Point(46, 74)
        CodigoLabel.Name = "CodigoLabel"
        CodigoLabel.Size = New System.Drawing.Size(56, 17)
        CodigoLabel.TabIndex = 0
        CodigoLabel.Text = "Código:"
        '
        'lblCodigo
        '
        lblCodigo.AutoSize = True
        lblCodigo.Location = New System.Drawing.Point(157, 30)
        lblCodigo.Name = "lblCodigo"
        lblCodigo.Size = New System.Drawing.Size(0, 13)
        lblCodigo.TabIndex = 10
        '
        'lblCorrelativoInicial
        '
        lblCorrelativoInicial.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblCorrelativoInicial.AutoSize = True
        lblCorrelativoInicial.Location = New System.Drawing.Point(46, 192)
        lblCorrelativoInicial.Name = "lblCorrelativoInicial"
        lblCorrelativoInicial.Size = New System.Drawing.Size(115, 17)
        lblCorrelativoInicial.TabIndex = 4
        lblCorrelativoInicial.Text = "Correlativo Inicial:"
        '
        'ActivoLabel
        '
        ActivoLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        ActivoLabel.AutoSize = True
        ActivoLabel.Location = New System.Drawing.Point(784, 51)
        ActivoLabel.Name = "ActivoLabel"
        ActivoLabel.Size = New System.Drawing.Size(51, 17)
        ActivoLabel.TabIndex = 6
        ActivoLabel.Text = "Activo:"
        '
        'lblOperador
        '
        lblOperador.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblOperador.AutoSize = True
        lblOperador.ForeColor = System.Drawing.Color.Black
        lblOperador.Location = New System.Drawing.Point(46, 106)
        lblOperador.Name = "lblOperador"
        lblOperador.Size = New System.Drawing.Size(71, 17)
        lblOperador.TabIndex = 8
        lblOperador.Text = "Operador:"
        '
        'lblBodega
        '
        lblBodega.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblBodega.AutoSize = True
        lblBodega.ForeColor = System.Drawing.Color.Black
        lblBodega.Location = New System.Drawing.Point(46, 134)
        lblBodega.Name = "lblBodega"
        lblBodega.Size = New System.Drawing.Size(59, 17)
        lblBodega.TabIndex = 10
        lblBodega.Text = "Bodega:"
        '
        'lblCorrelativoFinal
        '
        lblCorrelativoFinal.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblCorrelativoFinal.AutoSize = True
        lblCorrelativoFinal.Location = New System.Drawing.Point(46, 223)
        lblCorrelativoFinal.Name = "lblCorrelativoFinal"
        lblCorrelativoFinal.Size = New System.Drawing.Size(109, 17)
        lblCorrelativoFinal.TabIndex = 12
        lblCorrelativoFinal.Text = "Correlativo Final:"
        '
        'lblCorrelativoActual
        '
        lblCorrelativoActual.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblCorrelativoActual.AutoSize = True
        lblCorrelativoActual.Location = New System.Drawing.Point(46, 254)
        lblCorrelativoActual.Name = "lblCorrelativoActual"
        lblCorrelativoActual.Size = New System.Drawing.Size(120, 17)
        lblCorrelativoActual.TabIndex = 14
        lblCorrelativoActual.Text = "Correlativo Actual:"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.RibbonControl.SearchEditItem, Me.mnuGuardar, Me.mnuActualizar, Me.mnuEliminar, Me.mnuAsignacion})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 6
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(953, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'mnuGuardar
        '
        Me.mnuGuardar.Caption = "Guardar"
        Me.mnuGuardar.Id = 1
        Me.mnuGuardar.ImageOptions.SvgImage = CType(resources.GetObject("mnuGuardar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuGuardar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.G))
        Me.mnuGuardar.Name = "mnuGuardar"
        Me.mnuGuardar.ShortcutKeyDisplayString = "G"
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
        'mnuAsignacion
        '
        Me.mnuAsignacion.Caption = "Asignacion"
        Me.mnuAsignacion.Id = 4
        Me.mnuAsignacion.ImageOptions.Image = CType(resources.GetObject("mnuAsignacion.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuAsignacion.ImageOptions.LargeImage = CType(resources.GetObject("mnuAsignacion.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuAsignacion.Name = "mnuAsignacion"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Mantenimiento de Resoluciones para LP"
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
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 558)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(953, 30)
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(lblCorrelativoActual)
        Me.GroupControl1.Controls.Add(Me.txtCorrelativoActual)
        Me.GroupControl1.Controls.Add(lblCorrelativoFinal)
        Me.GroupControl1.Controls.Add(Me.txtCorrelativoFinal)
        Me.GroupControl1.Controls.Add(lblBodega)
        Me.GroupControl1.Controls.Add(Me.cmbBodega)
        Me.GroupControl1.Controls.Add(lblOperador)
        Me.GroupControl1.Controls.Add(Me.cmbOperador)
        Me.GroupControl1.Controls.Add(Me.lbl)
        Me.GroupControl1.Controls.Add(ActivoLabel)
        Me.GroupControl1.Controls.Add(Me.chkActivo)
        Me.GroupControl1.Controls.Add(lblCorrelativoInicial)
        Me.GroupControl1.Controls.Add(Me.txtCorrelativoInicial)
        Me.GroupControl1.Controls.Add(CodigoLabel)
        Me.GroupControl1.Controls.Add(lblSerie)
        Me.GroupControl1.Controls.Add(Me.txtNoSerie)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 193)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(953, 339)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Datos de Resolución"
        '
        'txtCorrelativoActual
        '
        Me.txtCorrelativoActual.Location = New System.Drawing.Point(193, 252)
        Me.txtCorrelativoActual.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCorrelativoActual.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtCorrelativoActual.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtCorrelativoActual.Name = "txtCorrelativoActual"
        Me.txtCorrelativoActual.Size = New System.Drawing.Size(241, 23)
        Me.txtCorrelativoActual.TabIndex = 15
        '
        'txtCorrelativoFinal
        '
        Me.txtCorrelativoFinal.Location = New System.Drawing.Point(193, 221)
        Me.txtCorrelativoFinal.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCorrelativoFinal.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtCorrelativoFinal.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtCorrelativoFinal.Name = "txtCorrelativoFinal"
        Me.txtCorrelativoFinal.Size = New System.Drawing.Size(241, 23)
        Me.txtCorrelativoFinal.TabIndex = 13
        '
        'cmbBodega
        '
        Me.cmbBodega.Location = New System.Drawing.Point(193, 131)
        Me.cmbBodega.Name = "cmbBodega"
        Me.cmbBodega.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbBodega.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodega.Properties.NullText = ""
        Me.cmbBodega.Size = New System.Drawing.Size(241, 22)
        Me.cmbBodega.TabIndex = 11
        '
        'cmbOperador
        '
        Me.cmbOperador.Location = New System.Drawing.Point(193, 103)
        Me.cmbOperador.Name = "cmbOperador"
        Me.cmbOperador.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbOperador.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbOperador.Properties.NullText = ""
        Me.cmbOperador.Size = New System.Drawing.Size(241, 22)
        Me.cmbOperador.TabIndex = 9
        '
        'lbl
        '
        Me.lbl.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar
        Me.lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl.Location = New System.Drawing.Point(193, 72)
        Me.lbl.Name = "lbl"
        Me.lbl.Size = New System.Drawing.Size(241, 26)
        Me.lbl.TabIndex = 1
        '
        'chkActivo
        '
        Me.chkActivo.EditValue = True
        Me.chkActivo.Location = New System.Drawing.Point(857, 47)
        Me.chkActivo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkActivo.MenuManager = Me.RibbonControl
        Me.chkActivo.Name = "chkActivo"
        Me.chkActivo.Properties.Caption = ""
        Me.chkActivo.Size = New System.Drawing.Size(41, 24)
        Me.chkActivo.TabIndex = 7
        '
        'txtCorrelativoInicial
        '
        Me.txtCorrelativoInicial.Location = New System.Drawing.Point(193, 190)
        Me.txtCorrelativoInicial.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCorrelativoInicial.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtCorrelativoInicial.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtCorrelativoInicial.Name = "txtCorrelativoInicial"
        Me.txtCorrelativoInicial.Size = New System.Drawing.Size(241, 23)
        Me.txtCorrelativoInicial.TabIndex = 5
        '
        'txtNoSerie
        '
        Me.txtNoSerie.Location = New System.Drawing.Point(193, 160)
        Me.txtNoSerie.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNoSerie.MenuManager = Me.RibbonControl
        Me.txtNoSerie.Name = "txtNoSerie"
        Me.txtNoSerie.Properties.MaxLength = 2
        Me.txtNoSerie.Size = New System.Drawing.Size(241, 22)
        Me.txtNoSerie.TabIndex = 3
        '
        'Fec_agrDateEdit
        '
        Me.Fec_agrDateEdit.EditValue = Nothing
        Me.Fec_agrDateEdit.Enabled = False
        Me.Fec_agrDateEdit.Location = New System.Drawing.Point(157, 39)
        Me.Fec_agrDateEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Fec_agrDateEdit.MenuManager = Me.RibbonControl
        Me.Fec_agrDateEdit.Name = "Fec_agrDateEdit"
        Me.Fec_agrDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit.Size = New System.Drawing.Size(261, 22)
        Me.Fec_agrDateEdit.TabIndex = 5
        '
        'User_agrTextEdit
        '
        Me.User_agrTextEdit.Enabled = False
        Me.User_agrTextEdit.Location = New System.Drawing.Point(157, 7)
        Me.User_agrTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.User_agrTextEdit.MenuManager = Me.RibbonControl
        Me.User_agrTextEdit.Name = "User_agrTextEdit"
        Me.User_agrTextEdit.Size = New System.Drawing.Size(261, 22)
        Me.User_agrTextEdit.TabIndex = 1
        '
        'Fec_modDateEdit
        '
        Me.Fec_modDateEdit.EditValue = Nothing
        Me.Fec_modDateEdit.Enabled = False
        Me.Fec_modDateEdit.Location = New System.Drawing.Point(546, 39)
        Me.Fec_modDateEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Fec_modDateEdit.MenuManager = Me.RibbonControl
        Me.Fec_modDateEdit.Name = "Fec_modDateEdit"
        Me.Fec_modDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit.Size = New System.Drawing.Size(261, 22)
        Me.Fec_modDateEdit.TabIndex = 7
        '
        'User_modTextEdit
        '
        Me.User_modTextEdit.Enabled = False
        Me.User_modTextEdit.Location = New System.Drawing.Point(546, 7)
        Me.User_modTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.User_modTextEdit.MenuManager = Me.RibbonControl
        Me.User_modTextEdit.Name = "User_modTextEdit"
        Me.User_modTextEdit.Size = New System.Drawing.Size(261, 22)
        Me.User_modTextEdit.TabIndex = 2
        '
        'dkArancel
        '
        Me.dkArancel.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerBottom})
        Me.dkArancel.Form = Me
        Me.dkArancel.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane"})
        '
        'hideContainerBottom
        '
        Me.hideContainerBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.hideContainerBottom.Controls.Add(Me.DockPanel1)
        Me.hideContainerBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 532)
        Me.hideContainerBottom.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(953, 26)
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.ID = New System.Guid("7c7484eb-6749-462a-9bd0-98c989935c63")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 0)
        Me.DockPanel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(200, 89)
        Me.DockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.SavedIndex = 0
        Me.DockPanel1.Size = New System.Drawing.Size(953, 110)
        Me.DockPanel1.Text = "Bitácora"
        Me.DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.Fec_modDateEdit)
        Me.DockPanel1_Container.Controls.Add(Me.Fec_agrDateEdit)
        Me.DockPanel1_Container.Controls.Add(Fec_modLabel)
        Me.DockPanel1_Container.Controls.Add(User_agrLabel)
        Me.DockPanel1_Container.Controls.Add(Me.User_modTextEdit)
        Me.DockPanel1_Container.Controls.Add(Fec_agrLabel)
        Me.DockPanel1_Container.Controls.Add(User_modLabel)
        Me.DockPanel1_Container.Controls.Add(Me.User_agrTextEdit)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(4, 34)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(945, 72)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'frmResolucion_Lp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(953, 588)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.Name = "frmResolucion_Lp"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Mantenimiento de resolución para Licencias"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.txtCorrelativoActual, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCorrelativoFinal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbOperador.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCorrelativoInicial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dkArancel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerBottom.ResumeLayout(False)
        Me.DockPanel1.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        Me.DockPanel1_Container.PerformLayout()
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
    Friend WithEvents mnuAsignacion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtNoSerie As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_agrDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_agrTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_modDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_modTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCorrelativoInicial As System.Windows.Forms.NumericUpDown
    Friend WithEvents chkActivo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents lbl As System.Windows.Forms.Label
    Friend WithEvents dkArancel As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents cmbBodega As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbOperador As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents txtCorrelativoActual As NumericUpDown
    Friend WithEvents txtCorrelativoFinal As NumericUpDown
End Class