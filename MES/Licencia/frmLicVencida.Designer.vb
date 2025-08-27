<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLicVencida
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLicVencida))
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.cmdCerrar = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblFechaServer = New System.Windows.Forms.DateTimePicker()
        Me.lblFechaLicencia = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblSFechaExpiro = New System.Windows.Forms.Label()
        Me.PicSolLIcencia = New DevExpress.XtraEditors.PictureEdit()
        Me.lblMsj = New System.Windows.Forms.Label()
        Me.btnAplicar = New DevExpress.XtraEditors.SimpleButton()
        Me.lblSol = New System.Windows.Forms.Label()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PicSolLIcencia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.cmdCerrar)
        Me.GroupControl1.Controls.Add(Me.GroupBox1)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(767, 620)
        Me.GroupControl1.TabIndex = 7
        Me.GroupControl1.Text = "Licencia inactiva"
        '
        'cmdCerrar
        '
        Me.cmdCerrar.AutoSize = True
        Me.cmdCerrar.ImageOptions.Image = CType(resources.GetObject("cmdCerrar.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdCerrar.Location = New System.Drawing.Point(774, 0)
        Me.cmdCerrar.Name = "cmdCerrar"
        Me.cmdCerrar.Size = New System.Drawing.Size(38, 36)
        Me.cmdCerrar.TabIndex = 34
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblFechaServer)
        Me.GroupBox1.Controls.Add(Me.lblFechaLicencia)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.lblSFechaExpiro)
        Me.GroupBox1.Controls.Add(Me.PicSolLIcencia)
        Me.GroupBox1.Controls.Add(Me.lblMsj)
        Me.GroupBox1.Controls.Add(Me.btnAplicar)
        Me.GroupBox1.Controls.Add(Me.lblSol)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(2, 33)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(763, 585)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        '
        'lblFechaServer
        '
        Me.lblFechaServer.Enabled = False
        Me.lblFechaServer.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFechaServer.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.lblFechaServer.Location = New System.Drawing.Point(510, 190)
        Me.lblFechaServer.Name = "lblFechaServer"
        Me.lblFechaServer.Size = New System.Drawing.Size(200, 32)
        Me.lblFechaServer.TabIndex = 44
        '
        'lblFechaLicencia
        '
        Me.lblFechaLicencia.Enabled = False
        Me.lblFechaLicencia.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFechaLicencia.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.lblFechaLicencia.Location = New System.Drawing.Point(510, 147)
        Me.lblFechaLicencia.Name = "lblFechaLicencia"
        Me.lblFechaLicencia.Size = New System.Drawing.Size(200, 32)
        Me.lblFechaLicencia.TabIndex = 43
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(175, 190)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(288, 29)
        Me.Label2.TabIndex = 42
        Me.Label2.Text = "Fecha actual del servidor:"
        '
        'lblSFechaExpiro
        '
        Me.lblSFechaExpiro.AutoSize = True
        Me.lblSFechaExpiro.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSFechaExpiro.ForeColor = System.Drawing.Color.Black
        Me.lblSFechaExpiro.Location = New System.Drawing.Point(175, 146)
        Me.lblSFechaExpiro.Name = "lblSFechaExpiro"
        Me.lblSFechaExpiro.Size = New System.Drawing.Size(325, 29)
        Me.lblSFechaExpiro.TabIndex = 40
        Me.lblSFechaExpiro.Text = "Fecha expiración de licencia:"
        '
        'PicSolLIcencia
        '
        Me.PicSolLIcencia.EditValue = CType(resources.GetObject("PicSolLIcencia.EditValue"), Object)
        Me.PicSolLIcencia.Location = New System.Drawing.Point(716, 291)
        Me.PicSolLIcencia.Name = "PicSolLIcencia"
        Me.PicSolLIcencia.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.PicSolLIcencia.Properties.Appearance.Options.UseBackColor = True
        Me.PicSolLIcencia.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.[Auto]
        Me.PicSolLIcencia.Size = New System.Drawing.Size(48, 43)
        Me.PicSolLIcencia.TabIndex = 39
        '
        'lblMsj
        '
        Me.lblMsj.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblMsj.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMsj.ForeColor = System.Drawing.Color.Firebrick
        Me.lblMsj.Location = New System.Drawing.Point(3, 19)
        Me.lblMsj.Name = "lblMsj"
        Me.lblMsj.Size = New System.Drawing.Size(757, 51)
        Me.lblMsj.TabIndex = 38
        Me.lblMsj.Text = "La licencia del servidor ha expirado!, " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Debe activar la licencia del servidor pa" &
    "ra ingresar en las terminales"""
        Me.lblMsj.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnAplicar
        '
        Me.btnAplicar.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAplicar.Appearance.Options.UseFont = True
        Me.btnAplicar.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnAplicar.Location = New System.Drawing.Point(3, 531)
        Me.btnAplicar.Name = "btnAplicar"
        Me.btnAplicar.Size = New System.Drawing.Size(757, 51)
        Me.btnAplicar.TabIndex = 35
        Me.btnAplicar.Text = "Aceptar"
        '
        'lblSol
        '
        Me.lblSol.AutoSize = True
        Me.lblSol.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSol.Location = New System.Drawing.Point(87, 306)
        Me.lblSol.Name = "lblSol"
        Me.lblSol.Size = New System.Drawing.Size(585, 20)
        Me.lblSol.TabIndex = 2
        Me.lblSol.Text = "Si desea registrar éste ordenadaor como servidor de licencias haga clic aquí."
        '
        'frmLicVencida
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(767, 620)
        Me.Controls.Add(Me.GroupControl1)
        Me.Name = "frmLicVencida"
        Me.Text = "frmLicVencida"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PicSolLIcencia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cmdCerrar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents lblFechaServer As DateTimePicker
    Friend WithEvents lblFechaLicencia As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents lblSFechaExpiro As Label
    Friend WithEvents PicSolLIcencia As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents lblMsj As Label
    Friend WithEvents btnAplicar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblSol As Label

End Class
