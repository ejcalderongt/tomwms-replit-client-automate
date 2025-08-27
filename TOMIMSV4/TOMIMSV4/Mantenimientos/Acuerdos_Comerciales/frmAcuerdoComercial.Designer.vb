<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAcuerdoComercial
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
        Dim Label1 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAcuerdoComercial))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.cmdAplicar = New DevExpress.XtraBars.BarButtonItem()
        Me.lblRegs = New DevExpress.XtraBars.BarButtonItem()
        Me.chkActivos = New DevExpress.XtraBars.BarCheckItem()
        Me.cmdImportarAcuerdosERP = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdRecargar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImpresion = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup3 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.cmbPropietario = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.DGridAcuerdos = New DevExpress.XtraGrid.GridControl()
        Me.GrdAcuerdosComerciales = New DevExpress.XtraGrid.Views.Grid.GridView()
        Label1 = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGridAcuerdos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrdAcuerdosComerciales, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(5, 36)
        Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(74, 16)
        Label1.TabIndex = 23
        Label1.Text = "Propietario:"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.cmdAplicar, Me.lblRegs, Me.chkActivos, Me.cmdImportarAcuerdosERP, Me.cmdRecargar, Me.cmdImpresion})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.RibbonControl.MaxItemId = 7
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1424, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'cmdAplicar
        '
        Me.cmdAplicar.Caption = "Guardar"
        Me.cmdAplicar.Id = 1
        Me.cmdAplicar.ImageOptions.SvgImage = CType(resources.GetObject("cmdAplicar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdAplicar.Name = "cmdAplicar"
        '
        'lblRegs
        '
        Me.lblRegs.Caption = "Registros: 0"
        Me.lblRegs.Id = 2
        Me.lblRegs.Name = "lblRegs"
        '
        'chkActivos
        '
        Me.chkActivos.BindableChecked = True
        Me.chkActivos.Caption = "Activos"
        Me.chkActivos.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText
        Me.chkActivos.Checked = True
        Me.chkActivos.Id = 3
        Me.chkActivos.Name = "chkActivos"
        '
        'cmdImportarAcuerdosERP
        '
        Me.cmdImportarAcuerdosERP.Caption = "Importar Acuerdos desde ERP"
        Me.cmdImportarAcuerdosERP.Id = 4
        Me.cmdImportarAcuerdosERP.ImageOptions.SvgImage = CType(resources.GetObject("cmdImportarAcuerdosERP.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdImportarAcuerdosERP.Name = "cmdImportarAcuerdosERP"
        '
        'cmdRecargar
        '
        Me.cmdRecargar.Caption = "Recargar Lista"
        Me.cmdRecargar.Id = 5
        Me.cmdRecargar.ImageOptions.SvgImage = CType(resources.GetObject("cmdRecargar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdRecargar.Name = "cmdRecargar"
        '
        'cmdImpresion
        '
        Me.cmdImpresion.Caption = "Impresion"
        Me.cmdImpresion.Id = 6
        Me.cmdImpresion.ImageOptions.SvgImage = CType(resources.GetObject("cmdImpresion.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdImpresion.Name = "cmdImpresion"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1, Me.RibbonPageGroup3})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Acuerdos Comerciales"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdAplicar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdImpresion)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdRecargar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.chkActivos)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonPageGroup3
        '
        Me.RibbonPageGroup3.ItemLinks.Add(Me.cmdImportarAcuerdosERP)
        Me.RibbonPageGroup3.Name = "RibbonPageGroup3"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblRegs)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 697)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1424, 30)
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Label1)
        Me.PanelControl1.Controls.Add(Me.cmbPropietario)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 193)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1424, 110)
        Me.PanelControl1.TabIndex = 2
        '
        'cmbPropietario
        '
        Me.cmbPropietario.Location = New System.Drawing.Point(85, 33)
        Me.cmbPropietario.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbPropietario.Name = "cmbPropietario"
        Me.cmbPropietario.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbPropietario.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPropietario.Properties.NullText = "Seleccione propietario"
        Me.cmbPropietario.Properties.PopupView = Me.GridLookUpEdit1View
        Me.cmbPropietario.Size = New System.Drawing.Size(432, 22)
        Me.cmbPropietario.TabIndex = 24
        '
        'GridLookUpEdit1View
        '
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'DGridAcuerdos
        '
        Me.DGridAcuerdos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGridAcuerdos.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.DGridAcuerdos.Location = New System.Drawing.Point(0, 303)
        Me.DGridAcuerdos.MainView = Me.GrdAcuerdosComerciales
        Me.DGridAcuerdos.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.DGridAcuerdos.Name = "DGridAcuerdos"
        Me.DGridAcuerdos.Size = New System.Drawing.Size(1424, 394)
        Me.DGridAcuerdos.TabIndex = 3
        Me.DGridAcuerdos.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GrdAcuerdosComerciales})
        '
        'GrdAcuerdosComerciales
        '
        Me.GrdAcuerdosComerciales.GridControl = Me.DGridAcuerdos
        Me.GrdAcuerdosComerciales.Name = "GrdAcuerdosComerciales"
        Me.GrdAcuerdosComerciales.OptionsView.ShowAutoFilterRow = True
        Me.GrdAcuerdosComerciales.OptionsView.ShowGroupPanel = False
        '
        'frmAcuerdoComercial
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1424, 727)
        Me.Controls.Add(Me.DGridAcuerdos)
        Me.Controls.Add(Me.PanelControl1)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.Name = "frmAcuerdoComercial"
        Me.Ribbon = Me.RibbonControl
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Acuerdos Comerciales"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGridAcuerdos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrdAcuerdosComerciales, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents cmdAplicar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents DGridAcuerdos As DevExpress.XtraGrid.GridControl
    Friend WithEvents GrdAcuerdosComerciales As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lblRegs As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents chkActivos As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents cmdImportarAcuerdosERP As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup3 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents cmdRecargar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmbPropietario As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cmdImpresion As DevExpress.XtraBars.BarButtonItem
End Class
