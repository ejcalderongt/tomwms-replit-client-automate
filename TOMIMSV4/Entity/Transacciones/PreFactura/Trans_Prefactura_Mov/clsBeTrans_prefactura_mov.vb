Public Class clsBeTrans_prefactura_mov
    Implements ICloneable

    Public Property Idtransprefacturamov() As Integer = 0
    Public Property IdTransPrefacturaEnc() As Integer = 0
    Public Property Poliza_oc_numero_orden() As String = ""
    Public Property Cantidad_tarimas() As Integer = 0
    Public Property Cantidad_cajas() As Double = 0.0
    Public Property Costo_x_caja() As Double = 0.0
    Public Property Almacenaje() As Double = 0.0
    Public Property Manejo() As Double = 0.0
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Activo() As Boolean = False
    Public Property Unidades() As Integer = 0
    Public Property IdOrdenCompraEnc() As Integer = 0
    Public Property Fecha_cobro As Date = Now
    Public Property Valor_total As Double = 0.00
    Public Property Codigo_producto As String = ""

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
