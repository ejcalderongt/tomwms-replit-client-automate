<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmCambioUbicacion
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If components IsNot Nothing Then
                    components.Dispose()
                End If
                If pObjDet IsNot Nothing Then
                    pObjDet.Dispose()
                    pObjDet = Nothing
                End If
                If DT IsNot Nothing Then
                    DT.Dispose()
                    DT = Nothing
                End If
                If pObjStockMov IsNot Nothing Then
                    pObjStockMov.Dispose()
                    pObjStockMov = Nothing
                End If
                If gBeTransUbicacinHhDet IsNot Nothing Then
                    gBeTransUbicacinHhDet.Dispose()
                    gBeTransUbicacinHhDet = Nothing
                End If
                If gBeTransubicacionHHEnc IsNot Nothing Then
                    gBeTransubicacionHHEnc.Dispose()
                    gBeTransubicacionHHEnc = Nothing
                End If
                If gBeTareaHh IsNot Nothing Then
                    gBeTareaHh.Dispose()
                    gBeTareaHh = Nothing
                End If
                If gBeMotivoUbicacion IsNot Nothing Then
                    gBeMotivoUbicacion.Dispose()
                    gBeMotivoUbicacion = Nothing
                End If
                If BePres IsNot Nothing Then
                    BePres.Dispose()
                    BePres = Nothing
                End If
                If pObjVWStock IsNot Nothing Then
                    pObjVWStock.Dispose()
                    pObjVWStock = Nothing
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim IdPropietarioLabel As System.Windows.Forms.Label
        Dim Label9 As System.Windows.Forms.Label
        Dim Label24 As System.Windows.Forms.Label
        Dim lblCantidad As System.Windows.Forms.Label
        Dim Label16 As System.Windows.Forms.Label
        Dim Label17 As System.Windows.Forms.Label
        Dim Label10 As System.Windows.Forms.Label
        Dim Label8 As System.Windows.Forms.Label
        Dim Label13 As System.Windows.Forms.Label
        Dim Label19 As System.Windows.Forms.Label
        Dim Label18 As System.Windows.Forms.Label
        Dim Label22 As System.Windows.Forms.Label
        Dim Label21 As System.Windows.Forms.Label
        Dim Label20 As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim CodigoLabel As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim Label5 As System.Windows.Forms.Label
        Dim ActivoLabel As System.Windows.Forms.Label
        Dim Label6 As System.Windows.Forms.Label
        Dim Label15 As System.Windows.Forms.Label
        Dim Label14 As System.Windows.Forms.Label
        Dim Label25 As System.Windows.Forms.Label
        Dim Label11 As System.Windows.Forms.Label
        Dim Label7 As System.Windows.Forms.Label
        Dim Label12 As System.Windows.Forms.Label
        Dim Label23 As System.Windows.Forms.Label
        Dim Label26 As System.Windows.Forms.Label
        Dim Label28 As System.Windows.Forms.Label
        Dim Fec_agrLabel As System.Windows.Forms.Label
        Dim User_modLabel As System.Windows.Forms.Label
        Dim User_agrLabel As System.Windows.Forms.Label
        Dim Fec_modLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCambioUbicacion))
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuAsignacion = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuImprimir1 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuLiberarStockNoProcesado = New DevExpress.XtraBars.BarButtonItem()
        Me.lblRegs = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuImportarListaCambioUbic = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuPendiente = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdDescargarPlantilla = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdEliminarDocumento = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar1 = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.DataBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsOperadorUbicacion = New TOMWMS.DsOperadorUbicacion()
        Me.TarimasDisponiblesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.GroupControl7 = New DevExpress.XtraEditors.GroupControl()
        Me.prg = New System.Windows.Forms.ProgressBar()
        Me.cmdQuitar = New System.Windows.Forms.Button()
        Me.cmdAgregar = New System.Windows.Forms.Button()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.lvTarimasDisponibles = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.lvTarimasUsadas = New System.Windows.Forms.ListView()
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GroupControl6 = New DevExpress.XtraEditors.GroupControl()
        Me.Grid = New DevExpress.XtraGrid.GridControl()
        Me.GrdOperadorBobega = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.Selección = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.IdTransUbicHhOp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Operador = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IdOperadorBodega = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ToolStripP = New System.Windows.Forms.ToolStrip()
        Me.cmdNewP = New System.Windows.Forms.ToolStripButton()
        Me.cmdGuardar = New System.Windows.Forms.ToolStripButton()
        Me.mnuEliminarDet = New System.Windows.Forms.ToolStripButton()
        Me.groupCambioDeEstado = New DevExpress.XtraEditors.GroupControl()
        Me.txtIdEstado = New DevExpress.XtraEditors.TextEdit()
        Me.lnkCambioDeEstado = New System.Windows.Forms.LinkLabel()
        Me.txtNombreEstado = New DevExpress.XtraEditors.TextEdit()
        Me.GroupControl4 = New DevExpress.XtraEditors.GroupControl()
        Me.txtColor = New DevExpress.XtraEditors.TextEdit()
        Me.txtTalla = New DevExpress.XtraEditors.TextEdit()
        Me.txtLicPlate = New DevExpress.XtraEditors.TextEdit()
        Me.cmbOperadores = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblCantRef = New System.Windows.Forms.Label()
        Me.lblVolumenProducto = New System.Windows.Forms.Label()
        Me.txtLote = New DevExpress.XtraEditors.TextEdit()
        Me.txtIngreso = New DevExpress.XtraEditors.TextEdit()
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel()
        Me.txtPresentacion = New DevExpress.XtraEditors.TextEdit()
        Me.txtAñada = New DevExpress.XtraEditors.TextEdit()
        Me.txtUnidadMedida = New DevExpress.XtraEditors.TextEdit()
        Me.txtEstado = New DevExpress.XtraEditors.TextEdit()
        Me.txtVence = New DevExpress.XtraEditors.TextEdit()
        Me.txtSerie = New DevExpress.XtraEditors.TextEdit()
        Me.txtProducto = New DevExpress.XtraEditors.TextEdit()
        Me.txtIdStock = New DevExpress.XtraEditors.TextEdit()
        Me.txtIdOrigen = New DevExpress.XtraEditors.TextEdit()
        Me.GroupControl5 = New DevExpress.XtraEditors.GroupControl()
        Me.lblItemBandera = New System.Windows.Forms.Label()
        Me.txtCantidad = New System.Windows.Forms.NumericUpDown()
        Me.lblVolumenUbicacion = New System.Windows.Forms.Label()
        Me.lblFactor = New System.Windows.Forms.Label()
        Me.txtIdUbicacionDestino = New DevExpress.XtraEditors.TextEdit()
        Me.chkActivoDet = New DevExpress.XtraEditors.CheckEdit()
        Me.txtUbicacionDestino = New DevExpress.XtraEditors.TextEdit()
        Me.lnkUbicacionDestino = New System.Windows.Forms.LinkLabel()
        Me.chkRealizadoDet = New DevExpress.XtraEditors.CheckEdit()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.txtNoDocumento = New System.Windows.Forms.TextBox()
        Me.dtpFechaFin = New DevExpress.XtraEditors.DateEdit()
        Me.dtpFechaInicio = New DevExpress.XtraEditors.DateEdit()
        Me.cmbPropietarioBodega = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbBodega = New DevExpress.XtraEditors.LookUpEdit()
        Me.groupPrioridad = New System.Windows.Forms.GroupBox()
        Me.rbAlto = New System.Windows.Forms.RadioButton()
        Me.rbBajo = New System.Windows.Forms.RadioButton()
        Me.rbMedio = New System.Windows.Forms.RadioButton()
        Me.pbAlto = New System.Windows.Forms.PictureBox()
        Me.pbMedio = New System.Windows.Forms.PictureBox()
        Me.pbBajo = New System.Windows.Forms.PictureBox()
        Me.txtNombreMotivoUbicacion = New DevExpress.XtraEditors.TextEdit()
        Me.lnkMotivoUbicacion = New System.Windows.Forms.LinkLabel()
        Me.txtIdMotivoUbicacion = New DevExpress.XtraEditors.TextEdit()
        Me.lblEstado = New System.Windows.Forms.Label()
        Me.lbl = New System.Windows.Forms.Label()
        Me.chkUbicacionConHh = New DevExpress.XtraEditors.CheckEdit()
        Me.chkOperadorPorlinea = New DevExpress.XtraEditors.CheckEdit()
        Me.chkActivo = New DevExpress.XtraEditors.CheckEdit()
        Me.txtObservacion = New System.Windows.Forms.TextBox()
        Me.dtpHoraFin = New System.Windows.Forms.DateTimePicker()
        Me.dtpHoraInicio = New System.Windows.Forms.DateTimePicker()
        Me.tabDatos = New DevExpress.XtraTab.XtraTabControl()
        Me.xtabGeneral = New DevExpress.XtraTab.XtraTabPage()
        Me.xTabDetalle = New DevExpress.XtraTab.XtraTabPage()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.grdDetalle = New DevExpress.XtraGrid.GridControl()
        Me.GridViewDet = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView4 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.xtabOperador = New DevExpress.XtraTab.XtraTabPage()
        Me.xtabTarima = New DevExpress.XtraTab.XtraTabPage()
        Me.XtraTabPage1 = New DevExpress.XtraTab.XtraTabPage()
        Me.grdStockReservado = New DevExpress.XtraGrid.GridControl()
        Me.GridView6 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.Fec_agrDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_agrTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_modDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_modTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.lblTalla = New DevExpress.XtraEditors.LabelControl()
        Me.lblColor = New DevExpress.XtraEditors.LabelControl()
        IdPropietarioLabel = New System.Windows.Forms.Label()
        Label9 = New System.Windows.Forms.Label()
        Label24 = New System.Windows.Forms.Label()
        lblCantidad = New System.Windows.Forms.Label()
        Label16 = New System.Windows.Forms.Label()
        Label17 = New System.Windows.Forms.Label()
        Label10 = New System.Windows.Forms.Label()
        Label8 = New System.Windows.Forms.Label()
        Label13 = New System.Windows.Forms.Label()
        Label19 = New System.Windows.Forms.Label()
        Label18 = New System.Windows.Forms.Label()
        Label22 = New System.Windows.Forms.Label()
        Label21 = New System.Windows.Forms.Label()
        Label20 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        CodigoLabel = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        Label5 = New System.Windows.Forms.Label()
        ActivoLabel = New System.Windows.Forms.Label()
        Label6 = New System.Windows.Forms.Label()
        Label15 = New System.Windows.Forms.Label()
        Label14 = New System.Windows.Forms.Label()
        Label25 = New System.Windows.Forms.Label()
        Label11 = New System.Windows.Forms.Label()
        Label7 = New System.Windows.Forms.Label()
        Label12 = New System.Windows.Forms.Label()
        Label23 = New System.Windows.Forms.Label()
        Label26 = New System.Windows.Forms.Label()
        Label28 = New System.Windows.Forms.Label()
        Fec_agrLabel = New System.Windows.Forms.Label()
        User_modLabel = New System.Windows.Forms.Label()
        User_agrLabel = New System.Windows.Forms.Label()
        Fec_modLabel = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsOperadorUbicacion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TarimasDisponiblesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl7.SuspendLayout()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl6.SuspendLayout()
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrdOperadorBobega, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStripP.SuspendLayout()
        CType(Me.groupCambioDeEstado, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupCambioDeEstado.SuspendLayout()
        CType(Me.txtIdEstado.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreEstado.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl4.SuspendLayout()
        CType(Me.txtColor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTalla.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLicPlate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbOperadores.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIngreso.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPresentacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAñada.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUnidadMedida.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEstado.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVence.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtProducto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdStock.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdOrigen.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl5.SuspendLayout()
        CType(Me.txtCantidad, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdUbicacionDestino.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivoDet.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUbicacionDestino.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRealizadoDet.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.dtpFechaFin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFechaFin.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFechaInicio.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFechaInicio.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPropietarioBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupPrioridad.SuspendLayout()
        CType(Me.pbAlto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbMedio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbBajo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreMotivoUbicacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdMotivoUbicacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkUbicacionConHh.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkOperadorPorlinea.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tabDatos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabDatos.SuspendLayout()
        Me.xtabGeneral.SuspendLayout()
        Me.xTabDetalle.SuspendLayout()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        CType(Me.grdDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewDet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtabOperador.SuspendLayout()
        Me.xtabTarima.SuspendLayout()
        Me.XtraTabPage1.SuspendLayout()
        CType(Me.grdStockReservado, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerBottom.SuspendLayout()
        Me.DockPanel1.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'IdPropietarioLabel
        '
        IdPropietarioLabel.AutoSize = True
        IdPropietarioLabel.Location = New System.Drawing.Point(9, 48)
        IdPropietarioLabel.Name = "IdPropietarioLabel"
        IdPropietarioLabel.Size = New System.Drawing.Size(66, 16)
        IdPropietarioLabel.TabIndex = 0
        IdPropietarioLabel.Text = "Operador:"
        '
        'Label9
        '
        Label9.AutoSize = True
        Label9.Location = New System.Drawing.Point(15, 144)
        Label9.Name = "Label9"
        Label9.Size = New System.Drawing.Size(67, 16)
        Label9.TabIndex = 7
        Label9.Text = "Realizado:"
        '
        'Label24
        '
        Label24.AutoSize = True
        Label24.Location = New System.Drawing.Point(161, 144)
        Label24.Name = "Label24"
        Label24.Size = New System.Drawing.Size(46, 16)
        Label24.TabIndex = 9
        Label24.Text = "Activo:"
        '
        'lblCantidad
        '
        lblCantidad.AutoSize = True
        lblCantidad.Location = New System.Drawing.Point(14, 66)
        lblCantidad.Name = "lblCantidad"
        lblCantidad.Size = New System.Drawing.Size(62, 16)
        lblCantidad.TabIndex = 3
        lblCantidad.Text = "Cantidad:"
        '
        'Label16
        '
        Label16.AutoSize = True
        Label16.Location = New System.Drawing.Point(423, 81)
        Label16.Name = "Label16"
        Label16.Size = New System.Drawing.Size(78, 16)
        Label16.TabIndex = 4
        Label16.Text = "Ubic Origen:"
        '
        'Label17
        '
        Label17.AutoSize = True
        Label17.Location = New System.Drawing.Point(423, 150)
        Label17.Name = "Label17"
        Label17.Size = New System.Drawing.Size(50, 16)
        Label17.TabIndex = 12
        Label17.Text = "Estado:"
        '
        'Label10
        '
        Label10.AutoSize = True
        Label10.Location = New System.Drawing.Point(423, 117)
        Label10.Name = "Label10"
        Label10.Size = New System.Drawing.Size(47, 16)
        Label10.TabIndex = 8
        Label10.Text = "Vence:"
        '
        'Label8
        '
        Label8.AutoSize = True
        Label8.Location = New System.Drawing.Point(9, 118)
        Label8.Name = "Label8"
        Label8.Size = New System.Drawing.Size(62, 16)
        Label8.TabIndex = 6
        Label8.Text = "Producto:"
        '
        'Label13
        '
        Label13.AutoSize = True
        Label13.Location = New System.Drawing.Point(9, 150)
        Label13.Name = "Label13"
        Label13.Size = New System.Drawing.Size(42, 16)
        Label13.TabIndex = 10
        Label13.Text = "Serie:"
        '
        'Label19
        '
        Label19.AutoSize = True
        Label19.Location = New System.Drawing.Point(9, 250)
        Label19.Name = "Label19"
        Label19.Size = New System.Drawing.Size(62, 16)
        Label19.TabIndex = 22
        Label19.Text = "U.M. Bas:"
        '
        'Label18
        '
        Label18.AutoSize = True
        Label18.Location = New System.Drawing.Point(9, 183)
        Label18.Name = "Label18"
        Label18.Size = New System.Drawing.Size(48, 16)
        Label18.TabIndex = 14
        Label18.Text = "Añada:"
        '
        'Label22
        '
        Label22.AutoSize = True
        Label22.Location = New System.Drawing.Point(423, 213)
        Label22.Name = "Label22"
        Label22.Size = New System.Drawing.Size(85, 16)
        Label22.TabIndex = 20
        Label22.Text = "Presentación:"
        '
        'Label21
        '
        Label21.AutoSize = True
        Label21.Location = New System.Drawing.Point(9, 214)
        Label21.Name = "Label21"
        Label21.Size = New System.Drawing.Size(55, 16)
        Label21.TabIndex = 18
        Label21.Text = "Ingreso:"
        '
        'Label20
        '
        Label20.AutoSize = True
        Label20.Location = New System.Drawing.Point(423, 182)
        Label20.Name = "Label20"
        Label20.Size = New System.Drawing.Size(36, 16)
        Label20.TabIndex = 16
        Label20.Text = "Lote:"
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(423, 277)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(70, 16)
        Label2.TabIndex = 24
        Label2.Text = "Disponible:"
        '
        'CodigoLabel
        '
        CodigoLabel.AutoSize = True
        CodigoLabel.Location = New System.Drawing.Point(15, 32)
        CodigoLabel.Name = "CodigoLabel"
        CodigoLabel.Size = New System.Drawing.Size(51, 16)
        CodigoLabel.TabIndex = 0
        CodigoLabel.Text = "Código:"
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(15, 124)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(74, 16)
        Label1.TabIndex = 6
        Label1.Text = "Propietario:"
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.Location = New System.Drawing.Point(15, 192)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(80, 16)
        Label3.TabIndex = 11
        Label3.Text = "Fecha Inicio:"
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(15, 225)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(67, 16)
        Label4.TabIndex = 14
        Label4.Text = "Fecha Fin:"
        '
        'Label5
        '
        Label5.AutoSize = True
        Label5.Location = New System.Drawing.Point(15, 280)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(82, 16)
        Label5.TabIndex = 17
        Label5.Text = "Observación:"
        '
        'ActivoLabel
        '
        ActivoLabel.AutoSize = True
        ActivoLabel.Location = New System.Drawing.Point(5, 389)
        ActivoLabel.Name = "ActivoLabel"
        ActivoLabel.Size = New System.Drawing.Size(46, 16)
        ActivoLabel.TabIndex = 19
        ActivoLabel.Text = "Activo:"
        '
        'Label6
        '
        Label6.AutoSize = True
        Label6.Location = New System.Drawing.Point(15, 89)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(54, 16)
        Label6.TabIndex = 4
        Label6.Text = "Bodega:"
        '
        'Label15
        '
        Label15.AutoSize = True
        Label15.Location = New System.Drawing.Point(5, 423)
        Label15.Name = "Label15"
        Label15.Size = New System.Drawing.Size(120, 16)
        Label15.TabIndex = 22
        Label15.Text = "Operador por linea:"
        '
        'Label14
        '
        Label14.AutoSize = True
        Label14.Location = New System.Drawing.Point(5, 455)
        Label14.Name = "Label14"
        Label14.Size = New System.Drawing.Size(108, 16)
        Label14.TabIndex = 23
        Label14.Text = "Ubicacion con hh:"
        '
        'Label25
        '
        Label25.AutoSize = True
        Label25.Location = New System.Drawing.Point(15, 59)
        Label25.Name = "Label25"
        Label25.Size = New System.Drawing.Size(50, 16)
        Label25.TabIndex = 3
        Label25.Text = "Estado:"
        '
        'Label11
        '
        Label11.AutoSize = True
        Label11.Location = New System.Drawing.Point(15, 114)
        Label11.Name = "Label11"
        Label11.Size = New System.Drawing.Size(48, 16)
        Label11.TabIndex = 5
        Label11.Text = "Factor:"
        '
        'Label7
        '
        Label7.AutoSize = True
        Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label7.Location = New System.Drawing.Point(173, 105)
        Label7.Name = "Label7"
        Label7.Size = New System.Drawing.Size(147, 17)
        Label7.TabIndex = 7
        Label7.Text = "Tarimas disponibles:"
        '
        'Label12
        '
        Label12.AutoSize = True
        Label12.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label12.Location = New System.Drawing.Point(602, 105)
        Label12.Name = "Label12"
        Label12.Size = New System.Drawing.Size(128, 17)
        Label12.TabIndex = 8
        Label12.Text = "Tarimas a utilizar:"
        '
        'Label23
        '
        Label23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Label23.Dock = System.Windows.Forms.DockStyle.Top
        Label23.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Label23.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label23.ForeColor = System.Drawing.Color.Teal
        Label23.Location = New System.Drawing.Point(2, 28)
        Label23.Name = "Label23"
        Label23.Size = New System.Drawing.Size(1270, 45)
        Label23.TabIndex = 9
        Label23.Text = "Tarimas a utilizar en proceso de ubicación."
        Label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label26
        '
        Label26.AutoSize = True
        Label26.Location = New System.Drawing.Point(423, 245)
        Label26.Name = "Label26"
        Label26.Size = New System.Drawing.Size(59, 16)
        Label26.TabIndex = 29
        Label26.Text = "Lic Plate:"
        '
        'Label28
        '
        Label28.AutoSize = True
        Label28.Location = New System.Drawing.Point(15, 253)
        Label28.Name = "Label28"
        Label28.Size = New System.Drawing.Size(99, 16)
        Label28.TabIndex = 30
        Label28.Text = "No. Documento:"
        '
        'Fec_agrLabel
        '
        Fec_agrLabel.AutoSize = True
        Fec_agrLabel.Location = New System.Drawing.Point(187, 57)
        Fec_agrLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Fec_agrLabel.Name = "Fec_agrLabel"
        Fec_agrLabel.Size = New System.Drawing.Size(91, 16)
        Fec_agrLabel.TabIndex = 12
        Fec_agrLabel.Text = "Fecha Agregó:"
        '
        'User_modLabel
        '
        User_modLabel.AutoSize = True
        User_modLabel.Location = New System.Drawing.Point(666, 24)
        User_modLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        User_modLabel.Name = "User_modLabel"
        User_modLabel.Size = New System.Drawing.Size(106, 16)
        User_modLabel.TabIndex = 11
        User_modLabel.Text = "Usuario Modificó:"
        '
        'User_agrLabel
        '
        User_agrLabel.AutoSize = True
        User_agrLabel.Location = New System.Drawing.Point(185, 25)
        User_agrLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        User_agrLabel.Name = "User_agrLabel"
        User_agrLabel.Size = New System.Drawing.Size(100, 16)
        User_agrLabel.TabIndex = 8
        User_agrLabel.Text = "Usuario Agregó:"
        '
        'Fec_modLabel
        '
        Fec_modLabel.AutoSize = True
        Fec_modLabel.Location = New System.Drawing.Point(671, 57)
        Fec_modLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Fec_modLabel.Name = "Fec_modLabel"
        Fec_modLabel.Size = New System.Drawing.Size(97, 16)
        Fec_modLabel.TabIndex = 14
        Fec_modLabel.Text = "Fecha Modificó:"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuGuardar, Me.mnuActualizar, Me.mnuEliminar, Me.mnuAsignacion, Me.mnuImprimir1, Me.mnuLiberarStockNoProcesado, Me.lblRegs, Me.mnuImportarListaCambioUbic, Me.mnuPendiente, Me.cmdDescargarPlantilla, Me.cmdEliminarDocumento})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 13
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1276, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar1
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
        'mnuImprimir1
        '
        Me.mnuImprimir1.Caption = "Imprimir"
        Me.mnuImprimir1.Id = 6
        Me.mnuImprimir1.ImageOptions.SvgImage = CType(resources.GetObject("mnuImprimir1.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuImprimir1.Name = "mnuImprimir1"
        '
        'mnuLiberarStockNoProcesado
        '
        Me.mnuLiberarStockNoProcesado.Caption = "Liberar stock no procesado"
        Me.mnuLiberarStockNoProcesado.Id = 7
        Me.mnuLiberarStockNoProcesado.ImageOptions.SvgImage = CType(resources.GetObject("mnuLiberarStockNoProcesado.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuLiberarStockNoProcesado.Name = "mnuLiberarStockNoProcesado"
        '
        'lblRegs
        '
        Me.lblRegs.Caption = "Registros: 0"
        Me.lblRegs.Id = 8
        Me.lblRegs.Name = "lblRegs"
        '
        'mnuImportarListaCambioUbic
        '
        Me.mnuImportarListaCambioUbic.Caption = "Importar Lista Cambio de Ubicaciones"
        Me.mnuImportarListaCambioUbic.Id = 9
        Me.mnuImportarListaCambioUbic.ImageOptions.SvgImage = CType(resources.GetObject("mnuImportarListaCambioUbic.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuImportarListaCambioUbic.Name = "mnuImportarListaCambioUbic"
        '
        'mnuPendiente
        '
        Me.mnuPendiente.Caption = "Enviar a Handheld"
        Me.mnuPendiente.Id = 10
        Me.mnuPendiente.ImageOptions.SvgImage = CType(resources.GetObject("mnuPendiente.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuPendiente.Name = "mnuPendiente"
        '
        'cmdDescargarPlantilla
        '
        Me.cmdDescargarPlantilla.Caption = "Descargar plantilla"
        Me.cmdDescargarPlantilla.Id = 11
        Me.cmdDescargarPlantilla.ImageOptions.SvgImage = CType(resources.GetObject("cmdDescargarPlantilla.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdDescargarPlantilla.Name = "cmdDescargarPlantilla"
        '
        'cmdEliminarDocumento
        '
        Me.cmdEliminarDocumento.Caption = "Eliminar documento"
        Me.cmdEliminarDocumento.Id = 12
        Me.cmdEliminarDocumento.ImageOptions.SvgImage = CType(resources.GetObject("cmdEliminarDocumento.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdEliminarDocumento.Name = "cmdEliminarDocumento"
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
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdEliminarDocumento)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuImprimir1)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuLiberarStockNoProcesado)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuPendiente)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuImportarListaCambioUbic)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdDescargarPlantilla)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar1
        '
        Me.RibbonStatusBar1.ItemLinks.Add(Me.lblRegs)
        Me.RibbonStatusBar1.Location = New System.Drawing.Point(0, 931)
        Me.RibbonStatusBar1.Name = "RibbonStatusBar1"
        Me.RibbonStatusBar1.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar1.Size = New System.Drawing.Size(1276, 30)
        '
        'DataBindingSource
        '
        Me.DataBindingSource.DataMember = "Data"
        Me.DataBindingSource.DataSource = Me.DsOperadorUbicacion
        '
        'DsOperadorUbicacion
        '
        Me.DsOperadorUbicacion.DataSetName = "DsOperadorUbicacion"
        Me.DsOperadorUbicacion.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'TarimasDisponiblesBindingSource
        '
        Me.TarimasDisponiblesBindingSource.DataMember = "TarimasDisponibles"
        Me.TarimasDisponiblesBindingSource.DataSource = Me.DsOperadorUbicacion
        '
        'GroupControl7
        '
        Me.GroupControl7.Controls.Add(Label23)
        Me.GroupControl7.Controls.Add(Label12)
        Me.GroupControl7.Controls.Add(Label7)
        Me.GroupControl7.Controls.Add(Me.prg)
        Me.GroupControl7.Controls.Add(Me.cmdQuitar)
        Me.GroupControl7.Controls.Add(Me.cmdAgregar)
        Me.GroupControl7.Controls.Add(Me.CheckBox2)
        Me.GroupControl7.Controls.Add(Me.lvTarimasDisponibles)
        Me.GroupControl7.Controls.Add(Me.CheckBox1)
        Me.GroupControl7.Controls.Add(Me.lvTarimasUsadas)
        Me.GroupControl7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl7.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl7.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl7.Name = "GroupControl7"
        Me.GroupControl7.Size = New System.Drawing.Size(1274, 682)
        Me.GroupControl7.TabIndex = 0
        Me.GroupControl7.Text = "Tarimas"
        '
        'prg
        '
        Me.prg.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.prg.Location = New System.Drawing.Point(2, 652)
        Me.prg.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.prg.Name = "prg"
        Me.prg.Size = New System.Drawing.Size(1270, 28)
        Me.prg.TabIndex = 6
        Me.prg.Visible = False
        '
        'cmdQuitar
        '
        Me.cmdQuitar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdQuitar.Location = New System.Drawing.Point(479, 256)
        Me.cmdQuitar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmdQuitar.Name = "cmdQuitar"
        Me.cmdQuitar.Size = New System.Drawing.Size(87, 28)
        Me.cmdQuitar.TabIndex = 2
        Me.cmdQuitar.Text = "<<"
        Me.cmdQuitar.UseVisualStyleBackColor = True
        '
        'cmdAgregar
        '
        Me.cmdAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAgregar.Location = New System.Drawing.Point(479, 209)
        Me.cmdAgregar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmdAgregar.Name = "cmdAgregar"
        Me.cmdAgregar.Size = New System.Drawing.Size(87, 28)
        Me.cmdAgregar.TabIndex = 1
        Me.cmdAgregar.Text = ">>"
        Me.cmdAgregar.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBox2.ForeColor = System.Drawing.Color.DimGray
        Me.CheckBox2.Location = New System.Drawing.Point(605, 421)
        Me.CheckBox2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(84, 21)
        Me.CheckBox2.TabIndex = 5
        Me.CheckBox2.Text = "Todos"
        '
        'lvTarimasDisponibles
        '
        Me.lvTarimasDisponibles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lvTarimasDisponibles.CheckBoxes = True
        Me.lvTarimasDisponibles.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader5, Me.ColumnHeader2})
        Me.lvTarimasDisponibles.FullRowSelect = True
        Me.lvTarimasDisponibles.HideSelection = False
        Me.lvTarimasDisponibles.Location = New System.Drawing.Point(176, 124)
        Me.lvTarimasDisponibles.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lvTarimasDisponibles.Name = "lvTarimasDisponibles"
        Me.lvTarimasDisponibles.Size = New System.Drawing.Size(280, 289)
        Me.lvTarimasDisponibles.TabIndex = 0
        Me.lvTarimasDisponibles.UseCompatibleStateImageBehavior = False
        Me.lvTarimasDisponibles.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "IdTarima"
        Me.ColumnHeader1.Width = 85
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Codigo"
        Me.ColumnHeader5.Width = 100
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Descripcion"
        Me.ColumnHeader2.Width = 150
        '
        'CheckBox1
        '
        Me.CheckBox1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBox1.ForeColor = System.Drawing.Color.DimGray
        Me.CheckBox1.Location = New System.Drawing.Point(176, 421)
        Me.CheckBox1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(84, 21)
        Me.CheckBox1.TabIndex = 4
        Me.CheckBox1.Text = "Todos"
        '
        'lvTarimasUsadas
        '
        Me.lvTarimasUsadas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lvTarimasUsadas.CheckBoxes = True
        Me.lvTarimasUsadas.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader6})
        Me.lvTarimasUsadas.FullRowSelect = True
        Me.lvTarimasUsadas.HideSelection = False
        Me.lvTarimasUsadas.Location = New System.Drawing.Point(605, 124)
        Me.lvTarimasUsadas.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lvTarimasUsadas.Name = "lvTarimasUsadas"
        Me.lvTarimasUsadas.Size = New System.Drawing.Size(280, 289)
        Me.lvTarimasUsadas.TabIndex = 3
        Me.lvTarimasUsadas.UseCompatibleStateImageBehavior = False
        Me.lvTarimasUsadas.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "IdTarima"
        Me.ColumnHeader3.Width = 85
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Codigo"
        Me.ColumnHeader4.Width = 69
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Descripcion"
        Me.ColumnHeader6.Width = 150
        '
        'GridView2
        '
        Me.GridView2.Name = "GridView2"
        '
        'GroupControl6
        '
        Me.GroupControl6.Controls.Add(Me.Grid)
        Me.GroupControl6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl6.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl6.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl6.Name = "GroupControl6"
        Me.GroupControl6.Size = New System.Drawing.Size(1274, 682)
        Me.GroupControl6.TabIndex = 0
        Me.GroupControl6.Text = "Selección de Bodega"
        '
        'Grid
        '
        Me.Grid.Cursor = System.Windows.Forms.Cursors.Default
        Me.Grid.DataSource = Me.DataBindingSource
        Me.Grid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Grid.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Grid.Location = New System.Drawing.Point(2, 28)
        Me.Grid.MainView = Me.GrdOperadorBobega
        Me.Grid.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Grid.Name = "Grid"
        Me.Grid.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
        Me.Grid.Size = New System.Drawing.Size(1270, 652)
        Me.Grid.TabIndex = 0
        Me.Grid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GrdOperadorBobega, Me.GridView3})
        '
        'GrdOperadorBobega
        '
        Me.GrdOperadorBobega.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.Selección, Me.IdTransUbicHhOp, Me.Operador, Me.IdOperadorBodega})
        Me.GrdOperadorBobega.GridControl = Me.Grid
        Me.GrdOperadorBobega.Name = "GrdOperadorBobega"
        Me.GrdOperadorBobega.OptionsFind.AlwaysVisible = True
        '
        'Selección
        '
        Me.Selección.Caption = "Asignar"
        Me.Selección.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.Selección.FieldName = "Selección"
        Me.Selección.Name = "Selección"
        Me.Selección.Visible = True
        Me.Selección.VisibleIndex = 0
        Me.Selección.Width = 141
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        '
        'IdTransUbicHhOp
        '
        Me.IdTransUbicHhOp.Caption = "Asignación"
        Me.IdTransUbicHhOp.FieldName = "IdTransUbicHhOp"
        Me.IdTransUbicHhOp.Name = "IdTransUbicHhOp"
        Me.IdTransUbicHhOp.OptionsColumn.ReadOnly = True
        Me.IdTransUbicHhOp.Width = 141
        '
        'Operador
        '
        Me.Operador.Caption = "Operador"
        Me.Operador.FieldName = "Operador"
        Me.Operador.Name = "Operador"
        Me.Operador.OptionsColumn.ReadOnly = True
        Me.Operador.Visible = True
        Me.Operador.VisibleIndex = 1
        Me.Operador.Width = 396
        '
        'IdOperadorBodega
        '
        Me.IdOperadorBodega.Caption = "IdOperadorBodega"
        Me.IdOperadorBodega.FieldName = "IdOperadorBodega"
        Me.IdOperadorBodega.Name = "IdOperadorBodega"
        Me.IdOperadorBodega.OptionsColumn.ReadOnly = True
        '
        'GridView3
        '
        Me.GridView3.GridControl = Me.Grid
        Me.GridView3.Name = "GridView3"
        '
        'ToolStripP
        '
        Me.ToolStripP.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStripP.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdNewP, Me.cmdGuardar, Me.mnuEliminarDet})
        Me.ToolStripP.Location = New System.Drawing.Point(740, 0)
        Me.ToolStripP.Name = "ToolStripP"
        Me.ToolStripP.Size = New System.Drawing.Size(534, 27)
        Me.ToolStripP.TabIndex = 0
        Me.ToolStripP.Text = "ToolStrip1"
        '
        'cmdNewP
        '
        Me.cmdNewP.Image = Global.TOMWMS.My.Resources.Resources.add
        Me.cmdNewP.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdNewP.Name = "cmdNewP"
        Me.cmdNewP.Size = New System.Drawing.Size(76, 24)
        Me.cmdNewP.Text = "Nuevo"
        '
        'cmdGuardar
        '
        Me.cmdGuardar.Image = Global.TOMWMS.My.Resources.Resources.greencheck
        Me.cmdGuardar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdGuardar.Name = "cmdGuardar"
        Me.cmdGuardar.Size = New System.Drawing.Size(86, 24)
        Me.cmdGuardar.Text = "Guardar"
        '
        'mnuEliminarDet
        '
        Me.mnuEliminarDet.Image = Global.TOMWMS.My.Resources.Resources.desactivar
        Me.mnuEliminarDet.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuEliminarDet.Name = "mnuEliminarDet"
        Me.mnuEliminarDet.Size = New System.Drawing.Size(87, 24)
        Me.mnuEliminarDet.Text = "Eliminar"
        '
        'groupCambioDeEstado
        '
        Me.groupCambioDeEstado.Controls.Add(Me.txtIdEstado)
        Me.groupCambioDeEstado.Controls.Add(Me.lnkCambioDeEstado)
        Me.groupCambioDeEstado.Controls.Add(Me.txtNombreEstado)
        Me.groupCambioDeEstado.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.groupCambioDeEstado.Location = New System.Drawing.Point(740, 321)
        Me.groupCambioDeEstado.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.groupCambioDeEstado.Name = "groupCambioDeEstado"
        Me.groupCambioDeEstado.Size = New System.Drawing.Size(534, 80)
        Me.groupCambioDeEstado.TabIndex = 3
        Me.groupCambioDeEstado.Text = "Cambiar de Estado"
        '
        'txtIdEstado
        '
        Me.txtIdEstado.Location = New System.Drawing.Point(101, 36)
        Me.txtIdEstado.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtIdEstado.MenuManager = Me.RibbonControl
        Me.txtIdEstado.Name = "txtIdEstado"
        Me.txtIdEstado.Properties.Mask.EditMask = "n0"
        Me.txtIdEstado.Size = New System.Drawing.Size(77, 22)
        Me.txtIdEstado.TabIndex = 1
        '
        'lnkCambioDeEstado
        '
        Me.lnkCambioDeEstado.AutoSize = True
        Me.lnkCambioDeEstado.Location = New System.Drawing.Point(17, 36)
        Me.lnkCambioDeEstado.Name = "lnkCambioDeEstado"
        Me.lnkCambioDeEstado.Size = New System.Drawing.Size(45, 16)
        Me.lnkCambioDeEstado.TabIndex = 0
        Me.lnkCambioDeEstado.TabStop = True
        Me.lnkCambioDeEstado.Text = "Estado"
        '
        'txtNombreEstado
        '
        Me.txtNombreEstado.Location = New System.Drawing.Point(185, 36)
        Me.txtNombreEstado.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNombreEstado.MenuManager = Me.RibbonControl
        Me.txtNombreEstado.Name = "txtNombreEstado"
        Me.txtNombreEstado.Properties.Mask.EditMask = "n0"
        Me.txtNombreEstado.Properties.ReadOnly = True
        Me.txtNombreEstado.Size = New System.Drawing.Size(190, 22)
        Me.txtNombreEstado.TabIndex = 2
        '
        'GroupControl4
        '
        Me.GroupControl4.Controls.Add(Me.lblColor)
        Me.GroupControl4.Controls.Add(Me.lblTalla)
        Me.GroupControl4.Controls.Add(Me.txtColor)
        Me.GroupControl4.Controls.Add(Me.txtTalla)
        Me.GroupControl4.Controls.Add(Me.txtLicPlate)
        Me.GroupControl4.Controls.Add(Label26)
        Me.GroupControl4.Controls.Add(Me.cmbOperadores)
        Me.GroupControl4.Controls.Add(Me.lblCantRef)
        Me.GroupControl4.Controls.Add(Label2)
        Me.GroupControl4.Controls.Add(Me.lblVolumenProducto)
        Me.GroupControl4.Controls.Add(Me.txtLote)
        Me.GroupControl4.Controls.Add(Me.txtIngreso)
        Me.GroupControl4.Controls.Add(Label20)
        Me.GroupControl4.Controls.Add(IdPropietarioLabel)
        Me.GroupControl4.Controls.Add(Label21)
        Me.GroupControl4.Controls.Add(Label22)
        Me.GroupControl4.Controls.Add(Me.LinkLabel2)
        Me.GroupControl4.Controls.Add(Me.txtPresentacion)
        Me.GroupControl4.Controls.Add(Me.txtAñada)
        Me.GroupControl4.Controls.Add(Me.txtUnidadMedida)
        Me.GroupControl4.Controls.Add(Label18)
        Me.GroupControl4.Controls.Add(Label19)
        Me.GroupControl4.Controls.Add(Me.txtEstado)
        Me.GroupControl4.Controls.Add(Label13)
        Me.GroupControl4.Controls.Add(Me.txtVence)
        Me.GroupControl4.Controls.Add(Me.txtSerie)
        Me.GroupControl4.Controls.Add(Me.txtProducto)
        Me.GroupControl4.Controls.Add(Label8)
        Me.GroupControl4.Controls.Add(Label10)
        Me.GroupControl4.Controls.Add(Me.txtIdStock)
        Me.GroupControl4.Controls.Add(Label17)
        Me.GroupControl4.Controls.Add(Me.txtIdOrigen)
        Me.GroupControl4.Controls.Add(Label16)
        Me.GroupControl4.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupControl4.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl4.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl4.Name = "GroupControl4"
        Me.GroupControl4.Size = New System.Drawing.Size(740, 401)
        Me.GroupControl4.TabIndex = 1
        Me.GroupControl4.Text = "Origen"
        '
        'txtColor
        '
        Me.txtColor.Location = New System.Drawing.Point(135, 313)
        Me.txtColor.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtColor.MenuManager = Me.RibbonControl
        Me.txtColor.Name = "txtColor"
        Me.txtColor.Properties.MaskSettings.Set("mask", "n0")
        Me.txtColor.Properties.ReadOnly = True
        Me.txtColor.Size = New System.Drawing.Size(219, 22)
        Me.txtColor.TabIndex = 34
        '
        'txtTalla
        '
        Me.txtTalla.Location = New System.Drawing.Point(135, 277)
        Me.txtTalla.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtTalla.MenuManager = Me.RibbonControl
        Me.txtTalla.Name = "txtTalla"
        Me.txtTalla.Properties.MaskSettings.Set("mask", "n0")
        Me.txtTalla.Properties.ReadOnly = True
        Me.txtTalla.Size = New System.Drawing.Size(219, 22)
        Me.txtTalla.TabIndex = 32
        '
        'txtLicPlate
        '
        Me.txtLicPlate.Location = New System.Drawing.Point(514, 239)
        Me.txtLicPlate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtLicPlate.MenuManager = Me.RibbonControl
        Me.txtLicPlate.Name = "txtLicPlate"
        Me.txtLicPlate.Properties.MaskSettings.Set("mask", "n0")
        Me.txtLicPlate.Properties.ReadOnly = True
        Me.txtLicPlate.Size = New System.Drawing.Size(204, 22)
        Me.txtLicPlate.TabIndex = 30
        '
        'cmbOperadores
        '
        Me.cmbOperadores.Location = New System.Drawing.Point(135, 44)
        Me.cmbOperadores.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbOperadores.MenuManager = Me.RibbonControl
        Me.cmbOperadores.Name = "cmbOperadores"
        Me.cmbOperadores.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbOperadores.Properties.NullText = ""
        Me.cmbOperadores.Size = New System.Drawing.Size(219, 22)
        Me.cmbOperadores.TabIndex = 28
        '
        'lblCantRef
        '
        Me.lblCantRef.AutoSize = True
        Me.lblCantRef.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCantRef.Location = New System.Drawing.Point(512, 273)
        Me.lblCantRef.Name = "lblCantRef"
        Me.lblCantRef.Size = New System.Drawing.Size(19, 24)
        Me.lblCantRef.TabIndex = 25
        Me.lblCantRef.Text = "-"
        '
        'lblVolumenProducto
        '
        Me.lblVolumenProducto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblVolumenProducto.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblVolumenProducto.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVolumenProducto.Location = New System.Drawing.Point(2, 376)
        Me.lblVolumenProducto.Name = "lblVolumenProducto"
        Me.lblVolumenProducto.Size = New System.Drawing.Size(736, 23)
        Me.lblVolumenProducto.TabIndex = 26
        Me.lblVolumenProducto.Text = "0x0x0=X"
        Me.lblVolumenProducto.Visible = False
        '
        'txtLote
        '
        Me.txtLote.Location = New System.Drawing.Point(514, 177)
        Me.txtLote.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtLote.MenuManager = Me.RibbonControl
        Me.txtLote.Name = "txtLote"
        Me.txtLote.Properties.MaskSettings.Set("mask", "n0")
        Me.txtLote.Properties.ReadOnly = True
        Me.txtLote.Size = New System.Drawing.Size(204, 22)
        Me.txtLote.TabIndex = 17
        '
        'txtIngreso
        '
        Me.txtIngreso.Location = New System.Drawing.Point(135, 210)
        Me.txtIngreso.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtIngreso.MenuManager = Me.RibbonControl
        Me.txtIngreso.Name = "txtIngreso"
        Me.txtIngreso.Properties.MaskSettings.Set("mask", "n0")
        Me.txtIngreso.Properties.ReadOnly = True
        Me.txtIngreso.Size = New System.Drawing.Size(219, 22)
        Me.txtIngreso.TabIndex = 19
        '
        'LinkLabel2
        '
        Me.LinkLabel2.AutoSize = True
        Me.LinkLabel2.Location = New System.Drawing.Point(9, 82)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(38, 16)
        Me.LinkLabel2.TabIndex = 2
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "Stock"
        '
        'txtPresentacion
        '
        Me.txtPresentacion.Location = New System.Drawing.Point(514, 209)
        Me.txtPresentacion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtPresentacion.MenuManager = Me.RibbonControl
        Me.txtPresentacion.Name = "txtPresentacion"
        Me.txtPresentacion.Properties.MaskSettings.Set("mask", "n0")
        Me.txtPresentacion.Properties.ReadOnly = True
        Me.txtPresentacion.Size = New System.Drawing.Size(204, 22)
        Me.txtPresentacion.TabIndex = 21
        '
        'txtAñada
        '
        Me.txtAñada.Location = New System.Drawing.Point(135, 178)
        Me.txtAñada.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtAñada.MenuManager = Me.RibbonControl
        Me.txtAñada.Name = "txtAñada"
        Me.txtAñada.Properties.MaskSettings.Set("mask", "n0")
        Me.txtAñada.Properties.ReadOnly = True
        Me.txtAñada.Size = New System.Drawing.Size(219, 22)
        Me.txtAñada.TabIndex = 15
        '
        'txtUnidadMedida
        '
        Me.txtUnidadMedida.Location = New System.Drawing.Point(135, 242)
        Me.txtUnidadMedida.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtUnidadMedida.MenuManager = Me.RibbonControl
        Me.txtUnidadMedida.Name = "txtUnidadMedida"
        Me.txtUnidadMedida.Properties.MaskSettings.Set("mask", "n0")
        Me.txtUnidadMedida.Properties.ReadOnly = True
        Me.txtUnidadMedida.Size = New System.Drawing.Size(219, 22)
        Me.txtUnidadMedida.TabIndex = 23
        '
        'txtEstado
        '
        Me.txtEstado.Location = New System.Drawing.Point(514, 146)
        Me.txtEstado.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtEstado.MenuManager = Me.RibbonControl
        Me.txtEstado.Name = "txtEstado"
        Me.txtEstado.Properties.MaskSettings.Set("mask", "n0")
        Me.txtEstado.Properties.ReadOnly = True
        Me.txtEstado.Size = New System.Drawing.Size(204, 22)
        Me.txtEstado.TabIndex = 13
        '
        'txtVence
        '
        Me.txtVence.Location = New System.Drawing.Point(514, 113)
        Me.txtVence.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtVence.MenuManager = Me.RibbonControl
        Me.txtVence.Name = "txtVence"
        Me.txtVence.Properties.MaskSettings.Set("mask", "n0")
        Me.txtVence.Properties.ReadOnly = True
        Me.txtVence.Size = New System.Drawing.Size(204, 22)
        Me.txtVence.TabIndex = 9
        '
        'txtSerie
        '
        Me.txtSerie.Location = New System.Drawing.Point(135, 147)
        Me.txtSerie.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtSerie.MenuManager = Me.RibbonControl
        Me.txtSerie.Name = "txtSerie"
        Me.txtSerie.Properties.MaskSettings.Set("mask", "n0")
        Me.txtSerie.Properties.ReadOnly = True
        Me.txtSerie.Size = New System.Drawing.Size(219, 22)
        Me.txtSerie.TabIndex = 11
        '
        'txtProducto
        '
        Me.txtProducto.Location = New System.Drawing.Point(135, 114)
        Me.txtProducto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtProducto.MenuManager = Me.RibbonControl
        Me.txtProducto.Name = "txtProducto"
        Me.txtProducto.Properties.MaskSettings.Set("mask", "n0")
        Me.txtProducto.Properties.ReadOnly = True
        Me.txtProducto.Size = New System.Drawing.Size(219, 22)
        Me.txtProducto.TabIndex = 7
        '
        'txtIdStock
        '
        Me.txtIdStock.Location = New System.Drawing.Point(135, 79)
        Me.txtIdStock.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtIdStock.MenuManager = Me.RibbonControl
        Me.txtIdStock.Name = "txtIdStock"
        Me.txtIdStock.Properties.MaskSettings.Set("mask", "n0")
        Me.txtIdStock.Properties.ReadOnly = True
        Me.txtIdStock.Size = New System.Drawing.Size(219, 22)
        Me.txtIdStock.TabIndex = 3
        '
        'txtIdOrigen
        '
        Me.txtIdOrigen.Location = New System.Drawing.Point(514, 78)
        Me.txtIdOrigen.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtIdOrigen.MenuManager = Me.RibbonControl
        Me.txtIdOrigen.Name = "txtIdOrigen"
        Me.txtIdOrigen.Properties.MaskSettings.Set("mask", "n0")
        Me.txtIdOrigen.Properties.ReadOnly = True
        Me.txtIdOrigen.Size = New System.Drawing.Size(204, 22)
        Me.txtIdOrigen.TabIndex = 5
        '
        'GroupControl5
        '
        Me.GroupControl5.Controls.Add(Me.lblItemBandera)
        Me.GroupControl5.Controls.Add(Me.txtCantidad)
        Me.GroupControl5.Controls.Add(Me.lblVolumenUbicacion)
        Me.GroupControl5.Controls.Add(Me.lblFactor)
        Me.GroupControl5.Controls.Add(Label11)
        Me.GroupControl5.Controls.Add(lblCantidad)
        Me.GroupControl5.Controls.Add(Me.txtIdUbicacionDestino)
        Me.GroupControl5.Controls.Add(Label24)
        Me.GroupControl5.Controls.Add(Me.chkActivoDet)
        Me.GroupControl5.Controls.Add(Me.txtUbicacionDestino)
        Me.GroupControl5.Controls.Add(Me.lnkUbicacionDestino)
        Me.GroupControl5.Controls.Add(Label9)
        Me.GroupControl5.Controls.Add(Me.chkRealizadoDet)
        Me.GroupControl5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl5.Location = New System.Drawing.Point(740, 27)
        Me.GroupControl5.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl5.Name = "GroupControl5"
        Me.GroupControl5.Size = New System.Drawing.Size(534, 294)
        Me.GroupControl5.TabIndex = 2
        Me.GroupControl5.Text = "Destino"
        '
        'lblItemBandera
        '
        Me.lblItemBandera.AutoSize = True
        Me.lblItemBandera.Location = New System.Drawing.Point(276, 145)
        Me.lblItemBandera.Name = "lblItemBandera"
        Me.lblItemBandera.Size = New System.Drawing.Size(70, 16)
        Me.lblItemBandera.TabIndex = 12
        Me.lblItemBandera.Text = "En proceso"
        '
        'txtCantidad
        '
        Me.txtCantidad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCantidad.DecimalPlaces = 6
        Me.txtCantidad.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCantidad.Location = New System.Drawing.Point(101, 64)
        Me.txtCantidad.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCantidad.Maximum = New Decimal(New Integer() {10000000, 0, 0, 0})
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.Size = New System.Drawing.Size(274, 32)
        Me.txtCantidad.TabIndex = 4
        Me.txtCantidad.ThousandsSeparator = True
        '
        'lblVolumenUbicacion
        '
        Me.lblVolumenUbicacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblVolumenUbicacion.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblVolumenUbicacion.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVolumenUbicacion.Location = New System.Drawing.Point(2, 269)
        Me.lblVolumenUbicacion.Name = "lblVolumenUbicacion"
        Me.lblVolumenUbicacion.Size = New System.Drawing.Size(530, 23)
        Me.lblVolumenUbicacion.TabIndex = 11
        Me.lblVolumenUbicacion.Text = "0x0x0=X"
        Me.lblVolumenUbicacion.Visible = False
        '
        'lblFactor
        '
        Me.lblFactor.AutoSize = True
        Me.lblFactor.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.lblFactor.Location = New System.Drawing.Point(104, 111)
        Me.lblFactor.Name = "lblFactor"
        Me.lblFactor.Size = New System.Drawing.Size(16, 21)
        Me.lblFactor.TabIndex = 6
        Me.lblFactor.Text = "-"
        '
        'txtIdUbicacionDestino
        '
        Me.txtIdUbicacionDestino.Location = New System.Drawing.Point(101, 30)
        Me.txtIdUbicacionDestino.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtIdUbicacionDestino.MenuManager = Me.RibbonControl
        Me.txtIdUbicacionDestino.Name = "txtIdUbicacionDestino"
        Me.txtIdUbicacionDestino.Properties.Mask.EditMask = "n0"
        Me.txtIdUbicacionDestino.Properties.ReadOnly = True
        Me.txtIdUbicacionDestino.Size = New System.Drawing.Size(77, 22)
        Me.txtIdUbicacionDestino.TabIndex = 1
        '
        'chkActivoDet
        '
        Me.chkActivoDet.EditValue = True
        Me.chkActivoDet.Location = New System.Drawing.Point(216, 142)
        Me.chkActivoDet.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkActivoDet.MenuManager = Me.RibbonControl
        Me.chkActivoDet.Name = "chkActivoDet"
        Me.chkActivoDet.Properties.Caption = ""
        Me.chkActivoDet.Size = New System.Drawing.Size(41, 24)
        Me.chkActivoDet.TabIndex = 10
        '
        'txtUbicacionDestino
        '
        Me.txtUbicacionDestino.Location = New System.Drawing.Point(185, 30)
        Me.txtUbicacionDestino.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtUbicacionDestino.MenuManager = Me.RibbonControl
        Me.txtUbicacionDestino.Name = "txtUbicacionDestino"
        Me.txtUbicacionDestino.Properties.Mask.EditMask = "n0"
        Me.txtUbicacionDestino.Properties.ReadOnly = True
        Me.txtUbicacionDestino.Size = New System.Drawing.Size(190, 22)
        Me.txtUbicacionDestino.TabIndex = 2
        '
        'lnkUbicacionDestino
        '
        Me.lnkUbicacionDestino.AutoSize = True
        Me.lnkUbicacionDestino.Location = New System.Drawing.Point(14, 33)
        Me.lnkUbicacionDestino.Name = "lnkUbicacionDestino"
        Me.lnkUbicacionDestino.Size = New System.Drawing.Size(77, 16)
        Me.lnkUbicacionDestino.TabIndex = 0
        Me.lnkUbicacionDestino.TabStop = True
        Me.lnkUbicacionDestino.Text = "Ubic Destino"
        '
        'chkRealizadoDet
        '
        Me.chkRealizadoDet.Enabled = False
        Me.chkRealizadoDet.Location = New System.Drawing.Point(101, 140)
        Me.chkRealizadoDet.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkRealizadoDet.MenuManager = Me.RibbonControl
        Me.chkRealizadoDet.Name = "chkRealizadoDet"
        Me.chkRealizadoDet.Properties.Caption = ""
        Me.chkRealizadoDet.Properties.ReadOnly = True
        Me.chkRealizadoDet.Size = New System.Drawing.Size(41, 24)
        Me.chkRealizadoDet.TabIndex = 8
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.txtNoDocumento)
        Me.GroupControl1.Controls.Add(Label28)
        Me.GroupControl1.Controls.Add(Me.dtpFechaFin)
        Me.GroupControl1.Controls.Add(Me.dtpFechaInicio)
        Me.GroupControl1.Controls.Add(Me.cmbPropietarioBodega)
        Me.GroupControl1.Controls.Add(Me.cmbBodega)
        Me.GroupControl1.Controls.Add(Me.groupPrioridad)
        Me.GroupControl1.Controls.Add(Me.txtNombreMotivoUbicacion)
        Me.GroupControl1.Controls.Add(Me.lnkMotivoUbicacion)
        Me.GroupControl1.Controls.Add(Me.txtIdMotivoUbicacion)
        Me.GroupControl1.Controls.Add(Me.lblEstado)
        Me.GroupControl1.Controls.Add(Label25)
        Me.GroupControl1.Controls.Add(Me.lbl)
        Me.GroupControl1.Controls.Add(Label14)
        Me.GroupControl1.Controls.Add(Me.chkUbicacionConHh)
        Me.GroupControl1.Controls.Add(Label15)
        Me.GroupControl1.Controls.Add(Me.chkOperadorPorlinea)
        Me.GroupControl1.Controls.Add(Label6)
        Me.GroupControl1.Controls.Add(ActivoLabel)
        Me.GroupControl1.Controls.Add(Me.chkActivo)
        Me.GroupControl1.Controls.Add(Label5)
        Me.GroupControl1.Controls.Add(Me.txtObservacion)
        Me.GroupControl1.Controls.Add(Label4)
        Me.GroupControl1.Controls.Add(Me.dtpHoraFin)
        Me.GroupControl1.Controls.Add(Label3)
        Me.GroupControl1.Controls.Add(Me.dtpHoraInicio)
        Me.GroupControl1.Controls.Add(Label1)
        Me.GroupControl1.Controls.Add(CodigoLabel)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1274, 682)
        Me.GroupControl1.TabIndex = 0
        '
        'txtNoDocumento
        '
        Me.txtNoDocumento.AcceptsReturn = True
        Me.txtNoDocumento.BackColor = System.Drawing.Color.White
        Me.txtNoDocumento.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNoDocumento.Location = New System.Drawing.Point(163, 250)
        Me.txtNoDocumento.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.txtNoDocumento.Name = "txtNoDocumento"
        Me.txtNoDocumento.Size = New System.Drawing.Size(323, 23)
        Me.txtNoDocumento.TabIndex = 31
        '
        'dtpFechaFin
        '
        Me.dtpFechaFin.EditValue = New Date(2017, 11, 20, 9, 8, 48, 729)
        Me.dtpFechaFin.Location = New System.Drawing.Point(162, 220)
        Me.dtpFechaFin.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtpFechaFin.MenuManager = Me.RibbonControl
        Me.dtpFechaFin.Name = "dtpFechaFin"
        Me.dtpFechaFin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpFechaFin.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpFechaFin.Size = New System.Drawing.Size(104, 22)
        Me.dtpFechaFin.TabIndex = 29
        '
        'dtpFechaInicio
        '
        Me.dtpFechaInicio.EditValue = New Date(2017, 11, 20, 9, 8, 7, 372)
        Me.dtpFechaInicio.Location = New System.Drawing.Point(162, 190)
        Me.dtpFechaInicio.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtpFechaInicio.MenuManager = Me.RibbonControl
        Me.dtpFechaInicio.Name = "dtpFechaInicio"
        Me.dtpFechaInicio.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpFechaInicio.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpFechaInicio.Size = New System.Drawing.Size(104, 22)
        Me.dtpFechaInicio.TabIndex = 28
        '
        'cmbPropietarioBodega
        '
        Me.cmbPropietarioBodega.Location = New System.Drawing.Point(163, 117)
        Me.cmbPropietarioBodega.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbPropietarioBodega.MenuManager = Me.RibbonControl
        Me.cmbPropietarioBodega.Name = "cmbPropietarioBodega"
        Me.cmbPropietarioBodega.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPropietarioBodega.Properties.NullText = ""
        Me.cmbPropietarioBodega.Size = New System.Drawing.Size(323, 22)
        Me.cmbPropietarioBodega.TabIndex = 27
        '
        'cmbBodega
        '
        Me.cmbBodega.Location = New System.Drawing.Point(163, 85)
        Me.cmbBodega.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbBodega.MenuManager = Me.RibbonControl
        Me.cmbBodega.Name = "cmbBodega"
        Me.cmbBodega.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodega.Properties.NullText = ""
        Me.cmbBodega.Size = New System.Drawing.Size(323, 22)
        Me.cmbBodega.TabIndex = 26
        '
        'groupPrioridad
        '
        Me.groupPrioridad.Controls.Add(Me.rbAlto)
        Me.groupPrioridad.Controls.Add(Me.rbBajo)
        Me.groupPrioridad.Controls.Add(Me.rbMedio)
        Me.groupPrioridad.Controls.Add(Me.pbAlto)
        Me.groupPrioridad.Controls.Add(Me.pbMedio)
        Me.groupPrioridad.Controls.Add(Me.pbBajo)
        Me.groupPrioridad.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.groupPrioridad.Location = New System.Drawing.Point(163, 359)
        Me.groupPrioridad.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.groupPrioridad.Name = "groupPrioridad"
        Me.groupPrioridad.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.groupPrioridad.Size = New System.Drawing.Size(323, 130)
        Me.groupPrioridad.TabIndex = 25
        Me.groupPrioridad.TabStop = False
        Me.groupPrioridad.Text = "Prioridad"
        '
        'rbAlto
        '
        Me.rbAlto.AutoSize = True
        Me.rbAlto.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbAlto.Location = New System.Drawing.Point(40, 28)
        Me.rbAlto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.rbAlto.Name = "rbAlto"
        Me.rbAlto.Size = New System.Drawing.Size(53, 21)
        Me.rbAlto.TabIndex = 0
        Me.rbAlto.TabStop = True
        Me.rbAlto.Text = "Alta"
        Me.rbAlto.UseVisualStyleBackColor = True
        '
        'rbBajo
        '
        Me.rbBajo.AutoSize = True
        Me.rbBajo.Checked = True
        Me.rbBajo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbBajo.Location = New System.Drawing.Point(40, 98)
        Me.rbBajo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.rbBajo.Name = "rbBajo"
        Me.rbBajo.Size = New System.Drawing.Size(57, 21)
        Me.rbBajo.TabIndex = 2
        Me.rbBajo.TabStop = True
        Me.rbBajo.Text = "Baja"
        Me.rbBajo.UseVisualStyleBackColor = True
        '
        'rbMedio
        '
        Me.rbMedio.AutoSize = True
        Me.rbMedio.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbMedio.Location = New System.Drawing.Point(40, 64)
        Me.rbMedio.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.rbMedio.Name = "rbMedio"
        Me.rbMedio.Size = New System.Drawing.Size(67, 21)
        Me.rbMedio.TabIndex = 1
        Me.rbMedio.TabStop = True
        Me.rbMedio.Text = "Media"
        Me.rbMedio.UseVisualStyleBackColor = True
        '
        'pbAlto
        '
        Me.pbAlto.Image = Global.TOMWMS.My.Resources.Resources.red_ball
        Me.pbAlto.Location = New System.Drawing.Point(13, 30)
        Me.pbAlto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.pbAlto.Name = "pbAlto"
        Me.pbAlto.Size = New System.Drawing.Size(19, 20)
        Me.pbAlto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbAlto.TabIndex = 91
        Me.pbAlto.TabStop = False
        '
        'pbMedio
        '
        Me.pbMedio.Image = Global.TOMWMS.My.Resources.Resources.yellow_ball
        Me.pbMedio.Location = New System.Drawing.Point(13, 65)
        Me.pbMedio.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.pbMedio.Name = "pbMedio"
        Me.pbMedio.Size = New System.Drawing.Size(19, 20)
        Me.pbMedio.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbMedio.TabIndex = 93
        Me.pbMedio.TabStop = False
        '
        'pbBajo
        '
        Me.pbBajo.Image = Global.TOMWMS.My.Resources.Resources.green_ball
        Me.pbBajo.Location = New System.Drawing.Point(13, 100)
        Me.pbBajo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.pbBajo.Name = "pbBajo"
        Me.pbBajo.Size = New System.Drawing.Size(19, 20)
        Me.pbBajo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbBajo.TabIndex = 95
        Me.pbBajo.TabStop = False
        '
        'txtNombreMotivoUbicacion
        '
        Me.txtNombreMotivoUbicacion.Location = New System.Drawing.Point(267, 155)
        Me.txtNombreMotivoUbicacion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNombreMotivoUbicacion.MenuManager = Me.RibbonControl
        Me.txtNombreMotivoUbicacion.Name = "txtNombreMotivoUbicacion"
        Me.txtNombreMotivoUbicacion.Properties.ReadOnly = True
        Me.txtNombreMotivoUbicacion.Size = New System.Drawing.Size(219, 22)
        Me.txtNombreMotivoUbicacion.TabIndex = 10
        '
        'lnkMotivoUbicacion
        '
        Me.lnkMotivoUbicacion.AutoSize = True
        Me.lnkMotivoUbicacion.Location = New System.Drawing.Point(15, 159)
        Me.lnkMotivoUbicacion.Name = "lnkMotivoUbicacion"
        Me.lnkMotivoUbicacion.Size = New System.Drawing.Size(102, 16)
        Me.lnkMotivoUbicacion.TabIndex = 8
        Me.lnkMotivoUbicacion.TabStop = True
        Me.lnkMotivoUbicacion.Text = "Motivo Ubicación"
        '
        'txtIdMotivoUbicacion
        '
        Me.txtIdMotivoUbicacion.Location = New System.Drawing.Point(163, 155)
        Me.txtIdMotivoUbicacion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtIdMotivoUbicacion.MenuManager = Me.RibbonControl
        Me.txtIdMotivoUbicacion.Name = "txtIdMotivoUbicacion"
        Me.txtIdMotivoUbicacion.Properties.Mask.EditMask = "n0"
        Me.txtIdMotivoUbicacion.Properties.ReadOnly = True
        Me.txtIdMotivoUbicacion.Size = New System.Drawing.Size(104, 22)
        Me.txtIdMotivoUbicacion.TabIndex = 9
        '
        'lblEstado
        '
        Me.lblEstado.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar
        Me.lblEstado.AutoSize = True
        Me.lblEstado.Location = New System.Drawing.Point(163, 58)
        Me.lblEstado.Name = "lblEstado"
        Me.lblEstado.Size = New System.Drawing.Size(45, 16)
        Me.lblEstado.TabIndex = 2
        Me.lblEstado.Text = "Estado"
        '
        'lbl
        '
        Me.lbl.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar
        Me.lbl.AutoSize = True
        Me.lbl.Location = New System.Drawing.Point(163, 32)
        Me.lbl.Name = "lbl"
        Me.lbl.Size = New System.Drawing.Size(63, 16)
        Me.lbl.TabIndex = 1
        Me.lbl.Text = "00000001"
        '
        'chkUbicacionConHh
        '
        Me.chkUbicacionConHh.EditValue = True
        Me.chkUbicacionConHh.Location = New System.Drawing.Point(132, 455)
        Me.chkUbicacionConHh.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkUbicacionConHh.MenuManager = Me.RibbonControl
        Me.chkUbicacionConHh.Name = "chkUbicacionConHh"
        Me.chkUbicacionConHh.Properties.Caption = ""
        Me.chkUbicacionConHh.Size = New System.Drawing.Size(20, 24)
        Me.chkUbicacionConHh.TabIndex = 24
        '
        'chkOperadorPorlinea
        '
        Me.chkOperadorPorlinea.EditValue = True
        Me.chkOperadorPorlinea.Location = New System.Drawing.Point(132, 421)
        Me.chkOperadorPorlinea.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkOperadorPorlinea.MenuManager = Me.RibbonControl
        Me.chkOperadorPorlinea.Name = "chkOperadorPorlinea"
        Me.chkOperadorPorlinea.Properties.Caption = ""
        Me.chkOperadorPorlinea.Size = New System.Drawing.Size(19, 24)
        Me.chkOperadorPorlinea.TabIndex = 21
        '
        'chkActivo
        '
        Me.chkActivo.EditValue = True
        Me.chkActivo.Location = New System.Drawing.Point(132, 387)
        Me.chkActivo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkActivo.MenuManager = Me.RibbonControl
        Me.chkActivo.Name = "chkActivo"
        Me.chkActivo.Properties.Caption = ""
        Me.chkActivo.Size = New System.Drawing.Size(20, 24)
        Me.chkActivo.TabIndex = 20
        '
        'txtObservacion
        '
        Me.txtObservacion.Location = New System.Drawing.Point(163, 280)
        Me.txtObservacion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtObservacion.Multiline = True
        Me.txtObservacion.Name = "txtObservacion"
        Me.txtObservacion.Size = New System.Drawing.Size(324, 70)
        Me.txtObservacion.TabIndex = 18
        '
        'dtpHoraFin
        '
        Me.dtpHoraFin.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtpHoraFin.Location = New System.Drawing.Point(267, 220)
        Me.dtpHoraFin.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtpHoraFin.Name = "dtpHoraFin"
        Me.dtpHoraFin.Size = New System.Drawing.Size(219, 23)
        Me.dtpHoraFin.TabIndex = 16
        '
        'dtpHoraInicio
        '
        Me.dtpHoraInicio.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtpHoraInicio.Location = New System.Drawing.Point(267, 188)
        Me.dtpHoraInicio.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtpHoraInicio.Name = "dtpHoraInicio"
        Me.dtpHoraInicio.Size = New System.Drawing.Size(219, 23)
        Me.dtpHoraInicio.TabIndex = 13
        '
        'tabDatos
        '
        Me.tabDatos.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.tabDatos.Appearance.Options.UseBackColor = True
        Me.tabDatos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabDatos.Location = New System.Drawing.Point(0, 193)
        Me.tabDatos.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.tabDatos.Name = "tabDatos"
        Me.tabDatos.SelectedTabPage = Me.xtabGeneral
        Me.tabDatos.Size = New System.Drawing.Size(1276, 712)
        Me.tabDatos.TabIndex = 3
        Me.tabDatos.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtabGeneral, Me.xTabDetalle, Me.xtabOperador, Me.xtabTarima, Me.XtraTabPage1})
        '
        'xtabGeneral
        '
        Me.xtabGeneral.Controls.Add(Me.GroupControl1)
        Me.xtabGeneral.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.xtabGeneral.Name = "xtabGeneral"
        Me.xtabGeneral.Size = New System.Drawing.Size(1274, 682)
        Me.xtabGeneral.Text = "General"
        '
        'xTabDetalle
        '
        Me.xTabDetalle.Appearance.PageClient.BackColor = System.Drawing.Color.Black
        Me.xTabDetalle.Appearance.PageClient.Options.UseBackColor = True
        Me.xTabDetalle.Controls.Add(Me.GroupControl5)
        Me.xTabDetalle.Controls.Add(Me.ToolStripP)
        Me.xTabDetalle.Controls.Add(Me.groupCambioDeEstado)
        Me.xTabDetalle.Controls.Add(Me.GroupControl4)
        Me.xTabDetalle.Controls.Add(Me.GroupControl3)
        Me.xTabDetalle.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.xTabDetalle.Name = "xTabDetalle"
        Me.xTabDetalle.Size = New System.Drawing.Size(1274, 682)
        Me.xTabDetalle.Text = "Detalle"
        '
        'GroupControl3
        '
        Me.GroupControl3.Controls.Add(Me.grdDetalle)
        Me.GroupControl3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupControl3.Location = New System.Drawing.Point(0, 401)
        Me.GroupControl3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(1274, 281)
        Me.GroupControl3.TabIndex = 5
        Me.GroupControl3.Text = "Detalle "
        '
        'grdDetalle
        '
        Me.grdDetalle.Cursor = System.Windows.Forms.Cursors.Default
        Me.grdDetalle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdDetalle.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        GridLevelNode1.RelationName = "Level1"
        Me.grdDetalle.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.grdDetalle.Location = New System.Drawing.Point(2, 28)
        Me.grdDetalle.MainView = Me.GridViewDet
        Me.grdDetalle.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdDetalle.MenuManager = Me.RibbonControl
        Me.grdDetalle.Name = "grdDetalle"
        Me.grdDetalle.Size = New System.Drawing.Size(1270, 251)
        Me.grdDetalle.TabIndex = 0
        Me.grdDetalle.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewDet, Me.GridView4, Me.GridView1})
        '
        'GridViewDet
        '
        Me.GridViewDet.GridControl = Me.grdDetalle
        Me.GridViewDet.Name = "GridViewDet"
        Me.GridViewDet.OptionsBehavior.Editable = False
        Me.GridViewDet.OptionsView.ColumnAutoWidth = False
        Me.GridViewDet.OptionsView.ShowFooter = True
        Me.GridViewDet.OptionsView.ShowGroupPanel = False
        '
        'GridView4
        '
        Me.GridView4.GridControl = Me.grdDetalle
        Me.GridView4.Name = "GridView4"
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.grdDetalle
        Me.GridView1.Name = "GridView1"
        '
        'xtabOperador
        '
        Me.xtabOperador.Controls.Add(Me.GroupControl6)
        Me.xtabOperador.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.xtabOperador.Name = "xtabOperador"
        Me.xtabOperador.Size = New System.Drawing.Size(1274, 682)
        Me.xtabOperador.Text = "Operador"
        '
        'xtabTarima
        '
        Me.xtabTarima.Controls.Add(Me.GroupControl7)
        Me.xtabTarima.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.xtabTarima.Name = "xtabTarima"
        Me.xtabTarima.Size = New System.Drawing.Size(1274, 682)
        Me.xtabTarima.Text = "Tarima"
        '
        'XtraTabPage1
        '
        Me.XtraTabPage1.Controls.Add(Me.grdStockReservado)
        Me.XtraTabPage1.Name = "XtraTabPage1"
        Me.XtraTabPage1.Size = New System.Drawing.Size(1274, 682)
        Me.XtraTabPage1.Text = "Stock Reservado"
        '
        'grdStockReservado
        '
        Me.grdStockReservado.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdStockReservado.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdStockReservado.Location = New System.Drawing.Point(0, 0)
        Me.grdStockReservado.MainView = Me.GridView6
        Me.grdStockReservado.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdStockReservado.MenuManager = Me.RibbonControl
        Me.grdStockReservado.Name = "grdStockReservado"
        Me.grdStockReservado.Size = New System.Drawing.Size(1274, 682)
        Me.grdStockReservado.TabIndex = 2
        Me.grdStockReservado.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView6})
        '
        'GridView6
        '
        Me.GridView6.GridControl = Me.grdStockReservado
        Me.GridView6.Name = "GridView6"
        Me.GridView6.OptionsBehavior.Editable = False
        Me.GridView6.OptionsBehavior.ReadOnly = True
        Me.GridView6.OptionsFind.AlwaysVisible = True
        Me.GridView6.OptionsView.ColumnAutoWidth = False
        Me.GridView6.OptionsView.ShowAutoFilterRow = True
        Me.GridView6.OptionsView.ShowFooter = True
        '
        'DockManager1
        '
        Me.DockManager1.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerBottom})
        Me.DockManager1.Form = Me
        Me.DockManager1.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane", "DevExpress.XtraBars.TabFormControl", "DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl", "DevExpress.XtraBars.ToolbarForm.ToolbarFormControl"})
        '
        'hideContainerBottom
        '
        Me.hideContainerBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.hideContainerBottom.Controls.Add(Me.DockPanel1)
        Me.hideContainerBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 905)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(1276, 26)
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.ID = New System.Guid("4b1f6942-2d8d-4c3b-a1a9-4bdb69a541f1")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 0)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(200, 133)
        Me.DockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.SavedIndex = 0
        Me.DockPanel1.Size = New System.Drawing.Size(1276, 133)
        Me.DockPanel1.Text = "Bitácora"
        Me.DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.Fec_agrDateEdit)
        Me.DockPanel1_Container.Controls.Add(Me.User_agrTextEdit)
        Me.DockPanel1_Container.Controls.Add(Me.Fec_modDateEdit)
        Me.DockPanel1_Container.Controls.Add(Fec_agrLabel)
        Me.DockPanel1_Container.Controls.Add(Fec_modLabel)
        Me.DockPanel1_Container.Controls.Add(User_modLabel)
        Me.DockPanel1_Container.Controls.Add(User_agrLabel)
        Me.DockPanel1_Container.Controls.Add(Me.User_modTextEdit)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(4, 34)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1268, 95)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'Fec_agrDateEdit
        '
        Me.Fec_agrDateEdit.EditValue = Nothing
        Me.Fec_agrDateEdit.Enabled = False
        Me.Fec_agrDateEdit.Location = New System.Drawing.Point(289, 53)
        Me.Fec_agrDateEdit.Margin = New System.Windows.Forms.Padding(4)
        Me.Fec_agrDateEdit.MenuManager = Me.RibbonControl
        Me.Fec_agrDateEdit.Name = "Fec_agrDateEdit"
        Me.Fec_agrDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit.Size = New System.Drawing.Size(261, 22)
        Me.Fec_agrDateEdit.TabIndex = 13
        '
        'User_agrTextEdit
        '
        Me.User_agrTextEdit.Enabled = False
        Me.User_agrTextEdit.Location = New System.Drawing.Point(289, 21)
        Me.User_agrTextEdit.Margin = New System.Windows.Forms.Padding(4)
        Me.User_agrTextEdit.MenuManager = Me.RibbonControl
        Me.User_agrTextEdit.Name = "User_agrTextEdit"
        Me.User_agrTextEdit.Size = New System.Drawing.Size(261, 22)
        Me.User_agrTextEdit.TabIndex = 9
        '
        'Fec_modDateEdit
        '
        Me.Fec_modDateEdit.EditValue = Nothing
        Me.Fec_modDateEdit.Enabled = False
        Me.Fec_modDateEdit.Location = New System.Drawing.Point(774, 53)
        Me.Fec_modDateEdit.Margin = New System.Windows.Forms.Padding(4)
        Me.Fec_modDateEdit.MenuManager = Me.RibbonControl
        Me.Fec_modDateEdit.Name = "Fec_modDateEdit"
        Me.Fec_modDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit.Size = New System.Drawing.Size(261, 22)
        Me.Fec_modDateEdit.TabIndex = 15
        '
        'User_modTextEdit
        '
        Me.User_modTextEdit.Enabled = False
        Me.User_modTextEdit.Location = New System.Drawing.Point(774, 21)
        Me.User_modTextEdit.Margin = New System.Windows.Forms.Padding(4)
        Me.User_modTextEdit.MenuManager = Me.RibbonControl
        Me.User_modTextEdit.Name = "User_modTextEdit"
        Me.User_modTextEdit.Size = New System.Drawing.Size(261, 22)
        Me.User_modTextEdit.TabIndex = 10
        '
        'lblTalla
        '
        Me.lblTalla.Location = New System.Drawing.Point(12, 283)
        Me.lblTalla.Name = "lblTalla"
        Me.lblTalla.Size = New System.Drawing.Size(28, 16)
        Me.lblTalla.TabIndex = 35
        Me.lblTalla.Text = "Talla"
        '
        'lblColor
        '
        Me.lblColor.Location = New System.Drawing.Point(12, 321)
        Me.lblColor.Name = "lblColor"
        Me.lblColor.Size = New System.Drawing.Size(30, 16)
        Me.lblColor.TabIndex = 36
        Me.lblColor.Text = "Color"
        '
        'frmCambioUbicacion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1276, 961)
        Me.Controls.Add(Me.tabDatos)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Controls.Add(Me.RibbonStatusBar1)
        Me.Controls.Add(Me.RibbonControl)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmCambioUbicacion"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar1
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsOperadorUbicacion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TarimasDisponiblesBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl7.ResumeLayout(False)
        Me.GroupControl7.PerformLayout()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl6.ResumeLayout(False)
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrdOperadorBobega, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStripP.ResumeLayout(False)
        Me.ToolStripP.PerformLayout()
        CType(Me.groupCambioDeEstado, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupCambioDeEstado.ResumeLayout(False)
        Me.groupCambioDeEstado.PerformLayout()
        CType(Me.txtIdEstado.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreEstado.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl4.ResumeLayout(False)
        Me.GroupControl4.PerformLayout()
        CType(Me.txtColor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTalla.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLicPlate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbOperadores.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIngreso.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPresentacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAñada.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUnidadMedida.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEstado.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVence.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtProducto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdStock.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdOrigen.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl5.ResumeLayout(False)
        Me.GroupControl5.PerformLayout()
        CType(Me.txtCantidad, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdUbicacionDestino.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivoDet.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUbicacionDestino.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRealizadoDet.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.dtpFechaFin.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFechaFin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFechaInicio.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFechaInicio.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPropietarioBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupPrioridad.ResumeLayout(False)
        Me.groupPrioridad.PerformLayout()
        CType(Me.pbAlto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbMedio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbBajo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreMotivoUbicacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdMotivoUbicacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkUbicacionConHh.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkOperadorPorlinea.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tabDatos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabDatos.ResumeLayout(False)
        Me.xtabGeneral.ResumeLayout(False)
        Me.xTabDetalle.ResumeLayout(False)
        Me.xTabDetalle.PerformLayout()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        CType(Me.grdDetalle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewDet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtabOperador.ResumeLayout(False)
        Me.xtabTarima.ResumeLayout(False)
        Me.XtraTabPage1.ResumeLayout(False)
        CType(Me.grdStockReservado, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerBottom.ResumeLayout(False)
        Me.DockPanel1.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        Me.DockPanel1_Container.PerformLayout()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents mnuGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEliminar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuAsignacion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents DataBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DsOperadorUbicacion As TOMWMS.DsOperadorUbicacion
    Friend WithEvents TarimasDisponiblesBindingSource As BindingSource
    Private WithEvents GroupControl7 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents prg As ProgressBar
    Friend WithEvents cmdQuitar As Button
    Friend WithEvents cmdAgregar As Button
    Friend WithEvents CheckBox2 As CheckBox
    Friend WithEvents lvTarimasDisponibles As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader5 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents lvTarimasUsadas As ListView
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GroupControl6 As DevExpress.XtraEditors.GroupControl
    Private WithEvents Grid As DevExpress.XtraGrid.GridControl
    Private WithEvents GrdOperadorBobega As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents Selección As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents IdTransUbicHhOp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Operador As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IdOperadorBodega As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lblVolumenProducto As Label
    Friend WithEvents groupCambioDeEstado As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtIdEstado As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lnkCambioDeEstado As LinkLabel
    Friend WithEvents txtNombreEstado As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GroupControl4 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblCantRef As Label
    Friend WithEvents txtLote As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtIngreso As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LinkLabel2 As LinkLabel
    Friend WithEvents txtPresentacion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtAñada As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtUnidadMedida As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtEstado As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtVence As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtSerie As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtProducto As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtIdStock As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtIdOrigen As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GroupControl5 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtIdUbicacionDestino As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkActivoDet As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtUbicacionDestino As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lnkUbicacionDestino As LinkLabel
    Friend WithEvents chkRealizadoDet As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents groupPrioridad As GroupBox
    Friend WithEvents rbAlto As RadioButton
    Friend WithEvents rbBajo As RadioButton
    Friend WithEvents rbMedio As RadioButton
    Friend WithEvents pbAlto As PictureBox
    Friend WithEvents pbMedio As PictureBox
    Friend WithEvents pbBajo As PictureBox
    Friend WithEvents txtNombreMotivoUbicacion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lnkMotivoUbicacion As LinkLabel
    Friend WithEvents txtIdMotivoUbicacion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblEstado As Label
    Friend WithEvents lbl As Label
    Friend WithEvents chkUbicacionConHh As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkOperadorPorlinea As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkActivo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtObservacion As TextBox
    Friend WithEvents dtpHoraFin As DateTimePicker
    Friend WithEvents dtpHoraInicio As DateTimePicker
    Friend WithEvents ColumnHeader6 As ColumnHeader
    Friend WithEvents lblFactor As Label
    Friend WithEvents ToolStripP As ToolStrip
    Friend WithEvents cmdNewP As ToolStripButton
    Friend WithEvents cmdGuardar As ToolStripButton
    Friend WithEvents mnuEliminarDet As ToolStripButton
    Friend WithEvents lblVolumenUbicacion As Label
    Friend WithEvents txtCantidad As NumericUpDown
    Friend WithEvents tabDatos As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtabGeneral As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents xTabDetalle As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grdDetalle As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewDet As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView4 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents xtabOperador As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents xtabTarima As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents mnuImprimir1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lblItemBandera As Label
    Friend WithEvents cmbBodega As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbPropietarioBodega As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbOperadores As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents dtpFechaFin As DevExpress.XtraEditors.DateEdit
    Friend WithEvents dtpFechaInicio As DevExpress.XtraEditors.DateEdit
    Friend WithEvents txtLicPlate As DevExpress.XtraEditors.TextEdit
    Friend WithEvents mnuLiberarStockNoProcesado As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents XtraTabPage1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents grdStockReservado As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView6 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RibbonStatusBar1 As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents lblRegs As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuImportarListaCambioUbic As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuPendiente As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdDescargarPlantilla As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdEliminarDocumento As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents txtNoDocumento As TextBox
    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents Fec_agrDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_agrTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_modDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_modTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents txtColor As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtTalla As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblColor As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblTalla As DevExpress.XtraEditors.LabelControl
End Class
