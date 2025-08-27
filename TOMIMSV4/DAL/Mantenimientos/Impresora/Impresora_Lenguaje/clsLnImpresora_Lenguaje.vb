Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnImpresora_Lenguaje

    Public Shared Sub Cargar(ByRef oBeImpresora_Lenguaje As clsBeImpresora_Lenguaje, ByRef dr As DataRow)
        Try
            With oBeImpresora_Lenguaje
                .IdImpresoraLenguaje = IIf(IsDBNull(dr.Item("IdImpresoraLenguaje")), 0, dr.Item("IdImpresoraLenguaje"))
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

    Public Shared Function Insertar(ByRef oBeImpresora_Lenguaje As clsBeImpresora_Lenguaje, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("impresora_lenguaje")
            Ins.Add("idimpresoralenguaje", "@idimpresoralenguaje", DataType.Parametro)
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

            cmd.Parameters.Add(New SqlParameter("@IDIMPRESORALENGUAJE", oBeImpresora_Lenguaje.IdImpresoraLenguaje))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeImpresora_Lenguaje.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeImpresora_Lenguaje.Nombre))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeImpresora_Lenguaje.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeImpresora_Lenguaje.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeImpresora_Lenguaje.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeImpresora_Lenguaje.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeImpresora_Lenguaje.Activo))

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

    Public Shared Function Actualizar(ByRef oBeImpresora_Lenguaje As clsBeImpresora_Lenguaje, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("impresora_lenguaje")
            Upd.Add("idimpresoralenguaje", "@idimpresoralenguaje", DataType.Parametro)
            Upd.Add("codigo", "@codigo", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("IdImpresoraLenguaje = @IdImpresoraLenguaje")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDIMPRESORALENGUAJE", oBeImpresora_Lenguaje.IdImpresoraLenguaje))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeImpresora_Lenguaje.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeImpresora_Lenguaje.Nombre))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeImpresora_Lenguaje.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeImpresora_Lenguaje.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeImpresora_Lenguaje.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeImpresora_Lenguaje.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeImpresora_Lenguaje.Activo))

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


    Public Shared Function Eliminar(ByRef oBeImpresora_Lenguaje As clsBeImpresora_Lenguaje, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Impresora_Lenguaje" & _
             "  Where(IdImpresoraLenguaje = @IdImpresoraLenguaje)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDIMPRESORALENGUAJE", oBeImpresora_Lenguaje.IdImpresoraLenguaje))

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

            Const sp As String = "SELECT * FROM Impresora_Lenguaje"
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

    Public Shared Function Get_All() As List(Of clsBeImpresora_Lenguaje)

        Dim lReturnList As New List(Of clsBeImpresora_Lenguaje)

        Try

            Const sp As String = "SELECT * FROM Impresora_Lenguaje"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeImpresora_Lenguaje As New clsBeImpresora_Lenguaje

                        For Each dr As DataRow In lDataTable.Rows
                            vBeImpresora_Lenguaje = New clsBeImpresora_Lenguaje()
                            Cargar(vBeImpresora_Lenguaje, dr)
                            lReturnList.Add(vBeImpresora_Lenguaje)
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

    Public Shared Sub GetSingle(ByRef pBeImpresora_Lenguaje As clsBeImpresora_Lenguaje)

        Try

            Const sp As String = "SELECT * FROM Impresora_Lenguaje" & _
            " Where(IdImpresoraLenguaje = @IdImpresoraLenguaje)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeImpresora_Lenguaje As New clsBeImpresora_Lenguaje

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeImpresora_Lenguaje, lDataTable.Rows(0))
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

            Const sp As String = "SELECT ISNULL(Max(IdImpresoraLenguaje),0) FROM Impresora_Lenguaje"

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
