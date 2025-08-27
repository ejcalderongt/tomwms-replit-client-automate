<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSplashQuickTag
    Inherits DevExpress.XtraSplashScreen.SplashScreen

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.peImage = New DevExpress.XtraEditors.PictureEdit()
        Me.labelCopyright = New DevExpress.XtraEditors.LabelControl()
        Me.marqueeProgressBarControl1 = New DevExpress.XtraEditors.MarqueeProgressBarControl()
        CType(Me.peImage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.marqueeProgressBarControl1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'peImage
        '
        Me.peImage.Cursor = System.Windows.Forms.Cursors.Default
        Me.peImage.Dock = System.Windows.Forms.DockStyle.Top
        Me.peImage.EditValue = Global.Quick_Tag.My.Resources.Resources.QuickTag_DTS
        Me.peImage.Location = New System.Drawing.Point(1, 1)
        Me.peImage.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.peImage.Name = "peImage"
        Me.peImage.Properties.AllowFocused = False
        Me.peImage.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.peImage.Properties.Appearance.Options.UseBackColor = True
        Me.peImage.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.peImage.Properties.ShowMenu = False
        Me.peImage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch
        Me.peImage.Properties.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.None
        Me.peImage.Size = New System.Drawing.Size(586, 548)
        Me.peImage.TabIndex = 14
        '
        'labelCopyright
        '
        Me.labelCopyright.Appearance.BackColor = System.Drawing.Color.Black
        Me.labelCopyright.Appearance.ForeColor = System.Drawing.Color.White
        Me.labelCopyright.Appearance.Options.UseBackColor = True
        Me.labelCopyright.Appearance.Options.UseForeColor = True
        Me.labelCopyright.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.labelCopyright.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.labelCopyright.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.labelCopyright.Location = New System.Drawing.Point(1, 587)
        Me.labelCopyright.Margin = New System.Windows.Forms.Padding(4)
        Me.labelCopyright.Name = "labelCopyright"
        Me.labelCopyright.Size = New System.Drawing.Size(586, 32)
        Me.labelCopyright.TabIndex = 16
        Me.labelCopyright.Text = "Copyright © 2007-2025"
        '
        'marqueeProgressBarControl1
        '
        Me.marqueeProgressBarControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.marqueeProgressBarControl1.EditValue = 0
        Me.marqueeProgressBarControl1.Location = New System.Drawing.Point(1, 548)
        Me.marqueeProgressBarControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.marqueeProgressBarControl1.Name = "marqueeProgressBarControl1"
        Me.marqueeProgressBarControl1.Properties.Appearance.BackColor = System.Drawing.Color.Black
        Me.marqueeProgressBarControl1.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.marqueeProgressBarControl1.Size = New System.Drawing.Size(586, 39)
        Me.marqueeProgressBarControl1.TabIndex = 17
        '
        'frmSplashQuickTag
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(588, 620)
        Me.Controls.Add(Me.marqueeProgressBarControl1)
        Me.Controls.Add(Me.labelCopyright)
        Me.Controls.Add(Me.peImage)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "frmSplashQuickTag"
        Me.Padding = New System.Windows.Forms.Padding(1)
        Me.Text = "frmSplashQuickTag"
        CType(Me.peImage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.marqueeProgressBarControl1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents peImage As DevExpress.XtraEditors.PictureEdit
    Private WithEvents labelCopyright As DevExpress.XtraEditors.LabelControl
    Private WithEvents marqueeProgressBarControl1 As DevExpress.XtraEditors.MarqueeProgressBarControl
End Class
