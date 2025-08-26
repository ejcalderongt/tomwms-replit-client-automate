Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnBodega_monitor_parametro
    Implements IDisposable

    Public Shared Sub Cargar(ByRef oBeBodega_monitor_parametro As clsBeBodega_monitor_parametro, ByRef dr As DataRow)
        Try
            With oBeBodega_monitor_parametro
                .IdMonitor = IIf(IsDBNull(dr.Item("IdMonitor")), 0, dr.Item("IdMonitor"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .Nombre = IIf(IsDBNull(dr.Item("Nombre")), "", dr.Item("Nombre"))
                .TiempoActualizacion = IIf(IsDBNull(dr.Item("TiempoActualizacion")), 0, dr.Item("TiempoActualizacion"))
            End With
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Function Insertar(ByRef oBeBodega_monitor_parametro As clsBeBodega_monitor_parametro, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Ins.Init("bodega_monitor_parametro")
            Ins.Add("idmonitor", "@idmonitor", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("tiempoactualizacion", "@tiempoactualizacion", DataType.Parametro)

            Dim sp As String = Ins.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text


            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            cmd.Parameters.Add(New SqlParameter("@IDMONITOR", oBeBodega_monitor_parametro.IdMonitor))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeBodega_monitor_parametro.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeBodega_monitor_parametro.Nombre))
            cmd.Parameters.Add(New SqlParameter("@TIEMPOACTUALIZACION", oBeBodega_monitor_parametro.TiempoActualizacion))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
            Return rowsAffected

            oBeBodega_monitor_parametro.IdMonitor = CInt(cmd.Parameters("@IDMONITOR").Value)

        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Function Actualizar(ByRef oBeBodega_monitor_parametro As clsBeBodega_monitor_parametro, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("bodega_monitor_parametro")
            Upd.Add("idmonitor", "@idmonitor", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            'Upd.Add("nombre","@nombre", DataType.Parametro)
            Upd.Add("tiempoactualizacion", "@tiempoactualizacion", DataType.Parametro)
            Upd.Where("IdMonitor = @IdMonitor  " &
                "AND IdBodega = @IdBodega")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text


            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMONITOR", oBeBodega_monitor_parametro.IdMonitor))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeBodega_monitor_parametro.IdBodega))
            'cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeBodega_monitor_parametro.Nombre))
            cmd.Parameters.Add(New SqlParameter("@TIEMPOACTUALIZACION", oBeBodega_monitor_parametro.TiempoActualizacion))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
            Return rowsAffected

        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function


    Public Shared Function Eliminar(ByRef oBeBodega_monitor_parametro As clsBeBodega_monitor_parametro, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text


            Dim sp As String = " Delete from Bodega_monitor_parametro" &
             "  Where(IdMonitor = @IdMonitor)" &
             "  AND (IdBodega = @IdBodega)"


            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then

                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else

                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)


            End If


            cmd.Parameters.Add(New SqlParameter("@IDMONITOR", oBeBodega_monitor_parametro.IdMonitor))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeBodega_monitor_parametro.IdBodega))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
            Return rowsAffected

        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()
        End Try
    End Function

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Bodega_monitor_parametro"
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

    Public Shared Function Obtener(ByRef oBeBodega_monitor_parametro As clsBeBodega_monitor_parametro) As Boolean

        Try

            Dim sp As String = "SELECT * FROM Bodega_monitor_parametro" &
            " Where(IdMonitor = @IdMonitor)" &
            "AND (IdBodega = @IdBodega)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDMONITOR", oBeBodega_monitor_parametro.IdMonitor))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", oBeBodega_monitor_parametro.IdMonitor))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeBodega_monitor_parametro, dt.Rows(0))
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

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdMonitor),0) FROM bodega_monitor_parametro"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                'HS 07112017 Quité query dentro de SqlCommand.
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
            Throw ex
        End Try

    End Function



End Class
