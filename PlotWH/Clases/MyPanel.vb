Public Class MyPanel
    Inherits Panel

    Public Sub New()
        BorderStyle = BorderStyle.None
    End Sub

    Private bWidth As Integer = 2
    Public Property BorderWidth() As Integer
        Get
            Return bWidth
        End Get
        Set(ByVal value As Integer)
            bWidth = Math.Abs(value)
            Refresh()
        End Set
    End Property

    Private bColor As Color = Color.Red
    Public Property BorderColor() As Color
        Get
            Return bColor
        End Get
        Set(ByVal value As Color)
            bColor = value
            Refresh()
        End Set
    End Property

    Public Overridable Sub MyPanel_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles Me.Paint
        e.Graphics.DrawRectangle(New Pen(bColor, bWidth), ClientRectangle)
    End Sub

End Class
