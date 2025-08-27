Public Class clsBeTrans_ajuste_det_rep
    Public Property Codigo() As String
    Public Property Producto() As String
    Public Property Ubicacion() As String
    Public Property Motivo() As String
    Public Property Tipo() As String
    Public Property Existencia() As String '#CKFK 20180604 Modifiqué el nombre del campo Valor_Orig por Existencia
    Public Property Cantidad() As String '#CKFK 20180604 Modifiqué el nombre del campo Valor_Nuevo por Cantidad
    Public Property Observacion() As String
    Public Property Lote() As String '#CKFK 20180604 Agregué la property Lote
    Public Property LP() As String '#CKFK 20220208 Agregué la property LP
End Class
