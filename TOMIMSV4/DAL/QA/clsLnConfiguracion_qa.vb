Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnConfiguracion_qa

    Public Shared Sub Cargar(ByRef oBeConfiguracion_qa As clsBeConfiguracion_qa, ByRef dr As DataRow)
        Try
            With oBeConfiguracion_qa
                .IdConfiguracionQA = IIf(IsDBNull(dr.Item("IdConfiguracionQA")), 0, dr.Item("IdConfiguracionQA"))
                .Nombre = IIf(IsDBNull(dr.Item("Nombre")), "", dr.Item("Nombre"))
                .FechaEjecucion = IIf(IsDBNull(dr.Item("FechaEjecucion")), Date.Now, dr.Item("FechaEjecucion"))
                .IdEmpresaOrigen = IIf(IsDBNull(dr.Item("IdEmpresaOrigen")), 0, dr.Item("IdEmpresaOrigen"))
                .IdBodegaOrigen = IIf(IsDBNull(dr.Item("IdBodegaOrigen")), 0, dr.Item("IdBodegaOrigen"))
                .IdPropietarioOrigen = IIf(IsDBNull(dr.Item("IdPropietarioOrigen")), 0, dr.Item("IdPropietarioOrigen"))
                .IdProducto = IIf(IsDBNull(dr.Item("IdProducto")), 0, dr.Item("IdProducto"))
                .IdCliente = IIf(IsDBNull(dr.Item("IdCliente")), 0, dr.Item("IdCliente"))
                .Cantidad_Pedido_Presentacion = IIf(IsDBNull(dr.Item("Cantidad_Pedido_Presentacion")), 0.0, dr.Item("Cantidad_Pedido_Presentacion"))
                .Cantidad_Pedido_UMBas = IIf(IsDBNull(dr.Item("Cantidad_Pedido_UMBas")), 0.0, dr.Item("Cantidad_Pedido_UMBas"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Resultado = IIf(IsDBNull(dr.Item("Resultado")), "", dr.Item("Resultado"))
                .Observaciones = IIf(IsDBNull(dr.Item("Observaciones")), "", dr.Item("Observaciones"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeConfiguracion_qa As clsBeConfiguracion_qa, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("configuracion_qa")
            Ins.Add("idconfiguracionqa", "@idconfiguracionqa", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("fechaejecucion", "@fechaejecucion", DataType.Parametro)
            Ins.Add("idempresaorigen", "@idempresaorigen", DataType.Parametro)
            Ins.Add("idbodegaorigen", "@idbodegaorigen", DataType.Parametro)
            Ins.Add("idpropietarioorigen", "@idpropietarioorigen", DataType.Parametro)
            Ins.Add("idproducto", "@idproducto", DataType.Parametro)
            Ins.Add("idcliente", "@idcliente", DataType.Parametro)
            Ins.Add("cantidad_pedido_presentacion", "@cantidad_pedido_presentacion", DataType.Parametro)
            Ins.Add("cantidad_pedido_umbas", "@cantidad_pedido_umbas", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("resultado", "@resultado", DataType.Parametro)
            Ins.Add("observaciones", "@observaciones", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCONFIGURACIONQA", oBeConfiguracion_qa.IdConfiguracionQA))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeConfiguracion_qa.Nombre))
            cmd.Parameters.Add(New SqlParameter("@FECHAEJECUCION", oBeConfiguracion_qa.FechaEjecucion))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESAORIGEN", oBeConfiguracion_qa.IdEmpresaOrigen))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGAORIGEN", oBeConfiguracion_qa.IdBodegaOrigen))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOORIGEN", oBeConfiguracion_qa.IdPropietarioOrigen))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeConfiguracion_qa.IdProducto))
            cmd.Parameters.Add(New SqlParameter("@IDCLIENTE", oBeConfiguracion_qa.IdCliente))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_PEDIDO_PRESENTACION", oBeConfiguracion_qa.Cantidad_Pedido_Presentacion))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_PEDIDO_UMBAS", oBeConfiguracion_qa.Cantidad_Pedido_UMBas))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeConfiguracion_qa.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeConfiguracion_qa.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeConfiguracion_qa.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeConfiguracion_qa.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeConfiguracion_qa.Activo))
            cmd.Parameters.Add(New SqlParameter("@RESULTADO", oBeConfiguracion_qa.Resultado))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACIONES", oBeConfiguracion_qa.Observaciones))

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

    Public Shared Function Actualizar(ByRef oBeConfiguracion_qa As clsBeConfiguracion_qa, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("configuracion_qa")
            Upd.Add("idconfiguracionqa", "@idconfiguracionqa", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("fechaejecucion", "@fechaejecucion", DataType.Parametro)
            Upd.Add("idempresaorigen", "@idempresaorigen", DataType.Parametro)
            Upd.Add("idbodegaorigen", "@idbodegaorigen", DataType.Parametro)
            Upd.Add("idpropietarioorigen", "@idpropietarioorigen", DataType.Parametro)
            Upd.Add("idproducto", "@idproducto", DataType.Parametro)
            Upd.Add("idcliente", "@idcliente", DataType.Parametro)
            Upd.Add("cantidad_pedido_presentacion", "@cantidad_pedido_presentacion", DataType.Parametro)
            Upd.Add("cantidad_pedido_umbas", "@cantidad_pedido_umbas", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("resultado", "@resultado", DataType.Parametro)
            Upd.Add("observaciones", "@observaciones", DataType.Parametro)
            Upd.Where("IdConfiguracionQA = @IdConfiguracionQA")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCONFIGURACIONQA", oBeConfiguracion_qa.IdConfiguracionQA))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeConfiguracion_qa.Nombre))
            cmd.Parameters.Add(New SqlParameter("@FECHAEJECUCION", oBeConfiguracion_qa.FechaEjecucion))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESAORIGEN", oBeConfiguracion_qa.IdEmpresaOrigen))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGAORIGEN", oBeConfiguracion_qa.IdBodegaOrigen))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOORIGEN", oBeConfiguracion_qa.IdPropietarioOrigen))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeConfiguracion_qa.IdProducto))
            cmd.Parameters.Add(New SqlParameter("@IDCLIENTE", oBeConfiguracion_qa.IdCliente))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_PEDIDO_PRESENTACION", oBeConfiguracion_qa.Cantidad_Pedido_Presentacion))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_PEDIDO_UMBAS", oBeConfiguracion_qa.Cantidad_Pedido_UMBas))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeConfiguracion_qa.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeConfiguracion_qa.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeConfiguracion_qa.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeConfiguracion_qa.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeConfiguracion_qa.Activo))
            cmd.Parameters.Add(New SqlParameter("@RESULTADO", oBeConfiguracion_qa.Resultado))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACIONES", oBeConfiguracion_qa.Observaciones))

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


    Public Shared Function Eliminar(ByRef oBeConfiguracion_qa As clsBeConfiguracion_qa, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Configuracion_qa" & _
             "  Where(IdConfiguracionQA = @IdConfiguracionQA)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCONFIGURACIONQA", oBeConfiguracion_qa.IdConfiguracionQA))

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

            Const sp As String = "SELECT * FROM Configuracion_qa"
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

    Public Shared Function Get_All() As List(Of clsBeConfiguracion_qa)

        Dim lReturnList As New List(Of clsBeConfiguracion_qa)

        Try

            Const sp As String = "SELECT * FROM Configuracion_qa"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeConfiguracion_qa As New clsBeConfiguracion_qa

                        For Each dr As DataRow In lDataTable.Rows
                            vBeConfiguracion_qa = New clsBeConfiguracion_qa()
                            Cargar(vBeConfiguracion_qa, dr)
                            lReturnList.Add(vBeConfiguracion_qa)
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

    Public Shared Sub GetSingle(ByRef pBeConfiguracion_qa As clsBeConfiguracion_qa)

        Try

            Const sp As String = "SELECT * FROM Configuracion_qa" & _
            " Where(IdConfiguracionQA = @IdConfiguracionQA)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        lDTA.SelectCommand.Parameters.AddWithValue("@IdConfiguracionQA", pBeConfiguracion_qa.IdConfiguracionQA)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeConfiguracion_qa As New clsBeConfiguracion_qa

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeConfiguracion_qa, lDataTable.Rows(0))
                            pBeConfiguracion_qa = vBeConfiguracion_qa
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

            Const sp As String = "SELECT ISNULL(Max(IdConfiguracionQA),0) FROM Configuracion_qa"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

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

    Public Shared Function Existe_Configuracion_Por_Defecto() As Boolean

        Try

            Const sp As String = "SELECT top(1) * FROM Configuracion_qa "


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeConfiguracion_qa As New clsBeConfiguracion_qa

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Existe_Configuracion_Por_Defecto = True
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

End Class