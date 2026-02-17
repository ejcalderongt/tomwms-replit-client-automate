Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTempComparacionInventario

    Public Shared Sub Cargar(ByRef oBeTempComparacionInventario As clsBeTempComparacionInventario, ByRef dr As DataRow)
        Try
            With oBeTempComparacionInventario
                .IdInventario = IIf(IsDBNull(dr.Item("IdInventario")), 0, dr.Item("IdInventario"))
                .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                .IdProducto = IIf(IsDBNull(dr.Item("IdProducto")), 0, dr.Item("IdProducto"))
                .IdUnidadMedida = IIf(IsDBNull(dr.Item("IdUnidadMedida")), 0, dr.Item("IdUnidadMedida"))
                .Codigo = IIf(IsDBNull(dr.Item("Codigo")), "", dr.Item("Codigo"))
                .Producto = IIf(IsDBNull(dr.Item("Producto")), "", dr.Item("Producto"))
                .Cantidad_Stock = IIf(IsDBNull(dr.Item("Cantidad_Stock")), 0.0, dr.Item("Cantidad_Stock"))
                .Cantidad = IIf(IsDBNull(dr.Item("Cantidad")), 0.0, dr.Item("Cantidad"))
                .Peso_Stock = IIf(IsDBNull(dr.Item("Peso_Stock")), 0.0, dr.Item("Peso_Stock"))
                .Peso = IIf(IsDBNull(dr.Item("Peso")), 0.0, dr.Item("Peso"))
                .Entradas_Salidas = IIf(IsDBNull(dr.Item("Entradas_Salidas")), 0.0, dr.Item("Entradas_Salidas"))
                .Entradas = IIf(IsDBNull(dr.Item("Entradas")), 0.0, dr.Item("Entradas"))
                .Salidas = IIf(IsDBNull(dr.Item("Salidas")), 0.0, dr.Item("Salidas"))
                .LoteOrigen = IIf(IsDBNull(dr.Item("LoteOrigen")), "", dr.Item("LoteOrigen"))
                .LoteDestino = IIf(IsDBNull(dr.Item("Lote")), "", dr.Item("Lote"))
                .EstadoOrigen = IIf(IsDBNull(dr.Item("EstadoOrigen")), "", dr.Item("EstadoOrigen"))
                .EstadoDestino = IIf(IsDBNull(dr.Item("EstadoDestino")), "", dr.Item("EstadoDestino"))
                .UbicacionOrigen = IIf(IsDBNull(dr.Item("UbicacionOrigen")), "", dr.Item("UbicacionOrigen"))
                .UbicacionDestino = IIf(IsDBNull(dr.Item("UbicacionDestino")), "", dr.Item("UbicacionDestino"))
                .FechaVence = IIf(IsDBNull(dr.Item("FechaVence")), Nothing, dr.Item("FechaVence"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTempComparacionInventario As clsBeTempComparacionInventario, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("tempcomparacioninventario")
            Ins.Add("idinventario", "@idinventario", DataType.Parametro)
            Ins.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Ins.Add("idproducto", "@idproducto", DataType.Parametro)
            Ins.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Ins.Add("codigo", "@codigo", DataType.Parametro)
            Ins.Add("producto", "@producto", DataType.Parametro)
            Ins.Add("cantidad_stock", "@cantidad_stock", DataType.Parametro)
            Ins.Add("cantidad", "@cantidad", DataType.Parametro)
            Ins.Add("peso_stock", "@peso_stock", DataType.Parametro)
            Ins.Add("peso", "@peso", DataType.Parametro)
            Ins.Add("entradas_salidas", "@entradas_salidas", DataType.Parametro)
            Ins.Add("entradas", "@entradas", DataType.Parametro)
            Ins.Add("salidas", "@salidas", DataType.Parametro)
            Ins.Add("loteOrigen", "@loteOrigen", DataType.Parametro)
            Ins.Add("lote", "@lote", DataType.Parametro)
            Ins.Add("licencia", "@licencia", DataType.Parametro)
            Ins.Add("fechavence", "@fechavence", DataType.Parametro)
            Ins.Add("EstadoOrigen", "@EstadoOrigen", DataType.Parametro)
            Ins.Add("EstadoDestino", "@EstadoDestino", DataType.Parametro)
            Ins.Add("UbicacionOrigen", "@UbicacionOrigen", DataType.Parametro)
            Ins.Add("UbicacionDestino", "@UbicacionDestino", DataType.Parametro)
            Ins.Add("IdProductoTallaColor", "@IdProductoTallaColor", DataType.Parametro)
            Ins.Add("IdProductoTallaColor_nuevo", "@IdProductoTallaColor_nuevo", DataType.Parametro)
            Ins.Add("TallaStock", "@TallaStock", DataType.Parametro)
            Ins.Add("ColorStock", "@ColorStock", DataType.Parametro)
            Ins.Add("TallaNueva", "@TallaNueva", DataType.Parametro)
            Ins.Add("ColorNuevo", "@ColorNuevo", DataType.Parametro)
            Ins.Add("IdUbicacion", "@IdUbicacion", DataType.Parametro)
            Ins.Add("IdUbicacionDestino", "@IdUbicacionDestino", DataType.Parametro)
            Ins.Add("Fec_Mod", "@Fec_Mod", DataType.Parametro)
            Ins.Add("IdInvciclico", "@IdInvciclico", DataType.Parametro)
            Ins.Add("FechaVenceStock", "@FechaVenceStock", DataType.Parametro)
            Ins.Add("IdProductoEstado", "@IdProductoEstado", DataType.Parametro)
            Ins.Add("IdProductoEst_nuevo", "@IdProductoEst_nuevo", DataType.Parametro)
            Ins.Add("IdPresentacion", "@IdPresentacion", DataType.Parametro)
            Ins.Add("Cantidad_Reservada_UmBas", "@Cantidad_Reservada_UmBas", DataType.Parametro)
            Ins.Add("TieneReservaYConteoInsuficiente", "@TieneReservaYConteoInsuficiente", DataType.Parametro)
            Ins.Add("Observacion", "@Observacion", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIO", oBeTempComparacionInventario.IdInventario))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTempComparacionInventario.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeTempComparacionInventario.IdProducto))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeTempComparacionInventario.IdUnidadMedida))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeTempComparacionInventario.Codigo))
            cmd.Parameters.Add(New SqlParameter("@PRODUCTO", oBeTempComparacionInventario.Producto))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_STOCK", oBeTempComparacionInventario.Cantidad_Stock))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTempComparacionInventario.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@PESO_STOCK", oBeTempComparacionInventario.Peso_Stock))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeTempComparacionInventario.Peso))
            cmd.Parameters.Add(New SqlParameter("@ENTRADAS_SALIDAS", oBeTempComparacionInventario.Entradas_Salidas))
            cmd.Parameters.Add(New SqlParameter("@ENTRADAS", oBeTempComparacionInventario.Entradas))
            cmd.Parameters.Add(New SqlParameter("@SALIDAS", oBeTempComparacionInventario.Salidas))
            cmd.Parameters.Add(New SqlParameter("@LOTEORIGEN", oBeTempComparacionInventario.LoteOrigen))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeTempComparacionInventario.LoteDestino))
            cmd.Parameters.Add(New SqlParameter("@ESTADOORIGEN", oBeTempComparacionInventario.EstadoOrigen))
            cmd.Parameters.Add(New SqlParameter("@ESTADODESTINO", oBeTempComparacionInventario.EstadoDestino))
            cmd.Parameters.Add(New SqlParameter("@UBICACIONORIGEN", oBeTempComparacionInventario.UbicacionOrigen))
            cmd.Parameters.Add(New SqlParameter("@UBICACIONDESTINO", oBeTempComparacionInventario.UbicacionDestino))
            cmd.Parameters.Add(New SqlParameter("@LICENCIA", oBeTempComparacionInventario.Licencia))
            cmd.Parameters.Add(New SqlParameter("@FECHAVENCE", oBeTempComparacionInventario.FechaVence))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOTALLACOLOR", oBeTempComparacionInventario.IdProductoTallaColor))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOTALLACOLOR_NUEVO", oBeTempComparacionInventario.IdProductoTallaColor_nuevo))
            cmd.Parameters.Add(New SqlParameter("@TALLASTOCK", oBeTempComparacionInventario.TallaStock))
            cmd.Parameters.Add(New SqlParameter("@COLORSTOCK", oBeTempComparacionInventario.ColorStock))
            cmd.Parameters.Add(New SqlParameter("@TALLANUEVA", oBeTempComparacionInventario.TallaNueva))
            cmd.Parameters.Add(New SqlParameter("@COLORNUEVO", oBeTempComparacionInventario.ColorNuevo))
            cmd.Parameters.Add(New SqlParameter("@IdUbicacion", oBeTempComparacionInventario.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@IdUbicacionDestino", oBeTempComparacionInventario.IdUbicacionDestino))
            cmd.Parameters.Add(New SqlParameter("@Fec_Mod", oBeTempComparacionInventario.Fec_Mod))
            cmd.Parameters.Add(New SqlParameter("@IdInvciclico", oBeTempComparacionInventario.IdInvciclico))
            cmd.Parameters.Add(New SqlParameter("@FechaVenceStock", oBeTempComparacionInventario.FechaVenceStock))
            cmd.Parameters.Add(New SqlParameter("@IdProductoEstado", oBeTempComparacionInventario.IdProductoEstado))
            cmd.Parameters.Add(New SqlParameter("@IdProductoEst_nuevo", oBeTempComparacionInventario.IdProductoEst_nuevo))
            cmd.Parameters.Add(New SqlParameter("@IdPresentacion", oBeTempComparacionInventario.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@Cantidad_Reservada_UmBas", oBeTempComparacionInventario.Cantidad_Reservada_UmBas))
            cmd.Parameters.Add(New SqlParameter("@TieneReservaYConteoInsuficiente", oBeTempComparacionInventario.TieneReservaYConteoInsuficiente))
            cmd.Parameters.Add(New SqlParameter("@Observacion", oBeTempComparacionInventario.Observacion))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeTempComparacionInventario As clsBeTempComparacionInventario, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("tempcomparacioninventario")
            Upd.Add("idinventario", "@idinventario", DataType.Parametro)
            Upd.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Upd.Add("idproducto", "@idproducto", DataType.Parametro)
            Upd.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Upd.Add("codigo", "@codigo", DataType.Parametro)
            Upd.Add("producto", "@producto", DataType.Parametro)
            Upd.Add("cantidad_stock", "@cantidad_stock", DataType.Parametro)
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Add("peso_stock", "@peso_stock", DataType.Parametro)
            Upd.Add("peso", "@peso", DataType.Parametro)
            Upd.Add("entradas_salidas", "@entradas_salidas", DataType.Parametro)
            Upd.Add("entradas", "@entradas", DataType.Parametro)
            Upd.Add("salidas", "@salidas", DataType.Parametro)
            Upd.Add("loteOrigen", "@loteOrigen", DataType.Parametro)
            Upd.Add("lote", "@lote", DataType.Parametro)
            Upd.Add("licencia", "@licencia", DataType.Parametro)
            Upd.Add("fechavence", "@fechavence", DataType.Parametro)
            Upd.Add("EstadoOrigen", "@EstadoOrigen", DataType.Parametro)
            Upd.Add("EstadoDestino", "@EstadoDestino", DataType.Parametro)
            Upd.Add("UbicacionOrigen", "@UbicacionOrigen", DataType.Parametro)
            Upd.Add("UbicacionDestino", "@UbicacionDestino", DataType.Parametro)

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIO", oBeTempComparacionInventario.IdInventario))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTempComparacionInventario.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeTempComparacionInventario.IdProducto))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeTempComparacionInventario.IdUnidadMedida))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeTempComparacionInventario.Codigo))
            cmd.Parameters.Add(New SqlParameter("@PRODUCTO", oBeTempComparacionInventario.Producto))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_STOCK", oBeTempComparacionInventario.Cantidad_Stock))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTempComparacionInventario.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@PESO_STOCK", oBeTempComparacionInventario.Peso_Stock))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeTempComparacionInventario.Peso))
            cmd.Parameters.Add(New SqlParameter("@ENTRADAS_SALIDAS", oBeTempComparacionInventario.Entradas_Salidas))
            cmd.Parameters.Add(New SqlParameter("@ENTRADAS", oBeTempComparacionInventario.Entradas))
            cmd.Parameters.Add(New SqlParameter("@SALIDAS", oBeTempComparacionInventario.Salidas))
            cmd.Parameters.Add(New SqlParameter("@LOTEORIGEN", oBeTempComparacionInventario.LoteOrigen))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeTempComparacionInventario.LoteDestino))
            cmd.Parameters.Add(New SqlParameter("@ESTADOORIGEN", oBeTempComparacionInventario.EstadoOrigen))
            cmd.Parameters.Add(New SqlParameter("@ESTADODESTINO", oBeTempComparacionInventario.EstadoDestino))
            cmd.Parameters.Add(New SqlParameter("@UBICACIONORIGEN", oBeTempComparacionInventario.UbicacionOrigen))
            cmd.Parameters.Add(New SqlParameter("@UBICACIONDESTINO", oBeTempComparacionInventario.UbicacionDestino))
            cmd.Parameters.Add(New SqlParameter("@LICENCIA", oBeTempComparacionInventario.Licencia))
            cmd.Parameters.Add(New SqlParameter("@FECHAVENCE", oBeTempComparacionInventario.FechaVence))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Eliminar(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from TempComparacionInventario "

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
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
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try
    End Function

    Public Shared Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from TempComparacionInventario"
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
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
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM TempComparacionInventario"
            Dim lConnection As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeTempComparacionInventario As clsBeTempComparacionInventario) As Boolean

        Try

            Const sp As String = "SELECT * FROM TempComparacionInventario"

            Dim lConnection As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)


            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTempComparacionInventario, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeTempComparacionInventario)

        Try

            Dim lReturnList As New List(Of clsBeTempComparacionInventario)
            Const sp As String = "SELECT * FROM TempComparacionInventario"
            Dim lConnection As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTempComparacionInventario As New clsBeTempComparacionInventario

            For Each dr As DataRow In dt.Rows
                vBeTempComparacionInventario = New clsBeTempComparacionInventario
                Cargar(vBeTempComparacionInventario, dr)
                lReturnList.Add(vBeTempComparacionInventario)
            Next

            Return lReturnList

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeTempComparacionInventario As clsBeTempComparacionInventario)

        Try

            Const sp As String = "SELECT * FROM TempComparacionInventario"

            Dim lConnection As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTempComparacionInventario, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT * FROM TempComparacionInventario"

            Using lConnection As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
                Using lCommand As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CInt(lReturnValue)
                    End If
                End Using
            End Using

            Return lMax

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Eliminar(ByVal IdInventario As Integer) As Integer

        Dim lConnection As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Const sp As String = " Delete from TempComparacionInventario where IdInventario=@IdInventario"
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            cmd.Parameters.AddWithValue("@IdInventario", IdInventario)


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try
    End Function

    Public Shared Function Eliminar(ByVal IdInventario As Integer, lConnection As SqlConnection, lTransaction As SqlTransaction) As Integer

        Try

            Const sp As String = " Delete from TempComparacionInventario where IdInventario=@IdInventario"
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            cmd.Parameters.AddWithValue("@IdInventario", IdInventario)

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            Return rowsAffected

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function Insertar_Comparacion_Inventario(ByVal pIdInventarioEnc As Integer, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim sp As String = "DELETE FROM ComparacionInventario WHERE IdInventario = @IdInventarioEnc;
                                INSERT INTO ComparacionInventario SELECT * FROM TempComparacionInventario WHERE IdInventario = @IdInventarioEnc;"

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.AddWithValue("@IdInventarioEnc", pIdInventarioEnc)

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

End Class
