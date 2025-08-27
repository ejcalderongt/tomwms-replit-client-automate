Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_pe_docu_ref_det

    Public Shared Sub Cargar(ByRef oBeTrans_pe_docu_ref_det As clsBeTrans_pe_docu_ref_det, ByRef dr As DataRow)
        Try
            With oBeTrans_pe_docu_ref_det
                .IdDocumentoRef = IIf(IsDBNull(dr.Item("IdDocumentoRef")), 0, dr.Item("IdDocumentoRef"))
                .IdDocumentoRefDet = IIf(IsDBNull(dr.Item("IdDocumentoRefDet")), 0, dr.Item("IdDocumentoRefDet"))
                .Codigo_producto = IIf(IsDBNull(dr.Item("codigo_producto")), "", dr.Item("codigo_producto"))
                .Nombre_producto = IIf(IsDBNull(dr.Item("nombre_producto")), "", dr.Item("nombre_producto"))
                .Cantidad = IIf(IsDBNull(dr.Item("cantidad")), 0.0, dr.Item("cantidad"))
                .Peso = IIf(IsDBNull(dr.Item("peso")), 0.0, dr.Item("peso"))
                .Umbas = IIf(IsDBNull(dr.Item("umbas")), "", dr.Item("umbas"))
                .Presentaacion = IIf(IsDBNull(dr.Item("presentaacion")), "", dr.Item("presentaacion"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_pe_docu_ref_det As clsBeTrans_pe_docu_ref_det, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_pe_docu_ref_det")
            Ins.Add("iddocumentoref", "@iddocumentoref", DataType.Parametro)
            Ins.Add("iddocumentorefdet", "@iddocumentorefdet", DataType.Parametro)
            Ins.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
            Ins.Add("nombre_producto", "@nombre_producto", DataType.Parametro)
            Ins.Add("cantidad", "@cantidad", DataType.Parametro)
            Ins.Add("peso", "@peso", DataType.Parametro)
            Ins.Add("umbas", "@umbas", DataType.Parametro)
            Ins.Add("presentaacion", "@presentaacion", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDDOCUMENTOREF", oBeTrans_pe_docu_ref_det.IdDocumentoRef))
            cmd.Parameters.Add(New SqlParameter("@IDDOCUMENTOREFDET", oBeTrans_pe_docu_ref_det.IdDocumentoRefDet))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeTrans_pe_docu_ref_det.Codigo_producto))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_PRODUCTO", oBeTrans_pe_docu_ref_det.Nombre_producto))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_pe_docu_ref_det.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeTrans_pe_docu_ref_det.Peso))
            cmd.Parameters.Add(New SqlParameter("@UMBAS", oBeTrans_pe_docu_ref_det.Umbas))
            cmd.Parameters.Add(New SqlParameter("@PRESENTAACION", oBeTrans_pe_docu_ref_det.Presentaacion))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection is Nothing Then lConnection.Dispose()
            If Not lTransaction is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeTrans_pe_docu_ref_det As clsBeTrans_pe_docu_ref_det, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_pe_docu_ref_det")
            Upd.Add("iddocumentoref", "@iddocumentoref", DataType.Parametro)
            Upd.Add("iddocumentorefdet", "@iddocumentorefdet", DataType.Parametro)
            Upd.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
            Upd.Add("nombre_producto", "@nombre_producto", DataType.Parametro)
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Add("peso", "@peso", DataType.Parametro)
            Upd.Add("umbas", "@umbas", DataType.Parametro)
            Upd.Add("presentaacion", "@presentaacion", DataType.Parametro)
            Upd.Where("IdDocumentoRef = @IdDocumentoRef" & _
                " AND IdDocumentoRefDet = @IdDocumentoRefDet")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDDOCUMENTOREF", oBeTrans_pe_docu_ref_det.IdDocumentoRef))
            cmd.Parameters.Add(New SqlParameter("@IDDOCUMENTOREFDET", oBeTrans_pe_docu_ref_det.IdDocumentoRefDet))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeTrans_pe_docu_ref_det.Codigo_producto))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_PRODUCTO", oBeTrans_pe_docu_ref_det.Nombre_producto))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_pe_docu_ref_det.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeTrans_pe_docu_ref_det.Peso))
            cmd.Parameters.Add(New SqlParameter("@UMBAS", oBeTrans_pe_docu_ref_det.Umbas))
            cmd.Parameters.Add(New SqlParameter("@PRESENTAACION", oBeTrans_pe_docu_ref_det.Presentaacion))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection is Nothing Then lConnection.Dispose()
            If Not lTransaction is Nothing Then lTransaction.Dispose()
        End Try

    End Function


    Public Shared Function Eliminar(ByRef oBeTrans_pe_docu_ref_det As clsBeTrans_pe_docu_ref_det, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_pe_docu_ref_det" & _
             "  Where(IdDocumentoRef = @IdDocumentoRef)" & _
             "  AND (IdDocumentoRefDet = @IdDocumentoRefDet)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDDOCUMENTOREF", oBeTrans_pe_docu_ref_det.IdDocumentoRef))
            cmd.Parameters.Add(New SqlParameter("@IDDOCUMENTOREFDET", oBeTrans_pe_docu_ref_det.IdDocumentoRefDet))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection is Nothing Then lConnection.Dispose()
            If Not lTransaction is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_pe_docu_ref_det"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection is Nothing Then lConnection.Dispose()
            If Not lTransaction is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_All() As List(Of clsBeTrans_pe_docu_ref_det)

        Dim lReturnList As New List(Of clsBeTrans_pe_docu_ref_det)

        Try

            Const sp As String = "SELECT * FROM Trans_pe_docu_ref_det"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_pe_docu_ref_det As New clsBeTrans_pe_docu_ref_det

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_pe_docu_ref_det = New clsBeTrans_pe_docu_ref_det()
                            Cargar(vBeTrans_pe_docu_ref_det, dr)
                            lReturnList.Add(vBeTrans_pe_docu_ref_det)
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Sub GetSingle(ByRef pBeTrans_pe_docu_ref_det As clsBeTrans_pe_docu_ref_det)

        Try

            Const sp As String = "SELECT * FROM Trans_pe_docu_ref_det" & _
            " Where(IdDocumentoRef = @IdDocumentoRef)" & _
            " AND (IdDocumentoRefDet = @IdDocumentoRefDet)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_pe_docu_ref_det As New clsBeTrans_pe_docu_ref_det

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTrans_pe_docu_ref_det, lDataTable.Rows(0))
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Public Shared Function MaxID() as Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdDocumentoRef),0) FROM Trans_pe_docu_ref_det"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()
                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lMax = CInt(lReturnValue)
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lMax

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function MaxID(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdDocumentoRef),0) FROM Trans_pe_docu_ref_det "

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
