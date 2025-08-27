Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnTrans_inv_tramo

    Public Shared Function Get_All_Tramos_By_IdTarea(pIdTarea As Integer) As List(Of clsBeTrans_inv_tramo)

        Get_All_Tramos_By_IdTarea = Nothing

        Dim lReturnList As New List(Of clsBeTrans_inv_tramo)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Dim vSQL As String = "SELECT *
                                    FROM  trans_inv_tramo INNER JOIN
                                    bodega_tramo ON trans_inv_tramo.idtramo = bodega_tramo.IdTramo INNER JOIN
                                    trans_inv_enc ON trans_inv_tramo.idinventario = trans_inv_enc.idinventarioenc AND 
                                    bodega_tramo.IdBodega = trans_inv_enc.idbodega
                                    WHERE (trans_inv_tramo.idinventario = @IdInventario)
                                    ORDER BY trans_inv_tramo.idinventario "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        If pIdTarea > 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@IdInventario", pIdTarea)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeTrans_inv_tramo

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            For Each lRow As DataRow In lDataTable.Rows
                                Obj = New clsBeTrans_inv_tramo
                                CargarConNombre(Obj, lRow)
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

    Public Shared Sub CargarConNombre(ByRef oBeTrans_inv_tramo As clsBeTrans_inv_tramo, ByRef dr As DataRow)
        Try
            With oBeTrans_inv_tramo
                .Idinventario = IIf(IsDBNull(dr.Item("idinventario")), 0, dr.Item("idinventario"))
                .Idtramo = IIf(IsDBNull(dr.Item("idtramo")), 0, dr.Item("idtramo"))
                .Det_idoperador = IIf(IsDBNull(dr.Item("det_idoperador")), 0, dr.Item("det_idoperador"))
                .Det_estado = IIf(IsDBNull(dr.Item("det_estado")), "", dr.Item("det_estado"))
                .Det_inicio = IIf(IsDBNull(dr.Item("det_inicio")), "01/01/1900", dr.Item("det_inicio"))
                .Det_fin = IIf(IsDBNull(dr.Item("det_fin")), "01/01/1900", dr.Item("det_fin"))
                .Res_idoperador = IIf(IsDBNull(dr.Item("res_idoperador")), 0, dr.Item("res_idoperador"))
                .Res_estado = IIf(IsDBNull(dr.Item("res_estado")), "", dr.Item("res_estado"))
                .Res_inicio = IIf(IsDBNull(dr.Item("res_inicio")), "01/01/1900", dr.Item("res_inicio"))
                .Res_fin = IIf(IsDBNull(dr.Item("res_fin")), "01/01/1900", dr.Item("res_fin"))
                .Aplicado = IIf(IsDBNull(dr.Item("aplicado")), False, dr.Item("aplicado"))
                .Nombre_Tramo = IIf(IsDBNull(dr.Item("descripcion")), False, dr.Item("descripcion"))
                .Es_Rack = IIf(IsDBNull(dr.Item("es_rack")), False, dr.Item("es_rack"))
            End With
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function Get_Single_By_BeTramo(ByRef pBeTrans_inv_tramo As clsBeTrans_inv_tramo)

        Dim sp As String

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing


        Try

            sp = "SELECT  trans_inv_tramo.idinventario, trans_inv_tramo.idtramo, trans_inv_tramo.det_idoperador, trans_inv_tramo.det_estado, 
                        trans_inv_tramo.det_inicio, trans_inv_tramo.det_fin, trans_inv_tramo.res_idoperador, trans_inv_tramo.res_estado, trans_inv_tramo.res_inicio, 
                        trans_inv_tramo.res_fin, trans_inv_tramo.aplicado, bodega_tramo.descripcion, bodega_tramo.es_rack
                  FROM trans_inv_tramo INNER JOIN
                        bodega_tramo ON trans_inv_tramo.idtramo = bodega_tramo.IdTramo INNER JOIN
                        trans_inv_enc ON trans_inv_tramo.idinventario = trans_inv_enc.idinventarioenc AND 
                        bodega_tramo.IdBodega = trans_inv_enc.idbodega
                    WHERE (trans_inv_tramo.IdInventario=@IdInventario ) AND (trans_inv_tramo.Idtramo = @Idtramo) "

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Transaction = lTransaction

            dad.SelectCommand.Parameters.Add(New SqlParameter("@Idinventario", pBeTrans_inv_tramo.Idinventario))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@Idtramo", pBeTrans_inv_tramo.Idtramo))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                CargarConNombre(pBeTrans_inv_tramo, dt.Rows(0))
            End If

            lTransaction.Commit()

            Return True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_Single_By_BeTramo(ByRef pBeTrans_inv_tramo As clsBeTrans_inv_tramo,
                                                 ByRef pConnection As SqlConnection,
                                                 ByRef pTransaction As SqlTransaction) As Boolean


        Get_Single_By_BeTramo = False

        Dim sp As String = ""

        Try

            sp = "SELECT  trans_inv_tramo.idinventario, trans_inv_tramo.idtramo, trans_inv_tramo.det_idoperador, trans_inv_tramo.det_estado, 
                        trans_inv_tramo.det_inicio, trans_inv_tramo.det_fin, trans_inv_tramo.res_idoperador, trans_inv_tramo.res_estado, trans_inv_tramo.res_inicio, 
                        trans_inv_tramo.res_fin, trans_inv_tramo.aplicado, bodega_tramo.descripcion, bodega_tramo.es_rack
                  FROM trans_inv_tramo INNER JOIN
                        bodega_tramo ON trans_inv_tramo.idtramo = bodega_tramo.IdTramo INNER JOIN
                        trans_inv_enc ON trans_inv_tramo.idinventario = trans_inv_enc.idinventarioenc AND 
                        bodega_tramo.IdBodega = trans_inv_enc.idbodega
                    WHERE (trans_inv_tramo.IdInventario=@IdInventario ) AND (trans_inv_tramo.Idtramo = @Idtramo) "

            Dim cmd As New SqlCommand(sp, pConnection, pTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@Idinventario", pBeTrans_inv_tramo.Idinventario))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@Idtramo", pBeTrans_inv_tramo.Idtramo))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                CargarConNombre(pBeTrans_inv_tramo, dt.Rows(0))
                Get_Single_By_BeTramo = True
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar_Tramo(ByRef oBeTrans_inv_tramo As clsBeTrans_inv_tramo, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_inv_tramo")
            Upd.Add("det_idoperador", "@det_idoperador", DataType.Parametro)
            Upd.Add("det_estado", "@det_estado", DataType.Parametro)
            Upd.Add("det_inicio", "@det_inicio", DataType.Parametro)
            Upd.Add("det_fin", "@det_fin", DataType.Parametro)
            Upd.Add("res_idoperador", "@res_idoperador", DataType.Parametro)
            Upd.Add("res_estado", "@res_estado", DataType.Parametro)
            Upd.Add("res_inicio", "@res_inicio", DataType.Parametro)
            Upd.Add("res_fin", "@res_fin", DataType.Parametro)
            Upd.Add("aplicado", "@aplicado", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Where(" (trans_inv_tramo.IdInventario=@IdInventario ) AND (trans_inv_tramo.Idtramo = @Idtramo)")

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

            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIO", oBeTrans_inv_tramo.Idinventario))
            cmd.Parameters.Add(New SqlParameter("@IDTRAMO", oBeTrans_inv_tramo.Idtramo))
            cmd.Parameters.Add(New SqlParameter("@DET_IDOPERADOR", oBeTrans_inv_tramo.Det_idoperador))
            cmd.Parameters.Add(New SqlParameter("@DET_ESTADO", oBeTrans_inv_tramo.Det_estado))
            cmd.Parameters.Add(New SqlParameter("@DET_INICIO", oBeTrans_inv_tramo.Det_inicio))
            cmd.Parameters.Add(New SqlParameter("@DET_FIN", oBeTrans_inv_tramo.Det_fin))
            cmd.Parameters.Add(New SqlParameter("@RES_IDOPERADOR", oBeTrans_inv_tramo.Res_idoperador))
            cmd.Parameters.Add(New SqlParameter("@RES_ESTADO", oBeTrans_inv_tramo.Res_estado))
            cmd.Parameters.Add(New SqlParameter("@RES_INICIO", oBeTrans_inv_tramo.Res_inicio))
            cmd.Parameters.Add(New SqlParameter("@RES_FIN", oBeTrans_inv_tramo.Res_fin))
            cmd.Parameters.Add(New SqlParameter("@APLICADO", oBeTrans_inv_tramo.Aplicado))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_inv_tramo.IdBodega))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_Fecha_Inicio_By_IdInventarioEnc(pIdinventario As Integer) As Date

        Dim fecha As Date

        Try

            Dim vSQL As String = "SELECT min(det_inicio) as fecha from trans_inv_tramo where (idinventario=@IdInventario) AND (det_idoperador>0)
                        union
                        select min(res_inicio) as fecha from trans_inv_tramo where (idinventario=@IdInventario) AND (res_idoperador>0)
                        order by fecha desc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdInventario", pIdinventario)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Try
                            fecha = lDataTable.Rows(0).Item(0)
                        Catch ex As Exception
                            fecha = New DateTime(1900, 1, 1)
                        End Try

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return fecha

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
