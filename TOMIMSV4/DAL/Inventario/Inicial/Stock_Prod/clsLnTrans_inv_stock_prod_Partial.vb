Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnTrans_inv_stock_prod

    Public Shared Sub Importar_Productos(ByRef pListInvStockPrd As List(Of clsBeTrans_inv_stock_prod), ByVal InsertaInv As Boolean,
                                         ByVal IdBodega As Integer,
                                         ByVal IdEmpresa As Integer,
                                         ByVal IdOperador As Integer,
                                         ByVal NomOperador As String,
                                         ByVal DobleVerificacion As Boolean,
                                         ByRef prg As ProgressBar,
                                         ByVal pEliminarTeorico As Boolean,
                                         ByVal pExisteInventarioTeorico As Boolean)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim Contador As Integer = 0
        Dim InvDetalle As New clsBeTrans_inv_detalle
        Dim InvResumen As New clsBeTrans_inv_resumen
        Dim BeTramoInv As New clsBeTrans_inv_tramo
        Dim lTramosInv As New List(Of clsBeTrans_inv_tramo)
        Dim MaxIdDet As Integer = 0
        Dim BeUbicacion As New clsBeBodega_ubicacion
        Dim BeUbicacionRecepcionDefectoPorBodega As New clsBeBodega_ubicacion
        Dim IdUbicacionRecepcion As Integer
        Dim IdProductoEstado As Integer
        Dim vCantidadUMBas As Double = 0
        Dim vFactor As Double = 0
        Dim vIdInventarioEnc As Integer = 0

        Try

            If Not pListInvStockPrd Is Nothing Then
                If pListInvStockPrd.Count > 0 Then
                    vIdInventarioEnc = pListInvStockPrd(0).Idinventario
                End If
            End If

        Catch ex As Exception
            Throw New Exception("#EJC20211117: No se pudo obtener el número de inventario de la lista")
        End Try

        Dim vIndiceTramoInvExistente As Integer = -1

        Dim lUbicaciones As New List(Of clsBeBodega_ubicacion)
        Dim BeUbicacionLista As New clsBeBodega_ubicacion

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            If pExisteInventarioTeorico Then
                If pEliminarTeorico Then
                    Eliminar(pListInvStockPrd(0).Idinventario,
                             pListInvStockPrd(0).TipoTeoricoImportacion,
                             lConnection,
                             lTransaction)
                    pExisteInventarioTeorico = False
                End If
            End If

            'EFREN16112021: Esta validación queda inicial, porque aunque no se guarde como inv. inicial, se debe setear la ubicacion y bodega en stock_prod
            IdUbicacionRecepcion = clsLnBodega.Get_IdUbicacion_Recepcion_By_IdBodega(IdBodega,
                                                                                     lConnection,
                                                                                     lTransaction)

            BeUbicacionRecepcionDefectoPorBodega = clsLnBodega_ubicacion.Get_Ubicacion_Recepcion(IdUbicacionRecepcion,
                                                                                                 IdBodega,
                                                                                                 lConnection,
                                                                                                 lTransaction)

            prg.Maximum = pListInvStockPrd.Count
            prg.Visible = True

            lUbicaciones = New List(Of clsBeBodega_ubicacion)
            BeUbicacionLista = New clsBeBodega_ubicacion

            Dim vMaxIdInvStockProd As Integer = 1

            If Not pListInvStockPrd Is Nothing Then
                If pListInvStockPrd.Count > 0 Then
                    vMaxIdInvStockProd = MaxID(vIdInventarioEnc,
                                           lConnection,
                                           lTransaction) + 1
                End If
            End If

            If Not pExisteInventarioTeorico Then

                'EFREN16112021: Se guarda el stock_prod, y el obj IdTramo queda seteado, si es que se llegara a insertar como inv. inicial
                For Each BeTransInvStockProd As clsBeTrans_inv_stock_prod In pListInvStockPrd

                    BeTramoInv = New clsBeTrans_inv_tramo()
                    BeTramoInv.Idinventario = vIdInventarioEnc
                    BeTramoInv.Det_estado = "Nuevo"
                    BeTramoInv.Det_idoperador = IdOperador
                    BeTramoInv.IdBodega = IdBodega

                    BeTransInvStockProd.Idinvstockprod = vMaxIdInvStockProd

                    If BeTransInvStockProd.IdUbicacion > 0 Then

                        BeUbicacionLista = lUbicaciones.Find(Function(x) x.IdUbicacion = BeTransInvStockProd.IdUbicacion)

                        If BeUbicacionLista Is Nothing Then

                            BeUbicacion = New clsBeBodega_ubicacion()
                            BeUbicacion = clsLnBodega_ubicacion.Get_Single_By_IdUbicacion_And_IdBodega(BeTransInvStockProd.IdUbicacion,
                                                                                                       IdBodega,
                                                                                                       lConnection,
                                                                                                       lTransaction)
                            lUbicaciones.Add(BeUbicacion)

                            BeTramoInv.Idtramo = BeUbicacion.IdTramo

                            vIndiceTramoInvExistente = lTramosInv.FindIndex(Function(x) x.IdBodega = IdBodega _
                                                                            AndAlso x.Idtramo = BeTramoInv.Idtramo _
                                                                            AndAlso x.Idinventario = BeTramoInv.Idinventario)

                            If vIndiceTramoInvExistente = -1 Then
                                lTramosInv.Add(BeTramoInv)
                            End If

                        End If

                    Else

                        BeTramoInv.Idtramo = BeUbicacionRecepcionDefectoPorBodega.IdTramo

                        vIndiceTramoInvExistente = lTramosInv.FindIndex(Function(x) x.IdBodega = IdBodega _
                                                                    AndAlso x.Idtramo = BeTramoInv.Idtramo _
                                                                    AndAlso x.Idinventario = BeTramoInv.Idinventario)

                        If vIndiceTramoInvExistente = -1 Then
                            lTramosInv.Add(BeTramoInv)
                        End If

                        BeTransInvStockProd.IdUbicacion = BeUbicacionRecepcionDefectoPorBodega.IdUbicacion

                    End If

                    Insertar(BeTransInvStockProd, lConnection, lTransaction)

                    prg.Value = Contador

                    Contador += 1 : vMaxIdInvStockProd += 1

                Next

            End If

            If InsertaInv Then

                MaxIdDet = clsLnTrans_inv_resumen.MaxID(lConnection, lTransaction)

                IdProductoEstado = clsLnI_nav_config_enc.Get_IdProductoEstado_By_IdBodega_And_IdEmpresa(IdBodega,
                                                                                                        IdEmpresa,
                                                                                                        lConnection,
                                                                                                        lTransaction)

                If IdProductoEstado = 0 Then
                    Throw New Exception("ERR_20220510_0929: No está definido el estado de producto en la configuración de la interfase.")
                End If

                lUbicaciones = New List(Of clsBeBodega_ubicacion)
                BeUbicacionLista = New clsBeBodega_ubicacion

                For Each BeTransInvStockProd As clsBeTrans_inv_stock_prod In pListInvStockPrd

                    BeTramoInv = New clsBeTrans_inv_tramo()
                    BeTramoInv.Idinventario = vIdInventarioEnc
                    BeTramoInv.Det_estado = "En Proceso"
                    BeTramoInv.Det_idoperador = IdOperador
                    BeTramoInv.IdBodega = IdBodega

                    InvDetalle = New clsBeTrans_inv_detalle
                    InvDetalle.Idinventariodet = MaxIdDet
                    InvDetalle.Idinventarioenc = BeTransInvStockProd.Idinventario
                    InvDetalle.IdBodega = IdBodega

                    'EFREN10112021: Si el registro tiene idubicación se sobreescribe, la ubicación por defecto no se registrará.
                    If BeTransInvStockProd.IdUbicacion > 0 Then

                        BeUbicacionLista = lUbicaciones.Find(Function(x) x.IdUbicacion = BeTransInvStockProd.IdUbicacion)

                        If BeUbicacionLista Is Nothing Then

                            BeUbicacion = New clsBeBodega_ubicacion()
                            BeUbicacion = clsLnBodega_ubicacion.Get_Single_By_IdUbicacion_And_IdBodega(BeTransInvStockProd.IdUbicacion,
                                                                                                       IdBodega,
                                                                                                       lConnection,
                                                                                                       lTransaction)
                            InvDetalle.IdUbicacion = BeTransInvStockProd.IdUbicacion
                            InvDetalle.Idtramo = BeUbicacion.IdTramo
                            lUbicaciones.Add(BeUbicacion)

                            BeTramoInv.Idtramo = BeUbicacion.IdTramo

                            If DobleVerificacion Then
                                BeTramoInv.Res_estado = "En Proceso"
                                BeTramoInv.Res_idoperador = IdOperador
                            End If

                            vIndiceTramoInvExistente = lTramosInv.FindIndex(Function(x) x.IdBodega = IdBodega _
                                                                            AndAlso x.Idtramo = BeTramoInv.Idtramo _
                                                                            AndAlso x.Idinventario = BeTramoInv.Idinventario)

                            If vIndiceTramoInvExistente = -1 Then
                                lTramosInv.Add(BeTramoInv)
                            End If

                        Else
                            BeTramoInv.Idtramo = BeUbicacionLista.IdTramo
                            InvDetalle.IdUbicacion = BeTransInvStockProd.IdUbicacion
                            InvDetalle.Idtramo = BeUbicacionLista.IdTramo
                        End If

                    Else
                        'BeTramoInv.Idtramo = BeUbicacionRecepcionDefectoPorBodega.IdTramo
                        InvDetalle.Idtramo = BeUbicacionRecepcionDefectoPorBodega.IdTramo
                        InvDetalle.IdUbicacion = BeUbicacionRecepcionDefectoPorBodega.IdUbicacion
                    End If

                    'EFREN17112021 valores de un inv multipropietario
                    Dim pBeProducto As New clsBeProducto()
                    Dim pCampos(4) As clsBeProducto.ProdPropiedades
                    pCampos(0) = clsBeProducto.ProdPropiedades.Control_lote
                    pCampos(1) = clsBeProducto.ProdPropiedades.Control_vencimiento
                    pCampos(2) = clsBeProducto.ProdPropiedades.Codigo
                    pCampos(3) = clsBeProducto.ProdPropiedades.Propietario

                    'EFREN10052021 se utiliza un metodo sobrecargado, el método original no devuelve todas las propiedades de Producto
                    pBeProducto = clsLnProducto.Get_Single_By_Codigo(BeTransInvStockProd.Codigo,
                                                                     pCampos,
                                                                     lConnection,
                                                                     lTransaction)

                    Dim vIdPropietarioBodega = clsLnPropietario_bodega.Get_IdPropietarioBodega_By_IdPropietario_And_IdBodega(pBeProducto.Propietario.IdPropietario,
                                                                                                                             BeTransInvStockProd.IdBodega)

                    InvDetalle.Idoperador = IdOperador
                    InvDetalle.Idproducto = BeTransInvStockProd.IdProducto
                    InvDetalle.IdPresentacion = BeTransInvStockProd.IdPresentacion
                    InvDetalle.Idunidadmedida = BeTransInvStockProd.IdUnidadMedida
                    InvDetalle.Lote = BeTransInvStockProd.Lote
                    InvDetalle.Fecha_vence = BeTransInvStockProd.Fecha_vence
                    InvDetalle.Serie = ""
                    InvDetalle.Idproductoestado = IdProductoEstado
                    InvDetalle.Cantidad = BeTransInvStockProd.Cant
                    InvDetalle.Fecha_captura = Date.Now.Date
                    InvDetalle.Host = "IMP"
                    InvDetalle.Nom_producto = clsLnProducto.Get_Nombre_By_IdProducto(BeTransInvStockProd.IdProducto,
                                                                                     lConnection,
                                                                                     lTransaction)
                    InvDetalle.Nom_operador = NomOperador
                    InvDetalle.Carga = 0
                    InvDetalle.Peso = BeTransInvStockProd.Peso
                    'EFREN10052021 se agregan los campos que identifican al inv multiempresa
                    InvDetalle.IdPropietarioBodega = vIdPropietarioBodega
                    InvDetalle.nombre_propietario = pBeProducto.Propietario.Nombre_comercial
                    'GT02122021 Se agregan los valores de la LP y cod_variante
                    InvDetalle.License_plate = BeTransInvStockProd.License_plate
                    InvDetalle.Codigo_variante = BeTransInvStockProd.Codigo_variante
                    '#GT25112022_1120: campos DyD
                    InvDetalle.costo = BeTransInvStockProd.Costo
                    InvDetalle.precio = BeTransInvStockProd.Precio
                    InvDetalle.IdProductoParametroA = IIf(BeTransInvStockProd.Parametro_a = "", 0, BeTransInvStockProd.Parametro_a)
                    InvDetalle.IdProductoParametroB = IIf(BeTransInvStockProd.Parametro_b = "", 0, BeTransInvStockProd.Parametro_b)
                    InvDetalle.IdProductoTallaColor = BeTransInvStockProd.IdProductoTallaColor

                    clsLnTrans_inv_detalle.InsertarSinID(InvDetalle,
                                                         lConnection,
                                                         lTransaction)

                    '#CKFK20220506 Si se inserta el inventario inicial tambien se debe insertar la verificacion
                    'If DobleVerificacion Then

                    BeTramoInv.Res_estado = "En Proceso"
                    BeTramoInv.Res_idoperador = IdOperador

                    InvResumen = New clsBeTrans_inv_resumen
                    InvResumen.Idinventariores = MaxIdDet
                    InvResumen.Idinventarioenct = BeTransInvStockProd.Idinventario
                    InvResumen.Idtramo = InvDetalle.Idtramo
                    InvResumen.Idoperador = IdOperador
                    InvResumen.Idproducto = BeTransInvStockProd.IdProducto
                    InvResumen.Idpresentacion = BeTransInvStockProd.IdPresentacion
                    InvResumen.IdUnidadMedida = BeTransInvStockProd.IdUnidadMedida
                    InvResumen.Idproductoestado = IdProductoEstado
                    InvResumen.Cantidad = BeTransInvStockProd.Cant
                    InvResumen.Fecha_captura = Now
                    InvResumen.Host = "IMP"
                    InvResumen.Nom_producto = InvDetalle.Nom_producto
                    InvResumen.Nom_operador = NomOperador
                    InvResumen.IdUbicacion = InvDetalle.IdUbicacion
                    InvResumen.Lic_plate = BeTransInvStockProd.License_plate
                    InvResumen.IdBodega = InvDetalle.IdBodega
                    InvResumen.IdProductoTallaColor = BeTransInvStockProd.IdProductoTallaColor

                    clsLnTrans_inv_resumen.Insertar(InvResumen,
                                                    lConnection,
                                                    lTransaction)

                    ' End If

                    MaxIdDet = MaxIdDet + 1

                    vIndiceTramoInvExistente = lTramosInv.FindIndex(Function(x) x.IdBodega = IdBodega _
                                                                    AndAlso x.Idtramo = BeTramoInv.Idtramo _
                                                                    AndAlso x.Idinventario = BeTramoInv.Idinventario)

                    If vIndiceTramoInvExistente = -1 Then
                        lTramosInv.Add(BeTramoInv)
                    End If

                Next

            End If

            For Each BeTramoInvInLista In lTramosInv

                '#EJC20191206: Aquí solo se llamaba al actualizar, porque no al insertar ?
                'Cuando no existía el tramo, no se insertaba y eso provocaba que no se mostrara el inv.
                'Tzirin ?
                If Not clsLnTrans_inv_tramo.Get_Single_By_BeTramo(BeTramoInvInLista,
                                                                  lConnection,
                                                                  lTransaction) Then

                    clsLnTrans_inv_tramo.Insertar(BeTramoInvInLista,
                                                  lConnection,
                                                  lTransaction)

                Else
                    clsLnTrans_inv_tramo.Actualizar_Tramo(BeTramoInvInLista,
                                                          lConnection,
                                                          lTransaction)
                End If

            Next

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Sub

    Public Shared Function Eliminar(ByVal pIdInventario As Integer,
                                    ByVal pTipoInventarioTeorico As Integer,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_inv_stock_prod  Where(idinventario = @idinventario AND tipoteoricoimportacion = @tipoteoricoimportacion)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIO", pIdInventario))
            cmd.Parameters.Add(New SqlParameter("@tipoteoricoimportacion", pTipoInventarioTeorico))

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

    Public Shared Function GetAll(idinventario As Integer) As List(Of clsBeTrans_inv_stock_prod)

        Dim lReturnList As List(Of clsBeTrans_inv_stock_prod)

        Try

            lReturnList = GetAll().FindAll(Function(x) x.Idinventario = idinventario).ToList()

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll(ByVal IdInventario As Integer,
                                  ByVal IdProducto As Integer) As List(Of clsBeTrans_inv_stock_prod)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        GetAll = Nothing

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_stock_prod)

            Const sp As String = "SELECT * FROM Trans_inv_stock_prod 
                                  WHERE IdInventario =@IdInventario
                                  AND IdProducto = @IdProducto "

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.SelectCommand.Parameters.AddWithValue("@IdInventario", IdInventario)
            dad.SelectCommand.Parameters.AddWithValue("@IdProducto", IdProducto)
            dad.Fill(dt)

            Dim vBeTrans_inv_stock_prod As New clsBeTrans_inv_stock_prod

            For Each dr As DataRow In dt.Rows
                vBeTrans_inv_stock_prod = New clsBeTrans_inv_stock_prod
                Cargar(vBeTrans_inv_stock_prod, dr)
                lReturnList.Add(vBeTrans_inv_stock_prod)
            Next

            lTransaction.Commit()

            Return lReturnList

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    ''' <summary>
    ''' ·EJC20220502: Obtener el inv. teòrico a partir de una licencia o còdigo.
    ''' </summary>
    ''' <param name="pIdInventario"></param>
    ''' <param name="pCodigo"></param>
    ''' <returns></returns>
    Public Shared Function Get_All_By_Codigo_O_Licencia(ByVal pIdInventario As Integer,
                                                        ByVal pCodigo As String,
                                                        ByVal pIdBodega As Integer) As List(Of clsBeTrans_inv_stock_prod)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Get_All_By_Codigo_O_Licencia = Nothing

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_stock_prod)

            'AT20220504 Se agregó un join para poder obtener IdPropietarioBodega
            Const sp As String = "SELECT a.*, b.IdPropietarioBodega FROM Trans_inv_stock_prod  AS a
                                  INNER JOIN propietario_bodega AS b ON b.IdBodega = a.idbodega
                                  INNER JOIN producto p ON p.IdProducto = a.IdProducto 
								  LEFT JOIN producto_codigos_barra pb ON pb.IdProducto = p.IdProducto and a.idProducto = pb.IdProducto
                                  WHERE a.IdInventario = @IdInventario
                                  AND (a.Codigo= @Codigo OR a.Lic_Plate = @Lic_Plate OR p.codigo_barra = @codigo_barra OR
                                      pb.codigo_barra = @Codigo) AND a.IdBodega = @IdBodega"

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.SelectCommand.Parameters.AddWithValue("@IdInventario", pIdInventario)
            dad.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)
            dad.SelectCommand.Parameters.AddWithValue("@Lic_Plate", pCodigo)
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
            dad.SelectCommand.Parameters.AddWithValue("@codigo_barra", pCodigo)

            dad.Fill(dt)

            Dim vBeTrans_inv_stock_prod As New clsBeTrans_inv_stock_prod

            For Each dr As DataRow In dt.Rows
                vBeTrans_inv_stock_prod = New clsBeTrans_inv_stock_prod
                Cargar(vBeTrans_inv_stock_prod, dr)
                vBeTrans_inv_stock_prod.BeProducto = clsLnProducto.Get_Single_By_IdProducto(vBeTrans_inv_stock_prod.IdProducto, lConnection, lTransaction)
                lReturnList.Add(vBeTrans_inv_stock_prod)
            Next

            lTransaction.Commit()

            Return lReturnList

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_All_Codigos_By_IdInventario_And_IdBodega(ByVal pIdInventario As Integer,
                                                                        ByVal pIdBodega As Integer) As List(Of clsBeTrans_inv_stock_prod_sug)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Get_All_Codigos_By_IdInventario_And_IdBodega = Nothing

        Dim lReturnList As New List(Of clsBeTrans_inv_stock_prod_sug)

        Try

            Const sp As String = "select distinct Codigo from Trans_inv_stock_prod 
                                  WHERE IdInventario = @IdInventario 
                                  AND IdBodega = @IdBodega "

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.SelectCommand.Parameters.AddWithValue("@IdInventario", pIdInventario)
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

            dad.Fill(dt)

            Dim vCodigo As String = ""
            Dim BeCodigo As New clsBeTrans_inv_stock_prod_sug()

            For Each dr As DataRow In dt.Rows
                BeCodigo = New clsBeTrans_inv_stock_prod_sug()
                vCodigo = IIf(IsDBNull(dr.Item("Codigo")), "", dr.Item("Codigo"))
                BeCodigo.Codigo = vCodigo
                lReturnList.Add(BeCodigo)
            Next

            lTransaction.Commit()

            Return lReturnList

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Exist(ByVal IdInventario As Integer,
                                 ByRef lConnection As SqlConnection,
                                 ByRef lTransaction As SqlTransaction) As Integer

        Try

            Dim lInv As Integer = 0

            Const sp As String = "SELECT idinvstockprod
                                  FROM Trans_inv_stock_prod WHERE IdInventario =@IdInventario  "

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                lCommand.Parameters.AddWithValue("@IdInventario", IdInventario)
                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lInv = CInt(lReturnValue)
                End If
            End Using

            Return lInv <> 0

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Exist(ByVal IdInventario As Integer,
                                 ByVal TipoInventarioTeorico As Integer) As Integer

        Try

            Dim lInv As Integer = 0

            Const sp As String = "SELECT idinvstockprod
                                  FROM Trans_inv_stock_prod WHERE IdInventario =@IdInventario  AND tipoteoricoimportacion = @tipoteoricoimportacion "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
                Using lCommand As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
                    lCommand.Parameters.AddWithValue("@IdInventario", IdInventario)
                    lCommand.Parameters.AddWithValue("@tipoteoricoimportacion", TipoInventarioTeorico)
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lInv = CInt(lReturnValue)
                    End If
                End Using
            End Using

            Return lInv <> 0

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    'AT2024052 Obtiene inventario por licencia, bodega y ubicacion
    Public Shared Function Get_Inventario_By_Licencia(Licencia As String,
                                                      Bodega As Integer,
                                                      Ubicacion As Integer) As clsBeTrans_inv_stock_prod

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransactiona As SqlTransaction = Nothing

        Dim item As New clsBeTrans_inv_stock_prod

        Get_Inventario_By_Licencia = Nothing

        Try

            lConnection.Open() : lTransactiona = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM Trans_inv_stock_prod Where (lic_plate = @Licencia) AND (idbodega = @Bodega)"
            Dim cmd As New SqlCommand(sp, lConnection, lTransactiona) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.CommandType = CommandType.Text
            dad.SelectCommand.Transaction = lTransactiona
            dad.SelectCommand.Parameters.AddWithValue("@Licencia", Licencia)
            dad.SelectCommand.Parameters.AddWithValue("@Bodega", Bodega)
            dad.SelectCommand.Parameters.AddWithValue("@Ubicacion", Ubicacion)

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                Dim lRow As DataRow = dt.Rows(0)

                Cargar(item, lRow)

                Get_Inventario_By_Licencia = item

            End If

            lTransactiona.Commit()

            Return Get_Inventario_By_Licencia

        Catch ex As Exception
            If Not lTransactiona Is Nothing Then lTransactiona.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function


    Public Shared Function Get_All_By_IdInventarioEnc(IdinventarioEnc As Integer, lConnection As SqlConnection, lTransaction As SqlTransaction) As List(Of clsBeTrans_inv_stock_prod)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_stock_prod)
            Const sp As String = "SELECT * FROM Trans_inv_stock_prod WHERE IdInventario = @IdInventario "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
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
    '#MA20250108
    Public Shared Function SiguienteId(ByVal idInventario As Integer) As Integer
        Dim sql As String = "SELECT ISNULL(MAX(idinvstockprod),0)+1 
                             FROM trans_inv_stock_prod 
                             WHERE idinventario = @idinventario"

        Using cn As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Using cmd As New SqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@idinventario", idInventario)
                cn.Open()
                Return CInt(cmd.ExecuteScalar())
            End Using
        End Using
    End Function


End Class
