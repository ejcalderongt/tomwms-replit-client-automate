Partial Public Class clsBeTrans_picking_ubic
    Implements IDisposable

    Public Property IsNew() As Boolean
    Public Property NombreUbicacion As String = ""
    Public Property NombreUbicacionTemporal As String = ""
    Public Property IdPedidoDet As Integer = 0
    Public Property IdStockRes As Integer = 0
    Public Property IdStock As Integer = 0
    Public Property CodigoProducto() As String = ""
    Public Property NombreProducto() As String = ""
    Public Property ProductoPresentacion() As String = ""
    Public Property ProductoUnidadMedida() As String = ""
    Public Property ProductoEstado() As String = ""
    Public Property IdProductoBodega As Integer = 0
    Public Property IdProductoEstado As Integer = 0
    Public Property IdPresentacion As Integer = 0
    Public Property IdUnidadMedida As Integer = 0
    Public Property IdPedidoEnc As Integer = 0
    Public Property Ubicacion As New clsBeBodega_ubicacion()
    Public Property IdPropietarioBodega As Integer = 0
    Public Property IdUbicacionAnterior As Integer = 0
    Public Property IdRecepcion As Integer = 0
    Public Property CantidadDañada As Double = 0
    Public Property Lic_plate_Reemplazo As String = ""
    Public Property IdUbicacion_reemplazo() As Integer = 0
    Public Property IdStock_reemplazo() As Integer = 0
    Public Property IdBodega As Integer = 0
    Public Property Tarima As Double = 0
    Public Property NombreArea As String = ""
    Public Property NombreClasificacion As String = ""
    ''' <summary>
    ''' #EJC20220330_CEALSA: Se utiliza para cambio de ubicación de producto que está reservado en picking.
    ''' </summary>
    ''' <returns></returns>
    Public Property IdUbicacionTemporal As Integer = 0
    Public Property Referencia As String = ""

    '#EJC20220816: Analizando para el futuro.
    'Public Property IdDespachoDet as Integer =0
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
