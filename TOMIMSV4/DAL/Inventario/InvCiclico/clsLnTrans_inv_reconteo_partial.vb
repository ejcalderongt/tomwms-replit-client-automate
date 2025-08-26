Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnTrans_inv_reconteo

    Public Shared Function ObtenerByUbicacion(ByRef oBeTrans_inv_reconteo As clsBeTrans_inv_reconteo) As Boolean

        Try

            Const sp As String = "SELECT * FROM Trans_inv_reconteo 
                                  Where(fec_agr=(SELECT MAX(fec_agr) 
                                                  FROM trans_inv_reconteo 
                                                  WHERE idinventarioenc = @idinventarioenc 
                                                  AND IdOperador=@IdOperador 
                                                  AND IdUbicacionAnterior=@IdUbicacionAnterior 
                                                  AND IdProductoBodega=@IdProductoBodega))
                                  AND IdUbicacionAnterior=@IdUbicacionAnterior"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_reconteo.Idinventarioenc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeTrans_inv_reconteo.IdOperador))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUBICACIONANTERIOR", oBeTrans_inv_reconteo.IdUbicacionAnterior))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_inv_reconteo.IdProductoBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_inv_reconteo, dt.Rows(0))
            Else
                Return False
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    'Public Shared Function Obtener_By_Ubicacion(ByRef oBeTrans_inv_reconteo As clsBeTrans_inv_reconteo,
    '                                          ByRef lConnection As SqlConnection,
    '                                          ByRef lTransaction As SqlTransaction) As Boolean

    '    Try

    '        Const sp As String = "SELECT * FROM Trans_inv_reconteo 
    '                              Where(fec_agr=(SELECT MAX(fec_agr) 
    '                                              FROM trans_inv_reconteo 
    '                                              WHERE idinventarioenc = @idinventarioenc 
    '                                              AND IdOperador=@IdOperador 
    '                                              AND IdUbicacionAnterior=@IdUbicacionAnterior 
    '                                              AND IdProductoBodega=@IdProductoBodega))
    '                              AND IdUbicacionAnterior=@IdUbicacionAnterior"

    '        Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
    '        Dim dad As New SqlDataAdapter(cmd)

    '        dad.SelectCommand.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_reconteo.Idinventarioenc))
    '        dad.SelectCommand.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeTrans_inv_reconteo.IdOperador))
    '        dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUBICACIONANTERIOR", oBeTrans_inv_reconteo.IdUbicacionAnterior))
    '        dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_inv_reconteo.IdProductoBodega))

    '        Dim dt As New DataTable
    '        dad.Fill(dt)

    '        If dt.Rows.Count = 1 Then
    '            Cargar(oBeTrans_inv_reconteo, dt.Rows(0))
    '        Else
    '            Return False
    '        End If

    '        Return True

    '    Catch ex As Exception
    '        Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
    '        clsLnLog_error_wms.Agregar_Error(vMsgError)
    '        Throw ex
    '    End Try

    'End Function    


    Public Shared Function GetAllByStockUbic(ByVal pIdInventario As Integer, ByVal pIdStock As Integer, ByVal pIdUbicacion As Integer, ByVal pIdReconteoEnc As Integer) As List(Of clsBeTrans_inv_reconteo)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_reconteo)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM trans_inv_reconteo
                WHERE    (trans_inv_reconteo.idinventarioenc = @idinventario AND trans_inv_reconteo.IdStock = @IdStock AND trans_inv_reconteo.IdUbicacion = @IdUbicacion AND trans_inv_reconteo.idreconteo = @IdReconteoEnc)"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@idinventario", pIdInventario)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdStock", pIdStock)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdReconteoEnc", pIdReconteoEnc)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeTrans_inv_reconteo

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeTrans_inv_reconteo()
                            Cargar(Obj, lRow)
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

    Public Shared Sub Guardar_Reconteo(enc As clsBeTrans_inv_enc_reconteo, items As List(Of clsBeTrans_inv_reconteo))

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open()

            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            clsLnTrans_inv_enc_reconteo.Insertar(enc, lConnection, lTransaction)

            For Each Obj As clsBeTrans_inv_reconteo In items

                Insertar(Obj, lConnection, lTransaction)

            Next

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Sub

    Public Shared Function Get_All_By_Item_Inv(ByVal pitem As clsBeTrans_inv_ciclico_vw,
                                                 ByRef lConnection As SqlConnection,
                                                 ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_inv_reconteo)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_reconteo)


            Dim vSQL As String = "SELECT  *
                        FROM  trans_inv_reconteo 
                        WHERE IdProductoBodega = @IdProductoBodega AND IdUbicacion  = @IdUbicacion 
                              AND Lote = @Lote  AND CONVERT(DATE, Fecha_Vence) = CONVERT(DATE, @Fecha_Vence)
                              AND IdInventarioEnc = @IdInventarioEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pitem.IdProductoBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pitem.IdUbicacion)
                lDTA.SelectCommand.Parameters.AddWithValue("@Lote", pitem.Lote_stock)
                lDTA.SelectCommand.Parameters.AddWithValue("@Fecha_Vence", pitem.Fecha_vence_stock)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdInventarioEnc", pitem.Idinventarioenc)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_inv_reconteo

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_inv_reconteo()

                        Cargar(Obj, lRow)

                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_Item_Inv(ByVal pitem As clsBeTrans_inv_ciclico,
                                                 ByRef lConnection As SqlConnection,
                                                 ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_inv_reconteo)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_reconteo)


            Dim vSQL As String = "SELECT  *
                        FROM  trans_inv_reconteo 
                        WHERE IdProductoBodega = @IdProductoBodega AND IdUbicacion  = @IdUbicacion 
                              AND Lote = @Lote  AND CONVERT(DATE, Fecha_Vence) = CONVERT(DATE, @Fecha_Vence)
                              AND IdInventarioEnc = @IdInventarioEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pitem.IdProductoBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pitem.IdUbicacion)
                lDTA.SelectCommand.Parameters.AddWithValue("@Lote", pitem.Lote_stock)
                lDTA.SelectCommand.Parameters.AddWithValue("@Fecha_Vence", pitem.Fecha_vence_stock)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdInventarioEnc", pitem.Idinventarioenc)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_inv_reconteo

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_inv_reconteo()

                        Cargar(Obj, lRow)

                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener_By_Ubicacion(ByRef oBeTrans_inv_reconteo As clsBeTrans_inv_reconteo) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Obtener_By_Ubicacion = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM Trans_inv_reconteo 
                                  Where(fec_agr=(SELECT MAX(fec_agr) 
                                                  FROM trans_inv_reconteo 
                                                  WHERE idinventarioenc = @idinventarioenc 
                                                  AND IdOperador=@IdOperador 
                                                  AND IdUbicacionAnterior=@IdUbicacionAnterior 
                                                  AND IdProductoBodega=@IdProductoBodega))
                                  AND IdUbicacionAnterior=@IdUbicacionAnterior"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_reconteo.Idinventarioenc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeTrans_inv_reconteo.IdOperador))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUBICACIONANTERIOR", oBeTrans_inv_reconteo.IdUbicacionAnterior))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_inv_reconteo.IdProductoBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_inv_reconteo, dt.Rows(0))
                Obtener_By_Ubicacion = True
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Obtener_By_Ubicacion(ByRef oBeTrans_inv_reconteo As clsBeTrans_inv_reconteo,
                                              ByRef lConnection As SqlConnection,
                                              ByRef lTransaction As SqlTransaction) As Boolean

        Try

            Const sp As String = "SELECT * FROM Trans_inv_reconteo 
                                  Where(fec_agr=(SELECT MAX(fec_agr) 
                                                  FROM trans_inv_reconteo 
                                                  WHERE idinventarioenc = @idinventarioenc 
                                                  AND IdOperador=@IdOperador 
                                                  AND IdUbicacionAnterior=@IdUbicacionAnterior 
                                                  AND IdProductoBodega=@IdProductoBodega))
                                  AND IdUbicacionAnterior=@IdUbicacionAnterior"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_reconteo.Idinventarioenc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeTrans_inv_reconteo.IdOperador))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUBICACIONANTERIOR", oBeTrans_inv_reconteo.IdUbicacionAnterior))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_inv_reconteo.IdProductoBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_inv_reconteo, dt.Rows(0))
            Else
                Return False
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
