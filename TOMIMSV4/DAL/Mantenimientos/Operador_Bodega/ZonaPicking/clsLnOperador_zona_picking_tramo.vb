Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnOperador_zona_picking_tramo

    Public Shared Sub Cargar(ByRef oBeOperador_zona_picking_tramo As clsBeOperador_zona_picking_tramo, ByRef dr As DataRow)
        Try
            With oBeOperador_zona_picking_tramo
                .IdZonaPickingTramoOperador = IIf(IsDBNull(dr.Item("IdZonaPickingTramoOperador")), 0, dr.Item("IdZonaPickingTramoOperador"))
                .IdZonaPickingTramo = IIf(IsDBNull(dr.Item("IdZonaPickingTramo")), 0, dr.Item("IdZonaPickingTramo"))
                .IdZonaPicking = IIf(IsDBNull(dr.Item("IdZonaPicking")), 0, dr.Item("IdZonaPicking"))
                .IdOperador = IIf(IsDBNull(dr.Item("IdOperador")), 0, dr.Item("IdOperador"))
                .Dia_semana = IIf(IsDBNull(dr.Item("dia_semana")), 0, dr.Item("dia_semana"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeOperador_zona_picking_tramo As clsBeOperador_zona_picking_tramo, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("operador_zona_picking_tramo")
            Ins.Add("idzonapickingtramooperador", "@idzonapickingtramooperador", DataType.Parametro)
            Ins.Add("idzonapickingtramo", "@idzonapickingtramo", DataType.Parametro)
            Ins.Add("idzonapicking", "@idzonapicking", DataType.Parametro)
            Ins.Add("idoperador", "@idoperador", DataType.Parametro)
            Ins.Add("dia_semana", "@dia_semana", DataType.Parametro)
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
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDZONAPICKINGTRAMOOPERADOR", oBeOperador_zona_picking_tramo.IdZonaPickingTramoOperador))
            cmd.Parameters.Add(New SqlParameter("@IDZONAPICKINGTRAMO", oBeOperador_zona_picking_tramo.IdZonaPickingTramo))
            cmd.Parameters.Add(New SqlParameter("@IDZONAPICKING", oBeOperador_zona_picking_tramo.IdZonaPicking))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeOperador_zona_picking_tramo.IdOperador))
            cmd.Parameters.Add(New SqlParameter("@DIA_SEMANA", oBeOperador_zona_picking_tramo.Dia_semana))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeOperador_zona_picking_tramo.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeOperador_zona_picking_tramo.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeOperador_zona_picking_tramo.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeOperador_zona_picking_tramo.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeOperador_zona_picking_tramo.Activo))

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

    Public Shared Function Actualizar(ByRef oBeOperador_zona_picking_tramo As clsBeOperador_zona_picking_tramo, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("operador_zona_picking_tramo")
            Upd.Add("idzonapickingtramooperador", "@idzonapickingtramooperador", DataType.Parametro)
            Upd.Add("idzonapickingtramo", "@idzonapickingtramo", DataType.Parametro)
            Upd.Add("idzonapicking", "@idzonapicking", DataType.Parametro)
            Upd.Add("idoperador", "@idoperador", DataType.Parametro)
            Upd.Add("dia_semana", "@dia_semana", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("IdZonaPickingTramoOperador = @IdZonaPickingTramoOperador")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDZONAPICKINGTRAMOOPERADOR", oBeOperador_zona_picking_tramo.IdZonaPickingTramoOperador))
            cmd.Parameters.Add(New SqlParameter("@IDZONAPICKINGTRAMO", oBeOperador_zona_picking_tramo.IdZonaPickingTramo))
            cmd.Parameters.Add(New SqlParameter("@IDZONAPICKING", oBeOperador_zona_picking_tramo.IdZonaPicking))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeOperador_zona_picking_tramo.IdOperador))
            cmd.Parameters.Add(New SqlParameter("@DIA_SEMANA", oBeOperador_zona_picking_tramo.Dia_semana))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeOperador_zona_picking_tramo.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeOperador_zona_picking_tramo.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeOperador_zona_picking_tramo.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeOperador_zona_picking_tramo.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeOperador_zona_picking_tramo.Activo))

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


    Public Shared Function Eliminar(ByRef oBeOperador_zona_picking_tramo As clsBeOperador_zona_picking_tramo, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Operador_zona_picking_tramo" &
             "  Where(IdZonaPickingTramoOperador = @IdZonaPickingTramoOperador)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDZONAPICKINGTRAMOOPERADOR", oBeOperador_zona_picking_tramo.IdZonaPickingTramoOperador))

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

            Const sp As String = "SELECT * FROM Operador_zona_picking_tramo"
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

    Public Shared Function Get_All() As List(Of clsBeOperador_zona_picking_tramo)

        Dim lReturnList As New List(Of clsBeOperador_zona_picking_tramo)

        Try

            Const sp As String = "SELECT * FROM Operador_zona_picking_tramo"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeOperador_zona_picking_tramo As New clsBeOperador_zona_picking_tramo

                        For Each dr As DataRow In lDataTable.Rows
                            vBeOperador_zona_picking_tramo = New clsBeOperador_zona_picking_tramo()
                            Cargar(vBeOperador_zona_picking_tramo, dr)
                            lReturnList.Add(vBeOperador_zona_picking_tramo)
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

    Public Shared Sub GetSingle(ByRef pBeOperador_zona_picking_tramo As clsBeOperador_zona_picking_tramo)

        Try

            Const sp As String = "SELECT * FROM Operador_zona_picking_tramo" &
            " Where(IdZonaPickingTramoOperador = @IdZonaPickingTramoOperador)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeOperador_zona_picking_tramo As New clsBeOperador_zona_picking_tramo

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeOperador_zona_picking_tramo, lDataTable.Rows(0))
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

            Const sp As String = "SELECT ISNULL(Max(IdZonaPickingTramoOperador),0) FROM Operador_zona_picking_tramo"

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

    Public Shared Function Get_All_DT_By_IdEmpresa_And_IdOperador(ByVal pIdEmpresa As Integer,
                                                                  ByVal pIdOperador As Integer) As DataTable

        Get_All_DT_By_IdEmpresa_And_IdOperador = Nothing

        Try

            Const sp As String = "SELECT IdEmpresa,
                                         IdZonaPickingTramoOperador,
                                         IdZonaPickingTramo,  
                                         IdZonaPicking,
                                         Zona_Picking,
                                         Dia_Semana as NoDia,
                                         Dia,                                         
                                         Tramo,
                                         min_x as ColumnaDesde,
                                         max_x as ColumnaHasta,
                                         min_y as NivelDesde,
                                         max_Y as NivelHasta
                                         FROM VW_Operador_Zona_Picking 
                                         WHERE IdEmpresa = @IdEmpresa AND IdOperador = @IdOperador "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdEmpresa", pIdEmpresa)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOperador", pIdOperador)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Get_All_DT_By_IdEmpresa_And_IdOperador = lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdEmpresa_And_IdBodega(ByVal pIdEmpresa As Integer,
                                                             ByVal IdBodega As Integer,
                                                             ByVal pDiaSemana As Integer) As List(Of clsBeOperador_zona_picking_tramo)

        Dim lReturnList As New List(Of clsBeOperador_zona_picking_tramo)

        Try

            Const sp As String = "SELECT * FROM VW_Zona_Picking_Tramo_Operador 
                                  WHERE IdEmpresa = @IdEmpresa AND IdBodega = @IdBodega  
                                  AND dia_semana =@dia_semana"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdEmpresa", pIdEmpresa)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@dia_semana", pDiaSemana)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeOperador_zona_picking_tramo As New clsBeOperador_zona_picking_tramo

                        For Each dr As DataRow In lDataTable.Rows

                            vBeOperador_zona_picking_tramo = New clsBeOperador_zona_picking_tramo()
                            Cargar(vBeOperador_zona_picking_tramo, dr)

                            'vBeOperador_zona_picking_tramo.Lista_Tramos_Zona_Picking_Tramo = clsLnZona_picking_tramo.Get_All_By_IdZonaPickingTramo(vBeOperador_zona_picking_tramo.IdZonaPickingTramo,
                            '                                                                                                                       lConnection,
                            '                                                                                                                       lTransaction)
                            lReturnList.Add(vBeOperador_zona_picking_tramo)

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

    Public Shared Function Exists(ByVal pIdOperador As Integer,
                                  ByVal pIdZonaPickingTramo As Integer,
                                  ByVal pDia As Integer) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM VW_Operador_Zona_Picking 
                                  WHERE IdOperador = @IdOPerador 
                                  AND IdZonaPickingTramo=@IdZonaPickingTramo And dia_semana=@dia
                                  AND IdOperador = @IdOperador  "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(vSQL, lConnection)

                    lCommand.CommandType = CommandType.Text

                    lCommand.Parameters.AddWithValue("@IdOperador", pIdOperador)
                    lCommand.Parameters.AddWithValue("@IdZonaPickingTramo", pIdZonaPickingTramo)
                    lCommand.Parameters.AddWithValue("@dia", pDia)

                    lConnection.Open()

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()

                    lConnection.Close()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then

                        lExists = CInt(lReturnValue) > 0

                    End If

                End Using

            End Using

            Return lExists

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function


    Public Shared Function Get_Single_By_IdZonaPickingTramoOperador(ByVal IdZonaPickingTramoOperador As Integer) As clsBeOperador_zona_picking_tramo

        Get_Single_By_IdZonaPickingTramoOperador = Nothing

        Try

            Const sp As String = "SELECT * FROM Operador_zona_picking_tramo Where(IdZonaPickingTramoOperador = @IdZonaPickingTramoOperador)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdZonaPickingTramoOperador", IdZonaPickingTramoOperador)
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeOperador_zona_picking_tramo As New clsBeOperador_zona_picking_tramo

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeOperador_zona_picking_tramo, lDataTable.Rows(0))
                            Get_Single_By_IdZonaPickingTramoOperador = vBeOperador_zona_picking_tramo
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' #EJC20220601: Se utiliza para determinar si la zona de picking ya fue previamente asignada al operador.
    ''' </summary>
    ''' <param name="pIdZonaPickinng"></param>
    ''' <returns></returns>
    Public Shared Function Get_All_By_IdZonaPicking(ByVal pIdZonaPickinng As Integer) As List(Of clsBeOperador_zona_picking_tramo)

        Dim lReturnList As New List(Of clsBeOperador_zona_picking_tramo)

        Try

            Const sp As String = "SELECT * FROM Operador_zona_picking_tramo WHERE IdZonaPicking = @IdZonaPicking "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdZonaPicking", pIdZonaPickinng)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeOperador_zona_picking_tramo As New clsBeOperador_zona_picking_tramo

                        For Each dr As DataRow In lDataTable.Rows
                            vBeOperador_zona_picking_tramo = New clsBeOperador_zona_picking_tramo()
                            Cargar(vBeOperador_zona_picking_tramo, dr)
                            lReturnList.Add(vBeOperador_zona_picking_tramo)
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

    Public Shared Function Get_All_By_IdZonaPicking(ByVal pIdZonaPickinng As Integer,
                                                    ByVal lConnection As SqlConnection,
                                                    ByVal lTransaction As SqlTransaction) As List(Of clsBeOperador_zona_picking_tramo)

        Dim lReturnList As New List(Of clsBeOperador_zona_picking_tramo)

        Try

            Const sp As String = "SELECT * FROM Operador_zona_picking_tramo WHERE IdZonaPicking = @IdZonaPicking "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdZonaPicking", pIdZonaPickinng)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeOperador_zona_picking_tramo As New clsBeOperador_zona_picking_tramo

                For Each dr As DataRow In lDataTable.Rows
                    vBeOperador_zona_picking_tramo = New clsBeOperador_zona_picking_tramo()
                    Cargar(vBeOperador_zona_picking_tramo, dr)
                    lReturnList.Add(vBeOperador_zona_picking_tramo)
                Next

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdZonaPickingTramoOperador),0) FROM Operador_zona_picking_tramo "

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




End Class