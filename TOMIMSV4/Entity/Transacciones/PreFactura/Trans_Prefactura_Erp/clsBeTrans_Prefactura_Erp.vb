Public Class clsBeTrans_prefactura_erp
    Implements ICloneable

    Public Property IdPrefacturaEnc As Integer
    Public Property Nit As String
    Public Property IdCliente_facturar As Integer
    Public Property Codigo_acuerdo As Integer
    Public Property IdCliente As Integer
    Public Property Moneda As String
    Public Property Periodo As String
    Public Property Mercaderia As String
    Public Property TipoCambio As Double
    Public Property Observaciones As String
    Public Property Detalle As List(Of clsBeTrans_prefactura_erp_det)

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class