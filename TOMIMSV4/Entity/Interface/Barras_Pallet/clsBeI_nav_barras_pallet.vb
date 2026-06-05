Public Class clsBeI_nav_barras_pallet
    Implements ICloneable

    Public Property IdPallet() As Integer = 0
    Public Property Codigo() As String = ""
    Public Property Nombre() As String = ""
    Public Property Camas_Por_Tarima() As Integer = 0
    Public Property Cajas_Por_Cama() As Integer = 0
    Public Property Cantidad_Presentacion() As Double = 0

    ''' <summary>
    ''' Cantidad de fardos o cajas por pallet, se emplea especialmente para tarimas parciales que no cumplen la norma estandar de estiba.
    ''' </summary>
    ''' <returns></returns>
    Public Property Cantidad_UMP() As Double = 0
    Public Property UM_Producto() As String = ""
    Public Property Lote() As String = ""
    Public Property Lote_Numerico() As Long = 0
    Public Property Fecha_Agregado() As Date = Date.Now
    Public Property Fecha_Ingreso() As Date = New Date(1900, 1, 1)
    Public Property Fecha_Vence() As Date = New Date(1900, 1, 1)
    Public Property Fecha_Produccion() As Date = New Date(1900, 1, 1)
    Public Property Activo() As Boolean = True
    Public Property Recibido() As Boolean = False
    Public Property IdRecepcion() As Integer = 0
    Public Property Bodega_Origen() As String = ""
    Public Property Bodega_Destino() As String = ""
    Public Property Codigo_barra() As String = ""
    Public Property Fecha_Procesado_ERP() As Date = New Date(1900, 1, 1)
    Public Property Impreso() As Boolean = False
    Public Property SSCC() As String = ""
    Public Property GTIN() As String = ""
    Public Property IdOrdenCompraEnc As Integer = 0
    Public Property IdOrdenCompraDet As Integer = 0
    Public Property Peso As Double = 0
    Public Property cant_etiquetas_presentacion_impresas As Integer = 0

    Sub New()
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
