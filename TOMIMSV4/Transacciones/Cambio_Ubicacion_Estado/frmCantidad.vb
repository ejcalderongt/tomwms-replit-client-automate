Imports DevExpress.XtraEditors

Public Class frmCantidad

    Public Property CantidadMaxima As Double = 0
    Public Property CantidadSugeridaAUbicar As Double = 0

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub frmCantidad_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        txtCantidad.Focus()
    End Sub

    Private Sub frmCantidad_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtCantidad.Focus()

        If CantidadSugeridaAUbicar = 0
            txtCantidad.Value = CantidadMaxima
        Else
            txtCantidad.Value = CantidadSugeridaAUbicar
        End If

    End Sub

    Private Sub cmdAplicarCantidad_Click(sender As Object, e As EventArgs) Handles cmdAplicarCantidad.Click

        If txtCantidad.Value < 1 Then
            XtraMessageBox.Show("La cantidad no puede ser negativa",
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
            txtCantidad.Value = CantidadMaxima
            txtCantidad.Focus()
        ElseIf txtCantidad.Value = 0 Then
            XtraMessageBox.Show("La cantidad debe ser mayor que 0",
Text,
MessageBoxButtons.OK,
MessageBoxIcon.Exclamation)
            txtCantidad.Value = CantidadMaxima
            txtCantidad.Focus()
        ElseIf txtCantidad.Value > CantidadMaxima Then
            XtraMessageBox.Show("La cantidad a ubicar mayor que disponible",
Text,
MessageBoxButtons.OK,
MessageBoxIcon.Exclamation)
            txtCantidad.Value = CantidadMaxima
            txtCantidad.Focus()
        Else
            DialogResult = DialogResult.OK
        End If

    End Sub

    Private Sub txtCantidad_Enter(sender As Object, e As EventArgs) Handles txtCantidad.Enter
        txtCantidad.Select(0, txtCantidad.Text.Length)
    End Sub

    Private Sub txtCantidad_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCantidad.KeyDown
        If e.KeyCode = Keys.Enter AndAlso txtCantidad.Value <> 0 Then
            cmdAplicarCantidad_Click(Nothing, Nothing)
        End If
    End Sub

End Class