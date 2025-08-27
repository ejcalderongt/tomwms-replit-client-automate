Public Class clsBePrintSettings
    Public Property request_type As String = "put"
    Public Property path As String = "/system/print_settings"
    Public Property id As Integer = 0
    Public Property name As String = "print"
    Public Property description As String = ""
    Public Property photocell_type As String = "printhead"
    Public Property photocell As String = "P1"
    Public Property encoder_type As String = "software"
    Public Property encoder_speed As Integer = 60
    Public Property encoder_resolution As Integer = 600
    Public Property photocell_sense As String = "edge"
    Public Property history_limit As Integer = 10
    Public Property cache_limit As Integer = 10
    Public Property print_dialog As Integer = 1
    Public Property realtime_printing As Boolean = True
    Public Property roundtrip_printing As Boolean = False
End Class
