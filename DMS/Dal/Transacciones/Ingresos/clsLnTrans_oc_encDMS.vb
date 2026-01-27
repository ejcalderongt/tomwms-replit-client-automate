Imports System.Reflection
Imports DevExpress.Compatibility
Imports DevExpress.Data.Helpers
Imports DevExpress.Data.Linq
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.ColorPick
Imports DevExpress.XtraEditors.TextEditController
Imports DevExpress.XtraPrinting.Native
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports TOMWMS


Public Class clsLnTrans_oc_encDMS



    Public Shared Async Sub Exportacion_IngresosAsync(ByVal lblprg As RichTextBox)
        Dim api As New ApiService()
        Dim reloj As New Stopwatch()
        Dim listOC As New List(Of clsBeTrans_oc_enc)
        Dim Contador As Integer = 0
        Dim RegistrosEncontrados As Integer = 0
        Dim pTablaSincronizada As String = ""
        Dim resultado As String = ""
        Dim pRegistrosFallidos As Integer = 0
        Dim pRegistrosExitosos As Integer = 0
        Try

            reloj.Start()
            pTablaSincronizada = clsHelper.ObtenerNombreTabla("ExportarIngresos")
            clsHelper.LogMensaje(lblprg, "Iniciando carga de ingresos...", clsHelper.TipoMensaje.Info)

            '#GT18062025: obtener los ingresos no sincronizados
            listOC = GetAll_By_CDC(pTablaSincronizada, listOC)

            If listOC IsNot Nothing AndAlso listOC.Count > 0 Then
                RegistrosEncontrados = listOC.Count
                clsHelper.LogMensaje(lblprg, "Ingresos encontrados " & listOC.Count, clsHelper.TipoMensaje.Exito)
            Else
                clsHelper.LogMensaje(lblprg, "Ingresos no encontrados ", clsHelper.TipoMensaje.Error_)
                Exit Sub
            End If

            '#Iteramos por ingreso y enviamos a la nube para no hacer un proceso único pesado
            For Each pOC In listOC
                Contador += 1
                Dim enviado As Boolean = False
                Dim intento As Integer = 0
                Const maxIntentos As Integer = 3

                clsHelper.LogMensaje(lblprg, "Iterando Registro: " & Contador & "/" & registros, clsHelper.TipoMensaje.Info)

                Dim JsonOC = Crear_Json(lblprg, pOC)

                If String.IsNullOrEmpty(JsonOC) Then
                    pRegistrosFallidos += 1
                    resultado = "No se generó el archivo json correspondiente."
                    Guadar_Envio_Rechazado(pOC.IdOrdenCompraEnc, resultado)
                    Continue For
                Else
                    While Not enviado And intento <= maxIntentos

                        resultado = Await api.EnviarJsonOCAsync(JsonOC, lblprg)

                        If resultado = "Ok" Then
                            enviado = True
                            pRegistrosExitosos += 1
                            '#GT marcar como enviado MI3 en oc_enc
                        Else
                            intento += 1
                            clsHelper.LogMensaje(lblprg, "Reintento de envio: " & intento, clsHelper.TipoMensaje.Info)
                            Await Task.Delay(2000) ' Esperar 2 segundos entre intentos
                        End If

                    End While

                    ' Si después de los intentos no se pudo enviar el registro
                    If Not enviado Then
                        pRegistrosFallidos += 1
                        Guadar_Envio_Rechazado(pOC, resultado)
                    End If
        Next

            End If

            Next

    Public Shared Sub Guadar_Envio_Rechazado(ByVal pOrdenCompra As clsBeTrans_oc_enc, ByVal pMensaje As String,
                                                                                 Optional ByRef lConnection As SqlConnection = Nothing,
                                                                                 Optional ByRef lTransaction As SqlTransaction = Nothing)

        Dim localConnection As Boolean = False
        Dim localTransaction As Boolean = False
        Dim BeLogSyncError As New clsBeDMS_Log_sincronizacion_fallos()

        Try

            ' Crear conexión si no se recibió
            If lConnection Is Nothing Then
                If lTransaction IsNot Nothing Then
                    lConnection = lTransaction.Connection
                Else
                    lConnection = New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                    lConnection.Open()
                    localConnection = True
                End If
            End If

            ' Crear transacción si no se recibió
            If lTransaction Is Nothing Then
                lTransaction = lConnection.BeginTransaction()
                localTransaction = True
            End If

            BeLogSyncError = New clsBeDMS_Log_sincronizacion_fallos()
            BeLogSyncError.IdLogFallo = clsLnDMS_Log_sincronizacion_fallos.MaxID(lConnection, lTransaction) + 1
            BeLogSyncError.IdOrdenCompraEnc = pOrdenCompra.IdOrdenCompraEnc
            BeLogSyncError.IdPropietario = pOrdenCompra.PropietarioBodega.IdPropietario
            BeLogSyncError.IdPedidoEnc = 0
            BeLogSyncError.Estado = "Error"
            BeLogSyncError.Mensaje_error = pMensaje
            BeLogSyncError.Fec_agr = Now
            BeLogSyncError.IdProducto = 0
            clsLnDMS_Log_sincronizacion_fallos.Insertar(BeLogSyncError, lConnection, lTransaction)

            If localTransaction Then
                lTransaction.Commit()
            End If

        Catch ex As Exception
            Try
                lTransaction.Rollback()
            Catch
                ' Ignorar errores de rollback

            Finally
                ' Cierre solo si es local
                If localConnection AndAlso lConnection IsNot Nothing AndAlso lConnection.State = ConnectionState.Open Then
                    lConnection.Close()
                End If
                Dim clsTransaccion As New clsTransaccion()
                Try
                    BeLogSyncError = New clsBeLog_sincronizacion_fallos()
                    BeLogSyncError.IdLogFallo = clsLnLog_sincronizacion_fallos.MaxID() + 1
    '    Dim localTransaction As Boolean = False
    '    Dim BeLogSyncError As New clsBeDMS_Log_sincronizacion_fallos()

    '    Try

    '        ' Crear conexión si no se recibió
    '        If lConnection Is Nothing Then
    '            lConnection = New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
    '            lConnection.Open()
    '            localConnection = True
    '        End If

    '        ' Crear transacción si no se recibió
    '        If lTransaction Is Nothing Then
    '            lTransaction = lConnection.BeginTransaction()
    '            localTransaction = True
    '        End If


    '        BeLogSyncError = New clsBeDMS_Log_sincronizacion_fallos()
    '        BeLogSyncError.IdLogFallo = clsLnDMS_Log_sincronizacion_fallos.MaxID(lConnection, lTransaction) + 1
    '        BeLogSyncError.IdOrdenCompraEnc = pOrdenCompra.IdOrdenCompraEnc
    '        BeLogSyncError.IdPropietario = pOrdenCompra.PropietarioBodega.IdPropietario
    '        BeLogSyncError.IdPedidoEnc = 0
    '        BeLogSyncError.Estado = "Error"
    '        BeLogSyncError.Mensaje_error = pMensaje
    '        BeLogSyncError.Fec_agr = Now
    '        BeLogSyncError.IdProducto = 0
    '        clsLnDMS_Log_sincronizacion_fallos.Insertar(BeLogSyncError, lConnection, lTransaction)

    '        If localTransaction Then
    '            lTransaction.Commit()
    '        End If

    '    Catch ex As Exception
    '        ' Rollback si la transacción es local
    '        If localTransaction AndAlso lTransaction IsNot Nothing Then
    '            lTransaction.Rollback()
    '        End If

    '        Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))

    '    Finally
    '        ' Cierre solo si es local
    '        If localConnection AndAlso lConnection IsNot Nothing AndAlso lConnection.State = ConnectionState.Open Then
    '            lConnection.Close()
    '        End If
    '    End Try
    'End Sub

    Public Shared Sub Actualizar_Envio_Rechazado(ByVal pOrdenCompraEnc As clsBeTrans_oc_enc)
        Dim BeLogSyncError As New clsBeDMS_Log_sincronizacion_fallos()
        Dim clsTransaccion As New clsTransaccion()
        Try
            clsTransaccion.Begin_Transaction()

            If clsLnDMS_Log_sincronizacion_fallos.Existe_by_Ingreso(pOrdenCompraEnc.IdOrdenCompraEnc, clsTransaccion.lConnection, clsTransaccion.lTransaction) Then
                BeLogSyncError = New clsBeDMS_Log_sincronizacion_fallos()
                BeLogSyncError.IdOrdenCompraEnc = pOrdenCompraEnc.IdOrdenCompraEnc
                BeLogSyncError.Mensaje_error = pMensaje
                BeLogSyncError.Fec_agr = Now
                BeLogSyncError.IdPedidoEnc = 0

                clsLnLog_sincronizacion_fallos.Insertar(BeLogSyncError)

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Crear_Json(ByRef lblprg As RichTextBox, ByVal pOCEnc As clsBeTrans_oc_enc) As String
        Crear_Json = ""
        Dim clsTransaccion As New clsTransaccion()
        Dim listPayload As New List(Of Object)
        Dim reOcList As New List(Of Object)
        Dim reOperadorList As New List(Of Object)
        Dim operadorBodegaList As New List(Of Object)
        Dim operadorList As New List(Of Object)
        Dim facturasList As New List(Of Object)
        Dim imgList As New List(Of Object)
        Dim recepcionesList As New List(Of Object)
        Dim PayLoadOCList As New List(Of Object)
        Dim stock_recList As New List(Of Object)
        Dim ocDetList As New List(Of Object)
        Dim ocDet As New Object
        Dim stockList As New List(Of Object)
        Dim trans_movimientosList As New List(Of Object)

        Dim pListOC As New List(Of clsBeTrans_oc_enc)()
        Dim pListRecepcionEnc As New List(Of clsBeTrans_re_enc)()
        Dim ListReOC As New List(Of clsBeTrans_re_oc)()
        Dim pTrans_re_enc As New clsBeTrans_re_enc
        Dim pListOperadores As New List(Of clsBeTrans_re_op)()
        Dim pListFacturasRe As New List(Of clsBeTrans_re_fact)()
        Dim pListaImgRe As New List(Of clsBeTrans_re_img)()
        Dim pTrans_re_tr As New clsBeTrans_re_tr()
        Dim pTrans_movimientos As New clsBeTrans_movimientos()
        Dim pStock_Rec As New clsBeStock_rec()
        Dim pStock As New clsBeStock()
        Dim pListBodega_Area As New List(Of clsBeBodega_area)()
        Dim pListBodega_Sector As New List(Of clsBeBodega_sector)()
        Dim pListBodega_Tramo As New List(Of clsBeBodega_tramo)()
        Dim pListBodega_Ubicacion As New List(Of clsBeBodega_ubicacion)()
        Dim pListOperadorBodega As New List(Of clsBeOperador_bodega)()
        Dim pListOperador As New List(Of clsBeOperador)()


        Try

            clsTransaccion.Begin_Transaction()
            listPayload = New List(Of Object)
            clsHelper.LogMensaje(lblprg, "Procesando ingreso: " & pOCEnc.IdOrdenCompraEnc, clsHelper.TipoMensaje.Info)


            reOcList = New List(Of Object)()
            reOperadorList = New List(Of Object)()
            operadorBodegaList = New List(Of Object)
            operadorList = New List(Of Object)
            facturasList = New List(Of Object)()
            imgList = New List(Of Object)()
            recepcionesList = New List(Of Object)()
            PayLoadOCList = New List(Of Object)
            stock_recList = New List(Of Object)
            stockList = New List(Of Object)
            ocDetList = New List(Of Object)
            trans_movimientosList = New List(Of Object)

            pOCEnc.DetalleOC = New List(Of clsBeTrans_oc_det)()
            pOCEnc.ObjPoliza = New clsBeTrans_oc_pol()
            pOCEnc.TipoIngreso = New clsBeTrans_oc_ti()
            pListRecepcionEnc = New List(Of clsBeTrans_re_enc)()
            ListReOC = New List(Of clsBeTrans_re_oc)
            Dim productobodega As New clsBeProducto_bodega()

            '#GT25062025: obtener listas de proveedor y proveedor_bdega.
            Dim oBePropietarioBodega = clsLnPropietario_bodega.Get_Single_With_Propietario(pOCEnc.IdPropietarioBodega, clsTransaccion.lConnection, clsTransaccion.lTransaction)

            If oBePropietarioBodega IsNot Nothing Then
                Dim ListBeProveedor = clsLnProveedor.GetListProveedores_By_Activo_and_IdPropietario(oBePropietarioBodega.IdPropietario, True, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                If ListBeProveedor IsNot Nothing AndAlso ListBeProveedor.Count > 0 Then
                    For Each Proveedor In ListBeProveedor

                        proveedorList.Add(New With {
                                        .IdEmpresa = Proveedor.IdEmpresa,
                                        .IdPropietario = Proveedor.IdPropietario,
                                        .IdProveedor = Proveedor.IdProveedor,
                                        .codigo = Proveedor.Codigo,
                                        .nombre = Proveedor.Nombre,
                                        .telefono = Proveedor.Telefono,
                                        .nit = Proveedor.Nit,
                                        .direccion = Proveedor.Direccion,
                                        .email = Proveedor.Email,
                                        .contacto = Proveedor.Contacto,
                                        .activo = Proveedor.Activo,
                                        .muestra_precio = Proveedor.Muestra_precio,
                                        .user_agr = Proveedor.User_agr,
                                        .fec_agr = Proveedor.Fec_agr,
                                        .user_mod = Proveedor.User_mod,
                                        .fec_mod = Proveedor.Fec_mod,
                                        .actualiza_costo_oc = Proveedor.Actualiza_costo_oc,
                                        .idubicacionvirtual = Proveedor.IdUbicacionVirtual,
                                        .es_bodega_recepcion = Proveedor.Es_Bodega_Recepcion,
                                        .es_bodega_traslado = Proveedor.Es_Bodega_Traslado,
                                        .referencia = Proveedor.Referencia,
                                        .sistema = Proveedor.Sistema,
                                        .IdConfiguracionBarraPallet = Proveedor.IdConfiguracionBarraPallet,
                                        .es_proveedor_servicio = Proveedor.Es_Proveedor_Servicio,
                                        .IdBodegaAreaSAP = Proveedor.IdBodegaAreaSAP,
                                        .IdPais = Proveedor.IdPais,
                                        .Codigo_Empresa_ERP = Proveedor.Codigo_Empresa_ERP
                                          })

                        Dim ListProveedorBodega = clsLnProveedor_bodega.Get_All_By_IdProveedor(Proveedor.IdProveedor, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                        If ListProveedorBodega IsNot Nothing AndAlso ListProveedorBodega.Count > 0 Then
                            For Each Proveedor_Bodega In ListProveedorBodega
                                proveedores_bodegaList.Add(New With {
                                                                    .IdAsignacion = Proveedor_Bodega.IdAsignacion,
                                                                    .IdProveedor = Proveedor_Bodega.IdProveedor,
                                                                    .IdBodega = Proveedor_Bodega.IdBodega,
                                                                    .activo = Proveedor_Bodega.Activo,
                                                                    .user_agr = Proveedor_Bodega.User_agr,
                                                                    .fec_agr = Proveedor_Bodega.Fec_agr,
                                                                    .user_mod = Proveedor_Bodega.User_mod,
                                                                    .fec_mod = Proveedor_Bodega.Fec_mod,
                                                                    .IdAreaOrigen = Proveedor_Bodega.IdAreaOrigen
                                                                    })
                            Next
                        Else
                            resultado = "Por un error desconocido, no se pudo obtener proveedor_bodega del ingreso: " & pOCEnc.IdOrdenCompraEnc
                            clsHelper.LogMensaje(lblprg, resultado, clsHelper.TipoMensaje.Error_)
                            Guadar_Envio_Rechazado(pOCEnc, resultado, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                            clsTransaccion.Commit_Transaction()
                            Return ""
                        End If

                    Next
                Else
                    resultado = "Por un error desconocido, no se pudo obtener proveedor del ingreso: " & pOCEnc.IdOrdenCompraEnc
                    clsHelper.LogMensaje(lblprg, resultado, clsHelper.TipoMensaje.Error_)
                    Guadar_Envio_Rechazado(pOCEnc, resultado, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                    clsTransaccion.Commit_Transaction()
                    Return ""
                End If
            Else
                resultado = "Por un error desconocido, no se pudo obtener propietario del ingreso: " & pOCEnc.IdOrdenCompraEnc
                clsHelper.LogMensaje(lblprg, resultado, clsHelper.TipoMensaje.Error_)
                Guadar_Envio_Rechazado(pOCEnc, resultado, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                clsTransaccion.Commit_Transaction()
                Return ""
            End If

            '#GT13052025: al obtener el detalle de la OC, agregar la propiedad de producto_bodega por linea
            pOCEnc.DetalleOC = clsLnTrans_oc_det.Get_Detalle_OC_By_IdOrdenCompraEnc(pOCEnc.IdOrdenCompraEnc, clsTransaccion.lConnection, clsTransaccion.lTransaction)

            If pOCEnc.DetalleOC IsNot Nothing AndAlso pOCEnc.DetalleOC.Count > 0 Then

                For Each oc_det As clsBeTrans_oc_det In pOCEnc.DetalleOC

                    productobodega = New clsBeProducto_bodega()
                    'productobodega = clsLnProducto_bodega.GetSingle(oc_det.IdProductoBodega, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                    productobodega.IdProductoBodega = oc_det.IdProductoBodega

                    If clsLnProducto_bodega.Obtener(productobodega, clsTransaccion.lConnection, clsTransaccion.lTransaction) Then

                        ocDet = New Object()
                        ocDet = New With {
                                                    .IdOrdenCompraEnc = oc_det.IdOrdenCompraEnc,
                                                    .IdOrdenCompraDet = oc_det.IdOrdenCompraDet,
                                                    .IdProductoBodega = oc_det.IdProductoBodega,
                                                    .IdArancel = oc_det.IdArancel,
                                                    .IdPresentacion = oc_det.IdPresentacion,
                                                    .IdUnidadMedidaBasica = oc_det.IdUnidadMedidaBasica,
                                                    .IdMotivoDevolucion = oc_det.IdMotivoDevolucion,
                                                    .No_Linea = oc_det.No_Linea,
                                                    .nombre_producto = oc_det.Nombre_producto,
                                                    .nombre_presentacion = oc_det.Nombre_presentacion,
                                                    .nombre_arancel = oc_det.Nombre_arancel,
                                                    .porcentaje_arancel = oc_det.Porcentaje_arancel,
                                                    .nombre_unidad_medida_basica = oc_det.Nombre_unidad_medida_basica,
                                                    .cantidad = oc_det.Cantidad,
                                                    .cantidad_recibida = oc_det.Cantidad_recibida,
                                                    .costo = oc_det.Costo,
                                                    .total_linea = oc_det.Total_linea,
                                                    .user_agr = oc_det.User_agr,
                                                    .fec_agr = oc_det.Fec_agr,
                                                    .user_mod = oc_det.User_mod,
                                                    .fec_mod = oc_det.Fec_mod,
                                                    .activo = oc_det.Activo,
                                                    .peso = oc_det.Peso,
                                                    .peso_recibido = oc_det.Peso_Recibido,
                                                    .atributo_variante_1 = oc_det.Atributo_variante_1,
                                                    .codigo_producto = oc_det.Codigo_Producto,
                                                    .valor_aduana = oc_det.valor_aduana,
                                                    .valor_fob = oc_det.valor_fob,
                                                    .valor_iva = oc_det.valor_iva,
                                                    .valor_dai = oc_det.valor_dai,
                                                    .valor_seguro = oc_det.valor_seguro,
                                                    .valor_flete = oc_det.valor_flete,
                                                    .peso_neto = oc_det.Peso_Neto,
                                                    .peso_bruto = oc_det.Peso_Bruto,
                                                    .IdPropietarioBodega = oc_det.IdPropietarioBodega,
                                                    .nombre_propietario = oc_det.Nombre_Propietario,
                                                    .IdOrdenCompraDetPadre = oc_det.IdOrdenCompraDetPadre,
                                                    .IdEmbarcador = oc_det.IdEmbarcador,
                                                    .productoBodega = New With {
                                                                                .idProductoBodega = productobodega.IdProductoBodega,
                                                                                .idProducto = productobodega.IdProducto,
                                                                                .idBodega = productobodega.IdBodega,
                                                                                .activo = productobodega.Activo,
                                                                                .sistema = productobodega.Sistema,
                                                                                .user_Agr = productobodega.User_agr,
                                                                                .fec_Agr = productobodega.Fec_agr,
                                                                                .user_Mod = productobodega.User_mod,
                                                                                .fec_Mod = productobodega.Fec_mod
                                                                                }
                                                    }

                    Else
                        'Throw New Exception("Por un error desconocido, no se pudo obtener el producto_bodega.")
                        clsHelper.LogMensaje(lblprg, "Por un error desconocido, no se pudo obtener el producto_bodega.", clsHelper.TipoMensaje.Error_)
                        Return ""
                    End If

                    ocDetList.Add(ocDet)

                Next
            Else
                clsHelper.LogMensaje(lblprg, "El ingreso no tiene detalle.", clsHelper.TipoMensaje.Error_)
                Return ""
            End If

            pOCEnc.TipoIngreso = clsLnTrans_oc_ti.GetSingle(pOCEnc.IdTipoIngresoOC, clsTransaccion.lConnection, clsTransaccion.lTransaction)
            If pOCEnc.Control_Poliza Then pOCEnc.ObjPoliza = clsLnTrans_oc_pol.GetSingle(pOCEnc.IdOrdenCompraEnc, clsTransaccion.lConnection, clsTransaccion.lTransaction)
            ListReOC = clsLnTrans_re_oc.GetListReOC_By_IdOrdenCompraEnc(pOCEnc.IdOrdenCompraEnc, clsTransaccion.lConnection, clsTransaccion.lTransaction)

            If ListReOC IsNot Nothing AndAlso ListReOC.Count > 0 Then

                For Each pTrans_re_oc As clsBeTrans_re_oc In ListReOC

                    pTrans_re_enc = New clsBeTrans_re_enc()
                    pTrans_re_enc = clsLnTrans_re_enc.GetSingle(pTrans_re_oc.IdRecepcionEnc, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                    pListOperadores = New List(Of clsBeTrans_re_op)()
                    pListOperadores = clsLnTrans_re_op.Get_All_Operadores_By_IdRecepcionEnc(pTrans_re_oc.IdRecepcionEnc, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                    pListFacturasRe = clsLnTrans_re_fact.Get_Detalle_Facturas_By_IdRecepcionEnc(pTrans_re_oc.IdRecepcionEnc, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                    pListaImgRe = clsLnTrans_re_img.Get_Detalle_Imagenes_By_IdRecepcionEnc(pTrans_re_oc.IdRecepcionOc, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                    pTrans_re_tr = clsLnTrans_re_tr_Partial.GetSingle(pTrans_re_enc.IdTipoTransaccion, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                    '#********************* propiedades de la recepción *****************************************
                    If pTrans_re_enc IsNot Nothing Then
                        pListRecepcionEnc.Add(pTrans_re_enc)
                        reOcList.Add(New With {
                                                             .idRecepcionOc = pTrans_re_oc.IdRecepcionOc,
                                                             .idRecepcionEnc = pTrans_re_oc.IdRecepcionEnc,
                                                             .idOrdenCompraEnc = pTrans_re_oc.IdOrdenCompraEnc,
                                                             .recepcion_Ciega = pTrans_re_oc.Recepcion_ciega,
                                                             .recepcion_Manual = pTrans_re_oc.Recepcion_manual,
                                                             .no_Docto = pTrans_re_oc.No_docto,
                                                             .hora_Ini_Hh = pTrans_re_oc.Hora_ini_hh,
                                                             .hora_Fin_Hh = pTrans_re_oc.Hora_fin_hh,
                                                             .user_Agr = pTrans_re_oc.User_agr,
                                                             .fec_Agr = pTrans_re_oc.Fec_agr,
                                                             .firma_Operador = pTrans_re_oc.Firma_operador
                                                           })
                    End If

                    '#GT08052025: iterar la lista de operadores-recepcion para crear el objeto anonimo para Json
                    If pListOperadores IsNot Nothing AndAlso pListOperadores.Count > 0 Then

                        '#GT19052025: obtener operador-recepcion
                        For Each pOperador In pListOperadores

                            reOperadorList.Add(New With {
                                                                                  .idOperadorRec = pOperador.IdOperadorRec,
                                                                                  .idRecepcionEnc = pOperador.IdRecepcionEnc,
                                                                                  .idOperadorBodega = pOperador.IdOperadorBodega,
                                                                                  .user_Agr = pOperador.User_agr,
                                                                                  .fec_Agr = pOperador.Fec_agr,
                                                                                  .user_Mod = pOperador.User_mod,
                                                                                  .fec_Mod = pOperador.Fec_mod
                                                       })


                            '#GT19052025: obtener operador-bodega
                            Dim pOperadorBodega As New clsBeOperador_bodega()
                            pOperadorBodega = clsLnOperador_bodega.GetSingle_By_IdOperadorBodega(pOperador.IdOperadorBodega,
                                                                                                             clsTransaccion.lConnection,
                                                                                                             clsTransaccion.lTransaction)
                            If pOperadorBodega IsNot Nothing Then
                                operadorBodegaList.Add(New With {
                                                                            .IdOperadorBodega = pOperadorBodega.IdOperadorBodega,
                                                                            .IdOperador = pOperadorBodega.IdOperador,
                                                                            .IdBodega = pOperadorBodega.IdBodega,
                                                                            .activo = pOperadorBodega.Activo,
                                                                            .user_agr = pOperadorBodega.User_agr,
                                                                            .fec_agr = pOperadorBodega.Fec_agr,
                                                                            .user_mod = pOperadorBodega.User_mod,
                                                                            .fec_mod = pOperadorBodega.Fec_mod
                                                                           })

                            End If

                            '#GT19052025: obtener operador
                            Dim pBeOperador As New clsBeOperador()
                            pBeOperador = clsLnOperador.Get_Single_By_IdOperador(pOperadorBodega.IdOperador,
                                                                                             clsTransaccion.lConnection,
                                                                                             clsTransaccion.lTransaction)
                            If pBeOperador IsNot Nothing Then
                                operadorList.Add(New With {
                                                                        .IdOperador = pBeOperador.IdOperador,
                                                                        .IdEmpresa = pBeOperador.IdEmpresa,
                                                                        .IdRolOperador = pBeOperador.IdRolOperador,
                                                                        .IdJornada = pBeOperador.IdJornada,
                                                                        .nombres = pBeOperador.Nombres,
                                                                        .apellidos = pBeOperador.Apellidos,
                                                                        .direccion = pBeOperador.Direccion,
                                                                        .telefono = pBeOperador.Telefono,
                                                                        .codigo = pBeOperador.Codigo,
                                                                        .clave = pBeOperador.Clave,
                                                                        .activo = pBeOperador.Activo,
                                                                        .user_agr = pBeOperador.User_agr,
                                                                        .fec_agr = pBeOperador.Fec_agr,
                                                                        .user_mod = pBeOperador.User_mod,
                                                                        .fec_mod = pBeOperador.Fec_mod,
                                                                        .costo_hora = pBeOperador.Costo_hora,
                                                                        .usa_hh = pBeOperador.Usa_hh,
                                                                        .foto = pBeOperador.Foto,
                                                                        .recibe = pBeOperador.Recibe,
                                                                        .ubica = pBeOperador.Ubica,
                                                                        .transporta = pBeOperador.Transporta,
                                                                        .pickea = pBeOperador.Pickea,
                                                                        .verifica = pBeOperador.Verifica,
                                                                        .montacarga = pBeOperador.Montacarga,
                                                                        .sistema = pBeOperador.Sistema
                                                                     })
                            End If

                        Next


                    Else
                        '#GT08052025: podrian no existir operadores asociados si fue recepción en bof

                        reOperadorList.Add(New With {
                                                               .idOperadorRec = 0,
                                                               .idRecepcionEnc = 0,
                                                               .idOperadorBodega = 0,
                                                               .user_Agr = "",
                                                               .fec_Agr = Date.MinValue,
                                                               .user_Mod = "",
                                                               .fec_Mod = Date.MinValue
                                                           })


                        operadorBodegaList.Add(New With {
                                                                         .IdOperadorBodega = 0,
                                                                         .IdOperador = 0,
                                                                         .IdBodega = 0,
                                                                         .activo = False,
                                                                         .user_agr = "",
                                                                         .fec_agr = Now.Date,
                                                                         .user_mod = "",
                                                                         .fec_mod = Now.Date
                                                                        })


                        operadorList.Add(New With {
                                                                        .IdOperador = 0,
                                                                        .IdEmpresa = 0,
                                                                        .IdRolOperador = 0,
                                                                        .IdJornada = 0,
                                                                        .nombres = "",
                                                                        .apellidos = "",
                                                                        .direccion = "",
                                                                        .telefono = "",
                                                                        .codigo = "",
                                                                        .clave = "",
                                                                        .activo = False,
                                                                        .user_agr = "",
                                                                        .fec_agr = Now.Date,
                                                                        .user_mod = "",
                                                                        .fec_mod = Now.Date,
                                                                        .costo_hora = 0,
                                                                        .usa_hh = False,
                                                                        .foto = False,
                                                                        .recibe = False,
                                                                        .ubica = False,
                                                                        .transporta = False,
                                                                        .pickea = False,
                                                                        .verifica = False,
                                                                        .montacarga = False,
                                                                        .sistema = False
                                                                     })


                    End If

                    '#GT09052025: iterar la lista de facturas para crear el objeto anonimo para Json
                    If pListFacturasRe IsNot Nothing AndAlso pListFacturasRe.Count > 0 Then
                        For Each pFactura In pListFacturasRe
                            facturasList.Add(New With {
                                                                                  .idFacturaRecepcion = pFactura.IdFacturaRecepcion,
                                                                                  .idRecepcionEnc = pFactura.IdRecepcionEnc,
                                                                                  .orden = pFactura.Orden,
                                                                                  .noFactura = pFactura.NoFactura,
                                                                                  .observacion = pFactura.Observacion,
                                                                                  .fec_Agr = pFactura.Fec_agr,
                                                                                  .user_Agr = pFactura.User_agr,
                                                                                  .fec_Mod = pFactura.Fec_mod,
                                                                                  .user_Mod = pFactura.User_mod,
                                                                                  .completa = pFactura.Completa
                                                    })
                        Next
                    Else
                        facturasList.Add(New With {
                                                                                    .idFacturaRecepcion = 0,
                                                                                    .idRecepcionEnc = 0,
                                                                                    .orden = 0,
                                                                                    .noFactura = "",
                                                                                    .observacion = "",
                                                                                    .fec_Agr = Date.MinValue,
                                                                                    .user_Agr = "",
                                                                                    .fec_Mod = Date.MinValue,
                                                                                    .user_Mod = "",
                                                                                    .completa = False
                                                        })
                    End If

                    '#GT09052025: iterar la lista de imagenes para crear el objeto anonimo para Json
                    If pListaImgRe IsNot Nothing AndAlso pListaImgRe.Count > 0 Then
                        For Each pImagen In pListaImgRe

                            imgList.Add(New With {
                                                                                         .idImagen = pImagen.IdImagen,
                                                                                         .idRecepcionEnc = pImagen.IdRecepcionEnc,
                                                                                         .imagen = pImagen.Imagen,
                                                                                         .user_Agr = pImagen.User_agr,
                                                                                         .fec_Agr = pImagen.Fec_agr,
                                                                                         .observacion = pImagen.Observacion
                                                           })
                        Next
                    Else
                        imgList.Add(New With {
                                                                                        .idImagen = 0,
                                                                                        .idRecepcionEnc = 0,
                                                                                        .imagen = "",
                                                                                        .user_Agr = "",
                                                                                        .fec_Agr = Date.MinValue,
                                                                                        .observacion = ""
                                                    })
                    End If

                    '#GT12052025: crear el objeto anonimo del tipo transaccion en la recepción
                    If pTrans_re_tr IsNot Nothing Then

                        Dim trans_re_tr As New With {
                                                            .IdTipoTransaccion = pTrans_re_tr.IdTipoTransaccion,
                                                            .Descripcion = pTrans_re_tr.Descripcion,
                                                            .Funcionalidad = pTrans_re_tr.Funcionalidad,
                                                            .UsarHH = pTrans_re_tr.UsaHH,
                                                            .DescDev = pTrans_re_tr.DescDev,
                                                            .TipoTrans = pTrans_re_tr.TipoTrans,
                                                            .ConRef = pTrans_re_tr.ConRef,
                                                            .Activo = pTrans_re_tr.Activo
                                        }
                    Else
                        clsHelper.LogMensaje(lblprg, "La tarea de recepción no tiene asociado un tipo de transacción", clsHelper.TipoMensaje.Error_)
                        Return ""
                        'Throw New Exception("La tarea de recepción no tiene asociado un tipo de transacción!")
                    End If

                    '*********************************************************************************************
                    '*********************** Recepción con las propiedades previamente calculadas ****************
                    If pListRecepcionEnc IsNot Nothing AndAlso pListRecepcionEnc.Count > 0 Then
                        For Each recepcion In pListRecepcionEnc
                            recepcionesList.Add(New With {
                                                                .encabezado = New With {
                                                                                    .idRecepcionEnc = recepcion.IdRecepcionEnc,
                                                                                    .idPropietarioBodega = recepcion.PropietarioBodega.IdPropietarioBodega,
                                                                                    .IdMuelle = recepcion.IdMuelle,
                                                                                    .IdUbicacionRecepcion = recepcion.IdUbicacionRecepcion,
                                                                                    .IdTipoTransaccion = recepcion.IdTipoTransaccion,
                                                                                    .fecha_recepcion = recepcion.Fecha_recepcion,
                                                                                    .hora_ini_pc = recepcion.Hora_ini_pc,
                                                                                    .hora_fin_pc = recepcion.Hora_fin_pc,
                                                                                    .muestra_precio = recepcion.Muestra_precio,
                                                                                    .estado = recepcion.Estado,
                                                                                    .user_agr = recepcion.User_agr,
                                                                                    .fec_agr = recepcion.Fec_agr,
                                                                                    .user_mod = recepcion.User_mod,
                                                                                    .fec_mod = recepcion.Fec_mod,
                                                                                    .fecha_tarea = recepcion.Fecha_tarea,
                                                                                    .tomar_fotos = recepcion.Tomar_fotos,
                                                                                    .escanear_rec_ubic = recepcion.Escanear_rec_ubic,
                                                                                    .para_por_codigo = recepcion.Para_por_codigo,
                                                                                    .observacion = recepcion.Observacion,
                                                                                    .firma_piloto = recepcion.Firma_piloto,
                                                                                    .activo = recepcion.Activo,
                                                                                    .NoGuia = recepcion.NoGuia,
                                                                                    .CorreoEnviado = recepcion.CorreoEnviado,
                                                                                    .Revision_Inconsistencia = recepcion.Revision_Inconsistencia,
                                                                                    .bloqueada = recepcion.bloqueada,
                                                                                    .bloqueada_por = recepcion.bloqueada_por,
                                                                                    .idusuariobloqueo = recepcion.IdUsuarioBloqueo,
                                                                                    .idmotivoanulacionbodega = recepcion.IdMotivoAnulacionBodega,
                                                                                    .Habilitar_Stock = recepcion.Habilitar_Stock,
                                                                                    .idvehiculo = recepcion.IdVehiculo,
                                                                                    .idpiloto = recepcion.IdPiloto,
                                                                                    .No_Marchamo = recepcion.No_Marchamo,
                                                                                    .mostrar_cantidad_esperada = recepcion.Mostrar_Cantidad_Esperada,
                                                                                    .IdBodega = recepcion.IdBodega,
                                                                                    .carta_cupo = recepcion.Carta_Cupo,
                                                                                    .IdEstado_defecto_recepcion = recepcion.IdEstado_Defecto_Recepcion,
                                                                                    .no_contenedor = recepcion.No_Contenedor
                                                              },
                                                              .detalle = recepcion.Detalle,
                                                              .ocsRelacionadas = reOcList,
                                                              .operadoresRec = reOperadorList,
                                                              .operadores = operadorList,
                                                              .operadorBodega = operadorBodegaList,
                                                              .facturas = facturasList,
                                                              .imagenes = imgList
                                                    })
                        Next

                    Else
                        clsHelper.LogMensaje(lblprg, "El ingreso no recepción asociada.", clsHelper.TipoMensaje.Error_)
                        Return ""
                        'Throw New Exception("El ingreso no recepción asociada.")
                    End If


                    If pTrans_re_enc.Detalle Is Nothing OrElse pTrans_re_enc.Detalle.Count = 0 Then
                        clsHelper.LogMensaje(lblprg, "La recepción no tiene detalle.", clsHelper.TipoMensaje.Error_)
                        Return ""
                    End If

                    '********************** Stock_Rec, Stock y movimientos con las propiedades ***********************************
                    For Each re_det As clsBeTrans_re_det In pTrans_re_enc.Detalle

                        pTrans_movimientos = New clsBeTrans_movimientos()
                        pStock_Rec = New clsBeStock_rec()
                        pStock = New clsBeStock()
                        pListBodega_Ubicacion = New List(Of clsBeBodega_ubicacion)()
                        pListBodega_Area = New List(Of clsBeBodega_area)()
                        pListBodega_Sector = New List(Of clsBeBodega_sector)()
                        pListBodega_Tramo = New List(Of clsBeBodega_tramo)()

                        If re_det.IdRecepcionEnc = 282 AndAlso re_det.IdRecepcionDet = 5 Then
                            Debug.Write("aqui")
                        End If

                        pTrans_movimientos = clsLnTrans_movimientos.GetSingle_By_IdRecepcionEnc_And_IdRecepcionDet(re_det.IdRecepcionEnc,
                                                                                                                               re_det.IdRecepcionDet,
                        '#GT06102025: no hay asociación por recepcion enc/det, validamos solo por lic_plate
                        If pTrans_movimientos Is Nothing Then
                            pTrans_movimientos = clsLnTrans_movimientos.GetSingle_By_LicPlate(re_det.IdRecepcionEnc, re_det.Lic_plate,
                                                                                                                    clsTransaccion.lConnection,
                                                                                                                    clsTransaccion.lTransaction)
                        End If

                        pStock_Rec = clsLnStock_rec.GetSingle_Stock_By_IdRecepcionEnc_And_IdRecpecionDet(re_det.IdRecepcionEnc,
                                                                                                                     re_det.IdRecepcionDet,
                                                                                                                     re_det.Lic_plate,
                                                                                                                     clsTransaccion.lConnection,
                                                                                                                     clsTransaccion.lTransaction)

                        pStock = clsLnStock.GetSingle_By_IdRecepcionEnc_And_IdRecepcionDet(re_det.IdRecepcionEnc,
                                                                                                       re_det.IdRecepcionDet,
                                                                                                       clsTransaccion.lConnection,
                                                                                                       clsTransaccion.lTransaction)

                        '#GT14012025: añadir bodega_area, bodega_sector y bodega_ubicacion
                        'esto para insertar stock con ubicaciones añadidas on premise, no existentes en portal.
                        pListBodega_Ubicacion = clsLnBodega_ubicacion.Get_All_By_IdUbicacion(pStock.IdUbicacion, clsTransaccion.lConnection,
                                                                                                                clsTransaccion.lTransaction)

                        If pListBodega_Ubicacion IsNot Nothing Then

                            If pListBodega_Sector Is Nothing Then pListBodega_Sector = New List(Of clsBeBodega_sector)()
                            If pListBodega_Area Is Nothing Then pListBodega_Area = New List(Of clsBeBodega_area)()
                            If pListBodega_Tramo Is Nothing Then pListBodega_Tramo = New List(Of clsBeBodega_tramo)()

                            ' Evita repetir consulta de sector por (IdArea|IdSector)
                            Dim SectoresProcesados As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)

                            ' Evita repetir consulta de área por IdArea
                            Dim areasProcesadas As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)

                            ' Evita repetir consulta de tramo
                            Dim TramosProcesados As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)


                            For Each ubicacion In pListBodega_Ubicacion

                                ' 1) ÁREA (una vez por IdArea)
                                Dim keyArea As String = ubicacion.IdArea.ToString()
                                If areasProcesadas.Add(keyArea) Then
                                    Dim Areas = clsLnBodega_area.Get_All_By_IdArea(ubicacion.IdArea,
                                                           clsTransaccion.lConnection,
                                                           clsTransaccion.lTransaction)
                                    If Areas IsNot Nothing Then
                                        pListBodega_Area.AddRange(Areas)
                                    End If
                                End If

                                ' 2) SECTOR (una vez por IdArea|IdSector)
                                Dim keyCombo As String = $"{ubicacion.IdArea}|{ubicacion.IdSector}"
                                If SectoresProcesados.Add(keyCombo) Then
                                    Dim sectores = clsLnBodega_sector.Get_All_By_IdArea_And_IdSector(ubicacion.IdArea, ubicacion.IdSector,
                                                                            clsTransaccion.lConnection,
                                                                            clsTransaccion.lTransaction)
                                    If sectores IsNot Nothing Then
                                        pListBodega_Sector.AddRange(sectores)
                                    End If
                                End If

                                ' 3) Tramos
                                Dim keyTramo As String = ubicacion.IdTramo.ToString()
                                If TramosProcesados.Add(keyTramo) Then
                                    Dim tramos = clsLnBodega_tramo.Get_All_Tramos_By_IdTramo(ubicacion.IdTramo,
                                                                                             clsTransaccion.lConnection,
                                                                                             clsTransaccion.lTransaction)

                                    If tramos IsNot Nothing Then
                                        pListBodega_Tramo.AddRange(tramos)
                                    End If
                                End If

                            Next

                        End If

                        If pTrans_movimientos IsNot Nothing Then
                            trans_movimientosList.Add(New With {
                                                                            .IdMovimiento = pTrans_movimientos.IdMovimiento,
                                                                            .IdEmpresa = pTrans_movimientos.IdEmpresa,
                                                                            .IdBodegaOrigen = pTrans_movimientos.IdBodegaOrigen,
                                                                            .IdTransaccion = pTrans_movimientos.IdTransaccion,
                                                                            .IdPropietarioBodega = pTrans_movimientos.IdPropietarioBodega,
                                                                            .IdProductoBodega = pTrans_movimientos.IdProductoBodega,
                                                                            .IdUbicacionOrigen = pTrans_movimientos.IdUbicacionOrigen,
                                                                            .IdUbicacionDestino = pTrans_movimientos.IdUbicacionDestino,
                                                                            .IdPresentacion = pTrans_movimientos.IdPresentacion,
                                                                            .IdEstadoOrigen = pTrans_movimientos.IdEstadoOrigen,
                                                                            .IdEstadoDestino = pTrans_movimientos.IdEstadoDestino,
                                                                            .IdUnidadMedida = pTrans_movimientos.IdUnidadMedida,
                                                                            .IdTipoTarea = pTrans_movimientos.IdTipoTarea,
                                                                            .IdBodegaDestino = pTrans_movimientos.IdBodegaDestino,
                                                                            .IdRecepcion = pTrans_movimientos.IdRecepcion,
                                                                            .cantidad = pTrans_movimientos.Cantidad,
                                                                            .serie = pTrans_movimientos.Serie,
                                                                            .peso = pTrans_movimientos.Peso,
                                                                            .lote = pTrans_movimientos.Lote,
                                                                            .fecha_vence = pTrans_movimientos.Fecha_vence,
                                                                            .fecha = pTrans_movimientos.Fecha,
                                                                            .barra_pallet = pTrans_movimientos.Barra_pallet,
                                                                            .hora_ini = pTrans_movimientos.Hora_ini,
                                                                            .hora_fin = pTrans_movimientos.Hora_fin,
                                                                            .fecha_agr = pTrans_movimientos.Fecha_agr,
                                                                            .usuario_agr = pTrans_movimientos.Usuario_agr,
                                                                            .cantidad_hist = pTrans_movimientos.Cantidad_hist,
                                                                            .peso_hist = pTrans_movimientos.Peso_hist,
                                                                            .lic_plate = pTrans_movimientos.Lic_plate,
                                                                            .IdOperadorBodega = pTrans_movimientos.IdOperadorBodega,
                                                                            .IdRecepcionDet = pTrans_movimientos.IdRecepcionDet,
                                                                            .IdPedidoEnc = pTrans_movimientos.IdPedidoEnc,
                                                                            .IdPedidoDet = pTrans_movimientos.IdPedidoDet,
                                                                            .IdDespachoEnc = pTrans_movimientos.IdDespachoEnc,
                                                                            .IdDespachoDet = pTrans_movimientos.IdDespachoDet
                                                                        })

                        Else
                            clsHelper.LogMensaje(lblprg, "No se obtuvo el movimiento asociado a la recepción " & re_det.IdRecepcionEnc & " detalle: " & re_det.IdRecepcionDet, clsHelper.TipoMensaje.Error_)
                            Return ""
                            'Throw New Exception("No se obtuvo el movimiento asociado a la recepción.")
                        End If

                        If pStock_Rec IsNot Nothing Then
                            stock_recList.Add(New With {
                                                                .IdStockRec = pStock_Rec.IdStockRec,
                                                                .IdPropietarioBodega = pStock_Rec.IdPropietarioBodega,
                                                                .IdProductoBodega = pStock_Rec.IdProductoBodega,
                                                                .IdProductoEstado = pStock_Rec.IdProductoEstado,
                                                                .IdPresentacion = pStock_Rec.IdPresentacion,
                                                                .IdUnidadMedida = pStock_Rec.IdUnidadMedida,
                                                                .IdUbicacion = pStock_Rec.IdUbicacion,
                                                                .IdUbicacion_anterior = pStock_Rec.IdUbicacion_anterior,
                                                                .IdRecepcionEnc = pStock_Rec.IdRecepcionEnc,
                                                                .IdRecepcionDet = pStock_Rec.IdRecepcionDet,
                                                                .IdPedidoEnc = pStock_Rec.IdPedidoEnc,
                                                                .IdPickingEnc = pStock_Rec.IdPickingEnc,
                                                                .IdDespachoEnc = pStock_Rec.IdDespachoEnc,
                                                                .lote = pStock_Rec.Lote,
                                                                .lic_plate = pStock_Rec.Lic_plate,
                                                                .serial = pStock_Rec.Serial,
                                                                .cantidad = pStock_Rec.Cantidad,
                                                                .fecha_ingreso = pStock_Rec.Fecha_Ingreso,
                                                                .fecha_vence = pStock_Rec.Fecha_vence,
                                                                .uds_lic_plate = pStock_Rec.Uds_lic_plate,
                                                                .no_bulto = pStock_Rec.No_bulto,
                                                                .fecha_manufactura = pStock_Rec.Fecha_Manufactura,
                                                                .añada = pStock_Rec.Añada,
                                                                .user_agr = pStock_Rec.User_agr,
                                                                .fec_agr = pStock_Rec.Fec_agr,
                                                                .user_mod = pStock_Rec.User_mod,
                                                                .fec_mod = pStock_Rec.Fec_mod,
                                                                .activo = pStock_Rec.Activo,
                                                                .peso = pStock_Rec.Peso,
                                                                .temperatura = pStock_Rec.Temperatura,
                                                                .regularizado = pStock_Rec.Regularizado,
                                                                .fecha_regularizacion = pStock_Rec.Fecha_regularizacion,
                                                                .no_linea = pStock_Rec.No_linea,
                                                                .atributo_variante_1 = pStock_Rec.Atributo_Variante_1,
                                                                .impreso = pStock_Rec.Impreso,
                                                                .IdBodega = pStock_Rec.IdBodega,
                                                                .pallet_no_estandar = pStock_Rec.Pallet_No_Estandar
                                                      })
                        Else
                            clsHelper.LogMensaje(lblprg, "No se obtuvo el stock_rec asociado a la recepción " & re_det.IdRecepcionEnc & " detalle: " & re_det.IdRecepcionDet, clsHelper.TipoMensaje.Error_)
                            Return ""
                            'Throw New Exception("No se obtuvo el stock_rec asociado a la recepción.")
                        End If

                        Dim areasOut = If(pListBodega_Area, New List(Of clsBeBodega_area)())
                        Dim sectoresOut = If(pListBodega_Sector, New List(Of clsBeBodega_sector)())
                        Dim ubicacionesOut = If(pListBodega_Ubicacion, New List(Of clsBeBodega_ubicacion)())
                        Dim tramosOut = If(pListBodega_Tramo, New List(Of clsBeBodega_tramo)())

                        If pStock IsNot Nothing Then

                            stockList.Add(New With {
                                                             .IdStock = pStock.IdStock,
                                                             .IdPropietarioBodega = pStock.IdPropietarioBodega,
                                                             .IdProductoBodega = pStock.IdProductoBodega,
                                                             .IdProductoEstado = pStock.IdProductoEstado,
                                                             .IdPresentacion = pStock.IdPresentacion,
                                                             .IdUnidadMedida = pStock.IdUnidadMedida,
                                                             .IdUbicacion = pStock.IdUbicacion,
                                                             .IdUbicacion_anterior = pStock.IdUbicacion_anterior,
                                                             .IdRecepcionEnc = pStock.IdRecepcionEnc,
                                                             .IdRecepcionDet = pStock.IdRecepcionDet,
                                                             .IdPedidoEnc = pStock.IdPedidoEnc,
                                                             .IdPickingEnc = pStock.IdPickingEnc,
                                                             .IdDespachoEnc = pStock.IdDespachoEnc,
                                                             .lote = pStock.Lote,
                                                             .lic_plate = pStock.Lic_plate,
                                                             .serial = pStock.Serial,
                                                             .cantidad = pStock.Cantidad,
                                                             .fecha_ingreso = pStock.Fecha_Ingreso,
                                                             .fecha_vence = pStock.Fecha_vence,
                                                             .uds_lic_plate = pStock.Uds_lic_plate,
                                                             .no_bulto = pStock.No_bulto,
                                                             .fecha_manufactura = pStock.Fecha_Manufactura,
                                                             .añada = pStock.Añada,
                                                             .user_agr = pStock.User_agr,
                                                             .fec_agr = pStock.Fec_agr,
                                                             .user_mod = pStock.User_mod,
                                                             .fec_mod = pStock.Fec_mod,
                                                             .activo = pStock.Activo,
                                                             .peso = pStock.Peso,
                                                             .temperatura = pStock.Temperatura,
                                                             .atributo_variante_1 = pStock.Atributo_Variante_1,
                                                             .IdBodega = pStock.IdBodega,
                                                             .pallet_no_estandar = pStock.Pallet_No_Estandar,
                                                             .Bodega_Areas = areasOut,
                                                             .Bodega_Sectores = sectoresOut,
                                                             .Bodega_Ubicaciones = ubicacionesOut,
                                                             .Bodega_Tramos = tramosOut
                                                         })
                        Else
                            clsHelper.LogMensaje(lblprg, "Omitiendo el stock asociado a recepción " & re_det.IdRecepcionEnc & " detalle: " & re_det.IdRecepcionDet, clsHelper.TipoMensaje.Info)
                            'Throw New Exception("No se obtuvo el stock asociado a la recepción.")

                            stockList.Add(New With {
                                                            .IdStock = 0,
                                                            .IdPropietarioBodega = 0,
                                                            .IdProductoBodega = 0,
                                                            .IdProductoEstado = 0,
                                                            .IdPresentacion = 0,
                                                            .IdUnidadMedida = 0,
                                                            .IdUbicacion = 0,
                                                            .IdUbicacion_anterior = 0,
                                                            .IdRecepcionEnc = 0,
                                                            .IdRecepcionDet = 0,
                                                            .IdPedidoEnc = 0,
                                                            .IdPickingEnc = 0,
                                                            .IdDespachoEnc = 0,
                                                            .lote = 0,
                                                            .lic_plate = 0,
                                                            .serial = 0,
                                                            .cantidad = 0,
                                                            .fecha_ingreso = New Date(1900, 1, 1),
                                                            .fecha_vence = New Date(1900, 1, 1),
                                                            .uds_lic_plate = "",
                                                            .no_bulto = 0,
                                                            .fecha_manufactura = New Date(1900, 1, 1),
                                                            .añada = 0,
                                                            .user_agr = "",
                                                            .fec_agr = New Date(1900, 1, 1),
                                                            .user_mod = "",
                                                            .fec_mod = New Date(1900, 1, 1),
                                                            .activo = False,
                                                            .peso = 0,
                                                            .temperatura = 0.0,
                                                            .atributo_variante_1 = "",
                                                            .IdBodega = 0,
                                                            .pallet_no_estandar = False,
                                                            .Bodega_Areas = areasOut,
                                                            .Bodega_Sectores = sectoresOut,
                                                            .Bodega_Ubicaciones = ubicacionesOut,
                                                            .Bodega_Tramos = tramosOut
                                                        })

                        End If

                    Next

                Next

            Else
                clsHelper.LogMensaje(lblprg, "No existe el registro entre el ingreso y la recepción ", clsHelper.TipoMensaje.Error_)
                Return ""
                'Throw New Exception("No existe el registro entre el ingreso y la recepción.")
            End If


            '#GT09052025: llenar la OC y añadir los objetos de detalle, poliza, tipo ingreso, recepciones [encabezado, detalle, OcRelacionada, operadores, facturas, imagenes]
            PayLoadOCList.Add(New With {
                                             .encabezado = New With {
                                                              .IdOrdenCompraEnc = pOCEnc.IdOrdenCompraEnc,
                                                              .IdPropietarioBodega = pOCEnc.IdPropietarioBodega,
                                                              .IdProveedorBodega = pOCEnc.IdProveedorBodega,
                                                              .IdTipoIngresoOC = pOCEnc.IdTipoIngresoOC,
                                                              .IdEstadoOC = pOCEnc.IdEstadoOC,
                                                              .IdMotivoDevolucion = pOCEnc.IdMotivoDevolucion,
                                                              .Fecha_Creacion = pOCEnc.Fecha_Creacion,
                                                              .Hora_Creacion = pOCEnc.Hora_Creacion,
                                                              .No_Documento = pOCEnc.No_Documento,
                                                              .User_Agr = pOCEnc.User_Agr,
                                                              .Fec_Agr = pOCEnc.Fec_Agr,
                                                              .User_Mod = pOCEnc.User_Mod,
                                                              .Fec_Mod = pOCEnc.Fec_Mod,
                                                              .Procedencia = pOCEnc.Procedencia,
                                                              .No_Marchamo = pOCEnc.No_Marchamo,
                                                              .Referencia = pOCEnc.Referencia,
                                                              .Observacion = pOCEnc.Observacion,
                                                              .Control_Poliza = pOCEnc.Control_Poliza,
                                                              .Activo = pOCEnc.Activo,
                                                              .Fecha_Recepcion = pOCEnc.Fecha_Recepcion,
                                                              .Hora_Inicio_Recepcion = pOCEnc.Hora_Inicio_Recepcion,
                                                              .Hora_Fin_Recepcion = pOCEnc.Hora_Fin_Recepcion,
                                                              .IdMuelleRecepcion = pOCEnc.IdMuelleRecepcion,
                                                              .Programar_Recepcion = pOCEnc.Programar_Recepcion,
                                                              .IdMotivoAnulacionBodega = pOCEnc.IdMotivoAnulacionBodega,
                                                              .Enviado_A_ERP = pOCEnc.Enviado_A_ERP,
                                                              .serie = pOCEnc.Serie,
                                                              .correlativo = pOCEnc.Correlativo,
                                                              .no_ticket_tms = pOCEnc.No_Ticket_TMS,
                                                              .IdNoDocumentoRef = pOCEnc.IdNoDocumentoRef,
                                                              .idacuerdocomercial = pOCEnc.IdAcuerdoComercial,
                                                              .IdOperadorBodegaDefecto = pOCEnc.IdOperadorBodegaDefecto,
                                                              .IdBodega = pOCEnc.IdBodega,
                                                              .no_documento_recepcion_erp = pOCEnc.No_Documento_Recepcion_ERP,
                                                              .no_documento_devolucion = pOCEnc.No_Documento_Devolucion,
                                                              .IdPedidoEncDevolucion = pOCEnc.IdPedidoEncDevolucion,
                                                              .push_to_nav = pOCEnc.Push_To_NAV,
                                                              .no_documento_ubicacion_erp = pOCEnc.No_Documento_Ubicacion_ERP,
                                                              .PutAway_Registrado = pOCEnc.PutAway_Registrado,
                                                              .Codigo_Empresa_ERP = pOCEnc.Codigo_Empresa_ERP
                                                              },
                                              .detalle = ocDetList,
                                              .polizas = If(pOCEnc.Control_Poliza,
                                                              New List(Of clsBeTrans_oc_pol) From {pOCEnc.ObjPoliza},
                                                              New List(Of clsBeTrans_oc_pol) From {
                                                                  New clsBeTrans_oc_pol With {
                                                                                              .IdOrdenCompraPol = 0,
                                                                                              .IdOrdenCompraEnc = 0,
                                                                                              .Bl_No = 0,
                                                                                              .NoPoliza = "",
                                                                                              .Pto_Descarga = "",
                                                                                              .Viaje_no = "",
                                                                                              .Buque_no = "",
                                                                                              .Remitente = "",
                                                                                              .Fecha_abordaje = Now.Date,
                                                                                              .Destino = "",
                                                                                              .Dir_destino = "",
                                                                                              .Descripcion = "",
                                                                                              .Po_number = "",
                                                                                              .Cantidad = 0,
                                                                                              .Piezas = 0,
                                                                                              .Total_kgs = 0,
                                                                                              .Cbm = 0,
                                                                                              .Dua = "",
                                                                                              .Fecha_poliza = Now.Date,
                                                                                              .Pais_procede = "",
                                                                                              .Tipo_cambio = 0.00,
                                                                                              .Total_valoraduana = 0,
                                                                                              .Total_lineas = 0,
                                                                                              .Total_bultos = 0,
                                                                                              .Total_bultos_Peso_Bruto = 0,
                                                                                              .Total_usd = 0,
                                                                                              .Total_flete = 0,
                                                                                              .Total_seguro = 0,
                                                                                              .User_agr = "",
                                                                                              .Fec_agr = Now.Date,
                                                                                              .User_mod = "",
                                                                                              .Fec_mod = Now.Date,
                                                                                              .codigo_poliza = "",
                                                                                              .ticket = "",
                                                                                              .numero_orden = "",
                                                                                              .fecha_aceptacion = Now.Date,
                                                                                              .fecha_llegada = Now.Date,
                                                                                              .total_otros = 0,
                                                                                              .IdRegimen = 0,
                                                                                              .Total_bultos_Peso_Neto = 0,
                                                                                              .clave_aduana = "",
                                                                                              .nit_imp_exp = "",
                                                                                              .clase = "",
                                                                                              .mod_transporte = "",
                                                                                              .total_liquidar = 0,
                                                                                              .total_general = 0,
                                                                                              .Codigo_Barra = "",
                                                                                              .activo = False,
                                                                                              .IdBodega = 0}
                                                                                            }),
                                              .tipoIngreso = pOCEnc.TipoIngreso,
                                              .recepciones = recepcionesList,
                                              .stockRec = stock_recList,
                                              .stock = stockList,
                                              .movimientos = trans_movimientosList
                                   })

            listPayload.AddRange(PayLoadOCList)

            clsTransaccion.Commit_Transaction()

            Crear_Json = JsonConvert.SerializeObject(listPayload)

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            clsTransaccion.Close_Conection()
        End Try

    End Function


    Public Shared Function GetAll_By_CDC(ByVal pTablaSincronizada As String, ByRef pListOC As List(Of clsBeTrans_oc_enc)) As List(Of clsBeTrans_oc_enc)
        Dim BeLogUltimaSincronizacion As New clsBeLog_sincronizacion_nube()
        Dim clsTransaccion As New clsTransaccion()
        Try

            clsTransaccion.Begin_Transaction()

            BeLogUltimaSincronizacion = New clsBeLog_sincronizacion_nube()
            BeLogUltimaSincronizacion = clsLnLog_sincronizacion_nube.GetLastSync(pTablaSincronizada, clsTransaccion.lConnection, clsTransaccion.lTransaction)
            If BeLogUltimaSincronizacion IsNot Nothing Then
                pListOC = clsLnTrans_oc_enc.GetAll_By_CDC(BeLogUltimaSincronizacion.Fecha_sincronizacion, clsTransaccion.lConnection, clsTransaccion.lTransaction)
            End If

            clsTransaccion.Commit_Transaction()

            Return pListOC

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            clsTransaccion.Close_Conection()
        End Try

    End Function

    Public Shared Function ImportarIngreso(ByRef lblprg As RichTextBox, ByVal pTablaSincronizada As String) As List(Of Object)

        Dim listPayload As New List(Of Object)
        Dim reOcList As New List(Of Object)
        Dim reOperadorList As New List(Of Object)
        Dim operadorBodegaList As New List(Of Object)
        Dim operadorList As New List(Of Object)
        Dim facturasList As New List(Of Object)
        Dim imgList As New List(Of Object)
        Dim recepcionesList As New List(Of Object)
        Dim ocList As New List(Of Object)
        Dim stock_recList As New List(Of Object)
        Dim ocDetList As New List(Of Object)
        Dim ocDet As New Object
        Dim stockList As New List(Of Object)
        Dim trans_movimientosList As New List(Of Object)

        Dim pListOC As New List(Of clsBeTrans_oc_enc)()
        Dim pListRecepcionEnc As New List(Of clsBeTrans_re_enc)()
        Dim ListReOC As New List(Of clsBeTrans_re_oc)()
        Dim pTrans_re_enc As New clsBeTrans_re_enc
        Dim pListOperadores As New List(Of clsBeTrans_re_op)()
        Dim pListFacturasRe As New List(Of clsBeTrans_re_fact)()
        Dim pListaImgRe As New List(Of clsBeTrans_re_img)()
        Dim pTrans_re_tr As New clsBeTrans_re_tr()
        Dim pTrans_movimientos As New clsBeTrans_movimientos()
        Dim pStock_Rec As New clsBeStock_rec()
        Dim pStock As New clsBeStock()
        Dim pListOperadorBodega As New List(Of clsBeOperador_bodega)()
        Dim pListOperador As New List(Of clsBeOperador)()
        Dim BeLogUltimaSincronizacion As New clsBeLog_sincronizacion_nube()
        Dim clsTransaccion As New clsTransaccion()
        Dim Contador As Integer = 0


        Try

            clsTransaccion.Begin_Transaction()

            '#GT29052025: obtener ultima sincronizacion
            BeLogUltimaSincronizacion = New clsBeLog_sincronizacion_nube()
            BeLogUltimaSincronizacion = clsLnLog_sincronizacion_nube.GetLastSync(pTablaSincronizada, clsTransaccion.lConnection, clsTransaccion.lTransaction)

            '#GT07052025: listar todas las OC cerradas y que se encuentren activas según ultma sincro
            If BeLogUltimaSincronizacion IsNot Nothing Then

                pListOC = clsLnTrans_oc_enc.GetAll_By_CDC(BeLogUltimaSincronizacion.Fecha_sincronizacion, clsTransaccion.lConnection, clsTransaccion.lTransaction)
            Else
                clsHelper.LogMensaje(lblprg, "No se encontró sincronización previa para '" & pTablaSincronizada & "'. Se realizará sincronización completa desde fecha base.", clsHelper.TipoMensaje.Advertencia)

            End If


            If pListOC IsNot Nothing AndAlso pListOC.Count > 0 Then

                listPayload = New List(Of Object)

                clsHelper.LogMensaje(lblprg, "Se encontrarón " & pListOC.Count & " ingresos.", clsHelper.TipoMensaje.Info)

                For Each OC In pListOC

                    If OC IsNot Nothing Then

                        Contador += 1

                        clsHelper.LogMensaje(lblprg, "Iterando Registro: " & Contador & "/" & pListOC.Count & " Ingreso: " & OC.IdOrdenCompraEnc, clsHelper.TipoMensaje.Info)

                        reOcList = New List(Of Object)()
                        reOperadorList = New List(Of Object)()
                        operadorBodegaList = New List(Of Object)
                        operadorList = New List(Of Object)
                        facturasList = New List(Of Object)()
                        imgList = New List(Of Object)()
                        recepcionesList = New List(Of Object)()
                        ocList = New List(Of Object)
                        stock_recList = New List(Of Object)
                        stockList = New List(Of Object)
                        ocDetList = New List(Of Object)
                        trans_movimientosList = New List(Of Object)

                        OC.DetalleOC = New List(Of clsBeTrans_oc_det)()
                        OC.ObjPoliza = New clsBeTrans_oc_pol()
                        OC.TipoIngreso = New clsBeTrans_oc_ti()
                        pListRecepcionEnc = New List(Of clsBeTrans_re_enc)()
                        ListReOC = New List(Of clsBeTrans_re_oc)
                        Dim productobodega As New clsBeProducto_bodega()

                        '#GT13052025: al obtener el detalle de la OC, agregar la propiedad de producto_bodega por linea
                        OC.DetalleOC = clsLnTrans_oc_det.Get_Detalle_OC_By_IdOrdenCompraEnc(OC.IdOrdenCompraEnc, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                        If OC.DetalleOC IsNot Nothing AndAlso OC.DetalleOC.Count > 0 Then

                            For Each oc_det As clsBeTrans_oc_det In OC.DetalleOC

                                productobodega = New clsBeProducto_bodega()
                                'productobodega = clsLnProducto_bodega.GetSingle(oc_det.IdProductoBodega, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                                productobodega.IdProductoBodega = oc_det.IdProductoBodega

                                If clsLnProducto_bodega.Obtener(productobodega, clsTransaccion.lConnection, clsTransaccion.lTransaction) Then

                                    ocDet = New Object()
                                    ocDet = New With {
                                                    .IdOrdenCompraEnc = oc_det.IdOrdenCompraEnc,
                                                    .IdOrdenCompraDet = oc_det.IdOrdenCompraDet,
                                                    .IdProductoBodega = oc_det.IdProductoBodega,
                                                    .IdArancel = oc_det.IdArancel,
                                                    .IdPresentacion = oc_det.IdPresentacion,
                                                    .IdUnidadMedidaBasica = oc_det.IdUnidadMedidaBasica,
                                                    .IdMotivoDevolucion = oc_det.IdMotivoDevolucion,
                                                    .No_Linea = oc_det.No_Linea,
                                                    .nombre_producto = oc_det.Nombre_producto,
                                                    .nombre_presentacion = oc_det.Nombre_presentacion,
                                                    .nombre_arancel = oc_det.Nombre_arancel,
                                                    .porcentaje_arancel = oc_det.Porcentaje_arancel,
                                                    .nombre_unidad_medida_basica = oc_det.Nombre_unidad_medida_basica,
                                                    .cantidad = oc_det.Cantidad,
                                                    .cantidad_recibida = oc_det.Cantidad_recibida,
                                                    .costo = oc_det.Costo,
                                                    .total_linea = oc_det.Total_linea,
                                                    .user_agr = oc_det.User_agr,
                                                    .fec_agr = oc_det.Fec_agr,
                                                    .user_mod = oc_det.User_mod,
                                                    .fec_mod = oc_det.Fec_mod,
                                                    .activo = oc_det.Activo,
                                                    .peso = oc_det.Peso,
                                                    .peso_recibido = oc_det.Peso_Recibido,
                                                    .atributo_variante_1 = oc_det.Atributo_variante_1,
                                                    .codigo_producto = oc_det.Codigo_Producto,
                                                    .valor_aduana = oc_det.valor_aduana,
                                                    .valor_fob = oc_det.valor_fob,
                                                    .valor_iva = oc_det.valor_iva,
                                                    .valor_dai = oc_det.valor_dai,
                                                    .valor_seguro = oc_det.valor_seguro,
                                                    .valor_flete = oc_det.valor_flete,
                                                    .peso_neto = oc_det.Peso_Neto,
                                                    .peso_bruto = oc_det.Peso_Bruto,
                                                    .IdPropietarioBodega = oc_det.IdPropietarioBodega,
                                                    .nombre_propietario = oc_det.Nombre_Propietario,
                                                    .IdOrdenCompraDetPadre = oc_det.IdOrdenCompraDetPadre,
                                                    .IdEmbarcador = oc_det.IdEmbarcador,
                                                    .productoBodega = New With {
                                                                                .idProductoBodega = productobodega.IdProductoBodega,
                                                                                .idProducto = productobodega.IdProducto,
                                                                                .idBodega = productobodega.IdBodega,
                                                                                .activo = productobodega.Activo,
                                                                                .sistema = productobodega.Sistema,
                                                                                .user_Agr = productobodega.User_agr,
                                                                                .fec_Agr = productobodega.Fec_agr,
                                                                                .user_Mod = productobodega.User_mod,
                                                                                .fec_Mod = productobodega.Fec_mod
                                                                                }
                                                    }

                                Else
                                    Throw New Exception("Por un error desconocido, no se pudo obtener el producto_bodega.")
                                End If

                                ocDetList.Add(ocDet)

                            Next
                        Else
                            clsHelper.LogMensaje(lblprg, "El ingreso no tiene detalle.", clsHelper.TipoMensaje.Error_)
                        End If

                        OC.TipoIngreso = clsLnTrans_oc_ti.GetSingle(OC.IdTipoIngresoOC, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                        If OC.Control_Poliza Then OC.ObjPoliza = clsLnTrans_oc_pol.GetSingle(OC.IdOrdenCompraEnc, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                        ListReOC = clsLnTrans_re_oc.GetListReOC_By_IdOrdenCompraEnc(OC.IdOrdenCompraEnc, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                        If ListReOC IsNot Nothing AndAlso ListReOC.Count > 0 Then

                            For Each pTrans_re_oc As clsBeTrans_re_oc In ListReOC

                                pTrans_re_enc = New clsBeTrans_re_enc()
                                pTrans_re_enc = clsLnTrans_re_enc.GetSingle(pTrans_re_oc.IdRecepcionEnc, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                                pListOperadores = New List(Of clsBeTrans_re_op)()
                                'pListOperadores = clsLnTrans_re_op.Get_All_By_IdRecepcionEnc(pTrans_re_oc.IdRecepcionEnc, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                                pListOperadores = clsLnTrans_re_op.Get_All_Operadores_By_IdRecepcionEnc(pTrans_re_oc.IdRecepcionEnc, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                                'pListFacturasRe = clsLnTrans_re_fact.GetAllByRecepcion(pTrans_re_oc.IdRecepcionEnc, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                                pListFacturasRe = clsLnTrans_re_fact.Get_Detalle_Facturas_By_IdRecepcionEnc(pTrans_re_oc.IdRecepcionEnc, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                                'pListaImgRe = clsLnTrans_re_img.GetByOrdenCompraRecepcion(pTrans_re_oc.IdRecepcionOc, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                                pListaImgRe = clsLnTrans_re_img.Get_Detalle_Imagenes_By_IdRecepcionEnc(pTrans_re_oc.IdRecepcionOc, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                                pTrans_re_tr = clsLnTrans_re_tr_Partial.GetSingle(pTrans_re_enc.IdTipoTransaccion, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                                '#********************* propiedades de la recepción *****************************************
                                If pTrans_re_enc IsNot Nothing Then
                                    pListRecepcionEnc.Add(pTrans_re_enc)
                                    reOcList.Add(New With {
                                                             .idRecepcionOc = pTrans_re_oc.IdRecepcionOc,
                                                             .idRecepcionEnc = pTrans_re_oc.IdRecepcionEnc,
                                                             .idOrdenCompraEnc = pTrans_re_oc.IdOrdenCompraEnc,
                                                             .recepcion_Ciega = pTrans_re_oc.Recepcion_ciega,
                                                             .recepcion_Manual = pTrans_re_oc.Recepcion_manual,
                                                             .no_Docto = pTrans_re_oc.No_docto,
                                                             .hora_Ini_Hh = pTrans_re_oc.Hora_ini_hh,
                                                             .hora_Fin_Hh = pTrans_re_oc.Hora_fin_hh,
                                                             .user_Agr = pTrans_re_oc.User_agr,
                                                             .fec_Agr = pTrans_re_oc.Fec_agr,
                                                             .firma_Operador = pTrans_re_oc.Firma_operador
                                                           })
                                End If

                                '#GT08052025: iterar la lista de operadores-recepcion para crear el objeto anonimo para Json
                                If pListOperadores IsNot Nothing AndAlso pListOperadores.Count > 0 Then

                                    '#GT19052025: obtener operador-recepcion
                                    For Each pOperador In pListOperadores

                                        reOperadorList.Add(New With {
                                                                                  .idOperadorRec = pOperador.IdOperadorRec,
                                                                                  .idRecepcionEnc = pOperador.IdRecepcionEnc,
                                                                                  .idOperadorBodega = pOperador.IdOperadorBodega,
                                                                                  .user_Agr = pOperador.User_agr,
                                                                                  .fec_Agr = pOperador.Fec_agr,
                                                                                  .user_Mod = pOperador.User_mod,
                                                                                  .fec_Mod = pOperador.Fec_mod
                                                       })


                                        '#GT19052025: obtener operador-bodega
                                        Dim pOperadorBodega As New clsBeOperador_bodega()
                                        pOperadorBodega = clsLnOperador_bodega.GetSingle_By_IdOperadorBodega(pOperador.IdOperadorBodega,
                                                                                                             clsTransaccion.lConnection,
                                                                                                             clsTransaccion.lTransaction)
                                        If pOperadorBodega IsNot Nothing Then
                                            operadorBodegaList.Add(New With {
                                                                            .IdOperadorBodega = pOperadorBodega.IdOperadorBodega,
                                                                            .IdOperador = pOperadorBodega.IdOperador,
                                                                            .IdBodega = pOperadorBodega.IdBodega,
                                                                            .activo = pOperadorBodega.Activo,
                                                                            .user_agr = pOperadorBodega.User_agr,
                                                                            .fec_agr = pOperadorBodega.Fec_agr,
                                                                            .user_mod = pOperadorBodega.User_mod,
                                                                            .fec_mod = pOperadorBodega.Fec_mod
                                                                           })

                                        End If

                                        '#GT19052025: obtener operador
                                        Dim pBeOperador As New clsBeOperador()
                                        pBeOperador = clsLnOperador.Get_Single_By_IdOperador(pOperadorBodega.IdOperador,
                                                                                             clsTransaccion.lConnection,
                                                                                             clsTransaccion.lTransaction)
                                        If pBeOperador IsNot Nothing Then
                                            operadorList.Add(New With {
                                                                        .IdOperador = pBeOperador.IdOperador,
                                                                        .IdEmpresa = pBeOperador.IdEmpresa,
                                                                        .IdRolOperador = pBeOperador.IdRolOperador,
                                                                        .IdJornada = pBeOperador.IdJornada,
                                                                        .nombres = pBeOperador.Nombres,
                                                                        .apellidos = pBeOperador.Apellidos,
                                                                        .direccion = pBeOperador.Direccion,
                                                                        .telefono = pBeOperador.Telefono,
                                                                        .codigo = pBeOperador.Codigo,
                                                                        .clave = pBeOperador.Clave,
                                                                        .activo = pBeOperador.Activo,
                                                                        .user_agr = pBeOperador.User_agr,
                                                                        .fec_agr = pBeOperador.Fec_agr,
                                                                        .user_mod = pBeOperador.User_mod,
                                                                        .fec_mod = pBeOperador.Fec_mod,
                                                                        .costo_hora = pBeOperador.Costo_hora,
                                                                        .usa_hh = pBeOperador.Usa_hh,
                                                                        .foto = pBeOperador.Foto,
                                                                        .recibe = pBeOperador.Recibe,
                                                                        .ubica = pBeOperador.Ubica,
                                                                        .transporta = pBeOperador.Transporta,
                                                                        .pickea = pBeOperador.Pickea,
                                                                        .verifica = pBeOperador.Verifica,
                                                                        .montacarga = pBeOperador.Montacarga,
                                                                        .sistema = pBeOperador.Sistema
                                                                     })
                                        End If

                                    Next


                                Else
                                    '#GT08052025: podrian no existir operadores asociados si fue recepción en bof

                                    reOperadorList.Add(New With {
                                                               .idOperadorRec = 0,
                                                               .idRecepcionEnc = 0,
                                                               .idOperadorBodega = 0,
                                                               .user_Agr = "",
                                                               .fec_Agr = Date.MinValue,
                                                               .user_Mod = "",
                                                               .fec_Mod = Date.MinValue
                                                           })


                                    operadorBodegaList.Add(New With {
                                                                         .IdOperadorBodega = 0,
                                                                         .IdOperador = 0,
                                                                         .IdBodega = 0,
                                                                         .activo = False,
                                                                         .user_agr = "",
                                                                         .fec_agr = Now.Date,
                                                                         .user_mod = "",
                                                                         .fec_mod = Now.Date
                                                                        })


                                    operadorList.Add(New With {
                                                                        .IdOperador = 0,
                                                                        .IdEmpresa = 0,
                                                                        .IdRolOperador = 0,
                                                                        .IdJornada = 0,
                                                                        .nombres = "",
                                                                        .apellidos = "",
                                                                        .direccion = "",
                                                                        .telefono = "",
                                                                        .codigo = "",
                                                                        .clave = "",
                                                                        .activo = False,
                                                                        .user_agr = "",
                                                                        .fec_agr = Now.Date,
                                                                        .user_mod = "",
                                                                        .fec_mod = Now.Date,
                                                                        .costo_hora = 0,
                                                                        .usa_hh = False,
                                                                        .foto = False,
                                                                        .recibe = False,
                                                                        .ubica = False,
                                                                        .transporta = False,
                                                                        .pickea = False,
                                                                        .verifica = False,
                                                                        .montacarga = False,
                                                                        .sistema = False
                                                                     })


                                End If

                                '#GT09052025: iterar la lista de facturas para crear el objeto anonimo para Json
                                If pListFacturasRe IsNot Nothing AndAlso pListFacturasRe.Count > 0 Then
                                    For Each pFactura In pListFacturasRe
                                        facturasList.Add(New With {
                                                                                  .idFacturaRecepcion = pFactura.IdFacturaRecepcion,
                                                                                  .idRecepcionEnc = pFactura.IdRecepcionEnc,
                                                                                  .orden = pFactura.Orden,
                                                                                  .noFactura = pFactura.NoFactura,
                                                                                  .observacion = pFactura.Observacion,
                                                                                  .fec_Agr = pFactura.Fec_agr,
                                                                                  .user_Agr = pFactura.User_agr,
                                                                                  .fec_Mod = pFactura.Fec_mod,
                                                                                  .user_Mod = pFactura.User_mod,
                                                                                  .completa = pFactura.Completa
                                                    })
                                    Next
                                Else
                                    facturasList.Add(New With {
                                                                                    .idFacturaRecepcion = 0,
                                                                                    .idRecepcionEnc = 0,
                                                                                    .orden = 0,
                                                                                    .noFactura = "",
                                                                                    .observacion = "",
                                                                                    .fec_Agr = Date.MinValue,
                                                                                    .user_Agr = "",
                                                                                    .fec_Mod = Date.MinValue,
                                                                                    .user_Mod = "",
                                                                                    .completa = False
                                                        })
                                End If

                                '#GT09052025: iterar la lista de imagenes para crear el objeto anonimo para Json
                                If pListaImgRe IsNot Nothing AndAlso pListaImgRe.Count > 0 Then
                                    For Each pImagen In pListaImgRe

                                        imgList.Add(New With {
                                                                                         .idImagen = pImagen.IdImagen,
                                                                                         .idRecepcionEnc = pImagen.IdRecepcionEnc,
                                                                                         .imagen = pImagen.Imagen,
                                                                                         .user_Agr = pImagen.User_agr,
                                                                                         .fec_Agr = pImagen.Fec_agr,
                                                                                         .observacion = pImagen.Observacion
                                                           })
                                    Next
                                Else
                                    imgList.Add(New With {
                                                                                        .idImagen = 0,
                                                                                        .idRecepcionEnc = 0,
                                                                                        .imagen = "",
                                                                                        .user_Agr = "",
                                                                                        .fec_Agr = Date.MinValue,
                                                                                        .observacion = ""
                                                    })
                                End If

                                '#GT12052025: crear el objeto anonimo del tipo transaccion en la recepción
                                If pTrans_re_tr IsNot Nothing Then

                                    Dim trans_re_tr As New With {
                                                            .IdTipoTransaccion = pTrans_re_tr.IdTipoTransaccion,
                                                            .Descripcion = pTrans_re_tr.Descripcion,
                                                            .Funcionalidad = pTrans_re_tr.Funcionalidad,
                                                            .UsarHH = pTrans_re_tr.UsaHH,
                                                            .DescDev = pTrans_re_tr.DescDev,
                                                            .TipoTrans = pTrans_re_tr.TipoTrans,
                                                            .ConRef = pTrans_re_tr.ConRef,
                                                            .Activo = pTrans_re_tr.Activo
                                        }
                                Else
                                    clsHelper.LogMensaje(lblprg, "La tarea de recepción no tiene asociado un tipo de transacción", clsHelper.TipoMensaje.Error_)
                                    Throw New Exception("La tarea de recepción no tiene asociado un tipo de transacción!")
                                End If

                                '*********************************************************************************************
                                '*********************** Recepción con las propiedades previamente calculadas ****************
                                If pListRecepcionEnc IsNot Nothing AndAlso pListRecepcionEnc.Count > 0 Then
                                    For Each recepcion In pListRecepcionEnc
                                        recepcionesList.Add(New With {
                                                                .encabezado = New With {
                                                                                    .idRecepcionEnc = recepcion.IdRecepcionEnc,
                                                                                    .idPropietarioBodega = recepcion.PropietarioBodega.IdPropietarioBodega,
                                                                                    .IdMuelle = recepcion.IdMuelle,
                                                                                    .IdUbicacionRecepcion = recepcion.IdUbicacionRecepcion,
                                                                                    .IdTipoTransaccion = recepcion.IdTipoTransaccion,
                                                                                    .fecha_recepcion = recepcion.Fecha_recepcion,
                                                                                    .hora_ini_pc = recepcion.Hora_ini_pc,
                                                                                    .hora_fin_pc = recepcion.Hora_fin_pc,
                                                                                    .muestra_precio = recepcion.Muestra_precio,
                                                                                    .estado = recepcion.Estado,
                                                                                    .user_agr = recepcion.User_agr,
                                                                                    .fec_agr = recepcion.Fec_agr,
                                                                                    .user_mod = recepcion.User_mod,
                                                                                    .fec_mod = recepcion.Fec_mod,
                                                                                    .fecha_tarea = recepcion.Fecha_tarea,
                                                                                    .tomar_fotos = recepcion.Tomar_fotos,
                                                                                    .escanear_rec_ubic = recepcion.Escanear_rec_ubic,
                                                                                    .para_por_codigo = recepcion.Para_por_codigo,
                                                                                    .observacion = recepcion.Observacion,
                                                                                    .firma_piloto = recepcion.Firma_piloto,
                                                                                    .activo = recepcion.Activo,
                                                                                    .NoGuia = recepcion.NoGuia,
                                                                                    .CorreoEnviado = recepcion.CorreoEnviado,
                                                                                    .Revision_Inconsistencia = recepcion.Revision_Inconsistencia,
                                                                                    .bloqueada = recepcion.bloqueada,
                                                                                    .bloqueada_por = recepcion.bloqueada_por,
                                                                                    .idusuariobloqueo = recepcion.IdUsuarioBloqueo,
                                                                                    .idmotivoanulacionbodega = recepcion.IdMotivoAnulacionBodega,
                                                                                    .Habilitar_Stock = recepcion.Habilitar_Stock,
                                                                                    .idvehiculo = recepcion.IdVehiculo,
                                                                                    .idpiloto = recepcion.IdPiloto,
                                                                                    .No_Marchamo = recepcion.No_Marchamo,
                                                                                    .mostrar_cantidad_esperada = recepcion.Mostrar_Cantidad_Esperada,
                                                                                    .IdBodega = recepcion.IdBodega,
                                                                                    .carta_cupo = recepcion.Carta_Cupo,
                                                                                    .IdEstado_defecto_recepcion = recepcion.IdEstado_Defecto_Recepcion,
                                                                                    .no_contenedor = recepcion.No_Contenedor
                                                              },
                                                              .detalle = recepcion.Detalle,
                                                              .ocsRelacionadas = reOcList,
                                                              .operadoresRec = reOperadorList,
                                                              .operadores = operadorList,
                                                              .operadorBodega = operadorBodegaList,
                                                              .facturas = facturasList,
                                                              .imagenes = imgList
                                                    })
                                    Next

                                Else
                                    clsHelper.LogMensaje(lblprg, "El ingreso no recepción asociada.", clsHelper.TipoMensaje.Error_)
                                    Throw New Exception("El ingreso no recepción asociada.")
                                End If


                                If pTrans_re_enc.Detalle Is Nothing OrElse pTrans_re_enc.Detalle.Count = 0 Then
                                    clsHelper.LogMensaje(lblprg, "La recepción no tiene detalle.", clsHelper.TipoMensaje.Error_)
                                    Throw New Exception("La recepción no tiene detalle.")
                                End If

                                '********************** Stock_Rec, Stock y movimientos con las propiedades ***********************************
                                For Each re_det As clsBeTrans_re_det In pTrans_re_enc.Detalle

                                    pTrans_movimientos = New clsBeTrans_movimientos()
                                    pStock_Rec = New clsBeStock_rec()
                                    pStock = New clsBeStock()


                                    pTrans_movimientos = clsLnTrans_movimientos.GetSingle_By_IdRecepcionEnc_And_IdRecepcionDet(re_det.IdRecepcionEnc,
                                                                                                                               re_det.IdRecepcionDet,
                                                                                                                               re_det.Lic_plate,
                                                                                                                               clsTransaccion.lConnection,
                                                                                                                               clsTransaccion.lTransaction)

                                    pStock_Rec = clsLnStock_rec.GetSingle_Stock_By_IdRecepcionEnc_And_IdRecpecionDet(re_det.IdRecepcionEnc,
                                                                                                                     re_det.IdRecepcionDet,
                                                                                                                     re_det.Lic_plate,
                                                                                                                     clsTransaccion.lConnection,
                                                                                                                     clsTransaccion.lTransaction)

                                    pStock = clsLnStock.GetSingle_By_IdRecepcionEnc_And_IdRecepcionDet(re_det.IdRecepcionEnc,
                                                                                                       re_det.IdRecepcionDet,
                                                                                                       clsTransaccion.lConnection,
                                                                                                       clsTransaccion.lTransaction)


                                    If pTrans_movimientos IsNot Nothing Then
                                        trans_movimientosList.Add(New With {
                                                                            .IdMovimiento = pTrans_movimientos.IdMovimiento,
                                                                            .IdEmpresa = pTrans_movimientos.IdEmpresa,
                                                                            .IdBodegaOrigen = pTrans_movimientos.IdBodegaOrigen,
                                                                            .IdTransaccion = pTrans_movimientos.IdTransaccion,
                                                                            .IdPropietarioBodega = pTrans_movimientos.IdPropietarioBodega,
                                                                            .IdProductoBodega = pTrans_movimientos.IdProductoBodega,
                                                                            .IdUbicacionOrigen = pTrans_movimientos.IdUbicacionOrigen,
                                                                            .IdUbicacionDestino = pTrans_movimientos.IdUbicacionDestino,
                                                                            .IdPresentacion = pTrans_movimientos.IdPresentacion,
                                                                            .IdEstadoOrigen = pTrans_movimientos.IdEstadoOrigen,
                                                                            .IdEstadoDestino = pTrans_movimientos.IdEstadoDestino,
                                                                            .IdUnidadMedida = pTrans_movimientos.IdUnidadMedida,
                                                                            .IdTipoTarea = pTrans_movimientos.IdTipoTarea,
                                                                            .IdBodegaDestino = pTrans_movimientos.IdBodegaDestino,
                                                                            .IdRecepcion = pTrans_movimientos.IdRecepcion,
                                                                            .cantidad = pTrans_movimientos.Cantidad,
                                                                            .serie = pTrans_movimientos.Serie,
                                                                            .peso = pTrans_movimientos.Peso,
                                                                            .lote = pTrans_movimientos.Lote,
                                                                            .fecha_vence = pTrans_movimientos.Fecha_vence,
                                                                            .fecha = pTrans_movimientos.Fecha,
                                                                            .barra_pallet = pTrans_movimientos.Barra_pallet,
                                                                            .hora_ini = pTrans_movimientos.Hora_ini,
                                                                            .hora_fin = pTrans_movimientos.Hora_fin,
                                                                            .fecha_agr = pTrans_movimientos.Fecha_agr,
                                                                            .usuario_agr = pTrans_movimientos.Usuario_agr,
                                                                            .cantidad_hist = pTrans_movimientos.Cantidad_hist,
                                                                            .peso_hist = pTrans_movimientos.Peso_hist,
                                                                            .lic_plate = pTrans_movimientos.Lic_plate,
                                                                            .IdOperadorBodega = pTrans_movimientos.IdOperadorBodega,
                                                                            .IdRecepcionDet = pTrans_movimientos.IdRecepcionDet,
                                                                            .IdPedidoEnc = pTrans_movimientos.IdPedidoEnc,
                                                                            .IdPedidoDet = pTrans_movimientos.IdPedidoDet,
                                                                            .IdDespachoEnc = pTrans_movimientos.IdDespachoEnc,
                                                                            .IdDespachoDet = pTrans_movimientos.IdDespachoDet
                                                                        })

                                    Else
                                        clsHelper.LogMensaje(lblprg, "No se obtuvo el movimiento asociado a la recepción " & re_det.IdRecepcionEnc & " detalle: " & re_det.IdRecepcionDet, clsHelper.TipoMensaje.Error_)
                                        Throw New Exception("No se obtuvo el movimiento asociado a la recepción.")
                                    End If

                                    If pStock_Rec IsNot Nothing Then
                                        stock_recList.Add(New With {
                                                                .IdStockRec = pStock_Rec.IdStockRec,
                                                                .IdPropietarioBodega = pStock_Rec.IdPropietarioBodega,
                                                                .IdProductoBodega = pStock_Rec.IdProductoBodega,
                                                                .IdProductoEstado = pStock_Rec.IdProductoEstado,
                                                                .IdPresentacion = pStock_Rec.IdPresentacion,
                                                                .IdUnidadMedida = pStock_Rec.IdUnidadMedida,
                                                                .IdUbicacion = pStock_Rec.IdUbicacion,
                                                                .IdUbicacion_anterior = pStock_Rec.IdUbicacion_anterior,
                                                                .IdRecepcionEnc = pStock_Rec.IdRecepcionEnc,
                                                                .IdRecepcionDet = pStock_Rec.IdRecepcionDet,
                                                                .IdPedidoEnc = pStock_Rec.IdPedidoEnc,
                                                                .IdPickingEnc = pStock_Rec.IdPickingEnc,
                                                                .IdDespachoEnc = pStock_Rec.IdDespachoEnc,
                                                                .lote = pStock_Rec.Lote,
                                                                .lic_plate = pStock_Rec.Lic_plate,
                                                                .serial = pStock_Rec.Serial,
                                                                .cantidad = pStock_Rec.Cantidad,
                                                                .fecha_ingreso = pStock_Rec.Fecha_Ingreso,
                                                                .fecha_vence = pStock_Rec.Fecha_vence,
                                                                .uds_lic_plate = pStock_Rec.Uds_lic_plate,
                                                                .no_bulto = pStock_Rec.No_bulto,
                                                                .fecha_manufactura = pStock_Rec.Fecha_Manufactura,
                                                                .añada = pStock_Rec.Añada,
                                                                .user_agr = pStock_Rec.User_agr,
                                                                .fec_agr = pStock_Rec.Fec_agr,
                                                                .user_mod = pStock_Rec.User_mod,
                                                                .fec_mod = pStock_Rec.Fec_mod,
                                                                .activo = pStock_Rec.Activo,
                                                                .peso = pStock_Rec.Peso,
                                                                .temperatura = pStock_Rec.Temperatura,
                                                                .regularizado = pStock_Rec.Regularizado,
                                                                .fecha_regularizacion = pStock_Rec.Fecha_regularizacion,
                                                                .no_linea = pStock_Rec.No_linea,
                                                                .atributo_variante_1 = pStock_Rec.Atributo_Variante_1,
                                                                .impreso = pStock_Rec.Impreso,
                                                                .IdBodega = pStock_Rec.IdBodega,
                                                                .pallet_no_estandar = pStock_Rec.Pallet_No_Estandar
                                                      })
                                    Else
                                        clsHelper.LogMensaje(lblprg, "No se obtuvo el stock_rec asociado a la recepción " & re_det.IdRecepcionEnc & " detalle: " & re_det.IdRecepcionDet, clsHelper.TipoMensaje.Error_)
                                        Throw New Exception("No se obtuvo el stock_rec asociado a la recepción.")
                                    End If

                                    If pStock IsNot Nothing Then
                                        stockList.Add(New With {
                                                             .IdStock = pStock.IdStock,
                                                             .IdPropietarioBodega = pStock.IdPropietarioBodega,
                                                             .IdProductoBodega = pStock.IdProductoBodega,
                                                             .IdProductoEstado = pStock.IdProductoEstado,
                                                             .IdPresentacion = pStock.IdPresentacion,
                                                             .IdUnidadMedida = pStock.IdUnidadMedida,
                                                             .IdUbicacion = pStock.IdUbicacion,
                                                             .IdUbicacion_anterior = pStock.IdUbicacion_anterior,
                                                             .IdRecepcionEnc = pStock.IdRecepcionEnc,
                                                             .IdRecepcionDet = pStock.IdRecepcionDet,
                                                             .IdPedidoEnc = pStock.IdPedidoEnc,
                                                             .IdPickingEnc = pStock.IdPickingEnc,
                                                             .IdDespachoEnc = pStock.IdDespachoEnc,
                                                             .lote = pStock.Lote,
                                                             .lic_plate = pStock.Lic_plate,
                                                             .serial = pStock.Serial,
                                                             .cantidad = pStock.Cantidad,
                                                             .fecha_ingreso = pStock.Fecha_Ingreso,
                                                             .fecha_vence = pStock.Fecha_vence,
                                                             .uds_lic_plate = pStock.Uds_lic_plate,
                                                             .no_bulto = pStock.No_bulto,
                                                             .fecha_manufactura = pStock.Fecha_Manufactura,
                                                             .añada = pStock.Añada,
                                                             .user_agr = pStock.User_agr,
                                                             .fec_agr = pStock.Fec_agr,
                                                             .user_mod = pStock.User_mod,
                                                             .fec_mod = pStock.Fec_mod,
                                                             .activo = pStock.Activo,
                                                             .peso = pStock.Peso,
                                                             .temperatura = pStock.Temperatura,
                                                             .atributo_variante_1 = pStock.Atributo_Variante_1,
                                                             .IdBodega = pStock.IdBodega,
                                                             .pallet_no_estandar = pStock.Pallet_No_Estandar
                                                         })
                                    Else
                                        clsHelper.LogMensaje(lblprg, "Omitiendo el stock asociado a la recepción " & re_det.IdRecepcionEnc & " detalle: " & re_det.IdRecepcionDet, clsHelper.TipoMensaje.Info)
                                        'Throw New Exception("No se obtuvo el stock asociado a la recepción.")

                                        stockList.Add(New With {
                                                            .IdStock = 0,
                                                            .IdPropietarioBodega = 0,
                                                            .IdProductoBodega = 0,
                                                            .IdProductoEstado = 0,
                                                            .IdPresentacion = 0,
                                                            .IdUnidadMedida = 0,
                                                            .IdUbicacion = 0,
                                                            .IdUbicacion_anterior = 0,
                                                            .IdRecepcionEnc = 0,
                                                            .IdRecepcionDet = 0,
                                                            .IdPedidoEnc = 0,
                                                            .IdPickingEnc = 0,
                                                            .IdDespachoEnc = 0,
                                                            .lote = 0,
                                                            .lic_plate = 0,
                                                            .serial = 0,
                                                            .cantidad = 0,
                                                            .fecha_ingreso = New Date(1900, 1, 1),
                                                            .fecha_vence = New Date(1900, 1, 1),
                                                            .uds_lic_plate = "",
                                                            .no_bulto = 0,
                                                            .fecha_manufactura = New Date(1900, 1, 1),
                                                            .añada = 0,
                                                            .user_agr = "",
                                                            .fec_agr = New Date(1900, 1, 1),
                                                            .user_mod = "",
                                                            .fec_mod = New Date(1900, 1, 1),
                                                            .activo = False,
                                                            .peso = 0,
                                                            .temperatura = 0.0,
                                                            .atributo_variante_1 = "",
                                                            .IdBodega = 0,
                                                            .pallet_no_estandar = False
                                                        })


                                    End If

                                Next

                            Next

                        Else
                            clsHelper.LogMensaje(lblprg, "No existe el registro entre el ingreso y la recepción ", clsHelper.TipoMensaje.Error_)
                            Throw New Exception("No existe el registro entre el ingreso y la recepción.")
                        End If


                        '#GT09052025: llenar la OC y añadir los objetos de detalle, poliza, tipo ingreso, recepciones [encabezado, detalle, OcRelacionada, operadores, facturas, imagenes]
                        ocList.Add(New With {
                                             .encabezado = New With {
                                                              .IdOrdenCompraEnc = OC.IdOrdenCompraEnc,
                                                              .IdPropietarioBodega = OC.IdPropietarioBodega,
                                                              .IdProveedorBodega = OC.IdProveedorBodega,
                                                              .IdTipoIngresoOC = OC.IdTipoIngresoOC,
                                                              .IdEstadoOC = OC.IdEstadoOC,
                                                              .IdMotivoDevolucion = OC.IdMotivoDevolucion,
                                                              .Fecha_Creacion = OC.Fecha_Creacion,
                                                              .Hora_Creacion = OC.Hora_Creacion,
                                                              .No_Documento = OC.No_Documento,
                                                              .User_Agr = OC.User_Agr,
                                                              .Fec_Agr = OC.Fec_Agr,
                                                              .User_Mod = OC.User_Mod,
                                                              .Fec_Mod = OC.Fec_Mod,
                                                              .Procedencia = OC.Procedencia,
                                                              .No_Marchamo = OC.No_Marchamo,
                                                              .Referencia = OC.Referencia,
                                                              .Observacion = OC.Observacion,
                                                              .Control_Poliza = OC.Control_Poliza,
                                                              .Activo = OC.Activo,
                                                              .Fecha_Recepcion = OC.Fecha_Recepcion,
                                                              .Hora_Inicio_Recepcion = OC.Hora_Inicio_Recepcion,
                                                              .Hora_Fin_Recepcion = OC.Hora_Fin_Recepcion,
                                                              .IdMuelleRecepcion = OC.IdMuelleRecepcion,
                                                              .Programar_Recepcion = OC.Programar_Recepcion,
                                                              .IdMotivoAnulacionBodega = OC.IdMotivoAnulacionBodega,
                                                              .Enviado_A_ERP = OC.Enviado_A_ERP,
                                                              .serie = OC.Serie,
                                                              .correlativo = OC.Correlativo,
                                                              .IdDespachoEnc = OC.IdDespachoEnc,
                                                              .no_ticket_tms = OC.No_Ticket_TMS,
                                                              .IdNoDocumentoRef = OC.IdNoDocumentoRef,
                                                              .idacuerdocomercial = OC.IdAcuerdoComercial,
                                                              .IdOperadorBodegaDefecto = OC.IdOperadorBodegaDefecto,
                                                              .IdBodega = OC.IdBodega,
                                                              .no_documento_recepcion_erp = OC.No_Documento_Recepcion_ERP,
                                                              .no_documento_devolucion = OC.No_Documento_Devolucion,
                                                              .IdPedidoEncDevolucion = OC.IdPedidoEncDevolucion,
                                                              .push_to_nav = OC.Push_To_NAV,
                                                              .no_documento_ubicacion_erp = OC.No_Documento_Ubicacion_ERP,
                                                              .PutAway_Registrado = OC.PutAway_Registrado,
                                                              .Codigo_Empresa_ERP = OC.Codigo_Empresa_ERP
                                                              },
                                              .detalle = ocDetList,
                                              .polizas = If(OC.Control_Poliza,
                                                              New List(Of clsBeTrans_oc_pol) From {OC.ObjPoliza},
                                                              New List(Of clsBeTrans_oc_pol) From {
                                                                  New clsBeTrans_oc_pol With {
                                                                                              .IdOrdenCompraPol = 0,
                                                                                              .IdOrdenCompraEnc = 0,
                                                                                              .Bl_No = 0,
                                                                                              .NoPoliza = "",
                                                                                              .Pto_Descarga = "",
                                                                                              .Viaje_no = "",
                                                                                              .Buque_no = "",
                                                                                              .Remitente = "",
                                                                                              .Fecha_abordaje = Now.Date,
                                                                                              .Destino = "",
                                                                                              .Dir_destino = "",
                                                                                              .Descripcion = "",
                                                                                              .Po_number = "",
                                                                                              .Cantidad = 0,
                                                                                              .Piezas = 0,
                                                                                              .Total_kgs = 0,
                                                                                              .Cbm = 0,
                                                                                              .Dua = "",
                                                                                              .Fecha_poliza = Now.Date,
                                                                                              .Pais_procede = "",
                                                                                              .Tipo_cambio = 0.00,
                                                                                              .Total_valoraduana = 0,
                                                                                              .Total_lineas = 0,
                                                                                              .Total_bultos = 0,
                                                                                              .Total_bultos_Peso_Bruto = 0,
                                                                                              .Total_usd = 0,
                                                                                              .Total_flete = 0,
                                                                                              .Total_seguro = 0,
                                                                                              .User_agr = "",
                                                                                              .Fec_agr = Now.Date,
                                                                                              .User_mod = "",
                                                                                              .Fec_mod = Now.Date,
                                                                                              .codigo_poliza = "",
                                                                                              .ticket = "",
                                                                                              .numero_orden = "",
                                                                                              .fecha_aceptacion = Now.Date,
                                                                                              .fecha_llegada = Now.Date,
                                                                                              .total_otros = 0,
                                                                                              .IdRegimen = 0,
                                                                                              .Total_bultos_Peso_Neto = 0,
                                                                                              .clave_aduana = "",
                                                                                              .nit_imp_exp = "",
                                                                                              .clase = "",
                                                                                              .mod_transporte = "",
                                                                                              .total_liquidar = 0,
                                                                                              .total_general = 0,
                                                                                              .Codigo_Barra = "",
                                                                                              .activo = False,
                                                                                              .IdBodega = 0}
                                                                                            }),
                                              .tipoIngreso = OC.TipoIngreso,
                                              .recepciones = recepcionesList,
                                              .stockRec = stock_recList,
                                              .stock = stockList,
                                              .movimientos = trans_movimientosList
                                   })

                    End If

                    listPayload.AddRange(ocList)

                Next

            End If

            clsTransaccion.Commit_Transaction()

            Return listPayload

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            clsTransaccion.Close_Conection()
        End Try

    End Function


End Class
