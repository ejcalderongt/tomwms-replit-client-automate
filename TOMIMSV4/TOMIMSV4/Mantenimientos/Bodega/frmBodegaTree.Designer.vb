<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBodegaTree
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If components IsNot Nothing Then
                    components.Dispose()
                End If
                If pObjBeB IsNot Nothing Then
                    pObjBeB.Dispose()
                    pObjBeB = Nothing
                End If
                If DTArea IsNot Nothing Then
                    DTArea.Dispose()
                    DTArea = Nothing
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim User_modLabel As System.Windows.Forms.Label
        Dim User_agrLabel As System.Windows.Forms.Label
        Dim Fec_modLabel As System.Windows.Forms.Label
        Dim Fec_agrLabel As System.Windows.Forms.Label
        Dim Ubic_mermaLabel As System.Windows.Forms.Label
        Dim Ubic_recepcionLabel As System.Windows.Forms.Label
        Dim ActivoLabel As System.Windows.Forms.Label
        Dim Ubic_despachoLabel As System.Windows.Forms.Label
        Dim Ubic_pickingLabel As System.Windows.Forms.Label
        Dim EncargadoLabel As System.Windows.Forms.Label
        Dim EmailLabel As System.Windows.Forms.Label
        Dim TelefonoLabel As System.Windows.Forms.Label
        Dim DireccionLabel As System.Windows.Forms.Label
        Dim NombreLabel As System.Windows.Forms.Label
        Dim IdBodegaLabel As System.Windows.Forms.Label
        Dim IdEmpresaLabel As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim lblAlto As System.Windows.Forms.Label
        Dim lblLargo As System.Windows.Forms.Label
        Dim lblAncho As System.Windows.Forms.Label
        Dim Label7 As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBodegaTree))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.AreaBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsBodega = New DsBodega()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.User_modTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.User_agrTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_modTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_agrTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.TabUbicacion = New System.Windows.Forms.TabPage()
        Me.tLArea = New DevExpress.XtraTreeList.TreeList()
        Me.colIdArea = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.colIdBodega = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.colCodigo = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.colDescripcion = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.tabBodega = New System.Windows.Forms.TabPage()
        Me.GroupControl4 = New DevExpress.XtraEditors.GroupControl()
        Me.txtCoordenadaX = New DevExpress.XtraEditors.TextEdit()
        Me.txtCoordenadaY = New DevExpress.XtraEditors.TextEdit()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.cmbEmpresa = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbPais = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtAncho = New System.Windows.Forms.NumericUpDown()
        Me.txtLargo = New System.Windows.Forms.NumericUpDown()
        Me.txtAlto = New System.Windows.Forms.NumericUpDown()
        Me.txtNombreComercial = New DevExpress.XtraEditors.TextEdit()
        Me.txtCodigoBarra = New DevExpress.XtraEditors.TextEdit()
        Me.txtCodigo = New DevExpress.XtraEditors.TextEdit()
        Me.txtNombre = New DevExpress.XtraEditors.TextEdit()
        Me.DireccionTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.TelefonoTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.EmailTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.EncargadoTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.cmdBuscar = New System.Windows.Forms.Button()
        Me.txtUbicacionRecepcion = New DevExpress.XtraEditors.TextEdit()
        Me.txtUbicacionPicking = New DevExpress.XtraEditors.TextEdit()
        Me.chkActivo = New DevExpress.XtraEditors.CheckEdit()
        Me.txtUbicacionDespacho = New DevExpress.XtraEditors.TextEdit()
        Me.txtUbicacionMerma = New DevExpress.XtraEditors.TextEdit()
        Me.tabDatos = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.tlPrueba = New DevExpress.XtraTreeList.TreeList()
        Me.Padre = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        User_modLabel = New System.Windows.Forms.Label()
        User_agrLabel = New System.Windows.Forms.Label()
        Fec_modLabel = New System.Windows.Forms.Label()
        Fec_agrLabel = New System.Windows.Forms.Label()
        Ubic_mermaLabel = New System.Windows.Forms.Label()
        Ubic_recepcionLabel = New System.Windows.Forms.Label()
        ActivoLabel = New System.Windows.Forms.Label()
        Ubic_despachoLabel = New System.Windows.Forms.Label()
        Ubic_pickingLabel = New System.Windows.Forms.Label()
        EncargadoLabel = New System.Windows.Forms.Label()
        EmailLabel = New System.Windows.Forms.Label()
        TelefonoLabel = New System.Windows.Forms.Label()
        DireccionLabel = New System.Windows.Forms.Label()
        NombreLabel = New System.Windows.Forms.Label()
        IdBodegaLabel = New System.Windows.Forms.Label()
        IdEmpresaLabel = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        lblAlto = New System.Windows.Forms.Label()
        lblLargo = New System.Windows.Forms.Label()
        lblAncho = New System.Windows.Forms.Label()
        Label7 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AreaBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsBodega, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabUbicacion.SuspendLayout()
        CType(Me.tLArea, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabBodega.SuspendLayout()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl4.SuspendLayout()
        CType(Me.txtCoordenadaX.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCoordenadaY.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.cmbEmpresa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPais.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAncho, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLargo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAlto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreComercial.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodigoBarra.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodigo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombre.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DireccionTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TelefonoTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmailTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EncargadoTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.txtUbicacionRecepcion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUbicacionPicking.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUbicacionDespacho.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUbicacionMerma.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabDatos.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.tlPrueba, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'User_modLabel
        '
        User_modLabel.AutoSize = True
        User_modLabel.Location = New System.Drawing.Point(637, 33)
        User_modLabel.Name = "User_modLabel"
        User_modLabel.Size = New System.Drawing.Size(111, 17)
        User_modLabel.TabIndex = 2
        User_modLabel.Text = "Usuario Modificó:"
        '
        'User_agrLabel
        '
        User_agrLabel.AutoSize = True
        User_agrLabel.Location = New System.Drawing.Point(190, 33)
        User_agrLabel.Name = "User_agrLabel"
        User_agrLabel.Size = New System.Drawing.Size(106, 17)
        User_agrLabel.TabIndex = 0
        User_agrLabel.Text = "Usuario Agregó:"
        '
        'Fec_modLabel
        '
        Fec_modLabel.AutoSize = True
        Fec_modLabel.Location = New System.Drawing.Point(637, 65)
        Fec_modLabel.Name = "Fec_modLabel"
        Fec_modLabel.Size = New System.Drawing.Size(102, 17)
        Fec_modLabel.TabIndex = 4
        Fec_modLabel.Text = "Fecha Modificó:"
        '
        'Fec_agrLabel
        '
        Fec_agrLabel.AutoSize = True
        Fec_agrLabel.Location = New System.Drawing.Point(190, 70)
        Fec_agrLabel.Name = "Fec_agrLabel"
        Fec_agrLabel.Size = New System.Drawing.Size(97, 17)
        Fec_agrLabel.TabIndex = 5
        Fec_agrLabel.Text = "Fecha Agregó:"
        '
        'Ubic_mermaLabel
        '
        Ubic_mermaLabel.AutoSize = True
        Ubic_mermaLabel.Location = New System.Drawing.Point(43, 133)
        Ubic_mermaLabel.Name = "Ubic_mermaLabel"
        Ubic_mermaLabel.Size = New System.Drawing.Size(130, 17)
        Ubic_mermaLabel.TabIndex = 6
        Ubic_mermaLabel.Text = "Ubicación de Merma"
        '
        'Ubic_recepcionLabel
        '
        Ubic_recepcionLabel.AutoSize = True
        Ubic_recepcionLabel.Location = New System.Drawing.Point(41, 38)
        Ubic_recepcionLabel.Name = "Ubic_recepcionLabel"
        Ubic_recepcionLabel.Size = New System.Drawing.Size(152, 17)
        Ubic_recepcionLabel.TabIndex = 0
        Ubic_recepcionLabel.Text = "Ubicación de Recepción"
        '
        'ActivoLabel
        '
        ActivoLabel.AutoSize = True
        ActivoLabel.Location = New System.Drawing.Point(43, 166)
        ActivoLabel.Name = "ActivoLabel"
        ActivoLabel.Size = New System.Drawing.Size(51, 17)
        ActivoLabel.TabIndex = 9
        ActivoLabel.Text = "Activo:"
        '
        'Ubic_despachoLabel
        '
        Ubic_despachoLabel.AutoSize = True
        Ubic_despachoLabel.Location = New System.Drawing.Point(43, 102)
        Ubic_despachoLabel.Name = "Ubic_despachoLabel"
        Ubic_despachoLabel.Size = New System.Drawing.Size(150, 17)
        Ubic_despachoLabel.TabIndex = 4
        Ubic_despachoLabel.Text = "Ubicación de Despacho"
        '
        'Ubic_pickingLabel
        '
        Ubic_pickingLabel.AutoSize = True
        Ubic_pickingLabel.Location = New System.Drawing.Point(42, 70)
        Ubic_pickingLabel.Name = "Ubic_pickingLabel"
        Ubic_pickingLabel.Size = New System.Drawing.Size(131, 17)
        Ubic_pickingLabel.TabIndex = 2
        Ubic_pickingLabel.Text = "Ubicación de Picking"
        '
        'EncargadoLabel
        '
        EncargadoLabel.AutoSize = True
        EncargadoLabel.Location = New System.Drawing.Point(51, 326)
        EncargadoLabel.Name = "EncargadoLabel"
        EncargadoLabel.Size = New System.Drawing.Size(79, 17)
        EncargadoLabel.TabIndex = 18
        EncargadoLabel.Text = "Encargado:"
        '
        'EmailLabel
        '
        EmailLabel.AutoSize = True
        EmailLabel.Location = New System.Drawing.Point(51, 294)
        EmailLabel.Name = "EmailLabel"
        EmailLabel.Size = New System.Drawing.Size(44, 17)
        EmailLabel.TabIndex = 16
        EmailLabel.Text = "Email:"
        '
        'TelefonoLabel
        '
        TelefonoLabel.AutoSize = True
        TelefonoLabel.Location = New System.Drawing.Point(51, 262)
        TelefonoLabel.Name = "TelefonoLabel"
        TelefonoLabel.Size = New System.Drawing.Size(65, 17)
        TelefonoLabel.TabIndex = 14
        TelefonoLabel.Text = "Teléfono:"
        '
        'DireccionLabel
        '
        DireccionLabel.AutoSize = True
        DireccionLabel.Location = New System.Drawing.Point(51, 230)
        DireccionLabel.Name = "DireccionLabel"
        DireccionLabel.Size = New System.Drawing.Size(69, 17)
        DireccionLabel.TabIndex = 12
        DireccionLabel.Text = "Dirección:"
        '
        'NombreLabel
        '
        NombreLabel.AutoSize = True
        NombreLabel.Location = New System.Drawing.Point(51, 166)
        NombreLabel.Name = "NombreLabel"
        NombreLabel.Size = New System.Drawing.Size(62, 17)
        NombreLabel.TabIndex = 8
        NombreLabel.Text = "Nombre:"
        '
        'IdBodegaLabel
        '
        IdBodegaLabel.AutoSize = True
        IdBodegaLabel.Location = New System.Drawing.Point(51, 102)
        IdBodegaLabel.Name = "IdBodegaLabel"
        IdBodegaLabel.Size = New System.Drawing.Size(125, 17)
        IdBodegaLabel.TabIndex = 4
        IdBodegaLabel.Text = "Código de Bodega:"
        '
        'IdEmpresaLabel
        '
        IdEmpresaLabel.AutoSize = True
        IdEmpresaLabel.Location = New System.Drawing.Point(51, 70)
        IdEmpresaLabel.Name = "IdEmpresaLabel"
        IdEmpresaLabel.Size = New System.Drawing.Size(66, 17)
        IdEmpresaLabel.TabIndex = 2
        IdEmpresaLabel.Text = "Empresa:"
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(51, 38)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(36, 17)
        Label1.TabIndex = 0
        Label1.Text = "Pais:"
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(51, 134)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(116, 17)
        Label2.TabIndex = 6
        Label2.Text = "Código de Barrra:"
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.Location = New System.Drawing.Point(51, 198)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(125, 17)
        Label3.TabIndex = 10
        Label3.Text = "Nombre Comercial:"
        '
        'lblAlto
        '
        lblAlto.AutoSize = True
        lblAlto.Location = New System.Drawing.Point(51, 357)
        lblAlto.Name = "lblAlto"
        lblAlto.Size = New System.Drawing.Size(36, 17)
        lblAlto.TabIndex = 20
        lblAlto.Text = "Alto:"
        '
        'lblLargo
        '
        lblLargo.AutoSize = True
        lblLargo.Location = New System.Drawing.Point(51, 390)
        lblLargo.Name = "lblLargo"
        lblLargo.Size = New System.Drawing.Size(48, 17)
        lblLargo.TabIndex = 22
        lblLargo.Text = "Largo:"
        '
        'lblAncho
        '
        lblAncho.AutoSize = True
        lblAncho.Location = New System.Drawing.Point(51, 423)
        lblAncho.Name = "lblAncho"
        lblAncho.Size = New System.Drawing.Size(52, 17)
        lblAncho.TabIndex = 24
        lblAncho.Text = "Ancho:"
        '
        'Label7
        '
        Label7.AutoSize = True
        Label7.Location = New System.Drawing.Point(41, 38)
        Label7.Name = "Label7"
        Label7.Size = New System.Drawing.Size(95, 17)
        Label7.TabIndex = 0
        Label7.Text = "Coordenada X"
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(42, 70)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(95, 17)
        Label4.TabIndex = 2
        Label4.Text = "Coordenada Y"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.RibbonControl.SearchEditItem, Me.BarButtonItem1, Me.mnuGuardar, Me.mnuActualizar, Me.mnuEliminar})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 6
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1209, 193)
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Id = 1
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'mnuGuardar
        '
        Me.mnuGuardar.Caption = "Guardar"
        Me.mnuGuardar.Id = 2
        Me.mnuGuardar.ImageOptions.SvgImage = CType(resources.GetObject("mnuGuardar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuGuardar.Name = "mnuGuardar"
        '
        'mnuActualizar
        '
        Me.mnuActualizar.Caption = "Actualizar"
        Me.mnuActualizar.Id = 3
        Me.mnuActualizar.ImageOptions.SvgImage = CType(resources.GetObject("mnuActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuActualizar.Name = "mnuActualizar"
        '
        'mnuEliminar
        '
        Me.mnuEliminar.Caption = "Eliminar"
        Me.mnuEliminar.Id = 4
        Me.mnuEliminar.ImageOptions.SvgImage = CType(resources.GetObject("mnuEliminar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEliminar.Name = "mnuEliminar"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
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
        'AreaBindingSource
        '
        Me.AreaBindingSource.DataMember = "Area"
        Me.AreaBindingSource.DataSource = Me.DsBodega
        '
        'DsBodega
        '
        Me.DsBodega.DataSetName = "DsBodega"
        Me.DsBodega.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'GroupControl3
        '
        Me.GroupControl3.Controls.Add(Me.User_modTextEdit)
        Me.GroupControl3.Controls.Add(User_modLabel)
        Me.GroupControl3.Controls.Add(Me.User_agrTextEdit)
        Me.GroupControl3.Controls.Add(User_agrLabel)
        Me.GroupControl3.Controls.Add(Me.Fec_modTextEdit)
        Me.GroupControl3.Controls.Add(Fec_modLabel)
        Me.GroupControl3.Controls.Add(Me.Fec_agrTextEdit)
        Me.GroupControl3.Controls.Add(Fec_agrLabel)
        Me.GroupControl3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupControl3.Location = New System.Drawing.Point(0, 752)
        Me.GroupControl3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(1209, 123)
        Me.GroupControl3.TabIndex = 1
        Me.GroupControl3.Text = "Bitácora"
        '
        'User_modTextEdit
        '
        Me.User_modTextEdit.Enabled = False
        Me.User_modTextEdit.Location = New System.Drawing.Point(748, 30)
        Me.User_modTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.User_modTextEdit.MenuManager = Me.RibbonControl
        Me.User_modTextEdit.Name = "User_modTextEdit"
        Me.User_modTextEdit.Size = New System.Drawing.Size(205, 22)
        Me.User_modTextEdit.TabIndex = 3
        '
        'User_agrTextEdit
        '
        Me.User_agrTextEdit.Enabled = False
        Me.User_agrTextEdit.Location = New System.Drawing.Point(296, 30)
        Me.User_agrTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.User_agrTextEdit.MenuManager = Me.RibbonControl
        Me.User_agrTextEdit.Name = "User_agrTextEdit"
        Me.User_agrTextEdit.Size = New System.Drawing.Size(205, 22)
        Me.User_agrTextEdit.TabIndex = 1
        '
        'Fec_modTextEdit
        '
        Me.Fec_modTextEdit.Enabled = False
        Me.Fec_modTextEdit.Location = New System.Drawing.Point(748, 62)
        Me.Fec_modTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Fec_modTextEdit.MenuManager = Me.RibbonControl
        Me.Fec_modTextEdit.Name = "Fec_modTextEdit"
        Me.Fec_modTextEdit.Size = New System.Drawing.Size(205, 22)
        Me.Fec_modTextEdit.TabIndex = 6
        '
        'Fec_agrTextEdit
        '
        Me.Fec_agrTextEdit.Enabled = False
        Me.Fec_agrTextEdit.Location = New System.Drawing.Point(296, 66)
        Me.Fec_agrTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Fec_agrTextEdit.MenuManager = Me.RibbonControl
        Me.Fec_agrTextEdit.Name = "Fec_agrTextEdit"
        Me.Fec_agrTextEdit.Size = New System.Drawing.Size(205, 22)
        Me.Fec_agrTextEdit.TabIndex = 7
        '
        'TabUbicacion
        '
        Me.TabUbicacion.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.TabUbicacion.Controls.Add(Me.tLArea)
        Me.TabUbicacion.Location = New System.Drawing.Point(4, 25)
        Me.TabUbicacion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TabUbicacion.Name = "TabUbicacion"
        Me.TabUbicacion.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TabUbicacion.Size = New System.Drawing.Size(1201, 653)
        Me.TabUbicacion.TabIndex = 4
        Me.TabUbicacion.Text = "Ubicación"
        '
        'tLArea
        '
        Me.tLArea.Columns.AddRange(New DevExpress.XtraTreeList.Columns.TreeListColumn() {Me.colIdArea, Me.colIdBodega, Me.colCodigo, Me.colDescripcion})
        Me.tLArea.DataSource = Me.AreaBindingSource
        Me.tLArea.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tLArea.Location = New System.Drawing.Point(3, 4)
        Me.tLArea.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.tLArea.MinWidth = 23
        Me.tLArea.Name = "tLArea"
        Me.tLArea.Size = New System.Drawing.Size(1195, 645)
        Me.tLArea.TabIndex = 0
        Me.tLArea.TreeLevelWidth = 21
        '
        'colIdArea
        '
        Me.colIdArea.FieldName = "IdArea"
        Me.colIdArea.MinWidth = 23
        Me.colIdArea.Name = "colIdArea"
        Me.colIdArea.Visible = True
        Me.colIdArea.VisibleIndex = 0
        Me.colIdArea.Width = 57
        '
        'colIdBodega
        '
        Me.colIdBodega.FieldName = "IdBodega"
        Me.colIdBodega.MinWidth = 23
        Me.colIdBodega.Name = "colIdBodega"
        Me.colIdBodega.Visible = True
        Me.colIdBodega.VisibleIndex = 1
        Me.colIdBodega.Width = 57
        '
        'colCodigo
        '
        Me.colCodigo.FieldName = "Codigo"
        Me.colCodigo.MinWidth = 23
        Me.colCodigo.Name = "colCodigo"
        Me.colCodigo.Visible = True
        Me.colCodigo.VisibleIndex = 3
        Me.colCodigo.Width = 57
        '
        'colDescripcion
        '
        Me.colDescripcion.FieldName = "Descripcion"
        Me.colDescripcion.MinWidth = 23
        Me.colDescripcion.Name = "colDescripcion"
        Me.colDescripcion.Visible = True
        Me.colDescripcion.VisibleIndex = 2
        Me.colDescripcion.Width = 57
        '
        'tabBodega
        '
        Me.tabBodega.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.tabBodega.Controls.Add(Me.GroupControl4)
        Me.tabBodega.Controls.Add(Me.GroupControl1)
        Me.tabBodega.Controls.Add(Me.GroupControl2)
        Me.tabBodega.ForeColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.tabBodega.Location = New System.Drawing.Point(4, 25)
        Me.tabBodega.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.tabBodega.Name = "tabBodega"
        Me.tabBodega.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.tabBodega.Size = New System.Drawing.Size(1201, 653)
        Me.tabBodega.TabIndex = 0
        Me.tabBodega.Text = "Bodega"
        '
        'GroupControl4
        '
        Me.GroupControl4.Controls.Add(Me.txtCoordenadaX)
        Me.GroupControl4.Controls.Add(Label4)
        Me.GroupControl4.Controls.Add(Me.txtCoordenadaY)
        Me.GroupControl4.Controls.Add(Label7)
        Me.GroupControl4.Location = New System.Drawing.Point(514, 215)
        Me.GroupControl4.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl4.Name = "GroupControl4"
        Me.GroupControl4.Size = New System.Drawing.Size(484, 108)
        Me.GroupControl4.TabIndex = 2
        Me.GroupControl4.Text = "Geolocalización"
        '
        'txtCoordenadaX
        '
        Me.txtCoordenadaX.Location = New System.Drawing.Point(187, 34)
        Me.txtCoordenadaX.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCoordenadaX.MenuManager = Me.RibbonControl
        Me.txtCoordenadaX.Name = "txtCoordenadaX"
        Me.txtCoordenadaX.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.txtCoordenadaX.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.txtCoordenadaX.Properties.LookAndFeel.SkinName = "Office 2010 Blue"
        Me.txtCoordenadaX.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.txtCoordenadaX.Size = New System.Drawing.Size(217, 22)
        Me.txtCoordenadaX.TabIndex = 1
        '
        'txtCoordenadaY
        '
        Me.txtCoordenadaY.Location = New System.Drawing.Point(187, 66)
        Me.txtCoordenadaY.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCoordenadaY.MenuManager = Me.RibbonControl
        Me.txtCoordenadaY.Name = "txtCoordenadaY"
        Me.txtCoordenadaY.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.txtCoordenadaY.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.txtCoordenadaY.Properties.LookAndFeel.SkinName = "Office 2010 Blue"
        Me.txtCoordenadaY.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.txtCoordenadaY.Size = New System.Drawing.Size(217, 22)
        Me.txtCoordenadaY.TabIndex = 3
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.cmbEmpresa)
        Me.GroupControl1.Controls.Add(Me.cmbPais)
        Me.GroupControl1.Controls.Add(Me.txtAncho)
        Me.GroupControl1.Controls.Add(Me.txtLargo)
        Me.GroupControl1.Controls.Add(Me.txtAlto)
        Me.GroupControl1.Controls.Add(lblAncho)
        Me.GroupControl1.Controls.Add(lblLargo)
        Me.GroupControl1.Controls.Add(lblAlto)
        Me.GroupControl1.Controls.Add(Label3)
        Me.GroupControl1.Controls.Add(Me.txtNombreComercial)
        Me.GroupControl1.Controls.Add(Me.txtCodigoBarra)
        Me.GroupControl1.Controls.Add(Label2)
        Me.GroupControl1.Controls.Add(Label1)
        Me.GroupControl1.Controls.Add(Me.txtCodigo)
        Me.GroupControl1.Controls.Add(IdEmpresaLabel)
        Me.GroupControl1.Controls.Add(IdBodegaLabel)
        Me.GroupControl1.Controls.Add(NombreLabel)
        Me.GroupControl1.Controls.Add(Me.txtNombre)
        Me.GroupControl1.Controls.Add(DireccionLabel)
        Me.GroupControl1.Controls.Add(Me.DireccionTextEdit)
        Me.GroupControl1.Controls.Add(TelefonoLabel)
        Me.GroupControl1.Controls.Add(Me.TelefonoTextEdit)
        Me.GroupControl1.Controls.Add(EmailLabel)
        Me.GroupControl1.Controls.Add(Me.EmailTextEdit)
        Me.GroupControl1.Controls.Add(EncargadoLabel)
        Me.GroupControl1.Controls.Add(Me.EncargadoTextEdit)
        Me.GroupControl1.Location = New System.Drawing.Point(20, 7)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(484, 464)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Datos de Bodega"
        '
        'cmbEmpresa
        '
        Me.cmbEmpresa.Location = New System.Drawing.Point(176, 66)
        Me.cmbEmpresa.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbEmpresa.MenuManager = Me.RibbonControl
        Me.cmbEmpresa.Name = "cmbEmpresa"
        Me.cmbEmpresa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbEmpresa.Properties.NullText = ""
        Me.cmbEmpresa.Size = New System.Drawing.Size(252, 22)
        Me.cmbEmpresa.TabIndex = 3
        '
        'cmbPais
        '
        Me.cmbPais.Location = New System.Drawing.Point(176, 34)
        Me.cmbPais.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbPais.MenuManager = Me.RibbonControl
        Me.cmbPais.Name = "cmbPais"
        Me.cmbPais.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPais.Properties.NullText = ""
        Me.cmbPais.Size = New System.Drawing.Size(252, 22)
        Me.cmbPais.TabIndex = 1
        '
        'txtAncho
        '
        Me.txtAncho.DecimalPlaces = 6
        Me.txtAncho.Location = New System.Drawing.Point(176, 421)
        Me.txtAncho.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtAncho.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtAncho.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.txtAncho.Name = "txtAncho"
        Me.txtAncho.Size = New System.Drawing.Size(252, 23)
        Me.txtAncho.TabIndex = 25
        '
        'txtLargo
        '
        Me.txtLargo.DecimalPlaces = 6
        Me.txtLargo.Location = New System.Drawing.Point(176, 388)
        Me.txtLargo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtLargo.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtLargo.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.txtLargo.Name = "txtLargo"
        Me.txtLargo.Size = New System.Drawing.Size(252, 23)
        Me.txtLargo.TabIndex = 23
        '
        'txtAlto
        '
        Me.txtAlto.DecimalPlaces = 6
        Me.txtAlto.Location = New System.Drawing.Point(176, 354)
        Me.txtAlto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtAlto.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtAlto.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.txtAlto.Name = "txtAlto"
        Me.txtAlto.Size = New System.Drawing.Size(252, 23)
        Me.txtAlto.TabIndex = 21
        '
        'txtNombreComercial
        '
        Me.txtNombreComercial.Location = New System.Drawing.Point(176, 196)
        Me.txtNombreComercial.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNombreComercial.MenuManager = Me.RibbonControl
        Me.txtNombreComercial.Name = "txtNombreComercial"
        Me.txtNombreComercial.Size = New System.Drawing.Size(252, 22)
        Me.txtNombreComercial.TabIndex = 11
        '
        'txtCodigoBarra
        '
        Me.txtCodigoBarra.Location = New System.Drawing.Point(176, 132)
        Me.txtCodigoBarra.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCodigoBarra.MenuManager = Me.RibbonControl
        Me.txtCodigoBarra.Name = "txtCodigoBarra"
        Me.txtCodigoBarra.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.txtCodigoBarra.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.txtCodigoBarra.Properties.LookAndFeel.SkinName = "Office 2010 Blue"
        Me.txtCodigoBarra.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.txtCodigoBarra.Size = New System.Drawing.Size(252, 22)
        Me.txtCodigoBarra.TabIndex = 7
        '
        'txtCodigo
        '
        Me.txtCodigo.Location = New System.Drawing.Point(176, 100)
        Me.txtCodigo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCodigo.MenuManager = Me.RibbonControl
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.txtCodigo.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.txtCodigo.Properties.LookAndFeel.SkinName = "Office 2010 Blue"
        Me.txtCodigo.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.txtCodigo.Size = New System.Drawing.Size(252, 22)
        Me.txtCodigo.TabIndex = 5
        '
        'txtNombre
        '
        Me.txtNombre.Location = New System.Drawing.Point(176, 164)
        Me.txtNombre.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNombre.MenuManager = Me.RibbonControl
        Me.txtNombre.Name = "txtNombre"
        Me.txtNombre.Size = New System.Drawing.Size(252, 22)
        Me.txtNombre.TabIndex = 9
        '
        'DireccionTextEdit
        '
        Me.DireccionTextEdit.Location = New System.Drawing.Point(176, 228)
        Me.DireccionTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DireccionTextEdit.MenuManager = Me.RibbonControl
        Me.DireccionTextEdit.Name = "DireccionTextEdit"
        Me.DireccionTextEdit.Size = New System.Drawing.Size(252, 22)
        Me.DireccionTextEdit.TabIndex = 13
        '
        'TelefonoTextEdit
        '
        Me.TelefonoTextEdit.Location = New System.Drawing.Point(176, 260)
        Me.TelefonoTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TelefonoTextEdit.MenuManager = Me.RibbonControl
        Me.TelefonoTextEdit.Name = "TelefonoTextEdit"
        Me.TelefonoTextEdit.Size = New System.Drawing.Size(252, 22)
        Me.TelefonoTextEdit.TabIndex = 15
        '
        'EmailTextEdit
        '
        Me.EmailTextEdit.Location = New System.Drawing.Point(176, 292)
        Me.EmailTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.EmailTextEdit.MenuManager = Me.RibbonControl
        Me.EmailTextEdit.Name = "EmailTextEdit"
        Me.EmailTextEdit.Size = New System.Drawing.Size(252, 22)
        Me.EmailTextEdit.TabIndex = 17
        '
        'EncargadoTextEdit
        '
        Me.EncargadoTextEdit.Location = New System.Drawing.Point(176, 324)
        Me.EncargadoTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.EncargadoTextEdit.MenuManager = Me.RibbonControl
        Me.EncargadoTextEdit.Name = "EncargadoTextEdit"
        Me.EncargadoTextEdit.Size = New System.Drawing.Size(252, 22)
        Me.EncargadoTextEdit.TabIndex = 19
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.cmdBuscar)
        Me.GroupControl2.Controls.Add(Me.txtUbicacionRecepcion)
        Me.GroupControl2.Controls.Add(Ubic_pickingLabel)
        Me.GroupControl2.Controls.Add(Me.txtUbicacionPicking)
        Me.GroupControl2.Controls.Add(Me.chkActivo)
        Me.GroupControl2.Controls.Add(Ubic_despachoLabel)
        Me.GroupControl2.Controls.Add(ActivoLabel)
        Me.GroupControl2.Controls.Add(Ubic_recepcionLabel)
        Me.GroupControl2.Controls.Add(Me.txtUbicacionDespacho)
        Me.GroupControl2.Controls.Add(Me.txtUbicacionMerma)
        Me.GroupControl2.Controls.Add(Ubic_mermaLabel)
        Me.GroupControl2.Location = New System.Drawing.Point(511, 7)
        Me.GroupControl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(484, 198)
        Me.GroupControl2.TabIndex = 1
        Me.GroupControl2.Text = "Ubicaciones por defecto"
        '
        'cmdBuscar
        '
        Me.cmdBuscar.Location = New System.Drawing.Point(411, 34)
        Me.cmdBuscar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmdBuscar.Name = "cmdBuscar"
        Me.cmdBuscar.Size = New System.Drawing.Size(48, 121)
        Me.cmdBuscar.TabIndex = 8
        Me.cmdBuscar.UseVisualStyleBackColor = True
        '
        'txtUbicacionRecepcion
        '
        Me.txtUbicacionRecepcion.Location = New System.Drawing.Point(187, 34)
        Me.txtUbicacionRecepcion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtUbicacionRecepcion.MenuManager = Me.RibbonControl
        Me.txtUbicacionRecepcion.Name = "txtUbicacionRecepcion"
        Me.txtUbicacionRecepcion.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.txtUbicacionRecepcion.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.txtUbicacionRecepcion.Properties.LookAndFeel.SkinName = "Office 2010 Blue"
        Me.txtUbicacionRecepcion.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.txtUbicacionRecepcion.Size = New System.Drawing.Size(217, 22)
        Me.txtUbicacionRecepcion.TabIndex = 1
        '
        'txtUbicacionPicking
        '
        Me.txtUbicacionPicking.Location = New System.Drawing.Point(187, 66)
        Me.txtUbicacionPicking.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtUbicacionPicking.MenuManager = Me.RibbonControl
        Me.txtUbicacionPicking.Name = "txtUbicacionPicking"
        Me.txtUbicacionPicking.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.txtUbicacionPicking.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.txtUbicacionPicking.Properties.LookAndFeel.SkinName = "Office 2010 Blue"
        Me.txtUbicacionPicking.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.txtUbicacionPicking.Size = New System.Drawing.Size(217, 22)
        Me.txtUbicacionPicking.TabIndex = 3
        '
        'chkActivo
        '
        Me.chkActivo.EditValue = True
        Me.chkActivo.Location = New System.Drawing.Point(187, 164)
        Me.chkActivo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkActivo.MenuManager = Me.RibbonControl
        Me.chkActivo.Name = "chkActivo"
        Me.chkActivo.Properties.Caption = ""
        Me.chkActivo.Size = New System.Drawing.Size(117, 24)
        Me.chkActivo.TabIndex = 10
        '
        'txtUbicacionDespacho
        '
        Me.txtUbicacionDespacho.Location = New System.Drawing.Point(187, 100)
        Me.txtUbicacionDespacho.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtUbicacionDespacho.MenuManager = Me.RibbonControl
        Me.txtUbicacionDespacho.Name = "txtUbicacionDespacho"
        Me.txtUbicacionDespacho.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.txtUbicacionDespacho.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.txtUbicacionDespacho.Properties.LookAndFeel.SkinName = "Office 2010 Blue"
        Me.txtUbicacionDespacho.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.txtUbicacionDespacho.Size = New System.Drawing.Size(217, 22)
        Me.txtUbicacionDespacho.TabIndex = 5
        '
        'txtUbicacionMerma
        '
        Me.txtUbicacionMerma.Location = New System.Drawing.Point(187, 130)
        Me.txtUbicacionMerma.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtUbicacionMerma.MenuManager = Me.RibbonControl
        Me.txtUbicacionMerma.Name = "txtUbicacionMerma"
        Me.txtUbicacionMerma.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.txtUbicacionMerma.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.txtUbicacionMerma.Properties.LookAndFeel.SkinName = "Office 2010 Blue"
        Me.txtUbicacionMerma.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.txtUbicacionMerma.Size = New System.Drawing.Size(217, 22)
        Me.txtUbicacionMerma.TabIndex = 7
        '
        'tabDatos
        '
        Me.tabDatos.Controls.Add(Me.tabBodega)
        Me.tabDatos.Controls.Add(Me.TabUbicacion)
        Me.tabDatos.Controls.Add(Me.TabPage1)
        Me.tabDatos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabDatos.Location = New System.Drawing.Point(0, 193)
        Me.tabDatos.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.tabDatos.Name = "tabDatos"
        Me.tabDatos.SelectedIndex = 0
        Me.tabDatos.Size = New System.Drawing.Size(1209, 682)
        Me.tabDatos.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.tlPrueba)
        Me.TabPage1.Location = New System.Drawing.Point(4, 25)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TabPage1.Size = New System.Drawing.Size(1201, 653)
        Me.TabPage1.TabIndex = 5
        Me.TabPage1.Text = "Padre"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'tlPrueba
        '
        Me.tlPrueba.Columns.AddRange(New DevExpress.XtraTreeList.Columns.TreeListColumn() {Me.Padre})
        Me.tlPrueba.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlPrueba.Location = New System.Drawing.Point(3, 4)
        Me.tlPrueba.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.tlPrueba.MinWidth = 23
        Me.tlPrueba.Name = "tlPrueba"
        Me.tlPrueba.BeginUnboundLoad()
        Me.tlPrueba.AppendNode(New Object() {"Hijo 1"}, -1)
        Me.tlPrueba.AppendNode(New Object() {"Nieto 1"}, 0)
        Me.tlPrueba.AppendNode(New Object() {"Nieto 2"}, 0)
        Me.tlPrueba.AppendNode(New Object() {"Hijo 2"}, -1)
        Me.tlPrueba.AppendNode(New Object() {"Nieto 1"}, 3)
        Me.tlPrueba.AppendNode(New Object() {"Nieto 2"}, 3)
        Me.tlPrueba.EndUnboundLoad()
        Me.tlPrueba.OptionsBehavior.AutoSelectAllInEditor = False
        Me.tlPrueba.OptionsBehavior.Editable = False
        Me.tlPrueba.OptionsBehavior.ReadOnly = True
        Me.tlPrueba.OptionsFilter.FilterMode = DevExpress.XtraTreeList.FilterMode.ParentBranch
        Me.tlPrueba.OptionsFilter.ShowAllValuesInFilterPopup = True
        Me.tlPrueba.OptionsFind.AlwaysVisible = True
        Me.tlPrueba.Size = New System.Drawing.Size(1195, 645)
        Me.tlPrueba.TabIndex = 0
        Me.tlPrueba.TreeLevelWidth = 21
        '
        'Padre
        '
        Me.Padre.Caption = "Padre"
        Me.Padre.FieldName = "Padre"
        Me.Padre.MinWidth = 61
        Me.Padre.Name = "Padre"
        Me.Padre.OptionsFilter.AutoFilterCondition = DevExpress.XtraTreeList.Columns.AutoFilterCondition.Contains
        Me.Padre.OptionsFilter.FilterPopupMode = DevExpress.XtraTreeList.FilterPopupMode.List
        Me.Padre.Visible = True
        Me.Padre.VisibleIndex = 0
        Me.Padre.Width = 87
        '
        'frmBodegaTree
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1209, 875)
        Me.Controls.Add(Me.GroupControl3)
        Me.Controls.Add(Me.tabDatos)
        Me.Controls.Add(Me.RibbonControl)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmBodegaTree"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Bodega"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AreaBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsBodega, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        Me.GroupControl3.PerformLayout()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabUbicacion.ResumeLayout(False)
        CType(Me.tLArea, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabBodega.ResumeLayout(False)
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl4.ResumeLayout(False)
        Me.GroupControl4.PerformLayout()
        CType(Me.txtCoordenadaX.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCoordenadaY.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.cmbEmpresa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPais.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAncho, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLargo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAlto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreComercial.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodigoBarra.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodigo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombre.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DireccionTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TelefonoTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmailTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EncargadoTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.GroupControl2.PerformLayout()
        CType(Me.txtUbicacionRecepcion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUbicacionPicking.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUbicacionDespacho.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUbicacionMerma.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabDatos.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.tlPrueba, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEliminar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents User_modTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents User_agrTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_modTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_agrTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents AreaBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DsBodega As DsBodega
    Friend WithEvents TabUbicacion As System.Windows.Forms.TabPage
    Friend WithEvents tLArea As DevExpress.XtraTreeList.TreeList
    Friend WithEvents colIdArea As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents colIdBodega As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents colCodigo As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents colDescripcion As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents tabBodega As System.Windows.Forms.TabPage
    Friend WithEvents GroupControl4 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtCoordenadaX As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCoordenadaY As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtAncho As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtLargo As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtAlto As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtNombreComercial As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCodigoBarra As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCodigo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNombre As DevExpress.XtraEditors.TextEdit
    Friend WithEvents DireccionTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TelefonoTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents EmailTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents EncargadoTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cmdBuscar As System.Windows.Forms.Button
    Friend WithEvents txtUbicacionRecepcion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtUbicacionPicking As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkActivo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtUbicacionDespacho As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtUbicacionMerma As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tabDatos As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents tlPrueba As DevExpress.XtraTreeList.TreeList
    Friend WithEvents Padre As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents cmbPais As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbEmpresa As DevExpress.XtraEditors.LookUpEdit
End Class
