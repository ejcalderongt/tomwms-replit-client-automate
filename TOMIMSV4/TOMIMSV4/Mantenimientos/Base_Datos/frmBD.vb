Imports System.Data.SqlClient
Imports DevExpress.XtraEditors

Public Class frmBD

    Private Sub frmBD_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            Dim dt As DataTable = GetTables()

            dgvTablas.DataSource = dt

            lblTablas.Text = "Tablas: " & dt.Rows.Count

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Function GetTables() As DataTable

        Dim dt As New DataTable

        Try

            vSQL = "SELECT UPPER(name) AS TABLA FROM sys.Tables ORDER BY name "
            BD.OpenDT(dt, vSQL)

            Return dt

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Sub dgvTablas_CellClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles dgvTablas.CellClick

        Try

            If dgvTablas.RowCount = 0 Then Exit Sub

            If e.RowIndex > 0 Then

                Dim vTabla As String = dgvTablas.Rows(e.RowIndex).Cells(0).Value

                txtQuery.Text = "SELECT * FROM " & vTabla

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub cmdXcute_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdXcute.ItemClick

        Dim conn As New SqlConnection(clsBD.Instancia.CadenaConexionSQLClient)
        Dim ltrans As SqlTransaction = Nothing

        Try

            If txtQuery.Text.Trim <> "" Then

                conn.Open()
                conn.FireInfoMessageEventOnUserErrors = True

                ltrans = conn.BeginTransaction()

                vSQL = txtQuery.Text.Trim

                Dim Result As Integer = clsBD.Xcute(vSQL, ltrans, conn)

                ltrans.Commit()

                lblRegistros.Caption = "Registros afectados: " & Result

                MsgBox(lblRegistros.Caption)

            End If

        Catch ex As Exception
            ltrans.Rollback()
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            If Not ltrans Is Nothing Then ltrans.Dispose()
            If conn.State = ConnectionState.Open Then conn.Close()
            conn.Dispose()
        End Try

    End Sub

    Private Sub cmdOpenDT_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdOpendt.ItemClick

        Dim DT As New DataTable

        Try

            If txtQuery.Text.Trim <> "" Then

                BD.OpenDT(DT, txtQuery.Text)

                DgridDatos.DataSource = DT

                lblRegistros.Caption = "Registros: " & DT.Rows.Count

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

End Class