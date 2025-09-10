Imports System.Reflection
Imports System.Text
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
    Dim ux As Integer

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
            '#GT09092025: ux es para licenciar propietarios que exportan data hacia el portal web
            ux = numUx.Value

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

                Dim s As String = String.Format("{0},{1},{2},{3},{4},{5}",
                                                cbo,
                                                chh,
                                                ux,
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
            txtSolicitudCliente.focus()
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
                si += String.Format(",{0},{1},{2},{3},{4},{5}", cbo, chh, ux, yy, mm, dd)
            End If

            txtServ.Text = EncodeString(si) : txtServ.Focus() : txtServ.SelectAll()

        Catch ex As Exception
            XtraMessageBox.Show("LLave de solicitud incompleta.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
        End Try

    End Sub

    Private Shared Function EncodeString(ByVal origText As String) As String
        'Dim stringBytes As Byte() = System.Text.Encoding.Unicode.GetBytes(origText)
        Dim stringBytes As Byte() = System.Text.Encoding.UTF8.GetBytes(origText)
        Return Convert.ToBase64String(stringBytes, 0, stringBytes.Length)

    End Function

    'Private Shared Function DecodeString(ByVal encodedText As String) As String
    '    Dim stringBytes As Byte() = Convert.FromBase64String(encodedText)
    '    'Return System.Text.Encoding.Unicode.GetString(stringBytes)
    '    Return System.Text.Encoding.UTF8.GetString(stringBytes)
    'End Function

    Private Shared Function DecodeString(ByVal encodedText As String) As String
        Dim bytes As Byte() = Convert.FromBase64String(encodedText)

        ' 1) Detectar BOMs
        If bytes.Length >= 2 Then
            ' UTF-16 LE BOM: FF FE
            If bytes(0) = &HFF AndAlso bytes(1) = &HFE Then
                Return Encoding.Unicode.GetString(bytes, 2, bytes.Length - 2)
            End If
            ' UTF-16 BE BOM: FE FF
            If bytes(0) = &HFE AndAlso bytes(1) = &HFF Then
                Return Encoding.BigEndianUnicode.GetString(bytes, 2, bytes.Length - 2)
            End If
        End If
        ' UTF-8 BOM: EF BB BF
        If bytes.Length >= 3 AndAlso bytes(0) = &HEF AndAlso bytes(1) = &HBB AndAlso bytes(2) = &HBF Then
            Return Encoding.UTF8.GetString(bytes, 3, bytes.Length - 3)
        End If

        ' 2) Heurística: ¿parece UTF-16 LE sin BOM? (alto = 0 en bytes impares)
        If (bytes.Length Mod 2 = 0) Then
            Dim looksUtf16Le As Boolean = True
            For i As Integer = 1 To bytes.Length - 1 Step 2
                If bytes(i) <> 0 Then
                    looksUtf16Le = False
                    Exit For
                End If
            Next
            If looksUtf16Le Then
                Return Encoding.Unicode.GetString(bytes)
            End If
        End If

        ' 3) Por defecto: UTF-8 (estricto)
        Dim utf8Strict As New UTF8Encoding(False, True)
        Try
            Return utf8Strict.GetString(bytes)
        Catch ex As DecoderFallbackException
            ' Último recurso: intentar UTF-16 LE
            Return Encoding.Unicode.GetString(bytes)
        End Try
    End Function

End Class