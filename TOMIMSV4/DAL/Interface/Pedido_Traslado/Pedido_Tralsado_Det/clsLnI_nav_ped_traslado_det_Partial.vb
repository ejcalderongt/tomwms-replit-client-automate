Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnI_nav_ped_traslado_det

    Public Shared Function GetAll(ByRef lConnection As SqlConnection,
                                  ByRef lTrans As SqlTransaction,
                                  ByVal pNoEnc As String) As List(Of clsBeI_nav_ped_traslado_det)

        Try

            Dim lReturnList As New List(Of clsBeI_nav_ped_traslado_det)
            Const sp As String = "SELECT * FROM I_nav_ped_traslado_det WHERE NoEnc = @No "
            Dim cmd As New SqlCommand(sp, lConnection, lTrans) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.SelectCommand.Parameters.Add(New SqlParameter("@No", pNoEnc))

            dad.Fill(dt)

            Dim vBeI_nav_ped_traslado_det As New clsBeI_nav_ped_traslado_det

            For Each dr As DataRow In dt.Rows

                vBeI_nav_ped_traslado_det = New clsBeI_nav_ped_traslado_det
                Cargar(vBeI_nav_ped_traslado_det, dr)
                lReturnList.Add(vBeI_nav_ped_traslado_det)

            Next

            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Exist(ByRef pBeI_nav_ped_traslado_det As clsBeI_nav_ped_traslado_det)

        Exist = False

        Try

            '#EJC20180614: Se agregó validación por número de línea porque el artículo se puede repetir por línea
            Dim vSQL As String = "SELECT No FROM I_nav_ped_traslado_det Where(NoEnc = @NoEnc AND No = @No AND Line_No = @Line_No )"

            Using lConnection As SqlConnection = New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)


                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.Add(New SqlParameter("@NOENC", pBeI_nav_ped_traslado_det.NoEnc))
                        lDTA.SelectCommand.Parameters.Add(New SqlParameter("@NO", pBeI_nav_ped_traslado_det.No))
                        lDTA.SelectCommand.Parameters.Add(New SqlParameter("@LINE_NO", pBeI_nav_ped_traslado_det.Line_No))
                        lDTA.SelectCommand.Transaction = lTransaction

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        Exist = lDT.Rows.Count > 0

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Exist(ByRef pBeI_nav_ped_traslado_det As clsBeI_nav_ped_traslado_det,
                                 ByVal pConnection As SqlConnection,
                                 ByVal pTransaction As SqlTransaction)

        Exist = False

        Try

            '#EJC20180614: Se agregó validación por número de línea porque el artículo se puede repetir por línea
            Dim vSQL As String = "SELECT No FROM I_nav_ped_traslado_det Where(NoEnc = @NoEnc AND No = @No AND Line_No = @Line_No )"

            Using lDTA As New SqlDataAdapter(vSQL, pConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@NOENC", pBeI_nav_ped_traslado_det.NoEnc))
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@NO", pBeI_nav_ped_traslado_det.No))
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@LINE_NO", pBeI_nav_ped_traslado_det.Line_No))
                lDTA.SelectCommand.Transaction = pTransaction

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                Exist = lDT.Rows.Count > 0

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function ActualizarFromIn(ByRef oBeI_nav_ped_traslado_det As clsBeI_nav_ped_traslado_det,
                                            ByVal pConection As SqlConnection,
                                            ByVal pTransaction As SqlTransaction) As Integer

        Try

            Upd.Init("i_nav_ped_traslado_det")
            Upd.Add("noenc", "@noenc", DataType.Parametro)
            Upd.Add("no", "@no", DataType.Parametro)
            Upd.Add("description", "@description", DataType.Parametro)
            Upd.Add("item_no", "@item_no", DataType.Parametro)
            Upd.Add("qty_to_receive", "@qty_to_receive", DataType.Parametro)
            Upd.Add("qty_to_ship", "@qty_to_ship", DataType.Parametro)
            Upd.Add("quantity", "@quantity", DataType.Parametro)
            If Not oBeI_nav_ped_traslado_det.Transfer_to_CodeField Is Nothing Then Upd.Add("transfer_to_codefield", "@transfer_to_codefield", DataType.Parametro)
            Upd.Add("shipment_date", "@shipment_date", DataType.Parametro)
            Upd.Add("unit_of_measure_code", "@unit_of_measure_code", DataType.Parametro)
            Upd.Where("NoEnc = @NoEnc" &
                      " AND No = @No AND Line_No = @Line_No")

            Dim sp As String = Upd.SQL()

            Dim cmd As New SqlCommand(sp, pConection) With {.CommandType = CommandType.Text, .Transaction = pTransaction}

            cmd.Parameters.Add(New SqlParameter("@NOENC", oBeI_nav_ped_traslado_det.NoEnc))
            cmd.Parameters.Add(New SqlParameter("@NO", oBeI_nav_ped_traslado_det.No))
            '#CKFK20221024 Agregué esta opción para que quite el &
            cmd.Parameters.Add(New SqlParameter("@DESCRIPTION", clsPublic.Quitar_Caracteres_No_Permitidos(oBeI_nav_ped_traslado_det.Description)))
            cmd.Parameters.Add(New SqlParameter("@ITEM_NO", oBeI_nav_ped_traslado_det.Item_No))
            cmd.Parameters.Add(New SqlParameter("@QTY_TO_RECEIVE", oBeI_nav_ped_traslado_det.Qty_to_Receive))
            cmd.Parameters.Add(New SqlParameter("@QTY_TO_SHIP", oBeI_nav_ped_traslado_det.Qty_to_Ship))
            cmd.Parameters.Add(New SqlParameter("@QUANTITY", oBeI_nav_ped_traslado_det.Quantity))
            cmd.Parameters.Add(New SqlParameter("@TRANSFER_TO_CODEFIELD", oBeI_nav_ped_traslado_det.Transfer_to_CodeField))
            cmd.Parameters.Add(New SqlParameter("@SHIPMENT_DATE", oBeI_nav_ped_traslado_det.Shipment_Date))
            cmd.Parameters.Add(New SqlParameter("@UNIT_OF_MEASURE_CODE", oBeI_nav_ped_traslado_det.Unit_of_Measure_Code))
            cmd.Parameters.Add(New SqlParameter("@Line_No", oBeI_nav_ped_traslado_det.Line_No))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            Return rowsAffected

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Eliminar_By_NoEnc(ByVal NoEnc As String,
                                             Optional ByVal pConection As SqlConnection = Nothing,
                                             Optional ByVal pTransaction As SqlTransaction = Nothing) As Boolean

        Eliminar_By_NoEnc = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from i_nav_ped_traslado_det
               Where(NoEnc = @NoEnc) "

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@NOENC", NoEnc))

            cmd.ExecuteNonQuery()

            cmd.Dispose()

            Return True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    '#CKFK20221121 Funcion para obtener un detalle de traslado
    Public Shared Sub GetSingle(ByRef lConnection As SqlConnection,
                                ByRef lTrans As SqlTransaction,
                                ByRef pBeTraslado As clsBeI_nav_ped_traslado_det)

        Try

            Const sp As String = "SELECT * FROM I_nav_ped_traslado_det 
                                  WHERE NoEnc = @NoEnc AND No = @No AND Line_No = @Line_No"
            Dim cmd As New SqlCommand(sp, lConnection, lTrans) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.SelectCommand.Parameters.Add(New SqlParameter("@NoEnc", pBeTraslado.NoEnc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@No", pBeTraslado.Item_No))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@Line_No", pBeTraslado.Line_No))

            dad.Fill(dt)

            pBeTraslado = Nothing

            If Not dt Is Nothing Then

                If dt.Rows.Count = 1 Then

                    pBeTraslado = New clsBeI_nav_ped_traslado_det
                    Cargar(pBeTraslado, dt.Rows(0))

                End If
            End If

            cmd.Dispose()

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

End Class
