Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnStock_parametro

    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdStockParametro),0) FROM stock_parametro"

            Using lCommand As New SqlCommand(vSQL, pConnection, pTransaction) With {.CommandType = CommandType.Text}
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

    ''' <summary>
    ''' Devuelve una lista de clsBeStock_parametro para un IdStock
    ''' </summary>
    ''' <param name="pIdStock">Recibe como parámetro el IdStock (Int) de tabla Stock</param>
    ''' <returns></returns>
    ''' <remarks>ejcalderon_20160512</remarks>
    Public Shared Function Get_All_By_IdStock(ByVal pIdStock As Integer) As List(Of clsBeStock_parametro)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim sp As String = "SELECT * FROM Stock_parametro " &
            " Where(IdStock = @IdStock)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDSTOCK", pIdStock))
            Dim dt As New DataTable
            dad.Fill(dt)

            cmd.Dispose()
            dad.Dispose()

            Dim oStockParam As clsBeStock_parametro
            Dim listStockParam As New List(Of clsBeStock_parametro)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                For Each dr As DataRow In dt.Rows

                    oStockParam = New clsBeStock_parametro
                    Cargar(oStockParam, dr)
                    listStockParam.Add(oStockParam)

                Next

            End If

            lTransaction.Commit()

            Return listStockParam

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Get_All_By_IdStock(ByVal pIdStock As Integer,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As List(Of clsBeStock_parametro)

        Get_All_By_IdStock = Nothing

        Try

            Dim sp As String = "SELECT * FROM Stock_parametro " &
            " Where(IdStock = @IdStock)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDSTOCK", pIdStock))
            Dim dt As New DataTable
            dad.Fill(dt)

            cmd.Dispose()
            dad.Dispose()

            Dim oStockParam As clsBeStock_parametro
            Dim listStockParam As New List(Of clsBeStock_parametro)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                For Each dr As DataRow In dt.Rows

                    oStockParam = New clsBeStock_parametro

                    Cargar(oStockParam, dr)

                    listStockParam.Add(oStockParam)

                Next

                Return listStockParam

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Insertar_Stock_Parametro_Recepcion(ByVal bo As clsBeStock_rec,
                                                             ByVal IdStock As Integer,
                                                             ByRef lConnection As SqlConnection,
                                                             ByRef lTransaction As SqlTransaction) As Integer

        Insertar_Stock_Parametro_Recepcion = 0

        Dim vRegistros As Integer = 0

        Try

            Dim listadetp As New List(Of clsBeTrans_re_det_parametros)

            Dim lMaxSP As Integer = MaxID(lConnection, lTransaction)

            listadetp = clsLnTrans_re_det_parametros.Get_All_By_IdRecepcionEnc_And_IdRecepcionDet(bo.IdRecepcionEnc,
                                                                                                  bo.IdRecepcionDet,
                                                                                                  lConnection,
                                                                                                  lTransaction)

            If Not listadetp Is Nothing Then

                If listadetp.Count > 0 Then

                    For Each dp As clsBeTrans_re_det_parametros In listadetp

                        Dim BeTransReDetParam As New clsBeStock_parametro()
                        clsPublic.CopyObject(dp, BeTransReDetParam)
                        lMaxSP += 1
                        BeTransReDetParam.IdStockParametro = lMaxSP
                        BeTransReDetParam.IdStock = IdStock
                        vRegistros += Insertar(BeTransReDetParam,
                                             lConnection,
                                             lTransaction)

                    Next


                    Insertar_Stock_Parametro_Recepcion = vRegistros

                End If

            End If


        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Sub Insertar_Stock_Parametro_Cambio_Ubicacion(ByVal lStockParam As List(Of clsBeStock_parametro),
                                                                ByVal IdStock As Integer,
                                                                ByRef lConnection As SqlConnection,
                                                                ByRef lTransaction As SqlTransaction)

        Try

            For Each dp As clsBeStock_parametro In lStockParam

                dp.IdStockParametro = MaxID(lConnection, lTransaction)
                dp.IdStock = IdStock
                Insertar(dp, lConnection, lTransaction)

            Next

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    '#EJC20171023_0228PM:Eliminar parámetros por IdStock por cambio de ubicación
    Public Shared Function Eliminar_Todos_By_IdStock(ByVal IdStock As Integer,
                                                  ByVal lConection As SQLConnection,
                                                  ByVal lTransaction As SqlTransaction) As Integer

        Try

            Const sp As String = " Delete from Stock_parametro Where IdStock = @IdStock"
            Dim cmd As New SqlCommand(sp, lConection, lTransaction) With {.CommandType = CommandType.Text}
            cmd.Parameters.AddWithValue("@IdStock", IdStock)
            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            Return rowsAffected

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
