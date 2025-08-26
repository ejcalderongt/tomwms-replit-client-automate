Public Class clsBeVW_operador_horario
    Implements ICloneable

    Public Property IdEmpresa() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property IdOperador() As Integer = 0
    Public Property IdJornada() As Integer = 0
    Public Property Fecha_inicio() As Date = New Date(1900, 1, 1)
    Public Property Fecha_fin() As Date = New Date(1900, 1, 1)
    Public Property IdHorarioLaboralEnc() As Integer = 0
    Public Property Dia() As Integer = 0
    Public Property Hora_inicio() As Date = New Date(1900, 1, 1)
    Public Property Hora_fin() As Date = New Date(1900, 1, 1)
    Public Property Minimo_min_hora_ingreso() As Integer = 0
    Public Property Maximo_min_hora_ingreso() As Integer = 0
    Public Property Minimo_min_hora_salida() As Integer = 0
    Public Property Maximo_min_hora_salida() As Integer = 0
    Public Property Tiempo_retraso_permitido() As Integer = 0
    Public Property Horas_extras() As Boolean = False

    Public Property OperadorActivo As Boolean = False
    Public Property OperadorBodegaActivo As Boolean = False
    Public Property JornadaLaboralActivo As Boolean = False
    Public Property HorarioActivo As Boolean = False
    Public Property HorarioLaboralDetActivo As Boolean = False
    Public Property TurnoActivo As Boolean = False

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
