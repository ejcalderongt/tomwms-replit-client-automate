<Serializable>
Public Class clsBeProducto_estado
    Implements ICloneable
    Implements IDisposable

    ''' <summary>
    ''' Gets or sets the identifier estado.
    ''' </summary>
    ''' <value>The identifier estado.</value>
    Public Property IdEstado() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier propietario.
    ''' </summary>
    ''' <value>The identifier propietario.</value>
    Public Property IdPropietario() As Integer = 0
    ''' <summary>
    ''' Gets or sets the propietario.
    ''' </summary>
    ''' <value>The propietario.</value>
    Public Property Propietario As clsBePropietarios
    ''' <summary>
    ''' Gets or sets the nombre.
    ''' </summary>
    ''' <value>The nombre.</value>
    Public Property Nombre() As String = ""
    ''' <summary>
    ''' Gets or sets the identifier ubicacion defecto.
    ''' </summary>
    ''' <value>The identifier ubicacion defecto.</value>
    Public Property IdUbicacionDefecto() As Integer = 0

    Public Property IdUbicacionBodegaDefecto() As Integer = 0
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeProducto_estado"/> is utilizable.
    ''' </summary>
    ''' <value><c>true</c> if utilizable; otherwise, <c>false</c>.</value>
    Public Property Utilizable() As Boolean = False
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeProducto_estado"/> is activo.
    ''' </summary>
    ''' <value><c>true</c> if activo; otherwise, <c>false</c>.</value>
    Public Property Activo() As Boolean = False
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
    ''' Gets or sets a value indicating whether this <see cref="clsBeProducto_estado"/> is dañado.
    ''' </summary>
    ''' <value><c>true</c> if dañado; otherwise, <c>false</c>.</value>
    Public Property Dañado() As Boolean = False

    Public Property Sistema() As Boolean = False

    ''' <summary>
    ''' #EJC202311150009: Codigo_Bodega_ERP Entity
    ''' </summary>
    ''' <returns></returns>
    Public Property Codigo_Bodega_ERP As String = ""

    ''' <summary>
    ''' #EJC20231118: Dias_Vencimiento_Clasificacion para definir estado del producto en base a la fecha
    ''' </summary>
    ''' <returns></returns>
    Public Property Dias_Vencimiento_Clasificacion As Integer = 0

    ''' <summary>
    ''' #EJC20231118: Tolerancia_DiasVencimiento para definir estado del producto en base a la fecha
    ''' </summary>
    ''' <returns></returns>
    Public Property Tolerancia_Dias_Vencimiento As Integer = 0

    ''' <summary>
    ''' #CKFK20250910: Campo para saber si el estado del producto en la reserva requiere UMBas,
    ''' esto aplica si el cliente tiene definido un estado de producto a importar
    ''' </summary>
    ''' <returns></returns>
    Public Property Reservar_En_UmBas As Boolean = False

    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeProducto_estado"/> class.
    ''' </summary>
    Sub New()
    End Sub

    ''' <summary>
    ''' Creates a new object that is a copy of the current instance.
    ''' </summary>
    ''' <returns>A new object that is a copy of this instance.</returns>
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
