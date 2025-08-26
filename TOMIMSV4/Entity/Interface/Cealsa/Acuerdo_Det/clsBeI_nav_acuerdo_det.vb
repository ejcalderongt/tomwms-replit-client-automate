Public Class clsBeI_nav_acuerdo_det
    Implements ICloneable

    Public Property IdAcuerdoDet() As Integer = 0
    Public Property IdAcuerdo() As Integer = 0
    Public Property Codigo_producto() As String = ""
    Public Property Servicio() As String = ""
    Public Property Nemonico() As String = ""
    Public Property IdCliente() As Integer = 0
    Public Property Codigo_acuerdo As Integer = 0
    Public Property Correlativo_detalleacuerdo() As Integer = 0
    Public Property Descripcion() As String = ""
    Public Property Numero_unidades() As Integer = 0
    Public Property Monto As Double = 0.00
    Public Property Porcentaje As Double = 0.00
    Public Property Dias_eventos As Integer = 0
    Public Property corre_cbcatalogoproductos As Integer = 0
    Public Property Procesado_wms() As Boolean = False
    Public Property Estado() As Boolean = False
    Public Property Prioridad() As Integer = 0



    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
