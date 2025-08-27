Imports System.Data.SqlClient

Partial Public Class clsLnTipo_tarima

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT ISNULL(Max(IdTipoTarima),0) FROM tipo_tarima"

                Using lCommand As New SqlCommand(vSQL, lConnection) With {.CommandType = CommandType.Text}

                    lConnection.Open()

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()

                    lConnection.Close()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CInt(lReturnValue) + 1
                    End If

                End Using

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Listar(ByVal pActivo As Boolean) As DataTable

        Try

            Dim sp As String = "SELECT IdTipoTarima AS Código, Nombre FROM tipo_tarima WHERE 1 > 0 "

            If pActivo Then
                sp += " AND activo=1"
            Else
                sp += " AND activo=0"
            End If

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll(ByVal pActivo As Boolean) As List(Of clsBeTipo_tarima)

        Dim Lista As New List(Of clsBeTipo_tarima)

        Dim vSQL As String = "SELECT * FROM tipo_tarima WHERE activo=@activo"

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                '#HS 08112017 Quité query dentro de SqlDataAdapter.
                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    lDTA.SelectCommand.Parameters.AddWithValue("@activo", pActivo)

                    Dim lDT As New DataTable()
                    lDTA.Fill(lDT)

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        Dim Obj As clsBeTipo_tarima

                        For Each lRow As DataRow In lDT.Rows

                            Obj = New clsBeTipo_tarima

                            Obj.IdTipoTarima = CType(lRow("IdTipoTarima"), Int32)

                            If lRow("Nombre") IsNot DBNull.Value AndAlso lRow("Nombre") IsNot Nothing Then

                                Obj.Nombre = CType(lRow("Nombre"), String)

                            End If

                            Lista.Add(Obj)

                        Next

                    End If

                End Using

            End Using

            Return Lista

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllForCombo(ByVal pActivo As Boolean) As DataTable

        Try

            Const sp As String = "SELECT IdTipoTarima,Nombre FROM tipo_tarima WHERE activo=@activo "
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@Activo", pActivo)
            Dim dt As New DataTable

            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()

            Return dt

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
