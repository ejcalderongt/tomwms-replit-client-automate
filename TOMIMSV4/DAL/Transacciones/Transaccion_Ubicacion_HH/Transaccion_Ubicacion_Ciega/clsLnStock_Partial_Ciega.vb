Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnStock

    Public Shared Function Get_All_UbicRecepcion_By_IdBodega(ByVal pIdBodega As Integer) As List(Of clsBeStock)

        Dim lReturnList As New List(Of clsBeStock)

        Dim vSQL As String = "Select * from stock where IdBodega = @IdBodega"

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeStock

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeStock
                                Cargar(Obj, lRow)
                                lReturnList.Add(Obj)

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

    Public Shared Function Get_Primera_Ubic_Recepcion_By_IdBodega(ByVal pIdBodega As Integer) As Integer

        Dim idUbicRecepcion = 0
        Dim vSQL As String = ""

        Try

            Dim lDTA, lDTA2 As New SqlDataAdapter
            Dim dt, dt2 As New DataTable
            Dim ubicid As Integer
            Dim cants, cantr As Double

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    vSQL = "SELECT t.IdUbicacion, sum(t.cantstock - t.cantres) as Cant
                            FROM (
                            SELECT stock.IdUbicacion, SUM(stock.cantidad) AS CantStock, 0 as CantRes
                            From stock INNER Join
                            bodega_ubicacion ON stock.IdUbicacion = bodega_ubicacion.IdUbicacion AND stock.IdBodega = bodega_ubicacion.IdBodega INNER JOIN 
                            bodega_tramo On bodega_ubicacion.IdTramo = bodega_tramo.IdTramo and bodega_ubicacion.IdBodega = bodega_tramo.IdBodega INNER JOIN 
                            bodega_sector ON bodega_tramo.IdSector = bodega_sector.IdSector and bodega_tramo.IdBodega = bodega_sector.IdBodega INNER JOIN 
                            bodega_area On bodega_sector.IdArea = bodega_area.IdArea and bodega_sector.IdBodega = bodega_area.IdBodega 
                            WHERE  (bodega_ubicacion.ubicacion_recepcion = 1) 
                            GROUP BY stock.IdUbicacion, bodega_area.IdBodega 
                            HAVING  (bodega_area.IdBodega =  @IdBodega)
                            union
                            SELECT stock_res.IdUbicacion, 0 AS CantStock, SUM(stock_res.cantidad) as CantRes
                            From stock_res INNER Join
                            bodega_ubicacion ON stock_res.IdUbicacion = bodega_ubicacion.IdUbicacion AND stock_res.IdBodega = bodega_ubicacion.IdBodega INNER JOIN 
                            bodega_tramo On bodega_ubicacion.IdTramo = bodega_tramo.IdTramo and bodega_ubicacion.IdBodega = bodega_tramo.IdBodega INNER JOIN 
                            bodega_sector ON bodega_tramo.IdSector = bodega_sector.IdSector and bodega_tramo.IdBodega = bodega_sector.IdBodega INNER JOIN 
                            bodega_area On bodega_sector.IdArea = bodega_area.IdArea and bodega_sector.IdBodega = bodega_area.IdBodega 
                            WHERE  (bodega_ubicacion.ubicacion_recepcion = 1) 
                            GROUP BY stock_res.IdUbicacion, bodega_area.IdBodega 
                            HAVING  (bodega_area.IdBodega =  @IdBodega)) AS T
                            Group by t.IdUbicacion"

                    lDTA = New SqlDataAdapter(vSQL, lConnection)
                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Transaction = lTransaction
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                    lDTA.Fill(dt)

                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                        For Each lRow As DataRow In dt.Rows

                            ubicid = lRow.Item("IdUbicacion")
                            cants = lRow.Item("Cant")

                            Try

                                vSQL = "SELECT SUM(trans_ubic_hh_det.cantidad - trans_ubic_hh_det.recibido) As total 
                                    From trans_ubic_hh_det INNER Join stock ON trans_ubic_hh_det.IdStock = stock.IdStock
                                    Group By stock.IdUbicacion HAVING (stock.IdUbicacion = @ubicid) "

                                lDTA2 = New SqlDataAdapter(vSQL, lConnection)
                                lDTA2.SelectCommand.CommandType = CommandType.Text
                                lDTA2.SelectCommand.Transaction = lTransaction
                                lDTA2.SelectCommand.Parameters.AddWithValue("@ubicid", ubicid)
                                lDTA2.Fill(dt2)

                                If dt2.Rows.Count > 0 Then
                                    cantr = dt2.Rows(0).Item("total")
                                Else
                                    cantr = 0
                                End If

                            Catch ex As Exception
                                cantr = 0
                            End Try

                            If (cants > cantr) Then Return ubicid

                        Next

                    End If

                    lTransaction.Commit()

                End Using


                lConnection.Close()

            End Using

            Return idUbicRecepcion

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message & vbCrLf & vSQL))
        End Try

    End Function

    '#EJC20171014_1140PM: Actualizar solo la cantidad para evitar que se pierdan/sobreescriban otros valores.
    Public Shared Function Actualiza_Cantidad_Y_Peso(ByRef oBeStock As clsBeStock, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("stock")
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Add("peso", "@peso", DataType.Parametro)
            Upd.Where("IdStock = @IdStock")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeStock.IdStock))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeStock.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeStock.Peso))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then
                lTransaction.Commit()
            End If

            cmd.Dispose()

            Return rowsAffected

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Actualiza_Ubicacion_Por_Picking(ByRef oBeStock As clsBeStock, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("stock")
            Upd.Add("IdUbicacion", "@IdUbicacion", DataType.Parametro)
            Upd.Add("IdUbicacion_anterior", "@IdUbicacion_anterior", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("IdStock = @IdStock")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeStock.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeStock.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION_ANTERIOR", IIf(oBeStock.IdUbicacion_anterior = 0, DBNull.Value, oBeStock.IdUbicacion_anterior)))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", Now))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar_IdUbicacion_By_IdStock(ByVal IdUbicacion As Integer,
                                                            ByVal IdUbicacionAnterior As Integer,
                                                            ByVal IdBodega As Integer,
                                                            ByVal IdStock As Integer,
                                                            Optional ByVal pConection As SqlConnection = Nothing,
                                                            Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("stock")
            Upd.Add("IdUbicacion", "@IdUbicacion", DataType.Parametro)
            Upd.Add("IdUbicacion_anterior", "@IdUbicacion_anterior", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("IdStock = @IdStock AND IdBodega = @IdBodega")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION_ANTERIOR", IdUbicacionAnterior))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", Now))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

End Class
