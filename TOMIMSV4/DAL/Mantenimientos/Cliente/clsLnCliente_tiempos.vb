Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnCliente_tiempos

    Public Shared Sub Cargar(ByRef oBeCliente_tiempos As clsBeCliente_tiempos, ByRef dr As DataRow)
        Try
            With oBeCliente_tiempos
                .IdTiempoCliente = IIf(IsDBNull(dr.Item("IdTiempoCliente")), 0, dr.Item("IdTiempoCliente"))
                .IdCliente = IIf(IsDBNull(dr.Item("IdCliente")), 0, dr.Item("IdCliente"))
                .IdFamilia = IIf(IsDBNull(dr.Item("IdFamilia")), 0, dr.Item("IdFamilia"))
                .IdClasificacion = IIf(IsDBNull(dr.Item("IdClasificacion")), 0, dr.Item("IdClasificacion"))
                .Dias_Local = IIf(IsDBNull(dr.Item("Dias_Local")), 0, dr.Item("Dias_Local"))
                .Dias_Exterior = IIf(IsDBNull(dr.Item("Dias_Exterior")), 0, dr.Item("Dias_Exterior"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Es_Manufactura = IIf(IsDBNull(dr.Item("es_manufactura")), False, dr.Item("es_manufactura"))
            End With

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeCliente_tiempos As clsBeCliente_tiempos,
                                        Optional ByVal pConection As SqlConnection = Nothing,
                                        Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("cliente_tiempos")
            Ins.Add("idtiempocliente", "@idtiempocliente", DataType.Parametro)
            Ins.Add("idcliente", "@idcliente", DataType.Parametro)
            'Ins.Add("idfamilia", "@idfamilia", DataType.Parametro)
            'Ins.Add("idclasificacion", "@idclasificacion", DataType.Parametro)
            If Not oBeCliente_tiempos.IdFamilia = 0 Then Ins.Add("idfamilia", "@idfamilia", DataType.Parametro)
            If Not oBeCliente_tiempos.IdClasificacion = 0 Then Ins.Add("idclasificacion", "@idclasificacion", DataType.Parametro)
            Ins.Add("dias_local", "@dias_local", DataType.Parametro)
            Ins.Add("dias_exterior", "@dias_exterior", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("es_manufactura", "@es_manufactura", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            '#EJC20191205: Trans_Ref03
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTIEMPOCLIENTE", oBeCliente_tiempos.IdTiempoCliente))
            cmd.Parameters.Add(New SqlParameter("@IDCLIENTE", oBeCliente_tiempos.IdCliente))
            'cmd.Parameters.Add(New SqlParameter("@IDFAMILIA", oBeCliente_tiempos.IdFamilia))
            'cmd.Parameters.Add(New SqlParameter("@IDCLASIFICACION", oBeCliente_tiempos.IdClasificacion))
            If Not oBeCliente_tiempos.IdClasificacion = 0 Then cmd.Parameters.Add(New SqlParameter("@IDCLASIFICACION", oBeCliente_tiempos.IdClasificacion))
            If Not oBeCliente_tiempos.IdFamilia = 0 Then cmd.Parameters.Add(New SqlParameter("@IDFAMILIA", oBeCliente_tiempos.IdFamilia))
            cmd.Parameters.Add(New SqlParameter("@DIAS_LOCAL", oBeCliente_tiempos.Dias_Local))
            cmd.Parameters.Add(New SqlParameter("@DIAS_EXTERIOR", oBeCliente_tiempos.Dias_Exterior))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeCliente_tiempos.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeCliente_tiempos.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeCliente_tiempos.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeCliente_tiempos.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeCliente_tiempos.Activo))
            cmd.Parameters.Add(New SqlParameter("@ES_MANUFACTURA", oBeCliente_tiempos.Es_Manufactura))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeCliente_tiempos As clsBeCliente_tiempos, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("cliente_tiempos")
            Upd.Add("idtiempocliente", "@idtiempocliente", DataType.Parametro)
            Upd.Add("idcliente", "@idcliente", DataType.Parametro)
            Upd.Add("idfamilia", "@idfamilia", DataType.Parametro)
            Upd.Add("idclasificacion", "@idclasificacion", DataType.Parametro)
            Upd.Add("dias_local", "@dias_local", DataType.Parametro)
            Upd.Add("dias_exterior", "@dias_exterior", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("es_manufactura", "@es_manufactura", DataType.Parametro)
            Upd.Where("IdTiempoCliente = @IdTiempoCliente")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            '#EJC20191205: Trans_Ref03
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTIEMPOCLIENTE", oBeCliente_tiempos.IdTiempoCliente))
            cmd.Parameters.Add(New SqlParameter("@IDCLIENTE", oBeCliente_tiempos.IdCliente))
            cmd.Parameters.Add(New SqlParameter("@IDFAMILIA", oBeCliente_tiempos.IdFamilia))
            cmd.Parameters.Add(New SqlParameter("@IDCLASIFICACION", oBeCliente_tiempos.IdClasificacion))
            cmd.Parameters.Add(New SqlParameter("@DIAS_LOCAL", oBeCliente_tiempos.Dias_Local))
            cmd.Parameters.Add(New SqlParameter("@DIAS_EXTERIOR", oBeCliente_tiempos.Dias_Exterior))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeCliente_tiempos.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeCliente_tiempos.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeCliente_tiempos.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeCliente_tiempos.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeCliente_tiempos.Activo))
            cmd.Parameters.Add(New SqlParameter("@ES_MANUFACTURA", oBeCliente_tiempos.Es_Manufactura))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Eliminar(ByRef oBeCliente_tiempos As clsBeCliente_tiempos, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Cliente_tiempos" &
                 "  Where(IdTiempoCliente = @IdTiempoCliente)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            '#EJC20191205: Trans_Ref03
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTIEMPOCLIENTE", oBeCliente_tiempos.IdTiempoCliente))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Cliente_tiempos"
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            '#EJC20191205: Trans_Ref02
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Cliente_tiempos"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeCliente_tiempos As clsBeCliente_tiempos) As Boolean

        Try

            Const sp As String = "SELECT * FROM Cliente_tiempos" &
                " Where(IdTiempoCliente = @IdTiempoCliente)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTIEMPOCLIENTE", oBeCliente_tiempos.IdTiempoCliente))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeCliente_tiempos, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeCliente_tiempos)

        Try

            Dim lReturnList As New List(Of clsBeCliente_tiempos)
            Const sp As String = "SELECT * FROM Cliente_tiempos"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeCliente_tiempos As New clsBeCliente_tiempos

            For Each dr As DataRow In dt.Rows

                vBeCliente_tiempos = New clsBeCliente_tiempos
                Cargar(vBeCliente_tiempos, dr)
                lReturnList.Add(vBeCliente_tiempos)

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

    Public Shared Function GetSingle(ByRef pBeCliente_tiempos As clsBeCliente_tiempos)

        Try

            Const sp As String = "SELECT * FROM Cliente_tiempos" &
                " Where(IdTiempoCliente = @IdTiempoCliente)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTIEMPOCLIENTE", pBeCliente_tiempos.IdTiempoCliente))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeCliente_tiempos, dt.Rows(0))
            End If

            Return True

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdTiempoCliente),0) FROM Cliente_tiempos"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
                Using lCommand As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CInt(lReturnValue)
                    End If
                End Using
            End Using

            Return lMax

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdTiempoCliente),0) FROM Cliente_tiempos"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If
            End Using

            Return lMax

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal IdTiempoCliente As Integer) As clsBeCliente_tiempos

        GetSingle = Nothing

        Try

            Const sp As String = "SELECT * FROM Cliente_tiempos" &
                " Where(IdTiempoCliente = @IdTiempoCliente)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTIEMPOCLIENTE", IdTiempoCliente))

            Dim dt As New DataTable
            dad.Fill(dt)

            Dim pBeCliente_tiempos As New clsBeCliente_tiempos

            If dt.Rows.Count = 1 Then
                Cargar(pBeCliente_tiempos, dt.Rows(0))
                Return pBeCliente_tiempos
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Cliente_Tiene_Tiempos(ByVal IdCliente As Integer,
                                                     ByRef lConnection As SqlConnection,
                                                     ByRef lTransaction As SqlTransaction) As Boolean

        Try

            Const sp As String = "SELECT IdCliente FROM Cliente_tiempos where IdCliente=@IdCliente"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                lCommand.Parameters.AddWithValue("@IdCliente", IdCliente)
                Dim lReturnValue As Object = lCommand.ExecuteNonQuery()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    Return (lReturnValue > 0)
                Else
                    Return False
                End If
            End Using

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
