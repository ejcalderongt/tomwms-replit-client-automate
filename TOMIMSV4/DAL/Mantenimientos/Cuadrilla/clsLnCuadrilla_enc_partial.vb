Imports System.Data.SqlClient

Partial Public Class clsLnCuadrilla_enc

    Public Shared Function MaxID(ByVal lConnection As SqlConnection,
                                 ByVal lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdCuadrillaEnc),0) FROM Cuadrilla_enc"

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

    Public Shared Function Guardar(ByVal pBeCuadrillaEnc As clsBeCuadrilla_enc,
                                   ByVal plDetOperadores As List(Of clsBeCuadrilla_det_operador),
                                   ByVal plDetMontaCargas As List(Of clsBeCuadrilla_det_montacarga)) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim Rows As Integer = 0

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            If pBeCuadrillaEnc.IsNew Then
                pBeCuadrillaEnc.IdCuadrillaEnc = MaxID(lConnection, lTransaction) + 1
                Rows = Insertar(pBeCuadrillaEnc, lConnection, lTransaction)
            Else
                Rows = Actualizar(pBeCuadrillaEnc, lConnection, lTransaction)
            End If

            Dim IdCuadrillaDetOperador As Integer = clsLnCuadrilla_det_operador.MaxID(lConnection, lTransaction) + 1

            For Each Det In plDetOperadores

                Det.IdCuadrillaEnc = pBeCuadrillaEnc.IdCuadrillaEnc

                If Det.IsNew Then
                    Det.IdCuadrillaDet = IdCuadrillaDetOperador
                    Rows += clsLnCuadrilla_det_operador.Insertar(Det, lConnection, lTransaction)
                    IdCuadrillaDetOperador += 1
                Else
                    If Det.Activo Then
                        Rows += clsLnCuadrilla_det_operador.Actualizar(Det, lConnection, lTransaction)
                    Else
                        Rows += clsLnCuadrilla_det_operador.Eliminar(Det, lConnection, lTransaction)
                    End If
                End If

            Next

            Dim IdCuadrillaDetMC As Integer = clsLnCuadrilla_det_montacarga.MaxID(lConnection, lTransaction) + 1

            For Each Det In plDetMontaCargas

                Det.IdCuadrillaEnc = pBeCuadrillaEnc.IdCuadrillaEnc

                If Det.IsNew Then
                    Det.IdCuadrillaDetMontaCarga = IdCuadrillaDetMC
                    Rows += clsLnCuadrilla_det_montacarga.Insertar(Det, lConnection, lTransaction)
                    IdCuadrillaDetMC += 1
                Else
                    If Det.Activo Then
                        Rows += clsLnCuadrilla_det_montacarga.Actualizar(Det, lConnection, lTransaction)
                    Else
                        Rows += clsLnCuadrilla_det_montacarga.Eliminar(Det, lConnection, lTransaction)
                    End If
                End If

            Next

            Guardar = Rows

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function


    Public Shared Function Get_All() As DataTable

        Get_All = Nothing

        Dim lReturnList As New List(Of clsBeCuadrilla_enc)

        Try

            Const sp As String = "SELECT * FROM VW_Cuadrilla "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Return lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
