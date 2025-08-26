Imports System.Data.SqlClient
Public Class clsLnTrans_oc_servicios

    Public Shared Sub Cargar(ByRef oBeTrans_oc_servicios As clsBeTrans_oc_servicios, ByRef dr As DataRow)
        Try
            With oBeTrans_oc_servicios
                .IdOrdenCompraServicio = IIf(IsDBNull(dr.Item("IdOrdenCompraServicio")), 0, dr.Item("IdOrdenCompraServicio"))
                .IdOrdenCompraEnc = IIf(IsDBNull(dr.Item("IdOrdenCompraEnc")), 0, dr.Item("IdOrdenCompraEnc"))
                .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                .IdAcuerdo = IIf(IsDBNull(dr.Item("IdAcuerdo")), 0, dr.Item("IdAcuerdo"))
                .IdAcuerdoDet = IIf(IsDBNull(dr.Item("IdAcuerdoDet")), 0, dr.Item("IdAcuerdoDet"))
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
                .Fecha_Servicio = IIf(IsDBNull(dr.Item("fecha_servicio")), Date.Now, dr.Item("fecha_servicio"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_oc_servicios As clsBeTrans_oc_servicios, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_oc_servicios")
            Ins.Add("idordencompraservicio", "@idordencompraservicio", DataType.Parametro)
            Ins.Add("idordencompraenc", "@idordencompraenc", DataType.Parametro)
            Ins.Add("idacuerdo", "@idacuerdo", DataType.Parametro)
            Ins.Add("idacuerdodet", "@idacuerdodet", DataType.Parametro)
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
            Ins.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Ins.Add("fecha_servicio", "@fecha_servicio", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRASERVICIO", oBeTrans_oc_servicios.IdOrdenCompraServicio))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_oc_servicios.IdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@IDACUERDO", oBeTrans_oc_servicios.IdAcuerdo))
            cmd.Parameters.Add(New SqlParameter("@IDACUERDODET", oBeTrans_oc_servicios.IdAcuerdoDet))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACION", oBeTrans_oc_servicios.Observacion))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeTrans_oc_servicios.Codigo_producto))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_SERVICIO", oBeTrans_oc_servicios.Nombre_servicio))
            cmd.Parameters.Add(New SqlParameter("@UNID_MEDIDA", oBeTrans_oc_servicios.Unid_medida))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_UNIDAD", oBeTrans_oc_servicios.Nombre_unidad))
            cmd.Parameters.Add(New SqlParameter("@CORRE_DETALLEACUERDO", oBeTrans_oc_servicios.Corre_detalleacuerdo))
            cmd.Parameters.Add(New SqlParameter("@CORRE_CATALOGOPRODUCTOS", oBeTrans_oc_servicios.Corre_catalogoproductos))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_oc_servicios.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_oc_servicios.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_oc_servicios.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_oc_servicios.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_oc_servicios.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_oc_servicios.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@FECHA_SERVICIO", oBeTrans_oc_servicios.Fecha_Servicio))

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

    Public Shared Function Actualizar(ByRef oBeTrans_oc_servicios As clsBeTrans_oc_servicios, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_oc_servicios")
            Upd.Add("idordencompraservicio", "@idordencompraservicio", DataType.Parametro)
            Upd.Add("idordencompraenc", "@idordencompraenc", DataType.Parametro)
            Upd.Add("idacuerdo", "@idacuerdo", DataType.Parametro)
            Upd.Add("idacuerdodet", "@idacuerdodet", DataType.Parametro)
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
            Upd.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Upd.Where("IdOrdenCompraServicio = @IdOrdenCompraServicio")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRASERVICIO", oBeTrans_oc_servicios.IdOrdenCompraServicio))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_oc_servicios.IdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@IDACUERDO", oBeTrans_oc_servicios.IdAcuerdo))
            cmd.Parameters.Add(New SqlParameter("@IDACUERDODET", oBeTrans_oc_servicios.IdAcuerdoDet))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACION", oBeTrans_oc_servicios.Observacion))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeTrans_oc_servicios.Codigo_producto))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_SERVICIO", oBeTrans_oc_servicios.Nombre_servicio))
            cmd.Parameters.Add(New SqlParameter("@UNID_MEDIDA", oBeTrans_oc_servicios.Unid_medida))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_UNIDAD", oBeTrans_oc_servicios.Nombre_unidad))
            cmd.Parameters.Add(New SqlParameter("@CORRE_DETALLEACUERDO", oBeTrans_oc_servicios.Corre_detalleacuerdo))
            cmd.Parameters.Add(New SqlParameter("@CORRE_CATALOGOPRODUCTOS", oBeTrans_oc_servicios.Corre_catalogoproductos))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_oc_servicios.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_oc_servicios.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_oc_servicios.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_oc_servicios.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_oc_servicios.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_oc_servicios.IdPropietarioBodega))

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


    Public Shared Function Eliminar(ByRef oBeTrans_oc_servicios As clsBeTrans_oc_servicios, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_oc_servicios" &
             "  Where(IdOrdenCompraServicio = @IdOrdenCompraServicio)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRASERVICIO", oBeTrans_oc_servicios.IdOrdenCompraServicio))

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

    Public Shared Function Listar() As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_oc_servicios"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_All() As List(Of clsBeTrans_oc_servicios)

        Dim lReturnList As New List(Of clsBeTrans_oc_servicios)

        Try

            Const sp As String = "SELECT * FROM Trans_oc_servicios"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_oc_servicios As New clsBeTrans_oc_servicios

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_oc_servicios = New clsBeTrans_oc_servicios()
                            Cargar(vBeTrans_oc_servicios, dr)
                            lReturnList.Add(vBeTrans_oc_servicios)
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

    Public Shared Sub GetSingle(ByRef pBeTrans_oc_servicios As clsBeTrans_oc_servicios)

        Try

            Const sp As String = "SELECT * FROM Trans_oc_servicios" &
            " Where(IdOrdenCompraServicio = @IdOrdenCompraServicio)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_oc_servicios As New clsBeTrans_oc_servicios

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTrans_oc_servicios, lDataTable.Rows(0))
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

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdOrdenCompraServicio),0) FROM Trans_oc_servicios"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

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
            Throw ex
        End Try

    End Function

End Class