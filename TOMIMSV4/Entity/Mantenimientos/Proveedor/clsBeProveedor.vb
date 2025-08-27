Public Class clsBeProveedor
    Implements ICloneable
    Implements IDisposable

    Public Property IdEmpresa() As Integer = 0
    Public Property Empresa As clsBeEmpresa
    Public Property IdPropietario() As Integer = 0
    Public Property Propietario As clsBePropietarios = New clsBePropietarios()
    Public Property IdProveedor() As Integer = 0
    Public Property Codigo As String = ""
    Public Property Nombre() As String = ""
    Public Property Telefono() As String = ""
    Public Property Nit() As String = ""
    Public Property Direccion() As String = ""
    Public Property Email() As String = ""
    Public Property Contacto() As String = ""
    Public Property Activo() As Boolean = False
    Public Property Muestra_precio() As Boolean = False
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Actualiza_costo_oc() As Boolean = False
    Public Property IdUbicacionVirtual As Integer = 0
    Public Property Es_Bodega_Recepcion As Boolean = False
    Public Property Es_Bodega_Traslado As Boolean = False
    Public Property Referencia As String = ""
    Public Property Sistema As Boolean = False
    Public Property Es_Proveedor_Servicio As Boolean = False
    Public Property IdConfiguracionBarraPallet As Integer = 0
    Public Property IdBodegaAreaSAP As Integer = 0
    Public Property IdPais As Integer = 0
    Public Codigo_Empresa_ERP As String = ""

    Sub New()
    End Sub

    Sub New(ByRef IdEmpresa As Integer, ByVal IdPropietario As Integer, ByVal IdProveedor As Integer, ByVal codigo As String, ByVal nombre As String, ByVal telefono As String, ByVal nit As String, ByVal direccion As String, ByVal email As String, ByVal contacto As String, ByVal activo As Boolean, ByVal muestra_precio As Boolean, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal actualiza_costo_oc As Boolean, ByVal IdUbicacionVirtual As Integer, ByVal Bodega_Recepcion As Boolean, ByVal Bodega_Traslado As Boolean, ByVal Referencia As String, ByVal Sistema As Boolean)
        Me.IdEmpresa = IdEmpresa
        Me.IdPropietario = IdPropietario
        Me.IdProveedor = IdProveedor
        Me.Codigo = codigo
        Me.Nombre = nombre
        Me.Telefono = telefono
        Me.Nit = nit
        Me.Direccion = direccion
        Me.Email = email
        Me.Contacto = contacto
        Me.Activo = activo
        Me.Muestra_precio = muestra_precio
        Me.User_agr = user_agr
        Me.Fec_agr = fec_agr
        Me.User_mod = user_mod
        Me.Fec_mod = fec_mod
        Me.Actualiza_costo_oc = actualiza_costo_oc
        Me.IdUbicacionVirtual = IdUbicacionVirtual
        Me.Es_Bodega_Recepcion = Bodega_Recepcion
        Me.Es_Bodega_Traslado = Bodega_Traslado
        Me.Referencia = Referencia
        Me.Sistema = Sistema
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
            End If
        End If
        disposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        If Empresa IsNot Nothing Then
            Empresa.Dispose()
            Empresa = Nothing
        End If
        If Propietario IsNot Nothing Then
            Propietario.Dispose()
            Propietario = Nothing
        End If
    End Sub
#End Region

End Class
