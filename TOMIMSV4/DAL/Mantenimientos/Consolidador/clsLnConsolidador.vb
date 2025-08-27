Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnConsolidador

    Public Shared Sub Cargar(ByRef oBeConsolidador As clsBeConsolidador, ByRef dr As DataRow)
        Try
            With oBeConsolidador
                .Idconsolidador = IIf(IsDBNull(dr.Item("Idconsolidador")), 0, dr.Item("Idconsolidador"))
                .Codigo = IIf(IsDBNull(dr.Item("codigo")), "", dr.Item("codigo"))
                .IdEmpresa = IIf(IsDBNull(dr.Item("IdEmpresa")), 0, dr.Item("IdEmpresa"))
                .Nom_comercial = IIf(IsDBNull(dr.Item("nom_comercial")), "", dr.Item("nom_comercial"))
                .Razon_social = IIf(IsDBNull(dr.Item("razon_social")), "", dr.Item("razon_social"))
                .Nit = IIf(IsDBNull(dr.Item("nit")), "", dr.Item("nit"))
                .Telefono = IIf(IsDBNull(dr.Item("telefono")), "", dr.Item("telefono"))
                .Direccion = IIf(IsDBNull(dr.Item("direccion")), "", dr.Item("direccion"))
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


    Public Shared Sub Guardar_Transaccion(ByVal pBeConsolidador As clsBeConsolidador)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            If pBeConsolidador.Idconsolidador = 0 Then
                pBeConsolidador.Idconsolidador = MaxID(lConnection, lTransaction) + 1
                Insertar(pBeConsolidador, lConnection, lTransaction)
            Else
                Actualizar(pBeConsolidador, lConnection, lTransaction)
            End If


            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeConsolidador As clsBeConsolidador, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("consolidador")
            Ins.Add("idconsolidador", "@idconsolidador", DataType.Parametro)
            Ins.Add("codigo", "@codigo", DataType.Parametro)
            Ins.Add("idempresa", "@idempresa", DataType.Parametro)
            Ins.Add("nom_comercial", "@nom_comercial", DataType.Parametro)
            Ins.Add("razon_social", "@razon_social", DataType.Parametro)
            Ins.Add("nit", "@nit", DataType.Parametro)
            Ins.Add("telefono", "@telefono", DataType.Parametro)
            Ins.Add("direccion", "@direccion", DataType.Parametro)
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
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCONSOLIDADOR", oBeConsolidador.Idconsolidador))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeConsolidador.Codigo))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeConsolidador.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@NOM_COMERCIAL", oBeConsolidador.Nom_comercial))
            cmd.Parameters.Add(New SqlParameter("@RAZON_SOCIAL", oBeConsolidador.Razon_social))
            cmd.Parameters.Add(New SqlParameter("@NIT", oBeConsolidador.Nit))
            cmd.Parameters.Add(New SqlParameter("@TELEFONO", oBeConsolidador.Telefono))
            cmd.Parameters.Add(New SqlParameter("@DIRECCION", oBeConsolidador.Direccion))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeConsolidador.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeConsolidador.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeConsolidador.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeConsolidador.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeConsolidador.Activo))

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

    Public Shared Function Actualizar(ByRef oBeConsolidador As clsBeConsolidador, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("consolidador")
            Upd.Add("idconsolidador", "@idconsolidador", DataType.Parametro)
            Upd.Add("codigo", "@codigo", DataType.Parametro)
            Upd.Add("idempresa", "@idempresa", DataType.Parametro)
            Upd.Add("nom_comercial", "@nom_comercial", DataType.Parametro)
            Upd.Add("razon_social", "@razon_social", DataType.Parametro)
            Upd.Add("nit", "@nit", DataType.Parametro)
            Upd.Add("telefono", "@telefono", DataType.Parametro)
            Upd.Add("direccion", "@direccion", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("Idconsolidador = @Idconsolidador")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCONSOLIDADOR", oBeConsolidador.Idconsolidador))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeConsolidador.Codigo))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeConsolidador.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@NOM_COMERCIAL", oBeConsolidador.Nom_comercial))
            cmd.Parameters.Add(New SqlParameter("@RAZON_SOCIAL", oBeConsolidador.Razon_social))
            cmd.Parameters.Add(New SqlParameter("@NIT", oBeConsolidador.Nit))
            cmd.Parameters.Add(New SqlParameter("@TELEFONO", oBeConsolidador.Telefono))
            cmd.Parameters.Add(New SqlParameter("@DIRECCION", oBeConsolidador.Direccion))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeConsolidador.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeConsolidador.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeConsolidador.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeConsolidador.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeConsolidador.Activo))

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


    Public Shared Function Eliminar(ByRef oBeConsolidador As clsBeConsolidador, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Consolidador" & _
             "  Where(Idconsolidador = @Idconsolidador)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCONSOLIDADOR", oBeConsolidador.Idconsolidador))

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

            Const sp As String = "SELECT * FROM Consolidador"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
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

    Public Shared Function Get_All() As List(Of clsBeConsolidador)

        Dim lReturnList As New List(Of clsBeConsolidador)

        Try

            Const sp As String = "SELECT * FROM Consolidador"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeConsolidador As New clsBeConsolidador

                        For Each dr As DataRow In lDataTable.Rows
                            vBeConsolidador = New clsBeConsolidador()
                            Cargar(vBeConsolidador, dr)
                            lReturnList.Add(vBeConsolidador)
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


    Public Shared Function Get_All_Filtro_Seleccion(ByVal pActivo As Boolean) As DataTable


        Try

            Using lCnn As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim lSQl As String = "
				select
				Idconsolidador as Correlativo,
				codigo,
				IdEmpresa as Empresa,
				nom_comercial as Nombre,
				razon_social as Descripcion,
				direccion as Direccion,
				activo as Activo
				from consolidador"

                If pActivo Then
                    lSQl += " where Activo=1"
                Else
                    lSQl += " where Activo=0"
                End If


                lCnn.Open()

                Using lTransaction As SqlTransaction = lCnn.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(lSQl, lCnn)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Return lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lCnn.Close()

            End Using


        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub GetSingle(ByRef pBeConsolidador As clsBeConsolidador)

        Try

            Const sp As String = "SELECT * FROM Consolidador" &
                                " Where(Idconsolidador = @Idconsolidador)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeConsolidador As New clsBeConsolidador

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeConsolidador, lDataTable.Rows(0))
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

    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(Idconsolidador),0) FROM Consolidador"

            Using lCommand As New SqlCommand(sp, pConnection)

                lCommand.CommandType = CommandType.Text
                lCommand.Transaction = pTransaction

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

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(Idconsolidador),0) FROM Consolidador"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
                Using lCommand As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CInt(lReturnValue) + 1
                    End If
                End Using
            End Using

            Return lMax

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe(ByVal pCodigo As String, ByRef Cnn As SqlConnection, ByRef pTransaction As SqlTransaction) As clsBeConsolidador

        Existe = Nothing

        Try

            Const vSQL As String = "SELECT * FROM Consolidador" &
                                " Where(Idconsolidador = @Idconsolidador)"


            Using lDTA As New SqlDataAdapter(vSQL, Cnn)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@Idconsolidador", pCodigo)
                lDTA.SelectCommand.Transaction = pTransaction

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                        Dim lRow As DataRow = lDataTable.Rows(0)
                        Dim vBeConsolidador As New clsBeConsolidador
                        Cargar(vBeConsolidador, lRow)
                        Existe = vBeConsolidador
                    End If

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
