' ***********************************************************************
' Assembly         : Entity
' Author           : ejcalderon
' Created          : 08-14-2017
'
' Last Modified By : ejcalderon
' Last Modified On : 08-04-2017
' ***********************************************************************
' <copyright file="clsBeProducto_presentaciones_conversiones.vb" company="TEAM OS">
'     Copyright ©  2016
' </copyright>
' <summary></summary>
' ***********************************************************************
''' <summary>
''' Class clsBeProducto_presentaciones_conversiones.
''' </summary>
''' <seealso cref="System.IDisposable" />
''' <seealso cref="System.ICloneable" />
Public Class clsBeProducto_presentaciones_conversiones
    Implements ICloneable
    Implements IDisposable

    ''' <summary>
    ''' Gets or sets the identifier conversion.
    ''' </summary>
    ''' <value>The identifier conversion.</value>
    Public Property IdConversion() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier presentacion origen.
    ''' </summary>
    ''' <value>The identifier presentacion origen.</value>
    Public Property IdPresentacionOrigen() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier presentacion destino.
    ''' </summary>
    ''' <value>The identifier presentacion destino.</value>
    Public Property IdPresentacionDestino() As Integer = 0
    ''' <summary>
    ''' Gets or sets the factor.
    ''' </summary>
    ''' <value>The factor.</value>
    Public Property Factor() As Double = 0.0
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeProducto_presentaciones_conversiones"/> is activo.
    ''' </summary>
    ''' <value><c>true</c> if activo; otherwise, <c>false</c>.</value>
    Public Property Activo() As Boolean = False
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
    ''' Gets or sets a value indicating whether this <see cref="clsBeProducto_presentaciones_conversiones"/> is inverso.
    ''' </summary>
    ''' <value><c>true</c> if inverso; otherwise, <c>false</c>.</value>
    Public Property Inverso() As Boolean = False

    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeProducto_presentaciones_conversiones"/> class.
    ''' </summary>
    Sub New()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeProducto_presentaciones_conversiones"/> class.
    ''' </summary>
    ''' <param name="IdConversion">The identifier conversion.</param>
    ''' <param name="IdPresentacionOrigen">The identifier presentacion origen.</param>
    ''' <param name="IdPresentacionDestino">The identifier presentacion destino.</param>
    ''' <param name="Factor">The factor.</param>
    ''' <param name="activo">if set to <c>true</c> [activo].</param>
    ''' <param name="fec_agr">The fec_agr.</param>
    ''' <param name="user_mod">The user_mod.</param>
    ''' <param name="fec_mod">The fec_mod.</param>
    ''' <param name="user_agr">The user_agr.</param>
    ''' <param name="inverso">if set to <c>true</c> [inverso].</param>
    Sub New(ByRef IdConversion As Integer, ByVal IdPresentacionOrigen As Integer, ByVal IdPresentacionDestino As Integer, ByVal Factor As Double, ByVal activo As Boolean, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal user_agr As String, ByVal inverso As Boolean)
        Me.IdConversion = IdConversion
        Me.IdPresentacionOrigen = IdPresentacionOrigen
        Me.IdPresentacionDestino = IdPresentacionDestino
        Me.Factor = Factor
        Me.Activo = activo
        Me.Fec_agr = fec_agr
        Me.User_mod = user_mod
        Me.Fec_mod = fec_mod
        Me.User_agr = user_agr
        Me.Inverso = inverso
    End Sub

    ''' <summary>
    ''' Creates a new object that is a copy of the current instance.
    ''' </summary>
    ''' <returns>A new object that is a copy of this instance.</returns>
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
