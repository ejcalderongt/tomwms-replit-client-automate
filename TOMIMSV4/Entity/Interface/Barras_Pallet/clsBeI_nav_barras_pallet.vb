Public Class clsBeI_nav_barras_pallet
    Implements ICloneable

    Public Property IdPallet() As Integer = 0

    Public Property Codigo() As String = ""

    Public Property Nombre() As String = ""

    Public Property Camas_Por_Tarima() As Integer = 0

    Public Property Cajas_Por_Cama() As Integer = 0

    Public Property Cantidad_Presentacion() As Double = 0

    ''' <summary>
    ''' Cantidad de fardos o cajas por pallet, se emplea especialmente para tarimas parciales que no cumplen la norma estándar de estiba.
    ''' </summary>
    ''' <returns></returns>
    Public Property Cantidad_UMP() As Double = 0


    Public Property UM_Producto() As String = ""

    Public Property Lote() As String = ""

    Public Property Lote_Numerico() As Long = 0

    Public Property Fecha_Agregado() As Date = Date.Now

    Public Property Fecha_Ingreso() As Date = "01/01/1990"

    Public Property Fecha_Vence() As Date = "01/01/1990"

    Public Property Fecha_Produccion() As Date = "01/01/1990"

    Public Property Activo() As Boolean = True

    Public Property Recibido() As Boolean = False

    Public Property IdRecepcion() As Integer = 0

    Public Property Bodega_Origen() As String = ""

    Public Property Bodega_Destino() As String = ""

    Public Property Codigo_barra() As String = ""

    Sub New()
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
