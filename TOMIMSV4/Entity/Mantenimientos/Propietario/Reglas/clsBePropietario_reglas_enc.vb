Public Class clsBePropietario_reglas_enc
    Implements ICloneable
    Implements IDisposable

    Public Property IdReglaPropietarioEnc() As Integer = 0
    Public Property IdReglaRecepcion() As Integer = 0
    Public Property IdPropietario() As Integer = 0
    Public Property IdMensajeRegla() As Integer = 0
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Activo() As Boolean = False

    Sub New()
    End Sub

    Sub New(ByRef IdReglaPropietarioEnc As Integer, ByVal IdReglaRecepcion As Integer, ByVal IdPropietario As Integer, ByVal IdMensajeRegla As Integer, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal activo As Boolean)
        Me.IdReglaPropietarioEnc = IdReglaPropietarioEnc
        Me.IdReglaRecepcion = IdReglaRecepcion
        Me.IdPropietario = IdPropietario
        Me.IdMensajeRegla = IdMensajeRegla
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
