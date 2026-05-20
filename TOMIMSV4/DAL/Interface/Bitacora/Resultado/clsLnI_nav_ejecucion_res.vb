Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnI_nav_ejecucion_res

    Public Shared Sub Cargar(ByRef oBeI_nav_ejecucion_res As clsBeI_nav_ejecucion_res, ByRef dr As DataRow)
        Try
            With oBeI_nav_ejecucion_res
                .IdEjecucionRes = IIf(IsDBNull(dr.Item("idejecucionres")), 0, dr.Item("idejecucionres"))
                .IdEjecucionEnc = IIf(IsDBNull(dr.Item("idejecucionenc")), 0, dr.Item("idejecucionenc"))
                .IdNavConfigDet = IIf(IsDBNull(dr.Item("idnavconfigdet")), 0, dr.Item("idnavconfigdet"))
                .Registros_ws = IIf(IsDBNull(dr.Item("registros_ws")), 0, dr.Item("registros_ws"))
                .Registros_ti = IIf(IsDBNull(dr.Item("registros_ti")), 0, dr.Item("registros_ti"))
                .Registros_WMS = IIf(IsDBNull(dr.Item("registros_wms")), 0, dr.Item("registros_wms"))
                .Exitosa = IIf(IsDBNull(dr.Item("exitosa")), False, dr.Item("exitosa"))
            End With
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function InsertarFromIn(ByRef oBeI_nav_ejecucion_res As clsBeI_nav_ejecucion_res, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("i_nav_ejecucion_res")
            Ins.Add("idejecucionenc", "@idejecucionenc", DataType.Parametro)
            Ins.Add("idnavconfigdet", "@idnavconfigdet", DataType.Parametro)
            Ins.Add("registros_ws", "@registros_ws", DataType.Parametro)
            Ins.Add("registros_ti", "@registros_ti", DataType.Parametro)
            Ins.Add("registros_wms", "@registros_wms", DataType.Parametro)
            Ins.Add("exitosa", "@exitosa", DataType.Parametro)

            '#EJCCKFK20260520: Cambio por Identity en tabla.
            Dim sp As String = Ins.SQLIdentity("idejecucionres")
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            '#EJC20191205: Trans_Ref03
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDEJECUCIONENC", oBeI_nav_ejecucion_res.IdEjecucionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDNAVCONFIGDET", oBeI_nav_ejecucion_res.IdNavConfigDet))
            cmd.Parameters.Add(New SqlParameter("@REGISTROS_WS", oBeI_nav_ejecucion_res.Registros_ws))
            cmd.Parameters.Add(New SqlParameter("@REGISTROS_TI", oBeI_nav_ejecucion_res.Registros_ti))
            cmd.Parameters.Add(New SqlParameter("@REGISTROS_WMS", oBeI_nav_ejecucion_res.Registros_WMS))
            cmd.Parameters.Add(New SqlParameter("@EXITOSA", oBeI_nav_ejecucion_res.Exitosa))

            '#EJCCKFK20260520: Cambio por Identity en tabla.
            Dim newId As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            oBeI_nav_ejecucion_res.IdEjecucionRes = newId

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return 1

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeI_nav_ejecucion_res As clsBeI_nav_ejecucion_res,
                                      Optional ByVal pConection As SqlConnection = Nothing,
                                      Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("i_nav_ejecucion_res")
            Upd.Add("idejecucionenc", "@idejecucionenc", DataType.Parametro)
            Upd.Add("idnavconfigdet", "@idnavconfigdet", DataType.Parametro)
            Upd.Add("registros_ws", "@registros_ws", DataType.Parametro)
            Upd.Add("registros_ti", "@registros_ti", DataType.Parametro)
            Upd.Add("registros_wms", "@registros_wms", DataType.Parametro)
            Upd.Add("exitosa", "@exitosa", DataType.Parametro)
            Upd.Where("idejecucionres = @idejecucionres")

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

            cmd.Parameters.Add(New SqlParameter("@IDEJECUCIONRES", oBeI_nav_ejecucion_res.IdEjecucionRes))
            cmd.Parameters.Add(New SqlParameter("@IDEJECUCIONENC", oBeI_nav_ejecucion_res.IdEjecucionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDNAVCONFIGDET", oBeI_nav_ejecucion_res.IdNavConfigDet))
            cmd.Parameters.Add(New SqlParameter("@REGISTROS_WS", oBeI_nav_ejecucion_res.Registros_ws))
            cmd.Parameters.Add(New SqlParameter("@REGISTROS_TI", oBeI_nav_ejecucion_res.Registros_ti))
            cmd.Parameters.Add(New SqlParameter("@REGISTROS_WMS", oBeI_nav_ejecucion_res.Registros_WMS))
            cmd.Parameters.Add(New SqlParameter("@EXITOSA", oBeI_nav_ejecucion_res.Exitosa))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()

        End Try

    End Function


    Public Shared Function Eliminar(ByRef oBeI_nav_ejecucion_res As clsBeI_nav_ejecucion_res, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from I_nav_ejecucion_res" &
             "  Where(idejecucionres = @idejecucionres)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            '#EJC20191205: Trans_Ref03
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDEJECUCIONRES", oBeI_nav_ejecucion_res.IdEjecucionRes))

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

            Const sp As String = " Delete from I_nav_ejecucion_res"
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

            Const sp As String = "SELECT * FROM I_nav_ejecucion_res"
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

    Public Shared Function Obtener(ByRef oBeI_nav_ejecucion_res As clsBeI_nav_ejecucion_res) As Boolean

        Try

            Const sp As String = "SELECT * FROM I_nav_ejecucion_res" &
            " Where(idejecucionres = @idejecucionres)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDEJECUCIONRES", oBeI_nav_ejecucion_res.IdEjecucionRes))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeI_nav_ejecucion_res, dt.Rows(0))
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

    Public Shared Function GetAll() As List(Of clsBeI_nav_ejecucion_res)

        Try

            Dim lReturnList As New List(Of clsBeI_nav_ejecucion_res)
            Const sp As String = "SELECT * FROM I_nav_ejecucion_res"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_ejecucion_res As New clsBeI_nav_ejecucion_res

            For Each dr As DataRow In dt.Rows

                vBeI_nav_ejecucion_res = New clsBeI_nav_ejecucion_res
                Cargar(vBeI_nav_ejecucion_res, dr)
                lReturnList.Add(vBeI_nav_ejecucion_res)

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

    Public Shared Function GetSingle(ByRef pBeI_nav_ejecucion_res As clsBeI_nav_ejecucion_res)

        Try

            Const sp As String = "SELECT * FROM I_nav_ejecucion_res" &
            " Where(idejecucionres = @idejecucionres)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDEJECUCIONRES", pBeI_nav_ejecucion_res.IdEjecucionRes))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeI_nav_ejecucion_res, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    'Public Shared Function MaxID(ByRef lConection As SqlConnection, ByRef lTransaction As SqlTransaction) as Integer

    '    Try

    '        Dim lMax As Integer = 0

    '        Const sp As String = "SELECT ISNULL(Max(idejecucionres),0) FROM I_nav_ejecucion_res"

    '        Using lCommand As New SqlCommand(sp, lConection, lTransaction) With {.CommandType = CommandType.Text}

    '            Dim lReturnValue As Object = lCommand.ExecuteScalar()

    '            If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
    '                lMax = CInt(lReturnValue)
    '            End If

    '        End Using

    '        Return lMax

    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Function

End Class
