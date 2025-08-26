Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnProducto_pallet

    Public Shared Function MaxID(ByRef pConnection As SqlConnection,
                                 pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdPallet),0) FROM Producto_pallet"

            Using lCommand As New SqlCommand(sp, pConnection, pTransaction) With {.CommandType = CommandType.Text}

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

    Public Shared Function Guarda_Producto_Pallet(ByVal IdRecepcionEnc As Integer,
                                                  ByVal pListProductoPallet As List(Of clsBeProducto_pallet),
                                                  ByRef lConnection As SqlConnection,
                                                  ByRef lTransaction As SqlTransaction) As Integer

        Guarda_Producto_Pallet = 0

        Dim vRegistros As Integer = 0

        Try

            If Not pListProductoPallet Is Nothing Then

                If pListProductoPallet.Count > 0 Then

                    Dim lMaxIdProdPallet As Integer = MaxID(lConnection, lTransaction) + 1

                    For Each BeProdPallet As clsBeProducto_pallet In pListProductoPallet

                        If BeProdPallet.IsNew Then
                            BeProdPallet.IdPallet = lMaxIdProdPallet
                            BeProdPallet.IdRecepcionEnc = IdRecepcionEnc
                            BeProdPallet.Fecha_ingreso = Now
                            BeProdPallet.Fec_agr = Now
                            BeProdPallet.Fec_mod = Now
                            vRegistros += Insertar(BeProdPallet,
                                                   lConnection,
                                                   lTransaction)
                            lMaxIdProdPallet += 1
                        Else
                            BeProdPallet.Fec_mod = Now
                            vRegistros += Actualizar(BeProdPallet,
                                                     lConnection,
                                                     lTransaction)
                        End If

                    Next

                End If

            End If

            Guarda_Producto_Pallet = vRegistros

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdRecepcionEnc(ByVal pIdRecepcionEnc As Integer) As List(Of clsBeProducto_pallet)

        Dim lReturnList As New List(Of clsBeProducto_pallet)

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM producto_pallet WHERE IdRecepcionEnc = @IdRecepcionEnc"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeProducto_pallet

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeProducto_pallet

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

    Public Shared Function Get_All_Pendientes_De_Imprimir() As List(Of clsBeVW_Impresion_Pallet)

        Dim lReturnList As New List(Of clsBeVW_Impresion_Pallet)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM VW_Impresion_Pallet "

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeVW_Impresion_Pallet

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows
                            Obj = New clsBeVW_Impresion_Pallet
                            clsLnVW_Impresion_Pallet.Cargar(Obj, lRow)
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


    Public Shared Function Get_All_Barras_Recepcion(ByVal IdRecepcionEnc As Integer) As List(Of clsBeVW_Impresion_Pallet)

        Dim lReturnList As New List(Of clsBeVW_Impresion_Pallet)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM VW_Impresion_Pallet_Rec WHERE Rec_No = @IdRecepcionEnc "

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", IdRecepcionEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeVW_Impresion_Pallet

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows
                                Obj = New clsBeVW_Impresion_Pallet
                                clsLnVW_Impresion_Pallet.Cargar(Obj, lRow)
                                Obj.Presentacion.IdPresentacion = Obj.IdPresentacion
                                Obj.Presentacion = clsLnProducto_presentacion.GetSingle(Obj.IdPresentacion, lConnection, lTransaction)
                                If Obj.Presentacion.IdPresentacion <> 0 Then
                                    If Obj.Presentacion.EsPallet Then
                                        Obj.Producto_Cantidad = 1
                                        Obj.Producto_Cantidad_Paralela = Obj.Presentacion.CajasPorCama * Obj.Presentacion.CamasPorTarima
                                    End If
                                Else
                                    Obj.Producto_Cantidad_Paralela = Obj.Producto_Cantidad
                                End If
                                lReturnList.Add(Obj)
                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                If lConnection.State = ConnectionState.Open Then lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_Codigo_Barra(ByVal pCodigoBarraPallet As String)

        Get_Single_By_Codigo_Barra = False

        Try

            Const sp As String = "SELECT * FROM Producto_pallet" &
            " Where(codigo_barra = @codigo_barra)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@CODIGO_BARRA", pCodigoBarraPallet))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeProducto_pallet As New clsBeProducto_pallet
                Cargar(pBeProducto_pallet, dt.Rows(0))
                Return True
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_Codigo_Barra(ByVal pCodigoBarraPallet As String,
                                                      ByRef lConnection As SqlConnection,
                                                      ByRef lTransaction As SqlTransaction)

        Get_Single_By_Codigo_Barra = Nothing

        Try

            Const sp As String = "SELECT * FROM Producto_pallet" &
            " Where(codigo_barra = @codigo_barra)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@CODIGO_BARRA", pCodigoBarraPallet))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeProducto_pallet As New clsBeProducto_pallet
                Cargar(pBeProducto_pallet, dt.Rows(0))
                Return pBeProducto_pallet
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
