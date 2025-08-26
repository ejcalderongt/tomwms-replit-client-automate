Public Class clsBeSimbologias_codigo_barra
    Implements ICloneable
    Public Property IdSimbologia() As Integer = 0
    Public Property Nombre() As String = ""
    Public Property Activo() As Boolean = False
    Sub New()
    End Sub
    Sub New(ByRef IdSimbologia As Integer, ByVal Nombre As String, ByVal Activo As Boolean)
        Me.IdSimbologia = IdSimbologia
        Me.Nombre = Nombre
        Me.Activo = Activo
    End Sub
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function
End Class
