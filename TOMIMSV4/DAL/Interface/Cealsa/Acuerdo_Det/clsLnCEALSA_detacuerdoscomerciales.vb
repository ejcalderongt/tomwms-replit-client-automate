Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnCEALSA_detacuerdoscomerciales

    Public Shared Sub Cargar(ByRef oBeCEALSA_detacuerdoscomerciales As clsBeCEALSA_detacuerdoscomerciales, ByRef dr As DataRow)
        Try
            With oBeCEALSA_detacuerdoscomerciales
                .Cod_empresa = IIf(IsDBNull(dr.Item("cod_empresa")), 0, dr.Item("cod_empresa"))
                .Codigoproducto = IIf(IsDBNull(dr.Item("codigoproducto")), "", dr.Item("codigoproducto"))
                .Servicio = IIf(IsDBNull(dr.Item("servicio")), "", dr.Item("servicio"))
                .Nemonico = IIf(IsDBNull(dr.Item("nemonico")), "", dr.Item("nemonico"))
                .Codigo_cliente = IIf(IsDBNull(dr.Item("codigo_cliente")), 0, dr.Item("codigo_cliente"))
                .Corre_cbmaeacuerdosservicios = IIf(IsDBNull(dr.Item("corre_cbmaeacuerdosservicios")), 0, dr.Item("corre_cbmaeacuerdosservicios"))
                .Correlativo = IIf(IsDBNull(dr.Item("correlativo")), 0, dr.Item("correlativo"))
                .Descripcion = IIf(IsDBNull(dr.Item("descripcion")), "", dr.Item("descripcion"))
                .Numero_unidades = IIf(IsDBNull(dr.Item("numero_unidades")), 0.0, dr.Item("numero_unidades"))
                .Monto = IIf(IsDBNull(dr.Item("monto")), 0.0, dr.Item("monto"))
                .Porcentaje = IIf(IsDBNull(dr.Item("porcentaje")), 0.0, dr.Item("porcentaje"))
                .Dias_eventos = IIf(IsDBNull(dr.Item("dias_eventos")), 0, dr.Item("dias_eventos"))
                ''.Numero_unidades_t = IIf(IsDBNull(dr.Item("numero_unidades_t")), 0, dr.Item("numero_unidades_t"))
                '.Monto_t = IIf(IsDBNull(dr.Item("monto_t")), 0, dr.Item("monto_t"))
                '.Porcentaje_t = IIf(IsDBNull(dr.Item("porcentaje_t")), 0, dr.Item("porcentaje_t"))
                '.Dias_t = IIf(IsDBNull(dr.Item("dias_t")), 0, dr.Item("dias_t"))
                .Corre_cbcatalogoproductos = IIf(IsDBNull(dr.Item("corre_cbcatalogoproductos")), 0, dr.Item("corre_cbcatalogoproductos"))
                .Estado = IIf(IsDBNull(dr.Item("estado")), 0, dr.Item("estado"))
                .Prioridad = IIf(IsDBNull(dr.Item("prioridad")), 0.0, dr.Item("prioridad"))

            End With
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function Listar() As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ERP"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM detacuerdoscomerciales"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction()
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

    Public Shared Function GetAll() As List(Of clsBeCEALSA_detacuerdoscomerciales)

        Dim lReturnList As New List(Of clsBeCEALSA_detacuerdoscomerciales)

        Try

            Const sp As String = "SELECT * FROM detacuerdoscomerciales"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ERP"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction()

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeCEALSA_detacuerdoscomerciales As New clsBeCEALSA_detacuerdoscomerciales

                        For Each dr As DataRow In lDataTable.Rows
                            vBeCEALSA_detacuerdoscomerciales = New clsBeCEALSA_detacuerdoscomerciales()
                            Cargar(vBeCEALSA_detacuerdoscomerciales, dr)
                            lReturnList.Add(vBeCEALSA_detacuerdoscomerciales)
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

    Public Shared Sub GetSingle(ByRef pBeCEALSA_detacuerdoscomerciales As clsBeCEALSA_detacuerdoscomerciales)

        Try

            Const sp As String = "SELECT * FROM detacuerdoscomerciales where corre_cbmaeacuerdosservicios=@corre_cbmaeacuerdosservicios"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ERP"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction()

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeCEALSA_detacuerdoscomerciales As New clsBeCEALSA_detacuerdoscomerciales

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeCEALSA_detacuerdoscomerciales, lDataTable.Rows(0))
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

    End Sub

    Public Shared Function Actualizar(ByRef oBeCEALSA_detacuerdoscomerciales As clsBeCEALSA_detacuerdoscomerciales, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ERP"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("detacuerdoscomerciales")
            Upd.Add("cod_empresa", "@cod_empresa", DataType.Parametro)
            Upd.Add("codigoproducto", "@codigoproducto", DataType.Parametro)
            Upd.Add("servicio", "@servicio", DataType.Parametro)
            Upd.Add("nemonico", "@nemonico", DataType.Parametro)
            Upd.Add("codigo_cliente", "@codigo_cliente", DataType.Parametro)
            Upd.Add("corre_cbmaeacuerdosservicios", "@corre_cbmaeacuerdosservicios", DataType.Parametro)
            Upd.Add("correlativo", "@correlativo", DataType.Parametro)
            Upd.Add("descripcion", "@descripcion", DataType.Parametro)
            Upd.Add("numero_unidades", "@numero_unidades", DataType.Parametro)
            Upd.Add("dias_eventos", "@dias_eventos", DataType.Parametro)
            Upd.Add("corre_cbcatalogoproductos", "@corre_cbcatalogoproductos", DataType.Parametro)
            Upd.Add("corre_cbcatalogoproductos", "@corre_cbcatalogoproductos", DataType.Parametro)

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction()
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@COD_EMPRESA", oBeCEALSA_detacuerdoscomerciales.Cod_empresa))
            cmd.Parameters.Add(New SqlParameter("@CODIGOPRODUCTO", oBeCEALSA_detacuerdoscomerciales.Codigoproducto))
            cmd.Parameters.Add(New SqlParameter("@SERVICIO", oBeCEALSA_detacuerdoscomerciales.Servicio))
            cmd.Parameters.Add(New SqlParameter("@NEMONICO", oBeCEALSA_detacuerdoscomerciales.Nemonico))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_CLIENTE", oBeCEALSA_detacuerdoscomerciales.Codigo_cliente))
            cmd.Parameters.Add(New SqlParameter("@CORRE_CBMAEACUERDOSSERVICIOS", oBeCEALSA_detacuerdoscomerciales.Corre_cbmaeacuerdosservicios))
            cmd.Parameters.Add(New SqlParameter("@CORRELATIVO", oBeCEALSA_detacuerdoscomerciales.Correlativo))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeCEALSA_detacuerdoscomerciales.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@NUMERO_UNIDADES", oBeCEALSA_detacuerdoscomerciales.Numero_unidades))
            cmd.Parameters.Add(New SqlParameter("@DIAS_EVENTOS", oBeCEALSA_detacuerdoscomerciales.Dias_eventos))
            cmd.Parameters.Add(New SqlParameter("@CORRE_CBCATALOGOPRODUCTOS", oBeCEALSA_detacuerdoscomerciales.Corre_cbcatalogoproductos))

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

    Public Shared Function Insertar(ByRef oBeCealsa_vwacuerdocomercialdet As clsBeCEALSA_detacuerdoscomerciales, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_acuerdoscomerciales_det")
            Ins.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
            Ins.Add("servicio", "@servicio", DataType.Parametro)
            Ins.Add("nemonico", "@nemonico", DataType.Parametro)
            Ins.Add("codigo_cliente", "@codigo_cliente", DataType.Parametro)
            Ins.Add("corre_cbmaeacuerdosservicios", "@corre_cbmaeacuerdosservicios", DataType.Parametro)
            Ins.Add("correlativo", "@correlativo", DataType.Parametro)
            Ins.Add("descripcion", "@descripcion", DataType.Parametro)
            Ins.Add("numero_unidades", "@numero_unidades", DataType.Parametro)
            Ins.Add("dias_eventos", "@dias_eventos", DataType.Parametro)
            Ins.Add("corre_cbcatalogoproductos", "@corre_cbcatalogoproductos", DataType.Parametro)
            Ins.Add("monto", "@monto", DataType.Parametro)
            Ins.Add("porcentaje", "@porcentaje", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeCealsa_vwacuerdocomercialdet.Codigoproducto))
            cmd.Parameters.Add(New SqlParameter("@SERVICIO", oBeCealsa_vwacuerdocomercialdet.Servicio))
            cmd.Parameters.Add(New SqlParameter("@NEMONICO", oBeCealsa_vwacuerdocomercialdet.Nemonico))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_CLIENTE", oBeCealsa_vwacuerdocomercialdet.Codigo_cliente))
            cmd.Parameters.Add(New SqlParameter("@CORRE_CBMAEACUERDOSSERVICIOS", oBeCealsa_vwacuerdocomercialdet.Corre_cbmaeacuerdosservicios))
            cmd.Parameters.Add(New SqlParameter("@CORRELATIVO", oBeCealsa_vwacuerdocomercialdet.Correlativo))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeCealsa_vwacuerdocomercialdet.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@NUMERO_UNIDADES", oBeCealsa_vwacuerdocomercialdet.Numero_unidades))
            cmd.Parameters.Add(New SqlParameter("@DIAS_EVENTOS", oBeCealsa_vwacuerdocomercialdet.Dias_eventos))
            cmd.Parameters.Add(New SqlParameter("@CORRE_CBCATALOGOPRODUCTOS", oBeCealsa_vwacuerdocomercialdet.Corre_cbcatalogoproductos))
            cmd.Parameters.Add(New SqlParameter("@MONTO", oBeCealsa_vwacuerdocomercialdet.Monto))
            cmd.Parameters.Add(New SqlParameter("@PORCENTAJE", oBeCealsa_vwacuerdocomercialdet.Porcentaje))

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

    '#GT13052024: actualizar la tabla i_nav_acuerdoscomerciales_det en aritecdb_pruebas
    Public Shared Function Actualizar_Procesado_Det(ByRef oBeTransAcuerdo_Det As clsBeI_nav_acuerdo_det,
                                                    Optional ByVal pConection As SqlConnection = Nothing,
                                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

        Try

            Upd.Init("i_nav_acuerdoscomerciales_det")
            Upd.Add("procesado_wms", "@procesado_wms", DataType.Parametro)
            Upd.Where("codigo_cliente=@codigo_cliente and corre_cbmaeacuerdosservicios=@codigo_acuerdo and correlativo=@correlativo")

            Dim sp As String = Upd.SQL()

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            '#CKFK 20210312 Reemplacé el IdCliente por el Codigo cliente
            cmd.Parameters.Add(New SqlParameter("@CODIGO_CLIENTE", oBeTransAcuerdo_Det.IdCliente))
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
