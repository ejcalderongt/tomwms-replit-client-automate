Imports DevExpress.XtraGrid.Views.Grid

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmStockList
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If components IsNot Nothing Then
                    components.Dispose()
                End If
                If pObjStock IsNot Nothing Then
                    pObjStock.Dispose()
                    pObjStock = Nothing
                End If
                If DTStock IsNot Nothing Then
                    DTStock.Dispose()
                    DTStock = Nothing
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmStockList))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuSalir = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem4 = New DevExpress.XtraBars.BarButtonItem()
        Me.chkActivos = New DevExpress.XtraBars.BarCheckItem()
        Me.lblReg = New DevExpress.XtraBars.BarStaticItem()
        Me.cmdImprimir = New DevExpress.XtraBars.BarButtonItem()
        Me.lblRegistros = New DevExpress.XtraBars.BarStaticItem()
        Me.lblProgress = New DevExpress.XtraBars.BarStaticItem()
        Me.chkSeleccionMultiple = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.RibbonPage2 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar1 = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.DGrid = New DevExpress.XtraGrid.GridControl()
        Me.grdvStock = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.RepositoryItemCheckEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit4 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.DsetSeleccionStockBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsetSeleccionStock = New TOMWMS.DsetSeleccionStock()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.prg = New DevExpress.XtraEditors.ProgressBarControl()
        Me.mnuTomarSeleccionados = New DevExpress.XtraBars.BarButtonItem()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdvStock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsetSeleccionStockBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsetSeleccionStock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.prg.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuActualizar, Me.mnuSalir, Me.BarButtonItem4, Me.chkActivos, Me.lblReg, Me.cmdImprimir, Me.lblRegistros, Me.lblProgress, Me.chkSeleccionMultiple, Me.mnuTomarSeleccionados})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 20
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage2})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(957, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar1
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
        'BarButtonItem4
        '
        Me.BarButtonItem4.Caption = "BarButtonItem4"
        Me.BarButtonItem4.Id = 4
        Me.BarButtonItem4.Name = "BarButtonItem4"
        '
        'chkActivos
        '
        Me.chkActivos.BindableChecked = True
        Me.chkActivos.Caption = "Activos"
        Me.chkActivos.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText
        Me.chkActivos.Checked = True
        Me.chkActivos.Id = 5
        Me.chkActivos.Name = "chkActivos"
        '
        'lblReg
        '
        Me.lblReg.Caption = "Registros: 0"
        Me.lblReg.Id = 8
        Me.lblReg.Name = "lblReg"
        '
        'cmdImprimir
        '
        Me.cmdImprimir.Caption = "Imprimir"
        Me.cmdImprimir.Id = 9
        Me.cmdImprimir.ImageOptions.SvgImage = CType(resources.GetObject("cmdImprimir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdImprimir.Name = "cmdImprimir"
        '
        'lblRegistros
        '
        Me.lblRegistros.Caption = "Registros: 0"
        Me.lblRegistros.Id = 14
        Me.lblRegistros.Name = "lblRegistros"
        '
        'lblProgress
        '
        Me.lblProgress.Id = 16
        Me.lblProgress.ImageOptions.Image = CType(resources.GetObject("lblProgress.ImageOptions.Image"), System.Drawing.Image)
        Me.lblProgress.ImageOptions.LargeImage = CType(resources.GetObject("lblProgress.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'chkSeleccionMultiple
        '
        Me.chkSeleccionMultiple.Caption = "Selección Múltiple"
        Me.chkSeleccionMultiple.Id = 18
        Me.chkSeleccionMultiple.Name = "chkSeleccionMultiple"
        '
        'RibbonPage2
        '
        Me.RibbonPage2.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1, Me.RibbonPageGroup2})
        Me.RibbonPage2.Name = "RibbonPage2"
        Me.RibbonPage2.Text = "Opciones de Lista"
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
        Me.RibbonPageGroup2.ItemLinks.Add(Me.chkSeleccionMultiple)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.mnuTomarSeleccionados)
        Me.RibbonPageGroup2.Name = "RibbonPageGroup2"
        '
        'RibbonStatusBar1
        '
        Me.RibbonStatusBar1.ItemLinks.Add(Me.lblProgress)
        Me.RibbonStatusBar1.ItemLinks.Add(Me.lblRegistros)
        Me.RibbonStatusBar1.Location = New System.Drawing.Point(0, 667)
        Me.RibbonStatusBar1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar1.Name = "RibbonStatusBar1"
        Me.RibbonStatusBar1.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar1.Size = New System.Drawing.Size(957, 30)
        '
        'DGrid
        '
        Me.DGrid.Cursor = System.Windows.Forms.Cursors.Default
        Me.DGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGrid.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DGrid.Location = New System.Drawing.Point(0, 271)
        Me.DGrid.MainView = Me.grdvStock
        Me.DGrid.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DGrid.Name = "DGrid"
        Me.DGrid.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit3, Me.RepositoryItemCheckEdit4})
        Me.DGrid.Size = New System.Drawing.Size(957, 396)
        Me.DGrid.TabIndex = 0
        Me.DGrid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdvStock})
        '
        'grdvStock
        '
        Me.grdvStock.DetailHeight = 431
        Me.grdvStock.GridControl = Me.DGrid
        Me.grdvStock.Name = "grdvStock"
        Me.grdvStock.OptionsBehavior.Editable = False
        Me.grdvStock.OptionsFind.AlwaysVisible = True
        Me.grdvStock.OptionsView.ColumnAutoWidth = False
        Me.grdvStock.OptionsView.ShowAutoFilterRow = True
        Me.grdvStock.OptionsView.ShowFooter = True
        '
        'RepositoryItemCheckEdit3
        '
        Me.RepositoryItemCheckEdit3.AutoHeight = False
        Me.RepositoryItemCheckEdit3.Name = "RepositoryItemCheckEdit3"
        '
        'RepositoryItemCheckEdit4
        '
        Me.RepositoryItemCheckEdit4.AutoHeight = False
        Me.RepositoryItemCheckEdit4.Name = "RepositoryItemCheckEdit4"
        '
        'DsetSeleccionStockBindingSource
        '
        Me.DsetSeleccionStockBindingSource.DataSource = Me.DsetSeleccionStock
        Me.DsetSeleccionStockBindingSource.Position = 0
        '
        'DsetSeleccionStock
        '
        Me.DsetSeleccionStock.DataSetName = "DsetSeleccionStock"
        Me.DsetSeleccionStock.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.prg)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl1.Location = New System.Drawing.Point(0, 193)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(957, 78)
        Me.GroupControl1.TabIndex = 3
        '
        'prg
        '
        Me.prg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.prg.Location = New System.Drawing.Point(2, 28)
        Me.prg.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.prg.MenuManager = Me.RibbonControl
        Me.prg.Name = "prg"
        Me.prg.Size = New System.Drawing.Size(953, 48)
        Me.prg.TabIndex = 0
        Me.prg.Visible = False
        '
        'mnuTomarSeleccionados
        '
        Me.mnuTomarSeleccionados.Caption = "Aplicar Selección"
        Me.mnuTomarSeleccionados.Id = 19
        Me.mnuTomarSeleccionados.ImageOptions.SvgImage = CType(resources.GetObject("BarButtonItem1.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuTomarSeleccionados.Name = "mnuTomarSeleccionados"
        '
        'FrmStockList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(957, 697)
        Me.Controls.Add(Me.DGrid)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.RibbonStatusBar1)
        Me.Controls.Add(Me.RibbonControl)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "FrmStockList"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar1
        Me.Text = "Listado de existencias"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdvStock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsetSeleccionStockBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsetSeleccionStock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.prg.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents mnuActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuSalir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem4 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents chkActivos As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents lblReg As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents cmdImprimir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPage2 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Private WithEvents DGrid As DevExpress.XtraGrid.GridControl
    Private WithEvents grdvStock As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemCheckEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit4 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents DsetSeleccionStockBindingSource As BindingSource
    Friend WithEvents DsetSeleccionStock As DsetSeleccionStock
    Friend WithEvents lblRegistros As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents RibbonStatusBar1 As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents lblProgress As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents prg As DevExpress.XtraEditors.ProgressBarControl
    Friend WithEvents chkSeleccionMultiple As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents RibbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents mnuTomarSeleccionados As DevExpress.XtraBars.BarButtonItem
End Class
