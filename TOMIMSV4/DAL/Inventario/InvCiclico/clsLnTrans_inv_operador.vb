Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_inv_operador

    Public Shared Sub Cargar(ByRef oBeTrans_inv_operador As clsBeTrans_inv_operador, ByRef dr As DataRow)
        Try
            With oBeTrans_inv_operador
                .Idinvoperador = IIf(IsDBNull(dr.Item("idinvoperador")), 0, dr.Item("idinvoperador"))
                .Idinventarioenc = IIf(IsDBNull(dr.Item("idinventarioenc")), 0, dr.Item("idinventarioenc"))
                .Idinvencreconteo = IIf(IsDBNull(dr.Item("idinvencreconteo")), 0, dr.Item("idinvencreconteo"))
                .Idubic = IIf(IsDBNull(dr.Item("idubic")), 0, dr.Item("idubic"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .Idoperador = IIf(IsDBNull(dr.Item("idoperador")), 0, dr.Item("idoperador"))
            End With
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_inv_operador As clsBeTrans_inv_operador, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_inv_operador")
            Ins.Add("idinvoperador", "@idinvoperador", DataType.Parametro)
            Ins.Add("idinventarioenc", "@idinventarioenc", DataType.Parametro)
            Ins.Add("idinvencreconteo", "@idinvencreconteo", DataType.Parametro)
            Ins.Add("idubic", "@idubic", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("idoperador", "@idoperador", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDINVOPERADOR", oBeTrans_inv_operador.Idinvoperador))
            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_operador.Idinventarioenc))
            cmd.Parameters.Add(New SqlParameter("@IDINVENCRECONTEO", oBeTrans_inv_operador.Idinvencreconteo))
            cmd.Parameters.Add(New SqlParameter("@IDUBIC", oBeTrans_inv_operador.Idubic))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeTrans_inv_operador.Idoperador))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_inv_operador.IdBodega))


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

    Public Shared Function Actualizar(ByRef oBeTrans_inv_operador As clsBeTrans_inv_operador, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_inv_operador")
            Upd.Add("idinvoperador", "@idinvoperador", DataType.Parametro)
            Upd.Add("idinventarioenc", "@idinventarioenc", DataType.Parametro)
            Upd.Add("idinvencreconteo", "@idinvencreconteo", DataType.Parametro)
            Upd.Add("idubic", "@idubic", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("idoperador", "@idoperador", DataType.Parametro)
            Upd.Where("idinvoperador = @idinvoperador")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDINVOPERADOR", oBeTrans_inv_operador.Idinvoperador))
            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_operador.Idinventarioenc))
            cmd.Parameters.Add(New SqlParameter("@IDINVENCRECONTEO", oBeTrans_inv_operador.Idinvencreconteo))
            cmd.Parameters.Add(New SqlParameter("@IDUBIC", oBeTrans_inv_operador.Idubic))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeTrans_inv_operador.Idoperador))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_inv_operador.IdBodega))


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

    Public Shared Function Eliminar(ByRef oBeTrans_inv_operador As clsBeTrans_inv_operador, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_inv_operador" &
             "  Where(idinvoperador = @idinvoperador)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDINVOPERADOR", oBeTrans_inv_operador.Idinvoperador))


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

    Public Shared Function EliminarByUbicacion(ByRef oBeTrans_inv_operador As clsBeTrans_inv_operador, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_inv_operador" &
             "  Where(idubic = @idubic AND Idinventarioenc=@Idinventarioenc)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@idubic", oBeTrans_inv_operador.Idubic))
            cmd.Parameters.Add(New SqlParameter("@Idinventarioenc", oBeTrans_inv_operador.Idinventarioenc))

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

    Public Shared Function Eliminar_IdUbicacion_By_IdOperador_And_IdProductoBodega(ByVal IdInventario As Integer,
                                                                                   ByVal IdOperador As Integer,
                                                                                   ByVal IdProductoBodega As Integer,
                                                                                   Optional ByVal pConection As SqlConnection = Nothing,
                                                                                   Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " DELETE FROM Trans_inv_operador  
                                   WHERE(Idinventarioenc=@Idinventarioenc AND idoperador=@IdOperador AND idubic IN  
                                         (SELECT IdUbicacion FROM trans_inv_ciclico WHERE idinventarioenc =@Idinventarioenc AND   
                                                                                          IdProductoBodega=@IdProductoBodega) AND idubic NOT IN  
                                         (SELECT IdUbicacion FROM trans_inv_ciclico WHERE idinventarioenc =@Idinventarioenc AND   
                                                                                          IdProductoBodega<>@IdProductoBodega))"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@Idinventarioenc", IdInventario))
            cmd.Parameters.Add(New SqlParameter("@IdOperador", IdOperador))
            cmd.Parameters.Add(New SqlParameter("@IdProductoBodega", IdProductoBodega))


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

    Public Shared Function EliminarByOperador(ByRef oBeTrans_inv_operador As clsBeTrans_inv_operador, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_inv_operador" &
             "  Where(idubic = @idubic AND idinventarioenc=@idinventarioenc AND idoperador=@idoperador)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@idubic", oBeTrans_inv_operador.Idubic))
            cmd.Parameters.Add(New SqlParameter("@idinventarioenc", oBeTrans_inv_operador.Idinventarioenc))
            cmd.Parameters.Add(New SqlParameter("@idoperador", oBeTrans_inv_operador.Idoperador))

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

    Public Shared Function Eliminar_Single(ByVal IdInventario As Integer, ByVal IdOperador As Integer, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_inv_operador" &
             "  Where(idinventarioenc=@idinventarioenc AND idoperador=@idoperador)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@idinventarioenc", IdInventario))
            cmd.Parameters.Add(New SqlParameter("@idoperador", IdOperador))


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

    Public Shared Function Eliminar_AllByInventario(ByVal IdInventario As Integer, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_inv_operador" &
             "  Where(idinventarioenc=@idinventarioenc)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@idinventarioenc", IdInventario))


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

    Public Shared Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_inv_operador"
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

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

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Trans_inv_operador"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeTrans_inv_operador As clsBeTrans_inv_operador) As Boolean

        Try

            Const sp As String = "SELECT * FROM Trans_inv_operador" &
            " Where(idinvoperador = @idinvoperador)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDINVOPERADOR", oBeTrans_inv_operador.Idinvoperador))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_inv_operador, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_By_Operador(ByRef oBeTrans_inv_operador As clsBeTrans_inv_operador) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Get_By_Operador = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM Trans_inv_operador " &
            " Where(idoperador = @idoperador AND idubic=@idubic AND idinventarioenc=@idinventarioenc)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeTrans_inv_operador.Idoperador))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUBIC", oBeTrans_inv_operador.Idubic))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_operador.Idinventarioenc))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_inv_operador, dt.Rows(0))
                Get_By_Operador = True
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Existe_Ubicacion_By_IdOperador(ByRef oBeTrans_inv_operador As clsBeTrans_inv_operador,
                                                          ByVal lConnection As SqlConnection,
                                                          ByVal lTransaction As SqlTransaction) As Boolean

        Existe_Ubicacion_By_IdOperador = False

        Try

            Const sp As String = "SELECT * FROM Trans_inv_operador " &
            " Where(idoperador = @idoperador AND idubic=@idubic AND idinventarioenc=@idinventarioenc)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeTrans_inv_operador.Idoperador))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUBIC", oBeTrans_inv_operador.Idubic))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_operador.Idinventarioenc))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_inv_operador, dt.Rows(0))
                Existe_Ubicacion_By_IdOperador = True
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_Operador_By_IdUbicacion(ByRef oBeTrans_inv_operador As clsBeTrans_inv_operador,
                                                          ByRef lConnection As SqlConnection,
                                                          ByRef lTransaction As SqlTransaction) As Boolean
        Existe_Operador_By_IdUbicacion = False

        Try

            Const sp As String = "SELECT count(idinvoperador) AS Existe
                                  FROM Trans_inv_operador
                                  Where(idoperador = @idoperador 
                                        AND idubic=@idubic 
                                        AND idinventarioenc=@idinventarioenc)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeTrans_inv_operador.Idoperador))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUBIC", oBeTrans_inv_operador.Idubic))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_operador.Idinventarioenc))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then
                Existe_Operador_By_IdUbicacion = IIf(IIf(IsDBNull(dt.Rows(0).Item("Existe")), 0, dt.Rows(0).Item("Existe")) > 0, True, False)
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function ObtenerByStock(ByRef oBeTrans_inv_operador As clsBeTrans_inv_operador) As Boolean

        Try

            Const sp As String = "SELECT trans_inv_operador.idinvoperador, trans_inv_operador.idinventarioenc, 
                                         trans_inv_operador.idinvencreconteo,trans_inv_operador.idubic, 
                                         trans_inv_operador.idoperador
                                    FROM trans_inv_operador INNER JOIN
                                         trans_inv_ciclico ON trans_inv_operador.idoperador = trans_inv_ciclico.idoperador AND 
                                         trans_inv_operador.idubic = trans_inv_ciclico.IdUbicacion
                                  Where (trans_inv_operador.idoperador = @idoperador AND trans_inv_operador.idubic=@idubic 
                                         AND trans_inv_operador.idinventarioenc=@idinventarioenc AND trans_inv_ciclico.IdStock = @IdStock)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeTrans_inv_operador.Idoperador))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUBIC", oBeTrans_inv_operador.Idubic))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_operador.Idinventarioenc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdStock", oBeTrans_inv_operador.IdStock))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_inv_operador, dt.Rows(0))
            Else
                Return False
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeTrans_inv_operador)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_operador)
            Const sp As String = "SELECT * FROM Trans_inv_operador"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_inv_operador As New clsBeTrans_inv_operador

            For Each dr As DataRow In dt.Rows
                vBeTrans_inv_operador = New clsBeTrans_inv_operador
                Cargar(vBeTrans_inv_operador, dr)
                lReturnList.Add(vBeTrans_inv_operador)
            Next

            Return lReturnList

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdInventario(ByVal pIdInventario As Integer) As List(Of clsBeTrans_inv_operador)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_operador)
            Const sp As String = "SELECT * FROM Trans_inv_operador where idinventarioenc=@idinventarioenc"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@idinventarioenc", pIdInventario))

            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_inv_operador As New clsBeTrans_inv_operador

            For Each dr As DataRow In dt.Rows
                vBeTrans_inv_operador = New clsBeTrans_inv_operador
                Cargar(vBeTrans_inv_operador, dr)
                lReturnList.Add(vBeTrans_inv_operador)
            Next

            Return lReturnList

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeTrans_inv_operador As clsBeTrans_inv_operador)

        Try

            Const sp As String = "SELECT * FROM Trans_inv_operador" &
            " Where(idinvoperador = @idinvoperador)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDINVOPERADOR", pBeTrans_inv_operador.Idinvoperador))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTrans_inv_operador, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(idinvoperador),0) FROM Trans_inv_operador"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
                Using lCommand As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
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

    Public Shared Function MaxID(ByRef lConnection As SqlConnection,
                                 ByRef lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(idinvoperador),0) FROM Trans_inv_operador"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue) + 1
                End If
            End Using

            Return lMax

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllForCombo(ByVal IdUbic As Integer, ByVal IdInventario As Integer) As DataTable

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT operador.IdOperador, operador.nombres
                        FROM trans_inv_operador INNER JOIN
                         operador ON trans_inv_operador.idoperador = operador.IdOperador 
                        WHERE trans_inv_operador.idubic=@idubic AND trans_inv_operador.idinventarioenc=@idinv"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@idubic", IdUbic)
                    lDTA.SelectCommand.Parameters.AddWithValue("@idinv", IdInventario)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Return lDataTable

                End Using

            End Using

        Catch ex As Exception
            Throw New Exception("BodegaArea_getAllAreaByBodega: " & ex.Message)
        End Try
    End Function

    Public Shared Function Eliminar_IdUbicacion_By_IdUbicacion_And_IdOperador_And_IdProductoBodega(ByVal IdInventario As Integer,
                                                                                                   ByVal IdOperador As Integer,
                                                                                                   ByVal IdProductoBodega As Integer,
                                                                                                   ByVal IdUbicacion As Integer,
                                                                                                   Optional ByVal pConection As SqlConnection = Nothing,
                                                                                                   Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " DELETE FROM Trans_inv_operador  
                                   WHERE(Idinventarioenc=@Idinventarioenc AND idubic IN
                                    (SELECT DISTINCT IdUbicacion FROM trans_inv_ciclico 
                                    WHERE idinventarioenc = @Idinventarioenc AND IdProductoBodega=@IdProductoBodega AND idoperador = @IdOperador AND IdUbicacion = @IdUbicacion) 
                                    AND IdUbic NOT IN
                                    (SELECT IdUbicacion FROM trans_inv_ciclico 
                                    WHERE idinventarioenc =@Idinventarioenc AND IdProductoBodega<>@IdProductoBodega AND idoperador = @IdOperador AND IdUbicacion = @IdUbicacion)) "

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@Idinventarioenc", IdInventario))
            cmd.Parameters.Add(New SqlParameter("@IdOperador", IdOperador))
            cmd.Parameters.Add(New SqlParameter("@IdProductoBodega", IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IdUbicacion", IdUbicacion))

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

    Public Shared Function Existe_Ubicacion_By_IdOperador(ByRef oBeTrans_inv_operador As clsBeTrans_inv_operador) As Boolean

        Existe_Ubicacion_By_IdOperador = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM Trans_inv_operador " &
            " Where(idoperador = @idoperador AND idubic=@idubic AND idinventarioenc=@idinventarioenc)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeTrans_inv_operador.Idoperador))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUBIC", oBeTrans_inv_operador.Idubic))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_operador.Idinventarioenc))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_inv_operador, dt.Rows(0))
                Existe_Ubicacion_By_IdOperador = True
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_By_Operador(ByRef oBeTrans_inv_operador As clsBeTrans_inv_operador, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Boolean

        Get_By_Operador = False

        Try

            Const sp As String = "SELECT * FROM Trans_inv_operador " &
            " Where(idoperador = @idoperador AND idubic=@idubic AND idinventarioenc=@idinventarioenc)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeTrans_inv_operador.Idoperador))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUBIC", oBeTrans_inv_operador.Idubic))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_operador.Idinventarioenc))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_inv_operador, dt.Rows(0))
                Get_By_Operador = True
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Eliminar_Operador_By_IdUbicacion(ByRef oBeTrans_inv_operador As clsBeTrans_inv_operador, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_inv_operador" &
             "  Where(idubic = @idubic AND idinventarioenc=@idinventarioenc)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@idubic", oBeTrans_inv_operador.Idubic))
            cmd.Parameters.Add(New SqlParameter("@idinventarioenc", oBeTrans_inv_operador.Idinventarioenc))

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

End Class
