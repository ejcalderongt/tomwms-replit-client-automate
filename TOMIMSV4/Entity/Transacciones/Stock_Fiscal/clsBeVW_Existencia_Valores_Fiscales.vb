Public Class clsBeVW_Existencia_Valores_Fiscales
    Implements ICloneable

    Public Property IdRecepcionEnc() As Integer = 0
    Public Property Propietario() As String = ""
    Public Property Proveedor() As String = ""
    Public Property Bodega() As String = ""
    Public Property IdOrdenCompraEnc() As Integer = 0
    Public Property No_DocumentoOC() As String = ""
    Public Property No_DocumentoRec() As String = ""
    Public Property ReferenciaOC() As String = ""
    Public Property Fecha() As Date = Date.Now
    Public Property Estado() As String = ""
    Public Property TipoTrans() As String = ""
    Public Property Descripcion() As String = ""
    Public Property Muelle() As String = ""
    Public Property Activo() As Boolean = False
    Public Property Fecha_Agrego() As Date = Date.Now
    Public Property CodigoProd() As String = ""
    Public Property BarraProd() As String = ""
    Public Property NombreProd() As String = ""
    Public Property Recibido() As Double = 0.0
    Public Property Existencia_Actual_UMBas() As Double = 0.0
    Public Property Existencia_Actual_Pres() As Double = 0.0
    Public Property UM() As String = ""
    Public Property EstadoProd() As String = ""
    Public Property PresProd() As String = ""
    Public Property Lic_plate() As String = ""
    Public Property Factor() As Double = 0.0
    Public Property Lote() As String = ""
    Public Property Vence() As Date = Date.Now
    Public Property IdStock() As Integer = 0
    Public Property Ubicacion_Origen() As String = ""
    Public Property NoPoliza() As String = ""
    Public Property Valor_aduana() As Double = 0.0
    Public Property Valor_fob() As Double = 0.0
    Public Property Valor_iva() As Double = 0.0
    Public Property Valor_dai() As Double = 0.0
    Public Property Valor_seguro() As Double = 0.0
    Public Property Valor_flete() As Double = 0.0
    Public Property Peso_neto() As Double = 0.0

    Public Property codigo_poliza() As String = ""

    Public Property numero_orden() As String = ""

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
