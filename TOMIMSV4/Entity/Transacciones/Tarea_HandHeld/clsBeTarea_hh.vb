Public Class clsBeTarea_hh
    Implements ICloneable
    Implements IDisposable

    Public Property IdTareahh() As Integer = 0
    Public Property IdPropietario() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property IdMuelle() As Integer = 0
    Public Property IdEstado() As Integer = 0
    Public Property IdPrioridad() As Integer = 0
    Public Property IdTipoTarea() As Integer = 0
    Public Property IdTransaccion() As Integer = 0
    Public Property Tipo() As Integer = 0
    Public Property FechaInicio() As Date = Date.Now
    Public Property FechaFin() As Date = Date.Now
    Public Property DiaCompleto() As Boolean = False
    Public Property Asunto() As String = ""
    Public Property Ubicacion() As String = ""
    Public Property Descripcion() As String = ""
    Public Property Recordatorio() As String = ""
    Public Property IdOperadorBodega_Cerro As Integer = 0
    Public Property Host_Cerro As String = ""

    Sub New()
    End Sub

    Sub New(ByRef IdTareahh As Integer, ByVal IdPropietario As Integer, ByVal IdBodega As Integer, ByVal IdMuelle As Integer, ByVal IdEstado As Integer, ByVal IdPrioridad As Integer, ByVal IdTipoTarea As Integer, ByVal IdTransaccion As Integer, ByVal Tipo As Integer, ByVal FechaInicio As Date, ByVal FechaFin As Date, ByVal DiaCompleto As Boolean, ByVal Asunto As String, ByVal Ubicacion As String, ByVal Descripcion As String, ByVal Recordatorio As String)
        Me.IdTareahh = IdTareahh
        Me.IdPropietario = IdPropietario
        Me.IdBodega = IdBodega
        Me.IdMuelle = IdMuelle
        Me.IdEstado = IdEstado
        Me.IdPrioridad = IdPrioridad
        Me.IdTipoTarea = IdTipoTarea
        Me.IdTransaccion = IdTransaccion
        Me.Tipo = Tipo
        Me.FechaInicio = FechaInicio
        Me.FechaFin = FechaFin
        Me.DiaCompleto = DiaCompleto
        Me.Asunto = Asunto
        Me.Ubicacion = Ubicacion
        Me.Descripcion = Descripcion
        Me.Recordatorio = Recordatorio
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
