Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnPais_region

    Public Shared Function Get_All_Region_Pais() As DataTable

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM VW_Pais_Region "
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function GetAllByIdPais(ByVal IdPais As Integer) As List(Of clsBePais_region)

        Try

            Dim lReturnList As New List(Of clsBePais_region)
            Const sp As String = "SELECT * FROM Pais_region WHERE IdPais = @IdPais"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdPais", IdPais)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBePais_region As New clsBePais_region

            For Each dr As DataRow In dt.Rows

                vBePais_region = New clsBePais_region
                Cargar(vBePais_region, dr)
                lReturnList.Add(vBePais_region)

            Next

            lConnection.Dispose()
            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllForCombo(ByVal IdPais As Integer) As DataTable

        Try

            Const sp As String = "SELECT IdRegion, Nombre FROM Pais_region WHERE IdPais = @IdPais"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdPais", IdPais)
            Dim dt As New DataTable

            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()

            Return dt

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Exists(ByVal pIdRegion As Integer) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM pais_region WHERE IdRegion=@IdRegion"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(vSQL, lConnection)

                    lCommand.CommandType = CommandType.Text
                    lCommand.Parameters.AddWithValue("@IdRegion", pIdRegion)

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

End Class