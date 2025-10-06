Public Class clsBeTrans_packing_enc
    Implements ICloneable

    Public Property Idpackingenc() As Integer = 0
    Public Property Idbodega() As Integer = 0
    Public Property Idpickingenc() As Integer = 0
    Public Property IdDespachoEnc() As Integer = 0
    Public Property Idproductobodega() As Integer = 0
    Public Property Idproductoestado() As Integer = 0
    Public Property Idpresentacion() As Integer = 0
    Public Property Idunidadmedida() As Integer = 0
    Public Property Lote() As String = ""
    Public Property Fecha_vence() As String = New Date(1900, 1, 1)
    Public Property Lic_plate() As String = ""
    Public Property No_linea() As String = ""
    Public Property Cantidad_bultos_packing() As Double = 0.0
    Public Property Cantidad_camas_packing() As Double = 0.0
    Public Property Idoperadorbodega() As Integer = 0
    Public Property Idempresaservicio() As Integer = 0
    Public Property Referencia() As String = ""
    Public Property Fecha_packing() As String = New Date(1900, 1, 1)
    Public Property IdPedidoEnc() As Integer = 0
    Public Property Finalizado() As Boolean = False
    Public Property Fec_agr As String = New Date(1900, 1, 1)
    Public Property Usr_agr As String = ""
    Public Property Fec_mod As String = New Date(1900, 1, 1)
    Public Property Usr_mod As String = ""
    Public Property IdProductoTallaColor As Integer = 0

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
