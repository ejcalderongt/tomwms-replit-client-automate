<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAjusteStock
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If components IsNot Nothing Then
                    components.Dispose()
                End If

                If Stock IsNot Nothing Then
                    Stock.Dispose()
                    Stock = Nothing
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
        Dim Label1 As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim Label5 As System.Windows.Forms.Label
        Dim Label23 As System.Windows.Forms.Label
        Dim Label7 As System.Windows.Forms.Label
        Dim User_agrLabel As System.Windows.Forms.Label
        Dim User_modLabel As System.Windows.Forms.Label
        Dim Fec_agrLabel As System.Windows.Forms.Label
        Dim Fec_modLabel As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim lblBodegaERP As System.Windows.Forms.Label
        Dim Label8 As System.Windows.Forms.Label
        Dim lblPropietario As System.Windows.Forms.Label
        Dim lblSerie As System.Windows.Forms.Label
        Dim Label9 As System.Windows.Forms.Label
        Dim Label10 As System.Windows.Forms.Label
        Dim Label11 As System.Windows.Forms.Label
        Dim Label12 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAjusteStock))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.lblCentroCosto = New System.Windows.Forms.Label()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView5 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuImprimir1 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEstadoEnviadoAERP = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuReimpresionEtiquetas = New DevExpress.XtraBars.BarButtonItem()
        Me.chkAuditado = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.chkBorrador = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.btnImportarExcel = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.lblRegs = New DevExpress.XtraBars.BarStaticItem()
        Me.DockPanel2 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.ControlContainer1 = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.DateEdit1 = New DevExpress.XtraEditors.DateEdit()
        Me.DateEdit2 = New DevExpress.XtraEditors.DateEdit()
        Me.TextEdit1 = New DevExpress.XtraEditors.TextEdit()
        Me.TextEdit2 = New DevExpress.XtraEditors.TextEdit()
        Me.ToolStripP = New System.Windows.Forms.ToolStrip()
        Me.cmdAdd = New System.Windows.Forms.ToolStripButton()
        Me.mnuAjustePositivo = New System.Windows.Forms.ToolStripButton()
        Me.mnuDel = New System.Windows.Forms.ToolStripButton()
        Me.mnuDividir = New System.Windows.Forms.ToolStripButton()
        Me.dtpFecha = New DevExpress.XtraEditors.DateEdit()
        Me.txtReferencia = New System.Windows.Forms.TextBox()
        Me.GridView6 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.gcCentroCosto = New DevExpress.XtraEditors.GroupControl()
        Me.txtCentroCostoDepERP = New DevExpress.XtraEditors.TextEdit()
        Me.txtCentroCostoERP = New DevExpress.XtraEditors.TextEdit()
        Me.txtCentroCostoDirERP = New DevExpress.XtraEditors.TextEdit()
        Me.cmbTipoAjuste = New DevExpress.XtraEditors.LookUpEdit()
        Me.lcmbCentroCosto = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtSerie = New DevExpress.XtraEditors.TextEdit()
        Me.cmbPropietarioBodega = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbProductoFamilia = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbBodegaERP = New DevExpress.XtraEditors.LookUpEdit()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.GroupControl4 = New DevExpress.XtraEditors.GroupControl()
        Me.dgrid = New System.Windows.Forms.DataGridView()
        Me.ColCodigoProducto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colNombreProducto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UmBas = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPresentacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colUbicacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.motivoajuste = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.tipoajuste = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.CantidadP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColCantidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColDiferencia = New System.Windows.Forms.DataGridViewImageColumn()
        Me.colLote = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColObservacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColEnviadoAErp = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ColIdAjusteDEt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LoteOrig = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColBodega = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.ColLicPlate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTalla = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.colColor = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.colIdProductoTallaColor = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.AutoHideContainer2 = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.Fec_modDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.Fec_agrDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_modTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.User_agrTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.XtraTabControl1 = New DevExpress.XtraTab.XtraTabControl()
        Me.XtraTabPage1 = New DevExpress.XtraTab.XtraTabPage()
        Me.XtraTabPage2 = New DevExpress.XtraTab.XtraTabPage()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.grdDocsAsociados = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Label1 = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        Label5 = New System.Windows.Forms.Label()
        Label23 = New System.Windows.Forms.Label()
        Label7 = New System.Windows.Forms.Label()
        User_agrLabel = New System.Windows.Forms.Label()
        User_modLabel = New System.Windows.Forms.Label()
        Fec_agrLabel = New System.Windows.Forms.Label()
        Fec_modLabel = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        lblBodegaERP = New System.Windows.Forms.Label()
        Label8 = New System.Windows.Forms.Label()
        lblPropietario = New System.Windows.Forms.Label()
        lblSerie = New System.Windows.Forms.Label()
        Label9 = New System.Windows.Forms.Label()
        Label10 = New System.Windows.Forms.Label()
        Label11 = New System.Windows.Forms.Label()
        Label12 = New System.Windows.Forms.Label()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DockPanel2.SuspendLayout()
        Me.ControlContainer1.SuspendLayout()
        CType(Me.DateEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateEdit1.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateEdit2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateEdit2.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEdit2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStripP.SuspendLayout()
        CType(Me.dtpFecha.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFecha.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.gcCentroCosto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gcCentroCosto.SuspendLayout()
        CType(Me.txtCentroCostoDepERP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCentroCostoERP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCentroCostoDirERP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTipoAjuste.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lcmbCentroCosto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPropietarioBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbProductoFamilia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBodegaERP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl4.SuspendLayout()
        CType(Me.dgrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.AutoHideContainer2.SuspendLayout()
        Me.DockPanel1.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabControl1.SuspendLayout()
        Me.XtraTabPage1.SuspendLayout()
        Me.XtraTabPage2.SuspendLayout()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.grdDocsAsociados, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(261, 25)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(96, 16)
        Label1.TabIndex = 2
        Label1.Text = "Fecha Agregó:"
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.Location = New System.Drawing.Point(536, 25)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(111, 16)
        Label3.TabIndex = 4
        Label3.Text = "Usuario Modificó:"
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(14, 21)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(105, 16)
        Label4.TabIndex = 0
        Label4.Text = "Usuario Agregó:"
        '
        'Label5
        '
        Label5.AutoSize = True
        Label5.Location = New System.Drawing.Point(787, 25)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(102, 16)
        Label5.TabIndex = 5
        Label5.Text = "Fecha Modificó:"
        '
        'Label23
        '
        Label23.AutoSize = True
        Label23.Location = New System.Drawing.Point(333, 42)
        Label23.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label23.Name = "Label23"
        Label23.Size = New System.Drawing.Size(50, 16)
        Label23.TabIndex = 11
        Label23.Text = "Fecha :"
        '
        'Label7
        '
        Label7.AutoSize = True
        Label7.Location = New System.Drawing.Point(333, 76)
        Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label7.Name = "Label7"
        Label7.Size = New System.Drawing.Size(73, 16)
        Label7.TabIndex = 17
        Label7.Text = "Referencia:"
        '
        'User_agrLabel
        '
        User_agrLabel.AutoSize = True
        User_agrLabel.Location = New System.Drawing.Point(31, 21)
        User_agrLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        User_agrLabel.Name = "User_agrLabel"
        User_agrLabel.Size = New System.Drawing.Size(100, 16)
        User_agrLabel.TabIndex = 9
        User_agrLabel.Text = "Usuario Agregó:"
        '
        'User_modLabel
        '
        User_modLabel.AutoSize = True
        User_modLabel.Location = New System.Drawing.Point(408, 23)
        User_modLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        User_modLabel.Name = "User_modLabel"
        User_modLabel.Size = New System.Drawing.Size(106, 16)
        User_modLabel.TabIndex = 16
        User_modLabel.Text = "Usuario Modificó:"
        '
        'Fec_agrLabel
        '
        Fec_agrLabel.AutoSize = True
        Fec_agrLabel.Location = New System.Drawing.Point(31, 60)
        Fec_agrLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Fec_agrLabel.Name = "Fec_agrLabel"
        Fec_agrLabel.Size = New System.Drawing.Size(91, 16)
        Fec_agrLabel.TabIndex = 18
        Fec_agrLabel.Text = "Fecha Agregó:"
        '
        'Fec_modLabel
        '
        Fec_modLabel.AutoSize = True
        Fec_modLabel.Location = New System.Drawing.Point(353, 48)
        Fec_modLabel.Name = "Fec_modLabel"
        Fec_modLabel.Size = New System.Drawing.Size(97, 16)
        Fec_modLabel.TabIndex = 13
        Fec_modLabel.Text = "Fecha Modificó:"
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(408, 60)
        Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(97, 16)
        Label2.TabIndex = 20
        Label2.Text = "Fecha Modificó:"
        '
        'lblBodegaERP
        '
        lblBodegaERP.AutoSize = True
        lblBodegaERP.Location = New System.Drawing.Point(8, 76)
        lblBodegaERP.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblBodegaERP.Name = "lblBodegaERP"
        lblBodegaERP.Size = New System.Drawing.Size(54, 16)
        lblBodegaERP.TabIndex = 32
        lblBodegaERP.Text = "Bodega:"
        '
        'Label8
        '
        Label8.AutoSize = True
        Label8.Location = New System.Drawing.Point(670, 42)
        Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label8.Name = "Label8"
        Label8.Size = New System.Drawing.Size(56, 16)
        Label8.TabIndex = 36
        Label8.Text = "Sección:"
        '
        'lblPropietario
        '
        lblPropietario.AutoSize = True
        lblPropietario.Location = New System.Drawing.Point(8, 42)
        lblPropietario.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblPropietario.Name = "lblPropietario"
        lblPropietario.Size = New System.Drawing.Size(74, 16)
        lblPropietario.TabIndex = 38
        lblPropietario.Text = "Propietario:"
        '
        'lblSerie
        '
        lblSerie.AutoSize = True
        lblSerie.Location = New System.Drawing.Point(670, 76)
        lblSerie.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblSerie.Name = "lblSerie"
        lblSerie.Size = New System.Drawing.Size(42, 16)
        lblSerie.TabIndex = 40
        lblSerie.Text = "Serie:"
        '
        'Label9
        '
        Label9.AutoSize = True
        Label9.Location = New System.Drawing.Point(930, 41)
        Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label9.Name = "Label9"
        Label9.Size = New System.Drawing.Size(77, 16)
        Label9.TabIndex = 46
        Label9.Text = "Tipo Ajuste:"
        '
        'Label10
        '
        Label10.AutoSize = True
        Label10.Location = New System.Drawing.Point(668, 17)
        Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label10.Name = "Label10"
        Label10.Size = New System.Drawing.Size(93, 16)
        Label10.TabIndex = 51
        Label10.Text = "Departamento:"
        '
        'Label11
        '
        Label11.AutoSize = True
        Label11.Location = New System.Drawing.Point(6, 17)
        Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label11.Name = "Label11"
        Label11.Size = New System.Drawing.Size(86, 16)
        Label11.TabIndex = 49
        Label11.Text = "Centro Costo:"
        '
        'Label12
        '
        Label12.AutoSize = True
        Label12.Location = New System.Drawing.Point(331, 17)
        Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label12.Name = "Label12"
        Label12.Size = New System.Drawing.Size(64, 16)
        Label12.TabIndex = 47
        Label12.Text = "Dirección:"
        '
        'lblCentroCosto
        '
        Me.lblCentroCosto.AutoSize = True
        Me.lblCentroCosto.Location = New System.Drawing.Point(930, 77)
        Me.lblCentroCosto.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCentroCosto.Name = "lblCentroCosto"
        Me.lblCentroCosto.Size = New System.Drawing.Size(86, 16)
        Me.lblCentroCosto.TabIndex = 44
        Me.lblCentroCosto.Text = "Centro Costo:"
        '
        'GridView2
        '
        Me.GridView2.Name = "GridView2"
        '
        'GridView5
        '
        Me.GridView5.Name = "GridView5"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuImprimir1, Me.mnuEstadoEnviadoAERP, Me.mnuReimpresionEtiquetas, Me.chkAuditado, Me.mnuGuardar, Me.chkBorrador, Me.btnImportarExcel})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(4)
        Me.RibbonControl.MaxItemId = 18
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1484, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'mnuImprimir1
        '
        Me.mnuImprimir1.Caption = "Imprimir"
        Me.mnuImprimir1.Id = 6
        Me.mnuImprimir1.ImageOptions.SvgImage = CType(resources.GetObject("mnuImprimir1.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuImprimir1.Name = "mnuImprimir1"
        '
        'mnuEstadoEnviadoAERP
        '
        Me.mnuEstadoEnviadoAERP.Caption = "No enviado"
        Me.mnuEstadoEnviadoAERP.Id = 8
        Me.mnuEstadoEnviadoAERP.ImageOptions.SvgImage = CType(resources.GetObject("mnuEstadoEnviadoAERP.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEstadoEnviadoAERP.Name = "mnuEstadoEnviadoAERP"
        '
        'mnuReimpresionEtiquetas
        '
        Me.mnuReimpresionEtiquetas.Caption = "Reimpresión etiquetas"
        Me.mnuReimpresionEtiquetas.Id = 9
        Me.mnuReimpresionEtiquetas.ImageOptions.SvgImage = CType(resources.GetObject("mnuReimpresionEtiquetas.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuReimpresionEtiquetas.Name = "mnuReimpresionEtiquetas"
        '
        'chkAuditado
        '
        Me.chkAuditado.Caption = "Auditado"
        Me.chkAuditado.Id = 11
        Me.chkAuditado.Name = "chkAuditado"
        '
        'mnuGuardar
        '
        Me.mnuGuardar.Caption = "Guardar"
        Me.mnuGuardar.Id = 12
        Me.mnuGuardar.ImageOptions.SvgImage = CType(resources.GetObject("mnuGuardar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuGuardar.Name = "mnuGuardar"
        '
        'chkBorrador
        '
        Me.chkBorrador.Caption = "Borrador"
        Me.chkBorrador.Id = 16
        Me.chkBorrador.Name = "chkBorrador"
        '
        'btnImportarExcel
        '
        Me.btnImportarExcel.Caption = "Importar"
        Me.btnImportarExcel.Id = 17
        Me.btnImportarExcel.ImageOptions.SvgImage = CType(resources.GetObject("btnImportarExcel.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnImportarExcel.Name = "btnImportarExcel"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Ajuste de Stock"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuImprimir1)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEstadoEnviadoAERP)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuReimpresionEtiquetas)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.chkAuditado)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.chkBorrador)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.btnImportarExcel)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblRegs)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(2, 377)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1478, 33)
        '
        'lblRegs
        '
        Me.lblRegs.Caption = "Registros: 0"
        Me.lblRegs.Id = 5
        Me.lblRegs.Name = "lblRegs"
        '
        'DockPanel2
        '
        Me.DockPanel2.Controls.Add(Me.ControlContainer1)
        Me.DockPanel2.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel2.ID = New System.Guid("3e166ed0-67ce-442a-85ea-6fd5a948d40f")
        Me.DockPanel2.Location = New System.Drawing.Point(0, 0)
        Me.DockPanel2.Name = "DockPanel2"
        Me.DockPanel2.OriginalSize = New System.Drawing.Size(200, 96)
        Me.DockPanel2.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel2.SavedIndex = 0
        Me.DockPanel2.Size = New System.Drawing.Size(1064, 96)
        '
        'ControlContainer1
        '
        Me.ControlContainer1.Controls.Add(Me.DateEdit1)
        Me.ControlContainer1.Controls.Add(Label1)
        Me.ControlContainer1.Controls.Add(Me.DateEdit2)
        Me.ControlContainer1.Controls.Add(Me.TextEdit1)
        Me.ControlContainer1.Controls.Add(Label3)
        Me.ControlContainer1.Controls.Add(Label4)
        Me.ControlContainer1.Controls.Add(Me.TextEdit2)
        Me.ControlContainer1.Controls.Add(Label5)
        Me.ControlContainer1.Location = New System.Drawing.Point(4, 23)
        Me.ControlContainer1.Name = "ControlContainer1"
        Me.ControlContainer1.Size = New System.Drawing.Size(1056, 69)
        Me.ControlContainer1.TabIndex = 0
        '
        'DateEdit1
        '
        Me.DateEdit1.EditValue = Nothing
        Me.DateEdit1.Enabled = False
        Me.DateEdit1.Location = New System.Drawing.Point(345, 18)
        Me.DateEdit1.MenuManager = Me.RibbonControl
        Me.DateEdit1.Name = "DateEdit1"
        Me.DateEdit1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DateEdit1.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DateEdit1.Size = New System.Drawing.Size(150, 22)
        Me.DateEdit1.TabIndex = 3
        '
        'DateEdit2
        '
        Me.DateEdit2.EditValue = Nothing
        Me.DateEdit2.Enabled = False
        Me.DateEdit2.Location = New System.Drawing.Point(875, 18)
        Me.DateEdit2.MenuManager = Me.RibbonControl
        Me.DateEdit2.Name = "DateEdit2"
        Me.DateEdit2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DateEdit2.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DateEdit2.Size = New System.Drawing.Size(150, 22)
        Me.DateEdit2.TabIndex = 6
        '
        'TextEdit1
        '
        Me.TextEdit1.Enabled = False
        Me.TextEdit1.Location = New System.Drawing.Point(102, 18)
        Me.TextEdit1.MenuManager = Me.RibbonControl
        Me.TextEdit1.Name = "TextEdit1"
        Me.TextEdit1.Size = New System.Drawing.Size(150, 22)
        Me.TextEdit1.TabIndex = 1
        '
        'TextEdit2
        '
        Me.TextEdit2.Enabled = False
        Me.TextEdit2.Location = New System.Drawing.Point(631, 22)
        Me.TextEdit2.MenuManager = Me.RibbonControl
        Me.TextEdit2.Name = "TextEdit2"
        Me.TextEdit2.Size = New System.Drawing.Size(150, 22)
        Me.TextEdit2.TabIndex = 7
        '
        'ToolStripP
        '
        Me.ToolStripP.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStripP.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdAdd, Me.mnuAjustePositivo, Me.mnuDel, Me.mnuDividir})
        Me.ToolStripP.Location = New System.Drawing.Point(2, 28)
        Me.ToolStripP.Name = "ToolStripP"
        Me.ToolStripP.Size = New System.Drawing.Size(1478, 27)
        Me.ToolStripP.TabIndex = 2
        Me.ToolStripP.Text = "ToolStrip1"
        '
        'cmdAdd
        '
        Me.cmdAdd.Image = Global.TOMWMS.My.Resources.Resources.add
        Me.cmdAdd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(155, 24)
        Me.cmdAdd.Text = "Ajuste sobre Stock"
        Me.cmdAdd.ToolTipText = "Se modificara stock existente"
        '
        'mnuAjustePositivo
        '
        Me.mnuAjustePositivo.Image = Global.TOMWMS.My.Resources.Resources.add
        Me.mnuAjustePositivo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuAjustePositivo.Name = "mnuAjustePositivo"
        Me.mnuAjustePositivo.Size = New System.Drawing.Size(138, 24)
        Me.mnuAjustePositivo.Text = "Ajuste Sin Stock"
        Me.mnuAjustePositivo.ToolTipText = "Se agrega existencia sin stock previa"
        '
        'mnuDel
        '
        Me.mnuDel.Image = Global.TOMWMS.My.Resources.Resources.desactivar
        Me.mnuDel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuDel.Name = "mnuDel"
        Me.mnuDel.Size = New System.Drawing.Size(87, 24)
        Me.mnuDel.Text = "Eliminar"
        Me.mnuDel.ToolTipText = "Eliminar Linea Seleccionada"
        '
        'mnuDividir
        '
        Me.mnuDividir.Image = Global.TOMWMS.My.Resources.Resources.ubic03
        Me.mnuDividir.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuDividir.Name = "mnuDividir"
        Me.mnuDividir.Size = New System.Drawing.Size(77, 24)
        Me.mnuDividir.Text = "Dividir"
        '
        'dtpFecha
        '
        Me.dtpFecha.EditValue = New Date(2017, 11, 20, 9, 8, 7, 372)
        Me.dtpFecha.Location = New System.Drawing.Point(415, 38)
        Me.dtpFecha.Margin = New System.Windows.Forms.Padding(4)
        Me.dtpFecha.MenuManager = Me.RibbonControl
        Me.dtpFecha.Name = "dtpFecha"
        Me.dtpFecha.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.dtpFecha.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpFecha.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpFecha.Size = New System.Drawing.Size(244, 22)
        Me.dtpFecha.TabIndex = 28
        '
        'txtReferencia
        '
        Me.txtReferencia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReferencia.Location = New System.Drawing.Point(415, 74)
        Me.txtReferencia.Margin = New System.Windows.Forms.Padding(4)
        Me.txtReferencia.MaxLength = 50
        Me.txtReferencia.Multiline = True
        Me.txtReferencia.Name = "txtReferencia"
        Me.txtReferencia.Size = New System.Drawing.Size(243, 26)
        Me.txtReferencia.TabIndex = 18
        '
        'GridView6
        '
        Me.GridView6.Name = "GridView6"
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.lblCentroCosto)
        Me.GroupControl2.Controls.Add(Me.gcCentroCosto)
        Me.GroupControl2.Controls.Add(Label9)
        Me.GroupControl2.Controls.Add(Me.cmbTipoAjuste)
        Me.GroupControl2.Controls.Add(Me.lcmbCentroCosto)
        Me.GroupControl2.Controls.Add(Me.txtSerie)
        Me.GroupControl2.Controls.Add(lblSerie)
        Me.GroupControl2.Controls.Add(Me.cmbPropietarioBodega)
        Me.GroupControl2.Controls.Add(lblPropietario)
        Me.GroupControl2.Controls.Add(Me.cmbProductoFamilia)
        Me.GroupControl2.Controls.Add(Label8)
        Me.GroupControl2.Controls.Add(Me.cmbBodegaERP)
        Me.GroupControl2.Controls.Add(lblBodegaERP)
        Me.GroupControl2.Controls.Add(Me.PictureBox3)
        Me.GroupControl2.Controls.Add(Me.PictureBox2)
        Me.GroupControl2.Controls.Add(Me.PictureBox1)
        Me.GroupControl2.Controls.Add(Label23)
        Me.GroupControl2.Controls.Add(Me.txtReferencia)
        Me.GroupControl2.Controls.Add(Me.dtpFecha)
        Me.GroupControl2.Controls.Add(Label7)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl2.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(1482, 167)
        Me.GroupControl2.TabIndex = 30
        '
        'gcCentroCosto
        '
        Me.gcCentroCosto.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.gcCentroCosto.Controls.Add(Me.txtCentroCostoDepERP)
        Me.gcCentroCosto.Controls.Add(Me.txtCentroCostoERP)
        Me.gcCentroCosto.Controls.Add(Label12)
        Me.gcCentroCosto.Controls.Add(Me.txtCentroCostoDirERP)
        Me.gcCentroCosto.Controls.Add(Label11)
        Me.gcCentroCosto.Controls.Add(Label10)
        Me.gcCentroCosto.Location = New System.Drawing.Point(1, 102)
        Me.gcCentroCosto.Name = "gcCentroCosto"
        Me.gcCentroCosto.Size = New System.Drawing.Size(921, 50)
        Me.gcCentroCosto.TabIndex = 55
        Me.gcCentroCosto.Text = "Centros de Costo"
        '
        'txtCentroCostoDepERP
        '
        Me.txtCentroCostoDepERP.Location = New System.Drawing.Point(761, 14)
        Me.txtCentroCostoDepERP.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCentroCostoDepERP.MenuManager = Me.RibbonControl
        Me.txtCentroCostoDepERP.Name = "txtCentroCostoDepERP"
        Me.txtCentroCostoDepERP.Properties.Appearance.BackColor = System.Drawing.Color.Lavender
        Me.txtCentroCostoDepERP.Properties.Appearance.Options.UseBackColor = True
        Me.txtCentroCostoDepERP.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtCentroCostoDepERP.Properties.ReadOnly = True
        Me.txtCentroCostoDepERP.Size = New System.Drawing.Size(156, 22)
        Me.txtCentroCostoDepERP.TabIndex = 52
        Me.txtCentroCostoDepERP.ToolTip = "Valor de departamento para ajuste tomado a partir de la bodega seleccionada"
        Me.txtCentroCostoDepERP.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information
        Me.txtCentroCostoDepERP.ToolTipTitle = "Bodega.Departamento"
        '
        'txtCentroCostoERP
        '
        Me.txtCentroCostoERP.Location = New System.Drawing.Point(93, 14)
        Me.txtCentroCostoERP.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCentroCostoERP.MenuManager = Me.RibbonControl
        Me.txtCentroCostoERP.Name = "txtCentroCostoERP"
        Me.txtCentroCostoERP.Properties.Appearance.BackColor = System.Drawing.Color.Lavender
        Me.txtCentroCostoERP.Properties.Appearance.Options.UseBackColor = True
        Me.txtCentroCostoERP.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtCentroCostoERP.Properties.ReadOnly = True
        Me.txtCentroCostoERP.Size = New System.Drawing.Size(230, 22)
        Me.txtCentroCostoERP.TabIndex = 54
        Me.txtCentroCostoERP.ToolTip = "Valor de centro de costo para ajuste tomado a partir de la bodega seleccionada"
        Me.txtCentroCostoERP.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information
        Me.txtCentroCostoERP.ToolTipTitle = "Bodega.CentroCosto"
        '
        'txtCentroCostoDirERP
        '
        Me.txtCentroCostoDirERP.Location = New System.Drawing.Point(413, 14)
        Me.txtCentroCostoDirERP.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCentroCostoDirERP.MenuManager = Me.RibbonControl
        Me.txtCentroCostoDirERP.Name = "txtCentroCostoDirERP"
        Me.txtCentroCostoDirERP.Properties.Appearance.BackColor = System.Drawing.Color.Lavender
        Me.txtCentroCostoDirERP.Properties.Appearance.Options.UseBackColor = True
        Me.txtCentroCostoDirERP.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtCentroCostoDirERP.Properties.ReadOnly = True
        Me.txtCentroCostoDirERP.Size = New System.Drawing.Size(243, 22)
        Me.txtCentroCostoDirERP.TabIndex = 53
        Me.txtCentroCostoDirERP.ToolTip = "Valor de dirección para ajuste tomado a partir de la bodega seleccionada"
        Me.txtCentroCostoDirERP.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information
        Me.txtCentroCostoDirERP.ToolTipTitle = "Bodega.Dirección"
        '
        'cmbTipoAjuste
        '
        Me.cmbTipoAjuste.Location = New System.Drawing.Point(1040, 36)
        Me.cmbTipoAjuste.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbTipoAjuste.Name = "cmbTipoAjuste"
        Me.cmbTipoAjuste.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbTipoAjuste.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTipoAjuste.Properties.NullText = ""
        Me.cmbTipoAjuste.Size = New System.Drawing.Size(187, 22)
        Me.cmbTipoAjuste.TabIndex = 45
        '
        'lcmbCentroCosto
        '
        Me.lcmbCentroCosto.Location = New System.Drawing.Point(1040, 74)
        Me.lcmbCentroCosto.Margin = New System.Windows.Forms.Padding(4)
        Me.lcmbCentroCosto.Name = "lcmbCentroCosto"
        Me.lcmbCentroCosto.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.lcmbCentroCosto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lcmbCentroCosto.Properties.NullText = ""
        Me.lcmbCentroCosto.Size = New System.Drawing.Size(431, 22)
        Me.lcmbCentroCosto.TabIndex = 43
        '
        'txtSerie
        '
        Me.txtSerie.Location = New System.Drawing.Point(763, 73)
        Me.txtSerie.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSerie.MenuManager = Me.RibbonControl
        Me.txtSerie.Name = "txtSerie"
        Me.txtSerie.Properties.Appearance.BackColor = System.Drawing.Color.Lavender
        Me.txtSerie.Properties.Appearance.Options.UseBackColor = True
        Me.txtSerie.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtSerie.Properties.ReadOnly = True
        Me.txtSerie.Size = New System.Drawing.Size(156, 22)
        Me.txtSerie.TabIndex = 42
        Me.txtSerie.ToolTip = "Valor de serie para ajuste tomado a partir de la bodega seleccionada"
        Me.txtSerie.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information
        Me.txtSerie.ToolTipTitle = "Bodega.Serie"
        '
        'cmbPropietarioBodega
        '
        Me.cmbPropietarioBodega.Location = New System.Drawing.Point(95, 38)
        Me.cmbPropietarioBodega.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbPropietarioBodega.Name = "cmbPropietarioBodega"
        Me.cmbPropietarioBodega.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbPropietarioBodega.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPropietarioBodega.Properties.NullText = ""
        Me.cmbPropietarioBodega.Size = New System.Drawing.Size(230, 22)
        Me.cmbPropietarioBodega.TabIndex = 39
        '
        'cmbProductoFamilia
        '
        Me.cmbProductoFamilia.Location = New System.Drawing.Point(763, 38)
        Me.cmbProductoFamilia.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbProductoFamilia.Name = "cmbProductoFamilia"
        Me.cmbProductoFamilia.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbProductoFamilia.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbProductoFamilia.Properties.NullText = ""
        Me.cmbProductoFamilia.Size = New System.Drawing.Size(156, 22)
        Me.cmbProductoFamilia.TabIndex = 37
        '
        'cmbBodegaERP
        '
        Me.cmbBodegaERP.AllowDrop = True
        Me.cmbBodegaERP.Location = New System.Drawing.Point(95, 73)
        Me.cmbBodegaERP.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbBodegaERP.Name = "cmbBodegaERP"
        Me.cmbBodegaERP.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbBodegaERP.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodegaERP.Properties.NullText = ""
        Me.cmbBodegaERP.Size = New System.Drawing.Size(230, 22)
        Me.cmbBodegaERP.TabIndex = 35
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(1381, 32)
        Me.PictureBox3.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(23, 25)
        Me.PictureBox3.TabIndex = 31
        Me.PictureBox3.TabStop = False
        Me.PictureBox3.Visible = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(1352, 32)
        Me.PictureBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(23, 25)
        Me.PictureBox2.TabIndex = 30
        Me.PictureBox2.TabStop = False
        Me.PictureBox2.Visible = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(1321, 32)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(23, 25)
        Me.PictureBox1.TabIndex = 29
        Me.PictureBox1.TabStop = False
        Me.PictureBox1.Visible = False
        '
        'GroupControl4
        '
        Me.GroupControl4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.GroupControl4.Controls.Add(Me.RibbonStatusBar)
        Me.GroupControl4.Controls.Add(Me.dgrid)
        Me.GroupControl4.Controls.Add(Me.ToolStripP)
        Me.GroupControl4.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl4.Location = New System.Drawing.Point(0, 167)
        Me.GroupControl4.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl4.Name = "GroupControl4"
        Me.GroupControl4.Size = New System.Drawing.Size(1482, 412)
        Me.GroupControl4.TabIndex = 31
        Me.GroupControl4.Text = "Detalle"
        '
        'dgrid
        '
        Me.dgrid.AllowUserToAddRows = False
        Me.dgrid.AllowUserToDeleteRows = False
        Me.dgrid.AllowUserToResizeRows = False
        Me.dgrid.BackgroundColor = System.Drawing.Color.PaleTurquoise
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 7.8!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.MidnightBlue
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgrid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgrid.ColumnHeadersHeight = 40
        Me.dgrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColCodigoProducto, Me.colNombreProducto, Me.UmBas, Me.colPresentacion, Me.colUbicacion, Me.motivoajuste, Me.tipoajuste, Me.CantidadP, Me.ColCantidad, Me.ColDiferencia, Me.colLote, Me.ColObservacion, Me.ColEnviadoAErp, Me.ColIdAjusteDEt, Me.LoteOrig, Me.ColBodega, Me.ColLicPlate, Me.colTalla, Me.colColor, Me.colIdProductoTallaColor})
        Me.dgrid.Dock = System.Windows.Forms.DockStyle.Top
        Me.dgrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.dgrid.EnableHeadersVisualStyles = False
        Me.dgrid.GridColor = System.Drawing.Color.Navy
        Me.dgrid.Location = New System.Drawing.Point(2, 55)
        Me.dgrid.Margin = New System.Windows.Forms.Padding(4)
        Me.dgrid.MultiSelect = False
        Me.dgrid.Name = "dgrid"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Tahoma", 7.8!)
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgrid.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgrid.RowHeadersVisible = False
        Me.dgrid.RowHeadersWidth = 60
        Me.dgrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgrid.ShowCellErrors = False
        Me.dgrid.ShowCellToolTips = False
        Me.dgrid.ShowEditingIcon = False
        Me.dgrid.ShowRowErrors = False
        Me.dgrid.Size = New System.Drawing.Size(1478, 316)
        Me.dgrid.TabIndex = 3
        '
        'ColCodigoProducto
        '
        Me.ColCodigoProducto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.ColCodigoProducto.HeaderText = "Codigo"
        Me.ColCodigoProducto.MinimumWidth = 6
        Me.ColCodigoProducto.Name = "ColCodigoProducto"
        Me.ColCodigoProducto.Width = 90
        '
        'colNombreProducto
        '
        Me.colNombreProducto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colNombreProducto.HeaderText = "Producto"
        Me.colNombreProducto.MinimumWidth = 6
        Me.colNombreProducto.Name = "colNombreProducto"
        Me.colNombreProducto.ReadOnly = True
        Me.colNombreProducto.Width = 200
        '
        'UmBas
        '
        Me.UmBas.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.UmBas.HeaderText = "UmBas"
        Me.UmBas.MinimumWidth = 6
        Me.UmBas.Name = "UmBas"
        Me.UmBas.Width = 75
        '
        'colPresentacion
        '
        Me.colPresentacion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.colPresentacion.HeaderText = "Presentación"
        Me.colPresentacion.MinimumWidth = 6
        Me.colPresentacion.Name = "colPresentacion"
        Me.colPresentacion.ReadOnly = True
        Me.colPresentacion.Width = 109
        '
        'colUbicacion
        '
        Me.colUbicacion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colUbicacion.HeaderText = "Ubicacion"
        Me.colUbicacion.MinimumWidth = 6
        Me.colUbicacion.Name = "colUbicacion"
        Me.colUbicacion.ReadOnly = True
        Me.colUbicacion.Width = 250
        '
        'motivoajuste
        '
        Me.motivoajuste.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.motivoajuste.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.motivoajuste.HeaderText = "Motivo"
        Me.motivoajuste.MinimumWidth = 6
        Me.motivoajuste.Name = "motivoajuste"
        Me.motivoajuste.Width = 150
        '
        'tipoajuste
        '
        Me.tipoajuste.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.tipoajuste.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.tipoajuste.HeaderText = "Tipo Ajuste"
        Me.tipoajuste.MinimumWidth = 6
        Me.tipoajuste.Name = "tipoajuste"
        Me.tipoajuste.Width = 150
        '
        'CantidadP
        '
        Me.CantidadP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "N6"
        DataGridViewCellStyle2.NullValue = "0"
        Me.CantidadP.DefaultCellStyle = DataGridViewCellStyle2
        Me.CantidadP.HeaderText = "Existencia"
        Me.CantidadP.MinimumWidth = 6
        Me.CantidadP.Name = "CantidadP"
        Me.CantidadP.ReadOnly = True
        Me.CantidadP.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.CantidadP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CantidadP.Width = 125
        '
        'ColCantidad
        '
        Me.ColCantidad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N6"
        DataGridViewCellStyle3.NullValue = "0"
        Me.ColCantidad.DefaultCellStyle = DataGridViewCellStyle3
        Me.ColCantidad.HeaderText = "Valor Actual"
        Me.ColCantidad.MinimumWidth = 6
        Me.ColCantidad.Name = "ColCantidad"
        Me.ColCantidad.Width = 150
        '
        'ColDiferencia
        '
        Me.ColDiferencia.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.ColDiferencia.HeaderText = "Diferencia"
        Me.ColDiferencia.MinimumWidth = 6
        Me.ColDiferencia.Name = "ColDiferencia"
        Me.ColDiferencia.Visible = False
        Me.ColDiferencia.Width = 70
        '
        'colLote
        '
        Me.colLote.HeaderText = "Lote"
        Me.colLote.MinimumWidth = 6
        Me.colLote.Name = "colLote"
        Me.colLote.Width = 150
        '
        'ColObservacion
        '
        Me.ColObservacion.HeaderText = "Observacion"
        Me.ColObservacion.MinimumWidth = 6
        Me.ColObservacion.Name = "ColObservacion"
        Me.ColObservacion.Width = 250
        '
        'ColEnviadoAErp
        '
        Me.ColEnviadoAErp.HeaderText = "Enviado"
        Me.ColEnviadoAErp.MinimumWidth = 6
        Me.ColEnviadoAErp.Name = "ColEnviadoAErp"
        Me.ColEnviadoAErp.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ColEnviadoAErp.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.ColEnviadoAErp.Width = 125
        '
        'ColIdAjusteDEt
        '
        Me.ColIdAjusteDEt.HeaderText = "IdAjusteDet"
        Me.ColIdAjusteDEt.MinimumWidth = 6
        Me.ColIdAjusteDEt.Name = "ColIdAjusteDEt"
        Me.ColIdAjusteDEt.ReadOnly = True
        Me.ColIdAjusteDEt.Visible = False
        Me.ColIdAjusteDEt.Width = 125
        '
        'LoteOrig
        '
        Me.LoteOrig.HeaderText = "LoteOrig"
        Me.LoteOrig.MinimumWidth = 6
        Me.LoteOrig.Name = "LoteOrig"
        Me.LoteOrig.Visible = False
        Me.LoteOrig.Width = 125
        '
        'ColBodega
        '
        Me.ColBodega.HeaderText = "Bodega"
        Me.ColBodega.MinimumWidth = 6
        Me.ColBodega.Name = "ColBodega"
        Me.ColBodega.Width = 200
        '
        'ColLicPlate
        '
        Me.ColLicPlate.HeaderText = "LP"
        Me.ColLicPlate.MinimumWidth = 6
        Me.ColLicPlate.Name = "ColLicPlate"
        Me.ColLicPlate.Width = 125
        '
        'colTalla
        '
        Me.colTalla.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.colTalla.HeaderText = "Talla"
        Me.colTalla.MinimumWidth = 6
        Me.colTalla.Name = "colTalla"
        Me.colTalla.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colTalla.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.colTalla.Width = 125
        '
        'colColor
        '
        Me.colColor.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.colColor.HeaderText = "Color"
        Me.colColor.MinimumWidth = 6
        Me.colColor.Name = "colColor"
        Me.colColor.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colColor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.colColor.Width = 125
        '
        'colIdProductoTallaColor
        '
        Me.colIdProductoTallaColor.HeaderText = "IdProductoTallaColor"
        Me.colIdProductoTallaColor.MinimumWidth = 6
        Me.colIdProductoTallaColor.Name = "colIdProductoTallaColor"
        Me.colIdProductoTallaColor.Visible = False
        Me.colIdProductoTallaColor.Width = 125
        '
        'DockManager1
        '
        Me.DockManager1.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.AutoHideContainer2})
        Me.DockManager1.Form = Me
        Me.DockManager1.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane", "DevExpress.XtraBars.TabFormControl"})
        '
        'AutoHideContainer2
        '
        Me.AutoHideContainer2.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.AutoHideContainer2.Controls.Add(Me.DockPanel1)
        Me.AutoHideContainer2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.AutoHideContainer2.Location = New System.Drawing.Point(0, 753)
        Me.AutoHideContainer2.Margin = New System.Windows.Forms.Padding(4)
        Me.AutoHideContainer2.Name = "AutoHideContainer2"
        Me.AutoHideContainer2.Size = New System.Drawing.Size(1484, 26)
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.ID = New System.Guid("9883a1ef-faca-4a54-a7cf-192ced1b291e")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 0)
        Me.DockPanel1.Margin = New System.Windows.Forms.Padding(4)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(200, 118)
        Me.DockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.SavedIndex = 0
        Me.DockPanel1.Size = New System.Drawing.Size(1420, 145)
        Me.DockPanel1.Text = "Bitácora"
        Me.DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.Fec_modDateEdit)
        Me.DockPanel1_Container.Controls.Add(Label2)
        Me.DockPanel1_Container.Controls.Add(Me.Fec_agrDateEdit)
        Me.DockPanel1_Container.Controls.Add(Fec_agrLabel)
        Me.DockPanel1_Container.Controls.Add(Me.User_modTextEdit)
        Me.DockPanel1_Container.Controls.Add(User_modLabel)
        Me.DockPanel1_Container.Controls.Add(Me.User_agrTextEdit)
        Me.DockPanel1_Container.Controls.Add(User_agrLabel)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(5, 30)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1410, 111)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'Fec_modDateEdit
        '
        Me.Fec_modDateEdit.EditValue = Nothing
        Me.Fec_modDateEdit.Enabled = False
        Me.Fec_modDateEdit.Location = New System.Drawing.Point(519, 59)
        Me.Fec_modDateEdit.Margin = New System.Windows.Forms.Padding(4)
        Me.Fec_modDateEdit.MenuManager = Me.RibbonControl
        Me.Fec_modDateEdit.Name = "Fec_modDateEdit"
        Me.Fec_modDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit.Size = New System.Drawing.Size(175, 22)
        Me.Fec_modDateEdit.TabIndex = 21
        '
        'Fec_agrDateEdit
        '
        Me.Fec_agrDateEdit.EditValue = Nothing
        Me.Fec_agrDateEdit.Enabled = False
        Me.Fec_agrDateEdit.Location = New System.Drawing.Point(149, 57)
        Me.Fec_agrDateEdit.Margin = New System.Windows.Forms.Padding(4)
        Me.Fec_agrDateEdit.MenuManager = Me.RibbonControl
        Me.Fec_agrDateEdit.Name = "Fec_agrDateEdit"
        Me.Fec_agrDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit.Size = New System.Drawing.Size(175, 22)
        Me.Fec_agrDateEdit.TabIndex = 19
        '
        'User_modTextEdit
        '
        Me.User_modTextEdit.Enabled = False
        Me.User_modTextEdit.Location = New System.Drawing.Point(519, 20)
        Me.User_modTextEdit.Margin = New System.Windows.Forms.Padding(4)
        Me.User_modTextEdit.MenuManager = Me.RibbonControl
        Me.User_modTextEdit.Name = "User_modTextEdit"
        Me.User_modTextEdit.Size = New System.Drawing.Size(175, 22)
        Me.User_modTextEdit.TabIndex = 17
        '
        'User_agrTextEdit
        '
        Me.User_agrTextEdit.Enabled = False
        Me.User_agrTextEdit.Location = New System.Drawing.Point(149, 17)
        Me.User_agrTextEdit.Margin = New System.Windows.Forms.Padding(4)
        Me.User_agrTextEdit.MenuManager = Me.RibbonControl
        Me.User_agrTextEdit.Name = "User_agrTextEdit"
        Me.User_agrTextEdit.Size = New System.Drawing.Size(175, 22)
        Me.User_agrTextEdit.TabIndex = 10
        '
        'XtraTabControl1
        '
        Me.XtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XtraTabControl1.Location = New System.Drawing.Point(0, 193)
        Me.XtraTabControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.XtraTabControl1.Name = "XtraTabControl1"
        Me.XtraTabControl1.SelectedTabPage = Me.XtraTabPage1
        Me.XtraTabControl1.Size = New System.Drawing.Size(1484, 560)
        Me.XtraTabControl1.TabIndex = 34
        Me.XtraTabControl1.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.XtraTabPage1, Me.XtraTabPage2})
        '
        'XtraTabPage1
        '
        Me.XtraTabPage1.Controls.Add(Me.GroupControl4)
        Me.XtraTabPage1.Controls.Add(Me.GroupControl2)
        Me.XtraTabPage1.Margin = New System.Windows.Forms.Padding(4)
        Me.XtraTabPage1.Name = "XtraTabPage1"
        Me.XtraTabPage1.Size = New System.Drawing.Size(1482, 530)
        Me.XtraTabPage1.Text = "Detalle ajuste"
        '
        'XtraTabPage2
        '
        Me.XtraTabPage2.Controls.Add(Me.PanelControl1)
        Me.XtraTabPage2.Margin = New System.Windows.Forms.Padding(4)
        Me.XtraTabPage2.Name = "XtraTabPage2"
        Me.XtraTabPage2.Size = New System.Drawing.Size(1482, 530)
        Me.XtraTabPage2.Text = "Documentos asociados"
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.grdDocsAsociados)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1482, 530)
        Me.PanelControl1.TabIndex = 0
        '
        'grdDocsAsociados
        '
        Me.grdDocsAsociados.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdDocsAsociados.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.grdDocsAsociados.Location = New System.Drawing.Point(2, 2)
        Me.grdDocsAsociados.MainView = Me.GridView1
        Me.grdDocsAsociados.Margin = New System.Windows.Forms.Padding(4)
        Me.grdDocsAsociados.MenuManager = Me.RibbonControl
        Me.grdDocsAsociados.Name = "grdDocsAsociados"
        Me.grdDocsAsociados.Size = New System.Drawing.Size(1478, 526)
        Me.grdDocsAsociados.TabIndex = 0
        Me.grdDocsAsociados.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.DetailHeight = 431
        Me.GridView1.GridControl = Me.grdDocsAsociados
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.ReadOnly = True
        Me.GridView1.OptionsFind.AlwaysVisible = True
        '
        'frmAjusteStock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1484, 779)
        Me.Controls.Add(Me.XtraTabControl1)
        Me.Controls.Add(Me.AutoHideContainer2)
        Me.Controls.Add(Me.RibbonControl)
        Me.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Glow
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmAjusteStock"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Ajuste de stock"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DockPanel2.ResumeLayout(False)
        Me.ControlContainer1.ResumeLayout(False)
        Me.ControlContainer1.PerformLayout()
        CType(Me.DateEdit1.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateEdit2.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateEdit2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEdit2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStripP.ResumeLayout(False)
        Me.ToolStripP.PerformLayout()
        CType(Me.dtpFecha.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFecha.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.GroupControl2.PerformLayout()
        CType(Me.gcCentroCosto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gcCentroCosto.ResumeLayout(False)
        Me.gcCentroCosto.PerformLayout()
        CType(Me.txtCentroCostoDepERP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCentroCostoERP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCentroCostoDirERP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTipoAjuste.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lcmbCentroCosto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPropietarioBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbProductoFamilia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBodegaERP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl4.ResumeLayout(False)
        Me.GroupControl4.PerformLayout()
        CType(Me.dgrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.AutoHideContainer2.ResumeLayout(False)
        Me.DockPanel1.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        Me.DockPanel1_Container.PerformLayout()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabControl1.ResumeLayout(False)
        Me.XtraTabPage1.ResumeLayout(False)
        Me.XtraTabPage2.ResumeLayout(False)
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.grdDocsAsociados, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView5 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents mnuImprimir1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents DockPanel2 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents ControlContainer1 As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents DateEdit1 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents DateEdit2 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents TextEdit1 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEdit2 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GridView6 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents dtpFecha As DevExpress.XtraEditors.DateEdit
    Friend WithEvents txtReferencia As TextBox
    Friend WithEvents ToolStripP As ToolStrip
    Friend WithEvents cmdAdd As ToolStripButton
    Friend WithEvents mnuDel As ToolStripButton
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl4 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents lblRegs As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents User_agrTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents User_modTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_agrDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents Fec_modDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents mnuDividir As ToolStripButton
    Friend WithEvents cmbBodegaERP As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents mnuEstadoEnviadoAERP As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmbProductoFamilia As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbPropietarioBodega As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents AutoHideContainer2 As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents txtSerie As DevExpress.XtraEditors.TextEdit
    Friend WithEvents XtraTabControl1 As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents XtraTabPage1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents XtraTabPage2 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents grdDocsAsociados As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lcmbCentroCosto As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbTipoAjuste As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents dgrid As DataGridView
    Friend WithEvents mnuReimpresionEtiquetas As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents chkAuditado As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents mnuGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuAjustePositivo As ToolStripButton
    Friend WithEvents txtCentroCostoDepERP As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCentroCostoERP As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCentroCostoDirERP As DevExpress.XtraEditors.TextEdit
    Friend WithEvents gcCentroCosto As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ColCodigoProducto As DataGridViewTextBoxColumn
    Friend WithEvents colNombreProducto As DataGridViewTextBoxColumn
    Friend WithEvents UmBas As DataGridViewTextBoxColumn
    Friend WithEvents colPresentacion As DataGridViewTextBoxColumn
    Friend WithEvents colUbicacion As DataGridViewTextBoxColumn
    Friend WithEvents motivoajuste As DataGridViewComboBoxColumn
    Friend WithEvents tipoajuste As DataGridViewComboBoxColumn
    Friend WithEvents CantidadP As DataGridViewTextBoxColumn
    Friend WithEvents ColCantidad As DataGridViewTextBoxColumn
    Friend WithEvents ColDiferencia As DataGridViewImageColumn
    Friend WithEvents colLote As DataGridViewTextBoxColumn
    Friend WithEvents ColObservacion As DataGridViewTextBoxColumn
    Friend WithEvents ColEnviadoAErp As DataGridViewCheckBoxColumn
    Friend WithEvents ColIdAjusteDEt As DataGridViewTextBoxColumn
    Friend WithEvents LoteOrig As DataGridViewTextBoxColumn
    Friend WithEvents ColBodega As DataGridViewComboBoxColumn
    Friend WithEvents ColLicPlate As DataGridViewTextBoxColumn
    Friend WithEvents colTalla As DataGridViewComboBoxColumn
    Friend WithEvents colColor As DataGridViewComboBoxColumn
    Friend WithEvents colIdProductoTallaColor As DataGridViewTextBoxColumn
    Friend WithEvents lblCentroCosto As Label
    Friend WithEvents chkBorrador As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents btnImportarExcel As DevExpress.XtraBars.BarButtonItem
End Class