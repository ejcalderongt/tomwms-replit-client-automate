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

    ''' <summary>
    ''' The m bodega_code
    ''' </summary>
    Private mBodega_code As String = ""
    ''' <summary>
    ''' The m bodega_name
    ''' </summary>
    Private mBodega_name As String = ""

    ''' <summary>
    ''' Gets or sets the bodega_code.
    ''' </summary>
    ''' <value>The bodega_code.</value>
    Public Property Bodega_code() As String
        Get
            Return mBodega_code
        End Get
        Set(ByVal Value As String)
            mBodega_code = Value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the bodega_name.
    ''' </summary>
    ''' <value>The bodega_name.</value>
    Public Property Bodega_name() As String
        Get
            Return mBodega_name
        End Get
        Set(ByVal Value As String)
            mBodega_name = Value
        End Set
    End Property

    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeI_nav_bodega"/> class.
    ''' </summary>
    Sub New()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeI_nav_bodega"/> class.
    ''' </summary>
    ''' <param name="bodega_code">The bodega_code.</param>
    ''' <param name="bodega_name">The bodega_name.</param>
    Sub New(ByRef bodega_code As String, ByVal bodega_name As String)
        mBodega_code = Bodega_code
        mBodega_name = Bodega_name
    End Sub

    ''' <summary>
    ''' Creates a new object that is a copy of the current instance.
    ''' </summary>
    ''' <returns>A new object that is a copy of this instance.</returns>
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
