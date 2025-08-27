Imports System.Data.SqlClient

Public Class clsLnProveedor_bodega

    Public Shared Sub Cargar(ByRef oBeProveedor_bodega As clsBeProveedor_bodega, ByRef dr As DataRow)

        Try

            With oBeProveedor_bodega

                .IdAsignacion = IIf(IsDBNull(dr.Item("IdAsignacion")), 0, dr.Item("IdAsignacion"))
                .IdProveedor = IIf(IsDBNull(dr.Item("IdProveedor")), 0, dr.Item("IdProveedor"))
                .Proveedor = New clsBeProveedor
                .Proveedor.IdProveedor = .IdProveedor
                .Proveedor = clsLnProveedor.GetSingle(.IdProveedor)
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .IdAreaOrigen = IIf(IsDBNull(dr.Item("IdAreaOrigen")), False, dr.Item("IdAreaOrigen"))

            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Sub Cargar(ByRef oBeProveedor_bodega As clsBeProveedor_bodega,
                             ByRef dr As DataRow,
                             ByRef lConnection As SqlConnection,
                             ByRef lTransaction As SqlTransaction)

        Try

            With oBeProveedor_bodega

                .IdAsignacion = IIf(IsDBNull(dr.Item("IdAsignacion")), 0, dr.Item("IdAsignacion"))
                .IdProveedor = IIf(IsDBNull(dr.Item("IdProveedor")), 0, dr.Item("IdProveedor"))
                .Proveedor = New clsBeProveedor
                .Proveedor.IdProveedor = .IdProveedor
                .Proveedor = clsLnProveedor.GetSingle(.IdProveedor, lConnection, lTransaction)
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .IdAreaOrigen = IIf(IsDBNull(dr.Item("IdAreaOrigen")), False, dr.Item("IdAreaOrigen"))

            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeProveedor_bodega As clsBeProveedor_bodega, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            Ins.Init("proveedor_bodega")
            Ins.Add("idasignacion", "@idasignacion", DataType.Parametro)
            Ins.Add("idproveedor", "@idproveedor", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            If oBeProveedor_bodega.IdAreaOrigen <> 0 Then
                Ins.Add("idareaorigen", "@idareaorigen", DataType.Parametro)
            End If
            Dim sp As String = Ins.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            '#20191205_Trans_Ref: Transacción_Local_Agregada
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            cmd.Parameters.Add(New SqlParameter("@IDASIGNACION", oBeProveedor_bodega.IdAsignacion))
            cmd.Parameters.Add(New SqlParameter("@IDPROVEEDOR", oBeProveedor_bodega.IdProveedor))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeProveedor_bodega.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProveedor_bodega.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProveedor_bodega.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProveedor_bodega.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProveedor_bodega.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProveedor_bodega.Fec_mod))
            If oBeProveedor_bodega.IdAreaOrigen <> 0 Then
                cmd.Parameters.Add(New SqlParameter("@IDAREAORIGEN", oBeProveedor_bodega.IdAreaOrigen))
            End If

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeProveedor_bodega As clsBeProveedor_bodega, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            Upd.Init("proveedor_bodega")
            Upd.Add("idasignacion", "@idasignacion", DataType.Parametro)
            Upd.Add("idproveedor", "@idproveedor", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            If oBeProveedor_bodega.IdAreaOrigen <> 0 Then
                Upd.Add("idareaorigen", "@idareaorigen", DataType.Parametro)
            End If
            Upd.Where("IdAsignacion = @idasignacion")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            '#20191205_Trans_Ref: Transacción_Local_Agregada
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDASIGNACION", oBeProveedor_bodega.IdAsignacion))
            cmd.Parameters.Add(New SqlParameter("@IDPROVEEDOR", oBeProveedor_bodega.IdProveedor))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeProveedor_bodega.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProveedor_bodega.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProveedor_bodega.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProveedor_bodega.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProveedor_bodega.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProveedor_bodega.Fec_mod))
            If oBeProveedor_bodega.IdAreaOrigen <> 0 Then
                cmd.Parameters.Add(New SqlParameter("@IDAREAORIGEN", oBeProveedor_bodega.IdAreaOrigen))
            End If

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then
                lTransaction.Commit()
            End If

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Eliminar(ByRef oBeProveedor_bodega As clsBeProveedor_bodega, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text

            Dim sp As String = " Delete from Proveedor_bodega" &
             "  Where(IdAsignacion = @IdAsignacion)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            '#20191205_Trans_Ref: Transacción_Local_Agregada
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            cmd.Parameters.Add(New SqlParameter("@IDASIGNACION", oBeProveedor_bodega.IdAsignacion))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Proveedor_bodega"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeProveedor_bodega As clsBeProveedor_bodega) As Boolean

        Try

            Dim sp As String = "SELECT * FROM Proveedor_bodega" &
            " Where(IdAsignacion = @IdAsignacion)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDASIGNACION", oBeProveedor_bodega.IdAsignacion))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeProveedor_bodega, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
