Partial Public Class clsBeTrans_re_enc
    Implements IDisposable

    Public Property OrdenCompraRec As New clsBeTrans_re_oc
    Public Property Detalle As New List(Of clsBeTrans_re_det)
    Public Property DetalleParametros As New List(Of clsBeTrans_re_det_parametros)
    Public Property DetalleOperadores As New List(Of clsBeTrans_re_op)
    Public Property DetalleImagenes As New List(Of clsBeTrans_re_img)
    Public Property DetalleFacturas As New List(Of clsBeTrans_re_fact)
    Public Property IsNew() As Boolean
    Public Property Descripcion As String
    Public Property UbicacionRecepcion As String
    Public Property NombrePropietario As String
    Public Property Bodega As String
    Public Property Usuario As String
    Public Property PropietarioBodega As New clsBePropietario_bodega
    Public Property PropietarioOC As String
    Public Property Proveedor As String
    Public Property NoOrdencompra As Integer
    Public Property NoDocumentoOC As String
    'Public Property TipoTrans As String
    Public Property Muelle As clsBeBodega_muelles
    Public Property MuelleRec As String = ""
    Public Property NOFactura As String
    Public Property TareaHH As New clsBeTarea_hh

    Public Enum pTipoTrans

        HCOC00 = 0
        HCOD00 = 1
        HHSR00 = 2
        HSOC00 = 3
        HSOD00 = 4
        MCOC00 = 5
        MCOD00 = 6
        MSOC00 = 7
        MSOD00 = 8
        PICH000 = 9

    End Enum

End Class
