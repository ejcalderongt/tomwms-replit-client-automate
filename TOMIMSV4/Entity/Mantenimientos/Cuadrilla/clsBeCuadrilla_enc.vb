Public Class clsBeCuadrilla_enc
    Implements ICloneable

    Public Property IdCuadrillaEnc() As Integer = 0
    Public Property IdTipoCuadrilla() As Integer = 0
    Public Property IdEmpresa() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property Nombre() As String = ""
    Public Property Descripcion() As String = ""
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = New Date(1900, 1, 1)
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = New Date(1900, 1, 1)
    Public Property Activo() As Boolean = False
    Public Property IsNew As Boolean = False

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
