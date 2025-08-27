Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_picking_enc

    Public Shared Sub Cargar(ByRef oBeTrans_picking_enc As clsBeTrans_picking_enc, ByRef dr As DataRow)

        Try

            With oBeTrans_picking_enc

                .IdPickingEnc = IIf(IsDBNull(dr.Item("IdPickingEnc")), 0, dr.Item("IdPickingEnc"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                .IdUbicacionPicking = IIf(IsDBNull(dr.Item("IdUbicacionPicking")), 0, dr.Item("IdUbicacionPicking"))
                .Fecha_picking = IIf(IsDBNull(dr.Item("fecha_picking")), Date.Now, dr.Item("fecha_picking"))
                .Hora_ini = IIf(IsDBNull(dr.Item("hora_ini")), Date.Now, dr.Item("hora_ini"))
                .Hora_fin = IIf(IsDBNull(dr.Item("hora_fin")), Date.Now, dr.Item("hora_fin"))
                .Estado = IIf(IsDBNull(dr.Item("estado")), "", dr.Item("estado"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Detalle_operador = IIf(IsDBNull(dr.Item("detalle_operador")), False, dr.Item("detalle_operador"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .verifica_auto = IIf(IsDBNull(dr.Item("verifica_auto")), False, dr.Item("verifica_auto"))
                .procesado_bof = IIf(IsDBNull(dr.Item("procesado_bof")), False, dr.Item("procesado_bof"))
                .Requiere_Preparacion = IIf(IsDBNull(dr.Item("Requiere_Preparacion")), False, dr.Item("Requiere_Preparacion"))
                .Tipo_Preparacion = IIf(IsDBNull(dr.Item("tipo_preparacion")), "", dr.Item("tipo_preparacion"))
                .Estado_Preparacion = IIf(IsDBNull(dr.Item("estado_preparacion")), "", dr.Item("estado_preparacion"))
                .Fecha_Inicio_Preparacion = IIf(IsDBNull(dr.Item("fecha_inicio_preparacion")), New Date(1900, 1, 1), dr.Item("fecha_inicio_preparacion"))
                .Fecha_Fin_Preparacion = IIf(IsDBNull(dr.Item("fecha_fin_preparacion")), New Date(1900, 1, 1), dr.Item("fecha_fin_preparacion"))
                .Referencia = IIf(IsDBNull(dr.Item("Referencia")), "", dr.Item("Referencia"))
                .Fotografia_Verificacion = IIf(IsDBNull(dr.Item("Fotografia_Verificacion")), False, dr.Item("Fotografia_Verificacion"))
                .IdBodegaMuelle = IIf(IsDBNull(dr.Item("IdBodegaMuelle")), 0, dr.Item("IdBodegaMuelle"))
                .IdPrioridadPicking = IIf(IsDBNull(dr.Item("IdPrioridadPicking")), 0, dr.Item("IdPrioridadPicking"))
                .IdTipoPicking = IIf(IsDBNull(dr.Item("IdTipoPicking")), 0, dr.Item("IdTipoPicking"))
                .Observacion = IIf(IsDBNull(dr.Item("Observacion")), "", dr.Item("Observacion"))

            End With

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_picking_enc As clsBeTrans_picking_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_picking_enc")
            Ins.Add("idpickingenc", "@idpickingenc", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Ins.Add("idubicacionpicking", "@idubicacionpicking", DataType.Parametro)
            Ins.Add("fecha_picking", "@fecha_picking", DataType.Parametro)
            Ins.Add("hora_ini", "@hora_ini", DataType.Parametro)
            Ins.Add("hora_fin", "@hora_fin", DataType.Parametro)
            Ins.Add("estado", "@estado", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("detalle_operador", "@detalle_operador", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("verifica_auto", "@verifica_auto", DataType.Parametro)
            Ins.Add("procesado_bof", "@procesado_bof", DataType.Parametro)
            Ins.Add("requiere_preparacion", "@requiere_preparacion", DataType.Parametro)
            Ins.Add("tipo_preparacion", "@tipo_preparacion", DataType.Parametro)
            Ins.Add("estado_preparacion", "@estado_preparacion", DataType.Parametro)
            Ins.Add("fecha_inicio_preparacion", "@fecha_inicio_preparacion", DataType.Parametro)
            Ins.Add("fecha_fin_preparacion", "@fecha_fin_preparacion", DataType.Parametro)
            Ins.Add("referencia", "@referencia", DataType.Parametro)
            Ins.Add("fotografia_verificacion", "@fotografia_verificacion", DataType.Parametro)
            Ins.Add("idprioridadpicking", "@idprioridadpicking", DataType.Parametro)
            Ins.Add("IdTipoPicking", "@IdTipoPicking", DataType.Parametro)
            Ins.Add("Observacion", "@Observacion", DataType.Parametro)

            If Not oBeTrans_picking_enc.IdBodegaMuelle = 0 Then Ins.Add("idbodegamuelle", "@idbodegamuelle", DataType.Parametro)

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

            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", oBeTrans_picking_enc.IdPickingEnc))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_picking_enc.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_picking_enc.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONPICKING", oBeTrans_picking_enc.IdUbicacionPicking))
            cmd.Parameters.Add(New SqlParameter("@FECHA_PICKING", oBeTrans_picking_enc.Fecha_picking))
            cmd.Parameters.Add(New SqlParameter("@HORA_INI", oBeTrans_picking_enc.Hora_ini))
            cmd.Parameters.Add(New SqlParameter("@HORA_FIN", oBeTrans_picking_enc.Hora_fin))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeTrans_picking_enc.Estado))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_picking_enc.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_picking_enc.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_picking_enc.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_picking_enc.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@DETALLE_OPERADOR", oBeTrans_picking_enc.Detalle_operador))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_picking_enc.Activo))
            cmd.Parameters.Add(New SqlParameter("@VERIFICA_AUTO", oBeTrans_picking_enc.verifica_auto))
            cmd.Parameters.Add(New SqlParameter("@PROCESADO_BOF", oBeTrans_picking_enc.procesado_bof))
            cmd.Parameters.Add(New SqlParameter("@REQUIERE_PREPARACION", oBeTrans_picking_enc.Requiere_Preparacion))
            cmd.Parameters.Add(New SqlParameter("@TIPO_PREPARACION", oBeTrans_picking_enc.Tipo_Preparacion))
            cmd.Parameters.Add(New SqlParameter("@ESTADO_PREPARACION", oBeTrans_picking_enc.Estado_Preparacion))
            cmd.Parameters.Add(New SqlParameter("@FECHA_INICIO_PREPARACION", oBeTrans_picking_enc.Fecha_Inicio_Preparacion))
            cmd.Parameters.Add(New SqlParameter("@FECHA_FIN_PREPARACION", oBeTrans_picking_enc.Fecha_Fin_Preparacion))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIA", oBeTrans_picking_enc.Referencia))
            cmd.Parameters.Add(New SqlParameter("@FOTOGRAFIA_VERIFICACION", oBeTrans_picking_enc.Fotografia_Verificacion))
            If Not oBeTrans_picking_enc.IdBodegaMuelle = 0 Then cmd.Parameters.Add(New SqlParameter("@IDBODEGAMUELLE", oBeTrans_picking_enc.IdBodegaMuelle))
            cmd.Parameters.Add(New SqlParameter("@IDPRIORIDADPICKING", oBeTrans_picking_enc.IdPrioridadPicking))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOPICKING", oBeTrans_picking_enc.IdTipoPicking))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACION", oBeTrans_picking_enc.Observacion))

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

    'Actualizar_FecMod_And_HoraFin
    Public Shared Function Actualizar(ByRef oBeTrans_picking_enc As clsBeTrans_picking_enc, Optional ByRef pConection As SqlConnection = Nothing, Optional ByRef pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_picking_enc")
            Upd.Add("idpickingenc", "@idpickingenc", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Upd.Add("idubicacionpicking", "@idubicacionpicking", DataType.Parametro)
            Upd.Add("fecha_picking", "@fecha_picking", DataType.Parametro)
            Upd.Add("hora_ini", "@hora_ini", DataType.Parametro)
            Upd.Add("hora_fin", "@hora_fin", DataType.Parametro)
            Upd.Add("estado", "@estado", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("detalle_operador", "@detalle_operador", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("verifica_auto", "@verifica_auto", DataType.Parametro)
            Upd.Add("procesado_bof", "@procesado_bof", DataType.Parametro)
            Upd.Add("requiere_preparacion", "@requiere_preparacion", DataType.Parametro)
            Upd.Add("tipo_preparacion", "@tipo_preparacion", DataType.Parametro)
            Upd.Add("estado_preparacion", "@estado_preparacion", DataType.Parametro)
            Upd.Add("fecha_inicio_preparacion", "@fecha_inicio_preparacion", DataType.Parametro)
            Upd.Add("fecha_fin_preparacion", "@fecha_fin_preparacion", DataType.Parametro)
            Upd.Add("referencia", "@referencia", DataType.Parametro)
            Upd.Add("fotografia_verificacion", "@fotografia_verificacion", DataType.Parametro)
            If Not oBeTrans_picking_enc.IdBodegaMuelle = 0 Then Upd.Add("idbodegamuelle", "@idbodegamuelle", DataType.Parametro)
            Upd.Add("idprioridadpicking", "@idprioridadpicking", DataType.Parametro)
            Upd.Add("IdTipoPicking", "@IdTipoPicking", DataType.Parametro)
            Upd.Add("Observacion", "@Observacion", DataType.Parametro)
            Upd.Where("IdPickingEnc = @IdPickingEnc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}


            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", oBeTrans_picking_enc.IdPickingEnc))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_picking_enc.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_picking_enc.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONPICKING", oBeTrans_picking_enc.IdUbicacionPicking))
            cmd.Parameters.Add(New SqlParameter("@FECHA_PICKING", oBeTrans_picking_enc.Fecha_picking))
            cmd.Parameters.Add(New SqlParameter("@HORA_INI", oBeTrans_picking_enc.Hora_ini))
            cmd.Parameters.Add(New SqlParameter("@HORA_FIN", oBeTrans_picking_enc.Hora_fin))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeTrans_picking_enc.Estado))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_picking_enc.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_picking_enc.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_picking_enc.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_picking_enc.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@DETALLE_OPERADOR", oBeTrans_picking_enc.Detalle_operador))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_picking_enc.Activo))
            cmd.Parameters.Add(New SqlParameter("@VERIFICA_AUTO", oBeTrans_picking_enc.verifica_auto))
            cmd.Parameters.Add(New SqlParameter("@PROCESADO_BOF", oBeTrans_picking_enc.procesado_bof))
            cmd.Parameters.Add(New SqlParameter("@REQUIERE_PREPARACION", oBeTrans_picking_enc.Requiere_Preparacion))
            cmd.Parameters.Add(New SqlParameter("@TIPO_PREPARACION", oBeTrans_picking_enc.Tipo_Preparacion))
            cmd.Parameters.Add(New SqlParameter("@ESTADO_PREPARACION", oBeTrans_picking_enc.Estado_Preparacion))
            cmd.Parameters.Add(New SqlParameter("@FECHA_INICIO_PREPARACION", oBeTrans_picking_enc.Fecha_Inicio_Preparacion))
            cmd.Parameters.Add(New SqlParameter("@FECHA_FIN_PREPARACION", oBeTrans_picking_enc.Fecha_Fin_Preparacion))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIA", oBeTrans_picking_enc.Referencia))
            cmd.Parameters.Add(New SqlParameter("@FOTOGRAFIA_VERIFICACION", oBeTrans_picking_enc.Fotografia_Verificacion))
            If Not oBeTrans_picking_enc.IdBodegaMuelle = 0 Then cmd.Parameters.Add(New SqlParameter("@IDBODEGAMUELLE", oBeTrans_picking_enc.IdBodegaMuelle))
            cmd.Parameters.Add(New SqlParameter("@IDPRIORIDADPICKING", oBeTrans_picking_enc.IdPrioridadPicking))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOPICKING", oBeTrans_picking_enc.IdTipoPicking))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACION", oBeTrans_picking_enc.Observacion))

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

    Public Shared Function Eliminar(ByRef oBeTrans_picking_enc As clsBeTrans_picking_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_picking_enc" &
             "  Where(IdPickingEnc = @IdPickingEnc)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", oBeTrans_picking_enc.IdPickingEnc))


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

            Const sp As String = " Delete from Trans_picking_enc"
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
            If lConnection.State = ConnectionState.Open Then lConnection.Close
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Trans_picking_enc"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt


        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeTrans_picking_enc As clsBeTrans_picking_enc) As Boolean

        Try

            Const sp As String = "SELECT * FROM Trans_picking_enc" &
            " Where(IdPickingEnc = @IdPickingEnc)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPICKINGENC", oBeTrans_picking_enc.IDPICKINGENC))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_picking_enc, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True


        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeTrans_picking_enc)

        Try

            Dim lReturnList As New List(Of clsBeTrans_picking_enc)
            Const sp As String = "SELECT * FROM Trans_picking_enc"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_picking_enc As New clsBeTrans_picking_enc

            For Each dr As DataRow In dt.Rows

                vBeTrans_picking_enc = New clsBeTrans_picking_enc
                Cargar(vBeTrans_picking_enc, dr)
                lReturnList.Add(vBeTrans_picking_enc)

            Next

            lConnection.Dispose()
            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeTrans_picking_enc As clsBeTrans_picking_enc) As Boolean

        GetSingle = False

        Try

            Const sp As String = "SELECT * FROM Trans_picking_enc" &
            " Where(IdPickingEnc = @IdPickingEnc)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPICKINGENC", pBeTrans_picking_enc.IDPICKINGENC))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTrans_picking_enc, dt.Rows(0))
                GetSingle = True
            End If

        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeTrans_picking_enc As clsBeTrans_picking_enc,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As Boolean

        GetSingle = False

        Try

            Const sp As String = "SELECT * FROM Trans_picking_enc" &
            " Where(IdPickingEnc = @IdPickingEnc)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPICKINGENC", pBeTrans_picking_enc.IdPickingEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            Dim vBeTrans_picking_enc As New clsBeTrans_picking_enc

            If dt.Rows.Count = 1 Then
                Cargar(vBeTrans_picking_enc, dt.Rows(0))
                pBeTrans_picking_enc = vBeTrans_picking_enc
                GetSingle = True
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID() as Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdPickingEnc),0) FROM Trans_picking_enc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
                Using lCommand As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CInt(lReturnValue) + 1
                    End If
                End Using
            End Using

            Return lMax


        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    'Actualizar_FecMod_And_HoraFin
    Public Shared Function Actualizar_FecMod_And_HoraFin(ByVal pIdPickingEnc As Integer, ByVal pEstadoPicking As String,
                                                         Optional ByRef pConection As SqlConnection = Nothing,
                                                         Optional ByRef pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_picking_enc")
            Upd.Add("estado", "@estado", DataType.Parametro)
            Upd.Add("hora_fin", "@hora_fin", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("IdPickingEnc = @IdPickingEnc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}


            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", pIdPickingEnc))
            'cmd.Parameters.Add(New SqlParameter("@ESTADO", "Procesado"))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", pEstadoPicking))
            cmd.Parameters.Add(New SqlParameter("@HORA_FIN", Now))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", Now))

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

    Shared Function get_cantidad_picking_activos(ByVal idBodega As Integer, ByVal d1 As Date, ByVal d2 As Date) As Integer

        get_cantidad_picking_activos = 0

        Try

            Dim vSQL As String = "SELECT COUNT(distinct Pedido) Cant_Pedido 
                                 FROM VW_Pedido 
                                 WHERE fecha between @FechaDesde and @FechaHasta and EstadoPedido <> 'Anulado'
                                 AND IdBodega=@IdBodega "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", idBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@FechaDesde", d1)
                        lDTA.SelectCommand.Parameters.AddWithValue("@FechaHasta", d2)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)

                            get_cantidad_picking_activos = IIf(IsDBNull(lRow("Cant_Pedido")), 0, lRow("Cant_Pedido"))

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
