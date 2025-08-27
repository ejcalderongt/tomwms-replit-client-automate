Imports System.Data.SqlClient

Partial Public Class clsLnSimbologias_codigo_barra

    Public Shared Function Exists(ByVal pNombre As String) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM simbologias_codigo_barra WHERE Nombre=@Nombre"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                '#HS 07112017 Quité query dentro de Sqlcommand.
                Using lCommand As New SqlCommand(vSQL, lConnection)

                    lCommand.CommandType = CommandType.Text
                    lCommand.Parameters.AddWithValue("@Nombre", pNombre)

                    lConnection.Open()

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lExists = CInt(lReturnValue) > 0
                    End If

                End Using

            End Using

            Return lExists

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Exists(ByVal pNombre As String,
                                  ByVal lConnection As SqlConnection,
                                  ByVal lTransaction As SqlTransaction) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM simbologias_codigo_barra WHERE Nombre=@Nombre"

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@Nombre", pNombre)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lExists = CInt(lReturnValue) > 0
                End If

            End Using

            Return lExists

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetIdSimbologiaByNombre(ByVal pNombre As Integer) As Integer

        Try

            Dim vSQL As String = "SELECT IdSimbologia FROM simbologias_codigo_barra WHERE Nombre=@Nombre"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                '#HS 08112017 Quité query dentro de SqlDataAdapter.
                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    lDTA.SelectCommand.Parameters.AddWithValue("@Nombre", pNombre)

                    Dim lDT As New DataTable()

                    lDTA.Fill(lDT)

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        Return lDT(0)(0)

                    End If

                End Using

            End Using

            Return Nothing

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
