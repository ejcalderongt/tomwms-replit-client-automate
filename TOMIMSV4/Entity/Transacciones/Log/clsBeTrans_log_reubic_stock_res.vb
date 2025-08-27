Public Class clsBeTrans_log_reubic_stock_res
    Implements ICloneable

    Public Property IdLogReubicStockRes() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property IdPickingUbic() As Integer = 0
    Public Property IdPickingDet() As Integer = 0
    Public Property IdPedidoEnc() As Integer = 0
    Public Property IdPedidoDet() As Integer = 0
    Public Property IdStock() As Integer = 0
    Public Property IdStockRes() As Integer = 0
    Public Property IdUbicacion() As Integer = 0
    Public Property IdUsuario() As Integer = 0
    Public Property Codigo_Producto() As String = ""
    Public Property Lote() As String = ""
    Public Property Lic_Plate() As String = ""
    Public Property Fecha_Vence() As Date = Date.Now
    Public Property Cantidad() As Double = 0.0
    Public Property Peso() As Double = 0.0
    Public Property Referencia() As String = ""
    Public Property Observacion() As String = ""
    Public Property IdProductoBodega() As Integer = 0
    Public Property IdProductoEstado() As Integer = 0
    Public Property IdPropietarioBodega() As Integer = 0
    Public Property IdUnidadMedida() As Integer = 0
    Public Property IdPresentacion() As Integer = 0
    Public Property Fecha_Sistema() As Date = Date.Now
    Public Property IdUbicacionAnterior() As Integer = 0
    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
