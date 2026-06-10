Imports System.IO
Imports System.Reflection
Imports System.Data.SqlClient
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen

Public Class frmAnaliticaA

    Private DT As New DataTable("StockEnUnaFecha")
    Public pBeListaProductos As New List(Of Integer)
    Public ListaMovimientos As New List(Of clsBeVW_Movimientos)
    Public RepMovEnUnaFecha As New List(Of clsBeStockEnUnaFecha)
    Public Property Modo As pModo
    Public Property ProductoEspecifico As New clsBeProducto
    Public Property ModoDepuracion As Boolean = False

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Class clsStockEnUnaFechaRep

        Public Property Codigo As String = ""
        Public Property Nombre As String = ""
        Public Property Lote As String = ""
        Public Property FechaVence As Date = Now
        Public Property InventarioInicial As Double = 0
        Public Property Ingresos As Double = 0
        Public Property Ajustes_Positivos As Double = 0
        Public Property Ajustes_Negativos As Double = 0
        Public Property Salidas As Double = 0
        Public Property Existencia_Teorica As Double = 0
        Public Property Existencia_Actual As Double = 0
        Public Property Diferencia As Double = 0

    End Class

    Private Sub SetDatataTable()

        DT.Columns.Add("Código", GetType(String))
        DT.Columns.Add("Nombre", GetType(String))
        DT.Columns.Add("Lote", GetType(String))
        DT.Columns.Add("Estado", GetType(String))
        DT.Columns.Add("Vence", GetType(Date))
        DT.Columns.Add("Inventario_Inicial", GetType(Double))
        DT.Columns.Add("Ingresos", GetType(Double))
        DT.Columns.Add("Ajustes_P", GetType(Double))
        DT.Columns.Add("Ajustes_N", GetType(Double))
        DT.Columns.Add("Salidas", GetType(Double))
        DT.Columns.Add("Existencia_Al", GetType(Double))
        DT.Columns.Add("Existencia_Actual", GetType(Double))
        DT.Columns.Add("Diferencia", GetType(Double))

    End Sub

    Public Function Get_Lista_Movimientos() As Boolean

        Get_Lista_Movimientos = False
        Dim cTrans As New clsTransaccion()

        Try

            If txtIdProducto.Text.Trim <> "" AndAlso Not ProductoEspecifico Is Nothing Then

                Dim IdProductoBodega As Integer = clsLnProducto_bodega.Get_IdProductoBodega_By_IdProducto_And_IdBodega(ProductoEspecifico.IdProducto, cmbBodega.EditValue)

                ListaMovimientos = clsLnTrans_movimientos.Get_All_Movimientos_By_IdProducto(dtpFechaDesde.Value,
                                                                                            dtpfechaHasta.Value,
                                                                                            IdProductoBodega,
                                                                                            cmbBodega.EditValue,
                                                                                            cmbPropietario.EditValue)
            Else

                If ProductoEspecifico Is Nothing OrElse ProductoEspecifico.IdProducto = 0 Then
                    lblPrg.Text = "Obteniendo listado de productos"
                    lblPrg.Refresh()
                    ListaMovimientos = clsLnTrans_movimientos.Get_All_Movimientos_By_Rango_Fechas(dtpFechaDesde.Value,
                                                                                                  dtpfechaHasta.Value)
                End If

            End If

            If Not ListaMovimientos Is Nothing Then
                Get_Lista_Movimientos = ListaMovimientos.Count > 0
            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Sub Generar_Reporte()

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Generando...")

            Dim BeStockEnFecha As New clsBeStockEnUnaFecha()
            Dim BeStockJornada As New clsBeStock_jornada()
            Dim Idx As Integer = -1
            Dim Idx1 As Integer = -1

            ListaMovimientos.Clear()
            RepMovEnUnaFecha.Clear()
            lblPrg.Visible = True

            If Get_Lista_Movimientos() Then

                RepMovEnUnaFecha.Clear()

                prg.Visible = True
                prg.Properties.Step = 1
                prg.Properties.PercentView = True
                prg.Properties.Maximum = ListaMovimientos.Count
                prg.Properties.Minimum = 0

                If Not ListaMovimientos Is Nothing Then

                    Dim TheGoalDate As Date = New Date(2019, 8, 30)

                    For Each ObjM In ListaMovimientos.OrderBy(Function(x) x.EstadoOrigen)

                        lblPrg.Text = "Procesando movimiento para producto: " & ObjM.Codigo
                        lblPrg.Refresh()

                        If ObjM.Fecha_Vence = TheGoalDate Then
                            Debug.Print("Wait a second!")
                        End If

                        If ObjM.Fecha_Vence = TheGoalDate AndAlso ObjM.EstadoOrigen = "SIN REGISTRO" AndAlso ObjM.TipoTarea = clsBeVW_Movimientos.pTipoTarea.DESP Then
                            Debug.Print("Wait a second!")
                        End If

                        BeStockEnFecha = New clsBeStockEnUnaFecha()
                        BeStockEnFecha.Codigo = ObjM.Codigo
                        BeStockEnFecha.Producto = ObjM.Producto
                        BeStockEnFecha.IdEstadoOrigen = ObjM.IdEstadoOrigen
                        BeStockEnFecha.Fecha_Vence = ObjM.Fecha_Vence
                        BeStockEnFecha.IdUnidadMedida = ObjM.IdUnidadMedida
                        clsPublic.CopyObject(ObjM, BeStockEnFecha)

                        Idx = RepMovEnUnaFecha.FindIndex(Function(x) x.Codigo = BeStockEnFecha.Codigo _
                                                         AndAlso x.IdEstadoOrigen = BeStockEnFecha.IdEstadoOrigen _
                                                         AndAlso x.Fecha_Vence = BeStockEnFecha.Fecha_Vence)

                        If Idx <> -1 Then 'Lo encontró por lote.

                            Idx1 = RepMovEnUnaFecha.FindIndex(Function(x) x.Codigo = BeStockEnFecha.Codigo _
                              AndAlso x.Lote = BeStockEnFecha.Lote _
                              AndAlso x.Fecha_Vence.Date = BeStockEnFecha.Fecha_Vence.Date)

                            If Idx1 = -1 Then 'No coincide la fecha de vencimiento para el mismo lote en el mismo movimiento
                                '(Por error en el cambio de ubicación fecha_vence = now -> JP.)
                                Debug.Print("Espera")
                                'Magia por EJC para corregir cagada.
                                If RepMovEnUnaFecha(Idx).Fecha_Vence.Date > BeStockEnFecha.Fecha_Vence.Date Then
                                    'BeStockEnFecha.Fecha_Vence = RepMovEnUnaFecha(Idx).Fecha_Vence.Date
                                    Debug.Print(BeStockEnFecha.Codigo)
                                End If
                            End If

                            'Si no tiene contro por lote...
                            If BeStockEnFecha.Lote = "" Then

                                Idx1 = RepMovEnUnaFecha.FindIndex(Function(x) x.Codigo = BeStockEnFecha.Codigo _
                                                                  AndAlso x.Fecha_Vence.Date = BeStockEnFecha.Fecha_Vence.Date)
                                If Idx1 = -1 Then 'No coincide la fecha de vencimiento.. no pasa nada
                                    Idx = Idx1
                                Else
                                    Idx1 = RepMovEnUnaFecha.FindIndex(Function(x) x.Codigo = BeStockEnFecha.Codigo _
                                                                      AndAlso x.Fecha_Vence.Date = BeStockEnFecha.Fecha_Vence.Date _
                                                                      AndAlso x.IdEstadoOrigen = BeStockEnFecha.IdEstadoOrigen)
                                    If Idx1 = -1 Then 'No coincide el estado
                                        Idx = Idx1
                                    Else
                                        Idx = Idx1
                                    End If

                                End If

                            End If

                        Else

                            Idx = RepMovEnUnaFecha.FindIndex(Function(x) x.Codigo = BeStockEnFecha.Codigo _
                                                             AndAlso x.Fecha_Vence = BeStockEnFecha.Fecha_Vence)

                            If Idx <> -1 Then 'Lo encontró por FechaVence.

                                Debug.Print(BeStockEnFecha.Codigo)

                                If RepMovEnUnaFecha(Idx).Fecha_Vence.Date > BeStockEnFecha.Fecha_Vence.Date Then
                                    'BeStockEnFecha.Fecha_Vence = RepMovEnUnaFecha(Idx).Fecha_Vence.Date
                                    Debug.Print(BeStockEnFecha.Codigo)
                                End If

                            End If

                        End If

                        If Idx = -1 Then
                            RepMovEnUnaFecha.Add(BeStockEnFecha)
                        Else
                            BeStockEnFecha = RepMovEnUnaFecha(Idx) 'Puntero =>
                        End If

                        If ObjM.TipoTarea = clsBeVW_Movimientos.pTipoTarea.INVE Then
                            BeStockEnFecha.Inventario_Inicial += ObjM.Cantidad
                        ElseIf ObjM.TipoTarea = clsBeVW_Movimientos.pTipoTarea.RECE Then
                            BeStockEnFecha.Ingresos += ObjM.Cantidad
                        ElseIf ObjM.TipoTarea = clsBeVW_Movimientos.pTipoTarea.AJCANTP OrElse ObjM.TipoTarea = clsBeVW_Movimientos.pTipoTarea.AJCANTPI Then
                            BeStockEnFecha.Ajuste_Positivo += ObjM.Cantidad
                        ElseIf ObjM.TipoTarea = clsBeVW_Movimientos.pTipoTarea.AJCANTN OrElse ObjM.TipoTarea = clsBeVW_Movimientos.pTipoTarea.AJCANTNI Then
                            BeStockEnFecha.Ajuste_Negativo += ObjM.Cantidad
                        ElseIf ObjM.TipoTarea = clsBeVW_Movimientos.pTipoTarea.DESP OrElse ObjM.TipoTarea = clsBeVW_Movimientos.pTipoTarea.TRAS Then
                            'EJC20260602_STOCK_FECHA: TRAS en la bodega origen debe descontar existencia teórica.
                            BeStockEnFecha.Salidas += ObjM.Cantidad
                        Else
                            Debug.Print(ObjM.TipoTarea)
                        End If

                        Debug.Print(ObjM.TipoTarea)

                        prg.PerformStep()

                        Application.DoEvents()

                    Next

                    '                    For Each R As DataRow In DTVWStockJornada.Rows

                    '                        vIdOrdenCompraEnc = IIf(IsDBNull(R.Item("IdOrdenCompraEnc")), 0, R.Item("IdOrdenCompraEnc"))

                    '                        If vIndiceDocumentoIngreso = -1 Then

                    '                            Dim vCalcularCostoUnitario As Boolean = False


                    '                            If vCalcularCostoUnitario Then
                    '                                vCostoUnitarioLinea = clsLnTrans_oc_det.Get_Costo_Unitario_By_IdOrdenCompraEnc_And_IdRecepcionDet(vIdOrdenCompraEnc,
                    '                                                                                                                                  vIdRecepcionDet,
                    '                                                                                                                                  lConnection,
                    '                                                                                                                                  lTransaction)
                    '                            End If

                    '                            vCostoUnitario = vCostoUnitarioLinea

                    '                        End If

                    '                        BeStockJornada = New clsBeStock_jornada()
                    '                        BeStockJornada.IdStockJornada = vIdStockJornada
                    '                        BeStockJornada.IdJornadaSistema = BeJornada.IdJornada
                    '                        BeStockJornada.Fecha = pFechaJornada
                    '                        BeStockJornada.IdBodega = IIf(IsDBNull(R.Item("IdBodega")), 0, R.Item("IdBodega"))
                    '                        BeStockJornada.IdStock = IIf(IsDBNull(R.Item("IdStock")), 0, R.Item("IdStock"))
                    '                        BeStockJornada.IdPropietarioBodega = IIf(IsDBNull(R.Item("IdPropietarioBodega")), 0, R.Item("IdPropietarioBodega"))
                    '                        BeStockJornada.IdProductoBodega = IIf(IsDBNull(R.Item("IdProductoBodega")), 0, R.Item("IdProductoBodega"))
                    '                        BeStockJornada.IdProductoEstado = IIf(IsDBNull(R.Item("IdProductoEstado")), 0, R.Item("IdProductoEstado"))
                    '                        BeStockJornada.IdPresentacion = IIf(IsDBNull(R.Item("IdPresentacion")), Nothing, R.Item("IdPresentacion"))
                    '                        BeStockJornada.IdUnidadMedida = IIf(IsDBNull(R.Item("IdUnidadMedida")), 0, R.Item("IdUnidadMedida"))
                    '                        BeStockJornada.IdUbicacion = IIf(IsDBNull(R.Item("IdUbicacion")), 0, R.Item("IdUbicacion"))
                    '                        BeStockJornada.IdUbicacion_anterior = IIf(IsDBNull(R.Item("IdUbicacion_anterior")), 0, R.Item("IdUbicacion_anterior"))
                    '                        BeStockJornada.IdRecepcionEnc = IIf(IsDBNull(R.Item("IdRecepcionEnc")), 0, R.Item("IdRecepcionEnc"))
                    '                        BeStockJornada.IdRecepcionDet = IIf(IsDBNull(R.Item("IdRecepcionDet")), 0, R.Item("IdRecepcionDet"))
                    '                        BeStockJornada.IdPedidoEnc = IIf(IsDBNull(R.Item("IdPedidoEnc")), 0, R.Item("IdPedidoEnc"))
                    '                        BeStockJornada.IdPickingEnc = IIf(IsDBNull(R.Item("IdPickingEnc")), 0, R.Item("IdPickingEnc"))
                    '                        BeStockJornada.IdDespachoEnc = IIf(IsDBNull(R.Item("IdDespachoEnc")), 0, R.Item("IdDespachoEnc"))
                    '                        BeStockJornada.Lote = IIf(IsDBNull(R.Item("lote")), "", R.Item("lote"))
                    '                        BeStockJornada.Lic_plate = IIf(IsDBNull(R.Item("lic_plate")), "", R.Item("lic_plate"))
                    '                        BeStockJornada.Serial = IIf(IsDBNull(R.Item("serial")), "", R.Item("serial"))
                    '                        BeStockJornada.Cantidad = IIf(IsDBNull(R.Item("cantidad")), 0, R.Item("cantidad"))
                    '                        BeStockJornada.Fecha_ingreso = IIf(IsDBNull(R.Item("fecha_ingreso")), "01/01/1900", R.Item("fecha_ingreso"))
                    '                        BeStockJornada.Fecha_vence = IIf(IsDBNull(R.Item("fecha_vence")), "01/01/1900", R.Item("fecha_vence"))
                    '                        BeStockJornada.Uds_lic_plate = IIf(IsDBNull(R.Item("uds_lic_plate")), 0, R.Item("uds_lic_plate"))
                    '                        BeStockJornada.No_bulto = IIf(IsDBNull(R.Item("no_bulto")), 0, R.Item("no_bulto"))
                    '                        BeStockJornada.Fecha_manufactura = IIf(IsDBNull(R.Item("fecha_manufactura")), "01/01/1900", R.Item("fecha_manufactura"))
                    '                        BeStockJornada.Añada = IIf(IsDBNull(R.Item("añada")), 0, R.Item("añada"))
                    '                        BeStockJornada.User_agr = IIf(IsDBNull(R.Item("user_agr")), "", R.Item("user_agr"))
                    '                        BeStockJornada.Fec_agr = IIf(IsDBNull(R.Item("fec_agr")), "01/01/1900", R.Item("fec_agr"))
                    '                        BeStockJornada.User_mod = IIf(IsDBNull(R.Item("user_mod")), "", R.Item("user_mod"))
                    '                        BeStockJornada.Fec_mod = IIf(IsDBNull(R.Item("fec_mod")), "01/01/1900", R.Item("fec_mod"))
                    '                        BeStockJornada.Activo = IIf(IsDBNull(R.Item("activo")), False, R.Item("activo"))
                    '                        BeStockJornada.Peso = IIf(IsDBNull(R.Item("peso")), 0, R.Item("peso"))
                    '                        BeStockJornada.Temperatura = IIf(IsDBNull(R.Item("temperatura")), 0, R.Item("temperatura"))
                    '                        BeStockJornada.Atributo_variante_1 = IIf(IsDBNull(R.Item("atributo_variante_1")), Nothing, R.Item("atributo_variante_1"))
                    '                        BeStockJornada.Pallet_no_estandar = IIf(IsDBNull(R.Item("pallet_no_estandar")), False, R.Item("pallet_no_estandar"))
                    '                        BeStockJornada.Propietario = IIf(IsDBNull(R.Item("Propietario")), "", R.Item("Propietario"))
                    '                        BeStockJornada.Proveedor = IIf(IsDBNull(R.Item("Proveedor")), "", R.Item("Proveedor"))
                    '                        BeStockJornada.Bodega = IIf(IsDBNull(R.Item("Bodega")), "", R.Item("Bodega"))
                    '                        BeStockJornada.IdOrdenCompraEnc = IIf(IsDBNull(R.Item("IdOrdenCompraEnc")), 0, R.Item("IdOrdenCompraEnc"))
                    '                        BeStockJornada.No_DocumentoOC = IIf(IsDBNull(R.Item("No_DocumentoOC")), "", R.Item("No_DocumentoOC"))
                    '                        BeStockJornada.No_DocumentoRec = IIf(IsDBNull(R.Item("No_DocumentoRec")), "", R.Item("No_DocumentoRec"))
                    '                        BeStockJornada.ReferenciaOC = IIf(IsDBNull(R.Item("ReferenciaOC")), "", R.Item("ReferenciaOC"))
                    '                        BeStockJornada.Fecha_Recepcion = IIf(IsDBNull(R.Item("Fecha_Recepcion")), New Date(1900, 1, 1), R.Item("Fecha_Recepcion"))
                    '                        BeStockJornada.TipoTrans = IIf(IsDBNull(R.Item("TipoTrans")), "", R.Item("TipoTrans"))
                    '                        BeStockJornada.Fecha_Agrego = IIf(IsDBNull(R.Item("Fecha_Agrego")), New Date(1900, 1, 1), R.Item("Fecha_Agrego"))
                    '                        BeStockJornada.Codigo_producto = IIf(IsDBNull(R.Item("codigo_producto")), "", R.Item("codigo_producto"))
                    '                        BeStockJornada.Codigo_barra_producto = IIf(IsDBNull(R.Item("codigo_barra_producto")), "", R.Item("codigo_barra_producto"))
                    '                        BeStockJornada.Nombre_producto = IIf(IsDBNull(R.Item("nombre_producto")), "", R.Item("nombre_producto"))
                    '                        BeStockJornada.Existencia = IIf(IsDBNull(R.Item("existencia")), 0, R.Item("existencia"))
                    '                        BeStockJornada.Nom_umBas = IIf(IsDBNull(R.Item("nom_umBas")), "", R.Item("nom_umBas"))
                    '                        BeStockJornada.Nom_estado_producto = IIf(IsDBNull(R.Item("nom_estado_producto")), "", R.Item("nom_estado_producto"))
                    '                        BeStockJornada.Ubicacion_origen = IIf(IsDBNull(R.Item("ubicacion_origen")), "", R.Item("ubicacion_origen"))
                    '                        BeStockJornada.No_poliza = IIf(IsDBNull(R.Item("no_poliza")), "", R.Item("no_poliza"))

                    '                        '#EJC20210604: Valores unitarios en stock_jornada
                    '                        'https://dev.azure.com/ejcalderon0892/CEALSA/_workitems/edit/54/
                    '                        BeStockJornada.Valor_aduana = IIf(IsDBNull(R.Item("valor_aduana")), 0, R.Item("valor_aduana"))
                    '                        BeStockJornada.Valor_fob = IIf(IsDBNull(R.Item("valor_fob")), 0, R.Item("valor_fob"))
                    '                        BeStockJornada.Valor_iva = IIf(IsDBNull(R.Item("valor_iva")), 0, R.Item("valor_iva"))
                    '                        BeStockJornada.Valor_dai = IIf(IsDBNull(R.Item("valor_dai")), 0, R.Item("valor_dai"))
                    '                        BeStockJornada.Valor_seguro = IIf(IsDBNull(R.Item("valor_seguro")), 0, R.Item("valor_seguro"))
                    '                        BeStockJornada.Costo_Unitario = vCostoUnitario
                    '                        BeStockJornada.Peso_neto = IIf(IsDBNull(R.Item("peso_neto")), 0, R.Item("peso_neto"))
                    '                        BeStockJornada.Numero_orden = IIf(IsDBNull(R.Item("numero_orden")), 0, R.Item("numero_orden"))
                    '                        BeStockJornada.Codigo_regimen = IIf(IsDBNull(R.Item("codigo_regimen")), 0, R.Item("codigo_regimen"))
                    '                        BeStockJornada.Nombre_regimen = IIf(IsDBNull(R.Item("nombre_regimen")), "", R.Item("nombre_regimen"))
                    '                        BeStockJornada.Dias_vencimiento_regimen = IIf(IsDBNull(R.Item("dias_vencimiento_regimen")), 0, R.Item("dias_vencimiento_regimen"))
                    '                        BeStockJornada.Factor = IIf(IsDBNull(R.Item("Factor")), 0, R.Item("Factor"))
                    '                        BeStockJornada.CamasPorTarima = IIf(IsDBNull(R.Item("CamasPorTarima")), 0, R.Item("CamasPorTarima"))
                    '                        BeStockJornada.CajasPorCama = IIf(IsDBNull(R.Item("CajasPorCama")), 0, R.Item("CajasPorCama"))
                    '                        BeStockJornada.Fecha_Ingreso_Ticket_TMS = IIf(IsDBNull(R.Item("Fecha_Ingreso_Ticket_TMS")), BeStockJornada.Fecha_Recepcion, R.Item("Fecha_Ingreso_Ticket_TMS"))
                    '                        BeStockJornada.IdTicketTMS = Val(IIf(IsDBNull(R.Item("IdTicketTMS")), 0, R.Item("IdTicketTMS")))
                    '                        BeStockJornada.IdPropietario = Val(IIf(IsDBNull(R.Item("IdPropietario")), 0, R.Item("IdPropietario")))
                    '                        BeStockJornada.IdClasificacion = Val(IIf(IsDBNull(R.Item("IdClasificacion")), 0, R.Item("IdClasificacion")))
                    '                        BeStockJornada.Clasificacion = IIf(IsDBNull(R.Item("Clasificacion")), "", R.Item("Clasificacion"))
                    '                        BeStockJornada.Regimen = IIf(IsDBNull(R.Item("Regimen")), "", R.Item("Regimen"))
                    '                        BeStockJornada.Nom_presentacion_producto = IIf(IsDBNull(R.Item("Presentacion_Producto")), "", R.Item("Presentacion_Producto"))

                    '                        '#EJC20210618: Optimizado, se obtiene la lista completo primero.-
                    '                        'GT 020820211028: En la lista completa, se valida que el id stock coincida con el detalle, para traer las posiciones ocupadas.
                    '                        BeStockDet = lBeStockDet.Find(Function(x) x.IdStock = BeStockJornada.IdStock)
                    '                        '#EJC20210603:Posiciones cealsa.
                    '                        'BeStockDet = clsLnStock_det.GetSingle(BeStockJornada.IdStock, lConnection, lTransaction)
                    '                        If Not BeStockDet Is Nothing Then
                    '                            BeStockJornada.Posiciones = BeStockDet.Posiciones
                    '                        Else
                    '                            BeStockJornada.Posiciones = 1
                    '                        End If

                    '                        Dim tickettms As New clsBeTms_ticket()

                    '                        If Not pListaIngresosYSalidasDelDia Is Nothing Then

                    '                            '#EJC20210519: Buscar en la lista si este producto existe con estos criterios
                    '                            vSingularidadStock = pListaIngresosYSalidasDelDia.FindAll(Function(x) x.IdProductoBodega = BeStockJornada.IdProductoBodega _
                    '                                                                                          AndAlso x.IdPropietarioBodega = BeStockJornada.IdPropietarioBodega _
                    '                                                                                          AndAlso x.Lote = BeStockJornada.Lote _
                    '                                                                                          AndAlso x.Lic_Plate = BeStockJornada.Lic_plate _
                    '                                                                                          AndAlso x.IdUbicacionOrigen = BeStockJornada.IdUnidadMedida _
                    '                                                                                          AndAlso x.IdPresentacion = BeStockJornada.IdPresentacion)

                    '                            'Si encuentra registros que coincidan sumarán al stock de la jornada el total que ingresó.
                    '                            For Each vs In vSingularidadStock
                    '                                BeStockJornada.Cantidad_Ingreso_Afecta_A_salida += vs.Ingresos
                    '                            Next

                    '                        End If

                    '                        Dim Hash As String = clsBeStock_jornada.GetRecordHash(BeStockJornada)
                    '                        BeStockJornada.Stock_Jornada_Hash = Hash
                    '                        BeStockJornada.Stock_Jornada_Hash = ""
                    '                        lJornada.Add(BeStockJornada)

                    '#Region "Retroactivo"

                    '                        vTicketTMSProcesado = False

                    '                        If lTicketsProcesados.Count > 0 Then
                    '                            If Not lTicketsProcesados.ContainsKey(BeStockJornada.IdTicketTMS) Then
                    '                                '#CKFK 20210523 Puse esto en comentario porque daba error ya que el ticket no existe en la lista lTicketsProcesados y daba error
                    '                                ' Dim num As Boolean = lTicketsProcesados.Item(BeStockJornada.IdTicketTMS)
                    '                                vTicketTMSProcesado = clsLnTms_ticket.Ticket_Procesado_Stock_Jornada(BeStockJornada.IdTicketTMS, lConnection, lTransaction)
                    '                                lTicketsProcesados.Add(BeStockJornada.IdTicketTMS, vTicketTMSProcesado)
                    '                                ' Console.WriteLine(num)
                    '                            Else
                    '                                Dim num As Boolean = lTicketsProcesados.Item(BeStockJornada.IdTicketTMS)
                    '                                vTicketTMSProcesado = lTicketsProcesados.Item(BeStockJornada.IdTicketTMS)
                    '                                Console.WriteLine(num)
                    '                            End If
                    '                        Else
                    '                            vTicketTMSProcesado = clsLnTms_ticket.Ticket_Procesado_Stock_Jornada(BeStockJornada.IdTicketTMS, lConnection, lTransaction)
                    '                            lTicketsProcesados.Add(BeStockJornada.IdTicketTMS, vTicketTMSProcesado)
                    '                        End If

                    '                        '#EJC20210521: Si el ticket ya fue procesado, no volver a escribir los días en el retroactivo.
                    '                        If Not vTicketTMSProcesado Then

                    '                            vDifDiasIngreso = DateDiff(DateInterval.Day, BeStockJornada.Fecha_Ingreso_Ticket_TMS, BeStockJornada.Fecha_Recepcion)

                    '                            If vDifDiasIngreso <> 0 Then

                    '                                Dim vDiaRetroaActivo As Date = BeStockJornada.Fecha_Ingreso_Ticket_TMS()

                    '                                vExisteRetroactivoDia = clsLnStock_jornada.Existe_Hash_Retroactivo_By_Fecha(Hash, vDiaRetroaActivo, lConnection, lTransaction)

                    '                                If Not vExisteRetroactivoDia Then

                    '                                    Dim BeStockJornadaRetroActiva As New clsBeStock_jornada

                    '                                    For i = 0 To vDifDiasIngreso - 1
                    '                                        BeStockJornadaRetroActiva = New clsBeStock_jornada()
                    '                                        clsPublic.CopyObject(BeStockJornada, BeStockJornadaRetroActiva)
                    '                                        BeStockJornadaRetroActiva.Fecha = vDiaRetroaActivo 'FormatoFechas.tFecha(vDiaRetroaActivo)
                    '                                        BeStockJornadaRetroActiva.Es_Retroactivo = True
                    '                                        Hash = clsBeStock_jornada.GetRecordHash(BeStockJornada)
                    '                                        BeStockJornadaRetroActiva.Stock_Jornada_Hash = ""
                    '                                        Debug.Print("I: " & i)
                    '                                        lJornada.Add(BeStockJornadaRetroActiva)
                    '                                        vDiaRetroaActivo = vDiaRetroaActivo.AddDays(1)
                    '                                        clsLnStock_jornada.Insertar(BeStockJornadaRetroActiva, lConnection, lTransaction)
                    '                                    Next

                    '                                End If

                    '                            End If

                    '                        End If
                    '#End Region

                    '                        clsLnStock_jornada.Insertar(BeStockJornada, lConnection, lTransaction)

                    '                        vIdStockJornada += 1

                    '                        Application.DoEvents()

                    '                        prg.Value = vContador
                    '                        vContador += 1

                    '                    Next

                End If

                Llena_Grid()

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            SplashScreenManager.CloseForm(False)
            prg.Visible = False
        End Try

    End Sub

    Private Sub Llena_Grid()

        Dim cTrans As New clsTransaccion
        Dim ExistenciasAl As Double
        Dim ExistenciaActualConFechaYEstado As Double
        Dim Diferencia As Double
        Dim pBeStock As New clsBeStock
        Dim pBeStockSinEstado As New clsBeStock
        Dim BeDiferencia As New clsBeDiferencias_movimientos
        Dim vIdDiferencia As Integer = 0
        Dim vExistenciaSinEstado As Double = 0
        Dim lMovimientos As New List(Of clsBeTrans_movimientos)
        Dim dExistenciaConEstado As New Dictionary(Of String, Double)
        Dim dExistenciaSinEstado As New Dictionary(Of String, Double)

        Try

            cTrans.Begin_Transaction()

            Dim Lista = From i In RepMovEnUnaFecha Group i By Keys = New With {Key i.IdProductoBodega,
                                                                                Key i.Codigo,
                                                                                Key i.Producto,
                                                                                Key i.UMBas,
                                                                                Key i.EstadoOrigen,
                                                                                Key i.IdEstadoOrigen,
                                                                                Key i.IdPresentacion,
                                                                                Key i.IdUnidadMedida,
                                                                                Key i.Lote,
                                                                                Key i.Fecha_Vence} Into Group
                        Select New With {Keys.Codigo,
                                        Keys.Producto,
                                        Keys.UMBas,
                                        Keys.IdEstadoOrigen,
                                        Keys.Lote,
                                        Keys.EstadoOrigen,
                                        Keys.Fecha_Vence,
                                        Keys.IdUnidadMedida,
                                        Keys.IdPresentacion,
                                        Keys.IdProductoBodega,
                                        .AjusteNegativo = Group.Sum(Function(x) x.Ajuste_Negativo),
                                        .AjustePositivo = Group.Sum(Function(x) x.Ajuste_Positivo),
                                        .Ingresos = Group.Sum(Function(x) x.Ingresos),
                                        .Salidas = Group.Sum(Function(x) x.Salidas),
                                        .Inventario_Inicial = Group.Sum(Function(x) x.Inventario_Inicial)}

            dgrid.DataSource = Nothing

            If Lista IsNot Nothing AndAlso Lista.Count > 0 Then
                Cargar_Snapshot_Existencias(cTrans.lConnection,
                                            cTrans.lTransaction,
                                            cmbBodega.EditValue,
                                            cmbPropietario.EditValue,
                                            dExistenciaConEstado,
                                            dExistenciaSinEstado)

                prg.Visible = True
                prg.Properties.Step = 1
                prg.Properties.PercentView = True
                prg.Properties.Maximum = Lista.Count
                prg.Properties.Minimum = 0
                prg.EditValue = 0

                DT.Rows.Clear()

                If ModoDepuracion Then

                    vIdDiferencia = clsLnDiferencias_movimientos.MaxID() + 1

                    clsLnDiferencias_movimientos.EliminarTodos()

                End If

                For Each Obj In Lista

                    lblPrg.Text = "Calculando inventarios para producto: " & Obj.Codigo
                    lblPrg.Refresh()

                    ExistenciasAl = ((Obj.Inventario_Inicial + Obj.Ingresos + Obj.AjustePositivo) - (Obj.AjusteNegativo + Obj.Salidas))

                    pBeStock.IdProductoBodega = Obj.IdProductoBodega
                    pBeStock.ProductoEstado = New clsBeProducto_estado
                    pBeStock.ProductoEstado.IdEstado = Obj.IdEstadoOrigen
                    pBeStock.IdProductoEstado = Obj.IdEstadoOrigen
                    pBeStock.Presentacion = New clsBeProducto_Presentacion
                    pBeStock.IdPresentacion = Obj.IdPresentacion
                    pBeStock.Presentacion.IdPresentacion = pBeStock.IdPresentacion
                    pBeStock.Presentacion = clsLnProducto_presentacion.GetSingle(pBeStock.Presentacion.IdPresentacion, cTrans.lConnection, cTrans.lTransaction)
                    pBeStock.IdUnidadMedida = Obj.IdUnidadMedida
                    pBeStock.Fecha_vence = Obj.Fecha_Vence
                    pBeStock.Lote = Obj.Lote
                    pBeStock.IsReportStockEnFecha = True
                    pBeStock.IncluirUbicacionesDespacho = True

                    If Obj.IdPresentacion <> 0 Then
                        ExistenciasAl = Math.Round(ExistenciasAl / pBeStock.Presentacion.Factor, 6)
                        Obj.Ingresos = Math.Round(Obj.Ingresos / pBeStock.Presentacion.Factor, 6)
                        Obj.Salidas = Math.Round(Obj.Salidas / pBeStock.Presentacion.Factor, 6)
                    End If

                    clsPublic.CopyObject(pBeStock, pBeStockSinEstado)

                    Dim kConEstado As String = Armar_Clave_Existencia(Obj.IdProductoBodega,
                                                                       Obj.IdEstadoOrigen,
                                                                       Obj.IdPresentacion,
                                                                       Obj.IdUnidadMedida,
                                                                       Obj.Lote,
                                                                       Obj.Fecha_Vence,
                                                                       True)

                    Dim kSinEstado As String = Armar_Clave_Existencia(Obj.IdProductoBodega,
                                                                       0,
                                                                       Obj.IdPresentacion,
                                                                       Obj.IdUnidadMedida,
                                                                       Obj.Lote,
                                                                       Obj.Fecha_Vence,
                                                                       False)

                    ExistenciaActualConFechaYEstado = Get_Valor_Existencia(dExistenciaConEstado, kConEstado)
                    vExistenciaSinEstado = Get_Valor_Existencia(dExistenciaSinEstado, kSinEstado)

                    If Obj.IdPresentacion <> 0 Then
                        ExistenciaActualConFechaYEstado = Math.Round(ExistenciaActualConFechaYEstado / IIf(pBeStock.Presentacion.Factor > 0, pBeStock.Presentacion.Factor, 1), 6)
                        vExistenciaSinEstado = Math.Round(vExistenciaSinEstado / pBeStock.Presentacion.Factor, 6)
                    End If

                    BeDiferencia = New clsBeDiferencias_movimientos()

                    If ExistenciasAl > 0 Then

                        Diferencia = Math.Round(ExistenciaActualConFechaYEstado, 3) - Math.Round(ExistenciasAl, 3)

                        If ExistenciasAl > ExistenciaActualConFechaYEstado Then
                            BeDiferencia.FaltaStock = True
                        End If

                    Else
                        Diferencia = Math.Round(ExistenciaActualConFechaYEstado, 3) + Math.Round(ExistenciasAl, 3)
                    End If

                    If Diferencia <> 0 Then

                        Dim vDiferenciaExistencia As Double = vExistenciaSinEstado - ExistenciaActualConFechaYEstado

                        If vDiferenciaExistencia > 0 Then
                            Diferencia += vDiferenciaExistencia
                        End If

                    End If

                    If Diferencia <> 0 AndAlso ModoDepuracion Then

                        If ExistenciasAl < 0 AndAlso ExistenciaActualConFechaYEstado = 0 Then

                            lMovimientos = clsLnTrans_movimientos.Get_Movimiento_Despacho_By_Stock(pBeStock)

                            If Not lMovimientos Is Nothing Then

                                If lMovimientos.Count > 0 Then

                                    For Each M In lMovimientos

                                        If Diferencia <> 0 Then

                                            Debug.Print("IdMovimiento: " & M.IdMovimiento & " " & M.Cantidad)

                                            If M.Cantidad >= Math.Abs(Diferencia) Then
                                                M.Cantidad += Diferencia
                                                Diferencia += 1
                                            Else
                                                M.Cantidad = 0
                                                Diferencia += M.Cantidad
                                            End If

                                            M.Serie = "#EJCAJUSTEDESFASE"

                                            If M.Cantidad = 0 Then
                                                clsLnTrans_movimientos.Eliminar(M)
                                            Else
                                                clsLnTrans_movimientos.Actualizar(M)
                                            End If

                                            lblPrg.Text = "Ajustando: " & Obj.Codigo & " Cantidad: " & Diferencia
                                            lblPrg.Refresh()

                                        End If

                                    Next

                                End If

                            End If

                        End If

                    End If


                    If Diferencia <> 0 AndAlso ModoDepuracion Then

                        Debug.Print("Why difference is Not 0? at: " & Obj.Codigo)

                        vExistenciaSinEstado = pBeStock.Cantidad

                        BeDiferencia.IdDiferencia = vIdDiferencia
                        BeDiferencia.IdProductoBodega = Obj.IdProductoBodega
                        BeDiferencia.Codigo = Obj.Codigo
                        BeDiferencia.Nombre = Obj.Producto
                        BeDiferencia.Lote = Obj.Lote
                        BeDiferencia.IdProductoEstado = Obj.IdEstadoOrigen
                        BeDiferencia.Estado = Obj.EstadoOrigen
                        BeDiferencia.FechaVence = Obj.Fecha_Vence
                        BeDiferencia.InventarioInicial = Obj.Inventario_Inicial
                        BeDiferencia.Ingresos = Obj.Ingresos
                        BeDiferencia.AjustesPositivos = Obj.AjustePositivo
                        BeDiferencia.AjustesNegativos = Obj.AjusteNegativo
                        BeDiferencia.Salidas = Obj.Salidas
                        BeDiferencia.ExistenciaAl = ExistenciasAl
                        BeDiferencia.ExistenciaActual = ExistenciaActualConFechaYEstado
                        BeDiferencia.ExistenciaSinEstado = vExistenciaSinEstado
                        BeDiferencia.Diferencia = Diferencia
                        clsLnDiferencias_movimientos.Insertar(BeDiferencia)

                        vIdDiferencia += 1

                    End If

                    DT.Rows.Add(Obj.Codigo,
                                Obj.Producto,
                                Obj.Lote,
                                Obj.EstadoOrigen,
                                Obj.Fecha_Vence,
                                Obj.Inventario_Inicial,
                                Obj.Ingresos,
                                Obj.AjustePositivo,
                                Obj.AjusteNegativo,
                                Obj.Salidas,
                                ExistenciasAl,
                                ExistenciaActualConFechaYEstado,
                                Diferencia)

                    Application.DoEvents()

                    prg.PerformStep()

                Next

                cTrans.Commit_Transaction()

                dgrid.DataSource = DT

                GridView1.OptionsView.ShowFooter = True

                GridView1.Columns("Código").Group()

                DT.Columns("Existencia_Al").Caption = "Existencia_Al: " & dtpfechaHasta.Value.Date

                Dim item0 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "Inventario_Inicial",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "Inv_Ini: {0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("Inventario_Inicial")}
                GridView1.GroupSummary.Add(item0)

                Dim item As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "Ingresos",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "Ingresos: {0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("Ingresos")}
                GridView1.GroupSummary.Add(item)

                Dim item1 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "Ajustes_P",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("Ajustes_P")}
                GridView1.GroupSummary.Add(item1)

                Dim item2 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "Ajustes_N",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "Ajustes_N: {0:n}",
                .ShowInGroupColumnFooter = GridView1.Columns("Ajustes_N")}
                GridView1.GroupSummary.Add(item2)

                Dim item3 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "Salidas",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "Salidas: {0:n}",
                .ShowInGroupColumnFooter = GridView1.Columns("Salidas")}
                GridView1.GroupSummary.Add(item3)

                Dim item4 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "Diferencia",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "Dif: {0:n}",
                .ShowInGroupColumnFooter = GridView1.Columns("Diferencia")}
                GridView1.GroupSummary.Add(item4)

                Dim item5 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "Existencia_Al",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "Sum: {0:n}",
                .ShowInGroupColumnFooter = GridView1.Columns("Existencia_Al")}
                GridView1.GroupSummary.Add(item5)

                Dim item6 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "Existencia_Actual",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "Sum: {0:n}",
                .ShowInGroupColumnFooter = GridView1.Columns("Existencia_Actual")}
                GridView1.GroupSummary.Add(item6)

                GridView1.Columns("Ingresos").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Ingresos").SummaryItem.DisplayFormat = "{0:n6}"
                GridView1.Columns("Ingresos").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Ingresos").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Inventario_Inicial").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Inventario_Inicial").SummaryItem.DisplayFormat = "{0:n6}"
                GridView1.Columns("Inventario_Inicial").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Inventario_Inicial").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Ajustes_P").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Ajustes_P").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("Ajustes_P").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Ajustes_P").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Ajustes_N").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Ajustes_N").SummaryItem.DisplayFormat = "{0:n6}"
                GridView1.Columns("Ajustes_N").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Ajustes_N").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Salidas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Salidas").SummaryItem.DisplayFormat = "{0:n6}"
                GridView1.Columns("Salidas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Salidas").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Existencia_Al").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Existencia_Al").SummaryItem.DisplayFormat = "{0:n6}"
                GridView1.Columns("Existencia_Al").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Existencia_Al").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Existencia_Actual").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Existencia_Actual").SummaryItem.DisplayFormat = "{0:n6}"
                GridView1.Columns("Existencia_Actual").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Existencia_Actual").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Diferencia").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Diferencia").SummaryItem.DisplayFormat = "{0:n6}"
                GridView1.Columns("Diferencia").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Diferencia").DisplayFormat.FormatString = "{0:n6}"

                GridView1.ExpandAllGroups()

                GridView1.BestFitColumns(True)

                lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

            End If

        Catch ex As Exception
            cTrans.RollBack_Transaction()
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            prg.Visible = False
            lblPrg.Visible = False
            cTrans.Close_Conection()
        End Try

    End Sub

    Private Sub fchDel_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaDesde.ValueChanged

        Try

            If Me.dtpFechaDesde.Value > Me.dtpfechaHasta.Value Or Me.dtpfechaHasta.Value < Me.dtpFechaDesde.Value Then
                Throw New Exception("Seleccione un rango de fechas válido.")
            Else
                Generar_Reporte()
                GridView1.Focus()
            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub fchAl_ValueChanged(sender As Object, e As EventArgs) Handles dtpfechaHasta.ValueChanged

        Try

            If Me.dtpFechaDesde.Value > Me.dtpfechaHasta.Value Or Me.dtpfechaHasta.Value < Me.dtpFechaDesde.Value Then
                Throw New Exception("Seleccione un rango de fechas válido.")
            Else
                Generar_Reporte()
                GridView1.Focus()
            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub GridView1_RowStyle(sender As Object, e As RowStyleEventArgs) Handles GridView1.RowStyle

        Try

            GridView1.OptionsBehavior.Editable = False
            GridView1.OptionsSelection.EnableAppearanceFocusedCell = False

            GridView1.FocusRectStyle = DrawFocusRectStyle.RowFocus

            GridView1.OptionsSelection.EnableAppearanceFocusedRow = True
            GridView1.OptionsSelection.EnableAppearanceHideSelection = True
            GridView1.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            GridView1.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")

            GridView1.Appearance.FocusedRow.ForeColor = Color.White
            GridView1.Appearance.SelectedRow.ForeColor = Color.White

            GridView1.Appearance.SelectedRow.Options.UseBackColor = True
            GridView1.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub frmStockEnUnaFecha_Load(sender As Object, e As EventArgs) Handles Me.Load



        clsUiGridCopyHelper.AttachToForm(Me, "Copiar")
    End Sub

    Private Sub BarButtonItem3_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem3.ItemClick
        Close()
    End Sub

    Private Sub lblProducto_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblProducto.LinkClicked

        Try

            Dim Rec As New frmProductoList() With {
                   .Modo = frmProductoList.pModo.Seleccion,
                   .StartPosition = FormStartPosition.CenterParent,
                   .WindowState = FormWindowState.Maximized}
            Rec.ShowDialog()

            dgrid.DataSource = Nothing

            If Rec.pObjProducto IsNot Nothing AndAlso Rec.pObjProducto.IdProducto <> 0 Then

                txtIdProducto.Text = Rec.pObjProducto.Codigo
                txtNombreProducto.Text = Rec.pObjProducto.Nombre
            End If

            Generar_Reporte()

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub txtIdProducto_Validated(sender As Object, e As EventArgs) Handles txtIdProducto.Validated

        Try

            If String.IsNullOrEmpty(txtIdProducto.Text.Trim()) = False AndAlso txtIdProducto.Text > "0" Then

                ProductoEspecifico = clsLnProducto.Get_Single_By_Codigo(txtIdProducto.Text)

                If ProductoEspecifico IsNot Nothing AndAlso ProductoEspecifico.IdProducto > 0 Then
                    txtNombreProducto.Text = Trim(String.Format("{0}", ProductoEspecifico.Nombre))
                Else
                    XtraMessageBox.Show(String.Format("No existe producto con código {0}", txtIdProducto.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtIdProducto.Focus()
                    txtIdProducto.SelectAll()
                    ProductoEspecifico = Nothing
                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Generar_Reporte()
    End Sub

    Private Sub frmStockEnUnaFecha_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        clsUiGridCopyHelper.AttachToForm(Me, "Copiar")
        AP.Listar_Bodegas_By_Usuario(cmbBodega)
        cmbBodega.EditValue = Integer.Parse(AP.IdBodega)
        IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, cmbBodega.EditValue)

        Try

            SetDatataTable()

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub GridView1_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles GridView1.RowCellStyle

        ' #EJC20260603_ROWSTYLE_PRINT_GUARD: evitar costo de formato por celda durante impresión.
        If clsUiPrintHelper.IsPrintingPreviewInProgress Then Exit Sub

        Try

            Dim View As GridView = sender
            Dim Dif As Object = View.GetRowCellDisplayText(e.RowHandle, View.Columns("Diferencia"))

            If e.Column.FieldName = "Diferencia" Then

                If Val(Dif) = 0 Then
                    e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Regular)
                    e.Appearance.BackColor = Color.Green
                    e.Appearance.BackColor2 = Color.White
                Else
                    e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                    e.Appearance.BackColor = Color.Salmon
                    e.Appearance.BackColor2 = Color.SeaShell
                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub txtIdProducto_TextChanged(sender As Object, e As EventArgs) Handles txtIdProducto.TextChanged
        txtNombreProducto.Text = ""
        ProductoEspecifico = Nothing
    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick

        Try
            Imprimir_Vista()
        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Imprimir_Vista()

        Try
            Dim printingSystem1 As New DevExpress.XtraPrinting.PrintingSystem()
            Dim printLink As New DevExpress.XtraPrinting.PrintableComponentLink()

            AddHandler printLink.CreateMarginalHeaderArea, AddressOf PrintableComponentLink_CreateReportHeaderArea

            Const leftColumnFoot As String = "Páginas: [Page # of Pages #] "
            Dim leftColumnHead As String = "Usuario: [User Name] - " & AP.UsuarioAp.Nombres

            Const rightColumn As String = "Fecha: [Date Printed] [Time Printed] "

            Dim phf As DevExpress.XtraPrinting.PageHeaderFooter =
            TryCast(printLink.PageHeaderFooter, DevExpress.XtraPrinting.PageHeaderFooter)

            phf.Header.Content.Clear()

            phf.Footer.Content.AddRange(New String() _
            {leftColumnFoot})
            phf.Footer.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Near

            phf.Header.Content.AddRange(New String() {leftColumnHead, "", rightColumn})
            phf.Header.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Far

            printLink.Margins = New System.Drawing.Printing.Margins(40, 40, 130, 60)
            printLink.PaperKind = System.Drawing.Printing.PaperKind.Letter
            printingSystem1.PageSettings.Landscape = True
            printLink.Component = dgrid
            printLink.Landscape = True
            Dim colScope As IDisposable = clsUiPrintHelper.BeginRelevantColumnsScope(dgrid, 12)
            Try
                printLink.CreateDocument(printingSystem1)
            Finally
                colScope.Dispose()
            End Try
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As System.Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim vTitulo As String = "STOCK EN UNA FECHA - RESUMEN"
        Dim vBodega As String = If(cmbBodega.Text, AP.NomBodega)
        Dim vPropietario As String = If(cmbPropietario.Text, "")
        Dim vUsuario As String = If(AP.UsuarioAp.Nombres, "")
        Dim vProducto As String = If(String.IsNullOrWhiteSpace(txtIdProducto.Text),
                                     "Todos",
                                     String.Format("{0} - {1}", txtIdProducto.Text.Trim(), txtNombreProducto.Text.Trim()))
        Dim vRango As String = String.Format("{0:dd/MM/yyyy} al {1:dd/MM/yyyy}", dtpFechaDesde.Value.Date, dtpfechaHasta.Value.Date)

        Dim vWidth As Single = e.Graph.ClientPageSize.Width
        Dim vBlue As Color = Color.FromArgb(24, 69, 117)

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Near)
        e.Graph.Font = New Font("Segoe UI Semibold", 16, FontStyle.Bold)
        e.Graph.DrawString(vTitulo,
                           vBlue,
                           New RectangleF(0, 0, vWidth, 34),
                           DevExpress.XtraPrinting.BorderSide.None)

        e.Graph.DrawLine(New PointF(0, 34), New PointF(vWidth, 34), vBlue, 1)

        e.Graph.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        e.Graph.DrawString("Bodega:", Color.Black, New RectangleF(0, 40, 90, 18), DevExpress.XtraPrinting.BorderSide.None)
        e.Graph.DrawString("Propietario:", Color.Black, New RectangleF(0, 58, 90, 18), DevExpress.XtraPrinting.BorderSide.None)
        e.Graph.DrawString("Rango:", Color.Black, New RectangleF(0, 76, 90, 18), DevExpress.XtraPrinting.BorderSide.None)
        e.Graph.DrawString("Producto:", Color.Black, New RectangleF(0, 94, 90, 18), DevExpress.XtraPrinting.BorderSide.None)
        e.Graph.DrawString("Usuario:", Color.Black, New RectangleF(vWidth - 250, 40, 70, 18), DevExpress.XtraPrinting.BorderSide.None)
        e.Graph.DrawString("Fecha impresión:", Color.Black, New RectangleF(vWidth - 250, 58, 110, 18), DevExpress.XtraPrinting.BorderSide.None)

        e.Graph.Font = New Font("Segoe UI", 10, FontStyle.Regular)
        e.Graph.DrawString(vBodega, Color.Black, New RectangleF(95, 40, vWidth - 360, 18), DevExpress.XtraPrinting.BorderSide.None)
        e.Graph.DrawString(vPropietario, Color.Black, New RectangleF(95, 58, vWidth - 360, 18), DevExpress.XtraPrinting.BorderSide.None)
        e.Graph.DrawString(vRango, Color.Black, New RectangleF(95, 76, vWidth - 360, 18), DevExpress.XtraPrinting.BorderSide.None)
        e.Graph.DrawString(vProducto, Color.Black, New RectangleF(95, 94, vWidth - 360, 18), DevExpress.XtraPrinting.BorderSide.None)
        e.Graph.DrawString(vUsuario, Color.Black, New RectangleF(vWidth - 175, 40, 170, 18), DevExpress.XtraPrinting.BorderSide.None)
        e.Graph.DrawString(Format(Now, "dd/MM/yyyy HH:mm"), Color.Black, New RectangleF(vWidth - 135, 58, 130, 18), DevExpress.XtraPrinting.BorderSide.None)

        e.Graph.DrawLine(New PointF(0, 116), New PointF(vWidth, 116), Color.Gainsboro, 1)

    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick

        Try

            'dgrid.ExportToXlsx()

            Dim myStream As Stream
            Dim saveFileDialog1 As New SaveFileDialog()

            saveFileDialog1.Filter = "xlsx files (*.xlsx)|*.xlsx"
            saveFileDialog1.FilterIndex = 1
            saveFileDialog1.RestoreDirectory = True
            saveFileDialog1.FileName = "WMS_Stock_En_Una_Fecha_Del_" & FormatoFechas.tFecha(dtpFechaDesde.Value) & "_Al_" & FormatoFechas.tFecha(dtpfechaHasta.Value) & ".xlsx"

            If saveFileDialog1.ShowDialog() = DialogResult.OK Then
                myStream = saveFileDialog1.OpenFile()
                If (myStream IsNot Nothing) Then
                    ' Code to write the stream goes here.
                    dgrid.ExportToXlsx(myStream)
                    myStream.Close()
                End If
            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub cmbBodega_EditValueChanged(sender As Object, e As EventArgs) Handles cmbBodega.EditValueChanged

        IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, cmbBodega.EditValue)

    End Sub

    Private Sub dgrid_Click(sender As Object, e As EventArgs) Handles dgrid.Click

        'Try

        '    If (GridView1.RowCount > 0) Then

        '        Dim Dr As DataRowView = GridView1.GetFocusedRow
        '        Dim CodigoProducto As String
        '        Dim Lote As String
        '        Dim Diferencia As Double =0

        '        CodigoProducto = Dr.Item("Código")
        '        Lote = Dr.Item("Lote")
        '        Diferencia = Dr.Item("Diferencia")

        '        Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle

        '        With frmListStockSeek
        '            .Modo = frmStockSeek.TipoTrans.Editar
        '            .rpCodigoProducto = CodigoProducto
        '            .rpLote = Lote
        '            .Diferencia = Diferencia
        '            .MdiParent = MdiParent
        '            .WindowState = FormWindowState.Normal
        '            .Show()
        '            .Focus()
        '        End With

        '    End If

        'Catch ex As Exception

        'End Try

    End Sub

    Private Sub dgrid_DoubleClick(sender As Object, e As EventArgs) Handles dgrid.DoubleClick

        Try

            If (GridView1.RowCount > 0) Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow
                Dim CodigoProducto As String
                Dim Lote As String
                Dim Diferencia As Double = 0

                CodigoProducto = Dr.Item("Código")
                Lote = Dr.Item("Lote")
                Diferencia = Dr.Item("Diferencia")

                If XtraMessageBox.Show(String.Format("¿Ir a detalle de existencias?" & vbNewLine & "Código: " & CodigoProducto & vbNewLine & "Lote: " & Lote & vbNewLine & "Diferencia: " & Diferencia),
                                       Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle

                    With frmListStockSeek
                        .Modo = frmStockSeek.TipoTrans.Editar
                        .rpCodigoProducto = CodigoProducto
                        .rpLote = Lote
                        .Diferencia = Diferencia
                        .IdBodega = cmbBodega.EditValue
                        .MdiParent = MdiParent
                        .WindowState = FormWindowState.Normal
                        .Show()
                        .Focus()
                    End With

                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub frmStockEnUnaFecha_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

        If e.Control AndAlso e.KeyCode = Keys.D Then
            ModoDepuracion = True
            MsgBox("Modo depuración activado, tenga cuidado...", MsgBoxStyle.Information, Text)
        End If

    End Sub

    Private Sub prg_EditValueChanged(sender As Object, e As EventArgs) Handles prg.EditValueChanged

    End Sub

    Private Shared Function Armar_Clave_Existencia(ByVal idProductoBodega As Integer,
                                                   ByVal idEstado As Integer,
                                                   ByVal idPresentacion As Integer,
                                                   ByVal idUnidadMedida As Integer,
                                                   ByVal lote As String,
                                                   ByVal fechaVence As Date,
                                                   ByVal conEstado As Boolean) As String
        Dim loteSeguro As String = If(lote, "").Trim()
        Dim estadoSeguro As Integer = If(conEstado, idEstado, 0)
        Return String.Format("{0}|{1}|{2}|{3}|{4}|{5}",
                             idProductoBodega,
                             estadoSeguro,
                             idPresentacion,
                             idUnidadMedida,
                             loteSeguro,
                             fechaVence.Date.ToString("yyyyMMdd"))
    End Function

    Private Shared Function Get_Valor_Existencia(ByVal d As Dictionary(Of String, Double), ByVal k As String) As Double
        If d.ContainsKey(k) Then
            Return d(k)
        End If
        Return 0
    End Function

    Private Sub Cargar_Snapshot_Existencias(ByVal lConnection As SqlConnection,
                                            ByVal lTransaction As SqlTransaction,
                                            ByVal idBodega As Integer,
                                            ByVal idPropietarioBodega As Integer,
                                            ByRef dConEstado As Dictionary(Of String, Double),
                                            ByRef dSinEstado As Dictionary(Of String, Double))

        Dim sql As String = "SELECT IdProductoBodega,
                                    IdProductoEstado,
                                    ISNULL(IdPresentacion,0) AS IdPresentacion,
                                    IdUnidadMedida,
                                    ISNULL(lote,'') AS lote,
                                    CAST(fecha_vence AS DATE) AS fecha_vence,
                                    SUM(Disponible_UMBas) AS Disponible_UMBas
                             FROM VW_Stock_Res
                             WHERE IdBodega = @IdBodega
                               AND IdPropietarioBodega = @IdPropietarioBodega
                             GROUP BY IdProductoBodega,
                                      IdProductoEstado,
                                      ISNULL(IdPresentacion,0),
                                      IdUnidadMedida,
                                      ISNULL(lote,''),
                                      CAST(fecha_vence AS DATE)"

        Using da As New SqlDataAdapter(sql, lConnection)
            da.SelectCommand.Transaction = lTransaction
            da.SelectCommand.CommandType = CommandType.Text
            da.SelectCommand.Parameters.AddWithValue("@IdBodega", idBodega)
            da.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", idPropietarioBodega)

            Dim dt As New DataTable()
            da.Fill(dt)

            dConEstado.Clear()
            dSinEstado.Clear()

            For Each r As DataRow In dt.Rows
                Dim idProdBod As Integer = IIf(IsDBNull(r("IdProductoBodega")), 0, r("IdProductoBodega"))
                Dim idEstado As Integer = IIf(IsDBNull(r("IdProductoEstado")), 0, r("IdProductoEstado"))
                Dim idPresentacion As Integer = IIf(IsDBNull(r("IdPresentacion")), 0, r("IdPresentacion"))
                Dim idUm As Integer = IIf(IsDBNull(r("IdUnidadMedida")), 0, r("IdUnidadMedida"))
                Dim lote As String = IIf(IsDBNull(r("lote")), "", r("lote"))
                Dim fechaVence As Date = IIf(IsDBNull(r("fecha_vence")), New Date(1900, 1, 1), r("fecha_vence"))
                Dim disponible As Double = Math.Max(0, IIf(IsDBNull(r("Disponible_UMBas")), 0, r("Disponible_UMBas")))

                Dim kConEstado As String = Armar_Clave_Existencia(idProdBod, idEstado, idPresentacion, idUm, lote, fechaVence, True)
                Dim kSinEstado As String = Armar_Clave_Existencia(idProdBod, 0, idPresentacion, idUm, lote, fechaVence, False)

                If dConEstado.ContainsKey(kConEstado) Then
                    dConEstado(kConEstado) += disponible
                Else
                    dConEstado.Add(kConEstado, disponible)
                End If

                If dSinEstado.ContainsKey(kSinEstado) Then
                    dSinEstado(kSinEstado) += disponible
                Else
                    dSinEstado.Add(kSinEstado, disponible)
                End If
            Next
        End Using
    End Sub
End Class




