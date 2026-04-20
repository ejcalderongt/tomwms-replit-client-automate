<Serializable>
Public Class clsBeVW_stock_res
    Implements ICloneable

    Public Property IdBodega() As Integer = 0
    Public Property IdPropietario() As Integer = 0
    Public Property IdPropietarioBodega() As Integer = 0
    Public Property IdProducto() As Integer = 0
    Public Property IdProductoBodega() As Integer = 0
    Public Property IdStock() As Integer = 0
    Public Property IdStockRes() As Integer = 0
    Public Property IdUbicacion() As Integer = 0
    Public Property IdUbicacion_Anterior() As Integer = 0
    Public Property IdUnidadMedida() As Integer = 0
    Public Property IdProductoEstado() As Integer = 0
    Public Property IdPresentacion_Anterior() As Integer = 0
    Public Property IdPresentacion() As Integer = 0
    Public Property IdRecepcionEnc() As Integer = 0
    Public Property Propietario() As String = ""
    Public Property UMBas As String = ""
    Public Property Nombre_Presentacion() As String = ""
    Public Property Codigo_Producto() As String = ""
    Public Property Nombre_Producto() As String = ""
    Public Property Lote() As String = ""
    Public Property Fecha_ingreso() As Date = Date.Now
    Public Property Serial() As String = ""
    Public Property Añada() As Integer = 0
    ''' <summary>
    ''' Cantidad en unidad de medida básica.
    ''' </summary>
    ''' <returns></returns>
    Public Property CantidadUmBas() As Double = 0.0
    Public Property Factor() As Double = 0.0

    ''' <summary>
    ''' Cantidad con presentación
    ''' </summary>
    ''' <returns></returns>
    ''' 
    Public Property CantidadPresentacion() As Double = 0
    Public Property Fecha_Vence() As Date = New Date(1900, 1, 1)
    Public Property NomEstado() As String = ""
    Public Property EstadoUtilizable() As Boolean = False
    Public Property Dañado() As Boolean = False
    Public Property Lic_plate_Anterior() As String = ""
    Public Property Lic_plate() As String = ""
    Public Property Peso() As Double = 0.0
    Public Property IdIndiceRotacion() As Integer = 0
    Public Property AltoUbicacion() As Double = 0.0
    Public Property LargoUbicacion() As Double = 0.0
    Public Property AnchoUbicacion() As Double = 0.0
    Public Property CantidadReservadaUMBas() As Double = 0.0
    Public Property IdTramo() As Integer = 0
    Public Property Ancho_ubicacion() As Double = 0.0
    Public Property Largo_ubicacion() As Double = 0.0
    Public Property Alto_ubicacion() As Double = 0.0
    Public Property IndiceRotacion() As String = ""
    Public Property Existencia_min_umbas() As Double = 0.0
    Public Property Existencia_max_umbas() As Double = 0.0
    Public Property Codigo_Barra() As String = ""
    Public Property Costo() As Double = 0.0
    Public Property Tolerancia As Double = 0
    Public Property Existencia_min_pres As Double = 0
    Public Property Existencia_max_pres As Double = 0
    Public Property Atributo_variante_1 As String = ""
    Public Property Ubicacion_Nivel As Integer = 0
    Public Property Ubicacion_Indice_x As Integer = 0
    Public Property Ubicacion_Nombre As String = ""
    Public Property Ubicacion_Tramo As String = ""
    Public Property Pallet_No_Estandar As Boolean = False
    Public Property Host As String = ""
    Public Property no_linea As Integer = 0
    Public Property Nombre_Clasificacion As String = ""
    Public Property Area As String = ""
    Public Property Nombre_Completo As String = ""
    Public Property IdOperadorBodega_Asignado As Integer = 0
    Public Property IdRecepcionDet As Integer = 0
    Public Property Codigo_Talla As String = ""
    Public Property Nombre_Talla As String = ""
    Public Property Codigo_Color As String = ""
    Public Property Nombre_Color As String = ""
    Public Property IdProductoTallaColor As Integer = 0
    Public Property Proveedor As String = ""
    Sub New()
    End Sub
    Sub New(ByRef IdBodega As Integer, ByVal IdPropietario As Integer, ByVal IdPropietarioBodega As Integer, ByVal IdProducto As Integer, ByVal IdProductoBodega As Integer, ByVal IdStock As Integer,
            ByVal IdUbicacionActual As Integer, ByVal IdUbicacion_anterior As Integer, ByVal IdUnidadMedida As Integer, ByVal IdProductoEstado As Integer, ByVal IdPresentacion As Integer,
            ByVal IdRecepcionEnc As Integer, ByVal Propietario As String, ByVal UnidadMedida As String, ByVal Presentacion As String, ByVal codigo As String, ByVal nombre As String,
            ByVal lote As String, ByVal fecha_ingreso As Date, ByVal serial As String, ByVal añada As Integer, ByVal CantidadSF As Double, ByVal factor As Double, ByVal Cantidad As Double,
            ByVal fecha_vence As Date, ByVal NomEstado As String, ByVal EstadoUtilizable As Boolean, ByVal dañado As Boolean, ByVal IdUbicacion As Integer, ByVal lic_plate As String,
            ByVal peso As Double, ByVal IdIndiceRotacion As Integer, ByVal alto As Double, ByVal largo As Double, ByVal ancho As Double, ByVal CantidadReservada As Double, ByVal IdTramo As Integer,
            ByVal ancho_ubicacion As Double, ByVal largo_ubicacion As Double, ByVal alto_ubicacion As Double, ByVal IndiceRotacion As String, ByVal existencia_min_umbas As Double,
            ByVal existencia_max_umbas As Double, ByVal codigo_barra As String, ByVal costo As Double, ByVal atributo_variante_1 As String, ByVal pallet_no_estandar As Boolean)
        Me.IdBodega = IdBodega
        Me.IdPropietario = IdPropietario
        Me.IdPropietarioBodega = IdPropietarioBodega
        Me.IdProducto = IdProducto
        Me.IdProductoBodega = IdProductoBodega
        Me.IdStock = IdStock
        Me.IdUbicacion = IdUbicacionActual
        Me.IdUbicacion_Anterior = IdUbicacion_anterior
        Me.IdUnidadMedida = IdUnidadMedida
        Me.IdProductoEstado = IdProductoEstado
        Me.IdPresentacion = IdPresentacion
        Me.IdRecepcionEnc = IdRecepcionEnc
        Me.Propietario = Propietario
        Me.UMBas = UnidadMedida
        Me.Nombre_Presentacion = Presentacion
        Me.Codigo_Producto = codigo
        Me.Nombre_Producto = nombre
        Me.Lote = lote
        Me.Fecha_ingreso = fecha_ingreso
        Me.Serial = serial
        Me.Añada = añada
        Me.CantidadUmBas = CantidadSF
        Me.Factor = factor
        Me.CantidadPresentacion = Cantidad
        Me.Fecha_Vence = fecha_vence
        Me.NomEstado = NomEstado
        Me.EstadoUtilizable = EstadoUtilizable
        Me.Dañado = dañado
        Me.IdUbicacion = IdUbicacion
        Me.Lic_plate = lic_plate
        Me.Peso = peso
        Me.IdIndiceRotacion = IdIndiceRotacion
        Me.AltoUbicacion = alto
        Me.LargoUbicacion = largo
        Me.AnchoUbicacion = ancho
        Me.CantidadReservadaUMBas = CantidadReservada
        Me.IdTramo = IdTramo
        Me.Ancho_ubicacion = ancho_ubicacion
        Me.Largo_ubicacion = largo_ubicacion
        Me.Alto_ubicacion = alto_ubicacion
        Me.IndiceRotacion = IndiceRotacion
        Me.Existencia_min_umbas = existencia_min_umbas
        Me.Existencia_max_umbas = existencia_max_umbas
        Me.Codigo_Barra = codigo_barra
        Me.Costo = costo
        Me.Atributo_variante_1 = atributo_variante_1
        Me.Pallet_No_Estandar = pallet_no_estandar
    End Sub
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function
End Class
