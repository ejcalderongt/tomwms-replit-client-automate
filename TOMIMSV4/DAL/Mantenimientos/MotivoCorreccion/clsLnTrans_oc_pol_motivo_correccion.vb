Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_oc_pol_motivo_correccion

    Public Shared Sub Cargar(ByRef oBeTrans_oc_pol_motivo_correccion As clsBeTrans_oc_pol_motivo_correccion, ByRef dr As DataRow)
        Try
            With oBeTrans_oc_pol_motivo_correccion
                .IdMotivoCorreccion = IIf(IsDBNull(dr.Item("IdMotivoCorreccion")), 0, dr.Item("IdMotivoCorreccion"))
                .IdEmpresa = IIf(IsDBNull(dr.Item("IdEmpresa")), 0, dr.Item("IdEmpresa"))
                .Nombre = IIf(IsDBNull(dr.Item("Nombre")), "", dr.Item("Nombre"))
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

    Public Shared Function Insertar(ByRef oBeTrans_oc_pol_motivo_correccion As clsBeTrans_oc_pol_motivo_correccion, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_oc_pol_motivo_correccion")
            Ins.Add("idmotivocorreccion", "@idmotivocorreccion", DataType.Parametro)
            Ins.Add("idempresa", "@idempresa", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
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

            cmd.Parameters.Add(New SqlParameter("@IDMOTIVOCORRECCION", oBeTrans_oc_pol_motivo_correccion.IdMotivoCorreccion))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeTrans_oc_pol_motivo_correccion.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeTrans_oc_pol_motivo_correccion.Nombre))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_oc_pol_motivo_correccion.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_oc_pol_motivo_correccion.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_oc_pol_motivo_correccion.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_oc_pol_motivo_correccion.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_oc_pol_motivo_correccion.Fec_mod))

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

    Public Shared Function Actualizar(ByRef oBeTrans_oc_pol_motivo_correccion As clsBeTrans_oc_pol_motivo_correccion, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_oc_pol_motivo_correccion")
            Upd.Add("idmotivocorreccion", "@idmotivocorreccion", DataType.Parametro)
            Upd.Add("idempresa", "@idempresa", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("IdMotivoCorreccion = @IdMotivoCorreccion")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMOTIVOCORRECCION", oBeTrans_oc_pol_motivo_correccion.IdMotivoCorreccion))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeTrans_oc_pol_motivo_correccion.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeTrans_oc_pol_motivo_correccion.Nombre))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_oc_pol_motivo_correccion.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_oc_pol_motivo_correccion.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_oc_pol_motivo_correccion.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_oc_pol_motivo_correccion.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_oc_pol_motivo_correccion.Fec_mod))

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


    Public Shared Function Eliminar(ByRef oBeTrans_oc_pol_motivo_correccion As clsBeTrans_oc_pol_motivo_correccion, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_oc_pol_motivo_correccion" & _
             "  Where(IdMotivoCorreccion = @IdMotivoCorreccion)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMOTIVOCORRECCION", oBeTrans_oc_pol_motivo_correccion.IdMotivoCorreccion))

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

            Const sp As String = "SELECT * FROM Trans_oc_pol_motivo_correccion"
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

    Public Shared Function Get_All() As List(Of clsBeTrans_oc_pol_motivo_correccion)

        Dim lReturnList As New List(Of clsBeTrans_oc_pol_motivo_correccion)

        Try

            Const sp As String = "SELECT * FROM Trans_oc_pol_motivo_correccion"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_oc_pol_motivo_correccion As New clsBeTrans_oc_pol_motivo_correccion

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_oc_pol_motivo_correccion = New clsBeTrans_oc_pol_motivo_correccion()
                            Cargar(vBeTrans_oc_pol_motivo_correccion, dr)
                            lReturnList.Add(vBeTrans_oc_pol_motivo_correccion)
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

    Public Shared Sub GetSingle(ByRef pBeTrans_oc_pol_motivo_correccion As clsBeTrans_oc_pol_motivo_correccion)

        Try

            Const sp As String = "SELECT * FROM Trans_oc_pol_motivo_correccion" & _
            " Where(IdMotivoCorreccion = @IdMotivoCorreccion)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_oc_pol_motivo_correccion As New clsBeTrans_oc_pol_motivo_correccion

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTrans_oc_pol_motivo_correccion, lDataTable.Rows(0))
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

    Public Shared Function GetSingle(ByVal pIdMotivoCorreccion As Integer) As clsBeTrans_oc_pol_motivo_correccion

        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM Trans_oc_pol_motivo_correccion WHERE IdMotivoCorreccion=@IdMotivoCorreccion"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdMotivoCorreccion", pIdMotivoCorreccion)

                    Dim lDT As New DataTable()
                    lDTA.Fill(lDT)

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        Dim lRow As DataRow = lDT.Rows(0)
                        Dim Obj As New clsBeTrans_oc_pol_motivo_correccion()

                        Cargar(Obj, lRow)
                        Return Obj

                    End If

                End Using

            End Using

            Return Nothing

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID() as Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdMotivoCorreccion),0) FROM Trans_oc_pol_motivo_correccion"

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

    Public Shared Function Get_All_By_IdBodega(ByVal IdBodega As Integer) As List(Of clsBeTrans_oc_pol_motivo_correccion)

        Dim lMa As New List(Of clsBeTrans_oc_pol_motivo_correccion)
        Dim Ma As New clsBeTrans_oc_pol_motivo_correccion

        Try

            Dim sp As String = " SELECT  ma.IdMotivoCorreccion,ma.IdEmpresa, ma.Nombre, ma.activo,
                                         ma.user_agr, ma.fec_agr, ma.user_mod, ma.fec_mod
                                 FROM trans_oc_pol_motivo_correccion ma INNER JOIN 
                                      trans_oc_pol_motivo_correccion_bodega mab ON ma.IdMotivoCorreccion =mab.IdMotivoCorreccion 
                                 WHERE (mab.IdBodega = @IdBodega) "

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.CommandType = CommandType.Text
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

            Dim dt As New DataTable
            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            cmd.Dispose()
            dad.Dispose()

            For Each vma As DataRow In dt.Rows
                Ma = New clsBeTrans_oc_pol_motivo_correccion
                Ma.Empresa = New clsBeEmpresa
                Cargar(Ma, vma)

                If vma("IdEmpresa") IsNot DBNull.Value AndAlso vma("IdEmpresa") IsNot Nothing Then
                    Ma.Empresa.Nombre = CType(vma("IdEmpresa"), String)
                End If

                lMa.Add(Ma)


            Next

            Return lMa

        Catch ex As Exception
            Throw New Exception("GetAllByBodega_Motivo_Anulacion_Partial: " & ex.Message)
        End Try

    End Function

    Public Shared Function Get_All_Filtro(ByVal pActivo As Boolean) As List(Of clsBeTrans_oc_pol_motivo_correccion)

        Dim lReturnList As New List(Of clsBeTrans_oc_pol_motivo_correccion)

        Try


            Dim vSQL As String = "SELECT ma.*, empresa.nombre AS Empresa
									FROM trans_oc_pol_motivo_correccion AS ma INNER JOIN
									empresa ON ma.IdEmpresa = empresa.IdEmpresa LEFT OUTER JOIN
									trans_oc_pol_motivo_correccion_bodega AS mab ON ma.IdMotivoCorreccion = mab.IdMotivoCorreccion
									WHERE  (1 > 0) "



            If pActivo = True Then
                vSQL += " AND ma.Activo=1"
            Else
                vSQL += " AND ma.Activo=0"
            End If

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim BeTrans_oc_pol_motivo_correccion As clsBeTrans_oc_pol_motivo_correccion

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                BeTrans_oc_pol_motivo_correccion = New clsBeTrans_oc_pol_motivo_correccion
                                BeTrans_oc_pol_motivo_correccion.Empresa = New clsBeEmpresa
                                Cargar(BeTrans_oc_pol_motivo_correccion, lRow)

                                If lRow("IdEmpresa") IsNot DBNull.Value AndAlso lRow("IdEmpresa") IsNot Nothing Then
                                    BeTrans_oc_pol_motivo_correccion.Empresa.Nombre = CType(lRow("Empresa"), String)
                                End If

                                lReturnList.Add(BeTrans_oc_pol_motivo_correccion)

                            Next

                        End If

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


    Public Shared Function ActualizarDatos(ByVal pObjMC As clsBeTrans_oc_pol_motivo_correccion, ByVal pListObjMAB As List(Of clsBeTrans_oc_pol_motivo_correccion_bodega)) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Actualizar(pObjMC, lConnection, lTransaction)

            '#GT04092024: Se obtenida de la tabla correcion y debe ser correccion_bodega
            Dim lMax As Integer = clsLnTrans_oc_pol_motivo_correccion_bodega.MaxID(lConnection, lTransaction)

            For Each Obj As clsBeTrans_oc_pol_motivo_correccion_bodega In pListObjMAB

                If Obj.IdMotivoCorreccionBodega = 0 Then
                    lMax += 1
                    Obj.IdMotivoCorreccionBodega = lMax
                    clsLnTrans_oc_pol_motivo_correccion_bodega.Insertar(Obj, lConnection, lTransaction)
                Else
                    If Not Obj.Activo Then
                        Try
                            clsLnTrans_oc_pol_motivo_correccion_bodega.Eliminar(Obj, lConnection, lTransaction)
                        Catch ex As Exception
                            clsLnTrans_oc_pol_motivo_correccion_bodega.Actualizar(Obj, lConnection, lTransaction)
                        End Try
                    End If
                End If

            Next

            lTransaction.Commit()
            lConnection.Close()

            Return True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            Throw ex
        End Try

    End Function

    Public Shared Sub GetIdMotivoCorreccionBodega(ByRef beMotivoCorreccion As clsBeTrans_oc_pol_motivo_correccion_bodega)

        Try

            Const sp As String = "SELECT * FROM trans_oc_pol_motivo_correccion_bodega " &
                                    " WHERE IdBodega = @IdBodega " &
                                    " AND IdMotivoCorreccion = @IdMotivoCorreccion"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Using lCommand As New SqlCommand(sp, lConnection)

                lCommand.CommandType = CommandType.Text

                lCommand.Parameters.AddWithValue("@IdBodega", beMotivoCorreccion.IdBodega)
                lCommand.Parameters.AddWithValue("@IdMotivoCorreccion", beMotivoCorreccion.IdMotivoCorreccion)
                lConnection.Open()

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                lConnection.Close()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    beMotivoCorreccion.IdMotivoCorreccionBodega = CInt(lReturnValue)
                End If

            End Using

        Catch ex As Exception
            Throw New Exception("GetIdMotivoCorreccionBodega: " & ex.Message)
        End Try

    End Sub


    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdMotivoCorreccion),0) FROM Trans_oc_pol_motivo_correccion"

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
