Public Class clsCadenaConexion
    Public Property NombreInstancia() As String = ""
    Public Property Server() As String = ""
    Public Property NombreBD() As String = ""
    Public Property Usuario() As String = ""
    Public Property Clave() As String = ""
    Public Property Seguridad_Integrada() As Boolean = False
    Public Property IpWCF As String = ""
    Public Property TimeOutConBD As Integer = 30

    Public ReadOnly Property CadenaConexionSQLClient() As String
        Get
            If Not Seguridad_Integrada Then
                Return String.Format("Data Source={0};Initial Catalog={1} ;User ID={2} ;Password = {3} ;Persist Security Info=True;Connect Timeout={4}", Server, NombreBD, Usuario, Clave, TimeOutConBD)
            Else
                Return String.Format("Data Source={0};Initial Catalog={1} ;Integrated Security=SSPI ;Persist Security Info=True;Connect Timeout={2}", Server, NombreBD, TimeOutConBD)
            End If
        End Get
    End Property

End Class