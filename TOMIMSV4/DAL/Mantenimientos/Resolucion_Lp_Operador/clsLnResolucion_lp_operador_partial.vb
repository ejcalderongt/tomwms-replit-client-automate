Imports System.Data.SqlClient

Partial Public Class clsLnResolucion_lp_operador

    Public Shared Function Get_All_By_IdOperador(ByVal pIdOperador As Integer) As List(Of clsBeResolucion_lp_operador)

        Dim lReturnList As New List(Of clsBeResolucion_lp_operador)

        Try

            Const sp As String = "SELECT * FROM Resolucion_lp_operador WHERE IdOperador = @IdOperador"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOperador", pIdOperador)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeResolucion_lp_operador As New clsBeResolucion_lp_operador

                        For Each dr As DataRow In lDataTable.Rows
                            vBeResolucion_lp_operador = New clsBeResolucion_lp_operador()
                            Cargar(vBeResolucion_lp_operador, dr)
                            lReturnList.Add(vBeResolucion_lp_operador)
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

    Public Shared Function Get_Resolucion_By_IdOperador_And_IdBodega(ByVal pIdOperador As Integer,
                                                                     ByVal pIdBodega As Integer) As clsBeResolucion_lp_operador

        Get_Resolucion_By_IdOperador_And_IdBodega = Nothing

        Dim lReturnList As New List(Of clsBeResolucion_lp_operador)

        Try

            Const sp As String = "SELECT * FROM Resolucion_lp_operador 
								  WHERE IdOperador = @IdOperador 
								  AND IdBodega = @IdBodega
								  AND Activo =1"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOperador", pIdOperador)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable.Rows.Count = 1 Then
                            Dim vBeResolucion_lp_operador As New clsBeResolucion_lp_operador
                            vBeResolucion_lp_operador = New clsBeResolucion_lp_operador()
                            Dim dr As DataRow = lDataTable.Rows(0)
                            Cargar(vBeResolucion_lp_operador, dr)
                            Get_Resolucion_By_IdOperador_And_IdBodega = vBeResolucion_lp_operador
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Resolucion_By_IdOperador_And_IdBodega(ByVal pIdOperador As Integer,
                                                                     ByVal pIdBodega As Integer,
                                                                     ByVal lConnection As SqlConnection,
                                                                     ByVal lTransaction As SqlTransaction) As clsBeResolucion_lp_operador

        Get_Resolucion_By_IdOperador_And_IdBodega = Nothing

        Dim lReturnList As New List(Of clsBeResolucion_lp_operador)

        Try

            Const sp As String = "SELECT * FROM Resolucion_lp_operador 
								  WHERE IdOperador = @IdOperador 
								  AND IdBodega = @IdBodega
								  AND Activo =1"

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOperador", pIdOperador)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable.Rows.Count = 1 Then
                    Dim vBeResolucion_lp_operador As New clsBeResolucion_lp_operador
                    vBeResolucion_lp_operador = New clsBeResolucion_lp_operador()
                    Dim dr As DataRow = lDataTable.Rows(0)
                    Cargar(vBeResolucion_lp_operador, dr)
                    Get_Resolucion_By_IdOperador_And_IdBodega = vBeResolucion_lp_operador
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_Serie(ByRef pSerie As String,
                                        ByVal pConection As SqlConnection,
                                        ByVal pTransaction As SqlTransaction) As Boolean


        Existe_Serie = False

        Try

            Dim sp As String = "SELECT * FROM resolucion_lp_operador WHERE serie = @Serie"

            Dim cmd As New SqlCommand(sp, pConection, pTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@Serie", pSerie))

            Dim dt As New DataTable
            dad.Fill(dt)

            cmd.Dispose()
            dad.Dispose()

            Existe_Serie = (dt.Rows.Count = 1)

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_Serie_By_IdOperador_And_IdBodega(ByVal pSerie As String,
                                                                   ByVal pOperador As Integer,
                                                                   ByVal pIdBodega As Integer) As Boolean


        Existe_Serie_By_IdOperador_And_IdBodega = False

        Try

            Const sp As String = "SELECT * FROM resolucion_lp_operador 
                                  WHERE serie = @Serie AND 
                                       ( (IdOperador = @IdOperador AND 
                                        IdBodega <> @IdBodega)OR
										(IdOperador <> @IdOperador AND 
                                        IdBodega = @IdBodega)OR
										(IdOperador <> @IdOperador AND 
                                        IdBodega <>  @IdBodega))"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@Serie", pSerie)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOperador", pOperador)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Existe_Serie_By_IdOperador_And_IdBodega = (lDataTable.Rows.Count = 1)

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Nuevo_Correlativo_LP_BOF(ByRef Resolucion_Operador As clsBeResolucion_lp_operador) As String

        'Get_Nuevo_Correlativo_LP_BOF = ""

        Try

            Dim pLpSiguiente As Long = Resolucion_Operador.Correlativo_Actual + 1
            Dim largoMaximo As Integer = Resolucion_Operador.Correlativo_Final.ToString("D").Length
            Dim pSerie As String = Resolucion_Operador.Serie
            Dim pLicencia = pSerie + pLpSiguiente.ToString("D" + largoMaximo.ToString())

            Return pLicencia

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_Serie(ByVal pSerie As String) As Boolean


        Existe_Serie = False

        Try

            Const sp As String = "SELECT * FROM resolucion_lp_operador WHERE serie = @Serie"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@Serie", pSerie)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Existe_Serie = (lDataTable.Rows.Count = 1)

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
