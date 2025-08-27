Public Class clsBeMontacarga_tipoFalla
    Implements ICloneable

    Public Property IdTipoFalla() As String = ""
    Public Property IdEmpresa() As Integer = 0
    Public Property Nombre() As String = ""
    Public Property Activo() As Integer = 0

    Sub New()
    End Sub

    Sub New(ByRef IdTipoFalla As String, ByVal IdEmpresa As Integer, ByVal Nombre As String, ByVal Activo As Integer)
        Me.IdTipoFalla = IdTipoFalla
        Me.IdEmpresa = IdEmpresa
        Me.Nombre = Nombre
        Me.Activo = Activo
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
