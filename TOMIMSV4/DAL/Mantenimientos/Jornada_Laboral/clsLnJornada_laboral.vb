Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnJornada_laboral

    Public Shared Sub Cargar(ByRef oBeJornada_laboral As clsBeJornada_laboral, ByRef dr As DataRow)
        Try
            With oBeJornada_laboral
                .IdJornada = IIf(IsDBNull(dr.Item("IdJornada")), 0, dr.Item("IdJornada"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .Nombre_jornada = IIf(IsDBNull(dr.Item("nombre_jornada")), "", dr.Item("nombre_jornada"))
                .Fecha_inicio = IIf(IsDBNull(dr.Item("fecha_inicio")), Date.Now, dr.Item("fecha_inicio"))
                .Fecha_fin = IIf(IsDBNull(dr.Item("fecha_fin")), Date.Now, dr.Item("fecha_fin"))
                .Horas_trabajadas = IIf(IsDBNull(dr.Item("horas_trabajadas")), 0, dr.Item("horas_trabajadas"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Fecha_baja = IIf(IsDBNull(dr.Item("fecha_baja")), Date.Now, dr.Item("fecha_baja"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
            End With

        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeJornada_laboral As clsBeJornada_laboral, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("jornada_laboral")
            Ins.Add("idjornada", "@idjornada", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("nombre_jornada", "@nombre_jornada", DataType.Parametro)
            Ins.Add("fecha_inicio", "@fecha_inicio", DataType.Parametro)
            Ins.Add("fecha_fin", "@fecha_fin", DataType.Parametro)
            Ins.Add("horas_trabajadas", "@horas_trabajadas", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("fecha_baja", "@fecha_baja", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)

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

            cmd.Parameters.Add(New SqlParameter("@IDJORNADA", oBeJornada_laboral.IdJornada))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeJornada_laboral.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_JORNADA", oBeJornada_laboral.Nombre_jornada))
            cmd.Parameters.Add(New SqlParameter("@FECHA_INICIO", oBeJornada_laboral.Fecha_inicio))
            cmd.Parameters.Add(New SqlParameter("@FECHA_FIN", oBeJornada_laboral.Fecha_fin))
            cmd.Parameters.Add(New SqlParameter("@HORAS_TRABAJADAS", oBeJornada_laboral.Horas_trabajadas))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeJornada_laboral.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeJornada_laboral.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeJornada_laboral.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeJornada_laboral.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@FECHA_BAJA", oBeJornada_laboral.Fecha_baja))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeJornada_laboral.Activo))

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

    Public Shared Function Actualizar(ByRef oBeJornada_laboral As clsBeJornada_laboral, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("jornada_laboral")
            Upd.Add("idjornada", "@idjornada", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("nombre_jornada", "@nombre_jornada", DataType.Parametro)
            Upd.Add("fecha_inicio", "@fecha_inicio", DataType.Parametro)
            Upd.Add("fecha_fin", "@fecha_fin", DataType.Parametro)
            Upd.Add("horas_trabajadas", "@horas_trabajadas", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("fecha_baja", "@fecha_baja", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("IdJornada = @IdJornada")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}


            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDJORNADA", oBeJornada_laboral.IdJornada))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeJornada_laboral.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_JORNADA", oBeJornada_laboral.Nombre_jornada))
            cmd.Parameters.Add(New SqlParameter("@FECHA_INICIO", oBeJornada_laboral.Fecha_inicio))
            cmd.Parameters.Add(New SqlParameter("@FECHA_FIN", oBeJornada_laboral.Fecha_fin))
            cmd.Parameters.Add(New SqlParameter("@HORAS_TRABAJADAS", oBeJornada_laboral.Horas_trabajadas))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeJornada_laboral.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeJornada_laboral.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeJornada_laboral.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeJornada_laboral.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@FECHA_BAJA", oBeJornada_laboral.Fecha_baja))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeJornada_laboral.Activo))

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


    Public Shared Function Eliminar(ByRef oBeJornada_laboral As clsBeJornada_laboral, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Jornada_laboral" &
             "  Where(IdJornada = @IdJornada)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDJORNADA", oBeJornada_laboral.IdJornada))

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

            Const sp As String = " Delete from Jornada_laboral"
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

        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Jornada_laboral"
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
    Public Shared Function Listar(ByVal pIdBodega As Integer) As DataTable

        Try

            Dim vSQL As String = "SELECT IdJornada, nombre_jornada FROM Jornada_Laboral WHERE IdBodega = @IdBodega AND Activo=1"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(vSQL, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function
    Public Shared Function Obtener(ByRef oBeJornada_laboral As clsBeJornada_laboral) As Boolean

        Try

            Const sp As String = "SELECT * FROM Jornada_laboral" & _
            " Where(IdJornada = @IdJornada)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)


            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDJORNADA", oBeJornada_laboral.IDJORNADA))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeJornada_laboral, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True


        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeJornada_laboral)

        Try

            Dim lReturnList As New List(Of clsBeJornada_laboral)
            Const sp As String = "SELECT * FROM Jornada_laboral"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
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
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeJornada_laboral As clsBeJornada_laboral)

        Try

            Const sp As String = "SELECT * FROM Jornada_laboral" & _
            " Where(IdJornada = @IdJornada)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDJORNADA", pBeJornada_laboral.IDJORNADA))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeJornada_laboral, dt.Rows(0))
            End If

            Return True


        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID() as Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdJornada),0) FROM Jornada_laboral"

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


        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
