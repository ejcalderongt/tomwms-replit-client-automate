<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBackup
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
        Dim FileNameLabel As System.Windows.Forms.Label
        Dim PathLabel As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim Label5 As System.Windows.Forms.Label
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuGenerar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuGenerarBackup = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.GroupControl = New DevExpress.XtraEditors.GroupControl()
        Me.txtTimer = New DevExpress.XtraEditors.SpinEdit()
        Me.btnSearchFolder = New DevExpress.XtraEditors.SimpleButton()
        Me.lbProgress = New System.Windows.Forms.Label()
        Me.pbProgress = New DevExpress.XtraEditors.ProgressBarControl()
        Me.DataNeeded = New DevExpress.XtraEditors.RadioGroup()
        Me.PathSpinEdit = New DevExpress.XtraEditors.TextEdit()
        Me.FileNameSpinEdit = New DevExpress.XtraEditors.TextEdit()
        Me.FolderBrowserDialog = New System.Windows.Forms.FolderBrowserDialog()
        FileNameLabel = New System.Windows.Forms.Label()
        PathLabel = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        Label5 = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl.SuspendLayout()
        CType(Me.txtTimer.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbProgress.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataNeeded.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PathSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FileNameSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FileNameLabel
        '
        FileNameLabel.AutoSize = True
        FileNameLabel.Location = New System.Drawing.Point(14, 47)
        FileNameLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        FileNameLabel.Name = "FileNameLabel"
        FileNameLabel.Size = New System.Drawing.Size(57, 16)
        FileNameLabel.TabIndex = 0
        FileNameLabel.Text = "Nombre:"
        '
        'PathLabel
        '
        PathLabel.AutoSize = True
        PathLabel.Location = New System.Drawing.Point(14, 79)
        PathLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        PathLabel.Name = "PathLabel"
        PathLabel.Size = New System.Drawing.Size(66, 16)
        PathLabel.TabIndex = 2
        PathLabel.Text = "Ubicación:"
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(14, 119)
        Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(69, 16)
        Label1.TabIndex = 5
        Label1.Text = "Contenido:"
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.Location = New System.Drawing.Point(766, 217)
        Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(37, 16)
        Label3.TabIndex = 9
        Label3.Text = "/ 100"
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(14, 217)
        Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(63, 16)
        Label2.TabIndex = 7
        Label2.Text = "Progreso:"
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(15, 168)
        Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(46, 16)
        Label4.TabIndex = 12
        Label4.Text = "Timer:"
        '
        'Label5
        '
        Label5.AutoSize = True
        Label5.BackColor = System.Drawing.Color.Transparent
        Label5.Font = New System.Drawing.Font("Tahoma", 7.0!)
        Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Label5.Location = New System.Drawing.Point(233, 166)
        Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(249, 14)
        Label5.TabIndex = 13
        Label5.Text = "*Tiempo en segundos para ejecutar la tarea"
        '
        'RibbonControl
        '
        Me.RibbonControl.EmptyAreaImageOptions.ImagePadding = New System.Windows.Forms.Padding(35, 37, 35, 37)
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuGenerar, Me.mnuGenerarBackup})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(4)
        Me.RibbonControl.MaxItemId = 3
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.OptionsMenuMinWidth = 385
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(859, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'mnuGenerar
        '
        Me.mnuGenerar.Caption = "Generar SQL"
        Me.mnuGenerar.Id = 1
        Me.mnuGenerar.ImageOptions.Image = Global.TOMWMS.My.Resources.Resources.download_db
        Me.mnuGenerar.ImageOptions.LargeImage = Global.TOMWMS.My.Resources.Resources.download_db
        Me.mnuGenerar.Name = "mnuGenerar"
        '
        'mnuGenerarBackup
        '
        Me.mnuGenerarBackup.Caption = "Backup"
        Me.mnuGenerarBackup.Id = 2
        Me.mnuGenerarBackup.ImageOptions.Image = Global.TOMWMS.My.Resources.Resources.download_db
        Me.mnuGenerarBackup.ImageOptions.LargeImage = Global.TOMWMS.My.Resources.Resources.download_db
        Me.mnuGenerarBackup.Name = "mnuGenerarBackup"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Backup"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGenerarBackup)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 507)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(859, 30)
        '
        'GroupControl
        '
        Me.GroupControl.Controls.Add(Label5)
        Me.GroupControl.Controls.Add(Label4)
        Me.GroupControl.Controls.Add(Me.txtTimer)
        Me.GroupControl.Controls.Add(Me.btnSearchFolder)
        Me.GroupControl.Controls.Add(Me.lbProgress)
        Me.GroupControl.Controls.Add(Label3)
        Me.GroupControl.Controls.Add(Label2)
        Me.GroupControl.Controls.Add(Me.pbProgress)
        Me.GroupControl.Controls.Add(Label1)
        Me.GroupControl.Controls.Add(Me.DataNeeded)
        Me.GroupControl.Controls.Add(Me.PathSpinEdit)
        Me.GroupControl.Controls.Add(PathLabel)
        Me.GroupControl.Controls.Add(Me.FileNameSpinEdit)
        Me.GroupControl.Controls.Add(FileNameLabel)
        Me.GroupControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl.Location = New System.Drawing.Point(0, 193)
        Me.GroupControl.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl.Name = "GroupControl"
        Me.GroupControl.Size = New System.Drawing.Size(859, 314)
        Me.GroupControl.TabIndex = 0
        Me.GroupControl.Text = "Generador de Backup"
        '
        'txtTimer
        '
        Me.txtTimer.EditValue = New Decimal(New Integer() {150, 0, 0, 0})
        Me.txtTimer.Location = New System.Drawing.Point(120, 163)
        Me.txtTimer.Margin = New System.Windows.Forms.Padding(5)
        Me.txtTimer.Name = "txtTimer"
        Me.txtTimer.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTimer.Properties.Appearance.Options.UseFont = True
        Me.txtTimer.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtTimer.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.txtTimer.Properties.MaskSettings.Set("MaskManagerType", GetType(DevExpress.Data.Mask.NumericMaskManager))
        Me.txtTimer.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False")
        Me.txtTimer.Properties.MaxValue = New Decimal(New Integer() {6, 0, 0, 0})
        Me.txtTimer.Properties.MinValue = New Decimal(New Integer() {2, 0, 0, 0})
        Me.txtTimer.Size = New System.Drawing.Size(104, 24)
        Me.txtTimer.TabIndex = 11
        '
        'btnSearchFolder
        '
        Me.btnSearchFolder.Location = New System.Drawing.Point(775, 75)
        Me.btnSearchFolder.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSearchFolder.Name = "btnSearchFolder"
        Me.btnSearchFolder.Size = New System.Drawing.Size(29, 25)
        Me.btnSearchFolder.TabIndex = 4
        Me.btnSearchFolder.Text = "..."
        '
        'lbProgress
        '
        Me.lbProgress.AutoSize = True
        Me.lbProgress.Location = New System.Drawing.Point(742, 217)
        Me.lbProgress.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbProgress.Name = "lbProgress"
        Me.lbProgress.Size = New System.Drawing.Size(14, 16)
        Me.lbProgress.TabIndex = 8
        Me.lbProgress.Text = "0"
        Me.lbProgress.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pbProgress
        '
        Me.pbProgress.Location = New System.Drawing.Point(18, 236)
        Me.pbProgress.Margin = New System.Windows.Forms.Padding(4)
        Me.pbProgress.MenuManager = Me.RibbonControl
        Me.pbProgress.Name = "pbProgress"
        Me.pbProgress.Size = New System.Drawing.Size(786, 22)
        Me.pbProgress.TabIndex = 10
        '
        'DataNeeded
        '
        Me.DataNeeded.Location = New System.Drawing.Point(120, 108)
        Me.DataNeeded.Margin = New System.Windows.Forms.Padding(4)
        Me.DataNeeded.MenuManager = Me.RibbonControl
        Me.DataNeeded.Name = "DataNeeded"
        Me.DataNeeded.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.DataNeeded.Properties.Appearance.Options.UseBackColor = True
        Me.DataNeeded.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.DataNeeded.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(True, "Esquema y Datos", True, "with_data"), New DevExpress.XtraEditors.Controls.RadioGroupItem(False, "Esquema", True, "without_data")})
        Me.DataNeeded.Size = New System.Drawing.Size(264, 39)
        Me.DataNeeded.TabIndex = 6
        '
        'PathSpinEdit
        '
        Me.PathSpinEdit.Location = New System.Drawing.Point(120, 75)
        Me.PathSpinEdit.Margin = New System.Windows.Forms.Padding(4)
        Me.PathSpinEdit.MenuManager = Me.RibbonControl
        Me.PathSpinEdit.Name = "PathSpinEdit"
        Me.PathSpinEdit.Properties.LookAndFeel.SkinName = "Office 2010 Blue"
        Me.PathSpinEdit.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.PathSpinEdit.Size = New System.Drawing.Size(650, 22)
        Me.PathSpinEdit.TabIndex = 3
        '
        'FileNameSpinEdit
        '
        Me.FileNameSpinEdit.Location = New System.Drawing.Point(120, 43)
        Me.FileNameSpinEdit.Margin = New System.Windows.Forms.Padding(4)
        Me.FileNameSpinEdit.MenuManager = Me.RibbonControl
        Me.FileNameSpinEdit.Name = "FileNameSpinEdit"
        Me.FileNameSpinEdit.Properties.LookAndFeel.SkinName = "Office 2010 Blue"
        Me.FileNameSpinEdit.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.FileNameSpinEdit.Size = New System.Drawing.Size(684, 22)
        Me.FileNameSpinEdit.TabIndex = 1
        '
        'frmBackup
        '
        Me.AllowFormGlass = DevExpress.Utils.DefaultBoolean.[True]
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(859, 537)
        Me.Controls.Add(Me.GroupControl)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmBackup"
        Me.Ribbon = Me.RibbonControl
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Backup"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl.ResumeLayout(False)
        Me.GroupControl.PerformLayout()
        CType(Me.txtTimer.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbProgress.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataNeeded.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PathSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FileNameSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents GroupControl As DevExpress.XtraEditors.GroupControl
    Friend WithEvents mnuGenerar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents DataNeeded As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents PathSpinEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FileNameSpinEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents pbProgress As DevExpress.XtraEditors.ProgressBarControl
    Friend WithEvents lbProgress As Label
    Friend WithEvents mnuGenerarBackup As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnSearchFolder As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FolderBrowserDialog As FolderBrowserDialog
    Friend WithEvents txtTimer As DevExpress.XtraEditors.SpinEdit
End Class
