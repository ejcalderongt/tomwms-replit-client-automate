Module mGlobal

    Public Ins As New clsInsert
    Public Upd As New clsUpdate

    Public Enum DataType
        Fecha
        FechaHora
        Texto
        Numero
        Parametro  ' Valor por defecto, representa un parámetro de función o SQL
    End Enum

End Module
