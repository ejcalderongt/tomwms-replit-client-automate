Public Class clsBeCamara
    Implements ICloneable
    Public Property IdCamara() As Integer = 0
    Public Property Codigo() As String = ""
    Public Property Nombre() As String = ""
    Public Property Modelo() As String = ""
    Public Property Serie() As String = ""
    Public Property Ip() As String = ""
    Public Property IdUbicacion() As Integer = 0
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Activo() As Boolean = False
    Sub New()
    End Sub
    Sub New(ByRef IdCamara As Integer, ByVal codigo As String, ByVal nombre As String, ByVal modelo As String, ByVal serie As String, ByVal Ip As String, ByVal IdUbicacion As Integer, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal activo As Boolean)
        Me.IdCamara = IdCamara
        Me.Codigo = Codigo
        Me.Nombre = Nombre
        Me.Modelo = Modelo
        Me.Serie = Serie
        Me.Ip = Ip
        Me.IdUbicacion = IdUbicacion
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
