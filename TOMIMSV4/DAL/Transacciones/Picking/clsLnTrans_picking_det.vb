Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_picking_det

    Public Shared Sub Cargar(ByRef oBeTrans_picking_det As clsBeTrans_picking_det, ByRef dr As DataRow)
        Try
            With oBeTrans_picking_det
                .IdPickingDet = IIf(IsDBNull(dr.Item("IdPickingDet")), 0, dr.Item("IdPickingDet"))
                .IdPickingEnc = IIf(IsDBNull(dr.Item("IdPickingEnc")), 0, dr.Item("IdPickingEnc"))
                .IdPedidoDet = IIf(IsDBNull(dr.Item("IdPedidoDet")), 0, dr.Item("IdPedidoDet"))
                .IdPedidoEnc = IIf(IsDBNull(dr.Item("IdPedidoEnc")), 0, dr.Item("IdPedidoEnc"))
                .IdOperadorBodega = IIf(IsDBNull(dr.Item("IdOperadorBodega")), 0, dr.Item("IdOperadorBodega"))
                .Cantidad = IIf(IsDBNull(dr.Item("cantidad")), 0.0, dr.Item("cantidad"))
                .Cliente_dias = IIf(IsDBNull(dr.Item("cliente_dias")), 0, dr.Item("cliente_dias"))
                .Cantidad_recibida = IIf(IsDBNull(dr.Item("cantidad_recibida")), 0.0, dr.Item("cantidad_recibida"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Codigo = IIf(IsDBNull(dr.Item("codigo")), "", dr.Item("codigo"))
                .NombreProducto = IIf(IsDBNull(dr.Item("nombre")), "", dr.Item("nombre"))
                '#CKFK20240517 Agregué esta condición
                If dr.Table.Columns.Contains("Bono") Then .Bono = IIf(IsDBNull(dr.Item("Bono")), "", dr.Item("Bono"))
            End With

        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdPedidoDet:=oBeTrans_picking_det.IdPedidoDet,
                                                  pIdPedidoEnc:=oBeTrans_picking_det.IdPedidoEnc,
                                                  pIdPickingEnc:=oBeTrans_picking_det.IdPickingEnc,
                                                  pIdPickingDet:=oBeTrans_picking_det.IdPickingDet,
                                                  pStackTrace:=ex.StackTrace)
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_picking_det As clsBeTrans_picking_det,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_picking_det")
            Ins.Add("idpickingdet", "@idpickingdet", DataType.Parametro)
            Ins.Add("idpickingenc", "@idpickingenc", DataType.Parametro)
            Ins.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
            Ins.Add("idpedidodet", "@idpedidodet", DataType.Parametro)
            Ins.Add("idoperadorbodega", "@idoperadorbodega", DataType.Parametro)
            Ins.Add("cantidad", "@cantidad", DataType.Parametro)
            Ins.Add("cliente_dias", "@cliente_dias", DataType.Parametro)
            Ins.Add("cantidad_recibida", "@cantidad_recibida", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("codigo", "@codigo", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)

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

            cmd.Parameters.Add(New SqlParameter("@IDPICKINGDET", oBeTrans_picking_det.IdPickingDet))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", oBeTrans_picking_det.IdPickingEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTrans_picking_det.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDODET", oBeTrans_picking_det.IdPedidoDet))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA", IIf(oBeTrans_picking_det.IdOperadorBodega = 0, DBNull.Value, oBeTrans_picking_det.IdOperadorBodega)))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_picking_det.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@CLIENTE_DIAS", oBeTrans_picking_det.Cliente_dias))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_RECIBIDA", oBeTrans_picking_det.Cantidad_recibida))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_picking_det.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_picking_det.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_picking_det.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_picking_det.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_picking_det.Activo))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeTrans_picking_det.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeTrans_picking_det.NombreProducto))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            oBeTrans_picking_det.IdPickingDet = CInt(cmd.Parameters("@IDPICKINGDET").Value)

            Return rowsAffected

            oBeTrans_picking_det.IdPickingDet = CInt(cmd.Parameters("@IDPICKINGDET").Value)

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

    Public Shared Function Actualizar(ByRef oBeTrans_picking_det As clsBeTrans_picking_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_picking_det")
            Upd.Add("idpickingdet", "@idpickingdet", DataType.Parametro)
            Upd.Add("idpickingenc", "@idpickingenc", DataType.Parametro)
            Upd.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
            Upd.Add("idpedidodet", "@idpedidodet", DataType.Parametro)
            Upd.Add("idoperadorbodega", "@idoperadorbodega", DataType.Parametro)
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Add("cliente_dias", "@cliente_dias", DataType.Parametro)
            Upd.Add("cantidad_recibida", "@cantidad_recibida", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("IdPickingDet = @IdPickingDet")
            Upd.Add("codigo", "@codigo", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)

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

            cmd.Parameters.Add(New SqlParameter("@IDPICKINGDET", oBeTrans_picking_det.IdPickingDet))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", oBeTrans_picking_det.IdPickingEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTrans_picking_det.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDODET", oBeTrans_picking_det.IdPedidoDet))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA", IIf(oBeTrans_picking_det.IdOperadorBodega = 0, DBNull.Value, oBeTrans_picking_det.IdOperadorBodega)))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_picking_det.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@CLIENTE_DIAS", oBeTrans_picking_det.Cliente_dias))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_RECIBIDA", oBeTrans_picking_det.Cantidad_recibida))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_picking_det.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_picking_det.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_picking_det.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_picking_det.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_picking_det.Activo))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeTrans_picking_det.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeTrans_picking_det.NombreProducto))


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
            lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    '#AT20230218 Agregué esta función para restar la cantidad reemplazada
    'en la verificacion a la cantidad recibida en tras_picking_det
    Public Shared Function Actualizar_Cantidad_Recibida(ByRef oBeTrans_picking_det As clsBeTrans_picking_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_picking_det")
            Upd.Add("cantidad_recibida", "@cantidad_recibida", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("IdPickingDet = @IdPickingDet")

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

            cmd.Parameters.Add(New SqlParameter("@IDPICKINGDET", oBeTrans_picking_det.IdPickingDet))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_RECIBIDA", oBeTrans_picking_det.Cantidad_recibida))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_picking_det.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_picking_det.User_mod))

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
            lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    '#CKFK20230220 Erik solicitó otro commit
    '#CKFK20230218 Agregué esta función restar la cantidad reemplazada en la verificacion a la cantidad recibida en tras_picking_det
    Public Shared Function Actualizar_Cantidad_Picking_Det(ByRef oBeTrans_picking_ubic As clsBeTrans_picking_ubic,
                                                           Optional ByVal pConnection As SqlConnection = Nothing,
                                                           Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim FilasPickingDet As Integer = 0
        Dim BePickingDet As New clsBeTrans_picking_det
        Dim BePedidoDet As New clsBeTrans_pe_det
        Dim Factor As Double = 0

        Try

            Dim Es_Transaccion_Remota As Boolean = (pConnection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                '#AT20230218 Si el reemplazo se hace en la verificación se debe restar a la cantidad recibida en picking det
                BePedidoDet = clsLnTrans_pe_det.Get_Single_By_IdPedidoEnc_And_IdPedidoDet(oBeTrans_picking_ubic.IdPedidoEnc,
                                                                                             oBeTrans_picking_ubic.IdPedidoDet,
                                                                                              pConnection, pTransaction)

                BePickingDet = clsLnTrans_picking_det.Get_Single_By_IdPickingEnc_And_IdPickingDet(oBeTrans_picking_ubic.IdPickingEnc,
                                                                                                      oBeTrans_picking_ubic.IdPickingDet,
                                                                                                       pConnection, pTransaction)

                If BePedidoDet.IdPresentacion <> oBeTrans_picking_ubic.IdPresentacion Then
                    If BePedidoDet.IdPresentacion = 0 AndAlso oBeTrans_picking_ubic.IdPresentacion <> 0 Then
                        Factor = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(oBeTrans_picking_ubic.IdProductoBodega,
                                                                                               oBeTrans_picking_ubic.IdPresentacion,
                                                                                               pConnection, pTransaction)
                        '#CKFK20230218 Aquí cambié la cantidad_solicitada por la cantidad recibida, porque no siempre se reemplaza
                        'todo lo solicitado
                        If Factor <> 0 Then
                            Dim CantPedido As Double = oBeTrans_picking_ubic.Cantidad_Recibida * Factor
                            BePickingDet.Cantidad_recibida = BePickingDet.Cantidad_recibida - CantPedido
                        End If
                    End If
                Else
                    BePickingDet.Cantidad_recibida = BePickingDet.Cantidad_recibida - oBeTrans_picking_ubic.Cantidad_Recibida
                End If

                FilasPickingDet = clsLnTrans_picking_det.Actualizar_Cantidad_Recibida(BePickingDet, pConnection, pTransaction)

                If FilasPickingDet = 0 Then
                    Throw New Exception("Error al actualizar cantidad recibida en Trans_picking_det")
                End If

            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                BePedidoDet = clsLnTrans_pe_det.Get_Single_By_IdPedidoEnc_And_IdPedidoDet(oBeTrans_picking_ubic.IdPedidoEnc,
                                                                                             oBeTrans_picking_ubic.IdPedidoDet,
                                                                                             lConnection,
                                                                                             lTransaction)

                BePickingDet = clsLnTrans_picking_det.Get_Single_By_IdPickingEnc_And_IdPickingDet(oBeTrans_picking_ubic.IdPickingEnc,
                                                                                                  oBeTrans_picking_ubic.IdPickingDet,
                                                                                                  lConnection,
                                                                                                  lTransaction)

                If BePedidoDet.IdPresentacion <> oBeTrans_picking_ubic.IdPresentacion Then
                    If BePedidoDet.IdPresentacion = 0 AndAlso oBeTrans_picking_ubic.IdPresentacion <> 0 Then
                        Factor = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(oBeTrans_picking_ubic.IdProductoBodega,
                                                                                           oBeTrans_picking_ubic.IdPresentacion,
                                                                                           lConnection,
                                                                                           lTransaction)
                        '#CKFK20230218 Aquí cambié la cantidad_solicitada por la cantidad recibida, porque no siempre se reemplaza
                        'todo lo solicitado
                        If Factor <> 0 Then
                            Dim CantPedido As Double = oBeTrans_picking_ubic.Cantidad_Recibida * Factor
                            BePickingDet.Cantidad_recibida = BePickingDet.Cantidad_recibida - CantPedido
                        End If
                    End If
                Else
                    BePickingDet.Cantidad_recibida = BePickingDet.Cantidad_recibida - oBeTrans_picking_ubic.Cantidad_Recibida
                End If

                FilasPickingDet = clsLnTrans_picking_det.Actualizar_Cantidad_Recibida(BePickingDet, lConnection, lTransaction)

                If FilasPickingDet = 0 Then
                    Throw New Exception("Error al actualizar cantidad recibida en Trans_picking_det")
                End If
            End If

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return FilasPickingDet

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function
    Public Shared Function Eliminar(ByRef oBeTrans_picking_det As clsBeTrans_picking_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_picking_det" &
             "  Where(IdPickingDet = @IdPickingDet)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPICKINGDET", oBeTrans_picking_det.IdPickingDet))


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

            Const sp As String = " Delete from Trans_picking_det"
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

            Const sp As String = "SELECT * FROM Trans_picking_det"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeTrans_picking_det As clsBeTrans_picking_det) As Boolean

        Obtener = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Const sp As String = "SELECT * FROM Trans_picking_det" &
            " Where(IdPickingDet = @IdPickingDet)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPICKINGDET", oBeTrans_picking_det.IdPickingDet))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_picking_det, dt.Rows(0))
                Obtener = True
            End If

            lTransaction.Commit()

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeTrans_picking_det As clsBeTrans_picking_det, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Boolean

        Obtener = False

        Try

            'lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Const sp As String = "SELECT * FROM Trans_picking_det" &
            " Where(IdPickingDet = @IdPickingDet)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPICKINGDET", oBeTrans_picking_det.IdPickingDet))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_picking_det, dt.Rows(0))
                Obtener = True
            End If

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeTrans_picking_det)

        Try

            Dim lReturnList As New List(Of clsBeTrans_picking_det)
            Const sp As String = "SELECT * FROM Trans_picking_det"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_picking_det As New clsBeTrans_picking_det

            For Each dr As DataRow In dt.Rows

                vBeTrans_picking_det = New clsBeTrans_picking_det
                Cargar(vBeTrans_picking_det, dr)
                lReturnList.Add(vBeTrans_picking_det)

            Next

            lConnection.Dispose()
            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeTrans_picking_det As clsBeTrans_picking_det)

        Try

            Const sp As String = "SELECT * FROM Trans_picking_det" &
            " Where(IdPickingDet = @IdPickingDet)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPICKINGDET", pBeTrans_picking_det.IDPICKINGDET))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTrans_picking_det, dt.Rows(0))
            End If

            Return True

        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdPedidoDet:=pBeTrans_picking_det.IdPedidoDet,
                                                  pIdPedidoEnc:=pBeTrans_picking_det.IdPedidoEnc,
                                                  pIdPickingEnc:=pBeTrans_picking_det.IdPickingEnc,
                                                  pIdPickingDet:=pBeTrans_picking_det.IdPickingDet,
                                                  pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdPickingDet),0) FROM Trans_picking_det"

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
            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdPickingEnc_And_IdPickingDet(ByVal pIdPickingEnc As Integer,
                                                                       ByVal pIdPickingDet As Integer) As clsBeTrans_picking_det

        Get_Single_By_IdPickingEnc_And_IdPickingDet = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM Trans_picking_det " &
            " Where(IdPickingDet = @IdPickingDet)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPICKINGENC", pIdPickingEnc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPICKINGDET", pIdPickingDet))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim oBeTrans_picking_det As New clsBeTrans_picking_det

                Cargar(oBeTrans_picking_det, dt.Rows(0))

                If EsDecimal(oBeTrans_picking_det.Cantidad.ToString) Then

                    Dim oBeTrans_pe_det As New clsBeTrans_pe_det
                    oBeTrans_pe_det = clsLnTrans_pe_det.Get_Single_By_IdPedidoDet(oBeTrans_picking_det.IdPedidoDet, lConnection, lTransaction)

                    If Not oBeTrans_picking_det Is Nothing Then

                        If oBeTrans_pe_det.IdPresentacion > 0 Then
                            Dim bePresentacion As New clsBeProducto_Presentacion
                            bePresentacion.IdPresentacion = oBeTrans_pe_det.IdPresentacion
                            clsLnProducto_presentacion.GetSingle(bePresentacion, lConnection, lTransaction)
                            If bePresentacion IsNot Nothing Then
                                If bePresentacion.Factor > 0 Then
                                    oBeTrans_picking_det.Cantidad = oBeTrans_picking_det.Cantidad * bePresentacion.Factor
                                End If
                            End If
                        End If

                    End If

                End If

                Get_Single_By_IdPickingEnc_And_IdPickingDet = oBeTrans_picking_det
            End If

            lTransaction.Commit()

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    ''#AT20230218 Función con conexión y transacción
    'Public Shared Function Get_Single_By_IdPickingEnc_And_IdPickingDet(ByVal pIdPickingEnc As Integer,
    '                                                                   ByVal pIdPickingDet As Integer,
    '                                                                   Optional ByVal pConection As SqlConnection = Nothing,
    '                                                                   Optional ByVal pTransaction As SqlTransaction = Nothing) As clsBeTrans_picking_det

    '    Get_Single_By_IdPickingEnc_And_IdPickingDet = Nothing

    '    Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
    '    Dim lTransaction As SqlTransaction = Nothing

    '    Try

    '        lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

    '        Const sp As String = "SELECT * FROM Trans_picking_det" &
    '        " Where(IdPickingDet = @IdPickingDet) AND IdPickinEnc = @IdPickingEnc "

    '        Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
    '        Dim dad As New SqlDataAdapter(cmd)

    '        dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPICKINGENC", pIdPickingEnc))
    '        dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPICKINGDET", pIdPickingDet))

    '        Dim dt As New DataTable
    '        dad.Fill(dt)

    '        If dt.Rows.Count = 1 Then
    '            Dim oBeTrans_picking_det As New clsBeTrans_picking_det
    '            Cargar(oBeTrans_picking_det, dt.Rows(0))
    '            Get_Single_By_IdPickingEnc_And_IdPickingDet = oBeTrans_picking_det
    '        End If

    '        lTransaction.Commit()

    '    Catch ex1 As SqlException
    '        If lTransaction IsNot Nothing Then lTransaction.Rollback()
    '        Throw ex1
    '    Catch ex As Exception
    '        If lTransaction IsNot Nothing Then lTransaction.Rollback()
    '        Throw ex
    '    Finally
    '        If lConnection.State = ConnectionState.Open Then lConnection.Close()
    '    End Try

    'End Function

    '#AT20230218 Función con conexión y transacción

    'Public Shared Function Get_Single_By_IdPickingEnc_And_IdPickingDet(ByVal pIdPickingEnc As Integer,
    '                                                                   ByVal pIdPickingDet As Integer,
    '                                                                   Optional ByVal pConnection As SqlConnection = Nothing,
    '                                                                   Optional ByVal pTransaction As SqlTransaction = Nothing) As clsBeTrans_picking_det

    '    Get_Single_By_IdPickingEnc_And_IdPickingDet = Nothing

    '    Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
    '    Dim lTransaction As SqlTransaction = Nothing

    '    Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)

    '    Try

    '        lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

    '        Const sp As String = "SELECT * FROM Trans_picking_det " &
    '                             "Where(IdPickingDet = @IdPickingDet) AND (IdPickinEnc = @IdPickingEnc) "

    '        Dim lCommand As New SqlCommand '(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
    '        Dim dad As New SqlDataAdapter '(cmd)

    '        If Es_Transaccion_Remota Then
    '            lCommand = New SqlCommand(sp, pConnection)
    '            lCommand.Transaction = pTransaction
    '        Else
    '            lCommand = New SqlCommand(sp, lConnection)
    '            lConnection.Open()
    '        End If

    '        dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPICKINGENC", pIdPickingEnc))
    '        dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPICKINGDET", pIdPickingDet))

    '        Dim dt As New DataTable
    '        dad.Fill(dt)

    '        If dt.Rows.Count = 1 Then
    '            Dim oBeTrans_picking_det As New clsBeTrans_picking_det
    '            Cargar(oBeTrans_picking_det, dt.Rows(0))
    '            Get_Single_By_IdPickingEnc_And_IdPickingDet = oBeTrans_picking_det
    '        End If

    '        lTransaction.Commit()

    '    Catch ex1 As SqlException
    '        If lTransaction IsNot Nothing Then lTransaction.Rollback()
    '        Throw ex1
    '    Catch ex As Exception
    '        If lTransaction IsNot Nothing Then lTransaction.Rollback()
    '        Throw ex
    '    Finally
    '        If lConnection.State = ConnectionState.Open Then lConnection.Close()
    '    End Try

    'End Function

    '#AT20230218 Función con conexión y transacción
    '#CKFK20230218 Corregí la función de Anderly
    Public Shared Function Get_Single_By_IdPickingEnc_And_IdPickingDet(ByVal pIdPickingEnc As Integer,
                                                                       ByVal pIdPickingDet As Integer,
                                                                       Optional ByVal pConnection As SqlConnection = Nothing,
                                                                       Optional ByVal pTransaction As SqlTransaction = Nothing) As clsBeTrans_picking_det

        Get_Single_By_IdPickingEnc_And_IdPickingDet = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)

        Try

            Const sp As String = "SELECT * FROM Trans_picking_det " &
                                 "Where(IdPickingDet = @IdPickingDet) AND (IdPickingEnc = @IdPickingEnc) "

            Dim lCommand As New SqlCommand '(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter '(cmd)

            If Es_Transaccion_Remota Then
                lCommand = New SqlCommand(sp, pConnection)
                lCommand.Transaction = pTransaction
            Else

                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                lCommand = New SqlCommand(sp, lConnection)
                lConnection.Open()
            End If

            dad = New SqlDataAdapter(lCommand)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPICKINGENC", pIdPickingEnc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPICKINGDET", pIdPickingDet))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim oBeTrans_picking_det As New clsBeTrans_picking_det
                Cargar(oBeTrans_picking_det, dt.Rows(0))
                Get_Single_By_IdPickingEnc_And_IdPickingDet = oBeTrans_picking_det
            End If

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function EsDecimal(ByVal numero As String) As Boolean
        Try
            Dim resultado As Decimal = Convert.ToDecimal(numero)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function Liberar_Producto_No_Pickeado_Multiple(ByVal IdPedidoDet As Integer,
                                                                 ByVal IdPedidoEnc As Integer,
                                                                 ByVal IdPickingEnc As Integer,
                                                                 ByVal IdUsuario As Integer,
                                                                 ByVal IdBodega As Integer,
                                                                 ByVal IdStockRes As Integer,
                                                                 ByVal lConnection As SqlConnection,
                                                                 ByVal lTransaction As SqlTransaction) As Boolean

        Liberar_Producto_No_Pickeado_Multiple = False

        Try

            Dim IdPickingUbic As Integer = 0
            Dim vCantidadDespachada As Double = 0
            Dim vCantidadSolicitada As Double = 0
            Dim vCantidadRecibida As Double = 0
            Dim vPesoDespachado As Double = 0
            Dim vIdStock As Double = 0
            Dim lBeStockPickeado As New List(Of clsBeStock)
            Dim lBeStockResByPickingUbic As New List(Of clsBeStock_res)
            Dim BeStockPickeado As clsBeStock = Nothing
            Dim vIdUbicacionPickingTomarDe As Integer
            Dim vIdUbicacionPickingDefectoPorBodega As Integer = 0
            Dim objStockOrigen As New clsBeStock()
            Dim vActuarlizarUbicacion As Boolean = False
            Dim lTransPickingDet As New List(Of clsBeTrans_picking_det)
            Dim ListaDetalleUbicacion As New List(Of clsBeTrans_picking_ubic)
            Dim BeTransPickingDet As New clsBeTrans_picking_det()
            Dim vResultEliminacionStock As Integer = 0
            Dim vIdLogLiberacion As Integer = 0
            Dim vResultadoEliminacionPickingUbic As Integer = 0
            Dim vResultadoEliminacionPickingDet As Integer = 0
            Dim lPickingUbic As New List(Of clsBeTrans_picking_ubic)
            Dim BePedidoEnc As New clsBeTrans_pe_enc
            Dim BePickingEnc As New clsBeTrans_picking_enc

            BePedidoEnc = clsLnTrans_pe_enc.Get_Single_By_IdPedidoEnc_And_IdBodega(IdPedidoEnc,
                                                                                   IdBodega,
                                                                                   lConnection,
                                                                                   lTransaction)



            If Not BePedidoEnc Is Nothing Then

                vIdLogLiberacion = clsLnTrans_log_pedido_liberacion.MaxID(lConnection, lTransaction) + 1

                vIdUbicacionPickingDefectoPorBodega = clsLnBodega.Get_IdUbicacion_Picking_By_IdBodega(IdBodega,
                                                                                                      lConnection,
                                                                                                      lTransaction)

                If vIdUbicacionPickingDefectoPorBodega = 0 Then
                    Throw New Exception("No está definida la posición de picking por defecto en la bodega, para liberar el producto a esa posición")
                End If

                BePickingEnc = clsLnTrans_picking_enc.GetSingle(IdPickingEnc, lConnection, lTransaction)

                lTransPickingDet = BePickingEnc.ListaPickingDet
                ListaDetalleUbicacion = BePickingEnc.ListaPickingUbic

                If Not lTransPickingDet Is Nothing Then

                    If lTransPickingDet.Count > 0 Then

                        For Each PickingDet In lTransPickingDet

                            If Not ListaDetalleUbicacion Is Nothing Then

                                For Each PickingUbic In ListaDetalleUbicacion.Where(Function(x) x.IdStockRes = IdStockRes _
                                                                                    AndAlso x.IdPickingEnc = IdPickingEnc _
                                                                                    AndAlso x.IdPedidoDet = IdPedidoDet _
                                                                                    AndAlso x.IdPedidoEnc = IdPedidoEnc _
                                                                                    AndAlso x.IdStockRes = IdStockRes)

                                    vResultadoEliminacionPickingUbic = 0
                                    vResultEliminacionStock = 0
                                    vResultadoEliminacionPickingDet = 0

                                    If PickingUbic.Cantidad_despachada = 0 Then

                                        lBeStockResByPickingUbic = clsLnStock_res.Get_All_By_Params(PickingUbic.IdPedidoDet,
                                                                                                    IdPedidoEnc,
                                                                                                    PickingUbic.IdStock,
                                                                                                    IdStockRes,
                                                                                                    PickingUbic.IdProductoBodega,
                                                                                                    PickingUbic.IdPropietarioBodega,
                                                                                                    IdPickingEnc,
                                                                                                    lConnection,
                                                                                                    lTransaction)

                                        If Not lBeStockResByPickingUbic Is Nothing Then

                                            If lBeStockResByPickingUbic.Count > 0 Then

                                                vResultEliminacionStock = clsLnStock_res.Eliminar_Stock_Reservado_By_IdPedidoDet_And_IdStock(PickingUbic.IdPedidoDet,
                                                                                                                                             IdPedidoEnc,
                                                                                                                                             PickingUbic.IdStock,
                                                                                                                                             IdStockRes,
                                                                                                                                             PickingUbic.IdBodega,
                                                                                                                                             lConnection,
                                                                                                                                             lTransaction)

                                                If vResultEliminacionStock > 0 Then

                                                    lPickingUbic = clsLnTrans_picking_ubic.Get_All_PickingUbic_By_IdPickingDet(PickingUbic.IdPickingDet,
                                                                                                                               PickingUbic.IdPickingEnc,
                                                                                                                               lConnection,
                                                                                                                               lTransaction)

                                                    vResultadoEliminacionPickingUbic = clsLnTrans_picking_ubic.Eliminar_By_Params(PickingUbic.IdPickingUbic,
                                                                                                                                  PickingUbic.IdPickingEnc,
                                                                                                                                  PickingUbic.IdPickingDet,
                                                                                                                                  PickingUbic.IdStock,
                                                                                                                                  IdStockRes,
                                                                                                                                  lConnection,
                                                                                                                                  lTransaction)

                                                    If vResultadoEliminacionPickingUbic > 0 Then

                                                        Try

                                                            If lPickingUbic.Count = 1 Then

                                                                vResultadoEliminacionPickingDet = Eliminar_By_Params(PickingUbic.IdPickingDet,
                                                                                                                     PickingUbic.IdPickingEnc,
                                                                                                                     IdPedidoEnc,
                                                                                                                     IdPedidoDet,
                                                                                                                     lConnection,
                                                                                                                     lTransaction)

                                                            End If

                                                        Catch ex As Exception
                                                            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), "Error_20220108_1148: Al eliminar el PickingDet: " & PickingUbic.IdPickingDet & " Probablemente ya tenga un despacho asociado. ")
                                                            'clsLnLog_error_wms.Agregar_Error(vMsgError)
                                                            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                                                                  pIdPedidoDet:=PickingUbic.IdPedidoDet,
                                                                                                  pIdPedidoEnc:=PickingUbic.IdPedidoEnc,
                                                                                                  pIdPickingEnc:=PickingUbic.IdPickingEnc,
                                                                                                  pIdPickingDet:=PickingUbic.IdPickingDet,
                                                                                                  pIdPickingUbic:=PickingUbic.IdPickingUbic,
                                                                                                  pCodigoProducto:=PickingUbic.CodigoProducto,
                                                                                                  pStackTrace:=ex.StackTrace,
                                                                                                  pIdBodega:=IdBodega,
                                                                                                  pUserAgr:=IdUsuario,
                                                                                                  pConection:=lConnection,
                                                                                                  pTransaction:=lTransaction)
                                                        End Try

                                                        Dim Betrans_log_pedido_liberacion As New clsBeTrans_log_pedido_liberacion
                                                        Betrans_log_pedido_liberacion.IdLogLiberacionStock = vIdLogLiberacion
                                                        Betrans_log_pedido_liberacion.IdUsuario = IdUsuario
                                                        Betrans_log_pedido_liberacion.Fecha = Now
                                                        Betrans_log_pedido_liberacion.IdPedidoEnc = IdPedidoEnc
                                                        Betrans_log_pedido_liberacion.IdPedidoDet = IdPedidoDet
                                                        Betrans_log_pedido_liberacion.Referencia = BePedidoEnc.Referencia
                                                        Betrans_log_pedido_liberacion.Codigo_Producto = PickingDet.Codigo
                                                        Betrans_log_pedido_liberacion.Lote = PickingUbic.Lote
                                                        Betrans_log_pedido_liberacion.Lic_Plate = PickingUbic.Lic_plate
                                                        Betrans_log_pedido_liberacion.Fecha_Vence = PickingUbic.Fecha_Vence
                                                        Betrans_log_pedido_liberacion.Observacion = "Se liberó producto del IdStock: " & PickingUbic.IdStock & " IdStockRes: " & IdStockRes
                                                        Betrans_log_pedido_liberacion.Cantidad = PickingUbic.Cantidad_Solicitada
                                                        Betrans_log_pedido_liberacion.IdPickingUbic = PickingUbic.IdPickingUbic
                                                        Betrans_log_pedido_liberacion.IdPickingDet = PickingDet.IdPickingDet
                                                        Betrans_log_pedido_liberacion.IdProductoBodega = PickingUbic.IdProductoBodega
                                                        Betrans_log_pedido_liberacion.IdProductoEstado = PickingUbic.IdPickingDet
                                                        Betrans_log_pedido_liberacion.IdPropietarioBodega = PickingUbic.IdPropietarioBodega
                                                        Betrans_log_pedido_liberacion.IdUnidadMedida = PickingUbic.IdUnidadMedida
                                                        Betrans_log_pedido_liberacion.IdPresentacion = PickingUbic.IdPresentacion
                                                        Betrans_log_pedido_liberacion.IdUbicacion = PickingUbic.IdUbicacion
                                                        Betrans_log_pedido_liberacion.IdStock = PickingUbic.IdStock
                                                        Betrans_log_pedido_liberacion.IdBodega = PickingUbic.IdBodega

                                                        clsLnTrans_log_pedido_liberacion.Insertar(Betrans_log_pedido_liberacion,
                                                                                                  lConnection,
                                                                                                  lTransaction)
                                                        vIdLogLiberacion += 1

                                                    End If

                                                End If

                                            End If

                                        End If

                                    Else

                                        lBeStockResByPickingUbic = clsLnStock_res.Get_All_By_Params(IdPedidoDet,
                                                                                                    IdPedidoEnc,
                                                                                                    PickingUbic.IdStock,
                                                                                                    PickingUbic.IdProductoBodega,
                                                                                                    PickingUbic.IdPropietarioBodega,
                                                                                                    IdPickingEnc,
                                                                                                    lConnection,
                                                                                                    lTransaction)
                                        If Not lBeStockResByPickingUbic Is Nothing Then

                                            If lBeStockResByPickingUbic.Count > 0 Then

                                                '#EJC20220108_1053AM: La cantidad despachada es > 0'
                                                If (PickingUbic.Cantidad_Solicitada = PickingUbic.Cantidad_Recibida) Then
                                                    '#EJC20220107: El operador bajó todo el producto de la posición, pero hizo un despacho parcial
                                                    'En este caso, el producto restante debe ser movido a la ubicación de picking

                                                    objStockOrigen.IdUbicacion = vIdUbicacionPickingDefectoPorBodega 'Nueva ubicación
                                                    vActuarlizarUbicacion = True
                                                Else
                                                    '#EJC20220107: El operador NO bajó todo el producto de la posición, pero hizo un despacho parcial
                                                    'En este caso, el producto restante (No pickeado y no despachado) debe quedarse en la misma posición.
                                                    objStockOrigen.IdUbicacion = PickingUbic.IdUbicacion
                                                    vActuarlizarUbicacion = False
                                                End If

                                                vResultEliminacionStock = clsLnStock_res.Eliminar_Stock_Reservado_By_IdPedidoDet_And_IdStock(IdPedidoDet,
                                                                                                                                             IdPedidoEnc,
                                                                                                                                             PickingUbic.IdStock,
                                                                                                                                             PickingUbic.IdBodega,
                                                                                                                                             lConnection,
                                                                                                                                             lTransaction)

                                                If vResultEliminacionStock > 0 Then

                                                    If vActuarlizarUbicacion Then

                                                        '#CKFK20220717 Me parece que hay que agregar esto para que funcione
                                                        'vIdStock = PickingUbic.IdStock
                                                        objStockOrigen = New clsBeStock()
                                                        objStockOrigen = clsLnStock.GetSingle(vIdStock,
                                                                                              lConnection,
                                                                                              lTransaction)

                                                        If Not objStockOrigen Is Nothing Then

                                                            objStockOrigen.IdUbicacion = vIdUbicacionPickingDefectoPorBodega
                                                            objStockOrigen.IdUbicacion_anterior = vIdUbicacionPickingTomarDe
                                                            objStockOrigen.Fec_mod = Now
                                                            clsLnStock.Actualiza_Ubicacion_Por_Picking(objStockOrigen,
                                                                                                       lConnection,
                                                                                                       lTransaction)

                                                        Else
                                                            Throw New Exception("Error_20220701: No se pudo obtnener el IdStock asociado a la transacción.")
                                                        End If


                                                    End If

                                                End If


                                            Else
                                                Debug.WriteLine("Error_202201081123: No se obtuvo stock reservado para la línea de pickingubic: " & PickingUbic.IdPickingUbic & " El count de la lista es 0")
                                            End If

                                        Else
                                            Debug.WriteLine("Error_202201081123A: No se obtuvo stock reservado para la línea de pickingubic: " & PickingUbic.IdPickingUbic & " la lista es nothig.")
                                        End If

                                    End If

                                Next

                            Else
                                Debug.WriteLine("La lista picking ubic está vacía para la línea de pickingdet: " & PickingDet.IdPickingDet)
                            End If

                        Next

                        Liberar_Producto_No_Pickeado_Multiple = True

                    Else
                        Throw New Exception("Error_20220108_1027: No se encontraron las líneas de picking asociadas al pedido, la lista está vacía.")
                    End If

                Else

                    Throw New Exception("Error_20220108_1027: No se encontraron las líneas de picking asociadas al pedido, la lista está vacía.")

                End If

            Else
                Throw New Exception("ERROR_20221006_1618: No se pudo obtener el objeto del pedido con el identificador: " & IdPedidoEnc)
            End If

        Catch ex As Exception
            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdPedidoDet:=IdPedidoDet,
                                                  pIdPedidoEnc:=IdPedidoEnc,
                                                  pIdPickingEnc:=IdPickingEnc,
                                                  pIdBodega:=IdBodega,
                                                  pUserAgr:=IdUsuario,
                                                  pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function

End Class