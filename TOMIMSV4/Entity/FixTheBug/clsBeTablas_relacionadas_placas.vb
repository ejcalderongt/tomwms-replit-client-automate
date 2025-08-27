Public Class clsBeTablas_relacionadas_placas
    Implements ICloneable

    Public Property Coplacas() As String = ""
    Public Property Copoliza() As Double = 0.0
    Public Property Fecha_orden_entrega() As Date = Date.Now
    Public Property Noordensalida() As String = ""
    Public Property Correlativo1() As Double = 0.0
    Public Property Descripcion() As String = ""
    Public Property IdPedidoEnc() As Integer = 0
    Public Property Fecha_despacho() As String = New Date(1900, 1, 1)
    Public Property NombreProducto() As String = ""
    Public Property IdDespachoEnc() As Integer = 0
    Public Property Utilizada() As Boolean = False

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
