Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnTrans_inv_detalle

    Public Shared Function GetAll(pIdinventarioenc As Integer, pIdtramo As Integer, pIdProducto As Integer) As List(Of clsBeTrans_inv_detalle)

        Dim lReturnList As New List(Of clsBeTrans_inv_detalle)

        Try
            lReturnList = GetAll().FindAll(Function(x) x.Idinventarioenc = pIdinventarioenc).OrderBy(Function(Y) Y.Idtramo).ThenBy(Function(z) z.Nom_producto).ToList()

            If (pIdtramo <> 0) Then lReturnList = lReturnList.FindAll(Function(x) x.Idtramo = pIdtramo).ToList()
            If (pIdProducto <> 0) Then lReturnList = lReturnList.FindAll(Function(x) x.Idproducto = pIdProducto).ToList()

            Return lReturnList
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function InsertarSinID(ByRef oBeTrans_inv_detalle As clsBeTrans_inv_detalle, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_inv_detalle")
            'Ins.Add("idinventariodet", "@idinventariodet", DataType.Parametro)
            Ins.Add("idinventarioenc", "@idinventarioenc", DataType.Parametro)
            Ins.Add("idtramo", "@idtramo", DataType.Parametro)
            Ins.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Ins.Add("idoperador", "@idoperador", DataType.Parametro)
            Ins.Add("idproducto", "@idproducto", DataType.Parametro)
            Ins.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Ins.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Ins.Add("lote", "@lote", DataType.Parametro)
            Ins.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Ins.Add("serie", "@serie", DataType.Parametro)
            Ins.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Ins.Add("cantidad", "@cantidad", DataType.Parametro)
            Ins.Add("fecha_captura", "@fecha_captura", DataType.Parametro)
            Ins.Add("host", "@host", DataType.Parametro)
            Ins.Add("nom_producto", "@nom_producto", DataType.Parametro)
            Ins.Add("nom_operador", "@nom_operador", DataType.Parametro)
            Ins.Add("carga", "@carga", DataType.Parametro)
            Ins.Add("peso", "@peso", DataType.Parametro)
            Ins.Add("IdPropietarioBodega", "@IdPropietarioBodega", DataType.Parametro)
            Ins.Add("nombre_propietario", "@nombre_propietario", DataType.Parametro)
            Ins.Add("lic_plate", "@lic_plate", DataType.Parametro)
            Ins.Add("cod_variante", "@cod_variante", DataType.Parametro)
            'AT20220504 Se agregó este campo para poder almancenar IdBodega
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            '#GT25112022_1400: campos DyD
            Ins.Add("costo", "@costo", DataType.Parametro)
            Ins.Add("precio", "@precio", DataType.Parametro)
            Ins.Add("IdProductoParametroA", "@IdProductoParametroA", DataType.Parametro)
            Ins.Add("IdProductoParametroB", "@IdProductoParametroB", DataType.Parametro)
            Ins.Add("IdProductoTallaColor", "@IdProductoTallaColor", DataType.Parametro)

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

            'cmd.Parameters.Add(New SqlParameter("@IDINVENTARIODET", oBeTrans_inv_detalle.Idinventariodet))
            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_detalle.Idinventarioenc))
            cmd.Parameters.Add(New SqlParameter("@IDTRAMO", oBeTrans_inv_detalle.Idtramo))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeTrans_inv_detalle.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeTrans_inv_detalle.Idoperador))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeTrans_inv_detalle.Idproducto))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeTrans_inv_detalle.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeTrans_inv_detalle.Idunidadmedida))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeTrans_inv_detalle.Lote))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeTrans_inv_detalle.Fecha_vence))
            cmd.Parameters.Add(New SqlParameter("@SERIE", oBeTrans_inv_detalle.Serie))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeTrans_inv_detalle.Idproductoestado))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_inv_detalle.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@FECHA_CAPTURA", oBeTrans_inv_detalle.Fecha_captura))
            cmd.Parameters.Add(New SqlParameter("@HOST", oBeTrans_inv_detalle.Host))
            cmd.Parameters.Add(New SqlParameter("@NOM_PRODUCTO", oBeTrans_inv_detalle.Nom_producto))
            cmd.Parameters.Add(New SqlParameter("@NOM_OPERADOR", oBeTrans_inv_detalle.Nom_operador))
            cmd.Parameters.Add(New SqlParameter("@CARGA", oBeTrans_inv_detalle.Carga))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeTrans_inv_detalle.Peso))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_inv_detalle.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_PROPIETARIO", oBeTrans_inv_detalle.nombre_propietario))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeTrans_inv_detalle.License_plate))
            cmd.Parameters.Add(New SqlParameter("@COD_VARIANTE", oBeTrans_inv_detalle.Codigo_variante))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_inv_detalle.IdBodega))
            '#GT25112022_1400: campos DyD
            cmd.Parameters.Add(New SqlParameter("@COSTO", oBeTrans_inv_detalle.costo))
            cmd.Parameters.Add(New SqlParameter("@PRECIO", oBeTrans_inv_detalle.precio))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOPARAMETROA", oBeTrans_inv_detalle.IdProductoParametroA))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOPARAMETROB", oBeTrans_inv_detalle.IdProductoParametroB))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOTALLACOLOR", oBeTrans_inv_detalle.IdProductoTallaColor))

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

    Public Shared Sub InsertaMulti(i1 As clsBeTrans_inv_detalle, i2 As clsBeTrans_inv_detalle, i3 As clsBeTrans_inv_detalle, i4 As clsBeTrans_inv_detalle)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            If i1.IdUbicacion > 0 Then InsertarSinID(i1, lConnection, lTransaction)
            If i2.IdUbicacion > 0 Then InsertarSinID(i2, lConnection, lTransaction)
            If i3.IdUbicacion > 0 Then InsertarSinID(i3, lConnection, lTransaction)
            If i4.IdUbicacion > 0 Then InsertarSinID(i4, lConnection, lTransaction)

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
            lTransaction.Dispose()
            lConnection.Dispose()
        End Try

    End Sub

    Public Shared Function Get_All_By_IdInventarioEnc(pIdInvEnc As Integer) As List(Of clsBeTrans_inv_detalle)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_detalle)

            Dim vSQL As String = "SELECT * FROM Trans_inv_detalle WHERE idinventarioenc=@idinventarioenc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@idinventarioenc", pIdInvEnc)

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

                        Dim vBeTrans_inv_detalle As New clsBeTrans_inv_detalle

                        For Each dr As DataRow In lDataTable.Rows

                            vBeTrans_inv_detalle = New clsBeTrans_inv_detalle
                            Cargar(vBeTrans_inv_detalle, dr)
                            lReturnList.Add(vBeTrans_inv_detalle)

                        Next

                    End Using

                    lTransaction.Commit()

                End Using


                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdInvDet(ByVal pIdInvDet As Integer) As clsBeTrans_inv_detalle

        Try

            Dim vSQL As String = "SELECT * FROM Trans_inv_detalle Where idinventariodet = @idinventariodet"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@idinventariodet", pIdInvDet)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim ObjProducto As New clsBeTrans_inv_detalle()

                            Cargar(ObjProducto, lRow)

                            Return ObjProducto

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return Nothing

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub Eliminar_Registros_Cantidad_0(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_inv_detalle Where (cantidad=0)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.ExecuteNonQuery()

            cmd.Dispose()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try
    End Sub

    Public Shared Function Get_All_By_InvEncUbic(pIdInvEnc As Integer, pIdUbicacion As Integer) As List(Of clsBeTrans_inv_detalle)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_detalle)

            Dim vSQL As String = "SELECT * FROM Trans_inv_detalle WHERE idinventarioenc=@idinventarioenc AND idubicacion=@idubicacion"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@idinventarioenc", pIdInvEnc)
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@idubicacion", pIdUbicacion)

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

                        Dim vBeTrans_inv_detalle As New clsBeTrans_inv_detalle

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_inv_detalle = New clsBeTrans_inv_detalle
                            Cargar(vBeTrans_inv_detalle, dr)
                            lReturnList.Add(vBeTrans_inv_detalle)
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_CantidadInvConteo_By_Producto(pIdUbicacion As Integer,
                                                          pIdProducto As Integer,
                                                          pIdBodega As Integer,
                                                          pIdPresentacion As Integer) As clsBeTrans_inv_detalle

        Dim lReturnValue As New clsBeTrans_inv_detalle

        Try


            Dim vSQL As String = "select 0 as idinventariodet,
                                idinventarioenc,
                                idtramo,
                                IdUbicacion,
                                0 as idoperador,
                                idproducto,
                                IdPresentacion,
                                idunidadmedida,
                                '' as lote,
                                '1900-01-01' as fecha_vence,
                                serie,
                                0 as idproductoestado,
                                '1900-01-01' as fecha_captura,
                                host,
                                nom_producto,
                                nom_operador,
                                carga,peso,
                                IdPropietarioBodega,
                                nombre_propietario,
                                lic_plate,
                                cod_variante,
                                idbodega, 
                                SUM(Cantidad) as Cantidad, costo 
                                from trans_inv_detalle
                                where idubicacion = @idubicacion
                                and idproducto =  @idproducto
                                and idbodega = @idbodega "

            If pIdPresentacion <> 0 Then
                vSQL += " And idpresentacion = @idpresentacion"
            Else
                vSQL += " And idpresentacion = 0"
            End If

            vSQL += " group by idinventarioenc,idtramo,IdUbicacion,idproducto,IdPresentacion,idunidadmedida,serie,
                    host,nom_producto,nom_operador,carga,peso,IdPropietarioBodega,nombre_propietario,lic_plate,cod_variante,idbodega,costo"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@idubicacion", pIdUbicacion)
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@idproducto", pIdProducto)
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@idbodega", pIdBodega)


                        If pIdPresentacion <> 0 Then
                            lDataAdapter.SelectCommand.Parameters.AddWithValue("@idpresentacion", pIdPresentacion)
                        End If

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

                        Dim vBeTrans_inv_detalle As New clsBeTrans_inv_detalle

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_inv_detalle = New clsBeTrans_inv_detalle
                            Cargar(vBeTrans_inv_detalle, dr)

                            lReturnValue = vBeTrans_inv_detalle
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnValue

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Function

    Public Shared Function Get_Conteo_By_IdProducto_And_IdUbicacion(ByVal pIdUbicacion As Integer,
                                                                    ByVal pIdBodega As Integer,
                                                                    ByVal pIdProducto As Integer) As List(Of clsBeTrans_inv_detalle)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_detalle)

            Dim vSQL As String = "select * from trans_inv_detalle
                                  where IdUbicacion = @IdUbicacion And 
                                      IdBodega = @IdBodega And
                                      IdProducto = @IdProducto"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text

                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

                        Dim vBeTrans_inv_detalle As New clsBeTrans_inv_detalle

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_inv_detalle = New clsBeTrans_inv_detalle
                            Cargar(vBeTrans_inv_detalle, dr)
                            lReturnList.Add(vBeTrans_inv_detalle)
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Valida_Producto_ConteoInventario(pIdBodega As Integer,
                                                            pIdUbicacion As Integer,
                                                            pIdProducto As Integer) As List(Of clsBeTrans_inv_detalle)
        Try
            Dim lReturnList As New List(Of clsBeTrans_inv_detalle)

            Dim vSQL As String = "select * from trans_inv_detalle 
                                  where idubicacion = @idubicacion and 
                                        idproducto=@idproducto and 
                                        idbodega = @idbodega"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text

                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@idbodega", pIdBodega)
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@idproducto", pIdProducto)
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@idubicacion", pIdUbicacion)

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

                        Dim vBeTrans_inv_detalle As New clsBeTrans_inv_detalle


                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_inv_detalle = New clsBeTrans_inv_detalle
                            Cargar(vBeTrans_inv_detalle, dr)
                            lReturnList.Add(vBeTrans_inv_detalle)
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Function

    '#MA20260105 
    Public Shared Function Get_Ubicaciones_No_Contadas_DT(IdInventario As Integer,
                                                          IdBodega As Integer,
                                                          ByRef lConnection As SqlConnection,
                                                          ByRef lTransaction As SqlTransaction) As DataTable

        Dim DT As New DataTable

        Dim SQL As String = "SELECT DISTINCT
                             u.IdUbicacion, u.IdBodega,
                             dbo.Nombre_Completo_Ubicacion(u.IdUbicacion, u.IdBodega) AS Ubicacion,
                                a.Descripcion AS Area,
                                s.Descripcion AS Sector,
                                t.Descripcion AS Tramo
                            FROM bodega_ubicacion u
                            INNER JOIN bodega_tramo t ON u.IdTramo = t.IdTramo AND u.IdBodega = t.IdBodega
                            INNER JOIN bodega_sector s ON t.IdSector = s.IdSector AND u.IdBodega = s.IdBodega
                            INNER JOIN bodega_area a ON s.IdArea = a.IdArea AND u.IdBodega = a.IdBodega
                            WHERE u.IdBodega = @IdBodega AND u.bloqueada = 0 
						    AND
							Exists(Select * 
							from trans_inv_tramo tit
							where IdInventario = @IdInventario AND tit.idtramo = u.IdTramo And 
							      u.IdBodega = tit.IdBodega)
							AND u.IdUbicacion NOT IN (
							Select Distinct IdUbicacion 
							from trans_inv_detalle
							where idinventarioenc = @IdInventario) AND 
							u.IdUbicacion NOT IN (SELECT u.IdUbicacion
                            FROM bodega_ubicacion u
                            INNER JOIN trans_inv_tramo tt ON u.IdTramo = tt.IdTramo
                            WHERE tt.IdInventario = @IdInventario and tt.det_estado= 'Finalizado'
                            AND u.IdBodega = @IdBodega )
                            ORDER BY Area, Sector, Tramo, Ubicacion"

        Using da As New SqlDataAdapter(SQL, lConnection)
            da.SelectCommand.Transaction = lTransaction
            da.SelectCommand.CommandType = CommandType.Text

            da.SelectCommand.Parameters.AddWithValue("@IdInventario", IdInventario)
            da.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

            da.Fill(DT)
        End Using
        Return DT
    End Function

    '#MA20260105
    Public Shared Function Get_Total_Ubicaciones_Asig(IdInventarioEnc As Integer,
                                                      IdBodega As Integer,
                                                      lConnection As SqlConnection,
                                                      lTransaction As SqlTransaction) As Integer

        Dim cmd As New SqlCommand()
        Dim Total As Integer = 0

        Try
            cmd.Connection = lConnection
            cmd.Transaction = lTransaction
            cmd.CommandType = CommandType.Text

            cmd.CommandText = "SELECT COUNT(*) 
                               FROM bodega_ubicacion u
                               INNER JOIN trans_inv_tramo tt ON u.IdTramo = tt.IdTramo
                               WHERE tt.IdInventario = @IdInventarioEnc
                               AND u.IdBodega = @IdBodega AND u.bloqueada = 0 "

            cmd.Parameters.AddWithValue("@IdInventarioEnc", IdInventarioEnc)
            cmd.Parameters.AddWithValue("@IdBodega", IdBodega)

            Total = Convert.ToInt32(cmd.ExecuteScalar())

        Catch ex As Exception
            Throw
        End Try

        Return Total

    End Function

    '#MA20260105
    Public Shared Function Get_Ubicaciones_Contadas_NoFinalizadas(IdInventario As Integer,
                                                                  IdBodega As Integer,
                                                                  ByRef lConnection As SqlConnection,
                                                                  ByRef lTransaction As SqlTransaction) As Integer
        Dim Total As Integer = 0

        Try
            If lConnection.State <> ConnectionState.Open Then
                lConnection.Open()
            End If

            Dim SQL As String = "SELECT COUNT(DISTINCT u.IdUbicacion)
                                 FROM bodega_ubicacion u
                                 INNER JOIN trans_inv_tramo tt ON u.IdTramo = tt.IdTramo
                                 WHERE tt.IdInventario = @IdInventario and tt.det_estado <> 'Finalizado'
                                  AND u.IdBodega = @IdBodega
                                  AND EXISTS (
                                        SELECT 1
                                        FROM trans_inv_detalle d
                                        WHERE d.IdUbicacion = u.IdUbicacion
                                          AND d.IdInventarioEnc = @IdInventario
                                      )"

            Using cmd As New SqlCommand(SQL, lConnection, lTransaction)
                cmd.CommandType = CommandType.Text
                cmd.Parameters.AddWithValue("@IdInventario", IdInventario)
                cmd.Parameters.AddWithValue("@IdBodega", IdBodega)
                Total = Convert.ToInt32(cmd.ExecuteScalar())
            End Using

        Catch
            Throw
        End Try

        Return Total
    End Function

    '#MA20260105
    Public Shared Function Get_Ubicaciones_Contadas_Finalizadas(IdInventario As Integer,
                                                                IdBodega As Integer,
                                                                ByRef lConnection As SqlConnection,
                                                                ByRef lTransaction As SqlTransaction) As Integer
        Dim Total As Integer = 0

        Try
            If lConnection.State <> ConnectionState.Open Then
                lConnection.Open()
            End If

            Dim SQL As String = "SELECT COUNT(DISTINCT u.IdUbicacion)
                                 FROM bodega_ubicacion u
                                 INNER JOIN trans_inv_tramo tt ON u.IdTramo = tt.IdTramo
                                 WHERE tt.IdInventario = @IdInventario and tt.det_estado = 'Finalizado'
                                  AND u.IdBodega = @IdBodega"

            Using cmd As New SqlCommand(SQL, lConnection, lTransaction)
                cmd.CommandType = CommandType.Text
                cmd.Parameters.AddWithValue("@IdInventario", IdInventario)
                cmd.Parameters.AddWithValue("@IdBodega", IdBodega)
                Total = Convert.ToInt32(cmd.ExecuteScalar())
            End Using

        Catch
            Throw
        End Try

        Return Total
    End Function

    Public Shared Function Get_Ubicaciones_Contadas_DT(pIdInventarioEnc As Integer,
                                                      pIdBodega As Integer,
                                                      ByRef lConnection As SqlConnection,
                                                      ByRef lTransaction As SqlTransaction) As DataTable

        Dim DT As New DataTable

        Dim SQL As String = "SELECT dbo.Nombre_Completo_Ubicacion(IdUbicacion, idbodega) Ubicación, 
                                    COUNT(DISTINCT lic_plate) Licencias_Distintas, 
                                    COUNT(DISTINCT idproducto) Productos_Distintos,
                                    COUNT(DISTINCT IdOperador) Operadores_Distintos,
	                                COUNT(idinventariodet) Conteos
                             FROM trans_inv_detalle
                             WHERE idinventarioenc = @IdInventarioEnc AND IdBodega = @IdBodega
                             GROUP BY IdUbicacion, idbodega"

        Using da As New SqlDataAdapter(SQL, lConnection)
            da.SelectCommand.Transaction = lTransaction
            da.SelectCommand.CommandType = CommandType.Text

            da.SelectCommand.Parameters.AddWithValue("@IdInventarioEnc", pIdInventarioEnc)
            da.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

            da.Fill(DT)
        End Using
        Return DT
    End Function

    Public Shared Function Get_Tiempos_Tramos_Conteo_DT(pIdInventarioEnc As Integer,
                                                     ByRef lConnection As SqlConnection,
                                                     ByRef lTransaction As SqlTransaction) As DataTable

        Dim DT As New DataTable

        Dim SQL As String = "SELECT t.descripcion Tramo,LTRIM(RTRIM(
                                            SUBSTRING(
                                                dbo.Nombre_Completo_Ubicacion(i.idubicacion, i.idbodega),
                                                CHARINDEX('N', dbo.Nombre_Completo_Ubicacion(i.idubicacion, i.idbodega)),
                                                CHARINDEX(' - ', dbo.Nombre_Completo_Ubicacion(i.idubicacion, i.idbodega) + ' - ', 
			                                    CHARINDEX('N', dbo.Nombre_Completo_Ubicacion(i.idubicacion, i.idbodega))) - 
			                                    CHARINDEX('N', dbo.Nombre_Completo_Ubicacion(i.idubicacion, i.idbodega))
                                            )
                                        )) AS Nivel,LTRIM(RTRIM(
                                            SUBSTRING(
                                                dbo.Nombre_Completo_Ubicacion(i.idubicacion, i.idbodega),
                                                CHARINDEX('C', dbo.Nombre_Completo_Ubicacion(i.idubicacion, i.idbodega)),
                                                CHARINDEX(' - ', dbo.Nombre_Completo_Ubicacion(i.idubicacion, i.idbodega) + ' - ', 
			                                    CHARINDEX('C', dbo.Nombre_Completo_Ubicacion(i.idubicacion, i.idbodega))) - 
			                                    CHARINDEX('C', dbo.Nombre_Completo_Ubicacion(i.idubicacion, i.idbodega))
                                            )
                                        )) AS Columna,min(i.fecha_captura) Fecha_Inicio, max(i.fecha_captura) Fecha_Fin, 
                                    DATEDIFF(MINUTE,min(i.fecha_captura),max(i.fecha_captura)) Tiempo_Conteo,
                                    COUNT(DISTINCT i.IdUbicacion) UbiContadas
                             FROM trans_inv_detalle i inner join bodega_tramo t on i.idtramo = t.IdTramo and i.idbodega = t.IdBodega
                             WHERE idinventarioenc = @IdInventarioEnc
                             GROUP BY t.descripcion, LTRIM(RTRIM(
                                            SUBSTRING(
                                                dbo.Nombre_Completo_Ubicacion(i.idubicacion, i.idbodega),
                                                CHARINDEX('N', dbo.Nombre_Completo_Ubicacion(i.idubicacion, i.idbodega)),
                                                CHARINDEX(' - ', dbo.Nombre_Completo_Ubicacion(i.idubicacion, i.idbodega) + ' - ', 
			                                    CHARINDEX('N', dbo.Nombre_Completo_Ubicacion(i.idubicacion, i.idbodega))) - 
			                                    CHARINDEX('N', dbo.Nombre_Completo_Ubicacion(i.idubicacion, i.idbodega))
                                            )
                                        )),LTRIM(RTRIM(
                                            SUBSTRING(
                                                dbo.Nombre_Completo_Ubicacion(i.idubicacion, i.idbodega),
                                                CHARINDEX('C', dbo.Nombre_Completo_Ubicacion(i.idubicacion, i.idbodega)),
                                                CHARINDEX(' - ', dbo.Nombre_Completo_Ubicacion(i.idubicacion, i.idbodega) + ' - ', 
			                                    CHARINDEX('C', dbo.Nombre_Completo_Ubicacion(i.idubicacion, i.idbodega))) - 
			                                    CHARINDEX('C', dbo.Nombre_Completo_Ubicacion(i.idubicacion, i.idbodega))
                                            )
                                        ))
                             UNION
                             SELECT t.descripcion Tramo, '' Nivel, '' Columna, '19000101' Fecha_Inicio, '19000101' Fecha_Fin, 
                                    0 Diferencia, 0 UbiContadas
                             FROM trans_inv_tramo i inner join bodega_tramo t on i.idtramo = t.IdTramo and i.idbodega = t.IdBodega
                             WHERE idinventario = @IdInventarioEnc and i.idtramo not in (SELECT idtramo
                             FROM trans_inv_detalle
                             WHERE idinventarioenc = @IdInventarioEnc)"

        Using da As New SqlDataAdapter(SQL, lConnection)
            da.SelectCommand.Transaction = lTransaction
            da.SelectCommand.CommandType = CommandType.Text

            da.SelectCommand.Parameters.AddWithValue("@IdInventarioEnc", pIdInventarioEnc)

            da.Fill(DT)
        End Using
        Return DT
    End Function

End Class
