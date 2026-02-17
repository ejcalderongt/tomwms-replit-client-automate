Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnLicencia_llave

    Public Shared Function Exist(ByVal pLlave As String)

        Exist = False

        Try

            Dim vSQL As String = "SELECT * FROM Licencia_llave" &
                   " Where(Llave = @Llave)"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.Add(New SqlParameter("@Llave", pLlave))

                    Dim lDT As New DataTable
                    lDTA.Fill(lDT)

                    Exist = lDT.Rows.Count > 0

                End Using

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Shared Sub Desencriptar_Licencia(ByRef pLicencia As clsBeLicencia_llave)

        Dim vParsingClaveDesencriptada() As String
        Dim vClaveDesencriptada As String
        Dim vFechaVence As Date
        Dim vNoLicenciasBOF, vNoLicenciasHH, vNoLicenciasUx, yy, mm, dd As Integer

        Try

            vClaveDesencriptada = clsPublic.DecodeString(pLicencia.Llave)

            If vClaveDesencriptada.Length < 10 Then
                '#EJC20171108_REF02_0605PM: Mensaje explicito en error de desencripción < 10 
                Throw New Exception("El valor obtenido de la licencia no es válido")
            ElseIf vClaveDesencriptada.StartsWith("SERVLIC") Then 'Es una licencia de server

                Dim vLlave As String = vClaveDesencriptada.Remove(0, 7) 'Remover identificador lic: SERVLIC
                Dim vMacServer As String = mid(vLlave, 1, vLlave.IndexOf("#")) 'Remover identificador lic: SERVLIC

                vLlave = vLlave.Remove(0, vMacServer.Length + 1) 'Remover MaccAdrr + indicador "#"

                '#GT16092025: si la cadena original tiene 6 valores, significa que es servidor legacy, que aun no maneja licencias UX
                'Se añade el valor 0 como licencia UX

                vParsingClaveDesencriptada = vLlave.Split(",")
                If vParsingClaveDesencriptada.Length = 6 Then
                    Dim lista As List(Of String) = vParsingClaveDesencriptada.ToList()
                    lista.Insert(3, 0)
                    vParsingClaveDesencriptada = lista.ToArray()
                End If

                vNoLicenciasBOF = vParsingClaveDesencriptada(1)
                vNoLicenciasHH = vParsingClaveDesencriptada(2)
                vNoLicenciasUx = vParsingClaveDesencriptada(3) '#GT09092025: licencias para propietarios en portal web, se corre un valor en el index para los demas campos
                yy = vParsingClaveDesencriptada(4) + 2000
                mm = vParsingClaveDesencriptada(5)
                dd = vParsingClaveDesencriptada(6)

                vFechaVence = New DateTime(yy, mm, dd)

                pLicencia.CantBackOffice = vNoLicenciasBOF
                pLicencia.CantHandHeld = vNoLicenciasHH
                pLicencia.CantUx = vNoLicenciasUx
                pLicencia.MacServer = vMacServer
                pLicencia.Vence = vFechaVence

            Else

                vParsingClaveDesencriptada = vClaveDesencriptada.Split(",")

                vNoLicenciasBOF = vParsingClaveDesencriptada(0)
                vNoLicenciasHH = vParsingClaveDesencriptada(1)
                vNoLicenciasUx = vParsingClaveDesencriptada(2) '#GT09092025: licencias para propietarios en portal web, se corre un valor en el index para los demas campos
                yy = vParsingClaveDesencriptada(3) + 2000
                mm = vParsingClaveDesencriptada(4)
                dd = vParsingClaveDesencriptada(5)

                vFechaVence = New DateTime(yy, mm, dd)

                pLicencia.CantBackOffice = vNoLicenciasBOF
                pLicencia.CantHandHeld = vNoLicenciasHH
                pLicencia.CantUx = vNoLicenciasUx
                pLicencia.Vence = vFechaVence
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

End Class