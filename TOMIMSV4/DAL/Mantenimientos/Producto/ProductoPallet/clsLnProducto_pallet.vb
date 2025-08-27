Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnProducto_pallet

    Public Shared Sub Cargar(ByRef oBeProducto_pallet As clsBeProducto_pallet, ByRef dr As DataRow)
        Try
            With oBeProducto_pallet
                .IdPallet = IIf(IsDBNull(dr.Item("IdPallet")), 0, dr.Item("IdPallet"))
                .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .IdOperadorBodega = IIf(IsDBNull(dr.Item("IdOperadorBodega")), 0, dr.Item("IdOperadorBodega"))
                .IdImpresora = IIf(IsDBNull(dr.Item("IdImpresora")), 0, dr.Item("IdImpresora"))
                .IdRecepcionEnc = IIf(IsDBNull(dr.Item("IdRecepcionEnc")), 0, dr.Item("IdRecepcionEnc"))
                .IdRecepcionDet = IIf(IsDBNull(dr.Item("IdRecepcionDet")), 0, dr.Item("IdRecepcionDet"))
                .Codigo_barra = IIf(IsDBNull(dr.Item("codigo_barra")), "", dr.Item("codigo_barra"))
                .Codigo_Producto = IIf(IsDBNull(dr.Item("codigo_producto")), "", dr.Item("codigo_producto"))
                .Cantidad = IIf(IsDBNull(dr.Item("cantidad")), 0.0, dr.Item("cantidad"))
                .Lote = IIf(IsDBNull(dr.Item("lote")), "", dr.Item("lote"))
                .Impreso = IIf(IsDBNull(dr.Item("Impreso")), False, dr.Item("Impreso"))
                .Reimpresiones = IIf(IsDBNull(dr.Item("Reimpresiones")), 0, dr.Item("Reimpresiones"))
                .Fecha_vence = IIf(IsDBNull(dr.Item("fecha_vence")), Date.Now, dr.Item("fecha_vence"))
                .Fecha_ingreso = IIf(IsDBNull(dr.Item("fecha_ingreso")), Date.Now, dr.Item("fecha_ingreso"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeProducto_pallet As clsBeProducto_pallet, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("producto_pallet")
            Ins.Add("idpallet", "@idpallet", DataType.Parametro)
            Ins.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Ins.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            If Not oBeProducto_pallet.IdPresentacion = 0 Then Ins.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Ins.Add("idoperadorbodega", "@idoperadorbodega", DataType.Parametro)
            If Not oBeProducto_pallet.IdImpresora = 0 Then Ins.Add("idimpresora", "@idimpresora", DataType.Parametro)
            '#AT20220513 Si IdRecepcionEnc e IdRecepcionDet no son iguales a 0 se insertan
            If Not oBeProducto_pallet.IdRecepcionEnc = 0 Then Ins.Add("idrecepcionenc", "@idrecepcionenc", DataType.Parametro)
            If Not oBeProducto_pallet.IdRecepcionDet = 0 Then Ins.Add("idrecepciondet", "@idrecepciondet", DataType.Parametro)
            Ins.Add("codigo_barra", "@codigo_barra", DataType.Parametro)
            Ins.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
            Ins.Add("cantidad", "@cantidad", DataType.Parametro)
            Ins.Add("lote", "@lote", DataType.Parametro)
            Ins.Add("impreso", "@impreso", DataType.Parametro)
            Ins.Add("reimpresiones", "@reimpresiones", DataType.Parametro)
            Ins.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Ins.Add("fecha_ingreso", "@fecha_ingreso", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)

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

            cmd.Parameters.Add(New SqlParameter("@IDPALLET", oBeProducto_pallet.IdPallet))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeProducto_pallet.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeProducto_pallet.IdProductoBodega))
            If Not oBeProducto_pallet.IdPresentacion = 0 Then cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeProducto_pallet.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA", IIf(oBeProducto_pallet.IdOperadorBodega = Nothing, DBNull.Value, oBeProducto_pallet.IdOperadorBodega)))
            If Not oBeProducto_pallet.IdImpresora = 0 Then cmd.Parameters.Add(New SqlParameter("@IDIMPRESORA", oBeProducto_pallet.IdImpresora))
            '#AT20220513 Si IdRecepcionEnc e IdRecepcionDet no son iguales a 0 se insertan
            If Not oBeProducto_pallet.IdRecepcionEnc = 0 Then cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeProducto_pallet.IdRecepcionEnc))
            If Not oBeProducto_pallet.IdRecepcionDet = 0 Then cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONDET", oBeProducto_pallet.IdRecepcionDet))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeProducto_pallet.Codigo_Barra))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeProducto_pallet.Codigo_Producto))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeProducto_pallet.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeProducto_pallet.Lote))
            cmd.Parameters.Add(New SqlParameter("@IMPRESO", oBeProducto_pallet.Impreso))
            cmd.Parameters.Add(New SqlParameter("@REIMPRESIONES", oBeProducto_pallet.Reimpresiones))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", IIf(oBeProducto_pallet.Fecha_vence = Nothing, DBNull.Value, oBeProducto_pallet.Fecha_vence)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_INGRESO", oBeProducto_pallet.Fecha_ingreso))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto_pallet.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProducto_pallet.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto_pallet.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto_pallet.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProducto_pallet.Activo))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

            oBeProducto_pallet.IdPallet = CInt(cmd.Parameters("@IDPALLET").Value)

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeProducto_pallet As clsBeProducto_pallet, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("producto_pallet")
            Upd.Add("idpallet", "@idpallet", DataType.Parametro)
            Upd.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Upd.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Upd.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Upd.Add("idoperadorbodega", "@idoperadorbodega", DataType.Parametro)
            Upd.Add("idimpresora", "@idimpresora", DataType.Parametro)
            Upd.Add("idrecepcionenc", "@idrecepcionenc", DataType.Parametro)
            Upd.Add("idrecepciondet", "@idrecepciondet", DataType.Parametro)
            Upd.Add("codigo_barra", "@codigo_barra", DataType.Parametro)
            Upd.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Add("lote", "@lote", DataType.Parametro)
            Upd.Add("impreso", "@impreso", DataType.Parametro)
            Upd.Add("reimpresiones", "@reimpresiones", DataType.Parametro)
            Upd.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Upd.Add("fecha_ingreso", "@fecha_ingreso", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("IdPallet = @IdPallet")

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

            cmd.Parameters.Add(New SqlParameter("@IDPALLET", oBeProducto_pallet.IdPallet))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeProducto_pallet.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeProducto_pallet.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeProducto_pallet.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA", IIf(oBeProducto_pallet.IdOperadorBodega = Nothing, DBNull.Value, oBeProducto_pallet.IdOperadorBodega))) '#CKFK 20180413 12:54 AM Agregué que si el operador de bodega es 0 inserte un valor nulo
            cmd.Parameters.Add(New SqlParameter("@IDIMPRESORA", oBeProducto_pallet.IdImpresora))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeProducto_pallet.IdRecepcionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONDET", oBeProducto_pallet.IdRecepcionDet))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeProducto_pallet.Codigo_Barra))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeProducto_pallet.Codigo_Producto))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeProducto_pallet.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeProducto_pallet.Lote))
            cmd.Parameters.Add(New SqlParameter("@IMPRESO", oBeProducto_pallet.Impreso))
            cmd.Parameters.Add(New SqlParameter("@REIMPRESIONES", oBeProducto_pallet.Reimpresiones))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeProducto_pallet.Fecha_vence))
            cmd.Parameters.Add(New SqlParameter("@FECHA_INGRESO", oBeProducto_pallet.Fecha_ingreso))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto_pallet.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProducto_pallet.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto_pallet.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto_pallet.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProducto_pallet.Activo))

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

    Public Shared Function Eliminar(ByRef oBeProducto_pallet As clsBeProducto_pallet, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Producto_pallet" &
             "  Where(IdPallet = @IdPallet)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPALLET", oBeProducto_pallet.IdPallet))

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

            Const sp As String = " Delete from Producto_pallet"
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

            Const sp As String = "SELECT * FROM Producto_pallet"
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

    Public Shared Function Obtener(ByRef oBeProducto_pallet As clsBeProducto_pallet) As Boolean

        Try

            Const sp As String = "SELECT * FROM Producto_pallet" &
            " Where(IdPallet = @IdPallet)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPALLET", oBeProducto_pallet.IdPallet))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeProducto_pallet, dt.Rows(0))
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

    Public Shared Function GetAll() As List(Of clsBeProducto_pallet)

        Try

            Dim lReturnList As New List(Of clsBeProducto_pallet)
            Const sp As String = "SELECT * FROM Producto_pallet"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeProducto_pallet As New clsBeProducto_pallet

            For Each dr As DataRow In dt.Rows
                vBeProducto_pallet = New clsBeProducto_pallet
                Cargar(vBeProducto_pallet, dr)
                lReturnList.Add(vBeProducto_pallet)
            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeProducto_pallet As clsBeProducto_pallet)

        Try

            Const sp As String = "SELECT * FROM Producto_pallet" &
            " Where(IdPallet = @IdPallet)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPALLET", pBeProducto_pallet.IDPALLET))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeProducto_pallet, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID() as Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdPallet),0) FROM Producto_pallet"

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

End Class
