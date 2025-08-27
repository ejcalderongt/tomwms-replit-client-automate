<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmUbicacion_Etiqueta
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUbicacion_Etiqueta))
        Dim SuperToolTip1 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
        Dim ToolTipTitleItem1 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
        Dim ReduceOperation1 As DevExpress.XtraBars.Ribbon.ReduceOperation = New DevExpress.XtraBars.Ribbon.ReduceOperation()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.GrpTramoUbic = New DevExpress.XtraEditors.GroupControl()
        Me.grdUbicacion = New DevExpress.XtraGrid.GridControl()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.GridViewUbi = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colSeleccionar = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.colIdUbicacion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDescripcion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCodigo_Barra = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cmbEtiquetaU = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbTramoUbic = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbArea = New DevExpress.XtraEditors.LookUpEdit()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuNuevo = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuSalir = New DevExpress.XtraBars.BarButtonItem()
        Me.chkActivos = New DevExpress.XtraBars.BarCheckItem()
        Me.cmdImprimir = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuMarcarTodos = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuMarcarUbicacionPicking = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup3 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RepositoryItemComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.lblArea = New System.Windows.Forms.Label()
        Me.pgbUbic = New DevExpress.XtraEditors.ProgressBarControl()
        Me.colUbicacionPicking = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.GrpTramoUbic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpTramoUbic.SuspendLayout()
        CType(Me.grdUbicacion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewUbi, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbArea.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pgbUbic.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GridView1
        '
        Me.GridView1.Name = "GridView1"
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.GrpTramoUbic)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl1.Location = New System.Drawing.Point(0, 193)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1164, 631)
        Me.PanelControl1.TabIndex = 1
        '
        'GrpTramoUbic
        '
        Me.GrpTramoUbic.Controls.Add(Me.grdUbicacion)
        Me.GrpTramoUbic.Controls.Add(Me.cmbEtiquetaU)
        Me.GrpTramoUbic.Controls.Add(Me.Label2)
        Me.GrpTramoUbic.Controls.Add(Me.cmbTramoUbic)
        Me.GrpTramoUbic.Controls.Add(Me.Label1)
        Me.GrpTramoUbic.Controls.Add(Me.cmbArea)
        Me.GrpTramoUbic.Controls.Add(Me.lblArea)
        Me.GrpTramoUbic.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpTramoUbic.Location = New System.Drawing.Point(2, 2)
        Me.GrpTramoUbic.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GrpTramoUbic.Name = "GrpTramoUbic"
        Me.GrpTramoUbic.Size = New System.Drawing.Size(1160, 627)
        Me.GrpTramoUbic.TabIndex = 0
        '
        'grdUbicacion
        '
        Me.grdUbicacion.DataSource = Me.BindingSource1
        Me.grdUbicacion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdUbicacion.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdUbicacion.Location = New System.Drawing.Point(2, 198)
        Me.grdUbicacion.MainView = Me.GridViewUbi
        Me.grdUbicacion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdUbicacion.Name = "grdUbicacion"
        Me.grdUbicacion.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit2})
        Me.grdUbicacion.Size = New System.Drawing.Size(1156, 427)
        Me.grdUbicacion.TabIndex = 1
        Me.grdUbicacion.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewUbi, Me.GridView2})
        '
        'BindingSource1
        '
        '        Me.BindingSource1.DataSource = GetType(TOMWMS.clsBeBodega_Ubicacion_Seleccion)
        '
        'GridViewUbi
        '
        Me.GridViewUbi.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colSeleccionar, Me.colIdUbicacion, Me.colDescripcion, Me.colCodigo_Barra, Me.colUbicacionPicking})
        Me.GridViewUbi.DetailHeight = 431
        Me.GridViewUbi.GridControl = Me.grdUbicacion
        Me.GridViewUbi.Name = "GridViewUbi"
        Me.GridViewUbi.OptionsView.ShowFooter = True
        Me.GridViewUbi.OptionsView.ShowGroupPanel = False
        '
        'colSeleccionar
        '
        Me.colSeleccionar.ColumnEdit = Me.RepositoryItemCheckEdit2
        Me.colSeleccionar.FieldName = "Seleccionar"
        Me.colSeleccionar.MinWidth = 23
        Me.colSeleccionar.Name = "colSeleccionar"
        Me.colSeleccionar.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "Seleccionar", "{0}")})
        Me.colSeleccionar.Visible = True
        Me.colSeleccionar.VisibleIndex = 0
        Me.colSeleccionar.Width = 87
        '
        'RepositoryItemCheckEdit2
        '
        Me.RepositoryItemCheckEdit2.AutoHeight = False
        Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
        '
        'colIdUbicacion
        '
        Me.colIdUbicacion.Caption = "Código"
        Me.colIdUbicacion.FieldName = "IdUbicacion"
        Me.colIdUbicacion.MinWidth = 23
        Me.colIdUbicacion.Name = "colIdUbicacion"
        Me.colIdUbicacion.OptionsColumn.AllowEdit = False
        Me.colIdUbicacion.Visible = True
        Me.colIdUbicacion.VisibleIndex = 1
        Me.colIdUbicacion.Width = 87
        '
        'colDescripcion
        '
        Me.colDescripcion.FieldName = "Descripcion"
        Me.colDescripcion.MinWidth = 23
        Me.colDescripcion.Name = "colDescripcion"
        Me.colDescripcion.OptionsColumn.AllowEdit = False
        Me.colDescripcion.Visible = True
        Me.colDescripcion.VisibleIndex = 2
        Me.colDescripcion.Width = 87
        '
        'colCodigo_Barra
        '
        Me.colCodigo_Barra.FieldName = "Codigo_Barra"
        Me.colCodigo_Barra.MinWidth = 23
        Me.colCodigo_Barra.Name = "colCodigo_Barra"
        Me.colCodigo_Barra.OptionsColumn.AllowEdit = False
        Me.colCodigo_Barra.Visible = True
        Me.colCodigo_Barra.VisibleIndex = 3
        Me.colCodigo_Barra.Width = 87
        '
        'GridView2
        '
        Me.GridView2.DetailHeight = 431
        Me.GridView2.GridControl = Me.grdUbicacion
        Me.GridView2.Name = "GridView2"
        '
        'cmbEtiquetaU
        '
        Me.cmbEtiquetaU.Dock = System.Windows.Forms.DockStyle.Top
        Me.cmbEtiquetaU.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbEtiquetaU.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbEtiquetaU.FormattingEnabled = True
        Me.cmbEtiquetaU.Location = New System.Drawing.Point(2, 169)
        Me.cmbEtiquetaU.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbEtiquetaU.Name = "cmbEtiquetaU"
        Me.cmbEtiquetaU.Size = New System.Drawing.Size(1156, 29)
        Me.cmbEtiquetaU.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(2, 141)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1156, 28)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Etiqueta:"
        '
        'cmbTramoUbic
        '
        Me.cmbTramoUbic.Dock = System.Windows.Forms.DockStyle.Top
        Me.cmbTramoUbic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTramoUbic.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbTramoUbic.FormattingEnabled = True
        Me.cmbTramoUbic.Location = New System.Drawing.Point(2, 112)
        Me.cmbTramoUbic.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbTramoUbic.Name = "cmbTramoUbic"
        Me.cmbTramoUbic.Size = New System.Drawing.Size(1156, 29)
        Me.cmbTramoUbic.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(2, 84)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1156, 28)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Tramo:"
        '
        'cmbArea
        '
        Me.cmbArea.Dock = System.Windows.Forms.DockStyle.Top
        Me.cmbArea.Location = New System.Drawing.Point(2, 56)
        Me.cmbArea.MenuManager = Me.RibbonControl
        Me.cmbArea.Name = "cmbArea"
        Me.cmbArea.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbArea.Properties.Appearance.Options.UseFont = True
        Me.cmbArea.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbArea.Size = New System.Drawing.Size(1156, 28)
        Me.cmbArea.TabIndex = 6
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.RibbonControl.SearchEditItem, Me.mnuNuevo, Me.mnuActualizar, Me.mnuSalir, Me.chkActivos, Me.cmdImprimir, Me.mnuMarcarTodos, Me.BarButtonItem1, Me.mnuMarcarUbicacionPicking})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 8
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemComboBox1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1164, 193)
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
        Me.cmdImprimir.Caption = "Imprimir" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Etiquetas"
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
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "Invertir"
        Me.BarButtonItem1.Id = 2
        Me.BarButtonItem1.ImageOptions.Image = CType(resources.GetObject("BarButtonItem1.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItem1.Name = "BarButtonItem1"
        ToolTipTitleItem1.Appearance.Image = CType(resources.GetObject("resource.Image"), System.Drawing.Image)
        ToolTipTitleItem1.Appearance.Options.UseImage = True
        ToolTipTitleItem1.ImageOptions.Image = CType(resources.GetObject("resource.Image1"), System.Drawing.Image)
        ToolTipTitleItem1.Text = "Invetir"
        SuperToolTip1.Items.Add(ToolTipTitleItem1)
        Me.BarButtonItem1.SuperTip = SuperToolTip1
        '
        'mnuMarcarUbicacionPicking
        '
        Me.mnuMarcarUbicacionPicking.Caption = "Marcar ubicación picking"
        Me.mnuMarcarUbicacionPicking.Id = 7
        Me.mnuMarcarUbicacionPicking.ImageOptions.SvgImage = CType(resources.GetObject("mnuMarcarUbicacionPicking.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuMarcarUbicacionPicking.Name = "mnuMarcarUbicacionPicking"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1, Me.RibbonPageGroup2, Me.RibbonPageGroup3})
        Me.RibbonPage1.Name = "RibbonPage1"
        ReduceOperation1.Behavior = DevExpress.XtraBars.Ribbon.ReduceOperationBehavior.[Single]
        ReduceOperation1.GroupName = Nothing
        ReduceOperation1.ItemLinkIndex = 0
        ReduceOperation1.ItemLinksCount = 0
        ReduceOperation1.Operation = DevExpress.XtraBars.Ribbon.ReduceOperationType.LargeButtons
        Me.RibbonPage1.ReduceOperations.Add(ReduceOperation1)
        Me.RibbonPage1.Text = "Opciones de Etiquetas"
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
        'RibbonPageGroup3
        '
        Me.RibbonPageGroup3.ItemLinks.Add(Me.mnuMarcarUbicacionPicking)
        Me.RibbonPageGroup3.Name = "RibbonPageGroup3"
        '
        'RepositoryItemComboBox1
        '
        Me.RepositoryItemComboBox1.AutoHeight = False
        Me.RepositoryItemComboBox1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemComboBox1.Name = "RepositoryItemComboBox1"
        '
        'lblArea
        '
        Me.lblArea.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblArea.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblArea.Location = New System.Drawing.Point(2, 28)
        Me.lblArea.Name = "lblArea"
        Me.lblArea.Size = New System.Drawing.Size(1156, 28)
        Me.lblArea.TabIndex = 5
        Me.lblArea.Text = "Área:"
        '
        'pgbUbic
        '
        Me.pgbUbic.Location = New System.Drawing.Point(669, 102)
        Me.pgbUbic.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.pgbUbic.Name = "pgbUbic"
        Me.pgbUbic.Properties.ShowTitle = True
        Me.pgbUbic.Size = New System.Drawing.Size(345, 28)
        Me.pgbUbic.TabIndex = 0
        Me.pgbUbic.Visible = False
        '
        'colUbicacionPicking
        '
        Me.colUbicacionPicking.Caption = "Ubicacion_Picking"
        Me.colUbicacionPicking.FieldName = "Ubicacion_Picking"
        Me.colUbicacionPicking.MinWidth = 25
        Me.colUbicacionPicking.Name = "colUbicacionPicking"
        Me.colUbicacionPicking.Visible = True
        Me.colUbicacionPicking.VisibleIndex = 4
        Me.colUbicacionPicking.Width = 94
        '
        'frmUbicacion_Etiqueta
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1164, 824)
        Me.Controls.Add(Me.pgbUbic)
        Me.Controls.Add(Me.PanelControl1)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmUbicacion_Etiqueta"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Impresión de Etiquetas"
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.GrpTramoUbic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpTramoUbic.ResumeLayout(False)
        CType(Me.grdUbicacion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewUbi, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbArea.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pgbUbic.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout

End Sub
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents cmbEtiquetaU As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents BindingSource1 As BindingSource
    Friend WithEvents pgbUbic As DevExpress.XtraEditors.ProgressBarControl
    Friend WithEvents grdUbicacion As DevExpress.XtraGrid.GridControl
    Private WithEvents GridViewUbi As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colSeleccionar As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents colIdUbicacion As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDescripcion As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCodigo_Barra As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GrpTramoUbic As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cmbTramoUbic As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents mnuNuevo As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuSalir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents chkActivos As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents cmdImprimir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuMarcarTodos As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RepositoryItemComboBox1 As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents lblArea As Label
    Friend WithEvents cmbArea As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents mnuMarcarUbicacionPicking As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup3 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents colUbicacionPicking As DevExpress.XtraGrid.Columns.GridColumn
End Class
