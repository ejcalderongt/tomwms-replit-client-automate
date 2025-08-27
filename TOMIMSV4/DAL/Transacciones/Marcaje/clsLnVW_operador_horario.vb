Imports System.Data.SqlClient

Public Class clsLnVW_operador_horario

    Private Const vSQL_Get_All As String = "SELECT * FROM vw_operador_horario 
											WHERE IdOperador = @IdOperador 
											AND IdBodega = @IdBodega "

    Public Shared Sub Cargar(ByRef oBevw_operador_horario As clsBeVW_operador_horario, ByRef dr As DataRow)

        Try

            With oBevw_operador_horario

                .IdEmpresa = IIf(IsDBNull(dr.Item("IdEmpresa")), 0, dr.Item("IdEmpresa"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .IdOperador = IIf(IsDBNull(dr.Item("IdOperador")), 0, dr.Item("IdOperador"))
                .IdJornada = IIf(IsDBNull(dr.Item("IdJornada")), 0, dr.Item("IdJornada"))
                .Fecha_inicio = IIf(IsDBNull(dr.Item("fecha_inicio")), Date.Now, dr.Item("fecha_inicio"))
                .Fecha_fin = IIf(IsDBNull(dr.Item("fecha_fin")), Date.Now, dr.Item("fecha_fin"))
                .IdHorarioLaboralEnc = IIf(IsDBNull(dr.Item("IdHorarioLaboralEnc")), 0, dr.Item("IdHorarioLaboralEnc"))
                .Dia = IIf(IsDBNull(dr.Item("dia")), 0, dr.Item("dia"))
                .Hora_inicio = IIf(IsDBNull(dr.Item("hora_inicio")), Date.Now, dr.Item("hora_inicio"))
                .Hora_fin = IIf(IsDBNull(dr.Item("hora_fin")), Date.Now, dr.Item("hora_fin"))
                .Minimo_min_hora_ingreso = IIf(IsDBNull(dr.Item("minimo_min_hora_ingreso")), 0, dr.Item("minimo_min_hora_ingreso"))
                .Maximo_min_hora_ingreso = IIf(IsDBNull(dr.Item("maximo_min_hora_ingreso")), 0, dr.Item("maximo_min_hora_ingreso"))
                .Minimo_min_hora_salida = IIf(IsDBNull(dr.Item("minimo_min_hora_salida")), 0, dr.Item("minimo_min_hora_salida"))
                .Maximo_min_hora_salida = IIf(IsDBNull(dr.Item("maximo_min_hora_salida")), 0, dr.Item("maximo_min_hora_salida"))
                .Tiempo_retraso_permitido = IIf(IsDBNull(dr.Item("tiempo_retraso_permitido")), 0, dr.Item("tiempo_retraso_permitido"))
                .Horas_extras = IIf(IsDBNull(dr.Item("horas_extras")), False, dr.Item("horas_extras"))

                .OperadorActivo = IIf(IsDBNull(dr.Item("OperadorActivo")), False, dr.Item("OperadorActivo"))
                .OperadorBodegaActivo = IIf(IsDBNull(dr.Item("OperadorBodegaActivo")), False, dr.Item("OperadorBodegaActivo"))
                .JornadaLaboralActivo = IIf(IsDBNull(dr.Item("JornadaLaboralActivo")), False, dr.Item("JornadaLaboralActivo"))
                .HorarioActivo = IIf(IsDBNull(dr.Item("HorarioActivo")), False, dr.Item("HorarioActivo"))
                .HorarioLaboralDetActivo = IIf(IsDBNull(dr.Item("HorarioLaboralDetActivo")), False, dr.Item("HorarioLaboralDetActivo"))
                .TurnoActivo = IIf(IsDBNull(dr.Item("TurnoActivo")), False, dr.Item("TurnoActivo"))

            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Sub GetSingle(ByRef pBevw_operador_horario As clsBeVW_operador_horario)

        Try

            Const sp As String = "SELECT * FROM vw_operador_horario"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBevw_operador_horario As New clsBeVW_operador_horario

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(pBevw_operador_horario, lDataTable.Rows(0))
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using


        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Get_All_Horarios_By_IdOperador_And_IdBodega(ByVal pIdOperador As Integer,
                                                                       ByVal pIdBodega As Integer) As List(Of clsBeVW_operador_horario)

        Get_All_Horarios_By_IdOperador_And_IdBodega = Nothing

        Dim lBevw_operador_horario As New List(Of clsBeVW_operador_horario)

        Try

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL_Get_All, lConnection)

                        lDTA.SelectCommand.Parameters.AddWithValue("IdOperador", pIdOperador)
                        lDTA.SelectCommand.Parameters.AddWithValue("IdBodega", pIdBodega)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBevw_operador_horario As New clsBeVW_operador_horario

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each dr In lDataTable.Rows

                                Dim Bevw_operador_horario As New clsBeVW_operador_horario
                                Cargar(New clsBeVW_operador_horario, lDataTable.Rows(0))
                                lBevw_operador_horario.Add(New clsBeVW_operador_horario)

                            Next

                            Return lBevw_operador_horario

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

    Public Shared Function Get_All_Horarios_By_IdOperador_And_IdBodega(ByVal pIdOperador As Integer,
                                                                       ByVal pIdBodega As Integer,
                                                                       ByVal lConnection As SqlConnection,
                                                                       ByVal lTransaction As SqlTransaction) As List(Of clsBeVW_operador_horario)

        Get_All_Horarios_By_IdOperador_And_IdBodega = Nothing

        Dim lBevw_operador_horario As New List(Of clsBeVW_operador_horario)

        Try

            Using lDTA As New SqlDataAdapter(vSQL_Get_All, lConnection)

                lDTA.SelectCommand.Parameters.AddWithValue("IdOperador", pIdOperador)
                lDTA.SelectCommand.Parameters.AddWithValue("IdBodega", pIdBodega)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBevw_operador_horario As New clsBeVW_operador_horario

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each dr In lDataTable.Rows

                        Dim Bevw_operador_horario As New clsBeVW_operador_horario
                        Cargar(Bevw_operador_horario, dr)
                        lBevw_operador_horario.Add(Bevw_operador_horario)

                    Next

                    Return lBevw_operador_horario

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
