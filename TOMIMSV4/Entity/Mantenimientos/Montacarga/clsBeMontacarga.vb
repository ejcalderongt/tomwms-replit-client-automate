Public Class clsBeMontacarga
    Implements ICloneable
    Public Property IdMontacarga() As Integer = 0
    Public Property IdEmpresa() As Integer = 0
    Public Property Nombre() As String = ""
    Public Property Modelo() As String = ""
    Public Property Serie() As String = ""
    Public Property Capacidad_basica() As Double = 0.0
    Public Property Desplazamiento_motor() As Double = 0.0
    Public Property Costo_Hora As Double = 0.0
    Public Property Tipo_combustible() As String = ""
    Public Property Tipo_montacarga() As String = ""
    Public Property Fecha_compra() As Date = Date.Now
    Public Property Fecha_inicio_operaciones() As Date = Date.Now
    Public Property Proximo_mantenimiento() As Date = Date.Now
    Public Property Nivel_Desde As Integer = 0
    Public Property Nivel_Hasta As Integer = 0


    Sub New()
    End Sub
    Sub New(ByRef IdMontacarga As Integer, ByVal IdEmpresa As Integer, ByVal Nombre As String, ByVal Modelo As String, ByVal Serie As String, ByVal capacidad_basica As Double, ByVal desplazamiento_motor As Double, ByVal tipo_combustible As String, ByVal tipo_montacarga As String, ByVal fecha_compra As Date, ByVal fecha_inicio_operaciones As Date, ByVal proximo_mantenimiento As Date)
        Me.IdMontacarga = IdMontacarga
        Me.IdEmpresa = IdEmpresa
        Me.Nombre = Nombre
        Me.Modelo = Modelo
        Me.Serie = Serie
        Me.Capacidad_basica = Capacidad_basica
        Me.Desplazamiento_motor = Desplazamiento_motor
        Me.Tipo_combustible = Tipo_combustible
        Me.Tipo_montacarga = Tipo_montacarga
        Me.Fecha_compra = Fecha_compra
        Me.Fecha_inicio_operaciones = Fecha_inicio_operaciones
        Me.Proximo_mantenimiento = Proximo_mantenimiento
    End Sub
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function
End Class
