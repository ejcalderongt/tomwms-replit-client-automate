' ***********************************************************************
' Assembly         : Entity
' Author           : ejcalderon
' Created          : 01-22-2018
'
' Last Modified By : ejcalderon
' Last Modified On : 03-30-2018
' ***********************************************************************
' <copyright file="clsBeAjuste_motivo.vb" company="TEAM OS">
'     Copyright ©  2016
' </copyright>
' <summary></summary>
' ***********************************************************************
''' <summary>
''' Class clsBeAjuste_motivo.
''' </summary>
''' <seealso cref="System.ICloneable" />
Public Class clsBeAjuste_motivo
    Implements ICloneable
    ''' <summary>
    ''' Gets or sets the idmotivoajuste.
    ''' </summary>
    ''' <value>The idmotivoajuste.</value>
    Public Property Idmotivoajuste() As Integer = 0
    ''' <summary>
    ''' Gets or sets the nombre.
    ''' </summary>
    ''' <value>The nombre.</value>
    Public Property Nombre() As String = ""
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
    ''' Gets or sets a value indicating whether this <see cref="clsBeAjuste_motivo"/> is activo.
    ''' </summary>
    ''' <value><c>true</c> if activo; otherwise, <c>false</c>.</value>
    Public Property Activo() As Boolean = False
    ''' <summary>
    ''' Gets or sets the Sistema.
    ''' </summary>
    ''' <value>The Sistema.</value>
    Public Property Sistema() As Boolean = False
    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeAjuste_motivo"/> class.
    ''' </summary>
    Sub New()
    End Sub
    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeAjuste_motivo"/> class.
    ''' </summary>
    ''' <param name="idmotivoajuste">The idmotivoajuste.</param>
    ''' <param name="nombre">The nombre.</param>
    ''' <param name="fec_agr">The fec_agr.</param>
    ''' <param name="user_agr">The user_agr.</param>
    ''' <param name="fec_mod">The fec_mod.</param>
    ''' <param name="user_mod">The user_mod.</param>
    ''' <param name="activo">if set to <c>true</c> [activo].</param>
    Sub New(ByRef idmotivoajuste As Integer, ByVal nombre As String, ByVal fec_agr As Date, ByVal user_agr As String, ByVal fec_mod As Date, ByVal user_mod As String, ByVal activo As Boolean)
        Me.Idmotivoajuste = Idmotivoajuste
        Me.Nombre = Nombre
        Me.Fec_agr = Fec_agr
        Me.User_agr = User_agr
        Me.Fec_mod = Fec_mod
        Me.User_mod = User_mod
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
