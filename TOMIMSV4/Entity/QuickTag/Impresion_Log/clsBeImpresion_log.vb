Public Class clsBeImpresion_log
    Implements ICloneable

    Public Property IdImpresionLog() As Integer = 0
    Public Property Descripcion() As String = ""
    Public Property Impresora() As String = ""
    Public Property Accion() As String = ""
    Public Property Estado() As String = ""
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property IdColaImpresion() As Integer = 0

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
