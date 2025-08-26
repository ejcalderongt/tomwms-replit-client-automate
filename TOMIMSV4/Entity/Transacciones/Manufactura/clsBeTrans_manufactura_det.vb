Public Class clsBeTrans_manufactura_det
    Implements ICloneable

    Public Property IdManufacturaDet() As Integer = 0
    Public Property IdManufacturaEnc() As Integer = 0
    Public Property IdPedidoDet() As Integer = 0
    Public Property IdPropietarioBodega() As Integer = 0
    Public Property IdProductoBodega() As Integer = 0
    Public Property Codigo_producto() As String = ""
    Public Property Nombre_producto() As String = ""
    Public Property Cantidad_esperada() As Double = 0.0
    Public Property Cantidad_recibida() As Double = 0.0
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
