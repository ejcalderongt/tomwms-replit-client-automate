Partial Public Class clsBeTrans_picking_enc
    Implements IDisposable

    Public Property IsNew() As Boolean
    Public Property NombreUbicacionPicking As String
    Public Property UbicacionPicking As New clsBeBodega_ubicacion
    Public Property ListaPickingDet As New List(Of clsBeTrans_picking_det)
    Public Property ListaPickingUbic As New List(Of clsBeTrans_picking_ubic)
    Public Property NombreBodega As String
    Public Property NombrePropietarioPicking As String
    Public Property IdPedidoEnc As Integer
    Public Property Tiene_Manufactura As Boolean
    Public Property NombrePrioridad() As String = ""
    Public Property NombreMuelle As String = ""
    Public Property IdTipoPicking As Integer

    '#GT29042025: campos para determinar el muelle adonde se debe llevar el picking.
    Public Property IdUbicacionMuelle As Integer = 0
    Public Property Codigo_Barra_Muelle As String = ""
    Public Property Guia_Transporte As String = ""

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
