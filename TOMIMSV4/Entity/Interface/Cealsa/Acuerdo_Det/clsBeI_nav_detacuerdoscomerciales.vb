Public Class clsBeI_nav_detacuerdoscomerciales
    Implements ICloneable

    Public Property Emp() As Integer = 0
    Public Property Nombre_emp() As String = ""
    Public Property Codcliente() As Integer = 0
    Public Property Nomcliente() As String = ""
    Public Property Codacuerdo() As Integer = 0
    Public Property Descrip() As String = ""
    Public Property Tipocobro() As String = ""
    Public Property Codmoneda() As Integer = 0
    Public Property Moneda() As String = ""
    Public Property Codigo_producto() As String = ""
    Public Property Servicio() As String = ""
    Public Property Nemonico() As String = ""
    Public Property Corre_detalleacuerdo() As Integer = 0
    Public Property Corre_catalogoproductos() As Integer = 0
    Public Property Unid_medida() As Integer = 0
    Public Property Nombre_unidad() As String = ""
    Public Property Procesado_wms() As Boolean = False
    Public Property Estado() As String = ""

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
