Public Class clsBeTrans_pe_enc
    Implements ICloneable
    Implements IDisposable

    Public Property IdPedidoEnc() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property IdCliente() As Integer = 0
    Public Property IdMuelle() As Integer = 0
    Public Property IdTipoPedido() As Integer = 0
    Public Property IdPropietarioBodega() As Integer = 0
    Public Property IdPickingEnc As Integer = 0
    Public Property Fecha_Pedido() As Date = Date.Now
    Public Property Hora_ini() As Date = Date.Now
    Public Property Hora_fin() As Date = Date.Now
    Public Property Ubicacion() As String = ""
    Public Property Estado() As String = ""
    Public Property No_despacho() As Integer = 0
    Public Property Activo() As Boolean = False
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property No_documento() As Integer = 0
    Public Property Local() As Boolean = False
    Public Property Pallet_primero() As Boolean = False
    Public Property Dias_cliente() As Double = 0.0
    Public Property Anulado() As Boolean = False
    Public Property RoadKilometraje() As Double = 0.0
    Public Property RoadFechaEntr() As Date = Date.Now
    Public Property RoadDirEntrega() As String = ""
    Public Property RoadTotal() As Double = 0.0
    Public Property RoadDesMonto() As Double = 0.0
    Public Property RoadImpMonto() As Double = 0.0
    Public Property RoadPeso() As Double = 0.0
    Public Property RoadBandera() As String = ""
    Public Property RoadStatCom() As String = ""
    Public Property RoadCalcoBJ() As String = ""
    Public Property RoadImpres() As Integer = 0
    Public Property RoadADD1() As String = ""
    Public Property RoadADD2() As String = ""
    Public Property RoadADD3() As String = ""
    Public Property RoadStatProc() As String = ""
    Public Property RoadRechazado() As Boolean = False
    Public Property RoadRazon_Rechazado() As String = ""
    Public Property RoadInformado() As Boolean = False
    Public Property RoadSucursal() As String = ""
    Public Property RoadIdDespacho() As Integer = 0
    Public Property RoadIdFacturacion() As Integer = 0
    Public Property RoadIdRuta() As Integer = 0
    Public Property RoadIdVendedor() As Integer = 0
    Public Property RoadIdRutaDespacho() As Integer = 0
    Public Property RoadIdVendedorDespacho() As Integer = 0
    Public Property Observacion() As String = ""
    Public Property PedidoRoad() As Boolean = False
    Public Property HoraEntregaDesde() As Date = Date.Now
    Public Property HoraEntregaHasta() As Date = Date.Now
    Public Property Referencia() As String = ""
    Public Property Enviado_A_ERP As Boolean = False
    Public Property Referencia_Documento_Ingreso_Bodega_Destino As String = ""
    Public Property Sync_MI3 As Boolean = True
    Public Property No_Picking_ERP As String = ""
    Public Property No_Documento_Externo As String = ""
    Public Property NombreRutaDespacho As String = ""
    Public Property Requiere_Tarimas As Boolean = False
    Public Property Fecha_Preparacion() As Date = Date.Now
    Public Property Bodega_Origen() As String = ""
    Public Property Bodega_Destino() As String = ""
    Public Property IdAcuerdoComercial As Integer = 0
    Public Property IdMotivoDevolucion As Integer = 0
    Public Property Codigo_Empresa_ERP As String = ""
    Public Property EsExportacion As Boolean = False
    Public Property Guia_Transporte As String = ""
    '#GT20052025: campo en bd, pero no existe en clase ni en git
    Public Property IdMotivoAnulacionBodega As Integer = 0
    Public Property IdEmpresaTransporte As Integer = 0
    Public Property IdPiloto As Integer = 0

    Sub New()
    End Sub

    Sub New(ByRef IdPedidoEnc As Integer, ByVal IdBodega As Integer, ByVal IdCliente As Integer, ByVal IdMuelle As Integer, ByVal IdPropietarioBodega As Integer, ByVal Fecha_Pedido As Date, ByVal hora_ini As Date, ByVal hora_fin As Date, ByVal ubicacion As String, ByVal estado As String, ByVal no_despacho As Integer, ByVal activo As Boolean, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal no_documento As Integer, ByVal local As Boolean, ByVal pallet_primero As Boolean, ByVal dias_cliente As Double, ByVal anulado As Boolean, ByVal RoadKilometraje As Double, ByVal RoadFechaEntr As Date, ByVal RoadDirEntrega As String, ByVal RoadTotal As Double, ByVal RoadDesMonto As Double, ByVal RoadImpMonto As Double, ByVal RoadPeso As Double, ByVal RoadBandera As String, ByVal RoadStatCom As String, ByVal RoadCalcoBJ As String, ByVal RoadImpres As Integer, ByVal RoadADD1 As String, ByVal RoadADD2 As String, ByVal RoadADD3 As String, ByVal RoadStatProc As String, ByVal RoadRechazado As Boolean, ByVal RoadRazon_Rechazado As String, ByVal RoadInformado As Boolean, ByVal RoadSucursal As String, ByVal RoadIdDespacho As Integer, ByVal RoadIdFacturacion As Integer, ByVal RoadIdRuta As Integer, ByVal RoadIdVendedor As Integer, ByVal RoadIdRutaDespacho As Integer, ByVal RoadIdVendedorDespacho As Integer, ByVal Observacion As String, ByVal PedidoRoad As Boolean, ByVal HoraEntregaDesde As Date, ByVal HoraEntregaHasta As Date, ByVal Referencia As String)
        Me.IdPedidoEnc = IdPedidoEnc
        Me.IdBodega = IdBodega
        Me.IdCliente = IdCliente
        Me.IdMuelle = IdMuelle
        Me.IdPropietarioBodega = IdPropietarioBodega
        Me.Fecha_Pedido = Fecha_Pedido
        Me.Hora_ini = hora_ini
        Me.Hora_fin = hora_fin
        Me.Ubicacion = ubicacion
        Me.Estado = estado
        Me.No_despacho = no_despacho
        Me.Activo = activo
        Me.User_agr = user_agr
        Me.Fec_agr = fec_agr
        Me.User_mod = user_mod
        Me.Fec_mod = fec_mod
        Me.No_documento = no_documento
        Me.Local = local
        Me.Pallet_primero = pallet_primero
        Me.Dias_cliente = dias_cliente
        Me.Anulado = anulado
        Me.RoadKilometraje = RoadKilometraje
        Me.RoadFechaEntr = RoadFechaEntr
        Me.RoadDirEntrega = RoadDirEntrega
        Me.RoadTotal = RoadTotal
        Me.RoadDesMonto = RoadDesMonto
        Me.RoadImpMonto = RoadImpMonto
        Me.RoadPeso = RoadPeso
        Me.RoadBandera = RoadBandera
        Me.RoadStatCom = RoadStatCom
        Me.RoadCalcoBJ = RoadCalcoBJ
        Me.RoadImpres = RoadImpres
        Me.RoadADD1 = RoadADD1
        Me.RoadADD2 = RoadADD2
        Me.RoadADD3 = RoadADD3
        Me.RoadStatProc = RoadStatProc
        Me.RoadRechazado = RoadRechazado
        Me.RoadRazon_Rechazado = RoadRazon_Rechazado
        Me.RoadInformado = RoadInformado
        Me.RoadSucursal = RoadSucursal
        Me.RoadIdDespacho = RoadIdDespacho
        Me.RoadIdFacturacion = RoadIdFacturacion
        Me.RoadIdRuta = RoadIdRuta
        Me.RoadIdVendedor = RoadIdVendedor
        Me.RoadIdRutaDespacho = RoadIdRutaDespacho
        Me.RoadIdVendedorDespacho = RoadIdVendedorDespacho
        Me.Observacion = Observacion
        Me.PedidoRoad = PedidoRoad
        Me.HoraEntregaDesde = HoraEntregaDesde
        Me.HoraEntregaHasta = HoraEntregaHasta
        Me.Referencia = Referencia
        TipoPedido = New clsBeTrans_pe_tipo

    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class