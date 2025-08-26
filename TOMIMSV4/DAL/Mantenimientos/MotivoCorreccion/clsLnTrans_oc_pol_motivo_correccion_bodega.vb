Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_oc_pol_motivo_correccion_bodega

    Public Shared Sub Cargar(ByRef oBeTrans_oc_pol_motivo_correccion_bodega As clsBeTrans_oc_pol_motivo_correccion_bodega, ByRef dr As DataRow)
        Try
            With oBeTrans_oc_pol_motivo_correccion_bodega
                .IdMotivoCorreccionBodega = IIf(IsDBNull(dr.Item("IdMotivoCorreccionBodega")), 0, dr.Item("IdMotivoCorreccionBodega"))
                .IdMotivoCorreccion = IIf(IsDBNull(dr.Item("IdMotivoCorreccion")), 0, dr.Item("IdMotivoCorreccion"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_oc_pol_motivo_correccion_bodega As clsBeTrans_oc_pol_motivo_correccion_bodega, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_oc_pol_motivo_correccion_bodega")
            Ins.Add("idmotivocorreccionbodega", "@idmotivocorreccionbodega", DataType.Parametro)
            Ins.Add("idmotivocorreccion", "@idmotivocorreccion", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMOTIVOCORRECCIONBODEGA", oBeTrans_oc_pol_motivo_correccion_bodega.IdMotivoCorreccionBodega))
            cmd.Parameters.Add(New SqlParameter("@IDMOTIVOCORRECCION", oBeTrans_oc_pol_motivo_correccion_bodega.IdMotivoCorreccion))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_oc_pol_motivo_correccion_bodega.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_oc_pol_motivo_correccion_bodega.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_oc_pol_motivo_correccion_bodega.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_oc_pol_motivo_correccion_bodega.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_oc_pol_motivo_correccion_bodega.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_oc_pol_motivo_correccion_bodega.Fec_mod))

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

    Public Shared Function Actualizar(ByRef oBeTrans_oc_pol_motivo_correccion_bodega As clsBeTrans_oc_pol_motivo_correccion_bodega, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_oc_pol_motivo_correccion_bodega")
            Upd.Add("idmotivocorreccionbodega", "@idmotivocorreccionbodega", DataType.Parametro)
            Upd.Add("idmotivocorreccion", "@idmotivocorreccion", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("IdMotivoCorreccionBodega = @IdMotivoCorreccionBodega")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMOTIVOCORRECCIONBODEGA", oBeTrans_oc_pol_motivo_correccion_bodega.IdMotivoCorreccionBodega))
            cmd.Parameters.Add(New SqlParameter("@IDMOTIVOCORRECCION", oBeTrans_oc_pol_motivo_correccion_bodega.IdMotivoCorreccion))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_oc_pol_motivo_correccion_bodega.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_oc_pol_motivo_correccion_bodega.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_oc_pol_motivo_correccion_bodega.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_oc_pol_motivo_correccion_bodega.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_oc_pol_motivo_correccion_bodega.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_oc_pol_motivo_correccion_bodega.Fec_mod))

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


    Public Shared Function Eliminar(ByRef oBeTrans_oc_pol_motivo_correccion_bodega As clsBeTrans_oc_pol_motivo_correccion_bodega, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_oc_pol_motivo_correccion_bodega" & _
             "  Where(IdMotivoCorreccionBodega = @IdMotivoCorreccionBodega)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMOTIVOCORRECCIONBODEGA", oBeTrans_oc_pol_motivo_correccion_bodega.IdMotivoCorreccionBodega))

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

            Const sp As String = "SELECT * FROM Trans_oc_pol_motivo_correccion_bodega"
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

    Public Shared Function Get_All() As List(Of clsBeTrans_oc_pol_motivo_correccion_bodega)

        Dim lReturnList As New List(Of clsBeTrans_oc_pol_motivo_correccion_bodega)

        Try

            Const sp As String = "SELECT * FROM Trans_oc_pol_motivo_correccion_bodega"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_oc_pol_motivo_correccion_bodega As New clsBeTrans_oc_pol_motivo_correccion_bodega

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_oc_pol_motivo_correccion_bodega = New clsBeTrans_oc_pol_motivo_correccion_bodega()
                            Cargar(vBeTrans_oc_pol_motivo_correccion_bodega, dr)
                            lReturnList.Add(vBeTrans_oc_pol_motivo_correccion_bodega)
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

    Public Shared Sub GetSingle(ByRef pBeTrans_oc_pol_motivo_correccion_bodega As clsBeTrans_oc_pol_motivo_correccion_bodega)

        Try

            Const sp As String = "SELECT * FROM Trans_oc_pol_motivo_correccion_bodega" & _
            " Where(IdMotivoCorreccionBodega = @IdMotivoCorreccionBodega)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_oc_pol_motivo_correccion_bodega As New clsBeTrans_oc_pol_motivo_correccion_bodega

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTrans_oc_pol_motivo_correccion_bodega, lDataTable.Rows(0))
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

            Const sp As String = "SELECT ISNULL(Max(IdMotivoCorreccionBodega),0) FROM Trans_oc_pol_motivo_correccion_bodega"

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

    Public Shared Function Get_All_By_MotivoCorreccion(ByVal pIdMotivoCorreccion As Integer) As List(Of clsBeTrans_oc_pol_motivo_correccion_bodega)

        Dim lReturnList As New List(Of clsBeTrans_oc_pol_motivo_correccion_bodega)

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM trans_oc_pol_motivo_correccion_bodega WHERE IdMotivoCorreccion = @IdMotivoCorreccion"


                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdMotivoCorreccion", pIdMotivoCorreccion)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim BeTrans_oc_pol_motivo_correccion_bodega As clsBeTrans_oc_pol_motivo_correccion_bodega

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            BeTrans_oc_pol_motivo_correccion_bodega = New clsBeTrans_oc_pol_motivo_correccion_bodega
                            Cargar(BeTrans_oc_pol_motivo_correccion_bodega, lRow)
                            lReturnList.Add(BeTrans_oc_pol_motivo_correccion_bodega)

                        Next

                    End If

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdMotivoCorreccionBodega),0) FROM Trans_oc_pol_motivo_correccion_bodega"

            Using lCommand As New SqlCommand(sp, pConnection, pTransaction)

                lCommand.CommandType = CommandType.Text

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
