Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnJornada_laboral

    Public Shared Function Listar(ByVal pActivo As Boolean, ByVal pFiltro As String) As DataTable

        Try

            Dim sp As String = "SELECT IdBodega,IdJornada AS Código, Nombre_Jornada AS Jornada FROM Jornada_laboral WHERE 1 > 0 "

            If pActivo Then
                sp += " AND Activo=1"
            Else
                sp += " AND Activo=0"
            End If

            If Not String.IsNullOrEmpty(pFiltro) Then
                sp += String.Format(" AND (IdJornada LIKE '%{0}%'", pFiltro)
                sp += String.Format(" OR Nombre_Jornada LIKE '%{0}%')", pFiltro)
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

    Public Shared Function GetAll(ByVal Activo As Boolean) As List(Of clsBeJornada_laboral)

        Try

            Dim lReturnList As New List(Of clsBeJornada_laboral)
            Const sp As String = "SELECT * FROM Jornada_laboral WHERE Activo = @Activo "
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@Activo", Activo)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeJornada_laboral As New clsBeJornada_laboral

            For Each dr As DataRow In dt.Rows

                vBeJornada_laboral = New clsBeJornada_laboral
                Cargar(vBeJornada_laboral, dr)
                lReturnList.Add(vBeJornada_laboral)

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

    Public Shared Function GetAllForCombo(ByVal pIdBodega As Integer) As DataTable

        Try

            Const sp As String = "SELECT IdJornada,nombre_jornada,bodega.nombre, bodega.IdBodega FROM 
                                  Jornada_laboral inner join bodega on jornada_laboral.IdBodega = bodega.IdBodega
                                  WHERE jornada_laboral.IdBodega= @pIdBodega and jornada_laboral.activo=1 "

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@pIdBodega", pIdBodega)
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

    Public Shared Function GetTotalHorasJornada(ByVal pIdJornada As Integer) as Double

        GetTotalHorasJornada = 0

        Try

            '#HS 20171016 0957 Quité String.Format.
            Dim vSQL As String = "SELECT ISNULL(sum(horas_trabajadas),0) as totalHorasJornada from jornada_laboral where idJornada = @idJornada"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
                Using lCommand As New SqlCommand(vSQL, lConnection) With {.CommandType = CommandType.Text}
                    lCommand.Parameters.AddWithValue("@IdJornada", pIdJornada)
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        GetTotalHorasJornada = lReturnValue
                    End If
                End Using
            End Using


        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    'query = 

End Class
