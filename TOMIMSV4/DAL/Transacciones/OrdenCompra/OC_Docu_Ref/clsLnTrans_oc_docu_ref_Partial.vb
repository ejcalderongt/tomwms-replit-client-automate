Imports System.Data.SqlClient

Partial Public Class clsLnTrans_oc_docu_ref

    Public Shared Function Desactivar(ByRef oBeTrans_oc_docu_ref As clsBeTrans_oc_docu_ref, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_oc_docu_ref")
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("IdDocumentoRef = @IdDocumentoRef")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDDOCUMENTOREF", oBeTrans_oc_docu_ref.IdDocumentoRef))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", False))

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

    Public Shared Function Get_Single_By_Codigo_Documento(ByRef pCodigoDocumento As String) As clsBeTrans_oc_docu_ref

        Get_Single_By_Codigo_Documento = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_oc_docu_ref " &
            " Where(Codigo = @Codigo)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@CODIGO", pCodigoDocumento)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_oc_docu_ref As New clsBeTrans_oc_docu_ref

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTrans_oc_docu_ref, lDataTable.Rows(0))
                            Get_Single_By_Codigo_Documento = vBeTrans_oc_docu_ref
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

    Public Shared Function Get_All_Activos_Pendientes_De_Asignar() As DataTable

        Get_All_Activos_Pendientes_De_Asignar = Nothing

        Dim lReturnList As New List(Of clsBeTrans_oc_docu_ref)

        Try

            Const sp As String = "SELECT IdDocumentoRef, Codigo, FechaDocumento FROM Trans_oc_docu_ref WHERE Activo = 1 AND Asignado = 0"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Get_All_Activos_Pendientes_De_Asignar = lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar_Fecha_Asignacion(ByVal pIdDocumentoRef As Integer,
                                                       Optional ByVal pConection As SqlConnection = Nothing,
                                                       Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_oc_docu_ref")
            Upd.Add("fechaasignacion", "@fechaasignacion", DataType.Parametro)
            Upd.Add("asignado", "@asignado", DataType.Parametro)
            Upd.Where("IdDocumentoRef = @IdDocumentoRef")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDDOCUMENTOREF", pIdDocumentoRef))
            cmd.Parameters.Add(New SqlParameter("@FECHAASIGNACION", Now))
            cmd.Parameters.Add(New SqlParameter("@ASIGNADO", True))

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

    Public Shared Function Get_Single_By_IdNoDocumento(ByVal pIdNoDocumentoRef) As DataTable

        Get_Single_By_IdNoDocumento = Nothing

        Try

            Const sp As String = "SELECT IdDocumentoRef, Codigo, FechaDocumento 
							      FROM Trans_oc_docu_ref 
								  WHERE (Activo = 1 AND Asignado = 1 AND IdDocumentoRef = @IdDocumentoRef)"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdDocumentoRef", pIdNoDocumentoRef)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Get_Single_By_IdNoDocumento = lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
