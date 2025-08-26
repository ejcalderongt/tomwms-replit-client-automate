Public Class clsBeReabasto

    Public Property IdReabasto As Integer = 0
    Public Property CodigoProducto As String = ""
    Public Property NombreProducto As String = ""
    Public Property Presentacion As String
    Public Property Estado As String
    Public Property Ubicacion As String
    Public Property Minimo As Double
    Public Property Maximo As Double
    Public Property Disponible As Double
    Public Property IdPropietarioBodega As Integer
    Public Property IdProductoBodega As Integer
    Public Property IdPresentacion As Integer
    Public Property IdProductoEstado As Integer
    Public Property IdUnidadMedida As Integer
    Public Property IdUbicacion As Integer
    Public Property IdPropietario As Integer
    Public Property IdBodega As Integer
    Public Property Factor As Double = 0
    Public Property NomUnidMedBas As String
    Public Property DispUbic As Double = 0
    Public Property CantUbicar As Double = 0
    Public Property IdPresentacionAbastercerCon As Integer = 0
    Public Property IdUnidadMedidaBasAbastercerCon As Integer = 0
    Public Property StockInferior As Boolean = False
    Public Property Seleccionado As Boolean = False

End Class
