Public Class clsBeTrans_ubic_hh_enc
    Implements ICloneable
    Public Property IdTareaUbicacionEnc() As Integer = 0
    Public Property IdPropietarioBodega() As Integer = 0
    Public Property IdMotivoUbicacion() As Integer = 0
    Public Property FechaInicio() As Date = Date.Now
    Public Property HoraInicio() As Date = Date.Now
    Public Property FechaFin() As Date = New Date(1900, 1, 1)
    Public Property HoraFin() As Date = Date.Now
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Observacion() As String = ""
    Public Property Activo() As Boolean = False
    Public Property Operador_por_linea() As Boolean = False
    Public Property Ubicacion_con_hh() As Boolean = False
    Public Property Estado() As String = ""
    Public Property Cambio_estado() As Boolean = False
    Public Property IdReabastecimientoLog As Integer = 0
    Public Property Es_Traslado_SAP As Boolean = False
    Public Property No_Documento As String = ""
    Public Property Usuario As String = ""
    Public Property Rol As String = ""

    Sub New()
    End Sub
    Sub New(ByRef IdTareaUbicacionEnc As Integer, ByVal IdPropietarioBodega As Integer, ByVal IdMotivoUbicacion As Integer, ByVal FechaInicio As String, ByVal HoraInicio As Date, ByVal FechaFin As String, ByVal HoraFin As Date, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal Observacion As String, ByVal activo As Boolean, ByVal operador_por_linea As Boolean, ByVal ubicacion_con_hh As Boolean, ByVal estado As String, ByVal cambio_estado As Boolean)
        Me.IdTareaUbicacionEnc = IdTareaUbicacionEnc
        Me.IdPropietarioBodega = IdPropietarioBodega
        Me.IdMotivoUbicacion = IdMotivoUbicacion
        Me.FechaInicio = FechaInicio
        Me.HoraInicio = HoraInicio
        Me.FechaFin = FechaFin
        Me.HoraFin = HoraFin
        Me.User_agr = user_agr
        Me.Fec_agr = fec_agr
        Me.User_mod = user_mod
        Me.Fec_mod = fec_mod
        Me.Observacion = Observacion
        Me.Activo = activo
        Me.Operador_por_linea = operador_por_linea
        Me.Ubicacion_con_hh = ubicacion_con_hh
        Me.Estado = estado
        Me.Cambio_estado = cambio_estado
    End Sub
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function
End Class
