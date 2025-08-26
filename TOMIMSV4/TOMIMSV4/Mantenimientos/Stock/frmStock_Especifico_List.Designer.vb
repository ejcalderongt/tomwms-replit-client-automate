<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStock_Especifico_List
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmStock_Especifico_List))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem3 = New DevExpress.XtraBars.BarButtonItem()
        Me.lblProgress = New DevExpress.XtraBars.BarStaticItem()
        Me.lblRegistros = New DevExpress.XtraBars.BarStaticItem()
        Me.mnuGuardarLayoutGrid = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminarLayoutGrid = New DevExpress.XtraBars.BarButtonItem()
        Me.chkFiltroPolizaActivo = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.chkSeleccionMultiple = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.mnuTomarSeleccionados = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuExportarExcel = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuMostrarMensajePorCadaReserva = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.grdStock = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.prg = New DevExpress.XtraEditors.ProgressBarControl()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.lblSeleccion = New DevExpress.XtraEditors.LabelControl()
        Me.lblPoliza = New System.Windows.Forms.Label()
        Me.txtNomPoliza = New DevExpress.XtraEditors.TextEdit()
        Me.txtNoPoliza = New DevExpress.XtraEditors.TextEdit()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.lblDispsin = New DevExpress.XtraEditors.LabelControl()
        Me.lblInfo = New DevExpress.XtraEditors.LabelControl()
        Me.lblDispcon = New DevExpress.XtraEditors.LabelControl()
        Me.lblDispNo = New DevExpress.XtraEditors.LabelControl()
        Me.txtNombreProducto = New DevExpress.XtraEditors.TextEdit()
        Me.txtIdProducto = New DevExpress.XtraEditors.TextEdit()
        Me.lblProducto = New System.Windows.Forms.LinkLabel()
        Me.BarButtonItem4 = New DevExpress.XtraBars.BarButtonItem()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdStock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.prg.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.txtNomPoliza.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoPoliza.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.txtNombreProducto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdProducto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.RibbonControl.SearchEditItem, Me.BarButtonItem1, Me.BarButtonItem2, Me.BarButtonItem3, Me.lblProgress, Me.lblRegistros, Me.mnuGuardarLayoutGrid, Me.mnuEliminarLayoutGrid, Me.chkFiltroPolizaActivo, Me.chkSeleccionMultiple, Me.mnuTomarSeleccionados, Me.mnuExportarExcel, Me.mnuMostrarMensajePorCadaReserva, Me.BarButtonItem4})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 15
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1339, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "Actualizar"
        Me.BarButtonItem1.Id = 1
        Me.BarButtonItem1.ImageOptions.SvgImage = CType(resources.GetObject("BarButtonItem1.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'BarButtonItem2
        '
        Me.BarButtonItem2.Caption = "Salir"
        Me.BarButtonItem2.Id = 2
        Me.BarButtonItem2.ImageOptions.SvgImage = CType(resources.GetObject("BarButtonItem2.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.BarButtonItem2.Name = "BarButtonItem2"
        '
        'BarButtonItem3
        '
        Me.BarButtonItem3.Caption = "Imprimir"
        Me.BarButtonItem3.Id = 3
        Me.BarButtonItem3.ImageOptions.SvgImage = CType(resources.GetObject("BarButtonItem3.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.BarButtonItem3.Name = "BarButtonItem3"
        '
        'lblProgress
        '
        Me.lblProgress.Id = 4
        Me.lblProgress.ImageOptions.Image = CType(resources.GetObject("lblProgress.ImageOptions.Image"), System.Drawing.Image)
        Me.lblProgress.ImageOptions.LargeImage = CType(resources.GetObject("lblProgress.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'lblRegistros
        '
        Me.lblRegistros.Caption = "Registros: 0"
        Me.lblRegistros.Id = 5
        Me.lblRegistros.Name = "lblRegistros"
        '
        'mnuGuardarLayoutGrid
        '
        Me.mnuGuardarLayoutGrid.Caption = "Guardar diseño de grid"
        Me.mnuGuardarLayoutGrid.Id = 6
        Me.mnuGuardarLayoutGrid.ImageOptions.SvgImage = CType(resources.GetObject("mnuGuardarLayoutGrid.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuGuardarLayoutGrid.Name = "mnuGuardarLayoutGrid"
        '
        'mnuEliminarLayoutGrid
        '
        Me.mnuEliminarLayoutGrid.Caption = "Eliminar diseño de grid"
        Me.mnuEliminarLayoutGrid.Id = 7
        Me.mnuEliminarLayoutGrid.ImageOptions.SvgImage = CType(resources.GetObject("mnuEliminarLayoutGrid.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEliminarLayoutGrid.Name = "mnuEliminarLayoutGrid"
        '
        'chkFiltroPolizaActivo
        '
        Me.chkFiltroPolizaActivo.Caption = "Filtro Poliza/T.O Activo"
        Me.chkFiltroPolizaActivo.Id = 8
        Me.chkFiltroPolizaActivo.Name = "chkFiltroPolizaActivo"
        '
        'chkSeleccionMultiple
        '
        Me.chkSeleccionMultiple.Caption = "Selección Múltiple"
        Me.chkSeleccionMultiple.Id = 9
        Me.chkSeleccionMultiple.Name = "chkSeleccionMultiple"
        '
        'mnuTomarSeleccionados
        '
        Me.mnuTomarSeleccionados.Caption = "Aplicar Selección"
        Me.mnuTomarSeleccionados.Id = 10
        Me.mnuTomarSeleccionados.ImageOptions.SvgImage = CType(resources.GetObject("mnuTomarSeleccionados.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuTomarSeleccionados.Name = "mnuTomarSeleccionados"
        Me.mnuTomarSeleccionados.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'mnuExportarExcel
        '
        Me.mnuExportarExcel.Caption = "Exportar Excel"
        Me.mnuExportarExcel.Id = 11
        Me.mnuExportarExcel.ImageOptions.SvgImage = CType(resources.GetObject("mnuExportarExcel.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuExportarExcel.Name = "mnuExportarExcel"
        '
        'mnuMostrarMensajePorCadaReserva
        '
        Me.mnuMostrarMensajePorCadaReserva.Caption = "Mostrar mensaje por cada línea reservada"
        Me.mnuMostrarMensajePorCadaReserva.Id = 13
        Me.mnuMostrarMensajePorCadaReserva.Name = "mnuMostrarMensajePorCadaReserva"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1, Me.RibbonPageGroup2})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Lista Stock Especifico "
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuTomarSeleccionados)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.BarButtonItem1)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.BarButtonItem3)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardarLayoutGrid)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuExportarExcel)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEliminarLayoutGrid)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.BarButtonItem2)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.chkFiltroPolizaActivo)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonPageGroup2
        '
        Me.RibbonPageGroup2.ItemLinks.Add(Me.chkSeleccionMultiple)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.mnuMostrarMensajePorCadaReserva)
        Me.RibbonPageGroup2.Name = "RibbonPageGroup2"
        Me.RibbonPageGroup2.Text = "Modo Selección"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblProgress)
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblRegistros)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 732)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1339, 30)
        '
        'grdStock
        '
        Me.grdStock.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdStock.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdStock.Location = New System.Drawing.Point(307, 243)
        Me.grdStock.MainView = Me.GridView1
        Me.grdStock.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdStock.MenuManager = Me.RibbonControl
        Me.grdStock.Name = "grdStock"
        Me.grdStock.Size = New System.Drawing.Size(1032, 489)
        Me.grdStock.TabIndex = 2
        Me.grdStock.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.DetailHeight = 431
        Me.GridView1.GridControl = Me.grdStock
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsFind.AlwaysVisible = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowFooter = True
        '
        'prg
        '
        Me.prg.Dock = System.Windows.Forms.DockStyle.Top
        Me.prg.Location = New System.Drawing.Point(307, 193)
        Me.prg.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.prg.MenuManager = Me.RibbonControl
        Me.prg.Name = "prg"
        Me.prg.Size = New System.Drawing.Size(1032, 50)
        Me.prg.TabIndex = 5
        Me.prg.Visible = False
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.lblSeleccion)
        Me.PanelControl1.Controls.Add(Me.lblPoliza)
        Me.PanelControl1.Controls.Add(Me.txtNomPoliza)
        Me.PanelControl1.Controls.Add(Me.txtNoPoliza)
        Me.PanelControl1.Controls.Add(Me.GroupControl1)
        Me.PanelControl1.Controls.Add(Me.txtNombreProducto)
        Me.PanelControl1.Controls.Add(Me.txtIdProducto)
        Me.PanelControl1.Controls.Add(Me.lblProducto)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelControl1.Location = New System.Drawing.Point(0, 193)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(307, 539)
        Me.PanelControl1.TabIndex = 8
        '
        'lblSeleccion
        '
        Me.lblSeleccion.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSeleccion.Appearance.Options.UseFont = True
        Me.lblSeleccion.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblSeleccion.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblSeleccion.Location = New System.Drawing.Point(2, 151)
        Me.lblSeleccion.Name = "lblSeleccion"
        Me.lblSeleccion.Size = New System.Drawing.Size(303, 44)
        Me.lblSeleccion.TabIndex = 11
        Me.lblSeleccion.Text = "-"
        '
        'lblPoliza
        '
        Me.lblPoliza.AutoSize = True
        Me.lblPoliza.Location = New System.Drawing.Point(15, 85)
        Me.lblPoliza.Name = "lblPoliza"
        Me.lblPoliza.Size = New System.Drawing.Size(74, 16)
        Me.lblPoliza.TabIndex = 11
        Me.lblPoliza.Text = "Poliza / T.O"
        '
        'txtNomPoliza
        '
        Me.txtNomPoliza.Location = New System.Drawing.Point(126, 106)
        Me.txtNomPoliza.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNomPoliza.MenuManager = Me.RibbonControl
        Me.txtNomPoliza.Name = "txtNomPoliza"
        Me.txtNomPoliza.Size = New System.Drawing.Size(175, 22)
        Me.txtNomPoliza.TabIndex = 12
        '
        'txtNoPoliza
        '
        Me.txtNoPoliza.Location = New System.Drawing.Point(12, 106)
        Me.txtNoPoliza.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNoPoliza.MenuManager = Me.RibbonControl
        Me.txtNoPoliza.Name = "txtNoPoliza"
        Me.txtNoPoliza.Size = New System.Drawing.Size(107, 22)
        Me.txtNoPoliza.TabIndex = 14
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.lblDispsin)
        Me.GroupControl1.Controls.Add(Me.lblInfo)
        Me.GroupControl1.Controls.Add(Me.lblDispcon)
        Me.GroupControl1.Controls.Add(Me.lblDispNo)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupControl1.Location = New System.Drawing.Point(2, 195)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(303, 342)
        Me.GroupControl1.TabIndex = 11
        '
        'lblDispsin
        '
        Me.lblDispsin.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDispsin.Appearance.Options.UseImage = True
        Me.lblDispsin.Appearance.Options.UseImageAlign = True
        Me.lblDispsin.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.lblDispsin.ImageOptions.Image = Global.TOMWMS.My.Resources.Resources.green_ball
        Me.lblDispsin.Location = New System.Drawing.Point(19, 32)
        Me.lblDispsin.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblDispsin.Name = "lblDispsin"
        Me.lblDispsin.Size = New System.Drawing.Size(225, 36)
        Me.lblDispsin.TabIndex = 11
        Me.lblDispsin.Text = "Disponible sin reserva (Si aplica)"
        '
        'lblInfo
        '
        Me.lblInfo.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInfo.Appearance.ForeColor = System.Drawing.Color.Red
        Me.lblInfo.Appearance.Image = CType(resources.GetObject("lblInfo.Appearance.Image"), System.Drawing.Image)
        Me.lblInfo.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblInfo.Appearance.Options.UseFont = True
        Me.lblInfo.Appearance.Options.UseForeColor = True
        Me.lblInfo.Appearance.Options.UseImage = True
        Me.lblInfo.Appearance.Options.UseImageAlign = True
        Me.lblInfo.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.lblInfo.Location = New System.Drawing.Point(19, 187)
        Me.lblInfo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(194, 38)
        Me.lblInfo.TabIndex = 11
        Me.lblInfo.Text = "El cliente no tiene " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "tiempos asignados."
        Me.lblInfo.Visible = False
        '
        'lblDispcon
        '
        Me.lblDispcon.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDispcon.Appearance.Options.UseImage = True
        Me.lblDispcon.Appearance.Options.UseImageAlign = True
        Me.lblDispcon.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.lblDispcon.ImageOptions.Image = Global.TOMWMS.My.Resources.Resources.yellow_ball
        Me.lblDispcon.Location = New System.Drawing.Point(19, 84)
        Me.lblDispcon.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblDispcon.Name = "lblDispcon"
        Me.lblDispcon.Size = New System.Drawing.Size(229, 36)
        Me.lblDispcon.TabIndex = 12
        Me.lblDispcon.Text = "Disponible con reserva (Si aplica)"
        '
        'lblDispNo
        '
        Me.lblDispNo.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDispNo.Appearance.Options.UseImage = True
        Me.lblDispNo.Appearance.Options.UseImageAlign = True
        Me.lblDispNo.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.lblDispNo.ImageOptions.Image = Global.TOMWMS.My.Resources.Resources.red_ball
        Me.lblDispNo.Location = New System.Drawing.Point(19, 135)
        Me.lblDispNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblDispNo.Name = "lblDispNo"
        Me.lblDispNo.Size = New System.Drawing.Size(162, 36)
        Me.lblDispNo.TabIndex = 13
        Me.lblDispNo.Text = "Disponible (No aplica)"
        '
        'txtNombreProducto
        '
        Me.txtNombreProducto.Location = New System.Drawing.Point(126, 47)
        Me.txtNombreProducto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNombreProducto.MenuManager = Me.RibbonControl
        Me.txtNombreProducto.Name = "txtNombreProducto"
        Me.txtNombreProducto.Size = New System.Drawing.Size(175, 22)
        Me.txtNombreProducto.TabIndex = 9
        '
        'txtIdProducto
        '
        Me.txtIdProducto.Location = New System.Drawing.Point(12, 47)
        Me.txtIdProducto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtIdProducto.MenuManager = Me.RibbonControl
        Me.txtIdProducto.Name = "txtIdProducto"
        Me.txtIdProducto.Size = New System.Drawing.Size(107, 22)
        Me.txtIdProducto.TabIndex = 10
        '
        'lblProducto
        '
        Me.lblProducto.AutoSize = True
        Me.lblProducto.Location = New System.Drawing.Point(15, 27)
        Me.lblProducto.Name = "lblProducto"
        Me.lblProducto.Size = New System.Drawing.Size(57, 16)
        Me.lblProducto.TabIndex = 9
        Me.lblProducto.TabStop = True
        Me.lblProducto.Text = "Producto"
        '
        'BarButtonItem4
        '
        Me.BarButtonItem4.Caption = "Guardar diseño de grid"
        Me.BarButtonItem4.Id = 14
        Me.BarButtonItem4.Name = "BarButtonItem4"
        '
        'frmStock_Especifico_List
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1339, 762)
        Me.Controls.Add(Me.grdStock)
        Me.Controls.Add(Me.prg)
        Me.Controls.Add(Me.PanelControl1)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmStock_Especifico_List"
        Me.Ribbon = Me.RibbonControl
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Lista Stock Especifico"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdStock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.prg.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.txtNomPoliza.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoPoliza.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.txtNombreProducto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdProducto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents grdStock As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem3 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents prg As DevExpress.XtraEditors.ProgressBarControl
    Friend WithEvents lblProgress As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents lblRegistros As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents txtNombreProducto As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtIdProducto As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblProducto As LinkLabel
    Friend WithEvents lblDispNo As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblDispcon As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblDispsin As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblInfo As DevExpress.XtraEditors.LabelControl
    Friend WithEvents mnuGuardarLayoutGrid As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEliminarLayoutGrid As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents txtNomPoliza As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNoPoliza As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents chkFiltroPolizaActivo As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents lblPoliza As Label
    Friend WithEvents RibbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents chkSeleccionMultiple As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents mnuTomarSeleccionados As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lblSeleccion As DevExpress.XtraEditors.LabelControl
    Friend WithEvents mnuExportarExcel As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuMostrarMensajePorCadaReserva As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents BarButtonItem4 As DevExpress.XtraBars.BarButtonItem
End Class
