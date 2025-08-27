Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnCuadrilla_tipo

    Public Shared Sub Cargar(ByRef oBeCuadrilla_tipo As clsBeCuadrilla_tipo, ByRef dr As DataRow)
        Try
            With oBeCuadrilla_tipo
                .IdTipoCuadrilla = IIf(IsDBNull(dr.Item("IdTipoCuadrilla")), 0, dr.Item("IdTipoCuadrilla"))
                .Nombre = IIf(IsDBNull(dr.Item("Nombre")), "", dr.Item("Nombre"))
                .Es_recepcion = IIf(IsDBNull(dr.Item("es_recepcion")), False, dr.Item("es_recepcion"))
                .Es_picking = IIf(IsDBNull(dr.Item("es_picking")), False, dr.Item("es_picking"))
                .Es_verificacion = IIf(IsDBNull(dr.Item("es_verificacion")), False, dr.Item("es_verificacion"))
                .Es_transito = IIf(IsDBNull(dr.Item("es_transito")), False, dr.Item("es_transito"))
                .Es_inventario = IIf(IsDBNull(dr.Item("es_inventario")), False, dr.Item("es_inventario"))
                .Es_ubicacion = IIf(IsDBNull(dr.Item("es_ubicacion")), False, dr.Item("es_ubicacion"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeCuadrilla_tipo As clsBeCuadrilla_tipo, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("cuadrilla_tipo")
            Ins.Add("idtipocuadrilla", "@idtipocuadrilla", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("es_recepcion", "@es_recepcion", DataType.Parametro)
            Ins.Add("es_picking", "@es_picking", DataType.Parametro)
            Ins.Add("es_verificacion", "@es_verificacion", DataType.Parametro)
            Ins.Add("es_transito", "@es_transito", DataType.Parametro)
            Ins.Add("es_inventario", "@es_inventario", DataType.Parametro)
            Ins.Add("es_ubicacion", "@es_ubicacion", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTIPOCUADRILLA", oBeCuadrilla_tipo.IdTipoCuadrilla))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeCuadrilla_tipo.Nombre))
            cmd.Parameters.Add(New SqlParameter("@ES_RECEPCION", oBeCuadrilla_tipo.Es_recepcion))
            cmd.Parameters.Add(New SqlParameter("@ES_PICKING", oBeCuadrilla_tipo.Es_picking))
            cmd.Parameters.Add(New SqlParameter("@ES_VERIFICACION", oBeCuadrilla_tipo.Es_verificacion))
            cmd.Parameters.Add(New SqlParameter("@ES_TRANSITO", oBeCuadrilla_tipo.Es_transito))
            cmd.Parameters.Add(New SqlParameter("@ES_INVENTARIO", oBeCuadrilla_tipo.Es_inventario))
            cmd.Parameters.Add(New SqlParameter("@ES_UBICACION", oBeCuadrilla_tipo.Es_ubicacion))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeCuadrilla_tipo.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeCuadrilla_tipo.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeCuadrilla_tipo.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeCuadrilla_tipo.User_mod))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeCuadrilla_tipo As clsBeCuadrilla_tipo, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("cuadrilla_tipo")
            Upd.Add("idtipocuadrilla", "@idtipocuadrilla", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("es_recepcion", "@es_recepcion", DataType.Parametro)
            Upd.Add("es_picking", "@es_picking", DataType.Parametro)
            Upd.Add("es_verificacion", "@es_verificacion", DataType.Parametro)
            Upd.Add("es_transito", "@es_transito", DataType.Parametro)
            Upd.Add("es_inventario", "@es_inventario", DataType.Parametro)
            Upd.Add("es_ubicacion", "@es_ubicacion", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Where("IdTipoCuadrilla = @IdTipoCuadrilla")

            Dim sp As String = Upd.SQL()

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTIPOCUADRILLA", oBeCuadrilla_tipo.IdTipoCuadrilla))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeCuadrilla_tipo.Nombre))
            cmd.Parameters.Add(New SqlParameter("@ES_RECEPCION", oBeCuadrilla_tipo.Es_recepcion))
            cmd.Parameters.Add(New SqlParameter("@ES_PICKING", oBeCuadrilla_tipo.Es_picking))
            cmd.Parameters.Add(New SqlParameter("@ES_VERIFICACION", oBeCuadrilla_tipo.Es_verificacion))
            cmd.Parameters.Add(New SqlParameter("@ES_TRANSITO", oBeCuadrilla_tipo.Es_transito))
            cmd.Parameters.Add(New SqlParameter("@ES_INVENTARIO", oBeCuadrilla_tipo.Es_inventario))
            cmd.Parameters.Add(New SqlParameter("@ES_UBICACION", oBeCuadrilla_tipo.Es_ubicacion))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeCuadrilla_tipo.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeCuadrilla_tipo.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeCuadrilla_tipo.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeCuadrilla_tipo.User_mod))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function


    Public Shared Function Eliminar(ByRef oBeCuadrilla_tipo As clsBeCuadrilla_tipo, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Cuadrilla_tipo" &
             "  Where(IdTipoCuadrilla = @IdTipoCuadrilla)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTIPOCUADRILLA", oBeCuadrilla_tipo.IdTipoCuadrilla))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function


    Public Shared Function GetAll() As List(Of clsBeCuadrilla_tipo)

        Dim lReturnList As New List(Of clsBeCuadrilla_tipo)

        Try

            Const sp As String = "SELECT * FROM Cuadrilla_tipo "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeCuadrilla_tipo As New clsBeCuadrilla_tipo

                        For Each dr As DataRow In lDataTable.Rows
                            vBeCuadrilla_tipo = New clsBeCuadrilla_tipo
                            Cargar(vBeCuadrilla_tipo, dr)
                            lReturnList.Add(vBeCuadrilla_tipo)
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub GetSingle(ByRef pBeCuadrilla_tipo As clsBeCuadrilla_tipo)

        Try

            Const sp As String = "SELECT * FROM Cuadrilla_tipo 
			  Where(IdTipoCuadrilla = @IdTipoCuadrilla) "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeCuadrilla_tipo As New clsBeCuadrilla_tipo

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(pBeCuadrilla_tipo, lDataTable.Rows(0))
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using


        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Public Shared Function MaxID() as Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdTipoCuadrilla),0) FROM Cuadrilla_tipo"

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
