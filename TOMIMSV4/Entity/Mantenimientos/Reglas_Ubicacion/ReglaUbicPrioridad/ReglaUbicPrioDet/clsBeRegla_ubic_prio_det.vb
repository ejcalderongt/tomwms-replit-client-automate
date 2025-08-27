Public Class clsBeRegla_ubic_prio_det
    Implements ICloneable

    Public Property IdReglaUbicPrioDet() As Integer = 0
    Public Property IdReglaUbicPrioParam() As Integer = 0
    Public Property IdReglaUbicPrioEnc() As Integer = 0
    Public Property Orden() As Integer = 0
    Public Property Activo() As Boolean = False

    Sub New()
    End Sub

    Sub New(ByRef IdReglaUbicPrioDet As Integer, ByVal IdReglaUbicPrioParam As Integer, ByVal IdReglaUbicPrioEnc As Integer, ByVal Orden As Integer, ByVal Activo As Boolean)
        Me.IdReglaUbicPrioDet = IdReglaUbicPrioDet
        Me.IdReglaUbicPrioParam = IdReglaUbicPrioParam
        Me.IdReglaUbicPrioEnc = IdReglaUbicPrioEnc
        Me.Orden = Orden
        Me.Activo = Activo
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
