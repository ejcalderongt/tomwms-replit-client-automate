<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOrdenCompraTI
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
        Dim NombreLabel As System.Windows.Forms.Label
        Dim User_agrLabel As System.Windows.Forms.Label
        Dim Fec_agrLabel As System.Windows.Forms.Label
        Dim User_modLabel As System.Windows.Forms.Label
        Dim Fec_modLabel As System.Windows.Forms.Label
        Dim Label12 As System.Windows.Forms.Label
        Dim lblDevolucion As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim lblControlPoliza As System.Windows.Forms.Label
        Dim lblRequerirDocumentoRef As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim lblgenera_tarea_ingreso As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim lblBloquearLotes As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOrdenCompraTI))
        Dim Label4 As System.Windows.Forms.Label
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuAsignacion = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.chkBloquearLotes = New DevExpress.XtraEditors.ToggleSwitch()
        Me.chkPreguntarEnBackOrder = New DevExpress.XtraEditors.ToggleSwitch()
        Me.chkGeneraTareaIngreso = New DevExpress.XtraEditors.ToggleSwitch()
        Me.chkEsPolizaConsolidada = New DevExpress.XtraEditors.ToggleSwitch()
        Me.chkRequerirDocumentoRef = New DevExpress.XtraEditors.ToggleSwitch()
        Me.chkControlPoliza = New DevExpress.XtraEditors.ToggleSwitch()
        Me.chkDevolucion = New DevExpress.XtraEditors.ToggleSwitch()
        Me.chkActivo = New DevExpress.XtraEditors.CheckEdit()
        Me.lblCodigo = New System.Windows.Forms.Label()
        Me.txtNombre = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_agrDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_agrTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_modDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_modTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.dkOrdenCompraTI = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.chkPermitirExcedenteLotes = New DevExpress.XtraEditors.ToggleSwitch()
        NombreLabel = New System.Windows.Forms.Label()
        User_agrLabel = New System.Windows.Forms.Label()
        Fec_agrLabel = New System.Windows.Forms.Label()
        User_modLabel = New System.Windows.Forms.Label()
        Fec_modLabel = New System.Windows.Forms.Label()
        Label12 = New System.Windows.Forms.Label()
        lblDevolucion = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        lblControlPoliza = New System.Windows.Forms.Label()
        lblRequerirDocumentoRef = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        lblgenera_tarea_ingreso = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        lblBloquearLotes = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.chkBloquearLotes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPreguntarEnBackOrder.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkGeneraTareaIngreso.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkEsPolizaConsolidada.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRequerirDocumentoRef.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkControlPoliza.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDevolucion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombre.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dkOrdenCompraTI, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerBottom.SuspendLayout()
        Me.DockPanel1.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.chkPermitirExcedenteLotes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'NombreLabel
        '
        NombreLabel.AutoSize = True
        NombreLabel.Location = New System.Drawing.Point(47, 115)
        NombreLabel.Name = "NombreLabel"
        NombreLabel.Size = New System.Drawing.Size(57, 16)
        NombreLabel.TabIndex = 2
        NombreLabel.Text = "Nombre:"
        '
        'User_agrLabel
        '
        User_agrLabel.AutoSize = True
        User_agrLabel.Location = New System.Drawing.Point(92, 18)
        User_agrLabel.Name = "User_agrLabel"
        User_agrLabel.Size = New System.Drawing.Size(100, 16)
        User_agrLabel.TabIndex = 0
        User_agrLabel.Text = "Usuario Agregó:"
        '
        'Fec_agrLabel
        '
        Fec_agrLabel.AutoSize = True
        Fec_agrLabel.Location = New System.Drawing.Point(92, 50)
        Fec_agrLabel.Name = "Fec_agrLabel"
        Fec_agrLabel.Size = New System.Drawing.Size(91, 16)
        Fec_agrLabel.TabIndex = 4
        Fec_agrLabel.Text = "Fecha Agregó:"
        '
        'User_modLabel
        '
        User_modLabel.AutoSize = True
        User_modLabel.Location = New System.Drawing.Point(576, 18)
        User_modLabel.Name = "User_modLabel"
        User_modLabel.Size = New System.Drawing.Size(106, 16)
        User_modLabel.TabIndex = 3
        User_modLabel.Text = "Usuario Modificó:"
        '
        'Fec_modLabel
        '
        Fec_modLabel.AutoSize = True
        Fec_modLabel.Location = New System.Drawing.Point(576, 50)
        Fec_modLabel.Name = "Fec_modLabel"
        Fec_modLabel.Size = New System.Drawing.Size(97, 16)
        Fec_modLabel.TabIndex = 6
        Fec_modLabel.Text = "Fecha Modificó:"
        '
        'Label12
        '
        Label12.AutoSize = True
        Label12.Location = New System.Drawing.Point(47, 73)
        Label12.Name = "Label12"
        Label12.Size = New System.Drawing.Size(46, 16)
        Label12.TabIndex = 0
        Label12.Text = "Código"
        '
        'lblDevolucion
        '
        lblDevolucion.AutoSize = True
        lblDevolucion.Location = New System.Drawing.Point(47, 157)
        lblDevolucion.Name = "lblDevolucion"
        lblDevolucion.Size = New System.Drawing.Size(90, 16)
        lblDevolucion.TabIndex = 4
        lblDevolucion.Text = "Es Devolución:"
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(807, 95)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(46, 16)
        Label1.TabIndex = 6
        Label1.Text = "Activo:"
        '
        'lblControlPoliza
        '
        lblControlPoliza.AutoSize = True
        lblControlPoliza.Location = New System.Drawing.Point(47, 198)
        lblControlPoliza.Name = "lblControlPoliza"
        lblControlPoliza.Size = New System.Drawing.Size(90, 16)
        lblControlPoliza.TabIndex = 8
        lblControlPoliza.Text = "Control Póliza:"
        '
        'lblRequerirDocumentoRef
        '
        lblRequerirDocumentoRef.AutoSize = True
        lblRequerirDocumentoRef.Location = New System.Drawing.Point(47, 239)
        lblRequerirDocumentoRef.Name = "lblRequerirDocumentoRef"
        lblRequerirDocumentoRef.Size = New System.Drawing.Size(113, 16)
        lblRequerirDocumentoRef.TabIndex = 10
        lblRequerirDocumentoRef.Text = "Requerir Doc. Ref:"
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(47, 279)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(135, 16)
        Label2.TabIndex = 12
        Label2.Text = "Es Póliza Consolidada:"
        '
        'lblgenera_tarea_ingreso
        '
        lblgenera_tarea_ingreso.AutoSize = True
        lblgenera_tarea_ingreso.Location = New System.Drawing.Point(47, 317)
        lblgenera_tarea_ingreso.Name = "lblgenera_tarea_ingreso"
        lblgenera_tarea_ingreso.Size = New System.Drawing.Size(151, 16)
        lblgenera_tarea_ingreso.TabIndex = 14
        lblgenera_tarea_ingreso.Text = "Genera tarea de ingreso:"
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.Location = New System.Drawing.Point(47, 356)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(149, 16)
        Label3.TabIndex = 21
        Label3.Text = "Preguntar en BackOrder:"
        '
        'lblBloquearLotes
        '
        lblBloquearLotes.AutoSize = True
        lblBloquearLotes.Location = New System.Drawing.Point(312, 157)
        lblBloquearLotes.Name = "lblBloquearLotes"
        lblBloquearLotes.Size = New System.Drawing.Size(96, 16)
        lblBloquearLotes.TabIndex = 23
        lblBloquearLotes.Text = "Bloquear Lotes:"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.RibbonControl.SearchEditItem, Me.mnuGuardar, Me.mnuActualizar, Me.mnuEliminar, Me.mnuAsignacion})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 5
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1128, 193)
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
        Me.mnuEliminar.Caption = "Eliminar"
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
        Me.RibbonPage1.Text = "Menu Tipo Ingreso"
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
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 646)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1128, 30)
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.chkPermitirExcedenteLotes)
        Me.GroupControl1.Controls.Add(Label4)
        Me.GroupControl1.Controls.Add(Me.chkBloquearLotes)
        Me.GroupControl1.Controls.Add(lblBloquearLotes)
        Me.GroupControl1.Controls.Add(Me.chkPreguntarEnBackOrder)
        Me.GroupControl1.Controls.Add(Label3)
        Me.GroupControl1.Controls.Add(Me.chkGeneraTareaIngreso)
        Me.GroupControl1.Controls.Add(Me.chkEsPolizaConsolidada)
        Me.GroupControl1.Controls.Add(Me.chkRequerirDocumentoRef)
        Me.GroupControl1.Controls.Add(Me.chkControlPoliza)
        Me.GroupControl1.Controls.Add(Me.chkDevolucion)
        Me.GroupControl1.Controls.Add(lblgenera_tarea_ingreso)
        Me.GroupControl1.Controls.Add(Label2)
        Me.GroupControl1.Controls.Add(lblRequerirDocumentoRef)
        Me.GroupControl1.Controls.Add(lblControlPoliza)
        Me.GroupControl1.Controls.Add(Label1)
        Me.GroupControl1.Controls.Add(lblDevolucion)
        Me.GroupControl1.Controls.Add(Me.chkActivo)
        Me.GroupControl1.Controls.Add(Me.lblCodigo)
        Me.GroupControl1.Controls.Add(Label12)
        Me.GroupControl1.Controls.Add(NombreLabel)
        Me.GroupControl1.Controls.Add(Me.txtNombre)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 193)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1128, 427)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Parámetros tipo de documento para ingresos"
        '
        'chkBloquearLotes
        '
        Me.chkBloquearLotes.Location = New System.Drawing.Point(472, 153)
        Me.chkBloquearLotes.MenuManager = Me.RibbonControl
        Me.chkBloquearLotes.Name = "chkBloquearLotes"
        Me.chkBloquearLotes.Properties.OffText = "No"
        Me.chkBloquearLotes.Properties.OnText = "Sí"
        Me.chkBloquearLotes.Size = New System.Drawing.Size(95, 24)
        Me.chkBloquearLotes.TabIndex = 24
        '
        'chkPreguntarEnBackOrder
        '
        Me.chkPreguntarEnBackOrder.Location = New System.Drawing.Point(211, 352)
        Me.chkPreguntarEnBackOrder.MenuManager = Me.RibbonControl
        Me.chkPreguntarEnBackOrder.Name = "chkPreguntarEnBackOrder"
        Me.chkPreguntarEnBackOrder.Properties.OffText = "No"
        Me.chkPreguntarEnBackOrder.Properties.OnText = "Sí"
        Me.chkPreguntarEnBackOrder.Size = New System.Drawing.Size(95, 24)
        Me.chkPreguntarEnBackOrder.TabIndex = 22
        '
        'chkGeneraTareaIngreso
        '
        Me.chkGeneraTareaIngreso.Location = New System.Drawing.Point(211, 313)
        Me.chkGeneraTareaIngreso.MenuManager = Me.RibbonControl
        Me.chkGeneraTareaIngreso.Name = "chkGeneraTareaIngreso"
        Me.chkGeneraTareaIngreso.Properties.OffText = "No"
        Me.chkGeneraTareaIngreso.Properties.OnText = "Sí"
        Me.chkGeneraTareaIngreso.Size = New System.Drawing.Size(95, 24)
        Me.chkGeneraTareaIngreso.TabIndex = 20
        '
        'chkEsPolizaConsolidada
        '
        Me.chkEsPolizaConsolidada.Location = New System.Drawing.Point(211, 275)
        Me.chkEsPolizaConsolidada.MenuManager = Me.RibbonControl
        Me.chkEsPolizaConsolidada.Name = "chkEsPolizaConsolidada"
        Me.chkEsPolizaConsolidada.Properties.OffText = "No"
        Me.chkEsPolizaConsolidada.Properties.OnText = "Sí"
        Me.chkEsPolizaConsolidada.Size = New System.Drawing.Size(95, 24)
        Me.chkEsPolizaConsolidada.TabIndex = 19
        '
        'chkRequerirDocumentoRef
        '
        Me.chkRequerirDocumentoRef.Location = New System.Drawing.Point(211, 231)
        Me.chkRequerirDocumentoRef.MenuManager = Me.RibbonControl
        Me.chkRequerirDocumentoRef.Name = "chkRequerirDocumentoRef"
        Me.chkRequerirDocumentoRef.Properties.OffText = "No"
        Me.chkRequerirDocumentoRef.Properties.OnText = "Sí"
        Me.chkRequerirDocumentoRef.Size = New System.Drawing.Size(95, 24)
        Me.chkRequerirDocumentoRef.TabIndex = 18
        '
        'chkControlPoliza
        '
        Me.chkControlPoliza.Location = New System.Drawing.Point(211, 190)
        Me.chkControlPoliza.MenuManager = Me.RibbonControl
        Me.chkControlPoliza.Name = "chkControlPoliza"
        Me.chkControlPoliza.Properties.OffText = "No"
        Me.chkControlPoliza.Properties.OnText = "Sí"
        Me.chkControlPoliza.Size = New System.Drawing.Size(95, 24)
        Me.chkControlPoliza.TabIndex = 17
        '
        'chkDevolucion
        '
        Me.chkDevolucion.Location = New System.Drawing.Point(211, 153)
        Me.chkDevolucion.MenuManager = Me.RibbonControl
        Me.chkDevolucion.Name = "chkDevolucion"
        Me.chkDevolucion.Properties.OffText = "No"
        Me.chkDevolucion.Properties.OnText = "Sí"
        Me.chkDevolucion.Size = New System.Drawing.Size(95, 24)
        Me.chkDevolucion.TabIndex = 16
        '
        'chkActivo
        '
        Me.chkActivo.EditValue = True
        Me.chkActivo.Location = New System.Drawing.Point(875, 91)
        Me.chkActivo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkActivo.MenuManager = Me.RibbonControl
        Me.chkActivo.Name = "chkActivo"
        Me.chkActivo.Properties.Caption = ""
        Me.chkActivo.Size = New System.Drawing.Size(41, 24)
        Me.chkActivo.TabIndex = 7
        '
        'lblCodigo
        '
        Me.lblCodigo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCodigo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblCodigo.Location = New System.Drawing.Point(211, 72)
        Me.lblCodigo.Name = "lblCodigo"
        Me.lblCodigo.Size = New System.Drawing.Size(226, 25)
        Me.lblCodigo.TabIndex = 1
        '
        'txtNombre
        '
        Me.txtNombre.Location = New System.Drawing.Point(211, 110)
        Me.txtNombre.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNombre.MenuManager = Me.RibbonControl
        Me.txtNombre.Name = "txtNombre"
        Me.txtNombre.Size = New System.Drawing.Size(226, 22)
        Me.txtNombre.TabIndex = 3
        '
        'Fec_agrDateEdit
        '
        Me.Fec_agrDateEdit.EditValue = Nothing
        Me.Fec_agrDateEdit.Enabled = False
        Me.Fec_agrDateEdit.Location = New System.Drawing.Point(195, 47)
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
        Me.User_agrTextEdit.Location = New System.Drawing.Point(195, 15)
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
        Me.Fec_modDateEdit.Location = New System.Drawing.Point(679, 47)
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
        Me.User_modTextEdit.Location = New System.Drawing.Point(679, 15)
        Me.User_modTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.User_modTextEdit.MenuManager = Me.RibbonControl
        Me.User_modTextEdit.Name = "User_modTextEdit"
        Me.User_modTextEdit.Size = New System.Drawing.Size(261, 22)
        Me.User_modTextEdit.TabIndex = 2
        '
        'dkOrdenCompraTI
        '
        Me.dkOrdenCompraTI.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerBottom})
        Me.dkOrdenCompraTI.Form = Me
        Me.dkOrdenCompraTI.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane"})
        '
        'hideContainerBottom
        '
        Me.hideContainerBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.hideContainerBottom.Controls.Add(Me.DockPanel1)
        Me.hideContainerBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 620)
        Me.hideContainerBottom.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(1128, 26)
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.ID = New System.Guid("8271ae84-b005-4add-8ac0-4c20bb905824")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 0)
        Me.DockPanel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(200, 96)
        Me.DockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.SavedIndex = 0
        Me.DockPanel1.Size = New System.Drawing.Size(1098, 118)
        Me.DockPanel1.Text = "Bitácora"
        Me.DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.Fec_agrDateEdit)
        Me.DockPanel1_Container.Controls.Add(Me.User_agrTextEdit)
        Me.DockPanel1_Container.Controls.Add(Fec_agrLabel)
        Me.DockPanel1_Container.Controls.Add(User_modLabel)
        Me.DockPanel1_Container.Controls.Add(Me.User_modTextEdit)
        Me.DockPanel1_Container.Controls.Add(User_agrLabel)
        Me.DockPanel1_Container.Controls.Add(Fec_modLabel)
        Me.DockPanel1_Container.Controls.Add(Me.Fec_modDateEdit)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(3, 31)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1091, 84)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'chkPermitirExcedenteLotes
        '
        Me.chkPermitirExcedenteLotes.Location = New System.Drawing.Point(472, 190)
        Me.chkPermitirExcedenteLotes.MenuManager = Me.RibbonControl
        Me.chkPermitirExcedenteLotes.Name = "chkPermitirExcedenteLotes"
        Me.chkPermitirExcedenteLotes.Properties.OffText = "No"
        Me.chkPermitirExcedenteLotes.Properties.OnText = "Sí"
        Me.chkPermitirExcedenteLotes.Size = New System.Drawing.Size(95, 24)
        Me.chkPermitirExcedenteLotes.TabIndex = 26
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(312, 194)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(153, 16)
        Label4.TabIndex = 25
        Label4.Text = "Permitir Excedente Lotes:"
        '
        'frmOrdenCompraTI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1128, 676)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmOrdenCompraTI"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Parámetros tipo de documento para ingresos"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.chkBloquearLotes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPreguntarEnBackOrder.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkGeneraTareaIngreso.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkEsPolizaConsolidada.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRequerirDocumentoRef.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkControlPoliza.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDevolucion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombre.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dkOrdenCompraTI, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerBottom.ResumeLayout(False)
        Me.DockPanel1.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        Me.DockPanel1_Container.PerformLayout()
        CType(Me.chkPermitirExcedenteLotes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents txtNombre As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_agrDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_agrTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_modDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_modTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblCodigo As System.Windows.Forms.Label
    Friend WithEvents chkActivo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents dkOrdenCompraTI As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents chkPreguntarEnBackOrder As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents chkGeneraTareaIngreso As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents chkEsPolizaConsolidada As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents chkRequerirDocumentoRef As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents chkControlPoliza As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents chkDevolucion As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents chkBloquearLotes As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents chkPermitirExcedenteLotes As DevExpress.XtraEditors.ToggleSwitch
End Class
