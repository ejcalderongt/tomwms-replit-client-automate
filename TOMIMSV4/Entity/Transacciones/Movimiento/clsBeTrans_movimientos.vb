Public Class clsBeTrans_movimientos
    Implements ICloneable
    Implements IDisposable

    Public Property IdMovimiento() As Integer = 0
    Public Property IdEmpresa() As Integer = 0
    Public Property IdBodegaOrigen() As Integer = 0
    Public Property IdTransaccion() As Integer = 0
    Public Property IdPropietarioBodega() As Integer = 0
    Public Property IdProductoBodega() As Integer = 0
    Public Property IdUbicacionOrigen() As Integer = 0
    Public Property IdUbicacionDestino() As Integer = 0
    Public Property IdPresentacion() As Integer = 0
    Public Property IdEstadoOrigen() As Integer = 0
    Public Property IdEstadoDestino() As Integer = 0
    Public Property IdUnidadMedida() As Integer = 0
    Public Property IdTipoTarea() As Integer = 0
    Public Property IdBodegaDestino() As Integer = 0
    Public Property IdRecepcion() As Integer = 0
    Public Property Cantidad() As Double = 0.0
    Public Property Serie() As String = ""
    Public Property Peso() As Double = 0.0
    Public Property Lote() As String = ""
    Public Property Fecha_vence() As Date = Date.Now
    Public Property Fecha() As Date = Date.Now
    Public Property Barra_pallet() As String = ""
    Public Property Hora_ini() As Date = Date.Now
    Public Property Hora_fin() As Date = Date.Now
    Public Property Fecha_agr() As Date = Date.Now
    Public Property Usuario_agr() As String = ""
    Public Property Cantidad_hist() As Double = 0.0
    Public Property Peso_hist() As Double = 0.0
    Public Property Lic_plate() As String = ""

    ''' <summary>
    ''' #EJC202302211347: Asociar detalle de recepci�n
    ''' </summary>
    ''' <returns></returns>
    Public Property IdRecepcionDet() As Integer = 0
    ''' <summary>
    ''' #EJC202302211347: Asociar IdPedidoEnc en despacho.
    ''' </summary>
    ''' <returns></returns>
    Public Property IdPedidoEnc() As Integer = 0
    ''' <summary>
    ''' #EJC202302211347: Asociar IdPedidoDet en despacho.
    ''' </summary>
    ''' <returns></returns>
    Public Property IdPedidoDet() As Integer = 0
    ''' <summary>
    ''' #EJC202302211347: Asociar IdDespachoEnc en despacho.
    ''' </summary>
    ''' <returns></returns>
    Public Property IdDespachoEnc() As Integer = 0
    ''' <summary>
    ''' #EJC202302211347: Asociar IdDespachoDet en despacho.
    ''' </summary>
    ''' <returns></returns>
    Public Property IdDespachoDet() As Integer = 0
    Public Property IdProductoTallaColor() As Integer = 0
    Public Property Talla() As String = ""
    Public Property Color() As String = ""

    Sub New()
    End Sub

    Sub New(ByRef IdMovimiento As Integer, ByVal IdEmpresa As Integer, ByVal IdBodegaOrigen As Integer, ByVal IdTransaccion As Integer, ByVal IdPropietarioBodega As Integer, ByVal IdProductoBodega As Integer, ByVal IdUbicacionOrigen As Integer, ByVal IdUbicacionDestino As Integer, ByVal IdPresentacion As Integer, ByVal IdEstadoOrigen As Integer, ByVal IdEstadoDestino As Integer, ByVal IdUnidadMedida As Integer, ByVal IdTipoTarea As Integer, ByVal IdBodegaDestino As Integer, ByVal IdRecepcion As Integer, ByVal cantidad As Double, ByVal serie As String, ByVal peso As Double, ByVal lote As String, ByVal fecha_vence As Date, ByVal fecha As Date, ByVal barra_pallet As String, ByVal hora_ini As Date, ByVal hora_fin As Date, ByVal fecha_agr As Date, ByVal usuario_agr As String, ByVal cantidad_hist As Double, ByVal peso_hist As Double)
        Me.IdMovimiento = IdMovimiento
        Me.IdEmpresa = IdEmpresa
        Me.IdBodegaOrigen = IdBodegaOrigen
        Me.IdTransaccion = IdTransaccion
        Me.IdPropietarioBodega = IdPropietarioBodega
        Me.IdProductoBodega = IdProductoBodega
        Me.IdUbicacionOrigen = IdUbicacionOrigen
        Me.IdUbicacionDestino = IdUbicacionDestino
        Me.IdPresentacion = IdPresentacion
        Me.IdEstadoOrigen = IdEstadoOrigen
        Me.IdEstadoDestino = IdEstadoDestino
        Me.IdUnidadMedida = IdUnidadMedida
        Me.IdTipoTarea = IdTipoTarea
        Me.IdBodegaDestino = IdBodegaDestino
        Me.IdRecepcion = IdRecepcion
        Me.Cantidad = cantidad
        Me.Serie = serie
        Me.Peso = peso
        Me.Lote = lote
        Me.Fecha_vence = fecha_vence
        Me.Fecha = fecha
        Me.Barra_pallet = barra_pallet
        Me.Hora_ini = hora_ini
        Me.Hora_fin = hora_fin
        Me.Fecha_agr = fecha_agr
        Me.Usuario_agr = usuario_agr
        Me.Cantidad_hist = cantidad_hist
        Me.Peso_hist = peso_hist
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
