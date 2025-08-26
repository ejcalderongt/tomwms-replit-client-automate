Public Class clsBeP_PRODUCTO
    Implements ICloneable

    Public Property CODIGO() As String = ""
    Public Property TIPO() As String = ""
    Public Property LINEA() As String = ""
    Public Property SUBLINEA() As String = ""
    Public Property EMPRESA() As String = ""
    Public Property MARCA() As String = ""
    Public Property CODBARRA() As String = ""
    Public Property DESCCORTA() As String = ""
    Public Property DESCLARGA() As String = ""
    Public Property COSTO() As Double = 0.0
    Public Property FACTORCONV() As Double = 0.0
    Public Property UNIDBAS() As String = ""
    Public Property UNIDMED() As String = ""
    Public Property UNIMEDFACT() As Double = 0.0
    Public Property UNIGRA() As String = ""
    Public Property UNIGRAFACT() As Double = 0.0
    Public Property DESCUENTO() As String = ""
    Public Property BONIFICACION() As String = ""
    Public Property IMP1() As Double = 0.0
    Public Property IMP2() As Double = 0.0
    Public Property IMP3() As Double = 0.0
    Public Property VENCOMP() As String = ""
    Public Property DEVOL() As String = ""
    Public Property OFRECER() As String = ""
    Public Property RENTAB() As String = ""
    Public Property DESCMAX() As String = ""
    Public Property IVA() As String = ""
    Public Property CODBARRA2() As String = ""
    Public Property CBCONV() As Integer = 0
    Public Property BODEGA() As String = ""
    Public Property SUBBODEGA() As String = ""
    Public Property PESO_PROMEDIO() As Double = 0.0
    Public Property MODIF_PRECIO() As Boolean = False
    Public Property IMAGEN() As String = ""
    Public Property VIDEO() As String = ""
    Public Property VENTA_POR_PESO() As Boolean = False
    Public Property ES_PROD_BARRA() As Boolean = False
    Public Property UNID_INV() As String = ""
    Public Property VENTA_POR_PAQUETE() As Boolean = False
    Public Property VENTA_POR_FACTOR_CONV() As Boolean = False
    Public Property ES_SERIALIZADO() As Boolean = False
    Public Property PARAM_CADUCIDAD() As Integer = 0
    Public Property PRODUCTO_PADRE() As String = ""
    Public Property FACTOR_PADRE() As Double = 0.0
    Public Property TIENE_INV() As Boolean = False
    Public Property TIENE_VINETA_O_TUBO() As Boolean = False
    Public Property PRECIO_VINETA_O_TUBO() As Double = 0.0
    Public Property ES_VENDIBLE() As Boolean = False
    Public Property UNIGRASAP() As Double = 0.0
    Public Property UM_SALIDA() As String = ""

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
