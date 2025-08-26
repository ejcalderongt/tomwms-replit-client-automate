Public Class clsBeOperador_montacarga
    Implements ICloneable

    Public Property IdAsignacionMontacarga() As Integer = 0
    Public Property IdOperador() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property IdMontacarga() As Integer = 0
    Public Property Fecha_Asignacion() As Date = Date.Now
    Public Property Fecha_Inactivo() As Date = Date.Now
    Public Property Activo() As Boolean = False

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
