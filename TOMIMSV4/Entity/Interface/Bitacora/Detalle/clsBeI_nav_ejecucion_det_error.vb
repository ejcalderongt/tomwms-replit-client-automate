' ***********************************************************************
' Assembly         : Entity
' Author           : ejcalderon
' Created          : 09-25-2017
'
' Last Modified By : ejcalderon
' Last Modified On : 09-28-2017
' ***********************************************************************
' <copyright file="clsBeI_nav_ejecucion_det_error.vb" company="TEAM OS">
'     Copyright ©  2016
' </copyright>
' <summary></summary>
' ***********************************************************************
''' <summary>
''' Class clsBeI_nav_ejecucion_det_error.
''' </summary>
''' <seealso cref="System.ICloneable" />
Public Class clsBeI_nav_ejecucion_det_error
    Implements ICloneable
    ''' <summary>
    ''' The m fecha
    ''' </summary>
    Private mFecha As Date = Date.Now

    ''' <summary>
    ''' Gets or sets the idejecuciondet.
    ''' </summary>
    ''' <value>The idejecuciondet.</value>
    Public Property Idejecuciondet() As Integer = 0

    ''' <summary>
    ''' Gets or sets the idejecucionenc.
    ''' </summary>
    ''' <value>The idejecucionenc.</value>
    Public Property Idejecucionenc() As Integer = 0

    ''' <summary>
    ''' Gets or sets the idnavconfigdet.
    ''' </summary>
    ''' <value>The idnavconfigdet.</value>
    Public Property Idnavconfigdet() As Integer = 0

    ''' <summary>
    ''' Gets or sets the errorr.
    ''' </summary>
    ''' <value>The errorr.</value>
    Public Property vError() As String = ""

    ''' <summary>
    ''' Gets or sets the referencia.
    ''' </summary>
    ''' <value>The referencia.</value>
    Public Property Referencia() As String = ""

    ''' <summary>
    ''' Gets or sets the fecha.
    ''' </summary>
    ''' <value>The fecha.</value>
    Public Property Fecha() As DateTime = Now

    Public Property No_Linea As Integer = 0

    Public Property Codigo_Producto As String = ""

    Public Property UMBas As String = ""

    Public Property Codigo_Presentacion As String = ""

    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeI_nav_ejecucion_det_error"/> class.
    ''' </summary>
    Sub New()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeI_nav_ejecucion_det_error"/> class.
    ''' </summary>
    ''' <param name="idejecuciondet">The idejecuciondet.</param>
    ''' <param name="idejecucionenc">The idejecucionenc.</param>
    ''' <param name="idnavconfigdet">The idnavconfigdet.</param>
    ''' <param name="errorr">The errorr.</param>
    ''' <param name="referencia">The referencia.</param>
    ''' <param name="fecha">The fecha.</param>
    Sub New(ByRef idejecuciondet As Integer, ByVal idejecucionenc As Integer, ByVal idnavconfigdet As Integer, ByVal errorr As String, ByVal referencia As String, ByVal fecha As Date)
        Me.Idejecuciondet = idejecuciondet
        Me.Idejecucionenc = idejecucionenc
        Me.Idnavconfigdet = idnavconfigdet
        vError = [errorr]
        Me.Referencia = referencia
        mFecha = fecha
    End Sub

    ''' <summary>
    ''' Creates a new object that is a copy of the current instance.
    ''' </summary>
    ''' <returns>A new object that is a copy of this instance.</returns>
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
