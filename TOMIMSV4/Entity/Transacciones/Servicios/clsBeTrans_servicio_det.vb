Public Class clsBeTrans_servicio_det
    Implements ICloneable

    Public Property IdServicioEnc() As Integer = 0
    Public Property IdServicioDet() As Integer = 0
    Public Property Observacion() As String = ""
    Public Property Codigo_producto() As String = ""
    Public Property Nombre_servicio() As String = ""
    Public Property Unid_medida() As Integer = 0
    Public Property Nombre_unidad() As String = ""
    Public Property Corre_detalleacuerdo() As Integer = 0
    Public Property Corre_catalogoproductos() As Integer = 0
    Public Property Cantidad() As Integer = 0
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property IdPropietario() As Integer = 0
    Public Property IsNew As Boolean = True

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
