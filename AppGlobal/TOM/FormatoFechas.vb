Public Class FormatoFechas

    Public Shared Function fFecha(ByVal pFecha As Date, Optional ByVal Con_Hora As Boolean = False) As String
        Try
            fFecha = ""
            If IsDate(pFecha) Then
                If Con_Hora Then
                    Dim Hora As String = String.Format("{0}:{1}:{2}", pFecha.Hour, Right("00" & pFecha.Minute, 2), Right("00" & pFecha.Second, 2))
                    fFecha = String.Format("'{0}{1}{2} {3}'", CStr(pFecha.Year), Right("00" & pFecha.Month, 2), Right("00" & pFecha.Day, 2), Hora)
                Else
                    fFecha = String.Format("'{0}{1}{2}'", CStr(pFecha.Year), Right("00" & pFecha.Month, 2), Right("00" & pFecha.Day, 2))
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function tFecha(ByVal pFecha As Date) As String
        tFecha = ""
        Try
            If IsDate(pFecha) Then
                tFecha = pFecha.Year & Right("00" & pFecha.Month, 2) & Right("00" & pFecha.Day, 2)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function sFecha(ByVal pFecha As Date) As String
        Try
            sFecha = ""
            If IsDate(pFecha) Then
                sFecha = String.Format("{0}/{1}/{2}", Right(String.Format("00{0}", pFecha.Day), 2), Right("00" & pFecha.Month, 2), CStr(pFecha.Year))
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function sFechaM(ByVal pFecha As Date) As String
        Try
            sFechaM = ""
            If IsDate(pFecha) Then
                sFechaM = String.Format("{0}/{1}/{2}", Right(String.Format("00{0}", pFecha.Month), 2), Right("00" & pFecha.Day, 2), CStr(pFecha.Year))
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function sHora(ByVal pFecha As Date) As String
        Try
            sHora = ""
            If IsDate(pFecha) Then
                sHora = String.Format("{0}:{1}:{2}", Right("00" & pFecha.Hour, 2), Right("00" & pFecha.Minute, 2), CStr(pFecha.Second))
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function fFechaHora(ByVal pFecha As Date) As String
        fFechaHora = "'01-Jan-1900 00:00:00'"
        If IsDate(pFecha) Then
            fFechaHora = String.Format("'{0}{1}{2} ", CStr(pFecha.Year), Right("00" & pFecha.Month, 2), Right("00" & pFecha.Day, 2))
            fFechaHora = String.Format("{0}{1}:{2}:{3}'", fFechaHora, CStr(pFecha.Hour), CStr(pFecha.Minute), CStr(pFecha.Second))
        End If
    End Function

    Public Shared Function fFechaHora_Time(ByVal pFecha As DateTime) As String
        fFechaHora_Time = "'01-Jan-1900 00:00:00'"
        If IsDate(pFecha) Then
            fFechaHora_Time = String.Format("'{0}{1}{2} ", CStr(pFecha.Year), Right("00" & pFecha.Month, 2), Right("00" & pFecha.Day, 2))
            fFechaHora_Time = String.Format("{0}{1}:{2}:{3}'", fFechaHora_Time, CStr(pFecha.Hour), CStr(pFecha.Minute), CStr(pFecha.Second))
        End If
    End Function

    Public Shared Function sFecha_To_Date(ByVal pFecha As String) As Date
        Try
            ' Inicializa la fecha por defecto
            Dim fechaResultado As New Date(1900, 1, 1)

            If Not String.IsNullOrWhiteSpace(pFecha) AndAlso pFecha.Length >= 8 Then
                ' Parsea el año, mes y día como enteros
                Dim vAño As Integer = Integer.Parse(pFecha.Substring(0, 4).Trim())
                Dim vMes As Integer = Integer.Parse(pFecha.Substring(4, 2).Trim())
                Dim vDia As Integer = Integer.Parse(pFecha.Substring(6, 2).Trim())

                ' Crea la fecha
                fechaResultado = New Date(vAño, vMes, vDia)
            End If

            Return fechaResultado

        Catch ex As Exception
            ' Considera manejar la excepción de manera más específica o registrarla
            Throw
        End Try
    End Function


    Public Shared Function tFechaHora(ByVal pFecha As Date) As String
        tFechaHora = ""
        Try
            If IsDate(pFecha) Then
                tFechaHora = pFecha.Year & Right("00" & pFecha.Month, 2) & Right("00" & pFecha.Day, 2) & Right("00" & pFecha.Minute, 2) & Right("00" & pFecha.Hour, 2) & Right("00" & pFecha.Second, 2)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class