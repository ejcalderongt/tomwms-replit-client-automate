Public Class clsBeVW_Movimientos_Propietario


    Public Property IdTransaccion As Integer
    Public Property TipoTarea As String = ""
    Public Property idpedidoenc As Integer
    Public Property iddespachoenc As Integer
    Public Property IdDespachoDet As Integer
    Public Property activo As Boolean = False
    Public Property observacion As String = ""
    Public Property no_ticket_tms As Integer
    Public Property poliza As String = ""
    Public Property numero_orden As String = ""
    Public Property referencia As String = ""
    Public Property IdRecepcion As Integer
    Public Property valor_aduana As Double = 0.00
    Public Property valor_dai As Double = 0.00
    Public Property valor_iva As Double = 0.00
    Public Property Propietario As String = ""
    Public Property Producto As String = ""
    Public Property presentacion As String = ""
    Public Property UMBas As String = ""
    Public Property cantidad As Integer
    Public Property fecha As DateTime

    Public Property IdProducto As Integer
    Public Property codigo As String = ""
    Public Property codigo_barra As String = ""
    Public Property IdTipoTarea As Integer
    Public Property Contabilizar As Boolean = False
    Public Property fecha_vence As Date
    Public Property IdPresentacion As Integer
    Public Property IdUnidadMedida As Integer
    Public Property IdPropietarioBodega As Integer
    Public Property IdBodega As Integer
    Public Property licencia As String = ""
    Public Property Clasificacion As String = ""
    Public Property Familia As String = ""
    Public Property IdMovimiento As Integer
    Public Property Codigo_Bodega_Origen As String = ""
    Public Property Nombre_Bodega_Origen As String = ""
    Public Property NombreArea As String = ""
    Public Property factor As Integer
    Public Property fecha_ingreso_rec As Date
    Public Property fecha_ingreso_ticket As Date
    Public Property numero_orden_salida As String = ""
    Public Property codigo_poliza_salida As String = ""
    Public Property regimen_ingreso As String = ""
    Public Property regimen_salida As String = ""


End Class
