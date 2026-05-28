Partial Public Class clsBeProducto
    Implements IDisposable

    Public Property IdProductoBodega As Integer
    Public Property Propietario As clsBePropietarios = New clsBePropietarios()
    Public Property Presentacion As New clsBeProducto_Presentacion()
    Public Property Clasificacion As New clsBeProducto_clasificacion()
    Public Property Familia As New clsBeProducto_familia()
    Public Property Marca As New clsBeProducto_marca()
    Public Property TipoProducto As New clsBeProducto_tipo()
    Public Property UnidadMedida As New clsBeUnidad_medida()
    Public Property Arancel As New clsBeArancel()
    Public Property Presentaciones As New List(Of clsBeProducto_Presentacion)
    Public Property Codigos_Barra As New List(Of clsBeProducto_codigos_barra)
    Public Property Parametros As New List(Of clsBeProducto_parametros)
    Public Property IsNew() As Boolean = True
    Public Property Tag As New Object
    Public Property IdPresentacionOrigen As Integer = 0
    Public Property IdPresentacionDestino As Integer = 0
    Public Property Factor As Double = 0.0
    Public Property ExistenciaUMBas As Double = 0
    Public Property Indice_Rotacion As clsBeIndice_rotacion
    Public Property IdProductoTallaColor As Integer = 0
    Public ReadOnly Property Volumen() As Double
        Get
            Return Alto * Largo * Ancho
        End Get
    End Property
    ''' <summary>
    ''' Gets or sets the parámetro A.
    ''' </summary>
    ''' <value>ParametroA.</value>
    Public Property ParametroA As New clsBeProducto_parametro_a()
    ''' <summary>
    ''' Gets or sets the parámetro B.
    ''' </summary>
    ''' <value>ParametroB.</value>
    Public Property ParametroB As New clsBeProducto_parametro_b()
    ''' <summary>
    ''' Enum Temperatura_tolerancia
    ''' </summary>
    Public Enum ProdPropiedades

        ''' <summary>
        ''' The identifier producto
        ''' </summary>
        IdProducto
        ''' <summary>
        ''' The identifier camara
        ''' </summary>
        IdCamara
        ''' <summary>
        ''' The identifier tipo rotacion
        ''' </summary>
        IdTipoRotacion
        ''' <summary>
        ''' The identifier perfil serializado
        ''' </summary>
        IdPerfilSerializado
        ''' <summary>
        ''' The identifier indice rotacion
        ''' </summary>
        IdIndiceRotacion
        ''' <summary>
        ''' The identifier simbologia
        ''' </summary>
        IdSimbologia
        ''' <summary>
        ''' The identifier arancel
        ''' </summary>
        IdArancel
        ''' <summary>
        ''' The codigo
        ''' </summary>
        Codigo
        ''' <summary>
        ''' The nombre
        ''' </summary>
        Nombre
        ''' <summary>
        ''' The codigo_barra
        ''' </summary>
        Codigo_barra
        ''' <summary>
        ''' The precio
        ''' </summary>
        Precio
        ''' <summary>
        ''' The existencia_min
        ''' </summary>
        Existencia_min
        ''' <summary>
        ''' The existencia_max
        ''' </summary>
        Existencia_max
        ''' <summary>
        ''' The costo
        ''' </summary>
        Costo
        ''' <summary>
        ''' The peso_referencia
        ''' </summary>
        Peso_referencia
        ''' <summary>
        ''' The peso_tolerancia
        ''' </summary>
        Peso_tolerancia
        ''' <summary>
        ''' The temperatura_referencia
        ''' </summary>
        Temperatura_referencia
        ''' <summary>
        ''' The temperatura_tolerancia
        ''' </summary>
        Temperatura_tolerancia
        ''' <summary>
        ''' The activo
        ''' </summary>
        Activo
        ''' <summary>
        ''' The serializado
        ''' </summary>
        Serializado
        ''' <summary>
        ''' The genera_lote
        ''' </summary>
        Genera_lote
        ''' <summary>
        ''' The genera_ lp
        ''' </summary>
        Genera_LP
        ''' <summary>
        ''' The control_vencimiento
        ''' </summary>
        Control_vencimiento
        ''' <summary>
        ''' The control_lote
        ''' </summary>
        Control_lote
        ''' <summary>
        ''' The peso_recepcion
        ''' </summary>
        Peso_recepcion
        ''' <summary>
        ''' The peso_despacho
        ''' </summary>
        Peso_despacho
        ''' <summary>
        ''' The temperatura_recepcion
        ''' </summary>
        Temperatura_recepcion
        ''' <summary>
        ''' The temperatura_despacho
        ''' </summary>
        Temperatura_despacho
        ''' <summary>
        ''' The materia_prima
        ''' </summary>
        Materia_prima
        ''' <summary>
        ''' The kit
        ''' </summary>
        Kit
        ''' <summary>
        ''' The tolerancia
        ''' </summary>
        Tolerancia
        ''' <summary>
        ''' The ciclo_vida
        ''' </summary>
        Ciclo_vida
        ''' <summary>
        ''' The user_agr
        ''' </summary>
        User_agr
        ''' <summary>
        ''' The fec_agr
        ''' </summary>
        Fec_agr
        ''' <summary>
        ''' The user_mod
        ''' </summary>
        User_mod
        ''' <summary>
        ''' The fec_mod
        ''' </summary>
        Fec_mod
        ''' <summary>
        ''' The imagen
        ''' </summary>
        Imagen
        ''' <summary>
        ''' The no serie
        ''' </summary>
        NoSerie
        ''' <summary>
        ''' The no parte
        ''' </summary>
        NoParte
        ''' <summary>
        ''' The fecha manufactura
        ''' </summary>
        FechaManufactura
        ''' <summary>
        ''' The capturar_ aniada
        ''' </summary>
        Capturar_Aniada
        ''' <summary>
        ''' The control_ peso
        ''' </summary>
        Control_Peso
        ''' <summary>
        ''' The captura_ arancel
        ''' </summary>
        Captura_Arancel
        ''' <summary>
        ''' The es_ hardware
        ''' </summary>
        Es_Hardware
        ''' <summary>
        ''' The identifier presentacion origen
        ''' </summary>
        IdPresentacionOrigen
        ''' <summary>
        ''' The identifier presentacion destino
        ''' </summary>
        IdPresentacionDestino
        ''' <summary>
        ''' The factor
        ''' </summary>
        Factor
        ''' <summary>
        ''' The identifier producto bodega
        ''' </summary>
        IdProductoBodega
        ''' <summary>
        ''' The propietario
        ''' </summary>
        Propietario
        ''' <summary>
        ''' The clasificacion
        ''' </summary>
        Clasificacion
        ''' <summary>
        ''' The familia
        ''' </summary>
        Familia
        ''' <summary>
        ''' The marca
        ''' </summary>
        Marca
        ''' <summary>
        ''' The tipo producto
        ''' </summary>
        TipoProducto
        ''' <summary>
        ''' The unidad medida
        ''' </summary>
        UnidadMedida
        ''' <summary>
        ''' The arancel
        ''' </summary>
        Arancel
        ''' <summary>
        ''' The presentaciones
        ''' </summary>
        Presentaciones
        ''' <summary>
        ''' The codigos_ barra
        ''' </summary>
        Codigos_Barra
        ''' <summary>
        ''' The parametros
        ''' </summary>
        Parametros
        ''' <summary>
        ''' The is new
        ''' </summary>
        IsNew
        ''' <summary>
        ''' The alto
        ''' </summary>
        Alto
        ''' <summary>
        ''' The largo
        ''' </summary>
        Largo
        ''' <summary>
        ''' The ancho
        ''' </summary>
        Ancho

        IdUnidadMedidaCobro

        Dias_Inventario_Promedio

        IdProductoParametroA

        IdProductoParametroB
        ''' <summary>
        ''' The parámetro A
        ''' </summary>
        ParametroA
        ''' <summary>
        ''' The parámetro B
        ''' </summary>
        ParametroB

        IdTipoManufactura

    End Enum

    'Campos usados para inventario ciclico
    Public Property Lote As String = ""
    Public Property FechaVence As Date = New Date(1900, 1, 1)
    Public Property Cantidad As Double = 0

    '#EJC202210150821: Para evitar sobrecargar el objeto para la HH
    Public Property Stock As New clsBeVW_stock_res()

End Class
