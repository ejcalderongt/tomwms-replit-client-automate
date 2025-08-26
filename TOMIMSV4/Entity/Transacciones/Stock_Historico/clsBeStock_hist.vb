Public Class clsBeStock_hist
    Implements ICloneable

    Public Property IdStockHist() As Integer = 0
    Public Property IdStock() As Integer = 0
    Public Property IdNuevoStock() As Integer = 0
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
    Public Property Uds_lic_plate() As Double = 0.0
    Public Property No_bulto() As Integer = 0
    Public Property Fecha_manufactura() As Date = Date.Now
    Public Property Añada() As Integer = 0
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Activo() As Boolean = False
    Public Property Peso() As Double = 0.0
    Public Property Temperatura() As Double = 0.0
    Public Property Posiciones() As Integer = 0

    Sub New()

    End Sub

    Sub New(ByRef IdStockHist As Integer, ByVal IdStock As Integer, ByVal IdNuevoStock As Integer, ByVal IdPropietarioBodega As Integer, ByVal IdProductoBodega As Integer, ByVal IdProductoEstado As Integer, ByVal IdPresentacion As Integer, ByVal IdUnidadMedida As Integer, ByVal IdUbicacion As Integer, ByVal IdUbicacion_anterior As Integer, ByVal IdRecepcionEnc As Integer, ByVal IdRecepcionDet As Integer, ByVal IdPedidoEnc As Integer, ByVal IdPickingEnc As Integer, ByVal IdDespachoEnc As Integer, ByVal lote As String, ByVal lic_plate As String, ByVal serial As String, ByVal cantidad As Double, ByVal fecha_ingreso As Date, ByVal fecha_vence As Date, ByVal uds_lic_plate As Double, ByVal no_bulto As String, ByVal fecha_manufactura As Date, ByVal añada As Integer, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal activo As Boolean, ByVal peso As Double, ByVal temperatura As Double)
        Me.IdStockHist = IdStockHist
        Me.IdStock = IdStock
        Me.IdNuevoStock = IdNuevoStock
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
        Me.Lote = Lote
        Me.Lic_plate = Lic_plate
        Me.Serial = Serial
        Me.Cantidad = Cantidad
        Me.Fecha_ingreso = Fecha_ingreso
        Me.Fecha_vence = Fecha_vence
        Me.Uds_lic_plate = Uds_lic_plate
        Me.No_bulto = No_bulto
        Me.Fecha_manufactura = Fecha_manufactura
        Me.Añada = Añada
        Me.User_agr = User_agr
        Me.Fec_agr = Fec_agr
        Me.User_mod = User_mod
        Me.Fec_mod = Fec_mod
        Me.Activo = Activo
        Me.Peso = Peso
        Me.Temperatura = Temperatura
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
