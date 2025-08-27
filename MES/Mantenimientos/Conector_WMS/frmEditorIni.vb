Imports System.IO
Imports System.Reflection
Imports DevExpress.XtraEditors

Public Class frmEditorIni

    Dim AppPath As String = CurDir() & "\"
    Private vRutaIni As String = AppPath & "conn.ini"

    Private Sub frmEditorIni_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            txtEditor.LoadDocument(vRutaIni)

        Catch ex As Exception
            Debug.Write("Error " & ex.Message)
        End Try

    End Sub

    Private Sub cmdGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdGuardar.ItemClick


        Try

            txtEditor.SaveDocument(vRutaIni, DevExpress.XtraRichEdit.DocumentFormat.PlainText)
            Dim fi As New FileInfo(vRutaIni)

            If XtraMessageBox.Show(String.Format("Se guardó el archivo, desea reiniciar QuickTag?", "Conn.ini"),
                Text,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) = DialogResult.Yes Then

                DialogResult = DialogResult.OK

                Process.Start(Application.ExecutablePath)

                Application.Exit()

            End If


        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

        End Try

    End Sub

End Class