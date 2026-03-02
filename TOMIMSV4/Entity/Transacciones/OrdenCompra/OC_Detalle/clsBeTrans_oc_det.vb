Public Class clsBeTrans_oc_det
    Implements ICloneable
    Implements IDisposable

    Public Property IdOrdenCompraEnc() As Integer = 0
    Public Property IdOrdenCompraDet() As Integer = 0
    Public Property IdProductoBodega() As Integer = 0
    Public Property IdArancel() As Integer = 0
    Public Property IdPresentacion() As Integer = 0
    Public Property IdUnidadMedidaBasica() As Integer = 0
    Public Property IdMotivoDevolucion() As Integer = 0
    Public Property No_Linea() As Integer = 0
    Public Property Nombre_producto() As String = ""
    Public Property Nombre_presentacion() As String = ""
    Public Property Nombre_arancel() As String = ""
    Public Property Porcentaje_arancel() As Double = 0.0
    Public Property Nombre_unidad_medida_basica() As String = ""
    Public Property Cantidad() As Double = 0.0
    Public Property Peso As Double = 0.0
    Public Property Peso_Recibido As Double = 0.0
    Public Property Cantidad_recibida() As Double = 0.0
    ''' <summary>
    ''' Costo unitario
    ''' </summary>
    ''' <returns></returns>
    Public Property Costo() As Double = 0.0
    Public Property Total_linea() As Double = 0.0
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Activo() As Boolean = False
    Public Property Atributo_variante_1() As String = ""
    Public Property Codigo_Producto As String = ""

    '#EJC20201228: Agregué campos necesarios poliza.
    Public Property valor_aduana As Double = 0
    Public Property valor_fob As Double = 0
    Public Property valor_iva As Double = 0
    Public Property valor_dai As Double = 0
    Public Property valor_seguro As Double = 0
    Public Property valor_flete As Double = 0
    Public Property Peso_Neto As Double = 0
    Public Property Peso_Bruto As Double = 0

    '#EJC20210313: Agregado para consolidadores de CEALSA
    Public Property IdPropietarioBodega As Integer = 0

    '#EJC20210322: Agregué porque creo que se utilizará para la gestión de documentos en tránsito...
    Public Property IdPedidoCompraDet As Integer = 0

    '#EJC20210403:Guarda el IdDet del detalle de la OC que pertenece al producto que es de tipo Kit o padre.
    Public Property IdOrdenCompraDetPadre As Integer = 0

    '#EJC20220224: Cealsa, guardar el shipper o embarcador 
    Public Property IdEmbarcador As Integer = 0
    Public Property Nombre_Embarcador As String = ""
    Public Property Nombre_Clasificacion As String = ""
    Public Property IdProductoTallaColor As Integer = 0

    Public Property Camas_Tarima As Integer = 0

    Public Property Cajas_Cama As Integer = 0

    Sub New()
    End Sub

    Sub New(ByRef IdOrdenCompraEnc As Integer, ByVal IdOrdenCompraDet As Integer, ByVal IdProductoBodega As Integer, ByVal IdArancel As Integer, ByVal IdPresentacion As Integer, ByVal IdUnidadMedidaBasica As Integer, ByVal IdMotivoDevolucion As Integer, ByVal No_Linea As Integer, ByVal nombre_producto As String, ByVal nombre_presentacion As String, ByVal nombre_arancel As String, ByVal porcentaje_arancel As Double, ByVal nombre_unidad_medida_basica As String, ByVal cantidad As Double, ByVal cantidad_recibida As Double, ByVal costo As Double, ByVal total_linea As Double, ByVal user_agr As String, ByVal fec_agr As Date, ByVal user_mod As String, ByVal fec_mod As Date, ByVal activo As Boolean)
        Me.IdOrdenCompraEnc = IdOrdenCompraEnc
        Me.IdOrdenCompraDet = IdOrdenCompraDet
        Me.IdProductoBodega = IdProductoBodega
        Me.IdArancel = IdArancel
        Me.IdPresentacion = IdPresentacion
        Me.IdUnidadMedidaBasica = IdUnidadMedidaBasica
        Me.IdMotivoDevolucion = IdMotivoDevolucion
        Me.No_Linea = No_Linea
        Me.Nombre_producto = nombre_producto
        Me.Nombre_presentacion = nombre_presentacion
        Me.Nombre_arancel = nombre_arancel
        Me.Porcentaje_arancel = porcentaje_arancel
        Me.Nombre_unidad_medida_basica = nombre_unidad_medida_basica
        Me.Cantidad = cantidad
        Me.Cantidad_recibida = cantidad_recibida
        Me.Costo = costo
        Me.Total_linea = total_linea
        Me.User_agr = user_agr
        Me.Fec_agr = fec_agr
        Me.User_mod = user_mod
        Me.Fec_mod = fec_mod
        Me.Activo = activo
    End Sub

    Public Function Clone() As Object Implements ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
