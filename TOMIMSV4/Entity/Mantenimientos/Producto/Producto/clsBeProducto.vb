<Serializable>
Public Class clsBeProducto
    Implements ICloneable
    Implements IDisposable
    ''' <summary>
    ''' Gets or sets the identifier producto.
    ''' </summary>
    ''' <value>The identifier producto.</value>
    Public Property IdProducto() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier propietario.
    ''' </summary>
    ''' <value>The identifier propietario.</value>
    Public Property IdPropietario() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier clasificacion.
    ''' </summary>
    ''' <value>The identifier clasificacion.</value>
    Public Property IdClasificacion() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier familia.
    ''' </summary>
    ''' <value>The identifier familia.</value>
    Public Property IdFamilia() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier marca.
    ''' </summary>
    ''' <value>The identifier marca.</value>
    Public Property IdMarca() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier tipo producto.
    ''' </summary>
    ''' <value>The identifier tipo producto.</value>
    Public Property IdTipoProducto() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier unidad medida basica.
    ''' </summary>
    ''' <value>The identifier unidad medida basica.</value>
    Public Property IdUnidadMedidaBasica() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier camara.
    ''' </summary>
    ''' <value>The identifier camara.</value>
    Public Property IdCamara() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier tipo rotacion.
    ''' </summary>
    ''' <value>The identifier tipo rotacion.</value>
    Public Property IdTipoRotacion() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier perfil serializado.
    ''' </summary>
    ''' <value>The identifier perfil serializado.</value>
    Public Property IdPerfilSerializado() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier indice rotacion.
    ''' </summary>
    ''' <value>The identifier indice rotacion.</value>
    Public Property IdIndiceRotacion() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier simbologia.
    ''' </summary>
    ''' <value>The identifier simbologia.</value>
    Public Property IdSimbologia() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier arancel.
    ''' </summary>
    ''' <value>The identifier arancel.</value>
    Public Property IdArancel() As Integer = 0
    ''' <summary>
    ''' Gets or sets the codigo.
    ''' </summary>
    ''' <value>The codigo.</value>
    Public Property Codigo() As String = ""
    ''' <summary>
    ''' Gets or sets the nombre.
    ''' </summary>
    ''' <value>The nombre.</value>
    Public Property Nombre() As String = ""
    ''' <summary>
    ''' Gets or sets the codigo_barra.
    ''' </summary>
    ''' <value>The codigo_barra.</value>
    Public Property Codigo_barra() As String = ""
    ''' <summary>
    ''' Gets or sets the precio.
    ''' </summary>
    ''' <value>The precio.</value>
    Public Property Precio() As Double = 0.0
    ''' <summary>
    ''' Gets or sets the existencia_min.
    ''' </summary>
    ''' <value>The existencia_min.</value>
    Public Property Existencia_min() As Double = 0.0
    ''' <summary>
    ''' Gets or sets the existencia_max.
    ''' </summary>
    ''' <value>The existencia_max.</value>
    Public Property Existencia_max() As Double = 0.0
    ''' <summary>
    ''' Gets or sets the costo.
    ''' </summary>
    ''' <value>The costo.</value>
    Public Property Costo() As Double = 0.0
    ''' <summary>
    ''' Gets or sets the peso_referencia.
    ''' </summary>
    ''' <value>The peso_referencia.</value>
    Public Property Peso_referencia() As Double = 0.0
    ''' <summary>
    ''' Gets or sets the peso_tolerancia.
    ''' </summary>
    ''' <value>The peso_tolerancia.</value>
    Public Property Peso_tolerancia() As Double = 0.0
    ''' <summary>
    ''' Gets or sets the temperatura_referencia.
    ''' </summary>
    ''' <value>The temperatura_referencia.</value>
    Public Property Temperatura_referencia() As Double = 0.0
    ''' <summary>
    ''' Gets or sets the temperatura_tolerancia.
    ''' </summary>
    ''' <value>The temperatura_tolerancia.</value>
    Public Property Temperatura_tolerancia() As Double = 0.0
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeProducto"/> is activo.
    ''' </summary>
    ''' <value><c>true</c> if activo; otherwise, <c>false</c>.</value>
    Public Property Activo() As Boolean = False
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeProducto"/> is serializado.
    ''' </summary>
    ''' <value><c>true</c> if serializado; otherwise, <c>false</c>.</value>
    Public Property Serializado() As Boolean = False
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeProducto"/> is genera_lote.
    ''' </summary>
    ''' <value><c>true</c> if genera_lote; otherwise, <c>false</c>.</value>
    Public Property Genera_lote() As Boolean = False
    ''' <summary>
    ''' #EJC20210426: Gets or sets a value indicating whether this <see cref="clsBeProducto"/> is genera_lp for umbas
    ''' Reactivado para CEALSA
    ''' </summary>
    ''' <value><c>true</c> if genera_lp; otherwise, <c>false</c>.</value>
    Public Property Genera_lp() As Boolean = False
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeProducto"/> is control_vencimiento.
    ''' </summary>
    ''' <value><c>true</c> if control_vencimiento; otherwise, <c>false</c>.</value>
    Public Property Control_vencimiento() As Boolean = False
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeProducto"/> is control_lote.
    ''' </summary>
    ''' <value><c>true</c> if control_lote; otherwise, <c>false</c>.</value>
    Public Property Control_lote() As Boolean = False
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeProducto"/> is peso_recepcion.
    ''' </summary>
    ''' <value><c>true</c> if peso_recepcion; otherwise, <c>false</c>.</value>
    Public Property Peso_recepcion() As Boolean = False
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeProducto"/> is peso_despacho.
    ''' </summary>
    ''' <value><c>true</c> if peso_despacho; otherwise, <c>false</c>.</value>
    Public Property Peso_despacho() As Boolean = False
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeProducto"/> is temperatura_recepcion.
    ''' </summary>
    ''' <value><c>true</c> if temperatura_recepcion; otherwise, <c>false</c>.</value>
    Public Property Temperatura_recepcion() As Boolean = False
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeProducto"/> is temperatura_despacho.
    ''' </summary>
    ''' <value><c>true</c> if temperatura_despacho; otherwise, <c>false</c>.</value>
    Public Property Temperatura_despacho() As Boolean = False
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeProducto"/> is materia_prima.
    ''' </summary>
    ''' <value><c>true</c> if materia_prima; otherwise, <c>false</c>.</value>
    Public Property Materia_prima() As Boolean = False
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeProducto"/> is kit.
    ''' </summary>
    ''' <value><c>true</c> if kit; otherwise, <c>false</c>.</value>
    Public Property Kit() As Boolean = False
    ''' <summary>
    ''' Gets or sets the tolerancia.
    ''' </summary>
    ''' <value>The tolerancia.</value>
    Public Property Tolerancia() As Integer = 0
    ''' <summary>
    ''' Gets or sets the ciclo_vida.
    ''' </summary>
    ''' <value>The ciclo_vida.</value>
    Public Property Ciclo_vida() As Integer = 0
    ''' <summary>
    ''' Gets or sets the user_agr.
    ''' </summary>
    ''' <value>The user_agr.</value>
    Public Property User_agr() As String = ""
    ''' <summary>
    ''' Gets or sets the fec_agr.
    ''' </summary>
    ''' <value>The fec_agr.</value>
    Public Property Fec_agr() As Date = Date.Now
    ''' <summary>
    ''' Gets or sets the user_mod.
    ''' </summary>
    ''' <value>The user_mod.</value>
    Public Property User_mod() As String = ""
    ''' <summary>
    ''' Gets or sets the fec_mod.
    ''' </summary>
    ''' <value>The fec_mod.</value>
    Public Property Fec_mod() As Date = Date.Now
    ''' <summary>
    ''' Gets or sets the imagen.
    ''' </summary>
    ''' <value>The imagen.</value>
    Public Property Imagen() As Byte() = Nothing
    ''' <summary>
    ''' Gets or sets the noserie.
    ''' </summary>
    ''' <value>The noserie.</value>
    Public Property Noserie() As String = ""
    ''' <summary>
    ''' Gets or sets the noparte.
    ''' </summary>
    ''' <value>The noparte.</value>
    Public Property Noparte() As String = ""
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeProducto"/> is fechamanufactura.
    ''' </summary>
    ''' <value><c>true</c> if fechamanufactura; otherwise, <c>false</c>.</value>
    Public Property Fechamanufactura() As Boolean = False
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeProducto"/> is capturar_aniada.
    ''' </summary>
    ''' <value><c>true</c> if capturar_aniada; otherwise, <c>false</c>.</value>
    Public Property Capturar_aniada() As Boolean = False
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeProducto"/> is control_peso.
    ''' </summary>
    ''' <value><c>true</c> if control_peso; otherwise, <c>false</c>.</value>
    Public Property Control_peso() As Boolean = False
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeProducto"/> is captura_arancel.
    ''' </summary>
    ''' <value><c>true</c> if captura_arancel; otherwise, <c>false</c>.</value>
    Public Property Captura_arancel() As Boolean = False
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeProducto"/> is es_hardware.
    ''' </summary>
    ''' <value><c>true</c> if es_hardware; otherwise, <c>false</c>.</value>
    Public Property Es_hardware() As Boolean = False
    ''' <summary>
    ''' Gets or sets the largo.
    ''' </summary>
    ''' <value>The largo.</value>
    Public Property Largo() As Double = 0.0
    ''' <summary>
    ''' Gets or sets the alto.
    ''' </summary>
    ''' <value>The alto.</value>
    Public Property Alto() As Double = 0.0
    ''' <summary>
    ''' Gets or sets the ancho.
    ''' </summary>
    ''' <value>The ancho.</value>
    Public Property Ancho() As Double = 0.0

    ''' <summary>
    ''' Utilizada en CEALSA para la gestión de las tarifas/servicios y unidade de cobranza.
    ''' </summary>
    ''' <returns></returns>
    Public Property IdUnidadMedidaCobro As Integer = 0
    Public Property IdTipoEtiqueta As Integer = 0

    ''' <summary>
    ''' --#EJC20211206: Para cálculo de índice de rotación.
    ''' </summary>
    ''' <returns></returns>
    Public Property Dias_Inventario_Promedio As Integer = 90

    ''' <summary>
    ''' #EJC20220630: Agregado para DYD Se utilizará para Lado.
    ''' </summary>
    ''' <returns></returns>
    Public Property IdProductoParametroA As Integer = 0

    ''' <summary>
    ''' #EJC20220630: Agregado para DYD Se utilizará para Es_Nuevo.
    ''' </summary>
    ''' <returns></returns>
    Public Property IdProductoParametroB As Integer = 0

    ''' <summary>
    ''' #GT26032024: agregado para identificar si aplica a manufactura ligera
    ''' </summary>
    ''' <returns></returns>
    Public Property IdTipoManufactura As Integer = 0
    ''' <summary>
    ''' #AT20250313: se agrego para tener el margen de impresion en sojet
    ''' </summary>
    ''' <returns></returns>
    Public Property Margen_Impresion As Double = 0


    ''' <summary>
    ''' Creates a new object that is a copy of the current instance.
    ''' </summary>
    ''' <returns>A new object that is a copy of this instance.</returns>
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function
    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeProducto"/> class.
    ''' </summary>
    Sub New()
    End Sub
    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeProducto"/> class.
    ''' </summary>
    ''' <param name="IdProducto">The identifier producto.</param>
    ''' <param name="IdPropietario">The identifier propietario.</param>
    ''' <param name="IdClasificacion">The identifier clasificacion.</param>
    ''' <param name="IdFamilia">The identifier familia.</param>
    ''' <param name="IdMarca">The identifier marca.</param>
    ''' <param name="IdTipoProducto">The identifier tipo producto.</param>
    ''' <param name="IdUnidadMedidaBasica">The identifier unidad medida basica.</param>
    ''' <param name="IdCamara">The identifier camara.</param>
    ''' <param name="IdTipoRotacion">The identifier tipo rotacion.</param>
    ''' <param name="IdPerfilSerializado">The identifier perfil serializado.</param>
    ''' <param name="IdIndiceRotacion">The identifier indice rotacion.</param>
    ''' <param name="IdSimbologia">The identifier simbologia.</param>
    ''' <param name="IdArancel">The identifier arancel.</param>
    ''' <param name="codigo">The codigo.</param>
    ''' <param name="nombre">The nombre.</param>
    ''' <param name="codigo_barra">The codigo_barra.</param>
    ''' <param name="precio">The precio.</param>
    ''' <param name="existencia_min">The existencia_min.</param>
    ''' <param name="existencia_max">The existencia_max.</param>
    ''' <param name="costo">The costo.</param>
    ''' <param name="peso_referencia">The peso_referencia.</param>
    ''' <param name="peso_tolerancia">The peso_tolerancia.</param>
    ''' <param name="temperatura_referencia">The temperatura_referencia.</param>
    ''' <param name="temperatura_tolerancia">The temperatura_tolerancia.</param>
    ''' <param name="activo">if set to <c>true</c> [activo].</param>
    ''' <param name="serializado">if set to <c>true</c> [serializado].</param>
    ''' <param name="genera_lote">if set to <c>true</c> [genera_lote].</param>
    ''' <param name="genera_lp">if set to <c>true</c> [genera_lp].</param>
    ''' <param name="control_vencimiento">if set to <c>true</c> [control_vencimiento].</param>
    ''' <param name="control_lote">if set to <c>true</c> [control_lote].</param>
    ''' <param name="peso_recepcion">if set to <c>true</c> [peso_recepcion].</param>
    ''' <param name="peso_despacho">if set to <c>true</c> [peso_despacho].</param>
    ''' <param name="temperatura_recepcion">if set to <c>true</c> [temperatura_recepcion].</param>
    ''' <param name="temperatura_despacho">if set to <c>true</c> [temperatura_despacho].</param>
    ''' <param name="materia_prima">if set to <c>true</c> [materia_prima].</param>
    ''' <param name="kit">if set to <c>true</c> [kit].</param>
    ''' <param name="tolerancia">The tolerancia.</param>
    ''' <param name="ciclo_vida">The ciclo_vida.</param>
    ''' <param name="user_agr">The user_agr.</param>
    ''' <param name="fec_agr">The fec_agr.</param>
    ''' <param name="user_mod">The user_mod.</param>
    ''' <param name="fec_mod">The fec_mod.</param>
    ''' <param name="imagen">The imagen.</param>
    ''' <param name="noserie">The noserie.</param>
    ''' <param name="noparte">The noparte.</param>
    ''' <param name="fechamanufactura">if set to <c>true</c> [fechamanufactura].</param>
    ''' <param name="capturar_aniada">if set to <c>true</c> [capturar_aniada].</param>
    ''' <param name="control_peso">if set to <c>true</c> [control_peso].</param>
    ''' <param name="captura_arancel">if set to <c>true</c> [captura_arancel].</param>
    ''' <param name="es_hardware">if set to <c>true</c> [es_hardware].</param>
    ''' <param name="largo">The largo.</param>
    ''' <param name="alto">The alto.</param>
    ''' <param name="ancho">The ancho.</param>
    ''' <param name="mIdPresentacionOrigen">The m identifier presentacion origen.</param>
    ''' <param name="mIdPresentacionDestino">The m identifier presentacion destino.</param>
    ''' <param name="mFactor">The m factor.</param>
    Sub New(ByRef IdProducto As Integer, ByVal IdPropietario As Integer, ByVal IdClasificacion As Integer, ByVal IdFamilia As Integer, ByVal IdMarca As Integer,
            ByVal IdTipoProducto As Integer, ByVal IdUnidadMedidaBasica As Integer, ByVal IdCamara As Integer, ByVal IdTipoRotacion As Integer, ByVal IdPerfilSerializado As Integer,
            ByVal IdIndiceRotacion As Integer, ByVal IdSimbologia As Integer, ByVal IdArancel As Integer, ByVal codigo As String, ByVal nombre As String,
            ByVal codigo_barra As String, ByVal precio As Double, ByVal existencia_min As Double, ByVal existencia_max As Double, ByVal costo As Double,
            ByVal peso_referencia As Double, ByVal peso_tolerancia As Double, ByVal temperatura_referencia As Double, ByVal temperatura_tolerancia As Double,
            ByVal activo As Boolean, ByVal serializado As Boolean, ByVal genera_lote As Boolean, ByVal genera_lp As Boolean, ByVal control_vencimiento As Boolean,
            ByVal control_lote As Boolean, ByVal peso_recepcion As Boolean, ByVal peso_despacho As Boolean, ByVal temperatura_recepcion As Boolean,
            ByVal temperatura_despacho As Boolean, ByVal materia_prima As Boolean, ByVal kit As Boolean, ByVal tolerancia As Integer, ByVal ciclo_vida As Integer,
            ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal imagen As Byte(), ByVal noserie As String,
            ByVal noparte As String, ByVal fechamanufactura As Boolean, ByVal capturar_aniada As Boolean, ByVal control_peso As Boolean, ByVal captura_arancel As Boolean,
            ByVal es_hardware As Boolean, ByVal largo As Double, ByVal alto As Double, ByVal ancho As Double, ByVal mIdPresentacionOrigen As Integer,
            ByVal mIdPresentacionDestino As Integer, ByVal mFactor As Double)
        Me.IdProducto = IdProducto
        Me.IdPropietario = IdPropietario
        Me.IdClasificacion = IdClasificacion
        Me.IdFamilia = IdFamilia
        Me.IdMarca = IdMarca
        Me.IdTipoProducto = IdTipoProducto
        Me.IdUnidadMedidaBasica = IdUnidadMedidaBasica
        Me.IdCamara = IdCamara
        Me.IdTipoRotacion = IdTipoRotacion
        Me.IdPerfilSerializado = IdPerfilSerializado
        Me.IdIndiceRotacion = IdIndiceRotacion
        Me.IdSimbologia = IdSimbologia
        Me.IdArancel = IdArancel
        Me.Codigo = codigo
        Me.Nombre = nombre
        Me.Codigo_barra = codigo_barra
        Me.Precio = precio
        Me.Existencia_min = existencia_min
        Me.Existencia_max = existencia_max
        Me.Costo = costo
        Me.Peso_referencia = peso_referencia
        Me.Peso_tolerancia = peso_tolerancia
        Me.Temperatura_referencia = temperatura_referencia
        Me.Temperatura_tolerancia = temperatura_tolerancia
        Me.Activo = activo
        Me.Serializado = serializado
        Me.Genera_lote = genera_lote
        Me.Genera_lp = genera_lp
        Me.Control_vencimiento = control_vencimiento
        Me.Control_lote = control_lote
        Me.Peso_recepcion = peso_recepcion
        Me.Peso_despacho = peso_despacho
        Me.Temperatura_recepcion = temperatura_recepcion
        Me.Temperatura_despacho = temperatura_despacho
        Me.Materia_prima = materia_prima
        Me.Kit = kit
        Me.Tolerancia = tolerancia
        Me.Ciclo_vida = ciclo_vida
        Me.User_agr = user_agr
        Me.Fec_agr = fec_agr
        Me.User_mod = user_mod
        Me.Fec_mod = fec_mod
        Me.Imagen = imagen
        Me.Noserie = noserie
        Me.Noparte = noparte
        Me.Fechamanufactura = fechamanufactura
        Me.Capturar_aniada = capturar_aniada
        Me.Control_peso = control_peso
        Me.Captura_arancel = captura_arancel
        Me.Es_hardware = es_hardware
        Me.Largo = largo
        Me.Alto = alto
        Me.Ancho = ancho
        Me.IdPresentacionOrigen = mIdPresentacionOrigen
        Me.IdPresentacionDestino = mIdPresentacionDestino
        Me.Factor = mFactor
    End Sub

#Region "IDisposable Support"
    ''' <summary>
    ''' The disposed value
    ''' </summary>
    Private disposedValue As Boolean ' To detect redundant calls
    ' IDisposable
    ''' <summary>
    ''' Releases unmanaged and - optionally - managed resources.
    ''' </summary>
    ''' <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If
            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub
    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    ''' <summary>
    ''' Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    ''' </summary>
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
