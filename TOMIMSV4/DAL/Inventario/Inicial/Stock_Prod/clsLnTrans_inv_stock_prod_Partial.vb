Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnTrans_inv_stock_prod

    Private Const TAG_INV_IMPORT_TRACE As String = "#EJC20260522_INV_IMPORT_TRACE"

    Private Shared Function InvImportTrace_Activo() As Boolean
        Try
            Dim vValor As String = If(System.Environment.GetEnvironmentVariable("TOMWMS_INV_IMPORT_TRACE"), "").ToUpperInvariant()
            Return Not (vValor = "0" OrElse vValor = "NO" OrElse vValor = "FALSE")
        Catch
            Return True
        End Try
    End Function

    Private Shared Function InvImportTrace_Path() As String
        Return System.IO.Path.Combine(System.IO.Path.GetTempPath(), "TOMWMS", "inventario-import-trace.log")
    End Function

    Private Shared Function InvImportTrace_Limpiar(ByVal pTexto As String) As String
        If pTexto Is Nothing Then Return ""
        Return pTexto.Replace(vbCr, " ").Replace(vbLf, " ").Replace("|", "/")
    End Function

    Private Shared Function InvImportTrace_Iniciar(ByVal pIdInventario As Integer,
                                                   ByVal pCantidad As Integer,
                                                   ByVal pExtra As String) As String
        Dim vSesion As String = Date.Now.ToString("yyyyMMddHHmmssfff") & "-" & Guid.NewGuid().ToString("N").Substring(0, 8)
        InvImportTrace_Escribir(vSesion, "DAL_IMPORTAR_PRODUCTOS_START", pIdInventario, pCantidad, 0, 0, pExtra)
        Return vSesion
    End Function

    Private Shared Sub InvImportTrace_Marca(ByVal pSesion As String,
                                            ByVal pTotal As System.Diagnostics.Stopwatch,
                                            ByVal pPaso As System.Diagnostics.Stopwatch,
                                            ByVal pPasoNombre As String,
                                            ByVal pIdInventario As Integer,
                                            ByVal pCantidad As Integer,
                                            Optional ByVal pExtra As String = "")
        If Not InvImportTrace_Activo() Then Return
        Dim vTotalMs As Long = If(pTotal Is Nothing, 0, pTotal.ElapsedMilliseconds)
        Dim vDeltaMs As Long = If(pPaso Is Nothing, 0, pPaso.ElapsedMilliseconds)
        If pPaso IsNot Nothing Then pPaso.Restart()
        InvImportTrace_Escribir(pSesion, pPasoNombre, pIdInventario, pCantidad, vTotalMs, vDeltaMs, pExtra)
    End Sub

    Private Shared Sub InvImportTrace_Escribir(ByVal pSesion As String,
                                               ByVal pPaso As String,
                                               ByVal pIdInventario As Integer,
                                               ByVal pCantidad As Integer,
                                               ByVal pTotalMs As Long,
                                               ByVal pDeltaMs As Long,
                                               Optional ByVal pExtra As String = "")
        If Not InvImportTrace_Activo() Then Return

        Try
            Dim vPath As String = InvImportTrace_Path()
            Dim vDir As String = System.IO.Path.GetDirectoryName(vPath)
            If Not System.IO.Directory.Exists(vDir) Then System.IO.Directory.CreateDirectory(vDir)

            If System.IO.File.Exists(vPath) AndAlso New System.IO.FileInfo(vPath).Length > 5242880 Then
                System.IO.File.Delete(vPath)
            End If

            Dim vLinea As String = String.Join("|", New String() {
                Date.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                TAG_INV_IMPORT_TRACE,
                "clsLnTrans_inv_stock_prod",
                InvImportTrace_Limpiar(pSesion),
                InvImportTrace_Limpiar(pPaso),
                "IdInventario=" & pIdInventario,
                "Cantidad=" & pCantidad,
                "TotalMs=" & pTotalMs,
                "DeltaMs=" & pDeltaMs,
                InvImportTrace_Limpiar(pExtra)
            })

            System.IO.File.AppendAllText(vPath, vLinea & Environment.NewLine, System.Text.Encoding.UTF8)
        Catch
        End Try
    End Sub

    Private Shared Function CrearBatchStockProd() As clsInsertBatch
        Dim vBatch As New clsInsertBatch()
        vBatch.Init("trans_inv_stock_prod")
        vBatch.AddColumn("idinventario", GetType(Integer))
        vBatch.AddColumn("idinvstockprod", GetType(Integer))
        vBatch.AddColumn("idproducto", GetType(Integer))
        vBatch.AddColumn("idpresentacion", GetType(Integer))
        vBatch.AddColumn("idunidadmedida", GetType(Integer))
        vBatch.AddColumn("cant", GetType(Double))
        vBatch.AddColumn("peso", GetType(Double))
        vBatch.AddColumn("lote", GetType(String))
        vBatch.AddColumn("fecha_vence", GetType(DateTime))
        vBatch.AddColumn("codigo", GetType(String))
        vBatch.AddColumn("idbodega", GetType(Integer))
        vBatch.AddColumn("idubicacion", GetType(Integer))
        vBatch.AddColumn("Lic_plate", GetType(String))
        vBatch.AddColumn("codigo_area", GetType(String))
        vBatch.AddColumn("codigo_talla", GetType(String))
        vBatch.AddColumn("codigo_color", GetType(String))
        vBatch.AddColumn("idproductotallacolor", GetType(Integer))
        vBatch.AddColumn("TipoTeoricoImportacion", GetType(Integer))
        Return vBatch
    End Function

    Private Shared Sub AgregarBatchStockProd(ByVal pBatch As clsInsertBatch,
                                             ByVal pObj As clsBeTrans_inv_stock_prod)
        pBatch.BeginRow()
        pBatch.Add("idinventario", pObj.Idinventario)
        pBatch.Add("idinvstockprod", pObj.Idinvstockprod)
        pBatch.Add("idproducto", pObj.IdProducto)
        pBatch.Add("idpresentacion", pObj.IdPresentacion)
        pBatch.Add("idunidadmedida", pObj.IdUnidadMedida)
        pBatch.Add("cant", pObj.Cant)
        pBatch.Add("peso", pObj.Peso)
        pBatch.Add("lote", pObj.Lote)
        pBatch.Add("fecha_vence", pObj.Fecha_vence)
        pBatch.Add("codigo", pObj.Codigo)
        pBatch.Add("idbodega", pObj.IdBodega)
        pBatch.Add("idubicacion", pObj.IdUbicacion)
        pBatch.Add("Lic_plate", pObj.License_plate)
        pBatch.Add("codigo_area", pObj.Codigo_Area)
        pBatch.Add("codigo_talla", pObj.Codigo_Talla)
        pBatch.Add("codigo_color", pObj.Codigo_Color)
        pBatch.Add("idproductotallacolor", pObj.IdProductoTallaColor)
        pBatch.Add("TipoTeoricoImportacion", pObj.TipoTeoricoImportacion)
        pBatch.EndRow()
    End Sub

    Private Shared Function CrearBatchDetalle() As clsInsertBatch
        Dim vBatch As New clsInsertBatch()
        vBatch.Init("trans_inv_detalle")
        vBatch.AddColumn("idinventarioenc", GetType(Integer))
        vBatch.AddColumn("idtramo", GetType(Integer))
        vBatch.AddColumn("idubicacion", GetType(Integer))
        vBatch.AddColumn("idoperador", GetType(Integer))
        vBatch.AddColumn("idproducto", GetType(Integer))
        vBatch.AddColumn("idpresentacion", GetType(Integer))
        vBatch.AddColumn("idunidadmedida", GetType(Integer))
        vBatch.AddColumn("lote", GetType(String))
        vBatch.AddColumn("fecha_vence", GetType(DateTime))
        vBatch.AddColumn("serie", GetType(String))
        vBatch.AddColumn("idproductoestado", GetType(Integer))
        vBatch.AddColumn("cantidad", GetType(Double))
        vBatch.AddColumn("fecha_captura", GetType(DateTime))
        vBatch.AddColumn("host", GetType(String))
        vBatch.AddColumn("nom_producto", GetType(String))
        vBatch.AddColumn("nom_operador", GetType(String))
        vBatch.AddColumn("carga", GetType(Integer))
        vBatch.AddColumn("peso", GetType(Double))
        vBatch.AddColumn("IdPropietarioBodega", GetType(Integer))
        vBatch.AddColumn("nombre_propietario", GetType(String))
        vBatch.AddColumn("lic_plate", GetType(String))
        vBatch.AddColumn("cod_variante", GetType(String))
        vBatch.AddColumn("idbodega", GetType(Integer))
        vBatch.AddColumn("costo", GetType(Double))
        vBatch.AddColumn("precio", GetType(Double))
        vBatch.AddColumn("IdProductoParametroA", GetType(Integer))
        vBatch.AddColumn("IdProductoParametroB", GetType(Integer))
        vBatch.AddColumn("IdProductoTallaColor", GetType(Integer))
        Return vBatch
    End Function

    Private Shared Sub AgregarBatchDetalle(ByVal pBatch As clsInsertBatch,
                                           ByVal pObj As clsBeTrans_inv_detalle)
        pBatch.BeginRow()
        pBatch.Add("idinventarioenc", pObj.Idinventarioenc)
        pBatch.Add("idtramo", pObj.Idtramo)
        pBatch.Add("idubicacion", pObj.IdUbicacion)
        pBatch.Add("idoperador", pObj.Idoperador)
        pBatch.Add("idproducto", pObj.Idproducto)
        pBatch.Add("idpresentacion", pObj.IdPresentacion)
        pBatch.Add("idunidadmedida", pObj.Idunidadmedida)
        pBatch.Add("lote", pObj.Lote)
        pBatch.Add("fecha_vence", pObj.Fecha_vence)
        pBatch.Add("serie", pObj.Serie)
        pBatch.Add("idproductoestado", pObj.Idproductoestado)
        pBatch.Add("cantidad", pObj.Cantidad)
        pBatch.Add("fecha_captura", pObj.Fecha_captura)
        pBatch.Add("host", pObj.Host)
        pBatch.Add("nom_producto", pObj.Nom_producto)
        pBatch.Add("nom_operador", pObj.Nom_operador)
        pBatch.Add("carga", pObj.Carga)
        pBatch.Add("peso", pObj.Peso)
        pBatch.Add("IdPropietarioBodega", pObj.IdPropietarioBodega)
        pBatch.Add("nombre_propietario", pObj.nombre_propietario)
        pBatch.Add("lic_plate", pObj.License_plate)
        pBatch.Add("cod_variante", pObj.Codigo_variante)
        pBatch.Add("idbodega", pObj.IdBodega)
        pBatch.Add("costo", pObj.costo)
        pBatch.Add("precio", pObj.precio)
        pBatch.Add("IdProductoParametroA", pObj.IdProductoParametroA)
        pBatch.Add("IdProductoParametroB", pObj.IdProductoParametroB)
        pBatch.Add("IdProductoTallaColor", pObj.IdProductoTallaColor)
        pBatch.EndRow()
    End Sub

    Private Shared Function CrearBatchResumen() As clsInsertBatch
        Dim vBatch As New clsInsertBatch()
        vBatch.Init("trans_inv_resumen")
        vBatch.AddColumn("idinventariores", GetType(Integer))
        vBatch.AddColumn("idinventarioenct", GetType(Integer))
        vBatch.AddColumn("idtramo", GetType(Integer))
        vBatch.AddColumn("idproducto", GetType(Integer))
        vBatch.AddColumn("idoperador", GetType(Integer))
        vBatch.AddColumn("idunidadmedida", GetType(Integer))
        vBatch.AddColumn("idpresentacion", GetType(Integer))
        vBatch.AddColumn("idproductoestado", GetType(Integer))
        vBatch.AddColumn("cantidad", GetType(Double))
        vBatch.AddColumn("fecha_captura", GetType(DateTime))
        vBatch.AddColumn("host", GetType(String))
        vBatch.AddColumn("nom_producto", GetType(String))
        vBatch.AddColumn("nom_operador", GetType(String))
        vBatch.AddColumn("idubicacion", GetType(Integer))
        vBatch.AddColumn("idbodega", GetType(Integer))
        vBatch.AddColumn("lic_plate", GetType(String))
        vBatch.AddColumn("idproductotallacolor", GetType(Integer))
        Return vBatch
    End Function

    Private Shared Sub AgregarBatchResumen(ByVal pBatch As clsInsertBatch,
                                           ByVal pObj As clsBeTrans_inv_resumen)
        pBatch.BeginRow()
        pBatch.Add("idinventariores", pObj.Idinventariores)
        pBatch.Add("idinventarioenct", pObj.Idinventarioenct)
        pBatch.Add("idtramo", pObj.Idtramo)
        pBatch.Add("idproducto", pObj.Idproducto)
        pBatch.Add("idoperador", pObj.Idoperador)
        pBatch.Add("idunidadmedida", pObj.IdUnidadMedida)
        pBatch.Add("idpresentacion", pObj.Idpresentacion)
        pBatch.Add("idproductoestado", pObj.Idproductoestado)
        pBatch.Add("cantidad", pObj.Cantidad)
        pBatch.Add("fecha_captura", pObj.Fecha_captura)
        pBatch.Add("host", pObj.Host)
        pBatch.Add("nom_producto", pObj.Nom_producto)
        pBatch.Add("nom_operador", pObj.Nom_operador)
        pBatch.Add("idubicacion", pObj.IdUbicacion)
        pBatch.Add("idbodega", pObj.IdBodega)
        pBatch.Add("lic_plate", pObj.Lic_plate)
        pBatch.Add("idproductotallacolor", pObj.IdProductoTallaColor)
        pBatch.EndRow()
    End Sub

    Public Shared Sub Importar_Productos(ByRef pListInvStockPrd As List(Of clsBeTrans_inv_stock_prod), ByVal InsertaInv As Boolean,
                                         ByVal IdBodega As Integer,
                                         ByVal IdEmpresa As Integer,
                                         ByVal IdOperador As Integer,
                                         ByVal NomOperador As String,
                                         ByVal DobleVerificacion As Boolean,
                                         ByRef prg As ProgressBar,
                                         ByVal pEliminarTeorico As Boolean,
                                         ByVal pExisteInventarioTeorico As Boolean,
                                         Optional ByVal pCancelar As Func(Of Boolean) = Nothing,
                                         Optional ByVal pProgreso As Action(Of Integer, Integer, String) = Nothing)

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
        Dim vTraceSesion As String = ""
        Dim vTraceTotal As System.Diagnostics.Stopwatch = System.Diagnostics.Stopwatch.StartNew()
        Dim vTracePaso As System.Diagnostics.Stopwatch = System.Diagnostics.Stopwatch.StartNew()
        Dim vTraceReloj As System.Diagnostics.Stopwatch
        Dim vTraceMsUbicStock As Long = 0
        Dim vTraceMsInsertStock As Long = 0
        Dim vTraceMsUbicDetalle As Long = 0
        Dim vTraceMsProductoDetalle As Long = 0
        Dim vTraceMsProductoCacheBulk As Long = 0
        Dim vTraceMsPropietarioBodega As Long = 0
        Dim vTraceMsNombreProducto As Long = 0
        Dim vTraceMsInsertDetalle As Long = 0
        Dim vTraceMsInsertResumen As Long = 0
        Dim vTraceMsTramos As Long = 0
        '#EJC20260522_INV_IMPORT_PRODUCTO_LITE: caches de lectura para no repetir producto/propietario por cada linea importada.
        Dim vProductosPorCodigo As New System.Collections.Generic.Dictionary(Of String, clsBeProducto)(System.StringComparer.OrdinalIgnoreCase)
        Dim vPropietarioBodegaPorClave As New System.Collections.Generic.Dictionary(Of String, Integer)(System.StringComparer.OrdinalIgnoreCase)
        '#EJC20260522_INV_APLICAR_TEORICO_CACHE: cache unico de ubicaciones para stock y detalle; evita roundtrips duplicados.
        Dim vUbicacionesPorId As New System.Collections.Generic.Dictionary(Of Integer, clsBeBodega_ubicacion)
        Dim vTotalRegistros As Integer = If(pListInvStockPrd Is Nothing, 0, pListInvStockPrd.Count)

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

        Dim BeUbicacionLista As New clsBeBodega_ubicacion

        Try
            vTraceSesion = InvImportTrace_Iniciar(vIdInventarioEnc,
                                                  If(pListInvStockPrd Is Nothing, 0, pListInvStockPrd.Count),
                                                  "InsertaInv=" & InsertaInv &
                                                  ";IdBodega=" & IdBodega &
                                                  ";IdEmpresa=" & IdEmpresa &
                                                  ";EliminarTeorico=" & pEliminarTeorico &
                                                  ";ExisteTeorico=" & pExisteInventarioTeorico)

            '#EJC20260523_INV_IMPORT_BG_CANCEL: cancelación cooperativa antes de abrir transacción larga.
            If pCancelar IsNot Nothing AndAlso pCancelar.Invoke() Then Throw New OperationCanceledException("#EJC20260523_INV_IMPORT_BG_CANCEL: importación cancelada antes de iniciar.")

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
            InvImportTrace_Marca(vTraceSesion, vTraceTotal, vTracePaso, "DAL_TX_OPEN", vIdInventarioEnc, If(pListInvStockPrd Is Nothing, 0, pListInvStockPrd.Count))

            If pExisteInventarioTeorico Then
                If pEliminarTeorico Then
                    Eliminar(pListInvStockPrd(0).Idinventario,
                             pListInvStockPrd(0).TipoTeoricoImportacion,
                             lConnection,
                             lTransaction)
                    pExisteInventarioTeorico = False
                    InvImportTrace_Marca(vTraceSesion, vTraceTotal, vTracePaso, "DAL_ELIMINAR_TEORICO", vIdInventarioEnc, pListInvStockPrd.Count)
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
            InvImportTrace_Marca(vTraceSesion, vTraceTotal, vTracePaso, "DAL_UBIC_RECEPCION", vIdInventarioEnc, pListInvStockPrd.Count, "IdUbicacionRecepcion=" & IdUbicacionRecepcion)

            If prg IsNot Nothing Then
                prg.Maximum = pListInvStockPrd.Count
                prg.Visible = True
            End If

            BeUbicacionLista = New clsBeBodega_ubicacion

            Dim vMaxIdInvStockProd As Integer = 1

            If Not pListInvStockPrd Is Nothing Then
                If pListInvStockPrd.Count > 0 Then
                    vMaxIdInvStockProd = MaxID(vIdInventarioEnc,
                                           lConnection,
                                           lTransaction) + 1
                End If
            End If
            InvImportTrace_Marca(vTraceSesion, vTraceTotal, vTracePaso, "DAL_MAX_ID_STOCK", vIdInventarioEnc, pListInvStockPrd.Count, "MaxId=" & vMaxIdInvStockProd)

            If Not pExisteInventarioTeorico Then
                InvImportTrace_Marca(vTraceSesion, vTraceTotal, vTracePaso, "DAL_STOCK_LOOP_START", vIdInventarioEnc, pListInvStockPrd.Count)
                Dim vBatchStockProd As clsInsertBatch = CrearBatchStockProd()

                'EFREN16112021: Se guarda el stock_prod, y el obj IdTramo queda seteado, si es que se llegara a insertar como inv. inicial
                For Each BeTransInvStockProd As clsBeTrans_inv_stock_prod In pListInvStockPrd
                    If pCancelar IsNot Nothing AndAlso pCancelar.Invoke() Then Throw New OperationCanceledException("#EJC20260523_INV_IMPORT_BG_CANCEL: importación cancelada durante stock_prod.")

                    BeTramoInv = New clsBeTrans_inv_tramo()
                    BeTramoInv.Idinventario = vIdInventarioEnc
                    BeTramoInv.Det_estado = "Nuevo"
                    BeTramoInv.Det_idoperador = IdOperador
                    BeTramoInv.IdBodega = IdBodega

                    BeTransInvStockProd.Idinvstockprod = vMaxIdInvStockProd

                    If BeTransInvStockProd.IdUbicacion > 0 Then

                        If Not vUbicacionesPorId.TryGetValue(BeTransInvStockProd.IdUbicacion, BeUbicacionLista) Then

                            BeUbicacion = New clsBeBodega_ubicacion()
                            vTraceReloj = System.Diagnostics.Stopwatch.StartNew()
                            BeUbicacion = clsLnBodega_ubicacion.Get_Single_By_IdUbicacion_And_IdBodega(BeTransInvStockProd.IdUbicacion,
                                                                                                       IdBodega,
                                                                                                        lConnection,
                                                                                                        lTransaction)
                            vTraceMsUbicStock += vTraceReloj.ElapsedMilliseconds
                            BeUbicacionLista = BeUbicacion
                            vUbicacionesPorId(BeTransInvStockProd.IdUbicacion) = BeUbicacionLista

                        End If

                        If BeUbicacionLista Is Nothing Then
                            Throw New Exception("No se encontro la ubicacion " & BeTransInvStockProd.IdUbicacion & " para la bodega " & IdBodega)
                        End If

                        BeTramoInv.Idtramo = BeUbicacionLista.IdTramo

                        vIndiceTramoInvExistente = lTramosInv.FindIndex(Function(x) x.IdBodega = IdBodega _
                                                                        AndAlso x.Idtramo = BeTramoInv.Idtramo _
                                                                        AndAlso x.Idinventario = BeTramoInv.Idinventario)

                        If vIndiceTramoInvExistente = -1 Then
                            lTramosInv.Add(BeTramoInv)
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

                    vTraceReloj = Stopwatch.StartNew()
                    AgregarBatchStockProd(vBatchStockProd, BeTransInvStockProd)
                    vTraceMsInsertStock += vTraceReloj.ElapsedMilliseconds

                    If prg IsNot Nothing Then prg.Value = Math.Min(Contador, prg.Maximum)

                    Contador += 1 : vMaxIdInvStockProd += 1

                    If Contador Mod 500 = 0 Then
                        '#EJC20260523_INV_IMPORT_PROGRESS: progreso reportado sin tocar UI cuando corre en background.
                        If pProgreso IsNot Nothing Then pProgreso.Invoke(Contador, pListInvStockPrd.Count, "Preparando stock_prod")
                        InvImportTrace_Marca(vTraceSesion,
                                             vTraceTotal,
                                             vTracePaso,
                                             "DAL_STOCK_LOOP_PROGRESS",
                                             vIdInventarioEnc,
                                             pListInvStockPrd.Count,
                                             "Contador=" & Contador &
                                             ";MsUbicStock=" & vTraceMsUbicStock &
                                             ";MsInsertStock=" & vTraceMsInsertStock &
                                             ";UbicCache=" & vUbicacionesPorId.Count &
                                             ";Tramos=" & lTramosInv.Count)
                    End If

                Next

                vTraceReloj = Stopwatch.StartNew()
                vBatchStockProd.Execute(lConnection,
                                        lTransaction,
                                        1000,
                                        0,
                                        pCancelar:=pCancelar,
                                        pProgreso:=Sub(pRowsCopied As Long)
                                                       If pProgreso IsNot Nothing Then pProgreso.Invoke(CInt(Math.Min(pRowsCopied, vTotalRegistros)), vTotalRegistros, "Insertando stock_prod")
                                                   End Sub)
                vTraceMsInsertStock += vTraceReloj.ElapsedMilliseconds
                InvImportTrace_Marca(vTraceSesion,
                                     vTraceTotal,
                                     vTracePaso,
                                     "DAL_STOCK_LOOP_END",
                                     vIdInventarioEnc,
                                     pListInvStockPrd.Count,
                                     "MsUbicStock=" & vTraceMsUbicStock &
                                     ";MsInsertStock=" & vTraceMsInsertStock &
                                     ";UbicCache=" & vUbicacionesPorId.Count &
                                     ";Tramos=" & lTramosInv.Count)

            End If

            If InsertaInv Then
                InvImportTrace_Marca(vTraceSesion, vTraceTotal, vTracePaso, "DAL_DETALLE_SETUP_START", vIdInventarioEnc, pListInvStockPrd.Count)

                MaxIdDet = clsLnTrans_inv_resumen.MaxID(lConnection, lTransaction) + 1

                IdProductoEstado = clsLnI_nav_config_enc.Get_IdProductoEstado_By_IdBodega_And_IdEmpresa(IdBodega,
                                                                                                        IdEmpresa,
                                                                                                        lConnection,
                                                                                                        lTransaction)
                InvImportTrace_Marca(vTraceSesion, vTraceTotal, vTracePaso, "DAL_DETALLE_SETUP_END", vIdInventarioEnc, pListInvStockPrd.Count, "MaxIdDet=" & MaxIdDet & ";IdProductoEstado=" & IdProductoEstado)

                If IdProductoEstado = 0 Then
                    Throw New Exception("ERR_20220510_0929: No está definido el estado de producto en la configuración de la interfase.")
                End If

                BeUbicacionLista = New clsBeBodega_ubicacion

                InvImportTrace_Marca(vTraceSesion,
                                     vTraceTotal,
                                     vTracePaso,
                                     "DAL_PRODUCTO_CACHE_START",
                                     vIdInventarioEnc,
                                     pListInvStockPrd.Count)
                vTraceReloj = System.Diagnostics.Stopwatch.StartNew()
                vProductosPorCodigo = clsLnProducto.Get_All_By_Codigos_For_InventarioImport(InvImportCodigosUnicos(pListInvStockPrd),
                                                                                             lConnection,
                                                                                             lTransaction)
                vTraceMsProductoCacheBulk = vTraceReloj.ElapsedMilliseconds
                InvImportTrace_Marca(vTraceSesion,
                                     vTraceTotal,
                                     vTracePaso,
                                     "DAL_PRODUCTO_CACHE_END",
                                     vIdInventarioEnc,
                                     pListInvStockPrd.Count,
                                     "ProductoCache=" & vProductosPorCodigo.Count &
                                     ";MsProductoCacheBulk=" & vTraceMsProductoCacheBulk)

                Contador = 0
                InvImportTrace_Marca(vTraceSesion, vTraceTotal, vTracePaso, "DAL_DETALLE_LOOP_START", vIdInventarioEnc, pListInvStockPrd.Count)
                Dim vBatchDetalle As clsInsertBatch = CrearBatchDetalle()
                Dim vBatchResumen As clsInsertBatch = CrearBatchResumen()
                For Each BeTransInvStockProd As clsBeTrans_inv_stock_prod In pListInvStockPrd
                    If pCancelar IsNot Nothing AndAlso pCancelar.Invoke() Then Throw New OperationCanceledException("#EJC20260523_INV_IMPORT_BG_CANCEL: importación cancelada durante detalle.")

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

                        If Not vUbicacionesPorId.TryGetValue(BeTransInvStockProd.IdUbicacion, BeUbicacionLista) Then

                            BeUbicacion = New clsBeBodega_ubicacion()
                            vTraceReloj = System.Diagnostics.Stopwatch.StartNew()
                            BeUbicacion = clsLnBodega_ubicacion.Get_Single_By_IdUbicacion_And_IdBodega(BeTransInvStockProd.IdUbicacion,
                                                                                                       IdBodega,
                                                                                                        lConnection,
                                                                                                        lTransaction)
                            vTraceMsUbicDetalle += vTraceReloj.ElapsedMilliseconds
                            BeUbicacionLista = BeUbicacion
                            vUbicacionesPorId(BeTransInvStockProd.IdUbicacion) = BeUbicacionLista

                        End If

                        If BeUbicacionLista Is Nothing Then
                            Throw New Exception("No se encontro la ubicacion " & BeTransInvStockProd.IdUbicacion & " para la bodega " & IdBodega)
                        End If

                        InvDetalle.IdUbicacion = BeTransInvStockProd.IdUbicacion
                        InvDetalle.Idtramo = BeUbicacionLista.IdTramo

                        BeTramoInv.Idtramo = BeUbicacionLista.IdTramo

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
                        'BeTramoInv.Idtramo = BeUbicacionRecepcionDefectoPorBodega.IdTramo
                        InvDetalle.Idtramo = BeUbicacionRecepcionDefectoPorBodega.IdTramo
                        InvDetalle.IdUbicacion = BeUbicacionRecepcionDefectoPorBodega.IdUbicacion
                    End If

                    'EFREN17112021 valores de un inv multipropietario
                    Dim pBeProducto As clsBeProducto = Nothing

                    '#EJC20260522_INV_IMPORT_PRODUCTO_LITE: producto lite cacheado; evita Obtener(Propietario) y Get_Nombre por fila.
                    vTraceReloj = System.Diagnostics.Stopwatch.StartNew()
                    If Not vProductosPorCodigo.TryGetValue(BeTransInvStockProd.Codigo, pBeProducto) Then
                        pBeProducto = clsLnProducto.Get_Single_By_Codigo_For_InventarioImport(BeTransInvStockProd.Codigo,
                                                                                              lConnection,
                                                                                              lTransaction)
                        vProductosPorCodigo(BeTransInvStockProd.Codigo) = pBeProducto
                    End If
                    vTraceMsProductoDetalle += vTraceReloj.ElapsedMilliseconds

                    vTraceReloj.Restart()
                    Dim vIdPropietario As Integer = If(pBeProducto Is Nothing OrElse pBeProducto.Propietario Is Nothing, 0, pBeProducto.Propietario.IdPropietario)
                    Dim vClavePropietarioBodega As String = vIdPropietario & "|" & BeTransInvStockProd.IdBodega
                    Dim vIdPropietarioBodega As Integer = 0

                    If Not vPropietarioBodegaPorClave.TryGetValue(vClavePropietarioBodega, vIdPropietarioBodega) Then
                        vIdPropietarioBodega = clsLnPropietario_bodega.Get_IdPropietarioBodega_By_IdPropietario_And_IdBodega(vIdPropietario,
                                                                                                                             BeTransInvStockProd.IdBodega)
                        vPropietarioBodegaPorClave(vClavePropietarioBodega) = vIdPropietarioBodega
                    End If
                    vTraceMsPropietarioBodega += vTraceReloj.ElapsedMilliseconds

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
                    vTraceReloj.Restart()
                    InvDetalle.Nom_producto = If(pBeProducto Is Nothing, "", pBeProducto.Nombre)
                    vTraceMsNombreProducto += vTraceReloj.ElapsedMilliseconds
                    InvDetalle.Nom_operador = NomOperador
                    InvDetalle.Carga = 0
                    InvDetalle.Peso = BeTransInvStockProd.Peso
                    'EFREN10052021 se agregan los campos que identifican al inv multiempresa
                    InvDetalle.IdPropietarioBodega = vIdPropietarioBodega
                    InvDetalle.nombre_propietario = If(pBeProducto Is Nothing OrElse pBeProducto.Propietario Is Nothing, "", pBeProducto.Propietario.Nombre_comercial)
                    'GT02122021 Se agregan los valores de la LP y cod_variante
                    InvDetalle.License_plate = BeTransInvStockProd.License_plate
                    InvDetalle.Codigo_variante = BeTransInvStockProd.Codigo_variante
                    '#GT25112022_1120: campos DyD
                    InvDetalle.costo = BeTransInvStockProd.Costo
                    InvDetalle.precio = BeTransInvStockProd.Precio
                    InvDetalle.IdProductoParametroA = IIf(BeTransInvStockProd.Parametro_a = "", 0, BeTransInvStockProd.Parametro_a)
                    InvDetalle.IdProductoParametroB = IIf(BeTransInvStockProd.Parametro_b = "", 0, BeTransInvStockProd.Parametro_b)
                    InvDetalle.IdProductoTallaColor = BeTransInvStockProd.IdProductoTallaColor

                    vTraceReloj.Restart()
                    AgregarBatchDetalle(vBatchDetalle, InvDetalle)
                    vTraceMsInsertDetalle += vTraceReloj.ElapsedMilliseconds

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

                    vTraceReloj.Restart()
                    AgregarBatchResumen(vBatchResumen, InvResumen)
                    vTraceMsInsertResumen += vTraceReloj.ElapsedMilliseconds

                    ' End If

                    MaxIdDet = MaxIdDet + 1

                    vIndiceTramoInvExistente = lTramosInv.FindIndex(Function(x) x.IdBodega = IdBodega _
                                                                    AndAlso x.Idtramo = BeTramoInv.Idtramo _
                                                                    AndAlso x.Idinventario = BeTramoInv.Idinventario)

                    If vIndiceTramoInvExistente = -1 Then
                        lTramosInv.Add(BeTramoInv)
                    End If

                    Contador += 1
                    If Contador Mod 250 = 0 Then
                        '#EJC20260523_INV_IMPORT_PROGRESS: progreso reportado desde el loop pesado del detalle.
                        If pProgreso IsNot Nothing Then pProgreso.Invoke(Contador, pListInvStockPrd.Count, "Preparando detalle")
                        InvImportTrace_Marca(vTraceSesion,
                                             vTraceTotal,
                                             vTracePaso,
                                             "DAL_DETALLE_LOOP_PROGRESS",
                                             vIdInventarioEnc,
                                             pListInvStockPrd.Count,
                                             "Contador=" & Contador &
                                             ";MsUbicDetalle=" & vTraceMsUbicDetalle &
                                             ";MsProducto=" & vTraceMsProductoDetalle &
                                             ";MsPropietarioBodega=" & vTraceMsPropietarioBodega &
                                             ";MsNombreProducto=" & vTraceMsNombreProducto &
                                             ";MsInsertDetalle=" & vTraceMsInsertDetalle &
                                             ";MsInsertResumen=" & vTraceMsInsertResumen &
                                             ";ProductoCache=" & vProductosPorCodigo.Count &
                                             ";PropBodegaCache=" & vPropietarioBodegaPorClave.Count &
                                             ";UbicCache=" & vUbicacionesPorId.Count &
                                             ";Tramos=" & lTramosInv.Count)
                    End If

                Next

                vTraceReloj = Stopwatch.StartNew()
                vBatchDetalle.Execute(lConnection,
                                      lTransaction,
                                      1000,
                                      0,
                                      pCancelar:=pCancelar,
                                      pProgreso:=Sub(pRowsCopied As Long)
                                                     If pProgreso IsNot Nothing Then pProgreso.Invoke(CInt(Math.Min(pRowsCopied, vTotalRegistros)), vTotalRegistros, "Insertando detalle")
                                                 End Sub)
                vTraceMsInsertDetalle += vTraceReloj.ElapsedMilliseconds

                vTraceReloj.Restart()
                vBatchResumen.Execute(lConnection,
                                      lTransaction,
                                      1000,
                                      0,
                                      pCancelar:=pCancelar,
                                      pProgreso:=Sub(pRowsCopied As Long)
                                                     If pProgreso IsNot Nothing Then pProgreso.Invoke(CInt(Math.Min(pRowsCopied, vTotalRegistros)), vTotalRegistros, "Insertando resumen")
                                                 End Sub)
                vTraceMsInsertResumen += vTraceReloj.ElapsedMilliseconds
                InvImportTrace_Marca(vTraceSesion,
                                     vTraceTotal,
                                     vTracePaso,
                                     "DAL_DETALLE_LOOP_END",
                                     vIdInventarioEnc,
                                     pListInvStockPrd.Count,
                                     "MsUbicDetalle=" & vTraceMsUbicDetalle &
                                     ";MsProductoCacheBulk=" & vTraceMsProductoCacheBulk &
                                     ";MsProducto=" & vTraceMsProductoDetalle &
                                     ";MsPropietarioBodega=" & vTraceMsPropietarioBodega &
                                     ";MsNombreProducto=" & vTraceMsNombreProducto &
                                     ";MsInsertDetalle=" & vTraceMsInsertDetalle &
                                     ";MsInsertResumen=" & vTraceMsInsertResumen &
                                     ";UbicCache=" & vUbicacionesPorId.Count &
                                     ";Tramos=" & lTramosInv.Count)

            End If

            For Each BeTramoInvInLista In lTramosInv
                If pCancelar IsNot Nothing AndAlso pCancelar.Invoke() Then Throw New OperationCanceledException("#EJC20260523_INV_IMPORT_BG_CANCEL: importación cancelada durante tramos.")
                vTraceReloj = System.Diagnostics.Stopwatch.StartNew()

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
                vTraceMsTramos += vTraceReloj.ElapsedMilliseconds

            Next
            InvImportTrace_Marca(vTraceSesion, vTraceTotal, vTracePaso, "DAL_TRAMOS_END", vIdInventarioEnc, pListInvStockPrd.Count, "Tramos=" & lTramosInv.Count & ";MsTramos=" & vTraceMsTramos)

            InvImportTrace_Marca(vTraceSesion, vTraceTotal, vTracePaso, "DAL_COMMIT_START", vIdInventarioEnc, pListInvStockPrd.Count)
            lTransaction.Commit()
            InvImportTrace_Marca(vTraceSesion, vTraceTotal, vTracePaso, "DAL_COMMIT_END", vIdInventarioEnc, pListInvStockPrd.Count)

        Catch ex As OperationCanceledException
            InvImportTrace_Marca(vTraceSesion, vTraceTotal, vTracePaso, "DAL_CANCELADO", vIdInventarioEnc, If(pListInvStockPrd Is Nothing, 0, pListInvStockPrd.Count), ex.Message)
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw
        Catch ex As Exception
            InvImportTrace_Marca(vTraceSesion, vTraceTotal, vTracePaso, "DAL_ERROR", vIdInventarioEnc, If(pListInvStockPrd Is Nothing, 0, pListInvStockPrd.Count), ex.Message)
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            InvImportTrace_Marca(vTraceSesion,
                                 vTraceTotal,
                                 vTracePaso,
                                 "DAL_IMPORTAR_PRODUCTOS_FIN",
                                 vIdInventarioEnc,
                                 If(pListInvStockPrd Is Nothing, 0, pListInvStockPrd.Count),
                                 "MsUbicStock=" & vTraceMsUbicStock &
                                 ";MsInsertStock=" & vTraceMsInsertStock &
                                 ";MsUbicDetalle=" & vTraceMsUbicDetalle &
                                 ";MsProductoCacheBulk=" & vTraceMsProductoCacheBulk &
                                 ";MsProductoDetalle=" & vTraceMsProductoDetalle &
                                 ";MsPropietarioBodega=" & vTraceMsPropietarioBodega &
                                 ";MsNombreProducto=" & vTraceMsNombreProducto &
                                 ";MsInsertDetalle=" & vTraceMsInsertDetalle &
                                 ";MsInsertResumen=" & vTraceMsInsertResumen &
                                 ";MsTramos=" & vTraceMsTramos &
                                 ";UbicCache=" & vUbicacionesPorId.Count)
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Sub

    Private Shared Function InvImportCodigosUnicos(ByVal pListInvStockPrd As List(Of clsBeTrans_inv_stock_prod)) As List(Of String)

        Dim vCodigos As New List(Of String)()
        Dim vUnicos As New System.Collections.Generic.HashSet(Of String)(System.StringComparer.OrdinalIgnoreCase)

        If pListInvStockPrd Is Nothing Then Return vCodigos

        For Each vItem As clsBeTrans_inv_stock_prod In pListInvStockPrd
            Dim vCodigo As String = If(vItem Is Nothing, "", If(vItem.Codigo, "").Trim())

            If vCodigo <> "" AndAlso vUnicos.Add(vCodigo) Then
                vCodigos.Add(vCodigo)
            End If
        Next

        Return vCodigos

    End Function
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
