Imports System.Data.SqlClient

Partial Public Class clsLnCuadrilla_det_montacarga

    Public Shared Function Get_All_By_IdCuadrillaEnc(ByVal pIdCuadrillaEnc As Integer) As List(Of clsBeCuadrilla_det_montacarga)

        Dim lReturnList As New List(Of clsBeCuadrilla_det_montacarga)

        Try

            Const sp As String = "SELECT * FROM Cuadrilla_det_montacarga WHERE IdCuadrillaEnc = @IdCuadrillaEnc"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdCuadrillaEnc", pIdCuadrillaEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeCuadrilla_det_montacarga As New clsBeCuadrilla_det_montacarga

                        For Each dr As DataRow In lDataTable.Rows
                            vBeCuadrilla_det_montacarga = New clsBeCuadrilla_det_montacarga
                            Cargar(vBeCuadrilla_det_montacarga, dr)
                            lReturnList.Add(vBeCuadrilla_det_montacarga)
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdCuadrillaEnc(ByVal pIdCuadrillaEnc As Integer,
                                                     ByVal lConnection As SqlConnection,
                                                     ByVal lTransaction As SqlTransaction) As List(Of clsBeCuadrilla_det_montacarga)

        Dim lReturnList As New List(Of clsBeCuadrilla_det_montacarga)

        Try

            Const sp As String = "SELECT * FROM Cuadrilla_det_montacarga WHERE IdCuadrillaEnc = @IdCuadrillaEnc"

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdCuadrillaEnc", pIdCuadrillaEnc)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeCuadrilla_det_montacarga As New clsBeCuadrilla_det_montacarga

                For Each dr As DataRow In lDataTable.Rows
                    vBeCuadrilla_det_montacarga = New clsBeCuadrilla_det_montacarga
                    Cargar(vBeCuadrilla_det_montacarga, dr)
                    vBeCuadrilla_det_montacarga.IsNew = False
                    lReturnList.Add(vBeCuadrilla_det_montacarga)
                Next

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Eliminar_By_IdMontacargaBodega_And_IdCuadrillaEnc(ByVal pIdMontaCargaBodega As Integer,
                                                                           ByVal pIdCuadrillaEnc As Integer,
                                                                           Optional ByVal pConection As SqlConnection = Nothing,
                                                                           Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from cuadrilla_det_montacarga 
								   Where(IdMontacargaBodega = @IdMontacargaBodega 
								   AND IdCuadrillaEnc = @IdCuadrillaEnc) "

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMONTACARGABODEGA", pIdMontaCargaBodega))
            cmd.Parameters.Add(New SqlParameter("@IDCUADRILLAENC", pIdCuadrillaEnc))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function MaxID(ByVal lConnection As SqlConnection,
                                 ByVal lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdCuadrillaDetMontaCarga),0) FROM Cuadrilla_det_montacarga"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
