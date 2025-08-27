Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnEstructura_grupo

    Public Shared Sub Cargar(ByRef oBeEstructura_grupo As clsBeEstructura_grupo, ByRef dr As DataRow)
        Try
            With oBeEstructura_grupo
                .IdGrupo = IIf(IsDBNull(dr.Item("IdGrupo")), 0, dr.Item("IdGrupo"))
                .IdTramo = IIf(IsDBNull(dr.Item("IdTramo")), 0, dr.Item("IdTramo"))
                .Pos = IIf(IsDBNull(dr.Item("pos")), 0, dr.Item("pos"))
                .Cant = IIf(IsDBNull(dr.Item("cant")), 0, dr.Item("cant"))
                .Tamano = IIf(IsDBNull(dr.Item("tamano")), 0, dr.Item("tamano"))
                .Offset = IIf(IsDBNull(dr.Item("offset")), 0, dr.Item("offset"))
                .Ancho = IIf(IsDBNull(dr.Item("ancho")), 0.0, dr.Item("ancho"))
                .Alto = IIf(IsDBNull(dr.Item("alto")), 0.0, dr.Item("alto"))
                .Largo = IIf(IsDBNull(dr.Item("largo")), 0.0, dr.Item("largo"))
                .Palet = IIf(IsDBNull(dr.Item("palet")), 0, dr.Item("palet"))
                .Orient = IIf(IsDBNull(dr.Item("orient")), 0, dr.Item("orient"))
                .Agrupacion = IIf(IsDBNull(dr.Item("agrupacion")), 0, dr.Item("agrupacion"))
                .Orden_Descendente = IIf(IsDBNull(dr.Item("orden_descendente")), False, dr.Item("orden_descendente"))
            End With
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeEstructura_grupo As clsBeEstructura_grupo, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("estructura_grupo")
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("idgrupo", "@idgrupo", DataType.Parametro)
            Ins.Add("idtramo", "@idtramo", DataType.Parametro)
            Ins.Add("pos", "@pos", DataType.Parametro)
            Ins.Add("cant", "@cant", DataType.Parametro)
            Ins.Add("tamano", "@tamano", DataType.Parametro)
            Ins.Add("offset", "@offset", DataType.Parametro)
            Ins.Add("ancho", "@ancho", DataType.Parametro)
            Ins.Add("alto", "@alto", DataType.Parametro)
            Ins.Add("largo", "@largo", DataType.Parametro)
            Ins.Add("palet", "@palet", DataType.Parametro)
            Ins.Add("orient", "@orient", DataType.Parametro)
            Ins.Add("agrupacion", "@agrupacion", DataType.Parametro)
            Ins.Add("orden_descendente", "@orden_descendente", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeEstructura_grupo.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDGRUPO", oBeEstructura_grupo.IdGrupo))
            cmd.Parameters.Add(New SqlParameter("@IDTRAMO", oBeEstructura_grupo.IdTramo))
            cmd.Parameters.Add(New SqlParameter("@POS", oBeEstructura_grupo.Pos))
            cmd.Parameters.Add(New SqlParameter("@CANT", oBeEstructura_grupo.Cant))
            cmd.Parameters.Add(New SqlParameter("@TAMANO", oBeEstructura_grupo.Tamano))
            cmd.Parameters.Add(New SqlParameter("@OFFSET", oBeEstructura_grupo.Offset))
            cmd.Parameters.Add(New SqlParameter("@ANCHO", oBeEstructura_grupo.Ancho))
            cmd.Parameters.Add(New SqlParameter("@ALTO", oBeEstructura_grupo.Alto))
            cmd.Parameters.Add(New SqlParameter("@LARGO", oBeEstructura_grupo.Largo))
            cmd.Parameters.Add(New SqlParameter("@PALET", oBeEstructura_grupo.Palet))
            cmd.Parameters.Add(New SqlParameter("@ORIENT", oBeEstructura_grupo.Orient))
            cmd.Parameters.Add(New SqlParameter("@AGRUPACION", oBeEstructura_grupo.Agrupacion))
            cmd.Parameters.Add(New SqlParameter("@ORDEN_DESCENDENTE", oBeEstructura_grupo.Orden_Descendente))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

            oBeEstructura_grupo.IdGrupo = CInt(cmd.Parameters("@IDGRUPO").Value)

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeEstructura_grupo As clsBeEstructura_grupo, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("estructura_grupo")
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("idtramo", "@idtramo", DataType.Parametro)
            Upd.Add("pos", "@pos", DataType.Parametro)
            Upd.Add("cant", "@cant", DataType.Parametro)
            Upd.Add("tamano", "@tamano", DataType.Parametro)
            Upd.Add("offset", "@offset", DataType.Parametro)
            Upd.Add("ancho", "@ancho", DataType.Parametro)
            Upd.Add("alto", "@alto", DataType.Parametro)
            Upd.Add("largo", "@largo", DataType.Parametro)
            Upd.Add("palet", "@palet", DataType.Parametro)
            Upd.Add("orient", "@orient", DataType.Parametro)
            Upd.Add("agrupacion", "@agrupacion", DataType.Parametro)
            Upd.Add("orden_descendente", "@orden_descendente", DataType.Parametro)
            Upd.Where("IdGrupo = @IdGrupo")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeEstructura_grupo.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDGRUPO", oBeEstructura_grupo.IdGrupo))
            cmd.Parameters.Add(New SqlParameter("@IDTRAMO", oBeEstructura_grupo.IdTramo))
            cmd.Parameters.Add(New SqlParameter("@POS", oBeEstructura_grupo.Pos))
            cmd.Parameters.Add(New SqlParameter("@CANT", oBeEstructura_grupo.Cant))
            cmd.Parameters.Add(New SqlParameter("@TAMANO", oBeEstructura_grupo.Tamano))
            cmd.Parameters.Add(New SqlParameter("@OFFSET", oBeEstructura_grupo.Offset))
            cmd.Parameters.Add(New SqlParameter("@ANCHO", oBeEstructura_grupo.Ancho))
            cmd.Parameters.Add(New SqlParameter("@ALTO", oBeEstructura_grupo.Alto))
            cmd.Parameters.Add(New SqlParameter("@LARGO", oBeEstructura_grupo.Largo))
            cmd.Parameters.Add(New SqlParameter("@PALET", oBeEstructura_grupo.Palet))
            cmd.Parameters.Add(New SqlParameter("@ORIENT", oBeEstructura_grupo.Orient))
            cmd.Parameters.Add(New SqlParameter("@AGRUPACION", oBeEstructura_grupo.Agrupacion))
            cmd.Parameters.Add(New SqlParameter("@ORDEN_DESCENDENTE", oBeEstructura_grupo.Orden_Descendente))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Eliminar(ByRef oBeEstructura_grupo As clsBeEstructura_grupo, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Estructura_grupo" &
             "  Where(IdGrupo = @IdGrupo)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDGRUPO", oBeEstructura_grupo.IdGrupo))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try
    End Function

    Public Shared Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Estructura_grupo"
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)

            End If


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Estructura_grupo"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeEstructura_grupo As clsBeEstructura_grupo) As Boolean

        Try

            Const sp As String = "SELECT * FROM Estructura_grupo" &
            " Where(IdGrupo = @IdGrupo)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDGRUPO", oBeEstructura_grupo.IdGrupo))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeEstructura_grupo, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeEstructura_grupo)

        Try

            Dim lReturnList As New List(Of clsBeEstructura_grupo)
            Const sp As String = "SELECT * FROM Estructura_grupo "
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeEstructura_grupo As New clsBeEstructura_grupo

            For Each dr As DataRow In dt.Rows
                vBeEstructura_grupo = New clsBeEstructura_grupo
                Cargar(vBeEstructura_grupo, dr)
                lReturnList.Add(vBeEstructura_grupo)
            Next

            Return lReturnList

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeEstructura_grupo As clsBeEstructura_grupo)

        Try

            Const sp As String = "SELECT * FROM Estructura_grupo" &
            " Where(IdGrupo = @IdGrupo)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDGRUPO", pBeEstructura_grupo.IdGrupo))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeEstructura_grupo, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdGrupo),0) FROM Estructura_grupo"

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
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdGrupo),0) FROM Estructura_grupo "

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If
            End Using

            Return lMax

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdBodega_And_IdTramo(ByVal pIdBodega As Integer, ByVal pIdTramo As Integer) As List(Of clsBeEstructura_grupo)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lReturnList As New List(Of clsBeEstructura_grupo)
            Const sp As String = "SELECT * FROM Estructura_grupo WHERE IdBodega = @IdBodega AND IdTramo = @IdTramo"
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
            dad.SelectCommand.Parameters.AddWithValue("@IdTramo", pIdTramo)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeEstructura_grupo As New clsBeEstructura_grupo

            For Each dr As DataRow In dt.Rows
                vBeEstructura_grupo = New clsBeEstructura_grupo
                Cargar(vBeEstructura_grupo, dr)
                lReturnList.Add(vBeEstructura_grupo)
            Next

            Get_All_By_IdBodega_And_IdTramo = lReturnList

            lTransaction.Commit()

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Get_All_By_IdBodega_And_IdTramo(ByVal pIdBodega As Integer,
                                                                 ByVal pIdTramo As Integer,
                                                                 ByVal lConnection As SqlConnection,
                                                                 ByVal lTransaction As SqlTransaction) As List(Of clsBeEstructura_grupo)
        Try

            Dim lReturnList As New List(Of clsBeEstructura_grupo)
            Const sp As String = "SELECT * FROM Estructura_grupo WHERE IdBodega = @IdBodega AND IdTramo = @IdTramo"
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
            dad.SelectCommand.Parameters.AddWithValue("@IdTramo", pIdTramo)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeEstructura_grupo As New clsBeEstructura_grupo

            For Each dr As DataRow In dt.Rows
                vBeEstructura_grupo = New clsBeEstructura_grupo
                Cargar(vBeEstructura_grupo, dr)
                lReturnList.Add(vBeEstructura_grupo)
            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdBodega_And_IdTramo_DT(ByVal pIdBodega As Integer, ByVal pIdTramo As Integer) As DataTable

        Get_All_By_IdBodega_And_IdTramo_DT = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lReturnList As New List(Of clsBeEstructura_grupo)
            Const sp As String = "SELECT * FROM Estructura_grupo WHERE IdBodega = @IdBodega AND IdTramo = @IdTramo"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
            dad.SelectCommand.Parameters.AddWithValue("@IdTramo", pIdTramo)
            Dim dt As New DataTable

            dad.Fill(dt)

            Get_All_By_IdBodega_And_IdTramo_DT = dt

            lTransaction.Commit()

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

End Class
