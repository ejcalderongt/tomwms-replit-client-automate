<Serializable>
Public Class clsBeProducto_codigos_barra
    Implements ICloneable
    Implements IDisposable

    ''' <summary>
    ''' Gets or sets the identifier producto codigo barra.
    ''' </summary>
    ''' <value>The identifier producto codigo barra.</value>
    Public Property IdProductoCodigoBarra() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier producto.
    ''' </summary>
    ''' <value>The identifier producto.</value>
    Public Property IdProducto() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier proveedor.
    ''' </summary>
    ''' <value>The identifier proveedor.</value>
    Public Property IdProveedor() As Integer = 0
    ''' <summary>
    ''' Gets or sets the codigo_barra.
    ''' </summary>
    ''' <value>The codigo_barra.</value>
    Public Property Codigo_barra() As String = ""
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
    ''' Gets or sets the user_agr.
    ''' </summary>
    ''' <value>The user_agr.</value>
    Public Property User_agr() As String = ""
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeProducto_codigos_barra"/> is activo.
    ''' </summary>
    ''' <value><c>true</c> if activo; otherwise, <c>false</c>.</value>
    Public Property Activo() As Boolean = False

    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeProducto_codigos_barra"/> class.
    ''' </summary>
    Sub New()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeProducto_codigos_barra"/> class.
    ''' </summary>
    ''' <param name="IdProductoCodigoBarra">The identifier producto codigo barra.</param>
    ''' <param name="IdProducto">The identifier producto.</param>
    ''' <param name="IdProveedor">The identifier proveedor.</param>
    ''' <param name="codigo_barra">The codigo_barra.</param>
    ''' <param name="fec_agr">The fec_agr.</param>
    ''' <param name="user_mod">The user_mod.</param>
    ''' <param name="fec_mod">The fec_mod.</param>
    ''' <param name="user_agr">The user_agr.</param>
    ''' <param name="activo">if set to <c>true</c> [activo].</param>
    Sub New(ByRef IdProductoCodigoBarra As Integer, ByVal IdProducto As Integer, ByVal IdProveedor As Integer, ByVal codigo_barra As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal user_agr As String, ByVal activo As Boolean)
        Me.IdProductoCodigoBarra = IdProductoCodigoBarra
        Me.IdProducto = IdProducto
        Me.IdProveedor = IdProveedor
        Me.Codigo_barra = codigo_barra
        Me.Fec_agr = fec_agr
        Me.User_mod = user_mod
        Me.Fec_mod = fec_mod
        Me.User_agr = user_agr
        Me.Activo = activo
    End Sub
    ''' <summary>
    ''' Creates a new object that is a copy of the current instance.
    ''' </summary>
    ''' <returns>A new object that is a copy of this instance.</returns>
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function
End Class
