Imports System.Data.SqlClient
Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports Newtonsoft.Json.Linq
Imports Sap.Data.Hana

Public Class clsSyncSAPCampaña

    Private Shared Function ConstruirQueryCampaña(Campaña As Integer) As String

        Return "SELECT  TOP 1 ""DocEntry"", ""Remark"", ""U_FechaInicial"", ""U_FechaFinal"" " &
               "FROM ""@APERIODOTIEMPO"" " &
               "WHERE ""DocEntry"" = " & Campaña & " " &
               " ORDER BY ""LogInst"" DESC"

    End Function

    Public Shared Function Insertar_Campaña_From_Sap_Hana(Campaña As Integer,
                                                         lConnection As SqlConnection,
                                                         lTransaction As SqlTransaction) As Integer

        Insertar_Campaña_From_Sap_Hana = 0

        Dim i As Integer = 0

        Try

            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface, lConnection, lTransaction)

            Using conn As HanaConnection = HanaHelper.OpenDB()

                Dim query As String = ConstruirQueryCampaña(Campaña)
                Dim dt As DataTable = HanaHelper.OpenDT(query, conn)

                If dt Is Nothing OrElse dt.Rows.Count = 0 Then
                    Throw New Exception("Error_20250105: Campaña no encontrada")
                    Return False
                End If

                Dim BeCampaña = MapearCampaña(Campaña, dt.Rows(0), BeConfigEnc.IdUsuario, lConnection, lTransaction)
                If Not clsLnCampaña.Existe_By_Codigo(BeCampaña.Codigo, lConnection, lTransaction) Then
                    clsLnCampaña.Insertar(BeCampaña, lConnection, lTransaction)
                    Insertar_Campaña_From_Sap_Hana = BeCampaña.IdCampaña
                End If

            End Using

        Catch ex As Exception
            Throw
        End Try

    End Function

    Private Shared Function MapearCampaña(Campaña As Integer, enc As DataRow, Usuario As String, lConnection As SqlConnection, lTransaction As SqlTransaction) As clsBeCampaña
        Dim be As New clsBeCampaña()
        be = New clsBeCampaña
        be.IdCampaña = Campaña
        be.Codigo = Convert.ToInt32(enc("DocEntry"))
        be.Nombre = IIf(IsDBNull(enc("Remark")), "", enc("Remark"))
        be.FechaInicio = Convert.ToDateTime(enc("U_FechaInicial"))
        be.FechaFin = Convert.ToDateTime(enc("U_FechaFinal"))
        be.Activo = True
        be.Fec_agr = Now
        be.Fec_mod = Now
        be.User_agr = Usuario
        be.User_mod = Usuario
        Return be
    End Function

    Public Shared Async Function Insertar_Campaña_From_Sap_Hana_SL(Campaña As Integer,
                                                                   lConnection As SqlConnection,
                                                                   lTransaction As SqlTransaction,
                                                                   sessionCookie As String,
                                                                   baseUrl As String,
                                                                   idUsuario As String) As Task(Of Integer)

        Dim vIdCampaña As Integer = Campaña

        Try

            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface, lConnection, lTransaction)

            Dim filtro As String = $"DocNum eq {Campaña}"
            Dim requestUrl As String = $"Periodo?$filter={Uri.EscapeDataString(filtro)}&$top=1"

            Using handler As New HttpClientHandler()
                handler.AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate
                handler.ServerCertificateCustomValidationCallback = Function(sender, cert, chain, errors) True
                handler.UseCookies = False

                Using client As New HttpClient(handler)
                    client.DefaultRequestHeaders.ConnectionClose = True

                    Using request As New HttpRequestMessage(HttpMethod.Get, baseUrl & requestUrl)
                        request.Headers.ConnectionClose = True
                        request.Headers.Add("Cookie", sessionCookie)
                        request.Headers.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))

                        Dim response = Await client.SendAsync(request).ConfigureAwait(False)
                        If Not response.IsSuccessStatusCode Then
                            Dim errContent = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
                            Throw New Exception($"Error al obtener campaña desde SAP SL. Código: {response.StatusCode}, Detalle: {errContent}")
                        End If

                        Dim jsonResponse = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
                        Dim obj = JObject.Parse(jsonResponse)
                        Dim rows = obj("value")

                        If rows Is Nothing OrElse Not rows.HasValues Then
                            Throw New Exception("Error_20250105: Campaña no encontrada en Service Layer")
                        End If

                        Dim dt As New DataTable()
                        dt.Columns.Add("DocEntry", GetType(Integer))
                        dt.Columns.Add("Remark", GetType(String))
                        dt.Columns.Add("U_FechaInicial", GetType(Date))
                        dt.Columns.Add("U_FechaFinal", GetType(Date))

                        Dim row = rows.First()
                        Dim dr = dt.NewRow()
                        dr("DocEntry") = row.Value(Of Integer)("DocEntry")
                        dr("Remark") = row.Value(Of String)("Remark")
                        dr("U_FechaInicial") = row.Value(Of Date)("U_FechaInicial")
                        dr("U_FechaFinal") = row.Value(Of Date)("U_FechaFinal")
                        dt.Rows.Add(dr)

                        Dim BeCampaña = MapearCampaña(Campaña, dr, idUsuario, lConnection, lTransaction)
                        If Not clsLnCampaña.Existe_By_Codigo(BeCampaña.Codigo, lConnection, lTransaction) Then
                            clsLnCampaña.Insertar(BeCampaña, lConnection, lTransaction)
                            vIdCampaña = BeCampaña.IdCampaña
                        End If

                    End Using
                End Using
            End Using

            Return vIdCampaña

        Catch ex As Exception
            Throw New Exception("Error en Insertar_Campaña_From_Sap_Hana_SL: " & ex.Message, ex)
        End Try

    End Function

End Class