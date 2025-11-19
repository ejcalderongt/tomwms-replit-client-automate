Public Class Smtp_Configuracion
    Public Property Servidor As String
    Public Property Puerto As Integer
    Public Property Usuario As String
    Public Property Password As String  ' aquí la guardamos ya encriptada o en claro, según decidas
    Public Property UsarSsl As Boolean
    Public Property RemitentePorDefecto As String
End Class
