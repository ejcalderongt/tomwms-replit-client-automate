
Public Class clsBeTrans_re_det
    Implements ICloneable
    Implements IDisposable

    Public Property IdPresentacion() As Integer
    Public Property IdUnidadMedida() As Integer
    Public Property IdProductoEstado() As Integer
    Public Property IdMotivoDevolucion() As Integer
    Public Property IdRecepcionDet() As Integer = 0
    Public Property IdRecepcionEnc() As Integer = 0
    Public Property IdProductoBodega As Integer = 0
    Public Property IdOperadorBodega() As Integer = 0
    Public Property No_Linea() As Integer = 0
    Public Property cantidad_recibida() As Double = 0.0
    Public Property Nombre_producto() As String = ""
    Public Property Nombre_presentacion() As String = ""
    Public Property Nombre_unidad_medida() As String = ""
    Public Property Nombre_producto_estado() As String = ""
    Public Property Lote() As String = ""
    Public Property Fecha_vence() As Date = New Date(1900, 1, 1)
    Public Property Fecha_ingreso() As Date = Date.Now
    Public Property Peso() As Double = 0.0
    Public Property Peso_Estadistico() As Double = 0.0
    Public Property Peso_Minimo() As Double = 0.0
    Public Property Peso_Maximo() As Double = 0.0
    Public Property peso_unitario() As Double = 0
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property Observacion() As String = ""
    Public Property Aniada As Integer = 0
    Public Property Costo As Double = 0.0
    Public Property Costo_Oc As Double = 0.0
    Public Property Costo_Estadistico As Double = 0.0
    Public Property Atributo_Variante_1 As String = ""
    Public Property Codigo_Producto As String = ""
    Public Property Pallet_No_Estandar As Boolean = False
    Public Property Posiciones As Integer = 0

    Public IdProductoTallaColor As Integer = 0
    Sub New()
    End Sub

    Sub New(ByRef IdRecepcionDet As Integer, ByVal IdRecepcionEnc As Integer, ByVal IdOperador As Integer, ByVal No_Linea As Integer, ByVal cantidad_recibida As Double, ByVal nombre_producto As String,
            ByVal nombre_presentacion As String, ByVal nombre_unidad_medida As String, ByVal nombre_producto_estado As String, ByVal lote As String, ByVal fecha_vence As Date, ByVal fecha_ingreso As Date,
            ByVal peso As Double, ByVal user_agr As String, ByVal fec_agr As Date, ByVal observacion As String, ByVal pallet_no_estandar As Boolean)
        Me.IdRecepcionDet = IdRecepcionDet
        Me.IdRecepcionEnc = IdRecepcionEnc
        Me.IdOperadorBodega = IdOperador
        Me.No_Linea = No_Linea
        Me.cantidad_recibida = cantidad_recibida
        Me.Nombre_producto = nombre_producto
        Me.Nombre_presentacion = nombre_presentacion
        Me.Nombre_unidad_medida = nombre_unidad_medida
        Me.Nombre_producto_estado = nombre_producto_estado
        Me.Lote = lote
        Me.Fecha_vence = fecha_vence
        Me.Fecha_ingreso = fecha_ingreso
        Me.Peso = peso
        Me.User_agr = user_agr
        Me.Fec_agr = fec_agr
        Me.Observacion = observacion
        Me.Pallet_No_Estandar = pallet_no_estandar
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class