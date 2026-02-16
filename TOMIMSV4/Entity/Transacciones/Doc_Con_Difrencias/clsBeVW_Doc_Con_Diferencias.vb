Public Class clsBeVW_Doc_Con_Diferencias

    Implements ICloneable

    Public Property ORDENCOMPRA() As String = ""
    Public Property CODIGO_PRODUCTO() As String = ""
    Public Property NOMBRE_PRODUCTO() As String = ""
    Public Property CANTIDAD() As Double = 0.0
    Public Property CANTIDAD_RECIBIDA() As Double = 0.0
    Public Property PRESENTACION() As String = ""
    Public Property DIFERENCIA() As Double = 0.0
    Public Property IDPROPIETARIOBODEGA() As Integer = 0
    Public Property BODEGA() As String = ""
    Public Property PROPIETARIO() As String = ""

    Public Property POLIZA() As String = ""
    Public Property IDPROVEEDORBODEGA() As Integer = 0
    Public Property IDTIPOINGRESOOC() As Integer = 0
    Public Property NOMBRE_INGRESOOC() As String = ""
    Public Property IDPRODUCTOBODEGA() As Integer = 0
    Public Property IDPRESENTACION() As Integer = 0
    Public Property IDUNIDADMEDIDABASICA() As Integer = 0

    Public Property UMBAS() As String = ""
    Public Property ESTADO() As String = ""
    Public Property ACTIVO() As Boolean = False

    Public Property FECHA_CREACION() As Date

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function



End Class
