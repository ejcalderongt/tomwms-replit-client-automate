Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnTrans_ubic_tarima

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdTarimaTareaUbic),0) FROM trans_ubic_tarima"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lMax = CInt(lReturnValue)
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdTarimaTareaUbic),0) FROM trans_ubic_tarima"

            Using lCommand As New SqlCommand(vSQL, pConnection)

                lCommand.CommandType = CommandType.Text
                lCommand.Transaction = pTransaction

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


    Public Shared Function GetAllByIdEnc(ByVal pIdTarimaTareaEnc As Integer) As List(Of clsBeTrans_ubic_tarima)

        Dim lReturnList As New List(Of clsBeTrans_ubic_tarima)

        Try

            Dim vSQL As String = "Select * from VW_TarimasUsadasEnTransaccion Where IdTarimaTareaUbic=@IdTarimaTareaUbic "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdTarimaTareaUbic", pIdTarimaTareaEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeTrans_ubic_tarima

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeTrans_ubic_tarima

                                Cargar(Obj, lRow)

                                If lRow("NombreTipoTarima") IsNot DBNull.Value AndAlso lRow("NombreTipoTarima") IsNot Nothing Then
                                    Obj.NombreTipoTarima = CType(lRow("NombreTipoTarima"), String)
                                End If

                                lReturnList.Add(Obj)

                            Next

                        End If

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

    Public Shared Function Eliminar_By_IdTarima(ByRef oBeTrans_ubic_tarima As clsBeTrans_ubic_tarima,
                                              ByVal pConection As SqlConnection,
                                              ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim sp As String = " Delete from Trans_ubic_tarima" &
             "  Where(IdTarima = @IdTarima)"

            Dim cmd As New SqlCommand(sp, pConection, pTransaction) With {.CommandType = CommandType.Text}

            cmd.Parameters.Add(New SqlParameter("@IdTarima", oBeTrans_ubic_tarima.IdTarima))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            Return rowsAffected

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub Guardar_Tarimas_Usadas(ByVal IdTareaUbicacionEnc As Integer,
                                             ByVal pListObjTransUbicTarimasUsadas As List(Of clsBeTrans_ubic_tarima),
                                             ByRef lConnection As SqlConnection,
                                             ByRef lTransaction As SqlTransaction)

        Try

            Dim lMaxTransTarima As Integer = MaxID(lConnection, lTransaction)

            If pListObjTransUbicTarimasUsadas IsNot Nothing AndAlso pListObjTransUbicTarimasUsadas.Count > 0 Then

                For Each Obj As clsBeTrans_ubic_tarima In pListObjTransUbicTarimasUsadas

                    Dim tarima As New clsBeTarimas() With {.IdTarima = Obj.IdTarima, .Disponible = False}

                    Eliminar_By_IdTarima(Obj, lConnection, lTransaction)

                    lMaxTransTarima += 1
                    Obj.IdTarimaTareaUbic = lMaxTransTarima
                    Obj.IdTareaUbicacionEnc = IdTareaUbicacionEnc

                    clsLnTarimas.ActualizarEstado(tarima, lConnection, lTransaction)

                    Insertar(Obj, lConnection, lTransaction)

                Next

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Sub Guardar_Tarimas_Disponibles(ByVal pListObjTransUbicTarimaDisponibles As List(Of clsBeTrans_ubic_tarima),
                                                  ByRef lConnection As SqlConnection,
                                                  ByRef lTransaction As SqlTransaction)

        Try

            If pListObjTransUbicTarimaDisponibles IsNot Nothing AndAlso pListObjTransUbicTarimaDisponibles.Count > 0 Then

                For Each Obj As clsBeTrans_ubic_tarima In pListObjTransUbicTarimaDisponibles
                    Dim tarima As New clsBeTarimas() With {.IdTarima = Obj.IdTarima, .Disponible = True}
                    clsLnTarimas.ActualizarEstado(tarima, lConnection, lTransaction)
                    Eliminar_By_IdTarima(Obj, lConnection, lTransaction)
                Next

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

End Class
