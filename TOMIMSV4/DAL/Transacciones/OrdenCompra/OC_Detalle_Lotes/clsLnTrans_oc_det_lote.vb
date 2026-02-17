Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_oc_det_lote

    Public Shared Sub Cargar(ByRef oBeTrans_oc_det_lote As clsBeTrans_oc_det_lote, ByRef dr As DataRow)

        Try

            With oBeTrans_oc_det_lote

                .IdOrdenCompraEnc = IIf(IsDBNull(dr.Item("IdOrdenCompraEnc")), 0, dr.Item("IdOrdenCompraEnc"))
                .IdOrdenCompraDet = IIf(IsDBNull(dr.Item("IdOrdenCompraDet")), 0, dr.Item("IdOrdenCompraDet"))
                .IdOrdenCompraDetLote = IIf(IsDBNull(dr.Item("IdOrdenCompraDetLote")), 0, dr.Item("IdOrdenCompraDetLote"))
                .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                .No_linea = IIf(IsDBNull(dr.Item("no_linea")), 0, dr.Item("no_linea"))
                .Codigo_producto = IIf(IsDBNull(dr.Item("codigo_producto")), "", dr.Item("codigo_producto"))
                .Cantidad = IIf(IsDBNull(dr.Item("cantidad")), 0.0, dr.Item("cantidad"))
                .Cantidad_recibida = IIf(IsDBNull(dr.Item("cantidad_recibida")), 0.0, dr.Item("cantidad_recibida"))
                .Lote = IIf(IsDBNull(dr.Item("lote")), "", dr.Item("lote"))
                .Lic_Plate = IIf(IsDBNull(dr.Item("lic_plate")), "", dr.Item("lic_plate"))
                .Fecha_vence = IIf(IsDBNull(dr.Item("fecha_vence")), Nothing, dr.Item("fecha_vence"))
                .Ubicacion = IIf(IsDBNull(dr.Item("ubicacion")), "", dr.Item("ubicacion"))
                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .IdUnidadMedidaBasica = IIf(IsDBNull(dr.Item("IdUnidadMedidaBasica")), 0, dr.Item("IdUnidadMedidaBasica"))
                .Presentacion.IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .UnidadMedida.IdUnidadMedida = IIf(IsDBNull(dr.Item("IdUnidadMedidaBasica")), 0, dr.Item("IdUnidadMedidaBasica"))
                .Reclasificar = IIf(IsDBNull(dr.Item("Reclasificar")), False, dr.Item("Reclasificar"))
                .Activo = IIf(IsDBNull(dr.Item("Activo")), True, dr.Item("Activo"))
                .No_Documento = IIf(IsDBNull(dr.Item("No_Documento")), "", dr.Item("No_Documento"))
                .Codigo_Sku = IIf(IsDBNull(dr.Item("Codigo_Sku")), "", dr.Item("Codigo_Sku"))
                .IdProductoTallaColor = IIf(IsDBNull(dr.Item("IdProductoTallaColor")), 0, dr.Item("IdProductoTallaColor"))
                .Talla = IIf(IsDBNull(dr.Item("Talla")), "", dr.Item("Talla"))
                .Color = IIf(IsDBNull(dr.Item("Color")), "", dr.Item("Color"))

            End With

        Catch ex As Exception
            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace, oBeTrans_oc_det_lote.IdOrdenCompraEnc)

            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_oc_det_lote As clsBeTrans_oc_det_lote, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_oc_det_lote")
            Ins.Add("idordencompraenc", "@idordencompraenc", DataType.Parametro)
            Ins.Add("idordencompradet", "@idordencompradet", DataType.Parametro)
            Ins.Add("idordencompradetlote", "@idordencompradetlote", DataType.Parametro)
            Ins.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Ins.Add("no_linea", "@no_linea", DataType.Parametro)
            Ins.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
            Ins.Add("cantidad", "@cantidad", DataType.Parametro)
            Ins.Add("cantidad_recibida", "@cantidad_recibida", DataType.Parametro)
            Ins.Add("lote", "@lote", DataType.Parametro)
            Ins.Add("lic_plate", "@lic_plate", DataType.Parametro)
            Ins.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Ins.Add("ubicacion", "@ubicacion", DataType.Parametro)
            Ins.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Ins.Add("idunidadmedidabasica", "@idunidadmedidabasica", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("Reclasificar", "@Reclasificar", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("no_documento", "@no_documento", DataType.Parametro)
            Ins.Add("codigo_sku", "@codigo_sku", DataType.Parametro)
            Ins.Add("idproductotallacolor", "@idproductotallacolor", DataType.Parametro)
            Ins.Add("talla", "@talla", DataType.Parametro)
            Ins.Add("color", "@color", DataType.Parametro)

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

            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_oc_det_lote.IdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRADET", oBeTrans_oc_det_lote.IdOrdenCompraDet))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRADETLOTE", oBeTrans_oc_det_lote.IdOrdenCompraDetLote))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_oc_det_lote.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@NO_LINEA", oBeTrans_oc_det_lote.No_linea))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeTrans_oc_det_lote.Codigo_producto))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_oc_det_lote.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_RECIBIDA", oBeTrans_oc_det_lote.Cantidad_recibida))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeTrans_oc_det_lote.Lote))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeTrans_oc_det_lote.Lic_Plate))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeTrans_oc_det_lote.Fecha_vence))
            cmd.Parameters.Add(New SqlParameter("@UBICACION", oBeTrans_oc_det_lote.Ubicacion))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", IIf(oBeTrans_oc_det_lote.Presentacion.IdPresentacion = 0, DBNull.Value, oBeTrans_oc_det_lote.Presentacion.IdPresentacion)))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDABASICA", IIf(oBeTrans_oc_det_lote.UnidadMedida.IdUnidadMedida = 0, DBNull.Value, oBeTrans_oc_det_lote.UnidadMedida.IdUnidadMedida)))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_oc_det_lote.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_oc_det_lote.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_oc_det_lote.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_oc_det_lote.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@RECLASIFICAR", oBeTrans_oc_det_lote.Reclasificar))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_oc_det_lote.Activo))
            cmd.Parameters.Add(New SqlParameter("@NO_DOCUMENTO", oBeTrans_oc_det_lote.No_Documento))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_SKU", oBeTrans_oc_det_lote.Codigo_Sku))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOTALLACOLOR", oBeTrans_oc_det_lote.IdProductoTallaColor))
            cmd.Parameters.Add(New SqlParameter("@TALLA", oBeTrans_oc_det_lote.Talla))
            cmd.Parameters.Add(New SqlParameter("@COLOR", oBeTrans_oc_det_lote.Color))

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

    Public Shared Function Actualizar(ByRef oBeTrans_oc_det_lote As clsBeTrans_oc_det_lote, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_oc_det_lote")
            Upd.Add("idordencompraenc", "@idordencompraenc", DataType.Parametro)
            Upd.Add("idordencompradet", "@idordencompradet", DataType.Parametro)
            Upd.Add("idordencompradetlote", "@idordencompradetlote", DataType.Parametro)
            Upd.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Upd.Add("no_linea", "@no_linea", DataType.Parametro)
            Upd.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Add("cantidad_recibida", "@cantidad_recibida", DataType.Parametro)
            Upd.Add("lote", "@lote", DataType.Parametro)
            Upd.Add("lic_plate", "@lic_plate", DataType.Parametro)
            Upd.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Upd.Add("ubicacion", "@ubicacion", DataType.Parametro)
            Upd.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Upd.Add("idunidadmedidabasica", "@idunidadmedidabasica", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("Reclasificar", "@Reclasificar", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("no_documento", "@no_documento", DataType.Parametro)
            Upd.Add("codigo_sku", "@codigo_sku", DataType.Parametro)
            Upd.Add("idproductotallacolor", "@idproductotallacolor", DataType.Parametro)
            Upd.Add("talla", "@talla", DataType.Parametro)
            Upd.Add("color", "@color", DataType.Parametro)
            Upd.Where("IdOrdenCompraEnc = @IdOrdenCompraEnc" &
                " AND IdOrdenCompraDet = @IdOrdenCompraDet" &
                " AND IdOrdenCompraDetLote = @IdOrdenCompraDetLote")

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

            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_oc_det_lote.IdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRADET", oBeTrans_oc_det_lote.IdOrdenCompraDet))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRADETLOTE", oBeTrans_oc_det_lote.IdOrdenCompraDetLote))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_oc_det_lote.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@NO_LINEA", oBeTrans_oc_det_lote.No_linea))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeTrans_oc_det_lote.Codigo_producto))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_oc_det_lote.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_RECIBIDA", oBeTrans_oc_det_lote.Cantidad_recibida))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeTrans_oc_det_lote.Lote))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeTrans_oc_det_lote.Lic_Plate))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeTrans_oc_det_lote.Fecha_vence))
            cmd.Parameters.Add(New SqlParameter("@UBICACION", oBeTrans_oc_det_lote.Ubicacion))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", IIf(oBeTrans_oc_det_lote.Presentacion.IdPresentacion = 0, DBNull.Value, oBeTrans_oc_det_lote.Presentacion.IdPresentacion)))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDABASICA", IIf(oBeTrans_oc_det_lote.UnidadMedida.IdUnidadMedida = 0, DBNull.Value, oBeTrans_oc_det_lote.UnidadMedida.IdUnidadMedida)))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_oc_det_lote.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_oc_det_lote.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_oc_det_lote.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_oc_det_lote.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@RECLASIFICAR", oBeTrans_oc_det_lote.Reclasificar))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_oc_det_lote.Activo))
            cmd.Parameters.Add(New SqlParameter("@NO_DOCUMENTO", oBeTrans_oc_det_lote.No_Documento))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_SKU", oBeTrans_oc_det_lote.Codigo_Sku))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOTALLACOLOR", oBeTrans_oc_det_lote.IdProductoTallaColor))
            cmd.Parameters.Add(New SqlParameter("@TALLA", oBeTrans_oc_det_lote.Talla))
            cmd.Parameters.Add(New SqlParameter("@COLOR", oBeTrans_oc_det_lote.Color))

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

    Public Shared Function Eliminar(ByRef oBeTrans_oc_det_lote As clsBeTrans_oc_det_lote, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_oc_det_lote" &
             "  Where(IdOrdenCompraEnc = @IdOrdenCompraEnc)" &
             "  AND (IdOrdenCompraDet = @IdOrdenCompraDet)" &
             "  AND (IdOrdenCompraDetLote = @IdOrdenCompraDetLote)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_oc_det_lote.IdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRADET", oBeTrans_oc_det_lote.IdOrdenCompraDet))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRADETLOTE", oBeTrans_oc_det_lote.IdOrdenCompraDetLote))

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

            Const sp As String = " Delete from Trans_oc_det_lote"
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

            Const sp As String = "SELECT * FROM Trans_oc_det_lote"
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

    Public Shared Function Obtener(ByRef oBeTrans_oc_det_lote As clsBeTrans_oc_det_lote) As Boolean

        Try

            Const sp As String = "SELECT * FROM Trans_oc_det_lote" &
            " Where(IdOrdenCompraEnc = @IdOrdenCompraEnc)" &
            " AND (IdOrdenCompraDet = @IdOrdenCompraDet)" &
            " AND (IdOrdenCompraDetLote = @IdOrdenCompraDetLote)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_oc_det_lote.IdOrdenCompraEnc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDORDENCOMPRADET", oBeTrans_oc_det_lote.IdOrdenCompraDet))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDORDENCOMPRADETLOTE", oBeTrans_oc_det_lote.IdOrdenCompraDetLote))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_oc_det_lote, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace, oBeTrans_oc_det_lote.IdOrdenCompraEnc)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeTrans_oc_det_lote)

        Try

            Dim lReturnList As New List(Of clsBeTrans_oc_det_lote)
            Const sp As String = "SELECT * FROM Trans_oc_det_lote"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_oc_det_lote As New clsBeTrans_oc_det_lote

            For Each dr As DataRow In dt.Rows
                vBeTrans_oc_det_lote = New clsBeTrans_oc_det_lote
                Cargar(vBeTrans_oc_det_lote, dr)
                lReturnList.Add(vBeTrans_oc_det_lote)
            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeTrans_oc_det_lote As clsBeTrans_oc_det_lote)

        Try

            Const sp As String = "SELECT * FROM Trans_oc_det_lote" &
            " Where(IdOrdenCompraEnc = @IdOrdenCompraEnc)" &
            " AND (IdOrdenCompraDet = @IdOrdenCompraDet)" &
            " AND (IdOrdenCompraDetLote = @IdOrdenCompraDetLote)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", pBeTrans_oc_det_lote.IDORDENCOMPRAENC))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDORDENCOMPRADET", pBeTrans_oc_det_lote.IDORDENCOMPRADET))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDORDENCOMPRADETLOTE", pBeTrans_oc_det_lote.IDORDENCOMPRADETLOTE))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTrans_oc_det_lote, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdOrdenCompraEnc),0) FROM Trans_oc_det_lote"

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

    Public Shared Function GetSingle(ByRef pBeTrans_oc_det_lote As clsBeTrans_oc_det_lote,
                                     ByVal lConnection As SqlConnection,
                                     ByVal lTransaction As SqlTransaction) As Boolean

        GetSingle = False

        Try

            Const sp As String = "SELECT * FROM Trans_oc_det_lote " &
            " Where(IdOrdenCompraEnc = @IdOrdenCompraEnc) " &
            " AND (IdOrdenCompraDet = @IdOrdenCompraDet) " &
            " AND (IdOrdenCompraDetLote = @IdOrdenCompraDetLote)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", pBeTrans_oc_det_lote.IdOrdenCompraEnc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDORDENCOMPRADET", pBeTrans_oc_det_lote.IdOrdenCompraDet))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDORDENCOMPRADETLOTE", pBeTrans_oc_det_lote.IdOrdenCompraDetLote))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTrans_oc_det_lote, dt.Rows(0))
                GetSingle = True
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
