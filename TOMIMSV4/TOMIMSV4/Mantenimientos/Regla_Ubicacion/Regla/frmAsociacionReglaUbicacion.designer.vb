<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAsociacionReglaUbicacion
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If components IsNot Nothing Then
                    components.Dispose()
                End If
                If BeBodega IsNot Nothing Then
                    BeBodega.Dispose()
                    BeBodega = Nothing
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAsociacionReglaUbicacion))
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuNuevo = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuSalir = New DevExpress.XtraBars.BarButtonItem()
        Me.chkActivos = New DevExpress.XtraBars.BarCheckItem()
        Me.cmdImprimir = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuMarcarTodos = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.lblRegs = New DevExpress.XtraBars.BarStaticItem()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.DgridTramos = New DevExpress.XtraGrid.GridControl()
        Me.grdvTramo = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colNombre = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.colSeleccion1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colNombre1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsReglaUb = New TOMWMS.DsReglaUb()
        Me.DgridUbicaciones = New DevExpress.XtraGrid.GridControl()
        Me.grdvUbicaciones = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.colIdBodega = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IdProductoBodega = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.prg = New DevExpress.XtraEditors.ProgressBarControl()
        Me.CachedrptRecepcionDetalleInfraccion1 = New TOMWMS.CachedrptRecepcionDetalleInfraccion()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgridTramos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdvTramo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsReglaUb, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgridUbicaciones, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdvUbicaciones, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.prg.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.RibbonControl.SearchEditItem, Me.mnuNuevo, Me.mnuActualizar, Me.mnuSalir, Me.chkActivos, Me.cmdImprimir, Me.mnuMarcarTodos})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 10
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1296, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
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
        Me.mnuActualizar.ImageOptions.SvgImage = CType(resources.GetObject("mnuActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuActualizar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A))
        Me.mnuActualizar.Name = "mnuActualizar"
        '
        'mnuSalir
        '
        Me.mnuSalir.Caption = "Salir"
        Me.mnuSalir.Id = 3
        Me.mnuSalir.ImageOptions.SvgImage = CType(resources.GetObject("mnuSalir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuSalir.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S))
        Me.mnuSalir.Name = "mnuSalir"
        '
        'chkActivos
        '
        Me.chkActivos.BindableChecked = True
        Me.chkActivos.Caption = "Activos"
        Me.chkActivos.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText
        Me.chkActivos.Checked = True
        Me.chkActivos.Id = 4
        Me.chkActivos.Name = "chkActivos"
        Me.chkActivos.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'cmdImprimir
        '
        Me.cmdImprimir.Caption = "Imprimir"
        Me.cmdImprimir.Id = 6
        Me.cmdImprimir.ImageOptions.SvgImage = CType(resources.GetObject("cmdImprimir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdImprimir.Name = "cmdImprimir"
        '
        'mnuMarcarTodos
        '
        Me.mnuMarcarTodos.Caption = "Marcar/Desmarcar todos"
        Me.mnuMarcarTodos.Id = 9
        Me.mnuMarcarTodos.Name = "mnuMarcarTodos"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1, Me.RibbonPageGroup2})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Opciones de Lista"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdImprimir)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuSalir)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonPageGroup2
        '
        Me.RibbonPageGroup2.ItemLinks.Add(Me.mnuMarcarTodos)
        Me.RibbonPageGroup2.Name = "RibbonPageGroup2"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 679)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1296, 30)
        '
        'lblRegs
        '
        Me.lblRegs.Caption = "Registros: 0"
        Me.lblRegs.Id = 5
        Me.lblRegs.Name = "lblRegs"
        '
        'GridColumn1
        '
        Me.GridColumn1.FieldName = "Seleccion"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.FieldName = "ID"
        Me.GridColumn2.Name = "GridColumn2"
        '
        'GridColumn3
        '
        Me.GridColumn3.FieldName = "Nombre"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 1
        '
        'DgridTramos
        '
        Me.DgridTramos.Cursor = System.Windows.Forms.Cursors.Default
        Me.DgridTramos.DataMember = "Tramo"
        Me.DgridTramos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgridTramos.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DgridTramos.Location = New System.Drawing.Point(0, 0)
        Me.DgridTramos.MainView = Me.grdvTramo
        Me.DgridTramos.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DgridTramos.MenuManager = Me.RibbonControl
        Me.DgridTramos.Name = "DgridTramos"
        Me.DgridTramos.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
        Me.DgridTramos.Size = New System.Drawing.Size(607, 486)
        Me.DgridTramos.TabIndex = 0
        Me.DgridTramos.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdvTramo})
        '
        'grdvTramo
        '
        Me.grdvTramo.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.grdvTramo.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.grdvTramo.Appearance.FocusedRow.Options.UseBackColor = True
        Me.grdvTramo.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grdvTramo.Appearance.SelectedRow.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grdvTramo.Appearance.SelectedRow.BorderColor = System.Drawing.Color.Red
        Me.grdvTramo.Appearance.SelectedRow.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.grdvTramo.Appearance.SelectedRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal
        Me.grdvTramo.Appearance.SelectedRow.Options.UseBackColor = True
        Me.grdvTramo.Appearance.SelectedRow.Options.UseBorderColor = True
        Me.grdvTramo.Appearance.SelectedRow.Options.UseFont = True
        Me.grdvTramo.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colID, Me.colNombre})
        Me.grdvTramo.DetailHeight = 431
        Me.grdvTramo.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus
        Me.grdvTramo.GridControl = Me.DgridTramos
        Me.grdvTramo.Name = "grdvTramo"
        Me.grdvTramo.OptionsBehavior.Editable = False
        Me.grdvTramo.OptionsFind.AlwaysVisible = True
        Me.grdvTramo.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grdvTramo.OptionsSelection.EnableAppearanceHideSelection = False
        Me.grdvTramo.OptionsView.ColumnAutoWidth = False
        Me.grdvTramo.OptionsView.ShowFooter = True
        Me.grdvTramo.OptionsView.ShowGroupPanel = False
        '
        'colID
        '
        Me.colID.Caption = "ID"
        Me.colID.FieldName = "ID"
        Me.colID.MinWidth = 23
        Me.colID.Name = "colID"
        Me.colID.Width = 87
        '
        'colNombre
        '
        Me.colNombre.Caption = "Tramo"
        Me.colNombre.FieldName = "Nombre"
        Me.colNombre.MinWidth = 23
        Me.colNombre.Name = "colNombre"
        Me.colNombre.OptionsColumn.AllowEdit = False
        Me.colNombre.Visible = True
        Me.colNombre.VisibleIndex = 0
        Me.colNombre.Width = 87
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        '
        'colSeleccion1
        '
        Me.colSeleccion1.FieldName = "Seleccion"
        Me.colSeleccion1.Name = "colSeleccion1"
        '
        'colNombre1
        '
        Me.colNombre1.FieldName = "Nombre"
        Me.colNombre1.Name = "colNombre1"
        '
        'BindingSource1
        '
        Me.BindingSource1.DataSource = Me.DsReglaUb
        Me.BindingSource1.Position = 0
        '
        'DsReglaUb
        '
        Me.DsReglaUb.DataSetName = "DsReglaUb"
        Me.DsReglaUb.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DgridUbicaciones
        '
        Me.DgridUbicaciones.Cursor = System.Windows.Forms.Cursors.Default
        Me.DgridUbicaciones.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgridUbicaciones.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        GridLevelNode1.RelationName = "Level1"
        Me.DgridUbicaciones.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.DgridUbicaciones.Location = New System.Drawing.Point(0, 0)
        Me.DgridUbicaciones.MainView = Me.grdvUbicaciones
        Me.DgridUbicaciones.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DgridUbicaciones.Name = "DgridUbicaciones"
        Me.DgridUbicaciones.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit2, Me.RepositoryItemCheckEdit3})
        Me.DgridUbicaciones.Size = New System.Drawing.Size(684, 486)
        Me.DgridUbicaciones.TabIndex = 0
        Me.DgridUbicaciones.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdvUbicaciones})
        '
        'grdvUbicaciones
        '
        Me.grdvUbicaciones.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.grdvUbicaciones.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.grdvUbicaciones.Appearance.FocusedRow.Options.UseBackColor = True
        Me.grdvUbicaciones.Appearance.SelectedRow.BackColor = System.Drawing.Color.SteelBlue
        Me.grdvUbicaciones.Appearance.SelectedRow.Options.UseBackColor = True
        Me.grdvUbicaciones.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn4, Me.colIdBodega, Me.IdProductoBodega})
        Me.grdvUbicaciones.DetailHeight = 431
        Me.grdvUbicaciones.GridControl = Me.DgridUbicaciones
        Me.grdvUbicaciones.Name = "grdvUbicaciones"
        Me.grdvUbicaciones.OptionsFind.AlwaysVisible = True
        Me.grdvUbicaciones.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grdvUbicaciones.OptionsView.ColumnAutoWidth = False
        Me.grdvUbicaciones.OptionsView.ShowFooter = True
        Me.grdvUbicaciones.OptionsView.ShowGroupPanel = False
        '
        'GridColumn4
        '
        Me.GridColumn4.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn4.Caption = "Asignar"
        Me.GridColumn4.ColumnEdit = Me.RepositoryItemCheckEdit2
        Me.GridColumn4.FieldName = "Asignar"
        Me.GridColumn4.MaxWidth = 93
        Me.GridColumn4.MinWidth = 58
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.GridColumn4.OptionsFilter.ImmediateUpdatePopupDateFilterOnCheck = DevExpress.Utils.DefaultBoolean.[True]
        Me.GridColumn4.OptionsFilter.ImmediateUpdatePopupDateFilterOnDateChange = DevExpress.Utils.DefaultBoolean.[True]
        Me.GridColumn4.OptionsFilter.ShowBlanksFilterItems = DevExpress.Utils.DefaultBoolean.[True]
        Me.GridColumn4.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 0
        Me.GridColumn4.Width = 87
        '
        'RepositoryItemCheckEdit2
        '
        Me.RepositoryItemCheckEdit2.AutoHeight = False
        Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
        '
        'colIdBodega
        '
        Me.colIdBodega.FieldName = "Id"
        Me.colIdBodega.MinWidth = 23
        Me.colIdBodega.Name = "colIdBodega"
        Me.colIdBodega.OptionsColumn.AllowEdit = False
        Me.colIdBodega.OptionsColumn.ReadOnly = True
        Me.colIdBodega.Width = 87
        '
        'IdProductoBodega
        '
        Me.IdProductoBodega.AppearanceCell.Options.UseTextOptions = True
        Me.IdProductoBodega.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.IdProductoBodega.Caption = "Ubicacion"
        Me.IdProductoBodega.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.IdProductoBodega.FieldName = "Nombre"
        Me.IdProductoBodega.MinWidth = 23
        Me.IdProductoBodega.Name = "IdProductoBodega"
        Me.IdProductoBodega.OptionsColumn.AllowEdit = False
        Me.IdProductoBodega.OptionsColumn.ReadOnly = True
        Me.IdProductoBodega.Visible = True
        Me.IdProductoBodega.VisibleIndex = 1
        Me.IdProductoBodega.Width = 87
        '
        'RepositoryItemCheckEdit3
        '
        Me.RepositoryItemCheckEdit3.AutoHeight = False
        Me.RepositoryItemCheckEdit3.Name = "RepositoryItemCheckEdit3"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 193)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.DgridTramos)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.DgridUbicaciones)
        Me.SplitContainer1.Size = New System.Drawing.Size(1296, 486)
        Me.SplitContainer1.SplitterDistance = 607
        Me.SplitContainer1.SplitterWidth = 5
        Me.SplitContainer1.TabIndex = 1
        '
        'prg
        '
        Me.prg.Location = New System.Drawing.Point(460, 97)
        Me.prg.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.prg.MenuManager = Me.RibbonControl
        Me.prg.Name = "prg"
        Me.prg.Properties.ShowTitle = True
        Me.prg.Size = New System.Drawing.Size(488, 22)
        Me.prg.TabIndex = 0
        '
        'frmAsociacionReglaUbicacion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1296, 709)
        Me.Controls.Add(Me.prg)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmAsociacionReglaUbicacion"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Asociación de ubicaciones a reglas de ubicación sugerida."
        CType(Me.RibbonControl,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.DgridTramos,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.grdvTramo,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemCheckEdit1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.BindingSource1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.DsReglaUb,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.DgridUbicaciones,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.grdvUbicaciones,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemCheckEdit2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemCheckEdit3,System.ComponentModel.ISupportInitialize).EndInit
        Me.SplitContainer1.Panel1.ResumeLayout(false)
        Me.SplitContainer1.Panel2.ResumeLayout(false)
        CType(Me.SplitContainer1,System.ComponentModel.ISupportInitialize).EndInit
        Me.SplitContainer1.ResumeLayout(false)
        CType(Me.prg.Properties,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents mnuNuevo As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuSalir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents chkActivos As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents cmdImprimir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents lblRegs As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents DgridTramos As DevExpress.XtraGrid.GridControl
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents grdvTramo As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colNombre As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colSeleccion1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colNombre1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BindingSource1 As BindingSource
    Friend WithEvents DsReglaUb As DsReglaUb
    Private WithEvents DgridUbicaciones As DevExpress.XtraGrid.GridControl
    Private WithEvents grdvUbicaciones As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents colIdBodega As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IdProductoBodega As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents mnuMarcarTodos As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents RibbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents prg As DevExpress.XtraEditors.ProgressBarControl
    Friend WithEvents CachedrptRecepcionDetalleInfraccion1 As CachedrptRecepcionDetalleInfraccion
End Class
