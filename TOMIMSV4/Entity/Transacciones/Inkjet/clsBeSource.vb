Public Class clsBeSource
    Public Property request_type As String = "post"
    Public Property path As String = "/data/source"
    Public Property type As String = "text"
    Public Property name As String
    Public Property attribute As AttributeContent
End Class

Public Class AttributeContent
    Public Property content As String = "Default"
End Class