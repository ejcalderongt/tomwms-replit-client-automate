Imports System.Data.SqlClient

Public Class clsLnTrans_pe_enc

    Public Shared Sub Cargar(ByRef oBeTrans_pe_enc As clsBeTrans_pe_enc, ByRef dr As DataRow)

        Try

            With oBeTrans_pe_enc

                .IdPedidoEnc = IIf(IsDBNull(dr.Item("IdPedidoEnc")), 0, dr.Item("IdPedidoEnc"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .IdCliente = IIf(IsDBNull(dr.Item("IdCliente")), 0, dr.Item("IdCliente"))
                .Cliente.IdCliente = IIf(IsDBNull(dr.Item("IdCliente")), 0, dr.Item("IdCliente"))
                .IdMuelle = IIf(IsDBNull(dr.Item("IdMuelle")), 0, dr.Item("IdMuelle"))
                .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                .PropietarioBodega.IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                .TipoPedido.IdTipoPedido = IIf(IsDBNull(dr.Item("IdTipoPedido")), 0, dr.Item("IdTipoPedido"))
                .IdTipoPedido = IIf(IsDBNull(dr.Item("IdTipoPedido")), 0, dr.Item("IdTipoPedido"))
                .Fecha_Pedido = IIf(IsDBNull(dr.Item("Fecha_Pedido")), Date.Now, dr.Item("Fecha_Pedido"))
                .Hora_ini = IIf(IsDBNull(dr.Item("hora_ini")), Date.Now, dr.Item("hora_ini"))
                .Hora_fin = IIf(IsDBNull(dr.Item("hora_fin")), Date.Now, dr.Item("hora_fin"))
                .Ubicacion = IIf(IsDBNull(dr.Item("ubicacion")), "", dr.Item("ubicacion"))
                .Estado = IIf(IsDBNull(dr.Item("estado")), "", dr.Item("estado"))
                .No_despacho = IIf(IsDBNull(dr.Item("no_despacho")), 0, dr.Item("no_despacho"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .No_documento = IIf(IsDBNull(dr.Item("no_documento")), 0, dr.Item("no_documento"))
                .Local = IIf(IsDBNull(dr.Item("local")), False, dr.Item("local"))
                .Pallet_primero = IIf(IsDBNull(dr.Item("pallet_primero")), False, dr.Item("pallet_primero"))
                .Dias_cliente = IIf(IsDBNull(dr.Item("dias_cliente")), 0.0, dr.Item("dias_cliente"))
                .Anulado = IIf(IsDBNull(dr.Item("anulado")), False, dr.Item("anulado"))
                .RoadKilometraje = IIf(IsDBNull(dr.Item("RoadKilometraje")), 0.0, dr.Item("RoadKilometraje"))
                .RoadFechaEntr = IIf(IsDBNull(dr.Item("RoadFechaEntr")), Date.Now, dr.Item("RoadFechaEntr"))
                .RoadDirEntrega = IIf(IsDBNull(dr.Item("RoadDirEntrega")), "", dr.Item("RoadDirEntrega"))
                .RoadTotal = IIf(IsDBNull(dr.Item("RoadTotal")), 0.0, dr.Item("RoadTotal"))
                .RoadDesMonto = IIf(IsDBNull(dr.Item("RoadDesMonto")), 0.0, dr.Item("RoadDesMonto"))
                .RoadImpMonto = IIf(IsDBNull(dr.Item("RoadImpMonto")), 0.0, dr.Item("RoadImpMonto"))
                .RoadPeso = IIf(IsDBNull(dr.Item("RoadPeso")), 0.0, dr.Item("RoadPeso"))
                .RoadBandera = IIf(IsDBNull(dr.Item("RoadBandera")), "", dr.Item("RoadBandera"))
                .RoadStatCom = IIf(IsDBNull(dr.Item("RoadStatCom")), "", dr.Item("RoadStatCom"))
                .RoadCalcoBJ = IIf(IsDBNull(dr.Item("RoadCalcoBJ")), "", dr.Item("RoadCalcoBJ"))
                .RoadImpres = IIf(IsDBNull(dr.Item("RoadImpres")), 0, dr.Item("RoadImpres"))
                .RoadADD1 = IIf(IsDBNull(dr.Item("RoadADD1")), "", dr.Item("RoadADD1"))
                .RoadADD2 = IIf(IsDBNull(dr.Item("RoadADD2")), "", dr.Item("RoadADD2"))
                .RoadADD3 = IIf(IsDBNull(dr.Item("RoadADD3")), "", dr.Item("RoadADD3"))
                .RoadStatProc = IIf(IsDBNull(dr.Item("RoadStatProc")), "", dr.Item("RoadStatProc"))
                .RoadRechazado = IIf(IsDBNull(dr.Item("RoadRechazado")), False, dr.Item("RoadRechazado"))
                .RoadRazon_Rechazado = IIf(IsDBNull(dr.Item("RoadRazon_Rechazado")), "", dr.Item("RoadRazon_Rechazado"))
                .RoadInformado = IIf(IsDBNull(dr.Item("RoadInformado")), False, dr.Item("RoadInformado"))
                .RoadSucursal = IIf(IsDBNull(dr.Item("RoadSucursal")), "", dr.Item("RoadSucursal"))
                .RoadIdDespacho = IIf(IsDBNull(dr.Item("RoadIdDespacho")), 0, dr.Item("RoadIdDespacho"))
                .RoadIdFacturacion = IIf(IsDBNull(dr.Item("RoadIdFacturacion")), 0, dr.Item("RoadIdFacturacion"))
                .RoadIdRuta = IIf(IsDBNull(dr.Item("RoadIdRuta")), 0, dr.Item("RoadIdRuta"))
                .RoadIdVendedor = IIf(IsDBNull(dr.Item("RoadIdVendedor")), 0, dr.Item("RoadIdVendedor"))
                .RoadIdRutaDespacho = IIf(IsDBNull(dr.Item("RoadIdRutaDespacho")), 0, dr.Item("RoadIdRutaDespacho"))
                .RoadIdVendedorDespacho = IIf(IsDBNull(dr.Item("RoadIdVendedorDespacho")), 0, dr.Item("RoadIdVendedorDespacho"))
                .Observacion = IIf(IsDBNull(dr.Item("Observacion")), "", dr.Item("Observacion"))
                .PedidoRoad = IIf(IsDBNull(dr.Item("PedidoRoad")), False, dr.Item("PedidoRoad"))
                .HoraEntregaDesde = IIf(IsDBNull(dr.Item("HoraEntregaDesde")), Date.Now, dr.Item("HoraEntregaDesde"))
                .HoraEntregaHasta = IIf(IsDBNull(dr.Item("HoraEntregaHasta")), Date.Now, dr.Item("HoraEntregaHasta"))
                .Referencia = IIf(IsDBNull(dr.Item("referencia")), "", dr.Item("referencia"))
                .Enviado_A_ERP = IIf(IsDBNull(dr.Item("Enviado_A_ERP")), False, dr.Item("Enviado_A_ERP"))
                .Control_Ultimo_Lote = IIf(IsDBNull(dr.Item("Control_Ultimo_Lote")), False, dr.Item("Control_Ultimo_Lote"))
                .Serie = IIf(IsDBNull(dr.Item("Serie")), "", dr.Item("Serie"))
                .Correlativo = IIf(IsDBNull(dr.Item("Correlativo")), "0", dr.Item("Correlativo"))
                .Referencia_Documento_Ingreso_Bodega_Destino = IIf(IsDBNull(dr.Item("Referencia_Documento_Ingreso_Bodega_Destino")), "", dr.Item("Referencia_Documento_Ingreso_Bodega_Destino"))
                .No_Picking_ERP = IIf(IsDBNull(dr.Item("No_Picking_ERP")), "", dr.Item("No_Picking_ERP"))
                .No_Documento_Externo = IIf(IsDBNull(dr.Item("No_Documento_Externo")), "", dr.Item("No_Documento_Externo"))
                .Requiere_Tarimas = IIf(IsDBNull(dr.Item("Requiere_Tarimas")), False, dr.Item("Requiere_Tarimas"))
                .Fecha_Preparacion = IIf(IsDBNull(dr.Item("Fecha_Preparacion")), Date.Now, dr.Item("Fecha_Preparacion"))
                .IdTipoManufactura = Convert.ToInt32(IIf(IsDBNull(dr.Item("IdTipoManufactura")), 0, dr.Item("IdTipoManufactura")))
                .IdPickingEnc = IIf(IsDBNull(dr.Item("IdPickingEnc")), 0, dr.Item("IdPickingEnc"))

                '#AT20220401 NombreRutaDespacho para pedidos HH
                If dr.Table.Columns.Contains("NombreRutaDespacho") Then
                    .NombreRutaDespacho = IIf(IsDBNull(dr.Item("NombreRutaDespacho")), "", dr.Item("NombreRutaDespacho"))
                End If

                .Bodega_Origen = IIf(IsDBNull(dr.Item("Bodega_Origen")), "", dr.Item("Bodega_Origen"))
                .Bodega_Destino = IIf(IsDBNull(dr.Item("Bodega_Destino")), "", dr.Item("Bodega_Destino"))
                .IdMotivoDevolucion = IIf(IsDBNull(dr.Item("IdMotivoDevolucion")), 0, dr.Item("IdMotivoDevolucion"))
                .Codigo_Empresa_ERP = IIf(IsDBNull(dr.Item("Codigo_Empresa_ERP")), "", dr.Item("Codigo_Empresa_ERP"))
                .EsExportacion = IIf(IsDBNull(dr.Item("EsExportacion")), False, dr.Item("EsExportacion"))

            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_pe_enc As clsBeTrans_pe_enc,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            Ins.Init("trans_pe_enc")
            Ins.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
            If oBeTrans_pe_enc.IdBodega <> 0 Then Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("idcliente", "@idcliente", DataType.Parametro)
            Ins.Add("idmuelle", "@idmuelle", DataType.Parametro)
            Ins.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Ins.Add("IdTipoPedido", "@IdTipoPedido", DataType.Parametro)
            Ins.Add("IdPickingEnc", "@IdPickingEnc", DataType.Parametro)
            Ins.Add("fecha_pedido", "@fecha_pedido", DataType.Parametro)
            Ins.Add("hora_ini", "@hora_ini", DataType.Parametro)
            Ins.Add("hora_fin", "@hora_fin", DataType.Parametro)
            Ins.Add("ubicacion", "@ubicacion", DataType.Parametro)
            Ins.Add("estado", "@estado", DataType.Parametro)
            Ins.Add("no_despacho", "@no_despacho", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("no_documento", "@no_documento", DataType.Parametro)
            Ins.Add("local", "@local", DataType.Parametro)
            Ins.Add("pallet_primero", "@pallet_primero", DataType.Parametro)
            Ins.Add("dias_cliente", "@dias_cliente", DataType.Parametro)
            Ins.Add("anulado", "@anulado", DataType.Parametro)
            Ins.Add("roadkilometraje", "@roadkilometraje", DataType.Parametro)
            Ins.Add("roadfechaentr", "@roadfechaentr", DataType.Parametro)
            Ins.Add("roaddirentrega", "@roaddirentrega", DataType.Parametro)
            Ins.Add("roadtotal", "@roadtotal", DataType.Parametro)
            Ins.Add("roaddesmonto", "@roaddesmonto", DataType.Parametro)
            Ins.Add("roadimpmonto", "@roadimpmonto", DataType.Parametro)
            Ins.Add("roadpeso", "@roadpeso", DataType.Parametro)
            Ins.Add("roadbandera", "@roadbandera", DataType.Parametro)
            Ins.Add("roadstatcom", "@roadstatcom", DataType.Parametro)
            Ins.Add("roadcalcobj", "@roadcalcobj", DataType.Parametro)
            Ins.Add("roadimpres", "@roadimpres", DataType.Parametro)
            Ins.Add("roadadd1", "@roadadd1", DataType.Parametro)
            Ins.Add("roadadd2", "@roadadd2", DataType.Parametro)
            Ins.Add("roadadd3", "@roadadd3", DataType.Parametro)
            Ins.Add("roadstatproc", "@roadstatproc", DataType.Parametro)
            Ins.Add("roadrechazado", "@roadrechazado", DataType.Parametro)
            Ins.Add("roadrazon_rechazado", "@roadrazon_rechazado", DataType.Parametro)
            Ins.Add("roadinformado", "@roadinformado", DataType.Parametro)
            Ins.Add("roadsucursal", "@roadsucursal", DataType.Parametro)
            Ins.Add("roadiddespacho", "@roadiddespacho", DataType.Parametro)
            Ins.Add("roadidfacturacion", "@roadidfacturacion", DataType.Parametro)
            Ins.Add("roadidruta", "@roadidruta", DataType.Parametro)
            Ins.Add("roadidvendedor", "@roadidvendedor", DataType.Parametro)
            Ins.Add("roadidrutadespacho", "@roadidrutadespacho", DataType.Parametro)
            Ins.Add("roadidvendedordespacho", "@roadidvendedordespacho", DataType.Parametro)
            Ins.Add("observacion", "@observacion", DataType.Parametro)
            Ins.Add("pedidoroad", "@pedidoroad", DataType.Parametro)
            Ins.Add("horaentregadesde", "@horaentregadesde", DataType.Parametro)
            Ins.Add("horaentregahasta", "@horaentregahasta", DataType.Parametro)
            Ins.Add("referencia", "@referencia", DataType.Parametro)
            Ins.Add("Enviado_A_ERP", "@Enviado_A_ERP", DataType.Parametro)
            Ins.Add("control_ultimo_lote", "@control_ultimo_lote", DataType.Parametro)
            Ins.Add("serie", "@serie", DataType.Parametro)
            Ins.Add("correlativo", "@correlativo", DataType.Parametro)
            Ins.Add("sync_mi3", "@sync_mi3", DataType.Parametro)
            Ins.Add("Referencia_Documento_Ingreso_Bodega_Destino", "@Referencia_Documento_Ingreso_Bodega_Destino", DataType.Parametro)
            Ins.Add("No_Picking_ERP", "@No_Picking_ERP", DataType.Parametro)
            Ins.Add("no_documento_externo", "@no_documento_externo", DataType.Parametro)
            Ins.Add("requiere_tarimas", "@requiere_tarimas", DataType.Parametro)
            Ins.Add("fecha_preparacion", "@fecha_preparacion", DataType.Parametro)
            Ins.Add("IdTipoManufactura", "@IdTipoManufactura", DataType.Parametro)
            Ins.Add("bodega_origen", "@bodega_origen", DataType.Parametro)
            Ins.Add("bodega_destino", "@bodega_destino", DataType.Parametro)
            Ins.Add("Codigo_Empresa_Erp", "@Codigo_Empresa_Erp", DataType.Parametro)
            Ins.Add("EsExportacion", "@EsExportacion", DataType.Parametro)

            If Not oBeTrans_pe_enc.IdMotivoDevolucion = 0 Then Ins.Add("IdMotivoDevolucion", "@IdMotivoDevolucion", DataType.Parametro)

            Dim sp As String = Ins.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTrans_pe_enc.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", IIf(oBeTrans_pe_enc.IdBodega = 0, DBNull.Value, oBeTrans_pe_enc.IdBodega)))
            cmd.Parameters.Add(New SqlParameter("@IDCLIENTE", IIf(oBeTrans_pe_enc.Cliente.IdCliente = 0, DBNull.Value, oBeTrans_pe_enc.Cliente.IdCliente)))
            cmd.Parameters.Add(New SqlParameter("@IDMUELLE", IIf(oBeTrans_pe_enc.IdMuelle = 0, DBNull.Value, oBeTrans_pe_enc.IdMuelle)))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", IIf(oBeTrans_pe_enc.PropietarioBodega.IdPropietarioBodega = 0, DBNull.Value, oBeTrans_pe_enc.PropietarioBodega.IdPropietarioBodega)))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOPEDIDO", IIf(oBeTrans_pe_enc.TipoPedido.IdTipoPedido = 0, DBNull.Value, oBeTrans_pe_enc.TipoPedido.IdTipoPedido)))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", oBeTrans_pe_enc.IdPickingEnc))
            cmd.Parameters.Add(New SqlParameter("@FECHA_PEDIDO", oBeTrans_pe_enc.Fecha_Pedido))
            cmd.Parameters.Add(New SqlParameter("@HORA_INI", oBeTrans_pe_enc.Hora_ini))
            cmd.Parameters.Add(New SqlParameter("@HORA_FIN", oBeTrans_pe_enc.Hora_fin))
            cmd.Parameters.Add(New SqlParameter("@UBICACION", oBeTrans_pe_enc.Ubicacion))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeTrans_pe_enc.Estado))
            cmd.Parameters.Add(New SqlParameter("@NO_DESPACHO", oBeTrans_pe_enc.No_despacho))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_pe_enc.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_pe_enc.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_pe_enc.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_pe_enc.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_pe_enc.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@NO_DOCUMENTO", oBeTrans_pe_enc.No_documento))
            cmd.Parameters.Add(New SqlParameter("@LOCAL", oBeTrans_pe_enc.Local))
            cmd.Parameters.Add(New SqlParameter("@PALLET_PRIMERO", oBeTrans_pe_enc.Pallet_primero))
            cmd.Parameters.Add(New SqlParameter("@DIAS_CLIENTE", oBeTrans_pe_enc.Dias_cliente))
            cmd.Parameters.Add(New SqlParameter("@ANULADO", oBeTrans_pe_enc.Anulado))
            cmd.Parameters.Add(New SqlParameter("@ROADKILOMETRAJE", oBeTrans_pe_enc.RoadKilometraje))
            cmd.Parameters.Add(New SqlParameter("@ROADFECHAENTR", oBeTrans_pe_enc.RoadFechaEntr))
            cmd.Parameters.Add(New SqlParameter("@ROADDIRENTREGA", oBeTrans_pe_enc.RoadDirEntrega))
            cmd.Parameters.Add(New SqlParameter("@ROADTOTAL", oBeTrans_pe_enc.RoadTotal))
            cmd.Parameters.Add(New SqlParameter("@ROADDESMONTO", oBeTrans_pe_enc.RoadDesMonto))
            cmd.Parameters.Add(New SqlParameter("@ROADIMPMONTO", oBeTrans_pe_enc.RoadImpMonto))
            cmd.Parameters.Add(New SqlParameter("@ROADPESO", oBeTrans_pe_enc.RoadPeso))
            cmd.Parameters.Add(New SqlParameter("@ROADBANDERA", oBeTrans_pe_enc.RoadBandera))
            cmd.Parameters.Add(New SqlParameter("@ROADSTATCOM", oBeTrans_pe_enc.RoadStatCom))
            cmd.Parameters.Add(New SqlParameter("@ROADCALCOBJ", oBeTrans_pe_enc.RoadCalcoBJ))
            cmd.Parameters.Add(New SqlParameter("@ROADIMPRES", oBeTrans_pe_enc.RoadImpres))
            cmd.Parameters.Add(New SqlParameter("@ROADADD1", oBeTrans_pe_enc.RoadADD1))
            cmd.Parameters.Add(New SqlParameter("@ROADADD2", oBeTrans_pe_enc.RoadADD2))
            cmd.Parameters.Add(New SqlParameter("@ROADADD3", oBeTrans_pe_enc.RoadADD3))
            cmd.Parameters.Add(New SqlParameter("@ROADSTATPROC", oBeTrans_pe_enc.RoadStatProc))
            cmd.Parameters.Add(New SqlParameter("@ROADRECHAZADO", oBeTrans_pe_enc.RoadRechazado))
            cmd.Parameters.Add(New SqlParameter("@ROADRAZON_RECHAZADO", oBeTrans_pe_enc.RoadRazon_Rechazado))
            cmd.Parameters.Add(New SqlParameter("@ROADINFORMADO", oBeTrans_pe_enc.RoadInformado))
            cmd.Parameters.Add(New SqlParameter("@ROADSUCURSAL", oBeTrans_pe_enc.RoadSucursal))
            cmd.Parameters.Add(New SqlParameter("@ROADIDDESPACHO", oBeTrans_pe_enc.RoadIdDespacho))
            cmd.Parameters.Add(New SqlParameter("@ROADIDFACTURACION", oBeTrans_pe_enc.RoadIdFacturacion))
            cmd.Parameters.Add(New SqlParameter("@ROADIDRUTA", oBeTrans_pe_enc.RoadIdRuta))
            cmd.Parameters.Add(New SqlParameter("@ROADIDVENDEDOR", oBeTrans_pe_enc.RoadIdVendedor))
            cmd.Parameters.Add(New SqlParameter("@ROADIDRUTADESPACHO", oBeTrans_pe_enc.RoadIdRutaDespacho))
            cmd.Parameters.Add(New SqlParameter("@ROADIDVENDEDORDESPACHO", oBeTrans_pe_enc.RoadIdVendedorDespacho))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACION", oBeTrans_pe_enc.Observacion))
            cmd.Parameters.Add(New SqlParameter("@PEDIDOROAD", oBeTrans_pe_enc.PedidoRoad))
            cmd.Parameters.Add(New SqlParameter("@HORAENTREGADESDE", oBeTrans_pe_enc.HoraEntregaDesde))
            cmd.Parameters.Add(New SqlParameter("@HORAENTREGAHASTA", oBeTrans_pe_enc.HoraEntregaHasta))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIA", oBeTrans_pe_enc.Referencia))
            cmd.Parameters.Add(New SqlParameter("@ENVIADO_A_ERP", oBeTrans_pe_enc.Enviado_A_ERP))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_ULTIMO_LOTE", oBeTrans_pe_enc.Control_Ultimo_Lote))
            cmd.Parameters.Add(New SqlParameter("@SERIE", oBeTrans_pe_enc.Serie))
            cmd.Parameters.Add(New SqlParameter("@CORRELATIVO", oBeTrans_pe_enc.Correlativo))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIA_DOCUMENTO_INGRESO_BODEGA_DESTINO", oBeTrans_pe_enc.Referencia_Documento_Ingreso_Bodega_Destino))
            cmd.Parameters.Add(New SqlParameter("@SYNC_MI3", oBeTrans_pe_enc.Sync_MI3))
            cmd.Parameters.Add(New SqlParameter("@NO_PICKING_ERP", oBeTrans_pe_enc.No_Picking_ERP))
            cmd.Parameters.Add(New SqlParameter("@NO_DOCUMENTO_EXTERNO", oBeTrans_pe_enc.No_Documento_Externo))
            cmd.Parameters.Add(New SqlParameter("@REQUIERE_TARIMAS", oBeTrans_pe_enc.Requiere_Tarimas))
            cmd.Parameters.Add(New SqlParameter("@FECHA_PREPARACION", oBeTrans_pe_enc.Fecha_Preparacion))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOMANUFACTURA", oBeTrans_pe_enc.IdTipoManufactura))
            cmd.Parameters.Add(New SqlParameter("@BODEGA_ORIGEN", oBeTrans_pe_enc.Bodega_Origen))
            cmd.Parameters.Add(New SqlParameter("@BODEGA_DESTINO", oBeTrans_pe_enc.Bodega_Destino))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_EMPRESA_ERP", oBeTrans_pe_enc.Codigo_Empresa_ERP))
            cmd.Parameters.Add(New SqlParameter("@ESEXPORTACION", oBeTrans_pe_enc.EsExportacion))

            If Not oBeTrans_pe_enc.IdMotivoDevolucion = 0 Then cmd.Parameters.Add(New SqlParameter("@IDMOTIVODEVOLUCION", oBeTrans_pe_enc.IdMotivoDevolucion))

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

    Public Shared Function Actualizar(ByRef oBeTrans_pe_enc As clsBeTrans_pe_enc,
                                      Optional ByVal pConection As SqlConnection = Nothing,
                                      Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("trans_pe_enc")
            Upd.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("idcliente", "@idcliente", DataType.Parametro)
            If Not oBeTrans_pe_enc.IdMuelle = 0 Then Upd.Add("idmuelle", "@idmuelle", DataType.Parametro)
            Upd.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Upd.Add("IdTipoPedido", "@IdTipoPedido", DataType.Parametro)
            Upd.Add("IdPickingEnc", "@IdPickingEnc", DataType.Parametro)
            Upd.Add("roadidruta", "@roadidruta", DataType.Parametro)
            Upd.Add("roadidvendedor", "@roadidvendedor", DataType.Parametro)
            Upd.Add("roadidrutadespacho", "@roadidrutadespacho", DataType.Parametro)
            Upd.Add("roadidvendedordespacho", "@roadidvendedordespacho", DataType.Parametro)
            Upd.Add("fecha_pedido", "@fecha_pedido", DataType.Parametro)
            Upd.Add("hora_ini", "@hora_ini", DataType.Parametro)
            Upd.Add("hora_fin", "@hora_fin", DataType.Parametro)
            Upd.Add("ubicacion", "@ubicacion", DataType.Parametro)
            Upd.Add("estado", "@estado", DataType.Parametro)
            Upd.Add("no_despacho", "@no_despacho", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("no_documento", "@no_documento", DataType.Parametro)
            Upd.Add("local", "@local", DataType.Parametro)
            Upd.Add("pallet_primero", "@pallet_primero", DataType.Parametro)
            Upd.Add("dias_cliente", "@dias_cliente", DataType.Parametro)
            Upd.Add("anulado", "@anulado", DataType.Parametro)
            Upd.Add("roadkilometraje", "@roadkilometraje", DataType.Parametro)
            Upd.Add("roadfechaentr", "@roadfechaentr", DataType.Parametro)
            Upd.Add("horaentregadesde", "@horaentregadesde", DataType.Parametro)
            Upd.Add("horaentregahasta", "@horaentregahasta", DataType.Parametro)
            Upd.Add("roaddirentrega", "@roaddirentrega", DataType.Parametro)
            Upd.Add("roadtotal", "@roadtotal", DataType.Parametro)
            Upd.Add("roaddesmonto", "@roaddesmonto", DataType.Parametro)
            Upd.Add("roadimpmonto", "@roadimpmonto", DataType.Parametro)
            Upd.Add("roadpeso", "@roadpeso", DataType.Parametro)
            Upd.Add("roadbandera", "@roadbandera", DataType.Parametro)
            Upd.Add("roadstatcom", "@roadstatcom", DataType.Parametro)
            Upd.Add("roadcalcobj", "@roadcalcobj", DataType.Parametro)
            Upd.Add("roadimpres", "@roadimpres", DataType.Parametro)
            Upd.Add("roadadd1", "@roadadd1", DataType.Parametro)
            Upd.Add("roadadd2", "@roadadd2", DataType.Parametro)
            Upd.Add("roadadd3", "@roadadd3", DataType.Parametro)
            Upd.Add("roadstatproc", "@roadstatproc", DataType.Parametro)
            Upd.Add("roadrechazado", "@roadrechazado", DataType.Parametro)
            Upd.Add("roadrazon_rechazado", "@roadrazon_rechazado", DataType.Parametro)
            Upd.Add("roadinformado", "@roadinformado", DataType.Parametro)
            Upd.Add("roadsucursal", "@roadsucursal", DataType.Parametro)
            Upd.Add("roadiddespacho", "@roadiddespacho", DataType.Parametro)
            Upd.Add("roadidfacturacion", "@roadidfacturacion", DataType.Parametro)
            Upd.Add("pedidoroad", "@pedidoroad", DataType.Parametro)
            Upd.Add("enviado_a_erp", "@enviado_a_erp", DataType.Parametro)
            Upd.Add("referencia", "@referencia", DataType.Parametro)
            Upd.Add("control_ultimo_lote", "@control_ultimo_lote", DataType.Parametro)
            Upd.Add("serie", "@serie", DataType.Parametro)
            Upd.Add("correlativo", "@correlativo", DataType.Parametro)
            Upd.Add("Referencia_Documento_Ingreso_Bodega_Destino", "@Referencia_Documento_Ingreso_Bodega_Destino", DataType.Parametro)
            Upd.Add("sync_mi3", "@sync_mi3", DataType.Parametro)
            Upd.Add("observacion", "@observacion", DataType.Parametro)
            Upd.Add("no_documento_externo", "@no_documento_externo", DataType.Parametro)
            Upd.Add("requiere_tarimas", "@requiere_tarimas", DataType.Parametro)
            Upd.Add("fecha_preparacion", "@fecha_preparacion", DataType.Parametro)
            Upd.Add("IdTipoManufactura", "@IdTipoManufactura", DataType.Parametro)
            Upd.Add("bodega_origen", "@bodega_origen", DataType.Parametro)
            Upd.Add("bodega_destino", "@bodega_destino", DataType.Parametro)
            Upd.Add("codigo_empresa_erp", "@codigo_empresa_erp", DataType.Parametro)
            If Not oBeTrans_pe_enc.IdMotivoDevolucion = 0 Then Upd.Add("IdMotivoDevolucion", "@IdMotivoDevolucion", DataType.Parametro)
            Upd.Add("EsExportacion", "@EsExportacion", DataType.Parametro)
            Upd.Where("IdPedidoEnc = @IdPedidoEnc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTrans_pe_enc.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_pe_enc.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDCLIENTE", oBeTrans_pe_enc.Cliente.IdCliente))
            If Not oBeTrans_pe_enc.IdMuelle = 0 Then cmd.Parameters.Add(New SqlParameter("@IDMUELLE", oBeTrans_pe_enc.IdMuelle))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_pe_enc.PropietarioBodega.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOPEDIDO", oBeTrans_pe_enc.TipoPedido.IdTipoPedido))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", oBeTrans_pe_enc.IdPickingEnc))
            cmd.Parameters.Add(New SqlParameter("@ROADIDRUTA", oBeTrans_pe_enc.RoadIdRuta))
            cmd.Parameters.Add(New SqlParameter("@ROADIDVENDEDOR", oBeTrans_pe_enc.RoadIdVendedor))
            cmd.Parameters.Add(New SqlParameter("@ROADIDRUTADESPACHO", oBeTrans_pe_enc.RoadIdRutaDespacho))
            cmd.Parameters.Add(New SqlParameter("@ROADIDVENDEDORDESPACHO", oBeTrans_pe_enc.RoadIdVendedorDespacho))
            cmd.Parameters.Add(New SqlParameter("@FECHA_PEDIDO", oBeTrans_pe_enc.Fecha_Pedido))
            cmd.Parameters.Add(New SqlParameter("@HORA_INI", oBeTrans_pe_enc.Hora_ini))
            cmd.Parameters.Add(New SqlParameter("@HORA_FIN", oBeTrans_pe_enc.Hora_fin))
            cmd.Parameters.Add(New SqlParameter("@UBICACION", oBeTrans_pe_enc.Ubicacion))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeTrans_pe_enc.Estado))
            cmd.Parameters.Add(New SqlParameter("@NO_DESPACHO", oBeTrans_pe_enc.No_despacho))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_pe_enc.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_pe_enc.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_pe_enc.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_pe_enc.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_pe_enc.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@NO_DOCUMENTO", oBeTrans_pe_enc.No_documento))
            cmd.Parameters.Add(New SqlParameter("@LOCAL", oBeTrans_pe_enc.Local))
            cmd.Parameters.Add(New SqlParameter("@PALLET_PRIMERO", oBeTrans_pe_enc.Pallet_primero))
            cmd.Parameters.Add(New SqlParameter("@DIAS_CLIENTE", oBeTrans_pe_enc.Dias_cliente))
            cmd.Parameters.Add(New SqlParameter("@ANULADO", oBeTrans_pe_enc.Anulado))
            cmd.Parameters.Add(New SqlParameter("@ROADKILOMETRAJE", oBeTrans_pe_enc.RoadKilometraje))
            cmd.Parameters.Add(New SqlParameter("@ROADFECHAENTR", oBeTrans_pe_enc.RoadFechaEntr))
            cmd.Parameters.Add(New SqlParameter("@ROADDIRENTREGA", oBeTrans_pe_enc.RoadDirEntrega))
            cmd.Parameters.Add(New SqlParameter("@HORAENTREGADESDE", oBeTrans_pe_enc.HoraEntregaDesde))
            cmd.Parameters.Add(New SqlParameter("@HORAENTREGAHASTA", oBeTrans_pe_enc.HoraEntregaHasta))
            cmd.Parameters.Add(New SqlParameter("@ROADTOTAL", oBeTrans_pe_enc.RoadTotal))
            cmd.Parameters.Add(New SqlParameter("@ROADDESMONTO", oBeTrans_pe_enc.RoadDesMonto))
            cmd.Parameters.Add(New SqlParameter("@ROADIMPMONTO", oBeTrans_pe_enc.RoadImpMonto))
            cmd.Parameters.Add(New SqlParameter("@ROADPESO", oBeTrans_pe_enc.RoadPeso))
            cmd.Parameters.Add(New SqlParameter("@ROADBANDERA", oBeTrans_pe_enc.RoadBandera))
            cmd.Parameters.Add(New SqlParameter("@ROADSTATCOM", oBeTrans_pe_enc.RoadStatCom))
            cmd.Parameters.Add(New SqlParameter("@ROADCALCOBJ", oBeTrans_pe_enc.RoadCalcoBJ))
            cmd.Parameters.Add(New SqlParameter("@ROADIMPRES", oBeTrans_pe_enc.RoadImpres))
            cmd.Parameters.Add(New SqlParameter("@ROADADD1", oBeTrans_pe_enc.RoadADD1))
            cmd.Parameters.Add(New SqlParameter("@ROADADD2", oBeTrans_pe_enc.RoadADD2))
            cmd.Parameters.Add(New SqlParameter("@ROADADD3", oBeTrans_pe_enc.RoadADD3))
            cmd.Parameters.Add(New SqlParameter("@ROADSTATPROC", oBeTrans_pe_enc.RoadStatProc))
            cmd.Parameters.Add(New SqlParameter("@ROADRECHAZADO", oBeTrans_pe_enc.RoadRechazado))
            cmd.Parameters.Add(New SqlParameter("@ROADRAZON_RECHAZADO", oBeTrans_pe_enc.RoadRazon_Rechazado))
            cmd.Parameters.Add(New SqlParameter("@ROADINFORMADO", oBeTrans_pe_enc.RoadInformado))
            cmd.Parameters.Add(New SqlParameter("@ROADSUCURSAL", oBeTrans_pe_enc.RoadSucursal))
            cmd.Parameters.Add(New SqlParameter("@ROADIDDESPACHO", oBeTrans_pe_enc.RoadIdDespacho))
            cmd.Parameters.Add(New SqlParameter("@ROADIDFACTURACION", oBeTrans_pe_enc.RoadIdFacturacion))
            cmd.Parameters.Add(New SqlParameter("@PEDIDOROAD", oBeTrans_pe_enc.PedidoRoad))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIA", oBeTrans_pe_enc.Referencia))
            cmd.Parameters.Add(New SqlParameter("@ENVIADO_A_ERP", oBeTrans_pe_enc.Enviado_A_ERP))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_ULTIMO_LOTE", oBeTrans_pe_enc.Control_Ultimo_Lote))
            cmd.Parameters.Add(New SqlParameter("@SERIE", oBeTrans_pe_enc.Serie))
            cmd.Parameters.Add(New SqlParameter("@CORRELATIVO", oBeTrans_pe_enc.Correlativo))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIA_DOCUMENTO_INGRESO_BODEGA_DESTINO", oBeTrans_pe_enc.Referencia_Documento_Ingreso_Bodega_Destino))
            cmd.Parameters.Add(New SqlParameter("@SYNC_MI3", oBeTrans_pe_enc.Sync_MI3))
            cmd.Parameters.Add(New SqlParameter("@NO_PICKING_ERP", oBeTrans_pe_enc.No_Picking_ERP))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACION", oBeTrans_pe_enc.Observacion))
            cmd.Parameters.Add(New SqlParameter("@NO_DOCUMENTO_EXTERNO", oBeTrans_pe_enc.No_Documento_Externo))
            cmd.Parameters.Add(New SqlParameter("@REQUIERE_TARIMAS", oBeTrans_pe_enc.Requiere_Tarimas))
            cmd.Parameters.Add(New SqlParameter("@FECHA_PREPARACION", oBeTrans_pe_enc.Fecha_Preparacion))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOMANUFACTURA", oBeTrans_pe_enc.IdTipoManufactura))
            cmd.Parameters.Add(New SqlParameter("@BODEGA_ORIGEN", oBeTrans_pe_enc.Bodega_Origen))
            cmd.Parameters.Add(New SqlParameter("@BODEGA_DESTINO", oBeTrans_pe_enc.Bodega_Destino))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_EMPRESA_ERP", oBeTrans_pe_enc.Codigo_Empresa_ERP))
            If Not oBeTrans_pe_enc.IdMotivoDevolucion = 0 Then cmd.Parameters.Add(New SqlParameter("@IDMOTIVODEVOLUCION", oBeTrans_pe_enc.IdMotivoDevolucion))
            cmd.Parameters.Add(New SqlParameter("@ESEXPORTACION", oBeTrans_pe_enc.EsExportacion))

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

    Public Function Eliminar(ByRef oBeTrans_pe_enc As clsBeTrans_pe_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text

            Dim sp As String = " Delete from Trans_pe_enc " &
             "  Where(IdPedidoEnc = @IdPedidoEnc)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTrans_pe_enc.IdPedidoEnc))

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

    Public Function Obtener(ByRef oBeTrans_pe_enc As clsBeTrans_pe_enc) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Obtener = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim sp As String = "SELECT * FROM Trans_pe_enc 
                                Where(IdPedidoEnc = @IdPedidoEnc)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTrans_pe_enc.IdPedidoEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_pe_enc, dt.Rows(0))
                Obtener = True
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

End Class
