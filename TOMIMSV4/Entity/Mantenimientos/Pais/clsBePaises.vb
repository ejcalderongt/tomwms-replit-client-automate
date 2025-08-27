Public Class clsBePaises
    Implements ICloneable
    Public Property IdPais() As Integer = 0
    Public Property ISONUM() As Integer = 0
    Public Property ISO2() As String = ""
    Public Property ISO3() As String = ""
    Public Property NOMBRE() As String = ""
    Public Property Activo() As Boolean = False
    Sub New()
    End Sub
    Sub New(ByRef IdPais As Integer, ByVal ISONUM As Integer, ByVal ISO2 As String, ByVal ISO3 As String, ByVal NOMBRE As String, ByVal Activo As Boolean)
        Me.IdPais = IdPais
        Me.ISONUM = ISONUM
        Me.ISO2 = ISO2
        Me.ISO3 = ISO3
        Me.NOMBRE = NOMBRE
        Me.Activo = Activo
    End Sub
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function
End Class
