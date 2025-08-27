<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmFont_Tramo
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If components IsNot Nothing Then
                    components.Dispose()
                End If
                If pObjBeFE IsNot Nothing Then
                    pObjBeFE.Dispose()
                    pObjBeFE = Nothing
                End If
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim Fec_agrLabel As System.Windows.Forms.Label
        Dim User_agrLabel As System.Windows.Forms.Label
        Dim Fec_modLabel As System.Windows.Forms.Label
        Dim User_modLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFont_Tramo))
        Me.FontDialog1 = New System.Windows.Forms.FontDialog()
        Me.cmdSeleccionarFont = New System.Windows.Forms.Button()
        Me.txtFont = New System.Windows.Forms.TextBox()
        Me.txtSize = New System.Windows.Forms.TextBox()
        Me.chkNegrita = New System.Windows.Forms.CheckBox()
        Me.chkCursiva = New System.Windows.Forms.CheckBox()
        Me.chkSubrayado = New System.Windows.Forms.CheckBox()
        Me.txtColor = New System.Windows.Forms.TextBox()
        Me.txtFondo = New System.Windows.Forms.TextBox()
        Me.lblLetra = New System.Windows.Forms.Label()
        Me.lblTamaño = New System.Windows.Forms.Label()
        Me.lblColorFont = New System.Windows.Forms.Label()
        Me.lblColorFondo = New System.Windows.Forms.Label()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl4 = New DevExpress.XtraEditors.GroupControl()
        Me.cmdSeleccionarColor = New System.Windows.Forms.Button()
        Me.lblSubrayado = New System.Windows.Forms.Label()
        Me.lblCursiva = New System.Windows.Forms.Label()
        Me.lblNegrita = New System.Windows.Forms.Label()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.txtDescripcion = New System.Windows.Forms.TextBox()
        Me.lblNombre = New System.Windows.Forms.Label()
        Me.lblCod = New System.Windows.Forms.Label()
        Me.lblCodigo = New System.Windows.Forms.Label()
        Me.Fec_agrDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_agrTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_modDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_modTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.dkFontTramo = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Fec_agrLabel = New System.Windows.Forms.Label()
        User_agrLabel = New System.Windows.Forms.Label()
        Fec_modLabel = New System.Windows.Forms.Label()
        User_modLabel = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl4.SuspendLayout()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dkFontTramo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerBottom.SuspendLayout()
        Me.DockPanel1.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        Me.SuspendLayout()
        '
        'Fec_agrLabel
        '
        Fec_agrLabel.AutoSize = True
        Fec_agrLabel.Location = New System.Drawing.Point(42, 39)
        Fec_agrLabel.Name = "Fec_agrLabel"
        Fec_agrLabel.Size = New System.Drawing.Size(97, 17)
        Fec_agrLabel.TabIndex = 4
        Fec_agrLabel.Text = "Fecha Agregó:"
        '
        'User_agrLabel
        '
        User_agrLabel.AutoSize = True
        User_agrLabel.Location = New System.Drawing.Point(42, 7)
        User_agrLabel.Name = "User_agrLabel"
        User_agrLabel.Size = New System.Drawing.Size(106, 17)
        User_agrLabel.TabIndex = 0
        User_agrLabel.Text = "Usuario Agregó:"
        '
        'Fec_modLabel
        '
        Fec_modLabel.AutoSize = True
        Fec_modLabel.Location = New System.Drawing.Point(526, 39)
        Fec_modLabel.Name = "Fec_modLabel"
        Fec_modLabel.Size = New System.Drawing.Size(102, 17)
        Fec_modLabel.TabIndex = 6
        Fec_modLabel.Text = "Fecha Modificó:"
        '
        'User_modLabel
        '
        User_modLabel.AutoSize = True
        User_modLabel.Location = New System.Drawing.Point(526, 7)
        User_modLabel.Name = "User_modLabel"
        User_modLabel.Size = New System.Drawing.Size(111, 17)
        User_modLabel.TabIndex = 3
        User_modLabel.Text = "Usuario Modificó:"
        '
        'FontDialog1
        '
        Me.FontDialog1.FontMustExist = True
        Me.FontDialog1.ScriptsOnly = True
        Me.FontDialog1.ShowApply = True
        Me.FontDialog1.ShowColor = True
        Me.FontDialog1.ShowHelp = True
        '
        'cmdSeleccionarFont
        '
        Me.cmdSeleccionarFont.Location = New System.Drawing.Point(360, 36)
        Me.cmdSeleccionarFont.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmdSeleccionarFont.Name = "cmdSeleccionarFont"
        Me.cmdSeleccionarFont.Size = New System.Drawing.Size(139, 28)
        Me.cmdSeleccionarFont.TabIndex = 1
        Me.cmdSeleccionarFont.Text = "Seleccionar Font"
        Me.cmdSeleccionarFont.UseVisualStyleBackColor = True
        '
        'txtFont
        '
        Me.txtFont.Location = New System.Drawing.Point(100, 38)
        Me.txtFont.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtFont.Name = "txtFont"
        Me.txtFont.Size = New System.Drawing.Size(238, 23)
        Me.txtFont.TabIndex = 0
        '
        'txtSize
        '
        Me.txtSize.Location = New System.Drawing.Point(100, 71)
        Me.txtSize.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtSize.Name = "txtSize"
        Me.txtSize.Size = New System.Drawing.Size(238, 23)
        Me.txtSize.TabIndex = 4
        '
        'chkNegrita
        '
        Me.chkNegrita.AutoSize = True
        Me.chkNegrita.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkNegrita.Location = New System.Drawing.Point(97, 114)
        Me.chkNegrita.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkNegrita.Name = "chkNegrita"
        Me.chkNegrita.Size = New System.Drawing.Size(18, 17)
        Me.chkNegrita.TabIndex = 5
        Me.chkNegrita.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkNegrita.UseVisualStyleBackColor = True
        '
        'chkCursiva
        '
        Me.chkCursiva.AutoSize = True
        Me.chkCursiva.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkCursiva.Location = New System.Drawing.Point(97, 143)
        Me.chkCursiva.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkCursiva.Name = "chkCursiva"
        Me.chkCursiva.Size = New System.Drawing.Size(18, 17)
        Me.chkCursiva.TabIndex = 7
        Me.chkCursiva.UseVisualStyleBackColor = True
        '
        'chkSubrayado
        '
        Me.chkSubrayado.AutoSize = True
        Me.chkSubrayado.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkSubrayado.Location = New System.Drawing.Point(97, 178)
        Me.chkSubrayado.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkSubrayado.Name = "chkSubrayado"
        Me.chkSubrayado.Size = New System.Drawing.Size(18, 17)
        Me.chkSubrayado.TabIndex = 9
        Me.chkSubrayado.UseVisualStyleBackColor = True
        '
        'txtColor
        '
        Me.txtColor.Location = New System.Drawing.Point(97, 215)
        Me.txtColor.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtColor.Name = "txtColor"
        Me.txtColor.Size = New System.Drawing.Size(238, 23)
        Me.txtColor.TabIndex = 12
        '
        'txtFondo
        '
        Me.txtFondo.Location = New System.Drawing.Point(97, 258)
        Me.txtFondo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtFondo.Name = "txtFondo"
        Me.txtFondo.Size = New System.Drawing.Size(238, 23)
        Me.txtFondo.TabIndex = 14
        '
        'lblLetra
        '
        Me.lblLetra.AutoSize = True
        Me.lblLetra.Location = New System.Drawing.Point(15, 52)
        Me.lblLetra.Name = "lblLetra"
        Me.lblLetra.Size = New System.Drawing.Size(44, 17)
        Me.lblLetra.TabIndex = 2
        Me.lblLetra.Text = "Letra:"
        '
        'lblTamaño
        '
        Me.lblTamaño.AutoSize = True
        Me.lblTamaño.Location = New System.Drawing.Point(15, 79)
        Me.lblTamaño.Name = "lblTamaño"
        Me.lblTamaño.Size = New System.Drawing.Size(63, 17)
        Me.lblTamaño.TabIndex = 3
        Me.lblTamaño.Text = "Tamaño:"
        '
        'lblColorFont
        '
        Me.lblColorFont.AutoSize = True
        Me.lblColorFont.Location = New System.Drawing.Point(15, 223)
        Me.lblColorFont.Name = "lblColorFont"
        Me.lblColorFont.Size = New System.Drawing.Size(77, 17)
        Me.lblColorFont.TabIndex = 11
        Me.lblColorFont.Text = "Color Font:"
        '
        'lblColorFondo
        '
        Me.lblColorFondo.AutoSize = True
        Me.lblColorFondo.Location = New System.Drawing.Point(15, 262)
        Me.lblColorFondo.Name = "lblColorFondo"
        Me.lblColorFondo.Size = New System.Drawing.Size(88, 17)
        Me.lblColorFondo.TabIndex = 13
        Me.lblColorFondo.Text = "Color Fondo:"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuGuardar, Me.mnuActualizar, Me.mnuEliminar, Me.RibbonControl.SearchEditItem})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 5
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(943, 193)
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
        Me.RibbonPage1.Text = "Font Tramo"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEliminar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.GroupControl4)
        Me.GroupControl1.Controls.Add(Me.GroupControl3)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 193)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(943, 662)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Font"
        '
        'GroupControl4
        '
        Me.GroupControl4.Controls.Add(Me.txtFont)
        Me.GroupControl4.Controls.Add(Me.chkNegrita)
        Me.GroupControl4.Controls.Add(Me.chkSubrayado)
        Me.GroupControl4.Controls.Add(Me.cmdSeleccionarColor)
        Me.GroupControl4.Controls.Add(Me.lblSubrayado)
        Me.GroupControl4.Controls.Add(Me.cmdSeleccionarFont)
        Me.GroupControl4.Controls.Add(Me.chkCursiva)
        Me.GroupControl4.Controls.Add(Me.lblCursiva)
        Me.GroupControl4.Controls.Add(Me.txtColor)
        Me.GroupControl4.Controls.Add(Me.lblNegrita)
        Me.GroupControl4.Controls.Add(Me.txtSize)
        Me.GroupControl4.Controls.Add(Me.txtFondo)
        Me.GroupControl4.Controls.Add(Me.lblTamaño)
        Me.GroupControl4.Controls.Add(Me.lblColorFont)
        Me.GroupControl4.Controls.Add(Me.lblColorFondo)
        Me.GroupControl4.Controls.Add(Me.lblLetra)
        Me.GroupControl4.Location = New System.Drawing.Point(37, 187)
        Me.GroupControl4.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl4.Name = "GroupControl4"
        Me.GroupControl4.Size = New System.Drawing.Size(505, 341)
        Me.GroupControl4.TabIndex = 1
        Me.GroupControl4.Text = "Detalle"
        '
        'cmdSeleccionarColor
        '
        Me.cmdSeleccionarColor.Location = New System.Drawing.Point(360, 256)
        Me.cmdSeleccionarColor.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmdSeleccionarColor.Name = "cmdSeleccionarColor"
        Me.cmdSeleccionarColor.Size = New System.Drawing.Size(139, 28)
        Me.cmdSeleccionarColor.TabIndex = 15
        Me.cmdSeleccionarColor.Text = "Seleccionar Color"
        Me.cmdSeleccionarColor.UseVisualStyleBackColor = True
        '
        'lblSubrayado
        '
        Me.lblSubrayado.AutoSize = True
        Me.lblSubrayado.Location = New System.Drawing.Point(15, 182)
        Me.lblSubrayado.Name = "lblSubrayado"
        Me.lblSubrayado.Size = New System.Drawing.Size(80, 17)
        Me.lblSubrayado.TabIndex = 10
        Me.lblSubrayado.Text = "Subrayado:"
        '
        'lblCursiva
        '
        Me.lblCursiva.AutoSize = True
        Me.lblCursiva.Location = New System.Drawing.Point(15, 148)
        Me.lblCursiva.Name = "lblCursiva"
        Me.lblCursiva.Size = New System.Drawing.Size(58, 17)
        Me.lblCursiva.TabIndex = 8
        Me.lblCursiva.Text = "Cursiva:"
        '
        'lblNegrita
        '
        Me.lblNegrita.AutoSize = True
        Me.lblNegrita.Location = New System.Drawing.Point(15, 118)
        Me.lblNegrita.Name = "lblNegrita"
        Me.lblNegrita.Size = New System.Drawing.Size(60, 17)
        Me.lblNegrita.TabIndex = 6
        Me.lblNegrita.Text = "Negrita: "
        '
        'GroupControl3
        '
        Me.GroupControl3.Controls.Add(Me.txtDescripcion)
        Me.GroupControl3.Controls.Add(Me.lblNombre)
        Me.GroupControl3.Controls.Add(Me.lblCod)
        Me.GroupControl3.Controls.Add(Me.lblCodigo)
        Me.GroupControl3.Location = New System.Drawing.Point(37, 46)
        Me.GroupControl3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(505, 122)
        Me.GroupControl3.TabIndex = 0
        Me.GroupControl3.Text = "Encabezado"
        '
        'txtDescripcion
        '
        Me.txtDescripcion.Location = New System.Drawing.Point(75, 70)
        Me.txtDescripcion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.Size = New System.Drawing.Size(207, 23)
        Me.txtDescripcion.TabIndex = 3
        '
        'lblNombre
        '
        Me.lblNombre.AutoSize = True
        Me.lblNombre.Location = New System.Drawing.Point(17, 75)
        Me.lblNombre.Name = "lblNombre"
        Me.lblNombre.Size = New System.Drawing.Size(62, 17)
        Me.lblNombre.TabIndex = 2
        Me.lblNombre.Text = "Nombre:"
        '
        'lblCod
        '
        Me.lblCod.AutoSize = True
        Me.lblCod.Location = New System.Drawing.Point(75, 41)
        Me.lblCod.Name = "lblCod"
        Me.lblCod.Size = New System.Drawing.Size(33, 17)
        Me.lblCod.TabIndex = 1
        Me.lblCod.Text = "Cod"
        '
        'lblCodigo
        '
        Me.lblCodigo.AutoSize = True
        Me.lblCodigo.Location = New System.Drawing.Point(17, 36)
        Me.lblCodigo.Name = "lblCodigo"
        Me.lblCodigo.Size = New System.Drawing.Size(60, 17)
        Me.lblCodigo.TabIndex = 0
        Me.lblCodigo.Text = "Código: "
        '
        'Fec_agrDateEdit
        '
        Me.Fec_agrDateEdit.EditValue = Nothing
        Me.Fec_agrDateEdit.Enabled = False
        Me.Fec_agrDateEdit.Location = New System.Drawing.Point(145, 36)
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
        Me.User_agrTextEdit.Location = New System.Drawing.Point(145, 4)
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
        Me.Fec_modDateEdit.Location = New System.Drawing.Point(629, 36)
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
        Me.User_modTextEdit.Location = New System.Drawing.Point(629, 4)
        Me.User_modTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.User_modTextEdit.MenuManager = Me.RibbonControl
        Me.User_modTextEdit.Name = "User_modTextEdit"
        Me.User_modTextEdit.Size = New System.Drawing.Size(261, 22)
        Me.User_modTextEdit.TabIndex = 2
        '
        'dkFontTramo
        '
        Me.dkFontTramo.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerBottom})
        Me.dkFontTramo.Form = Me
        Me.dkFontTramo.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane"})
        '
        'hideContainerBottom
        '
        Me.hideContainerBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.hideContainerBottom.Controls.Add(Me.DockPanel1)
        Me.hideContainerBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 855)
        Me.hideContainerBottom.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(943, 26)
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.ID = New System.Guid("cf0f0dbd-a299-4796-a875-f72c259f6e42")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 0)
        Me.DockPanel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(200, 90)
        Me.DockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.SavedIndex = 0
        Me.DockPanel1.Size = New System.Drawing.Size(943, 111)
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
        Me.DockPanel1_Container.Location = New System.Drawing.Point(5, 28)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(933, 78)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'frmFont_Tramo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(943, 881)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmFont_Tramo"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Font Tramo"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl4.ResumeLayout(False)
        Me.GroupControl4.PerformLayout()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        Me.GroupControl3.PerformLayout()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dkFontTramo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerBottom.ResumeLayout(False)
        Me.DockPanel1.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        Me.DockPanel1_Container.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout

