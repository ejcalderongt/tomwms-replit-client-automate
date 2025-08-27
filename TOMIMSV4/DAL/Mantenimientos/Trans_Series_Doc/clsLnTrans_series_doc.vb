Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_series_doc

    Public Shared Sub Cargar(ByRef oBeTrans_series_doc As clsBeTrans_series_doc, ByRef dr As DataRow)
        Try
            With oBeTrans_series_doc
                .IdTransSerieDoc = IIf(IsDBNull(dr.Item("IdTransSerieDoc")), 0, dr.Item("IdTransSerieDoc"))
                .Serie = IIf(IsDBNull(dr.Item("Serie")), "", dr.Item("Serie"))
                .Tipo_Doc = IIf(IsDBNull(dr.Item("Tipo_Doc")), "", dr.Item("Tipo_Doc"))
                .IdTipoTrans = IIf(IsDBNull(dr.Item("IdTipoTrans")), 0, dr.Item("IdTipoTrans"))
                .Inicial = IIf(IsDBNull(dr.Item("Inicial")), 0, dr.Item("Inicial"))
                .Final = IIf(IsDBNull(dr.Item("Final")), 0, dr.Item("Final"))
                .Actual = IIf(IsDBNull(dr.Item("Actual")), 0, dr.Item("Actual"))
                .Activo = IIf(IsDBNull(dr.Item("Activo")), False, dr.Item("Activo"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .UserAgr = IIf(IsDBNull(dr.Item("UserAgr")), "", dr.Item("UserAgr"))
                .FecAgr = IIf(IsDBNull(dr.Item("FecAgr")), Date.Now, dr.Item("FecAgr"))
                .UserMod = IIf(IsDBNull(dr.Item("UserMod")), "", dr.Item("UserMod"))
                .FecMod = IIf(IsDBNull(dr.Item("FecMod")), Date.Now, dr.Item("FecMod"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_series_doc As clsBeTrans_series_doc, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_series_doc")
            Ins.Add("IdTransSerieDoc", "@IdTransSerieDoc", DataType.Parametro)
            Ins.Add("serie", "@serie", DataType.Parametro)
            Ins.Add("tipo_doc", "@tipo_doc", DataType.Parametro)
            Ins.Add("idtipotrans", "@idtipotrans", DataType.Parametro)
            Ins.Add("inicial", "@inicial", DataType.Parametro)
            Ins.Add("final", "@final", DataType.Parametro)
            Ins.Add("actual", "@actual", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("useragr", "@useragr", DataType.Parametro)
            Ins.Add("fecagr", "@fecagr", DataType.Parametro)
            Ins.Add("usermod", "@usermod", DataType.Parametro)
            Ins.Add("fecmod", "@fecmod", DataType.Parametro)

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

            cmd.Parameters.Add(New SqlParameter("@IDTRANSSERIEDOC", oBeTrans_series_doc.IdTransSerieDoc))
            cmd.Parameters.Add(New SqlParameter("@SERIE", oBeTrans_series_doc.Serie))
            cmd.Parameters.Add(New SqlParameter("@TIPO_DOC", oBeTrans_series_doc.Tipo_Doc))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOTRANS", oBeTrans_series_doc.IdTipoTrans))
            cmd.Parameters.Add(New SqlParameter("@INICIAL", oBeTrans_series_doc.Inicial))
            cmd.Parameters.Add(New SqlParameter("@FINAL", oBeTrans_series_doc.Final))
            cmd.Parameters.Add(New SqlParameter("@ACTUAL", oBeTrans_series_doc.Actual))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_series_doc.Activo))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_series_doc.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@USERAGR", oBeTrans_series_doc.UserAgr))
            cmd.Parameters.Add(New SqlParameter("@FECAGR", oBeTrans_series_doc.FecAgr))
            cmd.Parameters.Add(New SqlParameter("@USERMOD", oBeTrans_series_doc.UserMod))
            cmd.Parameters.Add(New SqlParameter("@FECMOD", oBeTrans_series_doc.FecMod))

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

    Public Shared Function Actualizar(ByRef oBeTrans_series_doc As clsBeTrans_series_doc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_series_doc")
            Upd.Add("IdTransSerieDoc", "@IdTransSerieDoc", DataType.Parametro)
            Upd.Add("serie", "@serie", DataType.Parametro)
            Upd.Add("tipo_doc", "@tipo_doc", DataType.Parametro)
            Upd.Add("idtipotrans", "@idtipotrans", DataType.Parametro)
            Upd.Add("inicial", "@inicial", DataType.Parametro)
            Upd.Add("final", "@final", DataType.Parametro)
            Upd.Add("actual", "@actual", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("useragr", "@useragr", DataType.Parametro)
            Upd.Add("fecagr", "@fecagr", DataType.Parametro)
            Upd.Add("usermod", "@usermod", DataType.Parametro)
            Upd.Add("fecmod", "@fecmod", DataType.Parametro)
            Upd.Where("IdTransSerieDoc = @IdTransSerieDoc")

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

            cmd.Parameters.Add(New SqlParameter("@IDTRANSSERIEDOC", oBeTrans_series_doc.IdTransSerieDoc))
            cmd.Parameters.Add(New SqlParameter("@SERIE", oBeTrans_series_doc.Serie))
            cmd.Parameters.Add(New SqlParameter("@TIPO_DOC", oBeTrans_series_doc.Tipo_Doc))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOTRANS", oBeTrans_series_doc.IdTipoTrans))
            cmd.Parameters.Add(New SqlParameter("@INICIAL", oBeTrans_series_doc.Inicial))
            cmd.Parameters.Add(New SqlParameter("@FINAL", oBeTrans_series_doc.Final))
            cmd.Parameters.Add(New SqlParameter("@ACTUAL", oBeTrans_series_doc.Actual))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_series_doc.Activo))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_series_doc.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@USERAGR", oBeTrans_series_doc.UserAgr))
            cmd.Parameters.Add(New SqlParameter("@FECAGR", oBeTrans_series_doc.FecAgr))
            cmd.Parameters.Add(New SqlParameter("@USERMOD", oBeTrans_series_doc.UserMod))
            cmd.Parameters.Add(New SqlParameter("@FECMOD", oBeTrans_series_doc.FecMod))

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


    Public Shared Function Eliminar(ByRef oBeTrans_series_doc As clsBeTrans_series_doc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_series_doc" &
             "  Where(IdCorrelativo = @IdCorrelativo)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCORRELATIVO", oBeTrans_series_doc.IdTransSerieDoc))

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

            Const sp As String = " Delete from Trans_series_doc"
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

            Const sp As String = "SELECT * FROM Trans_series_doc"
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

    Public Shared Function Obtener(ByRef oBeTrans_series_doc As clsBeTrans_series_doc) As Boolean

        Try

            Const sp As String = "SELECT * FROM Trans_series_doc" &
            " Where(IdCorrelativo = @IdCorrelativo)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDCORRELATIVO", oBeTrans_series_doc.IdTransSerieDoc))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_series_doc, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeTrans_series_doc)

        Try

            Dim lReturnList As New List(Of clsBeTrans_series_doc)
            Const sp As String = "SELECT * FROM Trans_series_doc"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_series_doc As New clsBeTrans_series_doc

            For Each dr As DataRow In dt.Rows
                vBeTrans_series_doc = New clsBeTrans_series_doc
                Cargar(vBeTrans_series_doc, dr)
                lReturnList.Add(vBeTrans_series_doc)
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

    Public Shared Function GetSingle(ByRef pBeTrans_series_doc As clsBeTrans_series_doc)

        Try

            Const sp As String = "SELECT * FROM Trans_series_doc" &
            " Where(IdTransSerieDoc = @IdTransSerieDoc)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdTransSerieDoc", pBeTrans_series_doc.IdTransSerieDoc))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTrans_series_doc, dt.Rows(0))
            End If

            Return pBeTrans_series_doc

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function MaxID() as Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdTransSerieDoc),0) FROM Trans_series_doc"

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
