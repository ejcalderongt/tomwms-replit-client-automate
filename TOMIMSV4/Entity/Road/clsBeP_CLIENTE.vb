Public Class clsBeP_CLIENTE
    Implements ICloneable

    Public Property CODIGO() As String = ""
    Public Property NOMBRE() As String = ""
    Public Property BLOQUEADO() As String = ""
    Public Property TIPONEG() As String = ""
    Public Property TIPO() As String = ""
    Public Property SUBTIPO() As String = ""
    Public Property CANAL() As String = ""
    Public Property SUBCANAL() As String = ""
    Public Property NIVELPRECIO() As Integer = 0
    Public Property MEDIAPAGO() As String = ""
    Public Property LIMITECREDITO() As Double = 0.0
    Public Property DIACREDITO() As Integer = 0
    Public Property DESCUENTO() As String = ""
    Public Property BONIFICACION() As String = ""
    Public Property ULTVISITA() As Date = Date.Now
    Public Property IMPSPEC() As Double = 0.0
    Public Property INVTIPO() As String = ""
    Public Property INVEQUIPO() As String = ""
    Public Property INV1() As String = ""
    Public Property INV2() As String = ""
    Public Property INV3() As String = ""
    Public Property NIT() As String = ""
    Public Property MENSAJE() As String = ""
    Public Property EMAIL() As String = ""
    Public Property ESERVICE() As String = ""
    Public Property TELEFONO() As String = ""
    Public Property DIRTIPO() As String = ""
    Public Property DIRECCION() As String = ""
    Public Property REGION() As String = ""
    Public Property SUCURSAL() As String = ""
    Public Property MUNICIPIO() As String = ""
    Public Property CIUDAD() As String = ""
    Public Property ZONA() As Integer = 0
    Public Property COLONIA() As String = ""
    Public Property AVENIDA() As String = ""
    Public Property CALLE() As String = ""
    Public Property NUMERO() As String = ""
    Public Property CARTOGRAFICO() As String = ""
    Public Property COORX() As Double = 0.0
    Public Property COORY() As Double = 0.0
    Public Property BODEGA() As String = ""
    Public Property COD_PAIS() As String = ""
    Public Property FIRMADIG() As String = ""
    Public Property CODBARRA() As String = ""
    Public Property VALIDACREDITO() As String = ""
    Public Property FACT_VS_FACT() As String = ""
    Public Property CHEQUEPOST() As String = ""
    Public Property PRECIO_ESTRATEGICO() As String = ""
    Public Property NOMBRE_PROPIETARIO() As String = ""
    Public Property NOMBRE_REPRESENTANTE() As String = ""
    Public Property PERCEPCION() As Double = 0.0
    Public Property TIPO_CONTRIBUYENTE() As String = ""
    Public Property ID_DESPACHO() As Integer = 0
    Public Property ID_FACTURACION() As Integer = 0
    Public Property MODIF_PRECIO() As Boolean = False

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
