Imports System.Data.SqlClient

Partial Public Class clsLnTrans_pe_servicios

    Public Shared Function Exist(ByVal pIdServicio As Integer,
                                ByVal pIdOrdenPedidoEnc As Integer,
                                ByVal pConnection As SqlConnection,
                                ByVal pTransaction As SqlTransaction) As Boolean

        Exist = False

        Try

            Const sp As String = "Select IdOrdenPedidoEnc from trans_pe_servicios
                                  Where IdServicio = @pIdServicio AND IdOrdenPedidoEnc = @pIdOrdenPedidoEnc"

            Dim cmd As New SqlCommand(sp, pConnection) With {.CommandType = CommandType.Text, .Transaction = pTransaction}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@pIdServicio", pIdServicio))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@pIdOrdenPedidoEnc", pIdOrdenPedidoEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt.Rows.Count > 0

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdOrdenCompraEnc_DT(ByVal pIdOrdenPedidoEnc As Integer) As DataTable

        Dim lReturnList As New List(Of clsBeTarifa_tipo_transaccion_det)

        Get_All_By_IdOrdenCompraEnc_DT = Nothing

        Try

            Const sp As String = " Select trans_oc_servicios.IdServicio, i_nav_servicio.codigo_servicio As Codigo, i_nav_servicio.descripcion As Nombre, 
                                          i_nav_servicio.nemonico As Nemonico, Cantidad
                                   FROM trans_pe_servicios INNER JOIN
									     i_nav_servicio ON 
								    trans_oc_servicios.IdServicio = i_nav_servicio.IdServicio
									WHERE trans_oc_servicios.IdOrdenPedidoEnc = @pIdOrdenPedidoEnc "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        lDTA.SelectCommand.Parameters.AddWithValue("@pIdOrdenPedidoEnc", pIdOrdenPedidoEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)
                        Return lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar_By_PE_IdServicio(ByRef oBeTrans_pe_servicios As clsBeTrans_pe_servicios, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_pe_servicios")
            Upd.Add("idordenpedidoenc", "@idordenpedidoenc", DataType.Parametro)
            Upd.Add("idservicio", "@idservicio", DataType.Parametro)
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("observacion", "@observacion", DataType.Parametro)
            Upd.Where("idservicio = @idservicio AND idordenpedidoenc = @idordenpedidoenc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDORDENPEDIDOENC", oBeTrans_pe_servicios.IdOrdenPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDSERVICIO", oBeTrans_pe_servicios.IdServicio))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_pe_servicios.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_pe_servicios.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_pe_servicios.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_pe_servicios.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_pe_servicios.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACION", oBeTrans_pe_servicios.Observacion))

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


    Public Shared Function Actualizar_Servicio_By_IdServicio(ByRef oBeTrans_pe_servicios As clsBeTrans_pe_servicios, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_pe_servicios")
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)

            Upd.Where("IdOrdenPedidoServicio = @IdOrdenPedidoServicio AND idordenpedidoenc = @idordenpedidoenc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDORDENPEDIDOENC", oBeTrans_pe_servicios.IdOrdenPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDORDENPEDIDOSERVICIO", oBeTrans_pe_servicios.IdOrdenPedidoServicio))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_pe_servicios.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_pe_servicios.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_pe_servicios.Fec_mod))

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

    Public Shared Function Eliminar_By_IdOrdenPedidoServicio(ByRef oBeTrans_pe_servicios As clsBeTrans_pe_servicios, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_pe_servicios Where(IdOrdenPedidoServicio = @IdOrdenPedidoServicio)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDORDENPEDIDOSERVICIO", oBeTrans_pe_servicios.IdOrdenPedidoServicio))

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

    '#GT03062024: se usa para cargar los servicios en Pedido y en la prefactura.
    Public Shared Function Get_All_By_IdOrdenPedidoEnc(ByVal pIdOrdenPedidoEnc As Integer, Optional ByVal pIdAcuerdoEnc As Integer = 0) As List(Of clsBeTrans_pe_servicios)

        Get_All_By_IdOrdenPedidoEnc = Nothing

        Try

            'lReturnList = Nothing

            Dim sp As String = "SELECT * FROM Trans_pe_servicios 
										   where IdOrdenPedidoEnc=@pIdOrdenPedidoEnc "


            If pIdAcuerdoEnc > 0 Then
                sp += " and IdAcuerdo=@pIdAcuerdoEnc"
            End If

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@pIdOrdenPedidoEnc", pIdOrdenPedidoEnc)
                        If pIdAcuerdoEnc > 0 Then
                            lDTA.SelectCommand.Parameters.AddWithValue("@pIdAcuerdoEnc", pIdAcuerdoEnc)
                        End If
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_pe_servicios As New clsBeTrans_pe_servicios

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Get_All_By_IdOrdenPedidoEnc = New List(Of clsBeTrans_pe_servicios)

                            For Each dr As DataRow In lDataTable.Rows
                                vBeTrans_pe_servicios = New clsBeTrans_pe_servicios()
                                Cargar(vBeTrans_pe_servicios, dr)
                                Get_All_By_IdOrdenPedidoEnc.Add(vBeTrans_pe_servicios)
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


End Class
