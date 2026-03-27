Public Class clsBeTrans_ajuste_det_borrador

    Public Sub New()
        lote_original = String.Empty
        lote_nuevo = String.Empty
        codigo_producto = String.Empty
        nombre_producto = String.Empty
        observacion = String.Empty
        codigo_ajuste = String.Empty
        lic_plate = String.Empty
        referencia_ajuste_erp = String.Empty
        estado_borrador = "BORRADOR"
        usuario_creacion = String.Empty
        usuario_modificacion = String.Empty
    End Sub

    Public Property IdAjusteDetBorrador As Integer
    Public Property idajusteenc As Integer
    Public Property idajustedet As Nullable(Of Integer)
    Public Property IdStock As Nullable(Of Integer)
    Public Property IdPropietarioBodega As Nullable(Of Integer)
    Public Property IdProductoBodega As Nullable(Of Integer)
    Public Property IdProductoEstado As Nullable(Of Integer)
    Public Property IdPresentacion As Nullable(Of Integer)
    Public Property IdUnidadMedida As Nullable(Of Integer)
    Public Property IdUbicacion As Nullable(Of Integer)
    Public Property lote_original As String
    Public Property lote_nuevo As String
    Public Property fecha_vence_original As Nullable(Of DateTime)
    Public Property fecha_vence_nueva As Nullable(Of DateTime)
    Public Property peso_original As Nullable(Of Double)
    Public Property peso_nuevo As Nullable(Of Double)
    Public Property cantidad_original As Nullable(Of Double)
    Public Property cantidad_nueva As Nullable(Of Double)
    Public Property codigo_producto As String
    Public Property nombre_producto As String
    Public Property idtipoajuste As Nullable(Of Integer)
    Public Property idmotivoajuste As Nullable(Of Integer)
    Public Property observacion As String
    Public Property codigo_ajuste As String
    Public Property enviado As Nullable(Of Boolean)
    Public Property IdBodegaERP As Nullable(Of Integer)
    Public Property lic_plate As String
    Public Property referencia_ajuste_erp As String
    Public Property estado_ajuste_erp As Boolean
    Public Property estado_borrador As String
    Public Property confirmado As Boolean
    Public Property procesado As Boolean
    Public Property fecha_creacion As DateTime
    Public Property usuario_creacion As String
    Public Property fecha_modificacion As Nullable(Of DateTime)
    Public Property usuario_modificacion As String

End Class