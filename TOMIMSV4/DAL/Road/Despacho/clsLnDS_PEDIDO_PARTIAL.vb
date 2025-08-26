Imports System.Data.SqlClient

Partial Public Class clsLnDS_PEDIDO

    Public Shared Function Enviar_Despacho_Desde_WMS(ByVal BeDespachoEnc As clsBeTrans_despacho_enc) As Boolean

        Enviar_Despacho_Desde_WMS = False

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ROAD"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim DS_PedidoEnc As New clsBeDS_PEDIDO
            Dim DS_PedidoDet As New clsBeDS_PEDIDOD
            Dim BeRutaRoad As New clsBeRoad_ruta
            Dim BeVendedorRoad As New clsBeRoad_p_vendedor
            Dim vTotalDocumento As Double = 0
            Dim vCorel As String = "MI3" & Now.Year & Right("00" & Now.Month, 2) & Right("00" & Now.Day, 2) & Right("00" & Now.Hour, 2) & Right("00" & Now.Minute, 2) & Right("00" & Now.Second, 2)

            For Each P In BeDespachoEnc.ListaPedidos

                BeRutaRoad = clsLnRoad_ruta.GetSingle(P.RoadIdRuta)
                BeVendedorRoad = clsLnRoad_p_vendedor.GetSingle(P.RoadIdVendedor)

                'For Each D In P.Detalle
                '    vTotalDocumento += Math.Round(D.Cantidad * D.Precio, 6)
                'Next

                vTotalDocumento = P.Detalle.Sum(Function(x) x.Cantidad * x.Precio)

                DS_PedidoEnc.COREL = vCorel
                DS_PedidoEnc.ANULADO = "N"
                DS_PedidoEnc.FECHA = Now
                DS_PedidoEnc.EMPRESA = "03"
                DS_PedidoEnc.RUTA = BeRutaRoad.CODIGO
                DS_PedidoEnc.VENDEDOR = BeVendedorRoad.Codigo
                DS_PedidoEnc.CLIENTE = P.Cliente.Codigo
                DS_PedidoEnc.KILOMETRAJE = 0
                DS_PedidoEnc.FECHAENTR = P.RoadFechaEntr
                DS_PedidoEnc.DIRENTREGA = P.RoadDirEntrega
                DS_PedidoEnc.TOTAL = IIf(P.RoadTotal = 0, vTotalDocumento, P.RoadTotal)
                DS_PedidoEnc.DESMONTO = 0
                DS_PedidoEnc.IMPMONTO = 0
                DS_PedidoEnc.PESO = P.RoadPeso
                DS_PedidoEnc.BANDERA = "N"
                DS_PedidoEnc.STATCOM = "N"
                DS_PedidoEnc.CALCOBJ = "N"
                DS_PedidoEnc.IMPRES = "0"
                DS_PedidoEnc.ADD1 = "4"
                DS_PedidoEnc.ADD2 = ""
                DS_PedidoEnc.ADD3 = ""

                Insertar(DS_PedidoEnc, lConnection, lTransaction)

                For Each D In P.Detalle

                    DS_PedidoDet = New clsBeDS_PEDIDOD()
                    DS_PedidoDet.COREL = vCorel
                    DS_PedidoDet.PRODUCTO = D.Codigo_Producto
                    DS_PedidoDet.EMPRESA = "03"
                    DS_PedidoDet.ANULADO = "N"
                    DS_PedidoDet.CANT = D.Cantidad
                    DS_PedidoDet.PRECIO = D.Precio
                    DS_PedidoDet.IMP = 0
                    DS_PedidoDet.DESMON = 0
                    DS_PedidoDet.TOTAL = Math.Round(D.Cantidad * D.Precio, 2)
                    DS_PedidoDet.PRECIODOC = Math.Round(D.Cantidad * D.Precio, 6)
                    DS_PedidoDet.PESO = D.Peso
                    DS_PedidoDet.VAL1 = 0
                    DS_PedidoDet.VAL2 = ""
                    DS_PedidoDet.Ruta = DS_PedidoEnc.RUTA
                    clsLnDS_PEDIDOD.Insertar(DS_PedidoDet, lConnection, lTransaction)

                Next

            Next

            lTransaction.Commit()

            Enviar_Despacho_Desde_WMS = True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

End Class
