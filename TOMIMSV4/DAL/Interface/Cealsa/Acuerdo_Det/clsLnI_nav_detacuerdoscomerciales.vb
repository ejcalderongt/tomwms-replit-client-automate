Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnI_nav_detacuerdoscomerciales

    Public Shared Sub Cargar(ByRef oBeI_nav_detacuerdoscomerciales As clsBeI_nav_detacuerdoscomerciales, ByRef dr As DataRow)

        Try

            With oBeI_nav_detacuerdoscomerciales

                .Emp = IIf(IsDBNull(dr.Item("emp")), 0, dr.Item("emp"))
                .Nombre_emp = IIf(IsDBNull(dr.Item("nombre_emp")), "", dr.Item("nombre_emp"))
                .Codcliente = IIf(IsDBNull(dr.Item("codcliente")), 0, dr.Item("codcliente"))
                .Nomcliente = IIf(IsDBNull(dr.Item("nomcliente")), "", dr.Item("nomcliente"))
                .Codacuerdo = IIf(IsDBNull(dr.Item("codacuerdo")), 0, dr.Item("codacuerdo"))
                .Descrip = IIf(IsDBNull(dr.Item("descrip")), "", dr.Item("descrip"))
                .Tipocobro = IIf(IsDBNull(dr.Item("tipocobro")), "", dr.Item("tipocobro"))
                .Codmoneda = IIf(IsDBNull(dr.Item("codmoneda")), 0, dr.Item("codmoneda"))
                .Moneda = IIf(IsDBNull(dr.Item("moneda")), "", dr.Item("moneda"))
                .Codigo_producto = IIf(IsDBNull(dr.Item("codigo_producto")), "", dr.Item("codigo_producto"))
                .Servicio = IIf(IsDBNull(dr.Item("servicio")), "", dr.Item("servicio"))
                .Nemonico = IIf(IsDBNull(dr.Item("nemonico")), "", dr.Item("nemonico"))
                .Corre_detalleacuerdo = IIf(IsDBNull(dr.Item("corre_detalleacuerdo")), 0, dr.Item("corre_detalleacuerdo"))
                .Corre_catalogoproductos = IIf(IsDBNull(dr.Item("corre_catalogoproductos")), 0, dr.Item("corre_catalogoproductos"))
                .Unid_medida = IIf(IsDBNull(dr.Item("unid_medida")), 0, dr.Item("unid_medida"))
                .Nombre_unidad = IIf(IsDBNull(dr.Item("nombre_unidad")), "", dr.Item("nombre_unidad"))
                .Procesado_wms = IIf(IsDBNull(dr.Item("procesado_wms")), False, dr.Item("procesado_wms"))
                .Estado = IIf(IsDBNull(dr.Item("estado")), "", dr.Item("estado"))

            End With

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeI_nav_detacuerdoscomerciales As clsBeI_nav_detacuerdoscomerciales, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ERP"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("i_nav_detacuerdoscomerciales")
            Ins.Add("emp", "@emp", DataType.Parametro)
            Ins.Add("nombre_emp", "@nombre_emp", DataType.Parametro)
            Ins.Add("codcliente", "@codcliente", DataType.Parametro)
            Ins.Add("nomcliente", "@nomcliente", DataType.Parametro)
            Ins.Add("codacuerdo", "@codacuerdo", DataType.Parametro)
            Ins.Add("descrip", "@descrip", DataType.Parametro)
            Ins.Add("tipocobro", "@tipocobro", DataType.Parametro)
            Ins.Add("codmoneda", "@codmoneda", DataType.Parametro)
            Ins.Add("moneda", "@moneda", DataType.Parametro)
            Ins.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
            Ins.Add("servicio", "@servicio", DataType.Parametro)
            Ins.Add("nemonico", "@nemonico", DataType.Parametro)
            Ins.Add("corre_detalleacuerdo", "@corre_detalleacuerdo", DataType.Parametro)
            Ins.Add("corre_catalogoproductos", "@corre_catalogoproductos", DataType.Parametro)
            Ins.Add("unid_medida", "@unid_medida", DataType.Parametro)
            Ins.Add("nombre_unidad", "@nombre_unidad", DataType.Parametro)
            Ins.Add("procesado_wms", "@procesado_wms", DataType.Parametro)
            Ins.Add("estado", "@estado", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@EMP", oBeI_nav_detacuerdoscomerciales.Emp))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_EMP", oBeI_nav_detacuerdoscomerciales.Nombre_emp))
            cmd.Parameters.Add(New SqlParameter("@CODCLIENTE", oBeI_nav_detacuerdoscomerciales.Codcliente))
            cmd.Parameters.Add(New SqlParameter("@NOMCLIENTE", oBeI_nav_detacuerdoscomerciales.Nomcliente))
            cmd.Parameters.Add(New SqlParameter("@CODACUERDO", oBeI_nav_detacuerdoscomerciales.Codacuerdo))
            cmd.Parameters.Add(New SqlParameter("@DESCRIP", oBeI_nav_detacuerdoscomerciales.Descrip))
            cmd.Parameters.Add(New SqlParameter("@TIPOCOBRO", oBeI_nav_detacuerdoscomerciales.Tipocobro))
            cmd.Parameters.Add(New SqlParameter("@CODMONEDA", oBeI_nav_detacuerdoscomerciales.Codmoneda))
            cmd.Parameters.Add(New SqlParameter("@MONEDA", oBeI_nav_detacuerdoscomerciales.Moneda))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeI_nav_detacuerdoscomerciales.Codigo_producto))
            cmd.Parameters.Add(New SqlParameter("@SERVICIO", oBeI_nav_detacuerdoscomerciales.Servicio))
            cmd.Parameters.Add(New SqlParameter("@NEMONICO", oBeI_nav_detacuerdoscomerciales.Nemonico))
            cmd.Parameters.Add(New SqlParameter("@CORRE_DETALLEACUERDO", oBeI_nav_detacuerdoscomerciales.Corre_detalleacuerdo))
            cmd.Parameters.Add(New SqlParameter("@CORRE_CATALOGOPRODUCTOS", oBeI_nav_detacuerdoscomerciales.Corre_catalogoproductos))
            cmd.Parameters.Add(New SqlParameter("@UNID_MEDIDA", oBeI_nav_detacuerdoscomerciales.Unid_medida))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_UNIDAD", oBeI_nav_detacuerdoscomerciales.Nombre_unidad))
            cmd.Parameters.Add(New SqlParameter("@PROCESADO_WMS", oBeI_nav_detacuerdoscomerciales.Procesado_wms))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeI_nav_detacuerdoscomerciales.Estado))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeI_nav_detacuerdoscomerciales As clsBeI_nav_detacuerdoscomerciales, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ERP"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("i_nav_detacuerdoscomerciales")
            Upd.Add("emp", "@emp", DataType.Parametro)
            Upd.Add("nombre_emp", "@nombre_emp", DataType.Parametro)
            Upd.Add("codcliente", "@codcliente", DataType.Parametro)
            Upd.Add("nomcliente", "@nomcliente", DataType.Parametro)
            Upd.Add("codacuerdo", "@codacuerdo", DataType.Parametro)
            Upd.Add("descrip", "@descrip", DataType.Parametro)
            Upd.Add("tipocobro", "@tipocobro", DataType.Parametro)
            Upd.Add("codmoneda", "@codmoneda", DataType.Parametro)
            Upd.Add("moneda", "@moneda", DataType.Parametro)
            Upd.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
            Upd.Add("servicio", "@servicio", DataType.Parametro)
            Upd.Add("nemonico", "@nemonico", DataType.Parametro)
            Upd.Add("corre_detalleacuerdo", "@corre_detalleacuerdo", DataType.Parametro)
            Upd.Add("corre_catalogoproductos", "@corre_catalogoproductos", DataType.Parametro)
            Upd.Add("unid_medida", "@unid_medida", DataType.Parametro)
            Upd.Add("nombre_unidad", "@nombre_unidad", DataType.Parametro)
            Upd.Add("procesado_wms", "@procesado_wms", DataType.Parametro)
            Upd.Add("estado", "@estado", DataType.Parametro)
            Upd.Add("estado", "@estado", DataType.Parametro)

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@EMP", oBeI_nav_detacuerdoscomerciales.Emp))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_EMP", oBeI_nav_detacuerdoscomerciales.Nombre_emp))
            cmd.Parameters.Add(New SqlParameter("@CODCLIENTE", oBeI_nav_detacuerdoscomerciales.Codcliente))
            cmd.Parameters.Add(New SqlParameter("@NOMCLIENTE", oBeI_nav_detacuerdoscomerciales.Nomcliente))
            cmd.Parameters.Add(New SqlParameter("@CODACUERDO", oBeI_nav_detacuerdoscomerciales.Codacuerdo))
            cmd.Parameters.Add(New SqlParameter("@DESCRIP", oBeI_nav_detacuerdoscomerciales.Descrip))
            cmd.Parameters.Add(New SqlParameter("@TIPOCOBRO", oBeI_nav_detacuerdoscomerciales.Tipocobro))
            cmd.Parameters.Add(New SqlParameter("@CODMONEDA", oBeI_nav_detacuerdoscomerciales.Codmoneda))
            cmd.Parameters.Add(New SqlParameter("@MONEDA", oBeI_nav_detacuerdoscomerciales.Moneda))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeI_nav_detacuerdoscomerciales.Codigo_producto))
            cmd.Parameters.Add(New SqlParameter("@SERVICIO", oBeI_nav_detacuerdoscomerciales.Servicio))
            cmd.Parameters.Add(New SqlParameter("@NEMONICO", oBeI_nav_detacuerdoscomerciales.Nemonico))
            cmd.Parameters.Add(New SqlParameter("@CORRE_DETALLEACUERDO", oBeI_nav_detacuerdoscomerciales.Corre_detalleacuerdo))
            cmd.Parameters.Add(New SqlParameter("@CORRE_CATALOGOPRODUCTOS", oBeI_nav_detacuerdoscomerciales.Corre_catalogoproductos))
            cmd.Parameters.Add(New SqlParameter("@UNID_MEDIDA", oBeI_nav_detacuerdoscomerciales.Unid_medida))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_UNIDAD", oBeI_nav_detacuerdoscomerciales.Nombre_unidad))
            cmd.Parameters.Add(New SqlParameter("@PROCESADO_WMS", oBeI_nav_detacuerdoscomerciales.Procesado_wms))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeI_nav_detacuerdoscomerciales.Estado))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function


    Public Shared Function Eliminar(ByRef oBeI_nav_detacuerdoscomerciales As clsBeI_nav_detacuerdoscomerciales, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ERP"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from I_nav_detacuerdoscomerciales"

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
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ERP"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM I_nav_detacuerdoscomerciales"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_All() As List(Of clsBeI_nav_detacuerdoscomerciales)

        Dim lReturnList As New List(Of clsBeI_nav_detacuerdoscomerciales)

        Try

            Const sp As String = "SELECT * FROM I_nav_detacuerdoscomerciales"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ERP"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeI_nav_detacuerdoscomerciales As New clsBeI_nav_detacuerdoscomerciales

                        For Each dr As DataRow In lDataTable.Rows
                            vBeI_nav_detacuerdoscomerciales = New clsBeI_nav_detacuerdoscomerciales()
                            Cargar(vBeI_nav_detacuerdoscomerciales, dr)
                            lReturnList.Add(vBeI_nav_detacuerdoscomerciales)
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
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Sub GetSingle(ByRef pBeI_nav_detacuerdoscomerciales As clsBeI_nav_detacuerdoscomerciales)

        Try

            Const sp As String = "SELECT * FROM I_nav_detacuerdoscomerciales"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ERP"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeI_nav_detacuerdoscomerciales As New clsBeI_nav_detacuerdoscomerciales

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeI_nav_detacuerdoscomerciales, lDataTable.Rows(0))
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT * FROM I_nav_detacuerdoscomerciales"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ERP"))

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
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Actualizar_Procesado_WMS(ByVal AcuerdosDet As clsBeI_nav_detacuerdoscomerciales,
                                                    ByVal lConnectionERP As SqlConnection,
                                                    ByVal lTransactionERP As SqlTransaction) As Integer

        Actualizar_Procesado_WMS = 0

        Try

            Upd.Init("i_nav_detacuerdoscomerciales")
            Upd.Add("procesado_wms", "@procesado_wms", DataType.Parametro)
            Upd.Where("codacuerdo =@codacuerdo 
                       and corre_detalleacuerdo = @corre_detalleacuerdo 
                       and corre_catalogoproductos = @corre_catalogoproductos")

            Dim sp As String = Upd.SQL()

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}
            cmd = New SqlCommand(sp, lConnectionERP, lTransactionERP)


            cmd.Parameters.Add(New SqlParameter("@CODACUERDO", AcuerdosDet.Codacuerdo))
            cmd.Parameters.Add(New SqlParameter("@CORRE_DETALLEACUERDO", AcuerdosDet.Corre_detalleacuerdo))
            cmd.Parameters.Add(New SqlParameter("@CORRE_CATALOGOPRODUCTOS", AcuerdosDet.Corre_catalogoproductos))
            cmd.Parameters.Add(New SqlParameter("@PROCESADO_WMS", AcuerdosDet.Procesado_wms))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            Return rowsAffected

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try


    End Function

End Class
