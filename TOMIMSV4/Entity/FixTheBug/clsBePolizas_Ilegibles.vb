Public Class clsBePolizas_Ilegibles
    Implements ICloneable

    Public Property No_poliza() As String = ""
    Public Property Numero_orden() As Double = 0.0
    Public Property Dua() As String = ""
    Public Property Fecha_documento() As Date = Date.Now
    Public Property Fecha_aceptación() As Date = Date.Now
    Public Property Fecha_llegada() As Date = Date.Now
    Public Property Tipo_cambio() As Double = 0.0
    Public Property Regimen() As String = ""
    Public Property Clase() As Double = 0.0

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
