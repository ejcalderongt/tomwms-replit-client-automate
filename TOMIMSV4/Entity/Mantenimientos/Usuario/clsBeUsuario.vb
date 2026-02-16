Public Class clsBeUsuario
    Implements ICloneable

    Public Property IdUsuario As Integer = 0
    Public Property IdEmpresa As Integer = 0
    Public Property Nombres As String = ""
    Public Property Apellidos As String = ""
    Public Property Cedula As String = ""
    Public Property Direccion As String = ""
    Public Property Telefono As String = ""
    Public Property Email As String = ""
    Public Property Codigo As String = ""
    Public Property Clave As String = ""
    Public Property Ultimo_login As Date = Date.Now
    Public Property Activo As Boolean = False
    Public Property User_agr As String = ""
    Public Property Fec_agr As Date = Date.Now
    Public Property User_mod As String = ""
    Public Property Fec_mod As Date = Date.Now
    Public Property Foto As Byte()
    Public Property Sistema As Boolean = False
    Public Property Clave_autorizacion As String = ""

    Sub New()
    End Sub

    Sub New(ByRef IdUsuario As Integer, ByVal IdEmpresa As Integer, ByVal nombres As String, ByVal apellidos As String, ByVal cedula As String, ByVal direccion As String, ByVal telefono As String, ByVal email As String, ByVal codigo As String, ByVal clave As String, ByVal ultimo_login As Date, ByVal activo As Boolean, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal foto As String, ByVal sistema As Boolean, ByVal clave_autorizacion As String)
        IdUsuario = IdUsuario
        IdEmpresa = IdEmpresa
        nombres = nombres
        apellidos = apellidos
        cedula = cedula
        direccion = direccion
        telefono = telefono
        email = email
        codigo = codigo
        clave = clave
        ultimo_login = ultimo_login
        activo = activo
        user_agr = user_agr
        fec_agr = fec_agr
        user_mod = user_mod
        fec_mod = fec_mod
        foto = foto
        sistema = sistema
        clave_autorizacion = clave_autorizacion
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
