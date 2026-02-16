Imports System.Data.SqlClient
Imports System.Reflection
Public Class clsLnTrans_ubic_hh_det

    Public Shared Sub Cargar(ByRef oBeTrans_ubic_hh_det As clsBeTrans_ubic_hh_det, ByRef dr As DataRow)
        Try
            With oBeTrans_ubic_hh_det
                .IdTareaUbicacionEnc = IIf(IsDBNull(dr.Item("IdTareaUbicacionEnc")), 0, dr.Item("IdTareaUbicacionEnc"))
                .IdTareaUbicacionDet = IIf(IsDBNull(dr.Item("IdTareaUbicacionDet")), 0, dr.Item("IdTareaUbicacionDet"))
                .IdStock = IIf(IsDBNull(dr.Item("IdStock")), 0, dr.Item("IdStock"))
                .IdUbicacionOrigen = IIf(IsDBNull(dr.Item("IdUbicacionOrigen")), 0, dr.Item("IdUbicacionOrigen"))
                .IdUbicacionDestino = IIf(IsDBNull(dr.Item("IdUbicacionDestino")), 0, dr.Item("IdUbicacionDestino"))
                .IdEstadoOrigen = IIf(IsDBNull(dr.Item("IdEstadoOrigen")), 0, dr.Item("IdEstadoOrigen"))
                .IdEstadoDestino = IIf(IsDBNull(dr.Item("IdEstadoDestino")), 0, dr.Item("IdEstadoDestino"))
                .IdOperadorBodega = IIf(IsDBNull(dr.Item("IdOperadorBodega")), 0, dr.Item("IdOperadorBodega"))
                .HoraInicio = IIf(IsDBNull(dr.Item("HoraInicio")), Date.Now, dr.Item("HoraInicio"))
                .HoraFin = IIf(IsDBNull(dr.Item("HoraFin")), Date.Now, dr.Item("HoraFin"))
                .Realizado = IIf(IsDBNull(dr.Item("Realizado")), False, dr.Item("Realizado"))
                .Cantidad = IIf(IsDBNull(dr.Item("cantidad")), 0.0, dr.Item("cantidad"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Recibido = IIf(IsDBNull(dr.Item("recibido")), 0.0, dr.Item("recibido"))
                .Estado = IIf(IsDBNull(dr.Item("estado")), "", dr.Item("estado"))
                .Atributo_variante_1 = IIf(IsDBNull(dr.Item("atributo_variante_1")), "", dr.Item("atributo_variante_1"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .No_Linea = IIf(IsDBNull(dr.Item("No_Linea")), 0, dr.Item("No_Linea"))
            End With
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_ubic_hh_det As clsBeTrans_ubic_hh_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_ubic_hh_det")
            Ins.Add("idtareaubicacionenc", "@idtareaubicacionenc", DataType.Parametro)
            Ins.Add("idtareaubicaciondet", "@idtareaubicaciondet", DataType.Parametro)
            Ins.Add("idstock", "@idstock", DataType.Parametro)
            If oBeTrans_ubic_hh_det.IdUbicacionOrigen <> 0 Then Ins.Add("idubicacionorigen", "@idubicacionorigen", DataType.Parametro)
            Ins.Add("idubicaciondestino", "@idubicaciondestino", DataType.Parametro)
            Ins.Add("idestadoorigen", "@idestadoorigen", DataType.Parametro)
            Ins.Add("idestadodestino", "@idestadodestino", DataType.Parametro)
            If oBeTrans_ubic_hh_det.IdOperadorBodega <> 0 Then Ins.Add("IdOperadorBodega", "@IdOperadorBodega", DataType.Parametro)
            Ins.Add("horainicio", "@horainicio", DataType.Parametro)
            Ins.Add("horafin", "@horafin", DataType.Parametro)
            Ins.Add("realizado", "@realizado", DataType.Parametro)
            Ins.Add("cantidad", "@cantidad", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("recibido", "@recibido", DataType.Parametro)
            Ins.Add("estado", "@estado", DataType.Parametro)
            Ins.Add("atributo_variante_1", "@atributo_variante_1", DataType.Parametro)
            Ins.Add("IdBodega", "@IdBodega", DataType.Parametro)
            Ins.Add("no_linea", "@no_linea", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTAREAUBICACIONENC", oBeTrans_ubic_hh_det.IdTareaUbicacionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDTAREAUBICACIONDET", oBeTrans_ubic_hh_det.IdTareaUbicacionDet))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeTrans_ubic_hh_det.IdStock))
            If oBeTrans_ubic_hh_det.IdUbicacionOrigen <> 0 Then cmd.Parameters.Add(New SqlParameter("@IDUBICACIONORIGEN", oBeTrans_ubic_hh_det.IdUbicacionOrigen))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONDESTINO", oBeTrans_ubic_hh_det.IdUbicacionDestino))
            cmd.Parameters.Add(New SqlParameter("@IDESTADOORIGEN", oBeTrans_ubic_hh_det.IdEstadoOrigen))
            cmd.Parameters.Add(New SqlParameter("@IDESTADODESTINO", oBeTrans_ubic_hh_det.IdEstadoDestino))
            If oBeTrans_ubic_hh_det.IdOperadorBodega <> 0 Then cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA", oBeTrans_ubic_hh_det.IdOperadorBodega))
            cmd.Parameters.Add(New SqlParameter("@HORAINICIO", oBeTrans_ubic_hh_det.HoraInicio))
            cmd.Parameters.Add(New SqlParameter("@HORAFIN", oBeTrans_ubic_hh_det.HoraFin))
            cmd.Parameters.Add(New SqlParameter("@REALIZADO", oBeTrans_ubic_hh_det.Realizado))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_ubic_hh_det.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_ubic_hh_det.Activo))
            cmd.Parameters.Add(New SqlParameter("@RECIBIDO", oBeTrans_ubic_hh_det.Recibido))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeTrans_ubic_hh_det.Estado))
            cmd.Parameters.Add(New SqlParameter("@ATRIBUTO_VARIANTE_1", oBeTrans_ubic_hh_det.Atributo_variante_1))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_ubic_hh_det.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@NO_LINEA", oBeTrans_ubic_hh_det.No_Linea))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeTrans_ubic_hh_det As clsBeTrans_ubic_hh_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_ubic_hh_det")
            Upd.Add("idtareaubicacionenc", "@idtareaubicacionenc", DataType.Parametro)
            Upd.Add("idtareaubicaciondet", "@idtareaubicaciondet", DataType.Parametro)
            Upd.Add("idstock", "@idstock", DataType.Parametro)
            If oBeTrans_ubic_hh_det.IdUbicacionOrigen <> 0 Then Upd.Add("idubicacionorigen", "@idubicacionorigen", DataType.Parametro)
            Upd.Add("idubicaciondestino", "@idubicaciondestino", DataType.Parametro)
            Upd.Add("idestadoorigen", "@idestadoorigen", DataType.Parametro)
            Upd.Add("idestadodestino", "@idestadodestino", DataType.Parametro)
            If oBeTrans_ubic_hh_det.IdOperadorBodega <> 0 Then Upd.Add("idoperadorbodega", "@idoperadorbodega", DataType.Parametro)
            Upd.Add("horainicio", "@horainicio", DataType.Parametro)
            Upd.Add("horafin", "@horafin", DataType.Parametro)
            Upd.Add("realizado", "@realizado", DataType.Parametro)
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("recibido", "@recibido", DataType.Parametro)
            Upd.Add("estado", "@estado", DataType.Parametro)
            Upd.Add("atributo_variante_1", "@atributo_variante_1", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("no_linea", "@no_linea", DataType.Parametro)
            Upd.Where("IdTareaUbicacionEnc = @IdTareaUbicacionEnc" &
                " AND IdTareaUbicacionDet = @IdTareaUbicacionDet")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTAREAUBICACIONENC", oBeTrans_ubic_hh_det.IdTareaUbicacionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDTAREAUBICACIONDET", oBeTrans_ubic_hh_det.IdTareaUbicacionDet))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeTrans_ubic_hh_det.IdStock))
            If oBeTrans_ubic_hh_det.IdUbicacionOrigen <> 0 Then cmd.Parameters.Add(New SqlParameter("@IDUBICACIONORIGEN", oBeTrans_ubic_hh_det.IdUbicacionOrigen))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONDESTINO", oBeTrans_ubic_hh_det.IdUbicacionDestino))
            cmd.Parameters.Add(New SqlParameter("@IDESTADOORIGEN", oBeTrans_ubic_hh_det.IdEstadoOrigen))
            cmd.Parameters.Add(New SqlParameter("@IDESTADODESTINO", oBeTrans_ubic_hh_det.IdEstadoDestino))
            If oBeTrans_ubic_hh_det.IdOperadorBodega <> 0 Then cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA", oBeTrans_ubic_hh_det.IdOperadorBodega))
            cmd.Parameters.Add(New SqlParameter("@HORAINICIO", oBeTrans_ubic_hh_det.HoraInicio))
            cmd.Parameters.Add(New SqlParameter("@HORAFIN", oBeTrans_ubic_hh_det.HoraFin))
            cmd.Parameters.Add(New SqlParameter("@REALIZADO", oBeTrans_ubic_hh_det.Realizado))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_ubic_hh_det.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_ubic_hh_det.Activo))
            cmd.Parameters.Add(New SqlParameter("@RECIBIDO", oBeTrans_ubic_hh_det.Recibido))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeTrans_ubic_hh_det.Estado))
            cmd.Parameters.Add(New SqlParameter("@ATRIBUTO_VARIANTE_1", oBeTrans_ubic_hh_det.Atributo_variante_1))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_ubic_hh_det.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@NO_LINEA", oBeTrans_ubic_hh_det.No_Linea))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Eliminar(ByRef oBeTrans_ubic_hh_det As clsBeTrans_ubic_hh_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_ubic_hh_det" &
             "  Where(IdTareaUbicacionEnc = @IdTareaUbicacionEnc)" &
             "  AND (IdTareaUbicacionDet = @IdTareaUbicacionDet)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTAREAUBICACIONENC", oBeTrans_ubic_hh_det.IdTareaUbicacionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDTAREAUBICACIONDET", oBeTrans_ubic_hh_det.IdTareaUbicacionDet))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try
    End Function

    Public Shared Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_ubic_hh_det"
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Trans_ubic_hh_det"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeTrans_ubic_hh_det As clsBeTrans_ubic_hh_det) As Boolean

        Try

            Const sp As String = "SELECT * FROM Trans_ubic_hh_det" &
            " Where(IdTareaUbicacionEnc = @IdTareaUbicacionEnc)" &
            " AND (IdTareaUbicacionDet = @IdTareaUbicacionDet)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTAREAUBICACIONENC", oBeTrans_ubic_hh_det.IdTareaUbicacionEnc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTAREAUBICACIONDET", oBeTrans_ubic_hh_det.IdTareaUbicacionDet))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_ubic_hh_det, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeTrans_ubic_hh_det)

        Try

            Dim lReturnList As New List(Of clsBeTrans_ubic_hh_det)
            Const sp As String = "SELECT * FROM Trans_ubic_hh_det"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_ubic_hh_det As New clsBeTrans_ubic_hh_det

            For Each dr As DataRow In dt.Rows
                vBeTrans_ubic_hh_det = New clsBeTrans_ubic_hh_det
                Cargar(vBeTrans_ubic_hh_det, dr)
                lReturnList.Add(vBeTrans_ubic_hh_det)
            Next

            Return lReturnList

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeTrans_ubic_hh_det As clsBeTrans_ubic_hh_det)

        Try

            Const sp As String = "SELECT * FROM Trans_ubic_hh_det" &
            " Where(IdTareaUbicacionEnc = @IdTareaUbicacionEnc)" &
            " AND (IdTareaUbicacionDet = @IdTareaUbicacionDet)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTAREAUBICACIONENC", pBeTrans_ubic_hh_det.IdTareaUbicacionEnc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTAREAUBICACIONDET", pBeTrans_ubic_hh_det.IdTareaUbicacionDet))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTrans_ubic_hh_det, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdTareaUbicacionEnc),0) FROM Trans_ubic_hh_det"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
                Using lCommand As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CInt(lReturnValue)
                    End If
                End Using
            End Using

            Return lMax

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Guardar_Detalle_Traslado_SAP(ByVal IdTareaUbicHHEnc As Integer,
                                                        ByVal BePedidoCliente As clsBeI_nav_ped_traslado_enc,
                                                        ByVal pIdPropietarioBodega As Integer,
                                                        ByVal BeConfigEnc As clsBeI_nav_config_enc,
                                                        ByVal HostSolicita As String,
                                                        ByRef lBeTransUbicHHDet As List(Of clsBeTrans_ubic_hh_det),
                                                        ByRef lBeTransUbicHHStock As List(Of clsBeTrans_ubic_hh_stock),
                                                        ByRef lConnection As SqlConnection,
                                                        ByVal lTransaction As SqlTransaction,
                                                        ByVal lblprg As RichTextBox) As Boolean
        Dim pBeStockRes As New clsBeStock_res

        Guardar_Detalle_Traslado_SAP = False

        Try

            Dim BeProducto As New clsBeProducto
            Dim IdProductoBodega As Integer = 0
            Dim IdPropietarioBodega As Integer = 0
            Dim vCantidadDisponible As Double = 0
            Dim vCantReservada As Integer = 0
            Dim lStockResOut As New List(Of clsBeStock_res)

            If Not BePedidoCliente Is Nothing Then

                If Not BePedidoCliente.Lineas_Detalle Is Nothing Then

                    If BePedidoCliente.Lineas_Detalle.Count > 0 Then

                        For Each DetalleTraslado In BePedidoCliente.Lineas_Detalle

                            BeProducto = New clsBeProducto()
                            BeProducto = clsLnProducto.Get_Single_By_Codigo(DetalleTraslado.Item_No, lConnection, lTransaction)

                            If Not BeProducto Is Nothing Then

                                IdProductoBodega = clsLnProducto.Get_IdProductoBodega_By_IdProducto_And_IdBodega(BeProducto.IdProducto,
                                                                                                                 BeConfigEnc.Idbodega,
                                                                                                                 lConnection,
                                                                                                                 lTransaction)

                                Dim BeTransUbicHHDet As New clsBeTrans_ubic_hh_det
                                Dim BeTransUbicHHStock As New clsBeTrans_ubic_hh_stock

                                If Not DetalleTraslado.Lotes_Detalle Is Nothing Then

                                    If DetalleTraslado.Lotes_Detalle.Count > 0 Then
                                        'Reservar lotes por stock específico.

                                        For Each LoteSAP In DetalleTraslado.Lotes_Detalle

                                            Dim vIdEstadoDefectoAreaSAPTo As Integer = clsLnProducto_estado.Get_IdEstado_By_Codigo_Area(LoteSAP.WhsTo,
                                                                                                                                              lConnection,
                                                                                                                                              lTransaction)
                                            Dim vIdEstadoDefectoAreaSAPFrom As Integer = clsLnProducto_estado.Get_IdEstado_By_Codigo_Area(LoteSAP.WhsFrom,
                                                                                                                                              lConnection,
                                                                                                                                              lTransaction)

                                            Dim vAreaWMSTo = clsLnBodega_area.Get_IdArea_By_Codigo_Area(LoteSAP.WhsTo,
                                                                                                        lConnection,
                                                                                                        lTransaction)

                                            Dim vAreaWMSFrom = clsLnBodega_area.Get_IdArea_By_Codigo_Area(LoteSAP.WhsFrom,
                                                                                                          lConnection,
                                                                                                          lTransaction)

                                            If vIdEstadoDefectoAreaSAPTo = 0 Then
                                                clsPublic.Actualizar_Progreso(lblprg, $"#ERROR_2024030413003: No está configurado ningún estado con codigo_bodega_erp = {LoteSAP.WhsTo}")
                                                Continue For
                                            End If

                                            pBeStockRes = New clsBeStock_res
                                            pBeStockRes.IdStockRes = 0
                                            pBeStockRes.IdTransaccion = IdTareaUbicHHEnc
                                            pBeStockRes.IdPedidoDet = BeTransUbicHHDet.IdTareaUbicacionDet
                                            pBeStockRes.Indicador = "CEST"
                                            pBeStockRes.añada = 0
                                            pBeStockRes.Cantidad = LoteSAP.Quantity_Base
                                            pBeStockRes.Peso = 0
                                            pBeStockRes.Estado = "PPT" 'Producto pedido por cliente
                                            pBeStockRes.User_agr = BeConfigEnc.IdUsuario
                                            pBeStockRes.Fec_agr = Now
                                            pBeStockRes.User_mod = BeConfigEnc.IdUsuario
                                            pBeStockRes.Fec_mod = Now
                                            pBeStockRes.Host = HostSolicita
                                            pBeStockRes.IdProductoEstado = vIdEstadoDefectoAreaSAPFrom
                                            pBeStockRes.IdPresentacion = DetalleTraslado.Variant_Code 'vIdPresentacion                                            
                                            pBeStockRes.IdPedido = IdTareaUbicHHEnc
                                            pBeStockRes.IdPedidoDet = DetalleTraslado.IdPedidoDet
                                            pBeStockRes.IdProductoBodega = IdProductoBodega
                                            pBeStockRes.IdPropietarioBodega = IdPropietarioBodega
                                            pBeStockRes.IdUnidadMedida = BeProducto.IdUnidadMedidaBasica
                                            pBeStockRes.IdBodega = BeConfigEnc.Idbodega
                                            pBeStockRes.Lote = LoteSAP.Batch_No
                                            pBeStockRes.Fecha_vence = LoteSAP.Expiration_Date

                                            lStockResOut = New List(Of clsBeStock_res)

                                            If clsLnStock_res.Reserva_Stock_From_SAP(pBeStockRes,
                                                                                     0,
                                                                                     HostSolicita,
                                                                                     BeConfigEnc,
                                                                                     vCantidadDisponible,
                                                                                     pIdPropietarioBodega,
                                                                                     lStockResOut,
                                                                                     lConnection,
                                                                                     lTransaction,
                                                                                     vAreaWMSFrom,
                                                                                     DetalleTraslado.Line_No,
                                                                                     DetalleTraslado) Then

                                                clsPublic.Actualizar_Progreso(lblprg, "Si reservó")

                                                If Not lStockResOut Is Nothing Then

                                                    If lStockResOut.Count > 0 Then

                                                        For Each StockResForTras In lStockResOut

                                                            BeTransUbicHHDet = New clsBeTrans_ubic_hh_det

                                                            With BeTransUbicHHDet

                                                                .IdTareaUbicacionEnc = IdTareaUbicHHEnc
                                                                .IdTareaUbicacionDet = MaxID(lConnection, lTransaction) + 1
                                                                .IdStock = StockResForTras.IdStock
                                                                .IdUbicacionOrigen = clsLnBodega_area.Get_IdUbicacion_Recepcion_By_IdArea(vAreaWMSFrom, lConnection, lTransaction)
                                                                .IdUbicacionDestino = clsLnBodega_area.Get_IdUbicacion_Recepcion_By_IdArea(vAreaWMSTo, lConnection, lTransaction)
                                                                .IdEstadoOrigen = vIdEstadoDefectoAreaSAPFrom
                                                                .IdEstadoDestino = vIdEstadoDefectoAreaSAPTo
                                                                .IdOperadorBodega = 0
                                                                .HoraInicio = Now
                                                                .HoraFin = Now
                                                                .Realizado = False
                                                                .Cantidad = StockResForTras.Cantidad
                                                                .Activo = True
                                                                .Recibido = False
                                                                .Estado = "Nuevo"
                                                                .Atributo_variante_1 = ""
                                                                .IdBodega = StockResForTras.IdBodega
                                                                .No_Linea = DetalleTraslado.Line_No

                                                            End With

                                                            Insertar(BeTransUbicHHDet, lConnection, lTransaction)
                                                            lBeTransUbicHHDet.Add(BeTransUbicHHDet)

                                                            clsPublic.Actualizar_Progreso(lblprg, "Reserva generada para IdStock: " & StockResForTras.IdStock)

                                                            BeTransUbicHHStock = New clsBeTrans_ubic_hh_stock

                                                            With BeTransUbicHHStock

                                                                .IdStockTransUbicHHDet = clsLnTrans_ubic_hh_stock.MaxID(lConnection, lTransaction) + 1
                                                                .IdTareaUbicacionEnc = BeTransUbicHHDet.IdTareaUbicacionEnc
                                                                .IdTareaUbicacionDet = BeTransUbicHHDet.IdTareaUbicacionDet
                                                                .IdStock = StockResForTras.IdStock
                                                                .IdPropietarioBodega = StockResForTras.IdPropietarioBodega
                                                                .IdProductoBodega = StockResForTras.IdProductoBodega
                                                                .IdProductoEstado = BeTransUbicHHDet.IdEstadoDestino
                                                                .IdPresentacion = StockResForTras.IdPresentacion
                                                                .IdUnidadMedida = StockResForTras.IdUnidadMedida
                                                                .IdUbicacion = BeTransUbicHHDet.IdUbicacionDestino
                                                                .IdUbicacion_anterior = BeTransUbicHHDet.IdUbicacionOrigen
                                                                .IdRecepcionEnc = StockResForTras.IdRecepcion
                                                                .IdRecepcionDet = 0
                                                                .IdPedidoEnc = 0
                                                                .IdPickingEnc = 0
                                                                .IdDespachoEnc = 0
                                                                .Lote = StockResForTras.Lote
                                                                .Lic_plate = StockResForTras.Lic_plate
                                                                .Serial = StockResForTras.Serial
                                                                .Cantidad = StockResForTras.Cantidad
                                                                .Fecha_ingreso = StockResForTras.Fecha_ingreso
                                                                .Fecha_vence = StockResForTras.Fecha_vence
                                                                .Uds_lic_plate = StockResForTras.Uds_lic_plate
                                                                .No_bulto = StockResForTras.No_bulto
                                                                .Fecha_manufactura = StockResForTras.Fecha_manufactura
                                                                .añada = StockResForTras.añada
                                                                .User_agr = 1
                                                                .Fec_agr = Now
                                                                .User_mod = 1
                                                                .Fec_mod = Now
                                                                .Activo = True
                                                                .Peso = StockResForTras.Peso
                                                                .Temperatura = 0
                                                                .Fecha_mov_hist = Now
                                                                .Atributo_variante_1 = StockResForTras.Atributo_Variante_1

                                                            End With

                                                            clsLnTrans_ubic_hh_stock.Insertar(BeTransUbicHHStock, lConnection, lTransaction)
                                                            lBeTransUbicHHStock.Add(BeTransUbicHHStock)

                                                            clsPublic.Actualizar_Progreso(lblprg, "Reserva generada para IdStock: " & StockResForTras.IdStock)

                                                        Next

                                                    End If

                                                End If

                                                vCantReservada += 1

                                            Else
                                                clsPublic.Actualizar_Progreso(lblprg, "No se logró reservar el producto.")
                                            End If

                                        Next

                                    End If

                                End If

                            Else
                                Throw New Exception("ERROR_202403041030: El código de material: " & DetalleTraslado.Item_No & " no existe en maestro de productos, sincronice el catálogo.")
                            End If

                        Next

                        Guardar_Detalle_Traslado_SAP = vCantReservada > 0

                    End If

                End If

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class