
Public Class clsBeTrans_ubic_hh_det
    Implements ICloneable
    Public Property IdTareaUbicacionEnc() As Integer = 0
    Public Property IdTareaUbicacionDet() As Integer = 0
    Public Property IdStock() As Integer = 0
    Public Property IdUbicacionOrigen() As Integer = 0
    Public Property IdUbicacionDestino() As Integer = 0
    Public Property IdEstadoOrigen() As Integer = 0
    Public Property IdEstadoDestino() As Integer = 0
    Public Property IdOperadorBodega() As Integer = 0
    Public Property HoraInicio() As Date = Date.Now
    Public Property HoraFin() As Date = Date.Now
    Public Property Realizado() As Boolean = False
    Public Property Cantidad() As Double = 0.0
    Public Property Activo() As Boolean = False
    Public Property Recibido() As Double = 0.0
    Public Property Estado() As String = ""
    Public Property Atributo_variante_1() As String = ""
    Public Property IdBodega() As Integer = 0
    Public Property No_Linea() As Integer = 0

    Sub New()
    End Sub
    Sub New(ByRef IdTareaUbicacionEnc As Integer, ByVal IdTareaUbicacionDet As Integer, ByVal IdStock As Integer, ByVal IdUbicacionOrigen As Integer,
            ByVal IdUbicacionDestino As Integer, ByVal IdEstadoOrigen As Integer, ByVal IdEstadoDestino As Integer, ByVal IdOperador As Integer,
            ByVal HoraInicio As Date, ByVal HoraFin As Date, ByVal Realizado As Boolean, ByVal cantidad As Double, ByVal activo As Boolean,
            ByVal recibido As Double, ByVal estado As String, ByVal atributo_variante_1 As String, ByVal IdBodega As Integer)
        Me.IdTareaUbicacionEnc = IdTareaUbicacionEnc
        Me.IdTareaUbicacionDet = IdTareaUbicacionDet
        Me.IdStock = IdStock
        Me.IdUbicacionOrigen = IdUbicacionOrigen
        Me.IdUbicacionDestino = IdUbicacionDestino
        Me.IdEstadoOrigen = IdEstadoOrigen
        Me.IdEstadoDestino = IdEstadoDestino
        Me.IdOperadorBodega = IdOperador
        Me.HoraInicio = HoraInicio
        Me.HoraFin = HoraFin
        Me.Realizado = Realizado
        Me.Cantidad = cantidad
        Me.Activo = activo
        Me.Recibido = recibido
        Me.Estado = estado
        Me.Atributo_variante_1 = atributo_variante_1
        Me.IdBodega = IdBodega
    End Sub
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function
End Class
