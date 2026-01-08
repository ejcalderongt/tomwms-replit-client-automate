Imports System.Data.SqlClient
Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Reflection
Imports System.Text
Imports Sap.Data.Hana

Public Class clsSyncSapCodigosBarra : Inherits clsInterfaceBase

    Dim VContadorBitacoraTOMWMS As Integer = 0

    Public Shared Function Get_Codigos_Barra_Productos_SAP_HANA() As List(Of clsBeI_nav_producto)
        Dim codigo_barras As New List(Of clsBeI_nav_producto)

        Dim query As String =
            "SELECT DISTINCT T0.""Name"" ITEMCODE, T0.""CODIGOBARRA"" 
             FROM (
             SELECT ""Name"",concat(concat(""Name"", ""U_Color""), ""U_Talla"") CodigoBarra  
             FROM ""@CODIGO_BARRAS""
             WHERE ""U_ENVIADO_WMS"" = 2 AND concat(concat(""Name"", ""U_Color""), ""U_Talla"") IS NOT NULL
                   AND concat(concat(""Name"", ""U_Color""), ""U_Talla"")<>''
             UNION ALL
             SELECT ""Name"", ""U_CodigoAnterior"" CodigoBarra
             FROM ""@CODIGO_BARRAS""
             WHERE ""U_ENVIADO_WMS"" = 2 AND ""U_CodigoAnterior"" IS NOT NULL AND ""U_CodigoAnterior"" <> ''
             UNION ALL
             SELECT ""Name"", ""U_CodigoProv"" CodigoBarra
             FROM ""@CODIGO_BARRAS""
             WHERE ""U_ENVIADO_WMS"" = 2 AND ""U_CodigoProv"" IS NOT NULL AND ""U_CodigoProv"" <> ''
             UNION ALL
             SELECT ""Name"", ""U_CodigoAnterior2"" CodigoBarra
             FROM ""@CODIGO_BARRAS""
             WHERE ""U_ENVIADO_WMS"" = 2 AND ""U_CodigoAnterior2"" IS NOT NULL AND ""U_CodigoAnterior2""<>''
             UNION ALL
             SELECT ""Name"", ""U_CodigoAnterior3"" CodigoBarra
             FROM ""@CODIGO_BARRAS""
             WHERE ""U_ENVIADO_WMS"" = 2 AND ""U_CodigoAnterior3"" IS NOT NULL AND ""U_CodigoAnterior3""<>'') AS T0
            ORDER BY T0.""Name"""

        Try
            Using conn As HanaConnection = HanaHelper.OpenDB()
                Dim dt As DataTable = HanaHelper.OpenDT(query, conn)
                If dt IsNot Nothing Then
                    For Each row As DataRow In dt.Rows
                        codigo_barras.Add(MapRowToCodigoBarra(row))
                    Next
                End If
            End Using
        Catch ex As Exception
            Throw New Exception("Error al obtener productos: " & ex.Message)
        End Try

        Return codigo_barras
    End Function

    Public Function Importar_Codigos_Barra_Productos(ByRef lblprg As RichTextBox,
                                                     ByRef prg As ProgressBar) As Boolean

        Importar_Codigos_Barra_Productos = False

        Dim lConnection As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransaction As SqlTransaction = Nothing

        Try

            If MessageBox.Show("¿Actualizar códigos de barra de producto desde SAP?", "Alias", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                Dim lProductosFromSAP As New List(Of clsBeI_nav_producto)

                clsPublic.Actualizar_Progreso(lblprg, "Consultando productos en tabla intermedia ")

                lProductosFromSAP = Get_Codigos_Barra_Productos_SAP_HANA()

                clsPublic.Actualizar_Progreso(lblprg, String.Format("Productos en tabla intermedia: {0}", lProductosFromSAP.Count))

                If lProductosFromSAP.Count > 0 Then

                    Dim BeProductoExistente As clsBeProducto = Nothing
                    Dim BeProductoCodigoBarra As clsBeProducto_codigos_barra = Nothing
                    Dim vExisteBarra As Boolean = False

                    lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface, lConnection, lTransaction)

                    prg.Maximum = lProductosFromSAP.Count
                    prg.Visible = True

                    Dim vContador As Integer = 0

                    prg.Value = 0

                    clsPublic.Actualizar_Progreso(lblprg, "Trasladando codigos de barra de producto desde SAP a TOMWMS...")

                    For Each BeSAPProducto As clsBeI_nav_producto In lProductosFromSAP

                        BeProductoExistente = New clsBeProducto
                        BeProductoExistente = clsLnProducto.Existe(BeSAPProducto.No, lConnection, lTransaction)

                        If Not BeProductoExistente Is Nothing Then

                            vExisteBarra = clsLnProducto_codigos_barra.Existe_Codigo_Barra(BeProductoExistente.IdProducto, 1, BeSAPProducto.Item_Tracking_Code, lConnection, lTransaction)

                            If Not vExisteBarra Then

                                Try

                                    BeProductoCodigoBarra = New clsBeProducto_codigos_barra()
                                    BeProductoCodigoBarra.IdProductoCodigoBarra = clsLnProducto_codigos_barra.MaxID(lConnection, lTransaction) + 1
                                    BeProductoCodigoBarra.IdProducto = BeProductoExistente.IdProducto
                                    BeProductoCodigoBarra.IdProveedor = 1
                                    BeProductoCodigoBarra.Codigo_barra = BeSAPProducto.Item_Tracking_Code
                                    BeProductoCodigoBarra.User_agr = BeConfigEnc.IdUsuario
                                    BeProductoCodigoBarra.User_mod = BeConfigEnc.IdUsuario
                                    BeProductoCodigoBarra.Fec_agr = Now
                                    BeProductoCodigoBarra.Fec_mod = Now
                                    BeProductoCodigoBarra.Activo = True

                                    If clsLnProducto_codigos_barra.Insertar(BeProductoCodigoBarra, lConnection, lTransaction) > 0 Then

                                        If BeProductoExistente.Codigo_barra = "" Then
                                            BeProductoExistente.Codigo_barra = BeSAPProducto.Item_Tracking_Code
                                            clsLnProducto.Actualizar_CodigoBarra_By_IdProducto(BeProductoExistente, lConnection, lTransaction)
                                            clsPublic.Actualizar_Progreso(lblprg, "Se actualizó el código de barra: " & BeSAPProducto.Item_Tracking_Code & " para el dato maestro de WMS: " & BeProductoExistente.Codigo)
                                        End If

                                        VContadorBitacoraTOMWMS += 1

                                        clsPublic.Actualizar_Progreso(lblprg, "Código de barra: " & BeSAPProducto.Item_Tracking_Code & " actualizado para el itemcode: " & BeProductoExistente.Codigo)

                                    End If

                                Catch ex As Exception

                                    clsLnLog_error_wms.Agregar_Error(ex.Message)

                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Error: No se pudo actualizar el producto: {0} {1}", BeSAPProducto.No, vbNewLine))

                                    Application.DoEvents()

                                End Try

                            Else

                                clsPublic.Actualizar_Progreso(lblprg, "El código de barra: " & BeSAPProducto.Item_Tracking_Code & " ya existe para el itemcode: " & BeSAPProducto.No)

                            End If

                        Else
                            clsPublic.Actualizar_Progreso(lblprg, "No se encontró el itemcode: " & BeSAPProducto.No & " en el maestro de productos de WMS.")
                        End If

                        vContador += 1
                        prg.Value = vContador

                    Next

                    lTransaction.Commit()

                End If


                clsPublic.Actualizar_Progreso(lblprg, "Fin de proceso -> " & Now)
                clsPublic.Actualizar_Progreso(lblprg, String.Format("Productos procesados correctamente: {0}", VContadorBitacoraTOMWMS))
                Dim difSegundos As Double = DateDiff(DateInterval.Second, BeNavEjecucionEnc.Fecha, Now)
                clsPublic.Actualizar_Progreso(lblprg, String.Format("Tiempo transcurrido: {0} segundo(s)", difSegundos))

            End If


        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            prg.Value = 0
            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar producto a tabla de TOMWMS: {0}", ex.Message))
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
        End Try

    End Function

    Public Async Function Importar_Codigos_Barra_Productos_SL(lblprg As RichTextBox,
                                                              prg As ProgressBar) As Task(Of Boolean)

        Dim resultado As Boolean = False

        Dim lConnection As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransaction As SqlTransaction = Nothing

        Try
            If MessageBox.Show("¿Actualizar códigos de barra de producto desde SAP?", "Alias",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
                Return False
            End If

            clsPublic.Actualizar_Progreso(lblprg, "Consultando productos en tabla intermedia ")
            Dim vHanaService As New SapServiceLayerClient
            Dim loginResponse As LoginResponseDto = Await vHanaService.LoginAsync()

            If loginResponse Is Nothing OrElse String.IsNullOrEmpty(loginResponse.SessionId) Then
                clsPublic.Actualizar_Progreso(lblprg, "No se pudo obtener sesión.")
                Return False
            End If

            clsPublic.Actualizar_Progreso(lblprg, "Sesión iniciada correctamente.")

            ' Códigos de barra:
            Dim lCodigosBarra As List(Of ItemBarcodeDto)
            lCodigosBarra = Await clsSyncSAPCodigosBarra_SL.GetCodigosBarrasWmsAsync(vHanaService.SessionCookie,
                                               SapServiceLayerClient.baseUrl,
                                               lblprg)

            clsPublic.Actualizar_Progreso(lblprg,
                                      String.Format("Productos en tabla intermedia: {0}", lCodigosBarra.Count))

            If lCodigosBarra IsNot Nothing AndAlso lCodigosBarra.Count > 0 Then

                Dim BeProductoExistente As clsBeProducto = Nothing
                Dim BeProductoCodigoBarra As clsBeProducto_codigos_barra = Nothing
                Dim vExisteBarra As Boolean = False

                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface,
                                                          lConnection, lTransaction)

                prg.Maximum = lCodigosBarra.Count
                prg.Visible = True
                prg.Value = 0

                Dim vContador As Integer = 0

                clsPublic.Actualizar_Progreso(lblprg,
                                          "Trasladando codigos de barra de producto desde SAP a TOMWMS...")

                For Each BeSAPProducto As ItemBarcodeDto In lCodigosBarra

                    BeProductoExistente = clsLnProducto.Existe(BeSAPProducto.ItemCode, lConnection, lTransaction)

                    If BeProductoExistente IsNot Nothing Then

                        vExisteBarra = clsLnProducto_codigos_barra.Existe_Codigo_Barra(BeProductoExistente.IdProducto,
                                                                                    1,
                                                                                    BeSAPProducto.CodigoBarra,
                                                                                    lConnection, lTransaction)

                        If Not vExisteBarra Then
                            Try
                                BeProductoCodigoBarra = New clsBeProducto_codigos_barra()
                                BeProductoCodigoBarra.IdProductoCodigoBarra =
                                clsLnProducto_codigos_barra.MaxID(lConnection, lTransaction) + 1
                                BeProductoCodigoBarra.IdProducto = BeProductoExistente.IdProducto
                                BeProductoCodigoBarra.IdProveedor = 1
                                BeProductoCodigoBarra.Codigo_barra = BeSAPProducto.CodigoBarra
                                BeProductoCodigoBarra.User_agr = BeConfigEnc.IdUsuario
                                BeProductoCodigoBarra.User_mod = BeConfigEnc.IdUsuario
                                BeProductoCodigoBarra.Fec_agr = Now
                                BeProductoCodigoBarra.Fec_mod = Now
                                BeProductoCodigoBarra.Activo = True
                                BeProductoCodigoBarra.IdColor = BeSAPProducto.IdColor
                                BeProductoCodigoBarra.IdTalla = BeSAPProducto.IdTalla

                                If clsLnProducto_codigos_barra.Insertar(BeProductoCodigoBarra,
                                                                        lConnection, lTransaction) > 0 Then

                                    If BeProductoExistente.Codigo_barra = "" Then
                                        BeProductoExistente.Codigo_barra = BeSAPProducto.CodigoBarra
                                        clsLnProducto.Actualizar_CodigoBarra_By_IdProducto(BeProductoExistente,
                                                                                       lConnection, lTransaction)
                                        clsPublic.Actualizar_Progreso(lblprg,
                                        "Se actualizó el código de barra: " & BeSAPProducto.CodigoBarra &
                                        " para el dato maestro de WMS: " & BeProductoExistente.Codigo)
                                    End If

                                    VContadorBitacoraTOMWMS += 1

                                    Try
                                        Marcar_Codigo_Barra_Sincronizado_SL(BeSAPProducto.Code, lblprg)
                                    Catch ex As Exception

                                    End Try

                                    clsPublic.Actualizar_Progreso(lblprg,
                                    "Código de barra: " & BeSAPProducto.CodigoBarra &
                                    " actualizado para el itemcode: " & BeProductoExistente.Codigo)
                                End If

                            Catch ex As Exception
                                clsLnLog_error_wms.Agregar_Error(ex.Message)
                                clsPublic.Actualizar_Progreso(lblprg,
                                String.Format("Error: No se pudo actualizar el producto: {0} {1}",
                                              BeSAPProducto.ItemCode, vbNewLine))
                                Application.DoEvents()
                            End Try
                        Else
                            clsPublic.Actualizar_Progreso(lblprg,
                            "El código de barra: " & BeSAPProducto.CodigoBarra &
                            " ya existe para el itemcode: " & BeSAPProducto.ItemCode)
                        End If

                    Else
                        clsPublic.Actualizar_Progreso(lblprg,
                        "No se encontró el itemcode: " & BeSAPProducto.ItemCode &
                        " en el maestro de productos de WMS.")
                    End If

                    vContador += 1
                    prg.Value = vContador

                Next

                lTransaction.Commit()
                resultado = True
            End If

            clsPublic.Actualizar_Progreso(lblprg, "Fin de proceso -> " & Now)
            clsPublic.Actualizar_Progreso(lblprg,
                                      String.Format("Productos procesados correctamente: {0}", VContadorBitacoraTOMWMS))
            Dim difSegundos As Double = DateDiff(DateInterval.Second, BeNavEjecucionEnc.Fecha, Now)
            clsPublic.Actualizar_Progreso(lblprg,
                                      String.Format("Tiempo transcurrido: {0} segundo(s)", difSegundos))

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            prg.Value = 0
            clsPublic.Actualizar_Progreso(lblprg,
                                      String.Format("Error al insertar producto a tabla de TOMWMS: {0}", ex.Message))
            Throw
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
        End Try

        Return resultado

    End Function

    Private Shared Function MapRowToCodigoBarra(row As DataRow) As clsBeI_nav_producto
        Return New clsBeI_nav_producto With {
            .No = row("ITEMCODE").ToString(),
            .Item_Tracking_Code = row("CODIGOBARRA").ToString()
        }
    End Function

    Private Shared Function Marcar_Codigo_Barra_Sincronizado_SL(ItemCode As Integer,
                                                                lbl As RichTextBox) As Boolean

        Try
            If String.IsNullOrWhiteSpace(ItemCode) Then Return False

            Dim vHanaService As SapServiceLayerClient

            vHanaService = New SapServiceLayerClient()
            Dim loginResponse As LoginResponseDto = vHanaService.LoginAsync().GetAwaiter().GetResult()

            If loginResponse Is Nothing OrElse String.IsNullOrEmpty(loginResponse.SessionId) Then
                clsPublic.Actualizar_Progreso(lbl, "No se pudo obtener sesión.")
            Else
                clsPublic.Actualizar_Progreso(lbl, "Conexión correcta. Token: " & vHanaService.SessionCookie)
                Debug.WriteLine(vHanaService.SessionCookie)
            End If

            Dim baseUrl As String = SapServiceLayerClient.baseUrl
            Dim sessionCookie As String = vHanaService.SessionCookie

            Dim requestUrl As String = $"U_CODIGO_BARRAS({ItemCode})"
            Dim payload As String = "{""U_ENVIADO_WMS"": 1}"

            Dim httpPatch As New HttpMethod("PATCH")

            Using handler As New HttpClientHandler()
                handler.AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate
                handler.ServerCertificateCustomValidationCallback = Function(sender, cert, chain, errors) True
                handler.UseCookies = False

                Using client As New HttpClient(handler)
                    client.DefaultRequestHeaders.ConnectionClose = True

                    Using request As New HttpRequestMessage(httpPatch, baseUrl & requestUrl)
                        request.Headers.ConnectionClose = True
                        request.Headers.Add("Cookie", sessionCookie)
                        request.Headers.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
                        request.Content = New StringContent(payload, Encoding.UTF8, "application/json")

                        Dim response = client.SendAsync(request).Result

                        If response.IsSuccessStatusCode Then
                            Return True
                        Else
                            Dim errContent = response.Content.ReadAsStringAsync().Result
                            Throw New Exception($"Error al actualizar el código de barra. Código: {response.StatusCode}, Detalle: {errContent}")
                        End If
                    End Using
                End Using
            End Using

        Catch ex As Exception
            Throw New Exception($"(SL) {MethodBase.GetCurrentMethod().Name} {ex.Message}", ex)
        End Try

    End Function

End Class