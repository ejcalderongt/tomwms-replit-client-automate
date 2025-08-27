Public Class clsBeTrans_picking_ubic
    Implements ICloneable

    Public Property IdPickingEnc As Integer = 0
    Public Property IdPickingUbic() As Integer = 0
    Public Property IdPickingDet() As Integer = 0
    Public Property IdUbicacion() As Integer = 0
    Public Property Lote() As String = ""
    Public Property Fecha_Vence() As Date = New Date(1900, 1, 1)
    Public Property Fecha_minima() As Date = New Date(1900, 1, 1)
    Public Property Serial() As String = ""
    Public Property Lic_plate() As String = ""
    Public Property Acepto() As Boolean = False
    Public Property Peso_solicitado() As Double = 0.0
    Public Property Peso_recibido() As Double = 0.0
    Public Property Peso_verificado() As Double = 0.0
    Public Property Peso_despachado() As Double = 0.0
    Public Property Cantidad_Solicitada() As Double = 0.0
    Public Property Cantidad_Recibida() As Double = 0.0
    Public Property Cantidad_Verificada() As Double = 0.0
    Public Property Encontrado() As Boolean = False
    Public Property Dañado_verificacion() As Boolean
    Public Property Fecha_real_vence() As Date = New Date(1900, 1, 1)
    Public Property Fecha_picking() As Date = New Date(1900, 1, 1)
    Public Property Fecha_verificado() As Date = New Date(1900, 1, 1)
    Public Property Fecha_despachado() As Date = New Date(1900, 1, 1)
    Public Property Cantidad_despachada() As Double = 0.0
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Activo() As Boolean = False
    Public Property Dañado_picking() As Boolean
    Public Property IdProducto As Integer = 0
    Public Property IdOperadorBodega_Pickeo As Integer = 0
    Public Property IdOperadorBodega_Verifico As Integer = 0
    Public Property No_packing() As String = ""
    Public Property Fecha_packing() As Date = New Date(1900, 1, 1)
    Public Property No_encontrado() As Boolean = False

    ''' <summary>
    ''' #EJC20220303: Almacenar el operador el que le fue asignada la línea de forma auotmática.
    ''' </summary>
    ''' <returns></returns>
    Public Property IdOperadorBodega_Asignado As Integer = 0
    Public Property IdProductoTallaColor As Integer = 0
    Public Function Clone() As Object Implements ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class