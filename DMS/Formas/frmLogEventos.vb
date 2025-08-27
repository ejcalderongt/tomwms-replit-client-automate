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
                Dgrid.DataSource = lista
                'lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)
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