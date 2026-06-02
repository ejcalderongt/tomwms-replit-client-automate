Public Class clsBeTrans_inv_stock
    Implements ICloneable

    Public Property Idinventario() As Integer = 0
    Public Property IdStock() As Integer = 0
    Public Property IdPropietarioBodega() As Integer = 0
    Public Property IdProductoBodega() As Integer = 0
    Public Property IdProductoEstado() As Integer = 0
    Public Property IdPresentacion() As Integer = 0
    Public Property IdUnidadMedida() As Integer = 0
    Public Property IdUbicacion() As Integer = 0
    Public Property IdUbicacion_anterior() As Integer = 0
    Public Property IdRecepcionEnc() As Integer = 0
    Public Property IdRecepcionDet() As Integer = 0
    Public Property IdPedidoEnc() As Integer = 0
    Public Property IdPickingEnc() As Integer = 0
    Public Property IdDespachoEnc() As Integer = 0
    Public Property Lote() As String = ""
    Public Property Lic_plate() As String = ""
    Public Property Serial() As String = ""
    Public Property Cantidad() As Double = 0.0
    Public Property Fecha_ingreso() As Date = Date.Now
    Public Property Fecha_vence() As Date = New Date(1901, 1, 1)
    Public Property Uds_lic_plate() As Double = 0.0
    Public Property No_bulto() As String = ""
    Public Property Fecha_manufactura() As Date = Date.Now
    Public Property Añada() As Integer = 0
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Activo() As Boolean = False
    Public Property Peso() As Double = 0.0
    Public Property Temperatura() As Double = 0.0
    Public Property fecha_copia() As Date = Date.Now
    Public Property IdBodega As Integer = 0
    Public Property IdProductoTallaColor As Integer = 0
    Public Property Cantidad_Reservada_UMBas As Double = 0

    Sub New()
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
