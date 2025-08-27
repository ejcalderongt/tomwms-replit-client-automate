' ***********************************************************************
' Assembly         : Entity
' Author           : ejcalderon
' Created          : 11-08-2017
'
' Last Modified By : ejcalderon
' Last Modified On : 09-07-2017
' ***********************************************************************
' <copyright file="clsBeI_nav_ent.vb" company="TEAM OS">
'     Copyright ©  2016
' </copyright>
' <summary></summary>
' ***********************************************************************
''' <summary>
''' Class clsBeI_nav_ent.
''' </summary>
''' <seealso cref="System.ICloneable" />
Public Class clsBeI_nav_ent
    Implements ICloneable

    Public Property Idnavent() As Integer = 0
    Public Property Nombre() As String = ""
    Public Property Endpoint() As String
    Public Property lDetalleFiltros As New List(Of clsBeI_nav_ent_filtros)

    Sub New()
    End Sub

    Sub New(ByRef idnavent As Integer, ByVal nombre As String, ByVal endpoint As String)
        Me.Idnavent = idnavent
        Me.Nombre = nombre
        Me.Endpoint = endpoint
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
