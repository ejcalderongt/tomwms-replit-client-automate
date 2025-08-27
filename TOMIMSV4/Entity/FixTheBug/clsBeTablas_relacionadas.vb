Public Class clsBeTablas_relacionadas
    Implements ICloneable

    Public Property Tabla() As String = ""
    Public Property Unidad() As String = ""
    Public Property Descripcion() As String = ""
    Public Property Cantidad() As Double = 0.0
    Public Property Año() As Double = 0.0
    Public Property Correlativo() As Double = 0.0
    Public Property Turno() As Double = 0.0
    Public Property Fecha_orden_entrega() As Date = Date.Now
    Public Property Agente_aduanal() As Double = 0.0
    Public Property Correlativo1() As Double = 0.0
    Public Property NoOrdenSalida() As String = ""
    Public Property Coaniorec() As Double = 0.0
    Public Property Covehiculo() As String = ""
    Public Property Coplacas() As String = ""
    Public Property Copoliza() As Double = 0.0
    Public Property Observacion() As String = ""
    Public Property Tmoriginal() As Double = 0.0
    Public Property Tmsalidas() As Double = 0.0
    Public Property Crfecha() As Date = Date.Now
    Public Property Consignatario() As String = ""

    Public Property Utilizada As Boolean = False
    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
