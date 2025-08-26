Public Class clsBeTrans_pe_docu_ref_det
    Implements ICloneable

    Public Property IdDocumentoRef() As Integer = 0
    Public Property IdDocumentoRefDet() As Integer = 0
    Public Property Codigo_producto() As String = ""
    Public Property Nombre_producto() As String = ""
    Public Property Cantidad() As Double = 0.0
    Public Property Peso() As Double = 0.0
    Public Property Umbas() As String = ""
    Public Property Presentaacion() As String = ""

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
