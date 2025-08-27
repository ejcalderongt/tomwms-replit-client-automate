Public Class clsBeI_nav_transacciones_push
    Implements ICloneable

    Public Property IdTransaccionPush() As Integer = 0
    Public Property IdEmpresa() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property IdPropietariobodega() As Integer = 0
    Public Property IdOrdenCompra() As Integer = 0
    Public Property IdRecepcionEnc() As Integer = 0
    Public Property IdRecepcionDet() As Integer = 0
    Public Property Idproductobodega() As Integer = 0
    Public Property Idproducto() As Integer = 0
    Public Property Idunidadmedida() As Integer = 0
    Public Property Idpresentacion() As Integer = 0
    Public Property Idproductoestado() As Integer = 0
    Public Property Cantidad() As Double = 0.0
    Public Property Peso() As Double = 0.0
    Public Property Lote() As String = ""
    Public Property Fecha_vence() As Date = New Date(1900, 1, 1)
    Public Property No_linea() As String = ""
    Public Property Codigo_variante() As String = ""
    Public Property Nom_unidad_medida() As String = ""
    Public Property Tipo_transaccion() As String = ""
    Public Property IdTipoDocumento() As Integer = 0
    Public Property Tipo_push() As String = ""
    Public Property No_recepcion_almacen() As String = ""
    Public Property Documento_ubicacion() As String = ""
    Public Property Documento_ingreso() As String = ""
    Public Property Documento_recepcion() As String = ""
    Public Property Location_code() As String = ""
    Public Property Zone_code() As String = ""
    Public Property Bin_code() As String = ""
    Public Property Assigne_user_id() As String = ""
    Public Property Item_no() As String = ""
    Public Property No_orden_prod() As String = ""
    Public Property Respuesta_push() As String = ""
    Public Property Enviado_A_ERP() As Boolean = False
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_agr() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property User_mod() As String = ""

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
