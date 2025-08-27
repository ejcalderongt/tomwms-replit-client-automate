Public Class clsDalEx

    Public Shared Property ErrorS0001 As String = _
        "No se pudo obtener el registro de stock asociado a la transacción #ERR_S0001"

    Public Shared Property ErrorS0002 As String =
        "La cantidad solicitada es mayor que la existencia disponible UMBAS"

    Public Shared Property ErrorS0003 As String = _
       "El peso disponible es menor que el peso solicitado "

    Public Shared Property ErrorS0004 As String =
    "No se obtuvo ningun registro de stock con los parámetros proporcionados"

    Public Shared Property ErrorS0005 As String =
   "No se pudo reservar el stock, la lista no tiene registros"

    Public Shared Property ErrorS0006 As String =
   "No se pudo insertar el stock en la tabla de reserva stock_res"

    Public Shared Property ErrorS0002A As String =
        "La cantidad solicitada es mayor que la existencia disponible en presentación "

End Class
