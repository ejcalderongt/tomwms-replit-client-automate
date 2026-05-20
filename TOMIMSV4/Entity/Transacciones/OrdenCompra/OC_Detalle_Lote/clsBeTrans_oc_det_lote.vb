Public Class clsBeTrans_oc_det_lote
    Implements ICloneable

    Public Property IdOrdenCompraEnc() As Integer = 0
    Public Property IdOrdenCompraDet() As Integer = 0
    Public Property IdOrdenCompraDetLote() As Integer = 0
    Public Property IdProductoBodega() As Integer = 0
    Public Property No_linea() As Integer = 0
    Public Property Codigo_producto() As String = ""
    Public Property Cantidad() As Double = 0.0
    Public Property Cantidad_recibida() As Double = 0.0
    Public Property Lote() As String = ""
    Public Property Lic_Plate As String = ""
    Public Property Fecha_vence() As Date = New Date(1900, 1, 1)
    Public Property Ubicacion As String = ""
    Public Property IdPresentacion() As Integer = 0
    Public Property IdUnidadMedidaBasica() As Integer = 0
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now

    '#CKFK20220212 Se agregó este campo para cuando en la recepción de une devolución de venta no se reciba el mismo lote y se tenga que enviar el correcto al ERP
    Public Property Reclasificar As Boolean = False
    Public Property IsNew As Boolean = False
    Public Property Activo As Boolean = True
    Public Property No_Documento As String = ""
    Public Property Codigo_Sku As String = ""
    Public Property IdProductoTallaColor As Integer = 0
    Public Property Talla As String = ""
    Public Property Color As String = ""

    Public Peso_Licencia As Double = 0

    Sub New()
    End Sub

    Sub New(ByRef IdOrdenCompraEnc As Integer, ByVal IdOrdenCompraDet As Integer, ByVal IdOrdenCompraDetLote As Integer, ByVal IdProductoBodega As Integer, ByVal no_linea As Integer, ByVal codigo_producto As String, ByVal cantidad As Double, ByVal cantidad_recibida As Double, ByVal lote As String, ByVal fecha_vence As String)
        Me.IdOrdenCompraEnc = IdOrdenCompraEnc
        Me.IdOrdenCompraDet = IdOrdenCompraDet
        Me.IdOrdenCompraDetLote = IdOrdenCompraDetLote
        Me.IdProductoBodega = IdProductoBodega
        Me.No_linea = no_linea
        Me.Codigo_producto = codigo_producto
        Me.Cantidad = cantidad
        Me.Cantidad_recibida = cantidad_recibida
        Me.Lote = lote
        Me.Fecha_vence = fecha_vence
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
