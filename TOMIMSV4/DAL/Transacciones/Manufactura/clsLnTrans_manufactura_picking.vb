Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_manufactura_picking

    Public Shared Sub Cargar(ByRef oBeTrans_manufactura_picking As clsBeTrans_manufactura_picking, ByRef dr As DataRow)
        Try
            With oBeTrans_manufactura_picking
                .IdManufacturaPicking = IIf(IsDBNull(dr.Item("IdManufacturaPicking")), 0, dr.Item("IdManufacturaPicking"))
                .IdManufacturaDet = IIf(IsDBNull(dr.Item("IdManufacturaDet")), 0, dr.Item("IdManufacturaDet"))
                .IdManufacturaEnc = IIf(IsDBNull(dr.Item("IdManufacturaEnc")), 0, dr.Item("IdManufacturaEnc"))
                .IdPedidoDet = IIf(IsDBNull(dr.Item("IdPedidoDet")), 0, dr.Item("IdPedidoDet"))
                .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                .Codigo_barra = IIf(IsDBNull(dr.Item("codigo_barra")), "", dr.Item("codigo_barra"))
                .Licencia = IIf(IsDBNull(dr.Item("licencia")), "", dr.Item("licencia"))
                .Cantidad = IIf(IsDBNull(dr.Item("cantidad")), 0.0, dr.Item("cantidad"))
                .Licencia_manufactura = IIf(IsDBNull(dr.Item("licencia_manufactura")), "", dr.Item("licencia_manufactura"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_manufactura_picking As clsBeTrans_manufactura_picking, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_manufactura_picking")
            Ins.Add("idmanufacturapicking", "@idmanufacturapicking", DataType.Parametro)
            Ins.Add("idmanufacturadet", "@idmanufacturadet", DataType.Parametro)
            Ins.Add("idmanufacturaenc", "@idmanufacturaenc", DataType.Parametro)
            Ins.Add("idpedidodet", "@idpedidodet", DataType.Parametro)
            Ins.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Ins.Add("codigo_barra", "@codigo_barra", DataType.Parametro)
            Ins.Add("licencia", "@licencia", DataType.Parametro)
            Ins.Add("cantidad", "@cantidad", DataType.Parametro)
            Ins.Add("licencia_manufactura", "@licencia_manufactura", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMANUFACTURAPICKING", oBeTrans_manufactura_picking.IdManufacturaPicking))
            cmd.Parameters.Add(New SqlParameter("@IDMANUFACTURADET", oBeTrans_manufactura_picking.IdManufacturaDet))
            cmd.Parameters.Add(New SqlParameter("@IDMANUFACTURAENC", oBeTrans_manufactura_picking.IdManufacturaEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDODET", oBeTrans_manufactura_picking.IdPedidoDet))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_manufactura_picking.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeTrans_manufactura_picking.Codigo_barra))
            cmd.Parameters.Add(New SqlParameter("@LICENCIA", oBeTrans_manufactura_picking.Licencia))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_manufactura_picking.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@LICENCIA_MANUFACTURA", oBeTrans_manufactura_picking.Licencia_manufactura))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_manufactura_picking.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_manufactura_picking.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_manufactura_picking.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_manufactura_picking.Fec_mod))

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

    Public Shared Function Actualizar(ByRef oBeTrans_manufactura_picking As clsBeTrans_manufactura_picking, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_manufactura_picking")
            Upd.Add("idmanufacturapicking", "@idmanufacturapicking", DataType.Parametro)
            Upd.Add("idmanufacturadet", "@idmanufacturadet", DataType.Parametro)
            Upd.Add("idmanufacturaenc", "@idmanufacturaenc", DataType.Parametro)
            Upd.Add("idpedidodet", "@idpedidodet", DataType.Parametro)
            Upd.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Upd.Add("codigo_barra", "@codigo_barra", DataType.Parametro)
            Upd.Add("licencia", "@licencia", DataType.Parametro)
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Add("licencia_manufactura", "@licencia_manufactura", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("IdManufacturaPicking = @IdManufacturaPicking")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMANUFACTURAPICKING", oBeTrans_manufactura_picking.IdManufacturaPicking))
            cmd.Parameters.Add(New SqlParameter("@IDMANUFACTURADET", oBeTrans_manufactura_picking.IdManufacturaDet))
            cmd.Parameters.Add(New SqlParameter("@IDMANUFACTURAENC", oBeTrans_manufactura_picking.IdManufacturaEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDODET", oBeTrans_manufactura_picking.IdPedidoDet))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_manufactura_picking.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeTrans_manufactura_picking.Codigo_barra))
            cmd.Parameters.Add(New SqlParameter("@LICENCIA", oBeTrans_manufactura_picking.Licencia))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_manufactura_picking.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@LICENCIA_MANUFACTURA", oBeTrans_manufactura_picking.Licencia_manufactura))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_manufactura_picking.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_manufactura_picking.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_manufactura_picking.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_manufactura_picking.Fec_mod))

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


    Public Shared Function Eliminar(ByRef oBeTrans_manufactura_picking As clsBeTrans_manufactura_picking, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_manufactura_picking" &
             "  Where(IdManufacturaPicking = @IdManufacturaPicking)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMANUFACTURAPICKING", oBeTrans_manufactura_picking.IdManufacturaPicking))

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

    Public Shared Function Listar() As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_manufactura_picking "
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function


    Public Shared Function Listar(ByVal IdManufacturaEnc As Integer, ByVal IdManufacturaDet As Integer) As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_manufactura_picking 
							      where IdManufacturaEnc=@IdManufacturaEnc and IdManufacturaDet=@IdManufacturaDet"

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdManufacturaEnc", IdManufacturaEnc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdManufacturaDet", IdManufacturaDet))

            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_All() As List(Of clsBeTrans_manufactura_picking)

        Dim lReturnList As New List(Of clsBeTrans_manufactura_picking)

        Try

            Const sp As String = "SELECT * FROM Trans_manufactura_picking"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_manufactura_picking As New clsBeTrans_manufactura_picking

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_manufactura_picking = New clsBeTrans_manufactura_picking()
                            Cargar(vBeTrans_manufactura_picking, dr)
                            lReturnList.Add(vBeTrans_manufactura_picking)
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

    Public Shared Sub GetSingle(ByRef pBeTrans_manufactura_picking As clsBeTrans_manufactura_picking)

        Try

            Const sp As String = "SELECT * FROM Trans_manufactura_picking" & _
            " Where(IdManufacturaPicking = @IdManufacturaPicking)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdManufacturaPicking", pBeTrans_manufactura_picking.IdManufacturaPicking))
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_manufactura_picking As New clsBeTrans_manufactura_picking

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTrans_manufactura_picking, lDataTable.Rows(0))
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

            Const sp As String = "SELECT ISNULL(Max(IdManufacturaPicking),0) FROM Trans_manufactura_picking"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

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

    Public Shared Function MaxID(ByVal pConection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdManufacturaPicking),0) FROM Trans_manufactura_picking"


            Using lCommand As New SqlCommand(sp, pConection, pTransaction) With {.CommandType = CommandType.Text}

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

    Public Shared Function Guardar(ByVal pTransManufacturaEnc As clsBeTrans_manufactura_enc, ByVal pTransManufacturaPicking As clsBeTrans_manufactura_picking,
                                   ByVal pTransManufacturaDet As clsBeTrans_manufactura_det,
                                   ByVal pCantidad As Integer) As Boolean

        Guardar = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim pIdManufacturaPicking As Integer

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            pIdManufacturaPicking = MaxID(lConnection, lTransaction) + 1

            'For i As Integer = 1 To pCantidad
            pTransManufacturaPicking.IdManufacturaPicking = pIdManufacturaPicking
            Insertar(pTransManufacturaPicking, lConnection, lTransaction)
            pIdManufacturaPicking += 1
            'Next

            '#GT29022024: rebajar la cantidad leida en trans_manufactura_det
            clsLnTrans_manufactura_det.Actualizar_Cantidad_Recibida(pTransManufacturaDet, lConnection, lTransaction)

            '#GT26032024: actualizamos el encabezado de la manufactura si es nuevo
            If pTransManufacturaEnc.Estado = "Nuevo" Then
                clsLnTrans_manufactura_enc.Actualizar_Encabezado(pTransManufacturaDet.IdManufacturaEnc, "Proceso", lConnection, lTransaction)
            End If

            Guardar = True

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function GetSingle_By_IdManufacturaEnc_And_IdProductoBodega(ByVal pBeTrans_manufactura_picking As clsBeTrans_manufactura_picking) As Integer

        GetSingle_By_IdManufacturaEnc_And_IdProductoBodega = 0

        Try

            Dim sp As String = "SELECT count(IdManufacturaDet) 
                                FROM trans_manufactura_picking 
                                WHERE IdManufacturaEnc=@IdManufacturaEnc and 
							          IdProductoBodega=@IdProductoBodega "

            'If pBeTrans_manufactura_picking.Licencia <> "" Then
            '	sp += " and licencia = @licencia "
            'End If

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)


                    Using cmd = New SqlCommand(sp, lConnection)

                        cmd.CommandType = CommandType.Text
                        cmd.Transaction = lTransaction
                        cmd.Parameters.Add(New SqlParameter("@IdManufacturaEnc", pBeTrans_manufactura_picking.IdManufacturaEnc))
                        cmd.Parameters.Add(New SqlParameter("@IdProductoBodega", pBeTrans_manufactura_picking.IdProductoBodega))
                        'If pBeTrans_manufactura_picking.Licencia <> "" Then
                        '	cmd.Parameters.Add(New SqlParameter("@licencia", pBeTrans_manufactura_picking.Licencia))
                        'End If

                        GetSingle_By_IdManufacturaEnc_And_IdProductoBodega = Convert.ToInt16(cmd.ExecuteScalar())

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Tiempos_By_IdManufacturaEnc_and_IdManufacturaDet(ByVal IdManufacturaEnc As Integer,
                                                                                ByVal IdManufacturaDet As Integer) As DataTable

        Get_Tiempos_By_IdManufacturaEnc_and_IdManufacturaDet = Nothing

        Try

            Dim sp As String = "SELECT Convert(Time(0),min(fec_agr))hora_ini, Convert(Time(0),max(fec_agr))hora_fin, 
									   DATEDIFF(MINUTE,min(fec_agr),max(fec_agr)) duracion_minutos
								       FROM trans_manufactura_picking
									   WHERE IdManufacturaEnc=@IdManufacturaEnc and IdManufacturaDet=@IdManufacturaDet"
            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(sp, lConnection)
                        Dim lTable As New DataTable("Result")
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdManufacturaEnc", IdManufacturaEnc)
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdManufacturaDet", IdManufacturaDet)
                        lDataAdapter.Fill(lTable)

                        If lTable IsNot Nothing Then
                            Get_Tiempos_By_IdManufacturaEnc_and_IdManufacturaDet = lTable
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

    Public Shared Function Get_All_By_IdPedidoDet_IdManufactura(ByVal pIdManufacturaEnc As Integer,
                                                                ByVal pIdPedidoDet As Integer) As List(Of clsBeTrans_manufactura_picking)

        Get_All_By_IdPedidoDet_IdManufactura = Nothing

        Dim lReturnList As New List(Of clsBeTrans_manufactura_picking)

        Try

            Dim sp As String = "SELECT *
                                FROM trans_manufactura_picking 
                                WHERE IdManufacturaEnc=@IdManufacturaEnc and 
							          IdPedidoDet=@IdPedidoDet "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdManufacturaEnc", pIdManufacturaEnc)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoDet", pIdPedidoDet)
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_manufactura_picking As New clsBeTrans_manufactura_picking

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_manufactura_picking = New clsBeTrans_manufactura_picking()
                            Cargar(vBeTrans_manufactura_picking, dr)
                            lReturnList.Add(vBeTrans_manufactura_picking)
                        Next

                        Return lReturnList

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
