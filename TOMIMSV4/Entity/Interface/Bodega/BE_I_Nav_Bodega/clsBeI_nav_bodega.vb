' ***********************************************************************
' Assembly         : Entity
' Author           : ejcalderon
' Created          : 09-02-2017
'
' Last Modified By : ejcalderon
' Last Modified On : 09-02-2017
' ***********************************************************************
' <copyright file="clsBeI_nav_bodega.vb" company="TEAM OS">
'     Copyright ©  2016
' </copyright>
' <summary></summary>
' ***********************************************************************
''' <summary>
''' Class clsBeI_nav_bodega.
''' </summary>
''' <seealso cref="System.ICloneable" />
Public Class clsBeI_nav_bodega
    Implements ICloneable

    Public Property Bodega_code() As String = ""
    Public Property Bodega_name() As String = ""
    Sub New()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeI_nav_bodega"/> class.
    ''' </summary>
    ''' <param name="bodega_code">The bodega_code.</param>
    ''' <param name="bodega_name">The bodega_name.</param>
    Sub New(ByRef bodega_code As String, ByVal bodega_name As String)
        Me.Bodega_code = bodega_code
        Me.Bodega_name = bodega_name
    End Sub

    ''' <summary>
    ''' Creates a new object that is a copy of the current instance.
    ''' </summary>
    ''' <returns>A new object that is a copy of this instance.</returns>
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
