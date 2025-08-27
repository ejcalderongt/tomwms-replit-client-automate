Public Class clsBeRegla_ubic_prio_param
    Implements ICloneable

    Public Property IdReglaUbicPrioParam() As Integer = 0
    Public Property Nombre() As String = ""
    Public Property Activo() As Integer = 0
    Public Property Orden() As Integer = 0
    Public Property Tipo() As Integer = 0

    Sub New()
    End Sub

    Sub New(ByRef IdReglaUbicPrioParam As Integer, ByVal Nombre As String, ByVal Activo As Integer, ByVal Orden As Integer, ByVal Tipo As Integer)
        Me.IdReglaUbicPrioParam = IdReglaUbicPrioParam
        Me.Nombre = Nombre
        Me.Activo = Activo
        Me.Orden = Orden
        Me.Tipo = Tipo
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
