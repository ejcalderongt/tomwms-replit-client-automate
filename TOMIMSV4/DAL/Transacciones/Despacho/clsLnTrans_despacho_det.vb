Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_despacho_det

    Public Shared Sub Cargar(ByRef oBeTrans_despacho_det As clsBeTrans_despacho_det, ByRef dr As DataRow)
        Try
            With oBeTrans_despacho_det
                .IdDespachoDet = IIf(IsDBNull(dr.Item("IdDespachoDet")), 0, dr.Item("IdDespachoDet"))
                .IdDespachoEnc = IIf(IsDBNull(dr.Item("IdDespachoEnc")), 0, dr.Item("IdDespachoEnc"))
                .IdPickingUbic = IIf(IsDBNull(dr.Item("IdPickingUbic")), 0, dr.Item("IdPickingUbic"))
                .Fecha = IIf(IsDBNull(dr.Item("Fecha")), Date.Now, dr.Item("Fecha"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), "", dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .IdPedidoEnc = IIf(IsDBNull(dr.Item("IdPedidoEnc")), 0, dr.Item("IdPedidoEnc"))
                .IdPedidoDet = IIf(IsDBNull(dr.Item("IdPedidoDet")), 0, dr.Item("IdPedidoDet"))
                .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                .IdUnidadMedidaBasica = IIf(IsDBNull(dr.Item("IdUnidadMedidaBasica")), 0, dr.Item("IdUnidadMedidaBasica"))
                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .Codigo = IIf(IsDBNull(dr.Item("Codigo")), "", dr.Item("Codigo"))
                .NombreProducto = IIf(IsDBNull(dr.Item("NombreProducto")), "", dr.Item("NombreProducto"))
                .NombreEstado = IIf(IsDBNull(dr.Item("NombreEstado")), "", dr.Item("NombreEstado"))
                .CantidadDespachada = IIf(IsDBNull(dr.Item("CantidadDespachada")), 0.0, dr.Item("CantidadDespachada"))
                .PesoDespachado = IIf(IsDBNull(dr.Item("PesoDespachado")), 0.0, dr.Item("PesoDespachado"))
                .IdProductoEstado = IIf(IsDBNull(dr.Item("IdProductoEstado")), 0, dr.Item("IdProductoEstado"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_despacho_det As clsBeTrans_despacho_det, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_despacho_det")
            Ins.Add("iddespachodet", "@iddespachodet", DataType.Parametro)
            Ins.Add("iddespachoenc", "@iddespachoenc", DataType.Parametro)
            Ins.Add("idpickingubic", "@idpickingubic", DataType.Parametro)
            Ins.Add("fecha", "@fecha", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
            Ins.Add("idpedidodet", "@idpedidodet", DataType.Parametro)
            Ins.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Ins.Add("idunidadmedidabasica", "@idunidadmedidabasica", DataType.Parametro)
            Ins.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Ins.Add("codigo", "@codigo", DataType.Parametro)
            Ins.Add("nombreproducto", "@nombreproducto", DataType.Parametro)
            Ins.Add("nombreestado", "@nombreestado", DataType.Parametro)
            Ins.Add("cantidaddespachada", "@cantidaddespachada", DataType.Parametro)
            Ins.Add("pesodespachado", "@pesodespachado", DataType.Parametro)
            Ins.Add("idproductoestado", "@idproductoestado", DataType.Parametro)

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

            cmd.Parameters.Add(New SqlParameter("@IDDESPACHODET", oBeTrans_despacho_det.IdDespachoDet))
            cmd.Parameters.Add(New SqlParameter("@IDDESPACHOENC", oBeTrans_despacho_det.IdDespachoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGUBIC", oBeTrans_despacho_det.IdPickingUbic))
            cmd.Parameters.Add(New SqlParameter("@FECHA", oBeTrans_despacho_det.Fecha))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_despacho_det.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_despacho_det.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_despacho_det.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_despacho_det.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_despacho_det.Activo))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTrans_despacho_det.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDODET", oBeTrans_despacho_det.IdPedidoDet))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_despacho_det.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDABASICA", oBeTrans_despacho_det.IdUnidadMedidaBasica))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeTrans_despacho_det.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeTrans_despacho_det.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBREPRODUCTO", oBeTrans_despacho_det.NombreProducto))
            cmd.Parameters.Add(New SqlParameter("@NOMBREESTADO", oBeTrans_despacho_det.NombreEstado))
            cmd.Parameters.Add(New SqlParameter("@CANTIDADDESPACHADA", oBeTrans_despacho_det.CantidadDespachada))
            cmd.Parameters.Add(New SqlParameter("@PESODESPACHADO", oBeTrans_despacho_det.PesoDespachado))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeTrans_despacho_det.IdProductoEstado))

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

    Public Shared Function Actualizar(ByRef oBeTrans_despacho_det As clsBeTrans_despacho_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_despacho_det")
            Upd.Add("iddespachodet", "@iddespachodet", DataType.Parametro)
            Upd.Add("iddespachoenc", "@iddespachoenc", DataType.Parametro)
            'Upd.Add("idpickingubic", "@idpickingubic", DataType.Parametro)
            'Upd.Add("fecha", "@fecha", DataType.Parametro)
            'Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            'Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
            Upd.Add("idpedidodet", "@idpedidodet", DataType.Parametro)
            Upd.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Upd.Add("idunidadmedidabasica", "@idunidadmedidabasica", DataType.Parametro)
            Upd.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Upd.Add("codigo", "@codigo", DataType.Parametro)
            Upd.Add("nombreproducto", "@nombreproducto", DataType.Parametro)
            Upd.Add("nombreestado", "@nombreestado", DataType.Parametro)
            Upd.Add("cantidaddespachada", "@cantidaddespachada", DataType.Parametro)
            Upd.Add("pesodespachado", "@pesodespachado", DataType.Parametro)
            Upd.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Upd.Where("IdDespachoDet = @IdDespachoDet")

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

            cmd.Parameters.Add(New SqlParameter("@IDDESPACHODET", oBeTrans_despacho_det.IdDespachoDet))
            cmd.Parameters.Add(New SqlParameter("@IDDESPACHOENC", oBeTrans_despacho_det.IdDespachoEnc))
            'cmd.Parameters.Add(New SqlParameter("@IDPICKINGUBIC", oBeTrans_despacho_det.IdPickingUbic))
            'cmd.Parameters.Add(New SqlParameter("@FECHA", oBeTrans_despacho_det.Fecha))
            'cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_despacho_det.User_agr))
            'cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_despacho_det.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_despacho_det.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_despacho_det.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_despacho_det.Activo))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTrans_despacho_det.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDODET", oBeTrans_despacho_det.IdPedidoDet))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_despacho_det.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDABASICA", oBeTrans_despacho_det.IdUnidadMedidaBasica))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeTrans_despacho_det.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeTrans_despacho_det.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBREPRODUCTO", oBeTrans_despacho_det.NombreProducto))
            cmd.Parameters.Add(New SqlParameter("@NOMBREESTADO", oBeTrans_despacho_det.NombreEstado))
            cmd.Parameters.Add(New SqlParameter("@CANTIDADDESPACHADA", oBeTrans_despacho_det.CantidadDespachada))
            cmd.Parameters.Add(New SqlParameter("@PESODESPACHADO", oBeTrans_despacho_det.PesoDespachado))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeTrans_despacho_det.IdProductoEstado))

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

    Public Shared Function Eliminar(ByRef oBeTrans_despacho_det As clsBeTrans_despacho_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_despacho_det" &
             "  Where(IdDespachoDet = @IdDespachoDet)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDDESPACHODET", oBeTrans_despacho_det.IdDespachoDet))

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

            Const sp As String = " Delete from Trans_despacho_det"
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

            Const sp As String = "SELECT * FROM Trans_despacho_det"
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

    Public Shared Function Obtener(ByRef oBeTrans_despacho_det As clsBeTrans_despacho_det) As Boolean

        Try

            Const sp As String = "SELECT * FROM Trans_despacho_det" &
            " Where(IdDespachoDet = @IdDespachoDet)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDDESPACHODET", oBeTrans_despacho_det.IdDespachoDet))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_despacho_det, dt.Rows(0))
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

    Public Shared Function GetAll() As List(Of clsBeTrans_despacho_det)

        Try

            Dim lReturnList As New List(Of clsBeTrans_despacho_det)
            Const sp As String = "SELECT * FROM Trans_despacho_det"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_despacho_det As New clsBeTrans_despacho_det

            For Each dr As DataRow In dt.Rows
                vBeTrans_despacho_det = New clsBeTrans_despacho_det
                Cargar(vBeTrans_despacho_det, dr)
                lReturnList.Add(vBeTrans_despacho_det)
            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeTrans_despacho_det As clsBeTrans_despacho_det)

        Try

            Const sp As String = "SELECT * FROM Trans_despacho_det" &
            " Where(IdDespachoDet = @IdDespachoDet)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDDESPACHODET", pBeTrans_despacho_det.IDDESPACHODET))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTrans_despacho_det, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function
    Public Shared Function MaxID() as Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdDespachoDet),0) FROM Trans_despacho_det"

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
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_BePresentacion_By_IdDespachoDet(IdDespachoDet As Integer,
                                                               IdPedidoEnc As Integer,
                                                               lConnection As SqlConnection,
                                                               lTransaction As SqlTransaction) As clsBeProducto_Presentacion

        Get_BePresentacion_By_IdDespachoDet = Nothing

        Try

            Const sp As String = "SELECT pp.* FROM Trans_despacho_det d 
                                  JOIN trans_pe_det p ON d.IdPedidoEnc = p.IdPedidoEnc and d.IdPedidoDet = p.IdPedidoDet
                                  JOIN producto_presentacion pp ON d.IdPresentacion = pp.IdPresentacion  
                                  WHERE(d.IdDespachoDet = @IdDespachoDet AND d.IdPedidoEnc = @IdPedidoEnc)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDDESPACHODET", IdDespachoDet))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPEDIDOENC", IdPedidoEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim vPresentacion As New clsBeProducto_Presentacion
                clsLnProducto_presentacion.Cargar(vPresentacion, dt.Rows(0))
                Get_BePresentacion_By_IdDespachoDet = vPresentacion
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdDespachoEnc_And_IdPickingUbic(ByVal IdDespachoEnc As Integer, ByVal IdPickingUbic As Integer) As clsBeTrans_despacho_det

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Get_Single_By_IdDespachoEnc_And_IdPickingUbic = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM Trans_despacho_det" &
            " Where(IdDespachoEnc = @IdDespachoEnc AND IdPickingUbic = @IdPickingUbic)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDDESPACHOENC", IdDespachoEnc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPICKINGUBIC", IdPickingUbic))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeTrans_despacho_det As New clsBeTrans_despacho_det
                Cargar(pBeTrans_despacho_det, dt.Rows(0))
                Get_Single_By_IdDespachoEnc_And_IdPickingUbic = pBeTrans_despacho_det
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_Single_By_IdDespachoEnc_And_IdPickingUbic(ByVal IdDespachoEnc As Integer, ByVal IdPickingUbic As Integer, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As clsBeTrans_despacho_det

        Get_Single_By_IdDespachoEnc_And_IdPickingUbic = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_despacho_det" &
            " Where(IdDespachoEnc = @IdDespachoEnc AND IdPickingUbic = @IdPickingUbic) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDDESPACHOENC", IdDespachoEnc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPICKINGUBIC", IdPickingUbic))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeTrans_despacho_det As New clsBeTrans_despacho_det
                Cargar(pBeTrans_despacho_det, dt.Rows(0))
                Get_Single_By_IdDespachoEnc_And_IdPickingUbic = pBeTrans_despacho_det
            End If

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

End Class
