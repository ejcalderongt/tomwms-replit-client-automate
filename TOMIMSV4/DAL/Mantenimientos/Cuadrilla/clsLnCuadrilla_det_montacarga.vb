Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnCuadrilla_det_montacarga

    Public Shared Sub Cargar(ByRef oBeCuadrilla_det_montacarga As clsBeCuadrilla_det_montacarga, ByRef dr As DataRow)
        Try
            With oBeCuadrilla_det_montacarga
                .IdCuadrillaDetMontaCarga = IIf(IsDBNull(dr.Item("IdCuadrillaDetMontaCarga")), 0, dr.Item("IdCuadrillaDetMontaCarga"))
                .IdCuadrillaEnc = IIf(IsDBNull(dr.Item("IdCuadrillaEnc")), 0, dr.Item("IdCuadrillaEnc"))
                .IdMontacargaBodega = IIf(IsDBNull(dr.Item("IdMontacargaBodega")), 0, dr.Item("IdMontacargaBodega"))
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

    Public Shared Function Insertar(ByRef oBeCuadrilla_det_montacarga As clsBeCuadrilla_det_montacarga, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("cuadrilla_det_montacarga")
            Ins.Add("idcuadrilladetmontacarga", "@idcuadrilladetmontacarga", DataType.Parametro)
            Ins.Add("idcuadrillaenc", "@idcuadrillaenc", DataType.Parametro)
            Ins.Add("idmontacargabodega", "@idmontacargabodega", DataType.Parametro)
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

            cmd.Parameters.Add(New SqlParameter("@IDCUADRILLADETMONTACARGA", oBeCuadrilla_det_montacarga.IdCuadrillaDetMontaCarga))
            cmd.Parameters.Add(New SqlParameter("@IDCUADRILLAENC", oBeCuadrilla_det_montacarga.IdCuadrillaEnc))
            cmd.Parameters.Add(New SqlParameter("@IDMONTACARGABODEGA", oBeCuadrilla_det_montacarga.IdMontacargaBodega))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeCuadrilla_det_montacarga.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeCuadrilla_det_montacarga.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeCuadrilla_det_montacarga.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeCuadrilla_det_montacarga.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeCuadrilla_det_montacarga.Activo))

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

    Public Shared Function Actualizar(ByRef oBeCuadrilla_det_montacarga As clsBeCuadrilla_det_montacarga, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("cuadrilla_det_montacarga")
            Upd.Add("idcuadrilladetmontacarga", "@idcuadrilladetmontacarga", DataType.Parametro)
            Upd.Add("idcuadrillaenc", "@idcuadrillaenc", DataType.Parametro)
            Upd.Add("idmontacargabodega", "@idmontacargabodega", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("IdCuadrillaDetMontaCarga = @IdCuadrillaDetMontaCarga")

            Dim sp As String = Upd.SQL()

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCUADRILLADETMONTACARGA", oBeCuadrilla_det_montacarga.IdCuadrillaDetMontaCarga))
            cmd.Parameters.Add(New SqlParameter("@IDCUADRILLAENC", oBeCuadrilla_det_montacarga.IdCuadrillaEnc))
            cmd.Parameters.Add(New SqlParameter("@IDMONTACARGABODEGA", oBeCuadrilla_det_montacarga.IdMontacargaBodega))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeCuadrilla_det_montacarga.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeCuadrilla_det_montacarga.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeCuadrilla_det_montacarga.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeCuadrilla_det_montacarga.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeCuadrilla_det_montacarga.Activo))

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


    Public Shared Function Eliminar(ByRef oBeCuadrilla_det_montacarga As clsBeCuadrilla_det_montacarga, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Cuadrilla_det_montacarga" &
             "  Where(IdCuadrillaDetMontaCarga = @IdCuadrillaDetMontaCarga)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCUADRILLADETMONTACARGA", oBeCuadrilla_det_montacarga.IdCuadrillaDetMontaCarga))

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


    Public Shared Function GetAll() As List(Of clsBeCuadrilla_det_montacarga)

        Dim lReturnList As New List(Of clsBeCuadrilla_det_montacarga)

        Try

            Const sp As String = "SELECT * FROM Cuadrilla_det_montacarga"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeCuadrilla_det_montacarga As New clsBeCuadrilla_det_montacarga

                        For Each dr As DataRow In lDataTable.Rows
                            vBeCuadrilla_det_montacarga = New clsBeCuadrilla_det_montacarga
                            Cargar(vBeCuadrilla_det_montacarga, dr)
                            lReturnList.Add(vBeCuadrilla_det_montacarga)
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

    Public Shared Sub GetSingle(ByRef pBeCuadrilla_det_montacarga As clsBeCuadrilla_det_montacarga)

        Try

            Const sp As String = "SELECT * FROM Cuadrilla_det_montacarga 
			  Where(IdCuadrillaDetMontaCarga = @IdCuadrillaDetMontaCarga) "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeCuadrilla_det_montacarga As New clsBeCuadrilla_det_montacarga

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(pBeCuadrilla_det_montacarga, lDataTable.Rows(0))
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

            Const sp As String = "SELECT ISNULL(Max(IdCuadrillaDetMontaCarga),0) FROM Cuadrilla_det_montacarga"

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
