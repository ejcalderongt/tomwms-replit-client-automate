Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_oc_docu_ref

    Public Shared Sub Cargar(ByRef oBeTrans_oc_docu_ref As clsBeTrans_oc_docu_ref, ByRef dr As DataRow)
        Try
            With oBeTrans_oc_docu_ref
                .IdDocumentoRef = IIf(IsDBNull(dr.Item("IdDocumentoRef")), 0, dr.Item("IdDocumentoRef"))
                .Codigo = IIf(IsDBNull(dr.Item("Codigo")), "", dr.Item("Codigo"))
                .Nombre = IIf(IsDBNull(dr.Item("Nombre")), "", dr.Item("Nombre"))
                .Descripcion = IIf(IsDBNull(dr.Item("Descripcion")), "", dr.Item("Descripcion"))
                .FechaDocumento = IIf(IsDBNull(dr.Item("FechaDocumento")), Date.Now, dr.Item("FechaDocumento"))
                .FechaAsignacion = IIf(IsDBNull(dr.Item("FechaAsignacion")), Date.Now, dr.Item("FechaAsignacion"))
                .FechaAgregado = IIf(IsDBNull(dr.Item("FechaAgregado")), Date.Now, dr.Item("FechaAgregado"))
                .Asignado = IIf(IsDBNull(dr.Item("Asignado")), False, dr.Item("Asignado"))
                .Activo = IIf(IsDBNull(dr.Item("Activo")), False, dr.Item("Activo"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_oc_docu_ref As clsBeTrans_oc_docu_ref, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_oc_docu_ref")
            Ins.Add("iddocumentoref", "@iddocumentoref", DataType.Parametro)
            Ins.Add("codigo", "@codigo", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("descripcion", "@descripcion", DataType.Parametro)
            Ins.Add("fechadocumento", "@fechadocumento", DataType.Parametro)
            Ins.Add("fechaasignacion", "@fechaasignacion", DataType.Parametro)
            Ins.Add("fechaagregado", "@fechaagregado", DataType.Parametro)
            Ins.Add("asignado", "@asignado", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDDOCUMENTOREF", oBeTrans_oc_docu_ref.IdDocumentoRef))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeTrans_oc_docu_ref.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeTrans_oc_docu_ref.Nombre))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeTrans_oc_docu_ref.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@FECHADOCUMENTO", oBeTrans_oc_docu_ref.FechaDocumento))
            cmd.Parameters.Add(New SqlParameter("@FECHAASIGNACION", oBeTrans_oc_docu_ref.FechaAsignacion))
            cmd.Parameters.Add(New SqlParameter("@FECHAAGREGADO", oBeTrans_oc_docu_ref.FechaAgregado))
            cmd.Parameters.Add(New SqlParameter("@ASIGNADO", oBeTrans_oc_docu_ref.Asignado))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_oc_docu_ref.Activo))

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

    Public Shared Function Actualizar(ByRef oBeTrans_oc_docu_ref As clsBeTrans_oc_docu_ref, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_oc_docu_ref")
            Upd.Add("iddocumentoref", "@iddocumentoref", DataType.Parametro)
            Upd.Add("codigo", "@codigo", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("descripcion", "@descripcion", DataType.Parametro)
            Upd.Add("fechadocumento", "@fechadocumento", DataType.Parametro)
            Upd.Add("fechaasignacion", "@fechaasignacion", DataType.Parametro)
            Upd.Add("fechaagregado", "@fechaagregado", DataType.Parametro)
            Upd.Add("asignado", "@asignado", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("IdDocumentoRef = @IdDocumentoRef")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDDOCUMENTOREF", oBeTrans_oc_docu_ref.IdDocumentoRef))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeTrans_oc_docu_ref.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeTrans_oc_docu_ref.Nombre))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeTrans_oc_docu_ref.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@FECHADOCUMENTO", oBeTrans_oc_docu_ref.FechaDocumento))
            cmd.Parameters.Add(New SqlParameter("@FECHAASIGNACION", oBeTrans_oc_docu_ref.FechaAsignacion))
            cmd.Parameters.Add(New SqlParameter("@FECHAAGREGADO", oBeTrans_oc_docu_ref.FechaAgregado))
            cmd.Parameters.Add(New SqlParameter("@ASIGNADO", oBeTrans_oc_docu_ref.Asignado))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_oc_docu_ref.Activo))

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


    Public Shared Function Eliminar(ByRef oBeTrans_oc_docu_ref As clsBeTrans_oc_docu_ref, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_oc_docu_ref" & _
             "  Where(IdDocumentoRef = @IdDocumentoRef)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDDOCUMENTOREF", oBeTrans_oc_docu_ref.IdDocumentoRef))

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

            Const sp As String = "SELECT * FROM Trans_oc_docu_ref"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
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

    Public Shared Function Get_All() As List(Of clsBeTrans_oc_docu_ref)

        Dim lReturnList As New List(Of clsBeTrans_oc_docu_ref)

        Try

            Const sp As String = "SELECT * FROM Trans_oc_docu_ref"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_oc_docu_ref As New clsBeTrans_oc_docu_ref

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_oc_docu_ref = New clsBeTrans_oc_docu_ref()
                            Cargar(vBeTrans_oc_docu_ref, dr)
                            lReturnList.Add(vBeTrans_oc_docu_ref)
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

    Public Shared Sub GetSingle(ByRef pBeTrans_oc_docu_ref As clsBeTrans_oc_docu_ref)

        Try

            Const sp As String = "SELECT * FROM Trans_oc_docu_ref " &
            " Where(IdDocumentoRef = @IdDocumentoRef)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_oc_docu_ref As New clsBeTrans_oc_docu_ref

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTrans_oc_docu_ref, lDataTable.Rows(0))
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

            Const sp As String = "SELECT ISNULL(Max(IdDocumentoRef),0) FROM Trans_oc_docu_ref"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

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

End Class
