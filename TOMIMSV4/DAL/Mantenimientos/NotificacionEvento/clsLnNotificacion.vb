Imports System.Text
Public Class clsLnNotificacion

    Public Shared Sub Generar_Notificacion_Traslado_Directo_Desde_WMS(ByVal pIdPedidoEnc As Integer,
                                                                      ByVal pUsuario As String)

        Try
            If pIdPedidoEnc <= 0 Then Exit Sub

            Dim oBePedidoEnc As clsBeTrans_pe_enc = clsLnTrans_pe_enc.GetSingle(pIdPedidoEnc, False)

            If oBePedidoEnc Is Nothing Then
                Exit Sub
            End If

            Dim idTipoPedido As Integer = oBePedidoEnc.TipoPedido.IdTipoPedido
            If idTipoPedido <> 6 Then Exit Sub

            Dim codigoEvento As String = "WMS_PE0006_TRASLADO_DIRECTO"

            Dim oBeEvento As clsBENotificacionEvento = clsLnNotificacionEvento.ObtenerPorCodigo(codigoEvento)
            If oBeEvento Is Nothing OrElse oBeEvento.IdEvento <= 0 OrElse Not oBeEvento.Activo Then Exit Sub

            Dim oBePlantilla As clsBENotificacionPlantilla = clsLnNotificacionPlantilla.ObtenerPlantillaActivaPorEvento(oBeEvento.IdEvento, "EMAIL")
            If oBePlantilla Is Nothing OrElse oBePlantilla.IdPlantilla <= 0 OrElse Not oBePlantilla.Activo Then Exit Sub

            Dim oBeLayout As clsBENotificacionLayout = Nothing
            If oBePlantilla.UsaLayoutComun AndAlso oBePlantilla.IdLayout > 0 Then
                oBeLayout = clsLnNotificacionLayout.ObtenerPorId(oBePlantilla.IdLayout)
            End If

            ' Obtener detalles del pedido
            Dim oListaDetalles As List(Of clsBeTrans_pe_det) = oBePedidoEnc.Detalle

            ' Construir variables para reemplazo
            Dim dicVariables As Dictionary(Of String, String) = ConstruirVariablesPE0006(oBePedidoEnc, oListaDetalles, pUsuario)

            ' Reemplazar variables en plantilla
            Dim asuntoFinal As String = ReemplazarVariables(oBePlantilla.AsuntoTemplate, dicVariables)
            Dim bodyFinal As String = ReemplazarVariables(oBePlantilla.BodyHtmlTemplate, dicVariables)

            ' Aplicar layout si corresponde
            Dim htmlFinal As String = bodyFinal
            If oBeLayout IsNot Nothing Then
                htmlFinal = oBeLayout.HeaderHtml & bodyFinal & oBeLayout.FooterHtml
            End If

            ' Obtener ID de bodega destino
            Dim idBodegaDestino As Integer = ObtenerIdBodegaDestino(oBePedidoEnc)

            ' Resolver destinatarios
            Dim dtDestinatarios As DataTable = clsLnNotificacionDestinatarioRegla.ResolverDestinatarios(
                oBeEvento.IdEvento,
                "PE0006",
                oBePedidoEnc.Codigo_Empresa_ERP,
                oBePedidoEnc.RoadSucursal,
                oBePedidoEnc.IdBodega,
                idBodegaDestino
            )

            If dtDestinatarios Is Nothing OrElse dtDestinatarios.Rows.Count = 0 Then
                clsLnLog_error_wms.Agregar_Error("No se resolvieron destinatarios para PE0006. IdPedidoEnc=" & pIdPedidoEnc.ToString())
                Exit Sub
            End If

            ' Obtener correos por tipo
            Dim correosTo As String = ObtenerCorreosPorTipo(dtDestinatarios, "TO")
            Dim correosCc As String = ObtenerCorreosPorTipo(dtDestinatarios, "CC")
            Dim correosBcc As String = ObtenerCorreosPorTipo(dtDestinatarios, "BCC")

            If String.IsNullOrWhiteSpace(correosTo) Then
                clsLnLog_error_wms.Agregar_Error("No se resolvieron destinatarios TO para PE0006. IdPedidoEnc=" & pIdPedidoEnc.ToString())
                Exit Sub
            End If

            ' Crear registro en cola
            Dim oBeCola As New clsBENotificacionCola()
            oBeCola.IdEvento = oBeEvento.IdEvento
            oBeCola.IdPlantilla = oBePlantilla.IdPlantilla
            oBeCola.OrigenSistema = "WMS"
            oBeCola.LlaveNegocio = oBePedidoEnc.No_documento.ToString()
            oBeCola.EmpresaCodigo = oBePedidoEnc.Codigo_Empresa_ERP
            oBeCola.SucursalCodigo = oBePedidoEnc.RoadSucursal
            oBeCola.BodegaCodigo = oBePedidoEnc.Bodega_Origen
            oBeCola.TipoDocumento = "PE0006"
            oBeCola.Referencia1 = pIdPedidoEnc.ToString()
            oBeCola.Referencia2 = asuntoFinal
            oBeCola.DataJson = String.Format("{{""Asunto"":""{0}"",""BodyHtml"":""{1}"",""DestinatariosTo"":""{2}"",""DestinatariosCc"":""{3}"",""DestinatariosBcc"":""{4}""}}",
                                            asuntoFinal.Replace("""", "\"""),
                                            htmlFinal.Replace("""", "\"""),
                                            correosTo,
                                            correosCc,
                                            correosBcc)
            oBeCola.Estado = "PENDIENTE"
            oBeCola.Intentos = 0
            oBeCola.MaxIntentos = 3
            oBeCola.FechaProgramada = DateTime.Now
            oBeCola.UsuarioCreacion = pUsuario
            oBeCola.FechaCreacion = DateTime.Now

            clsLnNotificacionCola.Insertar(oBeCola)

        Catch ex As Exception
            clsLnLog_error_wms.Agregar_Error("Error generando notificación PE0006. " & ex.Message)
            Throw ex
        End Try

    End Sub

    Private Shared Function ConstruirVariablesPE0006(ByVal oBePedidoEnc As clsBeTrans_pe_enc,
                                                     ByVal oListaDetalles As List(Of clsBeTrans_pe_det),
                                                     ByVal pUsuario As String) As Dictionary(Of String, String)

        Dim dic As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)

        ' Datos del encabezado
        dic("{CodigoEmpresa}") = oBePedidoEnc.Codigo_Empresa_ERP
        dic("{NombreTipoPedido}") = "Traslado Directo desde WMS"
        dic("{NumeroDocumento}") = oBePedidoEnc.No_documento.ToString()
        dic("{FechaDocumento}") = oBePedidoEnc.Fecha_Pedido.ToString("yyyy-MM-dd HH:mm")
        dic("{UsuarioGeneracion}") = pUsuario

        ' Datos de bodegas
        dic("{CodigoBodegaOrigen}") = oBePedidoEnc.Bodega_Origen
        dic("{NombreBodegaOrigen}") = ObtenerNombreBodegaPorCodigo(oBePedidoEnc.Bodega_Origen)
        dic("{CodigoBodegaDestino}") = oBePedidoEnc.Bodega_Destino
        dic("{NombreBodegaDestino}") = ObtenerNombreBodegaPorCodigo(oBePedidoEnc.Bodega_Destino)
        dic("{Observaciones}") = oBePedidoEnc.Observacion

        ' Construir tabla detalle HTML
        dic("{TablaDetalleHtml}") = ConstruirTablaDetalleHtmlDesdeLista(oListaDetalles)

        Return dic

    End Function

    Private Shared Function ConstruirTablaDetalleHtmlDesdeLista(ByVal oListaDetalles As List(Of clsBeTrans_pe_det)) As String
        Try
            If oListaDetalles Is Nothing OrElse oListaDetalles.Count = 0 Then
                Return "<table border='1' cellpadding='4' cellspacing='0' style='border-collapse:collapse;font-family:Segoe UI,Arial,sans-serif;font-size:12px;'><tr><td colspan='3'>No hay detalles</td></tr></table>"
            End If

            Dim sb As New StringBuilder()

            sb.AppendLine("<table border='1' cellpadding='4' cellspacing='0' style='border-collapse:collapse;font-family:Segoe UI,Arial,sans-serif;font-size:12px;'>")
            sb.AppendLine("<tr style='background:#f2f2f2;'>")
            sb.AppendLine("    <th>Código</th>")
            sb.AppendLine("    <th>Producto</th>")
            sb.AppendLine("    <th style='text-align:right;'>Cantidad</th>")
            sb.AppendLine("</tr>")

            For Each oDetalle As clsBeTrans_pe_det In oListaDetalles
                sb.AppendLine("<tr>")
                sb.AppendLine($"    <td>{EscapeHtml(oDetalle.Codigo_Producto)}</td>")
                sb.AppendLine($"    <td>{EscapeHtml(oDetalle.Nombre_producto)}</td>")
                sb.AppendLine($"    <td style='text-align:right;'>{oDetalle.Cantidad.ToString("N2")}</td>")
                sb.AppendLine("</tr>")
            Next

            sb.AppendLine("</table>")

            Return sb.ToString()

        Catch ex As Exception
            Return $"<table border='1'><tr><td>Error al generar detalle: {EscapeHtml(ex.Message)}</td></tr></table>"
        End Try
    End Function

    Private Shared Function ObtenerNombreBodegaPorCodigo(ByVal pCodigoBodega As String) As String
        Try
            If String.IsNullOrWhiteSpace(pCodigoBodega) Then
                Return String.Empty
            End If

            Dim oBeBodega As clsBeBodega = clsLnBodega.GetSingle_By_Codigo(pCodigoBodega)

            If oBeBodega IsNot Nothing Then
                Return oBeBodega.Nombre
            End If

            Return pCodigoBodega

        Catch ex As Exception
            Return pCodigoBodega
        End Try
    End Function

    Private Shared Function ObtenerIdBodegaDestino(ByVal oBePedidoEnc As clsBeTrans_pe_enc) As Integer
        Try
            ' Intentar obtener de la propiedad IdBodegaDestino si existe
            Dim propInfo As System.Reflection.PropertyInfo = oBePedidoEnc.GetType().GetProperty("IdBodegaDestino")
            If propInfo IsNot Nothing Then
                Return Convert.ToInt32(propInfo.GetValue(oBePedidoEnc))
            End If

            ' Si no existe, buscar por código
            If Not String.IsNullOrWhiteSpace(oBePedidoEnc.Bodega_Destino) Then
                Dim oBeBodega As clsBeBodega = clsLnBodega.GetSingle_By_Codigo(oBePedidoEnc.Bodega_Destino)
                If oBeBodega IsNot Nothing Then
                    Return oBeBodega.IdBodega
                End If
            End If

            Return 0

        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Shared Function EscapeHtml(ByVal pTexto As String) As String
        If String.IsNullOrEmpty(pTexto) Then
            Return String.Empty
        End If

        Return pTexto.Replace("&", "&amp;") _
                 .Replace("<", "&lt;") _
                 .Replace(">", "&gt;") _
                 .Replace("""", "&quot;") _
                 .Replace("'", "&#39;")
    End Function

    Private Shared Function ReemplazarVariables(ByVal pTexto As String,
                                                ByVal pDic As Dictionary(Of String, String)) As String

        Dim resultado As String = If(pTexto, "")

        If pDic IsNot Nothing AndAlso pDic.Count > 0 Then
            For Each kvp In pDic
                resultado = resultado.Replace(kvp.Key, If(kvp.Value, ""))
            Next
        End If

        Return resultado

    End Function

    Private Shared Function ObtenerCorreosPorTipo(ByVal dt As DataTable,
                                                  ByVal pTipoDestinatario As String) As String

        Dim lst As New List(Of String)

        If dt Is Nothing OrElse dt.Rows.Count = 0 Then Return ""

        For Each dr As DataRow In dt.Rows
            Dim tipo As String = If(IsDBNull(dr("TipoDestinatario")), "", dr("TipoDestinatario").ToString().Trim().ToUpper())
            Dim correo As String = If(IsDBNull(dr("CorreoContacto")), "", dr("CorreoContacto").ToString().Trim())

            If tipo = pTipoDestinatario.Trim().ToUpper() AndAlso Not String.IsNullOrWhiteSpace(correo) Then
                If Not lst.Contains(correo.ToLower()) Then
                    lst.Add(correo.ToLower())
                End If
            End If
        Next

        Return String.Join(";", lst.ToArray())

    End Function

End Class