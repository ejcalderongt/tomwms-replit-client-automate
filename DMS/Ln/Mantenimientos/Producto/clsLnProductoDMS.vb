Imports System.Reflection
Imports Newtonsoft.Json.Linq
Imports TOMWMS

Public Class clsLnProductoDMS

    Public Shared Function ImportarCatalogoProducto(ByVal pCodigoProducto As String) As clsBeProducto

        ImportarCatalogoProducto = Nothing

        Try

            Dim pProducto = clsLnProducto.Get_Single_By_CodigoProducto("PCSINCESA")

            If pProducto IsNot Nothing Then

                pProducto.Marca = New clsBeProducto_marca
                If pProducto.IdMarca > 0 Then
                    pProducto.Marca = clsLnProducto_marca.GetSingle(pProducto.IdMarca)
                    pProducto.Marca.Propietario = pProducto.Propietario
                End If

                pProducto.TipoProducto = New clsBeProducto_tipo
                If pProducto.IdTipoProducto > 0 Then
                    pProducto.TipoProducto = clsLnProducto_tipo.GetSingle(pProducto.IdTipoProducto)
                    pProducto.TipoProducto.Propietario = pProducto.Propietario
                End If

                pProducto.Familia = New clsBeProducto_familia
                If pProducto.IdFamilia > 0 Then
                    pProducto.Familia = clsLnProducto_familia.GetSingle(pProducto.IdFamilia)
                    pProducto.Familia.Propietario = pProducto.Propietario
                End If

                pProducto.Clasificacion = New clsBeProducto_clasificacion
                If pProducto.IdClasificacion > 0 Then
                    pProducto.Clasificacion = clsLnProducto_clasificacion.GetSingle(pProducto.IdClasificacion)
                    pProducto.Clasificacion.Propietario = pProducto.Propietario
                End If

                pProducto.ParametroA = New clsBeProducto_parametro_a
                If pProducto.IdProductoParametroA > 0 Then pProducto.ParametroA = clsLnProducto_parametro_a.GetSingle(pProducto.IdProductoParametroA)

                pProducto.ParametroB = New clsBeProducto_parametro_b
                If pProducto.IdProductoParametroB > 0 Then pProducto.ParametroB = clsLnProducto_parametro_b.GetSingle(pProducto.IdProductoParametroB)

                pProducto.Propietario = New clsBePropietarios
                pProducto.Propietario = clsLnPropietarios.GetSingle(pProducto.IdPropietario)


                '#GT05052025: agregar propiedades al objeto que no son nativas
                'pProducto.Presentacion = New clsBeProducto_Presentacion
                'Dim pPresentacion As List(Of clsBeProducto_Presentacion) = clsLnProducto_presentacion.Get_All_By_IdProducto(pProducto.IdProducto)

                'If pPresentacion IsNot Nothing Then
                '    Dim propsExtras As New Dictionary(Of String, Object) From {
                '        {"presentaciones", pPresentacion}
                '}
                'End If

                'Dim pEstados As List(Of clsBeProducto_estado) = clsLnProducto_estado.Get_Estados_By_IdPropietario_For_HH(pProducto.IdPropietario)
                'If pEstados IsNot Nothing Then
                '    Dim propsExtras As New Dictionary(Of String, Object) From {
                '        {"Estados", pEstados}
                '}
                'End If

                'Dim jsonConExtras As String = SerializarConPropiedadesExtras(pProducto, propsExtras)

                ImportarCatalogoProducto = pProducto

            End If

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function


    Public Shared Function ImportarCatalogoProductos() As List(Of Object)

        Try

            '#GT16052025: objetos anonimos para añadir propiedades a producto que no son nativas (producto_bodega,producto_estados)
            Dim productoList As New List(Of Object)
            Dim productoEstadoList As New List(Of Object)
            Dim productoUmbasList As New List(Of Object)
            Dim productoBodegaList As New List(Of Object)

            '#GT16052025: objetos para llenar las listas
            Dim pListaProductos As New List(Of clsBeProducto)()
            Dim pListaUMBAS As New List(Of clsBeUnidad_medida)()
            Dim pListaEstadosProducto As New List(Of clsBeProducto_estado)()
            Dim pListaProductoBodega As New List(Of clsBeProducto_bodega)()

            '#GT16052025: Obtener lista de productos
            pListaProductos = clsLnProducto.Get_All_By_Activo(True)

            If pListaProductos IsNot Nothing AndAlso pListaProductos.Count > 0 Then

                productoList = New List(Of Object)

                For Each pProducto In pListaProductos

                    If pProducto IsNot Nothing Then

                        productoEstadoList = New List(Of Object)
                        productoUmbasList = New List(Of Object)
                        productoBodegaList = New List(Of Object)

                        pProducto.ParametroA = New clsBeProducto_parametro_a
                        If pProducto.IdProductoParametroA > 0 Then pProducto.ParametroA = clsLnProducto_parametro_a.GetSingle(pProducto.IdProductoParametroA)

                        pProducto.ParametroB = New clsBeProducto_parametro_b
                        If pProducto.IdProductoParametroB > 0 Then pProducto.ParametroB = clsLnProducto_parametro_b.GetSingle(pProducto.IdProductoParametroB)

                        pProducto.Propietario = New clsBePropietarios
                        pProducto.Propietario = clsLnPropietarios.GetSingle(pProducto.IdPropietario)

                        '#GT16052025: obtener propiedades externas al objeto producto (PRODUCTO_ESTADO, UNIDAD_MEDIDA, PRODUCTO_BODEGA)
                        If pProducto.Propietario.IdPropietario > 0 Then
                            pListaEstadosProducto = New List(Of clsBeProducto_estado)()
                            pListaUMBAS = New List(Of clsBeUnidad_medida)()
                            pListaEstadosProducto = clsLnProducto_estado.GetAll_ByPropietario_And_Activo(pProducto.IdPropietario, True)
                            pListaUMBAS = clsLnUnidad_medida.Get_All_Filtro(True, pProducto.IdPropietario)
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
                        pListaProductoBodega = clsLnProducto_bodega.Get_All_By_IdProducto(pProducto.IdProducto)
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


                        'ListProductos.Add(pProducto)


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
                                                 .productoBodega = productoBodegaList,
                                                 .productoEstado = productoEstadoList,
                                                 .unidadMedida = productoUmbasList
                                             })
                    Else
                        Throw New Exception("El producto no es valido!")
                    End If

                Next

            Else
                Throw New Exception("No hay productos para exportar!")
            End If


            Return productoList

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function



    ''' <summary>
    ''' Serializa un objeto agregando propiedades extra dinámicamente.
    ''' </summary>
    ''' <param name="objetoOriginal">El objeto base</param>
    ''' <param name="propiedadesExtras">Diccionario de propiedades adicionales</param>
    ''' <returns>Cadena JSON con propiedades combinadas</returns>
    Public Function SerializarConPropiedadesExtras(objetoOriginal As Object, propiedadesExtras As Dictionary(Of String, Object)) As String
        ' Convertir el objeto original a JObject
        Dim jObj As JObject = JObject.FromObject(objetoOriginal)

        ' Agregar propiedades dinámicamente
        For Each kvp In propiedadesExtras
            jObj(kvp.Key) = JToken.FromObject(kvp.Value)
        Next

        ' Devolver el JSON resultante
        Return jObj.ToString()
    End Function

End Class
