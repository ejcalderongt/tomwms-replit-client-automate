Imports System.Reflection
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports TOMWMS


Public Class clsLnTrans_oc_encDMS

    Public Shared Function ImportarIngreso(ByRef lblprg As RichTextBox) As String

        ImportarIngreso = Nothing

        Try

            '#GT09052025: Lista que acumula los objetos para la API
            Dim listaPayloads As New List(Of Object)
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


            '#GT09052025: lista de objetos para iterar el detalle y agregarlos a los objetos api
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


            '#GT07052025: listar todas las OC cerradas y que se encuentren activas
            pListOC = clsLnTrans_oc_enc.GetAll_By_CDC()




            If pListOC IsNot Nothing AndAlso pListOC.Count > 0 Then

                listaPayloads = New List(Of Object)

                lblprg.AppendText(vbNewLine)
                lblprg.AppendText("Se preparan " & pListOC.Count & " ingresos para enviar a la nube " & Now)
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()


                For Each OC In pListOC

                    If OC IsNot Nothing Then

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
                        OC.DetalleOC = clsLnTrans_oc_det.Get_Detalle_OC_By_IdOrdenCompraEnc(OC.IdOrdenCompraEnc)

                        If OC.DetalleOC IsNot Nothing AndAlso OC.DetalleOC.Count > 0 Then

                            For Each oc_det As clsBeTrans_oc_det In OC.DetalleOC

                                productobodega = New clsBeProducto_bodega()
                                productobodega = clsLnProducto_bodega.GetSingle(oc_det.IdProductoBodega)

                                If productobodega IsNot Nothing Then
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

                                End If

                                ocDetList.Add(ocDet)

                            Next
                        Else
                            lblprg.AppendText(vbNewLine)
                            lblprg.AppendText("El ingreso no tiene detalle! " & Now)
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()
                            Return 0
                        End If

                        OC.TipoIngreso = clsLnTrans_oc_ti.GetSingle(OC.IdTipoIngresoOC)
                        If OC.Control_Poliza Then OC.ObjPoliza = clsLnTrans_oc_pol.GetSingle_By_Numero_Orden_And_Activo(OC.IdOrdenCompraEnc)
                        ListReOC = clsLnTrans_re_oc.GetListReOC_By_IdOrdenCompraEnc(OC.IdOrdenCompraEnc)

                        If ListReOC IsNot Nothing AndAlso ListReOC.Count > 0 Then

                            '#GT08052025: iterar la lista de re_oc para obtener todas las recepciones asociadas
                            For Each pTrans_re_oc As clsBeTrans_re_oc In ListReOC

                                pTrans_re_enc = New clsBeTrans_re_enc()
                                pTrans_re_enc = clsLnTrans_re_enc.GetSingle(pTrans_re_oc.IdRecepcionEnc)
                                pListOperadores = New List(Of clsBeTrans_re_op)()
                                pListOperadores = clsLnTrans_re_op.Get_All_By_IdRecepcionEnc(pTrans_re_oc.IdRecepcionEnc)
                                pListFacturasRe = clsLnTrans_re_fact.GetAllByRecepcion(pTrans_re_oc.IdRecepcionEnc)
                                pListaImgRe = clsLnTrans_re_img.GetByOrdenCompraRecepcion(pTrans_re_oc.IdRecepcionOc)
                                pTrans_re_tr = clsLnTrans_re_tr_Partial.GetSingle(pTrans_re_enc.IdTipoTransaccion)

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
                                        pOperadorBodega = clsLnOperador_bodega.GetSingle_By_IdOperadorBodega(pOperador.IdOperadorBodega)
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
                                        pBeOperador = clsLnOperador.Get_Single_By_IdOperador(pOperadorBodega.IdOperador)
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
                                    Throw New Exception("El ingreso no recepción asociada!")
                                End If


                                If pTrans_re_enc.Detalle Is Nothing OrElse pTrans_re_enc.Detalle.Count = 0 Then
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.AppendText("La recepción no tiene detalle! " & Now)
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                    Return 0
                                End If

                                '********************** Stock_Rec, Stock y movimientos con las propiedades ***********************************
                                For Each re_det As clsBeTrans_re_det In pTrans_re_enc.Detalle

                                    pTrans_movimientos = New clsBeTrans_movimientos()
                                    pStock_Rec = New clsBeStock_rec()
                                    pStock = New clsBeStock()

                                    pTrans_movimientos = clsLnTrans_movimientos.GetSingle_By_IdRecepcionEnc_And_IdRecepcionDet(re_det.IdRecepcionEnc, re_det.IdRecepcionDet, re_det.Lic_plate)
                                    pStock_Rec = clsLnStock_rec.GetSingle_Stock_By_IdRecepcionEnc_And_IdRecpecionDet(re_det.IdRecepcionEnc, re_det.IdRecepcionDet, re_det.Lic_plate)
                                    pStock = clsLnStock.GetSingle_By_IdRecepcionEnc_And_IdRecepcionDet(re_det.IdRecepcionEnc, re_det.IdRecepcionDet)


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
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.AppendText("No se obtuvo el movimiento asociado a la recepción: " & re_det.IdRecepcionEnc & " detalle: " & re_det.IdRecepcionDet & " " & Now)
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.Refresh()
                                        lblprg.SelectionStart = lblprg.TextLength
                                        lblprg.ScrollToCaret()
                                        Return 0
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
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.AppendText("No se obtuvo el stock_rec asociado a la recepción! " & Now)
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.Refresh()
                                        lblprg.SelectionStart = lblprg.TextLength
                                        lblprg.ScrollToCaret()
                                        Return 0
                                        'Throw New Exception("No se obtuvo el stock asociado a la recepción " & re_det.IdRecepcionEnc & " y detalle " & re_det.IdRecepcionDet)
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
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.AppendText("No se obtuvo el stock asociado a la recepción! " & Now)
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.Refresh()
                                        lblprg.SelectionStart = lblprg.TextLength
                                        lblprg.ScrollToCaret()
                                        Return 0
                                    End If

                                Next

                            Next

                        Else
                            'Throw New Exception("No existe una relación entre el ingreso y la recepción!")
                            lblprg.AppendText(vbNewLine)
                            lblprg.AppendText("No existe el registro entre el ingreso y la recepción!.  " & Now)
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()
                            Return 0
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

                    listaPayloads.AddRange(ocList)

                Next


                lblprg.AppendText("Incicia serialización de ingresos.")
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                ImportarIngreso = JsonConvert.SerializeObject(listaPayloads)

            Else
                lblprg.AppendText("No se encontraron ingresos para exportar. " & Now)
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()
                Return 0
            End If


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
    Public Shared Function SerializarConPropiedadesExtras(objetoOriginal As Object, propiedadesExtras As Dictionary(Of String, Object)) As String
        ' Convertir el objeto original a JObject
        Dim jObj As JObject = JObject.FromObject(objetoOriginal)

        ' Agregar propiedades dinámicamente
        For Each kvp In propiedadesExtras
            jObj(kvp.Key) = JToken.FromObject(kvp.Value)
        Next

        ' Devolver el JSON resultante
        Return jObj.ToString()
    End Function




    ''' <summary>
    ''' Serializa un objeto agregando propiedades extra dinámicamente, incluyendo un Guid único.
    ''' </summary>
    ''' <param name="objetoOriginal">El objeto base (clase)</param>
    ''' <param name="propiedadesExtras">Diccionario con propiedades adicionales</param>
    ''' <param name="nombreCampoGuid">Nombre del campo Guid a insertar (opcional, por defecto "IdUnico")</param>
    ''' <returns>Cadena JSON con las propiedades combinadas</returns>
    Public Function SerializarConPropiedadesExtrasYGuid(objetoOriginal As Object,
                                                    propiedadesExtras As Dictionary(Of String, Object),
                                                    Optional nombreCampoGuid As String = "IdUnico") As String
        ' Convertir el objeto base a JObject
        Dim jObj As JObject = JObject.FromObject(objetoOriginal)

        ' Insertar el GUID único como propiedad
        jObj(nombreCampoGuid) = Guid.NewGuid().ToString()

        ' Agregar cualquier otra propiedad dinámica
        For Each kvp In propiedadesExtras
            jObj(kvp.Key) = JToken.FromObject(kvp.Value)
        Next

        ' Retornar el JSON resultante
        Return jObj.ToString()
    End Function

End Class
