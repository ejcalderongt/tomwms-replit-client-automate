Imports System.Data.SqlClient

Public Class clsLnTrans_log_reubic_stock_res

    Public Shared Sub Cargar(ByRef oBeTrans_log_reubic_stock_res As clsBeTrans_log_reubic_stock_res, ByRef dr As DataRow)
        Try
            With oBeTrans_log_reubic_stock_res
                .IdLogReubicStockRes = IIf(IsDBNull(dr.Item("IdLogReubicStockRes")), 0, dr.Item("IdLogReubicStockRes"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .IdPickingUbic = IIf(IsDBNull(dr.Item("IdPickingUbic")), 0, dr.Item("IdPickingUbic"))
                .IdPickingDet = IIf(IsDBNull(dr.Item("IdPickingDet")), 0, dr.Item("IdPickingDet"))
                .IdPedidoEnc = IIf(IsDBNull(dr.Item("IdPedidoEnc")), 0, dr.Item("IdPedidoEnc"))
                .IdPedidoDet = IIf(IsDBNull(dr.Item("IdPedidoDet")), 0, dr.Item("IdPedidoDet"))
                .IdStock = IIf(IsDBNull(dr.Item("IdStock")), 0, dr.Item("IdStock"))
                .IdStockRes = IIf(IsDBNull(dr.Item("IdStockRes")), 0, dr.Item("IdStockRes"))
                .IdUbicacion = IIf(IsDBNull(dr.Item("IdUbicacion")), 0, dr.Item("IdUbicacion"))
                .IdUsuario = IIf(IsDBNull(dr.Item("IdUsuario")), 0, dr.Item("IdUsuario"))
                .Codigo_Producto = IIf(IsDBNull(dr.Item("Codigo_Producto")), "", dr.Item("Codigo_Producto"))
                .Lote = IIf(IsDBNull(dr.Item("Lote")), "", dr.Item("Lote"))
                .Lic_Plate = IIf(IsDBNull(dr.Item("Lic_Plate")), "", dr.Item("Lic_Plate"))
                .Fecha_Vence = IIf(IsDBNull(dr.Item("Fecha_Vence")), Date.Now, dr.Item("Fecha_Vence"))
                .Cantidad = IIf(IsDBNull(dr.Item("Cantidad")), 0.0, dr.Item("Cantidad"))
                .Peso = IIf(IsDBNull(dr.Item("Peso")), 0.0, dr.Item("Peso"))
                .Referencia = IIf(IsDBNull(dr.Item("Referencia")), "", dr.Item("Referencia"))
                .Observacion = IIf(IsDBNull(dr.Item("Observacion")), "", dr.Item("Observacion"))
                .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                .IdProductoEstado = IIf(IsDBNull(dr.Item("IdProductoEstado")), 0, dr.Item("IdProductoEstado"))
                .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                .IdUnidadMedida = IIf(IsDBNull(dr.Item("IdUnidadMedida")), 0, dr.Item("IdUnidadMedida"))
                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .Fecha_Sistema = IIf(IsDBNull(dr.Item("Fecha_Sistema")), Date.Now, dr.Item("Fecha_Sistema"))
                .IdUbicacionAnterior = IIf(IsDBNull(dr.Item("IdUbicacionAnterior")), 0, dr.Item("IdUbicacionAnterior"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_log_reubic_stock_res As clsBeTrans_log_reubic_stock_res, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_log_reubic_stock_res")
            Ins.Add("idlogreubicstockres", "@idlogreubicstockres", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("idpickingubic", "@idpickingubic", DataType.Parametro)
            Ins.Add("idpickingdet", "@idpickingdet", DataType.Parametro)
            Ins.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
            Ins.Add("idpedidodet", "@idpedidodet", DataType.Parametro)
            Ins.Add("idstock", "@idstock", DataType.Parametro)
            Ins.Add("idstockres", "@idstockres", DataType.Parametro)
            Ins.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Ins.Add("idusuario", "@idusuario", DataType.Parametro)
            Ins.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
            Ins.Add("lote", "@lote", DataType.Parametro)
            Ins.Add("lic_plate", "@lic_plate", DataType.Parametro)
            Ins.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Ins.Add("cantidad", "@cantidad", DataType.Parametro)
            Ins.Add("peso", "@peso", DataType.Parametro)
            Ins.Add("referencia", "@referencia", DataType.Parametro)
            Ins.Add("observacion", "@observacion", DataType.Parametro)
            Ins.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Ins.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Ins.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Ins.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Ins.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Ins.Add("fecha_sistema", "@fecha_sistema", DataType.Parametro)
            Ins.Add("idubicacionAnterior", "@idubicacionAnterior", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDLOGREUBICSTOCKRES", oBeTrans_log_reubic_stock_res.IdLogReubicStockRes))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_log_reubic_stock_res.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGUBIC", oBeTrans_log_reubic_stock_res.IdPickingUbic))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGDET", oBeTrans_log_reubic_stock_res.IdPickingDet))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTrans_log_reubic_stock_res.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDODET", oBeTrans_log_reubic_stock_res.IdPedidoDet))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeTrans_log_reubic_stock_res.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCKRES", oBeTrans_log_reubic_stock_res.IdStockRes))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeTrans_log_reubic_stock_res.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@IDUSUARIO", oBeTrans_log_reubic_stock_res.IdUsuario))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeTrans_log_reubic_stock_res.Codigo_Producto))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeTrans_log_reubic_stock_res.Lote))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeTrans_log_reubic_stock_res.Lic_Plate))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeTrans_log_reubic_stock_res.Fecha_Vence))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_log_reubic_stock_res.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeTrans_log_reubic_stock_res.Peso))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIA", oBeTrans_log_reubic_stock_res.Referencia))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACION", oBeTrans_log_reubic_stock_res.Observacion))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_log_reubic_stock_res.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeTrans_log_reubic_stock_res.IdProductoEstado))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_log_reubic_stock_res.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeTrans_log_reubic_stock_res.IdUnidadMedida))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeTrans_log_reubic_stock_res.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@FECHA_SISTEMA", oBeTrans_log_reubic_stock_res.Fecha_Sistema))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONANTERIOR", oBeTrans_log_reubic_stock_res.IdUbicacionAnterior))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeTrans_log_reubic_stock_res As clsBeTrans_log_reubic_stock_res, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_log_reubic_stock_res")
            Upd.Add("idlogreubicstockres", "@idlogreubicstockres", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("idpickingubic", "@idpickingubic", DataType.Parametro)
            Upd.Add("idpickingdet", "@idpickingdet", DataType.Parametro)
            Upd.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
            Upd.Add("idpedidodet", "@idpedidodet", DataType.Parametro)
            Upd.Add("idstock", "@idstock", DataType.Parametro)
            Upd.Add("idstockres", "@idstockres", DataType.Parametro)
            Upd.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Upd.Add("idusuario", "@idusuario", DataType.Parametro)
            Upd.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
            Upd.Add("lote", "@lote", DataType.Parametro)
            Upd.Add("lic_plate", "@lic_plate", DataType.Parametro)
            Upd.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Add("peso", "@peso", DataType.Parametro)
            Upd.Add("referencia", "@referencia", DataType.Parametro)
            Upd.Add("observacion", "@observacion", DataType.Parametro)
            Upd.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Upd.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Upd.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Upd.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Upd.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Upd.Add("fecha_sistema", "@fecha_sistema", DataType.Parametro)
            Upd.Add("idubicacionAnterior", "@idubicacionAnterior", DataType.Parametro)
            Upd.Where("IdLogReubicStockRes = @IdLogReubicStockRes")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDLOGREUBICSTOCKRES", oBeTrans_log_reubic_stock_res.IdLogReubicStockRes))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_log_reubic_stock_res.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGUBIC", oBeTrans_log_reubic_stock_res.IdPickingUbic))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGDET", oBeTrans_log_reubic_stock_res.IdPickingDet))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTrans_log_reubic_stock_res.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDODET", oBeTrans_log_reubic_stock_res.IdPedidoDet))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeTrans_log_reubic_stock_res.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCKRES", oBeTrans_log_reubic_stock_res.IdStockRes))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeTrans_log_reubic_stock_res.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@IDUSUARIO", oBeTrans_log_reubic_stock_res.IdUsuario))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeTrans_log_reubic_stock_res.Codigo_Producto))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeTrans_log_reubic_stock_res.Lote))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeTrans_log_reubic_stock_res.Lic_Plate))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeTrans_log_reubic_stock_res.Fecha_Vence))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_log_reubic_stock_res.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeTrans_log_reubic_stock_res.Peso))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIA", oBeTrans_log_reubic_stock_res.Referencia))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACION", oBeTrans_log_reubic_stock_res.Observacion))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_log_reubic_stock_res.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeTrans_log_reubic_stock_res.IdProductoEstado))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_log_reubic_stock_res.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeTrans_log_reubic_stock_res.IdUnidadMedida))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeTrans_log_reubic_stock_res.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@FECHA_SISTEMA", oBeTrans_log_reubic_stock_res.Fecha_Sistema))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONANTERIOR", oBeTrans_log_reubic_stock_res.IdUbicacionAnterior))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function


    Public Shared Function Eliminar(ByRef oBeTrans_log_reubic_stock_res As clsBeTrans_log_reubic_stock_res, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_log_reubic_stock_res" &
             "  Where(IdLogReubicStockRes = @IdLogReubicStockRes)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDLOGREUBICSTOCKRES", oBeTrans_log_reubic_stock_res.IdLogReubicStockRes))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_log_reubic_stock_res"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
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
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_All() As List(Of clsBeTrans_log_reubic_stock_res)

        Dim lReturnList As New List(Of clsBeTrans_log_reubic_stock_res)

        Try

            Const sp As String = "SELECT * FROM Trans_log_reubic_stock_res"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_log_reubic_stock_res As New clsBeTrans_log_reubic_stock_res

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_log_reubic_stock_res = New clsBeTrans_log_reubic_stock_res()
                            Cargar(vBeTrans_log_reubic_stock_res, dr)
                            lReturnList.Add(vBeTrans_log_reubic_stock_res)
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub GetSingle(ByRef pBeTrans_log_reubic_stock_res As clsBeTrans_log_reubic_stock_res)

        Try

            Const sp As String = "SELECT * FROM Trans_log_reubic_stock_res" &
            " Where(IdLogReubicStockRes = @IdLogReubicStockRes)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_log_reubic_stock_res As New clsBeTrans_log_reubic_stock_res

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTrans_log_reubic_stock_res, lDataTable.Rows(0))
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdLogReubicStockRes),0) FROM Trans_log_reubic_stock_res"

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
            Throw ex
        End Try

    End Function

End Class
