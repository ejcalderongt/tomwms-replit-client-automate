Public Class clsBeRoad_p_vendedor
    Implements ICloneable
    Implements IDisposable

    Public Property IdVendedor() As Integer = 0
    Public Property Codigo() As String = ""
    Public Property Nombre() As String = ""
    Public Property Clave() As String = ""
    Public Property Ruta() As String = ""
    Public Property Nivel() As Integer = 0
    Public Property Nivelprecio() As Integer = 0
    Public Property Bodega() As String = ""
    Public Property Subbodega() As String = ""
    Public Property Cod_vehiculo() As String = ""
    Public Property Liquidando() As String = ""
    Public Property Ultima_fecha_liq() As Date = Date.Now
    Public Property Bloqueado() As Boolean = False
    Public Property Devolucion_sap() As Integer = 0
    Public Property IdRuta As Integer = 0

    Sub New()
    End Sub

    Sub New(ByRef IdVendedor As Integer, ByVal codigo As String, ByVal nombre As String, ByVal clave As String, ByVal ruta As String, ByVal nivel As Integer, ByVal nivelprecio As Integer, ByVal bodega As String, ByVal subbodega As String, ByVal cod_vehiculo As String, ByVal liquidando As String, ByVal ultima_fecha_liq As Date, ByVal bloqueado As Boolean, ByVal devolucion_sap As Integer)
        Me.IdVendedor = IdVendedor
        Me.Codigo = Codigo
        Me.Nombre = Nombre
        Me.Clave = Clave
        Me.Ruta = Ruta
        Me.Nivel = Nivel
        Me.Nivelprecio = Nivelprecio
        Me.Bodega = Bodega
        Me.Subbodega = Subbodega
        Me.Cod_vehiculo = Cod_vehiculo
        Me.Liquidando = Liquidando
        Me.Ultima_fecha_liq = Ultima_fecha_liq
        Me.Bloqueado = Bloqueado
        Me.Devolucion_sap = Devolucion_sap
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
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
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
