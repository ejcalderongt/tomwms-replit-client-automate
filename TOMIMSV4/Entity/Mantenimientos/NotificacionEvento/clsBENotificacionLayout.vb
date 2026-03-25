Public Class clsBENotificacionLayout
    Public Property IdLayout As Integer
    Public Property CodigoLayout As String = ""
    Public Property NombreLayout As String = ""
    Public Property HeaderHtml As String = ""
    Public Property FooterHtml As String = ""
    Public Property CssInline As String = ""
    Public Property Activo As Boolean = False
    Public Property EsDefault As Boolean = False
    Public Property FechaCreacion As Date = Now
    Public Property UsuarioCreacion As String
    Public Property FechaModificacion As Date = Now
    Public Property UsuarioModificacion As String = ""

End Class