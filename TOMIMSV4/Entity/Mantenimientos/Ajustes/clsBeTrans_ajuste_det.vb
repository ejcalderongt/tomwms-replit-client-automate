' ***********************************************************************
' Assembly         : Entity
' Author           : ejcalderon
' Created          : 01-24-2018
'
' Last Modified By : ejcalderon
' Last Modified On : 01-26-2018
' ***********************************************************************
' <copyright file="clsBeTrans_ajuste_det.vb" company="TEAM OS">
'     Copyright ©  2016
' </copyright>
' <summary></summary>
' ***********************************************************************
''' <summary>
''' Class clsBeTrans_ajuste_det.
''' </summary>
''' <seealso cref="System.ICloneable" />
Public Class clsBeTrans_ajuste_det
    Implements ICloneable
    ''' <summary>
    ''' Gets or sets the idajustedet.
    ''' </summary>
    ''' <value>The idajustedet.</value>
    Public Property IdAjusteDet() As Integer = 0
    ''' <summary>
    ''' Gets or sets the idajusteenc.
    ''' </summary>
    ''' <value>The idajusteenc.</value>
    Public Property IdAjusteEnc() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier stock.
    ''' </summary>
    ''' <value>The identifier stock.</value>
    Public Property IdStock() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier propietario bodega.
    ''' </summary>
    ''' <value>The identifier propietario bodega.</value>
    Public Property IdPropietarioBodega() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier producto bodega.
    ''' </summary>
    ''' <value>The identifier producto bodega.</value>
    Public Property IdProductoBodega() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier producto estado.
    ''' </summary>
    ''' <value>The identifier producto estado.</value>
    Public Property IdProductoEstado() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier presentacion.
    ''' </summary>
    ''' <value>The identifier presentacion.</value>
    Public Property IdPresentacion() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier unidad medida.
    ''' </summary>
    ''' <value>The identifier unidad medida.</value>
    Public Property IdUnidadMedida() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier ubicacion.
    ''' </summary>
    ''' <value>The identifier ubicacion.</value>
    Public Property IdUbicacion() As Integer = 0
    ''' <summary>
    ''' Gets or sets the lote_original.
    ''' </summary>
    ''' <value>The lote_original.</value>
    Public Property Lote_original() As String = ""
    ''' <summary>
    ''' Gets or sets the lote_nuevo.
    ''' </summary>
    ''' <value>The lote_nuevo.</value>
    Public Property Lote_nuevo() As String = ""
    ''' <summary>
    ''' Gets or sets the fecha_vence_original.
    ''' </summary>
    ''' <value>The fecha_vence_original.</value>
    Public Property Fecha_vence_original() As Date = Date.Now
    ''' <summary>
    ''' Gets or sets the fecha_vence_nueva.
    ''' </summary>
    ''' <value>The fecha_vence_nueva.</value>
    Public Property Fecha_vence_nueva() As Date = Date.Now
    ''' <summary>
    ''' Gets or sets the peso_original.
    ''' </summary>
    ''' <value>The peso_original.</value>
    Public Property Peso_original() As Double = 0.0
    ''' <summary>
    ''' Gets or sets the peso_nuevo.
    ''' </summary>
    ''' <value>The peso_nuevo.</value>
    Public Property Peso_nuevo() As Double = 0.0
    ''' <summary>
    ''' Gets or sets the cantidad_original.
    ''' </summary>
    ''' <value>The cantidad_original.</value>
    Public Property Cantidad_original() As Double = 0.0
    ''' <summary>
    ''' Gets or sets the cantidad_nueva.
    ''' </summary>
    ''' <value>The cantidad_nueva.</value>
    Public Property Cantidad_nueva() As Double = 0.0
    ''' <summary>
    ''' Gets or sets the codigo_producto.
    ''' </summary>
    ''' <value>The codigo_producto.</value>
    Public Property Codigo_producto() As String = ""
    ''' <summary>
    ''' Gets or sets the nombre_producto.
    ''' </summary>
    ''' <value>The nombre_producto.</value>
    Public Property Nombre_producto() As String = ""
    ''' <summary>
    ''' Gets or sets the idtipoajuste.
    ''' </summary>
    ''' <value>The idtipoajuste.</value>
    Public Property Idtipoajuste() As Integer = 0
    ''' <summary>
    ''' Gets or sets the idmotivoajuste.
    ''' </summary>
    ''' <value>The idmotivoajuste.</value>
    Public Property IdMotivoAjuste() As Integer = 0
    ''' <summary>
    ''' Gets or sets the observacion.
    ''' </summary>
    ''' <value>The observacion.</value>
    Public Property Observacion() As String = ""
    ''' <summary>
    ''' Gets or sets the codigo_ajuste.
    ''' </summary>
    ''' <value>The codigo_ajuste.</value>
    Public Property Codigo_ajuste() As String = ""
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeTrans_ajuste_det"/> is enviado.
    ''' </summary>
    ''' <value><c>true</c> if enviado; otherwise, <c>false</c>.</value>
    Public Property Enviado() As Boolean = False
    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeTrans_ajuste_det"/> class.
    ''' </summary>

    Public Presentacion As New clsBeProducto_Presentacion

    Public Property IdBodegaERP As Integer = 0

    '#CKFK20220208 Agregamos el campo LicPlate
    Public Property lic_plate As String = ""

    '#CKFK20220208 Agregamos estos campos para control del envío de los ajustes a NAV
    Public Property referencia_ajuste_erp As String = ""
    Public Property estado_ajuste_erp As Boolean = False

    '#GT28082025: control de producto por talla/color
    Public Property IdProductoTallaColor_origen As Integer = 0
    Public Property Talla_origen As String = ""
    Public Property Color_origen As String = ""

    '#GT17122025: se agregan propiedades para poder dar traza si una talla cambio a otra, igual con color
    Public Property Talla_destino As String = ""
    Public Property Color_destino As String = ""
    Public Property IdProductoTallaColor_destino As Integer = 0

    Sub New()
    End Sub
    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeTrans_ajuste_det"/> class.
    ''' </summary>
    ''' <param name="idajustedet">The idajustedet.</param>
    ''' <param name="idajusteenc">The idajusteenc.</param>
    ''' <param name="IdStock">The identifier stock.</param>
    ''' <param name="IdPropietarioBodega">The identifier propietario bodega.</param>
    ''' <param name="IdProductoBodega">The identifier producto bodega.</param>
    ''' <param name="IdProductoEstado">The identifier producto estado.</param>
    ''' <param name="IdPresentacion">The identifier presentacion.</param>
    ''' <param name="IdUnidadMedida">The identifier unidad medida.</param>
    ''' <param name="IdUbicacion">The identifier ubicacion.</param>
    ''' <param name="lote_original">The lote_original.</param>
    ''' <param name="lote_nuevo">The lote_nuevo.</param>
    ''' <param name="fecha_vence_original">The fecha_vence_original.</param>
    ''' <param name="fecha_vence_nueva">The fecha_vence_nueva.</param>
    ''' <param name="peso_original">The peso_original.</param>
    ''' <param name="peso_nuevo">The peso_nuevo.</param>
    ''' <param name="cantidad_original">The cantidad_original.</param>
    ''' <param name="cantidad_nueva">The cantidad_nueva.</param>
    ''' <param name="codigo_producto">The codigo_producto.</param>
    ''' <param name="nombre_producto">The nombre_producto.</param>
    ''' <param name="idtipoajuste">The idtipoajuste.</param>
    ''' <param name="idmotivoajuste">The idmotivoajuste.</param>
    ''' <param name="observacion">The observacion.</param>
    ''' <param name="codigo_ajuste">The codigo_ajuste.</param>
    ''' <param name="enviado">if set to <c>true</c> [enviado].</param>
    Sub New(ByRef idajustedet As Integer, ByVal idajusteenc As Integer, ByVal IdStock As Integer, ByVal IdPropietarioBodega As Integer, ByVal IdProductoBodega As Integer, ByVal IdProductoEstado As Integer, ByVal IdPresentacion As Integer, ByVal IdUnidadMedida As Integer, ByVal IdUbicacion As Integer, ByVal lote_original As String, ByVal lote_nuevo As String, ByVal fecha_vence_original As Date, ByVal fecha_vence_nueva As Date, ByVal peso_original As Double, ByVal peso_nuevo As Double, ByVal cantidad_original As Double, ByVal cantidad_nueva As Double, ByVal codigo_producto As String, ByVal nombre_producto As String, ByVal idtipoajuste As Integer, ByVal idmotivoajuste As Integer, ByVal observacion As String, ByVal codigo_ajuste As String, ByVal enviado As Boolean)
        Me.IdAjusteDet = Idajustedet
        Me.IdAjusteEnc = Idajusteenc
        Me.IdStock = IdStock
        Me.IdPropietarioBodega = IdPropietarioBodega
        Me.IdProductoBodega = IdProductoBodega
        Me.IdProductoEstado = IdProductoEstado
        Me.IdPresentacion = IdPresentacion
        Me.IdUnidadMedida = IdUnidadMedida
        Me.IdUbicacion = IdUbicacion
        Me.Lote_original = Lote_original
        Me.Lote_nuevo = Lote_nuevo
        Me.Fecha_vence_original = Fecha_vence_original
        Me.Fecha_vence_nueva = Fecha_vence_nueva
        Me.Peso_original = Peso_original
        Me.Peso_nuevo = Peso_nuevo
        Me.Cantidad_original = Cantidad_original
        Me.Cantidad_nueva = Cantidad_nueva
        Me.Codigo_producto = Codigo_producto
        Me.Nombre_producto = Nombre_producto
        Me.Idtipoajuste = Idtipoajuste
        Me.IdMotivoAjuste = Idmotivoajuste
        Me.Observacion = Observacion
        Me.Codigo_ajuste = Codigo_ajuste
        Me.Enviado = Enviado
    End Sub
    ''' <summary>
    ''' Creates a new object that is a copy of the current instance.
    ''' </summary>
    ''' <returns>A new object that is a copy of this instance.</returns>
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function
End Class
