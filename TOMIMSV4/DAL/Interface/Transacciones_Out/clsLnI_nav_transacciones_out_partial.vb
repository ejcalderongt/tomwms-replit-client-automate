Imports System
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Reflection
Imports TOMWMS.clsDataContractDI

Partial Public Class clsLnI_nav_transacciones_out

    Public Shared Function Insertar_Ingreso(ByVal pIdEmpresa As Integer,
                                            ByVal pIdBodega As Integer,
                                            ByVal pListObjDetR As List(Of clsBeTrans_re_det),
                                            ByVal pIdOrdenCompraEnc As Integer,
                                            ByRef pIdUsuario As Integer,
                                            ByVal pIdPropietarioBodega As Integer,
                                            ByRef lConnection As SqlConnection,
                                            ByRef lTransaction As SqlTransaction) As Boolean

        Insertar_Ingreso = False

        Try

            If pListObjDetR IsNot Nothing Then

                Dim lMaxS As Integer = MaxID(lConnection, lTransaction) + 1
                Dim BeTransaccionesOut As New clsBeI_nav_transacciones_out()
                Dim BeTransOcDet As New clsBeTrans_oc_det
                Dim BeTipoDocumentoIngreso As New clsBeTrans_oc_ti()
                Dim vIdTipoDocIngreso As New clsDataContractDI.tTipoDocumentoIngreso
                Dim BeProducto As New clsBeProducto

                For Each BeTransReDet As clsBeTrans_re_det In pListObjDetR

                    BeTransaccionesOut = New clsBeI_nav_transacciones_out()

                    BeTransOcDet = clsLnTrans_oc_det.Get_Single_By_IdOrdenCompraEnc_And_Linea(pIdOrdenCompraEnc,
                                                                                              BeTransReDet.No_Linea,
                                                                                              BeTransReDet.IdProductoBodega,
                                                                                              lConnection,
                                                                                              lTransaction)

                    If BeTransReDet.IdPropietarioBodega = 0 Then
                        '#EJC20220803_1014: Asignar pIdPropietarioBodega si rec es de BOF sin referencia.
                        BeTransReDet.IdPropietarioBodega = pIdPropietarioBodega
                    End If

                    BeTransaccionesOut.Idtransaccion = lMaxS
                    BeTransaccionesOut.Idempresa = pIdEmpresa
                    BeTransaccionesOut.Idbodega = pIdBodega
                    BeTransaccionesOut.Idpropietario = clsLnPropietarios.Get_IdPropietario(pIdBodega,
                                                                                           BeTransReDet.IdPropietarioBodega,
                                                                                           lConnection,
                                                                                           lTransaction)

                    BeTransaccionesOut.Idpropietariobodega = BeTransReDet.IdPropietarioBodega
                    BeTransaccionesOut.Idordencompra = pIdOrdenCompraEnc
                    BeTransaccionesOut.Idrecepcionenc = BeTransReDet.IdRecepcionEnc
                    BeTransaccionesOut.Idpedidoenc = 0
                    BeTransaccionesOut.Iddespachoenc = 0
                    BeTransaccionesOut.Idproductobodega = BeTransReDet.IdProductoBodega

                    BeProducto = clsLnProducto_bodega.Get_Producto_By_IdProductoBodega(BeTransReDet.IdProductoBodega,
                                                                                       lConnection,
                                                                                       lTransaction)

                    If BeProducto Is Nothing Then
                        Throw New Exception("Error_202212160939: No se obtuvo el objeto de producto para el IdProductoBodega: " & BeTransReDet.IdProductoBodega)
                    End If

                    BeTransaccionesOut.Idproducto = BeProducto.IdProducto
                    BeTransaccionesOut.Idunidadmedida = BeTransReDet.IdUnidadMedida
                    BeTransaccionesOut.Idpresentacion = BeTransReDet.Presentacion.IdPresentacion
                    BeTransaccionesOut.Idproductoestado = BeTransReDet.IdProductoEstado
                    BeTransaccionesOut.Cantidad = BeTransReDet.cantidad_recibida
                    BeTransaccionesOut.Peso = BeTransReDet.Peso
                    BeTransaccionesOut.Lote = BeTransReDet.Lote
                    BeTransaccionesOut.Fecha_vence = BeTransReDet.Fecha_vence
                    BeTransaccionesOut.Fecha_recepcion = BeTransReDet.Fecha_ingreso
                    BeTransaccionesOut.No_pedido = clsLnTrans_oc_enc.Get_No_Pedido(pIdOrdenCompraEnc,
                                                                                   lConnection,
                                                                                   lTransaction)
                    BeTransaccionesOut.No_linea = BeTransReDet.No_Linea
                    BeTransaccionesOut.Codigo_producto = BeProducto.Codigo
                    BeTransaccionesOut.codigo_barra = BeProducto.Codigo_barra
                    BeTransaccionesOut.Nombre_producto = BeProducto.Nombre
                    BeTransaccionesOut.Lic_Plate = BeTransReDet.Lic_plate
                    BeTransaccionesOut.Codigo_variante = clsLnTrans_oc_det.Get_Cod_Variante_Nav(pIdOrdenCompraEnc,
                                                                                                BeTransReDet.No_Linea,
                                                                                                lConnection,
                                                                                                lTransaction)
                    If BeTransaccionesOut.Idpresentacion = 0 Then
                        BeTransaccionesOut.Unidad_medida = IIf(BeTransReDet.UnidadMedida.Nombre = "", BeTransReDet.Nombre_unidad_medida, BeTransReDet.UnidadMedida.Nombre)
                    Else
                        If BeTransReDet.Presentacion.Nombre = "" Then
                            BeTransaccionesOut.Unidad_medida = BeTransReDet.Nombre_presentacion
                        Else
                            BeTransaccionesOut.Unidad_medida = BeTransReDet.Presentacion.Nombre
                        End If
                    End If

                    BeTipoDocumentoIngreso = clsLnTrans_oc_enc.Get_BeTipoDocumento_By_IdOrdenCompraEnc(pIdOrdenCompraEnc,
                                                                                                       lConnection,
                                                                                                       lTransaction)

                    If BeTipoDocumentoIngreso IsNot Nothing Then
                        vIdTipoDocIngreso = BeTipoDocumentoIngreso.IdTipoIngresoOC
                    Else
                        '#EJC202109016: recepción ciega desde BOF.
                        vIdTipoDocIngreso = clsDataContractDI.tTipoDocumentoIngreso.NoDefinido
                    End If

                    '#EJC20210507: Enviar IdtipoDoc, for Idealsa.
                    BeTransaccionesOut.IdTipoDocumento = vIdTipoDocIngreso
                    BeTransaccionesOut.Tipo_transaccion = "INGRESO"

                    '#CKFK20220814 Agregué el IdRecepcionDet de la transaccion de ingreso
                    BeTransaccionesOut.IdRecepcionDet = BeTransReDet.IdRecepcionDet

                    If Not BeTipoDocumentoIngreso Is Nothing Then
                        '#EJC20220504:Para BYB, marcar registros enviados en i_nav_transacciones_out
                        If BeTipoDocumentoIngreso.Marcar_Registros_Enviados_MI3 Then
                            BeTransaccionesOut.Enviado = True
                        Else
                            BeTransaccionesOut.Enviado = False
                        End If
                    Else
                        BeTransaccionesOut.Enviado = False
                    End If

                    BeTransaccionesOut.Fec_mod = Now
                    BeTransaccionesOut.Fec_agr = Now
                    BeTransaccionesOut.User_agr = pIdUsuario
                    BeTransaccionesOut.User_mod = pIdUsuario


                    'GT 10032021 valores del detalle con poliza.
                    'GT 11032021 si tiene OC se asignan los valores.
                    If BeTransOcDet IsNot Nothing Then
                        'BeTransaccionesOut.codigo_barra = BeTransOcDet.Producto.Codigo_barra.FirstOrDefault
                        'GT 15032021 el objeto BeTransaccionesOut no tiene seteado producto, por eso la consulta
                        BeTransaccionesOut.codigo_barra = clsLnProducto.Get_CodigoBarra_By_IdProducto(BeTransaccionesOut.Idproducto,
                                                                                                      lConnection,
                                                                                                      lTransaction)
                        BeTransaccionesOut.valor_aduana = BeTransOcDet.valor_aduana
                        BeTransaccionesOut.valor_fob = BeTransOcDet.valor_fob
                        BeTransaccionesOut.valor_iva = BeTransOcDet.valor_iva
                        BeTransaccionesOut.valor_dai = BeTransOcDet.valor_dai
                        BeTransaccionesOut.valor_seguro = BeTransOcDet.valor_seguro
                        BeTransaccionesOut.valor_flete = BeTransOcDet.valor_flete
                        BeTransaccionesOut.peso_neto = BeTransOcDet.Peso_Neto
                        BeTransaccionesOut.peso_bruto = BeTransOcDet.Peso_Bruto
                    End If

                    BeTransaccionesOut.fecha_despacho = "01/01/1900"

                    '#EJC20181004: Si no tiene OC
                    If BeTransOcDet Is Nothing Then
                        BeTransaccionesOut.Cantidad_Esperada = 0
                    Else
                        BeTransaccionesOut.Cantidad_Esperada = BeTransOcDet.Cantidad
                    End If

                    '#EJC20210923: Si no se hizo un documento de ingreso, no se tendrá tipo
                    'Generalmente ocurre o más bien, solo ocurre cuando es un ingreso ciego sin Documento de Ingreso.
                    If pIdOrdenCompraEnc = 0 Then
                        If BeTipoDocumentoIngreso Is Nothing Then
                            BeTipoDocumentoIngreso = New clsBeTrans_oc_ti()
                        End If
                    End If

                    If vIdTipoDocIngreso = clsDataContractDI.tTipoDocumentoIngreso.Liquidacion_De_Ruta_Devolucion OrElse BeTipoDocumentoIngreso.Es_devolucion Then

                        If pIdOrdenCompraEnc <> 0 Then

                            clsLnTrans_oc_enc.Get_Parametros_Devol_By_IdOrdenCompraEnc(pIdOrdenCompraEnc,
                                                                                       BeTransaccionesOut.IdPedidoEncDevol,
                                                                                       BeTransaccionesOut.no_documento_salida_ref_devol,
                                                                                       lConnection,
                                                                                       lTransaction)


                        End If

                    End If

                    BeTransaccionesOut.IdProductoTallaColor = BeTransReDet.IdProductoTallaColor

                    '#EJC20210923: Obtener talla y color.   
                    Dim DtPtc = clsLnProducto_talla_color.Get_Single_Dt_By_IdProductoTallaColor(BeTransReDet.IdProductoBodega)

                    If Not DtPtc Is Nothing Then
                        If DtPtc.Rows.Count > 0 Then
                            BeTransaccionesOut.Talla = DtPtc.Rows(0).Item("talla").ToString()
                            BeTransaccionesOut.Color = DtPtc.Rows(0).Item("color").ToString()
                        End If
                    End If

                    Insertar(BeTransaccionesOut,
                             lConnection,
                             lTransaction)

                    lMaxS += 1

                    '#EJC20190604: Tabla de homologación de lotes numéricos.
                    Dim BeLoteNum As New clsBeTrans_re_det_lote_num
                    BeLoteNum.IdLoteNum = clsLnTrans_re_det_lote_num.MaxID(lConnection, lTransaction) + 1
                    BeLoteNum.IdProductoBodega = BeTransReDet.IdProductoBodega
                    BeLoteNum.IdRecepcionEnc = BeTransReDet.IdRecepcionEnc
                    BeLoteNum.Codigo = BeTransReDet.Codigo_Producto
                    BeLoteNum.Lote = BeTransReDet.Lote
                    BeLoteNum.Lote_Numerico = BeLoteNum.IdLoteNum
                    BeLoteNum.Cantidad = BeTransReDet.cantidad_recibida
                    BeLoteNum.FechaIngreso = Now
                    clsLnTrans_re_det_lote_num.Insertar(BeLoteNum,
                                                        lConnection,
                                                        lTransaction)

                Next

                Insertar_Ingreso = True

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Insertar_Ingreso_Parcial(ByVal pIdEmpresa As Integer,
                                                    ByVal pIdBodega As Integer,
                                                    ByVal pIdTipoDocumento As clsDataContractDI.tTipoDocumentoIngreso,
                                                    ByVal BeTransReDet As clsBeTrans_re_det,
                                                    ByVal pIdOrdenCompraEnc As Integer,
                                                    ByRef pIdUsuario As Integer,
                                                    ByVal Enviado As Boolean,
                                                    ByRef lConnection As SqlConnection,
                                                    ByRef lTransaction As SqlTransaction) As String

        Dim CadenaResultado As String = ""

        Try

            Dim vCantidad As Double

            If BeTransReDet IsNot Nothing Then

                Dim lMaxS As Integer = MaxID(lConnection, lTransaction) + 1
                Dim BeInavTransaccionesOUT As New clsBeI_nav_transacciones_out()
                Dim BeTransOcDet As New clsBeTrans_oc_det
                Dim BeTipoDocumentoIngreso As New clsBeTrans_oc_ti()
                Dim vIdTipoDocIngreso As String = ""
                Dim BeProducto As New clsBeProducto

                BeInavTransaccionesOUT = New clsBeI_nav_transacciones_out()

                BeTransOcDet = clsLnTrans_oc_det.Get_Single_By_IdOrdenCompraEnc_And_Linea(pIdOrdenCompraEnc,
                                                                                          BeTransReDet.No_Linea,
                                                                                          BeTransReDet.IdProductoBodega,
                                                                                          lConnection,
                                                                                          lTransaction)

                CadenaResultado += "IdOrdenCompraDet " & BeTransOcDet.IdOrdenCompraDet

                BeInavTransaccionesOUT.Idtransaccion = lMaxS
                BeInavTransaccionesOUT.Idempresa = pIdEmpresa
                BeInavTransaccionesOUT.Idbodega = pIdBodega

                BeInavTransaccionesOUT.Idpropietario = clsLnPropietarios.Get_IdPropietario(pIdBodega,
                                                                                           BeTransReDet.IdPropietarioBodega,
                                                                                           lConnection,
                                                                                           lTransaction)

                CadenaResultado += "Propietario: " & BeInavTransaccionesOUT.Idpropietario

                BeInavTransaccionesOUT.Idpropietariobodega = BeTransReDet.IdPropietarioBodega

                '#EJC202208031124:Evitar que el IdPropietario e IdPropietarioBodega queden en 0 en transferencias.
                If BeInavTransaccionesOUT.Idpropietario = 0 OrElse BeInavTransaccionesOUT.Idpropietariobodega = 0 Then
                    Throw New Exception("ERROR_202208031123A: No se obtuvo el identificador de propietario para la tabla de interface, se ha prevenido la inserción para evitar inconsistencias.")
                End If

                BeInavTransaccionesOUT.Idordencompra = pIdOrdenCompraEnc
                BeInavTransaccionesOUT.Idrecepcionenc = BeTransReDet.IdRecepcionEnc
                BeInavTransaccionesOUT.Idpedidoenc = 0
                BeInavTransaccionesOUT.Iddespachoenc = 0
                BeInavTransaccionesOUT.Idproductobodega = BeTransReDet.IdProductoBodega

                BeProducto = clsLnProducto_bodega.Get_Producto_By_IdProductoBodega(BeTransReDet.IdProductoBodega, lConnection, lTransaction)

                If BeProducto Is Nothing Then
                    Throw New Exception("Error_202212160935: No se obtuvo el objeto de producto para el IdProductoBodega: " & BeTransReDet.IdProductoBodega)
                End If

                BeInavTransaccionesOUT.Idproducto = BeProducto.IdProducto

                CadenaResultado += "IdProducto: " & BeInavTransaccionesOUT.Idproducto

                BeInavTransaccionesOUT.Idunidadmedida = BeTransReDet.IdUnidadMedida
                BeInavTransaccionesOUT.Idpresentacion = IIf(Not BeTransReDet.Presentacion Is Nothing, BeTransReDet.Presentacion.IdPresentacion, 0)
                BeInavTransaccionesOUT.Idproductoestado = BeTransReDet.IdProductoEstado
                '#EJC202220608:Mantener cantidad en UMBAS e inidcar si tiene o no presentación.
                BeInavTransaccionesOUT.Cantidad = BeTransReDet.cantidad_recibida
                BeInavTransaccionesOUT.Peso = BeTransReDet.Peso
                BeInavTransaccionesOUT.Lote = BeTransReDet.Lote
                BeInavTransaccionesOUT.Fecha_vence = BeTransReDet.Fecha_vence
                BeInavTransaccionesOUT.Fecha_recepcion = BeTransReDet.Fecha_ingreso
                BeInavTransaccionesOUT.No_pedido = clsLnTrans_oc_enc.Get_No_Pedido(pIdOrdenCompraEnc,
                                                                                   lConnection,
                                                                                   lTransaction)

                CadenaResultado += "No_Pedido : " & BeInavTransaccionesOUT.No_pedido

                BeInavTransaccionesOUT.No_linea = BeTransReDet.No_Linea
                BeInavTransaccionesOUT.Codigo_producto = BeProducto.Codigo
                BeInavTransaccionesOUT.Nombre_producto = BeProducto.Nombre
                BeInavTransaccionesOUT.Codigo_variante = clsLnTrans_oc_det.Get_Cod_Variante_Nav(pIdOrdenCompraEnc,
                                                                                               BeTransReDet.No_Linea,
                                                                                               lConnection,
                                                                                               lTransaction)

                CadenaResultado += "Codigo_variante : " & BeInavTransaccionesOUT.Codigo_variante

                If BeInavTransaccionesOUT.Idpresentacion = 0 Then
                    BeInavTransaccionesOUT.Unidad_medida = IIf(BeTransReDet.UnidadMedida.Nombre = "", BeTransReDet.Nombre_unidad_medida, BeTransReDet.UnidadMedida.Nombre)
                Else
                    If BeTransReDet.Presentacion.Nombre = "" Then
                        BeInavTransaccionesOUT.Unidad_medida = BeTransReDet.Nombre_presentacion
                    Else
                        BeInavTransaccionesOUT.Unidad_medida = BeTransReDet.Presentacion.Nombre
                    End If
                End If


                BeInavTransaccionesOUT.Tipo_transaccion = "INGRESO"
                BeInavTransaccionesOUT.IdTipoDocumento = pIdTipoDocumento
                BeInavTransaccionesOUT.Fec_agr = Now
                BeInavTransaccionesOUT.User_agr = pIdUsuario
                BeInavTransaccionesOUT.User_mod = pIdUsuario

                '#CKFK20220814 Agregué el IdRecepcionDet de la transaccion de ingreso
                BeInavTransaccionesOUT.IdRecepcionDet = BeTransReDet.IdRecepcionDet

                vCantidad = 0

                'GT 10032021 valores del detalle con poliza.
                'GT 11032021 si el detalle existe
                If BeTransOcDet IsNot Nothing Then
                    'ObjM.codigo_barra = BeTransOcDet.Producto.Codigo_barra
                    BeInavTransaccionesOUT.codigo_barra = BeProducto.Codigo_barra
                    BeInavTransaccionesOUT.valor_aduana = BeTransOcDet.valor_aduana
                    BeInavTransaccionesOUT.valor_fob = BeTransOcDet.valor_fob
                    BeInavTransaccionesOUT.valor_iva = BeTransOcDet.valor_iva
                    BeInavTransaccionesOUT.valor_dai = BeTransOcDet.valor_dai
                    BeInavTransaccionesOUT.valor_seguro = BeTransOcDet.valor_seguro
                    BeInavTransaccionesOUT.valor_flete = BeTransOcDet.valor_flete
                    BeInavTransaccionesOUT.peso_neto = BeTransOcDet.Peso_Neto
                    BeInavTransaccionesOUT.peso_bruto = BeTransOcDet.Peso_Bruto
                End If

                BeInavTransaccionesOUT.fecha_despacho = "01/01/1900"

                If Not BeTransReDet.Presentacion Is Nothing Then

                    If BeTransReDet.Presentacion.IdPresentacion > 0 Then
                        If BeTransReDet.Presentacion.EsPallet Then
                            vCantidad = BeTransOcDet.Cantidad * BeTransReDet.Presentacion.Factor * BeTransReDet.Presentacion.CamasPorTarima * BeTransReDet.Presentacion.CajasPorCama
                        Else
                            vCantidad = BeTransReDet.Presentacion.Factor * BeTransOcDet.Cantidad
                        End If
                    Else
                        vCantidad = BeTransOcDet.Cantidad
                    End If

                Else
                    vCantidad = BeTransOcDet.Cantidad
                End If

                If BeTransOcDet Is Nothing Then
                    BeInavTransaccionesOUT.Cantidad_Esperada = 0
                Else
                    BeInavTransaccionesOUT.Cantidad_Esperada = vCantidad
                End If

                If BeTransReDet.Presentacion.IdPresentacion > 0 Then
                    BeInavTransaccionesOUT.Cantidad_Presentacion = BeTransReDet.cantidad_recibida
                End If

                BeInavTransaccionesOUT.Lic_Plate = BeTransReDet.Lic_plate
                BeInavTransaccionesOUT.Uds_Lic_Plate = BeTransReDet.Uds_lic_plate

                '#EJC20210619: Devolucion con ref BYB e IDEALSA.
                If pIdTipoDocumento = clsDataContractDI.tTipoDocumentoIngreso.Liquidacion_De_Ruta_Devolucion Then

                    clsLnTrans_oc_enc.Get_Parametros_Devol_By_IdOrdenCompraEnc(pIdOrdenCompraEnc,
                                                                               BeInavTransaccionesOUT.IdPedidoEncDevol,
                                                                               BeInavTransaccionesOUT.no_documento_salida_ref_devol)
                End If

                BeTipoDocumentoIngreso = clsLnTrans_oc_enc.Get_BeTipoDocumento_By_IdOrdenCompraEnc(pIdOrdenCompraEnc,
                                                                                                   lConnection,
                                                                                                   lTransaction)

                '#EJC20220504:Para BYB, marcar registros enviados en i_nav_transacciones_out
                If BeTipoDocumentoIngreso.Marcar_Registros_Enviados_MI3 Then
                    BeInavTransaccionesOUT.Enviado = True
                Else
                    BeInavTransaccionesOUT.Enviado = Enviado
                End If

                BeInavTransaccionesOUT.IdProductoTallaColor = BeTransReDet.IdProductoTallaColor

                If BeTransOcDet.IdProductoTallaColor <> 0 Then
                    Dim BeProductoTallaColor = clsLnProducto_talla_color.GetSingle(BeTransOcDet.IdProductoTallaColor,
                                                                                   lConnection,
                                                                                   lTransaction)

                    If BeProductoTallaColor IsNot Nothing Then
                        Dim Talla = clsLnTalla.GetSingle(BeProductoTallaColor.IdTalla, lConnection, lTransaction)
                        Dim Color = clsLnColor.GetSingle(BeProductoTallaColor.IdColor, lConnection, lTransaction)

                        BeInavTransaccionesOUT.Talla = Talla?.Nombre
                        BeInavTransaccionesOUT.Color = Color?.Nombre
                    End If
                End If

                Insertar(BeInavTransaccionesOUT,
                         lConnection,
                         lTransaction)

                CadenaResultado += "insertó transacciones_out"

            End If

            Return CadenaResultado

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Sub Insertar_Salida(ByVal pIdEmpresa As Integer,
                                      ByVal pIdBodega As Integer,
                                      ByVal pBeDespachoEnc As clsBeTrans_despacho_enc,
                                      ByRef lConnection As SqlConnection,
                                      ByRef lTransaction As SqlTransaction)

        Try

            Dim Factor As Double = 0
            Dim vEnviado As Boolean = False

            If pBeDespachoEnc IsNot Nothing Then

                Dim lMaxS As Integer = MaxID(lConnection,
                                             lTransaction) + 1

                Dim BePresentacionPedido As New clsBeProducto_Presentacion()
                Dim BeTipoDocumento As New clsBeTrans_pe_tipo()
                Dim BeConfigEnc As New clsBeI_nav_config_enc

                For Each BePedidoEnc As clsBeTrans_pe_enc In pBeDespachoEnc.ListaPedidos

                    BeConfigEnc = clsLnI_nav_config_enc.Get_Single_By_IdBodega(BePedidoEnc.IdBodega, lConnection, lTransaction)

                    '#EJC20220323D: Considerar el tipo de documento en despacho, al insertar registros de interface, bandera enviado.
                    BeTipoDocumento = clsLnTrans_pe_tipo.Get_Single_By_IdTipoPedido(BePedidoEnc.IdTipoPedido,
                                                                                    lConnection,
                                                                                    lTransaction)

                    If Not BeTipoDocumento Is Nothing Then
                        vEnviado = (BeTipoDocumento.Marcar_Registros_Enviados_MI3 OrElse BePedidoEnc.Enviado_A_ERP)
                    End If

                    If vEnviado AndAlso Not BePedidoEnc.Enviado_A_ERP Then
                        If BeConfigEnc.Interface_SAP Then
                            '#EJC20240924: La cumbre, interface SAP, despachos parciales.
                            If Not clsLnStock_res.Tiene_StockRes_By_IdPedidoEnc(BePedidoEnc.IdPedidoEnc,
                                                                                BePedidoEnc.IdBodega,
                                                                                lConnection,
                                                                                lTransaction) Then
                                clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP_By_IdPedidoEnc(BePedidoEnc.IdPedidoEnc,
                                                                                                 True,
                                                                                                 pBeDespachoEnc.User_mod,
                                                                                                 lConnection,
                                                                                                 lTransaction)
                            End If
                        Else
                            '#EJC20220517:Marcar el documento a petición de Marcelo, Mercosal 17052022_1829:
                            clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP_By_IdPedidoEnc(BePedidoEnc.IdPedidoEnc,
                                                                                                 True,
                                                                                                 pBeDespachoEnc.User_mod,
                                                                                                 lConnection,
                                                                                                 lTransaction)
                        End If
                    Else
                        If BeConfigEnc.Interface_SAP Then
                            If clsLnStock_res.Tiene_StockRes_By_IdPedidoEnc(BePedidoEnc.IdPedidoEnc,
                                                                                BePedidoEnc.IdBodega,
                                                                                lConnection,
                                                                                lTransaction) Then
                                clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP_By_IdPedidoEnc_Single(BePedidoEnc.IdPedidoEnc,
                                                                                                         False,
                                                                                                         pBeDespachoEnc.User_mod,
                                                                                                         lConnection,
                                                                                                         lTransaction)
                            End If
                        End If
                    End If

                    '#EJC20240924: La cumbre, interface SAP, despachos parciales.
                    If BeConfigEnc.Interface_SAP Then
                        '#EJC20250609: Validar si no afecta esta condición en la cumbre.
                        vEnviado = BePedidoEnc.Enviado_A_ERP

                        '#EJC20251010: Si ya tiene no_picking_erp = transferencia_sap (para Killios), entonces actualizo el no_picking_erp vacío.
                        'para que se pueda enviar nuevamente por la interface.
                        Dim vNoPickingErp As String = clsLnTrans_pe_enc.Get_No_Picking_ERP_By_IdPedidoEnc(BePedidoEnc.IdPedidoEnc, lConnection, lTransaction)
                        If Not vNoPickingErp = "" Then
                            BePedidoEnc.No_Picking_ERP = ""
                            pBeDespachoEnc.No_pase = 0
                            clsLnTrans_pe_enc.Actualizar_No_Picking_ERP(BePedidoEnc, lConnection, lTransaction)
                            clsLnTrans_despacho_enc.Actualizar_No_Pase(pBeDespachoEnc, lConnection, lTransaction)
                        End If

                    End If

                    For Each BeDespachoDet As clsBeTrans_despacho_det In pBeDespachoEnc.ListaDetalle.Where(Function(x) (x.IdPedidoEnc = BePedidoEnc.IdPedidoEnc AndAlso
                                                                                                               x.CantidadDespachada > 0 AndAlso
                                                                                                               x.IdDespachoEnc = pBeDespachoEnc.IdDespachoEnc))

                        Dim BePickingUbic As New clsBeTrans_picking_ubic
                        BePickingUbic = clsLnTrans_picking_ubic.Get_PickingUbic_By_IdPickingUbic(BeDespachoDet.IdPickingUbic, lConnection, lTransaction)

                        Dim BePedidoDet As New clsBeTrans_pe_det
                        BePedidoDet = clsLnTrans_pe_det.Get_Single_By_IdPedidoEnc_And_IdPedidoDet(BeDespachoDet.IdPedidoEnc,
                                                                                                  BeDespachoDet.IdPedidoDet,
                                                                                                  lConnection,
                                                                                                  lTransaction)

                        Dim BeI_nav_transacciones_out As New clsBeI_nav_transacciones_out()

                        With BeI_nav_transacciones_out

                            .Idtransaccion = lMaxS
                            .Idempresa = pIdEmpresa
                            .Idbodega = pIdBodega
                            .Idpropietario = BePedidoEnc.PropietarioBodega.Propietario.IdPropietario
                            .Idpropietariobodega = BePedidoEnc.IdPropietarioBodega
                            .Idordencompra = 0
                            .Idrecepcionenc = BePickingUbic.IdRecepcion
                            .Idpedidoenc = BePedidoEnc.IdPedidoEnc
                            .Iddespachoenc = pBeDespachoEnc.IdDespachoEnc
                            .IdDespachoDet = BeDespachoDet.IdDespachoDet
                            .Idproductobodega = BePickingUbic.IdProductoBodega
                            .Idproducto = clsLnProducto.Get_Id_Producto_By_IdProductoBodega(BePickingUbic.IdProductoBodega,
                                                                                            lConnection,
                                                                                            lTransaction)
                            .Idunidadmedida = BePickingUbic.IdUnidadMedida

                            '#EJC20180614: Si el pedido no fue creado con presentación, reflejar cantidad en umbas, sin presentación
                            If BePickingUbic.IdPresentacion <> 0 Then

                                .Idpresentacion = BePickingUbic.IdPresentacion

                                BePresentacionPedido = clsLnProducto_presentacion.Get_BeProductoPresentacion_By_IdPresentacion_And_IdProducto(BePickingUbic.IdPresentacion,
                                                                                                                                              BeI_nav_transacciones_out.Idproducto,
                                                                                                                                              lConnection,
                                                                                                                                              lTransaction)
                                If Not BePresentacionPedido Is Nothing Then

                                    BePickingUbic.Cantidad_Verificada = Math.Round(BePickingUbic.Cantidad_Verificada * BePresentacionPedido.Factor, 6)

                                Else
                                    Throw New Exception("#20211216: No se pudo obtener la presentación del pedido para el código de producto: " & BePickingUbic.CodigoProducto)
                                End If

                                '#EJC20220411:Para BYB, necesito saber si el producto va en presentación.
                                If Not BeConfigEnc.Equiparar_Productos Then
                                    If Not BeTipoDocumento.IdTipoPedido = clsDataContractDI.tTipoDocumentoSalida.Pedido_De_Venta_NAV And Not BeTipoDocumento.IdTipoPedido = 4 Then
                                        '#EJC20191122:Como se convierte a UMBas, mantener sin pres.
                                        .Idpresentacion = 0
                                    End If
                                End If

                            Else
                                .Idpresentacion = 0
                            End If

                            .Idproductoestado = BePickingUbic.IdProductoEstado

                            .Cantidad = BeDespachoDet.CantidadDespachada
                            .Cantidad_Pendiente = BePickingUbic.Cantidad_Solicitada - BeDespachoDet.CantidadDespachada
                            .Cantidad_Enviada = 0
                            .Peso = BeDespachoDet.PesoDespachado
                            .Lote = BeDespachoDet.Lote
                            .Fecha_vence = BePickingUbic.Fecha_Vence
                            .Fecha_recepcion = "01/01/1900"
                            .fecha_despacho = Now
                            .No_pedido = BePedidoEnc.Referencia
                            .No_linea = BePedidoDet.No_linea
                            '.Codigo_producto = BePedidoDet.Producto.Codigo
                            .Codigo_producto = BePedidoDet.Codigo_Producto
                            .codigo_barra = clsLnProducto.Get_CodigoBarra_By_IdProducto(BeI_nav_transacciones_out.Idproducto,
                                                                                        lConnection,
                                                                                        lTransaction)
                            .Nombre_producto = BePedidoDet.Nombre_producto

                            If BePickingUbic.IdPresentacion <> 0 Then
                                If BePedidoDet.Atributo_Variante_1 = "" Then
                                    .Codigo_variante = BePresentacionPedido.Codigo_barra
                                Else
                                    .Codigo_variante = BePedidoDet.Atributo_Variante_1
                                End If
                            Else
                                .Codigo_variante = ""
                            End If

                            .Unidad_medida = BePedidoDet.Nom_unid_med
                            .IdTipoDocumento = IIf(BePedidoEnc.TipoPedido.IdTipoPedido = 0,
                                                   BePedidoEnc.IdTipoPedido,
                                                   BePedidoEnc.TipoPedido.IdTipoPedido)
                            .Tipo_transaccion = "SALIDA"
                            .Lic_Plate = BePickingUbic.Lic_plate

                            If BeConfigEnc.Interface_SAP Then
                                .Enviado = vEnviado
                            Else
                                .Enviado = IIf(Not BePedidoEnc.Enviado_A_ERP, vEnviado, BePedidoEnc.Enviado_A_ERP)
                            End If

                            .Fec_agr = Now
                            .User_agr = pBeDespachoEnc.User_agr
                            .User_mod = pBeDespachoEnc.User_mod
                            .IdProductoTallaColor = BeDespachoDet.IdProductoTallaColor
                            .Talla = BeDespachoDet.Talla
                            .Color = BeDespachoDet.Color

                        End With

                        If Not BeDespachoDet.CantidadDespachada = 0 Then
                            Insertar(BeI_nav_transacciones_out,
                                     lConnection,
                                     lTransaction)
                        Else
                            Throw New Exception("Algo muy raro ha sucedido, se ha intentado enviar al ERP un despacho con cantidad 0, la razón mas probable por la que esto puede suceder (especulo) es porque el factor de la presentación es 0 o el producto sufrió una condición de no verificación (que sería muy extraño puesto que se está despachando, lamentablmente debo detener el envío para poder corregirlo, notificar por favor a Erik.)")
                        End If

                        lMaxS += 1

                    Next BeDespachoDet

                Next BePedidoEnc

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Sub Insertar_Salida_Origen(ByVal pIdEmpresa As Integer,
                                             ByVal pIdBodega As Integer,
                                             ByVal pBeDespachoEnc As clsBeTrans_despacho_enc,
                                             ByRef lConnection As SqlConnection,
                                             ByRef lTransaction As SqlTransaction)

        Try

            Dim Factor As Double = 0
            Dim vEnviado As Boolean = False

            If pBeDespachoEnc IsNot Nothing Then

                Dim lMaxS As Integer = MaxID(lConnection,
                                             lTransaction) + 1

                Dim BePresentacionPedido As New clsBeProducto_Presentacion()
                Dim BeTipoDocumento As New clsBeTrans_pe_tipo()

                For Each BePedidoEnc As clsBeTrans_pe_enc In pBeDespachoEnc.ListaPedidos

                    '#EJC20220323D: Considerar el tipo de documento en despacho, al insertar registros de interface, bandera enviado.
                    BeTipoDocumento = clsLnTrans_pe_tipo.Get_Single_By_IdTipoPedido(BePedidoEnc.IdTipoPedido,
                                                                                    lConnection,
                                                                                    lTransaction)

                    If Not BeTipoDocumento Is Nothing Then
                        vEnviado = BeTipoDocumento.Marcar_Registros_Enviados_MI3
                    End If

                    '#EJC20220517:Marcar el documento a petición de Marcelo, Mercosal 17052022_1829:
                    If vEnviado AndAlso Not BePedidoEnc.Enviado_A_ERP Then
                        clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP_By_IdPedidoEnc(BePedidoEnc.IdPedidoEnc, True, pBeDespachoEnc.User_mod, lConnection, lTransaction)
                    End If

                    For Each BePedidoDet As clsBeTrans_pe_det In BePedidoEnc.Detalle

                        If Not BePedidoDet.ListaPickingUbic Is Nothing Then

                            If BePedidoDet.Producto.Codigo.Trim = "" Then
                                Throw New Exception("#EJC20200210: Excepción de código no válido para transacciones MI3")
                            End If

                            '#GT03082022: se valida contra cantidad =0 para no duplicar en despachos parciales
                            Dim lPickingUbicVerificados = BePedidoDet.ListaPickingUbic.Where(Function(x) (x.Cantidad_Verificada > 0 OrElse x.Peso_verificado > 0) _
                                                                                             AndAlso x.Cantidad_despachada > 0).ToList()


                            If Not lPickingUbicVerificados Is Nothing Then

                                For Each BePickingUbic As clsBeTrans_picking_ubic In lPickingUbicVerificados

                                    Dim ObjM As New clsBeI_nav_transacciones_out()

                                    With ObjM

                                        .Idtransaccion = lMaxS
                                        .Idempresa = pIdEmpresa
                                        .Idbodega = pIdBodega
                                        .Idpropietario = BePedidoEnc.PropietarioBodega.Propietario.IdPropietario
                                        .Idpropietariobodega = BePedidoEnc.IdPropietarioBodega
                                        .Idordencompra = 0
                                        .Idrecepcionenc = BePickingUbic.IdRecepcion
                                        .Idpedidoenc = BePedidoEnc.IdPedidoEnc
                                        .Iddespachoenc = pBeDespachoEnc.IdDespachoEnc
                                        .Idproductobodega = BePickingUbic.IdProductoBodega
                                        .Idproducto = clsLnProducto.Get_Id_Producto_By_IdProductoBodega(BePickingUbic.IdProductoBodega,
                                                                                                        lConnection,
                                                                                                        lTransaction)
                                        .Idunidadmedida = BePickingUbic.IdUnidadMedida

                                        '#EJC20180614: Si el pedido no fue creado con presentación, reflejar cantidad en umbas, sin presentación
                                        If BePickingUbic.IdPresentacion <> 0 Then
                                            .Idpresentacion = BePickingUbic.IdPresentacion

                                            BePresentacionPedido = clsLnProducto_presentacion.Get_BeProductoPresentacion_By_IdPresentacion_And_IdProducto(BePickingUbic.IdPresentacion,
                                                                                                                                                          ObjM.Idproducto,
                                                                                                                                                          lConnection,
                                                                                                                                                          lTransaction)

                                            If Not BePresentacionPedido Is Nothing Then

                                                BePickingUbic.Cantidad_Verificada = Math.Round(BePickingUbic.Cantidad_Verificada * BePresentacionPedido.Factor, 6)

                                            Else
                                                Throw New Exception("#20211216: No se pudo obtener la presentación del pedido para el código de producto: " & BePickingUbic.CodigoProducto)
                                            End If

                                            '#EJC20220411:Para BYB, necesito saber si el producto va en presentación.
                                            If Not BeTipoDocumento.IdTipoPedido = clsDataContractDI.tTipoDocumentoSalida.Pedido_De_Venta_NAV AndAlso
                                               Not BeTipoDocumento.IdTipoPedido = clsDataContractDI.tTipoDocumentoSalida.Devolucion_Proveedor Then
                                                '#EJC20191122:Como se convierte a UMBas, mantener sin pres.
                                                .Idpresentacion = 0
                                            End If

                                        Else
                                            .Idpresentacion = 0
                                        End If

                                        .Idproductoestado = BePickingUbic.IdProductoEstado
                                        .Cantidad = BePickingUbic.Cantidad_Verificada
                                        .Cantidad_Pendiente = BePickingUbic.Cantidad_Verificada
                                        .Cantidad_Enviada = 0
                                        .Peso = BePickingUbic.Peso_verificado
                                        .Lote = BePickingUbic.Lote
                                        .Fecha_vence = BePickingUbic.Fecha_Vence
                                        .Fecha_recepcion = "01/01/1900"
                                        .fecha_despacho = Now
                                        .No_pedido = BePedidoEnc.Referencia
                                        .No_linea = BePedidoDet.No_linea
                                        .Codigo_producto = BePedidoDet.Producto.Codigo
                                        .codigo_barra = clsLnProducto.Get_CodigoBarra_By_IdProducto(ObjM.Idproducto,
                                                                                                    lConnection,
                                                                                                    lTransaction)
                                        .Nombre_producto = BePedidoDet.Producto.Nombre

                                        If BePickingUbic.IdPresentacion <> 0 Then
                                            If BePedidoDet.Atributo_Variante_1 = "" Then
                                                .Codigo_variante = BePresentacionPedido.Codigo_barra
                                            Else
                                                .Codigo_variante = BePedidoDet.Atributo_Variante_1
                                            End If
                                        Else
                                            .Codigo_variante = ""
                                        End If

                                        .Unidad_medida = BePedidoDet.Nom_unid_med
                                        .IdTipoDocumento = IIf(BePedidoEnc.TipoPedido.IdTipoPedido = 0,
                                                               BePedidoEnc.IdTipoPedido,
                                                               BePedidoEnc.TipoPedido.IdTipoPedido)
                                        .Tipo_transaccion = "SALIDA"
                                        .Lic_Plate = BePickingUbic.Lic_plate
                                        '#EJC20190902: Si se marca esta bandera en la pantalla de pedido, se debe colocar el registro como enviado a la interface para que la interface no lo procese.
                                        '#EJC20220323: Considerar flag del tipo de documento para determinar el valor enviado en i_nav_transacciones_out-
                                        .Enviado = IIf(Not BePedidoEnc.Enviado_A_ERP, vEnviado, BePedidoEnc.Enviado_A_ERP)
                                        .Fec_agr = Now
                                        .User_agr = pBeDespachoEnc.User_agr
                                        .User_mod = pBeDespachoEnc.User_mod

                                    End With

                                    If Not BePickingUbic.Cantidad_Verificada = 0 Then
                                        Insertar(ObjM,
                                                 lConnection,
                                                 lTransaction)
                                    Else
                                        Throw New Exception("Algo muy raro ha sucedido, se ha intentado enviar al ERP un despacho con cantidad 0, la razón mas probable por la que esto puede suceder (especulo) es porque el factor de la presentación es 0 o el producto sufrió una condición de no verificación (que sería muy extraño puesto que se está despachando, lamentablmente debo detener el envío para poder corregirlo, notificar por favor a Erik.)")
                                    End If

                                    lMaxS += 1

                                Next BePickingUbic

                            End If

                        End If 'Fin si, producto pickeado, verificado                       

                    Next BePedidoDet

                Next BePedidoEnc

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function MaxID(ByVal pConnection As SqlConnection,
                                 ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(idtransaccion),0) FROM I_nav_transacciones_out"

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

    Public Shared Function Get_Lotes_Ingreso_Pendientes_Envio() As List(Of clsBeI_nav_transacciones_out)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lReturnList As New List(Of clsBeI_nav_transacciones_out)

            Dim sp As String = "SELECT * FROM I_nav_transacciones_out 
                                  WHERE tipo_transaccion = 'INGRESO'
                                  AND enviado = 0 AND idrecepcionenc in (SELECT IdRecepcionEnc 
                                                  FROM trans_re_enc  
                                                  WHERE estado = 'Cerrado') "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_transacciones_out As New clsBeI_nav_transacciones_out

            For Each dr As DataRow In dt.Rows
                vBeI_nav_transacciones_out = New clsBeI_nav_transacciones_out
                Cargar(vBeI_nav_transacciones_out, dr)
                lReturnList.Add(vBeI_nav_transacciones_out)
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

    Public Shared Function Get_Lotes_Ingreso_Pendientes_Envio_By_IdBodega(ByVal pIdBodega As Integer) As List(Of clsBeI_nav_transacciones_out)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lReturnList As New List(Of clsBeI_nav_transacciones_out)

            Dim sp As String = "SELECT * FROM I_nav_transacciones_out 
                                WHERE tipo_transaccion = 'INGRESO'
                                AND enviado = 0 and IdBodega = @IdBodega"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            cmd.Parameters.AddWithValue("@IdBodega", pIdBodega)
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_transacciones_out As New clsBeI_nav_transacciones_out

            For Each dr As DataRow In dt.Rows
                vBeI_nav_transacciones_out = New clsBeI_nav_transacciones_out
                Cargar(vBeI_nav_transacciones_out, dr)
                lReturnList.Add(vBeI_nav_transacciones_out)
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
    Public Shared Function Get_Lotes_Salida_Pendientes_Envio(Optional ByVal pTipo As clsDataContractDI.tTipoDocumentoSalida = 0) As List(Of clsBeI_nav_transacciones_out)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lReturnList As New List(Of clsBeI_nav_transacciones_out)

            Dim sp As String = "SELECT * FROM I_nav_transacciones_out 
                                WHERE tipo_transaccion = 'SALIDA'  
                                AND enviado = 0 "

            sp &= IIf(pTipo <> 0, " AND IdTipoDocumento = " & pTipo, "")

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_transacciones_out As New clsBeI_nav_transacciones_out

            For Each dr As DataRow In dt.Rows

                vBeI_nav_transacciones_out = New clsBeI_nav_transacciones_out
                Cargar(vBeI_nav_transacciones_out, dr)

                vBeI_nav_transacciones_out.Codigo_Bodega_Destino = clsLnCliente.Get_Codigo_By_IdCliente(clsLnTrans_pe_enc.GetIdCliente(vBeI_nav_transacciones_out.Idpedidoenc, lConnection, lTransaction), lConnection, lTransaction)
                vBeI_nav_transacciones_out.Codigo_Bodega_Origen = clsLnBodega.Get_Codigo_By_IdBodega(vBeI_nav_transacciones_out.Idbodega, lConnection, lTransaction)

                lReturnList.Add(vBeI_nav_transacciones_out)

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
    Public Shared Function Get_Lotes_Salida_Pendientes_Envio(ByVal NoPedido As String) As List(Of clsBeI_nav_transacciones_out)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lReturnList As New List(Of clsBeI_nav_transacciones_out)

            Dim sp As String = "SELECT * FROM I_nav_transacciones_out 
                                WHERE tipo_transaccion = 'SALIDA'  
                                AND   no_pedido = @no_pedido "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@no_pedido", NoPedido)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_transacciones_out As New clsBeI_nav_transacciones_out

            For Each dr As DataRow In dt.Rows
                vBeI_nav_transacciones_out = New clsBeI_nav_transacciones_out
                Cargar(vBeI_nav_transacciones_out, dr)
                lReturnList.Add(vBeI_nav_transacciones_out)
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

    Public Shared Function Get_Lotes_Salida_Pendientes_Envio_By_IdBodega(ByVal pIdBodega As Integer) As List(Of clsBeI_nav_transacciones_out)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lReturnList As New List(Of clsBeI_nav_transacciones_out)

            Dim sp As String = "SELECT * FROM I_nav_transacciones_out 
                                WHERE tipo_transaccion = 'SALIDA'  
                                AND IdBodega = @IdBodega AND enviado = 0 AND no_pedido <> '' "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_transacciones_out As New clsBeI_nav_transacciones_out

            For Each dr As DataRow In dt.Rows
                vBeI_nav_transacciones_out = New clsBeI_nav_transacciones_out
                Cargar(vBeI_nav_transacciones_out, dr)
                vBeI_nav_transacciones_out.Codigo_Bodega_Origen = clsLnBodega.Get_Codigo_By_IdBodega(vBeI_nav_transacciones_out.Idbodega, lConnection, lTransaction)
                lReturnList.Add(vBeI_nav_transacciones_out)
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

    Public Shared Sub Insertar_Ajuste(ByVal pIdEmpresa As Integer,
                                      ByVal pIdBodega As Integer,
                                      ByVal pListObjDetAj As List(Of clsBeTrans_ajuste_det),
                                      ByRef pIdUsuario As Integer,
                                      ByRef lConnection As SqlConnection,
                                      ByRef lTransaction As SqlTransaction)
        Try

            Dim Presentacion As New clsBeProducto_Presentacion()

            If pListObjDetAj IsNot Nothing Then

                Dim lMaxS As Integer = MaxID(lConnection, lTransaction) + 1

                For Each BeTransAjusteDet As clsBeTrans_ajuste_det In pListObjDetAj

                    Dim BeI_nav_transacciones_out As New clsBeI_nav_transacciones_out()

                    BeI_nav_transacciones_out.Idtransaccion = lMaxS
                    BeI_nav_transacciones_out.Idempresa = pIdEmpresa
                    BeI_nav_transacciones_out.Idbodega = pIdBodega
                    BeI_nav_transacciones_out.Idpropietario = clsLnPropietarios.Get_IdPropietario(pIdBodega,
                                                                             BeTransAjusteDet.IdPropietarioBodega,
                                                                             lConnection,
                                                                             lTransaction)

                    BeI_nav_transacciones_out.Idpropietariobodega = BeTransAjusteDet.IdPropietarioBodega
                    BeI_nav_transacciones_out.Idordencompra = 0
                    BeI_nav_transacciones_out.Idrecepcionenc = 0
                    BeI_nav_transacciones_out.Idpedidoenc = 0
                    BeI_nav_transacciones_out.Iddespachoenc = 0
                    BeI_nav_transacciones_out.Idproductobodega = BeTransAjusteDet.IdProductoBodega
                    BeI_nav_transacciones_out.Idproducto = clsLnProducto.Get_Id_Producto_By_IdProductoBodega(BeTransAjusteDet.IdProductoBodega,
                                                                                        lConnection,
                                                                                        lTransaction)
                    BeI_nav_transacciones_out.Idunidadmedida = BeTransAjusteDet.IdUnidadMedida
                    BeI_nav_transacciones_out.Idpresentacion = BeTransAjusteDet.IdPresentacion
                    BeI_nav_transacciones_out.Idproductoestado = BeTransAjusteDet.IdProductoEstado
                    BeI_nav_transacciones_out.Cantidad = BeTransAjusteDet.Cantidad_nueva
                    BeI_nav_transacciones_out.Peso = BeTransAjusteDet.Peso_nuevo
                    BeI_nav_transacciones_out.Lote = BeTransAjusteDet.Lote_nuevo
                    BeI_nav_transacciones_out.Fecha_vence = BeTransAjusteDet.Fecha_vence_nueva
                    BeI_nav_transacciones_out.Fecha_recepcion = Now
                    BeI_nav_transacciones_out.No_pedido = 0

                    BeI_nav_transacciones_out.No_linea = 0
                    BeI_nav_transacciones_out.Codigo_producto = BeTransAjusteDet.Codigo_producto
                    BeI_nav_transacciones_out.Nombre_producto = BeTransAjusteDet.Nombre_producto
                    BeI_nav_transacciones_out.Codigo_variante = 0

                    If BeI_nav_transacciones_out.Idpresentacion = 0 Then
                        BeI_nav_transacciones_out.Unidad_medida = clsLnUnidad_medida.Get_Nombre_By_IdUnidadMedida(BeTransAjusteDet.IdUnidadMedida, lConnection, lTransaction)
                    Else
                        Presentacion = clsLnProducto_presentacion.GetSingle(BeTransAjusteDet.IdPresentacion, lConnection, lTransaction)
                        BeI_nav_transacciones_out.Unidad_medida = Presentacion.Nombre
                    End If

                    BeI_nav_transacciones_out.Tipo_transaccion = "AJUSTE"
                    BeI_nav_transacciones_out.Enviado = 0
                    BeI_nav_transacciones_out.Fec_agr = Now
                    BeI_nav_transacciones_out.User_agr = pIdUsuario
                    BeI_nav_transacciones_out.User_mod = pIdUsuario

                    Insertar(BeI_nav_transacciones_out, lConnection, lTransaction)

                    lMaxS += 1

                Next

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Get_All_By_Fecha(ByVal pFechaDel As Date,
                                            ByVal pFechaAl As Date) As List(Of clsBeI_nav_transacciones_out)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lReturnList As New List(Of clsBeI_nav_transacciones_out)

            Dim vSQL As String = "SELECT * FROM I_nav_transacciones_out "

            vSQL += String.Format(" Where cast(fec_agr AS DATE) BETWEEN {0} AND {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            vSQL += " ORDER BY fec_agr "

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_transacciones_out As New clsBeI_nav_transacciones_out

            For Each dr As DataRow In dt.Rows
                vBeI_nav_transacciones_out = New clsBeI_nav_transacciones_out
                Cargar(vBeI_nav_transacciones_out, dr)
                lReturnList.Add(vBeI_nav_transacciones_out)
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

    Public Shared Function Get_All_Ingresos_Pendientes_De_Envio_By_IdTipoDocumento(ByVal pIdTipoDocumento As Integer) As List(Of clsBeI_nav_transacciones_out)

        Get_All_Ingresos_Pendientes_De_Envio_By_IdTipoDocumento = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lReturnList As New List(Of clsBeI_nav_transacciones_out)

            Dim vSQL As String = "SELECT * FROM I_nav_transacciones_out WHERE IdTipoDocumento =@IdTipoDocumento"

            vSQL += String.Format(" AND tipo_transaccion = 'INGRESO' AND Enviado =0 ")

            vSQL += " ORDER BY fec_agr "

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.SelectCommand.Parameters.AddWithValue("@IdTipoDocumento", pIdTipoDocumento)
            dad.Fill(dt)

            Dim vBeI_nav_transacciones_out As New clsBeI_nav_transacciones_out

            For Each dr As DataRow In dt.Rows
                vBeI_nav_transacciones_out = New clsBeI_nav_transacciones_out
                Cargar(vBeI_nav_transacciones_out, dr)
                lReturnList.Add(vBeI_nav_transacciones_out)
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

    Public Shared Function Get_All_Ingresos_Pendientes_De_Envio_By_IdTipoDocumento(ByVal pIdTipoDocumento As Integer, ByVal pNoPedido As String) As List(Of clsBeI_nav_transacciones_out)

        Get_All_Ingresos_Pendientes_De_Envio_By_IdTipoDocumento = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lReturnList As New List(Of clsBeI_nav_transacciones_out)

            Dim vSQL As String = "SELECT * FROM I_nav_transacciones_out WHERE IdTipoDocumento =@IdTipoDocumento"

            vSQL += String.Format(" AND tipo_transaccion = 'INGRESO' AND Enviado =0 ")

            If pNoPedido.Trim() <> "" Then
                vSQL += String.Format(" AND no_pedido = @NoPedido ")
            End If

            vSQL += " ORDER BY fec_agr "

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.SelectCommand.Parameters.AddWithValue("@IdTipoDocumento", pIdTipoDocumento)

            If pNoPedido.Trim() <> "" Then
                dad.SelectCommand.Parameters.AddWithValue("@NoPedido", pNoPedido)
            End If

            dad.Fill(dt)

            Dim vBeI_nav_transacciones_out As New clsBeI_nav_transacciones_out

            For Each dr As DataRow In dt.Rows
                vBeI_nav_transacciones_out = New clsBeI_nav_transacciones_out
                Cargar(vBeI_nav_transacciones_out, dr)
                lReturnList.Add(vBeI_nav_transacciones_out)
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

    Public Shared Function Get_All_Ingresos_Pendientes_De_Envio() As List(Of clsBeI_nav_transacciones_out)

        Get_All_Ingresos_Pendientes_De_Envio = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lReturnList As New List(Of clsBeI_nav_transacciones_out)

            Dim vSQL As String = "SELECT * FROM I_nav_transacciones_out "

            vSQL += String.Format(" Where tipo_transaccion = 'INGRESO' AND Enviado =0 ")

            vSQL += " ORDER BY fec_agr "

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_transacciones_out As New clsBeI_nav_transacciones_out

            For Each dr As DataRow In dt.Rows
                vBeI_nav_transacciones_out = New clsBeI_nav_transacciones_out
                Cargar(vBeI_nav_transacciones_out, dr)
                lReturnList.Add(vBeI_nav_transacciones_out)
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

    Public Shared Function Get_All_Ingresos_Pendientes_De_Envio(ByVal pNoPedido As String) As List(Of clsBeI_nav_transacciones_out)

        Get_All_Ingresos_Pendientes_De_Envio = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lReturnList As New List(Of clsBeI_nav_transacciones_out)

            Dim vSQL As String = "SELECT * FROM I_nav_transacciones_out "

            vSQL += String.Format(" Where (tipo_transaccion = 'INGRESO' AND Enviado =0 AND no_pedido = @NoPedido)")

            vSQL += " ORDER BY fec_agr "

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("NoPedido", pNoPedido)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_transacciones_out As New clsBeI_nav_transacciones_out

            For Each dr As DataRow In dt.Rows
                vBeI_nav_transacciones_out = New clsBeI_nav_transacciones_out
                Cargar(vBeI_nav_transacciones_out, dr)
                lReturnList.Add(vBeI_nav_transacciones_out)
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

    Public Shared Function Get_All_Despachos_Pendientes_De_Envio() As List(Of clsBeI_nav_transacciones_out)

        Get_All_Despachos_Pendientes_De_Envio = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lReturnList As New List(Of clsBeI_nav_transacciones_out)

            Dim vSQL As String = "SELECT * FROM I_nav_transacciones_out "
            vSQL += String.Format(" WHERE (tipo_transaccion = 'SALIDA' AND Enviado =0)")
            vSQL += " ORDER BY fec_agr "

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_transacciones_out As New clsBeI_nav_transacciones_out
            Dim BeDespachoEnc As New clsBeTrans_despacho_enc
            Dim BePedidoEnc As New clsBeTrans_pe_enc
            Dim IdBodegaWMS As Integer = 0
            Dim lBePedidoEnc As New List(Of clsBeTrans_pe_enc)
            Dim lBeDespachoEnc As New List(Of clsBeTrans_despacho_enc)
            Dim dictionaryOfBodegas As New Dictionary(Of Integer, String)
            Dim dictionaryOfBodegasCliente As New Dictionary(Of String, Integer)
            Dim idxDespacho As Integer = -1
            Dim idxPedido As Integer = -1

            For Each dr As DataRow In dt.Rows

                vBeI_nav_transacciones_out = New clsBeI_nav_transacciones_out

                Cargar(vBeI_nav_transacciones_out, dr)

                If Not dictionaryOfBodegas.TryGetValue(vBeI_nav_transacciones_out.Idbodega,
                                                       vBeI_nav_transacciones_out.Codigo_Bodega_Origen) Then

                    vBeI_nav_transacciones_out.Codigo_Bodega_Origen = clsLnBodega.Get_Codigo_By_IdBodega(vBeI_nav_transacciones_out.Idbodega,
                                                                                                         lConnection,
                                                                                                         lTransaction)

                    dictionaryOfBodegas.Add(vBeI_nav_transacciones_out.Idbodega,
                                            vBeI_nav_transacciones_out.Codigo_Bodega_Origen)

                End If

                idxDespacho = lBeDespachoEnc.FindIndex(Function(x) x.IdDespachoEnc = vBeI_nav_transacciones_out.Iddespachoenc)

                If idxDespacho <> -1 Then
                    BeDespachoEnc = New clsBeTrans_despacho_enc
                    BeDespachoEnc = lBeDespachoEnc(idxDespacho).Clone()
                Else
                    BeDespachoEnc = New clsBeTrans_despacho_enc
                    BeDespachoEnc = clsLnTrans_despacho_enc.GetSingle(vBeI_nav_transacciones_out.Iddespachoenc,
                                                                      lConnection,
                                                                      lTransaction)
                    lBeDespachoEnc.Add(BeDespachoEnc.Clone())
                End If

                '#EJC20190902: Datos de transporte.
                vBeI_nav_transacciones_out.Observacion = BeDespachoEnc.Observacion
                vBeI_nav_transacciones_out.Empresa_Transporte = BeDespachoEnc.NombreVehiculo
                vBeI_nav_transacciones_out.Piloto_Transporte = BeDespachoEnc.NombrePiloto
                vBeI_nav_transacciones_out.Contacto_Recibe = ""
                vBeI_nav_transacciones_out.Contacto_Entrega = ""
                vBeI_nav_transacciones_out.Placa_Transporte = BeDespachoEnc.Placa
                vBeI_nav_transacciones_out.TCN_Transporte = BeDespachoEnc.Placa_Comercial
                vBeI_nav_transacciones_out.Marchamo_No = BeDespachoEnc.Marchamo

                If vBeI_nav_transacciones_out.Idpedidoenc <> 0 Then

                    idxPedido = lBePedidoEnc.FindIndex(Function(x) x.IdPedidoEnc = vBeI_nav_transacciones_out.Idpedidoenc)

                    If idxPedido <> -1 Then
                        BePedidoEnc = New clsBeTrans_pe_enc()
                        BePedidoEnc = lBePedidoEnc(idxPedido)
                    Else
                        BePedidoEnc = New clsBeTrans_pe_enc
                        BePedidoEnc = clsLnTrans_pe_enc.GetSingle(vBeI_nav_transacciones_out.Idpedidoenc, lConnection, lTransaction)
                        lBePedidoEnc.Add(BePedidoEnc.Clone())
                    End If

                    If Not BePedidoEnc Is Nothing Then

                        If Not dictionaryOfBodegasCliente.TryGetValue(BePedidoEnc.Cliente.Codigo, IdBodegaWMS) Then
                            IdBodegaWMS = clsLnBodega.Get_IdBodega_By_Codigo(BePedidoEnc.Cliente.Codigo, lConnection, lTransaction)
                            dictionaryOfBodegasCliente.Add(BePedidoEnc.Cliente.Codigo, IdBodegaWMS)
                        End If

                        If Not dictionaryOfBodegas.TryGetValue(IdBodegaWMS, vBeI_nav_transacciones_out.Codigo_Bodega_Destino) Then
                            vBeI_nav_transacciones_out.Codigo_Bodega_Destino = clsLnBodega.Get_Codigo_By_IdBodega(IdBodegaWMS, lConnection, lTransaction)
                            dictionaryOfBodegas.Add(IdBodegaWMS, vBeI_nav_transacciones_out.Codigo_Bodega_Destino)
                        End If

                        If IdBodegaWMS <> 0 Then 'Es una transferencia hacia una bodega de WMS.
                            vBeI_nav_transacciones_out.Codigo_Cliente = ""
                        Else
                            vBeI_nav_transacciones_out.Codigo_Bodega_Destino = ""
                            vBeI_nav_transacciones_out.Codigo_Cliente = BePedidoEnc.Cliente.Codigo
                        End If

                        lReturnList.Add(vBeI_nav_transacciones_out)

                    Else
                        Throw New Exception("El pedido asociado con la transacción de despacho no existe (se anuló y eliminó)")
                    End If

                End If

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

    Public Shared Function Get_All_Despachos_Pendientes_De_Envio(Optional ByVal pNoPedido As String = "") As List(Of clsBeI_nav_transacciones_out)

        Get_All_Despachos_Pendientes_De_Envio = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lReturnList As New List(Of clsBeI_nav_transacciones_out)

            Dim vSQL As String = "SELECT * FROM I_nav_transacciones_out "

            vSQL += String.Format(" WHERE (tipo_transaccion = 'SALIDA' AND Enviado =0)")

            If pNoPedido <> "" Then
                vSQL += " AND (no_pedido = @NoPedido)"
            End If

            vSQL += " ORDER BY fec_agr "

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            If pNoPedido <> "" Then
                dad.SelectCommand.Parameters.AddWithValue("NoPedido", pNoPedido)
            End If

            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_transacciones_out As New clsBeI_nav_transacciones_out
            Dim BeDespachoEnc As New clsBeTrans_despacho_enc
            Dim BePedidoEnc As New clsBeTrans_pe_enc
            Dim IdBodegaWMS As Integer = 0
            Dim lBePedidoEnc As New List(Of clsBeTrans_pe_enc)
            Dim lBeDespachoEnc As New List(Of clsBeTrans_despacho_enc)
            Dim dictionaryOfBodegas As New Dictionary(Of Integer, String)
            Dim dictionaryOfBodegasCliente As New Dictionary(Of String, Integer)
            Dim idxDespacho As Integer = -1
            Dim idxPedido As Integer = -1

            For Each dr As DataRow In dt.Rows

                vBeI_nav_transacciones_out = New clsBeI_nav_transacciones_out

                Cargar(vBeI_nav_transacciones_out, dr)

                If Not dictionaryOfBodegas.TryGetValue(vBeI_nav_transacciones_out.Idbodega, vBeI_nav_transacciones_out.Codigo_Bodega_Origen) Then

                    vBeI_nav_transacciones_out.Codigo_Bodega_Origen = clsLnBodega.Get_Codigo_By_IdBodega(vBeI_nav_transacciones_out.Idbodega,
                                                                                                         lConnection,
                                                                                                         lTransaction)

                    dictionaryOfBodegas.Add(vBeI_nav_transacciones_out.Idbodega, vBeI_nav_transacciones_out.Codigo_Bodega_Origen)

                End If

                idxDespacho = lBeDespachoEnc.FindIndex(Function(x) x.IdDespachoEnc = vBeI_nav_transacciones_out.Iddespachoenc)

                If idxDespacho <> -1 Then
                    BeDespachoEnc = New clsBeTrans_despacho_enc
                    BeDespachoEnc = lBeDespachoEnc(idxDespacho).Clone()
                Else
                    BeDespachoEnc = New clsBeTrans_despacho_enc
                    BeDespachoEnc = clsLnTrans_despacho_enc.GetSingle(vBeI_nav_transacciones_out.Iddespachoenc, lConnection, lTransaction)
                    lBeDespachoEnc.Add(BeDespachoEnc.Clone())
                End If

                '#EJC20190902: Datos de transporte.
                vBeI_nav_transacciones_out.Observacion = BeDespachoEnc.Observacion
                vBeI_nav_transacciones_out.Empresa_Transporte = BeDespachoEnc.NombreVehiculo
                vBeI_nav_transacciones_out.Piloto_Transporte = BeDespachoEnc.NombrePiloto
                vBeI_nav_transacciones_out.Contacto_Recibe = ""
                vBeI_nav_transacciones_out.Contacto_Entrega = ""
                vBeI_nav_transacciones_out.Placa_Transporte = BeDespachoEnc.Placa
                vBeI_nav_transacciones_out.TCN_Transporte = BeDespachoEnc.Placa_Comercial
                vBeI_nav_transacciones_out.Marchamo_No = BeDespachoEnc.Marchamo

                If vBeI_nav_transacciones_out.Idpedidoenc <> 0 Then

                    idxPedido = lBePedidoEnc.FindIndex(Function(x) x.IdPedidoEnc = vBeI_nav_transacciones_out.Idpedidoenc)

                    If idxPedido <> -1 Then
                        BePedidoEnc = New clsBeTrans_pe_enc()
                        BePedidoEnc = lBePedidoEnc(idxPedido)
                    Else
                        BePedidoEnc = New clsBeTrans_pe_enc
                        BePedidoEnc = clsLnTrans_pe_enc.GetSingle(vBeI_nav_transacciones_out.Idpedidoenc,
                                                                  lConnection,
                                                                  lTransaction)
                        lBePedidoEnc.Add(BePedidoEnc.Clone())
                    End If

                    If Not BePedidoEnc Is Nothing Then

                        If Not dictionaryOfBodegasCliente.TryGetValue(BePedidoEnc.Cliente.Codigo, IdBodegaWMS) Then
                            IdBodegaWMS = clsLnBodega.Get_IdBodega_By_Codigo(BePedidoEnc.Cliente.Codigo,
                                                                             lConnection,
                                                                             lTransaction)
                            dictionaryOfBodegasCliente.Add(BePedidoEnc.Cliente.Codigo, IdBodegaWMS)
                        End If

                        If Not dictionaryOfBodegas.TryGetValue(IdBodegaWMS, vBeI_nav_transacciones_out.Codigo_Bodega_Destino) Then
                            vBeI_nav_transacciones_out.Codigo_Bodega_Destino = clsLnBodega.Get_Codigo_By_IdBodega(IdBodegaWMS,
                                                                                                                  lConnection,
                                                                                                                  lTransaction)
                            dictionaryOfBodegas.Add(IdBodegaWMS, vBeI_nav_transacciones_out.Codigo_Bodega_Destino)
                        End If

                        If IdBodegaWMS <> 0 Then 'Es una transferencia hacia una bodega de WMS.
                            'vBeI_nav_transacciones_out.Codigo_Bodega_Destino = clsLnBodega.Get_Codigo_By_IdBodega(IdBodegaWMS, lConnection, lTransaction)
                            vBeI_nav_transacciones_out.Codigo_Cliente = ""
                            vBeI_nav_transacciones_out.IdTipoDocumento = "Transferencia"
                        Else
                            vBeI_nav_transacciones_out.Codigo_Bodega_Destino = ""
                            vBeI_nav_transacciones_out.Codigo_Cliente = BePedidoEnc.Cliente.Codigo
                            vBeI_nav_transacciones_out.IdTipoDocumento = "Despacho"
                        End If

                        lReturnList.Add(vBeI_nav_transacciones_out)

                    Else
                        Throw New Exception("El pedido asociado con la transacción de despacho no existe (se anuló y eliminó)")
                    End If

                End If

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

    Public Shared Function Get_All_Despachos_Pendientes_De_Envio(Optional ByVal pNoPedido As String = "",
                                                                 Optional ByVal pIdTipoDocumento As Integer = 0) As List(Of clsBeI_nav_transacciones_out)

        Get_All_Despachos_Pendientes_De_Envio = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lReturnList As New List(Of clsBeI_nav_transacciones_out)

            Dim vSQL As String = "SELECT * FROM I_nav_transacciones_out "

            vSQL += String.Format(" WHERE (tipo_transaccion = 'SALIDA' AND Enviado =0)")

            If pNoPedido <> "" Then
                vSQL += " AND (no_pedido = @NoPedido)"
            End If

            If pIdTipoDocumento <> 0 Then
                vSQL += " AND (IdTipoDocumento = @IdTipoDocumento)"
            End If

            vSQL += " ORDER BY fec_agr "

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            If pNoPedido <> "" Then
                dad.SelectCommand.Parameters.AddWithValue("@NoPedido", pNoPedido)
            End If

            If pIdTipoDocumento <> 0 Then
                dad.SelectCommand.Parameters.AddWithValue("@IdTipoDocumento", pIdTipoDocumento)
            End If

            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_transacciones_out As New clsBeI_nav_transacciones_out
            Dim BeDespachoEnc As New clsBeTrans_despacho_enc
            Dim BePedidoEnc As New clsBeTrans_pe_enc
            Dim IdBodegaWMS As Integer = 0
            Dim lBePedidoEnc As New List(Of clsBeTrans_pe_enc)
            Dim lBeDespachoEnc As New List(Of clsBeTrans_despacho_enc)
            Dim dictionaryOfBodegas As New Dictionary(Of Integer, String)
            Dim dictionaryOfBodegasCliente As New Dictionary(Of String, Integer)
            Dim idxDespacho As Integer = -1
            Dim idxPedido As Integer = -1

            For Each dr As DataRow In dt.Rows

                vBeI_nav_transacciones_out = New clsBeI_nav_transacciones_out

                Cargar(vBeI_nav_transacciones_out, dr)

                If Not dictionaryOfBodegas.TryGetValue(vBeI_nav_transacciones_out.Idbodega, vBeI_nav_transacciones_out.Codigo_Bodega_Origen) Then
                    vBeI_nav_transacciones_out.Codigo_Bodega_Origen = clsLnBodega.Get_Codigo_By_IdBodega(vBeI_nav_transacciones_out.Idbodega,
                                                                                                         lConnection,
                                                                                                         lTransaction)
                    dictionaryOfBodegas.Add(vBeI_nav_transacciones_out.Idbodega, vBeI_nav_transacciones_out.Codigo_Bodega_Origen)
                End If

                idxDespacho = lBeDespachoEnc.FindIndex(Function(x) x.IdDespachoEnc = vBeI_nav_transacciones_out.Iddespachoenc)

                If idxDespacho <> -1 Then
                    BeDespachoEnc = New clsBeTrans_despacho_enc
                    BeDespachoEnc = lBeDespachoEnc(idxDespacho).Clone()
                Else
                    BeDespachoEnc = New clsBeTrans_despacho_enc
                    BeDespachoEnc = clsLnTrans_despacho_enc.GetSingle(vBeI_nav_transacciones_out.Iddespachoenc,
                                                                      lConnection,
                                                                      lTransaction)
                    lBeDespachoEnc.Add(BeDespachoEnc.Clone())
                End If

                '#EJC20190902: Datos de transporte.
                vBeI_nav_transacciones_out.Observacion = BeDespachoEnc.Observacion
                vBeI_nav_transacciones_out.Empresa_Transporte = BeDespachoEnc.NombreVehiculo
                vBeI_nav_transacciones_out.Piloto_Transporte = BeDespachoEnc.NombrePiloto
                vBeI_nav_transacciones_out.Contacto_Recibe = ""
                vBeI_nav_transacciones_out.Contacto_Entrega = ""
                vBeI_nav_transacciones_out.Placa_Transporte = BeDespachoEnc.Placa
                vBeI_nav_transacciones_out.TCN_Transporte = BeDespachoEnc.Placa_Comercial
                vBeI_nav_transacciones_out.Marchamo_No = BeDespachoEnc.Marchamo

                If vBeI_nav_transacciones_out.Idpedidoenc <> 0 Then

                    idxPedido = lBePedidoEnc.FindIndex(Function(x) x.IdPedidoEnc = vBeI_nav_transacciones_out.Idpedidoenc)

                    If idxPedido <> -1 Then
                        BePedidoEnc = New clsBeTrans_pe_enc()
                        BePedidoEnc = lBePedidoEnc(idxPedido)
                    Else
                        BePedidoEnc = New clsBeTrans_pe_enc
                        BePedidoEnc = clsLnTrans_pe_enc.GetSingle(vBeI_nav_transacciones_out.Idpedidoenc,
                                                                  lConnection,
                                                                  lTransaction)
                        lBePedidoEnc.Add(BePedidoEnc.Clone())
                    End If

                    If Not BePedidoEnc Is Nothing Then

                        If Not dictionaryOfBodegasCliente.TryGetValue(BePedidoEnc.Cliente.Codigo, IdBodegaWMS) Then
                            IdBodegaWMS = clsLnBodega.Get_IdBodega_By_Codigo(BePedidoEnc.Cliente.Codigo,
                                                                             lConnection,
                                                                             lTransaction)
                            dictionaryOfBodegasCliente.Add(BePedidoEnc.Cliente.Codigo, IdBodegaWMS)
                        End If

                        If Not dictionaryOfBodegas.TryGetValue(IdBodegaWMS, vBeI_nav_transacciones_out.Codigo_Bodega_Destino) Then
                            vBeI_nav_transacciones_out.Codigo_Bodega_Destino = clsLnBodega.Get_Codigo_By_IdBodega(IdBodegaWMS,
                                                                                                                  lConnection,
                                                                                                                  lTransaction)
                            dictionaryOfBodegas.Add(IdBodegaWMS, vBeI_nav_transacciones_out.Codigo_Bodega_Destino)
                        End If

                        If IdBodegaWMS <> 0 Then 'Es una transferencia hacia una bodega de WMS.
                            'vBeI_nav_transacciones_out.Codigo_Bodega_Destino = clsLnBodega.Get_Codigo_By_IdBodega(IdBodegaWMS, lConnection, lTransaction)
                            vBeI_nav_transacciones_out.Codigo_Cliente = ""
                            vBeI_nav_transacciones_out.IdTipoDocumento = "Transferencia"
                        Else
                            vBeI_nav_transacciones_out.Codigo_Bodega_Destino = ""
                            vBeI_nav_transacciones_out.Codigo_Cliente = BePedidoEnc.Cliente.Codigo
                            vBeI_nav_transacciones_out.IdTipoDocumento = "Despacho"
                        End If

                        lReturnList.Add(vBeI_nav_transacciones_out)

                    Else
                        Throw New Exception("El pedido asociado con la transacción de despacho no existe (se anuló y eliminó)")
                    End If

                End If

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

    Public Shared Function Actualizar_Bandera_Enviado(ByRef oBeI_nav_transacciones_out As clsBeI_nav_transacciones_out,
                                                      Optional ByVal pConection As SqlConnection = Nothing,
                                                      Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("i_nav_transacciones_out")
            Upd.Add("enviado", "@enviado", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("idtransaccion = @idtransaccion")

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

            cmd.Parameters.Add(New SqlParameter("@IDTRANSACCION", oBeI_nav_transacciones_out.Idtransaccion))
            cmd.Parameters.Add(New SqlParameter("@ENVIADO", oBeI_nav_transacciones_out.Enviado))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeI_nav_transacciones_out.Fec_mod))

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

    Public Shared Function Actualizar_Bandera_Enviado(ByRef loBeI_nav_transacciones_out As List(Of clsBeI_nav_transacciones_out),
                                                      Optional ByVal pConection As SqlConnection = Nothing,
                                                      Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim rowsAffected As Integer

        Try

            Upd.Init("i_nav_transacciones_out")
            Upd.Add("enviado", "@enviado", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("idtransaccion = @idtransaccion")

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

            For Each oBeI_nav_transacciones_out In loBeI_nav_transacciones_out

                If Es_Transaccion_Remota Then
                    cmd = New SqlCommand(sp, pConection)
                    cmd.Transaction = pTransaction
                Else
                    cmd = New SqlCommand(sp, lConnection, lTransaction)
                End If

                cmd.Parameters.Add(New SqlParameter("@IDTRANSACCION", oBeI_nav_transacciones_out.Idtransaccion))
                cmd.Parameters.Add(New SqlParameter("@ENVIADO", True))
                cmd.Parameters.Add(New SqlParameter("@FEC_MOD", Now))

                rowsAffected += cmd.ExecuteNonQuery()

            Next

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

    ''' <summary>
    ''' #EJC20240917: Actualizar cantidad_enviada,cantidad_pendiente, además de la bandera enviado By IdTransacción.
    ''' </summary>
    ''' <param name="loBeI_nav_transacciones_out"></param>
    ''' <param name="pConection"></param>
    ''' <param name="pTransaction"></param>
    ''' <returns></returns>
    Public Shared Function Actualizar_Estado_Enviado(ByRef loBeI_nav_transacciones_out As List(Of clsBeI_nav_transacciones_out),
                                                     Optional ByVal pConection As SqlConnection = Nothing,
                                                     Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim rowsAffected As Integer

        Try

            Upd.Init("i_nav_transacciones_out")
            Upd.Add("enviado", "@enviado", DataType.Parametro)
            Upd.Add("cantidad_enviada", "@cantidad_enviada", DataType.Parametro)
            Upd.Add("cantidad_pendiente", "@cantidad_pendiente", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("auditar", "@auditar", DataType.Parametro)
            Upd.Where("idtransaccion = @idtransaccion")

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

            For Each oBeI_nav_transacciones_out In loBeI_nav_transacciones_out

                If Es_Transaccion_Remota Then
                    cmd = New SqlCommand(sp, pConection)
                    cmd.Transaction = pTransaction
                Else
                    cmd = New SqlCommand(sp, lConnection, lTransaction)
                End If

                cmd.Parameters.Add(New SqlParameter("@IDTRANSACCION", oBeI_nav_transacciones_out.Idtransaccion))
                cmd.Parameters.Add(New SqlParameter("@ENVIADO", oBeI_nav_transacciones_out.Enviado))
                cmd.Parameters.Add(New SqlParameter("@FEC_MOD", Now))
                cmd.Parameters.Add(New SqlParameter("@CANTIDAD_ENVIADA", oBeI_nav_transacciones_out.Cantidad_Enviada))
                cmd.Parameters.Add(New SqlParameter("@CANTIDAD_PENDIENTE", oBeI_nav_transacciones_out.Cantidad_Pendiente))
                cmd.Parameters.Add(New SqlParameter("@AUDITAR", oBeI_nav_transacciones_out.Auditar))

                rowsAffected += cmd.ExecuteNonQuery()

            Next

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
    ''
    Public Shared Function Actualizar_Registro_Procesado_By_IdRecepcionEnc_And_Codigo_Producto(ByVal pNoPedido As String,
                                                                                               ByVal pIdRecepcionEnc As Integer,
                                                                                               ByVal pCodigo_Producto As String,
                                                                                               ByVal pEnviado As Boolean,
                                                                                               Optional ByVal pConection As SqlConnection = Nothing,
                                                                                               Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("i_nav_transacciones_out")
            Upd.Add("enviado", "@enviado", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("no_pedido = @no_pedido 
                       and IdRecepcionEnc = @IdRecepcionEnc
                       and codigo_producto = @codigo_producto")

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

            cmd.Parameters.Add(New SqlParameter("@no_pedido", pNoPedido))
            cmd.Parameters.Add(New SqlParameter("@IdRecepcionEnc", pIdRecepcionEnc))
            cmd.Parameters.Add(New SqlParameter("@codigo_producto", pCodigo_Producto))
            cmd.Parameters.Add(New SqlParameter("@enviado", pEnviado))
            cmd.Parameters.Add(New SqlParameter("@fec_mod", Now))


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

    Public Shared Function Actualizar_Registro_Procesado_By_IdDespacho_And_Codigo_Producto(ByVal pNoPedido As String,
                                                                                           ByVal IdDespachoEncWMS As Integer,
                                                                                           ByVal pCodigo_Producto As String,
                                                                                           ByVal pEnviado As Boolean,
                                                                                           Optional ByVal pConection As SqlConnection = Nothing,
                                                                                           Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("i_nav_transacciones_out")
            Upd.Add("enviado", "@enviado", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("no_pedido = @no_pedido 
                       and IdDespachoEnc = @IdDespachoEnc
                       and codigo_producto = @codigo_producto")

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

            cmd.Parameters.Add(New SqlParameter("@no_pedido", pNoPedido))
            cmd.Parameters.Add(New SqlParameter("@IdDespachoEnc", IdDespachoEncWMS))
            cmd.Parameters.Add(New SqlParameter("@codigo_producto", pCodigo_Producto))
            cmd.Parameters.Add(New SqlParameter("@enviado", pEnviado))
            cmd.Parameters.Add(New SqlParameter("@fec_mod", Now))


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

    Public Shared Function Actualizar_Registro_Procesado_By_NoPedido_And_Codigo_Producto(ByVal pNoPedido As String,
                                                                                         ByVal pCodigo_Producto As String,
                                                                                         ByVal pEnviado As Boolean,
                                                                                         Optional ByVal pConection As SqlConnection = Nothing,
                                                                                         Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("i_nav_transacciones_out")
            Upd.Add("enviado", "@enviado", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("no_pedido = @no_pedido                        
                       and codigo_producto = @codigo_producto")

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

            cmd.Parameters.Add(New SqlParameter("@no_pedido", pNoPedido))
            cmd.Parameters.Add(New SqlParameter("@codigo_producto", pCodigo_Producto))
            cmd.Parameters.Add(New SqlParameter("@enviado", pEnviado))
            cmd.Parameters.Add(New SqlParameter("@fec_mod", Now))


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

    Public Shared Function Actualizar_Bandera_Enviado_By_No_Pedido_And_IdTipoDocumento(ByVal pNoPedido As String,
                                                                                       ByVal pIdTipoDocumento As Integer,
                                                                                       ByVal pEnviado As Boolean,
                                                                                       Optional ByVal pConection As SqlConnection = Nothing,
                                                                                       Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("i_nav_transacciones_out")
            Upd.Add("enviado", "@enviado", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("no_pedido = @no_pedido and IdTipoDocumento = @IdTipoDocumento")

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

            cmd.Parameters.Add(New SqlParameter("@NO_PEDIDO", pNoPedido))
            cmd.Parameters.Add(New SqlParameter("@IDTIPODOCUMENTO", pIdTipoDocumento))
            cmd.Parameters.Add(New SqlParameter("@ENVIADO", pEnviado))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", Now))


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

    'Actualizar_Estado_Enviado_A_ERP_
    Public Shared Function Actualizar_Bandera_Enviado_By_No_Documento_Salida_Ref_Devol(ByVal pNoPedido As String,
                                                                                       ByVal pEnviado As Boolean,
                                                                                       Optional ByVal pConection As SqlConnection = Nothing,
                                                                                       Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("i_nav_transacciones_out")
            Upd.Add("enviado", "@enviado", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("no_documento_salida_ref_devol = @no_documento_salida_ref_devol")

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

            cmd.Parameters.Add(New SqlParameter("@no_documento_salida_ref_devol", pNoPedido))
            cmd.Parameters.Add(New SqlParameter("@ENVIADO", pEnviado))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", Now))


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

    Public Shared Function Actualizar_Bandera_Enviado_By_No_Pedido(ByVal pNoPedido As String,
                                                                   ByVal pEnviado As Boolean,
                                                                   Optional ByVal pConection As SqlConnection = Nothing,
                                                                   Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("i_nav_transacciones_out")
            Upd.Add("enviado", "@enviado", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("no_pedido = @no_pedido")

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

            cmd.Parameters.Add(New SqlParameter("@NO_PEDIDO", pNoPedido))
            cmd.Parameters.Add(New SqlParameter("@ENVIADO", pEnviado))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", Now))


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

    Public Shared Function Actualizar_Bandera_Enviado_By_IdPedidoEnc(ByVal pIdPedidoEnc As String,
                                                                     ByVal pEnviado As Boolean,
                                                                     ByVal pUser_Mod As Integer,
                                                                     Optional ByVal pConection As SqlConnection = Nothing,
                                                                     Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("i_nav_transacciones_out")
            Upd.Add("enviado", "@enviado", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Where("idpedidoenc = @idpedidoenc")

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

            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", pIdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@ENVIADO", pEnviado))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", Now))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", pUser_Mod))

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

    Public Shared Function Get_Ajustes_Pendientes_Envio_MI3(ByRef pResult As String,
                                                            ByRef lConnection As SqlConnection,
                                                            ByRef lTransaction As SqlTransaction) As List(Of clsBeAjustesMI3)

        Get_Ajustes_Pendientes_Envio_MI3 = Nothing

        Dim lAjustesMI3 As New List(Of clsBeAjustesMI3)
        Dim lblprg As New RichTextBox
        Dim vNDoc As String = ""
        Dim lVistaAjustesPendientesEnvio As New List(Of clsBe_vw_ajustes)
        Dim vDif As Double = 0
        Dim vNoDocumento As String = ""
        Dim BeAjusteDet As New clsBeTrans_ajuste_det
        Dim DetallesEnviados As Integer = 0
        Dim BeFamilia As New clsBeProducto_familia
        Dim vSerieBodega As String = ""
        Dim BeCliente As New clsBeCliente
        Dim AjusteDoc As New clsBeTrans_ajuste_det_doc
        Dim MaxIdAjusteDoc As Integer = 0
        Dim BeAjusteMI3 As New clsBeAjustesMI3
        Dim vNomenclaturaBase As String = ""
        Dim vCorrelativoActual As Integer = 0
        Dim lAjustesPendEnvio As New List(Of clsBeTrans_ajuste_enc)

        Try

            lAjustesPendEnvio = clsLnTrans_ajuste_enc.Get_All_Pendientes_Envio(lConnection, lTransaction)

            If Not lAjustesPendEnvio Is Nothing Then

                MaxIdAjusteDoc = clsLnTrans_ajuste_det_doc.MaxID(lConnection, lTransaction) + 1

                For Each AjEnc In lAjustesPendEnvio

                    vNoDocumento = Right("000000" & AjEnc.Idajusteenc, 6)

                    vNoDocumento = "WMS" + vNoDocumento
                    vNDoc = vNoDocumento

                    lVistaAjustesPendientesEnvio = clsLn_vw_ajustes.Get_All_Pendientes_Envio(AjEnc.Idajusteenc,
                                                                                             lConnection,
                                                                                             lTransaction)

                    DetallesEnviados = 0

                    '#EJC20180711: Obtener la sección para NAV (Debe llenarse a traves del mantenimiento de familias y se selecciona en el encabezado del ajuste)
                    If AjEnc.IdProductoFamilia > 0 Then
                        BeFamilia = clsLnProducto_familia.GetSingle(AjEnc.IdProductoFamilia,
                                                                    lConnection,
                                                                    lTransaction)
                    End If

                    If lVistaAjustesPendientesEnvio.Count > 0 Then

                        clsPublic.Actualizar_Progreso(lblprg, "Detalle de ajustes para transacción: " & AjEnc.Idajusteenc)

                        Dim vCodigoBodega As String = clsLnBodega.Get_Codigo_By_IdBodega(AjEnc.IdBodega,
                                                                                         lConnection,
                                                                                         lTransaction)
                        Dim vCodigoBodegaERP As String = ""

                        For Each AjDet In lVistaAjustesPendientesEnvio

                            BeCliente = clsLnCliente.GetSingle(AjDet.IdBodegaERP,
                                                               lConnection,
                                                               lTransaction)

                            If Not BeCliente Is Nothing Then
                                vSerieBodega = BeCliente.Referencia
                                vCodigoBodegaERP = BeCliente.Codigo
                            Else
                                vSerieBodega = AjDet.Seccion
                                vCodigoBodegaERP = AjDet.IdBodegaERP
                                BeCliente = New clsBeCliente
                                BeCliente.Codigo = AjDet.IdBodegaERP
                            End If

                            AjusteDoc = New clsBeTrans_ajuste_det_doc()

                            '#CKFK 20180927 0953PM Agregué esta condición porque cuando la serie es nueva el correlativo actual va a ser igual a 999999
                            If vCorrelativoActual = 0 Then
                                vCorrelativoActual = Val(vNoDocumento.Substring(4, vNoDocumento.Length - 4))
                                vCorrelativoActual += 1
                            ElseIf vCorrelativoActual = 999999 Then
                                vCorrelativoActual = 1
                            Else
                                vCorrelativoActual += 1
                            End If

                            vNoDocumento = vNomenclaturaBase + Right("000000" & vCorrelativoActual, 6)

                            clsPublic.Actualizar_Progreso(lblprg, "Procesando ajuste número de documento: " & vNoDocumento)

                            BeAjusteDet.IdAjusteDet = AjDet.IdAjusteDet
                            BeAjusteDet.IdAjusteEnc = AjEnc.Idajusteenc
                            clsLnTrans_ajuste_det.GetSingle(BeAjusteDet, lConnection, lTransaction)

                            If Not BeFamilia Is Nothing Then
                                AjDet.Seccion = BeFamilia.Nombre
                            End If

                            '#CM_20180828_2:54PM: Llena datos para el documento de ajustes. 
                            AjusteDoc.Idajustedoc = MaxIdAjusteDoc
                            AjusteDoc.Idajusteenc = AjEnc.Idajusteenc

                            If AjDet.Modifica_Cantidad Then

                                If AjDet.Cantidad_original > AjDet.Cantidad_nueva Then 'Es un ajuste negativo

                                    vDif = Math.Round(AjDet.Cantidad_original - AjDet.Cantidad_nueva, 6)

                                    Try

                                        BeAjusteMI3 = New clsBeAjustesMI3()
                                        BeAjusteMI3.IdAjusteEnc = AjDet.IdAjusteEnc
                                        BeAjusteMI3.IdAjusteDet = AjDet.IdAjusteDet
                                        BeAjusteMI3.Codigo_Bodega = vCodigoBodega
                                        BeAjusteMI3.Codigo_Bodega_ERP = vCodigoBodegaERP
                                        BeAjusteMI3.NoDocumento = vNoDocumento
                                        BeAjusteMI3.Codigo_Producto = AjDet.Codigo_Producto
                                        BeAjusteMI3.TipoAjusteERP = IIf(AjEnc.Ajuste_Por_Inventario > 0, AjDet.Codigo_Bodega, BeCliente.Codigo)
                                        BeAjusteMI3.TipoAjusteWMS = AjDet.Tipo_Ajuste
                                        BeAjusteMI3.UMBas = AjDet.UMBas
                                        BeAjusteMI3.Cantidad = vDif
                                        BeAjusteMI3.Lote = AjDet.Lote_Nuevo
                                        BeAjusteMI3.Motivo_Ajuste = AjDet.Motivo_Ajuste
                                        BeAjusteMI3.Observacion = AjDet.Observacion
                                        BeAjusteMI3.Seccion = AjDet.Seccion
                                        BeAjusteMI3.IdCentroCosto = AjEnc.IdCentroCosto
                                        BeAjusteMI3.Codigo_Centro_Costo = clsLnCentro_costo.Get_Codigo_By_IdCentroCosto(AjEnc.IdCentroCosto)
                                        BeAjusteMI3.Centro_Costo_Erp = AjEnc.Centro_Costo_Erp
                                        BeAjusteMI3.Centro_Costo_Dep_Erp = AjEnc.Centro_Costo_Dep_Erp
                                        BeAjusteMI3.Centro_Costo_Dir_Erp = AjEnc.Centro_Costo_Dir_Erp
                                        lAjustesMI3.Add(BeAjusteMI3)

                                        clsPublic.Actualizar_Progreso(lblprg, "Procesando ajuste negativo para: " & AjDet.Codigo_Producto & " " & AjDet.Nombre_Producto)

                                        DetallesEnviados += 1

                                    Catch ex As Exception

                                        clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                                   AjEnc.Idajusteenc,
                                                                                   0,
                                                                                   300)

                                        clsPublic.Actualizar_Progreso(lblprg, "Error al procesar el ajuste #: " & AjEnc.Idajusteenc)

                                        vCorrelativoActual -= 1

                                    End Try

                                ElseIf AjDet.Cantidad_original < AjDet.Cantidad_nueva Then 'Es un ajuste positivo

                                    vDif = Math.Round(AjDet.Cantidad_nueva - AjDet.Cantidad_original, 6)

                                    Try

                                        BeAjusteMI3 = New clsBeAjustesMI3()
                                        BeAjusteMI3.IdAjusteEnc = AjDet.IdAjusteEnc
                                        BeAjusteMI3.IdAjusteDet = AjDet.IdAjusteDet
                                        BeAjusteMI3.Codigo_Bodega = vCodigoBodega
                                        BeAjusteMI3.Codigo_Bodega_ERP = vCodigoBodegaERP
                                        BeAjusteMI3.NoDocumento = vNoDocumento
                                        BeAjusteMI3.Codigo_Producto = AjDet.Codigo_Producto
                                        BeAjusteMI3.TipoAjusteERP = IIf(AjEnc.Ajuste_Por_Inventario > 0, AjDet.Codigo_Bodega, BeCliente.Codigo)
                                        BeAjusteMI3.TipoAjusteWMS = AjDet.Tipo_Ajuste
                                        BeAjusteMI3.UMBas = AjDet.UMBas
                                        BeAjusteMI3.Cantidad = vDif
                                        BeAjusteMI3.Lote = AjDet.Lote_Nuevo
                                        BeAjusteMI3.Motivo_Ajuste = AjDet.Motivo_Ajuste
                                        BeAjusteMI3.Observacion = AjDet.Observacion
                                        BeAjusteMI3.Seccion = AjDet.Seccion
                                        BeAjusteMI3.IdCentroCosto = AjEnc.IdCentroCosto
                                        BeAjusteMI3.Codigo_Centro_Costo = clsLnCentro_costo.Get_Codigo_By_IdCentroCosto(AjEnc.IdCentroCosto)
                                        BeAjusteMI3.Centro_Costo_Erp = AjEnc.Centro_Costo_Erp
                                        BeAjusteMI3.Centro_Costo_Dep_Erp = AjEnc.Centro_Costo_Dep_Erp
                                        BeAjusteMI3.Centro_Costo_Dir_Erp = AjEnc.Centro_Costo_Dir_Erp
                                        lAjustesMI3.Add(BeAjusteMI3)

                                        clsPublic.Actualizar_Progreso(lblprg, "Procesando ajuste positivo para: " & AjDet.Codigo_Producto & " " & AjDet.Nombre_Producto)

                                        BeAjusteDet.Enviado = True

                                        If AjEnc.Referencia.Trim = "" Then
                                            AjEnc.Referencia = vNoDocumento
                                        Else
                                            AjEnc.Referencia = AjEnc.Referencia
                                        End If

                                        DetallesEnviados += 1

                                    Catch ex As Exception

                                        clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                                    AjEnc.Idajusteenc,
                                                                                    0,
                                                                                    300)

                                        clsPublic.Actualizar_Progreso(lblprg, "Error al procesar el ajuste #: " & AjEnc.Idajusteenc)

                                        vCorrelativoActual -= 1

                                    End Try

                                End If

                            Else

                                '#EJC20180615: Es un tipo de ajuste que no se puede enviar a ERP, pej. cambio de lote
                                BeAjusteDet.Enviado = True
                                clsLnTrans_ajuste_det.Actualizar(BeAjusteDet)
                                DetallesEnviados += 1

                            End If

                            MaxIdAjusteDoc += 1

                        Next

                        vCorrelativoActual = 0

                    Else

                        clsLnI_nav_ejecucion_det_error.Inserta_Log("No hay detalle de ajustes válidos para el Id de ajuste #: " & AjEnc.Idajusteenc,
                                                                   AjEnc.Idajusteenc,
                                                                   0,
                                                                   300)

                        clsPublic.Actualizar_Progreso(lblprg, "No hay detalle de ajustes válidos para el Id de ajuste #: " & AjEnc.Idajusteenc)

                    End If

                    pResult = lblprg.Text
                Next

                Return lAjustesMI3

            Else
                clsPublic.Actualizar_Progreso(lblprg, "No hay ajustes auditados pendientes de envío.")
            End If

            clsPublic.Actualizar_Progreso(lblprg, "Fin de sincronización de ajustes.")

        Catch ex As Exception

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "Sync_Ajustes",
                                                       0,
                                                       300,)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al enviar ajustes a NAV: {0}{1}-{2}", vbNewLine, ex.Message, vNDoc))

            Throw ex

        Finally
            pResult = lblprg.Text
        End Try

    End Function

    Public Shared Function Get_Ajustes_Pendientes_Envio_MI3(ByRef pResult As String) As List(Of clsBeAjustesMI3)

        Get_Ajustes_Pendientes_Envio_MI3 = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim lAjustesMI3 As New List(Of clsBeAjustesMI3)
        Dim lblprg As New RichTextBox
        Dim vNDoc As String = ""
        Dim lVistaAjustesPendientesEnvio As New List(Of clsBe_vw_ajustes)
        Dim vDif As Double = 0
        Dim vNoDocumento As String = ""
        Dim BeAjusteDet As New clsBeTrans_ajuste_det
        Dim DetallesEnviados As Integer = 0
        Dim BeFamilia As New clsBeProducto_familia
        Dim vSerieBodega As String = ""
        Dim BeCliente As New clsBeCliente
        Dim AjusteDoc As New clsBeTrans_ajuste_det_doc
        Dim MaxIdAjusteDoc As Integer = 0
        Dim BeAjusteMI3 As New clsBeAjustesMI3
        Dim vNomenclaturaBase As String = ""
        Dim vCorrelativoActual As Integer = 0
        Dim lAjustesPendEnvio As New List(Of clsBeTrans_ajuste_enc)

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            lAjustesPendEnvio = clsLnTrans_ajuste_enc.Get_All_Pendientes_Envio(lConnection, lTransaction)

            If Not lAjustesPendEnvio Is Nothing Then

                MaxIdAjusteDoc = clsLnTrans_ajuste_det_doc.MaxID(lConnection, lTransaction) + 1

                For Each AjEnc In lAjustesPendEnvio

                    vNoDocumento = Right("000000" & AjEnc.Idajusteenc, 6)

                    vNoDocumento = "WMS" + vNoDocumento
                    vNDoc = vNoDocumento

                    lVistaAjustesPendientesEnvio = clsLn_vw_ajustes.Get_All_Pendientes_Envio(AjEnc.Idajusteenc,
                                                                                             lConnection,
                                                                                             lTransaction)

                    DetallesEnviados = 0

                    '#EJC20180711: Obtener la sección para NAV (Debe llenarse a traves del mantenimiento de familias y se selecciona en el encabezado del ajuste)
                    If AjEnc.IdProductoFamilia > 0 Then
                        BeFamilia = clsLnProducto_familia.GetSingle(AjEnc.IdProductoFamilia,
                                                                    lConnection,
                                                                    lTransaction)
                    End If

                    If lVistaAjustesPendientesEnvio.Count > 0 Then

                        clsPublic.Actualizar_Progreso(lblprg, "Detalle de ajustes para transacción: " & AjEnc.Idajusteenc)

                        Dim vCodigoBodega As String = clsLnBodega.Get_Codigo_By_IdBodega(AjEnc.IdBodega,
                                                                                         lConnection,
                                                                                         lTransaction)
                        Dim vCodigoBodegaERP As String = ""

                        For Each AjDet In lVistaAjustesPendientesEnvio

                            BeCliente = clsLnCliente.Get_Single_By_Codigo(AjDet.IdBodegaERP,
                                                                          lConnection,
                                                                          lTransaction)

                            If BeCliente Is Nothing Then
                                clsPublic.Actualizar_Progreso(lblprg, "#EJC20200219_2214: No se encontró cliente/Serie para IdBodega: " & AjEnc.IdBodega)
                                vSerieBodega = ""
                                vCodigoBodegaERP = ""

                                If BeBodega.Interface_SAP Then
                                    vCodigoBodegaERP = BeBodega.Codigo
                                Else

                                End If
                            Else
                                vSerieBodega = BeCliente.Referencia
                                vCodigoBodegaERP = BeCliente.Codigo
                            End If

                            AjusteDoc = New clsBeTrans_ajuste_det_doc()

                            '#CKFK 20180927 0953PM Agregué esta condición porque cuando la serie es nueva el correlativo actual va a ser igual a 999999
                            If vCorrelativoActual = 0 Then
                                vCorrelativoActual = Val(vNoDocumento.Substring(4, vNoDocumento.Length - 4))
                                vCorrelativoActual += 1
                            ElseIf vCorrelativoActual = 999999 Then
                                vCorrelativoActual = 1
                            Else
                                vCorrelativoActual += 1
                            End If

                            vNoDocumento = vNomenclaturaBase + Right("000000" & vCorrelativoActual, 6)

                            clsPublic.Actualizar_Progreso(lblprg, "Procesando ajuste número de documento: " & vNoDocumento)

                            BeAjusteDet.IdAjusteDet = AjDet.IdAjusteDet
                            BeAjusteDet.IdAjusteEnc = AjEnc.Idajusteenc
                            clsLnTrans_ajuste_det.GetSingle(BeAjusteDet, lConnection, lTransaction)

                            If Not BeFamilia Is Nothing Then
                                AjDet.Seccion = BeFamilia.Nombre
                            End If

                            '#CM_20180828_2:54PM: Llena datos para el documento de ajustes. 
                            AjusteDoc.Idajustedoc = MaxIdAjusteDoc
                            AjusteDoc.Idajusteenc = AjEnc.Idajusteenc

                            If AjDet.Modifica_Cantidad Then

                                If AjDet.Cantidad_original > AjDet.Cantidad_nueva Then 'Es un ajuste negativo

                                    vDif = Math.Round(AjDet.Cantidad_original - AjDet.Cantidad_nueva, 6)

                                    Try

                                        BeAjusteMI3 = New clsBeAjustesMI3()
                                        BeAjusteMI3.IdAjusteEnc = AjDet.IdAjusteEnc
                                        BeAjusteMI3.IdAjusteDet = AjDet.IdAjusteDet
                                        BeAjusteMI3.Codigo_Bodega = vCodigoBodega
                                        BeAjusteMI3.Codigo_Bodega_ERP = vCodigoBodegaERP
                                        BeAjusteMI3.NoDocumento = vNoDocumento
                                        BeAjusteMI3.Codigo_Producto = AjDet.Codigo_Producto
                                        BeAjusteMI3.TipoAjusteERP = IIf(AjEnc.Ajuste_Por_Inventario > 0, AjDet.Codigo_Bodega, BeCliente.Codigo)
                                        BeAjusteMI3.TipoAjusteWMS = AjDet.Tipo_Ajuste
                                        BeAjusteMI3.UMBas = AjDet.UMBas
                                        BeAjusteMI3.Cantidad = vDif
                                        BeAjusteMI3.Lote = AjDet.Lote_Nuevo
                                        BeAjusteMI3.Motivo_Ajuste = AjDet.Motivo_Ajuste
                                        BeAjusteMI3.Observacion = AjDet.Observacion
                                        BeAjusteMI3.Seccion = AjDet.Seccion
                                        BeAjusteMI3.IdCentroCosto = AjEnc.IdCentroCosto
                                        BeAjusteMI3.Codigo_Centro_Costo = clsLnCentro_costo.Get_Codigo_By_IdCentroCosto(AjEnc.IdCentroCosto, lConnection, lTransaction)
                                        BeAjusteMI3.Talla = AjDet.Talla
                                        BeAjusteMI3.Color = AjDet.Color
                                        BeAjusteMI3.Centro_Costo_Erp = AjEnc.Centro_Costo_Erp
                                        BeAjusteMI3.Centro_Costo_Dep_Erp = AjEnc.Centro_Costo_Dep_Erp
                                        BeAjusteMI3.Centro_Costo_Dir_Erp = AjEnc.Centro_Costo_Dir_Erp

                                        lAjustesMI3.Add(BeAjusteMI3)

                                        clsPublic.Actualizar_Progreso(lblprg, "Procesando ajuste negativo para: " & AjDet.Codigo_Producto & " " & AjDet.Nombre_Producto)

                                        DetallesEnviados += 1

                                    Catch ex As Exception

                                        clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                                   AjEnc.Idajusteenc,
                                                                                   0,
                                                                                   300)

                                        clsPublic.Actualizar_Progreso(lblprg, "Error al procesar el ajuste #: " & AjEnc.Idajusteenc)

                                        vCorrelativoActual -= 1

                                    End Try

                                ElseIf AjDet.Cantidad_original < AjDet.Cantidad_nueva Then 'Es un ajuste positivo

                                    vDif = Math.Round(AjDet.Cantidad_nueva - AjDet.Cantidad_original, 6)

                                    Try

                                        BeAjusteMI3 = New clsBeAjustesMI3()
                                        BeAjusteMI3.IdAjusteEnc = AjDet.IdAjusteEnc
                                        BeAjusteMI3.IdAjusteDet = AjDet.IdAjusteDet
                                        BeAjusteMI3.Codigo_Bodega = vCodigoBodega
                                        BeAjusteMI3.Codigo_Bodega_ERP = vCodigoBodegaERP
                                        BeAjusteMI3.NoDocumento = vNoDocumento
                                        BeAjusteMI3.Codigo_Producto = AjDet.Codigo_Producto
                                        BeAjusteMI3.TipoAjusteERP = IIf(AjEnc.Ajuste_Por_Inventario > 0, AjDet.Codigo_Bodega, BeCliente.Codigo)
                                        BeAjusteMI3.TipoAjusteWMS = AjDet.Tipo_Ajuste
                                        BeAjusteMI3.UMBas = AjDet.UMBas
                                        BeAjusteMI3.Cantidad = vDif
                                        BeAjusteMI3.Lote = AjDet.Lote_Nuevo
                                        BeAjusteMI3.Motivo_Ajuste = AjDet.Motivo_Ajuste
                                        BeAjusteMI3.Observacion = AjDet.Observacion
                                        BeAjusteMI3.Seccion = AjDet.Seccion
                                        BeAjusteMI3.IdCentroCosto = AjEnc.IdCentroCosto
                                        BeAjusteMI3.Codigo_Centro_Costo = clsLnCentro_costo.Get_Codigo_By_IdCentroCosto(AjEnc.IdCentroCosto, lConnection, lTransaction)
                                        BeAjusteMI3.Talla = AjDet.Talla
                                        BeAjusteMI3.Color = AjDet.Color
                                        BeAjusteMI3.Centro_Costo_Erp = AjEnc.Centro_Costo_Erp
                                        BeAjusteMI3.Centro_Costo_Dep_Erp = AjEnc.Centro_Costo_Dep_Erp
                                        BeAjusteMI3.Centro_Costo_Dir_Erp = AjEnc.Centro_Costo_Dir_Erp
                                        lAjustesMI3.Add(BeAjusteMI3)

                                        clsPublic.Actualizar_Progreso(lblprg, "Procesando ajuste positivo para: " & AjDet.Codigo_Producto & " " & AjDet.Nombre_Producto)

                                        BeAjusteDet.Enviado = True

                                        If AjEnc.Referencia.Trim = "" Then
                                            AjEnc.Referencia = vNoDocumento
                                        Else
                                            AjEnc.Referencia = AjEnc.Referencia
                                        End If

                                        DetallesEnviados += 1

                                    Catch ex As Exception

                                        clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                                    AjEnc.Idajusteenc,
                                                                                    0,
                                                                                    300)

                                        clsPublic.Actualizar_Progreso(lblprg, "Error al procesar el ajuste #: " & AjEnc.Idajusteenc)

                                        vCorrelativoActual -= 1

                                    End Try

                                End If

                            Else

                                '#EJC20180615: Es un tipo de ajuste que no se puede enviar a ERP, pej. cambio de lote
                                BeAjusteDet.Enviado = True
                                clsLnTrans_ajuste_det.Actualizar(BeAjusteDet)
                                DetallesEnviados += 1

                            End If

                            MaxIdAjusteDoc += 1

                        Next

                        vCorrelativoActual = 0

                    Else

                        clsLnI_nav_ejecucion_det_error.Inserta_Log("No hay detalle de ajustes válidos para el Id de ajuste #: " & AjEnc.Idajusteenc,
                                                                   AjEnc.Idajusteenc,
                                                                   0,
                                                                   300)

                        clsPublic.Actualizar_Progreso(lblprg, "No hay detalle de ajustes válidos para el Id de ajuste #: " & AjEnc.Idajusteenc)

                    End If

                    pResult = lblprg.Text
                Next

                Return lAjustesMI3

            Else
                clsPublic.Actualizar_Progreso(lblprg, "No hay ajustes pendientes de envío.")
            End If

            clsPublic.Actualizar_Progreso(lblprg, "Fin de sincronización de ajustes.")

        Catch ex As Exception

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "Sync_Ajustes",
                                                       0,
                                                       300,)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al enviar ajustes a NAV: {0}{1}-{2}", vbNewLine, ex.Message, vNDoc))

            Throw ex

        Finally
            pResult = lblprg.Text
        End Try

    End Function

    Public Shared Function Get_All_By_Fecha_Propietario(ByVal pFechaDel As Date,
                                                        ByVal pFechaAl As Date,
                                                        ByVal pIdPropietarioBodega As Integer,
                                                        ByVal pCodigo_producto As Integer) As List(Of clsBeI_nav_transacciones_out)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lReturnList As New List(Of clsBeI_nav_transacciones_out)

            Dim vSQL As String = "SELECT * FROM I_nav_transacciones_out "

            vSQL += String.Format(" Where cast(fec_agr AS DATE) BETWEEN {0} AND {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            vSQL += " AND IdPropietarioBodega=@pIdPropietarioBodega and codigo_producto=@pCodigo_producto"

            vSQL += " ORDER BY fec_agr "

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.AddWithValue("@pIdPropietarioBodega", pIdPropietarioBodega)
            dad.SelectCommand.Parameters.AddWithValue("@pCodigo_producto", pCodigo_producto)

            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_transacciones_out As New clsBeI_nav_transacciones_out

            For Each dr As DataRow In dt.Rows
                vBeI_nav_transacciones_out = New clsBeI_nav_transacciones_out
                Cargar(vBeI_nav_transacciones_out, dr)
                lReturnList.Add(vBeI_nav_transacciones_out)
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

    Public Shared Function Get_Devolucion_De_Ruta_By_Filtros(ByVal pIdTipoDocumento As clsDataContractDI.tTipoDocumentoIngreso,
                                                             ByVal pCodigoBodega As String,
                                                             ByVal pNoDocumentoDevolucionRef As String) As List(Of clsBeI_nav_transacciones_out)

        Get_Devolucion_De_Ruta_By_Filtros = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lReturnList As New List(Of clsBeI_nav_transacciones_out)

            Dim vSQL As String = "SELECT * FROM I_nav_transacciones_out 
                                  WHERE IdTipoDocumento =@IdTipoDocumento 
                                  AND IdBodega = @IdBodega
                                  AND no_documento_salida_ref_devol = @no_documento_salida_ref_devol"

            vSQL += String.Format(" AND tipo_transaccion = 'INGRESO' AND Enviado =0 ")

            vSQL += " ORDER BY fec_agr "

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            Dim vIdBodega As Integer = clsLnBodega.Get_IdBodegaWMS_By_Codigo(pCodigoBodega, lConnection, lTransaction)

            dad.SelectCommand.Parameters.AddWithValue("@IdTipoDocumento", pIdTipoDocumento)
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", vIdBodega)
            dad.SelectCommand.Parameters.AddWithValue("@no_documento_salida_ref_devol", pNoDocumentoDevolucionRef)

            dad.Fill(dt)

            Dim vBeI_nav_transacciones_out As New clsBeI_nav_transacciones_out

            For Each dr As DataRow In dt.Rows
                vBeI_nav_transacciones_out = New clsBeI_nav_transacciones_out
                Cargar(vBeI_nav_transacciones_out, dr)
                lReturnList.Add(vBeI_nav_transacciones_out)
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

    ''' <summary>
    ''' #EJC20211004: Funcionalidad para actualizar la bandera no_documento_procesado_erp en el registro de stock_jornada 
    ''' CEALSA
    ''' #CKFK20220517 Corregido porque los parámetros estaban incorrectos
    ''' </summary>
    ''' <param name="No_Documento_Procesado_ERP"></param>
    ''' <param name="pEnviado"></param>
    ''' <param name="pConection"></param>
    ''' <param name="pTransaction"></param>
    ''' <returns></returns>
    Public Shared Function Actualizar_Registro_Procesado_By_No_Referencia_ERP_Almacenaje(ByVal No_Documento_Procesado_ERP As String,
                                                                                         ByVal pEnviado As Boolean,
                                                                                         Optional ByVal pConection As SqlConnection = Nothing,
                                                                                         Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("stock_jornada")
            Upd.Add("enviado", "@procesado_erp", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("no_documento_procesado_erp = @no_documento_procesado_erp")

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

            cmd.Parameters.Add(New SqlParameter("@procesado_erp", pEnviado))
            cmd.Parameters.Add(New SqlParameter("@fec_mod", Now))
            cmd.Parameters.Add(New SqlParameter("@no_documento_procesado_erp", No_Documento_Procesado_ERP))

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

    ''' <summary>
    ''' #CKFK20220517 Creada nuevamente por que se habia perdido
    ''' CEALSA 
    ''' </summary>
    ''' <param name="No_Documento_Procesado_ERP"></param>
    ''' <param name="pEnviado"></param>
    ''' <param name="pConection"></param>
    ''' <param name="pTransaction"></param>
    ''' <returns></returns>
    Public Shared Function Actualizar_Registro_Procesado_By_No_Referencia_ERP_Servicios(ByVal No_Documento_Procesado_ERP As String,
                                                                                         ByVal pEnviado As Boolean,
                                                                                         Optional ByVal pConection As SqlConnection = Nothing,
                                                                                         Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_servicio_enc")
            Upd.Add("enviado", "@enviado_a_erp", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("no_orden = @no_documento_procesado_erp")

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

            cmd.Parameters.Add(New SqlParameter("@enviado_a_erp", pEnviado))
            cmd.Parameters.Add(New SqlParameter("@fec_mod", Now))
            cmd.Parameters.Add(New SqlParameter("@no_orden", No_Documento_Procesado_ERP))

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

    ''' <summary>
    ''' Requerida por Tzirin en IDEALSA
    ''' #EJC20211105
    ''' </summary>
    ''' <param name="pIdTipoDocumento"></param>
    ''' <param name="pNoPedido"></param>
    ''' <returns></returns>
    Public Shared Function Get_All_Despachos_Pendientes_De_Envio_By_No_Pedido_And_IdTipoDocumento(ByVal pIdTipoDocumento As clsDataContractDI.tTipoDocumentoSalida,
                                                                                                  ByVal pNoPedido As String) As List(Of clsBeI_nav_transacciones_out)

        Get_All_Despachos_Pendientes_De_Envio_By_No_Pedido_And_IdTipoDocumento = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lReturnList As New List(Of clsBeI_nav_transacciones_out)

            Dim vSQL As String = "SELECT * FROM I_nav_transacciones_out "

            vSQL += String.Format(" WHERE (tipo_transaccion = 'SALIDA' AND Enviado =0)")

            If pNoPedido <> "" Then
                vSQL += " AND (no_pedido = @NoPedido)"
            End If

            If pIdTipoDocumento <> "" Then
                vSQL += " AND (IdTipoDocumento = @IdTipoDocumento)"
            End If

            vSQL += " ORDER BY fec_agr "

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            If pNoPedido <> "" Then
                dad.SelectCommand.Parameters.AddWithValue("@NoPedido", pNoPedido)
            End If

            If pIdTipoDocumento <> 0 Then
                dad.SelectCommand.Parameters.AddWithValue("@IdTipoDocumento", pIdTipoDocumento)
            End If

            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_transacciones_out As New clsBeI_nav_transacciones_out
            Dim BeDespachoEnc As New clsBeTrans_despacho_enc
            Dim BePedidoEnc As New clsBeTrans_pe_enc
            Dim IdBodegaWMS As Integer = 0
            Dim lBePedidoEnc As New List(Of clsBeTrans_pe_enc)
            Dim lBeDespachoEnc As New List(Of clsBeTrans_despacho_enc)
            Dim dictionaryOfBodegas As New Dictionary(Of Integer, String)
            Dim dictionaryOfBodegasCliente As New Dictionary(Of String, Integer)
            Dim idxDespacho As Integer = -1
            Dim idxPedido As Integer = -1

            For Each dr As DataRow In dt.Rows

                vBeI_nav_transacciones_out = New clsBeI_nav_transacciones_out

                Cargar(vBeI_nav_transacciones_out, dr)

                If Not dictionaryOfBodegas.TryGetValue(vBeI_nav_transacciones_out.Idbodega, vBeI_nav_transacciones_out.Codigo_Bodega_Origen) Then
                    vBeI_nav_transacciones_out.Codigo_Bodega_Origen = clsLnBodega.Get_Codigo_By_IdBodega(vBeI_nav_transacciones_out.Idbodega,
                                                                                                         lConnection,
                                                                                                         lTransaction)
                    dictionaryOfBodegas.Add(vBeI_nav_transacciones_out.Idbodega, vBeI_nav_transacciones_out.Codigo_Bodega_Origen)
                End If

                idxDespacho = lBeDespachoEnc.FindIndex(Function(x) x.IdDespachoEnc = vBeI_nav_transacciones_out.Iddespachoenc)

                If idxDespacho <> -1 Then
                    BeDespachoEnc = New clsBeTrans_despacho_enc
                    BeDespachoEnc = lBeDespachoEnc(idxDespacho).Clone()
                Else
                    BeDespachoEnc = New clsBeTrans_despacho_enc
                    BeDespachoEnc = clsLnTrans_despacho_enc.GetSingle(vBeI_nav_transacciones_out.Iddespachoenc,
                                                                      lConnection,
                                                                      lTransaction)
                    lBeDespachoEnc.Add(BeDespachoEnc.Clone())
                End If

                '#EJC20190902: Datos de transporte.
                vBeI_nav_transacciones_out.Observacion = BeDespachoEnc.Observacion
                vBeI_nav_transacciones_out.Empresa_Transporte = BeDespachoEnc.NombreVehiculo
                vBeI_nav_transacciones_out.Piloto_Transporte = BeDespachoEnc.NombrePiloto
                vBeI_nav_transacciones_out.Contacto_Recibe = ""
                vBeI_nav_transacciones_out.Contacto_Entrega = ""
                vBeI_nav_transacciones_out.Placa_Transporte = BeDespachoEnc.Placa
                vBeI_nav_transacciones_out.TCN_Transporte = BeDespachoEnc.Placa_Comercial
                vBeI_nav_transacciones_out.Marchamo_No = BeDespachoEnc.Marchamo

                If vBeI_nav_transacciones_out.Idpedidoenc <> 0 Then

                    idxPedido = lBePedidoEnc.FindIndex(Function(x) x.IdPedidoEnc = vBeI_nav_transacciones_out.Idpedidoenc)

                    If idxPedido <> -1 Then
                        BePedidoEnc = New clsBeTrans_pe_enc()
                        BePedidoEnc = lBePedidoEnc(idxPedido)
                    Else
                        BePedidoEnc = New clsBeTrans_pe_enc
                        BePedidoEnc = clsLnTrans_pe_enc.GetSingle(vBeI_nav_transacciones_out.Idpedidoenc, lConnection, lTransaction)
                        lBePedidoEnc.Add(BePedidoEnc.Clone())
                    End If

                    If Not BePedidoEnc Is Nothing Then

                        If Not dictionaryOfBodegasCliente.TryGetValue(BePedidoEnc.Cliente.Codigo,
                                                                      IdBodegaWMS) Then

                            IdBodegaWMS = clsLnBodega.Get_IdBodega_By_Codigo(BePedidoEnc.Cliente.Codigo,
                                                                             lConnection,
                                                                             lTransaction)

                            dictionaryOfBodegasCliente.Add(BePedidoEnc.Cliente.Codigo, IdBodegaWMS)

                        End If

                        If Not dictionaryOfBodegas.TryGetValue(IdBodegaWMS, vBeI_nav_transacciones_out.Codigo_Bodega_Destino) Then
                            vBeI_nav_transacciones_out.Codigo_Bodega_Destino = clsLnBodega.Get_Codigo_By_IdBodega(IdBodegaWMS,
                                                                                                                  lConnection,
                                                                                                                  lTransaction)
                            dictionaryOfBodegas.Add(IdBodegaWMS, vBeI_nav_transacciones_out.Codigo_Bodega_Destino)
                        End If

                        If IdBodegaWMS <> 0 Then 'Es una transferencia hacia una bodega de WMS.
                            vBeI_nav_transacciones_out.Codigo_Cliente = ""
                            vBeI_nav_transacciones_out.IdTipoDocumento = "Transferencia"
                        Else
                            vBeI_nav_transacciones_out.Codigo_Bodega_Destino = ""
                            vBeI_nav_transacciones_out.Codigo_Cliente = BePedidoEnc.Cliente.Codigo
                            vBeI_nav_transacciones_out.IdTipoDocumento = "Despacho"
                        End If

                        lReturnList.Add(vBeI_nav_transacciones_out)

                    Else
                        Throw New Exception("El pedido asociado con la transacción de despacho no existe (se anuló y eliminó)")
                    End If

                End If

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

    Public Shared Function Get_Lotes_Salida_Pendientes_Envio_By_IdTipoDocumento(ByVal pTipoDocumento As clsDataContractDI.tTipoDocumentoSalida) As List(Of clsBeI_nav_transacciones_out)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lReturnList As New List(Of clsBeI_nav_transacciones_out)

            Dim sp As String = "SELECT * FROM I_nav_transacciones_out 
                                WHERE tipo_transaccion = 'SALIDA'  
                                AND enviado = 0 AND IdTipoDocumento = @IdTipoDocumento "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdTipoDocumento", pTipoDocumento)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_transacciones_out As New clsBeI_nav_transacciones_out

            For Each dr As DataRow In dt.Rows

                vBeI_nav_transacciones_out = New clsBeI_nav_transacciones_out()

                Cargar(vBeI_nav_transacciones_out, dr)

                vBeI_nav_transacciones_out.Codigo_Bodega_Destino = clsLnCliente.Get_Codigo_By_IdCliente(clsLnTrans_pe_enc.GetIdCliente(vBeI_nav_transacciones_out.Idpedidoenc,
                                                                                                                                       lConnection,
                                                                                                                                       lTransaction))

                vBeI_nav_transacciones_out.Codigo_Bodega_Origen = clsLnBodega.Get_Codigo_By_IdBodega(vBeI_nav_transacciones_out.Idbodega,
                                                                                                     lConnection,
                                                                                                     lTransaction)

                lReturnList.Add(vBeI_nav_transacciones_out)

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

    Public Shared Function Eliminar_By_IdRecepcionEnc_And_IdRecepcionDet(ByVal pIdRecepcionEnc As Integer,
                                                                         ByVal pIdRecepcionDet As Integer,
                                                                         ByVal pIdBodega As Integer,
                                                                         ByVal pIdProductoBodega As Integer,
                                                                         ByVal pLic_Plate As String,
                                                                         ByVal pNoLinea As String,
                                                                         ByVal pLote As String,
                                                                         ByVal pFechaVence As Date,
                                                                         Optional ByVal pConection As SqlConnection = Nothing,
                                                                         Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from I_nav_transacciones_out
                                   Where(IdRecepcionEnc = @IdRecepcionEnc AND IdRecepcionDet = @IdRecepcionDet ) "

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", pIdRecepcionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONDET", pIdRecepcionDet))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", pIdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", pIdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", pLic_Plate))
            cmd.Parameters.Add(New SqlParameter("@NO_LINEA", pNoLinea))
            cmd.Parameters.Add(New SqlParameter("@LOTE", pLote))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", pFechaVence))

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

    ''' <summary>
    ''' #EJC202309261339: Se utiliza en la interface SAP La Cumbre, para enviar entrega de mercancía a orden de compra.
    ''' </summary>
    ''' <param name="lConnection"></param>
    ''' <param name="lTransaction"></param>
    ''' <returns></returns>
    Public Shared Function Get_Lotes_Ingreso_Pendientes_Envio(ByVal lConnection As SqlConnection,
                                                              ByVal lTransaction As SqlTransaction,
                                                              ByVal pIdBodega As Integer) As List(Of clsBeI_nav_transacciones_out)

        Dim lReturnList As New List(Of clsBeI_nav_transacciones_out)

        Try

            Dim sp As String = "SELECT * FROM I_nav_transacciones_out 
                                WHERE tipo_transaccion = 'INGRESO' 
                                AND enviado = 0 AND idrecepcionenc in (SELECT IdRecepcionEnc 
                                                  FROM trans_re_enc  
                                                  WHERE estado = 'Cerrado') AND IdBodega = @IdBodega "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            cmd.Parameters.AddWithValue("@idBodega", pIdBodega)
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_transacciones_out As New clsBeI_nav_transacciones_out

            For Each dr As DataRow In dt.Rows
                vBeI_nav_transacciones_out = New clsBeI_nav_transacciones_out
                Cargar(vBeI_nav_transacciones_out, dr)
                lReturnList.Add(vBeI_nav_transacciones_out)
            Next

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Lotes_Ingreso_Pendientes_Envio_By_Tipo(ByVal pTipo As tTipoDocumentoIngreso,
                                                                      ByVal lConnection As SqlConnection,
                                                                      ByVal lTransaction As SqlTransaction,
                                                                      ByVal IdBodegaOrigen As Integer) As List(Of clsBeI_nav_transacciones_out)

        Dim lReturnList As New List(Of clsBeI_nav_transacciones_out)

        Try

            '#CKFK20251011 Modifiqué el query para que tome la talla y el color de las tablas maestras
            Dim sp As String = "SELECT idtransaccion, idempresa, idbodega, o.idpropietario, idpropietariobodega, idordencompra, idrecepcionenc, idpedidoenc, iddespachoenc, 
                                       idproductobodega, idproducto, idunidadmedida, idpresentacion, idproductoestado, cantidad, peso, lote, fecha_vence, fecha_recepcion, 
	                                   no_pedido, no_linea, codigo_producto, nombre_producto, codigo_variante, unidad_medida, tipo_transaccion, enviado, 
	                                   o.fec_agr, o.user_agr, o.fec_mod, o.user_mod, Cantidad_Esperada, lic_plate, uds_lic_plate, cantidad_presentacion, IdTipoDocumento, 
	                                   es_servicio, codigo_barra, valor_aduana, valor_fob, valor_iva, valor_dai, valor_seguro, valor_flete, peso_neto, peso_bruto, 
	                                   fecha_despacho, no_documento_salida_ref_devol, IdPedidoEncDevol, IdDespachoDet, IdRecepcionDet, cantidad_enviada, 
	                                   cantidad_pendiente, auditar, IdProductoTallaColor, t.Codigo Talla, c.Codigo Color
                                 FROM I_nav_transacciones_out o INNER JOIN
                                      talla t ON o.Talla = t.Nombre INNER JOIN
	                                  color c ON o.Color = c.Nombre 
                                 WHERE tipo_transaccion = 'INGRESO' 
                                       AND enviado = 0 
                                       AND IdTipoDocumento = @IdTipoDocumento 
                                       AND IdBodega = @IdBodega 
                                       AND idrecepcionenc in (SELECT IdRecepcionEnc 
                                                              FROM trans_re_enc  
                                                              WHERE estado = 'Cerrado')"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            cmd.Parameters.AddWithValue("@idBodega", IdBodegaOrigen)
            cmd.Parameters.AddWithValue("@IdTipoDocumento", pTipo)
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_transacciones_out As New clsBeI_nav_transacciones_out

            For Each dr As DataRow In dt.Rows
                vBeI_nav_transacciones_out = New clsBeI_nav_transacciones_out
                Cargar(vBeI_nav_transacciones_out, dr)
                lReturnList.Add(vBeI_nav_transacciones_out)
            Next

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar_Bandera_Enviado_By_IdOrdenCompraEnc(ByVal pIdOrdenCompraEnc As Integer,
                                                                          ByVal pEnviado As Boolean,
                                                                          Optional ByVal pConection As SqlConnection = Nothing,
                                                                          Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("i_nav_transacciones_out")
            Upd.Add("enviado", "@enviado", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("IdOrdenCompra = @IdOrdenCompraEnc")

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

            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", pIdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@ENVIADO", pEnviado))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", Now))


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

    Public Shared Sub Actualizar_Progreso(ByRef lblPrg As RichTextBox, mensaje As String)
        lblPrg.AppendText(mensaje & vbNewLine)
        lblPrg.Refresh()
        lblPrg.SelectionStart = lblPrg.TextLength
        lblPrg.ScrollToCaret()
    End Sub

    Public Shared Function Get_Ajustes_Auditados_Pendientes_Envio_MI3(ByRef pResult As String,
                                                                      ByRef lConnection As SqlConnection,
                                                                      ByRef lTransaction As SqlTransaction) As List(Of clsBeAjustesMI3)

        Get_Ajustes_Auditados_Pendientes_Envio_MI3 = Nothing

        Dim lAjustesMI3 As New List(Of clsBeAjustesMI3)
        Dim lblprg As New RichTextBox
        Dim vNDoc As String = ""
        Dim lVistaAjustesPendientesEnvio As New List(Of clsBe_vw_ajustes)
        Dim vDif As Double = 0
        Dim vNoDocumento As String = ""
        Dim BeAjusteDet As New clsBeTrans_ajuste_det
        Dim DetallesEnviados As Integer = 0
        Dim BeFamilia As New clsBeProducto_familia
        Dim vSerieBodega As String = ""
        Dim BeCliente As New clsBeCliente
        Dim AjusteDoc As New clsBeTrans_ajuste_det_doc
        Dim MaxIdAjusteDoc As Integer = 0
        Dim BeAjusteMI3 As New clsBeAjustesMI3
        Dim vNomenclaturaBase As String = ""
        Dim vCorrelativoActual As Integer = 0
        Dim lAjustesPendEnvio As New List(Of clsBeTrans_ajuste_enc)

        Try

            lAjustesPendEnvio = clsLnTrans_ajuste_enc.Get_All_Auditados_Pendientes_Envio(lConnection, lTransaction)

            If Not lAjustesPendEnvio Is Nothing Then

                MaxIdAjusteDoc = clsLnTrans_ajuste_det_doc.MaxID(lConnection, lTransaction) + 1

                For Each AjEnc In lAjustesPendEnvio

                    vNoDocumento = Right("000000" & AjEnc.Idajusteenc, 6)

                    vNoDocumento = "WMS" + vNoDocumento
                    vNDoc = vNoDocumento

                    lVistaAjustesPendientesEnvio = clsLn_vw_ajustes.Get_All_Pendientes_Envio(AjEnc.Idajusteenc,
                                                                                             lConnection,
                                                                                             lTransaction)

                    DetallesEnviados = 0

                    '#EJC20180711: Obtener la sección para NAV (Debe llenarse a traves del mantenimiento de familias y se selecciona en el encabezado del ajuste)
                    If AjEnc.IdProductoFamilia > 0 Then
                        BeFamilia = clsLnProducto_familia.GetSingle(AjEnc.IdProductoFamilia,
                                                                    lConnection,
                                                                    lTransaction)
                    End If

                    If lVistaAjustesPendientesEnvio.Count > 0 Then

                        clsPublic.Actualizar_Progreso(lblprg, "Detalle de ajustes para transacción: " & AjEnc.Idajusteenc)

                        Dim vCodigoBodega As String = clsLnBodega.Get_Codigo_By_IdBodega(AjEnc.IdBodega,
                                                                                         lConnection,
                                                                                         lTransaction)
                        Dim vCodigoBodegaERP As String = ""

                        For Each AjDet In lVistaAjustesPendientesEnvio

                            BeCliente = clsLnCliente.GetSingle(AjDet.IdBodegaERP,
                                                               lConnection,
                                                               lTransaction)

                            If Not BeCliente Is Nothing Then
                                vSerieBodega = BeCliente.Referencia
                                vCodigoBodegaERP = BeCliente.Codigo
                            Else
                                vSerieBodega = AjDet.Seccion
                                vCodigoBodegaERP = AjDet.IdBodegaERP
                                BeCliente = New clsBeCliente
                                BeCliente.Codigo = AjDet.IdBodegaERP
                            End If

                            AjusteDoc = New clsBeTrans_ajuste_det_doc()

                            '#CKFK 20180927 0953PM Agregué esta condición porque cuando la serie es nueva el correlativo actual va a ser igual a 999999
                            If vCorrelativoActual = 0 Then
                                vCorrelativoActual = Val(vNoDocumento.Substring(4, vNoDocumento.Length - 4))
                                vCorrelativoActual += 1
                            ElseIf vCorrelativoActual = 999999 Then
                                vCorrelativoActual = 1
                            Else
                                vCorrelativoActual += 1
                            End If

                            vNoDocumento = vNomenclaturaBase + Right("000000" & vCorrelativoActual, 6)

                            clsPublic.Actualizar_Progreso(lblprg, "Procesando ajuste número de documento: " & vNoDocumento)

                            BeAjusteDet.IdAjusteDet = AjDet.IdAjusteDet
                            BeAjusteDet.IdAjusteEnc = AjEnc.Idajusteenc
                            clsLnTrans_ajuste_det.GetSingle(BeAjusteDet, lConnection, lTransaction)

                            If Not BeFamilia Is Nothing Then
                                AjDet.Seccion = BeFamilia.Nombre
                            End If

                            '#CM_20180828_2:54PM: Llena datos para el documento de ajustes. 
                            AjusteDoc.Idajustedoc = MaxIdAjusteDoc
                            AjusteDoc.Idajusteenc = AjEnc.Idajusteenc

                            If AjDet.Modifica_Cantidad Then

                                If AjDet.Cantidad_original > AjDet.Cantidad_nueva Then 'Es un ajuste negativo

                                    vDif = Math.Round(AjDet.Cantidad_original - AjDet.Cantidad_nueva, 6)

                                    Try

                                        BeAjusteMI3 = New clsBeAjustesMI3()
                                        BeAjusteMI3.IdAjusteEnc = AjDet.IdAjusteEnc
                                        BeAjusteMI3.IdAjusteDet = AjDet.IdAjusteDet
                                        BeAjusteMI3.Codigo_Bodega = vCodigoBodega
                                        BeAjusteMI3.Codigo_Bodega_ERP = vCodigoBodegaERP
                                        BeAjusteMI3.NoDocumento = vNoDocumento
                                        BeAjusteMI3.Codigo_Producto = AjDet.Codigo_Producto
                                        BeAjusteMI3.TipoAjusteERP = IIf(AjEnc.Ajuste_Por_Inventario > 0, AjDet.Codigo_Bodega, BeCliente.Codigo)
                                        BeAjusteMI3.TipoAjusteWMS = AjDet.Tipo_Ajuste
                                        BeAjusteMI3.UMBas = AjDet.UMBas
                                        BeAjusteMI3.Cantidad = vDif
                                        BeAjusteMI3.Lote = AjDet.Lote_Nuevo
                                        BeAjusteMI3.Motivo_Ajuste = AjDet.Motivo_Ajuste
                                        BeAjusteMI3.Observacion = AjDet.Observacion
                                        BeAjusteMI3.Seccion = AjDet.Seccion
                                        BeAjusteMI3.IdCentroCosto = AjEnc.IdCentroCosto
                                        BeAjusteMI3.Codigo_Centro_Costo = clsLnCentro_costo.Get_Codigo_By_IdCentroCosto(AjEnc.IdCentroCosto)
                                        lAjustesMI3.Add(BeAjusteMI3)

                                        clsPublic.Actualizar_Progreso(lblprg, "Procesando ajuste negativo para: " & AjDet.Codigo_Producto & " " & AjDet.Nombre_Producto)

                                        DetallesEnviados += 1

                                    Catch ex As Exception

                                        clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                                   AjEnc.Idajusteenc,
                                                                                   0,
                                                                                   300)

                                        clsPublic.Actualizar_Progreso(lblprg, "Error al procesar el ajuste #: " & AjEnc.Idajusteenc)

                                        vCorrelativoActual -= 1

                                    End Try

                                ElseIf AjDet.Cantidad_original < AjDet.Cantidad_nueva Then 'Es un ajuste positivo

                                    vDif = Math.Round(AjDet.Cantidad_nueva - AjDet.Cantidad_original, 6)

                                    Try

                                        BeAjusteMI3 = New clsBeAjustesMI3()
                                        BeAjusteMI3.IdAjusteEnc = AjDet.IdAjusteEnc
                                        BeAjusteMI3.IdAjusteDet = AjDet.IdAjusteDet
                                        BeAjusteMI3.Codigo_Bodega = vCodigoBodega
                                        BeAjusteMI3.Codigo_Bodega_ERP = vCodigoBodegaERP
                                        BeAjusteMI3.NoDocumento = vNoDocumento
                                        BeAjusteMI3.Codigo_Producto = AjDet.Codigo_Producto
                                        BeAjusteMI3.TipoAjusteERP = IIf(AjEnc.Ajuste_Por_Inventario > 0, AjDet.Codigo_Bodega, BeCliente.Codigo)
                                        BeAjusteMI3.TipoAjusteWMS = AjDet.Tipo_Ajuste
                                        BeAjusteMI3.UMBas = AjDet.UMBas
                                        BeAjusteMI3.Cantidad = vDif
                                        BeAjusteMI3.Lote = AjDet.Lote_Nuevo
                                        BeAjusteMI3.Motivo_Ajuste = AjDet.Motivo_Ajuste
                                        BeAjusteMI3.Observacion = AjDet.Observacion
                                        BeAjusteMI3.Seccion = AjDet.Seccion
                                        BeAjusteMI3.IdCentroCosto = AjEnc.IdCentroCosto
                                        BeAjusteMI3.Codigo_Centro_Costo = clsLnCentro_costo.Get_Codigo_By_IdCentroCosto(AjEnc.IdCentroCosto)
                                        lAjustesMI3.Add(BeAjusteMI3)

                                        clsPublic.Actualizar_Progreso(lblprg, "Procesando ajuste positivo para: " & AjDet.Codigo_Producto & " " & AjDet.Nombre_Producto)

                                        BeAjusteDet.Enviado = True

                                        If AjEnc.Referencia.Trim = "" Then
                                            AjEnc.Referencia = vNoDocumento
                                        Else
                                            AjEnc.Referencia = AjEnc.Referencia
                                        End If

                                        DetallesEnviados += 1

                                    Catch ex As Exception

                                        clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                                    AjEnc.Idajusteenc,
                                                                                    0,
                                                                                    300)

                                        clsPublic.Actualizar_Progreso(lblprg, "Error al procesar el ajuste #: " & AjEnc.Idajusteenc)

                                        vCorrelativoActual -= 1

                                    End Try

                                End If

                            Else

                                '#EJC20180615: Es un tipo de ajuste que no se puede enviar a ERP, pej. cambio de lote
                                BeAjusteDet.Enviado = True
                                clsLnTrans_ajuste_det.Actualizar(BeAjusteDet)
                                DetallesEnviados += 1

                            End If

                            MaxIdAjusteDoc += 1

                        Next

                        vCorrelativoActual = 0

                    Else

                        clsLnI_nav_ejecucion_det_error.Inserta_Log("No hay detalle de ajustes válidos para el Id de ajuste #: " & AjEnc.Idajusteenc,
                                                                   AjEnc.Idajusteenc,
                                                                   0,
                                                                   300)

                        clsPublic.Actualizar_Progreso(lblprg, "No hay detalle de ajustes válidos para el Id de ajuste #: " & AjEnc.Idajusteenc)

                    End If

                    pResult = lblprg.Text
                Next

                Return lAjustesMI3

            Else
                clsPublic.Actualizar_Progreso(lblprg, "No hay ajustes auditados pendientes de envío.")
            End If

            clsPublic.Actualizar_Progreso(lblprg, "Fin de sincronización de ajustes.")

        Catch ex As Exception

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "Sync_Ajustes",
                                                       0,
                                                       300,)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al enviar ajustes a NAV: {0}{1}-{2}", vbNewLine, ex.Message, vNDoc))

            Throw ex

        Finally
            pResult = lblprg.Text
        End Try

    End Function

    Public Shared Function Get_All_Lotes_Pendientes_De_Envio(ByVal pIdBodega As Integer) As List(Of clsBeI_nav_transacciones_out)

        Get_All_Lotes_Pendientes_De_Envio = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lReturnList As New List(Of clsBeI_nav_transacciones_out)

            Dim vSQL As String = "SELECT * FROM I_nav_transacciones_out WHERE IdBodega =@IdBodega"

            vSQL += String.Format(" AND tipo_transaccion = 'SALIDA' AND Enviado =0 ")

            vSQL += " ORDER BY fec_agr "

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
            dad.Fill(dt)

            Dim vBeI_nav_transacciones_out As New clsBeI_nav_transacciones_out

            For Each dr As DataRow In dt.Rows
                vBeI_nav_transacciones_out = New clsBeI_nav_transacciones_out
                Cargar(vBeI_nav_transacciones_out, dr)
                lReturnList.Add(vBeI_nav_transacciones_out)
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

    Public Shared Function Get_All_Lotes_Pendientes_De_Envio(ByVal pFechaDel As Date,
                                                             ByVal pFechaAl As Date) As List(Of clsBeI_nav_transacciones_out)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lReturnList As New List(Of clsBeI_nav_transacciones_out)

            Dim vSQL As String = "SELECT * FROM I_nav_transacciones_out "

            vSQL += String.Format(" Where cast(fec_agr AS DATE) BETWEEN {0} AND {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))
            vSQL += " AND auditar =1 AND cantidad_pendiente > 0 "

            vSQL += " ORDER BY fec_agr "

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_transacciones_out As New clsBeI_nav_transacciones_out

            For Each dr As DataRow In dt.Rows
                vBeI_nav_transacciones_out = New clsBeI_nav_transacciones_out
                Cargar(vBeI_nav_transacciones_out, dr)
                lReturnList.Add(vBeI_nav_transacciones_out)
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

    ''' <summary>
    ''' #CKFK20241021: Se utiliza en la interface SAP Becofarma, para enviar entrega de mercancía a orden de compra.
    ''' </summary>
    ''' <param name="lConnection"></param>
    ''' <param name="lTransaction"></param>
    ''' <returns></returns>
    Public Shared Function Get_Lotes_Ingreso_Pendientes_Envio(ByVal lConnection As SqlConnection,
                                                              ByVal lTransaction As SqlTransaction) As List(Of clsBeI_nav_transacciones_out)

        Try

            Dim lReturnList As New List(Of clsBeI_nav_transacciones_out)

            '#CKFK20241017 Por favor no quitar lo que está en el Where, solo se deben de enviar las recepciones cerradas
            Dim sp As String = "SELECT * FROM I_nav_transacciones_out 
                                  WHERE tipo_transaccion = 'INGRESO'
                                  AND enviado = 0 AND idrecepcionenc in (SELECT IdRecepcionEnc 
                                                  FROM trans_re_enc  
                                                  WHERE estado = 'Cerrado') "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_transacciones_out As New clsBeI_nav_transacciones_out

            For Each dr As DataRow In dt.Rows
                vBeI_nav_transacciones_out = New clsBeI_nav_transacciones_out
                Cargar(vBeI_nav_transacciones_out, dr)
                lReturnList.Add(vBeI_nav_transacciones_out)
            Next

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' #CKFK20241021: Se utiliza en la interface SAP KILLIOS - GARESA, para enviar entrega de mercancía a orden de compra.
    ''' </summary>
    ''' <param name="lConnection"></param>
    ''' <param name="lTransaction"></param>
    ''' <returns></returns>
    Public Shared Function Get_Lotes_Ingreso_Pendientes_Envio_K(ByVal lConnection As SqlConnection,
                                                                ByVal lTransaction As SqlTransaction) As List(Of clsBeI_nav_transacciones_out)

        Try

            Dim lReturnList As New List(Of clsBeI_nav_transacciones_out)

            Dim sp As String = "SELECT o.idtransaccion, o.idempresa, o.idbodega, o.idpropietario, o.idpropietariobodega, o.idordencompra, 
                                       o.idrecepcionenc, o.idpedidoenc, o.iddespachoenc, o.idproductobodega, o.idproducto, o.idunidadmedida, 
	                                   o.idpresentacion, o.idproductoestado, o.cantidad, o.peso, o.lote, o.fecha_vence, o.fecha_recepcion, 
	                                   o.no_pedido, o.no_linea, 
	                                   CASE WHEN e.Codigo_Empresa_ERP = 'Killios' THEN
                                                 p.noparte ELSE p.noserie END codigo_producto, 
                                       o.nombre_producto, o.codigo_variante, o.unidad_medida, 
	                                   o.tipo_transaccion, o.enviado, o.fec_agr, o.user_agr, o.fec_mod, o.user_mod, o.Cantidad_Esperada, 
	                                   o.lic_plate, o.uds_lic_plate, o.cantidad_presentacion, o.tipo_documento, o.observacion, o.empresa_transporte, 
	                                   o.piloto_transporte, o.contacto_recibe, o.contacto_entrega, o.marchamo_no, o.IdTipoDocumento, 
	                                   o.es_servicio, o.codigo_barra, o.valor_aduana, o.valor_fob, o.valor_iva, o.valor_dai, o.valor_seguro,
	                                   o.valor_flete, o.peso_neto, o.peso_bruto, o.fecha_despacho, o.no_documento_salida_ref_devol, 
	                                   o.IdPedidoEncDevol, o.IdDespachoDet, o.IdRecepcionDet, o.cantidad_enviada, o.cantidad_pendiente, o.auditar
                                FROM   i_nav_transacciones_out o inner join
                                       trans_oc_enc e ON e.IdOrdenCompraEnc = o.idordencompra inner join 
                                       producto p ON o.idproducto = p.IdProducto
                                WHERE  tipo_transaccion = 'INGRESO' AND
                                       enviado = 0 AND idrecepcionenc in (SELECT IdRecepcionEnc 
                                                                         FROM trans_re_enc  
                                                                         WHERE estado = 'Cerrado') "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)
            '#EJC20241024a
            Dim vBeI_nav_transacciones_out As New clsBeI_nav_transacciones_out

            For Each dr As DataRow In dt.Rows
                vBeI_nav_transacciones_out = New clsBeI_nav_transacciones_out
                Cargar(vBeI_nav_transacciones_out, dr)
                lReturnList.Add(vBeI_nav_transacciones_out)
            Next

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Lotes_Pendientes_De_Envio_By_No_Pedido(ByVal pNoPedido As String,
                                                                          ByVal pTipoDocumento As Integer) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Get_All_Lotes_Pendientes_De_Envio_By_No_Pedido = 0

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim vSQL As String = "SELECT * FROM I_nav_transacciones_out 
                                  WHERE no_pedido = @NoPedido AND IdTipoDocumento = @IdTipoDocumento
                                  AND enviado =0 AND cantidad_pendiente > 0 AND auditar = 1 "

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            cmd.Parameters.AddWithValue("@NoPedido", pNoPedido)
            cmd.Parameters.AddWithValue("@IdTipoDocumento", pTipoDocumento)

            dad.Fill(dt)

            lTransaction.Commit()

            Return dt.Rows.Count > 0

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lTransaction.Dispose()
            lConnection.Dispose()
        End Try

    End Function

    '#CKFK20241212 Obtener ajustes por inventario cíclico
    Public Shared Function Get_Ajustes_Auditados_Pendientes_Envio_MI3_By_Inventario(ByRef pResult As String,
                                                                                    ByRef lblprg As RichTextBox,
                                                                                    ByRef lConnection As SqlConnection,
                                                                                    ByRef lTransaction As SqlTransaction) As List(Of clsBeAjustesMI3)

        Get_Ajustes_Auditados_Pendientes_Envio_MI3_By_Inventario = Nothing

        Dim lAjustesMI3 As New List(Of clsBeAjustesMI3)
        Dim lblprg As New RichTextBox
        Dim vNDoc As String = ""
        Dim lVistaAjustesPendientesEnvio As New List(Of clsBe_vw_ajustes)
        Dim vDif As Double = 0
        Dim vNoDocumento As String = ""
        Dim BeAjusteDet As New clsBeTrans_ajuste_det
        Dim DetallesEnviados As Integer = 0
        Dim BeFamilia As New clsBeProducto_familia
        Dim vSerieBodega As String = ""
        Dim BeCliente As New clsBeCliente
        Dim AjusteDoc As New clsBeTrans_ajuste_det_doc
        Dim MaxIdAjusteDoc As Integer = 0
        Dim BeAjusteMI3 As New clsBeAjustesMI3
        Dim vNomenclaturaBase As String = ""
        Dim vCorrelativoActual As Integer = 0
        Dim lAjustesPendEnvio As New List(Of clsBeTrans_ajuste_enc)

        Try

            lAjustesPendEnvio = clsLnTrans_ajuste_enc.Get_All_Auditados_Pendientes_Envio_By_IdInventarioEnc(lConnection, lTransaction)

            If Not lAjustesPendEnvio Is Nothing Then

                MaxIdAjusteDoc = clsLnTrans_ajuste_det_doc.MaxID(lConnection, lTransaction) + 1

                For Each AjEnc In lAjustesPendEnvio

                    vNoDocumento = Right("000000" & AjEnc.Idajusteenc, 6)

                    vNoDocumento = "WMS" + vNoDocumento
                    vNDoc = vNoDocumento

                    lVistaAjustesPendientesEnvio = clsLn_vw_ajustes.Get_All_Pendientes_Envio_Agrupados_By_Inventario(AjEnc.Idajusteenc,
                                                                                                                     lConnection,
                                                                                                                     lTransaction)

                    DetallesEnviados = 0

                    '#EJC20180711: Obtener la sección para NAV (Debe llenarse a traves del mantenimiento de familias y se selecciona en el encabezado del ajuste)
                    If AjEnc.IdProductoFamilia > 0 Then
                        BeFamilia = clsLnProducto_familia.GetSingle(AjEnc.IdProductoFamilia,
                                                                    lConnection,
                                                                    lTransaction)
                    End If

                    If lVistaAjustesPendientesEnvio.Count > 0 Then

                        clsPublic.Actualizar_Progreso(lblprg, "Detalle de ajustes para transacción: " & AjEnc.Idajusteenc)

                        Dim vCodigoBodega As String = clsLnBodega.Get_Codigo_By_IdBodega(AjEnc.IdBodega,
                                                                                         lConnection,
                                                                                         lTransaction)
                        Dim vCodigoBodegaERP As String = ""

                        For Each AjDet In lVistaAjustesPendientesEnvio

                            BeCliente = clsLnCliente.GetSingle(AjDet.IdBodegaERP,
                                                               lConnection,
                                                               lTransaction)

                            If Not BeCliente Is Nothing Then
                                vSerieBodega = BeCliente.Referencia
                                vCodigoBodegaERP = BeCliente.Codigo
                            Else
                                vSerieBodega = AjDet.Seccion
                                vCodigoBodegaERP = AjDet.IdBodegaERP
                                BeCliente = New clsBeCliente
                                BeCliente.Codigo = AjDet.IdBodegaERP
                            End If

                            AjusteDoc = New clsBeTrans_ajuste_det_doc()

                            '#CKFK 20180927 0953PM Agregué esta condición porque cuando la serie es nueva el correlativo actual va a ser igual a 999999
                            If vCorrelativoActual = 0 Then
                                vCorrelativoActual = Val(vNoDocumento.Substring(4, vNoDocumento.Length - 4))
                                vCorrelativoActual += 1
                            ElseIf vCorrelativoActual = 999999 Then
                                vCorrelativoActual = 1
                            Else
                                vCorrelativoActual += 1
                            End If

                            vNoDocumento = vNomenclaturaBase + Right("000000" & vCorrelativoActual, 6)

                            clsPublic.Actualizar_Progreso(lblprg, "Procesando ajuste número de documento: " & vNoDocumento)

                            BeAjusteDet.IdAjusteDet = AjDet.IdAjusteDet
                            BeAjusteDet.IdAjusteEnc = AjEnc.Idajusteenc
                            clsLnTrans_ajuste_det.GetSingle(BeAjusteDet, lConnection, lTransaction)

                            If Not BeFamilia Is Nothing Then
                                AjDet.Seccion = BeFamilia.Nombre
                            End If

                            '#CM_20180828_2:54PM: Llena datos para el documento de ajustes. 
                            AjusteDoc.Idajustedoc = MaxIdAjusteDoc
                            AjusteDoc.Idajusteenc = AjEnc.Idajusteenc

                            If AjDet.Modifica_Cantidad Then

                                If AjDet.Cantidad_original > AjDet.Cantidad_nueva Then 'Es un ajuste negativo

                                    vDif = Math.Round(AjDet.Cantidad_original - AjDet.Cantidad_nueva, 6)

                                    Try

                                        BeAjusteMI3 = New clsBeAjustesMI3()
                                        BeAjusteMI3.IdAjusteEnc = AjDet.IdAjusteEnc
                                        BeAjusteMI3.IdAjusteDet = AjDet.IdAjusteDet
                                        BeAjusteMI3.Codigo_Bodega = vCodigoBodega
                                        BeAjusteMI3.Codigo_Bodega_ERP = vCodigoBodegaERP
                                        BeAjusteMI3.NoDocumento = vNoDocumento
                                        BeAjusteMI3.Codigo_Producto = AjDet.Codigo_Producto
                                        BeAjusteMI3.TipoAjusteERP = IIf(AjEnc.Ajuste_Por_Inventario > 0, AjDet.Codigo_Bodega, BeCliente.Codigo)
                                        BeAjusteMI3.TipoAjusteWMS = AjDet.Tipo_Ajuste
                                        BeAjusteMI3.UMBas = AjDet.UMBas
                                        BeAjusteMI3.Cantidad = vDif
                                        BeAjusteMI3.Lote = AjDet.Lote_Nuevo
                                        BeAjusteMI3.Motivo_Ajuste = AjDet.Motivo_Ajuste
                                        BeAjusteMI3.Observacion = AjDet.Observacion
                                        BeAjusteMI3.Seccion = AjDet.Seccion
                                        BeAjusteMI3.IdCentroCosto = AjEnc.IdCentroCosto
                                        BeAjusteMI3.Codigo_Centro_Costo = clsLnCentro_costo.Get_Codigo_By_IdCentroCosto(AjEnc.IdCentroCosto)
                                        lAjustesMI3.Add(BeAjusteMI3)

                                        clsPublic.Actualizar_Progreso(lblprg, "Procesando ajuste negativo para: " & AjDet.Codigo_Producto & " " & AjDet.Nombre_Producto)

                                        DetallesEnviados += 1

                                    Catch ex As Exception

                                        clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                                   AjEnc.Idajusteenc,
                                                                                   0,
                                                                                   300)

                                        clsPublic.Actualizar_Progreso(lblprg, "Error al procesar el ajuste #: " & AjEnc.Idajusteenc)

                                        vCorrelativoActual -= 1

                                    End Try

                                ElseIf AjDet.Cantidad_original < AjDet.Cantidad_nueva Then 'Es un ajuste positivo

                                    vDif = Math.Round(AjDet.Cantidad_nueva - AjDet.Cantidad_original, 6)

                                    Try

                                        BeAjusteMI3 = New clsBeAjustesMI3()
                                        BeAjusteMI3.IdAjusteEnc = AjDet.IdAjusteEnc
                                        BeAjusteMI3.IdAjusteDet = AjDet.IdAjusteDet
                                        BeAjusteMI3.Codigo_Bodega = vCodigoBodega
                                        BeAjusteMI3.Codigo_Bodega_ERP = vCodigoBodegaERP
                                        BeAjusteMI3.NoDocumento = vNoDocumento
                                        BeAjusteMI3.Codigo_Producto = AjDet.Codigo_Producto
                                        BeAjusteMI3.TipoAjusteERP = IIf(AjEnc.Ajuste_Por_Inventario > 0, AjDet.Codigo_Bodega, BeCliente.Codigo)
                                        BeAjusteMI3.TipoAjusteWMS = AjDet.Tipo_Ajuste
                                        BeAjusteMI3.UMBas = AjDet.UMBas
                                        BeAjusteMI3.Cantidad = vDif
                                        BeAjusteMI3.Lote = AjDet.Lote_Nuevo
                                        BeAjusteMI3.Motivo_Ajuste = AjDet.Motivo_Ajuste
                                        BeAjusteMI3.Observacion = AjDet.Observacion
                                        BeAjusteMI3.Seccion = AjDet.Seccion
                                        BeAjusteMI3.IdCentroCosto = AjEnc.IdCentroCosto
                                        BeAjusteMI3.Codigo_Centro_Costo = clsLnCentro_costo.Get_Codigo_By_IdCentroCosto(AjEnc.IdCentroCosto)
                                        lAjustesMI3.Add(BeAjusteMI3)

                                        clsPublic.Actualizar_Progreso(lblprg, "Procesando ajuste positivo para: " & AjDet.Codigo_Producto & " " & AjDet.Nombre_Producto)

                                        BeAjusteDet.Enviado = True

                                        If AjEnc.Referencia.Trim = "" Then
                                            AjEnc.Referencia = vNoDocumento
                                        Else
                                            AjEnc.Referencia = AjEnc.Referencia
                                        End If

                                        DetallesEnviados += 1

                                    Catch ex As Exception

                                        clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                                    AjEnc.Idajusteenc,
                                                                                    0,
                                                                                    300)

                                        clsPublic.Actualizar_Progreso(lblprg, "Error al procesar el ajuste #: " & AjEnc.Idajusteenc)

                                        vCorrelativoActual -= 1

                                    End Try

                                End If

                            Else

                                '#EJC20180615: Es un tipo de ajuste que no se puede enviar a ERP, pej. cambio de lote
                                BeAjusteDet.Enviado = True
                                clsLnTrans_ajuste_det.Actualizar(BeAjusteDet)
                                DetallesEnviados += 1

                            End If

                            MaxIdAjusteDoc += 1

                        Next

                        vCorrelativoActual = 0

                    Else

                        clsLnI_nav_ejecucion_det_error.Inserta_Log("No hay detalle de ajustes válidos para el Id de ajuste #: " & AjEnc.Idajusteenc,
                                                                   AjEnc.Idajusteenc,
                                                                   0,
                                                                   300)

                        clsPublic.Actualizar_Progreso(lblprg, "No hay detalle de ajustes válidos para el Id de ajuste #: " & AjEnc.Idajusteenc)

                    End If

                    pResult = lblprg.Text
                Next

                Return lAjustesMI3

            Else
                clsPublic.Actualizar_Progreso(lblprg, "No hay ajustes auditados pendientes de envío.")
            End If

            clsPublic.Actualizar_Progreso(lblprg, "Fin de sincronización de ajustes.")

        Catch ex As Exception

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "Sync_Ajustes",
                                                       0,
                                                       300,)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al enviar ajustes a NAV: {0}{1}-{2}", vbNewLine, ex.Message, vNDoc))

            Throw ex

        Finally
            pResult = lblprg.Text
        End Try

    End Function

    Public Shared Function Get_Lotes_Salida_Pendientes_Envio(ByVal pTipo As clsDataContractDI.tTipoDocumentoSalida, lConnection As SqlConnection, lTransaction As SqlTransaction) As List(Of clsBeI_nav_transacciones_out)

        Try

            Dim lReturnList As New List(Of clsBeI_nav_transacciones_out)

            Dim sp As String = "SELECT * FROM I_nav_transacciones_out 
                                WHERE tipo_transaccion = 'SALIDA'  
                                AND enviado = 0 "

            sp &= IIf(pTipo <> 0, " AND IdTipoDocumento = " & pTipo, "")

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_transacciones_out As New clsBeI_nav_transacciones_out

            For Each dr As DataRow In dt.Rows

                vBeI_nav_transacciones_out = New clsBeI_nav_transacciones_out
                Cargar(vBeI_nav_transacciones_out, dr)

                vBeI_nav_transacciones_out.Codigo_Bodega_Destino = clsLnCliente.Get_Codigo_By_IdCliente(clsLnTrans_pe_enc.GetIdCliente(vBeI_nav_transacciones_out.Idpedidoenc, lConnection, lTransaction))
                vBeI_nav_transacciones_out.Codigo_Bodega_Origen = clsLnBodega.Get_Codigo_By_IdBodega(vBeI_nav_transacciones_out.Idbodega, lConnection, lTransaction)

                lReturnList.Add(vBeI_nav_transacciones_out)

            Next

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Lotes_Ingreso_Pendientes_Envio(ByVal IdTipoIngresoOC As Int16,
                                                              ByVal lConnection As SqlConnection,
                                                              ByVal lTransaction As SqlTransaction,
                                                              ByVal pIdBodega As Integer) As List(Of clsBeI_nav_transacciones_out)

        Dim lReturnList As New List(Of clsBeI_nav_transacciones_out)

        Try

            Dim sql As String = "SELECT 
                                    oc.IdTipoIngresoOC, 
                                    ino.*
                                FROM i_nav_transacciones_out AS ino
                                INNER JOIN trans_re_oc AS ro 
                                    ON ino.idrecepcionenc = ro.IdRecepcionEnc
                                INNER JOIN trans_oc_enc AS oc 
                                    ON ro.IdOrdenCompraEnc = oc.IdOrdenCompraEnc
                                WHERE ino.tipo_transaccion = 'INGRESO'
                                  AND oc.IdTipoIngresoOC = @IdTipoIngresoOC
                                  AND ino.Enviado =0 
                                  AND ino.idrecepcionenc IN (
                                      SELECT IdRecepcionEnc
                                      FROM trans_re_enc
                                      WHERE estado = 'Cerrado'
                                        AND IdBodega = @IdBodega
                                );"

            Dim cmd As New SqlCommand(sql, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            cmd.Parameters.AddWithValue("@idBodega", pIdBodega)
            cmd.Parameters.AddWithValue("@IdTipoIngresoOC", IdTipoIngresoOC)
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_transacciones_out As New clsBeI_nav_transacciones_out

            For Each dr As DataRow In dt.Rows
                vBeI_nav_transacciones_out = New clsBeI_nav_transacciones_out
                Cargar(vBeI_nav_transacciones_out, dr)
                lReturnList.Add(vBeI_nav_transacciones_out)
            Next

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Cantidad_Despachada(ByVal pIdDespachoEnc As Integer,
                                                   ByVal pConnection As SqlConnection,
                                                   ByVal pTransaction As SqlTransaction) As Double


        Try

            Dim lCantidadDespachada As Double = 0

            Const sp As String = "SELECT ISNULL(SUM(cantidad),0) 
                                  FROM I_nav_transacciones_out 
                                  WHERE IdDespachoEnc = @IdDespachoEnc  "

            Using lCommand As New SqlCommand(sp, pConnection)

                lCommand.CommandType = CommandType.Text
                lCommand.Transaction = pTransaction
                lCommand.Parameters.AddWithValue("@IdDespachoEnc", pIdDespachoEnc)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lCantidadDespachada = CDbl(lReturnValue)
                End If

            End Using

            Return lCantidadDespachada

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class