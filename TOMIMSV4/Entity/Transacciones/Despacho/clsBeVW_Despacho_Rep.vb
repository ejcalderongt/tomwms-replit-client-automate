Public Class clsBeVW_Despacho_Rep
    Implements ICloneable

    Public Property IdPickingUbic() As Integer = 0

    Public Property IdStock() As Integer = 0

    Public Property IdPedidoDet() As Integer = 0

    Public Property IdPropietarioBodega() As Integer = 0

    Public Property IdProductoBodega() As Integer = 0

    Public Property IdProductoEstado() As Integer = 0

    Public Property IdPresentacion() As Integer = 0

    Public Property IdUnidadMedida() As Integer = 0

    Public Property IdRecepcion() As Integer = 0

    Public Property IdDespachoEnc() As Integer = 0

    Public Property IdDespachoDet() As Integer = 0

    Public Property IdPedidoEnc() As Integer = 0

    Public Property Propietario() As String = ""

    Public Property Codigo_Producto() As String = ""

    Public Property Nombre_Producto() As String = ""

    Public Property UM() As String = ""

    Public Property Presentacion() As String = ""

    Public Property Factor() As Double = 0.0

    Public Property Estado() As String = ""

    Public Property Lote() As String = ""

    Public Property Licencia() As String = ""

    Public Property Vence() As Date = Date.Now

    Public Property Ubicacion_Origen() As String = ""

    Public Property Cantidad_pickeada() As Double = 0.0

    Public Property Cantidad_verificada() As Double = 0.0

    Public Property Peso_Pickeado() As Double = 0.0

    Public Property Peso_Verificado() As Double = 0.0

    Public Property CantidadDespachada() As Double = 0.0

    Public Property PesoDespachado() As Double = 0.0

    Public Property Encontrado() As Boolean = False

    Public Property Acepto() As Boolean = False

    Public Property No_Documento_WMS() As Integer = 0

    Public Property No_Referencia() As String = ""

    Public Property Codigo_Cliente() As String = ""

    Public Property Nombre_Cliente() As String = ""

    Public Property Idubicacionvirtual() As Integer = 0

    Public Property Es_bodega_recepcion() As Boolean = False

    Public Property Es_bodega_traslado() As Boolean = False

    Public Property No_pase() As Integer = 0

    Public Property Observacion() As String = ""

    Public Property Numero() As Integer = 0

    Public Property Marchamo() As String = ""

    Public Property Codigo_Ruta() As String = ""

    Public Property Nombre_Ruta() As String = ""

    Public Property Placa_Vehiculo() As String = ""

    Public Property Placa_Comercial() As String = ""

    Public Property Marca_Vehiculo() As String = ""

    Public Property Modelo_Vehiculo() As String = ""

    Public Property Nombre_Piloto() As String = ""

    Public Property Apellido_Piloto() As String = ""

    Public Property No_Carnet_Piloto() As String = ""

    Public Property No_Licencia_Piloto() As String = ""

    Public Property Fecha() As Date

    Public Property codigo_poliza_pedido() As String = ""

    '#GT23112022_1400: campos DyD
    Public Property clasificacion() As String = ""
    Public Property marca() As String = ""
    Public Property familia() As String = ""
    Public Property parametro_a() As String = ""
    Public Property parametro_b() As String = ""

    '#16082023: campos pedido-poliza
    Public Property numero_orden_pedido() As String = ""

    '#GT18082023: campos ingreso-poliza
    Public Property numero_orden_ingreso() As String = ""
    Public Property codigo_poliza_ingreso() As String = ""

    '#GT30092024: campos pedido CLC Fiscal
    Public Property codigo_regimen_salida As String = ""
    Public Property placa_contenedor_salida As String = ""
    Public Property Dua_salida As String = ""
    Public Property Talla As String = ""
    Public Property Color As String = ""

    Sub New()
    End Sub

    Sub New(ByRef IdPickingUbic As Integer, ByVal IdStock As Integer, ByVal IdPedidoDet As Integer,
                        ByVal IdPropietarioBodega As Integer, ByVal IdProductoBodega As Integer,
                        ByVal IdProductoEstado As Integer, ByVal IdPresentacion As Integer,
                        ByVal IdUnidadMedida As Integer, ByVal IdRecepcion As Integer,
                        ByVal IdDespachoEnc As Integer, ByVal IdDespachoDet As Integer,
                        ByVal IdPedidoEnc As Integer, ByVal Propietario As String,
                        ByVal Codigo_Producto As String, ByVal Nombre_Producto As String,
                        ByVal UM As String, ByVal Presentacion As String, ByVal factor As Double,
                        ByVal Estado As String, ByVal lote As String, ByVal Vence As Date,
                        ByVal Ubicacion_Origen As String, ByVal cantidad_pickeada As Double,
                        ByVal cantidad_verificada As Double, ByVal Peso_Pickeado As Double,
                        ByVal Peso_Verificado As Double, ByVal CantidadDespachada As Double,
                        ByVal PesoDespachado As Double, ByVal Encontrado As Boolean,
                        ByVal Acepto As Boolean, ByVal No_Documento_WMS As Integer,
                        ByVal No_Referencia As String, ByVal Codigo_Cliente As String,
                        ByVal Nombre_Cliente As String, ByVal idubicacionvirtual As Integer,
                        ByVal es_bodega_recepcion As Boolean, ByVal es_bodega_traslado As Boolean,
                        ByVal no_pase As Integer, ByVal observacion As String, ByVal numero As Integer,
                        ByVal marchamo As String, ByVal Codigo_Ruta As String, ByVal Nombre_Ruta As String,
                        ByVal Placa_Vehiculo As String, ByVal Placa_Comercial As String,
                        ByVal Marca_Vehiculo As String, ByVal Modelo_Vehiculo As String,
                        ByVal Nombre_Piloto As String, ByVal Apellido_Piloto As String,
                        ByVal No_Carnet_Piloto As String, ByVal No_Licencia_Piloto As String,
                        ByVal Fecha As Date, ByVal codigo_poliza_pedido As String,
                        ByVal numero_orden_pedido As String,
                        ByVal codigo_poliza_ingreso As String,
                        ByVal numero_orden_ingreso As String)

        Me.IdPickingUbic = IdPickingUbic
        Me.IdStock = IdStock
        Me.IdPedidoDet = IdPedidoDet
        Me.IdPropietarioBodega = IdPropietarioBodega
        Me.IdProductoBodega = IdProductoBodega
        Me.IdProductoEstado = IdProductoEstado
        Me.IdPresentacion = IdPresentacion
        Me.IdUnidadMedida = IdUnidadMedida
        Me.IdRecepcion = IdRecepcion
        Me.IdDespachoEnc = IdDespachoEnc
        Me.IdDespachoDet = IdDespachoDet
        Me.IdPedidoEnc = IdPedidoEnc
        Me.Propietario = Propietario
        Me.Codigo_Producto = Codigo_Producto
        Me.Nombre_Producto = Nombre_Producto
        Me.UM = UM
        Me.Presentacion = Presentacion
        Me.Factor = factor
        Me.Estado = Estado
        Me.Lote = lote
        Me.Vence = Vence
        Me.Ubicacion_Origen = Ubicacion_Origen
        Me.Cantidad_pickeada = cantidad_pickeada
        Me.Cantidad_verificada = cantidad_verificada
        Me.Peso_Pickeado = Peso_Pickeado
        Me.Peso_Verificado = Peso_Verificado
        Me.CantidadDespachada = CantidadDespachada
        Me.PesoDespachado = PesoDespachado
        Me.Encontrado = Encontrado
        Me.Acepto = Acepto
        Me.No_Documento_WMS = No_Documento_WMS
        Me.No_Referencia = No_Referencia
        Me.Codigo_Cliente = Codigo_Cliente
        Me.Nombre_Cliente = Nombre_Cliente
        Me.Idubicacionvirtual = idubicacionvirtual
        Me.Es_bodega_recepcion = es_bodega_recepcion
        Me.Es_bodega_traslado = es_bodega_traslado
        Me.No_pase = no_pase
        Me.Observacion = observacion
        Me.Numero = numero
        Me.Marchamo = marchamo
        Me.Codigo_Ruta = Codigo_Ruta
        Me.Nombre_Ruta = Nombre_Ruta
        Me.Placa_Vehiculo = Placa_Vehiculo
        Me.Placa_Comercial = Placa_Comercial
        Me.Marca_Vehiculo = Marca_Vehiculo
        Me.Modelo_Vehiculo = Modelo_Vehiculo
        Me.Nombre_Piloto = Nombre_Piloto
        Me.Apellido_Piloto = Apellido_Piloto
        Me.No_Carnet_Piloto = No_Carnet_Piloto
        Me.No_Licencia_Piloto = No_Licencia_Piloto
        Me.Fecha = Fecha
        Me.codigo_poliza_pedido = codigo_poliza_pedido
        Me.numero_orden_pedido = numero_orden_pedido
        Me.codigo_poliza_ingreso = codigo_poliza_ingreso
        Me.numero_orden_ingreso = numero_orden_ingreso
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
