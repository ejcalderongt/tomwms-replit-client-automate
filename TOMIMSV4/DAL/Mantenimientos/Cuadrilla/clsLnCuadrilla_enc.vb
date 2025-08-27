Imports System.Data.SqlClient

Public Class clsLnCuadrilla_enc

    Public Shared Sub Cargar(ByRef oBeCuadrilla_enc As clsBeCuadrilla_enc, ByRef dr As DataRow)
        Try
            With oBeCuadrilla_enc
                .IdCuadrillaEnc = IIf(IsDBNull(dr.Item("IdCuadrillaEnc")), 0, dr.Item("IdCuadrillaEnc"))
                .IdTipoCuadrilla = IIf(IsDBNull(dr.Item("IdTipoCuadrilla")), 0, dr.Item("IdTipoCuadrilla"))
                .IdEmpresa = IIf(IsDBNull(dr.Item("IdEmpresa")), 0, dr.Item("IdEmpresa"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .Nombre = IIf(IsDBNull(dr.Item("Nombre")), "", dr.Item("Nombre"))
                .Descripcion = IIf(IsDBNull(dr.Item("Descripcion")), "", dr.Item("Descripcion"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeCuadrilla_enc As clsBeCuadrilla_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("cuadrilla_enc")
            Ins.Add("idcuadrillaenc", "@idcuadrillaenc", DataType.Parametro)
            Ins.Add("idtipocuadrilla", "@idtipocuadrilla", DataType.Parametro)
            Ins.Add("idempresa", "@idempresa", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("descripcion", "@descripcion", DataType.Parametro)
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

            cmd.Parameters.Add(New SqlParameter("@IDCUADRILLAENC", oBeCuadrilla_enc.IdCuadrillaEnc))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOCUADRILLA", oBeCuadrilla_enc.IdTipoCuadrilla))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeCuadrilla_enc.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeCuadrilla_enc.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeCuadrilla_enc.Nombre))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeCuadrilla_enc.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeCuadrilla_enc.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeCuadrilla_enc.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeCuadrilla_enc.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeCuadrilla_enc.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeCuadrilla_enc.Activo))

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

    Public Shared Function Actualizar(ByRef oBeCuadrilla_enc As clsBeCuadrilla_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("cuadrilla_enc")
            Upd.Add("idtipocuadrilla", "@idtipocuadrilla", DataType.Parametro)
            Upd.Add("idempresa", "@idempresa", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("descripcion", "@descripcion", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("idcuadrillaenc = @idcuadrillaenc")

            Dim sp As String = Upd.SQL()

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCUADRILLAENC", oBeCuadrilla_enc.IdCuadrillaEnc))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOCUADRILLA", oBeCuadrilla_enc.IdTipoCuadrilla))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeCuadrilla_enc.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeCuadrilla_enc.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeCuadrilla_enc.Nombre))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeCuadrilla_enc.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeCuadrilla_enc.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeCuadrilla_enc.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeCuadrilla_enc.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeCuadrilla_enc.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeCuadrilla_enc.Activo))

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

    Public Shared Function Eliminar(ByRef oBeCuadrilla_enc As clsBeCuadrilla_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Cuadrilla_enc " &
             "  Where(IdCuadrillaEnc = @IdCuadrillaEnc)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCUADRILLAENC", oBeCuadrilla_enc.IdCuadrillaEnc))

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

    Public Shared Function GetAll() As List(Of clsBeCuadrilla_enc)

        Dim lReturnList As New List(Of clsBeCuadrilla_enc)

        Try

            Const sp As String = "SELECT * FROM Cuadrilla_enc "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeCuadrilla_enc As New clsBeCuadrilla_enc

                        For Each dr As DataRow In lDataTable.Rows
                            vBeCuadrilla_enc = New clsBeCuadrilla_enc
                            Cargar(vBeCuadrilla_enc, dr)
                            lReturnList.Add(vBeCuadrilla_enc)
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

    Public Shared Sub GetSingle(ByRef pBeCuadrilla_enc As clsBeCuadrilla_enc)

        Try

            Const sp As String = "SELECT * FROM Cuadrilla_enc 
			  Where(IdCuadrillaEnc = @IdCuadrillaEnc) "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdCuadrillaEnc", pBeCuadrilla_enc.IdCuadrillaEnc)
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeCuadrilla_enc As New clsBeCuadrilla_enc

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Cargar(pBeCuadrilla_enc, lDataTable.Rows(0))

                            pBeCuadrilla_enc.DetalleOperadores = clsLnCuadrilla_det_operador.Get_All_By_IdCuadrillaEnc(pBeCuadrilla_enc.IdCuadrillaEnc,
                                                                                                                       lConnection,
                                                                                                                       lTransaction)

                            pBeCuadrilla_enc.DetalleMontaCargas = clsLnCuadrilla_det_montacarga.Get_All_By_IdCuadrillaEnc(pBeCuadrilla_enc.IdCuadrillaEnc,
                                                                                                                          lConnection,
                                                                                                                          lTransaction)

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using


        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdCuadrillaEnc),0) FROM Cuadrilla_enc"

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
            Throw ex
        End Try

    End Function

End Class
