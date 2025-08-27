Public Class clsBeCEALSA_detacuerdoscomerciales
    Implements ICloneable

    Public Property Cod_empresa() As Integer = 0
    Public Property Codigoproducto() As String = ""
    Public Property Servicio() As String = ""
    Public Property Nemonico() As String = ""
    Public Property Codigo_cliente() As Integer = 0
    Public Property Corre_cbmaeacuerdosservicios() As Integer = 0
    Public Property Correlativo() As Integer = 0
    Public Property Descripcion() As String = ""
    Public Property Numero_unidades() As Double = 0.0
    Public Property Monto As Double = 0.0
    Public Property Porcentaje As Double = 0.0
    Public Property Dias_eventos() As Integer = 0
    'Public Property Numero_unidades_t() As Double = 0.0
    'Public Property Monto_t() As Double = 0.0
    'Public Property Porcentaje_t As Double = 0.0
    'Public Property Dias_t() As Integer = 0

    Public Property user_mod As Integer = 0
    Public Property fec_mod As Date = Now

    Public Property Corre_cbcatalogoproductos() As Integer = 0
    Public Property Procesado_wms As Boolean = False
    Public Property Estado As String = ""
    Public Property Prioridad As Integer = 0

    'Public Property EsAdaptado As Boolean = False

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
