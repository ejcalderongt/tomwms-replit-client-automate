Imports Newtonsoft.Json

Public Class clsBeObject
    Public Property request_type As String = "post"
    Public Property path As String = "/data/object"
    Public Property type As String = "text"
    Public Property name As String = "Object"
    Public Property style As New Style()
    Public Property hash As Long = Now.Ticks
    Public Property attribute As New Dictionary(Of String, Object)()
    Public Property source_list As New List(Of SourceItem)()
End Class

Public Class Style
    Public Property x As Integer = 0.0
    Public Property y As Integer = 65
    Public Property w As Integer = 228
    Public Property h As Integer = 228
    Public Property pivot_x As Double = 0.0
    Public Property pivot_y As Double = 0.0
    Public Property rotate As Double = 0.0
    Public Property scale_x As Double = 1.0
    Public Property scale_y As Double = 1.0
    Public Property paint_style As String = "fill"
    Public Property line_cap As String = "butt"
    Public Property line_join As String = "miter"
    Public Property line_width As Integer = 0
    Public Property miter_limit As Integer = 4
    Public Property font_style As String = "ttf-default-r*nnn*-40-40-UTF-8"
    Public Property visiblity As String = "visible"
    Public Property letter_spacing As Double = 0.0

    'Propiedades para BarCode
    <JsonProperty(NullValueHandling:=NullValueHandling.Ignore)>
    Public Property text_margin As Double? = Nothing

    <JsonProperty(NullValueHandling:=NullValueHandling.Ignore)>
    Public Property x_dimension As Double? = Nothing

    <JsonProperty(NullValueHandling:=NullValueHandling.Ignore)>
    Public Property bar_height As Double? = Nothing

    <JsonProperty(NullValueHandling:=NullValueHandling.Ignore)>
    Public Property quiet_zone As Integer? = Nothing

    <JsonProperty(NullValueHandling:=NullValueHandling.Ignore)>
    Public Property bearer_bar_thickness As Integer? = Nothing

    <JsonProperty(NullValueHandling:=NullValueHandling.Ignore)>
    Public Property escape_seq As Boolean? = Nothing

    <JsonProperty(NullValueHandling:=NullValueHandling.Ignore)>
    Public Property gs1_nocheck As Boolean? = Nothing

    <JsonProperty(NullValueHandling:=NullValueHandling.Ignore)>
    Public Property dot As Boolean? = Nothing

    <JsonProperty(NullValueHandling:=NullValueHandling.Ignore)>
    Public Property format As String = Nothing

    <JsonProperty(NullValueHandling:=NullValueHandling.Ignore)>
    Public Property data_type As String = Nothing

    <JsonProperty(NullValueHandling:=NullValueHandling.Ignore)>
    Public Property bearer_bar_type As String = Nothing

    <JsonProperty(NullValueHandling:=NullValueHandling.Ignore)>
    Public Property human_readable As String = Nothing
End Class

Public Class SourceItem
    Public Property type As String = "text"
    Public Property id As Integer = 0
End Class