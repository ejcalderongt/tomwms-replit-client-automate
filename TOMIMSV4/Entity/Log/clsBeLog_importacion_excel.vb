Public Class clsBeLog_importacion_excel
    Implements ICloneable

    Public Property IdImportacion() As Integer = 0
    Public Property IdEmpresa() As Integer = 0
    Public Property IdBodega() As Integer = 0
    Public Property IdUsuario() As Integer = 0
    Public Property Hash_archivo() As String = ""
    Public Property Fecha() As Date = Date.Now

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
