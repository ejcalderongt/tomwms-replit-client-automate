Imports Sap.Data.Hana

Public NotInheritable Class HanaHelper
    Private Sub New()
    End Sub

    Public Shared Function OpenDT(query As String, conn As HanaConnection) As DataTable
        Dim dt As New DataTable()
        Using cmd As New HanaCommand(query, conn)
            Using adapter As New HanaDataAdapter(cmd)
                adapter.Fill(dt)
            End Using
        End Using
        Return dt
    End Function

    Public Shared Function OpenDB() As HanaConnection
        Dim conn As New HanaConnection(gConnectionStringSAPHana)
        conn.Open()
        Using cmdSchema As New HanaCommand("SET SCHEMA " & BD.Instancia.SAP_COMPANY_DB, conn)
            cmdSchema.ExecuteNonQuery()
        End Using
        Return conn
    End Function

    Public Shared Function Xcute(query As String) As Integer
        Using conn As HanaConnection = OpenDB()
            Using cmd As New HanaCommand(query, conn)
                Return cmd.ExecuteNonQuery()
            End Using
        End Using
    End Function

    Public Shared Function OpenDT(query As String) As DataTable
        Dim dt As New DataTable()

        Using conn As HanaConnection = OpenDB()
            Using cmd As New HanaCommand(query, conn)
                Using adapter As New HanaDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using

        Return dt
    End Function

End Class