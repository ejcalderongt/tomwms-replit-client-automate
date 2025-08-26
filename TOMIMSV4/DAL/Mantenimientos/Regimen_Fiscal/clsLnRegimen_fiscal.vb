Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnRegimen_fiscal

    Public Shared Sub Cargar(ByRef oBeRegimen_fiscal As clsBeRegimen_fiscal, ByRef dr As DataRow)
        Try
            With oBeRegimen_fiscal
                .IdRegimen = IIf(IsDBNull(dr.Item("IdRegimen")), 0, dr.Item("IdRegimen"))
                .Codigo_regimen = IIf(IsDBNull(dr.Item("codigo_regimen")), "", dr.Item("codigo_regimen"))
                .Descripcion = IIf(IsDBNull(dr.Item("descripcion")), "", dr.Item("descripcion"))
                .Dias_vencimiento = IIf(IsDBNull(dr.Item("dias_vencimiento")), 0, dr.Item("dias_vencimiento"))
            End With
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeRegimen_fiscal As clsBeRegimen_fiscal, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("regimen_fiscal")
            Ins.Add("idregimen", "@idregimen", DataType.Parametro)
            Ins.Add("codigo_regimen", "@codigo_regimen", DataType.Parametro)
            Ins.Add("descripcion", "@descripcion", DataType.Parametro)
            Ins.Add("dias_vencimiento", "@dias_vencimiento", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDREGIMEN", oBeRegimen_fiscal.IdRegimen))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_REGIMEN", oBeRegimen_fiscal.Codigo_regimen))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeRegimen_fiscal.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@DIAS_VENCIMIENTO", oBeRegimen_fiscal.Dias_vencimiento))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeRegimen_fiscal As clsBeRegimen_fiscal, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("regimen_fiscal")
            Upd.Add("idregimen", "@idregimen", DataType.Parametro)
            Upd.Add("codigo_regimen", "@codigo_regimen", DataType.Parametro)
            Upd.Add("descripcion", "@descripcion", DataType.Parametro)
            Upd.Add("dias_vencimiento", "@dias_vencimiento", DataType.Parametro)
            Upd.Where("IdRegimen = @IdRegimen")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDREGIMEN", oBeRegimen_fiscal.IdRegimen))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_REGIMEN", oBeRegimen_fiscal.Codigo_regimen))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeRegimen_fiscal.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@DIAS_VENCIMIENTO", oBeRegimen_fiscal.Dias_vencimiento))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function


    Public Shared Function Eliminar(ByRef oBeRegimen_fiscal As clsBeRegimen_fiscal, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Regimen_fiscal" &
             "  Where(IdRegimen = @IdRegimen)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDREGIMEN", oBeRegimen_fiscal.IdRegimen))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM Regimen_fiscal"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_All() As List(Of clsBeRegimen_fiscal)

        Dim lReturnList As New List(Of clsBeRegimen_fiscal)

        Try

            Const sp As String = "SELECT * FROM Regimen_fiscal "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeRegimen_fiscal As New clsBeRegimen_fiscal

                        For Each dr As DataRow In lDataTable.Rows
                            vBeRegimen_fiscal = New clsBeRegimen_fiscal()
                            Cargar(vBeRegimen_fiscal, dr)
                            lReturnList.Add(vBeRegimen_fiscal)
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_For_Combo() As DataTable

        Dim lReturnList As New List(Of clsBeRegimen_fiscal)

        Try

            Const sp As String = "SELECT IdRegimen,codigo_regimen,descripcion FROM Regimen_fiscal "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)
                        Return lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Sub GetSingle(ByRef pBeRegimen_fiscal As clsBeRegimen_fiscal)

        Try

            Const sp As String = "SELECT * FROM Regimen_fiscal" &
            " Where(IdRegimen = @IdRegimen)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdRegimen", pBeRegimen_fiscal.IdRegimen)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeRegimen_fiscal As New clsBeRegimen_fiscal

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeRegimen_fiscal, lDataTable.Rows(0))
                            pBeRegimen_fiscal = vBeRegimen_fiscal
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub


    Public Shared Function GetSingle_By_IdRegimen(ByVal IdRegimen As Integer) As clsBeRegimen_fiscal


        GetSingle_By_IdRegimen = Nothing

        Dim Evaluar As Boolean = True

        Try

            Dim vSQL As String = ""

            If IdRegimen <> 0 Then

                vSQL = "SELECT * FROM Regimen_fiscal Where(IdRegimen = @IdRegimen)"

                Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                    lConnection.Open()

                    Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                        Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                            lDTA.SelectCommand.CommandType = CommandType.Text
                            lDTA.SelectCommand.Transaction = lTransaction
                            lDTA.SelectCommand.Parameters.AddWithValue("@IdRegimen", IdRegimen)

                            Dim lDT As New DataTable()
                            lDTA.Fill(lDT)

                            If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                                Dim lRow As DataRow = lDT.Rows(0)
                                Dim Obj As New clsBeRegimen_fiscal()
                                Cargar(Obj, lRow)
                                GetSingle_By_IdRegimen = Obj

                            End If

                        End Using

                        lTransaction.Commit()

                    End Using

                    lConnection.Close()

                End Using

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle_By_IdRegimen(ByVal IdRegimen As Integer,
                                                  ByVal lConnection As SqlConnection,
                                                  ByVal lTransaction As SqlTransaction) As clsBeRegimen_fiscal


        GetSingle_By_IdRegimen = Nothing

        Dim Evaluar As Boolean = True

        Try

            Dim vSQL As String = ""

            If IdRegimen <> 0 Then

                vSQL = "SELECT * FROM Regimen_fiscal Where(IdRegimen = @IdRegimen)"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Transaction = lTransaction
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdRegimen", IdRegimen)

                    Dim lDT As New DataTable()
                    lDTA.Fill(lDT)

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        Dim lRow As DataRow = lDT.Rows(0)
                        Dim Obj As New clsBeRegimen_fiscal()
                        Cargar(Obj, lRow)
                        GetSingle_By_IdRegimen = Obj

                    End If

                End Using

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle_By_Codigo_Regimen(ByVal Codigo As String) As clsBeRegimen_fiscal


        GetSingle_By_Codigo_Regimen = Nothing

        Dim Evaluar As Boolean = True

        Try

            Dim vSQL As String = ""

            If Codigo IsNot Nothing Then

                vSQL = "SELECT * FROM Regimen_fiscal Where(codigo_regimen = @codigo_regimen)"

                Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                    lConnection.Open()

                    Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                        Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                            lDTA.SelectCommand.CommandType = CommandType.Text
                            lDTA.SelectCommand.Transaction = lTransaction
                            lDTA.SelectCommand.Parameters.AddWithValue("@codigo_regimen", Codigo)

                            Dim lDT As New DataTable()
                            lDTA.Fill(lDT)

                            If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                                Dim lRow As DataRow = lDT.Rows(0)
                                Dim Obj As New clsBeRegimen_fiscal()
                                Cargar(Obj, lRow)
                                GetSingle_By_Codigo_Regimen = Obj

                            End If

                        End Using

                        lTransaction.Commit()

                    End Using

                    lConnection.Close()

                End Using

            End If


        Catch ex As Exception
            Throw ex
        End Try

    End Function

    'Public Shared Function GetSingle_By_Codigo_Regimen(ByVal Codigo As String,
    '                                                   ByVal lConnection As SqlConnection,
    '                                                   ByVal lTransaction As SqlTransaction) As clsBeRegimen_fiscal


    '    GetSingle_By_Codigo_Regimen = Nothing

    '    Dim Evaluar As Boolean = True

    '    Try

    '        Dim vSQL As String = ""

    '        If Codigo IsNot Nothing Then

    '            vSQL = "SELECT * FROM Regimen_fiscal Where(codigo_regimen = @codigo_regimen)"

    '            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

    '                lDTA.SelectCommand.CommandType = CommandType.Text
    '                lDTA.SelectCommand.Transaction = lTransaction
    '                lDTA.SelectCommand.Parameters.AddWithValue("@codigo_regimen", Codigo)

    '                Dim lDT As New DataTable()
    '                lDTA.Fill(lDT)

    '                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

    '                    Dim lRow As DataRow = lDT.Rows(0)
    '                    Dim Obj As New clsBeRegimen_fiscal()
    '                    Cargar(Obj, lRow)
    '                    GetSingle_By_Codigo_Regimen = Obj

    '                End If

    '            End Using

    '        End If

    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Function

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdRegimen),0) FROM Regimen_fiscal"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()
                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lMax = CInt(lReturnValue) + 1
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lMax

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_For_Combo(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As DataTable

        Dim lReturnList As New List(Of clsBeRegimen_fiscal)

        Try

            Const sp As String = "SELECT IdRegimen,codigo_regimen,descripcion FROM Regimen_fiscal "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)
                Return lDataTable

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle_By_Codigo_Regimen(ByVal Codigo As String,
                                                       ByVal lConnection As SqlConnection,
                                                       ByVal lTransaction As SqlTransaction) As clsBeRegimen_fiscal


        GetSingle_By_Codigo_Regimen = Nothing

        Dim Evaluar As Boolean = True

        Try

            Dim vSQL As String = ""

            If Codigo IsNot Nothing Then

                vSQL = "SELECT * FROM Regimen_fiscal Where(codigo_regimen = @codigo_regimen)"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Transaction = lTransaction
                    lDTA.SelectCommand.Parameters.AddWithValue("@codigo_regimen", Codigo)

                    Dim lDT As New DataTable()
                    lDTA.Fill(lDT)

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        Dim lRow As DataRow = lDT.Rows(0)
                        Dim Obj As New clsBeRegimen_fiscal()
                        Cargar(Obj, lRow)
                        GetSingle_By_Codigo_Regimen = Obj

                    End If

                End Using

            End If


        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class