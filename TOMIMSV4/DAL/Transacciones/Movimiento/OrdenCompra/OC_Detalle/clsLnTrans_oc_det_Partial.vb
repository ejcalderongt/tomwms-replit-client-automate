Imports System.Reflection
Imports System.Data.SqlClient

Partial Public Class clsLnTrans_oc_det


    Public Shared Function GetByOrdenCompra(ByVal pIdOrdenCompraEnc) As List(Of clsBeTrans_oc_det)
        
        Dim lReturnList As New List(Of clsBeTrans_oc_det)
        
        
        Dim Obj3 As New clsLnProducto_presentacion
        

        Try

            'Validacion y estandarización de los datos
            Using lCnn As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim lSQL As String = String.Format("SELECT p.IdProducto,det.* FROM trans_oc_det AS det " _
                                                 & "INNER JOIN producto_bodega AS pb ON det.IdProductoBodega = pb.IdProductoBodega " _
                                                 & "INNER JOIN producto AS p ON pb.IdProducto = p.IdProducto " _
                                                 & "WHERE det.IdOrdenCompraEnc={0}", pIdOrdenCompraEnc)

                'Acceso a los datos.
                Using lDTA As New SqlDataAdapter(lSQL, lCnn)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeTrans_oc_det

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeTrans_oc_det

                            Cargar(Obj, lRow)

                            Obj.Producto.IdProducto = CType(lRow("IdProducto"), Integer)
                            clsLnProducto.Obtener(Obj.Producto)

                            If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                                Obj.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                            End If

                            If lRow("IdArancel") IsNot DBNull.Value AndAlso lRow("IdArancel") IsNot Nothing Then
                                Obj.Arancel.IdArancel = CType(lRow("IdArancel"), Integer)
                                clsLnArancel.Obtener(Obj.Arancel)
                            End If

                            If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                                Obj.Presentacion.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                                Obj3.Obtener(Obj.Presentacion)
                            End If

                            If lRow("IdUnidadMedidaBasica") IsNot DBNull.Value AndAlso lRow("IdUnidadMedidaBasica") IsNot Nothing Then
                                Obj.UnidadMedida.IdUnidadMedida = CType(lRow("IdUnidadMedidaBasica"), Integer)
                                clsLnUnidad_medida.Obtener(Obj.UnidadMedida)
                            End If

                            If lRow("IdMotivoDevolucion") IsNot DBNull.Value AndAlso lRow("IdMotivoDevolucion") IsNot Nothing Then
                                Obj.IdMotivoDevolucion = CType(lRow("IdMotivoDevolucion"), Integer)
                            End If

                            'If lRow("Factor") IsNot DBNull.Value AndAlso lRow("Factor") IsNot Nothing Then
                            '    Obj.FactorPresentacion = CType(lRow("Factor"), System.Decimal)
                            'End If

                            Obj.IsNew = False

                            Obj.ExisteEnRecepcion = clsLnTrans_re_det.NoExisteProductoBodega(Obj.IdProductoBodega)

                            lReturnList.Add(Obj)

                        Next

                    End If

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        End Try

    End Function

    Public Shared Function GetByOrdenCompra(ByVal pIdOrdenCompraEnc As Integer, ByVal pIdProductoBodega As Integer, ByVal pConnection As SqlClient.SqlConnection, ByVal pTransaction As SqlClient.SqlTransaction) As clsBeTrans_oc_det

        Dim Obj As New clsBeTrans_oc_det

        Try

            'Validacion y estandarización de los datos
            ' Using lCnn As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim lSQL As String = String.Format(String.Format("SELECT TOP 1 * FROM trans_oc_det WHERE IdOrdenCompraenc={0} And IdProductoBodega={1}", pIdOrdenCompraEnc, pIdProductoBodega))

            'Acceso a los datos.
            Using lDTA As New SqlDataAdapter(lSQL, pConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = pTransaction

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDataTable.Rows(0)
                    Obj = New clsBeTrans_oc_det

                    Cargar(Obj, lRow)

                    If lRow("IdArancel") IsNot DBNull.Value AndAlso lRow("IdArancel") IsNot Nothing Then
                        Obj.Arancel.IdArancel = CType(lRow("IdArancel"), Integer)
                    End If

                    If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                        Obj.Presentacion.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                    End If

                    If lRow("IdUnidadMedidaBasica") IsNot DBNull.Value AndAlso lRow("IdUnidadMedidaBasica") IsNot Nothing Then
                        Obj.UnidadMedida.IdUnidadMedida = CType(lRow("IdUnidadMedidaBasica"), Integer)
                    End If

                    Obj.IsNew = False

                    Return Obj

                End If
            End Using
            'End Using

            Return Nothing

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        End Try

    End Function

    Public Shared Sub Delete(ByVal pIdOrdenCompraEnc As Integer, ByVal pIdOrdenCompraDet As Integer)

        Try

            'Validacion y estandarizacion de los datos
            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                'Acceso a los datos.
                Using lCommand As New SqlCommand("DELETE FROM trans_oc_det WHERE IdOrdenCompraEnc=@IdOrdenCompraEnc And IdOrdenCompraDet=@IdOrdenCompraDet", lConnection)

                    lCommand.CommandType = CommandType.Text
                    lCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)
                    lCommand.Parameters.AddWithValue("@IdOrdenCompraDet", pIdOrdenCompraDet)

                    lConnection.Open()
                    lCommand.ExecuteNonQuery()
                    lConnection.Close()

                End Using

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        End Try

    End Sub

    Public Shared Function MaxID(ByVal pIdOrdenCompraEnc As Integer) As Integer

        Try

            Dim lMax As Integer = 0
            'Validacion y estandarizacion de los datos
            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                'Acceso a los datos.
                Using lCommand As New SqlCommand(String.Format("SELECT ISNULL(Max(IdOrdenCompraDet),0) FROM trans_oc_det WHERE IdOrdenCompraEnc={0}", pIdOrdenCompraEnc), lConnection)
                    lCommand.CommandType = CommandType.Text
                    lCommand.CommandTimeout = 200
                    lConnection.Open()
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CInt(lReturnValue)
                    End If

                End Using

            End Using

            Return lMax

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        End Try

    End Function

    ''' <summary>
    ''' Creada por Erik Calderón
    ''' </summary>
    ''' <param name="pIdOrdenCompraEnc"></param>
    ''' <returns></returns>
    Public Shared Function GetAllByOrdenCompra(ByVal pIdOrdenCompraEnc As Integer) As List(Of clsBeTrans_oc_det)

        Dim lReturnList As New List(Of clsBeTrans_oc_det)

        Try

            Using lCnn As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim lSQL As String = String.Format("SELECT IdProductoBodega, Cantidad FROM trans_oc_det WHERE IdOrdenCompraEnc={0}", pIdOrdenCompraEnc)

                Using lDTA As New SqlDataAdapter(lSQL, lCnn)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeTrans_oc_det

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeTrans_oc_det

                            If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                                Obj.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                            End If

                            If lRow("Cantidad") IsNot DBNull.Value AndAlso lRow("Cantidad") IsNot Nothing Then
                                Obj.Cantidad = CType(lRow("Cantidad"), System.Double)
                            End If

                            Obj.IsNew = False

                            lReturnList.Add(Obj)

                        Next

                    End If

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        End Try

    End Function

    Public Shared Function GetDetalleOCByIdEncIdProdBod(ByVal pIdOrdenCompraEnc As Integer, pIdProductoBodega As Integer) As List(Of clsBeTrans_oc_det)

        Dim lReturnList As New List(Of clsBeTrans_oc_det)

        Try

            Using lCnn As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim lSQL As String = String.Format("SELECT * FROM trans_oc_det WHERE IdOrdenCompraEnc={0} AND IdProductoBodega={1}", pIdOrdenCompraEnc, pIdProductoBodega)

                Using lDTA As New SqlDataAdapter(lSQL, lCnn)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeTrans_oc_det

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeTrans_oc_det
                            clsLnTrans_oc_det.Cargar(Obj, lRow)

                            Obj.IsNew = False

                            lReturnList.Add(Obj)

                        Next

                    End If

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        End Try

    End Function

End Class
