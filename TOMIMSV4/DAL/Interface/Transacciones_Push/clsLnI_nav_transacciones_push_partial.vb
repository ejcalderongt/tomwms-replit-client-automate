Imports System.Data.SqlClient

Partial Public Class clsLnI_nav_transacciones_push

    Public Shared Function Guardar_Transaccion_Existente(ByVal DocumentoUbicacion As String,
                                                         ByVal Recepcion_Almacen As String,
                                                         ByVal Tipo_Push As String,
                                                         ByVal Error_Push As String,
                                                         ByVal IdRecepcionEnc As Integer,
                                                         ByVal IdRecepcionDet As Integer,
                                                         ByVal IdUsuario As Integer) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim oBeI_nav_transacciones_push As New clsBeI_nav_transacciones_push
            Dim beTransReDet As New clsBeTrans_re_det
            Dim beTransReEnc As New clsBeTrans_re_enc
            Dim pIdEmpresa As Integer

            Dim rowsAffected As Integer = 0

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim vMax As Integer = MaxID(lConnection,
                                        lTransaction)

            beTransReEnc = clsLnTrans_re_enc.GetSingle(IdRecepcionEnc,
                                                       lConnection,
                                                       lTransaction)


            pIdEmpresa = clsLnPropietario_bodega.GetIdEmpresa_By_IdPropietarioBodega(beTransReDet.IdPropietarioBodega,
                                                                                     lConnection,
                                                                                     lTransaction)

            beTransReDet = beTransReEnc.Detalle.Find(Function(x) x.IdRecepcionDet = IdRecepcionDet)

            oBeI_nav_transacciones_push.IdRecepcionEnc = beTransReDet.IdRecepcionEnc
            oBeI_nav_transacciones_push.IdRecepcionDet = beTransReDet.IdRecepcionDet
            GetSingle_By_RecepcionDet(oBeI_nav_transacciones_push, lConnection, lTransaction)

            If oBeI_nav_transacciones_push Is Nothing Then

                oBeI_nav_transacciones_push = New clsBeI_nav_transacciones_push

                oBeI_nav_transacciones_push.IdTransaccionPush = vMax + 1
                oBeI_nav_transacciones_push.IdEmpresa = pIdEmpresa
                oBeI_nav_transacciones_push.IdBodega = beTransReEnc.IdBodega
                oBeI_nav_transacciones_push.IdPropietariobodega = beTransReEnc.OrdenCompraRec.OC.IdPropietarioBodega
                oBeI_nav_transacciones_push.IdOrdenCompra = beTransReEnc.OrdenCompraRec.IdOrdenCompraEnc
                oBeI_nav_transacciones_push.IdRecepcionEnc = beTransReDet.IdRecepcionEnc
                oBeI_nav_transacciones_push.IdRecepcionDet = beTransReDet.IdRecepcionDet
                oBeI_nav_transacciones_push.Idproductobodega = beTransReDet.IdProductoBodega
                oBeI_nav_transacciones_push.Idproducto = beTransReDet.Producto.IdProducto
                oBeI_nav_transacciones_push.Idunidadmedida = beTransReDet.IdUnidadMedida
                oBeI_nav_transacciones_push.Idpresentacion = beTransReDet.IdPresentacion
                oBeI_nav_transacciones_push.Idproductoestado = beTransReDet.IdProductoEstado

                If Tipo_Push = "Push_Recepcion_Devolucion_Venta_To_NAV_For_BYB" Then
                    oBeI_nav_transacciones_push.Cantidad = beTransReDet.cantidad_recibida
                Else
                    If beTransReDet.IdPresentacion <> 0 Then
                        If beTransReDet.Presentacion.Factor = 0 Then
                            oBeI_nav_transacciones_push.Cantidad = beTransReDet.cantidad_recibida
                        Else
                            oBeI_nav_transacciones_push.Cantidad = beTransReDet.cantidad_recibida * beTransReDet.Presentacion.Factor
                        End If
                    Else
                        oBeI_nav_transacciones_push.Cantidad = beTransReDet.cantidad_recibida
                    End If
                End If

                oBeI_nav_transacciones_push.Peso = beTransReDet.Peso
                oBeI_nav_transacciones_push.Lote = beTransReDet.Lote
                oBeI_nav_transacciones_push.Fecha_vence = beTransReDet.Fecha_vence
                oBeI_nav_transacciones_push.No_linea = beTransReDet.No_Linea
                oBeI_nav_transacciones_push.Codigo_variante = beTransReDet.Atributo_Variante_1
                oBeI_nav_transacciones_push.Nom_unidad_medida = beTransReDet.Nombre_unidad_medida
                oBeI_nav_transacciones_push.Tipo_transaccion = "INGRESO"
                oBeI_nav_transacciones_push.IdTipoDocumento = beTransReEnc.OrdenCompraRec.OC.IdTipoIngresoOC
                oBeI_nav_transacciones_push.Tipo_push = Tipo_Push
                oBeI_nav_transacciones_push.No_recepcion_almacen = Recepcion_Almacen
                oBeI_nav_transacciones_push.Documento_ubicacion = DocumentoUbicacion
                oBeI_nav_transacciones_push.Documento_ingreso = beTransReEnc.OrdenCompraRec.OC.Referencia
                oBeI_nav_transacciones_push.Documento_recepcion = beTransReEnc.OrdenCompraRec.OC.No_Documento_Recepcion_ERP
                oBeI_nav_transacciones_push.Location_code = ""
                oBeI_nav_transacciones_push.Zone_code = ""
                oBeI_nav_transacciones_push.Bin_code = ""
                oBeI_nav_transacciones_push.Assigne_user_id = ""
                oBeI_nav_transacciones_push.Item_no = ""
                oBeI_nav_transacciones_push.No_orden_prod = ""
                oBeI_nav_transacciones_push.Respuesta_push = Error_Push
                oBeI_nav_transacciones_push.Enviado_A_ERP = False
                oBeI_nav_transacciones_push.Fec_agr = Now
                oBeI_nav_transacciones_push.User_agr = IdUsuario
                oBeI_nav_transacciones_push.Fec_mod = Now
                oBeI_nav_transacciones_push.User_mod = IdUsuario

                rowsAffected = Insertar(oBeI_nav_transacciones_push,
                                    lConnection,
                                    lTransaction)

            Else

                oBeI_nav_transacciones_push.Tipo_push = Tipo_Push
                oBeI_nav_transacciones_push.Respuesta_push = Error_Push
                oBeI_nav_transacciones_push.Enviado_A_ERP = False
                oBeI_nav_transacciones_push.Fec_mod = Now
                oBeI_nav_transacciones_push.User_mod = IdUsuario
                oBeI_nav_transacciones_push.No_recepcion_almacen = Recepcion_Almacen
                oBeI_nav_transacciones_push.Documento_ubicacion = DocumentoUbicacion

                rowsAffected = Actualizar(oBeI_nav_transacciones_push,
                                          lConnection,
                                          lTransaction)
            End If


            lTransaction.Commit()

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

    Public Shared Function Guardar_Transaccion_Existente(ByVal DocumentoUbicacion As String,
                                                         ByVal Recepcion_Almacen As String,
                                                         ByVal Tipo_Push As String,
                                                         ByVal Error_Push As String,
                                                         ByVal IdRecepcionEnc As Integer,
                                                         ByVal IdRecepcionDet As Integer,
                                                         ByVal IdUsuario As Integer,
                                                         ByVal lConnection As SqlConnection,
                                                         ByVal lTransaction As SqlTransaction) As Integer

        Try

            Dim oBeI_nav_transacciones_push As New clsBeI_nav_transacciones_push
            Dim beTransReDet As New clsBeTrans_re_det
            Dim beTransReEnc As New clsBeTrans_re_enc
            Dim pIdEmpresa As Integer

            Dim rowsAffected As Integer = 0

            Dim vMax As Integer = MaxID(lConnection,
                                        lTransaction)

            beTransReEnc = clsLnTrans_re_enc.GetSingle(IdRecepcionEnc,
                                                       lConnection,
                                                       lTransaction)

            pIdEmpresa = clsLnPropietario_bodega.GetIdEmpresa_By_IdPropietarioBodega(beTransReDet.IdPropietarioBodega,
                                                                                     lConnection,
                                                                                     lTransaction)

            beTransReDet = beTransReEnc.Detalle.Find(Function(x) x.IdRecepcionDet = IdRecepcionDet)

            oBeI_nav_transacciones_push.IdRecepcionEnc = beTransReDet.IdRecepcionEnc
            oBeI_nav_transacciones_push.IdRecepcionDet = beTransReDet.IdRecepcionDet
            GetSingle_By_RecepcionDet(oBeI_nav_transacciones_push, lConnection, lTransaction)

            If oBeI_nav_transacciones_push Is Nothing Then

                oBeI_nav_transacciones_push = New clsBeI_nav_transacciones_push

                oBeI_nav_transacciones_push.IdTransaccionPush = vMax + 1
                oBeI_nav_transacciones_push.IdEmpresa = pIdEmpresa
                oBeI_nav_transacciones_push.IdBodega = beTransReEnc.IdBodega
                oBeI_nav_transacciones_push.IdPropietariobodega = beTransReEnc.OrdenCompraRec.OC.IdPropietarioBodega
                oBeI_nav_transacciones_push.IdOrdenCompra = beTransReEnc.OrdenCompraRec.IdOrdenCompraEnc
                oBeI_nav_transacciones_push.IdRecepcionEnc = beTransReDet.IdRecepcionEnc
                oBeI_nav_transacciones_push.IdRecepcionDet = beTransReDet.IdRecepcionDet
                oBeI_nav_transacciones_push.Idproductobodega = beTransReDet.IdProductoBodega
                oBeI_nav_transacciones_push.Idproducto = beTransReDet.Producto.IdProducto
                oBeI_nav_transacciones_push.Idunidadmedida = beTransReDet.IdUnidadMedida
                oBeI_nav_transacciones_push.Idpresentacion = beTransReDet.IdPresentacion
                oBeI_nav_transacciones_push.Idproductoestado = beTransReDet.IdProductoEstado

                If Tipo_Push = "Push_Recepcion_Devolucion_Venta_To_NAV_For_BYB" Then
                    oBeI_nav_transacciones_push.Cantidad = beTransReDet.cantidad_recibida
                Else
                    If beTransReDet.IdPresentacion <> 0 Then
                        If beTransReDet.Presentacion.Factor = 0 Then
                            oBeI_nav_transacciones_push.Cantidad = beTransReDet.cantidad_recibida
                        Else
                            oBeI_nav_transacciones_push.Cantidad = beTransReDet.cantidad_recibida * beTransReDet.Presentacion.Factor
                        End If
                    Else
                        oBeI_nav_transacciones_push.Cantidad = beTransReDet.cantidad_recibida
                    End If
                End If

                oBeI_nav_transacciones_push.Peso = beTransReDet.Peso
                oBeI_nav_transacciones_push.Lote = beTransReDet.Lote
                oBeI_nav_transacciones_push.Fecha_vence = beTransReDet.Fecha_vence
                oBeI_nav_transacciones_push.No_linea = beTransReDet.No_Linea
                oBeI_nav_transacciones_push.Codigo_variante = beTransReDet.Atributo_Variante_1
                oBeI_nav_transacciones_push.Nom_unidad_medida = beTransReDet.Nombre_unidad_medida
                oBeI_nav_transacciones_push.Tipo_transaccion = "INGRESO"
                oBeI_nav_transacciones_push.IdTipoDocumento = beTransReEnc.OrdenCompraRec.OC.IdTipoIngresoOC
                oBeI_nav_transacciones_push.Tipo_push = Tipo_Push
                oBeI_nav_transacciones_push.No_recepcion_almacen = Recepcion_Almacen
                oBeI_nav_transacciones_push.Documento_ubicacion = DocumentoUbicacion
                oBeI_nav_transacciones_push.Documento_ingreso = beTransReEnc.OrdenCompraRec.OC.Referencia
                oBeI_nav_transacciones_push.Documento_recepcion = beTransReEnc.OrdenCompraRec.OC.No_Documento_Recepcion_ERP
                oBeI_nav_transacciones_push.Location_code = ""
                oBeI_nav_transacciones_push.Zone_code = ""
                oBeI_nav_transacciones_push.Bin_code = ""
                oBeI_nav_transacciones_push.Assigne_user_id = ""
                oBeI_nav_transacciones_push.Item_no = ""
                oBeI_nav_transacciones_push.No_orden_prod = ""
                oBeI_nav_transacciones_push.Respuesta_push = Error_Push
                oBeI_nav_transacciones_push.Enviado_A_ERP = False
                oBeI_nav_transacciones_push.Fec_agr = Now
                oBeI_nav_transacciones_push.User_agr = IdUsuario
                oBeI_nav_transacciones_push.Fec_mod = Now
                oBeI_nav_transacciones_push.User_mod = IdUsuario

                rowsAffected = Insertar(oBeI_nav_transacciones_push,
                                    lConnection,
                                    lTransaction)

            Else

                oBeI_nav_transacciones_push.Tipo_push = Tipo_Push
                oBeI_nav_transacciones_push.Respuesta_push = Error_Push
                oBeI_nav_transacciones_push.Enviado_A_ERP = False
                oBeI_nav_transacciones_push.Fec_mod = Now
                oBeI_nav_transacciones_push.User_mod = IdUsuario
                oBeI_nav_transacciones_push.No_recepcion_almacen = Recepcion_Almacen
                oBeI_nav_transacciones_push.Documento_ubicacion = DocumentoUbicacion

                rowsAffected = Actualizar(oBeI_nav_transacciones_push,
                                          lConnection,
                                          lTransaction)
            End If

            Return rowsAffected

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByVal pConnection As SqlConnection,
                                 ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdTransaccionPush),0) FROM I_nav_transacciones_push"

            Using lCommand As New SqlCommand(sp, pConnection)

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

    Public Shared Function Get_All_By_Fecha(ByVal pFechaDel As Date,
                                            ByVal pFechaAl As Date,
                                            ByVal pEnviado As Boolean) As List(Of clsBeI_nav_transacciones_push)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lReturnList As New List(Of clsBeI_nav_transacciones_push)

            Dim vSQL As String = "SELECT * FROM I_nav_transacciones_push "

            vSQL += String.Format(" Where cast(fec_agr AS DATE) BETWEEN {0} AND {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            vSQL += String.Format(" AND Enviado_a_ERP = {0} ", IIf(pEnviado, 0, 1))

            vSQL += " ORDER BY fec_agr "

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_transacciones_push As New clsBeI_nav_transacciones_push

            For Each dr As DataRow In dt.Rows
                vBeI_nav_transacciones_push = New clsBeI_nav_transacciones_push
                Cargar(vBeI_nav_transacciones_push, dr)
                lReturnList.Add(vBeI_nav_transacciones_push)
            Next

            lTransaction.Commit()

            Return lReturnList

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lTransaction.Dispose()
            lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Get_All_No_Enviadas(ByVal pIdBodega As Integer) As List(Of clsBeI_nav_transacciones_push)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lReturnList As New List(Of clsBeI_nav_transacciones_push)

            Dim vSQL As String = "SELECT * FROM I_nav_transacciones_push Where Enviado_A_ERP = 0 AND IdBodega = @IdBodega "

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            cmd.Parameters.AddWithValue("@IdBodega", pIdBodega)
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_transacciones_push As New clsBeI_nav_transacciones_push

            For Each dr As DataRow In dt.Rows
                vBeI_nav_transacciones_push = New clsBeI_nav_transacciones_push
                Cargar(vBeI_nav_transacciones_push, dr)
                lReturnList.Add(vBeI_nav_transacciones_push)
            Next

            lTransaction.Commit()

            Return lReturnList

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lTransaction.Dispose()
            lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar_Bandera_Enviado(ByRef oBeI_nav_transacciones_push As clsBeI_nav_transacciones_push,
                                                      Optional ByVal pConection As SqlConnection = Nothing,
                                                      Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("i_nav_transacciones_push")
            Upd.Add("Enviado_A_ERP", "@Enviado_A_ERP", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("IdTransaccionPush = @IdTransaccionPush")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTRANSACCIONPUSH", oBeI_nav_transacciones_push.IdTransaccionPush))
            cmd.Parameters.Add(New SqlParameter("@ENVIADO_A_ERP", oBeI_nav_transacciones_push.Enviado_A_ERP))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeI_nav_transacciones_push.Fec_mod))


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

    Public Shared Function Actualizar_Ubic_FechaV_Lote(ByRef oBeTrans_oc_det_lote As clsBeTrans_oc_det_lote,
                                                       ByVal pUbicacionAnterior As String,
                                                       Optional ByVal pConection As SqlConnection = Nothing,
                                                       Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("i_nav_transacciones_push")
            Upd.Add("documento_ubicacion", "@Ubicacion", DataType.Parametro)
            Upd.Add("Lote", "@Lote", DataType.Parametro)
            Upd.Add("Fecha_Vence", "@Fecha_Vence", DataType.Parametro)
            Upd.Where("documento_ubicacion = @Documento_Ubicacion")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@UBICACION", oBeTrans_oc_det_lote.Ubicacion))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeTrans_oc_det_lote.Lote))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeTrans_oc_det_lote.Fecha_vence))
            cmd.Parameters.Add(New SqlParameter("@DOCUMENTO_UBICACION", pUbicacionAnterior))

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

    Public Shared Sub GetSingle_By_RecepcionDet(ByRef pBeI_nav_transacciones_push As clsBeI_nav_transacciones_push,
                                                Optional ByVal pConnection As SqlConnection = Nothing,
                                                Optional ByVal pTransaction As SqlTransaction = Nothing)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim lDTA As New SqlDataAdapter

        Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)

        Try

            Const sp As String = "SELECT * FROM I_nav_transacciones_push" &
            " Where (IdRecepcionEnc = @IdRecepcionEnc) AND (IdRecepcionDet = @IdRecepcionDet) "

            If Es_Transaccion_Remota Then
                lDTA = New SqlDataAdapter(sp, pConnection)
                lDTA.SelectCommand.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                lDTA = New SqlDataAdapter(sp, lConnection)
                lDTA.SelectCommand.Transaction = lTransaction
            End If

            lDTA.SelectCommand.CommandType = CommandType.Text
            lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pBeI_nav_transacciones_push.IdRecepcionEnc)
            lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionDet", pBeI_nav_transacciones_push.IdRecepcionDet)

            Dim lDataTable As New DataTable
            lDTA.Fill(lDataTable)

            If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                Cargar(pBeI_nav_transacciones_push, lDataTable.Rows(0))
            Else
                pBeI_nav_transacciones_push = Nothing
            End If

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Sub


End Class
