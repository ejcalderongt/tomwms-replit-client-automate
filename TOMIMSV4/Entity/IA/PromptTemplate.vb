Public Class PromptTemplate

    Public Property id As String            ' "wms.receiving.v1"
    Public Property name As String          ' "Recepción / ASN"
    Public Property version As String       ' "1.0.0"
    Public Property description As String   ' Visible en UI
    Public Property language As String      ' "es"
    Public Property variables As List(Of TemplateVar)
    Public Property content As String       ' Texto del system prompt con {{tokens}}
    Public Property tags As String()        ' {"WMS","receiving"}

End Class
