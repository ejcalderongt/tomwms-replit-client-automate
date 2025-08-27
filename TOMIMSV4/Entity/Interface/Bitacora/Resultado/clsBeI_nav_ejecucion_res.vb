' ***********************************************************************
' Assembly         : Entity
' Author           : ejcalderon
' Created          : 09-25-2017
'
' Last Modified By : ejcalderon
' Last Modified On : 11-26-2017
' ***********************************************************************
' <copyright file="clsBeI_nav_ejecucion_res.vb" company="TEAM OS">
'     Copyright ©  2016
' </copyright>
' <summary></summary>
' ***********************************************************************
''' <summary>
''' Class clsBeI_nav_ejecucion_res.
''' </summary>
''' <seealso cref="System.ICloneable" />
Public Class clsBeI_nav_ejecucion_res
    Implements ICloneable

    ''' <summary>
    ''' Gets or sets the idejecucionres.
    ''' </summary>
    ''' <value>The idejecucionres.</value>
    Public Property IdEjecucionRes() As Integer = 0
    ''' <summary>
    ''' Gets or sets the idejecucionenc.
    ''' </summary>
    ''' <value>The idejecucionenc.</value>
    Public Property IdEjecucionEnc() As Integer = 0
    ''' <summary>
    ''' Gets or sets the idnavconfigdet.
    ''' </summary>
    ''' <value>The idnavconfigdet.</value>
    Public Property IdNavConfigDet() As Integer = 0
    ''' <summary>
    ''' Gets or sets the registros_ws.
    ''' </summary>
    ''' <value>The registros_ws.</value>
    Public Property Registros_ws() As Integer = 0
    ''' <summary>
    ''' Gets or sets the registros_ti.
    ''' </summary>
    ''' <value>The registros_ti.</value>
    Public Property Registros_ti() As Integer = 0
    ''' <summary>
    ''' Gets or sets the registros_wms.
    ''' </summary>
    ''' <value>The registros_wms.</value>
    Public Property Registros_WMS() As Integer = 0
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeI_nav_ejecucion_res"/> is exitosa.
    ''' </summary>
    ''' <value><c>true</c> if exitosa; otherwise, <c>false</c>.</value>
    Public Property Exitosa() As Boolean = False

    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeI_nav_ejecucion_res"/> class.
    ''' </summary>
    Sub New()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeI_nav_ejecucion_res"/> class.
    ''' </summary>
    ''' <param name="idejecucionres">The idejecucionres.</param>
    ''' <param name="idejecucionenc">The idejecucionenc.</param>
    ''' <param name="idnavconfigdet">The idnavconfigdet.</param>
    ''' <param name="registros_ws">The registros_ws.</param>
    ''' <param name="registros_ti">The registros_ti.</param>
    ''' <param name="registros_wms">The registros_wms.</param>
    ''' <param name="exitosa">if set to <c>true</c> [exitosa].</param>
    Sub New(ByRef idejecucionres As Integer, ByVal idejecucionenc As Integer, ByVal idnavconfigdet As Integer, ByVal registros_ws As Integer, ByVal registros_ti As Integer, ByVal registros_wms As Integer, ByVal exitosa As Boolean)
        Me.IdEjecucionRes = idejecucionres
        Me.IdEjecucionEnc = idejecucionenc
        Me.IdNavConfigDet = idnavconfigdet
        Me.Registros_ws = registros_ws
        Me.Registros_ti = registros_ti
        Me.Registros_WMS = registros_wms
        Me.Exitosa = exitosa
    End Sub

    ''' <summary>
    ''' Creates a new object that is a copy of the current instance.
    ''' </summary>
    ''' <returns>A new object that is a copy of this instance.</returns>
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
