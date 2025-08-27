Imports System.Data.SqlClient

Public Class clsLnDh_ocupacion_bodega

    Public Shared Sub Cargar(ByRef oBeDh_ocupacion_bodega As clsBeDh_ocupacion_bodega, ByRef dr As DataRow)
        Try
            With oBeDh_ocupacion_bodega
                .IdOcupacionBodega = IIf(IsDBNull(dr.Item("IdOcupacionBodega")), 0, dr.Item("IdOcupacionBodega"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .Cant_ubicaciones_vacias = IIf(IsDBNull(dr.Item("cant_ubicaciones_vacias")), 0.0, dr.Item("cant_ubicaciones_vacias"))
                .Cant_ubicaciones_ocupadas = IIf(IsDBNull(dr.Item("cant_ubicaciones_ocupadas")), 0.0, dr.Item("cant_ubicaciones_ocupadas"))
                .Fecha = IIf(IsDBNull(dr.Item("fecha")), Date.Now, dr.Item("fecha"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeDh_ocupacion_bodega As clsBeDh_ocupacion_bodega, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("dh_ocupacion_bodega")
            Ins.Add("idocupacionbodega", "@idocupacionbodega", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("cant_ubicaciones_vacias", "@cant_ubicaciones_vacias", DataType.Parametro)
            Ins.Add("cant_ubicaciones_ocupadas", "@cant_ubicaciones_ocupadas", DataType.Parametro)
            Ins.Add("fecha", "@fecha", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDOCUPACIONBODEGA", oBeDh_ocupacion_bodega.IdOcupacionBodega))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeDh_ocupacion_bodega.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@CANT_UBICACIONES_VACIAS", oBeDh_ocupacion_bodega.Cant_ubicaciones_vacias))
            cmd.Parameters.Add(New SqlParameter("@CANT_UBICACIONES_OCUPADAS", oBeDh_ocupacion_bodega.Cant_ubicaciones_ocupadas))
            cmd.Parameters.Add(New SqlParameter("@FECHA", oBeDh_ocupacion_bodega.Fecha))

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

    Public Shared Function Actualizar(ByRef oBeDh_ocupacion_bodega As clsBeDh_ocupacion_bodega, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("dh_ocupacion_bodega")
            Upd.Add("idocupacionbodega", "@idocupacionbodega", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("cant_ubicaciones_vacias", "@cant_ubicaciones_vacias", DataType.Parametro)
            Upd.Add("cant_ubicaciones_ocupadas", "@cant_ubicaciones_ocupadas", DataType.Parametro)
            Upd.Add("fecha", "@fecha", DataType.Parametro)
            Upd.Where("IdOcupacionBodega = @IdOcupacionBodega")

            Dim sp As String = Upd.SQL()

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDOCUPACIONBODEGA", oBeDh_ocupacion_bodega.IdOcupacionBodega))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeDh_ocupacion_bodega.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@CANT_UBICACIONES_VACIAS", oBeDh_ocupacion_bodega.Cant_ubicaciones_vacias))
            cmd.Parameters.Add(New SqlParameter("@CANT_UBICACIONES_OCUPADAS", oBeDh_ocupacion_bodega.Cant_ubicaciones_ocupadas))
            cmd.Parameters.Add(New SqlParameter("@FECHA", oBeDh_ocupacion_bodega.Fecha))

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

    Public Shared Function Eliminar(ByRef oBeDh_ocupacion_bodega As clsBeDh_ocupacion_bodega, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Dh_ocupacion_bodega" &
             "  Where(IdOcupacionBodega = @IdOcupacionBodega)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDOCUPACIONBODEGA", oBeDh_ocupacion_bodega.IdOcupacionBodega))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

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

    Public Shared Function GetAll() As List(Of clsBeDh_ocupacion_bodega)

        Dim lReturnList As New List(Of clsBeDh_ocupacion_bodega)

        Try

            Const sp As String = "SELECT * FROM Dh_ocupacion_bodega"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeDh_ocupacion_bodega As New clsBeDh_ocupacion_bodega

                        For Each dr As DataRow In lDataTable.Rows
                            vBeDh_ocupacion_bodega = New clsBeDh_ocupacion_bodega
                            Cargar(vBeDh_ocupacion_bodega, dr)
                            lReturnList.Add(vBeDh_ocupacion_bodega)
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

    Public Shared Sub GetSingle(ByRef pBeDh_ocupacion_bodega As clsBeDh_ocupacion_bodega)

        Try

            Const sp As String = "SELECT * FROM Dh_ocupacion_bodega 
								  Where(IdOcupacionBodega = @IdOcupacionBodega) "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeDh_ocupacion_bodega As New clsBeDh_ocupacion_bodega

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(pBeDh_ocupacion_bodega, lDataTable.Rows(0))
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

            Const sp As String = "SELECT ISNULL(Max(IdOcupacionBodega),0) FROM Dh_ocupacion_bodega"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

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

End Class
