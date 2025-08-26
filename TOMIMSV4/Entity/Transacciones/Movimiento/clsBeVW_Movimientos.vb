Public Class clsBeVW_Movimientos
    Public Property TipoTarea As clsDataContractDI.tTipoTarea
    Public Property TTarea As String
    Public Property IdProducto As Integer
    Public Property IdRecepcion As Integer
    Public Property IdRecepcionOC As Integer
    Public Property IdBodegaOrigen As Integer
    Public Property IdUbicacionOrigen As Integer
    Public Property IdUbicacionDestino As Integer
    Public Property Codigo As String
    Public Property CodigoBarra As String
    Public Property Producto As String
    Public Property Propietario As String
    Public Property Presentacion As String
    Public Property EstadoOrigen As String
    Public Property EstadoDestino As String
    Public Property Motivo As String
    Public Property UMBas As String
    Public Property UbicOrigen As String
    Public Property UbicDestino As String
    Public Property Cantidad As Double
    Public Property Peso As Double
    Public Property Lote As String
    Public Property Fecha_Vence As Date
    Public Property Fecha As Date
    Public Property IdTipoTarea As Integer
    Public Property No_Doc_Ingreso As String '#CKFK 20180613 Agregué el número del documento donde se recepcionó el producto
    Public Property No_Ref_Ingreso As String '#CKFK 20180613 Agregué el número de referencia donde se recepcionó el producto
    Public Property No_Doc_Salida As String '#CKFK 20180615 Agregué el número del documento donde se recepcionó el producto
    Public Property No_Ref_Salida As String '#CKFK 20180615 Agregué el número de referencia donde se recepcionó el producto
    Public Property IdUnidadMedida As Integer = 0 '#EJC20180616: Cálculo de stock en una fecha
    Public Property IdPresentacion As Integer = 0 '#EJC20180616: Cálculo de stock en una fecha
    Public Property IdEstadoOrigen As Integer '#EJC20180616: Cálculo de stock en una fecha
    Public Property IdProductoBodega As Integer '#EJC20180616: Cálculo de stock en una fecha
    Public Property Ingresos As Double = 0
    Public Property Salidas As Double = 0
    Public Property Ajustes_Positivos As Double = 0
    Public Property Ajustes_Negativos As Double = 0
    Public Property Lic_Plate As String = ""
    ''' <summary>
    ''' #EJC20250415: Variable para regularización de inventario cíclico.
    ''' </summary>
    ''' <returns></returns>
    Public Property EnMovimiento As Double = 0
    Public Property Fecha_Conteo As Date = New Date(1900.1, 1)

End Class
