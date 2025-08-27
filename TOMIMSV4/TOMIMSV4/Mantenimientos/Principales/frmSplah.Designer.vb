<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSplah
    Inherits DevExpress.XtraSplashScreen.SplashScreen

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.labelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.labelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.marqueeProgressBarControl1 = New DevExpress.XtraEditors.MarqueeProgressBarControl()
        Me.PictureEdit3 = New DevExpress.XtraEditors.PictureEdit()
        CType(Me.marqueeProgressBarControl1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureEdit3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'labelControl2
        '
        Me.labelControl2.Appearance.BackColor = System.Drawing.Color.Black
        Me.labelControl2.Appearance.ForeColor = System.Drawing.Color.White
        Me.labelControl2.Appearance.Options.UseBackColor = True
        Me.labelControl2.Appearance.Options.UseForeColor = True
        Me.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.labelControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.labelControl2.Location = New System.Drawing.Point(0, 789)
        Me.labelControl2.Margin = New System.Windows.Forms.Padding(4)
        Me.labelControl2.Name = "labelControl2"
        Me.labelControl2.Size = New System.Drawing.Size(853, 30)
        Me.labelControl2.TabIndex = 2
        Me.labelControl2.Text = "Cargando Módulos..."
        '
        'labelControl1
        '
        Me.labelControl1.Appearance.BackColor = System.Drawing.Color.Black
        Me.labelControl1.Appearance.ForeColor = System.Drawing.Color.White
        Me.labelControl1.Appearance.Options.UseBackColor = True
        Me.labelControl1.Appearance.Options.UseForeColor = True
        Me.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.labelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.labelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.labelControl1.Location = New System.Drawing.Point(0, 819)
        Me.labelControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.labelControl1.Name = "labelControl1"
        Me.labelControl1.Size = New System.Drawing.Size(853, 32)
        Me.labelControl1.TabIndex = 4
        Me.labelControl1.Text = "Copyright © 2007-2024"
        '
        'marqueeProgressBarControl1
        '
        Me.marqueeProgressBarControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.marqueeProgressBarControl1.EditValue = 0
        Me.marqueeProgressBarControl1.Location = New System.Drawing.Point(0, 748)
        Me.marqueeProgressBarControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.marqueeProgressBarControl1.Name = "marqueeProgressBarControl1"
        Me.marqueeProgressBarControl1.Properties.Appearance.BackColor = System.Drawing.Color.Black
        Me.marqueeProgressBarControl1.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.marqueeProgressBarControl1.Size = New System.Drawing.Size(853, 41)
        Me.marqueeProgressBarControl1.TabIndex = 3
        '
        'PictureEdit3
        '
        Me.PictureEdit3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PictureEdit3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureEdit3.EditValue = Global.TOMWMS.My.Resources.Resources.tom_wms
        Me.PictureEdit3.Location = New System.Drawing.Point(0, 0)
        Me.PictureEdit3.Name = "PictureEdit3"
        Me.PictureEdit3.Properties.Appearance.BackColor = System.Drawing.Color.Black
        Me.PictureEdit3.Properties.Appearance.Options.UseBackColor = True
        Me.PictureEdit3.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.[Auto]
        Me.PictureEdit3.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.PictureEdit3.Size = New System.Drawing.Size(853, 789)
        Me.PictureEdit3.TabIndex = 35
        '
        'frmSplah
        '
        Me.ActiveGlowColor = System.Drawing.Color.Black
        Me.AutoScaleDimensions = New System.Drawing.SizeF(120.0!, 120.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.Gainsboro
        Me.ClientSize = New System.Drawing.Size(853, 851)
        Me.Controls.Add(Me.marqueeProgressBarControl1)
        Me.Controls.Add(Me.PictureEdit3)
        Me.Controls.Add(Me.labelControl2)
        Me.Controls.Add(Me.labelControl1)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmSplah"
        Me.Text = "Cargando aplicación..."
        CType(Me.marqueeProgressBarControl1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureEdit3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents labelControl2 As DevExpress.XtraEditors.LabelControl
    Private WithEvents labelControl1 As DevExpress.XtraEditors.LabelControl
    Private WithEvents marqueeProgressBarControl1 As DevExpress.XtraEditors.MarqueeProgressBarControl
    Friend WithEvents PictureEdit3 As DevExpress.XtraEditors.PictureEdit
End Class
