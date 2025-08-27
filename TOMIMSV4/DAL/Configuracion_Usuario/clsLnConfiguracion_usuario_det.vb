Imports System.Data.SqlClient

Public Class clsLnConfiguracion_usuario_det

    Public Shared Sub Cargar(ByRef oBeConfiguracion_usuario_det As clsBeConfiguracion_usuario_det, ByRef dr As DataRow)

        Try

            With oBeConfiguracion_usuario_det
                .IdConfiguracionUsuarioDet = IIf(IsDBNull(dr.Item("IdConfiguracionUsuarioDet")), 0, dr.Item("IdConfiguracionUsuarioDet"))
                .IdConfiguracionUsuarioEnc = IIf(IsDBNull(dr.Item("IdConfiguracionUsuarioEnc")), 0, dr.Item("IdConfiguracionUsuarioEnc"))
                .Maquina_Host = IIf(IsDBNull(dr.Item("Maquina_Host")), "", dr.Item("Maquina_Host"))
                .Maquina_IP = IIf(IsDBNull(dr.Item("Maquina_IP")), "", dr.Item("Maquina_IP"))
                .Nombre_Template = IIf(IsDBNull(dr.Item("Nombre_Template")), "", dr.Item("Nombre_Template"))
                .String_Template = IIf(IsDBNull(dr.Item("String_Template")), "", dr.Item("String_Template"))
                .Fecha_Agregado = IIf(IsDBNull(dr.Item("Fecha_Agregado")), Date.Now, dr.Item("Fecha_Agregado"))
                .Fecha_Modificado = IIf(IsDBNull(dr.Item("Fecha_Modificado")), Date.Now, dr.Item("Fecha_Modificado"))
            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Public Shared Function Insertar(ByRef oBeConfiguracion_usuario_det As clsBeConfiguracion_usuario_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("configuracion_usuario_det")
            Ins.Add("idconfiguracionusuariodet", "@idconfiguracionusuariodet", DataType.Parametro)
            Ins.Add("idconfiguracionusuarioenc", "@idconfiguracionusuarioenc", DataType.Parametro)
            Ins.Add("maquina_host", "@maquina_host", DataType.Parametro)
            Ins.Add("maquina_ip", "@maquina_ip", DataType.Parametro)
            Ins.Add("nombre_template", "@nombre_template", DataType.Parametro)
            Ins.Add("string_template", "@string_template", DataType.Parametro)
            Ins.Add("fecha_agregado", "@fecha_agregado", DataType.Parametro)
            Ins.Add("fecha_modificado", "@fecha_modificado", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCONFIGURACIONUSUARIODET", oBeConfiguracion_usuario_det.IdConfiguracionUsuarioDet))
            cmd.Parameters.Add(New SqlParameter("@IDCONFIGURACIONUSUARIOENC", oBeConfiguracion_usuario_det.IdConfiguracionUsuarioEnc))
            cmd.Parameters.Add(New SqlParameter("@MAQUINA_HOST", oBeConfiguracion_usuario_det.Maquina_Host))
            cmd.Parameters.Add(New SqlParameter("@MAQUINA_IP", oBeConfiguracion_usuario_det.Maquina_IP))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_TEMPLATE", oBeConfiguracion_usuario_det.Nombre_Template))
            cmd.Parameters.Add(New SqlParameter("@STRING_TEMPLATE", oBeConfiguracion_usuario_det.String_Template))
            cmd.Parameters.Add(New SqlParameter("@FECHA_AGREGADO", oBeConfiguracion_usuario_det.Fecha_Agregado))
            cmd.Parameters.Add(New SqlParameter("@FECHA_MODIFICADO", oBeConfiguracion_usuario_det.Fecha_Modificado))

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
    Public Shared Function Actualizar(ByRef oBeConfiguracion_usuario_det As clsBeConfiguracion_usuario_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("configuracion_usuario_det")
            Upd.Add("idconfiguracionusuariodet", "@idconfiguracionusuariodet", DataType.Parametro)
            Upd.Add("idconfiguracionusuarioenc", "@idconfiguracionusuarioenc", DataType.Parametro)
            Upd.Add("maquina_host", "@maquina_host", DataType.Parametro)
            Upd.Add("maquina_ip", "@maquina_ip", DataType.Parametro)
            Upd.Add("nombre_template", "@nombre_template", DataType.Parametro)
            Upd.Add("string_template", "@string_template", DataType.Parametro)
            Upd.Add("fecha_agregado", "@fecha_agregado", DataType.Parametro)
            Upd.Add("fecha_modificado", "@fecha_modificado", DataType.Parametro)
            Upd.Where("IdConfiguracionUsuarioDet = @IdConfiguracionUsuarioDet")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCONFIGURACIONUSUARIODET", oBeConfiguracion_usuario_det.IdConfiguracionUsuarioDet))
            cmd.Parameters.Add(New SqlParameter("@IDCONFIGURACIONUSUARIOENC", oBeConfiguracion_usuario_det.IdConfiguracionUsuarioEnc))
            cmd.Parameters.Add(New SqlParameter("@MAQUINA_HOST", oBeConfiguracion_usuario_det.Maquina_Host))
            cmd.Parameters.Add(New SqlParameter("@MAQUINA_IP", oBeConfiguracion_usuario_det.Maquina_IP))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_TEMPLATE", oBeConfiguracion_usuario_det.Nombre_Template))
            cmd.Parameters.Add(New SqlParameter("@STRING_TEMPLATE", oBeConfiguracion_usuario_det.String_Template))
            cmd.Parameters.Add(New SqlParameter("@FECHA_AGREGADO", oBeConfiguracion_usuario_det.Fecha_Agregado))
            cmd.Parameters.Add(New SqlParameter("@FECHA_MODIFICADO", oBeConfiguracion_usuario_det.Fecha_Modificado))

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
    Public Shared Function Eliminar(ByRef oBeConfiguracion_usuario_det As clsBeConfiguracion_usuario_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Configuracion_usuario_det 
							       Where(IdConfiguracionUsuarioDet = @IdConfiguracionUsuarioDet 
								   AND IdConfiguracionUsuarioEnc =@IdConfiguracionUsuarioEnc AND Nombre_Template = @NombreTemplate)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCONFIGURACIONUSUARIODET", oBeConfiguracion_usuario_det.IdConfiguracionUsuarioDet))
            cmd.Parameters.Add(New SqlParameter("@IDCONFIGURACIONUSUARIOENC", oBeConfiguracion_usuario_det.IdConfiguracionUsuarioEnc))
            cmd.Parameters.Add(New SqlParameter("@NOMBRETEMPLATE", oBeConfiguracion_usuario_det.Nombre_Template))


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
    Public Shared Function MaxID(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdConfiguracionUsuarioDet),0) FROM Configuracion_usuario_det"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

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
    Public Shared Function Get_Single_By_IdUsuario_And_NombreTemplate(ByVal pIdEmpresa As Integer,
                                                                     ByVal pIdUsuario As Integer,
                                                                     ByVal pNombreTemplate As String) As clsBeConfiguracion_usuario_det

        Get_Single_By_IdUsuario_And_NombreTemplate = Nothing

        Try

            Const sp As String = "SELECT * FROM VW_Configuracion_Usuario_Template " &
                                 " WHERE(IdEmpresa = @IdEmpresa 
										 AND IdUsuario = @IdUsuario 
										 AND Nombre_Template = @NombreTemplate)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdEmpresa", pIdEmpresa)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdUsuario", pIdUsuario)
                        lDTA.SelectCommand.Parameters.AddWithValue("@NombreTemplate", pNombreTemplate)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeConfiguracion_usuario_det As New clsBeConfiguracion_usuario_det

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeConfiguracion_usuario_det, lDataTable.Rows(0))
                            Get_Single_By_IdUsuario_And_NombreTemplate = vBeConfiguracion_usuario_det
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
    Public Shared Function Get_Single_By_IdUsuario_And_NombreTemplate(ByVal pIdEmpresa As Integer,
                                                                      ByVal pIdUsuario As Integer,
                                                                      ByVal pNombreTemplate As String,
                                                                      ByVal lConnection As SqlConnection,
                                                                      ByVal lTransaction As SqlTransaction) As clsBeConfiguracion_usuario_det

        Get_Single_By_IdUsuario_And_NombreTemplate = Nothing

        Try

            Const sp As String = "SELECT * FROM VW_Configuracion_Usuario_Template " &
                                 " WHERE(IdEmpresa = @IdEmpresa 
								   AND IdUsuario = @IdUsuario 
								   AND Nombre_Template = @NombreTemplate)"


            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdEmpresa", pIdEmpresa)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUsuario", pIdUsuario)
                lDTA.SelectCommand.Parameters.AddWithValue("@NombreTemplate", pNombreTemplate)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeConfiguracion_usuario_det As New clsBeConfiguracion_usuario_det

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Cargar(vBeConfiguracion_usuario_det, lDataTable.Rows(0))
                    Get_Single_By_IdUsuario_And_NombreTemplate = vBeConfiguracion_usuario_det
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Shared Function Actualizar_Template(ByRef oBeConfiguracion_usuario_det As clsBeConfiguracion_usuario_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("configuracion_usuario_det")
            Upd.Add("string_template", "@string_template", DataType.Parametro)
            Upd.Add("fecha_modificado", "@fecha_modificado", DataType.Parametro)
            Upd.Where("IdConfiguracionUsuarioDet = @IdConfiguracionUsuarioDet AND IdConfiguracionUsuarioEnc = @IdConfiguracionUsuarioEnc ")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCONFIGURACIONUSUARIODET", oBeConfiguracion_usuario_det.IdConfiguracionUsuarioDet))
            cmd.Parameters.Add(New SqlParameter("@IDCONFIGURACIONUSUARIOENC", oBeConfiguracion_usuario_det.IdConfiguracionUsuarioEnc))
            cmd.Parameters.Add(New SqlParameter("@STRING_TEMPLATE", oBeConfiguracion_usuario_det.String_Template))
            cmd.Parameters.Add(New SqlParameter("@FECHA_MODIFICADO", oBeConfiguracion_usuario_det.Fecha_Modificado))

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

End Class
