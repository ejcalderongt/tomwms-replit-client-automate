Public Class clsBeCliente
    Implements ICloneable
    Public Property IdCliente() As Integer = 0
    Public Property IdEmpresa() As Integer = 0
    Public Property IdPropietario() As Integer = 0
    Public Property IdTipoCliente() As Integer = 0
    Public Property IdUbicacionManufactura() As Integer = 0
    Public Property Codigo() As String = ""
    Public Property Nombre_comercial() As String = ""
    Public Property Nombre_contacto() As String = ""
    Public Property Telefono() As String = ""
    Public Property Nit() As String = ""
    Public Property Direccion() As String = ""
    Public Property Correo_electronico() As String = ""
    Public Property Activo() As Boolean = False
    Public Property Realiza_manufactura() As Boolean = False
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Despachar_lotes_completos() As Boolean = False
    Public Property Sistema() As Boolean = False
    Public Property Es_bodega_recepcion() As Boolean = False
    Public Property Es_Bodega_Traslado() As Boolean = False
    Public Property IdUbicacionVirtual() As Integer = 0
    Public Property Control_Ultimo_Lote As Boolean = False
    Public Property Referencia As String = ""
    Public Property Control_Calidad As Boolean = False
    Public Property IdUbicacionAbastecerCon As Integer = 0
    Public Property IdBodegaAreaSAP As Integer = 0
    Public Property Es_Proveedor As Boolean = False
    Public Property Codigo_Empresa_ERP As String = ""
    Public Property IdProductoEstadoDefecto As Integer = 0

    Sub New()
    End Sub
    Sub New(ByRef IdCliente As Integer, ByVal IdEmpresa As Integer, ByVal IdPropietario As Integer, ByVal IdTipoCliente As Integer, ByVal IdUbicacionManufactura As Integer, ByVal codigo As String, ByVal nombre_comercial As String, ByVal nombre_contacto As String, ByVal telefono As String, ByVal nit As String, ByVal direccion As String, ByVal correo_electronico As String, ByVal activo As Boolean, ByVal realiza_manufactura As Boolean, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal despachar_lotes_completos As Boolean, ByVal sistema As Boolean)
        Me.IdCliente = IdCliente
        Me.IdEmpresa = IdEmpresa
        Me.IdPropietario = IdPropietario
        Me.IdTipoCliente = IdTipoCliente
        Me.IdUbicacionManufactura = IdUbicacionManufactura
        Me.Codigo = codigo
        Me.Nombre_comercial = nombre_comercial
        Me.Nombre_contacto = nombre_contacto
        Me.Telefono = telefono
        Me.Nit = nit
        Me.Direccion = direccion
        Me.Correo_electronico = correo_electronico
        Me.Activo = activo
        Me.Realiza_manufactura = realiza_manufactura
        Me.User_agr = user_agr
        Me.Fec_agr = fec_agr
        Me.User_mod = user_mod
        Me.Fec_mod = fec_mod
        Me.Despachar_lotes_completos = despachar_lotes_completos
        Me.Sistema = sistema
    End Sub
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
