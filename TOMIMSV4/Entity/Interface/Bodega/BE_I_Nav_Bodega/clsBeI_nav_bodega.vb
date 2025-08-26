Public Class clsBeI_nav_bodega
    Implements ICloneable

    Public Property Bodega_code() As String = ""
    Public Property Bodega_name() As String = ""
    Sub New()
    End Sub

    Sub New(ByRef bodega_code As String, ByVal bodega_name As String)
        Me.Bodega_code = bodega_code
        Me.Bodega_name = bodega_name
    End Sub
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
