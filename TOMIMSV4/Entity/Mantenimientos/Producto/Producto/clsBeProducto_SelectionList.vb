<Serializable>
Public Class clsBeProducto_SelectionList
    Inherits clsBeProducto

    Public Property Seleccionar As Boolean = False

    Public Sub New()

    End Sub

    Public Sub New(ByRef IdProducto As Integer, ByVal IdPropietario As Integer, ByVal IdClasificacion As Integer, ByVal IdFamilia As Integer,
                   ByVal IdMarca As Integer, ByVal IdTipoProducto As Integer, ByVal IdUnidadMedidaBasica As Integer, ByVal IdCamara As Integer,
                   ByVal IdTipoRotacion As Integer, ByVal IdPerfilSerializado As Integer, ByVal IdIndiceRotacion As Integer,
                   ByVal IdSimbologia As Integer, ByVal IdArancel As Integer, ByVal codigo As String, ByVal nombre As String,
                   ByVal codigo_barra As String, ByVal precio As Double, ByVal existencia_min As Double, ByVal existencia_max As Double,
                   ByVal costo As Double, ByVal peso_referencia As Double, ByVal peso_tolerancia As Double, ByVal temperatura_referencia As Double,
                   ByVal temperatura_tolerancia As Double, ByVal activo As Boolean, ByVal serializado As Boolean, ByVal genera_lote As Boolean,
                   ByVal genera_lp As Boolean, ByVal control_vencimiento As Boolean, ByVal control_lote As Boolean, ByVal peso_recepcion As Boolean,
                   ByVal peso_despacho As Boolean, ByVal temperatura_recepcion As Boolean, ByVal temperatura_despacho As Boolean,
                   ByVal materia_prima As Boolean, ByVal kit As Boolean, ByVal tolerancia As Integer, ByVal ciclo_vida As Integer,
                   ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date,
                   ByVal imagen As Byte(), ByVal NoSerie As String, ByVal NoParte As String, ByVal FechaManufactura As Boolean,
                   ByVal Capturar_Aniada As Boolean, ByVal control_peso As Boolean, ByVal Captura_Arancel As Boolean, ByVal EsHW As Boolean,
                   largo As Double, alto As Double, ancho As Double, ByVal IdPresentacionOrigen As Integer, ByVal IdPresentacionDestino As Integer, ByVal Factor As Double)
        MyBase.New(IdProducto, IdPropietario, IdClasificacion, IdFamilia, IdMarca, IdTipoProducto, IdUnidadMedidaBasica, IdCamara,
                   IdTipoRotacion, IdPerfilSerializado, IdIndiceRotacion, IdSimbologia, IdArancel, codigo, nombre, codigo_barra,
                   precio, existencia_min, existencia_max, costo, peso_referencia, peso_tolerancia, temperatura_referencia,
                   temperatura_tolerancia, activo, serializado, genera_lote, genera_lp, control_vencimiento, control_lote,
                   peso_recepcion, peso_despacho, temperatura_recepcion, temperatura_despacho, materia_prima, kit, tolerancia,
                   ciclo_vida, user_agr, fec_agr, user_mod, fec_mod, imagen, NoSerie, NoParte, FechaManufactura, Capturar_Aniada,
                   control_peso, Captura_Arancel, EsHW, largo, alto, ancho, IdPresentacionOrigen, IdPresentacionDestino, Factor)
    End Sub

End Class