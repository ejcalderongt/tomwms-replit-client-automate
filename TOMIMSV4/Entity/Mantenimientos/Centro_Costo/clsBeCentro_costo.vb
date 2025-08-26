Public Class clsBeCentro_costo
    Implements ICloneable

    Public Property IdCentroCosto() As Integer = 0
    Public Property IdEmpresa() As Integer = 0
    Public Property Codigo() As String = ""
    Public Property Nombre() As String = ""
    Public Property Referencia() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_agr() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Activo() As Boolean = False
    Public Property Control_Inventario() As Boolean = False

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
