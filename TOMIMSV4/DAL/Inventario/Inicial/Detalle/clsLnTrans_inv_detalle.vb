Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_inv_detalle

    Public Shared Sub Cargar(ByRef oBeTrans_inv_detalle As clsBeTrans_inv_detalle, ByRef dr As DataRow)

        Try

            With oBeTrans_inv_detalle

                .Idinventariodet = IIf(IsDBNull(dr.Item("idinventariodet")), 0, dr.Item("idinventariodet"))
                .Idinventarioenc = IIf(IsDBNull(dr.Item("idinventarioenc")), 0, dr.Item("idinventarioenc"))
                .Idtramo = IIf(IsDBNull(dr.Item("idtramo")), 0, dr.Item("idtramo"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .IdUbicacion = IIf(IsDBNull(dr.Item("IdUbicacion")), 0, dr.Item("IdUbicacion"))
                .Idoperador = IIf(IsDBNull(dr.Item("idoperador")), 0, dr.Item("idoperador"))
                .Idproducto = IIf(IsDBNull(dr.Item("idproducto")), 0, dr.Item("idproducto"))
                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .Idunidadmedida = IIf(IsDBNull(dr.Item("idunidadmedida")), 0, dr.Item("idunidadmedida"))
                .Lote = IIf(IsDBNull(dr.Item("lote")), "", dr.Item("lote"))
                .Fecha_vence = IIf(IsDBNull(dr.Item("fecha_vence")), Nothing, dr.Item("fecha_vence"))
                .Serie = IIf(IsDBNull(dr.Item("serie")), "", dr.Item("serie"))
                .Idproductoestado = IIf(IsDBNull(dr.Item("idproductoestado")), "", dr.Item("idproductoestado"))
                .Cantidad = IIf(IsDBNull(dr.Item("cantidad")), 0.0, dr.Item("cantidad"))
                .Fecha_captura = IIf(IsDBNull(dr.Item("fecha_captura")), Date.Now, dr.Item("fecha_captura"))
                .Host = IIf(IsDBNull(dr.Item("host")), "", dr.Item("host"))
                .Nom_producto = IIf(IsDBNull(dr.Item("nom_producto")), "", dr.Item("nom_producto"))
                .Nom_operador = IIf(IsDBNull(dr.Item("nom_operador")), "", dr.Item("nom_operador"))
                .Carga = IIf(IsDBNull(dr.Item("carga")), 0, dr.Item("carga"))
                .Peso = IIf(IsDBNull(dr.Item("Peso")), 0, dr.Item("Peso"))
                .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                .nombre_propietario = IIf(IsDBNull(dr.Item("nombre_propietario")), "", dr.Item("nombre_propietario"))
                .License_plate = IIf(IsDBNull(dr.Item("lic_plate")), 0, dr.Item("lic_plate"))
                .Codigo_variante = IIf(IsDBNull(dr.Item("cod_variante")), "", dr.Item("cod_variante"))
                .IdProductoTallaColor = IIf(IsDBNull(dr.Item("IdProductoTallaColor")), "", dr.Item("IdProductoTallaColor"))

            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_inv_detalle As clsBeTrans_inv_detalle, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_inv_detalle")
            Ins.Add("idinventariodet", "@idinventariodet", DataType.Parametro)
            Ins.Add("idinventarioenc", "@idinventarioenc", DataType.Parametro)
            Ins.Add("idtramo", "@idtramo", DataType.Parametro)
            Ins.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Ins.Add("idoperador", "@idoperador", DataType.Parametro)
            Ins.Add("idproducto", "@idproducto", DataType.Parametro)
            Ins.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Ins.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Ins.Add("lote", "@lote", DataType.Parametro)
            Ins.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Ins.Add("serie", "@serie", DataType.Parametro)
            Ins.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Ins.Add("cantidad", "@cantidad", DataType.Parametro)
            Ins.Add("fecha_captura", "@fecha_captura", DataType.Parametro)
            Ins.Add("host", "@host", DataType.Parametro)
            Ins.Add("nom_producto", "@nom_producto", DataType.Parametro)
            Ins.Add("nom_operador", "@nom_operador", DataType.Parametro)
            Ins.Add("carga", "@carga", DataType.Parametro)
            Ins.Add("peso", "@peso", DataType.Parametro)

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

            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIODET", oBeTrans_inv_detalle.Idinventariodet))
            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_detalle.Idinventarioenc))
            cmd.Parameters.Add(New SqlParameter("@IDTRAMO", oBeTrans_inv_detalle.Idtramo))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeTrans_inv_detalle.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeTrans_inv_detalle.Idoperador))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeTrans_inv_detalle.Idproducto))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeTrans_inv_detalle.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeTrans_inv_detalle.Idunidadmedida))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeTrans_inv_detalle.Lote))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeTrans_inv_detalle.Fecha_vence))
            cmd.Parameters.Add(New SqlParameter("@SERIE", oBeTrans_inv_detalle.Serie))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeTrans_inv_detalle.Idproductoestado))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_inv_detalle.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@FECHA_CAPTURA", oBeTrans_inv_detalle.Fecha_captura))
            cmd.Parameters.Add(New SqlParameter("@HOST", oBeTrans_inv_detalle.Host))
            cmd.Parameters.Add(New SqlParameter("@NOM_PRODUCTO", oBeTrans_inv_detalle.Nom_producto))
            cmd.Parameters.Add(New SqlParameter("@NOM_OPERADOR", oBeTrans_inv_detalle.Nom_operador))
            cmd.Parameters.Add(New SqlParameter("@CARGA", oBeTrans_inv_detalle.Carga))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeTrans_inv_detalle.Peso))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeTrans_inv_detalle As clsBeTrans_inv_detalle, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_inv_detalle")
            Upd.Add("idinventarioenc", "@idinventarioenc", DataType.Parametro)
            Upd.Add("idtramo", "@idtramo", DataType.Parametro)
            Upd.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Upd.Add("idoperador", "@idoperador", DataType.Parametro)
            Upd.Add("idproducto", "@idproducto", DataType.Parametro)
            Upd.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Upd.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Upd.Add("lote", "@lote", DataType.Parametro)
            Upd.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Upd.Add("serie", "@serie", DataType.Parametro)
            Upd.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Add("fecha_captura", "@fecha_captura", DataType.Parametro)
            Upd.Add("host", "@host", DataType.Parametro)
            Upd.Add("nom_producto", "@nom_producto", DataType.Parametro)
            Upd.Add("nom_operador", "@nom_operador", DataType.Parametro)
            Upd.Add("carga", "@carga", DataType.Parametro)
            Upd.Add("peso", "@peso", DataType.Parametro)
            Upd.Where("idinventariodet = @idinventariodet")

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

            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIODET", oBeTrans_inv_detalle.Idinventariodet))
            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_detalle.Idinventarioenc))
            cmd.Parameters.Add(New SqlParameter("@IDTRAMO", oBeTrans_inv_detalle.Idtramo))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeTrans_inv_detalle.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeTrans_inv_detalle.Idoperador))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeTrans_inv_detalle.Idproducto))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeTrans_inv_detalle.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeTrans_inv_detalle.Idunidadmedida))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeTrans_inv_detalle.Lote))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeTrans_inv_detalle.Fecha_vence))
            cmd.Parameters.Add(New SqlParameter("@SERIE", oBeTrans_inv_detalle.Serie))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeTrans_inv_detalle.Idproductoestado))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_inv_detalle.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@FECHA_CAPTURA", oBeTrans_inv_detalle.Fecha_captura))
            cmd.Parameters.Add(New SqlParameter("@HOST", oBeTrans_inv_detalle.Host))
            cmd.Parameters.Add(New SqlParameter("@NOM_PRODUCTO", oBeTrans_inv_detalle.Nom_producto))
            cmd.Parameters.Add(New SqlParameter("@NOM_OPERADOR", oBeTrans_inv_detalle.Nom_operador))
            cmd.Parameters.Add(New SqlParameter("@CARGA", oBeTrans_inv_detalle.Carga))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeTrans_inv_detalle.Peso))

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

    Public Shared Function Eliminar(ByRef oBeTrans_inv_detalle As clsBeTrans_inv_detalle, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_inv_detalle" &
             "  Where(idinventariodet = @idinventariodet)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIODET", oBeTrans_inv_detalle.Idinventariodet))

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

            Const sp As String = " Delete from Trans_inv_detalle"
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

            Const sp As String = "SELECT * FROM Trans_inv_detalle"
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

    Public Shared Function Obtener(ByRef oBeTrans_inv_detalle As clsBeTrans_inv_detalle) As Boolean

        Try

            Const sp As String = "SELECT * FROM Trans_inv_detalle" &
            " Where(idinventariodet = @idinventariodet)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDINVENTARIODET", oBeTrans_inv_detalle.Idinventariodet))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_inv_detalle, dt.Rows(0))
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

    Public Shared Function GetAll() As List(Of clsBeTrans_inv_detalle)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_detalle)
            Const sp As String = "SELECT * FROM Trans_inv_detalle"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_inv_detalle As New clsBeTrans_inv_detalle

            For Each dr As DataRow In dt.Rows
                vBeTrans_inv_detalle = New clsBeTrans_inv_detalle
                Cargar(vBeTrans_inv_detalle, dr)
                lReturnList.Add(vBeTrans_inv_detalle)
            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeTrans_inv_detalle As clsBeTrans_inv_detalle)

        Try

            Const sp As String = "SELECT * FROM Trans_inv_detalle" &
            " Where(idinventariodet = @idinventariodet)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDINVENTARIODET", pBeTrans_inv_detalle.IDINVENTARIODET))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTrans_inv_detalle, dt.Rows(0))
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

            Const sp As String = "SELECT ISNULL(Max(idinventariodet),0) FROM Trans_inv_detalle"

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

            Const sp As String = "SELECT ISNULL(Max(idinventariodet),0) FROM Trans_inv_detalle"

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
