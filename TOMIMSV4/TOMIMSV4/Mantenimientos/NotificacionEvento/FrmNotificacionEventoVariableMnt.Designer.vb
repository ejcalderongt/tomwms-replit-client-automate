<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmNotificacionEventoVariableMnt
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If components IsNot Nothing Then components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmNotificacionEventoVariableMnt))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuNuevo = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuRefrescar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.gcVariables = New DevExpress.XtraGrid.GridControl()
        Me.gvVariables = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.txtBuscar = New DevExpress.XtraEditors.SearchControl()
        Me.LayoutMain = New DevExpress.XtraLayout.LayoutControl()
        Me.txtIdEventoVariable = New DevExpress.XtraEditors.TextEdit()
        Me.lueEvento = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtNombreVariable = New DevExpress.XtraEditors.TextEdit()
        Me.txtDescripcion = New DevExpress.XtraEditors.MemoEdit()
        Me.txtEjemploValor = New DevExpress.XtraEditors.MemoEdit()
        Me.chkObligatoria = New DevExpress.XtraEditors.CheckEdit()
        Me.chkActivo = New DevExpress.XtraEditors.CheckEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutDatos = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.txtIdEventoVariableItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lueEventoItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.txtNombreVariableItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.txtDescripcionItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.txtEjemploValorItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.chkObligatoriaItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.chkActivoItem = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel1.SuspendLayout()
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel2.SuspendLayout()
        Me.SplitContainerControl1.SuspendLayout()
        CType(Me.gcVariables, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvVariables, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBuscar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutMain.SuspendLayout()
        CType(Me.txtIdEventoVariable.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lueEvento.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreVariable.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescripcion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEjemploValor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkObligatoria.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutDatos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdEventoVariableItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lueEventoItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreVariableItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescripcionItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEjemploValorItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkObligatoriaItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivoItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuNuevo, Me.mnuGuardar, Me.mnuEliminar, Me.mnuRefrescar})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 10
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1300, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'mnuNuevo
        '
        Me.mnuNuevo.Caption = "Nuevo"
        Me.mnuNuevo.Id = 1
        Me.mnuNuevo.ImageOptions.SvgImage = CType(resources.GetObject("mnuNuevo.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuNuevo.Name = "mnuNuevo"
        '
        'mnuGuardar
        '
        Me.mnuGuardar.Caption = "Guardar"
        Me.mnuGuardar.Id = 2
        Me.mnuGuardar.ImageOptions.SvgImage = CType(resources.GetObject("mnuGuardar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuGuardar.Name = "mnuGuardar"
        '
        'mnuEliminar
        '
        Me.mnuEliminar.Caption = "Eliminar"
        Me.mnuEliminar.Id = 3
        Me.mnuEliminar.ImageOptions.SvgImage = CType(resources.GetObject("mnuEliminar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEliminar.Name = "mnuEliminar"
        '
        'mnuRefrescar
        '
        Me.mnuRefrescar.Caption = "Refrescar"
        Me.mnuRefrescar.Id = 4
        Me.mnuRefrescar.ImageOptions.SvgImage = CType(resources.GetObject("mnuRefrescar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuRefrescar.Name = "mnuRefrescar"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Mantenimiento de variables"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuNuevo)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEliminar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuRefrescar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 690)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1300, 30)
        '
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl1.Location = New System.Drawing.Point(0, 193)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        '
        'SplitContainerControl1.Panel1
        '
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.gcVariables)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.txtBuscar)
        '
        'SplitContainerControl1.Panel2
        '
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.LayoutMain)
        Me.SplitContainerControl1.Size = New System.Drawing.Size(1300, 497)
        Me.SplitContainerControl1.SplitterPosition = 500
        Me.SplitContainerControl1.TabIndex = 0
        '
        'gcVariables
        '
        Me.gcVariables.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcVariables.Location = New System.Drawing.Point(0, 22)
        Me.gcVariables.MainView = Me.gvVariables
        Me.gcVariables.MenuManager = Me.RibbonControl
        Me.gcVariables.Name = "gcVariables"
        Me.gcVariables.Size = New System.Drawing.Size(500, 475)
        Me.gcVariables.TabIndex = 0
        Me.gcVariables.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvVariables})
        '
        'gvVariables
        '
        Me.gvVariables.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus
        Me.gvVariables.GridControl = Me.gcVariables
        Me.gvVariables.Name = "gvVariables"
        Me.gvVariables.OptionsBehavior.Editable = False
        Me.gvVariables.OptionsBehavior.ReadOnly = True
        Me.gvVariables.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvVariables.OptionsView.ShowGroupPanel = False
        '
        'txtBuscar
        '
        Me.txtBuscar.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtBuscar.Location = New System.Drawing.Point(0, 0)
        Me.txtBuscar.MenuManager = Me.RibbonControl
        Me.txtBuscar.Name = "txtBuscar"
        Me.txtBuscar.Properties.NullValuePrompt = "Buscar por evento o variable..."
        Me.txtBuscar.Properties.ShowClearButton = False
        Me.txtBuscar.Properties.ShowSearchButton = False
        Me.txtBuscar.Size = New System.Drawing.Size(500, 22)
        Me.txtBuscar.TabIndex = 1
        '
        'LayoutMain
        '
        Me.LayoutMain.Controls.Add(Me.txtIdEventoVariable)
        Me.LayoutMain.Controls.Add(Me.lueEvento)
        Me.LayoutMain.Controls.Add(Me.txtNombreVariable)
        Me.LayoutMain.Controls.Add(Me.txtDescripcion)
        Me.LayoutMain.Controls.Add(Me.txtEjemploValor)
        Me.LayoutMain.Controls.Add(Me.chkObligatoria)
        Me.LayoutMain.Controls.Add(Me.chkActivo)
        Me.LayoutMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutMain.Location = New System.Drawing.Point(0, 0)
        Me.LayoutMain.Name = "LayoutMain"
        Me.LayoutMain.Root = Me.LayoutControlGroup1
        Me.LayoutMain.Size = New System.Drawing.Size(788, 497)
        Me.LayoutMain.TabIndex = 0
        '
        'txtIdEventoVariable
        '
        Me.txtIdEventoVariable.Enabled = False
        Me.txtIdEventoVariable.Location = New System.Drawing.Point(113, 54)
        Me.txtIdEventoVariable.MenuManager = Me.RibbonControl
        Me.txtIdEventoVariable.Name = "txtIdEventoVariable"
        Me.txtIdEventoVariable.Size = New System.Drawing.Size(647, 22)
        Me.txtIdEventoVariable.StyleController = Me.LayoutMain
        Me.txtIdEventoVariable.TabIndex = 4
        '
        'lueEvento
        '
        Me.lueEvento.Location = New System.Drawing.Point(113, 447)
        Me.lueEvento.MenuManager = Me.RibbonControl
        Me.lueEvento.Name = "lueEvento"
        Me.lueEvento.Properties.NullText = ""
        Me.lueEvento.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoSearch
        Me.lueEvento.Properties.ShowHeader = False
        Me.lueEvento.Size = New System.Drawing.Size(647, 22)
        Me.lueEvento.StyleController = Me.LayoutMain
        Me.lueEvento.TabIndex = 5
        '
        'txtNombreVariable
        '
        Me.txtNombreVariable.Location = New System.Drawing.Point(113, 421)
        Me.txtNombreVariable.MenuManager = Me.RibbonControl
        Me.txtNombreVariable.Name = "txtNombreVariable"
        Me.txtNombreVariable.Size = New System.Drawing.Size(647, 22)
        Me.txtNombreVariable.StyleController = Me.LayoutMain
        Me.txtNombreVariable.TabIndex = 6
        '
        'txtDescripcion
        '
        Me.txtDescripcion.Location = New System.Drawing.Point(113, 220)
        Me.txtDescripcion.MenuManager = Me.RibbonControl
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.Size = New System.Drawing.Size(647, 197)
        Me.txtDescripcion.StyleController = Me.LayoutMain
        Me.txtDescripcion.TabIndex = 7
        '
        'txtEjemploValor
        '
        Me.txtEjemploValor.Location = New System.Drawing.Point(113, 136)
        Me.txtEjemploValor.MenuManager = Me.RibbonControl
        Me.txtEjemploValor.Name = "txtEjemploValor"
        Me.txtEjemploValor.Size = New System.Drawing.Size(647, 80)
        Me.txtEjemploValor.StyleController = Me.LayoutMain
        Me.txtEjemploValor.TabIndex = 8
        '
        'chkObligatoria
        '
        Me.chkObligatoria.Location = New System.Drawing.Point(113, 108)
        Me.chkObligatoria.MenuManager = Me.RibbonControl
        Me.chkObligatoria.Name = "chkObligatoria"
        Me.chkObligatoria.Properties.Caption = ""
        Me.chkObligatoria.Size = New System.Drawing.Size(647, 24)
        Me.chkObligatoria.StyleController = Me.LayoutMain
        Me.chkObligatoria.TabIndex = 9
        '
        'chkActivo
        '
        Me.chkActivo.Location = New System.Drawing.Point(113, 80)
        Me.chkActivo.MenuManager = Me.RibbonControl
        Me.chkActivo.Name = "chkActivo"
        Me.chkActivo.Properties.Caption = ""
        Me.chkActivo.Size = New System.Drawing.Size(647, 24)
        Me.chkActivo.StyleController = Me.LayoutMain
        Me.chkActivo.TabIndex = 10
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutDatos})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(788, 497)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutDatos
        '
        Me.LayoutDatos.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.txtIdEventoVariableItem, Me.lueEventoItem, Me.txtNombreVariableItem, Me.txtDescripcionItem, Me.txtEjemploValorItem, Me.chkObligatoriaItem, Me.chkActivoItem})
        Me.LayoutDatos.Location = New System.Drawing.Point(0, 0)
        Me.LayoutDatos.Name = "LayoutDatos"
        Me.LayoutDatos.Size = New System.Drawing.Size(764, 473)
        Me.LayoutDatos.Text = "Datos de la variable"
        '
        'txtIdEventoVariableItem
        '
        Me.txtIdEventoVariableItem.Control = Me.txtIdEventoVariable
        Me.txtIdEventoVariableItem.Location = New System.Drawing.Point(0, 0)
        Me.txtIdEventoVariableItem.Name = "txtIdEventoVariableItem"
        Me.txtIdEventoVariableItem.Size = New System.Drawing.Size(736, 26)
        Me.txtIdEventoVariableItem.Text = "Id Variable:"
        Me.txtIdEventoVariableItem.TextSize = New System.Drawing.Size(70, 16)
        '
        'lueEventoItem
        '
        Me.lueEventoItem.Control = Me.lueEvento
        Me.lueEventoItem.Location = New System.Drawing.Point(0, 393)
        Me.lueEventoItem.Name = "lueEventoItem"
        Me.lueEventoItem.Size = New System.Drawing.Size(736, 26)
        Me.lueEventoItem.Text = "Evento:"
        Me.lueEventoItem.TextSize = New System.Drawing.Size(70, 16)
        '
        'txtNombreVariableItem
        '
        Me.txtNombreVariableItem.Control = Me.txtNombreVariable
        Me.txtNombreVariableItem.Location = New System.Drawing.Point(0, 367)
        Me.txtNombreVariableItem.Name = "txtNombreVariableItem"
        Me.txtNombreVariableItem.Size = New System.Drawing.Size(736, 26)
        Me.txtNombreVariableItem.Text = "Variable:"
        Me.txtNombreVariableItem.TextSize = New System.Drawing.Size(70, 16)
        '
        'txtDescripcionItem
        '
        Me.txtDescripcionItem.Control = Me.txtDescripcion
        Me.txtDescripcionItem.Location = New System.Drawing.Point(0, 166)
        Me.txtDescripcionItem.Name = "txtDescripcionItem"
        Me.txtDescripcionItem.Size = New System.Drawing.Size(736, 201)
        Me.txtDescripcionItem.Text = "Descripción:"
        Me.txtDescripcionItem.TextSize = New System.Drawing.Size(70, 16)
        '
        'txtEjemploValorItem
        '
        Me.txtEjemploValorItem.Control = Me.txtEjemploValor
        Me.txtEjemploValorItem.Location = New System.Drawing.Point(0, 82)
        Me.txtEjemploValorItem.Name = "txtEjemploValorItem"
        Me.txtEjemploValorItem.Size = New System.Drawing.Size(736, 84)
        Me.txtEjemploValorItem.Text = "Ejemplo:"
        Me.txtEjemploValorItem.TextSize = New System.Drawing.Size(70, 16)
        '
        'chkObligatoriaItem
        '
        Me.chkObligatoriaItem.Control = Me.chkObligatoria
        Me.chkObligatoriaItem.Location = New System.Drawing.Point(0, 54)
        Me.chkObligatoriaItem.Name = "chkObligatoriaItem"
        Me.chkObligatoriaItem.Size = New System.Drawing.Size(736, 28)
        Me.chkObligatoriaItem.Text = "Obligatoria:"
        Me.chkObligatoriaItem.TextSize = New System.Drawing.Size(70, 16)
        '
        'chkActivoItem
        '
        Me.chkActivoItem.Control = Me.chkActivo
        Me.chkActivoItem.Location = New System.Drawing.Point(0, 26)
        Me.chkActivoItem.Name = "chkActivoItem"
        Me.chkActivoItem.Size = New System.Drawing.Size(736, 28)
        Me.chkActivoItem.Text = "Activo:"
        Me.chkActivoItem.TextSize = New System.Drawing.Size(70, 16)
        '
        'FrmNotificacionEventoVariableMnt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1300, 720)
        Me.Controls.Add(Me.SplitContainerControl1)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmNotificacionEventoVariableMnt"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Mantenimiento de variables por evento"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel1.ResumeLayout(False)
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        CType(Me.gcVariables, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvVariables, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBuscar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutMain.ResumeLayout(False)
        CType(Me.txtIdEventoVariable.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lueEvento.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreVariable.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescripcion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEjemploValor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkObligatoria.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutDatos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdEventoVariableItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lueEventoItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreVariableItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescripcionItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEjemploValorItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkObligatoriaItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivoItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents mnuNuevo As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEliminar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuRefrescar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents txtBuscar As DevExpress.XtraEditors.SearchControl
    Friend WithEvents gcVariables As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvVariables As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutMain As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents txtIdEventoVariable As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lueEvento As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents txtNombreVariable As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtDescripcion As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents txtEjemploValor As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents chkObligatoria As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkActivo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutDatos As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents txtIdEventoVariableItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lueEventoItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtNombreVariableItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtDescripcionItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtEjemploValorItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents chkObligatoriaItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents chkActivoItem As DevExpress.XtraLayout.LayoutControlItem
End Class