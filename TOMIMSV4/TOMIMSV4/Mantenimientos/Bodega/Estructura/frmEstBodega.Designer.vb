<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmEstBodega
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEstBodega))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnTramos = New DevExpress.XtraEditors.SimpleButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboSector = New System.Windows.Forms.ComboBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.sbEstTramo = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCopiar = New DevExpress.XtraEditors.SimpleButton()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboTramo2 = New System.Windows.Forms.ComboBox()
        Me.btnUbic = New DevExpress.XtraEditors.SimpleButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboTramo = New System.Windows.Forms.ComboBox()
        Me.btnAplicar = New DevExpress.XtraEditors.SimpleButton()
        Me.btnValidar = New DevExpress.XtraEditors.SimpleButton()
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.sbEstSector = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.sbEstSector)
        Me.GroupBox1.Controls.Add(Me.btnTramos)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cboSector)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(648, 81)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Creación y configuración de tramos "
        '
        'btnTramos
        '
        Me.btnTramos.ImageOptions.Image = CType(resources.GetObject("btnTramos.ImageOptions.Image"), System.Drawing.Image)
        Me.btnTramos.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleRight
        Me.btnTramos.Location = New System.Drawing.Point(296, 25)
        Me.btnTramos.Name = "btnTramos"
        Me.btnTramos.Size = New System.Drawing.Size(169, 39)
        Me.btnTramos.TabIndex = 2
        Me.btnTramos.Text = "Modificar"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Sector "
        '
        'cboSector
        '
        Me.cboSector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSector.FormattingEnabled = True
        Me.cboSector.Location = New System.Drawing.Point(98, 31)
        Me.cboSector.Name = "cboSector"
        Me.cboSector.Size = New System.Drawing.Size(192, 28)
        Me.cboSector.TabIndex = 1
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.sbEstTramo)
        Me.GroupBox2.Controls.Add(Me.GroupBox3)
        Me.GroupBox2.Controls.Add(Me.btnUbic)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.cboTramo)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox2.Location = New System.Drawing.Point(0, 81)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(648, 179)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Creación y configuración de Ubicaciones "
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnCopiar)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.cboTramo2)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox3.Location = New System.Drawing.Point(3, 100)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(642, 76)
        Me.GroupBox3.TabIndex = 3
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "  Copiar estructura "
        '
        'sbEstTramo
        '
        Me.sbEstTramo.ImageOptions.Image = CType(resources.GetObject("sbEstTramo.ImageOptions.Image"), System.Drawing.Image)
        Me.sbEstTramo.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleRight
        Me.sbEstTramo.Location = New System.Drawing.Point(470, 47)
        Me.sbEstTramo.Name = "sbEstTramo"
        Me.sbEstTramo.Size = New System.Drawing.Size(169, 40)
        Me.sbEstTramo.TabIndex = 7
        Me.sbEstTramo.Text = "Tramos Estructura"
        '
        'btnCopiar
        '
        Me.btnCopiar.ImageOptions.Image = CType(resources.GetObject("btnCopiar.ImageOptions.Image"), System.Drawing.Image)
        Me.btnCopiar.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleRight
        Me.btnCopiar.Location = New System.Drawing.Point(293, 28)
        Me.btnCopiar.Name = "btnCopiar"
        Me.btnCopiar.Size = New System.Drawing.Size(169, 40)
        Me.btnCopiar.TabIndex = 2
        Me.btnCopiar.Text = "Copiar"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(17, 37)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 20)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Al tramo"
        '
        'cboTramo2
        '
        Me.cboTramo2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTramo2.FormattingEnabled = True
        Me.cboTramo2.Location = New System.Drawing.Point(95, 34)
        Me.cboTramo2.Name = "cboTramo2"
        Me.cboTramo2.Size = New System.Drawing.Size(191, 28)
        Me.cboTramo2.TabIndex = 1
        '
        'btnUbic
        '
        Me.btnUbic.ImageOptions.Image = CType(resources.GetObject("btnUbic.ImageOptions.Image"), System.Drawing.Image)
        Me.btnUbic.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleRight
        Me.btnUbic.Location = New System.Drawing.Point(295, 47)
        Me.btnUbic.Name = "btnUbic"
        Me.btnUbic.Size = New System.Drawing.Size(169, 40)
        Me.btnUbic.TabIndex = 2
        Me.btnUbic.Text = "Modificar"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 20)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Tramo"
        '
        'cboTramo
        '
        Me.cboTramo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTramo.FormattingEnabled = True
        Me.cboTramo.Location = New System.Drawing.Point(98, 53)
        Me.cboTramo.Name = "cboTramo"
        Me.cboTramo.Size = New System.Drawing.Size(192, 28)
        Me.cboTramo.TabIndex = 1
        '
        'btnAplicar
        '
        Me.btnAplicar.ImageOptions.Image = CType(resources.GetObject("btnAplicar.ImageOptions.Image"), System.Drawing.Image)
        Me.btnAplicar.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter
        Me.btnAplicar.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopRight
        Me.btnAplicar.Location = New System.Drawing.Point(390, 275)
        Me.btnAplicar.Name = "btnAplicar"
        Me.btnAplicar.Size = New System.Drawing.Size(169, 38)
        Me.btnAplicar.TabIndex = 3
        Me.btnAplicar.Text = "  Generar estructura     "
        '
        'btnValidar
        '
        Me.btnValidar.ImageOptions.Image = CType(resources.GetObject("btnValidar.ImageOptions.Image"), System.Drawing.Image)
        Me.btnValidar.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter
        Me.btnValidar.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft
        Me.btnValidar.Location = New System.Drawing.Point(81, 275)
        Me.btnValidar.Name = "btnValidar"
        Me.btnValidar.Size = New System.Drawing.Size(173, 38)
        Me.btnValidar.TabIndex = 2
        Me.btnValidar.Text = "Validar estructura     "
        '
        'SimpleButton1
        '
        Me.SimpleButton1.ImageOptions.Image = CType(resources.GetObject("SimpleButton1.ImageOptions.Image"), System.Drawing.Image)
        Me.SimpleButton1.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter
        Me.SimpleButton1.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopRight
        Me.SimpleButton1.Location = New System.Drawing.Point(260, 275)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(124, 38)
        Me.SimpleButton1.TabIndex = 4
        Me.SimpleButton1.Text = "Diseño   "
        '
        'sbEstSector
        '
        Me.sbEstSector.ImageOptions.Image = CType(resources.GetObject("SimpleButton2.ImageOptions.Image"), System.Drawing.Image)
        Me.sbEstSector.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleRight
        Me.sbEstSector.Location = New System.Drawing.Point(471, 24)
        Me.sbEstSector.Name = "sbEstSector"
        Me.sbEstSector.Size = New System.Drawing.Size(169, 40)
        Me.sbEstSector.TabIndex = 8
        Me.sbEstSector.Text = "Sector Estructura"
        '
        'frmEstBodega
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(648, 329)
        Me.Controls.Add(Me.SimpleButton1)
        Me.Controls.Add(Me.btnValidar)
        Me.Controls.Add(Me.btnAplicar)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEstBodega"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Estructura de Bodega"
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        Me.GroupBox2.ResumeLayout(false)
        Me.GroupBox2.PerformLayout
        Me.GroupBox3.ResumeLayout(false)
        Me.GroupBox3.PerformLayout
        Me.ResumeLayout(false)

End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents cboSector As ComboBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents btnAplicar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnTramos As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnUbic As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label2 As Label
    Friend WithEvents cboTramo As ComboBox
    Friend WithEvents btnValidar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents btnCopiar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label3 As Label
    Friend WithEvents cboTramo2 As ComboBox
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents sbEstTramo As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents sbEstSector As DevExpress.XtraEditors.SimpleButton
End Class
