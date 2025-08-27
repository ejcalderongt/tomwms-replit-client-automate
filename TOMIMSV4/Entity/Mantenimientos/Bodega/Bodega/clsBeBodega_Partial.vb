Partial Public Class clsBeBodega
    Implements IDisposable
    Public Property Empresa As New clsBeEmpresa
    Public Property Areas As New List(Of clsBeBodega_area)
    Public Property Sectores As New List(Of clsBeBodega_sector)
    Public Property Tramos As New List(Of clsBeBodega_tramo)
    Public Property Ubicaciones As New List(Of clsBeBodega_ubicacion)
    Public Property Muelles As New List(Of clsBeBodega_muelles)

End Class
