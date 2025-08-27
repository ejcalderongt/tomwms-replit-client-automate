Public Class clsBeRegla_ubic_det_pp
    Implements ICloneable
    Implements IDisposable

    Public Property IdReglaUbicacionDetPP() As Integer = 0
    Public Property IdReglaUbicacionEnc() As Integer = 0
    Public Property IdPresentacion() As Integer = 0
    Public Property Activo() As Boolean = False
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now

    Sub New()
    End Sub
    Sub New(ByRef IdReglaUbicacionDetPP As Integer, ByVal IdReglaUbicacionEnc As Integer, ByVal IdPresentacion As Integer, ByVal Activo As Boolean, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date)
        Me.IdReglaUbicacionDetPP = IdReglaUbicacionDetPP
        Me.IdReglaUbicacionEnc = IdReglaUbicacionEnc
        Me.IdPresentacion = IdPresentacion
        Me.Activo = Activo
        Me.User_agr = User_agr
        Me.Fec_agr = Fec_agr
        Me.User_mod = User_mod
        Me.Fec_mod = Fec_mod
    End Sub
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function
End Class
