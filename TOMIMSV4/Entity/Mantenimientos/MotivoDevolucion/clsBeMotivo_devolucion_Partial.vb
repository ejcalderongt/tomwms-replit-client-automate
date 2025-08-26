Partial Public Class clsBeMotivo_devolucion
    Implements IDisposable

    Public Property IsNew() As Boolean = False
    Public Property Empresa As clsBeEmpresa = New clsBeEmpresa()
    Public Property Propietario As clsBePropietarios = New clsBePropietarios()

End Class
