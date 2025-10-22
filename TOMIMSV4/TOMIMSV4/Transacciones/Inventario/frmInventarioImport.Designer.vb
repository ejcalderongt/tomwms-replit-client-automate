<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInventarioImport
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInventarioImport))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuAplicar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuValidar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdPegar = New DevExpress.XtraBars.BarButtonItem()
        Me.BarStaticItem1 = New DevExpress.XtraBars.BarStaticItem()
        Me.cmdAdd = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdDel = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuImportarExcel = New DevExpress.XtraBars.BarButtonItem()
        Me.lblRegs = New DevExpress.XtraBars.BarStaticItem()
        Me.ckInsertaStock = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.BarStaticItem2 = New DevExpress.XtraBars.BarStaticItem()
        Me.btgInsInv = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.lblTipoImportacion = New DevExpress.XtraBars.BarStaticItem()
        Me.RibbonPageCategory1 = New DevExpress.XtraBars.Ribbon.RibbonPageCategory()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup3 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup4 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup5 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.grpVerificaInv = New DevExpress.XtraEditors.GroupControl()
        Me.lblPrg = New System.Windows.Forms.Label()
        Me.prg = New System.Windows.Forms.ProgressBar()
        Me.cmbPropietario = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblPropietario = New System.Windows.Forms.Label()
        Me.grdData = New System.Windows.Forms.DataGridView()
        Me.DsExcel = New TOMWMS.DsExcel()
        Me.colEstado = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCodigo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPresentacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCantidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPeso = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colUnidadMedida = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colIdProducto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colIdPresentacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColIdUnidadMedida = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColLote = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFechaVence = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColUbicacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colIdPropietarioBodega = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colnombre_propietario = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColLp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColCodVariante = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCosto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPrecio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colParametro_a = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colParametro_b = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColCodigo_Area = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColColor = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColTalla = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colError = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpVerificaInv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpVerificaInv.SuspendLayout()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.EmptyAreaImageOptions.ImagePadding = New System.Windows.Forms.Padding(49, 46, 49, 46)
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuAplicar, Me.mnuValidar, Me.cmdPegar, Me.BarStaticItem1, Me.cmdAdd, Me.cmdDel, Me.BarButtonItem1, Me.mnuImportarExcel, Me.lblRegs, Me.ckInsertaStock, Me.BarStaticItem2, Me.btgInsInv, Me.lblTipoImportacion})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.RibbonControl.MaxItemId = 18
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.OptionsMenuMinWidth = 552
        Me.RibbonControl.PageCategories.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageCategory() {Me.RibbonPageCategory1})
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.RepositoryItemCheckEdit2})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1335, 158)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'mnuAplicar
        '
        Me.mnuAplicar.Caption = "Aplicar      "
        Me.mnuAplicar.Id = 1
        Me.mnuAplicar.ImageOptions.SvgImage = CType(resources.GetObject("mnuAplicar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuAplicar.Name = "mnuAplicar"
        '
        'mnuValidar
        '
        Me.mnuValidar.Caption = "  Validar  "
        Me.mnuValidar.Id = 2
        Me.mnuValidar.ImageOptions.SvgImage = CType(resources.GetObject("mnuValidar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuValidar.Name = "mnuValidar"
        '
        'cmdPegar
        '
        Me.cmdPegar.Caption = "   Pegar   "
        Me.cmdPegar.Id = 3
        Me.cmdPegar.ImageOptions.SvgImage = CType(resources.GetObject("cmdPegar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdPegar.Name = "cmdPegar"
        '
        'BarStaticItem1
        '
        Me.BarStaticItem1.Caption = "BarStaticItem1"
        Me.BarStaticItem1.Id = 4
        Me.BarStaticItem1.Name = "BarStaticItem1"
        '
        'cmdAdd
        '
        Me.cmdAdd.Caption = "  Agegar   Linea"
        Me.cmdAdd.Id = 5
        Me.cmdAdd.ImageOptions.SvgImage = CType(resources.GetObject("cmdAdd.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdAdd.Name = "cmdAdd"
        '
        'cmdDel
        '
        Me.cmdDel.Caption = "  Eliminar   Linea"
        Me.cmdDel.Id = 6
        Me.cmdDel.ImageOptions.SvgImage = CType(resources.GetObject("cmdDel.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdDel.Name = "cmdDel"
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "  Eliminar   Todo"
        Me.BarButtonItem1.Id = 9
        Me.BarButtonItem1.ImageOptions.SvgImage = CType(resources.GetObject("BarButtonItem1.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'mnuImportarExcel
        '
        Me.mnuImportarExcel.Caption = "Importar Excel"
        Me.mnuImportarExcel.Id = 10
        Me.mnuImportarExcel.ImageOptions.SvgImage = CType(resources.GetObject("mnuImportarExcel.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuImportarExcel.Name = "mnuImportarExcel"
        '
        'lblRegs
        '
        Me.lblRegs.Caption = "Registros: 0"
        Me.lblRegs.Id = 11
        Me.lblRegs.Name = "lblRegs"
        '
        'ckInsertaStock
        '
        Me.ckInsertaStock.AutoHideEdit = False
        Me.ckInsertaStock.Caption = "Insertar Inv. Ini"
        Me.ckInsertaStock.Edit = Me.RepositoryItemCheckEdit1
        Me.ckInsertaStock.EditWidth = 1
        Me.ckInsertaStock.Id = 13
        Me.ckInsertaStock.Name = "ckInsertaStock"
        Me.ckInsertaStock.RibbonStyle = CType(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large Or DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) _
            Or DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText), DevExpress.XtraBars.Ribbon.RibbonItemStyles)
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.Caption = ""
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        '
        'BarStaticItem2
        '
        Me.BarStaticItem2.Caption = "Insertar como inventario inicial"
        Me.BarStaticItem2.Id = 14
        Me.BarStaticItem2.Name = "BarStaticItem2"
        '
        'btgInsInv
        '
        Me.btgInsInv.Caption = "Insertar como inventario inicial"
        Me.btgInsInv.Id = 16
        Me.btgInsInv.Name = "btgInsInv"
        '
        'lblTipoImportacion
        '
        Me.lblTipoImportacion.Caption = "Importación de teórico WMS"
        Me.lblTipoImportacion.Id = 17
        Me.lblTipoImportacion.ImageOptions.SvgImage = CType(resources.GetObject("lblTipoImportacion.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.lblTipoImportacion.Name = "lblTipoImportacion"
        Me.lblTipoImportacion.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'RibbonPageCategory1
        '
        Me.RibbonPageCategory1.Name = "RibbonPageCategory1"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1, Me.RibbonPageGroup2, Me.RibbonPageGroup3, Me.RibbonPageGroup4, Me.RibbonPageGroup5})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Opciones de Lista"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuAplicar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonPageGroup2
        '
        Me.RibbonPageGroup2.ItemLinks.Add(Me.mnuValidar)
        Me.RibbonPageGroup2.Name = "RibbonPageGroup2"
        '
        'RibbonPageGroup3
        '
        Me.RibbonPageGroup3.ItemLinks.Add(Me.cmdPegar)
        Me.RibbonPageGroup3.ItemLinks.Add(Me.mnuImportarExcel)
        Me.RibbonPageGroup3.Name = "RibbonPageGroup3"
        '
        'RibbonPageGroup4
        '
        Me.RibbonPageGroup4.ItemLinks.Add(Me.cmdAdd)
        Me.RibbonPageGroup4.ItemLinks.Add(Me.cmdDel)
        Me.RibbonPageGroup4.ItemLinks.Add(Me.BarButtonItem1)
        Me.RibbonPageGroup4.Name = "RibbonPageGroup4"
        '
        'RibbonPageGroup5
        '
        Me.RibbonPageGroup5.ItemLinks.Add(Me.lblTipoImportacion)
        Me.RibbonPageGroup5.Name = "RibbonPageGroup5"
        '
        'RepositoryItemCheckEdit2
        '
        Me.RepositoryItemCheckEdit2.Caption = "chkInsertaInv"
        Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblRegs)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 568)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1335, 24)
        '
        'grpVerificaInv
        '
        Me.grpVerificaInv.Controls.Add(Me.lblPrg)
        Me.grpVerificaInv.Controls.Add(Me.prg)
        Me.grpVerificaInv.Controls.Add(Me.cmbPropietario)
        Me.grpVerificaInv.Controls.Add(Me.lblPropietario)
        Me.grpVerificaInv.Controls.Add(Me.grdData)
        Me.grpVerificaInv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpVerificaInv.Location = New System.Drawing.Point(0, 158)
        Me.grpVerificaInv.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.grpVerificaInv.Name = "grpVerificaInv"
        Me.grpVerificaInv.Size = New System.Drawing.Size(1335, 410)
        Me.grpVerificaInv.TabIndex = 0
        '
        'lblPrg
        '
        Me.lblPrg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPrg.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblPrg.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrg.Location = New System.Drawing.Point(2, -221)
        Me.lblPrg.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblPrg.Name = "lblPrg"
        Me.lblPrg.Size = New System.Drawing.Size(1331, 51)
        Me.lblPrg.TabIndex = 2
        Me.lblPrg.Text = "..."
        '
        'prg
        '
        Me.prg.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.prg.Location = New System.Drawing.Point(2, -170)
        Me.prg.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.prg.Name = "prg"
        Me.prg.Size = New System.Drawing.Size(1331, 45)
        Me.prg.TabIndex = 3
        Me.prg.Visible = False
        '
        'cmbPropietario
        '
        Me.cmbPropietario.Dock = System.Windows.Forms.DockStyle.Top
        Me.cmbPropietario.Location = New System.Drawing.Point(2, 55)
        Me.cmbPropietario.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.cmbPropietario.MenuManager = Me.RibbonControl
        Me.cmbPropietario.Name = "cmbPropietario"
        Me.cmbPropietario.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPropietario.Properties.NullText = ""
        Me.cmbPropietario.Size = New System.Drawing.Size(1331, 20)
        Me.cmbPropietario.TabIndex = 1
        '
        'lblPropietario
        '
        Me.lblPropietario.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblPropietario.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPropietario.Location = New System.Drawing.Point(2, 23)
        Me.lblPropietario.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblPropietario.Name = "lblPropietario"
        Me.lblPropietario.Size = New System.Drawing.Size(1331, 32)
        Me.lblPropietario.TabIndex = 0
        Me.lblPropietario.Text = "Propietario:"
        '
        'grdData
        '
        Me.grdData.AllowUserToAddRows = False
        Me.grdData.AllowUserToDeleteRows = False
        Me.grdData.AllowUserToResizeRows = False
        Me.grdData.BackgroundColor = System.Drawing.Color.White
        Me.grdData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdData.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colEstado, Me.colId, Me.colCodigo, Me.colPresentacion, Me.colCantidad, Me.colPeso, Me.colUnidadMedida, Me.colIdProducto, Me.colIdPresentacion, Me.ColIdUnidadMedida, Me.ColLote, Me.colFechaVence, Me.ColUbicacion, Me.colIdPropietarioBodega, Me.colnombre_propietario, Me.ColLp, Me.ColCodVariante, Me.colCosto, Me.colPrecio, Me.colParametro_a, Me.colParametro_b, Me.ColCodigo_Area, Me.ColColor, Me.ColTalla, Me.colError})
        Me.grdData.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.grdData.Location = New System.Drawing.Point(2, -125)
        Me.grdData.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.grdData.MultiSelect = False
        Me.grdData.Name = "grdData"
        Me.grdData.RowHeadersVisible = False
        Me.grdData.RowHeadersWidth = 51
        Me.grdData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdData.ShowCellErrors = False
        Me.grdData.ShowCellToolTips = False
        Me.grdData.ShowEditingIcon = False
        Me.grdData.ShowRowErrors = False
        Me.grdData.Size = New System.Drawing.Size(1331, 533)
        Me.grdData.TabIndex = 4
        '
        'DsExcel
        '
        Me.DsExcel.DataSetName = "DsExcel"
        Me.DsExcel.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'colEstado
        '
        Me.colEstado.HeaderText = "Estado"
        Me.colEstado.MinimumWidth = 6
        Me.colEstado.Name = "colEstado"
        Me.colEstado.ReadOnly = True
        Me.colEstado.Width = 60
        '
        'colId
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro
        Me.colId.DefaultCellStyle = DataGridViewCellStyle1
        Me.colId.HeaderText = "Id"
        Me.colId.MinimumWidth = 6
        Me.colId.Name = "colId"
        Me.colId.ReadOnly = True
        Me.colId.Width = 125
        '
        'colCodigo
        '
        Me.colCodigo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.colCodigo.HeaderText = "Codigo"
        Me.colCodigo.MinimumWidth = 6
        Me.colCodigo.Name = "colCodigo"
        Me.colCodigo.Width = 65
        '
        'colPresentacion
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.colPresentacion.DefaultCellStyle = DataGridViewCellStyle2
        Me.colPresentacion.HeaderText = "Presentacion"
        Me.colPresentacion.MinimumWidth = 6
        Me.colPresentacion.Name = "colPresentacion"
        Me.colPresentacion.Width = 125
        '
        'colCantidad
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N6"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.colCantidad.DefaultCellStyle = DataGridViewCellStyle3
        Me.colCantidad.HeaderText = "Cantidad"
        Me.colCantidad.MinimumWidth = 6
        Me.colCantidad.Name = "colCantidad"
        Me.colCantidad.Width = 80
        '
        'colPeso
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colPeso.DefaultCellStyle = DataGridViewCellStyle4
        Me.colPeso.HeaderText = "Peso"
        Me.colPeso.MinimumWidth = 6
        Me.colPeso.Name = "colPeso"
        Me.colPeso.Width = 80
        '
        'colUnidadMedida
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.colUnidadMedida.DefaultCellStyle = DataGridViewCellStyle5
        Me.colUnidadMedida.HeaderText = "UnidadMed"
        Me.colUnidadMedida.MinimumWidth = 6
        Me.colUnidadMedida.Name = "colUnidadMedida"
        Me.colUnidadMedida.Width = 80
        '
        'colIdProducto
        '
        Me.colIdProducto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.WhiteSmoke
        Me.colIdProducto.DefaultCellStyle = DataGridViewCellStyle6
        Me.colIdProducto.HeaderText = "idProd"
        Me.colIdProducto.MinimumWidth = 6
        Me.colIdProducto.Name = "colIdProducto"
        Me.colIdProducto.ReadOnly = True
        Me.colIdProducto.Visible = False
        Me.colIdProducto.Width = 62
        '
        'colIdPresentacion
        '
        Me.colIdPresentacion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.WhiteSmoke
        Me.colIdPresentacion.DefaultCellStyle = DataGridViewCellStyle7
        Me.colIdPresentacion.HeaderText = "idPres"
        Me.colIdPresentacion.MinimumWidth = 6
        Me.colIdPresentacion.Name = "colIdPresentacion"
        Me.colIdPresentacion.ReadOnly = True
        Me.colIdPresentacion.Visible = False
        Me.colIdPresentacion.Width = 61
        '
        'ColIdUnidadMedida
        '
        Me.ColIdUnidadMedida.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ColIdUnidadMedida.DefaultCellStyle = DataGridViewCellStyle8
        Me.ColIdUnidadMedida.HeaderText = "idUM"
        Me.ColIdUnidadMedida.MinimumWidth = 6
        Me.ColIdUnidadMedida.Name = "ColIdUnidadMedida"
        Me.ColIdUnidadMedida.ReadOnly = True
        Me.ColIdUnidadMedida.Visible = False
        Me.ColIdUnidadMedida.Width = 55
        '
        'ColLote
        '
        Me.ColLote.HeaderText = "Lote"
        Me.ColLote.MinimumWidth = 6
        Me.ColLote.Name = "ColLote"
        Me.ColLote.Width = 125
        '
        'colFechaVence
        '
        DataGridViewCellStyle9.Format = "d"
        DataGridViewCellStyle9.NullValue = Nothing
        Me.colFechaVence.DefaultCellStyle = DataGridViewCellStyle9
        Me.colFechaVence.HeaderText = "Fecha_Vence"
        Me.colFechaVence.MinimumWidth = 6
        Me.colFechaVence.Name = "colFechaVence"
        Me.colFechaVence.Width = 125
        '
        'ColUbicacion
        '
        Me.ColUbicacion.HeaderText = "Ubicacion"
        Me.ColUbicacion.MinimumWidth = 6
        Me.ColUbicacion.Name = "ColUbicacion"
        Me.ColUbicacion.Width = 125
        '
        'colIdPropietarioBodega
        '
        Me.colIdPropietarioBodega.HeaderText = "IdPropBodega"
        Me.colIdPropietarioBodega.MinimumWidth = 6
        Me.colIdPropietarioBodega.Name = "colIdPropietarioBodega"
        Me.colIdPropietarioBodega.Width = 125
        '
        'colnombre_propietario
        '
        Me.colnombre_propietario.HeaderText = "Propietario"
        Me.colnombre_propietario.MinimumWidth = 6
        Me.colnombre_propietario.Name = "colnombre_propietario"
        Me.colnombre_propietario.Width = 125
        '
        'ColLp
        '
        Me.ColLp.HeaderText = "Licence Plate"
        Me.ColLp.MinimumWidth = 6
        Me.ColLp.Name = "ColLp"
        Me.ColLp.Width = 125
        '
        'ColCodVariante
        '
        Me.ColCodVariante.HeaderText = "Cod_Variante"
        Me.ColCodVariante.MinimumWidth = 6
        Me.ColCodVariante.Name = "ColCodVariante"
        Me.ColCodVariante.Width = 125
        '
        'colCosto
        '
        Me.colCosto.HeaderText = "Costo"
        Me.colCosto.MinimumWidth = 6
        Me.colCosto.Name = "colCosto"
        Me.colCosto.Width = 125
        '
        'colPrecio
        '
        Me.colPrecio.HeaderText = "Precio"
        Me.colPrecio.MinimumWidth = 6
        Me.colPrecio.Name = "colPrecio"
        Me.colPrecio.Width = 125
        '
        'colParametro_a
        '
        Me.colParametro_a.HeaderText = "Parametro_a"
        Me.colParametro_a.MinimumWidth = 6
        Me.colParametro_a.Name = "colParametro_a"
        Me.colParametro_a.Width = 125
        '
        'colParametro_b
        '
        Me.colParametro_b.HeaderText = "Parametro_b"
        Me.colParametro_b.MinimumWidth = 6
        Me.colParametro_b.Name = "colParametro_b"
        Me.colParametro_b.Width = 125
        '
        'ColCodigo_Area
        '
        Me.ColCodigo_Area.HeaderText = "Codigo_Area"
        Me.ColCodigo_Area.MinimumWidth = 6
        Me.ColCodigo_Area.Name = "ColCodigo_Area"
        Me.ColCodigo_Area.Width = 125
        '
        'ColColor
        '
        Me.ColColor.HeaderText = "Color"
        Me.ColColor.Name = "ColColor"
        '
        'ColTalla
        '
        Me.ColTalla.HeaderText = "Talla"
        Me.ColTalla.Name = "ColTalla"
        '
        'colError
        '
        Me.colError.HeaderText = "Error"
        Me.colError.MinimumWidth = 6
        Me.colError.Name = "colError"
        Me.colError.ReadOnly = True
        Me.colError.Width = 125
        '
        'frmInventarioImport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1335, 592)
        Me.Controls.Add(Me.grpVerificaInv)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frmInventarioImport"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Importación de inventario"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpVerificaInv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpVerificaInv.ResumeLayout(False)
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsExcel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout

End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents mnuAplicar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuValidar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents grpVerificaInv As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cmdPegar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents grdData As DataGridView
    Friend WithEvents cmbPropietario As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblPropietario As Label
    Friend WithEvents prg As ProgressBar
    Friend WithEvents BarStaticItem1 As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents RibbonPageCategory1 As DevExpress.XtraBars.Ribbon.RibbonPageCategory
    Friend WithEvents lblPrg As Label
    Friend WithEvents RibbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonPageGroup3 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents cmdAdd As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdDel As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup4 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuImportarExcel As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lblRegs As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents ckInsertaStock As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents BarStaticItem2 As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents btgInsInv As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents DsExcel As DsExcel
    Friend WithEvents lblTipoImportacion As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents RibbonPageGroup5 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents colEstado As DataGridViewTextBoxColumn
    Friend WithEvents colId As DataGridViewTextBoxColumn
    Friend WithEvents colCodigo As DataGridViewTextBoxColumn
    Friend WithEvents colPresentacion As DataGridViewTextBoxColumn
    Friend WithEvents colCantidad As DataGridViewTextBoxColumn
    Friend WithEvents colPeso As DataGridViewTextBoxColumn
    Friend WithEvents colUnidadMedida As DataGridViewTextBoxColumn
    Friend WithEvents colIdProducto As DataGridViewTextBoxColumn
    Friend WithEvents colIdPresentacion As DataGridViewTextBoxColumn
    Friend WithEvents ColIdUnidadMedida As DataGridViewTextBoxColumn
    Friend WithEvents ColLote As DataGridViewTextBoxColumn
    Friend WithEvents colFechaVence As DataGridViewTextBoxColumn
    Friend WithEvents ColUbicacion As DataGridViewTextBoxColumn
    Friend WithEvents colIdPropietarioBodega As DataGridViewTextBoxColumn
    Friend WithEvents colnombre_propietario As DataGridViewTextBoxColumn
    Friend WithEvents ColLp As DataGridViewTextBoxColumn
    Friend WithEvents ColCodVariante As DataGridViewTextBoxColumn
    Friend WithEvents colCosto As DataGridViewTextBoxColumn
    Friend WithEvents colPrecio As DataGridViewTextBoxColumn
    Friend WithEvents colParametro_a As DataGridViewTextBoxColumn
    Friend WithEvents colParametro_b As DataGridViewTextBoxColumn
    Friend WithEvents ColCodigo_Area As DataGridViewTextBoxColumn
    Friend WithEvents ColColor As DataGridViewTextBoxColumn
    Friend WithEvents ColTalla As DataGridViewTextBoxColumn
    Friend WithEvents colError As DataGridViewTextBoxColumn
End Class
