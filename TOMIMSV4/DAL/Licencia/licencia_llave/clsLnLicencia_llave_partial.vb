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
        Dim vNoLicenciasBOF, vNoLicenciasHH, yy, mm, dd As Integer

        Try

            vClaveDesencriptada = clsPublic.DecodeString(pLicencia.Llave)

            If vClaveDesencriptada.Length < 10 Then
                '#EJC20171108_REF02_0605PM: Mensaje explicito en error de desencripción < 10 
                Throw New Exception("El valor obtenido de la licencia no es válido")
            ElseIf vClaveDesencriptada.StartsWith("SERVLIC") 'Es una licencia de server

                Dim vLlave As String = vClaveDesencriptada.Remove(0, 7) 'Remover identificador lic: SERVLIC
                Dim vMacServer As String = mid(vLlave, 1, vLlave.IndexOf("#")) 'Remover identificador lic: SERVLIC

                vLlave = vLlave.Remove(0, vMacServer.Length + 1) 'Remover MaccAdrr + indicador "#"

                vParsingClaveDesencriptada = vLlave.Split(",")

                vNoLicenciasBOF = vParsingClaveDesencriptada(1)
                vNoLicenciasHH = vParsingClaveDesencriptada(2)
                yy = vParsingClaveDesencriptada(3) + 2000
                mm = vParsingClaveDesencriptada(4)
                dd = vParsingClaveDesencriptada(5)

                vFechaVence = New DateTime(yy, mm, dd)

                pLicencia.CantBackOffice = vNoLicenciasBOF
                pLicencia.CantHandHeld = vNoLicenciasHH
                pLicencia.MacServer = vMacServer
                pLicencia.Vence = vFechaVence

            Else

                vParsingClaveDesencriptada = vClaveDesencriptada.Split(",")

                vNoLicenciasBOF = vParsingClaveDesencriptada(0)
                vNoLicenciasHH = vParsingClaveDesencriptada(1)
                yy = vParsingClaveDesencriptada(2) + 2000
                mm = vParsingClaveDesencriptada(3)
                dd = vParsingClaveDesencriptada(4)

                vFechaVence = New DateTime(yy, mm, dd)

                pLicencia.CantBackOffice = vNoLicenciasBOF
                pLicencia.CantHandHeld = vNoLicenciasHH
                pLicencia.Vence = vFechaVence
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

End Class