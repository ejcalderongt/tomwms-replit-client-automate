<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInventarioList
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
        Dim Label3 As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInventarioList))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.cmdActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdSalir = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImprimir = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdNuevo = New DevExpress.XtraBars.BarButtonItem()
        Me.lblRegs = New DevExpress.XtraBars.BarStaticItem()
        Me.cmdPlantilla = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.grdInventario = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.grpInventario = New System.Windows.Forms.GroupBox()
        Me.dtpFechaInicio = New System.Windows.Forms.DateTimePicker()
        Me.dtpFechaFin = New System.Windows.Forms.DateTimePicker()
        Me.chkActivo = New DevExpress.XtraBars.BarCheckItem()
        Label3 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdInventario, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpInventario.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.Location = New System.Drawing.Point(15, 32)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(80, 16)
        Label3.TabIndex = 4
        Label3.Text = "Fecha Inicio:"
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(212, 32)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(67, 16)
        Label4.TabIndex = 6
        Label4.Text = "Fecha Fin:"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.cmdActualizar, Me.cmdSalir, Me.cmdImprimir, Me.cmdNuevo, Me.lblRegs, Me.cmdPlantilla, Me.chkActivo})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 10
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(882, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'cmdActualizar
        '
        Me.cmdActualizar.Caption = "Actualizar"
        Me.cmdActualizar.Id = 1
        Me.cmdActualizar.ImageOptions.SvgImage = CType(resources.GetObject("cmdActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdActualizar.Name = "cmdActualizar"
        '
        'cmdSalir
        '
        Me.cmdSalir.Caption = "Salir"
        Me.cmdSalir.Id = 2
        Me.cmdSalir.ImageOptions.SvgImage = CType(resources.GetObject("cmdSalir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdSalir.Name = "cmdSalir"
        '
        'cmdImprimir
        '
        Me.cmdImprimir.Caption = "Imprimir"
        Me.cmdImprimir.Id = 3
        Me.cmdImprimir.ImageOptions.SvgImage = CType(resources.GetObject("cmdImprimir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdImprimir.Name = "cmdImprimir"
        '
        'cmdNuevo
        '
        Me.cmdNuevo.Caption = "Nuevo"
        Me.cmdNuevo.Id = 4
        Me.cmdNuevo.ImageOptions.SvgImage = CType(resources.GetObject("cmdNuevo.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdNuevo.Name = "cmdNuevo"
        '
        'lblRegs
        '
        Me.lblRegs.Caption = "Registros: 0"
        Me.lblRegs.Id = 5
        Me.lblRegs.Name = "lblRegs"
        '
        'cmdPlantilla
        '
        Me.cmdPlantilla.Caption = "Descargar plantilla"
        Me.cmdPlantilla.Id = 6
        Me.cmdPlantilla.ImageOptions.SvgImage = CType(resources.GetObject("cmdPlantilla.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdPlantilla.Name = "cmdPlantilla"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Lista Inventario"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdNuevo)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdImprimir)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdSalir)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdPlantilla)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.chkActivo)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblRegs)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 631)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(882, 30)
        '
        'grdInventario
        '
        Me.grdInventario.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdInventario.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdInventario.Location = New System.Drawing.Point(0, 255)
        Me.grdInventario.MainView = Me.GridView1
        Me.grdInventario.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdInventario.MenuManager = Me.RibbonControl
        Me.grdInventario.Name = "grdInventario"
        Me.grdInventario.Size = New System.Drawing.Size(882, 376)
        Me.grdInventario.TabIndex = 0
        Me.grdInventario.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.DetailHeight = 431
        Me.GridView1.GridControl = Me.grdInventario
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.Editable = False
        '
        'grpInventario
        '
        Me.grpInventario.Controls.Add(Me.dtpFechaInicio)
        Me.grpInventario.Controls.Add(Me.dtpFechaFin)
        Me.grpInventario.Controls.Add(Label3)
        Me.grpInventario.Controls.Add(Label4)
        Me.grpInventario.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpInventario.Location = New System.Drawing.Point(0, 193)
        Me.grpInventario.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grpInventario.Name = "grpInventario"
        Me.grpInventario.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grpInventario.Size = New System.Drawing.Size(882, 62)
        Me.grpInventario.TabIndex = 1
        Me.grpInventario.TabStop = False
        Me.grpInventario.Text = "Filtro por Fecha"
        '
        'dtpFechaInicio
        '
        Me.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaInicio.Location = New System.Drawing.Point(96, 25)
        Me.dtpFechaInicio.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtpFechaInicio.Name = "dtpFechaInicio"
        Me.dtpFechaInicio.Size = New System.Drawing.Size(103, 23)
        Me.dtpFechaInicio.TabIndex = 5
        '
        'dtpFechaFin
        '
        Me.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaFin.Location = New System.Drawing.Point(283, 25)
        Me.dtpFechaFin.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtpFechaFin.Name = "dtpFechaFin"
        Me.dtpFechaFin.Size = New System.Drawing.Size(103, 23)
        Me.dtpFechaFin.TabIndex = 7
        '
        'chkActivo
        '
        Me.chkActivo.BindableChecked = True
        Me.chkActivo.Caption = "Activos"
        Me.chkActivo.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText
        Me.chkActivo.Checked = True
        Me.chkActivo.Id = 9
        Me.chkActivo.Name = "chkActivo"
        '
        'frmInventarioList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(882, 661)
        Me.Controls.Add(Me.grdInventario)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.grpInventario)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmInventarioList"
        Me.Ribbon = Me.RibbonControl
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Inventario"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdInventario, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpInventario.ResumeLayout(False)
        Me.grpInventario.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents cmdActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdSalir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdImprimir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdNuevo As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents grdInventario As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grpInventario As GroupBox
    Friend WithEvents dtpFechaInicio As DateTimePicker
    Friend WithEvents dtpFechaFin As DateTimePicker
    Friend WithEvents lblRegs As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents cmdPlantilla As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents chkActivo As DevExpress.XtraBars.BarCheckItem
End Class
