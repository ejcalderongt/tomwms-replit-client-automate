Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnRol_usuario_estado

    Public Shared Sub Cargar(ByRef oBeRol_usuario_estado As clsBeRol_usuario_estado, ByRef dr As DataRow)
        Try
            With oBeRol_usuario_estado
                .IdRolUsuarioEstado = IIf(IsDBNull(dr.Item("IdRolUsuarioEstado")), 0, dr.Item("IdRolUsuarioEstado"))
                .IdRolUsuario = IIf(IsDBNull(dr.Item("IdRolUsuario")), 0, dr.Item("IdRolUsuario"))
                .IdPropietario = IIf(IsDBNull(dr.Item("IdPropietario")), 0, dr.Item("IdPropietario"))
                .IdEstadoOrigen = IIf(IsDBNull(dr.Item("IdEstadoOrigen")), 0, dr.Item("IdEstadoOrigen"))
                .IdEstadoDestino = IIf(IsDBNull(dr.Item("IdEstadoDestino")), 0, dr.Item("IdEstadoDestino"))
                .Permitir = IIf(IsDBNull(dr.Item("Permitir")), False, dr.Item("Permitir"))
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

    Public Shared Function Insertar(ByRef oBeRol_usuario_estado As clsBeRol_usuario_estado, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("rol_usuario_estado")
            Ins.Add("idrolusuarioestado", "@idrolusuarioestado", WMSTipoDato.Tipo.Parametro)
            Ins.Add("idrolusuario", "@idrolusuario", WMSTipoDato.Tipo.Parametro)
            Ins.Add("idpropietario", "@idpropietario", WMSTipoDato.Tipo.Parametro)
            Ins.Add("idestadoorigen", "@idestadoorigen", WMSTipoDato.Tipo.Parametro)
            Ins.Add("idestadodestino", "@idestadodestino", WMSTipoDato.Tipo.Parametro)
            Ins.Add("permitir", "@permitir", WMSTipoDato.Tipo.Parametro)
            Ins.Add("user_agr", "@user_agr", WMSTipoDato.Tipo.Parametro)
            Ins.Add("fec_agr", "@fec_agr", WMSTipoDato.Tipo.Parametro)
            Ins.Add("user_mod", "@user_mod", WMSTipoDato.Tipo.Parametro)
            Ins.Add("fec_mod", "@fec_mod", WMSTipoDato.Tipo.Parametro)
            Ins.Add("activo", "@activo", WMSTipoDato.Tipo.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDROLUSUARIOESTADO", oBeRol_usuario_estado.IdRolUsuarioEstado))
            cmd.Parameters.Add(New SqlParameter("@IDROLUSUARIO", oBeRol_usuario_estado.IdRolUsuario))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeRol_usuario_estado.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@IDESTADOORIGEN", oBeRol_usuario_estado.IdEstadoOrigen))
            cmd.Parameters.Add(New SqlParameter("@IDESTADODESTINO", oBeRol_usuario_estado.IdEstadoDestino))
            cmd.Parameters.Add(New SqlParameter("@PERMITIR", oBeRol_usuario_estado.Permitir))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeRol_usuario_estado.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeRol_usuario_estado.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeRol_usuario_estado.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeRol_usuario_estado.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeRol_usuario_estado.Activo))

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

    Public Shared Function Actualizar(ByRef oBeRol_usuario_estado As clsBeRol_usuario_estado, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("rol_usuario_estado")
            Upd.Add("idrolusuarioestado", "@idrolusuarioestado", WMSTipoDato.Tipo.Parametro)
            Upd.Add("idrolusuario", "@idrolusuario", WMSTipoDato.Tipo.Parametro)
            Upd.Add("idpropietario", "@idpropietario", WMSTipoDato.Tipo.Parametro)
            Upd.Add("idestadoorigen", "@idestadoorigen", WMSTipoDato.Tipo.Parametro)
            Upd.Add("idestadodestino", "@idestadodestino", WMSTipoDato.Tipo.Parametro)
            Upd.Add("permitir", "@permitir", WMSTipoDato.Tipo.Parametro)
            Upd.Add("user_agr", "@user_agr", WMSTipoDato.Tipo.Parametro)
            Upd.Add("fec_agr", "@fec_agr", WMSTipoDato.Tipo.Parametro)
            Upd.Add("user_mod", "@user_mod", WMSTipoDato.Tipo.Parametro)
            Upd.Add("fec_mod", "@fec_mod", WMSTipoDato.Tipo.Parametro)
            Upd.Add("activo", "@activo", WMSTipoDato.Tipo.Parametro)
            Upd.Where("IdRolUsuarioEstado = @IdRolUsuarioEstado")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDROLUSUARIOESTADO", oBeRol_usuario_estado.IdRolUsuarioEstado))
            cmd.Parameters.Add(New SqlParameter("@IDROLUSUARIO", oBeRol_usuario_estado.IdRolUsuario))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeRol_usuario_estado.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@IDESTADOORIGEN", oBeRol_usuario_estado.IdEstadoOrigen))
            cmd.Parameters.Add(New SqlParameter("@IDESTADODESTINO", oBeRol_usuario_estado.IdEstadoDestino))
            cmd.Parameters.Add(New SqlParameter("@PERMITIR", oBeRol_usuario_estado.Permitir))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeRol_usuario_estado.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeRol_usuario_estado.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeRol_usuario_estado.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeRol_usuario_estado.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeRol_usuario_estado.Activo))

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


    Public Shared Function Eliminar(ByRef oBeRol_usuario_estado As clsBeRol_usuario_estado, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Rol_usuario_estado" & _
             "  Where(IdRolUsuarioEstado = @IdRolUsuarioEstado)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDROLUSUARIOESTADO", oBeRol_usuario_estado.IdRolUsuarioEstado))

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

            Const sp As String = "SELECT * FROM Rol_usuario_estado"
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

    Public Shared Function Get_All() As List(Of clsBeRol_usuario_estado)

        Dim lReturnList As New List(Of clsBeRol_usuario_estado)

        Try

            Const sp As String = "SELECT * FROM Rol_usuario_estado"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeRol_usuario_estado As New clsBeRol_usuario_estado

                        For Each dr As DataRow In lDataTable.Rows
                            vBeRol_usuario_estado = New clsBeRol_usuario_estado()
                            Cargar(vBeRol_usuario_estado, dr)
                            lReturnList.Add(vBeRol_usuario_estado)
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

    Public Shared Sub GetSingle(ByRef pBeRol_usuario_estado As clsBeRol_usuario_estado)

        Try

            Const sp As String = "SELECT * FROM Rol_usuario_estado" & _
            " Where(IdRolUsuarioEstado = @IdRolUsuarioEstado)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeRol_usuario_estado As New clsBeRol_usuario_estado

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeRol_usuario_estado, lDataTable.Rows(0))
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

            Const sp As String = "SELECT ISNULL(Max(IdRolUsuarioEstado),0) FROM Rol_usuario_estado"

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
