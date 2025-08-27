Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnOperador_montacarga

    Public Shared Sub Cargar(ByRef oBeOperador_montacarga As clsBeOperador_montacarga, ByRef dr As DataRow)
        Try
            With oBeOperador_montacarga
                .IdAsignacionMontacarga = IIf(IsDBNull(dr.Item("IdAsignacionMontacarga")), 0, dr.Item("IdAsignacionMontacarga"))
                .IdOperador = IIf(IsDBNull(dr.Item("IdOperador")), 0, dr.Item("IdOperador"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .IdMontacarga = IIf(IsDBNull(dr.Item("IdMontacarga")), 0, dr.Item("IdMontacarga"))
                .Fecha_Asignacion = IIf(IsDBNull(dr.Item("Fecha_Asignacion")), Date.Now, dr.Item("Fecha_Asignacion"))
                .Fecha_Inactivo = IIf(IsDBNull(dr.Item("Fecha_Inactivo")), Date.Now, dr.Item("Fecha_Inactivo"))
                .Activo = IIf(IsDBNull(dr.Item("Activo")), False, dr.Item("Activo"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeOperador_montacarga As clsBeOperador_montacarga, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("operador_montacarga")
            Ins.Add("idasignacionmontacarga", "@idasignacionmontacarga", DataType.Parametro)
            Ins.Add("idoperador", "@idoperador", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("idmontacarga", "@idmontacarga", DataType.Parametro)
            Ins.Add("fecha_asignacion", "@fecha_asignacion", DataType.Parametro)
            Ins.Add("fecha_inactivo", "@fecha_inactivo", DataType.Parametro)
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

            cmd.Parameters.Add(New SqlParameter("@IDASIGNACIONMONTACARGA", oBeOperador_montacarga.IdAsignacionMontacarga))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeOperador_montacarga.IdOperador))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeOperador_montacarga.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDMONTACARGA", oBeOperador_montacarga.IdMontacarga))
            cmd.Parameters.Add(New SqlParameter("@FECHA_ASIGNACION", oBeOperador_montacarga.Fecha_Asignacion))
            cmd.Parameters.Add(New SqlParameter("@FECHA_INACTIVO", oBeOperador_montacarga.Fecha_Inactivo))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeOperador_montacarga.Activo))

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

    Public Shared Function Actualizar(ByRef oBeOperador_montacarga As clsBeOperador_montacarga, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("operador_montacarga")
            Upd.Add("idasignacionmontacarga", "@idasignacionmontacarga", DataType.Parametro)
            Upd.Add("idoperador", "@idoperador", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("idmontacarga", "@idmontacarga", DataType.Parametro)
            Upd.Add("fecha_asignacion", "@fecha_asignacion", DataType.Parametro)
            Upd.Add("fecha_inactivo", "@fecha_inactivo", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("IdAsignacionMontacarga = @IdAsignacionMontacarga")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDASIGNACIONMONTACARGA", oBeOperador_montacarga.IdAsignacionMontacarga))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeOperador_montacarga.IdOperador))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeOperador_montacarga.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDMONTACARGA", oBeOperador_montacarga.IdMontacarga))
            cmd.Parameters.Add(New SqlParameter("@FECHA_ASIGNACION", oBeOperador_montacarga.Fecha_Asignacion))
            cmd.Parameters.Add(New SqlParameter("@FECHA_INACTIVO", oBeOperador_montacarga.Fecha_Inactivo))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeOperador_montacarga.Activo))

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


    Public Shared Function Eliminar(ByRef oBeOperador_montacarga As clsBeOperador_montacarga, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Operador_montacarga" & _
             "  Where(IdAsignacionMontacarga = @IdAsignacionMontacarga)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDASIGNACIONMONTACARGA", oBeOperador_montacarga.IdAsignacionMontacarga))

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

            Const sp As String = "SELECT * FROM Operador_montacarga"
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

    Public Shared Function Get_All() As List(Of clsBeOperador_montacarga)

        Dim lReturnList As New List(Of clsBeOperador_montacarga)

        Try

            Const sp As String = "SELECT * FROM Operador_montacarga"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeOperador_montacarga As New clsBeOperador_montacarga

                        For Each dr As DataRow In lDataTable.Rows
                            vBeOperador_montacarga = New clsBeOperador_montacarga()
                            Cargar(vBeOperador_montacarga, dr)
                            lReturnList.Add(vBeOperador_montacarga)
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

    Public Shared Sub GetSingle(ByRef pBeOperador_montacarga As clsBeOperador_montacarga)

        Try

            Const sp As String = "SELECT * FROM Operador_montacarga" & _
            " Where(IdAsignacionMontacarga = @IdAsignacionMontacarga)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeOperador_montacarga As New clsBeOperador_montacarga

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeOperador_montacarga, lDataTable.Rows(0))
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

            Const sp As String = "SELECT ISNULL(Max(IdAsignacionMontacarga),0) FROM Operador_montacarga"

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
