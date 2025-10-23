Imports System.Data.SqlClient
Imports System.Reflection
Imports DevExpress.Compatibility
Imports DevExpress.Data.Filtering.Helpers.SubExprHelper.ThreadHoppingFiltering
Imports DevExpress.Utils.Drawing
Imports DevExpress.XtraEditors
Imports Newtonsoft
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports TOMWMS

Public Class clsLnProductoDMS

    Public Shared Async Function Exportacion_ProductosAsync(ByVal lblprg As RichTextBox, ByVal listaPropietarios As List(Of Integer)) As Task
        Dim reloj As New Stopwatch()
        Dim listProducto As New List(Of clsBeProducto)
        Dim Contador As Integer = 0
        Dim RegistrosEncontrados As Integer = 0
        Dim pRegistrosFallidos As Integer = 0
        Dim pRegistrosExitosos As Integer = 0
        Dim pTablaSincronizada As String = ""
        Dim vTotalRegistrosEncontrados As Integer = 0
        Dim resultado As String = ""
        'Dim pRespuesta As String = ""

        Try
            reloj.Start()
            pTablaSincronizada = clsHelper.ObtenerNombreTabla("ExportarProductos")
            clsHelper.LogMensaje(lblprg, "Iniciando carga de productos...", clsHelper.TipoMensaje.Info)

            '#GT15072025: validar existencia de registros que no se enviaron por algún error.
            Dim listaProductosPendientes = ObtenerRegistrosFallidos(listaPropietarios)
            If listaProductosPendientes.Count > 0 Then
                listProducto = New List(Of clsBeProducto)()
                vTotalRegistrosEncontrados = listaProductosPendientes.Count
                clsHelper.LogMensaje(lblprg,
                                     String.Format("Se encontraron {0} producto(s) pendientes de sincronizar.", vTotalRegistrosEncontrados),
                                     clsHelper.TipoMensaje.Info)

                listProducto = GetAll_By_CDC_Pendientes(pTablaSincronizada, listProducto, listaProductosPendientes)
                If listProducto IsNot Nothing AndAlso listProducto.Count > 0 Then
                    Dim objRespuesta As Object = Await ProcesarProductos(lblprg, listProducto)
                    pRegistrosExitosos = objRespuesta.item1
                    pRegistrosFallidos = objRespuesta.item2
                End If
            End If

            '#GT01072025: limpiamos lista previa, e iteramos por registros nuevos.
            '#GT15072055: luego de validar registros pendientes, se inicia el envio de nuevos productos asociados a los propietarios UX
            listProducto = New List(Of clsBeProducto)()
            listProducto = GetAll_By_CDC(pTablaSincronizada, listProducto, listaPropietarios)

            If listProducto IsNot Nothing AndAlso listProducto.Count > 0 Then
                clsHelper.LogMensaje(lblprg, "Cargando nuevos productos para sincronizar: " & listProducto.Count, clsHelper.TipoMensaje.Info)


                Dim grupos = From r In listProducto
                             Group r By r.Propietario.IdPropietario Into RegistrosProp = Group
                             Select New With {
                     .IdPropietario = IdPropietario,
                     .Lista = RegistrosProp.ToList()
                 }


                Dim propietarioAnterior As Integer = -1

                For Each grupo In grupos

                    If propietarioAnterior <> grupo.IdPropietario Then
                        clsHelper.LogMensaje(lblprg, $"Procesando registros del propietario {grupo.IdPropietario}", clsHelper.TipoMensaje.Info)
                    End If

                    Dim objRespuesta As Object = Await ProcesarProductos(lblprg, grupo.Lista)
                    pRegistrosExitosos = objRespuesta.item1
                    pRegistrosFallidos = objRespuesta.item2

                    Dim pRespuesta As String = ""
                    If objRespuesta.item2 > 0 AndAlso objRespuesta.item1 > 0 Then
                        pRespuesta = $"Parcial: no se sincronizaron {objRespuesta.item2} registros de propietario {grupo.IdPropietario}. Total enviados: {objRespuesta.item1 + objRespuesta.item2}"
                    ElseIf objRespuesta.item2 > 0 Then
                        pRespuesta = $"Error al sincronizar todos los registros de propietario {grupo.IdPropietario} ({objRespuesta.item2})"
                    Else
                        pRespuesta = $"Ok - propietario {grupo.IdPropietario}"
                    End If

                    clsHelper.Registrar_Log_Nube(grupo.IdPropietario, grupo.Lista.Count, pRespuesta, pTablaSincronizada, CInt(reloj.Elapsed.TotalSeconds))

                    propietarioAnterior = grupo.IdPropietario

                Next

            Else
                clsHelper.LogMensaje(lblprg, "Productos no encontrados para sincronizar", clsHelper.TipoMensaje.Error_)
                Exit Function
            End If

            reloj.Stop()

            Dim mensajeFinal As String = $"Sincronización finalizada. Tiempo total: {reloj.Elapsed.TotalSeconds} segundos."
            clsHelper.LogMensaje(lblprg, mensajeFinal, clsHelper.TipoMensaje.Exito)

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            reloj.Stop()
        End Try
    End Function

    Private Shared listaIdsEnviados As New List(Of Integer)
    Private Shared Async Function ProcesarProductos(ByVal lblprg As RichTextBox, ByVal productos As List(Of clsBeProducto)) As Task(Of Object)
        Dim api As New ApiService()
        Dim Contador As Integer = 0
        Dim resultado As String = ""
        Dim pRegistrosExitosos As Integer = 0
        Dim pRegistrosFallidos As Integer = 0
        Dim registros As Integer = productos.Count

        Try

            ' Iteramos los productos
            For Each BeProducto In productos
                Contador += 1
                Dim enviado As Boolean = False
                Dim intento As Integer = 1
                Dim maxIntentos As Integer = 2

                clsHelper.LogMensaje(lblprg, String.Format("Iterando Registro: {0}/{1}", Contador, registros), clsHelper.TipoMensaje.Info)
                clsHelper.LogMensaje(lblprg, "Producto: " & BeProducto.IdProducto, clsHelper.TipoMensaje.Info)

                '#GT15072025: aqui se crea el Json que será enviado en el siguiente proceso
                Dim JsonProducto = Crear_Json(lblprg, BeProducto)

                If String.IsNullOrEmpty(JsonProducto) Then
                    pRegistrosFallidos += 1 'resultado = "No se generó el archivo json correspondiente."
                    Continue For
                End If

                While Not enviado And intento <= maxIntentos
                    resultado = Await api.EnviarJsonProductoAsync(JsonProducto, lblprg)

                    If resultado = "Ok" Then
                        enviado = True
                        pRegistrosExitosos += 1
                        listaIdsEnviados.Add(BeProducto.IdProducto)
                        Actualizar_Envio_Rechazado(BeProducto)
                    Else
                        intento += 1
                        clsHelper.LogMensaje(lblprg, "Reintento de envio: " & intento, clsHelper.TipoMensaje.Info)
                        Await Task.Delay(2000) ' Esperar 2 segundos entre intentos
                    End If
                End While

                ' Si el registro no fue enviado, se guarda el fallo
                If Not enviado Then
                    pRegistrosFallidos += 1
                    Guadar_Envio_Rechazado(BeProducto, resultado)
                End If
            Next

            Return (pRegistrosExitosos, pRegistrosFallidos)

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Sub Actualizar_Envio_Rechazado(ByVal pProducto As clsBeProducto)
        Dim BeLogSyncError As New clsBeDMS_Log_sincronizacion_fallos()
        Dim clsTransaccion As New clsTransaccion()
        Try
            clsTransaccion.Begin_Transaction()

            '#GT15072025: validar que existe producto pendiente de envio
            If clsLnDMS_Log_sincronizacion_fallos.Existe_by_Producto(pProducto, clsTransaccion.lConnection, clsTransaccion.lTransaction) Then
                BeLogSyncError = New clsBeDMS_Log_sincronizacion_fallos()
                BeLogSyncError.IdProducto = pProducto.IdProducto
                BeLogSyncError.IdPropietario = pProducto.IdPropietario
                BeLogSyncError.IdOrdenCompraEnc = 0
                BeLogSyncError.IdPedidoEnc = 0
                BeLogSyncError.Estado = "Ok"
                clsLnDMS_Log_sincronizacion_fallos.Actualizar_Registro(BeLogSyncError, clsTransaccion.lConnection, clsTransaccion.lTransaction)
            End If

            clsTransaccion.Commit_Transaction()

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            clsTransaccion.Close_Conection()
        End Try
    End Sub

    Public Shared Function Crear_Json(ByRef lblprg As RichTextBox, ByVal pProducto As clsBeProducto) As String
        Crear_Json = ""
        Dim listPayload As New List(Of Object)
        Dim clsTransaccion As New clsTransaccion()
        Dim BeLogUltimaSincronizacion As New clsBeDMS_Log_sincronizacion_nube()
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
        Dim resultado As String = ""
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
                resultado = "Propietario no esta asociado correctamente al producto: " & pProducto.IdProducto
                clsHelper.LogMensaje(lblprg, resultado, clsHelper.TipoMensaje.Info)
                Guadar_Envio_Rechazado(pProducto, resultado, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                clsTransaccion.Commit_Transaction()
                Return ""

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
                resultado = "No existe un estado asociado al producto: " & pProducto.IdProducto
                clsHelper.LogMensaje(lblprg, resultado, clsHelper.TipoMensaje.Info)
                Guadar_Envio_Rechazado(pProducto, resultado, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                clsTransaccion.Commit_Transaction()
                Return ""
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
                resultado = "No existe UMBAS asociada al producto: " & pProducto.IdProducto
                clsHelper.LogMensaje(lblprg, resultado, clsHelper.TipoMensaje.Info)
                Guadar_Envio_Rechazado(pProducto, resultado, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                clsTransaccion.Commit_Transaction()
                Return ""
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
                resultado = "El propietario no esta asociado a ninguna bodega, para el producto: " & pProducto.IdProducto
                clsHelper.LogMensaje(lblprg, resultado, clsHelper.TipoMensaje.Info)
                Guadar_Envio_Rechazado(pProducto, resultado, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                clsTransaccion.Commit_Transaction()
                Return ""
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


            listPayload.AddRange(productoList)
            clsTransaccion.Commit_Transaction()

            Crear_Json = JsonConvert.SerializeObject(listPayload)

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            clsTransaccion.Close_Conection()
        End Try

    End Function

    '#GT15072025: Guardar en log el envio fallido con transaccion
    Public Shared Sub Guadar_Envio_Rechazado(ByVal pProducto As clsBeProducto,
                                            ByVal pMensaje As String,
                                            Optional ByRef lConnection As SqlConnection = Nothing,
                                            Optional ByRef lTransaction As SqlTransaction = Nothing)

        Dim localConnection As Boolean = False
        Dim localTransaction As Boolean = False
        Dim BeLogSyncError As New clsBeDMS_Log_sincronizacion_fallos()

        Try
            ' Crear conexión si no se recibió
            If lConnection Is Nothing Then
                lConnection = New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
                localConnection = True
            End If

            ' Crear transacción si no se recibió
            If lTransaction Is Nothing Then
                lTransaction = lConnection.BeginTransaction()
                localTransaction = True
            End If

            '#GT08102025: validar que no exista un registro previo para no duplicar el mismo error
            If Not clsLnDMS_Log_sincronizacion_fallos.Existe_by_Producto(pProducto) Then
                BeLogSyncError.IdLogFallo = clsLnDMS_Log_sincronizacion_fallos.MaxID(lConnection, lTransaction) + 1
                BeLogSyncError.IdOrdenCompraEnc = 0
                BeLogSyncError.IdPedidoEnc = 0
                BeLogSyncError.Estado = "Error"
                BeLogSyncError.Mensaje_error = pMensaje
                BeLogSyncError.Fec_agr = Now
                BeLogSyncError.Fec_mod = Now
                BeLogSyncError.IdProducto = pProducto.IdProducto
                BeLogSyncError.IdPropietario = pProducto.IdPropietario
                clsLnDMS_Log_sincronizacion_fallos.Insertar(BeLogSyncError, lConnection, lTransaction)
            End If

            ' Confirmar si se inició transacción local
            If localTransaction Then
                lTransaction.Commit()
            End If

        Catch ex As Exception
            ' Rollback si la transacción es local
            If localTransaction AndAlso lTransaction IsNot Nothing Then
                Try
                    lTransaction.Rollback()
                Catch
                    ' Ignorar errores de rollback
                End Try
            End If
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))

        Finally
            ' Cierre solo si es local
            If localConnection AndAlso lConnection IsNot Nothing AndAlso lConnection.State = ConnectionState.Open Then
                lConnection.Close()
            End If
        End Try

    End Sub

    Public Shared Function GetAll_By_CDC(ByVal pTablaSincronizada As String,
                                         ByRef pListProducto As List(Of clsBeProducto),
                                         ByVal listaPropietarios As List(Of Integer)) As List(Of clsBeProducto)

        Dim BeLogUltimaSincronizacion As New clsBeDMS_Log_sincronizacion_nube()
        Dim clsTransaccion As New clsTransaccion()

        Try

            clsTransaccion.Begin_Transaction()

            If listaPropietarios.Count > 0 Then

                For Each pPropietario In listaPropietarios
                    BeLogUltimaSincronizacion = New clsBeDMS_Log_sincronizacion_nube()
                    BeLogUltimaSincronizacion = clsLnDMS_Log_sincronizacion_nube.GetLastSync(pTablaSincronizada, pPropietario, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                    If BeLogUltimaSincronizacion IsNot Nothing Then
                        Dim listaProducto As List(Of clsBeProducto) = clsLnProducto.Get_All_By_Activo(BeLogUltimaSincronizacion.Fecha_sincronizacion, True, pPropietario, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                        If listaProducto IsNot Nothing Then
                            pListProducto.AddRange(listaProducto)
                        End If

                    End If

                Next

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


    Public Shared Function ObtenerRegistrosFallidos(ByVal listaPropietarios As List(Of Integer)) As List(Of Integer)
        Dim clsTransaccion As New clsTransaccion()
        ObtenerRegistrosFallidos = New List(Of Integer)

        Try
            clsTransaccion.Begin_Transaction()
            ObtenerRegistrosFallidos = clsLnDMS_Log_sincronizacion_fallos.ObtenerRegistrosFallidos_by_Producto(listaPropietarios, Now, clsTransaccion.lConnection, clsTransaccion.lTransaction)
            clsTransaccion.Commit_Transaction()
        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            clsTransaccion.Close_Conection()
        End Try
    End Function


    Public Shared Function GetAll_By_CDC_Pendientes(ByVal pTablaSincronizada As String, ByRef pListProducto As List(Of clsBeProducto), ByVal listaProductos As List(Of Integer)) As List(Of clsBeProducto)
        Dim BeLogUltimaSincronizacion As New clsBeDMS_Log_sincronizacion_nube()
        Dim clsTransaccion As New clsTransaccion()
        Try

            clsTransaccion.Begin_Transaction()

            For Each IdProducto As Integer In listaProductos
                Dim pProducto = clsLnProducto.Get_Single_By_IdProducto(IdProducto, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                pListProducto.Add(pProducto)
            Next

            clsTransaccion.Commit_Transaction()

            Return pListProducto

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            clsTransaccion.Close_Conection()
        End Try

    End Function

End Class
