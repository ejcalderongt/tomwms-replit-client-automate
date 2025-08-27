Imports System.Data.SqlClient
Imports System.Reflection
Public Class clsLnTrans_manufactura_det

    Public Shared Sub Cargar(ByRef oBeTrans_manufactura_det As clsBeTrans_manufactura_det, ByRef dr As DataRow)
        Try
            With oBeTrans_manufactura_det
                .IdManufacturaDet = IIf(IsDBNull(dr.Item("IdManufacturaDet")), 0, dr.Item("IdManufacturaDet"))
                .IdManufacturaEnc = IIf(IsDBNull(dr.Item("IdManufacturaEnc")), 0, dr.Item("IdManufacturaEnc"))
                .IdPedidoDet = IIf(IsDBNull(dr.Item("IdPedidoDet")), 0, dr.Item("IdPedidoDet"))
                .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                .Codigo_producto = IIf(IsDBNull(dr.Item("codigo_producto")), "", dr.Item("codigo_producto"))
                .Nombre_producto = IIf(IsDBNull(dr.Item("nombre_producto")), "", dr.Item("nombre_producto"))
                .Cantidad_esperada = IIf(IsDBNull(dr.Item("cantidad_esperada")), 0.0, dr.Item("cantidad_esperada"))
                .Cantidad_recibida = IIf(IsDBNull(dr.Item("cantidad_recibida")), 0.0, dr.Item("cantidad_recibida"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_manufactura_det As clsBeTrans_manufactura_det, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_manufactura_det")
            Ins.Add("idmanufacturadet", "@idmanufacturadet", DataType.Parametro)
            Ins.Add("idmanufacturaenc", "@idmanufacturaenc", DataType.Parametro)
            Ins.Add("idpedidodet", "@idpedidodet", DataType.Parametro)
            Ins.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Ins.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Ins.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
            Ins.Add("nombre_producto", "@nombre_producto", DataType.Parametro)
            Ins.Add("cantidad_esperada", "@cantidad_esperada", DataType.Parametro)
            Ins.Add("cantidad_recibida", "@cantidad_recibida", DataType.Parametro)
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

            cmd.Parameters.Add(New SqlParameter("@IDMANUFACTURADET", oBeTrans_manufactura_det.IdManufacturaDet))
            cmd.Parameters.Add(New SqlParameter("@IDMANUFACTURAENC", oBeTrans_manufactura_det.IdManufacturaEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDODET", oBeTrans_manufactura_det.IdPedidoDet))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_manufactura_det.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_manufactura_det.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeTrans_manufactura_det.Codigo_producto))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_PRODUCTO", oBeTrans_manufactura_det.Nombre_producto))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_ESPERADA", oBeTrans_manufactura_det.Cantidad_esperada))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_RECIBIDA", oBeTrans_manufactura_det.Cantidad_recibida))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_manufactura_det.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_manufactura_det.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_manufactura_det.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_manufactura_det.Fec_mod))

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

    Public Shared Function Actualizar(ByRef oBeTrans_manufactura_det As clsBeTrans_manufactura_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_manufactura_det")
            Upd.Add("idmanufacturadet", "@idmanufacturadet", DataType.Parametro)
            Upd.Add("idmanufacturaenc", "@idmanufacturaenc", DataType.Parametro)
            Upd.Add("idpedidodet", "@idpedidodet", DataType.Parametro)
            Upd.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Upd.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Upd.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
            Upd.Add("nombre_producto", "@nombre_producto", DataType.Parametro)
            Upd.Add("cantidad_esperada", "@cantidad_esperada", DataType.Parametro)
            Upd.Add("cantidad_recibida", "@cantidad_recibida", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("IdManufacturaDet = @IdManufacturaDet")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMANUFACTURADET", oBeTrans_manufactura_det.IdManufacturaDet))
            cmd.Parameters.Add(New SqlParameter("@IDMANUFACTURAENC", oBeTrans_manufactura_det.IdManufacturaEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDODET", oBeTrans_manufactura_det.IdPedidoDet))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_manufactura_det.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_manufactura_det.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeTrans_manufactura_det.Codigo_producto))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_PRODUCTO", oBeTrans_manufactura_det.Nombre_producto))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_ESPERADA", oBeTrans_manufactura_det.Cantidad_esperada))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_RECIBIDA", oBeTrans_manufactura_det.Cantidad_recibida))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_manufactura_det.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_manufactura_det.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_manufactura_det.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_manufactura_det.Fec_mod))

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

    Public Shared Function Eliminar(ByRef oBeTrans_manufactura_det As clsBeTrans_manufactura_det, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_manufactura_det" & _
             "  Where(IdManufacturaDet = @IdManufacturaDet)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMANUFACTURADET", oBeTrans_manufactura_det.IdManufacturaDet))

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

            Const sp As String = "SELECT * FROM Trans_manufactura_det"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
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

    Public Shared Function Get_All() As List(Of clsBeTrans_manufactura_det)

        Dim lReturnList As New List(Of clsBeTrans_manufactura_det)

        Try

            Const sp As String = "SELECT * FROM Trans_manufactura_det"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_manufactura_det As New clsBeTrans_manufactura_det

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_manufactura_det = New clsBeTrans_manufactura_det()
                            Cargar(vBeTrans_manufactura_det, dr)
                            lReturnList.Add(vBeTrans_manufactura_det)
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

    Public Shared Sub GetSingle(ByRef pBeTrans_manufactura_det As clsBeTrans_manufactura_det)

        Try

            Const sp As String = "SELECT * FROM Trans_manufactura_det" & _
            " Where(IdManufacturaDet = @IdManufacturaDet)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdManufacturaDet", pBeTrans_manufactura_det.IdManufacturaDet)
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_manufactura_det As New clsBeTrans_manufactura_det

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTrans_manufactura_det, lDataTable.Rows(0))
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

            Const sp As String = "SELECT ISNULL(Max(IdManufacturaDet),0) FROM Trans_manufactura_det"

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

    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdManufacturaDet),0) FROM Trans_manufactura_det"

            Using lCommand As New SqlCommand(vSQL, pConnection)

                lCommand.CommandType = CommandType.Text
                lCommand.Transaction = pTransaction

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

    Friend Shared Sub Guardar_Detalle(pManufacturaEnc As Integer,
                                      pListManufacturaDet As List(Of clsBeTrans_manufactura_det),
                                      ByRef lConnection As SqlConnection,
                                      ByRef lTransaction As SqlTransaction)
        Try

            For Each ManufacturaDet In pListManufacturaDet
                If ManufacturaDet.IsNew Then
                    Dim pIdManufacturaDet = MaxID(lConnection, lTransaction) + 1
                    ManufacturaDet.IdManufacturaEnc = pManufacturaEnc
                    ManufacturaDet.IdManufacturaDet = pIdManufacturaDet
                    Insertar(ManufacturaDet, lConnection, lTransaction)
                Else
                    Actualizar(ManufacturaDet, lConnection, lTransaction)
                End If
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function Get_Detalle_By_IdManufacturaEnc(ByVal IdManufacturaEnc As Integer,
                                                           ByVal lConnection As SqlConnection,
                                                           ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_manufactura_det)

        Get_Detalle_By_IdManufacturaEnc = Nothing
        Dim lReturnList As New List(Of clsBeTrans_manufactura_det)

        Try

            Try

                Const sp As String = "SELECT * FROM Trans_manufactura_det where IdManufacturaEnc=@IdManufacturaEnc "

                Using lDTA As New SqlDataAdapter(sp, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdManufacturaEnc", IdManufacturaEnc)
                    lDTA.SelectCommand.Transaction = lTransaction
                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim vBeTrans_manufactura_det = New clsBeTrans_manufactura_det

                    If lDataTable IsNot Nothing Then

                        lReturnList = New List(Of clsBeTrans_manufactura_det)

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_manufactura_det = New clsBeTrans_manufactura_det()
                            Cargar(vBeTrans_manufactura_det, dr)
                            lReturnList.Add(vBeTrans_manufactura_det)
                        Next

                        Get_Detalle_By_IdManufacturaEnc = lReturnList

                    End If

                End Using

                Return Get_Detalle_By_IdManufacturaEnc

            Catch ex As Exception
                Throw ex
            End Try

        Catch ex As Exception

        End Try
    End Function

    Public Shared Function Actualizar_Cantidad_Recibida(ByRef oBeTrans_manufactura_det As clsBeTrans_manufactura_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_manufactura_det")
            Upd.Add("cantidad_recibida", "@cantidad_recibida", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("IdManufacturaDet = @IdManufacturaDet and IdManufacturaEnc=@IdManufacturaEnc and IdPedidoDet=@IdPedidoDet ")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IdManufacturaDet", oBeTrans_manufactura_det.IdManufacturaDet))
            cmd.Parameters.Add(New SqlParameter("@IdManufacturaEnc", oBeTrans_manufactura_det.IdManufacturaEnc))
            cmd.Parameters.Add(New SqlParameter("@IdPedidoDet", oBeTrans_manufactura_det.IdPedidoDet))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_RECIBIDA", oBeTrans_manufactura_det.Cantidad_recibida))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_manufactura_det.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_manufactura_det.Fec_mod))

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

End Class
