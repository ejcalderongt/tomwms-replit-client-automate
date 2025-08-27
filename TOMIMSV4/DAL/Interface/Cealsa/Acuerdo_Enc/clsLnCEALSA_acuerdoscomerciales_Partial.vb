Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnCEALSA_acuerdoscomerciales

    Public Shared Function Get_All_With_Detail() As List(Of clsBeCEALSA_acuerdoscomerciales)

        Dim lReturnList As New List(Of clsBeCEALSA_acuerdoscomerciales)

        Try

            Const sp As String = "SELECT * FROM i_nav_acuerdoscomerciales_enc "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ERP"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeCEALSA_acuerdoscomerciales As New clsBeCEALSA_acuerdoscomerciales
                        Dim vBeCEALSA_acuerdoscomercialExistente As New clsBeCEALSA_acuerdoscomerciales
                        Dim vBeDetExistente As New clsBeCEALSA_detacuerdoscomerciales

                        For Each dr As DataRow In lDataTable.Rows

                            vBeCEALSA_acuerdoscomerciales = New clsBeCEALSA_acuerdoscomerciales()
                            Cargar(vBeCEALSA_acuerdoscomerciales, dr)

                            vBeCEALSA_acuerdoscomercialExistente = lReturnList.Find(Function(x) x.Descrip = vBeCEALSA_acuerdoscomerciales.Descrip AndAlso
                                                                                        x.Codcliente = vBeCEALSA_acuerdoscomerciales.Codcliente AndAlso
                                                                                        x.Codacuerdo = vBeCEALSA_acuerdoscomerciales.Codacuerdo)


                            If vBeCEALSA_acuerdoscomercialExistente Is Nothing Then
                                vBeCEALSA_acuerdoscomerciales.lDetalle = clsLnCEALSA_detacuerdoscomerciales.Get_All_By_Codigo_Acuerdo(vBeCEALSA_acuerdoscomerciales.Codcliente,
                                                                                                                                      vBeCEALSA_acuerdoscomerciales.Codacuerdo,
                                                                                                                                      lConnection,
                                                                                                                                      lTransaction)

                                lReturnList.Add(vBeCEALSA_acuerdoscomerciales)
                            Else

                                vBeCEALSA_acuerdoscomerciales.lDetalle = clsLnCEALSA_detacuerdoscomerciales.Get_All_By_Codigo_Acuerdo(vBeCEALSA_acuerdoscomerciales.Codcliente,
                                                                                                                                      vBeCEALSA_acuerdoscomerciales.Codacuerdo,
                                                                                                                                      lConnection,
                                                                                                                                      lTransaction)


                                If vBeCEALSA_acuerdoscomerciales.lDetalle IsNot Nothing Then

                                    For Each Det In vBeCEALSA_acuerdoscomerciales.lDetalle

                                        If vBeCEALSA_acuerdoscomercialExistente.lDetalle IsNot Nothing Then

                                            vBeDetExistente = vBeCEALSA_acuerdoscomercialExistente.lDetalle.Find(Function(x) x.Codigoproducto = Det.Codigoproducto)

                                            If vBeDetExistente Is Nothing Then

                                                'Det.EsAdaptado = True
                                                ''Se encontró un producto/servicio que está en un acuerdo con el mismo nombre, pero no existe en el base.
                                                'vBeCEALSA_acuerdoscomercialExistente.lDetalle.Add(Det)

                                                Debug.WriteLine("*** El producto: " & Det.Codigoproducto & " NO existe para el mismo acuerdo, pero se adoptó.***")

                                            Else
                                                Debug.WriteLine("El producto: " & Det.Codigoproducto & " ya existe para el mismo acuerdo")
                                            End If

                                        End If

                                    Next

                                Else
                                    Debug.WriteLine("El Acuerdo Comercial: " & vBeCEALSA_acuerdoscomerciales.Codacuerdo & " - " & vBeCEALSA_acuerdoscomerciales.Descrip & " Ya existe, pero esta variación no tiene detalle.")
                                End If

                            End If

                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All() As List(Of clsBeCEALSA_acuerdoscomerciales)

        Dim lReturnList As New List(Of clsBeCEALSA_acuerdoscomerciales)

        Try

            Const sp As String = "SELECT * FROM i_nav_acuerdoscomerciales_enc "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ERP"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeCEALSA_acuerdoscomerciales As New clsBeCEALSA_acuerdoscomerciales
                        Dim vBeCEALSA_acuerdoscomercialExistente As New clsBeCEALSA_acuerdoscomerciales
                        Dim vBeDetExistente As New clsBeCEALSA_detacuerdoscomerciales

                        For Each dr As DataRow In lDataTable.Rows

                            vBeCEALSA_acuerdoscomerciales = New clsBeCEALSA_acuerdoscomerciales()
                            Cargar(vBeCEALSA_acuerdoscomerciales, dr)
                            lReturnList.Add(vBeCEALSA_acuerdoscomerciales)

                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_For_Combo(ByVal pIdCliente As Integer) As DataTable


        Get_All_For_Combo = Nothing

        Try

            Const sp As String = "select codacuerdo,descrip
                                  from i_nav_acuerdoscomerciales_enc where codcliente=@codigo_cliente "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@codigo_cliente", pIdCliente)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Get_All_For_Combo = lDataTable
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return Get_All_For_Combo

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar_Procesado_WMS(ByRef oBeCealsa_AcuerdoEnc As clsBeI_nav_acuerdo_enc,
                                                    ByVal lConnectionERP As SqlConnection,
                                                    ByVal lTransactionERP As SqlTransaction) As Integer



        Try
            If oBeCealsa_AcuerdoEnc IsNot Nothing Then

                Actualizar_procesado_enc(oBeCealsa_AcuerdoEnc, lConnectionERP, lTransactionERP)

                If oBeCealsa_AcuerdoEnc.lDetalle IsNot Nothing Then

                    If oBeCealsa_AcuerdoEnc.lDetalle.Count > 0 Then

                        For Each detalle As clsBeI_nav_acuerdo_det In oBeCealsa_AcuerdoEnc.lDetalle
                            detalle.Procesado_wms = True
                            detalle.IdCliente = oBeCealsa_AcuerdoEnc.Idcliente
                            clsLnCEALSA_detacuerdoscomerciales.Actualizar_Procesado_Det(detalle, lConnectionERP, lTransactionERP)
                        Next
                    End If

                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar_procesado_enc(ByRef oBeI_nav_acuerdo As clsBeI_nav_acuerdo_enc,
                                                    Optional ByVal lConnectionERP As SqlConnection = Nothing,
                                                    Optional ByVal lTransactionERP As SqlTransaction = Nothing) As Integer

        Try


            Upd.Init("i_nav_acuerdoscomerciales_enc")
            Upd.Add("procesado_wms", "@procesado_wms", DataType.Parametro)
            Upd.Where("codcliente = @codcliente and codacuerdo=@codacuerdo")

            Dim sp As String = Upd.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}
            cmd = New SqlCommand(sp, lConnectionERP, lTransactionERP)

            cmd.Parameters.Add(New SqlParameter("@CODCLIENTE", oBeI_nav_acuerdo.Idcliente))
            cmd.Parameters.Add(New SqlParameter("@CODACUERDO", oBeI_nav_acuerdo.Codigo_acuerdo))
            cmd.Parameters.Add(New SqlParameter("@PROCESADO_WMS", oBeI_nav_acuerdo.Procesado_wms))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            Return rowsAffected

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
