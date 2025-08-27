Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnOperador_jornada_laboral

    Public Shared Sub Cargar(ByRef oBeOperador_jornada_laboral As clsBeOperador_jornada_laboral, ByRef dr As DataRow)
        Try
            With oBeOperador_jornada_laboral
                .IdOperadorJornadaLaboral = IIf(IsDBNull(dr.Item("IdOperadorJornadaLaboral")), 0, dr.Item("IdOperadorJornadaLaboral"))
                .IdOperador = IIf(IsDBNull(dr.Item("IdOperador")), 0, dr.Item("IdOperador"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .IdJornada = IIf(IsDBNull(dr.Item("IdJornada")), 0, dr.Item("IdJornada"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeOperador_jornada_laboral As clsBeOperador_jornada_laboral, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("operador_jornada_laboral")
            Ins.Add("idoperadorjornadalaboral", "@idoperadorjornadalaboral", DataType.Parametro)
            Ins.Add("idoperador", "@idoperador", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("idjornada", "@idjornada", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction()
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDOPERADORJORNADALABORAL", oBeOperador_jornada_laboral.IdOperadorJornadaLaboral))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeOperador_jornada_laboral.IdOperador))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeOperador_jornada_laboral.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDJORNADA", oBeOperador_jornada_laboral.IdJornada))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeOperador_jornada_laboral.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeOperador_jornada_laboral.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeOperador_jornada_laboral.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeOperador_jornada_laboral.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeOperador_jornada_laboral.Fec_mod))

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

    Public Shared Function Actualizar(ByRef oBeOperador_jornada_laboral As clsBeOperador_jornada_laboral, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("operador_jornada_laboral")
            Upd.Add("idoperadorjornadalaboral", "@idoperadorjornadalaboral", DataType.Parametro)
            Upd.Add("idoperador", "@idoperador", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("idjornada", "@idjornada", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("IdOperadorJornadaLaboral = @IdOperadorJornadaLaboral")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction()
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDOPERADORJORNADALABORAL", oBeOperador_jornada_laboral.IdOperadorJornadaLaboral))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeOperador_jornada_laboral.IdOperador))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeOperador_jornada_laboral.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDJORNADA", oBeOperador_jornada_laboral.IdJornada))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeOperador_jornada_laboral.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeOperador_jornada_laboral.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeOperador_jornada_laboral.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeOperador_jornada_laboral.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeOperador_jornada_laboral.Fec_mod))

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


    Public Shared Function Eliminar(ByRef oBeOperador_jornada_laboral As clsBeOperador_jornada_laboral, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Operador_jornada_laboral" & _
             "  Where(IdOperadorJornadaLaboral = @IdOperadorJornadaLaboral)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction()
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDOPERADORJORNADALABORAL", oBeOperador_jornada_laboral.IdOperadorJornadaLaboral))

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

            Const sp As String = "SELECT * FROM Operador_jornada_laboral"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction()
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

    Public Shared Function GetAll() As List(Of clsBeOperador_jornada_laboral)

        Dim lReturnList As New List(Of clsBeOperador_jornada_laboral)

        Try

            Const sp As String = "SELECT * FROM Operador_jornada_laboral"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction()

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeOperador_jornada_laboral As New clsBeOperador_jornada_laboral

                        For Each dr As DataRow In lDataTable.Rows
                            vBeOperador_jornada_laboral = New clsBeOperador_jornada_laboral()
                            Cargar(vBeOperador_jornada_laboral, dr)
                            lReturnList.Add(vBeOperador_jornada_laboral)
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

    Public Shared Sub GetSingle(ByRef pBeOperador_jornada_laboral As clsBeOperador_jornada_laboral)

        Try

            Const sp As String = "SELECT * FROM Operador_jornada_laboral" & _
            " Where(IdOperadorJornadaLaboral = @IdOperadorJornadaLaboral)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction()

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeOperador_jornada_laboral As New clsBeOperador_jornada_laboral

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeOperador_jornada_laboral, lDataTable.Rows(0))
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

            Const sp As String = "SELECT ISNULL(Max(IdOperadorJornadaLaboral),0) FROM Operador_jornada_laboral"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction()

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


    Public Shared Function MaxID(ByRef lConnection As SqlConnection, ByRef lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdOperadorJornadaLaboral),0) FROM Operador_jornada_laboral"

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

    Public Shared Function Existe_Jornada_Laboral(ByRef pBeOperador_jornada_laboral As clsBeOperador_jornada_laboral) As Boolean

        Existe_Jornada_Laboral = False

        Try

            Const sp As String = "SELECT * FROM Operador_jornada_laboral" &
            " Where (IdOperador = @IdOperador and  IdBodega=@IdBodega and IdJornada=@IdJornada )"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction()

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IDOPERADOR", pBeOperador_jornada_laboral.IdOperador))
                        lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", pBeOperador_jornada_laboral.IdBodega))
                        lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IDJORNADA", pBeOperador_jornada_laboral.IdJornada))

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeOperador_jornada_laboral As New clsBeOperador_jornada_laboral

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            'Cargar(vBeOperador_jornada_laboral, lDataTable.Rows(0))
                            Existe_Jornada_Laboral = True
                        End If

                    End Using

                    lTransaction.Commit()

                    Return Existe_Jornada_Laboral
                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdOperador(ByRef pOperador As Integer) As List(Of clsBeOperador_jornada_laboral)

        Dim lReturnList As New List(Of clsBeOperador_jornada_laboral)

        Try

            Const sp As String = "SELECT * FROM Operador_jornada_laboral
								 WHERE IdOperador = @IdOperador"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction()

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IDOPERADOR", pOperador))

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeOperador_jornada_laboral As New clsBeOperador_jornada_laboral

                        For Each dr As DataRow In lDataTable.Rows
                            vBeOperador_jornada_laboral = New clsBeOperador_jornada_laboral()
                            Cargar(vBeOperador_jornada_laboral, dr)
                            lReturnList.Add(vBeOperador_jornada_laboral)
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
End Class
