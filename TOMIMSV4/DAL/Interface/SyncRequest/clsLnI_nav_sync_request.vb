Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnI_nav_sync_request

    Public Shared Sub Cargar(ByRef oBeI_nav_sync_request As clsBeI_nav_sync_request, ByRef dr As DataRow)

        Try

            With oBeI_nav_sync_request
                .IdSyncRequest = IIf(IsDBNull(dr.Item("idsyncrequest")), 0, dr.Item("idsyncrequest"))
                .IdNavConfigEnc = IIf(IsDBNull(dr.Item("idnavconfigenc")), 0, dr.Item("idnavconfigenc"))
                .IdEmpresa = IIf(IsDBNull(dr.Item("idempresa")), 0, dr.Item("idempresa"))
                .IdBodega = IIf(IsDBNull(dr.Item("idbodega")), 0, dr.Item("idbodega"))
                .IdUsuario = IIf(IsDBNull(dr.Item("idusuario")), 0, dr.Item("idusuario"))
                .Tipo_Interface = IIf(IsDBNull(dr.Item("tipo_interface")), 0, dr.Item("tipo_interface"))
                .Origen = IIf(IsDBNull(dr.Item("origen")), "", dr.Item("origen"))
                .Estado = IIf(IsDBNull(dr.Item("estado")), clsBeI_nav_sync_request.ESTADO_PENDIENTE, dr.Item("estado"))
                .Parametros = IIf(IsDBNull(dr.Item("parametros")), "", dr.Item("parametros"))
                .Fecha_Solicitud = IIf(IsDBNull(dr.Item("fecha_solicitud")), Date.Now, dr.Item("fecha_solicitud"))
                .Fecha_Inicio = IIf(IsDBNull(dr.Item("fecha_inicio")), New Date(1900, 1, 1), dr.Item("fecha_inicio"))
                .Fecha_Fin = IIf(IsDBNull(dr.Item("fecha_fin")), New Date(1900, 1, 1), dr.Item("fecha_fin"))
                .Intento = IIf(IsDBNull(dr.Item("intento")), 0, dr.Item("intento"))
                .Mensaje = IIf(IsDBNull(dr.Item("mensaje")), "", dr.Item("mensaje"))
                .Host_Solicita = IIf(IsDBNull(dr.Item("host_solicita")), "", dr.Item("host_solicita"))
                .Host_Procesa = IIf(IsDBNull(dr.Item("host_procesa")), "", dr.Item("host_procesa"))
            End With

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeI_nav_sync_request As clsBeI_nav_sync_request,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Long

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("i_nav_sync_request")
            Ins.Add("idnavconfigenc", "@idnavconfigenc", DataType.Parametro)
            Ins.Add("idempresa", "@idempresa", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("idusuario", "@idusuario", DataType.Parametro)
            Ins.Add("tipo_interface", "@tipo_interface", DataType.Parametro)
            Ins.Add("origen", "@origen", DataType.Parametro)
            Ins.Add("estado", "@estado", DataType.Parametro)
            Ins.Add("parametros", "@parametros", DataType.Parametro)
            Ins.Add("fecha_solicitud", "@fecha_solicitud", DataType.Parametro)
            Ins.Add("intento", "@intento", DataType.Parametro)
            Ins.Add("mensaje", "@mensaje", DataType.Parametro)
            Ins.Add("host_solicita", "@host_solicita", DataType.Parametro)

            Dim sp As String = Ins.SQLIdentity("idsyncrequest")
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDNAVCONFIGENC", oBeI_nav_sync_request.IdNavConfigEnc))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeI_nav_sync_request.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeI_nav_sync_request.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDUSUARIO", oBeI_nav_sync_request.IdUsuario))
            cmd.Parameters.Add(New SqlParameter("@TIPO_INTERFACE", oBeI_nav_sync_request.Tipo_Interface))
            cmd.Parameters.Add(New SqlParameter("@ORIGEN", oBeI_nav_sync_request.Origen))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeI_nav_sync_request.Estado))
            cmd.Parameters.Add(New SqlParameter("@PARAMETROS", oBeI_nav_sync_request.Parametros))
            cmd.Parameters.Add(New SqlParameter("@FECHA_SOLICITUD", oBeI_nav_sync_request.Fecha_Solicitud))
            cmd.Parameters.Add(New SqlParameter("@INTENTO", oBeI_nav_sync_request.Intento))
            cmd.Parameters.Add(New SqlParameter("@MENSAJE", oBeI_nav_sync_request.Mensaje))
            cmd.Parameters.Add(New SqlParameter("@HOST_SOLICITA", oBeI_nav_sync_request.Host_Solicita))

            Dim newId As Long = CLng(cmd.ExecuteScalar())
            oBeI_nav_sync_request.IdSyncRequest = newId

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return newId

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Existe_Solicitud_Activa(ByVal pIdNavConfigEnc As Integer,
                                                   ByVal pTipoInterface As Integer,
                                                   Optional ByVal pConection As SqlConnection = Nothing,
                                                   Optional ByVal pTransaction As SqlTransaction = Nothing) As Boolean

        Const sp As String = "SELECT COUNT(1) " &
                             "FROM i_nav_sync_request WITH (UPDLOCK, HOLDLOCK) " &
                             "WHERE idnavconfigenc = @idnavconfigenc " &
                             "AND tipo_interface = @tipo_interface " &
                             "AND estado IN ('PENDIENTE', 'PROCESANDO')"

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

        Try

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open()
                cmd = New SqlCommand(sp, lConnection)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDNAVCONFIGENC", pIdNavConfigEnc))
            cmd.Parameters.Add(New SqlParameter("@TIPO_INTERFACE", pTipoInterface))

            Return CInt(cmd.ExecuteScalar()) > 0

        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Encolar_Si_No_Existe_Activa(ByRef oBeI_nav_sync_request As clsBeI_nav_sync_request,
                                                       Optional ByVal pConection As SqlConnection = Nothing,
                                                       Optional ByVal pTransaction As SqlTransaction = Nothing) As Long

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                If Existe_Solicitud_Activa(oBeI_nav_sync_request.IdNavConfigEnc, oBeI_nav_sync_request.Tipo_Interface, pConection, pTransaction) Then Return 0
                Return Insertar(oBeI_nav_sync_request, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.Serializable)

                If Existe_Solicitud_Activa(oBeI_nav_sync_request.IdNavConfigEnc, oBeI_nav_sync_request.Tipo_Interface, lConnection, lTransaction) Then
                    lTransaction.Commit()
                    Return 0
                End If

                Dim vIdSyncRequest As Long = Insertar(oBeI_nav_sync_request, lConnection, lTransaction)
                lTransaction.Commit()
                Return vIdSyncRequest
            End If

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Tomar_Siguiente_Pendiente(ByVal pHostProcesa As String,
                                                     Optional ByVal pTipoInterface As Integer = 0,
                                                     Optional ByVal pConection As SqlConnection = Nothing,
                                                     Optional ByVal pTransaction As SqlTransaction = Nothing) As clsBeI_nav_sync_request

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                Return Tomar_Siguiente_Pendiente_Internal(pHostProcesa, pTipoInterface, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                Dim vRequest As clsBeI_nav_sync_request = Tomar_Siguiente_Pendiente_Internal(pHostProcesa, pTipoInterface, lConnection, lTransaction)
                lTransaction.Commit()
                Return vRequest
            End If

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Private Shared Function Tomar_Siguiente_Pendiente_Internal(ByVal pHostProcesa As String,
                                                              ByVal pTipoInterface As Integer,
                                                              ByVal pConnection As SqlConnection,
                                                              ByVal pTransaction As SqlTransaction) As clsBeI_nav_sync_request

        Dim vSQL As String = "SELECT TOP 1 * " &
                             "FROM i_nav_sync_request WITH (UPDLOCK, READPAST, ROWLOCK) " &
                             "WHERE estado = @estado_pendiente " &
                             If(pTipoInterface > 0, "AND tipo_interface = @tipo_interface ", "") &
                             "ORDER BY fecha_solicitud, idsyncrequest"

        Dim cmdSelect As New SqlCommand(vSQL, pConnection, pTransaction) With {.CommandType = CommandType.Text}
        cmdSelect.Parameters.Add(New SqlParameter("@ESTADO_PENDIENTE", clsBeI_nav_sync_request.ESTADO_PENDIENTE))
        If pTipoInterface > 0 Then cmdSelect.Parameters.Add(New SqlParameter("@TIPO_INTERFACE", pTipoInterface))

        Dim dt As New DataTable
        Using dad As New SqlDataAdapter(cmdSelect)
            dad.Fill(dt)
        End Using

        If dt.Rows.Count = 0 Then Return Nothing

        Dim vRequest As New clsBeI_nav_sync_request
        Cargar(vRequest, dt.Rows(0))

        Marcar_Procesando(vRequest.IdSyncRequest, pHostProcesa, pConnection, pTransaction)

        vRequest.Estado = clsBeI_nav_sync_request.ESTADO_PROCESANDO
        vRequest.Fecha_Inicio = Date.Now
        vRequest.Host_Procesa = pHostProcesa
        vRequest.Intento += 1

        Return vRequest

    End Function

    Public Shared Function Marcar_Procesando(ByVal pIdSyncRequest As Long,
                                             ByVal pHostProcesa As String,
                                             Optional ByVal pConection As SqlConnection = Nothing,
                                             Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Const sp As String = "UPDATE i_nav_sync_request " &
                             "SET estado = @estado_procesando, fecha_inicio = GETDATE(), intento = ISNULL(intento,0) + 1, host_procesa = @host_procesa " &
                             "WHERE idsyncrequest = @idsyncrequest AND estado = @estado_pendiente"

        Return Ejecutar_Update_Estado(sp, pIdSyncRequest, clsBeI_nav_sync_request.ESTADO_PROCESANDO, "", pHostProcesa, pConection, pTransaction)

    End Function

    Public Shared Function Marcar_Finalizado(ByVal pIdSyncRequest As Long,
                                             Optional ByVal pMensaje As String = "",
                                             Optional ByVal pConection As SqlConnection = Nothing,
                                             Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Const sp As String = "UPDATE i_nav_sync_request " &
                             "SET estado = @estado, fecha_fin = GETDATE(), mensaje = @mensaje " &
                             "WHERE idsyncrequest = @idsyncrequest"

        Return Ejecutar_Update_Estado(sp, pIdSyncRequest, clsBeI_nav_sync_request.ESTADO_FINALIZADO, pMensaje, "", pConection, pTransaction)

    End Function

    Public Shared Function Marcar_Error(ByVal pIdSyncRequest As Long,
                                        ByVal pMensaje As String,
                                        Optional ByVal pConection As SqlConnection = Nothing,
                                        Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Const sp As String = "UPDATE i_nav_sync_request " &
                             "SET estado = @estado, fecha_fin = GETDATE(), mensaje = @mensaje " &
                             "WHERE idsyncrequest = @idsyncrequest"

        Return Ejecutar_Update_Estado(sp, pIdSyncRequest, clsBeI_nav_sync_request.ESTADO_ERROR, pMensaje, "", pConection, pTransaction)

    End Function

    Public Shared Function Marcar_Ignorado(ByVal pIdSyncRequest As Long,
                                           ByVal pMensaje As String,
                                           Optional ByVal pConection As SqlConnection = Nothing,
                                           Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Const sp As String = "UPDATE i_nav_sync_request " &
                             "SET estado = @estado, fecha_fin = GETDATE(), mensaje = @mensaje " &
                             "WHERE idsyncrequest = @idsyncrequest"

        Return Ejecutar_Update_Estado(sp, pIdSyncRequest, clsBeI_nav_sync_request.ESTADO_IGNORADO, pMensaje, "", pConection, pTransaction)

    End Function

    Private Shared Function Ejecutar_Update_Estado(ByVal pSQL As String,
                                                   ByVal pIdSyncRequest As Long,
                                                   ByVal pEstado As String,
                                                   ByVal pMensaje As String,
                                                   ByVal pHostProcesa As String,
                                                   Optional ByVal pConection As SqlConnection = Nothing,
                                                   Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(pSQL, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(pSQL, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDSYNCREQUEST", pIdSyncRequest))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", pEstado))
            cmd.Parameters.Add(New SqlParameter("@ESTADO_PROCESANDO", clsBeI_nav_sync_request.ESTADO_PROCESANDO))
            cmd.Parameters.Add(New SqlParameter("@ESTADO_PENDIENTE", clsBeI_nav_sync_request.ESTADO_PENDIENTE))
            cmd.Parameters.Add(New SqlParameter("@MENSAJE", pMensaje))
            cmd.Parameters.Add(New SqlParameter("@HOST_PROCESA", pHostProcesa))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

End Class
