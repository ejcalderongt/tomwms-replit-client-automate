Public Class clsBeLicencia_llave
    Implements ICloneable

    Public Property IdLlave() As Integer = 0
    Public Property Llave() As String = ""

    Sub New()
        'MyBase.New()?
    End Sub

    Sub New(ByRef idLlave As Integer, ByVal Llave As String)
        Me.IdLlave = IdLlave
        Me.Llave = Llave
    End Sub

    Public Function Clone() As Object Implements ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
