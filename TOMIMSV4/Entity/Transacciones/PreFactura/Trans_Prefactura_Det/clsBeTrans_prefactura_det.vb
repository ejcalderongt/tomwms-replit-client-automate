Public Class clsBeTrans_prefactura_det
    Implements ICloneable

    Public Property IdTransPrefacturaDet() As Integer = 0
    Public Property IdTransPrefacturaEnc() As Integer = 0
    Public Property IdAcuerdoEnc() As Integer = 0
    Public Property Codigo_acuerdo_enc() As Integer = 0
    Public Property Codigo_producto_acuerdo_det() As String = ""
    Public Property IdAcuerdoDet() As Integer = 0
    Public Property Correlativo_detalle_acuerdo() As Integer = 0
    Public Property Numero_unidades_acuerdo_det() As Integer = 0
    Public Property Servicio As String = ""
    Public Property Descripcion As String = ""
    Public Property Monto() As Double = 0.0
    Public Property Porcentaje() As Double = 0.0
    Public Property Dias_eventos() As Integer = 0
    Public Property Valor() As Double = 0.0
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Monto_Erp As Double = 0.00
    Public Property Moneda As String = ""

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
