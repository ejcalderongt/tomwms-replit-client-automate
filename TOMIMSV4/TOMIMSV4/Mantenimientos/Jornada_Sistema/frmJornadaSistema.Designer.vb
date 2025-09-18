<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmJornadaSistema
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmJornadaSistema))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.lblUsuario = New DevExpress.XtraBars.BarStaticItem()
        Me.lblServer = New DevExpress.XtraBars.BarStaticItem()
        Me.lblBD = New DevExpress.XtraBars.BarStaticItem()
        Me.lblEmpresa = New DevExpress.XtraBars.BarStaticItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.SchedulerDataStorage1 = New DevExpress.XtraScheduler.SchedulerDataStorage(Me.components)
        Me.TabPane1 = New DevExpress.XtraBars.Navigation.TabPane()
        Me.TabNavigationPage1 = New DevExpress.XtraBars.Navigation.TabNavigationPage()
        Me.lblprg = New System.Windows.Forms.RichTextBox()
        Me.prg = New System.Windows.Forms.ProgressBar()
        Me.BW = New System.ComponentModel.BackgroundWorker()
        Me.tmrJornada = New System.Windows.Forms.Timer(Me.components)
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SchedulerDataStorage1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TabPane1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPane1.SuspendLayout()
        Me.TabNavigationPage1.SuspendLayout()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.lblUsuario, Me.lblServer, Me.lblBD, Me.lblEmpresa, Me.mnuActualizar})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.RibbonControl.MaxItemId = 8
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.Size = New System.Drawing.Size(1444, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'lblUsuario
        '
        Me.lblUsuario.Caption = "Usuario"
        Me.lblUsuario.Id = 2
        Me.lblUsuario.ImageOptions.Image = CType(resources.GetObject("lblUsuario.ImageOptions.Image"), System.Drawing.Image)
        Me.lblUsuario.ImageOptions.LargeImage = CType(resources.GetObject("lblUsuario.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.lblUsuario.Name = "lblUsuario"
        Me.lblUsuario.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'lblServer
        '
        Me.lblServer.Caption = "Server"
        Me.lblServer.Id = 3
        Me.lblServer.ImageOptions.Image = CType(resources.GetObject("lblServer.ImageOptions.Image"), System.Drawing.Image)
        Me.lblServer.ImageOptions.LargeImage = CType(resources.GetObject("lblServer.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.lblServer.Name = "lblServer"
        Me.lblServer.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'lblBD
        '
        Me.lblBD.Caption = "BD"
        Me.lblBD.Id = 4
        Me.lblBD.ImageOptions.Image = CType(resources.GetObject("lblBD.ImageOptions.Image"), System.Drawing.Image)
        Me.lblBD.ImageOptions.LargeImage = CType(resources.GetObject("lblBD.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.lblBD.Name = "lblBD"
        Me.lblBD.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'lblEmpresa
        '
        Me.lblEmpresa.Caption = "Empresa"
        Me.lblEmpresa.Id = 5
        Me.lblEmpresa.ImageOptions.Image = CType(resources.GetObject("lblEmpresa.ImageOptions.Image"), System.Drawing.Image)
        Me.lblEmpresa.ImageOptions.LargeImage = CType(resources.GetObject("lblEmpresa.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.lblEmpresa.Name = "lblEmpresa"
        Me.lblEmpresa.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'mnuActualizar
        '
        Me.mnuActualizar.Caption = "Actualizar"
        Me.mnuActualizar.Id = 6
        Me.mnuActualizar.ImageOptions.SvgImage = CType(resources.GetObject("mnuActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuActualizar.Name = "mnuActualizar"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "JS - Menú Principal"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblUsuario)
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblServer)
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblBD)
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblEmpresa)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 632)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1444, 30)
        '
        'SchedulerDataStorage1
        '
        '
        '
        '
        Me.SchedulerDataStorage1.AppointmentDependencies.AutoReload = False
        '
        '
        '
        Me.SchedulerDataStorage1.Appointments.Labels.CreateNewLabel(0, "None", "&None", System.Drawing.SystemColors.Window)
        Me.SchedulerDataStorage1.Appointments.Labels.CreateNewLabel(1, "Important", "&Important", System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(190, Byte), Integer)))
        Me.SchedulerDataStorage1.Appointments.Labels.CreateNewLabel(2, "Business", "&Business", System.Drawing.Color.FromArgb(CType(CType(168, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(255, Byte), Integer)))
        Me.SchedulerDataStorage1.Appointments.Labels.CreateNewLabel(3, "Personal", "&Personal", System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(156, Byte), Integer)))
        Me.SchedulerDataStorage1.Appointments.Labels.CreateNewLabel(4, "Vacation", "&Vacation", System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(199, Byte), Integer)))
        Me.SchedulerDataStorage1.Appointments.Labels.CreateNewLabel(5, "Must Attend", "Must &Attend", System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(206, Byte), Integer), CType(CType(147, Byte), Integer)))
        Me.SchedulerDataStorage1.Appointments.Labels.CreateNewLabel(6, "Travel Required", "&Travel Required", System.Drawing.Color.FromArgb(CType(CType(199, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(255, Byte), Integer)))
        Me.SchedulerDataStorage1.Appointments.Labels.CreateNewLabel(7, "Needs Preparation", "&Needs Preparation", System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(152, Byte), Integer)))
        Me.SchedulerDataStorage1.Appointments.Labels.CreateNewLabel(8, "Birthday", "&Birthday", System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(233, Byte), Integer)))
        Me.SchedulerDataStorage1.Appointments.Labels.CreateNewLabel(9, "Anniversary", "&Anniversary", System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(223, Byte), Integer)))
        Me.SchedulerDataStorage1.Appointments.Labels.CreateNewLabel(10, "Phone Call", "Phone &Call", System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(165, Byte), Integer)))
        '
        'TabPane1
        '
        Me.TabPane1.Controls.Add(Me.TabNavigationPage1)
        Me.TabPane1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabPane1.Location = New System.Drawing.Point(0, 193)
        Me.TabPane1.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.TabPane1.Name = "TabPane1"
        Me.TabPane1.Pages.AddRange(New DevExpress.XtraBars.Navigation.NavigationPageBase() {Me.TabNavigationPage1})
        Me.TabPane1.RegularSize = New System.Drawing.Size(1444, 439)
        Me.TabPane1.SelectedPage = Me.TabNavigationPage1
        Me.TabPane1.Size = New System.Drawing.Size(1444, 439)
        Me.TabPane1.TabIndex = 5
        Me.TabPane1.Text = "TabPane1"
        '
        'TabNavigationPage1
        '
        Me.TabNavigationPage1.Caption = "Log. Jornada de sistema"
        Me.TabNavigationPage1.Controls.Add(Me.lblprg)
        Me.TabNavigationPage1.Controls.Add(Me.prg)
        Me.TabNavigationPage1.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.TabNavigationPage1.Name = "TabNavigationPage1"
        Me.TabNavigationPage1.Size = New System.Drawing.Size(1444, 398)
        '
        'lblprg
        '
        Me.lblprg.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lblprg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblprg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblprg.Font = New System.Drawing.Font("Consolas", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblprg.ForeColor = System.Drawing.Color.Black
        Me.lblprg.Location = New System.Drawing.Point(0, 0)
        Me.lblprg.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.lblprg.Name = "lblprg"
        Me.lblprg.Size = New System.Drawing.Size(1444, 361)
        Me.lblprg.TabIndex = 1
        Me.lblprg.Text = ""
        '
        'prg
        '
        Me.prg.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.prg.Location = New System.Drawing.Point(0, 361)
        Me.prg.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.prg.Name = "prg"
        Me.prg.Size = New System.Drawing.Size(1444, 37)
        Me.prg.TabIndex = 2
        '
        'BW
        '
        Me.BW.WorkerSupportsCancellation = True
        '
        'tmrJornada
        '
        Me.tmrJornada.Enabled = True
        Me.tmrJornada.Interval = 300000
        '
        'frmJornadaSistema
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1444, 662)
        Me.Controls.Add(Me.TabPane1)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.Name = "frmJornadaSistema"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Jornada de sistema - Menú Principal"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SchedulerDataStorage1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TabPane1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPane1.ResumeLayout(False)
        Me.TabNavigationPage1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents SchedulerDataStorage1 As DevExpress.XtraScheduler.SchedulerDataStorage
    Friend WithEvents lblUsuario As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents lblServer As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents lblBD As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents lblEmpresa As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents TabPane1 As DevExpress.XtraBars.Navigation.TabPane
    Friend WithEvents TabNavigationPage1 As DevExpress.XtraBars.Navigation.TabNavigationPage
    Friend WithEvents mnuActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BW As System.ComponentModel.BackgroundWorker
    Friend WithEvents tmrJornada As Timer
    Friend WithEvents lblprg As RichTextBox
    Friend WithEvents prg As ProgressBar
End Class
