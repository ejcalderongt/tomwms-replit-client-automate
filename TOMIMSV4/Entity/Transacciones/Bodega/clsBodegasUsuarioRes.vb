Public Class clsBodegasUsuarioRes
    Implements ICloneable
    Public Property Codigo() As Integer = 0
    Public Property Nombre() As String = ""

    Public Function Clone() As Object Implements ICloneable.Clone
        Throw New NotImplementedException()
    End Function
End Class
