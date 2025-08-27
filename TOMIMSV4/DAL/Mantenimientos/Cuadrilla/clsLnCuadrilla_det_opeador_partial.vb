Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnCuadrilla_det_operador

    Public Shared Sub Cargar(ByRef oBeCuadrilla_det As clsBeCuadrilla_det_operador, ByRef dr As DataRow)
        Try
            With oBeCuadrilla_det
                .IdCuadrillaDet = IIf(IsDBNull(dr.Item("IdCuadrillaDet")), 0, dr.Item("IdCuadrillaDet"))
                .IdCuadrillaEnc = IIf(IsDBNull(dr.Item("IdCuadrillaEnc")), 0, dr.Item("IdCuadrillaEnc"))
                .IdOperadorBodega = IIf(IsDBNull(dr.Item("IdOperadorBodega")), 0, dr.Item("IdOperadorBodega"))
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

    Public Shared Function Insertar(ByRef oBeCuadrilla_det As clsBeCuadrilla_det_operador, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("cuadrilla_det_operador")
            Ins.Add("idcuadrilladet", "@idcuadrilladet", DataType.Parametro)
            Ins.Add("idcuadrillaenc", "@idcuadrillaenc", DataType.Parametro)
            Ins.Add("idoperadorbodega", "@idoperadorbodega", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCUADRILLADET", oBeCuadrilla_det.IdCuadrillaDet))
            cmd.Parameters.Add(New SqlParameter("@IDCUADRILLAENC", oBeCuadrilla_det.IdCuadrillaEnc))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA", oBeCuadrilla_det.IdOperadorBodega))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeCuadrilla_det.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeCuadrilla_det.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeCuadrilla_det.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeCuadrilla_det.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeCuadrilla_det.Activo))

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

    Public Shared Function Actualizar(ByRef oBeCuadrilla_det As clsBeCuadrilla_det_operador, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("cuadrilla_det_operador")
            Upd.Add("idcuadrilladet", "@idcuadrilladet", DataType.Parametro)
            Upd.Add("idcuadrillaenc", "@idcuadrillaenc", DataType.Parametro)
            Upd.Add("idoperadorbodega", "@idoperadorbodega", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("IdCuadrillaDet = @IdCuadrillaDet")

            Dim sp As String = Upd.SQL()

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCUADRILLADET", oBeCuadrilla_det.IdCuadrillaDet))
            cmd.Parameters.Add(New SqlParameter("@IDCUADRILLAENC", oBeCuadrilla_det.IdCuadrillaEnc))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA", oBeCuadrilla_det.IdOperadorBodega))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeCuadrilla_det.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeCuadrilla_det.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeCuadrilla_det.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeCuadrilla_det.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeCuadrilla_det.Activo))

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


    Public Shared Function Eliminar(ByRef oBeCuadrilla_det As clsBeCuadrilla_det_operador, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from cuadrilla_det_operador " &
             "  Where(IdCuadrillaDet = @IdCuadrillaDet)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCUADRILLADET", oBeCuadrilla_det.IdCuadrillaDet))

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


    Public Shared Function GetAll() As List(Of clsBeCuadrilla_det_operador)

        Dim lReturnList As New List(Of clsBeCuadrilla_det_operador)

        Try

            Const sp As String = "SELECT * FROM cuadrilla_det_operador "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeCuadrilla_det As New clsBeCuadrilla_det_operador

                        For Each dr As DataRow In lDataTable.Rows
                            vBeCuadrilla_det = New clsBeCuadrilla_det_operador
                            Cargar(vBeCuadrilla_det, dr)
                            lReturnList.Add(vBeCuadrilla_det)
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

    Public Shared Sub GetSingle(ByRef pBeCuadrilla_det As clsBeCuadrilla_det_operador)

        Try

            Const sp As String = "SELECT * FROM cuadrilla_det_operador 
			  Where(IdCuadrillaDet = @IdCuadrillaDet) "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeCuadrilla_det As New clsBeCuadrilla_det_operador

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(pBeCuadrilla_det, lDataTable.Rows(0))
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

            Const sp As String = "SELECT ISNULL(Max(IdCuadrillaDet),0) FROM Cuadrilla_det "

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
