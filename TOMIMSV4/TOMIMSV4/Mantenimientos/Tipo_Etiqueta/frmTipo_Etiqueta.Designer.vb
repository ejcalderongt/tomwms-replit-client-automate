<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmTipo_Etiqueta
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
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
        Dim lblActivo As System.Windows.Forms.Label
        Dim lblMargenInf As System.Windows.Forms.Label
        Dim lblMargenSup As System.Windows.Forms.Label
        Dim lblMargenDer As System.Windows.Forms.Label
        Dim lblMargenIzq As System.Windows.Forms.Label
        Dim lblAncho As System.Windows.Forms.Label
        Dim lblAlto As System.Windows.Forms.Label
        Dim lblCod As System.Windows.Forms.Label
        Dim lblNombre As System.Windows.Forms.Label
        Dim Fec_agrLabel As System.Windows.Forms.Label
        Dim User_agrLabel As System.Windows.Forms.Label
        Dim Fec_modLabel As System.Windows.Forms.Label
        Dim User_modLabel As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim Label5 As System.Windows.Forms.Label
        Dim Label10 As System.Windows.Forms.Label
        Dim lblInkjet As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTipo_Etiqueta))
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chkActivo = New DevExpress.XtraEditors.CheckEdit()
        Me.txtMargenInf = New System.Windows.Forms.NumericUpDown()
        Me.txtMargenSup = New System.Windows.Forms.NumericUpDown()
        Me.txtMargenDer = New System.Windows.Forms.NumericUpDown()
        Me.txtMargenIzq = New System.Windows.Forms.NumericUpDown()
        Me.txtAncho = New System.Windows.Forms.NumericUpDown()
        Me.txtAlto = New System.Windows.Forms.NumericUpDown()
        Me.lblCodigo = New System.Windows.Forms.Label()
        Me.txtNombre = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_agrDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_agrTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_modDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_modTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.dkTipoEtiqueta = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkInkjet = New DevExpress.XtraEditors.CheckEdit()
        Me.cmbClasificacionEtiqueta = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbDPI = New DevExpress.XtraEditors.LookUpEdit()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblpAlto = New DevExpress.XtraEditors.LabelControl()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lblpAncho = New DevExpress.XtraEditors.LabelControl()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.pbZPLImage = New System.Windows.Forms.PictureBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnGenerarImagen = New DevExpress.XtraEditors.SimpleButton()
        Me.txtZPL = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.XtraTabControl1 = New DevExpress.XtraTab.XtraTabControl()
        Me.tabZPL = New DevExpress.XtraTab.XtraTabPage()
        Me.tabTexto = New DevExpress.XtraTab.XtraTabPage()
        Me.btnAgregarTexto = New DevExpress.XtraEditors.SimpleButton()
        Me.btnImprimir = New DevExpress.XtraEditors.SimpleButton()
        Me.pnlEtiqueta = New DevExpress.XtraEditors.PanelControl()
        Me.tbDetalleEtiqueta = New DevExpress.XtraTab.XtraTabPage()
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.gpForm = New DevExpress.XtraEditors.GroupControl()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.cmbTipoEtiqueta = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtNombreE = New DevExpress.XtraEditors.TextEdit()
        Me.txtAnchoW = New DevExpress.XtraEditors.TextEdit()
        Me.txtCampo = New DevExpress.XtraEditors.TextEdit()
        Me.txtAltoH = New DevExpress.XtraEditors.TextEdit()
        Me.txtCoorX = New DevExpress.XtraEditors.TextEdit()
        Me.txtCoorY = New DevExpress.XtraEditors.TextEdit()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.itemNombre = New DevExpress.XtraLayout.LayoutControlItem()
        Me.itemAncho = New DevExpress.XtraLayout.LayoutControlItem()
        Me.itemCampo = New DevExpress.XtraLayout.LayoutControlItem()
        Me.itemAlto = New DevExpress.XtraLayout.LayoutControlItem()
        Me.itemCoorX = New DevExpress.XtraLayout.LayoutControlItem()
        Me.itemCoorY = New DevExpress.XtraLayout.LayoutControlItem()
        Me.itemTipoEtiqueta = New DevExpress.XtraLayout.LayoutControlItem()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.gpLista = New DevExpress.XtraEditors.GroupControl()
        Me.gcLista = New DevExpress.XtraGrid.GridControl()
        Me.grdDetalleEtiqueta = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ApplicationMenu1 = New DevExpress.XtraBars.Ribbon.ApplicationMenu(Me.components)
        Me.ApplicationMenu2 = New DevExpress.XtraBars.Ribbon.ApplicationMenu(Me.components)
        lblActivo = New System.Windows.Forms.Label()
        lblMargenInf = New System.Windows.Forms.Label()
        lblMargenSup = New System.Windows.Forms.Label()
        lblMargenDer = New System.Windows.Forms.Label()
        lblMargenIzq = New System.Windows.Forms.Label()
        lblAncho = New System.Windows.Forms.Label()
        lblAlto = New System.Windows.Forms.Label()
        lblCod = New System.Windows.Forms.Label()
        lblNombre = New System.Windows.Forms.Label()
        Fec_agrLabel = New System.Windows.Forms.Label()
        User_agrLabel = New System.Windows.Forms.Label()
        Fec_modLabel = New System.Windows.Forms.Label()
        User_modLabel = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        Label5 = New System.Windows.Forms.Label()
        Label10 = New System.Windows.Forms.Label()
        lblInkjet = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMargenInf, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMargenSup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMargenDer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMargenIzq, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAncho, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAlto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombre.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dkTipoEtiqueta, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerBottom.SuspendLayout()
        Me.DockPanel1.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkInkjet.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbClasificacionEtiqueta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbDPI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.pbZPLImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabControl1.SuspendLayout()
        Me.tabZPL.SuspendLayout()
        Me.tabTexto.SuspendLayout()
        CType(Me.pnlEtiqueta, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbDetalleEtiqueta.SuspendLayout()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel1.SuspendLayout()
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel2.SuspendLayout()
        Me.SplitContainerControl1.SuspendLayout()
        CType(Me.gpForm, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gpForm.SuspendLayout()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.cmbTipoEtiqueta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAnchoW.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCampo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAltoH.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCoorX.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCoorY.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.itemNombre, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.itemAncho, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.itemCampo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.itemAlto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.itemCoorX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.itemCoorY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.itemTipoEtiqueta, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.gpLista, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gpLista.SuspendLayout()
        CType(Me.gcLista, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdDetalleEtiqueta, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ApplicationMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ApplicationMenu2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblActivo
        '
        lblActivo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblActivo.AutoSize = True
        lblActivo.Location = New System.Drawing.Point(14, 468)
        lblActivo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblActivo.Name = "lblActivo"
        lblActivo.Size = New System.Drawing.Size(46, 16)
        lblActivo.TabIndex = 26
        lblActivo.Text = "Activo:"
        '
        'lblMargenInf
        '
        lblMargenInf.AutoSize = True
        lblMargenInf.Location = New System.Drawing.Point(14, 359)
        lblMargenInf.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblMargenInf.Name = "lblMargenInf"
        lblMargenInf.Size = New System.Drawing.Size(101, 16)
        lblMargenInf.TabIndex = 25
        lblMargenInf.Text = "Margen Inferior:"
        '
        'lblMargenSup
        '
        lblMargenSup.AutoSize = True
        lblMargenSup.Location = New System.Drawing.Point(14, 318)
        lblMargenSup.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblMargenSup.Name = "lblMargenSup"
        lblMargenSup.Size = New System.Drawing.Size(108, 16)
        lblMargenSup.TabIndex = 18
        lblMargenSup.Text = "Margen Superior:"
        '
        'lblMargenDer
        '
        lblMargenDer.AutoSize = True
        lblMargenDer.Location = New System.Drawing.Point(14, 276)
        lblMargenDer.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblMargenDer.Name = "lblMargenDer"
        lblMargenDer.Size = New System.Drawing.Size(106, 16)
        lblMargenDer.TabIndex = 15
        lblMargenDer.Text = "Margen Derecho:"
        '
        'lblMargenIzq
        '
        lblMargenIzq.AutoSize = True
        lblMargenIzq.Location = New System.Drawing.Point(14, 233)
        lblMargenIzq.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblMargenIzq.Name = "lblMargenIzq"
        lblMargenIzq.Size = New System.Drawing.Size(112, 16)
        lblMargenIzq.TabIndex = 12
        lblMargenIzq.Text = "Margen Izquierdo:"
        '
        'lblAncho
        '
        lblAncho.AutoSize = True
        lblAncho.Location = New System.Drawing.Point(14, 190)
        lblAncho.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblAncho.Name = "lblAncho"
        lblAncho.Size = New System.Drawing.Size(47, 16)
        lblAncho.TabIndex = 9
        lblAncho.Text = "Ancho:"
        '
        'lblAlto
        '
        lblAlto.AutoSize = True
        lblAlto.Location = New System.Drawing.Point(14, 149)
        lblAlto.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblAlto.Name = "lblAlto"
        lblAlto.Size = New System.Drawing.Size(34, 16)
        lblAlto.TabIndex = 5
        lblAlto.Text = "Alto:"
        '
        'lblCod
        '
        lblCod.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblCod.AutoSize = True
        lblCod.Location = New System.Drawing.Point(14, 27)
        lblCod.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblCod.Name = "lblCod"
        lblCod.Size = New System.Drawing.Size(46, 16)
        lblCod.TabIndex = 0
        lblCod.Text = "Código"
        '
        'lblNombre
        '
        lblNombre.AutoSize = True
        lblNombre.Location = New System.Drawing.Point(14, 110)
        lblNombre.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblNombre.Name = "lblNombre"
        lblNombre.Size = New System.Drawing.Size(57, 16)
        lblNombre.TabIndex = 3
        lblNombre.Text = "Nombre:"
        '
        'Fec_agrLabel
        '
        Fec_agrLabel.AutoSize = True
        Fec_agrLabel.Location = New System.Drawing.Point(203, 50)
        Fec_agrLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Fec_agrLabel.Name = "Fec_agrLabel"
        Fec_agrLabel.Size = New System.Drawing.Size(91, 16)
        Fec_agrLabel.TabIndex = 4
        Fec_agrLabel.Text = "Fecha Agregó:"
        '
        'User_agrLabel
        '
        User_agrLabel.AutoSize = True
        User_agrLabel.Location = New System.Drawing.Point(203, 18)
        User_agrLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        User_agrLabel.Name = "User_agrLabel"
        User_agrLabel.Size = New System.Drawing.Size(100, 16)
        User_agrLabel.TabIndex = 0
        User_agrLabel.Text = "Usuario Agregó:"
        '
        'Fec_modLabel
        '
        Fec_modLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Fec_modLabel.AutoSize = True
        Fec_modLabel.Location = New System.Drawing.Point(687, 50)
        Fec_modLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Fec_modLabel.Name = "Fec_modLabel"
        Fec_modLabel.Size = New System.Drawing.Size(97, 16)
        Fec_modLabel.TabIndex = 6
        Fec_modLabel.Text = "Fecha Modificó:"
        '
        'User_modLabel
        '
        User_modLabel.AutoSize = True
        User_modLabel.Location = New System.Drawing.Point(687, 18)
        User_modLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        User_modLabel.Name = "User_modLabel"
        User_modLabel.Size = New System.Drawing.Size(106, 16)
        User_modLabel.TabIndex = 3
        User_modLabel.Text = "Usuario Modificó:"
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(14, 402)
        Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(158, 16)
        Label4.TabIndex = 30
        Label4.Text = "Densidad Impresion (DPI):"
        '
        'Label5
        '
        Label5.AutoSize = True
        Label5.Location = New System.Drawing.Point(14, 66)
        Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(132, 16)
        Label5.TabIndex = 32
        Label5.Text = "Clasificación Etiqueta:"
        '
        'Label10
        '
        Label10.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label10.AutoSize = True
        Label10.Location = New System.Drawing.Point(13, 606)
        Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label10.Name = "Label10"
        Label10.Size = New System.Drawing.Size(791, 16)
        Label10.TabIndex = 62
        Label10.Text = "*La densidad de impresión puede afectar la dimensión de la etiqueta, genere vista" &
    " antes de guardar cambios, para confirmar el tamaño."
        '
        'lblInkjet
        '
        lblInkjet.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblInkjet.AutoSize = True
        lblInkjet.Location = New System.Drawing.Point(14, 434)
        lblInkjet.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblInkjet.Name = "lblInkjet"
        lblInkjet.Size = New System.Drawing.Size(61, 16)
        lblInkjet.TabIndex = 33
        lblInkjet.Text = "Es Inkjet:"
        '
        'RibbonControl
        '
        Me.RibbonControl.EmptyAreaImageOptions.ImagePadding = New System.Windows.Forms.Padding(37)
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuGuardar, Me.mnuActualizar, Me.mnuEliminar})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.RibbonControl.MaxItemId = 5
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.OptionsMenuMinWidth = 412
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1522, 193)
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
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Tipo Etiqueta"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEliminar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'Label9
        '
        Me.Label9.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(335, 314)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(24, 16)
        Me.Label9.TabIndex = 19
        Me.Label9.Text = "plg"
        '
        'Label8
        '
        Me.Label8.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(335, 273)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(24, 16)
        Me.Label8.TabIndex = 17
        Me.Label8.Text = "plg"
        '
        'Label7
        '
        Me.Label7.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(335, 230)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(24, 16)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "plg"
        '
        'Label6
        '
        Me.Label6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(335, 196)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(24, 16)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "plg"
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(335, 149)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(24, 16)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "plg"
        '
        'chkActivo
        '
        Me.chkActivo.EditValue = True
        Me.chkActivo.Location = New System.Drawing.Point(178, 464)
        Me.chkActivo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkActivo.MenuManager = Me.RibbonControl
        Me.chkActivo.Name = "chkActivo"
        Me.chkActivo.Properties.Caption = ""
        Me.chkActivo.Size = New System.Drawing.Size(155, 24)
        Me.chkActivo.TabIndex = 27
        '
        'txtMargenInf
        '
        Me.txtMargenInf.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMargenInf.DecimalPlaces = 4
        Me.txtMargenInf.Location = New System.Drawing.Point(178, 356)
        Me.txtMargenInf.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtMargenInf.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtMargenInf.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtMargenInf.Name = "txtMargenInf"
        Me.txtMargenInf.Size = New System.Drawing.Size(139, 23)
        Me.txtMargenInf.TabIndex = 23
        '
        'txtMargenSup
        '
        Me.txtMargenSup.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMargenSup.DecimalPlaces = 4
        Me.txtMargenSup.Location = New System.Drawing.Point(178, 313)
        Me.txtMargenSup.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtMargenSup.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtMargenSup.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtMargenSup.Name = "txtMargenSup"
        Me.txtMargenSup.Size = New System.Drawing.Size(139, 23)
        Me.txtMargenSup.TabIndex = 22
        '
        'txtMargenDer
        '
        Me.txtMargenDer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMargenDer.DecimalPlaces = 4
        Me.txtMargenDer.Location = New System.Drawing.Point(178, 272)
        Me.txtMargenDer.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtMargenDer.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtMargenDer.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtMargenDer.Name = "txtMargenDer"
        Me.txtMargenDer.Size = New System.Drawing.Size(139, 23)
        Me.txtMargenDer.TabIndex = 16
        '
        'txtMargenIzq
        '
        Me.txtMargenIzq.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMargenIzq.DecimalPlaces = 4
        Me.txtMargenIzq.Location = New System.Drawing.Point(178, 231)
        Me.txtMargenIzq.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtMargenIzq.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtMargenIzq.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtMargenIzq.Name = "txtMargenIzq"
        Me.txtMargenIzq.Size = New System.Drawing.Size(139, 23)
        Me.txtMargenIzq.TabIndex = 13
        '
        'txtAncho
        '
        Me.txtAncho.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAncho.DecimalPlaces = 2
        Me.txtAncho.Location = New System.Drawing.Point(178, 186)
        Me.txtAncho.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtAncho.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtAncho.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtAncho.Name = "txtAncho"
        Me.txtAncho.Size = New System.Drawing.Size(139, 23)
        Me.txtAncho.TabIndex = 10
        '
        'txtAlto
        '
        Me.txtAlto.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAlto.DecimalPlaces = 2
        Me.txtAlto.Location = New System.Drawing.Point(178, 145)
        Me.txtAlto.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtAlto.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtAlto.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtAlto.Name = "txtAlto"
        Me.txtAlto.Size = New System.Drawing.Size(139, 23)
        Me.txtAlto.TabIndex = 6
        '
        'lblCodigo
        '
        Me.lblCodigo.AutoSize = True
        Me.lblCodigo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCodigo.Location = New System.Drawing.Point(175, 26)
        Me.lblCodigo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCodigo.Name = "lblCodigo"
        Me.lblCodigo.Size = New System.Drawing.Size(14, 17)
        Me.lblCodigo.TabIndex = 1
        Me.lblCodigo.Text = "-"
        '
        'txtNombre
        '
        Me.txtNombre.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNombre.Location = New System.Drawing.Point(178, 105)
        Me.txtNombre.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtNombre.MenuManager = Me.RibbonControl
        Me.txtNombre.Name = "txtNombre"
        Me.txtNombre.Size = New System.Drawing.Size(139, 22)
        Me.txtNombre.TabIndex = 4
        '
        'Fec_agrDateEdit
        '
        Me.Fec_agrDateEdit.EditValue = Nothing
        Me.Fec_agrDateEdit.Enabled = False
        Me.Fec_agrDateEdit.Location = New System.Drawing.Point(306, 47)
        Me.Fec_agrDateEdit.Margin = New System.Windows.Forms.Padding(4)
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
        Me.User_agrTextEdit.Location = New System.Drawing.Point(306, 15)
        Me.User_agrTextEdit.Margin = New System.Windows.Forms.Padding(4)
        Me.User_agrTextEdit.MenuManager = Me.RibbonControl
        Me.User_agrTextEdit.Name = "User_agrTextEdit"
        Me.User_agrTextEdit.Size = New System.Drawing.Size(261, 22)
        Me.User_agrTextEdit.TabIndex = 1
        '
        'Fec_modDateEdit
        '
        Me.Fec_modDateEdit.EditValue = Nothing
        Me.Fec_modDateEdit.Enabled = False
        Me.Fec_modDateEdit.Location = New System.Drawing.Point(790, 47)
        Me.Fec_modDateEdit.Margin = New System.Windows.Forms.Padding(4)
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
        Me.User_modTextEdit.Location = New System.Drawing.Point(790, 15)
        Me.User_modTextEdit.Margin = New System.Windows.Forms.Padding(4)
        Me.User_modTextEdit.MenuManager = Me.RibbonControl
        Me.User_modTextEdit.Name = "User_modTextEdit"
        Me.User_modTextEdit.Size = New System.Drawing.Size(261, 22)
        Me.User_modTextEdit.TabIndex = 2
        '
        'dkTipoEtiqueta
        '
        Me.dkTipoEtiqueta.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerBottom})
        Me.dkTipoEtiqueta.Form = Me
        Me.dkTipoEtiqueta.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane"})
        '
        'hideContainerBottom
        '
        Me.hideContainerBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.hideContainerBottom.Controls.Add(Me.DockPanel1)
        Me.hideContainerBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 863)
        Me.hideContainerBottom.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(1522, 26)
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.ID = New System.Guid("97f5b8e5-aac9-4de7-951e-c616f90a91c5")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 0)
        Me.DockPanel1.Margin = New System.Windows.Forms.Padding(4)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(200, 126)
        Me.DockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.SavedIndex = 0
        Me.DockPanel1.Size = New System.Drawing.Size(1662, 126)
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
        Me.DockPanel1_Container.Location = New System.Drawing.Point(4, 34)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1654, 89)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkInkjet)
        Me.Panel1.Controls.Add(lblInkjet)
        Me.Panel1.Controls.Add(Label5)
        Me.Panel1.Controls.Add(Me.cmbClasificacionEtiqueta)
        Me.Panel1.Controls.Add(Label4)
        Me.Panel1.Controls.Add(Me.cmbDPI)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(lblCod)
        Me.Panel1.Controls.Add(lblNombre)
        Me.Panel1.Controls.Add(lblAlto)
        Me.Panel1.Controls.Add(Me.chkActivo)
        Me.Panel1.Controls.Add(lblActivo)
        Me.Panel1.Controls.Add(Me.txtMargenInf)
        Me.Panel1.Controls.Add(lblAncho)
        Me.Panel1.Controls.Add(Me.txtMargenSup)
        Me.Panel1.Controls.Add(lblMargenIzq)
        Me.Panel1.Controls.Add(Me.txtMargenDer)
        Me.Panel1.Controls.Add(lblMargenInf)
        Me.Panel1.Controls.Add(Me.txtMargenIzq)
        Me.Panel1.Controls.Add(lblMargenDer)
        Me.Panel1.Controls.Add(Me.txtAncho)
        Me.Panel1.Controls.Add(lblMargenSup)
        Me.Panel1.Controls.Add(Me.txtAlto)
        Me.Panel1.Controls.Add(Me.lblCodigo)
        Me.Panel1.Controls.Add(Me.txtNombre)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(4, 18)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(369, 536)
        Me.Panel1.TabIndex = 57
        '
        'chkInkjet
        '
        Me.chkInkjet.Location = New System.Drawing.Point(178, 430)
        Me.chkInkjet.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkInkjet.MenuManager = Me.RibbonControl
        Me.chkInkjet.Name = "chkInkjet"
        Me.chkInkjet.Properties.Caption = ""
        Me.chkInkjet.Size = New System.Drawing.Size(155, 24)
        Me.chkInkjet.TabIndex = 34
        '
        'cmbClasificacionEtiqueta
        '
        Me.cmbClasificacionEtiqueta.Location = New System.Drawing.Point(178, 64)
        Me.cmbClasificacionEtiqueta.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.cmbClasificacionEtiqueta.Name = "cmbClasificacionEtiqueta"
        Me.cmbClasificacionEtiqueta.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbClasificacionEtiqueta.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbClasificacionEtiqueta.Size = New System.Drawing.Size(141, 22)
        Me.cmbClasificacionEtiqueta.TabIndex = 31
        '
        'cmbDPI
        '
        Me.cmbDPI.Location = New System.Drawing.Point(178, 400)
        Me.cmbDPI.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.cmbDPI.Name = "cmbDPI"
        Me.cmbDPI.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbDPI.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbDPI.Size = New System.Drawing.Size(141, 22)
        Me.cmbDPI.TabIndex = 29
        '
        'Label2
        '
        Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(335, 359)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(24, 16)
        Me.Label2.TabIndex = 28
        Me.Label2.Text = "plg"
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Label10)
        Me.GroupControl2.Controls.Add(Me.GroupBox4)
        Me.GroupControl2.Controls.Add(Me.GroupBox2)
        Me.GroupControl2.Controls.Add(Me.GroupBox3)
        Me.GroupControl2.Controls.Add(Me.GroupBox1)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl2.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(1520, 640)
        Me.GroupControl2.TabIndex = 60
        Me.GroupControl2.Text = "Diseño de la etiqueta"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Panel2)
        Me.GroupBox4.Location = New System.Drawing.Point(1090, 37)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.GroupBox4.Size = New System.Drawing.Size(385, 457)
        Me.GroupBox4.TabIndex = 63
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Dimensión fisica"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.lblpAlto)
        Me.Panel2.Controls.Add(Me.Label11)
        Me.Panel2.Controls.Add(Me.Label13)
        Me.Panel2.Controls.Add(Me.Label15)
        Me.Panel2.Controls.Add(Me.lblpAncho)
        Me.Panel2.Controls.Add(Me.PictureBox2)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(4, 18)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(377, 437)
        Me.Panel2.TabIndex = 58
        '
        'Label3
        '
        Me.Label3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Red
        Me.Label3.Location = New System.Drawing.Point(296, 196)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 34)
        Me.Label3.TabIndex = 58
        Me.Label3.Text = "Margen " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Derecho"
        '
        'lblpAlto
        '
        Me.lblpAlto.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblpAlto.Appearance.Options.UseTextOptions = True
        Me.lblpAlto.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblpAlto.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblpAlto.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Horizontal
        Me.lblpAlto.LineVisible = True
        Me.lblpAlto.Location = New System.Drawing.Point(14, 242)
        Me.lblpAlto.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.lblpAlto.Name = "lblpAlto"
        Me.lblpAlto.Size = New System.Drawing.Size(93, 106)
        Me.lblpAlto.TabIndex = 57
        Me.lblpAlto.Text = "Alto: 0 Pulg"
        '
        'Label11
        '
        Me.Label11.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Red
        Me.Label11.Location = New System.Drawing.Point(24, 194)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(75, 34)
        Me.Label11.TabIndex = 20
        Me.Label11.Text = "Margen " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Izquierdo"
        '
        'Label13
        '
        Me.Label13.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Red
        Me.Label13.Location = New System.Drawing.Point(169, 26)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(67, 34)
        Me.Label13.TabIndex = 2
        Me.Label13.Text = "Margen " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Superior"
        '
        'Label15
        '
        Me.Label15.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Red
        Me.Label15.Location = New System.Drawing.Point(172, 359)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(64, 34)
        Me.Label15.TabIndex = 29
        Me.Label15.Text = "Margen " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Inferior"
        '
        'lblpAncho
        '
        Me.lblpAncho.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblpAncho.Appearance.Options.UseTextOptions = True
        Me.lblpAncho.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblpAncho.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblpAncho.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Horizontal
        Me.lblpAncho.LineVisible = True
        Me.lblpAncho.Location = New System.Drawing.Point(141, 82)
        Me.lblpAncho.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.lblpAncho.Name = "lblpAncho"
        Me.lblpAncho.Size = New System.Drawing.Size(118, 86)
        Me.lblpAncho.TabIndex = 8
        Me.lblpAncho.Text = "Ancho: 0 Pulg"
        '
        'PictureBox2
        '
        Me.PictureBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(130, 186)
        Me.PictureBox2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(141, 142)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox2.TabIndex = 56
        Me.PictureBox2.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.pbZPLImage)
        Me.GroupBox2.Location = New System.Drawing.Point(774, 33)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.GroupBox2.Size = New System.Drawing.Size(310, 462)
        Me.GroupBox2.TabIndex = 62
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Vista Previa Etiqueta"
        '
        'pbZPLImage
        '
        Me.pbZPLImage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pbZPLImage.Location = New System.Drawing.Point(4, 18)
        Me.pbZPLImage.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.pbZPLImage.Name = "pbZPLImage"
        Me.pbZPLImage.Size = New System.Drawing.Size(302, 442)
        Me.pbZPLImage.TabIndex = 60
        Me.pbZPLImage.TabStop = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnGenerarImagen)
        Me.GroupBox3.Controls.Add(Me.txtZPL)
        Me.GroupBox3.Location = New System.Drawing.Point(398, 33)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.GroupBox3.Size = New System.Drawing.Size(370, 553)
        Me.GroupBox3.TabIndex = 62
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Códificación ZPL"
        '
        'btnGenerarImagen
        '
        Me.btnGenerarImagen.Location = New System.Drawing.Point(16, 486)
        Me.btnGenerarImagen.Margin = New System.Windows.Forms.Padding(4)
        Me.btnGenerarImagen.Name = "btnGenerarImagen"
        Me.btnGenerarImagen.Size = New System.Drawing.Size(326, 48)
        Me.btnGenerarImagen.TabIndex = 61
        Me.btnGenerarImagen.Text = "Generar vista previa"
        '
        'txtZPL
        '
        Me.txtZPL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtZPL.Location = New System.Drawing.Point(16, 25)
        Me.txtZPL.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtZPL.Multiline = True
        Me.txtZPL.Name = "txtZPL"
        Me.txtZPL.Size = New System.Drawing.Size(334, 429)
        Me.txtZPL.TabIndex = 59
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Panel1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 33)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.GroupBox1.Size = New System.Drawing.Size(377, 556)
        Me.GroupBox1.TabIndex = 59
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Parametros"
        '
        'XtraTabControl1
        '
        Me.XtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XtraTabControl1.Location = New System.Drawing.Point(0, 193)
        Me.XtraTabControl1.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.XtraTabControl1.Name = "XtraTabControl1"
        Me.XtraTabControl1.SelectedTabPage = Me.tabZPL
        Me.XtraTabControl1.Size = New System.Drawing.Size(1522, 670)
        Me.XtraTabControl1.TabIndex = 63
        Me.XtraTabControl1.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tabZPL, Me.tabTexto, Me.tbDetalleEtiqueta})
        '
        'tabZPL
        '
        Me.tabZPL.Controls.Add(Me.GroupControl2)
        Me.tabZPL.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.tabZPL.Name = "tabZPL"
        Me.tabZPL.Size = New System.Drawing.Size(1520, 640)
        Me.tabZPL.Text = "Impresión por ZPL"
        '
        'tabTexto
        '
        Me.tabTexto.Controls.Add(Me.btnAgregarTexto)
        Me.tabTexto.Controls.Add(Me.btnImprimir)
        Me.tabTexto.Controls.Add(Me.pnlEtiqueta)
        Me.tabTexto.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.tabTexto.Name = "tabTexto"
        Me.tabTexto.Size = New System.Drawing.Size(1520, 640)
        Me.tabTexto.Text = "Impresión por Texto"
        '
        'btnAgregarTexto
        '
        Me.btnAgregarTexto.Location = New System.Drawing.Point(50, 32)
        Me.btnAgregarTexto.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.btnAgregarTexto.Name = "btnAgregarTexto"
        Me.btnAgregarTexto.Size = New System.Drawing.Size(94, 30)
        Me.btnAgregarTexto.TabIndex = 1
        Me.btnAgregarTexto.Text = "SimpleButton1"
        '
        'btnImprimir
        '
        Me.btnImprimir.Location = New System.Drawing.Point(211, 32)
        Me.btnImprimir.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(94, 30)
        Me.btnImprimir.TabIndex = 2
        Me.btnImprimir.Text = "SimpleButton1"
        '
        'pnlEtiqueta
        '
        Me.pnlEtiqueta.AllowDrop = True
        Me.pnlEtiqueta.Location = New System.Drawing.Point(10, 100)
        Me.pnlEtiqueta.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.pnlEtiqueta.Name = "pnlEtiqueta"
        Me.pnlEtiqueta.Size = New System.Drawing.Size(810, 306)
        Me.pnlEtiqueta.TabIndex = 0
        '
        'tbDetalleEtiqueta
        '
        Me.tbDetalleEtiqueta.Controls.Add(Me.SplitContainerControl1)
        Me.tbDetalleEtiqueta.Margin = New System.Windows.Forms.Padding(4)
        Me.tbDetalleEtiqueta.Name = "tbDetalleEtiqueta"
        Me.tbDetalleEtiqueta.Size = New System.Drawing.Size(1520, 640)
        Me.tbDetalleEtiqueta.Text = "Detalle Etiqueta"
        '
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl1.Horizontal = False
        Me.SplitContainerControl1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        '
        'SplitContainerControl1.Panel1
        '
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.gpForm)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.ToolStrip1)
        Me.SplitContainerControl1.Panel1.Text = "Panel1"
        '
        'SplitContainerControl1.Panel2
        '
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.gpLista)
        Me.SplitContainerControl1.Panel2.Text = "Panel2"
        Me.SplitContainerControl1.Size = New System.Drawing.Size(1520, 640)
        Me.SplitContainerControl1.SplitterPosition = 338
        Me.SplitContainerControl1.TabIndex = 1
        '
        'gpForm
        '
        Me.gpForm.Controls.Add(Me.LayoutControl1)
        Me.gpForm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gpForm.Location = New System.Drawing.Point(0, 27)
        Me.gpForm.Margin = New System.Windows.Forms.Padding(4)
        Me.gpForm.Name = "gpForm"
        Me.gpForm.Size = New System.Drawing.Size(1520, 311)
        Me.gpForm.TabIndex = 1
        Me.gpForm.Text = "Datos Generales"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.cmbTipoEtiqueta)
        Me.LayoutControl1.Controls.Add(Me.txtNombreE)
        Me.LayoutControl1.Controls.Add(Me.txtAnchoW)
        Me.LayoutControl1.Controls.Add(Me.txtCampo)
        Me.LayoutControl1.Controls.Add(Me.txtAltoH)
        Me.LayoutControl1.Controls.Add(Me.txtCoorX)
        Me.LayoutControl1.Controls.Add(Me.txtCoorY)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.LayoutControl1.Location = New System.Drawing.Point(2, 28)
        Me.LayoutControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.Root
        Me.LayoutControl1.Size = New System.Drawing.Size(1044, 281)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'cmbTipoEtiqueta
        '
        Me.cmbTipoEtiqueta.Location = New System.Drawing.Point(114, 152)
        Me.cmbTipoEtiqueta.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbTipoEtiqueta.MenuManager = Me.RibbonControl
        Me.cmbTipoEtiqueta.Name = "cmbTipoEtiqueta"
        Me.cmbTipoEtiqueta.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTipoEtiqueta.Size = New System.Drawing.Size(433, 22)
        Me.cmbTipoEtiqueta.StyleController = Me.LayoutControl1
        Me.cmbTipoEtiqueta.TabIndex = 10
        '
        'txtNombreE
        '
        Me.txtNombreE.Location = New System.Drawing.Point(114, 16)
        Me.txtNombreE.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNombreE.MenuManager = Me.RibbonControl
        Me.txtNombreE.Name = "txtNombreE"
        Me.txtNombreE.Size = New System.Drawing.Size(433, 22)
        Me.txtNombreE.StyleController = Me.LayoutControl1
        Me.txtNombreE.TabIndex = 4
        '
        'txtAnchoW
        '
        Me.txtAnchoW.Location = New System.Drawing.Point(657, 16)
        Me.txtAnchoW.Margin = New System.Windows.Forms.Padding(4)
        Me.txtAnchoW.MenuManager = Me.RibbonControl
        Me.txtAnchoW.Name = "txtAnchoW"
        Me.txtAnchoW.Size = New System.Drawing.Size(371, 22)
        Me.txtAnchoW.StyleController = Me.LayoutControl1
        Me.txtAnchoW.TabIndex = 5
        '
        'txtCampo
        '
        Me.txtCampo.Location = New System.Drawing.Point(114, 50)
        Me.txtCampo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCampo.MenuManager = Me.RibbonControl
        Me.txtCampo.Name = "txtCampo"
        Me.txtCampo.Size = New System.Drawing.Size(433, 22)
        Me.txtCampo.StyleController = Me.LayoutControl1
        Me.txtCampo.TabIndex = 6
        '
        'txtAltoH
        '
        Me.txtAltoH.Location = New System.Drawing.Point(657, 50)
        Me.txtAltoH.Margin = New System.Windows.Forms.Padding(4)
        Me.txtAltoH.MenuManager = Me.RibbonControl
        Me.txtAltoH.Name = "txtAltoH"
        Me.txtAltoH.Size = New System.Drawing.Size(371, 22)
        Me.txtAltoH.StyleController = Me.LayoutControl1
        Me.txtAltoH.TabIndex = 7
        '
        'txtCoorX
        '
        Me.txtCoorX.Location = New System.Drawing.Point(114, 84)
        Me.txtCoorX.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCoorX.MenuManager = Me.RibbonControl
        Me.txtCoorX.Name = "txtCoorX"
        Me.txtCoorX.Size = New System.Drawing.Size(433, 22)
        Me.txtCoorX.StyleController = Me.LayoutControl1
        Me.txtCoorX.TabIndex = 8
        '
        'txtCoorY
        '
        Me.txtCoorY.Location = New System.Drawing.Point(114, 118)
        Me.txtCoorY.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCoorY.MenuManager = Me.RibbonControl
        Me.txtCoorY.Name = "txtCoorY"
        Me.txtCoorY.Size = New System.Drawing.Size(433, 22)
        Me.txtCoorY.StyleController = Me.LayoutControl1
        Me.txtCoorY.TabIndex = 9
        '
        'Root
        '
        Me.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.Root.GroupBordersVisible = False
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.itemNombre, Me.itemAncho, Me.itemCampo, Me.itemAlto, Me.itemCoorX, Me.itemCoorY, Me.itemTipoEtiqueta})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(1044, 281)
        Me.Root.TextVisible = False
        '
        'itemNombre
        '
        Me.itemNombre.Control = Me.txtNombreE
        Me.itemNombre.Location = New System.Drawing.Point(0, 0)
        Me.itemNombre.Name = "itemNombre"
        Me.itemNombre.Padding = New DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6)
        Me.itemNombre.Size = New System.Drawing.Size(543, 34)
        Me.itemNombre.Text = "Nombre:"
        Me.itemNombre.TextSize = New System.Drawing.Size(86, 16)
        '
        'itemAncho
        '
        Me.itemAncho.Control = Me.txtAnchoW
        Me.itemAncho.Location = New System.Drawing.Point(543, 0)
        Me.itemAncho.Name = "itemAncho"
        Me.itemAncho.Padding = New DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6)
        Me.itemAncho.Size = New System.Drawing.Size(481, 34)
        Me.itemAncho.Text = "Ancho:"
        Me.itemAncho.TextSize = New System.Drawing.Size(86, 16)
        '
        'itemCampo
        '
        Me.itemCampo.Control = Me.txtCampo
        Me.itemCampo.Location = New System.Drawing.Point(0, 34)
        Me.itemCampo.Name = "itemCampo"
        Me.itemCampo.Padding = New DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6)
        Me.itemCampo.Size = New System.Drawing.Size(543, 34)
        Me.itemCampo.Text = "Campo:"
        Me.itemCampo.TextSize = New System.Drawing.Size(86, 16)
        '
        'itemAlto
        '
        Me.itemAlto.Control = Me.txtAltoH
        Me.itemAlto.Location = New System.Drawing.Point(543, 34)
        Me.itemAlto.Name = "itemAlto"
        Me.itemAlto.Padding = New DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6)
        Me.itemAlto.Size = New System.Drawing.Size(481, 227)
        Me.itemAlto.Text = "Alto:"
        Me.itemAlto.TextSize = New System.Drawing.Size(86, 16)
        '
        'itemCoorX
        '
        Me.itemCoorX.Control = Me.txtCoorX
        Me.itemCoorX.Location = New System.Drawing.Point(0, 68)
        Me.itemCoorX.Name = "itemCoorX"
        Me.itemCoorX.Padding = New DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6)
        Me.itemCoorX.Size = New System.Drawing.Size(543, 34)
        Me.itemCoorX.Text = "Coordenada X:"
        Me.itemCoorX.TextSize = New System.Drawing.Size(86, 16)
        '
        'itemCoorY
        '
        Me.itemCoorY.Control = Me.txtCoorY
        Me.itemCoorY.Location = New System.Drawing.Point(0, 102)
        Me.itemCoorY.Name = "itemCoorY"
        Me.itemCoorY.Padding = New DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6)
        Me.itemCoorY.Size = New System.Drawing.Size(543, 34)
        Me.itemCoorY.Text = "Coordenada Y:"
        Me.itemCoorY.TextSize = New System.Drawing.Size(86, 16)
        '
        'itemTipoEtiqueta
        '
        Me.itemTipoEtiqueta.Control = Me.cmbTipoEtiqueta
        Me.itemTipoEtiqueta.Location = New System.Drawing.Point(0, 136)
        Me.itemTipoEtiqueta.Name = "itemTipoEtiqueta"
        Me.itemTipoEtiqueta.Padding = New DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6)
        Me.itemTipoEtiqueta.Size = New System.Drawing.Size(543, 125)
        Me.itemTipoEtiqueta.Text = "Tipo Etiqueta:"
        Me.itemTipoEtiqueta.TextSize = New System.Drawing.Size(86, 16)
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.ToolStripButton2})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1520, 27)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Image = Global.TOMWMS.My.Resources.Resources.add
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(76, 24)
        Me.ToolStripButton1.Text = "Nuevo"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.Image = Global.TOMWMS.My.Resources.Resources.green_check
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(86, 24)
        Me.ToolStripButton2.Text = "Guardar"
        '
        'gpLista
        '
        Me.gpLista.Controls.Add(Me.gcLista)
        Me.gpLista.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gpLista.Location = New System.Drawing.Point(0, 0)
        Me.gpLista.Margin = New System.Windows.Forms.Padding(4)
        Me.gpLista.Name = "gpLista"
        Me.gpLista.Size = New System.Drawing.Size(1520, 290)
        Me.gpLista.TabIndex = 0
        Me.gpLista.Text = "Lista"
        '
        'gcLista
        '
        Me.gcLista.Cursor = System.Windows.Forms.Cursors.Default
        Me.gcLista.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcLista.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode1.RelationName = "Level1"
        Me.gcLista.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.gcLista.Location = New System.Drawing.Point(2, 28)
        Me.gcLista.MainView = Me.grdDetalleEtiqueta
        Me.gcLista.Margin = New System.Windows.Forms.Padding(4)
        Me.gcLista.MenuManager = Me.RibbonControl
        Me.gcLista.Name = "gcLista"
        Me.gcLista.Size = New System.Drawing.Size(1516, 260)
        Me.gcLista.TabIndex = 0
        Me.gcLista.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdDetalleEtiqueta})
        '
        'grdDetalleEtiqueta
        '
        Me.grdDetalleEtiqueta.DetailHeight = 431
        Me.grdDetalleEtiqueta.GridControl = Me.gcLista
        Me.grdDetalleEtiqueta.Name = "grdDetalleEtiqueta"
        Me.grdDetalleEtiqueta.OptionsBehavior.Editable = False
        Me.grdDetalleEtiqueta.OptionsEditForm.PopupEditFormWidth = 933
        '
        'ApplicationMenu1
        '
        Me.ApplicationMenu1.Name = "ApplicationMenu1"
        '
        'ApplicationMenu2
        '
        Me.ApplicationMenu2.Name = "ApplicationMenu2"
        '
        'frmTipo_Etiqueta
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1522, 889)
        Me.Controls.Add(Me.XtraTabControl1)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmTipo_Etiqueta"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Tipo Etiqueta"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMargenInf, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMargenSup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMargenDer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMargenIzq, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAncho, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAlto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombre.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dkTipoEtiqueta, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerBottom.ResumeLayout(False)
        Me.DockPanel1.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        Me.DockPanel1_Container.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkInkjet.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbClasificacionEtiqueta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbDPI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.GroupControl2.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.pbZPLImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabControl1.ResumeLayout(False)
        Me.tabZPL.ResumeLayout(False)
        Me.tabTexto.ResumeLayout(False)
        CType(Me.pnlEtiqueta, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbDetalleEtiqueta.ResumeLayout(False)
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel1.ResumeLayout(False)
        Me.SplitContainerControl1.Panel1.PerformLayout()
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        CType(Me.gpForm, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gpForm.ResumeLayout(False)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.cmbTipoEtiqueta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAnchoW.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCampo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAltoH.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCoorX.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCoorY.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.itemNombre, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.itemAncho, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.itemCampo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.itemAlto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.itemCoorX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.itemCoorY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.itemTipoEtiqueta, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.gpLista, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gpLista.ResumeLayout(False)
        CType(Me.gcLista, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdDetalleEtiqueta, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ApplicationMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ApplicationMenu2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents mnuGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEliminar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents chkActivo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtMargenInf As NumericUpDown
    Friend WithEvents txtMargenSup As NumericUpDown
    Friend WithEvents txtMargenDer As NumericUpDown
    Friend WithEvents txtMargenIzq As NumericUpDown
    Friend WithEvents txtAncho As NumericUpDown
    Friend WithEvents txtAlto As NumericUpDown
    Friend WithEvents lblCodigo As Label
    Friend WithEvents txtNombre As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_agrDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_agrTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_modDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_modTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents dkTipoEtiqueta As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents Panel1 As Panel
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents Panel2 As Panel
    Friend WithEvents lblpAlto As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Label11 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents lblpAncho As DevExpress.XtraEditors.LabelControl
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Label2 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtZPL As TextBox
    Friend WithEvents pbZPLImage As PictureBox
    Friend WithEvents btnGenerarImagen As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cmbClasificacionEtiqueta As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbDPI As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents XtraTabControl1 As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents tabZPL As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tabTexto As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents pnlEtiqueta As DevExpress.XtraEditors.PanelControl
    Friend WithEvents btnAgregarTexto As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnImprimir As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tbDetalleEtiqueta As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents gpForm As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents ToolStripButton1 As ToolStripButton
    Friend WithEvents ToolStripButton2 As ToolStripButton
    Friend WithEvents gpLista As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ApplicationMenu1 As DevExpress.XtraBars.Ribbon.ApplicationMenu
    Friend WithEvents ApplicationMenu2 As DevExpress.XtraBars.Ribbon.ApplicationMenu
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents txtNombreE As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtAnchoW As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCampo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtAltoH As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCoorX As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCoorY As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents itemNombre As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents itemAncho As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents itemCampo As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents itemAlto As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents itemCoorX As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents itemCoorY As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents gcLista As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdDetalleEtiqueta As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cmbTipoEtiqueta As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents itemTipoEtiqueta As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents chkInkjet As DevExpress.XtraEditors.CheckEdit
End Class
