Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnI_nav_acuerdo_det

    Public Shared Function Get_All_By_IdCliente(ByVal pIdCliente As Integer,
                                                ByVal lConnection As SqlConnection,
                                                ByVal lTransaction As SqlTransaction) As List(Of clsBeI_nav_acuerdo_det)

        Dim lReturnList As New List(Of clsBeI_nav_acuerdo_det)

        Try

            Const sp As String = "SELECT * FROM i_nav_acuerdo_det det 
                                  INNER JOIN i_nav_acuerdo_enc enc ON
								  det.idAcuerdo = enc.IdAcuerdo
								  WHERE idcliente = @idcliente "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@idcliente", pIdCliente)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeI_nav_detacuerdoscomerciales As New clsBeI_nav_acuerdo_det

                For Each dr As DataRow In lDataTable.Rows
                    vBeI_nav_detacuerdoscomerciales = New clsBeI_nav_acuerdo_det()
                    Cargar(vBeI_nav_detacuerdoscomerciales, dr)
                    lReturnList.Add(vBeI_nav_detacuerdoscomerciales)
                Next

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_Acuerdo_By_IdCliente_And_IdAcuerdo(ByVal pIdCliente As Integer,
                                                                     ByVal pIdAcuerdo As Integer,
                                                                     ByVal lConnection As SqlConnection,
                                                                     ByVal lTransaction As SqlTransaction) As List(Of clsBeI_nav_acuerdo_det)

        Dim lReturnList As New List(Of clsBeI_nav_acuerdo_det)

        Try

            Const sp As String = "SELECT det.* FROM i_nav_acuerdo_det det 
                                  INNER JOIN i_nav_acuerdo_enc enc ON
								  det.idAcuerdo = enc.IdAcuerdo
								  WHERE idcliente = @codcliente 
								  AND idAcuerdo = @idAcuerdo"

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@idcliente", pIdCliente)
                lDTA.SelectCommand.Parameters.AddWithValue("@idAcuerdo", pIdAcuerdo)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeI_nav_detacuerdoscomerciales As New clsBeI_nav_acuerdo_det

                For Each dr As DataRow In lDataTable.Rows
                    vBeI_nav_detacuerdoscomerciales = New clsBeI_nav_acuerdo_det()
                    Cargar(vBeI_nav_detacuerdoscomerciales, dr)
                    lReturnList.Add(vBeI_nav_detacuerdoscomerciales)
                Next

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdAcuerdoDet),0) FROM I_nav_acuerdo_det"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

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

    Public Shared Function Get_Single_By_IdAcuerdoDet_And_IdAcuerdoEnc(ByVal pIdAcuerdoDet As Integer,
                                                                       ByVal pIdAcuerdoEnc As Integer) As clsBeI_nav_acuerdo_det

        Get_Single_By_IdAcuerdoDet_And_IdAcuerdoEnc = Nothing

        Try

            Const sp As String = "SELECT * FROM I_nav_acuerdo_det" &
            " Where(IdAcuerdoDet = @IdAcuerdoDet AND IdAcuerdo = @IdAcuerdo)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdAcuerdoDet", pIdAcuerdoDet)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdAcuerdo", pIdAcuerdoEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeI_nav_acuerdo_det As New clsBeI_nav_acuerdo_det()

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeI_nav_acuerdo_det, lDataTable.Rows(0))
                            Get_Single_By_IdAcuerdoDet_And_IdAcuerdoEnc = vBeI_nav_acuerdo_det
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_Codigo_Acuerdo(ByVal pCodigoAcuerdo As String,
                                                     ByVal lConnection As SqlConnection,
                                                     ByVal lTransaction As SqlTransaction) As List(Of clsBeI_nav_acuerdo_det)

        Get_All_By_Codigo_Acuerdo = Nothing

        Dim lReturnList As New List(Of clsBeI_nav_acuerdo_det)

        Try

            Const sp As String = "SELECT * FROM i_nav_acuerdo_det 
                                  WHERE codigo_acuerdo = @CODACUERDO "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@CODACUERDO", pCodigoAcuerdo)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable.Rows.Count > 0 Then

                    Dim vBeCEALSA_detacuerdoscomerciales As New clsBeI_nav_acuerdo_det

                    For Each dr As DataRow In lDataTable.Rows
                        vBeCEALSA_detacuerdoscomerciales = New clsBeI_nav_acuerdo_det()
                        Cargar(vBeCEALSA_detacuerdoscomerciales, dr)
                        lReturnList.Add(vBeCEALSA_detacuerdoscomerciales)
                    Next

                    Return lReturnList

                End If

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function


    Public Shared Function Get_All_Encabezados_Acuerdos_Comerciales_No_Procesados(ByVal lConnection As SqlConnection,
                                                                                  ByVal lTransaction As SqlTransaction) As List(Of clsBeI_nav_acuerdo_enc)

        Dim lReturnList As New List(Of clsBeI_nav_acuerdo_enc)

        Try

            Const sp As String = "SELECT codcliente as IdCliente, codacuerdo as IdAcuerdo, 
                                  codacuerdo as codigo_acuerdo, descrip as descripcion, 
                                  tipocobro as tipo_cobro,codmoneda as cod_moneda, moneda as nom_moneda,
                                  procesado_wms
                                  FROM i_nav_acuerdo_det
                                  WHERE procesado_wms =0
                                  GROUP BY codcliente, codacuerdo,descrip, tipocobro,codmoneda, moneda,procesado_wms "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeI_nav_detacuerdoscomerciales As New clsBeI_nav_acuerdo_enc

                For Each dr As DataRow In lDataTable.Rows
                    vBeI_nav_detacuerdoscomerciales = New clsBeI_nav_acuerdo_enc()
                    clsLnI_nav_acuerdo_enc.Cargar(vBeI_nav_detacuerdoscomerciales, dr)
                    lReturnList.Add(vBeI_nav_detacuerdoscomerciales)
                Next

            End Using

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Detalles_No_Procesados(ByVal lConnection As SqlConnection,
                                                          ByVal lTransaction As SqlTransaction) As List(Of clsBeI_nav_acuerdo_det)

        Dim lReturnList As New List(Of clsBeI_nav_acuerdo_det)

        Try

            Const sp As String = "SELECT * FROM i_nav_acuerdo_det WHERE procesado_wms =0"

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeI_nav_detacuerdoscomerciales As New clsBeI_nav_acuerdo_det

                For Each dr As DataRow In lDataTable.Rows
                    vBeI_nav_detacuerdoscomerciales = New clsBeI_nav_acuerdo_det()
                    Cargar(vBeI_nav_detacuerdoscomerciales, dr)
                    lReturnList.Add(vBeI_nav_detacuerdoscomerciales)
                Next

            End Using

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar_Procesado_Det(ByRef oBeTransAcuerdo_Det As clsBeI_nav_acuerdo_det,
                                                    Optional ByVal pConection As SqlConnection = Nothing,
                                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

        Try

            '#GT13052024: actualizar por acuerdo y el cliente, ya que el cliente puede tener mas de un acuerdo.
            Upd.Init("i_nav_acuerdo_det")
            Upd.Add("procesado_wms", "@procesado_wms", DataType.Parametro)
            Upd.Where("codigo_acuerdo=@codigo_acuerdo and corre_detalleacuerdo=@correlativo")

            Dim sp As String = Upd.SQL()

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            '#CKFK 20210312 Reemplacé el IdCliente por el Codigo cliente
            cmd.Parameters.Add(New SqlParameter("@CODIGO_ACUERDO", oBeTransAcuerdo_Det.Codigo_acuerdo))
            cmd.Parameters.Add(New SqlParameter("@CORRELATIVO", oBeTransAcuerdo_Det.Correlativo_detalleacuerdo))
            cmd.Parameters.Add(New SqlParameter("@PROCESADO_WMS", oBeTransAcuerdo_Det.Procesado_wms))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

End Class
