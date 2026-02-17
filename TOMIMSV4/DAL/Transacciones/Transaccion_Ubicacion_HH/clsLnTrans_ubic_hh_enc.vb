Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_ubic_hh_enc

    Public Shared Sub Cargar(ByRef oBeTrans_ubic_hh_enc As clsBeTrans_ubic_hh_enc, ByRef dr As DataRow)

        Try

            With oBeTrans_ubic_hh_enc

                .IdTareaUbicacionEnc = IIf(IsDBNull(dr.Item("IdTareaUbicacionEnc")), 0, dr.Item("IdTareaUbicacionEnc"))
                .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                .IdMotivoUbicacion = IIf(IsDBNull(dr.Item("IdMotivoUbicacion")), 0, dr.Item("IdMotivoUbicacion"))
                .FechaInicio = IIf(IsDBNull(dr.Item("FechaInicio")), Nothing, dr.Item("FechaInicio"))
                .HoraInicio = IIf(IsDBNull(dr.Item("HoraInicio")), Date.Now, dr.Item("HoraInicio"))
                .FechaFin = IIf(IsDBNull(dr.Item("FechaFin")), Nothing, dr.Item("FechaFin"))
                .HoraFin = IIf(IsDBNull(dr.Item("HoraFin")), Date.Now, dr.Item("HoraFin"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Observacion = IIf(IsDBNull(dr.Item("Observacion")), "", dr.Item("Observacion"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Operador_por_linea = IIf(IsDBNull(dr.Item("operador_por_linea")), False, dr.Item("operador_por_linea"))
                .Ubicacion_con_hh = IIf(IsDBNull(dr.Item("ubicacion_con_hh")), False, dr.Item("ubicacion_con_hh"))
                .Estado = IIf(IsDBNull(dr.Item("estado")), "", dr.Item("estado"))
                .Cambio_estado = IIf(IsDBNull(dr.Item("cambio_estado")), False, dr.Item("cambio_estado"))
                .IdReabastecimientoLog = IIf(IsDBNull(dr.Item("IdReabastecimientoLog")), 0, dr.Item("IdReabastecimientoLog"))

                If dr.Table.Columns.Contains("Nombre_Operador") Then .Nombre_Operador = IIf(IsDBNull(dr.Item("Nombre_Operador")), "", dr.Item("Nombre_Operador"))

                .Es_Traslado_SAP = IIf(IsDBNull(dr.Item("es_traslado_sap")), False, dr.Item("es_traslado_sap"))
                .No_Documento = IIf(IsDBNull(dr.Item("no_documento")), "", dr.Item("no_documento"))
                If dr.Table.Columns.Contains("Usuario") Then .Usuario = IIf(IsDBNull(dr.Item("Usuario")), "", dr.Item("Usuario"))
                If dr.Table.Columns.Contains("Rol") Then .Rol = IIf(IsDBNull(dr.Item("Rol")), "", dr.Item("Rol"))

            End With

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            '#MECR03112025: Se agrego bitacora de ubicacion
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_ubic.Agregar_Error(vMsgError,
                                                  pStackTrace:=ex.StackTrace,
                                                  pIdBodega:=oBeTrans_ubic_hh_enc.IdBodega,
                                                  pIdTareaUbicacionEnc:=oBeTrans_ubic_hh_enc.IdTareaUbicacionEnc,
                                                  pIdMotivoUbicacion:=oBeTrans_ubic_hh_enc.IdMotivoUbicacion)

            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_ubic_hh_enc As clsBeTrans_ubic_hh_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_ubic_hh_enc")
            Ins.Add("idtareaubicacionenc", "@idtareaubicacionenc", DataType.Parametro)
            Ins.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Ins.Add("idmotivoubicacion", "@idmotivoubicacion", DataType.Parametro)
            Ins.Add("fechainicio", "@fechainicio", DataType.Parametro)
            Ins.Add("horainicio", "@horainicio", DataType.Parametro)
            Ins.Add("fechafin", "@fechafin", DataType.Parametro)
            Ins.Add("horafin", "@horafin", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("observacion", "@observacion", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("operador_por_linea", "@operador_por_linea", DataType.Parametro)
            Ins.Add("ubicacion_con_hh", "@ubicacion_con_hh", DataType.Parametro)
            Ins.Add("estado", "@estado", DataType.Parametro)
            Ins.Add("cambio_estado", "@cambio_estado", DataType.Parametro)
            Ins.Add("es_traslado_sap", "@es_traslado_sap", DataType.Parametro)
            Ins.Add("no_documento", "@no_documento", DataType.Parametro)
            Ins.Add("idreabastecimientolog", "@idreabastecimientolog", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTAREAUBICACIONENC", oBeTrans_ubic_hh_enc.IdTareaUbicacionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_ubic_hh_enc.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDMOTIVOUBICACION", oBeTrans_ubic_hh_enc.IdMotivoUbicacion))
            cmd.Parameters.Add(New SqlParameter("@FECHAINICIO", oBeTrans_ubic_hh_enc.FechaInicio))
            cmd.Parameters.Add(New SqlParameter("@HORAINICIO", oBeTrans_ubic_hh_enc.HoraInicio))
            cmd.Parameters.Add(New SqlParameter("@FECHAFIN", oBeTrans_ubic_hh_enc.FechaFin))
            cmd.Parameters.Add(New SqlParameter("@HORAFIN", oBeTrans_ubic_hh_enc.HoraFin))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_ubic_hh_enc.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_ubic_hh_enc.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_ubic_hh_enc.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_ubic_hh_enc.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACION", oBeTrans_ubic_hh_enc.Observacion))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_ubic_hh_enc.Activo))
            cmd.Parameters.Add(New SqlParameter("@OPERADOR_POR_LINEA", oBeTrans_ubic_hh_enc.Operador_por_linea))
            cmd.Parameters.Add(New SqlParameter("@UBICACION_CON_HH", oBeTrans_ubic_hh_enc.Ubicacion_con_hh))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeTrans_ubic_hh_enc.Estado))
            cmd.Parameters.Add(New SqlParameter("@CAMBIO_ESTADO", oBeTrans_ubic_hh_enc.Cambio_estado))
            cmd.Parameters.Add(New SqlParameter("@IDREABASTECIMIENTOLOG", oBeTrans_ubic_hh_enc.IdReabastecimientoLog))
            cmd.Parameters.Add(New SqlParameter("@ES_TRASLADO_SAP", oBeTrans_ubic_hh_enc.Es_Traslado_SAP))
            cmd.Parameters.Add(New SqlParameter("@NO_DOCUMENTO", oBeTrans_ubic_hh_enc.No_Documento))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeTrans_ubic_hh_enc As clsBeTrans_ubic_hh_enc, Optional ByRef pConection As SqlConnection = Nothing, Optional ByRef pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_ubic_hh_enc")
            Upd.Add("idtareaubicacionenc", "@idtareaubicacionenc", DataType.Parametro)
            Upd.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Upd.Add("idmotivoubicacion", "@idmotivoubicacion", DataType.Parametro)
            Upd.Add("fechainicio", "@fechainicio", DataType.Parametro)
            Upd.Add("horainicio", "@horainicio", DataType.Parametro)
            Upd.Add("fechafin", "@fechafin", DataType.Parametro)
            Upd.Add("horafin", "@horafin", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("observacion", "@observacion", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("operador_por_linea", "@operador_por_linea", DataType.Parametro)
            Upd.Add("ubicacion_con_hh", "@ubicacion_con_hh", DataType.Parametro)
            Upd.Add("estado", "@estado", DataType.Parametro)
            Upd.Add("cambio_estado", "@cambio_estado", DataType.Parametro)
            Upd.Add("idreabastecimientolog", "@idreabastecimientolog", DataType.Parametro)
            Upd.Add("es_traslado_sap", "@es_traslado_sap", DataType.Parametro)
            Upd.Add("no_documento", "@no_documento", DataType.Parametro)
            Upd.Where("IdTareaUbicacionEnc = @IdTareaUbicacionEnc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTAREAUBICACIONENC", oBeTrans_ubic_hh_enc.IdTareaUbicacionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_ubic_hh_enc.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDMOTIVOUBICACION", oBeTrans_ubic_hh_enc.IdMotivoUbicacion))
            cmd.Parameters.Add(New SqlParameter("@FECHAINICIO", oBeTrans_ubic_hh_enc.FechaInicio))
            cmd.Parameters.Add(New SqlParameter("@HORAINICIO", oBeTrans_ubic_hh_enc.HoraInicio))
            cmd.Parameters.Add(New SqlParameter("@FECHAFIN", oBeTrans_ubic_hh_enc.FechaFin))
            cmd.Parameters.Add(New SqlParameter("@HORAFIN", oBeTrans_ubic_hh_enc.HoraFin))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_ubic_hh_enc.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_ubic_hh_enc.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_ubic_hh_enc.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_ubic_hh_enc.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACION", oBeTrans_ubic_hh_enc.Observacion))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_ubic_hh_enc.Activo))
            cmd.Parameters.Add(New SqlParameter("@OPERADOR_POR_LINEA", oBeTrans_ubic_hh_enc.Operador_por_linea))
            cmd.Parameters.Add(New SqlParameter("@UBICACION_CON_HH", oBeTrans_ubic_hh_enc.Ubicacion_con_hh))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeTrans_ubic_hh_enc.Estado))
            cmd.Parameters.Add(New SqlParameter("@CAMBIO_ESTADO", oBeTrans_ubic_hh_enc.Cambio_estado))
            cmd.Parameters.Add(New SqlParameter("@IDREABASTECIMIENTOLOG", oBeTrans_ubic_hh_enc.IdReabastecimientoLog))
            cmd.Parameters.Add(New SqlParameter("@ES_TRASLADO_SAP", oBeTrans_ubic_hh_enc.Es_Traslado_SAP))
            cmd.Parameters.Add(New SqlParameter("@NO_DOCUMENTO", oBeTrans_ubic_hh_enc.No_Documento))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Eliminar(ByRef oBeTrans_ubic_hh_enc As clsBeTrans_ubic_hh_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_ubic_hh_enc" &
             "  Where(IdTareaUbicacionEnc = @IdTareaUbicacionEnc)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTAREAUBICACIONENC", oBeTrans_ubic_hh_enc.IdTareaUbicacionEnc))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_ubic_hh_enc"
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

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Trans_ubic_hh_enc"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt


        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            '#MECR03112025: Se agrego bitacora de ubicacion
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_ubic.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeTrans_ubic_hh_enc As clsBeTrans_ubic_hh_enc) As Boolean

        Try

            Const sp As String = "SELECT * FROM Trans_ubic_hh_enc" &
            " Where(IdTareaUbicacionEnc = @IdTareaUbicacionEnc)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTAREAUBICACIONENC", oBeTrans_ubic_hh_enc.IdTareaUbicacionEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_ubic_hh_enc, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            '#MECR03112025: Se agrego bitacora de ubicacion
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_ubic.Agregar_Error(vMsgError,
                                                  pStackTrace:=ex.StackTrace,
                                                  pIdBodega:=oBeTrans_ubic_hh_enc.IdBodega,
                                                  pIdTareaUbicacionEnc:=oBeTrans_ubic_hh_enc.IdTareaUbicacionEnc,
                                                  pIdMotivoUbicacion:=oBeTrans_ubic_hh_enc.IdMotivoUbicacion)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeTrans_ubic_hh_enc As clsBeTrans_ubic_hh_enc) As Boolean

        GetSingle = False

        Try

            Const sp As String = "SELECT * FROM Trans_ubic_hh_enc" &
                                 " Where(IdTareaUbicacionEnc = @IdTareaUbicacionEnc)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim lTransaction As SqlTransaction = Nothing

            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTAREAUBICACIONENC", pBeTrans_ubic_hh_enc.IdTareaUbicacionEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTrans_ubic_hh_enc, dt.Rows(0))
                Return True
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            '#MECR03112025: Se agrego bitacora de ubicacion
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_ubic.Agregar_Error(vMsgError,
                                                  pStackTrace:=ex.StackTrace,
                                                  pIdBodega:=pBeTrans_ubic_hh_enc.IdBodega,
                                                  pIdTareaUbicacionEnc:=pBeTrans_ubic_hh_enc.IdTareaUbicacionEnc,
                                                  pIdMotivoUbicacion:=pBeTrans_ubic_hh_enc.IdMotivoUbicacion)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdTareaUbicacionEnc),0) FROM Trans_ubic_hh_enc"

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


        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            '#MECR03112025: Se agrego bitacora de ubicacion
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_ubic.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace)

            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar_Estado_Tarea(ByRef oBeTrans_ubic_hh_enc As clsBeTrans_ubic_hh_enc,
                                                   Optional ByRef pConection As SqlConnection = Nothing,
                                                   Optional ByRef pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_ubic_hh_enc")
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("estado", "@estado", DataType.Parametro)
            Upd.Where("IdTareaUbicacionEnc = @IdTareaUbicacionEnc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTAREAUBICACIONENC", oBeTrans_ubic_hh_enc.IdTareaUbicacionEnc))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_ubic_hh_enc.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_ubic_hh_enc.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeTrans_ubic_hh_enc.Estado))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

End Class
