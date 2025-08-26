<Serializable>
Public Class clsBeProducto_parametros
    Implements ICloneable
    Implements IDisposable

    ''' <summary>
    ''' Gets or sets the identifier producto parametro.
    ''' </summary>
    ''' <value>The identifier producto parametro.</value>
    Public Property IdProductoParametro() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier parametro.
    ''' </summary>
    ''' <value>The identifier parametro.</value>
    Public Property IdParametro() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier producto.
    ''' </summary>
    ''' <value>The identifier producto.</value>
    Public Property IdProducto() As Integer = 0
    ''' <summary>
    ''' Gets or sets the valor_texto.
    ''' </summary>
    ''' <value>The valor_texto.</value>
    Public Property Valor_texto() As String = ""
    ''' <summary>
    ''' Gets or sets the valor_numerico.
    ''' </summary>
    ''' <value>The valor_numerico.</value>
    Public Property Valor_numerico() As Double = 0
    ''' <summary>
    ''' Gets or sets the valor_fecha.
    ''' </summary>
    ''' <value>The valor_fecha.</value>
    Public Property Valor_fecha() As Date = New Date(1900, 1, 1)
    ''' <summary>
    ''' Gets or sets the valor_logico.
    ''' </summary>
    ''' <value>The valor_logico.</value>
    Public Property Valor_logico() As Boolean = False
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeProducto_parametros"/> is capturar_siempre.
    ''' </summary>
    ''' <value><c>true</c> if capturar_siempre; otherwise, <c>false</c>.</value>
    Public Property Capturar_siempre() As Boolean = False
    ''' <summary>
    ''' Gets or sets the user_agr.
    ''' </summary>
    ''' <value>The user_agr.</value>
    Public Property User_agr() As String = ""
    ''' <summary>
    ''' Gets or sets the fec_agr.
    ''' </summary>
    ''' <value>The fec_agr.</value>
    Public Property Fec_agr() As Date = New Date(1900, 1, 1)
    ''' <summary>
    ''' Gets or sets the user_mod.
    ''' </summary>
    ''' <value>The user_mod.</value>
    Public Property User_mod() As String = ""
    ''' <summary>
    ''' Gets or sets the fec_mod.
    ''' </summary>
    ''' <value>The fec_mod.</value>
    Public Property Fec_mod() As Date = New Date(1900, 1, 1)
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeProducto_parametros"/> is activo.
    ''' </summary>
    ''' <value><c>true</c> if activo; otherwise, <c>false</c>.</value>
    Public Property Activo() As Boolean = False

    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeProducto_parametros"/> class.
    ''' </summary>
    Sub New()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeProducto_parametros"/> class.
    ''' </summary>
    ''' <param name="IdProductoParametro">The identifier producto parametro.</param>
    ''' <param name="IdParametro">The identifier parametro.</param>
    ''' <param name="IdProducto">The identifier producto.</param>
    ''' <param name="valor_texto">The valor_texto.</param>
    ''' <param name="valor_numerico">The valor_numerico.</param>
    ''' <param name="valor_fecha">The valor_fecha.</param>
    ''' <param name="valor_logico">if set to <c>true</c> [valor_logico].</param>
    ''' <param name="capturar_siempre">if set to <c>true</c> [capturar_siempre].</param>
    ''' <param name="user_agr">The user_agr.</param>
    ''' <param name="fec_agr">The fec_agr.</param>
    ''' <param name="user_mod">The user_mod.</param>
    ''' <param name="fec_mod">The fec_mod.</param>
    ''' <param name="activo">if set to <c>true</c> [activo].</param>
    Sub New(ByRef IdProductoParametro As Integer, ByVal IdParametro As Integer, ByVal IdProducto As Integer, ByVal valor_texto As String, ByVal valor_numerico As Double, ByVal valor_fecha As Date, ByVal valor_logico As Boolean, ByVal capturar_siempre As Boolean, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal activo As Boolean)
        Me.IdProductoParametro = IdProductoParametro
        Me.IdParametro = IdParametro
        Me.IdProducto = IdProducto
        Me.Valor_texto = Valor_texto
        Me.Valor_numerico = Valor_numerico
        Me.Valor_fecha = Valor_fecha
        Me.Valor_logico = Valor_logico
        Me.Capturar_siempre = Capturar_siempre
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
