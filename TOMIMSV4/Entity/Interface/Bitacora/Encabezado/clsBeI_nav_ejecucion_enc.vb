' ***********************************************************************
' Assembly         : Entity
' Author           : ejcalderon
' Created          : 09-25-2017
'
' Last Modified By : ejcalderon
' Last Modified On : 09-25-2017
' ***********************************************************************
' <copyright file="clsBeI_nav_ejecucion_enc.vb" company="TEAM OS">
'     Copyright ©  2016
' </copyright>
' <summary></summary>
' ***********************************************************************
''' <summary>
''' Class clsBeI_nav_ejecucion_enc.
''' </summary>
''' <seealso cref="System.ICloneable" />
Public Class clsBeI_nav_ejecucion_enc
    Implements ICloneable

    ''' <summary>
    ''' The m idejecucionenc
    ''' </summary>
    Private mIdejecucionenc As Integer = 0
    ''' <summary>
    ''' The m idnavconfigenc
    ''' </summary>
    Private mIdnavconfigenc As Integer = 0
    ''' <summary>
    ''' The m fecha
    ''' </summary>
    Private mFecha As Date = Date.Now
    ''' <summary>
    ''' The m exitosa
    ''' </summary>
    Private mExitosa As Boolean = False

    ''' <summary>
    ''' Gets or sets the idejecucionenc.
    ''' </summary>
    ''' <value>The idejecucionenc.</value>
    Public Property IdEjecucionEnc() As Integer
        Get
            Return mIdejecucionenc
        End Get
        Set(ByVal Value As Integer)
            mIdejecucionenc = Value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the idnavconfigenc.
    ''' </summary>
    ''' <value>The idnavconfigenc.</value>
    Public Property IdNavConfigEnc() As Integer
        Get
            Return mIdnavconfigenc
        End Get
        Set(ByVal Value As Integer)
            mIdnavconfigenc = Value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the fecha.
    ''' </summary>
    ''' <value>The fecha.</value>
    Public Property Fecha() As Date
        Get
            Return mFecha
        End Get
        Set(ByVal Value As Date)
            mFecha = Value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeI_nav_ejecucion_enc"/> is exitosa.
    ''' </summary>
    ''' <value><c>true</c> if exitosa; otherwise, <c>false</c>.</value>
    Public Property Exitosa() As Boolean
        Get
            Return mExitosa
        End Get
        Set(ByVal Value As Boolean)
            mExitosa = Value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the identifier bodega.
    ''' </summary>
    ''' <value>The identifier bodega.</value>
    Public Property IdBodega() As Integer = 0

    ''' <summary>
    ''' Gets or sets the identifier tipo de documento.
    ''' </summary>
    ''' <value>The identifier tipo de documento.</value>
    Property IdTipoDocumento As Integer = 0

    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeI_nav_ejecucion_enc"/> class.
    ''' </summary>
    Sub New()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeI_nav_ejecucion_enc"/> class.
    ''' </summary>
    ''' <param name="idejecucionenc">The idejecucionenc.</param>
    ''' <param name="idnavconfigenc">The idnavconfigenc.</param>
    ''' <param name="fecha">The fecha.</param>
    ''' <param name="exitosa">if set to <c>true</c> [exitosa].</param>
    Sub New(ByRef idejecucionenc As Integer, ByVal idnavconfigenc As Integer, ByVal fecha As Date, ByVal exitosa As Boolean)
        mIdejecucionenc = Idejecucionenc
        mIdnavconfigenc = Idnavconfigenc
        mFecha = Fecha
        mExitosa = Exitosa
    End Sub

    ''' <summary>
    ''' Creates a new object that is a copy of the current instance.
    ''' </summary>
    ''' <returns>A new object that is a copy of this instance.</returns>
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
