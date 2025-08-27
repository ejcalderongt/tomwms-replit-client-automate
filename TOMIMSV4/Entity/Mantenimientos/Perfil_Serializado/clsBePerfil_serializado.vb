Public Class clsBePerfil_serializado
    Implements ICloneable
    Public Property IdPerfilSerializado() As Integer = 0
    Public Property Descripcion() As String = ""
    Public Property Activo() As Boolean = False
    Sub New()
    End Sub
    Sub New(ByRef IdPerfilSerializado As Integer, ByVal descripcion As String, ByVal activo As Boolean)
        Me.IdPerfilSerializado = IdPerfilSerializado
        Me.Descripcion = Descripcion
        Me.Activo = Activo
    End Sub
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function
End Class
