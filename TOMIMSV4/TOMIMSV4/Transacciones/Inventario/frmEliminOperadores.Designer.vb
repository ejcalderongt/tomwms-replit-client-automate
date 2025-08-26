<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEliminOperadores
    Inherits DevExpress.XtraEditors.XtraForm

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
        Me.cmbOperadoresAsignados = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblText = New System.Windows.Forms.Label()
        Me.cmdEliminar = New DevExpress.XtraEditors.SimpleButton()
        Me.cmdAsignarOperador = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.cmbOperadoresAsignados.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmbOperadoresAsignados
        '
        Me.cmbOperadoresAsignados.Location = New System.Drawing.Point(70, 70)
        Me.cmbOperadoresAsignados.Name = "cmbOperadoresAsignados"
        Me.cmbOperadoresAsignados.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbOperadoresAsignados.Properties.NullText = ""
        Me.cmbOperadoresAsignados.Size = New System.Drawing.Size(180, 20)
        Me.cmbOperadoresAsignados.TabIndex = 0
        '
        'lblText
        '
        Me.lblText.AutoSize = True
        Me.lblText.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblText.Location = New System.Drawing.Point(60, 36)
        Me.lblText.Name = "lblText"
        Me.lblText.Size = New System.Drawing.Size(217, 13)
        Me.lblText.TabIndex = 1
        Me.lblText.Text = "Seleccione un operador para eliminar"
        '
        'cmdEliminar
        '
        Me.cmdEliminar.Location = New System.Drawing.Point(70, 111)
        Me.cmdEliminar.Name = "cmdEliminar"
        Me.cmdEliminar.Size = New System.Drawing.Size(180, 23)
        Me.cmdEliminar.TabIndex = 2
        Me.cmdEliminar.Text = "Eliminar"
        '
        'cmdAsignarOperador
        '
        Me.cmdAsignarOperador.Location = New System.Drawing.Point(70, 111)
        Me.cmdAsignarOperador.Name = "cmdAsignarOperador"
        Me.cmdAsignarOperador.Size = New System.Drawing.Size(180, 23)
        Me.cmdAsignarOperador.TabIndex = 3
        Me.cmdAsignarOperador.Text = "Asignar"
        '
        'frmEliminOperadores
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(336, 175)
        Me.Controls.Add(Me.cmdAsignarOperador)
        Me.Controls.Add(Me.cmdEliminar)
        Me.Controls.Add(Me.lblText)
        Me.Controls.Add(Me.cmbOperadoresAsignados)
        Me.Name = "frmEliminOperadores"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Elimación de operadores"
        CType(Me.cmbOperadoresAsignados.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cmbOperadoresAsignados As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblText As Label
    Friend WithEvents cmdEliminar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cmdAsignarOperador As DevExpress.XtraEditors.SimpleButton
End Class
