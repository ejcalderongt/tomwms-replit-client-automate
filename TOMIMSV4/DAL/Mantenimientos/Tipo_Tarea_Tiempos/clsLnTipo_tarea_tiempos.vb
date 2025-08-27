Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTipo_tarea_tiempos

    Public Shared Sub Cargar(ByRef oBeTipo_tarea_tiempos As clsBeTipo_tarea_tiempos, ByRef dr As DataRow)
        Try
            With oBeTipo_tarea_tiempos
                .IdEmpresa = IIf(IsDBNull(dr.Item("IdEmpresa")), 0, dr.Item("IdEmpresa"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .IdTipoTarea = IIf(IsDBNull(dr.Item("IdTipoTarea")), 0, dr.Item("IdTipoTarea"))
                .TiempoMedioMinutos = IIf(IsDBNull(dr.Item("TiempoMedioMinutos")), 0.0, dr.Item("TiempoMedioMinutos"))
            End With
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTipo_tarea_tiempos As clsBeTipo_tarea_tiempos, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("tipo_tarea_tiempos")
            Ins.Add("idempresa", "@idempresa", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("idtipotarea", "@idtipotarea", DataType.Parametro)
            Ins.Add("tiempomediominutos", "@tiempomediominutos", DataType.Parametro)

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

            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeTipo_tarea_tiempos.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTipo_tarea_tiempos.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOTAREA", oBeTipo_tarea_tiempos.IdTipoTarea))
            cmd.Parameters.Add(New SqlParameter("@TIEMPOMEDIOMINUTOS", oBeTipo_tarea_tiempos.TiempoMedioMinutos))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

            oBeTipo_tarea_tiempos.IdEmpresa = CInt(cmd.Parameters("@IDEMPRESA").Value)

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeTipo_tarea_tiempos As clsBeTipo_tarea_tiempos, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("tipo_tarea_tiempos")
            Upd.Add("idempresa", "@idempresa", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("idtipotarea", "@idtipotarea", DataType.Parametro)
            Upd.Add("tiempomediominutos", "@tiempomediominutos", DataType.Parametro)
            Upd.Where("IdEmpresa = @IdEmpresa" &
                " AND IdBodega = @IdBodega" &
                " AND IdTipoTarea = @IdTipoTarea")

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

            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeTipo_tarea_tiempos.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTipo_tarea_tiempos.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOTAREA", oBeTipo_tarea_tiempos.IdTipoTarea))
            cmd.Parameters.Add(New SqlParameter("@TIEMPOMEDIOMINUTOS", oBeTipo_tarea_tiempos.TiempoMedioMinutos))

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


    Public Shared Function Eliminar(ByRef oBeTipo_tarea_tiempos As clsBeTipo_tarea_tiempos, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Tipo_tarea_tiempos" &
             "  Where(IdEmpresa = @IdEmpresa)" &
             "  AND (IdBodega = @IdBodega)" &
             "  AND (IdTipoTarea = @IdTipoTarea)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeTipo_tarea_tiempos.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTipo_tarea_tiempos.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOTAREA", oBeTipo_tarea_tiempos.IdTipoTarea))

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

            Const sp As String = " Delete from Tipo_tarea_tiempos"
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

            Const sp As String = "SELECT * FROM Tipo_tarea_tiempos"
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

    Public Shared Function Obtener(ByRef oBeTipo_tarea_tiempos As clsBeTipo_tarea_tiempos) As Boolean

        Try

            Const sp As String = "SELECT * FROM Tipo_tarea_tiempos" &
            " Where(IdEmpresa = @IdEmpresa)" &
            " AND (IdBodega = @IdBodega)" &
            " AND (IdTipoTarea = @IdTipoTarea)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeTipo_tarea_tiempos.IdEmpresa))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTipo_tarea_tiempos.IdBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTIPOTAREA", oBeTipo_tarea_tiempos.IdTipoTarea))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTipo_tarea_tiempos, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeTipo_tarea_tiempos)

        Try

            Dim lReturnList As New List(Of clsBeTipo_tarea_tiempos)
            Const sp As String = "SELECT * FROM Tipo_tarea_tiempos"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTipo_tarea_tiempos As New clsBeTipo_tarea_tiempos

            For Each dr As DataRow In dt.Rows
                vBeTipo_tarea_tiempos = New clsBeTipo_tarea_tiempos
                Cargar(vBeTipo_tarea_tiempos, dr)
                lReturnList.Add(vBeTipo_tarea_tiempos)
            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdEmpresa_And_IdBodega(ByVal pIdEmpresa As Integer,
                                                             ByVal pIdBodega As Integer) As List(Of clsBeTipo_tarea_tiempos)

        Get_All_By_IdEmpresa_And_IdBodega = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim lReturnList As New List(Of clsBeTipo_tarea_tiempos)

            Const sp As String = "SELECT * FROM Tipo_tarea_tiempos WHERE IdEmpresa =@IdEmpresa AND IdBodega = @IdBodega"

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.AddWithValue("@IdEmpresa", pIdEmpresa)
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTipo_tarea_tiempos As New clsBeTipo_tarea_tiempos

            For Each dr As DataRow In dt.Rows
                vBeTipo_tarea_tiempos = New clsBeTipo_tarea_tiempos
                Cargar(vBeTipo_tarea_tiempos, dr)
                lReturnList.Add(vBeTipo_tarea_tiempos)
            Next

            lTransaction.Commit()

            Return lReturnList

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_All_By_IdBodega(ByVal pIdBodega As Integer) As DataTable

        Get_All_By_IdBodega = Nothing

        Try

            Dim vSQL As String = "SELECT ttp.IdBodega, ttp.IdEmpresa, 
                        ttp.IdTipoTarea,emp.nombre as Empresa,bd.nombre as Bodega,
                        sis.Nombre as Tarea,ttp.TiempoMedioMinutos
                        FROM Tipo_tarea_tiempos ttp inner join 
                        empresa emp on emp.IdEmpresa = ttp.IdEmpresa inner join
                        bodega bd on ttp.IdBodega = bd.IdBodega inner join
                        sis_tipo_tarea sis on sis.IdTipoTarea = ttp.IdTipoTarea 
                        WHERE ttp.IdBodega=@IdBodega "

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

    Public Shared Function GetSingle(ByRef pBeTipo_tarea_tiempos As clsBeTipo_tarea_tiempos) As Boolean

        Try

            Const sp As String = "SELECT * FROM Tipo_tarea_tiempos" &
            " Where(IdEmpresa = @IdEmpresa)" &
            " AND (IdBodega = @IdBodega)" &
            " AND (IdTipoTarea = @IdTipoTarea)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDEMPRESA", pBeTipo_tarea_tiempos.IdEmpresa))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", pBeTipo_tarea_tiempos.IdBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTIPOTAREA", pBeTipo_tarea_tiempos.IdTipoTarea))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTipo_tarea_tiempos, dt.Rows(0))
                Return True
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdEmpresa),0) FROM Tipo_tarea_tiempos"

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

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
