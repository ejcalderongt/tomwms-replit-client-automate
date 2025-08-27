Public Class clsBeAjustesMI3

    Public Property NoDocumento As String
    Public Property Codigo_Producto As String
    ''' <summary>
    ''' Código de bodega de WMS.
    ''' </summary>
    ''' <returns></returns>
    Public Property Codigo_Bodega As String
    ''' <summary>
    ''' Código de bodega de ERP (Proviene de cliente)
    ''' </summary>
    ''' <returns></returns>
    Public Property Codigo_Bodega_ERP As String
    Public Property UMBas As String
    Public Property Cantidad As Double
    Public Property TipoAjusteERP As String
    Public Property TipoAjusteWMS As String
    Public Property Lote As String
    Public Property Motivo_Ajuste As String
    Public Property Observacion As String
    Public Property Seccion As String
    Public Property IdAjusteEnc As Integer = 0
    Public Property IdAjusteDet As Integer = 0
    Public Property IdCentroCosto As Integer = 0
    Public Property Codigo_Centro_Costo As String = ""

End Class
