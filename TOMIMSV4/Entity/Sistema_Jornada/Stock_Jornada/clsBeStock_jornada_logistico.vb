<Serializable>
Public Class clsBeStock_jornada_logistico
    Public Property Fecha As Date = New Date(1900, 1, 1)
    Public Property IdCliente As Integer = 0
    Public Property IdConsolidador As Integer = 0
    Public Property Codigo_Producto As String = ""
    Public Property Nombre_Producto As String = ""
    Public Property Numero_Orden As String = ""
    Public Property Cantidad As Double = 0
    Public Property UMBas As String = ""
    Public Property Almacen As String = ""
    Public Property TipoRubro As String = ""
    Public Property No_Documento_Procesado_ERP As String = ""
    Public Property Bultos_Por_Tarima As Double = 0
    Public Property UMBultos As String = ""

End Class
