Public Class clsBeTrans_picking_ubic_stock
    Implements ICloneable

    Public Property IdPickingUbicStock() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property IdPickingUbic() As Integer = 0
    Public Property IdPickingDet() As Integer = 0
    Public Property IdUbicacion() As Integer = 0
    Public Property IdStock() As Integer = 0
    Public Property IdStockRes() As Integer = 0
    Public Property IdPropietarioBodega() As Integer = 0
    Public Property IdProductoBodega() As Integer = 0
    Public Property IdProductoEstado() As Integer = 0
    Public Property IdPresentacion() As Integer = 0
    Public Property IdUnidadMedida() As Integer = 0
    Public Property IdUbicacionAnterior() As Integer = 0
    Public Property IdRecepcion() As Integer = 0
    Public Property IdPedidoEnc() As Integer = 0
    Public Property IdPedidoDet() As Integer = 0
    Public Property IdPickingEnc() As Integer = 0
    Public Property IdOperadorBodega() As Integer = 0
    Public Property IdOperadorBodega_Pickeo() As Integer = 0
    Public Property IdOperadorBodega_Verifico() As Integer = 0
    Public Property Lote() As String = ""
    Public Property Fecha_vence() As Date = Date.Now
    Public Property Fecha_minima() As Date = Date.Now
    Public Property Serial() As String = ""
    Public Property Licencia() As String = ""
    Public Property Cantidad_recibida() As Double = 0.0
    Public Property Cantidad_verificada() As Double = 0.0
    Public Property Fecha_picking() As Date = Date.Now
    Public Property Fecha_verificado() As Date = Date.Now
    Public Property Fecha_despachado() As Date = Date.Now
    Public Property Cantidad_despachada() As Double = 0.0
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Activo() As Boolean = False
    Public Property IdUbicacionTemporal() As Integer = 0
    Public Property IdOperadorBodega_Asignado() As Integer = 0
    Public Property Host As String = ""
    Public Property IdMovimiento As Integer = 0
    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
