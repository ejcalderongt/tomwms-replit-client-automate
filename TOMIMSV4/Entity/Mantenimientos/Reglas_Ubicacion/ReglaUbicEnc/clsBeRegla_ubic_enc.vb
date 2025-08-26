Public Class clsBeRegla_ubic_enc
    Implements ICloneable
    Implements IDisposable

    Public Property IdReglaUbicacionEnc() As Integer = 0
    Public Property IdEmpresa() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property Nombre() As String = ""
    Public Property Activo() As Boolean = False
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now

    Sub New()
    End Sub

    Sub New(ByRef IdReglaUbicacionEnc As Integer, ByVal IdEmpresa As Integer, ByVal IdBodega As Integer, ByVal Nombre As String, ByVal Activo As Boolean, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date)
        Me.IdReglaUbicacionEnc = IdReglaUbicacionEnc
        Me.IdEmpresa = IdEmpresa
        Me.IdBodega = IdBodega
        Me.Nombre = Nombre
        Me.Activo = Activo
        Me.User_agr = User_agr
        Me.Fec_agr = Fec_agr
        Me.User_mod = User_mod
        Me.Fec_mod = Fec_mod
    End Sub

    Public Function Clone() As Object Implements ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
