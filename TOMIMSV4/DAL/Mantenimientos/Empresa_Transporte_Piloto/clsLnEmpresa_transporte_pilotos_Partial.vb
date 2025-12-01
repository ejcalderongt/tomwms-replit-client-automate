Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnEmpresa_transporte_pilotos

    Public Shared Function GetNombre(ByVal pIdPiloto As Integer) As clsBeEmpresa_transporte_pilotos

        GetNombre = Nothing

        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM empresa_transporte_pilotos WHERE IdPiloto=@IdPiloto"

            Using lCnn As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDTA As New SqlDataAdapter(vSQL, lCnn)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdPiloto", pIdPiloto)

                    Dim lDT As New DataTable()
                    lDTA.Fill(lDT)

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        Dim lRow As DataRow = lDT.Rows(0)
                        Dim Obj As New clsBeEmpresa_transporte_pilotos()

                        Obj.IdPiloto = CType(lRow("IdPiloto"), Int32)
                        If lRow("Nombres") IsNot DBNull.Value AndAlso lRow("Nombres") IsNot Nothing Then
                            Obj.Nombres = CType(lRow("Nombres"), String)
                        End If
                        If lRow("Apellidos") IsNot DBNull.Value AndAlso lRow("Apellidos") IsNot Nothing Then
                            Obj.Apellidos = CType(lRow("Apellidos"), String)
                        End If
                        If lRow("activo") IsNot DBNull.Value AndAlso lRow("activo") IsNot Nothing Then
                            Obj.Activo = CType(lRow("activo"), Boolean)
                        End If
                        If lRow("user_agr") IsNot DBNull.Value AndAlso lRow("user_agr") IsNot Nothing Then
                            Obj.User_agr = CType(lRow("user_agr"), String)
                        End If
                        If lRow("fec_agr") IsNot DBNull.Value AndAlso lRow("fec_agr") IsNot Nothing Then
                            Obj.Fec_agr = CType(lRow("fec_agr"), DateTime)
                        End If
                        If lRow("user_mod") IsNot DBNull.Value AndAlso lRow("user_mod") IsNot Nothing Then
                            Obj.User_mod = CType(lRow("user_mod"), String)
                        End If
                        If lRow("fec_mod") IsNot DBNull.Value AndAlso lRow("fec_mod") IsNot Nothing Then
                            Obj.Fec_mod = CType(lRow("fec_mod"), DateTime)
                        End If

                        Return Obj

                    End If

                End Using

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Listar(ByVal pActivo As Boolean) As DataTable

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Listar = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim sp As String = "SELECT IdEmpresaTransporte,IdPiloto AS Código, Nombres, Apellidos,no_dpi AS 'No. DPI',
                                no_licencia AS 'No. Licencia',direccion AS Dirección 
                                FROM empresa_transporte_pilotos WHERE 1 > 0 "

            If pActivo Then
                sp += " AND Activo=1"
            Else
                sp += " AND Activo=0"
            End If

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Listar = dt

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                Dim vSQL As String = "SELECT ISNULL(Max(IdPiloto),0) FROM empresa_transporte_pilotos"
                Using lCommand As New SqlCommand(vSQL, lConnection)
                    lCommand.CommandType = CommandType.Text
                    lConnection.Open()
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CInt(lReturnValue) + 1
                    End If
                End Using
            End Using

            Return lMax

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function ExisteDPI(ByVal pDPI As String) As Boolean

        Try
            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM empresa_transporte_pilotos WHERE no_dpi=@no_dpi"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(vSQL, lConnection)

                    lCommand.CommandType = CommandType.Text
                    lCommand.Parameters.AddWithValue("@no_dpi", pDPI)

                    lConnection.Open()

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()

                    lConnection.Close()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lExists = CInt(lReturnValue) > 0
                    End If

                End Using

            End Using

            Return lExists

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function


    Public Shared Function Existe_No_Licencia(ByVal pNoLicencia As String) As Boolean

        Try
            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM empresa_transporte_pilotos WHERE no_licencia=@no_licencia"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        lCommand.Parameters.AddWithValue("@no_licencia", pNoLicencia)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lExists = CInt(lReturnValue) > 0
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lExists

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(lConnection As SqlConnection, lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdPiloto),0) FROM empresa_transporte_pilotos"

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue) + 1
                End If

            End Using

            Return lMax

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Listar_For_Despacho(ByVal pActivo As Boolean) As DataTable

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Listar_For_Despacho = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim sp As String = "SELECT IdEmpresaTransporte,IdPiloto, 
                                ISNULL(Nombres,'') + ' ' + ISNULL(Apellidos,'') AS Nombres,no_dpi AS 'No_DPI',
                                no_licencia AS 'No_Licencia',direccion AS Dirección 
                                FROM empresa_transporte_pilotos WHERE 1 > 0 "

            If pActivo Then
                sp += " AND Activo=1"
            Else
                sp += " AND Activo=0"
            End If

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Listar_For_Despacho = dt

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_All_By_IdEmpresaTransporte(ByVal pIdEmpresaTransporte As Integer,
                                                          ByVal pConnection As SqlConnection,
                                                          ByVal pTransaction As SqlTransaction) As DataTable

        Get_All_By_IdEmpresaTransporte = Nothing

        Try

            Dim sp As String = "SELECT IdPiloto, ISNULL(Nombres,'') + ' ' + ISNULL(Apellidos,'') AS Nombre   
                                FROM empresa_transporte_pilotos WHERE Activo = 1 AND IdEmpresaTransporte = @IdEmpresaTransporte"

            Dim cmd As New SqlCommand(sp, pConnection, pTransaction) With {.CommandType = CommandType.Text}
            cmd.Parameters.AddWithValue("@IdEmpresaTransporte", pIdEmpresaTransporte)
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Get_All_By_IdEmpresaTransporte = dt

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Function


End Class
