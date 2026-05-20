Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Reflection

Partial Public Class clsLnTrans_pe_det_log_reserva

    Private Shared Function Limpiar_Texto_Log_Reserva(ByVal pTexto As String) As String

        If String.IsNullOrWhiteSpace(pTexto) Then Return ""

        Return pTexto.Replace(vbCr, " ").
                      Replace(vbLf, " ").
                      Replace(vbTab, " ").
                      Trim()

    End Function

    Private Shared Function Extraer_Detalle_No_Reserva(ByVal pMensajeLog As String) As String

        Dim vTexto As String = Limpiar_Texto_Log_Reserva(pMensajeLog)
        Const vMarcador As String = "TIPO_NO_RESERVA="

        Dim vInicio As Integer = vTexto.IndexOf(vMarcador, StringComparison.OrdinalIgnoreCase)
        If vInicio < 0 Then Return vTexto

        Dim vSeparador As Integer = vTexto.IndexOf("|", vInicio, StringComparison.OrdinalIgnoreCase)
        If vSeparador < 0 Then Return vTexto

        Return vTexto.Substring(vSeparador + 1).Trim()

    End Function

    Private Shared Function Valor_Log(ByVal pTexto As String) As String

        Dim vTexto As String = Limpiar_Texto_Log_Reserva(pTexto)
        If String.IsNullOrWhiteSpace(vTexto) Then Return ""

        Return vTexto.Replace("|", "/")

    End Function

    Private Shared Function Valor_Numero_Log(ByVal pValor As Double) As String

        Return pValor.ToString("0.######", CultureInfo.InvariantCulture)

    End Function

    Private Shared Function Valor_Fecha_Log(ByVal pFecha As Date) As String

        If pFecha <= New Date(1900, 1, 1) Then Return ""

        Return pFecha.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)

    End Function

    Private Shared Sub Agregar_Campo_Log(ByVal pPartes As List(Of String),
                                         ByVal pNombre As String,
                                         ByVal pValor As String)

        Dim vValor As String = Valor_Log(pValor)
        If String.IsNullOrWhiteSpace(vValor) Then Exit Sub

        pPartes.Add(pNombre & "=" & vValor)

    End Sub

    Private Shared Function Formatear_Mensaje_Log_Reserva(ByVal pEvento As String,
                                                          ByVal pResultado As String,
                                                          ByVal pBeLogReserva As clsBeTrans_pe_det_log_reserva,
                                                          ByVal pMensajeOrigen As String,
                                                          Optional ByVal pTipoNoReserva As String = "") As String

        Dim vPartes As New List(Of String)

        Agregar_Campo_Log(vPartes, "EVENTO", pEvento)
        Agregar_Campo_Log(vPartes, "RESULTADO", pResultado)
        Agregar_Campo_Log(vPartes, "TIPO_NO_RESERVA", pTipoNoReserva)
        Agregar_Campo_Log(vPartes, "CASO", pBeLogReserva.Caso_Reserva)
        Agregar_Campo_Log(vPartes, "PEDIDO", pBeLogReserva.IdPedidoEnc.ToString(CultureInfo.InvariantCulture))
        Agregar_Campo_Log(vPartes, "DETALLE", pBeLogReserva.IdPedidoDet.ToString(CultureInfo.InvariantCulture))
        Agregar_Campo_Log(vPartes, "LINEA", pBeLogReserva.Line_No.ToString(CultureInfo.InvariantCulture))
        Agregar_Campo_Log(vPartes, "ITEM", pBeLogReserva.Item_No)
        Agregar_Campo_Log(vPartes, "CANTIDAD", Valor_Numero_Log(pBeLogReserva.Cantidad))
        Agregar_Campo_Log(vPartes, "UM", pBeLogReserva.UmBas)
        Agregar_Campo_Log(vPartes, "PRESENTACION", pBeLogReserva.Variant_Code)
        Agregar_Campo_Log(vPartes, "BODEGA", pBeLogReserva.IdBodega.ToString(CultureInfo.InvariantCulture))
        Agregar_Campo_Log(vPartes, "DOCUMENTO", pBeLogReserva.Referencia_Documento)
        Agregar_Campo_Log(vPartes, "IDSTOCK", pBeLogReserva.IdStock.ToString(CultureInfo.InvariantCulture))
        Agregar_Campo_Log(vPartes, "IDSTOCKRES", pBeLogReserva.IdStockRes.ToString(CultureInfo.InvariantCulture))
        Agregar_Campo_Log(vPartes, "FECHA_VENCE", Valor_Fecha_Log(pBeLogReserva.Fecha_Vence))

        If String.Equals(pEvento, "NO_RESERVA", StringComparison.OrdinalIgnoreCase) Then
            Agregar_Campo_Log(vPartes, "MOTIVO", Extraer_Detalle_No_Reserva(pMensajeOrigen))
        Else
            Agregar_Campo_Log(vPartes, "MENSAJE_ORIGEN", pMensajeOrigen)
        End If

        Return String.Join(" | ", vPartes)

    End Function

    Private Shared Sub Registrar_Error_Log_Reserva(ByVal pMetodo As String,
                                                   ByVal pEx As Exception)

        Dim vMsgError As String = String.Format("{0} {1}", pMetodo, pEx.Message)
        clsLnLog_error_wms.Agregar_Error(vMsgError)

    End Sub

    Private Shared Function Extraer_Tipo_No_Reserva(ByVal pMensajeLog As String) As String

        Dim vTexto As String = Limpiar_Texto_Log_Reserva(pMensajeLog)
        Const vMarcador As String = "TIPO_NO_RESERVA="

        Dim vInicio As Integer = vTexto.IndexOf(vMarcador, StringComparison.OrdinalIgnoreCase)
        If vInicio < 0 Then Return "RESERVA_NO_COMPLETADA"

        vInicio += vMarcador.Length
        Dim vFin As Integer = vTexto.IndexOf("|", vInicio, StringComparison.OrdinalIgnoreCase)

        If vFin < 0 Then
            Return vTexto.Substring(vInicio).Trim()
        End If

        Return vTexto.Substring(vInicio, vFin - vInicio).Trim()

    End Function

    Private Shared Function Caso_No_Reserva(ByVal pNombreEscenario As String,
                                            ByVal pMensajeLog As String) As String

        Dim vTipo As String = Extraer_Tipo_No_Reserva(pMensajeLog)
        Dim vEscenario As String = Limpiar_Texto_Log_Reserva(pNombreEscenario)

        If String.IsNullOrWhiteSpace(vEscenario) Then
            Return "NO_RESERVA_MI3:" & vTipo
        End If

        If vEscenario.IndexOf(vTipo, StringComparison.OrdinalIgnoreCase) >= 0 Then
            Return vEscenario
        End If

        Return vEscenario & "|NO_RESERVA:" & vTipo

    End Function

    Private Shared Sub Completar_Datos_Linea_Pedido(ByRef pBeLogReserva As clsBeTrans_pe_det_log_reserva,
                                                    ByVal pBeStockRes As clsBeStock_res,
                                                    ByVal lConnection As SqlConnection,
                                                    ByVal lTransaction As SqlTransaction)

        If pBeLogReserva Is Nothing OrElse pBeStockRes Is Nothing Then Exit Sub
        If pBeStockRes.IdPedidoDet = 0 Then Exit Sub

        Const sp As String = "SELECT TOP 1 det.no_linea, det.codigo_producto, det.nom_unid_med, enc.referencia " &
                             "FROM trans_pe_det det " &
                             "LEFT JOIN trans_pe_enc enc ON det.IdPedidoEnc = enc.IdPedidoEnc " &
                             "WHERE det.IdPedidoDet = @IdPedidoDet " &
                             "AND (@IdPedidoEnc = 0 OR det.IdPedidoEnc = @IdPedidoEnc)"

        Using cmd As New SqlCommand(sp, lConnection, lTransaction)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.AddWithValue("@IdPedidoDet", pBeStockRes.IdPedidoDet)
            cmd.Parameters.AddWithValue("@IdPedidoEnc", pBeStockRes.IdPedido)

            Using dr As SqlDataReader = cmd.ExecuteReader()
                If dr.Read() Then
                    If pBeLogReserva.Line_No = 0 AndAlso Not IsDBNull(dr("no_linea")) Then
                        pBeLogReserva.Line_No = CInt(dr("no_linea"))
                    End If

                    If String.IsNullOrWhiteSpace(pBeLogReserva.Item_No) AndAlso Not IsDBNull(dr("codigo_producto")) Then
                        pBeLogReserva.Item_No = CStr(dr("codigo_producto"))
                    End If

                    If String.IsNullOrWhiteSpace(pBeLogReserva.UmBas) AndAlso Not IsDBNull(dr("nom_unid_med")) Then
                        pBeLogReserva.UmBas = CStr(dr("nom_unid_med"))
                    End If

                    If String.IsNullOrWhiteSpace(pBeLogReserva.Referencia_Documento) AndAlso Not IsDBNull(dr("referencia")) Then
                        pBeLogReserva.Referencia_Documento = CStr(dr("referencia"))
                    End If

                End If
            End Using
        End Using

    End Sub

    Public Shared Sub Agregar_Log_No_Reserva_MI3(ByVal pStockResSolicitud As clsBeStock_res,
                                                 ByVal pBeTrasladoDet As clsBeI_nav_ped_traslado_det,
                                                 ByVal pNombreEscenario As String,
                                                 ByVal pMensajeLog As String,
                                                 ByVal lConnection As SqlConnection,
                                                 ByVal lTransaction As SqlTransaction)

        Try

            If pStockResSolicitud Is Nothing OrElse lConnection Is Nothing OrElse lTransaction Is Nothing Then Exit Sub
            If lConnection.State <> ConnectionState.Open Then Exit Sub

            Dim vMensajeLog As String = Limpiar_Texto_Log_Reserva(pMensajeLog)
            If String.IsNullOrWhiteSpace(vMensajeLog) Then Exit Sub
            Dim vTipoNoReserva As String = Extraer_Tipo_No_Reserva(vMensajeLog)

            Dim oBeLog_reserva_pedido As New clsBeTrans_pe_det_log_reserva()
            oBeLog_reserva_pedido.IdLogReserva = MaxID(lConnection, lTransaction) + 1
            oBeLog_reserva_pedido.Caso_Reserva = Caso_No_Reserva(pNombreEscenario, vMensajeLog)
            oBeLog_reserva_pedido.EsError = True
            oBeLog_reserva_pedido.Fecha = Now
            oBeLog_reserva_pedido.IdBodega = pStockResSolicitud.IdBodega
            oBeLog_reserva_pedido.Cantidad = pStockResSolicitud.Cantidad
            oBeLog_reserva_pedido.IdPedidoEnc = pStockResSolicitud.IdPedido
            oBeLog_reserva_pedido.IdPedidoDet = pStockResSolicitud.IdPedidoDet
            oBeLog_reserva_pedido.Fecha_Vence = If(pStockResSolicitud.Fecha_vence > New Date(1900, 1, 1),
                                                    pStockResSolicitud.Fecha_vence,
                                                    Date.Today)
            oBeLog_reserva_pedido.IdStock = pStockResSolicitud.IdStock
            oBeLog_reserva_pedido.IdStockRes = pStockResSolicitud.IdStockRes

            If pBeTrasladoDet IsNot Nothing Then
                oBeLog_reserva_pedido.Line_No = pBeTrasladoDet.Line_No
                oBeLog_reserva_pedido.Item_No = pBeTrasladoDet.Item_No
                oBeLog_reserva_pedido.UmBas = pBeTrasladoDet.Unit_of_Measure_Code
                oBeLog_reserva_pedido.Variant_Code = pBeTrasladoDet.Variant_Code
                oBeLog_reserva_pedido.Referencia_Documento = pBeTrasladoDet.NoEnc

                If oBeLog_reserva_pedido.Cantidad = 0 Then
                    oBeLog_reserva_pedido.Cantidad = pBeTrasladoDet.Quantity
                End If
            Else
                oBeLog_reserva_pedido.Line_No = pStockResSolicitud.Serial
                oBeLog_reserva_pedido.Variant_Code = pStockResSolicitud.IdPresentacion.ToString()
            End If

            If String.IsNullOrWhiteSpace(oBeLog_reserva_pedido.Item_No) AndAlso pStockResSolicitud.IdProductoBodega > 0 Then
                oBeLog_reserva_pedido.Item_No = clsLnProducto.Get_Codigo_By_IdProductoBodega(pStockResSolicitud.IdProductoBodega,
                                                                                              lConnection,
                                                                                              lTransaction)
            End If

            If String.IsNullOrWhiteSpace(oBeLog_reserva_pedido.UmBas) AndAlso pStockResSolicitud.IdUnidadMedida > 0 Then
                oBeLog_reserva_pedido.UmBas = clsLnUnidad_medida.Get_Nombre_By_IdUnidadMedida(pStockResSolicitud.IdUnidadMedida,
                                                                                               lConnection,
                                                                                               lTransaction)
            End If

            Completar_Datos_Linea_Pedido(oBeLog_reserva_pedido,
                                         pStockResSolicitud,
                                         lConnection,
                                         lTransaction)
            oBeLog_reserva_pedido.MensajeLog = Formatear_Mensaje_Log_Reserva("NO_RESERVA",
                                                                              "NO_RESERVADO",
                                                                              oBeLog_reserva_pedido,
                                                                              vMensajeLog,
                                                                              vTipoNoReserva)

            If Not Existe_By_Parametros(oBeLog_reserva_pedido.IdPedidoEnc,
                                        oBeLog_reserva_pedido.IdPedidoDet,
                                        oBeLog_reserva_pedido.IdStock,
                                        oBeLog_reserva_pedido.Cantidad,
                                        oBeLog_reserva_pedido.Caso_Reserva,
                                        lConnection,
                                        lTransaction) Then

                Insertar(oBeLog_reserva_pedido, lConnection, lTransaction)

            End If

        Catch ex As Exception
            Registrar_Error_Log_Reserva(MethodBase.GetCurrentMethod.Name(), ex)
        End Try

    End Sub

    Public Shared Sub Agregar_Log_Reserva(ByVal pBeStockAReservar As clsBeStock_res,
                                          ByVal pNombreEscenario As String,
                                          ByVal pMensajeLog As String)

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            If lConnection.State = ConnectionState.Open Then

                Dim vMensajeLog As String = Limpiar_Texto_Log_Reserva(pMensajeLog)
                Dim oBeLog_reserva_pedido As New clsBeTrans_pe_det_log_reserva()
                oBeLog_reserva_pedido.IdLogReserva = MaxID(lConnection, lTransaction) + 1
                oBeLog_reserva_pedido.Caso_Reserva = pNombreEscenario
                oBeLog_reserva_pedido.EsError = False
                oBeLog_reserva_pedido.Fecha = Now
                oBeLog_reserva_pedido.IdBodega = pBeStockAReservar.IdBodega
                oBeLog_reserva_pedido.Cantidad = pBeStockAReservar.Cantidad
                oBeLog_reserva_pedido.Variant_Code = pBeStockAReservar.IdPresentacion
                oBeLog_reserva_pedido.IdPedidoEnc = pBeStockAReservar.IdPedido
                oBeLog_reserva_pedido.IdPedidoDet = pBeStockAReservar.IdPedidoDet
                oBeLog_reserva_pedido.Item_No = clsLnProducto.Get_Codigo_By_IdProductoBodega(pBeStockAReservar.IdProductoBodega, lConnection, lTransaction)
                oBeLog_reserva_pedido.UmBas = clsLnUnidad_medida.Get_Nombre_By_IdUnidadMedida(pBeStockAReservar.IdUnidadMedida, lConnection, lTransaction)
                oBeLog_reserva_pedido.Fecha_Vence = pBeStockAReservar.Fecha_vence
                oBeLog_reserva_pedido.IdStock = pBeStockAReservar.IdStock
                oBeLog_reserva_pedido.IdStockRes = pBeStockAReservar.IdStockRes

                Completar_Datos_Linea_Pedido(oBeLog_reserva_pedido,
                                             pBeStockAReservar,
                                             lConnection,
                                             lTransaction)
                oBeLog_reserva_pedido.MensajeLog = Formatear_Mensaje_Log_Reserva("RESERVA_STOCK",
                                                                                  "RESERVADO",
                                                                                  oBeLog_reserva_pedido,
                                                                                  vMensajeLog)

                If Not Existe_By_Parametros(pBeStockAReservar.IdPedido,
                                            pBeStockAReservar.IdPedidoDet,
                                            pBeStockAReservar.IdStock,
                                            pBeStockAReservar.Cantidad,
                                            pNombreEscenario,
                                            lConnection,
                                            lTransaction) Then

                    Insertar(oBeLog_reserva_pedido, lConnection, lTransaction)

                End If

            End If

            If Not lTransaction Is Nothing Then lTransaction.Commit()

        Catch ex As Exception

            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Registrar_Error_Log_Reserva(MethodBase.GetCurrentMethod.Name(), ex)

        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Sub

    Public Shared Sub Agregar_Log_Reserva(ByVal pBeStockAReservar As clsBeStock_res,
                                          ByVal pNombreEscenario As String,
                                          ByVal lConnection As SqlConnection,
                                          ByVal lTransaction As SqlTransaction)

        Try

            If lConnection.State = ConnectionState.Open Then

                Dim vMensajeLog As String = Limpiar_Texto_Log_Reserva(pNombreEscenario)
                Dim oBeLog_reserva_pedido As New clsBeTrans_pe_det_log_reserva()
                oBeLog_reserva_pedido.IdLogReserva = MaxID(lConnection, lTransaction) + 1
                oBeLog_reserva_pedido.Caso_Reserva = pNombreEscenario
                oBeLog_reserva_pedido.EsError = False
                oBeLog_reserva_pedido.Fecha = Now
                oBeLog_reserva_pedido.IdBodega = pBeStockAReservar.IdBodega
                oBeLog_reserva_pedido.Cantidad = pBeStockAReservar.Cantidad
                oBeLog_reserva_pedido.Variant_Code = pBeStockAReservar.IdPresentacion
                oBeLog_reserva_pedido.IdPedidoEnc = pBeStockAReservar.IdPedido
                oBeLog_reserva_pedido.IdPedidoDet = pBeStockAReservar.IdPedidoDet
                oBeLog_reserva_pedido.Item_No = clsLnProducto.Get_Codigo_By_IdProductoBodega(pBeStockAReservar.IdProductoBodega, lConnection, lTransaction)
                oBeLog_reserva_pedido.UmBas = clsLnUnidad_medida.Get_Nombre_By_IdUnidadMedida(pBeStockAReservar.IdUnidadMedida, lConnection, lTransaction)
                oBeLog_reserva_pedido.Fecha_Vence = pBeStockAReservar.Fecha_vence
                oBeLog_reserva_pedido.IdStock = pBeStockAReservar.IdStock
                oBeLog_reserva_pedido.IdStockRes = pBeStockAReservar.IdStockRes

                Completar_Datos_Linea_Pedido(oBeLog_reserva_pedido,
                                             pBeStockAReservar,
                                             lConnection,
                                             lTransaction)
                oBeLog_reserva_pedido.MensajeLog = Formatear_Mensaje_Log_Reserva("RESERVA_STOCK",
                                                                                  "RESERVADO",
                                                                                  oBeLog_reserva_pedido,
                                                                                  vMensajeLog)

                If Not Existe_By_Parametros(pBeStockAReservar.IdPedido,
                                            pBeStockAReservar.IdPedidoDet,
                                            pBeStockAReservar.IdStock,
                                            pBeStockAReservar.Cantidad,
                                            pNombreEscenario,
                                            lConnection,
                                            lTransaction) Then

                    Insertar(oBeLog_reserva_pedido, lConnection, lTransaction)

                End If

            End If

        Catch ex As Exception
            Registrar_Error_Log_Reserva(MethodBase.GetCurrentMethod.Name(), ex)
        End Try

    End Sub
    Public Shared Sub Existe_By_Parametros(ByVal IdPedidoEnc As Integer,
                                           ByVal IdPedidoDet As Integer,
                                           ByVal IdStock As Integer,
                                           ByVal Cantidad As Double)

        Try

            Const sp As String = "SELECT * FROM Trans_pe_det_log_reserva" &
                                 " Where(IdPedidoEnc = @IdPedidoEnc AND IdPedidoDet = @IdPedidoDet AND IdStock = @IdStock AND Cantidad = @Cantidad)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", IdPedidoEnc)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoDet", IdPedidoDet)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdStock", IdStock)
                        lDTA.SelectCommand.Parameters.AddWithValue("@Cantidad", Cantidad)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_pe_det_log_reserva As New clsBeTrans_pe_det_log_reserva

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTrans_pe_det_log_reserva, lDataTable.Rows(0))
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Public Shared Function MaxID(ByVal lConnection As SqlConnection,
                                 ByVal lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdLogReserva),0) FROM trans_pe_det_log_reserva"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_By_Parametros(ByVal IdPedidoEnc As Integer,
                                                ByVal IdPedidoDet As Integer,
                                                ByVal IdStock As Integer,
                                                ByVal Cantidad As Double,
                                                ByVal Caso_Reserva As String,
                                                ByVal lConnection As SqlConnection,
                                                ByVal lTransaction As SqlTransaction) As Boolean

        Existe_By_Parametros = False

        Try

            Const sp As String = "SELECT * FROM Trans_pe_det_log_reserva" &
                                 " WHERE(IdPedidoEnc = @IdPedidoEnc 
                                   AND IdPedidoDet = @IdPedidoDet 
                                   AND IdStock = @IdStock AND Cantidad = @Cantidad AND Caso_Reserva = @Caso_Reserva)"


            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", IdPedidoEnc)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoDet", IdPedidoDet)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdStock", IdStock)
                lDTA.SelectCommand.Parameters.AddWithValue("@Cantidad", Cantidad)
                lDTA.SelectCommand.Parameters.AddWithValue("@Caso_Reserva", Caso_Reserva)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Existe_By_Parametros = True
                End If

            End Using
        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
