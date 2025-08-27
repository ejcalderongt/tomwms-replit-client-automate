Imports System.Data.SqlClient

Public Class clsLnRoad_p_vendedor

    Public Shared Sub Cargar(ByRef oBeRoad_p_vendedor As clsBeRoad_p_vendedor, ByRef dr As DataRow)

        Try

            With oBeRoad_p_vendedor

                .IdRuta = IIf(IsDBNull(dr.Item("IdRuta")), 0, dr.Item("IdRuta"))
                .IdVendedor = IIf(IsDBNull(dr.Item("IdVendedor")), 0, dr.Item("IdVendedor"))
                .Codigo = IIf(IsDBNull(dr.Item("codigo")), "", dr.Item("codigo"))
                .Nombre = IIf(IsDBNull(dr.Item("nombre")), "", dr.Item("nombre"))
                .Clave = IIf(IsDBNull(dr.Item("clave")), "", dr.Item("clave"))
                .Ruta = IIf(IsDBNull(dr.Item("ruta")), "", dr.Item("ruta"))
                .Nivel = IIf(IsDBNull(dr.Item("nivel")), 0, dr.Item("nivel"))
                .Nivelprecio = IIf(IsDBNull(dr.Item("nivelprecio")), 0, dr.Item("nivelprecio"))
                .Bodega = IIf(IsDBNull(dr.Item("bodega")), "", dr.Item("bodega"))
                .Subbodega = IIf(IsDBNull(dr.Item("subbodega")), "", dr.Item("subbodega"))
                .Cod_vehiculo = IIf(IsDBNull(dr.Item("cod_vehiculo")), "", dr.Item("cod_vehiculo"))
                .Liquidando = IIf(IsDBNull(dr.Item("liquidando")), "", dr.Item("liquidando"))
                .Ultima_fecha_liq = IIf(IsDBNull(dr.Item("ultima_fecha_liq")), Date.Now, dr.Item("ultima_fecha_liq"))
                .Bloqueado = IIf(IsDBNull(dr.Item("bloqueado")), False, dr.Item("bloqueado"))
                .Devolucion_sap = IIf(IsDBNull(dr.Item("devolucion_sap")), 0, dr.Item("devolucion_sap"))

            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public shared Function Insertar(ByRef oBeRoad_p_vendedor As clsBeRoad_p_vendedor, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Ins.Init("road_p_vendedor")
            Ins.Add("idvendedor", "@idvendedor", DataType.Parametro)
            Ins.Add("codigo", "@codigo", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("clave", "@clave", DataType.Parametro)
            Ins.Add("ruta", "@ruta", DataType.Parametro)
            Ins.Add("nivel", "@nivel", DataType.Parametro)
            Ins.Add("nivelprecio", "@nivelprecio", DataType.Parametro)
            Ins.Add("bodega", "@bodega", DataType.Parametro)
            Ins.Add("subbodega", "@subbodega", DataType.Parametro)
            Ins.Add("cod_vehiculo", "@cod_vehiculo", DataType.Parametro)
            Ins.Add("liquidando", "@liquidando", DataType.Parametro)
            Ins.Add("ultima_fecha_liq", "@ultima_fecha_liq", DataType.Parametro)
            Ins.Add("bloqueado", "@bloqueado", DataType.Parametro)
            Ins.Add("devolucion_sap", "@devolucion_sap", DataType.Parametro)
            Ins.Add("idruta", "@idruta", DataType.Parametro)

            Dim sp As String = Ins.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDVENDEDOR", oBeRoad_p_vendedor.IdVendedor))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeRoad_p_vendedor.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeRoad_p_vendedor.Nombre))
            cmd.Parameters.Add(New SqlParameter("@CLAVE", oBeRoad_p_vendedor.Clave))
            cmd.Parameters.Add(New SqlParameter("@RUTA", oBeRoad_p_vendedor.Ruta))
            cmd.Parameters.Add(New SqlParameter("@NIVEL", oBeRoad_p_vendedor.Nivel))
            cmd.Parameters.Add(New SqlParameter("@NIVELPRECIO", oBeRoad_p_vendedor.Nivelprecio))
            cmd.Parameters.Add(New SqlParameter("@BODEGA", oBeRoad_p_vendedor.Bodega))
            cmd.Parameters.Add(New SqlParameter("@SUBBODEGA", oBeRoad_p_vendedor.Subbodega))
            cmd.Parameters.Add(New SqlParameter("@COD_VEHICULO", oBeRoad_p_vendedor.Cod_vehiculo))
            cmd.Parameters.Add(New SqlParameter("@LIQUIDANDO", oBeRoad_p_vendedor.Liquidando))
            cmd.Parameters.Add(New SqlParameter("@ULTIMA_FECHA_LIQ", oBeRoad_p_vendedor.Ultima_fecha_liq))
            cmd.Parameters.Add(New SqlParameter("@BLOQUEADO", oBeRoad_p_vendedor.Bloqueado))
            cmd.Parameters.Add(New SqlParameter("@DEVOLUCION_SAP", oBeRoad_p_vendedor.Devolucion_sap))
            cmd.Parameters.Add(New SqlParameter("@IDRUTA", oBeRoad_p_vendedor.IdRuta))

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

    Public Shared Function Actualizar(ByRef oBeRoad_p_vendedor As clsBeRoad_p_vendedor, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("road_p_vendedor")
            Upd.Add("idvendedor", "@idvendedor", DataType.Parametro)
            Upd.Add("codigo", "@codigo", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("clave", "@clave", DataType.Parametro)
            Upd.Add("ruta", "@ruta", DataType.Parametro)
            Upd.Add("nivel", "@nivel", DataType.Parametro)
            Upd.Add("nivelprecio", "@nivelprecio", DataType.Parametro)
            Upd.Add("bodega", "@bodega", DataType.Parametro)
            Upd.Add("subbodega", "@subbodega", DataType.Parametro)
            Upd.Add("cod_vehiculo", "@cod_vehiculo", DataType.Parametro)
            Upd.Add("liquidando", "@liquidando", DataType.Parametro)
            Upd.Add("ultima_fecha_liq", "@ultima_fecha_liq", DataType.Parametro)
            Upd.Add("bloqueado", "@bloqueado", DataType.Parametro)
            Upd.Add("devolucion_sap", "@devolucion_sap", DataType.Parametro)
            Upd.Add("idruta", "@idruta", DataType.Parametro)
            Upd.Where("IdVendedor = @IdVendedor" &
                " AND codigo = @codigo")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDVENDEDOR", oBeRoad_p_vendedor.IdVendedor))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeRoad_p_vendedor.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeRoad_p_vendedor.Nombre))
            cmd.Parameters.Add(New SqlParameter("@CLAVE", oBeRoad_p_vendedor.Clave))
            cmd.Parameters.Add(New SqlParameter("@RUTA", oBeRoad_p_vendedor.Ruta))
            cmd.Parameters.Add(New SqlParameter("@NIVEL", oBeRoad_p_vendedor.Nivel))
            cmd.Parameters.Add(New SqlParameter("@NIVELPRECIO", oBeRoad_p_vendedor.Nivelprecio))
            cmd.Parameters.Add(New SqlParameter("@BODEGA", oBeRoad_p_vendedor.Bodega))
            cmd.Parameters.Add(New SqlParameter("@SUBBODEGA", oBeRoad_p_vendedor.Subbodega))
            cmd.Parameters.Add(New SqlParameter("@COD_VEHICULO", oBeRoad_p_vendedor.Cod_vehiculo))
            cmd.Parameters.Add(New SqlParameter("@LIQUIDANDO", oBeRoad_p_vendedor.Liquidando))
            cmd.Parameters.Add(New SqlParameter("@ULTIMA_FECHA_LIQ", oBeRoad_p_vendedor.Ultima_fecha_liq))
            cmd.Parameters.Add(New SqlParameter("@BLOQUEADO", oBeRoad_p_vendedor.Bloqueado))
            cmd.Parameters.Add(New SqlParameter("@DEVOLUCION_SAP", oBeRoad_p_vendedor.Devolucion_sap))
            cmd.Parameters.Add(New SqlParameter("@IDRUTA", oBeRoad_p_vendedor.IdRuta))

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

    Public Shared Function Eliminar(ByRef oBeRoad_p_vendedor As clsBeRoad_p_vendedor, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text

            Dim sp As String = " Delete from Road_p_vendedor" &
             "  Where(IdVendedor = @IdVendedor)" &
             "  AND (codigo = @codigo)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDVENDEDOR", oBeRoad_p_vendedor.IdVendedor))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeRoad_p_vendedor.Codigo))

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

    Public Function Obtener(ByRef oBeRoad_p_vendedor As clsBeRoad_p_vendedor) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Obtener = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim sp As String = "SELECT * FROM Road_p_vendedor" &
            " Where(IdVendedor = @IdVendedor)" &
            "AND (codigo = @codigo)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDVENDEDOR", oBeRoad_p_vendedor.IdVendedor))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@CODIGO", oBeRoad_p_vendedor.Codigo))

            Dim dt As New DataTable
            dad.Fill(dt)

            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count = 1 Then
                Cargar(oBeRoad_p_vendedor, dt.Rows(0))
                Obtener = True
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

End Class
