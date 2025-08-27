Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnJornada_sistema

    Public Shared Sub Cargar(ByRef oBeJornada_sistema As clsBeJornada_sistema, ByRef dr As DataRow)
        Try
            With oBeJornada_sistema
                .IdJornada = IIf(IsDBNull(dr.Item("IdJornada")), 0, dr.Item("IdJornada"))
                .IdEmpresa = IIf(IsDBNull(dr.Item("IdEmpresa")), 0, dr.Item("IdEmpresa"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .Fecha = IIf(IsDBNull(dr.Item("Fecha")), New Date(1900, 1, 1), dr.Item("Fecha"))
                .IdUsuario = IIf(IsDBNull(dr.Item("IdUsuario")), 0, dr.Item("IdUsuario"))
                .FechaAgregado = IIf(IsDBNull(dr.Item("FechaAgregado")), Date.Now, dr.Item("FechaAgregado"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeJornada_sistema As clsBeJornada_sistema, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("jornada_sistema")
            Ins.Add("idjornada", "@idjornada", DataType.Parametro)
            Ins.Add("idempresa", "@idempresa", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("fecha", "@fecha", DataType.Parametro)
            Ins.Add("idusuario", "@idusuario", DataType.Parametro)
            Ins.Add("fechaagregado", "@fechaagregado", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDJORNADA", oBeJornada_sistema.IdJornada))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeJornada_sistema.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeJornada_sistema.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@FECHA", oBeJornada_sistema.Fecha))
            cmd.Parameters.Add(New SqlParameter("@IDUSUARIO", oBeJornada_sistema.IdUsuario))
            cmd.Parameters.Add(New SqlParameter("@FECHAAGREGADO", oBeJornada_sistema.FechaAgregado))

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

    Public Shared Function Actualizar(ByRef oBeJornada_sistema As clsBeJornada_sistema, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("jornada_sistema")
            Upd.Add("idjornada", "@idjornada", DataType.Parametro)
            Upd.Add("idempresa", "@idempresa", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("fecha", "@fecha", DataType.Parametro)
            Upd.Add("idusuario", "@idusuario", DataType.Parametro)
            Upd.Add("fechaagregado", "@fechaagregado", DataType.Parametro)
            Upd.Where("IdJornada = @IdJornada")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDJORNADA", oBeJornada_sistema.IdJornada))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeJornada_sistema.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeJornada_sistema.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@FECHA", oBeJornada_sistema.Fecha))
            cmd.Parameters.Add(New SqlParameter("@IDUSUARIO", oBeJornada_sistema.IdUsuario))
            cmd.Parameters.Add(New SqlParameter("@FECHAAGREGADO", oBeJornada_sistema.FechaAgregado))

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


    Public Shared Function Eliminar(ByRef oBeJornada_sistema As clsBeJornada_sistema, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Jornada_sistema" &
             "  Where(IdJornada = @IdJornada)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDJORNADA", oBeJornada_sistema.IdJornada))

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

            Const sp As String = "SELECT * FROM Jornada_sistema"
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

    Public Shared Function Get_All() As List(Of clsBeJornada_sistema)

        Dim lReturnList As New List(Of clsBeJornada_sistema)

        Try

            Const sp As String = "SELECT * FROM Jornada_sistema"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeJornada_sistema As New clsBeJornada_sistema

                        For Each dr As DataRow In lDataTable.Rows
                            vBeJornada_sistema = New clsBeJornada_sistema()
                            Cargar(vBeJornada_sistema, dr)
                            lReturnList.Add(vBeJornada_sistema)
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

    Public Shared Sub GetSingle(ByRef pBeJornada_sistema As clsBeJornada_sistema)

        Try

            Const sp As String = "SELECT * FROM Jornada_sistema" &
            " Where(IdJornada = @IdJornada)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeJornada_sistema As New clsBeJornada_sistema

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeJornada_sistema, lDataTable.Rows(0))
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

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdJornada),0) FROM Jornada_sistema"

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

    Public Shared Function Get_All_For_Grid(ByVal pIdEmpresa As Integer, ByVal pFechaDel As Date, ByVal pFechaAl As Date) As DataTable

        Get_All_For_Grid = Nothing

        Try

            Dim vSQL As String = "SELECT IdJornada, IdEmpresa, Fecha, FechaAgregado
										 FROM jornada_sistema WHERE 1 > 0 "

            vSQL += String.Format(" AND cast(Fecha AS DATE) BETWEEN {0} AND {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("IdEmpresa", pIdEmpresa)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Get_All_For_Grid = lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
