Public Class clsBeTrans_prefactura_erp_det

    Implements ICloneable

    Public Property corre_cbdetacuerdosservicios As Integer = 0
    Public Property codigoproducto As String = ""
    Public Property dias As Integer = 0
    Public Property monto As Double = 0.00

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
