Public Class clsBePrintData
    Public Property request_type As String = "post"
    Public Property path As String = "/engine/printjob"
    Public Property hash As Long = Now.Ticks
    Public Property attribute As PrintAttributeMsg
End Class

Public Class PrintAttributeMsg
    Public Property print_data_name As String
End Class
