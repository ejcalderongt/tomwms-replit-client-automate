<Serializable>
Public Class clsBeProducto_clasificacion
    Implements ICloneable

    ''' <summary>
    ''' Gets or sets the identifier clasificacion.
    ''' </summary>
    ''' <value>The identifier clasificacion.</value>
    Public Property IdClasificacion() As Integer = 0
    ''' <summary>
    ''' Gets or sets the propietario.
    ''' </summary>
    ''' <value>The propietario.</value>
    Public Property Propietario As clsBePropietarios = New clsBePropietarios()
    ''' <summary>
    ''' Gets or sets the nombre.
    ''' </summary>
    ''' <value>The nombre.</value>
    Public Property Nombre() As String = ""
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeProducto_clasificacion"/> is activo.
    ''' </summary>
    ''' <value><c>true</c> if activo; otherwise, <c>false</c>.</value>
    Public Property Activo() As Boolean = False
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeProducto_clasificacion"/> is sistema.
    ''' </summary>
    ''' <value><c>true</c> if sistema; otherwise, <c>false</c>.</value>
    Public Property Sistema() As Boolean = False
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
    ''' Gets or sets a value indicating whether this instance is new.
    ''' </summary>
    ''' <value><c>true</c> if this instance is new; otherwise, <c>false</c>.</value>
    Public Property IsNew() As Boolean = False

    Public Property Codigo As String = ""

    ''' <summary>
    ''' Creates a new object that is a copy of the current instance.
    ''' </summary>
    ''' <returns>A new object that is a copy of this instance.</returns>
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function
End Class
