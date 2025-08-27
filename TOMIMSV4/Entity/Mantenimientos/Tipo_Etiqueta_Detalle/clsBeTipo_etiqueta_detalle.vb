Public Class clsBeTipo_etiqueta_detalle
    Implements ICloneable

    Public Property IdTipoEtiquetaDetalle() As Integer = 0
    Public Property IdTipoEtiqueta() As Integer = 0
    Public Property Orden() As Integer = 0
    Public Property Nombre() As String = ""
    Public Property Campo() As String = ""
    Public Property Coor_x As Double = 0
    Public Property Coor_y As Double = 0
    Public Property Width As Double = 0
    Public Property Height As Double = 0

    Sub New()
        '#EJC: Add your constructor here... 
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return MyBase.MemberwiseClone()
    End Function

End Class
