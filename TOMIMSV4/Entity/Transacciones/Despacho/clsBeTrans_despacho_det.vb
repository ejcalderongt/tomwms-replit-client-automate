Public Class clsBeTrans_despacho_det
    Implements ICloneable
    Public Property IdDespachoDet() As Integer = 0
    Public Property IdDespachoEnc() As Integer = 0
    Public Property IdPickingUbic() As Integer = 0
    Public Property Fecha() As Date = Date.Now
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Activo() As Boolean = False
    Public Property IdPedidoEnc() As Integer = 0
    Public Property IdPedidoDet() As Integer = 0
    Public Property IdProductoBodega() As Integer = 0
    Public Property IdUnidadMedidaBasica() As Integer = 0
    Public Property IdPresentacion() As Integer = 0
    Public Property Codigo() As String = ""
    Public Property NombreProducto() As String = ""
    Public Property NombreEstado() As String = ""
    Public Property CantidadDespachada() As Double = 0.0
    Public Property PesoDespachado() As Double = 0.0
    Public Property IdProductoEstado() As Integer = 0
    Sub New()
    End Sub
    Sub New(ByRef IdDespachoDet As Integer, ByVal IdDespachoEnc As Integer, ByVal IdPickingUbic As Integer, ByVal Fecha As Date, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As String, ByVal activo As Boolean, ByVal IdPedidoEnc As Integer, ByVal IdPedidoDet As Integer, ByVal IdProductoBodega As Integer, ByVal IdUnidadMedidaBasica As Integer, ByVal IdPresentacion As Integer, ByVal Codigo As String, ByVal NombreProducto As String, ByVal NombreEstado As String, ByVal CantidadDespachada As Double, ByVal PesoDespachado As Double, ByVal IdProductoEstado As Integer)
        Me.IdDespachoDet = IdDespachoDet
        Me.IdDespachoEnc = IdDespachoEnc
        Me.IdPickingUbic = IdPickingUbic
        Me.Fecha = Fecha
        Me.User_agr = User_agr
        Me.Fec_agr = Fec_agr
        Me.User_mod = User_mod
        Me.Fec_mod = Fec_mod
        Me.Activo = Activo
        Me.IdPedidoEnc = IdPedidoEnc
        Me.IdPedidoDet = IdPedidoDet
        Me.IdProductoBodega = IdProductoBodega
        Me.IdUnidadMedidaBasica = IdUnidadMedidaBasica
        Me.IdPresentacion = IdPresentacion
        Me.Codigo = Codigo
        Me.NombreProducto = NombreProducto
        Me.NombreEstado = NombreEstado
        Me.CantidadDespachada = CantidadDespachada
        Me.PesoDespachado = PesoDespachado
        Me.IdProductoEstado = IdProductoEstado
    End Sub
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function
End Class
