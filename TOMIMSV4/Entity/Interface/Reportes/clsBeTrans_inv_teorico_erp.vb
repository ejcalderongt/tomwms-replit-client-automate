Public Class clsBeTrans_inv_teorico_erp
    Implements ICloneable

    Public Property Idinvteoricoerp() As Integer = 0
    Public Property IdProducto() As Integer = 0
    Public Property IdPresentacion() As Integer = 0
    Public Property Cant() As Double = 0.0
    Public Property Peso() As Double = 0.0
    Public Property IdUnidadMedida() As Integer = 0
    Public Property Lote() As String = ""
    Public Property Fecha_vence() As Date = Date.Now
    Public Property Codigo() As String = ""
    Public Property Idbodega() As Integer = 0
    Public Property Idubicacion() As Integer = 0
    Public Property Lic_plate() As String = ""
    Public Property Codigo_area() As String = ""
    Public Property Fecha_agr() As Date = Date.Now
    Public Property Usuario_agr() As String = ""

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
