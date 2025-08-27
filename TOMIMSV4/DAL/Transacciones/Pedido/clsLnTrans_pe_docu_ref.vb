Imports System.Data.SqlClient

Public Class clsLnTrans_pe_docu_ref

    Public Shared Sub Cargar(ByRef oBeTrans_pe_docu_ref As clsBeTrans_pe_docu_ref, ByRef dr As DataRow)

        Try

            With oBeTrans_pe_docu_ref

                .IdDocumentoRef = IIf(IsDBNull(dr.Item("IdDocumentoRef")), 0, dr.Item("IdDocumentoRef"))
                .Codigo = IIf(IsDBNull(dr.Item("Codigo")), "", dr.Item("Codigo"))
                .Nombre = IIf(IsDBNull(dr.Item("Nombre")), "", dr.Item("Nombre"))
                .Descripcion = IIf(IsDBNull(dr.Item("Descripcion")), "", dr.Item("Descripcion"))
                .FechaDocumento = IIf(IsDBNull(dr.Item("FechaDocumento")), Date.Now, dr.Item("FechaDocumento"))
                .FechaAsignacion = IIf(IsDBNull(dr.Item("FechaAsignacion")), Date.Now, dr.Item("FechaAsignacion"))
                .FechaAgregado = IIf(IsDBNull(dr.Item("FechaAgregado")), Date.Now, dr.Item("FechaAgregado"))
                .Asignado = IIf(IsDBNull(dr.Item("Asignado")), False, dr.Item("Asignado"))
                .Activo = IIf(IsDBNull(dr.Item("Activo")), False, dr.Item("Activo"))
                .Empresa = IIf(IsDBNull(dr.Item("Empresa")), "", dr.Item("Empresa"))
                .Bodega = IIf(IsDBNull(dr.Item("Bodega")), "", dr.Item("Bodega"))
                .Referencia = IIf(IsDBNull(dr.Item("referencia")), "", dr.Item("referencia"))
                .Codigo_Cliente = IIf(IsDBNull(dr.Item("Codigo_Cliente")), "", dr.Item("Codigo_Cliente"))

            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_pe_docu_ref As clsBeTrans_pe_docu_ref, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_pe_docu_ref")
            Ins.Add("iddocumentoref", "@iddocumentoref", DataType.Parametro)
            Ins.Add("codigo", "@codigo", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("descripcion", "@descripcion", DataType.Parametro)
            Ins.Add("fechadocumento", "@fechadocumento", DataType.Parametro)
            Ins.Add("fechaasignacion", "@fechaasignacion", DataType.Parametro)
            Ins.Add("fechaagregado", "@fechaagregado", DataType.Parametro)
            Ins.Add("asignado", "@asignado", DataType.Parametro)
            Ins.Add("empresa", "@empresa", DataType.Parametro)
            Ins.Add("bodega", "@bodega", DataType.Parametro)
            Ins.Add("referencia", "@referencia", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("codigo_cliente", "@codigo_cliente", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDDOCUMENTOREF", oBeTrans_pe_docu_ref.IdDocumentoRef))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeTrans_pe_docu_ref.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeTrans_pe_docu_ref.Nombre))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeTrans_pe_docu_ref.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@FECHADOCUMENTO", oBeTrans_pe_docu_ref.FechaDocumento))
            cmd.Parameters.Add(New SqlParameter("@FECHAASIGNACION", oBeTrans_pe_docu_ref.FechaAsignacion))
            cmd.Parameters.Add(New SqlParameter("@FECHAAGREGADO", oBeTrans_pe_docu_ref.FechaAgregado))
            cmd.Parameters.Add(New SqlParameter("@ASIGNADO", oBeTrans_pe_docu_ref.Asignado))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_pe_docu_ref.Activo))
            cmd.Parameters.Add(New SqlParameter("@EMPRESA", oBeTrans_pe_docu_ref.Empresa))
            cmd.Parameters.Add(New SqlParameter("@BODEGA", oBeTrans_pe_docu_ref.Bodega))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIA", oBeTrans_pe_docu_ref.Referencia))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_CLIENTE", oBeTrans_pe_docu_ref.Codigo_Cliente))

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

    Public Shared Function Actualizar(ByRef oBeTrans_pe_docu_ref As clsBeTrans_pe_docu_ref, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_pe_docu_ref")
            Upd.Add("iddocumentoref", "@iddocumentoref", DataType.Parametro)
            Upd.Add("codigo", "@codigo", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("descripcion", "@descripcion", DataType.Parametro)
            Upd.Add("fechadocumento", "@fechadocumento", DataType.Parametro)
            Upd.Add("fechaasignacion", "@fechaasignacion", DataType.Parametro)
            Upd.Add("fechaagregado", "@fechaagregado", DataType.Parametro)
            Upd.Add("asignado", "@asignado", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("empresa", "@empresa", DataType.Parametro)
            Upd.Add("bodega", "@bodega", DataType.Parametro)
            Upd.Add("referencia", "@referencia", DataType.Parametro)
            Upd.Add("codigo_cliente", "@codigo_cliente", DataType.Parametro)
            Upd.Where("IdDocumentoRef = @IdDocumentoRef")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDDOCUMENTOREF", oBeTrans_pe_docu_ref.IdDocumentoRef))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeTrans_pe_docu_ref.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeTrans_pe_docu_ref.Nombre))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeTrans_pe_docu_ref.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@FECHADOCUMENTO", oBeTrans_pe_docu_ref.FechaDocumento))
            cmd.Parameters.Add(New SqlParameter("@FECHAASIGNACION", oBeTrans_pe_docu_ref.FechaAsignacion))
            cmd.Parameters.Add(New SqlParameter("@FECHAAGREGADO", oBeTrans_pe_docu_ref.FechaAgregado))
            cmd.Parameters.Add(New SqlParameter("@ASIGNADO", oBeTrans_pe_docu_ref.Asignado))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_pe_docu_ref.Activo))
            cmd.Parameters.Add(New SqlParameter("@EMPRESA", oBeTrans_pe_docu_ref.Empresa))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIA", oBeTrans_pe_docu_ref.Referencia))
            cmd.Parameters.Add(New SqlParameter("@BODEGA", oBeTrans_pe_docu_ref.Bodega))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_CLIENTE", oBeTrans_pe_docu_ref.Codigo_Cliente))

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

    Public Shared Function Eliminar(ByRef oBeTrans_pe_docu_ref As clsBeTrans_pe_docu_ref, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_pe_docu_ref" &
             "  Where(IdDocumentoRef = @IdDocumentoRef)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDDOCUMENTOREF", oBeTrans_pe_docu_ref.IdDocumentoRef))

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

            Const sp As String = "SELECT * FROM Trans_pe_docu_ref"
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

    Public Shared Function Get_All() As List(Of clsBeTrans_pe_docu_ref)

        Dim lReturnList As New List(Of clsBeTrans_pe_docu_ref)

        Try

            Const sp As String = "SELECT * FROM Trans_pe_docu_ref"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_pe_docu_ref As New clsBeTrans_pe_docu_ref

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_pe_docu_ref = New clsBeTrans_pe_docu_ref()
                            Cargar(vBeTrans_pe_docu_ref, dr)
                            lReturnList.Add(vBeTrans_pe_docu_ref)
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

    Public Shared Sub GetSingle(ByRef pBeTrans_pe_docu_ref As clsBeTrans_pe_docu_ref)

        Try

            Const sp As String = "SELECT * FROM Trans_pe_docu_ref" &
            " Where(IdDocumentoRef = @IdDocumentoRef)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_pe_docu_ref As New clsBeTrans_pe_docu_ref

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTrans_pe_docu_ref, lDataTable.Rows(0))
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

            Const sp As String = "SELECT ISNULL(Max(IdDocumentoRef),0) FROM Trans_pe_docu_ref"

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

    Public Shared Function Set_Flag_Activo_Documento(ByVal IdDocumentoRef As Integer,
                                                     ByVal Activo As Boolean,
                                                     Optional ByVal pConection As SqlConnection = Nothing,
                                                     Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_pe_docu_ref")
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("IdDocumentoRef = @IdDocumentoRef")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDDOCUMENTOREF", IdDocumentoRef))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", Activo))

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

    Public Shared Function Get_Single_By_Filtros(ByVal Serie As String,
                                                 ByVal NoFactura As String,
                                                 ByVal Empresa As String,
                                                 ByVal Bodega As String) As clsBeTrans_pe_docu_ref

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Get_Single_By_Filtros = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM trans_pe_docu_ref 
								  Where(Codigo= @Serie 
								  AND Nombre = @NoFactura 
								  AND Empresa = @Empresa 
								  AND Bodega = @Bodega)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@Serie", Serie))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@NoFactura", NoFactura))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@Empresa", Empresa))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@Bodega", Bodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count >= 1 Then
                Dim pBeI_nav_transacciones_out As New clsBeTrans_pe_docu_ref
                Cargar(pBeI_nav_transacciones_out, dt.Rows(0))
                Get_Single_By_Filtros = pBeI_nav_transacciones_out
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lTransaction.Dispose()
            lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Get_All_By_Codigo_Bodega(ByVal pCodigoBodega As String) As List(Of clsBeTrans_pe_docu_ref)

        Dim lReturnList As New List(Of clsBeTrans_pe_docu_ref)

        Try

            Const sp As String = "SELECT * FROM Trans_pe_docu_ref WHERE bodega = @CodigoBodega"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@CodigoBodega", pCodigoBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_pe_docu_ref As New clsBeTrans_pe_docu_ref

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_pe_docu_ref = New clsBeTrans_pe_docu_ref()
                            Cargar(vBeTrans_pe_docu_ref, dr)
                            lReturnList.Add(vBeTrans_pe_docu_ref)
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

    Public Shared Function Get_All_By_Codigo_Bodega_DT(ByVal pCodigoBodega As String, ByVal pCodigoCliente As String) As DataTable

        Dim lReturnList As New List(Of clsBeTrans_pe_docu_ref)

        Get_All_By_Codigo_Bodega_DT = Nothing

        Try

            Const sp As String = "SELECT 
							      IdDocumentoRef AS Codigo,
								  Codigo AS Serie,
								  Nombre AS Numero_Factura,
							      Referencia AS Referencia_ERP, 
								  FechaDocumento FROM Trans_pe_docu_ref 
								  WHERE bodega = @CodigoBodega 
								  AND Codigo_Cliente = @CodigoCliente
							 	  AND Activo = 1"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@CodigoBodega", pCodigoBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@CodigoCliente", pCodigoCliente)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Get_All_By_Codigo_Bodega_DT = lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Desactivar_Documento(ByVal Referencia As String, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim sp As String = "UPDATE trans_pe_docu_ref SET Activo =0 WHERE Referencia = @Referencia"

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@REFERENCIA", Referencia))

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

    Public Shared Function Insertar_Con_Detalle(ByRef oBeTrans_pe_docu_ref As clsBeTrans_pe_docu_ref,
                                                ByVal lDetalle As List(Of clsBeTrans_pe_docu_ref_det),
                                                Optional ByVal pConection As SqlConnection = Nothing,
                                                Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            oBeTrans_pe_docu_ref.IdDocumentoRef = MaxID(lConnection, lTransaction) + 1
            Insertar(oBeTrans_pe_docu_ref, lConnection, lTransaction)

            Dim vMaxIdDet As Integer = clsLnTrans_pe_docu_ref_det.MaxID(lConnection, lTransaction) + 1

            For Each D In lDetalle
                D.IdDocumentoRef = oBeTrans_pe_docu_ref.IdDocumentoRef
                D.IdDocumentoRefDet = vMaxIdDet
                clsLnTrans_pe_docu_ref_det.Insertar(D, lConnection, lTransaction)
                vMaxIdDet += 1
            Next

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function MaxID(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdDocumentoRef),0) FROM Trans_pe_docu_ref"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

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