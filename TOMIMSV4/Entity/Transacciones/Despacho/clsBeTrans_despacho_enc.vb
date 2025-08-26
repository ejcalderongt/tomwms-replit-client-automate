Public Class clsBeTrans_despacho_enc
    Implements ICloneable
    Implements IDisposable

    Public Property IdDespachoEnc() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property IdPropietarioBodega() As Integer = 0
    Public Property IdVehiculo() As Integer = 0
    Public Property IdPiloto() As Integer = 0
    Public Property IdRuta() As Integer = 0
    Public Property Fecha() As DateTime = Date.Now
    Public Property No_pase() As Integer = 0
    Public Property Observacion() As String = ""
    Public Property Hora_ini() As DateTime
    Public Property Hora_fin() As Date = Date.Now
    Public Property Estado() As String = ""
    Public Property Numero() As Double = 0
    Public Property Marchamo() As String = ""
    Public Property Cant_bultos() As Double = 0.0
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Activo() As Boolean = False
    Public Property No_Documento_Externo As String = ""

    Sub New()
    End Sub

    Sub New(ByRef IdDespachoEnc As Integer, ByVal IdBodega As Integer, ByVal IdPropietarioBodega As Integer, ByVal IdVehiculo As Integer, ByVal IdPiloto As Integer, ByVal IdRuta As Integer, ByVal fecha As Date, ByVal no_pase As Integer, ByVal observacion As String, ByVal hora_ini As Date, ByVal hora_fin As Date, ByVal estado As String, ByVal numero As Integer, ByVal marchamo As String, ByVal cant_bultos As Double, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal activo As Boolean)
        Me.IdDespachoEnc = IdDespachoEnc
        Me.IdBodega = IdBodega
        Me.IdPropietarioBodega = IdPropietarioBodega
        Me.IdVehiculo = IdVehiculo
        Me.IdPiloto = IdPiloto
        Me.IdRuta = IdRuta
        Me.Fecha = Fecha
        Me.No_pase = No_pase
        Me.Observacion = Observacion
        Me.Hora_ini = Hora_ini
        Me.Hora_fin = Hora_fin
        Me.Estado = Estado
        Me.Numero = Numero
        Me.Marchamo = Marchamo
        Me.Cant_bultos = Cant_bultos
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
