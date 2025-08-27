Public Class clsBeTrans_oc_pol_motivo_correccion
    Implements ICloneable

    Public Property IdMotivoCorreccion() As Integer = 0
    Public Property IdEmpresa() As Integer = 0
    Public Property Empresa As New clsBeEmpresa
    Public Property Nombre() As String = ""
    Public Property Activo() As Boolean = False
    Public Property User_agr() As String = ""
    Public Property Fec_agr() As Date = Date.Now
    Public Property User_mod() As String = ""
    Public Property Fec_mod() As Date = Date.Now

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
