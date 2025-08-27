Public Class clsBeImpresora_mensaje
    Implements ICloneable

    Public Property IdImpresoraMensaje() As Integer = 0
    Public Property IdImpresora() As Integer = 0
    Public Property Mensaje() As String = ""
    Public Property IdMensaje() As Integer = 0
    Public Property Host() As String = ""
    Public Property User_agr() As Integer = 0
    Public Property Fec_agr() As Date = Date.Now

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
