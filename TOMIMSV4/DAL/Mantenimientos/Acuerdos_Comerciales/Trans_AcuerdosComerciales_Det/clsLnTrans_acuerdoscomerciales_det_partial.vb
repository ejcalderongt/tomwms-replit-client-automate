Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnTrans_acuerdoscomerciales_det


    Public Shared Function Get_All_By_Codigo_Acuerdo(ByVal pCodigoAcuerdo As String,
                                                     ByVal pIdBodega As Integer,
                                                     ByVal lConnection As SqlConnection,
                                                     ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_acuerdoscomerciales_det)

        Get_All_By_Codigo_Acuerdo = Nothing

        Dim lReturnList As New List(Of clsBeTrans_acuerdoscomerciales_det)

        Try

            Const sp As String = "SELECT * FROM trans_acuerdoscomerciales_det 
                                  WHERE codigo_acuerdo = @CODIGO_ACUERDO and estado=1 and IdBodega=@IdBodega 
                                  order by IdTipoCobro desc "





            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@CODIGO_ACUERDO", pCodigoAcuerdo)
                lDTA.SelectCommand.Parameters.AddWithValue("@IDBODEGA", pIdBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable.Rows.Count > 0 Then

                    Dim vBeCEALSA_detacuerdoscomerciales As New clsBeTrans_acuerdoscomerciales_det

                    For Each dr As DataRow In lDataTable.Rows
                        vBeCEALSA_detacuerdoscomerciales = New clsBeTrans_acuerdoscomerciales_det()
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

    Public Shared Function Get_All_For_Combo(ByVal pCodigo_cuerdo As Integer) As DataTable

        Get_All_For_Combo = Nothing

        Try

            Const sp As String = "SELECT IdAcuerdoDet,correlativo_detalleacuerdo,servicio,monto,porcentaje,codigo_producto 
                                  FROM trans_acuerdoscomerciales_det  
                                  WHERE codigo_acuerdo = @CODIGO_ACUERDO and estado=1 "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@CODIGO_ACUERDO", pCodigo_cuerdo)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot DBNull.Value AndAlso lDataTable IsNot Nothing Then
                            Get_All_For_Combo = lDataTable
                        End If

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

    Public Shared Function Get_AcuerdoDetalle_By_IdPropietario_And_IdBodega(ByVal pIdPropietario As Integer, ByVal pIdBodega As Integer) As DataTable

        Get_AcuerdoDetalle_By_IdPropietario_And_IdBodega = Nothing

        Try

            Const sp As String = "SELECT det.IdAcuerdoEnc,det.IdAcuerdoDet,det.correlativo_detalleacuerdo,det.codigo_producto,det.servicio,det.descripcion
                                  FROM trans_acuerdoscomerciales_det det inner join
                                       trans_acuerdoscomerciales_enc enc on enc.IdAcuerdoEnc=det.IdAcuerdoEnc and enc.codigo_acuerdo=det.codigo_acuerdo
                                  WHERE IdCliente = @IdCliente and IdBodega=@IdBodega 
                                        and det.estado=1 
                                        and det.servicio not like '%ALMACENAJE%' 
                                        and det.servicio not like '%MANEJO%' "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdCliente", pIdPropietario)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot DBNull.Value AndAlso lDataTable IsNot Nothing Then

                            If lDataTable.Rows.Count > 0 Then
                                Get_AcuerdoDetalle_By_IdPropietario_And_IdBodega = lDataTable
                            End If

                        End If

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

    Public Shared Function Existe_by_AcuerdoEnc(ByVal pCodigoAcuerdo As String, ByVal pCorrelativo As Integer, ByRef lConnection As SqlConnection, ByRef lTransaction As SqlTransaction) As Boolean

        Existe_by_AcuerdoEnc = False

        Try

            Dim vSQL As String = "SELECT * FROM trans_acuerdoscomerciales_det WHERE codigo_acuerdo=@Codigo_Acuerdo and correlativo_detalleacuerdo=@correlativo "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo_Acuerdo", pCodigoAcuerdo)
                lDTA.SelectCommand.Parameters.AddWithValue("@correlativo", pCorrelativo)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                Existe_by_AcuerdoEnc = (lDT.Rows.Count > 0)

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#GT27052024: asociar el acuerdo con la bodega (fiscal/general) y con la regla de negocio de cobro (unidad o valor mercaderia)
    Public Shared Function Actualizar_Bodega_and_TipoCobro(ByRef oBeTrans_acuerdoscomerciales_det As clsBeTrans_acuerdoscomerciales_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_acuerdoscomerciales_det")
            Upd.Add("IdBodega", "@idbodega", DataType.Parametro)
            Upd.Add("IdTipoCobro", "@idtipocobro", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)

            Upd.Where("IdAcuerdoEnc = @IdAcuerdoEnc and IdAcuerdoDet = @IdAcuerdoDet and correlativo_detalleacuerdo=@correlativo_detalleacuerdo")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDACUERDOENC", oBeTrans_acuerdoscomerciales_det.IdAcuerdoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDACUERDODET", oBeTrans_acuerdoscomerciales_det.IdAcuerdoDet))
            cmd.Parameters.Add(New SqlParameter("@CORRELATIVO_DETALLEACUERDO", oBeTrans_acuerdoscomerciales_det.Correlativo_detalleacuerdo))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_acuerdoscomerciales_det.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_acuerdoscomerciales_det.Fec_mod))

            If oBeTrans_acuerdoscomerciales_det.IdBodega > 0 Then
                cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_acuerdoscomerciales_det.IdBodega))
            Else
                cmd.Parameters.Add(New SqlParameter("@IDTIPOCOBRO", DBNull.Value))
            End If

            If oBeTrans_acuerdoscomerciales_det.IdTipoCobro > 0 Then
                cmd.Parameters.Add(New SqlParameter("@IDTIPOCOBRO", oBeTrans_acuerdoscomerciales_det.IdTipoCobro))
            Else
                cmd.Parameters.Add(New SqlParameter("@IDTIPOCOBRO", DBNull.Value))
            End If

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Sub GetSingle_By_Correlativo(ByRef pBeTrans_acuerdoscomerciales_det As clsBeTrans_acuerdoscomerciales_det)

        Try

            Const sp As String = "SELECT * FROM trans_acuerdoscomerciales_det 
                                           WHERE correlativo_detalleacuerdo=@correlativo"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@correlativo", pBeTrans_acuerdoscomerciales_det.Correlativo_detalleacuerdo)
                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                            Dim pAcuerdoComercialDet As New clsBeTrans_acuerdoscomerciales_det
                            Cargar(pAcuerdoComercialDet, lDT.Rows(0))

                            pBeTrans_acuerdoscomerciales_det = pAcuerdoComercialDet
                        End If

                    End Using



                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Sub GetSingle_By_Correlativo(ByRef pBeTrans_acuerdoscomerciales_det As clsBeTrans_acuerdoscomerciales_det,
                                                                                         ByVal pConnection As SqlConnection,
                                                                                         ByVal pTransaction As SqlTransaction)

        Try

            Const sp As String = "SELECT * FROM trans_acuerdoscomerciales_det WHERE correlativo_detalleacuerdo=@correlativo "

            Using lDTA As New SqlDataAdapter(sp, pConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = pTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@correlativo", pBeTrans_acuerdoscomerciales_det.Correlativo_detalleacuerdo)
                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                    Dim pAcuerdoComercialDet As New clsBeTrans_acuerdoscomerciales_det
                    Cargar(pAcuerdoComercialDet, lDT.Rows(0))

                    pBeTrans_acuerdoscomerciales_det = pAcuerdoComercialDet
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Sub Actualizar_Lista(ByVal pListaAcuerdos As List(Of clsBeTrans_acuerdoscomerciales_det))
        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try
            For Each detAcuerdo As clsBeTrans_acuerdoscomerciales_det In pListaAcuerdos
                Actualizar_Estado(detAcuerdo, lConnection, lTransaction)
            Next

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function Actualizar_Estado(ByRef oBeCEALSA_detacuerdoscomerciales As clsBeTrans_acuerdoscomerciales_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_acuerdoscomerciales_det")
            Upd.Add("estado", "@estado", DataType.Parametro)
            Upd.Add("prioridad", "@prioridad", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)

            Upd.Where("codigo_acuerdo=@CORRE_CBMAEACUERDOSSERVICIOS and
                       codigo_producto = @CODIGOPRODUCTO and correlativo_detalleacuerdo=@CORRELATIVO ")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction()
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@CODIGOPRODUCTO", oBeCEALSA_detacuerdoscomerciales.Codigo_producto))
            cmd.Parameters.Add(New SqlParameter("@CORRE_CBMAEACUERDOSSERVICIOS", oBeCEALSA_detacuerdoscomerciales.Codigo_acuerdo))
            cmd.Parameters.Add(New SqlParameter("@CORRELATIVO", oBeCEALSA_detacuerdoscomerciales.Correlativo_detalleacuerdo))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeCEALSA_detacuerdoscomerciales.Estado))
            cmd.Parameters.Add(New SqlParameter("@PRIORIDAD", oBeCEALSA_detacuerdoscomerciales.Prioridad))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeCEALSA_detacuerdoscomerciales.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeCEALSA_detacuerdoscomerciales.Fec_mod))

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

    '#GT10062024: llenar oc con detalle según acuerdo encabezado.
    Public Shared Function Get_Detalle_By_Codigo_Acuerdo(ByVal pCodigoAcuerdo As Integer, ByVal pIdBodega As Integer) As DataTable

        Get_Detalle_By_Codigo_Acuerdo = Nothing


        Try

            'Const sp As String = "SELECT det.IdAcuerdoEnc,det.IdAcuerdoDet,det.correlativo_detalleacuerdo,det.codigo_producto,det.servicio,det.descripcion
            '                      FROM trans_acuerdoscomerciales_det det inner join
            '                           trans_acuerdoscomerciales_enc enc on enc.IdAcuerdoEnc=det.IdAcuerdoEnc and enc.codigo_acuerdo=det.codigo_acuerdo
            '                      WHERE enc.codigo_acuerdo=@CODIGO_ACUERDO
            '                            and det.estado=1 
            '                            and det.servicio not like '%ALMACENAJE%' 
            '                            and det.servicio not like '%MANEJO%' "


            Const sp As String = "SELECT det.IdAcuerdoEnc,det.IdAcuerdoDet,det.correlativo_detalleacuerdo,det.codigo_producto,det.servicio,det.descripcion
                                  FROM trans_acuerdoscomerciales_det det inner join
                                       trans_acuerdoscomerciales_enc enc on enc.IdAcuerdoEnc=det.IdAcuerdoEnc and enc.codigo_acuerdo=det.codigo_acuerdo
                                  WHERE enc.IdAcuerdoEnc=@CODIGO_ACUERDO and det.IdBodega=@IdBodega 
                                        and det.estado=1 
                                        and det.servicio not like '%ALMACENAJE%' 
                                        and det.servicio not like '%MANEJO%' "


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@CODIGO_ACUERDO", pCodigoAcuerdo)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IDBODEGA", pIdBodega)
                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                            Get_Detalle_By_Codigo_Acuerdo = lDT
                        End If

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



End Class
