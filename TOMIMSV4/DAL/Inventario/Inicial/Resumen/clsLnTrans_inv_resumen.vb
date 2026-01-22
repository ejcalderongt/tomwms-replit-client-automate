Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_inv_resumen

    Public Shared Sub Cargar(ByRef oBeTrans_inv_resumen As clsBeTrans_inv_resumen, ByRef dr As DataRow)
        Try
            With oBeTrans_inv_resumen
                .Idinventariores = IIf(IsDBNull(dr.Item("idinventariores")), 0, dr.Item("idinventariores"))
                .Idinventarioenct = IIf(IsDBNull(dr.Item("idinventarioenct")), 0, dr.Item("idinventarioenct"))
                .Idtramo = IIf(IsDBNull(dr.Item("idtramo")), 0, dr.Item("idtramo"))
                .Idproducto = IIf(IsDBNull(dr.Item("idproducto")), 0, dr.Item("idproducto"))
                .Idoperador = IIf(IsDBNull(dr.Item("idoperador")), 0, dr.Item("idoperador"))
                .IdUnidadMedida = IIf(IsDBNull(dr.Item("IdUnidadMedida")), 0, dr.Item("IdUnidadMedida"))
                .Idpresentacion = IIf(IsDBNull(dr.Item("idpresentacion")), 0, dr.Item("idpresentacion"))
                .Idproductoestado = IIf(IsDBNull(dr.Item("idproductoestado")), 0, dr.Item("idproductoestado"))
                .Cantidad = IIf(IsDBNull(dr.Item("cantidad")), 0.0, dr.Item("cantidad"))
                .Fecha_captura = IIf(IsDBNull(dr.Item("fecha_captura")), Date.Now, dr.Item("fecha_captura"))
                .Host = IIf(IsDBNull(dr.Item("host")), "", dr.Item("host"))
                .Nom_producto = IIf(IsDBNull(dr.Item("nom_producto")), "", dr.Item("nom_producto"))
                .Nom_operador = IIf(IsDBNull(dr.Item("nom_operador")), "", dr.Item("nom_operador"))
                .IdUbicacion = IIf(IsDBNull(dr.Item("idubicacion")), 0, dr.Item("idubicacion"))
                .Lic_plate = IIf(IsDBNull(dr.Item("lic_plate")), 0, dr.Item("lic_plate"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_inv_resumen As clsBeTrans_inv_resumen, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_inv_resumen")
            Ins.Add("idinventariores", "@idinventariores", DataType.Parametro)
            Ins.Add("idinventarioenct", "@idinventarioenct", DataType.Parametro)
            Ins.Add("idtramo", "@idtramo", DataType.Parametro)
            Ins.Add("idproducto", "@idproducto", DataType.Parametro)
            Ins.Add("idoperador", "@idoperador", DataType.Parametro)
            Ins.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Ins.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Ins.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Ins.Add("cantidad", "@cantidad", DataType.Parametro)
            Ins.Add("fecha_captura", "@fecha_captura", DataType.Parametro)
            Ins.Add("host", "@host", DataType.Parametro)
            Ins.Add("nom_producto", "@nom_producto", DataType.Parametro)
            Ins.Add("nom_operador", "@nom_operador", DataType.Parametro)
            Ins.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("lic_plate", "@lic_plate", DataType.Parametro)
            Ins.Add("idproductotallacolor", "@idproductotallacolor", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
                oBeTrans_inv_resumen.Idinventariores = MaxID(pConection, pTransaction) + 1
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
                oBeTrans_inv_resumen.Idinventariores = MaxID(lConnection, lTransaction) + 1
            End If

            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIORES", oBeTrans_inv_resumen.Idinventariores))
            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIOENCT", oBeTrans_inv_resumen.Idinventarioenct))
            cmd.Parameters.Add(New SqlParameter("@IDTRAMO", oBeTrans_inv_resumen.Idtramo))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeTrans_inv_resumen.Idproducto))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeTrans_inv_resumen.Idoperador))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeTrans_inv_resumen.IdUnidadMedida))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeTrans_inv_resumen.Idpresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeTrans_inv_resumen.Idproductoestado))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_inv_resumen.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@FECHA_CAPTURA", oBeTrans_inv_resumen.Fecha_captura))
            cmd.Parameters.Add(New SqlParameter("@HOST", oBeTrans_inv_resumen.Host))
            cmd.Parameters.Add(New SqlParameter("@NOM_PRODUCTO", oBeTrans_inv_resumen.Nom_producto))
            cmd.Parameters.Add(New SqlParameter("@NOM_OPERADOR", oBeTrans_inv_resumen.Nom_operador))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeTrans_inv_resumen.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_inv_resumen.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeTrans_inv_resumen.Lic_plate))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOTALLACOLOR", oBeTrans_inv_resumen.IdProductoTallaColor))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

            oBeTrans_inv_resumen.Idinventariores = CInt(cmd.Parameters("@IDINVENTARIORES").Value)

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeTrans_inv_resumen As clsBeTrans_inv_resumen, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_inv_resumen")
            Upd.Add("idinventariores", "@idinventariores", DataType.Parametro)
            Upd.Add("idinventarioenct", "@idinventarioenct", DataType.Parametro)
            Upd.Add("idtramo", "@idtramo", DataType.Parametro)
            Upd.Add("idproducto", "@idproducto", DataType.Parametro)
            Upd.Add("idoperador", "@idoperador", DataType.Parametro)
            Upd.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Upd.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Upd.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Add("fecha_captura", "@fecha_captura", DataType.Parametro)
            Upd.Add("host", "@host", DataType.Parametro)
            Upd.Add("nom_producto", "@nom_producto", DataType.Parametro)
            Upd.Add("nom_operador", "@nom_operador", DataType.Parametro)
            Upd.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Upd.Add("lic_plate", "@lic_plate", DataType.Parametro)
            Upd.Where("idinventariores = @idinventariores")

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

            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIORES", oBeTrans_inv_resumen.Idinventariores))
            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIOENCT", oBeTrans_inv_resumen.Idinventarioenct))
            cmd.Parameters.Add(New SqlParameter("@IDTRAMO", oBeTrans_inv_resumen.Idtramo))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeTrans_inv_resumen.Idproducto))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeTrans_inv_resumen.Idoperador))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeTrans_inv_resumen.IdUnidadMedida))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeTrans_inv_resumen.Idpresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeTrans_inv_resumen.Idproductoestado))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_inv_resumen.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@FECHA_CAPTURA", oBeTrans_inv_resumen.Fecha_captura))
            cmd.Parameters.Add(New SqlParameter("@HOST", oBeTrans_inv_resumen.Host))
            cmd.Parameters.Add(New SqlParameter("@NOM_PRODUCTO", oBeTrans_inv_resumen.Nom_producto))
            cmd.Parameters.Add(New SqlParameter("@NOM_OPERADOR", oBeTrans_inv_resumen.Nom_operador))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeTrans_inv_resumen.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeTrans_inv_resumen.Lic_plate))
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

    Public Shared Function Eliminar(ByRef oBeTrans_inv_resumen As clsBeTrans_inv_resumen, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_inv_resumen" &
             "  Where(idinventariores = @idinventariores)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIORES", oBeTrans_inv_resumen.Idinventariores))

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

            Const sp As String = " Delete from Trans_inv_resumen"
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

            Const sp As String = "SELECT * FROM Trans_inv_resumen"
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

    Public Shared Function Obtener(ByRef oBeTrans_inv_resumen As clsBeTrans_inv_resumen) As Boolean

        Try

            Const sp As String = "SELECT * FROM Trans_inv_resumen" &
            " Where(idinventariores = @idinventariores)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDINVENTARIORES", oBeTrans_inv_resumen.Idinventariores))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_inv_resumen, dt.Rows(0))
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

    Public Shared Function GetAll() As List(Of clsBeTrans_inv_resumen)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_resumen)
            Const sp As String = "SELECT * FROM Trans_inv_resumen"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_inv_resumen As New clsBeTrans_inv_resumen

            For Each dr As DataRow In dt.Rows
                vBeTrans_inv_resumen = New clsBeTrans_inv_resumen
                Cargar(vBeTrans_inv_resumen, dr)
                lReturnList.Add(vBeTrans_inv_resumen)
            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeTrans_inv_resumen As clsBeTrans_inv_resumen)

        Try

            Const sp As String = "SELECT * FROM Trans_inv_resumen " &
            " Where(idinventariores = @idinventariores)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDINVENTARIORES", pBeTrans_inv_resumen.IDINVENTARIORES))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTrans_inv_resumen, dt.Rows(0))
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

            Const sp As String = "SELECT ISNULL(Max(idinventariores),0) FROM Trans_inv_resumen"

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

    Public Shared Function MaxID(ByRef lConnection As SqlConnection,
                                 ByRef lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(idinventariores),0) FROM Trans_inv_resumen"

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

End Class
