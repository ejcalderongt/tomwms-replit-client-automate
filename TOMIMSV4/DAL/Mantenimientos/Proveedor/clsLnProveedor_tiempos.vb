Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnProveedor_tiempos

    Public Shared Sub Cargar(ByRef oBeProveedor_tiempos As clsBeProveedor_tiempos, ByRef dr As DataRow)
        Try
            With oBeProveedor_tiempos
                .IdTiempoproveedor = IIf(IsDBNull(dr.Item("IdTiempoproveedor")), 0, dr.Item("IdTiempoproveedor"))
                .Idproveedor = IIf(IsDBNull(dr.Item("Idproveedor")), 0, dr.Item("Idproveedor"))
                .IdFamilia = IIf(IsDBNull(dr.Item("IdFamilia")), 0, dr.Item("IdFamilia"))
                .IdClasificacion = IIf(IsDBNull(dr.Item("IdClasificacion")), 0, dr.Item("IdClasificacion"))
                .Dias_Local = IIf(IsDBNull(dr.Item("Dias_Local")), 0, dr.Item("Dias_Local"))
                .Dias_Exterior = IIf(IsDBNull(dr.Item("Dias_Exterior")), 0, dr.Item("Dias_Exterior"))
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
    Public Shared Sub Cargar(ByRef oBeProveedor_tiempos As clsBeProveedor_tiempos,
                                 ByRef dr As DataRow,
                                 ByRef lConnection As SqlConnection,
                                 ByRef lTransaction As SqlTransaction)

        Try

            With oBeProveedor_tiempos

                .IdTiempoProveedor = IIf(IsDBNull(dr.Item("IdTiempoproveedor")), 0, dr.Item("IdTiempoproveedor"))
                .IdProveedor = IIf(IsDBNull(dr.Item("Idproveedor")), 0, dr.Item("Idproveedor"))
                .IdFamilia = IIf(IsDBNull(dr.Item("IdFamilia")), 0, dr.Item("IdFamilia"))
                .IdClasificacion = IIf(IsDBNull(dr.Item("IdClasificacion")), 0, dr.Item("IdClasificacion"))
                .Dias_Local = IIf(IsDBNull(dr.Item("Dias_Local")), 0, dr.Item("Dias_Local"))
                .Dias_Exterior = IIf(IsDBNull(dr.Item("Dias_Exterior")), 0, dr.Item("Dias_Exterior"))
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
    Public Shared Function Insertar(ByRef oBeProveedor_tiempos As clsBeProveedor_tiempos, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("proveedor_tiempos")
            Ins.Add("idtiempoproveedor", "@idtiempoproveedor", DataType.Parametro)
            Ins.Add("idproveedor", "@idproveedor", DataType.Parametro)
            If Not oBeProveedor_tiempos.IdFamilia = 0 Then Ins.Add("idfamilia", "@idfamilia", DataType.Parametro)
            If Not oBeProveedor_tiempos.IdClasificacion = 0 Then Ins.Add("idclasificacion", "@idclasificacion", DataType.Parametro)
            Ins.Add("dias_local", "@dias_local", DataType.Parametro)
            Ins.Add("dias_exterior", "@dias_exterior", DataType.Parametro)
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

            cmd.Parameters.Add(New SqlParameter("@IDTIEMPOPROVEEDOR", oBeProveedor_tiempos.IdTiempoproveedor))
            cmd.Parameters.Add(New SqlParameter("@IDPROVEEDOR", oBeProveedor_tiempos.IdProveedor))
            If Not oBeProveedor_tiempos.IdClasificacion = 0 Then cmd.Parameters.Add(New SqlParameter("@IDCLASIFICACION", oBeProveedor_tiempos.IdClasificacion))
            If Not oBeProveedor_tiempos.IdFamilia = 0 Then cmd.Parameters.Add(New SqlParameter("@IDFAMILIA", oBeProveedor_tiempos.IdFamilia))
            cmd.Parameters.Add(New SqlParameter("@DIAS_LOCAL", oBeProveedor_tiempos.Dias_Local))
            cmd.Parameters.Add(New SqlParameter("@DIAS_EXTERIOR", oBeProveedor_tiempos.Dias_Exterior))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProveedor_tiempos.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProveedor_tiempos.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProveedor_tiempos.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProveedor_tiempos.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProveedor_tiempos.Activo))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function
    Public Shared Function Actualizar(ByRef oBeProveedor_tiempos As clsBeProveedor_tiempos, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("proveedor_tiempos")
            Upd.Add("idtiempoproveedor", "@idtiempoproveedor", DataType.Parametro)
            Upd.Add("idproveedor", "@idproveedor", DataType.Parametro)
            Upd.Add("idfamilia", "@idfamilia", DataType.Parametro)
            Upd.Add("idclasificacion", "@idclasificacion", DataType.Parametro)
            Upd.Add("dias_local", "@dias_local", DataType.Parametro)
            Upd.Add("dias_exterior", "@dias_exterior", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("IdTiempoproveedor = @IdTiempoproveedor")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTIEMPOPROVEEDOR", oBeProveedor_tiempos.IdTiempoproveedor))
            cmd.Parameters.Add(New SqlParameter("@IDPROVEEDOR", oBeProveedor_tiempos.Idproveedor))
            cmd.Parameters.Add(New SqlParameter("@IDFAMILIA", oBeProveedor_tiempos.IdFamilia))
            cmd.Parameters.Add(New SqlParameter("@IDCLASIFICACION", oBeProveedor_tiempos.IdClasificacion))
            cmd.Parameters.Add(New SqlParameter("@DIAS_LOCAL", oBeProveedor_tiempos.Dias_Local))
            cmd.Parameters.Add(New SqlParameter("@DIAS_EXTERIOR", oBeProveedor_tiempos.Dias_Exterior))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProveedor_tiempos.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProveedor_tiempos.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProveedor_tiempos.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProveedor_tiempos.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProveedor_tiempos.Activo))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function
    Public Shared Function MaxID(ByRef lConnection As SqlConnection,
                                 ByRef lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdTiempoproveedor),0) FROM Proveedor_tiempos"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If
            End Using

            Return lMax

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function
    Public Shared Function Get_All_Tiempos_By_IdCliente(ByVal pIdProveedor As Integer) As List(Of clsBeProveedor_tiempos)

        Dim lReturnList As New List(Of clsBeProveedor_tiempos)

        '#HS 07112017 Quité query dentro de SqlDataAdapter.
        Dim vSQL As String = "SELECT * FROM VW_TiempoProveedor WHERE activo=1 AND IdProveedor=@pIdProveedor"

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                '#HS20171023_1630pm: Quité String.Format.
                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@pIdProveedor", pIdProveedor)

                    Dim lDT As New DataTable()
                    lDTA.Fill(lDT)

                    Dim Obj As clsBeProveedor_tiempos

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDT.Rows

                            Obj = New clsBeProveedor_tiempos()
                            Obj.Familia = New clsBeProducto_familia()
                            Obj.Clasificacion = New clsBeProducto_clasificacion()
                            Cargar(Obj, lRow)

                            If lRow("Familia") IsNot DBNull.Value AndAlso lRow("Familia") IsNot Nothing Then
                                Obj.Familia.Nombre = CType(lRow("Familia"), String)
                            End If

                            If lRow("Clasificación") IsNot DBNull.Value AndAlso lRow("Clasificación") IsNot Nothing Then
                                Obj.Clasificacion.Nombre = CType(lRow("Clasificación"), String)
                            End If

                            lReturnList.Add(Obj)

                        Next

                    End If

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Shared Function Get_All_Tiempos_By_IdProveedor(ByVal pIdProveedor As Integer,
                                                           ByRef lConnection As SqlConnection,
                                                          ByRef lTransaction As SqlTransaction) As List(Of clsBeProveedor_tiempos)

        Dim lReturnList As List(Of clsBeProveedor_tiempos) = Nothing

        Dim vSQL As String = "SELECT * FROM VW_TiempoProveedor WHERE activo=1 AND IdProveedor=@pIdProveedor"

        Try

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@pIdProveedor", pIdProveedor)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                Dim BeProveedorTiempos As clsBeProveedor_tiempos

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    lReturnList = New List(Of clsBeProveedor_tiempos)

                    For Each lRow As DataRow In lDT.Rows

                        BeProveedorTiempos = New clsBeProveedor_tiempos()
                        BeProveedorTiempos.Familia = New clsBeProducto_familia()
                        BeProveedorTiempos.Clasificacion = New clsBeProducto_clasificacion()
                        Cargar(BeProveedorTiempos, lRow)

                        If lRow("Familia") IsNot DBNull.Value AndAlso lRow("Familia") IsNot Nothing Then
                            BeProveedorTiempos.Familia.Nombre = CType(lRow("Familia"), String)
                        End If

                        If lRow("Clasificación") IsNot DBNull.Value AndAlso lRow("Clasificación") IsNot Nothing Then
                            BeProveedorTiempos.Clasificacion.Nombre = CType(lRow("Clasificación"), String)
                        End If

                        lReturnList.Add(BeProveedorTiempos)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Shared Function Obtener(ByRef oBeProveedor_tiempos As clsBeProveedor_tiempos,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As Boolean

        Obtener = False

        Try

            Dim sp As String = "SELECT * FROM Proveedor_tiempos" &
            " Where(IdProveedor = @IdProveedor)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPROVEEDOR", oBeProveedor_tiempos.IdProveedor))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeProveedor_tiempos, dt.Rows(0), lConnection, lTransaction)
                Obtener = True
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
