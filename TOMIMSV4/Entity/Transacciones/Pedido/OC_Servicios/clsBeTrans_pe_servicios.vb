Public Class clsBeTrans_pe_servicios

    Implements ICloneable

    Public Property IdOrdenPedidoServicio() As Integer = 0
    Public Property IdOrdenPedidoEnc() As Integer = 0
    Public Property IdServicio() As Integer = 0
    Public Property IdAcuerdo() As Integer = 0
    Public Property IdAcuerdoDet() As Integer = 0
    Public Property Observacion() As String = ""
    Public Property Codigo_producto() As String = ""
    Public Property Nombre_servicio() As String = ""
    Public Property Unid_medida() As Integer = 0
    Public Property Nombre_unidad() As String = ""
    Public Property Corre_detalleacuerdo() As Integer = 0
    Public Property Corre_catalogoproductos() As Integer = 0
    Public Property IdPropietarioBodega() As Integer = 0
    Public Property Cantidad() As Integer = 0
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
