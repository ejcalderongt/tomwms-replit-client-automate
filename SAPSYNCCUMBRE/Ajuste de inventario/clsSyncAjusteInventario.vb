Imports System.Data.SqlClient
Imports System.Net.Http
Imports System.Reflection
Imports System.Text
Imports Newtonsoft.Json
Imports SAPbobsCOM

Public Class clsSyncAjusteInventario : Inherits clsInterfaceBase
    Implements IDisposable

    Private oCompany As Company
    Dim lRetCode, lErrCode As Long
    Dim sErrMsg As String = ""

    Dim CuentaSAPInventario As String
    Public Property URLServicioEntrada As String = ""
    Public Property URLServicioSalida As String = ""

    'Public Sub Sync_Ajustes(ByVal lblprg As RichTextBox,
    '                        ByRef prg As System.Windows.Forms.ProgressBar)

    '    Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
    '    Dim TransLog As SqlTransaction = Nothing
    '    Dim Resultado As String = ""

    '    Try

    '        CnnLog.Open() : TransLog = CnnLog.BeginTransaction(IsolationLevel.ReadUncommitted)

    '        clsPublic.Actualizar_Progreso(lblprg, "Consultando ajustes pendientes de envío.")

    '        Dim lAjustesPendEnvio As New List(Of clsBeAjustesMI3)
    '        lAjustesPendEnvio = clsLnI_nav_transacciones_out.Get_Ajustes_Auditados_Pendientes_Envio_MI3(Resultado, CnnLog, TransLog)

    '        If Not lAjustesPendEnvio Is Nothing Then

    '            Dim lVistaAjustesPendientesEnvio As New List(Of clsBe_vw_ajustes)
    '            Dim vDif As Double = 0
    '            Dim vNoDocumento As String = ""
    '            Dim vContador As Integer = 0
    '            Dim BeAjusteDet As New clsBeTrans_ajuste_det
    '            Dim DetallesEnviados As Integer = 0
    '            Dim BeFamilia As New clsBeProducto_familia
    '            Dim vSerieBodega As String = ""
    '            Dim BeCliente As New clsBeCliente
    '            Dim Cod_Variante As String = ""
    '            Dim AjusteDoc As New clsBeTrans_ajuste_det_doc
    '            Dim MaxIdAjusteDoc As Integer = 0
    '            Dim vNomenclaturaBase As String = ""
    '            Dim vCorrelativoActual As Integer = 0
    '            Dim BeBodega As New clsBeBodega
    '            Dim vCuentaIngresos As String = ""
    '            Dim vCuentaEgresos As String = ""
    '            prg.Maximum = lAjustesPendEnvio.Count

    '            If lAjustesPendEnvio.Count > 0 Then

    '                BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface, CnnLog, TransLog)

    '                BeBodega = clsLnBodega.GetSingle_By_Idbodega(BeConfigEnc.Idbodega, CnnLog, TransLog)

    '                vCuentaIngresos = BeBodega.Cuenta_Ingreso_Mercancias
    '                vCuentaEgresos = BeBodega.Cuenta_Egreso_Mercancias

    '                URLServicioEntrada = BD.Instancia.URL_ENTRADA_AJUSTE_POST
    '                URLServicioSalida = BD.Instancia.URL_SALIDA_AJUSTE_POST

    '                ' Agrupar por IdAjusteEnc y luego por Codigo_Bodega
    '                Dim groupedByAjuste = lAjustesPendEnvio.GroupBy(Function(ajuste) ajuste.IdAjusteEnc)

    '                For Each ajusteGroup In groupedByAjuste

    '                    Dim ajustesPorBodega = ajusteGroup.GroupBy(Function(ajuste) ajuste.Codigo_Bodega)
    '                    Dim ajustesPorBodegaYProducto = ajusteGroup _
    '                                                            .GroupBy(Function(ajuste) New With {
    '                                                                Key .IdAjusteEnc = ajuste.IdAjusteEnc,
    '                                                                Key .Codigo_Bodega = ajuste.Codigo_Bodega,
    '                                                                Key .Codigo_Producto = ajuste.Codigo_Producto,
    '                                                                Key .Codigo_Centro_Costo = ajuste.Codigo_Centro_Costo,
    '                                                                Key .Tipo = ajuste.TipoAjusteWMS}) _
    '                                                            .Select(Function(grp) New With {
    '                                                                .IdAjusteEnc = grp.Key.IdAjusteEnc,
    '                                                                .Codigo_Bodega = grp.Key.Codigo_Bodega,
    '                                                                .Codigo_Producto = grp.Key.Codigo_Producto,
    '                                                                .Codigo_Centro_Costo = grp.Key.Codigo_Centro_Costo,
    '                                                                .TipoAjusteWMSGrp = grp.Key.Tipo,
    '                                                                .Cantidad_Total = grp.Sum(Function(ajuste) ajuste.Cantidad),
    '                                                                .Ajustes = grp.ToList()
    '                                                            })

    '                    For Each bodegaGroup In ajustesPorBodegaYProducto

    '                        Dim entradas As New List(Of Object)
    '                        Dim salidas As New List(Of Object)

    '                        Dim entry = New With {
    '                                                .ItemCode = bodegaGroup.Codigo_Producto,
    '                                                .Cantidad = bodegaGroup.Cantidad_Total
    '                                            }

    '                        If bodegaGroup.TipoAjusteWMSGrp = "Ajuste Positivo" Then
    '                            entradas.Add(entry)
    '                            clsPublic.Actualizar_Progreso(lblprg, "Procesando ajuste positivo + WMS:# :" & bodegaGroup.IdAjusteEnc)
    '                        ElseIf bodegaGroup.TipoAjusteWMSGrp = "Ajuste Negativo" Then
    '                            salidas.Add(entry)
    '                            clsPublic.Actualizar_Progreso(lblprg, "Procesando ajuste negativo - WMS: #: " & bodegaGroup.IdAjusteEnc)
    '                        End If

    '                        If entradas.Count > 0 Then

    '                            Dim entradaBody = New With {
    '                                .Bodega = bodegaGroup.Codigo_Bodega,
    '                                .RemarkCode = ajusteGroup.First().Codigo_Centro_Costo,
    '                                .NombreUsuario = "TOMWMS",
    '                                .Entries = entradas
    '                            }
    '                            Dim entradaJson = JsonConvert.SerializeObject(entradaBody)
    '                            Dim entradaResponse = SendPostRequest(URLServicioEntrada, entradaJson)
    '                            ' Procesar la respuesta de la entrada

    '                            clsPublic.Actualizar_Progreso(lblprg, "Ajuste positivo enviado a SAP.")
    '                            clsPublic.Actualizar_Progreso(lblprg, "IdAjusteEnc ->  " & ajusteGroup.First().IdAjusteEnc)
    '                            clsPublic.Actualizar_Progreso(lblprg, "Codigo_Bodega ->  " & bodegaGroup.Codigo_Bodega)
    '                            clsPublic.Actualizar_Progreso(lblprg, "Remark ->  " & bodegaGroup.Codigo_Centro_Costo)
    '                            clsPublic.Actualizar_Progreso(lblprg, entradaResponse.ToString())

    '                            If entradaResponse.ToString.Contains("éxito") Then

    '                                Dim ajPositivos As New List(Of clsBeAjustesMI3)
    '                                ajPositivos = lAjustesPendEnvio.FindAll(Function(x) x.TipoAjusteWMS = "Ajuste Positivo" AndAlso x.IdAjusteEnc = bodegaGroup.IdAjusteEnc)

    '                                For Each aj In ajPositivos
    '                                    aj.Observacion = "Enviado"
    '                                Next

    '                            End If

    '                        End If

    '                        If salidas.Count > 0 Then
    '                            Dim salidaBody = New With {
    '                                .Bodega = bodegaGroup.Codigo_Bodega,
    '                                .RemarkCode = ajusteGroup.First().Codigo_Centro_Costo,
    '                                .NombreUsuario = "TOMWMS",
    '                                .Entries = salidas
    '                            }
    '                            Dim salidaJson = JsonConvert.SerializeObject(salidaBody)
    '                            Dim salidaResponse = SendPostRequest(URLServicioSalida, salidaJson)
    '                            ' Procesar la respuesta de la salida

    '                            clsPublic.Actualizar_Progreso(lblprg, "Ajuste negativo enviado a SAP.")
    '                            clsPublic.Actualizar_Progreso(lblprg, "IdAjusteEnc ->  " & ajusteGroup.First().IdAjusteEnc)
    '                            clsPublic.Actualizar_Progreso(lblprg, "Codigo_Bodega ->  " & bodegaGroup.Codigo_Bodega)
    '                            clsPublic.Actualizar_Progreso(lblprg, "Remark ->  " & bodegaGroup.Codigo_Centro_Costo)
    '                            clsPublic.Actualizar_Progreso(lblprg, salidaResponse.ToString())

    '                            If salidaResponse.ToString.Contains("éxito") Then

    '                                Dim ajNegativos As New List(Of clsBeAjustesMI3)
    '                                ajNegativos = lAjustesPendEnvio.FindAll(Function(x) x.TipoAjusteWMS = "Ajuste Negativo" AndAlso x.IdAjusteEnc = bodegaGroup.IdAjusteEnc)

    '                                For Each aj In ajNegativos
    '                                    aj.Observacion = "Enviado"
    '                                Next

    '                            End If

    '                        End If

    '                    Next

    '                Next

    '                For Each ajuste In lAjustesPendEnvio

    '                    If (ajuste.Observacion.Contains("Enviado")) Then

    '                        clsLnTrans_ajuste_det.Actualizar_Estado_Enviado_A_ERP(ajuste.IdAjusteDet, True, CnnLog, TransLog)

    '                    End If

    '                Next

    '            Else
    '                clsPublic.Actualizar_Progreso(lblprg, "No hay ajustes pendientes de envío.")
    '            End If

    '        Else
    '            clsPublic.Actualizar_Progreso(lblprg, "No hay ajustes pendientes de envío.")
    '        End If

    '        clsPublic.Actualizar_Progreso(lblprg, "Fin de sincronización de ajustes.")

    '        TransLog.Commit()

    '    Catch ex As Exception

    '        clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al enviar ajustes a SAP: {0}{1}", vbNewLine, ex.Message))

    '        If Not TransLog Is Nothing Then TransLog.Rollback()

    '        clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
    '                                                   "Sync_Ajustes",
    '                                                   BeNavEjecucionEnc.IdEjecucionEnc,
    '                                                   BeConfigDet.Idnavconfigdet, CnnLog)

    '        Throw New Exception(String.Format(" (M) {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))

    '    Finally
    '        If Not CnnLog Is Nothing AndAlso CnnLog.State = ConnectionState.Open Then CnnLog.Close()
    '    End Try

    'End Sub

    Public Sub Sync_Ajustes1(ByVal lblprg As RichTextBox,
                            ByRef prg As System.Windows.Forms.ProgressBar)

        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim TransLog As SqlTransaction = Nothing
        Dim Resultado As String = ""

        Try

            CnnLog.Open() : TransLog = CnnLog.BeginTransaction(IsolationLevel.ReadUncommitted)
            clsPublic.Actualizar_Progreso(lblprg, "Consultando ajustes pendientes de envío.")

            Dim lAjustesPendEnvio As New List(Of clsBeAjustesMI3)
            lAjustesPendEnvio = clsLnI_nav_transacciones_out.Get_Ajustes_Auditados_Pendientes_Envio_MI3(Resultado, CnnLog, TransLog)

            If Not lAjustesPendEnvio Is Nothing AndAlso lAjustesPendEnvio.Count > 0 Then

                Dim BeConfigEnc As clsBeI_nav_config_enc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface, CnnLog, TransLog)
                Dim BeBodega As clsBeBodega = clsLnBodega.GetSingle_By_Idbodega(BeConfigEnc.Idbodega, CnnLog, TransLog)

                Dim vCuentaIngresos As String = BeBodega.Cuenta_Ingreso_Mercancias
                Dim vCuentaEgresos As String = BeBodega.Cuenta_Egreso_Mercancias

                URLServicioEntrada = BD.Instancia.URL_ENTRADA_AJUSTE_POST
                URLServicioSalida = BD.Instancia.URL_SALIDA_AJUSTE_POST

                ' Agrupar por tipo de ajuste (Positivo/Negativo)
                Dim ajustesPositivos = New List(Of Object)()
                Dim ajustesNegativos = New List(Of Object)()

                prg.Maximum = lAjustesPendEnvio.Count

                For Each ajuste In lAjustesPendEnvio

                    Dim entry = New With {
                    .ItemCode = ajuste.Codigo_Producto,
                    .Cantidad = ajuste.Cantidad
                }

                    If ajuste.TipoAjusteWMS = "Ajuste Positivo" Then
                        ajustesPositivos.Add(entry)
                        clsPublic.Actualizar_Progreso(lblprg, "Agregando producto a ajuste positivo: " & ajuste.Codigo_Producto)
                    ElseIf ajuste.TipoAjusteWMS = "Ajuste Negativo" Then
                        ajustesNegativos.Add(entry)
                        clsPublic.Actualizar_Progreso(lblprg, "Agregando producto a ajuste negativo: " & ajuste.Codigo_Producto)
                    End If

                Next

                ' Enviar un solo documento para todos los productos positivos
                If ajustesPositivos.Count > 0 Then
                    Dim entradaBody = New With {
                    .Bodega = lAjustesPendEnvio.First().Codigo_Bodega,
                    .RemarkCode = lAjustesPendEnvio.First().Codigo_Centro_Costo,
                    .NombreUsuario = "TOMWMS",
                    .Entries = ajustesPositivos
                }
                    Dim entradaJson = JsonConvert.SerializeObject(entradaBody)
                    Dim entradaResponse = SendPostRequest(URLServicioEntrada, entradaJson)

                    clsPublic.Actualizar_Progreso(lblprg, "Ajuste positivo enviado a SAP.")
                    clsPublic.Actualizar_Progreso(lblprg, entradaResponse.ToString())

                    ' Marcar ajustes positivos como enviados
                    If entradaResponse.ToString.Contains("éxito") Then
                        For Each ajuste In lAjustesPendEnvio.Where(Function(x) x.TipoAjusteWMS = "Ajuste Positivo")
                            ajuste.Observacion = "Enviado"
                        Next
                    End If
                End If

                ' Enviar un solo documento para todos los productos negativos
                If ajustesNegativos.Count > 0 Then
                    Dim salidaBody = New With {
                    .Bodega = lAjustesPendEnvio.First().Codigo_Bodega,
                    .RemarkCode = lAjustesPendEnvio.First().Codigo_Centro_Costo,
                    .NombreUsuario = "TOMWMS",
                    .Entries = ajustesNegativos
                }
                    Dim salidaJson = JsonConvert.SerializeObject(salidaBody)
                    Dim salidaResponse = SendPostRequest(URLServicioSalida, salidaJson)

                    clsPublic.Actualizar_Progreso(lblprg, "Ajuste negativo enviado a SAP.")
                    clsPublic.Actualizar_Progreso(lblprg, salidaResponse.ToString())

                    ' Marcar ajustes negativos como enviados
                    If salidaResponse.ToString.Contains("éxito") Then
                        For Each ajuste In lAjustesPendEnvio.Where(Function(x) x.TipoAjusteWMS = "Ajuste Negativo")
                            ajuste.Observacion = "Enviado"
                        Next
                    End If
                End If

                ' Actualizar ajustes como enviados en la base de datos
                For Each ajuste In lAjustesPendEnvio
                    If ajuste.Observacion.Contains("Enviado") Then
                        clsLnTrans_ajuste_det.Actualizar_Estado_Enviado_A_ERP(ajuste.IdAjusteDet, True, CnnLog, TransLog)
                    End If
                Next

                clsPublic.Actualizar_Progreso(lblprg, "Fin de sincronización de ajustes.")
                TransLog.Commit()

            Else
                clsPublic.Actualizar_Progreso(lblprg, "No hay ajustes pendientes de envío.")
            End If

        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al enviar ajustes a SAP: {0}{1}", vbNewLine, ex.Message))
            If Not TransLog Is Nothing Then TransLog.Rollback()

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                   "Sync_Ajustes",
                                                   BeNavEjecucionEnc.IdEjecucionEnc,
                                                   BeConfigDet.Idnavconfigdet, CnnLog)

            Throw New Exception(String.Format(" (M) {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))

        Finally
            If Not CnnLog Is Nothing AndAlso CnnLog.State = ConnectionState.Open Then CnnLog.Close()
        End Try

    End Sub


    Public Async Function EnviarEntradaAsync(entrada As clsBeEntidadAjusteSAPCumbre) As Task(Of String)

        Try

            'Dim url As String = "http://10.10.1.12:8088/api/TOMWMS/Entrada"

            Using client As New HttpClient()

                client.DefaultRequestHeaders.Accept.Clear()
                client.DefaultRequestHeaders.Accept.Add(New Headers.MediaTypeWithQualityHeaderValue("application/json"))

                Dim json As String = JsonConvert.SerializeObject(entrada)
                Dim content As New StringContent(json, Encoding.UTF8, "application/json")

                Dim response As HttpResponseMessage = Await client.PostAsync(URLServicioEntrada, content)
                If response.IsSuccessStatusCode Then
                    Return Await response.Content.ReadAsStringAsync()
                Else
                    Throw New Exception($"Error: {response.StatusCode} - {response.ReasonPhrase}")
                End If

            End Using

        Catch ex As Exception
            Throw
        End Try

    End Function

    Public Async Function EnviarSalidaAsync(salida As clsBeEntidadAjusteSAPCumbre) As Task(Of String)

        Try

            'Dim url As String = "http://10.10.1.12:8088/api/TOMWMS/Salida"

            Using client As New HttpClient()

                client.DefaultRequestHeaders.Accept.Clear()
                client.DefaultRequestHeaders.Accept.Add(New Headers.MediaTypeWithQualityHeaderValue("application/json"))

                Dim json As String = JsonConvert.SerializeObject(salida)
                Dim content As New StringContent(json, Encoding.UTF8, "application/json")

                Dim response As HttpResponseMessage = Await client.PostAsync(URLServicioSalida, content)
                If response.IsSuccessStatusCode Then
                    Return Await response.Content.ReadAsStringAsync()
                Else
                    Throw New Exception($"Error: {response.StatusCode} - {response.ReasonPhrase}")
                End If

            End Using

        Catch ex As Exception
            Throw
        End Try

    End Function

    Public Shared Function SendPostRequest(url As String, jsonContent As String) As String

        Try

            Using client As New HttpClient()
                Dim content As New StringContent(jsonContent, Encoding.UTF8, "application/json")
                Dim response As HttpResponseMessage = client.PostAsync(url, content).Result
                Return response.Content.ReadAsStringAsync().Result
            End Using

        Catch ex As Exception
            Throw
        End Try

    End Function

    Public Class clsBeEntidadAjusteSAPCumbre
        Public Property Bodega As String
        Public Property RemarkCode As String
        Public Property NombreUsuario As String
        Public Property Entries As List(Of Entry)
    End Class

    Public Class Entry
        Public Property ItemCode As String
        Public Property Cantidad As Integer
    End Class


    Public Sub Sync_Ajustes(ByVal lblprg As RichTextBox,
                            ByRef prg As System.Windows.Forms.ProgressBar)

        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim TransLog As SqlTransaction = Nothing
        Dim Resultado As String = ""
        Dim lAjustesInventario As New List(Of clsBeAjustesMI3)

        Try

            CnnLog.Open() : TransLog = CnnLog.BeginTransaction(IsolationLevel.ReadUncommitted)

            clsPublic.Actualizar_Progreso(lblprg, "Consultando ajustes pendientes de envío.")

            Dim lAjustesPendEnvioInv As New List(Of clsBeAjustesMI3)
            lAjustesPendEnvioInv = clsLnI_nav_transacciones_out.Get_Ajustes_Auditados_Pendientes_Envio_MI3_By_Inventario(Resultado, CnnLog, TransLog)

            If Not lAjustesPendEnvioInv Is Nothing AndAlso lAjustesPendEnvioInv.Count > 0 Then

                Dim BeConfigEnc As clsBeI_nav_config_enc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface, CnnLog, TransLog)
                Dim BeBodega As clsBeBodega = clsLnBodega.GetSingle_By_Idbodega(BeConfigEnc.Idbodega, CnnLog, TransLog)

                Dim vCuentaIngresos As String = BeBodega.Cuenta_Ingreso_Mercancias
                Dim vCuentaEgresos As String = BeBodega.Cuenta_Egreso_Mercancias

                URLServicioEntrada = BD.Instancia.URL_ENTRADA_AJUSTE_POST
                URLServicioSalida = BD.Instancia.URL_SALIDA_AJUSTE_POST

                ' Agrupar ajustes por IdAjusteEnc
                Dim ajustesAgrupados = lAjustesPendEnvioInv.GroupBy(Function(a) a.IdAjusteEnc)

                prg.Maximum = lAjustesPendEnvioInv.Count

                For Each grupo In ajustesAgrupados
                    ' Cada grupo contiene los ajustes con el mismo IdAjusteEnc
                    Dim IdAjusteEncActual As Integer = grupo.Key

                    ' Agrupar los productos dentro del grupo por tipo de ajuste
                    Dim ajustesPositivos = New List(Of Object)()
                    Dim ajustesNegativos = New List(Of Object)()

                    For Each ajuste In grupo
                        Dim entry = New With {
                        .ItemCode = ajuste.Codigo_Producto,
                        .Cantidad = ajuste.Cantidad
                    }

                        If ajuste.TipoAjusteWMS = "Ajuste Positivo" Then
                            ajustesPositivos.Add(entry)
                            clsPublic.Actualizar_Progreso(lblprg, "Agregando producto a ajuste positivo: " & ajuste.Codigo_Producto)
                        ElseIf ajuste.TipoAjusteWMS = "Ajuste Negativo" Then
                            ajustesNegativos.Add(entry)
                            clsPublic.Actualizar_Progreso(lblprg, "Agregando producto a ajuste negativo: " & ajuste.Codigo_Producto)
                        End If
                    Next

                    ' Enviar un solo documento para todos los productos positivos en el grupo actual
                    If ajustesPositivos.Count > 0 Then
                        Dim entradaBody = New With {
                        .Bodega = grupo.First().Codigo_Bodega,
                        .RemarkCode = grupo.First().Codigo_Centro_Costo,
                        .NombreUsuario = "TOMWMS",
                        .Entries = ajustesPositivos
                    }
                        Dim entradaJson = JsonConvert.SerializeObject(entradaBody)
                        Dim entradaResponse = SendPostRequest(URLServicioEntrada, entradaJson)

                        clsPublic.Actualizar_Progreso(lblprg, "Ajuste positivo enviado a SAP para IdAjusteEnc: " & IdAjusteEncActual)
                        clsPublic.Actualizar_Progreso(lblprg, entradaResponse.ToString())

                        ' Marcar ajustes positivos como enviados
                        If entradaResponse.ToString.Contains("""status"":1") Then
                            For Each ajuste In grupo.Where(Function(x) x.TipoAjusteWMS = "Ajuste Positivo")
                                ajuste.Observacion = "Enviado"
                            Next
                        End If
                    End If

                    ' Enviar un solo documento para todos los productos negativos en el grupo actual
                    If ajustesNegativos.Count > 0 Then
                        Dim salidaBody = New With {
                        .Bodega = grupo.First().Codigo_Bodega,
                        .RemarkCode = grupo.First().Codigo_Centro_Costo,
                        .NombreUsuario = "TOMWMS",
                        .Entries = ajustesNegativos
                    }
                        Dim salidaJson = JsonConvert.SerializeObject(salidaBody)
                        Dim salidaResponse = SendPostRequest(URLServicioSalida, salidaJson)

                        clsPublic.Actualizar_Progreso(lblprg, "Ajuste negativo enviado a SAP para IdAjusteEnc: " & IdAjusteEncActual)
                        clsPublic.Actualizar_Progreso(lblprg, salidaResponse.ToString())

                        ' Marcar ajustes negativos como enviados
                        Dim SalidaToParse = salidaResponse
                        If salidaResponse.ToString.Contains("""status"":1") Then
                            For Each ajuste In grupo.Where(Function(x) x.TipoAjusteWMS = "Ajuste Negativo")
                                ajuste.Observacion = "Enviado"
                            Next
                        End If
                    End If
                Next

                Dim vIdStock As Integer = 0
                Dim vIdnventarioEnc As Integer = 0
                ' Actualizar ajustes como enviados en la base de datos
                For Each grupo In ajustesAgrupados
                    For Each ajuste In grupo
                        If ajuste.Observacion.Contains("Enviado") Then
                            vIdStock = clsLnTrans_ajuste_det.Get_IdStock_By_IdAjusteDet(ajuste.IdAjusteDet, CnnLog, TransLog)
                            vIdnventarioEnc = clsLnTrans_ajuste_det.Get_IdInventarioEnc_By_IdAjusteDet(ajuste.IdAjusteDet, CnnLog, TransLog)
                            clsLnTrans_ajuste_det.Actualizar_Estado_Enviado_A_ERP_By_Inventario(vIdStock,
                                                                                                vIdnventarioEnc,
                                                                                                True,
                                                                                                CnnLog,
                                                                                                TransLog)
                        End If
                    Next
                Next

                Try
                    Dim lAjustes As List(Of Integer) = lAjustesPendEnvioInv.Select(Function(a) a.IdAjusteEnc).Distinct.ToList
                    clsLnTrans_ajuste_enc.Actualizar_Estado_Enviado_A_ERP_All(lAjustes, CnnLog, TransLog)
                Catch ex As Exception

                End Try


                clsPublic.Actualizar_Progreso(lblprg, "Fin de sincronización de ajustes por inventario.")

            Else
                clsPublic.Actualizar_Progreso(lblprg, "No hay ajustes por inventario pendientes de envío.")
            End If

            Dim lAjustesPendEnvio As New List(Of clsBeAjustesMI3)
            lAjustesPendEnvio = clsLnI_nav_transacciones_out.Get_Ajustes_Auditados_Pendientes_Envio_MI3(Resultado, CnnLog, TransLog)

            If Not lAjustesPendEnvio Is Nothing AndAlso lAjustesPendEnvio.Count > 0 Then

                Dim BeConfigEnc As clsBeI_nav_config_enc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface, CnnLog, TransLog)
                Dim BeBodega As clsBeBodega = clsLnBodega.GetSingle_By_Idbodega(BeConfigEnc.Idbodega, CnnLog, TransLog)

                Dim vCuentaIngresos As String = BeBodega.Cuenta_Ingreso_Mercancias
                Dim vCuentaEgresos As String = BeBodega.Cuenta_Egreso_Mercancias

                URLServicioEntrada = BD.Instancia.URL_ENTRADA_AJUSTE_POST
                URLServicioSalida = BD.Instancia.URL_SALIDA_AJUSTE_POST

                ' Agrupar ajustes por IdAjusteEnc
                Dim ajustesAgrupados = lAjustesPendEnvio.GroupBy(Function(a) a.IdAjusteEnc)

                prg.Maximum = lAjustesPendEnvio.Count

                For Each grupo In ajustesAgrupados
                    ' Cada grupo contiene los ajustes con el mismo IdAjusteEnc
                    Dim IdAjusteEncActual As Integer = grupo.Key

                    ' Agrupar los productos dentro del grupo por tipo de ajuste
                    Dim ajustesPositivos = New List(Of Object)()
                    Dim ajustesNegativos = New List(Of Object)()
                    Dim BeLogError = New clsBeLog_error_wms

                    For Each ajuste In grupo

                        Dim entry = New With {
                        .ItemCode = ajuste.Codigo_Producto,
                        .Cantidad = ajuste.Cantidad}

                        If ajuste.TipoAjusteWMS = "Ajuste Positivo" Then
                            ajustesPositivos.Add(entry)
                            clsPublic.Actualizar_Progreso(lblprg, "Agregando producto a ajuste positivo: " & ajuste.Codigo_Producto)
                        ElseIf ajuste.TipoAjusteWMS = "Ajuste Negativo" Then
                            ajustesNegativos.Add(entry)
                            clsPublic.Actualizar_Progreso(lblprg, "Agregando producto a ajuste negativo: " & ajuste.Codigo_Producto)
                        End If

                        '#CKFK20250206 Agregué este log para poder saber que enviamos a SAP y validar que
                        'estamos enviando todos los productos
                        Try

                            BeLogError = New clsBeLog_error_wms

                            BeLogError.IdError = clsLnLog_error_wms.MaxID(CnnLog, TransLog)
                            BeLogError.IdEmpresa = 0
                            BeLogError.IdBodega = lAjustesPendEnvio(0).Codigo_Bodega
                            BeLogError.Fecha = Now
                            BeLogError.MensajeError = "Ajuste: " & ajuste.IdAjusteEnc & " Tipo: " & ajuste.TipoAjusteWMS & " Producto: " & ajuste.Codigo_Producto & " Cantidad ajuste: " & ajuste.Cantidad
                            BeLogError.IdPedidoEnc = 0
                            BeLogError.IdPickingEnc = 0
                            BeLogError.IdRecepcionEnc = 0
                            BeLogError.IdUsuarioAgr = 1
                            BeLogError.Cantidad = ajuste.Cantidad

                            clsLnLog_error_wms.Insertar(BeLogError)

                        Catch ex As Exception

                        End Try

                    Next

                    ' Enviar un solo documento para todos los productos positivos en el grupo actual
                    If ajustesPositivos.Count > 0 Then
                        Dim entradaBody = New With {
                        .Bodega = grupo.First().Codigo_Bodega,
                        .RemarkCode = grupo.First().Codigo_Centro_Costo,
                        .NombreUsuario = "TOMWMS",
                        .Entries = ajustesPositivos
                    }
                        Dim entradaJson = JsonConvert.SerializeObject(entradaBody)
                        Dim entradaResponse = SendPostRequest(URLServicioEntrada, entradaJson)

                        clsPublic.Actualizar_Progreso(lblprg, "Ajuste positivo enviado a SAP para IdAjusteEnc: " & IdAjusteEncActual)
                        clsPublic.Actualizar_Progreso(lblprg, entradaResponse.ToString())

                        ' Marcar ajustes positivos como enviados
                        If entradaResponse.ToString.Contains("""status"":1") Then
                            For Each ajuste In grupo.Where(Function(x) x.TipoAjusteWMS = "Ajuste Positivo")
                                ajuste.Observacion = "Enviado"
                            Next
                        End If
                    End If

                    ' Enviar un solo documento para todos los productos negativos en el grupo actual
                    If ajustesNegativos.Count > 0 Then
                        Dim salidaBody = New With {
                        .Bodega = grupo.First().Codigo_Bodega,
                        .RemarkCode = grupo.First().Codigo_Centro_Costo,
                        .NombreUsuario = "TOMWMS",
                        .Entries = ajustesNegativos
                    }
                        Dim salidaJson = JsonConvert.SerializeObject(salidaBody)
                        Dim salidaResponse = SendPostRequest(URLServicioSalida, salidaJson)

                        clsPublic.Actualizar_Progreso(lblprg, "Ajuste negativo enviado a SAP para IdAjusteEnc: " & IdAjusteEncActual)
                        clsPublic.Actualizar_Progreso(lblprg, salidaResponse.ToString())

                        ' Marcar ajustes negativos como enviados
                        Dim SalidaToParse = salidaResponse
                        If salidaResponse.ToString.Contains("""status"":1") Then
                            For Each ajuste In grupo.Where(Function(x) x.TipoAjusteWMS = "Ajuste Negativo")
                                ajuste.Observacion = "Enviado"
                            Next
                        End If
                    End If
                Next

                ' Actualizar ajustes como enviados en la base de datos
                For Each grupo In ajustesAgrupados
                    For Each ajuste In grupo
                        If ajuste.Observacion.Contains("Enviado") Then
                            clsLnTrans_ajuste_det.Actualizar_Estado_Enviado_A_ERP(ajuste.IdAjusteDet,
                                                                                  True,
                                                                                  CnnLog,
                                                                                  TransLog)
                        End If
                    Next
                Next

                Try
                    Dim lAjustes As List(Of Integer) = lAjustesPendEnvio.Select(Function(a) a.IdAjusteEnc).Distinct.ToList
                    clsLnTrans_ajuste_enc.Actualizar_Estado_Enviado_A_ERP_All(lAjustes, CnnLog, TransLog)
                Catch ex As Exception

                End Try

                clsPublic.Actualizar_Progreso(lblprg, "Fin de sincronización de ajustes.")

                TransLog.Commit()

            Else
                clsPublic.Actualizar_Progreso(lblprg, "No hay ajustes pendientes de envío.")
            End If

        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al enviar ajustes a SAP: {0}{1}", vbNewLine, ex.Message))
            If Not TransLog Is Nothing Then TransLog.Rollback()

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "Sync_Ajustes",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet,
                                                       CnnLog)

            Throw New Exception(String.Format(" (M) {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))

        Finally
            If Not CnnLog Is Nothing AndAlso CnnLog.State = ConnectionState.Open Then CnnLog.Close()
        End Try

    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)        
    End Sub

#End Region

End Class
