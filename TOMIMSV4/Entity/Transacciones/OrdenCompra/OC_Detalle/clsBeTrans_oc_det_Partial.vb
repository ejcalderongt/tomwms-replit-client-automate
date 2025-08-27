Partial Public Class clsBeTrans_oc_det
    Implements IDisposable

    Public Property IsNew() As Boolean
    Public Property ExisteEnRecepcion() As Boolean
    Public Property FactorPresentacion() As Decimal
    Public Property Arancel As clsBeArancel = New clsBeArancel()
    Public Property Producto As clsBeProducto = New clsBeProducto()
    Public Property Presentacion As clsBeProducto_Presentacion = New clsBeProducto_Presentacion()
    Public Property UnidadMedida As clsBeUnidad_medida = New clsBeUnidad_medida()
    Public Property RowIndex As Integer = 0
    Public Property lProductosHijosKit As New List(Of clsBeTrans_oc_det)
    Public Property lProductoComposicionKit As New List(Of clsBeProducto_kit_composicion)
    Public Property Nombre_Propietario As String = ""
    Public Property Talla As clsBeTalla = New clsBeTalla()
    Public Property Color As clsBeColor = New clsBeColor()

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
        If Arancel IsNot Nothing Then
            Arancel.Dispose()
            Arancel = Nothing
        End If
        If Producto IsNot Nothing Then
            Producto.Dispose()
            Producto = Nothing
        End If
    End Sub
#End Region

End Class
