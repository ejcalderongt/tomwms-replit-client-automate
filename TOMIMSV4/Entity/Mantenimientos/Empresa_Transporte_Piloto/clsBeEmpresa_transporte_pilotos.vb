Public Class clsBeEmpresa_transporte_pilotos
    Implements ICloneable
    Public Property IdPiloto() As Integer = 0
    Public Property IdEmpresaTransporte() As Integer = 0
    Public Property Nombres() As String = ""
    Public Property Apellidos() As String = ""
    Public Property Telefono() As String = ""
    Public Property Correo_electronico() As String = ""
    Public Property No_carnet() As String = ""
    Public Property Fecha_expiracion_carnet() As Date = Date.Now
    Public Property No_dpi() As String = ""
    Public Property No_Licencia() As String = ""
    Public Property Fecha_expiracion_licencia() As Date = Date.Now
    Public Property Codigo_barra() As String = ""
    Public Property Direccion() As String = ""
    Public Property Foto() As Byte()
    Public Property Fecha_nacimiento() As Date = Date.Now
    Public Property Fecha_ingreso() As Date = Date.Now
    Public Property Fecha_salida() As Date = Date.Now
    Public Property IdTipoLicencia() As String = ""
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Activo() As Boolean = False
    Public Property IsNew As Boolean = False
    Public Property EscaneoDocumento As Boolean = False

    Sub New()
    End Sub
    Sub New(ByRef IdPiloto As Integer, ByVal IdEmpresaTransporte As Integer, ByVal nombres As String, ByVal apellidos As String, ByVal telefono As String, ByVal correo_electronico As String, ByVal no_carnet As String, ByVal fecha_expiracion_carnet As Date, ByVal no_dpi As String, ByVal no_licencia As String, ByVal fecha_expiracion_licencia As Date, ByVal codigo_barra As String, ByVal direccion As String, ByVal foto As Byte(), ByVal fecha_nacimiento As Date, ByVal fecha_ingreso As Date, ByVal fecha_salida As Date, ByVal IdTipoLicencia As String, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal activo As Boolean)
        Me.IdPiloto = IdPiloto
        Me.IdEmpresaTransporte = IdEmpresaTransporte
        Me.Nombres = nombres
        Me.Apellidos = apellidos
        Me.Telefono = telefono
        Me.Correo_electronico = correo_electronico
        Me.No_carnet = no_carnet
        Me.Fecha_expiracion_carnet = fecha_expiracion_carnet
        Me.No_dpi = no_dpi
        Me.No_Licencia = no_licencia
        Me.Fecha_expiracion_licencia = fecha_expiracion_licencia
        Me.Codigo_barra = codigo_barra
        Me.Direccion = direccion
        Me.Foto = foto
        Me.Fecha_nacimiento = fecha_nacimiento
        Me.Fecha_ingreso = fecha_ingreso
        Me.Fecha_salida = fecha_salida
        Me.IdTipoLicencia = IdTipoLicencia
        Me.User_agr = user_agr
        Me.Fec_agr = fec_agr
        Me.User_mod = user_mod
        Me.Fec_mod = fec_mod
        Me.Activo = activo
    End Sub
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function
End Class
