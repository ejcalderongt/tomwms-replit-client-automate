Imports System.Reflection
Imports DevExpress.XtraEditors

Public Class frmLicGen
    Private Sub mnuGenerarLic_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGenerarLic.ItemClick

        Try

            Select Case TabLic.SelectedPageIndex
                Case 0 : Genera_Licencia_Servidor()
                Case 1 : Genera_Licencia_Cliente()
                Case 2 : Genera_Antidoto_Licencia_Conectada()
                Case 3 : Genera_Licencia_Activacion_Server()
            End Select

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0}", ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Dim yy, mm, dd, cbo, chh As Integer
    Dim s, ss As String

    Private Sub TabLic_SelectedPageChanged(sender As Object, e As DevExpress.XtraBars.Navigation.SelectedPageChangedEventArgs) Handles TabLic.SelectedPageChanged

        Select Case TabLic.SelectedPageIndex
            Case 0 : numBO.Focus()
            Case 1 : txtSolicitudCliente.Focus()
            Case 2 : txtSolicitudAntidoto.Focus()
        End Select

    End Sub

    Private Function Parse_Data() As Boolean

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

    Private Sub Genera_Licencia_Servidor()

        Try

            txtLlave.Text = ""

            If Parse_Data() Then

                Dim s As String = String.Format("{0},{1},{2},{3},{4}",
                                                cbo,
                                                chh,
                                                yy,
                                                mm,
                                                dd)
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

    Private Sub Genera_Licencia_Cliente()

        Dim si, mac As String

        ss = txtSolicitudCliente.Text : txtLic.Text = ""

        If txtSolicitudCliente.Text.Trim = "" Then
            XtraMessageBox.Show("Ingrese clave de solicitud del cliente",
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
            txtSolicitudCliente.Focus()
            Exit Sub
        End If

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

    Private Sub Genera_Antidoto_Licencia_Conectada()

        Dim si, mac As String

        ss = txtSolicitudAntidoto.Text : txtCon.Text = ""

        If txtSolicitudAntidoto.Text.Trim = "" Then
            XtraMessageBox.Show("Ingrese clave de solicitud del antídoto",
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
            txtSolicitudAntidoto.Focus()
            Exit Sub
        End If

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

    Private Sub Genera_Licencia_Activacion_Server()

        Dim si, mac As String

        ss = txtSolicitudActivacionServer.Text : txtServ.Text = ""

        If txtSolicitudActivacionServer.Text.Trim = "" Then
            XtraMessageBox.Show("Ingrese clave de solicitud para activación de servidor",
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
            txtSolicitudAntidoto.Focus()
            Exit Sub
        End If

        Try

            s = DecodeString(ss)

            Dim d As String = EncodeString(ss)
            Debug.Write(d)
        Catch ex As Exception
            XtraMessageBox.Show("LLave de solicitud incompleta.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
        End Try

        Try
            si = Mid(s, 1, 3) : mac = Mid(s, 4)

            If si <> "SRV" Then
                XtraMessageBox.Show("LLave de solicitud incorrecta.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If

            si = String.Format("SERVLIC{0}#", mac)

            If Parse_Data() Then
                si += String.Format(",{0},{1},{2},{3},{4}", cbo, chh, yy, mm, dd)
            End If

            txtServ.Text = EncodeString(si) : txtServ.Focus() : txtServ.SelectAll()

        Catch ex As Exception
            XtraMessageBox.Show("LLave de solicitud incompleta.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
        End Try

    End Sub

    Private Shared Function EncodeString(ByVal origText As String) As String
        Dim stringBytes As Byte() = System.Text.Encoding.Unicode.GetBytes(origText)
        Return Convert.ToBase64String(stringBytes, 0, stringBytes.Length)
    End Function

    Private Shared Function DecodeString(ByVal encodedText As String) As String
        Dim stringBytes As Byte() = Convert.FromBase64String(encodedText)
        Return System.Text.Encoding.Unicode.GetString(stringBytes)
    End Function

End Class