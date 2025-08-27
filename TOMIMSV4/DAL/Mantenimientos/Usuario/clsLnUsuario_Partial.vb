Imports System.Data.SqlClient
Imports System.DirectoryServices
Imports System.Reflection

Partial Public Class clsLnUsuario

    Public Shared Function Listar(ByVal Activos As Boolean) As DataTable

        Try

            Dim sp As String = "SELECT IdUsuario as Codigo, nombres, apellidos, cedula, direccion, " &
                " telefono, email, codigo, clave, ultimo_login, activo, user_agr, fec_agr, " &
                " user_mod, fec_mod FROM Usuario WHERE 1 >0  "

            If Activos Then
                sp += " and activo =1 "
            End If

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub Actualiza_Ultimo_Ingreso(ByRef oBeUsuario As clsBeUsuario)

        Try

            Dim vSQL As String = "UPDATE usuario set ultimo_login =GETDATE() Where IdUsuario = @IdUsuario"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(vSQL, lConnection)
            Dim rowsAffected As Integer = 0
            lConnection.Open()

            cmd.Parameters.Add(New SqlParameter("@IDUSUARIO", oBeUsuario.IdUsuario))

            rowsAffected = cmd.ExecuteNonQuery()

            lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Get_Usuario_Valido_For_DTS(ByRef oBeUsuario As clsBeUsuario, ByVal pIdBodega As Integer) As Boolean

        Get_Usuario_Valido_For_DTS = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim vSQL As String = "SELECT usuario.IdUsuario, 
                    usuario.nombres, 
                    usuario.apellidos,
                    usuario_bodega.idrol,
                    usuario.clave 
                    FROM usuario INNER JOIN " &
                   " usuario_bodega ON usuario.IdUsuario = usuario_bodega.IdUsuario " &
                   " WHERE  usuario.codigo = @Codigo " &
                   " and usuario.IdEmpresa =@IdEmpresa " &
                   " and usuario_bodega.IdBodega =@IdBodega " &
                   " and usuario_bodega.activo =1" 'validad usuario este activo
            'No mostrar columna Clave

            Dim sp As String = vSQL

            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeUsuario.IdEmpresa))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", pIdBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@CODIGO", clsPublic.Encriptar(oBeUsuario.Codigo)))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then

                oBeUsuario.IdUsuario = IIf(IsDBNull(dt.Rows(0).Item("IdUsuario")), "", dt.Rows(0).Item("IdUsuario"))
                oBeUsuario.Nombres = IIf(IsDBNull(dt.Rows(0).Item("Nombres")), "", dt.Rows(0).Item("Nombres"))
                oBeUsuario.Apellidos = IIf(IsDBNull(dt.Rows(0).Item("Apellidos")), "", dt.Rows(0).Item("Apellidos"))
                oBeUsuario.IdRol = IIf(IsDBNull(dt.Rows(0).Item("IdRol")), 0, dt.Rows(0).Item("IdRol"))
                oBeUsuario.Clave = IIf(IsDBNull(dt.Rows(0).Item("clave")), 0, dt.Rows(0).Item("clave"))
                oBeUsuario.Clave = clsPublic.Desencriptar(oBeUsuario.Clave)
                Get_Usuario_Valido_For_DTS = True

            Else

                dt.Dispose()

                Dim vSQL1 As String = "SELECT usuario.IdUsuario, 
                                        usuario.nombres, 
                                        usuario.apellidos,
                                        usuario.clave 
                                        FROM usuario 
                                        WHERE usuario.codigo = @Codigo 
                                        AND usuario.IdEmpresa =@IdEmpresa "

                cmd = New SqlCommand(vSQL1, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                dad = New SqlDataAdapter(cmd)

                cmd.Parameters.Clear()

                dad.SelectCommand.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeUsuario.IdEmpresa))
                dad.SelectCommand.Parameters.Add(New SqlParameter("@CODIGO", clsPublic.Encriptar(oBeUsuario.Codigo)))
                dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", pIdBodega))

                dad.Fill(dt)

                If dt.Rows.Count > 0 Then

                    'El usuario no existe para la bodega, pero lo vamos a asociar.
                    Dim BeUsuarioBodega As New clsBeUsuario_bodega
                    BeUsuarioBodega.IdUsuarioBodega = clsLnUsuario_bodega.MaxID() + 1
                    BeUsuarioBodega.IdUsuario = IIf(IsDBNull(dt.Rows(0).Item("IdUsuario")), "", dt.Rows(0).Item("IdUsuario"))
                    BeUsuarioBodega.IdBodega = pIdBodega
                    BeUsuarioBodega.Activo = True
                    BeUsuarioBodega.IdRol = 1
                    BeUsuarioBodega.User_agr = "Admin_DTS"
                    BeUsuarioBodega.User_mod = "Admin_DTS"
                    BeUsuarioBodega.Fec_agr = Now
                    BeUsuarioBodega.Fec_mod = Now
                    clsLnUsuario_bodega.Insertar(BeUsuarioBodega, lConnection, lTransaction)

                    oBeUsuario.IdUsuario = BeUsuarioBodega.IdUsuario
                    oBeUsuario = GetSingle(oBeUsuario, lConnection, lTransaction)

                End If

            End If

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try


    End Function

    Public Shared Function Usuario_Valido(ByRef oBeUsuario As clsBeUsuario, ByVal pIdBodega As Integer) As Boolean

        Usuario_Valido = False

        Try

            Dim vSQL As String = "SELECT usuario.IdUsuario, usuario.nombres, usuario.apellidos,usuario_bodega.idrol " &
                   " FROM usuario INNER JOIN " &
                   " usuario_bodega ON usuario.IdUsuario = usuario_bodega.IdUsuario " &
                   " WHERE  usuario.codigo = @Codigo " &
                   " and usuario.clave = @Clave " &
                   " and usuario.IdEmpresa =@IdEmpresa " &
                   " and usuario_bodega.IdBodega =@IdBodega " &
                   " and usuario_bodega.activo =1" 'validad usuario este activo
            'No mostrar columna Clave

            Dim sp As String = vSQL
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)


            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeUsuario.IdEmpresa))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", pIdBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@CODIGO", clsPublic.Encriptar(oBeUsuario.Codigo)))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@CLAVE", clsPublic.Encriptar(oBeUsuario.Clave)))

            Dim vResultDesencriptar As String = clsPublic.Desencriptar("eFUp98pb+Aw=")
            Dim vResultEncriptar As String = clsPublic.Encriptar(oBeUsuario.Clave)

            'u4AXhWg6c5A

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then

                oBeUsuario.IdUsuario = IIf(IsDBNull(dt.Rows(0).Item("IdUsuario")), "", dt.Rows(0).Item("IdUsuario"))
                oBeUsuario.Nombres = IIf(IsDBNull(dt.Rows(0).Item("Nombres")), "", dt.Rows(0).Item("Nombres"))
                oBeUsuario.Apellidos = IIf(IsDBNull(dt.Rows(0).Item("Apellidos")), "", dt.Rows(0).Item("Apellidos"))
                oBeUsuario.IdRol = IIf(IsDBNull(dt.Rows(0).Item("IdRol")), 0, dt.Rows(0).Item("IdRol"))

                Usuario_Valido = True

            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Shared Function Usuario_Valido_For_TMS_By_IdEmpresa(ByRef oBeUsuario As clsBeUsuario) As Boolean

        Usuario_Valido_For_TMS_By_IdEmpresa = False

        Try

            Dim vSQL As String = "SELECT usuario.IdUsuario, usuario.nombres, usuario.apellidos
                    FROM usuario 
                    WHERE usuario.codigo = @Codigo 
                    and usuario.clave = @Clave 
                    and usuario.IdEmpresa =@IdEmpresa 
                    and usuario.activo =1 "

            Dim sp As String = vSQL
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeUsuario.IdEmpresa))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@CODIGO", clsPublic.Encriptar(oBeUsuario.Codigo)))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@CLAVE", clsPublic.Encriptar(oBeUsuario.Clave)))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then

                oBeUsuario.IdUsuario = IIf(IsDBNull(dt.Rows(0).Item("IdUsuario")), "", dt.Rows(0).Item("IdUsuario"))
                oBeUsuario.Nombres = IIf(IsDBNull(dt.Rows(0).Item("Nombres")), "", dt.Rows(0).Item("Nombres"))
                oBeUsuario.Apellidos = IIf(IsDBNull(dt.Rows(0).Item("Apellidos")), "", dt.Rows(0).Item("Apellidos"))
                oBeUsuario.IdRol = 0

                Usuario_Valido_For_TMS_By_IdEmpresa = True

            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Shared Function Rol_Administrador(ByRef oBeUsuario As clsBeUsuario, ByVal pIdBodega As Integer) As Boolean

        Rol_Administrador = False

        Try

            Dim vSQL As String = "SELECT usuario_bodega.idrol 
                                  FROM usuario INNER JOIN 
                                       usuario_bodega ON usuario.IdUsuario = usuario_bodega.IdUsuario 
                                  WHERE  usuario.codigo = @Codigo 
                                     and usuario_bodega.IdBodega =@IdBodega 
                                     and usuario.IdEmpresa =@IdEmpresa 
                                     and usuario_bodega.IdRol = 1 
                                     and usuario_bodega.activo = 1" 'validad usuario este activo
            'No mostrar columna Clave

            Dim sp As String = vSQL
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)


            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeUsuario.IdEmpresa))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", pIdBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@CODIGO", clsPublic.Encriptar(oBeUsuario.Codigo)))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then

                Rol_Administrador = True

            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Shared Function GetAllByIdEmpresa(ByVal pIdEmpresa As Integer) As List(Of clsBeUsuario)

        Try

            Dim lReturnList As New List(Of clsBeUsuario)
            Const sp As String = "SELECT * FROM Usuario WHERE IdEmpresa = @IdEmpresa"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdEmpresa", pIdEmpresa)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeUsuario As New clsBeUsuario

            For Each dr As DataRow In dt.Rows

                vBeUsuario = New clsBeUsuario
                Cargar(vBeUsuario, dr)
                lReturnList.Add(vBeUsuario)

            Next

            lConnection.Dispose()
            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllUsuariosSistema(ByVal pIdEmpresa As Integer) As List(Of clsBeUsuario)

        Try

            Dim lReturnList As New List(Of clsBeUsuario)
            Const sp As String = "SELECT * FROM usuario WHERE IdEmpresa = @IdEmpresa AND Activo = 1 And Sistema = 1"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdEmpresa", pIdEmpresa)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeUsuario As New clsBeUsuario

            For Each dr As DataRow In dt.Rows

                vBeUsuario = New clsBeUsuario
                Cargar(vBeUsuario, dr)
                lReturnList.Add(vBeUsuario)

            Next

            lConnection.Dispose()
            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Clave_Autorizacion_Es_Valida(ByVal pClaveAutorizacionEncriptada As String) As Boolean

        Clave_Autorizacion_Es_Valida = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM Usuario Where(clave_autorizacion = @clave_autorizacion)"
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@clave_autorizacion", pClaveAutorizacionEncriptada))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then
                Clave_Autorizacion_Es_Valida = True
            End If

            lTransaction.Commit()

        Catch ex1 As SqlException
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Usuario_Valido_LDAP(ByVal pUsuario As String, ByVal pClave As String) As Boolean

        Usuario_Valido_LDAP = False

        Try

            Dim vPathLDAP As String = "LDAP://SvrDominio.byb.gt"
            Dim vUsuario As String = "byb\wmsbodega1"
            pClave = "31/05+Byb"

            Dim de As New DirectoryEntry(vPathLDAP, vUsuario, pClave, AuthenticationTypes.Secure)

            Dim ds As New DirectorySearcher(de)
            Dim rs As SearchResult
            rs = ds.FindOne()

            If Not rs Is Nothing Then
                Usuario_Valido_LDAP = True
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Reporte_Resoluciones_Usuario() As DataTable

        Dim lTable As New DataTable("Result")

        Try

            Dim vSQL As String = "SELECT * FROM VW_Resoluciones_Usuario "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.CommandTimeout = 100
                        lDataAdapter.Fill(lTable)
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
