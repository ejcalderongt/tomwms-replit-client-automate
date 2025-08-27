<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCapturaParametroPicking
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If components IsNot Nothing Then
                    components.Dispose()
                End If
                If pObjP IsNot Nothing Then
                    pObjP.Dispose()
                    pObjP = Nothing
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCapturaParametroPicking))
        Me.GrpStock = New DevExpress.XtraEditors.GroupControl()
        Me.GrpParametro = New DevExpress.XtraEditors.GroupControl()
        Me.Grid = New DevExpress.XtraGrid.GridControl()
        Me.DTBindingSource = New System.Windows.Forms.BindingSource()
        Me.DSPR = New TOMWMS.DSPR()
        Me.GrdP = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colTexto = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colNumerico = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colFecha = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colLogico = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.IdParametro = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDescripcion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IdParametroDet = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.lblRegs = New DevExpress.XtraBars.BarStaticItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.mnuNuevo = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuSalir = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImprimir = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.chkActivos = New DevExpress.XtraBars.BarCheckItem()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager()
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
        Me.xtrStock = New DevExpress.XtraTab.XtraTabControl()
        Me.Stock = New DevExpress.XtraTab.XtraTabPage()
        CType(Me.GrpStock, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpStock.SuspendLayout()
        CType(Me.GrpParametro, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpParametro.SuspendLayout()
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DTBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DSPR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrdP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtrStock, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtrStock.SuspendLayout()
        Me.Stock.SuspendLayout()
        Me.SuspendLayout()
        '
        'GrpStock
        '
        Me.GrpStock.Controls.Add(Me.GrpParametro)
        Me.GrpStock.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpStock.Location = New System.Drawing.Point(0, 0)
        Me.GrpStock.Name = "GrpStock"
        Me.GrpStock.Size = New System.Drawing.Size(571, 286)
        Me.GrpStock.TabIndex = 0
        '
        'GrpParametro
        '
        Me.GrpParametro.Controls.Add(Me.Grid)
        Me.GrpParametro.Location = New System.Drawing.Point(5, 23)
        Me.GrpParametro.Name = "GrpParametro"
        Me.GrpParametro.Size = New System.Drawing.Size(551, 252)
        Me.GrpParametro.TabIndex = 0
        Me.GrpParametro.Text = "Parametros"
        '
        'Grid
        '
        Me.Grid.Cursor = System.Windows.Forms.Cursors.Default
        Me.Grid.DataSource = Me.DTBindingSource
        Me.Grid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Grid.Location = New System.Drawing.Point(2, 20)
        Me.Grid.MainView = Me.GrdP
        Me.Grid.Name = "Grid"
        Me.Grid.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
        Me.Grid.Size = New System.Drawing.Size(547, 230)
        Me.Grid.TabIndex = 0
        Me.Grid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GrdP})
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
        'GrdP
        '
        Me.GrdP.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colTexto, Me.colNumerico, Me.colFecha, Me.colLogico, Me.IdParametro, Me.colDescripcion, Me.IdParametroDet})
        Me.GrdP.GridControl = Me.Grid
        Me.GrdP.Name = "GrdP"
        Me.GrdP.OptionsFind.AlwaysVisible = True
        '
        'colTexto
        '
        Me.colTexto.Caption = "Texto"
        Me.colTexto.FieldName = "colTexto"
        Me.colTexto.Name = "colTexto"
        Me.colTexto.Visible = True
        Me.colTexto.VisibleIndex = 1
        '
        'colNumerico
        '
        Me.colNumerico.Caption = "Númerico"
        Me.colNumerico.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colNumerico.FieldName = "colNumerico"
        Me.colNumerico.Name = "colNumerico"
        Me.colNumerico.Visible = True
        Me.colNumerico.VisibleIndex = 2
        '
        'colFecha
        '
        Me.colFecha.Caption = "Fecha"
        Me.colFecha.DisplayFormat.FormatString = "d"
        Me.colFecha.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.colFecha.FieldName = "colFecha"
        Me.colFecha.Name = "colFecha"
        Me.colFecha.Visible = True
        Me.colFecha.VisibleIndex = 3
        '
        'colLogico
        '
        Me.colLogico.Caption = "Lógico"
        Me.colLogico.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.colLogico.FieldName = "colLogico"
        Me.colLogico.Name = "colLogico"
        Me.colLogico.Visible = True
        Me.colLogico.VisibleIndex = 4
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        '
        'IdParametro
        '
        Me.IdParametro.Caption = "IdParametro"
        Me.IdParametro.FieldName = "IdParametro"
        Me.IdParametro.Name = "IdParametro"
        Me.IdParametro.OptionsColumn.ReadOnly = True
        '
        'colDescripcion
        '
        Me.colDescripcion.Caption = "Descripción"
        Me.colDescripcion.FieldName = "colDescripcion"
        Me.colDescripcion.Name = "colDescripcion"
        Me.colDescripcion.OptionsColumn.ReadOnly = True
        Me.colDescripcion.Visible = True
        Me.colDescripcion.VisibleIndex = 0
        '
        'IdParametroDet
        '
        Me.IdParametroDet.Caption = "IdParametroDet"
        Me.IdParametroDet.FieldName = "IdParametroDet"
        Me.IdParametroDet.Name = "IdParametroDet"
        Me.IdParametroDet.OptionsColumn.ReadOnly = True
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
        Me.Bar2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.cmdAcept), New DevExpress.XtraBars.LinkPersistInfo(Me.BarStaticItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdCancel)})
        Me.Bar2.OptionsBar.MultiLine = True
        Me.Bar2.OptionsBar.UseWholeRow = True
        Me.Bar2.Text = "Main menu"
        '
        'cmdAcept
        '
        Me.cmdAcept.Caption = "Aceptar"
        Me.cmdAcept.Id = 0
        Me.cmdAcept.Name = "cmdAcept"
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
        Me.cmdCancel.Name = "cmdCancel"
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Manager = Me.BarManager1
        Me.barDockControlTop.Size = New System.Drawing.Size(577, 22)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 336)
        Me.barDockControlBottom.Manager = Me.BarManager1
        Me.barDockControlBottom.Size = New System.Drawing.Size(577, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 22)
        Me.barDockControlLeft.Manager = Me.BarManager1
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 314)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(577, 22)
        Me.barDockControlRight.Manager = Me.BarManager1
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 314)
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
        'xtrStock
        '
        Me.xtrStock.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtrStock.Location = New System.Drawing.Point(0, 22)
        Me.xtrStock.Name = "xtrStock"
        Me.xtrStock.SelectedTabPage = Me.Stock
        Me.xtrStock.Size = New System.Drawing.Size(577, 314)
        Me.xtrStock.TabIndex = 0
        Me.xtrStock.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.Stock})
        '
        'Stock
        '
        Me.Stock.Controls.Add(Me.GrpStock)
        Me.Stock.Name = "Stock"
        Me.Stock.Size = New System.Drawing.Size(571, 286)
        Me.Stock.Text = "Stock"
        '
        'frmCapturaParametroPicking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(577, 336)
        Me.Controls.Add(Me.xtrStock)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCapturaParametroPicking"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.GrpStock, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpStock.ResumeLayout(False)
        CType(Me.GrpParametro, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpParametro.ResumeLayout(False)
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DTBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DSPR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrdP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtrStock, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtrStock.ResumeLayout(False)
        Me.Stock.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GrpStock As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GrpParametro As DevExpress.XtraEditors.GroupControl
    Private WithEvents Grid As DevExpress.XtraGrid.GridControl
    Private WithEvents GrdP As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colTexto As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colNumerico As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colFecha As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colLogico As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents IdParametro As DevExpress.XtraGrid.Columns.GridColumn
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
    Friend WithEvents DSPR As TOMWMS.DSPR
    Friend WithEvents colDescripcion As DevExpress.XtraGrid.Columns.GridColumn
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
    Friend WithEvents IdParametroDet As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xtrStock As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents Stock As DevExpress.XtraTab.XtraTabPage
End Class
