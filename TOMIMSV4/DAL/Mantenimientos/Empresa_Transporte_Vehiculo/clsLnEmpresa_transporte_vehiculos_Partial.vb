Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnEmpresa_transporte_vehiculos

    Public Shared Function GetNombre(ByVal pIdVehiculo As Integer) As clsBeEmpresa_transporte_vehiculos

        GetNombre = Nothing

        Try

            Using lCnn As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT TOP 1 * FROM empresa_transporte_vehiculos WHERE IdVehiculo=@IdVehiculo"

                Using lDTA As New SqlDataAdapter(vSQL, lCnn)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdVehiculo", pIdVehiculo)

                    Dim lDT As New DataTable()
                    lDTA.Fill(lDT)

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        Dim lRow As DataRow = lDT.Rows(0)
                        Dim Obj As New clsBeEmpresa_transporte_vehiculos()

                        Obj.IdVehiculo = CType(lRow("IdVehiculo"), Integer)

                        If lRow("Marca") IsNot DBNull.Value AndAlso lRow("Marca") IsNot Nothing Then
                            Obj.Marca = CType(lRow("Marca"), String)
                        End If
                        If lRow("Modelo") IsNot DBNull.Value AndAlso lRow("Modelo") IsNot Nothing Then
                            Obj.Modelo = CType(lRow("Modelo"), String)
                        End If
                        If lRow("Tipo") IsNot DBNull.Value AndAlso lRow("Tipo") IsNot Nothing Then
                            Obj.Tipo = CType(lRow("Tipo"), String)
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
                            Obj.Fec_mod = CType(lRow("fec_mod"), Date)
                        End If

                        If lRow("placa") IsNot DBNull.Value AndAlso lRow("placa") IsNot Nothing Then
                            Obj.Placa = CType(lRow("placa"), String)
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

        Try

            Dim sp As String = "SELECT IdEmpresaTransporte,IdVehiculo AS Código, 
                                Placa,Marca,Modelo,Tipo 
                                FROM empresa_transporte_vehiculos WHERE 1 > 0 "

            If pActivo Then
                sp += " AND Activo=1"
            Else
                sp += " AND Activo=0"
            End If


            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function


    Public Shared Function MaxID() As Integer

        Dim lMax As Integer = 0

        Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim vSQL As String = "SELECT ISNULL(Max(IdVehiculo),0) FROM empresa_transporte_vehiculos"

            Using lCommand As New SqlCommand(vSQL, lConnection)
                lCommand.CommandType = CommandType.Text
                lConnection.Open()
                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                lConnection.Close()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue) + 1
                Else
                    lMax = 1
                End If
            End Using
        End Using

        Return lMax

    End Function

    Public Shared Function Existe_Placa(ByVal pPlaca As String) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM empresa_transporte_vehiculos WHERE Placa=@Placa"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        lCommand.Parameters.AddWithValue("@Placa", pPlaca)

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

    Public Shared Function MaxID(lconnection As SqlConnection, ltransaction As SqlTransaction) As Integer

        Dim lMax As Integer = 0

        Dim vSQL As String = "SELECT ISNULL(Max(IdVehiculo),0) FROM empresa_transporte_vehiculos"

        Using lCommand As New SqlCommand(vSQL, lconnection, ltransaction) With {.CommandType = CommandType.Text}

            lCommand.CommandType = CommandType.Text

            Dim lReturnValue As Object = lCommand.ExecuteScalar()

            If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                lMax = CInt(lReturnValue) + 1
            Else
                lMax = 1
            End If

        End Using

        Return lMax

    End Function

    Public Shared Function Listar_For_Despacho(ByVal pActivo As Boolean) As DataTable

        Listar_For_Despacho = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim sp As String = "SELECT IdEmpresaTransporte,IdVehiculo, 
                                Placa,Marca,Modelo,Tipo 
                                FROM empresa_transporte_vehiculos WHERE 1 > 0 "

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

End Class
