<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmLicenciasPorUbicacion
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLicenciasPorUbicacion))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImprimir = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdSalir = New DevExpress.XtraBars.BarButtonItem()
        Me.lblRegistros = New DevExpress.XtraBars.BarSubItem()
        Me.cmdExcel = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminarLayoutGrid = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuGuardarLayoutGrid = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuImpresiónmnu = New DevExpress.XtraBars.BarSubItem()
        Me.mnuImprimirLicenciasPorUbicaicon = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuLicenciasInconsistentes = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.XtraTabControl1 = New DevExpress.XtraTab.XtraTabControl()
        Me.xtpLicenciasPorUbicacion = New DevExpress.XtraTab.XtraTabPage()
        Me.grdStock = New DevExpress.XtraGrid.GridControl()
        Me.grdvStock = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.prg = New DevExpress.XtraEditors.ProgressBarControl()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbPropietarioBodega = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbBodega = New DevExpress.XtraEditors.LookUpEdit()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.xtpLicenciasIncosistentes = New DevExpress.XtraTab.XtraTabPage()
        Me.grdLicenciasInconsistentes = New DevExpress.XtraGrid.GridControl()
        Me.grdvLicenciasInconsistentes = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabControl1.SuspendLayout()
        Me.xtpLicenciasPorUbicacion.SuspendLayout()
        CType(Me.grdStock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdvStock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.prg.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.cmbPropietarioBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtpLicenciasIncosistentes.SuspendLayout()
        CType(Me.grdLicenciasInconsistentes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdvLicenciasInconsistentes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuActualizar, Me.cmdImprimir, Me.cmdSalir, Me.lblRegistros, Me.cmdExcel, Me.mnuEliminarLayoutGrid, Me.mnuGuardarLayoutGrid, Me.mnuImpresiónmnu, Me.mnuImprimirLicenciasPorUbicaicon, Me.mnuLicenciasInconsistentes})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 13
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1340, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'mnuActualizar
        '
        Me.mnuActualizar.Caption = "Actualizar"
        Me.mnuActualizar.Id = 1
        Me.mnuActualizar.ImageOptions.SvgImage = CType(resources.GetObject("mnuActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuActualizar.Name = "mnuActualizar"
        '
        'cmdImprimir
        '
        Me.cmdImprimir.Caption = "Imprimir"
        Me.cmdImprimir.Id = 2
        Me.cmdImprimir.ImageOptions.SvgImage = CType(resources.GetObject("cmdImprimir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdImprimir.Name = "cmdImprimir"
        '
        'cmdSalir
        '
        Me.cmdSalir.Caption = "Salir"
        Me.cmdSalir.Id = 3
        Me.cmdSalir.ImageOptions.SvgImage = CType(resources.GetObject("cmdSalir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdSalir.Name = "cmdSalir"
        '
        'lblRegistros
        '
        Me.lblRegistros.Caption = "Registros: 0"
        Me.lblRegistros.Id = 4
        Me.lblRegistros.Name = "lblRegistros"
        '
        'cmdExcel
        '
        Me.cmdExcel.Caption = "Exportar a Excel "
        Me.cmdExcel.Id = 7
        Me.cmdExcel.ImageOptions.SvgImage = CType(resources.GetObject("cmdExcel.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdExcel.Name = "cmdExcel"
        '
        'mnuEliminarLayoutGrid
        '
        Me.mnuEliminarLayoutGrid.Caption = "Eliminar diseño grid"
        Me.mnuEliminarLayoutGrid.Id = 8
        Me.mnuEliminarLayoutGrid.ImageOptions.SvgImage = CType(resources.GetObject("mnuEliminarLayoutGrid.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEliminarLayoutGrid.Name = "mnuEliminarLayoutGrid"
        '
        'mnuGuardarLayoutGrid
        '
        Me.mnuGuardarLayoutGrid.Caption = "Guardar diseño de grid"
        Me.mnuGuardarLayoutGrid.Id = 9
        Me.mnuGuardarLayoutGrid.ImageOptions.SvgImage = CType(resources.GetObject("mnuGuardarLayoutGrid.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuGuardarLayoutGrid.Name = "mnuGuardarLayoutGrid"
        '
        'mnuImpresiónmnu
        '
        Me.mnuImpresiónmnu.Caption = "Impresión"
        Me.mnuImpresiónmnu.Id = 10
        Me.mnuImpresiónmnu.ImageOptions.SvgImage = CType(resources.GetObject("mnuImpresiónmnu.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuImpresiónmnu.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuImprimirLicenciasPorUbicaicon), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuLicenciasInconsistentes)})
        Me.mnuImpresiónmnu.Name = "mnuImpresiónmnu"
        '
        'mnuImprimirLicenciasPorUbicaicon
        '
        Me.mnuImprimirLicenciasPorUbicaicon.Caption = "Licencias por ubicaciòn"
        Me.mnuImprimirLicenciasPorUbicaicon.Id = 11
        Me.mnuImprimirLicenciasPorUbicaicon.Name = "mnuImprimirLicenciasPorUbicaicon"
        '
        'mnuLicenciasInconsistentes
        '
        Me.mnuLicenciasInconsistentes.Caption = "Licencias Inconsistentes"
        Me.mnuLicenciasInconsistentes.Id = 12
        Me.mnuLicenciasInconsistentes.Name = "mnuLicenciasInconsistentes"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Licencias por Ubicación"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuImpresiónmnu)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdExcel)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardarLayoutGrid)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEliminarLayoutGrid)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdSalir)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 736)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1340, 30)
        '
        'XtraTabControl1
        '
        Me.XtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XtraTabControl1.Location = New System.Drawing.Point(0, 193)
        Me.XtraTabControl1.Name = "XtraTabControl1"
        Me.XtraTabControl1.SelectedTabPage = Me.xtpLicenciasPorUbicacion
        Me.XtraTabControl1.Size = New System.Drawing.Size(1340, 543)
        Me.XtraTabControl1.TabIndex = 5
        Me.XtraTabControl1.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtpLicenciasPorUbicacion, Me.xtpLicenciasIncosistentes})
        '
        'xtpLicenciasPorUbicacion
        '
        Me.xtpLicenciasPorUbicacion.Controls.Add(Me.grdStock)
        Me.xtpLicenciasPorUbicacion.Controls.Add(Me.prg)
        Me.xtpLicenciasPorUbicacion.Controls.Add(Me.PanelControl1)
        Me.xtpLicenciasPorUbicacion.Name = "xtpLicenciasPorUbicacion"
        Me.xtpLicenciasPorUbicacion.Size = New System.Drawing.Size(1338, 513)
        Me.xtpLicenciasPorUbicacion.Text = "Licencias por ubicación"
        '
        'grdStock
        '
        Me.grdStock.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdStock.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdStock.Location = New System.Drawing.Point(306, 48)
        Me.grdStock.MainView = Me.grdvStock
        Me.grdStock.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdStock.MenuManager = Me.RibbonControl
        Me.grdStock.Name = "grdStock"
        Me.grdStock.Size = New System.Drawing.Size(1032, 465)
        Me.grdStock.TabIndex = 5
        Me.grdStock.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdvStock})
        '
        'grdvStock
        '
        Me.grdvStock.DetailHeight = 431
        Me.grdvStock.GridControl = Me.grdStock
        Me.grdvStock.Name = "grdvStock"
        Me.grdvStock.OptionsBehavior.ReadOnly = True
        Me.grdvStock.OptionsFind.AlwaysVisible = True
        Me.grdvStock.OptionsView.ColumnAutoWidth = False
        Me.grdvStock.OptionsView.ShowAutoFilterRow = True
        '
        'prg
        '
        Me.prg.Dock = System.Windows.Forms.DockStyle.Top
        Me.prg.Location = New System.Drawing.Point(306, 0)
        Me.prg.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.prg.MenuManager = Me.RibbonControl
        Me.prg.Name = "prg"
        Me.prg.Size = New System.Drawing.Size(1032, 48)
        Me.prg.TabIndex = 4
        Me.prg.Visible = False
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.Label2)
        Me.PanelControl1.Controls.Add(Me.cmbPropietarioBodega)
        Me.PanelControl1.Controls.Add(Me.cmbBodega)
        Me.PanelControl1.Controls.Add(Me.Label1)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(306, 513)
        Me.PanelControl1.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 102)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 16)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Propietario:"
        '
        'cmbPropietarioBodega
        '
        Me.cmbPropietarioBodega.Location = New System.Drawing.Point(9, 122)
        Me.cmbPropietarioBodega.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbPropietarioBodega.MenuManager = Me.RibbonControl
        Me.cmbPropietarioBodega.Name = "cmbPropietarioBodega"
        Me.cmbPropietarioBodega.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPropietarioBodega.Properties.NullText = ""
        Me.cmbPropietarioBodega.Size = New System.Drawing.Size(270, 22)
        Me.cmbPropietarioBodega.TabIndex = 3
        '
        'cmbBodega
        '
        Me.cmbBodega.Location = New System.Drawing.Point(6, 53)
        Me.cmbBodega.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbBodega.MenuManager = Me.RibbonControl
        Me.cmbBodega.Name = "cmbBodega"
        Me.cmbBodega.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodega.Properties.NullText = ""
        Me.cmbBodega.Size = New System.Drawing.Size(274, 22)
        Me.cmbBodega.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Bodega:"
        '
        'xtpLicenciasIncosistentes
        '
        Me.xtpLicenciasIncosistentes.Controls.Add(Me.grdLicenciasInconsistentes)
        Me.xtpLicenciasIncosistentes.Name = "xtpLicenciasIncosistentes"
        Me.xtpLicenciasIncosistentes.Size = New System.Drawing.Size(1338, 513)
        Me.xtpLicenciasIncosistentes.Text = "Licencias inconsistentes"
        '
        'grdLicenciasInconsistentes
        '
        Me.grdLicenciasInconsistentes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdLicenciasInconsistentes.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdLicenciasInconsistentes.Location = New System.Drawing.Point(0, 0)
        Me.grdLicenciasInconsistentes.MainView = Me.grdvLicenciasInconsistentes
        Me.grdLicenciasInconsistentes.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdLicenciasInconsistentes.MenuManager = Me.RibbonControl
        Me.grdLicenciasInconsistentes.Name = "grdLicenciasInconsistentes"
        Me.grdLicenciasInconsistentes.Size = New System.Drawing.Size(1338, 513)
        Me.grdLicenciasInconsistentes.TabIndex = 6
        Me.grdLicenciasInconsistentes.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdvLicenciasInconsistentes})
        '
        'grdvLicenciasInconsistentes
        '
        Me.grdvLicenciasInconsistentes.DetailHeight = 431
        Me.grdvLicenciasInconsistentes.GridControl = Me.grdLicenciasInconsistentes
        Me.grdvLicenciasInconsistentes.Name = "grdvLicenciasInconsistentes"
        Me.grdvLicenciasInconsistentes.OptionsBehavior.ReadOnly = True
        Me.grdvLicenciasInconsistentes.OptionsFind.AlwaysVisible = True
        Me.grdvLicenciasInconsistentes.OptionsView.ColumnAutoWidth = False
        Me.grdvLicenciasInconsistentes.OptionsView.ShowAutoFilterRow = True
        '
        'frmLicenciasPorUbicacion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1340, 766)
        Me.Controls.Add(Me.XtraTabControl1)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmLicenciasPorUbicacion"
        Me.Ribbon = Me.RibbonControl
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Licencias por Ubicación"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabControl1.ResumeLayout(False)
        Me.xtpLicenciasPorUbicacion.ResumeLayout(False)
        CType(Me.grdStock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdvStock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.prg.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.cmbPropietarioBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtpLicenciasIncosistentes.ResumeLayout(False)
        CType(Me.grdLicenciasInconsistentes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdvLicenciasInconsistentes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents mnuActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdImprimir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdSalir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lblRegistros As DevExpress.XtraBars.BarSubItem
    Friend WithEvents cmdExcel As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEliminarLayoutGrid As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuGuardarLayoutGrid As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents XtraTabControl1 As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtpLicenciasPorUbicacion As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents grdStock As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdvStock As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents prg As DevExpress.XtraEditors.ProgressBarControl
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents Label2 As Label
    Friend WithEvents cmbPropietarioBodega As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbBodega As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label1 As Label
    Friend WithEvents xtpLicenciasIncosistentes As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents grdLicenciasInconsistentes As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdvLicenciasInconsistentes As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents mnuImpresiónmnu As DevExpress.XtraBars.BarSubItem
    Friend WithEvents mnuImprimirLicenciasPorUbicaicon As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuLicenciasInconsistentes As DevExpress.XtraBars.BarButtonItem
End Class
