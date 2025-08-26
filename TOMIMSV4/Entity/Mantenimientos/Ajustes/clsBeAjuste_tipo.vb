' ***********************************************************************
' Assembly         : Entity
' Author           : ejcalderon
' Created          : 01-22-2018
'
' Last Modified By : ejcalderon
' Last Modified On : 01-26-2018
' ***********************************************************************
' <copyright file="clsBeAjuste_tipo.vb" company="TEAM OS">
'     Copyright ©  2016
' </copyright>
' <summary></summary>
' ***********************************************************************
''' <summary>
''' Class clsBeAjuste_tipo.
''' </summary>
''' <seealso cref="System.ICloneable" />
Public Class clsBeAjuste_tipo
    Implements ICloneable
    ''' <summary>
    ''' Gets or sets the idtipoajuste.
    ''' </summary>
    ''' <value>The idtipoajuste.</value>
    Public Property Idtipoajuste() As Integer = 0
    ''' <summary>
    ''' Gets or sets the nombre.
    ''' </summary>
    ''' <value>The nombre.</value>
    Public Property Nombre() As String = ""
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeAjuste_tipo"/> is modifica_lote.
    ''' </summary>
    ''' <value><c>true</c> if modifica_lote; otherwise, <c>false</c>.</value>
    Public Property Modifica_lote() As Boolean = False
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeAjuste_tipo"/> is momdifica_vencimiento.
    ''' </summary>
    ''' <value><c>true</c> if momdifica_vencimiento; otherwise, <c>false</c>.</value>
    Public Property Momdifica_vencimiento() As Boolean = False
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeAjuste_tipo"/> is modifica_cantidad.
    ''' </summary>
    ''' <value><c>true</c> if modifica_cantidad; otherwise, <c>false</c>.</value>
    Public Property Modifica_cantidad() As Boolean = False
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeAjuste_tipo"/> is modifica_peso.
    ''' </summary>
    ''' <value><c>true</c> if modifica_peso; otherwise, <c>false</c>.</value>
    Public Property Modifica_peso() As Boolean = False
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
    ''' Gets or sets a value indicating whether this <see cref="clsBeAjuste_tipo"/> is activo.
    ''' </summary>
    ''' <value><c>true</c> if activo; otherwise, <c>false</c>.</value>
    Public Property Activo() As Boolean = False
    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeAjuste_tipo"/> class.
    ''' </summary>
    Sub New()
    End Sub
    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeAjuste_tipo"/> class.
    ''' </summary>
    ''' <param name="idtipoajuste">The idtipoajuste.</param>
    ''' <param name="nombre">The nombre.</param>
    ''' <param name="modifica_lote">if set to <c>true</c> [modifica_lote].</param>
    ''' <param name="momdifica_vencimiento">if set to <c>true</c> [momdifica_vencimiento].</param>
    ''' <param name="modifica_cantidad">if set to <c>true</c> [modifica_cantidad].</param>
    ''' <param name="modifica_peso">if set to <c>true</c> [modifica_peso].</param>
    ''' <param name="fec_agr">The fec_agr.</param>
    ''' <param name="user_agr">The user_agr.</param>
    ''' <param name="fec_mod">The fec_mod.</param>
    ''' <param name="user_mod">The user_mod.</param>
    ''' <param name="activo">if set to <c>true</c> [activo].</param>
    Sub New(ByRef idtipoajuste As Integer, ByVal nombre As String, ByVal modifica_lote As Boolean, ByVal momdifica_vencimiento As Boolean, ByVal modifica_cantidad As Boolean, ByVal modifica_peso As Boolean, ByVal fec_agr As Date, ByVal user_agr As String, ByVal fec_mod As Date, ByVal user_mod As String, ByVal activo As Boolean)
        Me.Idtipoajuste = Idtipoajuste
        Me.Nombre = Nombre
        Me.Modifica_lote = Modifica_lote
        Me.Momdifica_vencimiento = Momdifica_vencimiento
        Me.Modifica_cantidad = Modifica_cantidad
        Me.Modifica_peso = Modifica_peso
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
