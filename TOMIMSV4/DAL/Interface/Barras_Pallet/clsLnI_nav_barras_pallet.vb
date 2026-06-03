Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnI_nav_barras_pallet

    Public Shared Sub Cargar(ByRef oBeI_nav_barras_pallet As clsBeI_nav_barras_pallet, ByRef dr As DataRow)

        Try

            With oBeI_nav_barras_pallet

                .IdPallet = IIf(IsDBNull(dr.Item("IdPallet")), 0, dr.Item("IdPallet"))
                .Codigo = IIf(IsDBNull(dr.Item("Codigo")), "", dr.Item("Codigo"))
                .Nombre = IIf(IsDBNull(dr.Item("Nombre")), "", dr.Item("Nombre"))
                .Camas_Por_Tarima = IIf(IsDBNull(dr.Item("CAMAS_POR_TARIMA")), 0, dr.Item("CAMAS_POR_TARIMA"))
                .Cajas_Por_Cama = IIf(IsDBNull(dr.Item("Cajas_Por_Cama")), 0, dr.Item("Cajas_Por_Cama"))
                .Cantidad_Presentacion = IIf(IsDBNull(dr.Item("Cantidad_Presentacion")), 0.0, dr.Item("Cantidad_Presentacion"))
                .UM_Producto = IIf(IsDBNull(dr.Item("UM_Producto")), "", dr.Item("UM_Producto"))
                .Lote = IIf(IsDBNull(dr.Item("Lote")), "", dr.Item("Lote"))
                .Lote_Numerico = IIf(IsDBNull(dr.Item("Lote_Numerico")), "0", dr.Item("Lote_Numerico"))
                .Fecha_Agregado = IIf(IsDBNull(dr.Item("Fecha_Agregado")), Date.Now, dr.Item("Fecha_Agregado"))
                .Fecha_Ingreso = IIf(IsDBNull(dr.Item("Fecha_Ingreso")), Nothing, dr.Item("Fecha_Ingreso"))
                .Fecha_Vence = IIf(IsDBNull(dr.Item("Fecha_Vence")), Nothing, dr.Item("Fecha_Vence"))
                .Fecha_Produccion = IIf(IsDBNull(dr.Item("Fecha_Produccion")), Nothing, dr.Item("Fecha_Produccion"))
                .Activo = IIf(IsDBNull(dr.Item("Activo")), False, dr.Item("Activo"))
                .Recibido = IIf(IsDBNull(dr.Item("Recibido")), False, dr.Item("Recibido"))
                .IdRecepcion = IIf(IsDBNull(dr.Item("IdRecepcion")), 0, dr.Item("IdRecepcion"))
                .Bodega_Origen = IIf(IsDBNull(dr.Item("Bodega_Origen")), "", dr.Item("Bodega_Origen"))
                .Bodega_Destino = IIf(IsDBNull(dr.Item("Bodega_Destino")), "", dr.Item("Bodega_Destino"))
                .Codigo_barra = IIf(IsDBNull(dr.Item("codigo_barra")), "", dr.Item("codigo_barra"))
                .Cantidad_UMP = IIf(IsDBNull(dr.Item("cantidad_ump")), "0", dr.Item("cantidad_ump"))
                .Fecha_Procesado_ERP = IIf(IsDBNull(dr.Item("fecha_procesado_erp")), Nothing, dr.Item("fecha_procesado_erp"))
                .Impreso = IIf(IsDBNull(dr.Item("Impreso")), False, dr.Item("Impreso"))
                .SSCC = IIf(IsDBNull(dr.Item("sscc")), "", dr.Item("sscc"))
                .GTIN = IIf(IsDBNull(dr.Item("gtin")), "", dr.Item("gtin"))
                .IdOrdenCompraEnc = IIf(IsDBNull(dr.Item("IdOrdenCompraEnc")), 0, dr.Item("IdOrdenCompraEnc"))
                .IdOrdenCompraDet = IIf(IsDBNull(dr.Item("IdOrdenCompraDet")), 0, dr.Item("IdOrdenCompraDet"))
                .Peso = IIf(IsDBNull(dr.Item("Peso")), 0, dr.Item("Peso"))

            End With

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeI_nav_barras_pallet As clsBeI_nav_barras_pallet, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("i_nav_barras_pallet")
            Ins.Add("idpallet", "@idpallet", DataType.Parametro)
            Ins.Add("codigo", "@codigo", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("camas_por_tarima", "@camas_por_tarima", DataType.Parametro)
            Ins.Add("cajas_por_cama", "@cajas_por_cama", DataType.Parametro)
            Ins.Add("cantidad_presentacion", "@cantidad_presentacion", DataType.Parametro)
            Ins.Add("cantidad_ump", "@cantidad_ump", DataType.Parametro)
            Ins.Add("um_producto", "@um_producto", DataType.Parametro)
            Ins.Add("lote", "@lote", DataType.Parametro)
            Ins.Add("lote_numerico", "@lote_numerico", DataType.Parametro)
            Ins.Add("fecha_agregado", "@fecha_agregado", DataType.Parametro)
            Ins.Add("fecha_ingreso", "@fecha_ingreso", DataType.Parametro)
            Ins.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Ins.Add("fecha_produccion", "@fecha_produccion", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("recibido", "@recibido", DataType.Parametro)
            Ins.Add("idrecepcion", "@idrecepcion", DataType.Parametro)
            Ins.Add("bodega_origen", "@bodega_origen", DataType.Parametro)
            Ins.Add("bodega_destino", "@bodega_destino", DataType.Parametro)
            Ins.Add("codigo_barra", "@codigo_barra", DataType.Parametro)
            Ins.Add("impreso", "@impreso", DataType.Parametro)
            Ins.Add("idordencompraenc", "@idordencompraenc", DataType.Parametro)
            Ins.Add("idordencompradet", "@idordencompradet", DataType.Parametro)

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

            cmd.Parameters.Add(New SqlParameter("@IDPALLET", oBeI_nav_barras_pallet.IdPallet))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeI_nav_barras_pallet.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", clsPublic.Quitar_Caracteres_No_Permitidos(oBeI_nav_barras_pallet.Nombre)))
            cmd.Parameters.Add(New SqlParameter("@CAMAS_POR_TARIMA", oBeI_nav_barras_pallet.Camas_Por_Tarima))
            cmd.Parameters.Add(New SqlParameter("@CAJAS_POR_CAMA", oBeI_nav_barras_pallet.Cajas_Por_Cama))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_PRESENTACION", oBeI_nav_barras_pallet.Cantidad_Presentacion))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_UMP", oBeI_nav_barras_pallet.Cantidad_UMP))
            cmd.Parameters.Add(New SqlParameter("@UM_PRODUCTO", oBeI_nav_barras_pallet.UM_Producto))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeI_nav_barras_pallet.Lote))
            cmd.Parameters.Add(New SqlParameter("@LOTE_NUMERICO", oBeI_nav_barras_pallet.Lote_Numerico))
            cmd.Parameters.Add(New SqlParameter("@FECHA_AGREGADO", oBeI_nav_barras_pallet.Fecha_Agregado))
            cmd.Parameters.Add(New SqlParameter("@FECHA_INGRESO", oBeI_nav_barras_pallet.Fecha_Ingreso))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeI_nav_barras_pallet.Fecha_Vence))
            cmd.Parameters.Add(New SqlParameter("@FECHA_PRODUCCION", oBeI_nav_barras_pallet.Fecha_Produccion))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeI_nav_barras_pallet.Activo))
            cmd.Parameters.Add(New SqlParameter("@RECIBIDO", oBeI_nav_barras_pallet.Recibido))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCION", oBeI_nav_barras_pallet.IdRecepcion))
            cmd.Parameters.Add(New SqlParameter("@BODEGA_ORIGEN", oBeI_nav_barras_pallet.Bodega_Origen))
            cmd.Parameters.Add(New SqlParameter("@BODEGA_DESTINO", oBeI_nav_barras_pallet.Bodega_Destino))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeI_nav_barras_pallet.Codigo_barra))
            cmd.Parameters.Add(New SqlParameter("@IMPRESO", oBeI_nav_barras_pallet.Impreso))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeI_nav_barras_pallet.IdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRADET", oBeI_nav_barras_pallet.IdOrdenCompraDet))

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
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeI_nav_barras_pallet As clsBeI_nav_barras_pallet, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("i_nav_barras_pallet")
            Upd.Add("idpallet", "@idpallet", DataType.Parametro)
            Upd.Add("codigo", "@codigo", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("CAMAS_POR_TARIMA", "@CAMAS_POR_TARIMA", DataType.Parametro)
            Upd.Add("cajas_por_cama", "@cajas_por_cama", DataType.Parametro)
            Upd.Add("cantidad_presentacion", "@cantidad_presentacion", DataType.Parametro)
            Upd.Add("cantidad_ump", "@cantidad_ump", DataType.Parametro)
            Upd.Add("um_producto", "@um_producto", DataType.Parametro)
            Upd.Add("lote", "@lote", DataType.Parametro)
            Upd.Add("lote_numerico", "@lote_numerico", DataType.Parametro)
            Upd.Add("fecha_agregado", "@fecha_agregado", DataType.Parametro)
            Upd.Add("fecha_ingreso", "@fecha_ingreso", DataType.Parametro)
            Upd.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Upd.Add("fecha_produccion", "@fecha_produccion", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("recibido", "@recibido", DataType.Parametro)
            Upd.Add("idrecepcion", "@idrecepcion", DataType.Parametro)
            Upd.Add("bodega_origen", "@bodega_origen", DataType.Parametro)
            Upd.Add("bodega_destino", "@bodega_destino", DataType.Parametro)
            Upd.Add("codigo_barra", "@codigo_barra", DataType.Parametro)
            Upd.Add("impreso", "@impreso", DataType.Parametro)
            Upd.Add("idordencompraenc", "@idordencompraenc", DataType.Parametro)
            Upd.Add("idordencompradet", "@idordencompradet", DataType.Parametro)
            Upd.Where("IdPallet = @IdPallet")

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

            cmd.Parameters.Add(New SqlParameter("@IDPALLET", oBeI_nav_barras_pallet.IdPallet))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeI_nav_barras_pallet.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeI_nav_barras_pallet.Nombre))
            cmd.Parameters.Add(New SqlParameter("@CAMAS_POR_TARIMA", oBeI_nav_barras_pallet.Camas_Por_Tarima))
            cmd.Parameters.Add(New SqlParameter("@CAJAS_POR_CAMA", oBeI_nav_barras_pallet.Cajas_Por_Cama))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_PRESENTACION", oBeI_nav_barras_pallet.Cantidad_Presentacion))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_UMP", oBeI_nav_barras_pallet.Cantidad_UMP))
            cmd.Parameters.Add(New SqlParameter("@UM_PRODUCTO", oBeI_nav_barras_pallet.UM_Producto))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeI_nav_barras_pallet.Lote))
            cmd.Parameters.Add(New SqlParameter("@LOTE_NUMERICO", oBeI_nav_barras_pallet.Lote_Numerico))
            cmd.Parameters.Add(New SqlParameter("@FECHA_AGREGADO", oBeI_nav_barras_pallet.Fecha_Agregado))
            cmd.Parameters.Add(New SqlParameter("@FECHA_INGRESO", oBeI_nav_barras_pallet.Fecha_Ingreso))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeI_nav_barras_pallet.Fecha_Vence))
            cmd.Parameters.Add(New SqlParameter("@FECHA_PRODUCCION", oBeI_nav_barras_pallet.Fecha_Produccion))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeI_nav_barras_pallet.Activo))
            cmd.Parameters.Add(New SqlParameter("@RECIBIDO", oBeI_nav_barras_pallet.Recibido))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCION", oBeI_nav_barras_pallet.IdRecepcion))
            cmd.Parameters.Add(New SqlParameter("@BODEGA_ORIGEN", oBeI_nav_barras_pallet.Bodega_Origen))
            cmd.Parameters.Add(New SqlParameter("@BODEGA_DESTINO", oBeI_nav_barras_pallet.Bodega_Destino))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeI_nav_barras_pallet.Codigo_barra))
            cmd.Parameters.Add(New SqlParameter("@IMPRESO", oBeI_nav_barras_pallet.Impreso))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeI_nav_barras_pallet.IdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRADET", oBeI_nav_barras_pallet.IdOrdenCompraDet))

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
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Eliminar(ByRef oBeI_nav_barras_pallet As clsBeI_nav_barras_pallet, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from I_nav_barras_pallet" &
             "  Where(IdPallet = @IdPallet)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPALLET", oBeI_nav_barras_pallet.IdPallet))

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
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try
    End Function

    Public Shared Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from I_nav_barras_pallet"
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
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Const sp As String = "SELECT * FROM I_nav_barras_pallet "
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
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeI_nav_barras_pallet As clsBeI_nav_barras_pallet) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Obtener = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Const sp As String = "SELECT * FROM I_nav_barras_pallet 
             Where(IdPallet = @IdPallet)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPALLET", oBeI_nav_barras_pallet.IdPallet))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeI_nav_barras_pallet, dt.Rows(0))
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

    Public Shared Function GetAll() As List(Of clsBeI_nav_barras_pallet)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim lReturnList As New List(Of clsBeI_nav_barras_pallet)
            Const sp As String = "SELECT * FROM I_nav_barras_pallet "
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_barras_pallet As New clsBeI_nav_barras_pallet

            For Each dr As DataRow In dt.Rows
                vBeI_nav_barras_pallet = New clsBeI_nav_barras_pallet
                Cargar(vBeI_nav_barras_pallet, dr)
                lReturnList.Add(vBeI_nav_barras_pallet)
            Next

            lTransaction.Commit()

            Return lReturnList

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function GetSingle(ByVal IdPallet As Integer) As clsBeI_nav_barras_pallet

        GetSingle = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Const sp As String = "SELECT * FROM I_nav_barras_pallet Where(IdPallet = @IdPallet) "
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPALLET", IdPallet))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                GetSingle = New clsBeI_nav_barras_pallet
                Cargar(GetSingle, dt.Rows(0))
            End If

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function MaxID() As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdPallet),0) FROM I_nav_barras_pallet"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If
            End Using

            lTransaction.Commit()

            Return lMax

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function MaxID(ByRef lConnection As SqlConnection,
                                 ByRef lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdPallet),0) FROM I_nav_barras_pallet"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If
            End Using

            Return lMax

        Catch ex1 As SqlException
            Throw ex1
        End Try

    End Function

    Public Shared Sub Guardar_Transaccion(ByVal pListBarras_Pallet As List(Of clsBeI_nav_barras_pallet))

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim lMax As Integer = MaxID(lConnection, lTransaction)

            If pListBarras_Pallet IsNot Nothing AndAlso pListBarras_Pallet.Count > 0 Then

                For Each Obj As clsBeI_nav_barras_pallet In pListBarras_Pallet
                    Obj.IdPallet = lMax + 1
                    Insertar(Obj, lConnection, lTransaction)
                Next

            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lTransaction.Dispose()
            lConnection.Dispose()
        End Try

    End Sub

    Public Shared Function Existe(ByVal pCodigoBarraPallet As String) As Boolean

        Existe = False

        Try

            Dim vSQL As String = "SELECT * FROM i_nav_barras_pallet WHERE Codigo_Barra=@Codigo_Barra"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using ltransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@Codigo_Barra", pCodigoBarraPallet)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                            Return True
                        End If

                    End Using

                    ltransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Activos(ByVal pActivo As Boolean) As List(Of clsBeI_nav_barras_pallet)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim lReturnList As New List(Of clsBeI_nav_barras_pallet)

            Dim sp As String = "SELECT * FROM I_nav_barras_pallet WHERE 1 >0 "

            If pActivo Then
                sp += " AND Activo=1"
            Else
                sp += " AND Activo=0"
            End If

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_barras_pallet As New clsBeI_nav_barras_pallet

            For Each dr As DataRow In dt.Rows
                vBeI_nav_barras_pallet = New clsBeI_nav_barras_pallet
                Cargar(vBeI_nav_barras_pallet, dr)
                lReturnList.Add(vBeI_nav_barras_pallet)
            Next

            lTransaction.Commit()

            Return lReturnList

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Get_All_Pendientes_Recepcion(ByVal pActivo As Boolean) As List(Of clsBeI_nav_barras_pallet)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim lReturnList As New List(Of clsBeI_nav_barras_pallet)

            Dim sp As String = "SELECT * FROM I_nav_barras_pallet WHERE 1 >0 "

            If pActivo Then
                sp += " AND Recibido=1"
            Else
                sp += " AND Recibido=0"
            End If

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_barras_pallet As New clsBeI_nav_barras_pallet

            For Each dr As DataRow In dt.Rows
                vBeI_nav_barras_pallet = New clsBeI_nav_barras_pallet
                Cargar(vBeI_nav_barras_pallet, dr)
                lReturnList.Add(vBeI_nav_barras_pallet)
            Next

            lTransaction.Commit()

            Return lReturnList

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            lTransaction.Dispose()
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Get_Single_By_Codigo_Barra_Pallet(ByVal pCodigoBarraPallet As String,
                                                             ByVal pIdBodega As Integer,
                                                             ByRef BeProducto As clsBeProducto) As clsBeI_nav_barras_pallet

        Get_Single_By_Codigo_Barra_Pallet = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Const sp As String = "SELECT * FROM I_nav_barras_pallet Where(codigo_barra = @codigo_barra) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim pBeI_nav_barras_pallet As New clsBeI_nav_barras_pallet

            dad.SelectCommand.Parameters.Add(New SqlParameter("@CODIGO_BARRA", pCodigoBarraPallet))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then

                Cargar(pBeI_nav_barras_pallet, dt.Rows(0))

                BeProducto = clsLnProducto.Get_BeProducto_By_Codigo(pBeI_nav_barras_pallet.Codigo,
                                                                    pIdBodega,
                                                                    lConnection,
                                                                    lTransaction)

                If BeProducto IsNot Nothing Then
                    Get_Single_By_Codigo_Barra_Pallet = pBeI_nav_barras_pallet
                Else
                    Dim vMensaje As String = String.Format("Se obtuvo la información del pallet, pero no se pudo obtener la información del código de producto:{0} asociado al identificador de bodega:{1} ", pBeI_nav_barras_pallet.Codigo, pIdBodega)
                    Throw New Exception(vMensaje)
                End If

            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_Single_Pallet_Ingreso_By_Codigo_Barra_Pallet(ByVal pCodigoBarraPallet As String,
                                                                            ByVal pIdBodega As Integer,
                                                                            ByRef BeProducto As clsBeProducto) As clsBeI_nav_barras_pallet

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Get_Single_Pallet_Ingreso_By_Codigo_Barra_Pallet = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Const sp As String = "SELECT * FROM I_nav_barras_pallet 
             Where(codigo_barra = @codigo_barra AND recibido =0) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim pBeI_nav_barras_pallet As New clsBeI_nav_barras_pallet

            dad.SelectCommand.Parameters.Add(New SqlParameter("@CODIGO_BARRA", pCodigoBarraPallet))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then

                Cargar(pBeI_nav_barras_pallet, dt.Rows(0))

                Get_Single_Pallet_Ingreso_By_Codigo_Barra_Pallet = pBeI_nav_barras_pallet

                If Not pBeI_nav_barras_pallet.Recibido Then

                    BeProducto = clsLnProducto.Get_BeProducto_By_Codigo(pBeI_nav_barras_pallet.Codigo,
                                                                   pIdBodega,
                                                                   lConnection,
                                                                   lTransaction)

                    If BeProducto Is Nothing Then
                        Dim vMensaje As String = String.Format("Se obtuvo la información del pallet, pero no se pudo obtener la información del código de producto:{0} asociado al identificador de bodega:{1} ", pBeI_nav_barras_pallet.Codigo, pIdBodega)
                        Throw New Exception(vMensaje)
                    End If

                    Return pBeI_nav_barras_pallet

                End If

            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lTransaction.Dispose()
            lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Get_All_Pallet_Ingreso_By_Codigo_Barra_Pallet(ByVal pCodigoBarraPallet As String,
                                                                         ByVal pIdBodega As Integer,
                                                                         ByRef BeProducto As clsBeProducto) As List(Of clsBeI_nav_barras_pallet)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Get_All_Pallet_Ingreso_By_Codigo_Barra_Pallet = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM I_nav_barras_pallet 
                                  Where(codigo_barra = @codigo_barra AND Recibido =0) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim pBeI_nav_barras_pallet As New clsBeI_nav_barras_pallet

            dad.SelectCommand.Parameters.Add(New SqlParameter("@CODIGO_BARRA", Trim(pCodigoBarraPallet)))

            Dim dt As New DataTable
            dad.Fill(dt)

            Dim vBeI_nav_barras_pallet As New clsBeI_nav_barras_pallet
            Dim lBeI_nav_barras_pallet As New List(Of clsBeI_nav_barras_pallet)

            If dt.Rows.Count > 0 Then

                For Each dr As DataRow In dt.Rows

                    vBeI_nav_barras_pallet = New clsBeI_nav_barras_pallet

                    Cargar(vBeI_nav_barras_pallet, dr)

                    If Not vBeI_nav_barras_pallet.Recibido Then

                        BeProducto = clsLnProducto.Get_BeProducto_By_Codigo(vBeI_nav_barras_pallet.Codigo,
                                                                           pIdBodega,
                                                                           lConnection,
                                                                           lTransaction)

                        If BeProducto Is Nothing Then
                            Dim vMensaje As String = String.Format("Se obtuvo la información del pallet, pero no se pudo obtener la información del código de producto:{0} asociado al identificador de bodega:{1} ", pBeI_nav_barras_pallet.Codigo, pIdBodega)
                            Throw New Exception(vMensaje)
                        End If
                    Else
                        '#GT27012025: para cuando hay producto que retornar.
                        BeProducto = Nothing
                    End If

                    lBeI_nav_barras_pallet.Add(vBeI_nav_barras_pallet)

                Next

                Get_All_Pallet_Ingreso_By_Codigo_Barra_Pallet = lBeI_nav_barras_pallet

            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lTransaction.Dispose()
            lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Get_Single_Lp_Recibido_By_Codigo_Barra_And_Bodega(ByVal pCodigoBarraPallet As String,
                                                                            ByRef lConnection As SqlConnection,
                                                                            ByRef lTransaction As SqlTransaction) As clsBeI_nav_barras_pallet


        Get_Single_Lp_Recibido_By_Codigo_Barra_And_Bodega = Nothing

        Try

            Const sp As String = "SELECT * FROM I_nav_barras_pallet 
             Where(codigo_barra = @codigo_barra AND (recibido = 1 OR recibido =2) and IdRecepcion <> 0 ) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim pBeI_nav_barras_pallet As New clsBeI_nav_barras_pallet

            dad.SelectCommand.Parameters.Add(New SqlParameter("@CODIGO_BARRA", pCodigoBarraPallet))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeI_nav_barras_pallet, dt.Rows(0))
                Return pBeI_nav_barras_pallet
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_Codigo_Barra_Pallet(ByVal pCodigoBarraPallet As String,
                                                             ByVal pIdBodega As Integer,
                                                             ByVal lConnection As SqlConnection,
                                                             ByVal lTransaction As SqlTransaction) As clsBeProducto

        Get_Single_By_Codigo_Barra_Pallet = Nothing

        Try

            Const sp As String = "SELECT * FROM I_nav_barras_pallet Where(codigo_barra = @codigo_barra and recibido =0) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim pBeI_nav_barras_pallet As New clsBeI_nav_barras_pallet
            Dim BeProducto As New clsBeProducto

            dad.SelectCommand.Parameters.Add(New SqlParameter("@CODIGO_BARRA", pCodigoBarraPallet))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then

                Cargar(pBeI_nav_barras_pallet, dt.Rows(0))

                BeProducto = clsLnProducto.Get_BeProducto_By_Codigo(pBeI_nav_barras_pallet.Codigo,
                                                                    pIdBodega,
                                                                    lConnection,
                                                                    lTransaction)

                If BeProducto IsNot Nothing Then
                    Get_Single_By_Codigo_Barra_Pallet = BeProducto
                Else
                    Dim vMensaje As String = String.Format("Se obtuvo la información del pallet, pero no se pudo obtener la información del código de producto:{0} asociado al identificador de bodega:{1} ", pBeI_nav_barras_pallet.Codigo, pIdBodega)
                    Throw New Exception(vMensaje)
                End If

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Guardar_Pallet_PreImpresion(ByVal pListBarras_Pallet As clsBeI_nav_barras_pallet,
                                                      ByVal lConnection As SqlConnection,
                                                      ByVal lTransaction As SqlTransaction) As Boolean

        Guardar_Pallet_PreImpresion = False

        Try

            Dim lMax As Integer = MaxID(lConnection, lTransaction) + 1
            pListBarras_Pallet.IdPallet = lMax
            Insertar(pListBarras_Pallet, lConnection, lTransaction)

            Guardar_Pallet_PreImpresion = True

        Catch ex As Exception
            Guardar_Pallet_PreImpresion = False
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Shared Function Get_Single_By_Licencia(ByVal pLicencia As String, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Boolean

        Get_Single_By_Licencia = Nothing


        Try

            Const sp As String = "SELECT * FROM i_nav_barras_pallet 
                                  Where(Codigo_Barra = @lic_plate) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@LIC_PLATE", pLicencia))

            Dim dt As New DataTable
            dad.Fill(dt)

            Get_Single_By_Licencia = dt.Rows.Count > 0

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#GT05032026: listar barras_pallet si tienen oc-recepcion
    Public Shared Function Get_All_Activos_By_IdRecepcion(ByVal IdRecepcionEnc As Integer) As List(Of clsBeI_nav_barras_pallet)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim lReturnList As New List(Of clsBeI_nav_barras_pallet)

            Dim sp As String = "SELECT * FROM I_nav_barras_pallet WHERE IdRecepcion=@IdRecepcionEnc "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdRecepcionEnc", IdRecepcionEnc))
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_barras_pallet As New clsBeI_nav_barras_pallet

            For Each dr As DataRow In dt.Rows
                vBeI_nav_barras_pallet = New clsBeI_nav_barras_pallet
                Cargar(vBeI_nav_barras_pallet, dr)
                lReturnList.Add(vBeI_nav_barras_pallet)
            Next

            lTransaction.Commit()

            Return lReturnList

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    '#GT06032026: listar barras_pallet con impreso=1
    Public Shared Function Get_All_By_EstadoImpresion(ByVal Impreso As Boolean) As List(Of clsBeI_nav_barras_pallet)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Get_All_By_EstadoImpresion = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim sp As String = "SELECT IdPallet,
            Codigo,
            Nombre,
            Camas_Por_Tarima,
            Cajas_Por_Cama,
            Cantidad_Presentacion,
            UM_Producto,
            Lote,
            Fecha_Agregado,
            Fecha_Ingreso,
            Fecha_Vence,
            Fecha_Produccion,
            Activo,
            Recibido,
            IdRecepcion,
            Bodega_Origen,
            Bodega_Destino,
            Codigo_Barra,
            Cantidad_UMP,
            Lote_Numerico,
            fecha_procesado_erp,
            sscc,
            gtin,
            Impreso,
            fecha_procesado_erp,
            IdOrdenCompraEnc,
            IdOrdenCompraDet,
            Peso
            FROM I_nav_barras_pallet WHERE ISNULL(Impreso, 0)=@Impreso "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@Impreso", Impreso))
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_barras_pallet As New clsBeI_nav_barras_pallet

            If dt.Rows.Count > 0 Then

                Get_All_By_EstadoImpresion = New List(Of clsBeI_nav_barras_pallet)

                For Each dr As DataRow In dt.Rows
                    vBeI_nav_barras_pallet = New clsBeI_nav_barras_pallet
                    Cargar(vBeI_nav_barras_pallet, dr)
                    Get_All_By_EstadoImpresion.Add(vBeI_nav_barras_pallet)
                Next
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

    '#GT18032026: retonar el objeto barra_pallet a la HH para el proceso de RFID
    Public Shared Function Get_Single_By_Barra_RFID(ByVal pListaCodigoBarraPallet As List(Of String)) As List(Of clsBeI_nav_barras_pallet)

        Get_Single_By_Barra_RFID = New List(Of clsBeI_nav_barras_pallet)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Const sp As String = "SELECT * FROM I_nav_barras_pallet Where(codigo_barra = @codigo_barra) "

            For Each pCodigoBarraPallet As String In pListaCodigoBarraPallet

                Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                Dim dad As New SqlDataAdapter(cmd)
                Dim pBeI_nav_barras_pallet As New clsBeI_nav_barras_pallet

                dad.SelectCommand.Parameters.Add(New SqlParameter("@CODIGO_BARRA", pCodigoBarraPallet))

                Dim dt As New DataTable
                dad.Fill(dt)

                If dt.Rows.Count = 1 Then
                    Cargar(pBeI_nav_barras_pallet, dt.Rows(0))
                    pBeI_nav_barras_pallet.Activo = True
                Else
                    pBeI_nav_barras_pallet.Codigo_barra = pCodigoBarraPallet
                    pBeI_nav_barras_pallet.Activo = False
                End If

                Get_Single_By_Barra_RFID.Add(pBeI_nav_barras_pallet)

            Next

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_CodigoProducto_By_Barra_EPC(ByVal pBarra_Epc As String,
                                                           ByRef lConnection As SqlConnection,
                                                           ByRef lTransaction As SqlTransaction) As String


        Get_CodigoProducto_By_Barra_EPC = ""

        Try

            Const sp As String = "select Codigo from i_nav_barras_pallet where Codigo_Barra=@pBarra_epc "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)


            dad.SelectCommand.Parameters.Add(New SqlParameter("@pBarra_epc", pBarra_Epc))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Get_CodigoProducto_By_Barra_EPC = dt.Rows(0)("Codigo").ToString()
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    '#GT09042026: el tag no debe tener una salida previa.
    Public Shared Function Obtener_EPC_Con_Existencia_Para_Salida(ByVal pListaCodigoBarraPallet As List(Of String)) As List(Of clsBeI_nav_barras_pallet)

        Obtener_EPC_Con_Existencia_Para_Salida = New List(Of clsBeI_nav_barras_pallet)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Const spActivo As String =
                "SELECT p.* " &
                "FROM I_nav_barras_pallet p " &
                "WHERE p.codigo_barra = @codigo_barra " &
                "AND EXISTS ( " &
                "    SELECT 1 " &
                "    FROM i_nav_barras_rfid_det d " &
                "    INNER JOIN i_nav_barras_rfid_enc e ON e.IdRFIDEnc = d.IdRFIDEnc " &
                "    WHERE d.Barra_epc = p.codigo_barra " &
                "      AND e.Tipo = 'ING' " &
                ") " &
                "AND NOT EXISTS ( " &
                "    SELECT 1 " &
                "    FROM i_nav_barras_rfid_det d " &
                "    INNER JOIN i_nav_barras_rfid_enc e ON e.IdRFIDEnc = d.IdRFIDEnc " &
                "    WHERE d.Barra_epc = p.codigo_barra " &
                "      AND e.Tipo = 'SAL' " &
                ")"

            Const spYaSalio As String =
                "SELECT TOP 1 1 " &
                "FROM I_nav_barras_pallet p " &
                "INNER JOIN i_nav_barras_rfid_det d ON d.Barra_epc = p.codigo_barra " &
                "INNER JOIN i_nav_barras_rfid_enc e ON e.IdRFIDEnc = d.IdRFIDEnc " &
                "WHERE p.codigo_barra = @codigo_barra " &
                "  AND e.Tipo = 'SAL' "

            For Each pCodigoBarraPallet As String In pListaCodigoBarraPallet

                Dim pBeI_nav_barras_pallet As New clsBeI_nav_barras_pallet()

                Dim cmd As New SqlCommand(spActivo, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                Dim dad As New SqlDataAdapter(cmd)
                dad.SelectCommand.Parameters.Add(New SqlParameter("@CODIGO_BARRA", pCodigoBarraPallet))

                Dim dt As New DataTable
                dad.Fill(dt)

                If dt.Rows.Count = 1 Then
                    Cargar(pBeI_nav_barras_pallet, dt.Rows(0))
                    pBeI_nav_barras_pallet.Activo = True
                    pBeI_nav_barras_pallet.Recibido = False
                Else
                    pBeI_nav_barras_pallet.Codigo_barra = pCodigoBarraPallet
                    pBeI_nav_barras_pallet.Activo = False

                    Dim cmdYaSalio As New SqlCommand(spYaSalio, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                    cmdYaSalio.Parameters.Add(New SqlParameter("@CODIGO_BARRA", pCodigoBarraPallet))

                    Dim existeSalidaPrevia As Object = cmdYaSalio.ExecuteScalar()

                    If Not existeSalidaPrevia Is Nothing Then
                        pBeI_nav_barras_pallet.Recibido = True
                    Else
                        pBeI_nav_barras_pallet.Recibido = False
                    End If
                End If

                Obtener_EPC_Con_Existencia_Para_Salida.Add(pBeI_nav_barras_pallet)

            Next

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    '#GT20042026: el tag existe y no tiene ingreso previo
    Public Shared Function Lista_Tags_SinIgreso_Valida(ByVal pListaCodigoBarraPallet As List(Of String)) As List(Of clsBeI_nav_barras_pallet)
        Lista_Tags_SinIgreso_Valida = New List(Of clsBeI_nav_barras_pallet)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Const sp As String = "
            SELECT  p.*,
                    CASE 
                        WHEN EXISTS (
                            SELECT 1
                            FROM i_nav_barras_rfid_det d
                            INNER JOIN i_nav_barras_rfid_enc e 
                                ON e.IdRFIDEnc = d.IdRFIDEnc
                            INNER JOIN i_nav_barras_rfid_tipo_mov tm
                                ON tm.IdrfidTipoMov = e.Tipo
                            WHERE d.barra_epc = p.codigo_barra
                              AND e.Tipo = 1
                        )
                        THEN 1
                        ELSE 0
                    END AS TieneIngresoPrevio
            FROM I_nav_barras_pallet p
            WHERE p.codigo_barra = @codigo_barra"

            For Each pCodigoBarraPallet As String In pListaCodigoBarraPallet

                Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                Dim dad As New SqlDataAdapter(cmd)
                Dim pBeI_nav_barras_pallet As New clsBeI_nav_barras_pallet

                dad.SelectCommand.Parameters.Add(New SqlParameter("@codigo_barra", pCodigoBarraPallet))

                Dim dt As New DataTable
                dad.Fill(dt)

                If dt.Rows.Count = 1 Then
                    Cargar(pBeI_nav_barras_pallet, dt.Rows(0))

                    If Convert.ToInt32(dt.Rows(0)("TieneIngresoPrevio")) = 1 Then
                        pBeI_nav_barras_pallet.Activo = False
                    Else
                        pBeI_nav_barras_pallet.Activo = True
                    End If
                Else
                    pBeI_nav_barras_pallet.Codigo_barra = pCodigoBarraPallet
                    pBeI_nav_barras_pallet.Activo = False
                End If

                Lista_Tags_SinIgreso_Valida.Add(pBeI_nav_barras_pallet)

            Next

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function GetAll_By_IdProducto_And_Barra_And_Idbodega(ByVal pIdProductoBodega As Integer, ByVal pCodigo As String,
                                                                       ByVal pIdBodega As Integer,
                                                                       ByRef lConnection As SqlConnection,
                                                                       ByRef lTransaction As SqlTransaction) As List(Of clsBeI_nav_barras_pallet)


        GetAll_By_IdProducto_And_Barra_And_Idbodega = Nothing

        Try

            Const sp As String = "SELECT IdPallet,
                                    nav.Codigo,
                                    nav.Nombre,
                                    Camas_Por_Tarima,
                                    Cajas_Por_Cama,
                                    Cantidad_Presentacion,
                                    UM_Producto,
                                    Lote,
                                    Fecha_Agregado,
                                    Fecha_Ingreso,
                                    Fecha_Vence,
                                    Fecha_Produccion,
                                    nav.activo,
                                    Recibido,
                                    IdRecepcion,
                                    Bodega_Origen,
                                    Bodega_Destino,
                                    nav.codigo_barra,
                                    Cantidad_UMP,
                                    Lote_Numerico,
                                    fecha_procesado_erp,
                                    sscc,
                                    gtin,
                                    Impreso,
                                    fecha_procesado_erp,
                                    IdOrdenCompraEnc,
                                    IdOrdenCompraDet,
                                    Peso
                                    FROM I_nav_barras_pallet nav
                                    inner join producto pr 
								    on nav.codigo=pr.codigo inner join producto_bodega pb
								    on pr.IdProducto = pb.IdProducto where (nav.Codigo=@pCodigo 
                                    and pb.IdBodega=@pIdBodega) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@pIdBodega", pIdBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@pIdProductoBodega", pIdProductoBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@pCodigo", pCodigo))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 AndAlso dt IsNot Nothing Then

                GetAll_By_IdProducto_And_Barra_And_Idbodega = New List(Of clsBeI_nav_barras_pallet)

                For Each row As DataRow In dt.Rows

                    Dim pBeI_nav_barras_pallet As New clsBeI_nav_barras_pallet

                    Cargar(pBeI_nav_barras_pallet, row)

                    GetAll_By_IdProducto_And_Barra_And_Idbodega.Add(pBeI_nav_barras_pallet)

                Next

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdOrdenCompraEnc_Det(ByVal pIdOrdenCompraEnc As Integer,
                                                           ByVal pIdOrdenCompraDet As Integer) As List(Of clsBeI_nav_barras_pallet)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim lReturnList As New List(Of clsBeI_nav_barras_pallet)

            Dim sp As String = "SELECT * FROM I_nav_barras_pallet 
                                WHERE IdOrdenCompraEnc = @IdOrdenCompraEnc AND 
                                      IdOrdenCompraDet = @IdOrdenCompraDet AND 
                                      recibido = 0 "

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            cmd.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)
            cmd.Parameters.AddWithValue("@IdOrdenCompraDet", pIdOrdenCompraDet)
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_barras_pallet As New clsBeI_nav_barras_pallet

            For Each dr As DataRow In dt.Rows
                vBeI_nav_barras_pallet = New clsBeI_nav_barras_pallet
                Cargar(vBeI_nav_barras_pallet, dr)
                lReturnList.Add(vBeI_nav_barras_pallet)
            Next

            lTransaction.Commit()

            Return lReturnList

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            lTransaction.Dispose()
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Get_Count_By_IdOrdenCompraEnc_And_IdOrdenCompraDet(ByVal pIdOrdenCompraEnc As Integer,
                                                                              ByVal pIdOrdenCompraDet As Integer) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim sp As String =
            "SELECT COUNT(*) 
               FROM I_nav_barras_pallet
              WHERE IdOrdenCompraEnc = @IdOrdenCompraEnc
                AND IdOrdenCompraDet = @IdOrdenCompraDet
                AND recibido = 0"

            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {
            .CommandType = CommandType.Text
        }

            cmd.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOrdenCompraEnc)
            cmd.Parameters.AddWithValue("@IdOrdenCompraDet", pIdOrdenCompraDet)

            Dim result As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            lTransaction.Commit()

            Return result

        Catch ex As Exception

            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex

        Finally

            If lTransaction IsNot Nothing Then lTransaction.Dispose()

            If lConnection.State = ConnectionState.Open Then
                lConnection.Close()
            End If

            lConnection.Dispose()

        End Try

    End Function

End Class