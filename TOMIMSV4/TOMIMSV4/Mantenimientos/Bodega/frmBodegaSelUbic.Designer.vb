<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBodegaSelUbic
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
                If pObjDet IsNot Nothing Then
                    pObjDet.Dispose()
                    pObjDet = Nothing
                End If
                If DTArea IsNot Nothing Then
                    DTArea.Dispose()
                    DTArea = Nothing
                End If
                If DTSector IsNot Nothing Then
                    DTSector.Dispose()
                    DTSector = Nothing
                End If
                If DTTramo IsNot Nothing Then
                    DTTramo.Dispose()
                    DTTramo = Nothing
                End If
                If DTUbiacion IsNot Nothing Then
                    DTUbiacion.Dispose()
                    DTUbiacion = Nothing
                End If
                If pUbs IsNot Nothing Then
                    pUbs.Dispose()
                    pUbs = Nothing
                End If
                If BePresentacion IsNot Nothing Then
                    BePresentacion.Dispose()
                    BePresentacion = Nothing
                End If
                If BeProducto IsNot Nothing Then
                    BeProducto.Dispose()
                    BeProducto = Nothing
                End If
                If BeEstadoProd IsNot Nothing Then
                    BeEstadoProd.Dispose()
                    BeEstadoProd = Nothing
                End If
                If DTOri IsNot Nothing Then
                    DTOri.Dispose()
                    DTOri = Nothing
                End If
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBodegaSelUbic))
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim GridLevelNode2 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Me.RibbonPageGroup3 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.chkActivos = New DevExpress.XtraBars.BarCheckItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.mnuAplicar = New DevExpress.XtraBars.BarButtonItem()
        Me.mmuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuSalir = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImportarExcel = New DevExpress.XtraBars.BarButtonItem()
        Me.lblRegs = New DevExpress.XtraBars.BarStaticItem()
        Me.BarEditItem2 = New DevExpress.XtraBars.BarEditItem()
        Me.mnuNuevo = New DevExpress.XtraBars.BarButtonItem()
        Me.BarListItem1 = New DevExpress.XtraBars.BarListItem()
        Me.BarSubItem2 = New DevExpress.XtraBars.BarSubItem()
        Me.RibbonGalleryBarItem1 = New DevExpress.XtraBars.RibbonGalleryBarItem()
        Me.BarEditItem1 = New DevExpress.XtraBars.BarEditItem()
        Me.BarSubItem1 = New DevExpress.XtraBars.BarSubItem()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.DsBodega = New TOMWMS.DsBodega()
        Me.BodegaBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.tlUbicacionesTodas = New DevExpress.XtraTreeList.TreeList()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.txtFiltroUbic = New DevExpress.XtraEditors.TextEdit()
        Me.btnFiltLimpia = New DevExpress.XtraEditors.SimpleButton()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.PanelControl5 = New DevExpress.XtraEditors.PanelControl()
        Me.lblDisponible = New System.Windows.Forms.Label()
        Me.lblTDisponible = New System.Windows.Forms.Label()
        Me.chkActivo = New System.Windows.Forms.CheckBox()
        Me.chkBloqueado = New System.Windows.Forms.CheckBox()
        Me.chkDañado = New System.Windows.Forms.CheckBox()
        Me.chkAceptaPallet = New System.Windows.Forms.CheckBox()
        Me.lblVolumen = New System.Windows.Forms.Label()
        Me.lblAlto = New System.Windows.Forms.Label()
        Me.lblLargo = New System.Windows.Forms.Label()
        Me.lblAncho = New System.Windows.Forms.Label()
        Me.lblNivel = New System.Windows.Forms.Label()
        Me.lblTVolumen = New System.Windows.Forms.Label()
        Me.lblTAlto = New System.Windows.Forms.Label()
        Me.lblTLargo = New System.Windows.Forms.Label()
        Me.lblTAncho = New System.Windows.Forms.Label()
        Me.lblTNivel = New System.Windows.Forms.Label()
        Me.lblUbicacion = New System.Windows.Forms.Label()
        Me.PanelControl4 = New DevExpress.XtraEditors.PanelControl()
        Me.lblVolumenDisponible = New System.Windows.Forms.Label()
        Me.lblVolumenDispTitulo = New System.Windows.Forms.Label()
        Me.lblDisponibleAlmacenaje = New System.Windows.Forms.Label()
        Me.lblDisponibleAlmacenajeTitulo = New System.Windows.Forms.Label()
        Me.dgridStockUbic = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView6 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.lblUbicacionTitulo = New System.Windows.Forms.Label()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.dgridUbicacionesSugeridas = New DevExpress.XtraGrid.GridControl()
        Me.GridViewDet = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView4 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.PanelControl6 = New DevExpress.XtraEditors.PanelControl()
        Me.lblTFalta = New System.Windows.Forms.Label()
        Me.lblAlmacenado = New System.Windows.Forms.Label()
        Me.lblFalta = New System.Windows.Forms.Label()
        Me.lblTAlmacenado = New System.Windows.Forms.Label()
        Me.DgridDetalleUbics = New DevExpress.XtraGrid.GridControl()
        Me.GridUbic = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView5 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ToolStripP = New System.Windows.Forms.ToolStrip()
        Me.cmdDesactivarParametro = New System.Windows.Forms.ToolStripButton()
        Me.PanelControl3 = New DevExpress.XtraEditors.PanelControl()
        Me.lblProducto = New System.Windows.Forms.Label()
        Me.xtrBodegaSelUbic = New DevExpress.XtraTab.XtraTabControl()
        Me.UbicSug = New DevExpress.XtraTab.XtraTabPage()
        Me.TodasUbicaciones = New DevExpress.XtraTab.XtraTabPage()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TextEdit1 = New DevExpress.XtraEditors.TextEdit()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsBodega, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BodegaBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tlUbicacionesTodas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.txtFiltroUbic.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.PanelControl5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl5.SuspendLayout()
        CType(Me.PanelControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl4.SuspendLayout()
        CType(Me.dgridStockUbic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgridUbicacionesSugeridas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewDet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.PanelControl6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl6.SuspendLayout()
        CType(Me.DgridDetalleUbics, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridUbic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStripP.SuspendLayout()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl3.SuspendLayout()
        CType(Me.xtrBodegaSelUbic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtrBodegaSelUbic.SuspendLayout()
        Me.UbicSug.SuspendLayout()
        Me.TodasUbicaciones.SuspendLayout()
        CType(Me.TextEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'RibbonPageGroup3
        '
        Me.RibbonPageGroup3.ItemLinks.Add(Me.chkActivos)
        Me.RibbonPageGroup3.Name = "RibbonPageGroup3"
        '
        'chkActivos
        '
        Me.chkActivos.BindableChecked = True
        Me.chkActivos.Caption = "Activos"
        Me.chkActivos.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText
        Me.chkActivos.Checked = True
        Me.chkActivos.Id = 5
        Me.chkActivos.Name = "chkActivos"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup3, Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Opciones de Lista"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ImageOptions.Image = CType(resources.GetObject("RibbonPageGroup1.ImageOptions.Image"), System.Drawing.Image)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuAplicar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mmuActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuSalir)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'mnuAplicar
        '
        Me.mnuAplicar.Caption = "Aplicar    "
        Me.mnuAplicar.Description = "Aplicar"
        Me.mnuAplicar.Id = 16
        Me.mnuAplicar.ImageOptions.SvgImage = CType(resources.GetObject("mnuAplicar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuAplicar.Name = "mnuAplicar"
        '
        'mmuActualizar
        '
        Me.mmuActualizar.Caption = "&Actualizar"
        Me.mmuActualizar.Id = 10
        Me.mmuActualizar.ImageOptions.SvgImage = CType(resources.GetObject("mmuActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mmuActualizar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A))
        Me.mmuActualizar.Name = "mmuActualizar"
        '
        'mnuSalir
        '
        Me.mnuSalir.Caption = "&Salir"
        Me.mnuSalir.Id = 11
        Me.mnuSalir.ImageOptions.SvgImage = CType(resources.GetObject("mnuSalir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuSalir.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S))
        Me.mnuSalir.Name = "mnuSalir"
        '
        'cmdImportarExcel
        '
        Me.cmdImportarExcel.Caption = "Importar Excel"
        Me.cmdImportarExcel.Id = 14
        Me.cmdImportarExcel.Name = "cmdImportarExcel"
        '
        'lblRegs
        '
        Me.lblRegs.Caption = "Registros:"
        Me.lblRegs.Id = 12
        Me.lblRegs.Name = "lblRegs"
        '
        'BarEditItem2
        '
        Me.BarEditItem2.Caption = "BarEditItem2"
        Me.BarEditItem2.Edit = Nothing
        Me.BarEditItem2.Id = 9
        Me.BarEditItem2.Name = "BarEditItem2"
        '
        'mnuNuevo
        '
        Me.mnuNuevo.Caption = "&Nuevo"
        Me.mnuNuevo.Id = 7
        Me.mnuNuevo.ImageOptions.Image = CType(resources.GetObject("mnuNuevo.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuNuevo.ImageOptions.LargeImage = CType(resources.GetObject("mnuNuevo.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuNuevo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N))
        Me.mnuNuevo.Name = "mnuNuevo"
        '
        'BarListItem1
        '
        Me.BarListItem1.Caption = "BarListItem1"
        Me.BarListItem1.Id = 6
        Me.BarListItem1.Name = "BarListItem1"
        '
        'BarSubItem2
        '
        Me.BarSubItem2.Caption = "BarSubItem2"
        Me.BarSubItem2.Id = 4
        Me.BarSubItem2.Name = "BarSubItem2"
        '
        'RibbonGalleryBarItem1
        '
        Me.RibbonGalleryBarItem1.Caption = "RibbonGalleryBarItem1"
        Me.RibbonGalleryBarItem1.Id = 3
        Me.RibbonGalleryBarItem1.Name = "RibbonGalleryBarItem1"
        '
        'BarEditItem1
        '
        Me.BarEditItem1.Caption = "BarEditItem1"
        Me.BarEditItem1.Edit = Nothing
        Me.BarEditItem1.Id = 2
        Me.BarEditItem1.Name = "BarEditItem1"
        '
        'BarSubItem1
        '
        Me.BarSubItem1.Caption = "BarSubItem1"
        Me.BarSubItem1.Id = 1
        Me.BarSubItem1.Name = "BarSubItem1"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.BarSubItem1, Me.BarEditItem1, Me.RibbonGalleryBarItem1, Me.BarSubItem2, Me.chkActivos, Me.BarListItem1, Me.mnuNuevo, Me.BarEditItem2, Me.mmuActualizar, Me.mnuSalir, Me.lblRegs, Me.cmdImportarExcel, Me.BarButtonItem1, Me.mnuAplicar})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 17
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1291, 193)
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "BarButtonItem1"
        Me.BarButtonItem1.Id = 15
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'DsBodega
        '
        Me.DsBodega.DataSetName = "DsBodega"
        Me.DsBodega.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'BodegaBindingSource
        '
        Me.BodegaBindingSource.DataMember = "Bodega"
        Me.BodegaBindingSource.DataSource = Me.DsBodega
        '
        'tlUbicacionesTodas
        '
        Me.tlUbicacionesTodas.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.tlUbicacionesTodas.Location = New System.Drawing.Point(2, 219)
        Me.tlUbicacionesTodas.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.tlUbicacionesTodas.Name = "tlUbicacionesTodas"
        Me.tlUbicacionesTodas.OptionsBehavior.Editable = False
        Me.tlUbicacionesTodas.OptionsBehavior.ReadOnly = True
        Me.tlUbicacionesTodas.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.tlUbicacionesTodas.Size = New System.Drawing.Size(314, 449)
        Me.tlUbicacionesTodas.TabIndex = 2
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.txtFiltroUbic)
        Me.GroupControl1.Controls.Add(Me.btnFiltLimpia)
        Me.GroupControl1.Controls.Add(Me.tlUbicacionesTodas)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(318, 670)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Bodega -> Area -> Sector -> Tramo -> Ubicación"
        '
        'txtFiltroUbic
        '
        Me.txtFiltroUbic.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.[True]
        Me.txtFiltroUbic.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtFiltroUbic.Location = New System.Drawing.Point(2, 28)
        Me.txtFiltroUbic.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtFiltroUbic.MenuManager = Me.RibbonControl
        Me.txtFiltroUbic.Name = "txtFiltroUbic"
        Me.txtFiltroUbic.Properties.Appearance.BackColor = System.Drawing.Color.AliceBlue
        Me.txtFiltroUbic.Properties.Appearance.Font = New System.Drawing.Font("Consolas", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFiltroUbic.Properties.Appearance.Options.UseBackColor = True
        Me.txtFiltroUbic.Properties.Appearance.Options.UseFont = True
        Me.txtFiltroUbic.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat
        Me.txtFiltroUbic.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtFiltroUbic.Size = New System.Drawing.Size(285, 30)
        Me.txtFiltroUbic.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.txtFiltroUbic, "Utilice expresiones como <b><href=N1>N1</href></b> para buscar ubicaciones de niv" &
        "el uno; ")
        Me.txtFiltroUbic.ToolTip = resources.GetString("txtFiltroUbic.ToolTip")
        Me.txtFiltroUbic.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information
        Me.txtFiltroUbic.ToolTipTitle = "Filtro de búsqueda"
        '
        'btnFiltLimpia
        '
        Me.btnFiltLimpia.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnFiltLimpia.ImageOptions.Image = CType(resources.GetObject("btnFiltLimpia.ImageOptions.Image"), System.Drawing.Image)
        Me.btnFiltLimpia.Location = New System.Drawing.Point(287, 28)
        Me.btnFiltLimpia.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnFiltLimpia.Name = "btnFiltLimpia"
        Me.btnFiltLimpia.Size = New System.Drawing.Size(29, 191)
        Me.btnFiltLimpia.TabIndex = 1
        '
        'GridView2
        '
        Me.GridView2.Name = "GridView2"
        '
        'PanelControl2
        '
        Me.PanelControl2.AutoSize = True
        Me.PanelControl2.Controls.Add(Me.PanelControl5)
        Me.PanelControl2.Controls.Add(Me.lblUbicacion)
        Me.PanelControl2.Controls.Add(Me.PanelControl4)
        Me.PanelControl2.Controls.Add(Me.dgridStockUbic)
        Me.PanelControl2.Controls.Add(Me.lblUbicacionTitulo)
        Me.PanelControl2.Location = New System.Drawing.Point(329, 4)
        Me.PanelControl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(588, 700)
        Me.PanelControl2.TabIndex = 1
        '
        'PanelControl5
        '
        Me.PanelControl5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelControl5.Controls.Add(Me.lblDisponible)
        Me.PanelControl5.Controls.Add(Me.lblTDisponible)
        Me.PanelControl5.Controls.Add(Me.chkActivo)
        Me.PanelControl5.Controls.Add(Me.chkBloqueado)
        Me.PanelControl5.Controls.Add(Me.chkDañado)
        Me.PanelControl5.Controls.Add(Me.chkAceptaPallet)
        Me.PanelControl5.Controls.Add(Me.lblVolumen)
        Me.PanelControl5.Controls.Add(Me.lblAlto)
        Me.PanelControl5.Controls.Add(Me.lblLargo)
        Me.PanelControl5.Controls.Add(Me.lblAncho)
        Me.PanelControl5.Controls.Add(Me.lblNivel)
        Me.PanelControl5.Controls.Add(Me.lblTVolumen)
        Me.PanelControl5.Controls.Add(Me.lblTAlto)
        Me.PanelControl5.Controls.Add(Me.lblTLargo)
        Me.PanelControl5.Controls.Add(Me.lblTAncho)
        Me.PanelControl5.Controls.Add(Me.lblTNivel)
        Me.PanelControl5.Location = New System.Drawing.Point(5, 89)
        Me.PanelControl5.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PanelControl5.Name = "PanelControl5"
        Me.PanelControl5.Size = New System.Drawing.Size(577, 148)
        Me.PanelControl5.TabIndex = 3
        '
        'lblDisponible
        '
        Me.lblDisponible.AutoSize = True
        Me.lblDisponible.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDisponible.Location = New System.Drawing.Point(241, 122)
        Me.lblDisponible.Name = "lblDisponible"
        Me.lblDisponible.Size = New System.Drawing.Size(17, 17)
        Me.lblDisponible.TabIndex = 14
        Me.lblDisponible.Text = "0"
        '
        'lblTDisponible
        '
        Me.lblTDisponible.AutoSize = True
        Me.lblTDisponible.Location = New System.Drawing.Point(166, 122)
        Me.lblTDisponible.Name = "lblTDisponible"
        Me.lblTDisponible.Size = New System.Drawing.Size(65, 16)
        Me.lblTDisponible.TabIndex = 13
        Me.lblTDisponible.Text = "Disponible"
        '
        'chkActivo
        '
        Me.chkActivo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkActivo.Enabled = False
        Me.chkActivo.Location = New System.Drawing.Point(359, 118)
        Me.chkActivo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkActivo.Name = "chkActivo"
        Me.chkActivo.Size = New System.Drawing.Size(152, 21)
        Me.chkActivo.TabIndex = 15
        Me.chkActivo.Text = "Activo"
        Me.chkActivo.UseVisualStyleBackColor = True
        '
        'chkBloqueado
        '
        Me.chkBloqueado.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkBloqueado.Enabled = False
        Me.chkBloqueado.Location = New System.Drawing.Point(359, 87)
        Me.chkBloqueado.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkBloqueado.Name = "chkBloqueado"
        Me.chkBloqueado.Size = New System.Drawing.Size(152, 21)
        Me.chkBloqueado.TabIndex = 8
        Me.chkBloqueado.Text = "Bloqueado"
        Me.chkBloqueado.UseVisualStyleBackColor = True
        '
        'chkDañado
        '
        Me.chkDañado.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkDañado.Enabled = False
        Me.chkDañado.Location = New System.Drawing.Point(359, 60)
        Me.chkDañado.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkDañado.Name = "chkDañado"
        Me.chkDañado.Size = New System.Drawing.Size(152, 21)
        Me.chkDañado.TabIndex = 7
        Me.chkDañado.Text = "Dañado"
        Me.chkDañado.UseVisualStyleBackColor = True
        '
        'chkAceptaPallet
        '
        Me.chkAceptaPallet.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkAceptaPallet.Enabled = False
        Me.chkAceptaPallet.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAceptaPallet.Location = New System.Drawing.Point(359, 17)
        Me.chkAceptaPallet.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkAceptaPallet.Name = "chkAceptaPallet"
        Me.chkAceptaPallet.Size = New System.Drawing.Size(152, 21)
        Me.chkAceptaPallet.TabIndex = 2
        Me.chkAceptaPallet.Text = "Acepta palet"
        Me.chkAceptaPallet.UseVisualStyleBackColor = True
        '
        'lblVolumen
        '
        Me.lblVolumen.AutoSize = True
        Me.lblVolumen.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVolumen.Location = New System.Drawing.Point(80, 122)
        Me.lblVolumen.Name = "lblVolumen"
        Me.lblVolumen.Size = New System.Drawing.Size(17, 17)
        Me.lblVolumen.TabIndex = 12
        Me.lblVolumen.Text = "0"
        '
        'lblAlto
        '
        Me.lblAlto.AutoSize = True
        Me.lblAlto.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAlto.Location = New System.Drawing.Point(80, 94)
        Me.lblAlto.Name = "lblAlto"
        Me.lblAlto.Size = New System.Drawing.Size(17, 17)
        Me.lblAlto.TabIndex = 10
        Me.lblAlto.Text = "0"
        '
        'lblLargo
        '
        Me.lblLargo.AutoSize = True
        Me.lblLargo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLargo.Location = New System.Drawing.Point(80, 65)
        Me.lblLargo.Name = "lblLargo"
        Me.lblLargo.Size = New System.Drawing.Size(17, 17)
        Me.lblLargo.TabIndex = 6
        Me.lblLargo.Text = "0"
        '
        'lblAncho
        '
        Me.lblAncho.AutoSize = True
        Me.lblAncho.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAncho.Location = New System.Drawing.Point(80, 38)
        Me.lblAncho.Name = "lblAncho"
        Me.lblAncho.Size = New System.Drawing.Size(17, 17)
        Me.lblAncho.TabIndex = 4
        Me.lblAncho.Text = "0"
        '
        'lblNivel
        '
        Me.lblNivel.AutoSize = True
        Me.lblNivel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNivel.Location = New System.Drawing.Point(80, 14)
        Me.lblNivel.Name = "lblNivel"
        Me.lblNivel.Size = New System.Drawing.Size(17, 17)
        Me.lblNivel.TabIndex = 1
        Me.lblNivel.Text = "0"
        '
        'lblTVolumen
        '
        Me.lblTVolumen.AutoSize = True
        Me.lblTVolumen.Location = New System.Drawing.Point(14, 122)
        Me.lblTVolumen.Name = "lblTVolumen"
        Me.lblTVolumen.Size = New System.Drawing.Size(57, 16)
        Me.lblTVolumen.TabIndex = 11
        Me.lblTVolumen.Text = "Volumen"
        '
        'lblTAlto
        '
        Me.lblTAlto.AutoSize = True
        Me.lblTAlto.Location = New System.Drawing.Point(13, 94)
        Me.lblTAlto.Name = "lblTAlto"
        Me.lblTAlto.Size = New System.Drawing.Size(29, 16)
        Me.lblTAlto.TabIndex = 9
        Me.lblTAlto.Text = "Alto"
        '
        'lblTLargo
        '
        Me.lblTLargo.AutoSize = True
        Me.lblTLargo.Location = New System.Drawing.Point(14, 65)
        Me.lblTLargo.Name = "lblTLargo"
        Me.lblTLargo.Size = New System.Drawing.Size(39, 16)
        Me.lblTLargo.TabIndex = 5
        Me.lblTLargo.Text = "Largo"
        '
        'lblTAncho
        '
        Me.lblTAncho.AutoSize = True
        Me.lblTAncho.Location = New System.Drawing.Point(13, 38)
        Me.lblTAncho.Name = "lblTAncho"
        Me.lblTAncho.Size = New System.Drawing.Size(42, 16)
        Me.lblTAncho.TabIndex = 3
        Me.lblTAncho.Text = "Ancho"
        '
        'lblTNivel
        '
        Me.lblTNivel.AutoSize = True
        Me.lblTNivel.Location = New System.Drawing.Point(13, 14)
        Me.lblTNivel.Name = "lblTNivel"
        Me.lblTNivel.Size = New System.Drawing.Size(34, 16)
        Me.lblTNivel.TabIndex = 0
        Me.lblTNivel.Text = "Nivel"
        '
        'lblUbicacion
        '
        Me.lblUbicacion.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblUbicacion.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUbicacion.Location = New System.Drawing.Point(444, 7)
        Me.lblUbicacion.Name = "lblUbicacion"
        Me.lblUbicacion.Size = New System.Drawing.Size(138, 27)
        Me.lblUbicacion.TabIndex = 1
        Me.lblUbicacion.Text = "Ubicacion"
        Me.lblUbicacion.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'PanelControl4
        '
        Me.PanelControl4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelControl4.Controls.Add(Me.lblVolumenDisponible)
        Me.PanelControl4.Controls.Add(Me.lblVolumenDispTitulo)
        Me.PanelControl4.Controls.Add(Me.lblDisponibleAlmacenaje)
        Me.PanelControl4.Controls.Add(Me.lblDisponibleAlmacenajeTitulo)
        Me.PanelControl4.Location = New System.Drawing.Point(5, 43)
        Me.PanelControl4.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PanelControl4.Name = "PanelControl4"
        Me.PanelControl4.Size = New System.Drawing.Size(577, 41)
        Me.PanelControl4.TabIndex = 2
        '
        'lblVolumenDisponible
        '
        Me.lblVolumenDisponible.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblVolumenDisponible.Location = New System.Drawing.Point(434, 9)
        Me.lblVolumenDisponible.Name = "lblVolumenDisponible"
        Me.lblVolumenDisponible.Size = New System.Drawing.Size(87, 23)
        Me.lblVolumenDisponible.TabIndex = 3
        Me.lblVolumenDisponible.Text = "0"
        Me.lblVolumenDisponible.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblVolumenDispTitulo
        '
        Me.lblVolumenDispTitulo.AutoSize = True
        Me.lblVolumenDispTitulo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVolumenDispTitulo.Location = New System.Drawing.Point(306, 11)
        Me.lblVolumenDispTitulo.Name = "lblVolumenDispTitulo"
        Me.lblVolumenDispTitulo.Size = New System.Drawing.Size(133, 17)
        Me.lblVolumenDispTitulo.TabIndex = 2
        Me.lblVolumenDispTitulo.Text = "Volumen disponible :"
        '
        'lblDisponibleAlmacenaje
        '
        Me.lblDisponibleAlmacenaje.AutoSize = True
        Me.lblDisponibleAlmacenaje.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblDisponibleAlmacenaje.Location = New System.Drawing.Point(159, 9)
        Me.lblDisponibleAlmacenaje.Name = "lblDisponibleAlmacenaje"
        Me.lblDisponibleAlmacenaje.Size = New System.Drawing.Size(23, 24)
        Me.lblDisponibleAlmacenaje.TabIndex = 1
        Me.lblDisponibleAlmacenaje.Text = "0"
        '
        'lblDisponibleAlmacenajeTitulo
        '
        Me.lblDisponibleAlmacenajeTitulo.AutoSize = True
        Me.lblDisponibleAlmacenajeTitulo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDisponibleAlmacenajeTitulo.Location = New System.Drawing.Point(13, 11)
        Me.lblDisponibleAlmacenajeTitulo.Name = "lblDisponibleAlmacenajeTitulo"
        Me.lblDisponibleAlmacenajeTitulo.Size = New System.Drawing.Size(150, 17)
        Me.lblDisponibleAlmacenajeTitulo.TabIndex = 0
        Me.lblDisponibleAlmacenajeTitulo.Text = "Disponible almacenaje :"
        '
        'dgridStockUbic
        '
        Me.dgridStockUbic.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgridStockUbic.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgridStockUbic.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        GridLevelNode1.RelationName = "Level1"
        Me.dgridStockUbic.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.dgridStockUbic.Location = New System.Drawing.Point(2, 242)
        Me.dgridStockUbic.MainView = Me.GridView1
        Me.dgridStockUbic.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dgridStockUbic.MenuManager = Me.RibbonControl
        Me.dgridStockUbic.Name = "dgridStockUbic"
        Me.dgridStockUbic.Size = New System.Drawing.Size(582, 503)
        Me.dgridStockUbic.TabIndex = 4
        Me.dgridStockUbic.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1, Me.GridView6})
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.dgridStockUbic
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView1.OptionsCustomization.AllowRowSizing = True
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsSelection.EnableAppearanceHideSelection = False
        Me.GridView1.OptionsView.ColumnAutoWidth = False
        Me.GridView1.OptionsView.ShowFooter = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'GridView6
        '
        Me.GridView6.GridControl = Me.dgridStockUbic
        Me.GridView6.Name = "GridView6"
        '
        'lblUbicacionTitulo
        '
        Me.lblUbicacionTitulo.AutoSize = True
        Me.lblUbicacionTitulo.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUbicacionTitulo.Location = New System.Drawing.Point(7, 10)
        Me.lblUbicacionTitulo.Name = "lblUbicacionTitulo"
        Me.lblUbicacionTitulo.Size = New System.Drawing.Size(109, 24)
        Me.lblUbicacionTitulo.TabIndex = 0
        Me.lblUbicacionTitulo.Text = "Ubicación"
        '
        'GridView3
        '
        Me.GridView3.Name = "GridView3"
        '
        'dgridUbicacionesSugeridas
        '
        Me.dgridUbicacionesSugeridas.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgridUbicacionesSugeridas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridUbicacionesSugeridas.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        GridLevelNode2.RelationName = "Level1"
        Me.dgridUbicacionesSugeridas.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode2})
        Me.dgridUbicacionesSugeridas.Location = New System.Drawing.Point(0, 0)
        Me.dgridUbicacionesSugeridas.MainView = Me.GridViewDet
        Me.dgridUbicacionesSugeridas.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dgridUbicacionesSugeridas.MenuManager = Me.RibbonControl
        Me.dgridUbicacionesSugeridas.Name = "dgridUbicacionesSugeridas"
        Me.dgridUbicacionesSugeridas.Size = New System.Drawing.Size(318, 670)
        Me.dgridUbicacionesSugeridas.TabIndex = 0
        Me.dgridUbicacionesSugeridas.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewDet, Me.GridView4})
        '
        'GridViewDet
        '
        Me.GridViewDet.GridControl = Me.dgridUbicacionesSugeridas
        Me.GridViewDet.Name = "GridViewDet"
        Me.GridViewDet.OptionsBehavior.Editable = False
        Me.GridViewDet.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridViewDet.OptionsSelection.EnableAppearanceHideSelection = False
        Me.GridViewDet.OptionsView.ShowGroupPanel = False
        '
        'GridView4
        '
        Me.GridView4.GridControl = Me.dgridUbicacionesSugeridas
        Me.GridView4.Name = "GridView4"
        '
        'PanelControl1
        '
        Me.PanelControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelControl1.Controls.Add(Me.PanelControl6)
        Me.PanelControl1.Controls.Add(Me.DgridDetalleUbics)
        Me.PanelControl1.Controls.Add(Me.ToolStripP)
        Me.PanelControl1.Location = New System.Drawing.Point(925, 4)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(363, 700)
        Me.PanelControl1.TabIndex = 2
        '
        'PanelControl6
        '
        Me.PanelControl6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelControl6.Controls.Add(Me.lblTFalta)
        Me.PanelControl6.Controls.Add(Me.lblAlmacenado)
        Me.PanelControl6.Controls.Add(Me.lblFalta)
        Me.PanelControl6.Controls.Add(Me.lblTAlmacenado)
        Me.PanelControl6.Location = New System.Drawing.Point(2, 618)
        Me.PanelControl6.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PanelControl6.Name = "PanelControl6"
        Me.PanelControl6.Size = New System.Drawing.Size(358, 80)
        Me.PanelControl6.TabIndex = 2
        '
        'lblTFalta
        '
        Me.lblTFalta.AutoSize = True
        Me.lblTFalta.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTFalta.Location = New System.Drawing.Point(112, 43)
        Me.lblTFalta.Name = "lblTFalta"
        Me.lblTFalta.Size = New System.Drawing.Size(67, 24)
        Me.lblTFalta.TabIndex = 2
        Me.lblTFalta.Text = "Falta :"
        '
        'lblAlmacenado
        '
        Me.lblAlmacenado.AutoSize = True
        Me.lblAlmacenado.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblAlmacenado.Location = New System.Drawing.Point(181, 9)
        Me.lblAlmacenado.Name = "lblAlmacenado"
        Me.lblAlmacenado.Size = New System.Drawing.Size(90, 24)
        Me.lblAlmacenado.TabIndex = 1
        Me.lblAlmacenado.Text = "Label19"
        '
        'lblFalta
        '
        Me.lblFalta.AutoSize = True
        Me.lblFalta.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblFalta.Location = New System.Drawing.Point(181, 44)
        Me.lblFalta.Name = "lblFalta"
        Me.lblFalta.Size = New System.Drawing.Size(90, 24)
        Me.lblFalta.TabIndex = 3
        Me.lblFalta.Text = "Label21"
        '
        'lblTAlmacenado
        '
        Me.lblTAlmacenado.AutoSize = True
        Me.lblTAlmacenado.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTAlmacenado.Location = New System.Drawing.Point(49, 9)
        Me.lblTAlmacenado.Name = "lblTAlmacenado"
        Me.lblTAlmacenado.Size = New System.Drawing.Size(132, 24)
        Me.lblTAlmacenado.TabIndex = 0
        Me.lblTAlmacenado.Text = "Almacenado :"
        '
        'DgridDetalleUbics
        '
        Me.DgridDetalleUbics.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DgridDetalleUbics.Cursor = System.Windows.Forms.Cursors.Default
        Me.DgridDetalleUbics.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DgridDetalleUbics.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode2})
        Me.DgridDetalleUbics.Location = New System.Drawing.Point(2, 33)
        Me.DgridDetalleUbics.MainView = Me.GridUbic
        Me.DgridDetalleUbics.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DgridDetalleUbics.MenuManager = Me.RibbonControl
        Me.DgridDetalleUbics.Name = "DgridDetalleUbics"
        Me.DgridDetalleUbics.Size = New System.Drawing.Size(358, 571)
        Me.DgridDetalleUbics.TabIndex = 1
        Me.DgridDetalleUbics.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridUbic, Me.GridView5})
        '
        'GridUbic
        '
        Me.GridUbic.GridControl = Me.DgridDetalleUbics
        Me.GridUbic.Name = "GridUbic"
        Me.GridUbic.OptionsBehavior.Editable = False
        Me.GridUbic.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridUbic.OptionsSelection.EnableAppearanceHideSelection = False
        Me.GridUbic.OptionsView.ColumnAutoWidth = False
        Me.GridUbic.OptionsView.ShowGroupPanel = False
        '
        'GridView5
        '
        Me.GridView5.GridControl = Me.DgridDetalleUbics
        Me.GridView5.Name = "GridView5"
        '
        'ToolStripP
        '
        Me.ToolStripP.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStripP.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdDesactivarParametro})
        Me.ToolStripP.Location = New System.Drawing.Point(2, 2)
        Me.ToolStripP.Name = "ToolStripP"
        Me.ToolStripP.Size = New System.Drawing.Size(359, 27)
        Me.ToolStripP.TabIndex = 0
        Me.ToolStripP.Text = "ToolStrip1"
        '
        'cmdDesactivarParametro
        '
        Me.cmdDesactivarParametro.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdDesactivarParametro.Name = "cmdDesactivarParametro"
        Me.cmdDesactivarParametro.Size = New System.Drawing.Size(67, 24)
        Me.cmdDesactivarParametro.Text = "Eliminar"
        '
        'PanelControl3
        '
        Me.PanelControl3.Controls.Add(Me.lblProducto)
        Me.PanelControl3.Location = New System.Drawing.Point(329, 155)
        Me.PanelControl3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PanelControl3.Name = "PanelControl3"
        Me.PanelControl3.Size = New System.Drawing.Size(938, 38)
        Me.PanelControl3.TabIndex = 0
        '
        'lblProducto
        '
        Me.lblProducto.AutoSize = True
        Me.lblProducto.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProducto.Location = New System.Drawing.Point(13, 10)
        Me.lblProducto.Name = "lblProducto"
        Me.lblProducto.Size = New System.Drawing.Size(136, 24)
        Me.lblProducto.TabIndex = 0
        Me.lblProducto.Text = "Producto - A"
        '
        'xtrBodegaSelUbic
        '
        Me.xtrBodegaSelUbic.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtrBodegaSelUbic.Location = New System.Drawing.Point(3, 4)
        Me.xtrBodegaSelUbic.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.xtrBodegaSelUbic.Name = "xtrBodegaSelUbic"
        Me.xtrBodegaSelUbic.SelectedTabPage = Me.UbicSug
        Me.xtrBodegaSelUbic.Size = New System.Drawing.Size(320, 700)
        Me.xtrBodegaSelUbic.TabIndex = 0
        Me.xtrBodegaSelUbic.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.UbicSug, Me.TodasUbicaciones})
        '
        'UbicSug
        '
        Me.UbicSug.Controls.Add(Me.dgridUbicacionesSugeridas)
        Me.UbicSug.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.UbicSug.Name = "UbicSug"
        Me.UbicSug.Size = New System.Drawing.Size(318, 670)
        Me.UbicSug.Text = "Ubicaciones sugeridas"
        '
        'TodasUbicaciones
        '
        Me.TodasUbicaciones.Controls.Add(Me.GroupControl1)
        Me.TodasUbicaciones.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TodasUbicaciones.Name = "TodasUbicaciones"
        Me.TodasUbicaciones.Size = New System.Drawing.Size(318, 670)
        Me.TodasUbicaciones.Text = "Todas las ubicaciones"
        '
        'TextEdit1
        '
        Me.TextEdit1.Location = New System.Drawing.Point(32738, 32710)
        Me.TextEdit1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TextEdit1.MenuManager = Me.RibbonControl
        Me.TextEdit1.Name = "TextEdit1"
        Me.TextEdit1.Size = New System.Drawing.Size(117, 22)
        Me.TextEdit1.TabIndex = 3
        '
        'StatusStrip1
        '
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 901)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(1, 0, 16, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(1291, 22)
        Me.StatusStrip1.TabIndex = 2
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.4536!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64.5464!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 370.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.xtrBodegaSelUbic, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.PanelControl2, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.PanelControl1, 2, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 193)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1291, 708)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'frmBodegaSelUbic
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1291, 923)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.TextEdit1)
        Me.Controls.Add(Me.PanelControl3)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.RibbonControl)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmBodegaSelUbic"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Bodega Ubicacion Selección"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsBodega, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BodegaBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tlUbicacionesTodas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.txtFiltroUbic.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        Me.PanelControl2.PerformLayout()
        CType(Me.PanelControl5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl5.ResumeLayout(False)
        Me.PanelControl5.PerformLayout()
        CType(Me.PanelControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl4.ResumeLayout(False)
        Me.PanelControl4.PerformLayout()
        CType(Me.dgridStockUbic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgridUbicacionesSugeridas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewDet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.PanelControl6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl6.ResumeLayout(False)
        Me.PanelControl6.PerformLayout()
        CType(Me.DgridDetalleUbics, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridUbic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStripP.ResumeLayout(False)
        Me.ToolStripP.PerformLayout()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl3.ResumeLayout(False)
        Me.PanelControl3.PerformLayout()
        CType(Me.xtrBodegaSelUbic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtrBodegaSelUbic.ResumeLayout(False)
        Me.UbicSug.ResumeLayout(False)
        Me.TodasUbicaciones.ResumeLayout(False)
        CType(Me.TextEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout

End Sub

    Friend WithEvents RibbonPageGroup3 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents chkActivos As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents mmuActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuSalir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdImportarExcel As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lblRegs As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents BarEditItem2 As DevExpress.XtraBars.BarEditItem
    Friend WithEvents mnuNuevo As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarListItem1 As DevExpress.XtraBars.BarListItem
    Friend WithEvents BarSubItem2 As DevExpress.XtraBars.BarSubItem
    Friend WithEvents RibbonGalleryBarItem1 As DevExpress.XtraBars.RibbonGalleryBarItem
    Friend WithEvents BarEditItem1 As DevExpress.XtraBars.BarEditItem
    Friend WithEvents BarSubItem1 As DevExpress.XtraBars.BarSubItem
    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents DsBodega As DsBodega
    Friend WithEvents BodegaBindingSource As BindingSource
    Friend WithEvents tlUbicacionesTodas As DevExpress.XtraTreeList.TreeList
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ToolStripP As ToolStrip
    Friend WithEvents cmdDesactivarParametro As ToolStripButton
    Friend WithEvents PanelControl3 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents dgridUbicacionesSugeridas As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewDet As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView4 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lblProducto As Label
    Friend WithEvents DgridDetalleUbics As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridUbic As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView5 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lblUbicacionTitulo As Label
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuAplicar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lblUbicacion As Label
    Friend WithEvents PanelControl4 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblDisponibleAlmacenajeTitulo As Label
    Friend WithEvents dgridStockUbic As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView6 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents PanelControl5 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblTNivel As Label
    Friend WithEvents lblVolumenDisponible As Label
    Friend WithEvents lblVolumenDispTitulo As Label
    Friend WithEvents lblDisponibleAlmacenaje As Label
    Friend WithEvents lblTAlto As Label
    Friend WithEvents lblTLargo As Label
    Friend WithEvents lblTAncho As Label
    Friend WithEvents lblVolumen As Label
    Friend WithEvents lblAlto As Label
    Friend WithEvents lblLargo As Label
    Friend WithEvents lblAncho As Label
    Friend WithEvents lblNivel As Label
    Friend WithEvents lblTVolumen As Label
    Friend WithEvents lblDisponible As Label
    Friend WithEvents lblTDisponible As Label
    Friend WithEvents chkActivo As CheckBox
    Friend WithEvents chkBloqueado As CheckBox
    Friend WithEvents chkDañado As CheckBox
    Friend WithEvents chkAceptaPallet As CheckBox
    Friend WithEvents PanelControl6 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblTFalta As Label
    Friend WithEvents lblAlmacenado As Label
    Friend WithEvents lblFalta As Label
    Friend WithEvents lblTAlmacenado As Label
    Friend WithEvents xtrBodegaSelUbic As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents UbicSug As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents TodasUbicaciones As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents TextEdit1 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnFiltLimpia As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtFiltroUbic As DevExpress.XtraEditors.TextEdit
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
End Class
