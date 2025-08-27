<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.btnGen = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.numHH = New System.Windows.Forms.NumericUpDown()
        Me.numBO = New System.Windows.Forms.NumericUpDown()
        Me.dtpFecha = New System.Windows.Forms.DateTimePicker()
        Me.txtLlave = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnServ = New System.Windows.Forms.Button()
        Me.btnCon = New System.Windows.Forms.Button()
        Me.btnLic = New System.Windows.Forms.Button()
        Me.txtServ = New System.Windows.Forms.TextBox()
        Me.txtCon = New System.Windows.Forms.TextBox()
        Me.txtLic = New System.Windows.Forms.TextBox()
        Me.txtSol = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtLlaveDesencriptada = New System.Windows.Forms.TextBox()
        Me.cmdInvertirEncripcion = New System.Windows.Forms.Button()
        CType(Me.numHH,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.numBO,System.ComponentModel.ISupportInitialize).BeginInit
        Me.GroupBox1.SuspendLayout
        Me.GroupBox2.SuspendLayout
        Me.SuspendLayout
        '
        'btnGen
        '
        Me.btnGen.Location = New System.Drawing.Point(718, 61)
        Me.btnGen.Name = "btnGen"
        Me.btnGen.Size = New System.Drawing.Size(93, 31)
        Me.btnGen.TabIndex = 6
        Me.btnGen.Text = "Generar"
        Me.btnGen.UseVisualStyleBackColor = true
        '
        'Label3
        '
        Me.Label3.AutoSize = true
        Me.Label3.Location = New System.Drawing.Point(344, 34)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(47, 16)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Vence"
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.Location = New System.Drawing.Point(191, 34)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 16)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Hand held"
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(22, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 16)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Back Office"
        '
        'numHH
        '
        Me.numHH.Location = New System.Drawing.Point(267, 32)
        Me.numHH.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numHH.Name = "numHH"
        Me.numHH.Size = New System.Drawing.Size(54, 22)
        Me.numHH.TabIndex = 2
        Me.numHH.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'numBO
        '
        Me.numBO.Location = New System.Drawing.Point(115, 30)
        Me.numBO.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numBO.Name = "numBO"
        Me.numBO.Size = New System.Drawing.Size(53, 22)
        Me.numBO.TabIndex = 1
        Me.numBO.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'dtpFecha
        '
        Me.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFecha.Location = New System.Drawing.Point(409, 32)
        Me.dtpFecha.Name = "dtpFecha"
        Me.dtpFecha.Size = New System.Drawing.Size(120, 22)
        Me.dtpFecha.TabIndex = 0
        '
        'txtLlave
        '
        Me.txtLlave.Location = New System.Drawing.Point(25, 70)
        Me.txtLlave.Name = "txtLlave"
        Me.txtLlave.Size = New System.Drawing.Size(671, 22)
        Me.txtLlave.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtLlave)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.btnGen)
        Me.GroupBox1.Controls.Add(Me.dtpFecha)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.numBO)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.numHH)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(829, 113)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = false
        Me.GroupBox1.Text = " Licencia general "
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cmdInvertirEncripcion)
        Me.GroupBox2.Controls.Add(Me.txtLlaveDesencriptada)
        Me.GroupBox2.Controls.Add(Me.btnServ)
        Me.GroupBox2.Controls.Add(Me.btnCon)
        Me.GroupBox2.Controls.Add(Me.btnLic)
        Me.GroupBox2.Controls.Add(Me.txtServ)
        Me.GroupBox2.Controls.Add(Me.txtCon)
        Me.GroupBox2.Controls.Add(Me.txtLic)
        Me.GroupBox2.Controls.Add(Me.txtSol)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(12, 120)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(829, 301)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = false
        Me.GroupBox2.Text = " Solicitud individual "
        '
        'btnServ
        '
        Me.btnServ.Location = New System.Drawing.Point(714, 245)
        Me.btnServ.Name = "btnServ"
        Me.btnServ.Size = New System.Drawing.Size(93, 31)
        Me.btnServ.TabIndex = 10
        Me.btnServ.Text = "Generar"
        Me.btnServ.UseVisualStyleBackColor = true
        '
        'btnCon
        '
        Me.btnCon.Location = New System.Drawing.Point(714, 190)
        Me.btnCon.Name = "btnCon"
        Me.btnCon.Size = New System.Drawing.Size(93, 31)
        Me.btnCon.TabIndex = 9
        Me.btnCon.Text = "Generar"
        Me.btnCon.UseVisualStyleBackColor = true
        '
        'btnLic
        '
        Me.btnLic.Location = New System.Drawing.Point(715, 134)
        Me.btnLic.Name = "btnLic"
        Me.btnLic.Size = New System.Drawing.Size(93, 31)
        Me.btnLic.TabIndex = 8
        Me.btnLic.Text = "Generar"
        Me.btnLic.UseVisualStyleBackColor = true
        '
        'txtServ
        '
        Me.txtServ.Location = New System.Drawing.Point(25, 254)
        Me.txtServ.Name = "txtServ"
        Me.txtServ.Size = New System.Drawing.Size(671, 22)
        Me.txtServ.TabIndex = 7
        '
        'txtCon
        '
        Me.txtCon.Location = New System.Drawing.Point(25, 199)
        Me.txtCon.Name = "txtCon"
        Me.txtCon.Size = New System.Drawing.Size(671, 22)
        Me.txtCon.TabIndex = 6
        '
        'txtLic
        '
        Me.txtLic.Location = New System.Drawing.Point(28, 143)
        Me.txtLic.Name = "txtLic"
        Me.txtLic.Size = New System.Drawing.Size(668, 22)
        Me.txtLic.TabIndex = 5
        '
        'txtSol
        '
        Me.txtSol.Location = New System.Drawing.Point(28, 51)
        Me.txtSol.Name = "txtSol"
        Me.txtSol.Size = New System.Drawing.Size(783, 22)
        Me.txtSol.TabIndex = 4
        '
        'Label7
        '
        Me.Label7.AutoSize = true
        Me.Label7.Location = New System.Drawing.Point(25, 31)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(112, 16)
        Me.Label7.TabIndex = 3
        Me.Label7.Text = "Llave de solicitud"
        '
        'Label6
        '
        Me.Label6.AutoSize = true
        Me.Label6.Location = New System.Drawing.Point(25, 235)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(160, 16)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = " Activar bandera Servidor"
        '
        'Label5
        '
        Me.Label5.AutoSize = true
        Me.Label5.Location = New System.Drawing.Point(25, 179)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(181, 16)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Limpiar bandera Conectado  "
        '
        'Label4
        '
        Me.Label4.AutoSize = true
        Me.Label4.Location = New System.Drawing.Point(25, 123)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(105, 16)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Activar Licencia "
        '
        'txtLlaveDesencriptada
        '
        Me.txtLlaveDesencriptada.Location = New System.Drawing.Point(28, 79)
        Me.txtLlaveDesencriptada.Name = "txtLlaveDesencriptada"
        Me.txtLlaveDesencriptada.Size = New System.Drawing.Size(783, 22)
        Me.txtLlaveDesencriptada.TabIndex = 11
        '
        'cmdInvertirEncripcion
        '
        Me.cmdInvertirEncripcion.Location = New System.Drawing.Point(718, 22)
        Me.cmdInvertirEncripcion.Name = "cmdInvertirEncripcion"
        Me.cmdInvertirEncripcion.Size = New System.Drawing.Size(75, 23)
        Me.cmdInvertirEncripcion.TabIndex = 12
        Me.cmdInvertirEncripcion.Text = "Invertir Encrip"
        Me.cmdInvertirEncripcion.UseVisualStyleBackColor = true
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(853, 433)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.MaximizeBox = false
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TOM IMS Lic"
        CType(Me.numHH,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.numBO,System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        Me.GroupBox2.ResumeLayout(false)
        Me.GroupBox2.PerformLayout
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents btnGen As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents numHH As NumericUpDown
    Friend WithEvents numBO As NumericUpDown
    Friend WithEvents dtpFecha As DateTimePicker
    Friend WithEvents txtLlave As TextBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents txtServ As TextBox
    Friend WithEvents txtCon As TextBox
    Friend WithEvents txtLic As TextBox
    Friend WithEvents txtSol As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents btnServ As Button
    Friend WithEvents btnCon As Button
    Friend WithEvents btnLic As Button
    Friend WithEvents cmdInvertirEncripcion As Button
    Friend WithEvents txtLlaveDesencriptada As TextBox
End Class
