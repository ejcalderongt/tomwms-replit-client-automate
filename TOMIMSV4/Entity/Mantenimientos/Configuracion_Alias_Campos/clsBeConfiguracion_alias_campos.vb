Public Class clsBeConfiguracion_alias_campos
    Implements ICloneable

    Public Property IdConfiguracionAlias() As Integer = 0
    Public Property Nombre_WMS() As String = ""
    Public Property Alias_WMS() As String = ""

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
