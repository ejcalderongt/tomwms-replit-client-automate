<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLicenciaSolicitada
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLicenciaSolicitada))
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.cmdCerrar = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblServerAPP = New DevExpress.XtraEditors.LabelControl()
        Me.lblBDAPP = New DevExpress.XtraEditors.LabelControl()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblMsj = New System.Windows.Forms.Label()
        Me.cmdLiberacionLicencia = New DevExpress.XtraEditors.SimpleButton()
        Me.btnAplicar = New DevExpress.XtraEditors.SimpleButton()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.lblserverinfo = New System.Windows.Forms.Label()
        Me.lblHostInfo = New System.Windows.Forms.Label()
        Me.txtHostSolicitante = New System.Windows.Forms.TextBox()
        Me.txtServidorLicencias = New System.Windows.Forms.TextBox()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.cmdCerrar)
        Me.GroupControl1.Controls.Add(Me.GroupBox1)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(871, 559)
        Me.GroupControl1.TabIndex = 8
        Me.GroupControl1.Text = "Solicitud de licencia"
        '
        'cmdCerrar
        '
        Me.cmdCerrar.AutoSize = True
        Me.cmdCerrar.ImageOptions.Image = CType(resources.GetObject("cmdCerrar.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdCerrar.Location = New System.Drawing.Point(839, 0)
        Me.cmdCerrar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmdCerrar.Name = "cmdCerrar"
        Me.cmdCerrar.Size = New System.Drawing.Size(38, 36)
        Me.cmdCerrar.TabIndex = 34
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblServerAPP)
        Me.GroupBox1.Controls.Add(Me.lblBDAPP)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.lblMsj)
        Me.GroupBox1.Controls.Add(Me.cmdLiberacionLicencia)
        Me.GroupBox1.Controls.Add(Me.btnAplicar)
        Me.GroupBox1.Controls.Add(Me.PictureBox3)
        Me.GroupBox1.Controls.Add(Me.PictureBox1)
        Me.GroupBox1.Controls.Add(Me.lblserverinfo)
        Me.GroupBox1.Controls.Add(Me.lblHostInfo)
        Me.GroupBox1.Controls.Add(Me.txtHostSolicitante)
        Me.GroupBox1.Controls.Add(Me.txtServidorLicencias)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(2, 28)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(867, 529)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        '
        'lblServerAPP
        '
        Me.lblServerAPP.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblServerAPP.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.lblServerAPP.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.lblServerAPP.ImageOptions.SvgImage = CType(resources.GetObject("lblServerAPP.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.lblServerAPP.Location = New System.Drawing.Point(283, 85)
        Me.lblServerAPP.Name = "lblServerAPP"
        Me.lblServerAPP.Size = New System.Drawing.Size(269, 44)
        Me.lblServerAPP.TabIndex = 40
        Me.lblServerAPP.Text = "Server:"
        '
        'lblBDAPP
        '
        Me.lblBDAPP.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblBDAPP.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.lblBDAPP.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.lblBDAPP.ImageOptions.Image = CType(resources.GetObject("lblBDAPP.ImageOptions.Image"), System.Drawing.Image)
        Me.lblBDAPP.Location = New System.Drawing.Point(573, 85)
        Me.lblBDAPP.Name = "lblBDAPP"
        Me.lblBDAPP.Size = New System.Drawing.Size(256, 44)
        Me.lblBDAPP.TabIndex = 39
        Me.lblBDAPP.Text = "BD: "
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(292, 318)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(311, 82)
        Me.Label1.TabIndex = 38
        Me.Label1.Text = "Luego de confirmar la solicitud en el servidor de licencias, presione aceptar."
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblMsj
        '
        Me.lblMsj.AutoSize = True
        Me.lblMsj.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMsj.Location = New System.Drawing.Point(41, 44)
        Me.lblMsj.Name = "lblMsj"
        Me.lblMsj.Size = New System.Drawing.Size(669, 25)
        Me.lblMsj.TabIndex = 37
        Me.lblMsj.Text = "No se ha encontrado registro de licencia activa para éste ordenador!"
        '
        'cmdLiberacionLicencia
        '
        Me.cmdLiberacionLicencia.AutoSize = True
        Me.cmdLiberacionLicencia.ImageOptions.Image = CType(resources.GetObject("cmdLiberacionLicencia.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdLiberacionLicencia.Location = New System.Drawing.Point(826, 353)
        Me.cmdLiberacionLicencia.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmdLiberacionLicencia.Name = "cmdLiberacionLicencia"
        Me.cmdLiberacionLicencia.Size = New System.Drawing.Size(38, 36)
        Me.cmdLiberacionLicencia.TabIndex = 36
        '
        'btnAplicar
        '
        Me.btnAplicar.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAplicar.Appearance.Options.UseFont = True
        Me.btnAplicar.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnAplicar.Location = New System.Drawing.Point(3, 462)
        Me.btnAplicar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnAplicar.Name = "btnAplicar"
        Me.btnAplicar.Size = New System.Drawing.Size(861, 63)
        Me.btnAplicar.TabIndex = 35
        Me.btnAplicar.Text = "Aceptar"
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(47, 254)
        Me.PictureBox3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(30, 27)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox3.TabIndex = 33
        Me.PictureBox3.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(47, 144)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(30, 27)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 6
        Me.PictureBox1.TabStop = False
        '
        'lblserverinfo
        '
        Me.lblserverinfo.AutoSize = True
        Me.lblserverinfo.BackColor = System.Drawing.Color.Transparent
        Me.lblserverinfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblserverinfo.ForeColor = System.Drawing.Color.Black
        Me.lblserverinfo.Location = New System.Drawing.Point(80, 223)
        Me.lblserverinfo.Name = "lblserverinfo"
        Me.lblserverinfo.Size = New System.Drawing.Size(489, 25)
        Me.lblserverinfo.TabIndex = 5
        Me.lblserverinfo.Text = "Ha generado una solicitud para el servidor de licencias:"
        '
        'lblHostInfo
        '
        Me.lblHostInfo.AutoSize = True
        Me.lblHostInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHostInfo.Location = New System.Drawing.Point(80, 113)
        Me.lblHostInfo.Name = "lblHostInfo"
        Me.lblHostInfo.Size = New System.Drawing.Size(129, 25)
        Me.lblHostInfo.TabIndex = 2
        Me.lblHostInfo.Text = "El ordenador:"
        '
        'txtHostSolicitante
        '
        Me.txtHostSolicitante.BackColor = System.Drawing.Color.LightSteelBlue
        Me.txtHostSolicitante.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHostSolicitante.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtHostSolicitante.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHostSolicitante.ForeColor = System.Drawing.Color.Black
        Me.txtHostSolicitante.Location = New System.Drawing.Point(80, 144)
        Me.txtHostSolicitante.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtHostSolicitante.Name = "txtHostSolicitante"
        Me.txtHostSolicitante.ReadOnly = True
        Me.txtHostSolicitante.Size = New System.Drawing.Size(763, 30)
        Me.txtHostSolicitante.TabIndex = 0
        Me.txtHostSolicitante.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtServidorLicencias
        '
        Me.txtServidorLicencias.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtServidorLicencias.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtServidorLicencias.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtServidorLicencias.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold)
        Me.txtServidorLicencias.ForeColor = System.Drawing.Color.ForestGreen
        Me.txtServidorLicencias.Location = New System.Drawing.Point(80, 254)
        Me.txtServidorLicencias.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtServidorLicencias.Name = "txtServidorLicencias"
        Me.txtServidorLicencias.ReadOnly = True
        Me.txtServidorLicencias.Size = New System.Drawing.Size(763, 30)
        Me.txtServidorLicencias.TabIndex = 1
        Me.txtServidorLicencias.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'frmLicenciaSolicitada
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(871, 559)
        Me.Controls.Add(Me.GroupControl1)
        Me.Name = "frmLicenciaSolicitada"
        Me.Text = "frmLicenciaSolicitada"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cmdCerrar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents lblServerAPP As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblBDAPP As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Label1 As Label
    Friend WithEvents lblMsj As Label
    Friend WithEvents cmdLiberacionLicencia As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnAplicar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents lblserverinfo As Label
    Friend WithEvents lblHostInfo As Label
    Friend WithEvents txtHostSolicitante As TextBox
    Friend WithEvents txtServidorLicencias As TextBox

End Class
