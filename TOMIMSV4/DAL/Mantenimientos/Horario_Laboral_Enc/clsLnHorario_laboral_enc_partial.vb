Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnHorario_laboral_enc

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            '#HS 08112017 Quité query dentro de SqlCommand.
            Dim vSQL As String = "SELECT ISNULL(Max(IdHorarioLaboralEnc),0) FROM horario_laboral_enc"

            'Validacion y estandarizacion de los datos
            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                'Acceso a los datos.
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

    ''' <summary>
    ''' 'FUncion que devuelve todos los horarios
    ''' </summary>
    ''' <param name="pActivo"></param>
    ''' <returns></returns>
    Public Shared Function Listar(ByVal pActivo As Boolean) As DataTable

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim sp As String = "Select * from VW_HorarioLaboralEnc where 1 > 0"

                If pActivo Then
                    sp += " AND Activo=1"
                Else
                    sp += " AND Activo=0"
                End If

                Dim cmd As New SqlCommand(sp, lConnection)
                Dim dad As New SqlDataAdapter(cmd)
                Dim dt As New DataTable
                dad.Fill(dt)

                Return dt

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Exists(ByVal pIdBodega As Integer, ByVal pIdJornada As Integer, ByVal pIdTurno As Integer) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM horario_laboral_enc WHERE IdBodega=@IdBodega And IdJornada=@IdJornada And IdTurno=@IdTurno "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(vSQL, lConnection)

                    lCommand.CommandType = CommandType.Text

                    lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                    lCommand.Parameters.AddWithValue("@IdJornada", pIdJornada)
                    lCommand.Parameters.AddWithValue("@IdTurno", pIdTurno)

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
