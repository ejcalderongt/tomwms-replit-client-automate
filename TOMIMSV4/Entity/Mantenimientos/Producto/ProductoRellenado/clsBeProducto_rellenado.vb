<Serializable>
Public Class clsBeProducto_rellenado
    Implements ICloneable
    Implements IDisposable

    ''' <summary>
    ''' Gets or sets the identifier rellenado.
    ''' </summary>
    ''' <value>The identifier rellenado.</value>
    Public Property IdRellenado() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier presentacion.
    ''' </summary>
    ''' <value>The identifier presentacion.</value>
    Public Property IdPresentacion() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier producto estado.
    ''' </summary>
    ''' <value>The identifier producto estado.</value>
    Public Property IdProductoEstado() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier ubicacion.
    ''' </summary>
    ''' <value>The identifier ubicacion.</value>
    Public Property IdUbicacion() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier tipo accion.
    ''' </summary>
    ''' <value>The identifier tipo accion.</value>
    Public Property IdTipoAccion() As Integer = 0
    ''' <summary>
    ''' Gets or sets the minimo.
    ''' </summary>
    ''' <value>The minimo.</value>
    Public Property Minimo() As Double = 0.0
    ''' <summary>
    ''' Gets or sets the maximo.
    ''' </summary>
    ''' <value>The maximo.</value>
    Public Property Maximo() As Double = 0.0
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
    ''' Gets or sets a value indicating whether this <see cref="clsBeProducto_rellenado"/> is activo.
    ''' </summary>
    ''' <value><c>true</c> if activo; otherwise, <c>false</c>.</value>
    Public Property Activo() As Boolean = False
    Public Property IdProductoBodega As Integer = 0
    Public Property IdBodega As Integer = 0
    Public Property IdUnidadMedidaBasica As Integer = 0

    <Obsolete("This property is deprecated, the reabasto solo se hará a partir de una presentación, with love Erik C.")>
    Public Property IdUmBasAbastercerCon As Integer = 0
    <Obsolete("This property is deprecated, the reabasto solo se hará a partir de una presentación, with love Erik C.")>
    Public Property NomUmBasAbastecerCon As String = ""

    Public Property IdPresentacionAbastercerCon As Integer = 0
    Public Property NomPresentacionRellenarCon As String = ""

    Public Property IdPropietario As Integer = 0

    Public Property IdOperadorDefecto As Integer = 0
    Public Property NomOperador As String = ""

    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeProducto_rellenado"/> class.
    ''' </summary>
    Sub New()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeProducto_rellenado"/> class.
    ''' </summary>
    ''' <param name="IdRellenado">The identifier rellenado.</param>
    ''' <param name="IdPresentacion">The identifier presentacion.</param>
    ''' <param name="IdProductoEstado">The identifier producto estado.</param>
    ''' <param name="IdUbicacion">The identifier ubicacion.</param>
    ''' <param name="IdTipoAccion">The identifier tipo accion.</param>
    ''' <param name="Minimo">The minimo.</param>
    ''' <param name="Maximo">The maximo.</param>
    ''' <param name="user_agr">The user_agr.</param>
    ''' <param name="fec_agr">The fec_agr.</param>
    ''' <param name="user_mod">The user_mod.</param>
    ''' <param name="fec_mod">The fec_mod.</param>
    ''' <param name="Activo">if set to <c>true</c> [activo].</param>
    Sub New(ByRef IdRellenado As Integer, ByVal IdPresentacion As Integer, ByVal IdProductoEstado As Integer, ByVal IdUbicacion As Integer, ByVal IdTipoAccion As Integer, ByVal Minimo As Double, ByVal Maximo As Double, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal Activo As Boolean)
        Me.IdRellenado = IdRellenado
        Me.IdPresentacion = IdPresentacion
        Me.IdProductoEstado = IdProductoEstado
        Me.IdUbicacion = IdUbicacion
        Me.IdTipoAccion = IdTipoAccion
        Me.Minimo = Minimo
        Me.Maximo = Maximo
        Me.User_agr = User_agr
        Me.Fec_agr = Fec_agr
        Me.User_mod = User_mod
        Me.Fec_mod = Fec_mod
        Me.Activo = Activo
    End Sub

    ''' <summary>
    ''' Creates a new object that is a copy of the current instance.
    ''' </summary>
    ''' <returns>A new object that is a copy of this instance.</returns>
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
