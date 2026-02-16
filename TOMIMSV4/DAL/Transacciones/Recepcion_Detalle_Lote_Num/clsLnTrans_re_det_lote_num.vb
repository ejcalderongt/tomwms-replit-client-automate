Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_re_det_lote_num

    Public Shared Sub Cargar(ByRef oBeTrans_re_det_lote_num As clsBeTrans_re_det_lote_num, ByRef dr As DataRow)
        Try
            With oBeTrans_re_det_lote_num
                .IdLoteNum = IIf(IsDBNull(dr.Item("IdLoteNum")), 0, dr.Item("IdLoteNum"))
                .IdRecepcionEnc = IIf(IsDBNull(dr.Item("IdRecepcionEnc")), 0, dr.Item("IdRecepcionEnc"))
                .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                .Codigo = IIf(IsDBNull(dr.Item("Codigo")), "", dr.Item("Codigo"))
                .Lote = IIf(IsDBNull(dr.Item("Lote")), "", dr.Item("Lote"))
                .Lote_Numerico = IIf(IsDBNull(dr.Item("lote_numerico")), 0, dr.Item("lote_numerico"))
                .FechaIngreso = IIf(IsDBNull(dr.Item("FechaIngreso")), Nothing, dr.Item("FechaIngreso"))
                .Cantidad = IIf(IsDBNull(dr.Item("Cantidad")), 0.0, dr.Item("Cantidad"))
            End With
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub
    Public Shared Function Insertar(ByRef oBeTrans_re_det_lote_num As clsBeTrans_re_det_lote_num, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_re_det_lote_num")
            Ins.Add("idlotenum", "@idlotenum", DataType.Parametro)
            Ins.Add("idrecepcionenc", "@idrecepcionenc", DataType.Parametro)
            Ins.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Ins.Add("codigo", "@codigo", DataType.Parametro)
            Ins.Add("lote", "@lote", DataType.Parametro)
            Ins.Add("lote_numerico", "@lote_numerico", DataType.Parametro)
            Ins.Add("fechaingreso", "@fechaingreso", DataType.Parametro)
            Ins.Add("cantidad", "@cantidad", DataType.Parametro)

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

            cmd.Parameters.Add(New SqlParameter("@IDLOTENUM", oBeTrans_re_det_lote_num.IdLoteNum))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeTrans_re_det_lote_num.IdRecepcionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_re_det_lote_num.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeTrans_re_det_lote_num.Codigo))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeTrans_re_det_lote_num.Lote))
            cmd.Parameters.Add(New SqlParameter("@LOTE_NUMERICO", oBeTrans_re_det_lote_num.Lote_Numerico))
            cmd.Parameters.Add(New SqlParameter("@FECHAINGRESO", oBeTrans_re_det_lote_num.FechaIngreso))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_re_det_lote_num.Cantidad))


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
    Public Shared Function Actualizar(ByRef oBeTrans_re_det_lote_num As clsBeTrans_re_det_lote_num, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_re_det_lote_num")
            Upd.Add("idlotenum", "@idlotenum", DataType.Parametro)
            Upd.Add("idrecepcionenc", "@idrecepcionenc", DataType.Parametro)
            Upd.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Upd.Add("codigo", "@codigo", DataType.Parametro)
            Upd.Add("lote", "@lote", DataType.Parametro)
            Upd.Add("lote_numerico", "@lote_numerico", DataType.Parametro)
            Upd.Add("fechaingreso", "@fechaingreso", DataType.Parametro)
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Where("IdLoteNum = @IdLoteNum")

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

            cmd.Parameters.Add(New SqlParameter("@IDLOTENUM", oBeTrans_re_det_lote_num.IdLoteNum))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeTrans_re_det_lote_num.IdRecepcionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_re_det_lote_num.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeTrans_re_det_lote_num.Codigo))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeTrans_re_det_lote_num.Lote))
            cmd.Parameters.Add(New SqlParameter("@LOTE_NUMERICO", oBeTrans_re_det_lote_num.Lote_Numerico))
            cmd.Parameters.Add(New SqlParameter("@FECHAINGRESO", oBeTrans_re_det_lote_num.FechaIngreso))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_re_det_lote_num.Cantidad))

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
    Public Shared Function Eliminar(ByRef oBeTrans_re_det_lote_num As clsBeTrans_re_det_lote_num, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_re_det_lote_num" &
             "  Where(IdLoteNum = @IdLoteNum)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDLOTENUM", oBeTrans_re_det_lote_num.IdLoteNum))

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

            Const sp As String = " Delete from Trans_re_det_lote_num"
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

            Const sp As String = "SELECT * FROM Trans_re_det_lote_num"
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
    Public Shared Function Obtener(ByRef oBeTrans_re_det_lote_num As clsBeTrans_re_det_lote_num) As Boolean

        Try

            Const sp As String = "SELECT * FROM Trans_re_det_lote_num" &
            " Where(IdLoteNum = @IdLoteNum)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDLOTENUM", oBeTrans_re_det_lote_num.IdLoteNum))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_re_det_lote_num, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function
    Public Shared Function GetAll() As List(Of clsBeTrans_re_det_lote_num)

        Try

            Dim lReturnList As New List(Of clsBeTrans_re_det_lote_num)
            Const sp As String = "SELECT * FROM Trans_re_det_lote_num"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_re_det_lote_num As New clsBeTrans_re_det_lote_num

            For Each dr As DataRow In dt.Rows
                vBeTrans_re_det_lote_num = New clsBeTrans_re_det_lote_num
                Cargar(vBeTrans_re_det_lote_num, dr)
                lReturnList.Add(vBeTrans_re_det_lote_num)
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
    Public Shared Function GetSingle(ByRef pBeTrans_re_det_lote_num As clsBeTrans_re_det_lote_num)

        Try

            Const sp As String = "SELECT * FROM Trans_re_det_lote_num" &
            " Where(IdLoteNum = @IdLoteNum)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDLOTENUM", pBeTrans_re_det_lote_num.IDLOTENUM))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTrans_re_det_lote_num, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function
    Public Shared Function MaxID() as Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdLoteNum),0) FROM Trans_re_det_lote_num"

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
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

End Class