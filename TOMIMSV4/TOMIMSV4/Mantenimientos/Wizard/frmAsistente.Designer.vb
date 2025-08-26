<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAsistente
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAsistente))
        Me.lblUsuarios = New System.Windows.Forms.Label()
        Me.lblUsuario = New System.Windows.Forms.Label()
        Me.lblConBod = New System.Windows.Forms.Label()
        Me.lblPaso2 = New System.Windows.Forms.Label()
        Me.lblDesc = New System.Windows.Forms.Label()
        Me.lblTitulo = New System.Windows.Forms.Label()
        Me.lblCreaEmp = New System.Windows.Forms.Label()
        Me.lblPaso1 = New System.Windows.Forms.Label()
        Me.cmdSiguiente = New DevExpress.XtraEditors.SimpleButton()
        Me.cmdCancelar = New DevExpress.XtraEditors.SimpleButton()
        Me.picUsuario = New System.Windows.Forms.PictureBox()
        Me.picBodega = New System.Windows.Forms.PictureBox()
        Me.PicEmpresa = New System.Windows.Forms.PictureBox()
        CType(Me.picUsuario, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picBodega, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicEmpresa, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblUsuarios
        '
        Me.lblUsuarios.AutoSize = True
        Me.lblUsuarios.Font = New System.Drawing.Font("Tahoma", 18.0!)
        Me.lblUsuarios.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblUsuarios.Location = New System.Drawing.Point(301, 321)
        Me.lblUsuarios.Name = "lblUsuarios"
        Me.lblUsuarios.Size = New System.Drawing.Size(93, 29)
        Me.lblUsuarios.TabIndex = 7
        Me.lblUsuarios.Text = "Usuario"
        '
        'lblUsuario
        '
        Me.lblUsuario.AutoSize = True
        Me.lblUsuario.Font = New System.Drawing.Font("Tahoma", 18.0!)
        Me.lblUsuario.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblUsuario.Location = New System.Drawing.Point(262, 321)
        Me.lblUsuario.Name = "lblUsuario"
        Me.lblUsuario.Size = New System.Drawing.Size(33, 29)
        Me.lblUsuario.TabIndex = 6
        Me.lblUsuario.Text = "3."
        '
        'lblConBod
        '
        Me.lblConBod.AutoSize = True
        Me.lblConBod.Font = New System.Drawing.Font("Tahoma", 18.0!)
        Me.lblConBod.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblConBod.Location = New System.Drawing.Point(301, 252)
        Me.lblConBod.Name = "lblConBod"
        Me.lblConBod.Size = New System.Drawing.Size(92, 29)
        Me.lblConBod.TabIndex = 5
        Me.lblConBod.Text = "Bodega"
        '
        'lblPaso2
        '
        Me.lblPaso2.AutoSize = True
        Me.lblPaso2.Font = New System.Drawing.Font("Tahoma", 18.0!)
        Me.lblPaso2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblPaso2.Location = New System.Drawing.Point(262, 252)
        Me.lblPaso2.Name = "lblPaso2"
        Me.lblPaso2.Size = New System.Drawing.Size(33, 29)
        Me.lblPaso2.TabIndex = 4
        Me.lblPaso2.Text = "2."
        '
        'lblDesc
        '
        Me.lblDesc.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDesc.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDesc.Location = New System.Drawing.Point(16, 77)
        Me.lblDesc.Name = "lblDesc"
        Me.lblDesc.Size = New System.Drawing.Size(774, 65)
        Me.lblDesc.TabIndex = 1
        Me.lblDesc.Text = resources.GetString("lblDesc.Text")
        '
        'lblTitulo
        '
        Me.lblTitulo.AutoSize = True
        Me.lblTitulo.Font = New System.Drawing.Font("Tahoma", 24.0!)
        Me.lblTitulo.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTitulo.Location = New System.Drawing.Point(12, 23)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(774, 39)
        Me.lblTitulo.TabIndex = 0
        Me.lblTitulo.Text = "Bienvenido al asistente de configuración de TOM-IMS"
        '
        'lblCreaEmp
        '
        Me.lblCreaEmp.AutoSize = True
        Me.lblCreaEmp.Font = New System.Drawing.Font("Tahoma", 18.0!)
        Me.lblCreaEmp.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblCreaEmp.Location = New System.Drawing.Point(301, 184)
        Me.lblCreaEmp.Name = "lblCreaEmp"
        Me.lblCreaEmp.Size = New System.Drawing.Size(117, 29)
        Me.lblCreaEmp.TabIndex = 3
        Me.lblCreaEmp.Text = "Compañia"
        '
        'lblPaso1
        '
        Me.lblPaso1.AutoSize = True
        Me.lblPaso1.Font = New System.Drawing.Font("Tahoma", 18.0!)
        Me.lblPaso1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblPaso1.Location = New System.Drawing.Point(262, 184)
        Me.lblPaso1.Name = "lblPaso1"
        Me.lblPaso1.Size = New System.Drawing.Size(33, 29)
        Me.lblPaso1.TabIndex = 2
        Me.lblPaso1.Text = "1."
        '
        'cmdSiguiente
        '
        Me.cmdSiguiente.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdSiguiente.Appearance.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSiguiente.Appearance.Options.UseFont = True
        Me.cmdSiguiente.Location = New System.Drawing.Point(616, 412)
        Me.cmdSiguiente.Name = "cmdSiguiente"
        Me.cmdSiguiente.Size = New System.Drawing.Size(174, 46)
        Me.cmdSiguiente.TabIndex = 9
        Me.cmdSiguiente.Text = "Siguiente"
        '
        'cmdCancelar
        '
        Me.cmdCancelar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdCancelar.Appearance.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancelar.Appearance.Options.UseFont = True
        Me.cmdCancelar.Location = New System.Drawing.Point(12, 412)
        Me.cmdCancelar.Name = "cmdCancelar"
        Me.cmdCancelar.Size = New System.Drawing.Size(174, 46)
        Me.cmdCancelar.TabIndex = 8
        Me.cmdCancelar.Text = "Cancelar"
        '
        'picUsuario
        '
        Me.picUsuario.Image = Global.TOMWMS.My.Resources.Resources.unchecked
        Me.picUsuario.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.picUsuario.Location = New System.Drawing.Point(496, 310)
        Me.picUsuario.Name = "picUsuario"
        Me.picUsuario.Size = New System.Drawing.Size(51, 45)
        Me.picUsuario.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picUsuario.TabIndex = 24
        Me.picUsuario.TabStop = False
        '
        'picBodega
        '
        Me.picBodega.Image = Global.TOMWMS.My.Resources.Resources.unchecked
        Me.picBodega.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.picBodega.Location = New System.Drawing.Point(496, 241)
        Me.picBodega.Name = "picBodega"
        Me.picBodega.Size = New System.Drawing.Size(51, 45)
        Me.picBodega.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picBodega.TabIndex = 21
        Me.picBodega.TabStop = False
        '
        'PicEmpresa
        '
        Me.PicEmpresa.Image = Global.TOMWMS.My.Resources.Resources.unchecked
        Me.PicEmpresa.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.PicEmpresa.Location = New System.Drawing.Point(496, 173)
        Me.PicEmpresa.Name = "PicEmpresa"
        Me.PicEmpresa.Size = New System.Drawing.Size(51, 45)
        Me.PicEmpresa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicEmpresa.TabIndex = 18
        Me.PicEmpresa.TabStop = False
        '
        'frmAsistente
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(803, 487)
        Me.Controls.Add(Me.cmdCancelar)
        Me.Controls.Add(Me.cmdSiguiente)
        Me.Controls.Add(Me.picUsuario)
        Me.Controls.Add(Me.lblUsuarios)
        Me.Controls.Add(Me.lblUsuario)
        Me.Controls.Add(Me.picBodega)
        Me.Controls.Add(Me.lblConBod)
        Me.Controls.Add(Me.lblPaso2)
        Me.Controls.Add(Me.PicEmpresa)
        Me.Controls.Add(Me.lblDesc)
        Me.Controls.Add(Me.lblTitulo)
        Me.Controls.Add(Me.lblCreaEmp)
        Me.Controls.Add(Me.lblPaso1)
        Me.Name = "frmAsistente"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmAsistente"
        CType(Me.picUsuario, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picBodega, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicEmpresa, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents picUsuario As System.Windows.Forms.PictureBox
    Friend WithEvents lblUsuarios As System.Windows.Forms.Label
    Friend WithEvents lblUsuario As System.Windows.Forms.Label
    Friend WithEvents picBodega As System.Windows.Forms.PictureBox
    Friend WithEvents lblConBod As System.Windows.Forms.Label
    Friend WithEvents lblPaso2 As System.Windows.Forms.Label
    Friend WithEvents PicEmpresa As System.Windows.Forms.PictureBox
    Friend WithEvents lblDesc As System.Windows.Forms.Label
    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents lblCreaEmp As System.Windows.Forms.Label
    Friend WithEvents lblPaso1 As System.Windows.Forms.Label
    Friend WithEvents cmdSiguiente As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cmdCancelar As DevExpress.XtraEditors.SimpleButton



End Class
