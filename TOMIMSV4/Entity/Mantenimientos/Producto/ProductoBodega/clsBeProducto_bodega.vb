<Serializable>
Public Class clsBeProducto_bodega
    Implements ICloneable
    Implements IDisposable

    ''' <summary>
    ''' Gets or sets the identifier producto bodega.
    ''' </summary>
    ''' <value>The identifier producto bodega.</value>
    Public Property IdProductoBodega() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier producto.
    ''' </summary>
    ''' <value>The identifier producto.</value>
    Public Property IdProducto() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier bodega.
    ''' </summary>
    ''' <value>The identifier bodega.</value>
    Public Property IdBodega() As Integer = 0
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeProducto_bodega"/> is activo.
    ''' </summary>
    ''' <value><c>true</c> if activo; otherwise, <c>false</c>.</value>
    Public Property Activo() As Boolean = False
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="clsBeProducto_bodega"/> is sistema.
    ''' </summary>
    ''' <value><c>true</c> if sistema; otherwise, <c>false</c>.</value>
    Public Property Sistema() As Boolean = False
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
    ''' Initializes a new instance of the <see cref="clsBeProducto_bodega"/> class.
    ''' </summary>
    Sub New()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeProducto_bodega"/> class.
    ''' </summary>
    ''' <param name="IdProductoBodega">The identifier producto bodega.</param>
    ''' <param name="IdProducto">The identifier producto.</param>
    ''' <param name="IdBodega">The identifier bodega.</param>
    ''' <param name="activo">if set to <c>true</c> [activo].</param>
    ''' <param name="sistema">if set to <c>true</c> [sistema].</param>
    ''' <param name="user_agr">The user_agr.</param>
    ''' <param name="fec_agr">The fec_agr.</param>
    ''' <param name="user_mod">The user_mod.</param>
    ''' <param name="fec_mod">The fec_mod.</param>
    Sub New(ByRef IdProductoBodega As Integer, ByVal IdProducto As Integer, ByVal IdBodega As Integer, ByVal activo As Boolean, ByVal sistema As Boolean, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date)
        Me.IdProductoBodega = IdProductoBodega
        Me.IdProducto = IdProducto
        Me.IdBodega = IdBodega
        Me.Activo = Activo
        Me.Sistema = Sistema
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
        If Producto IsNot Nothing Then
            Producto.Dispose()
            Producto = Nothing
        End If
    End Sub
#End Region

End Class
