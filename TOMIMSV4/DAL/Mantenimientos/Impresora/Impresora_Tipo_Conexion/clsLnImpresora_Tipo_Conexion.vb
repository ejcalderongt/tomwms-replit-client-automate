Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnImpresora_Tipo_Conexion

    Public Shared Sub Cargar(ByRef oBeImpresora_Tipo_Conexion As clsBeImpresora_Tipo_Conexion, ByRef dr As DataRow)
        Try
            With oBeImpresora_Tipo_Conexion
                .IdImpresoraTipoConexion = IIf(IsDBNull(dr.Item("IdImpresoraTipoConexion")), 0, dr.Item("IdImpresoraTipoConexion"))
                .Codigo = IIf(IsDBNull(dr.Item("Codigo")), "", dr.Item("Codigo"))
                .Nombre = IIf(IsDBNull(dr.Item("Nombre")), "", dr.Item("Nombre"))
                .User_agr = IIf(IsDBNull(dr.Item("User_agr")), "", dr.Item("User_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("Fec_agr")), Date.Now, dr.Item("Fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("User_mod")), "", dr.Item("User_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("Fec_mod")), Date.Now, dr.Item("Fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("Activo")), False, dr.Item("Activo"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeImpresora_Tipo_Conexion As clsBeImpresora_Tipo_Conexion, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("impresora_tipo_conexion")
            Ins.Add("idimpresoratipoconexion", "@idimpresoratipoconexion", DataType.Parametro)
            Ins.Add("codigo", "@codigo", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDIMPRESORATIPOCONEXION", oBeImpresora_Tipo_Conexion.IdImpresoraTipoConexion))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeImpresora_Tipo_Conexion.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeImpresora_Tipo_Conexion.Nombre))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeImpresora_Tipo_Conexion.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeImpresora_Tipo_Conexion.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeImpresora_Tipo_Conexion.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeImpresora_Tipo_Conexion.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeImpresora_Tipo_Conexion.Activo))

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

    Public Shared Function Actualizar(ByRef oBeImpresora_Tipo_Conexion As clsBeImpresora_Tipo_Conexion, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("impresora_tipo_conexion")
            Upd.Add("idimpresoratipoconexion", "@idimpresoratipoconexion", DataType.Parametro)
            Upd.Add("codigo", "@codigo", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("IdImpresoraTipoConexion = @IdImpresoraTipoConexion")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDIMPRESORATIPOCONEXION", oBeImpresora_Tipo_Conexion.IdImpresoraTipoConexion))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeImpresora_Tipo_Conexion.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeImpresora_Tipo_Conexion.Nombre))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeImpresora_Tipo_Conexion.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeImpresora_Tipo_Conexion.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeImpresora_Tipo_Conexion.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeImpresora_Tipo_Conexion.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeImpresora_Tipo_Conexion.Activo))

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


    Public Shared Function Eliminar(ByRef oBeImpresora_Tipo_Conexion As clsBeImpresora_Tipo_Conexion, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Impresora_Tipo_Conexion" & _
             "  Where(IdImpresoraTipoConexion = @IdImpresoraTipoConexion)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDIMPRESORATIPOCONEXION", oBeImpresora_Tipo_Conexion.IdImpresoraTipoConexion))

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

            Const sp As String = "SELECT * FROM Impresora_Tipo_Conexion"
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

    Public Shared Function Get_All() As List(Of clsBeImpresora_Tipo_Conexion)

        Dim lReturnList As New List(Of clsBeImpresora_Tipo_Conexion)

        Try

            Const sp As String = "SELECT * FROM Impresora_Tipo_Conexion"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeImpresora_Tipo_Conexion As New clsBeImpresora_Tipo_Conexion

                        For Each dr As DataRow In lDataTable.Rows
                            vBeImpresora_Tipo_Conexion = New clsBeImpresora_Tipo_Conexion()
                            Cargar(vBeImpresora_Tipo_Conexion, dr)
                            lReturnList.Add(vBeImpresora_Tipo_Conexion)
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

    Public Shared Sub GetSingle(ByRef pBeImpresora_Tipo_Conexion As clsBeImpresora_Tipo_Conexion)

        Try

            Const sp As String = "SELECT * FROM Impresora_Tipo_Conexion" & _
            " Where(IdImpresoraTipoConexion = @IdImpresoraTipoConexion)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeImpresora_Tipo_Conexion As New clsBeImpresora_Tipo_Conexion

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeImpresora_Tipo_Conexion, lDataTable.Rows(0))
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

            Const sp As String = "SELECT ISNULL(Max(IdImpresoraTipoConexion),0) FROM Impresora_Tipo_Conexion"

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

End Class
