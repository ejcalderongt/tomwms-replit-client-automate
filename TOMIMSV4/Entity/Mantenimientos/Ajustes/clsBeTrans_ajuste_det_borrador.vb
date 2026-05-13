Public Class clsBeTrans_ajuste_det_borrador

    Public Sub New()
        IdAjusteDetBorrador = 0
        idajusteenc = 0
        idajustedet = 0
        IdStock = 0
        IdPropietarioBodega = 0
        IdProductoBodega = 0
        IdProductoEstado = 0
        IdPresentacion = 0
        IdUnidadMedida = 0
        IdUbicacion = 0

        lote_original = String.Empty
        lote_nuevo = String.Empty

        fecha_vence_original = New Date(1900, 1, 1)
        fecha_vence_nueva = New Date(1900, 1, 1)

        peso_original = 0
        peso_nuevo = 0
        cantidad_original = 0
        cantidad_nueva = 0

        codigo_producto = String.Empty
        nombre_producto = String.Empty

        idtipoajuste = 0
        idmotivoajuste = 0

        observacion = String.Empty
        codigo_ajuste = String.Empty
        enviado = False
        IdBodegaERP = 0
        lic_plate = String.Empty
        referencia_ajuste_erp = String.Empty
        estado_ajuste_erp = False
        estado_borrador = "BORRADOR"
        confirmado = False
        procesado = False
        fecha_creacion = New Date(1900, 1, 1)
        usuario_creacion = String.Empty
        fecha_modificacion = New Date(1900, 1, 1)
        usuario_modificacion = String.Empty

        Nombre_Presentacion = String.Empty
        Factor = 0
        UmBas = String.Empty

        IdProductoTallaColor_origen = 0
        Color_origen = String.Empty
        IdProductoTallaColor_destino = 0
        Talla_destino = String.Empty
        Color_destino = String.Empty
        Talla_origen = String.Empty

        idstocklink = 0
        CantReservada = 0
        idstockres = 0
        esnuevolink = 0

        '#FIX_v20_PROVEEDOR_PERSIST_2026-04-25
        IdProveedor = 0
        Codigo_Proveedor = String.Empty
        Nombre_Proveedor = String.Empty

        Presentacion = New clsBeProducto_Presentacion()
    End Sub

    Public Property IdAjusteDetBorrador As Integer
    Public Property idajusteenc As Integer
    Public Property idajustedet As Integer
    Public Property IdStock As Integer
    Public Property IdPropietarioBodega As Integer
    Public Property IdProductoBodega As Integer
    Public Property IdProductoEstado As Integer
    Public Property IdPresentacion As Integer
    Public Property IdUnidadMedida As Integer
    Public Property IdUbicacion As Integer
    Public Property lote_original As String
    Public Property lote_nuevo As String
    Public Property fecha_vence_original As DateTime
    Public Property fecha_vence_nueva As DateTime
    Public Property peso_original As Double
    Public Property peso_nuevo As Double
    Public Property cantidad_original As Double
    Public Property cantidad_nueva As Double
    Public Property codigo_producto As String
    Public Property nombre_producto As String
    Public Property idtipoajuste As Integer
    Public Property idmotivoajuste As Integer
    Public Property observacion As String
    Public Property codigo_ajuste As String
    Public Property enviado As Boolean
    Public Property IdBodegaERP As Integer
    Public Property lic_plate As String
    Public Property referencia_ajuste_erp As String
    Public Property estado_ajuste_erp As Boolean
    Public Property estado_borrador As String
    Public Property confirmado As Boolean
    Public Property procesado As Boolean
    Public Property fecha_creacion As DateTime
    Public Property usuario_creacion As String
    Public Property fecha_modificacion As DateTime
    Public Property usuario_modificacion As String
    Public Property Nombre_Presentacion As String
    Public Property Factor As Double
    Public Property UmBas As String
    Public Property IdProductoTallaColor_origen As Integer
    Public Property Color_origen As String
    Public Property IdProductoTallaColor_destino As Integer
    Public Property Talla_destino As String
    Public Property Color_destino As String
    Public Property Talla_origen As String
    Public Property idstocklink As Integer
    Public Property CantReservada As Object
    Public Property idstockres As Integer
    Public Property esnuevolink As Integer

    '#FIX_v20_PROVEEDOR_PERSIST_2026-04-25
    Public Property IdProveedor As Integer
    Public Property Codigo_Proveedor As String
    Public Property Nombre_Proveedor As String

    Public Property Presentacion As clsBeProducto_Presentacion
    Public Property IdRecepcionEnc As Integer = 0
End Class