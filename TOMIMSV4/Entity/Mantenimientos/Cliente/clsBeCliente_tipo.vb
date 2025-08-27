' ***********************************************************************
' Assembly         : Entity
' Author           : ejcalderon
' Created          : 08-14-2017
'
' Last Modified By : ejcalderon
' Last Modified On : 08-30-2017
' ***********************************************************************
' <copyright file="clsBeCliente_tipo.vb" company="TEAM OS">
'     Copyright ©  2016
' </copyright>
' <summary></summary>
' ***********************************************************************
''' <summary>
''' Class clsBeCliente_tipo.
''' </summary>
''' <seealso cref="System.ICloneable" />
''' <seealso cref="System.IDisposable" />
Public Class clsBeCliente_tipo
    Implements ICloneable
    Implements IDisposable

    ''' <summary>
    ''' Gets or sets the identifier tipo cliente.
    ''' </summary>
    ''' <value>The identifier tipo cliente.</value>
    Public Property IdTipoCliente As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier propietario.
    ''' </summary>
    ''' <value>The identifier propietario.</value>
    Public Property IdPropietario As Integer = 0
    ''' <summary>
    ''' Gets or sets the propietario.
    ''' </summary>
    ''' <value>The propietario.</value>
    Public Property Propietario As clsBePropietarios
    ''' <summary>
    ''' Gets or sets the nombre tipo cliente.
    ''' </summary>
    ''' <value>The nombre tipo cliente.</value>
    Public Property NombreTipoCliente As String = ""
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeCliente_tipo"/> is activo.
    ''' </summary>
    ''' <value><c>true</c> if activo; otherwise, <c>false</c>.</value>
    Public Property Activo As Boolean = False
    ''' <summary>
    ''' Gets or sets the user_agr.
    ''' </summary>
    ''' <value>The user_agr.</value>
    Public Property User_agr As String = ""
    ''' <summary>
    ''' Gets or sets the fec_agr.
    ''' </summary>
    ''' <value>The fec_agr.</value>
    Public Property Fec_agr As Date = Date.Now
    ''' <summary>
    ''' Gets or sets the user_mod.
    ''' </summary>
    ''' <value>The user_mod.</value>
    Public Property User_mod As String = ""
    ''' <summary>
    ''' Gets or sets the fec_mod.
    ''' </summary>
    ''' <value>The fec_mod.</value>
    Public Property Fec_mod As Date = Date.Now

    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeCliente_tipo"/> class.
    ''' </summary>
    Sub New()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeCliente_tipo"/> class.
    ''' </summary>
    ''' <param name="IdTipoCliente">The identifier tipo cliente.</param>
    ''' <param name="IdPropietario">The identifier propietario.</param>
    ''' <param name="NombreTipoCliente">The nombre tipo cliente.</param>
    ''' <param name="Activo">if set to <c>true</c> [activo].</param>
    ''' <param name="user_agr">The user_agr.</param>
    ''' <param name="fec_agr">The fec_agr.</param>
    ''' <param name="user_mod">The user_mod.</param>
    ''' <param name="fec_mod">The fec_mod.</param>
    Sub New(ByRef IdTipoCliente As Integer, ByVal IdPropietario As Integer, ByVal NombreTipoCliente As String, ByVal Activo As Boolean, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date)
        IdTipoCliente = IdTipoCliente
        IdPropietario = IdPropietario
        NombreTipoCliente = NombreTipoCliente
        Activo = Activo
        user_agr = user_agr
        fec_agr = fec_agr
        user_mod = user_mod
        fec_mod = fec_mod
    End Sub

    ''' <summary>
    ''' Creates a new object that is a copy of the current instance.
    ''' </summary>
    ''' <returns>A new object that is a copy of this instance.</returns>
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function


#Region "IDisposable Support"
    ''' <summary>
    ''' The disposed value
    ''' </summary>
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    ''' <summary>
    ''' Releases unmanaged and - optionally - managed resources.
    ''' </summary>
    ''' <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    ''' <summary>
    ''' Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    ''' </summary>
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
        If Propietario IsNot Nothing Then
            Propietario.Dispose()
            Propietario = Nothing
        End If
    End Sub
#End Region

End Class
