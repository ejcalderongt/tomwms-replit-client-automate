Public Class clsBeTrans_ubic_hh_stock
    Implements ICloneable

    Public Property IdStockTransUbicHHDet() As Integer = 0
    Public Property IdTareaUbicacionEnc() As Integer = 0
    Public Property IdTareaUbicacionDet() As Integer = 0
    Public Property IdStock() As Integer = 0
    Public Property IdPropietarioBodega() As Integer = 0
    Public Property IdProductoBodega() As Integer = 0
    Public Property IdProductoEstado() As Integer = 0
    Public Property IdPresentacion() As Integer = 0
    Public Property IdUnidadMedida() As Integer = 0
    Public Property IdUbicacion() As Integer = 0
    Public Property IdUbicacion_anterior() As Integer = 0
    Public Property IdRecepcionEnc() As Integer = 0
    Public Property IdRecepcionDet() As Integer = 0
    Public Property IdPedidoEnc() As Integer = 0
    Public Property IdPickingEnc() As Integer = 0
    Public Property IdDespachoEnc() As Integer = 0
    Public Property Lote() As String = ""
    Public Property Lic_plate() As String = ""
    Public Property Serial() As String = ""
    Public Property Cantidad() As Double = 0.0
    Public Property Fecha_ingreso() As Date = Date.Now
    Public Property Fecha_vence() As Date = Date.Now
    Public Property Uds_lic_plate() As Double = 0
    Public Property No_bulto() As Integer = 0
    Public Property Fecha_manufactura() As Date = Date.Now
    Public Property añada() As Integer = 0
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Activo() As Boolean = False
    Public Property Peso() As Double = 0.0
    Public Property Temperatura() As Double = 0.0
    Public Property Fecha_mov_hist() As Date = Date.Now
    Public Property Atributo_variante_1() As String = ""
    Public Property IdProductoTallaColor() As Integer = 0
    Public Property Talla() As String = String.Empty
    Public Property Color() As String = String.Empty

    Sub New()
    End Sub

    Sub New(ByRef IdStockTransUbicHHDet As Integer, ByVal IdTareaUbicacionEnc As Integer, ByVal IdTareaUbicacionDet As Integer, ByVal IdStock As Integer, ByVal IdPropietarioBodega As Integer, ByVal IdProductoBodega As Integer, ByVal IdProductoEstado As Integer, ByVal IdPresentacion As Integer, ByVal IdUnidadMedida As Integer, ByVal IdUbicacion As Integer, ByVal IdUbicacion_anterior As Integer, ByVal IdRecepcionEnc As Integer, ByVal IdRecepcionDet As Integer, ByVal IdPedidoEnc As Integer, ByVal IdPickingEnc As Integer, ByVal IdDespachoEnc As Integer, ByVal lote As String, ByVal lic_plate As String, ByVal serial As String, ByVal cantidad As Double, ByVal fecha_ingreso As Date, ByVal fecha_vence As Date, ByVal uds_lic_plate As String, ByVal no_bulto As String, ByVal fecha_manufactura As Date, ByVal añada As Integer, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal activo As Boolean, ByVal peso As Double, ByVal temperatura As Double, ByVal fecha_mov_hist As Date, ByVal atributo_variante_1 As String)
        Me.IdStockTransUbicHHDet = IdStockTransUbicHHDet
        Me.IdTareaUbicacionEnc = IdTareaUbicacionEnc
        Me.IdTareaUbicacionDet = IdTareaUbicacionDet
        Me.IdStock = IdStock
        Me.IdPropietarioBodega = IdPropietarioBodega
        Me.IdProductoBodega = IdProductoBodega
        Me.IdProductoEstado = IdProductoEstado
        Me.IdPresentacion = IdPresentacion
        Me.IdUnidadMedida = IdUnidadMedida
        Me.IdUbicacion = IdUbicacion
        Me.IdUbicacion_anterior = IdUbicacion_anterior
        Me.IdRecepcionEnc = IdRecepcionEnc
        Me.IdRecepcionDet = IdRecepcionDet
        Me.IdPedidoEnc = IdPedidoEnc
        Me.IdPickingEnc = IdPickingEnc
        Me.IdDespachoEnc = IdDespachoEnc
        Me.Lote = lote
        Me.Lic_plate = lic_plate
        Me.Serial = serial
        Me.Cantidad = cantidad
        Me.Fecha_ingreso = fecha_ingreso
        Me.Fecha_vence = fecha_vence
        Me.Uds_lic_plate = uds_lic_plate
        Me.No_bulto = no_bulto
        Me.Fecha_manufactura = fecha_manufactura
        Me.añada = añada
        Me.User_agr = user_agr
        Me.Fec_agr = fec_agr
        Me.User_mod = user_mod
        Me.Fec_mod = fec_mod
        Me.Activo = activo
        Me.Peso = peso
        Me.Temperatura = temperatura
        Me.Fecha_mov_hist = fecha_mov_hist
        Me.Atributo_variante_1 = atributo_variante_1
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
