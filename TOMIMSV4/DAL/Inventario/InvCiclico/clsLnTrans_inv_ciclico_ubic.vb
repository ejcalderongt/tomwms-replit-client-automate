Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_inv_ciclico_ubic

    Public Shared Sub Cargar(ByRef oBeTrans_inv_ciclico_ubic As clsBeTrans_inv_ciclico_ubic, ByRef dr As DataRow)
        Try
            With oBeTrans_inv_ciclico_ubic
                .Idinventarioenc = IIf(IsDBNull(dr.Item("idinventarioenc")), 0, dr.Item("idinventarioenc"))
                .Idubicacion = IIf(IsDBNull(dr.Item("idubicacion")), 0, dr.Item("idubicacion"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_inv_ciclico_ubic As clsBeTrans_inv_ciclico_ubic, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_inv_ciclico_ubic")
            Ins.Add("idinventarioenc", "@idinventarioenc", DataType.Parametro)
            Ins.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)

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

            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_ciclico_ubic.Idinventarioenc))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeTrans_inv_ciclico_ubic.Idubicacion))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_inv_ciclico_ubic.IdBodega))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

            oBeTrans_inv_ciclico_ubic.Idinventarioenc = CInt(cmd.Parameters("@IDINVENTARIOENC").Value)

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeTrans_inv_ciclico_ubic As clsBeTrans_inv_ciclico_ubic, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_inv_ciclico_ubic")
            Upd.Add("idinventarioenc", "@idinventarioenc", DataType.Parametro)
            Upd.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Upd.Where("idinventarioenc = @idinventarioenc AND idubicacion = @idubicacion")

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

            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_ciclico_ubic.Idinventarioenc))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeTrans_inv_ciclico_ubic.Idubicacion))

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

    Public Shared Function Eliminar(ByRef oBeTrans_inv_ciclico_ubic As clsBeTrans_inv_ciclico_ubic, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " DELETE FROM Trans_inv_ciclico_ubic
                                   WHERE (idinventarioenc = @idinventarioenc)
                                     AND (idubicacion = @idubicacion)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_ciclico_ubic.Idinventarioenc))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeTrans_inv_ciclico_ubic.Idubicacion))

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

    Public Shared Function GetSingle(ByRef pBeTrans_inv_ciclico_ubic As clsBeTrans_inv_ciclico_ubic)

        Try

            Const sp As String = "SELECT * FROM Trans_inv_ciclico_ubic" &
            " Where(idinventarioenc = @idinventarioenc)" &
            " AND (idubicacion = @idubicacion)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", pBeTrans_inv_ciclico_ubic.Idinventarioenc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUBICACION", pBeTrans_inv_ciclico_ubic.Idubicacion))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTrans_inv_ciclico_ubic, dt.Rows(0))
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

            Const sp As String = "SELECT ISNULL(Max(idinventarioenc),0) FROM Trans_inv_ciclico_ubic"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
                Using lCommand As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CInt(lReturnValue)
                    End If
                End Using
            End Using

            Return lMax

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdInventarioEnc(ByVal pIdInv As Integer, ByVal IdBodega As Integer) As List(Of clsBeTrans_inv_ciclico_ubic)

        Dim lReturnList As New List(Of clsBeTrans_inv_ciclico_ubic)

        Try

            Dim vSQL As String = "SELECT dbo.bodega_area.IdArea, dbo.bodega_area.Descripcion AS Area, dbo.bodega_sector.IdSector, dbo.bodega_sector.descripcion AS Sector, dbo.bodega_tramo.IdTramo, dbo.bodega_tramo.descripcion AS Tramo, 
                                         dbo.bodega_ubicacion.IdUbicacion, dbo.Nombre_Completo_Ubicacion(dbo.bodega_ubicacion.IdUbicacion, dbo.bodega_ubicacion.IdBodega) AS Ubicacion, dbo.trans_inv_ciclico_ubic.idinventarioenc, 
                                         dbo.bodega_ubicacion.IdBodega
                                    FROM dbo.bodega_tramo INNER JOIN
                                         dbo.bodega_sector ON dbo.bodega_tramo.IdSector = dbo.bodega_sector.IdSector AND dbo.bodega_tramo.IdBodega = dbo.bodega_tramo.IdBodega INNER JOIN
                                         dbo.bodega_ubicacion ON dbo.bodega_tramo.IdTramo = dbo.bodega_ubicacion.IdTramo AND dbo.bodega_tramo.IdBodega = dbo.bodega_ubicacion.IdBodega INNER JOIN
                                         dbo.bodega_area ON dbo.bodega_sector.IdArea = dbo.bodega_area.IdArea AND dbo.bodega_sector.IdBodega = dbo.bodega_area.IdBodega INNER JOIN
                                         dbo.trans_inv_ciclico_ubic ON dbo.bodega_ubicacion.IdUbicacion = dbo.trans_inv_ciclico_ubic.idubicacion AND dbo.bodega_ubicacion.IdBodega = dbo.trans_inv_ciclico_ubic.IdBodega
                                 WHERE trans_inv_ciclico_ubic.idinventarioenc = @idinventarioenc AND trans_inv_ciclico_ubic.IdBodega = @IdBodega"
            vSQL += " ORDER BY IdUbicacion "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@idinventarioenc", pIdInv)
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Dim BeTransInvCiclicoUbic As New clsBeTrans_inv_ciclico_ubic()

                                If lRow("Area") IsNot DBNull.Value AndAlso lRow("Area") IsNot Nothing Then
                                    BeTransInvCiclicoUbic.Area = CType(lRow("Area"), String)
                                End If

                                If lRow("Sector") IsNot DBNull.Value AndAlso lRow("Sector") IsNot Nothing Then
                                    BeTransInvCiclicoUbic.Sector = CType(lRow("Sector"), String)
                                End If

                                If lRow("Tramo") IsNot DBNull.Value AndAlso lRow("Tramo") IsNot Nothing Then
                                    BeTransInvCiclicoUbic.Tramo = CType(lRow("Tramo"), String)
                                End If

                                If lRow("Ubicacion") IsNot DBNull.Value AndAlso lRow("Ubicacion") IsNot Nothing Then
                                    BeTransInvCiclicoUbic.Ubicacion = CType(lRow("Ubicacion"), String)
                                End If

                                If lRow("IdUbicacion") IsNot DBNull.Value AndAlso lRow("IdUbicacion") IsNot Nothing Then
                                    BeTransInvCiclicoUbic.Idubicacion = CType(lRow("IdUbicacion"), String)
                                End If

                                If lRow("IdBodega") IsNot DBNull.Value AndAlso lRow("IdBodega") IsNot Nothing Then
                                    BeTransInvCiclicoUbic.IdBodega = CType(lRow("IdBodega"), String)
                                End If

                                lReturnList.Add(BeTransInvCiclicoUbic)

                            Next

                        End If

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

    Public Shared Function Get_All_By_IdUbicacion(ByVal pIdUbicacion As Integer,
                                                                 ByVal pIdInventario As Integer) As DataTable

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT trans_inv_operador.idubic, trans_inv_operador.idinventarioenc,operador.nombres as Nombre, operador.apellidos
                        FROM trans_inv_operador INNER JOIN
                         operador ON trans_inv_operador.idoperador = operador.IdOperador
                        WHERE trans_inv_operador.idubic =@IdUbicacion AND trans_inv_operador.idinventarioenc=@IdInventario"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdInventario", pIdInventario)

                    Dim lDataTable As New DataTable

                    lDTA.Fill(lDataTable)

                    Return lDataTable

                End Using

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_Ubicacion(ByVal pIdUbicacion As Integer,
                                            ByVal pIdInventario As Integer) As Boolean

        Existe_Ubicacion = False

        Try

            Dim vSQL As String = "SELECT * from trans_inv_ciclico_ubic 
                        WHERE trans_inv_ciclico_ubic.idubicacion =@IdUbicacion AND trans_inv_ciclico_ubic.idinventarioenc=@IdInventario"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdInventario", pIdInventario)

                        Dim lDataTable As New DataTable

                        lDTA.Fill(lDataTable)

                        Existe_Ubicacion = (lDataTable.Rows.Count > 0)

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_Ubicacion(ByVal pIdUbicacion As Integer,
                                            ByVal pIdInventario As Integer,
                                            ByRef lConnection As SqlConnection,
                                            ByRef lTransaction As SqlTransaction) As Double

        Try

            Dim vSQL As String = "SELECT * from trans_inv_ciclico_ubic 
                        WHERE trans_inv_ciclico_ubic.idubicacion =@IdUbicacion AND trans_inv_ciclico_ubic.idinventarioenc=@IdInventario"

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.CommandType = CommandType.Text
            dad.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)
            dad.SelectCommand.Parameters.AddWithValue("@IdInventario", pIdInventario)

            Dim lDataTable As New DataTable

            dad.Fill(lDataTable)

            Return lDataTable.Rows.Count > 0

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub Guardar_Ubicaciones(ByVal items As IList(Of clsBeTrans_inv_ciclico_ubic), ByVal pIdOperador As Integer)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim Operador As New clsBeTrans_inv_operador()

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim vMaxIdInvOperador As Integer = clsLnTrans_inv_operador.MaxID(lConnection, lTransaction)

            For Each Obj As clsBeTrans_inv_ciclico_ubic In items

                If Not Existe_Ubicacion(Obj.Idubicacion,
                                        Obj.Idinventarioenc,
                                        lConnection, lTransaction) Then

                    Insertar(Obj, lConnection, lTransaction)

                End If

                Operador.Idinvoperador = vMaxIdInvOperador
                Operador.Idinventarioenc = Obj.Idinventarioenc
                Operador.Idinvencreconteo = 0
                Operador.Idubic = Obj.Idubicacion
                Operador.IdBodega = Obj.IdBodega
                Operador.Idoperador = pIdOperador

                If Not clsLnTrans_inv_operador.Existe_Ubicacion_By_IdOperador(Operador, lConnection, lTransaction) Then
                    clsLnTrans_inv_operador.Insertar(Operador, lConnection, lTransaction)
                    vMaxIdInvOperador += 1
                End If

            Next

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Sub

    Public Shared Function Elimina_UbicacionesByIdUbicacion(ByVal IdInventario As Integer, ByVal IdProductoBodega As Integer, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " DELETE FROM Trans_inv_ciclico_ubic 
                                   WHERE(idinventarioenc=@idinventarioenc AND idubicacion IN
                                        (SELECT DISTINCT IdUbicacion FROM trans_inv_ciclico 
                                         WHERE idinventarioenc = @idinventarioenc AND IdProductoBodega=@IdProductoBodega AND IdUbicacion NOT IN
                                        (SELECT IdUbicacion FROM trans_inv_ciclico 
                                         WHERE idinventarioenc =@idinventarioenc AND IdProductoBodega<>@IdProductoBodega)))"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@idinventarioenc", IdInventario))
            cmd.Parameters.Add(New SqlParameter("@IdProductoBodega", IdProductoBodega))


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

    Public Shared Function Eliminar_AllByInventario(ByVal IdInventario As Integer, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_inv_ciclico_ubic" &
             "  Where(idinventarioenc=@idinventarioenc)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@idinventarioenc", IdInventario))


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

    Public Shared Function Get_All_By_IdProductoBodega(ByVal pIdProductoBodega As Integer,
                                                       ByVal pIdInventario As Integer) As DataTable

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT Distinct IdUbicacion
                                        FROM trans_inv_ciclico 
                                        WHERE IdOperador IS NULL AND IdProductoBodega =@IdProductoBodega AND 
                                              idinventarioenc=@IdInventario"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdInventario", pIdInventario)

                    Dim lDataTable As New DataTable

                    lDTA.Fill(lDataTable)

                    Return lDataTable

                End Using

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Elimina_Ubicaciones_By_IdUbicacion_And_IdOperador(ByVal IdInventario As Integer,
                                                                             ByVal IdProductoBodega As Integer,
                                                                             ByVal IdOperador As Integer,
                                                                             Optional ByVal pConection As SqlConnection = Nothing,
                                                                             Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim sp As String = " DELETE FROM Trans_inv_ciclico_ubic 
                                   WHERE idinventarioenc=@idinventarioenc 
                                         AND idubicacion IN
                                        (SELECT DISTINCT IdUbicacion FROM trans_inv_ciclico 
                                         WHERE idinventarioenc = @idinventarioenc AND IdProductoBodega=@IdProductoBodega AND IdOperador = @IdOperador) 
                                         AND IdUbicacion NOT IN
                                         (SELECT IdUbicacion FROM trans_inv_ciclico 
                                         WHERE idinventarioenc =@idinventarioenc AND IdProductoBodega=@IdProductoBodega AND IdOperador <> @IdOperador)
                                         AND IdUbicacion NOT IN
                                        (SELECT IdUbicacion FROM trans_inv_ciclico 
                                         WHERE idinventarioenc =@idinventarioenc AND IdProductoBodega<>@IdProductoBodega)  "

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@idinventarioenc", IdInventario))
            cmd.Parameters.Add(New SqlParameter("@IdProductoBodega", IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IdOperador", IdOperador))

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
    Public Shared Sub Guardar_Ubicaciones(ByVal items As IList(Of clsBeTrans_inv_ciclico_ubic), ByVal pIdOperador As Integer, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction)

        Dim Operador As New clsBeTrans_inv_operador()

        Try

            Dim vMaxIdInvOperador As Integer = clsLnTrans_inv_operador.MaxID(lConnection, lTransaction)

            For Each Obj As clsBeTrans_inv_ciclico_ubic In items

                If Not Existe_Ubicacion(Obj.Idubicacion,
                                        Obj.Idinventarioenc,
                                        lConnection, lTransaction) Then

                    Insertar(Obj, lConnection, lTransaction)

                End If

                Operador.Idinvoperador = vMaxIdInvOperador
                Operador.Idinventarioenc = Obj.Idinventarioenc
                Operador.Idinvencreconteo = 0
                Operador.Idubic = Obj.Idubicacion
                Operador.IdBodega = Obj.IdBodega
                Operador.Idoperador = pIdOperador

                If Not clsLnTrans_inv_operador.Existe_Ubicacion_By_IdOperador(Operador, lConnection, lTransaction) Then
                    clsLnTrans_inv_operador.Insertar(Operador, lConnection, lTransaction)
                    vMaxIdInvOperador += 1
                End If

            Next

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Elimina_By_IdUbicacion(ByVal IdInventario As Integer,
                                                  ByVal IdUbicacion As Integer,
                                                  Optional ByVal pConection As SqlConnection = Nothing,
                                                  Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim sp As String = " DELETE FROM Trans_inv_ciclico_ubic 
                                   WHERE idinventarioenc=@idinventarioenc 
                                         AND idubicacion = @IdUbicacion  "

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@idinventarioenc", IdInventario))
            cmd.Parameters.Add(New SqlParameter("@IdUbicacion", IdUbicacion))

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

End Class
