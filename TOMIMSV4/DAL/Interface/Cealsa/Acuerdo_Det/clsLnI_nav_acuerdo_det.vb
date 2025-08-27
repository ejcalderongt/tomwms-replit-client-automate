Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnI_nav_acuerdo_det

    Public Shared Sub Cargar(ByRef oBeI_nav_acuerdo_det As clsBeI_nav_acuerdo_det, ByRef dr As DataRow)
        Try
            With oBeI_nav_acuerdo_det
                .IdAcuerdoDet = IIf(IsDBNull(dr.Item("IdAcuerdoDet")), 0, dr.Item("IdAcuerdoDet"))
                .IdAcuerdo = IIf(IsDBNull(dr.Item("IdAcuerdo")), 0, dr.Item("IdAcuerdo"))
                .Codigo_producto = IIf(IsDBNull(dr.Item("codigo_producto")), "", dr.Item("codigo_producto"))
                .Servicio = IIf(IsDBNull(dr.Item("servicio")), "", dr.Item("servicio"))
                .Nemonico = IIf(IsDBNull(dr.Item("nemonico")), "", dr.Item("nemonico"))
                .Codigo_acuerdo = IIf(IsDBNull(dr.Item("codigo_acuerdo")), "", dr.Item("codigo_acuerdo"))
                .Correlativo_detalleacuerdo = IIf(IsDBNull(dr.Item("corre_detalleacuerdo")), 0, dr.Item("corre_detalleacuerdo"))
                .Descripcion = IIf(IsDBNull(dr.Item("descripcion")), "", dr.Item("descripcion"))
                .Numero_unidades = IIf(IsDBNull(dr.Item("numero_unidades")), 0, dr.Item("numero_unidades"))
                .Monto = IIf(IsDBNull(dr.Item("monto")), 0.00, dr.Item("monto"))
                .Porcentaje = IIf(IsDBNull(dr.Item("porcentaje")), 0.00, dr.Item("porcentaje"))
                .Dias_eventos = IIf(IsDBNull(dr.Item("dias_eventos")), 0.00, dr.Item("dias_eventos"))
                .corre_cbcatalogoproductos = IIf(IsDBNull(dr.Item("corre_cbcatalogoproductos")), 0.00, dr.Item("corre_cbcatalogoproductos"))
                .Procesado_wms = IIf(IsDBNull(dr.Item("procesado_wms")), False, dr.Item("procesado_wms"))
                .Estado = IIf(IsDBNull(dr.Item("estado")), False, dr.Item("estado"))
                .Prioridad = IIf(IsDBNull(dr.Item("prioridad")), 0, dr.Item("prioridad"))


            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeI_nav_acuerdo_det As clsBeI_nav_acuerdo_det, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("i_nav_acuerdo_det")
            Ins.Add("idacuerdodet", "@idacuerdodet", DataType.Parametro)
            Ins.Add("idacuerdo", "@idacuerdo", DataType.Parametro)
            Ins.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
            Ins.Add("servicio", "@servicio", DataType.Parametro)
            Ins.Add("nemonico", "@nemonico", DataType.Parametro)
            Ins.Add("codigo_acuerdo", "@codigo_acuerdo", DataType.Parametro)
            Ins.Add("corre_detalleacuerdo", "@corre_detalleacuerdo", DataType.Parametro)
            Ins.Add("descripcion", "@descripcion", DataType.Parametro)
            Ins.Add("numero_unidades", "@numero_unidades", DataType.Parametro)
            Ins.Add("monto", "@monto", DataType.Parametro)
            Ins.Add("porcentaje", "@porcentaje", DataType.Parametro)
            Ins.Add("dias_eventos", "@dias_eventos", DataType.Parametro)
            Ins.Add("corre_cbcatalogoproductos", "@corre_cbcatalogoproductos", DataType.Parametro)
            Ins.Add("procesado_wms", "@procesado_wms", DataType.Parametro)
            Ins.Add("estado", "@estado", DataType.Parametro)
            Ins.Add("prioridad", "@prioridad", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDACUERDODET", oBeI_nav_acuerdo_det.IdAcuerdoDet))
            cmd.Parameters.Add(New SqlParameter("@IDACUERDO", oBeI_nav_acuerdo_det.IdAcuerdo))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeI_nav_acuerdo_det.Codigo_producto))
            cmd.Parameters.Add(New SqlParameter("@SERVICIO", oBeI_nav_acuerdo_det.Servicio))
            cmd.Parameters.Add(New SqlParameter("@NEMONICO", oBeI_nav_acuerdo_det.Nemonico))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_ACUERDO", oBeI_nav_acuerdo_det.Codigo_acuerdo))
            cmd.Parameters.Add(New SqlParameter("@CORRE_DETALLEACUERDO", oBeI_nav_acuerdo_det.Correlativo_detalleacuerdo))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeI_nav_acuerdo_det.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@NUMERO_UNIDADES", oBeI_nav_acuerdo_det.Numero_unidades))
            cmd.Parameters.Add(New SqlParameter("@MONTO", oBeI_nav_acuerdo_det.Monto))
            cmd.Parameters.Add(New SqlParameter("@PORCENTAJE", oBeI_nav_acuerdo_det.Porcentaje))
            cmd.Parameters.Add(New SqlParameter("@DIAS_EVENTOS", oBeI_nav_acuerdo_det.Dias_eventos))
            cmd.Parameters.Add(New SqlParameter("@CORRE_CBCATALOGOPRODUCTOS", oBeI_nav_acuerdo_det.corre_cbcatalogoproductos))
            cmd.Parameters.Add(New SqlParameter("@PROCESADO_WMS", oBeI_nav_acuerdo_det.Procesado_wms))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeI_nav_acuerdo_det.Estado))
            cmd.Parameters.Add(New SqlParameter("@PRIORIDAD", oBeI_nav_acuerdo_det.Prioridad))

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

    Public Shared Function Actualizar(ByRef oBeI_nav_acuerdo_det As clsBeI_nav_acuerdo_det, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("i_nav_acuerdo_det")
            Upd.Add("idacuerdodet", "@idacuerdodet", DataType.Parametro)
            Upd.Add("idacuerdo", "@idacuerdo", DataType.Parametro)
            Upd.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
            Upd.Add("servicio", "@servicio", DataType.Parametro)
            Upd.Add("cod_moneda", "@cod_moneda", DataType.Parametro)
            Upd.Add("nemonico", "@nemonico", DataType.Parametro)
            Upd.Add("corre_detalleacuerdo", "@corre_detalleacuerdo", DataType.Parametro)
            Upd.Add("corre_catalogoproductos", "@corre_catalogoproductos", DataType.Parametro)
            Upd.Add("unid_medida", "@unid_medida", DataType.Parametro)
            Upd.Add("nombre_unidad", "@nombre_unidad", DataType.Parametro)
            Upd.Add("procesado_wms", "@procesado_wms", DataType.Parametro)
            Upd.Add("estado", "@estado", DataType.Parametro)
            Upd.Where("IdAcuerdoDet = @IdAcuerdoDet")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            'cmd.Parameters.Add(New SqlParameter("@IDACUERDODET", oBeI_nav_acuerdo_det.IdAcuerdoDet))
            'cmd.Parameters.Add(New SqlParameter("@IDACUERDO", oBeI_nav_acuerdo_det.IdAcuerdo))
            'cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeI_nav_acuerdo_det.Codigo_producto))
            'cmd.Parameters.Add(New SqlParameter("@SERVICIO", oBeI_nav_acuerdo_det.Servicio))
            'cmd.Parameters.Add(New SqlParameter("@COD_MONEDA", oBeI_nav_acuerdo_det.Cod_moneda))
            'cmd.Parameters.Add(New SqlParameter("@NEMONICO", oBeI_nav_acuerdo_det.Nemonico))
            'cmd.Parameters.Add(New SqlParameter("@CORRE_DETALLEACUERDO", oBeI_nav_acuerdo_det.Corre_detalleacuerdo))
            'cmd.Parameters.Add(New SqlParameter("@CORRE_CATALOGOPRODUCTOS", oBeI_nav_acuerdo_det.Corre_catalogoproductos))
            'cmd.Parameters.Add(New SqlParameter("@UNID_MEDIDA", oBeI_nav_acuerdo_det.Unid_medida))
            'cmd.Parameters.Add(New SqlParameter("@NOMBRE_UNIDAD", oBeI_nav_acuerdo_det.Nombre_unidad))
            'cmd.Parameters.Add(New SqlParameter("@PROCESADO_WMS", oBeI_nav_acuerdo_det.Procesado_wms))
            'cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeI_nav_acuerdo_det.Estado))

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


    Public Shared Function Eliminar(ByRef oBeI_nav_acuerdo_det As clsBeI_nav_acuerdo_det, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from I_nav_acuerdo_det" & _
             "  Where(IdAcuerdoDet = @IdAcuerdoDet)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDACUERDODET", oBeI_nav_acuerdo_det.IdAcuerdoDet))

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

            Const sp As String = "SELECT * FROM I_nav_acuerdo_det"
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

    Public Shared Function Get_All() As List(Of clsBeI_nav_acuerdo_det)

        Dim lReturnList As New List(Of clsBeI_nav_acuerdo_det)

        Try

            Const sp As String = "SELECT * FROM I_nav_acuerdo_det"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeI_nav_acuerdo_det As New clsBeI_nav_acuerdo_det

                        For Each dr As DataRow In lDataTable.Rows
                            vBeI_nav_acuerdo_det = New clsBeI_nav_acuerdo_det()
                            Cargar(vBeI_nav_acuerdo_det, dr)
                            lReturnList.Add(vBeI_nav_acuerdo_det)
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

    Public Shared Sub GetSingle(ByRef pBeI_nav_acuerdo_det As clsBeI_nav_acuerdo_det)

        Try

            Const sp As String = "SELECT * FROM I_nav_acuerdo_det" & _
            " Where(IdAcuerdoDet = @IdAcuerdoDet)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeI_nav_acuerdo_det As New clsBeI_nav_acuerdo_det

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeI_nav_acuerdo_det, lDataTable.Rows(0))
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

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdAcuerdoDet),0) FROM I_nav_acuerdo_det"

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

    Public Shared Function Eliminar(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from I_nav_acuerdo_det "


            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
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

End Class
