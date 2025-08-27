<Serializable>
Public Class clsBePropietarios
    Implements ICloneable
    Implements IDisposable

    Public Property IdPropietario() As Integer = 0
    Public Property IdEmpresa() As Integer = 0
    Public Property IdTipoActualizacionCosto() As Integer = 0
    Public Property Contacto() As String = ""
    Public Property Nombre_comercial() As String = ""
    Public Property Imagen() As Byte() = Nothing
    Public Property Telefono() As String = ""
    Public Property NIT As String = ""
    Public Property Direccion() As String = ""
    Public Property Activo() As Boolean = False
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Email() As String = ""
    Public Property Actualiza_costo_oc() As Boolean = False
    Public Property Color() As Integer = 0
    Public Property Codigo As String = ""
    Public Property Sistema As Boolean = False
    Public Property Es_Consolidador As Boolean = False
    Public Property ControlUx As Boolean = False

    Sub New()
    End Sub

    Sub New(ByRef IdPropietario As Integer, ByVal IdEmpresa As Integer, ByVal IdTipoActualizacionCosto As Integer, ByVal contacto As String, ByVal nombre_comercial As String, ByVal imagen As Byte(), ByVal telefono As String, ByVal direccion As String, ByVal activo As Boolean, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal email As String, ByVal actualiza_costo_oc As Boolean, ByVal color As Integer, ByVal Codigo As String)
        Me.IdPropietario = IdPropietario
        Me.IdEmpresa = IdEmpresa
        Me.IdTipoActualizacionCosto = IdTipoActualizacionCosto
        Me.Contacto = contacto
        Me.Nombre_comercial = nombre_comercial
        Me.Imagen = imagen
        Me.Telefono = telefono
        Me.Direccion = direccion
        Me.Activo = activo
        Me.User_agr = user_agr
        Me.Fec_agr = fec_agr
        Me.User_mod = user_mod
        Me.Fec_mod = fec_mod
        Me.Email = email
        Me.Actualiza_costo_oc = actualiza_costo_oc
        Me.Color = color
        Me.Codigo = Codigo
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
