Public Class clsBeTrans_servicio_enc
    Implements ICloneable

    Public Property IdServicioEnc() As Integer = 0
    Public Property IdOrdenCompraEnc() As Integer = 0
    Public Property IdEmpresa() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property No_poliza() As String = ""
    Public Property No_orden() As String = ""
    Public Property Fecha_doc_ingreso() As Date = Date.Now
    Public Property Fecha_servicio() As Date = Date.Now
    Public Property Enviado_a_erp() As Boolean = False
    Public Property Activo() As Boolean = False
    Public Property IdPropietario() As Integer = 0
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now
    Public Property Estado() As String = "Nuevo"
    Public Property IsNew As Boolean = True
    Public Property Es_Ingreso As Boolean = True
    Public Property IdPedidoEnc As Integer = 0


    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
