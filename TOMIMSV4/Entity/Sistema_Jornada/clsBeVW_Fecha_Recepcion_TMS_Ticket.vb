Public Class clsBeVW_Fecha_Recepcion_TMS_Ticket
    Implements ICloneable

    Public Property IdTicket() As Integer = 0
    Public Property IdOrdenCompraEnc() As Integer = 0
    Public Property IdRecepcionEnc() As Integer = 0
    Public Property Fecha_Ingreso() As Date = Date.Now
    Public Property Fecha_Creacion() As Date = Date.Now
    Public Property Fecha_Recepcion() As Date = Date.Now

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
