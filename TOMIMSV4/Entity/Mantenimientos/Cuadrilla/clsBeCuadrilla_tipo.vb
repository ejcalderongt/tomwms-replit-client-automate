Public Class clsBeCuadrilla_tipo
    Implements ICloneable

    Public Property IdTipoCuadrilla() As Integer = 0
    Public Property Nombre() As String = ""
    Public Property Es_recepcion() As Boolean = False
    Public Property Es_picking() As Boolean = False
    Public Property Es_verificacion() As Boolean = False
    Public Property Es_transito() As Boolean = False
    Public Property Es_inventario() As Boolean = False
    Public Property Es_ubicacion() As Boolean = False
    Public Property Fec_agr() As Date = New Date(1900, 1, 1)
    Public Property User_agr() As String = ""
    Public Property Fec_mod() As Date = New Date(1900, 1, 1)
    Public Property User_mod() As String = ""

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
