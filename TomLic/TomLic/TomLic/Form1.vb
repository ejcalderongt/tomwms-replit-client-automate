Imports System.Reflection
Imports DevExpress.XtraEditors

Public Class Form1

    Dim yy, mm, dd, cbo, chh As Integer
    Dim s, ss As String

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub btnLic_Click(sender As Object, e As EventArgs) Handles btnLic.Click
        Dim si, mac As String

        ss = txtSol.Text : txtLic.Text = ""
        If ss = "" Then Return

        Try
            s = DecodeString(ss)
        Catch ex As Exception
            XtraMessageBox.Show("LLave de solicitud incompleta.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
        End Try

        Try
            si = Mid(s, 1, 3) : mac = Mid(s, 4)
            If si <> "LIC" AndAlso si <> "SRV" Then
                XtraMessageBox.Show("LLave de solicitud incorrecta.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If

            si = "LCENC" & mac
            txtLic.Text = EncodeString(si) : txtLic.Focus() : txtLic.SelectAll()

        Catch ex As Exception
            XtraMessageBox.Show("LLave de solicitud incompleta.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
        End Try
    End Sub

    Private Sub btnCon_Click(sender As Object, e As EventArgs) Handles btnCon.Click
        Dim si, mac As String

        ss = txtSol.Text : txtCon.Text = ""
        If ss = "" Then Return

        Try
            s = DecodeString(ss)
        Catch ex As Exception
            XtraMessageBox.Show("LLave de solicitud incompleta.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
        End Try

        Try
            si = Mid(s, 1, 3) : mac = Mid(s, 4)
            If si <> "CON" Then
                XtraMessageBox.Show("LLave de solicitud incorrecta.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If

            si = "CONEX" & mac
            txtCon.Text = EncodeString(si) : txtCon.Focus() : txtCon.SelectAll()
        Catch ex As Exception
            XtraMessageBox.Show("LLave de solicitud incompleta.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
        End Try
    End Sub

    Private Sub cmdInvertirEncripcion_Click(sender As Object, e As EventArgs) Handles cmdInvertirEncripcion.Click

        If txtLlaveDesencriptada.Text.Trim = "" AndAlso txtSol.Text.Trim <> "" Then
            txtLlaveDesencriptada.Text = DecodeString(txtSol.Text)
        Else
            txtSol.Text = EncodeString(txtLlaveDesencriptada.Text)
        End If

    End Sub

    Private Sub btnServ_Click(sender As Object, e As EventArgs) Handles btnServ.Click

        Dim si, mac As String

        ss = txtSol.Text : txtServ.Text = ""

        If ss = "" Then Return

        Try
            s = DecodeString(ss)

        Catch ex As Exception
            XtraMessageBox.Show("LLave de solicitud incompleta.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
        End Try

        Try
            si = Mid(s, 1, 3) : mac = Mid(s, 4)

            If si <> "SRV" Then
                XtraMessageBox.Show("LLave de solicitud incorrecta.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If

            si = String.Format("SERVLIC{0}#", mac)

            If parseData() Then
                si += String.Format(",{0},{1},{2},{3},{4}", cbo, chh, yy, mm, dd)
            End If

            txtServ.Text = EncodeString(si) : txtServ.Focus() : txtServ.SelectAll()

        Catch ex As Exception
            XtraMessageBox.Show("LLave de solicitud incompleta.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnGen.Click

        Try

            txtLlave.Text = ""

            If parseData() Then
                Dim s As String = String.Format("{0},{1},{2},{3},{4}", cbo, chh, yy, mm, dd)
                txtLlave.Text = EncodeString(s)
                txtLlave.SelectAll()
                txtLlave.Focus()
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try


    End Sub

    Private Function parseData() As Boolean

        Try

            cbo = numBO.Value
            chh = numHH.Value

            yy = dtpFecha.Value.Year - 2000
            mm = dtpFecha.Value.Month
            dd = dtpFecha.Value.Day

            Return True

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return False
        End Try

    End Function

    Private Shared Function EncodeString(ByVal origText As String) As String
        Dim stringBytes As Byte() = System.Text.Encoding.Unicode.GetBytes(origText)
        Return Convert.ToBase64String(stringBytes, 0, stringBytes.Length)
    End Function

    Private Shared Function DecodeString(ByVal encodedText As String) As String
        Dim stringBytes As Byte() = Convert.FromBase64String(encodedText)
        Return System.Text.Encoding.Unicode.GetString(stringBytes)
    End Function

End Class
