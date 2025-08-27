Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_inv_stock_prod

    Public Shared Sub Cargar(ByRef oBeTrans_inv_stock_prod As clsBeTrans_inv_stock_prod, ByRef dr As DataRow)

        Try

            With oBeTrans_inv_stock_prod

                .Idinventario = IIf(IsDBNull(dr.Item("idinventario")), 0, dr.Item("idinventario"))
                .Idinvstockprod = IIf(IsDBNull(dr.Item("Idinvstockprod")), 0, dr.Item("Idinvstockprod"))
                .IdProducto = IIf(IsDBNull(dr.Item("idProducto")), 0, dr.Item("idProducto"))
                .IdPresentacion = IIf(IsDBNull(dr.Item("idPresentacion")), 0, dr.Item("idPresentacion"))
                .Cant = IIf(IsDBNull(dr.Item("cant")), 0.0, dr.Item("cant"))
                .Peso = IIf(IsDBNull(dr.Item("peso")), 0.0, dr.Item("peso"))
                .IdUnidadMedida = IIf(IsDBNull(dr.Item("idUnidadMedida")), 0, dr.Item("idUnidadMedida"))
                .Lote = IIf(IsDBNull(dr.Item("lote")), "", dr.Item("lote"))
                .Fecha_vence = IIf(IsDBNull(dr.Item("fecha_vence")), Now, dr.Item("fecha_vence"))
                .Codigo = IIf(IsDBNull(dr.Item("codigo")), "", dr.Item("codigo"))
                .IdBodega = IIf(IsDBNull(dr.Item("idbodega")), 0, dr.Item("idbodega"))
                .IdUbicacion = IIf(IsDBNull(dr.Item("idubicacion")), 0, dr.Item("idubicacion"))
                .License_plate = IIf(IsDBNull(dr.Item("lic_plate")), "", dr.Item("lic_plate"))

                'AT20220504 Se carga IdPropietarioBodega
                If dr.Table.Columns.Contains("IdPropietarioBodega") Then
                    .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                End If

                .TipoTeoricoImportacion = IIf(IsDBNull(dr.Item("TipoTeoricoImportacion")), 0, dr.Item("TipoTeoricoImportacion"))
                .Codigo_Area = IIf(IsDBNull(dr.Item("Codigo_Area")), 0, dr.Item("Codigo_Area"))

            End With

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_inv_stock_prod As clsBeTrans_inv_stock_prod, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_inv_stock_prod")
            Ins.Add("idinventario", "@idinventario", DataType.Parametro)
            Ins.Add("idinvstockprod", "@idinvstockprod", DataType.Parametro)
            Ins.Add("idproducto", "@idproducto", DataType.Parametro)
            Ins.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Ins.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Ins.Add("cant", "@cant", DataType.Parametro)
            Ins.Add("peso", "@peso", DataType.Parametro)
            Ins.Add("lote", "@lote", DataType.Parametro)
            Ins.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Ins.Add("codigo", "@codigo", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Ins.Add("Lic_plate", "@License_plate", DataType.Parametro)
            Ins.Add("codigo_area", "@codigo_area", DataType.Parametro)
            Ins.Add("TipoTeoricoImportacion", "@TipoTeoricoImportacion", DataType.Parametro)

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

            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIO", oBeTrans_inv_stock_prod.Idinventario))
            cmd.Parameters.Add(New SqlParameter("@IDINVSTOCKPROD", oBeTrans_inv_stock_prod.Idinvstockprod))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeTrans_inv_stock_prod.IdProducto))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeTrans_inv_stock_prod.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@CANT", oBeTrans_inv_stock_prod.Cant))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeTrans_inv_stock_prod.Peso))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeTrans_inv_stock_prod.IdUnidadMedida))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeTrans_inv_stock_prod.Lote))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeTrans_inv_stock_prod.Fecha_vence))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeTrans_inv_stock_prod.Codigo))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_inv_stock_prod.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeTrans_inv_stock_prod.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@LICENSE_PLATE", oBeTrans_inv_stock_prod.License_plate))
            cmd.Parameters.Add(New SqlParameter("@TIPOTEORICOIMPORTACION", oBeTrans_inv_stock_prod.TipoTeoricoImportacion))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_AREA", oBeTrans_inv_stock_prod.Codigo_Area))

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

    Public Shared Function Actualizar(ByRef oBeTrans_inv_stock_prod As clsBeTrans_inv_stock_prod, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_inv_stock_prod")
            Upd.Add("idproducto", "@idproducto", DataType.Parametro)
            Upd.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Upd.Add("cant", "@cant", DataType.Parametro)
            Upd.Add("peso", "@peso", DataType.Parametro)
            Upd.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Upd.Add("lote", "@lote", DataType.Parametro)
            Upd.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Upd.Add("codigo", "@codigo", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Upd.Add("Lic_plate", "@License_plate", DataType.Parametro)
            Upd.Add("TipoTeoricoImportacion", "@TipoTeoricoImportacion", DataType.Parametro)
            Upd.Add("codigo_area", "@codigo_area", DataType.Parametro)
            Upd.Where("idinventario = @idinventario" &
                " AND idinvstockprod = @idinvstockprod")

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

            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIO", oBeTrans_inv_stock_prod.Idinventario))
            cmd.Parameters.Add(New SqlParameter("@IDINVSTOCKPROD", oBeTrans_inv_stock_prod.Idinvstockprod))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeTrans_inv_stock_prod.IdProducto))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeTrans_inv_stock_prod.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@CANT", oBeTrans_inv_stock_prod.Cant))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeTrans_inv_stock_prod.Peso))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeTrans_inv_stock_prod.IdUnidadMedida))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeTrans_inv_stock_prod.Lote))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeTrans_inv_stock_prod.Fecha_vence))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeTrans_inv_stock_prod.Codigo))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_inv_stock_prod.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeTrans_inv_stock_prod.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@LICENSE_PLATE", oBeTrans_inv_stock_prod.License_plate))
            cmd.Parameters.Add(New SqlParameter("@TIPOTEORICOIMPORTACION", oBeTrans_inv_stock_prod.TipoTeoricoImportacion))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_AREA", oBeTrans_inv_stock_prod.Codigo_Area))

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

    Public Shared Function Eliminar(ByRef oBeTrans_inv_stock_prod As clsBeTrans_inv_stock_prod, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_inv_stock_prod" &
             "  Where(idinventario = @idinventario)" &
             "  AND (idProducto = @idProducto)" &
             "  AND (idPresentacion = @idPresentacion)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIO", oBeTrans_inv_stock_prod.Idinventario))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeTrans_inv_stock_prod.IdProducto))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeTrans_inv_stock_prod.IdPresentacion))


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

            Const sp As String = " Delete from Trans_inv_stock_prod"
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

            Const sp As String = "SELECT * FROM Trans_inv_stock_prod"
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

    Public Shared Function Obtener(ByRef oBeTrans_inv_stock_prod As clsBeTrans_inv_stock_prod) As Boolean

        Try

            Const sp As String = "SELECT * FROM Trans_inv_stock_prod" &
            " Where(idinventario = @idinventario)" &
            " AND (idProducto = @idProducto)" &
            " AND (idPresentacion = @idPresentacion)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDINVENTARIO", oBeTrans_inv_stock_prod.Idinventario))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeTrans_inv_stock_prod.IdProducto))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeTrans_inv_stock_prod.IdPresentacion))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_inv_stock_prod, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeTrans_inv_stock_prod)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_stock_prod)
            Const sp As String = "SELECT * FROM Trans_inv_stock_prod "
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_inv_stock_prod As New clsBeTrans_inv_stock_prod

            For Each dr As DataRow In dt.Rows
                vBeTrans_inv_stock_prod = New clsBeTrans_inv_stock_prod
                Cargar(vBeTrans_inv_stock_prod, dr)
                lReturnList.Add(vBeTrans_inv_stock_prod)
            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeTrans_inv_stock_prod As clsBeTrans_inv_stock_prod)

        Try

            Const sp As String = "SELECT * FROM Trans_inv_stock_prod" &
            " Where(idinventario = @idinventario)" &
            " AND (idProducto = @idProducto)" &
            " AND (idPresentacion = @idPresentacion)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDINVENTARIO", pBeTrans_inv_stock_prod.Idinventario))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPRODUCTO", pBeTrans_inv_stock_prod.IdProducto))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPRESENTACION", pBeTrans_inv_stock_prod.IdPresentacion))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTrans_inv_stock_prod, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(idinventario),0) FROM Trans_inv_stock_prod"

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
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByVal IdInventario As Integer,
                                 ByRef lConnection As SqlConnection,
                                 ByRef lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(idinvstockprod),0) 
                                  FROM Trans_inv_stock_prod WHERE IdInventario =@IdInventario "

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                lCommand.Parameters.AddWithValue("@IdInventario", IdInventario)
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
