Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_servicio_det

    Public Shared Sub Cargar(ByRef oBeTrans_servicio_det As clsBeTrans_servicio_det, ByRef dr As DataRow)
        Try
            With oBeTrans_servicio_det
                .IdServicioEnc = IIf(IsDBNull(dr.Item("IdServicioEnc")), 0, dr.Item("IdServicioEnc"))
                .IdServicioDet = IIf(IsDBNull(dr.Item("IdServicioDet")), 0, dr.Item("IdServicioDet"))
                .Observacion = IIf(IsDBNull(dr.Item("observacion")), "", dr.Item("observacion"))
                .Codigo_producto = IIf(IsDBNull(dr.Item("codigo_producto")), "", dr.Item("codigo_producto"))
                .Nombre_servicio = IIf(IsDBNull(dr.Item("nombre_servicio")), "", dr.Item("nombre_servicio"))
                .Unid_medida = IIf(IsDBNull(dr.Item("unid_medida")), 0, dr.Item("unid_medida"))
                .Nombre_unidad = IIf(IsDBNull(dr.Item("nombre_unidad")), "", dr.Item("nombre_unidad"))
                .Corre_detalleacuerdo = IIf(IsDBNull(dr.Item("corre_detalleacuerdo")), 0, dr.Item("corre_detalleacuerdo"))
                .Corre_catalogoproductos = IIf(IsDBNull(dr.Item("corre_catalogoproductos")), 0, dr.Item("corre_catalogoproductos"))
                .Cantidad = IIf(IsDBNull(dr.Item("cantidad")), 0, dr.Item("cantidad"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .IdPropietario = IIf(IsDBNull(dr.Item("IdPropietario")), 0, dr.Item("IdPropietario"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_servicio_det As clsBeTrans_servicio_det, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_servicio_det")
            Ins.Add("idservicioenc", "@idservicioenc", DataType.Parametro)
            Ins.Add("idserviciodet", "@idserviciodet", DataType.Parametro)
            Ins.Add("observacion", "@observacion", DataType.Parametro)
            Ins.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
            Ins.Add("nombre_servicio", "@nombre_servicio", DataType.Parametro)
            Ins.Add("unid_medida", "@unid_medida", DataType.Parametro)
            Ins.Add("nombre_unidad", "@nombre_unidad", DataType.Parametro)
            Ins.Add("corre_detalleacuerdo", "@corre_detalleacuerdo", DataType.Parametro)
            Ins.Add("corre_catalogoproductos", "@corre_catalogoproductos", DataType.Parametro)
            Ins.Add("cantidad", "@cantidad", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("idpropietario", "@idpropietario", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDSERVICIOENC", oBeTrans_servicio_det.IdServicioEnc))
            cmd.Parameters.Add(New SqlParameter("@IDSERVICIODET", oBeTrans_servicio_det.IdServicioDet))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACION", oBeTrans_servicio_det.Observacion))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeTrans_servicio_det.Codigo_producto))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_SERVICIO", oBeTrans_servicio_det.Nombre_servicio))
            cmd.Parameters.Add(New SqlParameter("@UNID_MEDIDA", oBeTrans_servicio_det.Unid_medida))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_UNIDAD", oBeTrans_servicio_det.Nombre_unidad))
            cmd.Parameters.Add(New SqlParameter("@CORRE_DETALLEACUERDO", oBeTrans_servicio_det.Corre_detalleacuerdo))
            cmd.Parameters.Add(New SqlParameter("@CORRE_CATALOGOPRODUCTOS", oBeTrans_servicio_det.Corre_catalogoproductos))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_servicio_det.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_servicio_det.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_servicio_det.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_servicio_det.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_servicio_det.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeTrans_servicio_det.IdPropietario))

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

    Public Shared Function Actualizar(ByRef oBeTrans_servicio_det As clsBeTrans_servicio_det, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_servicio_det")
            Upd.Add("idservicioenc", "@idservicioenc", DataType.Parametro)
            Upd.Add("idserviciodet", "@idserviciodet", DataType.Parametro)
            Upd.Add("observacion", "@observacion", DataType.Parametro)
            Upd.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
            Upd.Add("nombre_servicio", "@nombre_servicio", DataType.Parametro)
            Upd.Add("unid_medida", "@unid_medida", DataType.Parametro)
            Upd.Add("nombre_unidad", "@nombre_unidad", DataType.Parametro)
            Upd.Add("corre_detalleacuerdo", "@corre_detalleacuerdo", DataType.Parametro)
            Upd.Add("corre_catalogoproductos", "@corre_catalogoproductos", DataType.Parametro)
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("idpropietario", "@idpropietario", DataType.Parametro)
            Upd.Where("IdServicioEnc = @IdServicioEnc AND IdServicioDet = @IdServicioDet")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDSERVICIOENC", oBeTrans_servicio_det.IdServicioEnc))
            cmd.Parameters.Add(New SqlParameter("@IDSERVICIODET", oBeTrans_servicio_det.IdServicioDet))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACION", oBeTrans_servicio_det.Observacion))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeTrans_servicio_det.Codigo_producto))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_SERVICIO", oBeTrans_servicio_det.Nombre_servicio))
            cmd.Parameters.Add(New SqlParameter("@UNID_MEDIDA", oBeTrans_servicio_det.Unid_medida))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_UNIDAD", oBeTrans_servicio_det.Nombre_unidad))
            cmd.Parameters.Add(New SqlParameter("@CORRE_DETALLEACUERDO", oBeTrans_servicio_det.Corre_detalleacuerdo))
            cmd.Parameters.Add(New SqlParameter("@CORRE_CATALOGOPRODUCTOS", oBeTrans_servicio_det.Corre_catalogoproductos))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_servicio_det.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_servicio_det.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_servicio_det.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_servicio_det.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_servicio_det.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeTrans_servicio_det.IdPropietario))

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


    Public Shared Function Eliminar(ByRef oBeTrans_servicio_det As clsBeTrans_servicio_det, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_servicio_det" & _
             "  Where(IdServicioEnc = @IdServicioEnc)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDSERVICIOENC", oBeTrans_servicio_det.IdServicioEnc))

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

            Const sp As String = "SELECT * FROM Trans_servicio_det"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
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

    Public Shared Function Get_All() As List(Of clsBeTrans_servicio_det)

        Dim lReturnList As New List(Of clsBeTrans_servicio_det)

        Try

            Const sp As String = "SELECT * FROM Trans_servicio_det"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_servicio_det As New clsBeTrans_servicio_det

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_servicio_det = New clsBeTrans_servicio_det()
                            Cargar(vBeTrans_servicio_det, dr)
                            lReturnList.Add(vBeTrans_servicio_det)
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

    Public Shared Sub GetSingle(ByRef pBeTrans_servicio_det As clsBeTrans_servicio_det)

        Try

            Const sp As String = "SELECT * FROM Trans_servicio_det" &
            " Where(IdServicioEnc = @IdServicioEnc)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_servicio_det As New clsBeTrans_servicio_det

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTrans_servicio_det, lDataTable.Rows(0))
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

    Public Shared Function List_By_Parameter(ByRef pServicioEnc As Integer) As List(Of clsBeTrans_servicio_det)

        Dim lReturnList As New List(Of clsBeTrans_servicio_det)

        Try

            Const sp As String = "SELECT * FROM Trans_servicio_det where IdServicioEnc=@IdServicioEnc "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdServicioEnc", pServicioEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_servicio_det As New clsBeTrans_servicio_det

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_servicio_det = New clsBeTrans_servicio_det()
                            Cargar(vBeTrans_servicio_det, dr)
                            vBeTrans_servicio_det.IsNew = False
                            lReturnList.Add(vBeTrans_servicio_det)
                        Next

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

    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdServicioDet),0) FROM Trans_servicio_det "

            Using lCommand As New SqlCommand(sp, pConnection, pTransaction)

                lCommand.CommandType = CommandType.Text

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
