Public Class clsBeRol
    Implements ICloneable
    Public Property IdRol() As Integer = 0
    Public Property IdEmpresa() As Integer = 0
    Public Property Nombre() As String = ""
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Activo() As Boolean = False
    Public Property Registrar_clave_autorizacion() As Boolean = False
    Sub New()
    End Sub
    Sub New(ByRef IdRol As Integer, ByVal IdEmpresa As Integer, ByVal nombre As String, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal activo As Boolean, ByVal registrar_clave_autorizacion As Boolean)
        Me.IdRol = IdRol
        Me.IdEmpresa = IdEmpresa
        Me.Nombre = nombre
        Me.User_agr = user_agr
        Me.Fec_agr = fec_agr
        Me.User_mod = user_mod
        Me.Fec_mod = fec_mod
        Me.Activo = activo
        Me.Registrar_clave_autorizacion = registrar_clave_autorizacion
    End Sub
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function
    Public Overrides Function ToString() As String
        Return " " + Nombre
    End Function
End Class
