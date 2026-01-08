Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_inv_ciclico

    Public Shared Sub Cargar(ByRef oBeTrans_inv_ciclico As clsBeTrans_inv_ciclico, ByRef dr As DataRow)

        Try

            With oBeTrans_inv_ciclico

                .IdInvCiclico = IIf(IsDBNull(dr.Item("idinvciclico")), 0, dr.Item("idinvciclico"))
                .Idinventarioenc = IIf(IsDBNull(dr.Item("idinventarioenc")), 0, dr.Item("idinventarioenc"))
                .IdStock = IIf(IsDBNull(dr.Item("IdStock")), 0, dr.Item("IdStock"))
                .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                .IdProductoEstado = IIf(IsDBNull(dr.Item("IdProductoEstado")), 0, dr.Item("IdProductoEstado"))
                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .IdUnidadMedida = IIf(IsDBNull(dr.Item("IdUnidadMedida")), 0, dr.Item("IdUnidadMedida"))
                .IdUbicacion = IIf(IsDBNull(dr.Item("IdUbicacion")), 0, dr.Item("IdUbicacion"))
                .EsNuevo = IIf(IsDBNull(dr.Item("EsNuevo")), False, dr.Item("EsNuevo"))
                .Lote_stock = IIf(IsDBNull(dr.Item("lote_stock")), "", dr.Item("lote_stock"))
                .Lote = IIf(IsDBNull(dr.Item("lote")), "", dr.Item("lote"))
                .Fecha_vence_stock = IIf(IsDBNull(dr.Item("fecha_vence_stock")), Nothing, dr.Item("fecha_vence_stock"))
                .Fecha_vence = IIf(IsDBNull(dr.Item("fecha_vence")), Nothing, dr.Item("fecha_vence"))
                .Cant_stock = IIf(IsDBNull(dr.Item("cant_stock")), 0.0, dr.Item("cant_stock"))
                .Cantidad = IIf(IsDBNull(dr.Item("cantidad")), 0.0, dr.Item("cantidad"))
                .Cant_reconteo = IIf(IsDBNull(dr.Item("cant_reconteo")), 0.0, dr.Item("cant_reconteo"))
                .Peso_stock = IIf(IsDBNull(dr.Item("peso_stock")), 0.0, dr.Item("peso_stock"))
                .Peso = IIf(IsDBNull(dr.Item("peso")), 0.0, dr.Item("peso"))
                .Peso_reconteo = IIf(IsDBNull(dr.Item("peso_reconteo")), 0.0, dr.Item("peso_reconteo"))
                .Idoperador = IIf(IsDBNull(dr.Item("idoperador")), 0, dr.Item("idoperador"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .IdProductoEst_nuevo = IIf(IsDBNull(dr.Item("IdProductoEst_nuevo")), 0, dr.Item("IdProductoEst_nuevo"))
                .IdPresentacion_nuevo = IIf(IsDBNull(dr.Item("IdPresentacion_nuevo")), 0, dr.Item("IdPresentacion_nuevo"))
                .IdUbicacion_nuevo = IIf(IsDBNull(dr.Item("IdUbicacion_nuevo")), 0, dr.Item("IdUbicacion_nuevo"))
                .EsPallet = IIf(IsDBNull(dr.Item("EsPallet")), False, dr.Item("EsPallet"))
                .lic_plate = IIf(IsDBNull(dr.Item("lic_plate")), "", dr.Item("lic_plate"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .Regularizar = IIf(IsDBNull(dr.Item("Regularizar")), True, dr.Item("Regularizar"))
                .IdProductoTallaColor = IIf(IsDBNull(dr.Item("IdProductoTallaColor")), 0, dr.Item("IdProductoTallaColor"))

            End With

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_inv_ciclico As clsBeTrans_inv_ciclico, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_inv_ciclico")
            Ins.Add("idinvciclico", "@idinvciclico", DataType.Parametro)
            Ins.Add("idinventarioenc", "@idinventarioenc", DataType.Parametro)
            Ins.Add("idstock", "@idstock", DataType.Parametro)
            Ins.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Ins.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Ins.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Ins.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Ins.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Ins.Add("esnuevo", "@esnuevo", DataType.Parametro)
            Ins.Add("lote_stock", "@lote_stock", DataType.Parametro)
            Ins.Add("lote", "@lote", DataType.Parametro)
            Ins.Add("fecha_vence_stock", "@fecha_vence_stock", DataType.Parametro)
            Ins.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Ins.Add("cant_stock", "@cant_stock", DataType.Parametro)
            Ins.Add("cantidad", "@cantidad", DataType.Parametro)
            Ins.Add("cant_reconteo", "@cant_reconteo", DataType.Parametro)
            Ins.Add("peso_stock", "@peso_stock", DataType.Parametro)
            Ins.Add("peso", "@peso", DataType.Parametro)
            Ins.Add("peso_reconteo", "@peso_reconteo", DataType.Parametro)
            Ins.Add("idoperador", "@idoperador", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("IdProductoEst_nuevo", "@IdProductoEst_nuevo", DataType.Parametro)
            Ins.Add("IdPresentacion_nuevo", "@IdPresentacion_nuevo", DataType.Parametro)
            Ins.Add("IdUbicacion_nuevo", "@IdUbicacion_nuevo", DataType.Parametro)
            Ins.Add("espallet", "@espallet", DataType.Parametro)
            Ins.Add("lic_plate", "@lic_plate", DataType.Parametro)
            Ins.Add("IdBodega", "@IdBodega", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("regularizar", "@regularizar", DataType.Parametro)
            Ins.Add("IdProductoTallaColor", "@IdProductoTallaColor", DataType.Parametro)
            Ins.Add("gondola", "@Gondola", DataType.Parametro)

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

            '#EJC20191216: Se hacía x fuera en el ws.
            If Es_Transaccion_Remota Then
                oBeTrans_inv_ciclico.IdInvCiclico = MaxID(pConection, pTransaction)
            Else
                oBeTrans_inv_ciclico.IdInvCiclico = MaxID(lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDINVCICLICO", oBeTrans_inv_ciclico.IdInvCiclico))
            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_ciclico.Idinventarioenc))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeTrans_inv_ciclico.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", IIf(oBeTrans_inv_ciclico.IdProductoBodega = 0, 0, oBeTrans_inv_ciclico.IdProductoBodega)))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", IIf(oBeTrans_inv_ciclico.IdProductoEstado = 0, 0, oBeTrans_inv_ciclico.IdProductoEstado)))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", IIf(oBeTrans_inv_ciclico.IdPresentacion = 0, 0, oBeTrans_inv_ciclico.IdPresentacion)))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", IIf(oBeTrans_inv_ciclico.IdUbicacion = 0, 0, oBeTrans_inv_ciclico.IdUbicacion)))
            cmd.Parameters.Add(New SqlParameter("@ESNUEVO", oBeTrans_inv_ciclico.EsNuevo))
            cmd.Parameters.Add(New SqlParameter("@LOTE_STOCK", IIf(oBeTrans_inv_ciclico.Lote_stock.Trim = "", "", oBeTrans_inv_ciclico.Lote_stock.Trim)))
            cmd.Parameters.Add(New SqlParameter("@LOTE", IIf(oBeTrans_inv_ciclico.Lote = Nothing, "", oBeTrans_inv_ciclico.Lote)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE_STOCK", IIf(oBeTrans_inv_ciclico.Fecha_vence_stock = Nothing, DBNull.Value, oBeTrans_inv_ciclico.Fecha_vence_stock)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", IIf(oBeTrans_inv_ciclico.Fecha_vence = Nothing, DBNull.Value, oBeTrans_inv_ciclico.Fecha_vence)))
            cmd.Parameters.Add(New SqlParameter("@CANT_STOCK", IIf(oBeTrans_inv_ciclico.Cant_stock = 0.0, 0, oBeTrans_inv_ciclico.Cant_stock)))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", IIf(oBeTrans_inv_ciclico.Cantidad = 0, 0, oBeTrans_inv_ciclico.Cantidad)))
            cmd.Parameters.Add(New SqlParameter("@CANT_RECONTEO", IIf(oBeTrans_inv_ciclico.Cant_reconteo = 0.0, 0, oBeTrans_inv_ciclico.Cant_reconteo)))
            cmd.Parameters.Add(New SqlParameter("@PESO_STOCK", IIf(oBeTrans_inv_ciclico.Peso_stock = 0.0, 0, oBeTrans_inv_ciclico.Peso_stock)))
            cmd.Parameters.Add(New SqlParameter("@PESO", IIf(oBeTrans_inv_ciclico.Peso = 0.0, 0, oBeTrans_inv_ciclico.Peso)))
            cmd.Parameters.Add(New SqlParameter("@PESO_RECONTEO", IIf(oBeTrans_inv_ciclico.Peso_reconteo = 0.0, 0, oBeTrans_inv_ciclico.Peso_reconteo)))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADOR", IIf(oBeTrans_inv_ciclico.Idoperador = 0, 0, oBeTrans_inv_ciclico.Idoperador)))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", IIf(oBeTrans_inv_ciclico.User_agr = Nothing, "", oBeTrans_inv_ciclico.User_agr)))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_inv_ciclico.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOEST_NUEVO", IIf(oBeTrans_inv_ciclico.IdProductoEst_nuevo = 0, 0, oBeTrans_inv_ciclico.IdProductoEst_nuevo)))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION_NUEVO", IIf(oBeTrans_inv_ciclico.IdPresentacion_nuevo = 0, 0, oBeTrans_inv_ciclico.IdPresentacion_nuevo)))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION_NUEVO", IIf(oBeTrans_inv_ciclico.IdUbicacion_nuevo = 0, 0, oBeTrans_inv_ciclico.IdUbicacion_nuevo)))
            cmd.Parameters.Add(New SqlParameter("@ESPALLET", (oBeTrans_inv_ciclico.EsPallet)))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", IIf(oBeTrans_inv_ciclico.lic_plate = "", "", oBeTrans_inv_ciclico.lic_plate)))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeTrans_inv_ciclico.IdUnidadMedida))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_inv_ciclico.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_inv_ciclico.Fec_Mod))
            cmd.Parameters.Add(New SqlParameter("@REGULARIZAR", oBeTrans_inv_ciclico.Regularizar))
            cmd.Parameters.Add(New SqlParameter("@IdProductoTallaColor", oBeTrans_inv_ciclico.IdProductoTallaColor))
            cmd.Parameters.Add(New SqlParameter("@GONDOLA", oBeTrans_inv_ciclico.Gondola))

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

    Public Shared Function Actualizar(ByRef oBeTrans_inv_ciclico As clsBeTrans_inv_ciclico, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_inv_ciclico")
            Upd.Add("idinvciclico", "@idinvciclico", DataType.Parametro)
            Upd.Add("idinventarioenc", "@idinventarioenc", DataType.Parametro)
            Upd.Add("idstock", "@idstock", DataType.Parametro)
            Upd.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Upd.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Upd.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Upd.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Upd.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Upd.Add("esnuevo", "@esnuevo", DataType.Parametro)
            Upd.Add("lote_stock", "@lote_stock", DataType.Parametro)
            Upd.Add("lote", "@lote", DataType.Parametro)
            Upd.Add("fecha_vence_stock", "@fecha_vence_stock", DataType.Parametro)
            Upd.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Upd.Add("cant_stock", "@cant_stock", DataType.Parametro)
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Add("cant_reconteo", "@cant_reconteo", DataType.Parametro)
            Upd.Add("peso_stock", "@peso_stock", DataType.Parametro)
            Upd.Add("peso", "@peso", DataType.Parametro)
            Upd.Add("peso_reconteo", "@peso_reconteo", DataType.Parametro)
            Upd.Add("idoperador", "@idoperador", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("IdProductoEst_nuevo", "@IdProductoEst_nuevo", DataType.Parametro)
            Upd.Add("IdPresentacion_nuevo", "@IdPresentacion_nuevo", DataType.Parametro)
            Upd.Add("IdUbicacion_nuevo", "@IdUbicacion_nuevo", DataType.Parametro)
            Upd.Add("espallet", "@espallet", DataType.Parametro)
            Upd.Add("lic_plate", "@lic_plate", DataType.Parametro)
            Upd.Add("IdBodega", "@IdBodega", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("regularizar", "@regularizar", DataType.Parametro)
            Upd.Add("IdProductoTallaColor", "@IdProductoTallaColor", DataType.Parametro)
            Upd.Add("gondola", "@gondola", DataType.Parametro)
            Upd.Where("idinvciclico = @idinvciclico")

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

            cmd.Parameters.Add(New SqlParameter("@IDINVCICLICO", oBeTrans_inv_ciclico.IdInvCiclico))
            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_ciclico.Idinventarioenc))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeTrans_inv_ciclico.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_inv_ciclico.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeTrans_inv_ciclico.IdProductoEstado))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeTrans_inv_ciclico.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeTrans_inv_ciclico.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@ESNUEVO", oBeTrans_inv_ciclico.EsNuevo))
            cmd.Parameters.Add(New SqlParameter("@LOTE_STOCK", IIf(oBeTrans_inv_ciclico.Lote_stock.Trim = "", "", oBeTrans_inv_ciclico.Lote_stock.Trim)))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeTrans_inv_ciclico.Lote))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE_STOCK", oBeTrans_inv_ciclico.Fecha_vence_stock))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeTrans_inv_ciclico.Fecha_vence))
            cmd.Parameters.Add(New SqlParameter("@CANT_STOCK", oBeTrans_inv_ciclico.Cant_stock))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_inv_ciclico.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@CANT_RECONTEO", oBeTrans_inv_ciclico.Cant_reconteo))
            cmd.Parameters.Add(New SqlParameter("@PESO_STOCK", oBeTrans_inv_ciclico.Peso_stock))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeTrans_inv_ciclico.Peso))
            cmd.Parameters.Add(New SqlParameter("@PESO_RECONTEO", oBeTrans_inv_ciclico.Peso_reconteo))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeTrans_inv_ciclico.Idoperador))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_inv_ciclico.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_inv_ciclico.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOEST_NUEVO", IIf(oBeTrans_inv_ciclico.IdProductoEst_nuevo = 0, 0, oBeTrans_inv_ciclico.IdProductoEst_nuevo)))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION_NUEVO", IIf(oBeTrans_inv_ciclico.IdPresentacion_nuevo = 0, 0, oBeTrans_inv_ciclico.IdPresentacion_nuevo)))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION_NUEVO", IIf(oBeTrans_inv_ciclico.IdUbicacion_nuevo = 0, 0, oBeTrans_inv_ciclico.IdUbicacion_nuevo)))
            cmd.Parameters.Add(New SqlParameter("@ESPALLET", oBeTrans_inv_ciclico.EsPallet))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeTrans_inv_ciclico.lic_plate))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeTrans_inv_ciclico.IdUnidadMedida))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_inv_ciclico.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_inv_ciclico.Fec_Mod))
            cmd.Parameters.Add(New SqlParameter("@REGULARIZAR", oBeTrans_inv_ciclico.Regularizar))
            cmd.Parameters.Add(New SqlParameter("@IdProductoTallaColor", oBeTrans_inv_ciclico.IdProductoTallaColor))
            cmd.Parameters.Add(New SqlParameter("@GONDOLA", oBeTrans_inv_ciclico.Gondola))

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

    Public Shared Function Eliminar(ByRef oBeTrans_inv_ciclico As clsBeTrans_inv_ciclico, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_inv_ciclico" &
             "  Where(idinvciclico = @idinvciclico)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDINVCICLICO", oBeTrans_inv_ciclico.IdInvCiclico))


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

            Const sp As String = " Delete from Trans_inv_ciclico"
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

            Const sp As String = "SELECT * FROM Trans_inv_ciclico"
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

    Public Shared Function Obtener(ByRef oBeTrans_inv_ciclico As clsBeTrans_inv_ciclico) As Boolean

        Try

            Const sp As String = "SELECT * FROM Trans_inv_ciclico" &
            " Where(idinvciclico = @idinvciclico)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDINVCICLICO", oBeTrans_inv_ciclico.IdInvCiclico))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_inv_ciclico, dt.Rows(0))
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

    Public Shared Function GetAll() As List(Of clsBeTrans_inv_ciclico)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_ciclico)
            Const sp As String = "SELECT * FROM Trans_inv_ciclico"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_inv_ciclico As New clsBeTrans_inv_ciclico

            For Each dr As DataRow In dt.Rows
                vBeTrans_inv_ciclico = New clsBeTrans_inv_ciclico
                Cargar(vBeTrans_inv_ciclico, dr)
                lReturnList.Add(vBeTrans_inv_ciclico)
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

    Public Shared Function Get_All_By_IdInventario(ByVal pIdInventario As Integer) As List(Of clsBeTrans_inv_ciclico)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Get_All_By_IdInventario = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lReturnList As New List(Of clsBeTrans_inv_ciclico)
            Const sp As String = "SELECT * FROM Trans_inv_ciclico where idinventarioenc=@idinventarioenc"
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@idinventarioenc", pIdInventario))

            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_inv_ciclico As New clsBeTrans_inv_ciclico

            For Each dr As DataRow In dt.Rows
                vBeTrans_inv_ciclico = New clsBeTrans_inv_ciclico
                Cargar(vBeTrans_inv_ciclico, dr)
                lReturnList.Add(vBeTrans_inv_ciclico)
            Next

            lTransaction.Commit()

            Get_All_By_IdInventario = lReturnList

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_All_By_IdInventarioEnc_And_IdStock(ByVal pIdInventario As Integer, ByVal IdStock As Integer) As List(Of clsBeTrans_inv_ciclico)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_ciclico)
            Const sp As String = "SELECT * FROM Trans_inv_ciclico where idinventarioenc=@idinventarioenc and trans_inv_ciclico.IdStock=@IdStock"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@idinventarioenc", pIdInventario))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdStock", IdStock))

            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_inv_ciclico As New clsBeTrans_inv_ciclico

            For Each dr As DataRow In dt.Rows
                vBeTrans_inv_ciclico = New clsBeTrans_inv_ciclico
                Cargar(vBeTrans_inv_ciclico, dr)
                lReturnList.Add(vBeTrans_inv_ciclico)
            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeTrans_inv_ciclico As clsBeTrans_inv_ciclico)

        Try

            Const sp As String = "SELECT * FROM Trans_inv_ciclico" &
            " Where(idinvciclico = @idinvciclico)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDINVCICLICO", pBeTrans_inv_ciclico.IdInvCiclico))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTrans_inv_ciclico, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingleByStock(ByRef pBeTrans_inv_ciclico As clsBeTrans_inv_ciclico)

        Try

            Const sp As String = "SELECT * FROM Trans_inv_ciclico" &
            " Where(IdStock = @IdStock AND idinventarioenc=@idinventarioenc AND IdProductoBodega=@IdProductoBodega)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDSTOCK", pBeTrans_inv_ciclico.IdStock))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", pBeTrans_inv_ciclico.Idinventarioenc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", pBeTrans_inv_ciclico.IdProductoBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTrans_inv_ciclico, dt.Rows(0))
            Else
                Cargar(pBeTrans_inv_ciclico, dt.Rows(0))
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

            Const sp As String = "SELECT ISNULL(Max(idinvciclico),0) FROM Trans_inv_ciclico"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                        Dim lReturnValue As Object = lCommand.ExecuteScalar()
                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lMax = CInt(lReturnValue) + 1
                        End If
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lMax

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByRef lConnection As SqlConnection,
                                 ByRef lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(idinvciclico),0) FROM Trans_inv_ciclico"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue) + 1
                End If
            End Using

            Return lMax

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar_By_IdUbicacion(ByRef oBeTrans_inv_ciclico As clsBeTrans_inv_ciclico, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_inv_ciclico")
            Upd.Add("idoperador", "@idoperador", DataType.Parametro)
            Upd.Where("idubicacion = @idubicacion and idinventarioenc=@idinventarioenc")

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

            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeTrans_inv_ciclico.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADOR", IIf(oBeTrans_inv_ciclico.Idoperador = 0, DBNull.Value, oBeTrans_inv_ciclico.Idoperador)))
            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_ciclico.Idinventarioenc))


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

    Public Shared Function Actualizar_By_IdProducto(ByRef oBeTrans_inv_ciclico As clsBeTrans_inv_ciclico, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_inv_ciclico")
            Upd.Add("idoperador", "@idoperador", DataType.Parametro)
            Upd.Where("IdProductoBodega = @IdProductoBodega AND idinventarioenc=@idinventarioenc")

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

            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_inv_ciclico.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADOR", IIf(oBeTrans_inv_ciclico.Idoperador = 0, DBNull.Value, oBeTrans_inv_ciclico.Idoperador)))
            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_ciclico.Idinventarioenc))


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

    Public Shared Function Get_All_By_IdInventarioEnc_And_IdStock(ByVal pIdInventario As Integer,
                                                                  ByVal IdStock As Integer,
                                                                  ByVal lConnection As SqlConnection,
                                                                  ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_inv_ciclico)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_ciclico)
            Const sp As String = "SELECT * FROM Trans_inv_ciclico where idinventarioenc=@idinventarioenc and trans_inv_ciclico.IdStock=@IdStock"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@idinventarioenc", pIdInventario))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdStock", IdStock))

            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_inv_ciclico As New clsBeTrans_inv_ciclico

            For Each dr As DataRow In dt.Rows
                vBeTrans_inv_ciclico = New clsBeTrans_inv_ciclico
                Cargar(vBeTrans_inv_ciclico, dr)
                lReturnList.Add(vBeTrans_inv_ciclico)
            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdInventario(ByVal pIdInventario As Integer, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_inv_ciclico)

        Get_All_By_IdInventario = Nothing

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_ciclico)
            Const sp As String = "SELECT * FROM Trans_inv_ciclico where idinventarioenc=@idinventarioenc"
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@idinventarioenc", pIdInventario))

            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_inv_ciclico As New clsBeTrans_inv_ciclico

            For Each dr As DataRow In dt.Rows
                vBeTrans_inv_ciclico = New clsBeTrans_inv_ciclico
                Cargar(vBeTrans_inv_ciclico, dr)
                lReturnList.Add(vBeTrans_inv_ciclico)
            Next

            Get_All_By_IdInventario = lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_BeTransInvCiclico_By_IdUbicacion(IdinventarioEnc As Integer, vIdOperador As Integer, IdUbicacion As Integer) As List(Of clsBeTrans_inv_ciclico)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_ciclico)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM trans_inv_ciclico
                                      WHERE (trans_inv_ciclico.idinventarioenc = @idinventario AND                                      
                                             trans_inv_ciclico.IdUbicacion = @IdUbicacion )"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@idinventario", IdinventarioEnc)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", IdUbicacion)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeTrans_inv_ciclico

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeTrans_inv_ciclico()
                            Cargar(Obj, lRow)
                            lReturnList.Add(Obj)

                        Next

                    End If

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar_Regularizar_By_IdInventarioEnc_And_IdInvCiclico(ByRef oBeTrans_inv_ciclico As clsBeTrans_inv_ciclico,
                                                                                      Optional ByVal pConection As SqlConnection = Nothing,
                                                                                      Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_inv_ciclico")
            Upd.Add("regularizar", "@regularizar", DataType.Parametro)
            Upd.Where("IdInvCiclico = @IdInvCiclico and idinventarioenc=@idinventarioenc")

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

            cmd.Parameters.Add(New SqlParameter("@REGULARIZAR", oBeTrans_inv_ciclico.Regularizar))
            cmd.Parameters.Add(New SqlParameter("@IDINVCICLICO", oBeTrans_inv_ciclico.IdInvCiclico))
            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_ciclico.Idinventarioenc))

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

    Public Shared Function Actualizar_Regularizar_By_IdInventarioEnc(ByVal pIdInventarioEnc As Integer,
                                                                     ByVal pRegularizar As Boolean,
                                                                     Optional ByVal pConection As SqlConnection = Nothing,
                                                                     Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_inv_ciclico")
            Upd.Add("regularizar", "@regularizar", DataType.Parametro)
            Upd.Where("idinventarioenc=@idinventarioenc")

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

            cmd.Parameters.Add(New SqlParameter("@REGULARIZAR", pRegularizar))
            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", pIdInventarioEnc))

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

End Class
