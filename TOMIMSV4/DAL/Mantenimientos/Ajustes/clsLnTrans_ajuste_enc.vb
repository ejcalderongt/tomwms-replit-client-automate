Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_ajuste_enc

    Public Shared Sub Cargar(ByRef oBeTrans_ajuste_enc As clsBeTrans_ajuste_enc, ByRef dr As DataRow)

        Try

            With oBeTrans_ajuste_enc

                .Idajusteenc = IIf(IsDBNull(dr.Item("idajusteenc")), 0, dr.Item("idajusteenc"))
                .Fecha = IIf(IsDBNull(dr.Item("fecha")), Nothing, dr.Item("fecha"))
                .Idusuario = IIf(IsDBNull(dr.Item("idusuario")), 0, dr.Item("idusuario"))
                .Referencia = IIf(IsDBNull(dr.Item("referencia")), "", dr.Item("referencia"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .IdBodega = IIf(IsDBNull(dr.Item("idbodega")), 0, dr.Item("idbodega"))
                .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                .IdProductoFamilia = IIf(IsDBNull(dr.Item("IdProductoFamilia")), 0, dr.Item("IdProductoFamilia"))
                .Enviado_A_ERP = IIf(IsDBNull(dr.Item("enviado_a_erp")), 0, dr.Item("enviado_a_erp"))
                .Ajuste_Por_Inventario = IIf(IsDBNull(dr.Item("ajuste_por_inventario")), 0, dr.Item("ajuste_por_inventario"))
                .IdCentroCosto = IIf(IsDBNull(dr.Item("IdCentroCosto")), 0, dr.Item("IdCentroCosto"))
                .Auditado = IIf(IsDBNull(dr.Item("Auditado")), 0, dr.Item("Auditado"))

            End With

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_ajuste_enc As clsBeTrans_ajuste_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_ajuste_enc")
            Ins.Add("idajusteenc", "@idajusteenc", DataType.Parametro)
            Ins.Add("fecha", "@fecha", DataType.Parametro)
            Ins.Add("idusuario", "@idusuario", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("referencia", "@referencia", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("idproductofamilia", "@idproductofamilia", DataType.Parametro)
            Ins.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Ins.Add("enviado_a_erp", "@enviado_a_erp", DataType.Parametro)
            Ins.Add("ajuste_por_inventario", "@ajuste_por_inventario", DataType.Parametro)
            Ins.Add("idcentrocosto", "@idcentrocosto", DataType.Parametro)
            Ins.Add("auditado", "@auditado", DataType.Parametro)

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

            cmd.Parameters.Add(New SqlParameter("@IDAJUSTEENC", oBeTrans_ajuste_enc.Idajusteenc))
            cmd.Parameters.Add(New SqlParameter("@FECHA", oBeTrans_ajuste_enc.Fecha))
            cmd.Parameters.Add(New SqlParameter("@IDUSUARIO", oBeTrans_ajuste_enc.Idusuario))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_ajuste_enc.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIA", oBeTrans_ajuste_enc.Referencia))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_ajuste_enc.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_ajuste_enc.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_ajuste_enc.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_ajuste_enc.User_mod))
            cmd.Parameters.Add(New SqlParameter("@ENVIADO_A_ERP", oBeTrans_ajuste_enc.Enviado_A_ERP))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOFAMILIA", oBeTrans_ajuste_enc.IdProductoFamilia))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_ajuste_enc.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@AJUSTE_POR_INVENTARIO", oBeTrans_ajuste_enc.Ajuste_Por_Inventario))
            cmd.Parameters.Add(New SqlParameter("@IDCENTROCOSTO", oBeTrans_ajuste_enc.IdCentroCosto))
            cmd.Parameters.Add(New SqlParameter("@AUDITADO", oBeTrans_ajuste_enc.Auditado))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

            oBeTrans_ajuste_enc.Idajusteenc = CInt(cmd.Parameters("@IDAJUSTEENC").Value)

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function
    Public Shared Function Actualizar(ByRef oBeTrans_ajuste_enc As clsBeTrans_ajuste_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_ajuste_enc")
            Upd.Add("idajusteenc", "@idajusteenc", DataType.Parametro)
            Upd.Add("fecha", "@fecha", DataType.Parametro)
            Upd.Add("idusuario", "@idusuario", DataType.Parametro)
            Upd.Add("referencia", "@referencia", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("enviado_a_erp", "@enviado_a_erp", DataType.Parametro)
            Upd.Add("idproductofamilia", "@idproductofamilia", DataType.Parametro)
            Upd.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Upd.Add("ajuste_por_inventario", "@ajuste_por_inventario", DataType.Parametro)
            Upd.Add("idcentrocosto", "@idcentrocosto", DataType.Parametro)
            Upd.Add("auditado", "@auditado", DataType.Parametro)
            Upd.Where("idajusteenc = @idajusteenc")

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

            cmd.Parameters.Add(New SqlParameter("@IDAJUSTEENC", oBeTrans_ajuste_enc.Idajusteenc))
            cmd.Parameters.Add(New SqlParameter("@FECHA", oBeTrans_ajuste_enc.Fecha))
            cmd.Parameters.Add(New SqlParameter("@IDUSUARIO", oBeTrans_ajuste_enc.Idusuario))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIA", oBeTrans_ajuste_enc.Referencia))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_ajuste_enc.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_ajuste_enc.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_ajuste_enc.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_ajuste_enc.User_mod))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_ajuste_enc.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@ENVIADO_A_ERP", oBeTrans_ajuste_enc.Enviado_A_ERP))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOFAMILIA", oBeTrans_ajuste_enc.IdProductoFamilia))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_ajuste_enc.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@AJUSTE_POR_INVENTARIO", oBeTrans_ajuste_enc.Ajuste_Por_Inventario))
            cmd.Parameters.Add(New SqlParameter("@IDCENTROCOSTO", oBeTrans_ajuste_enc.IdCentroCosto))
            cmd.Parameters.Add(New SqlParameter("@AUDITADO", oBeTrans_ajuste_enc.Auditado))

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
    Public Shared Function Eliminar(ByRef oBeTrans_ajuste_enc As clsBeTrans_ajuste_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_ajuste_enc " &
             "  Where(idajusteenc = @idajusteenc)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDAJUSTEENC", oBeTrans_ajuste_enc.Idajusteenc))


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
    Public Shared Function GetSingle(ByRef pBeTrans_ajuste_enc As clsBeTrans_ajuste_enc)

        Try

            Const sp As String = "SELECT * FROM Trans_ajuste_enc" &
            " Where(idajusteenc = @idajusteenc)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDAJUSTEENC", pBeTrans_ajuste_enc.Idajusteenc))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTrans_ajuste_enc, dt.Rows(0))
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

            Const sp As String = "SELECT ISNULL(Max(idajusteenc),0) FROM Trans_ajuste_enc"

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

    Public Shared Function Inserta_Encabezado_Ajuste(ByVal IdInventario As Integer,
                                                     ByVal Usuario As clsBeUsuario,
                                                     ByVal IdBodega As Integer,
                                                     ByVal IdPropietario As Integer,
                                                     ByVal EsPorInventario As Boolean,
                                                     ByVal IdCentroCosto As Integer,
                                                     ByVal pReferencia As String,
                                                     ByVal lConnection As SqlConnection,
                                                     ByVal lTransaction As SqlTransaction) As Integer
        Try
            ' Obtener el nuevo ID de ajuste
            Dim NuevoIdAjusteEnc As Integer = MaxID(lConnection, lTransaction) + 1

            ' Inicializar la clase de inserción
            Ins.Init("trans_ajuste_enc")

            ' Agregar columnas y par�metros
            Ins.Add("idajusteenc", "@IdAjusteEnc", DataType.Parametro)
            Ins.Add("fecha", "@Fecha", DataType.Parametro)
            Ins.Add("idusuario", "@IdUsuario", DataType.Parametro)
            Ins.Add("referencia", "@Referencia", DataType.Parametro)
            Ins.Add("fec_agr", "@FecAgr", DataType.Parametro)
            Ins.Add("user_agr", "@UserAgr", DataType.Parametro)
            Ins.Add("fec_mod", "@FecMod", DataType.Parametro)
            Ins.Add("user_mod", "@UserMod", DataType.Parametro)
            Ins.Add("idbodega", "@IdBodega", DataType.Parametro)
            Ins.Add("enviado_a_erp", "@EnviadoAERP", DataType.Parametro)
            Ins.Add("idpropietariobodega", "@IdPropietarioBodega", DataType.Parametro)
            Ins.Add("ajuste_por_inventario", "@AjustePorInventario", DataType.Parametro)
            Ins.Add("idcentrocosto", "@IdCentroCosto", DataType.Parametro)
            Ins.Add("auditado", "@Auditado", DataType.Parametro)

            ' Generar el query de inserci�n
            Dim query As String = Ins.SQL()


            Using cmd As New SqlCommand(query, lConnection, lTransaction)
                cmd.Parameters.AddWithValue("@IdAjusteEnc", NuevoIdAjusteEnc)
                cmd.Parameters.AddWithValue("@Fecha", DateTime.Now)
                cmd.Parameters.AddWithValue("@IdUsuario", Usuario.IdUsuario)
                cmd.Parameters.AddWithValue("@Referencia", $"Ajuste {pReferencia} generado por inventario No. {IdInventario}")
                cmd.Parameters.AddWithValue("@FecAgr", DateTime.Now)
                cmd.Parameters.AddWithValue("@UserAgr", Usuario.Nombres)
                cmd.Parameters.AddWithValue("@FecMod", DateTime.Now)
                cmd.Parameters.AddWithValue("@UserMod", Usuario.Nombres)
                cmd.Parameters.AddWithValue("@IdBodega", IdBodega)
                cmd.Parameters.AddWithValue("@EnviadoAERP", True)
                cmd.Parameters.AddWithValue("@IdPropietarioBodega", IdPropietario)
                cmd.Parameters.AddWithValue("@AjustePorInventario", IIf(EsPorInventario, IdInventario, 0))
                cmd.Parameters.AddWithValue("@IdCentroCosto", IdCentroCosto)
                cmd.Parameters.AddWithValue("@Auditado", False)

                cmd.ExecuteNonQuery()
            End Using

            ' Devolver el nuevo ID generado
            Return NuevoIdAjusteEnc

        Catch ex As Exception
            Throw New Exception($"Error en Inserta_Encabezado_Ajuste: {ex.Message}")
        End Try
    End Function

    Public Shared Function MaxID(Optional ByVal lConnection As SqlConnection = Nothing, Optional ByVal lTransaction As SqlTransaction = Nothing) As Integer
        Try
            Dim lMax As Integer = 0
            Const sp As String = "SELECT ISNULL(MAX(idajusteenc), 0) FROM Trans_ajuste_enc"

            Dim isExternalConnection As Boolean = (lConnection IsNot Nothing)

            ' Si no hay una conexi�n externa, creamos una nueva
            If Not isExternalConnection Then
                lConnection = New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
            End If

            Using lCommand As New SqlCommand(sp, lConnection)
                lCommand.CommandType = CommandType.Text

                ' Si hay una transacci�n proporcionada, se asigna al comando
                If lTransaction IsNot Nothing Then
                    lCommand.Transaction = lTransaction
                End If

                ' Ejecutar el comando y obtener el valor m�ximo
                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If
            End Using

            ' Si la conexi�n no fue proporcionada externamente, la cerramos
            If Not isExternalConnection Then
                lConnection.Close()
            End If

            Return lMax

        Catch ex As Exception
            ' Manejo de errores
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(vMsgError, ex)
        End Try
    End Function


End Class
