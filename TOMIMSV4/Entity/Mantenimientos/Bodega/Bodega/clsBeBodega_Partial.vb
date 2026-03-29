Partial Public Class clsBeBodega
    Implements IDisposable
    Public Property Empresa As New clsBeEmpresa
    Public Property Areas As New List(Of clsBeBodega_area)
    Public Property Sectores As New List(Of clsBeBodega_sector)
    Public Property Tramos As New List(Of clsBeBodega_tramo)
    Public Property Ubicaciones As New List(Of clsBeBodega_ubicacion)
    Public Property Muelles As New List(Of clsBeBodega_muelles)

    Public Property cambio_ubicacion_restrictivo As Boolean = False
    Public Property permitir_cambio_ubic_indice_menor As Boolean = False

    Public Property requerir_mismo_producto_posiciones As Boolean = False


End Class
