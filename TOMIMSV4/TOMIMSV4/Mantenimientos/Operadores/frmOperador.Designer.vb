<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmOperador
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If components IsNot Nothing Then
                components.Dispose()
            End If
            If DT IsNot Nothing Then
                DT.Dispose()
                DT = Nothing
            End If
            If BeOperador IsNot Nothing Then
                BeOperador.Dispose()
                BeOperador = Nothing
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
        Dim IdEmpresaLabel As System.Windows.Forms.Label
        Dim NombreLabel As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim Label5 As System.Windows.Forms.Label
        Dim Label6 As System.Windows.Forms.Label
        Dim Label7 As System.Windows.Forms.Label
        Dim Label9 As System.Windows.Forms.Label
        Dim Fec_agrLabel As System.Windows.Forms.Label
        Dim User_agrLabel As System.Windows.Forms.Label
        Dim Fec_modLabel As System.Windows.Forms.Label
        Dim User_modLabel As System.Windows.Forms.Label
        Dim Label11 As System.Windows.Forms.Label
        Dim lblchkUbica As System.Windows.Forms.Label
        Dim Label13 As System.Windows.Forms.Label
        Dim lblchkPickea As System.Windows.Forms.Label
        Dim lblchkVerifica As System.Windows.Forms.Label
        Dim Label12 As System.Windows.Forms.Label
        Dim Label14 As System.Windows.Forms.Label
        Dim lblCorrelativoActual As System.Windows.Forms.Label
        Dim lblCorrelativoFinal As System.Windows.Forms.Label
        Dim lblBodega As System.Windows.Forms.Label
        Dim ActivoLabel As System.Windows.Forms.Label
        Dim lblCorrelativoInicial As System.Windows.Forms.Label
        Dim CodigoLabel As System.Windows.Forms.Label
        Dim lblSerie As System.Windows.Forms.Label
        Dim Label24 As System.Windows.Forms.Label
        Dim lblZonaPicking As System.Windows.Forms.Label
        Dim lblMontacarga As System.Windows.Forms.Label
        Dim Label15 As System.Windows.Forms.Label
        Dim Label17 As System.Windows.Forms.Label
        Dim Label8 As System.Windows.Forms.Label
        Dim Label10 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOperador))
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim GridLevelNode2 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim GridLevelNode3 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim GridLevelNode4 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImprimeCarnet = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuCapturarFoto = New DevExpress.XtraBars.BarButtonItem()
        Me.chkActivo = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.chkUsaHH = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.chkSistema = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.PanDatosMotivoAnulacion = New DevExpress.XtraEditors.GroupControl()
        Me.dtpUltimaSesion = New System.Windows.Forms.DateTimePicker()
        Me.picEstadoOp = New DevExpress.XtraEditors.PictureEdit()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.chkMontacarga = New DevExpress.XtraEditors.CheckEdit()
        Me.chkVerifica = New DevExpress.XtraEditors.CheckEdit()
        Me.chkPickea = New DevExpress.XtraEditors.CheckEdit()
        Me.chkTransporta = New DevExpress.XtraEditors.CheckEdit()
        Me.chkUbica = New DevExpress.XtraEditors.CheckEdit()
        Me.chkRecibe = New DevExpress.XtraEditors.CheckEdit()
        Me.CameraControl1 = New DevExpress.XtraEditors.Camera.CameraControl()
        Me.cmbJornada = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbEmpresa = New DevExpress.XtraEditors.LookUpEdit()
        Me.btnExaminar = New DevExpress.XtraEditors.SimpleButton()
        Me.picFoto = New DevExpress.XtraEditors.PictureEdit()
        Me.lnkRolOperador = New System.Windows.Forms.LinkLabel()
        Me.txtIdRolOperador = New DevExpress.XtraEditors.TextEdit()
        Me.txtNombreOperador = New DevExpress.XtraEditors.TextEdit()
        Me.txtCostoHora = New System.Windows.Forms.NumericUpDown()
        Me.txtClave = New DevExpress.XtraEditors.TextEdit()
        Me.txtCodigo = New DevExpress.XtraEditors.TextEdit()
        Me.txtTelefono = New DevExpress.XtraEditors.TextEdit()
        Me.txtDireccion = New DevExpress.XtraEditors.TextEdit()
        Me.txtApellidos = New DevExpress.XtraEditors.TextEdit()
        Me.lblCodigo = New System.Windows.Forms.Label()
        Me.txtNombres = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_agrDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_agrTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_modDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_modTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.GrpEmpresaTB = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.Grid = New DevExpress.XtraGrid.GridControl()
        Me.DataBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsOperador = New DsOperador()
        Me.gridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colSeleccion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.colIdBodega = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IdOperadorBodega = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colBodega = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IdInterno = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.dkOperador = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.TabDatos = New DevExpress.XtraTab.XtraTabControl()
        Me.XtraTabPage1 = New DevExpress.XtraTab.XtraTabPage()
        Me.TabOperadorBodega = New DevExpress.XtraTab.XtraTabPage()
        Me.ResolucionesLP = New DevExpress.XtraTab.XtraTabPage()
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.ToolStripPR = New System.Windows.Forms.ToolStrip()
        Me.cmdNewPR = New System.Windows.Forms.ToolStripButton()
        Me.cmdSavePR = New System.Windows.Forms.ToolStripButton()
        Me.cmdDesactivarResolucion = New System.Windows.Forms.ToolStripButton()
        Me.txtCorrelativoActual = New System.Windows.Forms.NumericUpDown()
        Me.txtCorrelativoFinal = New System.Windows.Forms.NumericUpDown()
        Me.cmbBodega = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblIdResolucionLP = New System.Windows.Forms.Label()
        Me.chkResolucionLPActiva = New DevExpress.XtraEditors.CheckEdit()
        Me.txtCorrelativoInicial = New System.Windows.Forms.NumericUpDown()
        Me.txtNoSerie = New DevExpress.XtraEditors.TextEdit()
        Me.GroupControl5 = New DevExpress.XtraEditors.GroupControl()
        Me.dGridPresentacion = New DevExpress.XtraGrid.GridControl()
        Me.GrdPresentacion = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView6 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.chkActivoPR = New DevExpress.XtraEditors.CheckEdit()
        Me.XtraTabPage2 = New DevExpress.XtraTab.XtraTabPage()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupControl4 = New DevExpress.XtraEditors.GroupControl()
        Me.dgridTramosZonaPicking = New DevExpress.XtraGrid.GridControl()
        Me.gvTramosPorZona = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.lblIdZonaPickingTramoOp = New DevExpress.XtraEditors.LabelControl()
        Me.cmbZonaPicking = New DevExpress.XtraEditors.LookUpEdit()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.mnuNuevoTramo = New System.Windows.Forms.ToolStripButton()
        Me.mnuGuardarTramo = New System.Windows.Forms.ToolStripButton()
        Me.mnuEliminarTramo = New System.Windows.Forms.ToolStripButton()
        Me.chkLunes = New System.Windows.Forms.CheckBox()
        Me.chkMartes = New System.Windows.Forms.CheckBox()
        Me.chkMiercoles = New System.Windows.Forms.CheckBox()
        Me.chkDomingo = New System.Windows.Forms.CheckBox()
        Me.chkJueves = New System.Windows.Forms.CheckBox()
        Me.chkViernes = New System.Windows.Forms.CheckBox()
        Me.chkSabado = New System.Windows.Forms.CheckBox()
        Me.GroupControl6 = New DevExpress.XtraEditors.GroupControl()
        Me.DgridZonaPickingOperador = New DevExpress.XtraGrid.GridControl()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.xTabJornadaLaboral = New DevExpress.XtraTab.XtraTabPage()
        Me.SplitContainerControl2 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.GroupControl7 = New DevExpress.XtraEditors.GroupControl()
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
        Me.tbGuardar = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton()
        Me.cmbJornadaLaboral = New DevExpress.XtraEditors.LookUpEdit()
        Me.CheckEdit1 = New DevExpress.XtraEditors.CheckEdit()
        Me.GroupControl8 = New DevExpress.XtraEditors.GroupControl()
        Me.gdListaJornadaLaboral = New DevExpress.XtraGrid.GridControl()
        Me.GridView4 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView5 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.CheckEdit2 = New DevExpress.XtraEditors.CheckEdit()
        Me.ImageCollection1 = New DevExpress.Utils.ImageCollection(Me.components)
        IdEmpresaLabel = New System.Windows.Forms.Label()
        NombreLabel = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        Label5 = New System.Windows.Forms.Label()
        Label6 = New System.Windows.Forms.Label()
        Label7 = New System.Windows.Forms.Label()
        Label9 = New System.Windows.Forms.Label()
        Fec_agrLabel = New System.Windows.Forms.Label()
        User_agrLabel = New System.Windows.Forms.Label()
        Fec_modLabel = New System.Windows.Forms.Label()
        User_modLabel = New System.Windows.Forms.Label()
        Label11 = New System.Windows.Forms.Label()
        lblchkUbica = New System.Windows.Forms.Label()
        Label13 = New System.Windows.Forms.Label()
        lblchkPickea = New System.Windows.Forms.Label()
        lblchkVerifica = New System.Windows.Forms.Label()
        Label12 = New System.Windows.Forms.Label()
        Label14 = New System.Windows.Forms.Label()
        lblCorrelativoActual = New System.Windows.Forms.Label()
        lblCorrelativoFinal = New System.Windows.Forms.Label()
        lblBodega = New System.Windows.Forms.Label()
        ActivoLabel = New System.Windows.Forms.Label()
        lblCorrelativoInicial = New System.Windows.Forms.Label()
        CodigoLabel = New System.Windows.Forms.Label()
        lblSerie = New System.Windows.Forms.Label()
        Label24 = New System.Windows.Forms.Label()
        lblZonaPicking = New System.Windows.Forms.Label()
        lblMontacarga = New System.Windows.Forms.Label()
        Label15 = New System.Windows.Forms.Label()
        Label17 = New System.Windows.Forms.Label()
        Label8 = New System.Windows.Forms.Label()
        Label10 = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanDatosMotivoAnulacion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanDatosMotivoAnulacion.SuspendLayout()
        CType(Me.picEstadoOp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.chkMontacarga.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkVerifica.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPickea.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkTransporta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkUbica.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRecibe.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbJornada.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbEmpresa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picFoto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdRolOperador.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreOperador.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCostoHora, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtClave.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodigo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTelefono.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDireccion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtApellidos.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombres.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpEmpresaTB, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpEmpresaTB.SuspendLayout()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsOperador, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dkOperador, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerBottom.SuspendLayout()
        Me.DockPanel1.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.TabDatos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabDatos.SuspendLayout()
        Me.XtraTabPage1.SuspendLayout()
        Me.TabOperadorBodega.SuspendLayout()
        Me.ResolucionesLP.SuspendLayout()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel1.SuspendLayout()
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel2.SuspendLayout()
        Me.SplitContainerControl1.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        Me.ToolStripPR.SuspendLayout()
        CType(Me.txtCorrelativoActual, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCorrelativoFinal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkResolucionLPActiva.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCorrelativoInicial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl5.SuspendLayout()
        CType(Me.dGridPresentacion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrdPresentacion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivoPR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabPage2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl4.SuspendLayout()
        CType(Me.dgridTramosZonaPicking, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvTramosPorZona, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbZonaPicking.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.GroupControl6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl6.SuspendLayout()
        CType(Me.DgridZonaPickingOperador, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xTabJornadaLaboral.SuspendLayout()
        CType(Me.SplitContainerControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl2.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl2.Panel1.SuspendLayout()
        CType(Me.SplitContainerControl2.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl2.Panel2.SuspendLayout()
        Me.SplitContainerControl2.SuspendLayout()
        CType(Me.GroupControl7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl7.SuspendLayout()
        Me.ToolStrip2.SuspendLayout()
        CType(Me.cmbJornadaLaboral.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CheckEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl8.SuspendLayout()
        CType(Me.gdListaJornadaLaboral, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CheckEdit2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImageCollection1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'IdEmpresaLabel
        '
        IdEmpresaLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        IdEmpresaLabel.AutoSize = True
        IdEmpresaLabel.Location = New System.Drawing.Point(26, 75)
        IdEmpresaLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        IdEmpresaLabel.Name = "IdEmpresaLabel"
        IdEmpresaLabel.Size = New System.Drawing.Size(62, 16)
        IdEmpresaLabel.TabIndex = 2
        IdEmpresaLabel.Text = "Empresa:"
        '
        'NombreLabel
        '
        NombreLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        NombreLabel.AutoSize = True
        NombreLabel.Location = New System.Drawing.Point(26, 170)
        NombreLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        NombreLabel.Name = "NombreLabel"
        NombreLabel.Size = New System.Drawing.Size(63, 16)
        NombreLabel.TabIndex = 9
        NombreLabel.Text = "Nombres:"
        '
        'Label1
        '
        Label1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(26, 44)
        Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(46, 16)
        Label1.TabIndex = 0
        Label1.Text = "Código"
        '
        'Label2
        '
        Label2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(26, 204)
        Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(63, 16)
        Label2.TabIndex = 11
        Label2.Text = "Apellidos:"
        '
        'Label3
        '
        Label3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label3.AutoSize = True
        Label3.Location = New System.Drawing.Point(26, 236)
        Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(64, 16)
        Label3.TabIndex = 13
        Label3.Text = "Dirección:"
        '
        'Label4
        '
        Label4.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(26, 270)
        Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(62, 16)
        Label4.TabIndex = 15
        Label4.Text = "Teléfono:"
        '
        'Label5
        '
        Label5.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label5.AutoSize = True
        Label5.Location = New System.Drawing.Point(26, 300)
        Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(51, 16)
        Label5.TabIndex = 17
        Label5.Text = "Código:"
        '
        'Label6
        '
        Label6.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label6.AutoSize = True
        Label6.Location = New System.Drawing.Point(26, 331)
        Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(43, 16)
        Label6.TabIndex = 19
        Label6.Text = "Clave:"
        '
        'Label7
        '
        Label7.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label7.AutoSize = True
        Label7.Location = New System.Drawing.Point(26, 361)
        Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label7.Name = "Label7"
        Label7.Size = New System.Drawing.Size(75, 16)
        Label7.TabIndex = 21
        Label7.Text = "Costo Hora:"
        '
        'Label9
        '
        Label9.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label9.AutoSize = True
        Label9.Location = New System.Drawing.Point(26, 108)
        Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label9.Name = "Label9"
        Label9.Size = New System.Drawing.Size(57, 16)
        Label9.TabIndex = 4
        Label9.Text = "Jornada:"
        '
        'Fec_agrLabel
        '
        Fec_agrLabel.AutoSize = True
        Fec_agrLabel.Location = New System.Drawing.Point(37, 39)
        Fec_agrLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Fec_agrLabel.Name = "Fec_agrLabel"
        Fec_agrLabel.Size = New System.Drawing.Size(91, 16)
        Fec_agrLabel.TabIndex = 4
        Fec_agrLabel.Text = "Fecha Agregó:"
        '
        'User_agrLabel
        '
        User_agrLabel.AutoSize = True
        User_agrLabel.Location = New System.Drawing.Point(37, 7)
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
        Fec_modLabel.Location = New System.Drawing.Point(513, 39)
        Fec_modLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Fec_modLabel.Name = "Fec_modLabel"
        Fec_modLabel.Size = New System.Drawing.Size(97, 16)
        Fec_modLabel.TabIndex = 6
        Fec_modLabel.Text = "Fecha Modificó:"
        '
        'User_modLabel
        '
        User_modLabel.AutoSize = True
        User_modLabel.Location = New System.Drawing.Point(513, 7)
        User_modLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        User_modLabel.Name = "User_modLabel"
        User_modLabel.Size = New System.Drawing.Size(106, 16)
        User_modLabel.TabIndex = 2
        User_modLabel.Text = "Usuario Modificó:"
        '
        'Label11
        '
        Label11.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label11.AutoSize = True
        Label11.Location = New System.Drawing.Point(74, 39)
        Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label11.Name = "Label11"
        Label11.Size = New System.Drawing.Size(50, 16)
        Label11.TabIndex = 32
        Label11.Text = "Recibe:"
        '
        'lblchkUbica
        '
        lblchkUbica.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblchkUbica.AutoSize = True
        lblchkUbica.Location = New System.Drawing.Point(74, 71)
        lblchkUbica.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblchkUbica.Name = "lblchkUbica"
        lblchkUbica.Size = New System.Drawing.Size(43, 16)
        lblchkUbica.TabIndex = 34
        lblchkUbica.Text = "Ubica:"
        '
        'Label13
        '
        Label13.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label13.AutoSize = True
        Label13.Location = New System.Drawing.Point(74, 105)
        Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label13.Name = "Label13"
        Label13.Size = New System.Drawing.Size(75, 16)
        Label13.TabIndex = 36
        Label13.Text = "Transporta:"
        '
        'lblchkPickea
        '
        lblchkPickea.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblchkPickea.AutoSize = True
        lblchkPickea.Location = New System.Drawing.Point(334, 39)
        lblchkPickea.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblchkPickea.Name = "lblchkPickea"
        lblchkPickea.Size = New System.Drawing.Size(48, 16)
        lblchkPickea.TabIndex = 38
        lblchkPickea.Text = "Pickea:"
        '
        'lblchkVerifica
        '
        lblchkVerifica.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblchkVerifica.AutoSize = True
        lblchkVerifica.Location = New System.Drawing.Point(334, 71)
        lblchkVerifica.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblchkVerifica.Name = "lblchkVerifica"
        lblchkVerifica.Size = New System.Drawing.Size(55, 16)
        lblchkVerifica.TabIndex = 40
        lblchkVerifica.Text = "Verifica:"
        '
        'Label12
        '
        Label12.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label12.AutoSize = True
        Label12.Location = New System.Drawing.Point(597, 50)
        Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label12.Name = "Label12"
        Label12.Size = New System.Drawing.Size(108, 16)
        Label12.TabIndex = 44
        Label12.Text = "Estado de sesión:"
        '
        'Label14
        '
        Label14.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label14.AutoSize = True
        Label14.Location = New System.Drawing.Point(597, 97)
        Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label14.Name = "Label14"
        Label14.Size = New System.Drawing.Size(88, 16)
        Label14.TabIndex = 45
        Label14.Text = "Última sesión:"
        '
        'lblCorrelativoActual
        '
        lblCorrelativoActual.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblCorrelativoActual.AutoSize = True
        lblCorrelativoActual.Location = New System.Drawing.Point(33, 249)
        lblCorrelativoActual.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblCorrelativoActual.Name = "lblCorrelativoActual"
        lblCorrelativoActual.Size = New System.Drawing.Size(113, 16)
        lblCorrelativoActual.TabIndex = 14
        lblCorrelativoActual.Text = "Correlativo Actual:"
        '
        'lblCorrelativoFinal
        '
        lblCorrelativoFinal.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblCorrelativoFinal.AutoSize = True
        lblCorrelativoFinal.Location = New System.Drawing.Point(33, 218)
        lblCorrelativoFinal.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblCorrelativoFinal.Name = "lblCorrelativoFinal"
        lblCorrelativoFinal.Size = New System.Drawing.Size(105, 16)
        lblCorrelativoFinal.TabIndex = 12
        lblCorrelativoFinal.Text = "Correlativo Final:"
        '
        'lblBodega
        '
        lblBodega.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblBodega.AutoSize = True
        lblBodega.ForeColor = System.Drawing.Color.Black
        lblBodega.Location = New System.Drawing.Point(33, 129)
        lblBodega.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblBodega.Name = "lblBodega"
        lblBodega.Size = New System.Drawing.Size(54, 16)
        lblBodega.TabIndex = 10
        lblBodega.Text = "Bodega:"
        '
        'ActivoLabel
        '
        ActivoLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        ActivoLabel.AutoSize = True
        ActivoLabel.Location = New System.Drawing.Point(451, 94)
        ActivoLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        ActivoLabel.Name = "ActivoLabel"
        ActivoLabel.Size = New System.Drawing.Size(46, 16)
        ActivoLabel.TabIndex = 6
        ActivoLabel.Text = "Activo:"
        '
        'lblCorrelativoInicial
        '
        lblCorrelativoInicial.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblCorrelativoInicial.AutoSize = True
        lblCorrelativoInicial.Location = New System.Drawing.Point(33, 187)
        lblCorrelativoInicial.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblCorrelativoInicial.Name = "lblCorrelativoInicial"
        lblCorrelativoInicial.Size = New System.Drawing.Size(111, 16)
        lblCorrelativoInicial.TabIndex = 4
        lblCorrelativoInicial.Text = "Correlativo Inicial:"
        '
        'CodigoLabel
        '
        CodigoLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        CodigoLabel.AutoSize = True
        CodigoLabel.Location = New System.Drawing.Point(33, 94)
        CodigoLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        CodigoLabel.Name = "CodigoLabel"
        CodigoLabel.Size = New System.Drawing.Size(51, 16)
        CodigoLabel.TabIndex = 0
        CodigoLabel.Text = "Código:"
        '
        'lblSerie
        '
        lblSerie.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblSerie.AutoSize = True
        lblSerie.Location = New System.Drawing.Point(33, 158)
        lblSerie.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblSerie.Name = "lblSerie"
        lblSerie.Size = New System.Drawing.Size(42, 16)
        lblSerie.TabIndex = 2
        lblSerie.Text = "Serie:"
        '
        'Label24
        '
        Label24.AutoSize = True
        Label24.Location = New System.Drawing.Point(304, 78)
        Label24.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label24.Name = "Label24"
        Label24.Size = New System.Drawing.Size(30, 16)
        Label24.TabIndex = 9
        Label24.Text = "Dia:"
        '
        'lblZonaPicking
        '
        lblZonaPicking.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblZonaPicking.AutoSize = True
        lblZonaPicking.ForeColor = System.Drawing.Color.Black
        lblZonaPicking.Location = New System.Drawing.Point(29, 85)
        lblZonaPicking.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblZonaPicking.Name = "lblZonaPicking"
        lblZonaPicking.Size = New System.Drawing.Size(101, 16)
        lblZonaPicking.TabIndex = 17
        lblZonaPicking.Text = "Zona de Picking:"
        '
        'lblMontacarga
        '
        lblMontacarga.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblMontacarga.AutoSize = True
        lblMontacarga.ForeColor = System.Drawing.Color.Firebrick
        lblMontacarga.Location = New System.Drawing.Point(334, 105)
        lblMontacarga.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblMontacarga.Name = "lblMontacarga"
        lblMontacarga.Size = New System.Drawing.Size(79, 16)
        lblMontacarga.TabIndex = 42
        lblMontacarga.Text = "Montacarga:"
        '
        'Label15
        '
        Label15.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label15.AutoSize = True
        Label15.ForeColor = System.Drawing.Color.Black
        Label15.Location = New System.Drawing.Point(33, 91)
        Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label15.Name = "Label15"
        Label15.Size = New System.Drawing.Size(103, 16)
        Label15.TabIndex = 10
        Label15.Text = "Jornada Laboral:"
        '
        'Label17
        '
        Label17.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label17.AutoSize = True
        Label17.Location = New System.Drawing.Point(451, 94)
        Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label17.Name = "Label17"
        Label17.Size = New System.Drawing.Size(46, 16)
        Label17.TabIndex = 6
        Label17.Text = "Activo:"
        '
        'Label8
        '
        Label8.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label8.AutoSize = True
        Label8.ForeColor = System.Drawing.Color.Red
        Label8.Location = New System.Drawing.Point(451, 218)
        Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label8.Name = "Label8"
        Label8.Size = New System.Drawing.Size(112, 16)
        Label8.TabIndex = 17
        Label8.Text = "*máximo 9 digitos"
        '
        'Label10
        '
        Label10.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label10.AutoSize = True
        Label10.ForeColor = System.Drawing.Color.Red
        Label10.Location = New System.Drawing.Point(451, 158)
        Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label10.Name = "Label10"
        Label10.Size = New System.Drawing.Size(135, 16)
        Label10.TabIndex = 18
        Label10.Text = "*máximo 3 caracteres"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuGuardar, Me.mnuActualizar, Me.mnuEliminar, Me.cmdImprimeCarnet, Me.mnuCapturarFoto, Me.chkActivo, Me.chkUsaHH, Me.chkSistema})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(4)
        Me.RibbonControl.MaxItemId = 10
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1338, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'mnuGuardar
        '
        Me.mnuGuardar.Caption = "&Guardar"
        Me.mnuGuardar.Id = 1
        Me.mnuGuardar.ImageOptions.SvgImage = CType(resources.GetObject("mnuGuardar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuGuardar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.G))
        Me.mnuGuardar.Name = "mnuGuardar"
        '
        'mnuActualizar
        '
        Me.mnuActualizar.Caption = "&Actualizar"
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
        'cmdImprimeCarnet
        '
        Me.cmdImprimeCarnet.Caption = "Imprimir Carnet"
        Me.cmdImprimeCarnet.Id = 5
        Me.cmdImprimeCarnet.ImageOptions.SvgImage = CType(resources.GetObject("cmdImprimeCarnet.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdImprimeCarnet.Name = "cmdImprimeCarnet"
        '
        'mnuCapturarFoto
        '
        Me.mnuCapturarFoto.Caption = "Capturar Foto"
        Me.mnuCapturarFoto.Id = 6
        Me.mnuCapturarFoto.ImageOptions.SvgImage = CType(resources.GetObject("mnuCapturarFoto.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuCapturarFoto.Name = "mnuCapturarFoto"
        '
        'chkActivo
        '
        Me.chkActivo.Caption = "Activo"
        Me.chkActivo.Id = 7
        Me.chkActivo.Name = "chkActivo"
        '
        'chkUsaHH
        '
        Me.chkUsaHH.Caption = "Usa HH"
        Me.chkUsaHH.Id = 8
        Me.chkUsaHH.Name = "chkUsaHH"
        '
        'chkSistema
        '
        Me.chkSistema.Caption = "Sistema"
        Me.chkSistema.Id = 9
        Me.chkSistema.Name = "chkSistema"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1, Me.RibbonPageGroup2})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Mantenimiento de operador"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardar, "G")
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizar, "A")
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEliminar, "E")
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdImprimeCarnet)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuCapturarFoto)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonPageGroup2
        '
        Me.RibbonPageGroup2.ItemLinks.Add(Me.chkActivo)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.chkUsaHH)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.chkSistema)
        Me.RibbonPageGroup2.Name = "RibbonPageGroup2"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 852)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1338, 30)
        '
        'PanDatosMotivoAnulacion
        '
        Me.PanDatosMotivoAnulacion.Controls.Add(Me.dtpUltimaSesion)
        Me.PanDatosMotivoAnulacion.Controls.Add(Label14)
        Me.PanDatosMotivoAnulacion.Controls.Add(Label12)
        Me.PanDatosMotivoAnulacion.Controls.Add(Me.picEstadoOp)
        Me.PanDatosMotivoAnulacion.Controls.Add(Me.GroupControl1)
        Me.PanDatosMotivoAnulacion.Controls.Add(Me.CameraControl1)
        Me.PanDatosMotivoAnulacion.Controls.Add(Me.cmbJornada)
        Me.PanDatosMotivoAnulacion.Controls.Add(Me.cmbEmpresa)
        Me.PanDatosMotivoAnulacion.Controls.Add(Me.btnExaminar)
        Me.PanDatosMotivoAnulacion.Controls.Add(Label9)
        Me.PanDatosMotivoAnulacion.Controls.Add(Me.picFoto)
        Me.PanDatosMotivoAnulacion.Controls.Add(Me.lnkRolOperador)
        Me.PanDatosMotivoAnulacion.Controls.Add(Me.txtIdRolOperador)
        Me.PanDatosMotivoAnulacion.Controls.Add(Me.txtNombreOperador)
        Me.PanDatosMotivoAnulacion.Controls.Add(Me.txtCostoHora)
        Me.PanDatosMotivoAnulacion.Controls.Add(Label7)
        Me.PanDatosMotivoAnulacion.Controls.Add(Label6)
        Me.PanDatosMotivoAnulacion.Controls.Add(Me.txtClave)
        Me.PanDatosMotivoAnulacion.Controls.Add(Label5)
        Me.PanDatosMotivoAnulacion.Controls.Add(Label4)
        Me.PanDatosMotivoAnulacion.Controls.Add(Label3)
        Me.PanDatosMotivoAnulacion.Controls.Add(Me.txtCodigo)
        Me.PanDatosMotivoAnulacion.Controls.Add(Me.txtTelefono)
        Me.PanDatosMotivoAnulacion.Controls.Add(Me.txtDireccion)
        Me.PanDatosMotivoAnulacion.Controls.Add(Label2)
        Me.PanDatosMotivoAnulacion.Controls.Add(Me.txtApellidos)
        Me.PanDatosMotivoAnulacion.Controls.Add(Me.lblCodigo)
        Me.PanDatosMotivoAnulacion.Controls.Add(IdEmpresaLabel)
        Me.PanDatosMotivoAnulacion.Controls.Add(Label1)
        Me.PanDatosMotivoAnulacion.Controls.Add(NombreLabel)
        Me.PanDatosMotivoAnulacion.Controls.Add(Me.txtNombres)
        Me.PanDatosMotivoAnulacion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanDatosMotivoAnulacion.Location = New System.Drawing.Point(0, 0)
        Me.PanDatosMotivoAnulacion.Margin = New System.Windows.Forms.Padding(4)
        Me.PanDatosMotivoAnulacion.Name = "PanDatosMotivoAnulacion"
        Me.PanDatosMotivoAnulacion.Size = New System.Drawing.Size(1336, 603)
        Me.PanDatosMotivoAnulacion.TabIndex = 0
        Me.PanDatosMotivoAnulacion.Tag = ""
        '
        'dtpUltimaSesion
        '
        Me.dtpUltimaSesion.CustomFormat = "dd/MM/yyyy HH:mm:ss"
        Me.dtpUltimaSesion.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpUltimaSesion.Location = New System.Drawing.Point(766, 92)
        Me.dtpUltimaSesion.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.dtpUltimaSesion.Name = "dtpUltimaSesion"
        Me.dtpUltimaSesion.Size = New System.Drawing.Size(190, 23)
        Me.dtpUltimaSesion.TabIndex = 47
        '
        'picEstadoOp
        '
        Me.picEstadoOp.Location = New System.Drawing.Point(766, 36)
        Me.picEstadoOp.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.picEstadoOp.MenuManager = Me.RibbonControl
        Me.picEstadoOp.Name = "picEstadoOp"
        Me.picEstadoOp.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picEstadoOp.Properties.Appearance.Options.UseBackColor = True
        Me.picEstadoOp.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picEstadoOp.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.[Auto]
        Me.picEstadoOp.Size = New System.Drawing.Size(189, 39)
        Me.picEstadoOp.TabIndex = 43
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.chkMontacarga)
        Me.GroupControl1.Controls.Add(lblMontacarga)
        Me.GroupControl1.Controls.Add(Me.chkVerifica)
        Me.GroupControl1.Controls.Add(lblchkVerifica)
        Me.GroupControl1.Controls.Add(Me.chkPickea)
        Me.GroupControl1.Controls.Add(lblchkPickea)
        Me.GroupControl1.Controls.Add(Me.chkTransporta)
        Me.GroupControl1.Controls.Add(Label13)
        Me.GroupControl1.Controls.Add(Me.chkUbica)
        Me.GroupControl1.Controls.Add(lblchkUbica)
        Me.GroupControl1.Controls.Add(Me.chkRecibe)
        Me.GroupControl1.Controls.Add(Label11)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupControl1.Location = New System.Drawing.Point(2, 425)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1332, 176)
        Me.GroupControl1.TabIndex = 42
        Me.GroupControl1.Text = "Atribución Operador"
        '
        'chkMontacarga
        '
        Me.chkMontacarga.Location = New System.Drawing.Point(464, 101)
        Me.chkMontacarga.Margin = New System.Windows.Forms.Padding(4)
        Me.chkMontacarga.MenuManager = Me.RibbonControl
        Me.chkMontacarga.Name = "chkMontacarga"
        Me.chkMontacarga.Properties.Caption = ""
        Me.chkMontacarga.Size = New System.Drawing.Size(84, 24)
        Me.chkMontacarga.TabIndex = 43
        '
        'chkVerifica
        '
        Me.chkVerifica.Location = New System.Drawing.Point(464, 68)
        Me.chkVerifica.Margin = New System.Windows.Forms.Padding(4)
        Me.chkVerifica.MenuManager = Me.RibbonControl
        Me.chkVerifica.Name = "chkVerifica"
        Me.chkVerifica.Properties.Caption = ""
        Me.chkVerifica.Size = New System.Drawing.Size(84, 24)
        Me.chkVerifica.TabIndex = 41
        '
        'chkPickea
        '
        Me.chkPickea.Location = New System.Drawing.Point(464, 36)
        Me.chkPickea.Margin = New System.Windows.Forms.Padding(4)
        Me.chkPickea.MenuManager = Me.RibbonControl
        Me.chkPickea.Name = "chkPickea"
        Me.chkPickea.Properties.Caption = ""
        Me.chkPickea.Size = New System.Drawing.Size(84, 24)
        Me.chkPickea.TabIndex = 39
        '
        'chkTransporta
        '
        Me.chkTransporta.Location = New System.Drawing.Point(158, 101)
        Me.chkTransporta.Margin = New System.Windows.Forms.Padding(4)
        Me.chkTransporta.MenuManager = Me.RibbonControl
        Me.chkTransporta.Name = "chkTransporta"
        Me.chkTransporta.Properties.Caption = ""
        Me.chkTransporta.Size = New System.Drawing.Size(84, 24)
        Me.chkTransporta.TabIndex = 37
        '
        'chkUbica
        '
        Me.chkUbica.Location = New System.Drawing.Point(158, 68)
        Me.chkUbica.Margin = New System.Windows.Forms.Padding(4)
        Me.chkUbica.MenuManager = Me.RibbonControl
        Me.chkUbica.Name = "chkUbica"
        Me.chkUbica.Properties.Caption = ""
        Me.chkUbica.Size = New System.Drawing.Size(84, 24)
        Me.chkUbica.TabIndex = 35
        '
        'chkRecibe
        '
        Me.chkRecibe.Location = New System.Drawing.Point(158, 36)
        Me.chkRecibe.Margin = New System.Windows.Forms.Padding(4)
        Me.chkRecibe.MenuManager = Me.RibbonControl
        Me.chkRecibe.Name = "chkRecibe"
        Me.chkRecibe.Properties.Caption = ""
        Me.chkRecibe.Size = New System.Drawing.Size(84, 24)
        Me.chkRecibe.TabIndex = 33
        '
        'CameraControl1
        '
        Me.CameraControl1.AutoStartDefaultDevice = False
        Me.CameraControl1.DeviceNotFoundString = "No hay dispositivos para captura de imágen."
        Me.CameraControl1.Enabled = False
        Me.CameraControl1.Location = New System.Drawing.Point(664, 170)
        Me.CameraControl1.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.CameraControl1.Name = "CameraControl1"
        Me.CameraControl1.Size = New System.Drawing.Size(276, 214)
        Me.CameraControl1.TabIndex = 31
        Me.CameraControl1.Text = "CameraControl1"
        '
        'cmbJornada
        '
        Me.cmbJornada.Location = New System.Drawing.Point(156, 106)
        Me.cmbJornada.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbJornada.MenuManager = Me.RibbonControl
        Me.cmbJornada.Name = "cmbJornada"
        Me.cmbJornada.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbJornada.Properties.NullText = ""
        Me.cmbJornada.Size = New System.Drawing.Size(241, 22)
        Me.cmbJornada.TabIndex = 30
        '
        'cmbEmpresa
        '
        Me.cmbEmpresa.Location = New System.Drawing.Point(156, 71)
        Me.cmbEmpresa.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbEmpresa.MenuManager = Me.RibbonControl
        Me.cmbEmpresa.Name = "cmbEmpresa"
        Me.cmbEmpresa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbEmpresa.Properties.NullText = ""
        Me.cmbEmpresa.Size = New System.Drawing.Size(241, 22)
        Me.cmbEmpresa.TabIndex = 29
        '
        'btnExaminar
        '
        Me.btnExaminar.Location = New System.Drawing.Point(405, 388)
        Me.btnExaminar.Margin = New System.Windows.Forms.Padding(4)
        Me.btnExaminar.Name = "btnExaminar"
        Me.btnExaminar.Size = New System.Drawing.Size(241, 31)
        Me.btnExaminar.TabIndex = 26
        Me.btnExaminar.Text = "Examinar..."
        '
        'picFoto
        '
        Me.picFoto.Cursor = System.Windows.Forms.Cursors.Default
        Me.picFoto.Location = New System.Drawing.Point(405, 170)
        Me.picFoto.Margin = New System.Windows.Forms.Padding(4)
        Me.picFoto.MenuManager = Me.RibbonControl
        Me.picFoto.Name = "picFoto"
        Me.picFoto.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat
        Me.picFoto.Properties.PictureStoreMode = DevExpress.XtraEditors.Controls.PictureStoreMode.ByteArray
        Me.picFoto.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Always
        Me.picFoto.Size = New System.Drawing.Size(241, 214)
        Me.picFoto.TabIndex = 23
        '
        'lnkRolOperador
        '
        Me.lnkRolOperador.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lnkRolOperador.AutoSize = True
        Me.lnkRolOperador.Location = New System.Drawing.Point(26, 142)
        Me.lnkRolOperador.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lnkRolOperador.Name = "lnkRolOperador"
        Me.lnkRolOperador.Size = New System.Drawing.Size(83, 16)
        Me.lnkRolOperador.TabIndex = 6
        Me.lnkRolOperador.TabStop = True
        Me.lnkRolOperador.Text = "Rol Operador"
        '
        'txtIdRolOperador
        '
        Me.txtIdRolOperador.Location = New System.Drawing.Point(156, 138)
        Me.txtIdRolOperador.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIdRolOperador.MenuManager = Me.RibbonControl
        Me.txtIdRolOperador.Name = "txtIdRolOperador"
        Me.txtIdRolOperador.Properties.Mask.EditMask = "n0"
        Me.txtIdRolOperador.Size = New System.Drawing.Size(241, 22)
        Me.txtIdRolOperador.TabIndex = 7
        '
        'txtNombreOperador
        '
        Me.txtNombreOperador.Location = New System.Drawing.Point(405, 138)
        Me.txtNombreOperador.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNombreOperador.MenuManager = Me.RibbonControl
        Me.txtNombreOperador.Name = "txtNombreOperador"
        Me.txtNombreOperador.Properties.ReadOnly = True
        Me.txtNombreOperador.Size = New System.Drawing.Size(241, 22)
        Me.txtNombreOperador.TabIndex = 8
        '
        'txtCostoHora
        '
        Me.txtCostoHora.DecimalPlaces = 6
        Me.txtCostoHora.Location = New System.Drawing.Point(156, 358)
        Me.txtCostoHora.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCostoHora.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtCostoHora.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtCostoHora.Name = "txtCostoHora"
        Me.txtCostoHora.Size = New System.Drawing.Size(241, 23)
        Me.txtCostoHora.TabIndex = 22
        '
        'txtClave
        '
        Me.txtClave.Location = New System.Drawing.Point(156, 327)
        Me.txtClave.Margin = New System.Windows.Forms.Padding(4)
        Me.txtClave.MenuManager = Me.RibbonControl
        Me.txtClave.Name = "txtClave"
        Me.txtClave.Size = New System.Drawing.Size(241, 22)
        Me.txtClave.TabIndex = 20
        '
        'txtCodigo
        '
        Me.txtCodigo.Location = New System.Drawing.Point(156, 297)
        Me.txtCodigo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCodigo.MenuManager = Me.RibbonControl
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.Size = New System.Drawing.Size(241, 22)
        Me.txtCodigo.TabIndex = 18
        '
        'txtTelefono
        '
        Me.txtTelefono.Location = New System.Drawing.Point(156, 266)
        Me.txtTelefono.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTelefono.MenuManager = Me.RibbonControl
        Me.txtTelefono.Name = "txtTelefono"
        Me.txtTelefono.Size = New System.Drawing.Size(241, 22)
        Me.txtTelefono.TabIndex = 16
        '
        'txtDireccion
        '
        Me.txtDireccion.Location = New System.Drawing.Point(156, 233)
        Me.txtDireccion.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDireccion.MenuManager = Me.RibbonControl
        Me.txtDireccion.Name = "txtDireccion"
        Me.txtDireccion.Size = New System.Drawing.Size(241, 22)
        Me.txtDireccion.TabIndex = 14
        '
        'txtApellidos
        '
        Me.txtApellidos.Location = New System.Drawing.Point(156, 201)
        Me.txtApellidos.Margin = New System.Windows.Forms.Padding(4)
        Me.txtApellidos.MenuManager = Me.RibbonControl
        Me.txtApellidos.Name = "txtApellidos"
        Me.txtApellidos.Size = New System.Drawing.Size(241, 22)
        Me.txtApellidos.TabIndex = 12
        '
        'lblCodigo
        '
        Me.lblCodigo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCodigo.AutoSize = True
        Me.lblCodigo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCodigo.Location = New System.Drawing.Point(153, 44)
        Me.lblCodigo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCodigo.Name = "lblCodigo"
        Me.lblCodigo.Size = New System.Drawing.Size(0, 17)
        Me.lblCodigo.TabIndex = 1
        '
        'txtNombres
        '
        Me.txtNombres.Location = New System.Drawing.Point(156, 167)
        Me.txtNombres.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNombres.MenuManager = Me.RibbonControl
        Me.txtNombres.Name = "txtNombres"
        Me.txtNombres.Size = New System.Drawing.Size(241, 22)
        Me.txtNombres.TabIndex = 10
        '
        'Fec_agrDateEdit
        '
        Me.Fec_agrDateEdit.EditValue = Nothing
        Me.Fec_agrDateEdit.Enabled = False
        Me.Fec_agrDateEdit.Location = New System.Drawing.Point(140, 36)
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
        Me.User_agrTextEdit.Location = New System.Drawing.Point(140, 4)
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
        Me.Fec_modDateEdit.Location = New System.Drawing.Point(624, 36)
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
        Me.User_modTextEdit.Location = New System.Drawing.Point(624, 4)
        Me.User_modTextEdit.Margin = New System.Windows.Forms.Padding(4)
        Me.User_modTextEdit.MenuManager = Me.RibbonControl
        Me.User_modTextEdit.Name = "User_modTextEdit"
        Me.User_modTextEdit.Size = New System.Drawing.Size(261, 22)
        Me.User_modTextEdit.TabIndex = 3
        '
        'GrpEmpresaTB
        '
        Me.GrpEmpresaTB.Controls.Add(Me.GroupControl3)
        Me.GrpEmpresaTB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpEmpresaTB.Location = New System.Drawing.Point(0, 0)
        Me.GrpEmpresaTB.Margin = New System.Windows.Forms.Padding(4)
        Me.GrpEmpresaTB.Name = "GrpEmpresaTB"
        Me.GrpEmpresaTB.Size = New System.Drawing.Size(1336, 603)
        Me.GrpEmpresaTB.TabIndex = 0
        Me.GrpEmpresaTB.Tag = ""
        '
        'GroupControl3
        '
        Me.GroupControl3.Controls.Add(Me.Grid)
        Me.GroupControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl3.Location = New System.Drawing.Point(2, 28)
        Me.GroupControl3.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(1332, 573)
        Me.GroupControl3.TabIndex = 0
        Me.GroupControl3.Text = "Selección de Bodega"
        '
        'Grid
        '
        Me.Grid.Cursor = System.Windows.Forms.Cursors.Default
        Me.Grid.DataSource = Me.DataBindingSource
        Me.Grid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Grid.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.Grid.Location = New System.Drawing.Point(2, 28)
        Me.Grid.MainView = Me.gridView1
        Me.Grid.Margin = New System.Windows.Forms.Padding(4)
        Me.Grid.Name = "Grid"
        Me.Grid.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.RepositoryItemCheckEdit2})
        Me.Grid.Size = New System.Drawing.Size(1328, 543)
        Me.Grid.TabIndex = 0
        Me.Grid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridView1})
        '
        'DataBindingSource
        '
        Me.DataBindingSource.DataMember = "Data"
        Me.DataBindingSource.DataSource = Me.DsOperador
        '
        'DsOperador
        '
        Me.DsOperador.DataSetName = "DsOperador"
        Me.DsOperador.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'gridView1
        '
        Me.gridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colSeleccion, Me.colIdBodega, Me.IdOperadorBodega, Me.colBodega, Me.IdInterno})
        Me.gridView1.CustomizationFormBounds = New System.Drawing.Rectangle(590, 414, 252, 234)
        Me.gridView1.DetailHeight = 431
        Me.gridView1.GridControl = Me.Grid
        Me.gridView1.Name = "gridView1"
        Me.gridView1.OptionsView.ShowGroupPanel = False
        '
        'colSeleccion
        '
        Me.colSeleccion.Caption = "Asignar"
        Me.colSeleccion.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.colSeleccion.FieldName = "Selección"
        Me.colSeleccion.MinWidth = 23
        Me.colSeleccion.Name = "colSeleccion"
        Me.colSeleccion.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.colSeleccion.OptionsFilter.ImmediateUpdatePopupDateFilterOnCheck = DevExpress.Utils.DefaultBoolean.[True]
        Me.colSeleccion.OptionsFilter.ImmediateUpdatePopupDateFilterOnDateChange = DevExpress.Utils.DefaultBoolean.[True]
        Me.colSeleccion.OptionsFilter.ShowBlanksFilterItems = DevExpress.Utils.DefaultBoolean.[True]
        Me.colSeleccion.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.colSeleccion.Visible = True
        Me.colSeleccion.VisibleIndex = 0
        Me.colSeleccion.Width = 87
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        '
        'colIdBodega
        '
        Me.colIdBodega.FieldName = "IdBodega"
        Me.colIdBodega.MinWidth = 23
        Me.colIdBodega.Name = "colIdBodega"
        Me.colIdBodega.OptionsColumn.ReadOnly = True
        Me.colIdBodega.Width = 87
        '
        'IdOperadorBodega
        '
        Me.IdOperadorBodega.Caption = "Asignar"
        Me.IdOperadorBodega.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.IdOperadorBodega.FieldName = "IdOperadorBodega"
        Me.IdOperadorBodega.MinWidth = 23
        Me.IdOperadorBodega.Name = "IdOperadorBodega"
        Me.IdOperadorBodega.OptionsColumn.ReadOnly = True
        Me.IdOperadorBodega.Width = 87
        '
        'colBodega
        '
        Me.colBodega.FieldName = "Bodega"
        Me.colBodega.MinWidth = 23
        Me.colBodega.Name = "colBodega"
        Me.colBodega.OptionsColumn.AllowEdit = False
        Me.colBodega.OptionsColumn.ReadOnly = True
        Me.colBodega.Visible = True
        Me.colBodega.VisibleIndex = 1
        Me.colBodega.Width = 87
        '
        'IdInterno
        '
        Me.IdInterno.Caption = "IdInterno"
        Me.IdInterno.FieldName = "IdInterno"
        Me.IdInterno.MinWidth = 23
        Me.IdInterno.Name = "IdInterno"
        Me.IdInterno.OptionsColumn.ReadOnly = True
        Me.IdInterno.Width = 87
        '
        'RepositoryItemCheckEdit2
        '
        Me.RepositoryItemCheckEdit2.AutoHeight = False
        Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
        '
        'dkOperador
        '
        Me.dkOperador.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerBottom})
        Me.dkOperador.Form = Me
        Me.dkOperador.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane"})
        '
        'hideContainerBottom
        '
        Me.hideContainerBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.hideContainerBottom.Controls.Add(Me.DockPanel1)
        Me.hideContainerBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 826)
        Me.hideContainerBottom.Margin = New System.Windows.Forms.Padding(4)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(1338, 26)
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.ID = New System.Guid("e618db3d-6cc7-4035-b9d1-a3d9d50d5805")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 684)
        Me.DockPanel1.Margin = New System.Windows.Forms.Padding(4)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(200, 114)
        Me.DockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.SavedIndex = 0
        Me.DockPanel1.Size = New System.Drawing.Size(1338, 142)
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
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1330, 104)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'TabDatos
        '
        Me.TabDatos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabDatos.Location = New System.Drawing.Point(0, 193)
        Me.TabDatos.Margin = New System.Windows.Forms.Padding(4)
        Me.TabDatos.Name = "TabDatos"
        Me.TabDatos.SelectedTabPage = Me.XtraTabPage1
        Me.TabDatos.Size = New System.Drawing.Size(1338, 633)
        Me.TabDatos.TabIndex = 4
        Me.TabDatos.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.XtraTabPage1, Me.TabOperadorBodega, Me.ResolucionesLP, Me.XtraTabPage2, Me.xTabJornadaLaboral})
        '
        'XtraTabPage1
        '
        Me.XtraTabPage1.Controls.Add(Me.PanDatosMotivoAnulacion)
        Me.XtraTabPage1.Margin = New System.Windows.Forms.Padding(4)
        Me.XtraTabPage1.Name = "XtraTabPage1"
        Me.XtraTabPage1.Size = New System.Drawing.Size(1336, 603)
        Me.XtraTabPage1.Text = "Datos generales"
        '
        'TabOperadorBodega
        '
        Me.TabOperadorBodega.Controls.Add(Me.GrpEmpresaTB)
        Me.TabOperadorBodega.Margin = New System.Windows.Forms.Padding(4)
        Me.TabOperadorBodega.Name = "TabOperadorBodega"
        Me.TabOperadorBodega.Size = New System.Drawing.Size(1336, 603)
        Me.TabOperadorBodega.Text = "Asignación por bodega"
        '
        'ResolucionesLP
        '
        Me.ResolucionesLP.Controls.Add(Me.SplitContainerControl1)
        Me.ResolucionesLP.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.ResolucionesLP.Name = "ResolucionesLP"
        Me.ResolucionesLP.Size = New System.Drawing.Size(1336, 603)
        Me.ResolucionesLP.Text = "Resoluciones LP"
        '
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.SplitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl1.Horizontal = False
        Me.SplitContainerControl1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl1.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        '
        'SplitContainerControl1.Panel1
        '
        Me.SplitContainerControl1.Panel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.GroupControl2)
        Me.SplitContainerControl1.Panel1.Text = "Panel1"
        '
        'SplitContainerControl1.Panel2
        '
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.GroupControl5)
        Me.SplitContainerControl1.Panel2.Text = "Panel2"
        Me.SplitContainerControl1.Size = New System.Drawing.Size(1336, 603)
        Me.SplitContainerControl1.SplitterPosition = 372
        Me.SplitContainerControl1.TabIndex = 18
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Label10)
        Me.GroupControl2.Controls.Add(Label8)
        Me.GroupControl2.Controls.Add(Me.ToolStripPR)
        Me.GroupControl2.Controls.Add(lblCorrelativoActual)
        Me.GroupControl2.Controls.Add(Me.txtCorrelativoActual)
        Me.GroupControl2.Controls.Add(lblCorrelativoFinal)
        Me.GroupControl2.Controls.Add(Me.txtCorrelativoFinal)
        Me.GroupControl2.Controls.Add(lblBodega)
        Me.GroupControl2.Controls.Add(Me.cmbBodega)
        Me.GroupControl2.Controls.Add(Me.lblIdResolucionLP)
        Me.GroupControl2.Controls.Add(ActivoLabel)
        Me.GroupControl2.Controls.Add(Me.chkResolucionLPActiva)
        Me.GroupControl2.Controls.Add(lblCorrelativoInicial)
        Me.GroupControl2.Controls.Add(Me.txtCorrelativoInicial)
        Me.GroupControl2.Controls.Add(CodigoLabel)
        Me.GroupControl2.Controls.Add(lblSerie)
        Me.GroupControl2.Controls.Add(Me.txtNoSerie)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl2.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(1328, 368)
        Me.GroupControl2.TabIndex = 1
        Me.GroupControl2.Text = "Datos de Resolución"
        '
        'ToolStripPR
        '
        Me.ToolStripPR.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStripPR.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdNewPR, Me.cmdSavePR, Me.cmdDesactivarResolucion})
        Me.ToolStripPR.Location = New System.Drawing.Point(2, 28)
        Me.ToolStripPR.Name = "ToolStripPR"
        Me.ToolStripPR.Size = New System.Drawing.Size(1324, 27)
        Me.ToolStripPR.TabIndex = 16
        Me.ToolStripPR.Text = "ToolStrip2"
        '
        'cmdNewPR
        '
        Me.cmdNewPR.Image = CType(resources.GetObject("cmdNewPR.Image"), System.Drawing.Image)
        Me.cmdNewPR.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdNewPR.Name = "cmdNewPR"
        Me.cmdNewPR.Size = New System.Drawing.Size(76, 24)
        Me.cmdNewPR.Text = "Nuevo"
        '
        'cmdSavePR
        '
        Me.cmdSavePR.Image = CType(resources.GetObject("cmdSavePR.Image"), System.Drawing.Image)
        Me.cmdSavePR.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdSavePR.Name = "cmdSavePR"
        Me.cmdSavePR.Size = New System.Drawing.Size(86, 24)
        Me.cmdSavePR.Text = "Guardar"
        '
        'cmdDesactivarResolucion
        '
        Me.cmdDesactivarResolucion.Image = CType(resources.GetObject("cmdDesactivarResolucion.Image"), System.Drawing.Image)
        Me.cmdDesactivarResolucion.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdDesactivarResolucion.Name = "cmdDesactivarResolucion"
        Me.cmdDesactivarResolucion.Size = New System.Drawing.Size(102, 24)
        Me.cmdDesactivarResolucion.Text = "Desactivar"
        '
        'txtCorrelativoActual
        '
        Me.txtCorrelativoActual.Location = New System.Drawing.Point(180, 247)
        Me.txtCorrelativoActual.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCorrelativoActual.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtCorrelativoActual.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtCorrelativoActual.Name = "txtCorrelativoActual"
        Me.txtCorrelativoActual.ReadOnly = True
        Me.txtCorrelativoActual.Size = New System.Drawing.Size(241, 23)
        Me.txtCorrelativoActual.TabIndex = 6
        '
        'txtCorrelativoFinal
        '
        Me.txtCorrelativoFinal.Location = New System.Drawing.Point(180, 217)
        Me.txtCorrelativoFinal.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCorrelativoFinal.Maximum = New Decimal(New Integer() {999999999, 0, 0, 0})
        Me.txtCorrelativoFinal.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.txtCorrelativoFinal.Name = "txtCorrelativoFinal"
        Me.txtCorrelativoFinal.Size = New System.Drawing.Size(241, 23)
        Me.txtCorrelativoFinal.TabIndex = 5
        Me.txtCorrelativoFinal.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'cmbBodega
        '
        Me.cmbBodega.Location = New System.Drawing.Point(180, 126)
        Me.cmbBodega.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.cmbBodega.Name = "cmbBodega"
        Me.cmbBodega.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbBodega.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodega.Properties.NullText = ""
        Me.cmbBodega.Size = New System.Drawing.Size(241, 22)
        Me.cmbBodega.TabIndex = 2
        '
        'lblIdResolucionLP
        '
        Me.lblIdResolucionLP.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar
        Me.lblIdResolucionLP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblIdResolucionLP.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIdResolucionLP.Location = New System.Drawing.Point(180, 92)
        Me.lblIdResolucionLP.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblIdResolucionLP.Name = "lblIdResolucionLP"
        Me.lblIdResolucionLP.Size = New System.Drawing.Size(241, 27)
        Me.lblIdResolucionLP.TabIndex = 1
        '
        'chkResolucionLPActiva
        '
        Me.chkResolucionLPActiva.EditValue = True
        Me.chkResolucionLPActiva.Location = New System.Drawing.Point(525, 90)
        Me.chkResolucionLPActiva.Margin = New System.Windows.Forms.Padding(4)
        Me.chkResolucionLPActiva.MenuManager = Me.RibbonControl
        Me.chkResolucionLPActiva.Name = "chkResolucionLPActiva"
        Me.chkResolucionLPActiva.Properties.Caption = ""
        Me.chkResolucionLPActiva.Size = New System.Drawing.Size(41, 24)
        Me.chkResolucionLPActiva.TabIndex = 7
        '
        'txtCorrelativoInicial
        '
        Me.txtCorrelativoInicial.Location = New System.Drawing.Point(180, 185)
        Me.txtCorrelativoInicial.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCorrelativoInicial.Maximum = New Decimal(New Integer() {999999999, 0, 0, 0})
        Me.txtCorrelativoInicial.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.txtCorrelativoInicial.Name = "txtCorrelativoInicial"
        Me.txtCorrelativoInicial.Size = New System.Drawing.Size(241, 23)
        Me.txtCorrelativoInicial.TabIndex = 4
        Me.txtCorrelativoInicial.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'txtNoSerie
        '
        Me.txtNoSerie.Location = New System.Drawing.Point(180, 155)
        Me.txtNoSerie.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNoSerie.MenuManager = Me.RibbonControl
        Me.txtNoSerie.Name = "txtNoSerie"
        Me.txtNoSerie.Properties.MaxLength = 3
        Me.txtNoSerie.Size = New System.Drawing.Size(241, 22)
        Me.txtNoSerie.TabIndex = 3
        '
        'GroupControl5
        '
        Me.GroupControl5.Controls.Add(Me.dGridPresentacion)
        Me.GroupControl5.Controls.Add(Me.chkActivoPR)
        Me.GroupControl5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl5.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl5.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl5.Name = "GroupControl5"
        Me.GroupControl5.Size = New System.Drawing.Size(1332, 215)
        Me.GroupControl5.TabIndex = 30
        Me.GroupControl5.Text = "Listado de resoluciones"
        '
        'dGridPresentacion
        '
        Me.dGridPresentacion.Cursor = System.Windows.Forms.Cursors.Default
        Me.dGridPresentacion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dGridPresentacion.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode1.RelationName = "Level1"
        Me.dGridPresentacion.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.dGridPresentacion.Location = New System.Drawing.Point(2, 52)
        Me.dGridPresentacion.MainView = Me.GrdPresentacion
        Me.dGridPresentacion.Margin = New System.Windows.Forms.Padding(4)
        Me.dGridPresentacion.MenuManager = Me.RibbonControl
        Me.dGridPresentacion.Name = "dGridPresentacion"
        Me.dGridPresentacion.Size = New System.Drawing.Size(1328, 161)
        Me.dGridPresentacion.TabIndex = 0
        Me.dGridPresentacion.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GrdPresentacion, Me.GridView6})
        '
        'GrdPresentacion
        '
        Me.GrdPresentacion.DetailHeight = 431
        Me.GrdPresentacion.GridControl = Me.dGridPresentacion
        Me.GrdPresentacion.Name = "GrdPresentacion"
        Me.GrdPresentacion.OptionsBehavior.Editable = False
        Me.GrdPresentacion.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.GrdPresentacion.OptionsView.ShowGroupPanel = False
        '
        'GridView6
        '
        Me.GridView6.DetailHeight = 431
        Me.GridView6.GridControl = Me.dGridPresentacion
        Me.GridView6.Name = "GridView6"
        '
        'chkActivoPR
        '
        Me.chkActivoPR.Dock = System.Windows.Forms.DockStyle.Top
        Me.chkActivoPR.EditValue = True
        Me.chkActivoPR.Location = New System.Drawing.Point(2, 28)
        Me.chkActivoPR.Margin = New System.Windows.Forms.Padding(4)
        Me.chkActivoPR.MenuManager = Me.RibbonControl
        Me.chkActivoPR.Name = "chkActivoPR"
        Me.chkActivoPR.Properties.Caption = "Resoluciones Activas"
        Me.chkActivoPR.Size = New System.Drawing.Size(1328, 24)
        Me.chkActivoPR.TabIndex = 2
        '
        'XtraTabPage2
        '
        Me.XtraTabPage2.Controls.Add(Me.TableLayoutPanel1)
        Me.XtraTabPage2.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.XtraTabPage2.Name = "XtraTabPage2"
        Me.XtraTabPage2.Size = New System.Drawing.Size(1336, 603)
        Me.XtraTabPage2.Text = "Tramos por Operador"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.GroupControl4, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupControl6, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 300.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1336, 603)
        Me.TableLayoutPanel1.TabIndex = 32
        '
        'GroupControl4
        '
        Me.GroupControl4.Controls.Add(Me.dgridTramosZonaPicking)
        Me.GroupControl4.Controls.Add(Me.lblIdZonaPickingTramoOp)
        Me.GroupControl4.Controls.Add(lblZonaPicking)
        Me.GroupControl4.Controls.Add(Me.cmbZonaPicking)
        Me.GroupControl4.Controls.Add(Me.ToolStrip1)
        Me.GroupControl4.Controls.Add(Label24)
        Me.GroupControl4.Controls.Add(Me.chkLunes)
        Me.GroupControl4.Controls.Add(Me.chkMartes)
        Me.GroupControl4.Controls.Add(Me.chkMiercoles)
        Me.GroupControl4.Controls.Add(Me.chkDomingo)
        Me.GroupControl4.Controls.Add(Me.chkJueves)
        Me.GroupControl4.Controls.Add(Me.chkViernes)
        Me.GroupControl4.Controls.Add(Me.chkSabado)
        Me.GroupControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl4.Location = New System.Drawing.Point(4, 4)
        Me.GroupControl4.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl4.Name = "GroupControl4"
        Me.GroupControl4.Size = New System.Drawing.Size(1328, 295)
        Me.GroupControl4.TabIndex = 2
        Me.GroupControl4.Text = "Datos de tramo"
        '
        'dgridTramosZonaPicking
        '
        Me.dgridTramosZonaPicking.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgridTramosZonaPicking.Dock = System.Windows.Forms.DockStyle.Right
        Me.dgridTramosZonaPicking.EmbeddedNavigator.Appearance.BorderColor = System.Drawing.Color.Black
        Me.dgridTramosZonaPicking.EmbeddedNavigator.Appearance.Options.UseBorderColor = True
        Me.dgridTramosZonaPicking.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode2.RelationName = "Level1"
        Me.dgridTramosZonaPicking.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode2})
        Me.dgridTramosZonaPicking.Location = New System.Drawing.Point(383, 55)
        Me.dgridTramosZonaPicking.MainView = Me.gvTramosPorZona
        Me.dgridTramosZonaPicking.Margin = New System.Windows.Forms.Padding(4)
        Me.dgridTramosZonaPicking.MenuManager = Me.RibbonControl
        Me.dgridTramosZonaPicking.Name = "dgridTramosZonaPicking"
        Me.dgridTramosZonaPicking.Size = New System.Drawing.Size(943, 238)
        Me.dgridTramosZonaPicking.TabIndex = 20
        Me.dgridTramosZonaPicking.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvTramosPorZona})
        '
        'gvTramosPorZona
        '
        Me.gvTramosPorZona.DetailHeight = 431
        Me.gvTramosPorZona.GridControl = Me.dgridTramosZonaPicking
        Me.gvTramosPorZona.Name = "gvTramosPorZona"
        Me.gvTramosPorZona.OptionsBehavior.Editable = False
        Me.gvTramosPorZona.OptionsView.ShowAutoFilterRow = True
        Me.gvTramosPorZona.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.gvTramosPorZona.OptionsView.ShowFooter = True
        Me.gvTramosPorZona.OptionsView.ShowGroupPanel = False
        '
        'lblIdZonaPickingTramoOp
        '
        Me.lblIdZonaPickingTramoOp.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIdZonaPickingTramoOp.Appearance.Options.UseFont = True
        Me.lblIdZonaPickingTramoOp.Appearance.Options.UseTextOptions = True
        Me.lblIdZonaPickingTramoOp.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblIdZonaPickingTramoOp.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.lblIdZonaPickingTramoOp.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblIdZonaPickingTramoOp.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.lblIdZonaPickingTramoOp.Location = New System.Drawing.Point(28, 132)
        Me.lblIdZonaPickingTramoOp.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.lblIdZonaPickingTramoOp.Name = "lblIdZonaPickingTramoOp"
        Me.lblIdZonaPickingTramoOp.Size = New System.Drawing.Size(241, 146)
        Me.lblIdZonaPickingTramoOp.TabIndex = 19
        Me.lblIdZonaPickingTramoOp.Text = "-"
        Me.lblIdZonaPickingTramoOp.Visible = False
        '
        'cmbZonaPicking
        '
        Me.cmbZonaPicking.Location = New System.Drawing.Point(28, 103)
        Me.cmbZonaPicking.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.cmbZonaPicking.Name = "cmbZonaPicking"
        Me.cmbZonaPicking.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbZonaPicking.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbZonaPicking.Properties.NullText = ""
        Me.cmbZonaPicking.Size = New System.Drawing.Size(241, 22)
        Me.cmbZonaPicking.TabIndex = 18
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuNuevoTramo, Me.mnuGuardarTramo, Me.mnuEliminarTramo})
        Me.ToolStrip1.Location = New System.Drawing.Point(2, 28)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1324, 27)
        Me.ToolStrip1.TabIndex = 16
        Me.ToolStrip1.Text = "ToolStrip2"
        '
        'mnuNuevoTramo
        '
        Me.mnuNuevoTramo.Image = CType(resources.GetObject("mnuNuevoTramo.Image"), System.Drawing.Image)
        Me.mnuNuevoTramo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuNuevoTramo.Name = "mnuNuevoTramo"
        Me.mnuNuevoTramo.Size = New System.Drawing.Size(76, 24)
        Me.mnuNuevoTramo.Text = "Nuevo"
        '
        'mnuGuardarTramo
        '
        Me.mnuGuardarTramo.Image = CType(resources.GetObject("mnuGuardarTramo.Image"), System.Drawing.Image)
        Me.mnuGuardarTramo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuGuardarTramo.Name = "mnuGuardarTramo"
        Me.mnuGuardarTramo.Size = New System.Drawing.Size(86, 24)
        Me.mnuGuardarTramo.Text = "Guardar"
        '
        'mnuEliminarTramo
        '
        Me.mnuEliminarTramo.Image = CType(resources.GetObject("mnuEliminarTramo.Image"), System.Drawing.Image)
        Me.mnuEliminarTramo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuEliminarTramo.Name = "mnuEliminarTramo"
        Me.mnuEliminarTramo.Size = New System.Drawing.Size(87, 24)
        Me.mnuEliminarTramo.Text = "Eliminar"
        '
        'chkLunes
        '
        Me.chkLunes.AutoSize = True
        Me.chkLunes.Location = New System.Drawing.Point(307, 107)
        Me.chkLunes.Margin = New System.Windows.Forms.Padding(4)
        Me.chkLunes.Name = "chkLunes"
        Me.chkLunes.Size = New System.Drawing.Size(35, 20)
        Me.chkLunes.TabIndex = 1
        Me.chkLunes.Tag = "1"
        Me.chkLunes.Text = "L"
        Me.chkLunes.UseVisualStyleBackColor = True
        '
        'chkMartes
        '
        Me.chkMartes.AutoSize = True
        Me.chkMartes.Location = New System.Drawing.Point(307, 135)
        Me.chkMartes.Margin = New System.Windows.Forms.Padding(4)
        Me.chkMartes.Name = "chkMartes"
        Me.chkMartes.Size = New System.Drawing.Size(39, 20)
        Me.chkMartes.TabIndex = 2
        Me.chkMartes.Tag = "2"
        Me.chkMartes.Text = "M"
        Me.chkMartes.UseVisualStyleBackColor = True
        '
        'chkMiercoles
        '
        Me.chkMiercoles.AutoSize = True
        Me.chkMiercoles.Location = New System.Drawing.Point(307, 162)
        Me.chkMiercoles.Margin = New System.Windows.Forms.Padding(4)
        Me.chkMiercoles.Name = "chkMiercoles"
        Me.chkMiercoles.Size = New System.Drawing.Size(39, 20)
        Me.chkMiercoles.TabIndex = 3
        Me.chkMiercoles.Tag = "3"
        Me.chkMiercoles.Text = "M"
        Me.chkMiercoles.UseVisualStyleBackColor = True
        '
        'chkDomingo
        '
        Me.chkDomingo.AutoSize = True
        Me.chkDomingo.Location = New System.Drawing.Point(307, 274)
        Me.chkDomingo.Margin = New System.Windows.Forms.Padding(4)
        Me.chkDomingo.Name = "chkDomingo"
        Me.chkDomingo.Size = New System.Drawing.Size(37, 20)
        Me.chkDomingo.TabIndex = 7
        Me.chkDomingo.Tag = "7"
        Me.chkDomingo.Text = "D"
        Me.chkDomingo.UseVisualStyleBackColor = True
        '
        'chkJueves
        '
        Me.chkJueves.AutoSize = True
        Me.chkJueves.Location = New System.Drawing.Point(307, 190)
        Me.chkJueves.Margin = New System.Windows.Forms.Padding(4)
        Me.chkJueves.Name = "chkJueves"
        Me.chkJueves.Size = New System.Drawing.Size(34, 20)
        Me.chkJueves.TabIndex = 4
        Me.chkJueves.Tag = "4"
        Me.chkJueves.Text = "J"
        Me.chkJueves.UseVisualStyleBackColor = True
        '
        'chkViernes
        '
        Me.chkViernes.AutoSize = True
        Me.chkViernes.Location = New System.Drawing.Point(307, 218)
        Me.chkViernes.Margin = New System.Windows.Forms.Padding(4)
        Me.chkViernes.Name = "chkViernes"
        Me.chkViernes.Size = New System.Drawing.Size(37, 20)
        Me.chkViernes.TabIndex = 5
        Me.chkViernes.Tag = "5"
        Me.chkViernes.Text = "V"
        Me.chkViernes.UseVisualStyleBackColor = True
        '
        'chkSabado
        '
        Me.chkSabado.AutoSize = True
        Me.chkSabado.Location = New System.Drawing.Point(307, 246)
        Me.chkSabado.Margin = New System.Windows.Forms.Padding(4)
        Me.chkSabado.Name = "chkSabado"
        Me.chkSabado.Size = New System.Drawing.Size(37, 20)
        Me.chkSabado.TabIndex = 6
        Me.chkSabado.Tag = "6"
        Me.chkSabado.Text = "S"
        Me.chkSabado.UseVisualStyleBackColor = True
        '
        'GroupControl6
        '
        Me.GroupControl6.Controls.Add(Me.DgridZonaPickingOperador)
        Me.GroupControl6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl6.Location = New System.Drawing.Point(4, 307)
        Me.GroupControl6.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl6.Name = "GroupControl6"
        Me.GroupControl6.Size = New System.Drawing.Size(1328, 292)
        Me.GroupControl6.TabIndex = 31
        Me.GroupControl6.Text = "Configuración de zonas de picking"
        '
        'DgridZonaPickingOperador
        '
        Me.DgridZonaPickingOperador.Cursor = System.Windows.Forms.Cursors.Default
        Me.DgridZonaPickingOperador.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgridZonaPickingOperador.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode3.RelationName = "Level1"
        Me.DgridZonaPickingOperador.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode3})
        Me.DgridZonaPickingOperador.Location = New System.Drawing.Point(2, 28)
        Me.DgridZonaPickingOperador.MainView = Me.GridView2
        Me.DgridZonaPickingOperador.Margin = New System.Windows.Forms.Padding(4)
        Me.DgridZonaPickingOperador.MenuManager = Me.RibbonControl
        Me.DgridZonaPickingOperador.Name = "DgridZonaPickingOperador"
        Me.DgridZonaPickingOperador.Size = New System.Drawing.Size(1324, 262)
        Me.DgridZonaPickingOperador.TabIndex = 0
        Me.DgridZonaPickingOperador.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView2, Me.GridView3})
        '
        'GridView2
        '
        Me.GridView2.DetailHeight = 431
        Me.GridView2.GridControl = Me.DgridZonaPickingOperador
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsBehavior.Editable = False
        Me.GridView2.OptionsView.ShowAutoFilterRow = True
        Me.GridView2.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.GridView2.OptionsView.ShowFooter = True
        Me.GridView2.OptionsView.ShowGroupPanel = False
        '
        'GridView3
        '
        Me.GridView3.DetailHeight = 431
        Me.GridView3.GridControl = Me.DgridZonaPickingOperador
        Me.GridView3.Name = "GridView3"
        '
        'xTabJornadaLaboral
        '
        Me.xTabJornadaLaboral.Controls.Add(Me.SplitContainerControl2)
        Me.xTabJornadaLaboral.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.xTabJornadaLaboral.Name = "xTabJornadaLaboral"
        Me.xTabJornadaLaboral.Size = New System.Drawing.Size(1336, 603)
        Me.xTabJornadaLaboral.Text = "Jornada Laboral"
        '
        'SplitContainerControl2
        '
        Me.SplitContainerControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.SplitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl2.Horizontal = False
        Me.SplitContainerControl2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl2.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.SplitContainerControl2.Name = "SplitContainerControl2"
        '
        'SplitContainerControl2.Panel1
        '
        Me.SplitContainerControl2.Panel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.SplitContainerControl2.Panel1.Controls.Add(Me.GroupControl7)
        Me.SplitContainerControl2.Panel1.Text = "Panel1"
        '
        'SplitContainerControl2.Panel2
        '
        Me.SplitContainerControl2.Panel2.Controls.Add(Me.GroupControl8)
        Me.SplitContainerControl2.Panel2.Text = "Panel2"
        Me.SplitContainerControl2.Size = New System.Drawing.Size(1336, 603)
        Me.SplitContainerControl2.SplitterPosition = 372
        Me.SplitContainerControl2.TabIndex = 19
        '
        'GroupControl7
        '
        Me.GroupControl7.Controls.Add(Me.ToolStrip2)
        Me.GroupControl7.Controls.Add(Label15)
        Me.GroupControl7.Controls.Add(Me.cmbJornadaLaboral)
        Me.GroupControl7.Controls.Add(Label17)
        Me.GroupControl7.Controls.Add(Me.CheckEdit1)
        Me.GroupControl7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl7.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl7.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl7.Name = "GroupControl7"
        Me.GroupControl7.Size = New System.Drawing.Size(1328, 368)
        Me.GroupControl7.TabIndex = 1
        Me.GroupControl7.Text = "Datos de Jornada Laboral"
        '
        'ToolStrip2
        '
        Me.ToolStrip2.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tbGuardar, Me.ToolStripButton3})
        Me.ToolStrip2.Location = New System.Drawing.Point(2, 28)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.Size = New System.Drawing.Size(1324, 27)
        Me.ToolStrip2.TabIndex = 16
        Me.ToolStrip2.Text = "ToolStrip2"
        '
        'tbGuardar
        '
        Me.tbGuardar.Image = CType(resources.GetObject("tbGuardar.Image"), System.Drawing.Image)
        Me.tbGuardar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbGuardar.Name = "tbGuardar"
        Me.tbGuardar.Size = New System.Drawing.Size(86, 24)
        Me.tbGuardar.Text = "Guardar"
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.Image = CType(resources.GetObject("ToolStripButton3.Image"), System.Drawing.Image)
        Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.Size = New System.Drawing.Size(102, 24)
        Me.ToolStripButton3.Text = "Desactivar"
        '
        'cmbJornadaLaboral
        '
        Me.cmbJornadaLaboral.Location = New System.Drawing.Point(180, 89)
        Me.cmbJornadaLaboral.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.cmbJornadaLaboral.Name = "cmbJornadaLaboral"
        Me.cmbJornadaLaboral.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbJornadaLaboral.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbJornadaLaboral.Properties.NullText = ""
        Me.cmbJornadaLaboral.Size = New System.Drawing.Size(241, 22)
        Me.cmbJornadaLaboral.TabIndex = 11
        '
        'CheckEdit1
        '
        Me.CheckEdit1.EditValue = True
        Me.CheckEdit1.Location = New System.Drawing.Point(525, 90)
        Me.CheckEdit1.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckEdit1.MenuManager = Me.RibbonControl
        Me.CheckEdit1.Name = "CheckEdit1"
        Me.CheckEdit1.Properties.Caption = ""
        Me.CheckEdit1.Size = New System.Drawing.Size(41, 24)
        Me.CheckEdit1.TabIndex = 7
        '
        'GroupControl8
        '
        Me.GroupControl8.Controls.Add(Me.gdListaJornadaLaboral)
        Me.GroupControl8.Controls.Add(Me.CheckEdit2)
        Me.GroupControl8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl8.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl8.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl8.Name = "GroupControl8"
        Me.GroupControl8.Size = New System.Drawing.Size(1332, 215)
        Me.GroupControl8.TabIndex = 30
        Me.GroupControl8.Text = "Listado de Jornadas Laborales"
        '
        'gdListaJornadaLaboral
        '
        Me.gdListaJornadaLaboral.Cursor = System.Windows.Forms.Cursors.Default
        Me.gdListaJornadaLaboral.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdListaJornadaLaboral.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode4.RelationName = "Level1"
        Me.gdListaJornadaLaboral.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode4})
        Me.gdListaJornadaLaboral.Location = New System.Drawing.Point(2, 52)
        Me.gdListaJornadaLaboral.MainView = Me.GridView4
        Me.gdListaJornadaLaboral.Margin = New System.Windows.Forms.Padding(4)
        Me.gdListaJornadaLaboral.MenuManager = Me.RibbonControl
        Me.gdListaJornadaLaboral.Name = "gdListaJornadaLaboral"
        Me.gdListaJornadaLaboral.Size = New System.Drawing.Size(1328, 161)
        Me.gdListaJornadaLaboral.TabIndex = 0
        Me.gdListaJornadaLaboral.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView4, Me.GridView5})
        '
        'GridView4
        '
        Me.GridView4.DetailHeight = 431
        Me.GridView4.GridControl = Me.gdListaJornadaLaboral
        Me.GridView4.Name = "GridView4"
        Me.GridView4.OptionsBehavior.Editable = False
        Me.GridView4.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.GridView4.OptionsView.ShowGroupPanel = False
        '
        'GridView5
        '
        Me.GridView5.DetailHeight = 431
        Me.GridView5.GridControl = Me.gdListaJornadaLaboral
        Me.GridView5.Name = "GridView5"
        '
        'CheckEdit2
        '
        Me.CheckEdit2.Dock = System.Windows.Forms.DockStyle.Top
        Me.CheckEdit2.EditValue = True
        Me.CheckEdit2.Location = New System.Drawing.Point(2, 28)
        Me.CheckEdit2.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckEdit2.MenuManager = Me.RibbonControl
        Me.CheckEdit2.Name = "CheckEdit2"
        Me.CheckEdit2.Properties.Caption = "Jornadas Asociadas Activas"
        Me.CheckEdit2.Size = New System.Drawing.Size(1328, 24)
        Me.CheckEdit2.TabIndex = 2
        '
        'ImageCollection1
        '
        Me.ImageCollection1.ImageStream = CType(resources.GetObject("ImageCollection1.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.ImageCollection1.Images.SetKeyName(0, "off")
        Me.ImageCollection1.Images.SetKeyName(1, "on")
        '
        'frmOperador
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1338, 882)
        Me.Controls.Add(Me.TabDatos)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmOperador"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Mantenimiento de operador"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanDatosMotivoAnulacion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanDatosMotivoAnulacion.ResumeLayout(False)
        Me.PanDatosMotivoAnulacion.PerformLayout()
        CType(Me.picEstadoOp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.chkMontacarga.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkVerifica.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPickea.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkTransporta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkUbica.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRecibe.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbJornada.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbEmpresa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picFoto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdRolOperador.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreOperador.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCostoHora, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtClave.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodigo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTelefono.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDireccion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtApellidos.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombres.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpEmpresaTB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpEmpresaTB.ResumeLayout(False)
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsOperador, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dkOperador, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerBottom.ResumeLayout(False)
        Me.DockPanel1.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        Me.DockPanel1_Container.PerformLayout()
        CType(Me.TabDatos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabDatos.ResumeLayout(False)
        Me.XtraTabPage1.ResumeLayout(False)
        Me.TabOperadorBodega.ResumeLayout(False)
        Me.ResolucionesLP.ResumeLayout(False)
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel1.ResumeLayout(False)
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.GroupControl2.PerformLayout()
        Me.ToolStripPR.ResumeLayout(False)
        Me.ToolStripPR.PerformLayout()
        CType(Me.txtCorrelativoActual, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCorrelativoFinal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkResolucionLPActiva.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCorrelativoInicial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl5.ResumeLayout(False)
        CType(Me.dGridPresentacion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrdPresentacion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivoPR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabPage2.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl4.ResumeLayout(False)
        Me.GroupControl4.PerformLayout()
        CType(Me.dgridTramosZonaPicking, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvTramosPorZona, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbZonaPicking.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.GroupControl6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl6.ResumeLayout(False)
        CType(Me.DgridZonaPickingOperador, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xTabJornadaLaboral.ResumeLayout(False)
        CType(Me.SplitContainerControl2.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl2.Panel1.ResumeLayout(False)
        CType(Me.SplitContainerControl2.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl2.ResumeLayout(False)
        CType(Me.GroupControl7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl7.ResumeLayout(False)
        Me.GroupControl7.PerformLayout()
        Me.ToolStrip2.ResumeLayout(False)
        Me.ToolStrip2.PerformLayout()
        CType(Me.cmbJornadaLaboral.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CheckEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl8.ResumeLayout(False)
        CType(Me.gdListaJornadaLaboral, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CheckEdit2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImageCollection1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents PanDatosMotivoAnulacion As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtNombres As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblCodigo As System.Windows.Forms.Label
    Friend WithEvents GrpEmpresaTB As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Private WithEvents Grid As DevExpress.XtraGrid.GridControl
    Private WithEvents gridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colSeleccion As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents colIdBodega As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IdOperadorBodega As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBodega As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IdInterno As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents txtApellidos As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtClave As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCodigo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtTelefono As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtDireccion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCostoHora As System.Windows.Forms.NumericUpDown
    Friend WithEvents DataBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DsOperador As DsOperador
    Friend WithEvents cmdImprimeCarnet As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lnkRolOperador As System.Windows.Forms.LinkLabel
    Friend WithEvents txtIdRolOperador As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNombreOperador As DevExpress.XtraEditors.TextEdit
    Friend WithEvents picFoto As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents Fec_agrDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_agrTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_modDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_modTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnExaminar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents dkOperador As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents TabDatos As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents XtraTabPage1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents TabOperadorBodega As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents cmbEmpresa As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbJornada As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents mnuCapturarFoto As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents CameraControl1 As DevExpress.XtraEditors.Camera.CameraControl
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents chkVerifica As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkPickea As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkTransporta As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkUbica As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkRecibe As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents picEstadoOp As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents ImageCollection1 As DevExpress.Utils.ImageCollection
    Friend WithEvents dtpUltimaSesion As DateTimePicker
    Friend WithEvents ResolucionesLP As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtCorrelativoActual As NumericUpDown
    Friend WithEvents txtCorrelativoFinal As NumericUpDown
    Friend WithEvents cmbBodega As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblIdResolucionLP As Label
    Friend WithEvents chkResolucionLPActiva As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtCorrelativoInicial As NumericUpDown
    Friend WithEvents txtNoSerie As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ToolStripPR As ToolStrip
    Friend WithEvents cmdNewPR As ToolStripButton
    Friend WithEvents cmdSavePR As ToolStripButton
    Friend WithEvents cmdDesactivarResolucion As ToolStripButton
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents GroupControl5 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dGridPresentacion As DevExpress.XtraGrid.GridControl
    Friend WithEvents GrdPresentacion As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView6 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents chkActivoPR As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents XtraTabPage2 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GroupControl4 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents mnuNuevoTramo As ToolStripButton
    Friend WithEvents mnuGuardarTramo As ToolStripButton
    Friend WithEvents mnuEliminarTramo As ToolStripButton
    Friend WithEvents chkActivo As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents chkUsaHH As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents RibbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents chkLunes As CheckBox
    Friend WithEvents chkMartes As CheckBox
    Friend WithEvents chkMiercoles As CheckBox
    Friend WithEvents chkDomingo As CheckBox
    Friend WithEvents chkJueves As CheckBox
    Friend WithEvents chkViernes As CheckBox
    Friend WithEvents chkSabado As CheckBox
    Friend WithEvents GroupControl6 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents DgridZonaPickingOperador As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cmbZonaPicking As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblIdZonaPickingTramoOp As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dgridTramosZonaPicking As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvTramosPorZona As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents chkMontacarga As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents xTabJornadaLaboral As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents SplitContainerControl2 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents GroupControl7 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ToolStrip2 As ToolStrip
    Friend WithEvents tbGuardar As ToolStripButton
    Friend WithEvents ToolStripButton3 As ToolStripButton
    Friend WithEvents cmbJornadaLaboral As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents CheckEdit1 As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents GroupControl8 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents gdListaJornadaLaboral As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView4 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView5 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CheckEdit2 As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkSistema As DevExpress.XtraBars.BarToggleSwitchItem
End Class
