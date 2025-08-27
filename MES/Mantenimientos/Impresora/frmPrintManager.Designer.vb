<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmPrintManager
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrintManager))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdTestImpresion = New DevExpress.XtraBars.BarButtonItem()
        Me.lblEstadoCola = New DevExpress.XtraBars.BarButtonItem()
        Me.lblEstadoProcesado = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage2 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.GroupControl5 = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.grdProcesado = New DevExpress.XtraGrid.GridControl()
        Me.gridviewProcesado = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GroupControl4 = New DevExpress.XtraEditors.GroupControl()
        Me.PanelControl3 = New DevExpress.XtraEditors.PanelControl()
        Me.grdCola = New DevExpress.XtraGrid.GridControl()
        Me.gridviewCola = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbIniciarTarea = New System.Windows.Forms.ToolStripButton()
        Me.tlbReintentarTarea = New System.Windows.Forms.ToolStripButton()
        Me.tsbCancelarTarea = New System.Windows.Forms.ToolStripButton()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.grdPrinters = New DevExpress.XtraGrid.GridControl()
        Me.gridViewPrinters = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
        Me.tlbCrearTarea = New System.Windows.Forms.ToolStripButton()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.lblLog = New System.Windows.Forms.RichTextBox()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl5.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.grdProcesado, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridviewProcesado, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl4.SuspendLayout()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl3.SuspendLayout()
        CType(Me.grdCola, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridviewCola, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.grdPrinters, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridViewPrinters, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip2.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.BarButtonItem1, Me.cmdActualizar, Me.cmdTestImpresion, Me.lblEstadoCola, Me.lblEstadoProcesado})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.RibbonControl.MaxItemId = 11
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage2})
        Me.RibbonControl.Size = New System.Drawing.Size(1780, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "Actualizar"
        Me.BarButtonItem1.Id = 3
        Me.BarButtonItem1.ImageOptions.SvgImage = CType(resources.GetObject("BarButtonItem1.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'cmdActualizar
        '
        Me.cmdActualizar.Caption = "Actualizar"
        Me.cmdActualizar.Id = 4
        Me.cmdActualizar.ImageOptions.SvgImage = CType(resources.GetObject("cmdActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdActualizar.Name = "cmdActualizar"
        '
        'cmdTestImpresion
        '
        Me.cmdTestImpresion.Caption = "Prueba de Impresión"
        Me.cmdTestImpresion.Id = 5
        Me.cmdTestImpresion.ImageOptions.SvgImage = CType(resources.GetObject("cmdTestImpresion.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdTestImpresion.Name = "cmdTestImpresion"
        '
        'lblEstadoCola
        '
        Me.lblEstadoCola.Caption = "Trabajos en cola: 0"
        Me.lblEstadoCola.Id = 8
        Me.lblEstadoCola.Name = "lblEstadoCola"
        '
        'lblEstadoProcesado
        '
        Me.lblEstadoProcesado.Caption = "BarButtonItem2"
        Me.lblEstadoProcesado.Id = 9
        Me.lblEstadoProcesado.Name = "lblEstadoProcesado"
        '
        'RibbonPage2
        '
        Me.RibbonPage2.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1, Me.RibbonPageGroup2})
        Me.RibbonPage2.Name = "RibbonPage2"
        Me.RibbonPage2.Text = "Lista de Impresoras"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdTestImpresion)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonPageGroup2
        '
        Me.RibbonPageGroup2.Name = "RibbonPageGroup2"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblEstadoCola)
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblEstadoProcesado)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 800)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1780, 30)
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Name = "RibbonPage1"
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.GroupControl5)
        Me.PanelControl1.Controls.Add(Me.GroupControl1)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl1.Location = New System.Drawing.Point(0, 193)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1780, 607)
        Me.PanelControl1.TabIndex = 3
        '
        'GroupControl5
        '
        Me.GroupControl5.Controls.Add(Me.GroupControl2)
        Me.GroupControl5.Controls.Add(Me.GroupControl4)
        Me.GroupControl5.Controls.Add(Me.GroupControl3)
        Me.GroupControl5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl5.Location = New System.Drawing.Point(2, 2)
        Me.GroupControl5.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl5.Name = "GroupControl5"
        Me.GroupControl5.Size = New System.Drawing.Size(1776, 511)
        Me.GroupControl5.TabIndex = 103
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.grdProcesado)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupControl2.Location = New System.Drawing.Point(1106, 28)
        Me.GroupControl2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(667, 481)
        Me.GroupControl2.TabIndex = 100
        Me.GroupControl2.Text = "Procesos Cerrados"
        '
        'grdProcesado
        '
        Me.grdProcesado.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdProcesado.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.grdProcesado.Location = New System.Drawing.Point(2, 28)
        Me.grdProcesado.MainView = Me.gridviewProcesado
        Me.grdProcesado.Margin = New System.Windows.Forms.Padding(4)
        Me.grdProcesado.MenuManager = Me.RibbonControl
        Me.grdProcesado.Name = "grdProcesado"
        Me.grdProcesado.Size = New System.Drawing.Size(663, 451)
        Me.grdProcesado.TabIndex = 2
        Me.grdProcesado.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridviewProcesado})
        '
        'gridviewProcesado
        '
        Me.gridviewProcesado.DetailHeight = 431
        Me.gridviewProcesado.GridControl = Me.grdProcesado
        Me.gridviewProcesado.Name = "gridviewProcesado"
        Me.gridviewProcesado.OptionsBehavior.ReadOnly = True
        Me.gridviewProcesado.OptionsFind.AlwaysVisible = True
        Me.gridviewProcesado.OptionsView.ShowAutoFilterRow = True
        '
        'GroupControl4
        '
        Me.GroupControl4.Controls.Add(Me.PanelControl3)
        Me.GroupControl4.Controls.Add(Me.ToolStrip1)
        Me.GroupControl4.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupControl4.Location = New System.Drawing.Point(520, 28)
        Me.GroupControl4.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl4.Name = "GroupControl4"
        Me.GroupControl4.Size = New System.Drawing.Size(586, 481)
        Me.GroupControl4.TabIndex = 102
        Me.GroupControl4.Text = "Procesos en Cola"
        '
        'PanelControl3
        '
        Me.PanelControl3.Controls.Add(Me.grdCola)
        Me.PanelControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl3.Location = New System.Drawing.Point(2, 55)
        Me.PanelControl3.Name = "PanelControl3"
        Me.PanelControl3.Size = New System.Drawing.Size(582, 424)
        Me.PanelControl3.TabIndex = 4
        '
        'grdCola
        '
        Me.grdCola.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdCola.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.grdCola.Location = New System.Drawing.Point(2, 2)
        Me.grdCola.MainView = Me.gridviewCola
        Me.grdCola.Margin = New System.Windows.Forms.Padding(4)
        Me.grdCola.MenuManager = Me.RibbonControl
        Me.grdCola.Name = "grdCola"
        Me.grdCola.Size = New System.Drawing.Size(578, 420)
        Me.grdCola.TabIndex = 2
        Me.grdCola.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridviewCola})
        '
        'gridviewCola
        '
        Me.gridviewCola.DetailHeight = 431
        Me.gridviewCola.GridControl = Me.grdCola
        Me.gridviewCola.Name = "gridviewCola"
        Me.gridviewCola.OptionsBehavior.ReadOnly = True
        Me.gridviewCola.OptionsFind.AlwaysVisible = True
        Me.gridviewCola.OptionsView.ShowAutoFilterRow = True
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbIniciarTarea, Me.tlbReintentarTarea, Me.tsbCancelarTarea})
        Me.ToolStrip1.Location = New System.Drawing.Point(2, 28)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(582, 27)
        Me.ToolStrip1.TabIndex = 3
        Me.ToolStrip1.Text = "ToolStrip4"
        '
        'tsbIniciarTarea
        '
        Me.tsbIniciarTarea.Image = CType(resources.GetObject("tsbIniciarTarea.Image"), System.Drawing.Image)
        Me.tsbIniciarTarea.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbIniciarTarea.Name = "tsbIniciarTarea"
        Me.tsbIniciarTarea.Size = New System.Drawing.Size(112, 24)
        Me.tsbIniciarTarea.Text = "Iniciar Tarea"
        '
        'tlbReintentarTarea
        '
        Me.tlbReintentarTarea.Image = CType(resources.GetObject("tlbReintentarTarea.Image"), System.Drawing.Image)
        Me.tlbReintentarTarea.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbReintentarTarea.Name = "tlbReintentarTarea"
        Me.tlbReintentarTarea.Size = New System.Drawing.Size(140, 24)
        Me.tlbReintentarTarea.Text = "Reintentar Tarea"
        '
        'tsbCancelarTarea
        '
        Me.tsbCancelarTarea.Image = CType(resources.GetObject("tsbCancelarTarea.Image"), System.Drawing.Image)
        Me.tsbCancelarTarea.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbCancelarTarea.Name = "tsbCancelarTarea"
        Me.tsbCancelarTarea.Size = New System.Drawing.Size(129, 24)
        Me.tsbCancelarTarea.Text = "Cancelar Tarea"
        '
        'GroupControl3
        '
        Me.GroupControl3.Controls.Add(Me.PanelControl2)
        Me.GroupControl3.Controls.Add(Me.ToolStrip2)
        Me.GroupControl3.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupControl3.Location = New System.Drawing.Point(2, 28)
        Me.GroupControl3.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(518, 481)
        Me.GroupControl3.TabIndex = 101
        Me.GroupControl3.Text = "Impresoras Disponibles"
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.grdPrinters)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl2.Location = New System.Drawing.Point(2, 55)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(514, 424)
        Me.PanelControl2.TabIndex = 3
        '
        'grdPrinters
        '
        Me.grdPrinters.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdPrinters.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.grdPrinters.Location = New System.Drawing.Point(2, 2)
        Me.grdPrinters.MainView = Me.gridViewPrinters
        Me.grdPrinters.Margin = New System.Windows.Forms.Padding(4)
        Me.grdPrinters.MenuManager = Me.RibbonControl
        Me.grdPrinters.Name = "grdPrinters"
        Me.grdPrinters.Size = New System.Drawing.Size(510, 420)
        Me.grdPrinters.TabIndex = 1
        Me.grdPrinters.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridViewPrinters})
        '
        'gridViewPrinters
        '
        Me.gridViewPrinters.DetailHeight = 431
        Me.gridViewPrinters.GridControl = Me.grdPrinters
        Me.gridViewPrinters.Name = "gridViewPrinters"
        Me.gridViewPrinters.OptionsBehavior.ReadOnly = True
        Me.gridViewPrinters.OptionsFind.AlwaysVisible = True
        Me.gridViewPrinters.OptionsView.ShowAutoFilterRow = True
        '
        'ToolStrip2
        '
        Me.ToolStrip2.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlbCrearTarea})
        Me.ToolStrip2.Location = New System.Drawing.Point(2, 28)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip2.Size = New System.Drawing.Size(514, 27)
        Me.ToolStrip2.TabIndex = 2
        Me.ToolStrip2.Text = "ToolStrip4"
        '
        'tlbCrearTarea
        '
        Me.tlbCrearTarea.Image = CType(resources.GetObject("tlbCrearTarea.Image"), System.Drawing.Image)
        Me.tlbCrearTarea.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbCrearTarea.Name = "tlbCrearTarea"
        Me.tlbCrearTarea.Size = New System.Drawing.Size(107, 24)
        Me.tlbCrearTarea.Text = "Crear Tarea"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.lblLog)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupControl1.Location = New System.Drawing.Point(2, 513)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1776, 92)
        Me.GroupControl1.TabIndex = 99
        Me.GroupControl1.Text = "Log"
        '
        'lblLog
        '
        Me.lblLog.BackColor = System.Drawing.Color.OldLace
        Me.lblLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblLog.Location = New System.Drawing.Point(2, 28)
        Me.lblLog.Margin = New System.Windows.Forms.Padding(4)
        Me.lblLog.Name = "lblLog"
        Me.lblLog.Size = New System.Drawing.Size(1772, 62)
        Me.lblLog.TabIndex = 6
        Me.lblLog.Text = ""
        '
        'frmPrintManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1780, 830)
        Me.Controls.Add(Me.PanelControl1)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.Name = "frmPrintManager"
        Me.Ribbon = Me.RibbonControl
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Impresoras"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl5.ResumeLayout(False)
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.grdProcesado, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridviewProcesado, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl4.ResumeLayout(False)
        Me.GroupControl4.PerformLayout()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl3.ResumeLayout(False)
        CType(Me.grdCola, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridviewCola, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        Me.GroupControl3.PerformLayout()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        CType(Me.grdPrinters, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridViewPrinters, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip2.ResumeLayout(False)
        Me.ToolStrip2.PerformLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents grdPrinters As DevExpress.XtraGrid.GridControl
    Friend WithEvents gridViewPrinters As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RibbonPage2 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdTestImpresion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents lblEstadoCola As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents grdProcesado As DevExpress.XtraGrid.GridControl
    Friend WithEvents gridviewProcesado As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grdCola As DevExpress.XtraGrid.GridControl
    Friend WithEvents gridviewCola As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lblEstadoProcesado As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblLog As RichTextBox
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl4 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl5 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ToolStrip2 As ToolStrip
    Friend WithEvents tlbCrearTarea As ToolStripButton
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents tlbReintentarTarea As ToolStripButton
    Friend WithEvents tsbIniciarTarea As ToolStripButton
    Friend WithEvents tsbCancelarTarea As ToolStripButton
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelControl3 As DevExpress.XtraEditors.PanelControl
End Class
