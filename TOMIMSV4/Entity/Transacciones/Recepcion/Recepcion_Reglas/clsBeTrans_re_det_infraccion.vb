Public Class clsBeTrans_re_det_infraccion
    Implements ICloneable
    Implements IDisposable

    Public Property IdRecepcionDetInfraccion() As Integer = 0
    Public Property IdReglaPropietarioEnc() As Integer = 0
    Public Property IdOrdenCompraEnc() As Integer = 0
    Public Property IdRecepcionEnc() As Integer = 0
    Public Property IdPresentacion() As Integer = 0
    Public Property IdProductoBodega() As Integer = 0
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Activo() As Boolean = False

    Sub New()
    End Sub

    Sub New(ByRef IdRecepcionDetInfraccion As Integer, ByVal IdReglaPropietarioEnc As Integer, ByVal IdOrdenCompraEnc As Integer, ByVal IdRecepcionEnc As Integer, ByVal IdPresentacion As Integer, ByVal IdProductoBodega As Integer, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal activo As Boolean)
        Me.IdRecepcionDetInfraccion = IdRecepcionDetInfraccion
        Me.IdReglaPropietarioEnc = IdReglaPropietarioEnc
        Me.IdOrdenCompraEnc = IdOrdenCompraEnc
        Me.IdRecepcionEnc = IdRecepcionEnc
        Me.IdPresentacion = IdPresentacion
        Me.IdProductoBodega = IdProductoBodega
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
