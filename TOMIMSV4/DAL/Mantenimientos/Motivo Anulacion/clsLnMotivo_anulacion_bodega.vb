
Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnMotivo_anulacion_bodega

    Public Shared Sub Cargar(ByRef oBeMotivo_anulacion_bodega As clsBeMotivo_anulacion_bodega, ByRef dr As DataRow)
        Try
            With oBeMotivo_anulacion_bodega
                .IdMotivoAnulacionBodega = IIf(IsDBNull(dr.Item("IdMotivoAnulacionBodega")), 0, dr.Item("IdMotivoAnulacionBodega"))
                .IdMotivoAnulacion = IIf(IsDBNull(dr.Item("IdMotivoAnulacion")), 0, dr.Item("IdMotivoAnulacion"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
            End With
        Catch ex As Exception
            Throw New Exception("Motivo_anulacion_bodega_Cargar: " & ex.Message)
        End Try
    End Sub

    Public Function Insertar(ByRef oBeMotivo_anulacion_bodega As clsBeMotivo_anulacion_bodega, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("motivo_anulacion_bodega")
            Ins.Add("idmotivoanulacionbodega", "@idmotivoanulacionbodega", DataType.Parametro)
            Ins.Add("idmotivoanulacion", "@idmotivoanulacion", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMOTIVOANULACIONBODEGA", oBeMotivo_anulacion_bodega.IdMotivoAnulacionBodega))
            cmd.Parameters.Add(New SqlParameter("@IDMOTIVOANULACION", oBeMotivo_anulacion_bodega.IdMotivoAnulacion))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeMotivo_anulacion_bodega.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeMotivo_anulacion_bodega.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeMotivo_anulacion_bodega.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeMotivo_anulacion_bodega.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeMotivo_anulacion_bodega.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeMotivo_anulacion_bodega.Fec_mod))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

            oBeMotivo_anulacion_bodega.IdMotivoAnulacionBodega = CInt(cmd.Parameters("@IDMOTIVOANULACIONBODEGA").Value)

        Catch ex As Exception
            Throw New Exception("Motivo_anulacion_bodega_Insertar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

    Public Function Actualizar(ByRef oBeMotivo_anulacion_bodega As clsBeMotivo_anulacion_bodega, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("motivo_anulacion_bodega")
            Upd.Add("idmotivoanulacionbodega", "@idmotivoanulacionbodega", DataType.Parametro)
            Upd.Add("idmotivoanulacion", "@idmotivoanulacion", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("IdMotivoAnulacionBodega = @IdMotivoAnulacionBodega")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}


            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMOTIVOANULACIONBODEGA", oBeMotivo_anulacion_bodega.IdMotivoAnulacionBodega))
            cmd.Parameters.Add(New SqlParameter("@IDMOTIVOANULACION", oBeMotivo_anulacion_bodega.IdMotivoAnulacion))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeMotivo_anulacion_bodega.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeMotivo_anulacion_bodega.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeMotivo_anulacion_bodega.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeMotivo_anulacion_bodega.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeMotivo_anulacion_bodega.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeMotivo_anulacion_bodega.Fec_mod))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception("Motivo_anulacion_bodega_Actualizar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function


    Public Shared Function Eliminar(ByRef oBeMotivo_anulacion_bodega As clsBeMotivo_anulacion_bodega, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Motivo_anulacion_bodega" &
         "  Where(IdMotivoAnulacionBodega = @IdMotivoAnulacionBodega)"


            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            cmd.Parameters.Add(New SqlParameter("@IDMOTIVOANULACIONBODEGA", oBeMotivo_anulacion_bodega.IdMotivoAnulacionBodega))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception("Motivo_anulacion_bodega_Eliminar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try
    End Function

    Public Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Motivo_anulacion_bodega"

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
            Throw New Exception("Motivo_anulacion_bodega_Eliminar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

    Public Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Motivo_anulacion_bodega"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)

            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw New Exception("Motivo_anulacion_bodega_Listar: " & ex.Message)
        End Try

    End Function

    Public Function Obtener(ByRef oBeMotivo_anulacion_bodega As clsBeMotivo_anulacion_bodega) As Boolean

        Try

            Const sp As String = "SELECT * FROM Motivo_anulacion_bodega" &
        " Where(IdMotivoAnulacionBodega = @IdMotivoAnulacionBodega)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)


            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDMOTIVOANULACIONBODEGA", oBeMotivo_anulacion_bodega.IdMotivoAnulacionBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeMotivo_anulacion_bodega, dt.Rows(0))
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

    Public Shared Function GetAll() As List(Of clsBeMotivo_anulacion_bodega)

        Try

            Dim lReturnList As New List(Of clsBeMotivo_anulacion_bodega)
            Const sp As String = "SELECT * FROM Motivo_anulacion_bodega"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeMotivo_anulacion_bodega As New clsBeMotivo_anulacion_bodega

            For Each dr As DataRow In dt.Rows

                vBeMotivo_anulacion_bodega = New clsBeMotivo_anulacion_bodega
                Cargar(vBeMotivo_anulacion_bodega, dr)
                lReturnList.Add(vBeMotivo_anulacion_bodega)

            Next

            lConnection.Dispose()
            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Throw New Exception("Motivo_anulacion_bodega_GetAll: " & ex.Message)
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeMotivo_anulacion_bodega As clsBeMotivo_anulacion_bodega) As Boolean

        GetSingle = False

        Try

            Const sp As String = "SELECT * FROM Motivo_anulacion_bodega" &
        " Where(IdMotivoAnulacionBodega = @IdMotivoAnulacionBodega)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDMOTIVOANULACIONBODEGA", pBeMotivo_anulacion_bodega.IdMotivoAnulacionBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeMotivo_anulacion_bodega, dt.Rows(0))
                Return True
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_With_Detail(ByRef pBeMotivo_anulacion_bodega As clsBeMotivo_anulacion_bodega) As Boolean

        Get_Single_With_Detail = False

        Try

            Const sp As String = "SELECT * FROM Motivo_anulacion_bodega" &
        " Where(IdMotivoAnulacionBodega = @IdMotivoAnulacionBodega)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDMOTIVOANULACIONBODEGA", pBeMotivo_anulacion_bodega.IdMotivoAnulacionBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeMotivo_anulacion_bodega, dt.Rows(0))
                pBeMotivo_anulacion_bodega.MotivoAnulacion.IdMotivoAnulacion = pBeMotivo_anulacion_bodega.IdMotivoAnulacion
                pBeMotivo_anulacion_bodega.MotivoAnulacion = clsLnMotivo_anulacion.GetSingle(pBeMotivo_anulacion_bodega.IdMotivoAnulacion)
                Return True
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
