<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCantidad
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
        Me.txtCantidad = New System.Windows.Forms.NumericUpDown()
        Me.lblCantMax = New System.Windows.Forms.Label()
        Me.cmdAplicarCantidad = New System.Windows.Forms.Button()
        Me.lblTIngreseCant = New System.Windows.Forms.Label()
        CType(Me.txtCantidad,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'txtCantidad
        '
        Me.txtCantidad.DecimalPlaces = 6
        Me.txtCantidad.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCantidad.Location = New System.Drawing.Point(55, 127)
        Me.txtCantidad.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.Size = New System.Drawing.Size(318, 38)
        Me.txtCantidad.TabIndex = 0
        Me.txtCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblCantMax
        '
        Me.lblCantMax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCantMax.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblCantMax.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCantMax.Location = New System.Drawing.Point(0, 0)
        Me.lblCantMax.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCantMax.Name = "lblCantMax"
        Me.lblCantMax.Size = New System.Drawing.Size(446, 38)
        Me.lblCantMax.TabIndex = 1
        Me.lblCantMax.Text = "Cantidad máxima: 0"
        '
        'cmdAplicarCantidad
        '
        Me.cmdAplicarCantidad.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.cmdAplicarCantidad.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAplicarCantidad.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAplicarCantidad.Location = New System.Drawing.Point(0, 213)
        Me.cmdAplicarCantidad.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdAplicarCantidad.Name = "cmdAplicarCantidad"
        Me.cmdAplicarCantidad.Size = New System.Drawing.Size(446, 37)
        Me.cmdAplicarCantidad.TabIndex = 2
        Me.cmdAplicarCantidad.Text = "Aplicar"
        Me.cmdAplicarCantidad.UseVisualStyleBackColor = True
        '
        'lblTIngreseCant
        '
        Me.lblTIngreseCant.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTIngreseCant.Location = New System.Drawing.Point(16, 74)
        Me.lblTIngreseCant.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTIngreseCant.Name = "lblTIngreseCant"
        Me.lblTIngreseCant.Size = New System.Drawing.Size(357, 38)
        Me.lblTIngreseCant.TabIndex = 3
        Me.lblTIngreseCant.Text = "Ingrese cantidad"
        Me.lblTIngreseCant.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmCantidad
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(446, 250)
        Me.Controls.Add(Me.lblTIngreseCant)
        Me.Controls.Add(Me.cmdAplicarCantidad)
        Me.Controls.Add(Me.lblCantMax)
        Me.Controls.Add(Me.txtCantidad)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = false
        Me.Name = "frmCantidad"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cantidad"
        CType(Me.txtCantidad,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub

    Friend WithEvents txtCantidad As NumericUpDown
    Friend WithEvents lblCantMax As Label
    Friend WithEvents cmdAplicarCantidad As Button
    Friend WithEvents lblTIngreseCant As Label
End Class
