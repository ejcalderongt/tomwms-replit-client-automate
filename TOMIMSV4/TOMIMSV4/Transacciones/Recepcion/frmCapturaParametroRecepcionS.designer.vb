<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCapturaParametroRecepcionS
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If components IsNot Nothing Then
                    components.Dispose()
                End If
                If pBeProducto IsNot Nothing Then
                    pBeProducto.Dispose()
                    pBeProducto = Nothing
                End If
                If pBePresentacionProducto IsNot Nothing Then
                    pBePresentacionProducto.Dispose()
                    pBePresentacionProducto = Nothing
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim NombreLabel As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim lblPesoIngreso As System.Windows.Forms.Label
        Dim lblPesoReferencia As System.Windows.Forms.Label
        Dim lblPresentacion As System.Windows.Forms.Label
        Dim Label6 As System.Windows.Forms.Label
        Dim Label8 As System.Windows.Forms.Label
        Dim Label9 As System.Windows.Forms.Label
        Dim Label7 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCapturaParametroRecepcionS))
        Dim GridLevelNode2 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Me.DTBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DSPR = New DSPR()
        Me.lblRegs = New DevExpress.XtraBars.BarStaticItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.mnuNuevo = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuSalir = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImprimir = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.chkActivos = New DevExpress.XtraBars.BarCheckItem()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar2 = New DevExpress.XtraBars.Bar()
        Me.cmdAcept = New DevExpress.XtraBars.BarButtonItem()
        Me.BarStaticItem1 = New DevExpress.XtraBars.BarStaticItem()
        Me.cmdCancel = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.BarListItem1 = New DevExpress.XtraBars.BarListItem()
        Me.BarEditItem1 = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemTextEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.xtrCapParametros = New DevExpress.XtraTab.XtraTabControl()
        Me.Stockk = New DevExpress.XtraTab.XtraTabPage()
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.GrpSeries = New DevExpress.XtraEditors.GroupControl()
        Me.txtSerieFinal = New System.Windows.Forms.NumericUpDown()
        Me.txtSerieInicial = New System.Windows.Forms.NumericUpDown()
        Me.GrpTemperatura = New DevExpress.XtraEditors.GroupControl()
        Me.lblTempTolerancia = New System.Windows.Forms.Label()
        Me.txtTemperaturaReal = New System.Windows.Forms.NumericUpDown()
        Me.txtTemperaturaReferencia = New System.Windows.Forms.NumericUpDown()
        Me.GrpPeso = New DevExpress.XtraEditors.GroupControl()
        Me.lblToleranciaPeso = New System.Windows.Forms.Label()
        Me.txtPesoReal = New System.Windows.Forms.NumericUpDown()
        Me.txtPesoReferencia = New System.Windows.Forms.NumericUpDown()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.lblFactor = New System.Windows.Forms.Label()
        Me.chkPaletizarPres = New DevExpress.XtraEditors.CheckEdit()
        Me.grpConfigPallet = New System.Windows.Forms.GroupBox()
        Me.txtCamasPorTarima = New System.Windows.Forms.NumericUpDown()
        Me.chkGeneraLPAuto = New DevExpress.XtraEditors.CheckEdit()
        Me.txtCajasPorCama = New System.Windows.Forms.NumericUpDown()
        Me.lblY = New System.Windows.Forms.Label()
        Me.lblX = New System.Windows.Forms.Label()
        Me.chkPermitirPaletizar = New DevExpress.XtraEditors.CheckEdit()
        Me.cmbPresentacion = New System.Windows.Forms.ComboBox()
        Me.GrpParametro = New DevExpress.XtraEditors.GroupControl()
        Me.Grid = New DevExpress.XtraGrid.GridControl()
        Me.GrdP = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colIdParametro = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDescripcion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTexto = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemTextEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.colNumerico = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemSpinEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.colFecha = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemDateEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.colLogico = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.colTipoParametro = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIdParametroDet = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemTimeEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GrpStock2 = New DevExpress.XtraEditors.GroupControl()
        Me.dtmFechaManufactura = New DevExpress.XtraEditors.DateEdit()
        Me.dtmFechaIngreso = New DevExpress.XtraEditors.DateEdit()
        Me.lblLicPlate = New System.Windows.Forms.Label()
        Me.lblSerial = New System.Windows.Forms.Label()
        Me.lblFechaManufactura = New System.Windows.Forms.Label()
        Me.lblAniada = New System.Windows.Forms.Label()
        Me.txtAniada = New DevExpress.XtraEditors.TextEdit()
        Me.txtSerial = New DevExpress.XtraEditors.TextEdit()
        Me.txtLicPlate = New DevExpress.XtraEditors.TextEdit()
        Me.Series = New DevExpress.XtraTab.XtraTabPage()
        Me.GrpSerie = New DevExpress.XtraEditors.GroupControl()
        Me.cmdGuardar = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.GrdSerie = New DevExpress.XtraGrid.GridControl()
        Me.GridViewSerie = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.txtSerialL = New DevExpress.XtraEditors.TextEdit()
        NombreLabel = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        lblPesoIngreso = New System.Windows.Forms.Label()
        lblPesoReferencia = New System.Windows.Forms.Label()
        lblPresentacion = New System.Windows.Forms.Label()
        Label6 = New System.Windows.Forms.Label()
        Label8 = New System.Windows.Forms.Label()
        Label9 = New System.Windows.Forms.Label()
        Label7 = New System.Windows.Forms.Label()
        CType(Me.DTBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DSPR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtrCapParametros, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtrCapParametros.SuspendLayout()
        Me.Stockk.SuspendLayout()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel1.SuspendLayout()
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel2.SuspendLayout()
        Me.SplitContainerControl1.SuspendLayout()
        CType(Me.GrpSeries, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpSeries.SuspendLayout()
        CType(Me.txtSerieFinal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSerieInicial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpTemperatura, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpTemperatura.SuspendLayout()
        CType(Me.txtTemperaturaReal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTemperaturaReferencia, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpPeso, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpPeso.SuspendLayout()
        CType(Me.txtPesoReal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPesoReferencia, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.chkPaletizarPres.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpConfigPallet.SuspendLayout()
        CType(Me.txtCamasPorTarima, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkGeneraLPAuto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCajasPorCama, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPermitirPaletizar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpParametro, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpParametro.SuspendLayout()
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrdP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSpinEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit1.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTimeEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpStock2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpStock2.SuspendLayout()
        CType(Me.dtmFechaManufactura.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtmFechaManufactura.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtmFechaIngreso.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtmFechaIngreso.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAniada.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSerial.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLicPlate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Series.SuspendLayout()
        CType(Me.GrpSerie, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpSerie.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.GrdSerie, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewSerie, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSerialL.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'NombreLabel
        '
        NombreLabel.AutoSize = True
        NombreLabel.Location = New System.Drawing.Point(19, 46)
        NombreLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        NombreLabel.Name = "NombreLabel"
        NombreLabel.Size = New System.Drawing.Size(45, 16)
        NombreLabel.TabIndex = 0
        NombreLabel.Text = "Serial:"
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(392, 39)
        Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(93, 16)
        Label2.TabIndex = 2
        Label2.Text = "Fecha Ingreso:"
        '
        'lblPesoIngreso
        '
        lblPesoIngreso.AutoSize = True
        lblPesoIngreso.Location = New System.Drawing.Point(16, 96)
        lblPesoIngreso.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblPesoIngreso.Name = "lblPesoIngreso"
        lblPesoIngreso.Size = New System.Drawing.Size(68, 16)
        lblPesoIngreso.TabIndex = 3
        lblPesoIngreso.Text = "Peso Real:"
        '
        'lblPesoReferencia
        '
        lblPesoReferencia.AutoSize = True
        lblPesoReferencia.Location = New System.Drawing.Point(16, 64)
        lblPesoReferencia.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblPesoReferencia.Name = "lblPesoReferencia"
        lblPesoReferencia.Size = New System.Drawing.Size(103, 16)
        lblPesoReferencia.TabIndex = 0
        lblPesoReferencia.Text = "Peso Estadístico:"
        '
        'lblPresentacion
        '
        lblPresentacion.AutoSize = True
        lblPresentacion.Location = New System.Drawing.Point(16, 44)
        lblPresentacion.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblPresentacion.Name = "lblPresentacion"
        lblPresentacion.Size = New System.Drawing.Size(85, 16)
        lblPresentacion.TabIndex = 0
        lblPresentacion.Text = "Presentación:"
        '
        'Label6
        '
        Label6.AutoSize = True
        Label6.Location = New System.Drawing.Point(16, 73)
        Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(116, 16)
        Label6.TabIndex = 3
        Label6.Text = "Temperatura Real:"
        '
        'Label8
        '
        Label8.AutoSize = True
        Label8.Location = New System.Drawing.Point(16, 41)
        Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label8.Name = "Label8"
        Label8.Size = New System.Drawing.Size(151, 16)
        Label8.TabIndex = 0
        Label8.Text = "Temperatura Estadística:"
        '
        'Label9
        '
        Label9.AutoSize = True
        Label9.Location = New System.Drawing.Point(16, 41)
        Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label9.Name = "Label9"
        Label9.Size = New System.Drawing.Size(79, 16)
        Label9.TabIndex = 0
        Label9.Text = "Serie Inicial:"
        '
        'Label7
        '
        Label7.AutoSize = True
        Label7.Location = New System.Drawing.Point(16, 73)
        Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label7.Name = "Label7"
        Label7.Size = New System.Drawing.Size(73, 16)
        Label7.TabIndex = 2
        Label7.Text = "Serie Final:"
        '
        'DTBindingSource
        '
        Me.DTBindingSource.DataMember = "DT"
        Me.DTBindingSource.DataSource = Me.DSPR
        '
        'DSPR
        '
        Me.DSPR.DataSetName = "DSPR"
        Me.DSPR.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'lblRegs
        '
        Me.lblRegs.Caption = "Registros: 0"
        Me.lblRegs.Id = 5
        Me.lblRegs.Name = "lblRegs"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1, Me.RibbonPageGroup2})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Opciones de Lista"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuNuevo)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuSalir)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdImprimir)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'mnuNuevo
        '
        Me.mnuNuevo.Caption = "Nuevo"
        Me.mnuNuevo.Id = 1
        Me.mnuNuevo.ImageOptions.Image = CType(resources.GetObject("mnuNuevo.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuNuevo.ImageOptions.LargeImage = CType(resources.GetObject("mnuNuevo.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuNuevo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N))
        Me.mnuNuevo.Name = "mnuNuevo"
        '
        'mnuActualizar
        '
        Me.mnuActualizar.Caption = "Actualizar"
        Me.mnuActualizar.Id = 2
        Me.mnuActualizar.ImageOptions.Image = CType(resources.GetObject("mnuActualizar.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuActualizar.ImageOptions.LargeImage = CType(resources.GetObject("mnuActualizar.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuActualizar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A))
        Me.mnuActualizar.Name = "mnuActualizar"
        '
        'mnuSalir
        '
        Me.mnuSalir.Caption = "Salir"
        Me.mnuSalir.Id = 3
        Me.mnuSalir.ImageOptions.Image = CType(resources.GetObject("mnuSalir.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuSalir.ImageOptions.LargeImage = CType(resources.GetObject("mnuSalir.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuSalir.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E))
        Me.mnuSalir.Name = "mnuSalir"
        '
        'cmdImprimir
        '
        Me.cmdImprimir.Caption = "Imprimir"
        Me.cmdImprimir.Id = 6
        Me.cmdImprimir.ImageOptions.Image = CType(resources.GetObject("cmdImprimir.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdImprimir.ImageOptions.LargeImage = CType(resources.GetObject("cmdImprimir.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.cmdImprimir.Name = "cmdImprimir"
        '
        'RibbonPageGroup2
        '
        Me.RibbonPageGroup2.ItemLinks.Add(Me.chkActivos)
        Me.RibbonPageGroup2.Name = "RibbonPageGroup2"
        '
        'chkActivos
        '
        Me.chkActivos.BindableChecked = True
        Me.chkActivos.Caption = "Activos"
        Me.chkActivos.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText
        Me.chkActivos.Checked = True
        Me.chkActivos.Id = 4
        Me.chkActivos.Name = "chkActivos"
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar2})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.cmdAcept, Me.BarListItem1, Me.BarEditItem1, Me.BarStaticItem1, Me.cmdCancel})
        Me.BarManager1.MainMenu = Me.Bar2
        Me.BarManager1.MaxItemId = 5
        Me.BarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemTextEdit1})
        '
        'Bar2
        '
        Me.Bar2.BarName = "Main menu"
        Me.Bar2.DockCol = 0
        Me.Bar2.DockRow = 0
        Me.Bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar2.FloatLocation = New System.Drawing.Point(113, 78)
        Me.Bar2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.cmdAcept), New DevExpress.XtraBars.LinkPersistInfo(Me.BarStaticItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdCancel)})
        Me.Bar2.OptionsBar.MultiLine = True
        Me.Bar2.OptionsBar.UseWholeRow = True
        Me.Bar2.Text = "Main menu"
        '
        'cmdAcept
        '
        Me.cmdAcept.Caption = "Aceptar"
        Me.cmdAcept.Id = 0
        Me.cmdAcept.ImageOptions.Image = CType(resources.GetObject("cmdAcept.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdAcept.Name = "cmdAcept"
        Me.cmdAcept.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'BarStaticItem1
        '
        Me.BarStaticItem1.Caption = "|"
        Me.BarStaticItem1.Id = 3
        Me.BarStaticItem1.Name = "BarStaticItem1"
        '
        'cmdCancel
        '
        Me.cmdCancel.Caption = "Cancelar"
        Me.cmdCancel.Id = 4
        Me.cmdCancel.ImageOptions.Image = CType(resources.GetObject("cmdCancel.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.cmdCancel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Manager = Me.BarManager1
        Me.barDockControlTop.Margin = New System.Windows.Forms.Padding(4)
        Me.barDockControlTop.Size = New System.Drawing.Size(1532, 50)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 709)
        Me.barDockControlBottom.Manager = Me.BarManager1
        Me.barDockControlBottom.Margin = New System.Windows.Forms.Padding(4)
        Me.barDockControlBottom.Size = New System.Drawing.Size(1532, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 50)
        Me.barDockControlLeft.Manager = Me.BarManager1
        Me.barDockControlLeft.Margin = New System.Windows.Forms.Padding(4)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 659)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1532, 50)
        Me.barDockControlRight.Manager = Me.BarManager1
        Me.barDockControlRight.Margin = New System.Windows.Forms.Padding(4)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 659)
        '
        'BarListItem1
        '
        Me.BarListItem1.Caption = "BarListItem1"
        Me.BarListItem1.Id = 1
        Me.BarListItem1.Name = "BarListItem1"
        '
        'BarEditItem1
        '
        Me.BarEditItem1.Caption = "BarEditItem1"
        Me.BarEditItem1.Edit = Me.RepositoryItemTextEdit1
        Me.BarEditItem1.Id = 2
        Me.BarEditItem1.Name = "BarEditItem1"
        '
        'RepositoryItemTextEdit1
        '
        Me.RepositoryItemTextEdit1.AutoHeight = False
        Me.RepositoryItemTextEdit1.Name = "RepositoryItemTextEdit1"
        '
        'xtrCapParametros
        '
        Me.xtrCapParametros.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.xtrCapParametros.Appearance.Options.UseBackColor = True
        Me.xtrCapParametros.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtrCapParametros.Location = New System.Drawing.Point(0, 50)
        Me.xtrCapParametros.Margin = New System.Windows.Forms.Padding(4)
        Me.xtrCapParametros.Name = "xtrCapParametros"
        Me.xtrCapParametros.SelectedTabPage = Me.Stockk
        Me.xtrCapParametros.Size = New System.Drawing.Size(1532, 659)
        Me.xtrCapParametros.TabIndex = 0
        Me.xtrCapParametros.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.Stockk, Me.Series})
        '
        'Stockk
        '
        Me.Stockk.Controls.Add(Me.SplitContainerControl1)
        Me.Stockk.Margin = New System.Windows.Forms.Padding(4)
        Me.Stockk.Name = "Stockk"
        Me.Stockk.Size = New System.Drawing.Size(1530, 629)
        Me.Stockk.Text = "Stock"
        '
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        '
        'SplitContainerControl1.Panel1
        '
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.GrpSeries)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.GrpTemperatura)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.GrpPeso)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.GroupControl2)
        Me.SplitContainerControl1.Panel1.Text = "Panel1"
        '
        'SplitContainerControl1.Panel2
        '
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.GrpParametro)
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.GrpStock2)
        Me.SplitContainerControl1.Panel2.Text = "Panel2"
        Me.SplitContainerControl1.Size = New System.Drawing.Size(1530, 629)
        Me.SplitContainerControl1.SplitterPosition = 772
        Me.SplitContainerControl1.TabIndex = 0
        Me.SplitContainerControl1.Text = "SplitContainerControl1"
        '
        'GrpSeries
        '
        Me.GrpSeries.Controls.Add(Me.txtSerieFinal)
        Me.GrpSeries.Controls.Add(Me.txtSerieInicial)
        Me.GrpSeries.Controls.Add(Label7)
        Me.GrpSeries.Controls.Add(Label9)
        Me.GrpSeries.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpSeries.Location = New System.Drawing.Point(0, 502)
        Me.GrpSeries.Margin = New System.Windows.Forms.Padding(4)
        Me.GrpSeries.Name = "GrpSeries"
        Me.GrpSeries.Size = New System.Drawing.Size(772, 127)
        Me.GrpSeries.TabIndex = 3
        Me.GrpSeries.Text = "Serie Rango"
        '
        'txtSerieFinal
        '
        Me.txtSerieFinal.DecimalPlaces = 6
        Me.txtSerieFinal.Location = New System.Drawing.Point(193, 70)
        Me.txtSerieFinal.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSerieFinal.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtSerieFinal.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtSerieFinal.Name = "txtSerieFinal"
        Me.txtSerieFinal.Size = New System.Drawing.Size(180, 23)
        Me.txtSerieFinal.TabIndex = 3
        '
        'txtSerieInicial
        '
        Me.txtSerieInicial.DecimalPlaces = 6
        Me.txtSerieInicial.Location = New System.Drawing.Point(193, 38)
        Me.txtSerieInicial.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSerieInicial.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtSerieInicial.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtSerieInicial.Name = "txtSerieInicial"
        Me.txtSerieInicial.Size = New System.Drawing.Size(180, 23)
        Me.txtSerieInicial.TabIndex = 1
        '
        'GrpTemperatura
        '
        Me.GrpTemperatura.Controls.Add(Me.lblTempTolerancia)
        Me.GrpTemperatura.Controls.Add(Me.txtTemperaturaReal)
        Me.GrpTemperatura.Controls.Add(Label8)
        Me.GrpTemperatura.Controls.Add(Label6)
        Me.GrpTemperatura.Controls.Add(Me.txtTemperaturaReferencia)
        Me.GrpTemperatura.Dock = System.Windows.Forms.DockStyle.Top
        Me.GrpTemperatura.Location = New System.Drawing.Point(0, 363)
        Me.GrpTemperatura.Margin = New System.Windows.Forms.Padding(4)
        Me.GrpTemperatura.Name = "GrpTemperatura"
        Me.GrpTemperatura.Size = New System.Drawing.Size(772, 139)
        Me.GrpTemperatura.TabIndex = 2
        Me.GrpTemperatura.Text = "Temperatura"
        '
        'lblTempTolerancia
        '
        Me.lblTempTolerancia.Location = New System.Drawing.Point(381, 41)
        Me.lblTempTolerancia.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTempTolerancia.Name = "lblTempTolerancia"
        Me.lblTempTolerancia.Size = New System.Drawing.Size(59, 16)
        Me.lblTempTolerancia.TabIndex = 2
        Me.lblTempTolerancia.Text = "±10%"
        '
        'txtTemperaturaReal
        '
        Me.txtTemperaturaReal.DecimalPlaces = 6
        Me.txtTemperaturaReal.Location = New System.Drawing.Point(193, 70)
        Me.txtTemperaturaReal.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTemperaturaReal.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtTemperaturaReal.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtTemperaturaReal.Name = "txtTemperaturaReal"
        Me.txtTemperaturaReal.Size = New System.Drawing.Size(180, 23)
        Me.txtTemperaturaReal.TabIndex = 4
        '
        'txtTemperaturaReferencia
        '
        Me.txtTemperaturaReferencia.DecimalPlaces = 6
        Me.txtTemperaturaReferencia.Enabled = False
        Me.txtTemperaturaReferencia.Location = New System.Drawing.Point(193, 38)
        Me.txtTemperaturaReferencia.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTemperaturaReferencia.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtTemperaturaReferencia.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtTemperaturaReferencia.Name = "txtTemperaturaReferencia"
        Me.txtTemperaturaReferencia.ReadOnly = True
        Me.txtTemperaturaReferencia.Size = New System.Drawing.Size(180, 23)
        Me.txtTemperaturaReferencia.TabIndex = 1
        '
        'GrpPeso
        '
        Me.GrpPeso.Controls.Add(Me.lblToleranciaPeso)
        Me.GrpPeso.Controls.Add(Me.txtPesoReal)
        Me.GrpPeso.Controls.Add(lblPesoReferencia)
        Me.GrpPeso.Controls.Add(lblPesoIngreso)
        Me.GrpPeso.Controls.Add(Me.txtPesoReferencia)
        Me.GrpPeso.Dock = System.Windows.Forms.DockStyle.Top
        Me.GrpPeso.Location = New System.Drawing.Point(0, 214)
        Me.GrpPeso.Margin = New System.Windows.Forms.Padding(4)
        Me.GrpPeso.Name = "GrpPeso"
        Me.GrpPeso.Size = New System.Drawing.Size(772, 149)
        Me.GrpPeso.TabIndex = 1
        Me.GrpPeso.Text = "Peso"
        '
        'lblToleranciaPeso
        '
        Me.lblToleranciaPeso.Location = New System.Drawing.Point(381, 64)
        Me.lblToleranciaPeso.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblToleranciaPeso.Name = "lblToleranciaPeso"
        Me.lblToleranciaPeso.Size = New System.Drawing.Size(59, 16)
        Me.lblToleranciaPeso.TabIndex = 2
        Me.lblToleranciaPeso.Text = "±10%"
        '
        'txtPesoReal
        '
        Me.txtPesoReal.DecimalPlaces = 6
        Me.txtPesoReal.Location = New System.Drawing.Point(193, 94)
        Me.txtPesoReal.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPesoReal.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtPesoReal.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtPesoReal.Name = "txtPesoReal"
        Me.txtPesoReal.Size = New System.Drawing.Size(180, 23)
        Me.txtPesoReal.TabIndex = 4
        '
        'txtPesoReferencia
        '
        Me.txtPesoReferencia.DecimalPlaces = 6
        Me.txtPesoReferencia.Enabled = False
        Me.txtPesoReferencia.Location = New System.Drawing.Point(193, 62)
        Me.txtPesoReferencia.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPesoReferencia.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtPesoReferencia.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtPesoReferencia.Name = "txtPesoReferencia"
        Me.txtPesoReferencia.ReadOnly = True
        Me.txtPesoReferencia.Size = New System.Drawing.Size(180, 23)
        Me.txtPesoReferencia.TabIndex = 1
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.lblFactor)
        Me.GroupControl2.Controls.Add(Me.chkPaletizarPres)
        Me.GroupControl2.Controls.Add(lblPresentacion)
        Me.GroupControl2.Controls.Add(Me.grpConfigPallet)
        Me.GroupControl2.Controls.Add(Me.cmbPresentacion)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl2.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(772, 214)
        Me.GroupControl2.TabIndex = 0
        Me.GroupControl2.Text = "Parámetros de presentación"
        '
        'lblFactor
        '
        Me.lblFactor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFactor.Location = New System.Drawing.Point(193, 70)
        Me.lblFactor.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFactor.Name = "lblFactor"
        Me.lblFactor.Size = New System.Drawing.Size(179, 27)
        Me.lblFactor.TabIndex = 3
        Me.lblFactor.Text = "Factor:"
        '
        'chkPaletizarPres
        '
        Me.chkPaletizarPres.Location = New System.Drawing.Point(428, 38)
        Me.chkPaletizarPres.Margin = New System.Windows.Forms.Padding(4)
        Me.chkPaletizarPres.Name = "chkPaletizarPres"
        Me.chkPaletizarPres.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.chkPaletizarPres.Properties.Caption = "Paletizar"
        Me.chkPaletizarPres.Size = New System.Drawing.Size(155, 28)
        Me.chkPaletizarPres.TabIndex = 2
        Me.chkPaletizarPres.ToolTip = "Índica si la presentación puede paletizarse, " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "éste parámetro es excluyente con e" &
    "l parámetro ""Es Pallet""" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'grpConfigPallet
        '
        Me.grpConfigPallet.Controls.Add(Me.txtCamasPorTarima)
        Me.grpConfigPallet.Controls.Add(Me.chkGeneraLPAuto)
        Me.grpConfigPallet.Controls.Add(Me.txtCajasPorCama)
        Me.grpConfigPallet.Controls.Add(Me.lblY)
        Me.grpConfigPallet.Controls.Add(Me.lblX)
        Me.grpConfigPallet.Controls.Add(Me.chkPermitirPaletizar)
        Me.grpConfigPallet.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.grpConfigPallet.Location = New System.Drawing.Point(2, 102)
        Me.grpConfigPallet.Margin = New System.Windows.Forms.Padding(4)
        Me.grpConfigPallet.Name = "grpConfigPallet"
        Me.grpConfigPallet.Padding = New System.Windows.Forms.Padding(4)
        Me.grpConfigPallet.Size = New System.Drawing.Size(768, 110)
        Me.grpConfigPallet.TabIndex = 4
        Me.grpConfigPallet.TabStop = False
        '
        'txtCamasPorTarima
        '
        Me.txtCamasPorTarima.Enabled = False
        Me.txtCamasPorTarima.Location = New System.Drawing.Point(191, 58)
        Me.txtCamasPorTarima.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCamasPorTarima.Name = "txtCamasPorTarima"
        Me.txtCamasPorTarima.Size = New System.Drawing.Size(180, 23)
        Me.txtCamasPorTarima.TabIndex = 4
        '
        'chkGeneraLPAuto
        '
        Me.chkGeneraLPAuto.Location = New System.Drawing.Point(425, 55)
        Me.chkGeneraLPAuto.Margin = New System.Windows.Forms.Padding(4)
        Me.chkGeneraLPAuto.Name = "chkGeneraLPAuto"
        Me.chkGeneraLPAuto.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.chkGeneraLPAuto.Properties.Caption = "Genera LP Auto"
        Me.chkGeneraLPAuto.Size = New System.Drawing.Size(155, 28)
        Me.chkGeneraLPAuto.TabIndex = 5
        Me.chkGeneraLPAuto.ToolTip = "Índica si se generará un correlativo único para cada pallet o se ingresará el núm" &
    "ero de pallet."
        '
        'txtCajasPorCama
        '
        Me.txtCajasPorCama.Enabled = False
        Me.txtCajasPorCama.Location = New System.Drawing.Point(191, 23)
        Me.txtCajasPorCama.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCajasPorCama.Name = "txtCajasPorCama"
        Me.txtCajasPorCama.Size = New System.Drawing.Size(180, 23)
        Me.txtCajasPorCama.TabIndex = 2
        '
        'lblY
        '
        Me.lblY.AutoSize = True
        Me.lblY.Location = New System.Drawing.Point(15, 60)
        Me.lblY.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblY.Name = "lblY"
        Me.lblY.Size = New System.Drawing.Size(108, 16)
        Me.lblY.TabIndex = 3
        Me.lblY.Text = "Camas X Tarima:"
        '
        'lblX
        '
        Me.lblX.AutoSize = True
        Me.lblX.Location = New System.Drawing.Point(13, 26)
        Me.lblX.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblX.Name = "lblX"
        Me.lblX.Size = New System.Drawing.Size(93, 16)
        Me.lblX.TabIndex = 0
        Me.lblX.Text = "Cajas X Cama:"
        '
        'chkPermitirPaletizar
        '
        Me.chkPermitirPaletizar.EditValue = True
        Me.chkPermitirPaletizar.Location = New System.Drawing.Point(425, 20)
        Me.chkPermitirPaletizar.Margin = New System.Windows.Forms.Padding(4)
        Me.chkPermitirPaletizar.Name = "chkPermitirPaletizar"
        Me.chkPermitirPaletizar.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.chkPermitirPaletizar.Properties.Caption = "Permitir paletizar"
        Me.chkPermitirPaletizar.Size = New System.Drawing.Size(155, 28)
        Me.chkPermitirPaletizar.TabIndex = 1
        Me.chkPermitirPaletizar.ToolTip = "Índica si la presentación puede paletizarse, " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "éste parámetro es excluyente con e" &
    "l parámetro ""Es Pallet""" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'cmbPresentacion
        '
        Me.cmbPresentacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPresentacion.FormattingEnabled = True
        Me.cmbPresentacion.Location = New System.Drawing.Point(193, 41)
        Me.cmbPresentacion.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbPresentacion.Name = "cmbPresentacion"
        Me.cmbPresentacion.Size = New System.Drawing.Size(179, 24)
        Me.cmbPresentacion.TabIndex = 1
        '
        'GrpParametro
        '
        Me.GrpParametro.Controls.Add(Me.Grid)
        Me.GrpParametro.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpParametro.Location = New System.Drawing.Point(0, 150)
        Me.GrpParametro.Margin = New System.Windows.Forms.Padding(4)
        Me.GrpParametro.Name = "GrpParametro"
        Me.GrpParametro.Size = New System.Drawing.Size(746, 479)
        Me.GrpParametro.TabIndex = 1
        Me.GrpParametro.Text = "Parametros"
        '
        'Grid
        '
        Me.Grid.Cursor = System.Windows.Forms.Cursors.Default
        Me.Grid.DataSource = Me.DTBindingSource
        Me.Grid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Grid.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.Grid.Location = New System.Drawing.Point(2, 28)
        Me.Grid.MainView = Me.GrdP
        Me.Grid.Margin = New System.Windows.Forms.Padding(4)
        Me.Grid.Name = "Grid"
        Me.Grid.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.RepositoryItemTextEdit2, Me.RepositoryItemSpinEdit1, Me.RepositoryItemTimeEdit1, Me.RepositoryItemDateEdit1})
        Me.Grid.Size = New System.Drawing.Size(742, 449)
        Me.Grid.TabIndex = 0
        Me.Grid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GrdP, Me.GridView2})
        '
        'GrdP
        '
        Me.GrdP.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colIdParametro, Me.colDescripcion, Me.colTexto, Me.colNumerico, Me.colFecha, Me.colLogico, Me.colTipoParametro, Me.colIdParametroDet})
        Me.GrdP.DetailHeight = 431
        Me.GrdP.GridControl = Me.Grid
        Me.GrdP.Name = "GrdP"
        Me.GrdP.OptionsView.ShowGroupPanel = False
        '
        'colIdParametro
        '
        Me.colIdParametro.Caption = "Código"
        Me.colIdParametro.FieldName = "IdParametro"
        Me.colIdParametro.MinWidth = 27
        Me.colIdParametro.Name = "colIdParametro"
        Me.colIdParametro.Visible = True
        Me.colIdParametro.VisibleIndex = 0
        Me.colIdParametro.Width = 100
        '
        'colDescripcion
        '
        Me.colDescripcion.Caption = "Descripción"
        Me.colDescripcion.FieldName = "colDescripcion"
        Me.colDescripcion.MinWidth = 27
        Me.colDescripcion.Name = "colDescripcion"
        Me.colDescripcion.Visible = True
        Me.colDescripcion.VisibleIndex = 1
        Me.colDescripcion.Width = 100
        '
        'colTexto
        '
        Me.colTexto.Caption = "Texto"
        Me.colTexto.ColumnEdit = Me.RepositoryItemTextEdit2
        Me.colTexto.FieldName = "colTexto"
        Me.colTexto.MinWidth = 27
        Me.colTexto.Name = "colTexto"
        Me.colTexto.Visible = True
        Me.colTexto.VisibleIndex = 2
        Me.colTexto.Width = 100
        '
        'RepositoryItemTextEdit2
        '
        Me.RepositoryItemTextEdit2.AutoHeight = False
        Me.RepositoryItemTextEdit2.Name = "RepositoryItemTextEdit2"
        '
        'colNumerico
        '
        Me.colNumerico.Caption = "Valor"
        Me.colNumerico.ColumnEdit = Me.RepositoryItemSpinEdit1
        Me.colNumerico.FieldName = "colNumerico"
        Me.colNumerico.MinWidth = 27
        Me.colNumerico.Name = "colNumerico"
        Me.colNumerico.Visible = True
        Me.colNumerico.VisibleIndex = 3
        Me.colNumerico.Width = 100
        '
        'RepositoryItemSpinEdit1
        '
        Me.RepositoryItemSpinEdit1.AutoHeight = False
        Me.RepositoryItemSpinEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemSpinEdit1.Name = "RepositoryItemSpinEdit1"
        '
        'colFecha
        '
        Me.colFecha.Caption = "Fecha"
        Me.colFecha.ColumnEdit = Me.RepositoryItemDateEdit1
        Me.colFecha.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.colFecha.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.colFecha.FieldName = "colFecha"
        Me.colFecha.MinWidth = 27
        Me.colFecha.Name = "colFecha"
        Me.colFecha.Visible = True
        Me.colFecha.VisibleIndex = 4
        Me.colFecha.Width = 100
        '
        'RepositoryItemDateEdit1
        '
        Me.RepositoryItemDateEdit1.AutoHeight = False
        Me.RepositoryItemDateEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateEdit1.Name = "RepositoryItemDateEdit1"
        '
        'colLogico
        '
        Me.colLogico.Caption = "Verdadero"
        Me.colLogico.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.colLogico.FieldName = "colLogico"
        Me.colLogico.MinWidth = 27
        Me.colLogico.Name = "colLogico"
        Me.colLogico.Visible = True
        Me.colLogico.VisibleIndex = 5
        Me.colLogico.Width = 100
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        '
        'colTipoParametro
        '
        Me.colTipoParametro.FieldName = "TipoParametro"
        Me.colTipoParametro.MinWidth = 27
        Me.colTipoParametro.Name = "colTipoParametro"
        Me.colTipoParametro.Visible = True
        Me.colTipoParametro.VisibleIndex = 6
        Me.colTipoParametro.Width = 100
        '
        'colIdParametroDet
        '
        Me.colIdParametroDet.FieldName = "IdParametroDet"
        Me.colIdParametroDet.MinWidth = 27
        Me.colIdParametroDet.Name = "colIdParametroDet"
        Me.colIdParametroDet.Width = 100
        '
        'RepositoryItemTimeEdit1
        '
        Me.RepositoryItemTimeEdit1.AutoHeight = False
        Me.RepositoryItemTimeEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemTimeEdit1.Name = "RepositoryItemTimeEdit1"
        '
        'GridView2
        '
        Me.GridView2.DetailHeight = 431
        Me.GridView2.GridControl = Me.Grid
        Me.GridView2.Name = "GridView2"
        '
        'GrpStock2
        '
        Me.GrpStock2.Controls.Add(Me.dtmFechaManufactura)
        Me.GrpStock2.Controls.Add(Me.dtmFechaIngreso)
        Me.GrpStock2.Controls.Add(Me.lblLicPlate)
        Me.GrpStock2.Controls.Add(Me.lblSerial)
        Me.GrpStock2.Controls.Add(Me.lblFechaManufactura)
        Me.GrpStock2.Controls.Add(Me.lblAniada)
        Me.GrpStock2.Controls.Add(Me.txtAniada)
        Me.GrpStock2.Controls.Add(Label2)
        Me.GrpStock2.Controls.Add(Me.txtSerial)
        Me.GrpStock2.Controls.Add(Me.txtLicPlate)
        Me.GrpStock2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GrpStock2.Location = New System.Drawing.Point(0, 0)
        Me.GrpStock2.Margin = New System.Windows.Forms.Padding(4)
        Me.GrpStock2.Name = "GrpStock2"
        Me.GrpStock2.Size = New System.Drawing.Size(746, 150)
        Me.GrpStock2.TabIndex = 0
        Me.GrpStock2.Text = "Stock"
        '
        'dtmFechaManufactura
        '
        Me.dtmFechaManufactura.EditValue = New Date(2017, 11, 20, 9, 21, 19, 758)
        Me.dtmFechaManufactura.Location = New System.Drawing.Point(533, 68)
        Me.dtmFechaManufactura.Margin = New System.Windows.Forms.Padding(4)
        Me.dtmFechaManufactura.MenuManager = Me.BarManager1
        Me.dtmFechaManufactura.Name = "dtmFechaManufactura"
        Me.dtmFechaManufactura.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtmFechaManufactura.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtmFechaManufactura.Size = New System.Drawing.Size(179, 22)
        Me.dtmFechaManufactura.TabIndex = 7
        '
        'dtmFechaIngreso
        '
        Me.dtmFechaIngreso.EditValue = New Date(2017, 11, 20, 9, 20, 34, 58)
        Me.dtmFechaIngreso.Location = New System.Drawing.Point(533, 37)
        Me.dtmFechaIngreso.Margin = New System.Windows.Forms.Padding(4)
        Me.dtmFechaIngreso.MenuManager = Me.BarManager1
        Me.dtmFechaIngreso.Name = "dtmFechaIngreso"
        Me.dtmFechaIngreso.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtmFechaIngreso.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtmFechaIngreso.Size = New System.Drawing.Size(179, 22)
        Me.dtmFechaIngreso.TabIndex = 3
        '
        'lblLicPlate
        '
        Me.lblLicPlate.AutoSize = True
        Me.lblLicPlate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLicPlate.Location = New System.Drawing.Point(21, 39)
        Me.lblLicPlate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblLicPlate.Name = "lblLicPlate"
        Me.lblLicPlate.Size = New System.Drawing.Size(60, 17)
        Me.lblLicPlate.TabIndex = 0
        Me.lblLicPlate.Text = "Licencia:"
        '
        'lblSerial
        '
        Me.lblSerial.AutoSize = True
        Me.lblSerial.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSerial.Location = New System.Drawing.Point(21, 71)
        Me.lblSerial.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSerial.Name = "lblSerial"
        Me.lblSerial.Size = New System.Drawing.Size(44, 17)
        Me.lblSerial.TabIndex = 4
        Me.lblSerial.Text = "Serial:"
        '
        'lblFechaManufactura
        '
        Me.lblFechaManufactura.AutoSize = True
        Me.lblFechaManufactura.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFechaManufactura.Location = New System.Drawing.Point(392, 71)
        Me.lblFechaManufactura.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFechaManufactura.Name = "lblFechaManufactura"
        Me.lblFechaManufactura.Size = New System.Drawing.Size(129, 17)
        Me.lblFechaManufactura.TabIndex = 6
        Me.lblFechaManufactura.Text = "Fecha Manufactura:"
        '
        'lblAniada
        '
        Me.lblAniada.AutoSize = True
        Me.lblAniada.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAniada.Location = New System.Drawing.Point(21, 103)
        Me.lblAniada.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblAniada.Name = "lblAniada"
        Me.lblAniada.Size = New System.Drawing.Size(51, 17)
        Me.lblAniada.TabIndex = 8
        Me.lblAniada.Text = "Añada:"
        '
        'txtAniada
        '
        Me.txtAniada.Location = New System.Drawing.Point(133, 100)
        Me.txtAniada.Margin = New System.Windows.Forms.Padding(4)
        Me.txtAniada.Name = "txtAniada"
        Me.txtAniada.Properties.Mask.EditMask = "n0"
        Me.txtAniada.Size = New System.Drawing.Size(251, 22)
        Me.txtAniada.TabIndex = 9
        '
        'txtSerial
        '
        Me.txtSerial.Location = New System.Drawing.Point(133, 68)
        Me.txtSerial.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSerial.Name = "txtSerial"
        Me.txtSerial.Size = New System.Drawing.Size(251, 22)
        Me.txtSerial.TabIndex = 5
        '
        'txtLicPlate
        '
        Me.txtLicPlate.Location = New System.Drawing.Point(133, 36)
        Me.txtLicPlate.Margin = New System.Windows.Forms.Padding(4)
        Me.txtLicPlate.Name = "txtLicPlate"
        Me.txtLicPlate.Properties.Mask.EditMask = "n0"
        Me.txtLicPlate.Size = New System.Drawing.Size(251, 22)
        Me.txtLicPlate.TabIndex = 1
        '
        'Series
        '
        Me.Series.Controls.Add(Me.GrpSerie)
        Me.Series.Margin = New System.Windows.Forms.Padding(4)
        Me.Series.Name = "Series"
        Me.Series.Size = New System.Drawing.Size(1530, 629)
        Me.Series.Text = "Series"
        '
        'GrpSerie
        '
        Me.GrpSerie.Controls.Add(Me.cmdGuardar)
        Me.GrpSerie.Controls.Add(Me.GroupControl1)
        Me.GrpSerie.Controls.Add(NombreLabel)
        Me.GrpSerie.Controls.Add(Me.txtSerialL)
        Me.GrpSerie.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpSerie.Location = New System.Drawing.Point(0, 0)
        Me.GrpSerie.Margin = New System.Windows.Forms.Padding(4)
        Me.GrpSerie.Name = "GrpSerie"
        Me.GrpSerie.Size = New System.Drawing.Size(1530, 629)
        Me.GrpSerie.TabIndex = 0
        '
        'cmdGuardar
        '
        Me.cmdGuardar.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdGuardar.Appearance.Options.UseFont = True
        Me.cmdGuardar.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003
        Me.cmdGuardar.Location = New System.Drawing.Point(615, 41)
        Me.cmdGuardar.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdGuardar.Name = "cmdGuardar"
        Me.cmdGuardar.Size = New System.Drawing.Size(113, 25)
        Me.cmdGuardar.TabIndex = 1
        Me.cmdGuardar.Text = "Guardar"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.GrdSerie)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupControl1.Location = New System.Drawing.Point(2, 180)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1526, 447)
        Me.GroupControl1.TabIndex = 3
        Me.GroupControl1.Text = "Detalle Series"
        '
        'GrdSerie
        '
        Me.GrdSerie.Cursor = System.Windows.Forms.Cursors.Default
        Me.GrdSerie.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrdSerie.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode2.RelationName = "Level1"
        Me.GrdSerie.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode2})
        Me.GrdSerie.Location = New System.Drawing.Point(2, 28)
        Me.GrdSerie.MainView = Me.GridViewSerie
        Me.GrdSerie.Margin = New System.Windows.Forms.Padding(4)
        Me.GrdSerie.Name = "GrdSerie"
        Me.GrdSerie.Size = New System.Drawing.Size(1522, 417)
        Me.GrdSerie.TabIndex = 0
        Me.GrdSerie.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewSerie, Me.GridView1})
        '
        'GridViewSerie
        '
        Me.GridViewSerie.DetailHeight = 431
        Me.GridViewSerie.GridControl = Me.GrdSerie
        Me.GridViewSerie.Name = "GridViewSerie"
        Me.GridViewSerie.OptionsBehavior.Editable = False
        Me.GridViewSerie.OptionsFind.AlwaysVisible = True
        Me.GridViewSerie.OptionsView.ShowGroupPanel = False
        '
        'GridView1
        '
        Me.GridView1.DetailHeight = 431
        Me.GridView1.GridControl = Me.GrdSerie
        Me.GridView1.Name = "GridView1"
        '
        'txtSerialL
        '
        Me.txtSerialL.Location = New System.Drawing.Point(75, 42)
        Me.txtSerialL.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSerialL.Name = "txtSerialL"
        Me.txtSerialL.Size = New System.Drawing.Size(532, 22)
        Me.txtSerialL.TabIndex = 2
        '
        'frmCapturaParametroRecepcionS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1532, 709)
        Me.ControlBox = False
        Me.Controls.Add(Me.xtrCapParametros)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCapturaParametroRecepcionS"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.DTBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DSPR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtrCapParametros, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtrCapParametros.ResumeLayout(False)
        Me.Stockk.ResumeLayout(False)
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel1.ResumeLayout(False)
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(false)
        CType(Me.GrpSeries,System.ComponentModel.ISupportInitialize).EndInit
        Me.GrpSeries.ResumeLayout(false)
        Me.GrpSeries.PerformLayout
        CType(Me.txtSerieFinal,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtSerieInicial,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GrpTemperatura,System.ComponentModel.ISupportInitialize).EndInit
        Me.GrpTemperatura.ResumeLayout(false)
        Me.GrpTemperatura.PerformLayout
        CType(Me.txtTemperaturaReal,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtTemperaturaReferencia,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GrpPeso,System.ComponentModel.ISupportInitialize).EndInit
        Me.GrpPeso.ResumeLayout(false)
        Me.GrpPeso.PerformLayout
        CType(Me.txtPesoReal,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtPesoReferencia,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GroupControl2,System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupControl2.ResumeLayout(false)
        Me.GroupControl2.PerformLayout
        CType(Me.chkPaletizarPres.Properties,System.ComponentModel.ISupportInitialize).EndInit
        Me.grpConfigPallet.ResumeLayout(false)
        Me.grpConfigPallet.PerformLayout
        CType(Me.txtCamasPorTarima,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.chkGeneraLPAuto.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtCajasPorCama,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.chkPermitirPaletizar.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GrpParametro,System.ComponentModel.ISupportInitialize).EndInit
        Me.GrpParametro.ResumeLayout(false)
        CType(Me.Grid,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GrdP,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemTextEdit2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemSpinEdit1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemDateEdit1.CalendarTimeProperties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemDateEdit1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemCheckEdit1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemTimeEdit1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GridView2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GrpStock2,System.ComponentModel.ISupportInitialize).EndInit
        Me.GrpStock2.ResumeLayout(false)
        Me.GrpStock2.PerformLayout
        CType(Me.dtmFechaManufactura.Properties.CalendarTimeProperties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dtmFechaManufactura.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dtmFechaIngreso.Properties.CalendarTimeProperties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dtmFechaIngreso.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtAniada.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtSerial.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtLicPlate.Properties,System.ComponentModel.ISupportInitialize).EndInit
        Me.Series.ResumeLayout(false)
        CType(Me.GrpSerie,System.ComponentModel.ISupportInitialize).EndInit
        Me.GrpSerie.ResumeLayout(false)
        Me.GrpSerie.PerformLayout
        CType(Me.GroupControl1,System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupControl1.ResumeLayout(false)
        CType(Me.GrdSerie,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GridViewSerie,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GridView1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtSerialL.Properties,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents lblRegs As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents mnuNuevo As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuSalir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdImprimir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents chkActivos As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents DTBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DSPR As DSPR
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar2 As DevExpress.XtraBars.Bar
    Friend WithEvents cmdAcept As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarStaticItem1 As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents cmdCancel As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarListItem1 As DevExpress.XtraBars.BarListItem
    Friend WithEvents BarEditItem1 As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemTextEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents xtrCapParametros As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents Stockk As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GrpSeries As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtSerieFinal As NumericUpDown
    Friend WithEvents txtSerieInicial As NumericUpDown
    Friend WithEvents GrpTemperatura As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblTempTolerancia As Label
    Friend WithEvents txtTemperaturaReal As NumericUpDown
    Friend WithEvents txtTemperaturaReferencia As NumericUpDown
    Friend WithEvents GrpPeso As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cmbPresentacion As ComboBox
    Friend WithEvents lblToleranciaPeso As Label
    Friend WithEvents txtPesoReal As NumericUpDown
    Friend WithEvents txtPesoReferencia As NumericUpDown
    Friend WithEvents GrpStock2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GrpParametro As DevExpress.XtraEditors.GroupControl
    Private WithEvents Grid As DevExpress.XtraGrid.GridControl
    Private WithEvents GrdP As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colIdParametro As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDescripcion As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTexto As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemTextEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents colNumerico As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemSpinEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents colFecha As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemDateEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents colLogico As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents colTipoParametro As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIdParametroDet As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemTimeEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit
    Friend WithEvents lblLicPlate As Label
    Friend WithEvents lblSerial As Label
    Friend WithEvents lblFechaManufactura As Label
    Friend WithEvents lblAniada As Label
    Friend WithEvents txtAniada As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtSerial As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtLicPlate As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Series As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GrpSerie As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cmdGuardar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GrdSerie As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewSerie As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents txtSerialL As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents chkGeneraLPAuto As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents grpConfigPallet As GroupBox
    Friend WithEvents txtCamasPorTarima As NumericUpDown
    Friend WithEvents txtCajasPorCama As NumericUpDown
    Friend WithEvents lblY As Label
    Friend WithEvents lblX As Label
    Friend WithEvents chkPermitirPaletizar As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkPaletizarPres As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents dtmFechaManufactura As DevExpress.XtraEditors.DateEdit
    Friend WithEvents dtmFechaIngreso As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lblFactor As Label
End Class
