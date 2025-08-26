Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnTipo_rotacion

    Public Shared Function Exists(ByVal pDescripcion As String) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM tipo_rotacion WHERE descripcion=@descripcion"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                '#HS 07112017 Quité query dentro de SqlCommand.
                Using lCommand As New SqlCommand(vSQL, lConnection)

                    lCommand.CommandType = CommandType.Text
                    lCommand.Parameters.AddWithValue("@descripcion", pDescripcion)

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

    Public Shared Function Exists(ByVal pDescripcion As String,
                                  ByVal lConnection As SqlConnection,
                                  ByVal lTransaction As SqlTransaction) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM tipo_rotacion WHERE descripcion=@descripcion"

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@descripcion", pDescripcion)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lExists = CInt(lReturnValue) > 0
                End If

            End Using

            Return lExists

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Exists_rotacion_By_IdRotacion(ByVal pIdTipoRotacion As Integer,
                                                         ByVal lConnection As SqlConnection,
                                                         ByVal lTransaction As SqlTransaction) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT IdTipoRotacion FROM tipo_rotacion WHERE IdTipoRotacion=@IdTipoRotacion"

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@IdTipoRotacion", pIdTipoRotacion)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lExists = CInt(lReturnValue) > 0
                End If

            End Using

            Return lExists

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetIdTipoRotacionByNombre(ByVal pDescripcion As String,
                                  ByVal lConnection As SqlConnection,
                                  ByVal lTransaction As SqlTransaction) As Integer

        Try

            Dim vSQL As String = "SELECT IdTipoRotacion FROM tipo_rotacion WHERE descripcion=@descripcion"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@descripcion", pDescripcion)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Return lDT(0)(0)

                End If

            End Using

            Return Nothing

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_For_Selection() As List(Of clsBeTipo_rotacion)

        Try

            Dim lReturnList As New List(Of clsBeTipo_rotacion)
            Const sp As String = "SELECT * FROM Tipo_rotacion "
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTipo_rotacion As New clsBeTipo_rotacion

            For Each dr As DataRow In dt.Rows

                vBeTipo_rotacion = New clsBeTipo_rotacion
                Cargar(vBeTipo_rotacion, dr)
                vBeTipo_rotacion.Activo = False
                lReturnList.Add(vBeTipo_rotacion)

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

    Public Shared Function GetAll(ByVal Activo As Boolean) As List(Of clsBeTipo_rotacion)

        Try

            Dim lReturnList As New List(Of clsBeTipo_rotacion)
            Const sp As String = "SELECT * FROM Tipo_rotacion WHERE Activo = @Activo"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@Activo", Activo)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTipo_rotacion As New clsBeTipo_rotacion

            For Each dr As DataRow In dt.Rows

                vBeTipo_rotacion = New clsBeTipo_rotacion
                Cargar(vBeTipo_rotacion, dr)
                lReturnList.Add(vBeTipo_rotacion)

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

    Public Shared Function GetAllForCombo(ByVal Activo As Boolean) As DataTable

        Try

            Const sp As String = "SELECT IdTipoRotacion,Descripcion as Nombre FROM Tipo_rotacion WHERE Activo = @Activo"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@Activo", Activo)
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





End Class
