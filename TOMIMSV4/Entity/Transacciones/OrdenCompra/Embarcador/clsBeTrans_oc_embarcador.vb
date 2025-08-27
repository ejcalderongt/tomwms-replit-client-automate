Public Class clsBeTrans_oc_embarcador
    Implements ICloneable

    Public Property IdEmbarcador() As Integer = 0
    Public Property Codigo() As String = ""
    Public Property Nombre() As String = ""

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
