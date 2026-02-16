Partial Public Class clsBeLicencia_llave
    Implements ICloneable

    Public Property CantBackOffice() As Integer = 0
    Public Property CantHandHeld() As Integer = 0
    Public Property Vence() As Date = Now.AddDays(-1)
    Public Property MacServer As String = ""

    '#EJC20171108_REF03_0637PM: ReadOnlyProperty Activa
    Public ReadOnly Property Activa As Boolean
        Get
            Return (Vence >= Today)
        End Get
    End property


End Class