Public Class clsBeVW_Movimientos_Poliza
    Public Property TipoTarea As pTipoTarea
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


    Public Enum pTipoTarea

        RECE = 1
        UBIC = 2
        CEST = 3
        TRAS = 4
        DESP = 5
        INVE = 6
        AJUS = 7
        PIK = 8
        DEVP = 9
        PICK = 10
        VERI = 11
        PACK = 12
        AJCANTP = 13
        AJPESO = 14
        AJVENC = 15
        AJLOTE = 16
        AJCANTN = 17
        AJCANTNI = 18
        AJCANTPI = 19
        EXPLOSION = 20
        UBIC_ANUL_PICK = 21

    End Enum

    Public Property Cantidad_Presentacion As Double = 0
    Public Property Factor As Double = 0
    Public Property Poliza As String = ""
    Public Property IdPropietarioBodega As Integer = 0
    Public Property Clasificacion As String = ""
    Public Property Area_Origen As String = ""

    '#GT24032022: campos para reporte cealsa
    Public Property regimen_ingreso As String = ""
    Public Property no_ticket_tms As Integer = 0
    Public Property fecha_ingreso As String = ""
    Public Property placa_ingreso As String = ""
    Public Property marca_ingreso As String = ""
    Public Property tipo_ingreso As String = ""
    Public Property contenedor_ingreso As String = ""
    Public Property Poliza_Salida As String = ""
    Public Property Fecha_Salida As String = ""
    Public Property placa_salida As String = ""
    Public Property marca_salida As String = ""
    Public Property tipo_salida As String = ""
    Public Property regimen_salida As String = ""
    Public Property contenedor_salida As String = ""
    Public Property NombreArea As String = ""

    Public Property numero_orden As String = ""

End Class
