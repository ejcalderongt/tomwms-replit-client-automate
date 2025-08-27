<Serializable>
Public Class clsBeProducto_presentacion_tarima
    Implements ICloneable
    Implements IDisposable

    ''' <summary>
    ''' Gets or sets the identifier presentacion tarima.
    ''' </summary>
    ''' <value>The identifier presentacion tarima.</value>
    Public Property IdPresentacionTarima() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier presentacion.
    ''' </summary>
    ''' <value>The identifier presentacion.</value>
    Public Property IdPresentacion() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier tipo tarima.
    ''' </summary>
    ''' <value>The identifier tipo tarima.</value>
    Public Property IdTipoTarima() As Integer = 0
    ''' <summary>
    ''' Gets or sets the cantidad por cama.
    ''' </summary>
    ''' <value>The cantidad por cama.</value>
    Public Property CantidadPorCama As Double = 0
    ''' <summary>
    ''' Gets or sets the cantidad.
    ''' </summary>
    ''' <value>The cantidad.</value>
    Public Property Cantidad() As Double = 0.0
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
    ''' Gets or sets a value indicating whether this <see cref="clsBeProducto_presentacion_tarima"/> is activo.
    ''' </summary>
    ''' <value><c>true</c> if activo; otherwise, <c>false</c>.</value>
    Public Property Activo() As Boolean = False

    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeProducto_presentacion_tarima"/> class.
    ''' </summary>
    Sub New()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeProducto_presentacion_tarima"/> class.
    ''' </summary>
    ''' <param name="IdPresentacionTarima">The identifier presentacion tarima.</param>
    ''' <param name="IdPresentacion">The identifier presentacion.</param>
    ''' <param name="IdTipoTarima">The identifier tipo tarima.</param>
    ''' <param name="Cantidad">The cantidad.</param>
    ''' <param name="user_agr">The user_agr.</param>
    ''' <param name="fec_agr">The fec_agr.</param>
    ''' <param name="user_mod">The user_mod.</param>
    ''' <param name="fec_mod">The fec_mod.</param>
    ''' <param name="activo">if set to <c>true</c> [activo].</param>
    Sub New(ByRef IdPresentacionTarima As Integer, ByVal IdPresentacion As Integer, ByVal IdTipoTarima As Integer, ByVal Cantidad As Double, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal activo As Boolean)
        Me.IdPresentacionTarima = IdPresentacionTarima
        Me.IdPresentacion = IdPresentacion
        Me.IdTipoTarima = IdTipoTarima
        Me.Cantidad = Cantidad
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
