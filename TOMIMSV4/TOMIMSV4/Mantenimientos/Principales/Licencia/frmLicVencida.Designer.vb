<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLicVencida
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLicVencida))
        Me.cmdCerrar = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.lblSol = New System.Windows.Forms.Label()
        Me.btnAplicar = New DevExpress.XtraEditors.SimpleButton()
        Me.lblMsj = New System.Windows.Forms.Label()
        Me.PicSolLIcencia = New DevExpress.XtraEditors.PictureEdit()
        Me.lblSFechaExpiro = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblFechaLicencia = New System.Windows.Forms.DateTimePicker()
        Me.lblFechaServer = New System.Windows.Forms.DateTimePicker()
        CType(Me.GroupControl1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.GroupControl1.SuspendLayout
        CType(Me.PicSolLIcencia.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        Me.GroupBox1.SuspendLayout
        Me.SuspendLayout
        '
        'cmdCerrar
        '
        Me.cmdCerrar.AutoSize = true
        Me.cmdCerrar.Image = CType(resources.GetObject("cmdCerrar.Image"),System.Drawing.Image)
        Me.cmdCerrar.Location = New System.Drawing.Point(774, 0)
        Me.cmdCerrar.Name = "cmdCerrar"
        Me.cmdCerrar.Size = New System.Drawing.Size(42, 38)
        Me.cmdCerrar.TabIndex = 34
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.cmdCerrar)
        Me.GroupControl1.Controls.Add(Me.GroupBox1)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(816, 431)
        Me.GroupControl1.TabIndex = 6
        Me.GroupControl1.Text = "Licencia inactiva"
        '
        'lblSol
        '
        Me.lblSol.AutoSize = true
        Me.lblSol.Font = New System.Drawing.Font("Arial monospaced for SAP", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblSol.Location = New System.Drawing.Point(87, 306)
        Me.lblSol.Name = "lblSol"
        Me.lblSol.Size = New System.Drawing.Size(623, 15)
        Me.lblSol.TabIndex = 2
        Me.lblSol.Text = "Si desea registrar éste ordenadaor como servidor de licencias haga clic aquí."
        '
        'btnAplicar
        '
        Me.btnAplicar.Appearance.Font = New System.Drawing.Font("Tahoma", 12!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnAplicar.Appearance.Options.UseFont = true
        Me.btnAplicar.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnAplicar.Location = New System.Drawing.Point(3, 355)
        Me.btnAplicar.Name = "btnAplicar"
        Me.btnAplicar.Size = New System.Drawing.Size(806, 51)
        Me.btnAplicar.TabIndex = 35
        Me.btnAplicar.Text = "Aceptar"
        '
        'lblMsj
        '
        Me.lblMsj.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblMsj.Font = New System.Drawing.Font("Arial monospaced for SAP", 12!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic),System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMsj.ForeColor = System.Drawing.Color.Firebrick
        Me.lblMsj.Location = New System.Drawing.Point(3, 17)
        Me.lblMsj.Name = "lblMsj"
        Me.lblMsj.Size = New System.Drawing.Size(806, 51)
        Me.lblMsj.TabIndex = 38
        Me.lblMsj.Text = "La licencia del servidor ha expirado!, "&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"Debe activar la licencia del servidor pa"& _ 
    "ra ingresar en las terminales"""
        Me.lblMsj.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PicSolLIcencia
        '
        Me.PicSolLIcencia.EditValue = CType(resources.GetObject("PicSolLIcencia.EditValue"),Object)
        Me.PicSolLIcencia.Location = New System.Drawing.Point(716, 291)
        Me.PicSolLIcencia.Name = "PicSolLIcencia"
        Me.PicSolLIcencia.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(235,Byte),Integer), CType(CType(236,Byte),Integer), CType(CType(239,Byte),Integer))
        Me.PicSolLIcencia.Properties.Appearance.Options.UseBackColor = true
        Me.PicSolLIcencia.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.[Auto]
        Me.PicSolLIcencia.Size = New System.Drawing.Size(48, 43)
        Me.PicSolLIcencia.TabIndex = 39
        '
        'lblSFechaExpiro
        '
        Me.lblSFechaExpiro.AutoSize = true
        Me.lblSFechaExpiro.Font = New System.Drawing.Font("Arial monospaced for SAP", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblSFechaExpiro.ForeColor = System.Drawing.Color.Black
        Me.lblSFechaExpiro.Location = New System.Drawing.Point(175, 146)
        Me.lblSFechaExpiro.Name = "lblSFechaExpiro"
        Me.lblSFechaExpiro.Size = New System.Drawing.Size(329, 22)
        Me.lblSFechaExpiro.TabIndex = 40
        Me.lblSFechaExpiro.Text = "Fecha expiración de licencia:"
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.Font = New System.Drawing.Font("Arial monospaced for SAP", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(175, 190)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(296, 22)
        Me.Label2.TabIndex = 42
        Me.Label2.Text = "Fecha actual del servidor:"
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
        Me.GroupBox1.Location = New System.Drawing.Point(2, 20)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(812, 409)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = false
        '
        'lblFechaLicencia
        '
        Me.lblFechaLicencia.Enabled = false
        Me.lblFechaLicencia.Font = New System.Drawing.Font("Tahoma", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFechaLicencia.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.lblFechaLicencia.Location = New System.Drawing.Point(510, 147)
        Me.lblFechaLicencia.Name = "lblFechaLicencia"
        Me.lblFechaLicencia.Size = New System.Drawing.Size(200, 27)
        Me.lblFechaLicencia.TabIndex = 43
        '
        'lblFechaServer
        '
        Me.lblFechaServer.Enabled = false
        Me.lblFechaServer.Font = New System.Drawing.Font("Tahoma", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFechaServer.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.lblFechaServer.Location = New System.Drawing.Point(510, 190)
        Me.lblFechaServer.Name = "lblFechaServer"
        Me.lblFechaServer.Size = New System.Drawing.Size(200, 27)
        Me.lblFechaServer.TabIndex = 44
        '
        'frmLicVencida
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8!, 16!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(816, 431)
        Me.Controls.Add(Me.GroupControl1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmLicVencida"
        Me.ShowIcon = false
        Me.ShowInTaskbar = false
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Solicitud de Licencia"
        CType(Me.GroupControl1,System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupControl1.ResumeLayout(false)
        Me.GroupControl1.PerformLayout
        CType(Me.PicSolLIcencia.Properties,System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        Me.ResumeLayout(false)

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
