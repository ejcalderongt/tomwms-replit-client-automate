Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnHorario_laboral_det

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdHorarioLaboralDet),0) FROM horario_laboral_Det"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(vSQL, lConnection)

                    lCommand.CommandType = CommandType.Text

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
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function
    Public Shared Function getAllByHorarioEnc(ByVal pIdHorarioEnc As Integer, ByVal Activo As Boolean) As List(Of clsBeHorario_laboral_det)
        Try

            Dim lReturnList As New List(Of clsBeHorario_laboral_det)

            Using lCnn As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim lSQL As String = String.Format("SELECT * FROM VW_HorarioLaboralDet WHERE IdHorarioLaboralEnc={0}", pIdHorarioEnc)

                If Activo Then
                    lSQL += " AND activo=1"
                Else
                    lSQL += " AND activo=0"
                End If

                'Acceso a los datos.
                Using lDTA As New SqlClient.SqlDataAdapter(lSQL, lCnn)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeHorario_laboral_det

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows
                            Obj = New clsBeHorario_laboral_det()

                            Obj.IdHorarioLaboralDet = CType(lRow("IdHorarioLaboralDet"), System.Int32)

                            If lRow("IdHorarioLaboralEnc") IsNot DBNull.Value AndAlso lRow("IdHorarioLaboralEnc") IsNot Nothing Then
                                Obj.IdHorarioLaboralEnc = CType(lRow("IdHorarioLaboralEnc"), System.Int32)
                            End If

                            If lRow("Dia") IsNot DBNull.Value AndAlso lRow("Dia") IsNot Nothing Then
                                Obj.Dia = CType(lRow("Dia"), System.Int32)
                            End If

                            If lRow("NombreDia") IsNot DBNull.Value AndAlso lRow("NombreDia") IsNot Nothing Then
                                Obj.NombreDia = CType(lRow("NombreDia"), System.String)
                            End If

                            If lRow("Hora_inicio") IsNot DBNull.Value AndAlso lRow("Hora_inicio") IsNot Nothing Then
                                Obj.Hora_inicio = CType(lRow("Hora_inicio"), System.DateTime)
                            End If

                            If lRow("NHora_inicio") IsNot DBNull.Value AndAlso lRow("NHora_inicio") IsNot Nothing Then
                                Obj.NHoraInicio = CType(lRow("NHora_inicio"), System.String)
                            End If

                            If lRow("Hora_fin") IsNot DBNull.Value AndAlso lRow("Hora_fin") IsNot Nothing Then
                                Obj.Hora_fin = CType(lRow("Hora_fin"), System.DateTime)
                            End If

                            If lRow("NHora_fin") IsNot DBNull.Value AndAlso lRow("NHora_fin") IsNot Nothing Then
                                Obj.NHoraFin = CType(lRow("NHora_fin"), System.String)
                            End If

                            If lRow("Minimo_min_hora_ingreso") IsNot DBNull.Value AndAlso lRow("Minimo_min_hora_ingreso") IsNot Nothing Then
                                Obj.Minimo_min_hora_ingreso = CType(lRow("Minimo_min_hora_ingreso"), System.Int32)
                            End If

                            If lRow("Maximo_min_hora_ingreso") IsNot DBNull.Value AndAlso lRow("Maximo_min_hora_ingreso") IsNot Nothing Then
                                Obj.Maximo_min_hora_ingreso = CType(lRow("Maximo_min_hora_ingreso"), System.Int32)
                            End If

                            If lRow("Minimo_min_hora_salida") IsNot DBNull.Value AndAlso lRow("Minimo_min_hora_salida") IsNot Nothing Then
                                Obj.Minimo_min_hora_salida = CType(lRow("Minimo_min_hora_salida"), System.Int32)
                            End If

                            If lRow("Maximo_min_hora_salida") IsNot DBNull.Value AndAlso lRow("Maximo_min_hora_salida") IsNot Nothing Then
                                Obj.Maximo_min_hora_salida = CType(lRow("Maximo_min_hora_salida"), System.Int32)
                            End If

                            If lRow("Tiempo_retraso_permitido") IsNot DBNull.Value AndAlso lRow("Tiempo_retraso_permitido") IsNot Nothing Then
                                Obj.Tiempo_retraso_permitido = CType(lRow("Tiempo_retraso_permitido"), System.Int32)
                            End If

                            If lRow("Horas_extras") IsNot DBNull.Value AndAlso lRow("Horas_extras") IsNot Nothing Then
                                Obj.Horas_extras = CType(lRow("Horas_extras"), System.Boolean)
                            End If

                            If lRow("user_agr") IsNot DBNull.Value AndAlso lRow("user_agr") IsNot Nothing Then
                                Obj.User_agr = CType(lRow("user_agr"), System.String)
                            End If

                            If lRow("fec_agr") IsNot DBNull.Value AndAlso lRow("fec_agr") IsNot Nothing Then
                                Obj.Fec_agr = CType(lRow("fec_agr"), System.DateTime)
                            End If

                            If lRow("user_mod") IsNot DBNull.Value AndAlso lRow("user_mod") IsNot Nothing Then
                                Obj.User_mod = CType(lRow("user_mod"), System.String)
                            End If

                            If lRow("fec_mod") IsNot DBNull.Value AndAlso lRow("fec_mod") IsNot Nothing Then
                                Obj.Fec_mod = CType(lRow("fec_mod"), System.DateTime)
                            End If

                            If lRow("Fecha_baja") IsNot DBNull.Value AndAlso lRow("Fecha_baja") IsNot Nothing Then
                                Obj.Fecha_baja = CType(lRow("Fecha_baja"), System.DateTime)
                            End If

                            If lRow("Activo") IsNot DBNull.Value AndAlso lRow("Activo") IsNot Nothing Then
                                Obj.Activo = CType(lRow("Activo"), System.Boolean)
                            End If

                            lReturnList.Add(Obj)

                        Next

                    End If

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function
    Public Shared Function GetSingle(ByVal pIdHorarioLaboralDet As Integer) As clsBeHorario_laboral_det


        Try

            Using lCnn As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim lSQL As String = String.Format("SELECT * FROM horario_laboral_det WHERE IdHorarioLaboralDet={0}", pIdHorarioLaboralDet)

                Using lDTA As New SqlDataAdapter(lSQL, lCnn)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDT As New DataTable
                    lDTA.Fill(lDT)

                    Dim Obj As clsBeHorario_laboral_det


                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        Dim lRow As DataRow = lDT.Rows(0)
                        Obj = New clsBeHorario_laboral_det()

                        Obj.IdHorarioLaboralDet = CType(lRow("IdHorarioLaboralDet"), System.Int32)

                        If lRow("IdHorarioLaboralEnc") IsNot DBNull.Value AndAlso lRow("IdHorarioLaboralEnc") IsNot Nothing Then
                            Obj.IdHorarioLaboralEnc = CType(lRow("IdHorarioLaboralEnc"), System.Int32)
                        End If
                        If lRow("Hora_inicio") IsNot DBNull.Value AndAlso lRow("Hora_inicio") IsNot Nothing Then
                            Obj.Hora_inicio = CType(lRow("Hora_inicio"), System.DateTime)
                        End If
                        If lRow("Hora_fin") IsNot DBNull.Value AndAlso lRow("Hora_fin") IsNot Nothing Then
                            Obj.Hora_fin = CType(lRow("Hora_fin"), System.DateTime)
                        End If
                        If lRow("Maximo_min_hora_ingreso") IsNot DBNull.Value AndAlso lRow("Maximo_min_hora_ingreso") IsNot Nothing Then
                            Obj.Maximo_min_hora_ingreso = CType(lRow("Maximo_min_hora_ingreso"), System.Int32)
                        End If
                        If lRow("Minimo_min_hora_ingreso") IsNot DBNull.Value AndAlso lRow("Minimo_min_hora_ingreso") IsNot Nothing Then
                            Obj.Minimo_min_hora_ingreso = CType(lRow("Minimo_min_hora_ingreso"), System.Int32)
                        End If
                        If lRow("Maximo_min_hora_salida") IsNot DBNull.Value AndAlso lRow("Maximo_min_hora_salida") IsNot Nothing Then
                            Obj.Maximo_min_hora_salida = CType(lRow("Maximo_min_hora_salida"), System.Int32)
                        End If
                        If lRow("Minimo_min_hora_salida") IsNot DBNull.Value AndAlso lRow("Minimo_min_hora_salida") IsNot Nothing Then
                            Obj.Minimo_min_hora_salida = CType(lRow("Minimo_min_hora_salida"), System.Int32)
                        End If
                        If lRow("Horas_extras") IsNot DBNull.Value AndAlso lRow("Horas_extras") IsNot Nothing Then
                            Obj.Horas_extras = CType(lRow("Horas_extras"), System.Boolean)
                        End If
                        If lRow("user_agr") IsNot DBNull.Value AndAlso lRow("user_agr") IsNot Nothing Then
                            Obj.User_agr = CType(lRow("user_agr"), System.String)
                        End If
                        If lRow("fec_agr") IsNot DBNull.Value AndAlso lRow("fec_agr") IsNot Nothing Then
                            Obj.Fec_agr = CType(lRow("fec_agr"), System.DateTime)
                        End If
                        If lRow("user_mod") IsNot DBNull.Value AndAlso lRow("user_mod") IsNot Nothing Then
                            Obj.User_mod = CType(lRow("user_mod"), System.String)
                        End If
                        If lRow("fec_mod") IsNot DBNull.Value AndAlso lRow("fec_mod") IsNot Nothing Then
                            Obj.Fec_mod = CType(lRow("fec_mod"), System.DateTime)
                        End If
                        If lRow("Activo") IsNot DBNull.Value AndAlso lRow("Activo") IsNot Nothing Then
                            Obj.Activo = CType(lRow("Activo"), System.Boolean)
                        End If

                        Return Obj

                    End If
                End Using
            End Using

            Return Nothing

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function
    Public Shared Function Exists(ByVal pIdEnc As Integer, ByVal pDia As Integer) As Boolean
        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM horario_laboral_det WHERE IdHorarioLaboralEnc=@IdHorarioLaboralEnc And dia=@dia "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(vSQL, lConnection)

                    lCommand.CommandType = CommandType.Text

                    lCommand.Parameters.AddWithValue("@IdHorarioLaboralEnc", pIdEnc)
                    lCommand.Parameters.AddWithValue("@dia", pDia)

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
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Function

End Class
