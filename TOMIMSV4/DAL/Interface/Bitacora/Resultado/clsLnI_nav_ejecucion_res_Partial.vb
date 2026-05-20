Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnI_nav_ejecucion_res

    Public Shared Function Insertar(ByRef oBeI_nav_ejecucion_res As clsBeI_nav_ejecucion_res,
                                    ByVal pConection As SqlConnection) As Integer

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
            Dim cmd As New SqlCommand(sp, pConection) With {.CommandType = CommandType.Text}

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

            Return 1

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeI_nav_ejecucion_res As clsBeI_nav_ejecucion_res,
                                      ByVal pConection As SqlConnection) As Integer

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
            Dim cmd As New SqlCommand(sp, pConection) With {.CommandType = CommandType.Text}

            cmd.Parameters.Add(New SqlParameter("@IDEJECUCIONRES", oBeI_nav_ejecucion_res.IdEjecucionRes))
            cmd.Parameters.Add(New SqlParameter("@IDEJECUCIONENC", oBeI_nav_ejecucion_res.IdEjecucionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDNAVCONFIGDET", oBeI_nav_ejecucion_res.IdNavConfigDet))
            cmd.Parameters.Add(New SqlParameter("@REGISTROS_WS", oBeI_nav_ejecucion_res.Registros_ws))
            cmd.Parameters.Add(New SqlParameter("@REGISTROS_TI", oBeI_nav_ejecucion_res.Registros_ti))
            cmd.Parameters.Add(New SqlParameter("@REGISTROS_WMS", oBeI_nav_ejecucion_res.Registros_WMS))
            cmd.Parameters.Add(New SqlParameter("@EXITOSA", oBeI_nav_ejecucion_res.Exitosa))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            Return rowsAffected

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Insertar(ByRef oBeI_nav_ejecucion_res As clsBeI_nav_ejecucion_res) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Ins.Init("i_nav_ejecucion_res")
            Ins.Add("idejecucionenc", "@idejecucionenc", DataType.Parametro)
            Ins.Add("idnavconfigdet", "@idnavconfigdet", DataType.Parametro)
            Ins.Add("registros_ws", "@registros_ws", DataType.Parametro)
            Ins.Add("registros_ti", "@registros_ti", DataType.Parametro)
            Ins.Add("registros_wms", "@registros_wms", DataType.Parametro)
            Ins.Add("exitosa", "@exitosa", DataType.Parametro)

            '#EJCCKFK20260520: Cambio por Identity en tabla.
            Dim sp As String = Ins.SQLIdentity("idejecucionres")
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

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

            lTransaction.Commit()

            Insertar = 1

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeI_nav_ejecucion_res As clsBeI_nav_ejecucion_res) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Upd.Init("i_nav_ejecucion_res")
            Upd.Add("idejecucionenc", "@idejecucionenc", DataType.Parametro)
            Upd.Add("idnavconfigdet", "@idnavconfigdet", DataType.Parametro)
            Upd.Add("registros_ws", "@registros_ws", DataType.Parametro)
            Upd.Add("registros_ti", "@registros_ti", DataType.Parametro)
            Upd.Add("registros_wms", "@registros_wms", DataType.Parametro)
            Upd.Add("exitosa", "@exitosa", DataType.Parametro)
            Upd.Where("idejecucionres = @idejecucionres")

            Dim sp As String = Upd.SQL()
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

            cmd.Parameters.Add(New SqlParameter("@IDEJECUCIONRES", oBeI_nav_ejecucion_res.IdEjecucionRes))
            cmd.Parameters.Add(New SqlParameter("@IDEJECUCIONENC", oBeI_nav_ejecucion_res.IdEjecucionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDNAVCONFIGDET", oBeI_nav_ejecucion_res.IdNavConfigDet))
            cmd.Parameters.Add(New SqlParameter("@REGISTROS_WS", oBeI_nav_ejecucion_res.Registros_ws))
            cmd.Parameters.Add(New SqlParameter("@REGISTROS_TI", oBeI_nav_ejecucion_res.Registros_ti))
            cmd.Parameters.Add(New SqlParameter("@REGISTROS_WMS", oBeI_nav_ejecucion_res.Registros_WMS))
            cmd.Parameters.Add(New SqlParameter("@EXITOSA", oBeI_nav_ejecucion_res.Exitosa))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            lTransaction.Commit()

            Actualizar = rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

End Class
