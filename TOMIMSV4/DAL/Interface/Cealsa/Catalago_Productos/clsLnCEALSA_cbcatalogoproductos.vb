Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnCEALSA_cbcatalogoproductos

    Public Shared Sub Cargar(ByRef oBeCEALSA_cbcatalogoproductos As clsBeCEALSA_cbcatalogoproductos, ByRef dr As DataRow)
        Try
            With oBeCEALSA_cbcatalogoproductos
                .Cod_empresa = IIf(IsDBNull(dr.Item("cod_empresa")), 0, dr.Item("cod_empresa"))
                .Codigoproducto = IIf(IsDBNull(dr.Item("codigoproducto")), "", dr.Item("codigoproducto"))
                .Descripcion = IIf(IsDBNull(dr.Item("descripcion")), "", dr.Item("descripcion"))
                .Nemonico = IIf(IsDBNull(dr.Item("nemonico")), "", dr.Item("nemonico"))
                .Codigo_rubro = IIf(IsDBNull(dr.Item("codigo_rubro")), 0, dr.Item("codigo_rubro"))
                .Movimiento = IIf(IsDBNull(dr.Item("movimiento")), False, dr.Item("movimiento"))
                .Estado = IIf(IsDBNull(dr.Item("estado")), "", dr.Item("estado"))
                .Cod_centro = IIf(IsDBNull(dr.Item("cod_centro")), "", dr.Item("cod_centro"))
                .Cod_cuentaxcobrar = IIf(IsDBNull(dr.Item("cod_cuentaxcobrar")), "", dr.Item("cod_cuentaxcobrar"))
                .Cod_cuentaproducto = IIf(IsDBNull(dr.Item("cod_cuentaproducto")), "", dr.Item("cod_cuentaproducto"))
                .Usuario = IIf(IsDBNull(dr.Item("usuario")), "", dr.Item("usuario"))
                .Fechamov = IIf(IsDBNull(dr.Item("fechamov")), Date.Now, dr.Item("fechamov"))
                .Control = IIf(IsDBNull(dr.Item("control")), 0, dr.Item("control"))
                .Correlativo = IIf(IsDBNull(dr.Item("correlativo")), 0, dr.Item("correlativo"))
                .Cod_cuentapasivodiferido = IIf(IsDBNull(dr.Item("cod_cuentapasivodiferido")), "", dr.Item("cod_cuentapasivodiferido"))
                .Cod_cuenta_dif_cxc = IIf(IsDBNull(dr.Item("cod_cuenta_dif_cxc")), "", dr.Item("cod_cuenta_dif_cxc"))
                .Cod_cuenta_dif_pasdif = IIf(IsDBNull(dr.Item("cod_cuenta_dif_pasdif")), "", dr.Item("cod_cuenta_dif_pasdif"))
                .Cod_cuentaxcobrar_me = IIf(IsDBNull(dr.Item("cod_cuentaxcobrar_me")), "", dr.Item("cod_cuentaxcobrar_me"))
                .Cod_cuentapasivodiferido_me = IIf(IsDBNull(dr.Item("cod_cuentapasivodiferido_me")), "", dr.Item("cod_cuentapasivodiferido_me"))
                .Corre_cbcesantes = IIf(IsDBNull(dr.Item("corre_cbcesantes")), 0, dr.Item("corre_cbcesantes"))
                .Montominimo = IIf(IsDBNull(dr.Item("montominimo")), 0.0, dr.Item("montominimo"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeCEALSA_cbcatalogoproductos As clsBeCEALSA_cbcatalogoproductos, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ERP"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("cealsa_cbcatalogoproductos")
            Ins.Add("cod_empresa", "@cod_empresa", DataType.Parametro)
            Ins.Add("codigoproducto", "@codigoproducto", DataType.Parametro)
            Ins.Add("descripcion", "@descripcion", DataType.Parametro)
            Ins.Add("nemonico", "@nemonico", DataType.Parametro)
            Ins.Add("codigo_rubro", "@codigo_rubro", DataType.Parametro)
            Ins.Add("movimiento", "@movimiento", DataType.Parametro)
            Ins.Add("estado", "@estado", DataType.Parametro)
            Ins.Add("cod_centro", "@cod_centro", DataType.Parametro)
            Ins.Add("cod_cuentaxcobrar", "@cod_cuentaxcobrar", DataType.Parametro)
            Ins.Add("cod_cuentaproducto", "@cod_cuentaproducto", DataType.Parametro)
            Ins.Add("usuario", "@usuario", DataType.Parametro)
            Ins.Add("fechamov", "@fechamov", DataType.Parametro)
            Ins.Add("control", "@control", DataType.Parametro)
            Ins.Add("correlativo", "@correlativo", DataType.Parametro)
            Ins.Add("cod_cuentapasivodiferido", "@cod_cuentapasivodiferido", DataType.Parametro)
            Ins.Add("cod_cuenta_dif_cxc", "@cod_cuenta_dif_cxc", DataType.Parametro)
            Ins.Add("cod_cuenta_dif_pasdif", "@cod_cuenta_dif_pasdif", DataType.Parametro)
            Ins.Add("cod_cuentaxcobrar_me", "@cod_cuentaxcobrar_me", DataType.Parametro)
            Ins.Add("cod_cuentapasivodiferido_me", "@cod_cuentapasivodiferido_me", DataType.Parametro)
            Ins.Add("corre_cbcesantes", "@corre_cbcesantes", DataType.Parametro)
            Ins.Add("montominimo", "@montominimo", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@COD_EMPRESA", oBeCEALSA_cbcatalogoproductos.Cod_empresa))
            cmd.Parameters.Add(New SqlParameter("@CODIGOPRODUCTO", oBeCEALSA_cbcatalogoproductos.Codigoproducto))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeCEALSA_cbcatalogoproductos.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@NEMONICO", oBeCEALSA_cbcatalogoproductos.Nemonico))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_RUBRO", oBeCEALSA_cbcatalogoproductos.Codigo_rubro))
            cmd.Parameters.Add(New SqlParameter("@MOVIMIENTO", oBeCEALSA_cbcatalogoproductos.Movimiento))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeCEALSA_cbcatalogoproductos.Estado))
            cmd.Parameters.Add(New SqlParameter("@COD_CENTRO", oBeCEALSA_cbcatalogoproductos.Cod_centro))
            cmd.Parameters.Add(New SqlParameter("@COD_CUENTAXCOBRAR", oBeCEALSA_cbcatalogoproductos.Cod_cuentaxcobrar))
            cmd.Parameters.Add(New SqlParameter("@COD_CUENTAPRODUCTO", oBeCEALSA_cbcatalogoproductos.Cod_cuentaproducto))
            cmd.Parameters.Add(New SqlParameter("@USUARIO", oBeCEALSA_cbcatalogoproductos.Usuario))
            cmd.Parameters.Add(New SqlParameter("@FECHAMOV", oBeCEALSA_cbcatalogoproductos.Fechamov))
            cmd.Parameters.Add(New SqlParameter("@CONTROL", oBeCEALSA_cbcatalogoproductos.Control))
            cmd.Parameters.Add(New SqlParameter("@CORRELATIVO", oBeCEALSA_cbcatalogoproductos.Correlativo))
            cmd.Parameters.Add(New SqlParameter("@COD_CUENTAPASIVODIFERIDO", oBeCEALSA_cbcatalogoproductos.Cod_cuentapasivodiferido))
            cmd.Parameters.Add(New SqlParameter("@COD_CUENTA_DIF_CXC", oBeCEALSA_cbcatalogoproductos.Cod_cuenta_dif_cxc))
            cmd.Parameters.Add(New SqlParameter("@COD_CUENTA_DIF_PASDIF", oBeCEALSA_cbcatalogoproductos.Cod_cuenta_dif_pasdif))
            cmd.Parameters.Add(New SqlParameter("@COD_CUENTAXCOBRAR_ME", oBeCEALSA_cbcatalogoproductos.Cod_cuentaxcobrar_me))
            cmd.Parameters.Add(New SqlParameter("@COD_CUENTAPASIVODIFERIDO_ME", oBeCEALSA_cbcatalogoproductos.Cod_cuentapasivodiferido_me))
            cmd.Parameters.Add(New SqlParameter("@CORRE_CBCESANTES", oBeCEALSA_cbcatalogoproductos.Corre_cbcesantes))
            cmd.Parameters.Add(New SqlParameter("@MONTOMINIMO", oBeCEALSA_cbcatalogoproductos.Montominimo))

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

    Public Shared Function Actualizar(ByRef oBeCEALSA_cbcatalogoproductos As clsBeCEALSA_cbcatalogoproductos, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ERP"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("cealsa_cbcatalogoproductos")
            Upd.Add("cod_empresa", "@cod_empresa", DataType.Parametro)
            Upd.Add("codigoproducto", "@codigoproducto", DataType.Parametro)
            Upd.Add("descripcion", "@descripcion", DataType.Parametro)
            Upd.Add("nemonico", "@nemonico", DataType.Parametro)
            Upd.Add("codigo_rubro", "@codigo_rubro", DataType.Parametro)
            Upd.Add("movimiento", "@movimiento", DataType.Parametro)
            Upd.Add("estado", "@estado", DataType.Parametro)
            Upd.Add("cod_centro", "@cod_centro", DataType.Parametro)
            Upd.Add("cod_cuentaxcobrar", "@cod_cuentaxcobrar", DataType.Parametro)
            Upd.Add("cod_cuentaproducto", "@cod_cuentaproducto", DataType.Parametro)
            Upd.Add("usuario", "@usuario", DataType.Parametro)
            Upd.Add("fechamov", "@fechamov", DataType.Parametro)
            Upd.Add("control", "@control", DataType.Parametro)
            Upd.Add("correlativo", "@correlativo", DataType.Parametro)
            Upd.Add("cod_cuentapasivodiferido", "@cod_cuentapasivodiferido", DataType.Parametro)
            Upd.Add("cod_cuenta_dif_cxc", "@cod_cuenta_dif_cxc", DataType.Parametro)
            Upd.Add("cod_cuenta_dif_pasdif", "@cod_cuenta_dif_pasdif", DataType.Parametro)
            Upd.Add("cod_cuentaxcobrar_me", "@cod_cuentaxcobrar_me", DataType.Parametro)
            Upd.Add("cod_cuentapasivodiferido_me", "@cod_cuentapasivodiferido_me", DataType.Parametro)
            Upd.Add("corre_cbcesantes", "@corre_cbcesantes", DataType.Parametro)
            Upd.Add("montominimo", "@montominimo", DataType.Parametro)
            Upd.Add("montominimo", "@montominimo", DataType.Parametro)

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@COD_EMPRESA", oBeCEALSA_cbcatalogoproductos.Cod_empresa))
            cmd.Parameters.Add(New SqlParameter("@CODIGOPRODUCTO", oBeCEALSA_cbcatalogoproductos.Codigoproducto))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeCEALSA_cbcatalogoproductos.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@NEMONICO", oBeCEALSA_cbcatalogoproductos.Nemonico))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_RUBRO", oBeCEALSA_cbcatalogoproductos.Codigo_rubro))
            cmd.Parameters.Add(New SqlParameter("@MOVIMIENTO", oBeCEALSA_cbcatalogoproductos.Movimiento))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeCEALSA_cbcatalogoproductos.Estado))
            cmd.Parameters.Add(New SqlParameter("@COD_CENTRO", oBeCEALSA_cbcatalogoproductos.Cod_centro))
            cmd.Parameters.Add(New SqlParameter("@COD_CUENTAXCOBRAR", oBeCEALSA_cbcatalogoproductos.Cod_cuentaxcobrar))
            cmd.Parameters.Add(New SqlParameter("@COD_CUENTAPRODUCTO", oBeCEALSA_cbcatalogoproductos.Cod_cuentaproducto))
            cmd.Parameters.Add(New SqlParameter("@USUARIO", oBeCEALSA_cbcatalogoproductos.Usuario))
            cmd.Parameters.Add(New SqlParameter("@FECHAMOV", oBeCEALSA_cbcatalogoproductos.Fechamov))
            cmd.Parameters.Add(New SqlParameter("@CONTROL", oBeCEALSA_cbcatalogoproductos.Control))
            cmd.Parameters.Add(New SqlParameter("@CORRELATIVO", oBeCEALSA_cbcatalogoproductos.Correlativo))
            cmd.Parameters.Add(New SqlParameter("@COD_CUENTAPASIVODIFERIDO", oBeCEALSA_cbcatalogoproductos.Cod_cuentapasivodiferido))
            cmd.Parameters.Add(New SqlParameter("@COD_CUENTA_DIF_CXC", oBeCEALSA_cbcatalogoproductos.Cod_cuenta_dif_cxc))
            cmd.Parameters.Add(New SqlParameter("@COD_CUENTA_DIF_PASDIF", oBeCEALSA_cbcatalogoproductos.Cod_cuenta_dif_pasdif))
            cmd.Parameters.Add(New SqlParameter("@COD_CUENTAXCOBRAR_ME", oBeCEALSA_cbcatalogoproductos.Cod_cuentaxcobrar_me))
            cmd.Parameters.Add(New SqlParameter("@COD_CUENTAPASIVODIFERIDO_ME", oBeCEALSA_cbcatalogoproductos.Cod_cuentapasivodiferido_me))
            cmd.Parameters.Add(New SqlParameter("@CORRE_CBCESANTES", oBeCEALSA_cbcatalogoproductos.Corre_cbcesantes))
            cmd.Parameters.Add(New SqlParameter("@MONTOMINIMO", oBeCEALSA_cbcatalogoproductos.Montominimo))

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


    Public Shared Function Eliminar(ByRef oBeCEALSA_cbcatalogoproductos As clsBeCEALSA_cbcatalogoproductos, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ERP"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from CEALSA_cbcatalogoproductos"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

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
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ERP"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM CEALSA_cbcatalogoproductos"
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

    Public Shared Function GetAll() As List(Of clsBeCEALSA_cbcatalogoproductos)

        Dim lReturnList As New List(Of clsBeCEALSA_cbcatalogoproductos)

        Try

            Const sp As String = "SELECT * FROM cbcatalogoproductos where estado = 'A'"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ERP"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeCEALSA_cbcatalogoproductos As New clsBeCEALSA_cbcatalogoproductos

                        For Each dr As DataRow In lDataTable.Rows
                            vBeCEALSA_cbcatalogoproductos = New clsBeCEALSA_cbcatalogoproductos()
                            Cargar(vBeCEALSA_cbcatalogoproductos, dr)
                            lReturnList.Add(vBeCEALSA_cbcatalogoproductos)
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

    Public Shared Sub GetSingle(ByRef pBeCEALSA_cbcatalogoproductos As clsBeCEALSA_cbcatalogoproductos)

        Try

            Const sp As String = "SELECT * FROM CEALSA_cbcatalogoproductos"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ERP"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeCEALSA_cbcatalogoproductos As New clsBeCEALSA_cbcatalogoproductos

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeCEALSA_cbcatalogoproductos, lDataTable.Rows(0))
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

            Const sp As String = "SELECT * FROM CEALSA_cbcatalogoproductos"

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
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

End Class
