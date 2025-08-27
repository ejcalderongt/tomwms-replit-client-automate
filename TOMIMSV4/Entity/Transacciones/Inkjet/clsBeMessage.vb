Public Class clsBeMessage
    Public Property request_type As String = "post"
    Public Property path As String = "/data/data"
    Public Property name As String = "Msg"
    Public Property hash As Long = Now.Ticks
    Public Property attribute As New PrintAttribute
    Public Property object_list As New List(Of PrintObject)
End Class

Public Class PrintAttribute
    Public Property printdata_pref As New PrintDataPref
End Class

Public Class PrintDataPref
    Public Property print_prefs As New List(Of PrintPrefs)
End Class

Public Class PrintPrefs
    Public Property ff_margin As Double
    Public Property fr_margin As Double
    Public Property bf_margin As Double
    Public Property br_margin As Double
End Class

Public Class PrintObject
    Public Property type As String = "text"
    Public Property id As Integer
End Class