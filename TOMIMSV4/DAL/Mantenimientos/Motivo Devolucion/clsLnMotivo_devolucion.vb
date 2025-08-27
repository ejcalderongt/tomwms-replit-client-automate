Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnMotivo_devolucion

    Public Shared Sub Cargar(ByRef oBeMotivo_devolucion As clsBeMotivo_devolucion, ByRef dr As DataRow)
        Try
            With oBeMotivo_devolucion
                .IdMotivoDevolucion = IIf(IsDBNull(dr.Item("IdMotivoDevolucion")), 0, dr.Item("IdMotivoDevolucion"))
                .IdEmpresa = IIf(IsDBNull(dr.Item("IdEmpresa")), 0, dr.Item("IdEmpresa"))
                .IdPropietario = IIf(IsDBNull(dr.Item("IdPropietario")), 0, dr.Item("IdPropietario"))
                .Nombre = IIf(IsDBNull(dr.Item("Nombre")), "", dr.Item("Nombre"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Es_detalle = IIf(IsDBNull(dr.Item("es_detalle")), False, dr.Item("es_detalle"))
            End With
        Catch ex As Exception
            Throw New Exception("Motivo_devolucion_Cargar: " & ex.Message)
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeMotivo_devolucion As clsBeMotivo_devolucion, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("motivo_devolucion")
            Ins.Add("idmotivodevolucion", "@idmotivodevolucion", DataType.Parametro)
            Ins.Add("idempresa", "@idempresa", DataType.Parametro)
            Ins.Add("idpropietario", "@idpropietario", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("es_detalle", "@es_detalle", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMOTIVODEVOLUCION", oBeMotivo_devolucion.IdMotivoDevolucion))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeMotivo_devolucion.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeMotivo_devolucion.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeMotivo_devolucion.Nombre))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeMotivo_devolucion.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeMotivo_devolucion.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeMotivo_devolucion.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeMotivo_devolucion.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeMotivo_devolucion.Activo))
            cmd.Parameters.Add(New SqlParameter("@ES_DETALLE", oBeMotivo_devolucion.Es_detalle))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

            oBeMotivo_devolucion.IdMotivoDevolucion = CInt(cmd.Parameters("@IDMOTIVODEVOLUCION").Value)

        Catch ex As Exception
            Throw New Exception("Motivo_devolucion_Insertar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeMotivo_devolucion As clsBeMotivo_devolucion, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("motivo_devolucion")
            Upd.Add("idmotivodevolucion", "@idmotivodevolucion", DataType.Parametro)
            Upd.Add("idempresa", "@idempresa", DataType.Parametro)
            Upd.Add("idpropietario", "@idpropietario", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("es_detalle", "@es_detalle", DataType.Parametro)
            Upd.Where("IdMotivoDevolucion = @IdMotivoDevolucion")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}


            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMOTIVODEVOLUCION", oBeMotivo_devolucion.IdMotivoDevolucion))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeMotivo_devolucion.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeMotivo_devolucion.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeMotivo_devolucion.Nombre))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeMotivo_devolucion.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeMotivo_devolucion.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeMotivo_devolucion.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeMotivo_devolucion.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeMotivo_devolucion.Activo))
            cmd.Parameters.Add(New SqlParameter("@ES_DETALLE", oBeMotivo_devolucion.Es_detalle))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception("Motivo_devolucion_Actualizar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function


    Public Shared Function Eliminar(ByRef oBeMotivo_devolucion As clsBeMotivo_devolucion, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try


            Const sp As String = " Delete from Motivo_devolucion Where(IdMotivoDevolucion = @IdMotivoDevolucion)"


            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then

                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else

                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)


            End If


            cmd.Parameters.Add(New SqlParameter("@IDMOTIVODEVOLUCION", oBeMotivo_devolucion.IdMotivoDevolucion))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception("Motivo_devolucion_Eliminar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try
    End Function

    Public Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Motivo_devolucion"

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
            Throw New Exception("Motivo_devolucion_Eliminar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            lConnection.Dispose
        End Try

    End Function

    Public Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Motivo_devolucion"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)

            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw New Exception("Motivo_devolucion_Listar: " & ex.Message)
        End Try

    End Function

    Public Function Obtener(ByRef oBeMotivo_devolucion As clsBeMotivo_devolucion) As Boolean

        Try

            Const sp As String = "SELECT * FROM Motivo_devolucion" &
            " Where(IdMotivoDevolucion = @IdMotivoDevolucion)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)


            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDMOTIVODEVOLUCION", oBeMotivo_devolucion.IdMotivoDevolucion))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeMotivo_devolucion, dt.Rows(0))
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

    Public Shared Function GetAll() As List(Of clsBeMotivo_devolucion)

        Try

            Dim lReturnList As New List(Of clsBeMotivo_devolucion)
            Const sp As String = "SELECT * FROM Motivo_devolucion"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeMotivo_devolucion As New clsBeMotivo_devolucion

            For Each dr As DataRow In dt.Rows

                vBeMotivo_devolucion = New clsBeMotivo_devolucion
                Cargar(vBeMotivo_devolucion, dr)
                lReturnList.Add(vBeMotivo_devolucion)

            Next

            lConnection.Dispose()
            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Throw New Exception("Motivo_devolucion_GetAll: " & ex.Message)
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeMotivo_devolucion As clsBeMotivo_devolucion)

        Try

            Const sp As String = "SELECT * FROM Motivo_devolucion" &
            " Where(IdMotivoDevolucion = @IdMotivoDevolucion)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDMOTIVODEVOLUCION", pBeMotivo_devolucion.IdMotivoDevolucion))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeMotivo_devolucion, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
