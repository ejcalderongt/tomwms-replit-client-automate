Imports System.Reflection
Imports DevExpress.Compatibility
Imports DevExpress.Data.Filtering.Helpers.SubExprHelper.ThreadHoppingFiltering
Imports DevExpress.XtraEditors
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports TOMWMS

Public Class clsLnProductoDMS


    Public Shared Async Sub Exportacion_ProductosAsync(ByVal lblprg As RichTextBox)
        Dim api As New ApiService()
        Dim reloj As New Stopwatch()
        Dim listProducto As New List(Of clsBeProducto)
        Dim Contador As Integer = 0
        Dim RegistrosEncontrados As Integer = 0
        Dim pRegistrosFallidos As Integer = 0
        Dim pRegistrosExitosos As Integer = 0
        Dim pTablaSincronizada As String = ""
        Dim resultado As String = ""

        Try


            reloj.Start()
            pTablaSincronizada = clsHelper.ObtenerNombreTabla("ExportarProductos")
            clsHelper.LogMensaje(lblprg, "Iniciando carga de productos...", clsHelper.TipoMensaje.Info)

            listProducto = GetAll_By_CDC(pTablaSincronizada, listProducto)

            If listProducto IsNot Nothing AndAlso listProducto.Count > 0 Then
                RegistrosEncontrados = listProducto.Count
                clsHelper.LogMensaje(lblprg, "Productos encontrados " & listProducto.Count, clsHelper.TipoMensaje.Exito)
            Else
                clsHelper.LogMensaje(lblprg, "Productos no encontrados ", clsHelper.TipoMensaje.Error_)
                Exit Sub
            End If

            '#Iteramos por ingreso y enviamos a la nube para no hacer un proceso único pesado
            For Each BeProducto In listProducto
                Contador += 1
                Dim enviado As Boolean = False
                Dim intento As Integer = 1
                Dim maxIntentos As Integer = 3

                clsHelper.LogMensaje(lblprg, "Iterando Registro: " & Contador & "/" & RegistrosEncontrados, clsHelper.TipoMensaje.Info)
                Dim JsonOC = Crear_Json(lblprg, BeProducto)

                While Not enviado And intento <= maxIntentos

                    resultado = Await api.EnviarJsonProductoAsync(JsonOC, lblprg)

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

                If Not enviado Then
                    pRegistrosFallidos += 1
                    Guadar_Envio_Rechazado(BeProducto.IdProducto, resultado)
                End If

            Next

            reloj.Stop()

            Dim pRespuesta As String = ""
            If pRegistrosFallidos > 0 And pRegistrosExitosos > 0 Then
                pRespuesta = $"Parcial:  no se sincronizaron {pRegistrosFallidos} registros. Total enviados: {RegistrosEncontrados}"
            ElseIf pRegistrosFallidos > 0 Then
                pRespuesta = $"Error al sincronizar todos los registros ({pRegistrosFallidos})"
            Else
                pRespuesta = "Ok"
            End If

            Dim mensajeFinal As String = $"Sincronización finalizada. Tiempo total: {reloj.Elapsed.TotalSeconds:N2} segundos."
            clsHelper.LogMensaje(lblprg, mensajeFinal, clsHelper.TipoMensaje.Exito)

            clsHelper.Registrar_Log(pRespuesta, pTablaSincronizada, CInt(reloj.Elapsed.TotalSeconds))

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsHelper.Registrar_Log("Error " & vMsgError, pTablaSincronizada)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            reloj.Stop()

        End Try
    End Sub


    Public Shared Function Crear_Json(ByRef lblprg As RichTextBox, ByVal pProducto As clsBeProducto) As String
        Crear_Json = ""
        Dim listPayload As New List(Of Object)
        Dim clsTransaccion As New clsTransaccion()
        Dim BeLogUltimaSincronizacion As New clsBeLog_sincronizacion_nube()
        Dim productoList As New List(Of Object)
        Dim productoEstadoList As New List(Of Object)
        Dim productoUmbasList As New List(Of Object)
        Dim productoBodegaList As New List(Of Object)
        Dim propietarioBodegaList As New List(Of Object)
        Dim pListaProductos As New List(Of clsBeProducto)()
        Dim pListaUMBAS As New List(Of clsBeUnidad_medida)()
        Dim pListaEstadosProducto As New List(Of clsBeProducto_estado)()
        Dim pListaProductoBodega As New List(Of clsBeProducto_bodega)()
        Dim pListPropietarioBodega As New List(Of clsBePropietario_bodega)()
        Try
            clsTransaccion.Begin_Transaction()
            listPayload = New List(Of Object)
            clsHelper.LogMensaje(lblprg, "Procesando producto: " & pProducto.IdProducto, clsHelper.TipoMensaje.Info)

            productoEstadoList = New List(Of Object)
            productoUmbasList = New List(Of Object)
            productoBodegaList = New List(Of Object)
            propietarioBodegaList = New List(Of Object)

            pProducto.ParametroA = New clsBeProducto_parametro_a
            If pProducto.IdProductoParametroA > 0 Then clsLnProducto_parametro_a.Obtener(pProducto.ParametroA, clsTransaccion.lConnection, clsTransaccion.lTransaction)

            pProducto.ParametroB = New clsBeProducto_parametro_b
            If pProducto.IdProductoParametroB > 0 Then clsLnProducto_parametro_b.Obtener(pProducto.ParametroB, clsTransaccion.lConnection, clsTransaccion.lTransaction)

            pProducto.Propietario = New clsBePropietarios
            pProducto.Propietario = clsLnPropietarios.GetSingle(pProducto.IdPropietario, clsTransaccion.lConnection, clsTransaccion.lTransaction)

            '#GT16052025: obtener propiedades externas al objeto producto (PRODUCTO_ESTADO, UNIDAD_MEDIDA, PRODUCTO_BODEGA)
            If pProducto.Propietario.IdPropietario > 0 Then
                pListaEstadosProducto = New List(Of clsBeProducto_estado)()
                pListaUMBAS = New List(Of clsBeUnidad_medida)()
                pListaEstadosProducto = clsLnProducto_estado.GetAll_ByPropietario_And_Activo(pProducto.IdPropietario, True, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                pListaUMBAS = clsLnUnidad_medida.Get_All_Filtro(True, pProducto.IdPropietario, clsTransaccion.lConnection, clsTransaccion.lTransaction)
            Else
                Throw New Exception("Producto sin propietario asociado correctamente!")
            End If

            '#GT16052025: Obtener los estados de un producto
            If pListaEstadosProducto IsNot Nothing AndAlso pListaEstadosProducto.Count > 0 Then

                For Each Estado In pListaEstadosProducto
                    productoEstadoList.Add(New With {
                            .IdEstado = Estado.IdEstado,
                            .IdPropietario = Estado.IdPropietario,
                            .nombre = Estado.Nombre,
                            .IdUbicacionDefecto = Estado.IdUbicacionDefecto,
                            .utilizable = Estado.Utilizable,
                            .activo = Estado.Activo,
                            .user_agr = Estado.User_agr,
                            .fec_agr = Estado.Fec_agr,
                            .user_mod = Estado.User_mod,
                            .fec_mod = Estado.Fec_mod,
                            .dañado = Estado.Dañado,
                            .codigo_bodega_erp = Estado.Codigo_Bodega_ERP,
                            .sistema = Estado.Sistema,
                            .dias_vencimiento_clasificacion = Estado.Dias_Vencimiento_Clasificacion,
                            .tolerancia_dias_vencimiento = Estado.Tolerancia_Dias_Vencimiento
                        })
                Next

            Else
                Throw New Exception("Producto sin estados definidos!")
            End If

            '#GT16052025: Obtener las Umbas activas de un producto
            If pListaUMBAS IsNot Nothing AndAlso pListaUMBAS.Count > 0 Then

                For Each Umbas In pListaUMBAS
                    productoUmbasList.Add(New With {
                        .IdUnidadMedida = Umbas.IdUnidadMedida,
                        .IdPropietario = Umbas.IdPropietario,
                        .Nombre = Umbas.Nombre,
                        .activo = Umbas.Activo,
                        .fec_agr = Umbas.Fec_agr,
                        .user_mod = Umbas.User_mod,
                        .fec_mod = Umbas.Fec_mod,
                        .user_agr = Umbas.User_agr,
                        .codigo = Umbas.Codigo,
                        .es_um_cobro = Umbas.es_um_cobro,
                        .factor = Umbas.factor
                    })
                Next

            Else
                Throw New Exception("Producto sin UMBAS definidas!")
            End If

            '#GT16052025: Obtener los productos_bodega asociados a un producto, sino existe asociacion retornar objeto default
            pListaProductoBodega = clsLnProducto_bodega.Get_All_By_IdProducto(pProducto.IdProducto, clsTransaccion.lConnection, clsTransaccion.lTransaction)
            If pListaProductoBodega IsNot Nothing AndAlso pListaProductoBodega.Count > 0 Then

                For Each Producto_Bodega In pListaProductoBodega
                    productoBodegaList.Add(New With {
                                                    .IdProductoBodega = Producto_Bodega.IdProductoBodega,
                                                    .IdProducto = Producto_Bodega.IdProducto,
                                                    .IdBodega = Producto_Bodega.IdBodega,
                                                    .activo = Producto_Bodega.Activo,
                                                    .sistema = Producto_Bodega.Sistema,
                                                    .user_agr = Producto_Bodega.User_agr,
                                                    .fec_agr = Producto_Bodega.Fec_agr,
                                                    .user_mod = Producto_Bodega.User_mod,
                                                    .fec_mod = Producto_Bodega.Fec_mod
                                                     })
                Next

            Else
                productoBodegaList.Add(New With {
                                                    .IdProductoBodega = 0,
                                                    .IdProducto = 0,
                                                    .IdBodega = 0,
                                                    .activo = False,
                                                    .sistema = False,
                                                    .user_agr = "",
                                                    .fec_agr = Now.Date,
                                                    .user_mod = "",
                                                    .fec_mod = Now.Date
                                                     })
            End If

            '#GT19062025: obtener propietarios_bodega
            pListPropietarioBodega = New List(Of clsBePropietario_bodega)()
            pListPropietarioBodega = clsLnPropietario_bodega.Get_All_By_IdPropietario(pProducto.IdPropietario, clsTransaccion.lConnection, clsTransaccion.lTransaction)

            If pListPropietarioBodega IsNot Nothing AndAlso pListPropietarioBodega.Count > 0 Then

                For Each PropietarioBodega In pListPropietarioBodega

                    propietarioBodegaList.Add(New With {
                                                            .IdPropietarioBodega = PropietarioBodega.IdPropietarioBodega,
                                                            .IdPropietario = PropietarioBodega.IdPropietario,
                                                            .IdBodega = PropietarioBodega.IdBodega,
                                                            .user_agr = PropietarioBodega.User_agr,
                                                            .fec_agr = PropietarioBodega.Fec_agr,
                                                            .user_mod = PropietarioBodega.User_mod,
                                                            .fec_mod = PropietarioBodega.Fec_mod,
                                                            .activo = PropietarioBodega.Activo
                                                            })

                Next
            Else
                Throw New Exception("El propietario no esta asociado a ninguna bodega.")
            End If

            productoList.Add(New With {
                                     .idProducto = pProducto.IdProducto,
                                     .idPropietario = pProducto.IdPropietario,
                                     .idClasificacion = pProducto.IdClasificacion,
                                     .idFamilia = pProducto.IdFamilia,
                                     .idMarca = pProducto.IdMarca,
                                     .idTipoProducto = pProducto.IdTipoProducto,
                                     .idUnidadMedidaBasica = pProducto.IdUnidadMedidaBasica,
                                     .idCamara = pProducto.IdCamara,
                                     .idTipoRotacion = pProducto.IdTipoRotacion,
                                     .idPerfilSerializado = pProducto.IdPerfilSerializado,
                                     .idIndiceRotacion = pProducto.IdIndiceRotacion,
                                     .idSimbologia = pProducto.IdSimbologia,
                                     .idArancel = pProducto.IdArancel,
                                     .codigo = pProducto.Codigo,
                                     .nombre = pProducto.Nombre,
                                     .codigo_Barra = pProducto.Codigo_barra,
                                     .precio = pProducto.Precio,
                                     .existencia_Min = pProducto.Existencia_min,
                                     .existencia_Max = pProducto.Existencia_max,
                                     .costo = pProducto.Costo,
                                     .peso_Referencia = pProducto.Peso_referencia,
                                     .peso_Tolerancia = pProducto.Peso_tolerancia,
                                     .temperatura_Referencia = pProducto.Temperatura_referencia,
                                     .temperatura_Tolerancia = pProducto.Temperatura_tolerancia,
                                     .activo = pProducto.Activo,
                                     .serializado = pProducto.Serializado,
                                     .genera_Lote = pProducto.Genera_lote,
                                     .genera_Lp_Old = pProducto.Genera_lp,
                                     .control_Vencimiento = pProducto.Control_vencimiento,
                                     .control_Lote = pProducto.Control_lote,
                                     .peso_Recepcion = pProducto.Peso_recepcion,
                                     .peso_Despacho = pProducto.Peso_despacho,
                                     .temperatura_Recepcion = pProducto.Temperatura_recepcion,
                                     .temperatura_Despacho = pProducto.Temperatura_despacho,
                                     .materia_Prima = pProducto.Materia_prima,
                                     .kit = pProducto.Kit,
                                     .tolerancia = pProducto.Tolerancia,
                                     .ciclo_Vida = pProducto.Ciclo_vida,
                                     .user_Agr = pProducto.User_agr,
                                     .fec_Agr = pProducto.Fec_agr,
                                     .user_Mod = pProducto.User_mod,
                                     .fec_Mod = pProducto.Fec_mod,
                                     .noSerie = pProducto.Noserie,
                                     .noParte = pProducto.Noparte,
                                     .fechaManufactura = pProducto.Fechamanufactura,
                                     .capturar_Aniada = pProducto.Capturar_aniada,
                                     .control_Peso = pProducto.Control_peso,
                                     .captura_Arancel = pProducto.Captura_arancel,
                                     .es_Hardware = pProducto.Es_hardware,
                                     .largo = pProducto.Largo,
                                     .alto = pProducto.Alto,
                                     .ancho = pProducto.Ancho,
                                     .idUnidadMedidaCobro = pProducto.IdUnidadMedidaCobro,
                                     .idTipoEtiqueta = pProducto.IdTipoEtiqueta,
                                     .dias_Inventario_Promedio = pProducto.Dias_Inventario_Promedio,
                                     .idproductoparametroa = pProducto.IdProductoParametroA,
                                     .idproductoparametrob = pProducto.IdProductoParametroB,
                                     .idTipoManufactura = pProducto.IdTipoManufactura,
                                     .imagen = pProducto.Imagen,
                                     .marca = pProducto.Marca,
                                     .tipoProducto = pProducto.TipoProducto,
                                     .familia = pProducto.Familia,
                                     .clasificacion = pProducto.Clasificacion,
                                     .parametroA = pProducto.ParametroA,
                                     .parametroB = pProducto.ParametroB,
                                     .propietario = pProducto.Propietario,
                                     .propietariobodega = propietarioBodegaList,
                                     .productoBodega = productoBodegaList,
                                     .productoEstado = productoEstadoList,
                                     .unidadMedida = productoUmbasList
                                 })

            clsTransaccion.Commit_Transaction()

            listPayload.AddRange(productoList)
            Crear_Json = JsonConvert.SerializeObject(listPayload)

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function


    Public Shared Sub Guadar_Envio_Rechazado(ByVal pIdProducto As Integer, ByVal pMensaje As String)
        Dim BeLogSyncError As New clsBeLog_sincronizacion_fallos()
        Try
            BeLogSyncError = New clsBeLog_sincronizacion_fallos()
            BeLogSyncError.IdLogFallo = clsLnLog_sincronizacion_fallos.MaxID() + 1
            BeLogSyncError.IdOrdenCompraEnc = 0
            BeLogSyncError.IdPedidoEnc = 0
            BeLogSyncError.Estado = "Error"
            BeLogSyncError.Mensaje_error = pMensaje
            BeLogSyncError.Fec_agr = Now
            BeLogSyncError.IdProducto = pIdProducto

            clsLnLog_sincronizacion_fallos.Insertar(BeLogSyncError)

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub


    Public Shared Function GetAll_By_CDC(ByVal pTablaSincronizada As String, ByRef pListProducto As List(Of clsBeProducto)) As List(Of clsBeProducto)
        Dim BeLogUltimaSincronizacion As New clsBeLog_sincronizacion_nube()
        Dim clsTransaccion As New clsTransaccion()
        Try

            clsTransaccion.Begin_Transaction()

            BeLogUltimaSincronizacion = New clsBeLog_sincronizacion_nube()
            BeLogUltimaSincronizacion = clsLnLog_sincronizacion_nube.GetLastSync(pTablaSincronizada, clsTransaccion.lConnection, clsTransaccion.lTransaction)
            If BeLogUltimaSincronizacion IsNot Nothing Then
                pListProducto = clsLnProducto.Get_All_By_Activo(BeLogUltimaSincronizacion.Fecha_sincronizacion, True, clsTransaccion.lConnection, clsTransaccion.lTransaction)
            End If

            clsTransaccion.Commit_Transaction()

            Return pListProducto

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            clsTransaccion.Close_Conection()
        End Try

    End Function


    'Public Shared Function ImportarCatalogoProducto(ByVal pCodigoProducto As String) As clsBeProducto

    '    ImportarCatalogoProducto = Nothing

    '    Try

    '        Dim pProducto = clsLnProducto.Get_Single_By_CodigoProducto("PCSINCESA")

    '        If pProducto IsNot Nothing Then

    '            pProducto.Marca = New clsBeProducto_marca
    '            If pProducto.IdMarca > 0 Then
    '                pProducto.Marca = clsLnProducto_marca.GetSingle(pProducto.IdMarca)
    '                pProducto.Marca.Propietario = pProducto.Propietario
    '            End If

    '            pProducto.TipoProducto = New clsBeProducto_tipo
    '            If pProducto.IdTipoProducto > 0 Then
    '                pProducto.TipoProducto = clsLnProducto_tipo.GetSingle(pProducto.IdTipoProducto)
    '                pProducto.TipoProducto.Propietario = pProducto.Propietario
    '            End If

    '            pProducto.Familia = New clsBeProducto_familia
    '            If pProducto.IdFamilia > 0 Then
    '                pProducto.Familia = clsLnProducto_familia.GetSingle(pProducto.IdFamilia)
    '                pProducto.Familia.Propietario = pProducto.Propietario
    '            End If

    '            pProducto.Clasificacion = New clsBeProducto_clasificacion
    '            If pProducto.IdClasificacion > 0 Then
    '                pProducto.Clasificacion = clsLnProducto_clasificacion.GetSingle(pProducto.IdClasificacion)
    '                pProducto.Clasificacion.Propietario = pProducto.Propietario
    '            End If

    '            pProducto.ParametroA = New clsBeProducto_parametro_a
    '            If pProducto.IdProductoParametroA > 0 Then pProducto.ParametroA = clsLnProducto_parametro_a.GetSingle(pProducto.IdProductoParametroA)

    '            pProducto.ParametroB = New clsBeProducto_parametro_b
    '            If pProducto.IdProductoParametroB > 0 Then pProducto.ParametroB = clsLnProducto_parametro_b.GetSingle(pProducto.IdProductoParametroB)

    '            pProducto.Propietario = New clsBePropietarios
    '            pProducto.Propietario = clsLnPropietarios.GetSingle(pProducto.IdPropietario)


    '            '#GT05052025: agregar propiedades al objeto que no son nativas
    '            'pProducto.Presentacion = New clsBeProducto_Presentacion
    '            'Dim pPresentacion As List(Of clsBeProducto_Presentacion) = clsLnProducto_presentacion.Get_All_By_IdProducto(pProducto.IdProducto)

    '            'If pPresentacion IsNot Nothing Then
    '            '    Dim propsExtras As New Dictionary(Of String, Object) From {
    '            '        {"presentaciones", pPresentacion}
    '            '}
    '            'End If

    '            'Dim pEstados As List(Of clsBeProducto_estado) = clsLnProducto_estado.Get_Estados_By_IdPropietario_For_HH(pProducto.IdPropietario)
    '            'If pEstados IsNot Nothing Then
    '            '    Dim propsExtras As New Dictionary(Of String, Object) From {
    '            '        {"Estados", pEstados}
    '            '}
    '            'End If

    '            'Dim jsonConExtras As String = SerializarConPropiedadesExtras(pProducto, propsExtras)

    '            ImportarCatalogoProducto = pProducto

    '        End If

    '    Catch ex As Exception
    '        Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
    '    End Try

    'End Function


    'Public Shared Function ImportarCatalogoProductos(ByRef lblprg As RichTextBox, ByVal pTablaSincronizada As String) As List(Of Object)

    '    Dim clsTransaccion As New clsTransaccion()

    '    Dim BeLogUltimaSincronizacion As New clsBeLog_sincronizacion_nube()
    '    Dim productoList As New List(Of Object)
    '    Dim productoEstadoList As New List(Of Object)
    '    Dim productoUmbasList As New List(Of Object)
    '    Dim productoBodegaList As New List(Of Object)
    '    Dim propietarioBodegaList As New List(Of Object)

    '    Dim pListaProductos As New List(Of clsBeProducto)()
    '    Dim pListaUMBAS As New List(Of clsBeUnidad_medida)()
    '    Dim pListaEstadosProducto As New List(Of clsBeProducto_estado)()
    '    Dim pListaProductoBodega As New List(Of clsBeProducto_bodega)()
    '    Dim pListPropietarioBodega As New List(Of clsBePropietario_bodega)()

    '    Try

    '        clsTransaccion.Begin_Transaction()

    '        BeLogUltimaSincronizacion = New clsBeLog_sincronizacion_nube()
    '        BeLogUltimaSincronizacion = clsLnLog_sincronizacion_nube.GetLastSync(pTablaSincronizada, clsTransaccion.lConnection, clsTransaccion.lTransaction)

    '        If BeLogUltimaSincronizacion IsNot Nothing Then
    '            pListaProductos = clsLnProducto.Get_All_By_Activo(BeLogUltimaSincronizacion.Fecha_sincronizacion, True, clsTransaccion.lConnection, clsTransaccion.lTransaction)
    '        Else
    '            clsHelper.LogMensaje(lblprg, "No se encontró sincronización previa para '" & pTablaSincronizada & "'. Se realizará sincronización completa desde fecha base.", clsHelper.TipoMensaje.Advertencia)
    '        End If

    '        If pListaProductos IsNot Nothing AndAlso pListaProductos.Count > 0 Then

    '            productoList = New List(Of Object)

    '            For Each pProducto In pListaProductos

    '                If pProducto IsNot Nothing Then

    '                    productoEstadoList = New List(Of Object)
    '                    productoUmbasList = New List(Of Object)
    '                    productoBodegaList = New List(Of Object)
    '                    propietarioBodegaList = New List(Of Object)

    '                    pProducto.ParametroA = New clsBeProducto_parametro_a
    '                    If pProducto.IdProductoParametroA > 0 Then clsLnProducto_parametro_a.Obtener(pProducto.ParametroA, clsTransaccion.lConnection, clsTransaccion.lTransaction)

    '                    pProducto.ParametroB = New clsBeProducto_parametro_b
    '                    If pProducto.IdProductoParametroB > 0 Then clsLnProducto_parametro_b.Obtener(pProducto.ParametroB, clsTransaccion.lConnection, clsTransaccion.lTransaction)

    '                    pProducto.Propietario = New clsBePropietarios
    '                    pProducto.Propietario = clsLnPropietarios.GetSingle(pProducto.IdPropietario, clsTransaccion.lConnection, clsTransaccion.lTransaction)

    '                    '#GT16052025: obtener propiedades externas al objeto producto (PRODUCTO_ESTADO, UNIDAD_MEDIDA, PRODUCTO_BODEGA)
    '                    If pProducto.Propietario.IdPropietario > 0 Then
    '                        pListaEstadosProducto = New List(Of clsBeProducto_estado)()
    '                        pListaUMBAS = New List(Of clsBeUnidad_medida)()
    '                        pListaEstadosProducto = clsLnProducto_estado.GetAll_ByPropietario_And_Activo(pProducto.IdPropietario, True, clsTransaccion.lConnection, clsTransaccion.lTransaction)
    '                        pListaUMBAS = clsLnUnidad_medida.Get_All_Filtro(True, pProducto.IdPropietario, clsTransaccion.lConnection, clsTransaccion.lTransaction)
    '                    Else
    '                        Throw New Exception("Producto sin propietario asociado correctamente!")
    '                    End If

    '                    '#GT16052025: Obtener los estados de un producto
    '                    If pListaEstadosProducto IsNot Nothing AndAlso pListaEstadosProducto.Count > 0 Then

    '                        For Each Estado In pListaEstadosProducto
    '                            productoEstadoList.Add(New With {
    '                                    .IdEstado = Estado.IdEstado,
    '                                    .IdPropietario = Estado.IdPropietario,
    '                                    .nombre = Estado.Nombre,
    '                                    .IdUbicacionDefecto = Estado.IdUbicacionDefecto,
    '                                    .utilizable = Estado.Utilizable,
    '                                    .activo = Estado.Activo,
    '                                    .user_agr = Estado.User_agr,
    '                                    .fec_agr = Estado.Fec_agr,
    '                                    .user_mod = Estado.User_mod,
    '                                    .fec_mod = Estado.Fec_mod,
    '                                    .dañado = Estado.Dañado,
    '                                    .codigo_bodega_erp = Estado.Codigo_Bodega_ERP,
    '                                    .sistema = Estado.Sistema,
    '                                    .dias_vencimiento_clasificacion = Estado.Dias_Vencimiento_Clasificacion,
    '                                    .tolerancia_dias_vencimiento = Estado.Tolerancia_Dias_Vencimiento
    '                                })
    '                        Next

    '                    Else
    '                        Throw New Exception("Producto sin estados definidos!")
    '                    End If

    '                    '#GT16052025: Obtener las Umbas activas de un producto
    '                    If pListaUMBAS IsNot Nothing AndAlso pListaUMBAS.Count > 0 Then

    '                        For Each Umbas In pListaUMBAS
    '                            productoUmbasList.Add(New With {
    '                                .IdUnidadMedida = Umbas.IdUnidadMedida,
    '                                .IdPropietario = Umbas.IdPropietario,
    '                                .Nombre = Umbas.Nombre,
    '                                .activo = Umbas.Activo,
    '                                .fec_agr = Umbas.Fec_agr,
    '                                .user_mod = Umbas.User_mod,
    '                                .fec_mod = Umbas.Fec_mod,
    '                                .user_agr = Umbas.User_agr,
    '                                .codigo = Umbas.Codigo,
    '                                .es_um_cobro = Umbas.es_um_cobro,
    '                                .factor = Umbas.factor
    '                            })
    '                        Next

    '                    Else
    '                        Throw New Exception("Producto sin UMBAS definidas!")
    '                    End If

    '                    '#GT16052025: Obtener los productos_bodega asociados a un producto, sino existe asociacion retornar objeto default
    '                    pListaProductoBodega = clsLnProducto_bodega.Get_All_By_IdProducto(pProducto.IdProducto, clsTransaccion.lConnection, clsTransaccion.lTransaction)
    '                    If pListaProductoBodega IsNot Nothing AndAlso pListaProductoBodega.Count > 0 Then

    '                        For Each Producto_Bodega In pListaProductoBodega
    '                            productoBodegaList.Add(New With {
    '                                                            .IdProductoBodega = Producto_Bodega.IdProductoBodega,
    '                                                            .IdProducto = Producto_Bodega.IdProducto,
    '                                                            .IdBodega = Producto_Bodega.IdBodega,
    '                                                            .activo = Producto_Bodega.Activo,
    '                                                            .sistema = Producto_Bodega.Sistema,
    '                                                            .user_agr = Producto_Bodega.User_agr,
    '                                                            .fec_agr = Producto_Bodega.Fec_agr,
    '                                                            .user_mod = Producto_Bodega.User_mod,
    '                                                            .fec_mod = Producto_Bodega.Fec_mod
    '                                                             })
    '                        Next

    '                    Else
    '                        productoBodegaList.Add(New With {
    '                                                            .IdProductoBodega = 0,
    '                                                            .IdProducto = 0,
    '                                                            .IdBodega = 0,
    '                                                            .activo = False,
    '                                                            .sistema = False,
    '                                                            .user_agr = "",
    '                                                            .fec_agr = Now.Date,
    '                                                            .user_mod = "",
    '                                                            .fec_mod = Now.Date
    '                                                             })
    '                    End If

    '                    '#GT19062025: obtener propietarios_bodega
    '                    pListPropietarioBodega = New List(Of clsBePropietario_bodega)()
    '                    pListPropietarioBodega = clsLnPropietario_bodega.Get_All_By_IdPropietario(pProducto.IdPropietario, clsTransaccion.lConnection, clsTransaccion.lTransaction)

    '                    If pListPropietarioBodega IsNot Nothing AndAlso pListPropietarioBodega.Count > 0 Then

    '                        For Each PropietarioBodega In pListPropietarioBodega

    '                            propietarioBodegaList.Add(New With {
    '                                                                    .IdPropietarioBodega = PropietarioBodega.IdPropietarioBodega,
    '                                                                    .IdPropietario = PropietarioBodega.IdPropietario,
    '                                                                    .IdBodega = PropietarioBodega.IdBodega,
    '                                                                    .user_agr = PropietarioBodega.User_agr,
    '                                                                    .fec_agr = PropietarioBodega.Fec_agr,
    '                                                                    .user_mod = PropietarioBodega.User_mod,
    '                                                                    .fec_mod = PropietarioBodega.Fec_mod,
    '                                                                    .activo = PropietarioBodega.Activo
    '                                                                    })

    '                        Next
    '                    Else
    '                        Throw New Exception("El propietario no esta asociado a ninguna bodega.")
    '                    End If


    '                    productoList.Add(New With {
    '                                             .idProducto = pProducto.IdProducto,
    '                                             .idPropietario = pProducto.IdPropietario,
    '                                             .idClasificacion = pProducto.IdClasificacion,
    '                                             .idFamilia = pProducto.IdFamilia,
    '                                             .idMarca = pProducto.IdMarca,
    '                                             .idTipoProducto = pProducto.IdTipoProducto,
    '                                             .idUnidadMedidaBasica = pProducto.IdUnidadMedidaBasica,
    '                                             .idCamara = pProducto.IdCamara,
    '                                             .idTipoRotacion = pProducto.IdTipoRotacion,
    '                                             .idPerfilSerializado = pProducto.IdPerfilSerializado,
    '                                             .idIndiceRotacion = pProducto.IdIndiceRotacion,
    '                                             .idSimbologia = pProducto.IdSimbologia,
    '                                             .idArancel = pProducto.IdArancel,
    '                                             .codigo = pProducto.Codigo,
    '                                             .nombre = pProducto.Nombre,
    '                                             .codigo_Barra = pProducto.Codigo_barra,
    '                                             .precio = pProducto.Precio,
    '                                             .existencia_Min = pProducto.Existencia_min,
    '                                             .existencia_Max = pProducto.Existencia_max,
    '                                             .costo = pProducto.Costo,
    '                                             .peso_Referencia = pProducto.Peso_referencia,
    '                                             .peso_Tolerancia = pProducto.Peso_tolerancia,
    '                                             .temperatura_Referencia = pProducto.Temperatura_referencia,
    '                                             .temperatura_Tolerancia = pProducto.Temperatura_tolerancia,
    '                                             .activo = pProducto.Activo,
    '                                             .serializado = pProducto.Serializado,
    '                                             .genera_Lote = pProducto.Genera_lote,
    '                                             .genera_Lp_Old = pProducto.Genera_lp,
    '                                             .control_Vencimiento = pProducto.Control_vencimiento,
    '                                             .control_Lote = pProducto.Control_lote,
    '                                             .peso_Recepcion = pProducto.Peso_recepcion,
    '                                             .peso_Despacho = pProducto.Peso_despacho,
    '                                             .temperatura_Recepcion = pProducto.Temperatura_recepcion,
    '                                             .temperatura_Despacho = pProducto.Temperatura_despacho,
    '                                             .materia_Prima = pProducto.Materia_prima,
    '                                             .kit = pProducto.Kit,
    '                                             .tolerancia = pProducto.Tolerancia,
    '                                             .ciclo_Vida = pProducto.Ciclo_vida,
    '                                             .user_Agr = pProducto.User_agr,
    '                                             .fec_Agr = pProducto.Fec_agr,
    '                                             .user_Mod = pProducto.User_mod,
    '                                             .fec_Mod = pProducto.Fec_mod,
    '                                             .noSerie = pProducto.Noserie,
    '                                             .noParte = pProducto.Noparte,
    '                                             .fechaManufactura = pProducto.Fechamanufactura,
    '                                             .capturar_Aniada = pProducto.Capturar_aniada,
    '                                             .control_Peso = pProducto.Control_peso,
    '                                             .captura_Arancel = pProducto.Captura_arancel,
    '                                             .es_Hardware = pProducto.Es_hardware,
    '                                             .largo = pProducto.Largo,
    '                                             .alto = pProducto.Alto,
    '                                             .ancho = pProducto.Ancho,
    '                                             .idUnidadMedidaCobro = pProducto.IdUnidadMedidaCobro,
    '                                             .idTipoEtiqueta = pProducto.IdTipoEtiqueta,
    '                                             .dias_Inventario_Promedio = pProducto.Dias_Inventario_Promedio,
    '                                             .idproductoparametroa = pProducto.IdProductoParametroA,
    '                                             .idproductoparametrob = pProducto.IdProductoParametroB,
    '                                             .idTipoManufactura = pProducto.IdTipoManufactura,
    '                                             .imagen = pProducto.Imagen,
    '                                             .marca = pProducto.Marca,
    '                                             .tipoProducto = pProducto.TipoProducto,
    '                                             .familia = pProducto.Familia,
    '                                             .clasificacion = pProducto.Clasificacion,
    '                                             .parametroA = pProducto.ParametroA,
    '                                             .parametroB = pProducto.ParametroB,
    '                                             .propietario = pProducto.Propietario,
    '                                             .propietariobodega = propietarioBodegaList,
    '                                             .productoBodega = productoBodegaList,
    '                                             .productoEstado = productoEstadoList,
    '                                             .unidadMedida = productoUmbasList
    '                                         })
    '                Else
    '                    Throw New Exception("El producto no es valido!")
    '                End If

    '            Next

    '        Else
    '            Throw New Exception("No hay productos para exportar!")
    '        End If


    '        Return productoList

    '    Catch ex As Exception
    '        'agregar al log que no se encontraron nuevos productos, no lanzar throw excepcion
    '        Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
    '    End Try

    'End Function


End Class
