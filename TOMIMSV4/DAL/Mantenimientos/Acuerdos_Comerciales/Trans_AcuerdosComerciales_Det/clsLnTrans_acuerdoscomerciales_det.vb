Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_acuerdoscomerciales_det

    Public Shared Sub Cargar(ByRef oBeTrans_acuerdoscomerciales_det As clsBeTrans_acuerdoscomerciales_det, ByRef dr As DataRow)
        Try
            With oBeTrans_acuerdoscomerciales_det
                .IdAcuerdoDet = IIf(IsDBNull(dr.Item("IdAcuerdoDet")), 0, dr.Item("IdAcuerdoDet"))
                .IdAcuerdoEnc = IIf(IsDBNull(dr.Item("IdAcuerdoEnc")), 0, dr.Item("IdAcuerdoEnc"))
                .Codigo_producto = IIf(IsDBNull(dr.Item("codigo_producto")), "", dr.Item("codigo_producto"))
                .Servicio = IIf(IsDBNull(dr.Item("servicio")), "", dr.Item("servicio"))
                .Nemonico = IIf(IsDBNull(dr.Item("nemonico")), "", dr.Item("nemonico"))
                .Codigo_acuerdo = IIf(IsDBNull(dr.Item("codigo_acuerdo")), 0, dr.Item("codigo_acuerdo"))
                .Correlativo_detalleacuerdo = IIf(IsDBNull(dr.Item("correlativo_detalleacuerdo")), 0, dr.Item("correlativo_detalleacuerdo"))
                .Descripcion = IIf(IsDBNull(dr.Item("descripcion")), "", dr.Item("descripcion"))
                .Numero_unidades = IIf(IsDBNull(dr.Item("numero_unidades")), 0.0, dr.Item("numero_unidades"))
                .Monto = IIf(IsDBNull(dr.Item("monto")), 0.0, dr.Item("monto"))
                .Porcentaje = IIf(IsDBNull(dr.Item("porcentaje")), 0.0, dr.Item("porcentaje"))
                .Dias_eventos = IIf(IsDBNull(dr.Item("dias_eventos")), 0, dr.Item("dias_eventos"))
                .Corre_cbcatalogoproductos = IIf(IsDBNull(dr.Item("corre_cbcatalogoproductos")), 0, dr.Item("corre_cbcatalogoproductos"))
                .Estado = IIf(IsDBNull(dr.Item("estado")), False, dr.Item("estado"))
                .Prioridad = IIf(IsDBNull(dr.Item("prioridad")), 0, dr.Item("prioridad"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .IdTipoCobro = IIf(IsDBNull(dr.Item("IdTipoCobro")), 0, dr.Item("IdTipoCobro"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_acuerdoscomerciales_det As clsBeTrans_acuerdoscomerciales_det, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_acuerdoscomerciales_det")
            Ins.Add("idacuerdodet", "@idacuerdodet", DataType.Parametro)
            Ins.Add("idacuerdoenc", "@idacuerdoenc", DataType.Parametro)
            Ins.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
            Ins.Add("servicio", "@servicio", DataType.Parametro)
            Ins.Add("nemonico", "@nemonico", DataType.Parametro)
            Ins.Add("codigo_acuerdo", "@codigo_acuerdo", DataType.Parametro)
            Ins.Add("correlativo_detalleacuerdo", "@correlativo_detalleacuerdo", DataType.Parametro)
            Ins.Add("descripcion", "@descripcion", DataType.Parametro)
            Ins.Add("numero_unidades", "@numero_unidades", DataType.Parametro)
            Ins.Add("monto", "@monto", DataType.Parametro)
            Ins.Add("porcentaje", "@porcentaje", DataType.Parametro)
            Ins.Add("dias_eventos", "@dias_eventos", DataType.Parametro)
            Ins.Add("corre_cbcatalogoproductos", "@corre_cbcatalogoproductos", DataType.Parametro)
            Ins.Add("estado", "@estado", DataType.Parametro)
            Ins.Add("prioridad", "@prioridad", DataType.Parametro)
            Ins.Add("IdBodega", "@idbodega", DataType.Parametro)
            Ins.Add("IdTipoCobro", "@idtipocobro", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDACUERDODET", oBeTrans_acuerdoscomerciales_det.IdAcuerdoDet))
            cmd.Parameters.Add(New SqlParameter("@IDACUERDOENC", oBeTrans_acuerdoscomerciales_det.IdAcuerdoEnc))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeTrans_acuerdoscomerciales_det.Codigo_producto))
            cmd.Parameters.Add(New SqlParameter("@SERVICIO", oBeTrans_acuerdoscomerciales_det.Servicio))
            cmd.Parameters.Add(New SqlParameter("@NEMONICO", oBeTrans_acuerdoscomerciales_det.Nemonico))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_ACUERDO", oBeTrans_acuerdoscomerciales_det.Codigo_acuerdo))
            cmd.Parameters.Add(New SqlParameter("@CORRELATIVO_DETALLEACUERDO", oBeTrans_acuerdoscomerciales_det.Correlativo_detalleacuerdo))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeTrans_acuerdoscomerciales_det.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@NUMERO_UNIDADES", oBeTrans_acuerdoscomerciales_det.Numero_unidades))
            cmd.Parameters.Add(New SqlParameter("@MONTO", oBeTrans_acuerdoscomerciales_det.Monto))
            cmd.Parameters.Add(New SqlParameter("@PORCENTAJE", oBeTrans_acuerdoscomerciales_det.Porcentaje))
            cmd.Parameters.Add(New SqlParameter("@DIAS_EVENTOS", oBeTrans_acuerdoscomerciales_det.Dias_eventos))
            cmd.Parameters.Add(New SqlParameter("@CORRE_CBCATALOGOPRODUCTOS", oBeTrans_acuerdoscomerciales_det.Corre_cbcatalogoproductos))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeTrans_acuerdoscomerciales_det.Estado))
            cmd.Parameters.Add(New SqlParameter("@PRIORIDAD", oBeTrans_acuerdoscomerciales_det.Prioridad))

            If oBeTrans_acuerdoscomerciales_det.IdBodega > 0 Then
                cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_acuerdoscomerciales_det.IdBodega))
            Else
                cmd.Parameters.Add(New SqlParameter("@IDBODEGA", DBNull.Value))
            End If

            If oBeTrans_acuerdoscomerciales_det.IdTipoCobro > 0 Then
                cmd.Parameters.Add(New SqlParameter("@IDTIPOCOBRO", oBeTrans_acuerdoscomerciales_det.IdTipoCobro))
            Else
                cmd.Parameters.Add(New SqlParameter("@IDTIPOCOBRO", DBNull.Value))
            End If

            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_acuerdoscomerciales_det.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_acuerdoscomerciales_det.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_acuerdoscomerciales_det.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_acuerdoscomerciales_det.Fec_mod))

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

    Public Shared Function Actualizar(ByRef oBeTrans_acuerdoscomerciales_det As clsBeTrans_acuerdoscomerciales_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_acuerdoscomerciales_det")
            Upd.Add("idacuerdodet", "@idacuerdodet", DataType.Parametro)
            Upd.Add("idacuerdoenc", "@idacuerdoenc", DataType.Parametro)
            Upd.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
            Upd.Add("servicio", "@servicio", DataType.Parametro)
            Upd.Add("nemonico", "@nemonico", DataType.Parametro)
            Upd.Add("codigo_acuerdo", "@codigo_acuerdo", DataType.Parametro)
            Upd.Add("correlativo_detalleacuerdo", "@correlativo_detalleacuerdo", DataType.Parametro)
            Upd.Add("descripcion", "@descripcion", DataType.Parametro)
            Upd.Add("numero_unidades", "@numero_unidades", DataType.Parametro)
            Upd.Add("monto", "@monto", DataType.Parametro)
            Upd.Add("porcentaje", "@porcentaje", DataType.Parametro)
            Upd.Add("dias_eventos", "@dias_eventos", DataType.Parametro)
            Upd.Add("corre_cbcatalogoproductos", "@corre_cbcatalogoproductos", DataType.Parametro)
            Upd.Add("estado", "@estado", DataType.Parametro)
            Upd.Add("prioridad", "@prioridad", DataType.Parametro)
            Upd.Where("IdAcuerdoDet = @IdAcuerdoDet")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDACUERDODET", oBeTrans_acuerdoscomerciales_det.IdAcuerdoDet))
            cmd.Parameters.Add(New SqlParameter("@IDACUERDOENC", oBeTrans_acuerdoscomerciales_det.IdAcuerdoEnc))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeTrans_acuerdoscomerciales_det.Codigo_producto))
            cmd.Parameters.Add(New SqlParameter("@SERVICIO", oBeTrans_acuerdoscomerciales_det.Servicio))
            cmd.Parameters.Add(New SqlParameter("@NEMONICO", oBeTrans_acuerdoscomerciales_det.Nemonico))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_ACUERDO", oBeTrans_acuerdoscomerciales_det.Codigo_acuerdo))
            cmd.Parameters.Add(New SqlParameter("@CORRELATIVO_DETALLEACUERDO", oBeTrans_acuerdoscomerciales_det.Correlativo_detalleacuerdo))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeTrans_acuerdoscomerciales_det.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@NUMERO_UNIDADES", oBeTrans_acuerdoscomerciales_det.Numero_unidades))
            cmd.Parameters.Add(New SqlParameter("@MONTO", oBeTrans_acuerdoscomerciales_det.Monto))
            cmd.Parameters.Add(New SqlParameter("@PORCENTAJE", oBeTrans_acuerdoscomerciales_det.Porcentaje))
            cmd.Parameters.Add(New SqlParameter("@DIAS_EVENTOS", oBeTrans_acuerdoscomerciales_det.Dias_eventos))
            cmd.Parameters.Add(New SqlParameter("@CORRE_CBCATALOGOPRODUCTOS", oBeTrans_acuerdoscomerciales_det.Corre_cbcatalogoproductos))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeTrans_acuerdoscomerciales_det.Estado))
            cmd.Parameters.Add(New SqlParameter("@PRIORIDAD", oBeTrans_acuerdoscomerciales_det.Prioridad))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection is Nothing Then lConnection.Dispose()
            If Not lTransaction is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Eliminar(ByRef oBeTrans_acuerdoscomerciales_det As clsBeTrans_acuerdoscomerciales_det, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_acuerdoscomerciales_det" & _
             "  Where(IdAcuerdoDet = @IdAcuerdoDet)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDACUERDODET", oBeTrans_acuerdoscomerciales_det.IdAcuerdoDet))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection is Nothing Then lConnection.Dispose()
            If Not lTransaction is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_acuerdoscomerciales_det"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection is Nothing Then lConnection.Dispose()
            If Not lTransaction is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_All() As List(Of clsBeTrans_acuerdoscomerciales_det)

        Dim lReturnList As New List(Of clsBeTrans_acuerdoscomerciales_det)

        Try

            Const sp As String = "SELECT * FROM Trans_acuerdoscomerciales_det"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_acuerdoscomerciales_det As New clsBeTrans_acuerdoscomerciales_det

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_acuerdoscomerciales_det = New clsBeTrans_acuerdoscomerciales_det()
                            Cargar(vBeTrans_acuerdoscomerciales_det, dr)
                            lReturnList.Add(vBeTrans_acuerdoscomerciales_det)
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Sub GetSingle(ByRef pBeTrans_acuerdoscomerciales_det As clsBeTrans_acuerdoscomerciales_det)

        Try

            Const sp As String = "SELECT * FROM Trans_acuerdoscomerciales_det" & _
            " Where(IdAcuerdoDet = @IdAcuerdoDet)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdAcuerdoDet", pBeTrans_acuerdoscomerciales_det.IdAcuerdoDet)
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        'Dim vBeTrans_acuerdoscomerciales_det As New clsBeTrans_acuerdoscomerciales_det

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(pBeTrans_acuerdoscomerciales_det, lDataTable.Rows(0))
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Public Shared Function MaxID() as Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdAcuerdoDet),0) FROM Trans_acuerdoscomerciales_det"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()
                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lMax = CInt(lReturnValue)
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lMax

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdAcuerdoDet),0) FROM Trans_acuerdoscomerciales_det"

            Using lCommand As New SqlCommand(sp, pConnection)

                lCommand.CommandType = CommandType.Text
                lCommand.Transaction = pTransaction

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


End Class
