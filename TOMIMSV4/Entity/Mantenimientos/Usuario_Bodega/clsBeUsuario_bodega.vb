Public Class clsBeUsuario_bodega
    Implements ICloneable
    Public Property IdUsuarioBodega() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property IdUsuario() As Integer = 0
    Public Property IdUsuarioSuperior() As Integer = 0
    Public Property IdRol() As Integer = 0
    Public Property Activo() As Boolean = False
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Sub New()
    End Sub
    Sub New(ByRef IdUsuarioBodega As Integer, ByVal IdBodega As Integer, ByVal IdUsuario As Integer, ByVal IdUsuarioSuperior As Integer, ByVal IdRol As Integer, ByVal Activo As Boolean, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date)
        Me.IdUsuarioBodega = IdUsuarioBodega
        Me.IdBodega = IdBodega
        Me.IdUsuario = IdUsuario
        Me.IdUsuarioSuperior = IdUsuarioSuperior
        Me.IdRol = IdRol
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
