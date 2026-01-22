Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnVW_Despacho_Rep

    Public Shared Sub Cargar(ByRef oBeTmp_VW_Despacho_Rep As clsBeVW_Despacho_Rep, ByRef dr As DataRow)

        Try

            With oBeTmp_VW_Despacho_Rep
                .IdPickingUbic = IIf(IsDBNull(dr.Item("IdPickingUbic")), 0, dr.Item("IdPickingUbic"))
                .IdStock = IIf(IsDBNull(dr.Item("IdStock")), 0, dr.Item("IdStock"))
                .IdPedidoDet = IIf(IsDBNull(dr.Item("IdPedidoDet")), 0, dr.Item("IdPedidoDet"))
                .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                .IdProductoEstado = IIf(IsDBNull(dr.Item("IdProductoEstado")), 0, dr.Item("IdProductoEstado"))
                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .IdUnidadMedida = IIf(IsDBNull(dr.Item("IdUnidadMedida")), 0, dr.Item("IdUnidadMedida"))
                .IdRecepcion = IIf(IsDBNull(dr.Item("IdRecepcion")), 0, dr.Item("IdRecepcion"))
                .IdDespachoEnc = IIf(IsDBNull(dr.Item("IdDespachoEnc")), 0, dr.Item("IdDespachoEnc"))
                .IdDespachoDet = IIf(IsDBNull(dr.Item("IdDespachoDet")), 0, dr.Item("IdDespachoDet"))
                .IdPedidoEnc = IIf(IsDBNull(dr.Item("IdPedidoEnc")), 0, dr.Item("IdPedidoEnc"))
                .Propietario = IIf(IsDBNull(dr.Item("Propietario")), "", dr.Item("Propietario"))
                .Codigo_Producto = IIf(IsDBNull(dr.Item("Codigo_Producto")), "", dr.Item("Codigo_Producto"))
                .Nombre_Producto = IIf(IsDBNull(dr.Item("Nombre_Producto")), "", dr.Item("Nombre_Producto"))
                .UM = IIf(IsDBNull(dr.Item("UM")), "", dr.Item("UM"))
                .Presentacion = IIf(IsDBNull(dr.Item("Presentacion")), "", dr.Item("Presentacion"))
                .Factor = IIf(IsDBNull(dr.Item("factor")), 0.0, dr.Item("factor"))
                .Estado = IIf(IsDBNull(dr.Item("Estado")), "", dr.Item("Estado"))
                .Lote = IIf(IsDBNull(dr.Item("lote")), "", dr.Item("lote"))
                .Licencia = IIf(IsDBNull(dr.Item("licencia")), "", dr.Item("licencia"))
                .Vence = IIf(IsDBNull(dr.Item("Vence")), Date.Now, dr.Item("Vence"))
                .Ubicacion_Origen = IIf(IsDBNull(dr.Item("Ubicacion_Origen")), "", dr.Item("Ubicacion_Origen"))
                .Cantidad_pickeada = IIf(IsDBNull(dr.Item("cantidad_pickeada")), 0.0, dr.Item("cantidad_pickeada"))
                .Cantidad_verificada = IIf(IsDBNull(dr.Item("cantidad_verificada")), 0.0, dr.Item("cantidad_verificada"))
                .Peso_Pickeado = IIf(IsDBNull(dr.Item("Peso_Pickeado")), 0.0, dr.Item("Peso_Pickeado"))
                .Peso_Verificado = IIf(IsDBNull(dr.Item("Peso_Verificado")), 0.0, dr.Item("Peso_Verificado"))
                .CantidadDespachada = IIf(IsDBNull(dr.Item("CantidadDespachada")), 0.0, dr.Item("CantidadDespachada"))
                .PesoDespachado = IIf(IsDBNull(dr.Item("PesoDespachado")), 0.0, dr.Item("PesoDespachado"))
                .Encontrado = IIf(IsDBNull(dr.Item("Encontrado")), False, dr.Item("Encontrado"))
                .Acepto = IIf(IsDBNull(dr.Item("Acepto")), False, dr.Item("Acepto"))
                .No_Documento_WMS = IIf(IsDBNull(dr.Item("No_Documento_WMS")), 0, dr.Item("No_Documento_WMS"))
                .No_Referencia = IIf(IsDBNull(dr.Item("No_Referencia")), "", dr.Item("No_Referencia"))
                .Codigo_Cliente = IIf(IsDBNull(dr.Item("Codigo_Cliente")), "", dr.Item("Codigo_Cliente"))
                .Nombre_Cliente = IIf(IsDBNull(dr.Item("Nombre_Cliente")), "", dr.Item("Nombre_Cliente"))
                .Idubicacionvirtual = IIf(IsDBNull(dr.Item("idubicacionvirtual")), 0, dr.Item("idubicacionvirtual"))
                .Es_bodega_recepcion = IIf(IsDBNull(dr.Item("es_bodega_recepcion")), False, dr.Item("es_bodega_recepcion"))
                .Es_bodega_traslado = IIf(IsDBNull(dr.Item("es_bodega_traslado")), False, dr.Item("es_bodega_traslado"))
                .No_pase = IIf(IsDBNull(dr.Item("no_pase")), 0, dr.Item("no_pase"))
                .Observacion = IIf(IsDBNull(dr.Item("observacion")), "", dr.Item("observacion"))
                .Numero = IIf(IsDBNull(dr.Item("numero")), 0, dr.Item("numero"))
                .Marchamo = IIf(IsDBNull(dr.Item("marchamo")), "", dr.Item("marchamo"))
                .Codigo_Ruta = IIf(IsDBNull(dr.Item("Codigo_Ruta")), "", dr.Item("Codigo_Ruta"))
                .Nombre_Ruta = IIf(IsDBNull(dr.Item("Nombre_Ruta")), "", dr.Item("Nombre_Ruta"))
                .Placa_Vehiculo = IIf(IsDBNull(dr.Item("Placa_Vehiculo")), "", dr.Item("Placa_Vehiculo"))
                .Placa_Comercial = IIf(IsDBNull(dr.Item("Placa_Comercial")), "", dr.Item("Placa_Comercial"))
                .Marca_Vehiculo = IIf(IsDBNull(dr.Item("Marca_Vehiculo")), "", dr.Item("Marca_Vehiculo"))
                .Modelo_Vehiculo = IIf(IsDBNull(dr.Item("Modelo_Vehiculo")), "", dr.Item("Modelo_Vehiculo"))
                .Nombre_Piloto = IIf(IsDBNull(dr.Item("Nombre_Piloto")), "", dr.Item("Nombre_Piloto"))
                .Apellido_Piloto = IIf(IsDBNull(dr.Item("Apellido_Piloto")), "", dr.Item("Apellido_Piloto"))
                .No_Carnet_Piloto = IIf(IsDBNull(dr.Item("No_Carnet_Piloto")), "", dr.Item("No_Carnet_Piloto"))
                .No_Licencia_Piloto = IIf(IsDBNull(dr.Item("No_Licencia_Piloto")), "", dr.Item("No_Licencia_Piloto"))
                .Fecha = IIf(IsDBNull(dr.Item("fecha")), Now, dr.Item("fecha"))
                '#GT23112022_1500: campos DyD
                .clasificacion = IIf(IsDBNull(dr.Item("clasificacion")), "", dr.Item("clasificacion"))
                .marca = IIf(IsDBNull(dr.Item("marca")), "", dr.Item("marca"))
                .familia = IIf(IsDBNull(dr.Item("familia")), "", dr.Item("familia"))
                .parametro_a = IIf(IsDBNull(dr.Item("parametro_a")), "", dr.Item("parametro_a"))
                .parametro_b = IIf(IsDBNull(dr.Item("parametro_b")), "", dr.Item("parametro_b"))
                '#GT16082023: campo de poliza de salida
                .numero_orden_pedido = IIf(IsDBNull(dr.Item("numero_orden_pedido")), "", dr.Item("numero_orden_pedido"))
                .codigo_poliza_pedido = IIf(IsDBNull(dr.Item("codigo_poliza_pedido")), "", dr.Item("codigo_poliza_pedido"))
                '#GT18082023: campo de poliza de ingreso
                .numero_orden_ingreso = IIf(IsDBNull(dr.Item("numero_orden_ingreso")), "", dr.Item("numero_orden_ingreso"))
                .codigo_poliza_ingreso = IIf(IsDBNull(dr.Item("codigo_poliza_ingreso")), "", dr.Item("codigo_poliza_ingreso"))
                '#GT30092024: campos CLC
                .codigo_regimen_salida = IIf(IsDBNull(dr.Item("codigo_regimen_salida")), "", dr.Item("codigo_regimen_salida"))
                .placa_contenedor_salida = IIf(IsDBNull(dr.Item("placa_contenedor_salida")), "", dr.Item("placa_contenedor_salida"))
                .Dua_salida = IIf(IsDBNull(dr.Item("dua_salida")), "", dr.Item("dua_salida"))
                .Talla = IIf(IsDBNull(dr.Item("Talla")), "", dr.Item("Talla"))
                .Color = IIf(IsDBNull(dr.Item("Color")), "", dr.Item("Color"))

            End With

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Eliminar(ByRef oBeTmp_VW_Despacho_Rep As clsBeVW_Despacho_Rep, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Tmp_VW_Despacho_Rep"

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

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Tmp_VW_Despacho_Rep"
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

            Const sp As String = "SELECT * FROM Tmp_VW_Despacho_Rep"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

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

    Public Shared Function Obtener(ByRef oBeTmp_VW_Despacho_Rep As clsBeVW_Despacho_Rep) As Boolean

        Try

            Const sp As String = "SELECT * FROM Tmp_VW_Despacho_Rep "

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTmp_VW_Despacho_Rep, dt.Rows(0))
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

    Public Shared Function GetAll() As List(Of clsBeVW_Despacho_Rep)

        Try

            Dim lReturnList As New List(Of clsBeVW_Despacho_Rep)
            Const sp As String = "SELECT * FROM VW_Despacho_Rep "
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTmp_VW_Despacho_Rep As New clsBeVW_Despacho_Rep

            For Each dr As DataRow In dt.Rows
                vBeTmp_VW_Despacho_Rep = New clsBeVW_Despacho_Rep
                Cargar(vBeTmp_VW_Despacho_Rep, dr)
                lReturnList.Add(vBeTmp_VW_Despacho_Rep)
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

    Public Shared Function GetSingle(ByRef pBeTmp_VW_Despacho_Rep As clsBeVW_Despacho_Rep)

        Try

            Const sp As String = "SELECT * FROM Tmp_VW_Despacho_Rep"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTmp_VW_Despacho_Rep, dt.Rows(0))
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

            Const sp As String = "SELECT * FROM Tmp_VW_Despacho_Rep "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

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
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_Rango_Fechas(ByVal pFechaDel As Date, ByVal pFechaAl As Date, ByVal pBodega As clsBeBodega) As List(Of clsBeVW_Despacho_Rep)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim lReturnList As New List(Of clsBeVW_Despacho_Rep)

            Dim vSQL As String = String.Format("SELECT * FROM VW_Despacho_Rep_Det_I 
                                               WHERE IdBodega =@pIdBodega and cast(Fecha AS DATE) BETWEEN {0} AND {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))


            If pBodega.Es_Bodega_Fiscal Then
                vSQL += " And poliza_salida_activa=1"
            End If

            vSQL += " Order By fecha desc"

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@pIdBodega", pBodega.IdBodega)
            Dim dt As New DataTable

            dad.Fill(dt)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim vBeTmp_VW_Despacho_Rep As New clsBeVW_Despacho_Rep

                For Each dr As DataRow In dt.Rows
                    vBeTmp_VW_Despacho_Rep = New clsBeVW_Despacho_Rep
                    Cargar(vBeTmp_VW_Despacho_Rep, dr)
                    lReturnList.Add(vBeTmp_VW_Despacho_Rep)
                Next
            End If

            lTransaction.Commit()

            Return lReturnList

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    ''' <summary>
    ''' #EJC20190313: Devuelve el último lote despachado para un cliente  y producto.
    ''' </summary>
    ''' <param name="pIdCliente">Id de cliente</param>
    ''' <param name="pIdProductoBodega">Id de producto</param>
    ''' <returns>El número de lote</returns>
    Public Shared Function Get_Ultimo_Lote_By_IdCliente(ByVal pIdCliente As Integer,
                                                        ByVal pIdProductoBodega As Integer) As String

        Dim lConection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Get_Ultimo_Lote_By_IdCliente = ""

        Try

            Dim vSQL As String = "SELECT TOP(1) lote FROM VW_Lotes_Despacho
                                  WHERE IdCliente = @pIdCliente 
                                  AND IdProductoBodega = @pIdProductoBodega  "

            vSQL += " order by fecha_despacho desc "


            lConection.Open() : lTransaction = lConection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim cmd As New SqlCommand(vSQL, lConection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@pIdCliente", pIdCliente)
            dad.SelectCommand.Parameters.AddWithValue("@pIdProductoBodega", pIdProductoBodega)
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then

                Dim vLoteAlfanumerico As String = ""

                vLoteAlfanumerico = IIf(IsDBNull(dt.Rows(0).Item("lote")), "0", dt.Rows(0).Item("lote"))

                Dim vSQL1 As String = "SELECT TOP(1) lote_numerico 
                                       FROM trans_re_det_lote_num 
                                       WHERE lote = @LoteAlfanumerico
                                       AND IdProductoBodega = @pIdProductoBodega "

                vSQL1 += " order by fechaingreso desc "


                Dim cmd1 As New SqlCommand(vSQL1, lConection, lTransaction) With {.CommandType = CommandType.Text}
                Dim dad1 As New SqlDataAdapter(cmd1)
                dad1.SelectCommand.Parameters.AddWithValue("@LoteAlfanumerico", vLoteAlfanumerico)
                dad1.SelectCommand.Parameters.AddWithValue("@pIdProductoBodega", pIdProductoBodega)
                Dim dt1 As New DataTable
                dad1.Fill(dt1)

                If dt1.Rows.Count > 0 Then
                    'Equivalente lote numérico.
                    Get_Ultimo_Lote_By_IdCliente = IIf(IsDBNull(dt1.Rows(0).Item("lote_numerico")), "0", dt1.Rows(0).Item("lote_numerico"))
                End If

            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConection Is Nothing AndAlso lConection.State = ConnectionState.Open Then lConection.Close()
        End Try

    End Function


    Public Shared Function Get_Ultimo_Lote_By_IdCliente(ByVal pIdCliente As Integer,
                                                        ByVal pIdProductoBodega As Integer,
                                                        ByVal lConnection As SqlConnection,
                                                        ByVal lTransaction As SqlTransaction) As String

        Get_Ultimo_Lote_By_IdCliente = ""

        Try

            Dim vSQL As String = "SELECT TOP(1) lote FROM VW_Lotes_Despacho
                                  WHERE IdCliente = @pIdCliente 
                                  AND IdProductoBodega = @pIdProductoBodega  "

            vSQL += " order by fecha_despacho desc "

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@pIdCliente", pIdCliente)
            dad.SelectCommand.Parameters.AddWithValue("@pIdProductoBodega", pIdProductoBodega)
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then

                Dim vLoteAlfanumerico As String = ""

                vLoteAlfanumerico = IIf(IsDBNull(dt.Rows(0).Item("lote")), "0", dt.Rows(0).Item("lote"))

                Dim vSQL1 As String = "SELECT TOP(1) lote_numerico 
                                       FROM trans_re_det_lote_num 
                                       WHERE lote = @LoteAlfanumerico
                                       AND IdProductoBodega = @pIdProductoBodega "

                vSQL1 += " order by fechaingreso desc "


                Dim cmd1 As New SqlCommand(vSQL1, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                Dim dad1 As New SqlDataAdapter(cmd1)
                dad1.SelectCommand.Parameters.AddWithValue("@LoteAlfanumerico", vLoteAlfanumerico)
                dad1.SelectCommand.Parameters.AddWithValue("@pIdProductoBodega", pIdProductoBodega)
                Dim dt1 As New DataTable
                dad1.Fill(dt1)

                If dt1.Rows.Count > 0 Then
                    'Equivalente lote numérico.
                    Get_Ultimo_Lote_By_IdCliente = IIf(IsDBNull(dt1.Rows(0).Item("lote_numerico")), "0", dt1.Rows(0).Item("lote_numerico"))
                End If

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function


End Class
