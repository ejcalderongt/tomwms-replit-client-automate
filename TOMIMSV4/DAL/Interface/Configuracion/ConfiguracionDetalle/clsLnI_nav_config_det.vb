Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnI_nav_config_det

    Public Shared Sub Cargar(ByRef oBeI_nav_config_det As clsBeI_nav_config_det, ByRef dr As DataRow)
        Try
            With oBeI_nav_config_det
                .Idnavconfigdet = IIf(IsDBNull(dr.Item("idnavconfigdet")), 0, dr.Item("idnavconfigdet"))
                .IdNavEnt = IIf(IsDBNull(dr.Item("idnavent")), 0, dr.Item("idnavent"))
                .IdNavConfigEnc = IIf(IsDBNull(dr.Item("idnavconfigenc")), 0, dr.Item("idnavconfigenc"))
                .Dia = IIf(IsDBNull(dr.Item("dia")), 0, dr.Item("dia"))
                .HoraInicio = IIf(IsDBNull(dr.Item("horainicio")), Date.Now, dr.Item("horainicio"))
                .HoraFin = IIf(IsDBNull(dr.Item("horafin")), Date.Now, dr.Item("horafin"))
                .Frecuencia = IIf(IsDBNull(dr.Item("frecuencia")), 0, dr.Item("frecuencia"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
            End With

        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeI_nav_config_det As clsBeI_nav_config_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("i_nav_config_det")
            Ins.Add("idnavconfigdet", "@idnavconfigdet", DataType.Parametro)
            Ins.Add("idnavent", "@idnavent", DataType.Parametro)
            Ins.Add("idnavconfigenc", "@idnavconfigenc", DataType.Parametro)
            Ins.Add("dia", "@dia", DataType.Parametro)
            Ins.Add("horainicio", "@horainicio", DataType.Parametro)
            Ins.Add("horafin", "@horafin", DataType.Parametro)
            Ins.Add("frecuencia", "@frecuencia", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)

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

            cmd.Parameters.Add(New SqlParameter("@IDNAVCONFIGDET", oBeI_nav_config_det.Idnavconfigdet))
            cmd.Parameters.Add(New SqlParameter("@IDNAVENT", oBeI_nav_config_det.IdNavEnt))
            cmd.Parameters.Add(New SqlParameter("@IDNAVCONFIGENC", oBeI_nav_config_det.IdNavConfigEnc))
            cmd.Parameters.Add(New SqlParameter("@DIA", oBeI_nav_config_det.Dia))
            cmd.Parameters.Add(New SqlParameter("@HORAINICIO", oBeI_nav_config_det.HoraInicio))
            cmd.Parameters.Add(New SqlParameter("@HORAFIN", oBeI_nav_config_det.HoraFin))
            cmd.Parameters.Add(New SqlParameter("@FRECUENCIA", oBeI_nav_config_det.Frecuencia))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeI_nav_config_det.Activo))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeI_nav_config_det.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeI_nav_config_det.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeI_nav_config_det.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeI_nav_config_det.User_mod))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeI_nav_config_det As clsBeI_nav_config_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("i_nav_config_det")
            Upd.Add("idnavconfigdet", "@idnavconfigdet", DataType.Parametro)
            Upd.Add("idnavent", "@idnavent", DataType.Parametro)
            Upd.Add("idnavconfigenc", "@idnavconfigenc", DataType.Parametro)
            Upd.Add("dia", "@dia", DataType.Parametro)
            Upd.Add("horainicio", "@horainicio", DataType.Parametro)
            Upd.Add("horafin", "@horafin", DataType.Parametro)
            Upd.Add("frecuencia", "@frecuencia", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Where("idnavconfigdet = @idnavconfigdet")

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

            cmd.Parameters.Add(New SqlParameter("@IDNAVCONFIGDET", oBeI_nav_config_det.Idnavconfigdet))
            cmd.Parameters.Add(New SqlParameter("@IDNAVENT", oBeI_nav_config_det.IdNavEnt))
            cmd.Parameters.Add(New SqlParameter("@IDNAVCONFIGENC", oBeI_nav_config_det.IdNavConfigEnc))
            cmd.Parameters.Add(New SqlParameter("@DIA", oBeI_nav_config_det.Dia))
            cmd.Parameters.Add(New SqlParameter("@HORAINICIO", oBeI_nav_config_det.HoraInicio))
            cmd.Parameters.Add(New SqlParameter("@HORAFIN", oBeI_nav_config_det.HoraFin))
            cmd.Parameters.Add(New SqlParameter("@FRECUENCIA", oBeI_nav_config_det.Frecuencia))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeI_nav_config_det.Activo))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeI_nav_config_det.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeI_nav_config_det.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeI_nav_config_det.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeI_nav_config_det.User_mod))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function


    Public Shared Function Eliminar(ByRef oBeI_nav_config_det As clsBeI_nav_config_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from I_nav_config_det" &
             "  Where(idnavconfigdet = @idnavconfigdet)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDNAVCONFIGDET", oBeI_nav_config_det.Idnavconfigdet))

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

            Const sp As String = " Delete from I_nav_config_det "
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

            Const sp As String = "SELECT * FROM I_nav_config_det"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt


        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeI_nav_config_det As clsBeI_nav_config_det) As Boolean

        Try

            Const sp As String = "SELECT * FROM I_nav_config_det" &
            " Where(idnavconfigdet = @idnavconfigdet)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDNAVCONFIGDET", oBeI_nav_config_det.IDNAVCONFIGDET))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeI_nav_config_det, dt.Rows(0))
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

    Public Shared Function GetAll() As List(Of clsBeI_nav_config_det)

        Try

            Dim lReturnList As New List(Of clsBeI_nav_config_det)
            Const sp As String = "SELECT * FROM I_nav_config_det"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_config_det As New clsBeI_nav_config_det

            For Each dr As DataRow In dt.Rows

                vBeI_nav_config_det = New clsBeI_nav_config_det
                Cargar(vBeI_nav_config_det, dr)
                lReturnList.Add(vBeI_nav_config_det)

            Next

            Return lReturnList



        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetList(ByRef pBeI_nav_config_det As clsBeI_nav_config_det) As List(Of clsBeI_nav_config_det)

        GetList = Nothing
        Try

            Const sp As String = "SELECT * FROM I_nav_config_det" &
            " Where(idnavconfigdet = @idnavconfigdet and idnavent=@idnavent and dia=@dia)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDNAVCONFIGDET", pBeI_nav_config_det.Idnavconfigdet))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDNAVENT", pBeI_nav_config_det.IdNavEnt))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@DIA", pBeI_nav_config_det.Dia))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                GetList = New List(Of clsBeI_nav_config_det)()
                Cargar(pBeI_nav_config_det, dt.Rows(0))
                GetList.Add(pBeI_nav_config_det)
            End If

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

            Const sp As String = "SELECT ISNULL(Max(idnavconfigdet),0) FROM I_nav_config_det"

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


    Public Shared Function GetSingle_By_Dia_And_Entidad(ByVal pConfiguracion_del_dia As clsBeI_nav_config_det) As clsBeI_nav_config_det

        GetSingle_By_Dia_And_Entidad = Nothing

        Try

            Const sp As String = "SELECT * FROM I_nav_config_det" &
            " Where(IdNavEnt = @IdNavEnt and dia=@Dia and activo=1) "

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdNavEnt", pConfiguracion_del_dia.IdNavEnt))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@Dia", pConfiguracion_del_dia.Dia))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                GetSingle_By_Dia_And_Entidad = New clsBeI_nav_config_det()
                Cargar(GetSingle_By_Dia_And_Entidad, dt.Rows(0))
            End If

        Catch ex As Exception

        End Try
    End Function


End Class
