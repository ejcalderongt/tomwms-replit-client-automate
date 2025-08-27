Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_inv_tramo

    Public Shared Sub Cargar(ByRef oBeTrans_inv_tramo As clsBeTrans_inv_tramo, ByRef dr As DataRow)
        Try

            With oBeTrans_inv_tramo
                .Idinventario = IIf(IsDBNull(dr.Item("idinventario")), 0, dr.Item("idinventario"))
                .Idtramo = IIf(IsDBNull(dr.Item("idtramo")), 0, dr.Item("idtramo"))
                .Det_idoperador = IIf(IsDBNull(dr.Item("det_idoperador")), 0, dr.Item("det_idoperador"))
                .Det_estado = IIf(IsDBNull(dr.Item("det_estado")), "", dr.Item("det_estado"))
                .Det_inicio = IIf(IsDBNull(dr.Item("det_inicio")), "01/01/1900", dr.Item("det_inicio"))
                .Det_fin = IIf(IsDBNull(dr.Item("det_fin")), "01/01/1900", dr.Item("det_fin"))
                .Res_idoperador = IIf(IsDBNull(dr.Item("res_idoperador")), 0, dr.Item("res_idoperador"))
                .Res_estado = IIf(IsDBNull(dr.Item("res_estado")), "", dr.Item("res_estado"))
                .Res_inicio = IIf(IsDBNull(dr.Item("res_inicio")), "01/01/1900", dr.Item("res_inicio"))
                .Res_fin = IIf(IsDBNull(dr.Item("res_fin")), "01/01/1900", dr.Item("res_fin"))
                .Aplicado = IIf(IsDBNull(dr.Item("aplicado")), False, dr.Item("aplicado"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
            End With
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_inv_tramo As clsBeTrans_inv_tramo, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_inv_tramo")
            Ins.Add("idinventario", "@idinventario", DataType.Parametro)
            Ins.Add("idtramo", "@idtramo", DataType.Parametro)
            Ins.Add("det_idoperador", "@det_idoperador", DataType.Parametro)
            Ins.Add("det_estado", "@det_estado", DataType.Parametro)
            Ins.Add("det_inicio", "@det_inicio", DataType.Parametro)
            Ins.Add("det_fin", "@det_fin", DataType.Parametro)
            Ins.Add("res_idoperador", "@res_idoperador", DataType.Parametro)
            Ins.Add("res_estado", "@res_estado", DataType.Parametro)
            Ins.Add("res_inicio", "@res_inicio", DataType.Parametro)
            Ins.Add("res_fin", "@res_fin", DataType.Parametro)
            Ins.Add("aplicado", "@aplicado", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)

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

            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIO", oBeTrans_inv_tramo.Idinventario))
            cmd.Parameters.Add(New SqlParameter("@IDTRAMO", oBeTrans_inv_tramo.Idtramo))
            cmd.Parameters.Add(New SqlParameter("@DET_IDOPERADOR", oBeTrans_inv_tramo.Det_idoperador))
            cmd.Parameters.Add(New SqlParameter("@DET_ESTADO", oBeTrans_inv_tramo.Det_estado))
            cmd.Parameters.Add(New SqlParameter("@DET_INICIO", oBeTrans_inv_tramo.Det_inicio))
            cmd.Parameters.Add(New SqlParameter("@DET_FIN", oBeTrans_inv_tramo.Det_fin))
            cmd.Parameters.Add(New SqlParameter("@RES_IDOPERADOR", oBeTrans_inv_tramo.Res_idoperador))
            cmd.Parameters.Add(New SqlParameter("@RES_ESTADO", oBeTrans_inv_tramo.Res_estado))
            cmd.Parameters.Add(New SqlParameter("@RES_INICIO", oBeTrans_inv_tramo.Res_inicio))
            cmd.Parameters.Add(New SqlParameter("@RES_FIN", oBeTrans_inv_tramo.Res_fin))
            cmd.Parameters.Add(New SqlParameter("@APLICADO", oBeTrans_inv_tramo.Aplicado))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_inv_tramo.IdBodega))


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

    Public Shared Function Actualizar(ByRef oBeTrans_inv_tramo As clsBeTrans_inv_tramo, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_inv_tramo")
            Upd.Add("idinventario", "@idinventario", DataType.Parametro)
            Upd.Add("idtramo", "@idtramo", DataType.Parametro)
            Upd.Add("det_idoperador", "@det_idoperador", DataType.Parametro)
            Upd.Add("det_estado", "@det_estado", DataType.Parametro)
            Upd.Add("det_inicio", "@det_inicio", DataType.Parametro)
            Upd.Add("det_fin", "@det_fin", DataType.Parametro)
            Upd.Add("res_idoperador", "@res_idoperador", DataType.Parametro)
            Upd.Add("res_estado", "@res_estado", DataType.Parametro)
            Upd.Add("res_inicio", "@res_inicio", DataType.Parametro)
            Upd.Add("res_fin", "@res_fin", DataType.Parametro)
            Upd.Add("aplicado", "@aplicado", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Where("idinventario = @idinventario
                       AND idtramo = @idtramo")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIO", oBeTrans_inv_tramo.Idinventario))
            cmd.Parameters.Add(New SqlParameter("@IDTRAMO", oBeTrans_inv_tramo.Idtramo))
            cmd.Parameters.Add(New SqlParameter("@DET_IDOPERADOR", oBeTrans_inv_tramo.Det_idoperador))
            cmd.Parameters.Add(New SqlParameter("@DET_ESTADO", oBeTrans_inv_tramo.Det_estado))
            cmd.Parameters.Add(New SqlParameter("@DET_INICIO", oBeTrans_inv_tramo.Det_inicio))
            cmd.Parameters.Add(New SqlParameter("@DET_FIN", oBeTrans_inv_tramo.Det_fin))
            cmd.Parameters.Add(New SqlParameter("@RES_IDOPERADOR", oBeTrans_inv_tramo.Res_idoperador))
            cmd.Parameters.Add(New SqlParameter("@RES_ESTADO", oBeTrans_inv_tramo.Res_estado))
            cmd.Parameters.Add(New SqlParameter("@RES_INICIO", oBeTrans_inv_tramo.Res_inicio))
            cmd.Parameters.Add(New SqlParameter("@RES_FIN", oBeTrans_inv_tramo.Res_fin))
            cmd.Parameters.Add(New SqlParameter("@APLICADO", oBeTrans_inv_tramo.Aplicado))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_inv_tramo.IdBodega))

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

    Public Shared Function Eliminar(ByRef oBeTrans_inv_tramo As clsBeTrans_inv_tramo, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_inv_tramo" &
             "  Where(idinventario = @idinventario)" &
             "  AND (idtramo = @idtramo)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIO", oBeTrans_inv_tramo.Idinventario))
            cmd.Parameters.Add(New SqlParameter("@IDTRAMO", oBeTrans_inv_tramo.Idtramo))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_inv_tramo"
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
            lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pIdTramo As Integer, ByVal pIdInv As Integer) As clsBeTrans_inv_tramo

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Dim vSQL As String = "SELECT * FROM trans_inv_tramo WHERE IdTramo=@IdTramo AND IdInventario=@IdInventario"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdTramo", pIdTramo)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdInventario", pIdInv)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        Dim Obj As clsBeTrans_inv_tramo

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Obj = New clsBeTrans_inv_tramo()
                            Cargar(Obj, lRow)
                            Return Obj

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return Nothing

        Catch ex As Exception
            Throw New Exception("Inventario_GetSingle: " & ex.Message)
        End Try

    End Function

End Class
