<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmIndicadores
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmIndicadores))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.btnRefresh = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.dvIndicadores = New DevExpress.DashboardWin.DashboardViewer(Me.components)
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbIndicador = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbOperacion = New DevExpress.XtraEditors.LookUpEdit()
        Me.grpFiltros = New DevExpress.XtraEditors.GroupControl()
        Me.lblPrg = New System.Windows.Forms.Label()
        Me.dtpFechaAl = New System.Windows.Forms.DateTimePicker()
        Me.dtpFechaDel = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dvIndicadores, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbIndicador.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbOperacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpFiltros, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpFiltros.SuspendLayout()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.EmptyAreaImageOptions.ImagePadding = New System.Windows.Forms.Padding(35, 37, 35, 37)
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.btnRefresh})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(4)
        Me.RibbonControl.MaxItemId = 2
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.OptionsMenuMinWidth = 385
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1378, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'btnRefresh
        '
        Me.btnRefresh.Caption = "Actualizar"
        Me.btnRefresh.Id = 1
        Me.btnRefresh.ImageOptions.SvgImage = CType(resources.GetObject("btnRefresh.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnRefresh.Name = "btnRefresh"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Indicadores"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.btnRefresh)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 817)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1378, 30)
        '
        'dvIndicadores
        '
        Me.dvIndicadores.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.dvIndicadores.Appearance.Options.UseBackColor = True
        Me.dvIndicadores.AsyncMode = True
        Me.dvIndicadores.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dvIndicadores.Location = New System.Drawing.Point(244, 193)
        Me.dvIndicadores.Margin = New System.Windows.Forms.Padding(4)
        Me.dvIndicadores.Name = "dvIndicadores"
        Me.dvIndicadores.Size = New System.Drawing.Size(1134, 624)
        Me.dvIndicadores.TabIndex = 1
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(13, 127)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(60, 16)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "Indicador"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(13, 67)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(65, 16)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "Operación"
        '
        'cmbIndicador
        '
        Me.cmbIndicador.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbIndicador.Location = New System.Drawing.Point(13, 148)
        Me.cmbIndicador.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbIndicador.MenuManager = Me.RibbonControl
        Me.cmbIndicador.Name = "cmbIndicador"
        Me.cmbIndicador.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbIndicador.Properties.NullText = ""
        Me.cmbIndicador.Size = New System.Drawing.Size(213, 22)
        Me.cmbIndicador.TabIndex = 6
        '
        'cmbOperacion
        '
        Me.cmbOperacion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbOperacion.Location = New System.Drawing.Point(13, 88)
        Me.cmbOperacion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbOperacion.MenuManager = Me.RibbonControl
        Me.cmbOperacion.Name = "cmbOperacion"
        Me.cmbOperacion.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbOperacion.Properties.NullText = ""
        Me.cmbOperacion.Size = New System.Drawing.Size(213, 22)
        Me.cmbOperacion.TabIndex = 4
        '
        'grpFiltros
        '
        Me.grpFiltros.Controls.Add(Me.Label2)
        Me.grpFiltros.Controls.Add(Me.Label1)
        Me.grpFiltros.Controls.Add(Me.dtpFechaAl)
        Me.grpFiltros.Controls.Add(Me.dtpFechaDel)
        Me.grpFiltros.Controls.Add(Me.Label7)
        Me.grpFiltros.Controls.Add(Me.lblPrg)
        Me.grpFiltros.Controls.Add(Me.Label6)
        Me.grpFiltros.Controls.Add(Me.cmbIndicador)
        Me.grpFiltros.Controls.Add(Me.cmbOperacion)
        Me.grpFiltros.Dock = System.Windows.Forms.DockStyle.Left
        Me.grpFiltros.Location = New System.Drawing.Point(0, 193)
        Me.grpFiltros.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grpFiltros.Name = "grpFiltros"
        Me.grpFiltros.Size = New System.Drawing.Size(244, 624)
        Me.grpFiltros.TabIndex = 21
        Me.grpFiltros.Text = "Filtros"
        '
        'lblPrg
        '
        Me.lblPrg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPrg.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblPrg.Location = New System.Drawing.Point(2, 510)
        Me.lblPrg.Name = "lblPrg"
        Me.lblPrg.Size = New System.Drawing.Size(240, 112)
        Me.lblPrg.TabIndex = 7
        '
        'dtpFechaAl
        '
        Me.dtpFechaAl.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaAl.Location = New System.Drawing.Point(13, 283)
        Me.dtpFechaAl.Margin = New System.Windows.Forms.Padding(4)
        Me.dtpFechaAl.Name = "dtpFechaAl"
        Me.dtpFechaAl.Size = New System.Drawing.Size(213, 23)
        Me.dtpFechaAl.TabIndex = 18
        '
        'dtpFechaDel
        '
        Me.dtpFechaDel.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaDel.Location = New System.Drawing.Point(13, 224)
        Me.dtpFechaDel.Margin = New System.Windows.Forms.Padding(4)
        Me.dtpFechaDel.Name = "dtpFechaDel"
        Me.dtpFechaDel.Size = New System.Drawing.Size(213, 23)
        Me.dtpFechaDel.TabIndex = 17
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 204)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(25, 16)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "Del"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 263)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(18, 16)
        Me.Label2.TabIndex = 20
        Me.Label2.Text = "Al"
        '
        'frmIndicadores
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1378, 847)
        Me.Controls.Add(Me.dvIndicadores)
        Me.Controls.Add(Me.grpFiltros)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmIndicadores"
        Me.Ribbon = Me.RibbonControl
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Indicadores"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dvIndicadores, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbIndicador.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbOperacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpFiltros, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpFiltros.ResumeLayout(False)
        Me.grpFiltros.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents cmbIndicador As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbOperacion As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents btnRefresh As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents dvIndicadores As DevExpress.DashboardWin.DashboardViewer
    Friend WithEvents grpFiltros As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblPrg As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents dtpFechaAl As DateTimePicker
    Friend WithEvents dtpFechaDel As DateTimePicker
End Class
