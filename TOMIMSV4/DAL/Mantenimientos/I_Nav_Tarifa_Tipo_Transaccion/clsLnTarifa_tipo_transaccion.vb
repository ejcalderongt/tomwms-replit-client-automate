Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTarifa_tipo_transaccion

    Public Shared Sub Cargar(ByRef oBeTarifa_tipo_transaccion As clsBeTarifa_tipo_transaccion, ByRef dr As DataRow)
        Try
            With oBeTarifa_tipo_transaccion
                .IdTipoTransaccion = IIf(IsDBNull(dr.Item("IdTipoTransaccion")), 0, dr.Item("IdTipoTransaccion"))
                .Codigo = IIf(IsDBNull(dr.Item("Codigo")), "", dr.Item("Codigo"))
                .Nombre = IIf(IsDBNull(dr.Item("Nombre")), "", dr.Item("Nombre"))
                .Activo = IIf(IsDBNull(dr.Item("Activo")), False, dr.Item("Activo"))
            End With
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTarifa_tipo_transaccion As clsBeTarifa_tipo_transaccion, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("tarifa_tipo_transaccion")
            Ins.Add("idtipotransaccion", "@idtipotransaccion", DataType.Parametro)
            Ins.Add("codigo", "@codigo", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction()
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTIPOTRANSACCION", oBeTarifa_tipo_transaccion.IdTipoTransaccion))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeTarifa_tipo_transaccion.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeTarifa_tipo_transaccion.Nombre))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTarifa_tipo_transaccion.Activo))

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

    Public Shared Function Actualizar(ByRef oBeTarifa_tipo_transaccion As clsBeTarifa_tipo_transaccion, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("tarifa_tipo_transaccion")
            Upd.Add("idtipotransaccion", "@idtipotransaccion", DataType.Parametro)
            Upd.Add("codigo", "@codigo", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("IdTipoTransaccion = @IdTipoTransaccion")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction()
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTIPOTRANSACCION", oBeTarifa_tipo_transaccion.IdTipoTransaccion))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeTarifa_tipo_transaccion.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeTarifa_tipo_transaccion.Nombre))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTarifa_tipo_transaccion.Activo))

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

    Public Shared Function Guardar_Transaccion(ByRef oBeTarifa_tipo_transaccion As clsBeTarifa_tipo_transaccion, IsNew As Boolean)

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim rowsAffected As Integer = 0

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            If IsNew Then
                rowsAffected = Insertar(oBeTarifa_tipo_transaccion, lConnection, lTransaction)
            Else
                rowsAffected = Actualizar(oBeTarifa_tipo_transaccion, lConnection, lTransaction)
            End If


            For Each BeTarifaTipoTransaccionDet In oBeTarifa_tipo_transaccion.lDetalleServicios

                If BeTarifaTipoTransaccionDet.Activo Then
                    If Not clsLnTarifa_tipo_transaccion_det.Existe(BeTarifaTipoTransaccionDet.IdTipoTransaccion, BeTarifaTipoTransaccionDet.IdServicio, lConnection, lTransaction) Then
                        rowsAffected += clsLnTarifa_tipo_transaccion_det.Insertar(BeTarifaTipoTransaccionDet, lConnection, lTransaction)
                    End If
                Else
                    rowsAffected += clsLnTarifa_tipo_transaccion_det.Eliminar(BeTarifaTipoTransaccionDet, lConnection, lTransaction)
                End If

            Next

            lTransaction.Commit()

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

    Public Shared Function Eliminar(ByRef oBeTarifa_tipo_transaccion As clsBeTarifa_tipo_transaccion, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Tarifa_tipo_transaccion" &
             "  Where(IdTipoTransaccion = @IdTipoTransaccion)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction()
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTIPOTRANSACCION", oBeTarifa_tipo_transaccion.IdTipoTransaccion))

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

            Const sp As String = "SELECT * FROM Tarifa_tipo_transaccion"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction()
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

    Public Shared Function GetAll() As List(Of clsBeTarifa_tipo_transaccion)

        Dim lReturnList As New List(Of clsBeTarifa_tipo_transaccion)

        Try

            Const sp As String = "SELECT * FROM Tarifa_tipo_transaccion"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction()

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTarifa_tipo_transaccion As New clsBeTarifa_tipo_transaccion

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTarifa_tipo_transaccion = New clsBeTarifa_tipo_transaccion()
                            Cargar(vBeTarifa_tipo_transaccion, dr)
                            lReturnList.Add(vBeTarifa_tipo_transaccion)
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

    Public Shared Sub GetSingle(ByRef pBeTarifa_tipo_transaccion As clsBeTarifa_tipo_transaccion)

        Try

            Const sp As String = "SELECT * FROM Tarifa_tipo_transaccion" &
            " Where(IdTipoTransaccion = @IdTipoTransaccion)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction()

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdTipoTransaccion", pBeTarifa_tipo_transaccion.IdTipoTransaccion)
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTarifa_tipo_transaccion As New clsBeTarifa_tipo_transaccion

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTarifa_tipo_transaccion, lDataTable.Rows(0))
                            vBeTarifa_tipo_transaccion.lDetalleServicios = clsLnTarifa_tipo_transaccion_det.Get_All_By_IdTipoTransaccion(vBeTarifa_tipo_transaccion.IdTipoTransaccion, lConnection, lTransaction)
                            pBeTarifa_tipo_transaccion = vBeTarifa_tipo_transaccion
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

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdTipoTransaccion),0) FROM Tarifa_tipo_transaccion"

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
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
