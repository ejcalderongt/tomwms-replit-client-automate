Public Class clsBeTrans_pe_det
    Implements ICloneable
    Implements IDisposable

    Public Property IdPedidoDet() As Integer = 0
    Public Property IdPedidoEnc() As Integer = 0
    Public Property IdProductoBodega() As Integer = 0
    Public Property ProductoBodega As New clsBeProducto_bodega
    Public Property IdEstado() As Integer = 0
    Public Property IdPresentacion() As Integer = 0
    Public Property IdUnidadMedidaBasica() As Integer = 0
    Public Property Cantidad() As Double = 0.0
    Public Property Peso() As Double = 0.0
    Public Property Precio() As Double = 0.0
    Public Property No_recepcion() As Integer = 0
    Public Property Ndias() As Integer = 0
    Public Property Cant_despachada() As Double = 0.0
    Public Property Peso_despachado() As Double = 0.0
    Public Property Nombre_producto() As String = ""
    Public Property Nom_presentacion() As String = ""
    Public Property Nom_unid_med() As String = ""
    Public Property Nom_estado() As String = ""
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property Fecha_especifica() As Boolean = False
    Public Property RoadDes() As Double = 0.0
    Public Property RoadDesMon() As Double = 0.0
    Public Property RoadTotal() As Double = 0.0
    Public Property RoadPrecioDoc() As Double = 0.0
    Public Property RoadVAL1() As Double = 0.0
    Public Property RoadVAL2() As String = ""
    Public Property RoadCantProc() As Double = 0.0
    Public Property No_linea As Integer = 0
    Public Property Atributo_Variante_1 As String = ""
    Public Property IdStockEspecifico As Integer = 0
    Public Property EsPadre As Boolean = False
    Public Property IdPedidoDetPadre As Integer = 0
    Public Property Peso_Bruto As Double = 0
    Public Property Peso_Neto As Double = 0
    Public Property Costo As Double = 0
    Public Property valor_aduana As Double = 0
    Public Property valor_fob As Double = 0
    Public Property valor_iva As Double = 0
    Public Property valor_dai As Double = 0
    Public Property valor_seguro As Double = 0
    Public Property valor_flete As Double = 0
    Public Property Total_linea As Double = 0

    ''' <summary>
    ''' #EJC20220307: Cliente en detalle para BYB.
    ''' </summary>
    ''' <returns></returns>
    Public Property IdCliente As Integer = 0

    Sub New()
    End Sub

    Sub New(ByRef IdPedidoDet As Integer, ByVal IdPedidoEnc As Integer, ByVal IdProductoBodega As Integer, ByVal IdEstado As Integer, ByVal IdPresentacion As Integer, ByVal IdUnidadMedidaBasica As Integer, ByVal Cantidad As Double, ByVal Peso As Double, ByVal Precio As Double, ByVal no_recepcion As Integer, ByVal ndias As Integer, ByVal cant_despachada As Double, ByVal nombre_producto As String, ByVal nom_presentacion As String, ByVal nom_unid_med As String, ByVal nom_estado As String, ByVal user_agr As String, ByVal fec_agr As Date, ByVal fecha_especifica As Boolean, ByVal RoadDes As Double, ByVal RoadDesMon As Double, ByVal RoadTotal As Double, ByVal RoadPrecioDoc As Double, ByVal RoadVAL1 As Double, ByVal RoadVAL2 As String, ByVal RoadCantProc As Double, ByVal IdStockEspecifico As Integer, ByVal EsKit As Boolean, ByVal IdPedidoDetPadre As Integer)
        Me.IdPedidoDet = IdPedidoDet
        Me.IdPedidoEnc = IdPedidoEnc
        Me.IdEstado = IdEstado
        Me.IdPresentacion = IdPresentacion
        Me.IdUnidadMedidaBasica = IdUnidadMedidaBasica
        Me.Cantidad = Cantidad
        Me.Peso = Peso
        Me.Precio = Precio
        Me.No_recepcion = no_recepcion
        Me.Ndias = ndias
        Me.Cant_despachada = cant_despachada
        Me.Nombre_producto = nombre_producto
        Me.Nom_presentacion = nom_presentacion
        Me.Nom_unid_med = nom_unid_med
        Me.Nom_estado = nom_estado
        Me.User_agr = user_agr
        Me.Fec_agr = fec_agr
        Me.Fecha_especifica = fecha_especifica
        Me.RoadDes = RoadDes
        Me.RoadDesMon = RoadDesMon
        Me.RoadTotal = RoadTotal
        Me.RoadPrecioDoc = RoadPrecioDoc
        Me.RoadVAL1 = RoadVAL1
        Me.RoadVAL2 = RoadVAL2
        Me.RoadCantProc = RoadCantProc
        Me.IdProductoBodega = IdProductoBodega
        Me.IdStockEspecifico = IdStockEspecifico
        Me.EsPadre = EsPadre
        Me.IdPedidoDetPadre = IdPedidoDetPadre
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
