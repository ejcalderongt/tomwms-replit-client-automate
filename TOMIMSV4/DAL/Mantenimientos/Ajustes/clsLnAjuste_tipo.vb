Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnAjuste_tipo

    Public Shared Sub Cargar(ByRef oBeAjuste_tipo As clsBeAjuste_tipo, ByRef dr As DataRow)
        Try
            With oBeAjuste_tipo
                .Idtipoajuste = IIf(IsDBNull(dr.Item("idtipoajuste")), 0, dr.Item("idtipoajuste"))
                .Nombre = IIf(IsDBNull(dr.Item("nombre")), "", dr.Item("nombre"))
                .Modifica_lote = IIf(IsDBNull(dr.Item("modifica_lote")), False, dr.Item("modifica_lote"))
                .Momdifica_vencimiento = IIf(IsDBNull(dr.Item("momdifica_vencimiento")), False, dr.Item("momdifica_vencimiento"))
                .Modifica_cantidad = IIf(IsDBNull(dr.Item("modifica_cantidad")), False, dr.Item("modifica_cantidad"))
                .Modifica_peso = IIf(IsDBNull(dr.Item("modifica_peso")), False, dr.Item("modifica_peso"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
            End With
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub
    Public Shared Function Insertar(ByRef oBeAjuste_tipo As clsBeAjuste_tipo, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("ajuste_tipo")
            Ins.Add("idtipoajuste", "@idtipoajuste", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("modifica_lote", "@modifica_lote", DataType.Parametro)
            Ins.Add("momdifica_vencimiento", "@momdifica_vencimiento", DataType.Parametro)
            Ins.Add("modifica_cantidad", "@modifica_cantidad", DataType.Parametro)
            Ins.Add("modifica_peso", "@modifica_peso", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
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

            cmd.Parameters.Add(New SqlParameter("@IDTIPOAJUSTE", oBeAjuste_tipo.Idtipoajuste))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeAjuste_tipo.Nombre))
            cmd.Parameters.Add(New SqlParameter("@MODIFICA_LOTE", oBeAjuste_tipo.Modifica_lote))
            cmd.Parameters.Add(New SqlParameter("@MOMDIFICA_VENCIMIENTO", oBeAjuste_tipo.Momdifica_vencimiento))
            cmd.Parameters.Add(New SqlParameter("@MODIFICA_CANTIDAD", oBeAjuste_tipo.Modifica_cantidad))
            cmd.Parameters.Add(New SqlParameter("@MODIFICA_PESO", oBeAjuste_tipo.Modifica_peso))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeAjuste_tipo.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeAjuste_tipo.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeAjuste_tipo.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeAjuste_tipo.User_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeAjuste_tipo.Activo))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

            oBeAjuste_tipo.Idtipoajuste = CInt(cmd.Parameters("@IDTIPOAJUSTE").Value)

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function
    Public Shared Function Actualizar(ByRef oBeAjuste_tipo As clsBeAjuste_tipo, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("ajuste_tipo")
            Upd.Add("idtipoajuste", "@idtipoajuste", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("modifica_lote", "@modifica_lote", DataType.Parametro)
            Upd.Add("momdifica_vencimiento", "@momdifica_vencimiento", DataType.Parametro)
            Upd.Add("modifica_cantidad", "@modifica_cantidad", DataType.Parametro)
            Upd.Add("modifica_peso", "@modifica_peso", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("idtipoajuste = @idtipoajuste")

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

            cmd.Parameters.Add(New SqlParameter("@IDTIPOAJUSTE", oBeAjuste_tipo.Idtipoajuste))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeAjuste_tipo.Nombre))
            cmd.Parameters.Add(New SqlParameter("@MODIFICA_LOTE", oBeAjuste_tipo.Modifica_lote))
            cmd.Parameters.Add(New SqlParameter("@MOMDIFICA_VENCIMIENTO", oBeAjuste_tipo.Momdifica_vencimiento))
            cmd.Parameters.Add(New SqlParameter("@MODIFICA_CANTIDAD", oBeAjuste_tipo.Modifica_cantidad))
            cmd.Parameters.Add(New SqlParameter("@MODIFICA_PESO", oBeAjuste_tipo.Modifica_peso))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeAjuste_tipo.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeAjuste_tipo.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeAjuste_tipo.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeAjuste_tipo.User_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeAjuste_tipo.Activo))


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
    Public Shared Function Listar(ByVal Activo As Boolean) As DataTable

        Try

            Const sp As String = "SELECT * FROM Ajuste_tipo WHERE activo = @activo "
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.SelectCommand.Parameters.Add(New SqlParameter("@activo", Activo))

            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function
    Public Shared Function GetAll() As List(Of clsBeAjuste_tipo)

        Try

            Dim lReturnList As New List(Of clsBeAjuste_tipo)
            Const sp As String = "SELECT * FROM Ajuste_tipo where activo=1"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeAjuste_tipo As New clsBeAjuste_tipo

            For Each dr As DataRow In dt.Rows
                vBeAjuste_tipo = New clsBeAjuste_tipo
                Cargar(vBeAjuste_tipo, dr)
                lReturnList.Add(vBeAjuste_tipo)
            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function
    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(idtipoajuste),0) FROM Ajuste_tipo"

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

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function
    Public Shared Function Get_All_ForCombo() As DataTable

        Get_All_ForCombo = Nothing

        Try

            Dim vSQL As String = "SELECT idtipoajuste as IdTipoAjuste, nombre as Nombre from ajuste_tipo where activo=0"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(vSQL, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            cmd.Dispose()
            dad.Dispose()

            Return dt

        Catch ex As Exception
            Throw New Exception("Get_All_ForCombo: " & ex.Message)
        End Try

    End Function

    'GT22042022: combo para Ajuste Stock, que se llena en el enc y no en el grid por cada linea de stock
    Public Shared Function Get_All_ForCombo_Activo() As DataTable

        Get_All_ForCombo_Activo = Nothing

        Try

            'Dim vSQL As String = "SELECT idtipoajuste as IdTipoAjuste, nombre as Nombre,modifica_cantidad from ajuste_tipo where activo=1"
            Dim vSQL As String = "  SELECT
                                    3 as IdTipoAjuste,
                                    'Ajuste x Cantidad (+/-)' as Nombre
                                    union all
                                    select
                                    idtipoajuste as IdTipoAjuste, 
                                    nombre as Nombre 
                                    from ajuste_tipo where activo=1 and modifica_cantidad = 0 
                                    order by IdTipoAjuste asc "

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(vSQL, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            cmd.Dispose()
            dad.Dispose()

            Return dt

        Catch ex As Exception
            Throw New Exception("Get_All_ForCombo: " & ex.Message)
        End Try

    End Function

    'GT22042022_1458: carga tipo por cantidad positiva/negativa en el combo
    Public Shared Function Get_by_Cantidad() As List(Of clsBeAjuste_tipo)

        Try

            Dim lReturnList As New List(Of clsBeAjuste_tipo)
            Const sp As String = "SELECT * FROM Ajuste_tipo where activo=1 and modifica_cantidad=1 "
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeAjuste_tipo As New clsBeAjuste_tipo

            For Each dr As DataRow In dt.Rows
                vBeAjuste_tipo = New clsBeAjuste_tipo
                Cargar(vBeAjuste_tipo, dr)
                lReturnList.Add(vBeAjuste_tipo)
            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function
    Public Shared Function Get_by_Cantidad(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As List(Of clsBeAjuste_tipo)

        Try

            Dim lReturnList As New List(Of clsBeAjuste_tipo)
            Const sp As String = "SELECT * FROM Ajuste_tipo where activo=1 and modifica_cantidad=1 "
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeAjuste_tipo As New clsBeAjuste_tipo

            For Each dr As DataRow In dt.Rows
                vBeAjuste_tipo = New clsBeAjuste_tipo
                Cargar(vBeAjuste_tipo, dr)
                lReturnList.Add(vBeAjuste_tipo)
            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
