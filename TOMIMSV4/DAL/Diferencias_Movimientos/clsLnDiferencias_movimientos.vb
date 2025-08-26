Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnDiferencias_movimientos

    Public Shared Sub Cargar(ByRef oBeDiferencias_movimientos As clsBeDiferencias_movimientos, ByRef dr As DataRow)
        Try
            With oBeDiferencias_movimientos
                .IdDiferencia = IIf(IsDBNull(dr.Item("IdDiferencia")), 0, dr.Item("IdDiferencia"))
                .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                .Codigo = IIf(IsDBNull(dr.Item("Codigo")), "", dr.Item("Codigo"))
                .Nombre = IIf(IsDBNull(dr.Item("Nombre")), "", dr.Item("Nombre"))
                .Lote = IIf(IsDBNull(dr.Item("Lote")), "", dr.Item("Lote"))
                .IdProductoEstado = IIf(IsDBNull(dr.Item("IdProductoEstado")), 0, dr.Item("IdProductoEstado"))
                .Estado = IIf(IsDBNull(dr.Item("Estado")), "", dr.Item("Estado"))
                .FechaVence = IIf(IsDBNull(dr.Item("FechaVence")), New Date(1900, 1, 1), dr.Item("FechaVence"))
                .InventarioInicial = IIf(IsDBNull(dr.Item("InventarioInicial")), 0.0, dr.Item("InventarioInicial"))
                .Ingresos = IIf(IsDBNull(dr.Item("Ingresos")), 0.0, dr.Item("Ingresos"))
                .AjustesPositivos = IIf(IsDBNull(dr.Item("AjustesPositivos")), 0.0, dr.Item("AjustesPositivos"))
                .AjustesNegativos = IIf(IsDBNull(dr.Item("AjustesNegativos")), 0.0, dr.Item("AjustesNegativos"))
                .Salidas = IIf(IsDBNull(dr.Item("Salidas")), 0.0, dr.Item("Salidas"))
                .ExistenciaAl = IIf(IsDBNull(dr.Item("ExistenciaAl")), 0.0, dr.Item("ExistenciaAl"))
                .ExistenciaActual = IIf(IsDBNull(dr.Item("ExistenciaActual")), 0.0, dr.Item("ExistenciaActual"))
                .ExistenciaSinEstado = IIf(IsDBNull(dr.Item("ExistenciaSinEstado")), 0.0, dr.Item("ExistenciaSinEstado"))
                .FechaGen = IIf(IsDBNull(dr.Item("FechaGen")), New Date(1900, 1, 1), dr.Item("FechaGen"))
                .Diferencia = IIf(IsDBNull(dr.Item("Diferencia")), 0.0, dr.Item("Diferencia"))
                .FaltaStock = IIf(IsDBNull(dr.Item("FaltaStock")), False, dr.Item("FaltaStock"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeDiferencias_movimientos As clsBeDiferencias_movimientos, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("diferencias_movimientos")
            Ins.Add("iddiferencia", "@iddiferencia", DataType.Parametro)
            Ins.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Ins.Add("codigo", "@codigo", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("lote", "@lote", DataType.Parametro)
            Ins.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Ins.Add("estado", "@estado", DataType.Parametro)
            Ins.Add("fechavence", "@fechavence", DataType.Parametro)
            Ins.Add("inventarioinicial", "@inventarioinicial", DataType.Parametro)
            Ins.Add("ingresos", "@ingresos", DataType.Parametro)
            Ins.Add("ajustespositivos", "@ajustespositivos", DataType.Parametro)
            Ins.Add("ajustesnegativos", "@ajustesnegativos", DataType.Parametro)
            Ins.Add("salidas", "@salidas", DataType.Parametro)
            Ins.Add("existenciaal", "@existenciaal", DataType.Parametro)
            Ins.Add("existenciaactual", "@existenciaactual", DataType.Parametro)
            Ins.Add("existenciasinestado", "@existenciasinestado", DataType.Parametro)
            Ins.Add("fechagen", "@fechagen", DataType.Parametro)
            Ins.Add("diferencia", "@diferencia", DataType.Parametro)
            Ins.Add("faltastock", "@faltastock", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDDIFERENCIA", oBeDiferencias_movimientos.IdDiferencia))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeDiferencias_movimientos.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeDiferencias_movimientos.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeDiferencias_movimientos.Nombre))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeDiferencias_movimientos.Lote))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeDiferencias_movimientos.IdProductoEstado))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeDiferencias_movimientos.Estado))
            cmd.Parameters.Add(New SqlParameter("@FECHAVENCE", oBeDiferencias_movimientos.FechaVence))
            cmd.Parameters.Add(New SqlParameter("@INVENTARIOINICIAL", oBeDiferencias_movimientos.InventarioInicial))
            cmd.Parameters.Add(New SqlParameter("@INGRESOS", oBeDiferencias_movimientos.Ingresos))
            cmd.Parameters.Add(New SqlParameter("@AJUSTESPOSITIVOS", oBeDiferencias_movimientos.AjustesPositivos))
            cmd.Parameters.Add(New SqlParameter("@AJUSTESNEGATIVOS", oBeDiferencias_movimientos.AjustesNegativos))
            cmd.Parameters.Add(New SqlParameter("@SALIDAS", oBeDiferencias_movimientos.Salidas))
            cmd.Parameters.Add(New SqlParameter("@EXISTENCIAAL", oBeDiferencias_movimientos.ExistenciaAl))
            cmd.Parameters.Add(New SqlParameter("@EXISTENCIAACTUAL", oBeDiferencias_movimientos.ExistenciaActual))
            cmd.Parameters.Add(New SqlParameter("@EXISTENCIASINESTADO", oBeDiferencias_movimientos.ExistenciaSinEstado))
            cmd.Parameters.Add(New SqlParameter("@FECHAGEN", oBeDiferencias_movimientos.FechaGen))
            cmd.Parameters.Add(New SqlParameter("@DIFERENCIA", oBeDiferencias_movimientos.Diferencia))
            cmd.Parameters.Add(New SqlParameter("@FALTASTOCK", oBeDiferencias_movimientos.FaltaStock))

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

    Public Shared Function Actualizar(ByRef oBeDiferencias_movimientos As clsBeDiferencias_movimientos, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("diferencias_movimientos")
            Upd.Add("iddiferencia", "@iddiferencia", DataType.Parametro)
            Upd.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Upd.Add("codigo", "@codigo", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("lote", "@lote", DataType.Parametro)
            Upd.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Upd.Add("estado", "@estado", DataType.Parametro)
            Upd.Add("fechavence", "@fechavence", DataType.Parametro)
            Upd.Add("inventarioinicial", "@inventarioinicial", DataType.Parametro)
            Upd.Add("ingresos", "@ingresos", DataType.Parametro)
            Upd.Add("ajustespositivos", "@ajustespositivos", DataType.Parametro)
            Upd.Add("ajustesnegativos", "@ajustesnegativos", DataType.Parametro)
            Upd.Add("salidas", "@salidas", DataType.Parametro)
            Upd.Add("existenciaal", "@existenciaal", DataType.Parametro)
            Upd.Add("existenciaactual", "@existenciaactual", DataType.Parametro)
            Upd.Add("existenciasinestado", "@existenciasinestado", DataType.Parametro)
            Upd.Add("fechagen", "@fechagen", DataType.Parametro)
            Upd.Add("diferencia", "@diferencia", DataType.Parametro)
            Upd.Add("faltastock", "@faltastock", DataType.Parametro)
            Upd.Where("IdDiferencia = @IdDiferencia")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDDIFERENCIA", oBeDiferencias_movimientos.IdDiferencia))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeDiferencias_movimientos.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeDiferencias_movimientos.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeDiferencias_movimientos.Nombre))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeDiferencias_movimientos.Lote))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeDiferencias_movimientos.IdProductoEstado))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeDiferencias_movimientos.Estado))
            cmd.Parameters.Add(New SqlParameter("@FECHAVENCE", oBeDiferencias_movimientos.FechaVence))
            cmd.Parameters.Add(New SqlParameter("@INVENTARIOINICIAL", oBeDiferencias_movimientos.InventarioInicial))
            cmd.Parameters.Add(New SqlParameter("@INGRESOS", oBeDiferencias_movimientos.Ingresos))
            cmd.Parameters.Add(New SqlParameter("@AJUSTESPOSITIVOS", oBeDiferencias_movimientos.AjustesPositivos))
            cmd.Parameters.Add(New SqlParameter("@AJUSTESNEGATIVOS", oBeDiferencias_movimientos.AjustesNegativos))
            cmd.Parameters.Add(New SqlParameter("@SALIDAS", oBeDiferencias_movimientos.Salidas))
            cmd.Parameters.Add(New SqlParameter("@EXISTENCIAAL", oBeDiferencias_movimientos.ExistenciaAl))
            cmd.Parameters.Add(New SqlParameter("@EXISTENCIAACTUAL", oBeDiferencias_movimientos.ExistenciaActual))
            cmd.Parameters.Add(New SqlParameter("@EXISTENCIASINESTADO", oBeDiferencias_movimientos.ExistenciaSinEstado))
            cmd.Parameters.Add(New SqlParameter("@FECHAGEN", oBeDiferencias_movimientos.FechaGen))
            cmd.Parameters.Add(New SqlParameter("@DIFERENCIA", oBeDiferencias_movimientos.Diferencia))
            cmd.Parameters.Add(New SqlParameter("@FALTASTOCK", oBeDiferencias_movimientos.FaltaStock))

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

    Public Shared Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Diferencias_movimientos "

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

    Public Shared Function Eliminar(ByRef oBeDiferencias_movimientos As clsBeDiferencias_movimientos, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Diferencias_movimientos" &
             "  Where(IdDiferencia = @IdDiferencia)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDDIFERENCIA", oBeDiferencias_movimientos.IdDiferencia))

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

            Const sp As String = "SELECT * FROM Diferencias_movimientos"
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

    Public Shared Function GetAll() As List(Of clsBeDiferencias_movimientos)

        Dim lReturnList As New List(Of clsBeDiferencias_movimientos)

        Try

            Const sp As String = "SELECT * FROM Diferencias_movimientos"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeDiferencias_movimientos As New clsBeDiferencias_movimientos

                        For Each dr As DataRow In lDataTable.Rows
                            vBeDiferencias_movimientos = New clsBeDiferencias_movimientos()
                            Cargar(vBeDiferencias_movimientos, dr)
                            lReturnList.Add(vBeDiferencias_movimientos)
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

    Public Shared Sub GetSingle(ByRef pBeDiferencias_movimientos As clsBeDiferencias_movimientos)

        Try

            Const sp As String = "SELECT * FROM Diferencias_movimientos" &
            " Where(IdDiferencia = @IdDiferencia)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeDiferencias_movimientos As New clsBeDiferencias_movimientos

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeDiferencias_movimientos, lDataTable.Rows(0))
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

            Const sp As String = "SELECT ISNULL(Max(IdDiferencia),0) FROM Diferencias_movimientos"

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
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

End Class
