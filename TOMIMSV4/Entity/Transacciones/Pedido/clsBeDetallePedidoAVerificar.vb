Public Class clsBeDetallePedidoAVerificar

    Public Property IdPedidoEnc As Integer
    Public Property IdPedidoDet As Integer
    Public Property IdProductoBodega As Integer
    Public Property Lote As String = ""
    Public Property LicPlate As String = ""
    Public Property Fecha_Vence As DateTime
    Public Property Nom_Presentacion As String = ""
    Public Property Nom_Unid_Med As String = ""
    Public Property Nombre_Producto As String = ""
    Public Property Nom_Estado As String = ""
    Public Property Cantidad_Solicitada As Double = 0
    Public Property Cantidad_Recibida As Double = 0
    Public Property Cantidad_Verificada As Double = 0
    Public Property IdPresentacion As Integer = 0
    Public Property IdUnidadMedidaBasica As Integer = 0
    Public Property IdProductoEstado As Integer = 0
    Public Property Codigo As String = ""
    Public Property NDias As Integer = 0 '#CKFK 20180502 04:15 PM Agregué este campo porque me hace falta para poder listar el inventario disponible al realizar reemplazos

    '#AT 20220303 Se agregó dos nuevos campos: área y clasificación
    Public Property NombreArea As String = ""
    Public Property NombreClasificacion As String = ""
    Public Property Bono As String = ""

End Class
