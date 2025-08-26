Public Class clsBeTrans_acuerdoscomerciales_enc
    Implements ICloneable

    Public Property IdAcuerdoEnc() As Integer = 0
    Public Property IdCliente() As Integer = 0
    Public Property Codigo_acuerdo() As Integer = 0
    Public Property Descripcion() As String = ""
    Public Property Tipo_cobro() As String = ""
    Public Property Cod_moneda() As Integer = 0
    Public Property Moneda() As String = ""
    Public Property Estado() As Boolean = False
    Public Property user_agr As String = ""
    Public Property fec_agr As DateTime = Now
    Public Property user_mod As String = ""
    Public Property fec_mod As DateTime = Now
    Public Property fec_erp As DateTime = Now
    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
