Partial Public Class clsBeTrans_pe_enc
    Implements IDisposable

    Public Property IsNew As Boolean = False

    Public Detalle As List(Of clsBeTrans_pe_det) = Nothing

    '#EJC20171021_0450PM: Obtener el picking (Activo) asociado al pedido.
    Public Property Picking As New clsBeTrans_picking_enc
    Public Property PropietarioBodega As New clsBePropietario_bodega
    Public Property Cliente As New clsBeCliente
    Public Property TipoPedido As New clsBeTrans_pe_tipo
    Public Property Control_Ultimo_Lote As Boolean = False
    Public Property Serie As String = ""
    Public Property Correlativo As Integer = 0
    Public Property ObjPoliza As New clsBeTrans_pe_pol
    Public Property IdTipoManufactura As Integer = clsDataContractDI.Manufacturing_Process.Sin_Proceso_Nativo

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

#Region "IDisposable Support"
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
        If Cliente IsNot Nothing Then
            Cliente.Dispose()
            Cliente = Nothing
        End If
        If TipoPedido IsNot Nothing Then
            TipoPedido.Dispose()
            TipoPedido = Nothing
        End If
        If PropietarioBodega IsNot Nothing Then
            PropietarioBodega.Dispose()
            PropietarioBodega = Nothing
        End If
        If Picking IsNot Nothing Then
            Picking.Dispose()
            Picking = Nothing
        End If
    End Sub
#End Region

End Class
