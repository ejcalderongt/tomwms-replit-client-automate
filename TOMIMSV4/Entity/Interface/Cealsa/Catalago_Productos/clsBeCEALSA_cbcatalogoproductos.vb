Public Class clsBeCEALSA_cbcatalogoproductos
    Implements ICloneable

    Public Property Cod_empresa() As Integer = 0
    Public Property Codigoproducto() As String = ""
    Public Property Descripcion() As String = ""
    Public Property Nemonico() As String = ""
    Public Property Codigo_rubro() As Integer = 0
    Public Property Movimiento() As Boolean = False
    Public Property Estado() As String = ""
    Public Property Cod_centro() As String = ""
    Public Property Cod_cuentaxcobrar() As String = ""
    Public Property Cod_cuentaproducto() As String = ""
    Public Property Usuario() As String = ""
    Public Property Fechamov() As Date = Date.Now
    Public Property Control() As Integer = 0
    Public Property Correlativo() As Integer = 0
    Public Property Cod_cuentapasivodiferido() As String = ""
    Public Property Cod_cuenta_dif_cxc() As String = ""
    Public Property Cod_cuenta_dif_pasdif() As String = ""
    Public Property Cod_cuentaxcobrar_me() As String = ""
    Public Property Cod_cuentapasivodiferido_me() As String = ""
    Public Property Corre_cbcesantes() As Integer = 0
    Public Property Montominimo() As Double = 0.0

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
