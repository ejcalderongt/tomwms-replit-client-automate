' ***********************************************************************
' Assembly         : Entity
' Author           : ejcalderon
' Created          : 11-08-2017
'
' Last Modified By : ejcalderon
' Last Modified On : 11-08-2017
' ***********************************************************************
' <copyright file="clsBeI_nav_config_ent.vb" company="TEAM OS">
'     Copyright ©  2016
' </copyright>
' <summary></summary>
' ***********************************************************************
''' <summary>
''' Class clsBeI_nav_config_ent.
''' </summary>
''' <seealso cref="System.ICloneable" />
Public Class clsBeI_nav_config_ent
    Implements ICloneable

    ''' <summary>
    ''' The m idnavconfigent
    ''' </summary>
    Private mIdnavconfigent As Integer = 0
    ''' <summary>
    ''' The m idnavent
    ''' </summary>
    Private mIdnavent As Integer = 0
    ''' <summary>
    ''' The m endpoint
    ''' </summary>
    Private mEndpoint As String = ""
    ''' <summary>
    ''' The m activo
    ''' </summary>
    Private mActivo As Boolean = False
    ''' <summary>
    ''' The m fec_agr
    ''' </summary>
    Private mFec_agr As Date = Date.Now
    ''' <summary>
    ''' The m user_agr
    ''' </summary>
    Private mUser_agr As String = ""
    ''' <summary>
    ''' The m fec_mod
    ''' </summary>
    Private mFec_mod As Date = Date.Now
    ''' <summary>
    ''' The m user_mod
    ''' </summary>
    Private mUser_mod As Integer = 0

    ''' <summary>
    ''' Gets or sets the identifier nav configuration ent.
    ''' </summary>
    ''' <value>The identifier nav configuration ent.</value>
    Public Property IdNavConfigEnt() As Integer
        Get
            Return mIdnavconfigent
        End Get
        Set(ByVal Value As Integer)
            mIdnavconfigent = Value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the identifier nav ent.
    ''' </summary>
    ''' <value>The identifier nav ent.</value>
    Public Property IdNavEnt() As Integer
        Get
            Return mIdnavent
        End Get
        Set(ByVal Value As Integer)
            mIdnavent = Value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the endpoint.
    ''' </summary>
    ''' <value>The endpoint.</value>
    Public Property Endpoint() As String
        Get
            Return mEndpoint
        End Get
        Set(ByVal Value As String)
            mEndpoint = Value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeI_nav_config_ent"/> is activo.
    ''' </summary>
    ''' <value><c>true</c> if activo; otherwise, <c>false</c>.</value>
    Public Property Activo() As Boolean
        Get
            Return mActivo
        End Get
        Set(ByVal Value As Boolean)
            mActivo = Value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the fec_agr.
    ''' </summary>
    ''' <value>The fec_agr.</value>
    Public Property Fec_agr() As Date
        Get
            Return mFec_agr
        End Get
        Set(ByVal Value As Date)
            mFec_agr = Value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the user_agr.
    ''' </summary>
    ''' <value>The user_agr.</value>
    Public Property User_agr() As String
        Get
            Return mUser_agr
        End Get
        Set(ByVal Value As String)
            mUser_agr = Value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the fec_mod.
    ''' </summary>
    ''' <value>The fec_mod.</value>
    Public Property Fec_mod() As Date
        Get
            Return mFec_mod
        End Get
        Set(ByVal Value As Date)
            mFec_mod = Value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the user_mod.
    ''' </summary>
    ''' <value>The user_mod.</value>
    Public Property User_mod() As Integer
        Get
            Return mUser_mod
        End Get
        Set(ByVal Value As Integer)
            mUser_mod = Value
        End Set
    End Property

    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeI_nav_config_ent"/> class.
    ''' </summary>
    Sub New()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeI_nav_config_ent"/> class.
    ''' </summary>
    ''' <param name="idnavconfigent">The idnavconfigent.</param>
    ''' <param name="idnavent">The idnavent.</param>
    ''' <param name="endpoint">The endpoint.</param>
    ''' <param name="activo">if set to <c>true</c> [activo].</param>
    ''' <param name="fec_agr">The fec_agr.</param>
    ''' <param name="user_agr">The user_agr.</param>
    ''' <param name="fec_mod">The fec_mod.</param>
    ''' <param name="user_mod">The user_mod.</param>
    Sub New(ByRef idnavconfigent As Integer, ByVal idnavent As Integer, ByVal endpoint As String, ByVal activo As Boolean, ByVal fec_agr As Date, ByVal user_agr As String, ByVal fec_mod As Date, ByVal user_mod As Integer)
        mIdnavconfigent = Idnavconfigent
        mIdnavent = Idnavent
        mEndpoint = Endpoint
        mActivo = Activo
        mFec_agr = Fec_agr
        mUser_agr = User_agr
        mFec_mod = Fec_mod
        mUser_mod = User_mod
    End Sub

    ''' <summary>
    ''' Creates a new object that is a copy of the current instance.
    ''' </summary>
    ''' <returns>A new object that is a copy of this instance.</returns>
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
