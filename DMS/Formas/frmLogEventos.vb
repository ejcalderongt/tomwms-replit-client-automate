Imports System.Reflection
Imports DevExpress.Data
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmLogEventos



    Private Sub CargarEventos()
        Try

            Dim lista As New DataTable
            lista = clsLnDMS_Log_sincronizacion_fallos.Listar_By_Error()


            If lista IsNot Nothing AndAlso lista.Rows.Count > 0 Then

                ' Reducir Mensaje_error a 120 caracteres (con …)
                If lista.Columns.Contains("Mensaje_error") Then
                    For Each r As DataRow In lista.Rows
                        Dim s As String = If(r("Mensaje_error"), "").ToString()
                        If s.Length > 120 Then
                            r("Mensaje_error") = s.Substring(0, 120) & "…"
                        End If
                    Next
                End If

                Dgrid.DataSource = lista

                If GridView1.Columns.Count > 0 Then
                    GridView1.OptionsView.ColumnAutoWidth = False
                    GridView1.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Full
                    GridView1.BestFitMaxRowCount = 200

                    GridView1.BeginUpdate()
                    Try
                        GridView1.BestFitColumns()
                    Finally
                        GridView1.EndUpdate()
                    End Try
                End If
            End If


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub frmLogEventos_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            CargarEventos()
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
End Class