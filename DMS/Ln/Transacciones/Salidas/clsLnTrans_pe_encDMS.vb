Imports DevExpress.XtraEditors
Imports System.Reflection
Imports Newtonsoft.Json
Imports TOMWMS
Imports Newtonsoft.Json.Linq
Imports DevExpress.Data.Helpers
Imports DevExpress.XtraPrinting.Native
Imports System.Drawing.Drawing2D
Imports System.Security.Cryptography
Imports DevExpress.XtraEditors.TextEditController
Public Class clsLnTrans_pe_encDMS

    Public Shared Function ImportarSalidas(ByRef lblprg As RichTextBox) As List(Of Object)

        Try

            '#GT09052025: Lista que acumula los objetos para la API
            Dim listaPayloads As New List(Of Object)()
            Dim peEncList As New List(Of Object)()
            Dim pePolizaList As New List(Of Object)()
            Dim pePicking As New Object()
            Dim pPickingOperadoresList As New List(Of Object)
            Dim pPickingImgList As New List(Of Object)


            '#GT23052025: banderas que indican si lista esta vacia o no


            '#GT09052025: lista de objetos para iterar el detalle y agregarlos a los objetos api
            Dim ListPickingOp As New List(Of clsBeTrans_picking_op)()
            Dim pPickingPrioridad As New clsBeTrans_picking_prioridad()
            Dim pListPE As New List(Of clsBeTrans_pe_enc)()
            pListPE = clsLnTrans_pe_enc.GetAll()

            If pListPE IsNot Nothing AndAlso pListPE.Count > 0 Then


                listaPayloads = New List(Of Object)

                For Each pBePeEnc In pListPE

                    If pBePeEnc IsNot Nothing Then

                        '#GT23052025: limpiar los objetos para no duplicar registros
                        peEncList = New List(Of Object)()
                        pPickingOperadoresList = New List(Of Object)
                        pPickingImgList = New List(Of Object)

                        If pBePeEnc.TipoPedido.Control_Poliza Then
                            pBePeEnc.ObjPoliza = clsLnTrans_pe_pol.GetSingleId(pBePeEnc.IdPedidoEnc)
                        End If


                        If pBePeEnc.Detalle Is Nothing OrElse pBePeEnc.Detalle.Count = 0 Then
                            Return peEncList
                        End If

                        If pBePeEnc.Picking.ListaPickingUbic Is Nothing Then
                            Throw New Exception("El pedido no tiene detalle del picking_ubic.")
                        End If

                        '#GT21052025: cargar lista de pickingUbicStock
                        Dim ListPickingUbicStock As New List(Of clsBeTrans_picking_ubic_stock)()
                        For Each pickingUbic In pBePeEnc.Picking.ListaPickingUbic
                            Dim pPickingUbicSTock As New clsBeTrans_picking_ubic_stock()
                            pPickingUbicSTock = clsLnTrans_picking_ubic_stock.Get_Single_By_IdPickingUbic_And_IdStock(pBePeEnc.Picking.IdPickingEnc, pickingUbic.IdPickingUbic, pickingUbic.IdStock)
                            If pPickingUbicSTock IsNot Nothing Then
                                ListPickingUbicStock.Add(pPickingUbicSTock)
                            End If
                        Next

                        '#GT21052025: cargar lista operadores picking, sino tiene, cargar objeto por defecto
                        ListPickingOp = clsLnTrans_picking_op.Get_All_By_IdPickingEnc(pBePeEnc.IdPickingEnc)
                        If ListPickingOp IsNot Nothing AndAlso ListPickingOp.Count > 0 Then

                            For Each OperadorPicking In ListPickingOp
                                pPickingOperadoresList.Add(New With {
                                                                    .IdOperadorPicking = OperadorPicking.IdOperadorPicking,
                                                                    .IdPickingEnc = OperadorPicking.IdPickingEnc,
                                                                    .IdOperadorBodega = OperadorPicking.IdOperadorBodega,
                                                                    .user_agr = OperadorPicking.User_agr,
                                                                    .fec_agr = OperadorPicking.Fec_agr,
                                                                    .user_mod = OperadorPicking.User_mod,
                                                                    .fec_mod = OperadorPicking.Fec_mod
                                                           })
                            Next
                        Else
                            pPickingOperadoresList.Add(New With {
                                                                                      .IdOperadorPicking = 0,
                                                                                      .IdPickingEnc = 0,
                                                                                      .IdOperadorBodega = 0,
                                                                                      .user_Agr = "",
                                                                                      .fec_agr = New Date(1900, 1, 1),
                                                                                      .user_mod = "",
                                                                                      .fec_mod = New Date(1900, 1, 1)
                                                  })
                        End If

                        '#GT23052025: cargar lista imagenes del picking
                        Dim ListPicking_img As New List(Of clsBeTrans_picking_img)()
                        ListPicking_img = clsLnTrans_Picking_Img.Get_All_Imagen_By_IdPedidoEnc(pBePeEnc.IdPedidoEnc)
                        If ListPicking_img IsNot Nothing AndAlso ListPicking_img.Count > 0 Then

                            For Each pPickingImg In ListPicking_img
                                pPickingImgList.Add(New With {
                                                            .IdImagen = pPickingImg.IdImagen,
                                                            .IdPickingEnc = pPickingImg.IdPickingEnc,
                                                            .IdPickingDet = pPickingImg.IdPickingDet,
                                                            .IdPedidoEnc = pPickingImg.IdPedidoEnc,
                                                            .IdPedidoDet = pPickingImg.IdPedidoDet,
                                                            .Imagen = pPickingImg.Imagen,
                                                            .user_agr = pPickingImg.User_agr,
                                                            .fec_agr = pPickingImg.Fec_agr,
                                                            .observacion = pPickingImg.Observacion
                                                            })
                            Next

                        Else
                            pPickingImgList.Add(New With {
                                                        .IdImagen = 0,
                                                        .IdPickingEnc = 0,
                                                        .IdPickingDet = 0,
                                                        .IdPedidoEnc = 0,
                                                        .IdPedidoDet = 0,
                                                        .Imagen = Nothing,
                                                        .user_agr = "",
                                                        .fec_agr = New Date(1900, 1, 1),
                                                        .observacion = ""
                                                        })

                        End If

                        '#GT23052025: cargar prioridad del picking
                        pPickingPrioridad = clsLnTrans_picking_prioridad.GetSingle_By_IdPickingPrioridad(pBePeEnc.Picking.IdPrioridadPicking)

                        If pPickingPrioridad Is Nothing Then
                            pPickingPrioridad = New clsBeTrans_picking_prioridad With {
                                                                                .IdPrioridadPicking = 0,
                                                                                .Codigo = 0,
                                                                                .Nombre = "",
                                                                                .User_agr = "",
                                                                                .Fec_agr = New Date(1900, 1, 1),
                                                                                .User_mod = "",
                                                                                .Fec_mod = New Date(1900, 1, 1),
                                                                                .Activo = False
                                                                                }
                        End If

                        '#GT09052025: llenar el Pedido y añadir los objetos de detalle
                        peEncList.Add(New With {
                                                 .tipopedido = pBePeEnc.TipoPedido,
                                                 .encabezado = New With {
                                                                         .IdPedidoEnc = pBePeEnc.IdPedidoEnc,
                                                                         .IdBodega = pBePeEnc.IdBodega,
                                                                         .IdCliente = pBePeEnc.IdCliente,
                                                                         .IdMuelle = pBePeEnc.IdMuelle,
                                                                         .IdPropietarioBodega = pBePeEnc.IdPropietarioBodega,
                                                                         .IdTipoPedido = pBePeEnc.IdTipoPedido,
                                                                         .IdPickingEnc = pBePeEnc.IdPickingEnc,
                                                                         .Fecha_Pedido = pBePeEnc.Fecha_Pedido,
                                                                         .hora_ini = pBePeEnc.Hora_ini,
                                                                         .hora_fin = pBePeEnc.Hora_fin,
                                                                         .ubicacion = pBePeEnc.Ubicacion,
                                                                         .estado = pBePeEnc.Estado,
                                                                         .no_despacho = pBePeEnc.No_despacho,
                                                                         .activo = pBePeEnc.Activo,
                                                                         .user_agr = pBePeEnc.User_agr,
                                                                         .fec_agr = pBePeEnc.Fec_agr,
                                                                         .user_mod = pBePeEnc.User_mod,
                                                                         .fec_mod = pBePeEnc.Fec_mod,
                                                                         .no_documento = pBePeEnc.No_documento,
                                                                         .local = pBePeEnc.Local,
                                                                         .pallet_primero = pBePeEnc.Pallet_primero,
                                                                         .dias_cliente = pBePeEnc.Dias_cliente,
                                                                         .anulado = pBePeEnc.Anulado,
                                                                         .RoadKilometraje = pBePeEnc.RoadKilometraje,
                                                                         .RoadFechaEntr = pBePeEnc.RoadFechaEntr,
                                                                         .RoadDirEntrega = pBePeEnc.RoadDirEntrega,
                                                                         .RoadTotal = pBePeEnc.RoadTotal,
                                                                         .RoadDesMonto = pBePeEnc.RoadDesMonto,
                                                                         .RoadImpMonto = pBePeEnc.RoadImpMonto,
                                                                         .RoadPeso = pBePeEnc.RoadPeso,
                                                                         .RoadBandera = pBePeEnc.RoadBandera,
                                                                         .RoadStatCom = pBePeEnc.RoadStatCom,
                                                                         .RoadCalcoBJ = pBePeEnc.RoadCalcoBJ,
                                                                         .RoadImpres = pBePeEnc.RoadImpres,
                                                                         .RoadADD1 = pBePeEnc.RoadADD1,
                                                                         .RoadADD2 = pBePeEnc.RoadADD2,
                                                                         .RoadADD3 = pBePeEnc.RoadADD3,
                                                                         .RoadStatProc = pBePeEnc.RoadStatProc,
                                                                         .RoadRechazado = pBePeEnc.RoadRechazado,
                                                                         .RoadRazon_Rechazado = pBePeEnc.RoadRazon_Rechazado,
                                                                         .RoadInformado = pBePeEnc.RoadInformado,
                                                                         .RoadSucursal = pBePeEnc.RoadSucursal,
                                                                         .RoadIdDespacho = pBePeEnc.RoadIdDespacho,
                                                                         .RoadIdFacturacion = pBePeEnc.RoadIdFacturacion,
                                                                         .RoadIdRuta = pBePeEnc.RoadIdRuta,
                                                                         .RoadIdVendedor = pBePeEnc.RoadIdVendedor,
                                                                         .RoadIdRutaDespacho = pBePeEnc.RoadIdRutaDespacho,
                                                                         .RoadIdVendedorDespacho = pBePeEnc.RoadIdVendedorDespacho,
                                                                         .Observacion = pBePeEnc.Observacion,
                                                                         .PedidoRoad = pBePeEnc.PedidoRoad,
                                                                         .HoraEntregaDesde = pBePeEnc.HoraEntregaDesde,
                                                                         .HoraEntregaHasta = pBePeEnc.HoraEntregaHasta,
                                                                         .referencia = pBePeEnc.Referencia,
                                                                         .IdMotivoAnulacionBodega = pBePeEnc.IdMotivoAnulacionBodega,
                                                                         .Enviado_A_ERP = pBePeEnc.Enviado_A_ERP,
                                                                         .control_ultimo_lote = pBePeEnc.Control_Ultimo_Lote,
                                                                         .serie = pBePeEnc.Serie,
                                                                         .correlativo = pBePeEnc.Correlativo,
                                                                         .Referencia_Documento_Ingreso_Bodega_Destino = pBePeEnc.Referencia_Documento_Ingreso_Bodega_Destino,
                                                                         .sync_mi3 = pBePeEnc.Sync_MI3,
                                                                         .No_Picking_ERP = pBePeEnc.No_Picking_ERP,
                                                                         .no_documento_externo = pBePeEnc.No_Documento_Externo,
                                                                         .requiere_tarimas = pBePeEnc.Requiere_Tarimas,
                                                                         .fecha_preparacion = pBePeEnc.Fecha_Preparacion,
                                                                         .IdTipoManufactura = pBePeEnc.IdTipoManufactura,
                                                                         .bodega_origen = pBePeEnc.Bodega_Origen,
                                                                         .bodega_destino = pBePeEnc.Bodega_Destino,
                                                                         .idacuerdocomercial = pBePeEnc.IdAcuerdoComercial,
                                                                         .IdMotivoDevolucion = pBePeEnc.IdMotivoDevolucion,
                                                                         .Codigo_Empresa_ERP = pBePeEnc.Codigo_Empresa_ERP
                                                                        },
                                                 .detalle = pBePeEnc.Detalle,
                                                 .poliza = If(pBePeEnc.TipoPedido.Control_Poliza,
                                                              New List(Of clsBeTrans_pe_pol) From {pBePeEnc.ObjPoliza},
                                                              New List(Of clsBeTrans_pe_pol) From {New clsBeTrans_pe_pol With {
                                                                                                                            .IdOrdenPedidoPol = 0,
                                                                                                                            .IdOrdenPedidoEnc = 0,
                                                                                                                            .Bl_No = "",
                                                                                                                            .NoPoliza = "",
                                                                                                                            .Pto_Descarga = "",
                                                                                                                            .Viaje_no = "",
                                                                                                                            .Buque_no = "",
                                                                                                                            .Remitente = "",
                                                                                                                            .Fecha_abordaje = New Date(1900, 1, 1),
                                                                                                                            .Destino = "",
                                                                                                                            .Dir_destino = "",
                                                                                                                            .Descripcion = "",
                                                                                                                            .Po_number = "",
                                                                                                                            .Cantidad = 0,
                                                                                                                            .Piezas = 0,
                                                                                                                            .Total_kgs = 0D,
                                                                                                                            .Cbm = 0D,
                                                                                                                            .Dua = "",
                                                                                                                            .Fecha_poliza = New Date(1900, 1, 1),
                                                                                                                            .Pais_procede = "",
                                                                                                                            .Tipo_cambio = 0D,
                                                                                                                            .Total_valoraduana = 0D,
                                                                                                                            .Total_lineas = 0,
                                                                                                                            .Total_bultos = 0,
                                                                                                                            .Total_bultos_Peso = 0D,
                                                                                                                            .Total_usd = 0D,
                                                                                                                            .Total_flete = 0D,
                                                                                                                            .Total_seguro = 0D,
                                                                                                                            .User_agr = "",
                                                                                                                            .Fec_agr = New Date(1900, 1, 1),
                                                                                                                            .User_mod = "",
                                                                                                                            .Fec_mod = New Date(1900, 1, 1),
                                                                                                                            .clave_aduana = "",
                                                                                                                            .nit_imp_exp = "",
                                                                                                                            .clase = "",
                                                                                                                            .mod_transporte = "",
                                                                                                                            .total_liquidar = 0D,
                                                                                                                            .total_general = 0D,
                                                                                                                            .codigo_poliza = "",
                                                                                                                            .ticket = "",
                                                                                                                            .numero_orden = "",
                                                                                                                            .fecha_aceptacion = New Date(1900, 1, 1),
                                                                                                                            .fecha_llegada = New Date(1900, 1, 1),
                                                                                                                            .total_otros = 0D,
                                                                                                                            .IdRegimen = 0,
                                                                                                                            .Total_bultos_Peso_Neto = 0D,
                                                                                                                            .activo = False
                                                                                                                            }}),
                                                 .picking = New With {
                                                                        .encabezado = New With {
                                                                                                .idPickingEnc = pBePeEnc.Picking.IdPickingEnc,
                                                                                                .idBodega = pBePeEnc.Picking.IdBodega,
                                                                                                .idPropietarioBodega = pBePeEnc.Picking.IdPropietarioBodega,
                                                                                                .idUbicacionPicking = pBePeEnc.Picking.IdUbicacionPicking,
                                                                                                .fecha_picking = pBePeEnc.Picking.Fecha_picking,
                                                                                                .hora_ini = pBePeEnc.Picking.Hora_ini,
                                                                                                .hora_fin = pBePeEnc.Picking.Hora_fin,
                                                                                                .estado = pBePeEnc.Picking.Estado,
                                                                                                .user_agr = pBePeEnc.Picking.User_agr,
                                                                                                .fec_agr = pBePeEnc.Picking.Fec_agr,
                                                                                                .user_mod = pBePeEnc.Picking.User_mod,
                                                                                                .fec_mod = pBePeEnc.Picking.Fec_mod,
                                                                                                .detalle_operador = pBePeEnc.Picking.Detalle_operador,
                                                                                                .activo = pBePeEnc.Picking.Activo,
                                                                                                .verifica_auto = pBePeEnc.Picking.verifica_auto,
                                                                                                .procesado_bof = pBePeEnc.Picking.procesado_bof,
                                                                                                .requiere_preparacion = pBePeEnc.Picking.Requiere_Preparacion,
                                                                                                .tipo_preparacion = pBePeEnc.Picking.Tipo_Preparacion,
                                                                                                .estado_preparacion = pBePeEnc.Picking.Estado_Preparacion,
                                                                                                .fecha_inicio_preparacion = pBePeEnc.Picking.Fecha_Inicio_Preparacion,
                                                                                                .fecha_fin_preparacion = pBePeEnc.Picking.Fecha_Fin_Preparacion,
                                                                                                .referencia = pBePeEnc.Picking.Referencia,
                                                                                                .fotografia_verificacion = pBePeEnc.Picking.Fotografia_Verificacion,
                                                                                                .idBodegaMuelle = pBePeEnc.Picking.IdBodegaMuelle,
                                                                                                .idPrioridadPicking = pBePeEnc.Picking.IdPrioridadPicking,
                                                                                                .idTipoPicking = pBePeEnc.Picking.IdTipoPicking
                                                                                            },
                                                                        .detalle = pBePeEnc.Picking.ListaPickingDet,
                                                                        .pickingUbic = pBePeEnc.Picking.ListaPickingUbic,
                                                                        .pickingUbicStock = ListPickingUbicStock,
                                                                        .pickingImg = pPickingImgList,
                                                                        .pickingOperadores = pPickingOperadoresList,
                                                                        .prioridad = pPickingPrioridad
                                                                       }
                                                })

                    End If

                Next

            End If

            Return peEncList

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

End Class
