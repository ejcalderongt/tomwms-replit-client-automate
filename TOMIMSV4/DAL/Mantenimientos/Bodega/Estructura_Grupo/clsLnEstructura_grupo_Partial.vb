Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnEstructura_grupo

    Public Shared Function MaxPos(pidTramo As Integer) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(pos),0) FROM Estructura_grupo Where(IdTramo = @IdTramo)"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
                Using lCommand As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

                    lCommand.Parameters.Add(New SqlParameter("@IdTramo", pidTramo))
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CInt(lReturnValue)
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

    'Public Shared Function Get_All_By_IdBodega_And_IdTramo(ByVal pIdBodega As Integer,
    '                                                       ByVal pIdtramo As Integer) As List(Of clsBeEstructura_grupo)

    '    Dim lReturnList As New List(Of clsBeEstructura_grupo)

    '    Try


    '        'lReturnList = GetAll().FindAll(Function(x) x.IdTramo = pIdtramo).OrderBy(Function(Y) Y.Pos).ThenByDescending(Function(Y) Y.IdGrupo).ToList()
    '        lReturnList = Get_All_By_IdBodega_And_IdTramo(pIdBodega, pIdtramo)

    '        Return lReturnList

    '    Catch ex As Exception
    '        Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
    '        clsLnLog_error_wms.Agregar_Error(vMsgError)
    '        Throw ex
    '    End Try

    'End Function

    Public Shared Function Actualizar_Batch(ByVal listBeEstructuraGrupo As List(Of clsBeEstructura_grupo)) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Actualizar_Batch = False

        Try

            If listBeEstructuraGrupo.Count > 0 Then

                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                For Each grupo As clsBeEstructura_grupo In listBeEstructuraGrupo
                    Actualizar(grupo, lConnection, lTransaction)
                Next

                lTransaction.Commit()

                Actualizar_Batch = True

            End If

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Insertar_Batch(ByVal IdTramo As Integer, ByVal IdBodega As Integer, grupos As List(Of clsBeEstructura_grupo)) As Boolean

        Insertar_Batch = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        If grupos.Count = 0 Then Return False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Eliminar_Tramo(IdTramo, IdBodega, lConnection, lTransaction)

            For Each grupo As clsBeEstructura_grupo In grupos
                Insertar(grupo, lConnection, lTransaction)
            Next

            lTransaction.Commit()

            Insertar_Batch = True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Eliminar(idgrupo As Integer, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Estructura_grupo" &
             "  Where(IdGrupo = @IdGrupo)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDGRUPO", idgrupo))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try
    End Function

    Public Shared Function Eliminar_Tramo(ByVal IdTramo As Integer, ByVal IdBodega As Integer, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Estructura_grupo  Where (IdTramo = @IdTramo AND IdBodega = @IdBodega)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IdTramo", IdTramo))
            cmd.Parameters.Add(New SqlParameter("@IdBodega", IdBodega))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try
    End Function

    Public Shared Function Get_All_By_IdTramo(ByVal pIdBodega As Integer,
                                              ByVal pIdTramo As Integer) As List(Of clsBeEstructura_grupo)

        Get_All_By_IdTramo = Nothing
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lReturnList As New List(Of clsBeEstructura_grupo)
            Const sp As String = "SELECT * FROM Estructura_grupo WHERE IdTramo = @IdTramo AND IdBodega = @IdBodega"
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
            dad.SelectCommand.Parameters.AddWithValue("@IdTramo", pIdTramo)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeEstructura_grupo As New clsBeEstructura_grupo

            For Each dr As DataRow In dt.Rows
                vBeEstructura_grupo = New clsBeEstructura_grupo
                Cargar(vBeEstructura_grupo, dr)
                lReturnList.Add(vBeEstructura_grupo)
            Next

            Get_All_By_IdTramo = lReturnList

            lTransaction.Commit()

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function


    Public Shared Function Limpiar_Todo(ByVal IdBodega As Integer, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Integer

        Try

            Const sp As String = " Delete from Estructura_grupo where IdBodega = @IdBodega"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                lCommand.Parameters.Add(New SqlParameter("@IdBodega", IdBodega))

                Dim rowsAffected As Integer = lCommand.ExecuteNonQuery()
                lCommand.Dispose()

                Return rowsAffected
            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class


