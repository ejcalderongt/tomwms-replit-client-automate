Public Class clsBeCuadrilla_det_montacarga
    Implements ICloneable

    Public Property IdCuadrillaDetMontaCarga() As Integer = 0
    Public Property IdCuadrillaEnc() As Integer = 0
    Public Property IdMontacargaBodega() As Integer = 0
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = New Date(1900, 1, 1)
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = New Date(1900, 1, 1)
    Public Property Activo() As Boolean = False

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
