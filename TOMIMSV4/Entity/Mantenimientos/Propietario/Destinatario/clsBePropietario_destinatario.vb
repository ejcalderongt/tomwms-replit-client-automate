Public Class clsBePropietario_destinatario
    Implements ICloneable
    Implements IDisposable

    Public Property IdDestinatarioPropietario() As Integer = 0
    Public Property IdPropietario() As Integer = 0
    Public Property Nombre() As String = ""
    Public Property Apellido() As String = ""
    Public Property Correo_electronico() As String = ""
    Public Property Telefono() As String = ""
    Public Property Telefono1() As String = ""
    Public Property Cargo() As String = ""
    Public Property Activo() As Boolean = False

    Sub New()
    End Sub

    Sub New(ByRef IdDestinatarioPropietario As Integer, ByVal IdPropietario As Integer, ByVal nombre As String, ByVal apellido As String, ByVal correo_electronico As String, ByVal telefono As String, ByVal telefono1 As String, ByVal cargo As String, ByVal activo As Boolean)
        Me.IdDestinatarioPropietario = IdDestinatarioPropietario
        Me.IdPropietario = IdPropietario
        Me.Nombre = Nombre
        Me.Apellido = Apellido
        Me.Correo_electronico = Correo_electronico
        Me.Telefono = Telefono
        Me.Telefono1 = Telefono1
        Me.Cargo = Cargo
        Me.Activo = Activo
    End Sub
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function
End Class
