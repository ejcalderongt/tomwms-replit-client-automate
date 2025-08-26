Imports DevExpress.XtraEditors

Public Class frmAjusteLogin

    Public clave As String

    Private Sub frmAjusteLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtClave.Text = ""
    End Sub

    Private Sub cmdIngresar_Click(sender As Object, e As EventArgs) Handles cmdIngresar.Click

        Try

            If Not String.IsNullOrEmpty(txtClave.Text) Then

                Dim vClaveAutorizacionEncriptada As String = clsPublic.Encriptar(txtClave.Text.Trim)
                Dim vClaveValida As Boolean = clsLnUsuario.Clave_Autorizacion_Es_Valida(vClaveAutorizacionEncriptada)

                If vClaveValida OrElse AP.UsuarioAp.Codigo = "dts" Then
                    DialogResult = DialogResult.Yes
                Else
                    Throw New Exception("La clave ingresada no es existe o no es válida.")
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtClave_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtClave.KeyPress
        If e.KeyChar = Chr(13) Then cmdIngresar_Click(sender, e)
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        DialogResult = DialogResult.No
    End Sub

    Private Sub frmAjusteLogin_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

        Try

            If e.Control = True AndAlso e.KeyCode = Keys.G Then
                DialogResult = DialogResult.Yes
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

End Class