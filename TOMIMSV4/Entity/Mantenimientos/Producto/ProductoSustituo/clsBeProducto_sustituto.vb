<Serializable>
Public Class clsBeProducto_sustituto
    Implements ICloneable
    Implements IDisposable

    ''' <summary>
    ''' Gets or sets the identifier producto sustituto.
    ''' </summary>
    ''' <value>The identifier producto sustituto.</value>
    Public Property IdProductoSustituto() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier producto original.
    ''' </summary>
    ''' <value>The identifier producto original.</value>
    Public Property IdProductoOriginal() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier producto presentacion original.
    ''' </summary>
    ''' <value>The identifier producto presentacion original.</value>
    Public Property IdProductoPresentacionOriginal() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier producto reemplazo.
    ''' </summary>
    ''' <value>The identifier producto reemplazo.</value>
    Public Property IdProductoReemplazo() As Integer = 0
    ''' <summary>
    ''' Gets or sets the identifier producto presentacion reemplazo.
    ''' </summary>
    ''' <value>The identifier producto presentacion reemplazo.</value>
    Public Property IdProductoPresentacionReemplazo() As Integer = 0
    ''' <summary>
    ''' Gets or sets the producto reemplazo.
    ''' </summary>
    ''' <value>The producto reemplazo.</value>
    Public Property ProductoReemplazo As clsBeProducto = New clsBeProducto()
    ''' <summary>
    ''' Gets or sets the producto presentacion original.
    ''' </summary>
    ''' <value>The producto presentacion original.</value>
    Public Property ProductoPresentacionOriginal As clsBeProducto_Presentacion = New clsBeProducto_Presentacion()
    ''' <summary>
    ''' Gets or sets the producto presentacion reemplazo.
    ''' </summary>
    ''' <value>The producto presentacion reemplazo.</value>
    Public Property ProductoPresentacionReemplazo As clsBeProducto_Presentacion = New clsBeProducto_Presentacion()
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
    ''' Gets or sets a value indicating whether this <see cref="clsBeProducto_sustituto"/> is activo.
    ''' </summary>
    ''' <value><c>true</c> if activo; otherwise, <c>false</c>.</value>
    Public Property Activo() As Boolean = False
    ''' <summary>
    ''' Gets or sets a value indicating whether this instance is new.
    ''' </summary>
    ''' <value><c>true</c> if this instance is new; otherwise, <c>false</c>.</value>
    Public Property IsNew() As Boolean = False

    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeProducto_sustituto"/> class.
    ''' </summary>
    Sub New()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="clsBeProducto_sustituto"/> class.
    ''' </summary>
    ''' <param name="IdProductoSustituto">The identifier producto sustituto.</param>
    ''' <param name="IdProductoOriginal">The identifier producto original.</param>
    ''' <param name="IdProductoPresentacionOriginal">The identifier producto presentacion original.</param>
    ''' <param name="IdProductoReemplazo">The identifier producto reemplazo.</param>
    ''' <param name="IdProductoPresentacionReemplazo">The identifier producto presentacion reemplazo.</param>
    ''' <param name="user_agr">The user_agr.</param>
    ''' <param name="fec_agr">The fec_agr.</param>
    ''' <param name="user_mod">The user_mod.</param>
    ''' <param name="fec_mod">The fec_mod.</param>
    ''' <param name="activo">if set to <c>true</c> [activo].</param>
    Sub New(ByRef IdProductoSustituto As Integer, ByVal IdProductoOriginal As Integer, ByVal IdProductoPresentacionOriginal As Integer, ByVal IdProductoReemplazo As Integer, ByVal IdProductoPresentacionReemplazo As Integer, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal activo As Boolean)
        Me.IdProductoSustituto = IdProductoSustituto
        Me.IdProductoOriginal = IdProductoOriginal
        Me.IdProductoPresentacionOriginal = IdProductoPresentacionOriginal
        Me.IdProductoReemplazo = IdProductoReemplazo
        Me.IdProductoPresentacionReemplazo = IdProductoPresentacionReemplazo
        Me.User_agr = User_agr
        Me.Fec_agr = Fec_agr
        Me.User_mod = User_mod
        Me.Fec_mod = Fec_mod
        Me.Activo = Activo
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
        If ProductoReemplazo IsNot Nothing Then
            ProductoReemplazo.Dispose()
            ProductoReemplazo = Nothing
        End If
    End Sub
#End Region

End Class
