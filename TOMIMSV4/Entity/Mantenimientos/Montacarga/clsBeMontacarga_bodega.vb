Public Class clsBeMontacarga_bodega
    Implements ICloneable
    Public Property IdMontacargaBodega() As Integer = 0
    Public Property IdMontacarga() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Activo() As Boolean = False
    Sub New()
    End Sub
    Sub New(ByRef IdMontacargaBodega As Integer, ByVal IdMontacarga As Integer, ByVal IdBodega As Integer, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal activo As Boolean)
        Me.IdMontacargaBodega = IdMontacargaBodega
        Me.IdMontacarga = IdMontacarga
        Me.IdBodega = IdBodega
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
