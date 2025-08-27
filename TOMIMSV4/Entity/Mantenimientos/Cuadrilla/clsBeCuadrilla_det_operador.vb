Public Class clsBeCuadrilla_det_operador
    Implements ICloneable

    Public Property IdCuadrillaDet() As Integer = 0
    Public Property IdCuadrillaEnc() As Integer = 0
    Public Property IdOperadorBodega() As Integer = 0
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = New Date(1900, 1, 1)
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = New Date(1900, 1, 1)
    Public Property Activo() As Boolean = True

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
