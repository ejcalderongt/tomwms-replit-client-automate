Public Class clsBeMarcaje
    Implements ICloneable

    Public Property IdMarcaje() As Integer = 0
    Public Property IdEmpresa() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property IdOperador() As Integer = 0
    Public Property IdDispositivo() As String = 0
    Public Property IdHorarioLaboral() As Integer = 0
    Public Property Fec_lectura() As Date = New Date(1900, 1, 1)
    Public Property Hora_inicio_horario() As Date = New Date(1900, 1, 1)
    Public Property Hora_fin_horario() As Date = New Date(1900, 1, 1)
    Public Property Ingreso_anticipado() As Boolean = False
    Public Property Salida_anticipada() As Boolean = False
    Public Property Ingreso_tardio() As Boolean = False
    Public Property Salida_tardia() As Boolean = False
    Public Property Hora_lectura() As Date = New Date(1900, 1, 1)
    Public Property Entro() As Boolean = False
    Public Property Salio() As Boolean = False
    Public Property Hora_entro() As Date = New Date(1900, 1, 1)
    Public Property Hora_salio() As Date = New Date(1900, 1, 1)
    Public Property Marcaje_manual() As Boolean = False
    Public Property Primer_marcaje() As Integer = 0
    Public Property Marcaje_contabilizado() As Boolean = False
    Public Property Marcaje_aproximado() As Boolean = False
    Public Property Marcaje_fuera_de_sucursal() As Boolean = False
    Public Property Es_bitacora() As Boolean = False

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
