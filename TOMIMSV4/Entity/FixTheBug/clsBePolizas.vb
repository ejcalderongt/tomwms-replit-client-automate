Public Class clsBePolizas
    Implements ICloneable

    Public Property Polizas() As String = ""
    Public Procesada As Boolean = False

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
