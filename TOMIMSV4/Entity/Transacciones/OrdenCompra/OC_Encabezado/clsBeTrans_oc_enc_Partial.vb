Partial Public Class clsBeTrans_oc_enc
    Implements IDisposable

    Public Property DetalleOC As New List(Of clsBeTrans_oc_det)
    Public Property DetalleLotes As New List(Of clsBeTrans_oc_det_lote)
    Public Property DetallePallets As New List(Of clsBeI_nav_barras_pallet)
    Public Property ObjPoliza As New clsBeTrans_oc_pol
    Public Property ListaImg As New List(Of clsBeTrans_oc_imagen)
    Public Property PropietarioBodega As clsBePropietario_bodega = New clsBePropietario_bodega()
    Public Property ProveedorBodega As clsBeProveedor_bodega = New clsBeProveedor_bodega()
    Public Property EstadoOC() As clsBeTrans_oc_estado = New clsBeTrans_oc_estado()
    Public Property IdBodega() As Integer
    Public Property IsNew() As Boolean
    Public Property EsDevolucion() As Boolean
    Public Property TipoIngreso As New clsBeTrans_oc_ti
    Public Property ExisteRecepcionNoFinalizada As Boolean

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
        If ObjPoliza IsNot Nothing Then
            ObjPoliza.Dispose()
            ObjPoliza = Nothing
        End If
        If EstadoOC IsNot Nothing Then
            EstadoOC.Dispose()
            EstadoOC = Nothing
        End If
    End Sub
#End Region

End Class