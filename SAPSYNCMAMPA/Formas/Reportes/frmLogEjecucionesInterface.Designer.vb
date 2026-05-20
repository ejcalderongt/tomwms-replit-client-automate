<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmLogEjecucionesInterface
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim GridLevelNode2 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLogEjecucionesInterface))
        Me.gvResultados = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.grdLog = New DevExpress.XtraGrid.GridControl()
        Me.gvErrores = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.gvEjecuciones = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.cmdActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdLimpiar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdExpandir = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdContraer = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImprimir = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdSalir = New DevExpress.XtraBars.BarButtonItem()
        Me.lblResumen = New DevExpress.XtraBars.BarStaticItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.pnlFiltros = New DevExpress.XtraEditors.PanelControl()
        Me.btnBuscar = New DevExpress.XtraEditors.SimpleButton()
        Me.txtTexto = New DevExpress.XtraEditors.TextEdit()
        Me.txtProceso = New DevExpress.XtraEditors.TextEdit()
        Me.txtTransaccion = New DevExpress.XtraEditors.TextEdit()
        Me.dtHasta = New DevExpress.XtraEditors.DateEdit()
        Me.dtDesde = New DevExpress.XtraEditors.DateEdit()
        Me.lblTexto = New DevExpress.XtraEditors.LabelControl()
        Me.lblProceso = New DevExpress.XtraEditors.LabelControl()
        Me.lblTransaccion = New DevExpress.XtraEditors.LabelControl()
        Me.lblHasta = New DevExpress.XtraEditors.LabelControl()
        Me.lblDesde = New DevExpress.XtraEditors.LabelControl()
        Me.lblFiltros = New DevExpress.XtraEditors.LabelControl()
        CType(Me.gvResultados, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdLog, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvErrores, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvEjecuciones, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pnlFiltros, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlFiltros.SuspendLayout()
        CType(Me.txtTexto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtProceso.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTransaccion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtHasta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtHasta.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtDesde.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtDesde.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gvResultados
        '
        Me.gvResultados.DetailHeight = 431
        Me.gvResultados.GridControl = Me.grdLog
        Me.gvResultados.Name = "gvResultados"
        Me.gvResultados.OptionsEditForm.PopupEditFormWidth = 933
        Me.gvResultados.OptionsFind.AlwaysVisible = True
        '
        'grdLog
        '
        Me.grdLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdLog.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode1.LevelTemplate = Me.gvResultados
        GridLevelNode1.RelationName = "Resultados"
        GridLevelNode2.LevelTemplate = Me.gvErrores
        GridLevelNode2.RelationName = "Errores"
        Me.grdLog.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1, GridLevelNode2})
        Me.grdLog.Location = New System.Drawing.Point(315, 193)
        Me.grdLog.MainView = Me.gvEjecuciones
        Me.grdLog.Margin = New System.Windows.Forms.Padding(4)
        Me.grdLog.MenuManager = Me.RibbonControl
        Me.grdLog.Name = "grdLog"
        Me.grdLog.Size = New System.Drawing.Size(1043, 629)
        Me.grdLog.TabIndex = 2
        Me.grdLog.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvErrores, Me.gvEjecuciones, Me.gvResultados})
        '
        'gvErrores
        '
        Me.gvErrores.DetailHeight = 431
        Me.gvErrores.GridControl = Me.grdLog
        Me.gvErrores.Name = "gvErrores"
        Me.gvErrores.OptionsEditForm.PopupEditFormWidth = 933
        Me.gvErrores.OptionsFind.AlwaysVisible = True
        '
        'gvEjecuciones
        '
        Me.gvEjecuciones.DetailHeight = 431
        Me.gvEjecuciones.GridControl = Me.grdLog
        Me.gvEjecuciones.Name = "gvEjecuciones"
        Me.gvEjecuciones.OptionsEditForm.PopupEditFormWidth = 933
        Me.gvEjecuciones.OptionsFind.AlwaysVisible = True
        '
        'RibbonControl
        '
        Me.RibbonControl.EmptyAreaImageOptions.ImagePadding = New System.Windows.Forms.Padding(35, 37, 35, 37)
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.cmdActualizar, Me.cmdLimpiar, Me.cmdExpandir, Me.cmdContraer, Me.cmdImprimir, Me.cmdSalir, Me.lblResumen})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(4)
        Me.RibbonControl.MaxItemId = 8
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.OptionsMenuMinWidth = 385
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1358, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'cmdActualizar
        '
        Me.cmdActualizar.Caption = "Actualizar"
        Me.cmdActualizar.Id = 1
        Me.cmdActualizar.ImageOptions.SvgImage = CType(resources.GetObject("cmdActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdActualizar.Name = "cmdActualizar"
        '
        'cmdLimpiar
        '
        Me.cmdLimpiar.Caption = "Limpiar filtros"
        Me.cmdLimpiar.Id = 2
        Me.cmdLimpiar.ImageOptions.SvgImage = CType(resources.GetObject("cmdLimpiar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdLimpiar.Name = "cmdLimpiar"
        '
        'cmdExpandir
        '
        Me.cmdExpandir.Caption = "Expandir"
        Me.cmdExpandir.Id = 3
        Me.cmdExpandir.ImageOptions.SvgImage = CType(resources.GetObject("cmdExpandir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdExpandir.Name = "cmdExpandir"
        '
        'cmdContraer
        '
        Me.cmdContraer.Caption = "Contraer"
        Me.cmdContraer.Id = 4
        Me.cmdContraer.ImageOptions.SvgImage = CType(resources.GetObject("cmdContraer.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdContraer.Name = "cmdContraer"
        '
        'cmdImprimir
        '
        Me.cmdImprimir.Caption = "Imprimir"
        Me.cmdImprimir.Id = 5
        Me.cmdImprimir.ImageOptions.SvgImage = CType(resources.GetObject("cmdImprimir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdImprimir.Name = "cmdImprimir"
        '
        'cmdSalir
        '
        Me.cmdSalir.Caption = "Salir"
        Me.cmdSalir.Id = 6
        Me.cmdSalir.ImageOptions.SvgImage = CType(resources.GetObject("cmdSalir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdSalir.Name = "cmdSalir"
        '
        'lblResumen
        '
        Me.lblResumen.Caption = "Ejecuciones: 0 | Resultados: 0 | Errores: 0"
        Me.lblResumen.Id = 7
        Me.lblResumen.Name = "lblResumen"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Log de Interface"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdLimpiar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdExpandir)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdContraer)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdImprimir)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdSalir)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        Me.RibbonPageGroup1.Text = "Opciones"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblResumen)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 822)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1358, 30)
        '
        'pnlFiltros
        '
        Me.pnlFiltros.Controls.Add(Me.btnBuscar)
        Me.pnlFiltros.Controls.Add(Me.txtTexto)
        Me.pnlFiltros.Controls.Add(Me.txtProceso)
        Me.pnlFiltros.Controls.Add(Me.txtTransaccion)
        Me.pnlFiltros.Controls.Add(Me.dtHasta)
        Me.pnlFiltros.Controls.Add(Me.dtDesde)
        Me.pnlFiltros.Controls.Add(Me.lblTexto)
        Me.pnlFiltros.Controls.Add(Me.lblProceso)
        Me.pnlFiltros.Controls.Add(Me.lblTransaccion)
        Me.pnlFiltros.Controls.Add(Me.lblHasta)
        Me.pnlFiltros.Controls.Add(Me.lblDesde)
        Me.pnlFiltros.Controls.Add(Me.lblFiltros)
        Me.pnlFiltros.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlFiltros.Location = New System.Drawing.Point(0, 193)
        Me.pnlFiltros.Margin = New System.Windows.Forms.Padding(4)
        Me.pnlFiltros.Name = "pnlFiltros"
        Me.pnlFiltros.Size = New System.Drawing.Size(315, 629)
        Me.pnlFiltros.TabIndex = 3
        '
        'btnBuscar
        '
        Me.btnBuscar.Location = New System.Drawing.Point(19, 407)
        Me.btnBuscar.Margin = New System.Windows.Forms.Padding(4)
        Me.btnBuscar.Name = "btnBuscar"
        Me.btnBuscar.Size = New System.Drawing.Size(278, 37)
        Me.btnBuscar.TabIndex = 11
        Me.btnBuscar.Text = "Buscar"
        '
        'txtTexto
        '
        Me.txtTexto.Location = New System.Drawing.Point(19, 340)
        Me.txtTexto.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTexto.MenuManager = Me.RibbonControl
        Me.txtTexto.Name = "txtTexto"
        Me.txtTexto.Size = New System.Drawing.Size(278, 22)
        Me.txtTexto.TabIndex = 10
        '
        'txtProceso
        '
        Me.txtProceso.Location = New System.Drawing.Point(19, 271)
        Me.txtProceso.Margin = New System.Windows.Forms.Padding(4)
        Me.txtProceso.MenuManager = Me.RibbonControl
        Me.txtProceso.Name = "txtProceso"
        Me.txtProceso.Size = New System.Drawing.Size(278, 22)
        Me.txtProceso.TabIndex = 9
        '
        'txtTransaccion
        '
        Me.txtTransaccion.Location = New System.Drawing.Point(19, 202)
        Me.txtTransaccion.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTransaccion.MenuManager = Me.RibbonControl
        Me.txtTransaccion.Name = "txtTransaccion"
        Me.txtTransaccion.Size = New System.Drawing.Size(278, 22)
        Me.txtTransaccion.TabIndex = 8
        '
        'dtHasta
        '
        Me.dtHasta.EditValue = Nothing
        Me.dtHasta.Location = New System.Drawing.Point(19, 133)
        Me.dtHasta.Margin = New System.Windows.Forms.Padding(4)
        Me.dtHasta.MenuManager = Me.RibbonControl
        Me.dtHasta.Name = "dtHasta"
        Me.dtHasta.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtHasta.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtHasta.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.dtHasta.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtHasta.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        Me.dtHasta.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtHasta.Properties.MaskSettings.Set("mask", "d")
        Me.dtHasta.Size = New System.Drawing.Size(278, 22)
        Me.dtHasta.TabIndex = 7
        '
        'dtDesde
        '
        Me.dtDesde.EditValue = Nothing
        Me.dtDesde.Location = New System.Drawing.Point(19, 69)
        Me.dtDesde.Margin = New System.Windows.Forms.Padding(4)
        Me.dtDesde.MenuManager = Me.RibbonControl
        Me.dtDesde.Name = "dtDesde"
        Me.dtDesde.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtDesde.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtDesde.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.dtDesde.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtDesde.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        Me.dtDesde.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtDesde.Properties.MaskSettings.Set("mask", "d")
        Me.dtDesde.Size = New System.Drawing.Size(278, 22)
        Me.dtDesde.TabIndex = 6
        '
        'lblTexto
        '
        Me.lblTexto.Location = New System.Drawing.Point(19, 316)
        Me.lblTexto.Margin = New System.Windows.Forms.Padding(4)
        Me.lblTexto.Name = "lblTexto"
        Me.lblTexto.Size = New System.Drawing.Size(66, 16)
        Me.lblTexto.TabIndex = 5
        Me.lblTexto.Text = "Texto libre:"
        '
        'lblProceso
        '
        Me.lblProceso.Location = New System.Drawing.Point(19, 247)
        Me.lblProceso.Margin = New System.Windows.Forms.Padding(4)
        Me.lblProceso.Name = "lblProceso"
        Me.lblProceso.Size = New System.Drawing.Size(50, 16)
        Me.lblProceso.TabIndex = 4
        Me.lblProceso.Text = "Proceso:"
        '
        'lblTransaccion
        '
        Me.lblTransaccion.Location = New System.Drawing.Point(19, 178)
        Me.lblTransaccion.Margin = New System.Windows.Forms.Padding(4)
        Me.lblTransaccion.Name = "lblTransaccion"
        Me.lblTransaccion.Size = New System.Drawing.Size(74, 16)
        Me.lblTransaccion.TabIndex = 3
        Me.lblTransaccion.Text = "Transaccion:"
        '
        'lblHasta
        '
        Me.lblHasta.Location = New System.Drawing.Point(19, 110)
        Me.lblHasta.Margin = New System.Windows.Forms.Padding(4)
        Me.lblHasta.Name = "lblHasta"
        Me.lblHasta.Size = New System.Drawing.Size(37, 16)
        Me.lblHasta.TabIndex = 2
        Me.lblHasta.Text = "Hasta:"
        '
        'lblDesde
        '
        Me.lblDesde.Location = New System.Drawing.Point(19, 46)
        Me.lblDesde.Margin = New System.Windows.Forms.Padding(4)
        Me.lblDesde.Name = "lblDesde"
        Me.lblDesde.Size = New System.Drawing.Size(40, 16)
        Me.lblDesde.TabIndex = 1
        Me.lblDesde.Text = "Desde:"
        '
        'lblFiltros
        '
        Me.lblFiltros.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblFiltros.Appearance.Options.UseFont = True
        Me.lblFiltros.Location = New System.Drawing.Point(19, 15)
        Me.lblFiltros.Margin = New System.Windows.Forms.Padding(4)
        Me.lblFiltros.Name = "lblFiltros"
        Me.lblFiltros.Size = New System.Drawing.Size(54, 21)
        Me.lblFiltros.TabIndex = 0
        Me.lblFiltros.Text = "Filtros"
        '
        'frmLogEjecucionesInterface
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1358, 852)
        Me.Controls.Add(Me.grdLog)
        Me.Controls.Add(Me.pnlFiltros)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmLogEjecucionesInterface"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Log de Ejecuciones de Interface"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.gvResultados, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdLog, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvErrores, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvEjecuciones, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pnlFiltros, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlFiltros.ResumeLayout(False)
        Me.pnlFiltros.PerformLayout()
        CType(Me.txtTexto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtProceso.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTransaccion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtHasta.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtHasta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtDesde.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtDesde.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents cmdActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdLimpiar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdExpandir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdContraer As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdImprimir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdSalir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lblResumen As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents grdLog As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvEjecuciones As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gvResultados As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gvErrores As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents pnlFiltros As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblFiltros As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblDesde As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblHasta As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblTransaccion As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblProceso As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblTexto As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dtDesde As DevExpress.XtraEditors.DateEdit
    Friend WithEvents dtHasta As DevExpress.XtraEditors.DateEdit
    Friend WithEvents txtTransaccion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtProceso As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtTexto As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnBuscar As DevExpress.XtraEditors.SimpleButton
End Class
