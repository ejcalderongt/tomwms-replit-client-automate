Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTarifa_tipo_transaccion_det

    Public Shared Sub Cargar(ByRef oBeTarifa_tipo_transaccion_det As clsBeTarifa_tipo_transaccion_det, ByRef dr As DataRow)
        Try
            With oBeTarifa_tipo_transaccion_det
                .IdTipoTransaccion = IIf(IsDBNull(dr.Item("IdTipoTransaccion")), 0, dr.Item("IdTipoTransaccion"))
                .IdServicio = IIf(IsDBNull(dr.Item("IdServicio")), 0, dr.Item("IdServicio"))
                .Activo = True
            End With
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTarifa_tipo_transaccion_det As clsBeTarifa_tipo_transaccion_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("tarifa_tipo_transaccion_det")
            Ins.Add("idtipotransaccion", "@idtipotransaccion", DataType.Parametro)
            Ins.Add("idservicio", "@idservicio", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction()
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTIPOTRANSACCION", oBeTarifa_tipo_transaccion_det.IdTipoTransaccion))
            cmd.Parameters.Add(New SqlParameter("@IDSERVICIO", oBeTarifa_tipo_transaccion_det.IdServicio))

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

    Public Shared Function Actualizar(ByRef oBeTarifa_tipo_transaccion_det As clsBeTarifa_tipo_transaccion_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("tarifa_tipo_transaccion_det")
            Upd.Add("idtipotransaccion", "@idtipotransaccion", DataType.Parametro)
            Upd.Add("idservicio", "@idservicio", DataType.Parametro)
            Upd.Where("IdTipoTransaccion = @IdTipoTransaccion" &
                " AND IdServicio = @IdServicio")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction()
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTIPOTRANSACCION", oBeTarifa_tipo_transaccion_det.IdTipoTransaccion))
            cmd.Parameters.Add(New SqlParameter("@IDSERVICIO", oBeTarifa_tipo_transaccion_det.IdServicio))

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

    Public Shared Function Existe(idTipoTransaccion As Integer, idServicio As Integer) As Boolean

        Existe = False

        Try

            Const sp As String = "SELECT * FROM Tarifa_tipo_transaccion_det " &
            " Where(IdTipoTransaccion = @IdTipoTransaccion)" &
            " AND (IdServicio = @IdServicio)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction()

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("IdTipoTransaccion", idTipoTransaccion)
                        lDTA.SelectCommand.Parameters.AddWithValue("IdServicio", idServicio)
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTarifa_tipo_transaccion_det As New clsBeTarifa_tipo_transaccion_det

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            'Cargar(vBeTarifa_tipo_transaccion_det, lDataTable.Rows(0))
                            Existe = True
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

    End Function

    Public Shared Function Existe(idTipoTransaccion As Integer, idServicio As Integer, lConnection As SqlConnection, lTransaction As SqlTransaction) As Boolean

        Existe = False

        Try

            Const sp As String = "SELECT * FROM Tarifa_tipo_transaccion_det " &
            " Where(IdTipoTransaccion = @IdTipoTransaccion)" &
            " AND (IdServicio = @IdServicio)"

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("IdTipoTransaccion", idTipoTransaccion)
                lDTA.SelectCommand.Parameters.AddWithValue("IdServicio", idServicio)
                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeTarifa_tipo_transaccion_det As New clsBeTarifa_tipo_transaccion_det

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Existe = True
                End If

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Eliminar(ByRef oBeTarifa_tipo_transaccion_det As clsBeTarifa_tipo_transaccion_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Tarifa_tipo_transaccion_det" &
             "  Where(IdTipoTransaccion = @IdTipoTransaccion)" &
             "  AND (IdServicio = @IdServicio)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction()
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTIPOTRANSACCION", oBeTarifa_tipo_transaccion_det.IdTipoTransaccion))
            cmd.Parameters.Add(New SqlParameter("@IDSERVICIO", oBeTarifa_tipo_transaccion_det.IdServicio))

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

            Const sp As String = "SELECT * FROM Tarifa_tipo_transaccion_det"
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

    Public Shared Function GetAll() As List(Of clsBeTarifa_tipo_transaccion_det)

        Dim lReturnList As New List(Of clsBeTarifa_tipo_transaccion_det)

        Try

            Const sp As String = "SELECT * FROM Tarifa_tipo_transaccion_det"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction()

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTarifa_tipo_transaccion_det As New clsBeTarifa_tipo_transaccion_det

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTarifa_tipo_transaccion_det = New clsBeTarifa_tipo_transaccion_det()
                            Cargar(vBeTarifa_tipo_transaccion_det, dr)
                            lReturnList.Add(vBeTarifa_tipo_transaccion_det)
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

    Public Shared Function Get_All_By_IdTipoTransaccion_DT(ByVal pIdTipoTransaccion As Integer) As DataTable

        Dim lReturnList As New List(Of clsBeTarifa_tipo_transaccion_det)

        Get_All_By_IdTipoTransaccion_DT = Nothing

        Try

            'Select Case tarifa_tipo_transaccion_det.IdTipoTransaccion, tarifa_tipo_transaccion_det.IdServicio, i_nav_servicio.codigo_servicio As Codigo, i_nav_servicio.descripcion As Nombre, i_nav_servicio.nemonico As Nemonico

            Const sp As String = " Select tarifa_tipo_transaccion_det.IdServicio, i_nav_servicio.codigo_servicio As Codigo, i_nav_servicio.descripcion As Nombre, i_nav_servicio.nemonico As Nemonico, 0 As Cantidad
									FROM tarifa_tipo_transaccion_det INNER JOIN
									i_nav_servicio ON 
								    tarifa_tipo_transaccion_det.IdServicio = i_nav_servicio.IdServicio
									WHERE tarifa_tipo_transaccion_det.IdTipoTransaccion = @IdTipoTransaccion "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdTipoTransaccion", pIdTipoTransaccion)
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)
                        Return lDataTable

                        'Dim vBeTarifa_tipo_transaccion_det As New clsBeTarifa_tipo_transaccion_det

                        'For Each dr As DataRow In lDataTable.Rows
                        '	vBeTarifa_tipo_transaccion_det = New clsBeTarifa_tipo_transaccion_det()
                        '	Cargar(vBeTarifa_tipo_transaccion_det, dr)
                        '	lReturnList.Add(vBeTarifa_tipo_transaccion_det)
                        'Next

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

    Public Shared Function Get_All_By_IdTipoTransaccion(ByVal pIdTipoTransaccion As Integer) As List(Of clsBeTarifa_tipo_transaccion_det)

        Dim lReturnList As New List(Of clsBeTarifa_tipo_transaccion_det)

        Get_All_By_IdTipoTransaccion = Nothing

        Try

            Const sp As String = " SELECT tarifa_tipo_transaccion_det.IdTipoTransaccion, tarifa_tipo_transaccion_det.IdServicio, i_nav_servicio.codigo_servicio as Codigo, i_nav_servicio.descripcion as Nombre, i_nav_servicio.nemonico as Nemonico
									FROM tarifa_tipo_transaccion_det INNER JOIN
									i_nav_servicio ON 
								    tarifa_tipo_transaccion_det.IdServicio = i_nav_servicio.IdServicio
									WHERE tarifa_tipo_transaccion_det.IdTipoTransaccion = @IdTipoTransaccion "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdTipoTransaccion", pIdTipoTransaccion)
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTarifa_tipo_transaccion_det As New clsBeTarifa_tipo_transaccion_det

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTarifa_tipo_transaccion_det = New clsBeTarifa_tipo_transaccion_det()
                            Cargar(vBeTarifa_tipo_transaccion_det, dr)
                            lReturnList.Add(vBeTarifa_tipo_transaccion_det)
                        Next

                        Return lReturnList

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

    Public Shared Function Get_All_By_IdTipoTransaccion(ByVal pIdTipoTransaccion As Integer, lConnection As SqlConnection, lTransaction As SqlTransaction) As List(Of clsBeTarifa_tipo_transaccion_det)

        Dim lReturnList As New List(Of clsBeTarifa_tipo_transaccion_det)

        Get_All_By_IdTipoTransaccion = Nothing

        Try

            Const sp As String = " SELECT * 
									FROM tarifa_tipo_transaccion_det 
									WHERE IdTipoTransaccion = @IdTipoTransaccion "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdTipoTransaccion", pIdTipoTransaccion)
                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeTarifa_tipo_transaccion_det As New clsBeTarifa_tipo_transaccion_det

                For Each dr As DataRow In lDataTable.Rows
                    vBeTarifa_tipo_transaccion_det = New clsBeTarifa_tipo_transaccion_det()
                    Cargar(vBeTarifa_tipo_transaccion_det, dr)
                    lReturnList.Add(vBeTarifa_tipo_transaccion_det)
                Next

                Return lReturnList

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Sub GetSingle(ByRef pBeTarifa_tipo_transaccion_det As clsBeTarifa_tipo_transaccion_det)

        Try

            Const sp As String = "SELECT * FROM Tarifa_tipo_transaccion_det" &
            " Where(IdTipoTransaccion = @IdTipoTransaccion)" &
            " AND (IdServicio = @IdServicio)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction()

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTarifa_tipo_transaccion_det As New clsBeTarifa_tipo_transaccion_det

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTarifa_tipo_transaccion_det, lDataTable.Rows(0))
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

            Const sp As String = "SELECT ISNULL(Max(IdTipoTransaccion),0) FROM Tarifa_tipo_transaccion_det"

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

    Public Shared Function Get_All_By_IdTipoTransaccion_DT(ByVal pIdTipoTransaccion As Integer, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As DataTable

        Dim lReturnList As New List(Of clsBeTarifa_tipo_transaccion_det)

        Get_All_By_IdTipoTransaccion_DT = Nothing

        Try

            'Select Case tarifa_tipo_transaccion_det.IdTipoTransaccion, tarifa_tipo_transaccion_det.IdServicio, i_nav_servicio.codigo_servicio As Codigo, i_nav_servicio.descripcion As Nombre, i_nav_servicio.nemonico As Nemonico

            Const sp As String = " Select tarifa_tipo_transaccion_det.IdServicio, i_nav_servicio.codigo_servicio As Codigo, i_nav_servicio.descripcion As Nombre, i_nav_servicio.nemonico As Nemonico, 0 As Cantidad
									FROM tarifa_tipo_transaccion_det INNER JOIN
									i_nav_servicio ON 
								    tarifa_tipo_transaccion_det.IdServicio = i_nav_servicio.IdServicio
									WHERE tarifa_tipo_transaccion_det.IdTipoTransaccion = @IdTipoTransaccion "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdTipoTransaccion", pIdTipoTransaccion)
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

End Class
