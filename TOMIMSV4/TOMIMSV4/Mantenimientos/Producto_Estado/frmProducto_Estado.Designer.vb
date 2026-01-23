<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmProducto_Estado
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If components IsNot Nothing Then
                components.Dispose()
            End If
            If pObjPE IsNot Nothing Then
                pObjPE.Dispose()
                pObjPE = Nothing
            End If
            If pObjEstadoProductoUbicacion IsNot Nothing Then
                pObjEstadoProductoUbicacion.Dispose()
            End If
            If gBeProductoEstado IsNot Nothing Then
                gBeProductoEstado.Dispose()
                gBeProductoEstado = Nothing
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
        Dim IdPropietarioLabel As System.Windows.Forms.Label
        Dim NombreLabel As System.Windows.Forms.Label
        Dim User_agrLabel As System.Windows.Forms.Label
        Dim Fec_agrLabel As System.Windows.Forms.Label
        Dim User_modLabel As System.Windows.Forms.Label
        Dim Fec_modLabel As System.Windows.Forms.Label
        Dim Label12 As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProducto_Estado))
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuAsignacion = New DevExpress.XtraBars.BarButtonItem()
        Me.chkActivo = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.chkSistema = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.chkUtilizable = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.nudToleranciaDiasVencimiento = New System.Windows.Forms.NumericUpDown()
        Me.nudDiasVencimientoClasificacion = New System.Windows.Forms.NumericUpDown()
        Me.txtCodigoBodegaERP = New DevExpress.XtraEditors.TextEdit()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.rdMalo = New System.Windows.Forms.RadioButton()
        Me.rdBueno = New System.Windows.Forms.RadioButton()
        Me.cmbPropietario = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtNombreUbicación = New DevExpress.XtraEditors.TextEdit()
        Me.txtIdUbicacion = New DevExpress.XtraEditors.TextEdit()
        Me.lnkClasificacion = New System.Windows.Forms.LinkLabel()
        Me.lblCodigo = New System.Windows.Forms.Label()
        Me.txtNombre = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_agrDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_agrTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_modDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_modTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Dgrid = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.chkEstadoUbicActivo = New DevExpress.XtraEditors.CheckEdit()
        Me.ToolStrip = New System.Windows.Forms.ToolStrip()
        Me.cmdAdd = New System.Windows.Forms.ToolStripButton()
        Me.cmdDelete = New System.Windows.Forms.ToolStripButton()
        Me.dkProducto_Estado = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.chkReservaRequiereUMBas = New DevExpress.XtraEditors.ToggleSwitch()
        IdPropietarioLabel = New System.Windows.Forms.Label()
        NombreLabel = New System.Windows.Forms.Label()
        User_agrLabel = New System.Windows.Forms.Label()
        Fec_agrLabel = New System.Windows.Forms.Label()
        User_modLabel = New System.Windows.Forms.Label()
        Fec_modLabel = New System.Windows.Forms.Label()
        Label12 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.nudToleranciaDiasVencimiento, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudDiasVencimientoClasificacion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodigoBodegaERP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreUbicación.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdUbicacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombre.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Dgrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        CType(Me.chkEstadoUbicActivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip.SuspendLayout()
        CType(Me.dkProducto_Estado, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerBottom.SuspendLayout()
        Me.DockPanel1.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.chkReservaRequiereUMBas.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'IdPropietarioLabel
        '
        IdPropietarioLabel.AutoSize = True
        IdPropietarioLabel.Location = New System.Drawing.Point(29, 74)
        IdPropietarioLabel.Name = "IdPropietarioLabel"
        IdPropietarioLabel.Size = New System.Drawing.Size(74, 16)
        IdPropietarioLabel.TabIndex = 2
        IdPropietarioLabel.Text = "Propietario:"
        '
        'NombreLabel
        '
        NombreLabel.AutoSize = True
        NombreLabel.Location = New System.Drawing.Point(29, 107)
        NombreLabel.Name = "NombreLabel"
        NombreLabel.Size = New System.Drawing.Size(57, 16)
        NombreLabel.TabIndex = 5
        NombreLabel.Text = "Nombre:"
        '
        'User_agrLabel
        '
        User_agrLabel.AutoSize = True
        User_agrLabel.Location = New System.Drawing.Point(35, 18)
        User_agrLabel.Name = "User_agrLabel"
        User_agrLabel.Size = New System.Drawing.Size(100, 16)
        User_agrLabel.TabIndex = 0
        User_agrLabel.Text = "Usuario Agregó:"
        '
        'Fec_agrLabel
        '
        Fec_agrLabel.AutoSize = True
        Fec_agrLabel.Location = New System.Drawing.Point(35, 50)
        Fec_agrLabel.Name = "Fec_agrLabel"
        Fec_agrLabel.Size = New System.Drawing.Size(91, 16)
        Fec_agrLabel.TabIndex = 4
        Fec_agrLabel.Text = "Fecha Agregó:"
        '
        'User_modLabel
        '
        User_modLabel.AutoSize = True
        User_modLabel.Location = New System.Drawing.Point(519, 18)
        User_modLabel.Name = "User_modLabel"
        User_modLabel.Size = New System.Drawing.Size(106, 16)
        User_modLabel.TabIndex = 3
        User_modLabel.Text = "Usuario Modificó:"
        '
        'Fec_modLabel
        '
        Fec_modLabel.AutoSize = True
        Fec_modLabel.Location = New System.Drawing.Point(519, 50)
        Fec_modLabel.Name = "Fec_modLabel"
        Fec_modLabel.Size = New System.Drawing.Size(97, 16)
        Fec_modLabel.TabIndex = 6
        Fec_modLabel.Text = "Fecha Modificó:"
        '
        'Label12
        '
        Label12.AutoSize = True
        Label12.Location = New System.Drawing.Point(29, 44)
        Label12.Name = "Label12"
        Label12.Size = New System.Drawing.Size(46, 16)
        Label12.TabIndex = 0
        Label12.Text = "Código"
        '
        'Label2
        '
        Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(980, 36)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(46, 16)
        Label2.TabIndex = 1
        Label2.Text = "Activo:"
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(28, 169)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(123, 16)
        Label1.TabIndex = 16
        Label1.Text = "Código Bodega ERP:"
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.Location = New System.Drawing.Point(29, 202)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(108, 16)
        Label3.TabIndex = 18
        Label3.Text = "Días vencimiento:"
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(28, 234)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(171, 16)
        Label4.TabIndex = 19
        Label4.Text = "Tolerancia días vencimiento:"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuGuardar, Me.mnuActualizar, Me.mnuEliminar, Me.mnuAsignacion, Me.chkActivo, Me.chkSistema, Me.chkUtilizable})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 10
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
        'chkActivo
        '
        Me.chkActivo.Caption = "Activo"
        Me.chkActivo.Id = 5
        Me.chkActivo.Name = "chkActivo"
        '
        'chkSistema
        '
        Me.chkSistema.Caption = "Sistema"
        Me.chkSistema.Id = 6
        Me.chkSistema.Name = "chkSistema"
        '
        'chkUtilizable
        '
        Me.chkUtilizable.Caption = "Utilizable"
        Me.chkUtilizable.Id = 9
        Me.chkUtilizable.Name = "chkUtilizable"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1, Me.RibbonPageGroup2})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Menu"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEliminar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonPageGroup2
        '
        Me.RibbonPageGroup2.ItemLinks.Add(Me.chkActivo)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.chkSistema)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.chkUtilizable)
        Me.RibbonPageGroup2.Name = "RibbonPageGroup2"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 1005)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1072, 30)
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.chkReservaRequiereUMBas)
        Me.GroupControl1.Controls.Add(Me.nudToleranciaDiasVencimiento)
        Me.GroupControl1.Controls.Add(Me.nudDiasVencimientoClasificacion)
        Me.GroupControl1.Controls.Add(Label4)
        Me.GroupControl1.Controls.Add(Label3)
        Me.GroupControl1.Controls.Add(Label1)
        Me.GroupControl1.Controls.Add(Me.txtCodigoBodegaERP)
        Me.GroupControl1.Controls.Add(Me.GroupControl2)
        Me.GroupControl1.Controls.Add(Me.cmbPropietario)
        Me.GroupControl1.Controls.Add(Me.txtNombreUbicación)
        Me.GroupControl1.Controls.Add(Me.txtIdUbicacion)
        Me.GroupControl1.Controls.Add(Me.lnkClasificacion)
        Me.GroupControl1.Controls.Add(Me.lblCodigo)
        Me.GroupControl1.Controls.Add(Label12)
        Me.GroupControl1.Controls.Add(IdPropietarioLabel)
        Me.GroupControl1.Controls.Add(NombreLabel)
        Me.GroupControl1.Controls.Add(Me.txtNombre)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl1.Location = New System.Drawing.Point(0, 193)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1072, 278)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Datos Estado Producto"
        '
        'nudToleranciaDiasVencimiento
        '
        Me.nudToleranciaDiasVencimiento.Location = New System.Drawing.Point(206, 234)
        Me.nudToleranciaDiasVencimiento.Name = "nudToleranciaDiasVencimiento"
        Me.nudToleranciaDiasVencimiento.Size = New System.Drawing.Size(132, 23)
        Me.nudToleranciaDiasVencimiento.TabIndex = 21
        '
        'nudDiasVencimientoClasificacion
        '
        Me.nudDiasVencimientoClasificacion.Location = New System.Drawing.Point(206, 195)
        Me.nudDiasVencimientoClasificacion.Name = "nudDiasVencimientoClasificacion"
        Me.nudDiasVencimientoClasificacion.Size = New System.Drawing.Size(132, 23)
        Me.nudDiasVencimientoClasificacion.TabIndex = 20
        '
        'txtCodigoBodegaERP
        '
        Me.txtCodigoBodegaERP.Location = New System.Drawing.Point(206, 165)
        Me.txtCodigoBodegaERP.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCodigoBodegaERP.MenuManager = Me.RibbonControl
        Me.txtCodigoBodegaERP.Name = "txtCodigoBodegaERP"
        Me.txtCodigoBodegaERP.Properties.Appearance.BackColor = System.Drawing.Color.AliceBlue
        Me.txtCodigoBodegaERP.Properties.Appearance.Options.UseBackColor = True
        Me.txtCodigoBodegaERP.Properties.MaxLength = 30
        Me.txtCodigoBodegaERP.Size = New System.Drawing.Size(419, 22)
        Me.txtCodigoBodegaERP.TabIndex = 17
        Me.txtCodigoBodegaERP.ToolTip = "Código de la bodega de SAP (Interface)"
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.rdMalo)
        Me.GroupControl2.Controls.Add(Me.rdBueno)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Right
        Me.GroupControl2.Location = New System.Drawing.Point(783, 28)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(287, 248)
        Me.GroupControl2.TabIndex = 15
        Me.GroupControl2.Text = "Flujo de disponibilidad"
        '
        'rdMalo
        '
        Me.rdMalo.AutoSize = True
        Me.rdMalo.Location = New System.Drawing.Point(28, 95)
        Me.rdMalo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.rdMalo.Name = "rdMalo"
        Me.rdMalo.Size = New System.Drawing.Size(194, 20)
        Me.rdMalo.TabIndex = 6
        Me.rdMalo.TabStop = True
        Me.rdMalo.Text = "No Disponible (Dañado/Malo)"
        Me.rdMalo.UseVisualStyleBackColor = True
        '
        'rdBueno
        '
        Me.rdBueno.AutoSize = True
        Me.rdBueno.Checked = True
        Me.rdBueno.Location = New System.Drawing.Point(28, 64)
        Me.rdBueno.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.rdBueno.Name = "rdBueno"
        Me.rdBueno.Size = New System.Drawing.Size(234, 20)
        Me.rdBueno.TabIndex = 4
        Me.rdBueno.TabStop = True
        Me.rdBueno.Text = "Disponible (Buen Estado / Utilizable)"
        Me.rdBueno.UseVisualStyleBackColor = True
        '
        'cmbPropietario
        '
        Me.cmbPropietario.Location = New System.Drawing.Point(206, 71)
        Me.cmbPropietario.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbPropietario.MenuManager = Me.RibbonControl
        Me.cmbPropietario.Name = "cmbPropietario"
        Me.cmbPropietario.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPropietario.Properties.NullText = ""
        Me.cmbPropietario.Size = New System.Drawing.Size(419, 22)
        Me.cmbPropietario.TabIndex = 14
        '
        'txtNombreUbicación
        '
        Me.txtNombreUbicación.CausesValidation = False
        Me.txtNombreUbicación.Location = New System.Drawing.Point(296, 135)
        Me.txtNombreUbicación.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNombreUbicación.MenuManager = Me.RibbonControl
        Me.txtNombreUbicación.Name = "txtNombreUbicación"
        Me.txtNombreUbicación.Properties.ReadOnly = True
        Me.txtNombreUbicación.Size = New System.Drawing.Size(326, 22)
        Me.txtNombreUbicación.TabIndex = 11
        '
        'txtIdUbicacion
        '
        Me.txtIdUbicacion.Location = New System.Drawing.Point(206, 135)
        Me.txtIdUbicacion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtIdUbicacion.MenuManager = Me.RibbonControl
        Me.txtIdUbicacion.Name = "txtIdUbicacion"
        Me.txtIdUbicacion.Properties.Mask.EditMask = "n0"
        Me.txtIdUbicacion.Size = New System.Drawing.Size(82, 22)
        Me.txtIdUbicacion.TabIndex = 10
        '
        'lnkClasificacion
        '
        Me.lnkClasificacion.AutoSize = True
        Me.lnkClasificacion.Location = New System.Drawing.Point(28, 139)
        Me.lnkClasificacion.Name = "lnkClasificacion"
        Me.lnkClasificacion.Size = New System.Drawing.Size(108, 16)
        Me.lnkClasificacion.TabIndex = 8
        Me.lnkClasificacion.TabStop = True
        Me.lnkClasificacion.Text = "Ubicación Defecto"
        '
        'lblCodigo
        '
        Me.lblCodigo.AutoSize = True
        Me.lblCodigo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCodigo.Location = New System.Drawing.Point(164, 44)
        Me.lblCodigo.Name = "lblCodigo"
        Me.lblCodigo.Size = New System.Drawing.Size(0, 17)
        Me.lblCodigo.TabIndex = 1
        '
        'txtNombre
        '
        Me.txtNombre.Location = New System.Drawing.Point(206, 103)
        Me.txtNombre.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNombre.MenuManager = Me.RibbonControl
        Me.txtNombre.Name = "txtNombre"
        Me.txtNombre.Properties.MaxLength = 30
        Me.txtNombre.Size = New System.Drawing.Size(416, 22)
        Me.txtNombre.TabIndex = 7
        '
        'Fec_agrDateEdit
        '
        Me.Fec_agrDateEdit.EditValue = Nothing
        Me.Fec_agrDateEdit.Enabled = False
        Me.Fec_agrDateEdit.Location = New System.Drawing.Point(138, 47)
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
        Me.User_agrTextEdit.Location = New System.Drawing.Point(138, 15)
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
        Me.Fec_modDateEdit.Location = New System.Drawing.Point(622, 47)
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
        Me.User_modTextEdit.Location = New System.Drawing.Point(622, 15)
        Me.User_modTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.User_modTextEdit.MenuManager = Me.RibbonControl
        Me.User_modTextEdit.Name = "User_modTextEdit"
        Me.User_modTextEdit.Size = New System.Drawing.Size(261, 22)
        Me.User_modTextEdit.TabIndex = 2
        '
        'Dgrid
        '
        Me.Dgrid.Cursor = System.Windows.Forms.Cursors.Default
        Me.Dgrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Dgrid.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        GridLevelNode1.RelationName = "Level1"
        Me.Dgrid.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.Dgrid.Location = New System.Drawing.Point(2, 55)
        Me.Dgrid.MainView = Me.GridView1
        Me.Dgrid.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Dgrid.MenuManager = Me.RibbonControl
        Me.Dgrid.Name = "Dgrid"
        Me.Dgrid.Size = New System.Drawing.Size(1068, 451)
        Me.Dgrid.TabIndex = 3
        Me.Dgrid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.DetailHeight = 431
        Me.GridView1.GridControl = Me.Dgrid
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView1.OptionsFind.AllowFindPanel = False
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'GroupControl3
        '
        Me.GroupControl3.Controls.Add(Me.Dgrid)
        Me.GroupControl3.Controls.Add(Label2)
        Me.GroupControl3.Controls.Add(Me.chkEstadoUbicActivo)
        Me.GroupControl3.Controls.Add(Me.ToolStrip)
        Me.GroupControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl3.Location = New System.Drawing.Point(0, 471)
        Me.GroupControl3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(1072, 508)
        Me.GroupControl3.TabIndex = 1
        Me.GroupControl3.Text = "Ubicaciones por Defecto"
        '
        'chkEstadoUbicActivo
        '
        Me.chkEstadoUbicActivo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkEstadoUbicActivo.EditValue = True
        Me.chkEstadoUbicActivo.Location = New System.Drawing.Point(1035, 33)
        Me.chkEstadoUbicActivo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkEstadoUbicActivo.MenuManager = Me.RibbonControl
        Me.chkEstadoUbicActivo.Name = "chkEstadoUbicActivo"
        Me.chkEstadoUbicActivo.Properties.Caption = ""
        Me.chkEstadoUbicActivo.Size = New System.Drawing.Size(27, 24)
        Me.chkEstadoUbicActivo.TabIndex = 2
        '
        'ToolStrip
        '
        Me.ToolStrip.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdAdd, Me.cmdDelete})
        Me.ToolStrip.Location = New System.Drawing.Point(2, 28)
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.Size = New System.Drawing.Size(1068, 27)
        Me.ToolStrip.TabIndex = 0
        Me.ToolStrip.Text = "ToolStrip4"
        '
        'cmdAdd
        '
        Me.cmdAdd.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAdd.Image = Global.TOMWMS.My.Resources.Resources.greencheck
        Me.cmdAdd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(90, 24)
        Me.cmdAdd.Text = "Guardar"
        '
        'cmdDelete
        '
        Me.cmdDelete.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.Image = CType(resources.GetObject("cmdDelete.Image"), System.Drawing.Image)
        Me.cmdDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(90, 24)
        Me.cmdDelete.Text = "Eliminar"
        '
        'dkProducto_Estado
        '
        Me.dkProducto_Estado.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerBottom})
        Me.dkProducto_Estado.Form = Me
        Me.dkProducto_Estado.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane"})
        '
        'hideContainerBottom
        '
        Me.hideContainerBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.hideContainerBottom.Controls.Add(Me.DockPanel1)
        Me.hideContainerBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 979)
        Me.hideContainerBottom.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(1072, 26)
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.ID = New System.Guid("99ad9000-2e7b-4d4b-9875-9ab99ba2581f")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 0)
        Me.DockPanel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(200, 96)
        Me.DockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.SavedIndex = 0
        Me.DockPanel1.Size = New System.Drawing.Size(953, 118)
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
        Me.DockPanel1_Container.Size = New System.Drawing.Size(946, 84)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'chkReservaRequiereUMBas
        '
        Me.chkReservaRequiereUMBas.Location = New System.Drawing.Point(389, 230)
        Me.chkReservaRequiereUMBas.MenuManager = Me.RibbonControl
        Me.chkReservaRequiereUMBas.Name = "chkReservaRequiereUMBas"
        Me.chkReservaRequiereUMBas.Properties.OffText = "Reserva no requiere UM Bas"
        Me.chkReservaRequiereUMBas.Properties.OnText = "Reserva requiere UM Bas"
        Me.chkReservaRequiereUMBas.Size = New System.Drawing.Size(236, 24)
        Me.chkReservaRequiereUMBas.TabIndex = 24
        '
        'frmProducto_Estado
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1072, 1035)
        Me.Controls.Add(Me.GroupControl3)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmProducto_Estado"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Estado Producto "
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.nudToleranciaDiasVencimiento, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudDiasVencimientoClasificacion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodigoBodegaERP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.GroupControl2.PerformLayout()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreUbicación.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdUbicacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombre.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Dgrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        Me.GroupControl3.PerformLayout()
        CType(Me.chkEstadoUbicActivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip.ResumeLayout(False)
        Me.ToolStrip.PerformLayout()
        CType(Me.dkProducto_Estado, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerBottom.ResumeLayout(False)
        Me.DockPanel1.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        Me.DockPanel1_Container.PerformLayout()
        CType(Me.chkReservaRequiereUMBas.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents lnkClasificacion As System.Windows.Forms.LinkLabel
    Friend WithEvents txtNombreUbicación As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtIdUbicacion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents rdMalo As System.Windows.Forms.RadioButton
    Friend WithEvents rdBueno As System.Windows.Forms.RadioButton
    Friend WithEvents Dgrid As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ToolStrip As ToolStrip
    Friend WithEvents cmdAdd As ToolStripButton
    Friend WithEvents cmdDelete As ToolStripButton
    Friend WithEvents chkEstadoUbicActivo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents dkProducto_Estado As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents cmbPropietario As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents chkActivo As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents chkSistema As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents RibbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents chkUtilizable As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents txtCodigoBodegaERP As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents nudToleranciaDiasVencimiento As NumericUpDown
    Friend WithEvents nudDiasVencimientoClasificacion As NumericUpDown
    Friend WithEvents chkReservaRequiereUMBas As DevExpress.XtraEditors.ToggleSwitch
End Class
