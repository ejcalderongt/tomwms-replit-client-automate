Public Class clsBePais_departamento
    Implements ICloneable

    Public Property IdDepartamento() As Integer = 0
    Public Property IdPais() As Integer = 0
    Public Property Nombre() As String = ""
    Public Property Fec_agr() As DateTime = Now
    Public Property Fec_mod() As DateTime = Now
    Public Property User_agr() As Integer = 0
    Public Property User_mod() As Integer = 0

    Sub New()
    End Sub

    Sub New(ByRef IdDepartamento As Integer, ByVal IdPais As Integer, ByVal Nombre As String, ByVal fec_agr As String, ByVal fec_mod As String, ByVal user_agr As Integer, ByVal user_mod As Integer)
        Me.IdDepartamento = IdDepartamento
        Me.IdPais = IdPais
        Me.Nombre = Nombre
        Me.Fec_agr = Fec_agr
        Me.Fec_mod = Fec_mod
        Me.User_agr = User_agr
        Me.User_mod = User_mod
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
