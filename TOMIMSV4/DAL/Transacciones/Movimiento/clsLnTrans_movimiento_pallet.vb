Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_movimiento_pallet

    Public Shared Sub Cargar(ByRef oBeTrans_movimiento_pallet As clsBeTrans_movimiento_pallet, ByRef dr As DataRow)
        Try
            With oBeTrans_movimiento_pallet
                .Idmovimientopallet = IIf(IsDBNull(dr.Item("idmovimientopallet")), 0, dr.Item("idmovimientopallet"))
                .IdBodega = IIf(IsDBNull(dr.Item("idbodega")), 0, dr.Item("idbodega"))
                .Lp_origen = IIf(IsDBNull(dr.Item("lp_origen")), "", dr.Item("lp_origen"))
                .Lp_destino = IIf(IsDBNull(dr.Item("lp_destino")), "", dr.Item("lp_destino"))
                .Orientacion = IIf(IsDBNull(dr.Item("orientacion")), "", dr.Item("orientacion"))
                .Fecha = IIf(IsDBNull(dr.Item("fecha")), Nothing, dr.Item("fecha"))
                .Idusuario = IIf(IsDBNull(dr.Item("idusuario")), 0, dr.Item("idusuario"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_movimiento_pallet As clsBeTrans_movimiento_pallet, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand() With {.CommandType = CommandType.Text}

        Try

            Ins.Init("trans_movimiento_pallet")
            Ins.Add("idmovimientopallet", "@idmovimientopallet", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("lp_origen", "@lp_origen", DataType.Parametro)
            Ins.Add("lp_destino", "@lp_destino", DataType.Parametro)
            Ins.Add("orientacion", "@orientacion", DataType.Parametro)
            Ins.Add("fecha", "@fecha", DataType.Parametro)
            Ins.Add("idusuario", "@idusuario", DataType.Parametro)

            Dim sp As String = Ins.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMOVIMIENTOPALLET", oBeTrans_movimiento_pallet.Idmovimientopallet))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_movimiento_pallet.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@LP_ORIGEN", oBeTrans_movimiento_pallet.Lp_origen))
            cmd.Parameters.Add(New SqlParameter("@LP_DESTINO", oBeTrans_movimiento_pallet.Lp_destino))
            cmd.Parameters.Add(New SqlParameter("@ORIENTACION", oBeTrans_movimiento_pallet.Orientacion))
            cmd.Parameters.Add(New SqlParameter("@FECHA", oBeTrans_movimiento_pallet.Fecha))
            cmd.Parameters.Add(New SqlParameter("@IDUSUARIO", oBeTrans_movimiento_pallet.Idusuario))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeTrans_movimiento_pallet As clsBeTrans_movimiento_pallet, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand() With {.CommandType = CommandType.Text}

        Try

            Upd.Init("trans_movimiento_pallet")
            Upd.Add("idmovimientopallet", "@idmovimientopallet", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("lp_origen", "@lp_origen", DataType.Parametro)
            Upd.Add("lp_destino", "@lp_destino", DataType.Parametro)
            Upd.Add("orientacion", "@orientacion", DataType.Parametro)
            Upd.Add("fecha", "@fecha", DataType.Parametro)
            Upd.Add("idusuario", "@idusuario", DataType.Parametro)
            Upd.Where("idmovimientopallet = @idmovimientopallet")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMOVIMIENTOPALLET", oBeTrans_movimiento_pallet.Idmovimientopallet))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_movimiento_pallet.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@LP_ORIGEN", oBeTrans_movimiento_pallet.Lp_origen))
            cmd.Parameters.Add(New SqlParameter("@LP_DESTINO", oBeTrans_movimiento_pallet.Lp_destino))
            cmd.Parameters.Add(New SqlParameter("@ORIENTACION", oBeTrans_movimiento_pallet.Orientacion))
            cmd.Parameters.Add(New SqlParameter("@FECHA", oBeTrans_movimiento_pallet.Fecha))
            cmd.Parameters.Add(New SqlParameter("@IDUSUARIO", oBeTrans_movimiento_pallet.Idusuario))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeTrans_movimiento_pallet)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim lReturnList As New List(Of clsBeTrans_movimiento_pallet)
            Const sp As String = "SELECT * FROM Trans_movimiento_pallet"
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_movimiento_pallet As New clsBeTrans_movimiento_pallet

            For Each dr As DataRow In dt.Rows
                vBeTrans_movimiento_pallet = New clsBeTrans_movimiento_pallet
                Cargar(vBeTrans_movimiento_pallet, dr)
                lReturnList.Add(vBeTrans_movimiento_pallet)
            Next

            lTransaction.Commit()

            Return lReturnList

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lTransaction.Dispose()
            lConnection.Dispose()
        End Try


    End Function

    Public Shared Function GetSingle(ByRef pBeTrans_movimiento_pallet As clsBeTrans_movimiento_pallet) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        GetSingle = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Const sp As String = "SELECT * FROM Trans_movimiento_pallet" &
            " Where(idmovimientopallet = @idmovimientopallet)"

            Dim cnn As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, cnn) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDMOVIMIENTOPALLET", pBeTrans_movimiento_pallet.Idmovimientopallet))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTrans_movimiento_pallet, dt.Rows(0))
                GetSingle = True
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lTransaction.Dispose()
            lConnection.Dispose()
        End Try

    End Function

    Public Shared Function MaxID() As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(idmovimientopallet),0) FROM Trans_movimiento_pallet"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If
            End Using

            lTransaction.Commit()

            Return lMax

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lTransaction.Dispose()
            lConnection.Dispose()
        End Try

    End Function


    Public Shared Function MaxID(ByRef lConnection As SqlConnection,
                                 ByRef lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(idmovimientopallet),0) FROM Trans_movimiento_pallet"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If
            End Using

            Return lMax

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
