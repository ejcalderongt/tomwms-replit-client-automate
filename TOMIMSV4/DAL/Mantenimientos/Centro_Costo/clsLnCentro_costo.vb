Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnCentro_costo

    Public Shared Sub Cargar(ByRef oBeCentro_costo As clsBeCentro_costo, ByRef dr As DataRow)

        Try

            With oBeCentro_costo
                .IdCentroCosto = IIf(IsDBNull(dr.Item("IdCentroCosto")), 0, dr.Item("IdCentroCosto"))
                .IdEmpresa = IIf(IsDBNull(dr.Item("IdEmpresa")), 0, dr.Item("IdEmpresa"))
                .Codigo = IIf(IsDBNull(dr.Item("Codigo")), "", dr.Item("Codigo"))
                .Nombre = IIf(IsDBNull(dr.Item("Nombre")), "", dr.Item("Nombre"))
                .Referencia = IIf(IsDBNull(dr.Item("Referencia")), "", dr.Item("Referencia"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Control_Inventario = IIf(IsDBNull(dr.Item("Control_Inventario")), False, dr.Item("Control_Inventario"))
            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeCentro_costo As clsBeCentro_costo, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("centro_costo")
            Ins.Add("idcentrocosto", "@idcentrocosto", DataType.Parametro)
            Ins.Add("idempresa", "@idempresa", DataType.Parametro)
            Ins.Add("codigo", "@codigo", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("referencia", "@referencia", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("control_inventario", "@control_inventario", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCENTROCOSTO", oBeCentro_costo.IdCentroCosto))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeCentro_costo.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeCentro_costo.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeCentro_costo.Nombre))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIA", oBeCentro_costo.Referencia))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeCentro_costo.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeCentro_costo.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeCentro_costo.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeCentro_costo.User_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeCentro_costo.Activo))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_INVENTARIO", oBeCentro_costo.Control_Inventario))

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

    Public Shared Function Actualizar(ByRef oBeCentro_costo As clsBeCentro_costo, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("centro_costo")
            Upd.Add("idcentrocosto", "@idcentrocosto", DataType.Parametro)
            Upd.Add("idempresa", "@idempresa", DataType.Parametro)
            Upd.Add("codigo", "@codigo", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("referencia", "@referencia", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("control_inventario", "@control_inventario", DataType.Parametro)
            Upd.Where("IdCentroCosto = @IdCentroCosto")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCENTROCOSTO", oBeCentro_costo.IdCentroCosto))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeCentro_costo.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeCentro_costo.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeCentro_costo.Nombre))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIA", oBeCentro_costo.Referencia))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeCentro_costo.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeCentro_costo.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeCentro_costo.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeCentro_costo.User_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeCentro_costo.Activo))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_INVENTARIO", oBeCentro_costo.Control_Inventario))

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


    Public Shared Function Eliminar(ByRef oBeCentro_costo As clsBeCentro_costo, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Centro_costo " &
                                 "  Where(IdCentroCosto = @IdCentroCosto) "

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCENTROCOSTO", oBeCentro_costo.IdCentroCosto))

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

    Public Shared Function Listar() As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM Centro_costo"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
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
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_All() As List(Of clsBeCentro_costo)

        Dim lReturnList As New List(Of clsBeCentro_costo)

        Try

            Const sp As String = "SELECT * FROM Centro_costo "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeCentro_costo As New clsBeCentro_costo

                        For Each dr As DataRow In lDataTable.Rows
                            vBeCentro_costo = New clsBeCentro_costo()
                            Cargar(vBeCentro_costo, dr)
                            lReturnList.Add(vBeCentro_costo)
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

    Public Shared Sub GetSingle(ByRef pBeCentro_costo As clsBeCentro_costo)

        Try

            Const sp As String = "SELECT * FROM Centro_costo " &
            " Where(IdCentroCosto = @IdCentroCosto) "


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdCentroCosto", pBeCentro_costo.IdCentroCosto)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeCentro_costo As New clsBeCentro_costo

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(pBeCentro_costo, lDataTable.Rows(0))
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

    Public Shared Function GetSingle(ByVal pIdCentroCosto As Integer,
                                     ByVal lConnection As SqlConnection,
                                     ByVal lTransaction As SqlTransaction) As clsBeCentro_costo

        GetSingle = Nothing

        Try

            Const sp As String = "SELECT * FROM Centro_costo
                                  Where(IdCentroCosto = @IdCentroCosto)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdCentroCosto", pIdCentroCosto))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeCentroCosto As New clsBeCentro_costo
                Cargar(pBeCentroCosto, dt.Rows(0))
                Return pBeCentroCosto
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdCentroCosto),0) FROM Centro_costo"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

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

    Public Shared Function MaxID(ByRef lConnection As SqlConnection,
                                 ByVal lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdCentroCosto),0) FROM Centro_costo "

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

    Public Shared Function Exist_By_Codigo(ByVal pCodigo As String) As Boolean

        Exist_By_Codigo = False

        Try

            Const sp As String = "SELECT * FROM Centro_costo Where(codigo = @codigo)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@codigo", pCodigo)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Exist_By_Codigo = True
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

    Public Shared Function Existe_By_Codigo(ByVal pCodigo As String,
                                            ByVal lConnection As SqlConnection,
                                            ByVal lTransaction As SqlTransaction) As clsBeCentro_costo

        Existe_By_Codigo = Nothing

        Try

            Const sp As String = "SELECT * FROM Centro_costo Where(codigo = @codigo) "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@codigo", pCodigo)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeCentro_costo As New clsBeCentro_costo

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Cargar(vBeCentro_costo, lDataTable.Rows(0))
                    Existe_By_Codigo = vBeCentro_costo
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Sub Guardar_Transaccion(ByVal pBeCentroCosto As clsBeCentro_costo)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            If pBeCentroCosto.IdCentroCosto = 0 Then
                pBeCentroCosto.IdCentroCosto = MaxID(lConnection, lTransaction) + 1
                Insertar(pBeCentroCosto, lConnection, lTransaction)
            Else
                Actualizar(pBeCentroCosto, lConnection, lTransaction)
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Sub

    Public Shared Function Get_All_By_IdEmpresa(ByVal pIdEmpresa As Integer) As List(Of clsBeCentro_costo)

        Dim lReturnList As New List(Of clsBeCentro_costo)

        Try

            Const sp As String = "SELECT * FROM Centro_costo WHERE IdEmpresa = @IdEmpresa"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdEmpresa", pIdEmpresa)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeCentro_costo As New clsBeCentro_costo

                        For Each dr As DataRow In lDataTable.Rows
                            vBeCentro_costo = New clsBeCentro_costo()
                            Cargar(vBeCentro_costo, dr)
                            lReturnList.Add(vBeCentro_costo)
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

    Public Shared Function Get_All_By_IdEmpresa_DT(ByVal pIdEmpresa As Integer) As DataTable

        Get_All_By_IdEmpresa_DT = Nothing

        Try

            Const sp As String = "SELECT IdCentroCosto, Codigo + ' ' + Nombre as Nombre FROM Centro_costo WHERE IdEmpresa = @IdEmpresa"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdEmpresa", pIdEmpresa)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Get_All_By_IdEmpresa_DT = lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_By_Codigo(ByVal pCodigo As String) As Boolean

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Existe_By_Codigo = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM Centro_costo Where(codigo = @codigo) "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@codigo", pCodigo)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Existe_By_Codigo = (lDataTable.Rows.Count > 0)

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Codigo_By_IdCentroCosto(ByVal IdCentroCosto) As String

        Get_Codigo_By_IdCentroCosto = ""

        Try

            Const sp As String = "SELECT codigo FROM Centro_costo " &
            " Where(IdCentroCosto = @IdCentroCosto) "


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdCentroCosto", IdCentroCosto)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeCentro_costo As New clsBeCentro_costo

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Dim dr As DataRow = lDataTable.Rows(0)
                            Get_Codigo_By_IdCentroCosto = IIf(IsDBNull(dr.Item("codigo")), "", dr.Item("codigo"))
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

    Public Shared Function Get_All_By_IdEmpresa_DT_For_Inv(ByVal pIdEmpresa As Integer) As DataTable

        Get_All_By_IdEmpresa_DT_For_Inv = Nothing

        Try

            Const sp As String = "SELECT IdCentroCosto, Codigo + ' ' + Nombre as Nombre 
								  FROM Centro_costo WHERE IdEmpresa = @IdEmpresa AND Activo = 1 AND control_inventario = 1 "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdEmpresa", pIdEmpresa)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Get_All_By_IdEmpresa_DT_For_Inv = lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdEmpresa_DT_For_Inv(ByVal pIdEmpresa As Integer, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As DataTable

        Get_All_By_IdEmpresa_DT_For_Inv = Nothing

        Try

            Const sp As String = "SELECT IdCentroCosto, Codigo + ' ' + Nombre as Nombre 
								  FROM Centro_costo WHERE IdEmpresa = @IdEmpresa AND Activo = 1 AND control_inventario = 1 "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdEmpresa", pIdEmpresa)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Get_All_By_IdEmpresa_DT_For_Inv = lDataTable

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Codigo_By_IdCentroCosto(ByVal IdCentroCosto As Integer, lConnection As SqlConnection, lTransaction As SqlTransaction) As String

        Get_Codigo_By_IdCentroCosto = ""

        Try

            Const sp As String = "SELECT codigo FROM Centro_costo " &
            " Where(IdCentroCosto = @IdCentroCosto) "


            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdCentroCosto", IdCentroCosto)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeCentro_costo As New clsBeCentro_costo

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Dim dr As DataRow = lDataTable.Rows(0)
                    Get_Codigo_By_IdCentroCosto = IIf(IsDBNull(dr.Item("codigo")), "", dr.Item("codigo"))
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
