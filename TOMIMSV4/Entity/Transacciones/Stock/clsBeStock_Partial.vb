Partial Public Class clsBeStock
    Implements IDisposable

    Public Property IsNew() As Boolean = True
    Public Property ProductoValidado() As Boolean
    Public Property UbicacionAnterior() As String = ""
    Public Property Presentacion As New clsBeProducto_Presentacion
    Public Property ProductoEstado As New clsBeProducto_estado
    Public Property Parametros As New List(Of clsBeStock_parametro)
    Public Property Producto As New clsBeProducto
    Public Property IdStockOrigen As Integer = 0
    Public Property IsReportStockEnFecha As Boolean = False
    'EJC20260602_STOCK_FECHA: Permite incluir existencias en ubicaciones de despacho en reportes puntuales.
    Public Property IncluirUbicacionesDespacho As Boolean = False
    Public Property UbicacionPicking As Boolean = False
    Public Property UbicacionNivel As Integer = 0
    Public Property Pallet_Completo As Boolean = False
    Public Property Talla As String = String.Empty
    Public Property Color As String = String.Empty


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
        If Presentacion IsNot Nothing Then
            Presentacion.Dispose()
            Presentacion = Nothing
        End If
        If ProductoEstado IsNot Nothing Then
            ProductoEstado.Dispose()
            ProductoEstado = Nothing
        End If
        If Producto IsNot Nothing Then
            Producto.Dispose()
            Producto = Nothing
        End If
    End Sub
#End Region

End Class