End Sub

    Friend WithEvents FontDialog1 As FontDialog
    Friend WithEvents cmdSeleccionarFont As Button
    Friend WithEvents txtFont As TextBox
    Friend WithEvents txtSize As TextBox
    Friend WithEvents chkNegrita As CheckBox
    Friend WithEvents chkCursiva As CheckBox
    Friend WithEvents chkSubrayado As CheckBox
    Friend WithEvents txtColor As TextBox
    Friend WithEvents txtFondo As TextBox
    Friend WithEvents lblLetra As Label
    Friend WithEvents lblTamaño As Label
    Friend WithEvents lblColorFont As Label
    Friend WithEvents lblColorFondo As Label
    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents mnuGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEliminar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cmdSeleccionarColor As Button
    Friend WithEvents ColorDialog1 As ColorDialog
    Friend WithEvents lblNegrita As Label
    Friend WithEvents lblSubrayado As Label
    Friend WithEvents lblCursiva As Label
    Friend WithEvents lblNombre As Label
    Friend WithEvents txtDescripcion As TextBox
    Friend WithEvents lblCod As Label
    Friend WithEvents lblCodigo As Label
    Friend WithEvents Fec_agrDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_agrTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_modDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_modTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GroupControl4 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dkFontTramo As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
End Class
