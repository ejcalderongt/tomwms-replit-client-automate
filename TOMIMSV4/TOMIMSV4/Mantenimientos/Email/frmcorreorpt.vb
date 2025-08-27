Imports System.IO
Imports System.Net.Mail
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid

Public Class frmcorreorpt

    Public gridReporte As New GridControl
    Private fileName As String = ""


    Private Sub frmcorreorpt_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try
            Dim fecha As String
            Dim dia As String = ""
            Dim mes As String = ""
            Dim anio As String = ""

            anio = Now.Year.ToString()
            mes = Now.Month.ToString()
            dia = Now.Day.ToString()

            fecha = dia & "_" & mes & "_" & anio

            txtFrom.Visible = False
            txtAsunto.EditValue = "Reporte de vencimientos por reglas"
            fileName = "WMS_ReporteVencimiento_" & fecha & ".xlsx"
            txtAdjunto.EditValue = fileName

        Catch ex As Exception

        End Try

    End Sub

    Private Function Cargar_Configuracion() As Boolean

        Cargar_Configuracion = False

        Try
            Dim mail As New MailMessage()
            mail.From = New MailAddress("soportesw@dts.com.gt")
            mail.To.Add(txtTO.EditValue)
            mail.Subject = txtAsunto.EditValue
            mail.Body = txtBody.Text


            gridReporte.ExportToXlsx(fileName)
            Dim path As String = Directory.GetCurrentDirectory() & "\" & fileName

            mail.Attachments.Add(New Attachment(path))

            Dim smtpClient As New SmtpClient("outlook.office365.com")
            smtpClient.Port = 587
            smtpClient.Credentials = New System.Net.NetworkCredential("soportesw@dts.com.gt", "Dts2021#")
            smtpClient.EnableSsl = True

            smtpClient.Send(mail)

            Cargar_Configuracion = True

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
          Text,
          MessageBoxButtons.OK,
          MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Function

    Private Sub cmdEnviar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdEnviar.ItemClick
        If Cargar_Configuracion() Then
            XtraMessageBox.Show("Correo enviado!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub txtAdjunto_Click(sender As Object, e As EventArgs) Handles txtAdjunto.Click
        Try

            gridReporte.ExportToXlsx(fileName)
            Dim path As String = Directory.GetCurrentDirectory() & "\" & fileName
            Process.Start("Excel", path)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
         Text,
         MessageBoxButtons.OK,
         MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub lnkRolOperador_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkRolOperador.LinkClicked

    End Sub
End Class