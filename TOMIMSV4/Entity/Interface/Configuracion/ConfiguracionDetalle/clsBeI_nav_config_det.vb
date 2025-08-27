' ***********************************************************************
' Assembly         : Entity
' Author           : ejcalderon
' Created          : 11-08-2017
'
' Last Modified By : ejcalderon
' Last Modified On : 11-08-2017
' ***********************************************************************
' <copyright file="clsBeI_nav_config_det.vb" company="TEAM OS">
'     Copyright ©  2016
' </copyright>
' <summary></summary>
' ***********************************************************************
''' <summary>
''' Class clsBeI_nav_config_det.
''' </summary>
''' <seealso cref="System.ICloneable" />
Public Class clsBeI_nav_config_det
    Implements ICloneable
    ''' <summary>
    ''' Gets or sets the idnavconfigdet.
    ''' </summary>
    ''' <value>The idnavconfigdet.</value>
    Public Property Idnavconfigdet() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier nav ent.
    ''' </summary>
    ''' <value>The identifier nav ent.</value>
    Public Property IdNavEnt() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier nav configuration enc.
    ''' </summary>
    ''' <value>The identifier nav configuration enc.</value>
    Public Property IdNavConfigEnc() As Integer = 0
    ''' <summary>
    ''' Gets or sets the dia.
    ''' </summary>
    ''' <value>The dia.</value>
    Public Property Dia() As Integer = 0
    ''' <summary>
    ''' Gets or sets the hora inicio.
    ''' </summary>
    ''' <value>The hora inicio.</value>
    Public Property HoraInicio() As Date = Date.Now
    ''' <summary>
    ''' Gets or sets the hora fin.
    ''' </summary>
    ''' <value>The hora fin.</value>
    Public Property HoraFin() As Date = Date.Now
    ''' <summary>
    ''' Gets or sets the frecuencia.
    ''' </summary>
    ''' <value>The frecuencia.</value>
    Public Property Frecuencia() As Integer = 0
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeI_nav_config_det"/> is activo.
    ''' </summary>
    ''' <value><c>true</c> if activo; otherwise, <c>false</c>.</value>
    Public Property Activo() As Boolean = False
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
    ''' Initializes a new instance of the <see cref="clsBeI_nav_config_det"/> class.
    ''' </summary>
    Sub New()
    End Sub
    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeI_nav_config_det"/> class.
    ''' </summary>
    ''' <param name="idnavconfigdet">The idnavconfigdet.</param>
    ''' <param name="idnavent">The idnavent.</param>
    ''' <param name="idnavconfigenc">The idnavconfigenc.</param>
    ''' <param name="dia">The dia.</param>
    ''' <param name="horainicio">The horainicio.</param>
    ''' <param name="horafin">The horafin.</param>
    ''' <param name="frecuencia">The frecuencia.</param>
    ''' <param name="activo">if set to <c>true</c> [activo].</param>
    ''' <param name="fec_agr">The fec_agr.</param>
    ''' <param name="user_agr">The user_agr.</param>
    ''' <param name="fec_mod">The fec_mod.</param>
    ''' <param name="user_mod">The user_mod.</param>
    Sub New(ByRef idnavconfigdet As Integer, ByVal idnavent As Integer, ByVal idnavconfigenc As Integer, ByVal dia As Integer, ByVal horainicio As Date, ByVal horafin As Date, ByVal frecuencia As Integer, ByVal activo As Boolean, ByVal fec_agr As Date, ByVal user_agr As String, ByVal fec_mod As Date, ByVal user_mod As String)
        Me.Idnavconfigdet = Idnavconfigdet
        Me.IdNavEnt = Idnavent
        Me.IdNavConfigEnc = Idnavconfigenc
        Me.Dia = Dia
        Me.HoraInicio = Horainicio
        Me.HoraFin = Horafin
        Me.Frecuencia = Frecuencia
        Me.Activo = Activo
        Me.Fec_agr = Fec_agr
        Me.User_agr = User_agr
        Me.Fec_mod = Fec_mod
        Me.User_mod = User_mod
    End Sub
    ''' <summary>
    ''' Creates a new object that is a copy of the current instance.
    ''' </summary>
    ''' <returns>A new object that is a copy of this instance.</returns>
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function
End Class
