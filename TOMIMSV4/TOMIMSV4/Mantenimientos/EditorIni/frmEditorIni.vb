Imports System.IO
Imports DevExpress.XtraEditors

Public Class frmEditorIni

    Private vRutaIni As String = CurDir() & "\conn.ini"

    Private Sub frmEditorIni_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            txtEditor.LoadDocument(vRutaIni)

        Catch ex As Exception
            Debug.Write("Error " & ex.Message)
        End Try

    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick


        Try

            txtEditor.SaveDocument(vRutaIni, DevExpress.XtraRichEdit.DocumentFormat.PlainText)
            Dim fi As New FileInfo(vRutaIni)

            If XtraMessageBox.Show(String.Format("Se guardó el archivo, desea reiniciar el WMS?", "Conn.ini"),
                Text,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) = DialogResult.Yes Then

                Process.Start(Application.ExecutablePath)

                Application.Exit()

            End If


        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

        End Try

    End Sub

End Class