Public Class clsBeTrans_oc_docu_ref
    Implements ICloneable

    Public Property IdDocumentoRef() As Integer = 0
    Public Property Codigo() As String = ""
    Public Property Nombre() As String = ""
    Public Property Descripcion() As String = ""
    Public Property FechaDocumento() As Date = Date.Now
    Public Property FechaAsignacion() As Date = Date.Now
    Public Property FechaAgregado() As Date = Date.Now
    Public Property Asignado() As Boolean = False
    Public Property Activo() As Boolean = False

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
