Imports System.Data.SqlClient

Partial Public Class clsLnRol_usuario_estado

    Public Shared Function Get_All_By_IdRol(ByVal IdRol As Integer) As List(Of clsBeRol_usuario_estado)

        Dim lReturnList As New List(Of clsBeRol_usuario_estado)

        Try

            Const sp As String = "SELECT * FROM VW_Rol_Usuario_Estado WHERE IdRolUsuario = @IdRolUsuario AND Activo = 1 "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdRolUsuario", IdRol)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeUsuario_estado As New clsBeRol_usuario_estado

                        For Each dr As DataRow In lDataTable.Rows
                            vBeUsuario_estado = New clsBeRol_usuario_estado()
                            Cargar(vBeUsuario_estado, dr)
                            vBeUsuario_estado.NombrePropietario = IIf(IsDBNull(dr.Item("Propietario")), "", dr.Item("Propietario"))
                            vBeUsuario_estado.NombreEstadoOrigen = IIf(IsDBNull(dr.Item("EstadoOrigen")), "", dr.Item("EstadoOrigen"))
                            vBeUsuario_estado.NombreEstadoDestino = IIf(IsDBNull(dr.Item("EstadoDestino")), "", dr.Item("EstadoDestino"))
                            lReturnList.Add(vBeUsuario_estado)
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

    Public Shared Function Get_All_By_IdRol_And_IdEstadoOrigen(ByVal IdRol As Integer, ByVal IdEstadoOrigen As Integer) As List(Of clsBeRol_usuario_estado)

        Dim lReturnList As New List(Of clsBeRol_usuario_estado)

        Try

            Const sp As String = "SELECT * FROM VW_Rol_Usuario_Estado WHERE IdRolUsuario = @IdRolUsuario AND Activo = 1 AND IdEstadoOrigen = @IdEstadoOrigen"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdRolUsuario", IdRol)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdEstadoOrigen", IdEstadoOrigen)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeUsuario_estado As New clsBeRol_usuario_estado

                        For Each dr As DataRow In lDataTable.Rows
                            vBeUsuario_estado = New clsBeRol_usuario_estado()
                            Cargar(vBeUsuario_estado, dr)
                            vBeUsuario_estado.NombrePropietario = IIf(IsDBNull(dr.Item("Propietario")), "", dr.Item("Propietario"))
                            vBeUsuario_estado.NombreEstadoOrigen = IIf(IsDBNull(dr.Item("EstadoOrigen")), "", dr.Item("EstadoOrigen"))
                            vBeUsuario_estado.NombreEstadoDestino = IIf(IsDBNull(dr.Item("EstadoDestino")), "", dr.Item("EstadoDestino"))
                            lReturnList.Add(vBeUsuario_estado)
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

    Public Shared Function Desactivar(ByRef BeUsuarioEstado As clsBeRol_usuario_estado,
                                      Optional ByVal pConection As SqlConnection = Nothing,
                                      Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("rol_usuario_estado")
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("IDROLUSUARIOESTADO = @IDROLUSUARIOESTADO")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDROLUSUARIOESTADO", BeUsuarioEstado.IdRolUsuarioEstado))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", 0))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", BeUsuarioEstado.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", BeUsuarioEstado.User_mod))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
