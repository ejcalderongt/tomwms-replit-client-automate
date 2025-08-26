<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLicSolicitud
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLicSolicitud))
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.lblServerAPP = New DevExpress.XtraEditors.LabelControl()
        Me.lblBDAPP = New DevExpress.XtraEditors.LabelControl()
        Me.cmdCerrar = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblMsj = New System.Windows.Forms.Label()
        Me.btnAplicar = New DevExpress.XtraEditors.SimpleButton()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblSol = New System.Windows.Forms.Label()
        Me.txtSol = New System.Windows.Forms.TextBox()
        Me.txtLic = New System.Windows.Forms.TextBox()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.lblServerAPP)
        Me.GroupControl1.Controls.Add(Me.lblBDAPP)
        Me.GroupControl1.Controls.Add(Me.cmdCerrar)
        Me.GroupControl1.Controls.Add(Me.GroupBox1)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(999, 680)
        Me.GroupControl1.TabIndex = 7
        Me.GroupControl1.Text = "Solicitud para servidor de licencias"
        '
        'lblServerAPP
        '
        Me.lblServerAPP.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.lblServerAPP.ImageOptions.SvgImage = CType(resources.GetObject("lblServerAPP.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.lblServerAPP.Location = New System.Drawing.Point(49, 498)
        Me.lblServerAPP.Name = "lblServerAPP"
        Me.lblServerAPP.Size = New System.Drawing.Size(89, 44)
        Me.lblServerAPP.TabIndex = 36
        Me.lblServerAPP.Text = "Server:"
        '
        'lblBDAPP
        '
        Me.lblBDAPP.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.lblBDAPP.ImageOptions.Image = CType(resources.GetObject("lblBDAPP.ImageOptions.Image"), System.Drawing.Image)
        Me.lblBDAPP.Location = New System.Drawing.Point(220, 502)
        Me.lblBDAPP.Name = "lblBDAPP"
        Me.lblBDAPP.Size = New System.Drawing.Size(58, 36)
        Me.lblBDAPP.TabIndex = 35
        Me.lblBDAPP.Text = "BD:"
        '
        'cmdCerrar
        '
        Me.cmdCerrar.AutoSize = True
        Me.cmdCerrar.ImageOptions.Image = CType(resources.GetObject("cmdCerrar.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdCerrar.Location = New System.Drawing.Point(961, 0)
        Me.cmdCerrar.Name = "cmdCerrar"
        Me.cmdCerrar.Size = New System.Drawing.Size(38, 36)
        Me.cmdCerrar.TabIndex = 34
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblMsj)
        Me.GroupBox1.Controls.Add(Me.btnAplicar)
        Me.GroupBox1.Controls.Add(Me.PictureBox3)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.PictureBox2)
        Me.GroupBox1.Controls.Add(Me.PictureBox1)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.lblSol)
        Me.GroupBox1.Controls.Add(Me.txtSol)
        Me.GroupBox1.Controls.Add(Me.txtLic)
        Me.GroupBox1.Location = New System.Drawing.Point(46, 71)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(812, 401)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        '
        'lblMsj
        '
        Me.lblMsj.AutoSize = True
        Me.lblMsj.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMsj.Location = New System.Drawing.Point(16, 30)
        Me.lblMsj.Name = "lblMsj"
        Me.lblMsj.Size = New System.Drawing.Size(669, 25)
        Me.lblMsj.TabIndex = 38
        Me.lblMsj.Text = "No se ha encontrado registro de licencia activa para éste ordenador!"
        '
        'btnAplicar
        '
        Me.btnAplicar.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAplicar.Appearance.Options.UseFont = True
        Me.btnAplicar.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnAplicar.Location = New System.Drawing.Point(3, 347)
        Me.btnAplicar.Name = "btnAplicar"
        Me.btnAplicar.Size = New System.Drawing.Size(806, 51)
        Me.btnAplicar.TabIndex = 35
        Me.btnAplicar.Text = "Aplicar"
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(19, 194)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(26, 22)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox3.TabIndex = 33
        Me.PictureBox3.TabStop = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(307, 298)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(171, 17)
        Me.Label11.TabIndex = 32
        Me.Label11.Text = "email : apalala@dts.com.gt"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(307, 270)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(198, 17)
        Me.Label7.TabIndex = 31
        Me.Label7.Text = "email : pvelasquez@dts.com.gt"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(151, 298)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(194, 17)
        Me.Label1.TabIndex = 30
        Me.Label1.Text = "tel : (502) 2210-7800 ext 113"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(49, 298)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(90, 17)
        Me.Label12.TabIndex = 29
        Me.Label12.Text = "Área soporte "
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(49, 270)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(100, 17)
        Me.Label10.TabIndex = 28
        Me.Label10.Text = "Área comercial "
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(151, 270)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(194, 17)
        Me.Label9.TabIndex = 27
        Me.Label9.Text = "tel : (502) 2210-7800 ext 103"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.Label8.Location = New System.Drawing.Point(83, 235)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(269, 21)
        Me.Label8.TabIndex = 26
        Me.Label8.Text = "Obtener llave por telefono o correo"
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(51, 235)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(26, 22)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox2.TabIndex = 25
        Me.PictureBox2.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(19, 105)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(26, 22)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 6
        Me.PictureBox1.TabStop = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(51, 164)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(400, 25)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Entre aquí la clave para completar la solicitud"
        '
        'lblSol
        '
        Me.lblSol.AutoSize = True
        Me.lblSol.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSol.Location = New System.Drawing.Point(51, 76)
        Me.lblSol.Name = "lblSol"
        Me.lblSol.Size = New System.Drawing.Size(378, 25)
        Me.lblSol.TabIndex = 2
        Me.lblSol.Text = "Indique ésta llave al proveedor  de licencia"
        '
        'txtSol
        '
        Me.txtSol.BackColor = System.Drawing.Color.LightSteelBlue
        Me.txtSol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSol.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSol.ForeColor = System.Drawing.Color.Black
        Me.txtSol.Location = New System.Drawing.Point(48, 105)
        Me.txtSol.Name = "txtSol"
        Me.txtSol.ReadOnly = True
        Me.txtSol.Size = New System.Drawing.Size(741, 30)
        Me.txtSol.TabIndex = 0
        Me.txtSol.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtLic
        '
        Me.txtLic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLic.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold)
        Me.txtLic.ForeColor = System.Drawing.Color.ForestGreen
        Me.txtLic.Location = New System.Drawing.Point(48, 194)
        Me.txtLic.Name = "txtLic"
        Me.txtLic.Size = New System.Drawing.Size(741, 30)
        Me.txtLic.TabIndex = 1
        '
        'frmLicSolicitud
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(999, 680)
        Me.Controls.Add(Me.GroupControl1)
        Me.Name = "frmLicSolicitud"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmLicSolicitud"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblServerAPP As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblBDAPP As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmdCerrar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents lblMsj As Label
    Friend WithEvents btnAplicar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label6 As Label
    Friend WithEvents lblSol As Label
    Friend WithEvents txtSol As TextBox
    Friend WithEvents txtLic As TextBox

End Class
