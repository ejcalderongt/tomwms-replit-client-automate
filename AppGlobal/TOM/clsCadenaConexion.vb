Public Class clsCadenaConexion
    Public Property NombreInstancia() As String = ""
    Public Property Server() As String = ""
    Public Property NombreBD() As String = ""
    Public Property Usuario() As String = ""
    Public Property Clave() As String = ""
    Public Property Seguridad_Integrada() As Boolean = False
    Public Property Seguridad_Integrada_ERP() As Boolean = False
    Public Property IpWCF As String = ""
    Public Property TimeOutConBD As Integer = 30
    Public Property TimeOutConBD_ERP As Integer = 30
    Public Property Indice As Integer = 0
    Public Property ID_EMPRESA_TMS As Integer = 1
    Public Property URL_WSHH_QAS As String = ""
    Public Property URL_WSHH_PRD As String = ""
    Public Property URL_ENTRADA_AJUSTE_POST As String = ""
    Public Property URL_SALIDA_AJUSTE_POST As String = ""
    Public Property FORMATO_INGRESO_FISCAL As Integer = 1
    Public Property URL_VALIDA_PRODUCTOS_POST As String = ""

    '#GT04032025: parametros para instalar rapidamente quicktag en banasa
    Public Property ID_EMPRESA_QUICKTAG As Integer = 0
    Public Property ID_STANDALONE As Integer = 0
    Public Property IMPRESORA_DEFAULT As String = ""
    Public Property HANA_SL As String = "https//10.138.26.78:50000"
    Public Property HANA_SL_USR As String = "DevAccess"
    Public Property HANA_SL_PWD As String = "d3V_205!W4"

    Public ReadOnly Property CadenaConexionSQLClient() As String
        Get
            ''#EJC20171112_0820PM: MultipleActiveResultSets=true agregado por multithreading en monitor de menú
            'Encrypt=True;TrustServerCertificate=False;
            If Not Seguridad_Integrada Then
                Return String.Format("Data Source={0};Initial Catalog={1} ;User ID={2} ;Password = {3} ;Persist Security Info=True;Connect Timeout={4}; MultipleActiveResultSets=true;Encrypt=True;TrustServerCertificate=True", Server, NombreBD, Usuario, Clave, TimeOutConBD)
            Else
                Return String.Format("Data Source={0};Initial Catalog={1} ;Integrated Security=SSPI ;Persist Security Info=True;Connect Timeout={2}; MultipleActiveResultSets=true;Encrypt=True;TrustServerCertificate=True", Server, NombreBD, TimeOutConBD)
            End If
        End Get
    End Property

    Public ReadOnly Property Cadena_Conexion_SQL_ERP() As String
        Get
            ''#EJC20171112_0820PM: MultipleActiveResultSets=true agregado por multithreading en monitor de menú
            If Not Seguridad_Integrada_ERP Then
                Return String.Format("Data Source={0};Initial Catalog={1} ;User ID={2} ;Password = {3} ;Persist Security Info=True;Connect Timeout={4}; MultipleActiveResultSets=true;Encrypt=True;TrustServerCertificate=True", SERVER_ERP, NOMBRE_BD_ERP, USUARIO_BD_ERP, CLAVE_BD_ERP, TimeOutConBD)
            Else
                Return String.Format("Data Source={0};Initial Catalog={1} ;Integrated Security=SSPI ;Persist Security Info=True;Connect Timeout={2}; MultipleActiveResultSets=true,;Encrypt=True;TrustServerCertificate=True", SERVER_ERP, NOMBRE_BD_ERP, TimeOutConBD)
            End If
        End Get
    End Property

End Class

