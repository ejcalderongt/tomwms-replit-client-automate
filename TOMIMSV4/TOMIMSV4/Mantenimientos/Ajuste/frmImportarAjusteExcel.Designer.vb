'=============================================================================
' ARCHIVO: frmImportarAjusteExcel.Designer.vb
'
' DESCRIPCIÓN:
'   Parte visual del formulario (Partial Class).
'   Declaraciones de controles, Dispose e InitializeComponent.
'   La lógica vive en frmImportarAjusteExcel.vb
'=============================================================================

Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.XtraBars
Imports DevExpress.XtraBars.Ribbon
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraTab
Imports DevExpress.Utils

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class frmImportarAjusteExcel

    '─── Controles del formulario (DevExpress) ───────────────────────────────
    Friend Shadows WithEvents ribbon As RibbonControl
    Friend WithEvents rpHome As RibbonPage
    Friend WithEvents rpgArchivo As RibbonPageGroup
    Friend WithEvents rpgValidacion As RibbonPageGroup
    Friend WithEvents rpgMotivos As RibbonPageGroup
    Friend WithEvents rpgProcesar As RibbonPageGroup
    Friend WithEvents rpgImprimir As RibbonPageGroup
    Friend Shadows WithEvents statusBar As RibbonStatusBar

    Friend WithEvents btnSeleccionar As BarButtonItem
    Friend WithEvents btnDescargarPlantilla As BarButtonItem
    Friend WithEvents btnInsertarMotivos As BarButtonItem
    Friend WithEvents btnValidar As BarButtonItem
    Friend WithEvents btnImprimirDatos As BarButtonItem
    Friend WithEvents btnImprimirErrores As BarButtonItem

    Friend WithEvents barStatusInfo As BarStaticItem
    Friend WithEvents barStatusConteo As BarStaticItem

    Friend WithEvents pnlInfo As PanelControl

    Friend WithEvents lblTitulo As LabelControl
    Friend WithEvents lblArchivoLbl As LabelControl
    Friend WithEvents txtRutaArchivo As TextEdit

    Friend WithEvents btnProcesar As BarButtonItem
    Friend WithEvents btnCancelar As BarButtonItem

    Friend WithEvents tabControl As XtraTabControl
    Friend WithEvents tabDatos As XtraTabPage
    Friend WithEvents tabErrores As XtraTabPage

    Friend WithEvents gridPreview As GridControl
    Friend WithEvents gvPreview As GridView
    Friend WithEvents dtPreview As DataTable

    Friend WithEvents gridErrores As GridControl
    Friend WithEvents gvErrores As GridView
    Friend WithEvents dtErrores As DataTable

    Friend WithEvents progressBar As DevExpress.XtraEditors.MarqueeProgressBarControl

    '─── Dispose ─────────────────────────────────────────────────────────────
    Private components As System.ComponentModel.IContainer

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    '=========================================================================
    ' CONSTRUCCIÓN DINÁMICA DE LA INTERFAZ
    '=========================================================================
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmImportarAjusteExcel))
        Me.dtPreview = New System.Data.DataTable()
        Me.dtErrores = New System.Data.DataTable()
        Me.ribbon = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.btnSeleccionar = New DevExpress.XtraBars.BarButtonItem()
        Me.btnDescargarPlantilla = New DevExpress.XtraBars.BarButtonItem()
        Me.btnValidar = New DevExpress.XtraBars.BarButtonItem()
        Me.btnInsertarMotivos = New DevExpress.XtraBars.BarButtonItem()
        Me.btnProcesar = New DevExpress.XtraBars.BarButtonItem()
        Me.btnCancelar = New DevExpress.XtraBars.BarButtonItem()
        Me.btnImprimirDatos = New DevExpress.XtraBars.BarButtonItem()
        Me.btnImprimirErrores = New DevExpress.XtraBars.BarButtonItem()
        Me.barStatusInfo = New DevExpress.XtraBars.BarStaticItem()
        Me.barStatusConteo = New DevExpress.XtraBars.BarStaticItem()
        Me.rpHome = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.rpgArchivo = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rpgValidacion = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rpgMotivos = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rpgProcesar = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rpgImprimir = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.statusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.pnlInfo = New DevExpress.XtraEditors.PanelControl()
        Me.lblTitulo = New DevExpress.XtraEditors.LabelControl()
        Me.lblArchivoLbl = New DevExpress.XtraEditors.LabelControl()
        Me.txtRutaArchivo = New DevExpress.XtraEditors.TextEdit()
        Me.progressBar = New DevExpress.XtraEditors.MarqueeProgressBarControl()
        Me.tabControl = New DevExpress.XtraTab.XtraTabControl()
        Me.tabDatos = New DevExpress.XtraTab.XtraTabPage()
        Me.gridPreview = New DevExpress.XtraGrid.GridControl()
        Me.gvPreview = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colEstado = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colHoja = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colFila = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIdUbicacion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colUbicacion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIdStock = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCodigo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colNombre = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colLote = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colLoteNuevo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTipo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCantidad = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colMotivo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colObservacion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.tabErrores = New DevExpress.XtraTab.XtraTabPage()
        Me.gridErrores = New DevExpress.XtraGrid.GridControl()
        Me.gvErrores = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.errColHoja = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.errColFila = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.errColCodigo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.errColError = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.dtPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtErrores, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ribbon, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pnlInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlInfo.SuspendLayout()
        CType(Me.txtRutaArchivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressBar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tabControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabControl.SuspendLayout()
        Me.tabDatos.SuspendLayout()
        CType(Me.gridPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabErrores.SuspendLayout()
        CType(Me.gridErrores, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvErrores, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dtPreview
        '
        Me.dtPreview.TableName = "Preview"
        '
        'dtErrores
        '
        Me.dtErrores.TableName = "Errores"
        '
        'ribbon
        '
        Me.ribbon.ExpandCollapseItem.Id = 0
        Me.ribbon.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.ribbon.ExpandCollapseItem, Me.btnSeleccionar, Me.btnDescargarPlantilla, Me.btnValidar, Me.btnInsertarMotivos, Me.btnProcesar, Me.btnCancelar, Me.btnImprimirDatos, Me.btnImprimirErrores, Me.barStatusInfo, Me.barStatusConteo})
        Me.ribbon.Location = New System.Drawing.Point(0, 0)
        Me.ribbon.MaxItemId = 11
        Me.ribbon.Name = "ribbon"
        Me.ribbon.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.rpHome})
        Me.ribbon.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2010
        Me.ribbon.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.ribbon.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide
        Me.ribbon.ShowToolbarCustomizeItem = False
        Me.ribbon.Size = New System.Drawing.Size(1198, 193)
        Me.ribbon.StatusBar = Me.statusBar
        Me.ribbon.Toolbar.ShowCustomizeItem = False
        Me.ribbon.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden
        '
        'btnSeleccionar
        '
        Me.btnSeleccionar.Caption = "Seleccionar Excel"
        Me.btnSeleccionar.Hint = "Seleccionar el archivo Excel a importar"
        Me.btnSeleccionar.Id = 1
        Me.btnSeleccionar.ImageOptions.SvgImage = CType(resources.GetObject("btnSeleccionar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnSeleccionar.Name = "btnSeleccionar"
        Me.btnSeleccionar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.btnSeleccionar.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large
        '
        'btnDescargarPlantilla
        '
        Me.btnDescargarPlantilla.Caption = "Descargar" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Plantilla"
        Me.btnDescargarPlantilla.Hint = "Descargar la plantilla oficial Excel"
        Me.btnDescargarPlantilla.Id = 2
        Me.btnDescargarPlantilla.ImageOptions.SvgImage = CType(resources.GetObject("btnDescargarPlantilla.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnDescargarPlantilla.Name = "btnDescargarPlantilla"
        Me.btnDescargarPlantilla.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.btnDescargarPlantilla.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large
        '
        'btnValidar
        '
        Me.btnValidar.Caption = "Validar"
        Me.btnValidar.Enabled = False
        Me.btnValidar.Hint = "Validar el contenido del Excel contra la base de datos"
        Me.btnValidar.Id = 3
        Me.btnValidar.ImageOptions.SvgImage = CType(resources.GetObject("btnValidar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnValidar.Name = "btnValidar"
        Me.btnValidar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.btnValidar.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large
        '
        'btnInsertarMotivos
        '
        Me.btnInsertarMotivos.Caption = "Insertar Motivos" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Faltantes"
        Me.btnInsertarMotivos.Hint = "Insertar los motivos de ajuste estándar que aún no existan en la BD"
        Me.btnInsertarMotivos.Id = 4
        Me.btnInsertarMotivos.ImageOptions.SvgImage = CType(resources.GetObject("btnInsertarMotivos.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnInsertarMotivos.Name = "btnInsertarMotivos"
        Me.btnInsertarMotivos.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.btnInsertarMotivos.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large
        '
        'btnProcesar
        '
        Me.btnProcesar.Caption = "Procesar"
        Me.btnProcesar.Enabled = False
        Me.btnProcesar.Hint = "Cargar las filas válidas al ajuste de stock"
        Me.btnProcesar.Id = 5
        Me.btnProcesar.ImageOptions.SvgImage = CType(resources.GetObject("btnProcesar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnProcesar.Name = "btnProcesar"
        Me.btnProcesar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.btnProcesar.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large
        '
        'btnCancelar
        '
        Me.btnCancelar.Caption = "Cancelar"
        Me.btnCancelar.Hint = "Cerrar sin cargar nada"
        Me.btnCancelar.Id = 6
        Me.btnCancelar.ImageOptions.SvgImage = CType(resources.GetObject("btnCancelar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.btnCancelar.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large
        '
        'btnImprimirDatos
        '
        Me.btnImprimirDatos.Caption = "Imprimir" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Vista Previa"
        Me.btnImprimirDatos.Enabled = False
        Me.btnImprimirDatos.Hint = "Imprimir la lista de la Vista Previa de Datos"
        Me.btnImprimirDatos.Id = 7
        Me.btnImprimirDatos.ImageOptions.SvgImage = CType(resources.GetObject("btnImprimirDatos.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnImprimirDatos.Name = "btnImprimirDatos"
        Me.btnImprimirDatos.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.btnImprimirDatos.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large
        '
        'btnImprimirErrores
        '
        Me.btnImprimirErrores.Caption = "Imprimir" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Errores"
        Me.btnImprimirErrores.Enabled = False
        Me.btnImprimirErrores.Hint = "Imprimir la lista de Errores de Validación"
        Me.btnImprimirErrores.Id = 8
        Me.btnImprimirErrores.ImageOptions.SvgImage = CType(resources.GetObject("btnImprimirErrores.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnImprimirErrores.Name = "btnImprimirErrores"
        Me.btnImprimirErrores.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.btnImprimirErrores.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large
        '
        'barStatusInfo
        '
        Me.barStatusInfo.Caption = "Listo."
        Me.barStatusInfo.Hint = "Estado actual del proceso"
        Me.barStatusInfo.Id = 9
        Me.barStatusInfo.ItemAppearance.Normal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.barStatusInfo.ItemAppearance.Normal.Options.UseForeColor = True
        Me.barStatusInfo.Name = "barStatusInfo"
        '
        'barStatusConteo
        '
        Me.barStatusConteo.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
        Me.barStatusConteo.Hint = "Cantidad de registros en la vista actual"
        Me.barStatusConteo.Id = 10
        Me.barStatusConteo.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.barStatusConteo.ItemAppearance.Normal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.barStatusConteo.ItemAppearance.Normal.Options.UseFont = True
        Me.barStatusConteo.ItemAppearance.Normal.Options.UseForeColor = True
        Me.barStatusConteo.Name = "barStatusConteo"
        '
        'rpHome
        '
        Me.rpHome.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.rpgArchivo, Me.rpgValidacion, Me.rpgMotivos, Me.rpgProcesar, Me.rpgImprimir})
        Me.rpHome.Name = "rpHome"
        Me.rpHome.Text = "Importar Ajuste"
        '
        'rpgArchivo
        '
        Me.rpgArchivo.AllowTextClipping = False
        Me.rpgArchivo.ItemLinks.Add(Me.btnSeleccionar)
        Me.rpgArchivo.ItemLinks.Add(Me.btnDescargarPlantilla)
        Me.rpgArchivo.Name = "rpgArchivo"
        Me.rpgArchivo.Text = "Archivo"
        '
        'rpgValidacion
        '
        Me.rpgValidacion.AllowTextClipping = False
        Me.rpgValidacion.ItemLinks.Add(Me.btnValidar)
        Me.rpgValidacion.Name = "rpgValidacion"
        Me.rpgValidacion.Text = "Validación"
        '
        'rpgMotivos
        '
        Me.rpgMotivos.AllowTextClipping = False
        Me.rpgMotivos.ItemLinks.Add(Me.btnInsertarMotivos)
        Me.rpgMotivos.Name = "rpgMotivos"
        Me.rpgMotivos.Text = "Mantenimiento"
        '
        'rpgProcesar
        '
        Me.rpgProcesar.AllowTextClipping = False
        Me.rpgProcesar.ItemLinks.Add(Me.btnProcesar)
        Me.rpgProcesar.ItemLinks.Add(Me.btnCancelar)
        Me.rpgProcesar.Name = "rpgProcesar"
        Me.rpgProcesar.Text = "Procesar"
        '
        'rpgImprimir
        '
        Me.rpgImprimir.AllowTextClipping = False
        Me.rpgImprimir.ItemLinks.Add(Me.btnImprimirDatos)
        Me.rpgImprimir.ItemLinks.Add(Me.btnImprimirErrores)
        Me.rpgImprimir.Name = "rpgImprimir"
        Me.rpgImprimir.Text = "Imprimir"
        '
        'statusBar
        '
        Me.statusBar.ItemLinks.Add(Me.barStatusInfo)
        Me.statusBar.ItemLinks.Add(Me.barStatusConteo)
        Me.statusBar.Location = New System.Drawing.Point(0, -20)
        Me.statusBar.Name = "statusBar"
        Me.statusBar.Ribbon = Me.ribbon
        Me.statusBar.Size = New System.Drawing.Size(0, 20)
        '
        'pnlInfo
        '
        Me.pnlInfo.Appearance.BackColor = System.Drawing.SystemColors.Control
        Me.pnlInfo.Appearance.Options.UseBackColor = True
        Me.pnlInfo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.pnlInfo.Controls.Add(Me.lblTitulo)
        Me.pnlInfo.Controls.Add(Me.lblArchivoLbl)
        Me.pnlInfo.Controls.Add(Me.txtRutaArchivo)
        Me.pnlInfo.Controls.Add(Me.progressBar)
        Me.pnlInfo.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlInfo.Location = New System.Drawing.Point(0, 193)
        Me.pnlInfo.Name = "pnlInfo"
        Me.pnlInfo.Size = New System.Drawing.Size(1198, 80)
        Me.pnlInfo.TabIndex = 1
        '
        'lblTitulo
        '
        Me.lblTitulo.Appearance.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitulo.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblTitulo.Appearance.Options.UseFont = True
        Me.lblTitulo.Appearance.Options.UseForeColor = True
        Me.lblTitulo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblTitulo.Location = New System.Drawing.Point(12, 8)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(700, 24)
        Me.lblTitulo.TabIndex = 0
        Me.lblTitulo.Text = "Importación de Ajuste de Stock desde Excel"
        '
        'lblArchivoLbl
        '
        Me.lblArchivoLbl.Location = New System.Drawing.Point(12, 40)
        Me.lblArchivoLbl.Name = "lblArchivoLbl"
        Me.lblArchivoLbl.Size = New System.Drawing.Size(80, 16)
        Me.lblArchivoLbl.TabIndex = 1
        Me.lblArchivoLbl.Text = "Archivo Excel:"
        '
        'txtRutaArchivo
        '
        Me.txtRutaArchivo.Location = New System.Drawing.Point(100, 38)
        Me.txtRutaArchivo.Name = "txtRutaArchivo"
        Me.txtRutaArchivo.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.txtRutaArchivo.Properties.Appearance.Options.UseBackColor = True
        Me.txtRutaArchivo.Properties.ReadOnly = True
        Me.txtRutaArchivo.Size = New System.Drawing.Size(800, 22)
        Me.txtRutaArchivo.TabIndex = 2
        '
        'progressBar
        '
        Me.progressBar.EditValue = 0
        Me.progressBar.Location = New System.Drawing.Point(12, 66)
        Me.progressBar.Name = "progressBar"
        Me.progressBar.Size = New System.Drawing.Size(888, 6)
        Me.progressBar.TabIndex = 3
        Me.progressBar.Visible = False
        '
        'tabControl
        '
        Me.tabControl.AppearancePage.Header.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.tabControl.AppearancePage.Header.Options.UseFont = True
        Me.tabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabControl.Location = New System.Drawing.Point(0, 273)
        Me.tabControl.Name = "tabControl"
        Me.tabControl.SelectedTabPage = Me.tabDatos
        Me.tabControl.Size = New System.Drawing.Size(1198, 426)
        Me.tabControl.TabIndex = 0
        Me.tabControl.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tabDatos, Me.tabErrores})
        '
        'tabDatos
        '
        Me.tabDatos.Controls.Add(Me.gridPreview)
        Me.tabDatos.Name = "tabDatos"
        Me.tabDatos.Size = New System.Drawing.Size(1196, 392)
        Me.tabDatos.Text = "Vista Previa de Datos"
        '
        'gridPreview
        '
        Me.gridPreview.DataSource = Me.dtPreview
        Me.gridPreview.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gridPreview.Location = New System.Drawing.Point(0, 0)
        Me.gridPreview.MainView = Me.gvPreview
        Me.gridPreview.Name = "gridPreview"
        Me.gridPreview.Size = New System.Drawing.Size(1196, 392)
        Me.gridPreview.TabIndex = 0
        Me.gridPreview.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvPreview})
        '
        'gvPreview
        '
        Me.gvPreview.Appearance.ColumnFilterButton.BackColor = System.Drawing.SystemColors.Control
        Me.gvPreview.Appearance.ColumnFilterButton.Options.UseBackColor = True
        Me.gvPreview.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.SystemColors.Control
        Me.gvPreview.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.gvPreview.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.gvPreview.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
        Me.gvPreview.Appearance.HeaderPanel.ForeColor = System.Drawing.SystemColors.Control
        Me.gvPreview.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.gvPreview.Appearance.HeaderPanel.Options.UseFont = True
        Me.gvPreview.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvPreview.Appearance.OddRow.Options.UseBackColor = True
        Me.gvPreview.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colEstado, Me.colHoja, Me.colFila, Me.colIdUbicacion, Me.colUbicacion, Me.colIdStock, Me.colCodigo, Me.colNombre, Me.colLote, Me.colLoteNuevo, Me.colTipo, Me.colCantidad, Me.colMotivo, Me.colObservacion})
        Me.gvPreview.GridControl = Me.gridPreview
        Me.gvPreview.Name = "gvPreview"
        Me.gvPreview.OptionsBehavior.Editable = False
        Me.gvPreview.OptionsBehavior.ReadOnly = True
        Me.gvPreview.OptionsSelection.MultiSelect = True
        Me.gvPreview.OptionsView.ColumnAutoWidth = False
        Me.gvPreview.OptionsView.EnableAppearanceEvenRow = True
        Me.gvPreview.OptionsView.EnableAppearanceOddRow = True
        Me.gvPreview.OptionsView.ShowAutoFilterRow = True
        Me.gvPreview.OptionsView.ShowGroupPanel = False
        '
        'colEstado
        '
        Me.colEstado.Caption = "Estado"
        Me.colEstado.FieldName = "Estado"
        Me.colEstado.Name = "colEstado"
        Me.colEstado.OptionsColumn.AllowEdit = False
        Me.colEstado.Visible = True
        Me.colEstado.VisibleIndex = 0
        Me.colEstado.Width = 70
        '
        'colHoja
        '
        Me.colHoja.Caption = "Hoja"
        Me.colHoja.FieldName = "Hoja"
        Me.colHoja.Name = "colHoja"
        Me.colHoja.OptionsColumn.AllowEdit = False
        Me.colHoja.Visible = True
        Me.colHoja.VisibleIndex = 1
        Me.colHoja.Width = 100
        '
        'colFila
        '
        Me.colFila.Caption = "Fila"
        Me.colFila.FieldName = "Fila"
        Me.colFila.Name = "colFila"
        Me.colFila.OptionsColumn.AllowEdit = False
        Me.colFila.Visible = True
        Me.colFila.VisibleIndex = 2
        Me.colFila.Width = 50
        '
        'colIdUbicacion
        '
        Me.colIdUbicacion.Caption = "IdUbicacion"
        Me.colIdUbicacion.FieldName = "IdUbicacion"
        Me.colIdUbicacion.Name = "colIdUbicacion"
        Me.colIdUbicacion.OptionsColumn.AllowEdit = False
        Me.colIdUbicacion.Visible = True
        Me.colIdUbicacion.VisibleIndex = 3
        Me.colIdUbicacion.Width = 90
        '
        'colUbicacion
        '
        Me.colUbicacion.Caption = "Ubicación"
        Me.colUbicacion.FieldName = "Ubicacion"
        Me.colUbicacion.Name = "colUbicacion"
        Me.colUbicacion.OptionsColumn.AllowEdit = False
        Me.colUbicacion.Visible = True
        Me.colUbicacion.VisibleIndex = 4
        Me.colUbicacion.Width = 180
        '
        'colIdStock
        '
        Me.colIdStock.Caption = "IdStock"
        Me.colIdStock.FieldName = "IdStock"
        Me.colIdStock.Name = "colIdStock"
        Me.colIdStock.OptionsColumn.AllowEdit = False
        Me.colIdStock.Visible = True
        Me.colIdStock.VisibleIndex = 5
        Me.colIdStock.Width = 80
        '
        'colCodigo
        '
        Me.colCodigo.Caption = "Código"
        Me.colCodigo.FieldName = "Codigo"
        Me.colCodigo.Name = "colCodigo"
        Me.colCodigo.OptionsColumn.AllowEdit = False
        Me.colCodigo.Visible = True
        Me.colCodigo.VisibleIndex = 6
        Me.colCodigo.Width = 120
        '
        'colNombre
        '
        Me.colNombre.Caption = "Nombre Producto"
        Me.colNombre.FieldName = "Nombre"
        Me.colNombre.Name = "colNombre"
        Me.colNombre.OptionsColumn.AllowEdit = False
        Me.colNombre.Visible = True
        Me.colNombre.VisibleIndex = 7
        Me.colNombre.Width = 200
        '
        'colLote
        '
        Me.colLote.Caption = "Lote / Lote Ant."
        Me.colLote.FieldName = "Lote"
        Me.colLote.Name = "colLote"
        Me.colLote.OptionsColumn.AllowEdit = False
        Me.colLote.Visible = True
        Me.colLote.VisibleIndex = 8
        Me.colLote.Width = 110
        '
        'colLoteNuevo
        '
        Me.colLoteNuevo.Caption = "Lote Nuevo"
        Me.colLoteNuevo.FieldName = "LoteNuevo"
        Me.colLoteNuevo.Name = "colLoteNuevo"
        Me.colLoteNuevo.OptionsColumn.AllowEdit = False
        Me.colLoteNuevo.Visible = True
        Me.colLoteNuevo.VisibleIndex = 9
        Me.colLoteNuevo.Width = 110
        '
        'colTipo
        '
        Me.colTipo.Caption = "Tipo Ajuste"
        Me.colTipo.FieldName = "Tipo"
        Me.colTipo.Name = "colTipo"
        Me.colTipo.OptionsColumn.AllowEdit = False
        Me.colTipo.Visible = True
        Me.colTipo.VisibleIndex = 10
        Me.colTipo.Width = 100
        '
        'colCantidad
        '
        Me.colCantidad.Caption = "Cantidad"
        Me.colCantidad.DisplayFormat.FormatString = "N2"
        Me.colCantidad.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colCantidad.FieldName = "Cantidad"
        Me.colCantidad.Name = "colCantidad"
        Me.colCantidad.OptionsColumn.AllowEdit = False
        Me.colCantidad.Visible = True
        Me.colCantidad.VisibleIndex = 11
        Me.colCantidad.Width = 90
        '
        'colMotivo
        '
        Me.colMotivo.Caption = "Motivo"
        Me.colMotivo.FieldName = "Motivo"
        Me.colMotivo.Name = "colMotivo"
        Me.colMotivo.OptionsColumn.AllowEdit = False
        Me.colMotivo.Visible = True
        Me.colMotivo.VisibleIndex = 12
        Me.colMotivo.Width = 90
        '
        'colObservacion
        '
        Me.colObservacion.Caption = "Observación"
        Me.colObservacion.FieldName = "Observacion"
        Me.colObservacion.Name = "colObservacion"
        Me.colObservacion.OptionsColumn.AllowEdit = False
        Me.colObservacion.Visible = True
        Me.colObservacion.VisibleIndex = 13
        Me.colObservacion.Width = 160
        '
        'tabErrores
        '
        Me.tabErrores.Controls.Add(Me.gridErrores)
        Me.tabErrores.Name = "tabErrores"
        Me.tabErrores.Size = New System.Drawing.Size(1196, 392)
        Me.tabErrores.Text = "Errores de Validación  (0)"
        '
        'gridErrores
        '
        Me.gridErrores.DataSource = Me.dtErrores
        Me.gridErrores.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gridErrores.Location = New System.Drawing.Point(0, 0)
        Me.gridErrores.MainView = Me.gvErrores
        Me.gridErrores.Name = "gridErrores"
        Me.gridErrores.Size = New System.Drawing.Size(1196, 392)
        Me.gridErrores.TabIndex = 0
        Me.gridErrores.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvErrores})
        '
        'gvErrores
        '
        Me.gvErrores.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(150, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.gvErrores.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
        Me.gvErrores.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.gvErrores.Appearance.Row.Options.UseBackColor = True
        Me.gvErrores.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.errColHoja, Me.errColFila, Me.errColCodigo, Me.errColError})
        Me.gvErrores.GridControl = Me.gridErrores
        Me.gvErrores.Name = "gvErrores"
        Me.gvErrores.OptionsBehavior.Editable = False
        Me.gvErrores.OptionsBehavior.ReadOnly = True
        Me.gvErrores.OptionsSelection.MultiSelect = True
        Me.gvErrores.OptionsView.ColumnAutoWidth = False
        Me.gvErrores.OptionsView.ShowAutoFilterRow = True
        Me.gvErrores.OptionsView.ShowGroupPanel = False
        '
        'errColHoja
        '
        Me.errColHoja.Caption = "Hoja"
        Me.errColHoja.FieldName = "Hoja"
        Me.errColHoja.Name = "errColHoja"
        Me.errColHoja.Visible = True
        Me.errColHoja.VisibleIndex = 0
        Me.errColHoja.Width = 120
        '
        'errColFila
        '
        Me.errColFila.Caption = "Fila"
        Me.errColFila.FieldName = "Fila"
        Me.errColFila.Name = "errColFila"
        Me.errColFila.Visible = True
        Me.errColFila.VisibleIndex = 1
        Me.errColFila.Width = 60
        '
        'errColCodigo
        '
        Me.errColCodigo.Caption = "Código"
        Me.errColCodigo.FieldName = "Codigo"
        Me.errColCodigo.Name = "errColCodigo"
        Me.errColCodigo.Visible = True
        Me.errColCodigo.VisibleIndex = 2
        Me.errColCodigo.Width = 130
        '
        'errColError
        '
        Me.errColError.Caption = "Descripción del Error"
        Me.errColError.FieldName = "Error"
        Me.errColError.Name = "errColError"
        Me.errColError.Visible = True
        Me.errColError.VisibleIndex = 3
        Me.errColError.Width = 600
        '
        'frmImportarAjusteExcel
        '
        Me.AllowDrop = True
        Me.ClientSize = New System.Drawing.Size(1198, 699)
        Me.Controls.Add(Me.tabControl)
        Me.Controls.Add(Me.pnlInfo)
        Me.Controls.Add(Me.ribbon)
        Me.Name = "frmImportarAjusteExcel"
        Me.ribbon = Me.ribbon
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Importar Ajuste de Stock desde Excel"
        CType(Me.dtPreview, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtErrores, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ribbon, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pnlInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlInfo.ResumeLayout(False)
        Me.pnlInfo.PerformLayout()
        CType(Me.txtRutaArchivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressBar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tabControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabControl.ResumeLayout(False)
        Me.tabDatos.ResumeLayout(False)
        CType(Me.gridPreview, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvPreview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabErrores.ResumeLayout(False)
        CType(Me.gridErrores, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvErrores, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents colEstado As Columns.GridColumn
    Friend WithEvents colHoja As Columns.GridColumn
    Friend WithEvents colFila As Columns.GridColumn
    Friend WithEvents colIdUbicacion As Columns.GridColumn
    Friend WithEvents colUbicacion As Columns.GridColumn
    Friend WithEvents colIdStock As Columns.GridColumn
    Friend WithEvents colCodigo As Columns.GridColumn
    Friend WithEvents colNombre As Columns.GridColumn
    Friend WithEvents colLote As Columns.GridColumn
    Friend WithEvents colLoteNuevo As Columns.GridColumn
    Friend WithEvents colTipo As Columns.GridColumn
    Friend WithEvents colCantidad As Columns.GridColumn
    Friend WithEvents colMotivo As Columns.GridColumn
    Friend WithEvents colObservacion As Columns.GridColumn
    Friend WithEvents errColHoja As Columns.GridColumn
    Friend WithEvents errColFila As Columns.GridColumn
    Friend WithEvents errColCodigo As Columns.GridColumn
    Friend WithEvents errColError As Columns.GridColumn
End Class