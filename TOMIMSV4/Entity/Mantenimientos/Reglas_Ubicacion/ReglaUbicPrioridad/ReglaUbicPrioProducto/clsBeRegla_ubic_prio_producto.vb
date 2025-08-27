Public Class clsBeRegla_ubic_prio_producto
    Implements ICloneable

    Public Property IdReglaUbicPrioProd() As Integer = 0
    Public Property IdReglaUbicPrioEnc() As Integer = 0
    Public Property IdProducto() As Integer = 0

    Sub New()
    End Sub

    Sub New(ByRef IdReglaUbicPrioProd As Integer, ByVal IdReglaUbicPrioEnc As Integer, ByVal IdProducto As Integer)
        Me.IdReglaUbicPrioProd = IdReglaUbicPrioProd
        Me.IdReglaUbicPrioEnc = IdReglaUbicPrioEnc
        Me.IdProducto = IdProducto
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
