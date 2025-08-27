Public Class clsBeTrans_picking_det
    Implements ICloneable
    Implements IDisposable

    Public Property IdPickingDet() As Integer = 0
    Public Property IdPickingEnc() As Integer = 0
    Public Property IdPedidoDet() As Integer = 0
    Public Property IdOperadorBodega() As Integer = 0
    Public Property Cantidad() As Double = 0.0
    Public Property Cliente_dias() As Integer = 0
    Public Property Cantidad_recibida() As Double = 0.0
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Activo() As Boolean = False

    Sub New()
    End Sub

    Sub New(ByRef IdPickingDet As Integer, ByVal IdPickingEnc As Integer, ByVal IdPedidoDet As Integer, ByVal IdOperadorBodega As Integer, ByVal cantidad As Double, ByVal cliente_dias As Integer, ByVal cantidad_recibida As Double, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal activo As Boolean)
        Me.IdPickingDet = IdPickingDet
        Me.IdPickingEnc = IdPickingEnc
        Me.IdPedidoDet = IdPedidoDet
        Me.IdOperadorBodega = IdOperadorBodega
        Me.Cantidad = Cantidad
        Me.Cliente_dias = Cliente_dias
        Me.Cantidad_recibida = Cantidad_recibida
        Me.User_agr = User_agr
        Me.Fec_agr = Fec_agr
        Me.User_mod = User_mod
        Me.Fec_mod = Fec_mod
        Me.Activo = Activo
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
