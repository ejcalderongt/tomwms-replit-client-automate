<Serializable>
Public Class clsBeProducto_estado_ubic
    Implements ICloneable

    ''' <summary>
    ''' Gets or sets the identifier producto estad ubic.
    ''' </summary>
    ''' <value>The identifier producto estad ubic.</value>
    Public Property IdProductoEstadUbic() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier estado.
    ''' </summary>
    ''' <value>The identifier estado.</value>
    Public Property IdEstado() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier ubicacion defecto.
    ''' </summary>
    ''' <value>The identifier ubicacion defecto.</value>
    Public Property IdUbicacionDefecto() As Integer = 0
    ''' <summary>
    ''' Gets or sets the fec_agr.
    ''' </summary>
    ''' <value>The fec_agr.</value>
    Public Property Fec_agr() As Date = Date.Now
    ''' <summary>
    ''' Gets or sets the user_agr.
    ''' </summary>
    ''' <value>The user_agr.</value>
    Public Property User_agr() As String = ""
    ''' <summary>
    ''' Gets or sets the fec_mod.
    ''' </summary>
    ''' <value>The fec_mod.</value>
    Public Property Fec_mod() As Date = Date.Now
    ''' <summary>
    ''' Gets or sets the user_mod.
    ''' </summary>
    ''' <value>The user_mod.</value>
    Public Property User_mod() As String = ""
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeProducto_estado_ubic"/> is activo.
    ''' </summary>
    ''' <value><c>true</c> if activo; otherwise, <c>false</c>.</value>
    Public Property Activo() As Boolean = False

    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeProducto_estado_ubic"/> class.
    ''' </summary>
    Sub New()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeProducto_estado_ubic"/> class.
    ''' </summary>
    ''' <param name="idProductoEstadUbic">The identifier producto estad ubic.</param>
    ''' <param name="IdEstado">The identifier estado.</param>
    ''' <param name="IdUbicacionDefecto">The identifier ubicacion defecto.</param>
    ''' <param name="fec_agr">The fec_agr.</param>
    ''' <param name="user_agr">The user_agr.</param>
    ''' <param name="fec_mod">The fec_mod.</param>
    ''' <param name="user_mod">The user_mod.</param>
    ''' <param name="activo">if set to <c>true</c> [activo].</param>    
    Sub New(ByRef idProductoEstadUbic As Integer, ByVal IdEstado As Integer, ByVal IdUbicacionDefecto As Integer, ByVal fec_agr As Date, ByVal user_agr As String, ByVal fec_mod As Date, ByVal user_mod As String, ByVal activo As Boolean)
        Me.IdProductoEstadUbic = idProductoEstadUbic
        Me.IdEstado = IdEstado
        Me.IdUbicacionDefecto = IdUbicacionDefecto
        Me.Fec_agr = fec_agr
        Me.User_agr = user_agr
        Me.Fec_mod = fec_mod
        Me.User_mod = user_mod
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
