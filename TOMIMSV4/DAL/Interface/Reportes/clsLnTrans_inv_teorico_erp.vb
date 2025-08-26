Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_inv_teorico_erp

    Public Shared Sub Cargar(ByRef oBeTrans_inv_teorico_erp As clsBeTrans_inv_teorico_erp, ByRef dr As DataRow)
        Try
            With oBeTrans_inv_teorico_erp
                .Idinvteoricoerp = IIf(IsDBNull(dr.Item("idinvteoricoerp")), 0, dr.Item("idinvteoricoerp"))
                .IdProducto = IIf(IsDBNull(dr.Item("idProducto")), 0, dr.Item("idProducto"))
                .IdPresentacion = IIf(IsDBNull(dr.Item("idPresentacion")), 0, dr.Item("idPresentacion"))
                .Cant = IIf(IsDBNull(dr.Item("cant")), 0.0, dr.Item("cant"))
                .Peso = IIf(IsDBNull(dr.Item("peso")), 0.0, dr.Item("peso"))
                .IdUnidadMedida = IIf(IsDBNull(dr.Item("idUnidadMedida")), 0, dr.Item("idUnidadMedida"))
                .Lote = IIf(IsDBNull(dr.Item("lote")), "", dr.Item("lote"))
                .Fecha_vence = IIf(IsDBNull(dr.Item("fecha_vence")), Date.Now, dr.Item("fecha_vence"))
                .Codigo = IIf(IsDBNull(dr.Item("codigo")), "", dr.Item("codigo"))
                .Idbodega = IIf(IsDBNull(dr.Item("idbodega")), 0, dr.Item("idbodega"))
                .Idubicacion = IIf(IsDBNull(dr.Item("idubicacion")), 0, dr.Item("idubicacion"))
                .Lic_plate = IIf(IsDBNull(dr.Item("lic_plate")), "", dr.Item("lic_plate"))
                .Codigo_area = IIf(IsDBNull(dr.Item("codigo_area")), "", dr.Item("codigo_area"))
                .Fecha_agr = IIf(IsDBNull(dr.Item("fecha_agr")), Date.Now, dr.Item("fecha_agr"))
                .Usuario_agr = IIf(IsDBNull(dr.Item("usuario_agr")), "", dr.Item("usuario_agr"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_inv_teorico_erp As clsBeTrans_inv_teorico_erp, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_inv_teorico_erp")
            Ins.Add("idinvteoricoerp", "@idinvteoricoerp", WMSTipoDato.Tipo.Parametro)
            Ins.Add("idproducto", "@idproducto", WMSTipoDato.Tipo.Parametro)
            Ins.Add("idpresentacion", "@idpresentacion", WMSTipoDato.Tipo.Parametro)
            Ins.Add("cant", "@cant", WMSTipoDato.Tipo.Parametro)
            Ins.Add("peso", "@peso", WMSTipoDato.Tipo.Parametro)
            Ins.Add("idunidadmedida", "@idunidadmedida", WMSTipoDato.Tipo.Parametro)
            Ins.Add("lote", "@lote", WMSTipoDato.Tipo.Parametro)
            Ins.Add("fecha_vence", "@fecha_vence", WMSTipoDato.Tipo.Parametro)
            Ins.Add("codigo", "@codigo", WMSTipoDato.Tipo.Parametro)
            Ins.Add("idbodega", "@idbodega", WMSTipoDato.Tipo.Parametro)
            Ins.Add("idubicacion", "@idubicacion", WMSTipoDato.Tipo.Parametro)
            Ins.Add("lic_plate", "@lic_plate", WMSTipoDato.Tipo.Parametro)
            Ins.Add("codigo_area", "@codigo_area", WMSTipoDato.Tipo.Parametro)
            Ins.Add("fecha_agr", "@fecha_agr", WMSTipoDato.Tipo.Parametro)
            Ins.Add("usuario_agr", "@usuario_agr", WMSTipoDato.Tipo.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDINVTEORICOERP", oBeTrans_inv_teorico_erp.Idinvteoricoerp))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeTrans_inv_teorico_erp.IdProducto))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeTrans_inv_teorico_erp.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@CANT", oBeTrans_inv_teorico_erp.Cant))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeTrans_inv_teorico_erp.Peso))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeTrans_inv_teorico_erp.IdUnidadMedida))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeTrans_inv_teorico_erp.Lote))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeTrans_inv_teorico_erp.Fecha_vence))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeTrans_inv_teorico_erp.Codigo))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_inv_teorico_erp.Idbodega))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeTrans_inv_teorico_erp.Idubicacion))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeTrans_inv_teorico_erp.Lic_plate))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_AREA", oBeTrans_inv_teorico_erp.Codigo_area))
            cmd.Parameters.Add(New SqlParameter("@FECHA_AGR", oBeTrans_inv_teorico_erp.Fecha_agr))
            cmd.Parameters.Add(New SqlParameter("@USUARIO_AGR", oBeTrans_inv_teorico_erp.Usuario_agr))

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

    Public Shared Function Actualizar(ByRef oBeTrans_inv_teorico_erp As clsBeTrans_inv_teorico_erp, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_inv_teorico_erp")
            Upd.Add("idinvteoricoerp", "@idinvteoricoerp", WMSTipoDato.Tipo.Parametro)
            Upd.Add("idproducto", "@idproducto", WMSTipoDato.Tipo.Parametro)
            Upd.Add("idpresentacion", "@idpresentacion", WMSTipoDato.Tipo.Parametro)
            Upd.Add("cant", "@cant", WMSTipoDato.Tipo.Parametro)
            Upd.Add("peso", "@peso", WMSTipoDato.Tipo.Parametro)
            Upd.Add("idunidadmedida", "@idunidadmedida", WMSTipoDato.Tipo.Parametro)
            Upd.Add("lote", "@lote", WMSTipoDato.Tipo.Parametro)
            Upd.Add("fecha_vence", "@fecha_vence", WMSTipoDato.Tipo.Parametro)
            Upd.Add("codigo", "@codigo", WMSTipoDato.Tipo.Parametro)
            Upd.Add("idbodega", "@idbodega", WMSTipoDato.Tipo.Parametro)
            Upd.Add("idubicacion", "@idubicacion", WMSTipoDato.Tipo.Parametro)
            Upd.Add("lic_plate", "@lic_plate", WMSTipoDato.Tipo.Parametro)
            Upd.Add("codigo_area", "@codigo_area", WMSTipoDato.Tipo.Parametro)
            Upd.Add("fecha_agr", "@fecha_agr", WMSTipoDato.Tipo.Parametro)
            Upd.Add("usuario_agr", "@usuario_agr", WMSTipoDato.Tipo.Parametro)
            Upd.Where("idinvteoricoerp = @idinvteoricoerp")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDINVTEORICOERP", oBeTrans_inv_teorico_erp.Idinvteoricoerp))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeTrans_inv_teorico_erp.IdProducto))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeTrans_inv_teorico_erp.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@CANT", oBeTrans_inv_teorico_erp.Cant))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeTrans_inv_teorico_erp.Peso))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeTrans_inv_teorico_erp.IdUnidadMedida))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeTrans_inv_teorico_erp.Lote))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeTrans_inv_teorico_erp.Fecha_vence))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeTrans_inv_teorico_erp.Codigo))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_inv_teorico_erp.Idbodega))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeTrans_inv_teorico_erp.Idubicacion))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeTrans_inv_teorico_erp.Lic_plate))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_AREA", oBeTrans_inv_teorico_erp.Codigo_area))
            cmd.Parameters.Add(New SqlParameter("@FECHA_AGR", oBeTrans_inv_teorico_erp.Fecha_agr))
            cmd.Parameters.Add(New SqlParameter("@USUARIO_AGR", oBeTrans_inv_teorico_erp.Usuario_agr))

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


    Public Shared Function Eliminar(ByRef oBeTrans_inv_teorico_erp As clsBeTrans_inv_teorico_erp, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_inv_teorico_erp" & _
             "  Where(idinvteoricoerp = @idinvteoricoerp)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDINVTEORICOERP", oBeTrans_inv_teorico_erp.Idinvteoricoerp))

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

            Const sp As String = "SELECT * FROM Trans_inv_teorico_erp"
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

    Public Shared Function Get_All() As List(Of clsBeTrans_inv_teorico_erp)

        Dim lReturnList As New List(Of clsBeTrans_inv_teorico_erp)

        Try

            Const sp As String = "SELECT * FROM Trans_inv_teorico_erp"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_inv_teorico_erp As New clsBeTrans_inv_teorico_erp

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_inv_teorico_erp = New clsBeTrans_inv_teorico_erp()
                            Cargar(vBeTrans_inv_teorico_erp, dr)
                            lReturnList.Add(vBeTrans_inv_teorico_erp)
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

    Public Shared Sub GetSingle(ByRef pBeTrans_inv_teorico_erp As clsBeTrans_inv_teorico_erp)

        Try

            Const sp As String = "SELECT * FROM Trans_inv_teorico_erp" & _
            " Where(idinvteoricoerp = @idinvteoricoerp)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_inv_teorico_erp As New clsBeTrans_inv_teorico_erp

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTrans_inv_teorico_erp, lDataTable.Rows(0))
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

            Const sp As String = "SELECT ISNULL(Max(idinvteoricoerp),0) FROM Trans_inv_teorico_erp"

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

    Public Shared Function Get_Inventario_Vrs_Stock_Det_ERP(ByVal pIdBodega As Integer) As DataTable

        Get_Inventario_Vrs_Stock_Det_ERP = Nothing

        Dim vIdUbicacionRecepcion As Integer = 0

        Try

            Dim vSQL As String = "SELECT t.ubicacion Codigo_Area_SAP, 
                                        t.Codigo, t.Producto as Nombre, SUM(t.Inventario) AS StockWMS, 
										SUM(t.Stock) AS StockERP, 
										SUM(t.Inventario) - SUM(t.Stock) AS Dif, t.lote AS Lote, t.fecha_vence AS Fecha_Vence,
										t.UMBas,t.Presentacion, t.factor,IIF(t.factor>0,t.factor*SUM(t.Inventario),sum(t.Inventario)) as Inv_UM,
										IIF(t.factor>0,t.factor*SUM(t.Stock),sum(t.Stock)) as Stock_UM, t.Licencia
								FROM (
									SELECT producto.codigo,producto.IdProducto,  
										producto.nombre AS Producto,
										ISNULL(producto_presentacion.nombre,'') AS Presentacion,producto_presentacion.IdPresentacion, 
										SUM(cantidad) As Inventario,0 AS Stock,SUM(stock.peso) AS Peso, stock.Lote, 
										CONVERT(date,stock.Fecha_Vence) AS Fecha_Vence, 
										dbo.Get_Codigo_Area_By_IdUbicacion(stock.idubicacion, stock.idbodega) As ubicacion, 
										unidad_medida.Nombre UMBas, producto_presentacion.factor, '' AS Licencia
									FROM stock INNER JOIN
										producto_bodega ON producto_bodega.IdProductoBodega = stock.IdProductoBodega INNER JOIN
										producto ON producto.idproducto = producto_bodega.IdProducto INNER JOIN
										unidad_medida ON producto.IdUnidadMedidaBasica = producto.IdUnidadMedidaBasica AND 
										unidad_medida.IdUnidadMedida = stock.idunidadmedida LEFT OUTER JOIN 
										producto_presentacion ON stock.IdPresentacion = producto_presentacion.IdPresentacion 
									WHERE stock.IdpropietarioBodega <> 0 
									GROUP BY producto.codigo,  
										producto.nombre,producto_presentacion.nombre,
										producto_presentacion.IdPresentacion,producto.IdProducto, stock.Lote, stock.Fecha_Vence,
										dbo.Get_Codigo_Area_By_IdUbicacion(stock.idubicacion, stock.idbodega), unidad_medida.Nombre, 
                                        producto_presentacion.factor
								UNION ALL                     
									SELECT producto.codigo,producto.IdProducto,   
										producto.nombre AS Producto,
										ISNULL(producto_presentacion.nombre,'') AS Presentacion,producto_presentacion.IdPresentacion ,
										0 AS Detalle,SUM(cant) AS Stock,0 AS Peso, trans_inv_teorico_erp.Lote, trans_inv_teorico_erp.Fecha_Vence,
										trans_inv_teorico_erp.codigo_area  as ubicacion,
										unidad_medida.Nombre UMBas, producto_presentacion.factor, '' AS Licencia
									FROM trans_inv_teorico_erp INNER JOIN 
										producto ON trans_inv_teorico_erp.idproducto = producto.IdProducto INNER JOIN
										unidad_medida ON producto.IdUnidadMedidaBasica = producto.IdUnidadMedidaBasica AND 
										unidad_medida.IdUnidadMedida = trans_inv_teorico_erp.idunidadmedida LEFT OUTER JOIN 
										producto_presentacion ON trans_inv_teorico_erp.IdPresentacion = producto_presentacion.IdPresentacion
                                    WHERE trans_inv_teorico_erp.cant >0
									GROUP BY producto.codigo,  
										producto.nombre,producto_presentacion.nombre,producto_presentacion.IdPresentacion,producto.IdProducto,
										trans_inv_teorico_erp.Lote, trans_inv_teorico_erp.Fecha_Vence, trans_inv_teorico_erp.codigo_area,
										trans_inv_teorico_erp.idbodega,unidad_medida.Nombre, producto_presentacion.factor) AS T 
								GROUP BY t.lote, t.codigo, t.Producto, t.fecha_vence, t.UMBas, t.Presentacion,t.factor, t.Licencia, t.Ubicacion
								ORDER BY T.ubicacion, T.codigo "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Transaction = lTransaction

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Get_Inventario_Vrs_Stock_Det_ERP = lDataTable
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

    Public Shared Function Eliminar_Todos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_inv_teorico_erp "

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

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

    Public Shared Function MaxID(lConnection As SqlConnection, lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(idinvteoricoerp),0) FROM Trans_inv_teorico_erp "

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

    Public Shared Function Get_Inventario_Vrs_Stock_Det_ERP_Sin_Lote(ByVal pIdBodega As Integer) As DataTable

        Get_Inventario_Vrs_Stock_Det_ERP_Sin_Lote = Nothing

        Dim vIdUbicacionRecepcion As Integer = 0

        Try

            Dim vSQL As String = "SELECT t.Codigo, t.Producto as Nombre, SUM(t.Inventario) AS StockWMS, 
										SUM(t.Stock) AS StockERP, 
										SUM(t.Inventario) - SUM(t.Stock) AS Dif,
										t.UMBas,t.Presentacion, t.factor,IIF(t.factor>0,t.factor*SUM(t.Inventario),
                                        sum(t.Inventario)) as Inv_UM,
										IIF(t.factor>0,t.factor*SUM(t.Stock),sum(t.Stock)) as Stock_UM
								FROM (
									SELECT producto.codigo,producto.IdProducto,  
										producto.nombre AS Producto,
										ISNULL(producto_presentacion.nombre,'') AS Presentacion,producto_presentacion.IdPresentacion, 
										SUM(cantidad) As Inventario,0 AS Stock,SUM(stock.peso) AS Peso,
										unidad_medida.Nombre UMBas, producto_presentacion.factor
									FROM stock INNER JOIN
										producto_bodega ON producto_bodega.IdProductoBodega = stock.IdProductoBodega INNER JOIN
										producto ON producto.idproducto = producto_bodega.IdProducto INNER JOIN
										unidad_medida ON producto.IdUnidadMedidaBasica = producto.IdUnidadMedidaBasica AND 
										unidad_medida.IdUnidadMedida = stock.idunidadmedida LEFT OUTER JOIN 
										producto_presentacion ON stock.IdPresentacion = producto_presentacion.IdPresentacion 
									WHERE stock.IdpropietarioBodega <> 0 AND stock.IdBodega = @IdBodega
									GROUP BY producto.codigo,  
										producto.nombre,producto_presentacion.nombre,
										producto_presentacion.IdPresentacion,producto.IdProducto,
										unidad_medida.Nombre, producto_presentacion.factor
								UNION ALL                     
									SELECT producto.codigo,producto.IdProducto,   
										producto.nombre AS Producto,
										ISNULL(producto_presentacion.nombre,'') AS Presentacion,producto_presentacion.IdPresentacion ,
										0 AS Detalle,SUM(cant) AS Stock,0 AS Peso,unidad_medida.Nombre UMBas, producto_presentacion.factor
									FROM trans_inv_teorico_erp INNER JOIN 
										producto ON trans_inv_teorico_erp.idproducto = producto.IdProducto INNER JOIN
										unidad_medida ON producto.IdUnidadMedidaBasica = producto.IdUnidadMedidaBasica AND 
										unidad_medida.IdUnidadMedida = trans_inv_teorico_erp.idunidadmedida LEFT OUTER JOIN 
										producto_presentacion ON trans_inv_teorico_erp.IdPresentacion = producto_presentacion.IdPresentacion
                                    WHERE trans_inv_teorico_erp.cant >0
									GROUP BY producto.codigo,  
										producto.nombre,producto_presentacion.nombre,producto_presentacion.IdPresentacion,producto.IdProducto,
										trans_inv_teorico_erp.idbodega,unidad_medida.Nombre, producto_presentacion.factor) AS T 
								GROUP BY t.codigo, t.Producto, t.UMBas, t.Presentacion,t.factor
								ORDER BY T.codigo "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDataAdapter.SelectCommand.Transaction = lTransaction

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Get_Inventario_Vrs_Stock_Det_ERP_Sin_Lote = lDataTable
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

End Class
