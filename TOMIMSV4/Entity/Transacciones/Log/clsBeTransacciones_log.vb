Public Class clsBeTransacciones_log
    Implements ICloneable
    Implements IDisposable

    Public Property IdTransaccionLog() As Integer = 0
    Public Property IdEmpresa() As Integer = 0
    Public Property IdPropietarioBodega() As Integer = 0
    Public Property IdObservacion() As Integer = 0
    Public Property IdProductoBodega() As Integer = 0
    Public Property IdPresentacion() As Integer = 0
    Public Property IdPresentacionAbastercerCon As Integer = 0
    Public Property IdProductoEstado() As Integer = 0
    Public Property IdUnidadMedida() As Integer = 0
    Public Property IdUnidadMedidaBasAbastercerCon As Integer = 0
    Public Property IdUbicacion() As Integer = 0
    Public Property Cantidad_reabasto() As Double = 0.0
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Activo() As Boolean = False

    Sub New()
    End Sub

    Sub New(ByRef IdTransaccionLog As Integer, ByVal IdEmpresa As Integer, ByVal IdPropietarioBodega As Integer, ByVal IdObservacion As Integer, ByVal IdStock As Integer, ByVal IdProductoBodega As Integer, ByVal IdPresentacion As Integer, ByVal IdProductoEstado As Integer, ByVal IdUnidadMedida As Integer, ByVal IdUbicacion As Integer, ByVal cantidad As Double, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal activo As Boolean)
        Me.IdTransaccionLog = IdTransaccionLog
        Me.IdEmpresa = IdEmpresa
        Me.IdPropietarioBodega = IdPropietarioBodega
        Me.IdObservacion = IdObservacion
        Me.IdProductoBodega = IdProductoBodega
        Me.IdPresentacion = IdPresentacion
        Me.IdProductoEstado = IdProductoEstado
        Me.IdUnidadMedida = IdUnidadMedida
        Me.IdUbicacion = IdUbicacion
        Me.Cantidad_reabasto = cantidad
        Me.User_agr = User_agr
        Me.Fec_agr = Fec_agr
        Me.User_mod = User_mod
        Me.Fec_mod = fec_mod
        Me.Activo = activo
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
