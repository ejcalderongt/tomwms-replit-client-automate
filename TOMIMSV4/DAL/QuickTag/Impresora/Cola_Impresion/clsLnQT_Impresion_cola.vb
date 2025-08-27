Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnQT_Impresion_cola

    Public Shared Sub Cargar(ByRef oBeImpresion_cola As clsBeQT_Impresion_cola, ByRef dr As DataRow)
        Try
            With oBeImpresion_cola
                .IdColaImpresion = IIf(IsDBNull(dr.Item("IdColaImpresion")), 0, dr.Item("IdColaImpresion"))
                .Impresora = IIf(IsDBNull(dr.Item("impresora")), "", dr.Item("impresora"))
                .Tipoconexion = IIf(IsDBNull(dr.Item("tipoconexion")), "", dr.Item("tipoconexion"))
                .Detalleconexion = IIf(IsDBNull(dr.Item("detalleconexion")), "", dr.Item("detalleconexion"))
                .DataImpresion = IIf(IsDBNull(dr.Item("dataimpresion")), "", dr.Item("dataimpresion"))
                .Estado = IIf(IsDBNull(dr.Item("estado")), "", dr.Item("estado"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeImpresion_cola As clsBeQT_Impresion_cola, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("impresion_cola")
            Ins.Add("idcolaimpresion", "@idcolaimpresion", DataType.Parametro)
            Ins.Add("impresora", "@impresora", DataType.Parametro)
            Ins.Add("tipoconexion", "@tipoconexion", DataType.Parametro)
            Ins.Add("detalleconexion", "@detalleconexion", DataType.Parametro)
            Ins.Add("dataimpresion", "@dataimpresion", DataType.Parametro)
            Ins.Add("estado", "@estado", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("tipoimpresion", "@tipoimpresion", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCOLAIMPRESION", oBeImpresion_cola.IdColaImpresion))
            cmd.Parameters.Add(New SqlParameter("@IMPRESORA", oBeImpresion_cola.Impresora))
            cmd.Parameters.Add(New SqlParameter("@TIPOCONEXION", oBeImpresion_cola.Tipoconexion))
            cmd.Parameters.Add(New SqlParameter("@DETALLECONEXION", oBeImpresion_cola.Detalleconexion))
            cmd.Parameters.Add(New SqlParameter("@DATAIMPRESION", oBeImpresion_cola.DataImpresion))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeImpresion_cola.Estado))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeImpresion_cola.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeImpresion_cola.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeImpresion_cola.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeImpresion_cola.User_mod))
            cmd.Parameters.Add(New SqlParameter("@TIPOIMPRESION", oBeImpresion_cola.TipoImpresion))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeImpresion_cola As clsBeQT_Impresion_cola, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("impresion_cola")
            Upd.Add("idcolaimpresion", "@idcolaimpresion", DataType.Parametro)
            Upd.Add("impresora", "@impresora", DataType.Parametro)
            Upd.Add("tipoconexion", "@tipoconexion", DataType.Parametro)
            Upd.Add("detalleconexion", "@detalleconexion", DataType.Parametro)
            Upd.Add("dataimpresion", "@dataimpresion", DataType.Parametro)
            Upd.Add("estado", "@estado", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("tipoimpresion", "@user_mod", DataType.Parametro)
            Upd.Where("IdColaImpresion = @IdColaImpresion")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCOLAIMPRESION", oBeImpresion_cola.IdColaImpresion))
            cmd.Parameters.Add(New SqlParameter("@IMPRESORA", oBeImpresion_cola.Impresora))
            cmd.Parameters.Add(New SqlParameter("@TIPOCONEXION", oBeImpresion_cola.Tipoconexion))
            cmd.Parameters.Add(New SqlParameter("@DETALLECONEXION", oBeImpresion_cola.Detalleconexion))
            cmd.Parameters.Add(New SqlParameter("@DATAIMPRESION", oBeImpresion_cola.DataImpresion))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeImpresion_cola.Estado))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeImpresion_cola.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeImpresion_cola.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeImpresion_cola.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeImpresion_cola.User_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeImpresion_cola.TipoImpresion))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection is Nothing Then lConnection.Dispose()
            If Not lTransaction is Nothing Then lTransaction.Dispose()
        End Try

    End Function


    Public Shared Function Eliminar(ByRef oBeImpresion_cola As clsBeQT_Impresion_cola, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Impresion_cola" & _
             "  Where(IdColaImpresion = @IdColaImpresion)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCOLAIMPRESION", oBeImpresion_cola.IdColaImpresion))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection is Nothing Then lConnection.Dispose()
            If Not lTransaction is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM Impresion_cola"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection is Nothing Then lConnection.Dispose()
            If Not lTransaction is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function CargarColaImpresion() As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT IdColaImpresion IdCola,impresora,tipoimpresion tipo,tipoconexion conexion,estado,fec_mod FROM Impresion_cola where estado in ('Pendiente','Error') "
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function


    Public Shared Function CargarColaCerradaImpresion() As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT IdColaImpresion IdCola,impresora,tipoimpresion tipo,tipoconexion conexion,estado,fec_mod FROM Impresion_cola where estado in('Completado','Cancelado') "
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function
    Public Shared Function Get_All() As List(Of clsBeQT_Impresion_cola)

        Dim lReturnList As New List(Of clsBeQT_Impresion_cola)

        Try

            Const sp As String = "SELECT * FROM Impresion_cola"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeImpresion_cola As New clsBeQT_Impresion_cola

                        For Each dr As DataRow In lDataTable.Rows
                            vBeImpresion_cola = New clsBeQT_Impresion_cola()
                            Cargar(vBeImpresion_cola, dr)
                            lReturnList.Add(vBeImpresion_cola)
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Sub GetSingle(ByRef pBeImpresion_cola As clsBeQT_Impresion_cola)

        Try

            Const sp As String = "SELECT * FROM Impresion_cola Where(IdColaImpresion = @IdColaImpresion)"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdColaImpresion", pBeImpresion_cola.IdColaImpresion))
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeImpresion_cola As New clsBeQT_Impresion_cola

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeImpresion_cola, lDataTable.Rows(0))
                            pBeImpresion_cola = vBeImpresion_cola
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

            Const sp As String = "SELECT ISNULL(Max(IdColaImpresion),0) FROM Impresion_cola"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()
                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lMax = CInt(lReturnValue)
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lMax

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function GetSingle() As clsBeQT_Impresion_cola

        GetSingle = Nothing

        Try

            Const sp As String = "SELECT TOP 1 * FROM Impresion_cola WHERE estado = 'Pendiente' ORDER BY fec_agr"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeImpresion_cola As New clsBeQT_Impresion_cola

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeImpresion_cola, lDataTable.Rows(0))
                            GetSingle = vBeImpresion_cola
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function ActualizarEstadoCola(ByRef oBeImpresion_cola As clsBeQT_Impresion_cola, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("impresion_cola")
            Upd.Add("estado", "@estado", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Where("IdColaImpresion = @IdColaImpresion")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCOLAIMPRESION", oBeImpresion_cola.IdColaImpresion))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeImpresion_cola.Estado))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeImpresion_cola.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeImpresion_cola.User_mod))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

End Class
