Public Class clsBeTrans_prefactura_enc
    Implements ICloneable

    Public Property IdTransPrefacturaEnc() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property IdPropietarioBodega() As Integer = 0
    Public Property IdClienteBodega() As Integer = 0
    Public Property IdOrdenCompraEnc() As Integer = 0
    Public Property IdTipoCuenta() As Integer = 0
    Public Property Tipo_Cambio() As Double = 0.0
    Public Property IdOrdenCompraPol() As Integer = 0
    Public Property Poliza_oc_numero_orden() As String = ""
    Public Property IdOrdenPedidoEnc() As Integer = 0
    Public Property IdOrdenPedidoPol() As Integer = 0
    Public Property Poliza_pe_numero_orden() As String = ""
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Anulada() As Boolean = False
    Public Property Fecha_desde() As Date = Date.Now
    Public Property Fecha_hasta() As Date = Date.Now
    Public Property Es_consolidador() As Boolean = False
    Public Property Observacion() As String = ""
    Public Property Procesado_erp As Boolean = False
    Public Property Autorizacion_erp As String = ""
    Public Property Cobro_peso_bruto As Boolean = False
    Public Property Variante_cobro As Boolean = False
    Public Property Agrupar_producto As Boolean = False
    Public Property Valor_Aduana As Double = 0.00
    Public Property Valor_General As Double = 0.00
    Public Property Valor_Peso As Double = 0.00

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
