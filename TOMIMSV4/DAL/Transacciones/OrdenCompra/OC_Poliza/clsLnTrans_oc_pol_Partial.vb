Imports System.Data.SqlClient

Partial Public Class clsLnTrans_oc_pol


    Public Shared Function GetSingle(ByVal pIdOrdenCompra As Integer) As clsBeTrans_oc_pol

        GetSingle = Nothing

        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM Trans_oc_pol WHERE IdOrdenCompraEnc=@IdOrdenCompraEnc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompra)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim Obj As New clsBeTrans_oc_pol()
                            Cargar(Obj, lRow)
                            Obj.IsNew = False
                            GetSingle = Obj

                        End If

                    End Using

                    lTransaction.Commit()

                End Using


                lConnection.Close()

            End Using

            Return GetSingle

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pIdOrdenCompra As Integer,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As clsBeTrans_oc_pol


        GetSingle = Nothing

        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM Trans_oc_pol WHERE IdOrdenCompraEnc=@IdOrdenCompraEnc and activo=1 "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompra)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim Obj As New clsBeTrans_oc_pol()

                    Cargar(Obj, lRow)

                    Obj.IsNew = False

                    Return Obj

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function MaxID(ByVal pIdOrdenCompraEnc As Integer) As Integer

        Try

            Dim lMax As Integer = 0

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(String.Format("SELECT ISNULL(Max(IdOrdenCompraPol),0) FROM trans_oc_pol WHERE IdOrdenCompraEnc={0}", pIdOrdenCompraEnc), lConnection)
                    lCommand.CommandType = CommandType.Text

                    lConnection.Open()
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CInt(lReturnValue) + 1
                    End If

                End Using

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByVal pIdOrdenCompraEnc As Integer,
                                 ByRef lConnection As SqlConnection,
                                 ByVal lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Using lCommand As New SqlCommand(String.Format("SELECT ISNULL(Max(IdOrdenCompraPol),0) FROM trans_oc_pol WHERE IdOrdenCompraEnc={0}", pIdOrdenCompraEnc), lConnection)

                lCommand.CommandType = CommandType.Text
                lCommand.Transaction = lTransaction

                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue) + 1
                End If

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle_By_Numero_Orden(ByVal pNumero_Orden As String) As clsBeTrans_oc_pol

        GetSingle_By_Numero_Orden = Nothing

        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM Trans_oc_pol WHERE Numero_Orden=@Numero_Orden"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        lDTA.SelectCommand.Parameters.AddWithValue("@Numero_Orden", pNumero_Orden)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim Obj As New clsBeTrans_oc_pol()
                            Cargar(Obj, lRow)
                            Obj.IsNew = False
                            GetSingle_By_Numero_Orden = Obj

                        End If

                    End Using

                    lTransaction.Commit()

                End Using


                lConnection.Close()

            End Using

            Return GetSingle_By_Numero_Orden

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Desactivar_Pol_By_Numero_Orden_and_OC(ByRef oBeTrans_oc_pol As clsBeTrans_oc_pol, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Desactivar_Pol_By_Numero_Orden_and_OC = False

        Try

            Upd.Init("trans_oc_pol")
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("numero_orden = @numero_orden " &
                "AND IdOrdenCompraEnc = @IdOrdenCompraEnc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_oc_pol.IdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@NUMERO_ORDEN", oBeTrans_oc_pol.numero_orden))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_oc_pol.activo))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return Desactivar_Pol_By_Numero_Orden_and_OC = rowsAffected

            'Return rowsAffected

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

    Public Shared Function Get_All_By_IdOrdenCompraEnc(ByVal IdOrdenCompraEnc As Integer) As DataTable

        Get_All_By_IdOrdenCompraEnc = Nothing

        Try
            Dim listPolizas As New clsBeTrans_oc_pol

            Dim vSQL As String = " select * from trans_oc_pol where IdOrdenCompraEnc=@IdOrdenCompraEnc "

            vSQL += " ORDER BY IdOrdenCompraPol "


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", IdOrdenCompraEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Get_All_By_IdOrdenCompraEnc = lDataTable

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

    Public Shared Function Get_All_By_IdPropietarioBodega_And_IdBodega(ByVal IdPropietarioBodega As Integer,
                                                                       ByVal IdBodega As Integer) As DataTable

        Get_All_By_IdPropietarioBodega_And_IdBodega = Nothing

        Try
            Dim listPolizas As New clsBeTrans_oc_pol

            'Dim vSQL As String = " select oc_pol.IdOrdenCompraEnc,oc_pol.numero_orden,oc_pol.codigo_poliza 
            '                              from trans_oc_pol oc_pol inner join trans_oc_enc oc_enc on 
            '                                   oc_pol.IdOrdenCompraEnc=oc_enc.IdOrdenCompraEnc	
            '                              where oc_pol.activo=1 and IdPropietarioBodega=@IdPropietarioBodega 
            '                                    and oc_enc.IdBodega = @IdBodega and (oc_pol.activo=1 and oc_enc.IdEstadoOC <> 5)"

            '#GT14112024: mejora para mostrar fecha de ingreso o de ticket si aplicara

            Dim vSQL As String = " select oc_pol.IdOrdenCompraEnc,oc_pol.numero_orden,oc_pol.codigo_poliza,
                                          case when oc_enc.no_ticket_tms>0 then tk.Fecha_Ingreso else oc_enc.Fecha_Creacion end as fecha_ingreso
                                          from trans_oc_pol oc_pol inner join trans_oc_enc oc_enc on 
                                               oc_pol.IdOrdenCompraEnc=oc_enc.IdOrdenCompraEnc left outer join
											   tms_ticket tk on oc_enc.no_ticket_tms = tk.IdTicket
                                          where oc_pol.activo=1 and IdPropietarioBodega=@IdPropietarioBodega 
                                                and oc_enc.IdBodega = @IdBodega and (oc_pol.activo=1 and oc_enc.IdEstadoOC <> 5) "


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", IdPropietarioBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Get_All_By_IdPropietarioBodega_And_IdBodega = lDataTable

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

    '#GT17062024: listar todas las oc con el mismo numero_orden
    Public Shared Function Get_All_By_Numero_Orden(ByVal pNumero_Orden As String) As List(Of clsBeTrans_oc_pol)

        Get_All_By_Numero_Orden = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM Trans_oc_pol WHERE Numero_Orden=@Numero_Orden and activo=1 "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        lDTA.SelectCommand.Parameters.AddWithValue("@Numero_Orden", pNumero_Orden)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Get_All_By_Numero_Orden = New List(Of clsBeTrans_oc_pol)

                            For i As Integer = 0 To lDT.Rows.Count - 1

                                Dim lRow As DataRow = lDT.Rows(i)
                                Dim Obj As New clsBeTrans_oc_pol()
                                Cargar(Obj, lRow)
                                Obj.IsNew = False
                                Get_All_By_Numero_Orden.Add(Obj)
                            Next

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

    '#GT11072024: metodo para cealsa, en prefactura
    Public Shared Function GetSingle_By_Numero_Orden_And_IdOrdenCompraEnc(ByVal pNumero_Orden As String, ByVal pIdOrdenCompraEnc As Integer) As clsBeTrans_oc_pol

        GetSingle_By_Numero_Orden_And_IdOrdenCompraEnc = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM Trans_oc_pol WHERE Numero_Orden=@Numero_Orden and IdOrdenCompraEnc=@pIdOrdenCompraEnc and activo=1"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        lDTA.SelectCommand.Parameters.AddWithValue("@Numero_Orden", pNumero_Orden)
                        lDTA.SelectCommand.Parameters.AddWithValue("@pIdOrdenCompraEnc", pIdOrdenCompraEnc)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim Obj As New clsBeTrans_oc_pol()
                            Cargar(Obj, lRow)
                            Obj.IsNew = False
                            GetSingle_By_Numero_Orden_And_IdOrdenCompraEnc = Obj

                        End If

                    End Using

                    lTransaction.Commit()

                End Using


                lConnection.Close()

            End Using

            'Return GetSingle_By_Numero_Orden()

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle_By_Numero_Orden_And_Activo(ByVal pNumero_Orden As String) As clsBeTrans_oc_pol

        GetSingle_By_Numero_Orden_And_Activo = Nothing

        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM Trans_oc_pol WHERE Numero_Orden=@Numero_Orden and activo=1 "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        lDTA.SelectCommand.Parameters.AddWithValue("@Numero_Orden", pNumero_Orden)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim Obj As New clsBeTrans_oc_pol()
                            Cargar(Obj, lRow)
                            Obj.IsNew = False
                            GetSingle_By_Numero_Orden_And_Activo = Obj

                        End If

                    End Using

                    lTransaction.Commit()

                End Using


                lConnection.Close()

            End Using

            'Return GetSingle_By_Numero_Orden_And_Activo

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Eliminar_Poliza_By_IdOrdenCompra(ByVal pIdOrdenCompraEnc As Integer,
                                                            Optional ByVal pConection As SqlConnection = Nothing,
                                                            Optional ByVal pTransaction As SqlTransaction = Nothing) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Eliminar_Poliza_By_IdOrdenCompra = False

        Try

            Dim sp As String = "DELETE FROM trans_oc_pol Where  IdOrdenCompraEnc = @IdOrdenCompraEnc"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IdOrdenCompraEnc", pIdOrdenCompraEnc))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Eliminar_Poliza_By_IdOrdenCompra = True

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

End Class
