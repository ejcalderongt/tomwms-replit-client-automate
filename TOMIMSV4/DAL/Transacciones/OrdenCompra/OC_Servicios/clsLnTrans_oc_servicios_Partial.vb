Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnTrans_oc_servicios

    Public Shared Function Exist(ByVal pIdAcuerdoDet As Integer,
                                 ByVal pIdOrdenCompraEnc As Integer,
                                 ByVal pConnection As SqlConnection,
                                 ByVal pTransaction As SqlTransaction) As Boolean

        Exist = False

        Try

            Const sp As String = "Select IdOrdenCompraEnc from trans_oc_servicios
                                  Where IdAcuerdoDet  = @IdAcuerdoDet  AND IdOrdenCompraEnc = @pIdOrdenCompraEnc"

            Dim cmd As New SqlCommand(sp, pConnection) With {.CommandType = CommandType.Text, .Transaction = pTransaction}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdAcuerdoDet ", pIdAcuerdoDet))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@pIdOrdenCompraEnc", pIdOrdenCompraEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt.Rows.Count > 0

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdOrdenCompraEnc_DT(ByVal pIdOrdenCompraEnc As Integer) As DataTable

        Dim lReturnList As New List(Of clsBeTarifa_tipo_transaccion_det)

        Get_All_By_IdOrdenCompraEnc_DT = Nothing

        Try

            Const sp As String = " Select *
                                   FROM trans_oc_servicios 
								   WHERE IdOrdenCompraEnc = @pIdOrdenCompraEnc "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@pIdOrdenCompraEnc", pIdOrdenCompraEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)
                        Return lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    '#GT03062024: se usa para cargar los servicios en la OC y en la prefactura.
    Public Shared Function Get_All_By_IdOrdenCompraEnc(ByVal pIdOrdenCompraEnc As Integer, Optional ByVal pIdAcuerdoEnc As Integer = 0) As List(Of clsBeTrans_oc_servicios)

        Get_All_By_IdOrdenCompraEnc = Nothing

        Try

            'lReturnList = Nothing

            Dim sp As String = "SELECT * FROM Trans_oc_servicios 
										   where IdOrdenCompraEnc=@pIdOrdenCompraEnc "


            If pIdAcuerdoEnc > 0 Then
                sp += " and IdAcuerdo=@pIdAcuerdoEnc"
            End If

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@pIdOrdenCompraEnc", pIdOrdenCompraEnc)
                        If pIdAcuerdoEnc > 0 Then
                            lDTA.SelectCommand.Parameters.AddWithValue("@pIdAcuerdoEnc", pIdAcuerdoEnc)
                        End If
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_oc_servicios As New clsBeTrans_oc_servicios

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Get_All_By_IdOrdenCompraEnc = New List(Of clsBeTrans_oc_servicios)

                            For Each dr As DataRow In lDataTable.Rows
                                vBeTrans_oc_servicios = New clsBeTrans_oc_servicios()
                                Cargar(vBeTrans_oc_servicios, dr)
                                Get_All_By_IdOrdenCompraEnc.Add(vBeTrans_oc_servicios)
                            Next
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            'Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar_By_OC_IdServicio(ByRef oBeTrans_oc_servicios As clsBeTrans_oc_servicios, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_oc_servicios")
            Upd.Add("idordencompraenc", "@idordencompraenc", DataType.Parametro)
            Upd.Add("idordencompraservicio", "@idordencompraservicio", DataType.Parametro)
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("observacion", "@observacion", DataType.Parametro)
            Upd.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Upd.Where("idservicio = @idservicio AND idordencompraenc = @idordencompraenc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_oc_servicios.IdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRASERVICIO", oBeTrans_oc_servicios.IdOrdenCompraServicio))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_oc_servicios.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_oc_servicios.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_oc_servicios.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_oc_servicios.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_oc_servicios.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACION", oBeTrans_oc_servicios.Observacion))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_oc_servicios.IdPropietarioBodega))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    '#GT14082024: actualizar solo la cantidad, si deciden cobrar el mismo servicio mas de una vez
    Public Shared Function Actualizar_Servicio_By_IdServicio(ByRef oBeTrans_oc_servicios As clsBeTrans_oc_servicios, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_oc_servicios")
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("fecha_servicio", "@fecha_servicio", DataType.Parametro)

            Upd.Where("IDORDENCOMPRASERVICIO = @IDORDENCOMPRASERVICIO AND IDORDENCOMPRAENC = @IDORDENCOMPRAENC")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_oc_servicios.IdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRASERVICIO", oBeTrans_oc_servicios.IdOrdenCompraServicio))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_oc_servicios.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_oc_servicios.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_oc_servicios.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@FECHA_SERVICIO", oBeTrans_oc_servicios.Fecha_Servicio))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function MaxID(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdOrdenCompraServicio),0) FROM Trans_oc_servicios "

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

    Public Shared Function Eliminar_By_IdOrdenCompraServicio(ByVal IdOrdenCompraServicio As Integer, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_oc_servicios" &
             "  Where(IdOrdenCompraServicio = @IdOrdenCompraServicio)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRASERVICIO", IdOrdenCompraServicio))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_Single_By_IdOrdenCompraServicio(ByVal IdOrdenCompraServicio As Integer) As clsBeTrans_oc_servicios

        Get_Single_By_IdOrdenCompraServicio = Nothing
        Try

            Const sp As String = "SELECT * FROM Trans_oc_servicios" &
            " Where(IdOrdenCompraServicio = @IdOrdenCompraServicio)"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraServicio", IdOrdenCompraServicio)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_oc_servicios As New clsBeTrans_oc_servicios

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTrans_oc_servicios, lDataTable.Rows(0))
                            Get_Single_By_IdOrdenCompraServicio = vBeTrans_oc_servicios
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar_By_OC_IdOrdencompraServicio(ByRef oBeTrans_oc_servicios As clsBeTrans_oc_servicios, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_oc_servicios")
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("observacion", "@observacion", DataType.Parametro)
            Upd.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Upd.Where("idordencompraservicio = @IdOrdenCompraServicio AND idordencompraenc = @idordencompraenc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_oc_servicios.IdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRASERVICIO", oBeTrans_oc_servicios.IdOrdenCompraServicio))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_oc_servicios.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_oc_servicios.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_oc_servicios.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACION", oBeTrans_oc_servicios.Observacion))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_oc_servicios.IdPropietarioBodega))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

End Class
