Partial Public Class clsBeTrans_ubic_hh_det
    Implements ICloneable
    Implements IDisposable

    Public Property IdTramo() As Integer = 0
    Public Property Tramo() As String = ""
    Public Property Indice_x() As Integer = 0
    Public Property Nivel() As Integer = 0
    Public Property ProductoPresentacion As New clsBeProducto_Presentacion()
    Public Property UnidadMedida As New clsBeUnidad_medida()
    Public Property ProductoEstado As New clsBeProducto_estado()
    Public Property UbicacionOrigen() As New clsBeBodega_ubicacion()
    Public Property UbicacionDestino() As New clsBeBodega_ubicacion()
    Public Property Producto() As New clsBeProducto()
    Public Property Stock() As New clsBeStock()
    Public Property Operador As New clsBeOperador()

    Sub New(ByRef IdTramo As Integer, ByRef Tramo As String, ByRef Indice_x As Integer, ByRef nivel As Integer, ByRef Recibido As Double)
        Me.IdTramo = IdTramo
        Me.Tramo = Tramo
        Me.Indice_x = Indice_x
        Me.Nivel = nivel
        Me.Recibido = Recibido
    End Sub

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
        If ProductoPresentacion IsNot Nothing Then
            ProductoPresentacion.Dispose()
            ProductoPresentacion = Nothing
        End If
        If UnidadMedida IsNot Nothing Then
            UnidadMedida.Dispose()
            UnidadMedida = Nothing
        End If
        If ProductoEstado IsNot Nothing Then
            ProductoEstado.Dispose()
            ProductoEstado = Nothing
        End If
        If Producto IsNot Nothing Then
            Producto.Dispose()
            Producto = Nothing
        End If
        If Stock IsNot Nothing Then
            Stock.Dispose()
            Stock = Nothing
        End If
        If Operador IsNot Nothing Then
            Operador.Dispose()
            Operador = Nothing
        End If
    End Sub
#End Region

End Class
