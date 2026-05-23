Partial Public Class clsBeTrans_re_det
    Implements IDisposable

    Public Property Producto As clsBeProducto = New clsBeProducto()
    Public Property Presentacion As clsBeProducto_Presentacion = New clsBeProducto_Presentacion()
    Public Property ProductoEstado As clsBeProducto_estado = New clsBeProducto_estado()
    Public Property UnidadMedida As clsBeUnidad_medida = New clsBeUnidad_medida()
    Public Property MotivoDevolucion As clsBeMotivo_devolucion = New clsBeMotivo_devolucion()
    Public Property IsNew() As Boolean = True
    Public Property Control_Peso As Boolean
    Public Property IdPropietarioBodega As Integer
    Public Property IdUbicacion As Integer
    Public Property IdUbicacionAnterior As Integer
    Public Property Fecha_Rec As Date
    Public Property Fecha_tarea As Date
    Public Property Hora_ini As Date
    Public Property Hora_Fin As Date
    Public Property Estado_Rec As String
    Public Property UbicacionCompleta As String
    Public Property Lic_plate() As String = ""
    Public Property Uds_lic_plate() As Double = 0
    '#EJC20220407: El principio del fin.
    Public Property IdOrdenCompraEnc As Integer = 0
    Public Property IdOrdenCompraDet As Integer = 0
    '#EJC202302210910: Identificar si ya ejecutó proceso de retroactivo.
    Public Property IdJornadaSistema As Integer = 0
    Public Property Host As String = ""
    Public Property Talla As clsBeTalla = New clsBeTalla()
    Public Property Color As clsBeColor = New clsBeColor()
    Public Property CodigoSKU As String = ""

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
        If Producto IsNot Nothing Then
            Producto.Dispose()
            Producto = Nothing
        End If
        If MotivoDevolucion IsNot Nothing Then
            MotivoDevolucion.Dispose()
            MotivoDevolucion = Nothing
        End If
    End Sub
#End Region

End Class
