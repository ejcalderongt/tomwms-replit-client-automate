Imports System.Drawing
Imports System.Net
Imports System.Net.Http
Imports System.Threading.Tasks
Imports Newtonsoft.Json

Public Class ChatGPTService

    Private ReadOnly apiKey As String = Environment.GetEnvironmentVariable("OPENAI_API_KEY")

    Dim vprontEJC20231014 As String = "#EJC20231014" & vbCrLf &
                       "analizar estos datos y dime cuál está próximo a vencer, retorna la respuesta en un formato" & vbCrLf &
                       "json en una estructura que inicie con el el nombre respuesta_wms" & vbCrLf &
                       "{" & vbCrLf &
                       "    ""productos"": [" & vbCrLf &
                       "    {" & vbCrLf &
                       "        ""codigo"": ""47022""," & vbCrLf &
                       "        ""nombre"": ""200 ML. MANZANA FRUVITA CJ-24U TETRA 7401001508015""," & vbCrLf &
                       "        ""vence"": ""2026-12-01T00:00:00.000""," & vbCrLf &
                       "        ""cantidad"": 15," & vbCrLf &
                       "        ""ubicacion"": ""R02 - C6 - TA - N2 - C - #997""," & vbCrLf &
                       "        ""es_rack"": 1" & vbCrLf &
                       "    }," & vbCrLf &
                       "    {" & vbCrLf &
                       "        ""codigo"": ""47022""," & vbCrLf &
                       "        ""nombre"": ""200 ML. MANZANA FRUVITA CJ-24U TETRA 7401001508015""," & vbCrLf &
                       "        ""vence"": ""2026-12-30T00:00:00.000""," & vbCrLf &
                       "        ""cantidad"": 10," & vbCrLf &
                       "        ""ubicacion"": ""RECHAZO - #3222""," & vbCrLf &
                       "        ""es_rack"": 0" & vbCrLf &
                       "    }," & vbCrLf &
                       "    {" & vbCrLf &
                       "        ""codigo"": ""47022""," & vbCrLf &
                       "        ""nombre"": ""200 ML. MANZANA FRUVITA CJ-24U TETRA 7401001508015""," & vbCrLf &
                       "        ""vence"": ""2026-12-10T00:00:00.000""," & vbCrLf &
                       "        ""cantidad"": 24," & vbCrLf &
                       "        ""ubicacion"": ""J03 - C8 - TA - N1 - A - #26""," & vbCrLf &
                       "        ""es_rack"": 1" & vbCrLf &
                       "    }" & vbCrLf &
                       "    ]" & vbCrLf &
                       "}"

    Public Async Function Get_Generyc_Response_Async(inputText As String, modelName As String) As Task(Of String)

        Try

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Using client As New HttpClient()

                client.BaseAddress = New Uri("https://api.openai.com")
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " & apiKey)

                Dim organization As String = Nothing
                If Not String.IsNullOrEmpty(organization) Then
                    client.DefaultRequestHeaders.Add("Openai-Organization", organization)
                End If

                Dim requestBody = New With {
                .model = modelName,
                .messages = New List(Of Object) From {
                        New With {.role = "system", .content = "traduce el texto recibido en solicitud json para chatgpt"},
                        New With {.role = "user", .content = inputText}
                    }
                }

                Dim jsonContentString = JsonConvert.SerializeObject(requestBody)
                Dim jsonContent = New StringContent(jsonContentString, Encoding.UTF8, "application/json")
                Dim response As HttpResponseMessage = Await client.PostAsync("/v1/chat/completions", jsonContent)

                Dim result As New Dictionary(Of String, String)()
                If response.IsSuccessStatusCode Then
                    Dim apiResponse = Await response.Content.ReadAsStringAsync()
                    result("status") = "success"
                    result("response") = apiResponse
                ElseIf response.StatusCode = CType(429, HttpStatusCode) Then
                    result("status") = "error"
                    result("message") = "Demasiadas solicitudes. Por favor, inténtelo de nuevo más tarde."
                Else
                    Dim errorContent = Await response.Content.ReadAsStringAsync()
                    result("status") = "error"
                    result("message") = "Error: " & response.StatusCode.ToString() & " Contenido: " & errorContent
                End If

                Return JsonConvert.SerializeObject(result)

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    ''' <summary>
    ''' #EJC20231014: Pront: #EJC20231014
    ''' </summary>
    ''' <param name="prompt"></param>
    ''' <param name="modelName"></param>
    ''' <returns></returns>
    Public Async Function Get_Reporte_Proximos_A_Vencer_By_OpenIA(prompt As String, modelName As String) As Task(Of String)


        Try

            If Not prompt.Contains("#EJC20231014") Then

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

                Using client As New HttpClient()
                    client.BaseAddress = New Uri("https://api.openai.com")
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " & apiKey)

                    Dim gpt3Request = New GPT3Request() With {
                        .model = modelName,
                        .messages = New List(Of Message) From {
                            New Message() With {.role = "system", .content = "Eres un gerente de logística"},
                            New Message() With {.role = "user", .content = JsonConvert.SerializeObject(vprontEJC20231014)}
                        }
                    }

                    Dim jsonContentString = JsonConvert.SerializeObject(gpt3Request)
                    Dim jsonContent = New StringContent(jsonContentString, Encoding.UTF8, "application/json")

                    Dim response As HttpResponseMessage = Await client.PostAsync("/v1/chat/completions", jsonContent)

                    If response.IsSuccessStatusCode Then
                        Return Await response.Content.ReadAsStringAsync()
                    ElseIf response.StatusCode = CType(429, HttpStatusCode) Then
                        Return "Error: Too many requests. Please try again later."
                    Else
                        Dim errorContent = Await response.Content.ReadAsStringAsync()
                        Debug.WriteLine("Error Content: " & errorContent)
                        Return "Error: " & response.StatusCode.ToString() & " Content: " & errorContent
                    End If
                End Using
            Else
                Return "La solicitud está mal formata, se espera un prompt como el siguiente: " & vprontEJC20231014
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    'Public Async Function Get_Info_Imagen_By_IA(modelName As String) As Task(Of String)

    '    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

    '    Dim vResponse As String = ""

    '    Try
    '        Using client As New HttpClient()

    '            client.BaseAddress = New Uri("https://api.openai.com")
    '            client.DefaultRequestHeaders.Add("Authorization", "Bearer " & apiKey)

    '            Dim organization As String = Nothing
    '            If Not String.IsNullOrEmpty(organization) Then
    '                client.DefaultRequestHeaders.Add("Openai-Organization", organization)
    '            End If

    '            Dim listOfImages As New List(Of Image)
    '            listOfImages = GetAllResourceImages()

    '            If listOfImages IsNot Nothing AndAlso listOfImages.Count > 0 Then

    '                Dim allResults As New List(Of Dictionary(Of String, String))()

    '                Dim LaImagenEnByte As Byte() = clsPublic.ImageToByteArray(listOfImages(0))

    '                If LaImagenEnByte IsNot Nothing Then
    '                    ' Convertir la imagen a formato Base64
    '                    Dim base64Image As String = Convert.ToBase64String(LaImagenEnByte)

    '                    Dim requestBody = New With {
    '                        .model = modelName,
    '                        .messages = New List(Of Object) From {
    '                            New With {
    '                                .role = "system",
    '                                .content = "analiza la imágen y botén los valores que coincidan con:
    '                                            número de confirmación o de transacción, 
    '                                            valor o monto 
    '                                           fecha de documento   
    '                                           retornar estructura json con formato:
    '                                            {
    '                                                ""numero_confirmacion"": ""valor_obtenido"",
    '                                                ""monto"": ""valor_obtenido"",
    '                                                ""moneda"": ""valor_obtenido"",
    '                                                ""fecha_transaccion"": ""formato: yyyyMMdd"",
    '                                                ""destinatario"": ""si no se encuentra, colocar valor no identificado""  
    '                                            }"
    '                            },
    '                            New With {
    '                                .role = "user",
    '                                .content = "this is the image on base64Image '" & base64Image & "'"
    '                            }
    '                        }
    '                    }

    '                    Dim jsonContentString = JsonConvert.SerializeObject(requestBody)
    '                    Dim jsonContent = New StringContent(jsonContentString, Encoding.UTF8, "application/json")
    '                    Dim response As HttpResponseMessage = Await client.PostAsync("/v1/chat/completions", jsonContent)

    '                    Dim result As New Dictionary(Of String, String)()
    '                    If response.IsSuccessStatusCode Then
    '                        Dim apiResponse = Await response.Content.ReadAsStringAsync()
    '                        result("status") = "success"
    '                        result("response") = apiResponse
    '                        vResponse = apiResponse
    '                    ElseIf response.StatusCode = CType(429, HttpStatusCode) Then
    '                        result("status") = "error"
    '                        result("message") = "Demasiadas solicitudes. Por favor, inténtelo de nuevo más tarde."
    '                    Else
    '                        Dim errorContent = Await response.Content.ReadAsStringAsync()
    '                        result("status") = "error"
    '                        result("message") = "Error: " & response.StatusCode.ToString() & " Contenido: " & errorContent
    '                    End If

    '                    allResults.Add(result)

    '                End If

    '                Return vResponse

    '            End If

    '        End Using

    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Function

    Public Function GetAllResourceImages() As List(Of Image)
        Dim imagesList As New List(Of Image)()
        imagesList.Add(My.Resources.pago1)
        imagesList.Add(My.Resources.pago2)
        imagesList.Add(My.Resources.pago3)
        Return imagesList
    End Function

    Public Class Message
        Public Property content As String
        Public Property role As String
    End Class
    Public Class GPT3Request
        Public Property messages As List(Of Message)
        Public Property model As String
    End Class


End Class

