<Serializable>
Public Class clsBeProducto_marca
    Implements ICloneable
    Implements IDisposable

    ''' <summary>
    ''' Gets or sets the identifier marca.
    ''' </summary>
    ''' <value>The identifier marca.</value>
    Public Property IdMarca() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier propietario.
    ''' </summary>
    ''' <value>The identifier propietario.</value>
    Public Property IdPropietario() As Integer = 0
    ''' <summary>
    ''' Gets or sets the propietario.
    ''' </summary>
    ''' <value>The propietario.</value>
    Public Property Propietario As clsBePropietarios = New clsBePropietarios()
    ''' <summary>
    ''' Gets or sets the nombre.
    ''' </summary>
    ''' <value>The nombre.</value>
    Public Property Nombre() As String = ""
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeProducto_marca"/> is activo.
    ''' </summary>
    ''' <value><c>true</c> if activo; otherwise, <c>false</c>.</value>
    Public Property Activo() As Boolean = False
    ''' <summary>
    ''' Gets or sets the user_agr.
    ''' </summary>
    ''' <value>The user_agr.</value>
    Public Property User_agr() As String = ""
    ''' <summary>
    ''' Gets or sets the fec_agr.
    ''' </summary>
    ''' <value>The fec_agr.</value>
    Public Property Fec_agr() As Date = Date.Now
    ''' <summary>
    ''' Gets or sets the user_mod.
    ''' </summary>
    ''' <value>The user_mod.</value>
    Public Property User_mod() As String = ""
    ''' <summary>
    ''' Gets or sets the fec_mod.
    ''' </summary>
    ''' <value>The fec_mod.</value>
    Public Property Fec_mod() As Date = Date.Now
    ''' <summary>
    ''' Gets or sets a value indicating whether this instance is new.
    ''' </summary>
    ''' <value><c>true</c> if this instance is new; otherwise, <c>false</c>.</value>
    Public Property IsNew() As Boolean = False

    Public Property Codigo() As String = ""

    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeProducto_marca"/> class.
    ''' </summary>
    Sub New()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeProducto_marca"/> class.
    ''' </summary>
    ''' <param name="IdMarca">The identifier marca.</param>
    ''' <param name="IdPropietario">The identifier propietario.</param>
    ''' <param name="nombre">The nombre.</param>
    ''' <param name="activo">if set to <c>true</c> [activo].</param>
    ''' <param name="user_agr">The user_agr.</param>
    ''' <param name="fec_agr">The fec_agr.</param>
    ''' <param name="user_mod">The user_mod.</param>
    ''' <param name="fec_mod">The fec_mod.</param>
    Sub New(ByRef IdMarca As Integer, ByVal IdPropietario As Integer, ByVal nombre As String, ByVal activo As Boolean, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date)
        Me.IdMarca = IdMarca
        Me.IdPropietario = IdPropietario
        Me.Nombre = Nombre
        Me.Activo = Activo
        Me.User_agr = User_agr
        Me.Fec_agr = Fec_agr
        Me.User_mod = User_mod
        Me.Fec_mod = Fec_mod
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
