Imports System.Data.SqlClient

Public Class clsLnConfiguracion_alias_campos

    Public Shared Sub Cargar(ByRef oBeConfiguracion_alias_campos As clsBeConfiguracion_alias_campos, ByRef dr As DataRow)
        Try
            With oBeConfiguracion_alias_campos
                .IdConfiguracionAlias = IIf(IsDBNull(dr.Item("IdConfiguracionAlias")), 0, dr.Item("IdConfiguracionAlias"))
                .Nombre_WMS = IIf(IsDBNull(dr.Item("Nombre_WMS")), "", dr.Item("Nombre_WMS"))
                .Alias_WMS = IIf(IsDBNull(dr.Item("Alias_WMS")), "", dr.Item("Alias_WMS"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeConfiguracion_alias_campos As clsBeConfiguracion_alias_campos, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("configuracion_alias_campos")
            Ins.Add("idconfiguracionalias", "@idconfiguracionalias", DataType.Parametro)
            Ins.Add("nombre_wms", "@nombre_wms", DataType.Parametro)
            Ins.Add("Alias_WMS", "@Alias_WMS", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCONFIGURACIONALIAS", oBeConfiguracion_alias_campos.IdConfiguracionAlias))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_WMS", oBeConfiguracion_alias_campos.Nombre_WMS))
            cmd.Parameters.Add(New SqlParameter("@Alias_WMS", oBeConfiguracion_alias_campos.Alias_WMS))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeConfiguracion_alias_campos As clsBeConfiguracion_alias_campos, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("configuracion_alias_campos")
            Upd.Add("idconfiguracionalias", "@idconfiguracionalias", DataType.Parametro)
            Upd.Add("nombre_wms", "@nombre_wms", DataType.Parametro)
            Upd.Add("Alias_WMS", "@Alias_WMS", DataType.Parametro)
            Upd.Where("IdConfiguracionAlias = @IdConfiguracionAlias")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCONFIGURACIONALIAS", oBeConfiguracion_alias_campos.IdConfiguracionAlias))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_WMS", oBeConfiguracion_alias_campos.Nombre_WMS))
            cmd.Parameters.Add(New SqlParameter("@Alias_WMS", oBeConfiguracion_alias_campos.Alias_WMS))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function


    Public Shared Function Eliminar(ByRef oBeConfiguracion_alias_campos As clsBeConfiguracion_alias_campos, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Configuracion_alias_campos" &
             "  Where(IdConfiguracionAlias = @IdConfiguracionAlias)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCONFIGURACIONALIAS", oBeConfiguracion_alias_campos.IdConfiguracionAlias))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM Configuracion_alias_campos"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
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
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_All() As List(Of clsBeConfiguracion_alias_campos)

        Dim lReturnList As New List(Of clsBeConfiguracion_alias_campos)

        Get_All = Nothing

        Try

            Const sp As String = "SELECT * FROM Configuracion_alias_campos "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeConfiguracion_alias_campos As New clsBeConfiguracion_alias_campos

                        If Not lDataTable Is Nothing Then

                            If lDataTable.Rows.Count > 0 Then

                                For Each dr As DataRow In lDataTable.Rows
                                    vBeConfiguracion_alias_campos = New clsBeConfiguracion_alias_campos()
                                    Cargar(vBeConfiguracion_alias_campos, dr)
                                    lReturnList.Add(vBeConfiguracion_alias_campos)
                                Next

                                Get_All = lReturnList

                            End If

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

    Public Shared Sub GetSingle(ByRef pBeConfiguracion_alias_campos As clsBeConfiguracion_alias_campos)

        Try

            Const sp As String = "SELECT * FROM Configuracion_alias_campos" &
            " Where(IdConfiguracionAlias = @IdConfiguracionAlias)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeConfiguracion_alias_campos As New clsBeConfiguracion_alias_campos

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeConfiguracion_alias_campos, lDataTable.Rows(0))
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

            Const sp As String = "SELECT ISNULL(Max(IdConfiguracionAlias),0) FROM Configuracion_alias_campos"

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
            Throw ex
        End Try

    End Function

End Class
