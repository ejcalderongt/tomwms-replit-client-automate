Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnTrans_ajuste_det

    Public Shared Function GetAll() As List(Of clsBeTrans_ajuste_det)
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim lReturnList As New List(Of clsBeTrans_ajuste_det)
            Const sp As String = "SELECT * FROM Trans_ajuste_det"

            If lConnection.State = ConnectionState.Closed Then lConnection.Open()

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            lTransaction = lConnection.BeginTransaction()
            cmd.Transaction = lTransaction
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_ajuste_det As New clsBeTrans_ajuste_det

            For Each dr As DataRow In dt.Rows
                vBeTrans_ajuste_det = New clsBeTrans_ajuste_det
                Cargar(vBeTrans_ajuste_det, dr)
                vBeTrans_ajuste_det.UmBas = clsLnUnidad_medida.Get_Codigo_By_IdUnidadMedida(vBeTrans_ajuste_det.IdUnidadMedida,
                                                                                            lConnection,
                                                                                            lTransaction)
                If vBeTrans_ajuste_det.IdPresentacion <> 0 Then
                    vBeTrans_ajuste_det.Factor = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(vBeTrans_ajuste_det.IdProductoBodega,
                                                                                                           vBeTrans_ajuste_det.IdPresentacion,
                                                                                                           lConnection,
                                                                                                           lTransaction)
                    vBeTrans_ajuste_det.Nombre_Presentacion = clsLnProducto_presentacion.Get_Nombre_Presentacion_By_IdPresentacion(vBeTrans_ajuste_det.IdPresentacion,
                                                                                                                                   lConnection,
                                                                                                                                   lTransaction)
                Else
                    vBeTrans_ajuste_det.Factor = 0
                    vBeTrans_ajuste_det.Nombre_Presentacion = ""
                End If
                lReturnList.Add(vBeTrans_ajuste_det)
            Next

            lTransaction.Commit()

            Return lReturnList

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function
    Public Shared Function Get_By_IdAjusteEnc(ByVal pIdAjusteEnc As Integer) As List(Of clsBeTrans_ajuste_det)
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim lReturnList As New List(Of clsBeTrans_ajuste_det)
            Const sp As String = "SELECT * FROM Trans_ajuste_det WHERE IdAjusteEnc = @IdAjusteEnc"

            If lConnection.State = ConnectionState.Closed Then lConnection.Open()

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            cmd.Parameters.AddWithValue("@IdAjusteEnc", pIdAjusteEnc)

            lTransaction = lConnection.BeginTransaction()
            cmd.Transaction = lTransaction
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_ajuste_det As New clsBeTrans_ajuste_det

            For Each dr As DataRow In dt.Rows
                vBeTrans_ajuste_det = New clsBeTrans_ajuste_det
                Cargar(vBeTrans_ajuste_det, dr)
                vBeTrans_ajuste_det.UmBas = clsLnUnidad_medida.Get_Codigo_By_IdUnidadMedida(vBeTrans_ajuste_det.IdUnidadMedida,
                                                                                            lConnection,
                                                                                            lTransaction)
                If vBeTrans_ajuste_det.IdPresentacion <> 0 Then
                    vBeTrans_ajuste_det.Factor = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(vBeTrans_ajuste_det.IdProductoBodega,
                                                                                                           vBeTrans_ajuste_det.IdPresentacion,
                                                                                                           lConnection,
                                                                                                           lTransaction)
                    vBeTrans_ajuste_det.Nombre_Presentacion = clsLnProducto_presentacion.Get_Nombre_Presentacion_By_IdPresentacion(vBeTrans_ajuste_det.IdPresentacion,
                                                                                                                                   lConnection,
                                                                                                                                   lTransaction)
                Else
                    vBeTrans_ajuste_det.Factor = 0
                    vBeTrans_ajuste_det.Nombre_Presentacion = ""
                End If
                lReturnList.Add(vBeTrans_ajuste_det)
            Next

            lTransaction.Commit()

            Return lReturnList

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try
            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(idajustedet),0) FROM Trans_ajuste_det"

            Using lCommand As New SqlCommand(vSQL, pConnection, pTransaction) With {.CommandType = CommandType.Text}
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

    Public Shared Function Actualizar_Estado_Enviado_A_ERP(ByVal IdAjusteDet As Integer,
                                                           ByVal Enviado As Boolean) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Upd.Init("trans_ajuste_det")
            Upd.Add("idajustedet", "@idajustedet", DataType.Parametro)
            Upd.Add("enviado", "@enviado", DataType.Parametro)
            Upd.Where("idajustedet = @idajustedet")

            Dim sp As String = Upd.SQL()
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

            cmd.Parameters.Add(New SqlParameter("@IDAJUSTEDET", IdAjusteDet))
            cmd.Parameters.Add(New SqlParameter("@ENVIADO", Enviado))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar_Estado_Enviado_A_ERP(ByVal IdAjusteDet As Integer,
                                                           ByVal Enviado As Boolean,
                                                           ByRef lConnection As SqlConnection,
                                                           ByRef lTransaction As SqlTransaction) As Integer

        Try

            Upd.Init("trans_ajuste_det")
            Upd.Add("idajustedet", "@idajustedet", DataType.Parametro)
            Upd.Add("enviado", "@enviado", DataType.Parametro)
            Upd.Where("idajustedet = @idajustedet")

            Dim sp As String = Upd.SQL()
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

            cmd.Parameters.Add(New SqlParameter("@IDAJUSTEDET", IdAjusteDet))
            cmd.Parameters.Add(New SqlParameter("@ENVIADO", Enviado))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            Return rowsAffected

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar_Estado_And_Referencia_Ajuste_ERP(ByVal oBeAjusteDet As clsBeTrans_ajuste_det) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Upd.Init("trans_ajuste_det")
            Upd.Add("referencia_ajuste_erp", "@referencia_ajuste_erp", DataType.Parametro)
            Upd.Add("estado_ajuste_erp", "@estado_ajuste_erp", DataType.Parametro)
            Upd.Where("idajustedet = @idajustedet")

            Dim sp As String = Upd.SQL()
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

            cmd.Parameters.Add(New SqlParameter("@IDAJUSTEDET", oBeAjusteDet.IdAjusteDet))
            cmd.Parameters.Add(New SqlParameter("@referencia_ajuste_erp", oBeAjusteDet.referencia_ajuste_erp))
            cmd.Parameters.Add(New SqlParameter("@estado_ajuste_erp", oBeAjusteDet.estado_ajuste_erp))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Referencia_Realizada(ByVal oBeAjusteDet As clsBeTrans_ajuste_det) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Referencia_Realizada = False

        Try

            Const sp As String = "SELECT * FROM Trans_ajuste_det " &
            " Where(idajustedet = @idajustedet) and CONVERT(int, referencia_ajuste_erp)>= @referencia_ajuste_erp and estado_ajuste_erp = 1 "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDAJUSTEDET", oBeAjusteDet.IdAjusteDet))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@REFERENCIA_AJUSTE_ERP", oBeAjusteDet.referencia_ajuste_erp))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Referencia_Realizada = True
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdStock_By_IdAjusteEnc(ByVal pIdAjusteEnc As Integer) As List(Of clsBeTrans_ajuste_det)
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim lReturnList As New List(Of clsBeTrans_ajuste_det)
            Const sp As String = "SELECT * FROM Trans_ajuste_det WHERE IdAjusteEnc = @IdAjusteEnc"

            If lConnection.State = ConnectionState.Closed Then lConnection.Open()

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            cmd.Parameters.AddWithValue("@IdAjusteEnc", pIdAjusteEnc)

            lTransaction = lConnection.BeginTransaction()
            cmd.Transaction = lTransaction
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_ajuste_det As New clsBeTrans_ajuste_det

            For Each dr As DataRow In dt.Rows
                vBeTrans_ajuste_det = New clsBeTrans_ajuste_det
                Cargar(vBeTrans_ajuste_det, dr)
                vBeTrans_ajuste_det.UmBas = clsLnUnidad_medida.Get_Codigo_By_IdUnidadMedida(vBeTrans_ajuste_det.IdUnidadMedida,
                                                                                            lConnection,
                                                                                            lTransaction)
                If vBeTrans_ajuste_det.IdPresentacion <> 0 Then
                    vBeTrans_ajuste_det.Factor = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(vBeTrans_ajuste_det.IdProductoBodega,
                                                                                                           vBeTrans_ajuste_det.IdPresentacion,
                                                                                                           lConnection,
                                                                                                           lTransaction)
                    vBeTrans_ajuste_det.Nombre_Presentacion = clsLnProducto_presentacion.Get_Nombre_Presentacion_By_IdPresentacion(vBeTrans_ajuste_det.IdPresentacion,
                                                                                                                                   lConnection,
                                                                                                                                   lTransaction)
                Else
                    vBeTrans_ajuste_det.Factor = 0
                    vBeTrans_ajuste_det.Nombre_Presentacion = ""
                End If
                lReturnList.Add(vBeTrans_ajuste_det)
            Next

            lTransaction.Commit()

            Return lReturnList

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function


    Public Shared Function Get_IdStock_By_IdAjusteDet(ByVal pIdAjusteDet As Integer,
                                                      ByVal pConnection As SqlConnection,
                                                      ByVal pTransaction As SqlTransaction) As Integer

        Try
            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT IdStock FROM Trans_ajuste_det WHERE IdAjusteDet = @IdAjusteDet"

            Using lCommand As New SqlCommand(vSQL, pConnection, pTransaction) With {.CommandType = CommandType.Text}
                lCommand.Parameters.AddWithValue("@IdAjusteDet", pIdAjusteDet)
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

    Public Shared Function Get_IdInventarioEnc_By_IdAjusteDet(ByVal pIdAjusteDet As Integer,
                                                              ByVal pConnection As SqlConnection,
                                                              ByVal pTransaction As SqlTransaction) As Integer

        Try
            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT top(1) ajuste_por_inventario 
                                  FROM Trans_ajuste_enc
                                  WHERE IdAjusteEnc in (SELECT IdAjusteEnc FROM trans_ajuste_det WHERE  IdAjusteDet = @IdAjusteDet)"

            Using lCommand As New SqlCommand(vSQL, pConnection, pTransaction) With {.CommandType = CommandType.Text}
                lCommand.Parameters.AddWithValue("@IdAjusteDet", pIdAjusteDet)
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

    Public Shared Function Actualizar_Estado_Enviado_A_ERP_By_Inventario(ByVal IdStock As Integer,
                                                                         ByVal IdInventario As Integer,
                                                                         ByVal Enviado As Boolean,
                                                                         ByRef lConnection As SqlConnection,
                                                                         ByRef lTransaction As SqlTransaction) As Integer

        Try

            Upd.Init("trans_ajuste_det")
            Upd.Add("enviado", "@enviado", DataType.Parametro)
            Upd.Where("idstock = @idstock AND IdAjusteEnc IN (SELECT IdAjusteEnc FROM trans_ajuste_enc WHERE ajuste_por_inventario =  @IdInventario)")

            Dim sp As String = Upd.SQL()
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

            cmd.Parameters.Add(New SqlParameter("@IdInventario", IdInventario))
            cmd.Parameters.Add(New SqlParameter("@idstock", IdStock))
            cmd.Parameters.Add(New SqlParameter("@ENVIADO", Enviado))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            Return rowsAffected

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function Get_All_Ajustes_By_IdInventarioEnc_For_SAP(ByVal pIdInventarioEnc As Integer,
                                                                      ByVal lConnection As SqlConnection,
                                                                      ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_ajuste_det)

        Get_All_Ajustes_By_IdInventarioEnc_For_SAP = Nothing

        Try

            Dim lReturnList As New List(Of clsBeTrans_ajuste_det)

            Dim vSQL As String = "SELECT 0 IdAjustedet, 0 IdAjusteEnc, 
                                        MAX(IdStock) IdStock, 
                                        0 IdPropietarioBodega, 
                                        t.IdProductoBodega,
                                        MAX(IdProductoEstado) IdProductoEstado,
                                        IdPresentacion, IdUnidadMedida,
                                        MAX(IdUbicacion) IdUbicacion, 
                                        MAX(LoteOrigen) lote_original, 
                                        MAX(LoteOrigen) lote_nuevo, 
                                        MAX(fecha_vence_stock) fecha_vence_original, 
                                        MAX(fecha_vence_stock) fecha_vence_nueva, 
                                        MAX(peso_stock) peso_original, 
                                        SUM(peso_conteo) peso_nuevo, 
                                        SUM(cantidad_stock) cantidad_original, 
                                        SUM(cantidad_conteo) cantidad_nueva, 
                                        p.codigo codigo_producto, 
                                        p.nombre nombre_producto, 
                                        0 idtipoajuste,
                                        0 idmotivoajuste, 
                                        '' observacion, 
                                        '' codigo_ajuste,
                                        0 enviado,
                                        0 IdBodegaERP,
                                        MAX(Licencia)  lic_plate, 
                                        '' referencia_ajuste_erp, 
                                        0 estado_ajuste_erp
                                  FROM (SELECT  trans_inv_ciclico.idinventarioenc, 
                                                trans_inv_ciclico.IdStock,
                                                trans_inv_ciclico.IdProductoBodega, 
		                                        MAX(case when trans_inv_ciclico.nuevo_stock <>0 then case when trans_inv_ciclico.nuevo_stock = -1 then 0 else trans_inv_ciclico.nuevo_stock end else trans_inv_ciclico.cant_stock end) cantidad_stock,
                                                --MAX(trans_inv_ciclico.cant_stock) AS cantidad_stock, 
                                                SUM(trans_inv_ciclico.cantidad) AS cantidad_conteo, 
                                                SUM(trans_inv_ciclico.cant_reconteo) AS cantidad_reconteo, 
                                                MAX(trans_inv_ciclico.peso_stock) AS peso_stock, 
                                                SUM(trans_inv_ciclico.peso) AS peso_conteo, 
                                                SUM(trans_inv_ciclico.peso_reconteo) AS peso_reconteo, 
                                                trans_inv_ciclico.lote_stock as LoteOrigen, 
                                                trans_inv_ciclico.fecha_vence, trans_inv_ciclico.lic_plate AS Licencia, 
                                                dbo.Nombre_Completo_Ubicacion(trans_inv_ciclico.IdUbicacion,trans_inv_ciclico.IdBodega) as UbicacionOrigen,
                                                trans_inv_ciclico.IdUbicacion, MAX(trans_inv_ciclico.fec_mod) Fec_Mod,
                                                trans_inv_ciclico.IdPresentacion, trans_inv_ciclico.IdProductoEstado, 
                                                trans_inv_ciclico.fecha_vence_stock, 
                                                trans_inv_ciclico.IdUnidadMedida, trans_inv_ciclico.IdBodega,
                                                trans_inv_ciclico.regularizar, MAX(trans_inv_ciclico.nuevo_stock) nuevo_stock
                                        FROM    trans_inv_ciclico INNER JOIN
                                                trans_inv_enc ON trans_inv_ciclico.idinventarioenc = trans_inv_enc.idinventarioenc AND trans_inv_ciclico.idinventarioenc = trans_inv_enc.idinventarioenc 
                                        WHERE  (trans_inv_ciclico.idinventarioenc = @idinventario) AND 
                                               (trans_inv_ciclico.Cantidad >= trans_inv_ciclico.cantidad_reservada_umbas )
                                        GROUP BY dbo.trans_inv_ciclico.idinventarioenc,dbo.trans_inv_ciclico.IdStock, trans_inv_ciclico.IdProductoBodega,  
                                                    trans_inv_ciclico.fecha_vence, trans_inv_ciclico.lic_plate, 
                                                    trans_inv_ciclico.IdProductoEstado, trans_inv_ciclico.IdUbicacion, 
                                                    trans_inv_ciclico.lote_stock,trans_inv_ciclico.IdStock,
                                                    trans_inv_ciclico.IdBodega,trans_inv_ciclico.IdUnidadMedida,
			                                        trans_inv_ciclico.IdPresentacion, trans_inv_ciclico.IdProductoEstado, 
			                                        trans_inv_ciclico.fecha_vence_stock,trans_inv_ciclico.IdBodega, 
			                                        trans_inv_ciclico.regularizar) t INNER JOIN 
                                        producto_bodega pb ON t.IdProductoBodega = pb.IdProductoBodega INNER JOIN
	                                    producto p ON p.IdProducto = pb.IdProducto
                                WHERE idinventarioenc = @idinventario AND regularizar = 1 
                                GROUP BY t.IdProductoBodega, idPresentacion, IdUnidadMedida, p.codigo, p.nombre
                                HAVING SUM(cantidad_stock-cantidad_conteo) <>0"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@idinventario", pIdInventarioEnc)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim BeTransAjuste As New clsBeTrans_ajuste_det

                For Each lRow As DataRow In lDataTable.Rows

                    BeTransAjuste = New clsBeTrans_ajuste_det()
                    Cargar(BeTransAjuste, lRow)

                    lReturnList.Add(BeTransAjuste)

                Next

                Return lReturnList

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
