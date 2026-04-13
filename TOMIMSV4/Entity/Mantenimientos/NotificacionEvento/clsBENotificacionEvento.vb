Public Class clsBENotificacionEvento
    Public Property IdEvento As Integer
    Public Property CodigoEvento As String = ""
    Public Property NombreEvento As String = ""
    Public Property Modulo As String = ""
    Public Property Descripcion As String = ""
    Public Property Activo As Boolean = True
    Public Property FechaCreacion As Date = Now
    Public Property UsuarioCreacion As String = ""
    Public Property FechaModificacion As Date = Now
    Public Property UsuarioModificacion As String = ""
End Class