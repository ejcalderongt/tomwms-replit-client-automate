Imports System
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Diagnostics
Imports System.Reflection

Partial Public Class clsLnTrans_despacho_enc

    ''' <summary>
    ''' Creado por Erik Calderón
    ''' </summary>
    ''' <param name="pConnection"></param>
    ''' <param name="pTransaction"></param>
    ''' <returns></returns>
    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdDespachoEnc),0) FROM trans_despacho_enc"

            Using lCommand As New SqlCommand(vSQL, pConnection)

                lCommand.Transaction = pTransaction
                lCommand.CommandType = CommandType.Text

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue) + 1
                End If

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' Creado por Erik Calderón
    ''' </summary>
    ''' <param name="pActivo"></param>
    ''' <param name="pFechaDel"></param>
    ''' <param name="pFechaAl"></param>
    ''' <returns></returns>
    Public Shared Function GetAll(ByVal pActivo As Boolean, ByVal pFechaDel As Date, ByVal pFechaAl As Date) As DataTable

        Dim lTable As New DataTable("Result")

        Try

            Dim vSQL As String = "SELECT * FROM VW_Despacho WHERE 1 > 0  "

            If pActivo = True Then
                vSQL += " AND activo=1"
            Else
                vSQL += " AND activo=0"
            End If

            vSQL += String.Format(" AND cast(Fecha AS DATE) BETWEEN {0} AND {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                    lDataAdapter.SelectCommand.CommandType = CommandType.Text
                    lDataAdapter.Fill(lTable)
                End Using
            End Using

            Return lTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' Creado por Erik Calderón
    ''' </summary>
    ''' <param name="pActivo"></param>
    ''' <param name="pFechaDel"></param>
    ''' <param name="pFechaAl"></param>
    ''' <param name="pIdBodega"></param>
    ''' <returns></returns>
    Public Shared Function GetAllByIdBodega(ByVal pActivo As Boolean, ByVal pFechaDel As Date, ByVal pFechaAl As Date, ByVal pIdBodega As Integer) As DataTable

        Dim lTable As New DataTable("Result")

        Try
            Dim vSQL As String = "SELECT * FROM VW_Despacho WHERE IdBodega = @IdBodega  "

            If pActivo = True Then
                vSQL += " AND activo=1"
            Else
                vSQL += " AND activo=0"
            End If

            vSQL += String.Format(" AND cast(Fecha AS DATE) BETWEEN {0} AND {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                    lDataAdapter.SelectCommand.CommandType = CommandType.Text
                    lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                    lDataAdapter.Fill(lTable)
                End Using
            End Using

            Return lTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pIdDespachoEnc As Integer) As clsBeTrans_despacho_enc

        GetSingle = Nothing

        Try

            Dim vSQL As String = "SELECT enc.*,ISNULL(p.nombres,'') + ' ' + ISNULL(p.apellidos,'') AS Piloto , 
                                    ISNULL(v.marca,'') + ' - ' + ISNULL(v.modelo,'') + ' - ' + ISNULL(v.tipo,'') AS Vehiculo, 
                                    r.NOMBRE AS Ruta 
                                    FROM trans_despacho_enc AS enc 
                                    LEFT JOIN empresa_transporte_pilotos AS p ON enc.IdPiloto = p.IdPiloto 
                                    LEFT JOIN empresa_transporte_vehiculos AS v ON enc.IdVehiculo = v.IdVehiculo 
                                    LEFT JOIN road_ruta AS r ON enc.IdRuta = r.IdRuta 
                                    WHERE IdDespachoEnc=@IdDespachoEnc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdDespachoEnc", pIdDespachoEnc)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim Obj As New clsBeTrans_despacho_enc()

                            Cargar(Obj, lRow)

                            If lRow("IdPiloto") IsNot DBNull.Value AndAlso lRow("IdPiloto") IsNot Nothing Then
                                Obj.IdPiloto = CType(lRow("IdPiloto"), Integer)
                                Obj.NombrePiloto = CType(lRow("Piloto"), String)
                            End If

                            If lRow("IdVehiculo") IsNot DBNull.Value AndAlso lRow("IdVehiculo") IsNot Nothing Then
                                Obj.IdVehiculo = CType(lRow("IdVehiculo"), Integer)
                                Obj.NombreVehiculo = CType(lRow("Vehiculo"), String)
                            End If

                            If lRow("IdRuta") IsNot DBNull.Value AndAlso lRow("IdRuta") IsNot Nothing Then
                                Obj.IdRuta = CType(lRow("IdRuta"), Integer)
                                Obj.NombreRuta = CType(lRow("Ruta"), String)
                            End If

                            Obj.IsNew = False

                            Obj.ListaDetalle = clsLnTrans_despacho_det.Get_All_By_Despacho(Obj.IdDespachoEnc, lConnection, lTransaction)

                            GetSingle = Obj

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pIdDespachoEnc As Integer,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As clsBeTrans_despacho_enc

        GetSingle = Nothing

        Try

            Dim vSQL As String = "SELECT enc.*,ISNULL(p.nombres,'') + ' ' + ISNULL(p.apellidos,'') AS Piloto , 
                    ISNULL(v.marca,'') + ' - ' + ISNULL(v.modelo,'') + ' - ' + ISNULL(v.tipo,'') AS Vehiculo,
                    v.Placa, v.Placa_Comercial,
                    r.NOMBRE AS Ruta 
                    FROM trans_despacho_enc AS enc 
                    LEFT JOIN empresa_transporte_pilotos AS p ON enc.IdPiloto = p.IdPiloto 
                    LEFT JOIN empresa_transporte_vehiculos AS v ON enc.IdVehiculo = v.IdVehiculo 
                    LEFT JOIN road_ruta AS r ON enc.IdRuta = r.IdRuta 
                    WHERE IdDespachoEnc=@IdDespachoEnc"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdDespachoEnc", pIdDespachoEnc)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim Obj As New clsBeTrans_despacho_enc()

                    Cargar(Obj, lRow)

                    If lRow("IdPiloto") IsNot DBNull.Value AndAlso lRow("IdPiloto") IsNot Nothing Then
                        Obj.IdPiloto = CType(lRow("IdPiloto"), Integer)
                        Obj.NombrePiloto = CType(lRow("Piloto"), String)
                    End If

                    If lRow("IdVehiculo") IsNot DBNull.Value AndAlso lRow("IdVehiculo") IsNot Nothing Then
                        Obj.IdVehiculo = CType(lRow("IdVehiculo"), Integer)
                        Obj.NombreVehiculo = CType(lRow("Vehiculo"), String)
                        Obj.Placa = CType(lRow("Placa"), String)
                        Obj.Placa_Comercial = CType(lRow("Placa_Comercial"), String)
                    End If

                    If lRow("IdRuta") IsNot DBNull.Value AndAlso lRow("IdRuta") IsNot Nothing Then
                        Obj.IdRuta = CType(lRow("IdRuta"), Integer)
                        Obj.NombreRuta = CType(lRow("Ruta"), String)
                    End If

                    Obj.IsNew = False

                    Obj.ListaDetalle = clsLnTrans_despacho_det.Get_All_By_Despacho(Obj.IdDespachoEnc, lConnection, lTransaction)

                    Return Obj

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' Creado por Erik Calderón
    ''' </summary>
    ''' <param name="pIdDespachoEnc"></param>
    Public Shared Function Anular_Despacho(ByVal pIdDespachoEnc As Integer) As Boolean

        Anular_Despacho = False

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using ltransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Dim vSQL As String = "UPDATE trans_despacho_enc SET Estado='Anulado' WHERE IdDespachoEnc=@IdDespachoEnc"

                    Using lCommand As New SqlCommand(vSQL, lConnection, ltransaction)

                        lCommand.CommandType = CommandType.Text
                        lCommand.Parameters.AddWithValue("@IdDespachoEnc", pIdDespachoEnc)
                        lConnection.Open()
                        lCommand.ExecuteNonQuery()

                    End Using

                    vSQL = "DELETE FROM trans_movimientos WHERE IdTransaccion=@IdDespachoEnc AND IdTipoTarea = 5"

                    Using lCommand As New SqlCommand(vSQL, lConnection, ltransaction)

                        lCommand.CommandType = CommandType.Text
                        lCommand.Parameters.AddWithValue("@IdDespachoEnc", pIdDespachoEnc)
                        lConnection.Open()
                        lCommand.ExecuteNonQuery()

                    End Using

                    ltransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Anular_Despacho = True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Guardar(ByVal pBeDespachoEnc As clsBeTrans_despacho_enc,
                                   ByVal AllowNegativeExceptionOnStock As Boolean) As Boolean

        Guardar = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            'Validar que si tiene Packing esté cerrado

            Validar_Solicitado_Vrs_Despachado(pBeDespachoEnc,
                                              lConnection,
                                              lTransaction)

            'Despacho Encabezado
            Guarda_Trans_Despacho_Enc(pBeDespachoEnc,
                                      lConnection,
                                      lTransaction)

            'Despacho Detalle
            Guarda_Trans_Despacho_Det(pBeDespachoEnc,
                                      lConnection,
                                      lTransaction)

            'Movimientos
            Guarda_Trans_Despacho_Mov(pBeDespachoEnc,
                                      lConnection,
                                      lTransaction)

            Dim BeInterfaceConfig As New clsBeI_nav_config_enc
            BeInterfaceConfig = clsLnI_nav_config_enc.Get_Single_By_IdBodega_And_IdEmpresa(pBeDespachoEnc.IdBodega,
                                                                                           pBeDespachoEnc.IdEmpresa,
                                                                                           lConnection,
                                                                                           lTransaction)

            Dim OutBePedidoCompraEnc As New clsBeTrans_oc_enc()
            Dim OutBeRecepcionEnc As New clsBeTrans_re_enc()

            '#GT21012025: si tipo doc permite, se genera transferencia hacia otra bodega (aca hace el registro de oc y recepcion)
            Guardar_Despacho_Stock(pBeDespachoEnc,
                                   BeInterfaceConfig,
                                   OutBePedidoCompraEnc,
                                   AllowNegativeExceptionOnStock,
                                   lConnection,
                                   lTransaction)

            'Estado en Pickings asociados
            Verifica_Status_Picking(pBeDespachoEnc,
                                    lConnection,
                                    lTransaction)

            Guarda_Trans_Packing_Enc(pBeDespachoEnc,
                                     lConnection,
                                     lTransaction)

            'Tabla intermedia para interface.
            clsLnI_nav_transacciones_out.Insertar_Salida(pBeDespachoEnc.IdEmpresa,
                                                         pBeDespachoEnc.IdBodega,
                                                         pBeDespachoEnc,
                                                         lConnection,
                                                         lTransaction)

            Dim cantidadDespachada As Double = 0
            Dim CantidadInterface As Double = 0

            cantidadDespachada = Math.Round(clsLnTrans_despacho_det.Get_Cantidad_Despachada(pBeDespachoEnc.IdDespachoEnc, lConnection, lTransaction), 6)
            CantidadInterface = Math.Round(clsLnI_nav_transacciones_out.Get_Cantidad_Despachada(pBeDespachoEnc.IdDespachoEnc, lConnection, lTransaction), 6)

            If cantidadDespachada <> CantidadInterface Then
                clsLnLog_error_wms.Agregar_Error("La cantidad despachada es diferente de la cantidad verificada, no se puede guardar el despacho " & pBeDespachoEnc.IdDespachoEnc)
                Throw New Exception("La cantidad despachada es diferente de la cantidad verificada, no se puede guardar el despacho")
            End If

            lTransaction.Commit()

            Guardar = True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Sub Guarda_Trans_Despacho_Enc(ByRef BeTransDespachoEnc As clsBeTrans_despacho_enc,
                                                ByRef lConnection As SqlConnection,
                                                ByRef lTransaction As SqlTransaction)

        Try

            If BeTransDespachoEnc.IsNew Then
                BeTransDespachoEnc.IdDespachoEnc = MaxID(lConnection, lTransaction)
                Insertar(BeTransDespachoEnc, lConnection, lTransaction)
            Else
                Actualizar(BeTransDespachoEnc, lConnection, lTransaction)
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub


    ''' <summary>
    ''' #EJC20190312: EAT
    ''' Generar pedido de compa a partir de un despacho para una bodega interna del WMS configurada en el sistema.
    ''' </summary>
    ''' <param name="BeDespachoEnc"></param>
    ''' <param name="lConnection"></param>
    ''' <param name="lTransaction"></param>
    Private Shared Function Guardar_Despacho_Stock(ByVal BeDespachoEnc As clsBeTrans_despacho_enc,
                                                   ByVal BeInterfaceConfig As clsBeI_nav_config_enc,
                                                   ByRef OutBePedidoCompraEnc As clsBeTrans_oc_enc,
                                                   ByVal AllowNegativeExceptionOnStock As Boolean,
                                                   ByRef lConnection As SqlConnection,
                                                   ByRef lTransaction As SqlTransaction) As Boolean


        Guardar_Despacho_Stock = False : OutBePedidoCompraEnc = Nothing

        Try

            Dim vContadorDocumentosOC As Integer = 0
            Dim BeTipoDocumentoSalida As New clsBeTrans_pe_tipo()
            Dim vConfigInterGenDocIngresoBodDest As Boolean = False
            Dim vIdPropietario As Integer = 0

            If BeDespachoEnc IsNot Nothing Then

                '#EJC20190719: Se verifica si en la lista de pedidos del despacho hay pedidos para sucursales WMS.
                For Each BePedidoEnc As clsBeTrans_pe_enc In BeDespachoEnc.ListaPedidos

                    BeTipoDocumentoSalida = clsLnTrans_pe_tipo.Get_Single_By_IdTipoPedido(BePedidoEnc.IdTipoPedido,
                                                                                          lConnection,
                                                                                          lTransaction)

                    If (BePedidoEnc.IdTipoPedido = clsDataContractDI.tTipoDocumentoSalida.Transferencia_Interna_WMS AndAlso
                               BePedidoEnc.Cliente.Codigo = BePedidoEnc.Bodega_Destino) Then
                        BeTipoDocumentoSalida.Generar_pedido_ingreso_bodega_destino = True
                        BeTipoDocumentoSalida.Generar_Recepcion_Auto_Bodega_Destino = True
                        BeTipoDocumentoSalida.Recibir_Producto_Auto_Bodega_Destino = True
                    End If

                    '#EJC20190710:
                    'Si existe configuración de interface, y la interface dice que no se genera auto (puede que el ERP genere el ingreso atraves de mi3 en demanda en la bodega destino)
                    If Not BeInterfaceConfig Is Nothing Then
                        vConfigInterGenDocIngresoBodDest = BeInterfaceConfig.Generar_pedido_ingreso_bodega_destino
                    End If

                    '#EJC20210714:REC20200303C1
                    'Por tipo de documento se debe determinar cuáles si y cuáles no generan una tarea de recepción en el proceso de desapacho de la bodega origen.
                    'If vConfigInterGenDocIngresoBodDest OrElse BeTipoDocumentoSalida.Generar_pedido_ingreso_bodega_destino Then

                    'EJC20220428: si cumple las 2 condiciones para ser transfer, aunque tipo doc sea pedido cliente
                    If vConfigInterGenDocIngresoBodDest AndAlso BeTipoDocumentoSalida.Generar_pedido_ingreso_bodega_destino Then

                        Dim lMaxID As Integer = clsLnTrans_oc_enc.MaxID(lConnection, lTransaction) + 1

                        '#EJC20190719: Comprobar si el pedido es para una bodega interna de WMS?
                        '#EJC20190311_0905PM: Se agregó BePedidoEnc.Cliente.Es_bodega_recepcion a la condición
                        If ((BePedidoEnc.Cliente.Es_Bodega_Traslado _
                                OrElse BePedidoEnc.Cliente.Es_bodega_recepcion) _
                                AndAlso BePedidoEnc.Cliente.IdUbicacionVirtual <> 0) Then

                            Dim BePedidoCompraEnc As New clsBeTrans_oc_enc() With {.IsNew = True}
                            BePedidoCompraEnc.IdOrdenCompraEnc = lMaxID
                            BePedidoCompraEnc.PropietarioBodega = New clsBePropietario_bodega

                            vIdPropietario = clsLnPropietarios.Get_IdPropietario(BePedidoEnc.IdBodega,
                                                                                 BePedidoEnc.IdPropietarioBodega,
                                                                                 lConnection,
                                                                                 lTransaction)

                            '#EJC20190312: El IdPropietarioBodega es el que define ralmente en que bodega se registrará el pedido de compra ;)
                            BePedidoCompraEnc.PropietarioBodega.IdPropietarioBodega = clsLnPropietario_bodega.Get_IdPropietarioBodega_By_IdPropietario_And_IdBodega(vIdPropietario,
                                                                                                                                                                    BePedidoEnc.Cliente.IdUbicacionVirtual,
                                                                                                                                                                    lConnection,
                                                                                                                                                                    lTransaction)
                            If BePedidoCompraEnc.PropietarioBodega.IdPropietarioBodega = 0 Then
                                Throw New Exception("#EJC20200130: No se pudo obtener el IdPropietarioBodega para el IdPropietario: " & BePedidoEnc.Cliente.IdPropietario)
                            End If

                            BePedidoCompraEnc.IdPropietarioBodega = BePedidoCompraEnc.PropietarioBodega.IdPropietarioBodega
                            BePedidoCompraEnc.IdBodega = BePedidoEnc.Cliente.IdUbicacionVirtual
                            BePedidoCompraEnc.IdEstadoOC = clsDataContractDI.tEstadoOC.NUEVA
                            BePedidoCompraEnc.Hora_Creacion = Now
                            BePedidoCompraEnc.User_Agr = BeDespachoEnc.User_agr
                            BePedidoCompraEnc.Fec_Agr = Now
                            BePedidoCompraEnc.Fecha_Creacion = Now
                            BePedidoCompraEnc.Activo = True
                            BePedidoCompraEnc.ProveedorBodega = New clsBeProveedor_bodega
                            BePedidoCompraEnc.No_Documento_Recepcion_ERP = clsLnTrans_pe_enc.Get_No_Contenedor_Ingreso(BePedidoEnc, lConnection, lTransaction) 'No documento recepción de origen.                            

                            '#EJC20210901: Se agregó en función (Get_IdProveedorBodega_By_IdBodegaWMS) el IdPropietario como filtro (Generación de documento de ingreso en transferencia). CEALSA.
                            Dim vIdProveedorBodega As Integer = clsLnProveedor.Get_IdProveedorBodega_By_IdBodegaWMS(BeDespachoEnc.IdBodega,
                                                                                                                    vIdPropietario,
                                                                                                                    lConnection,
                                                                                                                    lTransaction)

                            If vIdProveedorBodega <> 0 Then
                                BePedidoCompraEnc.IdProveedorBodega = vIdProveedorBodega
                            Else
                                Throw New Exception("La bodega origen no está configurada como proveedor para la emisión automática de traslado (EAT). Proveedor -> IdUbicacionVirtual")
                            End If

                            Dim vCodigoBodegaOrigen As String = clsLnBodega.Get_Codigo_By_IdBodega(BeDespachoEnc.IdBodega, lConnection, lTransaction)
                            Dim vCodigoBodegaDestino As String = clsLnBodega.Get_Codigo_By_IdBodega(BePedidoEnc.Cliente.IdUbicacionVirtual, lConnection, lTransaction)

                            If vCodigoBodegaOrigen = "" Then
                                Throw New Exception("Error: 20190712_850: El código de la bodega origen no está definido, esto puede ocasionar problemas en la recepción de pallets. ")
                            End If

                            If vCodigoBodegaDestino = "" Then
                                Throw New Exception("Error: 20190712_850: El código de la bodega destino no está definido, esto puede ocasionar problemas en la recepción de pallets. ")
                            End If

                            '#EJC20210901: Parametrización por tipo de documento de salida, indica que tipo de documento de ingreso debe generarse para la bodega destino.
                            'Transferencia interna WMS.
                            BePedidoCompraEnc.IdTipoIngresoOC = IIf(BeTipoDocumentoSalida.IdTipoIngresoOC = 0,
                                                                    clsDataContractDI.tTipoDocumentoIngreso.Transferencia_WMS,
                                                                    BeTipoDocumentoSalida.IdTipoIngresoOC)

                            BePedidoCompraEnc.No_Documento = BeDespachoEnc.IdDespachoEnc
                            BePedidoCompraEnc.IdDespachoEnc = BeDespachoEnc.IdDespachoEnc
                            BePedidoCompraEnc.Serie = ""
                            BePedidoCompraEnc.Correlativo = ""
                            BePedidoCompraEnc.User_Mod = BeDespachoEnc.User_agr
                            BePedidoCompraEnc.Fec_Mod = Now

                            BePedidoCompraEnc.Procedencia = clsLnBodega.Get_Codigo_And_Nombre_By_IdBodegaWMS(BeDespachoEnc.IdEmpresa,
                                                                                                             BeDespachoEnc.IdBodega,
                                                                                                             lConnection,
                                                                                                             lTransaction)
                            BePedidoCompraEnc.No_Marchamo = "N/A"

                            '#EJC20190711: Si el ERP notifica el número de documento de ingreso, se asocia el pedido de compra del WMS con el Ingreso del ERP en la bodega destino.
                            If BePedidoEnc.Referencia_Documento_Ingreso_Bodega_Destino <> "" Then
                                BePedidoCompraEnc.Referencia = BePedidoEnc.Referencia_Documento_Ingreso_Bodega_Destino
                            Else
                                BePedidoCompraEnc.Referencia = "EAT" + Right("00000" + BeDespachoEnc.Numero, 6)
                                If BePedidoEnc.Referencia <> "" Then
                                    BePedidoCompraEnc.Referencia = BePedidoEnc.Referencia
                                End If
                            End If

                            If BeInterfaceConfig.Interface_SAP Then
                                BePedidoCompraEnc.Observacion = "Documento de ingreso, basado en transferencia de WMS IdPedidoEnc: " & BePedidoEnc.IdPedidoEnc & " Documento SAP: " & BePedidoEnc.No_Documento_Externo
                                BePedidoCompraEnc.Codigo_Empresa_ERP = BePedidoEnc.Codigo_Empresa_ERP
                            Else
                                BePedidoCompraEnc.Observacion = "Generado por MI3 de WMS basado en transferencia de WMS IdPedidoEnc: " & BePedidoEnc.IdPedidoEnc
                            End If

                            BePedidoCompraEnc.Control_Poliza = False

                            If BePedidoCompraEnc.ObjPoliza Is Nothing Then
                                BePedidoCompraEnc.ObjPoliza = New clsBeTrans_oc_pol
                                BePedidoCompraEnc.ObjPoliza.IsNew = True
                            End If

                            If BePedidoCompraEnc.ObjPoliza.IsNew Then
                                BePedidoCompraEnc.ObjPoliza.User_agr = BeDespachoEnc.User_agr
                                BePedidoCompraEnc.ObjPoliza.Fec_agr = Now
                            End If

                            ' Embarque
                            BePedidoCompraEnc.ObjPoliza.Bl_No = ""
                            BePedidoCompraEnc.ObjPoliza.Pto_Descarga = ""
                            BePedidoCompraEnc.ObjPoliza.Remitente = ""
                            BePedidoCompraEnc.ObjPoliza.Fecha_abordaje = Now
                            BePedidoCompraEnc.ObjPoliza.Descripcion = ""
                            BePedidoCompraEnc.ObjPoliza.Cantidad = 0
                            BePedidoCompraEnc.ObjPoliza.Total_kgs = 0
                            BePedidoCompraEnc.ObjPoliza.Viaje_no = 0
                            BePedidoCompraEnc.ObjPoliza.Buque_no = ""
                            BePedidoCompraEnc.ObjPoliza.Destino = ""
                            BePedidoCompraEnc.ObjPoliza.Dir_destino = ""
                            BePedidoCompraEnc.ObjPoliza.Po_number = ""
                            BePedidoCompraEnc.ObjPoliza.Piezas = 0
                            BePedidoCompraEnc.ObjPoliza.Cbm = 0

                            ' Poliza 
                            BePedidoCompraEnc.ObjPoliza.NoPoliza = 0
                            BePedidoCompraEnc.ObjPoliza.Pais_procede = ""
                            BePedidoCompraEnc.ObjPoliza.Total_valoraduana = "0"
                            BePedidoCompraEnc.ObjPoliza.Total_bultos_Peso_Bruto = 0
                            BePedidoCompraEnc.ObjPoliza.Total_flete = 0
                            BePedidoCompraEnc.ObjPoliza.Total_usd = 0
                            BePedidoCompraEnc.ObjPoliza.Dua = 0
                            BePedidoCompraEnc.ObjPoliza.Fecha_poliza = Now
                            BePedidoCompraEnc.ObjPoliza.Tipo_cambio = 0
                            BePedidoCompraEnc.ObjPoliza.Total_lineas = 0
                            BePedidoCompraEnc.ObjPoliza.Total_bultos = 0
                            BePedidoCompraEnc.ObjPoliza.Total_seguro = 0
                            BePedidoCompraEnc.ObjPoliza.User_mod = BeDespachoEnc.User_agr
                            BePedidoCompraEnc.ObjPoliza.Fec_mod = Now
                            BePedidoCompraEnc.Enviado_A_ERP = False

                            Dim listaD As New List(Of clsBeTrans_oc_det)
                            Dim listaPR As New List(Of clsBeProducto)
                            Dim BePedidoCompraDet As New clsBeTrans_oc_det
                            Dim BeOcDetLote As New clsBeTrans_oc_det_lote
                            Dim BeProductoPresentacion As New clsBeProducto_Presentacion()

                            clsLnTrans_oc_enc.Insertar(BePedidoCompraEnc, lConnection, lTransaction)

                            lMaxID += 1

                            For Each BePedidoDet As clsBeTrans_pe_det In BePedidoEnc.Detalle

                                Try

                                    BePedidoCompraDet = New clsBeTrans_oc_det
                                    BePedidoCompraDet.IdOrdenCompraEnc = BePedidoCompraEnc.IdOrdenCompraEnc
                                    BePedidoCompraDet.IdOrdenCompraDet = clsLnTrans_oc_det.MaxID(BePedidoCompraEnc.IdOrdenCompraEnc, lConnection, lTransaction) + 1

                                    '#EJC201907012: Buscar el IdProductoBodega de la bodega destino.
                                    'BePedidoDet.ProductoBodega.IdProductoBodega
                                    BePedidoCompraDet.IdProductoBodega = clsLnProducto_bodega.Get_IdProductoBodega_By_IdProducto_And_IdBodega(BePedidoDet.Producto.IdProducto, BePedidoEnc.Cliente.IdUbicacionVirtual)

                                    If BePedidoCompraDet.IdProductoBodega = 0 Then
                                        Throw New Exception("El código de producto: " & BePedidoDet.Codigo_Producto & " no está asociado a la bodega destino: " & vCodigoBodegaDestino)
                                    End If

                                    If BePedidoDet.IdPresentacion <> 0 Then
                                        BePedidoCompraDet.IdPresentacion = BePedidoDet.IdPresentacion
                                        BePedidoCompraDet.Presentacion.IdPresentacion = BePedidoDet.IdPresentacion
                                        BeProductoPresentacion.IdPresentacion = BePedidoDet.IdPresentacion
                                        clsLnProducto_presentacion.GetSingle(BeProductoPresentacion, lConnection, lTransaction)
                                    Else
                                        BePedidoCompraDet.IdPresentacion = 0
                                    End If

                                    BePedidoCompraDet.IdUnidadMedidaBasica = BePedidoDet.IdUnidadMedidaBasica
                                    BePedidoCompraDet.UnidadMedida.IdUnidadMedida = BePedidoDet.IdUnidadMedidaBasica
                                    BePedidoCompraDet.Nombre_producto = BePedidoDet.Nombre_producto
                                    BePedidoCompraDet.Nombre_unidad_medida_basica = BePedidoDet.Nom_unid_med
                                    BePedidoCompraDet.Nombre_presentacion = BePedidoDet.Nom_presentacion
                                    BePedidoCompraDet.Codigo_Producto = BePedidoDet.Codigo_Producto
                                    BePedidoCompraDet.Cantidad = BePedidoDet.Cantidad

                                    '#GT21012025: si la transferencia tambien recibe recepcion auto, la oc debe reflejar cantidad y cantidad_recibida completas.
                                    If BeTipoDocumentoSalida.Recibir_Producto_Auto_Bodega_Destino Then
                                        BePedidoCompraDet.Cantidad_recibida = BePedidoDet.Cantidad
                                    Else
                                        BePedidoCompraDet.Cantidad_recibida = 0
                                    End If

                                    '#GT05112025: si el costo esta vacio, revisar precio
                                    If BePedidoDet.Costo > 0 Then
                                        BePedidoCompraDet.Costo = BePedidoDet.Costo
                                    Else
                                        BePedidoCompraDet.Costo = BePedidoDet.Precio
                                    End If

                                    '#GT05112025: si hay costo, mostrar el total de la linea, si es 0, no afecta
                                    If BePedidoCompraDet.Total_linea = 0 Then
                                        BePedidoCompraDet.Total_linea = BePedidoCompraDet.Costo * BePedidoCompraDet.Cantidad
                                    End If

                                    'BePedidoCompraDet.Total_linea = BePedidoDet.Total_linea

                                    BePedidoCompraDet.No_Linea = BePedidoDet.No_linea
                                    BePedidoCompraDet.Activo = True
                                    BePedidoCompraDet.Porcentaje_arancel = 0
                                    BePedidoCompraDet.User_agr = BePedidoCompraEnc.User_Agr
                                    BePedidoCompraDet.User_mod = BePedidoCompraEnc.User_Agr
                                    BePedidoCompraDet.Atributo_variante_1 = BePedidoDet.Atributo_Variante_1

                                    '#CKFK20251027 Agregué la talla y color
                                    BePedidoCompraDet.Talla.Codigo = BePedidoDet.Talla
                                    BePedidoCompraDet.Color.Codigo = BePedidoDet.Color
                                    BePedidoCompraDet.IdProductoTallaColor = BePedidoDet.IdProductoTallaColor

                                    BePedidoCompraEnc.DetalleOC.Add(BePedidoCompraDet)

                                    clsLnTrans_oc_det.Insertar(BePedidoCompraDet, lConnection, lTransaction)

                                Catch ex As Exception

                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                               BePedidoCompraDet.Nombre_producto,
                                                                               1,
                                                                               3003)

                                    Throw New Exception(ex.Message)

                                End Try

                                '#EJC20210831: BeTipoDocumentoSalida.Trasladar_Lotes_Doc_Ingreso - parámetro que determina si los lotes se trasladan al documento de ingreso o no.
                                If BeTipoDocumentoSalida.Trasladar_Lotes_Doc_Ingreso Then

                                    If Not BePedidoDet.ListaPickingUbic Is Nothing Then

                                        '#EJC20190312: Aquí ya se toma lo que se despacho (cantidad despachada > 0)
                                        Dim lPickingUbicVerificados = BePedidoDet.ListaPickingUbic.Where(Function(x) (x.Cantidad_Verificada > 0 OrElse x.Peso_verificado > 0)).ToList()
                                        'Se verificó para despacho en esa línea del pedido
                                        If Not lPickingUbicVerificados Is Nothing Then

                                            Dim lMaxIdLoteDet As Integer = clsLnTrans_oc_det_lote.MaxID(lConnection, lTransaction) + 1
                                            Dim lMaxIdPallet As Integer = clsLnI_nav_barras_pallet.MaxID(lConnection, lTransaction) + 1
                                            Dim BeTransReDet As New clsBeTrans_re_det
                                            Dim BeINavBarraPallet As New clsBeI_nav_barras_pallet
                                            Dim BeINavBarraPalletOriginal As New clsBeI_nav_barras_pallet
                                            Dim BeStock As New clsBeStock
                                            Dim loteDouble As Double = 0
                                            Dim loteEntero As Double = 0


                                            For Each BePickingUbic As clsBeTrans_picking_ubic In lPickingUbicVerificados

                                                For Each BeDespachoDet As clsBeTrans_despacho_det In BeDespachoEnc.ListaDetalle.Where(Function(x) x.IdPickingUbic = BePickingUbic.IdPickingUbic _
                                                                                                                                      AndAlso x.IdPedidoEnc = BePedidoDet.IdPedidoEnc _
                                                                                                                                      AndAlso x.IdPedidoDet = BePedidoDet.IdPedidoDet)


                                                    BeOcDetLote = New clsBeTrans_oc_det_lote
                                                    BeOcDetLote.IdOrdenCompraDetLote = lMaxIdLoteDet
                                                    BeOcDetLote.IdOrdenCompraEnc = BePedidoCompraEnc.IdOrdenCompraEnc
                                                    BeOcDetLote.IdOrdenCompraDet = BePedidoCompraDet.IdOrdenCompraDet
                                                    BeOcDetLote.Cantidad = BeDespachoDet.CantidadDespachada
                                                    BeOcDetLote.No_linea = BePedidoDet.No_linea
                                                    BeOcDetLote.IdProductoBodega = BePedidoCompraDet.IdProductoBodega
                                                    BeOcDetLote.Lote = BePickingUbic.Lote
                                                    BeOcDetLote.Lic_Plate = BePickingUbic.Lic_plate
                                                    BeOcDetLote.Cantidad_recibida = 0
                                                    BeOcDetLote.Codigo_producto = BePedidoDet.Codigo_Producto
                                                    BeOcDetLote.Fecha_vence = BePickingUbic.Fecha_Vence
                                                    BeOcDetLote.IdPresentacion = BePickingUbic.IdPresentacion
                                                    BeOcDetLote.Presentacion.IdPresentacion = BePickingUbic.IdPresentacion
                                                    BeOcDetLote.IdUnidadMedidaBasica = BePickingUbic.IdUnidadMedida
                                                    BeOcDetLote.UnidadMedida.IdUnidadMedida = BePickingUbic.IdUnidadMedida
                                                    BeOcDetLote.IdProductoTallaColor = BePickingUbic.IdProductoTallaColor
                                                    BeOcDetLote.Talla = BePickingUbic.Codigo_Talla
                                                    BeOcDetLote.Color = BePickingUbic.Codigo_Color
                                                    BeOcDetLote.Activo = True
                                                    BeOcDetLote.User_agr = BeDespachoEnc.User_agr
                                                    BeOcDetLote.User_mod = BeDespachoEnc.User_mod
                                                    BePedidoCompraEnc.DetalleLotes.Add(BeOcDetLote)
                                                    clsLnTrans_oc_det_lote.Insertar(BeOcDetLote, lConnection, lTransaction)

                                                    lMaxIdLoteDet += 1

                                                    If BePickingUbic.Lic_plate <> "" AndAlso BePickingUbic.Lic_plate <> "0" Then

                                                        BeINavBarraPalletOriginal = clsLnI_nav_barras_pallet.Get_Single_Lp_Recibido_By_Codigo_Barra_And_Bodega(BePickingUbic.Lic_plate,
                                                                                                                                                               lConnection,
                                                                                                                                                               lTransaction)

                                                        BeStock = clsLnStock.Get_Single_By_IdStock(BePickingUbic.IdStock,
                                                                                                   lConnection,
                                                                                                   lTransaction)

                                                        If Not BeINavBarraPalletOriginal Is Nothing Then

                                                            'Es una barra existente en la tabla intermedia MI3 se borró. por eso.
                                                            BeINavBarraPallet = New clsBeI_nav_barras_pallet
                                                            BeINavBarraPallet.IdPallet = lMaxIdPallet
                                                            BeINavBarraPallet.Codigo = BePedidoDet.Codigo_Producto
                                                            BeINavBarraPallet.Nombre = BePedidoDet.Nombre_producto
                                                            BeINavBarraPallet.Camas_Por_Tarima = BeINavBarraPalletOriginal.Camas_Por_Tarima
                                                            BeINavBarraPallet.Cajas_Por_Cama = BeINavBarraPalletOriginal.Cajas_Por_Cama
                                                            BeINavBarraPallet.Cantidad_Presentacion = BePickingUbic.Cantidad_Verificada
                                                            BeINavBarraPallet.UM_Producto = IIf(BePickingUbic.IdPresentacion <> 0, BeProductoPresentacion.Codigo_barra, BePedidoDet.Nom_unid_med)
                                                            BeINavBarraPallet.Lote = BePickingUbic.Lote
                                                            BeINavBarraPallet.Fecha_Agregado = Now
                                                            BeINavBarraPallet.Fecha_Ingreso = BeStock.Fecha_Ingreso
                                                            BeINavBarraPallet.Fecha_Vence = BePickingUbic.Fecha_Vence
                                                            BeINavBarraPallet.Fecha_Produccion = BeStock.Fecha_Manufactura
                                                            BeINavBarraPallet.Activo = True
                                                            BeINavBarraPallet.Recibido = 0
                                                            BeINavBarraPallet.IdRecepcion = 0
                                                            BeINavBarraPallet.Bodega_Origen = vCodigoBodegaOrigen
                                                            BeINavBarraPallet.Bodega_Destino = vCodigoBodegaDestino
                                                            BeINavBarraPallet.Codigo_barra = BePickingUbic.Lic_plate
                                                            BeINavBarraPallet.Cantidad_UMP = BePickingUbic.Cantidad_Verificada
                                                            BeINavBarraPallet.Lote_Numerico = BeINavBarraPalletOriginal.Lote_Numerico
                                                            clsLnI_nav_barras_pallet.Insertar(BeINavBarraPallet, lConnection, lTransaction)

                                                            lMaxIdPallet += 1

                                                        Else

                                                            'Es una barra interna generada por WMS.
                                                            Dim BeProductoPallet As New clsBeProducto_pallet
                                                            BeProductoPallet = clsLnProducto_pallet.Get_Single_By_Codigo_Barra(BePickingUbic.Lic_plate, lConnection, lTransaction)

                                                            If Not BeProductoPallet Is Nothing Then

                                                                BeINavBarraPallet = New clsBeI_nav_barras_pallet
                                                                BeINavBarraPallet.IdPallet = lMaxIdPallet
                                                                BeINavBarraPallet.Codigo = BePedidoDet.Codigo_Producto
                                                                BeINavBarraPallet.Nombre = BePedidoDet.Nombre_producto
                                                                BeINavBarraPallet.Camas_Por_Tarima = 1
                                                                BeINavBarraPallet.Cajas_Por_Cama = BeProductoPallet.Cantidad
                                                                BeINavBarraPallet.Cantidad_Presentacion = BePickingUbic.Cantidad_Verificada
                                                                BeINavBarraPallet.UM_Producto = IIf(BePickingUbic.IdPresentacion <> 0, BeProductoPresentacion.Codigo_barra, BePedidoDet.Nom_unid_med)
                                                                BeINavBarraPallet.Lote = BePickingUbic.Lote
                                                                BeINavBarraPallet.Fecha_Agregado = Now
                                                                BeINavBarraPallet.Fecha_Ingreso = BeStock.Fecha_Ingreso
                                                                BeINavBarraPallet.Fecha_Vence = BePickingUbic.Fecha_Vence
                                                                BeINavBarraPallet.Fecha_Produccion = BeStock.Fecha_Manufactura
                                                                BeINavBarraPallet.Activo = 1
                                                                BeINavBarraPallet.Recibido = 0
                                                                BeINavBarraPallet.IdRecepcion = 0
                                                                BeINavBarraPallet.Bodega_Origen = vCodigoBodegaOrigen
                                                                BeINavBarraPallet.Bodega_Destino = vCodigoBodegaDestino
                                                                BeINavBarraPallet.Codigo_barra = BePickingUbic.Lic_plate
                                                                BeINavBarraPallet.Cantidad_UMP = BeProductoPallet.Cantidad
                                                                BeINavBarraPallet.Lote_Numerico = Val(BeProductoPallet.Lote)
                                                                clsLnI_nav_barras_pallet.Insertar(BeINavBarraPallet, lConnection, lTransaction)

                                                            Else

                                                                If Not BeStock Is Nothing Then

                                                                    loteEntero = ExtraerCorrelativoDesdeLote(BePickingUbic.Lote)

                                                                    BeINavBarraPallet = New clsBeI_nav_barras_pallet
                                                                    BeINavBarraPallet.IdPallet = lMaxIdPallet
                                                                    BeINavBarraPallet.Codigo = BePedidoDet.Codigo_Producto
                                                                    BeINavBarraPallet.Nombre = BePedidoDet.Nombre_producto
                                                                    BeINavBarraPallet.Camas_Por_Tarima = 1
                                                                    BeINavBarraPallet.Cajas_Por_Cama = 1
                                                                    BeINavBarraPallet.Cantidad_Presentacion = BePickingUbic.Cantidad_Verificada
                                                                    BeINavBarraPallet.UM_Producto = IIf(BePickingUbic.IdPresentacion <> 0, BeProductoPresentacion.Codigo_barra, BePedidoDet.Nom_unid_med)
                                                                    BeINavBarraPallet.Lote = BePickingUbic.Lote
                                                                    BeINavBarraPallet.Fecha_Agregado = Now
                                                                    BeINavBarraPallet.Fecha_Ingreso = BeStock.Fecha_Ingreso
                                                                    BeINavBarraPallet.Fecha_Vence = BePickingUbic.Fecha_Vence
                                                                    '#EJC20250527: Esto significa que la licencia no existía en i_nav_barras_pallet y tampoco existía en producto_pallet porque se cargó por inventario o ajuste.
                                                                    BeINavBarraPallet.Fecha_Produccion = New Date(1989, 7, 4)
                                                                    BeINavBarraPallet.Activo = True
                                                                    BeINavBarraPallet.Recibido = False
                                                                    BeINavBarraPallet.IdRecepcion = False
                                                                    BeINavBarraPallet.Bodega_Origen = vCodigoBodegaOrigen
                                                                    BeINavBarraPallet.Bodega_Destino = vCodigoBodegaDestino
                                                                    BeINavBarraPallet.Codigo_barra = BePickingUbic.Lic_plate
                                                                    BeINavBarraPallet.Cantidad_UMP = BePickingUbic.Cantidad_Verificada
                                                                    BeINavBarraPallet.Lote_Numerico = loteEntero
                                                                    clsLnI_nav_barras_pallet.Insertar(BeINavBarraPallet, lConnection, lTransaction)

                                                                End If

                                                            End If

                                                        End If

                                                    End If

                                                Next

                                            Next

                                        End If

                                    End If

                                End If

                            Next

                            OutBePedidoCompraEnc = BePedidoCompraEnc.Clone()

                            vContadorDocumentosOC += 1

                            If Not OutBePedidoCompraEnc Is Nothing Then

                                Dim OutBeRecepcionEnc As New clsBeTrans_re_enc

                                '#EJC20220428:Validar por configuración de interface (legacy) o por tipo de documento (new)
                                If BeInterfaceConfig.Generar_Recepcion_Auto_Bodega_Destino OrElse BeTipoDocumentoSalida.Generar_Recepcion_Auto_Bodega_Destino Then


                                    If clsLnTrans_re_enc.Generar_Tarea_Recepcion_By_OrdenCompraEnc_Transfer(OutBePedidoCompraEnc,
                                                                                                           "",
                                                                                                           True,
                                                                                                           BeInterfaceConfig,
                                                                                                           OutBeRecepcionEnc,
                                                                                                           BeTipoDocumentoSalida.Recibir_Producto_Auto_Bodega_Destino,
                                                                                                           lConnection,
                                                                                                           lTransaction) Then

                                        If BeTipoDocumentoSalida.Generar_Recepcion_Auto_Bodega_Destino AndAlso Not BeTipoDocumentoSalida.Recibir_Producto_Auto_Bodega_Destino Then

                                            'Despacha stock para sucursal WMS.                                    
                                            'Stock y Stock_Res - despacho a sucursal (bodega de WMS) (Dejar stock en tránsito en bodega origen)
                                            Guarda_Trans_Despacho_Stock_Transito_Destino(BePedidoEnc,
                                                                                         OutBePedidoCompraEnc.IdOrdenCompraEnc,
                                                                                         OutBeRecepcionEnc.IdRecepcionEnc,
                                                                                         BeDespachoEnc.IdDespachoEnc,
                                                                                         BeDespachoEnc.IdEmpresa,
                                                                                         BeDespachoEnc.IdBodega,
                                                                                         AllowNegativeExceptionOnStock,
                                                                                         lConnection,
                                                                                         lTransaction)

                                        ElseIf BeTipoDocumentoSalida.Recibir_Producto_Auto_Bodega_Destino Then

                                            BePedidoCompraEnc.IdEstadoOC = clsDataContractDI.tEstadoOC.CERRADA
                                            BePedidoCompraEnc.Enviado_A_ERP = True

                                            clsLnTrans_oc_enc.Actualizar_Estado_Cerrada(BePedidoCompraEnc.IdOrdenCompraEnc,
                                                                                        lConnection,
                                                                                        lTransaction)

                                            clsLnTrans_oc_enc.Actualizar_Estado_Enviado_A_ERP(BePedidoCompraEnc.IdOrdenCompraEnc,
                                                                                              True,
                                                                                              lConnection,
                                                                                              lTransaction)

                                            'Stock y Stock_Res - despacho a sucursal (bodega de WMS) (Dejar stock en tránsito en bodega origen)
                                            Guarda_Trans_Despacho_Stock_Recepcion_Auto_En_Destino(BePedidoEnc,
                                                                                                 OutBePedidoCompraEnc,
                                                                                                 OutBeRecepcionEnc,
                                                                                                 BeDespachoEnc,
                                                                                                 BeDespachoEnc.IdEmpresa,
                                                                                                 BeDespachoEnc.IdBodega,
                                                                                                 AllowNegativeExceptionOnStock,
                                                                                                 lConnection,
                                                                                                 lTransaction)

                                        ElseIf Not BeTipoDocumentoSalida.Recibir_Producto_Auto_Bodega_Destino Then

                                            'Despacha stock para sucursal WMS.                                    
                                            'Stock y Stock_Res - despacho a sucursal (bodega de WMS) (Dejar stock en tránsito en bodega origen)
                                            Guarda_Trans_Despacho_Stock_Restar_Origen(BeDespachoEnc,
                                                                                      BeInterfaceConfig,
                                                                                      BePedidoCompraEnc.IdOrdenCompraEnc,
                                                                                      OutBeRecepcionEnc.IdRecepcionEnc,
                                                                                      BePedidoEnc.IdPedidoEnc,
                                                                                      False,
                                                                                      lConnection,
                                                                                      lTransaction)

                                        End If

                                    End If

                                    'EJC20240525: Validar en despacho si se cambia de área en WMS, interface SAP.
                                ElseIf BeInterfaceConfig.Interface_SAP Then

                                    If BeTipoDocumentoSalida.IdTipoPedido = clsDataContractDI.tTipoDocumentoSalida.Transferencia_Interna_WMS Then

                                        Guarda_Trans_Despacho_Stock_AreaWMS_To_SAP(BePedidoEnc,
                                                                                   BeDespachoEnc,
                                                                                   BeInterfaceConfig,
                                                                                   BeDespachoEnc.IdEmpresa,
                                                                                   BeDespachoEnc.IdBodega,
                                                                                   AllowNegativeExceptionOnStock,
                                                                                   lConnection,
                                                                                   lTransaction)

                                    Else

                                        'Despacha stock para sucursal WMS.                                    
                                        'Stock y Stock_Res - despacho a sucursal (bodega de WMS) (Dejar stock en tránsito en bodega origen)
                                        Guarda_Trans_Despacho_Stock_Restar_Origen(BeDespachoEnc,
                                                                                  BeInterfaceConfig,
                                                                                  BePedidoCompraEnc.IdOrdenCompraEnc,
                                                                                  OutBeRecepcionEnc.IdRecepcionEnc,
                                                                                  BePedidoEnc.IdPedidoEnc,
                                                                                  False,
                                                                                  lConnection,
                                                                                  lTransaction)

                                    End If


                                Else


                                    '#EJC20220216: Si es una bodega de WMS, generar la tarea de recepción para la bodega destino
                                    'Pero no insertar el stock, dejar listo para que se reciba manualmente en HH.
                                    If clsLnTrans_re_enc.Generar_Tarea_Recepcion_By_OrdenCompraEnc(OutBePedidoCompraEnc,
                                                                                                   "",
                                                                                                   True,
                                                                                                   BeInterfaceConfig,
                                                                                                   OutBeRecepcionEnc,
                                                                                                   lConnection,
                                                                                                   lTransaction) Then


                                        '#EJC20220216: Si es una bodega de WMS pero se va a recibir manualmente.
                                        'Restar stock...
                                        'GT16022022: se elimina del stock porque se transfiere a otra bodega
                                        Guarda_Trans_Despacho_Stock_Restar_Origen(BeDespachoEnc,
                                                                                 BeInterfaceConfig,
                                                                                 0,
                                                                                 0,
                                                                                 BePedidoEnc.IdPedidoEnc,
                                                                                 AllowNegativeExceptionOnStock,
                                                                                 lConnection,
                                                                                 lTransaction)

                                    End If

                                End If

                            End If

                        Else

                            'GT16022022: se elimina del stock porque se transfiere a otra bodega
                            Guarda_Trans_Despacho_Stock_Restar_Origen(BeDespachoEnc,
                                                                      BeInterfaceConfig,
                                                                      0,
                                                                      0,
                                                                      BePedidoEnc.IdPedidoEnc,
                                                                      AllowNegativeExceptionOnStock,
                                                                      lConnection,
                                                                      lTransaction)

                        End If

                    ElseIf vConfigInterGenDocIngresoBodDest AndAlso BeInterfaceConfig.Interface_SAP Then

                        If BeTipoDocumentoSalida.IdTipoPedido = clsDataContractDI.tTipoDocumentoSalida.Traslado_Por_Estados_SAP Then

                            Guarda_Trans_Despacho_Stock_AreaWMS_To_SAP(BePedidoEnc,
                                                                       BeDespachoEnc,
                                                                       BeInterfaceConfig,
                                                                       BeDespachoEnc.IdEmpresa,
                                                                       BeDespachoEnc.IdBodega,
                                                                       AllowNegativeExceptionOnStock,
                                                                       lConnection,
                                                                       lTransaction)

                        Else

                            Guarda_Trans_Despacho_Stock_Restar_Origen(BeDespachoEnc,
                                                                      BeInterfaceConfig,
                                                                      0,
                                                                      0,
                                                                      BePedidoEnc.IdPedidoEnc,
                                                                      AllowNegativeExceptionOnStock,
                                                                      lConnection,
                                                                      lTransaction)

                        End If

                    Else

                        'GT16022022: se elimina del stock porque se transfiere a otra bodega
                        Guarda_Trans_Despacho_Stock_Restar_Origen(BeDespachoEnc,
                                                                  BeInterfaceConfig,
                                                                  0,
                                                                  0,
                                                                  BePedidoEnc.IdPedidoEnc,
                                                                  AllowNegativeExceptionOnStock,
                                                                  lConnection,
                                                                  lTransaction)


                    End If

                    '#CKFK20220729 Se agregó esta excepción para encontrar el error de stock sin reservar.
                    If BePedidoEnc.IdTipoPedido = clsDataContractDI.tTipoDocumentoSalida.Transferencia_Directa_WMS Then
                        If clsLnStock_res.Tiene_StockRes_By_IdPedidoEnc(BePedidoEnc.IdPedidoEnc, BePedidoEnc.IdBodega, lConnection, lTransaction) Then
                            Throw New Exception(String.Format("El pedido {0} con referencia {1} tiene stock reservado 
                                                               sin liberar, no se puede despachar",
                                                               BePedidoEnc.IdPedidoEnc, BePedidoEnc.Referencia))
                        End If
                    End If

                    '#GT11042023: al despachar, se valida la lista de pedidos para asignar la hora final
                    clsLnTrans_pe_enc.Actualizar_Fecha_Fin_Preparacion(BePedidoEnc.IdPedidoEnc,
                                                                       BeDespachoEnc.IdDespachoEnc,
                                                                       lConnection,
                                                                       lTransaction)

                Next BePedidoEnc

            End If

            Guardar_Despacho_Stock = (vContadorDocumentosOC > 0)

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Shared Sub Verifica_Status_Picking(ByRef BeDespachoEnc As clsBeTrans_despacho_enc,
                                               ByRef lConnection As SqlConnection,
                                               ByRef lTransaction As SqlTransaction)

        Try

            '#EJC20171120:
            'Cacería de brujas para buscar los números de picking
            'Reflejan un error conceptual en la abstracción de las clases o del diseño de la bd.

            Dim NumerosPicking = From P In BeDespachoEnc.ListaPedidos Select New _
                                With {Key P.IdPickingEnc, Key P.IdPedidoEnc} Distinct.ToList()

            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim BePicking As New clsBeTrans_picking_enc

            For Each PickingDePedido In NumerosPicking

                BePicking.IdPickingEnc = PickingDePedido.IdPickingEnc

                If clsLnTrans_picking_enc.GetSingle(BePicking, lConnection, lTransaction) Then

                    If Not clsLnTrans_picking_enc.Tiene_Despachos_Pendientes(PickingDePedido.IdPickingEnc,
                                                                             lConnection,
                                                                             lTransaction) Then

                        '#CKFK20230502 Agregar los datos del usuario y la fecha de modificación
                        BePicking.Estado = "Despachado"
                        BePicking.User_mod = BeDespachoEnc.User_agr
                        BePicking.Fec_mod = Now

                        clsLnTrans_picking_enc.Actualizar_Estado(BePicking, lConnection, lTransaction)

                    Else

                        If BePicking.Estado = "Verificado" Then

                            '#CKFK20230502 Agregar los datos del usuario y la fecha de modificación
                            BePicking.Estado = "Pendiente"
                            BePicking.User_mod = BeDespachoEnc.User_agr
                            BePicking.Fec_mod = Now

                            clsLnTrans_picking_enc.Actualizar_Estado(BePicking, lConnection, lTransaction)

                        End If


                    End If


                End If

            Next

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Shared Function Guarda_Trans_Packing_Enc(ByRef BeDespachoEnc As clsBeTrans_despacho_enc,
                                                    ByRef lConnection As SqlConnection,
                                                    ByRef lTransaction As SqlTransaction) As Integer

        Try

            '#EJC20171120:
            'Casería de brujas para buscar los números de picking
            'Reflejan un error conceptual en la abstracción de las clases o del diseño de la bd.

            Dim NumerosPicking = From P In BeDespachoEnc.ListaPedidos Select New _
                                With {Key P.IdPickingEnc, Key P.IdPedidoEnc} Distinct.ToList()

            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            Dim vTienePacking As Boolean = False
            Dim BePicking As New clsBeTrans_picking_enc
            Dim vRegistrosAfectados As Integer = 0

            For Each PickingDePedido In NumerosPicking

                BePicking.IdPickingEnc = PickingDePedido.IdPickingEnc

                'vTienePacking = clsLnTrans_packing_enc.Tiene_Packing_By_IdPicking(PickingDePedido.IdPickingEnc, lConnection, lTransaction)
                vTienePacking = clsLnTrans_pe_enc.Tiene_Packing(PickingDePedido.IdPedidoEnc)

                If vTienePacking Then
                    vRegistrosAfectados = clsLnTrans_packing_enc.Actualizar_IdDespachoEnc_By_IdPicking(PickingDePedido.IdPickingEnc,
                                                                                                       BeDespachoEnc.IdDespachoEnc,
                                                                                                       PickingDePedido.IdPedidoEnc,
                                                                                                       lConnection, lTransaction)
                End If

            Next

            Guarda_Trans_Packing_Enc = vRegistrosAfectados

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    '#CM_20171213: LinQ para guardar despacho mov.
    Private Shared Sub Guarda_Trans_Despacho_Mov(ByRef BeDespachoEnc As clsBeTrans_despacho_enc,
                                                 ByRef lConnection As SqlConnection,
                                                 ByRef lTransaction As SqlTransaction)

        Try

            If Not BeDespachoEnc Is Nothing Then

                Dim BeMovimiento As New clsBeTrans_movimientos()
                Dim vPedidoCompletado As Boolean = True
                Dim NuevoBePickingUbic As New clsBeTrans_picking_ubic
                Dim vFactor As Double = 0
                Dim vCantidadPicking As Double = 0
                Dim lDespachoDetByPedidoDet As New List(Of clsBeTrans_despacho_det)
                Dim BeDespachoDet As New clsBeTrans_despacho_det
                Dim BeBodega As New clsBeBodega

                BeBodega = clsLnBodega.GetSingle_By_Idbodega(BeDespachoEnc.IdBodega, lConnection, lTransaction)

                Dim vIdMovimiento As Integer = clsLnTrans_movimientos.MaxID(lConnection, lTransaction) + 1

                For Each BePedidoEnc As clsBeTrans_pe_enc In BeDespachoEnc.ListaPedidos

                    For Each BePedidoDet As clsBeTrans_pe_det In BePedidoEnc.Detalle.Where(Function(x) x.Cantidad <> x.Cant_despachada)

                        '#EJC20180607: Valida que el producto haya sido pickeado/verificado.

                        If Not BePedidoDet.ListaPickingUbic Is Nothing Then

                            Dim lPickingUbicVerificados = BePedidoDet.ListaPickingUbic _
                                                                    .Where(Function(x) _
                                                                        ((x.Cantidad_Verificada > 0 OrElse x.Peso_verificado > 0) AndAlso
                                                                            (x.Cantidad_despachada < x.Cantidad_Verificada) AndAlso
                                                                            (x.IdPedidoEnc = BePedidoEnc.IdPedidoEnc))
                                                                    ).ToList()



                            lDespachoDetByPedidoDet = BeDespachoEnc.ListaDetalle.Where(Function(x) x.IdPedidoEnc = BePedidoEnc.IdPedidoEnc AndAlso x.IdPedidoDet = BePedidoDet.IdPedidoDet).ToList()

                            'Se verificó para despacho en esa línea del pedido
                            If Not lPickingUbicVerificados Is Nothing Then

                                For Each BePickingUbic As clsBeTrans_picking_ubic In lPickingUbicVerificados

                                    BeMovimiento.IdMovimiento = vIdMovimiento
                                    BeMovimiento.IdEmpresa = BeDespachoEnc.IdEmpresa
                                    BeMovimiento.IdBodegaOrigen = BeDespachoEnc.IdBodega
                                    BeMovimiento.IdTransaccion = BeDespachoEnc.IdDespachoEnc
                                    BeMovimiento.IdPropietarioBodega = BeDespachoEnc.IdPropietarioBodega
                                    BeMovimiento.IdProductoBodega = BePickingUbic.IdProductoBodega
                                    BeMovimiento.IdUbicacionOrigen = BePickingUbic.IdUbicacion
                                    BeMovimiento.IdEstadoOrigen = BePickingUbic.IdProductoEstado
                                    BeMovimiento.IdEstadoDestino = BePickingUbic.IdProductoEstado

                                    '#EJC20190311_0905PM: Se agregó BePedidoEnc.Cliente.Es_bodega_recepcion a la condición
                                    If ((BePedidoEnc.Cliente.Es_Bodega_Traslado OrElse BePedidoEnc.Cliente.Es_bodega_recepcion) AndAlso BePedidoEnc.Cliente.IdUbicacionVirtual <> 0) Then

                                        Dim IdBodegaWMSDestino As Integer = BePedidoEnc.Cliente.IdUbicacionVirtual

                                        Dim IdUbicacionTransitoBodegaWMS As Integer = clsLnBodega.Get_IdUbicDespacho_By_IdBodega(IdBodegaWMSDestino,
                                                                                                                                 lConnection,
                                                                                                                                 lTransaction)

                                        If IdUbicacionTransitoBodegaWMS <> 0 Then
                                            BeMovimiento.IdUbicacionDestino = IdUbicacionTransitoBodegaWMS
                                        Else
                                            Throw New Exception("La ubicación de tránsito no está definida para la bodega del cliente:" & BePedidoEnc.Cliente.Nombre_comercial)
                                        End If

                                    ElseIf BeBodega.Interface_SAP AndAlso BePedidoEnc.Cliente.Es_Bodega_Traslado Then
                                        '#CKFK20240527 Asignar ubicación de recepción del cliente que es bodega de SAP.
                                        Dim IdUbicacionRecepcionEstadoSAP As Integer = clsLnBodega_area.Get_IdUbicacion_Recepcion_By_Codigo_Area(BePedidoEnc.Cliente.Codigo,
                                                                                                                                                 lConnection,
                                                                                                                                                 lTransaction)
                                        Dim IdEstadoSAP As Integer = clsLnProducto_estado.Get_IdEstado_By_Codigo_Area(BePedidoEnc.Cliente.Codigo,
                                                                                                                       lConnection,
                                                                                                                       lTransaction)

                                        If IdUbicacionRecepcionEstadoSAP <> 0 Then
                                            BeMovimiento.IdUbicacionDestino = IdUbicacionRecepcionEstadoSAP
                                            BeMovimiento.IdEstadoDestino = IdEstadoSAP
                                        Else
                                            Throw New Exception("La ubicación de tránsito no está definida para la bodega del cliente:" & BePedidoEnc.Cliente.Nombre_comercial)
                                        End If

                                    Else

                                        '#EJC202210050914: Mantener la integridad en las ubicaciones por las que transitó el producto.
                                        If BePickingUbic.IdUbicacionTemporal = 0 Then
                                            BeMovimiento.IdUbicacionDestino = BePickingUbic.IdUbicacion
                                        Else
                                            BeMovimiento.IdUbicacionDestino = BePickingUbic.IdUbicacionTemporal
                                        End If

                                    End If

                                    BeMovimiento.IdPresentacion = BePickingUbic.IdPresentacion
                                    BeMovimiento.IdUnidadMedida = BePickingUbic.IdUnidadMedida

                                    If (BePedidoEnc.Cliente.Es_Bodega_Traslado AndAlso BePedidoEnc.Cliente.IdUbicacionVirtual <> 0) Then
                                        BeMovimiento.IdTipoTarea = clsDataContractDI.tTipoTarea.TRAS
                                    ElseIf (BeBodega.Interface_SAP AndAlso BePedidoEnc.Cliente.Es_Bodega_Traslado) Then
                                        BeMovimiento.IdTipoTarea = clsDataContractDI.tTipoTarea.DESP
                                    Else
                                        BeMovimiento.IdTipoTarea = clsDataContractDI.tTipoTarea.DESP
                                    End If

                                    BeMovimiento.IdBodegaDestino = BeDespachoEnc.IdBodega
                                    BeMovimiento.IdRecepcion = BePickingUbic.IdRecepcion

                                    '#CKFK20220719 Agregué esto para obtener la cantidad verificada cuando el proceso es parcial
                                    vCantidadPicking = BePickingUbic.Cantidad_Verificada - BePickingUbic.Cantidad_despachada

                                    '#CKFK_CM_EJC 20190206 Se agregó la conversión a unidad de medida básica cuando el producto tenga presentación
                                    If BeMovimiento.IdPresentacion <> 0 Then
                                        vFactor = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(BeMovimiento.IdProductoBodega,
                                                                                                            BeMovimiento.IdPresentacion,
                                                                                                            lConnection,
                                                                                                            lTransaction)
                                        BeMovimiento.Cantidad = Math.Round(vCantidadPicking * vFactor, 6)
                                    Else
                                        BeMovimiento.Cantidad = BePickingUbic.Cantidad_Verificada
                                    End If

                                    BeMovimiento.Serie = BePickingUbic.Serial
                                    BeMovimiento.Peso = BePickingUbic.Peso_verificado
                                    BeMovimiento.Lote = BePickingUbic.Lote
                                    BeMovimiento.Barra_pallet = BePickingUbic.Lic_plate
                                    BeMovimiento.Fecha_vence = BePickingUbic.Fecha_Vence
                                    BeMovimiento.Fecha_agr = Now
                                    BeMovimiento.Usuario_agr = BeDespachoEnc.User_agr
                                    BeMovimiento.IdPedidoDet = BePedidoDet.IdPedidoDet
                                    BeMovimiento.IdPedidoEnc = BePedidoDet.IdPedidoEnc
                                    BeMovimiento.IdDespachoEnc = BeDespachoEnc.IdDespachoEnc
                                    BeMovimiento.IdDespachoDet = 0
                                    BeMovimiento.IdProductoTallaColor = BePedidoDet.IdProductoTallaColor
                                    BeMovimiento.Talla = BePedidoDet.Talla
                                    BeMovimiento.Color = BePedidoDet.Color

                                    If Not lDespachoDetByPedidoDet Is Nothing Then

                                        If lDespachoDetByPedidoDet.Count > 0 Then

                                            BeDespachoDet = lDespachoDetByPedidoDet.Find(Function(x) x.IdPickingUbic = BePickingUbic.IdPickingUbic)

                                            If Not BeDespachoDet Is Nothing Then
                                                BeMovimiento.IdDespachoDet = BeDespachoDet.IdDespachoDet
                                            End If

                                        End If

                                    End If

                                    If Not clsLnTrans_movimientos.Insertar(BeMovimiento,
                                                                           lConnection,
                                                                           lTransaction) > 0 Then
                                        Throw New Exception("No se pudo insertar el movimiento")
                                    Else

                                        BePickingUbic.Cantidad_Verificada = BePickingUbic.Cantidad_Verificada
                                        BePickingUbic.Peso_verificado = BePickingUbic.Peso_verificado

                                        '#EJC20181004: Si se hizo un despacho parcial, generar una línea de picking pendiente.
                                        If (BePickingUbic.Cantidad_Solicitada <> BePickingUbic.Cantidad_Recibida) AndAlso
                                           (BePickingUbic.Cantidad_Recibida - BePickingUbic.Cantidad_Verificada) <> 0 Then

                                            NuevoBePickingUbic.IdPickingUbic = BePickingUbic.IdPickingUbic
                                            NuevoBePickingUbic.IdPickingEnc = BePickingUbic.IdPickingEnc

                                            clsLnTrans_picking_ubic.GetSingle(NuevoBePickingUbic,
                                                                              lConnection,
                                                                              lTransaction)

                                            '#EJC20260226: Se utilizará identity.
                                            'NuevoBePickingUbic.IdPickingUbic = clsLnTrans_picking_ubic.MaxID(lConnection, lTransaction) + 1
                                            NuevoBePickingUbic.Cantidad_Solicitada = BePickingUbic.Cantidad_Solicitada - BePickingUbic.Cantidad_Verificada
                                            NuevoBePickingUbic.Cantidad_Recibida = BePickingUbic.Cantidad_Recibida - BePickingUbic.Cantidad_Verificada
                                            NuevoBePickingUbic.Cantidad_Verificada = 0

                                            clsLnTrans_picking_ubic.Insertar(NuevoBePickingUbic,
                                                                             lConnection,
                                                                             lTransaction)

                                            BePickingUbic.Cantidad_Solicitada = BePickingUbic.Cantidad_Verificada
                                            BePickingUbic.Cantidad_Recibida = BePickingUbic.Cantidad_Solicitada

                                        End If

                                        If Not clsLnTrans_picking_ubic.Actualizar(BePickingUbic, lConnection, lTransaction) > 0 Then
                                            Throw New Exception("No se pudo actualizar la ubicación de picking: " & BePickingUbic.IdPickingUbic)
                                        End If

                                    End If

                                    vIdMovimiento += 1

                                Next BePickingUbic

                            Else
                                'En la línea de pedido actual, no se verificó producto
                                'Por lo tanto no se puede marcar como despachado el pedido.
                                vPedidoCompletado = False
                            End If 'Fin tiene cantidad verificada

                        End If 'Fin si, el producto no se pickeo, ni se verificó.

                    Next BePedidoDet

                Next BePedidoEnc

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub


    ''' <summary>
    ''' #EJC20220428: Se elimina del stock porque se transfiere a otra bodega
    ''' </summary>
    ''' <param name="BeDespachoEnc"></param>
    ''' <param name="BeInterfaceConfig"></param>
    ''' <param name="pIdOrdenCompraEnc"></param>
    ''' <param name="pIdRecepcionEncDestino"></param>
    ''' <param name="pIdPedidoEnc"></param>
    ''' <param name="AllowNegativeExceptionOnStock"></param>
    ''' <param name="lConnection"></param>
    ''' <param name="lTransaction"></param>
    Private Shared Sub Guarda_Trans_Despacho_Stock_Restar_Origen(ByRef BeDespachoEnc As clsBeTrans_despacho_enc,
                                                                 ByVal BeInterfaceConfig As clsBeI_nav_config_enc,
                                                                 ByVal pIdOrdenCompraEnc As Integer,
                                                                 ByVal pIdRecepcionEncDestino As Integer,
                                                                 ByVal pIdPedidoEnc As Integer,
                                                                 ByVal AllowNegativeExceptionOnStock As Boolean,
                                                                 ByRef lConnection As SqlConnection,
                                                                 ByRef lTransaction As SqlTransaction)

        Dim vCantidadUMBasDespachada As Double = 0
        Dim vPesoDespachado As Double = 0
        Dim vPedidoCompletado As Boolean = False
        Dim vLineaCompletada As Boolean = True
        Dim vCantLineasCompletas As Integer = 0
        Dim vCantidadDespachadaAcum As Double = 0
        Dim vFactor As Integer = 0
        Dim vLiberarStockDespachosParciales As Boolean = False

        Try

            If Not BeDespachoEnc Is Nothing Then

                If Not BeDespachoEnc.ListaPedidos Is Nothing Then

                    If BeDespachoEnc.ListaPedidos.Count > 0 Then

                        vLiberarStockDespachosParciales = clsLnBodega.Get_Liberar_Stock_Despachos_Parciales(BeDespachoEnc.IdBodega, lConnection, lTransaction)

                        For Each BePedidoEnc As clsBeTrans_pe_enc In BeDespachoEnc.ListaPedidos.Where(Function(x) x.IdPedidoEnc = pIdPedidoEnc)

                            If BePedidoEnc.Detalle.Count > 0 Then

                                '#20180822_0452PM_ESA: Iterar solo sobre los pedidos que aún tiene cantidad pendientes de despacho.
                                For Each BePedidoDet As clsBeTrans_pe_det In BePedidoEnc.Detalle

                                    '#GT16092025: aqui la lista del pedido deberia traer solo lineas sin stock liberado, para que haga match con las lineas del despacho
                                    'If BePedidoDet.Stock_Liberado = 0 Then
                                    'End If

                                    '#EJC20180607: Si un producto del pedido, no tiene picking ni verificación, entonces la lista es vacía.
                                    If Not BePedidoDet.ListaPickingUbic Is Nothing Then

                                        '#20180822_0452PM_ESA: Se agregó x.Cantidad_Despachada =0 en lista de picking en Guarda_Trans_Despacho_Stock                            
                                        Dim lPickingUbicVerificados = BePedidoDet.ListaPickingUbic.Where(Function(x) (x.Cantidad_Verificada > 0 OrElse x.Peso_verificado > 0) _
                                                                                                         AndAlso (x.Cantidad_despachada < x.Cantidad_Verificada)).ToList()
                                        'Se verificó para despacho en esa línea del pedido
                                        If Not lPickingUbicVerificados Is Nothing Then

                                            If lPickingUbicVerificados.Count > 0 Then

                                                vLineaCompletada = False

                                                For Each BePickingUbic In lPickingUbicVerificados

                                                    '#EJC20190311_0905PM: Se agregó BePedidoEnc.Cliente.Es_bodega_recepcion a la condición
                                                    If ((BePedidoEnc.Cliente.Es_Bodega_Traslado OrElse
                                                        BePedidoEnc.Cliente.Es_bodega_recepcion) AndAlso
                                                        BePedidoEnc.Cliente.IdUbicacionVirtual <> 0 AndAlso
                                                        BeInterfaceConfig.Generar_Recepcion_Auto_Bodega_Destino) Then

                                                        Dim IdBodegaWMSDestino As Integer = BePedidoEnc.Cliente.IdUbicacionVirtual
                                                        Dim IdUbicacionTransitoBodegaWMS As Integer = clsLnBodega.Get_IdUbicDespacho_By_IdBodega(IdBodegaWMSDestino, lConnection, lTransaction)

                                                        If IdUbicacionTransitoBodegaWMS <> 0 Then

                                                            BePickingUbic.IdProducto = clsLnProducto.Get_Id_Producto_By_IdProductoBodega(BePickingUbic.IdProductoBodega, lConnection, lTransaction)

                                                            clsLnStock.Actualizar_Stock_Por_Traslado(BePickingUbic,
                                                                                                     IdUbicacionTransitoBodegaWMS,
                                                                                                     BeDespachoEnc.IdDespachoEnc,
                                                                                                     BeDespachoEnc.IdBodega,
                                                                                                     IdBodegaWMSDestino,
                                                                                                     BeDespachoEnc.IdEmpresa,
                                                                                                     pIdOrdenCompraEnc,
                                                                                                     pIdRecepcionEncDestino,
                                                                                                     lConnection,
                                                                                                     lTransaction)

                                                        Else
                                                            'If BePickingUbic.IdStockRes = 34 Then
                                                            '    Debug.Write(BePickingUbic.IdStockRes)
                                                            'End If
                                                            Throw New Exception("La ubicación de tránsito no está definida para la bodega del cliente.")
                                                        End If

                                                    Else

                                                        clsLnStock.Actualizar_Stock_Por_Despacho(BeDespachoEnc.IdDespachoEnc,
                                                                                                 BePickingUbic,
                                                                                                 AllowNegativeExceptionOnStock,
                                                                                                 lConnection,
                                                                                                 lTransaction)

                                                    End If



                                                    Actualiza_Stock_Reservado(BePickingUbic, lConnection, lTransaction)

                                                    vFactor = 0

                                                    '#CKFK20221209 Aquí debemos actualizar las cantidades tomando en cuenta el factor del PickingUbic
                                                    If BePickingUbic.IdPresentacion <> 0 AndAlso BePedidoDet.IdPresentacion = 0 Then
                                                        vFactor = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(BePickingUbic.IdProductoBodega,
                                                                                                                            BePickingUbic.IdPresentacion,
                                                                                                                            lConnection,
                                                                                                                            lTransaction)
                                                        If vFactor = 0 Then
                                                            Throw New Exception("No se pudo obtener el factor de la presentacion")
                                                        End If

                                                        If BePickingUbic.Cantidad_Verificada = BePickingUbic.Cantidad_despachada Then
                                                            vCantidadUMBasDespachada += BePickingUbic.Cantidad_despachada * vFactor
                                                            BePickingUbic.Cantidad_despachada += BePickingUbic.Cantidad_Verificada
                                                        ElseIf BePickingUbic.Cantidad_Verificada > BePickingUbic.Cantidad_despachada Then
                                                            vCantidadUMBasDespachada += BePickingUbic.Cantidad_Verificada * vFactor
                                                            '#CKKF20220719 quité esta forma de asignar lo despachado porque era mayor 
                                                            'BePickingUbic.Cantidad_despachada += BePickingUbic.Cantidad_Verificada
                                                            BePickingUbic.Cantidad_despachada = BePickingUbic.Cantidad_Verificada
                                                        End If

                                                    Else
                                                        If BePickingUbic.Cantidad_Verificada = BePickingUbic.Cantidad_despachada Then
                                                            vCantidadUMBasDespachada += BePickingUbic.Cantidad_despachada
                                                            BePickingUbic.Cantidad_despachada += BePickingUbic.Cantidad_Verificada
                                                        ElseIf BePickingUbic.Cantidad_Verificada > BePickingUbic.Cantidad_despachada Then
                                                            vCantidadUMBasDespachada += BePickingUbic.Cantidad_Verificada
                                                            '#CKKF20220719 quité esta forma de asignar lo despachado porque era mayor 
                                                            'BePickingUbic.Cantidad_despachada += BePickingUbic.Cantidad_Verificada
                                                            BePickingUbic.Cantidad_despachada = BePickingUbic.Cantidad_Verificada
                                                        End If

                                                    End If

                                                    '#EJC20210709:Encontrar las condiciones para las que aplica esto....
                                                    'vCantidadUMBasDespachada += BePickingUbic.Cantidad_Verificada - BePickingUbic.Cantidad_despachada
                                                    'BePickingUbic.Cantidad_despachada += BePickingUbic.Cantidad_Verificada - BePickingUbic.Cantidad_despachada

                                                    vPesoDespachado += BePickingUbic.Peso_verificado - BePickingUbic.Peso_despachado
                                                    BePickingUbic.Peso_despachado += BePickingUbic.Peso_verificado - BePickingUbic.Peso_despachado

                                                    '#GT10072025: esta linea es la original para asignar lo despachado, se utiliza la validación #GT10072025_10
                                                    'BePedidoDet.Cant_despachada = vCantidadUMBasDespachada


                                                    If Not clsLnTrans_picking_ubic.Actualizar(BePickingUbic, lConnection, lTransaction) > 0 Then
                                                        Throw New Exception("No se pudo actualizar la ubicación de picking: " & BePickingUbic.IdPickingUbic)
                                                    End If

                                                    '#GT10072025_10 : esto es chambonada para validar que, el lo despachado se agregue al detalle del pedido_det despachado y confirmar que
                                                    'no haya nada pendiente entre pedido_cantidad y pedido_despachado
                                                    If BePedidoDet.IdPedidoDet = BePickingUbic.IdPedidoDet Then
                                                        BePedidoDet.Cant_despachada += vCantidadUMBasDespachada
                                                    End If

                                                Next BePickingUbic

                                                '#CKKF20220719 quité esta forma de asignar lo despachado porque era mayor 
                                                'BePedidoDet.Cant_despachada += vCantidadUMBasDespachada
                                                BePedidoDet.Peso_despachado += vPesoDespachado

                                                vCantidadDespachadaAcum += vCantidadUMBasDespachada

                                                clsLnTrans_pe_det.Actualizar_Cantidad_Y_Peso_Despachado(BePedidoDet, lConnection, lTransaction)

                                                '#CKFK20220722 Agregué esta condición para determinar la diferencia
                                                Dim vDiferencia As Double = (BePedidoDet.Cantidad - BePedidoDet.Cant_despachada)

                                                If vDiferencia < 0.00000000001 Then
                                                    vDiferencia = 0
                                                End If

                                                If Not vLineaCompletada Then vLineaCompletada = (vDiferencia = 0)

                                                ' If Not vLineaCompletada Then vLineaCompletada = (BePedidoDet.Cantidad = BePedidoDet.Cant_despachada)

                                                If vLineaCompletada Then
                                                    vCantLineasCompletas += 1
                                                End If

                                                vCantidadUMBasDespachada = 0 : vPesoDespachado = 0

                                            Else

                                                'Ya se realizó un despacho previo de ese producto por el total?
                                                If BePedidoDet.Cantidad = BePedidoDet.Cant_despachada Then

                                                    vCantidadDespachadaAcum += BePedidoDet.Cant_despachada

                                                    '#CKFK20220722 Agregué esta condición para determinar la diferencia
                                                    Dim vDiferencia As Double = Math.Round((BePedidoDet.Cantidad - BePedidoDet.Cant_despachada), 2)

                                                    If vDiferencia < 0.00000000001 Then
                                                        vDiferencia = 0
                                                    End If

                                                    If Not vLineaCompletada Then vLineaCompletada = (vDiferencia = 0)

                                                    ' If Not vLineaCompletada Then vLineaCompletada = (BePedidoDet.Cantidad = BePedidoDet.Cant_despachada)

                                                    If vLineaCompletada Then
                                                        vCantLineasCompletas += 1
                                                    End If

                                                End If

                                            End If

                                            If vLiberarStockDespachosParciales Then

                                                If Not clsLnTrans_picking_det.Liberar_Producto_No_Pickeado(BePedidoDet.IdPedidoDet,
                                                                                                           BePedidoDet.IdPedidoEnc,
                                                                                                           BePedidoEnc.IdPickingEnc,
                                                                                                           BeDespachoEnc.User_agr,
                                                                                                           BePedidoEnc.Referencia,
                                                                                                           "Guarda_Trans_Despacho_Stock_Restar_Origen",
                                                                                                           BePedidoEnc.IdBodega,
                                                                                                           clsDataContractDI.tOpcionLiberaStock.Despacho,
                                                                                                           lConnection,
                                                                                                           lTransaction) Then


                                                    Throw New Exception("ERROR_202308281357: No se pudo liberar el producto del picking")

                                                End If

                                            End If

                                        Else
                                            'En la línea de pedido actual, no se verificó producto
                                            'Por lo tanto no se puede marcar como despachado el pedido.
                                            vPedidoCompletado = False
                                        End If 'Fin tiene cantidad verificada

                                    End If 'Fin si, el producto se pickeó, se verificó                        

                                Next BePedidoDet

                                vPedidoCompletado = (BePedidoEnc.Detalle.Count = vCantLineasCompletas)

                                If vPedidoCompletado Then
                                    BePedidoEnc.Estado = "Despachado"
                                    clsLnTrans_pe_enc.Actualizar_Estado(BePedidoEnc, lConnection, lTransaction)
                                Else
                                    BePedidoEnc.Estado = "Pendiente"
                                    clsLnTrans_pe_enc.Actualizar_Estado(BePedidoEnc, lConnection, lTransaction)
                                    vPedidoCompletado = True
                                End If

                            Else
                                Throw New Exception("#EJC20200729NODETINPED: El pedidos no tiene detalle.")
                                Throw New Exception("#EJC20200729NOSTCOKAFF C: No se pudo encontrar la información de stock asociado al despacho, esta es una excepción poco usual. Debe realizar un backup de la base de datos y enviarla al departamento de desarrollo.")
                            End If

                        Next BePedidoEnc

                    Else
                        Throw New Exception("#EJC20200729NOREG: El listado de pedidos tiene count 0.")
                    End If

                Else
                    Throw New Exception("#EJC20200729NOPED: No hay pedidos asociados")
                End If

            End If

            If vCantidadDespachadaAcum = 0 Then
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' #EJC20220428: El stock no se elimina, se mueve a ubicación de tránsito.
    ''' </summary>
    ''' <param name="BePedidoEnc"></param>
    ''' <param name="pIdOrdenCompraEnc"></param>
    ''' <param name="pIdRecepcionEnc"></param>
    ''' <param name="pIdDespachoEnc"></param>
    ''' <param name="pIdEmpresa"></param>
    ''' <param name="pIdBodega"></param>
    ''' <param name="AllowNegativeExceptionOnStock"></param>
    ''' <param name="lConnection"></param>
    ''' <param name="lTransaction"></param>
    Private Shared Sub Guarda_Trans_Despacho_Stock_Transito_Destino(ByRef BePedidoEnc As clsBeTrans_pe_enc,
                                                                   ByVal pIdOrdenCompraEnc As Integer,
                                                                   ByVal pIdRecepcionEnc As Integer,
                                                                   ByVal pIdDespachoEnc As Integer,
                                                                   ByVal pIdEmpresa As Integer,
                                                                   ByVal pIdBodega As Integer,
                                                                   ByVal AllowNegativeExceptionOnStock As Boolean,
                                                                   ByRef lConnection As SqlConnection,
                                                                   ByRef lTransaction As SqlTransaction)

        Dim vCantidadUMBasDespachada As Double = 0
        Dim vPesoDespachado As Double = 0
        Dim vPedidoCompletado As Boolean = True
        Dim vCantidadDespachadaAcum As Double = 0

        Try

            If Not BePedidoEnc Is Nothing Then

                '#20180822_0452PM_ESA: Iterar solo sobre los pedidos que aún tiene cantidad pendientes de despacho.
                For Each BePedidoDet As clsBeTrans_pe_det In BePedidoEnc.Detalle

                    '#EJC20180607: Si un producto del pedido, no tiene picking ni verificación, entonces la lista es vacía.
                    If Not BePedidoDet.ListaPickingUbic Is Nothing Then

                        '#20180822_0452PM_ESA: Se agregó x.Cantidad_Despachada =0 en lista de picking en Guarda_Trans_Despacho_Stock                            
                        Dim lPickingUbicVerificados = BePedidoDet.ListaPickingUbic.Where(Function(x) (x.Cantidad_Verificada > 0 OrElse x.Peso_verificado > 0) AndAlso (x.Cantidad_despachada < x.Cantidad_Verificada)).ToList()
                        'Se verificó para despacho en esa línea del pedido
                        If Not lPickingUbicVerificados Is Nothing Then

                            For Each BePickingUbic In lPickingUbicVerificados

                                '#EJC20190311_0905PM: Se agregó BePedidoEnc.Cliente.Es_bodega_recepcion a la condición
                                If ((BePedidoEnc.Cliente.Es_Bodega_Traslado OrElse
                                    BePedidoEnc.Cliente.Es_bodega_recepcion) AndAlso
                                    BePedidoEnc.Cliente.IdUbicacionVirtual <> 0) Then

                                    Dim IdBodegaWMSDestino As Integer = BePedidoEnc.Cliente.IdUbicacionVirtual
                                    Dim IdUbicacionTransitoBodegaWMS As Integer = clsLnBodega.Get_IdUbicDespacho_By_IdBodega(IdBodegaWMSDestino,
                                                                                                                             lConnection,
                                                                                                                             lTransaction)

                                    If IdUbicacionTransitoBodegaWMS <> 0 Then

                                        BePickingUbic.IdProducto = clsLnProducto.Get_Id_Producto_By_IdProductoBodega(BePickingUbic.IdProductoBodega,
                                                                                                                     lConnection,
                                                                                                                     lTransaction)

                                        clsLnStock.Actualizar_Stock_Por_Traslado(BePickingUbic,
                                                                                 IdUbicacionTransitoBodegaWMS,
                                                                                 pIdDespachoEnc,
                                                                                 pIdBodega,
                                                                                 IdBodegaWMSDestino,
                                                                                 pIdEmpresa,
                                                                                 pIdOrdenCompraEnc,
                                                                                 pIdRecepcionEnc,
                                                                                 lConnection,
                                                                                 lTransaction)

                                    Else
                                        Throw New Exception("La ubicación de tránsito no está definida para la bodega del cliente.")
                                    End If

                                Else

                                    clsLnStock.Actualizar_Stock_Por_Despacho(pIdDespachoEnc,
                                                                             BePickingUbic,
                                                                             AllowNegativeExceptionOnStock,
                                                                             lConnection,
                                                                             lTransaction)

                                End If

                                BePickingUbic.Cantidad_despachada = BePickingUbic.Cantidad_Verificada
                                BePickingUbic.Peso_despachado = BePickingUbic.Peso_verificado

                                vCantidadDespachadaAcum += BePickingUbic.Cantidad_Verificada

                                vCantidadUMBasDespachada += BePickingUbic.Cantidad_Verificada
                                vPesoDespachado += BePickingUbic.Peso_verificado

                                Actualiza_Stock_Reservado(BePickingUbic,
                                                          lConnection,
                                                          lTransaction)

                                If Not clsLnTrans_picking_ubic.Actualizar(BePickingUbic, lConnection, lTransaction) > 0 Then
                                    Throw New Exception("No se pudo actualizar la ubicación de picking: " & BePickingUbic.IdPickingUbic)
                                End If

                            Next BePickingUbic

                            BePedidoDet.Cant_despachada += vCantidadUMBasDespachada
                            BePedidoDet.Peso_despachado += vPesoDespachado

                            clsLnTrans_pe_det.Actualizar_Cantidad_Y_Peso_Despachado(BePedidoDet, lConnection, lTransaction)

                            If vPedidoCompletado Then vPedidoCompletado = (BePedidoDet.Cantidad = BePedidoDet.Cant_despachada)

                            vCantidadUMBasDespachada = 0 : vPesoDespachado = 0

                        Else
                            'En la línea de pedido actual, no se verificó producto
                            'Por lo tanto no se puede marcar como despachado el pedido.
                            vPedidoCompletado = False
                        End If 'Fin tiene cantidad verificada

                    End If 'Fin si, el producto se pickeó, se verificó                        

                Next BePedidoDet

                If vPedidoCompletado Then

                    BePedidoEnc.Estado = "Despachado"

                    clsLnTrans_pe_enc.Actualizar_Estado(BePedidoEnc,
                                                        lConnection,
                                                        lTransaction)

                    clsLnTrans_pe_enc.Actualizar_Fecha_Fin_Preparacion(BePedidoEnc.IdPedidoEnc,
                                                                       pIdDespachoEnc,
                                                                       lConnection,
                                                                       lTransaction)

                Else

                    BePedidoEnc.Estado = "Pendiente"

                    clsLnTrans_pe_enc.Actualizar_Estado(BePedidoEnc,
                                                        lConnection,
                                                        lTransaction)

                    clsLnTrans_pe_enc.Actualizar_Fecha_Fin_Preparacion(BePedidoEnc.IdPedidoEnc,
                                                                       pIdDespachoEnc,
                                                                       lConnection,
                                                                       lTransaction)

                    vPedidoCompletado = True

                End If

            End If

            If vCantidadDespachadaAcum = 0 Then
                Throw New Exception("#EJC20200729NOSTCOKAFF: No se pudo encontrar la información de stock asociado al despacho, esta es una excepción poco usual. Debe realizar un backup de la base de datos y enviarla al departamento de desarrollo.")
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Shared Function Actualiza_Stock_Reservado(ByRef BePickingUbic As clsBeTrans_picking_ubic,
                                                      ByRef lConnection As SqlConnection,
                                                      ByRef lTransaction As SqlTransaction) As Boolean

        Try

            '#EJC20181003_0827PM: Si el pickingubic ya fue despachado puede ser 0, se pasa a prd como hot-fix pero se debe validar.
            If BePickingUbic.IdStockRes <> 0 Then

                Dim BeStockRes As New clsBeStock_res() With {.IdStockRes = BePickingUbic.IdStockRes}
                BeStockRes.IdProductoBodega = BePickingUbic.IdProductoBodega

                Dim BePedidoDet As New clsBeTrans_pe_det
                Dim BePresentacion As New clsBeProducto_Presentacion
                Dim vCantidadPresentacion As Double = 0
                Dim vFactor As Integer = 0

                If clsLnStock_res.GetSingle(BeStockRes, lConnection, lTransaction) Then

                    BePedidoDet.IdPedidoDet = BeStockRes.IdPedidoDet
                    clsLnTrans_pe_det.GetSingle(BePedidoDet, lConnection, lTransaction)

                    If Not BePedidoDet Is Nothing Then

                        '#CKFK20221209 Voy a buscar el factor de la presentación si el PickingUbic tiene presentacion
                        If (BePickingUbic.IdPresentacion <> 0) Then
                            vFactor = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(BePickingUbic.IdProductoBodega,
                                                                                                BePickingUbic.IdPresentacion,
                                                                                                lConnection,
                                                                                                lTransaction)
                            If vFactor <> 0 Then
                                vCantidadPresentacion = Math.Round(BePickingUbic.Cantidad_Verificada * vFactor, 6)
                                BeStockRes.Cantidad = Math.Round(BeStockRes.Cantidad - vCantidadPresentacion, 6)
                                If Math.Round(BeStockRes.Cantidad, 6) <= 0 Then
                                    BeStockRes.Cantidad = 0
                                End If
                            Else
                                Throw New Exception("No se encontró el factor de la presentación al actualizar el stock reservado")
                            End If
                        Else

                            'GT:
                            If BePickingUbic.Cantidad_despachada = 0 Then
                                BeStockRes.Cantidad = Math.Round((BeStockRes.Cantidad - BePickingUbic.Cantidad_Verificada), 6)
                            ElseIf (BePickingUbic.Cantidad_despachada > 0) Then
                                BeStockRes.Cantidad = Math.Round(BeStockRes.Cantidad - (BePickingUbic.Cantidad_Verificada - BePickingUbic.Cantidad_despachada), 6)
                            End If

                        End If

                    Else
                        Throw New Exception("No se pudo obtener el detalle del pedido al actualizar el stock reservado")
                    End If

                    If BeStockRes.Cantidad = 0 Then
                        If clsLnStock_res.Eliminar(BeStockRes, lConnection, lTransaction) = 0 Then
                            Throw New Exception("No se pudo eliminar el stock reservado, no se puede concluir la descarga de stock")
                        End If
                    Else
                        If clsLnStock_res.Actualizar(BeStockRes, lConnection, lTransaction) = 0 Then
                            Throw New Exception("No se pudo acutalizar la cantidad en stock reservado, no se puede concluir la descarga de stock")
                        End If
                    End If

                Else
                    'Throw New Exception("No se pudo encontrar el IdStockRes para ser actualizado, no se puede concluir la descarga de stock")
                End If

            Else
                Debug.Print("Encontré IdStockRes 0 para IdPickingUbic: " & BePickingUbic.IdPickingUbic)
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Reporte_Despacho(ByVal IdDespachoEnc As Integer) As DataTable

        Dim lTable As New DataTable("Result")

        Try

            Dim vSQL As String = "SELECT * FROM VW_Despacho_rep_res WHERE IdDespachoEnc = @IdDespachoEnc and CantidadDespachada >0  "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdDespachoEnc", IdDespachoEnc)
                        lDataAdapter.SelectCommand.CommandTimeout = 100
                        lDataAdapter.Fill(lTable)
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Reporte_Despacho_DyD(ByVal IdDespachoEnc As Integer) As DataTable

        Dim lTable As New DataTable("Result")

        Try

            Dim vSQL As String = "SELECT * FROM VW_Despacho_Rep_DyD WHERE IdDespachoEnc = @IdDespachoEnc and CantidadDespachada >0  "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdDespachoEnc", IdDespachoEnc)
                        lDataAdapter.SelectCommand.CommandTimeout = 100
                        lDataAdapter.Fill(lTable)
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Reporte_Packing(ByVal IdDespachoEnc As Integer) As DataTable

        Dim lTable As New DataTable("Result")

        Try

            Dim vSQL As String = "SELECT * FROM VW_PACKING WHERE IdDespachoEnc = @IdDespachoEnc "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdDespachoEnc", IdDespachoEnc)
                        lDataAdapter.SelectCommand.CommandTimeout = 100
                        lDataAdapter.Fill(lTable)
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdPedidoEnc(ByVal IdPedidoEnc As Integer) As clsBeTrans_despacho_enc

        Get_Single_By_IdPedidoEnc = Nothing

        Try

            Dim vSQL As String = "SELECT trans_despacho_enc.IdDespachoEnc, trans_despacho_enc.IdBodega, trans_despacho_enc.IdPropietarioBodega, trans_despacho_enc.IdVehiculo, 
                         trans_despacho_enc.IdPiloto, trans_despacho_enc.IdRuta, trans_despacho_enc.fecha, trans_despacho_enc.no_pase, 
                         trans_despacho_enc.observacion, trans_despacho_enc.hora_ini, trans_despacho_enc.hora_fin, trans_despacho_enc.estado, 
                         trans_despacho_enc.numero, trans_despacho_enc.marchamo, trans_despacho_enc.cant_bultos, trans_despacho_enc.user_agr, 
                         trans_despacho_enc.fec_agr, trans_despacho_enc.user_mod, trans_despacho_enc.fec_mod, trans_despacho_enc.activo
                    FROM trans_pe_det INNER JOIN
                         trans_pe_enc ON trans_pe_det.IdPedidoEnc = trans_pe_enc.IdPedidoEnc INNER JOIN
                         trans_despacho_det ON trans_pe_det.IdPedidoDet = trans_despacho_det.IdPedidoDet INNER JOIN
                         trans_despacho_enc ON trans_despacho_det.IdDespachoEnc = trans_despacho_enc.IdDespachoEnc
                  WHERE trans_pe_enc.IdPedidoEnc=@IdPedidoEnc
                GROUP BY trans_despacho_enc.IdDespachoEnc, trans_despacho_enc.IdBodega, trans_despacho_enc.IdPropietarioBodega, trans_despacho_enc.IdVehiculo, 
                         trans_despacho_enc.IdPiloto, trans_despacho_enc.IdRuta, trans_despacho_enc.fecha, trans_despacho_enc.no_pase, 
                         trans_despacho_enc.observacion, trans_despacho_enc.hora_ini, trans_despacho_enc.hora_fin, trans_despacho_enc.estado, 
                         trans_despacho_enc.numero, trans_despacho_enc.marchamo, trans_despacho_enc.cant_bultos, trans_despacho_enc.user_agr, 
                         trans_despacho_enc.fec_agr, trans_despacho_enc.user_mod, trans_despacho_enc.fec_mod, trans_despacho_enc.activo order by trans_despacho_enc.fec_agr desc "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", IdPedidoEnc)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim Obj As New clsBeTrans_despacho_enc()

                            Cargar(Obj, lRow)

                            Get_Single_By_IdPedidoEnc = Obj

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function Get_Single_By_IdPedidoEnc(ByVal IdPedidoEnc As Integer, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As clsBeTrans_despacho_enc

        Get_Single_By_IdPedidoEnc = Nothing

        Try

            Dim vSQL As String = "SELECT trans_despacho_enc.IdDespachoEnc, trans_despacho_enc.IdBodega, trans_despacho_enc.IdPropietarioBodega, trans_despacho_enc.IdVehiculo, 
                         trans_despacho_enc.IdPiloto, trans_despacho_enc.IdRuta, trans_despacho_enc.fecha, trans_despacho_enc.no_pase, 
                         trans_despacho_enc.observacion, trans_despacho_enc.hora_ini, trans_despacho_enc.hora_fin, trans_despacho_enc.estado, 
                         trans_despacho_enc.numero, trans_despacho_enc.marchamo, trans_despacho_enc.cant_bultos, trans_despacho_enc.user_agr, 
                         trans_despacho_enc.fec_agr, trans_despacho_enc.user_mod, trans_despacho_enc.fec_mod, trans_despacho_enc.activo,
                         trans_despacho_enc.no_documento_externo 
                    FROM trans_pe_det INNER JOIN
                         trans_pe_enc ON trans_pe_det.IdPedidoEnc = trans_pe_enc.IdPedidoEnc INNER JOIN
                         trans_despacho_det ON trans_pe_det.IdPedidoDet = trans_despacho_det.IdPedidoDet INNER JOIN
                         trans_despacho_enc ON trans_despacho_det.IdDespachoEnc = trans_despacho_enc.IdDespachoEnc
                  WHERE trans_pe_enc.IdPedidoEnc=@IdPedidoEnc
                GROUP BY trans_despacho_enc.IdDespachoEnc, trans_despacho_enc.IdBodega, trans_despacho_enc.IdPropietarioBodega, trans_despacho_enc.IdVehiculo, 
                         trans_despacho_enc.IdPiloto, trans_despacho_enc.IdRuta, trans_despacho_enc.fecha, trans_despacho_enc.no_pase, 
                         trans_despacho_enc.observacion, trans_despacho_enc.hora_ini, trans_despacho_enc.hora_fin, trans_despacho_enc.estado, 
                         trans_despacho_enc.numero, trans_despacho_enc.marchamo, trans_despacho_enc.cant_bultos, trans_despacho_enc.user_agr, 
                         trans_despacho_enc.fec_agr, trans_despacho_enc.user_mod, trans_despacho_enc.fec_mod, trans_despacho_enc.activo, trans_despacho_enc.no_documento_externo "
            vSQL += "ORDER BY trans_despacho_enc.fecha desc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", IdPedidoEnc)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim BeTransDespachoEnc As New clsBeTrans_despacho_enc()
                    Cargar(BeTransDespachoEnc, lRow)
                    Return BeTransDespachoEnc

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Shared Sub Guarda_Trans_Despacho_Stock_Recepcion_Auto_En_Destino(ByRef BePedidoEnc As clsBeTrans_pe_enc,
                                                                             ByVal BeOrdenCompraEnc As clsBeTrans_oc_enc,
                                                                             ByVal BeRecepcionEnc As clsBeTrans_re_enc,
                                                                             ByVal BeDespachoEnc As clsBeTrans_despacho_enc,
                                                                             ByVal pIdEmpresa As Integer,
                                                                             ByVal pIdBodega As Integer,
                                                                             ByVal AllowNegativeExceptionOnStock As Boolean,
                                                                             ByRef lConnection As SqlConnection,
                                                                             ByRef lTransaction As SqlTransaction)

        Dim vCantidadUMBasDespachada As Double = 0
        Dim vPesoDespachado As Double = 0
        Dim vPedidoCompletado As Boolean = True
        Dim vCantidadDespachadaAcum As Double = 0
        Dim BeTransReDet As New clsBeTrans_re_det
        Dim vIdPropietario As Integer = 0
        Dim BeProductoEstado As New clsBeProducto_estado
        Dim lLicenciasProcesadas As New List(Of String)
        Dim vLicencia As String = ""
        Dim lBeTransRecDet As New List(Of clsBeTrans_re_det)
        Dim BeUnidadMedida As New clsBeUnidad_medida
        Dim BePresentacion As New clsBeProducto_Presentacion
        Dim vNoLineaTeorico As Integer = 0
        Dim BeTransOCDet As New clsBeTrans_oc_det
        Dim vIdProductoBodegaEquivalenteBodegaDestino As Integer = 0

        Try

            If Not BePedidoEnc Is Nothing Then

                '#20180822_0452PM_ESA: Iterar solo sobre los pedidos que aún tiene cantidad pendientes de despacho.
                For Each BePedidoDet As clsBeTrans_pe_det In BePedidoEnc.Detalle

                    '#EJC20180607: Si un producto del pedido, no tiene picking ni verificación, entonces la lista es vacía.
                    If Not BePedidoDet.ListaPickingUbic Is Nothing Then

                        '#20180822_0452PM_ESA: Se agregó x.Cantidad_Despachada =0 en lista de picking en Guarda_Trans_Despacho_Stock                            
                        Dim lPickingUbicVerificados = BePedidoDet.ListaPickingUbic.Where(Function(x) (x.Cantidad_Verificada > 0 OrElse x.Peso_verificado > 0) AndAlso (x.Cantidad_despachada < x.Cantidad_Verificada)).ToList()
                        'Se verificó para despacho en esa línea del pedido
                        If Not lPickingUbicVerificados Is Nothing Then

                            For Each BePickingUbic In lPickingUbicVerificados

                                '#EJC20190311_0905PM: Se agregó BePedidoEnc.Cliente.Es_bodega_recepcion a la condición
                                If ((BePedidoEnc.Cliente.Es_Bodega_Traslado OrElse
                                    BePedidoEnc.Cliente.Es_bodega_recepcion) AndAlso
                                    BePedidoEnc.Cliente.IdUbicacionVirtual <> 0) Then

                                    Dim IdBodegaWMSDestino As Integer = BePedidoEnc.Cliente.IdUbicacionVirtual
                                    Dim IdUbicacionRecBodDest As Integer = clsLnBodega.Get_IdUbicacion_Recepcion_By_IdBodega(IdBodegaWMSDestino,
                                                                                                                             lConnection,
                                                                                                                             lTransaction)

                                    If Not BePedidoEnc.TipoPedido Is Nothing Then
                                        If BePedidoEnc.TipoPedido.Transferir_Ubicacion Then
                                            IdUbicacionRecBodDest = BePickingUbic.IdUbicacion
                                            If Not clsLnBodega_ubicacion.Existe_By_IdUbicacion_And_IdBodega(IdUbicacionRecBodDest, BePedidoEnc.Cliente.IdUbicacionVirtual, lConnection, lTransaction) Then
                                                Throw New Exception("La configuración del documento tiene activada la transferencia de ubicaciones, pero la ubicación:  " & IdUbicacionRecBodDest & " No existe para la bodega destino: " & BePedidoEnc.Cliente.IdUbicacionVirtual)
                                            End If
                                        End If
                                    End If

                                    If IdUbicacionRecBodDest <> 0 Then

                                        BePickingUbic.IdProducto = clsLnProducto.Get_Id_Producto_By_IdProductoBodega(BePickingUbic.IdProductoBodega,
                                                                                                                     lConnection,
                                                                                                                     lTransaction)


                                        vIdPropietario = clsLnPropietario_bodega.Get_IdPropietario_By_IdBodega_IdPropietarioBodega(BeOrdenCompraEnc.IdBodega,
                                                                                                                                   BeOrdenCompraEnc.IdPropietarioBodega,
                                                                                                                                   lConnection,
                                                                                                                                   lTransaction)

                                        BeProductoEstado = clsLnProducto_estado.Get_Buen_Estado_Porducto_By_IdPropietario_And_IdBodegaHH(vIdPropietario,
                                                                                                                                         BeOrdenCompraEnc.IdBodega,
                                                                                                                                         lConnection,
                                                                                                                                         lTransaction)


                                        If String.IsNullOrEmpty(BeProductoEstado.Nombre) Then
                                            Dim vMensajeLog As String = "Advertencia_20250128_Transferencia_WMS: Error desconocido, no se obtuvo un Estado para el producto " & BePickingUbic.IdProducto & " con propietario " & vIdPropietario & " bodega " & BeOrdenCompraEnc.IdBodega & " propietario_bodega " & BeOrdenCompraEnc.IdPropietarioBodega
                                            clsLnLog_error_wms.Agregar_Error(vMensajeLog)
                                        End If


                                        BeUnidadMedida = clsLnUnidad_medida.GetSingle(BePickingUbic.IdUnidadMedida, lConnection, lTransaction)

                                        If String.IsNullOrEmpty(BeUnidadMedida.Nombre) Then
                                            Dim vMensajeLog As String = "Advertencia_20250128_Transferencia_WMS: Error desconocido, no se obtuvo la umbas para el producto " & BePickingUbic.IdProducto & " con Picking_Umbas " & BePickingUbic.IdUnidadMedida
                                            clsLnLog_error_wms.Agregar_Error(vMensajeLog)
                                        End If


                                        If BePickingUbic.IdPresentacion <> 0 Then
                                            BePresentacion = clsLnProducto_presentacion.GetSingle(BePickingUbic.IdPresentacion, lConnection, lTransaction)
                                        End If

                                        vIdProductoBodegaEquivalenteBodegaDestino = clsLnProducto_bodega.Get_IdProductoBodega_By_IdProducto_And_IdBodega(BePickingUbic.IdProducto,
                                                                                                                                                         BeOrdenCompraEnc.IdBodega,
                                                                                                                                                         lConnection,
                                                                                                                                                         lTransaction)

                                        BeTransOCDet = BeOrdenCompraEnc.DetalleOC.Find(Function(x) x.IdProductoBodega = vIdProductoBodegaEquivalenteBodegaDestino _
                                                                                       AndAlso x.IdUnidadMedidaBasica = BePickingUbic.IdUnidadMedida _
                                                                                       AndAlso x.IdPresentacion = BePickingUbic.IdPresentacion)

                                        If Not BeTransOCDet Is Nothing Then


                                            BeTransReDet = New clsBeTrans_re_det()
                                            BeTransReDet.IsNew = True
                                            BeTransReDet.IdRecepcionEnc = BeRecepcionEnc.IdRecepcionEnc
                                            BeTransReDet.IdRecepcionDet = 0 'EJC20260226: En recepción automática en destino, el detalle de recepción se va creando a medida que se van procesando las líneas de despacho, por lo tanto no se tiene un IdRecepcionDet definido al momento de crear el objeto.
                                            BeTransReDet.IdPropietarioBodega = BeOrdenCompraEnc.IdPropietarioBodega
                                            BeTransReDet.IdOrdenCompraEnc = BeOrdenCompraEnc.IdOrdenCompraEnc
                                            BeTransReDet.IdOrdenCompraDet = BeTransOCDet.IdOrdenCompraDet 'no lo se aun.
                                            BeTransReDet.Codigo_Producto = BePickingUbic.CodigoProducto
                                            BeTransReDet.Producto.IdProducto = BePickingUbic.IdProducto
                                            BeTransReDet.Producto.Codigo = BePickingUbic.CodigoProducto
                                            BeTransReDet.IdProductoBodega = BeTransOCDet.IdProductoBodega
                                            BeTransReDet.IdPresentacion = BePickingUbic.IdPresentacion
                                            BeTransReDet.Presentacion.IdPresentacion = BePickingUbic.IdPresentacion
                                            BeTransReDet.UnidadMedida = New clsBeUnidad_medida
                                            BeTransReDet.UnidadMedida.IdUnidadMedida = BePickingUbic.IdUnidadMedida
                                            BeTransReDet.IdUnidadMedida = BePickingUbic.IdUnidadMedida
                                            BeTransReDet.Lic_plate = BePickingUbic.Lic_plate
                                            BeTransReDet.Lote = BePickingUbic.Lote
                                            BeTransReDet.Fecha_vence = BePickingUbic.Fecha_Vence
                                            BeTransReDet.ProductoEstado = New clsBeProducto_estado
                                            BeTransReDet.ProductoEstado.IdEstado = BePickingUbic.IdProductoEstado
                                            BeTransReDet.IdProductoEstado = BePickingUbic.IdProductoEstado
                                            BeTransReDet.IdOperadorBodega = BeOrdenCompraEnc.IdOperadorBodegaDefecto
                                            BeTransReDet.MotivoDevolucion = New clsBeMotivo_devolucion
                                            BeTransReDet.No_Linea = BeTransOCDet.No_Linea  'OcDet.No_Linea no lo sé aun.
                                            BeTransReDet.cantidad_recibida = BePickingUbic.Cantidad_Verificada
                                            BeTransReDet.Nombre_producto = BePickingUbic.NombreProducto

                                            If BePickingUbic.IdPresentacion <> 0 Then
                                                BeTransReDet.Nombre_presentacion = BePresentacion.Nombre
                                            End If

                                            BeTransReDet.Nombre_unidad_medida = BeUnidadMedida.Nombre
                                            BeTransReDet.Nombre_producto_estado = BeProductoEstado.Nombre
                                            BeTransReDet.Fecha_ingreso = Now
                                            BeTransReDet.Peso = BePickingUbic.Peso_verificado
                                            BeTransReDet.Peso_Estadistico = 0
                                            BeTransReDet.Peso_Minimo = 0
                                            BeTransReDet.Peso_Maximo = 0
                                            BeTransReDet.Aniada = BeDespachoEnc.IdDespachoEnc '#EJC20220504: A tricki clue for mapping, Hope to not fuck nothing else.
                                            BeTransReDet.Costo_Oc = 0
                                            BeTransReDet.User_agr = BeDespachoEnc.User_agr
                                            BeTransReDet.Fec_agr = Now
                                            BeTransReDet.Observacion = "Transferencia_Interna_WMS_20220504"
                                            BeTransReDet.Atributo_Variante_1 = ""
                                            BeTransReDet.Pallet_No_Estandar = 0

                                            '#CKFK20251027 Agregué talla y color
                                            BeTransReDet.IdProductoTallaColor = BePickingUbic.IdProductoTallaColor
                                            BeTransReDet.Talla.Codigo = BePickingUbic.Codigo_Talla
                                            BeTransReDet.Color.Codigo = BePickingUbic.Codigo_Color

                                            clsLnTrans_re_det.Insertar(BeTransReDet, lConnection, lTransaction)

                                            clsLnStock.Actualizar_Stock_Por_Traslado_Con_Recepcion_En_Destino(BePickingUbic,
                                                                                                              IdUbicacionRecBodDest,
                                                                                                              BeDespachoEnc,
                                                                                                              pIdBodega,
                                                                                                              IdBodegaWMSDestino,
                                                                                                              pIdEmpresa,
                                                                                                              BeOrdenCompraEnc,
                                                                                                              BeRecepcionEnc.IdRecepcionEnc,
                                                                                                              BeTransReDet,
                                                                                                              lConnection,
                                                                                                              lTransaction)


                                        Else
                                            Throw New Exception("#ERR20220504: No se logró obtener el detalle del documento de ingreso para la bodega destino.")
                                        End If

                                    Else
                                        Throw New Exception("#ERR20220504_2345: La ubicación de recepción no está definida para la bodega destino.")
                                    End If

                                Else

                                    clsLnStock.Actualizar_Stock_Por_Despacho(BeDespachoEnc.IdDespachoEnc,
                                                                             BePickingUbic,
                                                                             AllowNegativeExceptionOnStock,
                                                                             lConnection,
                                                                             lTransaction)

                                End If

                                Actualiza_Stock_Reservado(BePickingUbic,
                                                          lConnection,
                                                          lTransaction)

                                BePickingUbic.Cantidad_despachada = BePickingUbic.Cantidad_Verificada
                                BePickingUbic.Peso_despachado = BePickingUbic.Peso_verificado

                                vCantidadDespachadaAcum += BePickingUbic.Cantidad_Verificada

                                vCantidadUMBasDespachada += BePickingUbic.Cantidad_Verificada
                                vPesoDespachado += BePickingUbic.Peso_verificado

                                If Not clsLnTrans_picking_ubic.Actualizar(BePickingUbic, lConnection, lTransaction) > 0 Then
                                    Throw New Exception("No se pudo actualizar la ubicación de picking: " & BePickingUbic.IdPickingUbic)
                                End If

                            Next BePickingUbic

                            '#CKFK20220725 Cambié el += por el =
                            BePedidoDet.Cant_despachada = vCantidadUMBasDespachada
                            BePedidoDet.Peso_despachado = vPesoDespachado

                            clsLnTrans_pe_det.Actualizar_Cantidad_Y_Peso_Despachado(BePedidoDet, lConnection, lTransaction)

                            If vPedidoCompletado Then vPedidoCompletado = (BePedidoDet.Cantidad = BePedidoDet.Cant_despachada)

                            vCantidadUMBasDespachada = 0 : vPesoDespachado = 0

                        Else
                            'En la línea de pedido actual, no se verificó producto
                            'Por lo tanto no se puede marcar como despachado el pedido.
                            vPedidoCompletado = False
                        End If 'Fin tiene cantidad verificada

                    End If 'Fin si, el producto se pickeó, se verificó                        

                Next BePedidoDet

                If vPedidoCompletado Then
                    BePedidoEnc.Estado = "Despachado"
                    clsLnTrans_pe_enc.Actualizar_Estado(BePedidoEnc, lConnection, lTransaction)
                Else
                    BePedidoEnc.Estado = "Pendiente"
                    clsLnTrans_pe_enc.Actualizar_Estado(BePedidoEnc, lConnection, lTransaction)
                    vPedidoCompletado = True
                End If

            End If

            If vCantidadDespachadaAcum = 0 Then
                Throw New Exception("#EJC20200729NOSTCOKAFF: No se pudo encontrar la información de stock asociado al despacho, esta es una excepción poco usual. Debe realizar un backup de la base de datos y enviarla al departamento de desarrollo.")
            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' #EJC20240525: Traslado SAP 2
    ''' </summary>
    ''' <param name="BePedidoEnc"></param>
    ''' <param name="BeDespachoEnc"></param>
    ''' <param name="BeConfigEnc"></param>
    ''' <param name="pIdEmpresa"></param>
    ''' <param name="pIdBodega"></param>
    ''' <param name="AllowNegativeExceptionOnStock"></param>
    ''' <param name="lConnection"></param>
    ''' <param name="lTransaction"></param>
    Private Shared Sub Guarda_Trans_Despacho_Stock_AreaWMS_To_SAP(ByRef BePedidoEnc As clsBeTrans_pe_enc,
                                                                  ByVal BeDespachoEnc As clsBeTrans_despacho_enc,
                                                                  ByVal BeConfigEnc As clsBeI_nav_config_enc,
                                                                  ByVal pIdEmpresa As Integer,
                                                                  ByVal pIdBodega As Integer,
                                                                  ByVal AllowNegativeExceptionOnStock As Boolean,
                                                                  ByRef lConnection As SqlConnection,
                                                                  ByRef lTransaction As SqlTransaction)

        Dim vCantidadUMBasDespachada As Double = 0
        Dim vPesoDespachado As Double = 0
        Dim vPedidoCompletado As Boolean = True
        Dim vCantidadDespachadaAcum As Double = 0
        Dim BeTransReDet As New clsBeTrans_re_det
        Dim vIdMaxRecepcionDet As Integer = 0
        Dim vIdPropietario As Integer = 0
        Dim BeProductoEstado As New clsBeProducto_estado
        Dim lLicenciasProcesadas As New List(Of String)
        Dim vLicencia As String = ""
        Dim lBeTransRecDet As New List(Of clsBeTrans_re_det)
        Dim BeUnidadMedida As New clsBeUnidad_medida
        Dim BePresentacion As New clsBeProducto_Presentacion
        Dim vNoLineaTeorico As Integer = 0
        Dim BeTransOCDet As New clsBeTrans_oc_det
        Dim vIdProductoBodegaEquivalenteBodegaDestino As Integer = 0

        Try

            If Not BePedidoEnc Is Nothing Then

                '#20180822_0452PM_ESA: Iterar solo sobre los pedidos que aún tiene cantidad pendientes de despacho.
                For Each BePedidoDet As clsBeTrans_pe_det In BePedidoEnc.Detalle

                    vCantidadUMBasDespachada = 0 : vPesoDespachado = 0

                    '#EJC20180607: Si un producto del pedido, no tiene picking ni verificación, entonces la lista es vacía.
                    If Not BePedidoDet.ListaPickingUbic Is Nothing Then

                        '#20180822_0452PM_ESA: Se agregó x.Cantidad_Despachada =0 en lista de picking en Guarda_Trans_Despacho_Stock                            
                        Dim lPickingUbicVerificados = BePedidoDet.ListaPickingUbic.Where(Function(x) (x.Cantidad_Verificada > 0 OrElse x.Peso_verificado > 0) AndAlso (x.Cantidad_despachada < x.Cantidad_Verificada)).ToList()
                        'Se verificó para despacho en esa línea del pedido
                        If Not lPickingUbicVerificados Is Nothing Then

                            If lPickingUbicVerificados.Count > 0 Then

                                For Each BePickingUbic In lPickingUbicVerificados

                                    '#EJC20190311_0905PM: Se agregó BePedidoEnc.Cliente.Es_bodega_recepcion a la condición
                                    If (BePedidoEnc.Cliente.Es_Bodega_Traslado) Then

                                        Dim IdUbicacionRecBodDest As String = clsLnBodega_area.Get_IdUbicacion_Recepcion_By_Codigo_Area(BePedidoEnc.Cliente.Codigo,
                                                                                                                                     lConnection,
                                                                                                                                     lTransaction)
                                        Dim vIdEstadoDestino As Integer = clsLnProducto_estado.Get_IdEstado_By_Codigo_Area(BePedidoEnc.Cliente.Codigo,
                                                                                                                           lConnection,
                                                                                                                           lTransaction)


                                        If IdUbicacionRecBodDest <> 0 Then

                                            BePickingUbic.IdProducto = clsLnProducto.Get_Id_Producto_By_IdProductoBodega(BePickingUbic.IdProductoBodega,
                                                                                                                         lConnection,
                                                                                                                         lTransaction)


                                            vIdPropietario = clsLnPropietario_bodega.Get_IdPropietario_By_IdBodega_IdPropietarioBodega(BePickingUbic.IdBodega,
                                                                                                                                       BePickingUbic.IdPropietarioBodega,
                                                                                                                                       lConnection,
                                                                                                                                       lTransaction)

                                            BeProductoEstado = clsLnProducto_estado.Get_Buen_Estado_Porducto_By_IdPropietario_And_IdBodegaHH(vIdPropietario,
                                                                                                                                             BePickingUbic.IdBodega,
                                                                                                                                             lConnection,
                                                                                                                                             lTransaction)


                                            BeUnidadMedida = clsLnUnidad_medida.GetSingle(BePickingUbic.IdUnidadMedida, lConnection, lTransaction)

                                            If BePickingUbic.IdPresentacion <> 0 Then
                                                BePresentacion = clsLnProducto_presentacion.GetSingle(BePickingUbic.IdPresentacion, lConnection, lTransaction)
                                            End If

                                            vIdProductoBodegaEquivalenteBodegaDestino = clsLnProducto_bodega.Get_IdProductoBodega_By_IdProducto_And_IdBodega(BePickingUbic.IdProducto,
                                                                                                                                                             BePickingUbic.IdBodega,
                                                                                                                                                             lConnection,
                                                                                                                                                             lTransaction)

                                            clsLnStock.Actualizar_Stock_Por_Traslado_AreaWMS_To_SAP(BePickingUbic,
                                                                                                    IdUbicacionRecBodDest,
                                                                                                    vIdEstadoDestino,
                                                                                                    BeDespachoEnc,
                                                                                                    lConnection,
                                                                                                    lTransaction)

                                        Else
                                            Throw New Exception("#ERR20220504_2345: La ubicación de recepción no está definida para la bodega destino.")
                                        End If

                                    Else

                                        clsLnStock.Actualizar_Stock_Por_Despacho(BeDespachoEnc.IdDespachoEnc,
                                                                                 BePickingUbic,
                                                                                 AllowNegativeExceptionOnStock,
                                                                                 lConnection,
                                                                                 lTransaction)

                                    End If

                                    Actualiza_Stock_Reservado(BePickingUbic,
                                                              lConnection,
                                                              lTransaction)

                                    BePickingUbic.Cantidad_despachada = BePickingUbic.Cantidad_Verificada
                                    BePickingUbic.Peso_despachado = BePickingUbic.Peso_verificado

                                    vCantidadDespachadaAcum += BePickingUbic.Cantidad_Verificada

                                    vCantidadUMBasDespachada += BePickingUbic.Cantidad_Verificada
                                    vPesoDespachado += BePickingUbic.Peso_verificado

                                    Throw New Exception("#EJC20200729NOSTCOKAFF A: No se pudo actualizar la cantidad y el peso despachado.")
                                    If Not clsLnTrans_picking_ubic.Actualizar(BePickingUbic, lConnection, lTransaction) > 0 Then
                                        Throw New Exception("No se pudo actualizar la ubicación de picking: " & BePickingUbic.IdPickingUbic)
                                    End If

                                Next BePickingUbic

                            End If

                            '#CKFK20220725 Cambié el += por el =
                            BePedidoDet.Cant_despachada = vCantidadUMBasDespachada
                            BePedidoDet.Peso_despachado = vPesoDespachado

                            Try
                                clsLnTrans_pe_det.Actualizar_Cantidad_Y_Peso_Despachado(BePedidoDet, lConnection, lTransaction)
                            Catch ex As Exception
                            End Try

                            If vPedidoCompletado Then vPedidoCompletado = (BePedidoDet.Cantidad = BePedidoDet.Cant_despachada)

                            vCantidadUMBasDespachada = 0 : vPesoDespachado = 0

                        Else
                            'En la línea de pedido actual, no se verificó producto
                            'Por lo tanto no se puede marcar como despachado el pedido.
                            vPedidoCompletado = False
                        End If 'Fin tiene cantidad verificada

                    End If 'Fin si, el producto se pickeó, se verificó                        
                    Throw New Exception("#EJC20200729NOSTCOKAFF B: No se obtuvo información de la cantidad despachada, no se puede continuar con el despacho.")

                Next BePedidoDet

                If vPedidoCompletado Then
                    BePedidoEnc.Estado = "Despachado"
                    clsLnTrans_pe_enc.Actualizar_Estado(BePedidoEnc, lConnection, lTransaction)
                Else
                    BePedidoEnc.Estado = "Pendiente"
                    clsLnTrans_pe_enc.Actualizar_Estado(BePedidoEnc, lConnection, lTransaction)
                    vPedidoCompletado = True
                End If

            End If

            If vCantidadDespachadaAcum = 0 Then
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Guardar(ByVal pBeDespachoEnc As clsBeTrans_despacho_enc,
                                   ByVal AllowNegativeExceptionOnStock As Boolean,
                                   ByRef lConnection As SqlConnection,
                                   ByRef lTransaction As SqlTransaction) As Boolean

        Guardar = False

        Try

            'Despacho Encabezado
            Guarda_Trans_Despacho_Enc(pBeDespachoEnc,
                                      lConnection,
                                      lTransaction)

            'Despacho Detalle
            Guarda_Trans_Despacho_Det(pBeDespachoEnc,
                                      lConnection,
                                      lTransaction)

            'Movimientos
            Guarda_Trans_Despacho_Mov(pBeDespachoEnc,
                                      lConnection,
                                      lTransaction)

            Dim BeInterfaceConfig As New clsBeI_nav_config_enc
            BeInterfaceConfig = clsLnI_nav_config_enc.Get_Single_By_IdBodega_And_IdEmpresa(pBeDespachoEnc.IdBodega,
                                                                                           pBeDespachoEnc.IdEmpresa,
                                                                                           lConnection,
                                                                                           lTransaction)

            Dim OutBePedidoCompraEnc As New clsBeTrans_oc_enc()
            Dim OutBeRecepcionEnc As New clsBeTrans_re_enc()

            Guardar_Despacho_Stock(pBeDespachoEnc,
                                   BeInterfaceConfig,
                                   OutBePedidoCompraEnc,
                                   AllowNegativeExceptionOnStock,
                                   lConnection,
                                   lTransaction)

            'Estado en Pickings asociados
            Verifica_Status_Picking(pBeDespachoEnc,
                                    lConnection,
                                    lTransaction)

            Guarda_Trans_Packing_Enc(pBeDespachoEnc,
                                     lConnection,
                                     lTransaction)

            'Tabla intermedia para interface.
            clsLnI_nav_transacciones_out.Insertar_Salida(pBeDespachoEnc.IdEmpresa,
                                                         pBeDespachoEnc.IdBodega,
                                                         pBeDespachoEnc,
                                                         lConnection,
                                                         lTransaction)


            Guardar = True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Generar_Ingreso_Por_Anulacion_NC_SAP(ByVal IdPedidoEnc As Integer,
                                                               ByVal DocEntrySolicitudDevolucion As Integer) As Boolean

        Generar_Ingreso_Por_Anulacion_NC_SAP = False

        Dim BePedidoEnc As New clsBeTrans_pe_enc
        Dim BePedidoCompraEnc As New clsBeTrans_oc_enc
        Dim BePedidoDetWMS As New clsBeI_nav_ped_compra_det()
        Dim vResult As String = ""

        Try

            BePedidoEnc = clsLnTrans_pe_enc.GetSingle(IdPedidoEnc)

            If Not BePedidoEnc Is Nothing Then

                Dim BeINavPedCompra As New clsBeI_nav_ped_compra_enc
                BeINavPedCompra = New clsBeI_nav_ped_compra_enc()
                BeINavPedCompra.No = clsLnTrans_oc_enc.MaxID() + 1
                BeINavPedCompra.Posting_Date = Now
                BeINavPedCompra.Order_Date = Now
                BeINavPedCompra.Document_Date = Now
                BeINavPedCompra.Expected_Receipt_Date = Now
                BeINavPedCompra.Status = 1
                BeINavPedCompra.Buy_From_Vendor_No = BePedidoEnc.Cliente.Codigo
                BeINavPedCompra.Buy_From_Vendor_Name = BePedidoEnc.Cliente.Nombre_comercial
                BeINavPedCompra.Is_Internal_Transfer = False
                BeINavPedCompra.Location_Code = BePedidoEnc.IdBodega
                BeINavPedCompra.Vendor_Invoice_No = DocEntrySolicitudDevolucion
                BeINavPedCompra.Posting_Description = BePedidoEnc.Observacion
                BeINavPedCompra.Product_Owner_Code = BePedidoEnc.PropietarioBodega.IdPropietario
                BeINavPedCompra.Vendor_Invoice_No = BePedidoEnc.IdPedidoEnc
                BeINavPedCompra.Document_Type = clsDataContractDI.tTipoDocumentoIngreso.Ingreso_Por_NC_Anulada
                BeINavPedCompra.IsImport = False

                Dim BeProducto As New clsBeProducto

                For Each Det In BePedidoEnc.Detalle

                    BeProducto = clsLnProducto.Get_Single_By_IdProductoBodega(Det.IdProductoBodega)

                    If Not BeProducto Is Nothing Then

                        BePedidoDetWMS = New clsBeI_nav_ped_compra_det()
                        BePedidoDetWMS.NoEnc = BeINavPedCompra.No
                        BePedidoDetWMS.No = BeProducto.Codigo
                        BePedidoDetWMS.Line_No = Det.No_linea
                        BePedidoDetWMS.Planed_Receipt_Date = Date.Now
                        BePedidoDetWMS.Quantity = Det.Cantidad
                        BePedidoDetWMS.Quantity_Received = 0
                        BePedidoDetWMS.Description = BeProducto.Nombre
                        BePedidoDetWMS.Unit_of_Measure_Code = Det.Nom_unid_med
                        BePedidoDetWMS.Type = 2
                        BePedidoDetWMS.Variant_Code = Nothing
                        BePedidoDetWMS.Location_Code = BePedidoCompraEnc.IdBodega
                        BeINavPedCompra.Lineas_Detalle.Add(BePedidoDetWMS)

                    Else
                        Throw New Exception("no se pudo obtener el producto")
                    End If

                Next

                If clsLnI_nav_ped_compra_enc.Procesar_Pedido_Compra_MI3(BeINavPedCompra,
                                                                        BePedidoCompraEnc,
                                                                        vResult) Then
                    Generar_Ingreso_Por_Anulacion_NC_SAP = True
                Else
                    Throw New Exception(vResult)
                End If


            Else
                Throw New Exception("No se obtuvo el objeto de pedido para el IdPedidodEnc: " & IdPedidoEnc)
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Revertir_Despacho_By_IdPedidoEnc(ByVal IdPedidoEnc As Integer) As Boolean

        Revertir_Despacho_By_IdPedidoEnc = False

        Dim BePedidoEnc As New clsBeTrans_pe_enc
        Dim BePedidoCompraEnc As New clsBeTrans_oc_enc
        Dim BePedidoDetWMS As New clsBeI_nav_ped_compra_det()
        Dim vResult As String = ""

        Try

            BePedidoEnc = clsLnTrans_pe_enc.GetSingle(IdPedidoEnc)

            If Not BePedidoEnc Is Nothing Then

                Dim BeINavPedCompra As New clsBeI_nav_ped_compra_enc
                BeINavPedCompra = New clsBeI_nav_ped_compra_enc()
                BeINavPedCompra.No = clsLnTrans_oc_enc.MaxID() + 1
                BeINavPedCompra.Posting_Date = Now
                BeINavPedCompra.Order_Date = Now
                BeINavPedCompra.Document_Date = Now
                BeINavPedCompra.Expected_Receipt_Date = Now
                BeINavPedCompra.Status = 1
                BeINavPedCompra.Buy_From_Vendor_No = BePedidoEnc.Cliente.Codigo
                BeINavPedCompra.Buy_From_Vendor_Name = BePedidoEnc.Cliente.Nombre_comercial
                BeINavPedCompra.Is_Internal_Transfer = False
                BeINavPedCompra.Location_Code = BePedidoEnc.IdBodega
                BeINavPedCompra.Posting_Description = BePedidoEnc.Observacion
                BeINavPedCompra.Product_Owner_Code = BePedidoEnc.PropietarioBodega.IdPropietario
                BeINavPedCompra.Vendor_Invoice_No = BePedidoEnc.IdPedidoEnc
                BeINavPedCompra.Document_Type = clsDataContractDI.tTipoDocumentoIngreso.Ingreso_Por_NC_Anulada
                BeINavPedCompra.IsImport = False

                Dim BeProducto As New clsBeProducto

                For Each Det In BePedidoEnc.Detalle

                    BeProducto = clsLnProducto.Get_Single_By_IdProductoBodega(Det.IdProductoBodega)

                    If Not BeProducto Is Nothing Then

                        BePedidoDetWMS = New clsBeI_nav_ped_compra_det()
                        BePedidoDetWMS.NoEnc = BeINavPedCompra.No
                        BePedidoDetWMS.No = BeProducto.Codigo
                        BePedidoDetWMS.Line_No = Det.No_linea
                        BePedidoDetWMS.Planed_Receipt_Date = Date.Now
                        BePedidoDetWMS.Quantity = Det.Cantidad
                        BePedidoDetWMS.Quantity_Received = 0
                        BePedidoDetWMS.Description = BeProducto.Nombre
                        BePedidoDetWMS.Unit_of_Measure_Code = Det.Nom_unid_med
                        BePedidoDetWMS.Type = 2
                        BePedidoDetWMS.Variant_Code = Nothing
                        BePedidoDetWMS.Location_Code = BePedidoCompraEnc.IdBodega
                        BeINavPedCompra.Lineas_Detalle.Add(BePedidoDetWMS)

                    Else
                        Throw New Exception("no se pudo obtener el producto")
                    End If

                Next

                If clsLnI_nav_ped_compra_enc.Procesar_Pedido_Compra_MI3(BeINavPedCompra,
                                                                        BePedidoCompraEnc,
                                                                        vResult,
                                                                        BePedidoEnc) Then
                    Revertir_Despacho_By_IdPedidoEnc = True
                Else
                    Throw New Exception(vResult)
                End If

            Else
                Throw New Exception("No se obtuvo el objeto de pedido para el IdPedidodEnc: " & IdPedidoEnc)
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdPedidoEnc(ByVal IdPedidoEnc As Integer,
                                                  ByVal lConnection As SqlConnection,
                                                  ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_despacho_enc)

        Get_All_By_IdPedidoEnc = Nothing

        Dim listTransDespachoEnc As New List(Of clsBeTrans_despacho_enc)

        Try

            Dim vSQL As String = "SELECT trans_despacho_enc.IdDespachoEnc, trans_despacho_enc.IdBodega, trans_despacho_enc.IdPropietarioBodega, trans_despacho_enc.IdVehiculo, 
                         trans_despacho_enc.IdPiloto, trans_despacho_enc.IdRuta, trans_despacho_enc.fecha, trans_despacho_enc.no_pase, 
                         trans_despacho_enc.observacion, trans_despacho_enc.hora_ini, trans_despacho_enc.hora_fin, trans_despacho_enc.estado, 
                         trans_despacho_enc.numero, trans_despacho_enc.marchamo, trans_despacho_enc.cant_bultos, trans_despacho_enc.user_agr, 
                         trans_despacho_enc.fec_agr, trans_despacho_enc.user_mod, trans_despacho_enc.fec_mod, 
                         trans_despacho_enc.activo, trans_despacho_enc.no_documento_externo
                    FROM trans_pe_det INNER JOIN
                         trans_pe_enc ON trans_pe_det.IdPedidoEnc = trans_pe_enc.IdPedidoEnc INNER JOIN
                         trans_despacho_det ON trans_pe_det.IdPedidoDet = trans_despacho_det.IdPedidoDet INNER JOIN
                         trans_despacho_enc ON trans_despacho_det.IdDespachoEnc = trans_despacho_enc.IdDespachoEnc
                  WHERE trans_pe_enc.IdPedidoEnc=@IdPedidoEnc
                GROUP BY trans_despacho_enc.IdDespachoEnc, trans_despacho_enc.IdBodega, trans_despacho_enc.IdPropietarioBodega, trans_despacho_enc.IdVehiculo, 
                         trans_despacho_enc.IdPiloto, trans_despacho_enc.IdRuta, trans_despacho_enc.fecha, trans_despacho_enc.no_pase, 
                         trans_despacho_enc.observacion, trans_despacho_enc.hora_ini, trans_despacho_enc.hora_fin, trans_despacho_enc.estado, 
                         trans_despacho_enc.numero, trans_despacho_enc.marchamo, trans_despacho_enc.cant_bultos, trans_despacho_enc.user_agr, 
                         trans_despacho_enc.fec_agr, trans_despacho_enc.user_mod, trans_despacho_enc.fec_mod, 
                         trans_despacho_enc.activo, trans_despacho_enc.no_documento_externo "
            vSQL += "ORDER BY trans_despacho_enc.fecha asc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", IdPedidoEnc)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                    For Each lRow As DataRow In lDT.Rows
                        Dim BeTransDespachoEnc As New clsBeTrans_despacho_enc()
                        Cargar(BeTransDespachoEnc, lRow)
                        listTransDespachoEnc.Add(BeTransDespachoEnc)
                    Next
                End If

            End Using

            Return listTransDespachoEnc

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Count_Despacho_By_IdPedidoEnc(ByVal IdPedidoEnc As Integer) As Integer

        Dim lCount As Integer = 0

        Try

            Dim vSQL As String = "SELECT COUNT(Distinct IdDespachoEnc) FROM trans_despacho_det WHERE IdPedidoEnc = @IdPedidoEnc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection)

                        lCommand.Transaction = lTransaction
                        lCommand.CommandType = CommandType.Text
                        lCommand.Parameters.AddWithValue("@IdPedidoEnc", IdPedidoEnc)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lCount = CInt(lReturnValue)
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lCount

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar_No_Documento_Externo(ByRef oBeTrans_despacho_enc As clsBeTrans_despacho_enc,
                                                           Optional ByVal pConection As SqlConnection = Nothing,
                                                           Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("trans_despacho_enc")
            Upd.Add("no_documento_externo", "@no_documento_externo", DataType.Parametro)
            Upd.Where("IdDespachoEnc = @IdDespachoEnc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IdDespachoEnc", oBeTrans_despacho_enc.IdDespachoEnc))
            cmd.Parameters.Add(New SqlParameter("@no_documento_externo", oBeTrans_despacho_enc.No_Documento_Externo))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar_No_Pase(ByRef oBeTrans_despacho_enc As clsBeTrans_despacho_enc,
                                              Optional ByVal pConection As SqlConnection = Nothing,
                                              Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("trans_despacho_enc")
            Upd.Add("no_pase", "@no_pase", DataType.Parametro)
            Upd.Where("IdDespachoEnc = @IdDespachoEnc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IdDespachoEnc", oBeTrans_despacho_enc.IdDespachoEnc))
            cmd.Parameters.Add(New SqlParameter("@no_pase", oBeTrans_despacho_enc.No_pase))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function
    Private Shared Function ExtraerCorrelativoDesdeLote(lote As String) As Integer
        ' Validamos que el valor no sea Nothing ni vacío
        If String.IsNullOrWhiteSpace(lote) Then
            Return 0
        End If

        ' Removemos espacios por seguridad
        lote = lote.Trim()

        ' Utilizamos una expresión regular para capturar solo los dígitos consecutivos iniciales
        Dim match = System.Text.RegularExpressions.Regex.Match(lote, "^\d+")

        If match.Success Then
            ' Convertimos la parte numérica capturada en entero
            Try
                Return Integer.Parse(match.Value)
            Catch ex As Exception
                Return 0
            End Try
        End If

        ' Si no hay dígitos iniciales, se retorna 0 (o puedes lanzar una excepción según tu lógica)
        Return 0
    End Function

    Private Shared Sub Guarda_Trans_Despacho_Det(ByRef ObjEnc As clsBeTrans_despacho_enc,
                                                 ByRef lConnection As SqlConnection,
                                                 ByRef lTransaction As SqlTransaction)

        Try

            If ObjEnc IsNot Nothing Then

                Dim lMaxID As Integer = clsLnTrans_despacho_det.MaxID(lConnection, lTransaction)

                For Each BePedidoEnc As clsBeTrans_pe_enc In ObjEnc.ListaPedidos

                    For Each BePedidoDet As clsBeTrans_pe_det In BePedidoEnc.Detalle

                        If Not BePedidoDet.ListaPickingUbic Is Nothing Then

                            Dim lPickingUbicVerificados = BePedidoDet.ListaPickingUbic.Where(Function(x) (x.Cantidad_Verificada > 0 OrElse x.Peso_verificado > 0) AndAlso (x.Cantidad_despachada < x.Cantidad_Verificada)).ToList()

                            Try
                                If BePedidoDet.Cantidad <= lPickingUbicVerificados.Sum(Function(x) x.Cantidad_Recibida) Then
                                    Debug.WriteLine("Error Solicitado: " & BePedidoDet.Cantidad & " Verificado: " & lPickingUbicVerificados.Sum(Function(x) x.Cantidad_Recibida))
                                End If
                            Catch ex As Exception

                            End Try

                            'Se verificó para despacho en esa línea del pedido
                            If Not lPickingUbicVerificados Is Nothing Then

                                If lPickingUbicVerificados.Count > 0 Then

                                    For Each BePickingUbic As clsBeTrans_picking_ubic In lPickingUbicVerificados

                                        For Each BeDespachoDet As clsBeTrans_despacho_det In ObjEnc.ListaDetalle.Where(Function(x) x.IdPickingUbic = BePickingUbic.IdPickingUbic)

                                            '#EJC20200721: Por despachos parciales CLC.
                                            BeDespachoDet.CantidadDespachada = BePickingUbic.Cantidad_Verificada - BePickingUbic.Cantidad_despachada

                                            If BeDespachoDet.IsNew Then
                                                lMaxID += 1
                                                BeDespachoDet.IdDespachoEnc = ObjEnc.IdDespachoEnc
                                                BeDespachoDet.IdDespachoDet = lMaxID
                                                clsLnTrans_despacho_det.Insertar(BeDespachoDet, lConnection, lTransaction)
                                            Else
                                                clsLnTrans_despacho_det.Actualizar(BeDespachoDet, lConnection, lTransaction)
                                            End If

                                        Next

                                    Next

                                Else
                                    Debug.Print("No hay detalle de despacho")
                                End If

                            End If

                        End If

                    Next

                Next

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Get_No_Pase_By_IdDespachoEnc(ByVal IdDespachoEnc As Integer,
                                                        ByVal pConnection As SqlConnection,
                                                        ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lNoPase As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(no_pase,0) no_pase FROM trans_despacho_enc WHERE IdDespachoEnc = @IdDespachoEnc "

            Using lCommand As New SqlCommand(vSQL, pConnection)

                lCommand.Transaction = pTransaction
                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@IdDespachoEnc", IdDespachoEnc)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lNoPase = (lReturnValue)
                End If

            End Using

            Return lNoPase

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Shared Function Guardar_Despacho(ByVal pListBePickingUbic As List(Of clsBeTrans_picking_ubic),
                                            ByVal pBePedidoEnc As clsBeTrans_pe_enc,
                                            ByVal pConnection As SqlConnection,
                                            ByVal pTransaction As SqlTransaction) As Boolean

        Dim hora_server As DateTime
        Dim BeDespachoEnc As New clsBeTrans_despacho_enc

        Guardar_Despacho = False

        Try

            hora_server = clsServidor.Get_Fecha_Servidor()

            BeDespachoEnc.IdBodega = pBePedidoEnc.IdBodega
            BeDespachoEnc.IdEmpresa = clsLnBodega.Get_IdEmpresa_By_IdBodega(BeDespachoEnc.IdBodega)
            BeDespachoEnc.IdPropietarioBodega = pBePedidoEnc.IdPropietarioBodega
            BeDespachoEnc.IdPiloto = 0
            BeDespachoEnc.IdVehiculo = 0
            BeDespachoEnc.IdRuta = 0
            BeDespachoEnc.Fecha = hora_server
            BeDespachoEnc.No_pase = 0
            BeDespachoEnc.Observacion = ""
            BeDespachoEnc.Hora_ini = hora_server
            BeDespachoEnc.Hora_fin = hora_server
            BeDespachoEnc.Estado = "Finalizado"
            BeDespachoEnc.Numero = 0
            BeDespachoEnc.Marchamo = ""
            BeDespachoEnc.Cant_bultos = 0
            BeDespachoEnc.IsNew = True
            BeDespachoEnc.User_agr = pBePedidoEnc.User_agr
            BeDespachoEnc.Fec_agr = hora_server
            BeDespachoEnc.User_mod = pBePedidoEnc.User_agr
            BeDespachoEnc.Fec_mod = hora_server
            BeDespachoEnc.Activo = True

            BeDespachoEnc.ListaPedidos.Add(pBePedidoEnc)

            If Not pListBePickingUbic Is Nothing Then
                For Each BePickingUbic In pListBePickingUbic
                    Adicionar_Producto_A_Detalle_Despacho(BeDespachoEnc,
                                                          BePickingUbic,
                                                          pBePedidoEnc.Fecha_Pedido,
                                                          pConnection,
                                                          pTransaction)
                Next
            End If

            Guardar_Despacho = Guardar_Auto(BeDespachoEnc, pConnection, pTransaction)

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Shared Sub Adicionar_Producto_A_Detalle_Despacho(ByRef BeDespachoEnc As clsBeTrans_despacho_enc,
                                                             ByVal BeTransPickingUbic As clsBeTrans_picking_ubic,
                                                             ByVal vFechaPedido As Date,
                                                             ByVal lConnection As SqlConnection,
                                                             ByVal lTransaction As SqlTransaction)

        Try

            Dim BeDespachoDet As New clsBeTrans_despacho_det

            If BeDespachoEnc.ListaDetalle IsNot Nothing AndAlso BeDespachoEnc.ListaDetalle.Count > 0 Then
                BeDespachoDet.IdDespachoDet = BeDespachoEnc.ListaDetalle.Max(Function(b) b.IdDespachoDet) + 1
            Else
                BeDespachoDet.IdDespachoDet = 1
            End If

            BeDespachoDet.Codigo = BeTransPickingUbic.CodigoProducto
            BeDespachoDet.NombreProducto = BeTransPickingUbic.NombreProducto
            BeDespachoDet.NombreEstado = BeTransPickingUbic.ProductoEstado
            BeDespachoDet.IdPickingUbic = BeTransPickingUbic.IdPickingUbic
            BeDespachoDet.IdProductoBodega = BeTransPickingUbic.IdProductoBodega
            BeDespachoDet.IdProductoEstado = BeTransPickingUbic.IdProductoEstado
            BeDespachoDet.IdPresentacion = BeTransPickingUbic.IdPresentacion
            BeDespachoDet.IdUnidadMedidaBasica = BeTransPickingUbic.IdUnidadMedida
            BeDespachoDet.IdPedidoEnc = BeTransPickingUbic.IdPedidoEnc
            BeDespachoDet.IdPedidoDet = BeTransPickingUbic.IdPedidoDet
            BeDespachoDet.IdPickingUbic = BeTransPickingUbic.IdPickingUbic
            BeDespachoDet.CantidadDespachada = BeTransPickingUbic.Cantidad_Verificada
            BeDespachoDet.PesoDespachado = BeTransPickingUbic.Peso_verificado
            BeDespachoDet.User_agr = BeDespachoEnc.User_agr
            BeDespachoDet.Fec_agr = Now
            BeDespachoDet.User_mod = BeDespachoEnc.User_agr
            BeDespachoDet.NombreUbicacion = BeTransPickingUbic.NombreUbicacion
            BeDespachoDet.Fec_mod = Now
            BeDespachoDet.Activo = True
            BeDespachoDet.IsNew = True
            BeDespachoDet.FechaPedido = vFechaPedido

            If BeTransPickingUbic.IdProductoTallaColor > 0 Then
                BeDespachoDet.Talla = BeTransPickingUbic.Codigo_Talla
                BeDespachoDet.Color = BeTransPickingUbic.Codigo_Color
                BeDespachoDet.IdProductoTallaColor = BeTransPickingUbic.IdProductoTallaColor
            Else
                BeDespachoDet.Talla = ""
                BeDespachoDet.Color = ""

            End If

            BeDespachoEnc.ListaDetalle.Add(BeDespachoDet)

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Guardar_Auto(ByVal pBeDespachoEnc As clsBeTrans_despacho_enc,
                                        ByVal lConnection As SqlConnection,
                                        ByVal lTransaction As SqlTransaction) As Boolean

        Guardar_Auto = False

        Try

            'Despacho Encabezado
            Guarda_Trans_Despacho_Enc(pBeDespachoEnc,
                                      lConnection,
                                      lTransaction)

            'Despacho Detalle
            Guarda_Trans_Despacho_Det(pBeDespachoEnc,
                                      lConnection,
                                      lTransaction)

            'Movimientos
            Guarda_Trans_Despacho_Mov(pBeDespachoEnc,
                                      lConnection,
                                      lTransaction)

            Dim BeInterfaceConfig As New clsBeI_nav_config_enc
            BeInterfaceConfig = clsLnI_nav_config_enc.Get_Single_By_IdBodega_And_IdEmpresa(pBeDespachoEnc.IdBodega,
                                                                                           pBeDespachoEnc.IdEmpresa,
                                                                                           lConnection,
                                                                                           lTransaction)

            Dim OutBePedidoCompraEnc As New clsBeTrans_oc_enc()
            Dim OutBeRecepcionEnc As New clsBeTrans_re_enc()

            '#GT21012025: si tipo doc permite, se genera transferencia hacia otra bodega (aca hace el registro de oc y recepcion)
            Guardar_Despacho_Stock(pBeDespachoEnc,
                                   BeInterfaceConfig,
                                   OutBePedidoCompraEnc,
                                   False,
                                   lConnection,
                                   lTransaction)

            'Estado en Pickings asociados
            Verifica_Status_Picking(pBeDespachoEnc,
                                    lConnection,
                                    lTransaction)

            Guarda_Trans_Packing_Enc(pBeDespachoEnc,
                                     lConnection,
                                     lTransaction)

            'Tabla intermedia para interface.
            clsLnI_nav_transacciones_out.Insertar_Salida(pBeDespachoEnc.IdEmpresa,
                                                         pBeDespachoEnc.IdBodega,
                                                         pBeDespachoEnc,
                                                         lConnection,
                                                         lTransaction)

            Guardar_Auto = True

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Private Shared Sub Validar_Solicitado_Vrs_Despachado(ByRef ObjEnc As clsBeTrans_despacho_enc,
                                                         ByRef lConnection As SqlConnection,
                                                         ByRef lTransaction As SqlTransaction)


        Try

            If ObjEnc IsNot Nothing Then

                For Each BePedidoEnc As clsBeTrans_pe_enc In ObjEnc.ListaPedidos

                    For Each peddet In BePedidoEnc.Detalle

                        If peddet.Cantidad > 0 Then

                            Dim cantidadpickeada As Double = ObjEnc.ListaDetalle?.FindAll(Function(x) x.IdPedidoDet = peddet.IdPedidoDet).Sum(Function(y) y.CantidadDespachada)

                            If peddet.Cantidad <> Math.Truncate(peddet.Cantidad) Then
                                ' Tiene decimales
                            Else
                                If cantidadpickeada > peddet.Cantidad Then
                                    Throw New Exception("La cantidad pickeada (" & cantidadpickeada & ") del producto " &
                                                    peddet.Codigo_Producto & " es mayor a la solicitada (" & peddet.Cantidad & ")")
                                End If

                            End If


                        End If

                    Next

                Next

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub
End Class