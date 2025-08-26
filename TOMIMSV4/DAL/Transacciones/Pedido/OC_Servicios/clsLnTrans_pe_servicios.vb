Imports System.Data.SqlClient

Public Class clsLnTrans_pe_servicios

    Public Shared Sub Cargar(ByRef oBeTrans_pe_servicios As clsBeTrans_pe_servicios, ByRef dr As DataRow)
        Try
            With oBeTrans_pe_servicios
                .IdOrdenPedidoServicio = IIf(IsDBNull(dr.Item("IdOrdenPedidoServicio")), 0, dr.Item("IdOrdenPedidoServicio"))
                .IdOrdenPedidoEnc = IIf(IsDBNull(dr.Item("IdOrdenPedidoEnc")), 0, dr.Item("IdOrdenPedidoEnc"))
                .IdServicio = IIf(IsDBNull(dr.Item("IdServicio")), 0, dr.Item("IdServicio"))
                .IdAcuerdo = IIf(IsDBNull(dr.Item("IdAcuerdo")), 0, dr.Item("IdAcuerdo"))
                .IdAcuerdoDet = IIf(IsDBNull(dr.Item("IdAcuerdoDet")), 0, dr.Item("IdAcuerdoDet"))
                .Observacion = IIf(IsDBNull(dr.Item("observacion")), "", dr.Item("observacion"))
                .Codigo_producto = IIf(IsDBNull(dr.Item("codigo_producto")), "", dr.Item("codigo_producto"))
                .Nombre_servicio = IIf(IsDBNull(dr.Item("nombre_servicio")), "", dr.Item("nombre_servicio"))
                .Unid_medida = IIf(IsDBNull(dr.Item("unid_medida")), "", dr.Item("unid_medida"))
                .Nombre_unidad = IIf(IsDBNull(dr.Item("nombre_unidad")), "", dr.Item("nombre_unidad"))
                .Corre_detalleacuerdo = IIf(IsDBNull(dr.Item("corre_detalleacuerdo")), "", dr.Item("corre_detalleacuerdo"))
                .Corre_catalogoproductos = IIf(IsDBNull(dr.Item("corre_catalogoproductos")), "", dr.Item("corre_catalogoproductos"))
                .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), "", dr.Item("IdPropietarioBodega"))
                .Cantidad = IIf(IsDBNull(dr.Item("cantidad")), 0, dr.Item("cantidad"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))

            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_pe_servicios As clsBeTrans_pe_servicios, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_pe_servicios")
            Ins.Add("idordenpedidoservicio", "@idordenpedidoservicio", DataType.Parametro)
            Ins.Add("idordenpedidoenc", "@idordenpedidoenc", DataType.Parametro)
            Ins.Add("idservicio", "@idservicio", DataType.Parametro)

            Ins.Add("IdAcuerdo", "@IdAcuerdo", DataType.Parametro)
            Ins.Add("IdAcuerdoDet", "@IdAcuerdoDet", DataType.Parametro)
            Ins.Add("observacion", "@observacion", DataType.Parametro)
            Ins.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
            Ins.Add("nombre_servicio", "@nombre_servicio", DataType.Parametro)
            Ins.Add("unid_medida", "@unid_medida", DataType.Parametro)
            Ins.Add("nombre_unidad", "@nombre_unidad", DataType.Parametro)
            Ins.Add("corre_detalleacuerdo", "@corre_detalleacuerdo", DataType.Parametro)
            Ins.Add("corre_catalogoproductos", "@corre_catalogoproductos", DataType.Parametro)
            Ins.Add("cantidad", "@cantidad", DataType.Parametro)
            Ins.Add("IdPropietarioBodega", "@IdPropietarioBodega", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDORDENPEDIDOSERVICIO", oBeTrans_pe_servicios.IdOrdenPedidoServicio))
            cmd.Parameters.Add(New SqlParameter("@IDORDENPEDIDOENC", oBeTrans_pe_servicios.IdOrdenPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDSERVICIO", oBeTrans_pe_servicios.IdServicio))

            cmd.Parameters.Add(New SqlParameter("@IDACUERDO", oBeTrans_pe_servicios.IdAcuerdo))
            cmd.Parameters.Add(New SqlParameter("@IDACUERDODET", oBeTrans_pe_servicios.IdAcuerdoDet))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACION", oBeTrans_pe_servicios.Observacion))

            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeTrans_pe_servicios.Codigo_producto))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_SERVICIO", oBeTrans_pe_servicios.Nombre_servicio))
            cmd.Parameters.Add(New SqlParameter("@UNID_MEDIDA", oBeTrans_pe_servicios.Unid_medida))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_UNIDAD", oBeTrans_pe_servicios.Nombre_unidad))
            cmd.Parameters.Add(New SqlParameter("@CORRE_DETALLEACUERDO", oBeTrans_pe_servicios.Corre_detalleacuerdo))
            cmd.Parameters.Add(New SqlParameter("@CORRE_CATALOGOPRODUCTOS", oBeTrans_pe_servicios.Corre_catalogoproductos))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_pe_servicios.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_pe_servicios.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_pe_servicios.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_pe_servicios.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_pe_servicios.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_pe_servicios.Fec_mod))


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

    Public Shared Function Actualizar(ByRef oBeTrans_pe_servicios As clsBeTrans_pe_servicios, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_pe_servicios")
            Upd.Add("idordenpedidoservicio", "@idordenpedidoservicio", DataType.Parametro)
            Upd.Add("idordenpedidoenc", "@idordenpedidoenc", DataType.Parametro)
            Upd.Add("idservicio", "@idservicio", DataType.Parametro)
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("observacion", "@observacion", DataType.Parametro)
            Upd.Where("IdOrdenPedidoServicio = @IdOrdenPedidoServicio")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDORDENPEDIDOSERVICIO", oBeTrans_pe_servicios.IdOrdenPedidoServicio))
            cmd.Parameters.Add(New SqlParameter("@IDORDENPEDIDOENC", oBeTrans_pe_servicios.IdOrdenPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDSERVICIO", oBeTrans_pe_servicios.IdServicio))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_pe_servicios.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_pe_servicios.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_pe_servicios.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_pe_servicios.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_pe_servicios.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACION", oBeTrans_pe_servicios.Observacion))

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

    Public Shared Function Eliminar(ByRef oBeTrans_pe_servicios As clsBeTrans_pe_servicios, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_pe_servicios" &
             "  Where(IdOrdenPedidoServicio = @IdOrdenPedidoServicio)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDORDENPEDIDOSERVICIO", oBeTrans_pe_servicios.IdOrdenPedidoServicio))

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

    Public Shared Function Listar() As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_pe_servicios"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_All() As List(Of clsBeTrans_pe_servicios)

        Dim lReturnList As New List(Of clsBeTrans_pe_servicios)

        Try

            Const sp As String = "SELECT * FROM Trans_pe_servicios"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_pe_servicios As New clsBeTrans_pe_servicios

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_pe_servicios = New clsBeTrans_pe_servicios()
                            Cargar(vBeTrans_pe_servicios, dr)
                            lReturnList.Add(vBeTrans_pe_servicios)
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

    Public Shared Sub GetSingle(ByRef pBeTrans_pe_servicios As clsBeTrans_pe_servicios)

        Try

            Const sp As String = "SELECT * FROM Trans_pe_servicios" &
            " Where(IdOrdenPedidoServicio = @IdOrdenPedidoServicio)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_pe_servicios As New clsBeTrans_pe_servicios

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTrans_pe_servicios, lDataTable.Rows(0))
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

    Public Shared Function MaxID(ByVal pConnection As SqlConnection,
                                 ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim sp As String = String.Format("SELECT ISNULL(Max(IdOrdenPedidoServicio),0) FROM Trans_pe_servicios ")

            Using lCommand As New SqlCommand(sp, pConnection, pTransaction) With {.CommandType = CommandType.Text}

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

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdOrdenPedidoServicio),0) FROM Trans_pe_servicios"

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
