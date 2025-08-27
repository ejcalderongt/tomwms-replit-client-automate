Public Class clsBeTrans_acuerdoscomerciales_det
    Implements ICloneable

    Public Property IdAcuerdoDet() As Integer = 0
    Public Property IdAcuerdoEnc() As Integer = 0
    Public Property Codigo_producto() As String = ""
    Public Property Servicio() As String = ""
    Public Property Nemonico() As String = ""
    Public Property Codigo_acuerdo() As Integer = 0
    Public Property Correlativo_detalleacuerdo() As Integer = 0
    Public Property Descripcion() As String = ""
    Public Property Numero_unidades() As Double = 0.0
    Public Property Monto() As Double = 0.0
    Public Property Porcentaje() As Double = 0.0
    Public Property Dias_eventos() As Integer = 0
    Public Property Corre_cbcatalogoproductos() As Integer = 0
    Public Property Estado() As Boolean = False
    Public Property Prioridad() As Integer = 0
    Public Property User_agr As Integer = 0
    Public Property Fec_agr As DateTime = Now
    Public Property User_mod As Integer = 0
    Public Property Fec_mod As DateTime = Now
    Public Property IdBodega As Integer = 0
    Public Property IdTipoCobro As Integer = 0


    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
