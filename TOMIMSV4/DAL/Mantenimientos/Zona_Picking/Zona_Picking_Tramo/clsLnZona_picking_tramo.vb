Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnZona_picking_tramo

    Public Shared Sub Cargar(ByRef oBeZona_picking_tramo As clsBeZona_picking_tramo, ByRef dr As DataRow)
        Try
            With oBeZona_picking_tramo
                .IdZonaPickingTramo = IIf(IsDBNull(dr.Item("IdZonaPickingTramo")), 0, dr.Item("IdZonaPickingTramo"))
                .IdZonaPicking = IIf(IsDBNull(dr.Item("IdZonaPicking")), 0, dr.Item("IdZonaPicking"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .IdArea = IIf(IsDBNull(dr.Item("IdArea")), 0, dr.Item("IdArea"))
                .IdSector = IIf(IsDBNull(dr.Item("IdSector")), 0, dr.Item("IdSector"))
                .IdTramo = IIf(IsDBNull(dr.Item("IdTramo")), 0, dr.Item("IdTramo"))
                .Min_x = IIf(IsDBNull(dr.Item("min_x")), 0, dr.Item("min_x"))
                .Max_x = IIf(IsDBNull(dr.Item("max_x")), 0, dr.Item("max_x"))
                .Min_y = IIf(IsDBNull(dr.Item("min_y")), 0, dr.Item("min_y"))
                .Max_y = IIf(IsDBNull(dr.Item("max_y")), 0, dr.Item("max_y"))
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

    Public Shared Function Insertar(ByRef oBeZona_picking_tramo As clsBeZona_picking_tramo, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("zona_picking_tramo")
            Ins.Add("idzonapickingtramo", "@idzonapickingtramo", DataType.Parametro)
            Ins.Add("idzonapicking", "@idzonapicking", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("idarea", "@idarea", DataType.Parametro)
            Ins.Add("idsector", "@idsector", DataType.Parametro)
            Ins.Add("idtramo", "@idtramo", DataType.Parametro)
            Ins.Add("min_x", "@min_x", DataType.Parametro)
            Ins.Add("max_x", "@max_x", DataType.Parametro)
            Ins.Add("min_y", "@min_y", DataType.Parametro)
            Ins.Add("max_y", "@max_y", DataType.Parametro)
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

            cmd.Parameters.Add(New SqlParameter("@IDZONAPICKINGTRAMO", oBeZona_picking_tramo.IdZonaPickingTramo))
            cmd.Parameters.Add(New SqlParameter("@IDZONAPICKING", oBeZona_picking_tramo.IdZonaPicking))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeZona_picking_tramo.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDAREA", oBeZona_picking_tramo.IdArea))
            cmd.Parameters.Add(New SqlParameter("@IDSECTOR", oBeZona_picking_tramo.IdSector))
            cmd.Parameters.Add(New SqlParameter("@IDTRAMO", oBeZona_picking_tramo.IdTramo))
            cmd.Parameters.Add(New SqlParameter("@MIN_X", oBeZona_picking_tramo.Min_x))
            cmd.Parameters.Add(New SqlParameter("@MAX_X", oBeZona_picking_tramo.Max_x))
            cmd.Parameters.Add(New SqlParameter("@MIN_Y", oBeZona_picking_tramo.Min_y))
            cmd.Parameters.Add(New SqlParameter("@MAX_Y", oBeZona_picking_tramo.Max_y))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeZona_picking_tramo.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeZona_picking_tramo.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeZona_picking_tramo.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeZona_picking_tramo.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeZona_picking_tramo.Activo))

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

    Public Shared Function Actualizar(ByRef oBeZona_picking_tramo As clsBeZona_picking_tramo, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("zona_picking_tramo")
            Upd.Add("idzonapickingtramo", "@idzonapickingtramo", DataType.Parametro)
            Upd.Add("idzonapicking", "@idzonapicking", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("idarea", "@idarea", DataType.Parametro)
            Upd.Add("idsector", "@idsector", DataType.Parametro)
            Upd.Add("idtramo", "@idtramo", DataType.Parametro)
            Upd.Add("min_x", "@min_x", DataType.Parametro)
            Upd.Add("max_x", "@max_x", DataType.Parametro)
            Upd.Add("min_y", "@min_y", DataType.Parametro)
            Upd.Add("max_y", "@max_y", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("IdZonaPickingTramo = @IdZonaPickingTramo")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDZONAPICKINGTRAMO", oBeZona_picking_tramo.IdZonaPickingTramo))
            cmd.Parameters.Add(New SqlParameter("@IDZONAPICKING", oBeZona_picking_tramo.IdZonaPicking))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeZona_picking_tramo.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDAREA", oBeZona_picking_tramo.IdArea))
            cmd.Parameters.Add(New SqlParameter("@IDSECTOR", oBeZona_picking_tramo.IdSector))
            cmd.Parameters.Add(New SqlParameter("@IDTRAMO", oBeZona_picking_tramo.IdTramo))
            cmd.Parameters.Add(New SqlParameter("@MIN_X", oBeZona_picking_tramo.Min_x))
            cmd.Parameters.Add(New SqlParameter("@MAX_X", oBeZona_picking_tramo.Max_x))
            cmd.Parameters.Add(New SqlParameter("@MIN_Y", oBeZona_picking_tramo.Min_y))
            cmd.Parameters.Add(New SqlParameter("@MAX_Y", oBeZona_picking_tramo.Max_y))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeZona_picking_tramo.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeZona_picking_tramo.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeZona_picking_tramo.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeZona_picking_tramo.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeZona_picking_tramo.Activo))

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


    Public Shared Function Eliminar(ByRef oBeZona_picking_tramo As clsBeZona_picking_tramo, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Zona_picking_tramo" & _
             "  Where(IdZonaPickingTramo = @IdZonaPickingTramo)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDZONAPICKINGTRAMO", oBeZona_picking_tramo.IdZonaPickingTramo))

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

            Const sp As String = "SELECT * FROM Zona_picking_tramo"
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

    Public Shared Function Get_All() As List(Of clsBeZona_picking_tramo)

        Dim lReturnList As New List(Of clsBeZona_picking_tramo)

        Try

            Const sp As String = "SELECT * FROM Zona_picking_tramo"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeZona_picking_tramo As New clsBeZona_picking_tramo

                        For Each dr As DataRow In lDataTable.Rows
                            vBeZona_picking_tramo = New clsBeZona_picking_tramo()
                            Cargar(vBeZona_picking_tramo, dr)
                            lReturnList.Add(vBeZona_picking_tramo)
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

    Public Shared Sub GetSingle(ByRef pBeZona_picking_tramo As clsBeZona_picking_tramo)

        Try

            Const sp As String = "SELECT * FROM Zona_picking_tramo" & _
            " Where(IdZonaPickingTramo = @IdZonaPickingTramo)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeZona_picking_tramo As New clsBeZona_picking_tramo

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeZona_picking_tramo, lDataTable.Rows(0))
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

            Const sp As String = "SELECT ISNULL(Max(IdZonaPickingTramo),0) FROM Zona_picking_tramo"

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

    Public Shared Function Get_All_By_IdZonaPicking(ByVal pIdZonaPicking As Integer,
                                                    ByVal lConnection As SqlConnection,
                                                    ByVal lTransaction As SqlTransaction) As List(Of clsBeZona_picking_tramo)

        Dim lReturnList As New List(Of clsBeZona_picking_tramo)

        Try

            Const sp As String = "SELECT * FROM Zona_picking_tramo WHERE IdZonaPicking = @IdZonaPicking "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdZonaPicking", pIdZonaPicking)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeZona_picking_tramo As New clsBeZona_picking_tramo

                For Each dr As DataRow In lDataTable.Rows
                    vBeZona_picking_tramo = New clsBeZona_picking_tramo()
                    Cargar(vBeZona_picking_tramo, dr)
                    lReturnList.Add(vBeZona_picking_tramo)
                Next

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdZonaPicking(ByVal pIdZonaPicking As Integer) As List(Of clsBeZona_picking_tramo)

        Dim lReturnList As New List(Of clsBeZona_picking_tramo)

        Try

            Const sp As String = "SELECT * FROM Zona_picking_tramo WHERE IdZonaPicking = @IdZonaPicking "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdZonaPicking", pIdZonaPicking)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeZona_picking_tramo As New clsBeZona_picking_tramo

                        For Each dr As DataRow In lDataTable.Rows
                            vBeZona_picking_tramo = New clsBeZona_picking_tramo()
                            Cargar(vBeZona_picking_tramo, dr)
                            lReturnList.Add(vBeZona_picking_tramo)
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

    Public Shared Function Insertar_Lista(ByVal pIdZonaPicking As Integer, ByRef lBeZona_picking_tramo As List(Of clsBeZona_picking_tramo)) As Boolean

        Insertar_Lista = False

        Dim lTramosPorZonaPickingOP As New List(Of clsBeOperador_zona_picking_tramo)
        Dim BeZonaPickingTramoNueva As New clsBeOperador_zona_picking_tramo()

        Try

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim lOpZonaPickTramo As New List(Of clsBeOperador_zona_picking_tramo)
                    lOpZonaPickTramo = clsLnOperador_zona_picking_tramo.Get_All_By_IdZonaPicking(pIdZonaPicking,
                                                                                                 lConnection,
                                                                                                 lTransaction)

                    Dim vMaxId As Integer = MaxID(lConnection, lTransaction) + 1
                    Dim vMaxIdOp As Integer = clsLnOperador_zona_picking_tramo.MaxID(lConnection, lTransaction) + 1

                    For Each zpt In lBeZona_picking_tramo

                        zpt.IdZonaPickingTramo = vMaxId
                        Insertar(zpt, lConnection, lTransaction)
                        vMaxId += 1

#Region "Actualizar automáticamente la zona de picking del operador si fue previamente asociada por día."

                        If Not lOpZonaPickTramo Is Nothing Then

                            If lOpZonaPickTramo.Count > 0 Then

                                lTramosPorZonaPickingOP = lOpZonaPickTramo.FindAll(Function(x) x.IdZonaPicking = pIdZonaPicking AndAlso x.IdZonaPickingTramo = zpt.IdZonaPickingTramo)

                                '#EJC20220601:Validamos si ya existe el tramo asociado al operador?
                                If Not lTramosPorZonaPickingOP Is Nothing Then

                                    If lTramosPorZonaPickingOP.Count = 0 Then

                                        lTramosPorZonaPickingOP = lOpZonaPickTramo.FindAll(Function(x) x.IdZonaPicking = pIdZonaPicking)

                                        Dim vDiasAsignadosPorZona = From c In lTramosPorZonaPickingOP Select New With {Key c.Dia_semana, c.IdOperador} Distinct.ToList()

                                        If Not vDiasAsignadosPorZona Is Nothing Then

                                            For Each DiaAsignadoZonaPicking In vDiasAsignadosPorZona

                                                BeZonaPickingTramoNueva = New clsBeOperador_zona_picking_tramo()
                                                BeZonaPickingTramoNueva.IdZonaPickingTramoOperador = vMaxIdOp
                                                BeZonaPickingTramoNueva.IdZonaPickingTramo = zpt.IdZonaPickingTramo
                                                BeZonaPickingTramoNueva.IdZonaPicking = pIdZonaPicking
                                                BeZonaPickingTramoNueva.IdOperador = DiaAsignadoZonaPicking.IdOperador
                                                BeZonaPickingTramoNueva.Dia_semana = DiaAsignadoZonaPicking.Dia_semana
                                                BeZonaPickingTramoNueva.User_agr = zpt.User_agr
                                                BeZonaPickingTramoNueva.Fec_agr = Now
                                                BeZonaPickingTramoNueva.User_mod = zpt.User_agr
                                                BeZonaPickingTramoNueva.Fec_mod = Now
                                                BeZonaPickingTramoNueva.Activo = True
                                                clsLnOperador_zona_picking_tramo.Insertar(BeZonaPickingTramoNueva, lConnection, lTransaction)
                                                vMaxIdOp += 1

                                            Next

                                        End If

                                    End If

                                End If

                            End If

                        End If
#End Region
                    Next

                    Insertar_Lista = True

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdZonaPickingTramo),0) FROM Zona_picking_tramo"

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

    Public Shared Function Get_All_VW_Zona_Picking_Tramo_By_IdZonaPicking(ByVal pIdZonaPicking As Integer) As DataTable

        Get_All_VW_Zona_Picking_Tramo_By_IdZonaPicking = Nothing

        Try

            Const sp As String = "SELECT IdZonaPickingTramo, Area, Sector, Tramo, min_x as X1,max_x as X2, min_y as Y1, max_y as Y2  
								  FROM VW_Zona_Picking_Tramo WHERE IdZonaPicking = @IdZonaPicking "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdZonaPicking", pIdZonaPicking)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Get_All_VW_Zona_Picking_Tramo_By_IdZonaPicking = lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdZonaPickingTramo(ByVal pIdZonaPickingTramo As Integer) As clsBeZona_picking_tramo

        Get_Single_By_IdZonaPickingTramo = Nothing

        Try

            Const sp As String = "SELECT * FROM Zona_picking_tramo Where(IdZonaPickingTramo = @IdZonaPickingTramo)"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdZonaPickingTramo", pIdZonaPickingTramo)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeZona_picking_tramo As New clsBeZona_picking_tramo

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeZona_picking_tramo, lDataTable.Rows(0))
                            Get_Single_By_IdZonaPickingTramo = vBeZona_picking_tramo
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

    Public Shared Function Get_All_By_IdZonaPickingTramo(ByVal pIdZonaPickingTramo As Integer) As List(Of clsBeZona_picking_tramo)

        Dim lReturnList As New List(Of clsBeZona_picking_tramo)

        Try

            Const sp As String = "SELECT * FROM Zona_picking_tramo WHERE IdZonaPickingTramo = @IdZonaPickingTramo "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdZonaPickingTramo", pIdZonaPickingTramo)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeZona_picking_tramo As New clsBeZona_picking_tramo

                        For Each dr As DataRow In lDataTable.Rows
                            vBeZona_picking_tramo = New clsBeZona_picking_tramo()
                            Cargar(vBeZona_picking_tramo, dr)
                            lReturnList.Add(vBeZona_picking_tramo)
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

    Public Shared Function Get_All_By_IdZonaPickingTramo(ByVal pIdZonaPickingTramo As Integer,
                                                         ByVal lConnection As SqlConnection,
                                                         ByVal lTransaction As SqlTransaction) As List(Of clsBeZona_picking_tramo)

        Dim lReturnList As New List(Of clsBeZona_picking_tramo)

        Get_All_By_IdZonaPickingTramo = Nothing

        Try

            Const sp As String = "SELECT b.* FROM Operador_zona_picking_tramo a 
								  INNER JOIN zona_picking_tramo b on a.IdZonaPickingTramo = b.IdZonaPickingTramo
								  AND a.IdZonaPicking = b.IdZonaPicking
								  INNER JOIN zona_picking c on c.IdZonaPicking = b.IdZonaPicking 
								  WHERE a.IdZonaPickingTramo = @IdZonaPickingTramo "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdZonaPickingTramo", pIdZonaPickingTramo)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeZona_picking_tramo As New clsBeZona_picking_tramo

                For Each dr As DataRow In lDataTable.Rows
                    vBeZona_picking_tramo = New clsBeZona_picking_tramo()
                    Cargar(vBeZona_picking_tramo, dr)
                    lReturnList.Add(vBeZona_picking_tramo)
                Next

                Get_All_By_IdZonaPickingTramo = lReturnList

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function Get_All_By_IdEmpresa_And_IdBodega(ByVal pIdEmpresa As Integer,
                                                             ByVal pIdBodega As Integer,
                                                             ByVal lConnection As SqlConnection,
                                                             ByVal lTransaction As SqlTransaction) As List(Of clsBeZona_picking_tramo)

        Dim lReturnList As New List(Of clsBeZona_picking_tramo)

        Get_All_By_IdEmpresa_And_IdBodega = Nothing

        Try

            Const sp As String = "SELECT c.IdEmpresa, b.* FROM Operador_zona_picking_tramo a 
								  INNER JOIN zona_picking_tramo b on a.IdZonaPickingTramo = b.IdZonaPickingTramo
								  AND a.IdZonaPicking = b.IdZonaPicking
								  INNER JOIN zona_picking c on c.IdZonaPicking = b.IdZonaPicking
								  WHERE b.IdEmpresa = @IdEmpresa AND c.IdBodega = @IdBodega "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdEmpresa", pIdEmpresa)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeZona_picking_tramo As New clsBeZona_picking_tramo

                For Each dr As DataRow In lDataTable.Rows
                    vBeZona_picking_tramo = New clsBeZona_picking_tramo()
                    Cargar(vBeZona_picking_tramo, dr)
                    lReturnList.Add(vBeZona_picking_tramo)
                Next

                Get_All_By_IdEmpresa_And_IdBodega = lReturnList

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdEmpresa_And_IdBodega(ByVal pIdEmpresa As Integer,
                                                             ByVal pIdBodega As Integer) As List(Of clsBeZona_picking_tramo)

        Dim lReturnList As New List(Of clsBeZona_picking_tramo)

        Get_All_By_IdEmpresa_And_IdBodega = Nothing

        Try

            Const sp As String = "SELECT c.IdEmpresa, b.* FROM Operador_zona_picking_tramo a 
								  INNER JOIN zona_picking_tramo b on a.IdZonaPickingTramo = b.IdZonaPickingTramo
								  AND a.IdZonaPicking = b.IdZonaPicking
								  INNER JOIN zona_picking c on c.IdZonaPicking = b.IdZonaPicking
								  WHERE IdEmpresa = @IdEmpresa AND IdBodega = @IdBodega "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdEmpresa", pIdEmpresa)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeZona_picking_tramo As New clsBeZona_picking_tramo

                        For Each dr As DataRow In lDataTable.Rows
                            vBeZona_picking_tramo = New clsBeZona_picking_tramo()
                            Cargar(vBeZona_picking_tramo, dr)
                            lReturnList.Add(vBeZona_picking_tramo)
                        Next

                        Get_All_By_IdEmpresa_And_IdBodega = lReturnList

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
