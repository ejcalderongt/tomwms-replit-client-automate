' ***********************************************************************
' Assembly         : Entity
' Author           : ejcalderon
' Created          : 01-08-2018
'
' Last Modified By : ejcalderon
' Last Modified On : 01-08-2018
' ***********************************************************************
' <copyright file="clsBeI_nav_ent_filtros.vb" company="TEAM OS">
'     Copyright ©  2016
' </copyright>
' <summary></summary>
' ***********************************************************************
''' <summary>
''' Class clsBeI_nav_ent_filtros.
''' </summary>
''' <seealso cref="System.ICloneable" />
Public Class clsBeI_nav_ent_filtros
    Implements ICloneable

    ''' <summary>
    ''' Gets or sets the idnaventfiltro.
    ''' </summary>
    ''' <value>The idnaventfiltro.</value>
    Public Property Idnaventfiltro() As Integer = 0

    ''' <summary>
    ''' Gets or sets the idnavent.
    ''' </summary>
    ''' <value>The idnavent.</value>
    Public Property Idnavent() As Integer = 0

    ''' <summary>
    ''' Gets or sets the valor.
    ''' </summary>
    ''' <value>The valor.</value>
    Public Property Valor() As String = ""

    Public Property Tipo_Filtro As String = ""

    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeI_nav_ent_filtros"/> class.
    ''' </summary>
    Sub New()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeI_nav_ent_filtros"/> class.
    ''' </summary>
    ''' <param name="idnaventfiltro">The idnaventfiltro.</param>
    ''' <param name="idnavent">The idnavent.</param>
    ''' <param name="valor">The valor.</param>
    Sub New(ByRef idnaventfiltro As Integer, ByVal idnavent As Integer, ByVal valor As String)
        Me.Idnaventfiltro = idnaventfiltro
        Me.Idnavent = idnavent
        Me.Valor = valor
    End Sub

    ''' <summary>
    ''' Creates a new object that is a copy of the current instance.
    ''' </summary>
    ''' <returns>A new object that is a copy of this instance.</returns>
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
